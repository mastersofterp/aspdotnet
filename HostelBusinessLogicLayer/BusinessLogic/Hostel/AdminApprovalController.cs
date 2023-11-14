using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AdminApprovalController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllGatePass()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["idno"]));
                        objParams[1] = new SqlParameter("@P_USERTYPE", Convert.ToInt32(System.Web.HttpContext.Current.Session["usertype"]));

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelGatePassController.GetAllGatePass() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateApproval(int recid, int Approve, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_RECORDID",recid),
                            new SqlParameter("@P_Approve",Approve),
                            new SqlParameter("@P_Remark",Remark),
                            new SqlParameter("@P_UANO",  Convert.ToInt32(System.Web.HttpContext.Current.Session["usertype"])),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_GATEPASS_REQUEST_APPROVE_BY_ADMIN", param, true);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.PrepareData-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
