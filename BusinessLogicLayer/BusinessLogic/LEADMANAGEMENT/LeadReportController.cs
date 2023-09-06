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
           public class LeadReportController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllLeadExcelReport(int AdmissionBatch,int rdoButtionId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BATCH_NO", AdmissionBatch);
                        if (rdoButtionId==1) //Consolited Report
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_CONSOLIDATED_EXCEL_REPORT", objParams);
                        else if (rdoButtionId == 2) //Enquiry_Lead_Alloted_and_Not_Alloted
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_ENQUIRY_LEAD_ALLOTED_NOTALLOTED_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeadReportController.GetAllLeadExcelReport() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
