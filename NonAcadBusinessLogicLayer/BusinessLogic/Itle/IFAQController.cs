using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IITMS;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Web;
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessLogic
        {
            public class IFAQController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddFAQustion(IFAQMaster objFrequent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New Assignment
                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_SESSIONNO", objFrequent.SESSIONNO);
                            objParams[1] = new SqlParameter("@P_COURSENO", objFrequent.COURSENO);
                            objParams[2] = new SqlParameter("@P_SUBJECT", objFrequent.SUBJECT);
                            objParams[3] = new SqlParameter("@P_QUESTION", objFrequent.QUESTION);
                            objParams[4] = new SqlParameter("@P_CREATE_DATE", objFrequent.CREATED_DATE);

                            objParams[5] = new SqlParameter("@P_IDNO", objFrequent.IDNO);
                            objParams[6] = new SqlParameter("@P_UA_NO", objFrequent.UA_NO);
                            objParams[7] = new SqlParameter("@P_QUES_NO", SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_FAQ", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);




                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.AddAssignment-> " + ex.ToString());
                    }




                    return retStatus;
                }



                public int UpdateFAQustion(IFAQMaster objFrequent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New Assignment
                            objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@P_QUES_NO", objFrequent.QUES_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objFrequent.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objFrequent.COURSENO);
                            objParams[3] = new SqlParameter("@P_SUBJECT", objFrequent.SUBJECT);
                            objParams[4] = new SqlParameter("@P_QUESTION", objFrequent.QUESTION);
                            objParams[5] = new SqlParameter("@P_CREATE_DATE", objFrequent.CREATED_DATE);

                            objParams[6] = new SqlParameter("@P_IDNO", objFrequent.IDNO);
                            objParams[7] = new SqlParameter("@P_UA_NO", objFrequent.UA_NO);
                            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[8].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPDATE_FAQ", objParams, true);



                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.AddAssignment-> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GetAllAssignmentListByUaNo(int session, int courseno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_ID_NO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_FAQ_BYID_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetSingleFAQustion(int assignno, int courseno, int sessionno, int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("P_QUES_NO", assignno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_ID_NO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEFAQ_BY_QUESNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }



                //ANSWERED REPLIED BY FACYLTY

                public DataSet GetAllFAQByCourseNo(int session, int courseno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_FAQ_BYCOURSE_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public int ReplyAssignment(IFAQMaster objFrequent, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        bool flag = true;
                        if (flag == true)
                        {
                            //Reply Assignment
                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_QUES_NO", objFrequent.QUES_NO);
                            objParams[1] = new SqlParameter("@P_UA_NO", objFrequent.UA_NO);
                            objParams[2] = new SqlParameter("@P_IDNO", objFrequent.IDNO);
                            objParams[3] = new SqlParameter("@P_REPLY_DATE", objFrequent.REPLY_DATE);
                            objParams[4] = new SqlParameter("@P_ANSWER_REPLY", objFrequent.ANSWER_REPLY);
                            objParams[5] = new SqlParameter("@P_ATTACHMENT", objFrequent.ATTACHMENT);
                            objParams[6] = new SqlParameter("@P_STATUS", objFrequent.STATUS);
                            objParams[7] = new SqlParameter("@P_FQUES_NO", SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_FAQ_REPLY", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                            string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/UPLOAD_FILES/FAQ/");
                            //Upload the File
                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                if (System.IO.File.Exists(uploadPath + objFrequent.ATTACHMENT))
                                {
                                    //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                                    return Convert.ToInt32(CustomStatus.FileExists);
                                }
                                else
                                {
                                    string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                    fuFile.PostedFile.SaveAs(uploadPath + "FAQ_" + Convert.ToInt32(ret) + uploadFile);
                                    //flag = true;
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.ReplyAssignmenr-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetSingleFAQForFaculty(int quesno, int ua_no)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_QUES_NO", quesno);
                        objParams[1] = new SqlParameter("P_UA_NO", ua_no);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLE_FAQ_FOR_FACULTY", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }


                public object GetSingleFAQCheckStatusForFaculty(int quesno, int ua_no)
                {


                    Object objStatus;//= new object();

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_QUES_NO", quesno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);

                        //bytStatus  = objSQLHelper.ExecuteScalarSP("PKG_ITLE_SP_RET_SINGLEASSIGNMENT_CHECK_STATUS_FOR_STUDENT", objParams).Tables[0].CreateDataReader();
                        objStatus = objSQLHelper.ExecuteScalarSP("PKG_ITLE_SP_RET_SINGLEASSIGNMENT_CHECK_STATUS_FOR_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return objStatus;
                }

                public int DeleteFAQ(IFAQMaster objFrequent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objFrequent.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objFrequent.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objFrequent.UA_NO);
                        objParams[3] = new SqlParameter("@P_QUES_NO", objFrequent.QUES_NO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_FAQ", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.DeleteAssignment -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GetAllFAQListByIdNo(int session, int courseno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_ID_NO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_FAQ_BYUA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetFAQAnswer(int assignno, int courseno, int sessionno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("P_QUES_NO", assignno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);


                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_FAQANSWER_BY_QUESNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }


                //FOR DISPLAYING TOTAL NUMBER OF QUESTIONS IN  FAQ

                public DataSet TotalQuestions(IFAQMaster objFrequent)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", objFrequent.COURSENO);
                        objParams[1] = new SqlParameter("@P_ID_NO", objFrequent.IDNO);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_TOTAL_QUESTIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                    }
                    return ds;
                }



            }
        }
    }
}

