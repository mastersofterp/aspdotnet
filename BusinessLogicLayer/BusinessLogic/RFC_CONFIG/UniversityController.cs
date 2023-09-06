
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : University Controller                  
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : S.Patil
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

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic.RFC_CONFIG
        {
            public class UniversityController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                ///// <summary>
                ///// ADDED ON 07-10-2021 BY S.Patil to GetUniversityMasterData
                ///// </summary>
                ///// <returns></returns>
                //public DataSet GetUniversityMasterData()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@UNIVERSITYID ", 0);
                //        ds = objSQLHelper.ExecuteDataSetSP("sptblConfigUniversityMaster_Get", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UniversityController.GetUniversityMasterData() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ds;
                //}
                /// <summary>
                /// ADDED ON 07-10-2021 BY S.Patil to GetUniversityMasterDateByid
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                public DataSet GetUniversityMasterDateByid(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@UNIVERSITYID", id);
                        ds = objSQLHelper.ExecuteDataSetSP("sptblConfigUniversityMaster_Get", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UniversityController.GetUniversityMasterDateByid() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                /// <summary>
                /// ADDED ON 07-10-2021 BY S.Patil to  SaveUniversityMasterData
                /// </summary>
                /// <param name="objUniversity"></param>
                /// <returns></returns>
                public int SaveUniversityMasterData(University objUniversity)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = null;
                        sqlParams = new SqlParameter[10];

                        sqlParams[0] = new SqlParameter("@UNIVERSITYNAME ", objUniversity.UniversityName);
                        sqlParams[1] = new SqlParameter("@UNIVERSITYID ", objUniversity.Universityid);
                        sqlParams[2] = new SqlParameter("@STATEID", objUniversity.Stateid);
                        sqlParams[3] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        sqlParams[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        sqlParams[5] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        sqlParams[6] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        sqlParams[7] = new SqlParameter("@ActiveStatus", objUniversity.Status);
                        sqlParams[8] = new SqlParameter("@MacAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                        sqlParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        sqlParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONFIG_UNIVERSITY_MASTER_INSERT_UPDATE", sqlParams, true);
                        status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UniversityController.SaveUniversityMasterData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

            }
        }
    }
}