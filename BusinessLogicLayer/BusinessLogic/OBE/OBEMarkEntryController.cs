using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Web;
using System.Collections.Generic;
using IITMS.NITPRM;
using IITMS.UAIMS;
namespace IITMS.NITPRM.BusinessLayer.BusinessLogic
{
    public class OBEMarkEntryController
    {
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

 #region Global Elective For Jecrc


  public int SaveMarkEntry_Advance(int userno, DataTable dtMarkentryData, DataTable dtTotMarkData, int LockStatus, int ExamNameId, string IpAddress, int Flag, int schemeno)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@CreatedBy", userno);
                objParams[1] = new SqlParameter("@ModifiedBy", userno);
                objParams[2] = new SqlParameter("@TblMarkEntryListData", dtMarkentryData);
                objParams[3] = new SqlParameter("@TblTotMarksListDataMapping", dtTotMarkData);
                objParams[4] = new SqlParameter("@LOCKSTATUS", LockStatus);
                objParams[5] = new SqlParameter("@EXAMNAMEID", ExamNameId);
                objParams[6] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[7] = new SqlParameter("@P_FLAG", Flag);
                objParams[8] = new SqlParameter("@P_Scheme", schemeno);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Insert_Update_CCODE_Advance_QP", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (retStatus == 3)
                {
                    retStatus = 3;
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {

            }
            return retStatus;
        }
        
        public DataSet GetSubjectData_Global(int SessionId, int UserId)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERID", Convert.ToInt32(UserId));
                objParams[1] = new SqlParameter("@P_SESSIONID", SessionId);
                ds = objSQLHelper.ExecuteDataSetSP("spTeacherSubject_Get_Global", objParams);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }


