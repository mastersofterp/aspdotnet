using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class DeptDesigMaster
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Department

                public int AddDepartment(PayMaster objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEPARTMENT", objPay.DEPARTMENT);
                       // objParams[1] = new SqlParameter("@P_DEPARTMENT_KANNADA", objPay.DEPARTMENT_KANNADA);
                        objParams[1] = new SqlParameter("@P_DEPTSHORTNAME", objPay.DEPTSHORTNAME);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objPay.SUBDEPTNO);
                        objParams[3] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEPARTMENT_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetDepartment()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_DEPARTMENTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Designation

                public DataSet GetDesignation()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_DESIGNATIONS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                public int AddDesignation(PayMaster objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DESIGNATION", objPay.DESIGNATION);
                        //objParams[1] = new SqlParameter("@P_DESIGNATION_KANNADA", objPay.DESIGNATION_KANNADA);
                        objParams[1] = new SqlParameter("@P_DESIGSHORTNAME", objPay.DESIGSHORT);
                        objParams[2] = new SqlParameter("@P_DESIG_SEQNO", objPay.DESIGNATION_SEQNO);
                        objParams[3] = new SqlParameter("@P_DESIGNO", objPay.SUBDESIGNO);
                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DESIGNATION_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if(Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if(Convert.ToInt32(ret) == 2627)
                        retStatus =Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion



                #region Main Department

                public int AddMainDepartment(PayMaster objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MAINDEPARTMENT", objPay.MAINDEPTNAME);
                        objParams[1] = new SqlParameter("@P_DEPTCODE", objPay.MAINDEPTCODE);
                        objParams[2] = new SqlParameter("@P_MAINDEPTNO", objPay.MAINDEPTNO);
                        objParams[3] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_MAIN_DEPARTMENT_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddMainDepartment -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMainDepartment()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_MAIN_DEPARTMENTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetMainDepartment->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                #region Division Master
                //Add Amol
                public int AddDivision(PayMaster objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SUBDEPTNO", objPay.SUBDEPTNO);
                        objParams[1] = new SqlParameter("@P_DIVNAME", objPay.DIVNAME);
                        objParams[2] = new SqlParameter("@P_DIVCODE", objPay.DIVCODE);
                        //objParams[3] = new SqlParameter("@P_CREATEDBY", objPay.SUBDEPTNO);
                        objParams[3] = new SqlParameter("@P_DIVIDNO", objPay.DIVIDNO);
                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DIVISIONMASTER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetDivision()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_DIVISIONMASTER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }



                #endregion

                #region Staff Type Master


                public int AddStaffType(PayMaster objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_STAFFNO", objPay.STAFFNO);
                        objParams[1] = new SqlParameter("@P_STAFF_NAME", objPay.STAFFTYPE);
                        objParams[2] = new SqlParameter("@P_ISACTIVE", objPay.ACTIVESTATUS);
                        objParams[3] = new SqlParameter("@P_ISTEACHING", objPay.IsTeaching);
                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_STAFF_TYPE_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetStaffType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_STAFFTYPE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region SCHEME MASTER

                public int AddSchemeMaster(PayMaster objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_STAFFNO", objPay.STAFFNO);
                        objParams[1] = new SqlParameter("@P_STAFF_NAME", objPay.STAFFTYPE);
                        objParams[2] = new SqlParameter("@P_RETIREMENT_AGE", objPay.RETIREMENTAGE);
                        objParams[3] = new SqlParameter("@P_ISACTIVE", objPay.ACTIVESTATUS);
                        objParams[4] = new SqlParameter("@P_ISTEACHING", objPay.IsTeaching);
                        objParams[5] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SCHEME_MASTER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSchemeMaster()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SCHEMEMASTER", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }
                #endregion
            }
        }
    }

}
