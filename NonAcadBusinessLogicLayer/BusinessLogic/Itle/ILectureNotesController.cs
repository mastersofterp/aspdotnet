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
            /// This ILectureNotesController is used to control ACD_ILECTURENOTES table.
            /// </summary>
            public partial class ILectureNotesController
            {
                /// <summary>ConnectionStrings</summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

                public int AddLectureNotes(ILecture objLNote)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        bool flag = true;
                        
                       
                        if (flag == true)
                        {
                            objParams = new SqlParameter[10];
                            objParams[0] = new SqlParameter("@P_SESSIONNO", objLNote.SESSIONNO);
                            objParams[1] = new SqlParameter("@P_COURSENO", objLNote.COURSENO);
                            objParams[2] = new SqlParameter("@P_UA_NO", objLNote.UA_NO);
                            objParams[3] = new SqlParameter("@P_SUB_NO", objLNote.SUB_NO);
                            objParams[4] = new SqlParameter("@P_TOPIC_NAME", objLNote.TOPIC_NAME);
                            objParams[5] = new SqlParameter("@P_DESCRIPTION", objLNote.DESCRIPTION);
                            objParams[6] = new SqlParameter("@P_ATTACHMENT", objLNote.ATTACHMENT);
                            objParams[7] = new SqlParameter("@P_CREATED_DATE", objLNote.CREATED_DATE);
                            objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objLNote.COLLEGE_CODE);
                            objParams[9] = new SqlParameter("@P_NOTE_NO", SqlDbType.Int);
                            objParams[9].Direction = ParameterDirection.Output;

                                              

                            object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ILECTURENOTES", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            /////////Eknath///////////
                            #region Single file attachment
                            // string uploadPath = file_path+("ITLE/upload_files/LectureNotes/");

                            //Upload the File
                            //THIS CODE WAS IN USE BEFORE MULTIPLE FILE ATTACHMENTS
                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{
                            //    if (System.IO.File.Exists(uploadPath +  objLNote.FILENAME ))
                            //    {
                            //        return Convert.ToInt32(CustomStatus.FileExists);
                            //    }
                            //    else
                            //    {
                            //        //string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                            //        string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //        //fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            //        fuFile.PostedFile.SaveAs(uploadPath + "lectureNotes_" + Convert.ToInt32(ret) + uploadFile);
                            //        flag = true;
                            //    }
                            //}
                            //////Eknath
                            #endregion
                           
                            //Upload Multiple Files

                            foreach (LectureNotesAttachment item in objLNote.Attachments)
                            {
                                objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_NOTE_NO", Convert.ToInt32(ret)),
                            new SqlParameter("@P_UA_NO", objLNote.UA_NO),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                             
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                                objSH.ExecuteNonQuerySP("PKG_ITLE_INS_LECTURE_NOTES_ATTACHMENT", objParams, true);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.AddLectureNotes -> " + ex.ToString());
                    }
                    return retStatus;
                }

            

                public int UpdateLectureNote(ILecture objLNote,int note_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        bool flag = true;
                        string uploadPath = file_path + ("ITLE/upload_files/LectureNotes/");

                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        if (flag == true)
                        {
                            //Update Announcement

                            string filename = string.Empty;

                            //if (objIAnnounce.FILENAME.Equals(string.Empty))
                            if (objLNote.ATTACHMENT.Equals(string.Empty))
                                filename = objLNote.OLDFILENAME;
                            else
                                //filename = objIAnnounce.FILENAME;
                                filename = objLNote.ATTACHMENT;

                            objParams = new SqlParameter[10];

                            objParams[0] = new SqlParameter("@P_NOTE_NO", objLNote.NOTE_NO);
                            objParams[1] = new SqlParameter("@P_SUB_NO", objLNote.SUB_NO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objLNote.COURSENO);
                            objParams[3] = new SqlParameter("@P_SESSIONNO", objLNote.SESSIONNO);
                            objParams[4] = new SqlParameter("@P_UA_NO", objLNote.UA_NO);
                            objParams[5] = new SqlParameter("@P_TOPIC_NAME", objLNote.TOPIC_NAME);
                            objParams[6] = new SqlParameter("@P_DESCRIPTION", objLNote.DESCRIPTION);
                            objParams[7] = new SqlParameter("@P_ATTACHMENT", filename);
                            objParams[8] = new SqlParameter("@P_CREATED_DATE", objLNote.CREATED_DATE);
                            objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[9].Direction = ParameterDirection.Output;

                            object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ILECTURENOTES_BY_UANO", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                             //Upload Multiple Files


                            foreach (LectureNotesAttachment item in objLNote.Attachments)
                            {
                                objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_NOTE_NO", note_no),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                            new SqlParameter("@P_UA_NO", objLNote.UA_NO),
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                                objSQL.ExecuteNonQuerySP("PKG_ITLE_INS_LECTURE_NOTES_ATTACHMENT", objParams, true);
                            }


                            //FILE UPLOAD 
                            
                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{

                            //    string oldFileName = string.Empty;
                            //    oldFileName = "lectureNotes_" + objLNote.NOTE_NO + System.IO.Path.GetExtension(objLNote.OLDFILENAME);

                            //    if (System.IO.File.Exists(uploadPath + oldFileName))
                            //    {
                            //        System.IO.File.Delete(uploadPath + oldFileName);
                            //    }

                            //    string uploadFile = "lectureNotes_" + objLNote.NOTE_NO + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //    fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                           



                                //if (System.IO.File.Exists(uploadPath + objIAnnounce.FILENAME))


                                //if (System.IO.File.Exists(uploadPath + objLNote.ATTACHMENT))
                                //{
                                //    return Convert.ToInt32(CustomStatus.FileExists);
                                //}
                                //else
                                //{
                                //    string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                                //    // fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                                //    //Delete Old File
                                //    fuFile.PostedFile.SaveAs(uploadPath + "lectureNotes_" + Convert.ToInt32(ret) + uploadFile);
                                //    if (objLNote.OLDFILENAME != string.Empty)
                                //        System.IO.File.Delete(uploadPath + objLNote.OLDFILENAME);
                                //    flag = true;
                                //}

                            }


                        }

                   
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.UpdateLectureNote-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetAllLectureNotes(ILecture objLNote)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objLNote.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objLNote.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objLNote.UA_NO);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ALLNOTES_BYUANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.GetAllLectureNotes -> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetStudentNotes(ILecture objNOTE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objNOTE.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objNOTE.COURSENO);
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_LECTURENOTES", objParams);
                    
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.GetStudentNotes -> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetStudentNotesBYLNo(ILecture objNOTE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objNOTE.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objNOTE.COURSENO);
                        objParams[2] = new SqlParameter("@P_LNO", objNOTE.NOTE_NO);
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_LECTURENOTESBYLNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.GetStudentNotes -> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetTopicsFromSyllabus(ILecture objLNote)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objLNote.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objLNote.COURSENO);
                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_SYLLABUSTOPIC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.GetTopicsFromSyllabus -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetSingleNoteByNoteNo(ILecture objLNote)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                     //   objParams[0] = new SqlParameter("P_NOTE_NO",objLNote.NOTE_NO);
                     //   objParams[1] = new SqlParameter("P_COURSENO",objLNote.COURSENO);
                     //   objParams[2] = new SqlParameter("P_SESSIONNO",objLNote.SESSIONNO);

                        objParams[0] = new SqlParameter("P_SESSIONNO", objLNote.SESSIONNO);
                        objParams[1] = new SqlParameter("P_COURSENO", objLNote.COURSENO);
                        objParams[2] = new SqlParameter("P_NOTE_NO", objLNote.NOTE_NO);


                        dtr = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_NOTES_BY_NOTENO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.GetSingleNoteByNoteNo-> " + ex.ToString());
                    }
                    return dtr;
                }



                public int DeleteLectureNotes(int ua_no, int sessionno, int courseno, int note_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_NOTE_NO", note_no);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_LECTURENOTES", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.DeleteAnnouncement -> " + ex.ToString());
                    }
                    return retStatus;
                }


                // RETURN FILES ATTACHED BY FACULTY AT THE TIME OF CREATION
                public DataSet GetAllAtachmentByNoteNo(ILecture objLNote, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_NOTE_NO", objLNote.NOTE_NO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objLNote.COURSENO);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objLNote.SESSIONNO);
                        objParams[3] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_LECTURENOTE_FILE_BY_NOTENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //END



                // USED TO GET MULTIPLE ATTACHMENTS AT STUDENT SIDE

                public DataSet GetLectureNotesAttachments(int note_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_NOTE_NO", note_no)
                };
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_LECTURENOTES_ATTACHMENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IMailController.GetAllSentMailsByUaNo -> " + ex.ToString());
                    }
                    return ds;
                }

            }
        }
    }
}

