//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE MGT.                               
// CREATION DATE : 29-02-2016                                                        
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
            public class PostAssignmentController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region PostMaster
              
                // To insert New Post Entry

                public int AddUpdatePostAssignment(PostAssignmentMaster objPM)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];

                        objParams[0] = new SqlParameter("@P_POST_ASSIGN_NO", objPM.POST_ASSIGN_NO);
                        objParams[1] = new SqlParameter("@P_STAFFNO", objPM.STAFFNO);
                        objParams[2] = new SqlParameter("@P_POST_NO", objPM.POST_NO);
                        objParams[3] = new SqlParameter("@P_POST_CODE_NO", objPM.POST_CODE_NO);
                        objParams[4] = new SqlParameter("@P_POST_STATUS", objPM.POST_STATUS);
                        objParams[5] = new SqlParameter("@P_IDNO", objPM.IDNO);
                        objParams[6] = new SqlParameter("@P_MODENO", objPM.MODE_NO);                       
                        objParams[7] = new SqlParameter("@P_CATEGORYNO", objPM.CATEGORYNO);                                               
                        
                        if (!objPM.APP_DATE.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_APP_DATE", objPM.APP_DATE);
                        else
                            objParams[8] = new SqlParameter("@P_APP_DATE", DBNull.Value);
                        if (!objPM.VAC_DATE.Equals(DateTime.MinValue))
                            objParams[9] = new SqlParameter("@P_VAC_DATE", objPM.VAC_DATE);
                        else
                            objParams[9] = new SqlParameter("@P_VAC_DATE", DBNull.Value);
                        objParams[10] = new SqlParameter("@P_STATUS_RECRUITMENT", objPM.STATUS_RECRUITMENT);
                        objParams[11] = new SqlParameter("@P_REMARK", objPM.REMARK);

                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        
                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_ASSIGNMENT_INSUPD", objParams, true);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_ASSIGNMENT_INSUPD", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostAssignmentController.AddUpdatePostAssignment->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataTable RetrieveEmpDetailsLeave(string search, string category, int staffno)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);
                        objParams[2] = new SqlParameter("@P_STAFFNO", staffno);

                        dt = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE_LEAVE", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return dt;
                }
                public DataSet GetAllPostAssignmentSearch(string staff, char status, string post, string plan_name)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STAFF", staff);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_POST", post);
                        objParams[3] = new SqlParameter("@P_PLAN_NAME", plan_name);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_ASSIGNMENT_GETALL_BYSEARCH", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostAssignmentController.GetAllPostAssignmentSearch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }
                public DataSet GetAllPostAssignmentSearch(string staff, char status, string post)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFF", staff);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_POST", post);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_ASSIGNMENT_GETALL_BYSEARCH", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostAssignmentController.GetAllPostAssignmentSearch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }
                // To Delete School
                public int DeletePostAssignment(int POST_ASSIGN_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_POST_ASSIGN_NO", POST_ASSIGN_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SANCTION_POST_ASSIGNMENT_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostAssignmentController.DeletePostAssignment-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Post
                public DataSet GetAllPostAssignment()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_ASSIGNMENT_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostAssignmentController.GetAllPostAssignment-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Post detail by passing Post No
                public DataSet GetSinglePostAssignment(int POST_ASSIGN_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_POST_ASSIGN_NO", POST_ASSIGN_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SANCTION_POST_ASSIGNMENT_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PostAssignmentController.GetSinglePostAssignment->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Post assignement Region
          
            }
        }
    }
}
