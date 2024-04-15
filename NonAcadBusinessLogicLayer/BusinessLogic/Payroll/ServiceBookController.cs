//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[SERVICE BOOK CONTROLLER]                                  
// CREATION DATE : 17-JUNE-2009                                                        
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
            public class ServiceBookController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                Common objcommon = new Common();
                #region SearchEmployee
                /// <summary>
                /// To get employee record as per college wise at search listview
                /// </summary>
                /// <param name="search"></param>
                /// <param name="category"></param>
                /// <param name="collegeid"></param>
                /// <returns></returns>
                /// 





                public DataTable RetrieveEmpDetailsServiceBook(string search, string category, int collegeid, int stno)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeid);
                        objParams[3] = new SqlParameter("@P_STNO", stno);

                        dt = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE_SERVICEBOOK", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return dt;
                }
                #endregion
                #region Payroll_Sb_DeptExam

                public int AddDeptExam(ServiceBook objDeptExam)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objDeptExam.IDNO);
                        objParams[1] = new SqlParameter("@P_EXAM", objDeptExam.EXAM);
                        objParams[2] = new SqlParameter("@P_REGNO", objDeptExam.REGNO);
                        objParams[3] = new SqlParameter("@P_PASSYEAR ", objDeptExam.PASSYEAR);
                        objParams[4] = new SqlParameter("@P_OFFICER ", objDeptExam.OFFICER);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objDeptExam.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_ATTACHMENT", objDeptExam.ATTACHMENTS);
                        objParams[7] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[8] = new SqlParameter("@P_EXAMID", objDeptExam.EXAMID);
                        objParams[9] = new SqlParameter("@P_EXAMNAME", objDeptExam.EXAMname);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objDeptExam.FILEPATH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objDeptExam.ISBLOB);
                        objParams[12] = new SqlParameter("@P_DENO ", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_DEPTEXAM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddDeptExam-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDeptExam(ServiceBook objDeptExam)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", objDeptExam.IDNO);
                        objParams[1] = new SqlParameter("@P_EXAM", objDeptExam.EXAM);
                        objParams[2] = new SqlParameter("@P_REGNO", objDeptExam.REGNO);
                        objParams[3] = new SqlParameter("@P_PASSYEAR ", objDeptExam.PASSYEAR);
                        objParams[4] = new SqlParameter("@P_OFFICER ", objDeptExam.OFFICER);
                        objParams[5] = new SqlParameter("@P_DENO ", objDeptExam.DENO);
                        objParams[6] = new SqlParameter("@P_ATTACHMENT", objDeptExam.ATTACHMENTS);
                        objParams[7] = new SqlParameter("@P_EXAMID ", objDeptExam.EXAMID);
                        objParams[8] = new SqlParameter("@P_EXAMNAME", objDeptExam.EXAMname);
                        objParams[9] = new SqlParameter("@P_FILEPATH", objDeptExam.FILEPATH);
                        objParams[10] = new SqlParameter("@P_ISBLOB", objDeptExam.ISBLOB);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_DEPTEXAM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateDeptExam-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteDeptExam(int deNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DENO ", deNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_DEPTEXAM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteDeptExam.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllDeptExamDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_DEPTEXAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllDeptExamDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleDeptExamDetailsOfEmployee(int deNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DENO", deNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_DEPTEXAM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleDeptExamDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllDeptExamCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_DEPT_EXAM_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllDeptExamDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_FamilyInfo

                public int AddFamilyInfo(ServiceBook objFamilyInfo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[31];
                        objParams[0] = new SqlParameter("@P_IDNO", objFamilyInfo.IDNO);
                        objParams[1] = new SqlParameter("@P_MEMNAME", objFamilyInfo.MEMNAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objFamilyInfo.ADDRESS);
                        objParams[3] = new SqlParameter("@P_RELATION", objFamilyInfo.RELATION);
                        objParams[4] = new SqlParameter("@P_AGE", objFamilyInfo.AGE);
                        objParams[5] = new SqlParameter("@P_DOB", objFamilyInfo.DOB);

                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objFamilyInfo.COLLEGE_CODE);

                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objFamilyInfo.ATTACHMENTS);
                        //objParams[8] = new SqlParameter("@P_FNNO", SqlDbType.Int);
                        //objParams[8].Direction = ParameterDirection.Output;
                        //objParams[8] = new SqlParameter("@P_FNNO", SqlDbType.Int);
                        //objParams[8].Direction = ParameterDirection.Output;
                        objParams[8] = new SqlParameter("@P_ADDRESS_FLAG", objFamilyInfo.ADDRESS_FLAG);
                        objParams[9] = new SqlParameter("@P_EDUCATION", objFamilyInfo.EDUCATION);
                        objParams[10] = new SqlParameter("@P_EMPLOYMENT", objFamilyInfo.EMPLOYMENT);
                        objParams[11] = new SqlParameter("@P_GENDER", objFamilyInfo.GENDER);
                        objParams[12] = new SqlParameter("@P_MARITAL", objFamilyInfo.MaritalStatus);
                        objParams[13] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[14] = new SqlParameter("@P_ADHARNO", objFamilyInfo.ADHARNO);
                        objParams[15] = new SqlParameter("@P_MOBNO", objFamilyInfo.MOBNO);
                        objParams[16] = new SqlParameter("@P_TALUKA", objFamilyInfo.TALUKA);
                        objParams[17] = new SqlParameter("@P_DISTRICT", objFamilyInfo.DISTRICT);
                        objParams[18] = new SqlParameter("@P_PINCODE", objFamilyInfo.PINCODE);
                        objParams[19] = new SqlParameter("@P_BLOODGROUP", objFamilyInfo.BLOODGROUP);
                        objParams[20] = new SqlParameter("@P_CITY", objFamilyInfo.CITY);
                        objParams[21] = new SqlParameter("@P_EMPSTATUS", objFamilyInfo.EMPSTATUS);
                        objParams[22] = new SqlParameter("@P_POSTNAME", objFamilyInfo.POSTNAME);
                        objParams[23] = new SqlParameter("@P_ORGNAME_ADDRESS", objFamilyInfo.ORGNAME_ADDRESS);
                        objParams[24] = new SqlParameter("@P_SALARY", objFamilyInfo.LASTSALARY);
                        objParams[25] = new SqlParameter("@P_STATE", objFamilyInfo.STATE);
                        objParams[26] = new SqlParameter("@P_COUNTRY", objFamilyInfo.COUNTRY);
                        objParams[27] = new SqlParameter("@P_RelationshipId", objFamilyInfo.RelationshipId);
                        objParams[28] = new SqlParameter("@P_FILEPATH", objFamilyInfo.FILEPATH);
                        objParams[29] = new SqlParameter("@P_ISBLOB", objFamilyInfo.ISBLOB);
                        objParams[30] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[30].Direction = ParameterDirection.Output;
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_FAMILYINFO", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_FAMILYINFO", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddFamilyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateFamilyInfo(ServiceBook objFamilyInfo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[30];
                        objParams[0] = new SqlParameter("@P_IDNO", objFamilyInfo.IDNO);
                        objParams[1] = new SqlParameter("@P_MEMNAME", objFamilyInfo.MEMNAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objFamilyInfo.ADDRESS);
                        objParams[3] = new SqlParameter("@P_RELATION", objFamilyInfo.RELATION);
                        objParams[4] = new SqlParameter("@P_AGE", objFamilyInfo.AGE);
                        objParams[5] = new SqlParameter("@P_DOB", objFamilyInfo.DOB);

                        objParams[6] = new SqlParameter("@P_FNNO", objFamilyInfo.FNNO);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objFamilyInfo.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_ADDRESS_FLAG", objFamilyInfo.ADDRESS_FLAG);
                        objParams[9] = new SqlParameter("@P_EDUCATION", objFamilyInfo.EDUCATION);
                        objParams[10] = new SqlParameter("@P_EMPLOYMENT", objFamilyInfo.EMPLOYMENT);
                        objParams[11] = new SqlParameter("@P_GENDER", objFamilyInfo.GENDER);
                        objParams[12] = new SqlParameter("@P_MARITAL", objFamilyInfo.MaritalStatus);
                        objParams[13] = new SqlParameter("@P_ADHARNO", objFamilyInfo.ADHARNO);
                        objParams[14] = new SqlParameter("@P_MOBNO", objFamilyInfo.MOBNO);
                        objParams[15] = new SqlParameter("@P_TALUKA", objFamilyInfo.TALUKA);
                        objParams[16] = new SqlParameter("@P_DISTRICT", objFamilyInfo.DISTRICT);
                        objParams[17] = new SqlParameter("@P_PINCODE", objFamilyInfo.PINCODE);
                        objParams[18] = new SqlParameter("@P_BLOODGROUP", objFamilyInfo.BLOODGROUP);
                        objParams[19] = new SqlParameter("@P_CITY", objFamilyInfo.CITY);
                        objParams[20] = new SqlParameter("@P_EMPSTATUS", objFamilyInfo.EMPSTATUS);
                        objParams[21] = new SqlParameter("@P_POSTNAME", objFamilyInfo.POSTNAME);
                        objParams[22] = new SqlParameter("@P_ORGNAME_ADDRESS", objFamilyInfo.ORGNAME_ADDRESS);
                        objParams[23] = new SqlParameter("@P_SALARY", objFamilyInfo.LASTSALARY);
                        objParams[24] = new SqlParameter("@P_STATE", objFamilyInfo.STATE);
                        objParams[25] = new SqlParameter("@P_COUNTRY", objFamilyInfo.COUNTRY);
                        objParams[26] = new SqlParameter("@P_RelationshipId", objFamilyInfo.RelationshipId);
                        objParams[27] = new SqlParameter("@P_ISBLOB", objFamilyInfo.ISBLOB);
                        objParams[28] = new SqlParameter("@P_FILEPATH", objFamilyInfo.FILEPATH);
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_FAMILYINFO", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //--------------------------------------------------------UPDATED ON 14-11-2019-----------------------------------------//
                        objParams[29] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[29].Direction = ParameterDirection.Output;
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_FAMILYINFO", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_FAMILYINFO", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateFamilyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteFamilyInfo(int fnNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FNNO", fnNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_FAMILYINFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteFamilyInfo.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllFamilyDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_FAMILYINFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleFamilyDetailsOfEmployee(int fnNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FNNO", fnNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_FAMILYINFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllFamilyDetailsCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_FAMILYINFO_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_ForService

                public int AddForService(ServiceBook objForService)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objForService.IDNO);
                        objParams[1] = new SqlParameter("@P_POSTNAME", objForService.POSTNAME);
                        objParams[2] = new SqlParameter("@P_FDT", objForService.FDT);
                        objParams[3] = new SqlParameter("@P_TDT", objForService.TDT);
                        objParams[4] = new SqlParameter("@P_LSC", objForService.LSC);
                        objParams[5] = new SqlParameter("@P_LSCR", objForService.LSCR);
                        objParams[6] = new SqlParameter("@P_REMARK", objForService.REMARK);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objForService.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_ATTACHMENT", objForService.ATTACHMENTS);
                        objParams[9] = new SqlParameter("@P_FSNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_FORSERVICE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddForService-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateForService(ServiceBook objForService)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objForService.IDNO);
                        objParams[1] = new SqlParameter("@P_POSTNAME", objForService.POSTNAME);
                        objParams[2] = new SqlParameter("@P_FDT", objForService.FDT);
                        objParams[3] = new SqlParameter("@P_TDT", objForService.TDT);
                        objParams[4] = new SqlParameter("@P_LSC", objForService.LSC);
                        objParams[5] = new SqlParameter("@P_LSCR", objForService.LSCR);
                        objParams[6] = new SqlParameter("@P_REMARK", objForService.REMARK);
                        objParams[7] = new SqlParameter("@P_FSNO", objForService.FSNO);
                        objParams[8] = new SqlParameter("@P_ATTACHMENT", objForService.ATTACHMENTS);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_FORSERVICE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateForService-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteForService(int fsNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FSNO", fsNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_FORSERVICE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteForService.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllForServiceDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_FORSERVICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllForServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleForServiceDetailsOfEmployee(int fsNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FSNO", fsNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_FORSERVICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleForServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_Loan

                //public int AddLoan(ServiceBook objLoan)
                //{
                //    int retStatus = 0;

                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New File
                //        objParams = new SqlParameter[11];
                //        objParams[0] = new SqlParameter("@P_IDNO", objLoan.IDNO);
                //        objParams[1] = new SqlParameter("@P_LOANNO", objLoan.LOANNO);
                //        objParams[2] = new SqlParameter("@P_ORDERNO", objLoan.ORDERNO);
                //        objParams[3] = new SqlParameter("@P_AMOUNT", objLoan.AMOUNT);
                //        objParams[4] = new SqlParameter("@P_INTEREST", objLoan.INTEREST);
                //        objParams[5] = new SqlParameter("@P_INSTAL", objLoan.INSTAL);

                //        if (!objLoan.LOANDT.Equals(DateTime.MinValue))
                //            objParams[6] = new SqlParameter("@P_LOANDT", objLoan.LOANDT);
                //        else
                //            objParams[6] = new SqlParameter("@P_LOANDT", DBNull.Value);

                //        objParams[7] = new SqlParameter("P_REMARK", objLoan.REMARK);
                //        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objLoan.COLLEGE_CODE);
                //        objParams[9] = new SqlParameter("@P_ATTACHMENT", objLoan.ATTACHMENTS);
                //        // objParams[10] = new SqlParameter("@P_LNO", SqlDbType.Int);
                //        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[10].Direction = ParameterDirection.Output;

                //        //  if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_LOAN", objParams, false) != null)
                //        // retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_LOAN", objParams, true);
                //        if (Convert.ToInt32(ret) == -1)
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                //        }
                //        else
                //        {
                //            //objLeave.LNO = Convert.ToInt32(ret);
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddLoan-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddLoan(ServiceBook objLoan)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", objLoan.IDNO);
                        objParams[1] = new SqlParameter("@P_LOANNO", objLoan.LOANNO);
                        objParams[2] = new SqlParameter("@P_ORDERNO", objLoan.ORDERNO);
                        objParams[3] = new SqlParameter("@P_AMOUNT", objLoan.AMOUNT);
                        objParams[4] = new SqlParameter("@P_INTEREST", objLoan.INTEREST);
                        objParams[5] = new SqlParameter("@P_INSTAL", objLoan.INSTAL);

                        if (!objLoan.LOANDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_LOANDT", objLoan.LOANDT);
                        else
                            objParams[6] = new SqlParameter("@P_LOANDT", DBNull.Value);

                        objParams[7] = new SqlParameter("P_REMARK", objLoan.REMARK);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objLoan.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_ATTACHMENT", objLoan.ATTACHMENTS);
                        objParams[10] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[11] = new SqlParameter("@P_AFFIDAVITATTACH", objLoan.AFFIDAVITATTACH);
                        objParams[12] = new SqlParameter("@P_FILEPATH", objLoan.FILEPATH);
                        objParams[13] = new SqlParameter("@P_ISBLOB", objLoan.ISBLOB);
                        objParams[14] = new SqlParameter("@P_LNO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_LOAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddLoan-> " + ex.ToString());
                    }
                    return retStatus;
                }






                //public int UpdateLoan(ServiceBook objLoan)
                //{
                //    int retStatus = 0;

                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New File
                //        objParams = new SqlParameter[11];
                //        objParams[0] = new SqlParameter("@P_IDNO", objLoan.IDNO);
                //        objParams[1] = new SqlParameter("@P_LOANNO", objLoan.LOANNO);
                //        objParams[2] = new SqlParameter("@P_ORDERNO", objLoan.ORDERNO);
                //        objParams[3] = new SqlParameter("@P_AMOUNT", objLoan.AMOUNT);
                //        objParams[4] = new SqlParameter("@P_INTEREST", objLoan.INTEREST);
                //        objParams[5] = new SqlParameter("@P_INSTAL", objLoan.INSTAL);


                //        if (!objLoan.LOANDT.Equals(DateTime.MinValue))
                //            objParams[6] = new SqlParameter("@P_LOANDT", objLoan.LOANDT);
                //        else
                //            objParams[6] = new SqlParameter("@P_LOANDT", DBNull.Value);
                //        objParams[7] = new SqlParameter("P_REMARK", objLoan.REMARK);
                //        objParams[8] = new SqlParameter("@P_LNO", objLoan.LNO);
                //        objParams[9] = new SqlParameter("@P_ATTACHMENT", objLoan.ATTACHMENTS);
                //        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_LOAN", objParams, false) != null)
                //        //     retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //        #region Edited on 18-11-2019 by Vijay

                //        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[10].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_LOAN", objParams, false);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-1"))
                //            {
                //                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                //            }
                //            else if (ret.ToString().Equals("1"))
                //            {
                //                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //            }

                //        }

                //        #endregion

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateLoan-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int UpdateLoan(ServiceBook objLoan)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objLoan.IDNO);
                        objParams[1] = new SqlParameter("@P_LOANNO", objLoan.LOANNO);
                        objParams[2] = new SqlParameter("@P_ORDERNO", objLoan.ORDERNO);
                        objParams[3] = new SqlParameter("@P_AMOUNT", objLoan.AMOUNT);
                        objParams[4] = new SqlParameter("@P_INTEREST", objLoan.INTEREST);
                        objParams[5] = new SqlParameter("@P_INSTAL", objLoan.INSTAL);


                        if (!objLoan.LOANDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_LOANDT", objLoan.LOANDT);
                        else
                            objParams[6] = new SqlParameter("@P_LOANDT", DBNull.Value);
                        objParams[7] = new SqlParameter("P_REMARK", objLoan.REMARK);
                        objParams[8] = new SqlParameter("@P_LNO", objLoan.LNO);
                        objParams[9] = new SqlParameter("@P_ATTACHMENT", objLoan.ATTACHMENTS);
                        objParams[10] = new SqlParameter("@P_AFFIDAVITATTACH", objLoan.AFFIDAVITATTACH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objLoan.ISBLOB);
                        objParams[12] = new SqlParameter("@P_FILEPATH", objLoan.FILEPATH);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_LOAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateLoan-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public int DeleteLoan(int lNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", lNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_LOAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteLoan.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllLoanDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_LOAN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllLoanDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleLoanDetailsOfEmployee(int lNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", lNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_LOAN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleLoanDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllLoanDetailsCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_LOAN_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllLoanDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_Matter

                public int AddMatter(ServiceBook objMatter)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objMatter.IDNO);


                        if (!objMatter.EDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_EDT", objMatter.EDT);
                        else
                            objParams[1] = new SqlParameter("@P_EDT", DBNull.Value);
                        objParams[2] = new SqlParameter("@P_HEADING", objMatter.HEADING);
                        objParams[3] = new SqlParameter("@P_MATTER", objMatter.MATTER);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objMatter.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_ATTACHMENT", objMatter.ATTACHMENTS);
                        objParams[6] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[7] = new SqlParameter("@P_FILEPATH", objMatter.FILEPATH);
                        objParams[8] = new SqlParameter("@P_ISBLOB", objMatter.ISBLOB);
                        objParams[9] = new SqlParameter("@P_MNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_MATTER", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_MATTER", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddMatter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMatter(ServiceBook objMatter)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objMatter.IDNO);

                        if (!objMatter.EDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_EDT", objMatter.EDT);
                        else
                            objParams[1] = new SqlParameter("@P_EDT", DBNull.Value);
                        objParams[2] = new SqlParameter("@P_HEADING", objMatter.HEADING);
                        objParams[3] = new SqlParameter("@P_MATTER", objMatter.MATTER);
                        objParams[4] = new SqlParameter("@P_MNO", objMatter.MNO);
                        objParams[5] = new SqlParameter("@P_ATTACHMENT", objMatter.ATTACHMENTS);
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_MATTER", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        objParams[6] = new SqlParameter("@P_FILEPATH", objMatter.FILEPATH);
                        objParams[7] = new SqlParameter("@P_ISBLOB", objMatter.ISBLOB);
                        #region Edited on 15-11-2019
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_MATTER", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                        #endregion



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateMatter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteMatter(int mNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MNO", mNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_MATTER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteMatter.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllMatterDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_MATTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllMatterDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleMatterDetailsOfEmployee(int mNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MNO", mNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_MATTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleMatterDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllMatterCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_MATTERS_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllMatterDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_NominiFor
                public int AddNominiFor(ServiceBook objNominiFor, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_IDNO", objNominiFor.IDNO);
                        objParams[1] = new SqlParameter("@P_NTNO", objNominiFor.NTNO);
                        objParams[2] = new SqlParameter("@P_NAME", objNominiFor.NAME);
                        objParams[3] = new SqlParameter("@P_RELATION", objNominiFor.RELATION);
                        objParams[4] = new SqlParameter("@P_DOB", objNominiFor.DOB);
                        objParams[5] = new SqlParameter("@P_AGE", objNominiFor.AGE);
                        objParams[6] = new SqlParameter("@P_ADDRESS", objNominiFor.ADDRESS);
                        objParams[7] = new SqlParameter("@P_LAST", objNominiFor.LAST);
                        objParams[8] = new SqlParameter("@P_PER", objNominiFor.PER);
                        objParams[9] = new SqlParameter("@P_CONTING", objNominiFor.CONTING);
                        objParams[10] = new SqlParameter("@P_REMARK", objNominiFor.REMARK);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objNominiFor.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_ATTACHMENT", objNominiFor.ATTACHMENTS);
                        objParams[13] = new SqlParameter("@P_ADDRESS_FLAG", objNominiFor.ADDRESS_FLAG);
                        objParams[14] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[15] = new SqlParameter("@P_TALUKA", objNominiFor.TALUKA);
                        objParams[16] = new SqlParameter("@P_DISTRICT", objNominiFor.DISTRICT);
                        objParams[17] = new SqlParameter("@P_PINCODE", objNominiFor.PINCODE);
                        objParams[18] = new SqlParameter("@P_CITY", objNominiFor.CITY);
                        objParams[19] = new SqlParameter("@P_STATE", objNominiFor.STATE);
                        objParams[20] = new SqlParameter("@P_COUNTRY", objNominiFor.COUNTRY);
                        objParams[21] = new SqlParameter("@P_ISBLOB", objNominiFor.ISBLOB);
                        objParams[22] = new SqlParameter("@P_FILEPATH", objNominiFor.FILEPATH);
                        objParams[23] = new SqlParameter("@P_PAYROLL_SB_NOMINATION_DOCUMENT_UPLOAD", dt);
                        objParams[24] = new SqlParameter("@P_NFNO", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_NOMINIFOR", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        #region Edited on 15-11-2019
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_NOMINIFOR", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddNominiFor-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateNominiFor(ServiceBook objNominiFor, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_IDNO", objNominiFor.IDNO);
                        objParams[1] = new SqlParameter("@P_NTNO", objNominiFor.NTNO);
                        objParams[2] = new SqlParameter("@P_NAME", objNominiFor.NAME);
                        objParams[3] = new SqlParameter("@P_RELATION", objNominiFor.RELATION);
                        objParams[4] = new SqlParameter("@P_DOB", objNominiFor.DOB);
                        objParams[5] = new SqlParameter("@P_AGE", objNominiFor.AGE);
                        objParams[6] = new SqlParameter("@P_ADDRESS", objNominiFor.ADDRESS);
                        objParams[7] = new SqlParameter("@P_LAST", objNominiFor.LAST);
                        objParams[8] = new SqlParameter("@P_PER", objNominiFor.PER);
                        objParams[9] = new SqlParameter("@P_CONTING", objNominiFor.CONTING);
                        objParams[10] = new SqlParameter("@P_REMARK", objNominiFor.REMARK);
                        objParams[11] = new SqlParameter("@P_NFNO", objNominiFor.NFNO);
                        //objParams[12] = new SqlParameter("@P_ATTACHMENT", objNominiFor.ATTACHMENTS);
                        objParams[12] = new SqlParameter("@P_ADDRESS_FLAG", objNominiFor.ADDRESS_FLAG);
                        objParams[13] = new SqlParameter("@P_TALUKA", objNominiFor.TALUKA);
                        objParams[14] = new SqlParameter("@P_DISTRICT", objNominiFor.DISTRICT);
                        objParams[15] = new SqlParameter("@P_PINCODE", objNominiFor.PINCODE);
                        objParams[16] = new SqlParameter("@P_CITY", objNominiFor.CITY);
                        objParams[17] = new SqlParameter("@P_STATE", objNominiFor.STATE);
                        objParams[18] = new SqlParameter("@P_COUNTRY", objNominiFor.COUNTRY);
                        objParams[19] = new SqlParameter("@P_ISBLOB", objNominiFor.ISBLOB);
                        //objParams[21] = new SqlParameter("@P_FILEPATH", objNominiFor.FILEPATH);
                        objParams[20] = new SqlParameter("@P_PAYROLL_SB_NOMINATION_DOCUMENT_UPLOAD", dt);
                        #region Editeed on 15-11-2019

                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_NOMINIFOR", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_NOMINIFOR", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            if (Convert.ToInt32(ret) > 0)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateNominiFor-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteNominifor(int nfNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NFNO", nfNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_NOMINIFOR", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteNominifor.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllNominiDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_NOMINIFOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllNominiDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleNominiDetailsOfEmployee(int nfNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NFNO", nfNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_NOMINIFOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleNominiDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllNominiPendingCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_NOMINI_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllNominiPendingCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_PayRev

                public int AddPayRevision(ServiceBook objPayRev)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", objPayRev.IDNO);
                        objParams[1] = new SqlParameter("@P_SUBDESIGNO", objPayRev.SUBDESIGNO);
                        objParams[2] = new SqlParameter("@P_SCALENO", objPayRev.SCALENO);
                        if (!objPayRev.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objPayRev.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);

                        if (!objPayRev.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objPayRev.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);


                        objParams[5] = new SqlParameter("@P_TYPE", objPayRev.TYPE);
                        objParams[6] = new SqlParameter("@P_REMARK", objPayRev.REMARK);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objPayRev.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_ATTACHMENT", objPayRev.ATTACHMENTS);
                        objParams[9] = new SqlParameter("@P_MODIFIED_AMOUNT", objPayRev.AMOUNT);
                        objParams[10] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[11] = new SqlParameter("@P_BASIC", objPayRev.BASIC);
                        objParams[12] = new SqlParameter("@P_AGP", objPayRev.AGP);
                        objParams[13] = new SqlParameter("@P_HRA", objPayRev.HRA);
                        objParams[14] = new SqlParameter("@P_REVISEDPOST", objPayRev.REVISEDPOST);
                        objParams[15] = new SqlParameter("@P_GROSS", objPayRev.GROSS);
                        objParams[16] = new SqlParameter("@P_NET", objPayRev.NET);
                        objParams[17] = new SqlParameter("@P_FILEPATH", objPayRev.FILEPATH);
                        objParams[18] = new SqlParameter("@P_ISBLOB", objPayRev.ISBLOB);
                        objParams[19] = new SqlParameter("@P_PRNO", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_PAYREV", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        #region Edited on 15-11-2019
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_PAYREV", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }


                        #endregion


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPayRevision-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePayRevision(ServiceBook objPayRev)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_IDNO", objPayRev.IDNO);
                        objParams[1] = new SqlParameter("@P_SUBDESIGNO", objPayRev.SUBDESIGNO);
                        objParams[2] = new SqlParameter("@P_SCALENO", objPayRev.SCALENO);

                        if (!objPayRev.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objPayRev.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);

                        if (!objPayRev.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objPayRev.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_TYPE", objPayRev.TYPE);
                        objParams[6] = new SqlParameter("@P_REMARK", objPayRev.REMARK);
                        objParams[7] = new SqlParameter("@P_PRNO", objPayRev.PRNO);
                        objParams[8] = new SqlParameter("@P_ATTACHMENT", objPayRev.ATTACHMENTS);
                        objParams[9] = new SqlParameter("@P_MODIFIED_AMOUNT", objPayRev.AMOUNT);
                        objParams[10] = new SqlParameter("@P_BASIC", objPayRev.BASIC);
                        objParams[11] = new SqlParameter("@P_AGP", objPayRev.AGP);
                        objParams[12] = new SqlParameter("@P_HRA", objPayRev.HRA);
                        objParams[13] = new SqlParameter("@P_REVISEDPOST", objPayRev.REVISEDPOST);
                        objParams[14] = new SqlParameter("@P_GROSS", objPayRev.GROSS);
                        objParams[15] = new SqlParameter("@P_NET", objPayRev.NET);
                        objParams[16] = new SqlParameter("@P_ISBLOB", objPayRev.ISBLOB);
                        objParams[17] = new SqlParameter("@P_FILEPATH", objPayRev.FILEPATH);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PAYREV", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePayRevision-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //public int UpdatePayRevision(ServiceBook objPayRev)
                //{
                //    int retStatus = 0;

                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New File
                //        objParams = new SqlParameter[11];
                //        objParams[0] = new SqlParameter("@P_IDNO", objPayRev.IDNO);
                //        objParams[1] = new SqlParameter("@P_SUBDESIGNO", objPayRev.SUBDESIGNO);
                //        objParams[2] = new SqlParameter("@P_SCALENO", objPayRev.SCALENO);

                //        if (!objPayRev.FDT.Equals(DateTime.MinValue))
                //            objParams[3] = new SqlParameter("@P_FDT", objPayRev.FDT);
                //        else
                //            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);

                //        if (!objPayRev.TDT.Equals(DateTime.MinValue))
                //            objParams[4] = new SqlParameter("@P_TDT", objPayRev.TDT);
                //        else
                //            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);


                //        objParams[5] = new SqlParameter("@P_TYPE", objPayRev.TYPE);
                //        objParams[6] = new SqlParameter("@P_REMARK", objPayRev.REMARK);
                //        objParams[7] = new SqlParameter("@P_PRNO", objPayRev.PRNO);
                //        objParams[8] = new SqlParameter("@P_ATTACHMENT", objPayRev.ATTACHMENTS);
                //        objParams[9] = new SqlParameter("@P_MODIFIED_AMOUNT", objPayRev.AMOUNT);
                //        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PAYREV", objParams, false) != null)
                //        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //        #region Edited on 15-11-2019

                //        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[10].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PAYREV", objParams, false);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("-1"))
                //            {
                //                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                //            }
                //            else if (ret.ToString().Equals("1"))
                //            {
                //                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //            }
                //        }
                //        #endregion

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePayRevision-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}



                public int DeletePayRevision(int prNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PRNO", prNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_PAYREV", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeletePayRevision.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllPayRevisionOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PAYREV", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPayRevisionOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSinglePayRevisionOfEmployee(int prNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PRNO", prNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_PAYREV", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSinglePayRevisionOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllPayRevisionCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PAY_REVISION_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPayRevisionCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetTotExperience(DateTime frmdt, DateTime todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_TOT_EXPERIENCE_SB_PAYREV", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetTotExperience-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Payroll_Sb_PreService

                public int AddPreService(ServiceBook objPreService)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[35];
                        objParams[0] = new SqlParameter("@P_IDNO", objPreService.IDNO);

                        if (!objPreService.FDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FDT", objPreService.FDT);
                        else
                            objParams[1] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objPreService.TDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_TDT", objPreService.TDT);
                        else
                            objParams[2] = new SqlParameter("@P_TDT", DBNull.Value);

                        objParams[3] = new SqlParameter("@P_INST", objPreService.INST);
                        objParams[4] = new SqlParameter("@P_TERMINATION", objPreService.TERMINATION);
                        objParams[5] = new SqlParameter("@P_OFFICER", objPreService.OFFICER);
                        objParams[6] = new SqlParameter("@P_REMARK", objPreService.REMARK);
                        objParams[7] = new SqlParameter("@P_POST", objPreService.POSTNAME);


                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objPreService.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_ATTACHMENT", objPreService.ATTACHMENTS);
                        objParams[10] = new SqlParameter("@P_EXPERIENCE", objPreService.EXPERIENCE);

                        objParams[11] = new SqlParameter("@P_EXPERIENCETYPE", objPreService.EXPERIENCETYPE); // ADD ON 21-01-2021 
                        objParams[12] = new SqlParameter("@P_NAMEOFUNI", objPreService.NAMEOFUNI);
                        objParams[13] = new SqlParameter("@P_NATUREOFWORK", objPreService.NATUREOFWORK);
                        objParams[14] = new SqlParameter("@P_PAYSCALE", objPreService.PAYSCALE);
                        objParams[15] = new SqlParameter("@P_LASTSALARY", objPreService.LASTSALARY);
                        objParams[16] = new SqlParameter("@P_Natureofworktext", objPreService.NatureOfWorkText);
                        objParams[17] = new SqlParameter("@P_Department", objPreService.Department);
                        objParams[18] = new SqlParameter("@P_ADDRESS", objPreService.ADDRESS);
                        objParams[19] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[20] = new SqlParameter("@P_UNIVERSITYAPPNO", objPreService.UNIVERSITYAPPNO);
                        objParams[21] = new SqlParameter("@P_UNIAPPDT", objPreService.UNIAPPDT);
                        objParams[22] = new SqlParameter("@P_UNIVERSITYATACHMENT", objPreService.UNIVERSITYATACHMENT);
                        objParams[23] = new SqlParameter("@P_PGAPPNO", objPreService.PGAPPNO);
                        objParams[24] = new SqlParameter("@P_PGTAPPDT", objPreService.PGTAPPDT);
                        objParams[25] = new SqlParameter("@P_PGTATTACHMENT", objPreService.PGTATTACHMENT);
                        objParams[26] = new SqlParameter("@P_UNIAPPSTATUS", objPreService.UNIAPPSTATUS);
                        objParams[27] = new SqlParameter("@P_PGTAPPSTATUS", objPreService.PGTAPPSTATUS);
                        objParams[28] = new SqlParameter("@P_NAME", objPreService.NAME);
                        objParams[29] = new SqlParameter("@P_DESIGNATION", objPreService.DESIGNATION);
                        objParams[30] = new SqlParameter("@P_EMAILID", objPreService.EMAIL);
                        objParams[31] = new SqlParameter("@P_MOBNO", objPreService.MOBNO);
                        objParams[32] = new SqlParameter("@P_FILEPATH", objPreService.FILEPATH);
                        objParams[33] = new SqlParameter("@P_ISBLOB", objPreService.ISBLOB);
                        objParams[34] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[34].Direction = ParameterDirection.Output;
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_PRESERVICE", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_PRESERVICE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPreService-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePreService(ServiceBook objPreService)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[33];
                        objParams[0] = new SqlParameter("@P_IDNO", objPreService.IDNO);
                        if (!objPreService.FDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FDT", objPreService.FDT);
                        else
                            objParams[1] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objPreService.TDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_TDT", objPreService.TDT);
                        else
                            objParams[2] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[3] = new SqlParameter("@P_INST", objPreService.INST);
                        objParams[4] = new SqlParameter("@P_TERMINATION", objPreService.TERMINATION);
                        objParams[5] = new SqlParameter("@P_OFFICER", objPreService.OFFICER);
                        objParams[6] = new SqlParameter("@P_REMARK", objPreService.REMARK);
                        objParams[7] = new SqlParameter("@P_POST", objPreService.POSTNAME);
                        objParams[8] = new SqlParameter("@P_PSNO", objPreService.PSNO);
                        objParams[9] = new SqlParameter("@P_ATTACHMENT", objPreService.ATTACHMENTS);
                        objParams[10] = new SqlParameter("@P_EXPERIENCE", objPreService.EXPERIENCE);

                        objParams[11] = new SqlParameter("@P_EXPERIENCETYPE", objPreService.EXPERIENCETYPE);
                        objParams[12] = new SqlParameter("@P_NAMEOFUNI", objPreService.NAMEOFUNI);
                        objParams[13] = new SqlParameter("@P_NATUREOFWORK", objPreService.NATUREOFWORK);
                        objParams[14] = new SqlParameter("@P_PAYSCALE", objPreService.PAYSCALE);
                        objParams[15] = new SqlParameter("@P_LASTSALARY", objPreService.LASTSALARY);
                        objParams[16] = new SqlParameter("@P_Natureofworktext", objPreService.NatureOfWorkText);
                        objParams[17] = new SqlParameter("@P_Department", objPreService.Department);
                        objParams[18] = new SqlParameter("@P_ADDRESS", objPreService.ADDRESS);
                        objParams[19] = new SqlParameter("@P_UNIVERSITYAPPNO", objPreService.UNIVERSITYAPPNO);
                        objParams[20] = new SqlParameter("@P_UNIAPPDT", objPreService.UNIAPPDT);
                        objParams[21] = new SqlParameter("@P_UNIVERSITYATACHMENT", objPreService.UNIVERSITYATACHMENT);
                        objParams[22] = new SqlParameter("@P_PGAPPNO", objPreService.PGAPPNO);
                        objParams[23] = new SqlParameter("@P_PGTAPPDT", objPreService.PGTAPPDT);
                        objParams[24] = new SqlParameter("@P_PGTATTACHMENT", objPreService.PGTATTACHMENT);
                        objParams[25] = new SqlParameter("@P_UNIAPPSTATUS", objPreService.UNIAPPSTATUS);
                        objParams[26] = new SqlParameter("@P_PGTAPPSTATUS", objPreService.PGTAPPSTATUS);
                        objParams[27] = new SqlParameter("@P_NAME", objPreService.NAME);
                        objParams[28] = new SqlParameter("@P_DESIGNATION", objPreService.DESIGNATION);
                        objParams[29] = new SqlParameter("@P_EMAILID", objPreService.EMAIL);
                        objParams[30] = new SqlParameter("@P_MOBNO", objPreService.MOBNO);
                        objParams[31] = new SqlParameter("@P_FILEPATH", objPreService.FILEPATH);
                        objParams[32] = new SqlParameter("@P_ISBLOB", objPreService.ISBLOB);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PRESERVICE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePreService-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PRESERVICE", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //        #region edited by vijay andoju(15-11-2019)
                //        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[11].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PRESERVICE", objParams, true);
                //        if (ret != null)
                //        {
                //            if (ret.ToString().Equals("1"))
                //            {
                //                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //            }
                //            else
                //            {
                //                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                //            }

                //        }
                //        #endregion

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePreService-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int DeletePreService(int psNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PSNO", psNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_PRESERVICE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeletePreService.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllPreServiceDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PRESERVICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPreServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSinglePreServiceDetailsOfEmployee(int psNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PSNO", psNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_PRESERVICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSinglePreServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllPreServiceCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PREVIOUS_EXPERIENCE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPreServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_Quali

                public int AddQualification(ServiceBook objQuali)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_IDNO", objQuali.IDNO);
                        objParams[1] = new SqlParameter("@P_LEVEL_NO", objQuali.LNO);
                        objParams[2] = new SqlParameter("@P_EXAMNAME", objQuali.EXAMNAME);
                        objParams[3] = new SqlParameter("@P_QUALINO", objQuali.QUALINO);
                        objParams[4] = new SqlParameter("@P_INST", objQuali.INST);
                        objParams[5] = new SqlParameter("@P_UNIVERSITY", objQuali.UNIVERSITY_NAME);
                        objParams[6] = new SqlParameter("@P_LOCATION", objQuali.LOCATION);
                        objParams[7] = new SqlParameter("@P_PASSYEAR", objQuali.PASSYEAR);
                        objParams[8] = new SqlParameter("@P_REGNO", objQuali.REGNO);
                        objParams[9] = new SqlParameter("@P_SPECI", objQuali.SPECI);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objQuali.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_ATTACHMENT", objQuali.ATTACHMENTS);
                        objParams[12] = new SqlParameter("@P_STATUS", objQuali.QSTATUS);



                        if (!objQuali.REG_DATE.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_REG_DATE", objQuali.REG_DATE);
                        else
                            objParams[13] = new SqlParameter("@P_REG_DATE", DBNull.Value);

                        objParams[14] = new SqlParameter("@P_REG_COUNCIL_NAME", objQuali.REG_NAME);
                        objParams[15] = new SqlParameter("@P_IDCARDNO", objQuali.IDCARDNO);
                        objParams[16] = new SqlParameter("@P_UNITYPE", objQuali.UNITYPE);
                        objParams[17] = new SqlParameter("@P_INITYPE", objQuali.INITYPE);
                        objParams[18] = new SqlParameter("@P_PERCENTAGE", objQuali.Percentage);
                        objParams[19] = new SqlParameter("@P_GRADE", objQuali.Grade);
                        objParams[20] = new SqlParameter("@P_HIGHTSTQUL", objQuali.HIGHTESTQUL);
                        objParams[21] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[22] = new SqlParameter("@P_CGPA", objQuali.CGPA);
                        objParams[23] = new SqlParameter("@P_MONTH", objQuali.QMONTH);
                        objParams[24] = new SqlParameter("@P_ISBLOB", objQuali.ISBLOB);
                        objParams[25] = new SqlParameter("@P_FILEPATH", objQuali.FILEPATH);
                        objParams[26] = new SqlParameter("@P_QNO", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_QUALI", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_QUALI", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.ServiceBookController.AddQualification-> " + ex.ToString());
                    }
                    return retStatus;
                }





                public int UpdateQualification(ServiceBook objQuali)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[26];
                        objParams[0] = new SqlParameter("@P_IDNO", objQuali.IDNO);
                        objParams[1] = new SqlParameter("@P_LEVEL_NO", objQuali.LNO);
                        objParams[2] = new SqlParameter("@P_EXAMNAME", objQuali.EXAMNAME);
                        objParams[3] = new SqlParameter("@P_QUALINO", objQuali.QUALINO);
                        objParams[4] = new SqlParameter("@P_INST", objQuali.INST);
                        objParams[5] = new SqlParameter("@P_UNIVERSITY", objQuali.UNIVERSITY_NAME);
                        objParams[6] = new SqlParameter("@P_LOCATION", objQuali.LOCATION);
                        objParams[7] = new SqlParameter("@P_PASSYEAR", objQuali.PASSYEAR);
                        objParams[8] = new SqlParameter("@P_REGNO", objQuali.REGNO);
                        objParams[9] = new SqlParameter("@P_SPECI", objQuali.SPECI);
                        objParams[10] = new SqlParameter("@P_QNO", objQuali.QNO);
                        objParams[11] = new SqlParameter("@P_STATUS", objQuali.QSTATUS);
                        objParams[12] = new SqlParameter("@P_ATTACHMENT", objQuali.ATTACHMENTS);
                        if (!objQuali.REG_DATE.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_REG_DATE", objQuali.REG_DATE);
                        else
                            objParams[13] = new SqlParameter("@P_REG_DATE", DBNull.Value);
                        objParams[14] = new SqlParameter("@P_REG_COUNCIL_NAME", objQuali.REG_NAME);
                        objParams[15] = new SqlParameter("@P_IDCARDNO", objQuali.IDCARDNO);
                        objParams[16] = new SqlParameter("@P_UNITYPE", objQuali.UNITYPE);
                        objParams[17] = new SqlParameter("@P_INITYPE", objQuali.INITYPE);
                        objParams[18] = new SqlParameter("@P_PERCENTAGE", objQuali.Percentage);
                        objParams[19] = new SqlParameter("@P_GRADE", objQuali.Grade);
                        objParams[20] = new SqlParameter("@P_HIGHTSTQUL", objQuali.HIGHTESTQUL);
                        objParams[21] = new SqlParameter("@P_CGPA", objQuali.CGPA);
                        objParams[22] = new SqlParameter("@P_MONTH", objQuali.QMONTH);
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_QUALI", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        objParams[23] = new SqlParameter("@P_FILEPATH", objQuali.FILEPATH);
                        objParams[24] = new SqlParameter("@P_ISBLOB", objQuali.ISBLOB);
                        objParams[25] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[25].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_QUALI", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.ServiceBookController.UpdateQualification-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteQualification(int qNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QNO", qNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_QUALI", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteQualification.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllQualificationDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_QUALI", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllQualificationDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleQualificationDetailsOfEmployee(int qNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QNO", qNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_QUALI", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleQualificationDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllQualificationDetailsCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_QUALIFICATION_DETAILS_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllQualificationDetailsCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Payroll_Sb_Scale
                #endregion

                #region Payroll_Sb_ServiceBk

                public int AddServiceBk(ServiceBook objServiceBk)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_IDNO", objServiceBk.IDNO);
                        // objParams[1] = new SqlParameter("@P_DOJ", objServiceBk.DOJ);
                        // objParams[2] = new SqlParameter("@P_DOI", objServiceBk.DOI);
                        objParams[1] = new SqlParameter("@P_SUBDEPTNO", objServiceBk.SUBDEPTNO);
                        objParams[2] = new SqlParameter("@P_SUBDESIGNO", objServiceBk.SUBDESIGNO);
                        //objParams[5] = new SqlParameter("@P_DESIGNATURENO", objServiceBk.DESIGNATURENO);
                        //objParams[6] = new SqlParameter("@P_APPOINTNO", objServiceBk.APPOINTNO);
                        objParams[3] = new SqlParameter("@P_ORDERNO", objServiceBk.ORDERNO);

                        if (!objServiceBk.ORDERDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_ORDERDT", objServiceBk.ORDERDT);
                        else
                            objParams[4] = new SqlParameter("@P_ORDERDT", DBNull.Value);
                        if (!objServiceBk.ORDEFFDT.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_ORDEFFDT", objServiceBk.ORDEFFDT);
                        else
                            objParams[5] = new SqlParameter("@P_ORDEFFDT", DBNull.Value);

                        // objParams[6] = new SqlParameter("@P_GRNO", objServiceBk.GRNO);
                        // objParams[7] = new SqlParameter("@P_GRDT", objServiceBk.GRDT);
                        objParams[6] = new SqlParameter("@P_SCALENO", objServiceBk.SCALENO);
                        //objParams[13] = new SqlParameter("@P_OLDSCALENO	", objServiceBk.OLDSCALENO);
                        objParams[7] = new SqlParameter("@P_TYPETRANNO", objServiceBk.TYPETRANNO);

                        if (!objServiceBk.TERMIDT.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_TERMIDT", objServiceBk.TERMIDT);
                        else
                            objParams[8] = new SqlParameter("@P_TERMIDT", DBNull.Value);

                        objParams[9] = new SqlParameter("@P_SEQNO", objServiceBk.SEQNO);
                        //objParams[22] = new SqlParameter("@P_COMMENDT", objServiceBk.COMMENDT);
                        objParams[10] = new SqlParameter("@P_REMARK", objServiceBk.REMARK);
                        //objParams[24] = new SqlParameter("@P_COMMENREM", objServiceBk.COMMENREM);
                        objParams[11] = new SqlParameter("@P_PAYALLOW", objServiceBk.PAYALLOW);
                        //objParams[26] = new SqlParameter("@P_TERMIREASON", objServiceBk.TERMINATION);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objServiceBk.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_ATTACHMENT", objServiceBk.ATTACHMENTS);
                        objParams[14] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[15] = new SqlParameter("@P_FILEPATH", objServiceBk.FILEPATH);
                        objParams[16] = new SqlParameter("@P_ISBLOB", objServiceBk.ISBLOB);
                        objParams[17] = new SqlParameter("@P_TRNO", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_SERVICEBK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddServiceBk-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateServiceBk(ServiceBook objServiceBk)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IDNO", objServiceBk.IDNO);


                        objParams[1] = new SqlParameter("@P_SUBDEPTNO", objServiceBk.SUBDEPTNO);
                        objParams[2] = new SqlParameter("@P_SUBDESIGNO", objServiceBk.SUBDESIGNO);

                        objParams[3] = new SqlParameter("@P_ORDERNO", objServiceBk.ORDERNO);

                        if (!objServiceBk.ORDERDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_ORDERDT", objServiceBk.ORDERDT);
                        else
                            objParams[4] = new SqlParameter("@P_ORDERDT", DBNull.Value);
                        if (!objServiceBk.ORDEFFDT.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_ORDEFFDT", objServiceBk.ORDEFFDT);
                        else
                            objParams[5] = new SqlParameter("@P_ORDEFFDT", DBNull.Value);

                        objParams[6] = new SqlParameter("@P_SCALENO", objServiceBk.SCALENO);

                        objParams[7] = new SqlParameter("@P_TYPETRANNO", objServiceBk.TYPETRANNO);

                        if (!objServiceBk.TERMIDT.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_TERMIDT", objServiceBk.TERMIDT);
                        else
                            objParams[8] = new SqlParameter("@P_TERMIDT", DBNull.Value);
                        objParams[9] = new SqlParameter("@P_SEQNO", objServiceBk.SEQNO);

                        objParams[10] = new SqlParameter("@P_REMARK", objServiceBk.REMARK);

                        objParams[11] = new SqlParameter("@P_PAYALLOW", objServiceBk.PAYALLOW);

                        objParams[12] = new SqlParameter("@P_TRNO", objServiceBk.TRNO);
                        objParams[13] = new SqlParameter("@P_ATTACHMENT", objServiceBk.ATTACHMENTS);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_SERVICEBK", objParams, false) != null)
                        //  retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        objParams[14] = new SqlParameter("@P_ISBLOB", objServiceBk.ISBLOB);
                        objParams[15] = new SqlParameter("@P_FILEPATH", objServiceBk.FILEPATH);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_SERVICEBK", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateServiceBk-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UpdateServiceBk(ServiceBook objServiceBk)
                //{
                //    int retStatus = 0;

                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New File
                //        objParams = new SqlParameter[14];
                //        objParams[0] = new SqlParameter("@P_IDNO", objServiceBk.IDNO);


                //        objParams[1] = new SqlParameter("@P_SUBDEPTNO", objServiceBk.SUBDEPTNO);
                //        objParams[2] = new SqlParameter("@P_SUBDESIGNO", objServiceBk.SUBDESIGNO);

                //        objParams[3] = new SqlParameter("@P_ORDERNO", objServiceBk.ORDERNO);

                //        if (!objServiceBk.ORDERDT.Equals(DateTime.MinValue))
                //            objParams[4] = new SqlParameter("@P_ORDERDT", objServiceBk.ORDERDT);
                //        else
                //            objParams[4] = new SqlParameter("@P_ORDERDT", DBNull.Value);
                //        if (!objServiceBk.ORDEFFDT.Equals(DateTime.MinValue))
                //            objParams[5] = new SqlParameter("@P_ORDEFFDT", objServiceBk.ORDEFFDT);
                //        else
                //            objParams[5] = new SqlParameter("@P_ORDEFFDT", DBNull.Value);

                //        objParams[6] = new SqlParameter("@P_SCALENO", objServiceBk.SCALENO);

                //        objParams[7] = new SqlParameter("@P_TYPETRANNO", objServiceBk.TYPETRANNO);

                //        if (!objServiceBk.TERMIDT.Equals(DateTime.MinValue))
                //            objParams[8] = new SqlParameter("@P_TERMIDT", objServiceBk.TERMIDT);
                //        else
                //            objParams[8] = new SqlParameter("@P_TERMIDT", DBNull.Value);
                //        objParams[9] = new SqlParameter("@P_SEQNO", objServiceBk.SEQNO);

                //        objParams[10] = new SqlParameter("@P_REMARK", objServiceBk.REMARK);

                //        objParams[11] = new SqlParameter("@P_PAYALLOW", objServiceBk.PAYALLOW);

                //        objParams[12] = new SqlParameter("@P_TRNO", objServiceBk.TRNO);
                //        objParams[13] = new SqlParameter("@P_ATTACHMENT", objServiceBk.ATTACHMENTS);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_SERVICEBK", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateServiceBk-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int DeleteServiceBk(int trNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRNO", trNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_SERVICEBK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteServiceBk.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllServiceBookDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_SERVICEBK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllServiceBookDetailsOfEmployee> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleServiceBookDetailsOfEmployee(int trNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRNO", trNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_SERVICEBK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleServiceBookDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllServiceBookIncrementCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_INCREMENT_TERM_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllServiceBookIncrementCount> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Sb_Training
                public int AddTraining(ServiceBook objTraining)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_IDNO", objTraining.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objTraining.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objTraining.INST);

                        if (!objTraining.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objTraining.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objTraining.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objTraining.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_REMARK", objTraining.REMARK);
                        objParams[6] = new SqlParameter("P_COLLEGE_CODE", objTraining.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objTraining.ATTACHMENTS);

                        objParams[8] = new SqlParameter("@P_INSTLOCATION", objTraining.LOCATION);
                        objParams[9] = new SqlParameter("@P_SPONSORED_AMOUNT", objTraining.SPONSORED_AMOUNT);
                        objParams[10] = new SqlParameter("@P_ELIGIBLECANDIDATE", objTraining.EligibleCandidate);
                        objParams[11] = new SqlParameter("@P_SERVICECRITERIA", objTraining.fulfilservice);


                        objParams[12] = new SqlParameter("@P_PRESENTATION_DETAILS", objTraining.PRESENTATION_DETAILS);
                        objParams[13] = new SqlParameter("@P_DURATION", objTraining.DURATION);
                        objParams[14] = new SqlParameter("@P_SPONSORED_BY", objTraining.SPONSORED_BY);
                        objParams[15] = new SqlParameter("@P_COST_INVOLVED", objTraining.COST_INVOLVED);
                        objParams[16] = new SqlParameter("@P_PROGRAM_LEVEL", objTraining.PROGRAM_LEVEL);
                        objParams[17] = new SqlParameter("@P_PROGRAM_TYPE", objTraining.PROGRAM_TYPE);
                        objParams[18] = new SqlParameter("@P_PARTICIPATION_TYPE", objTraining.PARTICIPATION_TYPE);
                        objParams[19] = new SqlParameter("@P_Certification_Type", objTraining.CertificationType);
                        objParams[20] = new SqlParameter("@P_Theme_Of_Training", objTraining.ThemeOfTraining);
                        objParams[21] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[22] = new SqlParameter("@P_MODE", objTraining.MODE);
                        //objParams[23] = new SqlParameter("@P_AcadYear", objTraining.YEAR);
                        objParams[23] = new SqlParameter("@P_AcadYear", objTraining.PASSYEAR);
                        objParams[24] = new SqlParameter("@P_FILEPATH", objTraining.FILEPATH);
                        objParams[25] = new SqlParameter("@P_ISBLOB", objTraining.ISBLOB);

                        objParams[26] = new SqlParameter("@P_TNO", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_TRAINING", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        #region Chenged on 15-11-2019(vijay Andoju)

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_TRAINING", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateTraining(ServiceBook objTraining)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[26];
                        objParams[0] = new SqlParameter("@P_IDNO", objTraining.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objTraining.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objTraining.INST);
                        if (!objTraining.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objTraining.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objTraining.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objTraining.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objTraining.REMARK);
                        objParams[6] = new SqlParameter("@P_TNO", objTraining.TNO);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objTraining.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_INSTLOCATION", objTraining.LOCATION);
                        objParams[9] = new SqlParameter("@P_SPONSORED_AMOUNT", objTraining.SPONSORED_AMOUNT);
                        objParams[10] = new SqlParameter("@P_ELIGIBLECANDIDATE", objTraining.EligibleCandidate);
                        objParams[11] = new SqlParameter("@P_SERVICECRITERIA", objTraining.fulfilservice);


                        objParams[12] = new SqlParameter("@P_PRESENTATION_DETAILS", objTraining.PRESENTATION_DETAILS);
                        objParams[13] = new SqlParameter("@P_DURATION", objTraining.DURATION);
                        objParams[14] = new SqlParameter("@P_SPONSORED_BY", objTraining.SPONSORED_BY);
                        objParams[15] = new SqlParameter("@P_COST_INVOLVED", objTraining.COST_INVOLVED);
                        objParams[16] = new SqlParameter("@P_PROGRAM_LEVEL", objTraining.PROGRAM_LEVEL);
                        objParams[17] = new SqlParameter("@P_PROGRAM_TYPE", objTraining.PROGRAM_TYPE);
                        objParams[18] = new SqlParameter("@P_PARTICIPATION_TYPE", objTraining.PARTICIPATION_TYPE);
                        objParams[19] = new SqlParameter("@P_Certification_Type", objTraining.CertificationType);
                        objParams[20] = new SqlParameter("@P_Theme_Of_Training", objTraining.ThemeOfTraining);
                        objParams[21] = new SqlParameter("@P_MODE", objTraining.MODE);
                        //objParams[22] = new SqlParameter("@P_AcadYear", objTraining.YEAR);
                        objParams[22] = new SqlParameter("@P_AcadYear", objTraining.PASSYEAR);
                        objParams[23] = new SqlParameter("@P_FILEPATH", objTraining.FILEPATH);
                        objParams[24] = new SqlParameter("@P_ISBLOB", objTraining.ISBLOB);
                        objParams[25] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[25].Direction = ParameterDirection.Output;


                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_TRAINING", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_TRAINING", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteTraining(int tNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", tNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_TRAINING", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteTraining.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllTrainingDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_TRAINING", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllTrainingDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleTrainingDetailsOfEmployee(int tNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", tNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_TRAINING", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleTrainingDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllTrainingDetailsCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB__TRAINING_ATTENDED_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllTrainingDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Payroll_Sb_Training_Conducted

                public int AddTrainingConducted(ServiceBook objTraining)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objTraining.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objTraining.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objTraining.INST);
                        if (!objTraining.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objTraining.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objTraining.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objTraining.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objTraining.REMARK);
                        objParams[6] = new SqlParameter("P_COLLEGE_CODE", objTraining.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objTraining.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                        objParams[9] = new SqlParameter("@P_TNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_TRAINING_CONDUCTED", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_TRAINING_CONDUCTED", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddTrainingConducted-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateTrainingConducted(ServiceBook objTraining)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objTraining.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objTraining.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objTraining.INST);
                        if (!objTraining.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objTraining.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objTraining.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objTraining.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objTraining.REMARK);
                        objParams[6] = new SqlParameter("@P_TNO", objTraining.TNO);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objTraining.ATTACHMENTS);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_TRAINING_CONDUCTED", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        #region checking duplicate (EDITED ON 15-11-2019)

                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_TRAINING_CONDUCTED", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateTrainingConducted-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteTrainingConducted(int tNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", tNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_TRAINING_CONDUCTED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteTrainingConducted.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllTrainingConductedDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_TRAINING_CONDUCTED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllTrainingConductedDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleTrainingConductedDetailsOfEmployee(int tNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", tNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_TRAINING_CONDUCTED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleTrainingConductedDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllTrainingConductedCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_TRAINING_CONDUCTED_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllTrainingConductedDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Payroll_LeaveTran

                public int AddLeaveTran(ServiceBook objLeaveTran)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_IDNO", objLeaveTran.IDNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaveTran.LNO);
                        objParams[2] = new SqlParameter("@P_APPDT", objLeaveTran.APPDT);
                        objParams[3] = new SqlParameter("@P_FDT", objLeaveTran.FDT);
                        objParams[4] = new SqlParameter("@P_TDT", objLeaveTran.TDT);
                        objParams[5] = new SqlParameter("@P_LEAVES", objLeaveTran.LEAVES);
                        objParams[6] = new SqlParameter("@P_ORDNO", objLeaveTran.ORDNO);
                        objParams[7] = new SqlParameter("@P_YEAR", objLeaveTran.YEAR);
                        objParams[8] = new SqlParameter("@P_ST", objLeaveTran.ST);
                        //  objParams[9] = new SqlParameter("@P_END_DT", objLeaveTran.END_DT);
                        objParams[9] = new SqlParameter("@P_JOINDT", objLeaveTran.JOINDT);
                        objParams[10] = new SqlParameter("@P_FIT", objLeaveTran.FIT);
                        objParams[11] = new SqlParameter("@P_UNFIT", objLeaveTran.UNFIT);
                        objParams[12] = new SqlParameter("@P_PERIOD", objLeaveTran.PERIOD);
                        //  objParams[14] = new SqlParameter("@P_FNAN", objLeaveTran.FNAN);
                        objParams[13] = new SqlParameter("@P_REMARK", objLeaveTran.REMARK);
                        objParams[14] = new SqlParameter("P_COLLEGE_CODE", objLeaveTran.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_ENO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_PAY_LEAVETRAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddLeaveTran-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateLeaveTran(ServiceBook objLeaveTran)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", objLeaveTran.IDNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaveTran.LNO);
                        objParams[2] = new SqlParameter("@P_APPDT", objLeaveTran.APPDT);
                        objParams[3] = new SqlParameter("@P_FDT", objLeaveTran.FDT);
                        objParams[4] = new SqlParameter("@P_TDT", objLeaveTran.TDT);
                        objParams[5] = new SqlParameter("@P_LEAVES", objLeaveTran.LEAVES);
                        objParams[6] = new SqlParameter("@P_ORDNO", objLeaveTran.ORDNO);
                        objParams[7] = new SqlParameter("@P_YEAR", objLeaveTran.YEAR);
                        objParams[8] = new SqlParameter("@P_ST", objLeaveTran.ST);
                        //objParams[9] = new SqlParameter("@P_END_DT", objLeaveTran.END_DT);
                        objParams[9] = new SqlParameter("@P_JOINDT", objLeaveTran.JOINDT);
                        objParams[10] = new SqlParameter("@P_FIT", objLeaveTran.FIT);
                        objParams[11] = new SqlParameter("@P_UNFIT", objLeaveTran.UNFIT);
                        objParams[12] = new SqlParameter("@P_PERIOD", objLeaveTran.PERIOD);
                        // objParams[14] = new SqlParameter("P_FNAN", objLeaveTran.FNAN);
                        objParams[13] = new SqlParameter("@P_REMARK", objLeaveTran.REMARK);
                        objParams[14] = new SqlParameter("@P_ENO", objLeaveTran.ENO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAY_LEAVETRAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateLeaveTran-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteLeaveTran(int eNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ENO", eNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_PAY_LEAVETRAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteLeaveTran.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllLeaveDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_LEAVETRAN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllLeaveDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleLeaveDetailsOfEmployee(int eNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ENO", eNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_LEAVETRAN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleLeaveDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_PersonalMemorandam

                public int UpdatePersonalMemorandam(EmpMaster objEmpMas, string mothername)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[45];
                        objParams[0] = new SqlParameter("@P_IDNO", objEmpMas.IDNO);
                        objParams[1] = new SqlParameter("@P_FATHERNAME", objEmpMas.FATHERNAME);
                        // objParams[2] = new SqlParameter("@P_DOB", objEmpMas.DOB);
                        if (!objEmpMas.DOB.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_DOB", objEmpMas.DOB);
                        else
                            objParams[2] = new SqlParameter("@P_DOB", DBNull.Value);
                        if (!objEmpMas.DOJ.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_DOJ", objEmpMas.DOJ);
                        else
                            objParams[3] = new SqlParameter("@P_DOJ", DBNull.Value);

                        objParams[4] = new SqlParameter("@P_HEIGHT", objEmpMas.HEIGHT);
                        objParams[5] = new SqlParameter("@P_IDMARK1", objEmpMas.IDMARK1);
                        objParams[6] = new SqlParameter("@P_IDMARK2", objEmpMas.IDMARK2);
                        objParams[7] = new SqlParameter("@P_RESADD1", objEmpMas.RESADD1);
                        objParams[8] = new SqlParameter("@P_TOWNADD1", objEmpMas.TOWNADD1);
                        objParams[9] = new SqlParameter("@P_PHONENO", objEmpMas.PHONENO);
                        objParams[10] = new SqlParameter("@P_EMAILID", objEmpMas.EMAILID);
                        objParams[11] = new SqlParameter("@P_CASTENO", objEmpMas.CASTENO);
                        objParams[12] = new SqlParameter("@P_CATEGORYNO", objEmpMas.CATEGORYNO);
                        objParams[13] = new SqlParameter("@P_RELIGIONNO", objEmpMas.RELIGIONNO);
                        objParams[14] = new SqlParameter("@P_NATIONALITYNO", objEmpMas.NATIONALITYNO);
                        objParams[15] = new SqlParameter("@P_STNO", objEmpMas.STNO);
                        objParams[16] = new SqlParameter("@P_ACATNO", objEmpMas.ACATNO);
                        objParams[17] = new SqlParameter("@P_AUTHENREF", objEmpMas.AUTHENREF);
                        objParams[18] = new SqlParameter("@P_ANFN", objEmpMas.ANFN);
                        objParams[19] = new SqlParameter("@P_MOTHERNAME", mothername);
                        if (!objEmpMas.RDT.Equals(DateTime.MinValue))
                            objParams[20] = new SqlParameter("@P_RDT", objEmpMas.RDT);
                        else
                            objParams[20] = new SqlParameter("@P_RDT", DBNull.Value);
                        objParams[21] = new SqlParameter("@P_STATUSNO", objEmpMas.STATUSNO);
                        if (!objEmpMas.STDATE.Equals(DateTime.MinValue))
                            objParams[22] = new SqlParameter("@P_STDATE", objEmpMas.STDATE);
                        else
                            objParams[22] = new SqlParameter("@P_STDATE", DBNull.Value);

                        objParams[23] = new SqlParameter("@P_BLOODGRPNO", objEmpMas.BLOODGRPNO);
                        objParams[24] = new SqlParameter("@P_PAN_NO", objEmpMas.PAN_NO);
                        objParams[25] = new SqlParameter("@P_ADHARNO", objEmpMas.ADHAR);
                        objParams[26] = new SqlParameter("@P_ALTERNATEMOBNO", objEmpMas.ALTERNATEPHONENO);
                        objParams[27] = new SqlParameter("@P_PASSPORTNO", objEmpMas.PASSPORT);
                        objParams[28] = new SqlParameter("@P_ALTERNATE_EMAILID", objEmpMas.ALTERNATEEMAILID);
                        objParams[29] = new SqlParameter("@P_TITLE", objEmpMas.TITLE);
                        //objParams[30] = new SqlParameter("@P_SUBDEPTNO", objEmpMas.SUBDESIGNO);
                        //objParams[31] = new SqlParameter("@P_SUBDESIGNO", objEmpMas.SUBDEPTNO);
                        //newly added on 20/05/2022
                        objParams[30] = new SqlParameter("@P_SUBDEPTNO", objEmpMas.SUBDEPTNO);
                        objParams[31] = new SqlParameter("@P_SUBDESIGNO", objEmpMas.SUBDESIGNO);
                        objParams[32] = new SqlParameter("@P_AICTE_NO", objEmpMas.AICTE_NO);
                        //
                        //objParams[20] = new SqlParameter("@P_BLOODGROUPNO", objEmpMas.BLOODGROUPNO);
                        //objParams[21] = new SqlParameter("@P_STATENO", objEmpMas.STATENO);
                        //objParams[22] = new SqlParameter("@P_DISTRICTNO", objEmpMas.DISTRICTNO);
                        //objParams[23] = new SqlParameter("@P_GRADE", objEmpMas.GRADE);
                        //objParams[24] = new SqlParameter("@P_CONFIDENTIAL_GRADE", objEmpMas.CONFIDENTIAL_GRADE);
                        objParams[33] = new SqlParameter("@P_TALUKA", objEmpMas.TALUKA);
                        objParams[34] = new SqlParameter("@P_DISTRICT", objEmpMas.DISTRICT);
                        objParams[35] = new SqlParameter("@P_PINCODE", objEmpMas.PINCODE);
                        objParams[36] = new SqlParameter("@P_CITY", objEmpMas.CITY);
                        objParams[37] = new SqlParameter("@P_STATE", objEmpMas.STATE);
                        objParams[38] = new SqlParameter("@P_COUNTRY", objEmpMas.COUNTRY);
                        objParams[39] = new SqlParameter("@P_FNAME", objEmpMas.FNAME);
                        objParams[40] = new SqlParameter("@P_MNAME", objEmpMas.MNAME);
                        objParams[41] = new SqlParameter("@P_LNAME", objEmpMas.LNAME);
                        objParams[42] = new SqlParameter("@P_UA_NO", objEmpMas.UA_NO);
                        objParams[43] = new SqlParameter("@P_COUNTRYNO", objEmpMas.COUNTRYNO);
                        objParams[44] = new SqlParameter("@P_STATENO", objEmpMas.STATENO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PERSONALMEMORADAM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateDeptExam-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSingleEmployeeDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SB_SP_RET_EMPLOYEE_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllDeptExamDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Payroll_Emp_Image

                public int AddEmpImage(ServiceBook objEmpImage)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", objEmpImage.IDNO);
                        objParams[1] = new SqlParameter("@P_IMAGEID", objEmpImage.imageid);
                        objParams[2] = new SqlParameter("@P_IMAGETYPE", objEmpImage.imagetype);
                        objParams[3] = new SqlParameter("@P_EMPIMAGE", objEmpImage.empimage);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objEmpImage.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                        objParams[6] = new SqlParameter("@P_IMAGETRXID ", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_EMP_IMAGE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddEmpImage-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateEmpImage(ServiceBook objEmpImage)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", objEmpImage.IDNO);
                        objParams[1] = new SqlParameter("@P_IMAGEID", objEmpImage.imageid);
                        objParams[2] = new SqlParameter("@P_IMAGETYPE", objEmpImage.imagetype);
                        objParams[3] = new SqlParameter("@P_EMPIMAGE", objEmpImage.empimage);
                        objParams[4] = new SqlParameter("@P_IMAGETRXID ", objEmpImage.imagetrxid);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_EMP_IMAGE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateEmpImage-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteEmpImage(int imageTrxId)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IMAGETRXID ", imageTrxId);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_EMP_IMAGE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllEmpImageDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMP_IMAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllEmpImageDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleEmpImageDetails(int imageTrxId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IMAGETRXID ", imageTrxId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_EMP_IMAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleEmpImageDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region PAYROLL_SB_PUBLICATION_DETAILS

                public int AddUpdPublicationDetails(ServiceBook objPubDel, DataTable SB_AUTHORLIST_RECORD)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[39];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_PUBLICATION_TYPE", objPubDel.PUBLICATION_TYPE);
                        objParams[2] = new SqlParameter("@P_TITLE", objPubDel.TITLE);
                        objParams[3] = new SqlParameter("@P_SUBJECT", objPubDel.SUBJECT);

                        if (!objPubDel.PUBLICATIONDATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", objPubDel.PUBLICATIONDATE);
                        else
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_DETAILS", objPubDel.DETAILS);
                        objParams[6] = new SqlParameter("@P_SPONSORED_AMOUNT", objPubDel.SPONSORED_AMOUNT);

                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objPubDel.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_NAME", objPubDel.CONFERENCE_NAME);
                        objParams[9] = new SqlParameter("@P_ORGANISOR", objPubDel.ORGANISOR);
                        objParams[10] = new SqlParameter("@P_PAGENO", objPubDel.PAGENO);
                        objParams[11] = new SqlParameter("@P_PUBLICATION", objPubDel.PUBLICATION);
                        objParams[12] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[13] = new SqlParameter("@P_ISBN", objPubDel.ISBN);
                        objParams[14] = new SqlParameter("@P_VolumeNo", objPubDel.VOLUME_NO);
                        objParams[15] = new SqlParameter("@P_IssueNo", objPubDel.ISSUE_NO);
                        objParams[16] = new SqlParameter("@P_Publication_Status", objPubDel.PUB_STATUS);
                        objParams[17] = new SqlParameter("@P_Year", objPubDel.YEAR);
                        objParams[18] = new SqlParameter("@p_Location", objPubDel.LOCATION);
                        objParams[19] = new SqlParameter("@P_Publisher", objPubDel.PUBLISHER);
                        objParams[20] = new SqlParameter("@P_IsConference", objPubDel.IS_CONFERENCE);
                        objParams[21] = new SqlParameter("@SB_AUTHORLIST_RECORD", SB_AUTHORLIST_RECORD);
                        objParams[22] = new SqlParameter("@P_PUBTRXNO", objPubDel.PUBTRXNO);
                        objParams[23] = new SqlParameter("@P_IsJournalScopus", objPubDel.IsJournalScopus);
                        //IsJournalScopus
                        objParams[24] = new SqlParameter("@P_IMPACTFACTORS", objPubDel.IMPACTFACTORS);
                        objParams[25] = new SqlParameter("@P_CITATIONINDEX", objPubDel.CITATIONINDEX);
                        objParams[26] = new SqlParameter("@P_EISSN", objPubDel.EISSN);
                        objParams[27] = new SqlParameter("@P_PUB_ADD", objPubDel.PUB_ADD);

                        #region Add on 13-01-20 By Sonali Ambedare
                        objParams[28] = new SqlParameter("@P_Month", objPubDel.MONTH);
                        objParams[29] = new SqlParameter("@P_DOINO", objPubDel.DOIN);
                        objParams[30] = new SqlParameter("@P_INDEXING_TYPE", objPubDel.INDEXING_TYPE);

                        #endregion

                        #region Add on 26-08-2020 By Sonali Ambedare
                        objParams[31] = new SqlParameter("@WEB_LINK", objPubDel.WEBLINK);
                        #endregion

                        objParams[32] = new SqlParameter("@P_IndexingFactors", objPubDel.IndexingFactors);
                        objParams[33] = new SqlParameter("@P_IndexingFactorValue", objPubDel.IndexingFactorValue);
                        objParams[34] = new SqlParameter("@P_IndexingDATE", objPubDel.IndexingDATE);
                        objParams[35] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[36] = new SqlParameter("@P_FILEPATH", objPubDel.FILEPATH);
                        objParams[37] = new SqlParameter("@P_ISBLOB", objPubDel.ISBLOB);

                        objParams[38] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[38].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_PUBLICATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                        //if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_PUBLICATION_DETAILS", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddPublicationDetails(ServiceBook objPubDel)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[1] = new SqlParameter("@P_PUBLICATION_TYPE", objPubDel.PUBLICATION_TYPE);
                        objParams[2] = new SqlParameter("@P_TITLE", objPubDel.TITLE);
                        objParams[3] = new SqlParameter("@P_SUBJECT", objPubDel.SUBJECT);

                        if (!objPubDel.PUBLICATIONDATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", objPubDel.PUBLICATIONDATE);
                        else
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", DBNull.Value);


                        objParams[5] = new SqlParameter("@P_DETAILS", objPubDel.DETAILS);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objPubDel.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_AUTHOR2", objPubDel.AUTHOR2);
                        objParams[8] = new SqlParameter("@P_AUTHOR3", objPubDel.AUTHOR3);
                        objParams[9] = new SqlParameter("@P_NAME", objPubDel.CONFERENCE_NAME);
                        objParams[10] = new SqlParameter("@P_ORGANISOR", objPubDel.ORGANISOR);
                        objParams[11] = new SqlParameter("@P_PAGENO", objPubDel.PAGENO);
                        objParams[12] = new SqlParameter("@P_PUBLICATION", objPubDel.PUBLICATION);
                        objParams[13] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[14] = new SqlParameter("@P_AUTHOR4", objPubDel.AUTHOR4);
                        objParams[15] = new SqlParameter("@P_ISBN", objPubDel.ISBN);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_PUBLICATION_DETAILS", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_PUBLICATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePublicationDetails(ServiceBook objPubDel)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        // objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IDNO);
                        objParams[0] = new SqlParameter("@P_PUBLICATION_TYPE", objPubDel.PUBLICATION_TYPE);
                        objParams[1] = new SqlParameter("@P_TITLE", objPubDel.TITLE);
                        objParams[2] = new SqlParameter("@P_SUBJECT", objPubDel.SUBJECT);

                        if (!objPubDel.PUBLICATIONDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_PUBLICATIONDATE", objPubDel.PUBLICATIONDATE);
                        else
                            objParams[3] = new SqlParameter("@P_PUBLICATIONDATE", DBNull.Value);

                        objParams[4] = new SqlParameter("@P_DETAILS", objPubDel.DETAILS);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objPubDel.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[7] = new SqlParameter("@P_AUTHOR4", objPubDel.AUTHOR4);
                        objParams[8] = new SqlParameter("@P_ISBN", objPubDel.ISBN);
                        objParams[9] = new SqlParameter("@P_AUTHOR2", objPubDel.AUTHOR2);
                        objParams[10] = new SqlParameter("@P_AUTHOR3", objPubDel.AUTHOR3);
                        objParams[11] = new SqlParameter("@P_PUBTRXNO", objPubDel.PUBTRXNO);


                        //  if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_UPD_PUBLICATION_DETAILS", objParams, false) != null)
                        //      retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        #region chenged on 15-11-2019( Andoju vijay)
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_UPD_PUBLICATION_DETAILS", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                        #endregion



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeletePublicationDetails(int pubTrxno)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PUBTRXNO", pubTrxno);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_DEL_PUBLICATION_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllPublicationDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETALL_PUBLICATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSinglePublicationDetails(int pubTrxno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PUBTRXNO", pubTrxno);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETSINGLE_PUBLICATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSinglePublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllPublicationDetailsCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PUBLICATION_DETAILS_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region PAYROLL_SB_ADMIN_RESPONSIBILITIES

                public int AddAdminResponsibilities(ServiceBook objAdminRespon)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objAdminRespon.IDNO);
                        objParams[1] = new SqlParameter("@P_RESPONSIBILITY", objAdminRespon.RESPONSIBILITY);
                        objParams[2] = new SqlParameter("@P_ORGANIZATION", objAdminRespon.ORGANIZATION);

                        if (!objAdminRespon.FROMDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROMDATE", objAdminRespon.FROMDATE);
                        else
                            objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objAdminRespon.TODATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TODATE", objAdminRespon.TODATE);
                        else
                            objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);


                        objParams[5] = new SqlParameter("@P_REMARK", objAdminRespon.REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objAdminRespon.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAdminRespon.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));



                        // if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_ADMIN_RESPONSIBILITIES", objParams, false) != null)
                        //     retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        objParams[9] = new SqlParameter("@P_Is_Current", objAdminRespon.IsCurrent);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objAdminRespon.FILEPATH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objAdminRespon.ISBLOB);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_ADMIN_RESPONSIBILITIES", objParams, false);
                        {
                            if (ret != null)
                            {
                                if (ret.ToString().Equals("-1"))
                                {
                                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                                }
                                else if (ret.ToString().Equals("1"))
                                {
                                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddAdminResponsibilities-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAdminResponsibilities(ServiceBook objAdminRespon)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_ADMINTRXNO", objAdminRespon.ADMINTRXNO);
                        objParams[1] = new SqlParameter("@P_RESPONSIBILITY", objAdminRespon.RESPONSIBILITY);
                        objParams[2] = new SqlParameter("@P_ORGANIZATION", objAdminRespon.ORGANIZATION);
                        if (!objAdminRespon.FROMDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROMDATE", objAdminRespon.FROMDATE);
                        else
                            objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objAdminRespon.TODATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TODATE", objAdminRespon.TODATE);
                        else
                            objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_REMARK", objAdminRespon.REMARK);

                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objAdminRespon.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAdminRespon.ATTACHMENTS);
                        //f (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_UPD_ADMIN_RESPONSIBILITIES", objParams, false) != null)
                        //   retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                        objParams[8] = new SqlParameter("@P_IDNO", objAdminRespon.IDNO);
                        objParams[9] = new SqlParameter("@P_Is_Current", objAdminRespon.IsCurrent);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objAdminRespon.FILEPATH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objAdminRespon.ISBLOB);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_UPD_ADMIN_RESPONSIBILITIES", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateAdminResponsibilities-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteAdminResponsibilities(int adminTrxno)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMINTRXNO", adminTrxno);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_DEL_ADMIN_RESPONSIBILITIES", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteAdminResponsibilities.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAdminResponsibilities(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETALL_ADMIN_RESPONSIBILITIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAdminResponsibilities-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleAdminResponsibilities(int adminTrxno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMINTRXNO", adminTrxno);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETSINGLE_ADMIN_RESPONSIBILITIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleAdminResponsibilities-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllAdminResponsibilitiesCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_ADMIN_RESPONSIBILITIES_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAdminResponsibilities-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region PAYROLL_SB_INVITED_TALK

                public int AddInvitedTalk(ServiceBook objInvTalk)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objInvTalk.IDNO);
                        objParams[1] = new SqlParameter("@P_SUBJECT", objInvTalk.SUBJECT);
                        objParams[2] = new SqlParameter("@P_VENU", objInvTalk.VENU);
                        objParams[3] = new SqlParameter("@P_DURATION", objInvTalk.DURATION);


                        if (!objInvTalk.DATEOFTALK.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_DATEOFTALK", objInvTalk.DATEOFTALK);
                        else
                            objParams[4] = new SqlParameter("@P_DATEOFTALK", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_REMARK", objInvTalk.REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objInvTalk.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objInvTalk.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[9] = new SqlParameter("@P_MODE", objInvTalk.MODE);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objInvTalk.FILEPATH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objInvTalk.ISBLOB);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_INVITED_TALK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_INVITED_TALK", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateInvitedTalk(ServiceBook objInvTalk)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_INVTRXNO", objInvTalk.INVTRXNO);
                        objParams[1] = new SqlParameter("@P_SUBJECT", objInvTalk.SUBJECT);
                        objParams[2] = new SqlParameter("@P_VENU", objInvTalk.VENU);
                        objParams[3] = new SqlParameter("@P_DURATION", objInvTalk.DURATION);
                        if (!objInvTalk.DATEOFTALK.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_DATEOFTALK", objInvTalk.DATEOFTALK);
                        else
                            objParams[4] = new SqlParameter("@P_DATEOFTALK", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objInvTalk.REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objInvTalk.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objInvTalk.ATTACHMENTS);
                        //if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPD_SB_INVITED_TALK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        objParams[8] = new SqlParameter("@P_IDNO", objInvTalk.IDNO);
                        objParams[9] = new SqlParameter("@P_MODE", objInvTalk.MODE);
                        objParams[10] = new SqlParameter("@P_ISBLOB", objInvTalk.ISBLOB);
                        objParams[11] = new SqlParameter("@P_FILEPATH", objInvTalk.FILEPATH);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPD_SB_INVITED_TALK", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist); ;
                            }
                            else if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteInvitedTalk(int invTrxno)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRXNO", invTrxno);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_DEL_SB_INVITED_TALK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllInvitedTalk(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETALL_SB_INVITED_TALK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleInvitedTalk(int invTrxno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRXNO", invTrxno);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_INVITED_TALK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllInvitedTalkCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_INVITED_TALK_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllInvitedTalkCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Upload Files in Folder

                public void upload_new_files(string folder, int idno, string primary, string table, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    //Upload the File
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        int newfilename = Convert.ToInt32(objcommon.LookUp(table, "max(" + primary + ")", ""));
                        string uploadFile = initials + newfilename + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        //fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }
                }

                public void update_upload(string folder, int trxno, string lastfilename, int idno, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    //Upload the File
                    string uploadPath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        //Update Assignment
                        string oldFileName = string.Empty;

                        oldFileName = initials + trxno + System.IO.Path.GetExtension(lastfilename);

                        if (System.IO.File.Exists(uploadPath + oldFileName))
                        {
                            System.IO.File.Delete(uploadPath + oldFileName);
                        }

                        string uploadFile = initials + trxno + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }

                }
                #endregion

                #region Employee Photo & Sign

                public int AddEmployeePhotoSign(ServiceBook objSevBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_IDNO", objSevBook.IDNO);

                        if (objSevBook.Photo == null)
                            objParams[1] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        else
                            objParams[1] = new SqlParameter("@P_PHOTO", objSevBook.Photo);

                        objParams[1].SqlDbType = SqlDbType.Image;


                        if (objSevBook.Photo == null)
                            objParams[2] = new SqlParameter("@P_PHOTOSign", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_PHOTOSign", objSevBook.PhotoSign);

                        objParams[2].SqlDbType = SqlDbType.Image;

                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SB_EMPLOYEE_PHOTO_SIGN_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {

                    }
                    return retStatus;
                }

                public DataSet GetAllEmpPhotoSign(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMPLOYEE_PHOTO_SIGN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllEmpImageDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                public DataSet GetAddressOfEmployee(int _idnoEmp)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", _idnoEmp);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_RET_EMPLOYEE_ADDRESS_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAddressOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public DataSet GetAllMISOTHERDetail(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETALL_MIS_OTHERDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAdminResponsibilities-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public int AddMiscellaneousOtherdetail(ServiceBook objmisdetail, DataTable dtIndex, DataTable dtBond, DataTable dtPhd, DataTable dtId, DataTable dtTh)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objmisdetail.IDNO);

                        //objParams[1] = new SqlParameter("@P_HIndex", objmisdetail.HIndex);
                        //objParams[2] = new SqlParameter("@P_BOND", objmisdetail.Bond);

                        //if (!objmisdetail.FDT.Equals(DateTime.MinValue))
                        //    objParams[3] = new SqlParameter("@P_FROMDATE", objmisdetail.FROMDATE);
                        //else
                        //    objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);
                        //if (!objmisdetail.TDT.Equals(DateTime.MinValue))
                        //    objParams[4] = new SqlParameter("@P_TODATE", objmisdetail.TODATE);
                        //else
                        //    objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);
                        //objParams[1] = new SqlParameter("@P_PHDGUIDED", objmisdetail.PHDGUIDED); s
                        //objParams[2] = new SqlParameter("@P_PHDAWARD", objmisdetail.PHDAWARD); s
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objmisdetail.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_ESTB_SB_MISCE_INDEX_FACTOR_DETAIL", dtIndex);
                        objParams[3] = new SqlParameter("@P_ESTB_SB_MISCE_BOND_DETAIL", dtBond);

                        objParams[4] = new SqlParameter("@P_ATTACHMENT", objmisdetail.ATTACHMENTS);
                        objParams[5] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        //objParams[8] = new SqlParameter("@P_CANDIDATENAME",objmisdetail.NAME);
                        //objParams[9] = new SqlParameter("@P_REGDATE",objmisdetail.DATE);
                        //objParams[10] = new SqlParameter("@P_UNIVERSITYNAME",objmisdetail.UNIVERSITY_NAME);
                        //objParams[11] = new SqlParameter("@P_RESEARCHNAME",objmisdetail.RESEARCHNAME);
                        //objParams[12] = new SqlParameter("@P_GUIDENAME",objmisdetail.GUIDENAME);
                        //objParams[13] = new SqlParameter("@P_PUBLICATIONPHDNO",objmisdetail.PUBLICATIONPHDNO);
                        //objParams[14] = new SqlParameter("@P_PHDGRANT",objmisdetail.PHDGRANT);
                        //objParams[15] = new SqlParameter("@P_PHDPATENT", objmisdetail.PHDPATENT);

                        objParams[6] = new SqlParameter("@P_ESTB_SB_MISCE_PHD_DETAIL", dtPhd);

                        objParams[7] = new SqlParameter("@P_PHDGUIDED", objmisdetail.PHDGUIDED);
                        objParams[8] = new SqlParameter("@P_PHDAWARD", objmisdetail.PHDAWARD);
                        objParams[9] = new SqlParameter("@P_ESTB_SB_ID_DETAIL", dtId);
                        objParams[10] = new SqlParameter("@P_ESTB_SB_THESIS_DETAIL", dtTh);

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_PRESERVICE", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_MIS_DETAIL_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddPreService-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetSingleMisdetailEmployee(int MOSNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MOSNO", MOSNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_MISDETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSinglePreServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public int UpdateMiscellaneousdetails(ServiceBook objmisdetail, DataTable dtIndex, DataTable dtBond, DataTable dtPhd, DataTable dtId, DataTable dtTh)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", objmisdetail.IDNO);

                        //objParams[1] = new SqlParameter("@P_HIndex", objmisdetail.HIndex);
                        // objParams[2] = new SqlParameter("@P_BOND", objmisdetail.Bond);


                        //if (!objmisdetail.FDT.Equals(DateTime.MinValue))
                        //    objParams[3] = new SqlParameter("@P_FROMDATE", objmisdetail.FROMDATE);
                        //else
                        //    objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);
                        //if (!objmisdetail.TDT.Equals(DateTime.MinValue))
                        //    objParams[4] = new SqlParameter("@P_TODATE", objmisdetail.TODATE);
                        //else
                        //    objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);

                        // objParams[1] = new SqlParameter("@P_PHDGUIDED", objmisdetail.PHDGUIDED);
                        //objParams[2] = new SqlParameter("@P_PHDAWARD", objmisdetail.PHDAWARD);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objmisdetail.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_ATTACHMENT", objmisdetail.ATTACHMENTS);
                        objParams[3] = new SqlParameter("@P_MOSNO", objmisdetail.MOSNO);
                        objParams[4] = new SqlParameter("@P_ESTB_SB_MISCE_INDEX_FACTOR_DETAIL", dtIndex);
                        objParams[5] = new SqlParameter("@P_ESTB_SB_MISCE_BOND_DETAIL", dtBond);
                        objParams[6] = new SqlParameter("@P_ESTB_SB_MISCE_PHD_DETAIL", dtPhd);
                        //objParams[8] = new SqlParameter("@P_CANDIDATENAME", objmisdetail.NAME);
                        //objParams[9] = new SqlParameter("@P_REGDATE", objmisdetail.DATE);
                        //objParams[10] = new SqlParameter("@P_UNIVERSITYNAME", objmisdetail.UNIVERSITY_NAME);
                        //objParams[11] = new SqlParameter("@P_RESEARCHNAME", objmisdetail.RESEARCHNAME);
                        //objParams[12] = new SqlParameter("@P_GUIDENAME", objmisdetail.GUIDENAME);
                        //objParams[13] = new SqlParameter("@P_PUBLICATIONPHDNO", objmisdetail.PUBLICATIONPHDNO);
                        //objParams[14] = new SqlParameter("@P_PHDGRANT", objmisdetail.PHDGRANT);
                        //objParams[15] = new SqlParameter("@P_PHDPATENT", objmisdetail.PHDPATENT);

                        objParams[7] = new SqlParameter("@P_PHDGUIDED", objmisdetail.PHDGUIDED);
                        objParams[8] = new SqlParameter("@P_PHDAWARD", objmisdetail.PHDAWARD);
                        objParams[9] = new SqlParameter("@P_ESTB_SB_ID_DETAIL", dtId);
                        objParams[10] = new SqlParameter("@P_ESTB_SB_THESIS_DETAIL", dtTh);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_MISEUPDATE_NEW", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePreService-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public int DeletemiseInfo(int fnNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MOSNO", fnNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_MISEDETAIL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteFamilyInfo.-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetSingleExperienceDetails(int SVCNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SVCNO", SVCNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_EXPSVCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddExperiencesDetails(ServiceBook objsvexp)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_IDNO", objsvexp.IDNO);
                        objParams[1] = new SqlParameter("@P_Department", objsvexp.DepID);
                        objParams[2] = new SqlParameter("@P_Designation", objsvexp.DesID);
                        objParams[3] = new SqlParameter("@P_NatureofAppointment", objsvexp.NatOfApp);
                        objParams[4] = new SqlParameter("@P_IsCurrent", objsvexp.IsCurrent);
                        if (!objsvexp.StartDate.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_StartDate", objsvexp.StartDate);
                        else
                            objParams[5] = new SqlParameter("@P_StartDate", DBNull.Value);

                        if (!objsvexp.EndDate.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_EndDate", objsvexp.EndDate);
                        else
                            objParams[6] = new SqlParameter("@P_EndDate", DBNull.Value);
                        objParams[7] = new SqlParameter("@P_DURATION", objsvexp.Duration);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objsvexp.CollegeCode);
                        objParams[9] = new SqlParameter("@P_Attachment", objsvexp.ATTACHMENTS);
                        objParams[10] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[11] = new SqlParameter("@P_FILEPATH", objsvexp.FILEPATH);
                        objParams[12] = new SqlParameter("@P_ISBLOB", objsvexp.ISBLOB);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_EXPERIENCES", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_EXPERIENCES", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddExperiencesDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetAllExperiencesDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EXPERIENCES_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        //ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateSVCEExp(ServiceBook objsvexp)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objsvexp.IDNO);
                        objParams[1] = new SqlParameter("@P_Department", objsvexp.DepID);
                        objParams[2] = new SqlParameter("@P_Designation", objsvexp.DesID);
                        objParams[3] = new SqlParameter("@P_NatureofAppointment ", objsvexp.NatOfApp);
                        objParams[4] = new SqlParameter("@P_Iscurrent", objsvexp.IsCurrent);
                        if (!objsvexp.StartDate.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_StartDate", objsvexp.StartDate);
                        else
                            objParams[5] = new SqlParameter("@P_StartDate", DBNull.Value);

                        if (!objsvexp.EndDate.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_EndDate", objsvexp.EndDate);
                        else
                            objParams[6] = new SqlParameter("@P_EndDate", DBNull.Value);
                        objParams[7] = new SqlParameter("@P_DURATION", objsvexp.Duration);
                        objParams[8] = new SqlParameter("@P_ATTACHMENT", objsvexp.ATTACHMENTS);
                        objParams[9] = new SqlParameter("@P_SVCNO", objsvexp.SVCNO);
                        objParams[10] = new SqlParameter("@P_ISBLOB", objsvexp.ISBLOB);
                        objParams[11] = new SqlParameter("@P_FILEPATH", objsvexp.FILEPATH);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_SVCEEXP", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateDeptExam-> " + ex.ToString());
                    }
                    return retStatus;
                }




                public int DeleteExperiencesByID(int SVCNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SVCNO", SVCNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_ExpByID", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteExperiencesByID.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllExperiencesCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_INSTITUE_EXPERIENCE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllExperiencesCount-> " + ex.ToString());
                    }
                    finally
                    {
                        //ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllStaffPatent(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PATENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public DataSet GetSinglePatentOfEmployee(int PCNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PCNO", PCNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_PATENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public int UpdatePatent(ServiceBook objPatent, DataTable dteng, DataTable dtFile)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_IDNO", objPatent.IDNO);
                        objParams[1] = new SqlParameter("@P_Title_Patent", objPatent.PatentTitle);
                        objParams[2] = new SqlParameter("@P_Applicant_Name", objPatent.ApplicantName);
                        objParams[3] = new SqlParameter("@P_Role", objPatent.ROLE);
                        objParams[4] = new SqlParameter("@P_Other_Role", objPatent.OtherRole);
                        objParams[5] = new SqlParameter("@P_Category", objPatent.PatentCategory);
                        objParams[6] = new SqlParameter("@P_Status", objPatent.PatentStatus);
                        objParams[7] = new SqlParameter("@P_Withdrawn", objPatent.Withdrawn);
                        if (!objPatent.FROMDATE.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_DATE", objPatent.FROMDATE);
                        else
                            objParams[8] = new SqlParameter("@P_DATE", DBNull.Value);
                        objParams[9] = new SqlParameter("@P_No_Of_Mem", objPatent.NO_GUIDED);

                        objParams[10] = new SqlParameter("@P_FILENO", objPatent.PATENTNO);
                        objParams[11] = new SqlParameter("@P_APPLICATION_NO", objPatent.APPLICATION_NUMBER);

                        if (!objPatent.STATUS_DATE.Equals(DateTime.MinValue))
                            objParams[12] = new SqlParameter("@P_STATUS_DATE", objPatent.STATUS_DATE);
                        else
                            objParams[12] = new SqlParameter("@P_STATUS_DATE", DBNull.Value);

                        objParams[13] = new SqlParameter("@P_ESTB_EMP_ROLE_RECORD", dteng);
                        objParams[14] = new SqlParameter("@P_PCNO", objPatent.PCNO);

                        #region Add on 14-01-2020 By Sonali Ambedare
                        objParams[15] = new SqlParameter("@P_Subject_Of_Patent", objPatent.SubjectOfPatent);
                        objParams[16] = new SqlParameter("@P_PAYROLL_SB_PATENT_DOCUMENT_UPLOAD_RECORD", dtFile);
                        //objParams[16] = new SqlParameter("@P_Attachment", objPatent.ATTACHMENTS);
                        #endregion

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PATEGetSinglePatentOfEmployeeNT", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        objParams[17] = new SqlParameter("@P_ISBLOB", objPatent.ISBLOB);
                        objParams[18] = new SqlParameter("@P_IPRNO", objPatent.IPRNO);
                        objParams[19] = new SqlParameter("@P_IPRNOAGNO", objPatent.IPRNOAGNO);
                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PATENT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateFamilyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeletePatentDetails(int PCNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PCNO", PCNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_DEL_PATENT_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteEmpImage.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddPatent(ServiceBook objPatent, DataTable dteng, DataTable dtFile)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_IDNO", objPatent.IDNO);
                        objParams[1] = new SqlParameter("@P_Title_Patent", objPatent.PatentTitle);
                        objParams[2] = new SqlParameter("@P_Applicant_Name", objPatent.ApplicantName);
                        objParams[3] = new SqlParameter("@P_Role", objPatent.ROLE);
                        objParams[4] = new SqlParameter("@P_Other_Role", objPatent.OtherRole);
                        objParams[5] = new SqlParameter("@P_Category", objPatent.PatentCategory);
                        objParams[6] = new SqlParameter("@P_Status", objPatent.PatentStatus);
                        objParams[7] = new SqlParameter("@P_Withdrawn", objPatent.Withdrawn);

                        if (!objPatent.FROMDATE.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_DATE", objPatent.FROMDATE);
                        else
                            objParams[8] = new SqlParameter("@P_DATE", DBNull.Value);
                        objParams[9] = new SqlParameter("@P_No_Of_Mem", objPatent.NO_GUIDED);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objPatent.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_ESTB_EMP_ROLE_RECORD", dteng);

                        objParams[12] = new SqlParameter("@P_FILENO", objPatent.PATENTNO);
                        objParams[13] = new SqlParameter("@P_APPLICATION_NO", objPatent.APPLICATION_NUMBER);



                        if (!objPatent.STATUS_DATE.Equals(DateTime.MinValue))
                            objParams[14] = new SqlParameter("@P_STATUS_DATE", objPatent.STATUS_DATE);
                        else
                            objParams[14] = new SqlParameter("@P_STATUS_DATE", DBNull.Value);

                        #region Add on 14-01-2020 By Sonali Ambedare
                        objParams[15] = new SqlParameter("@P_Subject_Of_Patent", objPatent.SubjectOfPatent);
                        objParams[16] = new SqlParameter("@P_PAYROLL_SB_PATENT_DOCUMENT_UPLOAD_RECORD", dtFile);
                        //objParams[16] = new SqlParameter("@P_Attachment ", objPatent.ATTACHMENTS);
                        #endregion
                        objParams[17] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[18] = new SqlParameter("@P_ISBLOB", objPatent.ISBLOB);
                        objParams[19] = new SqlParameter("@P_IPRNO", objPatent.IPRNO);
                        objParams[20] = new SqlParameter("@P_IPRNOAGNO", objPatent.IPRNOAGNO);
                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_INVITED_TALK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_Patent", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddAccomplishment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllStaffPatentCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PATENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllStaffFundedEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_StaffFunded", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetSingleStaffFundOfEmployee(int SFNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SFNO", SFNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_StafffFunded", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddStafffunded(ServiceBook objStaffFund, DataTable dt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_IDNO", objStaffFund.IDNO);
                        objParams[1] = new SqlParameter("@P_Project_Title", objStaffFund.Name_org);
                        objParams[2] = new SqlParameter("@P_Funding_Name", objStaffFund.Name_agency);
                        objParams[3] = new SqlParameter("@P_Address", objStaffFund.ADDRESS);
                        objParams[4] = new SqlParameter("@P_Agency_Category", objStaffFund.AGECATNO);
                        objParams[5] = new SqlParameter("@P_Role", objStaffFund.ROLE);
                        objParams[6] = new SqlParameter("@P_AMOUNT", objStaffFund.AMOUNT);
                        objParams[7] = new SqlParameter("@P_Project_Status", objStaffFund.ProjectStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objStaffFund.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_PAYROLL_SB_STAFF_DOCUMENT_UPLOAD_RECORD", dt);

                        #region Modified On 14-01-20 By Sonali Ambedare
                        objParams[10] = new SqlParameter("@P_ProjectNature", objStaffFund.ProjectNature);
                        objParams[11] = new SqlParameter("@P_SchemeName", objStaffFund.SchemeName);
                        objParams[12] = new SqlParameter("@p_Projectlevel", objStaffFund.ProjectLevel);
                        objParams[13] = new SqlParameter("@P_FDT", objStaffFund.FDT);
                        objParams[14] = new SqlParameter("@P_TDT", objStaffFund.TDT);
                        objParams[15] = new SqlParameter("@P_Duration", objStaffFund.DURATION);
                        #endregion

                        #region Add on 26-08-2020 by Sonali Ambedare
                        objParams[16] = new SqlParameter("@P_INVESTIGATOR", objStaffFund.INVESTIGATOR);
                        objParams[17] = new SqlParameter("@P_CO_INVESTIGATOR", objStaffFund.COINVESTIGATOR);
                        objParams[18] = new SqlParameter("@P_WEB_LINK", objStaffFund.WEBLINK);
                        objParams[19] = new SqlParameter("@P_PROJECT_STATUS_ID", objStaffFund.PROJECT_STATUS_ID);
                        #endregion
                        objParams[20] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                        objParams[21] = new SqlParameter("@P_ISBLOB", objStaffFund.ISBLOB);
                        objParams[22] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_StaffFunded", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddStaffConsultancy-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateStafffunded(ServiceBook objStaffFund, DataTable dt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_IDNO", objStaffFund.IDNO);
                        objParams[1] = new SqlParameter("@P_Project_Title", objStaffFund.Name_org);
                        objParams[2] = new SqlParameter("@P_Funding_Name", objStaffFund.Name_agency);
                        objParams[3] = new SqlParameter("@P_Address", objStaffFund.ADDRESS);
                        objParams[4] = new SqlParameter("@P_Agency_Category", objStaffFund.AGECATNO);
                        objParams[5] = new SqlParameter("@P_Role", objStaffFund.ROLE);
                        objParams[6] = new SqlParameter("@P_AMOUNT", objStaffFund.AMOUNT);
                        objParams[7] = new SqlParameter("@P_Project_Status", objStaffFund.ProjectStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objStaffFund.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_SFNO", objStaffFund.SFNO);
                        objParams[10] = new SqlParameter("@P_PAYROLL_SB_STAFF_DOCUMENT_UPLOAD_RECORD", dt);

                        #region modified On 14-01-20 By Sonali Ambedare
                        objParams[11] = new SqlParameter("@P_ProjectNature", objStaffFund.ProjectNature);
                        objParams[12] = new SqlParameter("@P_SchemeName", objStaffFund.SchemeName);
                        objParams[13] = new SqlParameter("@p_Projectlevel", objStaffFund.ProjectLevel);
                        objParams[14] = new SqlParameter("@P_FDT", objStaffFund.FDT);
                        objParams[15] = new SqlParameter("@P_TDT", objStaffFund.TDT);
                        objParams[16] = new SqlParameter("@P_Duration", objStaffFund.DURATION);
                        #endregion

                        #region Add On 26-08-20 By Sonali Ambedare
                        objParams[17] = new SqlParameter("@P_INVESTIGATOR", objStaffFund.INVESTIGATOR);
                        objParams[18] = new SqlParameter("@P_CO_INVESTIGATOR", objStaffFund.COINVESTIGATOR);
                        objParams[19] = new SqlParameter("@P_WEB_LINK", objStaffFund.WEBLINK);
                        objParams[20] = new SqlParameter("@P_PROJECT_STATUS_ID", objStaffFund.PROJECT_STATUS_ID);
                        objParams[21] = new SqlParameter("@P_ISBLOB", objStaffFund.ISBLOB);
                        #endregion

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_SB_STAFF_FUNDED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateStaffConsultancyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteStaffFundedInfo(int SFNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SFNO", SFNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_DEL_SB_STAFF_FUNDED_INFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteFamilyInfo.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllStaffFundedEmployeeCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_FUNDED_PROJECT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllMembershipinProfessional(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_MembershipinProfessional", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleMemberinProfebodyOfEmployee(int MPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MPNO", MPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_MembershipinProfessional", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public int AddMembershipinProfessionalbody(ServiceBook objMembershipinProfessional)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", objMembershipinProfessional.IDNO);
                        objParams[1] = new SqlParameter("@P_Prof_name", objMembershipinProfessional.NameOfProfBody);

                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objMembershipinProfessional.COLLEGE_CODE);
                        //  objParams[7] = new SqlParameter("@P_ATTACHMENT", objAccomplishment.ATTACHMENTS);

                        objParams[3] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_INVITED_TALK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        // object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_MembershipinProfessional", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_MembershipinProfessional_new", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddMembershipinProfessionalbody-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateMemberinProfessbodyInfo(ServiceBook objAccomplishment)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objAccomplishment.IDNO);
                        objParams[1] = new SqlParameter("@P_NAMEPROFBODY", objAccomplishment.NameOfProfBody);

                        objParams[2] = new SqlParameter("@P_MPNO", objAccomplishment.MPNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_MemberinProfessbodyInfo_new", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateFamilyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddMembershipinProfessionalbody(ServiceBook objMembershipinProfessional, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", objMembershipinProfessional.IDNO);
                        objParams[1] = new SqlParameter("@P_Prof_name", objMembershipinProfessional.NameOfProfBody);

                        #region  Add on 08-01-20 By Sonali Ambedare
                        objParams[2] = new SqlParameter("@P_MemebrShip_no", objMembershipinProfessional.MemberShipNumber);
                        objParams[3] = new SqlParameter("@P_Memeber_Type", objMembershipinProfessional.MemberShipType);
                        objParams[4] = new SqlParameter("@P_PAYROLL_SB_PROFESSIONAL_MEMBERSHIP_DOCUMENT_UPLOAD_RECORD", dt);
                        #endregion

                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objMembershipinProfessional.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_YEAR", objMembershipinProfessional.YEAR);
                        //  objParams[7] = new SqlParameter("@P_ATTACHMENT", objAccomplishment.ATTACHMENTS);
                        objParams[7] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[8] = new SqlParameter("@P_MEMTYPE", objMembershipinProfessional.MEMTYPE);
                        objParams[9] = new SqlParameter("@P_ISBLOB", objMembershipinProfessional.ISBLOB);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_INVITED_TALK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_MembershipinProfessional", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddMembershipinProfessionalbody-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMemberinProfessbodyInfo(ServiceBook objAccomplishment, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objAccomplishment.IDNO);
                        objParams[1] = new SqlParameter("@P_NAMEPROFBODY", objAccomplishment.NameOfProfBody);

                        #region  Add on 08-01-20 By Sonali Ambedare
                        objParams[2] = new SqlParameter("@P_MemebrShip_no", objAccomplishment.MemberShipNumber);
                        objParams[3] = new SqlParameter("@P_Memeber_Type", objAccomplishment.MemberShipType);
                        objParams[4] = new SqlParameter("@P_PAYROLL_SB_PROFESSIONAL_MEMBERSHIP_DOCUMENT_UPLOAD_RECORD", dt);
                        #endregion

                        objParams[5] = new SqlParameter("@P_MPNO", objAccomplishment.MPNO);
                        objParams[6] = new SqlParameter("@P_YEAR", objAccomplishment.YEAR);
                        objParams[7] = new SqlParameter("@P_MEMTYPE", objAccomplishment.MEMTYPE);
                        objParams[8] = new SqlParameter("@P_ISBLOB", objAccomplishment.ISBLOB);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_MemberinProfessbodyInfo", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateFamilyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int DeleteMembershipinProfessionalInfo(int MPNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MPNO", MPNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_MembershipinProfessionalINFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteMembershipinProfessionalInfo.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllMembershipinProfessionalCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_MEMBERSHIP_PROF_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllMembershipinProfessionalCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public DataSet GetAllAccomplishmentEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_Accomplishment", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetSingleAccomplishmentOfEmployee(int ACNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ACNO", ACNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_Accomplishment", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteAccomplishmentInfo(int ACNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ACNO", ACNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_AccomplishmentINFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteFamilyInfo.-> " + ex.ToString());
                    }
                    return retStatus;
                }




                public int AddAccomplishment(ServiceBook objAccomplishment, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objAccomplishment.IDNO);
                        objParams[1] = new SqlParameter("@P_AwardName", objAccomplishment.AwardName);
                        objParams[2] = new SqlParameter("@P_OrganizationAddress", objAccomplishment.OrganizationAdd);
                        if (!objAccomplishment.DOACH.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_DATEOFACC", objAccomplishment.DOACH);
                        else
                            objParams[3] = new SqlParameter("@P_DATEOFACC", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_AMOUNT_REC", objAccomplishment.AMOUNT_REC);

                        objParams[5] = new SqlParameter("@P_REMARK", objAccomplishment.Description);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objAccomplishment.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAccomplishment.ATTACHMENTS);

                        #region  Add on 08-01-20 By Sonali Ambedare
                        objParams[7] = new SqlParameter("@P_Award_Level", objAccomplishment.Awardlevel);
                        objParams[8] = new SqlParameter("@PAYROLL_SB_ACCOMPLISHMENT_DOCUMENT_UPLOAD_RECORD", dt);
                        #endregion
                        objParams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[10] = new SqlParameter("@P_ISSUINGORGANIZATION", objAccomplishment.ISSUINGORGANIZATION);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objAccomplishment.ISBLOB);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_INVITED_TALK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_Accomplishment", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddAccomplishment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAccomplishmentInfo(ServiceBook objAccomplishment, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objAccomplishment.IDNO);
                        objParams[1] = new SqlParameter("@P_AwardName", objAccomplishment.AwardName);
                        objParams[2] = new SqlParameter("@P_OrganizationAddress", objAccomplishment.OrganizationAdd);
                        if (!objAccomplishment.DOACH.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_DATEOFACC", objAccomplishment.DOACH);
                        else
                            objParams[3] = new SqlParameter("@P_DATEOFACC", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_AMOUNT_REC", objAccomplishment.AMOUNT_REC);
                        objParams[5] = new SqlParameter("@P_REMARK", objAccomplishment.Description);
                        objParams[6] = new SqlParameter("@P_ACNO", objAccomplishment.ACNO);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAccomplishment.ATTACHMENTS);

                        #region  Add on 08-01-20 By Sonali Ambedare
                        objParams[8] = new SqlParameter("@P_Award_Level", objAccomplishment.Awardlevel);
                        objParams[9] = new SqlParameter("@P_PAYROLL_SB_ACCOMPLISHMENT_DOCUMENT_UPLOAD_RECORD", dt);
                        #endregion


                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_AccomplishmentINFO", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        #region Edited on 22-11-2019 by vijay
                        objParams[10] = new SqlParameter("@P_ISSUINGORGANIZATION", objAccomplishment.ISSUINGORGANIZATION);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objAccomplishment.ISBLOB);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_AccomplishmentINFO", objParams, false);
                        // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_AccomplishmentINFO_NEW", objParams, false);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_AccomplishmentINFO_NEW", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                        #endregion


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateFamilyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAccomplishmentCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_ACCOMPLISHMENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAccomplishmentCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public DataSet GetAllStaffConsultancyEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_StaffConsultancy", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetSingleStaffConsultancyOfEmployee(int SCNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCNO", SCNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_StaffConsultancy", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleInvitedTalk-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddStaffConsultancy(ServiceBook objConsultancy, DataTable dt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", objConsultancy.IDNO);
                        objParams[1] = new SqlParameter("@P_Name_org", objConsultancy.Name_org);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objConsultancy.ADDRESS);
                        if (!objConsultancy.DOACH.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROMDATE", objConsultancy.FROMDATE);
                        else
                            objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);
                        if (!objConsultancy.DOACH.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TODATE", objConsultancy.TODATE);
                        else
                            objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);
                        //if (!objConsultancy.DOACH.Equals(DateTime.MinValue))
                        //    objParams[5] = new SqlParameter("@P_CONSDATE", objConsultancy.CONSDATE);
                        //else
                        //    objParams[5] = new SqlParameter("@P_CONSDATE", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_Duration", objConsultancy.DURATION);
                        objParams[6] = new SqlParameter("@P_AMOUNT", objConsultancy.AMOUNT);
                        objParams[7] = new SqlParameter("@P_NatureOfWorkText", objConsultancy.NatureOfWorkText);
                        objParams[8] = new SqlParameter("@P_Description", objConsultancy.Description);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objConsultancy.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_TITLE", objConsultancy.TITLE);
                        objParams[11] = new SqlParameter("@P_PAYROLL_SB_STAFF_DOCUMENT_UPLOAD_RECORD", dt);
                        objParams[12] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[13] = new SqlParameter("@P_ISBLOB", objConsultancy.ISBLOB);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INST_SB_StaffConsultancy", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddStaffConsultancy-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateStaffConsultancyInfo(ServiceBook objConsultancy, DataTable dt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objConsultancy.IDNO);
                        objParams[1] = new SqlParameter("@P_Name_org", objConsultancy.Name_org);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objConsultancy.ADDRESS);
                        if (!objConsultancy.FROMDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROMDATE", objConsultancy.FROMDATE);
                        else
                            objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);
                        if (!objConsultancy.TODATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TODATE", objConsultancy.TODATE);
                        else
                            objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);
                        //if (!objConsultancy.CONSDATE.Equals(DateTime.MinValue))
                        //    objParams[5] = new SqlParameter("@P_CONSDATE", objConsultancy.CONSDATE);
                        //else
                        //    objParams[5] = new SqlParameter("@P_CONSDATE", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_Duration", objConsultancy.DURATION);
                        objParams[6] = new SqlParameter("@P_AMOUNT", objConsultancy.AMOUNT);
                        objParams[7] = new SqlParameter("@P_NatureOfWorkText", objConsultancy.NatureOfWorkText);
                        objParams[8] = new SqlParameter("@P_Description", objConsultancy.Description);
                        objParams[9] = new SqlParameter("@P_SCNO", objConsultancy.SCNO);
                        objParams[10] = new SqlParameter("@P_TITLE", objConsultancy.TITLE);
                        objParams[11] = new SqlParameter("@P_PAYROLL_SB_STAFF_DOCUMENT_UPLOAD_RECORD", dt);
                        objParams[12] = new SqlParameter("@P_ISBLOB", objConsultancy.ISBLOB);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_StaffConsultancy", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateStaffConsultancyInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteStaffConsultancyInfo(int SCNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCNO", SCNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_DEL_SB_STAFFCONSULTANCY_INFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteFamilyInfo.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllStaffConsultancyCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_STAFF_CONSULTANCY_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllFamilyDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public int AddTrainingConducted(ServiceBook objTraining, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[29];
                        objParams[0] = new SqlParameter("@P_IDNO", objTraining.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objTraining.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objTraining.INST);
                        if (!objTraining.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objTraining.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objTraining.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objTraining.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objTraining.REMARK);
                        objParams[6] = new SqlParameter("P_COLLEGE_CODE", objTraining.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_SPONSORED_AMOUNT", objTraining.SPONSORED_AMOUNT);
                        objParams[8] = new SqlParameter("@P_LOCATION", objTraining.LOCATION);
                        objParams[9] = new SqlParameter("@P_SPONSOREDBY", objTraining.SPONSOREDBY);

                        objParams[10] = new SqlParameter("@P_PROGRAM_LEVEL", objTraining.PROGRAM_LEVEL);
                        objParams[11] = new SqlParameter("@P_PROGRAM_TYPE", objTraining.PROGRAM_TYPE);
                        objParams[12] = new SqlParameter("@P_PRESENTATION_DETAILS", objTraining.PRESENTATION_DETAILS);
                        objParams[13] = new SqlParameter("@P_NOOFPARTICIPANT", objTraining.NOOFPARTICIPANT);
                        objParams[14] = new SqlParameter("@P_DURATION", objTraining.DURATION);
                        objParams[15] = new SqlParameter("@P_ROLENAME", objTraining.ROLENAME);
                        objParams[16] = new SqlParameter("@P_COST_INVOLVED", objTraining.COST_INVOLVED);
                        objParams[17] = new SqlParameter("@P_NOOFPARTI", objTraining.NOOFPARTI);
                        objParams[18] = new SqlParameter("@P_INTERNAL_FACULTY", objTraining.InternalFaculty);
                        objParams[19] = new SqlParameter("@P_EXTERNAL_FACULTY", objTraining.ExternalFaculty);
                        objParams[20] = new SqlParameter("@P_INTERNAL_STUDENT", objTraining.InternalStudent);
                        objParams[21] = new SqlParameter("@P_EXTERNAL_STUDENT", objTraining.ExternalStudent);


                        objParams[22] = new SqlParameter("@P_PAYROLL_SB_TRAINING_COND_DOCUMENT_UPLOAD", dt);

                        //Add on 27-08-2020 
                        objParams[23] = new SqlParameter("@P_PROFESSIONAL_BODY", objTraining.PROFESSIONALBODY);
                        objParams[24] = new SqlParameter("@P_ATTACHMENT", objTraining.ATTACHMENTS);

                        objParams[25] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[26] = new SqlParameter("@P_MODE", objTraining.MODE);
                        objParams[27] = new SqlParameter("@P_ISBLOB", objTraining.ISBLOB);
                        objParams[28] = new SqlParameter("@P_TNO", SqlDbType.Int);
                        objParams[28].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_TRAINING_CONDUCTED", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_TRAINING_CONDUCTED", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddTrainingConducted-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateTrainingConducted(ServiceBook objTraining, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_IDNO", objTraining.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objTraining.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objTraining.INST);
                        if (!objTraining.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objTraining.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objTraining.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objTraining.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objTraining.REMARK);
                        objParams[6] = new SqlParameter("@P_TNO", objTraining.TNO);
                        objParams[7] = new SqlParameter("@P_SPONSORED_AMOUNT", objTraining.SPONSORED_AMOUNT);
                        objParams[8] = new SqlParameter("@P_LOCATION", objTraining.LOCATION);
                        objParams[9] = new SqlParameter("@P_SPONSOREDBY", objTraining.SPONSOREDBY);

                        objParams[10] = new SqlParameter("@P_PROGRAM_LEVEL", objTraining.PROGRAM_LEVEL);
                        objParams[11] = new SqlParameter("@P_PROGRAM_TYPE", objTraining.PROGRAM_TYPE);
                        objParams[12] = new SqlParameter("@P_PRESENTATION_DETAILS", objTraining.PRESENTATION_DETAILS);
                        objParams[13] = new SqlParameter("@P_NOOFPARTICIPANT", objTraining.NOOFPARTICIPANT);
                        objParams[14] = new SqlParameter("@P_DURATION", objTraining.DURATION);
                        objParams[15] = new SqlParameter("@P_ROLENAME", objTraining.ROLENAME);
                        objParams[16] = new SqlParameter("@P_COST_INVOLVED", objTraining.COST_INVOLVED);
                        objParams[17] = new SqlParameter("@P_NOOFPARTI", objTraining.NOOFPARTI);
                        objParams[18] = new SqlParameter("@P_INTERNAL_FACULTY", objTraining.InternalFaculty);
                        objParams[19] = new SqlParameter("@P_EXTERNAL_FACULTY", objTraining.ExternalFaculty);
                        objParams[20] = new SqlParameter("@P_INTERNAL_STUDENT", objTraining.InternalStudent);
                        objParams[21] = new SqlParameter("@P_EXTERNAL_STUDENT", objTraining.ExternalStudent);
                        //objParams[18] = new SqlParameter("@P_ATTACHMENT", objTraining.ATTACHMENTS);
                        objParams[22] = new SqlParameter("@P_PAYROLL_SB_TRAINING_COND_DOCUMENT_UPLOAD", dt);
                        //Add on 27-08-2020
                        objParams[23] = new SqlParameter("@P_PROFESSIONAL_BODY", objTraining.PROFESSIONALBODY);
                        objParams[24] = new SqlParameter("@P_MODE", objTraining.MODE);
                        objParams[25] = new SqlParameter("@P_ISBLOB", objTraining.ISBLOB);
                        objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_TRAINING_CONDUCTED", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_TRAINING_CONDUCTED", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateTrainingConducted-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddAward(ServiceBook objAward, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objAward.IDNO);
                        objParams[1] = new SqlParameter("@P_AWARDNAME", objAward.AwardName);
                        objParams[2] = new SqlParameter("@P_OrganizationAddress", objAward.OrganizationAdd);
                        if (!objAward.DOACH.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_DATEOFACC", objAward.DOACH);
                        else
                            objParams[3] = new SqlParameter("@P_DATEOFACC", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_AMOUNT_REC", objAward.AMOUNT_REC);

                        objParams[5] = new SqlParameter("@P_REMARK", objAward.Description);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objAward.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAward.ATTACHMENTS);


                        objParams[7] = new SqlParameter("@P_AWARD_LEVEL", objAward.Awardlevel);
                        objParams[8] = new SqlParameter("@PAYROLL_SB_AWARD_DOCUMENT_UPLOAD_RECORD", dt);

                        objParams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[10] = new SqlParameter("@P_ISSUINGORGANIZATION", objAward.ISSUINGORGANIZATION);

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INS_SB_AWARD", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddAward-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAward(ServiceBook objAward, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objAward.IDNO);
                        objParams[1] = new SqlParameter("@P_AWARDNAME", objAward.AwardName);
                        objParams[2] = new SqlParameter("@P_OrganizationAddress", objAward.OrganizationAdd);
                        if (!objAward.DOACH.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_DATEOFACC", objAward.DOACH);
                        else
                            objParams[3] = new SqlParameter("@P_DATEOFACC", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_AMOUNT_REC", objAward.AMOUNT_REC);
                        objParams[5] = new SqlParameter("@P_REMARK", objAward.Description);
                        objParams[6] = new SqlParameter("@P_AWDNO", objAward.AWDNO);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAward.ATTACHMENTS);

                        objParams[8] = new SqlParameter("@P_AWARD_LEVEL", objAward.Awardlevel);
                        objParams[9] = new SqlParameter("@PAYROLL_SB_AWARD_DOCUMENT_UPLOAD_RECORD", dt);


                        objParams[10] = new SqlParameter("@P_ISSUINGORGANIZATION", objAward.ISSUINGORGANIZATION);

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_AWARD", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateAward-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAwardEmployee(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_Award", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAwardEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleAwardOfEmployee(int AWDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AWDNO", AWDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SB_AWARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleAwardOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteAward(int AWDNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AWDNO", AWDNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_AWARD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteAward.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddAvishkar(ServiceBook objAvishkar)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objAvishkar.IDNO);
                        objParams[1] = new SqlParameter("@P_LEVEL", objAvishkar.LEVEL);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objAvishkar.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_PAPERTITLE", objAvishkar.PAPERTITLE);
                        objParams[4] = new SqlParameter("@P_VENUE", objAvishkar.VENUE);
                        objParams[5] = new SqlParameter("@P_DOR", objAvishkar.AVDATE);
                        objParams[6] = new SqlParameter("@P_AWARD", objAvishkar.AWARD);
                        objParams[7] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_AVISHKAR", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddAvishkar-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAvishkar(ServiceBook objAvishkar)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objAvishkar.IDNO);
                        objParams[1] = new SqlParameter("@P_LEVEL", objAvishkar.LEVEL);
                        objParams[2] = new SqlParameter("@P_AVNO", objAvishkar.AVNO);
                        objParams[3] = new SqlParameter("@P_PAPERTITLE", objAvishkar.PAPERTITLE);
                        objParams[4] = new SqlParameter("@P_VENUE", objAvishkar.VENUE);
                        objParams[5] = new SqlParameter("@P_DOR", objAvishkar.AVDATE);
                        objParams[6] = new SqlParameter("@P_AWARD", objAvishkar.AWARD);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_AVISHKAR", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateAvishkar-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAvishkar(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_AVISHKAR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAvishkar-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleFamilyDetailsOfAvishkar(int AVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AVNO", AVNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_AVISHKAR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleFamilyDetailsOfAvishkar-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteAvishkar(int AVNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AVNO", AVNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_AVISHKAR", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteAvishkar.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region PAY_SB_RESEARCH
                public int AddResearch(ServiceBook objConference)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_IDNO", objConference.IDNO);
                        objParams[1] = new SqlParameter("@P_DEPARTMENT", objConference.DEPARTMENT);
                        objParams[2] = new SqlParameter("@P_PROJECT_TITLE", objConference.PROJECT_TITLE);
                        objParams[3] = new SqlParameter("@P_NAME_OF_PRINCIPAL", objConference.NAME_OF_PRINCIPAL);
                        objParams[4] = new SqlParameter("@P_NATURE_OF_PROJECT_ID", objConference.NATURE_OF_PROJECT_ID);
                        objParams[5] = new SqlParameter("@P_SPONSERED_BY_ID", objConference.SPONSERED_BY_ID);
                        objParams[6] = new SqlParameter("@P_FUNDING_AJENCY_NAME", objConference.FUNDING_AJENCY_NAME);
                        objParams[7] = new SqlParameter("@P_AMOUNT", objConference.AMOUNT);
                        objParams[8] = new SqlParameter("@P_TOTAL_PROJECT_FUND", objConference.TOTAL_PROJECT_FUND);


                        if (!objConference.PERIOD_FROM_DATE.Equals(DateTime.MinValue))
                        {
                            objParams[9] = new SqlParameter("@P_PERIOD_FROM_DATE", objConference.PERIOD_FROM_DATE);
                        }
                        else
                        {
                            objParams[9] = new SqlParameter("@P_PERIOD_FROM_DATE", DBNull.Value);
                        }
                        if (!objConference.PERIOD_TO_DATE.Equals(DateTime.MinValue))
                        {
                            objParams[10] = new SqlParameter("@P_PERIOD_TO_DATE", objConference.PERIOD_TO_DATE);
                        }
                        else
                        {
                            objParams[10] = new SqlParameter("@P_PERIOD_TO_DATE", DBNull.Value);
                        }


                        objParams[11] = new SqlParameter("@P_YEAR", objConference.YEAR);

                        objParams[12] = new SqlParameter("@P_PROJECT_STATUS_ID", objConference.PROJECT_STATUS_ID);
                        objParams[13] = new SqlParameter("@P_TOTAL_FUND_UTILISED", objConference.TOTAL_FUND_UTILISED);
                        objParams[14] = new SqlParameter("@P_OWNERSHIP_ID", objConference.OWNERSHIP_ID);

                        objParams[15] = new SqlParameter("@P_JOINT_WITH", objConference.JOINT_WITH);


                        objParams[16] = new SqlParameter("@P_JOINT_BELONG_TO_ID", objConference.JOINT_BELONG_TO_ID);
                        objParams[17] = new SqlParameter("@P_RESULT", objConference.RESULT_OF_INNOVATION);
                        objParams[18] = new SqlParameter("@P_IMPACT_FACTOR", objConference.IMPACT_FACTOR);
                        objParams[19] = new SqlParameter("@P_COLLEGE_CODE", objConference.COLLEGE_CODE);
                        objParams[20] = new SqlParameter("@P_JOINT_WITH_ID", objConference.JOINT_WITH_ID);

                        objParams[21] = new SqlParameter("@P_RESEARNO", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_RESEARCH", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddConference-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateResearch(ServiceBook objConference)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_RESEARNO", objConference.RESEARNO);
                        objParams[1] = new SqlParameter("@P_DEPARTMENT", objConference.DEPARTMENT);
                        objParams[2] = new SqlParameter("@P_PROJECT_TITLE", objConference.PROJECT_TITLE);
                        objParams[3] = new SqlParameter("@P_NAME_OF_PRINCIPAL", objConference.NAME_OF_PRINCIPAL);
                        objParams[4] = new SqlParameter("@P_NATURE_OF_PROJECT_ID", objConference.NATURE_OF_PROJECT_ID);
                        objParams[5] = new SqlParameter("@P_SPONSERED_BY_ID", objConference.SPONSERED_BY_ID);
                        objParams[6] = new SqlParameter("@P_FUNDING_AJENCY_NAME", objConference.FUNDING_AJENCY_NAME);
                        objParams[7] = new SqlParameter("@P_AMOUNT", objConference.AMOUNT);
                        objParams[8] = new SqlParameter("@P_TOTAL_PROJECT_FUND", objConference.TOTAL_PROJECT_FUND);


                        if (!objConference.PERIOD_FROM_DATE.Equals(DateTime.MinValue))
                        {
                            objParams[9] = new SqlParameter("@P_PERIOD_FROM_DATE", objConference.PERIOD_FROM_DATE);
                        }
                        else
                        {
                            objParams[9] = new SqlParameter("@P_PERIOD_FROM_DATE", DBNull.Value);
                        }
                        if (!objConference.PERIOD_TO_DATE.Equals(DateTime.MinValue))
                        {
                            objParams[10] = new SqlParameter("@P_PERIOD_TO_DATE", objConference.PERIOD_TO_DATE);
                        }
                        else
                        {
                            objParams[10] = new SqlParameter("@P_PERIOD_TO_DATE", DBNull.Value);
                        }


                        objParams[11] = new SqlParameter("@P_YEAR", objConference.YEAR);

                        objParams[12] = new SqlParameter("@P_PROJECT_STATUS_ID", objConference.PROJECT_STATUS_ID);
                        objParams[13] = new SqlParameter("@P_TOTAL_FUND_UTILISED", objConference.TOTAL_FUND_UTILISED);
                        objParams[14] = new SqlParameter("@P_OWNERSHIP_ID", objConference.OWNERSHIP_ID);

                        objParams[15] = new SqlParameter("@P_JOINT_WITH", objConference.JOINT_WITH);


                        objParams[16] = new SqlParameter("@P_JOINT_BELONG_TO_ID", objConference.JOINT_BELONG_TO_ID);
                        objParams[17] = new SqlParameter("@P_RESULT", objConference.RESULT_OF_INNOVATION);
                        objParams[18] = new SqlParameter("@P_IMPACT_FACTOR", objConference.IMPACT_FACTOR);


                        objParams[19] = new SqlParameter("@P_COLLEGE_CODE", objConference.COLLEGE_CODE);
                        objParams[20] = new SqlParameter("@P_JOINT_WITH_ID", objConference.JOINT_WITH_ID);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_UPD_RESEARCH", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddConference-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllResearch(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETALL_RESEARCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllConference-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteResearch(int RESEARNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RESEARNO", RESEARNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_DEL_RESEARCH", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteInnovation.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSingleResearchDetails(int RESEARNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RESEARNO", RESEARNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETSINGLE_RESEARCH_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleConferenceDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddProfessionalCourse(ServiceBook objProf, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[30];
                        objParams[0] = new SqlParameter("@P_IDNO", objProf.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objProf.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objProf.INST);
                        if (!objProf.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objProf.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objProf.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objProf.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objProf.REMARK);
                        objParams[6] = new SqlParameter("P_COLLEGE_CODE", objProf.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_SPONSORED_AMOUNT", objProf.SPONSORED_AMOUNT);
                        objParams[8] = new SqlParameter("@P_LOCATION", objProf.LOCATION);
                        objParams[9] = new SqlParameter("@P_SPONSOREDBY", objProf.SPONSOREDBY);

                        objParams[10] = new SqlParameter("@P_PROGRAM_LEVEL", objProf.PROGRAM_LEVEL);
                        objParams[11] = new SqlParameter("@P_PROGRAM_TYPE", objProf.PROGRAM_TYPE);
                        objParams[12] = new SqlParameter("@P_PRESENTATION_DETAILS", objProf.PRESENTATION_DETAILS);

                        objParams[13] = new SqlParameter("@P_DURATION", objProf.DURATION);
                        objParams[14] = new SqlParameter("@P_ROLENAME", objProf.ROLENAME);
                        objParams[15] = new SqlParameter("@P_COST_INVOLVED", objProf.COST_INVOLVED);

                        objParams[16] = new SqlParameter("@P_INTERNAL_FACULTY", objProf.InternalFaculty);
                        objParams[17] = new SqlParameter("@P_EXTERNAL_FACULTY", objProf.ExternalFaculty);
                        objParams[18] = new SqlParameter("@P_INTERNAL_STUDENT", objProf.InternalStudent);
                        objParams[19] = new SqlParameter("@P_EXTERNAL_STUDENT", objProf.ExternalStudent);


                        objParams[20] = new SqlParameter("@P_PAYROLL_SB_PROFESSIONAL_COURSE_DOCUMENT_UPLOAD", dt);


                        objParams[21] = new SqlParameter("@P_PROFESSIONAL_BODY", objProf.PROFESSIONALBODY);
                        objParams[22] = new SqlParameter("@P_ATTACHMENT", objProf.ATTACHMENTS);

                        objParams[23] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[24] = new SqlParameter("@P_MODE", objProf.MODE);
                        objParams[25] = new SqlParameter("@P_AcadYear", objProf.PASSYEAR);
                        objParams[26] = new SqlParameter("@P_PARTITION_TYPE", objProf.PARTITION_TYPE);
                        objParams[27] = new SqlParameter("@P_ThemeOfTrainingAttended", objProf.ThemeOfTrainingAttended);
                        objParams[28] = new SqlParameter("@P_ISBLOB", objProf.ISBLOB);
                        objParams[29] = new SqlParameter("@P_PNO", SqlDbType.Int);
                        objParams[29].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_PROFESSIONAL_COURSE", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddProfessionalCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateProfessionalCourse(ServiceBook objProf, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_IDNO", objProf.IDNO);
                        objParams[1] = new SqlParameter("@P_COURSE", objProf.COURSE);
                        objParams[2] = new SqlParameter("@P_INST", objProf.INST);
                        if (!objProf.FDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objProf.FDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objProf.TDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objProf.TDT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REMARK", objProf.REMARK);
                        objParams[6] = new SqlParameter("@P_PNO", objProf.PNO);
                        objParams[7] = new SqlParameter("@P_SPONSORED_AMOUNT", objProf.SPONSORED_AMOUNT);
                        objParams[8] = new SqlParameter("@P_LOCATION", objProf.LOCATION);
                        objParams[9] = new SqlParameter("@P_SPONSOREDBY", objProf.SPONSOREDBY);

                        objParams[10] = new SqlParameter("@P_PROGRAM_LEVEL", objProf.PROGRAM_LEVEL);
                        objParams[11] = new SqlParameter("@P_PROGRAM_TYPE", objProf.PROGRAM_TYPE);
                        objParams[12] = new SqlParameter("@P_PRESENTATION_DETAILS", objProf.PRESENTATION_DETAILS);

                        objParams[13] = new SqlParameter("@P_DURATION", objProf.DURATION);
                        objParams[14] = new SqlParameter("@P_ROLENAME", objProf.ROLENAME);
                        objParams[15] = new SqlParameter("@P_COST_INVOLVED", objProf.COST_INVOLVED);

                        objParams[16] = new SqlParameter("@P_INTERNAL_FACULTY", objProf.InternalFaculty);
                        objParams[17] = new SqlParameter("@P_EXTERNAL_FACULTY", objProf.ExternalFaculty);
                        objParams[18] = new SqlParameter("@P_INTERNAL_STUDENT", objProf.InternalStudent);
                        objParams[19] = new SqlParameter("@P_EXTERNAL_STUDENT", objProf.ExternalStudent);

                        objParams[20] = new SqlParameter("@P_PAYROLL_SB_PROFESSIONAL_COURSE_DOCUMENT_UPLOAD", dt);

                        objParams[21] = new SqlParameter("@P_PROFESSIONAL_BODY", objProf.PROFESSIONALBODY);
                        objParams[22] = new SqlParameter("@P_MODE", objProf.MODE);
                        objParams[23] = new SqlParameter("@P_AcadYear", objProf.PASSYEAR);
                        objParams[24] = new SqlParameter("@P_PARTITION_TYPE", objProf.PARTITION_TYPE);
                        objParams[25] = new SqlParameter("@P_ThemeOfTrainingAttended", objProf.ThemeOfTrainingAttended);
                        objParams[26] = new SqlParameter("@P_ISBLOB", objProf.ISBLOB);
                        objParams[27] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_PROFESSIONAL_COURSE", objParams, true);
                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateProfessionalCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllProfessionalCourseDetail(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PROFESSIONAL_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllProfessionalCourseDetail-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleProfessionalCourseDetailsOfEmployee(int pNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", pNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_PROFESSIONAL_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleProfessionalCourseDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteProfessionalCourse(int pNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", pNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_PROFESSIONAL_COURSE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteTrainingConducted.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddCurrentAppointment(ServiceBook objCurrent)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_IDNO", objCurrent.IDNO);

                        if (!objCurrent.FDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FDT", objCurrent.FDT);
                        else
                            objParams[1] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objCurrent.TDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_TDT", objCurrent.TDT);
                        else
                            objParams[2] = new SqlParameter("@P_TDT", DBNull.Value);

                        //objParams[3] = new SqlParameter("@P_APPOINTMENT", objCurrent.APPOINTMENT);
                        objParams[3] = new SqlParameter("@P_APPOINTMENTMODE", objCurrent.APPOINTMENTMODE);
                        //objParams[5] = new SqlParameter("@P_COMMITTEEDETAILS", objCurrent.COMMITTEEDETAILS);
                        objParams[4] = new SqlParameter("@P_COMMITTEEMEMBER", objCurrent.COMMITTEEMEMBER);
                        // objParams[7] = new SqlParameter("@P_ADVERTISEMENT", objCurrent.ADVERTISEMENT);


                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objCurrent.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_NEWSPAPER", objCurrent.NEWSPAPER);
                        objParams[7] = new SqlParameter("@P_DATE", objCurrent.AVDATE);

                        objParams[8] = new SqlParameter("@P_REFERENCE", objCurrent.REFERENCE);
                        //objParams[12] = new SqlParameter("@P_AUTHORITYNAME", objCurrent.AUTHORITYNAME);
                        objParams[9] = new SqlParameter("@P_APPOINTMENTDDATE", objCurrent.APPOINTMENTDDATE);
                        objParams[10] = new SqlParameter("@P_APPNO", objCurrent.APPNO);
                        objParams[11] = new SqlParameter("@P_POST", objCurrent.POSTNAME);
                        objParams[12] = new SqlParameter("@P_APPSTATUS", objCurrent.APPSTATUS);
                        objParams[13] = new SqlParameter("@P_PAYSCALE", objCurrent.PAYSCALE);
                        objParams[14] = new SqlParameter("@P_Department", objCurrent.DEPARTMENT);
                        objParams[15] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[16] = new SqlParameter("@P_UNIVERSITYAPPNO", objCurrent.UNIVERSITYAPPNO);
                        objParams[17] = new SqlParameter("@P_UNIAPPDT", objCurrent.UNIAPPDT);
                        objParams[18] = new SqlParameter("@P_UNIVERSITYATACHMENT", objCurrent.UNIVERSITYATACHMENT);
                        objParams[19] = new SqlParameter("@P_PGAPPNO", objCurrent.PGAPPNO);
                        objParams[20] = new SqlParameter("@P_PGTAPPDT", objCurrent.PGTAPPDT);
                        objParams[21] = new SqlParameter("@P_PGTATTACHMENT", objCurrent.PGTATTACHMENT);
                        objParams[22] = new SqlParameter("@P_UNIAPPSTATUS", objCurrent.UNIAPPSTATUS);
                        objParams[23] = new SqlParameter("@P_PGTAPPSTATUS", objCurrent.PGTAPPSTATUS);
                        objParams[24] = new SqlParameter("@P_EXPERIENCE", objCurrent.EXPERIENCE);
                        objParams[25] = new SqlParameter("@P_EXPERIENCETYPE", objCurrent.EXPERIENCETYPE);
                        //objParams[30] = new SqlParameter("@P_PAYROLL_SB_CURRENTAPP_DOCUMENT_UPLOAD_RECORD", dt);
                        objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_CURRENT_APPOINTMENT_STATUS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddCurrentAppointment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateCurrentAppointment(ServiceBook objCurrent)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_IDNO", objCurrent.IDNO);

                        if (!objCurrent.FDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FDT", objCurrent.FDT);
                        else
                            objParams[1] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objCurrent.TDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_TDT", objCurrent.TDT);
                        else
                            objParams[2] = new SqlParameter("@P_TDT", DBNull.Value);

                        //  objParams[3] = new SqlParameter("@P_APPOINTMENT", objCurrent.APPOINTMENT);
                        objParams[3] = new SqlParameter("@P_APPOINTMENTMODE", objCurrent.APPOINTMENTMODE);
                        // objParams[5] = new SqlParameter("@P_COMMITTEEDETAILS", objCurrent.COMMITTEEDETAILS);
                        objParams[4] = new SqlParameter("@P_COMMITTEEMEMBER", objCurrent.COMMITTEEMEMBER);
                        // objParams[7] = new SqlParameter("@P_ADVERTISEMENT", objCurrent.ADVERTISEMENT);


                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objCurrent.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_NEWSPAPER", objCurrent.NEWSPAPER);
                        objParams[7] = new SqlParameter("@P_DATE", objCurrent.AVDATE);

                        objParams[8] = new SqlParameter("@P_REFERENCE", objCurrent.REFERENCE);
                        //objParams[12] = new SqlParameter("@P_AUTHORITYNAME", objCurrent.AUTHORITYNAME);
                        objParams[9] = new SqlParameter("@P_APPOINTMENTDDATE", objCurrent.APPOINTMENTDDATE);
                        objParams[10] = new SqlParameter("@P_APPNO", objCurrent.APPNO);
                        objParams[11] = new SqlParameter("@P_POST", objCurrent.POSTNAME);
                        objParams[12] = new SqlParameter("@P_APPSTATUS", objCurrent.APPSTATUS);
                        objParams[13] = new SqlParameter("@P_PAYSCALE", objCurrent.PAYSCALE);
                        objParams[14] = new SqlParameter("@P_Department", objCurrent.DEPARTMENT);
                        objParams[15] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[16] = new SqlParameter("@P_UNIVERSITYAPPNO", objCurrent.UNIVERSITYAPPNO);
                        objParams[17] = new SqlParameter("@P_UNIAPPDT", objCurrent.UNIAPPDT);
                        objParams[18] = new SqlParameter("@P_UNIVERSITYATACHMENT", objCurrent.UNIVERSITYATACHMENT);
                        objParams[19] = new SqlParameter("@P_PGAPPNO", objCurrent.PGAPPNO);
                        objParams[20] = new SqlParameter("@P_PGTAPPDT", objCurrent.PGTAPPDT);
                        objParams[21] = new SqlParameter("@P_PGTATTACHMENT", objCurrent.PGTATTACHMENT);
                        objParams[22] = new SqlParameter("@P_UNIAPPSTATUS", objCurrent.UNIAPPSTATUS);
                        objParams[23] = new SqlParameter("@P_PGTAPPSTATUS", objCurrent.PGTAPPSTATUS);
                        objParams[24] = new SqlParameter("@P_EXPERIENCE", objCurrent.EXPERIENCE);
                        objParams[25] = new SqlParameter("@P_EXPERIENCETYPE", objCurrent.EXPERIENCETYPE);
                        objParams[26] = new SqlParameter("@P_CANO", objCurrent.CANO);
                        //objParams[31] = new SqlParameter("@P_PAYROLL_SB_CURRENTAPP_DOCUMENT_UPLOAD_RECORD", dt);
                        objParams[27] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_CURRENT_APPOINTMENT_STATUS", objParams, true);

                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret) == -1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (Convert.ToInt32(ret) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdatePayRevision-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllCurrentAppointmentDetailsOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_CURRENT_APPOINTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllPreServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleCurrentAppoinmentDetailsOfEmployee(int caNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CANO", caNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_CURRENTAPPOINTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSinglePreServiceDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteCurrentAppointment(int caNo)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CANO", caNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_CURRENT_APPOINTMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeletePreService.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region DocumentUpload

                public DataSet RetrieveAllDocument(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_RETRIVE_ALL_DOCUMENT_SERVICEBOOK", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllHolodays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public int AddUpdateEmployeeDocumentsDetailNew(int idno, int hiddtudocno, string extension, string contentType, string filename, string path, ServiceBook objDoc)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_EMP_DOC_NO", hiddtudocno);
                        // objParams[2] = new SqlParameter("@P_CHK", chkDocuments);
                        objParams[2] = new SqlParameter("@P_EXTENSION", extension);
                        objParams[3] = new SqlParameter("@P_CONTENTTYPE", contentType);
                        // objParams[5] = new SqlParameter("@P_FILEDATA", document);
                        objParams[4] = new SqlParameter("@P_FILEPATH", path);
                        objParams[5] = new SqlParameter("@P_FILENAME", filename);
                        objParams[6] = new SqlParameter("@P_UPLOADED", objDoc.UPLOADED);


                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_INSERT_UPDATE_EMPLOYEE_FILE_UPLOAD_DOCUMENT", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddUpdateStudentDocumentsDetail-> " + ex.ToString());
                    }
                    return retStatus;
                    //throw new NotImplementedException();
                }

                #endregion

                #region University

                public int AddUniversity(ServiceBook objSevBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UNIVERSITY", objSevBook.UNIVERSITY);
                        objParams[1] = new SqlParameter("@P_ACTIVESTATUS", objSevBook.ACTIVESTATUS);
                        objParams[2] = new SqlParameter("@P_UNIVERSITYNO", objSevBook.UNIVERSITYNO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objSevBook.UCOLLEGE_CODE);

                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UNIVERSITY_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddUniversity -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetUniversity()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_UNIVERSITY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleUniversity(int uno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UNIVERSITYNO", uno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_UNIVERSITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeletUniversity(int sno)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UNIVERSITYNO", sno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_UNIVERSITY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteSuspension.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region EmployeeServicebookSearch
                public DataSet GetSingleEmployeePersonalDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SB_SP_RET_EMPLOYEE_PERSONAL_DETAILS_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllDeptExamDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region ServiceBook Approval

                #region family
                public int FamilyParticularApproval(int FNNO, int IDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FNNO", FNNO);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_FAMILYINFO_APPROVAL", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int FamilyParticularReject(int FNNO, int IDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FNNO", FNNO);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_FAMILYINFO_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Nomination
                public int NominationDetailsApproval(int nfno, int NOMIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_NFNO", nfno);
                        objParams[1] = new SqlParameter("@P_IDNO", NOMIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_NOMINATION_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int NominationDetailsReject(int nfno, int NOMIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_NFNO", nfno);
                        objParams[1] = new SqlParameter("@P_IDNO", NOMIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_NOMINATION_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region qualification
                public int QualificationDetailsApproval(int QNO, int QUAIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_QNO", QNO);
                        objParams[1] = new SqlParameter("@P_IDNO", QUAIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_QUALIFICATION_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int QualificationDetailsReject(int QNO, int QUAIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_QNO", QNO);
                        objParams[1] = new SqlParameter("@P_IDNO", QUAIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_QUALIFICATION_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region DEPT EXAM
                public int DeptExamDetailsApproval(int DENO, int DEPTIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DENO", DENO);
                        objParams[1] = new SqlParameter("@P_IDNO", DEPTIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_DEPT_EXAM_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int DeptExamDetailsReject(int DENO, int DEPTIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DENO", DENO);
                        objParams[1] = new SqlParameter("@P_IDNO", DEPTIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_DEPT_EXAM_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Previous Exp
                public int PrevExpDetailsApproval(int psno, int PREVIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PSNO", psno);
                        objParams[1] = new SqlParameter("@P_IDNO", PREVIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PREV_EXP_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int PrevExpDetailsReject(int psno, int PREVIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PSNO", psno);
                        objParams[1] = new SqlParameter("@P_IDNO", PREVIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PREV_EXP_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.FamilyParticularApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Administrative Responsibilities

                public int AdminResponDetailsApproval(int ADMINTRXNO, int ADMIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMINTRXNO", ADMINTRXNO);
                        objParams[1] = new SqlParameter("@P_IDNO", ADMIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_ADMIN_RESPONSIBILITIES_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AdminResponDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int AdminResponDetailsReject(int ADMINTRXNO, int ADMIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMINTRXNO", ADMINTRXNO);
                        objParams[1] = new SqlParameter("@P_IDNO", ADMIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_ADMIN_RESPONSIBILITIES_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AdminResponDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Publication Details
                public int PublicationDetailsApproval(int PUBTRXNO, int PUBIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PUBTRXNO", PUBTRXNO);
                        objParams[1] = new SqlParameter("@P_IDNO", PUBIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PUBLICATION_DETAILS_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AdminResponDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int PublicationDetailsReject(int PUBTRXNO, int PUBIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PUBTRXNO", PUBTRXNO);
                        objParams[1] = new SqlParameter("@P_IDNO", PUBIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PUBLICATION_DETAILS_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AdminResponDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region TrainingAttended
                public int TrainingAttendedDetailsApproval(int tno, int TRATTIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", tno);
                        objParams[1] = new SqlParameter("@P_IDNO", TRATTIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_TRAINING_ATTENDED_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.TrainingAttendedDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int TrainingAttendedDetailsReject(int tno, int TRATTIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", tno);
                        objParams[1] = new SqlParameter("@P_IDNO", TRATTIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_TRAINING_ATTENDED_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.TrainingAttendedDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Guest Lecture
                public int GuestLectureDetailsApproval(int INVTRXNO, int INVTIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_INVTRXNO", INVTRXNO);
                        objParams[1] = new SqlParameter("@P_IDNO", INVTIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_INVITED_TALK_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int GuestLectureDetailsReject(int INVTRXNO, int INVTIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_INVTRXNO", INVTRXNO);
                        objParams[1] = new SqlParameter("@P_IDNO", INVTIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_INVITED_TALK_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Training Conducted Appr
                public int TrainingConductedDetailsApproval(int tno, int TRCONIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", tno);
                        objParams[1] = new SqlParameter("@P_IDNO", TRCONIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_TRAINING_CONDUCTED_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int TrainingConductedDetailsReject(int tno, int TRCONIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", tno);
                        objParams[1] = new SqlParameter("@P_IDNO", TRCONIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_TRAINING_CONDUCTED_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Consultancy
                public int ConsultancyDetailsApproval(int SCNO, int SCIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCNO", SCNO);
                        objParams[1] = new SqlParameter("@P_IDNO", SCIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_STAFF_CONSULTANCY_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int ConsultancyDetailsReject(int SCNO, int SCIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCNO", SCNO);
                        objParams[1] = new SqlParameter("@P_IDNO", SCIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_STAFF_CONSULTANCY_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Accomplishment
                public int AccomplishmentDetailsApproval(int ACNO, int ACIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACNO", ACNO);
                        objParams[1] = new SqlParameter("@P_IDNO", ACIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_ACCOMPLISHMENT_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int AccomplishmentDetailsReject(int ACNO, int ACIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACNO", ACNO);
                        objParams[1] = new SqlParameter("@P_IDNO", ACIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_ACCOMPLISHMENT_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.GuestLectureDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Professional Membership
                public int ProfMemberDetailsApproval(int MPNO, int MPIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MPNO", MPNO);
                        objParams[1] = new SqlParameter("@P_IDNO", MPIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PROF_MEMBERSHIP_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int ProfMemberDetailsReject(int MPNO, int MPIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MPNO", MPNO);
                        objParams[1] = new SqlParameter("@P_IDNO", MPIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PROF_MEMBERSHIP_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Funded Project
                public int StaffFundedDetailsApproval(int SFNO, int SFIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SFNO", SFNO);
                        objParams[1] = new SqlParameter("@P_IDNO", SFIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_STAFF_FUNDED_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int StaffFundedDetailsReject(int SFNO, int SFIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SFNO", SFNO);
                        objParams[1] = new SqlParameter("@P_IDNO", SFIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_STAFF_FUNDED_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Patent
                public int PatentDetailsApproval(int PCNO, int PCIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PCNO", PCNO);
                        objParams[1] = new SqlParameter("@P_IDNO", PCIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PATENT_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int PatentDetailsReject(int PCNO, int PCIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PCNO", PCNO);
                        objParams[1] = new SqlParameter("@P_IDNO", PCIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PATENT_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Institute Experience
                public int InstituteExpDetailsApproval(int SVCNO, int SVCIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SVCNO", SVCNO);
                        objParams[1] = new SqlParameter("@P_IDNO", SVCIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_INSTITUTE_EXPERIENCE_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int InstituteExpDetailsReject(int SVCNO, int SVCIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SVCNO", SVCNO);
                        objParams[1] = new SqlParameter("@P_IDNO", SVCIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_INSTITUTE_EXPERIENCE_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Loans And Advance
                public int LoansAdvDetailsApproval(int lno, int LADIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_LNO", lno);
                        objParams[1] = new SqlParameter("@P_IDNO", LADIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_LOANS_AND_ADVANCE_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int LoansAdvDetailsReject(int lno, int LADIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_LNO", lno);
                        objParams[1] = new SqlParameter("@P_IDNO", LADIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_LOANS_AND_ADVANCE_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Pay Revision
                public int PayRevisionDetailsApproval(int PRNO, int PRIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PRNO", PRNO);
                        objParams[1] = new SqlParameter("@P_IDNO", PRIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PAY_REVISION_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int PayRevisionDetailsReject(int PRNO, int PRIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PRNO", PRNO);
                        objParams[1] = new SqlParameter("@P_IDNO", PRIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_PAY_REVISION_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Increment/Termination
                public int IncrementDetailsApproval(int TRNO, int TRIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objParams[1] = new SqlParameter("@P_IDNO", TRIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_INCREMENT_TERMINATION_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int IncrementDetailsReject(int TRNO, int TRIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objParams[1] = new SqlParameter("@P_IDNO", TRIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_INCREMENT_TERMINATION_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #region Matters
                public int MattersDetailsApproval(int mno, int MIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MNO", mno);
                        objParams[1] = new SqlParameter("@P_IDNO", MIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_MATTER_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsApproval-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int MattersDetailsReject(int mno, int MIDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MNO", mno);
                        objParams[1] = new SqlParameter("@P_IDNO", MIDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_MATTER_REJECT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ProfMemberDetailsReject-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion

                #endregion


                #region ServiceBook status Update

                public int ServiceBookstatusUpdate(int key, int IDNO, string type, string status)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_KEY", key);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_type", type);
                        objParams[3] = new SqlParameter("@P_status", status);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SB_SERVICEBOOKSTATUS_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.ServiceBookstatusUpdate-> " + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                public DataSet BindServiceBookStatus(int usertypeid, int idnoEmp)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UserTypeId", usertypeid);
                        objParams[1] = new SqlParameter("@P_IDNO", idnoEmp);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SB_GET_SERVICEBOOKSTATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EMP_Attandance_Controller.GetShiftList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDepartmentName(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[1];
                        objParam[0] = new SqlParameter("@P_UANO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_DEPT_NAME_FOR_USERTYPE", objParam);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetDepartmentName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmployeeListForDept(int userno, int shiftmanagement)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[2];
                        objParam[0] = new SqlParameter("@P_UANO", userno);
                        objParam[1] = new SqlParameter("@P_SHIFTMANAGEMENT", shiftmanagement);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_DEPT_WISE_EMPLOYEE_NAME", objParam);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //Added by Piyush Thakre 29/02/2024
                #region Payroll_SB_RevenueGenerated
                public int AddRevenueGenerated(ServiceBook objRevenue, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objRevenue.IDNO);
                        objParams[1] = new SqlParameter("@P_YEAR", objRevenue.YEAR);
                        objParams[2] = new SqlParameter("@P_RGT_VAC", objRevenue.RGVAC);
                        objParams[3] = new SqlParameter("@P_RGT_EVENT", objRevenue.RGEVENTS);
                        objParams[4] = new SqlParameter("@P_RGT_SPONSOR", objRevenue.RGSPONSORSHIP);
                        objParams[5] = new SqlParameter("@P_WEBLINK", objRevenue.WEBLINK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objRevenue.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objRevenue.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_FILEPATH ", objRevenue.FILEPATH);
                        objParams[9] = new SqlParameter("@P_ISBLOB", objRevenue.ISBLOB);
                        objParams[10] = new SqlParameter("@P_PAYROLL_SB_REVENUE_GENERATED_DOCUMENT_UPLOAD", dt);
                        objParams[11] = new SqlParameter("@P_RGNO", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SB_REVENUE_GENERATED", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddRevenueGenerated-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateRevenueGenerated(ServiceBook objRevenue, DataTable dt)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objRevenue.IDNO);
                        objParams[1] = new SqlParameter("@P_RGNO", objRevenue.RGNO);
                        objParams[2] = new SqlParameter("@P_YEAR", objRevenue.YEAR);
                        objParams[3] = new SqlParameter("@P_RGT_VAC", objRevenue.RGVAC);
                        objParams[4] = new SqlParameter("@P_RGT_EVENT", objRevenue.RGEVENTS);
                        objParams[5] = new SqlParameter("@P_RGT_SPONSOR", objRevenue.RGSPONSORSHIP);
                        objParams[6] = new SqlParameter("@P_WEBLINK", objRevenue.WEBLINK);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objRevenue.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_ATTACHMENT", objRevenue.ATTACHMENTS);
                        objParams[9] = new SqlParameter("@P_FILEPATH", objRevenue.FILEPATH);
                        objParams[10] = new SqlParameter("@P_ISBLOB", objRevenue.ISBLOB);
                        objParams[11] = new SqlParameter("@P_PAYROLL_SB_REVENUE_GENERATED_DOCUMENT_UPLOAD", dt);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SB_SB_REVENUE_GENERATED", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            if (Convert.ToInt32(ret) > 0)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateRevenueGenerated-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllRevenueGeneratedOfEmployee(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_REVENUE_GENERATED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllRevenueGeneratedOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleRevenueGeneratedOfEmployee(int RGNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RGNO", RGNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_SB_REVENUE_GENERATED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleRevenueGeneratedOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteRevenueGenerated(int RGNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RGNO", RGNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_SB_REVENUE_GENERATED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteRevenueGenerated.-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                //Added by Sonal Banode on 29-03-2024

                public DataSet GetServieBookPageName(int usertype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[1];
                        objParam[0] = new SqlParameter("@P_USERTYPE", usertype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_SERVICEBOOK_PAGE_NAME", objParam);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetServieBookPageName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //

                //Added  by Sonal Banode 01/04/2024
                #region PAYROLL_SB_ACADEMIC_RESPONSIBILITIES

                public int AddAcademicReponsibilities(ServiceBook objAcademic)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objAcademic.IDNO);
                        objParams[1] = new SqlParameter("@P_RESPONSIBILITY", objAcademic.RESPONSIBILITY);
                        objParams[2] = new SqlParameter("@P_DEPTLEVEL", objAcademic.DEPTLEVEL);

                        if (!objAcademic.FROMDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROMDATE", objAcademic.FROMDATE);
                        else
                            objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objAcademic.TODATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TODATE", objAcademic.TODATE);
                        else
                            objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);


                        objParams[5] = new SqlParameter("@P_REMARK", objAcademic.REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objAcademic.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAcademic.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[9] = new SqlParameter("@P_Is_Current", objAcademic.IsCurrent);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objAcademic.FILEPATH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objAcademic.ISBLOB);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_INST_ACADEMIC_RESPONSIBILITIES", objParams, false);
                        {
                            if (ret != null)
                            {
                                if (ret.ToString().Equals("-1"))
                                {
                                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                                }
                                else if (ret.ToString().Equals("1"))
                                {
                                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.AddAdminResponsibilities-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAcademicResponsibilities(ServiceBook objAcademic)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_ACDNO", objAcademic.ACDNO);
                        objParams[1] = new SqlParameter("@P_RESPONSIBILITY", objAcademic.RESPONSIBILITY);
                        objParams[2] = new SqlParameter("@P_DEPTLEVEL", objAcademic.DEPTLEVEL);
                        if (!objAcademic.FROMDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROMDATE", objAcademic.FROMDATE);
                        else
                            objParams[3] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objAcademic.TODATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TODATE", objAcademic.TODATE);
                        else
                            objParams[4] = new SqlParameter("@P_TODATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_REMARK", objAcademic.REMARK);

                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objAcademic.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objAcademic.ATTACHMENTS);
                        objParams[8] = new SqlParameter("@P_IDNO", objAcademic.IDNO);
                        objParams[9] = new SqlParameter("@P_Is_Current", objAcademic.IsCurrent);
                        objParams[10] = new SqlParameter("@P_FILEPATH", objAcademic.FILEPATH);
                        objParams[11] = new SqlParameter("@P_ISBLOB", objAcademic.ISBLOB);
                        objParams[12] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_UPD_ACADEMIC_RESPONSIBILITIES", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if (ret.ToString().Equals("1"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.UpdateAdminResponsibilities-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteAcademicResponsibilities(int ACADNO)
                {
                    int retStatus = 0;

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ACDNO", ACADNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_SB_DEL_ACADEMIC_RESPONSIBILITIES", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ServiceBookController.DeleteAdminResponsibilities.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAcademicResponsibilities(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETALL_ACADEMIC_RESPONSIBILITIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAdminResponsibilities-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleAcademicResponsibilities(int acdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ACDNO", acdno);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GETSINGLE_ACADEMIC_RESPONSIBILITIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetSingleAcademicResponsibilities-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllAcademicResponsibilitiesCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_ACADEMIC_RESPONSIBILITIES_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAcademicResponsibilitiesCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Responsibility Master
                public int AddResponsibility(ServiceBook objResp)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_RESPONSIBILITIES", objResp.RESPONSIBILITY);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objResp.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_CREATEDBY", objResp.CREATEDBY);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_RESPONSIBILITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.AddResponsibility-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateResponsibility(ServiceBook objResp)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_RNO", objResp.RNO);
                        objParams[1] = new SqlParameter("@P_RESPONSIBILITIES", objResp.RESPONSIBILITY);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objResp.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_MODIFYBY", objResp.MODIFYBY);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_RESPONSIBILITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.UpdateResponsibility-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllResponsibility()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_RESPONSIBILITY", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetAllProgramType-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public DataSet GetSingleResponsibility(int RNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RNO", RNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_RESPONSIBILITY_BY_RNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSingleResponsibility->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion
                //

                #region IPRCateMaster
                public DataSet GetIPRCategory()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IPRCategory", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleIPRCategory(int IPRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IPRNO", IPRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_IPRCategory", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddIPRCategory(ServiceBook objSevBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IPRCategory", objSevBook.IPRCategory);
                        objParams[1] = new SqlParameter("@P_ACTIVESTATUS", objSevBook.ACTIVESTATUS);
                        objParams[2] = new SqlParameter("@P_IPRNO", objSevBook.IPRNO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objSevBook.UCOLLEGE_CODE);

                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_IPRCategory_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddUniversity -> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region IPRIssuingAgency

                public DataSet GetIIssuingAgency()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IPRIssuingAgency", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleIPRAgency(int IPRNOAGNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IPRNOAGNO", IPRNOAGNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SINGLE_IPRIssuingAgency", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddIPRAgency(ServiceBook objSevBook)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IPRIssuing_Agency", objSevBook.IPRIssuingAgency);
                        objParams[1] = new SqlParameter("@P_ACTIVESTATUS", objSevBook.ACTIVESTATUS);
                        objParams[2] = new SqlParameter("@P_IPRNOAGNO", objSevBook.IPRNOAGNO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objSevBook.UCOLLEGE_CODE);

                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_IPRIssuingAgency_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddUniversity -> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                //Added by Sonal Banode on 08-04-2024

                public DataSet GetAllMiscellaneousCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_MISCELLANEOUSDETAIL_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllMiscellaneousCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //

                #region Service Book Config

                public DataSet GetServiceBookConfigurationForRestrict(int usertype, string CommandType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USERTYPE", usertype);
                        objParams[1] = new SqlParameter("@P_COMMDANDTYPE", CommandType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_CONFIG_FOR_SERVICEBOOK_EDIT_APPROVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetServiceBookConfigurationForRestrict-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                //Added by Sonal Banode on 12-04-2024
                public DataSet GetAllProfessionalCourseCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_PROFESSIONAL_COURSE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllProfessionalCourseCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllAvishkarCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_AVISHKAR_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAvishkarCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllAwardCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_AWARD_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllAwardCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllCurrentAppointmentCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_CURRENT_APPOINTMENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllCurrentAppointmentCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllResearchCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_RESEARCH_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllResearchCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllRevenueCount()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_SB_REVENUE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ServiceBookController.GetAllRevenueCount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //
            }

        }
    }
}




