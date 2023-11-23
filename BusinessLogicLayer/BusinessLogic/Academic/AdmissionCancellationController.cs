//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ADMISSION CANCELLATION CONTROLLER
// CREATION DATE : 08-AUG-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

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
    public class AdmissionCancellationController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet SearchStudents(string searchText, string searchBy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_SEARCHSTRING", searchText),
                    new SqlParameter("@P_SEARCH", searchBy)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetClearanceInfo(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ADMCAN_GET_CLEARANCE_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.GetClearanceInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool CancelAdmission(int studentId, string remark)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_IDNO", studentId)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_ADMCAN_CANCEL_ADMISSION", sqlParams, true);
                
                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        public DataSet ProspectusSearchStudents(string searchText, string searchBy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                   new SqlParameter("@P_SEARCHSTRING", searchText),
                    new SqlParameter("@P_SEARCH", searchBy)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PROSPECTUS_SEARCH_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool CancelProspectus(int prosno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    
                    new SqlParameter("@P_PROSNO", prosno)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_STUDENT_CANCEL_PROSPECTUS", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        public DataSet ProspectusSearchStudentsDatewise(string frmdate, string todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                   new SqlParameter("@P_FRMDATE", frmdate),
                    new SqlParameter("@P_TODATE", todate)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PROSPECTUS_SEARCH_STUDENT_DATEWISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ProspectusSearchStudentsDatewise.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet DisplayTimeTableFacultyCourse(Int32 SESSIONNO, Int32 COURSENO, Int32 SECTIONNO, Int32 UANO, Int32 VERSION, string CCODE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[5];
                string spname = "";
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                if (COURSENO > 0)
                {
                    objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                    spname = "PKG_EXAM_TIMETABLE_REPORT_FACULTY_COURSE";
                }
                else
                {
                    objParams[1] = new SqlParameter("@P_CCODE", CCODE);
                    spname = "PKG_EXAM_TIMETABLE_REPORT_FACULTY_COURSE_GLOBALELE";
                }
                objParams[2] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                objParams[3] = new SqlParameter("@P_UA_NO", UANO);
                objParams[4] = new SqlParameter("@V_VERSION", VERSION);
                ds = objSQLHelper.ExecuteDataSetSP(spname, objParams);


            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetTimeTable-> " + ex.ToString());
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }
        //FOR ADMISSION CANCEL STUDENT LIST EXCEL--[29-07-2016]
        public DataSet GetCancelledAdmissionStudentList(string START_DATE, string END_DATE, int DEGREENO, int BRANCHNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_START_DATE", START_DATE);
                objParams[1] = new SqlParameter("@P_END_DATE", END_DATE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);


                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT_IN_EXCEL", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetCancelledAdmissionStudentListList() -->" + ex.ToString());

            }
            return ds;
        }

        /// <summary>
        /// added by:Deepali Dated on:13/02/2020
        /// </summary>
        /// <param name="START_DATE"></param>
        /// <param name="END_DATE"></param>
        /// <param name="DEGREENO"></param>
        /// <param name="BRANCHNO"></param>
        /// <returns></returns>
        public DataSet GetCancelledAdmissionStudentReport(DateTime START_DATE, DateTime END_DATE, int DEGREENO, int BRANCHNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_START_DATE", START_DATE);
                objParams[1] = new SqlParameter("@P_END_DATE", END_DATE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);


                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetCancelledAdmissionStudentReport() -->" + ex.ToString());

            }
            return ds;
        }

        public int Cancel_Admission_First_Level(int IDNO, int UA_NO, string Remark, string ipAddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                objParams[2] = new SqlParameter("@P_REMARK", Remark);
                objParams[3] = new SqlParameter("@P_IP_ADDRESS", ipAddress);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CANCEL_ADM_FIRST_LEVEL", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.Cancel_Admission_First_Level-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudDetailsForFirstLevel(int admbatch)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ADM_BATCH ", admbatch);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMCAN_SP_RET_STUDDETAILS_FIRST_APPROVE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetStudDetailsForFirstLevel-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAdmCancelFirstLevel()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ADM_CANCEL_REPORT", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetAdmCancelFirstLevel-> " + ex.ToString());
            }
            return ds;
        }

        public int Cancel_Admission_Academic_Level(int IDNO, int UA_NO, string Remark, string ipAddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                objParams[2] = new SqlParameter("@P_REMARK", Remark);
                objParams[3] = new SqlParameter("@P_IP_ADDRESS", ipAddress);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CANCEL_ADM_ACADEMIC_LEVEL", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.Cancel_Admission_Academic_Level-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudDetailsForAcademicLevel(int admbatch, int degreeno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID ", admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMCAN_SP_RET_STUDDETAILS_ACADEMIC_APPROVE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetStudDetailsForAcademicLevel-> " + ex.ToString());
            }

            return ds;
        }

        public int AdmissionCancel_FinalApprove(int studentId, int ua_no, string remark, string ip_address)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Add Branch change data
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", studentId);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_UANO", ua_no);
                objParams[3] = new SqlParameter("@P_IP_ADDRESS", ip_address);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMCAN_SP_UPD_ADM_CANCEL_FINAL_APPROVE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdmissionCancellationController.AdmissionCancel_FinalApprove-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertReadmissionFileDetails(int idno, string fileType, string fileName, string path)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_FILETYPE", fileType);
                objParams[2] = new SqlParameter("@P_FILE_NAME", fileName);
                objParams[3] = new SqlParameter("@P_FILE_PATH", path);
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_READMISSION_FILE_DETAILS", objParams, true));

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.StudentController.InsertReadmissionFileDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AdmissionCancel_Request(int studentId, string remark, int ua_no, string ip_address, string college_code, string filetype, string filename, string path, int Organizationid, int breakflag, int month, int year)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_IDNO", studentId);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                objParams[3] = new SqlParameter("@P_UANO", ua_no);
                objParams[4] = new SqlParameter("@P_IP_ADDRESS", ip_address);
                objParams[5] = new SqlParameter("@P_FILE_TYPE", filetype);
                objParams[6] = new SqlParameter("@P_FILE_NAME", filename);
                objParams[7] = new SqlParameter("@P_PATH", path);
                objParams[8] = new SqlParameter("@P_ORGANIZATIONID", Organizationid);//Added By Dileep Kare on 31.12.2021
                objParams[9] = new SqlParameter("@P_BREAKFLAG", breakflag);
                objParams[10] = new SqlParameter("@P_MONTH", month);
                objParams[11] = new SqlParameter("@P_YEAR", year);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMCAN_SP_INS_ADM_CANCEL_REQUEST", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdmissionCancellationController.AdmissionCancel_Request-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetFeesDetailsForReadmission(int idno, int degreeno, int batchno, int paytypeno, int college_id, int branchno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BATCHNO", batchno),
                    new SqlParameter("@P_PAYTYPENO", paytypeno),
                    new SqlParameter("@P_COLLEGE_ID", college_id),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_SEMESTERNO", semesterno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_FEE_DETAILS_FOR_READMISSION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.GetFeesDetailsForReadmission() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

     
        //added by safal gupta on 22/02/2021

        public DataSet GetStudentsForCancelAdmission(StudentRegist srObj, int organizationid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO",srObj.IDNO),
                     new SqlParameter("@P_ORGANIZATIONID",organizationid)//added by Dileep Kare on 31.12.2021
                   
                  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_FOR_CANCEL_ADDMISSION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // added by Safal gupta on 03022021
        public DataSet GetFeesDetailsForAdmissionCan(int idno, int degreeno, int batchno, int paytypeno, int college_id, int branchno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", idno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BATCHNO", batchno),
                    new SqlParameter("@P_PAYTYPENO", paytypeno),
                    new SqlParameter("@P_COLLEGE_ID", college_id),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_SEMESTERNO", semesterno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_FEE_DETAILS_FOR_ADMISSION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.GetFeesDetailsForAdmissionCan() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY SAFAL GUPTA 01022021
        public DataSet SearchStudents1(string searchText, string searchBy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_SEARCHSTRING", searchText),
                    new SqlParameter("@P_SEARCH", searchBy)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT_FOR_RE_ADMIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCancelledAdmissionStudentReport(DateTime START_DATE, DateTime END_DATE, int DEGREENO, int BRANCHNO, int reporttype, int Organizationid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_START_DATE", START_DATE);
                objParams[1] = new SqlParameter("@P_END_DATE", END_DATE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_REPORTTYPE", reporttype);
                objParams[5] = new SqlParameter("@P_ORGANIZATIONID", Organizationid);//added by Dileep Kare on 31.12.2021


                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetCancelledAdmissionStudentReport() -->" + ex.ToString());

            }
            return ds;
        }

        //added by safal gupta on 19/02/2021

        public int AdmissionCancel(int studentId, string remark, int ua_no, string ip_address, string college_code, string filetype, string filename, string path)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_IDNO", studentId);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                objParams[3] = new SqlParameter("@P_UANO", ua_no);
                objParams[4] = new SqlParameter("@P_IP_ADDRESS", ip_address);
                objParams[5] = new SqlParameter("@P_FILE_TYPE", filetype);
                objParams[6] = new SqlParameter("@P_FILE_NAME", filename);
                objParams[7] = new SqlParameter("@P_PATH", path);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMISSION_CANCEL_SP_INS_REQUEST", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdmissionCancellationController.AdmissionCancel_Request-> " + ex.ToString());
            }
            return retStatus;
        }

        //public DataSet GetStudentsForCancelAdmission(StudentRegist srObj, int organizationid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        { 
        //            new SqlParameter("@P_REGNO",srObj.REGNO),
        //             new SqlParameter("@P_STUDNAME",srObj.STUDNAME),
        //             new SqlParameter("@P_ORGANIZATIONID",organizationid)//added by Dileep Kare on 31.12.2021
                   
                  
        //        };
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_FOR_CANCEL_ADDMISSION", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        //=================================================================================
        //public DataSet GetCancelledAdmissionStudentReport(DateTime START_DATE, DateTime END_DATE, int DEGREENO, int BRANCHNO, int reporttype, int Organizationid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] objParams = null;
        //        objParams = new SqlParameter[6];
        //        objParams[0] = new SqlParameter("@P_START_DATE", START_DATE);
        //        objParams[1] = new SqlParameter("@P_END_DATE", END_DATE);
        //        objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
        //        objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
        //        objParams[4] = new SqlParameter("@P_REPORTTYPE", reporttype);
        //        objParams[5] = new SqlParameter("@P_ORGANIZATIONID", Organizationid);//added by Dileep Kare on 31.12.2021


        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT", objParams);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetCancelledAdmissionStudentReport() -->" + ex.ToString());

        //    }
        //    return ds;
        //}
        //==================================================================================
        public bool CancelAdmission(int studentId, string remark, int organizationid)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_ORGANIZATIONID",organizationid),//Added by Dileep on 31.12.2021
                    new SqlParameter("@P_IDNO", studentId)
                  
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_ADMCAN_CANCEL_ADMISSION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
        //===============================================================================

        //public DataSet GetStudentsForCancelAdmission(StudentRegist srObj, int organizationid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        { 
        //            new SqlParameter("@P_IDNO",srObj.IDNO),
        //             new SqlParameter("@P_ORGANIZATIONID",organizationid)//added by Dileep Kare on 31.12.2021
                   
                  
        //        };
        //        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_FOR_CANCEL_ADDMISSION", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public int AdmissionCancel_Request(int studentId, string remark, int ua_no, string ip_address, string college_code, string filetype, string filename, string path, int Organizationid)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", studentId);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                objParams[3] = new SqlParameter("@P_UANO", ua_no);
                objParams[4] = new SqlParameter("@P_IP_ADDRESS", ip_address);
                objParams[5] = new SqlParameter("@P_FILE_TYPE", filetype);
                objParams[6] = new SqlParameter("@P_FILE_NAME", filename);
                objParams[7] = new SqlParameter("@P_PATH", path);
                objParams[8] = new SqlParameter("@P_ORGANIZATIONID", Organizationid);//Added By Dileep Kare on 31.12.2021
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMCAN_SP_INS_ADM_CANCEL_REQUEST", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdmissionCancellationController.AdmissionCancel_Request-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Adde by Dileep Kare on 03.01.2022
        /// </summary>
        /// <param name="Idno"></param>
        /// <param name="Organizationid"></param>
        /// <returns></returns>
        public DataSet Get_Student_Details_Readmit_Branch_Payment_type(int Idno, int Organizationid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", Idno),
                    new SqlParameter("@P_ORGANIZATIONID",Organizationid)
                   
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_READMIT_BRANCH_PAYMENY_CHANGE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep K
        /// Date:03.01.2021
        /// For Readmission ,Branch change and Payment Type Modification
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="studentId"></param>
        /// <param name="semesterNo"></param>
        /// <param name="receiptType"></param>
        /// <param name="examtype"></param>
        /// <param name="currency"></param>
        /// <param name="payTypeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataSet Get_Payment_Details(int sessionNo, int studentId, int semesterNo, string receiptType, int examtype, int currency, int payTypeNo, ref int status)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECEIPT_CODE", receiptType),
                    new SqlParameter("@P_EXAMTYPE", examtype),
                    new SqlParameter("@P_CURRENCY",currency),
                    new SqlParameter("@P_PAYTYPENO",payTypeNo),
                    new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_PAYMENT_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Payment_Details() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 05.01.20222
        /// </summary>
        /// <param name="studId"></param>
        /// <param name="demandCriteria"></param>
        /// <param name="remark"></param>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public bool ReAdmissionStudent(int studId, Student objs, string remark, string ipaddress, int organizationid)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                      
                    new SqlParameter("@P_ADMBATCHNO",objs.AdmBatch),
                    new SqlParameter("@P_COLLEGE_ID",objs.NewCollege_ID),
                    new SqlParameter("@P_DEGREENO",objs.NewDegreeNo),
                    new SqlParameter("@P_BRANCHNO",objs.NewBranchNo),
                    new SqlParameter("@P_SEMESTERNO",objs.SemesterNo),
                    new SqlParameter("@P_REMARK",remark),
                    new SqlParameter("@P_IPADDRESS", ipaddress),
                    new SqlParameter("@P_READMIT_UANO",objs.Uano),
                    new SqlParameter("@P_ORGANIZATIONID",organizationid),
                    new SqlParameter("@P_PTYPE",objs.NewPayTypeNO),
                    new SqlParameter("@P_IDNO", studId)

                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_STUDENT_INS_RE_ADMISSION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.ReAdmissionStudent() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// Added By Dileep Kare on 05.01.20222
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="demandCriteria"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public string CreateDemandReadmission(int studentID, Student objs, bool overwrite)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", objs.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", objs.IdNo),
                    new SqlParameter("@P_RECIEPTCODE", objs.ReceiptNo),
                    new SqlParameter("@P_BRANCHNO", objs.NewBranchNo),
                    new SqlParameter("@P_DEGREENO", objs.NewDegreeNo),
                    new SqlParameter("@P_SELECT_SEMESTERNO", objs.SemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", objs.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", objs.NewPayTypeNO),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", objs.Uano),
                    new SqlParameter("@P_COLLEGE_CODE", objs.CollegeCode),
                    new SqlParameter("@P_COLLEGE_ID", objs.NewCollege_ID),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_READMISSION", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        /// <summary>
        /// Added By Dileep Kare
        /// on 04.01.2022
        /// </summary>
        /// <param name="objStudent"></param>
        /// <param name="paidfee"></param>
        /// <param name="excessamt"></param>
        /// <param name="Organizationid"></param>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public int Ins_ChangeBranch(Student objStudent, decimal paidfee, decimal excessamt, int Organizationid, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Add Branch change data
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                objParams[4] = new SqlParameter("@P_REMARK", objStudent.Remark);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                objParams[6] = new SqlParameter("@P_USERNO", objStudent.Uano);
                objParams[7] = new SqlParameter("@P_PAIDFEE", paidfee);
                objParams[8] = new SqlParameter("@P_EXCESSAMOUNT", excessamt);
                objParams[9] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                objParams[10] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);
                objParams[11] = new SqlParameter("@P_NEW_COLLEGE_ID", objStudent.NewCollege_ID);
                objParams[12] = new SqlParameter("@P_ORGANIZATIONID", Organizationid);
                objParams[13] = new SqlParameter("@P_IPADDRESS", ipaddress);

                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_INS_BRANCH_CHANGE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.Ins_ChangeBranch-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added By Dileep Kare 
        /// on 05.01.2022 
        /// for Create Demand for Branch change student
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="objs"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public string CreateDemandBranchChange(int studentID, Student objs, bool overwrite, int organizationid)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", objs.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", objs.IdNo),
                    new SqlParameter("@P_RECIEPTCODE", objs.ReceiptNo),
                    new SqlParameter("@P_BRANCHNO", objs.NewBranchNo),
                    new SqlParameter("@P_DEGREENO", objs.NewDegreeNo),
                    new SqlParameter("@P_SELECT_SEMESTERNO", objs.SemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", objs.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", objs.PayTypeNO),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", objs.Uano),
                    new SqlParameter("@P_COLLEGE_CODE", objs.CollegeCode),
                    new SqlParameter("@P_COLLEGE_ID", objs.NewCollege_ID),
                    new SqlParameter("@P_ORGANIZATIONID",organizationid),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_FOR_BRANCHCHANGE", sqlParams, true);
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }


        /// <summary>
        /// Added By Dileep Kare 
        /// on 05.01.2022 
        /// for Create Demand for Branch change student
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="objs"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public string CreateDemandPaymentTypeModification(int studentID, Student objs, bool overwrite, int organizationid)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", objs.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", objs.IdNo),
                    new SqlParameter("@P_RECIEPTCODE", objs.ReceiptNo),
                    new SqlParameter("@P_BRANCHNO", objs.NewBranchNo),
                    new SqlParameter("@P_DEGREENO", objs.NewDegreeNo),
                    new SqlParameter("@P_SELECT_SEMESTERNO", objs.SemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", objs.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", objs.NewPayTypeNO),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", objs.Uano),
                    new SqlParameter("@P_COLLEGE_CODE", objs.CollegeCode),
                    new SqlParameter("@P_COLLEGE_ID", objs.NewCollege_ID),
                    new SqlParameter("@P_ORGANIZATIONID",organizationid),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_PAYTYPE_MODIFICATION", sqlParams, true);
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }
        /// <summary>
        /// Added By Dileep Kare 
        /// on 05.01.2022 
        /// </summary>
        /// <param name="objStudent"></param>
        /// <param name="paidfee"></param>
        /// <param name="excessamt"></param>
        /// <param name="Organizationid"></param>
        /// <param name="ipaddress"></param>
        /// <param name="iswithbranch"></param>
        /// <returns></returns>
        public int Ins_PaymentTypeModification(Student objStudent, int Organizationid, string ipaddress, int iswithbranch)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Add Branch change data
                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                objParams[4] = new SqlParameter("@P_REMARK", objStudent.Remark);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                objParams[6] = new SqlParameter("@P_USERNO", objStudent.Uano);
                objParams[7] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);
                objParams[9] = new SqlParameter("@P_NEW_COLLEGE_ID", objStudent.NewCollege_ID);
                objParams[10] = new SqlParameter("@P_ORGANIZATIONID", Organizationid);
                objParams[11] = new SqlParameter("@P_OLD_PTYPE", objStudent.PayTypeNO);
                objParams[12] = new SqlParameter("@P_NEW_PTYPE", objStudent.NewPayTypeNO);
                objParams[13] = new SqlParameter("@P_IS_WITH_BRANCH", iswithbranch);
                objParams[14] = new SqlParameter("@P_IPADDRESS", ipaddress);

                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_INS_PAYMENT_TYPE_MODIFICATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.Ins_ChangeBranch-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added By Dileep Kare
        /// on 05.01.2022
        /// for saving adjustment
        /// </summary>
        /// <param name="dcr"></param>
        /// <param name="checkAllow"></param>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public int SaveFeeCollection_Transaction(ref DailyCollectionRegister dcr)
        {
            int dcrno = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", dcr.StudentId),
                    new SqlParameter("@P_ENROLLNMENTNO", dcr.EnrollmentNo),
                    new SqlParameter("@P_NAME", dcr.StudentName),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),
                    new SqlParameter("@P_BRANCH", dcr.BranchName),
                    new SqlParameter("@P_DEGREENO", dcr.DegreeNo),
                    new SqlParameter("@P_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_SESSIONNO", dcr.SessionNo),
                    new SqlParameter("@P_CURRENCY",dcr.Currency),
                    new SqlParameter("@P_F1", dcr.FeeHeadAmounts[0]),
                    new SqlParameter("@P_F2", dcr.FeeHeadAmounts[1]),
                    new SqlParameter("@P_F3", dcr.FeeHeadAmounts[2]),
                    new SqlParameter("@P_F4", dcr.FeeHeadAmounts[3]),
                    new SqlParameter("@P_F5", dcr.FeeHeadAmounts[4]),
                    new SqlParameter("@P_F6", dcr.FeeHeadAmounts[5]),
                    new SqlParameter("@P_F7", dcr.FeeHeadAmounts[6]),
                    new SqlParameter("@P_F8", dcr.FeeHeadAmounts[7]),
                    new SqlParameter("@P_F9", dcr.FeeHeadAmounts[8]),
                    new SqlParameter("@P_F10", dcr.FeeHeadAmounts[9]),
                    new SqlParameter("@P_F11", dcr.FeeHeadAmounts[10]),
                    new SqlParameter("@P_F12", dcr.FeeHeadAmounts[11]),
                    new SqlParameter("@P_F13", dcr.FeeHeadAmounts[12]),
                    new SqlParameter("@P_F14", dcr.FeeHeadAmounts[13]),
                    new SqlParameter("@P_F15", dcr.FeeHeadAmounts[14]),
                    new SqlParameter("@P_F16", dcr.FeeHeadAmounts[15]),
                    new SqlParameter("@P_F17", dcr.FeeHeadAmounts[16]),
                    new SqlParameter("@P_F18", dcr.FeeHeadAmounts[17]),
                    new SqlParameter("@P_F19", dcr.FeeHeadAmounts[18]),
                    new SqlParameter("@P_F20", dcr.FeeHeadAmounts[19]),
                    new SqlParameter("@P_F21", dcr.FeeHeadAmounts[20]),
                    new SqlParameter("@P_F22", dcr.FeeHeadAmounts[21]),
                    new SqlParameter("@P_F23", dcr.FeeHeadAmounts[22]),
                    new SqlParameter("@P_F24", dcr.FeeHeadAmounts[23]),
                    new SqlParameter("@P_F25", dcr.FeeHeadAmounts[24]),
                    new SqlParameter("@P_F26", dcr.FeeHeadAmounts[25]),
                    new SqlParameter("@P_F27", dcr.FeeHeadAmounts[26]),
                    new SqlParameter("@P_F28", dcr.FeeHeadAmounts[27]),
                    new SqlParameter("@P_F29", dcr.FeeHeadAmounts[28]),
                    new SqlParameter("@P_F30", dcr.FeeHeadAmounts[29]),
                    new SqlParameter("@P_F31", dcr.FeeHeadAmounts[30]),
                    new SqlParameter("@P_F32", dcr.FeeHeadAmounts[31]),
                    new SqlParameter("@P_F33", dcr.FeeHeadAmounts[32]),
                    new SqlParameter("@P_F34", dcr.FeeHeadAmounts[33]),
                    new SqlParameter("@P_F35", dcr.FeeHeadAmounts[34]),
                    new SqlParameter("@P_F36", dcr.FeeHeadAmounts[35]),
                    new SqlParameter("@P_F37", dcr.FeeHeadAmounts[36]),
                    new SqlParameter("@P_F38", dcr.FeeHeadAmounts[37]),
                    new SqlParameter("@P_F39", dcr.FeeHeadAmounts[38]),
                    new SqlParameter("@P_F40", dcr.FeeHeadAmounts[39]),
                    new SqlParameter("@P_TOTAL_AMT", dcr.TotalAmount),
                    new SqlParameter("@P_RECIEPT_CODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_REMARK", dcr.Remark),
                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    new SqlParameter("@P_DCRNO", dcr.DcrNo),    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                dcrno = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_FEECOLLECT_INS_DCR_ADJUSTMENT", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SaveFeeCollection_Transaction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dcrno;
        }

        /// <summary>
        /// Added Dileep Kare 
        /// 05.01.2022
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="college_id"></param>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <param name="semesterno"></param>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public DataSet Get_Admission_Activity_status(int idno, int college_id, int degreeno, int branchno, int semesterno, int ptype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_IDNO", idno),
                     new SqlParameter("@P_COLLEGE_ID", college_id),
                     new SqlParameter("@P_DEGREENO",degreeno),
                     new SqlParameter("@P_BRANCHNO",branchno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_PTYPE",ptype),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_CHECK_ADMISSION_BRANCH_PTYE_STATUS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_Admission_Activity_status() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Tejaswini[05-1-23]
        public int UpdateReadmissionInSameBatch(int idno, Student objs, int organizationid,int ChkRegno, string IpAddress)
            {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_UA_NO",objs.Uano),
                            new SqlParameter("@P_ADMBATCHNO",objs.AdmBatch),
                            new SqlParameter("@P_SEMESTERID",objs.SemesterNo),
                            new SqlParameter("@P_SCHEMEID",objs.SchemeNo),
                            new SqlParameter("@P_CHKREGNO",ChkRegno),
                            new SqlParameter("@P_IP_ADDRESS",IpAddress),
                            new SqlParameter("@P_OUT", SqlDbType.Int),                       
                          };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_READMISSION_IN_SAME_BRANCH", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            catch (Exception ex)
                {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AdmissionCancellationController.UpdateReadmissionInSameBatch-> " + ex.ToString());
                }
            return retStatus;
            }
    }
}
