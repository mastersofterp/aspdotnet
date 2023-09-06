using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class ProvisionalCertificateController
            {
                string _UAIMS_constr = string.Empty;
                public ProvisionalCertificateController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                }

                public object Insert_Provisional_Certificate_Log(ProvisionalCertificateEntity objPCEntity)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO", objPCEntity.IDNO),
                            new SqlParameter("@P_SESSIONNO", objPCEntity.SESSIONNO),
                            new SqlParameter("@P_CREATE_BY", objPCEntity.CREATE_BY),
                            new SqlParameter("@P_IPADDRESS", objPCEntity.IPADDRESS),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_INSERT_PROVISION_CERTIFICATE_LOG]", sqlParams, true);
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



                 #region Call Dispose Methode

                private bool Disposed;

                ~ProvisionalCertificateController()
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
