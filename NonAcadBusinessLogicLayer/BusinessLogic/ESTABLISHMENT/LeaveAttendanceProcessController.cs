using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class LeaveAttendanceProcessController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public string LeaveAttendanceProcess(string idNos, int userIdno, string monYear, string CollegeCode)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNOS", idNos);
                        objParams[1] = new SqlParameter("@P_USER_IDNO", userIdno);
                        objParams[2] = new SqlParameter("@P_DATE", monYear);
                        objParams[3] = new SqlParameter("@P_COLLEGECODE", CollegeCode);

                        objParams[4] = new SqlParameter("@P_MESSAGE", SqlDbType.NVarChar, 1000);
                        objParams[4].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_ATTENDANCE_PROCESS", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToString(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.LeaveAttendanceProcessController.LeaveAttendanceProcess-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
