//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STUDENT FEEDBACK CONTROLLER                                          
// CREATION DATE : 24-SEPT-2009                                                         
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class StudentFeedBackController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddFeedBack(StudentFeedBack SFB)
                {
                    int retStatus = -1;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                        objParams[1] = new SqlParameter("@P_FEEDBACK", SFB.FeedBack);
                        objParams[2] = new SqlParameter("@P_FEEDBACK_DATE", SFB.FeedbackDate);
                        objParams[3] = new SqlParameter("@P_STATUS", SFB.Status);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                        objParams[5] = new SqlParameter("@P_TOKENNO", SFB.TokenNo);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_INSERT", objParams, true));
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetFeedBackDetailsById(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETFEEDBACKDETAILSBYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }


                public SqlDataReader GetCourseSelected(int sessionno, int idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return dr;
                }



                /// <summary>
                /// Modified by SP - 24032022
                /// </summary>
                /// <param name="objSEB"></param>
                /// <param name="SubId"></param>
                /// <returns></returns>
                public DataSet GetFeedBackQuestionForMaster(StudentFeedBack objSEB, int SubId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CTID", objSEB.CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", SubId);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSEB.SemesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_FEEDBACK_QUESTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }
                public int GetStudentTokenNo(string regno)
                {
                    int idno = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", regno);

                        idno = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_RET_STUDID_BY_REGNO", objParams));

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.GetStudentIDByRegNo-> " + ex.ToString());
                    }

                    return idno;
                }
                //public int AddStudentFeedBackAns(StudentFeedBack SFB)
                //{
                //    int retStatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[12];
                //        objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", SFB.SessionNo);
                //        objParams[2] = new SqlParameter("@P_IPADDRESS", SFB.Ipaddress);
                //        objParams[3] = new SqlParameter("@P_QUESTIONID", SFB.QuestionIds);
                //        objParams[4] = new SqlParameter("@P_ANSWERID", SFB.AnswerIds);
                //        objParams[5] = new SqlParameter("@P_DATE", SFB.Date);
                //        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                //        objParams[7] = new SqlParameter("@P_REMARK", SFB.Remark);
                //        objParams[8] = new SqlParameter("@P_COURSENO", SFB.CourseNo);
                //        objParams[9] = new SqlParameter("@P_STATUS", SFB.FB_Status);
                //        objParams[10] = new SqlParameter("@P_UA_NO", SFB.UA_NO);
                //        objParams[11] = new SqlParameter("@P_OUT", SFB.Out);
                //        objParams[11].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_ANSWER_INSERT", objParams, true);
                //        retStatus = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                //FeedBack Questions
                //public int AddStudentFeedBackAns(StudentFeedBack SFB)
                //{
                //    int retStatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[21];
                //        objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", SFB.SessionNo);
                //        objParams[2] = new SqlParameter("@P_IPADDRESS", SFB.Ipaddress);
                //        objParams[3] = new SqlParameter("@P_QUESTIONID", SFB.QuestionIds);
                //        objParams[4] = new SqlParameter("@P_ANSWERID", SFB.AnswerIds);
                //        objParams[5] = new SqlParameter("@P_DATE", SFB.Date);
                //        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                //        objParams[7] = new SqlParameter("@P_REMARK", SFB.Remark);
                //        objParams[8] = new SqlParameter("@P_COURSENO", SFB.CourseNo);
                //        objParams[9] = new SqlParameter("@P_STATUS", SFB.FB_Status);
                //        objParams[10] = new SqlParameter("@P_UA_NO", SFB.UA_NO);
                //        objParams[11] = new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A);
                //        objParams[12] = new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B);
                //        objParams[13] = new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C);
                //        objParams[14] = new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D);
                //        objParams[15] = new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression);

                //        objParams[16] = new SqlParameter("@P_CTID", SFB.CTID);
                //        objParams[17] = new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher);
                //        objParams[18] = new SqlParameter("@P_FROMDEPARTMENT", SFB.FromDepartment);
                //        objParams[19] = new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment);

                //        objParams[20] = new SqlParameter("@P_OUT", SFB.Out);
                //        objParams[20].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_ANSWER_INSERT", objParams, true);
                //        retStatus = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //public int AddFeedbackQuestion(StudentFeedBack SFB)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_SUBID", SFB.SubId);
                //        objParams[1] = new SqlParameter("@P_CTID", SFB.CTID);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                //        objParams[3] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                //        objParams[4] = new SqlParameter("@P_COLL_CODE", SFB.CollegeCode);
                //        objParams[5] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                //        objParams[6] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                //        objParams[7] = new SqlParameter("@P_OUT", SFB.Out);
                //        objParams[7].Direction = ParameterDirection.Output;
                //        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_QUESTION", objParams, true));
                //        if (ret.ToString() == "1" && ret != null)
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else if (ret.ToString() == "-99")
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackQuestion-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //public int UpdateFeedbackQuestion(StudentFeedBack SFB)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;

                //        //update
                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_QUESTIONID", SFB.QuestionId);
                //        objParams[1] = new SqlParameter("@P_SUBID", SFB.SubId);
                //        objParams[2] = new SqlParameter("@P_CTID", SFB.CTID);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                //        objParams[4] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                //        objParams[5] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                //        objParams[6] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                //        objParams[7] = new SqlParameter("@P_OUT", SFB.Out);
                //        objParams[7].Direction = ParameterDirection.Output;

                //        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_UPD_QUESTION", objParams, true));
                //        if (ret.ToString() == "2" && ret != null)
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //        }
                //        else if (ret.ToString() == "-99")
                //        {
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddStudentFeedBackAns(StudentFeedBack SFB)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_IDNO", SFB.Idno),
                        new SqlParameter("@P_SESSIONNO", SFB.SessionNo),
                        new SqlParameter("@P_IPADDRESS", SFB.Ipaddress),
                        new SqlParameter("@P_QUESTIONID", SFB.QuestionIds),
                        new SqlParameter("@P_ANSWERID", SFB.AnswerIds),
                        //new SqlParameter("@P_DATE", SFB.Date),
                        new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode),
                        new SqlParameter("@P_REMARK", SFB.Remark),
                        new SqlParameter("@P_COURSENO", SFB.CourseNo),
                        new SqlParameter("@P_STATUS", SFB.FB_Status),
                        new SqlParameter("@P_UA_NO", SFB.UA_NO),
                        new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A),
                        new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B),
                        new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C),
                        new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D),
                        new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression),
                        new SqlParameter("@P_CTID", SFB.CTID),
                        new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher),
                        new SqlParameter("@P_FROMDEPARTMENT", SFB.FromDepartment),
                        new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment),
                        new SqlParameter("@P_EXAMNO", SFB.ExamNo),
                        new SqlParameter("@P_OUT", SFB.Out),
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_ANSWER_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllFeedBackQuestionForMaster(int CTID, int SubId, string SemesterNos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CTID", CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", SubId);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ALL_FEEDBACK_QUESTION_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddFeedbackQuestion(StudentFeedBack SFB, int calcstatus, string ansoptiontype, int seqno, int queheader, int coursetype, int choisefor, int ismandatory)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[16];
                        //objParams[0] = new SqlParameter("@P_SUBID", SFB.SubId);
                        objParams[0] = (SFB.SubId != 0) ? new SqlParameter("@P_SUBID", SFB.SubId) : new SqlParameter("@P_SUBID", DBNull.Value);
                        objParams[1] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                        objParams[3] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                        objParams[4] = new SqlParameter("@P_COLL_CODE", SFB.CollegeCode);
                        objParams[5] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                        objParams[6] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                        objParams[7] = new SqlParameter("@P_ACTIVE_STATUS", SFB.ActiveStatus);
                        objParams[8] = new SqlParameter("@P_CALCULATION_STATUS", calcstatus);
                        objParams[9] = new SqlParameter("@P_OPTION_TYPE", ansoptiontype);
                        objParams[10] = new SqlParameter("@P_SEQ_NO", seqno);
                        objParams[11] = new SqlParameter("@P_QUESTION_HEADER", queheader);  //added by nehal on 26062023
                        objParams[12] = new SqlParameter("@P_COURSETYPE", coursetype);  //Added by Nehal N 25/08/2023
                        objParams[13] = new SqlParameter("@P_CHOISEFOR", choisefor);  //Added by Nehal N 25/08/2023
                        objParams[14] = new SqlParameter("@P_ISMANDATORY", ismandatory); 
                        objParams[15] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[15].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_QUESTION", objParams, true));
                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-1001" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackQuestion-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateFeedbackQuestion(StudentFeedBack SFB, int calculationstatus, string ansoptiontype, int seqno, int queheader, int coursetype, int choisefor,int ismandatory)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_QUESTIONID", SFB.QuestionId);
                        //objParams[1] = new SqlParameter("@P_SUBID", SFB.SubId);
                        objParams[1] = (SFB.SubId != 0) ? new SqlParameter("@P_SUBID", SFB.SubId) : new SqlParameter("@P_SUBID", DBNull.Value);
                        objParams[2] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                        objParams[4] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                        objParams[5] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                        objParams[6] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                        objParams[7] = new SqlParameter("@P_ACTIVE_STATUS", SFB.ActiveStatus);
                        objParams[8] = new SqlParameter("@P_CALCULATION_STATUS", calculationstatus);
                        objParams[9] = new SqlParameter("@P_SEQ_NO", seqno);
                        objParams[10] = new SqlParameter("@P_OPTION_TYPE", ansoptiontype);
                        objParams[11] = new SqlParameter("@P_QUESTION_HEADER", queheader);  //added by nehal on 26062023
                        objParams[12] = new SqlParameter("@P_COURSETYPE", coursetype);  //Added by Nehal N 25/08/2023
                        objParams[13] = new SqlParameter("@P_CHOISEFOR", choisefor);  //Added by Nehal N 25/08/2023
                        objParams[14] = new SqlParameter("@P_ISMANDATORY", ismandatory);
                        objParams[15] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_UPD_QUESTION", objParams, true));
                        if (ret.ToString() == "2" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (ret.ToString() == "-1001" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetFeedBackQuestion(StudentFeedBack SFB)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUBID", SFB.SubId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_FEEDBACK_ANS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeedBackQuestionForMaster(StudentFeedBack SFB)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", SFB.SubId);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                        objParams[3] = new SqlParameter("@P_IDNO", SFB.Idno);
                        objParams[4] = new SqlParameter("@P_UA_NO", SFB.UA_NO);
                        objParams[5] = new SqlParameter("@P_COURSENO", SFB.CourseNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_FEEDBACK_QUESTION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                //public DataSet GetFeedBackQuestionForMaster(int CTID, int SubId, string SemesterNos)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_CTID", CTID);
                //        objParams[1] = new SqlParameter("@P_SUBID", SubId);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNos);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_FEEDBACK_QUESTION_LIST", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GetEditFeedBack(StudentFeedBack SFB)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUESTIONID", SFB.QuestionId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FEEDBACK_QUESTION_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet RetrieveStudentFeedbackStatus(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_FEEDBACK_STATUS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeedbackStatus()-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added By Manish on dt. 23-Sept-2015
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="idno"></param>
                /// <param name="semesterno"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                public DataSet GetCourseSelected(int sessionno, int idno, int semesterno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT_NEXT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }
                //public SqlDataReader GetCourseSelected(int sessionno, int idno, int semesterno, int schemeno)
                //{
                //    SqlDataReader dr = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_IDNO", idno);
                //        dr = objSQLHelper.ExecuteReaderSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return dr;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                //    }
                //    return dr;
                //}
                public int FeedbackCount(int IDNO, int SESSIONMAX)
                {
                    int count = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONMAX);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        count = Convert.ToInt16((objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEECOLLECTION_FEEDBACK", objParams, true)));

                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackQuestion-> " + ex.ToString());
                    }
                    return count;
                }


                public SqlDataReader GetCourseSelectedStatus(int sessionno, int idno, int feedbackid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_FEEDBACK_ID", feedbackid);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_STUDENT_FEEDBACK_COURSE_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.GetCourseSelectedStatus-> " + ex.ToString());
                    }
                    return dr;
                }


                /// <summary>
                ///  Added By Rishabh on 07/05/2022
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="idno"></param>
                /// <param name="semesterno"></param>
                /// <param name="schemeno"></param>
                /// <param name="Feedback"></param>
                /// <returns></returns>
                public DataSet GetCourseListStatus(int sessionno, int idno, int semesterno, int schemeno, int Feedback)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_FEEBACK_ID",Feedback)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_FEEDBACK_COURSE_LIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.GetCourseListStatus-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added By Rishabh on 09-05-2022
                /// </summary>
                /// 
                /// <summary>
                /// Added by Amit B. on date 28/11/2023
                /// </summary>
                /// <param name="SFB"></param>
                /// <param name="dt_FEEDBACK"></param>
                /// <param name="COMMENTS"></param>
                /// <returns></returns>
                public int InsertStudentFeedBackAnswer(StudentFeedBack SFB, DataTable dt_FEEDBACK, string COMMENTS, int Is_Final)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_IDNO", SFB.Idno),
                        new SqlParameter("@P_SESSIONNO", SFB.SessionNo),
                        new SqlParameter("@P_IPADDRESS", SFB.Ipaddress),
                        new SqlParameter("@P_QUESTIONID", SFB.QuestionIds),
                        new SqlParameter("@P_ANSWERID", SFB.AnswerIds),
                        //new SqlParameter("@P_DATE", SFB.Date),
                        new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode),
                        new SqlParameter("@P_REMARK", SFB.Remark),
                        new SqlParameter("@P_COURSENO", SFB.CourseNo),
                        new SqlParameter("@P_STATUS", SFB.FB_Status),
                        new SqlParameter("@P_UA_NO", SFB.UA_NO),
                        new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A),
                        new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B),
                        new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C),
                        new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D),
                        new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression),
                        new SqlParameter("@P_CTID", SFB.CTID),
                        new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher),
                        new SqlParameter("@P_FROMDEPARTMENT", SFB.FromDepartment),
                        new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment),
                        new SqlParameter("@P_EXAMNO", SFB.ExamNo),
                        new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                        new SqlParameter("@P_ACD_FEEDBACK_QA", dt_FEEDBACK),
                        new SqlParameter("@P_COMMENTS", COMMENTS),
                        new SqlParameter("@P_IS_FINAL", Is_Final),                        
                        new SqlParameter("@P_OUT", SFB.Out),
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_STUDENT_FEEDBACK_ANSWER", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.InsertStudentFeedBackAnswer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int SaveStudentFeedBackAnswer(StudentFeedBack SFB, DataTable dt_FEEDBACK, string COMMENTS)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_IDNO", SFB.Idno),
                        new SqlParameter("@P_SESSIONNO", SFB.SessionNo),
                        new SqlParameter("@P_IPADDRESS", SFB.Ipaddress),
                        new SqlParameter("@P_QUESTIONID", SFB.QuestionIds),
                        new SqlParameter("@P_ANSWERID", SFB.AnswerIds),
                        //new SqlParameter("@P_DATE", SFB.Date),
                        new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode),
                        new SqlParameter("@P_REMARK", SFB.Remark),
                        new SqlParameter("@P_COURSENO", SFB.CourseNo),
                        new SqlParameter("@P_STATUS", SFB.FB_Status),
                        new SqlParameter("@P_UA_NO", SFB.UA_NO),
                        new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A),
                        new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B),
                        new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C),
                        new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D),
                        new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression),
                        new SqlParameter("@P_CTID", SFB.CTID),
                        new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher),
                        new SqlParameter("@P_FROMDEPARTMENT", SFB.FromDepartment),
                        new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment),
                        new SqlParameter("@P_EXAMNO", SFB.ExamNo),
                        new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                        new SqlParameter("@P_ACD_FEEDBACK_QA", dt_FEEDBACK),
                        new SqlParameter("@P_COMMENTS", COMMENTS),                  
                        new SqlParameter("@P_OUT", SFB.Out),
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SAVE_STUDENT_FEEDBACK_ANSWER", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.InsertStudentFeedBackAnswer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// Added by Amit B. on date 28/11/2023
                public int UpdateFinalSubmitStatus(StudentFeedBack SFB, int OrganisationId, int Is_Final)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_IDNO", SFB.Idno),
                        new SqlParameter("@P_SESSIONNO", SFB.SessionNo),
                        new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode),
                        //new SqlParameter("@P_UA_NO", SFB.UA_NO),
                        new SqlParameter("@P_EXAMNO", SFB.ExamNo),
                        new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                        new SqlParameter("@P_IS_FINAL", Is_Final),
                        new SqlParameter("@P_CTID", SFB.CTID),
                        new SqlParameter("@P_OUT", SFB.Out),
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_FINAL_STATUS_FEEDBACK", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.UpdateFinalSubmitStatus-> " + ex.ToString());
                    }
                    return retStatus;


                }


                #region "FeedbackMaster"
                //Updated by Sakshi M  ON 14-03-2024

                public int AddFeedbackMaster(string feedbackName, string collegeCode, int feedbackmode, int coursetype, int choisefor, int is_active, string note)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_FEEDBACK_NAME", feedbackName),
                            new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                            new SqlParameter("@P_MODE_ID", feedbackmode),
                            new SqlParameter("@P_COURSETYPE", coursetype),  //added by Nehal on 25/8/23
                            new SqlParameter("@P_CHOISEFOR", choisefor), //added by Nehal on 25/8/23
                            new SqlParameter("@P_IS_ACTIVE", is_active),
                            new SqlParameter("@P_FEEDBACK_NOTE", note),//added by sakshi m on 11/03/2024
                            new SqlParameter("@P_OUTPUT", status)
                            
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEEDBACK_MASTER_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;            //Added By Abhinay Lad [14-06-2019]
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //Updated By Sakhsi M on 14-03-2024
                public int UpdateFeedbackMaster(int feedbackNo, string feedbackName, string collegeCode, int modeid, int coursetype, int choisefor, int is_active, string note)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {         
                                 new SqlParameter("@P_FEEDBACK_NO", feedbackNo),
                                 new SqlParameter("@P_FEEDBACK_NAME", feedbackName),
                                 new SqlParameter("@P_COLLEGE_CODE", collegeCode),   
                                 new SqlParameter("@P_MODE_ID", modeid),   
                                 new SqlParameter("@P_COURSETYPE", coursetype),  //added by Nehal on 25/8/23
                                 new SqlParameter("@P_CHOISEFOR", choisefor), //added by Nehal on 25/8/23
                                 new SqlParameter("@P_IS_ACTIVE", is_active),
                                  new SqlParameter("@P_FEEDBACK_NOTE",note), //added by sakshi m on 11/03/2024
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEEDBACK_MASTER_UPDATE", sqlParams, true);
                        status = (Int32)obj; //Added By Abhinay Lad [14-06-2019]
                    }
                    catch (Exception ex)
                    {
                        status = -99; //Added By Abhinay Lad [14-06-2019]
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.UpdateFeedbackMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public DataSet GetAllFeedback()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FEEDBACK_MASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetAllFeedback() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public SqlDataReader GetFeedbackNo(int feedbackNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", feedbackNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_FEEDBACK_MASTER_GET_BY_FEEDBACK_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetFeedbackNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                #endregion

                #region "Student Attendnace Conditions For Feedback"
                ///Added by Neha Baranwal 11Oct19
                public DataSet GetCourseWiseAttPercent(int sessionno, int schemeno, int semesterno, int courseno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                             new SqlParameter("@P_COURSENO",courseno)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_WISE_STUD_ATTENDANCE_PERCENTAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedbackController.GetCourseWiseAttPercent-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetOverallAttPercent(int sessionno, int schemeno, int semesterno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OVERALL_STUD_ATTENDANCE_FEEDBACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedbackController.GetOverallAttPercent-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddFacultyFeedBackAns(StudentFeedBack SFB)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        //objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SFB.SessionNo);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", SFB.Ipaddress);
                        objParams[2] = new SqlParameter("@P_QUESTIONID", SFB.QuestionIds);
                        objParams[3] = new SqlParameter("@P_ANSWERID", SFB.AnswerIds);
                        objParams[4] = new SqlParameter("@P_DATE", SFB.Date);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                        //objParam0[7] = new SqlParameter("@P_REMARK", SFB.Remark);
                        // objParams[8] = new SqlParameter("@P_COURSENO", SFB.CourseNo);
                        objParams[6] = new SqlParameter("@P_STATUS", SFB.FB_Status);
                        objParams[7] = new SqlParameter("@P_TO_UA_NO", SFB.TO_UA_NO);
                        //objParams[11] = new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A);
                        //objParams[12] = new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B);
                        //objParams[13] = new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C);
                        //objParams[14] = new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D);
                        //objParams[15] = new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression);
                        objParams[8] = new SqlParameter("@P_CTID", SFB.CTID);
                        //objParams[17] = new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher);
                        objParams[9] = new SqlParameter("@P_FROM_UA_NO", SFB.FROM_UA_NO);
                        //objParams[19] = new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment);
                        //objParams[20] = new SqlParameter("@P_EXAMNO", SFB.ExamNo);
                        objParams[10] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FACULTY_FEEDBACK_ANSWER_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                public DataSet GetSubjectFeedbackCommonData(int session, int deg, int branch, int schemeno, int sem, int sectionno, int feedbacktypno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",session),
                            new SqlParameter("@P_DEGREENO",deg),
                            new SqlParameter("@P_BRANCHNO",branch),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_SEMESTERNO",sem),
                            new SqlParameter("@P_SECTIONNO",sectionno),
                            new SqlParameter("@P_FEEDBACK_TYPENO",feedbacktypno),
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_COMMON_FEEDBACK_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedbackController.GetSubjectFeedbackCommonData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllFeedbackMode(int modeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MODE", modeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FEEDBACK_MODE_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetAllFeedback() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public int InsertStudentFeedBackMode(string FeedbackMode, string collegeCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_FEEDBACK_MODE_NAME", FeedbackMode),  
                        new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                        //new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                        new SqlParameter("@P_OUTPUT", ParameterDirection.Output),
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_FEEDBACK_MODE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.InsertStudentFeedBackAnswer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudentFeedBackMode(string FeedbackMode, string collegeCode, int modeno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_MODE_ID", modeno), 
                        new SqlParameter("@P_FEEDBACK_MODE_NAME", FeedbackMode),  
                        new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                        //new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                        new SqlParameter("@P_OUTPUT", ParameterDirection.Output),
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_FEEDBACK_MODE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.InsertStudentFeedBackAnswer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added by pooja

                public DataSet AllFeedbackReport(string collegesession, int degree, int scheme, int branch, int semester, int course)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", collegesession);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[5] = new SqlParameter("@P_COURSENO", course);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_REPORT_FEEDBACK_NOT_SUBMITTED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetAllFeedback() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //Added by pooja S. on dated 291120222
                public DataSet GetAllFeedbackReportData(string session, int degree, int scheme, int branch, int semester, int course, int feedbacktype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESIONNO", session);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[5] = new SqlParameter("@P_COURSENO", course);
                        objParams[6] = new SqlParameter("@P_CTID", feedbacktype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_FEEDBACK_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Nehal N. on dated 130620223
                public DataSet GetAllFeedbackCommentsReport(string session, int degree, int scheme, int branch, int semester, int section, int feedbacktype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", section);
                        objParams[6] = new SqlParameter("@P_FEEDBACK_TYPENO", feedbacktype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_STUDENT_EXIT_FEEDBACK_COMMENT_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllFeedbackCommentsReport-> " + ex.ToString());
                    }
                    return ds;
                }
                //Added by Nehal N. on dated 210620223
                public int InsertStudentFeedBackExitHeader(string queheader)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_QUESTION_HEADER", queheader), 
                        new SqlParameter("@P_OUTPUT", ParameterDirection.Output),
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_FEEDBACK_HEADRER_QUESTION_MASTER", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.InsertStudentFeedBackExitHeader-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added by Nehal N. on dated 220620223
                public DataSet GetAllExitFeedBackQuestionForHeader()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ALL_EXIT_FEEDBACK_QUESTION_HEADER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetAllExitFeedBackQuestionForHeader-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Nehal N. on dated 260620223
                public SqlDataReader GetHeaderMasterNo(int headerid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", headerid) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_FEEDBACK_HEADER_MASTER_GET_BY_HEADER_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetHeaderMasterNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                //Added by Nehal N. on dated 260620223
                public int UpdateStudentFeedBackExitHeader(int headerid, string queheader)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {         
                                 new SqlParameter("@P_HEADER_ID", headerid),
                                 new SqlParameter("@P_QUESTION_HEADER", queheader),  
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_ACD_FEEDBACK_HEADRER_QUESTION_MASTER", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.UpdateStudentFeedBackExitHeader() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //Added by Shubham B  on dated 22112023
                public int AddConvocationQuestion(StudentFeedBack SFB)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                        objParams[1] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                        objParams[2] = new SqlParameter("@P_ANS_VALUE", SFB.Value);
                        objParams[3] = new SqlParameter("@P_COLL_CODE", SFB.CollegeCode);
                        objParams[4] = new SqlParameter("@P_ISCOMMAND", SFB.ActiveStatus);
                        objParams[5] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_CONVOCATION_FEEDBACK_QUESTION", objParams, true));
                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-1001" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddConvocationQuestion-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added by Shubham B  on dated 22112023
                public int UpdateConvocationQuestion(StudentFeedBack SFB)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                        objParams[1] = new SqlParameter("@P_QUESTIONID", SFB.QuestionId);
                        objParams[2] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                        objParams[3] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                        objParams[4] = new SqlParameter("@P_ISCOMMAND", SFB.ActiveStatus);
                        objParams[5] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_CONVOCATION_UPD_QUESTION", objParams, true));
                        if (ret.ToString() == "2" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (ret.ToString() == "-1001" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddConvocationQuestion-> " + ex.ToString());
                    }
                    return retStatus;
                }


                // Added by Gopal M 01032024 Ticket#55046
                public DataSet GetFacultyWiseFeedbackData(int sessionId, int fua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONID",sessionId),
                            new SqlParameter("@P_FUA_NO",fua_no),
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_FACULTY_WISE_FEEDBACK_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedbackController.GetSubjectFeedbackCommonData-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}