using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
---------------------------------------------------------------------------------------------------------------------------                                                                      
Created By : Ashutosh Dhobe                                                                 
Created On : 04-04-2024                                               
Purpose    : To manage student information pages controls                                                      
Version    : 1.0.0                                                                 
---------------------------------------------------------------------------------------------------------------------------                                                                        
Version      Modified On      Modified By        Purpose                                                                        
---------------------------------------------------------------------------------------------------------------------------                                                                        
                 
------------------------------------------- -------------------------------------------------------------------------------    
*/

using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessLogic.Academic
        {
            public class PageControlValidationController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public int SaveModuleConfiguration(ModuleConfig
                objConfig,
                int UANO,
                string IPAddress,
                string Mac_Address,
                bool trisem,
                bool chkoutsatnding,
                bool sempromdemand,
                bool semadmissionoffbtn,
                bool semadmbeforesempromotion,
                bool semadmissionaftersempromotion,
                bool studReactvationlarefine,
                bool IntakeCapacity,
                bool chktimeReport,
                bool chkGlobalCTAllotment,
                string BBCMailSENTRY,
                bool hosteltypeselection,
                bool chkElectChoiceFor,
                bool Seatcapacitynewstud,
                string Usernos,
                bool Dashboardoutstanding,
                string AttendanceUser,
                string CourseShow,
                bool Timeslotmandatory,
                string UserLoginNos,
                string CourseLocked,
                bool DisplayStudLoginDashboard,
                bool DisplayReceiptInHTMLFormat,
                bool chkValueAddedCTAllotment,
                bool CreateRegno,
                bool AttTeaching,
                bool createprnt,
                int AllowCurrSemForRedoImprovementCrsReg,
                string ModAdmInfoUserNos,
                string session_ids,
                string college_ids,
                int studAttendance,
                int RecEmail,
                int PartPay,
                string ParMinAmount,
                bool AddNote)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = null;
                        sqlParams = new SqlParameter[62];
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
                        // Added by Gopal M 01112023 - Ticket #50097
                        sqlParams[51] = new SqlParameter("@P_FEE_HEAD_GROUP", objConfig.FEE_HEAD_GROUP);
                        sqlParams[52] = new SqlParameter("@P_FEE_RECEIPT_COPIES", objConfig.FEE_RECEIPT_COPIES);
                        sqlParams[53] = new SqlParameter("@P_TOSHOW_FEEREC_STUDLOGIN", objConfig.TOSHOW_FEEREC_STUDLOGIN);
                        sqlParams[54] = new SqlParameter("@P_SESSION_IDS", session_ids);
                        sqlParams[55] = new SqlParameter("@P_COLLEGE_IDS", college_ids);
                        sqlParams[56] = new SqlParameter("@P_ATTENDANCE_STUDDISPLAY", studAttendance);
                        sqlParams[57] = new SqlParameter("@P_RECEMAIL", RecEmail); //Added By Jay Takalkhede on date 17-02-2024
                        sqlParams[58] = new SqlParameter("@P_ENABLEPARPAYMENT", PartPay); //Added By Jay Takalkhede on date 17-02-2024
                        sqlParams[59] = new SqlParameter("@P_PARTMIN_AMOUNT", ParMinAmount); //Added By Jay Takalkhede on date 17-02-2024
                        sqlParams[60] = new SqlParameter("@P_FEEDBACK_NOTE_FLAG", AddNote);
                        sqlParams[61] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        sqlParams[61].Direction = ParameterDirection.Output;

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


                public DataSet GetStudentConfigData(int OrgID, string PageNo, string PageName, string section)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@ORGID", OrgID);
                        objParams[1] = new SqlParameter("@PAGENO", PageNo);
                        objParams[2] = new SqlParameter("@P_PAGENAME", PageName);
                        objParams[3] = new SqlParameter("@P_SECTION", section);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAGE_CONTROL_VALITION_DATA", objParams);
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
                            SqlParameter[] objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@STUDCONFIG_ID", Convert.ToInt32(StudentConfig.STUDCONFIG_ID));
                            objParams[1] = new SqlParameter("@CAPTION_NAME", StudentConfig.CAPTION_NAME);
                            objParams[2] = new SqlParameter("@ISACTIVE", StudentConfig.ISACTIVE);
                            objParams[3] = new SqlParameter("@ISMANDATORY", StudentConfig.ISMANDATORY);
                            objParams[4] = new SqlParameter("@ISEDITABLE", StudentConfig.ISEDITABLE);
                            objParams[5] = new SqlParameter("@ORGANIZATION_ID", System.Web.HttpContext.Current.Session["OrgId"]);
                            objParams[6] = new SqlParameter("@PAGE_NO", StudentConfig.PAGE_NO);
                            objParams[7] = new SqlParameter("@IS_DISPLAY_SECTION_NAME", StudentConfig.DISPLAYSECTION);
                            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[8].Direction = ParameterDirection.Output;

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
            }

        }
    }
}