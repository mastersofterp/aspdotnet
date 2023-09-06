using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class DepartmentController
            {

                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region DepartMent

                public int AddDepartment(string DeptName, string DeptShortName, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MDNAME",DeptName);
                        objParams[1] = new SqlParameter("@P_MSNAME",DeptShortName);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE",collegeCode);
                        objParams[3] = new SqlParameter("@P_MDCODE",SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENT_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.AddDepartment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDepartment(string DeptName, string DeptShortName, string collegeCode, int mCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MDCODE",mCode);
                        objParams[1] = new SqlParameter("@P_MDNAME",DeptName);
                        objParams[2] = new SqlParameter("@P_MSNAME",DeptShortName);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE",collegeCode);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.UpdateDepartment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllDepartMent()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPARTMENT_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordDepartMent(int mCode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDCODE", mCode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPARTMENT_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetSingleRecordParyCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Sub DepartMent

                public int AddSubDepartMent(string subDeptName, string subDeptShortName, int show, int mCode, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New SubDepartMent
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DNAME",subDeptName);
                        objParams[1] = new SqlParameter("@P_SNAME",subDeptShortName);
                        objParams[2] = new SqlParameter("@P_SHOW",show);
                        objParams[3] = new SqlParameter("@P_MDCODE",mCode);
                        objParams[4] = new SqlParameter("P_COLLEGE_CODE",collegeCode);
                        objParams[5] = new SqlParameter("@P_DCODE",SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPT_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.AddSubDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSubDepartMent(int dcode,string subDeptName, string subDeptShortName, int show, int mCode, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update SubDepartMent                 
                   
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DCODE",dcode);
                        objParams[1] = new SqlParameter("@P_DNAME",subDeptName);
                        objParams[2] = new SqlParameter("@P_SNAME",subDeptShortName);
                        objParams[3] = new SqlParameter("@P_SHOW",show);
                        objParams[4] = new SqlParameter("@P_MDCODE",mCode);
                        objParams[5] = new SqlParameter("P_COLLEGE_CODE",collegeCode);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.UpdateParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllSubDepartMent()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllSubDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordSubDepartMent(int dcode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DCODE", dcode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetSingleRecordSubDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }
        }
    }

}
