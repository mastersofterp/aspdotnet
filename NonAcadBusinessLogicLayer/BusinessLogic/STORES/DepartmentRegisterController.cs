using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
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
            public class DepartmentRegisterController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region DepartMentRegister

                public int AddDeptRegister(string drName, int mdCode, decimal drsrno,string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DRNAME", drName);
                        objParams[1] = new SqlParameter("@P_MDCODE", mdCode);
                        objParams[2] = new SqlParameter("@P_DRSRNO", drsrno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_DRNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPTREGISTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentRegisterController.AddDeptRegister-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDeptRegister(string drName, int mdCode, decimal drsrno, string collegeCode, int drNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DRNO", drNo);
                        objParams[1] = new SqlParameter("@P_DRNAME", drName);
                        objParams[2] = new SqlParameter("@P_MDCODE", mdCode);
                        objParams[3] = new SqlParameter("@P_DRSRNO", drsrno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);                       
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPTREGISTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentRegisterController.UpdateDeptRegister-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllDeptRegister()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPTREGISTER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentRegisterController.GetAllDeptRegister-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordDeptRegister(int drNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DRNO", drNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPTREGISTER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentRegisterController.GetSingleRecordDeptRegister-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }
        }
    }
}
