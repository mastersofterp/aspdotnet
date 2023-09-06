using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class RoomAssetController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddRoomAsset(RoomAsset objRoomAsset)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Room Asset
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ASSET_NAME", objRoomAsset.AssetName);
                        objParams[1] = new SqlParameter("@P_ROOMNO", objRoomAsset.RoomNo);
                        objParams[2] = new SqlParameter("@P_HOSTELNO ", objRoomAsset.HostelNo);
                        objParams[3] = new SqlParameter("@P_HBLOCKNO", objRoomAsset.BlockNo);
                        objParams[4] = new SqlParameter("@P_QUANTITY", objRoomAsset.Quantity);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objRoomAsset.CollegeCode);
                        objParams[6] = new SqlParameter("@P_ASSETNO", objRoomAsset.AssetNo);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_SP_INS_HOSTEL_ASSET", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAssetController.AddRoom-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateRoomAsset(RoomAsset objRoomAsset)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Room Asset
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ASSET_NAME", objRoomAsset.AssetName);
                        objParams[1] = new SqlParameter("@P_ROOMNO", objRoomAsset.RoomNo);
                        objParams[2] = new SqlParameter("@P_HOSTELNO ", objRoomAsset.HostelNo);
                        objParams[3] = new SqlParameter("@P_HBLOCKNO", objRoomAsset.BlockNo);
                        objParams[4] = new SqlParameter("@P_QUANTITY", objRoomAsset.Quantity);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objRoomAsset.CollegeCode);
                        objParams[6] = new SqlParameter("@P_ASSETNO", objRoomAsset.AssetNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_SP_UPD_HOSTEL_ASSET", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAssetController.AddRoom-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllRoomAsset()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_SP_ALL_HOSTEL_ASSET", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAssetController.GetAllRoomAsset-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetRoomAsset(int assetno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ASSETNO", assetno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_SP_SINGLE_HOSTEL_ASSET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAssetController.GetRoomAsset-> " + ex.ToString());
                    }
                    return dr;
                }

                // methods for Identity card report added by sonali 28/06/2022

                public DataSet GetStudentSearchForHostelIdentityCard(int hostelSessionNo, int hostelNo, int blockNo, int floorNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo);
                        objParams[1] = new SqlParameter("@P_HOSTEL_NO", hostelNo);
                        objParams[2] = new SqlParameter("@P_BLOCK_NO", blockNo);
                        objParams[3] = new SqlParameter("@P_FLOOR_NO", floorNo);
                        objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SEARCH_IDENTITY_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetSTudentSearchForHostelIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                // method for inserting data of Identity card report added by sonali 08/02/2023

                public int AddUpdateIdCardStudData(int idno, int hostelno, int sessionno, string username, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                    
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_HOSTELNO", hostelno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO ", sessionno);
                        objParams[3] = new SqlParameter("@P_USERNAME", username);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[5] = new SqlParameter("@P_OUT",SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_UPDATE_IDENTITY_CARD_LOG", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomAssetController.AddRoom-> " + ex.ToString());
                    }

                    return retStatus;
                }
            }
        }
    }
}
