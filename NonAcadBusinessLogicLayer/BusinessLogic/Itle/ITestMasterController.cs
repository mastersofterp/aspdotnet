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


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This AssignmentController is used to control ASSIGNMASTER table.
            /// </summary>
            public class ITestMasterController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                // public string _nitprm_constr;

                //#region Constructors

                // /// <summary>
                // /// Default Constructor
                // /// </summary>
                // /// 
                // public ITestMasterController()
                // {
                //     //blank constructor
                // }

                // /// <summary>
                // /// This constructor is used to make the connection string
                // /// </summary>
                // /// <param name="DbUserName"></param>
                // /// <param name="DbPassword"></param>
                // /// <param name="DataBase"></param>
                // /// 
                // public ITestMasterController(string DbUserName, string DbPassword, string DataBase)
                // {
                //     _nitprm_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                // }
                // #endregion


                public int AddITestMaster(ITestMaster objTestMaster, string questionSet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[27];

                        objParams[0] = new SqlParameter("@P_TESTNAME", objTestMaster.TESTNAME);
                        objParams[1] = new SqlParameter("@P_COURSENO", objTestMaster.COURSENO);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objTestMaster.SESSIONNO);
                        objParams[3] = new SqlParameter("@P_STARTDATE", objTestMaster.STARTDATE);
                        objParams[4] = new SqlParameter("@P_ENDDATE", objTestMaster.ENDDATE);
                        objParams[5] = new SqlParameter("@P_MARKSRIGHTANS", objTestMaster.CORR_ANS);
                        objParams[6] = new SqlParameter("@P_TOTAL", objTestMaster.TOTAL);
                        objParams[7] = new SqlParameter("@P_SHOWRANDOM", objTestMaster.SHOWRANDOM);
                        objParams[8] = new SqlParameter("@P_SHOWRESULT", objTestMaster.SHOWRESULT);
                        objParams[9] = new SqlParameter("@P_TESTDURATION", objTestMaster.TESTDURATION);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objTestMaster.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_QUESTIONNO", objTestMaster.QUESTIONNO);
                        objParams[12] = new SqlParameter("@P_UA_NO", objTestMaster.UA_NO);
                        objParams[13] = new SqlParameter("@P_ATTEMPT", objTestMaster.ATTEMPTS);
                        objParams[14] = new SqlParameter("@P_TOTQUE", objTestMaster.TOTQUESTION);
                        objParams[15] = new SqlParameter("@P_TOPICS", objTestMaster.SELECTED_TOPICS);
                        objParams[16] = new SqlParameter("@P_QUESTIONRATIO", objTestMaster.QUESTIONRATIO);
                        objParams[17] = new SqlParameter("@P_MARKRATIO", objTestMaster.QUESTION_MARKS);
                        objParams[18] = new SqlParameter("@P_QUESTION_SET", questionSet);
                        objParams[19] = new SqlParameter("@P_TEST_TYPE", objTestMaster.TEST_TYPE);
                        objParams[20] = new SqlParameter("@P_STUD_IDNO", objTestMaster.Studidno);
                        objParams[21] = new SqlParameter("@P_FULL_RANDOME_TEST", objTestMaster.FULL_RANDOME_TEST);
                        objParams[22] = new SqlParameter("@P_MalFunctionCount", objTestMaster.MalFunctionCount);
                        objParams[23] = new SqlParameter("@P_SHOW_ANSWER_KEY", objTestMaster.SHOW_ANSWER_KEY);

                        objParams[24] = new SqlParameter("@P_IsAllowPassword", objTestMaster.IsAllowPassword);
                        objParams[25] = new SqlParameter("@P_Password", objTestMaster.Password);

                        objParams[26] = new SqlParameter("@P_TESTNO", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_ITESTMASTER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.AddITestMaster -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateITestMaster(ITestMaster objTestMaster, string questionSet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_TESTNAME", objTestMaster.TESTNAME);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objTestMaster.SESSIONNO);
                        objParams[2] = new SqlParameter("@P_STARTDATE", objTestMaster.STARTDATE);
                        objParams[3] = new SqlParameter("@P_ENDDATE", objTestMaster.ENDDATE);
                        objParams[4] = new SqlParameter("@P_MARKSRIGHTANS", objTestMaster.CORR_ANS);
                        objParams[5] = new SqlParameter("@P_TOTAL", objTestMaster.TOTAL);
                        objParams[6] = new SqlParameter("@P_SHOWRANDOM", objTestMaster.SHOWRANDOM);
                        objParams[7] = new SqlParameter("@P_SHOWRESULT", objTestMaster.SHOWRESULT);
                        objParams[8] = new SqlParameter("@P_TESTDURATION", objTestMaster.TESTDURATION);
                        objParams[9] = new SqlParameter("@P_QUESTIONNO", objTestMaster.QUESTIONNO);
                        objParams[10] = new SqlParameter("@P_COURSENO", objTestMaster.COURSENO);
                        objParams[11] = new SqlParameter("@P_TESTNO", objTestMaster.TESTNO);
                        objParams[12] = new SqlParameter("@P_UA_NO", objTestMaster.UA_NO);
                        objParams[13] = new SqlParameter("@P_ATTEMPT", objTestMaster.ATTEMPTS);
                        objParams[14] = new SqlParameter("@P_TOTQUE", objTestMaster.TOTQUESTION);
                        objParams[15] = new SqlParameter("@P_TOPICS", objTestMaster.SELECTED_TOPICS);
                        objParams[16] = new SqlParameter("@P_QUESTIONRATIO", objTestMaster.QUESTIONRATIO);

                        objParams[17] = new SqlParameter("@P_MARKRATIO", objTestMaster.QUESTION_MARKS);
                        objParams[18] = new SqlParameter("@P_TEST_TYPE", objTestMaster.TEST_TYPE);
                        objParams[19] = new SqlParameter("@P_QUESTION_SET", questionSet);
                        objParams[20] = new SqlParameter("@P_STUD_IDNO", objTestMaster.Studidno);
                        objParams[21] = new SqlParameter("@P_FULL_RANDOME_TEST", objTestMaster.FULL_RANDOME_TEST);
                        objParams[22] = new SqlParameter("@P_MalFunctionCount", objTestMaster.MalFunctionCount);
                        objParams[23] = new SqlParameter("@P_SHOW_ANSWER_KEY", objTestMaster.SHOW_ANSWER_KEY);

                        objParams[24] = new SqlParameter("@P_IsAllowPassword", objTestMaster.IsAllowPassword);
                        objParams[25] = new SqlParameter("@P_Password", objTestMaster.Password);

                        objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_ITESTMASTER_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.UpdateITestMaster -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GetAllTest(int COURSENO, int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_ITESTMASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }


                public DataSet GetTestByNo(int TESTNO, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TESTNO", TESTNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ITESTMASTER_GET_BY_NOs", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.GetTestByNo -> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetTestQue()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.GetTestByNo -> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetAllQuestionByTestNo(ITestMaster objTest)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_QUESTIONNO", objTest.QUESTIONNO);
                        objParams[0] = new SqlParameter("@P_TESTNO", objTest.TEST_NO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objTest.COURSENO);
                        objParams[2] = new SqlParameter("@P_UANO", objTest.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_ITESTMASTER_GET_BY_QNOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.GetAllQuestionByNo -> " + ex.ToString());
                    }
                    return ds;
                }



                public int AddITestMaster_Temp(string Query)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Query", Query);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_Test_Temp", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.AddITestMaster_Temp -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public int AddITestResultCopy(string InsertQuery)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Query", InsertQuery);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_QUES_SET_INTO_ACD_ITLE_RESULTCOPY_BYIDNO", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.AddITestMaster_Temp -> " + ex.ToString());
                    }

                    return retStatus;
                }


                #region StudentInfo
                public int AddStudInfo(ITestMaster objTestMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];


                        objParams[0] = new SqlParameter("@STUDENTID", objTestMaster.Studidno);
                        objParams[1] = new SqlParameter("@STUDENTNAME", objTestMaster.STUDNAME);
                        objParams[2] = new SqlParameter("@SDSRNO", objTestMaster.COURSENO);
                        objParams[3] = new SqlParameter("@TESTNO", objTestMaster.TEST_NO);
                        objParams[4] = new SqlParameter("@SESSIONNO", objTestMaster.SESSIONNO);
                        objParams[5] = new SqlParameter("@UA_NO", objTestMaster.Uano);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objTestMaster.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTEMPTS", objTestMaster.Reqno);
                        objParams[8] = new SqlParameter("@P_OUT", "1");
                        objParams[8].Direction = ParameterDirection.InputOutput;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INSERT_STUDENT_INFO", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.AddITestMaster -> " + ex.ToString());
                    }

                    return retStatus;
                }
                #endregion


                public DataSet getDataforRetest(int TESTNO, int SCHEMENO, int SEMESTERNO, int SESSIONNO, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TESTNO", TESTNO);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[4] = new SqlParameter("@P_COURSENO", COURSENO);

                        ds = objSQLHelper.ExecuteDataSetSP("pkg_ITLE_GEDATA_FORITLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetTestInfo-> " + ex.ToString());
                    }

                    return ds;
                }


                public int DeleteITestMaster(int SessionNo, int CourseNo, int Test_no, int Ua_No)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[2] = new SqlParameter("@P_TESTNO", Test_no);
                        objParams[3] = new SqlParameter("@P_UA_NO", Ua_No); 
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_ITESTMASTER_DELETE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.UpdateITestMaster -> " + ex.ToString());
                    }

                    return retStatus;
                }

            }


        }
    }
}
