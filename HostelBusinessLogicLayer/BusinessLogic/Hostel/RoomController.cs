//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ASSET MASTER CONTROLLER
// CREATION DATE : 12-AUG-2009
// CREATED BY    : SANJAY RATNAPARKHI AND AMIT YADAV
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
    public class RoomController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        public int AddRoom(Room room)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_BLOCK_NO", room.BlockNo),
                    new SqlParameter("@P_FLOOR_NO", room.FloorNo),//added for Floor waise Data to saved By shubham B on 03/06
                    //new SqlParameter("@P_BRANCHNO", room.BranchNo),
                    //new SqlParameter("@P_SEMESTERNO", room.SemesterNo),
                    new SqlParameter("@P_ROOM_NAME", room.RoomName),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", room.ResidentTypeNo),
                    new SqlParameter("@P_CAPACITY", room.Capacity),
                    new SqlParameter("@P_HOSTEL_NO",room.HostelNo),
                    new SqlParameter("@P_COLLEGE_CODE", room.CollegeCode),
                    new SqlParameter("@P_TYPE_NO", room.Roomtype),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    
                    new SqlParameter("@P_ROOM_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.AddRoom() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateRoom(Room room)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_BLOCK_NO", room.BlockNo),
                    new SqlParameter("@P_FLOOR_NO", room.FloorNo),
                    new SqlParameter("@P_BRANCHNO", room.BranchNo),
                    new SqlParameter("@P_SEMESTERNO", room.SemesterNo),
                    new SqlParameter("@P_ROOM_NAME", room.RoomName),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", room.ResidentTypeNo),
                    new SqlParameter("@P_CAPACITY", room.Capacity),
                    new SqlParameter("@P_COLLEGE_CODE", room.CollegeCode),
                    new SqlParameter("@P_TYPE_NO", room.Roomtype),
                    new SqlParameter("@P_HOSTEL_NO",room.HostelNo),  //Added By Himanshu Tamrakar 10-04-2024
                    new SqlParameter("@P_ROOM_NO", room.RoomNo)
                   
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ROOM_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.UpdateAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllRooms() 
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 

                   // new SqlParameter("@P_HOSTEL_NO", hbno),
                   //// new SqlParameter("@P_HBNO", hbno),
                   // new SqlParameter("@P_BLOCKNO", blockno) 
                   new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))  //-OrganizationId  added by Saurabh L on 23/05/2022
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_GET_ALL", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.GetAllRooms() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetRoomByNo(int roomNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ROOM_NO", roomNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ROOM_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.GetRoomByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}