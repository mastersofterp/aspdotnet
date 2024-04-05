using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class NEPController
            {
                string _UAIMS_constr = string.Empty;

                public NEPController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                }

                public DataSet Get_CollegeID_ByScheme(int schemeid)
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_SCHEMENO",schemeid)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_ACD_GET_COLLEGEIDBYSCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }

                public int SchemewiseNEPInsert(int activity, int schemeno, string collegeid, string collegecode, string categoryno, string nepschemename, 
                    DateTime date, int uano, string ip_address, int groupid) 
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[12];

                        sqlParams[0] = new SqlParameter("@P_ACTIVITY", activity);
                        sqlParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        sqlParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        sqlParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        sqlParams[4] = new SqlParameter("@P_CATEGORYNO", categoryno);
                        sqlParams[5] = new SqlParameter("@P_NEP_SCHEME_NAME", nepschemename);
                        sqlParams[6] = new SqlParameter("@P_CREATED_DATE", date);
                        sqlParams[7] = new SqlParameter("@P_CREATED_BY", uano);
                        sqlParams[8] = new SqlParameter("@P_IP_ADDRESS", ip_address);
                        sqlParams[9] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        sqlParams[10] = new SqlParameter("@P_GROUPID", groupid);
                        sqlParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        sqlParams[11].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPSERT_ACD_NEP_SCHEME_MAPPING", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.NEPController.SchemewiseNEPInsert() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetSchemeWiseNEPMapping()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[0];

                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_NEP_SCHEME_MAPPING_DATA", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.NEPController.GetSchemeWiseNEPMapping() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSchemeWiseNEPMappingbyGroupID(int groupid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[1];
                        sqlParams[0] = new SqlParameter("@P_GROUP_ID", groupid);

                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_NEP_SCHEME_MAPPING_DATA_BY_GROUPID", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.NEPController.GetSchemeWiseNEPMapping() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSchemeWiseNEPMappingEdit(int groupid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[1];
                        sqlParams[0] = new SqlParameter("@P_GROUP_ID", groupid);

                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_NEP_SCHEME_MAPPING_DATA_EDIT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.NEPController.GetSchemeWiseNEPMapping() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetNEPCategorybyScheme(int sessionid, int schemeid, int semid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[3];
                        sqlParams[0] = new SqlParameter("@P_SESSIONID", sessionid);
                        sqlParams[1] = new SqlParameter("@P_SCHEME_ID", schemeid);
                        sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semid);

                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_NEP_COURSE_CATEGORY_DATA_BY_SCHEME", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.NEPController.GetNEPCategorybyScheme() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int NEPCreditInsert(int sessionid, int schemeno, int semesterno, string categoryno, string maxcredit, string mincredit, 
                    string nosubjects, DateTime date, int uano, string ipaddress) 
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[12];

                        sqlParams[0] = new SqlParameter("@P_SESSIONID", sessionid);
                        sqlParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        sqlParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        sqlParams[3] = new SqlParameter("@P_CATEGORYNO", categoryno);
                        sqlParams[4] = new SqlParameter("@P_MAX_CREDIT", maxcredit);
                        sqlParams[5] = new SqlParameter("@P_MIN_CREDIT", mincredit);
                        sqlParams[6] = new SqlParameter("@P_NO_OF_SUBJECTS", nosubjects);
                        sqlParams[7] = new SqlParameter("@P_CREATED_DATE", date);
                        sqlParams[8] = new SqlParameter("@P_CREATED_BY", uano);
                        sqlParams[9] = new SqlParameter("@P_IP_ADDRESS", ipaddress);
                        sqlParams[10] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        sqlParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        sqlParams[11].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACD_UPSERT_ACD_NEP_CREDIT_DEFINATION", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.NEPController.NEPCreditInsert() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

            }
        }
    }
}
