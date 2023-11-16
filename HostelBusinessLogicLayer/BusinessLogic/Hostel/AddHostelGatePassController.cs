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

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_INFO_SEARCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddHostelGatePassController.GetHostelGatePassInfo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
