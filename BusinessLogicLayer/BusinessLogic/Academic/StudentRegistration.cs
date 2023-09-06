using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{

    public class StudentRegistration
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="utype"></param>
        /// <param name="userDec"></param>
        /// <param name="ua_no"></param>
        /// <returns></returns>
        public DataSet GetStudentList(int idno, int utype, int userDec, int ua_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }
        public int InsertStudentRegistrationByAdmin(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_BY_ADMIN", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetStud_SVETStud_Fetch(int session, int college, int degree, int branch, int semester)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_COLLEGEID", college);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                //objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FETCH_SVETADM_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStud_CourseReg_ChangeSemester_Bulk-> " + ex.ToString());
            }
            return ds;
        }
        public int AddUpdateRevalPhotoCopyChallenegeRegisteration(StudentRegist objSR, int Status, int operation, string RECHECKORREASS, int checkreadpaper)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APPROVE_FLAG", Status);
                objParams[11] = new SqlParameter("@P_OPERATION_FLAG", operation);
                objParams[12] = new SqlParameter("@P_RECHECKORREASS", RECHECKORREASS);
                objParams[13] = new SqlParameter("@P_CHECKSTATUS", checkreadpaper);
                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdateRevalPhotoCopyChallenegeRegisteration(StudentRegist objSR, int Status, int operation, string RECHECKORREASS)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APPROVE_FLAG", Status);
                objParams[11] = new SqlParameter("@P_OPERATION_FLAG", operation);
                objParams[12] = new SqlParameter("@P_RECHECKORREASS", RECHECKORREASS);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatedropcourses(StudentRegist objSR, string reason)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[7] = new SqlParameter("@P_REASON", reason);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddUpdatewithdrwcourses(StudentRegist objSR, string reason)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[7] = new SqlParameter("@P_REASON", reason);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_WITHDRAW_COURSES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatedropcoursesstatus(StudentRegist objSR, int courseno, string ccode, int userno, string remark)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", ccode);
                objParams[7] = new SqlParameter("@P_USERNO", userno);
                objParams[8] = new SqlParameter("@P_REMARK", remark);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_UPDATE_DROUP_COURSES_STATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddUpdatewithdrawcoursesstatus(StudentRegist objSR, int courseno, string ccode, int userno, string remark)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", ccode);
                objParams[7] = new SqlParameter("@P_USERNO", userno);
                objParams[8] = new SqlParameter("@P_REMARK", remark);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_UPDATE_WITHDRAW_COURSES_STATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetDropCourseList(int idno, int semesterno, int sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_DROP_status", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }
        public DataSet GetWITHDRAWCourseList(int idno, int semesterno, int sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_withdraw_status", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }


        public DataSet GetStudInfoForChangeCoreSubject(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQL.ExecuteDataSetSP("PKG_SHOW_STUDENT_INFO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CoreSubjectChange.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }


        public int GetChangeCoreSubject(int idno, int core_SubID, int Uano)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_CORE_SUBID", core_SubID);
                objParams[2] = new SqlParameter("@P_UANO", Uano);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object ret = objSQL.ExecuteNonQuerySP("PKG_ACD_CHANGE_CORE_SUBJECT", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeCoreSubject.GetChangeCoreSubject-> " + ex.ToString());
            }
            return retStatus;

        }

        public int InsertStudentRegistrationForUG(StudentRegist objSR, string ddlvalue)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_SUB_TYPE",ddlvalue),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_FOR_UG", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }
        public int InsertStudentDetails(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_STUDNAME", objSR.STUDNAME),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ADMBATCH", objSR.BATCHNO),
                            new SqlParameter("@P_COLLEGEID", objSR.COLLEGEID),
                            new SqlParameter("@P_DEGREENO", objSR.DEGREENO),
                            new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                           // new SqlParameter("@P_CSUBID", objSR.CORSUBID),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_STUDENT_REGISTRATION_DETAILS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudentDetails-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetStudInfoForCourseRegi(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQL.ExecuteDataSetSP("PKG_SHOW_STUDENT_INFO_FOR_STUD_REGI", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetStudentCoursesForBacklogRegistration(int sessionNo, int idno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public bool CheckStudentForFaculty(int idno, int ua_no)
        {
            bool ret = false;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);

                object rt = objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_CHECK_STUD_FOR_FAC", objParams);
                if (rt == null || rt == "")
                    ret = false;
                else
                    ret = true;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CheckStudentForFaculty-> " + ex.ToString());
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="sessionno"></param>
        /// <param name="schemeno"></param>
        /// <param name="sectionno"></param>
        /// <param name="flagStatus"></param>
        /// <returns></returns>
        public DataTable GetSubjectDetailsById(int idno, int sessionno, int schemeno, int sectionno, int flagStatus)
        {
            DataTable dt = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_FLAG", flagStatus);

                dt = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_SUBJECTDETAILS", objParams).Tables[0];
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetSubjectDetailsById-> " + ex.ToString());
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTableReader GetStudentDetails(int idno)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                dtr = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDDETAILS", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetails-> " + ex.ToString());
            }
            return dtr;
        }

        //public int AddRegisteredSubjectsBulk(StudentRegist objSR, int Prev_status)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        //Add New Registered Subject Details
        //        objParams = new SqlParameter[13];

        //        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
        //        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
        //        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
        //        objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
        //        objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
        //        objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
        //        objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
        //        objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
        //        objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
        //        objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
        //        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
        //        objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
        //        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[12].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_BULK", objParams, true);
        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
        //    }

        //    return retStatus;

        //}


        public DataSet GetRegistTotalStudents(int sessionno, int schemeno, int rdbtn, int regstatus)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_RD", rdbtn);
                objParams[3] = new SqlParameter("@P_REGSTATUS", regstatus);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_REPORT_COURSE_STATS", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetRegistTotalStudents-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetCourseWiseStudents(int sessionno, int schemeno, int rdbtn, int subid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_RD", rdbtn);
                objParams[3] = new SqlParameter("@P_SUBID", subid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_REPORT_ROLLLIST", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetCourseWiseStudents-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GetFailedSubjects(string regno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@REGNO", regno);

                ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_FAILSUBJ", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetDetainedSubjects(string regno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@REGNO", regno);

                ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_DETSUBJ", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSubjectHistory(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@IDNO", idno);

                ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_PASSSUBJ", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }


        public bool CheckCourseRegistered(int idno, int sessionno, int schemeno, int courseno)
        {
            bool ret = false;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                objParams[3] = new SqlParameter("@P_IDNO", idno);

                object rt = objSH.ExecuteScalarSP("PKG_PREREGIST_SP_CHK_COURSREGISTERD", objParams);
                if (rt == null || Convert.ToInt32(rt) == 0)
                    ret = false;
                else
                    ret = true;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CheckCourseRegistered-> " + ex.ToString());
            }
            return ret;
        }

        public int AddAddlRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_UA_N0", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course);
                objParams[11] = new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED);
                objParams[12] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int CountCourseRegistered(int sessionno, int schemeno, int idno)
        {
            int count = 0;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_IDNO", idno);

                object rt = objSH.ExecuteScalarSP("PKG_PREREGIST_SP_COUNT_COURSREGISTERED", objParams);
                count = Convert.ToInt32(rt);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CountCourseRegistered-> " + ex.ToString());
            }
            return count;
        }


        public DataSet GetFailedSubjects(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_RET_FAIL_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRegisteredSubjects(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_RET_REGISTERED_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }



        public int ExamRegistationRegularSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add Fail Subject Details
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[4] = new SqlParameter("@P_COURSENO", objSR.COURSENO);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_EXAM_REGISTERED", objSR.EXAM_REGISTERED);
                objParams[7] = new SqlParameter("@P_S1IND", objSR.S1IND);
                objParams[8] = new SqlParameter("@P_S2IND", objSR.S2IND);
                objParams[9] = new SqlParameter("@P_S3IND", objSR.S3IND);
                objParams[10] = new SqlParameter("@P_S4IND", objSR.S4IND);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTATION_REGULAR_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        #region Pre-Admission
        public int AddRegisteredSubjectsThirdSem(StudentRegist objSR, int Prev_status, int seatno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[7] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[8] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[9] = new SqlParameter("@P_SEATNO", seatno);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_GDPOINT", objSR.GDPOINT);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_THIRD_SEM_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public string PreAdmissionRegistration(int sessionno, long idno, string studname, string fathername, string mothername,
           string lastname, string gender, DateTime dob, int mtongueno, int pcity, string ptelephonestd,
           string ptelephone, string pmobile, string emailid, int stateno, int caste, int categoryno, int nationalityno, int minority,
           int qualifyno, int ssc_maths, int ssc_maths_max, decimal ssc_maths_per, int ssc_total, int ssc_outofmarks,
           int mhcet_score, int mhcet_maths_score, int mhcet_physics_score,
           int hsc_maths, int hsc_maths_max, int hsc_chem, int hsc_chem_max, int hsc_phy, int hsc_phy_max, int hsc_pcm, int hsc_pcm_max,
           decimal per, int hsc_total, int hsc_outofmarks, int aieee_score, int aieee_rank, int aieee_rollno, string branch_pref, DateTime REG_ENTRY_DATE)
        {
            //CustomStatus cs = CustomStatus.Error;
            string retStatus = string.Empty;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_STUDNAME", studname),
                            new SqlParameter("@P_MOTHERNAME", mothername),
                            new SqlParameter("@P_FATHERNAME", fathername),
                            new SqlParameter("@P_LASTNAME", lastname),
                            new SqlParameter("@P_GENDER", gender),
                            new SqlParameter("@P_DOB", dob),
                            new SqlParameter("@P_MTONGUENO", mtongueno),
                            new SqlParameter("@P_PCITY", pcity),
                            new SqlParameter("@P_PTELEPHONESTD", ptelephonestd),
                            new SqlParameter("@P_PTELEPHONE", ptelephone),
                            new SqlParameter("@P_PMOBILE", pmobile),
                            new SqlParameter("@P_EMAILID", emailid),
                            new SqlParameter("@P_STATENO", stateno),
                            new SqlParameter("@P_CASTE", caste),
                            new SqlParameter("@P_CATEGORYNO", categoryno),
                            new SqlParameter("@P_NATIONALITYNO", nationalityno),
                            new SqlParameter("@P_MINORITY", minority),
                            new SqlParameter("@P_QUALIFYNO", qualifyno),
                            new SqlParameter("@P_SSC_MATHS", ssc_maths),
                            new SqlParameter("@P_SSC_MATHS_MAX", ssc_maths_max),
                            new SqlParameter("@P_SSC_MATHS_PER", ssc_maths_per),
                            new SqlParameter("@P_SSC_TOTAL", ssc_total),
                            new SqlParameter("@P_SSC_OUTOF", ssc_outofmarks),
                            new SqlParameter("@P_MHCET_SCORE", mhcet_score),
                            new SqlParameter("@P_MHCET_MATHS_SCORE", mhcet_maths_score),
                            new SqlParameter("@P_MHCET_PHYSICS_SCORE", mhcet_physics_score),
                            new SqlParameter("@P_HSC_MAT", hsc_maths),
                            new SqlParameter("@P_HSC_MAT_MAX", hsc_maths_max),
                            new SqlParameter("@P_HSC_CHE", hsc_chem),
                            new SqlParameter("@P_HSC_CHE_MAX", hsc_chem_max),
                            new SqlParameter("@P_HSC_PHY", hsc_phy),
                            new SqlParameter("@P_HSC_PHY_MAX", hsc_phy_max),
                            new SqlParameter("@P_HSC_PCM", hsc_pcm),
                            new SqlParameter("@P_HSC_PCM_MAX", hsc_pcm_max),
                            new SqlParameter("@P_PER", per),
                            new SqlParameter("@P_HSC_TOTAL", hsc_total),
                            new SqlParameter("@P_HSC_OUTOF", hsc_outofmarks),
                            new SqlParameter("@P_AIEEE_SCORE", aieee_score),
                            new SqlParameter("@P_AIEEE_RANK", aieee_rank),
                            new SqlParameter("@P_AIEEE_ROLLNO", aieee_rollno),
                            new SqlParameter("@P_BRANCH_PREF", branch_pref),
                            //new SqlParameter("@P_REG_ENTRY_DATE", REG_ENTRY_DATE),
                            new SqlParameter("@P_IDNO", idno),
                            
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.BigInt;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_REGISTRATION", sqlParams, true);

                //if (Convert.ToInt32(ret) == -99)
                //    cs = CustomStatus.TransactionFailed;
                //else
                //    cs = CustomStatus.RecordSaved;
                //cs = ret.ToString();
                retStatus = ret.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.PreAdmissionRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet GetMeritList(int sessionno, int aieee_mhcet, int scorefrom, int scoreto, int hsc_pcm, int minority, int combined, string fromdate, string todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_AIEEE_MHCET", aieee_mhcet);
                objParams[2] = new SqlParameter("@P_SCOREFROM", scorefrom);
                objParams[3] = new SqlParameter("@P_SCORETO ", scoreto);
                objParams[4] = new SqlParameter("@P_HSC_PCM", hsc_pcm);
                objParams[5] = new SqlParameter("@P_MINORITY", minority);
                objParams[6] = new SqlParameter("@P_COMBINED", combined);
                objParams[7] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[8] = new SqlParameter("@P_TODATE", todate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_MERIT_LIST", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetMeritList-> " + ex.ToString());
            }
            return ds;
        }

        public CustomStatus GenerateMeritList(int sessionno, int aieee_mhcet, int minority, int combined, string fromdate, string todate)
        {
            CustomStatus cs = CustomStatus.Others;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_AIEEE_MHCET", aieee_mhcet);
                objParams[2] = new SqlParameter("@P_MINORITY", minority);
                objParams[3] = new SqlParameter("@P_COMBINED", combined);
                objParams[4] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[5] = new SqlParameter("@P_TODATE", todate);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GENERATE_MERIT_LIST", objParams, false);
                if (ret != null)
                    cs = CustomStatus.RecordSaved;
                else
                    cs = CustomStatus.Error;

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenerateMeritList-> " + ex.ToString());
            }
            return cs;
        }

        public CustomStatus AllotBranch(int sessionno, long idno, int branchno, int roundno, int batchno, int paytypeno, int idtypeno)
        {
            CustomStatus cs = CustomStatus.Error;
            //string retStatus = string.Empty;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ROUNDNO", roundno),
                            new SqlParameter("@P_ADMBATCH",batchno),
                            new SqlParameter("@P_PTYPE",paytypeno),
                            new SqlParameter("@P_IDTYPE",idtypeno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.Int;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_ALLOT_BRANCH", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    cs = CustomStatus.TransactionFailed;
                else
                    cs = CustomStatus.RecordSaved;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.UpdateDocumentVerfication() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }

        public DataSet GetBranchPreferences(int sessionno, long idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_PROCESS_FORM_BRANCH_PREF", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetBranchPreferences-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

        public int UpdatePaymentCategory(string idno, string ptype, string semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_PTYPE", ptype);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_PAYMENTTYPE", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }
        // Generate Enrollment no.   

        public CustomStatus GenereateRegistrationNo(int admbatch, int clg, int degree, int branch)
        {
            CustomStatus cs = CustomStatus.Error;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                           
                            new SqlParameter("@P_ADMBATCH", admbatch),
                          new SqlParameter("@P_COLLEGEID", clg),
                          new SqlParameter("@P_DEGREENO", degree),
                          new SqlParameter("@P_BRANCHNO", branch)
                           // new SqlParameter("@P_CONT", cont)
                        };

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACD_BULK_REGNO_GENERATION", sqlParams, false);

                cs = CustomStatus.RecordSaved;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.GenereateRegistrationNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }

        public int CheckN4Rule(int idno, int sessionno, int semesterno, int degreeno)
        {
            int ret = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);


                ret = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_ACAD_STUD_SEM_DATA", objParams));

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetPreviousSemesterStud-> " + ex.ToString());
            }

            return ret;
        }

        //public int CheckN4Rule(int idno,int sessionno,int semesterno)
        //{
        //    int ret = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

        //        SqlParameter[] objParams = new SqlParameter[3];
        //        objParams[0] = new SqlParameter("@P_IDNO", idno);
        //        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
        //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);


        //        ret = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_ACAD_STUD_SEM_DATA", objParams));

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetPreviousSemesterStud-> " + ex.ToString());
        //    }

        //    return ret;
        //}


        /// <summary>
        /// Modified by S.Patil - 13012020
        /// </summary>
        /// <returns></returns>
        //public int GenereateEnrollmentNo(int admbatch, int clg, int degree, int branch, int idtype, int flag)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[7];
        //        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
        //        objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
        //        objParams[2] = new SqlParameter("@P_DEGREENO", degree);
        //        objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
        //        objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
        //        objParams[5] = new SqlParameter("@P_FLAG", flag);
        //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[6].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ENROLLMENT_GENERATION", objParams, false);

        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
        //    }

        //    return retStatus;

        //}

        /// <summary>
        /// Modified by S.Patil - 13012020
        /// </summary>
        /// <returns></returns>
        public int GenereateEnrollmentNo(int admbatch, int clg, int degree, int branch, int idtype, int flag)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                objParams[5] = new SqlParameter("@P_FLAG", flag);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ENROLLMENT_GENERATION", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int UpdateSemesterPromotionNo(string idno, int semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_PROMOTION_SEMESTRENO", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int UpdateSemesterProAddPromotionNo(string idno, int semesterno, int sessionno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                //objParams[3] = new SqlParameter("@P_YEAR_OLD", oldyear);
                //objParams[4] = new SqlParameter("@P_SEMESTER_OLD", oldsem);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_PROMOTION_SEMESTRENO_PROV", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int UpdateBranchCategory(string idno, string branchno, string admcategoryno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_ADMCATEGORYNO", admcategoryno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_BRANCHCAT", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }


        //public int AddExamRegisteredSubjects(StudentRegist objSR)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        //Add New eXAM Registered Subject Details
        //        objParams = new SqlParameter[9];

        //        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
        //        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
        //        objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
        //        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
        //        objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
        //        objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
        //        //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
        //        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
        //        objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
        //        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[8].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
        //    }

        //    return retStatus;

        //}


        public int AddExamRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddRevalautionRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddRevalautionMarkEntry(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add revlauation entry
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNOS", objSR.IDNOS);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENO);
                objParams[4] = new SqlParameter("@P_IPADDRESS_V1", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IPADDRESS_V2", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_UA_NO_V1", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_UA_NO_V2", objSR.UA_NO);
                //valuer1 marks
                if (objSR.VALUER1_MKS == null)
                    objParams[9] = new SqlParameter("@P_VALUER1_MKS", DBNull.Value);
                else
                    objParams[9] = new SqlParameter("@P_VALUER1_MKS", objSR.VALUER1_MKS);
                //valuer2 marks
                if (objSR.VALUER2_MKS == null)
                    objParams[10] = new SqlParameter("@P_VALUER2_MKS", DBNull.Value);
                else
                    objParams[10] = new SqlParameter("@P_VALUER2_MKS", objSR.VALUER2_MKS);
                ////valuer1 marks
                //if (objSR.VALUER1_MKS == null)
                //    objParams[11] = new SqlParameter("@P_MARK_DIFFS", DBNull.Value);
                //else
                //    objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.VALUER1_MKS);
                //marks diff
                if (objSR.MARKDIFFS == null)
                    objParams[11] = new SqlParameter("@P_MARK_DIFFS", DBNull.Value);
                else
                    objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.MARKDIFFS);

                // new marks
                if (objSR.NEWMARKS == null)
                    objParams[12] = new SqlParameter("@P_NEW_MARKS", DBNull.Value);
                else
                    objParams[12] = new SqlParameter("@P_NEW_MARKS", objSR.NEWMARKS);


                //objParams[9] = new SqlParameter("@P_VALUER1_MKS", objSR.VALUER1_MKS);
                //objParams[10] = new SqlParameter("@P_VALUER2_MKS", objSR.VALUER2_MKS);
                //objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.MARKDIFFS);
                //objParams[12] = new SqlParameter("@P_NEW_MARKS", objSR.NEWMARKS);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_REVALUATION_MARK_ENTRY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddAddlRegisteredSubjectsPhd(StudentRegist objSR, string exem, string type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_CREDITS", objSR.CREDITS);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_EXEM", exem);
                objParams[11] = new SqlParameter("@P_TYPE", type);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_PHD", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }



        //for branch change
        public CustomStatus GenereateRegistrationNoBranch(int degreeno, int branchno, int admbatch, int idno)
        {
            CustomStatus cs = CustomStatus.Error;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_IDNO", idno),
                        };

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_BULK_REGNO_GENERATION_BRCHANGE_NEW", sqlParams, false);

                cs = CustomStatus.RecordSaved;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.GenereateRegistrationNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }


        public int AddAddCoursesForPhd(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[1] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_COURSE_SP_INS_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int GenereateSingleEnrollmentNo(int admbatch, int semesterno, int idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_SEMESTER", semesterno);
                objParams[2] = new SqlParameter("@P_IDNO", idno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SINGLE_ENROLLMENT_GENERATION", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int InsertBranchChange(StudentRegist objStudent)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        
                        {
                         new SqlParameter("@P_IDNO", objStudent.IDNO),
                         new SqlParameter("@P_OLD_BRANCH", objStudent.BRANCHNO),
                         new SqlParameter("@P_BR_PREF", objStudent.BRANCH_REF),
                         new SqlParameter("@P_UA_IDNO", objStudent.UA_NO),
                         new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS),
                         new SqlParameter("@P_COLLEGE_CODE", objStudent.COLLEGE_CODE),
                         new SqlParameter("@P_ABID", status)
                    };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_APPLY_BRANCH_CHANGE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.InsertBranchChange-> " + ex.ToString());
            }

            return status;
        }

        public int AddUpdElectiveSubject(StudentRegist objSR)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        
                        {
                         new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                         new SqlParameter("@P_IDNO", objSR.IDNO),
                         new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                         new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                         new SqlParameter("@P_COURSENOS", objSR.COURSENO),
                         new SqlParameter("@P_SELECTCOURSENOS", objSR.SELECT_COURSE),
                         new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                         new SqlParameter("@P_CREDITS", objSR.CREDITS),
                         new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                         new SqlParameter("@P_UA_NO", objSR.UA_NO),
                         new SqlParameter("@P_OUT", retStatus)
                    };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_INS_UPD_ELECTIVE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddUpdElectiveSubject-> " + ex.ToString());
            }

            return retStatus;


            //catch (Exception ex)
            //{
            //    retStatus = Convert.ToInt32(CustomStatus.Error);
            //    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddUpdElectiveSubject-> " + ex.ToString());
            //}

            //return retStatus;

        }
        // INSERT CONVOCATION CERTIFICATE//27-FEB-2014//UMESH
        public int AddConvocation(StudentRegist objSR, string studnames, string degree, string branch, string regulation_date, string convocation_date, string ipaddress, string deptname, int certno, int Conv_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];
                //idnos new memeber add in studentregistration.cs page.(27/01/2012)
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNOS);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_STUDENTNAME", studnames);
                objParams[3] = new SqlParameter("@P_DEGREE", degree);
                objParams[4] = new SqlParameter("@P_BRANCH", branch);
                objParams[5] = new SqlParameter("@P_REGULATION_DATE", regulation_date);
                objParams[6] = new SqlParameter("@P_CONVOCATION_DATE", convocation_date);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[9] = new SqlParameter("@P_DEPTNAME", deptname);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CERTNO", certno);
                objParams[12] = new SqlParameter("@P_CONV_NO", Conv_no);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONVOCATION_CERTIFICATE_INSERT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }
        public DataSet GetStudentCoursesForBacklogRegistrationBACK(int sessionNo, int idno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_REGIST_BACK", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }
        public int InsertStudentRegistrationByAdminBack(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_BY_ADMIN_BACK", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Modified By Rishabh on 30/12/2021 - Added Organization id
        /// </summary>
        /// <param name="objSR"></param>
        /// <param name="Prev_status"></param>
        /// <returns></returns>
        public int AddRegisteredSubjectsBulk(StudentRegist objSR, int Prev_status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                objParams[12] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_BULK", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }
        public int AddRegisteredSubjectsBulkBACK(StudentRegist objSR, int Prev_status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_BULK_BACK", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }
        //Added by Mr.Manish Walde on date 05/04/2016
        public DataSet GetRevalRegStudentList(int idno, int utype, int userDec, int ua_no, int sessionno, int semesterno, int select, int User_for)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[6] = new SqlParameter("@P_OPERATION_FLAG ", select);
                objParams[7] = new SqlParameter("@P_USE_FOR", User_for);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_REVALUATION", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }
        //Added by Mr.Manish Walde on date 05/04/2016
        public int AddUpdateRevalRegisteration(StudentRegist objSR, int Status, int operation, string App_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_Ccode", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APPROVE_FLAG", Status);
                objParams[11] = new SqlParameter("@P_OPERATION_FLAG", operation);
                objParams[12] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by reena
        public DataSet GetStudentCoursesForRegularRegistration(int idno, int sessionNo, int schemeNo, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[4] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_REG_SUBJECT_FOR_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
            }
            return ds;
        }
        // added by sandeep
        public DataSet GetStud_CourseReg_ChangeSemester_Bulk(int session, int college, int degree, int branch, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_COLLEGEID", college);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semester);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEM_PROMOTION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStud_CourseReg_ChangeSemester_Bulk-> " + ex.ToString());
            }
            return ds;
        }

        public int AddRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            //new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
                            new SqlParameter("@P_RE_APPEARED",objSR.Re_Appeared),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_UA_N0", objSR.UA_NO),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }


        public DataSet GetStudentCoursesForAuditRegistration(int idno, int sessionNo, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_AUDIT_SUBJECT_FOR_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentCoursesForReAppearedCourseRegistration(int sessionNo, int idno, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_REAPPEARED_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }


        public int UdpateRegistrationByHOD(int sessionno, string idnos, int cRegIdno, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_CREG_IDNO", cRegIdno),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_UPD_REGISTRATION_BY_HOD", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }

            return retStatus;

        }

        public DataSet GetCourseRegStudentList(int idno, int utype, int userDec, int ua_no, int sessionno, int opt_Flag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_OPT_FLAG", opt_Flag);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS_NEW", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[25-10-2016]
        public DataSet Get_Operator_Alloted_Details(int SESSIONNO, int SEMESTERNO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_OPERATOR_ALLOTED_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateOperatorAllotment_ForEndSem(int SESSIONNO, int SCHEMENO, int SEMESTERNO, int SUBID, string COURSENO, int VALUER_UA_NO, int TH_CUM_PR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SESSIONNO", SESSIONNO),
                        new SqlParameter("@P_SCHEMENO", SCHEMENO),
                        new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                        new SqlParameter("@P_SUBID", SUBID),
                        new SqlParameter("@P_COURSENO", COURSENO),
                        new SqlParameter("@P_VALUER_UA_NO", VALUER_UA_NO),
                        new SqlParameter("@P_TH_CUM_PR", TH_CUM_PR),
                        new SqlParameter("@P_OUT", SqlDbType.Int)

                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UP_OPERATOR_ALLOTMENT_FOR_ENDSEM", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
            }
            return retStatus;

        }

        /********************************ExamRegistrationForStudentLogin[05-10-2016]***********************************/

        //Method for get student details for Exam Registration--student login
        public DataSet GetStudentInfoById_ForExamRegistration(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_INFO_FOR_EXAM_REGISTRATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        //[06-10-2016]
        public DataSet GetCurrentSemesterCourseList_ForExamRegistration(int IDNO, int SESSIONNO, int SEMESTERNO, int DEGREENO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_CURRENT_SEMESTER_COURSELIST", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        //[15-10-2016]
        public DataSet GetBackLogSemesterCourseList_ForExamRegistration(int IDNO, int SESSIONNO, int SEMESTERNO, int DEGREENO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_BACKLOG_SEMESTER_COURSELIST", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //[14-11-2016]
        public DataSet GetCourseList_ForLateFees(int IDNO, int SESSIONNO, int SEMESTERNO, int DEGREENO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_COURSELIST_FOR_LATE_FEES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //[30-11-2016]
        ////public DataSet GetLateFees(DateTime CURR_DATE, int DEGREENO)
        ////{
        ////    DataSet ds = null;
        ////    try
        ////    {
        ////        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
        ////        SqlParameter[] sqlParams = new SqlParameter[] 
        ////{ 
        ////    new SqlParameter("@P_CURR_DATE", CURR_DATE),
        ////    new SqlParameter("@P_DEGREENO", DEGREENO)

        ////};
        ////        ds = objDataAccess.ExecuteDataSetSP("GET_LATE_FEES_DETAILS_FOR_EXAMREGISTRATION", sqlParams);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
        ////    }
        ////    return ds;
        ////}

        public DataSet GetLateFees(DateTime CURR_DATE, int DEGREENO, int SESSIONNO, string RECEIPT_CODE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                    { 
                    new SqlParameter("@P_CURR_DATE", CURR_DATE),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_RECEIPT_CODE", RECEIPT_CODE)
                    
                };
                ds = objDataAccess.ExecuteDataSetSP("GET_LATE_FEES_DETAILS_FOR_EXAMREGISTRATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddExamRegisteredSubjectsbackmake(StudentRegist objSR, string ExistCourses, string ddlValue)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details

                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_CANCOURSENOS", ExistCourses);
                objParams[10] = new SqlParameter("@P_SUB_TYPE", ddlValue);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_makeup", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        //Backlog Registration
        public int AddExamRegisteredSubjectsback(StudentRegist objSR, string ExistCourses, string ddlValue, int ExamReg)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_CANCOURSENOS", ExistCourses);
                objParams[10] = new SqlParameter("@P_SUB_TYPE", ddlValue);
                objParams[11] = new SqlParameter("@P_EXAM_REGISTERED", ExamReg);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        ////Backlog Exam Registration Status -04/02/2019
        public int UpdateBackLogExamRegisteredStatus(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_APPROVEUANO", objSR.UA_NO);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_UPDATE_BACKLOG_EXAM_COURSEREGSTATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }



        // Additional Backlog Registration -30/01/2019
        public int AddAdditionalExamRegisteredSubjectsback(StudentRegist objSR, string ExistCourses, string ddlValue)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_CANCOURSENOS", ExistCourses);
                objParams[10] = new SqlParameter("@P_SUB_TYPE", ddlValue);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_ADDITIONAL_BACKLOG_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentFailExamSubjectsmake(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_9242017_105_makeup", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }
        public DataSet GetRevalRegisteredStudentLists(int SESSIONNO, int COLLEGEID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_SESSIONNO",SESSIONNO),
                     new SqlParameter("@P_COLLEGEID", COLLEGEID),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_TYPE", type)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_REEVALUATION_AND_SCRUTINY_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetRevalRegisteredStudentLists() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetRevalRegisteredStudentLists_CourseWise(int SESSIONNO, int COLLEGEID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int COURSENO, int type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_SESSIONNO",SESSIONNO),
                     new SqlParameter("@P_COLLEGEID", COLLEGEID),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_COURSENO", COURSENO),
                    new SqlParameter("@P_TYPE", type)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_REEVALUATION_DETAILS_COURSEWISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetRevalRegisteredStudentLists() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentFailExamSubjects(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_9242017_105", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }



        public DataSet GetStudentDropSubjects(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_DROP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        //Added Mahesh Dt.19/01/2019
        public DataSet GetStudentBacklogExamSubjects(int idno, string BacklogSem, int Sessionno, int CurrentSem)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_BACKLOGSEM", BacklogSem);
                objParams[3] = new SqlParameter("@P_CURRENTSEM", CurrentSem);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_BACKLOG_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }


        //Get Additional Student Backlog list Added Mahesh Dt.19/01/2019
        public DataSet GetAdditionalStudentBacklogExamSubjects(int idno, string BacklogSem, int Sessionno, int CurrentSem)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_BACKLOGSEM", BacklogSem);
                objParams[3] = new SqlParameter("@P_CURRENTSEM", CurrentSem);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ADDITIONAL_STUDENT_BACKLOG_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAdditionalStudentBacklogExamSubjects->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentwithdrawSubjects(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_WITHDRAW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentForEnrollGeneration(int admbatch, int collegeid, int degreeno, int branchno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", collegeid);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_FLAG", 1);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_BULK_ENROLLMENT_GENERATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentForEnrollGeneration->" + ex.ToString());
            }
            return ds;
        }
        public int AddRegisteredSubjectsBulkElective(StudentRegist objSR, int Prev_status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                objParams[12] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_ELECTIVE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }


        public DataSet GetStudentCourseRegistrationSubject(int SESSIONNO, int IDNO, int SEMESTERNO, int SCHEMENO, int COMMANDTYPE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[4] = new SqlParameter("@P_COMMANDTYPE", COMMANDTYPE);

                ds = objSQL.ExecuteDataSetSP("PKG_COURSEREGISTRATION_SP_GET_OFFERED_SUBJECTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCourseRegistrationSubject-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet Get_Student_Registered_Course(int SESSIONNO, int IDNO, int SEMESTERNO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                ds = objSQL.ExecuteDataSetSP("PKG_ACD_STUD_REGISTERED_CRS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCourseRegistrationSubject-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GetTotalCreditsCount(int DEGREENO, int BRANCHNO, int SCHEMENO, int SEMESTERNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);

                ds = objSQL.ExecuteDataSetSP("PKG_ACAD_SCHEMEWISE_TOT_CREDITS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetTotalCreditsCount-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ROHIT KUMAR TIWARI ON 13-APRIL-2019
        /// </summary>
        /// <param name="objStudent"></param>
        /// <param name="session"></param>
        /// <param name="OBJSR"></param>
        /// <param name="idnos"></param>
        /// <returns></returns>
        public int AddExamRegisteredSubjectsNew(Student_Acd objStudent, int session, StudentRegist OBJSR, string idnos)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New student
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                objParams[1] = new SqlParameter("@P_DEGREENO", objStudent.DegreeNo);
                objParams[2] = new SqlParameter("@P_BRANCHNO", objStudent.BranchNo);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objStudent.SemesterNo);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objStudent.IpAddress);
                objParams[6] = new SqlParameter("@P_IDNOS", idnos);
                objParams[7] = new SqlParameter("@P_COURSENOS", OBJSR.COURSENOS);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.NVarChar, 4000);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_SINGLE_STUD", objParams, true);

                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddStudBacklogCourses-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 23-04-2019
        /// </summary>
        public DataSet Get_Courses_LateFine(DateTime penalty_date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PENALTY_DATE", penalty_date);
                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_LATE_FINE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        /////// <summary>
        /////// ADDED BY: M. REHBAR SHEIKH ON 23-04-2019
        /////// </summary>
        ////public DataSet GetStudentCoursesForBacklogRegistration1(int sessionNo, int idno, int semesterno, int pageFlag)
        ////{
        ////    DataSet ds = null;
        ////    try
        ////    {
        ////        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
        ////        SqlParameter[] objParams = null;
        ////        objParams = new SqlParameter[4];
        ////        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        ////        objParams[1] = new SqlParameter("@P_IDNO", idno);
        ////        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
        ////        objParams[3] = new SqlParameter("@P_PAGEFLAG", pageFlag);

        ////        ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_REGIST_FOR_EXM_REGISTRATION", objParams);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
        ////    }
        ////    return ds;
        ////}

        /////// <summary>
        /////// ADDED BY: M. REHBAR SHEIKH ON 23-04-2019
        /////// </summary>
        ////public DataSet GetStudentCoursesForRegularRegistration1(int idno, int sessionNo, int schemeNo, int semesterno, int pageFlag)
        ////{
        ////    DataSet ds = null;
        ////    try
        ////    {
        ////        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
        ////        SqlParameter[] objParams = null;
        ////        objParams = new SqlParameter[5];
        ////        objParams[0] = new SqlParameter("@P_IDNO", idno);
        ////        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
        ////        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeNo);
        ////        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
        ////        objParams[4] = new SqlParameter("@P_PAGEFLAG", pageFlag);

        ////        ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_REG_SUBJECT_FOR_REGIST", objParams);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
        ////    }
        ////    return ds;
        ////}

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 23-04-2019
        /// </summary>
        public DataSet GetStudentCoursesForReAppearedCourseRegistration1(int sessionNo, int idno, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_REAPPEARED_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 23-04-2019
        /// </summary>
        public DataSet GetStudentCoursesForAuditRegistration1(int idno, int sessionNo, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_AUDIT_SUBJECT_FOR_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 23-04-2019
        /// </summary>
        public DataSet GetCourseRegStudentList1(int idno, int utype, int userDec, int ua_no, int sessionno, int opt_Flag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_OPT_FLAG", opt_Flag);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS_FOR_STUD_EXAM", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        /// <summary>
        /// UPDATED BY: M. REHBAR SHEIKH ON 23-04-2019
        /// </summary>
        ////public int AddExamRegiSubjects1(StudentRegist objSR)
        ////{
        ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
        ////    try
        ////    {
        ////        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        ////        SqlParameter[] objParams = new SqlParameter[]
        ////                {
        ////                    new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
        ////                    new SqlParameter("@P_IDNO", objSR.IDNO),
        ////                    new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
        ////                    new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
        ////                    new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
        ////                    new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
        ////                    //new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
        ////                    //new SqlParameter("@P_RE_APPEARED",objSR.Re_Appeared),
        ////                    new SqlParameter("@P_REGNO", objSR.REGNO),
        ////                    new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
        ////                    new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
        ////                    new SqlParameter("@P_UA_N0", objSR.UA_NO),
        ////                    new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
        ////                    new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
        ////                    //ADDDED BY MR.MANISH WALDE ON DATE 23-01-2016
        ////                    //new SqlParameter("@P_COMMAN_FEE", objSR.CommanFee),
        ////                    //new SqlParameter("@P_COURSE_FEE", objSR.CourseFee),
        ////                    //new SqlParameter("@P_LATE_FINE", objSR.LateFine),
        ////                    //new SqlParameter("@P_TOTAL_AMT", objSR.TotalFee),
        ////                    //new SqlParameter("@P_RECEIPT_FLAG", objSR.ReceiptFlag),
        ////                    new SqlParameter("@P_OUT", SqlDbType.Int)
        ////                };
        ////        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

        ////        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_EXAM_REGIST_SUBJECTS", objParams, true);
        ////        if (Convert.ToInt32(ret) == -99)
        ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        ////        else
        ////            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        retStatus = Convert.ToInt32(CustomStatus.Error);
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
        ////    }

        ////    return retStatus;

        ////}

        /// <summary>
        /// Added by Rita M. on date 12/10/2019
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns></returns>
        //public int AddExamRegisteredBacklaog(StudentRegist objSR)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        //Add New eXAM Registered Subject Details
        //        objParams = new SqlParameter[11];

        //        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
        //        objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
        //        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
        //        objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
        //        objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
        //        objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
        //        //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
        //        objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
        //        objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
        //        objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
        //        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
        //        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[10].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);
        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
        //    }

        //    return retStatus;

        //}

        /// <summary>
        ///Added by S.Patilv - 16102019
        /// </summary>
        public int AddExamRegiSubjects_Bulk(StudentRegist objSR, string ids)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", ids),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_UA_N0", objSR.UA_NO),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_REGIST_SUBJECTS_BULK", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }


        /// <summary>
        /// ADDED BY: Swapnil - 27032020
        /// </summary>
        public DataSet GetStudentStatusDetails(int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ADMBATCH", batchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_FETCH_USER_DETAILS_EXCEL3_ONLINE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        /// <summary>
        /// Added by Swapnil - for online Admission User Creation dated on 30032020
        /// </summary>
        /// <param name="objNS"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public int Insert_Update_New_Student(Student objNS, string ipAddress)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[15];

                objParams[0] = new SqlParameter("@P_STUDNAME", objNS.StudName);
                objParams[1] = new SqlParameter("@P_MOBILENO", objNS.StudentMobile);
                objParams[2] = new SqlParameter("@P_EMAIL", objNS.EmailID);
                objParams[3] = new SqlParameter("@P_PASSWORD", objNS.Password);
                objParams[4] = new SqlParameter("@P_R_IPADDRESS", ipAddress);
                objParams[5] = new SqlParameter("@P_DEGREENO", objNS.DegreeNo);
                objParams[6] = new SqlParameter("@P_BRANCHNO", objNS.BranchNo);
                objParams[7] = new SqlParameter("@P_CITYNO", objNS.City);
                objParams[8] = new SqlParameter("@P_STATENO", objNS.PState);
                objParams[9] = new SqlParameter("@P_FEES", objNS.Fees);
                objParams[10] = new SqlParameter("@P_UGPGOT", objNS.Ugpgot);
                objParams[11] = new SqlParameter("@P_ADMTYPE", objNS.AdmType);
                objParams[12] = new SqlParameter("@P_LOGSTATUS", objNS.Lock);
                objParams[13] = new SqlParameter("@P_CDBNO", objNS.Cdbno);

                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_STUDENT_RECORD", objParams, true);
                if (ret != null)
                {
                    if (ret.ToString() == "-99")
                    {
                        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    else if (ret.ToString() == "1")
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                }


            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.updateDetention->" + ex.ToString());
            }
            return retStatus;

        }

        // Added by Safal manoj gupta on 20012021 

        // Added by Safal manoj gupta on 20012021 
        // Modified by Rishabh B. on 11/03/2022 - Added condition & perct.
        public DataSet GetRegisteredStudentList(int sessionno, int schemeno, int semesterno, int uano, int courseno, int value, string Condi, decimal per)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_VALUE", value);
                objParams[5] = new SqlParameter("@P_UA_NO", uano);
                objParams[6] = new SqlParameter("@P_CONDITION", Condi);
                objParams[7] = new SqlParameter("@P_PERCENTAGE", per);


                ds = objSQL.ExecuteDataSetSP("PKG_GET_STUDENT_REGISTERED_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetRegisteredStudentList-> " + ex.ToString());
            }
            return ds;

        }

        // Added by Safal manoj gupta on 23012021

        public int UpdateCancelDetend(StudentRegist objStud)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objStud.IDNO);
                objParams[3] = new SqlParameter("@P_UA_NO_DETCAN", objStud.UA_NO);
                objParams[2] = new SqlParameter("@P_IPADDRESS_FINAL", objStud.IPADDRESS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objStud.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objStud.COLLEGE_CODE);
                objParams[6] = new SqlParameter("@P_COURSENOS", objStud.COURSENOS);


                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CANCEL_FINAL_DETEND_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateCancelDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }



        // Added by Safal manoj gupta on 20012021

        public int AddDetendinfo(StudentRegist objSR, int orgid, int detain_type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[11];
                //Add Fail Subject Details
                //objParams = 

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNOS);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[4] = new SqlParameter("@P_PROV_DETEND", objSR.PROV_DETEND);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_ORGID", orgid);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_DETAIN_TYPE", detain_type);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREG_SP_INSERT_DETEND_STUDENT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.AddDetendinfo-> " + ex.ToString());
            }

            return retStatus;

        }




        // Added by Safal manoj gupta on 20012021
        public DataSet GetProvDetendInfo(int sessionno, int schemeno, int semesterno, int courseno, int value)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_VALUE", value);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_SP_RET_DETEND_INFO", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetProvDetendInfo()-> " + ex.ToString());
            }

            return ds;
        }


        // Added by Safal manoj gupta on 20012021 for final detend list
        public int UpdateFinalDetend(StudentRegist objStud)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[8];

                //update

                objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objStud.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SEMESTERNO);
                // objParams[3] = new SqlParameter("@P_COURSENOS", objStud.COURSENO);
                objParams[3] = new SqlParameter("@P_IDNO", objStud.IDNOS);
                objParams[4] = new SqlParameter("@P_FINAL_DETEND", objStud.FINAL_DETEND);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objStud.IPADDRESS);//IP FINAL

                objParams[6] = new SqlParameter("@P_UA_NO", objStud.UA_NO);//New Entity
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStud.COLLEGE_CODE);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FINAL_DETEND_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateFinalDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        // Added by Safal manoj gupta on 23012021
        public DataSet GetFinalDetendInfo(int sessionNo, int semesterno, int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_IDNO", idno);

                ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_FINAL_DETEND_INFO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetFinalDetendInfo-> " + ex.ToString());
            }
            return ds;
        }



        // Added by Safal manoj gupta on 23012021


        public DataSet Get_Cancel_Detent_StudentList(int sessionno, int semesterno, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_COLLEGE_ID", college_id),
                    // new SqlParameter("@P_COURSENO", courseno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_CANCEL_DETENTION_STUDENT_LIST", sqlParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.StudentController.Get_Cancel_Detent_StudentList() --> " + ex.Message);
            }
            return ds;
        }

        //Code added by Safal Gupta on 03022021
        public DataTableReader GetStudentDetailsNew(int idno)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                dtr = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDDETAILS_NEW", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetailsNew-> " + ex.ToString());
            }
            return dtr;
        }

        public DataTableReader GetStudentDetailsFinalApprove(int idno)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                dtr = objSH.ExecuteDataSetSP("PKG_ADMCAN_RET_STUDDETAILS", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetailsNew-> " + ex.ToString());
            }
            return dtr;
        }

        //public int AddExamRegiSubjects1(StudentRegist objSR)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = new SqlParameter[]
        //                {
        //                    new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
        //                    new SqlParameter("@P_IDNO", objSR.IDNO),
        //                    new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
        //                    new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
        //                    new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
        //                    new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
        //                    //new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
        //                    //new SqlParameter("@P_RE_APPEARED",objSR.Re_Appeared),
        //                    new SqlParameter("@P_REGNO", objSR.REGNO),
        //                    new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
        //                    new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
        //                    new SqlParameter("@P_UA_N0", objSR.UA_NO),
        //                    new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
        //                    new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
        //                    //ADDDED BY MR.MANISH WALDE ON DATE 23-01-2016
        //                    //new SqlParameter("@P_COMMAN_FEE", objSR.CommanFee),
        //                    //new SqlParameter("@P_COURSE_FEE", objSR.CourseFee),
        //                    //new SqlParameter("@P_LATE_FINE", objSR.LateFine),
        //                    new SqlParameter("@P_TOTAL_AMT", objSR.TotalFee),
        //                    new SqlParameter("@P_RECEIPT_FLAG", objSR.ReceiptFlag),
        //                    new SqlParameter("@P_OUT", SqlDbType.Int)
        //                };
        //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_EXAM_REGIST_SUBJECTS", objParams, true);
        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
        //    }

        //    return retStatus;

        //}

        /// <summary>
        /// UPDATED BY: M. REHBAR SHEIKH ON 23-04-2019
        /// </summary>
        public int AddExamRegiSubjects1(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            //new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
                            //new SqlParameter("@P_RE_APPEARED",objSR.Re_Appeared),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_UA_N0", objSR.UA_NO),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
                            //ADDDED BY MR.MANISH WALDE ON DATE 23-01-2016
                            //new SqlParameter("@P_COMMAN_FEE", objSR.CommanFee),
                            //new SqlParameter("@P_COURSE_FEE", objSR.CourseFee),
                            //new SqlParameter("@P_LATE_FINE", objSR.LateFine),
                            //new SqlParameter("@P_TOTAL_AMT", objSR.TotalFee),
                            //new SqlParameter("@P_RECEIPT_FLAG", objSR.ReceiptFlag),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_EXAM_REGIST_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;



        }

        public DataSet GetStudentCoursesForRegularRegistration1(int idno, int sessionNo, int schemeNo, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[4] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_REG_SUBJECT_FOR_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentCoursesForBacklogRegistration1(int sessionNo, int idno, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_REGIST_FOR_EXM_REGISTRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY DILEEP ON 19.03.2021
        /// FOR BACKLOG EXAM FORM FILLUP
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="idno"></param>
        /// <param name="semesterno"></param>
        /// <param name="pageFlag"></param>
        /// <returns></returns>
        public DataSet GetStudentCoursesForBacklogExamFormFillup(int sessionNo, int idno, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PKG_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_EXAM_FORM_FILLUP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForBacklogExamFormFillup-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY DILEEP ON 19.03.2021
        /// FOR BACKLOG EXAM FORM FILLUP
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="idno"></param>
        /// <param name="semesterno"></param>
        /// <param name="pageFlag"></param>
        /// <returns></returns>
        public int AddBacklogExamFormFillupCourses(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            //new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
                            //new SqlParameter("@P_RE_APPEARED",objSR.Re_Appeared),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_UA_N0", objSR.UA_NO),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
                            //ADDDED BY MR.MANISH WALDE ON DATE 23-01-2016
                            //new SqlParameter("@P_COMMAN_FEE", objSR.CommanFee),
                            //new SqlParameter("@P_COURSE_FEE", objSR.CourseFee),
                            //new SqlParameter("@P_LATE_FINE", objSR.LateFine),
                            //new SqlParameter("@P_TOTAL_AMT", objSR.TotalFee),
                            //new SqlParameter("@P_RECEIPT_FLAG", objSR.ReceiptFlag),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;



                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_BACKLOG_EXAM_FORM_FILLUP_COURSES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;

        }

        /// <summary>
        /// ADDED BY: SWAPNIL P ON 24-06-2021
        /// </summary>
        public DataSet Get_Exam_Mark_Status(int idno, string coursenos)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_COURSENOS", coursenos);
                ds = objSQL.ExecuteDataSetSP("PKG_GET_COURSE_MARKENTRY_STATUS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }
        //ADDED BY PRAFULL 30102021 

        public int GenereateEnrollmentNo(int admbatch, int clg, int degree, int branch, int idtype, int year, int sort)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                objParams[5] = new SqlParameter("@P_YEAR", year);
                // objParams[6] = new SqlParameter("@P_FLAG", flag);
                objParams[6] = new SqlParameter("@P_SORT", sort);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ENROLLMENT_GENERATION_NEW", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int GenereateRollNo(int admbatch, int clg, int degree, int branch, int idtype, int semester, int section, int sort)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SORT", sort);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ROLLNO_GENERATION_NEW", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }
        public int AddCourseDetendinfo(StudentRegist objSR, int orgid, int detain_type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[12];
                //Add Fail Subject Details
                //objParams = 

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNOS);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[4] = new SqlParameter("@P_PROV_DETEND", objSR.PROV_DETEND);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_ORGID", orgid);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_COURSENO", objSR.COURSENO);
                objParams[10] = new SqlParameter("@P_DETAIN_TYPE", detain_type);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREG_SP_INSERT_DETEND_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.AddCourseDetendinfo-> " + ex.ToString());
            }

            return retStatus;

        }
        public int UpdateCourseFinalDetend(StudentRegist objStud)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objStud.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_IDNO", objStud.IDNOS);
                objParams[4] = new SqlParameter("@P_FINAL_DETEND", objStud.FINAL_DETEND);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objStud.IPADDRESS);
                objParams[6] = new SqlParameter("@P_UA_NO", objStud.UA_NO);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStud.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_COURSENO", objStud.COURSENO);
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_COURSE_FINAL_DETEND_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateCourseFinalDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }
        public DataSet Get_Final_Detain_StudentList(int sessionno, int semesterno, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_COLLEGE_ID", college_id),
                    // new SqlParameter("@P_COURSENO", courseno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_FINAL_DETENTION_STUDENT_LIST", sqlParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.StudentController.Get_Final_Detain_StudentList() --> " + ex.Message);
            }
            return ds;
        }
        /// <summary>
        /// Added by SP
        /// </summary>
        /// <param name="admbatch"></param>
        /// <param name="clg"></param>
        /// <param name="degree"></param>
        /// <param name="branch"></param>
        /// <param name="idtype"></param>
        /// <param name="semester"></param>
        /// <param name="section"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        //public int GenereateRRNo(int admbatch, int clg, int degree, int branch, int idtype, int semester, int section, int sort1, string sort2)
        //    {
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //        {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[8];
        //        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
        //        objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
        //        objParams[2] = new SqlParameter("@P_DEGREENO", degree);
        //        objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
        //        //objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
        //        objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
        //        //objParams[6] = new SqlParameter("@P_SECTIONNO", section);
        //        objParams[5] = new SqlParameter("@P_SORT1", sort1);
        //        objParams[6] = new SqlParameter("@P_SORT2", sort2);
        //        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[7].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_NEW", objParams, false);

        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

        //        }
        //    catch (Exception ex)
        //        {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
        //        }

        //    return retStatus;

        //    }

        public int AddRevalautionRegisteredSubjects(StudentRegist objSR, int ApprovalFlag)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[10] = new SqlParameter("@P_OPERATION_FLAG", ApprovalFlag);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddRevalautionRegisteredSubjects(StudentRegist objSR, int ApprovalFlag, string revalstatus, string Photocopystatus, int OrgID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[10] = new SqlParameter("@P_OPERATION_FLAG", ApprovalFlag);
                objParams[11] = new SqlParameter("@P_REVALSTATUS", revalstatus);
                objParams[12] = new SqlParameter("@P_PHOTOCOPYSTATUS", Photocopystatus);
                objParams[13] = new SqlParameter("@P_ORGID", OrgID);
                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }



        public int AddExamRegisteredBacklaog(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }
        public int AddRevalautionRegisteredSubjects(StudentRegist objSR, int ApprovalFlag, string revalstatus, string Photocopystatus, int OrgID, string revalAmt, string PhotocopyAmt)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[10] = new SqlParameter("@P_OPERATION_FLAG", ApprovalFlag);
                objParams[11] = new SqlParameter("@P_REVALSTATUS", revalstatus);
                objParams[12] = new SqlParameter("@P_PHOTOCOPYSTATUS", Photocopystatus);
                objParams[13] = new SqlParameter("@P_ORGID", OrgID);
                objParams[14] = new SqlParameter("@P_REVALAMT", revalAmt);
                objParams[15] = new SqlParameter("@P_PHOTOCOPYAMT", PhotocopyAmt);
                objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }


        public int AddRevalTransactionDetails(Exam objSR, int ua_type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.Idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SemesterNo);
                objParams[3] = new SqlParameter("@P_TRANSACTION_NO", objSR.Transaction_no);
                objParams[4] = new SqlParameter("@P_TRANS_DATE", objSR.Transaction_date);
                objParams[5] = new SqlParameter("@P_TRANSACTION_AMT", objSR.trans_amt);
                objParams[6] = new SqlParameter("@P_DOC_NAME", objSR.file_name);
                objParams[7] = new SqlParameter("@P_DOC_PATH", objSR.file_path);
                objParams[8] = new SqlParameter("@P_APPROVAL_STATUS", objSR.Approvedstatus);
                objParams[9] = new SqlParameter("@P_APPROVED_BY", objSR.Approvedby);
                objParams[10] = new SqlParameter("@P_REMARK", objSR.Remark);
                objParams[11] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[12] = new SqlParameter("@P_ORGID", objSR.OrgId);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_REVAL_EXAM_TRANSACTION_DETAILS", objParams, true);
                retStatus = (int)ret;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRevalTransactionDetails-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentDetailsExam(int idno, int Sessionno, int Semesterno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO ", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_EXAM", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }




        /// <summary>
        /// Added by Rohit More on 26.05.2022
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns></returns>
        public int AddExamRegisteredBacklaogRedoCourseReg(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_REDO_COURSE_REGISTRATION", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        /// <summary>
        /// Added By Dileep Kare on 19.10.2021
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="pageno"></param>
        /// <returns></returns>
        public DataSet Get_Student_Details_for_Course_Registration(int idno, int pageno, int ua_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_PAGENO", pageno);
                objParams[2] = new SqlParameter("@P_UA_TYPE", ua_type);
                ds = objSQL.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_COURSE_DETAILS_FOR_REGISTRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.Get_Student_Details_for_Course_Registration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet Get_Student_Details_for_Course_Registration(int idno, int pageno, int ua_type,int crsActivityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_PAGENO", pageno);
                objParams[2] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[3] = new SqlParameter("@P_CRS_ACTIVITY_NO", crsActivityNo);
                ds = objSQL.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_COURSE_DETAILS_FOR_REGISTRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.Get_Student_Details_for_Course_Registration-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Diksha N on 28.06.2022
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns></returns>
        public DataSet AllcourseRegistrationDetailsforExcel(StudentRegist objSR)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);



                ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSEREGISTRATION_SP_GET_REGISTRED_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DefineTotalCreditController.AllcourseRegistrationDetailsforExcel-> " + ex.ToString());
            }
            return ds;
        }
        public int RejectRegisteredSubjectsPhd(StudentRegist objSR, int uatype, string Remark, string course)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //confirmed New Registered Subject Details
                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_UATYPE", uatype);
                objParams[4] = new SqlParameter("@P_REMARK", Remark);
                objParams[5] = new SqlParameter("@P_COURSENO", course);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SUBJECTS_PHD_REJECT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        public int ConfirmedRegisteredSubjectsPhd(StudentRegist objSR, int uatype, string Status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //confirmed New Registered Subject Details
                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_UATYPE", uatype);
                objParams[4] = new SqlParameter("@P_STATUS", Status);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SUBJECTS_PHD_CONFIRMED", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        /// Added by Nikhil Lambe on 23/07/2022 for adding exam registration subject.
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns></returns>
        public int AddExamRegisteredSubjectsPhD(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                //objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                //objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                //objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[2] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_PHD", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddExamRegisteredSubjectsPhD-> " + ex.ToString());
            }

            return retStatus;

        }


        #region RE_EXAM REGISTRATION BY SNEHA DOBLE ON 23/05/2022

        public DataSet GetStudentDetailsforReExam(int idno, int Sessionno, int Schemeno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO ", Sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_COURSE_LIST_FOR_RE_EXAM", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public int AddReExamRegisteredSubjects(StudentRegist objSR, string Semesterno, int OrgID, string retestAmt)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SUBID", objSR.SUBIDS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_ORGID", OrgID);
                objParams[9] = new SqlParameter("@P_RETESTAMT", retestAmt);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REEXAM", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_RULE_WISE", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddReExamRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddReExamTransactionDetails(Exam objSR, int ua_type, string IPAddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[15];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.Idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.Sessionno);
                objParams[2] = new SqlParameter("@P_UANO", objSR.Ua_No);
                objParams[3] = new SqlParameter("@P_TRANSACTION_NO", objSR.Transaction_no);
                objParams[4] = new SqlParameter("@P_TRANS_DATE", objSR.Transaction_date);
                objParams[5] = new SqlParameter("@P_TRANSACTION_AMT", objSR.trans_amt);
                objParams[6] = new SqlParameter("@P_DOC_NAME", objSR.file_name);
                objParams[7] = new SqlParameter("@P_DOC_PATH", objSR.file_path);
                objParams[8] = new SqlParameter("@P_APPROVAL_STATUS", objSR.Approvedstatus);
                objParams[9] = new SqlParameter("@P_APPROVED_BY", objSR.Approvedby);
                objParams[10] = new SqlParameter("@P_REMARK", objSR.Remark);
                objParams[11] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[12] = new SqlParameter("@P_ORGID", objSR.OrgId);
                objParams[13] = new SqlParameter("@P_IPADDRESS", IPAddress);
                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_RE_EXAM_TRANSACTION_DETAILS", objParams, true);
                retStatus = (int)ret;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRevalTransactionDetails-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentReExamDetails(int idno, int Sessionno, int Semesterno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO ", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_REEXAM_REGISTRATION", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentReExamDetails->" + ex.ToString());
            }
            return ds;
        }
        // ADDED BY NARESH BEERLA FOR THE EXAM REGISTRATION ON DT 27052022

        public int AddExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_EXAM_FEES", Amt);
                objParams[11] = new SqlParameter("@P_ORDER_ID", order_id);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTRATION_DETAILS_FOR_STUDENT_IDNOWISE", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }


        public int AddExamRegisteredBacklaog_All(StudentRegist objSR, string Backlogsems)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Backlogsems);
                objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG_ALL", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }


        #endregion

        /// <summary>
        /// Added By Rishabh - 29072022
        /// </summary>
        /// <param name="admbatch"></param>
        /// <param name="clg"></param>
        /// <param name="degree"></param>
        /// <param name="branch"></param>
        /// <param name="semester"></param>
        /// <param name="section"></param>
        /// <param name="sort"></param>
        /// <param name="idtype"></param>
        /// <returns></returns>
        public int GenereateRRNoForRcpiper(int admbatch, int clg, int degree, int branch, int semester, int section, int sort, int idtype)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[5] = new SqlParameter("@P_SORT", sort);
                objParams[6] = new SqlParameter("@P_ID_TYPE", idtype);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_RCPIPER", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenereateRRNoForRcpiper-> " + ex.ToString());
            }

            return retStatus;

        }

        // add jay takalkhede on dated 17/08/2022
        public int CancelRegisteredSubjectsBulkElective(StudentRegist objSR, int COURSENO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SECTIONNO", objSR.SECTIONNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENO", COURSENO);
                objParams[6] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CANCEL_REGIST_SUBJECTS_ELECTIVE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddReTestExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_RETEST_EXAM_REGISTRATION_DETAILS_FOR_STUDENT_IDNOWISE", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int GetGlobalCoursesAvailableSeats(int sessionno, int semesterno, int schemeno, int COURSNO,int branchNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_COURSENO", COURSNO);
                objParams[4] = new SqlParameter("@P_BRANCHNO", branchNo);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSEREGISTRATION_SP_GET_GLOBAL_COURSES_AVAILABLE_SEATS", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetGlobalCoursesAvailableSeats-> " + ex.ToString());
            }



            return retStatus;

        }

        //


        public DataSet GetStudentDetailsForRevaluationExam(int idno, int Sessionno, int Semesterno)
        {
            DataSet ds = null;



            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO ", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_REVAL_REGISTRATION", objParams);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_EXAM", objParams);



            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }
        public int CancelElectiveCourseRegistration(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SECTIONNO", objSR.SECTIONNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENO", objSR.COURSENO);
                objParams[6] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_CANCEL_REGIST_SUBJECTS_ELECTIVE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }


        #region "Photo Copy"
        //Added by Neha Baranwal on date 08/01/2020
        public int AddPhotoCopyRegisteration(StudentRegist objSR, string App_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_PHOTOCOPY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
            }
            return retStatus;
        }



        //public int InsertReviewApplyReg(StudentRegist objSR, decimal Amount, string DDNO)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //                { 
        //                     new SqlParameter("@P_IDNO", objSR.IDNO),
        //                    new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),                           
        //                    new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS),                            
        //                    new SqlParameter("@P_COURSENOS", objSR.COURSENOS),                          
        //                    new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),                        
        //                    new SqlParameter("@P_Amount", Amount),    
        //                     new SqlParameter("@P_DD_NO", DDNO),    
        //                    new SqlParameter("@P_OUT", SqlDbType.Int)
        //                };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_FOR_REVIEW_APPLY_ADMIN", sqlParams, true);
        //        if (Convert.ToInt32(ret) == -99)
        //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        else
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudent_Re_Admission-> " + ex.ToString());
        //    }
        //    return retStatus;
        //}


        public int InsertReviewApplyRegNew(StudentRegist objSR, string App_Type, int User_Type, decimal amount, string DDNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                // objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[3] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[6] = new SqlParameter("@P_CREG_IDNO", objSR.UA_NO);
                // objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[7] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[8] = new SqlParameter("@P_USER_TYPE", User_Type);
                objParams[9] = new SqlParameter("@P_Amount", amount);
                objParams[10] = new SqlParameter("@P_DD_NO", DDNO);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_FOR_REVIEW_APPLY_ADMIN", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
            }
            return retStatus;
        }


        public int AddPhotoCopyRegisteration(StudentRegist objSR, string App_Type, string Total_Exter_Marks, int User_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_GRADES", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_EXTER_MARKS", Total_Exter_Marks);
                objParams[12] = new SqlParameter("@P_USER_TYPE", User_Type);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_PHOTOCOPY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddPhotoRevalRegByAdmin(StudentRegist objSR, string App_Type, string Total_Exter_Marks, int User_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_GRADES", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_EXTER_MARKS", Total_Exter_Marks);
                objParams[12] = new SqlParameter("@P_USER_TYPE", User_Type);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_FOR_PHOTOREVAL_ADMIN", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoRevalRegByAdmin-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by neha
        public DataSet GetStudentFailExamSubjects(int idno, int semesterno, int Sessionno, int Schemeno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_SVCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentFailExamSubjects->" + ex.ToString());
            }
            return ds;
        }

        public int AddExamRegisteredSubjectsback(StudentRegist objSR, decimal Exam_Amt)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_EXAM_AMT", Exam_Amt);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddExamRegisteredSubjectsback-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentAllFailExamSubjects(int idno, int Sessionno, int Schemeno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_SVCE_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentFailExamSubjects->" + ex.ToString());
            }
            return ds;
        }

        public int AddExamRegisteredSubjectsback(StudentRegist objSR, decimal Exam_Amt, string SemesterNos, string SubIdS)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNOS", SemesterNos);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_SUBIDS", SubIdS);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_EXAM_AMT", Exam_Amt);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddExamRegisteredSubjectsback-> " + ex.ToString());
            }
            return retStatus;
        }
        #endregion



        public int AddExamRegisteredBacklaog_CC(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG_CC", objParams, true);
                //  object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        //================================
        //StudentRegistration
        public int AddStudentBacklogExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BACKLOG_EXAM_REGISTRATION_DETAILS_FOR_STUDENT_atlas", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddStudentExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTRATION_DETAILS_FOR_STUDENT", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        public int UpdateCourseRegistrationLoginAttempt(StudentRegist objSR, int subjID = 0)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSE_PATTERN", subjID);
                objParams[5] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_LOGIN_ATTEMPT", 1);

                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("UPSERT_ACD_STUD_COURSE_REG_LOGIN_ATTEMPT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.UpdateCourseRegistrationLoginAttempt-> " + ex.ToString());
            }

            return retStatus;

        }

        //Added dt on 20112022
        public int AddRevaluationChallanDetails(int idno, int sessionno, string schemeno, string courseno, string ipaddress, string semesternos, string college_code, int ua_no, string grades, string ccode, string App_Type, string Total_Exter_Marks, int User_Type)  // string orderid)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_COURSENOS", courseno);
                objParams[4] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[8] = new SqlParameter("@P_GRADES", grades);
                objParams[9] = new SqlParameter("@P_CCODE", ccode);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_EXTER_MARKS", Total_Exter_Marks);
                objParams[12] = new SqlParameter("@P_USER_TYPE", User_Type);
                // objParams[13] = new SqlParameter("@P_ORDERID", orderid);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_EXAM_CHALLAN_DETAILS_FOR_PHOTOCOPY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
            }
            return retStatus;
        }


        public DataSet GetTotalCreditOfValueAddedGrp(int SEMESTERNO, int SCHEMENO, int GROUPID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[2] = new SqlParameter("@P_GROUPID", GROUPID);

                ds = objSQL.ExecuteDataSetSP("PKG_ACD_GET_TOTAL_CREDIT_OF_VALUEADDED_GROUP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetTotalCreditOfValueAddedGrp-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetCourseSectionOfValueAddedGrp(int sessionNo, int idno, int SemNo, int SCHEMENO, int GROUPID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", SemNo);
                objParams[3] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[4] = new SqlParameter("@P_GROUPID", GROUPID);

                ds = objSQL.ExecuteDataSetSP("PKG_ACD_GET_COURSE_SECTIONS_OF_VALUEADDED_GROUP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetCourseSectionOfValueAddedGrp-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateGroupsForValueAddedCourse(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@V_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_GRP_VALUEADDED_COURSE_STATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.UpdateGroupsForValueAddedCourse-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpSertGroupsForValueAddedCourse(StudentRegist objSR, int grpID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_GROUPID", grpID);
                objParams[7] = new SqlParameter("@V_ORG_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPSERT_VALUEADDED_COURSE_STATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.UpSertGroupsForValueAddedCourse-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddStudentImproveExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_IMPROVE_EXAM_REGISTRATION_DETAILS_FOR_STUDENT ", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }


        public int AddExamRegisteredImprove_CC(StudentRegist objSR, int UserNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Improve eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[9] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[10] = new SqlParameter("@P_USER_TYPE", UserNo);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_IMPROVE_EXAMREGISTRATION", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }
        //Added By Sachin A dt on 22122022 for supplementary



        public int AddExamRegisteredSubjectsback(StudentRegist objSR, string SubIdS, decimal fees, string SEMESTERNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;



                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];



                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNOS", SEMESTERNO);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_SUBIDS", SubIdS);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_EXAM_AMT", fees);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;



                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_CRESCENT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentFailExamSubjects(int idno, string semesterno, int Sessionno)
        {
            DataSet ds = null;



            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);



                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_CRESCENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        //Added By Tejaswini[25-11-22]
        public DataSet GetStudentsForSchemeModifyBulk(int admbatch, int schemeno, int degreeno, int schemetype, int semno, int college_id, int sectionno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                objParams[4] = new SqlParameter("@P_SEM", semno);
                objParams[5] = new SqlParameter("@P_COLLEGEID", college_id);
                objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[7] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_STUDENT_BY_SCHEME1_MODIFY_BULK_STUDENT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentsForSchemeModifyBulk-> " + ex.ToString());
            }
            return ds;
        }

        //Added by Tejaswini Dhoble[25-11-22]
        public int AddRegisteredSubjectsModifyBulk(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                           // new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_BULK_MODYFY_COURSE", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetOfferedCourseListForModifyBulkCourseRegistration(int sessionno, int schemeno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_OFFERED_FOR_MODIFY_BULK_COURSE_REGISTRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllOfferedCourseList-> " + ex.ToString());
            }
            return ds;
        }


        //added by Prafull on 10-01-2023
        public int AddExamRegisteredSubjectsbackByAdmin(StudentRegist objSR, string SubIdS, decimal fees, string SEMESTERNO)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNOS", SEMESTERNO);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_SUBIDS", SubIdS);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_EXAM_AMT", fees);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_SUPPLEMENTARY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                }
            return retStatus;
            }


        //added by Prafull on 10-01-2023
        public int AddExamRegistrationDetails_Backlog(StudentRegist objSR, string Amt)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_EXAM_FEES", Amt);

                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTRATION_DETAILS_FOR_STUDENT_IDNOWISE_BACKLOG", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                }

            return retStatus;

            }


        /// <summary>
        /// Added by Swapnil P dated on 12-01-2023 for Approval login Course registration
        /// </summary>
        /// <param name="objSR"></param>
        /// <returns></returns>
        public int AddAddlRegisteredSubjectsApprovalLogin(StudentRegist objSR)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_UA_N0", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course);
                objParams[11] = new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED);
                objParams[12] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_APPROVAL_LOGIN", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
                }

            return retStatus;

            }
        public int GenereateRRNo(int admbatch, int clg, int degree, int branch, int idtype, int semester, int section, int sort1, string sort2)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semester);
                //objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[6] = new SqlParameter("@P_SORT1", sort1);
                objParams[7] = new SqlParameter("@P_SORT2", sort2);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_NEW", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

        public int AddStudentResultData(string SESSIONNOS, StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_SESSIONNOS", SESSIONNOS);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[3] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[7] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_GRADES", objSR.GRADE);

                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_RESULT_DATA", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }


        public int AddTransferedStudentRecord(string SESSIONNOS, StudentRegist objSR, string equigrade, DateTime date)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNOS);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[4] = new SqlParameter("@P_COURSENO", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_EQUI_CCODE", objSR.CCODES);
                objParams[6] = new SqlParameter("@P_EQUI_COURSE", objSR.COURSENAMES);
                objParams[7] = new SqlParameter("@P_EQUI_GRADE", equigrade);
                objParams[8] = new SqlParameter("@P_NEWGRADE", objSR.GRADE);
                objParams[9] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[10] = new SqlParameter("@P_TRANSDATE", date);
                objParams[11] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[12] = new SqlParameter("@P_ExamType", objSR.ExamType);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_TRANSFERED_STUD_EQUI_RECORD", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
                }
            return retStatus;
            }


        public DataTableReader GetStudentDetails_Crescent(int idno)
        {
            DataTableReader dtr = null;

            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                dtr = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDDETAILS_Crescent", objParams).Tables[0].CreateDataReader(); // PKG_PREREGIST_SP_RET_STUDDETAILS_Crescent
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetails-> " + ex.ToString());
            }

            return dtr;
        }

        // added by Ro-hit M on 23_05_2023
        public int GenereateRollNo_Rajagiri(int admbatch, int clg, int degree, int branch, int idtype, int semester, int section, int sort)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SORT", sort);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ROLLNO_GENERATION_RAJAGIRI", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                }

            return retStatus;

            }

        // added by Ro-hit M on 23_05_2023
        public int GenereateRRNoForRajagiri(int admbatch, int clg, int degree, int branch, int semester, int section, int sort, int idtype)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[5] = new SqlParameter("@P_SORT", sort);
                objParams[6] = new SqlParameter("@P_ID_TYPE", idtype);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_RAJAGIRI", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenereateRRNoForRcpiper-> " + ex.ToString());
                }

            return retStatus;

            }

        // added by Ro-hit M on 23_05_2023
        public int GenereateRRNoForMaher(int admbatch, int clg, int degree, int branch, int semester, int section, int sort, int idtype)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[5] = new SqlParameter("@P_SORT", sort);
                objParams[6] = new SqlParameter("@P_ID_TYPE", idtype);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_MAHER", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenereateRRNoForRcpiper-> " + ex.ToString());
                }

            return retStatus;

            }

        //Added by Nehal 09062023
        public int AddAddlRegisteredSubjectsAuditTypeCourse(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_UA_N0", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course);
                objParams[11] = new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED);
                objParams[12] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_AUDIT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;

        }

  //Changes made by Nehal N
        public DataSet GetStudentCourseRegistrationSubjectAudit_course_type(int SESSIONNO, int IDNO, int SEMESTERNO, int SCHEMENO, int COMMANDTYPE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[4] = new SqlParameter("@P_COMMANDTYPE", COMMANDTYPE);

                ds = objSQL.ExecuteDataSetSP("PKG_AUDIT_COURSEREGISTRATION_SP_GET_OFFERED_SUBJECTS_AUDIT_COURSE_TYPE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCourseRegistrationSubject-> " + ex.ToString());
            }
            return ds;
        }
       
        // ADDED BY Sagar mankar on dated 21062023
        public int AddExamRegisteredBacklaog_CC_REMEDAL(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG_CC_REMEDAL", objParams, true);
                //  object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;

        }

        //added by Rohit M on 13-07-2023
        public int GenereateRRNoForDaiict(int admbatch, int clg, int degree, int branch, int semester, int Year, int section, int sort, int idtype)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[5] = new SqlParameter("@P_YEAR", Year);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SORT", sort);
                objParams[8] = new SqlParameter("@P_ID_TYPE", idtype);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_DAIICT", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenereateRRNoForRcpiper-> " + ex.ToString());
                }

            return retStatus;

            }

        //added by Rohit M on 01-08-2023
        public int GenereateRRNoForHITS(int admbatch, int clg, int degree, int branch, int semester, int Year, int section, int sort, int idtype)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[5] = new SqlParameter("@P_YEAR", Year);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SORT", sort);
                objParams[8] = new SqlParameter("@P_ID_TYPE", idtype);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_HITS", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenereateRRNoForRcpiper-> " + ex.ToString());
                }

            return retStatus;

            }

        //added by Rohit M on 04-08-2023
        public int GenereateRRNoForPRMCEM(int admbatch, int clg, int degree, int branch, int semester, int Year, int section, int sort, int idtype)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[5] = new SqlParameter("@P_YEAR", Year);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SORT", sort);
                objParams[8] = new SqlParameter("@P_ID_TYPE", idtype);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_RR_GENERATION_PRMCEM", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenereateRRNoForRcpiper-> " + ex.ToString());
                }

            return retStatus;

            }

        //added by nehal n on 31072023
        public int InsertSemesterconfig(int semesterno, string schemenos)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SEMESTERNO", semesterno),  
                        new SqlParameter("@P_SCHEMENO", schemenos),  
                        new SqlParameter("@P_OUTPUT", ParameterDirection.Output),
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_SEMESTER_CONFIG_AUDIT_MODE", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertSemesterconfig-> " + ex.ToString());
            }
            return retStatus;
        }


        //added by nehal n on 31072023
        public DataSet GetAllSemConfig(int modeno)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_MODE", modeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEMESTER_CONFIG_AUDIT_MODE_GET_ALL", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllSemConfig() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }

        //Added by Prafull on 07_08_2023
        public int AddReExamTransactionDetails(Exam objSR, int ua_type, string IPAddress, int semesterno)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[16];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.Idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.Sessionno);
                objParams[2] = new SqlParameter("@P_UANO", objSR.Ua_No);
                objParams[3] = new SqlParameter("@P_TRANSACTION_NO", objSR.Transaction_no);
                objParams[4] = new SqlParameter("@P_TRANS_DATE", objSR.Transaction_date);
                objParams[5] = new SqlParameter("@P_TRANSACTION_AMT", objSR.trans_amt);
                objParams[6] = new SqlParameter("@P_DOC_NAME", objSR.file_name);
                objParams[7] = new SqlParameter("@P_DOC_PATH", objSR.file_path);
                objParams[8] = new SqlParameter("@P_APPROVAL_STATUS", objSR.Approvedstatus);
                objParams[9] = new SqlParameter("@P_APPROVED_BY", objSR.Approvedby);
                objParams[10] = new SqlParameter("@P_REMARK", objSR.Remark);
                objParams[11] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[12] = new SqlParameter("@P_ORGID", objSR.OrgId);
                objParams[13] = new SqlParameter("@P_SEMESTERNO", semesterno); ///added by prafull on dt:07082023
                objParams[14] = new SqlParameter("@P_IPADDRESS", IPAddress);
                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_RE_EXAM_TRANSACTION_DETAILS", objParams, true);
                retStatus = (int)ret;
                }
            catch (Exception ex)
                {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRevalTransactionDetails-> " + ex.ToString());
                }
            return retStatus;
            }

        public int AddExamRegisteredImprovementCourseReg(StudentRegist objSR)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_IMPROVEMENT_COURSE_REGISTRATION", objParams, true);
                //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                }

            return retStatus;

            }

        //added by nehal n on 23082023
        public int UpdateSemesterconfig(int semesterno, string schemenos, int id)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SEMESTERNO", semesterno),  
                        new SqlParameter("@P_SCHEMENO", schemenos), 
                        new SqlParameter("@P_ID", id), 
                        new SqlParameter("@P_OUTPUT", ParameterDirection.Output),
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_SEMESTER_CONFIG_AUDIT_MODE", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UpdateSemesterconfig-> " + ex.ToString());
            }
            return retStatus;
        }





    }
}

