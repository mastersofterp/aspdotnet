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
    AcademinDashboardController objADEController = new AcademinDashboardController();


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
                this.BindListViewAttConfig();
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

    #region Core Attendance Configuration
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

        DataSet dsSession = objADEController.Get_College_Session(4, Session["college_nos"].ToString());
        if (dsSession.Tables[0].Rows.Count > 0)
        {
            ddlSessionAttConfig.DataSource = dsSession;
            ddlSessionAttConfig.DataValueField = dsSession.Tables[0].Columns[0].ToString();
            ddlSessionAttConfig.DataTextField = dsSession.Tables[0].Columns[1].ToString();
            ddlSessionAttConfig.DataBind();
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

    #endregion

    #region Global Attendance Configuration

    protected void ddlSessionAttConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionAttConfig.SelectedIndex > 0)
        {
            objCommon.FillListBox(lstSemesterAttConfig, "ACD_GLOBAL_OFFERED_COURSE AS s CROSS APPLY STRING_SPLIT(s.TO_SEMESTERNO, ',') AS f INNER JOIN ACD_SEMESTER AS c ON f.value = c.SEMESTERNO", "C.SEMESTERNO", "C.SEMESTERNAME", "S.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(ddlSessionAttConfig.SelectedValue) + ") AND ISNULL(S.GLOBAL_OFFERED,0) = 1 GROUP BY C.SEMESTERNO,C.SEMESTERNAME", "C.SEMESTERNO");
        }
        else
        {
            lstSemesterAttConfig.ClearSelection();
        }

    }

    protected void btnSunmitAttConfig_Click(object sender, EventArgs e)
    {
        try
        {
            int srno = 0;
            if (txtGlobalStartDate.Text != string.Empty && txtGlobalEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtGlobalEndDate.Text) <= Convert.ToDateTime(txtGlobalStartDate.Text))
                {
                    objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                    return;
                }
                else
                {
                    //string Semesternnos = "";
                    //foreach (ListItem items in lstSemesterAttConfig.Items)
                    //{
                    //    if (items.Selected == true)
                    //    {
                    //        Semesternnos += items.Value + ',';
                    //    }
                    //}
                    //if (Semesternnos.Length > 0)
                    //{
                    //    Semesternnos = Semesternnos.Remove(Semesternnos.Length - 1);
                    //}

                    objAttModel.AttendanceStartDate = Convert.ToDateTime(txtGlobalStartDate.Text);
                    objAttModel.AttendanceEndDate = Convert.ToDateTime(txtGlobalEndDate.Text);
                    objAttModel.AttendanceLockDay = Convert.ToInt32(txtGlobalAttLockDay.Text);
                    if (hfdSms.Value == "true")
                    {
                        objAttModel.SMSFacility = true;
                    }
                    else
                    {
                        objAttModel.SMSFacility = false;
                    }

                    if (hfdEmail.Value == "true")
                    {
                        objAttModel.EmailFacility = true;
                    }
                    else
                    {
                        objAttModel.EmailFacility = false;
                    }

                    if (hfdCourse.Value == "true")
                    {
                        objAttModel.CRegStatus = true;
                    }
                    else
                    {
                        objAttModel.CRegStatus = false;
                    }

                    if (hfdTeaching.Value == "true")
                    {
                        objAttModel.TeachingPlan = true;
                    }
                    else
                    {
                        objAttModel.TeachingPlan = false;
                    }

                    if (hfdActive.Value == "true")
                    {
                        objAttModel.ActiveStatus = true;
                    }
                    else
                    {
                        objAttModel.ActiveStatus = false;
                    }
                    //End
                    objAttModel.College_code = Session["colcode"].ToString();
                    objAttModel.Schemeno = Convert.ToInt32(ViewState["attschemeno"]);
                    objAttModel.Sessionno = Convert.ToInt32(ddlSessionAttConfig.SelectedValue);
                    if (ViewState["attaction"] != null && ViewState["attaction"].ToString().Equals("edit"))
                    {
                        ClearAttConfigControls();
                        objCommon.DisplayMessage(this, "Configuration Updated Successfully", this.Page);
                        //this.BindListView();
                        this.BindListViewAttConfig();
                    }
                    else
                    {

                        CustomStatus cs = (CustomStatus)objAttC.AddGlobalElectiveAttendanceConfigModified(objAttModel, Convert.ToInt32(Session["OrgId"]));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ClearAttConfigControls();
                            objCommon.DisplayMessage(this, "Configuration Added Successfully", this.Page);
                            this.BindListViewAttConfig();
                        }
                        else if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(this, "Configuration Already Exists !!", this.Page);
                            ClearAttConfigControls();
                            this.BindListViewAttConfig();
                        }
                        else if (cs.Equals(CustomStatus.TransactionFailed))
                        {
                            objCommon.DisplayMessage(this, "Transaction Failed", this.Page);
                            ClearAttConfigControls();
                            this.BindListViewAttConfig();
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.btnSunmitAttConfig_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelAttConfig_Click(object sender, EventArgs e)
    {
        ClearAttConfigControls();
    }

    private void ClearAttConfigControls()
    {
        lstSemesterAttConfig.ClearSelection();
        txtGlobalEndDate.Text = string.Empty;
        txtGlobalStartDate.Text = string.Empty;
        txtGlobalAttLockDay.Text = string.Empty;
        //txtAttLockHrs.Text = string.Empty;
        //rdoSMSNo.Checked = true;
        //rdoEmailNo.Checked = true;
        //rblCRegAfter.Checked = true;
        ViewState["attaction"] = null;
        ddlSessionAttConfig.SelectedIndex = 0;
    }
    private void BindListViewAttConfig()
    {
        try
        {
            DataSet ds = objAttC.GetAllAttendanceConfigGlobalElective(Convert.ToInt32(Session["OrgId"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvAttConfig.DataSource = ds;
                lvAttConfig.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAttConfig);//Set label -
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.BindListViewAttConfig -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditAttConfig_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int sessionid = int.Parse(btnEdit.CommandArgument);
            ViewState["attschemeno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";

            this.ShowAttConfigDetails(sessionid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.btnEditAttConfig_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowAttConfigDetails(int sessionid)
    {
        try
        {

            char delimiterChars = ',';
            char delimiter = ',';
            //SessionController objSS = new SessionController();
            SqlDataReader dr = objAttC.GetSingleConfigurationForGlobalElective(sessionid);
            if (dr != null)
            {
                if (dr.Read())
                {
                    //objCommon.FillListBox(lstSemesterAttConfig, "ACD_GLOBAL_OFFERED_COURSE AS s CROSS APPLY STRING_SPLIT(s.TO_SEMESTERNO, ',') AS f INNER JOIN ACD_SEMESTER AS c ON f.value = c.SEMESTERNO", "C.SEMESTERNO", "C.SEMESTERNAME", "S.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(sessionid) + ") AND ISNULL(S.GLOBAL_OFFERED,0) = 1 GROUP BY C.SEMESTERNO,C.SEMESTERNAME", "C.SEMESTERNO");

                    ////objCommon.FillListBox(lstSemesterAttConfig, "ACD_GLOBAL_OFFERED_COURSE AS s CROSS APPLY STRING_SPLIT(s.TO_SEMESTERNO, ',') AS f INNER JOIN ACD_SEMESTER AS c ON f.value = c.SEMESTERNO", "C.SEMESTERNO", "C.SEMESTERNAME", "s.SCHEMENO=" + Convert.ToInt32(schemeno) + " AND isnull(S.GLOBAL_OFFERED,0) = 1 GROUP BY C.SEMESTERNO,C.SEMESTERNAME", "C.SEMESTERNO");

                    //string semesternos = dr["SEMESTERNO"] == DBNull.Value ? "0" : dr["SEMESTERNO"].ToString();

                    //string[] sem = semesternos.Split(delimiterChars);

                    //for (int j = 0; j < sem.Length; j++)
                    //{
                    //    for (int i = 0; i < lstSemesterAttConfig.Items.Count; i++)
                    //    {
                    //        if (sem[j] == lstSemesterAttConfig.Items[i].Value)
                    //        {
                    //            lstSemesterAttConfig.Items[i].Selected = true;
                    //        }
                    //    }
                    //}

                    ddlSessionAttConfig.SelectedValue = dr["SESSIONID"] == DBNull.Value ? "0" : dr["SESSIONID"].ToString();

                    txtGlobalStartDate.Text = dr["START_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["START_DATE"].ToString()).ToString("dd/MM/yyyy");
                    txtGlobalEndDate.Text = dr["END_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["END_DATE"].ToString()).ToString("dd/MM/yyyy");
                    txtGlobalAttLockDay.Text = dr["LOCK_ATT_DAYS"] == null ? string.Empty : dr["LOCK_ATT_DAYS"].ToString();

                    //txtAttLockHrs.Text = dr["LOCK_ATT_HOURS"] == null ? string.Empty : dr["LOCK_ATT_HOURS"].ToString();
                    ViewState["sms"] = dr["SMS_FACILITY"].ToString();

                    if (dr["TEACHING_PLAN"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "SetStatTeaching(true);", true);
                        // ScriptManager.RegisterClientScriptBlock(updpnl, this.GetType(), "script7", "SetStatTeaching(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src8", "SetStatTeaching(false);", true);
                        // ScriptManager.RegisterClientScriptBlock(updpnl, this.GetType(), "script8", "SetStatTeaching(false);", true);
                    }

                    if (dr["ACTIVE"].ToString() == "Active")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.updpnl, GetType(), "script", "SetStatActive(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9", "SetStatActive(true);", true);
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.updpnl, GetType(), "script", "SetStatActive(false);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript10", "SetStatActive(false);", true);
                    }

                }
            }
            if (dr != null) dr.Close();

            ViewState["attconfigaction"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.ShowAttConfigDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void rdoConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rdoConfig.SelectedValue) == 1)
        {
            divCoreAtt.Visible = true;
            divGlobalAtt.Visible = false;
        }
        else 
        {
            divGlobalAtt.Visible = true;
            divCoreAtt.Visible = false;
        }
    }
    #endregion
}