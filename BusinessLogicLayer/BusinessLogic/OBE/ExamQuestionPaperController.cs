using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Newtonsoft.Json;


using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
//using BusinessLogicLayer.BusinessEntities.OBE;

namespace IITMS.NITPRM.BusinessLayer.BusinessLogic
{
   public class ExamQuestionPaperController
    {
       CommonModel ObjComModel = new CommonModel();
       CommanController ObjComm = new CommanController();
       string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
       public int QuestionPaper_Unlock(string QuestionPaperId)
       {
           SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
           object ret = 0;
           try
           {

               SqlParameter[] objParams = null;
               //Changing Parameters for each form.
               objParams = new SqlParameter[2];
               objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
               objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
               objParams[1].Direction = ParameterDirection.Output;
               ret = objSQLHelper.ExecuteNonQuerySP("sptblExamQuestionPaper_Unlock", objParams, true);
               return Convert.ToInt32(ret);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.QuestionPaper_Unlock-> " + ex.ToString());

           }
       }
       //added on 02112022 
       public DataSet CHECKCOURSE(int SessionId, int UserId)
       {
           try
           {
               DataSet ds = new DataSet();
               SqlParameter[] objParams = null;
               SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

               objParams = new SqlParameter[2];
               objParams[0] = new SqlParameter("@P_SESSIONID", SessionId);
               objParams[1] = new SqlParameter("@P_USERID", UserId);

               ds = objSQLHelper.ExecuteDataSetSP("SP_TOCHECK_COURSE_NAME", objParams);

               


               return ds;
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.CHECKCOURSE-> " + ex.ToString());

           }
       }
       //end 
        public DataSet GetSubjectData(int SessionId, int UserId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[1] = new SqlParameter("@P_USERID", UserId);

                ds = objSQLHelper.ExecuteDataSetSP("spSchemeSubjectMapping_ExamPatternMapping_Get", objParams);

                var lstSubjectDetail = (from DataRow dr in ds.Tables[0].Rows
                                        select new ExamQuestionPaperModel
                                        {
                                            SchemeSubjectId = int.Parse(dr["SchemeSubjectId"].ToString()),
                                            SchemeMappingName = (dr["SchemeMappingName"].ToString()),
                                            SectionId = Convert.ToInt32(dr["SectionId"] == DBNull.Value ? 0 : dr["SectionId"]),         //  Added on 29-01-2020
                                            SectionName = dr["SectionName"] == null ? "" : dr["SectionName"].ToString(),                //  Added on 29-01-2020
                                            ExamPatternMappingId = int.Parse(dr["ExamPatternMappingId"].ToString()),
                                            srno = int.Parse(dr["srno"].ToString()),
                                            Status = (dr["Status"].ToString()),
                                            ExamName = (dr["ExamName"].ToString()),
                                            QuestionPaperId = Convert.ToInt32(dr["QuestionPaperId"].ToString()),
                                            FilePath = Convert.ToString(dr["DocumentName"])
                                        }).ToList();


                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetSubjectData-> " + ex.ToString());

            }
        }


        public DataSet GetSubjectData(int SessionId, int UserId,int Courseno)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONID", SessionId);
                objParams[1] = new SqlParameter("@P_USERID", UserId);
                objParams[2] = new SqlParameter("@P_COURSENO", Courseno);

                ds = objSQLHelper.ExecuteDataSetSP("spSchemeSubjectMapping_ExamPatternMapping_Get", objParams);

