//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Degree Controller         
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : RISHABH
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG
{
    public class DegreeController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// MODIFIED By Sakshi M. on 18/10/2023 - for Active Status
        /// </summary>
        public int SaveDegreeTypeData(Degree ObjDegree)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[9];

                sqlParams[0] = new SqlParameter("@UA_SECTION", ObjDegree.DegreeTypeID);
                sqlParams[1] = new SqlParameter("@UA_SECTIONNAME", ObjDegree.DegreeTypeName);
                sqlParams[2] = new SqlParameter("@CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[3] = new SqlParameter("@MODIFIEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[4] = new SqlParameter("@IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[5] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 06/12/2021
                sqlParams[6] = new SqlParameter("@ACTIVESTATUS", ObjDegree.Active);
                sqlParams[7] = new SqlParameter("@MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONFIG_UA_SECTION_INSERT_UPDATE", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DegreeController.SaveDegreeTypeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        /// ADDED ON 07-10-2021 BY Rishabh to Get Degree Type By UA_SECTION.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetDegreeTypeInfo(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@UA_SECTION", id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CONFIG_GET_DEGREETYPE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DegreeController.GetDegreeMasterInfoByDegreeTypeId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// MODIFIED By Sakshi M. on 18/10/2023 - for Active Status
        /// </summary>
        public int SaveDegreeMasterData(Degree ObjDegM)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = null;
                sqlParams = new SqlParameter[13];

                sqlParams[0] = new SqlParameter("@DEGREENO", ObjDegM.DegreeID);
                sqlParams[1] = new SqlParameter("@DEGREENAME", ObjDegM.DegreeName);
                sqlParams[2] = new SqlParameter("@CODE", ObjDegM.DegreeShort_Name);
                sqlParams[3] = new SqlParameter("@DEGREE_CODE", ObjDegM.DegreeCode);
                sqlParams[4] = new SqlParameter("@DEGREETYPEID", Convert.ToInt32(ObjDegM.DegreeTypeID));
                sqlParams[5] = new SqlParameter("@CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[6] = new SqlParameter("@MODIFIEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[7] = new SqlParameter("@IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[8] = new SqlParameter("@ACTIVESTATUS", ObjDegM.Active);
                sqlParams[9] = new SqlParameter("@MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[10] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 06/12/2021 
                sqlParams[11] = new SqlParameter("@DEGREENAMEHINDI", ObjDegM.DegreeName_Hindi);
                sqlParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONFIG_DEGREE_MASTER_INSERT_UPDATE", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DegreeController.SaveDegreeMasterData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


       
       
        /// <summary>
        /// Modified ON 22-10-2021 BY Rishabh to Get Degree master data by DEGREENO.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetDegreeInfo(int id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@DEGREENO", id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CONFIG_GET_DEGREE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DegreeController.GetDegreerInfoByDegreeId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
