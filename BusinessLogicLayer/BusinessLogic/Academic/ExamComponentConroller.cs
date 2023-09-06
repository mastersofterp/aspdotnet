//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [EXAMCOMPONENTCONTROLLER]                              
// CREATION DATE : 18-JULLY-2022                                                
// CREATED BY    : NIKHIL SHENDE                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

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
        namespace BusinessLayer.BusinessLogic.Academic
        {
            public class ExamComponentConroller
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public int AddAssesment(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SrNo", SqlDbType.Int);
                        objParams[1] = new SqlParameter("@P_Marks_Out_of", objSession.MarkOutOf);
                        objParams[2] = new SqlParameter("@P_Weightage", objSession.Weightage);
                        //objParams[3] = new SqlParameter("@P_Assesment_Category", objSession.AssesmentCategory);
                        //objParams[4] = new SqlParameter("@P_Assesment_Name", objSession.AssesmentName);
                        objParams[0].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ASSESSMENT", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return status;
                }

                public int Insert_Exam_Components_Details(string SrNo, string SubExamNo, int College_id, int Sessionno, int courseno, string MarkOutOf, string Weightage, string txtCaPer1, string txtFinal, string txtOverall, int UaNo, int Semesterno, int schemeno, int SubId, string totalMark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];

                        objParams[0] = new SqlParameter("@P_SRNO", SrNo);
                        objParams[1] = new SqlParameter("@P_SUBEXAM_NO", SubExamNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_MARKOUTOF", MarkOutOf);
                        objParams[6] = new SqlParameter("@P_WEIGHTAGE", Weightage);
                        objParams[7] = new SqlParameter("@P_INTERNAL_WEIGHT", txtCaPer1);
                        objParams[8] = new SqlParameter("@P_EXTERNAL_WEIGHT", txtFinal);
                        objParams[9] = new SqlParameter("@P_OVERALL", txtOverall);
                        objParams[10] = new SqlParameter("@P_UA_NO", UaNo);
                        objParams[11] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[12] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[13] = new SqlParameter("@P_SUBID", SubId);
                        objParams[14] = new SqlParameter("@P_TotalMark", totalMark);
                        objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[15].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_INS_ASSESSMENT_EXAM_COMPONENT", objParams, true));

                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamComponentConroller.Insert_Exam_Components_Details-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int Insert_Exam_Components_Details(string SrNo, string SubExamNo, int College_id, int Sessionno, int courseno, string MarkOutOf, string Weightage, string txtCaPer1, string txtFinal, string txtOverall, int UaNo, int Semesterno, int schemeno, int SubId, string totalMark, int islock)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];

                        objParams[0] = new SqlParameter("@P_SRNO", SrNo);
                        objParams[1] = new SqlParameter("@P_SUBEXAM_NO", SubExamNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_MARKOUTOF", MarkOutOf);
                        objParams[6] = new SqlParameter("@P_WEIGHTAGE", Weightage);
                        objParams[7] = new SqlParameter("@P_INTERNAL_WEIGHT", txtCaPer1);
                        objParams[8] = new SqlParameter("@P_EXTERNAL_WEIGHT", txtFinal);
                        objParams[9] = new SqlParameter("@P_OVERALL", txtOverall);
                        objParams[10] = new SqlParameter("@P_UA_NO", UaNo);
                        objParams[11] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[12] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[13] = new SqlParameter("@P_SUBID", SubId);
                        objParams[14] = new SqlParameter("@P_TotalMark", totalMark);
                        objParams[15] = new SqlParameter("@P_LockStat", islock);
                        objParams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[16].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_INS_ASSESSMENT_EXAM_COMPONENT", objParams, true));

                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamComponentConroller.Insert_Exam_Components_Details-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllSubExamName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SubExamName", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.Getdetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllAssesment()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_AssesmentName", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.Getdetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetDataCollegeFillDropDownlist()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@OrganizationId", orgid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COLLEGENAME_DROPDOWNLIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamComponentConroller.GetDataCollegeFillDropDownlist() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataTableReader GetOfferdPer(int SUBEXAMNO)
                {
                    DataTableReader ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUBEXAMNO", SUBEXAMNO);

                        ds = objSQLHelper.ExecuteDataSetSP("[GET_SUB_EXAM_MARK]", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.GetCourse-> " + ex.ToString());
                    }
                    return ds;
                }
                public int CancleExamComponent(int College_id, int Sessionno, int courseno, int SubExamNo, int UaNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SUBEXAM_NO", SubExamNo);
                        objParams[4] = new SqlParameter("@P_UA_NO", UaNo);
                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_ASSESSMENT_EXAM_COMPONENT", objParams, true));
                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamComponentConroller.Insert_Exam_Components_Details-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CancleExamComponents(int College_id, int Sessionno, int courseno, int ExamNo, int UaNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SUBEXAM_NO", ExamNo);
                        objParams[4] = new SqlParameter("@P_UA_NO", UaNo);
                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_ASSESSMENT_EXAM_COMPONENT", objParams, true));
                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamComponentConroller.Insert_Exam_Components_Details-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //Added by Injamam 27-2-23
                public DataSet ExamComponantLockStatus(int course)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", course);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_COMPONENT_LOCKTRACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditFacultyDiscplineData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet UpadteExamComponentLockStatus(int chlock, int college_id, int session_no, int course_no, int ua_no, int subexam_no, string ipaddress)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_LOCK", chlock);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", session_no);
                        objParams[3] = new SqlParameter("@P_COURSENO", course_no);
                        objParams[4] = new SqlParameter("@P_SUBEXAMNO", subexam_no);
                        objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[6] = new SqlParameter("@P_IP_ADDRESS", ipaddress);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_COMPONENT_LOCKTRACK_UPDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditFacultyDiscplineData-> " + ex.ToString());
                    }
                    return ds;

                }
                public int CheckEntrydoneornot(int schemeno, int Sessionno, int courseno, int ExamNo, int UaNo, int UATYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SUBEXAMNO", ExamNo);
                        objParams[4] = new SqlParameter("@P_UANO", UaNo);
                        objParams[5] = new SqlParameter("@P_USERTYPE", UATYPE);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_GETCHECKREMOVE_VALIDATE", objParams, true));
                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamComponentConroller.Insert_Exam_Components_Details-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}

