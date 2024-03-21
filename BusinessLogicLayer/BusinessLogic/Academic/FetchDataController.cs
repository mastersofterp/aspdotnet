using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class FetchDataController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetStudentData(int BRANCHNO, int DEGREENO, string REGDATE, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[2] = new SqlParameter("@P_APPDATE", REGDATE);
                objParams[3] = new SqlParameter("@P_ADMBATCH", admBatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL", objParams);


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetApplicantListInBranchPrefOrder(int DEGREENO, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_DEGREENO",DEGREENO),            
                new SqlParameter("@P_ADMBATCH",admBatch),

               
                };

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_WITH_COURSE_CODE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ONLINE_ADMISSION_REG_DATA_DEGREEWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAppliedStudentData(int DEGREENO, int BRANCHNO, string AppDate, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                if (DEGREENO > 0)
                {
                    objParams[0] = new SqlParameter("@P_DEGREENO", DEGREENO);
                }
                else
                {
                    objParams[0] = new SqlParameter("@P_DEGREENO", DBNull.Value);
                }
                if (BRANCHNO > 0)
                {
                    objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                }
                else
                {
                    objParams[1] = new SqlParameter("@P_BRANCHNO", DBNull.Value);
                }
                //if (AppDate != DateTime.MinValue)
                if (AppDate != string.Empty)
                {
                    objParams[2] = new SqlParameter("@P_APPDATE", AppDate);
                }
                else
                {
                    objParams[2] = new SqlParameter("@P_APPDATE", DBNull.Value);
                }
                if (admBatch > 0)
                {
                    objParams[3] = new SqlParameter("@P_ADMBATCH", admBatch);
                }
                else
                {
                    objParams[3] = new SqlParameter("@P_ADMBATCH", DBNull.Value);
                }
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAppliedStudentData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetUserData(int AdmBatch, int Degreeno, int Branchno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_ADMBATCH", AdmBatch),
                new SqlParameter("@P_DEGREENO",Degreeno),
                new SqlParameter("@P_BRANCHNO",Branchno)};

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_DETAILS_FOR_EXCELSHEET", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetUserData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetStudentListPaymentDetails(int BRANCHNO, int DEGREENO, string REGDATE, int admBatch, int reportType)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_DEGREENO",DEGREENO),
                new SqlParameter("@P_BRANCHNO", BRANCHNO),                
                new SqlParameter("@P_APPDATE",REGDATE),
                new SqlParameter("@P_ADMBATCH",admBatch),
                new SqlParameter("@P_REPORTTYPE",reportType),
                };

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_WITH_COURSE_CODE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL3", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetStudentListDatewise(int BRANCHNO, int DEGREENO, string REGDATE, int admBatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_DEGREENO",DEGREENO),
                new SqlParameter("@P_BRANCHNO", BRANCHNO),                
                new SqlParameter("@P_APPDATE",REGDATE),
                new SqlParameter("@P_ADMBATCH",admBatch)
                };

                //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_WITH_COURSE_CODE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL2", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UnlockConfirmStatus(int userno, int unlockby, DateTime uldate, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                        {   
                             new SqlParameter("@P_USERNO",userno),
                             new SqlParameter("@P_UNLOKED_BY",unlockby),
                             new SqlParameter("@P_UNLOCK_DATE",uldate),
                             new SqlParameter("@P_IPADDRESS",ipaddress),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_CONFIRM_STATUS", objParams, true);

                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddCopyCase --> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added By S.patil -18072019
        /// </summary>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <param name="semesterno"></param>
        /// <param name="prev_status"></param>
        /// <param name="sessionno"></param>
        /// <param name="colg"></param>
        /// <returns></returns>
        public DataSet GetStudentListForSendMail(int colg, int degreeno, int branchno, int semesterno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", colg);
                // objParams[6] = new SqlParameter("@P_ADMBTCH", admbatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_STUDENT_FOR_MAILSEND", objParams);

            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Deelip Kare - 16102019
        /// </summary>
        /// <param name="colg"></param>
        /// <param name="degree"></param>
        /// <param name="branch"></param>
        /// <param name="session"></param>
        /// <param name="scheme"></param>
        /// <param name="sem"></param>
        /// <returns></returns>
        public DataSet GetStudentDetailsForExamForm(int colg, int session, int degree, int branch, int scheme, int sem)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objhelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COLLEGEID", colg);
                objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", sem);
                ds = objhelper.ExecuteDataSetSP("PKG_GET_STUDENTDEATAILS_FOR_EXAMFORMGENRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentDetailsForExamForm->" + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// Added By S.Patil- 30102019
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public DataSet GetFacultyTimeTableCourses(int sessionno, int USERID, int uatype)
        {
            DataSet ds;
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_USERID", USERID);
                objParams[2] = new SqlParameter("@P_UA_TYPE", uatype);
                ds = objsqlhelper.ExecuteDataSetSP("PKG_GET_AllCOURSES_TIMETABLE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetFacultyTimeTableCourses->" + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added by S.Patil - 30032020
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="USERID"></param>
        /// <param name="uatype"></param>
        /// <returns></returns>
        public DataSet GetCoursesForAttendanceFromHomePage(int sessionno, int USERID, int uatype, int dayno)
        {
            DataSet ds;
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_USERID", USERID);
                objParams[2] = new SqlParameter("@P_UA_TYPE", uatype);
                objParams[3] = new SqlParameter("@P_DAYNO", dayno);
                ds = objsqlhelper.ExecuteDataSetSP("PKG_GET_AllCOURSES_TIMETABLE_FORFACULTY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetFacultyTimeTableCourses->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentListForSendMailAndMsg(int colg, int degreeno, string branchno, int Admbatch)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", colg);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@ADMBATCH", Admbatch);

                // objParams[6] = new SqlParameter("@P_ADMBTCH", admbatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_STUDENT_FOR_SEND_MAIL_AND_SMS", objParams);

            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForSendMail-> " + ex.ToString());
            }
            return ds;
        }

        // Added By Nikhil On 03/11/2020 To get login details
        public DataSet GetLoginAnalysis(DateTime FromDate, DateTime Todate, int userType)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_FROMDATE", FromDate.ToString("MM/dd/yyyy"));
                objParams[1] = new SqlParameter("@P_TODATE", Todate.ToString("MM/dd/yyyy"));
                objParams[2] = new SqlParameter("@P_USERTYPE", userType);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LOGIN_ANALYSIS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetLoginAnalysis-> " + ex.ToString());
            }
            return ds;
        }

        // for homeFaculty page 22102020
        public DataSet GetCoursesForAttendanceFromHomeFaculty(int sessionno, int USERID, int uatype)
        {
            DataSet ds;
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_USERID", USERID);
                objParams[2] = new SqlParameter("@P_UA_TYPE", uatype);
                objParams[3] = new SqlParameter("@P_DAYNO", System.DateTime.Now.DayOfWeek);
                ds = objsqlhelper.ExecuteDataSetSP("PKG_GET_AllCOURSES_TIMETABLE_FORFACULTY_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetCoursesForAttendanceFromHomePage->" + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added by Nikhil L. on 07/01/2022 to get the online portal applicants details.
        /// </summary>
        /// <param name="admBatch"></param>
        /// <param name="programmeType"></param>
        /// <param name="applicationStatus"></param>
        /// <returns></returns>
        public DataSet GetApplicantUserList(int admBatch, int programmeType, int applicationStatus, int degreeNo)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_ADMBATCH",admBatch),            
                new SqlParameter("@P_PROGRAMME_TYPE",programmeType),
                new SqlParameter("@P_APPLICATION_STATUS",applicationStatus),               
                new SqlParameter("@P_DEGREENO",degreeNo),              
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetApplicantUserList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. on 07/01/2022 to get complete details of applicants.
        /// </summary>
        /// <param name="admBatch"></param>
        /// <param name="programmeType"></param>
        /// <param name="applicationStatus"></param>
        /// <returns></returns>
        public DataSet GetApplicantsCompleteDetails(int admBatch, int programmeType, int applicationStatus, int degreeNo)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_ADMBATCH",admBatch),            
                new SqlParameter("@P_PROGRAMME_TYPE",programmeType),
                new SqlParameter("@P_APPLICATION_STATUS",applicationStatus),               
                new SqlParameter("@P_DEGREENO",degreeNo),               
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_COMPLETE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetApplicantsCompleteDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetOnlineStudentsCountForExcelAdmissionCount(int Admbatch, int degreeNo, int Application_Status, int programmeType, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[2] = new SqlParameter("@P_APPLICATION_STATUS", Application_Status);
                objParams[3] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ONLINE_ADMISSION_STUDENT_COUNT_FOR_EXCEL_ADMISSIONCOUNT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetOnlineStudentsCountForExcelAdmissionCount()-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetStudentDatanew(int Admbatch, int degreeNo, int Application_Status, int programmeType, int branchno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[2] = new SqlParameter("@P_APPLICATION_STATUS", Application_Status);
                objParams[3] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL", objParams);


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 04.08.2021
        /// </summary>
        /// <param name="admbatch"></param>
        /// <returns></returns>
        public DataSet GetFormFillingStatus(int admbatch, int Application_Status, int degreeNo, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_APPLICATION_STATUS", Application_Status);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_FORM_FILLING_STATUS_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetFormFillingStatus()-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentsDumpData_PG(int degree, int admBatch, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OA_DUMBD_DATA_PG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.FetchDataController.GetStudentsDumpData_PG()-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 05.08.2021
        /// </summary>
        /// <param name="admbatch"></param>
        /// <returns></returns>
        public DataSet GetStudentCompleteDetails(int degree, int admBatch)//, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                //objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OA_DUMBD_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetFormFillingStatus()-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetForeignStudentData(int Admbatch)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALIAH_FOREIGN_STUDENT_DETAILS_FOR_EXCEL_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetForeignStudentData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetOnlinePaymentStudentDetailsForProvisonalAdm(int Admbatch, string Programme_code, string From_date, string To_Date, int programmeType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_PROGRAMME_CODE", Programme_code);
                objParams[2] = new SqlParameter("@P_FROM_DATE", From_date);
                objParams[3] = new SqlParameter("@P_TO_DATE", To_Date);
                objParams[4] = new SqlParameter("@P_UGPGOT", programmeType);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_DETAILS_FOR_PROVISIONAL_ADM", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetOnlineStudentsCountForExcelAdmissionCount()-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetConfirmStudentsDetails(int Admbatch, int degree, int programmType, int Application_Status)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_PROGRAMME_TYPE", programmType);
                objParams[3] = new SqlParameter("@P_APPLICATION_STATUS", Application_Status);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSION_CONFIRM_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetConfirmStudentsDetails()-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. on 05-10-2021 to get the document status list in applied student list.
        /// </summary>
        /// <param name="Admbatch"></param>
        /// <param name="programmeType"></param>
        /// <returns></returns>
        public DataSet GetDocumentListStatus(int Admbatch, int programmeType, int Application_Status, int degree)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                objParams[2] = new SqlParameter("@P_APPLICATION_STATUS", Application_Status);
                objParams[3] = new SqlParameter("@P_DEGREENO", degree);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DOCUMENT_LIST_STATUS_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetDocumentListStatus()-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Nikhil L. on 26/10/2021 to get complete Phd data.
        /// </summary>
        /// <param name="Admbatch"></param>
        /// <returns></returns>
        public DataSet GetPhdCompleteStudentData(int Admbatch)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OA_DUMBD_DATA_PHD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetPhdCompleteStudentData()-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added by Nikhil L. on 22-11-2021 to get the summary report of admission.
        /// </summary>
        /// <param name="Admbatch"></param>
        /// <param name="ugpgot"></param>
        /// <param name="programme_Code"></param>
        /// <returns></returns>
        public DataSet GetAdmissionSummaryReport(int Admbatch, int ugpgot, int degree)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_UGPGOT", ugpgot);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ADMISSION_SUMMARY_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetAdmissionSummaryReport()-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetMinimumReportExcel(int Admbatch, int ugpgot, int degree)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_UGPGOT", ugpgot);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_MINIMUM_INFORMATION_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetMinimumReportExcel()-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentDetailsForExamForm(int colg, int session, int degree, int branch, int scheme, int sem, int Type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objhelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_COLLEGEID", colg);
                objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", sem);
                objParams[6] = new SqlParameter("@P_TYPE", Type);
                ds = objhelper.ExecuteDataSetSP("PKG_GET_STUDENTDEATAILS_FOR_EXAMFORMGENRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentDetailsForExamForm->" + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 05.08.2021
        /// </summary>
        /// <param name="admbatch"></param>
        /// <returns></returns>
        public DataSet GetStudentCompleteDetails(int degree, int admBatch, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OA_DUMBD_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetFormFillingStatus()-> " + ex.ToString());
            }
            return ds;
        }

        /// Added By Dileep Kare on 30.07.2021
        /// </summary>
        /// <param name="Admbatch"></param>
        /// <returns></returns>
        public DataSet GetOnlinePaymentStudentDetailsForProvisonalAdm(int Admbatch, int degree, int programmeType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_UGPGOT", programmeType);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ONLINE_PAYMENT_DETAILS_FOR_PROVISIONAL_ADM", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetOnlineStudentsCountForExcelAdmissionCount()-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetApplicantUserListNew(int admBatch, int programmeType, int applicationStatus, int degreeNo)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_ADMBATCH",admBatch),            
                new SqlParameter("@P_PROGRAMME_TYPE",programmeType),
                new SqlParameter("@P_APPLICATION_STATUS",applicationStatus),               
                new SqlParameter("@P_DEGREENO",degreeNo),              
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_LIST_SHOW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetApplicantUserList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public DataSet GetStudentCompleteDetailsNew(int degree, int admBatch, int Progtype, int appstatus, int branchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[2] = new SqlParameter("@P_PROGRAMME_TYPE", Progtype);
                objParams[3] = new SqlParameter("@P_APP_STATUS", appstatus);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OA_DUMBD_DATA_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetFormFillingStatus()-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetReplyForQueryManagement(int userNo, int queryNo)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_USERNO",userNo),            
                new SqlParameter("@P_CATEGORYNO",queryNo),               
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_REPLY_QUERY_MANAGEMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetReplyForQueryManagement() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetSubjectStatus(int subId)
        {
            DataSet ds = null;
            try
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_SUBJECT_ID", subId);
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_SUBJECT_LIST_STATUS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.GetSubjectStatus() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public int AddRegistrationDetails(User objus, int userNO, string motherTongue)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[78];
                objParams[0] = new SqlParameter("@P_USERNO", userNO);
                objParams[1] = new SqlParameter("@P_GENDER", objus.Gender);
                objParams[2] = new SqlParameter("@P_FIRSTNAME", objus.FIRSTNAME);
                objParams[3] = new SqlParameter("@P_DOB", objus.DOB);
                objParams[4] = new SqlParameter("@P_RELIGION", objus.Religion);
                objParams[5] = new SqlParameter("@P_COMMUNITY", objus.Community);
                objParams[6] = new SqlParameter("@P_MOTHER_TONGUE", objus.Mother_Tongue);
                objParams[7] = new SqlParameter("@P_AADHAR", objus.Aadhar);
                objParams[8] = new SqlParameter("@P_NATIONALITY", objus.Nationality);
                objParams[9] = new SqlParameter("@P_FATHER_NAME", objus.Father_Name);
                objParams[10] = new SqlParameter("@P_FATHER_OCCUPATION", objus.Father_Occupation);
                objParams[11] = new SqlParameter("@P_FATHER_MOBILE", objus.Father_Mobile);
                objParams[12] = new SqlParameter("@P_MOTHER_NAME", objus.Mother_Name);
                objParams[13] = new SqlParameter("@P_MOTHER_OCCUPATION", objus.Mother_Occupation);
                objParams[14] = new SqlParameter("@P_MOTHER_MOBILE", objus.Mother_Mobile);
                objParams[15] = new SqlParameter("@P_ADDRESS_1", objus.Address_1);
                objParams[16] = new SqlParameter("@P_ADDRESS_2", objus.Address_2);
                //Photo
                if (objus.PHOTO == null)
                    objParams[17] = new SqlParameter("@P_PHOTO", DBNull.Value);
                else
                    objParams[17] = new SqlParameter("@P_PHOTO", objus.PHOTO);
                objParams[17].SqlDbType = SqlDbType.Image;

                objParams[18] = new SqlParameter("@P_ADDRESS_3", objus.Address_3);
                objParams[19] = new SqlParameter("@P_CITY", objus.City);
                objParams[20] = new SqlParameter("@P_STATE_NO", objus.State_No);
                objParams[21] = new SqlParameter("@P_PINCODE", objus.PinCode);
                objParams[22] = new SqlParameter("@P_EDU_INFORMATION", objus.Edu_Info);
                objParams[23] = new SqlParameter("@P_EXM_REG_12", objus.Exm_Reg_12);
                objParams[24] = new SqlParameter("@P_SCHOOL_NAME", objus.School_name);
                objParams[25] = new SqlParameter("@P_MONTH_PASS_NO", objus.Month_No);
                objParams[26] = new SqlParameter("@P_MONTH_PASS", objus.Month);
                objParams[27] = new SqlParameter("@P_YEAR_PASS_NO", objus.Year_No);
                objParams[28] = new SqlParameter("@P_YEAR_PASS", objus.Year);
                objParams[29] = new SqlParameter("@P_MEDIUM_NO", objus.Medium_No);
                objParams[30] = new SqlParameter("@P_MEDIUM", objus.Medium);
                objParams[31] = new SqlParameter("@P_COUNTRY_NAME", objus.Country_Name);
                objParams[32] = new SqlParameter("@P_SUB_1", objus.Sub_1);
                objParams[33] = new SqlParameter("@P_MARKS_OBT_1", objus.Marks_Obt_1);
                objParams[34] = new SqlParameter("@P_MAX_MARKS_1", objus.Max_Marks_1);
                objParams[35] = new SqlParameter("@P_PER_1", objus.Per_1);
                objParams[36] = new SqlParameter("@P_LANGUAGE", objus.Language);
                objParams[37] = new SqlParameter("@P_MARKS_OBT_LANG", objus.Marks_Obt_Lang);
                objParams[38] = new SqlParameter("@P_MAX_MARKS_LANG", objus.Max_Marks_Lang);
                objParams[39] = new SqlParameter("@P_PER_LANG", objus.Per_Lang);
                objParams[40] = new SqlParameter("@P_SUB_2", objus.Sub_2);
                objParams[41] = new SqlParameter("@P_MARKS_OBT_2", objus.Marks_Obt_2);
                objParams[42] = new SqlParameter("@P_MAX_MARKS_2", objus.Max_Marks_2);
                objParams[43] = new SqlParameter("@P_PER_2", objus.Per_2);
                objParams[44] = new SqlParameter("@P_SUB_3", objus.Sub_3);
                objParams[45] = new SqlParameter("@P_MARKS_OBT_3", objus.Marks_Obt_3);
                objParams[46] = new SqlParameter("@P_MAX_MARKS_3", objus.Max_Marks_3);
                objParams[47] = new SqlParameter("@P_PER_3", objus.Per_3);
                objParams[48] = new SqlParameter("@P_SUB_4", objus.Sub_4);
                objParams[49] = new SqlParameter("@P_MARKS_OBT_4", objus.Marks_Obt_4);
                objParams[50] = new SqlParameter("@P_MAX_MARKS_4", objus.Max_Marks_4);
                objParams[51] = new SqlParameter("@P_PER_4", objus.Per_4);
                objParams[52] = new SqlParameter("@P_SUB_5", objus.Sub_5);
                objParams[53] = new SqlParameter("@P_MARKS_OBT_5", objus.Marks_Obt_5);
                objParams[54] = new SqlParameter("@P_MAX_MARKS_5", objus.Max_Marks_5);
                objParams[55] = new SqlParameter("@P_PER_5", objus.Per_5);
                objParams[56] = new SqlParameter("@P_SUB_6", objus.Sub_6);
                objParams[57] = new SqlParameter("@P_MARKS_OBT_6", objus.Marks_Obt_6);
                objParams[58] = new SqlParameter("@P_MAX_MARKS_6", objus.Max_Marks_6);
                objParams[59] = new SqlParameter("@P_PER_6", objus.Per_6);
                objParams[60] = new SqlParameter("@P_OTHER_SPECIFY", objus.Other_Specify);
                objParams[61] = new SqlParameter("@P_CUT_OFF", objus.Cut_off);
                objParams[62] = new SqlParameter("@P_RELIGION_OTHER", objus.Religion_Other);
                objParams[63] = new SqlParameter("@P_FOCCUPATION_OTHER", objus.FOccupation_Other);
                objParams[64] = new SqlParameter("@P_MOCCUPATION_OTHER", objus.MOccupation_Other);
                objParams[65] = new SqlParameter("@P_EDU_INFO_OTHER", objus.Edu_Info_Other);
                objParams[66] = new SqlParameter("@P_MEDIUM_OTHER", objus.Medium_Other);
                objParams[67] = new SqlParameter("@P_AVAILABLE", objus.Available);
                objParams[68] = new SqlParameter("@P_COUNTRY_ID", objus.Country_Id);
                objParams[69] = new SqlParameter("@P_ANNUAL_INCOME", objus.AnnualIncome);
                objParams[70] = new SqlParameter("@P_INSTITUTE_ADV", objus.InstituteADV);
                objParams[71] = new SqlParameter("@P_INSTITUTE_ADV_OTHER", objus.InstituteADV_Other);
                objParams[72] = new SqlParameter("@P_PHASE", objus.Phase);
                objParams[73] = new SqlParameter("@P_M_TONGUE_OTHER", motherTongue);
                objParams[74] = new SqlParameter("@P_DISTRICT", objus.District);
                objParams[75] = new SqlParameter("@P_PHOTO_PATH", objus.Photo_Path);
                objParams[76] = new SqlParameter("@P_CONTRIBUTE", objus.contribute);
                objParams[77] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[77].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_NEW_USER_REGISTRATION", objParams, true);
                retStatus = Convert.ToInt32(ret);

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
            }
        }
        public DataSet GetAllRegistrationDetails(string userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                      new SqlParameter("@P_USERNO", userno)
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_REGISTRATION_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.GetAllRegistrationDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetUserDetailsByUserno(string userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                      new SqlParameter("@P_USERNO", userno)
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_DETAILS_BY_USERNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserController.GetUserDetailsByUserno() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet Get_Students_Status_Details(int userNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_USER_INFO_FOR_STATUS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UserController.Get_Students_Status_Details()-> " + ex.ToString());
            }
            return ds;
        }




        /// <summary>
        /// Added by Nehal 07-02-2023
        /// </summary>
        /// <param name="JECRC"></param>
        /// <returns></returns>

        public DataSet GetApplicantUserListNew_JECRC(int admBatch, int programmeType, int applicationStatus, int degreeNo)
            {
            DataSet ds = new DataSet();
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_ADMBATCH",admBatch),            
                new SqlParameter("@P_PROGRAMME_TYPE",programmeType),
                new SqlParameter("@P_APPLICATION_STATUS",applicationStatus),               
                new SqlParameter("@P_DEGREENO",degreeNo),              
                };
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_LIST_JECRC", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_LIST_SHOW", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetApplicantUserListNew_JECRC() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        public DataSet GetAllBranchDetails(int USERNO)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", USERNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BRANCHPREFERENCE_DETAILS", objParams);
                }
            catch (Exception ex)
                {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetAllBranchDetails-> " + ex.ToString());
                }
            return ds;
            }

        public DataSet GetDoclist(int userno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DOCUMENTENTRY_SEL", objParams);


                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetDoclist() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        public DataSet GetRecordByUANo(string userno)
            {
            DataSet ds = null;

            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_REG_GET_USER", objParams);

                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetRecordByUANo-> " + ex.ToString());
                }
            return ds;
            }

        //modified by nehal on 19/04/2023
        public DataSet GetDocumentList_OA(string userno, int degreeno, int admType, int nationality)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_ADMTYPE", admType);
                objParams[3] = new SqlParameter("@P_NATIONALITY", nationality);
                //new SqlParameter("@P_ADMTYPE", admType)
                ds = objSQLHelper.ExecuteDataSetSP("PKG_OA_GET_DOCUMENT_LIST", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetDocumentList_OA() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }

        public DataSet GetEditBranchProgram(string userno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_USERNO", userno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_BRANCH_PREFERENCE_BIND", objParams);
                }
            catch (Exception ex)
                {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FetchDataController.GetEditBranchProgram-> " + ex.ToString());
                }
            return ds;
            }
        public DataSet GetAllBranchDetails(string userno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BRANCHPREFERENCE_DETAILS", objParams);
                }
            catch (Exception ex)
                {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetAllBranchDetails-> " + ex.ToString());
                }
            return ds;
            }
        public DataSet GetRecordForPersonalDetails(string userno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_REG_GET_USERDATA", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetRecordForPersonalDetails() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        public DataSet GetEntranceQualDetails(string userno, int branch)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", Convert.ToInt32(userno));
                objParams[1] = new SqlParameter("@P_BRANCH", Convert.ToInt32(branch));

                ds = objSQLHelper.ExecuteDataSetSP("PKG_OA_GET_ENTRANCE_QUAL_DETAILS", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetEntranceQualDetails() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }

        public int UpdateDocument(Document objDocuments, int DOCTYPENO, string temp)
            {
            int status = 0;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USERNO",objDocuments.USERNO),
                    new SqlParameter("@P_DOCTYPENO",DOCTYPENO), 
                    new SqlParameter("@P_PATH",objDocuments.PATH),
                    new SqlParameter("@P_FILENAME",objDocuments.FILENAME)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENTENTRY_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            catch (Exception ex)
                {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.UpdateDocument() --> " + ex.Message + " " + ex.StackTrace);
                }
            return status;
            }
        public DataSet GetEducationalDetails(string userno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EDU_DETAILS", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.FetchDataController.Common.GetEducationalDetails() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        public DataSet GetEducationalDetails_Qual(string userno, int stlqno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_STLQNO", stlqno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EDU_DETAILS_QUAL", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.FetchDataController.Common.GetEducationalDetails_Qual() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        public int UpadateBranchPreference1(int DEGREENO, int BRANCHNO, int ADM_TYPE, string brprno, double Fees, string userno, int isSpec)
            {

            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_BRPRNO", brprno);
                objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[2] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[3] = new SqlParameter("@P_ADM_TYPE", ADM_TYPE);
                objParams[4] = new SqlParameter("@P_FEES", Fees);
                objParams[5] = new SqlParameter("@P_USER_NO", userno);
                objParams[6] = new SqlParameter("@P_IS_SPEC", isSpec);

                objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_USER_UPD_BRANCH_PREF_EDIT", objParams, true));

                if (ret.ToString() == "2" && ret != null)
                    {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                else if (ret.ToString() == "-99")
                    {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                else if (ret.ToString() == "2627")
                    {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.UpadateBranchPreference1-> " + ex.ToString());
                }
            return retStatus;
            }

        public int SubmitPersonalandBankDetails(User obju)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[38];

                objParams[0] = new SqlParameter("@P_USERNO", obju.USERNO);
                objParams[1] = new SqlParameter("@P_FATHERNAME", obju.FATHERNAME);
                objParams[2] = new SqlParameter("@P_MOTHERNAME", obju.MOTHERNAME);
                objParams[3] = new SqlParameter("@P_GENDER", obju.GENDER);
                objParams[4] = new SqlParameter("@P_EMAILID", obju.EMAILID);
                objParams[5] = new SqlParameter("@P_ALTEREMAILID", obju.ALTERNATEEMAILID);
                objParams[6] = new SqlParameter("@P_NATIONALITYNO", obju.NATIONALITY);
                objParams[7] = new SqlParameter("@P_RELIGIONNO", obju.RELIGION);
                objParams[8] = new SqlParameter("@P_CATEGORYNO", obju.CATEGORY);
                objParams[9] = new SqlParameter("@P_BLOODGRPNO", obju.BLOODGRP);
                objParams[10] = new SqlParameter("@P_IDENTITY_MARK", obju.IDENTIFICATIONMARK);
                objParams[11] = new SqlParameter("@P_ADHAARNO", obju.ADHAARNO);
                objParams[12] = new SqlParameter("@P_MARITALSTATUS", obju.MaritalStatus);
                objParams[13] = new SqlParameter("@P_DIFFERENTLY_ABLED", obju.Differently_Abled);
                objParams[14] = new SqlParameter("@P_NATURE_DISABILITY", obju.Nature_Disability);
                objParams[15] = new SqlParameter("@P_PERCENTAGE_DISABILITY", obju.Percentage_Disability);
                objParams[16] = new SqlParameter("@P_STATE_DOMICILE", obju.State_Domicile);
                objParams[17] = new SqlParameter("@P_SPORTS_PERSON", obju.Sports_Person);
                objParams[18] = new SqlParameter("@P_SPORTS_REPRESENTED", obju.Sports_Represented);
                objParams[19] = new SqlParameter("@P_F_TELNUMBER", obju.F_TelNumber);
                objParams[20] = new SqlParameter("@P_F_MOBILENO", obju.F_MobileNo);
                objParams[21] = new SqlParameter("@P_F_OCCUPATION", obju.F_Occupation);
                objParams[22] = new SqlParameter("@P_F_DESIGNATION", obju.F_Designation);
                objParams[23] = new SqlParameter("@P_FEMAILADDRESS", obju.F_EmailAddress);
                objParams[24] = new SqlParameter("@P_M_TELNUMBER", obju.M_TelNumber);
                objParams[25] = new SqlParameter("@P_M_MOBILENO", obju.M_MobileNo);
                objParams[26] = new SqlParameter("@P_MOCCUPATION", obju.M_Occupation);
                objParams[27] = new SqlParameter("@P_M_DESIGNATION", obju.M_Designation);
                objParams[28] = new SqlParameter("@P_M_EMAILADDRESS", obju.M_EmailAddress);
                objParams[29] = new SqlParameter("@P_ANNUAL_INCOME", obju.ParentsAnnualIncome);

                objParams[30] = new SqlParameter("@P_SPORTS_NAME", obju.sportsName);
                objParams[31] = new SqlParameter("@P_SPORTS_DOC", obju.sportsDoc);
                objParams[32] = new SqlParameter("@P_SPORTS_DOC_PATH", obju.sportsDocPath);

                objParams[33] = new SqlParameter("@P_F_OTHER_OCCUPATION", obju.F_OccupationOther);
                objParams[34] = new SqlParameter("@P_M_OTHER_OCCUPATION", obju.M_OccupationOther);
                objParams[35] = new SqlParameter("@P_HOST_TRANS", obju.Host_Trans);
                objParams[36] = new SqlParameter("@P_HOST_TRANS_NAME", obju.Host_Trans_Name);
                objParams[37] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[37].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_NEWUSER_REGISTRATION_UPDATE", objParams, true);
                if (ret.ToString() == "2" && ret != null)
                    {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                else if (ret.ToString() == "-99")
                    {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                else if (ret.ToString() == "2627")
                    {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.SubmitPersonalandBankDetails-> " + ex.ToString());
                retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            return retStatus;
            }

        public string SubmitAddressDetails(NewUser objnu, User obju)
            {
            string retStatus = string.Empty;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[20];

                objParams[0] = new SqlParameter("@P_USERNO", obju.USERNO);
                objParams[1] = new SqlParameter("@P_STATE", objnu.STATE);
                objParams[2] = new SqlParameter("@P_CITY", objnu.CITY);
                objParams[3] = new SqlParameter("@P_CADDRESS", objnu.CADDRESS);
                objParams[4] = new SqlParameter("@P_PADDRESS", objnu.PADDRESS);
                objParams[5] = new SqlParameter("@P_PCITY", objnu.PCITY);
                objParams[6] = new SqlParameter("@P_PSTATE", objnu.PSTATE);
                objParams[7] = new SqlParameter("@P_CDISTRICT", objnu.CDISTRICT);
                objParams[8] = new SqlParameter("@P_PDISTRICT", objnu.PDISTRICT);
                objParams[9] = new SqlParameter("@P_CPINNO", objnu.CPINNO);
                objParams[10] = new SqlParameter("@P_PPINNO", objnu.PPINNO);
                objParams[11] = new SqlParameter("@P_COUNTRY", objnu.COUNTRY);
                objParams[12] = new SqlParameter("@P_P_COUNTRY", objnu.P_COUNTRY);
                objParams[13] = new SqlParameter("@P_COUNTRYID", objnu.CountryId);
                objParams[14] = new SqlParameter("@P_STATEID", objnu.StateId);
                objParams[15] = new SqlParameter("@P_CITYID", objnu.CityId);
                objParams[16] = new SqlParameter("@P_PCOUNTRYID", objnu.PCountryId);
                objParams[17] = new SqlParameter("@P_PSTATEID", objnu.PStateId);
                objParams[18] = new SqlParameter("@P_PCITYID", objnu.PCityId);
                objParams[19] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[19].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_NEWUSER_REGISTRATION_ADDRESS_DETAILS", objParams, false);
                retStatus = ret.ToString();

                return retStatus;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                }
            }

        public string SubmitPhotoandSignature(NewUser objnu, User obju)
            {
            string retStatus = string.Empty;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_USERNO", obju.USERNO);
                if (objnu.PHOTO == null)
                    objParams[1] = new SqlParameter("@P_PHOTO", DBNull.Value);
                else
                    objParams[1] = new SqlParameter("@P_PHOTO", objnu.PHOTO);
                objParams[1].SqlDbType = SqlDbType.Image;
                if (objnu.Studsign == null)
                    objParams[2] = new SqlParameter("@P_STUDSIGN", DBNull.Value);
                else
                    objParams[2] = new SqlParameter("@P_STUDSIGN", objnu.Studsign);
                objParams[2].SqlDbType = SqlDbType.Image;
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_NEWUSER_REGISTRATION_PHOTO_SIGN_DETAILS", objParams, false);
                retStatus = ret.ToString();

                return retStatus;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                }
            }
        public string UpdateEdcucationalDetails(NewUser objnu, User objus, string QualifyType, int stlqno)
            {
            string retStatus = string.Empty;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];

                objParams[0] = new SqlParameter("@P_USERNO", objus.USERNO);
                objParams[1] = new SqlParameter("@P_STLQNO", stlqno);
                objParams[2] = new SqlParameter("@P_QUALIFYNO", objnu.QualifyNo);
                objParams[3] = new SqlParameter("@P_QUALIFYEXAMNAME", objnu.ExamName);
                objParams[4] = new SqlParameter("@P_QUALIFYTYPE", QualifyType);
                objParams[5] = new SqlParameter("@P_PERCENTAGE", objnu.Percentage);
                objParams[6] = new SqlParameter("@P_OBTAINEDMARKS", objnu.MarksObtained);
                objParams[7] = new SqlParameter("@P_OUTOFMARKS", objnu.TotalMarks);

                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("INS_UPDATE_PREVIOUS_EXAM_DETAILS", objParams, false);
                retStatus = ret.ToString();

                return retStatus;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                }
            }
        public int Update_RegistrationDetails(User objus, int userNO, string motherTongue, int contribute, int AnnualIncome)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[52];
                objParams[0] = new SqlParameter("@P_USERNO", userNO);
                objParams[1] = new SqlParameter("@P_GENDER", objus.Gender);
                objParams[2] = new SqlParameter("@P_FIRSTNAME", objus.FIRSTNAME);
                objParams[3] = new SqlParameter("@P_DOB", objus.DOB);
                objParams[4] = new SqlParameter("@P_RELIGION", objus.Religion);
                objParams[5] = new SqlParameter("@P_COMMUNITY", objus.Community);
                objParams[6] = new SqlParameter("@P_MOTHER_TONGUE", objus.Mother_Tongue);
                objParams[7] = new SqlParameter("@P_AADHAR", objus.Aadhar);
                objParams[8] = new SqlParameter("@P_NATIONALITY", objus.Nationality);
                objParams[9] = new SqlParameter("@P_FATHER_NAME", objus.Father_Name);
                objParams[10] = new SqlParameter("@P_FATHER_OCCUPATION", objus.Father_Occupation);
                objParams[11] = new SqlParameter("@P_FATHER_MOBILE", objus.Father_Mobile);
                objParams[12] = new SqlParameter("@P_MOTHER_NAME", objus.Mother_Name);
                objParams[13] = new SqlParameter("@P_MOTHER_OCCUPATION", objus.Mother_Occupation);
                objParams[14] = new SqlParameter("@P_MOTHER_MOBILE", objus.Mother_Mobile);
                objParams[15] = new SqlParameter("@P_ADDRESS_1", objus.Address_1);
                objParams[16] = new SqlParameter("@P_ADDRESS_2", objus.Address_2);
                //Photo
                if (objus.PHOTO == null)
                    objParams[17] = new SqlParameter("@P_PHOTO", DBNull.Value);
                else
                    objParams[17] = new SqlParameter("@P_PHOTO", objus.PHOTO);
                objParams[17].SqlDbType = SqlDbType.Image;

                objParams[18] = new SqlParameter("@P_ADDRESS_3", objus.Address_3);
                objParams[19] = new SqlParameter("@P_CITY", objus.City);
                objParams[20] = new SqlParameter("@P_STATE_NO", objus.State_No);
                objParams[21] = new SqlParameter("@P_PINCODE", objus.PinCode);
                //objParams[22] = new SqlParameter("@P_EDU_INFORMATION", objus.Edu_Info);
                //objParams[23] = new SqlParameter("@P_EXM_REG_12", objus.Exm_Reg_12);
                //objParams[24] = new SqlParameter("@P_SCHOOL_NAME", objus.School_name);
                objParams[22] = new SqlParameter("@P_MONTH_PASS_NO", objus.Month_No);
                objParams[23] = new SqlParameter("@P_MONTH_PASS", objus.Month);
                objParams[24] = new SqlParameter("@P_YEAR_PASS_NO", objus.Year_No);
                objParams[25] = new SqlParameter("@P_YEAR_PASS", objus.Year);
                objParams[26] = new SqlParameter("@P_PHOTO_PATH", objus.Photo_Path);
                objParams[27] = new SqlParameter("@P_RELIGION_OTHER", objus.Religion_Other);
                objParams[28] = new SqlParameter("@P_FOCCUPATION_OTHER", objus.FOccupation_Other);
                objParams[29] = new SqlParameter("@P_MOCCUPATION_OTHER", objus.MOccupation_Other);
                objParams[30] = new SqlParameter("@P_M_TONGUE_OTHER", motherTongue);
                objParams[31] = new SqlParameter("@P_DISTRICT", objus.District);
                objParams[32] = new SqlParameter("@P_ANNUAL_INCOME", AnnualIncome);
                //Added by Nikhil L. on 02/04/2022 for PG parameters.
                objParams[33] = new SqlParameter("@P_QUAL_DEGREENO_PG", objus.Qual_DegreeNO_PG);
                objParams[34] = new SqlParameter("@P_BRANCH_STUDY_PG", objus.Branch_Of_Study_PG);
                objParams[35] = new SqlParameter("@P_BRANCH_STUDY_OTHER_PG", objus.Branch_Of_Study_Others_PG);
                objParams[36] = new SqlParameter("@P_INSTITUTE_NAME_PG", objus.Institute_Name_PG);
                objParams[37] = new SqlParameter("@P_UNIVERSITY_NAME_PG", objus.University_Name_PG);
                objParams[38] = new SqlParameter("@P_INSTITUTE_LOCATION_PG", objus.Location_PG);
                objParams[39] = new SqlParameter("@P_INSTITUTE_STATE_PG", objus.Institute_State_PG);
                objParams[40] = new SqlParameter("@P_PER_CGPA_PG", objus.Per_CGPA_PG);
                objParams[41] = new SqlParameter("@P_MARKS_PER_CGPA_PG", objus.Marks_Per_CGPA_PG);
                objParams[42] = new SqlParameter("@P_SEMESTER_PG", objus.Semesterno_PG);
                objParams[43] = new SqlParameter("@P_YEAR_PG", objus.Year_PG);
                objParams[44] = new SqlParameter("@P_MODE_OF_STUDY_PG", objus.Mode_Of_Study_PG);
                objParams[45] = new SqlParameter("@P_NAME_OF_TEST_PG", objus.Name_Of_Test_PG);
                objParams[46] = new SqlParameter("@P_SCORE_PG", objus.Score_PG);
                //--------------------------------------------------------------------------------------- 
                objParams[47] = new SqlParameter("@P_INSTITUTE_ADV", objus.InstituteADV);
                objParams[48] = new SqlParameter("@P_INSTITUTE_ADV_OTHER", objus.InstituteADV_Other);
                objParams[49] = new SqlParameter("@P_CONTRIBUTE", contribute);
                objParams[50] = new SqlParameter("@P_QUAL_DEGREE_OTHERS_PG", objus.Qual_Degree_Others);
                objParams[51] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[51].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_NEW_USER_REGISTRATION", objParams, true);
                retStatus = Convert.ToInt32(ret);
                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
            }
        }

        /// <summary>
        /// Added by Nehal 16-06-2023
        /// </summary>
        /// <param name="JECRC"></param>
        /// <returns></returns>

        public DataSet GetApplicantUserRevokeList(int admBatch, int collegeid, int degreeNo, int branchno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { 
                new SqlParameter("@P_ADMBATCH",admBatch),            
                new SqlParameter("@P_COLLEGE_ID",collegeid),               
                new SqlParameter("@P_DEGREENO",degreeNo),   
                new SqlParameter("@P_BRANCHNO",branchno),           
                };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT_REVOKE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetApplicantUserRevokeList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        //added by nehal 16-06-2023
        public int UpdateRevokeStudStatus(int idno, string remark)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_STUDENT_REVOKE_LIST", objParams, true);
                retStatus = Convert.ToInt32(ret);
                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.UpdateRevokeStudStatus()-> " + ex.ToString());
            }
        }

        public DataSet GetApplicantUserNamePassword(string ApplicationNumber, int UaNo)
        {
            DataSet ds = new DataSet();

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNAME", ApplicationNumber);
                objParams[1] = new SqlParameter("@P_UA_NO", UaNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_APPLICATION_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FetchDataController.GetApplicantUserNamePassword() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
