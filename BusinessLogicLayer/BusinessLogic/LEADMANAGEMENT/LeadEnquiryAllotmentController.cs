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
            public class LeadEnquiryAllotmentController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAdmissionBatchwiseStudentList(int Admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BATCHNO", Admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_LEAD_STUDENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeadEnquiryAllotmentController.GetAdmissionBatchwiseStudentList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int InsertUpdateEnquiryAllotment(DataTable ds)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ENQUIRYALLOTEMENTTBL", ds);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT_INSERT_UPDATE", objParams, true);
                        if (obj != null && obj.ToString() == "1")
                        {
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (obj.ToString() == "-99")
                        {
                            status = Convert.ToInt32(CustomStatus.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeadEnquiryAllotmentController.InsertUpdateEnquiryAllotment-> " + ex.ToString());
                    }
                    return status;
                }

                public DataSet GetAdmissionBatchwiseStudentListInExcel(int Admbatch,int Lead_UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_BATCH_NO", Admbatch);
                        objParams[1] = new SqlParameter("@P_LEAD_UA_NO", Lead_UA_No);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_LEAD_STUDENT_ENQUIRY_ALLOTEMENT_DETAIL_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeadEnquiryAllotmentController.GetAdmissionBatchwiseStudentListInExcel() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
