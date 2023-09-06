//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[ChangeInMasterFileController]                                  
// CREATION DATE : 26-JULY-2009                                                        
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
            public class ChangeInMasterFileController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetEmployeesForAmountDeduction(string PayHead, int Staff, string order, string PayRule, string Dept, int collegeNo, int subHeadNo,int empTypeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_PayHead", PayHead);
                        objParams[1] = new SqlParameter("@P_Staff", Staff);
                        objParams[2] = new SqlParameter("@P_ORDERBY", order);
                        objParams[3] = new SqlParameter("@P_PayRule", PayRule);                       
                        objParams[4] = new SqlParameter("@P_Dept", Dept);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[6] = new SqlParameter("@P_SubHeadNo", subHeadNo);
                        objParams[7] = new SqlParameter("@P_EMPTYPENO", empTypeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_PAYHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetEmployeesForAmountDeductionWithMultipleHead(int Staff, string order, string PayRule, int collegeNo, int empTypeno, int subdeptno, string HeadType, int userid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_Staff", Staff);
                        objParams[1] = new SqlParameter("@P_ORDERBY", order);
                        objParams[2] = new SqlParameter("@P_PayRule", PayRule);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[4] = new SqlParameter("@P_EMPTYPENO", empTypeno);
                        objParams[5] = new SqlParameter("@P_SUBDEPTNO", subdeptno);
                        objParams[6] = new SqlParameter("@P_HEADTYPE", HeadType);
                        objParams[7] = new SqlParameter("@P_UA_NO", userid);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_PAYHEAD_WITH_MULTIPLEHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdatePayHeadAmountIncome_Bulk(DataTable dt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@PayrollPaymasTbl", dt);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYHEAD_AMOUNT_INCOME_BULK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdatePayHeadAmountDeduction_Bulk(DataTable dt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@PayrollPaymasTbl", dt);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYHEAD_AMOUNT_DEDUCTION_BULK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdatePayHeadAmount(string payHead,decimal amount,int idNo)
                {
                    int retStatus = 0;
                    try
                    {     
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PayHead", payHead);
                        objParams[1] = new SqlParameter("@P_AMOUNT", amount);
                        objParams[2] = new SqlParameter("@P_IDNO", idNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYHEAD_AMOUNT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // TO ADD BULK UPDATE EMPLOYEE FACILITY
                public DataSet GetEmployeesForEditFields(int Staff, string payheadfield, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_Staff", Staff);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", payheadfield);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_PAYMAS_FIELD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdatePayEmpmasFields(string payHead, string field, int idNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PayHead", payHead);
                        objParams[1] = new SqlParameter("@P_FIELD", field);
                        objParams[2] = new SqlParameter("@P_IDNO", idNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYMAS_FIELDS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePayPaymasFieldsScaleRule(string payHead, string field, int idNo, string payRule)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PayHead", payHead);
                        objParams[1] = new SqlParameter("@P_FIELD", field);
                        objParams[2] = new SqlParameter("@P_IDNO", idNo);
                        objParams[3] = new SqlParameter("@P_PAYRULE", payRule);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYMAS_FIELDS_SCALE_RULE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Insert Sub Pay Head in Installment table from Monthly Changes In Master File page
                public int AddSubPayheadAmount(int IDNO, string PAYHEAD, string CODE, decimal MONAMT, int SUBHEADNO, int collegeNo, int staffNo, bool isMainHead)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", PAYHEAD);
                        objParams[2] = new SqlParameter("@P_CODE", CODE);
                        objParams[3] = new SqlParameter("@P_MONAMT", MONAMT);
                        objParams[4] = new SqlParameter("@P_SUBHEADNO", SUBHEADNO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[6] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[7] = new SqlParameter("@P_ISMAINHEAD", isMainHead);
                        objParams[8] = new SqlParameter("@P_INO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_SUBPAYHEAD_AMOUNT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayHeadPrivilegesController.AddUser-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetEmployeesForNoDues(int Staff, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Staff", Staff);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EMPLOYEE_NODUES_AUTHO_TYPE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }
                 
                public int UpdateNoDuesAuthoType(int AuthoTypeId, int idNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_AUTHO_TYPE", AuthoTypeId);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_UPD_EMP_AUTHOTYPE_DEATAIL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }
                
            }
        }
    }
}
