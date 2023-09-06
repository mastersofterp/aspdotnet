
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : InstituteType Controller                  
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
    public class InstituteTypeController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        /// <summary>
        /// ADDED ON 07-10-2021 BY S.Patil to Get InstituteType Data
        /// </summary>
        /// <returns></returns>
        //public DataSet InstituteTypeData()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@InstitutionTypeId", 0);
        //        ds = objSQLHelper.ExecuteDataSetSP("sptblconfiginstitutiontypemaster_Get", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstituteTypeController.InstituteTypeData() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        /// <summary>
        /// ADDED ON 07-10-2021 BY S.Patil to Get InstituteType by id.
        /// </summary>
        /// <returns></returns>
        public DataSet GetInstituteTypeDataById(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@InstitutionTypeId", id);
                ds = objSQLHelper.ExecuteDataSetSP("sptblconfiginstitutiontypemaster_Get", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstituteTypeController.GetById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// ADDED ON 07-10-2021 BY S.Patil to Insert and Update InstituteType 
        /// </summary>
        /// <returns></returns>
        public int SaveInstituteTypeData(InstituteType ObjInstitute)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[9];

                sqlParams[0] = new SqlParameter("@InstitutionTypeId", ObjInstitute.InstituteTypeNo);
                sqlParams[1] = new SqlParameter("@InstitutionTypeName", ObjInstitute.InstituteTypeName);
                sqlParams[2] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[3] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[4] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[5] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                sqlParams[6] = new SqlParameter("@ActiveStatus", ObjInstitute.IsActive);
                sqlParams[7] = new SqlParameter("@MacAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("sptblconfiginstitutiontypemaster_Insert_Update", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstituteTypeController.SaveInstituteTypeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}
