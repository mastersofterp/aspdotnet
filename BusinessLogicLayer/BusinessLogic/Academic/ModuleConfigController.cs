using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic.Academic
        {
            public class ModuleConfigController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                /// <summary>
                /// Added by SP -- Modified by Shailendra K, Saurabh S.,Rohit M.
                /// </summary>
                /// <param name="objConfig"></param>
                /// <returns></returns>
                /// //
                /// 
                /// Modified By Vinay Mishra on 01/08/2023(New Flag Course Related) 
                public int SaveModuleConfiguration(ModuleConfig objConfig, int UANO, string IPAddress, string Mac_Address, bool trisem, bool chkoutsatnding,
                bool sempromdemand, bool semadmissionoffbtn, bool semadmbeforesempromotion, bool semadmissionaftersempromotion, bool studReactvationlarefine,
                bool IntakeCapacity, bool chktimeReport, bool chkGlobalCTAllotment, string BBCMailSENTRY, bool hosteltypeselection, bool chkElectChoiceFor,
                    bool Seatcapacitynewstud, string Usernos, bool Dashboardoutstanding, string AttendanceUser, string CourseShow, bool Timeslotmandatory,
                    string UserLoginNos, string CourseLocked, bool DisplayStudLoginDashboard, bool DisplayReceiptInHTMLFormat, bool chkValueAddedCTAllotment,
                    bool CreateRegno, bool AttTeaching, bool createprnt, int AllowCurrSemForRedoImprovementCrsReg, string ModAdmInfoUserNos)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = null;

                        sqlParams = new SqlParameter[52];

                        sqlParams[0] = new SqlParameter("@Configid", objConfig.Configid);
                        sqlParams[1] = new SqlParameter("@AllowRegno", objConfig.AllowRegno);
                        sqlParams[2] = new SqlParameter("@AllowRollno", objConfig.AllowRollno);
                        sqlParams[3] = new SqlParameter("@AllowEnrollno", objConfig.AllowEnrollno);
                        sqlParams[4] = new SqlParameter("@COURSE_EXAM_REG_BOTH", objConfig.CourseExmRegSame);
                        //sqlParams[5] = new SqlParameter("@COURSE_EXAM_REG_SEPERATE", objConfig.course_exam_reg_seperate);
                        sqlParams[5] = new SqlParameter("@ONLINE_BTN_SEM_ADM", objConfig.OnlinebtnStudadm);
                        sqlParams[6] = new SqlParameter("@STUD_INFO_MANDATE", objConfig.StudInfoMandate);
                        sqlParams[7] = new SqlParameter("@EMAIL_TYPE", objConfig.EmailType);
                        sqlParams[8] = new SqlParameter("@NEW_STUD_EMAIL", objConfig.NewStudEmailSend);
                        sqlParams[9] = new SqlParameter("@FACULTY_ADVISOR", objConfig.FacultyAdvisorApp);
                        sqlParams[10] = new SqlParameter("@USERNOFEECOLLECTION", objConfig.FeeCollUserCreation);
                        sqlParams[11] = new SqlParameter("@REGNOCREATION", objConfig.RegnoGenFeeCollection);
                        sqlParams[12] = new SqlParameter("@NEWSTUDUSERCREATION", objConfig.NewStudUserCreation);
                        sqlParams[13] = new SqlParameter("@THIRDPARTYPAYMAIL", objConfig.TPPaymentLinkSendMail);
                        sqlParams[14] = new SqlParameter("@TPDOCUMENTVERIFICATION", objConfig.TPDocverificationAllow);
                        sqlParams[15] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        sqlParams[16] = new SqlParameter("@P_UANO", UANO);
                        sqlParams[17] = new SqlParameter("@P_IP_ADDRESS", IPAddress);
                        sqlParams[18] = new SqlParameter("@P_MAC_ADDRESS", Mac_Address);
                        sqlParams[19] = new SqlParameter("@P_TRISEM", trisem);
                        sqlParams[20] = new SqlParameter("@P_CHKOUTSTANDING", chkoutsatnding);
                        sqlParams[21] = new SqlParameter("@P_SEMPROMDEMANDCREATION", sempromdemand);
                        sqlParams[22] = new SqlParameter("@P_OFFBTNSEMADMISSION", semadmissionoffbtn);
                        sqlParams[23] = new SqlParameter("@P_SemadmissionBeforeSempromotion", semadmbeforesempromotion);
                        sqlParams[24] = new SqlParameter("@P_SemadmissionAfterSemPromotion", semadmissionaftersempromotion);
                        sqlParams[25] = new SqlParameter("@P_STUDENT_REACTIVATION_LATEFINE", studReactvationlarefine);
                        sqlParams[26] = new SqlParameter("@P_SEM_ADM_WITH_PAYMENT", objConfig.SemAdmWithPayment);
                        sqlParams[27] = new SqlParameter("@P_IS_DEPARTMENT_ELECTIVE_CAPACITY_CHECK", IntakeCapacity);
                        sqlParams[28] = new SqlParameter("@P_IS_SHORTNAME", chktimeReport);
                        sqlParams[29] = new SqlParameter("@P_IS_GLOBAL_ELECTIVE_CT_ALLOTMENT_REQUIRED", chkGlobalCTAllotment);
                        sqlParams[30] = new SqlParameter("@P_BBC_MAIL_NEW_STUD_ENTRY", BBCMailSENTRY);
                        sqlParams[31] = new SqlParameter("@P_HOSTE_TYPE_ONLINE_PAY", hosteltypeselection);
                        sqlParams[32] = new SqlParameter("@P_IS_SELECT_CHOICEFOR_OF_ELECT_CRS_FROM_CRDIT_DEFINITION_PAGE", chkElectChoiceFor);
                        sqlParams[33] = new SqlParameter("@P_SEAT_WISE_CAPACITY_NEW_STUD", Seatcapacitynewstud);
                        sqlParams[34] = new SqlParameter("@P_USERNOS_FOR_HEAD_TO_HEAD_ADJ_PAGE", Usernos);
                        sqlParams[35] = new SqlParameter("@P_DASHBOARD_OUTSTANDING", Dashboardoutstanding);
                        sqlParams[36] = new SqlParameter("@P_ATTENDANCE_USER_TYPE", AttendanceUser);
                        sqlParams[37] = new SqlParameter("@P_COURSE_USER_TYPE", CourseShow);
                        sqlParams[38] = new SqlParameter("@P_TP_SLOT_MANDATORY", Timeslotmandatory);
                        sqlParams[39] = new SqlParameter("@P_AUTHORISED_USERS_FOR_GO_TO_USERLOGIN", UserLoginNos);
                        sqlParams[40] = new SqlParameter("@P_USERS_LOCK_UNLOCK", CourseLocked);
                        sqlParams[41] = new SqlParameter("@P_DISPLAY_STUD_LOGIN_DASHBOARD", DisplayStudLoginDashboard);
                        sqlParams[42] = new SqlParameter("@P_DISPLAY_HTML_REPORT", DisplayReceiptInHTMLFormat);
                        sqlParams[43] = new SqlParameter("@P_IS_VALUE_ADDED_CT_ALLOTMENT_REQUIRED", chkValueAddedCTAllotment);
                        sqlParams[44] = new SqlParameter("@P_NEW_STUD_REGNO_GEN", CreateRegno);
                        sqlParams[45] = new SqlParameter("@P_VALUE_ADDED_ON_TEACHINGPLAN_ATT", AttTeaching);
                        sqlParams[46] = new SqlParameter("@P_NEW_PARENT_USER_CREATION", createprnt);
                        sqlParams[47] = new SqlParameter("@P_ALLOW_CURRENT_SEM_FOR_REDO_IMPROVE_CRS_REG", AllowCurrSemForRedoImprovementCrsReg);

                        sqlParams[48] = new SqlParameter("@P_AUTHORISED_USERS_FOR_MODIFY_ADMISSION_INFO", ModAdmInfoUserNos);

                        // Added by Gopal M 03102023 - Ticket #46419
                        sqlParams[49] = new SqlParameter("@P_OUTSTANDING_FEECOLLECTION", objConfig.OUTSTANDING_FEECOLLECTION);
                        sqlParams[50] = new SqlParameter("@P_OUTSTANDING_MESSAGE", objConfig.OUTSTANDING_MESSAGE);
                        sqlParams[51] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        sqlParams[51].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_MODULE_CONFIGURATION_INSERT_UPDATE", sqlParams, true);
                        status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.SaveModuleConfiguration() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                /// <summary>
                /// Added by SP
                /// </summary>
                /// <returns></returns>
                public DataSet GetModuleConfigData()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_MODULE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigAffilationTypeController.GetModuleConfigData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                /// <summary>
                /// Added by SP
                /// </summary>
                /// <returns></returns>

                public DataSet GetStudentConfigData(int OrgID, string PageNo, string PageName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@ORGID", OrgID);
                        objParams[1] = new SqlParameter("@PAGENO", PageNo);
                        objParams[2] = new SqlParameter("@P_PAGENAME", PageName);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_STUDENT_CONFIG_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigAffilationTypeController.GetModuleConfigData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int SaveUpdateStudentConfig(List<StudentModuleConfig> objStudentConfig)
                {
                    int status = 0;
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["OrgId"] == null)
                        {
                            return -99;
                        }
                        foreach (StudentModuleConfig StudentConfig in objStudentConfig)
                        {
                            SQLHelper objSQLHelper = new SQLHelper(connectionString);
                            SqlParameter[] objParams = new SqlParameter[7];
                            objParams[0] = new SqlParameter("@STUDCONFIG_ID", Convert.ToInt32(StudentConfig.STUDCONFIG_ID));
                            objParams[1] = new SqlParameter("@CAPTION_NAME", StudentConfig.CAPTION_NAME);
                            objParams[2] = new SqlParameter("@ISACTIVE", StudentConfig.ISACTIVE);
                            objParams[3] = new SqlParameter("@ISMANDATORY", StudentConfig.ISMANDATORY);
                            objParams[4] = new SqlParameter("@ORGANIZATION_ID", System.Web.HttpContext.Current.Session["OrgId"]);
                            objParams[5] = new SqlParameter("@PAGE_NO", StudentConfig.PAGE_NO);
                            objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[6].Direction = ParameterDirection.Output;

                            object objRef = objSQLHelper.ExecuteNonQuerySP("PKG_SP_STUDENT_CONFIGURATION_INSERT_UPDATE", objParams, true);
                            status = Convert.ToInt32(objRef);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.SaveUpdateStudentConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                // added new function for COurse and Exam Registration configuration on dated 14.10.2022 by Shailendra   
                public int UpsertCourseExamRegConfig(ModuleConfig objMod, int clgID)
                {
                    int status = 0;
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["OrgId"] == null)
                            return -99;

                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@CONFIGID", Convert.ToInt32(objMod.Configid));
                        objParams[1] = new SqlParameter("@COURSE_EXAM_REG_BOTH", objMod.CourseExmRegSame);
                        objParams[2] = new SqlParameter("@P_UANO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"].ToString()));
                        objParams[3] = new SqlParameter("@P_IP_ADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[4] = new SqlParameter("@ORG_ID", System.Web.HttpContext.Current.Session["OrgId"]);
                        objParams[5] = new SqlParameter("@COLLEGE_CODE", System.Web.HttpContext.Current.Session["colcode"]);
                        objParams[6] = new SqlParameter("@COLLEGE_ID", clgID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object objRef = objSQLHelper.ExecuteNonQuerySP("PKG_SP_MODULE_CONFIG_UPSERT_COURSE_EXAM_REG", objParams, true);
                        status = Convert.ToInt32(objRef);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.UpsertCourseExamRegConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int InsertAttendanceMailConfig(string dailymail, string absentmail, string studcc, string studbcc, string dailycc, string dailybcc, string absentcc, string absentbcc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_DAILY_FAC_TO_MAIL", dailymail);
                        objParams[1] = new SqlParameter("@P_ABSENT_STUD_TO_MAIL", absentmail);
                        objParams[2] = new SqlParameter("@P_STUD_CC_MAIL", studcc);
                        objParams[3] = new SqlParameter("@P_STUD_BCC_MAIL", studbcc);
                        objParams[4] = new SqlParameter("@P_DAILY_FAC_CC_MAIL", dailycc);
                        objParams[5] = new SqlParameter("@P_DAILY_FAC_BCC_MAIL", dailybcc);
                        objParams[6] = new SqlParameter("@P_ABSENT_STUD_CC_MAIL", absentcc);
                        objParams[7] = new SqlParameter("@P_ABSENT_STUD_BCC_MAIL", absentbcc);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ATTENDANCE_TRIGGER_MAIL_CONFIG_SP_INS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.InsertAttendanceMailConfig-> " + ex.ToString());
                    }
                }

                public DataSet GetAttendanceTriggerMailConfig()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_ATTENDANCE_TRIGGER_MAIL_CONFIG_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.GetAttendanceTriggerMailConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetPaymentDetailsofSemesterAdmissionConfig()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PAYMENT_DATA_OF_SEMESTER_ADMISSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.GetPaymentDetailsofSemesterAdmissionConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetPaymentDetailsofSemesterAdmissionConfigforEdit( int PaymodeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PAYMODENO", PaymodeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PAYMENT_DATA_OF_SEMESTER_ADMISSION_FOR_UPDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.GetPaymentDetailsofSemesterAdmissionConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int AddPaymentTypeDetailsConfiguration(int PaymentModeNo, string PaymentMode, string AccHolderName, string BankName, string AccountNo, string IFSCCode, string BranchName, double BounceCharges, string fileName, int ActiveStatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_PAYMODENGetDailyAdmissionStatussendemailConfigO", PaymentModeNo);
                        objParams[1] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                        objParams[2] = new SqlParameter("@P_ACCHOLDERNAME", AccHolderName);
                        objParams[3] = new SqlParameter("@P_BANKNAME", BankName);
                        objParams[4] = new SqlParameter("@P_ACCOUNTNO", AccountNo);
                        objParams[5] = new SqlParameter("@P_IFSCCODE", IFSCCode);
                        objParams[6] = new SqlParameter("@P_BRANCHNAME", BranchName);
                        objParams[7] = new SqlParameter("@P_BOUNCECHARGES", BounceCharges);
                        objParams[8] = new SqlParameter("@P_CHALLAN_FILE_NAME", fileName);
                        objParams[9] = new SqlParameter("@P_ACTIVESTATUS", ActiveStatus);
                        objParams[10] = new SqlParameter("@P_UANO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        objParams[11] = new SqlParameter("@P_IPADDRESS",System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[12] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_PAYMENT_CONFIG", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.AddPaymentTypeDetailsConfiguration-> " + ex.ToString());
                    }
                }

                //Added by pooja for daily admission status email configuration on date 05-08-2023
                public int InsertDailyAdmissionEmailConfig(string dailyTomail, string dailyccmail, string dailybccmail)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_DAILY_TO_MAIL", dailyTomail);
                        objParams[1] = new SqlParameter("@P_DAILY_CC_MAIL", dailyccmail);
                        objParams[2] = new SqlParameter("@P_DAILY_BCC_MAIL", dailybccmail);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ACD_ADMISSION_STATUS_EMAIL_CONFIG_SP_INS", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.InsertAttendanceMailConfig-> " + ex.ToString());
                    }
                }

                public DataSet GetAdmissionStatusEMailConfig()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_ADMISSION_STATUS_EMAIL_CONFIG_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.GetAttendanceTriggerMailConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetDailyAdmissionStatussendemailConfig(int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DAILY_ADMISSION_STATUS_STUDENT_COUNT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.GetPaymentDetailsofSemesterAdmissionConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAdmissionStatussendemailConfigDaily()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ADMISSION_STATUS_STUDENT_COUNT_DETAILS_DAILY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.GetPaymentDetailsofSemesterAdmissionConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
