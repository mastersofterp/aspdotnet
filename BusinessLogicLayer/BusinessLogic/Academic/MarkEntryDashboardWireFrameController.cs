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
            public class MarkEntryDashboardWireFrameController:IDisposable
            {
                string _UAIMS_constr = string.Empty;
                public MarkEntryDashboardWireFrameController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                
                }

                public DataSet GetCourseCollegeDepartmentWisePendingCount(MarkEntryDashboardWireFrameEntity objMEDF)
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    try
                    {
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_SESSIONNO",objMEDF.SESSIONNO),
                           new SqlParameter("@P_REPORT_TYPE",objMEDF.REPORT_TYPE),
                           new SqlParameter("@P_TEMP_EXAMNO",objMEDF.TEMP_EXAMNO),
                           new SqlParameter("@P_COLLEGE_ID",objMEDF.COLLEGE_ID),
                           new SqlParameter("@P_DEPTNO",objMEDF.DEPTNO),
                        };

                        ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GET_MARK_ENTRY_DASHBOARD_WIRE_FRAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }

                public object Insert_MarksEntry_Dashboard_Wire_Frame_Email_Sending_Log(DataTable dt)
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

                        object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[INSERT_MARK_ENTRY_PENDING_DASHBOARD_SENDMAIL_LOG]", sqlParams, true);
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


                #region Dipose function call

                private bool Disposed;
                ~MarkEntryDashboardWireFrameController()
                {
                    this.Dispose(false);
                }

                public void Dispose()
                {
                    this.Dispose(true);

                    GC.SuppressFinalize(this);
                }

                protected virtual void Dispose(bool Disposing)
                {
                    if (!Disposed)
                    {
                        if (Disposing)
                        {
                            //Release manage code
                        }

                        //Release Unmanaged Code.
                    }

                }

                #endregion Dispose function call.

            }
        }
    }
}
