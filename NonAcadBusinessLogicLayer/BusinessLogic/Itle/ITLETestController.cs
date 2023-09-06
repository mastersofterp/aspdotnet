using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer;





namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ITLETestController
            {
                string _nitmn_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Test Creation Part
                public int AddITLETestMaster(ITLETestMaster objTestMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];

                        objParams[0] = new SqlParameter("@P_TESTNAME", objTestMaster.TESTNAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objTestMaster.UA_NO);
                        objParams[2] = new SqlParameter("@P_STARTDATE", objTestMaster.STARTDATE);
                        objParams[3] = new SqlParameter("@P_ENDDATE", objTestMaster.ENDDATE);
                        objParams[4] = new SqlParameter("@P_TESTDURATION", objTestMaster.TESTDURATION);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objTestMaster.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_ATTEMPT", objTestMaster.ATTEMPTS);
                        objParams[7] = new SqlParameter("@P_TOTQUE", objTestMaster.TOTQUESTION);
                        objParams[8] = new SqlParameter("@P_TOTMARK", objTestMaster.TOTMARK);
                        objParams[9] = new SqlParameter("@P_IE_COURSENOS", objTestMaster.IE_COURSENOS);
                        objParams[10] = new SqlParameter("@P_QUESTIONRATIO", objTestMaster.QUESTIONRATIO);
                        objParams[11] = new SqlParameter("@P_MARKRATIO", objTestMaster.QUESTION_MARKS);
                        objParams[12] = new SqlParameter("@P_TEST_TYPE", objTestMaster.TEST_TYPE);
                        objParams[13] = new SqlParameter("@P_SHOWRESULT", objTestMaster.SHOWRESULT);
                        objParams[14] = new SqlParameter("@P_SHOWRANDOM", objTestMaster.SHOWRANDOM);
                        objParams[15] = new SqlParameter("@P_FULL_RANDOME_TEST", objTestMaster.FULL_RANDOME_TEST);
                        objParams[16] = new SqlParameter("@P_TESTNO", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_EXAM_TESTMASTER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLEExamTestController.AddITLETestMaster -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllIETest(int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_EXAM_TESTMASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }

                public DataSet GetIETestByNo(int TESTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TESTNO", TESTNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_EXAM_TESTMASTER_GET_BY_NOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestMasterController.GetTestByNo -> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateITLETestMaster(ITLETestMaster objTestMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_TESTNO", objTestMaster.TESTNO);
                        objParams[1] = new SqlParameter("@P_TESTNAME", objTestMaster.TESTNAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objTestMaster.UA_NO);
                        objParams[3] = new SqlParameter("@P_STARTDATE", objTestMaster.STARTDATE);
                        objParams[4] = new SqlParameter("@P_ENDDATE", objTestMaster.ENDDATE);
                        objParams[5] = new SqlParameter("@P_TESTDURATION", objTestMaster.TESTDURATION);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objTestMaster.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTEMPT", objTestMaster.ATTEMPTS);
                        objParams[8] = new SqlParameter("@P_TOTQUE", objTestMaster.TOTQUESTION);
                        objParams[9] = new SqlParameter("@P_TOTMARK", objTestMaster.TOTMARK);
                        objParams[10] = new SqlParameter("@P_IE_COURSENOS", objTestMaster.IE_COURSENOS);
                        objParams[11] = new SqlParameter("@P_QUESTIONRATIO", objTestMaster.QUESTIONRATIO);
                        objParams[12] = new SqlParameter("@P_MARKRATIO", objTestMaster.QUESTION_MARKS);
                        objParams[13] = new SqlParameter("@P_SHOWRESULT", objTestMaster.SHOWRESULT);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_EXAM_TESTMASTER_UPDATE", objParams, true);
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
                #endregion

                #region Student Test Part

                public int AddIETestDetails(int testno, int idno, DateTime durationleft, DateTime anssubmitdate, string WebBrowserName, string IPADDR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_TESTDURATIONLEFT", durationleft);
                        objParams[3] = new SqlParameter("@P_TESTSTATUS", "P");
                        objParams[4] = new SqlParameter("@P_ANSSUBMITTIME", anssubmitdate);
                        objParams[5] = new SqlParameter("@P_WebBrowserName", WebBrowserName);
                        objParams[6] = new SqlParameter("@P_IPADDR", IPADDR);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_TESTDETAILS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLEExamTestController.AddIETestDetails -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdIETestDurationLeft(int tdno, DateTime durationleft)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TDNO", tdno);
                        objParams[1] = new SqlParameter("@P_TESTDURATIONLEFT", durationleft);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_UPD_TEST_DURATIONLEFT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLEExamTestController.AddIETestDetails -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public int AddIEResultCopy(string Query)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Query", Query);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_RESULTCOPY_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLEExamTestController.AddIEResultCopy -> " + ex.ToString());
                    }

                    return retStatus;
                }


                //USED FOR DISPLAYING LIST OF CREATED TEST TO THE STUDENT
                public DataSet GetIETestAll(string test_type, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TEST_TYPE", test_type);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_EXAM_GET_TEST_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int SaveIETestSelectedAnswer(int tdno, int questionno, string selected, string selectedAns, string ansStat, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_TDNO", tdno);
                        objParams[1] = new SqlParameter("@P_QUESTIONNO", questionno);
                        objParams[2] = new SqlParameter("@P_SELECTED", selected);
                        objParams[3] = new SqlParameter("@P_SELECTEDANS", selectedAns);
                        objParams[4] = new SqlParameter("@P_ANS_STAT", Convert.ToChar(ansStat));
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_UPD_SELECTED_ANS", objParams, true);
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

                public int AddIETestResult(int tdno, int totquestion, int correctans, decimal totalmarks, decimal correctmarks, int studAttempts, int CorseNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_TDNO", tdno);
                        objParams[1] = new SqlParameter("@P_TOTQUESTION", totquestion);
                        objParams[2] = new SqlParameter("@P_CORRECTANS", correctans);
                        objParams[3] = new SqlParameter("@P_TOTALMARKS", totalmarks);
                        objParams[4] = new SqlParameter("@P_CORRECTMARKS", correctmarks);
                        objParams[5] = new SqlParameter("@P_STUDATTEMPTS", studAttempts);
                        objParams[6] = new SqlParameter("@P_COURSENO", CorseNo);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_TESTRESULT_INSERT", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLEExamTestController.AddIETestResult -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdIETestStratTime(int tdno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_TDNO", tdno);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_UPD_TEST_STARTTIME", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITLEExamTestController.UpdIETestStratTime -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public int UpdateQuestionStatus(int questionno, int testno, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_ANS_STAT", 'C');
                        objParams[1] = new SqlParameter("@P_QUESTIONNO", questionno);
                        objParams[2] = new SqlParameter("@P_TESTNO", testno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_UPD_QUESTION_STATUS", objParams, true);
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



                public int UpdateMalfunctionCount(int testNo, int idno, int count)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_MalFunctionCount", count);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_UPDATE_MALFUNCTIONCOUNT", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.UpdateIQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }





                public DataSet GetSelectedCorrectAnswer(int questionNo, int testNo, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_QUESTIONNO", questionNo);
                        objParams[1] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_SELECTED_ANSWER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetQuestionNo(int testNo, int idno, int courseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);

                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_QUESTIONNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateSkipStatus(int questionno, int testno, int idno, int courseNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_QUESTIONNO", questionno);
                        objParams[1] = new SqlParameter("@P_TESTNO", testno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_OBJTEST_UPD_SKIP_STATUS", objParams, true);
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

                public DataSet BindPreviewQuestionCount(int testNo, int idno, int courseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_PREVIEW_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetNextQuestionNo(int testNo, int idno, int questionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_QUESTIONNO", questionNo);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_NEXT_QUESTIONNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet DisplayPreview(int testNo, int idno, int courseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_DISPLAY_PREVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public string GetAnswerStatus(int testNo, int questionNo, int idno)
                {
                    DataSet ds = null;
                    string status = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_QUESTIONNO", questionNo);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_ANSWER_STATUS", objParams);
                        status = ds.Tables[0].Rows[0]["ANS_STAT"].ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return status;
                }

                #endregion

                #region Test Result Part
                public DataSet GetIETestResult(int testno, string orderBy)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        objParams[1] = new SqlParameter("@P_ORDERBY", orderBy);

                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_EXAM_GET_TEST_RESULT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestResultController.GetTestResult -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIETestAbsentStudent(int testno, string orderBy)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        objParams[1] = new SqlParameter("@P_ORDERBY", orderBy);

                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_EXAM_GET_TEST_ABSENT_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestResultController.GetTestResult -> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region TestChart
                public DataSet GetIETestStatusForChart(int testno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        ///objParams[1] = new SqlParameter("@P_SessionNo", Sessionno);
                        ///objParams[2] = new SqlParameter("@P_CourseNo", CourseNo);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_TEST_STATUS_FOR_CHART", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetIETestStatusForChart-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIETestPreview(int testno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_TEST_PREVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIETestAbsentList(int testno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_TEST_ABSENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetIETestAbsentList-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region ResultProcess
                public DataSet GetPendingStudent(int testno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_PENDING_STUDENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetCorrectAnswer(int testno, int idno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitmn_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TESTNO", testno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_CORRECT_ANSWER_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITLEExamTestController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion
            }
        }
    }
}
