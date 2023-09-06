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
            /// This ISyllabusController is used to control ACD_ISYLLABUS table.
            /// </summary>
            public partial class ISyllabusController
            {
                /// <summary>ConnectionStrings</summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

                public int AddSyllabus(ISyllabus objSub, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                         bool flag = true;
                        if (flag == true)
                        {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSub.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objSub.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objSub.UA_NO);
                        objParams[3] = new SqlParameter("@P_SYLLABUS_NAME", objSub.SYLLABUS_NAME);
                        objParams[4] = new SqlParameter("@P_UNIT_NAME", objSub.UNIT_NAME);
                        objParams[5] = new SqlParameter("@P_TOPIC_NAME", objSub.TOPIC_NAME);
                        objParams[6] = new SqlParameter("@P_DESCRIPTION", objSub.DESCRIPTION);
                        objParams[7] = new SqlParameter("@P_CREATED_DATE", objSub.CREATED_DATE);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSub.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_ATTACHMENT", objSub.ATTACHMENT);
                        objParams[10] = new SqlParameter("@P_Is_Blob", objSub.IsBlob);
                        objParams[11] = new SqlParameter("@P_FilePath", objSub.FILE_PATH);
                        objParams[12] = new SqlParameter("@P_SUB_NO", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ISYLLABUS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        string uploadPath = file_path+("ITLE/UPLOAD_FILES/SYLLABUS/");
                        //Upload the File
                        
                        if (!fuFile.Equals(string.Empty))
                        {
                            if (System.IO.File.Exists(uploadPath + objSub.FILENAME))
                            {
                                //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                                return Convert.ToInt32(CustomStatus.FileExists);
                            }
                            else
                            {
                                string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + "SYLLABUS_" + Convert.ToInt32(ret) + uploadFile);
                                flag = true;
                            }
                        }
                    }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ISyllabusController.AddSyllabus -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSyllabus(ISyllabus objSub, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSub.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objSub.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objSub.UA_NO);
                        objParams[3] = new SqlParameter("@P_SYLLABUS_NAME", objSub.SYLLABUS_NAME);
                        objParams[4] = new SqlParameter("@P_UNIT_NAME", objSub.UNIT_NAME);
                        objParams[5] = new SqlParameter("@P_TOPIC_NAME", objSub.TOPIC_NAME);
                        objParams[6] = new SqlParameter("@P_DESCRIPTION", objSub.DESCRIPTION);
                        objParams[7] = new SqlParameter("@P_ATTACHMENT", objSub.ATTACHMENT);
                        objParams[8] = new SqlParameter("@P_SUB_NO",objSub.SUB_NO);
                        objParams[9] = new SqlParameter("@P_Is_Blob", objSub.IsBlob);
                        objParams[10] = new SqlParameter("@P_FilePath", objSub.FILE_PATH);
                        
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                                                
                        object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ISYLLABUS_BYUANO", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        string uploadPath = file_path + ("ITLE/UPLOAD_FILES/SYLLABUS/");

                        if (!fuFile.FileName.Equals(string.Empty))
                        {
                            string oldFileName = string.Empty;
                            oldFileName = "SYLLABUS_" + objSub.SUB_NO + System.IO.Path.GetExtension(objSub.OLDFILENAME);

                            if (System.IO.File.Exists(uploadPath + oldFileName))
                            {
                                System.IO.File.Delete(uploadPath + oldFileName);
                            }

                            string uploadFile = "SYLLABUS_" + objSub.SUB_NO + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                            fuFile.PostedFile.SaveAs(uploadPath + uploadFile);

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ISyllabusController.UpdateSyllabus -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSyllabusByUaNo(ISyllabus objSub)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSub.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objSub.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objSub.UA_NO);

                        ds = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ISYLLABUS_BYUANO", objParams);
                    }
                    catch (Exception ex)
                    {                    
                    }
                    return ds;
                }

                public DataTableReader GetSyllabusViewBySession(ISyllabus objSub)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSub.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objSub.COURSENO);

                        dtr = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ISYLLABUS_BY_SESSIONANDCOURSE", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ISyllabusController.GetSyllabusViewBySession -> " + ex.ToString());
                    }
                    return dtr;
                }


                public DataTableReader GetSingleSyllabus(int sub_no, int courseno, int sessionno, int uano)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SUB_NO", sub_no);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_UA_NO", uano);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLE_SYLLABUS_BY_SUBNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetSingleEBook -> " + ex.ToString());
                    }
                    return dtr;
                }

                public int DeleteSyllabus(int ua_no, int sessionno, int courseno, int sub_no)
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
                        objParams[3] = new SqlParameter("@P_SUB_NO", sub_no);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_SYLLABUS", objParams, true);
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


                /// <summary>
                /// Added By Dileep Kare
                /// Date:13-01-2020
                /// </summary>
                /// <param name="IDNO"></param>
                /// <returns></returns>
                public DataSet GetUploadSyllabusByUaNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);


                        ds = objSH.ExecuteDataSetSP("PKG_GET_UPLOAD_SYLLABUS", objParams);
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