        public DataSet GetMarksEntryStudentData_GLOBAL(int QuestionPaperId, int SchemeSubjectId, int SessionId, int SectionId, int UA_NO)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[2] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@P_SECTIONID", SectionId);
                objParams[4] = new SqlParameter("@P_UANO", UA_NO);

                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsByID_Get_CCODE_GLOBAL]", objParams);
               
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet DownloadMarkEntryExcel_Global(int QuestionPaperId, int SchemeSubjectId, int SessionId, int SectionId, int userno)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[2] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@P_SectionId", SectionId);
                objParams[4] = new SqlParameter("@P_UserId", userno);

                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsWithStudentDataByID_Get_Excel_GLOBAL]", objParams);
                //  ds = objSQLHelper.ExecuteDataSetSP("spExamPaperQuestionsWithStudentDataByID_Get_Excel", objParams);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public int SaveMarkEntry_Global(int userno, DataTable dtMarkentryData, DataTable dtTotMarkData, int LockStatus, int ExamNameId, string IpAddress, int Flag, int schemeno)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@CreatedBy", userno);
                objParams[1] = new SqlParameter("@ModifiedBy", userno);
                objParams[2] = new SqlParameter("@TblMarkEntryListData", dtMarkentryData);
                objParams[3] = new SqlParameter("@TblTotMarksListDataMapping", dtTotMarkData);
                objParams[4] = new SqlParameter("@LOCKSTATUS", LockStatus);
                objParams[5] = new SqlParameter("@EXAMNAMEID", ExamNameId);
                objParams[6] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[7] = new SqlParameter("@P_FLAG", Flag);
                objParams[8] = new SqlParameter("@P_Scheme", schemeno);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Insert_Update", objParams, true); //PRC_TEST sptbltblQuestionsObtainedMarks_Insert_Update
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Insert_Update_CCODE_GLOBAL", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (retStatus == 3)
                {
                    retStatus = 3;
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {

            }
            return retStatus;
        }

        #endregion


        

        public DataSet GetSubjectData(int SessionId,int UserId)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERID", Convert.ToInt32(UserId));
                objParams[1] = new SqlParameter("@P_SESSIONID", SessionId);
                ds = objSQLHelper.ExecuteDataSetSP("spTeacherSubject_Get", objParams);

            }
            catch (Exception ex)
            {
 
            }
            return ds;
        }
        public DataSet GetMarksEntryStudentData(int QuestionPaperId, int SchemeSubjectId, int SessionId, int SectionId)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[2] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@P_SECTIONID", SectionId);
                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsByID_Get]", objParams); 
            }
            catch (Exception ex)
            {
                
            }
            return ds;
        }

        //*******************added on 18032023************************
        public DataSet GetMarksEntryStudentData(int QuestionPaperId, int SchemeSubjectId, int SessionId, int SectionId, int UA_NO)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[2] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@P_SECTIONID", SectionId);
                objParams[4] = new SqlParameter("@P_UANO", UA_NO);
                // ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsByID_Get]", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsByID_Get_CCODE]", objParams);
                //  ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsByID_Get_Createdby]", objParams);  //added on 140323
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        //*********************END************************************
        public DataSet GetLockStatus(int SessionId, int SchemeSubjectId, int QuestionPaperId, int SectionId)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            int Unlock = 0;
            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@P_SESSIONID", SessionId);
                objParam[1] = new SqlParameter("@P_SCHEMESUBJECTID", SchemeSubjectId);
                objParam[2] = new SqlParameter("@P_QUESTIONPAPERID", QuestionPaperId);
                objParam[3] = new SqlParameter("@P_SECTIONID", Convert.ToInt32(SectionId));
                ds = objSQLHelper.ExecuteDataSetSP("sp_CheckLockStatusOfExamQuestionPaper", objParam);
               
            }
            catch (Exception ex)
            {
               
            }
            return ds;
        }
        public int SaveMarkEntry(int userno, DataTable dtMarkentryData, DataTable dtTotMarkData, int LockStatus, int ExamNameId, string IpAddress, int Flag)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@CreatedBy", userno);
                objParams[1] = new SqlParameter("@ModifiedBy", userno);
                objParams[2] = new SqlParameter("@TblMarkEntryListData", dtMarkentryData);
                objParams[3] = new SqlParameter("@TblTotMarksListDataMapping", dtTotMarkData);
                objParams[4] = new SqlParameter("@LOCKSTATUS", LockStatus);
                objParams[5] = new SqlParameter("@EXAMNAMEID", ExamNameId);
                objParams[6] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[7] = new SqlParameter("@P_FLAG", Flag);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Insert_Update", objParams, true); //PRC_TEST sptbltblQuestionsObtainedMarks_Insert_Update
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (retStatus == 3)
                {
                    retStatus = 3;
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {
                
            }
            return retStatus;
        }
        public DataSet DownloadMarkEntryExcel(int QuestionPaperId, int SchemeSubjectId, int SessionId, int SectionId,int userno)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[2] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@P_SectionId", SectionId);
                objParams[4] = new SqlParameter("@P_UserId", userno);

                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsWithStudentDataByID_Get_Excel]", objParams);
              //  ds = objSQLHelper.ExecuteDataSetSP("spExamPaperQuestionsWithStudentDataByID_Get_Excel", objParams);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public int ImportExcelData(int Createdby, int ModifiedBy, string ipaddress, string macaddress, int organizationid, DataTable dtMarkentryData, DataTable dtTotMarkData,int ExamNameId)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@CreatedBy", Createdby);
                objParams[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                objParams[2] = new SqlParameter("@IPAddress", ipaddress);
                objParams[3] = new SqlParameter("@MACAddress", macaddress);
                objParams[4] = new SqlParameter("@OrganizationId", organizationid);
                objParams[5] = new SqlParameter("@TblExcelMarkentryData", dtMarkentryData);
                objParams[6] = new SqlParameter("@TblExcelTotMarkData", dtTotMarkData);
                objParams[7] = new SqlParameter("@P_QUESTIONPAPERID", ExamNameId);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Excel_Data_Import", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {

            }
            return retStatus;
        }

        public DataSet GetESEMarksEntryStudentData(int QuestionPaperId, int SchemeSubjectId, int SessionId, int SchemeId, int SemesterId)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[2] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@P_SCHEMEID", SchemeId);
                objParams[4] = new SqlParameter("@P_SEMESTERID", SemesterId);
                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[spExamPaperQuestionsByID_Get_ESE]", objParams);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public int ImportExcelDataForESE(int Createdby, int ModifiedBy, string ipaddress, string macaddress, int organizationid, DataTable dtMarkentryData, DataTable dtTotMarkData, int QuenPaperId, int ExamNameId, int LockStatus)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@CreatedBy", Createdby);
                objParams[1] = new SqlParameter("@ModifiedBy", ModifiedBy);
                objParams[2] = new SqlParameter("@IPAddress", ipaddress);
                objParams[3] = new SqlParameter("@MACAddress", macaddress);
                objParams[4] = new SqlParameter("@OrganizationId", organizationid);
                objParams[5] = new SqlParameter("@TblExcelMarkentryData", dtMarkentryData);
                objParams[6] = new SqlParameter("@TblExcelTotMarkData", dtTotMarkData);
                objParams[7] = new SqlParameter("@P_QUESTIONPAPERID", QuenPaperId);
                objParams[8] = new SqlParameter("@EXAMNAMEID", ExamNameId);
                objParams[9] = new SqlParameter("@LOCKSTATUS", LockStatus);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Excel_Data_Import_ESE", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {

            }
            return retStatus;
        }


        public int SaveFreeElectMarkEntry(int userno, DataTable dtMarkentryData, DataTable dtTotMarkData, int LockStatus, int ExamNameId, string IpAddress, int Flag, string Ccode)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@CreatedBy", userno);
                objParams[1] = new SqlParameter("@ModifiedBy", userno);
                objParams[2] = new SqlParameter("@TblMarkEntryListData", dtMarkentryData);
                objParams[3] = new SqlParameter("@TblTotMarksListDataMapping", dtTotMarkData);
                objParams[4] = new SqlParameter("@LOCKSTATUS", LockStatus);
                objParams[5] = new SqlParameter("@EXAMNAMEID", ExamNameId);
                objParams[6] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[7] = new SqlParameter("@P_FLAG", Flag);
                objParams[8] = new SqlParameter("@P_CCODE", Ccode);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblFreeElectQuestionsObtainedMarks_Insert_Update", objParams, true); //PRC_TEST sptbltblQuestionsObtainedMarks_Insert_Update
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {

            }
            return retStatus;
        }

        //Deepali//
        public DataSet GetSubjectDetails(int SchemeSubjectId, int SessionId, int SectionId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[1] = new SqlParameter("@P_SessionId", SessionId);
                objParams[2] = new SqlParameter("@P_SectionId", SectionId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUBJECT_DETAILS", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GetSubjectDetails-> " + ex.ToString());

            }
        }


        public DataSet GETLOCKCOUNT(int SCHEMENO, int COURSE, int userno)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_COURSENO", COURSE);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[2] = new SqlParameter("@P_UA_NO", userno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CHECK_SUB_EXAMS_LOCKED_OR_NOT", objParams); //PKG_ACD_CHECK_SUB_EXAMS_LOCKED_OR_NOT
                //PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GETLOCKCOUNT-> " + ex.ToString());

            }
        }

         //*****************added on 30032023*************
        public DataSet GETLOCKCOUNT(int SCHEMENO, int COURSE, int userno,int Sectionid)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_COURSENO", COURSE);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[2] = new SqlParameter("@P_UA_NO", userno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", Sectionid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CHECK_SUB_EXAMS_LOCKED_OR_NOT", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GETLOCKCOUNT-> " + ex.ToString());

            }
        }
       //***********************
         //*****************added on 26062023*************
        public DataSet GETLOCKCOUNT(int SCHEMENO, int COURSE, int userno, int Sectionid,int Sessionno)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_COURSENO", COURSE);
                objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                objParams[2] = new SqlParameter("@P_UA_NO", userno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", Sectionid);
                objParams[4] = new SqlParameter("@P_SESSIONNO", Sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CHECK_SUB_EXAMS_LOCKED_OR_NOT", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GETLOCKCOUNT-> " + ex.ToString());

            }
        }
        //***********************

        public DataSet GetExamDetailsByPaperId(int QuestionPaperId, int CourseNo, int SessionId, int UserNo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_CourseNo", CourseNo);
                objParams[2] = new SqlParameter("@P_SessionId", SessionId);
                objParams[3] = new SqlParameter("@P_UserNo", UserNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_DETAILS_BY_PAPER_ID", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GetExamDetailsByPaperId-> " + ex.ToString());

            }
        }

        public DataSet GetMarkEntryStatus(int SchemeSubjectId, int SessionId, int SectionId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[1] = new SqlParameter("@P_SessionId", SessionId);
                objParams[2] = new SqlParameter("@P_SectionId", SectionId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MARK_ENTRY_STATUS", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GetMarkEntryStatus-> " + ex.ToString());

            }
        }

        public DataSet GetPracticalMarkEntryStatus(int SessionId, int CourseNo, int SectionId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SessionId", SessionId);
                objParams[1] = new SqlParameter("@P_CourseNo", CourseNo);
                objParams[2] = new SqlParameter("@P_SectionId", SectionId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PRACTICAL_MARK_ENTRY_STATUS", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OBEMarkEntryController.GetPracticalMarkEntryStatus-> " + ex.ToString());

            }
        }

        public DataSet GetPatternData(int QuestionPatternId, int SchemeSubjectId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_QuestionPatternId", QuestionPatternId);
            objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PATTERN_DATA", objParams);

            return ds;
        }

        public DataSet GetPaperQuestionsId(int QuestionPaperId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAPER_QUESTIONS_ID", objParams);

            return ds;
        }

        public DataSet CheckSessionActivity(string UserType, string PageNo)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_UserType", UserType);
            objParams[1] = new SqlParameter("@P_PageNo", PageNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_SESSION_ACTIVITY", objParams);

            return ds;
        }

   public int SaveMarkEntry(int userno, DataTable dtMarkentryData, DataTable dtTotMarkData, int LockStatus, int ExamNameId, string IpAddress, int Flag,int schemeno)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            int retStatus = 0;
            try
            {
                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@CreatedBy", userno);
                objParams[1] = new SqlParameter("@ModifiedBy", userno);
                objParams[2] = new SqlParameter("@TblMarkEntryListData", dtMarkentryData);
                objParams[3] = new SqlParameter("@TblTotMarksListDataMapping", dtTotMarkData);
                objParams[4] = new SqlParameter("@LOCKSTATUS", LockStatus);
                objParams[5] = new SqlParameter("@EXAMNAMEID", ExamNameId);
                objParams[6] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[7] = new SqlParameter("@P_FLAG", Flag);
                objParams[8] = new SqlParameter("@P_Scheme", schemeno);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Insert_Update", objParams, true); //PRC_TEST sptbltblQuestionsObtainedMarks_Insert_Update
                object ret = objSQLHelper.ExecuteNonQuerySP("sptbltblQuestionsObtainedMarks_Insert_Update_CCODE", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (retStatus == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (retStatus == 3)
                {
                    retStatus = 3;
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {

            }
            return retStatus;
        }
 public DataSet CheckSessionActivity(string UserType, string PageNo,int Userno)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_UserType", UserType);
            objParams[1] = new SqlParameter("@P_PageNo", PageNo);
            objParams[2] = new SqlParameter("@P_UANO", Userno);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_SESSION_ACTIVITY", objParams);

            return ds;
        }

 #region added Schemeno parameter on date 06122023
 public DataSet GetSubjectData(int SessionId, int UserId, int Schemeno)
 {
     DataSet ds = new DataSet();
     SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
     try
     {
         SqlParameter[] objParams = null;

         objParams = new SqlParameter[3];
         objParams[0] = new SqlParameter("@P_USERID", Convert.ToInt32(UserId));
         objParams[1] = new SqlParameter("@P_SESSIONID", SessionId);
         objParams[2] = new SqlParameter("@P_SCHEMENO", Schemeno);
         ds = objSQLHelper.ExecuteDataSetSP("spTeacherSubject_Get", objParams);

     }
     catch (Exception ex)
     {

     }
     return ds;
 }

 #endregion

        
    }
}
