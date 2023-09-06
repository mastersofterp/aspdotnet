
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Organization Controller               
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
    public class OrganizationController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        ///// <summary>
        /////  ADDED ON 07-10-2021 BY S.Patil to get org
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetOrganization()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@OrganizationId", 0);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ConfigOrganizationMaster_Get_Data_by_id", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OrganizationController.GetOrganization() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        /// <summary>
        ///  ADDED ON 07-10-2021 BY S.Patil to to get org
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetOrganizationById(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@OrganizationId", id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ConfigOrganizationMaster_Get_Data_by_id", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OrganizationController.GetOrganizationById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetDataToFillDropDownlist(int orgid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@OrganizationId", orgid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CONFIG_FILL_DROPDOWNLIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OrganizationController.GetOrganizationById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Updated by Rishbah - 11/10/2021
        /// </summary>
        /// <param name="objOrg"></param>
        /// <returns></returns>
        public int SaveUpdOrganizationDetails(Organization objOrg, int hdf)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[22];

                sqlParams[0] = new SqlParameter("@OrganizationId", objOrg.OrganizationId);
                sqlParams[1] = new SqlParameter("@OrgName", objOrg.Name);
                sqlParams[2] = new SqlParameter("@Address", objOrg.Address);
                sqlParams[3] = new SqlParameter("@Email", objOrg.Email);
                sqlParams[4] = new SqlParameter("@Website", objOrg.Website);
                sqlParams[5] = new SqlParameter("@ContactName", objOrg.ContactName);
                sqlParams[6] = new SqlParameter("@ContactNo", objOrg.ContactNo);
                sqlParams[7] = new SqlParameter("@ContactDesignation", objOrg.ContactDesignation);

                sqlParams[8] = new SqlParameter("@ContactEmail", objOrg.ContactEmail);
                sqlParams[9] = new SqlParameter("@EstabishmentDate", objOrg.EstabishmentDate);
                sqlParams[10] = new SqlParameter("@InstitutionTypeId", objOrg.InstitutionTypeId);
                sqlParams[11] = new SqlParameter("@OwnershipStatusId", objOrg.OwnershipStatusId);
                sqlParams[12] = new SqlParameter("@MISOrderDate", objOrg.MISOrderDate);

                sqlParams[13] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[14] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[15] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());

                sqlParams[16] = new SqlParameter("@ActiveStatus", objOrg.ActiveStatus);
                sqlParams[17] = new SqlParameter("@MacAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                //Added by Rishabh 11-10-2021
                if (objOrg.Logo == null)
                    sqlParams[18] = new SqlParameter("@P_ORG_LOGO", DBNull.Value);
                else
                    sqlParams[18] = new SqlParameter("@P_ORG_LOGO", objOrg.Logo);
                sqlParams[18].SqlDbType = SqlDbType.Image;
                sqlParams[19] = new SqlParameter("@P_HDN", hdf);
                //End
                sqlParams[20] = new SqlParameter("@P_LogoFlag", objOrg.LogoFlag);

                sqlParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[21].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("sptblConfigOrganizationMaster_Insert_Update", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OrganizationController.SaveUpdOrganizationDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        /// Added By S.Patil - 28102021
        /// </summary>
        /// <returns></returns>
        public DataSet GetData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MENU", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OrganizationController.GetData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
