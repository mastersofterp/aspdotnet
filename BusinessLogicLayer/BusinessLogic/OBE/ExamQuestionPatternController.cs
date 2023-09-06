using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;

using System.Data.SqlClient;
using System.Data;
using IITMS.UAIMS;

namespace IITMS.NITPRM.BusinessLayer.BusinessLogic
{
    public class ExamQuestionPatternController
    {
        CommonModel ObjComModel = new CommonModel();
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


        public int SaveQuestionPatternDetails(int QuestionPatternSubID, string QuestionNumber, int TotalNoQuestions, int SolveNoQuestions, int Level_Id, int Question_Pattern_ID, string QUESTION_MARKS, int Que_Or_With, string Que_Description, int Parent_Question, int MarkEntry)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;
           
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_QuestionPatternSubID", QuestionPatternSubID);
                objParams[1] = new SqlParameter("@P_QuestionNumber", QuestionNumber);
                objParams[2] = new SqlParameter("@P_Level_Id", Level_Id);
                objParams[3] = new SqlParameter("@P_No_of_question", TotalNoQuestions);
                objParams[4] = new SqlParameter("@P_Solve_no_of_question", SolveNoQuestions);
                objParams[5] = new SqlParameter("@P_Question_Pattern_ID", Question_Pattern_ID);
                objParams[6] = new SqlParameter("@P_QUESTION_MARKS", QUESTION_MARKS);
                objParams[7] = new SqlParameter("@P_Que_Or_With", Que_Or_With);
                objParams[8] = new SqlParameter("@P_Que_Description", Que_Description);
                objParams[9] = new SqlParameter("@P_Parent_Question", Parent_Question);
                objParams[10] = new SqlParameter("@P_MarkEntry", MarkEntry);
                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PATTERN_DETAILS_INSERT_UPDATE", objParams, true);//PKG_ACD_PATTERN_DETAILS_INSERT_UPDATE

                return  Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.SaveQuestionPatternDetails-> " + ex.ToString());

            }
        }

        public int SaveQuestionPattern(int QuestionPatternId, string PatternName, string MARKS)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_QuestionPatternId", QuestionPatternId);
                objParams[1] = new SqlParameter("@P_QuestionPatternName", PatternName);
                objParams[2] = new SqlParameter("@P_MARKS", MARKS);
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PATTERN_INSERT_UPDATE", objParams, true);
                return Convert.ToInt32(ret);
            }
            catch(Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.SaveQuestionPatternDetails-> " + ex.ToString());
            }
        }

        public int DeleteQuestion(int QuestionPatternSubID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_QuestionPatternSubID", QuestionPatternSubID);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_EXAM_PATTERN_QUESTION", objParams, true);
                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.DeleteSport-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetPattern()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PATTERN", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetPattern-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetQuestionPatternDetails(int SubQuePatternId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SubQuePatternId", SubQuePatternId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_QUESTION_PATTERN_DETAILS", objParams);

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamQuestionPaperController.GetQuestionPatternDetails-> " + ex.ToString());

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
    }
}
