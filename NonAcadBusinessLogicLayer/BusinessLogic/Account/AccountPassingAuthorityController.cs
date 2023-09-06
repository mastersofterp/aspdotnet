//======================================================================================
// PROJECT NAME  : NITPRM                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[PASSING AUTHORITY CONTROLLER]                                  
// CREATION DATE : 26-11-2018                                                       
// CREATED BY    : NOKHLAL KUMAR                                   
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
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AccountPassingAuthorityController
            {

                /// <summary>
                /// ConnectionStrings
                /// </summary>


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public AccountPassingAuthorityController()
                {
                    //blank Constructor
                }

                public AccountPassingAuthorityController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
                }

                #region Passing Authority
                // To Fetch all existing passing authority details
                public DataSet GetAllPassAuthority(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PASSING_AUTHORITY_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.RetAllPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To fetch existing Passing Authority details by passing Passing Authority No.
                public DataSet GetSingPassAuthority(int PANo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PASSING_AUTHORITY_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.RetSingPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To insert New PASSING_AUTHORITY
                public int AddPassAuthority(AccountPassingAuthority objPA)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PANAME", objPA.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objPA.UANO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objPA.College_No);
                        objParams[3] = new SqlParameter("@P_PANO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PASSING_AUTHORITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To modify existing Passing Authority
                public int UpdatePassAuthority(AccountPassingAuthority objPA)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PANO", objPA.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", objPA.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objPA.UANO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objPA.College_No);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PASSING_AUTHORITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Passing Authority Path
                public int AddPAPath(AccountPassingAuthority objPA, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAN01", objPA.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objPA.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objPA.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objPA.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objPA.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objPA.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objPA.DEPTNO);
                        objparams[7] = new SqlParameter("@P_LNO", objPA.LNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_NO", objPA.College_No);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objPA.College_Code);
                        objparams[10] = new SqlParameter("@P_ACC_EMP_RECORD", dtEmpRecord);
                        objparams[11] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdatePAPath(AccountPassingAuthority objPA, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAPNO", objPA.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objPA.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objPA.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objPA.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objPA.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objPA.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objPA.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objPA.DEPTNO);
                        objparams[8] = new SqlParameter("@P_LNO", objPA.LNO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_NO", objPA.College_No);
                        objparams[10] = new SqlParameter("@P_COLLEGE_CODE", objPA.College_Code);
                        objparams[11] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.UpdatePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.GetAllPAPath->" + ex.ToString());
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
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_PASSING_AUTHORITY_PATH_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion


                #region Main Configuration
                public int UpdateMainConfiguration(int AO, int FO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_AO", AO);
                        objParams[1] = new SqlParameter("@P_FO", FO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_UPDATE_MAIN_CONFIGURATION", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.UpdateMainConfiguration->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion
            }
        }
    }
}