                var lstSubjectDetail = (from DataRow dr in ds.Tables[0].Rows
                                        select new ExamQuestionPaperModel
                                        {
                                            SchemeSubjectId = int.Parse(dr["SchemeSubjectId"].ToString()),
                                            SchemeMappingName = (dr["SchemeMappingName"].ToString()),
                                            SectionId = Convert.ToInt32(dr["SectionId"] == DBNull.Value ? 0 : dr["SectionId"]),         //  Added on 29-01-2020
                                            SectionName = dr["SectionName"] == null ? "" : dr["SectionName"].ToString(),                //  Added on 29-01-2020
                                            ExamPatternMappingId = int.Parse(dr["ExamPatternMappingId"].ToString()),
                                            srno = int.Parse(dr["srno"].ToString()),
                                            Status = (dr["Status"].ToString()),
                                            ExamName = (dr["ExamName"].ToString()),
                                            QuestionPaperId = Convert.ToInt32(dr["QuestionPaperId"].ToString()),
                                            FilePath = Convert.ToString(dr["DocumentName"])
                                        }).ToList();


                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetSubjectData-> " + ex.ToString());

            }
        }

        public DataSet CheckSessionData(int userno)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@P_Userno", userno);


            ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_SESSION_DATA", objParams);

            return ds;
        }

        public DataSet BindDropDown()
        {
            DataSet ds = new DataSet();
            try
            {
                MultiDrodown multiDropdown = new MultiDrodown();
                List<CommonModel> lstSessionMaster = ObjComm.FillDropDownOnly("tblAcdSessionMaster", "SessionId", "SessionName", "isnull(ActiveStatus,0)=1", "SessionId DESC");
                // List<CommonModel> lstSchemeMapping = ObjComm.FillDropDownOnly("tblAcdSchemeMapping", "SchemeMappingId", "SchemeMappingName", "isnull(ActiveStatus,0)=1 ", "SchemeMappingName");
                List<CommonModel> lstSchemeMapping = ObjComm.FillDropDownOnly("tblAcdSchemeSubjectMapping", "SchemeSubjectId", "SchemeMappingName", "isnull(ActiveStatus,0)=1 ", "SchemeMappingName");
                List<CommonModel> lstExamNameMaster = ObjComm.FillDropDownOnly("tblExamNameMaster", "ExamNameId", "ExamName", "isnull(ActiveStatus,0)=1 ", "ExamName");

                multiDropdown.lstDropDownList = lstSessionMaster;
                multiDropdown.IIDropDownList = lstSchemeMapping;
                multiDropdown.IIIDropDownList = lstExamNameMaster;
                return ds;
            }
            catch (Exception ex)
            {
                //string Msg, Desc;
                //Msg = ex.Message;
                //Desc = ex.StackTrace;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.BindDropDown-> " + ex.ToString());
            }
        }
        public int GetExamDetails(int SchemeSubjectId)
        {
            int ret = 0;
            DataSet ds = new DataSet();
            try
            {

                SqlParameter[] objPara = new SqlParameter[2];
                objPara[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objPara[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objPara[1].Direction = ParameterDirection.Output;

                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("spCHECK_CO_COPOMapping_DoneOrNot", objPara, true));
                if ((int)ret == 2 || (int)ret == 3)
                {
                    return Convert.ToInt32(ret);
                }
               
                return ret;
               
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetExamDetails-> " + ex.ToString());

            }
        }

        public DataSet GetPatternDataForMapping(int QuestionPatternId, int SchemeSubjectId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_QuestionPatternId", QuestionPatternId);
            objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PATTERN_DATA_FOR_MAPPING", objParams);  

            return ds;
        }

        public DataSet GetExamData(int OrganizationId, int SessionId, int SchemeSubjectId, int ExamPatternMappingId, int SectionId)
        {
            ExamQuestionPaperModel exmquestionm = new ExamQuestionPaperModel();
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@OrganizationId", OrganizationId);
            objParams[1] = new SqlParameter("@SESSIONID", SessionId);
            objParams[2] = new SqlParameter("@SchemeSubjectId", SchemeSubjectId);
            objParams[3] = new SqlParameter("@ExamPatternMappingId", ExamPatternMappingId);
            objParams[4] = new SqlParameter("@SectionId", SectionId);
            ds = objSQLHelper.ExecuteDataSetSP("sptblExamQuestionPaper_Get", objParams);

            return ds;
        }

         //<summary>
         //Bind Question Pattern Master Table
         //</summary>
         //<returns></returns>
        public int GetExamQuestion(ExamQuestionPaperModel queid)
        {
            int totnque = 0;
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;

            objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_QuestionPatternId", queid.QuestionPatternId);
            objParams[1] = new SqlParameter("@P_SchemeSubjectId", queid.SchemeSubjectId);
            objParams[2] = new SqlParameter("@ExamPatternMappingId", queid.ExamPatternMappingId);
            objParams[3] = new SqlParameter("@SESSIONID", queid.SessionId);
           
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
           
            totnque = Convert.ToInt32(objSQLHelper.ExecuteDataSetSP("sptblQuestionPatternMaster_GetTotalQuestionData", objParams));
            return totnque;
            
        }
        public int SaveQuestionPaper(string QuePaperName, int SchemeSubId, int ExamPatMappId, int SessionId, double TotalMaxMrk, string Desc, Boolean islock, Boolean activeStat, int UANO, int SectionId, DataTable dtExamPaperQuestionsData, DataTable dtQuestionsCOMapping, DataTable dtQuestionsBlooMapping, int OrgID)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;
            try
            {

                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[18];
                objParams[0] = new SqlParameter("@QuestionPaperName", QuePaperName.Replace('$', '&').ToString());  // objSM.QuestionPaperName
                objParams[1] = new SqlParameter("@SchemeSubjectId", SchemeSubId);
                objParams[2] = new SqlParameter("@ExamPatternMappingId", ExamPatMappId);
                objParams[3] = new SqlParameter("@SessionId", SessionId);
                objParams[4] = new SqlParameter("@TotalMaxMarks", TotalMaxMrk);
                objParams[5] = new SqlParameter("@Description", Desc);
                objParams[6] = new SqlParameter("@IsLock", islock);
                objParams[7] = new SqlParameter("@ActiveStatus", activeStat);
                objParams[8] = new SqlParameter("@CreatedBy", UANO);
                objParams[9] = new SqlParameter("@ModifiedBy", "");
                objParams[10] = new SqlParameter("@IPAddress", 1);
                objParams[11] = new SqlParameter("@MACAddress", 1);
                objParams[12] = new SqlParameter("@OrganizationId", Convert.ToInt32(OrgID));
                objParams[13] = new SqlParameter("@TblExamPaperQuestionsData", dtExamPaperQuestionsData);
                objParams[14] = new SqlParameter("@TbldtQuestionsCOMapping", dtQuestionsCOMapping);
                objParams[15] = new SqlParameter("@TbldtQuestionsBloomMapping", dtQuestionsBlooMapping);
                objParams[16] = new SqlParameter("@SectionId", SectionId);                             // Added on 29-014-2020
                objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[17].Direction = ParameterDirection.Output;

                //ret = objSQLHelper.ExecuteNonQuerySP("sptblExamQuestionPaper_Insert_Update", objParams, true);

                ret = objSQLHelper.ExecuteNonQuerySP("sptblExamQuestionPaper_Insert_Update", objParams, true); //sptblExamQuestionPaper_Insert_Update_Farheen
                //sptblExamQuestionPaper_Insert_Update_SVCE_DEMO,sptblExamQuestionPaper_Insert_Update  //sptblExamQuestionPaper_Insert_Update_SVCE_DEMO
                return Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.SaveQuestionPaper-> " + ex.ToString());

            }
        }

        public DataSet GetExamQuestionForEdit(int SchemeSubjectId, int ExamPatternMappingId, int SessionId, string GroupIds, int SectionId)
        {
           // AExamQuestionPaperModelList data = new AExamQuestionPaperModelList();
            try
            {

                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[7];
                //objParams[0] = new SqlParameter("@P_QuestionPatternId", obj.QuestionPatternId);
                objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[1] = new SqlParameter("@ExamPatternMappingId", ExamPatternMappingId);
                objParams[2] = new SqlParameter("@SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@GroupIds", GroupIds);
                objParams[4] = new SqlParameter("@CollegeId", 1);
                objParams[5] = new SqlParameter("@OrganizationId", 17);
                objParams[6] = new SqlParameter("@SectionId", SectionId);
                // Added on 29-014-2020

                ds = objSQLHelper.ExecuteDataSetSP("sptblQuestionPatternMaster_GetTotalQuestionData_Edit", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetExamQuestionForEdit-> " + ex.ToString());

            }
        }

        public DataSet GetExamQuestionForEdit(int SchemeSubjectId, int ExamPatternMappingId, int SessionId, int GroupIds, int SectionId, int OrgID)
        {
           // AExamQuestionPaperModelList data = new AExamQuestionPaperModelList();
            try
            {

                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[7];
                //objParams[0] = new SqlParameter("@P_QuestionPatternId", obj.QuestionPatternId);
                objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[1] = new SqlParameter("@ExamPatternMappingId", ExamPatternMappingId);
                objParams[2] = new SqlParameter("@SESSIONID", SessionId);
                objParams[3] = new SqlParameter("@GroupIds", GroupIds);
                objParams[4] = new SqlParameter("@CollegeId", 1);
                objParams[5] = new SqlParameter("@OrganizationId", OrgID);
                objParams[6] = new SqlParameter("@SectionId", SectionId);
                // Added on 29-014-2020

                ds = objSQLHelper.ExecuteDataSetSP("sptblQuestionPatternMaster_GetTotalQuestionData_Edit", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetExamQuestionForEdit-> " + ex.ToString());

            }
        }

        public DataSet GetExamQuestionPaperData(int QuestionPaperId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@P_QUESTION_PAPER_ID", QuestionPaperId);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PAPER_DATA", objParams);  

            return ds;


        }

        public DataSet FillQuestionPatternDropDown(string ncount, int SchemeSubjectId)
        {
            DataSet ds = new DataSet();
            try
            {


                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_NCOUNT", ncount);
                objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);

                ds = objSQLHelper.ExecuteDataSetSP("sptblQuestionPatternMaster_Get", objParams);
                //ds = objSQLHelper.ExecuteDataSetSP("spSchemeSubjectMapping_ExamPatternMapping_Get", objParams);
                //return DataSet(ObjComm.FillDropDownOnly("tblQuestionPatternMaster", "QuestionPatternId", "QuestionPatternName", "isnull(ActiveStatus,0)=1 AND QuestionPatternId NOT IN(" + ncount + ") AND OrganizationId=" + Session["OrganizationId"] + "And CollegeDepartmentId=" + Session["CollegeDepartmentId"] + " AND SchemeSubjectid=" + SchemeSubjectId, "QuestionPatternName"));
                //return DataSet(ObjComm.FillDropDownOnly("tblQuestionPatternMaster", "QuestionPatternId", "QuestionPatternName", "isnull(ActiveStatus,0)=1 AND QuestionPatternId NOT IN(" + ncount + ") AND OrganizationId=" + Session["OrganizationId"] + "AND SchemeSubjectid=" + SchemeSubjectId, "QuestionPatternName"));
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.FillQuestionPatternDropDown-> " + ex.ToString());

            }
            return ds;
        }

        public DataSet GetSubCO(int SchemeSubjectId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUB_CO", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetSubCO-> " + ex.ToString());

            }
        }

        public DataSet GetBloomCategory()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BLOOM_CATEGORY", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetBloomCategory-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetQuestionPaperId(int SchemeSubjectId, int ExamPatternMappingId, int SectionId, int SessionId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[4];

            objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
            objParams[1] = new SqlParameter("@P_ExamPatternMappingId", ExamPatternMappingId);
            objParams[2] = new SqlParameter("@P_SectionId", SectionId);
            objParams[3] = new SqlParameter("@P_SessionId", SessionId);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PAPER_ID", objParams);

            return ds;
        }

        public DataSet GetQuestionPaperDetails(int SchemeSubjectId, int ExamPatternMappingId, int SectionId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
            objParams[1] = new SqlParameter("@P_ExamPatternMappingId", ExamPatternMappingId);
            objParams[2] = new SqlParameter("@P_SectionId", SectionId);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PAPER_DETAILS", objParams);

            return ds;
        }


 //added on 02032023
       public DataSet BindExamName(String Coursecode, int SchemeSubjectId,int Section,int userno,int sessionno)
       {
           try
           {
               DataSet ds = new DataSet();
               SqlParameter[] objParams = null;
               SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

               objParams = new SqlParameter[5];
               objParams[0] = new SqlParameter("@P_CCODE", Coursecode);
               objParams[1] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
               objParams[2] = new SqlParameter("@P_Section", Section);
               objParams[3] = new SqlParameter("@P_UANO", userno);
               objParams[4] = new SqlParameter("@P_sessionno", sessionno);
               ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAMNAME_DROPDOWN", objParams);
               return ds;
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.BindExamName-> " + ex.ToString());

           }
       }
       //end


 public DataSet GetQuestionPaperDetails(int SchemeSubjectId, int ExamPatternMappingId, int SectionId,int Sessionno)
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            objParams = new SqlParameter[4];

            objParams[0] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
            objParams[1] = new SqlParameter("@P_ExamPatternMappingId", ExamPatternMappingId);
            objParams[2] = new SqlParameter("@P_SectionId", SectionId);
            objParams[3] = new SqlParameter("@P_Sessionno", Sessionno);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PAPER_DETAILS", objParams);
            // ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PAPER_DETAILS_CCODE", objParams);

            return ds;
        }
         public int SaveQuestionPaper(string QuePaperName, int SchemeSubId, int ExamPatMappId, int SessionId, double TotalMaxMrk, string Desc, Boolean islock, Boolean activeStat, int UANO, int SectionId, DataTable dtExamPaperQuestionsData, DataTable dtQuestionsCOMapping, DataTable dtQuestionsBlooMapping, int OrgID,int weightage,decimal intermark,decimal extermark,decimal marktot)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;
            try
            {

                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[22];
                objParams[0] = new SqlParameter("@QuestionPaperName", QuePaperName.Replace('$', '&').ToString());  // objSM.QuestionPaperName
                objParams[1] = new SqlParameter("@SchemeSubjectId", SchemeSubId);
                objParams[2] = new SqlParameter("@ExamPatternMappingId", ExamPatMappId);
                objParams[3] = new SqlParameter("@SessionId", SessionId);
                objParams[4] = new SqlParameter("@TotalMaxMarks", TotalMaxMrk);
                objParams[5] = new SqlParameter("@Description", Desc);
                objParams[6] = new SqlParameter("@IsLock", islock);
                objParams[7] = new SqlParameter("@ActiveStatus", activeStat);
                objParams[8] = new SqlParameter("@CreatedBy", UANO);
                objParams[9] = new SqlParameter("@ModifiedBy", "");
                objParams[10] = new SqlParameter("@IPAddress", 1);
                objParams[11] = new SqlParameter("@MACAddress", 1);
                objParams[12] = new SqlParameter("@OrganizationId", Convert.ToInt32(OrgID));
                objParams[13] = new SqlParameter("@TblExamPaperQuestionsData", dtExamPaperQuestionsData);
                objParams[14] = new SqlParameter("@TbldtQuestionsCOMapping", dtQuestionsCOMapping);
                objParams[15] = new SqlParameter("@TbldtQuestionsBloomMapping", dtQuestionsBlooMapping);
                objParams[16] = new SqlParameter("@weightage", weightage);
                objParams[17] = new SqlParameter("@intermark", intermark);
                objParams[18] = new SqlParameter("@extermark", extermark);
                objParams[19] = new SqlParameter("@marktot", marktot);
                objParams[20] = new SqlParameter("@SectionId", SectionId);
                objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[21].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("sptblExamQuestionPaper_Insert_Update_AssesmentComponent", objParams, true); 
                return Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.SaveQuestionPaper-> " + ex.ToString());

            }
        }

        public DataSet BindSchemeMappingGridBind(int userid)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERID", userid);

                ds = objSQLHelper.ExecuteDataSetSP("GET_EDIT_SCHEME_SUBJECT_OBE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.COController.BindSchemeMappingGridBind-> " + ex.ToString());

            }
            return ds;
        }

        #region  ADDED BY ROHIT 17_04_2024
        public int QuestionPaper_Unlock(string QuestionPaperId, int Userid)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;
            try
            {

                SqlParameter[] objParams = null;
                //Changing Parameters for each form.
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_QuestionPaperId", QuestionPaperId);
                objParams[1] = new SqlParameter("@P_USERID", Userid);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("sptblExamQuestionPaper_Unlock", objParams, true); //sptblExamQuestionPaper_Unlock
                return Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.QuestionPaper_Unlock-> " + ex.ToString());

            }
        }

        #endregion 
    }
}
