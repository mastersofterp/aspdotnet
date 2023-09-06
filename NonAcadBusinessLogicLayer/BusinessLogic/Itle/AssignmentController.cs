using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Collections.Generic;

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
            public partial class AssignmentController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

                /// <summary>
                /// This method is used to add new assignment in the ASSIGNMASTER table.
                /// </summary>
                /// <param name="objAssign">objAssign is the object of Assignment class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                public int AddAssignment(Assignment objAssign, string idno, int OrgId)
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
                            objParams = new SqlParameter[20];
                            objParams[0] = new SqlParameter("@P_UA_NO", objAssign.UA_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objAssign.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objAssign.COURSENO);
                            objParams[3] = new SqlParameter("@P_SUBJECT", objAssign.SUBJECT);
                            objParams[4] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[5] = new SqlParameter("@P_ATTACHMENT", objAssign.ATTACHMENT);
                            objParams[6] = new SqlParameter("@P_ASSIGNDATE", objAssign.ASSIGNDATE);

                            if (objAssign.SUBMITDATE == DateTime.MinValue)
                            {
                                objParams[7] = new SqlParameter("@P_SUBMITDATE", DBNull.Value);
                            }
                            else
                            {
                                objParams[7] = new SqlParameter("@P_SUBMITDATE", objAssign.SUBMITDATE);
                            }
                           
                            objParams[8] = new SqlParameter("@P_STATUS", objAssign.STATUS);
                            objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objAssign.COLLEGE_CODE);
                            objParams[10] = new SqlParameter("@P_ASSIGNMENT_MARKS", objAssign.ASSIGNMENT_TOALMARKS);
                            objParams[11] = new SqlParameter("@P_IDNO", idno);
                            objParams[12] = new SqlParameter("@P_Org_ID", OrgId);

                            objParams[13] = new SqlParameter("@P_DUEDATE", objAssign.DUEDATE);
                            objParams[14] = new SqlParameter("@P_SUBMISSION_FROM_DATE", objAssign.SUBMITFROMDATE);

                            objParams[15] = new SqlParameter("@P_DOC_TYPE_ID", objAssign.Doc_Type_Id);
                            objParams[16] = new SqlParameter("@P_MAX_NO_OF_DOC", objAssign.MAX_NO_OF_FILE);
                            objParams[17] = new SqlParameter("@P_FILE_TYPE", objAssign.File_type);
                            objParams[18] = new SqlParameter("@P_REMIND_DATE", objAssign.REMINDME_DATE);


                            objParams[19] = new SqlParameter("@P_AS_NO", SqlDbType.Int);
                            objParams[19].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ASSIGNMENT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/upload_files/assignment/");

                            //Upload the File

                            foreach (AssignmentAttachment item in objAssign.Attachments)
                    {
                        objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_AS_NO", Convert.ToInt32(ret)),
                            new SqlParameter("@P_UA_NO", objAssign.UA_NO),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                             
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INS_ASSIGNMENT_ATTACHMENT", objParams, true);
                    }


                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{
                            //    if (System.IO.File.Exists(uploadPath + objAssign.FILENAME))
                            //    {
                            //        return Convert.ToInt32(CustomStatus.FileExists);
                            //    }
                            //    else
                            //    {
                            //        //string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                            //        string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //        //fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            //        fuFile.PostedFile.SaveAs(uploadPath + "assignment_" + Convert.ToInt32(ret) + uploadFile);
                            //        flag = true;
                            //    }
                            //}


                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.AddAssignment-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method is used to update record in the ASSIGNMASTER table
                /// </summary>
                /// <param name="objAssign">object of Assignment Class</param>
                /// <param name="fuFile">File to be uploaded</param>
                /// <returns>Integer CustomStatus - Record Updated or Error</returns>
                public int UpdateAssignment(Assignment objAssign, string idno,int asno, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            objParams = new SqlParameter[20];
                            objParams[0] = new SqlParameter("@P_AS_NO", objAssign.AS_NO);
                            objParams[1] = new SqlParameter("@P_UA_NO", objAssign.UA_NO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objAssign.COURSENO);
                            objParams[3] = new SqlParameter("@P_SESSIONNO", objAssign.SESSIONNO);
                            objParams[4] = new SqlParameter("@P_SUBJECT", objAssign.SUBJECT);
                            objParams[5] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[6] = new SqlParameter("@P_ATTACHMENT", objAssign.ATTACHMENT);
                            objParams[7] = new SqlParameter("@P_ASSIGNDATE", objAssign.ASSIGNDATE);

                            if (objAssign.SUBMITDATE == DateTime.MinValue)
                            {
                                objParams[8] = new SqlParameter("@P_SUBMITDATE", DBNull.Value);
                            }
                            else
                            {
                                objParams[8] = new SqlParameter("@P_SUBMITDATE", objAssign.SUBMITDATE);
                            }
                           

                            
                            objParams[9] = new SqlParameter("@P_STATUS", objAssign.STATUS);
                            objParams[10] = new SqlParameter("@P_ASSIGNMENT_MARKS", objAssign.ASSIGNMENT_TOALMARKS);
                            objParams[11] = new SqlParameter("@P_IDNO", idno);
                            objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objAssign.COLLEGE_CODE);

                            objParams[13] = new SqlParameter("@P_Org_ID", OrgId);

                            objParams[14] = new SqlParameter("@P_DUEDATE", objAssign.DUEDATE);
                            objParams[15] = new SqlParameter("@P_SUBMISSION_FROM_DATE", objAssign.SUBMITFROMDATE);

                            objParams[16] = new SqlParameter("@P_DOC_TYPE_ID", objAssign.Doc_Type_Id);
                            objParams[17] = new SqlParameter("@P_MAX_NO_OF_DOC", objAssign.MAX_NO_OF_FILE);
                            objParams[18] = new SqlParameter("@P_REMIND_DATE", objAssign.REMINDME_DATE);   

                            
                            objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[19].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ASSIGNMENT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)                            
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);                           
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                            //Upload the File


                            foreach (AssignmentAttachment item in objAssign.Attachments)
                            {
                                objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_AS_NO", asno),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                            new SqlParameter("@P_UA_NO", objAssign.UA_NO),
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                                objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INS_ASSIGNMENT_ATTACHMENT", objParams, true);
                            }

                            //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/upload_files/assignment/");
                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{
                            //    //Update Assignment
                            //    string oldFileName = string.Empty;
                            //    oldFileName = "assignment_" + objAssign.AS_NO + System.IO.Path.GetExtension(objAssign.OLDFILENAME);

                            //    if (System.IO.File.Exists(uploadPath + oldFileName))
                            //    {
                            //        System.IO.File.Delete(uploadPath + oldFileName);
                            //    }

                            //    string uploadFile = "assignment_" + objAssign.AS_NO + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //    fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            //}

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.UpdateAssignment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to retrieve all records from ASSIGNMASTER table by logged in user.
                /// </summary>
                /// <param name="objAssign">object of Assignment Class</param>                
                public DataSet GetAllAssignmentListByUaNo(int session, int courseno, int uano, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", uano);
                        objParams[3] = new SqlParameter("@P_Org_ID", OrgId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ASSIGNMENT_BYUA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllAssignmentListByCourseNo(int session, int courseno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ASSIGNMENT_BYCOURSE_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetAllAssignmentNotificatios(int session, int id_no, int usertype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);                        
                        objParams[1] = new SqlParameter("@P_ID_NO", id_no);
                        objParams[2] = new SqlParameter("@P_USER_TYPE", usertype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ASSIGNMENT_NOTIFICATIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// This method is used to retrieve single assignment from ASSIGNMASTER table.
                /// </summary>
                /// <param name="newsid">Retrieve single assignment as per the passed assignno,courseno,sessionno and uano.</param>
                /// <returns>DataTableReader</returns>               
                public DataTableReader GetSingleAssignment(int assignno, int courseno, int sessionno, int uano, int OrgId)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("P_AS_NO", assignno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_UA_NO", uano);
                        objParams[4] = new SqlParameter("@P_Org_ID", OrgId);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEASSIGNMENT_BY_ASNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }

                public int DeleteAssignment(Assignment objAssign)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAssign.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objAssign.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objAssign.UA_NO);
                        objParams[3] = new SqlParameter("@P_AS_NO", objAssign.AS_NO);
                        objParams[4] = new SqlParameter("@P_OrganizationId", objAssign.OrganizationId);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_ASSIGNMENT", objParams, true);

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

                //public DataTableReader GetSingleAssignment(int assignno, int courseno, int sessionno, int uano)
                //{
                //    DataTableReader dtr = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("P_AS_NO", assignno);
                //        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                //        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                //        objParams[3] = new SqlParameter("P_UA_NO", uano);

                //        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEASSIGNMENT_BY_ASNO", objParams).Tables[0].CreateDataReader();
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                //    }
                //    return dtr;
                //}

                public DataTableReader GetSingleAssignmentForStudent(int assignno, int ua_no)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("P_AS_NO", assignno);
                        objParams[1] = new SqlParameter("P_UA_NO", ua_no);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEASSIGNMENT_BY_FOR_STUDENT", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }


                public object GetSingleAssignmentCheckStatusForStudent(int assignno, int ua_no)
                {


                    Object objStatus;//= new object();

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_AS_NO", assignno);
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





                public int ReplyAssignment(Assignment objAssign, System.Web.UI.WebControls.FileUpload fuFile)
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
                            objParams[0] = new SqlParameter("@P_AS_NO", objAssign.AS_NO);
                            objParams[1] = new SqlParameter("@P_UA_NO", objAssign.UA_NO);
                            objParams[2] = new SqlParameter("@P_IDNO", objAssign.IDNO);
                            objParams[3] = new SqlParameter("@P_REPLY_DATE", objAssign.REPLY_DATE);
                            objParams[4] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[5] = new SqlParameter("@P_ATTACHMENT", objAssign.ATTACHMENT);
                            objParams[6] = new SqlParameter("@P_STATUS", objAssign.STATUS);
                            objParams[7] = new SqlParameter("@P_SA_NO", SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ASSIGNMENT_REPLY", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);



                            //Upload the File

                            foreach (AssignmentAttachment item in objAssign.Attachments)
                            {
                                objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_AS_NO", Convert.ToInt32(ret)),
                            new SqlParameter("@P_ID_NO", objAssign.IDNO),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                             
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                                objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INS_STUDASSIGNMENT_REPLY_ATTACHMENT", objParams, true);
                            }

                            //string uploadPath = file_path + ("ITLE/UPLOAD_FILES/student_assignment_reply/");
                            ////Upload the File
                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{
                            //    string uploadFile = "assignment_reply" + Convert.ToInt32(ret) + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //    fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            //    flag = true;
                            //}
                        }


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.ReplyAssignmenr-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetStudent_Assignment_ReplyListByAs_No(int as_no)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AS_NO", as_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_STUDENT_ASSIGNMENT_REPLY_BY_AS_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetStudent_Assignment_ReplyListByAs_No-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataTableReader GetSingleAssignmentStudentReply(int sa_no)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SA_NO", sa_no);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEASSIGNMENT_STUDENT_REPLY_BY_SA_NO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }

                public int RemarkAssignmentReply(Assignment objAssign)
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
                            objParams = new SqlParameter[7];
                            objParams[0] = new SqlParameter("@P_SA_NO", objAssign.SA_NO);
                            objParams[1] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[2] = new SqlParameter("@P_STATUS", objAssign.STATUS);
                            objParams[3] = new SqlParameter("@P_CHECKED", objAssign.CHECKED);
                            objParams[4] = new SqlParameter("@P_STUDENT_MARKS", objAssign.ASSIGNMENTMARKS_STUDENT_OBTAINED);
                            objParams[5] = new SqlParameter("@P_DISPLAY_MARKS", objAssign.DISPLAY_MARKS);
                            objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[6].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ASSIGNMENT_REPLY_REMARK", objParams, true);
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





                //FOR BULK STUDENT LOGING CREATEION
                // DATE : 07/02/2014
                //COPPIED FROM CCMS_ITLE  BY ZUBAIR

                #region Bulk Login Creation
                public DataSet GetStudentTeacherDivisionWise(int SESSIONNO, int DEGREENO, int BRANCHNO, int SCHEMENO, int SEMNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);

                        objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", SEMNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_STUDENT_TEACHER_DIVISIONWISE_FOR_BULKLOGIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLayer.BusinessLogic.ExamController.GetSubjectTeacherDivisionWise -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetMenuItems(int MODULEID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MODULEID", MODULEID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_MENU_ITEMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLayer.BusinessLogic.ExamController.GetMenuItems -> " + ex.ToString());
                    }
                    return ds;
                }

                public long BulkStudLogin(string IdNo, string pwd, string menuid)
                {
                    long Pkid = 0;
                    byte[] photo = new byte[5];
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_STUDPWD", pwd);
                        objParams[2] = new SqlParameter("@P_MENUID", menuid);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_CREATE_BULK_STUD_LOGIN", objParams, true);
                        if (ret != null)
                        {
                            Pkid = Convert.ToInt64(ret.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ExamController.BulkStudLogin-> " + ex.ToString());
                    }
                    return Pkid;

                }
                #endregion

                //END LOGING




                //FOR SMS SENDING TO STUDENTS

                public int SendSMSofAssignment(Assignment objAssign, string idno)
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
                            objParams = new SqlParameter[4];
                            //objParams[0] = new SqlParameter("@P_UA_NO", objAssign.UA_NO);
                            objParams[0] = new SqlParameter("@P_AS_NO", objAssign.AS_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objAssign.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objAssign.COURSENO);
                            //objParams[3] = new SqlParameter("@P_SUBJECT", objAssign.SUBJECT);
                            //objParams[4] = new SqlParameter("@P_DESCRIPTION", objAssign.DESCRIPTION);
                            objParams[3] = new SqlParameter("@P_IDNO", idno);
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SEND_SMS_ABOUT_ASSIGNMENT", objParams, true);
                            if (ret.ToString() == "-99")
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.SendSMSofAssignment-> " + ex.ToString());
                    }

                    return retStatus;

                    //END SMS

                }



                // USED TO GET MULTIPLE ATTACHMENTS AT STUDENT SIDE

                public DataSet GetAssignmentAttachments(int assignno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_AS_NO", assignno)
                };
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_ASSIGNMENT_ATTACHMENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
                    }
                    return ds;
                }


                // RETURN FILES ATTACHED BY FACULTY AT THE TIME OF ASSIGNMETN CREATION
                public DataSet GetAllAtachmentByAsNo(int assignno, int courseno, int sessionno, int uano, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("P_AS_NO", assignno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_UA_NO", uano);
                        objParams[4] = new SqlParameter("@P_Org_ID", OrgId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ASSIGNMENT_FILE_BY_ASNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //END


                // RETURN FILES ATTACHED BY STUDENT AT THE TIME OF ASSIGNMENT REPLY

                public DataSet GetAllAtachmentRepliedByStud(int assignno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_AS_NO", assignno);
                        //objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        //objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_ID_NO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_STUDASSIGN_REPLY_FILE_BY_ASNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }
                //END


                //ASSIGNMENT REPLIED BY STUDENT

                public DataTableReader GetAssignmentRepliedByStudent(int assignno, int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("P_AS_NO", assignno);
                        objParams[1] = new SqlParameter("P_ID_NO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ASSIGNMENT_REPLIED_BY_STUDENT", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }
                //END


                // USED TO GET MULTIPLE ATTACHMENTS AT FACULTY SIDE WHICH ARE REPLIED BY STUDENT

                public DataSet GetStudentAttachments(int sa_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SA_NO", sa_no)
                };
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_STUDENT_ATTACHMENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader DisplayAssignMarks(int as_no, int courseno, int sessionno,int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_AS_NO", as_no);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);


                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_DISPLAY_ASSIGNMENT_MARKS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetSingleAssignment-> " + ex.ToString());
                    }
                    return dtr;
                }
                public DataSet GetAllStudentAssignmentResult_Of_All_Student(string session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_ASSIGNMENT_MARKS_OF_ALL_STUDENT_RPT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }



                public int AddExamMapping(Assignment objAssign,int SRNO)
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
                            objParams = new SqlParameter[6];
                            objParams[0] = new SqlParameter("@P_SESSION_NO", objAssign.SESSIONNO);
                            objParams[1] = new SqlParameter("@p_COURSENO", objAssign.COURSENO);
                            objParams[2] = new SqlParameter("@P_OrganizationId", objAssign.OrganizationId);
                            objParams[3] = new SqlParameter("@P_Exam_Map_Table", objAssign.ExamMapTbl);
                            objParams[4] = new SqlParameter("@P_SR_NO", SRNO);

                            objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[5].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INSUPD_EXAM_MAPPING_DATA", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
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

                public DataSet GetAllExamMappingByCourseNo(int sessionno,int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_EXAM_MAPPING_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetSingleExamMappingData(int SRNO,int sessionno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SR_NO", SRNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_SINGLE_EXAM_MAPPING_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetExamMappingStudentList(int sessionno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_MAPPEDEXAM_STUDENTLIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public int TransferAssignmentMark(int SESSIONNO, int COLLEGEID,int Exam_Id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        bool flag = true;
                        if (flag == true)
                        {
                            
                            objParams = new SqlParameter[4];
                            objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                            objParams[1] = new SqlParameter("@P_COURSENO", COLLEGEID);
                            objParams[2] = new SqlParameter("@P_EXAM_ID", Exam_Id);

                            objParams[3] = new SqlParameter("@R_OUT", SqlDbType.Int);
                            objParams[3].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_TRANSFER_STUDENTMARK", objParams, true);
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

            }
        }
    }
}
