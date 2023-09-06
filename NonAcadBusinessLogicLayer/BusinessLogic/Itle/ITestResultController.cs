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
            /// This Controller Controls The ACAD_ITEST_RESULT
            /// </summary>
           public partial class ITestResultController
            {
               string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
               
               /// <summary>
               /// This Method Is Used To Insert The Student Test Result
               /// </summary>
               /// <param name="ITESTNO"></param>
               /// <param name="IIDNO"></param>
               /// <param name="IMRKSOBTD"></param>
               /// <param name="ITESTDATE"></param>
               /// <param name="ICOLLAGECODE"></param>
               /// <returns></returns>
               /// 
               
               public int AddTestResult(int ITESTNO, int IIDNO, int IMRKSOBTD, DateTime ITESTDATE, int ICOLLAGECODE )
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[6];
                       objParams[0] = new SqlParameter("@P_ITESTNO",ITESTNO);
                       objParams[1] = new SqlParameter("@P_IIDNO",IIDNO);
                       objParams[2] = new SqlParameter("@P_IMRKSOBTD",IMRKSOBTD);
                       objParams[3] = new SqlParameter("@P_ITESTDATE",ITESTDATE);
                       objParams[4] = new SqlParameter("@P_ICOLLAGECODE", ICOLLAGECODE);
                       objParams[5] = new SqlParameter("@P_IAUTOINCNO", SqlDbType.Int);
                       objParams[5].Direction = ParameterDirection.Output;

                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ITEST_RESULT", objParams, true);
                       if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                       else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                   }
                   catch(Exception ex)
                   {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestResultController.AddTestResult -> " + ex.ToString());
                   }

                  return retStatus;

               }
               public DataSet GetTestResult(TestResult objResult, string orderBy)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;

                       objParams = new SqlParameter[4];
                       objParams[0] = new SqlParameter("@P_COURSENO", objResult.COURSENO);
                       objParams[1]=new SqlParameter("@P_TESTNO", objResult.TESTNO);
                       objParams[2] = new SqlParameter("@P_SESSIONNO", objResult.SESSIONNO);
                       objParams[3] = new SqlParameter("@P_ORDERBY", orderBy);

                       ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_GET_TEST_RESULT", objParams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestResultController.GetTestResult -> " + ex.ToString());
                   }
                   return ds;
               }


               public DataSet GetAbsentStudent(TestResult objResult, string orderBy)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;

                       objParams = new SqlParameter[4];
                       objParams[0] = new SqlParameter("@P_SESSIONNO", objResult.SESSIONNO);
                       objParams[1] = new SqlParameter("@P_COURSENO", objResult.COURSENO);
                       objParams[2] = new SqlParameter("@P_TESTNO", objResult.TESTNO);                     
                       objParams[3] = new SqlParameter("@P_ORDERBY", orderBy);

                       ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_ABSENT_STUD_REPORT", objParams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestResultController.GetTestResult -> " + ex.ToString());
                   }
                   return ds;
               }



               public DataSet GetAssignResult(int UA_NO)
               {


                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[1];

                       objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_GET_ASSIGNMENT_RESULT", objParams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetAllEbooksByUaNo -> " + ex.ToString());
                   }
                   return ds;






                   //DataSet ds = null;
                   //try
                   //{
                   //    SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                   //    SqlParameter[] objParams = null;

                   //    objParams = new SqlParameter[1];
                   //    objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);




                   //    ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_GET_ASSIGNMENT_RESULT", objParams);
                   //}
                   //catch (Exception ex)
                   //{
                   //    throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetAnnouncementByCourseNo -> " + ex.ToString());
                   //}
                   //return ds;
               }
               public int AddTestResult2(ITestMaster objQuest)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[8];

                       objParams[0] = new SqlParameter("@P_TESTNO", objQuest.TEST_NO);
                       objParams[1] = new SqlParameter("@P_IDNO", objQuest.IDNO);


                       objParams[2] = new SqlParameter("@P_CORRECTMARKS", objQuest.CORRECTMARKS);


                       objParams[3] = new SqlParameter("@P_TOTMARKS", objQuest.TOTAL);


                       objParams[4] = new SqlParameter("@P_SDSRNO", objQuest.SDSRNO);



                       objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objQuest.COLLEGE_CODE);

                       objParams[6] = new SqlParameter("@P_STUD_ATTEMPTS", objQuest.ATTEMPTS);



                       objParams[7] = new SqlParameter("@P_QUESTIONNO", SqlDbType.Int);
                       objParams[7].Direction = ParameterDirection.Output;

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



               //FOR DESCRIPTIVE TYPE TEST
               //13/03/2014
               public DataSet GetAllDescriptiveTestByCourseNo(int session, int courseno, int ua_no)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                       objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_DESCRIP_TEST_BYCOURSE_NO", objParams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                   }
                   return ds;
               }


               public DataSet GetAllStudentByTestNo(int session, int courseno, int testno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[3];
                       objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                       objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                       objParams[2] = new SqlParameter("@P_TESTNO", testno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_STUDENT_BY_TEST_NO", objParams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                   }
                   return ds;
               }



               public DataSet GetAllQuestionsByIdNo(int session, int courseno, int testno,int idno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[4];
                       objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                       objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                       objParams[2] = new SqlParameter("@P_TESTNO", testno);
                       objParams[3] = new SqlParameter("@P_IDNO", idno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_QUESTIONS_BY_IDNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                   }
                   return ds;
               }


               public DataTableReader GetSingleAnswerByQuestionNo(int session, int courseno, int testno, int idno,int quesno)
               {
                   DataTableReader dtr = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[5];
                       objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                       objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                       objParams[2] = new SqlParameter("@P_TESTNO", testno);
                       objParams[3] = new SqlParameter("@P_IDNO", idno);
                       objParams[4] = new SqlParameter("@P_QUESTIONNO", quesno);

                       dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ANSWERS_BY_QUESTIONNO", objParams).Tables[0].CreateDataReader();
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                   }
                   return dtr;
               }


               public int MarkEntryForTest(TestResult objtr,int quesno)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       bool flag = true;
                       if (flag == true)
                       {
                           //Remark Assignment Reply
                           objParams = new SqlParameter[8];
                           objParams[0] = new SqlParameter("@P_QUESTIONNO", quesno);
                           objParams[1] = new SqlParameter("@P_REMARKS", objtr.REMARKS);
                           objParams[2] = new SqlParameter("@P_IDNO", objtr.IDNO);
                           objParams[3] = new SqlParameter("@P_TESTNO", objtr.TESTNO);
                           objParams[4] = new SqlParameter("@P_COURSENO", objtr.COURSENO);
                           objParams[5] = new SqlParameter("@P_CHECKED", objtr.CHECKED);
                           objParams[6] = new SqlParameter("@P_STUDENT_MARKS", objtr.MARKS_OBTAINED);
                           objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                           objParams[7].Direction = ParameterDirection.Output;
                           object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_MARK_ENTRY_FOR_DESCRIPTIVE_TEST", objParams, true);
                           if (Convert.ToInt32(ret) == -99)
                               retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                           else
                               retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       }


                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.ReplyAssignmenr-> " + ex.ToString());
                   }
                   return retStatus;
               }

               //USED TO TEMPORARY STORE USER INTO OF IITMS FRESHER'S

               public int AddFresherInfo(int idno, string username,string qualification,string email,string address,string mobileno)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[6];

                       objParams[0] = new SqlParameter("@P_USERNAME",username);
                       
                       objParams[1] = new SqlParameter("@P_QUALIFICATION",qualification);
                       objParams[2] = new SqlParameter("@P_EMAIL", email);
                       objParams[3] = new SqlParameter("@P_ADDRESS", address);
                       objParams[4] = new SqlParameter("@P_MOBILENO", mobileno);
                       objParams[5] = new SqlParameter("@P_IDNO", idno);
                       //objParams[8] = new SqlParameter("@P_QUESTIONNO", SqlDbType.Int);
                       //objParams[8].Direction = ParameterDirection.Output;

                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_IITMS_FRESHERS", objParams, true);
                       //if (Convert.ToInt32(ret) == -99)
                       //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                       //else
                       //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       retStatus = Convert.ToInt32(ret);

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITestResultController.AddFresherInfo -> " + ex.ToString());
                   }

                   return retStatus;
               }

            }

             


             }
    }
}
                
