//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : MARKSHEET INVENTORY REPORT CONTROLLER
// CREATION DATE : 09-JAN-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class MarksheetInventoryRptController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetMarksheetRptData(string sessionNo, string examNo, string schemeNo, string courseNo, string collegeCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_EXAMNO", examNo),
                    new SqlParameter("@P_SCHEMENO", schemeNo),
                    new SqlParameter("@P_COURSENO", courseNo),
                    new SqlParameter("@P_COLLEGE_CODE", collegeCode)
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_MARKSHEET_INVENTORY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchemeController.GetControlsheetRptData --> " + ex.ToString());
            }
            return ds;
        }
    }
}