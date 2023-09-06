using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Text;
using System.Data.SqlClient;
using IITMS.NITPRM;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This IAnnouncementController is used to control ACD_IANNOUNCEMASTER table.
            /// </summary>
            public partial class IAnnouncementController
            {
                /// <summary>ConnectionStrings</summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];

                /// <summary>
                /// This method is used to add new announcement in the ACD_IANNOUNCEMASTER table.
                /// </summary>
                /// <param name="objAnnounce">objAnnounce is the object of IAnnouncement class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>

                #region Announcement

                public int AddAnnouncement(IAnnouncement objAnnounce, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/IAnnounce/"); //UPLOAD_FILES\announcement
                        if (flag == true)
                        {
                            //Add New Announcement

                            objParams = new SqlParameter[11];
                            objParams[0] = new SqlParameter("@P_UA_NO", objAnnounce.UA_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objAnnounce.SESSIONNO);
                            objParams[2] = new SqlParameter("@P_COURSENO", objAnnounce.COURSENO);
                            objParams[3] = new SqlParameter("@P_SUBJECT", objAnnounce.SUBJECT);
                            objParams[4] = new SqlParameter("@P_DESCRIPTION", objAnnounce.DESCRIPTION);
                            objParams[5] = new SqlParameter("@P_ATTACHMENT", objAnnounce.ATTACHMENT);
                            objParams[6] = new SqlParameter("@P_STARTDATE", objAnnounce.STARTDATE);
                            objParams[7] = new SqlParameter("@P_EXPDATE", (objAnnounce.EXPDATE != DateTime.MinValue ? objAnnounce.EXPDATE as object : DBNull.Value as object));
                            objParams[8] = new SqlParameter("@P_STATUS", objAnnounce.STATUS);
                            objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objAnnounce.COLLEGE_CODE);
                            objParams[10] = new SqlParameter("@P_AN_NO", SqlDbType.Int);
                            objParams[10].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ANNOUNCEMENT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                            //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/UPLOAD_FILES/announcement/"); //
                            string uploadPath = file_path+"ITLE/UPLOAD_FILES/announcement/";
                            //Upload the File
                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                if (System.IO.File.Exists(uploadPath + objAnnounce.ATTACHMENT))
                                {
                                    //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                                    return Convert.ToInt32(CustomStatus.FileExists);
                                }
                                else
                                {
                                    string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                    fuFile.PostedFile.SaveAs(uploadPath + "announcement_" + Convert.ToInt32(ret) + uploadFile);
                                    flag = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.AddAnnouncement -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int DeleteAnnouncement(int ua_no, int sessionno, int courseno, int an_no)
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
                        objParams[3] = new SqlParameter("@P_AN_NO", an_no);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_ANNOUNCEMENT", objParams, true);
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

                public int UpdateAnnouncement(IAnnouncement objIAnnounce, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        bool flag = true;
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        if (flag == true)
                        {
                            
                            objParams = new SqlParameter[11];

                            objParams[0] = new SqlParameter("@P_AN_NO", objIAnnounce.AN_NO);
                            objParams[1] = new SqlParameter("@P_COURSENO", objIAnnounce.COURSENO);
                            objParams[2] = new SqlParameter("@P_SESSIONNO", objIAnnounce.SESSIONNO); 
                            objParams[3] = new SqlParameter("@P_UA_NO", objIAnnounce.UA_NO);
                            objParams[4] = new SqlParameter("@P_SUBJECT", objIAnnounce.SUBJECT);
                            objParams[5] = new SqlParameter("@P_DESCRIPTION", objIAnnounce.DESCRIPTION);
                            objParams[6] = new SqlParameter("@P_ATTACHMENT",objIAnnounce.ATTACHMENT);
                            objParams[7] = new SqlParameter("@P_STARTDATE", objIAnnounce.STARTDATE);
                            objParams[8] = new SqlParameter("@P_EXPDATE", (objIAnnounce.EXPDATE != DateTime.MinValue? objIAnnounce.EXPDATE as object: DBNull.Value as object));
                            objParams[9] = new SqlParameter("@P_STATUS", objIAnnounce.STATUS);
                            objParams[10] = new SqlParameter("@P_OUT",SqlDbType.Int);
                            objParams[10].Direction = ParameterDirection.Output;

                            object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ANNOUNCEMENT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                            string uploadPath = file_path+"ITLE/UPLOAD_FILES/announcement/";
                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                 string oldFileName = string.Empty;
                                 oldFileName = "announcement_" + objIAnnounce.AN_NO + System.IO.Path.GetExtension(objIAnnounce.OLDFILENAME);

                                 if (System.IO.File.Exists(uploadPath + oldFileName))
                                 {
                                    // return Convert.ToInt32(CustomStatus.FileExists);
                                     System.IO.File.Delete(uploadPath + oldFileName);
                                 }


                                 string uploadFile = "announcement_" + objIAnnounce.AN_NO + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                   fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            } 

                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.UpdateAnnouncement-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to retrieve all records from ASSIGNMASTER table by logged in user.
                /// </summary>
                /// <param name="objAssign">object of Assignment Class</param>                
                public DataSet GetAllAnnouncementtListByUaNo(int session, int courseno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", uano);
                      

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ALLANNOUNCEMENT_BYUA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetAllAnnouncementtListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This method is used to retrieve single assignment from ASSIGNMASTER table.
                /// </summary>
                /// <param name="newsid">Retrieve single assignment as per the passed assignno,courseno,sessionno and uano.</param>
                /// <returns>DataTableReader</returns>               
                public DataTableReader GetSingleAnnouncement(int anno, int courseno, int sessionno, int uano)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("P_AN_NO", anno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_UA_NO", uano);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEANNOUNCEMENT_BY_ANNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetSingleAnnouncement-> " + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet GetAnnouncementByCourseNo(IAnnouncement objAnnounce)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnnounce.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objAnnounce.COURSENO);
                        

                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_ANNOUNCEMENT_BYCOURSE_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetAnnouncementByCourseNo -> " + ex.ToString());
                    }
                    return ds;
                }


                //FOR CHACKING STATUS OF ANNOUNCEMENT

                    //public int GetStatus(DateTime current_date)
                    //{
                    //    int flag=0;
                    //    try
                    //    {

                    //    SQLHelper objHelper=new SQLHelper(_nitprm_constr);
                    //        SqlParameter[] objparam=new SqlParameter[2];

                    //        objparam[0] = new SqlParameter("@P_DATE", current_date);
                    //        objparam[1]=new SqlParameter("@P_OUT",SqlDbType.Int);
                    //        objparam[1].Direction = ParameterDirection.Output;
                    //       Object ret = objHelper.ExecuteNonQuerySP("PKG_ITLE_SP_RET_ANNOUNCEMENT_STATUS", objparam,true);

                    //       if (Convert.ToInt32(ret) != -99)
                    //           flag = 1;
                    //       else
                    //           flag = 0;
                    //    }
                    //    catch(Exception ex)
                    //    {
                    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetStatus -> " + ex.ToString());
                    //    }
                    //    return flag;
                //}


                ////////////////////////////////////
                //Method for Teacher Announcement Only

                public string ScrollingFacultyNews(string path, string idno)
                {
                    string retNews = string.Empty;

                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        //dr = objSqlHelper.ExecuteReaderSP("PKG_NEWS_SP_ACTIVE_NEWS", objParams);

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dr = objSqlHelper.ExecuteReaderSP("PKG_ITLE_GET_FACULTY_ANNOUNCEMENT", objParams);

                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            DataTable dt = new DataTable();
                            while (dr.Read())
                            {
                                //str.Append(dr["TITLE"].ToString() + "<br>");
                                //string url = "file:///"+ file_path.Replace("\\","/") + "Itle/upload_files/announcement/" + "announcement_";

                                string url = "UPLOAD_FILES/announcement/announcement_";
                                //if (dr["link"] != null && dr["link"].ToString() != string.Empty)


                                str.Append("<b>CREATION DATE  </b>");
                                str.Append(dr["STARTDATE"].ToString());
                                
                                str.Append("<b>  By  </b>");
                                str.Append(dr["UA_FULLNAME"].ToString());
                                str.Append("<br>");

                                str.Append("<b>COURSE : </b>");
                                str.Append(dr["COURSE_NAME"].ToString());
                                str.Append("<br>");

                                str.Append("<b>SUBJECT : </b>");
                                str.Append(dr["SUBJECT"].ToString());
                                str.Append("<br>");
                                str.Append("<br>");
                                str.Append("<b>DETAILS : </b>");
                                str.Append(dr["DESCRIPTION"].ToString());
                                str.Append("<br>");

                                if (dr["ATTACHMENT"] != null && dr["ATTACHMENT"].ToString() != string.Empty)
                                {
                                    str.Append("<b> Attachments : </b>");
                                    str.Append("<a Target=_blank href=" + url + dr["AN_NO"].ToString() + System.IO.Path.GetExtension(dr["ATTACHMENT"].ToString()) + ">" + dr["ATTACHMENT"].ToString() + "</a>");
                                    str.Append("<br>");
                                   
                                }
                                str.Append("-----------------------------------------------------------------------");
                                str.Append("<br>");
                                str.Append("<br>");
                            }
                            
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

                            //int iSpeed = 5;    //Speed of Marquee (higher = slower)
                            //int iWidth = 400;
                            //int iHeight = 300;

                            //retNews = "<MARQUEE onmouseover='this.stop();' ";
                            //retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
                            //retNews += "scrolldelay='" + iSpeed + "'";
                            //retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";

                            retNews = newshtml.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();

                    return retNews;
                }




                #endregion 



                #region Faculty Achivement
                public int AddAchivements(IAnnouncement objAnnounce, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/IAnnounce/"); //UPLOAD_FILES\announcement
                        if (flag == true)
                        {
                            //Add New Achievement

                            objParams = new SqlParameter[10];
                            objParams[0] = new SqlParameter("@P_UA_NO", objAnnounce.UA_NO);
                            objParams[1] = new SqlParameter("@P_SESSIONNO", objAnnounce.SESSIONNO);

                            objParams[2] = new SqlParameter("@P_COURSENO", objAnnounce.COURSENO);
                            objParams[3] = new SqlParameter("@P_TYPE", objAnnounce.AWDID);
                            objParams[4] = new SqlParameter("@P_ACHIVDATE", objAnnounce.STARTDATE);
                            objParams[5] = new SqlParameter("@P_DESCRIPTION", objAnnounce.DESCRIPTION);
                            objParams[6] = new SqlParameter("@P_ATTACHMENT", objAnnounce.ATTACHMENT);

                            //objParams[7] = new SqlParameter("@P_EXPDATE", (objAnnounce.EXPDATE != DateTime.MinValue ? objAnnounce.EXPDATE as object : DBNull.Value as object));
                            objParams[7] = new SqlParameter("@P_STATUS", objAnnounce.STATUS);
                            objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objAnnounce.COLLEGE_CODE);
                            objParams[9] = new SqlParameter("@P_AN_NO", SqlDbType.Int);
                            objParams[9].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_FACULTY_ACHIVEMENTS", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                            string uploadPath = file_path+"Itle/upload_files/Achievements/"; //
                            //Upload the File
                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                if (System.IO.File.Exists(uploadPath + objAnnounce.ATTACHMENT))
                                {
                                    //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                                    return Convert.ToInt32(CustomStatus.FileExists);
                                }
                                else
                                {
                                    string uploadFile = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                    fuFile.PostedFile.SaveAs(uploadPath + "achievements_" + Convert.ToInt32(ret) + uploadFile);
                                    flag = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.AddAnnouncement -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int DeleteAchivement(int ua_no, int sessionno, int courseno, int an_no)
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
                        objParams[3] = new SqlParameter("@P_AN_NO", an_no);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_ACHIVEMENT", objParams, true);
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

                public int UpdateAchivement(IAnnouncement objIAnnounce, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        bool flag = true;
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        if (flag == true)
                        {

                            objParams = new SqlParameter[10];

                            objParams[0] = new SqlParameter("@P_AN_NO", objIAnnounce.AN_NO);
                            objParams[1] = new SqlParameter("@P_COURSENO", objIAnnounce.COURSENO);
                            objParams[2] = new SqlParameter("@P_SESSIONNO", objIAnnounce.SESSIONNO);
                            objParams[3] = new SqlParameter("@P_UA_NO", objIAnnounce.UA_NO);
                            objParams[4] = new SqlParameter("@P_TYPE", objIAnnounce.AWDID);
                            objParams[5] = new SqlParameter("@P_DESCRIPTION", objIAnnounce.DESCRIPTION);
                            objParams[6] = new SqlParameter("@P_ATTACHMENT", objIAnnounce.ATTACHMENT);
                            objParams[7] = new SqlParameter("@P_ACHIVDATE", objIAnnounce.STARTDATE);
                            //objParams[8] = new SqlParameter("@P_EXPDATE", (objIAnnounce.EXPDATE != DateTime.MinValue ? objIAnnounce.EXPDATE as object : DBNull.Value as object));
                            objParams[8] = new SqlParameter("@P_STATUS", objIAnnounce.STATUS);
                            objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[9].Direction = ParameterDirection.Output;

                            object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ACHIVEMENT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                            string uploadPath = file_path+"Itle/upload_files/Achievements/";
                            if (!fuFile.FileName.Equals(string.Empty))
                            {
                                string oldFileName = objIAnnounce.OLDFILENAME;
                                oldFileName = "achievements_" + objIAnnounce.AN_NO + System.IO.Path.GetExtension(objIAnnounce.OLDFILENAME);

                                if (System.IO.File.Exists(uploadPath + oldFileName))
                                {
                                    // return Convert.ToInt32(CustomStatus.FileExists);
                                    System.IO.File.Delete(uploadPath + oldFileName);
                                }


                                string uploadFile = "achievements_" + objIAnnounce.AN_NO + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.UpdateAnnouncement-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAchivementListByUaNo(int session, int courseno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_ACHIVEMENT_BYUA_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetAllAnnouncementtListByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetSingleAchivement(int anno, int courseno, int sessionno, int uano)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_AN_NO", anno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_UA_NO", uano);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEACHIVEMENT_BY_ANNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetSingleAnnouncement-> " + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet GetAllFAchivementCourseNo(IAnnouncement objFachiv)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objFachiv.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objFachiv.COURSENO);

                        ds = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_ALLFACHIVEMENT_BYCOURSENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllFAchivementCourseNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetSingleAchiveByAchivNo(int planNo)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ACHIV_NO", planNo) 
                };
                        dtr = objSQL.ExecuteDataSetSP("PKG_ITLE_SP_RET_ACHIVE_BY_NO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetSinglePlanByPlanNo-> " + ex.ToString());
                    }
                    return dtr;
                }
                #endregion
            }
        }
    }
}



