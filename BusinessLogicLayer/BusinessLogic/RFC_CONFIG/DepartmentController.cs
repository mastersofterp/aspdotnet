
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Department Controller                       
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : RISHABH
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
    public class DepartmentController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
         /// <summary>
        /// ADDED ON 07-10-2021 BY Rishabh to Get Department Type by id.
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepartmentData(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@DEPTNO", id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CONFIG_GET_DEPARTMENT_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentController.GetDepartmentData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// MODIFIED By Rishabh on 09/03/2022
        /// </summary>
        public int SaveDepartment(Department objDept)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[10];

                sqlParams[0] = new SqlParameter("@DEPTNO", objDept.DepartmentId);
                sqlParams[1] = new SqlParameter("@DEPTNAME", objDept.DepartmentName);
                sqlParams[2] = new SqlParameter("@DEPTCODE", objDept.DepartmentShortName);
                sqlParams[3] = new SqlParameter("@CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[4] = new SqlParameter("@MODIFIEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[5] = new SqlParameter("@IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[6] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 06/12/2021
                sqlParams[7] = new SqlParameter("@ACTIVESTATUS", objDept.ActiveStatus);
                sqlParams[8] = new SqlParameter("@MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONFIG_DEPARTMENT_MASTER_INSERT_UPDATE", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentController.SaveDepartment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}
