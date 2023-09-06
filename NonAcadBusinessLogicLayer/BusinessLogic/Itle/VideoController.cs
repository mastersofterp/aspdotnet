using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;


namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            public partial class VideoController
            {

                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;


                public int AddVideos(Video objV,System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retstaus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        bool flag = true;
                        if (flag == true)
                        {
                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_TITLE", objV.TITLE);
                            objParams[1] = new SqlParameter("@P_PUBLISHER_NAME", objV.PUBLISHER_NAME);
                            objParams[2] = new SqlParameter("@P_DESCRIPTION", objV.DESCRIPTION);
                            objParams[3] = new SqlParameter("@P_COURSENO", objV.COURSENO);
                            objParams[4] = new SqlParameter("@P_UANO", objV.UANO);
                            objParams[5] = new SqlParameter("@P_TYPE", objV.TYPE);
                            objParams[6] = new SqlParameter("@P_FILENAME", objV.FILENAME);
                            objParams[7] = new SqlParameter("@P_SR_NO", objV.SR_NO);
                            objParams[7].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_VIDEO_INSERT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retstaus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retstaus = Convert.ToInt32(CustomStatus.RecordSaved);


                            string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/UPLOAD_FILES/IVIDEOS/");
                            //UPLOAD FILE
                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                if (System.IO.File.Exists(uploadPath + objV.FILENAME))
                                {
                                    return Convert.ToInt32(CustomStatus.FileExists);
                                }

                                else
                                {
                                    string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                    fuFile.PostedFile.SaveAs(uploadPath + "IVIDEOS_" + Convert.ToInt32(ret) + uploadFile);
                                    flag = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retstaus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VideoController.AddVideos->" + ex.ToString());
                    }
                    return retstaus;
                }

                public int UpdVideos(Video objV,System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retstaus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@P_TITLE", objV.TITLE);
                            objParams[1] = new SqlParameter("@P_PUBLISHER_NAME", objV.PUBLISHER_NAME);
                            objParams[2] = new SqlParameter("@P_DESCRIPTION", objV.DESCRIPTION);
                            objParams[3] = new SqlParameter("@P_COURSENO", objV.COURSENO);
                            objParams[4] = new SqlParameter("@P_UANO", objV.UANO);
                            objParams[5] = new SqlParameter("@P_FILENAME", objV.FILENAME);
                            objParams[6] = new SqlParameter("@P_SR_NO", objV.SR_NO);
                            objParams[7] = new SqlParameter("@P_TYPE", objV.TYPE);
                            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[8].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_VIDEO_UPD", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retstaus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retstaus = Convert.ToInt32(CustomStatus.RecordSaved);

                            string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/UPLOAD_FILES/IVIDEOS/"); ;
                            // UPLOAD FILE

                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                string oldFilename = string.Empty;
                                oldFilename = "IVIDEOS_" + objV.SR_NO + System.IO.Path.GetExtension(objV.OLDFILENAME);
                                if (System.IO.File.Exists(uploadPath + oldFilename))
                                {
                                    System.IO.File.Delete(uploadPath + oldFilename);
                                }
                                string uploadFile = "IVIDEOS_" + objV.SR_NO + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            }
                        }
                           
                    }
                    catch (Exception ex)
                    {
                        retstaus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VideoController.AddVideos->" + ex.ToString());
                    }
                    return retstaus;
                }
                public DataSet GetVideoAll(Video objV)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", objV.COURSENO);
                        objParams[1] = new SqlParameter("@P_UANO", objV.UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_VIDEO_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VideoController.GetVideoAll->" + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteVideo(Video objV)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSENO", objV.COURSENO);
                        objParams[1] = new SqlParameter("@P_UANO", objV.UANO);
                        objParams[2] = new SqlParameter("@P_SR_NO", objV.SR_NO);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_VIDEO_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VideoController.DeleteVideo->" + ex.ToString());
                    }
                    return retstatus;
                        
                }

                //public int CalendarIns(Video objV)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_IDNO",objV.IDNO );
                //        objParams[1] = new SqlParameter("@P_TITLE",objV.TITLE);
                //        objParams[2] = new SqlParameter("@P_DATE",objV.DATETIME);
                //        objParams[3] = new SqlParameter("@P_TIME",objV.TIME);
                //        objParams[4] = new SqlParameter("@P_BODY",objV.BODY);
                //        objParams[5] = new SqlParameter("@P_COLLEGE_CODE",objV.COLLEGE_CODE);
                //        objParams[6] = new SqlParameter("@P_SRNO",SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;
                //        object ret = objHelper.ExecuteNonQuerySP("", objParams, true);
                //        if (Convert.ToInt32(ret)==-99)

                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VideoController.CalIns->" + ex.ToString());
                //    }
                //    return retstatus;

                //}
                public DataSet GetVideoByID(Video objV)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3]; ;
                        objParams[0] = new SqlParameter("@P_SR_NO", objV.SR_NO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objV.COURSENO);
                        objParams[2] = new SqlParameter("@P_UANO", objV.UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_VIDEO_GETBY_ID", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VideoController.GetVideoByID->" + ex.ToString());
                    }
                    return ds;
                }
            }

        }
    }
}
