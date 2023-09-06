using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AcademinDashboardController:IDisposable
            {
                string _UAIMS_constr = string.Empty;
                public AcademinDashboardController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                }

                public DataSet GetAcademicDashboardDetail(AcademicDashboardEntity objADE)
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_DASHBOARD_TYPE",objADE.Dashboard_Type),
                           new SqlParameter("@P_SESSIONNO",objADE.SessionNo),
                           new SqlParameter("@P_COLLEGE_ID",objADE.CollegeId),
                           new SqlParameter("@P_SCHEMENO",objADE.SchemeNo),
                           new SqlParameter("@P_DEPTNO",objADE.DeptNo),
                           new SqlParameter("@P_EXAMPROCESSVIEW",objADE.ExamProcessView)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("[DBO].[PKG_GET_ACADEMIC_DASHBOARD]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }

                public SqlDataReader GetAcademicExamRegistrationDashboardDetail(AcademicDashboardEntity objADE, string semester)
                {
                    SqlDataReader dr = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_DASHBOARD_TYPE",objADE.Dashboard_Type),
                           new SqlParameter("@P_SESSIONNO",objADE.SessionNo),
                           new SqlParameter("@P_COLLEGE_ID",objADE.CollegeId),
                           new SqlParameter("@P_SCHEMENO",objADE.SchemeNo),
                           new SqlParameter("@P_DEPTNO",objADE.DeptNo),
                           new SqlParameter("@P_SEMESTERNOS",semester),
                           new SqlParameter("@P_EXAMPROCESSVIEW",objADE.ExamProcessView)
                        };

                        dr = objDataAccessLayer.ExecuteReaderSP("[DBO].[PKG_GET_ACADEMIC_DASHBOARD]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return dr;
                }

                public DataSet GetAcademicDashboardPrograssbarDetail(AcademicDashboardEntity objADE, string semester)
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_DASHBOARD_TYPE",objADE.Dashboard_Type),
                           new SqlParameter("@P_SEMESTERNO",semester),
                           new SqlParameter("@P_SESSIONNO",objADE.SessionNo)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("[DBO].[PKG_GET_ACADEMIC_DASHBOARD_PROGRESSBAR]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }

                public object Insert_Academic_Dashboard_Email_Sending_Log(DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_TBLBULKDATA", dt),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[INSERT_ACADEMIC_DASHBOARD_SENDMAIL_LOG]", sqlParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return retStatus;
                }

                public DataSet Get_College_Session(int Mode, string College_Nos)
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_MODE",Mode),
                           new SqlParameter("@P_COLLEGE_IDNOS",College_Nos)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_ACAD_GET_CONCAT_COLLEGE_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }
                public DataSet Get_CollegeID_Sessionno(int Mode, string College_Nos) //Add by maithili [08-09-2022]
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_MODE",Mode),
                           new SqlParameter("@P_COLLEGE_IDNOS",College_Nos)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_ACAD_GET_CONCAT_COLLEGEID_SESSIONNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }

                //Added by Nehal on 22/03/2023
                public DataSet Get_CollegeID_BySession(int sessionid)
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_SESSIONID",sessionid)
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_ACAD_GET_COLLEGEID_SESSION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }

                #region Call Dispose Methode

                private bool Disposed;

                ~AcademinDashboardController()
                {
                    this.Dispose(false);
                }

                public void Dispose()
                {
                    this.Dispose(true);

                    //Call Garbage Collection .
                    GC.SuppressFinalize(this);
                }

                internal virtual void Dispose(bool Disposing)
                {
                    if (!Disposed)
                    {
                        if (Disposing)
                        { 
                            //Release Managed Code
                        }

                        //Release unmanaged code
                    }

                    Disposed = true;
                }

                #endregion Call Dispose Methode
            }
        }
    }
}
