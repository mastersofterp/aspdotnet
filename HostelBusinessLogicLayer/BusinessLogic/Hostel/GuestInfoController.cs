//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : GUEST INFO CONTROLLER                                                
// CREATION DATE : 18-AUG-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using HostelBusinessLogicLayer.BusinessEntities.Hostel;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class GuestInfoController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddGuestInfo(GuestInfo objGuestInfo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RESIDENT_TYPE_NO", objGuestInfo.ResidentTypeNo),
                    new SqlParameter("@P_GUEST_NAME", objGuestInfo.GuestName),
                    new SqlParameter("@P_GUEST_ADDRESS", objGuestInfo.GuestAddress),
                    new SqlParameter("@P_CONTACT_NO", objGuestInfo.ContactNo),
                    new SqlParameter("@P_PURPOSE", objGuestInfo.Purpose),
                    new SqlParameter("@P_COMPANY_NAME", objGuestInfo.CompanyName),
                    new SqlParameter("@P_COMPANY_ADDRESS", objGuestInfo.CompanyAddress),
                    new SqlParameter("@P_COMPANY_CONTACT_NO", objGuestInfo.CompanyContactNo),
                    new SqlParameter("@P_FROM_DATE", objGuestInfo.FromDate),
                    new SqlParameter("@P_TO_DATE", objGuestInfo.ToDate),
                    new SqlParameter("@P_COLLEGE_CODE", objGuestInfo.CollegeCode),
                    new SqlParameter("@P_ORGANIZATION_ID", objGuestInfo.OrganizationId),
                    new SqlParameter("@P_GUEST_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_GUEST_INFO_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.AddGuestInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateGuestInfo(GuestInfo objGuestInfo)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RESIDENT_TYPE_NO", objGuestInfo.ResidentTypeNo),
                    new SqlParameter("@P_GUEST_NAME", objGuestInfo.GuestName),
                    new SqlParameter("@P_GUEST_ADDRESS", objGuestInfo.GuestAddress),
                    new SqlParameter("@P_CONTACT_NO", objGuestInfo.ContactNo),
                    new SqlParameter("@P_PURPOSE", objGuestInfo.Purpose),
                    new SqlParameter("@P_COMPANY_NAME", objGuestInfo.CompanyName),
                    new SqlParameter("@P_COMPANY_ADDRESS", objGuestInfo.CompanyAddress),
                    new SqlParameter("@P_COMPANY_CONTACT_NO", objGuestInfo.CompanyContactNo),
                    new SqlParameter("@P_FROM_DATE", objGuestInfo.FromDate),
                    new SqlParameter("@P_TO_DATE", objGuestInfo.ToDate),
                    new SqlParameter("@P_COLLEGE_CODE", objGuestInfo.CollegeCode),
                    new SqlParameter("@P_ORGANIZATION_ID", objGuestInfo.OrganizationId),
                    new SqlParameter("@P_GUEST_NO", objGuestInfo.GuestNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_GUEST_INFO_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.UpdateGuestInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllGuestInfo()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GUEST_INFO_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.GetAllGuestInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetGuestInfoByNo(int guestNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                    new SqlParameter("@P_GUEST_NO", guestNo),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GUEST_INFO_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.GetGuestInfoByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Staff Information

        public int AddStaffInfo(GuestInfo objGuestInfo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", objGuestInfo.Sessionno),
                    new SqlParameter("@P_HOSTELNO", objGuestInfo.Hostelno),
                    new SqlParameter("@P_EMP_NAME", objGuestInfo.GuestName),
                    new SqlParameter("@P_DESIGNATION", objGuestInfo.Purpose),
                    new SqlParameter("@P_CONTACTNO", objGuestInfo.ContactNo),
                    new SqlParameter("@P_EMAILID", objGuestInfo.EmailId),
                    new SqlParameter("@P_EMP_ADDRESS",objGuestInfo.GuestAddress),
                    new SqlParameter("@P_UA_NO", objGuestInfo.Ua_no),
                    new SqlParameter("@P_ORGANIZATION_ID", objGuestInfo.OrganizationId),
                    new SqlParameter("@P_COLLEGE_CODE",objGuestInfo.CollegeCode),
                    new SqlParameter("@P_OUT",status),
                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STAFF_INFO_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
              
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.AddStaffInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateStaffInfo(GuestInfo objGuestInfo,int stno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_STNO",stno),
                    new SqlParameter("@P_SESSIONNO", objGuestInfo.Sessionno),
                    new SqlParameter("@P_HOSTELNO", objGuestInfo.Hostelno),
                    new SqlParameter("@P_EMP_NAME", objGuestInfo.GuestName),
                    new SqlParameter("@P_DESIGNATION", objGuestInfo.Purpose),
                    new SqlParameter("@P_CONTACTNO", objGuestInfo.ContactNo),
                    new SqlParameter("@P_EMAILID", objGuestInfo.EmailId),
                    new SqlParameter("@P_EMP_ADDRESS",objGuestInfo.GuestAddress),
                    new SqlParameter("@P_UA_NO", objGuestInfo.Ua_no),
                    new SqlParameter("@P_ORGANIZATION_ID", objGuestInfo.OrganizationId),
                    new SqlParameter("@P_COLLEGE_CODE",objGuestInfo.CollegeCode),
                    new SqlParameter("@P_OUT",status),
                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STAFF_INFO_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.UpdateStaffInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        // Hostel room Charges added by sonali on date 18-01-2023

        public int AddUpdateRoomCharge(HostelRoomCharge objroomcharge)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_CHARGE_NO", objroomcharge.CHARGE_NO),
                    new SqlParameter("@P_SESSIONNO", objroomcharge.SESSIONNO),
                    new SqlParameter("@P_HOSTEL_NO", objroomcharge.HOSTEL_NO),
                    new SqlParameter("@P_ROOM_TYPE", objroomcharge.ROOM_TYPE),
                    new SqlParameter("@P_RESIDENT_TYPE", objroomcharge.RESIDENT_TYPE),
                    new SqlParameter("@P_CHARGES", objroomcharge.CHARGES),
                    new SqlParameter("@P_COLLEGE_CODE",objroomcharge.COLLEGE_CODE),
                    new SqlParameter("@P_ORGANIZATIONID", objroomcharge.ORGANIZATIONID),
                    new SqlParameter("@P_USERNO", objroomcharge.USERNO),
                    new SqlParameter("@P_IPADDRESS",objroomcharge.IPADDRESS),
                    new SqlParameter("@P_OUT",status),
     
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_UPDATE_ROOM_CHARGE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.AddStaffInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetChargeData(int chargeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                    new SqlParameter("@P_CHARGE_NO", chargeNo),
                    new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_ALL_ROOM_CHARGE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.GetGuestInfoByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // Student day wise Room Allotment methods added on 20-01-2023 by sonali 
        public DataSet RetrieveStudentDetailsNew(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                objParams[1] = new SqlParameter("@P_SEARCH", category);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SP_SEARCH_STUDENT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentInfoById(int studentId, int OrgId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_ORGANIZATION_ID",OrgId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_HOSTEL_FEECOLLECT_GET_STUDENT_INFO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.BusinessLogic.HostelFeeCollectionController.GetStudentInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddUpdateStudDayWiseRoomAllot(RoomAllotment objroom, DateTime fromdate, DateTime todate, Decimal demandcharges, int hostelno, int userno, string ipaddress, int roomallotmentno, int staydays, int roomtype)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ROOM_ALLOTMENT_NO", roomallotmentno),
                    new SqlParameter("@P_HOSTEL_SESSION_NO", objroom.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO", objroom.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", objroom.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO", objroom.ResidentNo),
                    new SqlParameter("@P_ALLOTMENT_DATE", objroom.AllotmentDate),
                    new SqlParameter("@P_HOSTEL_NO", hostelno),
                    new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_FROMDATE", fromdate),
                    new SqlParameter("@P_TODATE", todate),
                    new SqlParameter("@P_CHARGES", demandcharges),
                    new SqlParameter("@P_USERNO",userno ),
                    new SqlParameter("@P_IPADDRESS", ipaddress), 
                    new SqlParameter("@P_STAYDAYS", staydays),   
                    new SqlParameter("@P_ROOM_TYPE", roomtype), 
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STUDENT_DAYWISE_ROOM_ALLOTMENT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.AddGuestInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetStudentRoomAllot(int student, int session)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objsql = new SQLHelper(connectionString);
                SqlParameter[] objparams = new SqlParameter[3];
                {
                    objparams[0] = new SqlParameter("@P_IDNO", student);
                    objparams[1] = new SqlParameter("@P_SESSIONNO", session);
                    objparams[2] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                    ds = objsql.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_DAYWISE_ROOM_ALLOTMENT_SELECT", objparams);
                }


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetGuestRoomAllot() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudInfoByNo(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                    new SqlParameter("@P_IDNO", idno),
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_DAYWISE_STUDENT_INFO_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.GetGuestInfoByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAvailableRooms(Room roomSearchCriteria, int roomtype)
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
                    new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_ROOMTYPE", roomtype)
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_STUD_DAYWISE_ROOM_ALLOT_GET_AVAILABLE_ROOMS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetAvailableRooms() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        #region disciplinary actions added by sonali on 24/02/2023
        // insert update disciplinary actions added by sonali on 24/02/2023

        public int AddUpdateStudDisciplinaryAction(int disci_id,int idno, string rollno, int curr_hostel, int curr_room, int hostelSession, DateTime fromdate, DateTime todate, string remark, int userno, string ipaddress, int college, int orgid)
        {          
              
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_DISCIP_ID", disci_id),
                    new SqlParameter("@P_RESIDENT_NO", idno),
                    new SqlParameter("@P_ROLLNO", rollno),
                    new SqlParameter("@P_CURR_HOSTEL", curr_hostel),
                    new SqlParameter("@P_CURR_ROOM", curr_room),
                    new SqlParameter("@P_CURR_SESSION", hostelSession),
                    new SqlParameter("@P_FROMDATE", fromdate),
                    new SqlParameter("@P_TODATE", todate),
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_USERNO", userno),
                    new SqlParameter("@P_IPADDRESS", ipaddress),
                    new SqlParameter("@P_COLLEGE_CODE", college),
                    new SqlParameter("@P_ORGID",orgid),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_UPDATE_STUDENT_DESCIPLINARY_ACTIONS", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestInfoController.AddGuestInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet Getdisciplinarydata(int idno,int disci_id,string command,int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objsql = new SQLHelper(connectionString);
                SqlParameter[] objparams = new SqlParameter[5];
                {
                    objparams[0] = new SqlParameter("@P_IDNO", idno);
                    objparams[1] = new SqlParameter("@P_DISCI_ID", disci_id);
                    objparams[2] = new SqlParameter("@P_COMMANDTYPE", command);
                    objparams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objparams[4] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                    ds = objsql.ExecuteDataSetSP("PKG_HOSTEL_GET_DISCIPLINARY_STUDENT", objparams);
                }


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAllotmentController.GetGuestRoomAllot() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int DeleteDiscipliStudentDetail(int disciplinary_id, int orgid)
        {
            int restatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@P_DISCI_ID",disciplinary_id),
                                new SqlParameter("@P_ORGID",orgid),
                                new SqlParameter("@P_OUT",restatus),
                            };
                param[param.Length - 1].Direction = ParameterDirection.InputOutput;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_DELETE_HOSTEL_DISCIPLINARY_STUDENT", param, true);
                restatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                restatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BUSSINESSLAYER.BUSINESSLOGIC.CREATEHOSTELCONTROLLER->" + ex.ToString());
            }
            return restatus;
        }


        #endregion
    }
}
