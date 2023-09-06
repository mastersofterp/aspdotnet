using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;
using System.Web.UI.WebControls;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This QuestionBankController is used to add questions to the Question Bank table.
            /// </summary>
            public class IQuestionbankController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //public string _nitprm_constr;

                //#region Constructors

                ///// <summary>
                ///// Default Constructor
                ///// </summary>
                ///// 
                //public IQuestionbankController()
                //{
                //    //blank constructor
                //}

                ///// <summary>
                ///// This constructor is used to make the connection string
                ///// </summary>
                ///// <param name="DbUserName"></param>
                ///// <param name="DbPassword"></param>
                ///// <param name="DataBase"></param>
                ///// 
                //public IQuestionbankController(string DbUserName, string DbPassword, string DataBase)
                //{
                //    _nitprm_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                //}
                //#endregion


                public int AddIQuestionBank(IQuestionbank objQuest, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[19];

                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_QUESTIONTEXT", objQuest.QUESTIONTEXT);
                        //if (objQuest.QUESTIONIMAGE == null)
                        //    objParams[2] = new SqlParameter("@P_QUESTIONIMAGE", DBNull.Value);
                        //else
                        //    objParams[2] = new SqlParameter("@P_QUESTIONIMAGE", objQuest.QUESTIONIMAGE);

                        //objParams[2].SqlDbType = SqlDbType.Image;

                        objParams[2] = new SqlParameter("@P_ANS1TEXT", objQuest.ANS1TEXT);

                        //if (objQuest.ANS1IMAGE == null)
                        //    objParams[4] = new SqlParameter("@P_ANS1IMG", DBNull.Value);
                        //else
                        //    objParams[4] = new SqlParameter("@P_ANS1IMG", objQuest.ANS1IMAGE);

                        //objParams[4].SqlDbType = SqlDbType.Image;
                        objParams[3] = new SqlParameter("@P_ANS2TEXT", objQuest.ANS2TEXT);

                        //if (objQuest.ANS2IMAGE == null)
                        //    objParams[6] = new SqlParameter("@P_ANS2IMG", DBNull.Value);
                        //else
                        //    objParams[6] = new SqlParameter("@P_ANS2IMG", objQuest.ANS2IMAGE);

                        //objParams[6].SqlDbType = SqlDbType.Image;
                        objParams[4] = new SqlParameter("@P_ANS3TEXT", objQuest.ANS3TEXT);

                        //if (objQuest.ANS3IMAGE == null)
                        //    objParams[8] = new SqlParameter("@P_ANS3IMG", DBNull.Value);
                        //else
                        //    objParams[8] = new SqlParameter("@P_ANS3IMG", objQuest.ANS3IMAGE);

                        //objParams[8].SqlDbType = SqlDbType.Image;
                        objParams[5] = new SqlParameter("@P_ANS4TEXT", objQuest.ANS4TEXT);
                        objParams[6] = new SqlParameter("@P_ANS5TEXT", objQuest.ANS5TEXT);
                        objParams[7] = new SqlParameter("@P_ANS6TEXT", objQuest.ANS6TEXT);

                        //if (objQuest.ANS4IMAGE == null)
                        //    objParams[10] = new SqlParameter("@P_ANS4IMG", DBNull.Value);
                        //else
                        //    objParams[10] = new SqlParameter("@P_ANS4IMG", objQuest.ANS4IMAGE);

                        //objParams[10].SqlDbType = SqlDbType.Image;

                        //Add New Question

                        objParams[8] = new SqlParameter("@P_CORRECTANS", objQuest.CORRECTANS);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objQuest.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                        objParams[11] = new SqlParameter("@P_TYPE", objQuest.TYPE);
                        objParams[12] = new SqlParameter("@P_TOPIC", objQuest.TOPIC);
                        objParams[13] = new SqlParameter("@P_REMARK", objQuest.REMARKS);
                        objParams[14] = new SqlParameter("@P_AUTHOR", objQuest.AUTHOR);
                        objParams[15] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);
                        objParams[16] = new SqlParameter("@P_MARKS_FOR_QUESTION", objQuest.MARKS_FOR_QUESTION);
                        objParams[17] = new SqlParameter("@P_Org_ID", OrgId);
                        objParams[18] = new SqlParameter("@P_QUESTIONNO", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_IQUESTIONBANK_INSERT", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.IAddQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateIQuestionBank(IQuestionbank objQuest, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_QUESTIONTEXT", objQuest.QUESTIONTEXT);
                        
                        //if (objQuest.QUESTIONIMAGE==null)
                        //    objParams[2] = new SqlParameter("@P_QUESTIONIMAGE", DBNull.Value);
                        //else
                        //    objParams[2] = new SqlParameter("@P_QUESTIONIMAGE", objQuest.QUESTIONIMAGE);

                        //objParams[2].SqlDbType = SqlDbType.Image;

                        objParams[2] = new SqlParameter("@P_ANS1TEXT", objQuest.ANS1TEXT);

                        //if (objQuest.ANS1IMAGE == null)
                        //    objParams[4] = new SqlParameter("@P_ANS1IMG",DBNull.Value);
                        //else
                        //    objParams[4] = new SqlParameter("@P_ANS1IMG",objQuest.ANS1IMAGE);

                        //objParams[4].SqlDbType = SqlDbType.Image;
                        objParams[3] = new SqlParameter("@P_ANS2TEXT", objQuest.ANS2TEXT);

                        //if (objQuest.ANS2IMAGE == null)
                        //    objParams[6] = new SqlParameter("@P_ANS2IMG", DBNull.Value);
                        //else
                        //    objParams[6] = new SqlParameter("@P_ANS2IMG", objQuest.ANS2IMAGE);

                        //objParams[6].SqlDbType = SqlDbType.Image;
                        objParams[4] = new SqlParameter("@P_ANS3TEXT", objQuest.ANS3TEXT);

                        //if (objQuest.ANS3IMAGE == null)
                        //    objParams[8] = new SqlParameter("@P_ANS3IMG", DBNull.Value);
                        //else
                        //    objParams[8] = new SqlParameter("@P_ANS3IMG", objQuest.ANS3IMAGE);

                        //objParams[8].SqlDbType = SqlDbType.Image;
                        objParams[5] = new SqlParameter("@P_ANS4TEXT", objQuest.ANS4TEXT);
                        objParams[6] = new SqlParameter("@P_ANS5TEXT", objQuest.ANS5TEXT);
                        objParams[7] = new SqlParameter("@P_ANS6TEXT", objQuest.ANS6TEXT);

                        //if (objQuest.ANS4IMAGE == null)
                        //    objParams[10] = new SqlParameter("@P_ANS4IMG", DBNull.Value);
                        //else
                        //    objParams[10] = new SqlParameter("@P_ANS4IMG", objQuest.ANS4IMAGE);

                        //objParams[10].SqlDbType = SqlDbType.Image;
                        objParams[8] = new SqlParameter("@P_CORRECTANS", objQuest.CORRECTANS);
                        objParams[9] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                        objParams[10] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        objParams[11] = new SqlParameter("@P_TYPE", objQuest.TYPE);
                        objParams[12] = new SqlParameter("@P_TOPIC", objQuest.TOPIC);
                        objParams[13] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);
                        objParams[14] = new SqlParameter("@P_MARKS_FOR_QUESTION", objQuest.MARKS_FOR_QUESTION);
                        objParams[15] = new SqlParameter("@P_Org_ID", OrgId);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_IQUESTIONBANK_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.UpdateIQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateImage(IQuestionbank objQuest, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);

                        if (objQuest.QUEIMGNAME == null)
                            objParams[1] = new SqlParameter("@P_QUESTIONIMAGE", DBNull.Value);
                        else
                            objParams[1] = new SqlParameter("@P_QUESTIONIMAGE", objQuest.QUEIMGNAME);


                        if (objQuest.ANS1IMGNAME == null)
                            objParams[2] = new SqlParameter("@P_ANS1IMG", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_ANS1IMG", objQuest.ANS1IMGNAME);

                        if (objQuest.ANS2IMGNAME == null)
                            objParams[3] = new SqlParameter("@P_ANS2IMG", DBNull.Value);
                        else
                            objParams[3] = new SqlParameter("@P_ANS2IMG", objQuest.ANS2IMGNAME);

                        if (objQuest.ANS3IMGNAME == null)
                            objParams[4] = new SqlParameter("@P_ANS3IMG", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_ANS3IMG", objQuest.ANS3IMGNAME);

                        if (objQuest.ANS4IMGNAME == null)
                            objParams[5] = new SqlParameter("@P_ANS4IMG", DBNull.Value);
                        else
                            objParams[5] = new SqlParameter("@P_ANS4IMG", objQuest.ANS4IMGNAME);

                        if (objQuest.ANS5IMGNAME == null)
                            objParams[6] = new SqlParameter("@P_ANS5IMG", DBNull.Value);
                        else
                            objParams[6] = new SqlParameter("@P_ANS5IMG", objQuest.ANS5IMGNAME);

                        if (objQuest.ANS6IMGNAME == null)
                            objParams[7] = new SqlParameter("@P_ANS6IMG", DBNull.Value);
                        else
                            objParams[7] = new SqlParameter("@P_ANS6IMG", objQuest.ANS6IMGNAME);

                        objParams[8] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                        objParams[9] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        //objParams[8] = new SqlParameter("@P_TOPIC", objQuest.TOPIC);
                        objParams[10] = new SqlParameter("@P_Org_ID", OrgId);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_IQUESTIONBANK_IMG_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.UpdateImage -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllQuestion(IQuestionbank objQuest, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                        objParams[2] = new SqlParameter("@P_TESTNO", objQuest.TEST_NO);
                        objParams[3] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);
                        objParams[4] = new SqlParameter("@P_Org_ID", OrgId);
                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_IQUESTIONBANK_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }

                public DataSet GetAllQuestionByNo(IQuestionbank objQuest)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_IQUESTIONBANK_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.GetAllQuestionByNo -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllQuestionByUaNo(IQuestionbank objQuest, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[2] = new SqlParameter("@P_UANO", objQuest.UA_NO);
                        objParams[3] = new SqlParameter("@P_Org_ID", OrgId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ITESTMASTER_GET_BY_QNOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.GetAllQuestionByNo -> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteQuestion(IQuestionbank objQuest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);

                        objParams[1] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[2] = new SqlParameter("@P_Q_NO", objQuest.QUESTIONNO);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_QUESTION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 4)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.DeleteAnnouncement -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddTestResultDetails(IQuestionbank objQuest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];

                        objParams[0] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        objParams[1] = new SqlParameter("@P_QUESTIONTEXT", objQuest.QUESTIONTEXT);
                        objParams[2] = new SqlParameter("@P_SELECTED", objQuest.SELECTED);
                        objParams[3] = new SqlParameter("@P_SELECTEDANS", objQuest.FTBANS);
                        objParams[4] = new SqlParameter("@P_CORRECTANS", objQuest.CORRECTANS);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objQuest.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_IDNO", objQuest.IDNO);
                        objParams[7] = new SqlParameter("@P_TESTNO", objQuest.TEST_NO);
                        objParams[8] = new SqlParameter("@P_DESCRIPTIVE_ANSWER", objQuest.DESCRIPTIVE_ANSWER);
                        objParams[9] = new SqlParameter("@P_QUESTION_MARKS", objQuest.QUESTION_MARKS);
                        objParams[10] = new SqlParameter("@P_TEST_TYPE", objQuest.TEST_TYPE);
                        objParams[11] = new SqlParameter("@P_MARKS_OBTAINED", objQuest.MARKS_OBTAINED);
                        objParams[12] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_TESTRESULT_DETAILS_INSERT", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.IAddQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateTestResultDetails(IQuestionbank objQuest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        objParams[1] = new SqlParameter("@P_DESCRIPTIVE_ANSWER", objQuest.DESCRIPTIVE_ANSWER);

                        objParams[2] = new SqlParameter("@P_TESTNO", objQuest.TEST_NO);
                        objParams[3] = new SqlParameter("@P_IDNO", objQuest.IDNO);
                      
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ANSWER_BYQUES_NO", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.UpdateTeachingPlan -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddTestResult(TestResult objQuest, int Retest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_TESTNO", objQuest.TESTNO);
                        objParams[1] = new SqlParameter("@P_IDNO", objQuest.IDNO);
                        objParams[2] = new SqlParameter("@P_CORRECTMARKS", objQuest.CORRECTMARKS);
                        objParams[3] = new SqlParameter("@P_TOTMARKS", objQuest.TOTALMARKS);
                        objParams[4] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objQuest.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_STUD_ATTEMPTS", objQuest.STUDATTEMPTS);
                        objParams[7] = new SqlParameter("@P_RETEST", Retest);
                        objParams[8] = new SqlParameter("@P_QUESTIONNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_TESTRESULT_INSERT", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.IAddQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }
                
                public DataTableReader GetSingleAnswerByQuesNo(int quesno, int idno, int testno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_QUESNO", quesno), 
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_TESTNO",testno)

                };
                        dtr = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ANSWER_BYQUESTION_NO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetSinglePlanByPlanNo-> " + ex.ToString());
                    }
                    return dtr;
                }
                
                // Used to Export questions from Question bank into excel file

                public DataSet GetAllQuestionFromQB(int courseno, string topicno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[1] = new SqlParameter("@P_TOPIC_NO", topicno);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_GET_EXPORT_IQUESTIONBANK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.GetAllQuestionFromQB-> " + ex.ToString());

                    }
                    return ds;
                }


                #region Student Question Bank
                //Student Question Bank

                public int AddIStudentQuestionBank(IQuestionbank objQuest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];

                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_QUESTIONTEXT", objQuest.QUESTIONTEXT);
                        

                        objParams[2] = new SqlParameter("@P_ANS1TEXT", objQuest.ANS1TEXT);

                        
                        objParams[3] = new SqlParameter("@P_ANS2TEXT", objQuest.ANS2TEXT);

                     
                        objParams[4] = new SqlParameter("@P_ANS3TEXT", objQuest.ANS3TEXT);

                    
                        objParams[5] = new SqlParameter("@P_ANS4TEXT", objQuest.ANS4TEXT);
                        objParams[6] = new SqlParameter("@P_ANS5TEXT", objQuest.ANS5TEXT);
                        objParams[7] = new SqlParameter("@P_ANS6TEXT", objQuest.ANS6TEXT);

                      
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objQuest.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                   
                        objParams[10] = new SqlParameter("@P_TOPIC", objQuest.TOPIC);
                      
                  
                        objParams[11] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);
                        objParams[12] = new SqlParameter("@P_TYPE", objQuest.TYPE);
                        objParams[13] = new SqlParameter("@P_QUESTIONNO", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_STUDQUESTIONBANK_INSERT", objParams, true);
                       
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.IAddQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllStudentQuestion(IQuestionbank objQuest)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_ISTUDQUESTIONBANK_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }



                public DataSet GetAllStudQuestionByUaNo(IQuestionbank objQuest)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[2] = new SqlParameter("@P_UANO", objQuest.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ITQUESMASTER_GET_BY_QNOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.GetAllQuestionByNo -> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateStudQuestionBank(IQuestionbank objQuest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_QUESTIONTEXT", objQuest.QUESTIONTEXT);

                      
                        objParams[2] = new SqlParameter("@P_ANS1TEXT", objQuest.ANS1TEXT);

                     
                        objParams[3] = new SqlParameter("@P_ANS2TEXT", objQuest.ANS2TEXT);

                     
                        objParams[4] = new SqlParameter("@P_ANS3TEXT", objQuest.ANS3TEXT);

                      
                        objParams[5] = new SqlParameter("@P_ANS4TEXT", objQuest.ANS4TEXT);
                        objParams[6] = new SqlParameter("@P_ANS5TEXT", objQuest.ANS5TEXT);
                        objParams[7] = new SqlParameter("@P_ANS6TEXT", objQuest.ANS6TEXT);
                        objParams[8] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                        objParams[9] = new SqlParameter("@P_QUESTIONNO", objQuest.QUESTIONNO);
                        objParams[10] = new SqlParameter("@P_TYPE", objQuest.TYPE);
                        objParams[11] = new SqlParameter("@P_TOPIC", objQuest.TOPIC);
                        objParams[12] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_STUDQUESTIONBANK_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.UpdateIQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public int DeleteStudQuestion(IQuestionbank objQuest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);

                        objParams[1] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[2] = new SqlParameter("@P_Q_NO", objQuest.QUESTIONNO);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_STUDQUESTION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 4)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.DeleteAnnouncement -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public void FillDropDownTopic(DropDownList ddlList, string TableName, string Column1, string Column2, string wherecondition, string orderby)
                {
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                        objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                        objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                        if (!wherecondition.Equals(string.Empty))
                            objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                        else
                            objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                        if (!orderby.Equals(string.Empty))
                            objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                        else
                            objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                        DataSet ds = null;
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);

                        ddlList.Items.Clear();
                        ddlList.Items.Add("Please Select");
                        ddlList.SelectedItem.Value = "0";

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlList.DataSource = ds;
                            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                            ddlList.DataTextField = ds.Tables[0].Columns[0].ToString();
                            ddlList.DataBind();
                            ddlList.SelectedIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                    }
                    // return ddlList;
                }




                public DataSet ViewAllStudentQuestion(IQuestionbank objQuest)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_Topic", objQuest.TOPIC);
                        objParams[2] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_VIEWSTUDQUESTIONBANK_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }
                #endregion

                #region AutoCompleteTopicName
                /// Purpose : Auto-complete Method for Topic Name. 
                /// FUNCTION NAME: AutoCompleteTopicName
                /// //Added by Saket Singh on 08-08-2017
                /// </summary>
                /// <returns></returns>
                public DataSet AutoCompleteTopicName(string TopicName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSENO", Convert.ToInt32(HttpContext.Current.Session["ICourseNo"].ToString()));
                        objParams[1] = new SqlParameter("@P_UA_NO", Convert.ToInt32(HttpContext.Current.Session["userno"].ToString()));
                        //objParams[2] = new SqlParameter("@P_QUESTION_TYPE", TopicName);
                        objParams[2] = new SqlParameter("@P_TOPIC", TopicName);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_IQUESTIONBANK_AUTOCOMPLETE_TOPIC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.GetAllQuestionByNo -> " + ex.ToString());
                    }
                    return ds;                   
                }
                #endregion
            }
        }
    }
}
