using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class StudentOnlineAdmissionMasterController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Exam Board Business Logic Methods
        //PKG_ACD_INS_UPD_EXAMBOARD //Ins Upd Oprations
        //PKG_ACD_GETALL_EXAMBOARD //Get All Board List
        //PKG_ACD_RET_EXAMBOARD    @P_BOARDNO //Get Single Board Info

        public string InsertUpdateExamBoard(int ExamBoardNo, int CountryNo, int StateNo, string BoardName, string QualifyNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_BOARDNO", ExamBoardNo);
                objParams[1] = new SqlParameter("@P_COUNTRYNO", CountryNo);
                objParams[2] = new SqlParameter("@P_STATENO", StateNo);
                objParams[3] = new SqlParameter("@P_BOARDNAME", BoardName);
                objParams[4] = new SqlParameter("@P_QUALIFYNO", QualifyNo);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_EXAMBOARD", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.UpdateExamBoard-> " + ex.ToString());
            }
        }

        public DataSet GetAllExamBoardList(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_GETALL_EXAMBOARD");

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetAllExamBoardList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleExamBoardInformation(int BoardNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_EXAMBOARD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleExamBoardInformation->" + ex.ToString());
            }
            return ds;
        }
        #endregion

        #region Subject Business Logic Methods
        public string InsertUpdateSubject(int SubjectNo, string SubjectName)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SUBJECTNO", SubjectNo);
                objParams[1] = new SqlParameter("@P_SUBJECTNAME", SubjectName);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_SUBJECT", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertUpdateSubject-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetRetAllSubjectList(int SubjectNo, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                ////SqlParameter[] objParams = null;              
                ////ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_SUBJECT", objParams);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_SUBJECTNO", SubjectNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_SUBJECT", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetRetAllSubjectList-> " + ex.ToString());
            }
            return ds;
        }

        //public string UpdateSubject__(int SubjectNo, string SubjectName)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        //Update Student
        //        objParams = new SqlParameter[3];

        //        objParams[0] = new SqlParameter("@P_SUBJECTNO", SubjectNo);
        //        objParams[1] = new SqlParameter("@P_SUBJECTNAME", SubjectName);

        //        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[2].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SUBJECT", objParams, true);

        //        //retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
        //        return ret.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.UpdateSubject-> " + ex.ToString());
        //    }
        //}
        //public DataSet GetSingleSubjectInformation__(int SubjetNo)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@P_SUBJECTNO", SubjetNo);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_SUBJECT", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetSingleSubjectInformation->" + ex.ToString());
        //    }
        //    return ds;
        //}
        #endregion

        #region Group Business Logic Methods
        public string InsertUpdateGroup(int GroupId, string GroupName)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_GROUPID", GroupId);
                objParams[1] = new SqlParameter("@P_GROUPNAME", GroupName);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_GROUP", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertUpdateGroup-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetRetAllGroupList(int GroupId, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                ////SqlParameter[] objParams = null;              
                ////ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_SUBJECT", objParams);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_GROUPID", GroupId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_GROUP", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetRetAllGroupList-> " + ex.ToString());
            }
            return ds;
        }

        //public string UpdateSubject__(int SubjectNo, string SubjectName)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = null;

        //        //Update Student
        //        objParams = new SqlParameter[3];

        //        objParams[0] = new SqlParameter("@P_SUBJECTNO", SubjectNo);
        //        objParams[1] = new SqlParameter("@P_SUBJECTNAME", SubjectName);

        //        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[2].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SUBJECT", objParams, true);

        //        //retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
        //        return ret.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.UpdateSubject-> " + ex.ToString());
        //    }
        //}
        //public DataSet GetSingleSubjectInformation__(int SubjetNo)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
        //        SqlParameter[] objParams = new SqlParameter[1];
        //        objParams[0] = new SqlParameter("@P_SUBJECTNO", SubjetNo);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_SUBJECT", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetSingleSubjectInformation->" + ex.ToString());
        //    }
        //    return ds;
        //}
        #endregion

        #region SubjectType Business Logic Methods
        public string InsertSubjectType(string SubjectType, string SubjectNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SUBJECTTYPE", SubjectType);
                objParams[1] = new SqlParameter("@P_SUBJECTNO", SubjectNo);


                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SUBJECTTYPE_TRANS", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertSubjectType-> " + ex.ToString());
            }
        }
        public string UpdateSubjectType(string SubjectType, int SubjectTypeNo, string SubjectNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_SUBJECTTYPE", SubjectType);
                objParams[1] = new SqlParameter("@P_SUBJECTTYPENO", SubjectTypeNo);
                objParams[2] = new SqlParameter("@P_SUBJECTNO", SubjectNo);

                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.NVarChar, -1);
                objParams[3].Direction = ParameterDirection.Output;

                //objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //objParams[3].Direction = ParameterDirection.Output;

                //objParams[4] = new SqlParameter("@P_OUT_SUBJECTS", SqlDbType.NVarChar,-1);
                //objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SUBJECTTYPE_TRANS", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.UpdateSubjectType-> " + ex.ToString());
            }
        }

        public DataSet GetAllSubjectTypeList(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_ALL_SUBJECTTYPE_TRANS");

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetAllSubjectTypeList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleSubjectTypeInformation(int SubjectTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SUBJECTTYPENO", SubjectTypeNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_SUBJECTTYPE_TRANS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleSubjectTypeInformation->" + ex.ToString());
            }
            return ds;

        }
        #endregion

        #region Board Subject Configuration
        public int InsertUpdateBoardSubjTypeConfig(int BoardNo, string xml)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_XMLDATA", xml);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADM_INS_UPD_BOARD_SUBJECT_CONFIG", objParams, true);
                return Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.InsertUpdateBoardSubjTypeConfig-> " + ex.ToString());
            }

        }
        #endregion

        #region Add Subjectwise Max Marks Business Logic Methods
        //PKG_ACD_GETALL_MAXMARKS_SUBJECTS @P_SUBJECTTYPENO int ---Subject ListView based on DDl SUBJECT Type
        //PKG_ACD_INS_ADM_SUBJECT_MARKS  @P_BOARDNO	INT,@P_QUALIFICATION_LEVEL INT,@P_SUBJECTTYPENO	INT,@P_XMLDATA XML,@P_OUT INT OUT 
        //PKG_ACD_UPD_ADM_SUBJECT_MARKS  @P_SUBJECT_MARKS_NO	INT,@P_XMLDATA XML,@P_OUT INT OUT
        //PKG_ACD_GETALL_ADM_SUBJECT_MARKS  Get ListView Of Fill Max Marks
        //PKG_ACD_RET_ADM_SUBJECT_MARKS @P_ORGANIZATIONID INT,@P_BOARDNO INT,@P_QUALIFICATION_LEVEL INT,@P_SUBJECTTYPENO INT

        public DataSet GetAllMaxMarksSubjectsList(int SubjectTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_SUBJECTTYPENO", SubjectTypeNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_MAXMARKS_SUBJECTS", objParams);//PKG_ACD_GET_ALL_MAXMARKS_SUBJECTS

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetAllMaxMarksSubjectsList-> " + ex.ToString());
            }
            return ds;
        }

        public int InsertSubjectwiseMaxMarks(int BoardNo, int QualificationLevel, int SubTypeNo, string xml, int countryno, int stateno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_QUALIFICATION_LEVEL", QualificationLevel);
                objParams[2] = new SqlParameter("@P_SUBJECTTYPENO", SubTypeNo);
                objParams[3] = new SqlParameter("@P_XMLDATA", xml);
                objParams[4] = new SqlParameter("@P_COUNTRYNO", countryno);
                objParams[5] = new SqlParameter("@P_STATENO", stateno);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ADM_SUBJECT_MARKS", objParams, true);
                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.AddSubjectwiseMaxMarks-> " + ex.ToString());
            }
            //return retStatus;
        }

        public int UpdateSubjectwiseMaxMarksSubjects(int BoardNo, int QualificationLevel, int SubTypeNo, string xml, int countryno, int stateno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_QUALIFICATION_LEVEL", QualificationLevel);
                objParams[2] = new SqlParameter("@P_SUBJECTTYPENO", SubTypeNo);
                objParams[3] = new SqlParameter("@P_XMLDATA", xml);
                objParams[4] = new SqlParameter("@P_COUNTRYNO", countryno);
                objParams[5] = new SqlParameter("@P_STATENO", stateno);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADM_SUBJECT_MARKS", objParams, true);

                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.UpdateSubjectwiseMaxMarksSubjects-> " + ex.ToString());
            }
            //return retStatus;
        }

        public DataSet GetAllSubjectwiseMaxMarksList(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_GETALL_ADM_SUBJECT_MARKS");

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetAllSubjectwiseMaxMarksList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleSubjectwiseMaxMarksInformation(int BoardNo, int QualificationLevel, int SubTypeno, int countryno, int stateno)
        {
            //int OrganizationID,
            DataSet ds = null;
            try
            {
                //@P_ORGANIZATIONID INT,@P_BOARDNO INT,@P_QUALIFICATION_LEVEL INT,@P_SUBJECTTYPENO INT
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                //objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrganizationID);
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_QUALIFICATION_LEVEL", QualificationLevel);
                objParams[2] = new SqlParameter("@P_SUBJECTTYPENO", SubTypeno);
                objParams[3] = new SqlParameter("@P_COUNTRYNO", countryno);
                objParams[4] = new SqlParameter("@P_STATENO", stateno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ADM_SUBJECT_MARKS", objParams);
            }
            catch (Exception ex)
            {
                // throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleSubjectwiseMaxMarksInformation->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetBoard(int countryno, int stateid, int qualifyno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_COUNTRYNO", countryno),  
                         new SqlParameter("@P_STATE", stateid),         
                         new SqlParameter("@P_QUALIFYNO", qualifyno),         
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_GET_BOARD_RAJAGIRI", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetBoard() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        #endregion

        #region Board Grade Scheme Business Logic Methods
        public int InsertBoardGradeScheme(int BoardNo, int QualificationLevel, string xml, int countryno, int stateno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_QUALIFICATION_LEVEL", QualificationLevel);
                objParams[2] = new SqlParameter("@P_XMLDATA", xml);
                objParams[3] = new SqlParameter("@P_COUNTRYNO", countryno);
                objParams[4] = new SqlParameter("@P_STATENO", stateno);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ADM_GRADE_POINT", objParams, true);
                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.InsertBoardGradeScheme-> " + ex.ToString());
            }
            //return retStatus;
        }

        public int UpdateBoardGradeScheme(int BoardNo, int QualificationLevel, string xml, int countryno, int stateno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_QUALIFICATION_LEVEL", QualificationLevel);
                objParams[2] = new SqlParameter("@P_XMLDATA", xml);
                objParams[3] = new SqlParameter("@P_COUNTRYNO", countryno);
                objParams[4] = new SqlParameter("@P_STATENO", stateno);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADM_GRADE_POINT", objParams, true);

                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.UpdateBoardGradeScheme-> " + ex.ToString());
            }
            //return retStatus;
        }

        public DataSet GetAllBoardGradeSchemeList(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_GETALL_ADM_GRADE_POINT");

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetAllBoardGradeSchemeList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleBoardGradeSchemeInformation(int BoardNo, int QualificationLevel, int countryno, int stateno)
        {
            //int OrganizationID,
            DataSet ds = null;
            try
            {
                //@P_ORGANIZATIONID INT,@P_BOARDNO INT,@P_QUALIFICATION_LEVEL INT,@P_SUBJECTTYPENO INT
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                //objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrganizationID);
                objParams[0] = new SqlParameter("@P_BOARDNO", BoardNo);
                objParams[1] = new SqlParameter("@P_QUALIFICATION_LEVEL", QualificationLevel);
                objParams[2] = new SqlParameter("@P_COUNTRYNO", countryno);
                objParams[3] = new SqlParameter("@P_STATENO", stateno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ADM_GRADE_POINT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleBoardGradeSchemeInformation->" + ex.ToString());
            }
            return ds;
        }

        #endregion

        #region Reservation Configuration Logic Methods
        public string InsertReservationConfig(int DegreeNo, int BranchNo, string ReservationIds, DateTime StartDate, DateTime EndDate)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_DEGREE_NO", DegreeNo);
                objParams[1] = new SqlParameter("@P_BRANCH_NO", BranchNo);
                objParams[2] = new SqlParameter("@P_RESERVATION_ID", ReservationIds);

                objParams[3] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[4] = new SqlParameter("@P_ENDDATE", EndDate);

                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_INS_RESERVATION_CONFIG", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertReservationConfig-> " + ex.ToString());
            }
        }
        public string UpdateReservationConfig(int DegreeNo, int BranchNo, string ReservationIds, DateTime StartDate, DateTime EndDate)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_DEGREE_NO", DegreeNo);
                objParams[1] = new SqlParameter("@P_BRANCH_NO", BranchNo);
                objParams[2] = new SqlParameter("@P_RESERVATION_ID", ReservationIds);

                objParams[3] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[4] = new SqlParameter("@P_ENDDATE", EndDate);

                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.NVarChar, -1);
                objParams[5].Direction = ParameterDirection.Output;

                //objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //objParams[3].Direction = ParameterDirection.Output;

                //objParams[4] = new SqlParameter("@P_OUT_SUBJECTS", SqlDbType.NVarChar,-1);
                //objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_UPD_RESERVATION_CONFIG", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.UpdateReservationConfig-> " + ex.ToString());
            }
        }

        public DataSet GetAllReservationConfigList(int DegreeNo, int BranchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_DEGREE_NO", DegreeNo);
                objParams[1] = new SqlParameter("@P_BRANCH_NO", BranchNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_ALL_RESERVATION_CONFIG", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetAllReservationConfigList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleReservationConfigInformation(int DegreeNo, int BranchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_DEGREE_NO", DegreeNo);
                objParams[1] = new SqlParameter("@P_BRANCH_NO", BranchNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_RET_RESERVATION_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleReservationConfigInformation->" + ex.ToString());
            }
            return ds;

        }
        #endregion



        #region Qualifying Degree Logic Methods
        public string InsertUpdateQualDegree(int QualDegreeId, string QualDegreeName, bool STATUS, int USERNO)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_QUALI_DEGREE_ID", QualDegreeId);
                objParams[1] = new SqlParameter("@P_QUALI_DEGREE_NAME", QualDegreeName);
                objParams[2] = new SqlParameter("@P_QUALI_DEGREE_STATUS", STATUS);
                objParams[3] = new SqlParameter("@P_USERNO", USERNO);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_QUALIDEGREE", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertUpdateQualDegree-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetRetAllQualDegreeList(int QualDegreeId, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_QUALI_DEGREE_ID", QualDegreeId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_QUALIDEGREE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetRetAllQualDegreeList-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

        #region Program Logic Methods
        public string InsertUpdateProgram(int ProgId, string ProgType, string QualDegree, string ProgCode, string ProgName, bool STATUS, int USERNO)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_PROG_ID", ProgId);
                objParams[1] = new SqlParameter("@P_QUALI_DEGREE_ID", QualDegree);
                objParams[2] = new SqlParameter("@P_PROG_TYPE", ProgType);
                objParams[3] = new SqlParameter("@P_PROG_CODE", ProgCode);
                objParams[4] = new SqlParameter("@P_PROG_NAME", ProgName);
                objParams[5] = new SqlParameter("@P_STATUS", STATUS);
                objParams[6] = new SqlParameter("@P_USERNO", USERNO);

                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_PROGRAM", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertUpdateProgram-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetRetAllProgramList(int ProgId, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_PROG_ID", ProgId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_PROGRAM", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetRetAllProgramList-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

        #region Test Score Master Logic Methods
        public string InsertUpdateTestSccore(int ScoreId, string BatchNo, string TestName, string MaxScore, string Avg, string StdDeviation, DateTime StartDate, DateTime EndDate, bool isGATEqualify, int years, string Degreetype, bool allowTestScoreSubject, string subject1, string maxmarks1, string subject2, string maxmarks2, string subject3, string maxmarks3, string subject4, string maxmarks4, string subject5, string maxmarks5, int scoreIdSubject1, int scoreIdSubject2, int scoreIdSubject3, int scoreIdSubject4, int scoreIdSubject5, int activeStatus, int userno)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[30];

                objParams[0] = new SqlParameter("@P_SCOREID", ScoreId);
                objParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);
                objParams[2] = new SqlParameter("@P_TESTNAME", TestName);
                objParams[3] = new SqlParameter("@P_MAXSCORE", MaxScore);
                objParams[4] = new SqlParameter("@P_AVERAGE", Avg);
                objParams[5] = new SqlParameter("@P_STDDEVIATION", StdDeviation);
                objParams[6] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[7] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[8] = new SqlParameter("@P_IS_GATE_QUALIFY", isGATEqualify);
                objParams[9] = new SqlParameter("@P_NO_OF_YEARS", years);
                objParams[10] = new SqlParameter("@P_DEGREE_TYPE", Degreetype);
                objParams[11] = new SqlParameter("@P_ALLOW_TESTSCORE_SUBJECT", allowTestScoreSubject);    
                objParams[12] = new SqlParameter("@P_SUBJECT_NAME1", subject1);
                objParams[13] = new SqlParameter("@P_MAXMARKS1", maxmarks1);
                objParams[14] = new SqlParameter("@P_SUBJECT_NAME2", subject2);
                objParams[15] = new SqlParameter("@P_MAXMARKS2", maxmarks2);
                objParams[16] = new SqlParameter("@P_SUBJECT_NAME3", subject3);
                objParams[17] = new SqlParameter("@P_MAXMARKS3", maxmarks3);
                objParams[18] = new SqlParameter("@P_SUBJECT_NAME4", subject4);
                objParams[19] = new SqlParameter("@P_MAXMARKS4", maxmarks4);
                objParams[20] = new SqlParameter("@P_SUBJECT_NAME5", subject5);
                objParams[21] = new SqlParameter("@P_MAXMARKS5", maxmarks5);
                objParams[22] = new SqlParameter("@P_SCOREIDSUBJECT1", scoreIdSubject1);
                objParams[23] = new SqlParameter("@P_SCOREIDSUBJECT2", scoreIdSubject2);
                objParams[24] = new SqlParameter("@P_SCOREIDSUBJECT3", scoreIdSubject3);
                objParams[25] = new SqlParameter("@P_SCOREIDSUBJECT4", scoreIdSubject4);
                objParams[26] = new SqlParameter("@P_SCOREIDSUBJECT5", scoreIdSubject5);
                objParams[27] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
                objParams[28] = new SqlParameter("@P_USERNO", userno);

                objParams[29] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[29].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_INS_UPD_MST_TESTSCORE", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertUpdateTestSccore-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetRetAllTestSccoreList(int ScoreId, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_SCOREID", ScoreId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_RET_ALL_MST_TESTSCORE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetRetAllTestSccoreList-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

        #region Gate-Non Gate Master Logic Methods
        public string InsertUpdateGate_NonGate(string GATE_SUB_CODE, bool IS_GATE_QUALIFY,
            string DEGREENO, string BRANCHNO, string QUALI_DEGREE_ID, string PROG_ID, int USERNO)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                /*
                    @P_GATE_SUB_CODE NVARCHAR(10)=NULL,
	                @P_IS_GATE_QUALIFY BIT,
	                @P_DEGREENO INT=NULL,
	                @P_BRANCHNO INT=NULL,
	                @P_QUALI_DEGREE_ID INT=NULL,
	                @P_PROG_ID  INT=NULL,   
                */

                objParams = new SqlParameter[8];

                ////objParams[0] = new SqlParameter("@P_GATEID", GATEID);
                objParams[0] = new SqlParameter("@P_GATE_SUB_CODE", GATE_SUB_CODE);
                objParams[1] = new SqlParameter("@P_IS_GATE_QUALIFY", IS_GATE_QUALIFY);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_QUALI_DEGREE_ID", QUALI_DEGREE_ID);
                objParams[5] = new SqlParameter("@P_PROG_ID", PROG_ID);
                objParams[6] = new SqlParameter("@P_USERNO", USERNO);

                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_GATE_NONGATE_DEGREE_BRANCH_MAPPING", objParams, true);

                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.InsertUpdateGate_NonGate-> " + ex.ToString());
            }

            return retStatus;
        }
        public int DeleteGate_NonGate(string GATE_SUB_CODE, bool IS_GATE_QUALIFY,
            string DEGREENO, string BRANCHNO, string QUALI_DEGREE_ID, string PROG_ID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            /*
                            	@P_GATE_SUB_CODE NVARCHAR(10)=NULL,
	                            @P_IS_GATE_QUALIFY BIT,
	                            @P_DEGREENO INT=NULL,
	                            @P_BRANCHNO INT=NULL,
	                            @P_QUALI_DEGREE_ID INT=NULL,
	                            @P_PROG_ID  INT=NULL,
                            */

                            new SqlParameter("@P_GATE_SUB_CODE",GATE_SUB_CODE),
                            new SqlParameter("@P_IS_GATE_QUALIFY",IS_GATE_QUALIFY),
                            new SqlParameter("@P_DEGREENO",DEGREENO),
                            new SqlParameter("@P_BRANCHNO",BRANCHNO),
                            new SqlParameter("@P_QUALI_DEGREE_ID",QUALI_DEGREE_ID),
                            new SqlParameter("@P_PROG_ID",PROG_ID),

                            new SqlParameter("@P_OUT",SqlDbType.Int),
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_GATE_NONGATE_DEGREE_BRANCH_MAPPING", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.DeleteGate_NonGate-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAll_GATE_DataList(string GATE_SUB_CODE, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_GATE_SUB_CODE", GATE_SUB_CODE);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_GATE_DEGREE_BRANCH_MAPPING", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetAll_GATE_DataList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAll_Non_GATE_DataList(string DEGREENO, string BRANCHNO, string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[2];

                objParams[0] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_NONGATE_QUALI_DEGREE_PROGRAM_MAPPING", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.StudentOnlineAdmissionMasterController.GetAll_Non_GATE_DataList-> " + ex.ToString());
            }
            return ds;
        }
        #endregion
    }
}


//PKG_ACD_INS_UPD_EXAMBOARD
//PKG_ACD_GETALL_EXAMBOARD 
//PKG_ACD_RET_EXAMBOARD
//PKG_ACD_GETALL_MAXMARKS_SUBJECTS
//PKG_ACD_INS_ADM_SUBJECT_MARKS  
//PKG_ACD_UPD_ADM_SUBJECT_MARKS  
//PKG_ACD_GETALL_ADM_SUBJECT_MARKS  
//PKG_ACD_RET_ADM_SUBJECT_MARKS
//PKG_ACD_RET_ADM_SUBJECT_MARKS

//##########
//PKG_ACD_INSERT_SUBJECT
//PKG_ACD_UPDATE_SUBJECT
//PKG_ACD_DELETE_SUBJECT
//PKG_ACD_ALL_SUBJECT
//PKG_ACD_RET_SUBJECT
