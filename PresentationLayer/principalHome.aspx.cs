using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using IITMS.SQLServer.SQLDAL;
using System.Data.Linq;
using System.Collections.Generic;
using System.Web.Services;
//using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.IO;
public partial class principalHome : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    NewsController objNc = new NewsController();
    LeavesController objLeave = new LeavesController();


    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    Show_Notice();//PRASHANTG-TN56760-180424
                    BindListViewNotice();
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "principalHome.aspx.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnNotice_Click(object sender, EventArgs e)
    {
        Show_Notice();
    }
    public void Show_Notice()
    {
        try
        {
            DataSet ds = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                     lvActiveNotice.DataSource = ds.Tables[0];
                     lvActiveNotice.DataBind();
                }
                if (ds.Tables.Count > 0 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                     lvExpNotice.DataSource = ds.Tables[1];
                    lvExpNotice.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.Show_Notice() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //public string GetFileNamePath(object filename)
    //{
    //    string filepath = Server.MapPath("~//UPLOAD_FILES//NOTICE_DOCUMENT/");

    //    FileInfo myfile = new FileInfo(filepath + filename);
    //    if (myfile.Exists)
    //    {
    //        return "~/upload_files/notice_document/" + filename.ToString();
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
    public string GetFileName(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return filename.ToString();
        else
            return "None";
    }
        
    public  void GetFileNamePathEventForActiveNotice(object sender, CommandEventArgs e)
    {
        string filename = e.CommandArgument.ToString();
        GetFileNamePath(filename);
    }

    protected void GetFileNamePathEventForExpiredNotice(object sender, CommandEventArgs e)
    {
        string filename = e.CommandArgument.ToString();
        GetFileNamePath(filename);
    }
    
    protected string GetFileNamePath(object filename)
    {
        string Url = string.Empty;
        string fileUrl = string.Empty;
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

                fileUrl = string.Format(ResolveUrl("~/DownloadImg/" + ImageName));


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                if (CheckIFExits(ImageName) == true)
                {
                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    string script = "window.open('" + fileUrl + "', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script, true);
                    return string.Format(ResolveUrl("~/DownloadImg/" + ImageName));
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetFileNamePath() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
        return string.Empty;
    }

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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.CheckIFExits() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retIfExists;
    }
    //Method Added by PRASHANTG-TN56760-TN56760-290324-fetched 3 counters
    [WebMethod]
    public static List<PrincipalDashboardModel.StudentCounters> ShowCounters()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.StudentCounters> objStudCount = new List<PrincipalDashboardModel.StudentCounters>();
        objStudCount = p.getCountDetail();
        return objStudCount;       
      
    }
    private List<PrincipalDashboardModel.StudentCounters> getCountDetail()
    {
        List<PrincipalDashboardModel.StudentCounters> objStudCount = new List<PrincipalDashboardModel.StudentCounters>();
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT ASTUD LEFT OUTER JOIN USER_ACC UA ON (ASTUD.IDNO = UA.UA_IDNO)", "DISTINCT COUNT(IDNO) 'TOTAL',SUM (CASE WHEN SEX='M' THEN 1 ELSE 0 END) 'MALE'", "SUM (CASE WHEN SEX='F' THEN 1 ELSE 0 END) 'FEMALE',SUM (CASE WHEN SEX='T' THEN 1 ELSE 0 END) 'OTHER'", "SEX IN ('M','F','T') AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(UA_STATUS,0) = 0 AND ISNULL(UA_TYPE,0)=2", "");
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudCount = (from DataRow dr in ds.Tables[0].Rows
                                    select new PrincipalDashboardModel.StudentCounters
                                    {
                                        _male = dr["MALE"].ToString(),
                                        _female = dr["FEMALE"].ToString(),
                                        _other = dr["OTHER"].ToString(),
                                        _total = dr["TOTAL"].ToString()
                                    }).ToList();
                }
                else
                {
                    objStudCount = null;
                }
            }
            else
            {
                objStudCount = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.getCountDetail() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objStudCount;
    }

    //BELOW CODE COMMENTED BY PRASHANTG-TN56760-270324- AGGREGATED TO A SINGLE METHOD ShowCounters()
    //[WebMethod]
    //public static string ShowMaleCount()
    //{
    //    principalHome p = new principalHome();
    //    string count = p.getMaleCountDetail();
    //    return count;
    //}

    //private string getMaleCountDetail()
    //{
    //    string maleCount = objCommon.LookUp("ACD_STUDENT ASTUD LEFT OUTER JOIN USER_ACC UA ON (ASTUD.IDNO = UA.UA_IDNO)", "COUNT(DISTINCT IDNO) MALECOUNT", "SEX='M' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(UA_STATUS,0) = 0 AND ISNULL(UA_TYPE,0)=2");
    //    return maleCount;
    //}

    ////Method to get female count
    //[WebMethod]
    //public static string ShowFeMaleCount()
    //{
    //    principalHome p = new principalHome();
    //    string count = p.getFemaleCountDetail();
    //    return count;
    //}
    //private string getFemaleCountDetail()
    //{
    //    string femaleCount = objCommon.LookUp("ACD_STUDENT ASTUD LEFT OUTER JOIN USER_ACC UA ON (ASTUD.IDNO = UA.UA_IDNO)", "COUNT(DISTINCT IDNO) FEMALECOUNT", "SEX='F' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(UA_STATUS,0) = 0 AND ISNULL(UA_TYPE,0)=2");
    //    return femaleCount;
    //}

    ////other count - Added By Rishabh on 02012024
    //[WebMethod]
    //public static string ShowOtherCount()
    //{
    //    principalHome p = new principalHome();
    //    string count = p.getOtherCountDetail();
    //    return count;
    //}
    //private string getOtherCountDetail()
    //{
    //    string othercount = objCommon.LookUp("ACD_STUDENT ASTUD LEFT OUTER JOIN USER_ACC UA ON (ASTUD.IDNO = UA.UA_IDNO)", "COUNT(DISTINCT IDNO) OTHERCOUNT", "SEX='T' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(UA_STATUS,0) = 0 AND ISNULL(UA_TYPE,0)=2");
    //    return othercount;
    //}


    //Method to get active user count
    [WebMethod]
    public static string ActiveUserCount()
    {
        principalHome p = new principalHome();
        string count = p.getActiveUserDetails();
        return count;
    }
    private string getActiveUserDetails()
    {
        string count = objCommon.LookUp("USER_ACC", "COUNT(DISTINCT UA_NO) USERCOUNT", "ISNULL(UA_STATUS,0)=0 ");
        return count;
    }


    //Method to get total student count
    //[WebMethod]
    //public static string TotalStudentCount()
    //{
    //    principalHome p = new principalHome();
    //    string count = p.getTotalStudentDetails();
    //    return count;
    //}
    //private string getTotalStudentDetails()
    //{
    //    string count = objCommon.LookUp("ACD_STUDENT ASTUD LEFT OUTER JOIN USER_ACC UA ON (ASTUD.IDNO = UA.UA_IDNO)", "COUNT(DISTINCT IDNO) STUDENT", "ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(UA_STATUS,0) = 0 AND ISNULL(UA_TYPE,0)=2");
    //    return count;
    //}

    // To Get Admission Batch wise Students count
    [WebMethod]
    public static List<PrincipalDashboardModel.StudentsCount> BindStudentsCount()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.StudentsCount> objStudCount = new List<PrincipalDashboardModel.StudentsCount>();
        objStudCount = p.GetStudentCounts();
        return objStudCount;
    }

    public List<PrincipalDashboardModel.StudentsCount> GetStudentCounts()
    {
        List<PrincipalDashboardModel.StudentsCount> objStudCount = new List<PrincipalDashboardModel.StudentsCount>();
        try
        {
            DataSet ds = objSC.GetAdmissionDataForPrincipalDashboard();
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudCount = (from DataRow dr in ds.Tables[0].Rows
                                    select new PrincipalDashboardModel.StudentsCount
                                    {
                                        BatchNo = dr["BATCHNO"].ToString(),
                                        Year = dr["YEAR"].ToString(),
                                        Count = dr["STUDCOUNT"].ToString()
                                    }).ToList();
                }
                else
                {
                    objStudCount = null;
                }
            }
            else
            {
                objStudCount = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetStudentCounts() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objStudCount;
    }

    // TO GET ACADEMIC ACTIVITY DETAILS 
    [WebMethod]
    public static List<PrincipalDashboardModel.ActivityDetails> BindActivityDetails()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.ActivityDetails> objActivity = new List<PrincipalDashboardModel.ActivityDetails>();
        objActivity = p.GetActivityDetails();
        return objActivity;
    }

    public List<PrincipalDashboardModel.ActivityDetails> GetActivityDetails()
    {
        List<PrincipalDashboardModel.ActivityDetails> objActivity = new List<PrincipalDashboardModel.ActivityDetails>();
        try
        {
            DataSet ds = objSC.GetAcdActivityDetailsForPrincipalDashboard();
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objActivity = (from DataRow dr in ds.Tables[0].Rows
                                   select new PrincipalDashboardModel.ActivityDetails
                                   {
                                       ActivityName = dr["ACTIVITY_NAME"].ToString(),
                                       SessionName = dr["SESSION_NAME"].ToString(),
                                       StartDate = dr["START_DATE"].ToString(),
                                       EndDate = dr["END_DATE"].ToString(),
                                       ActivityStatus = dr["Activity_Status"].ToString()
                                   }).ToList();
                }
                else
                {
                    objActivity = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetActivityDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objActivity;
    }

    // TO GET ACADEMIC ACTIVE NOTICE  
    [WebMethod]
    public static List<PrincipalDashboardModel.ActiveNotice> BindActiveNotice()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.ActiveNotice> objActivN = new List<PrincipalDashboardModel.ActiveNotice>();
        objActivN = p.GetActiveNotice();
        return objActivN;
    }

    public List<PrincipalDashboardModel.ActiveNotice> GetActiveNotice()
    {
        List<PrincipalDashboardModel.ActiveNotice> objActivN = new List<PrincipalDashboardModel.ActiveNotice>();
        try
        {
            DataSet ds = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objActivN = (from DataRow dr in ds.Tables[0].Rows
                                   select new PrincipalDashboardModel.ActiveNotice
                                   {
                                       MM = dr["MM"].ToString(),
                                       DD = dr["DD"].ToString(),
                                       TITLE = dr["TITLE"].ToString(),
                                       FILENAME = dr["FILENAME"].ToString(),
                                       NEWSDESC = dr["NEWSDESC"].ToString()
                                   }).ToList();
                }
                else
                {
                    objActivN = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetActivityNotice() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objActivN;
    }

    // TO GET ACADEMIC EXPIRED NOTICE  
    [WebMethod]
    public static List<PrincipalDashboardModel.ActiveNotice> BindExpNotice()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.ActiveNotice> objExpN = new List<PrincipalDashboardModel.ActiveNotice>();
        objExpN = p.GetExpNotice();
        return objExpN;
    }

    public List<PrincipalDashboardModel.ActiveNotice> GetExpNotice()
    {
        List<PrincipalDashboardModel.ActiveNotice> objExpN = new List<PrincipalDashboardModel.ActiveNotice>();
        try
        {
            DataSet ds = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (ds != null)
            {
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    objExpN = (from DataRow dr in ds.Tables[1].Rows
                                 select new PrincipalDashboardModel.ActiveNotice
                                 {
                                     MM = dr["MM"].ToString(),
                                     DD = dr["DD"].ToString(),
                                     TITLE = dr["TITLE"].ToString(),
                                     FILENAME = dr["FILENAME"].ToString(),
                                     NEWSDESC = dr["NEWSDESC"].ToString()
                                 }).ToList();
                }
                else
                {
                    objExpN = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetExpNotice() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objExpN;
    }
    
    // TO GET ADMISSION FEES DETAILS 
    //[WebMethod]
    //public static List<PrincipalDashboardModel.AdmFeesDeatils> BindAdmFeesDetails()
    //{
    //    principalHome p = new principalHome();
    //    List<PrincipalDashboardModel.AdmFeesDeatils> objAdmFees = new List<PrincipalDashboardModel.AdmFeesDeatils>();
    //    objAdmFees = p.GetAdmFeesDetails();
    //    return objAdmFees;
    //}

    //public List<PrincipalDashboardModel.AdmFeesDeatils> GetAdmFeesDetails()
    //{
    //    List<PrincipalDashboardModel.AdmFeesDeatils> objAdmFee = new List<PrincipalDashboardModel.AdmFeesDeatils>();
    //    try
    //    {
    //        DataSet ds = objSC.GetAdmFeesDetailsForPrincipalDashboard();
    //        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            objAdmFee = (from DataRow dr in ds.Tables[0].Rows
    //                         select new PrincipalDashboardModel.AdmFeesDeatils
    //                         {
    //                             Receipt = dr["Receipt"].ToString(),
    //                             Fee = dr["TOTAL"].ToString(),
    //                             Year = dr["YEAR"].ToString()
    //                         }).ToList();
    //        }
    //        else
    //        {
    //            objAdmFee = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    return objAdmFee;
    //}

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
    // TO GET NEWS DATA 
    [WebMethod]
    public static List<PrincipalDashboardModel.PrincipalNews> ShowNewsData()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.PrincipalNews> NewsData = p.BindListViewNews();
        return NewsData;
    }
    private List<PrincipalDashboardModel.PrincipalNews> BindListViewNews()
    {
        List<PrincipalDashboardModel.PrincipalNews> newsList = new List<PrincipalDashboardModel.PrincipalNews>();
        try
        {
            DataSet dsNews = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (dsNews != null && dsNews.Tables[0] != null && dsNews.Tables[0].Rows.Count > 0)
            {
                newsList = (from DataRow dr in dsNews.Tables[0].Rows
                            select new PrincipalDashboardModel.PrincipalNews
                            {
                                Day = dr["DD"].ToString(),
                                Month = dr["MM"].ToString(),
                                //Link = dr["LINK"].ToString(),
                                Link = checkFile(dr["FILENAME"].ToString()),
                                Title = dr["TITLE"].ToString(),
                                NewsDesc = dr["NEWSDESC"].ToString()
                            }).ToList();
            }
            else
            {
                newsList = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return newsList;
    }

    public static List<PrincipalDashboardModel.PrincipalNews> ShowExpiredNewsData()
    {
        principalHome p = new principalHome();
        List<PrincipalDashboardModel.PrincipalNews> ExpiredNewsData = p.BindListViewExpiredNews();
        return ExpiredNewsData;
    }
    private List<PrincipalDashboardModel.PrincipalNews> BindListViewExpiredNews()
    {
        List<PrincipalDashboardModel.PrincipalNews> ExpirednewsList = new List<PrincipalDashboardModel.PrincipalNews>();
        try
        {
            NewsController objNc = new NewsController();
            //DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NEWS");
            DataSet dsNews = objNc.GetUserTypeWiseNews(Convert.ToInt32(Session["usertype"]));
            if (dsNews != null && dsNews.Tables[1] != null && dsNews.Tables[1].Rows.Count > 0)
            {
                ExpirednewsList = (from DataRow dr in dsNews.Tables[1].Rows
                                   select new PrincipalDashboardModel.PrincipalNews
                                   {
                                       Day = dr["DD"].ToString(),
                                       Month = dr["MM"].ToString(),
                                       //Link = dr["LINK"].ToString(),
                                       Link = checkFile(dr["FILENAME"].ToString()),
                                       Title = dr["TITLE"].ToString(),
                                       NewsDesc = dr["NEWSDESC"].ToString()
                                   }).ToList();
            }
            else
            {
                ExpirednewsList = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.BindListViewExpiredNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return ExpirednewsList;
    }

    // TO GET RESULT ANALYSIS DATA 
    [WebMethod]
    public static PrincipalDashboardModel.PrincipalResultAnalysisData ShowResultData()
    {
        principalHome p = new principalHome();
        PrincipalDashboardModel.PrincipalResultAnalysisData resultData = p.GetResultAnalysisData();
        return resultData;
    }
    private PrincipalDashboardModel.PrincipalResultAnalysisData GetResultAnalysisData()
    {
        PrincipalDashboardModel.PrincipalResultAnalysisData ResultData = new PrincipalDashboardModel.PrincipalResultAnalysisData();
        List<PrincipalDashboardModel.ResultHeader> tHead = new List<PrincipalDashboardModel.ResultHeader>();
        List<PrincipalDashboardModel.ResultBody> tBody = new List<PrincipalDashboardModel.ResultBody>();
        try
        {
            DataSet ds = objSC.GetResultAnalysisForPrincipalDashboard();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    tHead.Add(new PrincipalDashboardModel.ResultHeader()
                    {
                        Header = dc.ToString()
                    });
                }
                tHead.RemoveRange(0, 7);
                //condition start-- added by Nikhil V. Lambe to specify between sem1,sem2,sem3,sem4
                string count = ds.Tables[0].Columns.Count.ToString();
                //HiddenField hdnField = new HiddenField();
                //hdnField.FindControl("hdnCount");
                //hdnField.Value = count;
                //string head =Convert.ToString(tHead);

                if (count == "8")
                {
                    tBody = (from DataRow dr in ds.Tables[0].Rows
                             select new PrincipalDashboardModel.ResultBody
                             {
                                 DegreeName = dr["DEGREENAME"].ToString(),
                                 BranchName = dr["BRANCHNAME"].ToString(),
                                 BranchShortName = dr["SHORTNAME"].ToString(),
                                 SessionName = dr["SESSION_PNAME"].ToString(),
                                 Sem1 = dr[7].ToString(),
                                 //Sem2 = dr[8].ToString(),
                                 //Sem3 = dr[9].ToString(),
                                 //Sem4 = dr[10].ToString()
                             }).ToList();
                }
                else if (count == "9")
                {
                    tBody = (from DataRow dr in ds.Tables[0].Rows
                             select new PrincipalDashboardModel.ResultBody
                             {
                                 DegreeName = dr["DEGREENAME"].ToString(),
                                 BranchName = dr["BRANCHNAME"].ToString(),
                                 BranchShortName = dr["SHORTNAME"].ToString(),
                                 SessionName = dr["SESSION_PNAME"].ToString(),
                                 Sem1 = dr[7].ToString(),
                                 Sem2 = dr[8].ToString(),
                                 //Sem3 = dr[9].ToString(),
                                 //Sem4 = dr[10].ToString()
                             }).ToList();
                }
                else if (count == "10")
                {
                    tBody = (from DataRow dr in ds.Tables[0].Rows
                             select new PrincipalDashboardModel.ResultBody
                             {
                                 DegreeName = dr["DEGREENAME"].ToString(),
                                 BranchName = dr["BRANCHNAME"].ToString(),
                                 BranchShortName = dr["SHORTNAME"].ToString(),
                                 SessionName = dr["SESSION_PNAME"].ToString(),
                                 Sem1 = dr[7].ToString(),
                                 Sem2 = dr[8].ToString(),
                                 Sem3 = dr[9].ToString(),
                                 //Sem4 = dr[10].ToString()
                             }).ToList();
                }
                else if (count == "11")
                {
                    tBody = (from DataRow dr in ds.Tables[0].Rows
                             select new PrincipalDashboardModel.ResultBody
                             {
                                 DegreeName = dr["DEGREENAME"].ToString(),
                                 BranchName = dr["BRANCHNAME"].ToString(),
                                 BranchShortName = dr["SHORTNAME"].ToString(),
                                 SessionName = dr["SESSION_PNAME"].ToString(),
                                 Sem1 = dr[7].ToString(),
                                 Sem2 = dr[8].ToString(),
                                 Sem3 = dr[9].ToString(),
                                 Sem4 = dr[10].ToString()
                             }).ToList();
                }
                // condition end
            }
            else
            {
                ResultData = null;
            }

            ResultData.tHeader = tHead;
            ResultData.tBody = tBody;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetResultAnalysisData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return ResultData;
    }


    [WebMethod]
    public static List<PrincipalQuickAccess> ShowQuickAccessData()
    {
        principalHome p = new principalHome();
        List<PrincipalQuickAccess> QuickAccess = p.GetQuickAccessData();
        return QuickAccess;
    }
    private List<PrincipalQuickAccess> GetQuickAccessData()
    {
        List<PrincipalQuickAccess> objQA = new List<PrincipalQuickAccess>();
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
                             select new PrincipalQuickAccess
                             {
                                 PageNo = Convert.ToInt32(dr[0].ToString()),
                                 LinkName = dr[1].ToString(),
                                 Link = ResolveUrl(dr[2].ToString())
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetQuickAccessData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objQA;
    }

    [WebMethod]
    public static LeaveCount BindLeaveCount()
    {
        principalHome p = new principalHome();
        LeaveCount objLeveCount = new LeaveCount();
        objLeveCount = p.GetLeaveCount();
        return objLeveCount;
    }
    public LeaveCount GetLeaveCount()
    {
        LeaveCount objLeveCount = new LeaveCount();
        try
        {
            DataSet ds = objLeave.GetLeavecountForDashboard(Convert.ToInt32(Session["userno"]));
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objLeveCount = (from DataRow dr in ds.Tables[0].Rows
                                    select new LeaveCount
                                    {
                                        ToTal_Applied = dr["ToTal_Applied"].ToString(),
                                        Approve_Leave = dr["Approve_Leave"].ToString(),
                                        Pending_Leave = dr["Pending_Leave"].ToString()
                                    }).FirstOrDefault();
                }
                else
                {
                    objLeveCount = null;

                }
            }
            else
            {
                objLeveCount = null;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetLeaveCount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objLeveCount;

    }

    // Added by shrikant Bharne on 8-06-2020 for Dashboard pop up//

    /* protected void btnAppliedLeave_Click(object sender, EventArgs e)
     {
         try
         {

             DataTable dt = objLeave.RetrieveEmpDeatilsallLeaveDashboard(Convert.ToInt32(Session["userno"]));
             {
                 if (dt.Rows.Count > 0)
                 {
                     ModalPopupExtender1.Show();
                     lvEmp.DataSource = dt;
                     lvEmp.DataBind();
                     //divdemo2.Visible = true;
                 }
             }

         }
         catch (Exception ex)
         {

         }
     }


     protected void btnClose_Click(object sender, EventArgs e)
     {
         ModalPopupExtender1.Hide();
     }
     protected void btnApprovedLeave_Click(object sender, EventArgs e)
     {
         try
         {
             DataTable dt = objLeave.RetrieveEmpDeatilsallLeaveApproveDashboard(Convert.ToInt32(Session["userno"]));
             if (dt.Rows.Count > 0)
             {
                 ModalPopupExtender1.Show();
                 lvEmp.DataSource = dt;
                 lvEmp.DataBind();
             }


         }
         catch (Exception ex)
         {

         }

     }
     protected void btnPendingLeave_Click(object sender, EventArgs e)
     {
         try
         {
             DataTable dt = objLeave.RetrieveEmpDeatilsallLeavePendingDashboard(Convert.ToInt32(Session["userno"]));
             if (dt.Rows.Count > 0)
             {
                 ModalPopupExtender1.Show();
                 lvEmp.DataSource = dt;
                 lvEmp.DataBind();
             }

         }
         catch (Exception ex)
         {

         }

     }*/
    
    [WebMethod]
    public static List<LeaveApprovalData> ShowLeaveapprove()
    {
        principalHome p = new principalHome();
        List<LeaveApprovalData> LeaveApprove = p.GetLeaveApprovalpanel();
        return LeaveApprove;
    }
    private List<LeaveApprovalData> GetLeaveApprovalpanel()
    {
        List<LeaveApprovalData> objQA = new List<LeaveApprovalData>();
        try
        {
            DataSet ds = new DataSet();

            int UANO = Convert.ToInt32(Convert.ToInt32(Session["userno"]));
            ds = objLeave.RetrieveEmpDeatilsallLeaveApproveDashboardnew(UANO);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objQA = (from DataRow dr in ds.Tables[0].Rows
                             select new LeaveApprovalData
                             {
                                 EmpName = dr[0].ToString(),
                                 SUBDEPT = dr[1].ToString(),
                                 LName = dr[2].ToString(),
                                 // From_date =Convert.ToDateTime(dr[3].ToString()),
                                 // TO_DATE = Convert.ToDateTime(dr[4].ToString())   
                                 From_date = dr[3].ToString(),
                                 TO_DATE = dr[4].ToString(),
                                 LETRNO = Convert.ToInt32(dr[5].ToString())
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetLeaveApprovalpanel() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objQA;
    }


    [WebMethod]
    public static List<LeaveApprovalData> ShowLeaveapproveCount()
    {
        principalHome p = new principalHome();
        List<LeaveApprovalData> LeaveApprove = p.GetLeaveApprovalpanelLeave();
        return LeaveApprove;
    }
    private List<LeaveApprovalData> GetLeaveApprovalpanelLeave()
    {
        List<LeaveApprovalData> objQA = new List<LeaveApprovalData>();
        try
        {
            DataSet ds = new DataSet();

            int UANO = Convert.ToInt32(Convert.ToInt32(Session["userno"]));
            ds = objLeave.RetrieveEmpDeatilsallLeaveApprovecountDashboardnew(UANO);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objQA = (from DataRow dr in ds.Tables[0].Rows
                             select new LeaveApprovalData
                             {
                                 EmpName = dr[0].ToString(),
                                 SUBDEPT = dr[1].ToString(),
                                 LName = dr[2].ToString(),
                                 // From_date =Convert.ToDateTime(dr[3].ToString()),
                                 // TO_DATE = Convert.ToDateTime(dr[4].ToString())   
                                 From_date = dr[3].ToString(),
                                 TO_DATE = dr[4].ToString(),
                                 // LETRNO = Convert.ToInt32(dr[5].ToString())

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
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetLeaveApprovalpanelLeave() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objQA;
    }

    [WebMethod]
    public static List<LeaveApprovalData> ShowPendingCount()
    {
        principalHome p = new principalHome();
        List<LeaveApprovalData> LeaveApprove = p.GetLeavePendingpanelLeave();
        return LeaveApprove;
    }
    private List<LeaveApprovalData> GetLeavePendingpanelLeave()
    {
        List<LeaveApprovalData> objQA = new List<LeaveApprovalData>();
        try
        {
            DataSet ds = new DataSet();

            int UANO = Convert.ToInt32(Convert.ToInt32(Session["userno"]));
            ds = objLeave.RetrieveEmpDeatilsallLeavePendingcountDashboardnew(UANO);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objQA = (from DataRow dr in ds.Tables[0].Rows
                             select new LeaveApprovalData
                             {
                                 EmpName = dr[0].ToString(),
                                 SUBDEPT = dr[1].ToString(),
                                 LName = dr[2].ToString(),
                                 // From_date =Convert.ToDateTime(dr[3].ToString()),
                                 // TO_DATE = Convert.ToDateTime(dr[4].ToString())   
                                 From_date = dr[3].ToString(),
                                 TO_DATE = dr[4].ToString(),
                                 Pending_on = dr[5].ToString()
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "principalHome.aspx.GetLeavePendingpanelLeave() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return objQA;
    }
    
    [WebMethod]
    public static int Approvedleave(int id)
    {

        principalHome objnew = new principalHome();
        int reaponce = objnew.UpdateLeave(id);
        return reaponce;
    }

    public int UpdateLeave(int id)
    {
        //int cnt = objSC.UpdateTodoListData(Convert.ToInt32(Session["userno"]), id);
        int LETRNO = id;
        Session["LETRNO"] = LETRNO;
        int UA_NO = Convert.ToInt32(Session["userno"]);
        DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
        string Status;
        Status = "A".ToString().Trim();
        bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));

        int cnt = objLeave.UpdateAppPassEntry(LETRNO, UA_NO, Status, "Leave Approve", Aprdate, 0);
        //SendMail(id);
        return cnt;
    }

    
    #region BlogStorage
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
    //public void SendMail(int letrno)
    //{
    //    try
    //    {
    //        string body = string.Empty;
    //        Leaves objLM = new Leaves();
    //        objLM.LETRNO = letrno;
    //        DataSet ds = objLeave.GetSMSInformation(objLM);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            string fromEmailId = ds.Tables[0].Rows[0]["SenderEmailId"].ToString();
    //            string fromEmailPwd = ds.Tables[0].Rows[0]["SenderEmailPassword"].ToString();
    //            if (ds.Tables[1].Rows.Count > 0)
    //            {
    //                string STATUS = ds.Tables[1].Rows[0]["STATUS"].ToString();
    //                string Name = ds.Tables[1].Rows[0]["NAME"].ToString();
    //                string AUTHORITY_NAME = ds.Tables[1].Rows[0]["AUTHORITY_NAME"].ToString();
    //                //AUTHORITY_NAME
    //                string FromDate = ds.Tables[1].Rows[0]["From_Date"].ToString();
    //                string ToDate = ds.Tables[1].Rows[0]["To_Date"].ToString();

    //                string employeename = ds.Tables[1].Rows[0]["name"].ToString();
    //                double tot_days = Convert.ToDouble(ds.Tables[1].Rows[0]["NO_OF_DAYS"].ToString());
    //                string Joindt = ds.Tables[1].Rows[0]["Joindt"].ToString();
    //                string toEmailId = ds.Tables[1].Rows[0]["EMAILID"].ToString();//Approve_Status
    //                string Approve_Status = ds.Tables[1].Rows[0]["Approve_Status"].ToString();//Approve_Status
    //                string Employeemail = ds.Tables[1].Rows[0]["Employeemail"].ToString();
    //                string APR_REMARKS = ds.Tables[1].Rows[0]["APR_REMARKS"].ToString();
    //                string Sub = "Leave Application Notification";
    //                //Full day/half day [casual] Leave Application Submitted By ['ABC']. Date from 1/1/2018 to 1/12018 & Joining Date is [Joindt]
    //                //body = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;

    //                //body = leavestatus + leavename + " Application Submitted By " + name + ". Date From " + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + " & Joining Date is " + Joindt;

    //                string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
    //                string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
    //                string leavenamestatus = string.Empty;

    //                if (leavestatus.Trim() != string.Empty || leavestatus != "")
    //                {
    //                    leavenamestatus = leavestatus + ' ' + leavename;
    //                }
    //                else
    //                {
    //                    leavenamestatus = leavename;
    //                }
    //                string ToEmail = string.Empty;//Approve_Status
    //                string successNote = string.Empty;
    //                if (STATUS == "A")
    //                {
    //                    successNote = " successufully !";
    //                }
    //                if (STATUS == "A")
    //                {
    //                    body = "Dear " + employeename + "," + Environment.NewLine +
    //                        "Your Leave application dated From " + FromDate + " To " + ToDate + " is " + Approve_Status + "." + Environment.NewLine + Environment.NewLine
    //                        + "HR-Department" + Environment.NewLine + " Sri Venkateswara College of Engineering";


    //                    //body = "Greeti " + employeename + "," + Environment.NewLine + " Your applied " + leavenamestatus + " from " + FromDate
    //                    //           + " " + "to " + ToDate + " , " + Environment.NewLine + ""
    //                    //           + " has been " + Approve_Status+" "+ successNote;
    //                    ToEmail = ds.Tables[1].Rows[0]["EmailId"].ToString();
    //                }

    //                else if (STATUS == "R")
    //                {
    //                    body = "Dear " + employeename + "," + Environment.NewLine +
    //                        "Your Leave application dated From " + FromDate + " To " + ToDate + " is " + Approve_Status + " due to the following reason . " + Environment.NewLine +
    //                        "Reason : " + APR_REMARKS + Environment.NewLine + Environment.NewLine + "HR-Department" + Environment.NewLine + " Sri Venkateswara College of Engineering ";
    //                }
    //                SmtpClient smtpClient = new SmtpClient();
    //                // MailMessage mailMessage = new MailMessage(fromEmailId, toEmailId, Sub, body);
    //                MailMessage mailMessage = new MailMessage(fromEmailId, Employeemail, Sub, body);
    //                smtpClient.Credentials = new System.Net.NetworkCredential(fromEmailId, fromEmailPwd);

    //                smtpClient.EnableSsl = true;
    //                smtpClient.Port = 587; // 587
    //                smtpClient.Host = "smtp.gmail.com";

    //                ServicePointManager.ServerCertificateValidationCallback =
    //                delegate(object s, X509Certificate certificate,
    //                X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //                { return true; };

    //                smtpClient.Send(mailMessage);// .SendAsync(mailMessage, null);
    //                // lblMsg.Text = "Email has been successfully sent..!!";
    //                /*
    //                   text = "Hi " + multipleName + "," + Environment.NewLine + employeename + " " + "has applied a leave from " + FromDate
    //                            + " " + "to " + ToDate + " " + Environment.NewLine + "with reason " + "'" + txtReason.Text.Trim() + "'" + ","
    //                            + "Joining date will be " + JoiningDate;
    //                 * */
    //            }
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }

    //}

}
