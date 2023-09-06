
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : OwnershipStatus Controller                  
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : S.Patil
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic.RFC_CONFIG
        {
            public class OwnershipStatusController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                ///// <summary>
                ///// ADDED ON 07-10-2021 BY S.Patil to Get OwnershipStatus Data 
                ///// </summary>
                ///// <returns></returns>
                //public DataSet GetOwnershipStatusData()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@OwnershipStatusId", 0);
                //        ds = objSQLHelper.ExecuteDataSetSP("sptblConfigOwnershipStatusMaster_Get", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OwnershipStatusController.GetAllQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ds;
                //}
                /// <summary>
                /// ADDED ON 07-10-2021 BY S.Patil to Get OwnershipStatus Data ById
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                public DataSet GetOwnershipStatusDataById(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@OwnershipStatusId", id);
                        ds = objSQLHelper.ExecuteDataSetSP("sptblConfigOwnershipStatusMaster_Get", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OwnershipStatusController.GetOwnershipStatusDataById() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                /// <summary>
                /// ADDED ON 07-10-2021 BY S.Patil to  SaveOwnershipStatusData
                /// </summary>
                /// <param name="ObjOwnership"></param>
                /// <returns></returns>
                public int SaveOwnershipStatusData(OwnershipStatus ObjOwnership)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = null;
                        sqlParams = new SqlParameter[9];

                        sqlParams[0] = new SqlParameter("@OwnershipStatusId", ObjOwnership.OwnershipStatusId);
                        sqlParams[1] = new SqlParameter("@OWNERSHIPSTATUSNAME", ObjOwnership.OwnershipStatusName);
                        sqlParams[2] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        sqlParams[3] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        sqlParams[4] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        sqlParams[5] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        sqlParams[6] = new SqlParameter("@ActiveStatus", ObjOwnership.IsActive);
                        sqlParams[7] = new SqlParameter("@MacAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                        sqlParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        sqlParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONFIG_OWNERSHIPSTATUSMASTER_INSERT_UPDATE", sqlParams, true);
                        status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OwnershipStatusController.SaveOwnershipStatusData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }
            }
        }
    }
}
