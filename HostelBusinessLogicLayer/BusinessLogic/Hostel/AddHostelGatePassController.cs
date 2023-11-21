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
            public class AddHostelGatePassController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetHostelGatePassInfo(int gatepassno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GATEPASSNO", gatepassno);
                        //objParams[1] = new SqlParameter("@P_IDNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["idno"]));
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_INFO_SEARCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddHostelGatePassController.GetHostelGatePassInfo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllGatePass()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_ALL_GATEPASS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelPurposeController.GetAllPurpose() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //Added by Saurabh L on 21 Nov 2023 
                public int UpdateColumnData(string TableName, string columnname, string Wherecondition)
                {
                    int ret;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                        objParams[1] = new SqlParameter("@P_COLUMNNAME", columnname);

                        if (!Wherecondition.Equals(string.Empty))
                            objParams[2] = new SqlParameter("@P_WHERECONDITION", Wherecondition);
                        else
                            objParams[2] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);

                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object obj = objsqlhelper.ExecuteNonQuerySP("PKG_UTILS_SP_UPDATE", objParams, true);

                        if (obj != null && obj.ToString().Equals("-1"))
                        {
                            ret = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            ret = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return ret;
                }
            }
        }
    }
}
