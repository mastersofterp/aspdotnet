using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

//using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class QuestionPatternFacultyMasterController
    {
        
        //CommonModel ObjComModel = new CommonModel();
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
       


        // Modified by swapnil thakare on dated 04-05-2021

        public DataSet BindSchemeMappingDropdown(int userid,int sessionno)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] objParams = null;
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@UserId", userid);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("GET_DROPDOWN_DATA_CO_OBE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.COController.BindSchemeMappingDropdown-> " + ex.ToString());

            }
            return ds;
        }


        // Added by swapnil thakare on dated 06-05-2021

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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.COController.BindSchemeMappingDropdown-> " + ex.ToString());

            }
            return ds;
        }

       

        public DataSet GetQuestionPatternMasterData(int SchemeMapId)
        {
            try
            {
                DataSet ds = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_OrganizationId", 17);
                objParams[1] = new SqlParameter("@P_CollegeDepartmentId",1);
                objParams[2] = new SqlParameter("@P_UserTypeId", 5);
                objParams[3] = new SqlParameter("@P_UserId", 1);
                objParams[4] = new SqlParameter("@P_SchemeSubjectId", SchemeMapId);
                ds = objSQLHelper.ExecuteDataSetSP("sptblQuestionPatternMaster_GetDataForFaculty", objParams);


                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.GetQuestionPatternMasterData-> " + ex.ToString());

            }
        }


        /// <summary>
        /// Fetch Question Pattern Master Data For Updation Purpose 
        /// </summary>
        /// <param name="QuestionPatternId"></param>
        /// <returns></returns>
        public DataSet GetQuestionPatternMasterDataById(int QuestionPatternId)
        {
            DataSet ds = new DataSet();
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                QuestionPatternMasterModel objQPM = new QuestionPatternMasterModel();
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_QuestionPatternId", QuestionPatternId);
                ds = objSQLHelper.ExecuteDataSetSP("sptblQuestionPatternMaster_GetDataForFaculty", objParams);

               

                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.GetQuestionPatternMasterDataById-> " + ex.ToString());

            }
        }


        public int CheckQuestionPatternForEdit(int QuestionPatternId)
        {
            object ret = 0;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@UserId", 1);
                objParams[1] = new SqlParameter("@PatternId", QuestionPatternId);
                objParams[2] = new SqlParameter("@POut", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("SpCheckExamQuestionPattern", objParams, true);

                return  Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.CheckQuestionPatternForEdit-> " + ex.ToString());

            }
        }

        /// <summary>
        /// Save Question Pattern Master Detail Data.
        /// </summary>
        /// <param name="PEOs"></param>
        /// <returns></returns>
        public int SaveQuestionPatternDetailsAndMaster(int QuestionPatternId, string QuestionPatternName, int TotalNoQuestions, int SolveNoQuestions,int ActiveStatus,int CollegeDepartmentId,int SchemeId,int SchemeSubjectId,DataTable SubQuestionsDetails)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;
           
            try
            {
               

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_QuestionPatternId", QuestionPatternId);
                objParams[1] = new SqlParameter("@P_QuestionPatternName", QuestionPatternName);
                objParams[2] = new SqlParameter("@P_TotalNoQuestions", TotalNoQuestions);
                objParams[3] = new SqlParameter("@P_SolveNoQuestions", SolveNoQuestions);
                objParams[4] = new SqlParameter("@P_ActiveStatus", ActiveStatus);
                objParams[5] = new SqlParameter("@P_IPAddress", 1);
                objParams[6] = new SqlParameter("@P_CreatedBy", 1);
                objParams[7] = new SqlParameter("@P_ModifiedBy", 1);
                objParams[8] = new SqlParameter("@P_MACAddress",1);
                objParams[9] = new SqlParameter("@P_OrganizationId", 1);
                objParams[10] = new SqlParameter("@P_CollegeDepartmentId", CollegeDepartmentId);
                objParams[11] = new SqlParameter("@P_SchemeId", SchemeId);
                objParams[12] = new SqlParameter("@P_SchemeSubjectId", SchemeSubjectId);
                objParams[13] = new SqlParameter("@P_SubQuestionsDetails", SubQuestionsDetails);
                objParams[14] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("sptblQuestionPatternDetailsMaster_Insert_Update", objParams, true);

                return  Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.SaveQuestionPatternMaster-> " + ex.ToString());

            }
        }


        public int SaveQuestionPatternMaster(QuestionPatternMasterModel objQPM)
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            object ret = 0;

            try
            {


                SqlParameter[] objParams = null;
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_QuestionPatternId", objQPM.QuestionPatternId);
                objParams[1] = new SqlParameter("@P_QuestionPatternName", objQPM.QuestionPatternName);
                objParams[2] = new SqlParameter("@P_TotalNoQuestions", objQPM.TotalNoQuestions);
                objParams[3] = new SqlParameter("@P_SolveNoQuestions", objQPM.SolveNoQuestions);
                objParams[4] = new SqlParameter("@P_ActiveStatus", objQPM.ActiveStatus);
                objParams[5] = new SqlParameter("@P_IPAddress", 1);
                objParams[6] = new SqlParameter("@P_CreatedBy", 1);
                objParams[7] = new SqlParameter("@P_ModifiedBy", 1);
                objParams[8] = new SqlParameter("@P_MACAddress", 1);
                objParams[9] = new SqlParameter("@P_OrganizationId", 1);
                objParams[10] = new SqlParameter("@P_CollegeDepartmentId", objQPM.CollegeDepartmentId);
                objParams[11] = new SqlParameter("@P_SchemeId", objQPM.SchemeId);
                objParams[12] = new SqlParameter("@P_SchemeSubjectId", objQPM.SchemeSubjectId);
                objParams[13] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("sptblQuestionPatternMaster_Insert_Update", objParams, true);

                return Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.POController.SaveQuestionPatternMaster-> " + ex.ToString());

            }
        }
    }
}
