//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : ConfigAffilationTypeController                 
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

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG
{
    public class ConfigAffilationTypeController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        /// <summary>
        /// ADDED ON 04-10-2021 BY S.P to get the details of Affilation Type
        /// </summary>
        /// <returns></returns>
        //public DataSet GetAffilationTypeData()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@AffilationTypeId", 0);
        //        ds = objSQLHelper.ExecuteDataSetSP("sptblConfigAffilationTypeMaster_Get", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigAffilationTypeController.GetAffilationTypeData() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        /// <summary>
        /// ADDED ON 05-10-2021 BY S.P to get datails of Affilation Type by Affilation id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAffilationTypeData(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@AffilationTypeId", id);
                ds = objSQLHelper.ExecuteDataSetSP("sptblConfigAffilationTypeMaster_Get", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigAffilationTypeController.GetAffilationTypeDataById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        ///  ADDED ON 04-10-2021 BY S.P to Insert/Update Affilation Type
        /// </summary>
        /// <param name="ObjAffilation"></param>
        /// <returns></returns>
        public int SaveAffilationTypeData(ConfigAffilationType ObjAffilation)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[9];

                sqlParams[0] = new SqlParameter("@AffilationTypeId", ObjAffilation.AffilationTypeId);
                sqlParams[1] = new SqlParameter("@AffilationName", ObjAffilation.AffilationName);
                sqlParams[2] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[3] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[4] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[5] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                sqlParams[6] = new SqlParameter("@ActiveStatus", ObjAffilation.ActiveStatus);
                sqlParams[7] = new SqlParameter("@MACAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("sptblConfigAffilationTypeMaster_Insert_Update", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigAffilationTypeController.SaveAffilationTypeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}
