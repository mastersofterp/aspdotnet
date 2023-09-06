//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : ConfigRefController              
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : RISHABH
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG
{
    public class ConfigRefController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// ADDED ON 04-11-2021 BY Rishabh to save the details of Config Reference.
        /// Modified By Rishabh on 07/12/2021 to add new column default page.
        /// </summary>
        /// <returns></returns>
        public int SaveConfigRefDetails(ConfigRefDetails objConfig)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@ReferenceDetailsId", objConfig.ReferenceDetailsId);
                objParams[1] = new SqlParameter("@OrganizationId", objConfig.OrganizationId);
                objParams[2] = new SqlParameter("@ProjectName", objConfig.ProjectName);
                objParams[3] = new SqlParameter("@ServerName", objConfig.ServerName);
                objParams[4] = new SqlParameter("@UserId", objConfig.UserId);
                objParams[5] = new SqlParameter("@Password", objConfig.Password);
                objParams[6] = new SqlParameter("@DBName", objConfig.DBName);
                objParams[7] = new SqlParameter("@OrganizationUrl", objConfig.OrganizationUrl); //Added By Rishabh on 04/12/2021
                objParams[8] = new SqlParameter("@DefaultPage", objConfig.DefaultPage); //Added By Rishabh on 07/12/2021
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("sptblConfigRefereneceDetails_Insert_Update", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.ConfigRefController.SaveConfigRefDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// ADDED ON 20-10-2021 BY Rishabh to get the details of Config Reference Details.
        /// </summary>
        /// <returns></returns>
        public DataSet GetConfigDetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_CONFIGREF_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RFC_CONFIG.ConfigRefController.GetConfigDetails-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ADDED ON 20-10-2021 BY Rishabh to get the details of Config Reference Details by ReferenceDetailsId.
        /// </summary>
        /// <returns></returns>
        public DataSet GetConfigDetailsById(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@ReferenceDetailsId", id);
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_CONFIGREF_DETAILS_BY_ID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RFC_CONFIG.ConfigRefController.GetConfigDetailsById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
