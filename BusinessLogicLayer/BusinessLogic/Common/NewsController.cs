using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Text;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This NewsController is used to control News table.
            /// </summary>
            public class NewsController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method adds a new record in the News Table
                /// </summary>
                /// <param name="objNews">News object</param>
                /// <param name="fuFile">File to be uploaded</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                public int Add(News objNews, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        string uploadPath = HttpContext.Current.Server.MapPath("~/upload_files/");

                        //Upload the File
                        if (!fuFile.FileName.Equals(string.Empty))
                        {
                            if (System.IO.File.Exists(uploadPath + objNews.Filename))
                            {
                                //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                                return Convert.ToInt32(CustomStatus.FileExists);
                            }
                            else
                            {
                                string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            //Add New News
                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_NEWSTITLE",  objNews.NewsTitle);
                            objParams[1] = new SqlParameter("@P_NEWSDESC",  objNews.NewsDesc);
                            objParams[2] = new SqlParameter("@P_LINK",  objNews.Link);
                            objParams[3] = new SqlParameter("@P_UPLOADED_DATE",  DateTime.Now);
                            objParams[4] = new SqlParameter("@P_CATEGORY",  objNews.Category);
                            objParams[5] = new SqlParameter("@P_FILENAME",  objNews.Filename);
                            objParams[6] = new SqlParameter("@P_EXPIRY_DATE",  objNews.ExpiryDate);
                            objParams[7] = new SqlParameter("@P_NEWSID",SqlDbType.Int);
                            objParams[7].Direction = ParameterDirection.Output;

                            //if (objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_INS_NEWS", objParams, false) != null)
                            //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_INS_NEWS", objParams, true));
                            if (Convert.ToInt32(retStatus) == 1)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved); //Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);//Convert.ToInt32(CustomStatus.RecordUpdated);  


                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to update record in the News table
                /// </summary>
                /// <param name="objNews">object of News class</param>
                /// <param name="fuFile">File to be uploaded</param>
                /// <returns>Integer CustomStatus - Record Updated or Error</returns>
                public int Update(News objNews, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        string uploadPath = HttpContext.Current.Server.MapPath("~/upload_files/");

                        //Upload the File
                        if (!fuFile.FileName.Equals(string.Empty))
                        {
                            if (System.IO.File.Exists(uploadPath + objNews.Filename))
                            {
                                //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                                return Convert.ToInt32(CustomStatus.FileExists);
                            }
                            else
                            {
                                string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                                //Delete Old File
                                if (objNews.OldFilename != string.Empty)
                                    System.IO.File.Delete(uploadPath + objNews.OldFilename);
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            //UpdateFaculty News
                            string filename = string.Empty;
                            if (objNews.Filename.Equals(string.Empty))
                                filename = objNews.OldFilename;
                            else
                                filename = objNews.Filename;

                            objParams = new SqlParameter[8];
                            objParams[0] = new SqlParameter("@P_NEWSID", objNews.NewsID);
                            objParams[1] = new SqlParameter("@P_NEWSTITLE", objNews.NewsTitle);
                            objParams[2] = new SqlParameter("@P_NEWSDESC", objNews.NewsDesc);
                            objParams[3] = new SqlParameter("@P_LINK", objNews.Link);
                            objParams[4] = new SqlParameter("@P_UPLOADED_DATE", DateTime.Now);
                            objParams[5] = new SqlParameter("@P_FILENAME", filename);
                            objParams[6] = new SqlParameter("@P_EXPIRY_DATE", objNews.ExpiryDate);
                            objParams[7] = new SqlParameter("@P_STATUS", objNews.Status);

                            if (objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_UPD_NEWS", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to delete record from news table.
                /// </summary>
                /// <param name="newsid">Delete record as per this newsid.</param>
                /// <returns>Integer CustomStatus - Record Deleted or Error</returns>
                public int Delete(int newsid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NEWSID", newsid);

                        objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_DEL_NEWS", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetSingleNews-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }

                /// <summary>
                /// This method is used to get all news from News table.
                /// </summary>
                /// <param name="spname">Get all news as per this spname.</param>
                /// <returns>DataSet</returns>
                public DataSet GetAllNews(string spname)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dsNews = objSQLHelper.ExecuteDataSetSP(spname, objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetAllNews-> " + ex.ToString());
                    }
                    return dsNews;
                }

                /// <summary>
                /// This method is used to retrieve single news from News table.
                /// </summary>
                /// <param name="newsid">Retrieve single news as per this newsid.</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleNews(int newsid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_NEWSID", newsid);
                        
                        dr = objSQLHelper.ExecuteReaderSP("Pkg_News_SP_RET_NEWS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetSingleNews-> " + ex.ToString());
                    }
                    return dr;
                }

                public int UpdateByDate(DateTime expiryDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_EXPIRY_DATE", expiryDate);

                        objSQLHelper.ExecuteNonQuerySP("PKG_NEWS_SP_UPD_NEWSSTATUS", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.UpdateByDate -> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }

                /// <summary>
                /// Generates the Scrolling News using Marque
                /// </summary>
                /// <param name="path">Used to get path in the form of string</param>
                /// <returns>String</returns>
                public string ScrollingNews(string path)
                {
                    string retNews = string.Empty;
                    string folderpath = System.Configuration.ConfigurationManager.AppSettings["Notice_Path"];
                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dr = objSqlHelper.ExecuteReaderSP("PKG_NEWS_SP_ACTIVE_NEWS", objParams);

                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            while (dr.Read())
                            {
                                str.Append(dr["TITLE"].ToString() + "<br>");
                                //string url = ".." + path + "//upload_files//";
                                //string url = "http://localhost:62778" + path + "/upload_files/";
                                string url = folderpath;
                                if (dr["link"] != null && dr["link"].ToString() != string.Empty)
                                    if (dr["filename"] != null && dr["filename"].ToString() != string.Empty)
                                        str.Append("<p><a Target=_blank href=" + url + dr["filename"].ToString() + ">" + dr["link"].ToString() + "</a></p>");


                                str.Append("<br>");
                                str.Append(dr["NEWSDESC"].ToString());
                                str.Append("<br>");
                            }
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

                            int iSpeed = 5;    //Speed of Marquee (higher = slower)
                            int iWidth = 260;
                            int iHeight = 300;

                            retNews = "<MARQUEE onmouseover='this.stop();' ";
                            retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
                            retNews += "scrolldelay='" + iSpeed + "'";
                            retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();

                    return retNews;
                }





                public string NoticeBoard(string path)
                {
                    string retNews = string.Empty;
                    string folderpath = System.Configuration.ConfigurationManager.AppSettings["Notice_Path"];
                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dr = objSqlHelper.ExecuteReaderSP("PKG_RET_NOTICE_BOARD", objParams);

                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            while (dr.Read())
                            {
                                str.Append(dr["TITLE"].ToString() + "<br>");
                                //  string url = "http://localhost:62778" + path + "/upload_files/";
                                string url = folderpath;

                                if (dr["link"] != null && dr["link"].ToString() != string.Empty)
                                    if (dr["filename"] != null && dr["filename"].ToString() != string.Empty)
                                        str.Append("<p><a Target=_blank href=" + url + dr["filename"].ToString() + ">" + dr["link"].ToString() + "</a></p>");


                                str.Append("<br>");
                                str.Append(dr["NEWSDESC"].ToString());
                                str.Append("<br>");
                            }
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

                            int iSpeed = 5;    //Speed of Marquee (higher = slower)
                            int iWidth = 260;
                            int iHeight = 300;

                            retNews = "<MARQUEE onmouseover='this.stop();' ";
                            retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
                            retNews += "scrolldelay='" + iSpeed + "'";
                            retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();

                    return retNews;
                }

                public string TP_NoticeBoard(string path)
                {
                    string retNews = string.Empty;
                    string folderpath = System.Configuration.ConfigurationManager.AppSettings["Notice_Path"];
                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dr = objSqlHelper.ExecuteReaderSP("PKG_RET_TANDP_NOTICE_BOARD", objParams);

                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            while (dr.Read())
                            {
                                str.Append(dr["TITLE"].ToString() + "<br>");
                                //string url = ".." + path + "/upload_files/";
                                //string url = "http://localhost:62778" + path + "/upload_files/";
                                string url = folderpath;
                                if (dr["link"] != null && dr["link"].ToString() != string.Empty)
                                    if (dr["filename"] != null && dr["filename"].ToString() != string.Empty)
                                        str.Append("<p><a Target=_blank href=" + url + dr["filename"].ToString() + ">" + dr["link"].ToString() + "</a></p>");


                                str.Append("<br>");
                                str.Append(dr["NEWSDESC"].ToString());
                                str.Append("<br>");
                            }
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

                            int iSpeed = 5;    //Speed of Marquee (higher = slower)
                            int iWidth = 260;
                            int iHeight = 300;

                            retNews = "<MARQUEE onmouseover='this.stop();' ";
                            retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
                            retNews += "scrolldelay='" + iSpeed + "'";
                            retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();

                    return retNews;
                }
                public DataSet GetUserTypeWiseNews(int ua_type)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", ua_type);


                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_NEWS_SP_ALL_NEWS_DASHBOARD", objParams);


                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNews-> " + ex.ToString());
                    }
                    return dsNews;
                }

                // Created by Nikhil V. Lambe on 30/06/2021 to add notice to selected students
                public int AddNoticeForSelectedStudents(News objNews, int userType, int collegeId, int degree, int branch, int semester, int select, int idno, int department, int OrgID, string filetext, int special_stud)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objsqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objparams = null;

                        bool flag = true;
                     //   string uploadPath = HttpContext.Current.Server.MapPath("~/ExcelData/");

                        //string uploadPath = HttpContext.Current.Server.MapPath("~/upload_files/notice_document/");
                        
                        ////Upload the File
                        //if (!fuFile.FileName.Equals(string.Empty))
                        //{
                        //    if (System.IO.File.Exists(uploadPath + objNews.Filename))
                        //    {
                        //        //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                        //        //return Convert.ToInt32(CustomStatus.FileExists);
                        //    }
                        //    else
                        //    {
                        //        string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName.Replace(" ", "_"));
                        //        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                        //        flag = true;
                        //    }
                        //}
                       
                        if (flag == true)
                        {
                            //Add New News
                            objparams = new SqlParameter[18];
                            objparams[0] = new SqlParameter("@P_NEWSTITLE", objNews.NewsTitle);
                            objparams[1] = new SqlParameter("@P_NEWSDESC", objNews.NewsDesc);
                            objparams[2] = new SqlParameter("@P_LINK", objNews.Link);
                            //objparams[3] = new SqlParameter("@P_UPLOADED_DATE", DateTime.Now);
                            objparams[3] = new SqlParameter("@P_CATEGORY", objNews.Category);
                            //if (fuFile.HasFile)
                            //{
                            //    objparams[4] = new SqlParameter("@P_FILENAME", objNews.Filename);
                            //}
                            //else
                            //{
                                objparams[4] = new SqlParameter("@P_FILENAME", filetext);
                            //}
                            objparams[5] = new SqlParameter("@P_EXPIRY_DATE", objNews.ExpiryDate);
                            objparams[6] = new SqlParameter("@P_USER_TYPE", userType);
                            objparams[7] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                            objparams[8] = new SqlParameter("@P_DEGREE", degree);
                            objparams[9] = new SqlParameter("@P_BRANCH", branch);
                            objparams[10] = new SqlParameter("@P_SEMESTER", semester);
                            objparams[11] = new SqlParameter("@P_SELECT_ALL", select);
                            objparams[12] = new SqlParameter("@P_IDNO", idno);
                            objparams[13] = new SqlParameter("@P_DEPARTMENT", department);
                            objparams[14] = new SqlParameter("@P_OrganizationId", OrgID);
                            objparams[15] = new SqlParameter("@P_STATUS",objNews.Status);
                            objparams[16] = new SqlParameter("@P_SPECIAL_STUD",special_stud);//added by kajal jaiswal on 20-12-2022
                            objparams[17] = new SqlParameter("@P_NEWSID", SqlDbType.Int);
                            objparams[17].Direction = ParameterDirection.Output;

                            //if (objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_INS_NEWS", objParams, false) != null)
                            //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            retStatus = Convert.ToInt32(objsqlHelper.ExecuteNonQuerySP("PKG_SP_INS_NOTICE_BOARD_FOR_SELECTED_STUDENTS", objparams, true));
                            if (Convert.ToInt32(retStatus) == 1)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved); //Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(retStatus) == 2)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);//Convert.ToInt32(CustomStatus.RecordUpdated);  


                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddNoticeForSelectedFaculty(News objNews, string filetext,int status,int ua_no, string userType, int collegeId, int degree, int branch, int semester, string department, int select,  int created_by, string ipaddr, int all_user, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
           
                        if (flag == true)
                        {
                            //Add New News
                            objParams = new SqlParameter[21];
                            objParams[0] = new SqlParameter("@P_NEWSTITLE", objNews.NewsTitle);
                            objParams[1] = new SqlParameter("@P_NEWSDESC", objNews.NewsDesc);
                            objParams[2] = new SqlParameter("@P_LINK", objNews.Link);
                            //objParams[3] = new SqlParameter("@P_UPLOADED_DATE", DateTime.Now);
                            objParams[3] = new SqlParameter("@P_CATEGORY", objNews.Category);
                            objParams[4] = new SqlParameter("@P_FILENAME",filetext);
                            objParams[5] = new SqlParameter("@P_EXPIRY_DATE", objNews.ExpiryDate);
                            objParams[6] = new SqlParameter("@P_USER_TYPE", userType);
                            objParams[7] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                            objParams[8] = new SqlParameter("@P_DEGREE", degree);
                            objParams[9] = new SqlParameter("@P_BRANCH", branch);
                            objParams[10] = new SqlParameter("@P_SEMESTER", semester);
                            objParams[11] = new SqlParameter("@P_DEPARTMENT", department);
                            objParams[12] = new SqlParameter("@P_SELECT_ALL", select);
                            objParams[13] = new SqlParameter("@P_UA_NO", ua_no);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[14] = new SqlParameter("@P_CREATED_BY", created_by);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[15] = new SqlParameter("@P_IPADDR", ipaddr);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[16] = new SqlParameter("@P_ALL_USERS", all_user);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[17] = new SqlParameter("@P_OrganizationId", OrgID);//Added By Ruchika Dhakate on 11.10.2022 
                            objParams[18] = new SqlParameter("@P_STATUS", objNews.Status);
                            objParams[19] = new SqlParameter("@P_STATUS_FAC ", status);
                            objParams[20] = new SqlParameter("@P_NEWSID", SqlDbType.Int);
                            objParams[20].Direction = ParameterDirection.Output;

                            //if (objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_INS_NEWS", objParams, false) != null)
                            //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_NOTICE_BOARD_FOR_SELECTED_FACULTY", objParams, true));
                            if (Convert.ToInt32(retStatus) == 1)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved); //Convert.ToInt32(CustomStatus.TransactionFailed);
                         
                                 else if (Convert.ToInt32(retStatus) == 2)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                                 else
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);//Convert.ToInt32(CustomStatus.RecordUpdated);  

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int Add_News_UserWise(News objNews, string filetext, string userType, int collegeId, int degree, int branch, int semester, string department, int select,  int created_by, string ipaddr, int all_user, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                       // string uploadPath = HttpContext.Current.Server.MapPath("~/UPLOAD_FILES/NOTICE_DOCUMENT/");
                       //string uploadPath = HttpContext.Current.Server.MapPath("~/ExcelData/");

                       // //Upload the File
                       // if (!fuFile.FileName.Equals(string.Empty))
                       // {
                       //     if (System.IO.File.Exists(uploadPath + objNews.Filename))
                       //     {
                       //         //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                       //         return Convert.ToInt32(CustomStatus.FileExists);
                       //     }
                       //     else
                       //     {
                       //         string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName.Replace(" ", "_"));
                       //         fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                       //         flag = true;
                       //     }
                       // }

                        if (flag == true)
                        {
                            //Add New News
                            objParams = new SqlParameter[17];
                            objParams[0] = new SqlParameter("@P_NEWSTITLE", objNews.NewsTitle);
                            objParams[1] = new SqlParameter("@P_NEWSDESC", objNews.NewsDesc);
                            objParams[2] = new SqlParameter("@P_LINK", objNews.Link);
                            //objParams[3] = new SqlParameter("@P_UPLOADED_DATE", DateTime.Now);
                            objParams[3] = new SqlParameter("@P_CATEGORY", objNews.Category);
                            objParams[4] = new SqlParameter("@P_FILENAME",filetext);
                            objParams[5] = new SqlParameter("@P_EXPIRY_DATE", objNews.ExpiryDate);
                            objParams[6] = new SqlParameter("@P_USER_TYPE", userType);
                            objParams[7] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                            objParams[8] = new SqlParameter("@P_DEGREE", degree);
                            objParams[9] = new SqlParameter("@P_BRANCH", branch);
                            objParams[10] = new SqlParameter("@P_SEMESTER", semester);
                            objParams[11] = new SqlParameter("@P_DEPARTMENT", department);
                            //objParams[13] = new SqlParameter("@P_SELECT_ALL", select);
                            //objParams[12] = new SqlParameter("@P_UA_NOS", ua_nos);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[12] = new SqlParameter("@P_CREATED_BY", created_by);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[13] = new SqlParameter("@P_IPADDR", ipaddr);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[14] = new SqlParameter("@P_ALL_USERS", all_user);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[15] = new SqlParameter("@P_OrganizationId", OrgID);//Added By Ruchika Dhakate on 11.10.2022 
                            objParams[16] = new SqlParameter("@P_NEWSID", SqlDbType.Int);
                            objParams[16].Direction = ParameterDirection.Output;

                            //if (objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_INS_NEWS", objParams, false) != null)
                            //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                            retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_INS_NEWS", objParams, true));
                            if (Convert.ToInt32(retStatus) == 1)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved); //Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);//Convert.ToInt32(CustomStatus.RecordUpdated);  


                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }
                
                //-------------------------------------------------------------------------------------------------------------------------
                public int Update_News_UserWise(News objNews, string filetext , string userType, int collegeId, int degree, int branch, int semester, string department, int select, int created_by, string ipaddr, int all_user, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                      //  string uploadPath = HttpContext.Current.Server.MapPath("~/UPLOAD_FILES/NOTICE_DOCUMENT/");
                        //string uploadPath = HttpContext.Current.Server.MapPath("~/ExcelData/");

                        ////Upload the File
                        //if (!fuFile.FileName.Equals(string.Empty))
                        //{
                        //    if (System.IO.File.Exists(uploadPath + objNews.Filename))
                        //    {
                        //        //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                        //        return Convert.ToInt32(CustomStatus.FileExists);
                        //    }
                        //    else
                        //    {
                        //        string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName.Replace(" ", "_"));
                        //        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                        //        //Delete Old File
                        //        if (objNews.OldFilename != string.Empty)
                        //            System.IO.File.Delete(uploadPath + objNews.OldFilename);
                        //        flag = true;
                        //    }
                        //}

                        if (flag == true)
                        {
                            //UpdateFaculty News
                            //string filename = string.Empty;
                            //if (objNews.Filename.Equals(string.Empty))
                            //    filename = objNews.OldFilename;
                            //else
                            //    filename = objNews.Filename;

                            objParams = new SqlParameter[17];
                            objParams[0] = new SqlParameter("@P_NEWSID", objNews.NewsID);
                            objParams[1] = new SqlParameter("@P_NEWSTITLE", objNews.NewsTitle);
                            objParams[2] = new SqlParameter("@P_NEWSDESC", objNews.NewsDesc);
                            objParams[3] = new SqlParameter("@P_LINK", objNews.Link);
                            objParams[4] = new SqlParameter("@P_UPLOADED_DATE", DateTime.Now);
                            objParams[5] = new SqlParameter("@P_FILENAME", filetext);
                            objParams[6] = new SqlParameter("@P_EXPIRY_DATE", objNews.ExpiryDate);
                            objParams[7] = new SqlParameter("@P_STATUS", objNews.Status);
                            objParams[8] = new SqlParameter("@P_USER_TYPE", userType);
                            objParams[9] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                            objParams[10] = new SqlParameter("@P_DEGREE", degree);
                            objParams[11] = new SqlParameter("@P_BRANCH", branch);
                            objParams[12] = new SqlParameter("@P_SEMESTER", semester);
                            objParams[13] = new SqlParameter("@P_DEPARTMENT", department);
                            //objParams[14] = new SqlParameter("@P_SELECT_ALL", select);
                            //objParams[14] = new SqlParameter("@P_UA_NOS", ua_nos);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[14] = new SqlParameter("@P_CREATED_BY", created_by);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[15] = new SqlParameter("@P_IPADDR", ipaddr);//Added By Dileep Kare on 26.07.2021 ticket number 
                            //objParams[18] = new SqlParameter("@P_ALL_USERS", all_user);//Added By Dileep Kare on 26.07.2021 ticket number 
                            objParams[16] = new SqlParameter("@P_OrganizationId",OrgID);//Added by Ruchika Dhakate on 11.10.2022 


                            if (objSQLHelper.ExecuteNonQuerySP("Pkg_News_SP_UPD_NEWS", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }
                 

                // Created by Nikhil V. Lambe on 30/06/2021 to show the notice by user type (faculty and HOD)
                public string NoticeBoard_Faculty_HOD(string path, int user_type, int select, int department, int All_Users, int UANO)
                {
                    string retNews = string.Empty;
                    string folderpath = System.Configuration.ConfigurationManager.AppSettings["Notice_Path"];

                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_USER_TYPE", user_type);
                        objParams[1] = new SqlParameter("@P_DEPARTMENT", department);
                        objParams[2] = new SqlParameter("@P_SELECT", select);
                        objParams[3] = new SqlParameter("@P_ALL_USERS", All_Users);//Added By Dileep Kare on 26.07.2021 ticket number 
                        objParams[4] = new SqlParameter("@P_UA_NO", UANO);//Added By Dileep Kare on 26.07.2021
                        dr = objSqlHelper.ExecuteReaderSP("PKG_RET_NOTICE_BOARD_FACULTY_AND_HOD", objParams);
                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            while (dr.Read())
                            {
                                str.Append(dr["TITLE"].ToString() + "<br>");
                                //string url = ".." + path + "//upload_files//";
                                // string url = path + "//UPLOAD_FILES//";
                                string url = folderpath;
                                if (dr["link"] != null && dr["link"].ToString() != string.Empty)
                                    if (dr["filename"] != null && dr["filename"].ToString() != string.Empty)
                                        str.Append("<p><a Target=_blank href=" + url + dr["filename"].ToString() + ">" + dr["link"].ToString() + "</a></p>");


                                str.Append("<br>");
                                str.Append(dr["NEWSDESC"].ToString());
                                str.Append("<br>");
                            }
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

                            int iSpeed = 5;    //Speed of Marquee (higher = slower)
                            int iWidth = 260;
                            int iHeight = 300;

                            retNews = "<MARQUEE onmouseover='this.stop();' ";
                            retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
                            retNews += "scrolldelay='" + iSpeed + "'";
                            retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();

                    return retNews;
                }



                // Created by Nikhil V. Lambe on 30/06/2021 to show the notice by user type(student). 
                // Created by Nikhil V. Lambe on 30/06/2021 to show the notice by user type(student). 
                public string NoticeBoardNew(string path, int user_type, int college_id, int degree, int branch, int semester, int select, int special, int idno)
                {
                    string retNews = string.Empty;
                    string folderpath = System.Configuration.ConfigurationManager.AppSettings["Notice_Path"];


                    SqlDataReader dr = null;
                    try
                    {
                        StringBuilder newshtml = new StringBuilder();
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_USER_TYPE", user_type);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_DEGREE", degree);
                        objParams[3] = new SqlParameter("@P_BRANCH", branch);
                        objParams[4] = new SqlParameter("@P_SEMESTER", semester);
                        //objParams[5] = new SqlParameter("@P_DEPARTMENT",department);
                        objParams[5] = new SqlParameter("@P_SELECT", select);
                        objParams[6] = new SqlParameter("@P_SPECIAL_STUD", special);
                        objParams[7] = new SqlParameter("@P_IDNO", idno);//Added By Dileep Kare on 13.08.2021



                        dr = objSqlHelper.ExecuteReaderSP("PKG_RET_NOTICE_BOARD_FOR_STUDENT", objParams);
                        if (dr != null)
                        {
                            StringBuilder str = new StringBuilder();
                            while (dr.Read())
                            {
                                str.Append(dr["TITLE"].ToString() + "<br>");
                                //string url = ".." + path + "//upload_files//";
                                string url = folderpath;



                                if (dr["link"] != null && dr["link"].ToString() != string.Empty)
                                    if (dr["filename"] != null && dr["filename"].ToString() != string.Empty)
                                        str.Append("<p><a Target=_blank href=" + url + dr["filename"].ToString() + ">" + dr["link"].ToString() + "</a></p>");




                                str.Append("<br>");
                                str.Append(dr["NEWSDESC"].ToString());
                                str.Append("<br>");
                            }
                            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");



                            int iSpeed = 5;    //Speed of Marquee (higher = slower)
                            int iWidth = 260;
                            int iHeight = 300;



                            retNews = "<MARQUEE onmouseover='this.stop();' ";
                            retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
                            retNews += "scrolldelay='" + iSpeed + "'";
                            retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";
                        }



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException(ex.ToString());
                    }
                    if (dr != null) dr.Close();



                    return retNews;
                }

                //======================================================================================= 
                /// <summary>
                /// Added by Dileep Kare on 13.08.2021
                /// </summary>
                /// <param name="ua_type"></param>
                /// <param name="idno"></param>
                /// <returns></returns>
                public DataSet GetUserTypeWiseNewsstudent(int ua_type, int idno)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);


                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_NEWS_SP_ALL_NEWS_STUDENT_DASHBOARD", objParams);


                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNewsstudent-> " + ex.ToString());
                    }
                    return dsNews;
                }
                //======================================================================================
                /// <summary>
                /// Added by Dileep Kare on 13.08.2021
                /// </summary>
                /// <param name="ua_type"></param>
                /// <param name="ua_no"></param>
                /// <returns></returns>
                public DataSet GetUserTypeWiseNewsFaculty_HOD(int ua_type, int ua_no)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_NEWS_SP_ALL_NEWS_FACULTY_HOD_DASHBOARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNewsFaculty_HOD-> " + ex.ToString());
                    }
                    return dsNews;
                }

                // Added By Kajal Jaiswal  17-12-2022
                public DataSet GetNews(int COLLEGE_ID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int NewsID)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[4] = new SqlParameter("@P_NewsID", NewsID);
                     
                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_GET_NOTICE", objParams);


                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNews-> " + ex.ToString());
                    }
                    return dsNews;
                }
                //Added by Kajal jaiswal on 19-12-2022
                public DataSet GetFaculty(string Departments, int USER_TYPES)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USER_TYPES", USER_TYPES);
                        objParams[1] = new SqlParameter("@P_UA_DEPTNO", Departments);
                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNews-> " + ex.ToString());
                    }
                    return dsNews;
                }

                //Added by Kajal jaiswal on 07-01-2023

                public DataSet GetDepartment( int UA_TYPE)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", UA_TYPE);
                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_GET_DEPARTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNews-> " + ex.ToString());
                    }
                    return dsNews;
                }

                public DataSet GetSelectedFaculty(string Departments, int USER_TYPES)
                {
                    DataSet dsNews = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USER_TYPES", USER_TYPES);
                        objParams[1] = new SqlParameter("@P_UA_DEPTNO", Departments);
                        dsNews = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsNews;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetUserTypeWiseNews-> " + ex.ToString());
                    }
                    return dsNews;
                }


            }//END: NewsController

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS