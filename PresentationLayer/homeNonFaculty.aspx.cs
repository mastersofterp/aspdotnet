﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.IO;
using System.Web.Services;
using System.Web;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;


public partial class homeNonFaculty : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    NewsController objNc = new NewsController();
    ResultProcessing objResult = new ResultProcessing();
    StudentController objSC = new StudentController();
    FetchDataController objFetch = new FetchDataController();
    ExamController objExamController = new ExamController();
    LeavesController objLeave = new LeavesController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                Page.Title = Session["coll_name"].ToString();

                // BindListViewEmployees();                                                  // Done
                // ApproveLeaves();                          
                //BindTimeTable(); //timetable *
                //  BindTodaysTimeTable();                                                   // Done
                //BindListViewNews();                                                        // Done
                //   BindEmployeeAttedence(); // att percentage                              // Done
                //BindHolidays();                                                            // Done
                // BindLeaveBalance();                                                       // Done
                // BindListViewTask();                                                       // Done
                //  BindQuickAccess(); // Not


                DataSet dsLastLoginTime = objCommon.FillDropDown("LogFile", "TOP(1) LEFT(FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt'), Charindex(' ', FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt')) - 1)AA ", "null", "UA_NAME='" + Session["username"].ToString() + "'", "LOGINTIME desc");
                DataSet dsLastLoginForm = objCommon.FillDropDown("LogFile", "TOP(1) right(FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt'), Charindex(' ', FORMAT(CAST(LOGINTIME AS DATETIME),'hh:mm tt')) - 4) as AA ", "null", "UA_NAME='" + Session["username"].ToString() + "'", "LOGINTIME desc");

                lblLastLoginTime.Text = dsLastLoginTime.Tables[0].Rows[0]["AA"].ToString();
                lblLastLoginForm.Text = dsLastLoginForm.Tables[0].Rows[0]["AA"].ToString();
                //Show_TodaysTT();
               // Show_ExamTT();
                Show_Notice();
            }
        }
    }

    private void ApproveLeaves()
    {
        try
        {
            int collegeno = 1;
            int userno = Convert.ToInt32(Session["userno"]);

            if (collegeno > 0 && userno > 0)
            {
                //DataSet ds = objLeave.RetrievePendingLeaves(userno);
                //lblLeaves.Text = ds.Tables[0].Rows[0]["PendingLeaves"].ToString();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    
    [WebMethod]
    public static string ShowAttPer()
    {
        homeNonFaculty a = new homeNonFaculty();
        // return a.BindEmployeeAttedence().GetXml();
        string LblPer = a.BindEmployeeAttedence();
        return LblPer;

    }

    [WebMethod]
    public static List<EmpInOutTIme> ShowInOutTime()
    {
        homeNonFaculty a = new homeNonFaculty();
        List<EmpInOutTIme> objList = a.BindListViewEmployees();
        return objList;
    }
    [WebMethod]
    public static List<EmpNews> ShowNewsData()
    {
        homeNonFaculty a = new homeNonFaculty();
        List<EmpNews> objList = a.BindListViewNews();
        return objList;
    }
    [WebMethod]
    public static List<EmpNews> ShowExpiredNewsData()
    {
        homeNonFaculty a = new homeNonFaculty();
        List<EmpNews> objList = a.BindListViewExpiredNews();
        return objList;
    }
    //[WebMethod]
    ////public static List<QuickAccessLinks> ShowQuickAccessLinks()
    ////{
    ////    HomeNonFaculty a = new HomeNonFaculty();
    ////    List<QuickAccessLinks> objQLList = a.BindQuickAccess();
    ////    return objQLList;
    ////}
    [WebMethod]
    public static List<EmployeeTask> ShowEmpTasks()
    {
        homeNonFaculty a = new homeNonFaculty();
        List<EmployeeTask> objETList = a.BindListViewTask();
        return objETList;
    }

    [WebMethod]
    public static string ShowCasualBalLeaves()
    {
        homeNonFaculty a = new homeNonFaculty();
        string LblCLBal = a.BindLeaveBalance();
        return LblCLBal;

    }
    [WebMethod]
    public static UpcommingHolidays ShowUpcommingHolidays()
    {
        homeNonFaculty a = new homeNonFaculty();
        UpcommingHolidays commingholidays = a.BindHolidays();
        return commingholidays;
    }
    //[WebMethod]
    //public static TableList ShowTimeTable()
    //{
    //    homeNonFaculty a = new homeNonFaculty();
    //    TableList objTablesList = a.BindTimeTable();
    //    return objTablesList;
    //}
    private string BindEmployeeAttedence()
    {
        DataSet ds = null;
        string LblName = string.Empty;
        //int collegeno = 1;
        int userno = Convert.ToInt32(Session["idno"]);

        if (userno > 0)
        {
            ds = objLeave.RetrieveEmployeeAttedeance(userno);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                LblName = ds.Tables[0].Rows[0]["Percentange"].ToString();
            }
            else
            {
                LblName = null;
            }
        }
        return LblName;
    }
  

    public List<EmpInOutTIme> BindListViewEmployees()
    {
        List<EmpInOutTIme> objList = new List<EmpInOutTIme>();
        try
        {
            //int collegeno = 1;
            int userno = Convert.ToInt32(Session["idno"]);
            DataSet ds = new DataSet();
            if (userno > 0)
            {
                ds = objLeave.RetrieveEmployeeInOut(userno);
            }
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                objList = (from DataRow dr in ds.Tables[0].Rows
                           select new EmpInOutTIme
                           {
                               Day = dr["dayname"].ToString(),
                               InTime = dr["INTIME"].ToString(),
                               OutTime = dr["OUTTIME"].ToString()
                           }).ToList();
            }
            else
            {
                objList = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return objList;
    }
    //public string GetFileNamePath(object filename)
    //{
    //    if (filename != null && filename.ToString() != "")
    //        return filename.ToString();
    //    else
    //        return "None";
    //}
    //public string GetFileName(object filename)
    //{
    //    if (filename != null && filename.ToString() != "")
    //        return filename.ToString();
    //    else
    //        return "None";
    //}
    //protected string checkFile(string filename)
    //{
    //    string filepath = Server.MapPath("~//UPLOAD_FILES//NOTICE_DOCUMENT/");
    //    string returnPath = "UPLOAD_FILES/NOTICE_DOCUMENT/";
    //    string returnFilename = "";

    //    FileInfo myfile = new FileInfo(filepath + filename);
    //    if (myfile.Exists)
    //    {
    //        if (filename.Contains(' '))
    //        {
    //            returnFilename = System.Web.HttpUtility.UrlPathEncode(filename);
    //        }
    //        else
    //        {
    //            returnFilename = filename;
    //        }
    //        return returnPath + returnFilename;
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
   
    private List<EmpNews> BindListViewNews()
    {
        List<EmpNews> objNewsList = new List<EmpNews>();
        try
        {

            //DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NEWS");
            DataSet dsNews = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));

            if (dsNews != null && dsNews.Tables[0] != null && dsNews.Tables[0].Rows.Count > 0)
            {
                objNewsList = (from DataRow dr in dsNews.Tables[0].Rows
                               select new EmpNews
                               {
                                   Day = dr["DD"].ToString(),
                                   Month = dr["MM"].ToString(),
                                   NewsDescription = dr["NEWSDESC"].ToString(),
                                   //NewsLink = dr["FILENAME"].ToString(),
                                   //NewsLink = checkFile("upload_files/notice_document/" + dr["FILENAME"].ToString()),
                                   NewsLink = checkFile(dr["FILENAME"].ToString()),
                                   Title = dr["TITLE"].ToString()
                               }).ToList();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return objNewsList;
    }
    private List<EmpNews> BindListViewExpiredNews()
    {
        List<EmpNews> objNewsList = new List<EmpNews>();
        try
        {
            NewsController objNc = new NewsController();
            //DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NEWS");
            DataSet dsNews = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));

            if (dsNews != null && dsNews.Tables[1] != null && dsNews.Tables[1].Rows.Count > 0)
            {
                objNewsList = (from DataRow dr in dsNews.Tables[1].Rows
                               select new EmpNews
                               {
                                   Day = dr["DD"].ToString(),
                                   Month = dr["MM"].ToString(),
                                   NewsDescription = dr["NEWSDESC"].ToString(),
                                   //NewsLink = dr["LINK"].ToString(),
                                   //NewsLink = checkFile("upload_files/notice_document/" + dr["FILENAME"].ToString()),
                                   NewsLink = checkFile(dr["FILENAME"].ToString()),
                                   Title = dr["TITLE"].ToString()
                               }).ToList();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return objNewsList;
    }
   

    private List<EmployeeTask> BindListViewTask()
    {
        List<EmployeeTask> TaskList = new List<EmployeeTask>();
        try
        {
            //DataSet dsTasks = objLeave.RetrieveEmployeeTaskDetails(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(Session["userno"]));
            int ua_no = Convert.ToInt32(Session["userno"]);
            int ua_type = Convert.ToInt32(Session["usertype"]);

            DataSet dsTasks = objSC.GetTaskForFacultyDashboard(ua_type, ua_no);
            if (dsTasks != null && dsTasks.Tables[0] != null && dsTasks.Tables[0].Rows.Count > 0)
            {
                TaskList = (from DataRow dr in dsTasks.Tables[0].Rows
                            select new EmployeeTask
                            {
                                //AL_URL = dr["AL_URL"].ToString(),
                                //ACTIVITY_NAME = dr["ACTIVITY_NAME"].ToString(),
                                //STAT = dr["STAT"].ToString()
                                PageNo = Convert.ToInt32(dr[0].ToString()),
                                LinkName = dr[1].ToString(),
                                Link = dr[2].ToString()
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HomeNonFaculty.BindListViewTask-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return TaskList;
    }
    private string BindLeaveBalance()
    {
        string CasualBalLeave = string.Empty;
        try
        {
            int userno = Convert.ToInt32(Session["idno"]);
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            if (userno > 0)
            {
                DataSet ds = objLeave.RetrieveLeaveBalance(userno, currentYear);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    CasualBalLeave = ds.Tables[0].Rows[0]["CLBal"].ToString();
                }
                else
                {
                    CasualBalLeave = null;
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return CasualBalLeave;
    }
    private UpcommingHolidays BindHolidays()
    {
        UpcommingHolidays commingHoliday = new UpcommingHolidays();
        try
        {
            int userno = Convert.ToInt32(Session["idno"]);
            if (userno > 0)
            {
                DataSet ds = objLeave.RetrieveHolidays(userno);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    commingHoliday.Holiday = ds.Tables[0].Rows[0]["holiday"].ToString();
                    commingHoliday.Month = ds.Tables[0].Rows[0]["MonthN"].ToString();
                }
                else
                {
                    commingHoliday = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return commingHoliday;
    }
    private TableList BindTimeTable()
    {
        TableList tblListData = new TableList();
        try
        {
            DataSet dsTimeTable = new DataSet();
            int sessionno = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SESSIONNO)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SESSIONNO)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
            int schemeno = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT MAX(SCHEMENO)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + sessionno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT MAX(SCHEMENO)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + sessionno));
            int semesterno = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT MAX(SEMESTERNO)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT MAX(SEMESTERNO)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno));
            int uano = Convert.ToInt32(Session["userno"].ToString());
            // Added on 07-04-2020
            int sectionNo = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SectionNo)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(SectionNo)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno));
            // Added on 07-04-2020
            dsTimeTable = objSC.RetrieveFacultyTimeTableDetails(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), uano, sectionNo);
            List<TimeTable> objTTList = new List<TimeTable>();
            if (dsTimeTable != null)
            {
                if (dsTimeTable.Tables[0] != null && dsTimeTable.Tables[0].Rows.Count > 0)
                {
                    objTTList = (from DataRow dr in dsTimeTable.Tables[0].Rows
                                 select new TimeTable
                                 {
                                     // Slot = dr[0].ToString(),
                                     Monday = dr[0].ToString(),
                                     Tuesday = dr[1].ToString(),
                                     Wednesday = dr[2].ToString(),
                                     Thursday = dr[3].ToString(),
                                     Friday = dr[4].ToString(),
                                     Saturday = dr[5].ToString(),
                                     Slot = dr[6].ToString(),
                                 }).ToList();
                    tblListData.objTTList = objTTList;
                }
                else
                {
                    tblListData = null;
                }
            }
            else
            {
                tblListData = null;
            }
        }
        catch (Exception ex)
        {

        }
        return tblListData;
    }



    [WebMethod]
    public static List<FacultyQuickAccess> ShowQuickAccessData()
    {
        homeNonFaculty a = new homeNonFaculty();
        List<FacultyQuickAccess> QuickAccess = a.GetQuickAccessData();
        return QuickAccess;
    }

    private List<FacultyQuickAccess> GetQuickAccessData()
    {
        List<FacultyQuickAccess> objQA = new List<FacultyQuickAccess>();
        try
        {
            DataSet ds = new DataSet();

            int UserTypeId = Convert.ToInt32(Session["userno"]);
            ds = objSC.GetQuickAccessForStudentDashboard(UserTypeId);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objQA = (from DataRow dr in ds.Tables[0].Rows
                             select new FacultyQuickAccess
                             {
                                 PageNo = Convert.ToInt32(dr[0].ToString()),
                                 LinkName = dr[1].ToString(),
                                 Link = dr[2].ToString()
                             }).ToList();
                }
                else
                {
                    objQA = null;
                }
            }
            else
            {
                objQA = null;
            }
        }
        catch (Exception ex)
        {

        }
        return objQA;
    }


   

    protected void lnkCourse_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;


        HiddenField hdnCoursename = lnk.NamingContainer.FindControl("hdnCoursename") as HiddenField;
        HiddenField hdnSchemename = lnk.NamingContainer.FindControl("hdnSchemename") as HiddenField;
        HiddenField hdnSectionname = lnk.NamingContainer.FindControl("hdnSectionname") as HiddenField;
        HiddenField hdnSubjecttype = lnk.NamingContainer.FindControl("hdnSubjecttype") as HiddenField;
        HiddenField hdnBatch = lnk.NamingContainer.FindControl("hdnBatch") as HiddenField;
        HiddenField hdnCourseno = lnk.NamingContainer.FindControl("hdnCourseno") as HiddenField;
        HiddenField hdnSectionno = lnk.NamingContainer.FindControl("hdnSectionno") as HiddenField;
        HiddenField hdnBatchno = lnk.NamingContainer.FindControl("hdnBatchno") as HiddenField;
        HiddenField hdnSubId = lnk.NamingContainer.FindControl("hdnSubId") as HiddenField;

        //hdnSchemename.Value,hdnSubjecttype.Value,hdnSubId.Value
        string[] arr = new string[] { hdnCoursename.Value,hdnSchemename.Value,hdnSectionname.Value,
            hdnSubjecttype.Value,hdnBatch.Value,hdnCourseno.Value,hdnSectionno.Value,hdnBatchno.Value,hdnSubId.Value};

        Session["arr"] = arr;


        //ArrayList arr = new ArrayList();
        //arr.Add(hdnCoursename.Value);
        //arr.Add(hdnSchemename.Value);
        //arr.Add(hdnSectionname.Value);
        //arr.Add(hdnSubjecttype.Value);
        //arr.Add(hdnBatch.Value);

        //string arry = String.Join(",", ((string[])arr.ToArray(typeof(String))));

        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            //string pageurl = "Academic/AttendenceByFaculty.aspx?pageno=112&coursename=" + hdnCoursename.Value + "&schemename=" + hdnSchemename.Value
            //    + "&sectionname=" + hdnSectionname.Value + "&subjecttype=" + hdnSubjecttype.Value + "&batch=" + hdnBatch.Value
            //     + "&courseno=" + hdnCourseno.Value + "&sectionno=" + hdnSectionno.Value + "&batchno=" + hdnBatchno.Value
            //      + "&subid=" + hdnSubId.Value;

            string pageurl = "Academic/AttendenceByFaculty.aspx?pageno=112";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('" + pageurl + "','_newtab');", true);


        }
    }

    public string GetFileName(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return filename.ToString();
        else
            return "None";
    }

    public void Show_Notice()
    {
        //DataSet ds = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
        DataSet ds = objNc.GetUserTypeWiseNewsFaculty_HOD(Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]));
        if (ds.Tables.Count > 0 && ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            lvActiveNotice.DataSource = ds.Tables[0];
            lvActiveNotice.DataBind();
        }

        if (ds.Tables.Count > 0 && ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            lvExpNotice.DataSource = ds.Tables[1];
            lvExpNotice.DataBind();
        }
       
    }

    public string GetFileNamePath(object filename)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = filename.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {
                return "";
            }
            else
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                if (CheckIFExits(ImageName) == true)
                {
                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    return string.Format(ResolveUrl("~/DownloadImg/" + ImageName));
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "homeFaculty.aspx.GetFileNamePath() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
        return string.Empty;
    }

    protected string checkFile(string filename)
    {
        string filepath = Server.MapPath("~//UPLOAD_FILES//NOTICE_DOCUMENT/");
        string returnPath = "UPLOAD_FILES/NOTICE_DOCUMENT/";
        string returnFilename = "";

        FileInfo myfile = new FileInfo(filepath + filename);
        if (myfile.Exists)
        {
            if (filename.Contains(' '))
            {
                returnFilename = System.Web.HttpUtility.UrlPathEncode(filename);
            }
            else
            {
                returnFilename = filename;
            }
            return returnPath + returnFilename;
        }
        else
        {
            return "";
        }
    }

    #region BlogStorage
    public bool CheckIFExits(string FileName)
    {
        bool retIfExists = false;

        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                retIfExists = true;
            });
        }
        catch (Exception) { }
        return retIfExists;
    }
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    #endregion BlogStorage
}