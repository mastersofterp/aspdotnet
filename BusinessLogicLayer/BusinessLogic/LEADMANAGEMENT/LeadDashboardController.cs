using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
           public class LeadDashboardController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetLeadDashboardDetail(int Batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BATCHNO", Batchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_DASHBOARD_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeadDashboardController.GetLeadDashboardDetail() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
