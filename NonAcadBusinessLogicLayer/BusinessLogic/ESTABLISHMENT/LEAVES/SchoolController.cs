//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE MGT.                               
// CREATION DATE : 24-08-2015                                                        
// CREATED BY    : SWATI GHATE
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
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
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class SchoolController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region SchoolMaster
              
                // To insert New School Entry

                public int AddUpdateSchool(SchoolMaster objSchool)
                {
                    int pkid = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SCHOOLNO", objSchool.SCHOOL_NO);
                        objParams[1] = new SqlParameter("@P_SCHOOL_NAME", objSchool.SCHOOL_NAME);
                        objParams[2] = new SqlParameter("@P_USER", objSchool.USER);
                      //  objParams[3] = new SqlParameter("@P_IPADDRESS", objSchool.IPADDRESS);
                       // objParams[4] = new SqlParameter("@P_MACADDRESS", objSchool.MACADDRESS);
                        objParams[3] = new SqlParameter("@P_DATE", objSchool.DATE);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objSchool.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_INS_UPD", objParams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_INS_UPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                pkid = -99;
                            }
                            else
                                pkid = Convert.ToInt32(ret.ToString());
                        }
                        else
                        {
                            pkid = -99;
                           
                        }

                    }
                    catch (Exception ee)
                    {
                        pkid = -99;
                    }
                    return pkid;
                }

                // To Delete School
                public int DeleteSchool(int SCHOOL_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHOOL_NO", SCHOOL_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all School
                public DataSet GetAllSchool()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SCHOOL_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single School detail by passing School No
                public DataSet GetSingleSchool(int SCHOOL_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHOOL_NO", SCHOOL_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SCHOOL_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End School Master Region
                #region School_Department
                
                // To insert New School departmentEntry

                public int AddUpdateSchoolDepartment(SchoolMaster objSchool)
                {
                    int pkid = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_SCHOOL_DEPT_NO", objSchool.SCHOOL_DEPT_NO);
                        objParams[1] = new SqlParameter("@P_SCHOOLNO", objSchool.SCHOOL_NO);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSchool.DEPT_NO);
                        objParams[3] = new SqlParameter("@P_USER", objSchool.USER);
                       // objParams[3] = new SqlParameter("@P_IPADDRESS", objSchool.IPADDRESS);
                       // objParams[4] = new SqlParameter("@P_MACADDRESS", objSchool.MACADDRESS);
                        objParams[4] = new SqlParameter("@P_DATE", objSchool.DATE);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objSchool.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_INS_UPD", objParams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_DEPT_INS_UPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                pkid = -99;
                            }
                            else
                                pkid = Convert.ToInt32(ret.ToString());
                        }
                        else
                        {
                            pkid = -99;

                        }

                    }
                    catch (Exception ee)
                    {
                        pkid = -99;
                    }
                    return pkid;
                }

                // To Delete School
                public int DeleteSchoolDepartment(int SCHOOL_DEPT_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHOOL_DEPT_NO", SCHOOL_DEPT_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_DEPT_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.DeleteSchoolDepartment-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all School
                public DataSet GetAllSchoolDept()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SCHOOL_DEPT_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.GetAllSchoolDept-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single School detail by passing School No
                public DataSet GetSingleSchoolDept(int SCHOOL_DEPT_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHOOL_DEPT_NO", SCHOOL_DEPT_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SCHOOL_DEPT_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.GetSingleSchoolDept->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion
            }
        }
    }
}
