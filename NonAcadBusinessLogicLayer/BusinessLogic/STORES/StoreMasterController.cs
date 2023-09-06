//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : StoreMasterController.cs                                                    
// CREATION DATE : 05-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;



namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class StoreMasterController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                //Leaves objLeave = new Leaves();
                //LeavesController 


                //private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                #region Sore REference
                public int AddStoreRefference(int MDNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[1] = new SqlParameter("@P_REFID", 1);
                        objParams[1].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_REFFERENCE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                    }
                    return retStatus;
                }
                #endregion

                #region STORE_DEPARTMENTUSER

                //public int AddDeptUser(int mdNo, string uaName, int uaNO, int aprlNo,string colCode,string userid)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New STORE_DEPT_PRIVELEGE
                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_MDNO", mdNo);
                //        objParams[1] = new SqlParameter("@P_UA_NAME",uaName);
                //        objParams[2] = new SqlParameter("@P_UA_NO",uaNO);
                //        objParams[3] = new SqlParameter("@P_APLNO", aprlNo);
                //        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                //        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                //        objParams[6] = new SqlParameter("@P_DUNO", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENTUSER_INSERT", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddDeptPrivelege-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                public int AddDeptUser(int mdNo, string uaName, int uaNO, int aprlNo, string colCode, string userid, int isapproval)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New STORE_DEPT_PRIVELEGE
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_MDNO", mdNo);
                        objParams[1] = new SqlParameter("@P_UA_NAME", uaName);
                        objParams[2] = new SqlParameter("@P_UA_NO", uaNO);
                        objParams[3] = new SqlParameter("@P_APLNO", aprlNo);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        objParams[6] = new SqlParameter("@P_ISAPPROVAL", isapproval);

                        objParams[7] = new SqlParameter("@P_DUNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENTUSER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddDeptPrivelege-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UpdateDeptUser(int mdNo, string uaName, int uaNO, int aprlNo, string colCode, int duNo,string userid)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        //Update  STORE_DEPT_PRIVELEGE
                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_DUNO", duNo);
                //        objParams[1] = new SqlParameter("@P_MDNO", mdNo);
                //        objParams[2] = new SqlParameter("@P_UA_NAME", uaName);
                //        objParams[3] = new SqlParameter("@P_UA_NO", uaNO);
                //        objParams[4] = new SqlParameter("@P_APLNO", aprlNo);
                //        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                //        objParams[6] = new SqlParameter("@P_USER_ID", userid);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENTUSER_UPDATE", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDeptPrivelege-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int UpdateDeptUser(int mdNo, string uaName, int uaNO, int aprlNo, string colCode, int duNo, string userid, int isapproval)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update  STORE_DEPT_PRIVELEGE
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_DUNO", duNo);
                        objParams[1] = new SqlParameter("@P_MDNO", mdNo);
                        objParams[2] = new SqlParameter("@P_UA_NAME", uaName);
                        objParams[3] = new SqlParameter("@P_UA_NO", uaNO);
                        objParams[4] = new SqlParameter("@P_APLNO", aprlNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                        objParams[6] = new SqlParameter("@P_USER_ID", userid);
                        objParams[7] = new SqlParameter("@P_ISAPPROVAL", isapproval);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENTUSER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDeptPrivelege-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllDeptUser(int Mdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", Mdno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPARTMENTUSER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllDeptPrivelege-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllTable()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TABLE_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllDeptPrivelege-> " + ex.ToString());
                    }
                    return ds;
                }


                public int AddStage(string sname, string colCode, string tablesInv)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New STORE_DEPT_PRIVELEGE
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STNO", SqlDbType.Int);
                        objParams[1] = new SqlParameter("@P_Sname", sname);
                        objParams[2] = new SqlParameter("@P_Table_INV", tablesInv);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                        objParams[0].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_STAGE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddDeptPrivelege-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStage(int stno, string sname, string colCode, string tablesInv)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update  STORE_DEPT_PRIVELEGE
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STNO", stno);
                        objParams[1] = new SqlParameter("@P_Sname", sname);
                        objParams[2] = new SqlParameter("@P_Table_INV", tablesInv);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colCode);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_STAGE_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDeptPrivelege-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region STORE_DEPARTMENT

                public int AddDepartment(string DeptName, string DeptShortName, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MDNAME", DeptName);
                        objParams[1] = new SqlParameter("@P_MDSNAME", DeptShortName);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        objParams[4] = new SqlParameter("@P_MDNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENT_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }

                    catch (Exception ex)
                    {

                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddDepartment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDepartment(string DeptName, string DeptShortName, string collegeCode, int mdNo, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update Department
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MDNO", mdNo);
                        objParams[1] = new SqlParameter("@P_MDNAME", DeptName);
                        objParams[2] = new SqlParameter("@P_MDSNAME", DeptShortName);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPARTMENT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (SqlException sqex)
                    {
                        throw new Exception(sqex.Number.ToString());
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDepartment-> " + ex.ToString());
                    }
                    return retStatus;
                }



                #endregion

                #region STORE_SUBDEPARTMENT

                public int AddSubDepartMent(string subDeptName, string subDeptShortName, int show, int mdNo, string collegeCode, string userid, int PAYROLL_SUBDEPTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New SubDepartMent
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SDNAME", subDeptName);
                        objParams[1] = new SqlParameter("@P_SDSNAME", subDeptShortName);
                        objParams[2] = new SqlParameter("@P_SHOW", show);
                        objParams[3] = new SqlParameter("@P_MDNO", mdNo);
                        objParams[4] = new SqlParameter("P_COLLEGE_CODE", collegeCode);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        objParams[6] = new SqlParameter("@P_PAYROLL_SUBDEPTNO", PAYROLL_SUBDEPTNO);
                        objParams[7] = new SqlParameter("@P_SDNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_SUBDEPARTMENT_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddSubDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSubDepartMent(int sdNo, string subDeptName, string subDeptShortName, int show, int mCode, string collegeCode, string userid, int PAYROLL_SUBDEPTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update SubDepartMent                 

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SDNO", sdNo);
                        objParams[1] = new SqlParameter("@P_SDNAME", subDeptName);
                        objParams[2] = new SqlParameter("@P_SDSNAME", subDeptShortName);
                        objParams[3] = new SqlParameter("@P_SHOW", show);
                        objParams[4] = new SqlParameter("@P_MDNO", mCode);
                        objParams[5] = new SqlParameter("P_COLLEGE_CODE", collegeCode);
                        objParams[6] = new SqlParameter("@P_USER_ID", userid);
                        objParams[7] = new SqlParameter("@P_PAYROLL_SUBDEPTNO", PAYROLL_SUBDEPTNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_SUBDEPARTMENT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateParty-> " + ex.ToString());
                    }
                    return retStatus;
                }



                #endregion

                #region STORE_BUDGETHEAD
                public int AddBudgetHead(StoreMaster objStrMst, string userid)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_BHNO", objStrMst.BHNO);
                        objParams[1] = new SqlParameter("@P_BAMT", objStrMst.BAMT);
                        objParams[2] = new SqlParameter("@P_BUDFSDATE", objStrMst.BUDFSDATE);
                        objParams[3] = new SqlParameter("@P_BUDFEDATE", objStrMst.BUDFEDATE);
                        objParams[4] = new SqlParameter("@P_BNATURE", objStrMst.BNATURE);
                        objParams[5] = new SqlParameter("@P_SCHEME", objStrMst.SCHEME);
                        objParams[6] = new SqlParameter("@P_BCOORDINATOR", objStrMst.BCOORDINATOR);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_USER_ID", userid);
                        objParams[9] = new SqlParameter("@P_BHALNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDGETHEAD_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddBudgetHead-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateBudgetHead(StoreMaster objStrMst, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New UPDATEBUDGETHEAD
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_BHNO", objStrMst.BHNO);
                        objParams[1] = new SqlParameter("@P_BHALNO", objStrMst.BHALNO);
                        objParams[2] = new SqlParameter("@P_BAMT", objStrMst.BAMT);
                        objParams[3] = new SqlParameter("@P_BUDFSDATE", objStrMst.BUDFSDATE);
                        objParams[4] = new SqlParameter("@P_BUDFEDATE", objStrMst.BUDFEDATE);
                        objParams[5] = new SqlParameter("@P_BNATURE", objStrMst.BNATURE);
                        objParams[6] = new SqlParameter("@P_SCHEME", objStrMst.SCHEME);
                        objParams[7] = new SqlParameter("@P_BCOORDINATOR", objStrMst.BCOORDINATOR);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDGETHEAD_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateBudgetHead-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //// public int Inctivate(string Bhname)
                // {
                //     int retStatus = Convert.ToInt32(CustomStatus.Others);

                //     try
                //     {

                //         SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //         SqlParameter[] objParams = null;
                //         //Incativate  BUDGETHEAD
                //         objParams = new SqlParameter[1];
                //         objParams[0] = new SqlParameter("@P_BHNAME", Bhname);


                //         if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDGET_INACTIVE", objParams, false) != null)
                //             retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //     }
                //     catch (Exception ex)
                //     {
                //         retStatus = Convert.ToInt32(CustomStatus.Error);
                //         throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddBudgetHead-> " + ex.ToString());
                //     }
                //     return retStatus;

                // }
                public DataSet GetAllBudgetHead()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDGETHEAD_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllBudgetHead-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordBudgetHead(int bhalNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BHALNO", bhalNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDGETHEAD_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordBudgetHead-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region STORE_SUBDEPT_BUDGET
                public int AddSubDeptBudget(StoreMaster objStrMst, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_BHALNO", objStrMst.BHALNO);
                        objParams[1] = new SqlParameter("@P_MDNO", objStrMst.SDNO);
                        objParams[2] = new SqlParameter("@P_SD_BUDAMT", objStrMst.SD_BUDAMT);
                        objParams[3] = new SqlParameter("@P_SD_BUDSDATE", objStrMst.SD_BUDSDATE);
                        objParams[4] = new SqlParameter("@P_SD_BUDEDATE", objStrMst.SD_BUDEDATE);
                        objParams[5] = new SqlParameter("@P_SD_BUDBALAMT", objStrMst.SD_BUDBALAMT);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_USER_ID", userid);
                        objParams[8] = new SqlParameter("@P_SD_BUDNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_SUBDEPT_BUDGET_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddBudget-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSubDeptBudget(StoreMaster objStrMst, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New UpdateBudget
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SD_BUDNO", objStrMst.SD_BUDNO);
                        objParams[1] = new SqlParameter("@P_BHALNO", objStrMst.BHALNO);
                        objParams[2] = new SqlParameter("@P_MDNO", objStrMst.SDNO);
                        objParams[3] = new SqlParameter("@P_SD_BUDAMT", objStrMst.SD_BUDAMT);
                        objParams[4] = new SqlParameter("@P_SD_BUDSDATE", objStrMst.SD_BUDSDATE);
                        objParams[5] = new SqlParameter("@P_SD_BUDEDATE", objStrMst.SD_BUDEDATE);
                        objParams[6] = new SqlParameter("@P_SD_BUDBALAMT", objStrMst.SD_BUDBALAMT);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_SUBDEPT_BUDGET_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateBudget-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public bool CHECKBUDGETDATE(int bhalno, DateTime sdate, DateTime edate)
                {
                    bool retst = false;
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlDataReader dr = objSQLHelper.ExecuteReader("select budfsdate ,budfedate from STORE_BUDGETHEAD_Alloction where bhno=" + bhalno);
                    dr.Read();
                    DateTime sdate1 = Convert.ToDateTime(dr[0]);
                    DateTime edate1 = Convert.ToDateTime(dr[1]);
                    if ((sdate <= edate1 && sdate >= sdate1) && (edate <= edate1 && edate >= sdate1))
                    {
                        retst = true;
                    }
                    else
                    {
                        retst = false;
                    }
                    return retst;

                }
                public DataSet GetAllSubDeptBudget()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SUBDEPT_BUDGET_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllBudget-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordSubDeptBudget(int SdBugNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SD_BUDNO", SdBugNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SUBDEPT_BUDGET_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordBudget-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetBudgetNameBYFYEAR(DateTime BUDFSDATE, DateTime BUDFEDATE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_BUDFSDATE", BUDFSDATE);
                        objParams[1] = new SqlParameter("@P_BUDFEDATE", BUDFEDATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_BUDGETHEAD_BY_FYEAR ", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllBudget-> " + ex.ToString());
                    }
                    return ds;

                }
                #endregion

                #region STORE_DEPARTMENTREGISTER

                public int AddDeptRegister(string drName, int mdNo, decimal drsrno, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DRNAME", drName);
                        objParams[1] = new SqlParameter("@P_MDNO", mdNo);
                        objParams[2] = new SqlParameter("@P_DRSRNO", drsrno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        objParams[5] = new SqlParameter("@P_DRNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPTREGISTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddDeptRegister-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDeptRegister(string drName, int mdNo, decimal drsrno, string collegeCode, int drNo, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Department
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DRNO", drNo);
                        objParams[1] = new SqlParameter("@P_DRNAME", drName);
                        objParams[2] = new SqlParameter("@P_MDNO", mdNo);
                        objParams[3] = new SqlParameter("@P_DRSRNO", drsrno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEPTREGISTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDeptRegister-> " + ex.ToString());
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
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllDeptRegister-> " + ex.ToString());
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
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordDeptRegister-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region STORE_FIELDMASTER

                public int AddField(StoreMaster objStrMst)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Field Master
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FNAME", objStrMst.FNAME);
                        objParams[1] = new SqlParameter("@P_FTYPE", objStrMst.FTYPE);
                        objParams[2] = new SqlParameter("@P_FSRNO", objStrMst.FSRNO);
                        objParams[3] = new SqlParameter("@P_IND_FOR", objStrMst.IND_FOR);
                        objParams[4] = new SqlParameter("@P_ADDED_IN_BASIC", objStrMst.ADDED_IN_BASIC);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_TAX_DEDUCTED", objStrMst.DEDUCT_IN_BASIC);
                        objParams[7] = new SqlParameter("@P_FNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_FIELDMASTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateFiled(StoreMaster objStrMst)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update Field Master
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FNO", objStrMst.FNO);
                        objParams[1] = new SqlParameter("@P_FNAME", objStrMst.FNAME);
                        objParams[2] = new SqlParameter("@P_FTYPE", objStrMst.FTYPE);
                        objParams[3] = new SqlParameter("@P_FSRNO", objStrMst.FSRNO);
                        objParams[4] = new SqlParameter("@P_IND_FOR", objStrMst.IND_FOR);
                        objParams[5] = new SqlParameter("@P_ADDED_IN_BASIC", objStrMst.ADDED_IN_BASIC);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_TAX_DEDUCTED", objStrMst.DEDUCT_IN_BASIC);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_FIELDMASTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.UpdateFiled-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllFields()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FIELDMASTER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.GetAllFields-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordField(int fNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FNO", fNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FIELDMASTER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.GetSingleRecordField-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region STORE_NEWSPAPER

                public int AddNewsPaper(string newsPaperName, int cityNo, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New NewsPaper
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_NPNAME", newsPaperName);
                        objParams[1] = new SqlParameter("@P_CITYNO", cityNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        objParams[4] = new SqlParameter("@P_NPNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_NEWSPAPER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateNewsPaper(string newsPaperName, int cityNo, string collegeCode, int nNo, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //update  NewsPaper 
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_NPNO", nNo);
                        objParams[1] = new SqlParameter("@P_NPNAME", newsPaperName);
                        objParams[2] = new SqlParameter("@P_CITYNO", cityNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_NEWSPAPER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.UpdateNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllNewPaper()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_NEWSPAPER_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.GetAllNewPaper-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetSingleNewPaper(int npNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_NPNO", npNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_NEWSPAPER_GET_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.GetSingleRecordBudget-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                #endregion

                #region STORE_PARTY_CATEGORY

                public int AddParyCategory(string categoryName, string collegeCode, string userid, string shortname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PCNAME", categoryName);
                        objParams[4] = new SqlParameter("@P_PCSHORTNAME", shortname);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[2] = new SqlParameter("@P_USER_ID", userid);
                        objParams[3] = new SqlParameter("@P_PCNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_CATEGORY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddParyCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParyCategory(string categoryName, int categoryNo, string collegeCode, string userid, string shortname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PCNO", categoryNo);
                        objParams[1] = new SqlParameter("@P_PCNAME", categoryName);
                        objParams[4] = new SqlParameter("@P_PCSHORTNAME", shortname);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_CATEGORY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateParyCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllParyCategory()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_CATEGORY_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllParyCategory-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetSingleRecordParyCategory(int categoryNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_PCNO", categoryNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_CATEGORY_GET_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordParyCategory-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                #endregion

                # region PASSING_AUTHORITY
                // To insert New PASSING_AUTHORITY
                public int AddPassAuthority(StoreMaster objStr)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_PANAME", objStr.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objStr.UANO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objStr.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_AMOUNT_FROM", objStr.AMOUNT_FROM);
                        objParams[4] = new SqlParameter("@P_AMOUNT_TO", objStr.AMOUNT_TO);
                        objParams[5] = new SqlParameter("@P_IS_SPECIAL", objStr.IS_SPECIAL);
                        objParams[6] = new SqlParameter("@P_PANO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("dbo.PKG_STR_PASSING_AUTHORITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To modify existing Passing Authority
                public int UpdatePassAuthority(StoreMaster objStr)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_PANO", objStr.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", objStr.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStr.UANO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objStr.COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@P_AMOUNT_FROM", objStr.AMOUNT_FROM);
                        objParams[5] = new SqlParameter("@P_AMOUNT_TO", objStr.AMOUNT_TO);
                        objParams[6] = new SqlParameter("@P_IS_SPECIAL", objStr.IS_SPECIAL);
                        if (objSQLHelper.ExecuteNonQuerySP("dbo.PKG_STR_PASSING_AUTHORITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To Delete existing Passing Authority
                public int DeletePassAuthority(int PANo)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        if (objSQLHelper.ExecuteNonQuerySP("dbo.PKG_STR_PASSING_AUTHORITY_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.DeletePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all existing passing authority details
                //public DataSet GetAllPassAuthority()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];
                //        ds = objSQLHelper.ExecuteDataSetSP("dbo.PKG_STR_PASSING_AUTHORITY_GET_BYALL", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.RetAllPassAuthority->" + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}

                // To fetch existing Passing Authority details by passing Passing Authority No.
                //public DataSet GetSingPassAuthority(int PANo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = null; ;
                //        objparams = new SqlParameter[1];
                //        objparams[0] = new SqlParameter("@P_PANO", PANo);
                //        ds = objSQLHelper.ExecuteDataSetSP("dbo.PKG_STR_PASSING_AUTHORITY_GET_BY_NO", objparams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.RetSingPassAuthority->" + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}
                #endregion

                //#region Store Approval
                ////Fetch Pending List of Leave application for approval to passing Authority
                //public DataSet GetPendListforLeaveApproval(int UA_No, int Dept_No)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                //        objParams[1] = new SqlParameter("@P_DEPT_NO", Dept_No);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_COLUMN_RETURN", objParams);
                //    }
                //    catch (Exception ex)
                //    {

                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}

                   public DataSet GetPendListforLeaveApproval(int UA_No, int Dept_No, int OrgId)    
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_DEPT_NO", Dept_No);
                        objParams[2] = new SqlParameter("@P_OrgId", OrgId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_COLUMN_RETURN", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                //Fetch Pending List of Requisition for approval to passing Authority
                public DataSet GetPendListforRequisitionApproval(int UA_No, int Dept_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_DEPT_NO", Dept_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_PENDING_REQUISITION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetApprovedRequisitionList(int Dept_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", Dept_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_APPROVED_REQUISITION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetApprovedRequisitionList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetApplDetail(int LETRNO, int stno, string tabs, int uano, int dptno)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        objParams[1] = new SqlParameter("@p_tablename", tabs);
                        objParams[2] = new SqlParameter("@p_stno", stno);
                        objParams[3] = new SqlParameter("@P_UA_NO", uano);
                        objParams[4] = new SqlParameter("@P_DEPTNO", dptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_APLDTL_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int UpdateAppPassEntry(int TRNO, int UA_NO, string STATUS, string REMARKS, int stno, int dept, DateTime APRDT, int p, char sanctioning_authority)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_DEPTNO", dept);
                        objparams[6] = new SqlParameter("@P_STNO", stno);
                        objparams[7] = new SqlParameter("@P_PAPNO", p);
                        objparams[8] = new SqlParameter("@P_SANCTIONING_AUTHORITY", sanctioning_authority);
                        objparams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_PASS_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet getNumberinfo(int trno, string tab)
                //fetch records Leave application which is approved  for Establishment Section For transfer to Service Book.
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_APPROVEDLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLvApplApprovedList->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeaveAppEntryByNO(int LETRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveAppEntryByNO->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet FillLeaveName(int EMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", EMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_FILL", objparams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.FillLeaveName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetNoofdays(DateTime Frdate, DateTime Todate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FRDATE", Frdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_NO_OF_DAYS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetColumns(string table_name)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TABLENAME", table_name);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_COLUMN_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmpName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //public DataTable GetLvApplAprStatus(int LETRNO)
                //    //fetch Leave Application Approval Status of particular leave By passing LETRNO
                //{
                //   DataTable dt = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);

                //        dt = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APL_APPROVALESTATUS", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return dt;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLvApplAprStatus->" + ex.ToString());
                //    }
                //    finally
                //    {

                //        dt.Dispose();
                //    }
                //    return dt;
                //}

                //public int AddAPP_PASS_ENTRY(Leaves objLeaves)
                //{
                //    int retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    try
                //    {
                //        SQLHelper objSQLHelper=new SQLHelper(_nitprm_constr);
                //        SqlParameter [] objParams=null;
                //        objParams =new SqlParameter[7];
                //        objParams[0]=new SqlParameter("@P_LETRNO",objLeaves.LETRNO);
                //        objParams[1]=new SqlParameter("@P_PANO",objLeaves.PANO);
                //        objParams[2]=new SqlParameter("@P_STATUS",objLeaves.STATUS);
                //        objParams[3]=new SqlParameter("@P_APP_DATE",objLeaves.APPDT);
                //        objParams[4]=new SqlParameter("@P_APP_REMARKS",objLeaves.APP_REMARKS);
                //        objParams[5]=new SqlParameter("@P_COLLEGE_CODE",objLeaves.COLLEGE_CODE);
                //        objParams[6]=new SqlParameter("@P_LAPENO",SqlDbType.Int);
                //        objParams[6].Direction=ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_INSERT", objParams, false) != null)
                //            retstatus =Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_PASS_ENTRY->"+ ex.ToString());
                //    }
                //    return retstatus;
                //}

                //#endregion

                # region PASSING_AUTHORITY_PATH
                // To insert New PASSING_AUTHORITY_PATH

                public int AddPAPath(StoreMaster objLeave, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[7] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_STR_EMP_RECORD", dtEmpRecord);
                        objparams[10] = new SqlParameter("@P_PATH_FOR", objLeave.PATH_FOR);
                        objparams[11] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdatePAPath(StoreMaster objLeave, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAPNO", objLeave.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[8] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_STR_EMP_RECORD", dtEmpRecord);
                        objparams[11] = new SqlParameter("@P_PATH_FOR", objLeave.PATH_FOR);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdatePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("dbo.PKG_str_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllPAPath(int MDNO, char PATH_FOR)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objparams[1] = new SqlParameter("@P_PATH_FOR", PATH_FOR);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetSinglePAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("dbo.PKG_STR_PASSING_AUTHORITY_PATH_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region STORE_PARTY

                public int AddParty(StoreMaster objStrMst, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_PCODE", objStrMst.PCODE);
                        objParams[1] = new SqlParameter("@P_PNAME", objStrMst.PNAME);
                        objParams[2] = new SqlParameter("@P_PCNO", objStrMst.PCNO);
                        objParams[3] = new SqlParameter("@P_ADDRESS", objStrMst.ADDRESS);
                        objParams[4] = new SqlParameter("@P_CITYNO", objStrMst.CITYNO);
                        objParams[5] = new SqlParameter("@P_STATENO", objStrMst.STATENO);
                        objParams[6] = new SqlParameter("@P_PHONE", objStrMst.PHONE);
                        objParams[7] = new SqlParameter("@P_EMAIL", objStrMst.EMAIL);
                        objParams[8] = new SqlParameter("@P_CST", objStrMst.CST);
                        objParams[9] = new SqlParameter("@P_BST", objStrMst.BST);
                        objParams[10] = new SqlParameter("@P_USER_ID", userid);
                        objParams[11] = new SqlParameter("@P_REMARK", objStrMst.REMARK);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_PARTY_BANK_DETAIL_TBL", objStrMst.PARTY_BANK_DETAIL_TBL);
                        objParams[14] = new SqlParameter("@P_PNO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.AddParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParty(StoreMaster objStrMst, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update Party                       
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_PNO", objStrMst.PNO);
                        objParams[1] = new SqlParameter("@P_PCODE", objStrMst.PCODE);
                        objParams[2] = new SqlParameter("@P_PNAME", objStrMst.PNAME);
                        objParams[3] = new SqlParameter("@P_PCNO", objStrMst.PCNO);
                        objParams[4] = new SqlParameter("@P_ADDRESS", objStrMst.ADDRESS);
                        objParams[5] = new SqlParameter("@P_CITYNO", objStrMst.CITYNO);
                        objParams[6] = new SqlParameter("@P_STATENO", objStrMst.STATENO);
                        objParams[7] = new SqlParameter("@P_PHONE", objStrMst.PHONE);
                        objParams[8] = new SqlParameter("@P_EMAIL", objStrMst.EMAIL);
                        objParams[9] = new SqlParameter("@P_CST", objStrMst.CST);
                        objParams[10] = new SqlParameter("@P_BST", objStrMst.BST);
                        objParams[11] = new SqlParameter("@P_USER_ID", userid);
                        objParams[12] = new SqlParameter("@P_REMARK", objStrMst.REMARK);
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[14] = new SqlParameter("@P_PARTY_BANK_DETAIL_TBL", objStrMst.PARTY_BANK_DETAIL_TBL);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.UpdateParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllParty()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.GetAllParty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordParty(int pNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", pNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.GetSingleRecordParty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GenratePartyCode(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PCNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_VENDORCODE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Entry_Controller_GeneratevendorCode-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region STORE_MAIN_ITEM_GROUP

                public int AddMainItemGroup(string mainItemGrpName, string Sname, string collegeCode, string userid, char ItemType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MIGNAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);

                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        objParams[4] = new SqlParameter("@P_ITEMTYPE", ItemType);
                        objParams[5] = new SqlParameter("@P_MIGNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }


                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainItemGroup(string mainItemGrpName, string Sname, string collegeCode, int mainItemGprNo, string userid, char ItemType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MIGNAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        objParams[5] = new SqlParameter("@P_ITEMTYPE", ItemType);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateMainItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllMainItemGroup()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_GROUP_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllMainItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetSingleRecordMainItemGroup(int mainItemGprNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_GROUP_GET_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordMainItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                #endregion

                #region STORE_MAIN_ITEM_SUBGROUP

                public int AddMainSubItemGroup(string mainSubItemGrpName, string Sname, int mainItemGprNo, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        objParams[5] = new SqlParameter("@P_MISGNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_SUB_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainSubItemGroup(string mainSubItemGrpName, string Sname, int mainItemGprNo, string collegeCode, int mainSubItemGprNo, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_MISGNO", mainSubItemGprNo);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        //objParams[6] = new SqlParameter("@ISTOOLKIT", chk);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_SUB_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllMainSubItemGroup()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_SUB_GROUP_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllMainSubItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetSingleRecordMainSubItemGroup(int mainSubItemGprNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MISGNO", mainSubItemGprNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_SUB_GROUP_GET_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordMainSubItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                #endregion
                #region STORE_DEPRECIATION_Entry
                public int AddSubGroupDepriciationEntry(int DepId, int ItemSubGroup, decimal Deppercentage, DateTime Depdate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEP_MAP_ID",DepId);
                        objParams[1] = new SqlParameter("@P_MISGNO",ItemSubGroup);
                        objParams[2] = new SqlParameter("@P_DEPR_PER",Deppercentage);
                        objParams[3] = new SqlParameter("@P_DEPR_FROM_DATE",Depdate);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INS_UPD_DEPRECIATION_ITEM_MAPPING", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddSubGroupDepriciationEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region STORE_ITEM_MASTER

                public int AddItemMaster(StoreMaster objStrMst, string userid, int itpno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_ITEM_CODE", objStrMst.ITEM_CODE);
                        objParams[1] = new SqlParameter("@P_ITEM_NAME", objStrMst.ITEM_NAME);
                        objParams[2] = new SqlParameter("@P_ITEM_DETAILS", objStrMst.ITEM_DETAILS);
                        objParams[3] = new SqlParameter("@P_MIGNO", objStrMst.MIGNO);
                        objParams[4] = new SqlParameter("@P_MISGNO", objStrMst.MISGNO);
                        objParams[5] = new SqlParameter("@P_ITEM_UNIT", objStrMst.ITEM_UNIT);
                        objParams[6] = new SqlParameter("@P_ITEM_REORDER_QTY", objStrMst.ITEM_REORDER_QTY);
                        objParams[7] = new SqlParameter("@P_ITEM_MIN_QTY", objStrMst.ITEM_MIN_QTY);
                        objParams[8] = new SqlParameter("@P_ITEM_MAX_QTY", objStrMst.ITEM_MAX_QTY);
                        objParams[9] = new SqlParameter("@P_ITEM_BUD_QTY", objStrMst.ITEM_BUD_QTY);
                        objParams[10] = new SqlParameter("@P_ITEM_CUR_QTY", objStrMst.ITEM_CUR_QTY);
                        objParams[11] = new SqlParameter("@P_ITEM_OB_QTY", objStrMst.ITEM_OB_QTY);
                        objParams[12] = new SqlParameter("@P_ITEM_OB_VALUE", objStrMst.ITEM_OB_VALUE);
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[14] = new SqlParameter("@P_USER_ID", userid);
                        objParams[15] = new SqlParameter("@P_ITPNO", itpno);
                        objParams[16] = new SqlParameter("@P_APPROVAL", objStrMst.ITEM_APPROVAL);
                        objParams[17] = new SqlParameter("@P_MDNO", objStrMst.DEPTNO);
                        objParams[18] = new SqlParameter("@P_TAX_ITEM_TBL", objStrMst.TaxFieldsTbl_TRAN);
                        objParams[19] = new SqlParameter("@P_ITEM_NO", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateItemMaster(StoreMaster objStrMst, string userid, int itpno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New Item_Master
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", objStrMst.ITEM_NO);
                        objParams[1] = new SqlParameter("@P_ITEM_CODE", objStrMst.ITEM_CODE);
                        objParams[2] = new SqlParameter("@P_ITEM_NAME", objStrMst.ITEM_NAME);
                        objParams[3] = new SqlParameter("@P_ITEM_DETAILS", objStrMst.ITEM_DETAILS);
                        objParams[4] = new SqlParameter("@P_MIGNO", objStrMst.MIGNO);
                        objParams[5] = new SqlParameter("@P_MISGNO", objStrMst.MISGNO);
                        objParams[6] = new SqlParameter("@P_ITEM_UNIT", objStrMst.ITEM_UNIT);
                        objParams[7] = new SqlParameter("@P_ITEM_REORDER_QTY", objStrMst.ITEM_REORDER_QTY);
                        objParams[8] = new SqlParameter("@P_ITEM_MIN_QTY", objStrMst.ITEM_MIN_QTY);
                        objParams[9] = new SqlParameter("@P_ITEM_MAX_QTY", objStrMst.ITEM_MAX_QTY);
                        objParams[10] = new SqlParameter("@P_ITEM_BUD_QTY", objStrMst.ITEM_BUD_QTY);
                        objParams[11] = new SqlParameter("@P_ITEM_CUR_QTY", objStrMst.ITEM_CUR_QTY);
                        objParams[12] = new SqlParameter("@P_ITEM_OB_QTY", objStrMst.ITEM_OB_QTY);
                        objParams[13] = new SqlParameter("@P_ITEM_OB_VALUE", objStrMst.ITEM_OB_VALUE);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_USER_ID", userid);
                        objParams[16] = new SqlParameter("@P_ITPNO", itpno);
                        objParams[17] = new SqlParameter("@P_APPROVAL", objStrMst.ITEM_APPROVAL);
                        objParams[18] = new SqlParameter("@P_MDNO", objStrMst.DEPTNO);
                        objParams[19] = new SqlParameter("@P_TAX_ITEM_TBL", objStrMst.TaxFieldsTbl_TRAN);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllItemMaster()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllItemMaster(int Deptno)  //,string StoreUser)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", Deptno);
                        // objParams[1] = new SqlParameter("@P_STORE_USER", StoreUser);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_GET_ALL_NEW", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordItemMaster(int itemNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", itemNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordTaxItemMaster(int itemNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", itemNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_STORE_TAX", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordTaxItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStoreTax()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_STORE_TAX", objParams);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ds;

                }

                #endregion

                #region STORE_TAX
                public int AddTax(string taxname, double taxper, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New TAX_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TAXNAME", taxname);
                        objParams[1] = new SqlParameter("@P_TAXPER", taxper);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        objParams[4] = new SqlParameter("@P_TAXNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TAX_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddTax-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllTax()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TAX_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllTax-> " + ex.ToString());
                    }
                    return ds;

                }
                public DataSet GetSingleRecordTax(int taxno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@TAXNO", taxno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TAX_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordTax-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateTax(int taxno, string taxname, double taxper, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TAXNO", taxno);
                        objParams[1] = new SqlParameter("@P_TAXNAME", taxname);
                        objParams[2] = new SqlParameter("@P_TAXPER", taxper);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TAX_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateTAX-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region STORE_GRAND
                public int AddGrand(string grandcode, string grandname, string detail, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New GRAND_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_GRAND_CODE", grandcode);
                        objParams[1] = new SqlParameter("@P_GRAND_NAME", grandname);
                        objParams[2] = new SqlParameter("@P_GRAND_DETAILS", detail);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        objParams[5] = new SqlParameter("@P_GRANDNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_GRAND_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddGrand-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public DataSet GetAllGRAND()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRAND_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllGrand-> " + ex.ToString());
                //    }
                //    return ds;

                //}
                //public DataSet GetSingleRecordGrand(int grandno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@GRANDNO ", grandno );
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRAND_GET_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordGrand-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public int UpdateGrand(int grandno, string grandcode, string grandname, string detail, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_GRANDNO", grandno);
                        objParams[1] = new SqlParameter("@P_GRAND_CODE", grandcode);
                        objParams[2] = new SqlParameter("@P_GRAND_NAME", grandname);
                        objParams[3] = new SqlParameter("@P_GRAND_DETAILS", detail);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_GRAND_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateGRAND-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region STORE_DSR
                public int AddDSR(string dsrname, string dsrshortname, int dsrYear, string drno, int mdno, int grandno, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New DSR_MASTER
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_DSTK_NAME", dsrname);
                        objParams[1] = new SqlParameter("@P_DSTK_SHORT_NAME", dsrshortname);
                        objParams[2] = new SqlParameter("@P_DSTK_YEAR", dsrYear);
                        objParams[3] = new SqlParameter("@P_DRNO", drno);
                        objParams[4] = new SqlParameter("@P_MDNO", mdno);
                        objParams[5] = new SqlParameter("@P_GRANDNO", grandno);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[7] = new SqlParameter("@P_USER_ID", userid);
                        objParams[8] = new SqlParameter("@P_DSTKNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DSR_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddDSR-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllDSR()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DSR_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllDSR-> " + ex.ToString());
                    }
                    return ds;

                }

                public int UpdateDSR(int Dskno, string dsrname, string dsrshortname, int dsrYear, string drno, int mdno, int grandno, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //UPdate DSR_MASTER
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_DSTK_NAME", dsrname);
                        objParams[1] = new SqlParameter("@P_DSTK_SHORT_NAME", dsrshortname);
                        objParams[2] = new SqlParameter("@P_DSTK_YEAR", dsrYear);
                        objParams[3] = new SqlParameter("@P_DRNO", drno);
                        objParams[4] = new SqlParameter("@P_MDNO", mdno);
                        objParams[5] = new SqlParameter("@P_GRANDNO", grandno);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[7] = new SqlParameter("@P_DSTKNO", Dskno);
                        objParams[8] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_DSR_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDSR-> " + ex.ToString());
                    }
                    return retStatus;
                }


                // It is used to update Store Configuration for DSR.
                public int UpdateDSRConfiguration(int PREVIOUS_YEAR_DSR, int CURRENT_YEAR_DSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //UPdate DSR_MASTER
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PREVIOUS_YEAR_DSR", PREVIOUS_YEAR_DSR);
                        objParams[1] = new SqlParameter("@P_CURRENT_YEAR_DSR", CURRENT_YEAR_DSR);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_DSR_CONFIGURATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateDSRConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Store_Invoice
                public DataSet GetAllInvoice()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllInvoice-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region STORE_BUDGET_NAME_MASTER
                public int AddBudgetHead_Name(string budgetname, string collegecode, string userid)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BHNAME", budgetname);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[2] = new SqlParameter("@P_USER_ID", userid);
                        objParams[3] = new SqlParameter("@P_BHNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_BUDGET_HEADNAME_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.AddBudgetHead-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateBudgetHead_Name(int bhno, string bhname, string collegecode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New UPDATEBUDGETHEAD
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BHNO", bhno);
                        objParams[1] = new SqlParameter("@P_BHNAME", bhname);

                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_BUDGET_HEADNAME_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.UpdateBudgetHead-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllBudgetHead_Name()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BUDGET_HEADNAME_GET_ALL ", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllBudgetHead-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordBudgetHead_Name(int bhNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BHNO", bhNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BUDGET_HEADNAME_GET_SINGLEHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordBudgetHead-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Store_Financial_Year
                public DataSet GetAllFinancial_Year()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FINANCIAL_YEAR_GET_ALL ", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllFianacial_Year-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region

                public int INSUPDATERECOMPATH(int RECOM_APP_NO, int UA_NO, int USERID, string College_Code)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];
                        objparams[0] = new SqlParameter("@P_RECOM_APP_NO", RECOM_APP_NO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_USERID", USERID);
                        objparams[3] = new SqlParameter("@P_COLLEGE_CODE", College_Code);
                        objparams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_Recom_Approval_Path", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int deleteRecom_Approval_Path(int RECOM_APP_NO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        string query = "delete from Store_Recom_Approval_Path where RECOM_APP_NO=" + RECOM_APP_NO.ToString();
                        retStatus = objSQLHelper.ExecuteNonQuery(query);

                        if (Convert.ToInt32(retStatus) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(retStatus) > 0)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;
                }


                public int updateItemQTY(string TRNO, int itemno, string itmqty, int userno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objparams[1] = new SqlParameter("@P_ITEMNO", itemno);
                        objparams[2] = new SqlParameter("@P_ITEMQTY", itmqty);
                        objparams[3] = new SqlParameter("@P_UA_NO", userno);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_ITEM_FROM_APPROVAL", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateAceeptRejectItem(int REQ_TNO, char AcceptReject, int UA_NO, int ItemNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_REQ_TNO", REQ_TNO);
                        objparams[1] = new SqlParameter("@P_ITEM_ACCEPT_REJECT", AcceptReject);
                        objparams[2] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[3] = new SqlParameter("@P_ITEMNO", ItemNo);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_ACCEPT_REJECT_ITEM", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int DeleteItemQTY(string TRNO, int itemno, string itmqty, int userno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objparams[1] = new SqlParameter("@P_ITEMNO", itemno);
                        objparams[2] = new SqlParameter("@P_ITEMQTY", itmqty);
                        objparams[3] = new SqlParameter("@P_UA_NO", userno);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_DELETE_ITEM_FROM_APPROVAL", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.DeleteItemQTY->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetVendorDetails(int PCNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@PCNO", PCNO);
                        ds = objSQLHelper.ExecuteDataSetSP("BindVendorDetails", objParams);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ds;

                }

                public int UpdateRatingStatus(int colId, DataTable dt)
                {
                    int retStatus = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@CollegeCode", colId);
                        objParams[1] = new SqlParameter("@VendorTbl", dt);

                        object retStatu = objSQLHelper.ExecuteNonQuerySP("UpdateVendorRating", objParams, true);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    return retStatus;
                }

                public DataSet GetEmployeeItemDetails(int UserNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@UA_No", UserNo);
                        ds = objSQLHelper.ExecuteDataSetSP("GetEmployeesItemDetail", objParams);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ds;

                }

                public int UpdateSubGroupEntry(int migno, bool isapp, string remark)
                {
                    int returnStatus = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparam = new SqlParameter[4];
                        objparam[0] = new SqlParameter("@MISGNO", migno);
                        objparam[1] = new SqlParameter("@ISAPPROVED", isapp);
                        objparam[2] = new SqlParameter("@REMARKS", remark);
                        objparam[3] = new SqlParameter("@R_OUT", SqlDbType.Int);
                        objparam[3].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_SUBGROUP_ENTRY", objparam, true);

                        if (Convert.ToInt32(ret) == -99)
                            returnStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            returnStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        returnStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return returnStatus;
                }


                public DataSet GetSelectedFields(string QUOTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUOTNO", QUOTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_SELECTED_FIELDS_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.GetSelectedFields-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Toolkit Management

                public int InsertToolKitData(string ToolKitName, string ItemNo, int CollegeCode)
                {
                    int returnStatus = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparam = new SqlParameter[4];
                        objparam[0] = new SqlParameter("@P_TOOLKITNAME", ToolKitName);
                        objparam[1] = new SqlParameter("@P_ITEMNO", ItemNo);
                        objparam[2] = new SqlParameter("@P_COLLEGECODE", CollegeCode);
                        objparam[3] = new SqlParameter("@P_TKNO", SqlDbType.Int);
                        objparam[3].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_STR_INSERT_TOOLKIT_DATA", objparam, true);

                        if (Convert.ToInt32(ret) == -99)
                            returnStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            returnStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        returnStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return returnStatus;
                }

                public int UpdateToolKitData(int Tkno, string ToolKitName, string ItemNo, int CollegeCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objSqlParam = new SqlParameter[4];
                        objSqlParam[0] = new SqlParameter("@P_TKNO", Tkno);
                        objSqlParam[1] = new SqlParameter("@P_TKNAME", ToolKitName);
                        objSqlParam[2] = new SqlParameter("@P_ITEMNO", ItemNo);
                        objSqlParam[3] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);
                        object ret = ObjSqlHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_TOOLKIT_DATA", objSqlParam, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetToolKitDetails(int TKNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@TK_NO", TKNO);
                        ds = objSQLHelper.ExecuteDataSetSP("GetToolKitDetails", objParams);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ds;

                }
                #endregion Toolkit Management


                #region Requisition Track

                public DataSet GetRequisitionStatusList(int SDNO, int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SDNO", SDNO);
                        objParams[1] = new SqlParameter("@P_UANO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_REQUISITION_STATUS", objParams);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ds;

                }

                #endregion


                #region getStoreTables
                public DataSet GetStoreTables()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_TABLES", objParams);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ds;

                }

                #endregion


                public int AddUpdTaxMasterField(StoreMaster objStrMst)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Field Master
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_TAXID", objStrMst.Tax_Id);
                        objParams[1] = new SqlParameter("@P_TNAME", objStrMst.Tax_Name);
                        objParams[2] = new SqlParameter("@P_TCODE", objStrMst.Tax_Code);
                        objParams[3] = new SqlParameter("@P_IS_PERCENTAGE", objStrMst.Is_Per);
                        objParams[4] = new SqlParameter("@P_TAX_PERCENTAGE", objStrMst.Tax_Per);
                        objParams[5] = new SqlParameter("@P_IS_STATE_TAX", objStrMst.Is_State_Tax);
                        objParams[6] = new SqlParameter("@P_CAL_ON_BASIC", objStrMst.Cal_Basic_Ammount);
                        objParams[7] = new SqlParameter("@P_TAX_SRNO", objStrMst.Tax_SerialNo);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objStrMst.College_Code);
                        objParams[9] = new SqlParameter("@P_CREATED_BY", objStrMst.CREATED_BY);
                        objParams[10] = new SqlParameter("@P_MODIFIED_BY", objStrMst.MODIFIED_BY);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TAX_MASTER_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.AddTaxMasterField-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region CalculateDepriciation

                // Created By: Shabina 
                // Created Date: 11/08/2021
                // Method Name: CalculateDepriciation
                // Method Parameters: int ddlSubCategory, int ddlItem, string ddlSerialNo  DateTime txtToDate
                // Method Description: Insert the Data and  Calculate Description 
                public DataSet CalculateDepriciation(int ddlSubCategory, int ddlItem, string ddlSerialNo, DateTime txtToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DSR_NUMBER", ddlSerialNo);
                        objParams[1] = new SqlParameter("@P_MISGNO", ddlSubCategory);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", ddlItem);
                        objParams[3] = new SqlParameter("@P_TODATE", txtToDate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INS_DEPRECIATION_CALCULATION_DATA", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.CalculateDepriciation-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion


                //------03/02/2022---for mail---------------
                public DataSet GetHighiestApprovalAuthrity(int trno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRNO", trno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_HIGHIEST_AUTHORITY_UANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmpName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //---------------01/11/2022--------------------------------------------------------------------------//
                ///Creator name: Shabina
                ///// Method name:AddUPDATELOCATIONMASTER
                //Purpose: For Location Master Entry Insert Update purpose
                 //--date:01/11/2022   
                public int AddUPDATELOCATIONMASTER(string LocationName, string ActiveStatus, string collegeCode, int OrgId, string userid, int LocationNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New NewsPaper
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_LOCATION_NAME", LocationName);
                        objParams[1] = new SqlParameter("@P_LOCATION_NO", LocationNo);
                        objParams[2] = new SqlParameter("@P_USER_ID", userid);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_OrganizationId", OrgId);
                        objParams[5] = new SqlParameter("@P_ACTIVESTATUS", ActiveStatus);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_STORE_INS_UPD_LOCATION_MASTER", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_INS_UPD_LOCATION_MASTER", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                      
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //---------------01/11/2022---------end-----------------------------------------------------------------//
            }
        }
    }
}
