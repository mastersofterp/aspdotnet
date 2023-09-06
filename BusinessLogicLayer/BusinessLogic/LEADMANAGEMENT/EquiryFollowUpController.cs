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
            public class EquiryFollowUpController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                    /// <summary>
                   /// Get Equiry List as per Level Configuration User.
                  /// </summary>
                 /// <param name="UA_NO"></param>
                /// <returns></returns>
                public DataSet GetStudentEnquiryFolloupList(int UA_NO,int BatchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_EQUIRY_FOLLOWUP_BY_UA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EquiryFollowUpController.GetStudentEnquiryFolloupList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                    /// <summary>
                   /// Update Equiry Follow Up Status.
                  /// </summary>
                 /// <param name="ds"></param>
                /// <returns></returns>
                public int UpdateEnquiryFollowUpStatus(DataTable ds)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ENQUIRY_FOLLOW_UPTBL", ds);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_EQUIRY_FOLLOWUP_STATUS_UPDATE", objParams, true);
                        if (obj != null && obj.ToString() == "1")
                        {
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (obj.ToString() == "-99")
                        {
                            status = Convert.ToInt32(CustomStatus.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EquiryFollowUpController.UpdateEnquiryFollowUpStatus-> " + ex.ToString());
                    }
                    return status;
                }


                    /// <summary>
                   /// Get Equiry Done List as per user.
                  /// </summary>
                 /// <param name="UA_NO"></param>
                /// <returns></returns>
                public DataSet GetStudentEnquiryFolloupDoneList(int UA_NO, int BatchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_EQUIRY_FOLLOWUP_DONE_BY_UA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EquiryFollowUpController.GetStudentEnquiryFolloupDoneList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                    /// <summary>
                   /// Get Equiry List in Excel of Particular Equiry Start & End Date & Time wise.
                  /// </summary>
                 /// <param name="UA_NO"></param>
                /// <returns></returns>
                public DataSet GetStudentEnquiryFolloupListExcelReport(int UA_NO, int BatchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_EQUIRY_FOLLOWUP_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EquiryFollowUpController.GetStudentEnquiryFolloupListExcelReport() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                  /// <summary>
                 /// Update Repeat Schedule Automatically When User on EquiryFollowUp.aspx Page.
                /// </summary>
                public void UpdateEnquiryFollowUpScheduleAutomatic()
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_STUDENT_REPEAT_ENQUIRY_FOLLOWUP_AUTO", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EquiryFollowUpController.UpdateEnquiryFollowUpScheduleAutomatic-> " + ex.ToString());
                    }
                }
                
            }
        }
    }
}
