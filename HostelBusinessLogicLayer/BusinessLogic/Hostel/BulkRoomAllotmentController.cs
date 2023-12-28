using System;
using System.Data;
using System.Data.SqlClient;
//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ALL MODULES                                                     
// PAGE NAME     : BULK ROOM ALLOTMENT                                              
// CREATION DATE : 04-oct-2023                                                   
// CREATED BY    : HIMANSHU TAMRAKAR                                                     
// MODIFIED BY   :                                                                 
// MODIFIED DESC :                                                                 
//=================================================================================

using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using HostelBusinessLogicLayer.BusinessLogic;
using HostelBusinessLogicLayer.BusinessEntities.Hostel;
namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{

    public class BulkRoomAllotmentController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        public DataSet GetStudentforRoomAllotment(int session_no, int Degree_no, int Branch_no, int Semester_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@HOSTEL_SESSION", session_no);
                objParams[1] = new SqlParameter("@DEGREE_NO", Degree_no);
                objParams[2] = new SqlParameter("@BRANCH_NO", Branch_no);
                objParams[3] = new SqlParameter("@SEMESTER_NO", Semester_no);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_FOR_BULK_ROOM_ALLOTMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BulkRoomAllotmentController.GetStudentforRoomAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AllotRoomForBulkStudent(BulkRoomAllotment ObjBA)
        {
            int ret = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_HOSTEL_SESSION_NO",ObjBA.HostelSessionNo),
                    new SqlParameter("@P_ROOM_NO",ObjBA.RoomNo),
                    new SqlParameter("@P_RESIDENT_TYPE_NO",ObjBA.ResidentTypeNo),
                    new SqlParameter("@P_RESIDENT_NO",ObjBA.ResidentNo),
                    new SqlParameter("@P_HOSTEL_NO",ObjBA.HostelNo),
                    new SqlParameter("@P_COLLEGE_CODE",ObjBA.CollegeCode),
                    new SqlParameter("@P_SEM",ObjBA.Sem),
                    new SqlParameter("@P_REGNO",ObjBA.Regno),
                    new SqlParameter("@P_ORGID",ObjBA.OrgID),
                    new SqlParameter("@P_UA_NO",ObjBA.UserNo),
                    new SqlParameter("@P_OUT", ret),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_BULK_ROOM_ALLOTMENT_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    ret = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    ret = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BulkRoomAllotmentController.AllotRoomForBulkStudent() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public DataSet GetAllStudents(int Degreeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_DEGREENO", Degreeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_BULK_ROOM_ALLOTMENT_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelPurposeController.GetAllPurpose() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddBulkRoomAllotmentExcelUpload(BulkRoomAllotment ObjBA, int SrNo, string HOSTEL_SESSION_NAME, string Student_REGNO, string Student_Name, string Semester, string Hostel_Name, string Block, string Floor, string Room_Name, int Room_Capacity, string Resident_Type, int CAN, DateTime? Room_Allotment_Date, int HOSTEL_SESSION_NO, int IDNO, int SEMESTER_NO, int HOSTEL_NO, int BLOCK_NO, int FLOOR_NO, int ROOM_NO, int RESIDENT_NO, int CollegeCode, int OrgID, int UserNo)
        {
            int ret = 0;
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[25];
                objParams[0] = new SqlParameter("@P_SrNo", SrNo);
                objParams[1] = new SqlParameter("@P_HOSTEL_SESSION_NAME", HOSTEL_SESSION_NAME);
                objParams[2] = new SqlParameter("@P_Student_REGNO", Student_REGNO);
                objParams[3] = new SqlParameter("@P_Student_Name", Student_Name);
                objParams[4] = new SqlParameter("@P_Semester", Semester);
                objParams[5] = new SqlParameter("@P_Hostel_Name", Hostel_Name);
                objParams[6] = new SqlParameter("@P_Block", Block);
                objParams[7] = new SqlParameter("@P_Floor", Floor);
                objParams[8] = new SqlParameter("@P_Room_Name", Room_Name);
                objParams[9] = new SqlParameter("@P_Room_Capacity", Room_Capacity);
                objParams[10] = new SqlParameter("@P_Resident_Type", Resident_Type);
                objParams[11] = new SqlParameter("@P_CAN", CAN);
                objParams[12] = new SqlParameter("@P_Room_Allotment_Date", Room_Allotment_Date ?? (object)DBNull.Value);
                objParams[13] = new SqlParameter("@P_Cancel_Date", ObjBA.Cancel_date ?? (object)DBNull.Value);
                objParams[14] = new SqlParameter("@P_HOSTEL_SESSION_NO", HOSTEL_SESSION_NO);
                objParams[15] = new SqlParameter("@P_IDNO", IDNO);
                objParams[16] = new SqlParameter("@P_SEMESTER_NO", SEMESTER_NO);
                objParams[17] = new SqlParameter("@P_HOSTEL_NO", HOSTEL_NO);
                objParams[18] = new SqlParameter("@P_BLOCK_NO", BLOCK_NO);
                objParams[19] = new SqlParameter("@P_FLOOR_NO", FLOOR_NO);
                objParams[20] = new SqlParameter("@P_ROOM_NO", ROOM_NO);
                objParams[21] = new SqlParameter("@P_RESIDENT_NO", RESIDENT_NO);
                objParams[22] = new SqlParameter("@P_CollegeCode", CollegeCode);
                objParams[23] = new SqlParameter("@P_OrgID", OrgID);
                objParams[24] = new SqlParameter("@P_UserNo", UserNo);

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_BULK_ROOM_ALLOTMENT_EXCEL_UPLOAD", objParams, true);
                return ret;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelPurposeController.GetAllPurpose() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public int AddStudentRoomAllotmentDataFromExcel(int OrgID)
        {
            int ret = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ORGID", OrgID),
                    new SqlParameter("@P_OUTPUT", ret),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_BULK_ROOM_ALLOTMENT_FROM_MIGRATION_DATA", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    ret = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    ret = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelPurposeController.GetAllPurpose() --> " + ex.Message + " " + ex.StackTrace);
            }

            return ret;
        }
        public DataSet GetAllStudentsDataFromMigrationTable()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_BULK_ROOM_GET_MIGRATION_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelPurposeController.GetAllPurpose() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }

}
