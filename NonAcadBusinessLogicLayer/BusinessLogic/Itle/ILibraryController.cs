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
            /// This ILibraryController is used to control ILibrary table.
            /// </summary>
            public partial class ILibraryController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];

                /// <summary>
                /// This method is used to add new E-Book/Link in the ILibrary table.
                /// </summary>
                /// <param name="objILib">objILib is the object of ILibrary class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>

                public int AddILibrary(ILibrary objILib)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New e-Book

                            objParams = new SqlParameter[11];
                            objParams[0] = new SqlParameter("@P_UA_NO", objILib.UA_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objILib.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objILib.COURSENO);
                            objParams[3] = new SqlParameter("@P_BOOK_NAME", objILib.BOOK_NAME);
                            objParams[4] = new SqlParameter("@P_AUTHOR_NAME", objILib.AUTHOR_NAME);
                            objParams[5] = new SqlParameter("@P_PUBLISHER_NAME", objILib.PUBLISHER_NAME);
                            objParams[6] = new SqlParameter("@P_ATTACHMENT", objILib.ATTACHMENT);
                            objParams[7] = new SqlParameter("@P_UPLOAD_DATE", objILib.UPLOAD_DATE);
                            objParams[8] = new SqlParameter("@P_WEBSITE_LINK", objILib.WEBSITE_LINK);
                            objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objILib.COLLEGE_CODE);
                            objParams[10] = new SqlParameter("@P_BOOK_NO", SqlDbType.Int);
                            objParams[10].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ELIBRARY", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                             else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            //Upload Multiple Files
                            string uploadPath = file_path + "ITLE/UPLOAD_FILES/ILIBRARY/";

                            foreach (ELibraryAttachment item in objILib.Attachments)
                            {
                                objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_BOOK_NO", Convert.ToInt32(ret)),
                            new SqlParameter("@P_UA_NO", objILib.UA_NO),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                             
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                                objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INS_ELIBRARY_BOOK_ATTACHMENT", objParams, true);
                            }

                            #region Single file attachment
                            //Upload the File
                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{
                            //    if (System.IO.File.Exists(uploadPath + objILib.FILENAME))
                            //    {
                            //        //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                            //        return Convert.ToInt32(CustomStatus.FileExists);
                            //    }
                            //    else
                            //    {
                            //        string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //        fuFile.PostedFile.SaveAs(uploadPath + "ILIBRARY_" + Convert.ToInt32(ret) + uploadFile);
                            //        flag = true;
                            //     }
                            //}
                            # endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.AddILibrary -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateILibrary(ILibrary objILib, int book_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            //Add New e-Book

                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_BOOK_NO", objILib.BOOK_NO);
                            objParams[1] = new SqlParameter("@P_BOOK_NAME", objILib.BOOK_NAME);
                            objParams[2] = new SqlParameter("@P_AUTHOR_NAME", objILib.AUTHOR_NAME);
                            objParams[3] = new SqlParameter("@P_PUBLISHER_NAME", objILib.PUBLISHER_NAME);
                            objParams[4] = new SqlParameter("@P_ATTACHMENT", objILib.ATTACHMENT);
                            objParams[5] = new SqlParameter("@P_UPLOAD_DATE", objILib.UPLOAD_DATE);
                            objParams[6] = new SqlParameter("@P_WEBSITE_LINK", objILib.WEBSITE_LINK);
                            objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ELIBRARY", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if(Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                           

                            //Upload Multiple Files
                            string uploadPath = file_path + "ITLE/UPLOAD_FILES/ILIBRARY/";


                            foreach (ELibraryAttachment item in objILib.Attachments)
                            {
                                objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_BOOK_NO", book_no),
                            new SqlParameter("@P_FILE_NAME", item.FileName),
                            new SqlParameter("@P_UA_NO", objILib.UA_NO),
                            new SqlParameter("@P_FILE_PATH", item.FilePath),
                            new SqlParameter("@P_SIZE", item.Size),
                            new SqlParameter("@P_ATTACHMENT_ID", item.AttachmentId)
                        };
                                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;
                                objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INS_ELIBRARY_BOOK_ATTACHMENT", objParams, true);
                            }


                            ////Upload the File
                            //if (!fuFile.FileName.Equals(string.Empty))
                            //{
                            //    string oldFileName = string.Empty;
                            //    oldFileName = "ILIBRARY_" + objILib.BOOK_NO + System.IO.Path.GetExtension(objILib.OLDFILENAME);
                                
                            //    if (System.IO.File.Exists(uploadPath + oldFileName))
                            //      {
                            //        System.IO.File.Delete(uploadPath + oldFileName);
                            //      }

                            //    string uploadFile = "ILIBRARY_" + objILib.BOOK_NO+ System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            //   fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                           
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.AddILibrary -> " + ex.ToString());
                    }

                    return retStatus;
                }



                /// <summary>
                /// This method is used to retrieve all records from ASSIGNMASTER table by logged in user.
                /// </summary>
                /// <param name="objAssign">object of Assignment Class</param>                
                public DataSet GetAllEbooksByUaNo(ILibrary objLib)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objLib.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objLib.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objLib.UA_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ALLBOOKS_BYUA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetAllEbooksByUaNo -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This method is used to retrieve single assignment from ASSIGNMASTER table.
                /// </summary>
                /// <param name="newsid">Retrieve single assignment as per the passed assignno,courseno,sessionno and uano.</param>
                /// <returns>DataTableReader</returns>               
                public DataTableReader GetSingleEBook(int bookno, int courseno, int sessionno, int uano)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BOOK_NO", bookno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_UA_NO", uano);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEBOOK_BY_BOOKNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetSingleEBook -> " + ex.ToString());
                    }
                    return dtr;
                }

                public int DeleteEBookByUaNo( ILibrary objILib)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objILib.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objILib.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objILib.UA_NO);
                        objParams[3] = new SqlParameter("@P_BOOK_NO", objILib.BOOK_NO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_ELIBRARY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.DeleteEBookByUaNo -> " + ex.ToString());
                    }

                    return retStatus;
                }


                // RETURN FILES ATTACHED BY FACULTY AT THE TIME OF CREATION
                public DataSet GetAllAtachmentByBookNo(int book_no,int courseno,int sessionno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BOOK_NO", book_no);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ELIBRARY_FILE_BY_NOTENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.GetAllAssignmentListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //END


                // USED TO GET MULTIPLE ATTACHMENTS AT STUDENT SIDE

                public DataSet GetEBookAttachments(int book_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_BOOK_NO", book_no)
                };
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_EBOOK_ATTACHMENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetEBookAttachments -> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetStudentEbookByBookNo(ILibrary objLib)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objLib.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objLib.COURSENO);
                        objParams[2] = new SqlParameter("@P_BOOK_NO", objLib.BOOK_NO);
                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_EBOOK_BY_BOOK_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILectureNotesController.GetStudentNotes -> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}
