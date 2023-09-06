//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ASSET MASTER CONTROLLER
// CREATION DATE : 12-AUG-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class AssetController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddAsset(char isavail, int assetTypeNo, string assetName, int assQuan, string collegeCode)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo),
                    new SqlParameter("@P_ASSET_NAME", assetName),
                    new SqlParameter("@P_ASSET_QUANTITY", assQuan),
                    new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                     new SqlParameter("@P_ISAVAIL", isavail),
                     new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_ASSET_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ASSET_INSERT", sqlParams, true);


                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.AddAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateAsset(char isavail, int assetNo, int assetTypeNo, string assetName, int assQuan)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_ASSET_NAME", assetName),
                    new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo),
                    new SqlParameter("@P_ASSET_QUANTITY", assQuan),
                     new SqlParameter("@P_ISAVAIL", isavail),
                     new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_ASSET_NO", assetNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_ASSET_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.UpdateAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllAssets()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_GET_ALL", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.GetAllAssets() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAssetByNo(int assetNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ASSET_NO", assetNo),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.GetAssetByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAssetTypeByNo(int assetTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_TYPE_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.GetAssetByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddUpdateAssetType(int assetTypeNo, string assetName, string assCode, string collegeCode)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ASSET_TYPE_NO", assetTypeNo),
                    new SqlParameter("@P_ASSET_NAME", assetName),
                    new SqlParameter("@P_ASSET_CODE", assCode),
                    new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_STATUS", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_UPDATE_ASSET_TYPE", sqlParams, true);


                if (Convert.ToInt32(obj) == 1)
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(obj) == 2)
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.AddAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllAssetsType()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                {
                   new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_ASSET_TYPE_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssetController.GetAllAssets() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}