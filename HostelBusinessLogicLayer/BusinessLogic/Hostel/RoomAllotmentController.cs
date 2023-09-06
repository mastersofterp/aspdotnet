//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : ROOM ALLOTMENT CONTROLLER                                            
// CREATION DATE : 17-AUG-2009                                                          
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI                                    
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class RoomAllotmentController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AllotRoom(RoomAllotment roomAllotment, int hostelNo, int Mess_no, string VehicleType, string VehicleName, string VehicleNo ,int OrganizationId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
                    new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
                    new SqlParameter("@P_REMARK", roomAllotment.Remark),
                    new SqlParameter("@P_USER_NO", roomAllotment.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
                    new SqlParameter("@P_HOSTEL_NO", hostelNo),
                    new SqlParameter("@P_MESS_NO", Mess_no),
                    new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
                    new SqlParameter("@P_VEHICLE_NAME", VehicleName),
                    new SqlParameter("@P_VEHICLE_NO", VehicleNo),
                    new SqlParameter("@P_ORGANIZATION_ID", OrganizationId),
                    //new SqlParameter("@P_ToDate", todate),
                    //new SqlParameter("@P_FromDate", fromdate ),
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOTMENT_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        // Added by Saurabh L on 21 June 2023
        public int AllotRoomWithoutDemand(RoomAllotment roomAllotment, int hostelNo, int Mess_no, string VehicleType, string VehicleName, string VehicleNo, int OrganizationId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
                    new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
                    new SqlParameter("@P_REMARK", roomAllotment.Remark),
                    new SqlParameter("@P_USER_NO", roomAllotment.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
                    new SqlParameter("@P_HOSTEL_NO", hostelNo),
                    new SqlParameter("@P_MESS_NO", Mess_no),
                    new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
                    new SqlParameter("@P_VEHICLE_NAME", VehicleName),
                    new SqlParameter("@P_VEHICLE_NO", VehicleNo),
                    new SqlParameter("@P_ORGANIZATION_ID", OrganizationId),
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOTMENT_INSERT_WITHOUT_DEMAND", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        
        public int AllotandApplyRoom(RoomAllotment roomAllotment, int hostelNo, int Mess_no, string VehicleType, string VehicleName, string VehicleNo, int bankno, string accountNo, string adharno, string MobileNo, string cursem, string applysem)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
                    new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
                    new SqlParameter("@P_REMARK", roomAllotment.Remark),
                    new SqlParameter("@P_USER_NO", roomAllotment.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
                    new SqlParameter("@P_HOSTEL_NO", hostelNo),
                    new SqlParameter("@P_MESS_NO", Mess_no),
                    new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
                    new SqlParameter("@P_VEHICLE_NAME", VehicleName),
                    new SqlParameter("@P_VEHICLE_NO", VehicleNo),
                   
                    new SqlParameter("@P_BANKNO",bankno),
                    new SqlParameter("@P_ACC_NO",accountNo),
                    new SqlParameter("@P_ADHARNO",adharno),
                    new SqlParameter("@P_STUDENTMOBILE",MobileNo),
                    new SqlParameter("@P_CUR_SEM ",cursem),
                    new SqlParameter("@P_APP_SEM ",applysem),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOTMENT_AND_APPLY_INSERT", sqlParams, true);
                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //public int AllotRoom(RoomAllotment roomAllotment, int hostelNo, int Mess_no, string VehicleType, string VehicleName, string VehicleNo, int bankno, string acc_no, string bank_branch, int payment_type, string dd_no, DateTime dd_Date, string dd_bank, string dd_City, int dd_BanckNo)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
        //            new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
        //            new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
        //            new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
        //            new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
        //            new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
        //            new SqlParameter("@P_REMARK", roomAllotment.Remark),
        //            new SqlParameter("@P_USER_NO", roomAllotment.UserNo),
        //            new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
        //            new SqlParameter("@P_HOSTEL_NO", hostelNo),
        //            new SqlParameter("@P_MESS_NO", Mess_no),
        //            new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
        //            new SqlParameter("@P_VEHICLE_NAME", VehicleName),
        //            new SqlParameter("@P_VEHICLE_NO", VehicleNo),
        //            new SqlParameter("@P_BANKNO", bankno),
        //            new SqlParameter("@P_ACC_NO", acc_no),
        //            new SqlParameter("@P_BANK_BRANCH", bank_branch),
        //            new SqlParameter("@P_PAYMENT_TYPE",payment_type),
        //            new SqlParameter("@P_DD_NO",dd_no),
        //            new SqlParameter("@P_DD_DT",dd_Date),
        //            new SqlParameter("@P_DD_BANK",dd_bank),
        //            new SqlParameter("@P_DD_CITY",dd_City),
        //            new SqlParameter("@P_DBANKNO",dd_BanckNo),
        //            new SqlParameter("@P_ROOM_ALLOTMENT_NO", status)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOTMENT_INSERT", sqlParams, true);

        //        if (obj != null && obj.ToString() != "-99")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}


        //public int AllotRoom(RoomAllotment roomAllotment, int hostelNo, string VehicleType, string VehicleName, string VehicleNo)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
        //            new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
        //            new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
        //            new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
        //            new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
        //            new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
        //            new SqlParameter("@P_REMARK", roomAllotment.Remark),
        //            new SqlParameter("@P_USER_NO", roomAllotment.UserNo),
        //            new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
        //            new SqlParameter("@P_HOSTEL_NO", hostelNo),
        //            new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
        //            new SqlParameter("@P_VEHICLE_NAME", VehicleName),
        //            new SqlParameter("@P_VEHICLE_NO", VehicleNo),
        //            new SqlParameter("@P_ROOM_ALLOTMENT_NO", status)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOT_INSERT", sqlParams, true);     //-----new PROCEDURE

        //        if (obj != null && obj.ToString() != "-99")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        public DataSet GetStudentInfoById(int studentId, int OrgId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {

                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_ORGANIZATION_ID",OrgId)

                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AllotGuestRoom(RoomAllotment roomAllotment, int hostelNo, int Mess_no, string VehicleType, string VehicleName, string VehicleNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
                    new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
                    new SqlParameter("@P_REMARK", roomAllotment.Remark),
                    new SqlParameter("@P_USER_NO", roomAllotment.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
                    new SqlParameter("@P_HOSTEL_NO", hostelNo),
                    new SqlParameter("@P_MESS_NO", Mess_no),
                    new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
                    new SqlParameter("@P_VEHICLE_NAME", VehicleName),
                    new SqlParameter("@P_VEHICLE_NO", VehicleNo),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    //new SqlParameter("@P_ToDate", todate),
                    //new SqlParameter("@P_FromDate", fromdate ),
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", SqlDbType.Int)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_GUEST_ROOM_ALLOTMENT_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        public int UpdateRoomAllotment(RoomAllotment roomAllotment, int hostelNo, int Mess_no, string VehicleType, string VehicleName, string VehicleNo, int bankno, string acc_no, string bank_branch, int payment_type, string dd_no, DateTime dd_Date, string dd_bank, string dd_City, int dd_BanckNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
                    new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
                    new SqlParameter("@P_REMARK", roomAllotment.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
                    new SqlParameter("@P_HOSTEL_NO", hostelNo),
                    new SqlParameter("@P_MESS_NO", Mess_no),
                    new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
                    new SqlParameter("@P_VEHICLE_NAME", VehicleName),
                    new SqlParameter("@P_VEHICLE_NO", VehicleNo),
                    new SqlParameter("@P_BANKNO", bankno),
                    new SqlParameter("@P_ACC_NO", acc_no),
                    new SqlParameter("@P_BANK_BRANCH", bank_branch),
                    new SqlParameter("@P_PAYMENT_TYPE",payment_type),
                    new SqlParameter("@P_DD_NO",dd_no),
                    new SqlParameter("@P_DD_DT",dd_Date),
                    new SqlParameter("@P_DD_BANK",dd_bank),
                    new SqlParameter("@P_DD_CITY",dd_City),
                    new SqlParameter("@P_DBANKNO",dd_BanckNo),
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", roomAllotment.RoomAllotmentNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOTMENT_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.UpdateRoomAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        public int UpdateRoomAllotment(RoomAllotment roomAllotment, int hostelNo, string VehicleType, string VehicleName, string VehicleNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO", roomAllotment.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", roomAllotment.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomAllotment.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", roomAllotment.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", roomAllotment.AllotmentDate),
                    new SqlParameter("@P_CAN", roomAllotment.IsCanceled),
                    new SqlParameter("@P_REMARK", roomAllotment.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode),
                    new SqlParameter("@P_HOSTEL_NO", hostelNo),
                    new SqlParameter("@P_VEHICLE_TYPE", VehicleType),
                    new SqlParameter("@P_VEHICLE_NAME", VehicleName),
                    new SqlParameter("@P_VEHICLE_NO", VehicleNo),
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", roomAllotment.RoomAllotmentNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOT_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.UpdateRoomAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllRoomAllotments(int OrganizationId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_ORGANIZATION_ID", OrganizationId)
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_ALLOTMENT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetAllRoomAllotments() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetRoomAllotmentByNo(int roomAllotmentNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", roomAllotmentNo),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_ALLOTMENT_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomAllotmentByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by sonali on 22/02/2023 purpose to get room alloted students for current session
        public DataSet GetRoomAllotmentInfoByResidentNo(int residentNo, int OrganizationId,int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_RESIDENT_NO", residentNo),
                    new SqlParameter("@P_ORGANIZATION_ID", OrganizationId),
                    new SqlParameter("@P_SESSIONO",sessionno)

                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_ALLOTMENT_GET_BY_RESIDENT_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomAllotmentInfoByResidentNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetGuestInfoByNo(int guestNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_GUEST_NO", guestNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GUEST_INFO_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetGuestInfoByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet SearchGuestsByName(string guestName)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_GUEST_NAME", guestName) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GUEST_INFO_GET_BY_NAME", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.SearchGuestsByName() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAvailableRooms(Room roomSearchCriteria)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_BLOCK_NO", roomSearchCriteria.BlockNo),
                    new SqlParameter("@P_FLOOR_NO", roomSearchCriteria.FloorNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", roomSearchCriteria.ResidentTypeNo),
                    new SqlParameter("@P_BRANCHNO", roomSearchCriteria.BranchNo),
                    new SqlParameter("@P_SEMESTERNO", roomSearchCriteria.SemesterNo),
                    new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_ALLOTMENT_GET_AVAILABLE_ROOMS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetAvailableRooms() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public bool CancelRoomAllotment(RoomAllotment roomAllotment, int roomAllotmentNo, string remark, int CancelHostel, int OrganizationId)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[20];
                sqlParams[0] = new SqlParameter("@P_IDNO", roomAllotment.ResidentNo);
                sqlParams[1] = new SqlParameter("@P_SESSIONNO", roomAllotment.HostelSessionNo);
                sqlParams[2] = new SqlParameter("@P_UA_NO", roomAllotment.UserNo);
                sqlParams[3] = new SqlParameter("@P_IP_ADDRESS", roomAllotment.IP_Address);
                sqlParams[4] = new SqlParameter("@P_PERCENTAGE", roomAllotment.Percentage);
                sqlParams[5] = new SqlParameter("@P_HOSTEL_CHARGE", roomAllotment.Hostel_Charge);
                sqlParams[6] = new SqlParameter("@P_DAYS", roomAllotment.Days);
                sqlParams[7] = new SqlParameter("@P_PERDAY_CHARGE", roomAllotment.PerDayCharge);
                sqlParams[8] = new SqlParameter("@P_MESS_CHARGE", roomAllotment.MessCharge);
                sqlParams[9] = new SqlParameter("@P_MESSMONTH_CHARGE", roomAllotment.MessMonthCharge);
                sqlParams[10] = new SqlParameter("@P_OTHER_CHARGE", roomAllotment.OtherCharge);
                sqlParams[11] = new SqlParameter("@P_TOTAL_AMT", roomAllotment.TotalAmt);
                sqlParams[12] = new SqlParameter("@P_REFUND_AMT", roomAllotment.RefundAmt);
                sqlParams[13] = new SqlParameter("@P_CHEQUE_NO", roomAllotment.ChequeNo);
                if (roomAllotment.ChequeDate == DateTime.MinValue)
                    sqlParams[14] = new SqlParameter("@P_CHEQUE_DATE", DBNull.Value);
                else
                    sqlParams[14] = new SqlParameter("@P_CHEQUE_DATE", roomAllotment.ChequeDate);
                sqlParams[15] = new SqlParameter("@P_COLLEGE_CODE", roomAllotment.CollegeCode);
                sqlParams[16] = new SqlParameter("@P_ROOM_ALLOTMENT_NO", roomAllotmentNo);
                sqlParams[17] = new SqlParameter("@P_REMARK", remark);
                sqlParams[18] = new SqlParameter("@P_CANCELHOSTEL", CancelHostel);  // Added for solution BUG_ID 164055 
                sqlParams[19] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                objDataAccess.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_ALLOTMENT_CANCELLATION", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RoomAllotmentController.CancelRoomAllotment() --> " + ex.Message + " " + ex.StackTrace);
                //return false;
            }
            return true;
        }

        public bool ChangeRoomAllotment(int idno, int sessionno, int oldroomNo, int newroomNo, int oldhostelNo, int newhostelNo, string remark)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@P_IDNO", idno);
                sqlParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                sqlParams[2] = new SqlParameter("@P_NEWHOSTELNO", newhostelNo);
                sqlParams[3] = new SqlParameter("@P_OLDHOSTELNO", oldhostelNo);
                sqlParams[4] = new SqlParameter("@P_NEWROOMNO", newroomNo);
                sqlParams[5] = new SqlParameter("@P_OLDROOMNO ", oldroomNo);
                sqlParams[6] = new SqlParameter("@P_REMARK", remark);
                sqlParams[7] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objDataAccess.ExecuteNonQuerySP("PKG_CHANGE_HOSTEL_ROOM_ALLOTMENT", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.RoomAllotmentController.CancelRoomAllotment() --> " + ex.Message + " " + ex.StackTrace);
                //return false;
            }
            return true;
        }
        
        public DataSet GetOtherStdFeesForDemandCreation()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_OTHER_STD_FEE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetOtherStdFeesForDemandCreation() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentInfoByIdno(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENT_INFO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetStudentInfoByIdno() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        // Apply Hostel
        // Apply Hostel

        public int ApplyHostel(int idno, int sessionno, int status, int ua_no, int cityno, int stateno, string paddress, int bankno, string accountNo, string BankBranch, string MobileNo)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@P_IDNO", idno);
                sqlParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                sqlParams[2] = new SqlParameter("@P_STATUS", status);
                sqlParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                sqlParams[4] = new SqlParameter("@P_PADDRESS", paddress);
                sqlParams[5] = new SqlParameter("@P_PCITY", cityno);
                sqlParams[6] = new SqlParameter("@P_PSTATE", stateno);
                sqlParams[7] = new SqlParameter("@P_BANKNO", bankno);
                sqlParams[8] = new SqlParameter("@P_ACC_NO", accountNo);
                sqlParams[9] = new SqlParameter("@P_BANK_BRANCH", BankBranch);
                sqlParams[10] = new SqlParameter("@P_STUDENTMOBILE", MobileNo);

                sqlParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[11].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_APPLY_STUDENT_INSERT", sqlParams, false);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.ApplyHostel() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int CutOffStudent(string studIds, int sessionno, int ua_no)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@P_IDNOS", studIds);
                sqlParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                sqlParams[2] = new SqlParameter("@P_CUT_UA_NO", ua_no);
                sqlParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_CUTOFF_STUDENT_UPDATE", sqlParams, false);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.CutOffStudent() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        //Added by Saurabh L on 30/12/2022
        public DataSet GetFloorNo(string floorNo, int BlockNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_BLOCK_NO", BlockNo);
                objParams[1] = new SqlParameter("@P_FLOORS", floorNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_FLOOR", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetFloorNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Saurabh L on 04/01/2022 Purpose: Insert attendance by Attendance Incharge
        public int DailyAttendanceInsertByIncharge(int hostelSession, int hostelNo, int ua_no, string studIds, string att_status, string remarknos, DateTime attDate, string attTime, string col_code, int OrganizationId, int block_no)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@P_HOSTEL_SESSIONNO", hostelSession);
                sqlParams[1] = new SqlParameter("@P_HOSTELNO", hostelNo);
                sqlParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                sqlParams[3] = new SqlParameter("@P_STUDIDS", studIds);
                sqlParams[4] = new SqlParameter("@P_ATT_STATUS", att_status);
                sqlParams[5] = new SqlParameter("@P_REMARKNO", remarknos);
                sqlParams[6] = new SqlParameter("@P_ATT_DATE", attDate);
                sqlParams[7] = new SqlParameter("@P_ATT_TIME", attTime);
                sqlParams[8] = new SqlParameter("@P_COLLEGE_CODE", col_code);
                sqlParams[9] = new SqlParameter("@P_BLOCK_NO", block_no);
                sqlParams[10] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                sqlParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[11].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STUDENT_ATTENDANCE_INCHARGEWISE_INSERT", sqlParams, false);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.DailyAttendanceInsertByIncharge() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        //Added by Saurabh L on 04/01/2022 Purpose: Fetch data for Attendance Incharge
        public DataSet DailyAttendanceSelectInchargeBlockwise(int hostelSession, int hostelNo, string attDate,int block_no,int ua_no, int OrganizationId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_HOSTEL_SESSIONNO", hostelSession);
                objParams[1] = new SqlParameter("@P_HOSTELNO", hostelNo);
                objParams[2] = new SqlParameter("@P_ATT_DATE", attDate);
                objParams[3] = new SqlParameter("@P_BLOCKNO", block_no);
                objParams[4] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[5] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_ATTENDANCE_DAYWISE_BY_INCHARGE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.DailyAttendanceSelectInchargeBlockwise() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



        //Attendance
        public int DailyAttendanceInsert(int hostelSession, int hostelNo, int ua_no, string studIds, string att_status, string remarknos, DateTime attDate, string attTime, string col_code, int OrganizationId)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[11];
                sqlParams[0] = new SqlParameter("@P_HOSTEL_SESSIONNO", hostelSession);
                sqlParams[1] = new SqlParameter("@P_HOSTELNO", hostelNo);
                sqlParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                sqlParams[3] = new SqlParameter("@P_STUDIDS", studIds);
                sqlParams[4] = new SqlParameter("@P_ATT_STATUS", att_status);
                sqlParams[5] = new SqlParameter("@P_REMARKNO", remarknos);
                sqlParams[6] = new SqlParameter("@P_ATT_DATE", attDate);
                sqlParams[7] = new SqlParameter("@P_ATT_TIME", attTime);
                sqlParams[8] = new SqlParameter("@P_COLLEGE_CODE", col_code);
                sqlParams[9] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                sqlParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STUDENT_ATTENDANCE_INSERT", sqlParams, false);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.DailyAttendanceInsert() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet DailyAttendanceSelect(int hostelSession, int hostelNo, string attDate, int OrganizationId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_HOSTEL_SESSIONNO", hostelSession);
                objParams[1] = new SqlParameter("@P_HOSTELNO", hostelNo);
                objParams[2] = new SqlParameter("@P_ATT_DATE", attDate);
                objParams[3] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_ATTENDANCE_DAYWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.DailyAttendanceSelect() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet MonthlyAttandance(int sessionno, int hostelno, int fromMonth, int toMonth, int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_HOSTEL_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_HOSTELNO", hostelno);
                objParams[2] = new SqlParameter("@P_FROM_MONTH", fromMonth);
                objParams[3] = new SqlParameter("@P_TO_MONTH", toMonth);
                objParams[4] = new SqlParameter("@P_IDNO", idno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ATTENDANCE_MONTHWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.MonthlyAttandance() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // show hostel guest room allotment

        public DataSet GetGuestRoomAllot(int guestno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objsql = new SQLHelper(connectionString);
                SqlParameter[] objparams = new SqlParameter[2];
                {
                    objparams[0] = new SqlParameter("@P_GUESTNO", guestno);
                    objparams[1] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                    ds = objsql.ExecuteDataSetSP("PKG_HOSTEL_RESIDENT_ROOM_ALLOTMENT_SELECT", objparams);
                }
                

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetGuestRoomAllot() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetHostelStudInfoByIdno(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_INFO_SEARCH", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetHostelStudInfoByIdno() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet RetrieveHostelStudDetails(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                objParams[1] = new SqlParameter("@P_SEARCH", category);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_HOSTEL", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.RetrieveHostelStudDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetHostelFeeRefundInfo(int idno, int sessionno, int OrganizationId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_CANCEL_REFUND_AMOUNT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetHostelFeeRefundInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int ReconcileHostelFee(RoomAllotment roomAllot, int dcr_no, string dd_no, DateTime dd_Date, string dd_bank, string dd_City, int dd_BanckNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", roomAllot.HostelSessionNo);
                sqlParams[1] = new SqlParameter("@P_IDNO", roomAllot.ResidentNo);
                sqlParams[2] = new SqlParameter("@P_DCR_NO", dcr_no);
                sqlParams[3] = new SqlParameter("@P_USER_NO", roomAllot.UserNo);
                sqlParams[4] = new SqlParameter("@P_DD_NO", dd_no);
                if (dd_Date == DateTime.MinValue)
                    sqlParams[5] = new SqlParameter("@P_DD_DT", DBNull.Value);
                else
                    sqlParams[5] = new SqlParameter("@P_DD_DT", dd_Date);
                sqlParams[6] = new SqlParameter("@P_DD_BANK", dd_bank);
                sqlParams[7] = new SqlParameter("@P_DD_CITY", dd_City);
                sqlParams[8] = new SqlParameter("@P_DBANKNO", dd_BanckNo);
                sqlParams[9] = new SqlParameter("@P_COLLEGE_CODE", roomAllot.CollegeCode);
                sqlParams[10] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                sqlParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[11].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_FEE_RECONCILE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.ReconcileHostelFee() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        public DataSet GetStudentsForHostelAndRoomAllot(int hostelSessionNo, StudentSearch objsearch, int roomtype, string Gender)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_HOSTEL_SESSION", hostelSessionNo);
                objParams[1] = new SqlParameter("@P_REGNO", objsearch.RegNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", objsearch.DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", objsearch.BranchNo);
                objParams[4] = new SqlParameter("@P_YEARNO", objsearch.YearNo);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objsearch.SemesterNo);
                objParams[6] = new SqlParameter("@P_ROLLNO", objsearch.DivisionNo);
                objParams[7] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[8] = new SqlParameter("@P_APLLICATIONID", objsearch.ApplicationID);
                objParams[9] = new SqlParameter("@P_ROOMTYPE", roomtype); //pass this parameter to get Roomtype amount as per selection of room by shubham on 03/10/2022
                objParams[10] = new SqlParameter("@GENDER", Gender);  //pass this parameter to get student as per gender by Shubham on 03/10/2022

                ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_SEARCH_STUDENT_FOR_ROOM_ALLOTMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomStatus-> " + ex.ToString());
            }
            finally
            { ds.Dispose(); }

            return ds;
        }

        // added by  sonali on 01/09/2022
        public DataSet GetStudentsForHostelAndRoomAllotCpu(int hostelSessionNo, StudentSearch objsearch, int roomtype, string Gender)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_HOSTEL_SESSION", hostelSessionNo);
                objParams[1] = new SqlParameter("@P_REGNO", objsearch.RegNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", objsearch.DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", objsearch.BranchNo);
                objParams[4] = new SqlParameter("@P_YEARNO", objsearch.YearNo);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objsearch.SemesterNo);
                objParams[6] = new SqlParameter("@P_ROLLNO", objsearch.DivisionNo);
                objParams[7] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[8] = new SqlParameter("@P_APLLICATIONID", objsearch.ApplicationID);
                objParams[9] = new SqlParameter("@P_ROOMTYPE", roomtype);
                objParams[10] = new SqlParameter("@GENDER", Gender);  //pass this parameter to get student as per gender by shubham on 03/10/2022

                ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_SEARCH_STUDENT_FOR_ROOM_ALLOTMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomStatus-> " + ex.ToString());
            }
            finally
            { ds.Dispose(); }

            return ds;
        }
        //public DataSet GetRoomAvailabilityStatus(int hostelno, int blockno)

        public DataSet GetRoomAvailabilityStatus(int hostelno, int Hostel_session, int Degree, int Semester, int blockno)
        {
            DataSet ds = null;
            try
            {
                //SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //SqlParameter[] objParams = null;
                //objParams = new SqlParameter[2];
                //objParams[0] = new SqlParameter("@P_HOSTEL_NO", hostelno);
                //objParams[1] = new SqlParameter("@P_BLOCK_NO", blockno);

                //ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS", objParams);

                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_HOSTEL_NO", hostelno);
                objParams[1] = new SqlParameter("@P_HOSTEL_SESSION", Hostel_session);
                objParams[2] = new SqlParameter("@P_DEGREE", Degree);
                objParams[3] = new SqlParameter("@P_SEMESTER", Semester);
                objParams[4] = new SqlParameter("@P_BLOCK_NO", blockno);

                ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS", objParams);
                //ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS_CHHAGAN", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomAvailabilityStatus-> " + ex.ToString());
            }
            return ds;
        }

        //Below method added by Saurabh L on 09/08/2022 Purpose: for Admin user all rooms (guest) shown
        public DataSet GetRoomAvailabilityStatusAdmin(int hostelno, int Hostel_session, int Degree, int Semester, int blockno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_HOSTEL_NO", hostelno);
                objParams[1] = new SqlParameter("@P_HOSTEL_SESSION", Hostel_session);
                objParams[2] = new SqlParameter("@P_DEGREE", Degree);
                objParams[3] = new SqlParameter("@P_SEMESTER", Semester);
                objParams[4] = new SqlParameter("@P_BLOCK_NO", blockno);

               // ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS", objParams);
                //ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS_CHHAGAN", objParams);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomAvailabilityStatus-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRoomAvailabilityStatusAdminCpuK(int hostelno, int Hostel_session, int Degree, int Semester, int blockno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_HOSTEL_NO", hostelno);
                objParams[1] = new SqlParameter("@P_HOSTEL_SESSION", Hostel_session);
                objParams[2] = new SqlParameter("@P_DEGREE", Degree);
                objParams[3] = new SqlParameter("@P_SEMESTER", Semester);
                objParams[4] = new SqlParameter("@P_BLOCK_NO", blockno);

                // ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS", objParams);
                //ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS_CHHAGAN", objParams);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_ROOMS_AVAILABILITY_STATUS_CPUK", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomAvailabilityStatus-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetTableBackUpDetail(int hostelno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_HOSTEL_NO", hostelno);
                objParams[1] = new SqlParameter("@P_SESSION_NO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("PR_HOSTEL_GET_BACKUP_DETAIL", objParams);


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetRoomStatus-> " + ex.ToString());
            }
            return ds;
        }
        public int ClearRoomAllotment(int hostelno, int sessionno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_HOSTEL_NO", hostelno);
                sqlParams[1] = new SqlParameter("@P_SESSION_NO", sessionno);
                sqlParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PR_HOSTEL_CLEAR_ALLOTMENT", sqlParams, false);
                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                return retStatus;
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                return retStatus;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.DailyAttendanceInsert() --> " + ex.Message + " " + ex.StackTrace);
            }
        }
        public int HostelPreApplyStudentInsert(int hostelno, int sessionno)
        {
            int status = 0;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                           
                           new SqlParameter("@P_HOSTEL_NO", hostelno),
                           new SqlParameter("@P_HOSTEL_SESSIONNO", sessionno),
                           new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                sqlParams[sqlParams.Length - 1].Size = 10000;
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                SqlConnection conn = new SqlConnection(connectionString);
                //conn.ConnectionTimeout = 1000;
                SqlCommand cmd = new SqlCommand("PKG_INSERT_IN_HOSTEL_APPLY_STUDENT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;
                int i;
                for (i = 0; i < sqlParams.Length; i++)
                    cmd.Parameters.Add(sqlParams[i]);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
                status = Convert.ToInt16(cmd.Parameters[2].Value);

            }
            catch (Exception ex)
            {
                // status = "Error";
            }
            return status;
        }
        public int HostelLastSessionAllotementInsert(int hostelno, int sessionno)
        {
            int status = 0;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                           
                           new SqlParameter("@P_HOSTEL_NO", hostelno),
                           new SqlParameter("@P_HOSTEL_SESSIONNO", sessionno),
                           new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                sqlParams[sqlParams.Length - 1].Size = 10000;
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                SqlConnection conn = new SqlConnection(connectionString);
                //conn.ConnectionTimeout = 1000;
                SqlCommand cmd = new SqlCommand("PKG_INSERT_LAST_SESSION_ALLOTEMENT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;
                int i;
                for (i = 0; i < sqlParams.Length; i++)
                    cmd.Parameters.Add(sqlParams[i]);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
                status = Convert.ToInt16(cmd.Parameters[2].Value);

            }
            catch (Exception ex)
            {
                // status = "Error";
            }
            return status;
        }
        public int HostelAttendanceInsert(string hostelSession, string hostelNo, string roomno, string ua_no, string studId, string att_status, string remark, int col_code, string ipaddress)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@P_HOSTEL_SESSIONNO", hostelSession);
                sqlParams[1] = new SqlParameter("@P_HOSTELNO", hostelNo);
                sqlParams[2] = new SqlParameter("@P_ROOMNO", roomno);
                sqlParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                sqlParams[4] = new SqlParameter("@P_STUDID", studId);
                sqlParams[5] = new SqlParameter("@P_ATT_STATUS", att_status);
                sqlParams[6] = new SqlParameter("@P_REASON", remark);
                sqlParams[7] = new SqlParameter("@P_COLLEGE_CODE", col_code);
                sqlParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);
                sqlParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ONLINE_STUDENT_ATTENDANCE_INSERT", sqlParams, false);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.DailyAttendanceInsert() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

    }
}