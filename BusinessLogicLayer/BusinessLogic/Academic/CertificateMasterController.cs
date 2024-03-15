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
            /// <summary>
            /// This CertificateMasterController is used to control certificate detail.
            /// </summary>
            public class CertificateMasterController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                //public DataSet GetStudentListForBC(int admbatchNo, int sessionNo, int collegeNo, int degreeNo, int branchNo, int semesterNo)
                //{
                //    DataSet ds = null;

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                //        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                //        objParams[3] = new SqlParameter("@P_DEGREENO", degreeNo);
                //        objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                //        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC", objParams);

                //    }
                //    catch (Exception ex)
                //    {

                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetStudentListForBC(int admbatchNo, int sessionNo, int collegeNo, int degreeNo, int branchNo, int semesterNo, string CertShortName, int partfullno, int certno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[6] = new SqlParameter("@P_CERTNAME", CertShortName);
                        objParams[7] = new SqlParameter("@P_SHIFT", partfullno);
                        objParams[8] = new SqlParameter("@P_CERTNO", certno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }
                //Added by pooja for Discontinue
                public DataSet GetStudentListForDiscontinue(int admbatchNo, int sessionNo, int collegeNo, int degreeNo, int branchNo, int semesterNo, string CertShortName, int partfullno, int certno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[6] = new SqlParameter("@P_CERTNAME", CertShortName);
                        objParams[7] = new SqlParameter("@P_SHIFT", partfullno);
                        objParams[8] = new SqlParameter("@P_CERTNO", certno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_DISCONTINUE_TC", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetStudentListForIssueCertBona(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_ISSUE_CERT_BONAFIDE", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddBonafideCertificate(CertificateMaster objcertMaster, decimal tuitionFee, decimal examFee, decimal otherFee, decimal hostelFee, int certifiate, int organizationid, int status, string branchcode)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_CERT_NO", objcertMaster.CertNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_ATTENDANCE", objcertMaster.Attendance);
                        objParams[3] = new SqlParameter("@P_CONDUCT", objcertMaster.Conduct);

                        if (objcertMaster.LeavingDate == DateTime.MinValue)
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", objcertMaster.LeavingDate);

                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[6] = new SqlParameter("@P_UA_NO", objcertMaster.UaNO);
                        objParams[7] = new SqlParameter("@P_ISSUE_STATUS", objcertMaster.IssueStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objcertMaster.CollegeCode);
                        //objParams[9] = new SqlParameter("@P_SESSIONNO", objcertMaster.SessionNo);
                        //objParams[10] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo);
                        objParams[9] = new SqlParameter("@P_REASON", objcertMaster.Reason);
                        objParams[10] = new SqlParameter("@P_REMARK", objcertMaster.Remark);
                        objParams[11] = new SqlParameter("@P_TUITION_FEE", tuitionFee);
                        objParams[12] = new SqlParameter("@P_EXAM_FEE", examFee);
                        objParams[13] = new SqlParameter("@P_OTHER_FEE", otherFee);
                        objParams[14] = new SqlParameter("@P_HOSTEL_FEE", hostelFee);
                        objParams[15] = new SqlParameter("@P_CONVOCATION_DATE", objcertMaster.ConvocationDate);//Added by Dileep K 09/11/2019
                        objParams[16] = new SqlParameter("@P_CLASS", objcertMaster.Class);
                        objParams[17] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo); //Added by DEEPALI 28/12/2020
                        objParams[18] = new SqlParameter("@P_TC_PARTFULL", certifiate);
                        objParams[19] = new SqlParameter("@P_CONDUCT_NO", objcertMaster.Conduct_No);
                        objParams[20] = new SqlParameter("@P_COMPLETE_PROGRAM", objcertMaster.CompleteProgram);


                        objParams[21] = new SqlParameter("@P_ISSUEDATE", objcertMaster.IssueDate);
                        objParams[22] = new SqlParameter("@P_ORGANIZATIONID", organizationid);//added by pooja
                        objParams[23] = new SqlParameter("@P_GREGNO", objcertMaster.RegNo);
                        objParams[24] = new SqlParameter("@P_STATUS", status);
                        objParams[25] = new SqlParameter("@P_BRANCHCODE", branchcode);


                        objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CERTIFICATE_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddBonafideCertificate-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetStudentListForBC_BYIDNO(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_BC_BYIDNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                //when certificate is issue
                public DataSet GetStudentListForIssueCert(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_ISSUE_CERT", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This method is used to delete record from news table.
                /// </summary>
                /// <param name="newsid">Delete record as per this newsid.</param>
                /// <returns>Integer CustomStatus - Record Deleted or Error</returns>
                public int CanelCertificate(int CERT_TR_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CERT_TR_NO", CERT_TR_NO);

                        objSQLHelper.ExecuteNonQuerySP("PKG_SP_DEL_ISSUE_CERTIFICATE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.certificatemastercontroller.CanelCertificate-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }
                public DataSet GetStudentListForTC(int branchNo, int semesterNo, int admbatchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[2] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_TC", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateIssueStatusCertificate(int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_ISSUE_CERTIFICATE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.certificatemastercontroller.UpdateIssueStatusCertificate-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }
                public DataSet GetStudentListForRegistrationCard(int admbatchNo, int collegeNo, int degreeNo, int branchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SHOW_STUDENTS_FOR_REGISTRATION_CARD ", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetStudentListForMigrationCard(int admbatchNo, int collegeNo, int degreeNo, int branchNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SHOW_STUDENTS_FOR_MIGRATION_CARD ", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDetailsByRegistrationNo(int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForBC-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet getstudentinfo(int admbtach, int regstatus)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbtach);
                        objParams[1] = new SqlParameter("@P_REGSTATUS", regstatus);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_YEARWISE_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }

                // ADDED BY PRANITA HIRADKAR ON 07/12/2021 FOR APPROVE BONAFIDE CERTIFICATE  
                public DataSet GetApproveBonafideCertificate(int admbatch, int certno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_ADMBATCH",admbatch),
                            new SqlParameter("@P_CERT_NO",certno),
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_APPROVE_BONAFIDE_CERTIFICATE", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.CertificateMasterController.GetApproveBonafideCertificate() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int UpdateApproveBonafideCertificateStatus(int idno, int approve_by, string ipaddress, int approvestatus, int cert_no)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_APPROVE_BY", approve_by);
                        objParams[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[3] = new SqlParameter("@P_APPROVE_STATUS", approvestatus);
                        objParams[4] = new SqlParameter("@P_CERT", cert_no);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_APPROVE_BONAFIDE_CERTIFICATE", objParams, true);

                        if (obj != null && obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (obj != null && obj.ToString() == "-99")
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CertificateMasterController.UpdateadmFeeDeduction() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                // ENDED BY PRANITA HIRADKAR ON 07/12/2021 FOR APPROVE BONAFIDE CERTIFICATE 



                //-----------------------------------------------------------------------------------------------
                //ADDED BY PRANITA ON 08/12/2021 FOR FOR EXCEL REPORT OF APPROVE BONAFIDE CERTIFICATE STUDENT
                public DataSet GetExcelBonafideCertificateReport(int admbatchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMBATCH ", admbatchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXCEL_APPROVE_BONAFIDE_CERTIFICATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetExcelBonafideCertificateReport()-> " + ex.ToString());
                    }
                    return ds;
                }
                //ENDED BY PRANITA ON 08/12/2021 FOR FOR EXCEL REPORT OF APPROVE BONAFIDE CERTIFICATE STUDENT


                public DataSet GetAllCertificateStatisticalData()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CERTIFICATE_STATISTICAL_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetAllCertificateStatisticalData-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY JAY ON 26/07/2022
                public DataSet GetStudentListForEBC(int Admyear, int branchNo, int semesterNo, int degreeNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ACADEMICID", Admyear);
                        objParams[1] = new SqlParameter("@P_DEGREENO ", degreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_EBC", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }
                //ADDED BY JAY TAKALKHEDE ON 02-09-2023(RFC.Enhancement.Major.2 (01-09-2023))
                public DataSet AddBankEstimateCertificate(CertificateMaster objcertMaster, int Backno, string TypeofAcc, string AccNo, string Ifsc, int mode)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TYPE_OF_ACC", TypeofAcc);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_Account_No", AccNo);
                        objParams[3] = new SqlParameter("@P_CREATED_BY", objcertMaster.UaNO);
                        objParams[4] = new SqlParameter("@P_MODE", mode);
                        objParams[5] = new SqlParameter("@P_IFSC_Code", Ifsc);
                        objParams[6] = new SqlParameter("@P_BANKNO", Backno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_INSERT_BANK_INFORMATION", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddBankEstimateCertificate-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY JAY TAKALKHEDE ON 27-07-2022
                public int AddEstimateCertificate(CertificateMaster objcertMaster, int ORGID)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[3] = new SqlParameter("@P_UA_NO", objcertMaster.UaNO);
                        objParams[4] = new SqlParameter("@P_ISSUE_STATUS", objcertMaster.IssueStatus);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objcertMaster.CollegeCode);
                        objParams[6] = new SqlParameter("@P_REMARK", objcertMaster.Remark);
                        objParams[7] = new SqlParameter("@P_ORGID", ORGID);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_ESTIMATE_CERTIFICATE_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddBonafideCertificate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Updated by Sakshi on Date 30112023
                public int InsertExpe(string Head, decimal firstAmount, decimal SecondAmount, decimal thirdAmount, decimal fourthAmount, string EXPE_CODE, int AcdYear, int OrgId, string ipAddress, int uano, int insert, string gender, int degreeno, int Admtype)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_HEAD", Head);
                        objParams[1] = new SqlParameter("@P_FIRSTYEAR", firstAmount);
                        objParams[2] = new SqlParameter("@P_SECONDYEAR", SecondAmount);
                        objParams[3] = new SqlParameter("@P_THIRDYEAR", thirdAmount);
                        objParams[4] = new SqlParameter("@P_FOURTHYEAR", fourthAmount);
                        objParams[5] = new SqlParameter("@P_EXPE_CODE", EXPE_CODE);
                        objParams[6] = new SqlParameter("@P_ACDYEAR", AcdYear);
                        objParams[7] = new SqlParameter("@P_ORGID", OrgId);
                        objParams[8] = new SqlParameter("@P_IP", ipAddress);
                        objParams[9] = new SqlParameter("@P_UANO", uano);
                        objParams[10] = new SqlParameter("@P_INS", insert);
                        objParams[11] = new SqlParameter("@P_GENDER", gender);
                        objParams[12] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[13] = new SqlParameter("@P_ADMTYPE", Admtype);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ISERT_EBC_CERTIFICATE_RCPIT", objParams, true);

                        if (obj != null && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() == "2627")
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }
                //Updated by Sakshi M on Date 30112023
                public DataSet GetParticularinfo(int Admyear, string EXPE_CODE, string gender, int degreeno, int Admtype)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ACADEMICID", Admyear);
                        objParams[1] = new SqlParameter("@P_EXPE_CODE", EXPE_CODE);
                        objParams[2] = new SqlParameter("@P_GENDER", gender);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_ADMTYPE", Admtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PARTICULAR_EXPENDITURE", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetStudentListForIssueCert-> " + ex.ToString());
                    }
                    return ds;
                }




                //Added by pooja for certificatereportmaster on date 04-04-2023
                public int AddCertificateReport(CertificateMaster objcertMaster, decimal tuitionFee, decimal examFee, decimal otherFee, decimal hostelFee, int certifiate, int organizationid, int status, string branchcode, DateTime fromdate, DateTime todate, string examseatno, string monthyear, string result)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[32];
                        objParams[0] = new SqlParameter("@P_CERT_NO", objcertMaster.CertNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objcertMaster.IdNo);
                        objParams[2] = new SqlParameter("@P_ATTENDANCE", objcertMaster.Attendance);
                        objParams[3] = new SqlParameter("@P_CONDUCT", objcertMaster.Conduct);

                        if (objcertMaster.LeavingDate == DateTime.MinValue)
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_LEAVING_DATE", objcertMaster.LeavingDate);

                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", objcertMaster.IpAddress);
                        objParams[6] = new SqlParameter("@P_UA_NO", objcertMaster.UaNO);
                        objParams[7] = new SqlParameter("@P_ISSUE_STATUS", objcertMaster.IssueStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objcertMaster.CollegeCode);
                        //objParams[9] = new SqlParameter("@P_SESSIONNO", objcertMaster.SessionNo);
                        //objParams[10] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo);
                        objParams[9] = new SqlParameter("@P_REASON", objcertMaster.Reason);
                        objParams[10] = new SqlParameter("@P_REMARK", objcertMaster.Remark);
                        objParams[11] = new SqlParameter("@P_TUITION_FEE", tuitionFee);
                        objParams[12] = new SqlParameter("@P_EXAM_FEE", examFee);
                        objParams[13] = new SqlParameter("@P_OTHER_FEE", otherFee);
                        objParams[14] = new SqlParameter("@P_HOSTEL_FEE", hostelFee);
                        objParams[15] = new SqlParameter("@P_CONVOCATION_DATE", objcertMaster.ConvocationDate);//Added by Dileep K 09/11/2019
                        objParams[16] = new SqlParameter("@P_CLASS", objcertMaster.Class);
                        objParams[17] = new SqlParameter("@P_SEMESTERNO", objcertMaster.SemesterNo); //Added by DEEPALI 28/12/2020
                        objParams[18] = new SqlParameter("@P_TC_PARTFULL", certifiate);
                        objParams[19] = new SqlParameter("@P_CONDUCT_NO", objcertMaster.Conduct_No);
                        objParams[20] = new SqlParameter("@P_COMPLETE_PROGRAM", objcertMaster.CompleteProgram);


                        objParams[21] = new SqlParameter("@P_ISSUEDATE", objcertMaster.IssueDate);
                        objParams[22] = new SqlParameter("@P_ORGANIZATIONID", organizationid);//added by pooja
                        objParams[23] = new SqlParameter("@P_GREGNO", objcertMaster.RegNo);
                        objParams[24] = new SqlParameter("@P_STATUS", status);
                        objParams[25] = new SqlParameter("@P_BRANCHCODE", branchcode);
                        objParams[26] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[27] = new SqlParameter("@P_TODATE", todate);
                        objParams[28] = new SqlParameter("@P_EXAMSEATNO", examseatno);
                        objParams[29] = new SqlParameter("@P_MONTHYEAR", monthyear);
                        objParams[30] = new SqlParameter("@P_RESULT", result);


                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CERTIFICATE_INSERT_RCPIPER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.AddCertificateReport-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetParticularinfoForSem(int Admyear, int degreeno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ACADEMICID", Admyear);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PARTICULAR_EXPENDITURE_SEMESTERWISE", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CertificateMasterController.GetParticularinfoForSem-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertExpeForSem(string Head, decimal firstAmount, int AcdYear, int OrgId, string IpAddress, int UaNO, int insert, int degreeno)
                {

                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_HEAD", Head);
                        objParams[1] = new SqlParameter("@P_FIRSTYEAR", firstAmount);
                        objParams[2] = new SqlParameter("@P_ACDYEAR", AcdYear);
                        objParams[3] = new SqlParameter("@P_ORGID", OrgId);
                        objParams[4] = new SqlParameter("@P_IP", IpAddress);
                        objParams[5] = new SqlParameter("@P_UANO", UaNO);
                        objParams[6] = new SqlParameter("@P_INS", insert);
                        objParams[7] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ISERT_EBC_CERTIFICATE_DAIICT", objParams, true);

                        if (obj != null && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() == "2627")
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.SessionActivityController.AddSessionActivity() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;

                }


            }
        }
    }
}
