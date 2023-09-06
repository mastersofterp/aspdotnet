
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : BranchNameController                      
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : Pranita 
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG
{
    public class BranchNameController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        /// <summary>
        /// ADDED ON 04-10-2021 BY Pranita to Get Branch Master Data.
        /// </summary>
        /// <returns></returns>
        public DataSet GetBranchMasterData(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@BRANCHNO ", id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CONFIG_GET_BRANCH_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchNameController.GetBranchMasterData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        ///// <summary>
        /////  ADDED ON 04-10-2021 BY Pranita to Get Branch Master Data by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public DataSet GetBranchMasterDataById(int id)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@BRANCHNO", id);
        //        ds = objSQLHelper.ExecuteDataSetSP("sptblAcdBranchMaster_Get", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchNameController.GetBranchMasterDataById() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        /// <summary>
        ///  MODIFIED By Rishabh on 06/12/2021 to add new column -Organization Id
        /// </summary>
        /// <param name="ObjBranch"></param>
        /// <returns></returns>
        /// <summary>
        ///  MODIFIED By Rishabh on 09/03/2022- for OrgId
        /// </summary>
        public int SaveBranchMasterData(Branch ObjBranch)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[13];

                sqlParams[0] = new SqlParameter("@BRANCHNO", ObjBranch.BranchNo);
                sqlParams[1] = new SqlParameter("@LONGNAME", ObjBranch.LongName);
                sqlParams[2] = new SqlParameter("@SHORTNAME", ObjBranch.ShortName);
                sqlParams[3] = new SqlParameter("@BRANCHNAME_ORIGNAL", ObjBranch.Branchname_Origral);
                sqlParams[4] = new SqlParameter("@KPNO", ObjBranch.KpNo);
                sqlParams[5] = new SqlParameter("@ISCORE", Convert.ToInt32(ObjBranch.Iscore));
                sqlParams[6] = new SqlParameter("@CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[7] = new SqlParameter("@MODIFIEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[8] = new SqlParameter("@IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[9] = new SqlParameter("@ACTIVESTATUS", ObjBranch.IsActive);
                sqlParams[10] = new SqlParameter("@MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[11] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 07/12/2021
                sqlParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONFIG_BRANCH_MASTER_INSERT_UPDATE", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchNameController.SaveBranchMasterData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}