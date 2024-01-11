///Created By : Rishabh B. 29.12.2023
///Removed old page, created new one with AJAX
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Services;
using System.Web.Script.Services;

public partial class ACADEMIC_AttendanceConfig : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();
    SessionActivityController activityController = new SessionActivityController();



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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            //Populate the Drop Down Lists

            PopulateDropDownList();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendanceConfig.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendanceConfig.aspx");
        }
    }

    //Fill Dropdowns
    private void PopulateDropDownList()
    {
        AcdAttendanceController objAtt = new AcdAttendanceController();
        int ua_no = Convert.ToInt32(HttpContext.Current.Session["userno"]);
        string college_ids = HttpContext.Current.Session["college_nos"].ToString();
        DataSet ds = objAtt.BindMastersAttendanceConfig(ua_no, 0, 1, college_ids);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlSession.DataSource = ds.Tables[0];
            ddlSession.DataTextField = "SESSION_NAME";
            ddlSession.DataValueField = "SESSIONID";
            ddlSession.DataBind();
        }
        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            ddlSchemeType.DataSource = ds.Tables[1];
            ddlSchemeType.DataTextField = "SCHEMETYPE";
            ddlSchemeType.DataValueField = "SCHEMETYPENO";
            ddlSchemeType.DataBind();
        }
        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
        {
            ddlSemester.DataSource = ds.Tables[2];
            ddlSemester.DataTextField = "SEMESTERNAME";
            ddlSemester.DataValueField = "SEMESTERNO";
            ddlSemester.DataBind();
        }
    }

    //Bind Colleges on session change
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Attendance.BindCollege> BindColleges(int SessionId)
    {
        AcdAttendanceController objAtt = new AcdAttendanceController();
        DataSet ds = new DataSet();
        List<Attendance.BindCollege> BindCollege = new List<Attendance.BindCollege>();

        try
        {
            int ua_no = Convert.ToInt32(HttpContext.Current.Session["userno"]);
            string college_ids = HttpContext.Current.Session["college_nos"].ToString();
            ds = objAtt.BindMastersAttendanceConfig(ua_no, SessionId, 2, college_ids);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                BindCollege = (from DataRow dr in ds.Tables[0].Rows
                               select new Attendance.BindCollege
                                  {
                                      College_Id = Convert.ToInt32(dr[0].ToString()),
                                      College_Name = dr[1].ToString(),
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ds.Dispose();
        }
        return BindCollege;
    }

    //Bind degrees on college selection change
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Attendance.BindDegree> BindDegrees(string CollegeIds)
    {
        AcdAttendanceController objAtt = new AcdAttendanceController();
        DataSet ds = new DataSet();
        List<Attendance.BindDegree> listDegree = new List<Attendance.BindDegree>();

        try
        {
            int ua_no = Convert.ToInt32(HttpContext.Current.Session["userno"]);
            ds = objAtt.BindMastersAttendanceConfig(ua_no, 0, 3, CollegeIds);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                listDegree = (from DataRow dr in ds.Tables[0].Rows
                              select new Attendance.BindDegree
                              {
                                  DegreeNo = Convert.ToInt32(dr[0].ToString()),
                                  Degree_Name = dr[1].ToString(),
                              }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ds.Dispose();
        }
        return listDegree;
    }

    //Save Button Click
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Attendance.AttendanceConfig> SaveAttendanceConfig(int Sessionno, string College_ids, string Degreenos, int SchemeType, string Semesternos, string txtStartDate, string txtEndDate, string lockbyday, bool SmsFac, bool EmailFac, bool TeachingPlan, bool IsActive)
    {
        AcdAttendanceController objAttC = new AcdAttendanceController();
        DataSet ds = new DataSet();
        List<Attendance.AttendanceConfig> ConfigList = new List<Attendance.AttendanceConfig>();
        Attendance.AttendanceConfig objAttE = new Attendance.AttendanceConfig();

        objAttE.SessionId = Sessionno;
        objAttE.College_Ids = College_ids;
        objAttE.DegreeNos = Degreenos;
        objAttE.SchemeType = SchemeType;
        objAttE.SemesterNos = Semesternos;
        objAttE.StartDate = Convert.ToDateTime(txtStartDate);
        objAttE.EndDate = Convert.ToDateTime(txtEndDate);
        objAttE.AttLockDays = lockbyday;
        objAttE.SMSFacility = SmsFac;
        objAttE.EmailFacility = EmailFac;
        objAttE.TeachingPlan = TeachingPlan;
        objAttE.ActiveStatus = IsActive;
        objAttE.OrgId = Convert.ToInt32(HttpContext.Current.Session["OrgId"]);

        try
        {
            ds = objAttC.InsUpdAttendanceConfig(objAttE);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ConfigList = (from DataRow dr in ds.Tables[0].Rows
                              select new Attendance.AttendanceConfig
                              {
                                  SessionId = Convert.ToInt32(dr[0].ToString()),
                                  SemesterNos = dr[1].ToString(),
                                  SchemeType = Convert.ToInt32(dr[2].ToString()),
                                  College_Ids = dr[3].ToString(),
                                  DegreeNos = dr[4].ToString(),

                                  SessionName = dr[5].ToString(),
                                  CollegeName = dr[6].ToString(),
                                  SchemetypeName = dr[7].ToString(),

                                  StartDateN = dr[8].ToString(),
                                  EndDateN = dr[9].ToString(),
                                  AttLockDays = dr[10].ToString(),

                                  SMSFacility = Convert.ToBoolean(dr[11].ToString()),
                                  EmailFacility = Convert.ToBoolean(dr[12].ToString()),
                                  TeachingPlan = Convert.ToBoolean(dr[13].ToString()),
                                  ActiveStatus = Convert.ToBoolean(dr[14].ToString()),


                              }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ds.Dispose();
        }
        return ConfigList;
    }

    //Bind Listview on PageLoad
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Attendance.AttendanceConfig> BindList()
    {
        AcdAttendanceController objAttC = new AcdAttendanceController();
        DataSet ds = new DataSet();
        List<Attendance.AttendanceConfig> ConfigList = new List<Attendance.AttendanceConfig>();

        try
        {
            int ua_type = Convert.ToInt32(HttpContext.Current.Session["usertype"].ToString());
            string college_ids = HttpContext.Current.Session["college_nos"].ToString();


            ds = objAttC.GetAttendanceConfigData(ua_type, college_ids, 1);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ConfigList = (from DataRow dr in ds.Tables[0].Rows
                              select new Attendance.AttendanceConfig
                              {
                                  SessionId = Convert.ToInt32(dr[0].ToString()),
                                  SemesterNos = dr[1].ToString(),
                                  SchemeType = Convert.ToInt32(dr[2].ToString()),
                                  College_Ids = dr[3].ToString(),
                                  DegreeNos = dr[4].ToString(),

                                  SessionName = dr[5].ToString(),
                                  CollegeName = dr[6].ToString(),
                                  SchemetypeName = dr[7].ToString(),

                                  StartDateN = dr[8].ToString(),
                                  EndDateN = dr[9].ToString(),
                                  AttLockDays = dr[10].ToString(),


                                  SMSFacility = Convert.ToBoolean(dr[11].ToString()),
                                  EmailFacility = Convert.ToBoolean(dr[12].ToString()),
                                  TeachingPlan = Convert.ToBoolean(dr[13].ToString()),
                                  ActiveStatus = Convert.ToBoolean(dr[14].ToString()),


                              }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ds.Dispose();
        }
        return ConfigList;
    }

    //Excel report click
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Attendance.AttendanceConfig> AttendanceConfigExcel()
    {
        AcdAttendanceController objAttC = new AcdAttendanceController();
        DataSet ds = new DataSet();
        List<Attendance.AttendanceConfig> ConfigList = new List<Attendance.AttendanceConfig>();

        //Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        int ua_type = Convert.ToInt32(HttpContext.Current.Session["usertype"].ToString());
        string college_ids = HttpContext.Current.Session["college_nos"].ToString();

        try
        {
            ds = objAttC.GetAttendanceConfigData(ua_type, college_ids, 2);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ConfigList = (from DataRow dr in ds.Tables[0].Rows
                              select new Attendance.AttendanceConfig
                              {
                                  SessionName = dr[0].ToString(),
                                  CollegeName = dr[1].ToString(),
                                  DegreeName = dr[2].ToString(),
                                  SemesterName = dr[3].ToString(),
                                  SchemetypeName = dr[4].ToString(),
                                  StartDateN = dr[5].ToString(),
                                  EndDateN = dr[6].ToString(),
                                  AttLockDays = dr[7].ToString(),
                                  SmsFacility_Str = dr[8].ToString(),
                                  EmailFacility_Str = dr[9].ToString(),
                                  TeachingPlan_Str = dr[10].ToString(),
                                  ActiveStatus_Str = dr[11].ToString(),

                              }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ds.Dispose();
        }
        return ConfigList;
    }

}