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

                public DataSet GetAllGatePass(string Applydate, int Purpose, string Todate, string Fromdate, string Status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["idno"]));
                        objParams[1] = new SqlParameter("@P_USERTYPE", Convert.ToInt32(System.Web.HttpContext.Current.Session["usertype"]));

                        //Below Code Added By Himanshu Tamrakar 02042024
                        objParams[2] = new SqlParameter("@P_TODATE", Todate);
                        objParams[3] = new SqlParameter("@P_FROMDATE", Fromdate);
                        objParams[4] = new SqlParameter("@P_PURPOSE", Purpose);
                        objParams[5] = new SqlParameter("@P_APPLYDATE", Applydate);
                        objParams[6] = new SqlParameter("@P_STATUS", Status);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelGatePassController.GetAllGatePass() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateApproval(int recid, string Approve, string Remark)  //Change int approve to string approve  By himanshu tamrakar 12/03/2024
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
                            new SqlParameter("@P_UANO",  Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"])),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_GATEPASS_REQUEST_APPROVE_BY_ADMIN", param, true);
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

                public int UpdateApprovalsAndPath(int recid, int AA1, int AA2, int AA3, int AA4, string PATH)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_RECORDID",recid),
                            new SqlParameter("@P_AA1",AA1),
                            new SqlParameter("@P_AA2",AA2),
                            new SqlParameter("@P_AA3",AA3),
                            new SqlParameter("@P_AA4",AA4),
                            new SqlParameter("@P_PATH",PATH),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_UPDATE_APRROVALS_PATH_BY_ADMIN", param, true);
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
