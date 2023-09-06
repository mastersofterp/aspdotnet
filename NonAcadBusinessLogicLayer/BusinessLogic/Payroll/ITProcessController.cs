using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ITProcessController
            {

                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int ITCalculation(string fromDate, string toDate, int collegeNo, int staffNo, int idNo, int proposedSal, int userIdno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        objParams[3] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[4] = new SqlParameter("@P_EMPNO", idNo);
                        objParams[5] = new SqlParameter("@P_PROPOSEDSAL", proposedSal);
                        objParams[6] = new SqlParameter("@P_USERID", userIdno);
                        objParams[7] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_IT_CALCULATION", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITProcessController.ITCalculation-> " + ex.ToString());
                    }
                    return retStatus;
                }

            }
        }
    }
}
