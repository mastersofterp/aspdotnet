using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class QueryManagerController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region "Service Department"

                public int AddServiceDepartment(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[4];
                        sqlParams[0] = new SqlParameter("@P_QMDepartmentName", objQM.QMDepartmentName);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentShortname", objQM.QMDepartmentShortname);
                        sqlParams[2] = new SqlParameter("@P_IsActive", objQM.IsActive);
                        sqlParams[3] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[3].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_SERVICE_DEPARTMENT_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateServiceDepartment(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[5];
                        sqlParams[0] = new SqlParameter("@P_QMDepartmentName", objQM.QMDepartmentName);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentShortname", objQM.QMDepartmentShortname);
                        sqlParams[2] = new SqlParameter("@P_IsActive", objQM.IsActive);
                        sqlParams[3] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[4] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[4].Direction = ParameterDirection.Output;


                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_SERVICE_DEPARTMENT_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GETDATA()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_SERVICE_DEPARTMENT_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllAchievement() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetDataBYID(int ID)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", ID) };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_SERVICE_DEPARTMENT_GETDATABYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAchievementNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion "Service Department"


                #region "Request Type"

                public int AddRequestType(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[4];
                        sqlParams[0] = new SqlParameter("@P_QMRequestTypeName", objQM.QMRequestTypeName);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[2] = new SqlParameter("@P_IsActive", objQM.IsActive);
                        sqlParams[3] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[3].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_REQUEST_TYPE_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateRequestType(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[5];
                        sqlParams[0] = new SqlParameter("@P_QMRequestTypeName", objQM.QMRequestTypeName);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[2] = new SqlParameter("@P_IsActive", objQM.IsActive);
                        sqlParams[3] = new SqlParameter("@P_QMRequestTypeID", objQM.QMRequestTypeID);
                        sqlParams[4] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[4].Direction = ParameterDirection.Output;


                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_REQUEST_TYPE_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion "Request Type"

                #region "Request Category"

                public int AddRequestCategory(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[4];
                        sqlParams[0] = new SqlParameter("@P_QMRequestCategoryName", objQM.QMRequestCategoryName);
                        sqlParams[1] = new SqlParameter("@P_QMRequestTypeID", objQM.QMRequestTypeID);
                        sqlParams[2] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[3] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[3].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_REQUEST_CATEGORY_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateRequestCategory(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[5];
                        sqlParams[0] = new SqlParameter("@P_QMRequestCategoryName", objQM.QMRequestCategoryName);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[2] = new SqlParameter("@P_QMRequestTypeID", objQM.QMRequestTypeID);
                        sqlParams[3] = new SqlParameter("@P_QMRequestCategoryID", objQM.QMRequestCategoryID);
                        sqlParams[4] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[4].Direction = ParameterDirection.Output;


                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_REQUEST_CATEGORY_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion "Request Category"

                #region "Request Sub Category"

                public int AddRequestSubCategory(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[10];

                        sqlParams[0] = new SqlParameter("@P_QMRequestSubCategoryName", objQM.QMRequestSubCategoryName);
                        sqlParams[1] = new SqlParameter("@P_QMRequestTypeID", objQM.QMRequestTypeID);
                        sqlParams[2] = new SqlParameter("@P_QMRequestCategoryID", objQM.QMRequestCategoryID);
                        sqlParams[3] = new SqlParameter("@P_GeneralInstruction", objQM.GeneralInstruction);
                        sqlParams[4] = new SqlParameter("@P_IsPaidService", objQM.IsPaidService);
                        sqlParams[5] = new SqlParameter("@P_PaidServiceAmount", objQM.PaidServiceAmount);
                        sqlParams[6] = new SqlParameter("@P_IsEmergencyService", objQM.IsEmergencyService);
                        sqlParams[7] = new SqlParameter("@P_EmergencyServiceAmount", objQM.EmergencyServiceAmount);
                        sqlParams[8] = new SqlParameter("@P_EmergencyServiceHours", objQM.EmergencyServiceHours);
                        sqlParams[9] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[9].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_REQUEST_SUB_CATEGORY_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateRequestSubCategory(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[11];
                        sqlParams[0] = new SqlParameter("@P_QMRequestSubCategoryName", objQM.QMRequestSubCategoryName);
                        sqlParams[1] = new SqlParameter("@P_QMRequestTypeID", objQM.QMRequestTypeID);
                        sqlParams[2] = new SqlParameter("@P_QMRequestCategoryID", objQM.QMRequestCategoryID);
                        sqlParams[3] = new SqlParameter("@P_GeneralInstruction", objQM.GeneralInstruction);
                        sqlParams[4] = new SqlParameter("@P_IsPaidService", objQM.IsPaidService);
                        sqlParams[5] = new SqlParameter("@P_PaidServiceAmount", objQM.PaidServiceAmount);
                        sqlParams[6] = new SqlParameter("@P_IsEmergencyService", objQM.IsEmergencyService);
                        sqlParams[7] = new SqlParameter("@P_EmergencyServiceAmount", objQM.EmergencyServiceAmount);
                        sqlParams[8] = new SqlParameter("@P_EmergencyServiceHours", objQM.EmergencyServiceHours);
                        sqlParams[9] = new SqlParameter("@P_QMRequestSubCategoryID", objQM.QMRequestSubCategoryID);
                        sqlParams[10] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[10].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_REQUEST_SUB_CATEGORY_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                #endregion "Request Sub Category"


                #region "USER Allocation"

                public int AddUser(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[5];
                        sqlParams[0] = new SqlParameter("@P_QMRequestTypeID", objQM.RequestType);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[2] = new SqlParameter("@P_InchargeID", objQM.InchargeID);
                        sqlParams[3] = new SqlParameter("@P_MemberID", objQM.MemberID);
                        sqlParams[4] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[4].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_UserAllocation_INSERT_NEW", sqlParams, true);
                        status = (Int32)obj;
                        if (status == 1)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (status == 2627)
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateUser(QueryManager objQM)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[6];
                        sqlParams[0] = new SqlParameter("@P_QMRequestTypeID", objQM.RequestType);
                        sqlParams[1] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                        sqlParams[2] = new SqlParameter("@P_InchargeID", objQM.InchargeID);
                        sqlParams[3] = new SqlParameter("@P_MemberID", objQM.MemberID);
                        sqlParams[4] = new SqlParameter("@P_QMUserAllocationID", objQM.QMUserAllocationID);
                        sqlParams[5] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[5].Direction = ParameterDirection.Output;


                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_UserAllocation_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GETUSERDATA()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_UserAllocation_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllAchievement() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //public DataSet GETDUPLICATEUser(QueryManager objQM)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] sqlParams = new SqlParameter[4];
                //        sqlParams[0] = new SqlParameter("@P_QMRequestTypeID", objQM.RequestType);
                //        sqlParams[1] = new SqlParameter("@P_QMDepartmentID", objQM.QMDepartmentID);
                //        sqlParams[2] = new SqlParameter("@P_InchargeID", objQM.InchargeID);
                //        sqlParams[3] = new SqlParameter("@P_MemberID", objQM.MemberID);


                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_UserAllocation_Get", sqlParams);

                //    }
                //    catch (Exception ex)
                //    {

                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ds;
                //}



                #endregion "User Allocation"
                

            }
        }
    }
}
