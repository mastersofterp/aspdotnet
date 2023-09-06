//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ASSET MASTER CONTROLLER
// CREATION DATE : 14-AUG-2009
// CREATED BY    : SANJAY RATNAPARKHI
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
    public class AssetAllotmentController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddAssetAllotment(AssetAllotment objAsssetAllotment)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ROOM_NO", objAsssetAllotment.RoomNo),
                    new SqlParameter("@P_ASSET_NO", objAsssetAllotment.AssetNo),
                    new SqlParameter("@P_QUANTITY",objAsssetAllotment.Quantity),
                    new SqlParameter("@P_ALLOTMENT_DATE", objAsssetAllotment.AllotmentDate),
                    new SqlParameter("@P_ALLOTMENT_CODE", objAsssetAllotment.AllotmentCode),
                    new SqlParameter("@P_COLLEGE_CODE", objAsssetAllotment.CollegeCode),
                    new SqlParameter("@P_ORGANIZATION_ID", objAsssetAllotment.organizationid),
                    new SqlParameter("@P_ASSET_ALLOTMENT_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ASSET_ALLOTMENT_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(obj) == 99)                     //Added by himanshu tamrakar 07-08-2023
                    status = Convert.ToInt32(CustomStatus.Others);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetAllotmentController.AddAssetAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateAssetAllotment(AssetAllotment objAsssetAllotment)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ROOM_NO", objAsssetAllotment.RoomNo),
                    new SqlParameter("@P_ASSET_NO", objAsssetAllotment.AssetNo),
                    new SqlParameter("@P_QUANTITY",objAsssetAllotment.Quantity),
                    new SqlParameter("@P_ALLOTMENT_DATE",objAsssetAllotment.AllotmentDate),
                    new SqlParameter("@P_ALLOTMENT_CODE", objAsssetAllotment.AllotmentCode),
                    new SqlParameter("@P_ORGANIZATION_ID", objAsssetAllotment.organizationid), //organizationid added by Saurabh L on 15/02/2023
                    new SqlParameter("@P_ASSET_ALLOTMENT_NO",objAsssetAllotment.AssetAllotmentNo)                   
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ASSET_ALLOTMENT_UPDATE", sqlParams, true);


                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(obj) == 99)
                    status = Convert.ToInt32(CustomStatus.Others);  //Added for bug_id:164583
                else
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetAllotmentController.UpdateAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllAssetAllotment()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_ALLOTMENT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetAllotmentController.GetAllAssetAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAssetAllotmentByNo(int assetAllotmentNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ASSET_ALLOTMENT_NO", assetAllotmentNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_ALLOTMENT_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetAllotmentController.GetRoomByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
