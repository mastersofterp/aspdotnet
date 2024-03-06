//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [PayHeadPrivilegesController]                                  
// CREATION DATE : 01-MAY-2009                                                        
// CREATED BY    : KIRAN GVS                                       
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
            public class PayHeadPrivilegesController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllPayHead()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_PAYHEAD_USER_ACCESS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetAllPayHead-> " + ex.ToString());
                    }
                   return ds;
                }


                public DataSet GetPayHeadUser(int uaNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", uaNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_PAY_ACCESS_PAYHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetPayHeadUser-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }



                public DataSet EditPayHeadUser(int uaNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", uaNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_USER_ACCESS_PAYHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.EditPayHeadUser-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }

                //TO CALL EARNING HEADS ONLY
                public DataSet EditPayHeadUserEarnings(int uaNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", uaNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_USER_ACCESS_PAYHEAD_EARNINGS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.EditPayHeadUser-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }


                public int AddUser(int uaNo,string payHead,string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO",uaNo);
                        objParams[1] = new SqlParameter("@P_PAYHEAD",payHead);                        
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE",colcode);
                        objParams[3] = new SqlParameter("@P_ACCESS_PAYHEAD_ID", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_PAY_ACCESS_PAYHEAD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayHeadPrivilegesController.AddUser-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int DeleteUser(int uaNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO",uaNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_PAY_ACCESS_PAYHEAD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayHeadPrivilegesController.DeleteUser-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //FOR ID CARD 12/10/2017
                public DataSet GetEmpForIdentityCard(int staffNo, int deptno, int COLLEGE_NO, string orderBy)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_ORDERBY", orderBy);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMPLOYEE_FOR_ID_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetEmpByStaffno-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //for dropdrown payhead 23-09-2022
                public DataSet GetPayHeadsforbulkupdate()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_UPD_ddlPayhead", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetPayHeadsforbulkupdate-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }
                 
                //for No Dues 11-10-2022
                public DataSet GetAllAuthoName(int AuthoID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AUTHO_TYP_ID", AuthoID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BIND_ALL_AUTHORITY_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddAuthoDetails(DataTable DT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_AuthoTBL", DT);

                        objParams[1] = new SqlParameter("@P_ID", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_PAYROLL_NODUES_AUTHORITY_DEATIL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayHeadPrivilegesController.AddUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //---------Start 05-11-2022 Shaikh Juned 
                public int SaveEmployeeExcelSheetData(int ClgCode, int OrgId, PayMaster objPayMas, int usertype, int Uano, int IsMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COLLEGE_CODE", ClgCode);
                        objParams[1] = new SqlParameter("@P_OrganizationId", OrgId);
                        objParams[2] = new SqlParameter("@P_EMP_MIGRATION_DATA", objPayMas.EmployeeDataImport_TBL);
                        objParams[3] = new SqlParameter("@P_User_Type", usertype);
                        objParams[4] = new SqlParameter("@P_CreatedBY", Uano);
                        objParams[5] = new SqlParameter("@P_IsMaster", IsMaster);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EMPLOYEE_DATA_MIGRATION_EXCEL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayHeadPrivilegesController.AddUser-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //---------end 05-11-2022 Shaikh Juned

                //---------START  02-01-2024 SHAIKH JUNED

                public DataSet GetMasterData(int ischeck)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IsCheck", ischeck);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYROLL_EMPLOYEE_MASTER_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetPayHeadUser-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }




                //---------END----02-01-2024----SHAIKH JUNDED

            }
        }
    }
}
