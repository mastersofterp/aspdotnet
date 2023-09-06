using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class DepartmentPreviliageController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region STORE_DEPT_PRIVELEGE

                public int AddDeptPrivelege(int mdCode, string uaName, int uaNO, int approvalLevelNo,string colCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New STORE_DEPT_PRIVELEGE
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MDCODE",mdCode);
                        objParams[1] = new SqlParameter("@P_UA_NAME",uaName);
                        objParams[2] = new SqlParameter("@P_UA_NO", uaNO);
                        objParams[3] = new SqlParameter("@P_APPROVAL_LEVEL_NO",approvalLevelNo);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                        objParams[5] = new SqlParameter("@P_DPV_NO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPT_PRIVELEGE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentPreviliageController.AddDeptPrivelege-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDeptPrivelege(int mdCode, string uaName, int uaNO, int approvalLevelNo, string colCode, int dpvNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update  STORE_DEPT_PRIVELEGE
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DPV_NO", dpvNo);
                        objParams[1] = new SqlParameter("@P_MDCODE", mdCode);
                        objParams[2] = new SqlParameter("@P_UA_NAME", uaName);
                        objParams[3] = new SqlParameter("@P_UA_NO", uaNO);
                        objParams[4] = new SqlParameter("@P_APPROVAL_LEVEL_NO", approvalLevelNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPT_PRIVELEGE_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentPreviliageController.UpdateDeptPrivelege-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllDeptPrivelege()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_PRIVELEGE_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentPreviliageController.GetAllDeptPrivelege-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordDeptPrivelege(int dpvNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DPV_NO", dpvNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_PRIVELEGE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentPreviliageController.GetSingleRecordDeptPrivelege-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }
        }
    }
}
