//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : BUSINESS LOGIC FILE//[VEHICLE MAINTENANCE]
//CREATION DATE : 19-NOV-2010      
//CREATED BY    : PRAKASH RADHWANI
//MODIFY  BY    : MRUNAL SINGH
//MODIFIED DATE : 09-09-2014
//MODIFIED DESC :
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
            public class VMController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                #region VM
                #region Driver Master

                public int AddUpdateDriverMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_DNO", objVM.DNO);
                        objParams[1] = new SqlParameter("@P_DNAME", objVM.DNAME);
                        objParams[2] = new SqlParameter("@P_PHONE", objVM.DPHONE);
                        objParams[3] = new SqlParameter("@P_DADD1", objVM.DADD1);
                        objParams[4] = new SqlParameter("@P_DADD2", objVM.DADD2);
                        objParams[5] = new SqlParameter("@P_D_DRIVING_LICENCE_TYPE", objVM.D_DRIVING_LICENCE_TYPE);
                        objParams[6] = new SqlParameter("@P_D_DRIVING_LICENCE_NO", objVM.D_DRIVING_LICENCE_NO);

                        if (objVM.D_DRIVING_LICENCE_FROM_DATE != DateTime.MinValue)
                        {
                            objParams[7] = new SqlParameter("@P_D_DRIVING_LICENCE_FROM_DATE", objVM.D_DRIVING_LICENCE_FROM_DATE);
                        }
                        else
                        {
                            objParams[7] = new SqlParameter("@P_D_DRIVING_LICENCE_FROM_DATE", DBNull.Value);
                        }
                        if (objVM.D_DRIVING_LICENCE_EXPIRY_DATE != DateTime.MinValue)
                        {
                            objParams[8] = new SqlParameter("@P_D_DRIVING_LICENCE_EXPIRY_DATE", objVM.D_DRIVING_LICENCE_EXPIRY_DATE);
                        }
                        else
                        {
                            objParams[8] = new SqlParameter("@P_D_DRIVING_LICENCE_EXPIRY_DATE", DBNull.Value);
                        }
                        objParams[9] = new SqlParameter("@P_D_CATEGORY", objVM.D_CATEGORY);
                        objParams[10] = new SqlParameter("@P_DRI_CON_TYPE", objVM.DRI_CON_TYPE);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_DRIVERMAS_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddDriverMas->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdDriverMas(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DNO", objVM.DNO);
                        objParams[1] = new SqlParameter("@P_DNAME", objVM.DNAME);
                        objParams[2] = new SqlParameter("@P_PHONE", objVM.DPHONE);
                        objParams[3] = new SqlParameter("@P_DADD1", objVM.DADD1);
                        objParams[4] = new SqlParameter("@P_DADD2", objVM.DADD2);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_DRIVERMAS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdDriverMas->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetDriverMasByDNo(int DNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DNO", DNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_DRIVERMAS_GETBY_DNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetDriverMas-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetDriverMasAll()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_DRIVERMAS_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetDriverMasAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Suppiler Master

                public long AddUpdateSuppilerMaster(VM objVM)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SUPPILER_ID", objVM.SUPPILER_ID);
                        objParams[1] = new SqlParameter("@P_SUPPILER_NAME ", objVM.SUPPILER_NAME);
                        objParams[2] = new SqlParameter("@P_CONTACT_ADDRESS ", objVM.CONTACT_ADDRESS);
                        objParams[3] = new SqlParameter("@P_CONTACT_NUMBER", objVM.CONTACT_NUMBER);
                        objParams[4] = new SqlParameter("@P_CONTACT_PERSON", objVM.CONTACT_PERSON);
                        objParams[5] = new SqlParameter("@P_IS_ACTIVE", objVM.IS_ACTIVE);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SP_SUPPILER_MASTER_INSERT_UPDATE", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }


                #endregion

                #region Vehicle Hire Master

                public long AddUpdateVehicleHireMaster(VM objVM)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_VEHICLE_ID", objVM.VEHICLE_ID);
                        objParams[1] = new SqlParameter("@P_SUPPILER_ID", objVM.SUPPILER_ID);
                        objParams[2] = new SqlParameter("@P_VEHICLE_NAME ", objVM.VEHICLE_NAME);
                        objParams[3] = new SqlParameter("@P_FROM_LOCATION ", objVM.FROM_LOCATION);
                        objParams[4] = new SqlParameter("@P_TO_LOCATION", objVM.TO_LOCATION);
                        objParams[5] = new SqlParameter("@P_IS_ACTIVE", objVM.IS_ACTIVE);
                        objParams[6] = new SqlParameter("@P_HIRE_TYPE", objVM.HIRE_TYPE);
                        objParams[7] = new SqlParameter("@P_HIRE_RATE", objVM.HIRE_RATE);
                        objParams[8] = new SqlParameter("@P_REGNO", objVM.REGNO);
                        objParams[9] = new SqlParameter("@P_FDATE", objVM.FDATE);
                        objParams[10] = new SqlParameter("@P_TDATE", objVM.TDATE);
                        objParams[11] = new SqlParameter("@P_VEHICLE_TYPE", objVM.VTID);
                        objParams[12] = new SqlParameter("@P_VTAC", objVM.VTAC);
                        objParams[13] = new SqlParameter("@P_VEHICLE_NUMBER", objVM.VEHICLE_NUMBER);

                        objParams[14] = new SqlParameter("@P_STATUS", objVM.Status);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SP_VEHICLE_HIRE_MASTER_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateVehicleHireMaster->" + ex.ToString());
                    }
                    return retstatus;
                }


                #endregion

                #region Vehicle Bill Entry

                public long AddUpdateVehicleBillEntry(VM objVM)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_BILL_ID", objVM.BILL_ID);
                        objParams[1] = new SqlParameter("@P_SUPPILER_ID", objVM.SUPPILER_ID);
                        objParams[2] = new SqlParameter("@P_VEHICLE_ID", objVM.VEHICLE_ID);
                        objParams[3] = new SqlParameter("@P_BILL_AMOUNT", objVM.BILL_AMOUNT);
                        objParams[4] = new SqlParameter("@P_BILL_FROM_DATE ", objVM.BILL_FROM_DATE);
                        objParams[5] = new SqlParameter("@P_BILL_TO_DATE ", objVM.BILL_TO_DATE);
                        objParams[6] = new SqlParameter("@P_HIKE_PRICE", objVM.HIKE_PRICE);
                        objParams[7] = new SqlParameter("@P_HIRE_FOR", objVM.HIRE_FOR);
                        objParams[8] = new SqlParameter("@P_FROM_TIME", objVM.FROM_TIME);
                        objParams[9] = new SqlParameter("@P_TO_TIME", objVM.TO_TIME);
                        objParams[10] = new SqlParameter("@P_HIRED_BY", objVM.HIRED_BY);
                        objParams[11] = new SqlParameter("@P_TOUR_PURPOSE", objVM.TOUR_PURPOSE);
                        objParams[12] = new SqlParameter("@P_VISIT_PLACE", objVM.VISIT_PLACE);
                        objParams[13] = new SqlParameter("@P_EXTRA_AMOUNT", objVM.EXTRA_AMOUNT);
                        objParams[14] = new SqlParameter("@P_EXTRA_DISTANCE_AMOUNT", objVM.EXTRA_DISTANCE_AMOUNT);
                        objParams[15] = new SqlParameter("@P_EXTRA_TIME_AMOUNT", objVM.EXTRA_TIME_AMOUNT);
                        objParams[16] = new SqlParameter("@P_TOUR_TOTAL_AMOUNT", objVM.TOUR_TOTAL_AMOUNT);
                        objParams[17] = new SqlParameter("@P_PAID_AMOUNT", objVM.PAID_AMOUNT);
                        objParams[18] = new SqlParameter("@P_BALANCE_AMOUNT", objVM.BALANCE_AMOUNT);
                        objParams[19] = new SqlParameter("@P_ROUTE_FROM ", objVM.ROUTE_FROM);
                        objParams[20] = new SqlParameter("@P_ROUTE_TO ", objVM.ROUTE_TO);
                        objParams[21] = new SqlParameter("@P_TRIP_TYPE ", objVM.TRIP_TYPE);
                        objParams[22] = new SqlParameter("@P_REMARK ", objVM.REMARK);
                        objParams[23] = new SqlParameter("@P_AUDIT_DATE", objVM.AUDIT_DATE);
                        objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SP_VEHICLE_BILL_ENTRY_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetBillEntryBySuppiler(int Trip_Type)
                {
                    try
                    {
                        DataSet ds = null;
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRIP_TYPE", Trip_Type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_RECORD_BY_TRIP_TYPE", objParams);
                        return ds;
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BankController.GetBank_ForReport-> " + ee.ToString());
                    }
                }

                #endregion

                #region Vehicle Daily Attendance Entry
                public DataSet GetAttendanceEntryBySuppiler(VM objVM)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUPPLIER_ID", objVM.SUPPILER_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SP_GET_ATTENDANCE_ENTRY_BY_SUPPILER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetAttendanceEntryBySuppiler-> " + ex.ToString());
                    }
                    return ds;


                }
                public long AddUpdateVehicleDailyAttendanceEntry(VM objVM)
                {
                    long retstatus = 0;
                    object ret;
                    try
                    {


                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_ATTENDANCE_ID", objVM.ATTENDANCE_ID);
                        objParams[1] = new SqlParameter("@P_TRAVELLING_DATE", objVM.TRAVELLING_DATE);
                        objParams[2] = new SqlParameter("@P_SUPPILER_ID", objVM.SUPPILER_ID);
                        objParams[3] = new SqlParameter("@P_VEHICLE_ID", objVM.VEHICLE_ID);
                        objParams[4] = new SqlParameter("@P_ATTENDANCE_MARK", objVM.ATTENDANCE_MARK);
                        objParams[5] = new SqlParameter("@P_DRIVER_ID ", objVM.DRIVER_ID);
                        objParams[6] = new SqlParameter("@P_DRIVER_TA_APPLY ", objVM.DRIVER_TA_APPLY);
                        objParams[7] = new SqlParameter("@P_DRIVER_TA_AMOUNT", objVM.DRIVER_TA_AMOUNT);
                        objParams[8] = new SqlParameter("@P_BETA", objVM.BETA);
                        objParams[9] = new SqlParameter("@P_TOTAL_AMOUNT", objVM.TOTAL_AMOUNT);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SP_VEHICLE_DAILY_ATTENDANCE_ENTRY_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetAttendanceEntryByCollegeVeh(VM objVM)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRAVELLING_DATE", objVM.TRAVELLING_DATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SP_GET_COLLGE_VEH_ATTENDANCE_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetAttendanceEntryBySuppiler-> " + ex.ToString());
                    }
                    return ds;


                }

                public long AddUpdCollegeVehicleDailyAttendanceEntry(VM objVM)
                {
                    long retstatus = 0;
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ATTENDANCE_ID", objVM.ATTENDANCE_ID);
                        objParams[1] = new SqlParameter("@P_TRAVELLING_DATE", objVM.TRAVELLING_DATE);
                        objParams[2] = new SqlParameter("@P_VEH_COLLEGE_DAILY_ATTENDANCE_TBL", objVM.VEHICLE_ATTENDANCE_DT);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SP_COLLEGE_VEHICLE_DAILY_ATTENDANCE_ENTRY_IU ", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdateCollegeVehicleDailyAttendanceEntry->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet GetCollegeVehAttendancedetails(DateTime Travelling_Date)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRAVELLING_DATE", Travelling_Date);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SP_GET_COLLEGE_VEH_ATTENDANCE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetCollegeVehAttendancedetails-> " + ex.ToString());
                    }
                    return ds;


                }
                #endregion

                #region Fitness

                public int AddFitness(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[1] = new SqlParameter("@P_FITNO", objVM.FITNO);
                        objParams[2] = new SqlParameter("@P_ENTRYDT", objVM.FENTRYDT);
                        objParams[3] = new SqlParameter("@P_FDATE", objVM.FDATE);
                        objParams[4] = new SqlParameter("@P_TDATE", objVM.TDATE);
                        objParams[5] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[6] = new SqlParameter("@P_FID", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_FITNESS_INS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddFitness->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdFitness(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FID", objVM.FID);
                        objParams[1] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[2] = new SqlParameter("@P_FITNO", objVM.FITNO);
                        objParams[3] = new SqlParameter("@P_ENTRYDT", objVM.FENTRYDT);
                        objParams[4] = new SqlParameter("@P_FDATE", objVM.FDATE);
                        objParams[5] = new SqlParameter("@P_TDATE", objVM.TDATE);
                        objParams[6] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_FITNESS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdFitness->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetFitnessByFId(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FID", objVM.FID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_FITNESS_GETBY_FID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetFitnessByFId-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetFitnessAll(int vehicle_cat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VEHICLE_CAT", vehicle_cat);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_FITNESS_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetFitnessAll-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteFitnessByFID(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FID", objVM.FID);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_FITNESS_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.DeleteInsuranceByInsIdNo->" + ex.ToString());
                    }
                    return retstatus;
                }

                //========start===Shaikh Juned 12-07-2022
                public int DeleteLogBookByFID(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_LOGBOOKID", objVM.LLOGBOOKID);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_LOGBOOK_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.DeleteInsuranceByInsIdNo->" + ex.ToString());
                    }
                    return retstatus;
                }

                //========end===Shaikh Juned 12-07-2022

                #endregion

                #region Insurance
                public int AddInsurance(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_INSCOMPANY", objVM.INSCOMPANY);
                        objParams[1] = new SqlParameter("@P_POLICYNO", objVM.POLICYNO);
                        objParams[2] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[3] = new SqlParameter("@P_INSDT", objVM.INSDT);
                        objParams[4] = new SqlParameter("@P_INSENDDT", objVM.INSENDDT);
                        objParams[5] = new SqlParameter("@P_PREMIUM", objVM.PREMIUM);
                        objParams[6] = new SqlParameter("@P_AGENTNAME", objVM.AGENTNAME);
                        objParams[7] = new SqlParameter("@P_AGENTNO", objVM.AGENTNO);
                        objParams[8] = new SqlParameter("@P_AGENTPHONE", objVM.AGENTPHONE);
                        objParams[9] = new SqlParameter("@P_NCB", objVM.NCB);
                        objParams[10] = new SqlParameter("@P_CLAIMAMT", objVM.CLAIMAMT);
                        if (objVM.CLAIMDT == DateTime.MinValue)
                        {
                            objParams[11] = new SqlParameter("@P_CLAIMDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[11] = new SqlParameter("@P_CLAIMDT", objVM.CLAIMDT);
                        }
                        objParams[12] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[13] = new SqlParameter("@P_AGENT_SEC_PHONE", objVM.AGENTSECPHONE);
                        objParams[14] = new SqlParameter("@P_INSIDNO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INSURANCE_INS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddInsurance->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdInsurance(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_INSIDNO", objVM.INSIDNO);
                        objParams[1] = new SqlParameter("@P_INSCOMPANY", objVM.INSCOMPANY);
                        objParams[2] = new SqlParameter("@P_POLICYNO", objVM.POLICYNO);
                        objParams[3] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[4] = new SqlParameter("@P_INSDT", objVM.INSDT);
                        objParams[5] = new SqlParameter("@P_INSENDDT", objVM.INSENDDT);
                        objParams[6] = new SqlParameter("@P_PREMIUM", objVM.PREMIUM);
                        objParams[7] = new SqlParameter("@P_AGENTNAME", objVM.AGENTNAME);
                        objParams[8] = new SqlParameter("@P_AGENTNO", objVM.AGENTNO);
                        objParams[9] = new SqlParameter("@P_AGENTPHONE", objVM.AGENTPHONE);
                        objParams[10] = new SqlParameter("@P_NCB", objVM.NCB);
                        objParams[11] = new SqlParameter("@P_CLAIMAMT", objVM.CLAIMAMT);
                        if (objVM.CLAIMDT == DateTime.MinValue)
                        {
                            objParams[12] = new SqlParameter("@P_CLAIMDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[12] = new SqlParameter("@P_CLAIMDT", objVM.CLAIMDT);
                        }
                        objParams[13] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[14] = new SqlParameter("@P_AGENT_SEC_PHONE", objVM.AGENTSECPHONE);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INSURANCE_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdInsurance->" + ex.ToString());

                    }
                    return retstatus;
                }
                public DataSet GetInsuranceByINSIDNO(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INSIDNO", objVM.INSIDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_INSURANCE_GETBY_INSIDNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetInsuranceByINSIDNO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetInsuranceAll(int vehicle_cat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VEHICLE_CAT", vehicle_cat);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_INSURANCE_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetInsuranceAll-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteInsuranceByInsIdNo(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_INSIDNO", objVM.INSIDNO);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INSURANCE_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.DeleteInsuranceByInsIdNo->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Vehicle
                public int AddVehicleMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_NAME", objVM.VNAME);
                        objParams[1] = new SqlParameter("@P_MAKE", objVM.MAKE);
                        objParams[2] = new SqlParameter("@P_MODEL", objVM.MODEL);
                        objParams[3] = new SqlParameter("@P_PURCHASEDT", objVM.PURCHASEDT);
                        objParams[4] = new SqlParameter("@P_VENDORNAME", objVM.VENDORNAME);
                        objParams[5] = new SqlParameter("@P_VENDORADD", objVM.VENDORADD);
                        objParams[6] = new SqlParameter("@P_REGDT", objVM.REGDT);
                        objParams[7] = new SqlParameter("@P_RCBOOKNO", objVM.RCBOOKNO);
                        objParams[8] = new SqlParameter("@P_ENGINENO", objVM.ENGINENO);
                        objParams[9] = new SqlParameter("@P_CHASISNO", objVM.CHASISNO);
                        objParams[10] = new SqlParameter("@P_REGNO", objVM.REGNO);
                        objParams[11] = new SqlParameter("@P_YROFMAKE", objVM.YROFMAKE);
                        objParams[12] = new SqlParameter("@P_COLOR", objVM.COKOR);
                        objParams[13] = new SqlParameter("@P_STATPERMIT", objVM.STATPERMIT);
                        objParams[14] = new SqlParameter("@P_ALLINDPERMIT", objVM.ALLINDIAPERMIT);
                        objParams[15] = new SqlParameter("@P_KM_RATE", objVM.KM_RATE);
                        objParams[16] = new SqlParameter("@P_DRIVE_TA", objVM.DRIVE_TA);
                        objParams[17] = new SqlParameter("@P_RCVALIDITY", objVM.RCVALIDITY); //mrunal

                        if (objVM.RCVALIDITYDT == DateTime.MinValue)
                        {
                            objParams[18] = new SqlParameter("@P_RCVALIDITYDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[18] = new SqlParameter("@P_RCVALIDITYDT", objVM.RCVALIDITYDT);
                        }

                        if (objVM.PUCDT == DateTime.MinValue)
                        {
                            objParams[19] = new SqlParameter("@P_PUCDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[19] = new SqlParameter("@P_PUCDT", objVM.PUCDT);
                        }
                        objParams[20] = new SqlParameter("@P_WAITCHARGE", objVM.WAITCHARGE);
                        objParams[21] = new SqlParameter("@P_LOGNO", objVM.LOGNO);
                        objParams[22] = new SqlParameter("@P_VEHICLE_TYPE", objVM.VTID);
                        objParams[23] = new SqlParameter("@P_VTAC", objVM.VTAC);
                        objParams[24] = new SqlParameter("@P_VEHICLE_NUMBER", objVM.VEHICLE_NUMBER);
                        objParams[25] = new SqlParameter("@P_STATUS", objVM.Status);
                        objParams[26] = new SqlParameter("@P_VIDNO", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_MAS_INS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddVehicleMaster->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdVehicleMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[1] = new SqlParameter("@P_NAME", objVM.VNAME);
                        objParams[2] = new SqlParameter("@P_MAKE", objVM.MAKE);
                        objParams[3] = new SqlParameter("@P_MODEL", objVM.MODEL);
                        objParams[4] = new SqlParameter("@P_PURCHASEDT", objVM.PURCHASEDT);
                        objParams[5] = new SqlParameter("@P_VENDORNAME", objVM.VENDORNAME);
                        objParams[6] = new SqlParameter("@P_VENDORADD", objVM.VENDORADD);
                        objParams[7] = new SqlParameter("@P_REGDT", objVM.REGDT);
                        objParams[8] = new SqlParameter("@P_RCBOOKNO", objVM.RCBOOKNO);
                        objParams[9] = new SqlParameter("@P_ENGINENO", objVM.ENGINENO);
                        objParams[10] = new SqlParameter("@P_CHASISNO", objVM.CHASISNO);
                        objParams[11] = new SqlParameter("@P_REGNO", objVM.REGNO);
                        objParams[12] = new SqlParameter("@P_YROFMAKE", objVM.YROFMAKE);
                        objParams[13] = new SqlParameter("@P_COLOR", objVM.COKOR);
                        objParams[14] = new SqlParameter("@P_STATPERMIT", objVM.STATPERMIT);
                        objParams[15] = new SqlParameter("@P_ALLINDPERMIT", objVM.ALLINDIAPERMIT);
                        objParams[16] = new SqlParameter("@P_KM_RATE", objVM.KM_RATE);
                        objParams[17] = new SqlParameter("@P_DRIVE_TA", objVM.DRIVE_TA);
                        objParams[18] = new SqlParameter("@P_RCVALIDITY", objVM.RCVALIDITY); //mrunal

                        if (objVM.RCVALIDITYDT == DateTime.MinValue)
                        {
                            objParams[19] = new SqlParameter("@P_RCVALIDITYDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[19] = new SqlParameter("@P_RCVALIDITYDT", objVM.RCVALIDITYDT);
                        }

                        if (objVM.PUCDT == DateTime.MinValue)
                        {
                            objParams[20] = new SqlParameter("@P_PUCDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[20] = new SqlParameter("@P_PUCDT", objVM.PUCDT);
                        }
                        objParams[21] = new SqlParameter("@P_WAITCHARGE", objVM.WAITCHARGE);
                        objParams[22] = new SqlParameter("@P_LOGNO", objVM.LOGNO);
                        objParams[23] = new SqlParameter("@P_VEHICLE_TYPE", objVM.VTID);
                        objParams[24] = new SqlParameter("@P_VTAC", objVM.VTAC);
                        objParams[25] = new SqlParameter("@P_VEHICLE_NUMBER", objVM.VEHICLE_NUMBER);
                        objParams[26] = new SqlParameter("@P_STATUS", objVM.Status);
                        objParams[27] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_MAS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdVehicleMaster->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetVehicleMasterByINSIDNO(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_MAS_GET_BY_VIDNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetVehicleMasterByINSIDNO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetVehicleMasterAll()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_MAS_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetVehicleMasterAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region RoadTax
                public int AddRoadTax(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[1] = new SqlParameter("@P_YR", objVM.RYR);
                        objParams[2] = new SqlParameter("@P_FDATE", objVM.RFDATE);
                        objParams[3] = new SqlParameter("@P_TDATE", objVM.RTDATE);
                        objParams[4] = new SqlParameter("@P_AMT", objVM.RAMT);
                        objParams[5] = new SqlParameter("@P_RECNO", objVM.RECNO);
                        objParams[6] = new SqlParameter("@P_PAIDDT", objVM.RPAIDDT);
                        objParams[7] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[8] = new SqlParameter("@P_RTAXID", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ROADTAX_INS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddRoadTax->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdRoadTax(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_RTAXID", objVM.RTAXID);
                        objParams[1] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[2] = new SqlParameter("@P_YR", objVM.RYR);
                        objParams[3] = new SqlParameter("@P_FDATE", objVM.RFDATE);
                        objParams[4] = new SqlParameter("@P_TDATE", objVM.RTDATE);
                        objParams[5] = new SqlParameter("@P_AMT", objVM.RAMT);
                        objParams[6] = new SqlParameter("@P_RECNO", objVM.RECNO);
                        objParams[7] = new SqlParameter("@P_PAIDDT", objVM.RPAIDDT);
                        objParams[8] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ROADTAX_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdRoadTax->" + ex.ToString());

                    }
                    return retstatus;
                }
                public DataSet GetRoadTaxByRTaxID(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RTAXID", objVM.RTAXID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_ROADTAX_GETBY_RTAXID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRoadTaxByRTaxID-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetRoadTaxAll(int vehicle_cat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //SqlParameter[] objParams = new SqlParameter[0];
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VEHICLE_CAT", vehicle_cat);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_ROADTAX_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRoadTaxAll-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteRoadTaxByRTaxID(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_RTAXID", objVM.RTAXID);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ROADTAX_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.DeleteRoadTaxByRTaxID->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Servicing
                public int AddServiceMaster(VM objVM, string Amt, string Qty)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[1] = new SqlParameter("@P_WSNO", objVM.WSNO);
                        objParams[2] = new SqlParameter("@P_BILLNO", objVM.SBILLNO);
                        objParams[3] = new SqlParameter("@P_BILLAMT", objVM.SBILLAMT);
                        objParams[4] = new SqlParameter("@P_PAIDBY", objVM.SPAIDBY);
                        
                        if (objVM.SPAIDDT == DateTime.MinValue)
                        {
                            objParams[5] = new SqlParameter("@P_PAIDDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[5] = new SqlParameter("@P_PAIDDT", objVM.SPAIDDT);
                        }
                        objParams[6] = new SqlParameter("@P_CHQNO", objVM.SCHQNO);
                        objParams[7] = new SqlParameter("@P_CHQBANK", objVM.SCHQBANK);
                        if (objVM.SCHQDT == DateTime.MinValue)
                        {
                            objParams[8] = new SqlParameter("@P_CHQDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[8] = new SqlParameter("@P_CHQDT", objVM.SCHQDT);
                        }
                        objParams[9] = new SqlParameter("@P_WSINDT", objVM.WSINDT);
                        objParams[10] = new SqlParameter("@P_WSINTIME", objVM.WSINTIME);
                        objParams[11] = new SqlParameter("@P_WSOUTDT", objVM.WSOUTDT);
                        objParams[12] = new SqlParameter("@P_WSOUTTIME", objVM.WSOUTTIME);
                        objParams[13] = new SqlParameter("@P_REMARK", objVM.REMARK);
                        

                        if (objVM.BILLDT == DateTime.MinValue)
                        {
                            objParams[14] = new SqlParameter("@P_BILLDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[14] = new SqlParameter("@P_BILLDT", objVM.BILLDT);
                        }

                        objParams[15] = new SqlParameter("@P_NEXTDT", objVM.NEXTDT);
                        objParams[16] = new SqlParameter("@P_ORDNO", objVM.ORDNO);
                        objParams[17] = new SqlParameter("@P_ITEM", objVM.ITEM);
                        objParams[18] = new SqlParameter("@P_QTY", Qty);
                        objParams[19] = new SqlParameter("@P_AMT", Amt);
                        objParams[20] = new SqlParameter("@P_TRANSCATIONNO", objVM.TRANSCATIONNO);
                        if (objVM.TRANSFERDT == DateTime.MinValue)
                        {
                            objParams[21] = new SqlParameter("@P_TRANSFERDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[21] = new SqlParameter("@P_TRANSFERDT", objVM.TRANSFERDT);
                        }
                        objParams[22] = new SqlParameter("@P_SIDNO", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SERVICEMAS_INS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddVehicleMaster->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdServiceMaster(VM objVM, string Amt, string Qty)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[24];
                        objParams[0] = new SqlParameter("@P_SIDNO", objVM.SIDNO);
                        objParams[1] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[2] = new SqlParameter("@P_WSNO", objVM.WSNO);
                        objParams[3] = new SqlParameter("@P_BILLNO", objVM.SBILLNO);
                        objParams[4] = new SqlParameter("@P_BILLAMT", objVM.SBILLAMT);
                        objParams[5] = new SqlParameter("@P_PAIDBY", objVM.SPAIDBY);
                        if (objVM.SPAIDDT == DateTime.MinValue)
                        {
                            objParams[6] = new SqlParameter("@P_PAIDDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[6] = new SqlParameter("@P_PAIDDT", objVM.SPAIDDT);
                        }

                        objParams[7] = new SqlParameter("@P_CHQNO", objVM.SCHQNO);
                        objParams[8] = new SqlParameter("@P_CHQBANK", objVM.SCHQBANK);
                        
                        if (objVM.SCHQDT == DateTime.MinValue)
                        {
                            objParams[9] = new SqlParameter("@P_CHQDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[9] = new SqlParameter("@P_CHQDT", objVM.SCHQDT);
                        }
                        objParams[10] = new SqlParameter("@P_WSINDT", objVM.WSINDT);
                        objParams[11] = new SqlParameter("@P_WSINTIME", objVM.WSINTIME);
                        objParams[12] = new SqlParameter("@P_WSOUTDT", objVM.WSOUTDT);
                        objParams[13] = new SqlParameter("@P_WSOUTTIME", objVM.WSOUTTIME);
                        objParams[14] = new SqlParameter("@P_REMARK", objVM.REMARK);                       

                        if (objVM.BILLDT == DateTime.MinValue)
                        {
                            objParams[15] = new SqlParameter("@P_BILLDT", DBNull.Value);
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_BILLDT", objVM.BILLDT);
                        }

                        objParams[16] = new SqlParameter("@P_NEXTDT", objVM.NEXTDT);
                        objParams[17] = new SqlParameter("@P_ORDNO", objVM.ORDNO);
                        objParams[18] = new SqlParameter("@P_ITEM", objVM.ITEM);
                        objParams[19] = new SqlParameter("@P_QTY", Qty);
                        objParams[20] = new SqlParameter("@P_AMT", Amt);
                        objParams[21] = new SqlParameter("@P_TRANSCATIONNO", objVM.TRANSCATIONNO);
                        if (objVM.TRANSFERDT == DateTime.MinValue)
                            objParams[22] = new SqlParameter("@P_TRANSFERDT", DBNull.Value);
                        else
                            objParams[22] = new SqlParameter("@P_TRANSFERDT", objVM.TRANSFERDT);
                        objParams[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[23].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SERVICEMAS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdVehicleMaster->" + ex.ToString());

                    }
                    return retstatus;
                }
                public DataSet GetServiceMasterByIDNO(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SIDNO", objVM.SIDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SERVICEMAS_GETBY_SIDNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetVehicleMasterByINSIDNO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetServiceMasterItem(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SIDNO", objVM.SIDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SERVICEMAS_GETITEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetServiceMasterItem-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetServiceMasterAll()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SERVICEMAS_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetVehicleMasterAll-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteServiceMasterBySIDNO(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SIDNO", objVM.SIDNO);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SERVICEMAS_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.DeleteRoadTaxByRTaxID->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region WorkShop
                public int AddUpdateWorkshopMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_WSNO", objVM.WSNO);
                        objParams[1] = new SqlParameter("@P_WORKSHOP", objVM.WORKSHOP);
                        objParams[2] = new SqlParameter("@P_ADD1", objVM.WADD1);
                        objParams[3] = new SqlParameter("@P_ADD2", objVM.WADD2);
                        objParams[4] = new SqlParameter("@P_PHONE", objVM.WPHONE);
                        objParams[5] = new SqlParameter("@P_PERSONNAME", objVM.WPERSONNAME);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objVM.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_WORKSHOP_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddWorkshop->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int UpdWorkshop(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_WSNO", objVM.WSNO);
                        objParams[1] = new SqlParameter("@P_WORKSHOP", objVM.WORKSHOP);
                        objParams[2] = new SqlParameter("@P_ADD1", objVM.WADD1);
                        objParams[3] = new SqlParameter("@P_ADD2", objVM.WADD2);
                        objParams[4] = new SqlParameter("@P_PHONE", objVM.WPHONE);
                        objParams[5] = new SqlParameter("@P_PERSONNAME", objVM.WPERSONNAME);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SERVICEMAS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdWorkshop->" + ex.ToString());

                    }
                    return retstatus;
                }
                public DataSet GetWorkshopByWSNO(VM objVM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_WSNO", objVM.INSIDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_WORKSHOP_GETBY_WSNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetWorkshopByWSNO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetWorkshopAll()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_WORKSHOP_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetWorkshopAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Trip Type
                public int TripTypeInsertUpdate(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_TTID", objVM.TTID);
                        objParams[1] = new SqlParameter("@P_TRIPTYPENAME", objVM.TRIPTYPENAME);
                        objParams[2] = new SqlParameter("@P_CHARGEABLE", objVM.CHARGEABLE);
                        objParams[3] = new SqlParameter("@P_ACTIVE", objVM.ACTIVE);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objVM.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_TRIPTYPE_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.TripTypeInsertUpdate->" + ex.ToString());
                    }
                    return retstatus;
                }

                //public int UpdDriverMas(VM objVM)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_DNO", objVM.DNO);
                //        objParams[1] = new SqlParameter("@P_DNAME", objVM.DNAME);
                //        objParams[2] = new SqlParameter("@P_PHONE", objVM.DPHONE);
                //        objParams[3] = new SqlParameter("@P_DADD1", objVM.DADD1);
                //        objParams[4] = new SqlParameter("@P_DADD2", objVM.DADD2);
                //        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[5].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_DRIVERMAS_UPD", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retstatus = Convert.ToInt32(CustomStatus.Error);
                //        else
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdDriverMas->" + ex.ToString());
                //    }
                //    return retstatus;
                //}



                public DataSet GetTripTypeById(int TTID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TTID", TTID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_TRIPTYPE_GETBY_TTID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTripTypeById-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetTripTypeAll()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_TRIPTYPE_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTripTypeAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Log Book
                public int LogBookInsertUpdate(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[20];


                        objParams[0] = new SqlParameter("@P_LLOGBOOKID", objVM.LLOGBOOKID);
                        objParams[1] = new SqlParameter("@P_LVIDNO", objVM.LVIDNO);
                        objParams[2] = new SqlParameter("@P_LTTID", objVM.LTTID);
                        objParams[3] = new SqlParameter("@P_LTOURDATE", objVM.LTOURDATE);
                        objParams[4] = new SqlParameter("@P_LDTIME", objVM.LDTIMEL);
                        objParams[5] = new SqlParameter("@P_LATIME", objVM.LATIME);
                        objParams[6] = new SqlParameter("@P_LFROMLOCATION", objVM.LFROMLOCATION);
                        objParams[7] = new SqlParameter("@P_LTOLOCATION", objVM.LTOLOCATION);
                        objParams[8] = new SqlParameter("@P_LREMARK", objVM.LREMARK);
                        objParams[9] = new SqlParameter("@P_LSTARTMETERREADING", objVM.LSTARTMETERREADING);
                        objParams[10] = new SqlParameter("@P_LENDMETERREADING", objVM.LENDMETERREADING);
                        objParams[11] = new SqlParameter("@P_LWAITINGCHARGES", objVM.LWAITINGCHARGES);
                        objParams[12] = new SqlParameter("@P_LHIRERATEPERKM", objVM.LHIRERATEPERKM);
                        objParams[13] = new SqlParameter("@P_LDRIVERTA", objVM.LDRIVERTA);
                        objParams[14] = new SqlParameter("@P_LTOTALAMT", objVM.LTOTALAMT);
                        objParams[15] = new SqlParameter("@P_LTOURDETAILS", objVM.LTOURDETAILS);
                        objParams[16] = new SqlParameter("@P_LDNO", objVM.LDNO);
                        objParams[17] = new SqlParameter("@P_PASSENGERNAME", objVM.PASSENGERNAME);
                        objParams[18] = new SqlParameter("@P_TOTALKM", objVM.LTOTALKM);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_LOGBOOK_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.TripTypeInsertUpdate->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetLogBookByLogBookId(int LogBookId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LOGBOOKID", LogBookId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_LOGBOOK_GETBY_LOGBOOKID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTripTypeById-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetLogBookAll()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_LOGBOOK_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTripTypeAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Fuel Entry

                public int FuelEntryInsertUpdate(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[30];

                        objParams[0] = new SqlParameter("@P_FEID", objVM.FFEID);
                        objParams[1] = new SqlParameter("@P_VIDNO", objVM.FVIDNO);
                        objParams[2] = new SqlParameter("@P_DNO", objVM.FDNO);
                        if (objVM.FFDATE == DateTime.MinValue)
                        {
                            objParams[3] = new SqlParameter("@P_FUELDATE  ", DBNull.Value);
                        }
                        else
                        {
                            objParams[3] = new SqlParameter("@P_FUELDATE  ", objVM.FFDATE);
                        }
                        //objParams[3] = new SqlParameter("@P_FUELDATE", objVM.FFDATE);
                        // objParams[4] = new SqlParameter("@P_BILLNO", objVM.FBILLNO);
                        // objParams[5] = new SqlParameter("@P_BILLDATE", objVM.FBILLDATE);
                        //  objParams[6] = new SqlParameter("@P_FUELTYPE", objVM.FFUELTYPE);
                        objParams[4] = new SqlParameter("@P_QTY", objVM.FQTY);
                        //objParams[5] = new SqlParameter("@P_AMOUNT", objVM.FAMOUNT);
                        objParams[5] = new SqlParameter("@P_REMARK", objVM.FREMARK);
                        objParams[6] = new SqlParameter("@P_LOGNO", objVM.FLOGNO);
                        objParams[7] = new SqlParameter("@P_ITEM_ID", objVM.ITEM_ID);
                        //  objParams[11] = new SqlParameter("@P_CMREADING", objVM.FCMREADING);
                        objParams[8] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        //  objParams[13] = new SqlParameter("@P_LMREADING", objVM.FLMREADING);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objVM.FCOLLEGECODE);
                        objParams[10] = new SqlParameter("@P_METER_READING", objVM.METER_READING);

                        objParams[11] = new SqlParameter("@P_END_METER_READING", objVM.END_METER_READING);
                        objParams[12] = new SqlParameter("@P_NO_OF_KMS", objVM.NO_OF_KMS);
                        objParams[13] = new SqlParameter("@P_MILEGE", objVM.MILEGE);
                        objParams[14] = new SqlParameter("@P_MILEGE_AMOUNT", objVM.MILEGE_AMOUNT);
                        objParams[15] = new SqlParameter("@P_RATE", objVM.RATE);
                        objParams[16] = new SqlParameter("@P_VEHICLETYPE", objVM.VEHICLETYPES);

                        objParams[17] = new SqlParameter("@P_ISSUE_TYPE", objVM.ISSUE_TYPE); //----29-03-2023
                        objParams[18] = new SqlParameter("@P_AVELABLE_QTY", objVM.AVELABLE_QTY); //----29-03-2023

                        if (objVM.COUPON_NO == null)
                        {
                            objParams[19] = new SqlParameter("@P_COUPON_NO", string.Empty);
                        }
                        else
                        {
                            objParams[19] = new SqlParameter("@P_COUPON_NO", objVM.COUPON_NO);
                        }

                      //  objParams[19] = new SqlParameter("@P_COUPON_NO", objVM.COUPON_NO); //----29-03-2023
                        objParams[20] = new SqlParameter("@P_TOTAL_AMOUNT1", objVM.TOTAL_AMOUNT1); //----29-03-2023

                        objParams[21] = new SqlParameter("@P_DEPARTMENT", objVM.DEPARTMENT); //----29-03-2023
                        if (objVM.DATE_OF_WITHDRAWAL == DateTime.MinValue)
                        {
                            objParams[22] = new SqlParameter("@P_DATE_OF_WITHDRAWAL  ", DBNull.Value);
                        }
                        else
                        {
                            objParams[22] = new SqlParameter("@P_DATE_OF_WITHDRAWAL  ", objVM.DATE_OF_WITHDRAWAL);
                        }
                       // objParams[22] = new SqlParameter("@P_DATE_OF_WITHDRAWAL", objVM.DATE_OF_WITHDRAWAL); //----29-03-2023
                        objParams[23] = new SqlParameter("@P_DIESEL_REQUESTER", objVM.DIESEL_REQUESTER); //----29-03-2023
                        objParams[24] = new SqlParameter("@P_APPROVER", objVM.APPROVER); //----29-03-2023
                        objParams[25] = new SqlParameter("@P_PURPOSE_OF_WITHDRAWAL", objVM.PURPOSE_OF_WITHDRAWAL); //----29-03-2023
                        objParams[26] = new SqlParameter("@P_UPLOAD_REQUEST_LETTER", objVM.UploadRequestLetter); //----01-04-2023
                        objParams[27] = new SqlParameter("@P_IsBlob", objVM.IsBlob); //----01-04-2023
                       // objParams[17] = new SqlParameter("@P_ORIGINAL_RATE", objVM.ORIGINAL_RATE);  //12-07-2022
                        objParams[28] = new SqlParameter("@P_USERNO", objVM.USERNO);     //12-07-2022
                        objParams[29] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[29].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_FUELENTRY_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 1)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.FuelEntryInsertUpdate->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet GetFuelEntryById(int FEID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FEID", FEID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_FUELENTRY_GETBY_FEID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetFuelEntryById-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFuelEntryAll(int cat, int issue)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VEHICLE_CAT", cat);
                        objParams[1] = new SqlParameter("@P_ISSUE_TYPE", issue);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_FUELENTRY_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetFuelEntryAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                #region Route Entry
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 18-AUG-2015
                /// DESCRIPTION  : USED TO INSERT AND UPDATE STOP NAME.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateStopMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_STOPNO", objVM.STOPNO);
                        objParams[1] = new SqlParameter("@P_STOPNAME", objVM.STOPNAME);
                        objParams[2] = new SqlParameter("@P_SEQNO", objVM.SEQNO);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[5] = new SqlParameter("@P_STUDENT_FEE", objVM.STUDENT_FEE);
                        objParams[6] = new SqlParameter("@P_EMPLOYEE_FEE", objVM.EMPLOYEE_FEE);
                        objParams[7] = new SqlParameter("@P_VCID", objVM.VCID);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_STOPNAME_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateStopMaster->" + ex.ToString());
                    }
                    return retstatus;
                }

                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 18-AUG-2015
                /// DESCRIPTION  : USED TO INSERT AND UPDATE VEHICLE ROUTE ALLOTMENT.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateVehicleRouteAllotment(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_RANO", objVM.RANO);
                        objParams[1] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[2] = new SqlParameter("@P_ROUTENO", objVM.ROUTENO);

                        if (objVM.FDATE == DateTime.MinValue)
                        {
                            objParams[3] = new SqlParameter("@P_FDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[3] = new SqlParameter("@P_FDATE", objVM.FDATE);
                        }

                        if (objVM.TDATE == DateTime.MinValue)
                        {
                            objParams[4] = new SqlParameter("@P_TDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[4] = new SqlParameter("@P_TDATE", objVM.TDATE);
                        }

                        objParams[5] = new SqlParameter("@P_DTIME", objVM.LDTIME);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[8] = new SqlParameter("@P_DRIVER_ID", objVM.DNO);
                        objParams[9] = new SqlParameter("@P_VEHICLE_CAT", objVM.VEHICLECAT);
                        objParams[10] = new SqlParameter("@P_ROUTENAME", objVM.ROUTENAME);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_VEHICLE_ROUTE_ALLOTMENT_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateVehicleRouteAllotment->" + ex.ToString());
                    }
                    return retstatus;
                }

                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 22-AUG-2015
                /// DESCRIPTION  : USED TO DELETE VEHICLE ROUTE ALLOTMENT.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int DeleteVehicleRouteAllotment(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_RAID", objVM.RANO);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_VEHICLE_ROUTE_ALLOTMENT_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.DeleteVehicleRouteAllotment->" + ex.ToString());
                    }
                    return retstatus;
                }


                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 18-AUG-2015
                /// DESCRIPTION  : USED TO INSERT AND UPDATE STOP NAME.  StartTime); //
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateRouteMaster(VM objVM) //, string StartTime)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_ROUTENO", objVM.ROUTENO);
                        objParams[1] = new SqlParameter("@P_ROUTENAME", objVM.ROUTENAME);
                        objParams[2] = new SqlParameter("@P_ROUTEPATH", objVM.ROUTEPATH);
                        objParams[3] = new SqlParameter("@P_KM", objVM.KM);
                        objParams[4] = new SqlParameter("@P_SEQNO", objVM.SEQNO);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[7] = new SqlParameter("@P_ROUTECODE", objVM.ROUTECODE);
                        objParams[8] = new SqlParameter("@P_STARTING_TIME", objVM.STARTING_TIME);
                        objParams[9] = new SqlParameter("@P_ROUTENUMBER", objVM.ROUTENUMBER);
                        objParams[10] = new SqlParameter("@P_VEHICLETYPE", objVM.VEHICLETYPE);
                        objParams[11] = new SqlParameter("@P_ROUTEFEES", objVM.ROUTEFEES);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ROUTENAME_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateRouteMaster->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetRouteDataByID(int ROUTENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROUTEID", ROUTENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_ROUTENAME_ROUTEPATH", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVehRouteAllotDataByID(int RANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RAID", RANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_VEHICLE_ROUTE_ALLOTMENT", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 24-AUG-2015
                /// DESCRIPTION  : USED TO INSERT AND UPDATE STOP NAME.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddBoardingPassDetails(VM objVM, string IdNo)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_BORID", objVM.BORID);
                        objParams[1] = new SqlParameter("@P_EXPIRY_DATE", objVM.EXPIRY_DATE);
                        objParams[2] = new SqlParameter("@P_ALLOT_DATE", objVM.ALLOT_DATE);
                        objParams[3] = new SqlParameter("@P_USERNO", objVM.USERNO);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[6] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[7] = new SqlParameter("@P_STATUS", objVM.STATUS);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_BOARDING_PASS_DETAILS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddBoardingPassDetails->" + ex.ToString());
                    }
                    return retstatus;
                }


                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 17-JUN-2019
                /// DESCRIPTION  : USED TO INSERT AND UPDATE USER ROUTE ALLOTMENT.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateUserRouteAllotment(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_URID", objVM.URID);
                        objParams[1] = new SqlParameter("@P_IDNO", objVM.IDNO);
                        objParams[2] = new SqlParameter("@P_ROUTEID", objVM.ROUTEID);
                        objParams[3] = new SqlParameter("@P_USER_TYPE", objVM.USER_TYPE);
                        objParams[4] = new SqlParameter("@P_STOPNO", objVM.STOPNO);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_USER_ROUTE_ALLOTMENT_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateUserRouteAllotment->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllottedVehicles()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_ALLOTTED_VEHICLES", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetBoardingPointsByRouteID(int RouteId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROUTEID", RouteId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_BOARDING_POINTS_BY_RAID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetBoardingPointsByRouteID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdateBulkUserAllotment(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_URID", objVM.URID);
                        objParams[1] = new SqlParameter("@P_ALLOTMENT_TABLE", objVM.ALLOTMENT_TABLE);
                        objParams[2] = new SqlParameter("@P_USER_TYPE", objVM.USER_TYPE);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[5] = new SqlParameter("@P_YEAR", objVM.YEAR);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_BULK_USER_ALLOTMENT_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateBulkUserAllotment->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet FillEmployeeName(string prefixText, string contextKey)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCHTEXT", prefixText);
                        objParams[1] = new SqlParameter("@P_USER_TYPE", HttpContext.Current.Session["UserType"].ToString());
                        objParams[2] = new SqlParameter("@P_SEARCH_BY", contextKey);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_SEARCH_USER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.FillEmployeeName()-> " + ex.ToString());
                    }

                    return ds;
                }


                public int AddCancelAllotment(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_URID", objVM.URID);
                        objParams[1] = new SqlParameter("@P_IDNO", objVM.IDNO);
                        objParams[2] = new SqlParameter("@P_USER_TYPE", objVM.USER_TYPE);
                        objParams[3] = new SqlParameter("@P_CANCEL_REMARK", objVM.CANCEL_REMARK);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_UPDATE_CANCEL_STATUS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddCancelAllotment->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet GetRouteAllotmentData(string USER_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USER_TYPE", USER_TYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_ROUTE_ALLOTMENT_DATA", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region ITEM MASTER
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 02-OCT-2015
                /// DESCRIPTION  : USED TO INSERT AND UPDATE ITEM NAME.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateItemMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ITEM_ID", objVM.ITEM_ID);
                        objParams[1] = new SqlParameter("@P_ITEM_NAME", objVM.ITEM_NAME);
                        objParams[2] = new SqlParameter("@P_UNIT", objVM.UNIT);
                        objParams[3] = new SqlParameter("@P_RATE", objVM.RATE);
                        objParams[4] = new SqlParameter("@P_ITEM_TYPE", objVM.ITEM_TYPE);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ITEMNAME_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateItemMaster->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region ITEM Purchased
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 03-OCT-2015
                /// DESCRIPTION  : USED TO INSERT AND UPDATE ITEMS PURCHASED.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateItemsPurchase(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_PURCHASE_ID", objVM.PURCHASE_ID);
                        objParams[1] = new SqlParameter("@P_ITEM_TYPE", objVM.ITEM_TYPE);
                        objParams[2] = new SqlParameter("@P_ITEM_ID", objVM.ITEM_ID);
                        objParams[3] = new SqlParameter("@P_PURCHASE_DATE", objVM.PURCHASE_DATE);
                        objParams[4] = new SqlParameter("@P_QUANTITY", objVM.QUANTITY);
                        objParams[5] = new SqlParameter("@P_TOTAL_AMT", objVM.TOTAL_AMT);
                        objParams[6] = new SqlParameter("@P_RATE", objVM.RATE);
                        objParams[7] = new SqlParameter("@FUEL_SUPPILER_ID", objVM.FUEL_SUPPILER_ID);
                        objParams[8] = new SqlParameter("@CRN", objVM.CRN);
                        objParams[9] = new SqlParameter("@PURCHASE_FOR", objVM.PURCHASE_FOR);
                        objParams[10] = new SqlParameter("@PURCHASE_COUPON_NUMBER", objVM.PURCHASE_COUPON_NUMBER);
                        objParams[11] = new SqlParameter("@P_USERNO", objVM.USERNO);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ITEM_PURCHASE_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateItemMaster->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion


                #region Vehicle Type Master
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 05-FEB-2016
                /// DESCRIPTION  : USED TO INSERT AND UPDATE VEHICLE TYPE NAME.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateVTMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_VTID", objVM.VTID);
                        objParams[1] = new SqlParameter("@P_VTNAME", objVM.VTNAME);
                        objParams[2] = new SqlParameter("@P_ROUTE_TYPE_NO", objVM.ROUTE_TYPE_NO);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_MACADDRESS", objVM.MACADDRESS);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_VEHICLE_TYPE_NAME_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateVTMaster->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion


                #region Vehicle Schedule
                public long AddUpdateSchedule(VM objVM)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SCHEDULEID", objVM.SCHEDULEID);
                        objParams[1] = new SqlParameter("@P_SCHEDULE_DATE", objVM.SCHEDULE_DATE);
                        objParams[2] = new SqlParameter("@P_MORNING_TRIP", objVM.MORNING_TRIP);
                        objParams[3] = new SqlParameter("@P_SPECIAL_TRIP", objVM.SPECIAL_TRIP);
                        objParams[4] = new SqlParameter("@P_EVENING_TRIP", objVM.EVENING_TRIP);
                        objParams[5] = new SqlParameter("@P_LATE_TRIP", objVM.LATE_TRIP);
                        objParams[6] = new SqlParameter("@P_USERNO", objVM.USERNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_SCHEDULE_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateSchedule->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion


                #region TransportList

                public DataSet Transport_Passenger_List()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TRANSPORT_LIST_OF_PASSENGERS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.Transport_Passenger_List-> " + ex.ToString());
                    }
                    return ds;
                }




                #endregion




                #region ArrivalTimeEntry

                public DataSet GetArrivalTimeEntryList(DateTime date)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARRIVAL_DATE", date);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_SP_ARRIVAL_TIME_ENTRY", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet GetTablesforArrival()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ARRIVAL_TABLES", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;

                }



                public int InsUpdateArrivalTimeEntry(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[7];
                        ObjPram[0] = new SqlParameter("@P_ARRIVAL_DATE", objEnt.ArrivalDate);
                        ObjPram[1] = new SqlParameter("@P_ARRIVAL_TIME_ENTRY", objEnt.ArrivalTimeEntryDataTable);
                        ObjPram[2] = new SqlParameter("@P_AC_BUS_38_SEAT", objEnt.AC_BUS_38_SEAT);
                        ObjPram[3] = new SqlParameter("@P_AC_BUS_55_SEAT", objEnt.AC_BUS_55_SEAT);
                        ObjPram[4] = new SqlParameter("@P_DEDICATED_BUSES", objEnt.DEDICATED_BUSES);
                        ObjPram[5] = new SqlParameter("@P_SVCE_BUSES", objEnt.SVCE_BUSES);
                        ObjPram[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INS_UPD_ARRIVAL_TIME_ENTRY", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }
                #endregion

                #region ComplaintRegister   Added  by Vijay Andoju on 21-02-2020 for Vehicle Complaint Register

                public int InsUpdComplaintRegister(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[9];
                        ObjPram[0] = new SqlParameter("@P_SNO", objEnt.Sno);
                        ObjPram[1] = new SqlParameter("@P_ROUTE_NO", objEnt.Route_no);
                        ObjPram[2] = new SqlParameter("@P_NATURE_OF_COMPLAINT", objEnt.NatureofComplaint);
                        ObjPram[3] = new SqlParameter("@P_COMPLAINT_REGISTERED_BY", objEnt.ComplaintRegister);
                        ObjPram[4] = new SqlParameter("@P_COMPLAINT_DATE", objEnt.ComplaintDate);
                        ObjPram[5] = new SqlParameter("@P_COMPLAINT_RECEIVED_THROUGH", objEnt.ComplaintRecivedThrough);
                        ObjPram[6] = new SqlParameter("@P_ACTION_TAKEN", objEnt.ActionTaken);
                        if (!objEnt.ActionTakenDate.Equals(DateTime.MinValue))
                            ObjPram[7] = new SqlParameter("@P_ACTION_TAKEN_DATE", objEnt.ActionTakenDate);
                        else
                            ObjPram[7] = new SqlParameter("@P_ACTION_TAKEN_DATE", DBNull.Value);
                        //  ObjPram[7] = new SqlParameter("@P_ACTION_TAKEN_DATE", objEnt.ActionTakenDate);
                        ObjPram[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INS_UPD_COMPLAINT_REGISTER", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion
                #region Dataset GETTABLES Added  by Vijay Andoju on 21-02-2020 for GET TABLES
                public DataSet GetTables()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_TABLES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllInvoice-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Incident Report   Added  by Vijay Andoju on 22-02-2020 for Incident Report

                public int IncidentReportEntry(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[8];
                        ObjPram[0] = new SqlParameter("@P_SNO", objEnt.Sno);
                        ObjPram[1] = new SqlParameter("@P_ROUTE_NO", objEnt.Route_no);
                        ObjPram[2] = new SqlParameter("@P_NATURE_OF_INCIDENT", objEnt.NATUREOFINCIDENT);
                        ObjPram[3] = new SqlParameter("@P_INCIDENT_PLACE", objEnt.INCIDENTPLACE);
                        ObjPram[4] = new SqlParameter("@P_INCIDENT_DATE", objEnt.ComplaintDate);
                        ObjPram[5] = new SqlParameter("@P_INCIDENT_TIME", objEnt.ActionTakenDate);
                        ObjPram[6] = new SqlParameter("@P_FOLLOW_UP", objEnt.FOLLOWUP);
                        ObjPram[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INS_UPD_INCIDENT_REPORT_TBL", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion


                #region Get StudentTransportStatus added by Vijay Andoju 27-02-2020 for Vehicle

                public DataSet GetStudentTransportStatus(int SEMESTERNO, int BRANCHNO, int DEGREENO, int YEAR, int STATUS)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@SEMESTERNO", SEMESTERNO);
                        objParams[1] = new SqlParameter("@BRANCHNO", BRANCHNO);
                        objParams[2] = new SqlParameter("@DEGREENO", DEGREENO);
                        objParams[3] = new SqlParameter("@YEAR", YEAR);
                        objParams[4] = new SqlParameter("@STATUS", STATUS);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_REPORT_STUDENT_STATUS", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion


                #region  STUDENT Transport Cancellation  Added  by Vijay Andoju on 01-03-2020
                public DataSet GetTablesStudent()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_TABLES_FOR_STUDENT_TRANSPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllInvoice-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertUpdateStudentTransportCancellation(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[8];
                        ObjPram[0] = new SqlParameter("@P_SRNO", objEnt.S_SRNO);
                        ObjPram[1] = new SqlParameter("@P_S_YEAR", objEnt.S_YEAR);
                        ObjPram[2] = new SqlParameter("@P_S_SEMESTER", objEnt.S_SEMESTER);
                        ObjPram[3] = new SqlParameter("@P_S_DEGREENO", objEnt.S_DEGREENO);
                        ObjPram[4] = new SqlParameter("@P_S_IDNO", objEnt.S_IDNO);
                        ObjPram[5] = new SqlParameter("@P_S_BRANCH", objEnt.S_BRANCH);
                        ObjPram[6] = new SqlParameter("@P_S_TRANSPORT", objEnt.S_TRANSPORT);
                        ObjPram[7] = new SqlParameter("@P_S_CRATED_BY", objEnt.S_CREATED_BY);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_UPD_STUDENT_TRANSPORT_CANCELLATION", ObjPram, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public DataSet GetDetailsOfStudentTransportCancellation()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ADMN_VEHICLE_STUDENT_TRANSPORT_CANCELLATION_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllInvoice-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GET_STUDENT_NAMES(int DEG, int BRANCH, int YEAR, int SEM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@DEGREENO", DEG);
                        objParams[1] = new SqlParameter("@BRANCHNO", BRANCH);
                        objParams[2] = new SqlParameter("@SEMESTERNO", SEM);
                        objParams[3] = new SqlParameter("@YEAR", YEAR);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_STUDENTNAMES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllInvoice-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                #region StudentTransportStatus added by andoju on 16-03-2020
                public DataSet GetTablesStudentTransportStatus()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_GET_TABLES_FOR_TRANSPORTCANCEL_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllInvoice-> " + ex.ToString());
                    }
                    return ds;
                }

                public int Ins_Upd_StudentTransportstaus(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[5];
                        ObjPram[0] = new SqlParameter("@P_S_IDNO", objEnt.S_IDNO);
                        ObjPram[1] = new SqlParameter("@P_S_CANCELLED_STATUS", objEnt.S_CANCELLED_STATUS);
                        ObjPram[2] = new SqlParameter("@P_S_CREATED_BY", objEnt.S_CREATED_BY);
                        ObjPram[3] = new SqlParameter("@P_S_STATUS", objEnt.S_STATUS);
                        ObjPram[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_STUDENT_TRANSPORT_STATUS", ObjPram, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }
                #endregion


                #region HOD Cars Details

                // it is used to insert/ update HOD Cars Details.
                public int HODCarsDetailsIU(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[4];
                        ObjPram[0] = new SqlParameter("@P_HODCARS_ID", objEnt.HODCARS_ID);
                        ObjPram[1] = new SqlParameter("@P_HODCARS_TABLE", objEnt.HODCarsTable);
                        ObjPram[2] = new SqlParameter("@P_CREATED_BY", objEnt.CREATED_BY);
                        ObjPram[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_HOD_CARS_DETAILS_IU", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }



                public DataSet GetHODCarsDetails(DateTime date)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARRIVAL_DATE", date);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_HOD_CARS_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;

                }

                public int UpdateHODCarsDetails(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[7];
                        ObjPram[0] = new SqlParameter("@P_HODCARS_ID", objEnt.HODCARS_ID);
                        ObjPram[1] = new SqlParameter("@P_ARRIVAL_DATE", objEnt.ARRIVAL_DATE);
                        ObjPram[2] = new SqlParameter("@P_TRAVELS_ID", objEnt.TRAVELS_ID);
                        ObjPram[3] = new SqlParameter("@P_REGNO", objEnt.REGNO);
                        ObjPram[4] = new SqlParameter("@P_IN_TIME", objEnt.IN_TIME);
                        ObjPram[5] = new SqlParameter("@P_CREATED_BY", objEnt.CREATED_BY);
                        ObjPram[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_HOD_CARS_DETAILS_UPDATE", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion

                #region BUS_TOKEN_ISSUE

                public DataSet GetBusTokenIssueDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_BUS_TOKEN_ISSUE_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;

                }

                public int InsUpdBusTokenIssue(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[8];
                        ObjPram[0] = new SqlParameter("@P_BTNO", objEnt.BTNO);
                        ObjPram[1] = new SqlParameter("@P_BUS_TOKEN_ISSUE_DATE", objEnt.BUS_TOKEN_ISSUE_DATE);
                        ObjPram[2] = new SqlParameter("@P_TOKEN_40", objEnt.TOKEN_40);
                        ObjPram[3] = new SqlParameter("@P_TOKEN_40_AMOUNT", objEnt.TOKEN_40_AMOUNT);
                        ObjPram[4] = new SqlParameter("@P_TOKEN_30", objEnt.TOKEN_30);
                        ObjPram[5] = new SqlParameter("@P_TOKEN_30_AMOUNT", objEnt.TOKEN_30_AMOUNT);
                        ObjPram[6] = new SqlParameter("@P_GRAND_TOTAL", objEnt.GRAND_TOTAL);
                        ObjPram[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_VEH_BUS_TOKEN_ISSUE_UI", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion

                #region FUEL_SUPPLER_CON
                public int InsUpdFuelSupllier(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[7];
                        ObjPram[0] = new SqlParameter("@P_FUEL_SUPPILER_ID", objEnt.FUEL_SUPPILER_ID);
                        ObjPram[1] = new SqlParameter("@P_FUEL_SUPPILER_NAME", objEnt.FUEL_SUPPILER_NAME);
                        ObjPram[2] = new SqlParameter("@P_FUEL_CONTACT_ADDRESS", objEnt.FUEL_CONTACT_ADDRESS);
                        ObjPram[3] = new SqlParameter("@P_FUEL_CONTACT_NUMBER", objEnt.FUEL_CONTACT_NUMBER);
                        ObjPram[4] = new SqlParameter("@P_FUEL_CONTACT_PERSON", objEnt.FUEL_CONTACT_PERSON);
                        //ObjPram[5] = new SqlParameter("@P_FUEL_SUPPLIED_DATE", objEnt.FUEL_SUPPLIED_DATE);
                        ObjPram[5] = new SqlParameter("@P_IS_ACTIVE", objEnt.FUEL_IS_ACTIVE);
                        ObjPram[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEHICLE_FUEL_SUPPLIER_MASTER_UI", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion

                #region Monthly_Fuel_Expense
                public int InsUpdMonthlyFuelExpense(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[6];
                        ObjPram[0] = new SqlParameter("@P_FROM_DATE", objEnt.Fule_FromDate);
                        ObjPram[1] = new SqlParameter("@P_TO_DATE", objEnt.Fule_ToDate);
                        ObjPram[2] = new SqlParameter("@P_FUEL_SUPPILER_ID", objEnt.FUEL_SUPPILER_ID);
                        ObjPram[3] = new SqlParameter("@P_AMOUNT", objEnt.Fuel_Amount);
                        ObjPram[4] = new SqlParameter("@P_MFE_NO", objEnt.MFE_NO);
                        ObjPram[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_MONTHLYFUELEXPENSEENTRY_UI", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion

                public DataSet FillBoarding(string RouteNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROUTENO", RouteNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_BOARDNING_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.FillBoarding-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Transport_Fee_Report_EXCEL
                public DataSet GET_REPORT_EXCEL_NEW(int sessionno, string degreeno, int recontype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHlper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_RECONTYPE", recontype);
                        ds = objSqlHlper.ExecuteDataSetSP("PKG_VEH_TRANSPORT_FEES_REPORT_EXCEL_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GET_REPORT_EXCEL-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Transport AC Fee Updation
                public DataSet GetStudentDetails(int AdmBatchno, int Degreeno, int Branchno, int Semesterno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", AdmBatchno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", Semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_VEH_GET_STUDENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetAttendanceEntryBySuppiler-> " + ex.ToString());
                    }
                    return ds;


                }

                public int UpdateTransportAcFesStatus(int Idno, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[3];
                        ObjPram[0] = new SqlParameter("@P_IDNO", Idno);
                        ObjPram[1] = new SqlParameter("@P_STATUS", Status);
                        ObjPram[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_UPDATE_TRANSPORT_AC_FEE_STATUS", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion

                #region Create Bus Incharge

                public int AddUpdBusIncharge(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_BUS_INC_ID", objVM.BUS_INC_ID);
                        objParams[1] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[2] = new SqlParameter("@P_IDNO", objVM.IDNO);
                        objParams[3] = new SqlParameter("@P_VEHICLE_CATEGORY", objVM.VEHICLECAT);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_BUS_INCHARGE_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdBusIncharge->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetBusInchargeAll(int vehicle_cat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VEHICLE_CAT", vehicle_cat);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_BUS_INCHARGE_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetBusInchargeAll-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Vehicle Requisition

                public int InsUpdateVehicleRequisition(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[8];
                        ObjPram[0] = new SqlParameter("@P_VEH_REQ_ID", objEnt.VEH_REQ_ID);
                        ObjPram[1] = new SqlParameter("@P_COLLEGE_ID", objEnt.COLLEGE_ID);
                        ObjPram[2] = new SqlParameter("@P_DATE_OF_JOURNEY", objEnt.DATE_OF_JOURNEY);
                        ObjPram[3] = new SqlParameter("@P_ONE_WAY", objEnt.ONE_WAY);
                        ObjPram[4] = new SqlParameter("@P_UANO", objEnt.UANO);
                        ObjPram[5] = new SqlParameter("@P_VEHICLE_REQ_EMP_TBL", objEnt.VEHICLE_REQ_EMP_TBL);
                        ObjPram[6] = new SqlParameter("@P_VEHICLE_REQ_VEH_TBL", objEnt.VEHICLE_REQ_VEH_TBL);
                        ObjPram[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_REQUISITION_IU", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());

                    }
                    return retStatus;
                }

                public DataSet GetAllReqDetailsForEdit(int VehReqId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VEH_REQ_ID", VehReqId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_REQ_GET_DETAILS_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetAllReqDetailsForEdit-> " + ex.ToString());
                    }
                    return ds;
                }

                public int ApproveRejectVehReq(int VehReqId, char Status, int Uano, String Remark)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[5];
                        ObjPram[0] = new SqlParameter("@P_VEH_REQ_ID", VehReqId);
                        ObjPram[1] = new SqlParameter("@P_STATUS", Status);
                        ObjPram[2] = new SqlParameter("@P_UANO", Uano);
                        ObjPram[3] = new SqlParameter("@P_REMARK", Remark);
                        ObjPram[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_REQ_APPROVE_REJECT", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.ApproveRejectVehReq-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion

                #region Vehicle Allotment By TC

                public int AddUpdVehicleAllotmentByTC(VM objEnt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] ObjPram = null;
                        ObjPram = new SqlParameter[11];
                        ObjPram[0] = new SqlParameter("@P_VATC_ID", objEnt.VATC_ID);
                        ObjPram[1] = new SqlParameter("@P_SUPPILER_ID", objEnt.SUPPILER_ID);
                        ObjPram[2] = new SqlParameter("@P_VEHICLE_NO", objEnt.VEHICLE_NO);
                        ObjPram[3] = new SqlParameter("@P_ARRIVAL_TIME", objEnt.ARRIVAL_TIME);
                        ObjPram[4] = new SqlParameter("@P_DEPARTURE_TIME", objEnt.DEPARTURE_TIME);
                        ObjPram[5] = new SqlParameter("@P_TOT_KM_TRAVEL", objEnt.TOT_KM_TRAVEL);
                        ObjPram[6] = new SqlParameter("@P_TOT_HRS_TRAVEL", objEnt.TOT_HRS_TRAVEL);
                        ObjPram[7] = new SqlParameter("@P_AMOUNT_PAY", objEnt.AMOUNT_PAY);
                        ObjPram[8] = new SqlParameter("@P_VEH_REQ_ID", objEnt.VEH_REQ_ID);
                        ObjPram[9] = new SqlParameter("@P_CREATED_BY", objEnt.USERNO);
                        ObjPram[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        ObjPram[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_ALLOT_BY_TRANSPORT_INCHRG", ObjPram, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdVehicleAllotmentByTC-> " + ex.ToString());

                    }
                    return retStatus;
                }

                #endregion


                #region Tour/ Event
                public int TourEventInsertUpdate(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_TEID", objVM.TEID);
                        objParams[1] = new SqlParameter("@P_TOUREVENTDATE", objVM.TOUREVENTDATE);
                        objParams[2] = new SqlParameter("@P_VEHICLECAT", objVM.VEHICLECAT);
                        objParams[3] = new SqlParameter("@P_VIDNO", objVM.VIDNO);
                        objParams[4] = new SqlParameter("@P_OUTTIME", objVM.OUTTIME);
                        objParams[5] = new SqlParameter("@P_OUTKM", objVM.OUTKM);
                        objParams[6] = new SqlParameter("@P_PURPOSE", objVM.PURPOSE);
                        objParams[7] = new SqlParameter("@P_PLACE", objVM.PLACE);
                        objParams[8] = new SqlParameter("@P_LDNO", objVM.LDNO);
                        objParams[9] = new SqlParameter("@P_INTIME", objVM.INTIME);
                        objParams[10] = new SqlParameter("@P_INKM", objVM.INKM);
                        objParams[11] = new SqlParameter("@P_MALE_COUNT", objVM.MALE_COUNT);
                        objParams[12] = new SqlParameter("@P_FEMALE_COUNT", objVM.FEMALE_COUNT);
                        objParams[13] = new SqlParameter("@P_CHILDREN_COUNT", objVM.CHILDREN_COUNT);
                        objParams[14] = new SqlParameter("@P_INFANT_COUNT", objVM.INFANT_COUNT);
                        objParams[15] = new SqlParameter("@P_TOTAL_NO_PATIENT", objVM.TOTAL_NO_PATIENT);
                        objParams[16] = new SqlParameter("@P_TOTAL_KM", objVM.TOTAL_KM);
                        objParams[17] = new SqlParameter("@P_UANO", objVM.USERNO);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_TOUR_EVENT_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.TourEventInsertUpdate->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetTourEventByTEId(int TEID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TEID", TEID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_TOUR_EVENT_BYID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTourEventByTEId-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetTourEventAll(int vehicle_cat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VEHICLE_CAT", vehicle_cat);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_TOUR_EVENT_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTourEventAll-> " + ex.ToString());
                    }
                    return ds;
                }




                #endregion

                #region Late Coming Vehicle Penalty
                public int AddUpdLateComingVehPenalty(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_PENALTY_ID", objVM.PENALTY_ID);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objVM.COLLEGE_ID);
                        objParams[2] = new SqlParameter("@P_VEHICLE_ID", objVM.VEHICLE_ID);
                        objParams[3] = new SqlParameter("@P_ARRIVAL_DATE", objVM.ARRIVAL_DATE);
                        objParams[4] = new SqlParameter("@P_ARRIVAL_TIME", objVM.ARRIVAL_TIME);
                        objParams[5] = new SqlParameter("@P_ACTUAL_ARRIVAL_TIME", objVM.ACTUAL_ARRIVAL_TIME);
                        objParams[6] = new SqlParameter("@P_LATE_HOURS", objVM.LATE_HOURS);
                        objParams[7] = new SqlParameter("@P_FINE_AMOUNT", objVM.FINE_AMOUNT);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_LATE_COME_PENALTY_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdLateComingVehPenalty->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Ambulance Maintanance
                public int AddUpdAmbulanceMaintanance(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_AM_ID", objVM.AM_ID);
                        objParams[1] = new SqlParameter("@P_DATE", objVM.DATE);
                        objParams[2] = new SqlParameter("@P_BILLNO", objVM.BILLNO);
                        objParams[3] = new SqlParameter("@P_SUPPLIERS_NAME", objVM.SUPPLIERS_NAME);
                        objParams[4] = new SqlParameter("@P_DISCRIPTION_NAME", objVM.DISCRIPTION_NAME);
                        objParams[5] = new SqlParameter("@P_RATE", objVM.RATE);
                        objParams[6] = new SqlParameter("@P_AMOUNT", objVM.AMOUNT);
                        objParams[7] = new SqlParameter("@P_CGST", objVM.CGST);
                        objParams[8] = new SqlParameter("@P_SGST", objVM.SGST);
                        objParams[9] = new SqlParameter("@P_TOTAL_AMOUNT", objVM.TOTAL_AMOUNT);
                        objParams[10] = new SqlParameter("@P_INCHARGE", objVM.INCHARGE);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_AMBULANCE_MAINTENANCE_INS_UPD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }

                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdLateComingVehPenalty->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion



                #region Vehicle Category Master
                /// <summary>
                /// CREATED BY   : VIDISHA KAMATKAR
                /// CREATED DATE : 19-APR-2021
                /// DESCRIPTION  : USED TO INSERT AND UPDATE VEHICLE CATEGORY NAME.
                /// </summary>
                /// <param name="objVM"></param>
                /// <returns></returns>
                public int AddUpdateVCMaster(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@VCID", objVM.VCID);
                        objParams[1] = new SqlParameter("@CategoryName", objVM.CATEGORYNAME);
                        objParams[2] = new SqlParameter("@Amount", objVM.AMOUNT);
                        objParams[3] = new SqlParameter("@CollegeNo", objVM.CollegeNo);
                        objParams[4] = new SqlParameter("@CreatedBy", objVM.UANO);
                        objParams[5] = new SqlParameter("@ModifiedBy", objVM.UANO);
                        objParams[6] = new SqlParameter("@ModifiedDate", objVM.ModifiedDate);
                        objParams[7] = new SqlParameter("@SESSIONNO", objVM.SESSIONNO);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_VEHICLE_CATEGORY_NAME_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 0)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdateVCMaster->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetStudentListForBusPass(int amdbatch, int clgnameint, int degreeno, int branchno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_BATCHNO", amdbatch);
                        objParams[1] = new SqlParameter("@P_CLGNO", clgnameint);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        //objParams[5] = new SqlParameter("@P_STUDENTIDTYPE", idcard);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_STUDENT_BUS_PASS", objParams);

                    }
                    catch (Exception ex)
                    {



                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetStudentListForBusPass-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion


                public DataSet GetStudentPersonalDetails(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_STUDENT_DETAILS_LIST_STUDLOGIN", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetStudentPersonalDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdVehicleTransportApplication(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_VTRAID", objVM.VTRAID);
                        objParams[1] = new SqlParameter("@P_APP_DATE", objVM.APP_DATE);
                        objParams[2] = new SqlParameter("@P_UANO", objVM.UANO);
                        objParams[3] = new SqlParameter("@P_VCID", objVM.VCID);
                        objParams[4] = new SqlParameter("@P_STOPNO", objVM.STOPNO);
                        objParams[5] = new SqlParameter("@P_PERIOD_FROM", objVM.PERIOD_FROM);
                        objParams[6] = new SqlParameter("@P_PERIOD_TO", objVM.PERIOD_TO);
                        //objParams[7] = new SqlParameter("@P_SESSIONNO", objVM.SESSIONNO);
                        //objParams[8] = new SqlParameter("@P_S_SEMESTER", objVM.S_SEMESTER);  
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_VEHICLE_TRANSPORT_APPLICATION_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);                        
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdVehicleTransportApplication->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetTransportRequisitionApplicationList(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("[PKG_VEHICLE_GET_TRANSPORT_REQUISITION_APPLICATION_LIST]", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetTransportRequisitionApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }


                public int CreateDemandForTransport(string studentIDs, int CurrentYear, int NextYear, string ReceiptCode, int college_id, int SessionNo, int UANO, string CollegeCode)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_STUDENT_IDS", studentIDs);
                        objParams[1] = new SqlParameter("@P_CURRRENT_YEAR", CurrentYear);
                        objParams[2] = new SqlParameter("@P_NEXT_YEAR", NextYear);
                        objParams[3] = new SqlParameter("@P_RECEIPT_CODE", ReceiptCode);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[5] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", UANO);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);   
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_CREATE_DEMAND_FOR_NEXT_YEAR", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdVehicleTransportApplication->" + ex.ToString());
                    }
                    return retstatus;
                }





                #region Cancel Transport Status

                // This method is used to update transport status in student table.
                public int UpdateCancelTransportStatus(DataTable StudentId, string TransportType, int CollegeId)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STUDENTID_TBL", StudentId);
                        //objParams[1] = new SqlParameter("@P_TRANSPORTTYPE", TransportType);
                       // objParams[2] = new SqlParameter("@P_COLLEGEID", CollegeId);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_CANCEL_TRANSPORT_STATUS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.UpdateCancelTransportStatus->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion



                #endregion
                #region Bus Structure Mapping

                public int AddUpdateBusStructure(VM objVM, int Org) 
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BUS_STRUCTURE_DATA_TBL", objVM.Bus_Structure_Data_TBL);
                        objParams[1] = new SqlParameter("@P_OrganizationId", Org);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_BUS_STRUCTURE_MAPPING_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.BusStructureMapping->" + ex.ToString());
                    }
                    return retstatus;
                }

             

                public DataSet BindBusStructureMapping()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_BUS_STRUCTURE_MAPPING_BIND_DATA", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetRouteDataByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddBusStructureImage(VM objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ROUTEID", objVM.Route);
                        objParams[1] = new SqlParameter("@P_BUSSTR_ID", objVM.BusStructure);
                        objParams[2] = new SqlParameter("@P_FILE_NAME", objVM.UploadBusStructure);
                        objParams[3] = new SqlParameter("@P_FILE_PATH", objVM.UploadBlobName);
                        objParams[4] = new SqlParameter("@P_IsBlob", objVM.IsBlob);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_INS_BUS_STRUCTURE_MAPPING_IMAGE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.BusStructureMapping->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet GetRouteFees(int stop, int route)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ROUTECODE", stop);
                        objParams[1] = new SqlParameter("@P_ROUTEID", route);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_ROUTE_FEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }


                //Demand Create

                public int AddStudentExamRegistrationDetails(VM objVM, string Amt, string order_id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details

                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objVM.SESSIONN);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objVM.SCHEMENO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objVM.SEMESTERNOS);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objVM.COURSENOS);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objVM.IPADDRESS1);
                        objParams[5] = new SqlParameter("@P_IDNOS", objVM.IDNO1);
                        objParams[6] = new SqlParameter("@P_REGNO", objVM.REGNO1);
                        objParams[7] = new SqlParameter("@P_UA_NO", objVM.UA_NO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objVM.COLLEGE_CODE1);
                        objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                        objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BUS_FEES_REGISTRATION_DETAILS_FOR_STUDENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;
                }


                //dcr temp

                public int InsertOnlinePayment_TempDCR(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_PAYSERVICETYPE", PAYSERVICETYPE),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_MESSAGE",msg),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_NON_ACAD_INSERT_ONLINE_PAYMENT_DCR", param, true);

                        if (ret != null && ret.ToString() != "-99")
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // INSERTB DCR TABLE 

                public int InsertOnlinePayment_Non_DCR(string UserNo, string recipt, string payId, string transid, string PaymentMode, string CashBook, string amount, string StatusF, string Regno, string msg)
                {
                    int retStatus1 = 0;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = null;
                        {
                            sqlparam = new SqlParameter[10];
                            sqlparam[0] = new SqlParameter("@P_IDNO", UserNo);
                            sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                            sqlparam[2] = new SqlParameter("@P_PAYID", payId);
                            sqlparam[3] = new SqlParameter("@P_TRANSID", transid);
                            sqlparam[4] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                            sqlparam[5] = new SqlParameter("@P_CASHBOOK", CashBook);
                            sqlparam[6] = new SqlParameter("@P_AMOUNT", amount);
                            sqlparam[7] = new SqlParameter("@P_PAY_STATUS", StatusF);
                            sqlparam[8] = new SqlParameter("@P_MESSAGE", msg);
                            //sqlparam[9] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                            sqlparam[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            sqlparam[9].Direction = ParameterDirection.Output;
                            string idcat = sqlparam[4].Direction.ToString();

                        };
                        object studid = objSqlhelper.ExecuteNonQuerySP("PKG_NON_ACAD_INSERT_ONLINE_PAYMENT_DCRTEMP_TO_DCR", sqlparam, true);

                        retStatus1 = Convert.ToInt32(studid);
                        return retStatus1;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
                    }
                }


                //Insert Bus Booking data
                public DataSet AddBusBookingDetails(int Idno, string lblstudentname, int session, int Semestr, int BusBookingRouteId, int BusBookingStopId, int BusBookingSeatNo, Decimal lblamount, string orderid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_NAME", lblstudentname);
                        objParams[2] = new SqlParameter("@P_SESSION", session);
                        objParams[3] = new SqlParameter("@P_SEMESTER", Semestr);
                        objParams[4] = new SqlParameter("@P_ROUTEID", BusBookingRouteId);
                        objParams[5] = new SqlParameter("@P_STOPID", BusBookingStopId);
                        objParams[6] = new SqlParameter("@P_SEAT_NO", BusBookingSeatNo);
                        objParams[7] = new SqlParameter("@P_FEES", lblamount);
                        objParams[8] = new SqlParameter("@P_ORD_ID", orderid);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_BUS_BUS_BOOKING_INSERT", objParams, true);


                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.BusStructureMapping->" + ex.ToString());
                    }
                    return ds;
                }

                // Online Payment Log
                public int InsertOnlinePaymentlog(string userno, string recipt, string PaymentMode, string Amount, string status, string PayId, int sessionbfr, int Semesterbfr, int BusRoute, int BusStope, int BusSeat)
                {
                    int retStatus1 = 0;
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = null;
                        {
                            sqlparam = new SqlParameter[12];
                            sqlparam[0] = new SqlParameter("@P_IDNO", userno);
                            sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                            sqlparam[2] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                            sqlparam[3] = new SqlParameter("@P_AMOUNT", Amount);
                            sqlparam[4] = new SqlParameter("@P_STATUS", status);
                            sqlparam[5] = new SqlParameter("@P_PAYID", PayId);
                            sqlparam[6] = new SqlParameter("@P_SESSION", sessionbfr);
                            sqlparam[7] = new SqlParameter("@P_SEMESTER", Semesterbfr);
                            sqlparam[8] = new SqlParameter("@P_ROUTEID", BusRoute);
                            sqlparam[9] = new SqlParameter("@P_STOPID", BusStope);
                            sqlparam[10] = new SqlParameter("@P_SEAT_NO", BusSeat);
                            sqlparam[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            sqlparam[11].Direction = ParameterDirection.Output;
                            string idcat = sqlparam[4].Direction.ToString();

                        };
                        object studid = objSqlhelper.ExecuteNonQuerySP("PKG_NON_ACD_ONLINE_PAYMENT_FOR_LOG_ADMIN", sqlparam, true);
                        //if (Convert.ToInt32(studid) == -99)
                        //{
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //}
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        retStatus1 = Convert.ToInt32(studid);
                        return retStatus1;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
                    }

                }

                
                // Added For get PG ConfigurationDetails
              
                public DataSet GetOnlinePaymentConfigurationDetails(int Organizationid, int payid, int activityno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATIONID", Organizationid),
                    new SqlParameter("@P_ACTIVITYNO", activityno),
                    new SqlParameter("@P_PAYID",payid)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_CONFIG_DETAILS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetOnlinePaymentConfigurationDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;

                }

                // Get Stope Acording to Route
                public DataSet GetStope(int route)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROUTEID", route);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_STOPE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }
                // Bind Bus Structure 

                public DataSet BindBusStructureDynamicGrid(int route)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROUTEID", route);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_BUS_STRUCTURE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetFuelEntryById-> " + ex.ToString());
                    }
                    return ds;
                }

                //Get ItemPurchease Report data for Excel Report
                public DataSet GetItemPurchaseReportForExcel(DateTime fdate, DateTime Todate, int itemtype)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_ITEM_TYPE", itemtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_ITEM_PURCHASE_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }


                //Get Fuel Transport Report data for Excel Report
                public DataSet GetItemIssueReportForTransport(DateTime fdate, DateTime Todate, int issuetype, int vehicle)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_ISSUETYPE", issuetype);
                        objParams[3] = new SqlParameter("@P_VEHICLE", vehicle);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_ITEM_ISSUE_FOR_TRANSPORT_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }

                //Get Fuel Other Than Transport Report data for Excel Report
                public DataSet GetItemIssueReportForOtherThanTransport(DateTime fdate, DateTime Todate, int issuetype)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_ISSUETYPE", issuetype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_ITEM_ISSUE_FOR_OTHER_THAN_TRANSPORT_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }

                //Get Indent Report data for Excel Report
                public DataSet GetItemIssueReportForIndent(DateTime fdate, DateTime Todate, char VehCateg)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_VEHICLE_CATEGORY", VehCateg);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_ITEM_ISSUE_DETAILS_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }





                public int AddBusCheckSeats(int idno, int SECTIONNO, int SEMESTERNO, int Routid, int Stopeid, int seat, decimal fees, int IsConfirm)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSION", SECTIONNO);
                        objParams[2] = new SqlParameter("@P_SEMESTER", SEMESTERNO);
                        objParams[3] = new SqlParameter("@P_ROUTEID  ", Routid);
                        objParams[4] = new SqlParameter("@P_STOPID", Stopeid);
                        objParams[5] = new SqlParameter("@P_SEAT_NO", seat);
                        objParams[6] = new SqlParameter("@P_FEES", fees);
                        objParams[7] = new SqlParameter("@P_IsConfirm", IsConfirm);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_VEH_BUS_CHECK_SEATS", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retstatus = Convert.ToInt32(CustomStatus.Error);
                        //else
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.FuelEntryInsertUpdate->" + ex.ToString());
                    }
                    return retstatus;
                }






                //Get Online Payment Report data for Excel Report
                public DataSet GetOnlinePaymentReportForExcel()
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_VEH_GET_BUS_ONLINE_PAYMENT_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetStudentTransportListForIdentityCard(int routeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROUTE_ID", routeid);


                       // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_VEHCLE_TRANSPORT_STUDENT_SEARCH_IDENTITY_CARD", objParams, true);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHCLE_TRANSPORT_STUDENT_SEARCH_IDENTITY_CARD", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetStudentTransportListForIdentityCard->" + ex.ToString());
                    }
                    return ds;
                }


                #endregion
            }
        }
    }
}