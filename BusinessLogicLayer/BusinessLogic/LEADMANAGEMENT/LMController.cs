using IITMS;

using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessLogic
        {
            public class LMController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddEnquiryStatus(LeadManage objenq)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ENQUIRYSTATUSNAME", objenq.ENQUIRYSTATUSNAME);
                        objParams[1] = new SqlParameter("@P_ENQUIRYSTATUS", objenq.ENQUIRYSTATUS);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_ENQUIRYSTATUS_INSERT", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.AddEnquiryStatus-> " + ex.ToString());
                    }
                    return status;
                }

                public int UpdateEnquiryStatus(LeadManage objenq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        //update
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ENQUIRYSTATUSNO", objenq.ENQUIRYSTATUSNO);
                        objParams[1] = new SqlParameter("@P_ENQUIRYSTATUSNAME", objenq.ENQUIRYSTATUSNAME);
                        objParams[2] = new SqlParameter("@P_ENQUIRYSTATUS", objenq.ENQUIRYSTATUS);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_ENQUIRYSTATUS_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.UpdateEnquiryStatus-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllEnquiryStatus()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_ALL_ENQUIRYSTATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetAllEnquiryStatus() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetSingleEnquiryStatus(int EnquiryStatusno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ENQUIRYSTATUSNO", EnquiryStatusno) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_LEAD_GET_ENQUIRYSTATUS_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetSingleEnquiryStatus() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }


                public int AddSourceType(LeadManage objenq)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SOURCETYPENAME", objenq.SOURCETYPENAME);
                        objParams[1] = new SqlParameter("@P_SOURCETYPESTATUS", objenq.SOURCETYPESTATUS);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_SOURCE_INSERT", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.AddSourceType-> " + ex.ToString());
                    }
                    return status;
                }

                public int UpdateSourceType(LeadManage objenq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        //update
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SOURCETYPENO", objenq.SOURCETYPENO);
                        objParams[1] = new SqlParameter("@P_SOURCETYPENAME", objenq.SOURCETYPENAME);
                        objParams[2] = new SqlParameter("@P_SOURCETYPESTATUS", objenq.SOURCETYPESTATUS);
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_SOURCE_UPDATE", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SOURCETYPE_UPDATE", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.UpdateSourceType-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllSourceType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_ALL_SOURCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetAllSourceType() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetSingleSourceType(int SourceTypeno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_SOURCETYPENO", SourceTypeno) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_LEAD_GET_SOURCE_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetSingleSourceType() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }
                //added by pooja

                public int SaveExcelSheetDataInDataBase(LeadManage objenq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[22];
                        //objParams[0] = new SqlParameter("@P_ENQGENNO", objenq.EnqGenNo);
                        objParams[0] = new SqlParameter("@P_STUDNAME", objenq.StudName);
                        objParams[1] = new SqlParameter("@P_STUDENTMOBILE", objenq.studentMobile);
                        objParams[2] = new SqlParameter("@P_EMAILID", objenq.EmailId);
                        objParams[3] = new SqlParameter("@P_PARENTSMOBILE", objenq.ParentsMobile);
                        objParams[4] = new SqlParameter("@P_SCHOOL_NAME", objenq.School_Name);
                        objParams[5] = new SqlParameter("@P_CITYNO", objenq.CityNo);
                        objParams[6] = new SqlParameter("@P_LEAD_COLLECTED_BY", objenq.Lead_Collected_by);
                        objParams[7] = new SqlParameter("@P_LEAD_SOURCE_NO", objenq.Lead_Source_No);
                        objParams[8] = new SqlParameter("@P_BATCHNO", objenq.BatchNo);
                        objParams[9] = new SqlParameter("@P_GENDER", objenq.Gender);
                        objParams[10] = new SqlParameter("@P_DOB", objenq.DOB);
                        objParams[11] = new SqlParameter("@P_ADDRESS", objenq.Address);
                        objParams[12] = new SqlParameter("@P_DISTRICTNO", objenq.DistrictNo);
                        objParams[13] = new SqlParameter("@P_STATENO", objenq.StateNo);
                        objParams[14] = new SqlParameter("@P_PIN", objenq.PIN);
                        objParams[15] = new SqlParameter("@P_OrganizationId", objenq.OrganizationId);
                        objParams[16] = new SqlParameter("@P_UA_SECTION", objenq.UA_SECTION);
                        objParams[17] = new SqlParameter("@P_DEGREENO", objenq.DegreeNo);
                        objParams[18] = new SqlParameter("@P_BRANCHNO", objenq.BranchNo);
                        objParams[19] = new SqlParameter("@P_PASSWORD", objenq.Password);
                        objParams[20] = new SqlParameter("@P_ADMBATCH", objenq.ADMBATCH);
                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_UPLOAD_ENQUIRYGENERATION_EXCEL_SHEET", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        {
                            if (obj.ToString() == "1")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (obj.ToString() == "2")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LMController.SaveExcelSheetDataInDataBase() -> " + ex.ToString());
                    }
                    return retStatus;
                }

               
                //Enquiry Form
                //added by pooja
                //Enquiry Form
                public int AddStudentEnquiryInformation(LeadManage objenqform,int confirm,string tempPassword)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        //Insert Student
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_STUDFIRSTNAME", objenqform.StudFirstName.ToUpper());
                        objParams[1] = new SqlParameter("@P_STUDMIDDLENAME", objenqform.StudMiddleName.ToUpper());
                        objParams[2] = new SqlParameter("@P_STUDLASTNAME", objenqform.StudLastName.ToUpper());
                        objParams[3] = new SqlParameter("@P_STUDMOBILE", objenqform.StudMobile);
                        objParams[4] = new SqlParameter("@P_STUDEMAIL", objenqform.StudEmail);
                        objParams[5] = new SqlParameter("@P_DOB", objenqform.DOB);
                        objParams[6] = new SqlParameter("@P_DEGREENO", objenqform.DegreeNo);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objenqform.BranchNo);
                        objParams[8] = new SqlParameter("@P_UA_SECTION", objenqform.UA_SECTION);
                        objParams[9] = new SqlParameter("@P_GENDER", objenqform.Gender);
                        objParams[10] = new SqlParameter("@P_ADDRESS", objenqform.Address);
                        objParams[11] = new SqlParameter("@P_SOURCENO", objenqform.SourceNo);
                        objParams[12] = new SqlParameter("@P_OrganizationId", objenqform.OrganizationId);
                        objParams[13] = new SqlParameter("@P_ADMBATCH", objenqform.BatchNo);
                        objParams[14] = new SqlParameter("@P_CONFIRM", confirm);
                        objParams[15] = new SqlParameter("@P_PASSWORD", tempPassword);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_STUD_ENQUIRY_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.AddStudentEnquiryInformation()-> " + ex.ToString());
                    }

                    return retStatus;
                }



                //Added by Deepali giri on 17/08/2020
                public DataSet GetAllEnquiryData(int AdmBatchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BATCHNO", AdmBatchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_ENQUIRY_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }
                //added by pooja
                public DataSet RetrieveMasterDataForTimeTableExcel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ENQUIRY_STUDENTDETAIL_EXCEL_BLANKSHEET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.RetrieveMasterDataForTimeTableExcel-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetStudentsDetails_LeadAllotment(int AdmBatchno,int ugpgot)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatchno);
                        objParams[1] = new SqlParameter("@P_UGPGOT", ugpgot);
                       // objParams[2] = new SqlParameter("@P_LEVEL", level);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTLIST_FOR_LEAD_ALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LEADMANAGEMENT.LMController.GetStudentsDetails_LeadAllotment-> " + ex.ToString());
                    }
                    return ds;
                }
                public int Add_Student_Record_For_Lead_Allot(string usernos,string degreenos,int mainUser,int uaNo,string ipAddress,int orgId,int admBatch,int programmeType,int main_counsellor,int sub_counseller)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        //Insert Student
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_USERNOS",usernos);
                        objParams[1] = new SqlParameter("@P_DEGREENOS",degreenos);
                        objParams[2] = new SqlParameter("@P_MAIN_USER_NO",mainUser);
                        objParams[3] = new SqlParameter("@P_UANO",uaNo);
                        objParams[4] = new SqlParameter("@P_IPADDRESS",ipAddress);
                        objParams[5] = new SqlParameter("@P_ORGANIZATION_ID",orgId);
                        objParams[6] = new SqlParameter("@P_ADMBATCH",admBatch);
                        objParams[7] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                        //objParams[8] = new SqlParameter("@P_PROGRAMME_LEVEL", level);
                        objParams[8] = new SqlParameter("@P_MAIN_COUNSELLOR", main_counsellor);
                        objParams[9] = new SqlParameter("@P_SUB_COUNSELLOR", sub_counseller);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_LEAD_ENQUERY_DETAILS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.AddStudentEnquiryInformation()-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet Get_Student_Record_For_Lead_Excel(int admBatch, int programmeType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlH = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams=new SqlParameter[2];
                        objParams[0]=new SqlParameter("@P_ADMBATCH",admBatch);
                        objParams[1]=new SqlParameter("@P_PROGRAMME_TYPE",programmeType);
                        ds = objSqlH.ExecuteDataSetSP("PKG_GET_LEAD_ALLOTMENT_STUDENT_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.Get_Student_Record_For_Lead_Excel()-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Nikhil L. on 27/04/2022 to get the students list for model popup for lead module.
                /// </summary>
                /// <param name="userno"></param>
                /// <returns></returns>
                public DataSet Get_Student_Record_ForPopUp_LeadModule(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlH = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        //objParams[1] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                        ds = objSqlH.ExecuteDataSetSP("PKG_GET_APPLICANT_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.Get_Student_Record_ForPopUp_LeadModule()-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Nikhil L. on 27/04/2022 to add lead status.
                /// </summary>
                /// <param name="leadstatus"></param>
                /// <param name="uano"></param>
                /// <param name="lead_uano"></param>
                /// <param name="remark"></param>
                /// <param name="enqueryno"></param>
                /// <param name="action"></param>
                /// <param name="nextfolloup"></param>
                /// <returns></returns>
                public int AddLeadStatus(int leadstatus, int userno, int lead_uano, string remark, int enqueryno, int action, DateTime nextfolloup,int organizationId)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Branch Type
                        {
                         new SqlParameter("@P_LEAD_STATUS", leadstatus),
                         new SqlParameter("@P_USERNO", userno),
                         new SqlParameter("@P_LEAD_UA_NO", lead_uano),
                         new SqlParameter("@P_REMARKS",  remark),
                         new SqlParameter("@P_ENQUERYNO", enqueryno),
                         new SqlParameter("@P_ACTION",  action),
                         new SqlParameter("@P_NEXTFOLLOUP_DATE",  nextfolloup),
                         new SqlParameter("@P_OrganizationId",organizationId),
                         new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_UPD_LEAD_ALLOTMENT", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "1" && obj.ToString() != "2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "2" && obj.ToString() != "1")
                        {
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.LMController.AddLeadStatus() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }

                public DataSet Check_Lead_Status(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlH = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        //objParams[1] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                        ds = objSqlH.ExecuteDataSetSP("PKG_GET_CHECK_LEAD_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.Check_Lead_Status()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet Get_Lead_Remarks(int admBatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlH = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admBatch);
                        //objParams[1] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                        ds = objSqlH.ExecuteDataSetSP("PKG_GET_LEAD_FOLLOWUP_BULK_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.Get_Lead_Remarks()-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Nikhil L. on 28/04/2022 to get the todays lead.
                /// </summary>
                /// <param name="uaNo"></param>
                /// <returns></returns>
                public DataSet Get_Todays_Lead(int uaNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlH = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", uaNo);
                        //objParams[1] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                        ds = objSqlH.ExecuteDataSetSP("PKG_ACD_GET_TODAYS_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.Get_Todays_Lead()-> " + ex.ToString());
                    }
                    return ds;
                }


               
                public DataSet GetUpcomingDates(int userNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_UPCOMING_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetUpcomingDates-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetOverdueDates(int userNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OVERDUE_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetOverdueDates-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetCompleteDates(int userNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", userNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COMPLETED_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetCompleteDates-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllDates(int Adm_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", Adm_type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetAllDates-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Nikhil L. on 29/04/2022 to update the student as completed.
                /// </summary>
                /// <param name="userno"></param>
                /// <param name="uano"></param>
                /// <param name="username"></param>
                /// <param name="name"></param>
                /// <param name="emailid"></param>
                /// <param name="mobile"></param>
                /// <param name="followdate"></param>
                /// <param name="complete"></param>
                /// <returns></returns>
                public int Add_Complete_Follow_Update(int userno, int uano, string name, string emailid, string mobile, DateTime followdate, int complete)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_UA_NO", uano);
                        //objParams[2] = new SqlParameter("@P_USERNAME", username);
                        objParams[2] = new SqlParameter("@P_NAME", name);
                        objParams[3] = new SqlParameter("@P_MOBILENO", emailid);
                        objParams[4] = new SqlParameter("@P_EMAILID", mobile);
                        objParams[5] = new SqlParameter("@P_FOLLOWUP_DATE", followdate);
                        objParams[6] = new SqlParameter("@P_COMPLETED_DATE", complete);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARKED_AS_COMPLETED_FOLLOWUPDATE_LEAD", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.Add_Complete_Follow_Update-> " + ex.ToString());
                    }
                    return retStatus;
                }



                //Added by Pooja
                public DataSet GetSubcouncellorDetails(int MAINUSER_UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MAINUSER_UA_NO", MAINUSER_UA_NO);
                      // objParams[1] = new SqlParameter("@P_UGPGOT", ugpgot);
                        // objParams[2] = new SqlParameter("@P_LEVEL", level);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SUBCOUNCELLOR_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LEADMANAGEMENT.LMController.GetStudentsDetails_LeadAllotment-> " + ex.ToString());
                    }
                    return ds;
                }





            }
        }
    }
}