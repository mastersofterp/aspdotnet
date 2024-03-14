using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class Activity_Exam_Activity : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    NotificationController objNF = new NotificationController();
    NotificationEntity NF = new NotificationEntity();
    NotificationComponent notificationComponent = new NotificationComponent();
    NotificationAndroid notificationAndroid = new NotificationAndroid();
    SessionActivityController activityController = new SessionActivityController();
    SessionActivity sessionActivity = new SessionActivity();
    AcademinDashboardController AcadDash = new AcademinDashboardController();

    List<SessionActivity> sessionActivityList = new List<SessionActivity>();

    private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string branches = string.Empty;
    string semester = string.Empty;
    string degrees = string.Empty;
    string UserTypes = string.Empty;
    string College_Ids = string.Empty;

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    this.CheckPageAuthorization();
                    Page.Title = Session["coll_name"].ToString();
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["sessionactivityno"] = "0";
                    ViewState["ipAddress"] = GetUserIPAddress();
                    ViewState["action"] = "add";
                    Session["OrgId"] = objCommon.LookUp("reff with (nolock)", "OrganizationId", string.Empty);
                    PopulateActivity();
                    MultipleCollegeBind();
                    objCommon.FillDropDownList(ddlExamPattern, "ACD_EXAM_PATTERN WITH (NOLOCK)", "PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0", "PATTERNNO");
                }
                PopulatedInstituteList();
                PopulateSemesterList();
                PopulatUserRightsList();
            }

            this.LoadDefinedSessionActivities();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
        }
        return User_IPAddress;
    }

    private DataTable GetDemandDraftDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("COLLEGENO", typeof(int)));
        dt.Columns.Add(new DataColumn("SCHEMENO", typeof(int)));
        dt.Columns.Add(new DataColumn("BRANCHNO", typeof(int)));
        dt.Columns.Add(new DataColumn("SEMESTERNO", typeof(int)));
        dt.Columns.Add(new DataColumn("UA_TYPE", typeof(int)));
        dt.Columns.Add(new DataColumn("TOUSERNO", typeof(int)));
        return dt;
    }

    private void PopulatedInstituteList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER WITH (NOLOCK) ", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID<>0", "COLLEGE_ID");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkInstituteList.DataTextField = "COLLEGE_NAME";
                    chkInstituteList.DataValueField = "COLLEGE_ID";
                    chkInstituteList.ToolTip = "COLLEGE_ID";
                    chkInstituteList.DataSource = ds.Tables[0];
                    chkInstituteList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateDegreeList()
    {
        try
        {

            string collegeId = "";
            if (ddlCollege.SelectedValue == "0" || ddlCollege.SelectedValue == "")
            {
                collegeId = Session["College_IDS"].ToString();
            }
            else
            {
                collegeId = ddlCollege.SelectedValue;
                if (collegeId.Contains("-"))
                {
                    collegeId = collegeId.Split('-')[0];
                }
            }

            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID=" + collegeId, "D.DEGREENO");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkDegreeList.DataTextField = "CODE";
                    chkDegreeList.DataValueField = "DEGREENO";
                    chkDegreeList.ToolTip = "DEGREENO";
                    chkDegreeList.DataSource = ds.Tables[0];
                    chkDegreeList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulatUserRightsList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("USER_RIGHTS WITH (NOLOCK)", "USERTYPEID", "USERDESC", "USERTYPEID<>0", "USERTYPEID");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkUserRightsList.DataTextField = "USERDESC";
                    chkUserRightsList.DataValueField = "USERTYPEID";
                    chkUserRightsList.ToolTip = "USERTYPEID";
                    chkUserRightsList.DataSource = ds.Tables[0];
                    chkUserRightsList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionActivityDefinition.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=SessionActivityDefinition.aspx");
        }
    }

    private void LoadDefinedSessionActivities()
    {
        int flg = 1;
        try
        {

            DataSet ds = activityController.GetDefinedSessionActivitiesFlag(flg);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvSessionActivities.DataSource = ds;
                lvSessionActivities.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Clear()
    {
        ddlExamPattern.SelectedIndex = 0;
        ddlExamPattern.Enabled = true;
        ddlExamNo.Enabled = true;
        ddlSubExamNo.Enabled = true;
        ddlCollege.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlActivity.Enabled = true;
        ddlActivity.SelectedIndex = 0;
        ddlCollege.Attributes.Remove("disabled");
        ddlCollege.ClearSelection();
        //ddlCollege.Items.Clear();
        chkDegreeList.Items.Clear();
        chkDegreeList.DataSource = null;
        chkDegreeList.DataBind();
        chkDegree.Checked = false;
        ViewState["sessionactivityno"] = "0";
        chkBranchList.Items.Clear();
        chkBranch.Checked = false;
        chkSemesterList.ClearSelection();
        chkSemester.Checked = false;
        chkUserRightsList.ClearSelection();
        chkUserRights.Checked = false;
        ViewState["action"] = "add";
        ddlSubExamNo.Items.Clear();
        ddlSubExamNo.Items.Add(new ListItem("Please Select", "0"));
        ddlExamNo.Items.Clear();
        ddlExamNo.Items.Add(new ListItem("Please Select", "0"));
        PopulateActivity();
        //MultipleCollegeBind();
        ViewState["EDIT_HIT"] = "0";
    }

    string regIds = "cPpIdXYCCOw:APA91bHHbHslRvJJv53XPmjE9lHP7DNYNxp8TX5qy_Cb8h2CbYbMbjNmTsupM_765x1W33gDPucf8pE_HCc-wRdL4mng2LEiiN51mUwVIC5sIpI_ntdm0asFzK5MxRd09LLPQToX8b1r";

    private void user_chk()
    {
        string UserTypes1 = string.Empty;
        for (int i = 0; i < chkUserRightsList.Items.Count; i++)
        {
            if (chkUserRightsList.Items[i].Selected)
            {
                UserTypes1 += chkUserRightsList.Items[i].Value + ",";
            }
        }

        if (!string.IsNullOrEmpty(UserTypes1))
            UserTypes1 = UserTypes1.Substring(0, UserTypes1.Length - 1);
    }

    private NotificationAndroid BindNotification_forAndroid(int activityno, string UserTypes)
    {

        try
        {
            sessionActivity = this.BindData();

            DataSet ds = activityController.GetFCM_ForAndroid_Details(UserTypes, activityno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string msg = ds.Tables[0].Rows[0][0].ToString();
                var charsToRemove = new string[] { "</br>", "<span style=", "color:#3c8dbc; font-weight:bold;", "</span>", ">", "\"" };
                foreach (var c in charsToRemove)
                {
                    msg = msg.Replace(c, string.Empty);
                }
                notificationAndroid.Message = msg;
                notificationAndroid.MessageTitle = ds.Tables[0].Rows[0][1].ToString();
                notificationAndroid.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[0][2]).ToString();
                notificationAndroid.Action = "0";
                notificationAndroid.ImageUrl = null;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return notificationAndroid;
    }

    private List<NotificationEntity> GetFCM_forAndroid()
    {
        List<NotificationEntity> notification = new List<NotificationEntity>();
        try
        {
            int time = 24000;
            user_chk();
            DataSet regIds = activityController.GetFCMRegID(UserTypes, degrees, branches, semester);
            if (regIds != null && regIds.Tables.Count > 0)
            {
                notification = (from DataRow dr in regIds.Tables[0].Rows
                                select new NotificationEntity
                                {
                                    RegID = dr["fcm_regid"].ToString()
                                }).ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return notification;
    }

    private List<SessionActivity> BindDataMultiple()
    {
        try
        {
            string sessionnos = string.Empty;
            foreach (ListItem Sessionitem in ddlCollege.Items)
            {
                SessionActivity sessionActivity = new SessionActivity();
                if (Sessionitem.Selected == true)
                {
                    sessionnos += Sessionitem.Value.Split('-')[1] + ',';
                    sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());
                    sessionActivity.SessionNo = Convert.ToInt32((Sessionitem.Value).Split('-')[1]);

                    string activitynos = string.Empty;
                    string activitynames = string.Empty;
                    activitynos = ddlActivity.SelectedValue;
                    activitynames = ddlActivity.SelectedItem.Text;
                    sessionActivity.ActivityNos = activitynos;
                    sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
                    sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
                    if (hfdActive.Value == "true")
                    {
                        sessionActivity.IsStarted = true;
                    }
                    else
                    {
                        sessionActivity.IsStarted = false;
                    }

                    if (hfdShowStatus.Value == "true")
                    {
                        sessionActivity.ShowStatus = 1;
                    }
                    else
                    {
                        sessionActivity.ShowStatus = 0;
                    }
                    sessionActivity.Session_Name = Sessionitem.Text;
                    sessionActivity.Activity_Name = activitynames;
                }
                sessionActivityList.Add(sessionActivity);
            }
            if (sessionnos.Length > 1)
            {
                sessionnos = sessionnos.Remove(sessionnos.Length - 1);
            }
            if (sessionnos.Length > 0)
            {

            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return sessionActivityList;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            branches = string.Empty; semester = string.Empty; degrees = string.Empty; UserTypes = string.Empty;
            int flg = 1;
            DataSet ds = new DataSet();
            if (getChk() == false)
            {
                return;
            }
            if (hfdActive.Value == "true")
            {
                if (DateTime.Parse(txtEndDate.Text) < DateTime.Today)
                {
                    objCommon.DisplayUserMessage(this.updSesActivity, "Active activities end date is equal or greater than Today", this.Page);
                    txtEndDate.Focus();
                    return;
                }
            }
            ds = (DataSet)ViewState["CollegeSession"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                MultipleCollegeBind();
                string College_Ids = ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                if (!College_Ids.Contains("-"))
                {
                    College_Ids = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString() + "-" + ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                }
                for (int j = 0; j < College_Ids.ToString().Length; j++)
                {
                    for (int i = 0; i < ddlCollege.Items.Count; i++)
                    {
                        if (College_Ids.ToString() == ddlCollege.Items[i].Value)
                        {
                            ddlCollege.Items[i].Selected = true;
                        }
                    }
                }
            }
            sessionActivityList = this.BindDataMultiple();
            int time = 24000;
            string Notification_Template = objCommon.LookUp("ACTIVITY_MASTER WITH (NOLOCK)", "ACTIVITYTEMPLATE", "ACTIVITY_NO = " + sessionActivity.ActivityNo);
            int userno = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = CustomStatus.Others;
            CustomStatus css = CustomStatus.Others;
            string CollegeIds = GetSelectedCollegeIds();
            string existingCollgeName = string.Empty;
            ViewState["dsSessionData"] = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_CODE,IS_ACTIVE,ORGANIZATIONID,COLLEGE_ID", "OrganizationId = " + Session["OrgId"].ToString(), "");
            ViewState["dsClgDegBrn"] = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH", "CDBNO", "COLLEGE_ID,DEGREENO,BRANCHNO,OrganizationId", "OrganizationId = " + Session["OrgId"].ToString(), "");
            if (ViewState["sessionactivityno"].ToString() != string.Empty && ViewState["sessionactivityno"].ToString() == "0")
            {
                string branchesO = branches.TrimEnd(','), Exists = ""; string semesterO = semester.TrimEnd(','); string degreesO = degrees.TrimEnd(','); string UserTypesO = UserTypes.TrimEnd(',');
                int isSaved = 0;
                foreach (SessionActivity sessionActivityItem in sessionActivityList)
                {
                    foreach (string clg in CollegeIds.Split(','))
                    {
                        int CollegeId = Convert.ToInt32(clg);
                        Boolean IsvalidForInsert = PreparingForInsertRecord(sessionActivityItem, ref branches, ref semester, ref degrees, ref UserTypes, CollegeId);
                        if (IsvalidForInsert)
                        {
                            DataSet ds1 = activityController.AddSessionActivity(sessionActivityItem, branches.TrimEnd(','), semester.TrimEnd(','), degrees.TrimEnd(','), UserTypes.TrimEnd(','), CollegeId, flg);

                            if (ds1.Tables.Count > 0)
                            {
                                for (int i = 0; i < ds1.Tables.Count; i++)
                                {
                                    if (ContainColumn("STATUS", ds1.Tables[i]))
                                    {
                                        if (ds1.Tables[i] != null && ds1.Tables[i].Rows.Count > 0)
                                        {
                                            for (int j = 0; j < ds1.Tables[i].Rows.Count; j++)
                                            {
                                                if (ds1.Tables[i].Rows[j]["STATUS"].ToString().Equals("2"))
                                                {
                                                    isSaved++;
                                                }
                                            }
                                        }
                                    }
                                    if (ContainColumn("COLLEGE_NAME", ds1.Tables[i]))
                                    {
                                        if (ds1.Tables[i] != null && ds1.Tables[i].Rows.Count > 0)
                                        {
                                            for (int j = 0; j < ds1.Tables[i].Rows.Count; j++)
                                            {
                                                existingCollgeName += ds1.Tables[i].Rows[j]["COLLEGE_NAME"].ToString() + ",";
                                            }
                                        }
                                    }
                                }
                            }
                            css = (CustomStatus)activityController.AddSessionActivity_FOR_NOTIFICATION(sessionActivityItem, branches, semester, degrees, UserTypes, Notification_Template, userno, CollegeId);
                        }
                    }
                }

                if (existingCollgeName.Length > 1)
                {
                    existingCollgeName = existingCollgeName.Substring(0, existingCollgeName.Length - 1);
                    objCommon.DisplayUserMessage(this.updSesActivity, "Selected Activity Already Exist:" + existingCollgeName, this.Page);
                    return;
                }

                if (isSaved > 0)
                {
                    objCommon.DisplayUserMessage(this.updSesActivity, "Exam Activity Definition Saved Successfully!", this.Page);
                    LoadDefinedSessionActivities();
                    Clear();
                    return;
                }

                branches = branchesO; semester = semesterO; degrees = degreesO; UserTypes = UserTypesO;
                this.LoadDefinedSessionActivities();
            }
            else if (int.Parse(ViewState["sessionactivityno"].ToString()) > 0)
            {

                string ii = ViewState["sessionactivityno"].ToString();
                int CollegeId = 0;
                if (Session["College_IDS"] != null)
                {
                    CollegeId = (int)Session["College_IDS"];
                }
                for (int i = 0; i < sessionActivityList.Count; i++)
                {
                    if (sessionActivityList[i].SessionActivityNo == Convert.ToInt32(ii))
                    {
                        sessionActivity = sessionActivityList[i];
                    }
                }
                cs = (CustomStatus)activityController.UpdateSessionActivity(sessionActivity, branches.TrimEnd(','), semester.TrimEnd(','), degrees.TrimEnd(','), UserTypes.TrimEnd(','), CollegeId, flg);
                this.LoadDefinedSessionActivities();
            }
            List<NotificationEntity> RegID = this.GetFCM_forAndroid();
            notificationAndroid = this.BindNotification_forAndroid(sessionActivity.ActivityNo, UserTypes);
            bool status = notificationComponent.SendToFCMServer(RegID, notificationAndroid, time);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Exam Activity Definition Saved Successfully!", this.Page);
                Clear();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Record Already Exists!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Exam Activity Definition Updated Successfully!", this.Page);
                Clear();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Error!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private bool ContainColumn(string columnName, DataTable table)
    {
        DataColumnCollection columns = table.Columns;
        if (columns.Contains(columnName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean getChk()
    {
        for (int i = 0; i < chkBranchList.Items.Count; i++)
        {
            if (chkBranchList.Items[i].Selected)
            {
                branches += chkBranchList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkSemesterList.Items.Count; i++)
        {
            if (chkSemesterList.Items[i].Selected)
            {
                semester += chkSemesterList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkDegreeList.Items.Count; i++)
        {
            if (chkDegreeList.Items[i].Selected)
            {
                degrees += chkDegreeList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkUserRightsList.Items.Count; i++)
        {
            if (chkUserRightsList.Items[i].Selected)
            {
                UserTypes += chkUserRightsList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkInstituteList.Items.Count; i++)
        {
            if (chkInstituteList.Items[i].Selected)
            {
                College_Ids += chkInstituteList.Items[i].Value + ",";
            }
        }

        if (string.IsNullOrEmpty(degrees))
        {
            objCommon.DisplayMessage(this.Page, "Please Select Degree", Page);
            return false;
        }

        else if (string.IsNullOrEmpty(branches))
        {
            objCommon.DisplayMessage(this.Page, "Please Select Branch", Page);
            return false;
        }
        else
        {
            return true;
        }

        if (!string.IsNullOrEmpty(branches))
            branches = branches.Substring(0, branches.Length - 1);
        if (!string.IsNullOrEmpty(semester))
            semester = semester.Substring(0, semester.Length - 1);
        if (!string.IsNullOrEmpty(degrees))
            degrees = degrees.Substring(0, degrees.Length - 1);
        if (!string.IsNullOrEmpty(UserTypes))
            UserTypes = UserTypes.Substring(0, UserTypes.Length - 1);
        if (!string.IsNullOrEmpty(College_Ids))
            College_Ids = College_Ids.Substring(0, College_Ids.Length - 1);
        return true;
    }

    #region
    private SessionActivity BindData()
    {
        try
        {
            sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());
            foreach (ListItem Sessionitem in ddlCollege.Items)
            {
                if (Sessionitem.Selected == true)
                {
                    sessionActivity.SessionNo = Convert.ToInt32((Sessionitem.Value).Split('-')[1]);
                }
            }


            string activitynos = string.Empty;
            string activitynames = string.Empty;
            foreach (ListItem items in ddlActivity.Items)
            {
                if (items.Selected == true)
                {
                    activitynos += items.Value + ',';
                    activitynames += items.Text + ',';
                }
            }
            if (activitynos.Length > 1)
            {
                activitynos = activitynos.Remove(activitynos.Length - 1);
            }
            if (activitynames.Length > 1)
            {
                activitynames = activitynames.Remove(activitynames.Length - 1);
            }
            sessionActivity.ActivityNos = activitynos;
            sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
            sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
            if (hfdActive.Value == "true")
            {
                sessionActivity.IsStarted = true;
            }
            else
            {
                sessionActivity.IsStarted = false;
            }

            if (hfdShowStatus.Value == "true")
            {
                sessionActivity.ShowStatus = 1;
            }
            else
            {
                sessionActivity.ShowStatus = 0;
            }
            sessionActivity.Activity_Name = activitynames;
        }
        catch (Exception ex)
        {
            throw;
        }
        return sessionActivity;
    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void PopulateBranchList()
    {
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkDegreeList.Items)
            {

                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }

            if (count > 0)
            {
                string degNos = values.TrimEnd(',');
                DataSet ds = BindBranchWithMultipleCollege(degNos);
                int organizatiionid = Convert.ToInt32(Session["OrgId"]);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkBranchList.Visible = true;
                        if (organizatiionid == 2)
                        {
                            chkBranchList.DataTextField = "CODE";
                        }
                        else
                        {
                            chkBranchList.DataTextField = "SHORTNAME";
                        }
                        chkBranchList.DataValueField = "BRANCHNO";
                        chkBranchList.ToolTip = "DURATION";
                        chkBranchList.DataSource = ds.Tables[0];
                        chkBranchList.DataBind();
                    }

                }
                else
                {
                    chkBranchList.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateSemesterList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <>0", "SEMESTERNO");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkSemesterList.DataTextField = "SEMESTERNAME";
                    chkSemesterList.DataValueField = "SEMESTERNO";
                    chkSemesterList.DataSource = ds.Tables[0];
                    chkSemesterList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void chkInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateDegreeList();
    }

    protected void chkDegreeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkBranchList.Items.Clear();
        chkSemesterList.Items.Clear();
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkDegreeList.Items)
            {

                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }
            ViewState["Degnos"] = values;
            if (count > 0)
            {
                PopulateBranchList();
                PopulateSemesterList();
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateExam();
    }

    protected void ddlExamNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateSubExam();
    }

    private void PopulateActivity()
    {

        try
        {
            ddlActivity.Enabled = true;
            objCommon.FillDropDownList(ddlActivity, "ACTIVITY_MASTER A LEFT JOIN ACD_SUBEXAM_NAME B ON(A.SUBEXAMNO=B.SUBEXAMNO) LEFT JOIN ACD_EXAM_PATTERN C ON (C.PATTERNNO=B.PATTERNNO) LEFT JOIN ACD_EXAM_NAME D ON (A.EXAMNO=D.EXAMNO) LEFT JOIN ACD_EXAM_NAME E ON (A.EXAMNO=E.EXAMNO)", "DISTINCT A.ACTIVITY_NO", "CONCAT(ACTIVITY_NAME,' - ',C.PATTERN_NAME ,' - ',B.SUBEXAMNAME) ACTIVITY_NAME", "(A.EXAMNO=" + Convert.ToInt32(ddlExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlExamNo.SelectedValue) + "=0) AND (A.SUBEXAMNO = " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + "=0) AND (D.PATTERNNO = " + Convert.ToInt32(ddlExamPattern.SelectedValue) + " OR " + Convert.ToInt32(ddlExamPattern.SelectedValue) + "=0) AND ISNULL(ASSIGN_TO,0)=1 AND ISNULL(A.ACTIVESTATUS,0)=1", "A.ACTIVITY_NO");
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateSubExam()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubExamNo, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "EXAMNO = " + Convert.ToInt32(ddlExamNo.SelectedValue) + " AND SUBEXAMNAME <> ''", "SUBEXAMNO");
            ddlSubExamNo.Focus();

            PopulateActivity();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateExam()
    {
        try
        {
            if (ddlExamPattern.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlExamNo, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "PATTERNNO=" + ddlExamPattern.SelectedValue + "AND EXAMNAME <> ''", "EXAMNO");
                ddlExamNo.Focus();
                ViewState["ExamPattern"] = Convert.ToInt32(ddlExamPattern.SelectedValue);

                PopulateActivity();
            }
            else
            {
                ddlSubExamNo.Items.Clear();
                ddlSubExamNo.Items.Add(new ListItem("Please Select", "0"));
                ddlExamNo.Items.Clear();
                ddlExamNo.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSubExamNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopulateActivity();
            ddlActivity.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    private void MultipleCollegeBind()
    {
        try
        {
            DataSet ds = null;
            ds = AcadDash.Get_CollegeID_Sessionno(1, Session["college_nos"].ToString());

            ddlCollege.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.DataSource = ds;
                ddlCollege.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlCollege.DataTextField = ds.Tables[0].Columns[4].ToString();
                ddlCollege.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["EDIT_HIT"]) == "1")
        {

        }
        else
        {
            chkBranchList.Items.Clear();
            chkSemesterList.Items.Clear();
            chkDegreeList.Items.Clear();
            string collegenose = string.Empty;
            string collegenos = string.Empty;
            foreach (ListItem items in ddlCollege.Items)
            {
                if (items.Selected == true)
                {
                    collegenos += (items.Value).Split('-')[0] + ',';
                }
            }
            if (collegenos.Length > 1)
            {
                collegenos = collegenos.Remove(collegenos.Length - 1);
                BindDegreeWithMultipleCollege(collegenos);
            }

            if (collegenos.Length >= 0)
            {
                BindDegreeWithMultipleCollege(collegenos);
            }
        }
    }

    private void BindDegreeWithMultipleCollege(string CollegeIds)
    {
        DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "DISTINCT CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "CD.DEGREENO");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkDegreeList.DataTextField = "CODE";
                chkDegreeList.DataValueField = "DEGREENO";
                chkDegreeList.ToolTip = "DEGREENO";
                chkDegreeList.DataSource = ds.Tables[0];
                chkDegreeList.DataBind();
            }
        }
    }

    private DataSet BindBranchWithMultipleCollege(string degNos)
    {
        string CollegeIds = GetSelectedCollegeIds();

        DataSet ds = new DataSet();
        int OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (OrganizationId == 2)
        {
            ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "DISTINCT B.BRANCHNO", "CD.CODE", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "CODE");
        }
        else
        {
            ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "DISTINCT B.BRANCHNO", "B.SHORTNAME +' ('+D.CODE+')' SHORTNAME", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "SHORTNAME");
        }

        return ds;
    }

    private Boolean CheckSessionSelected()
    {
        Boolean IsSessionSelected = false;

        string SessionNos = string.Empty;
        foreach (ListItem items in ddlCollege.Items)
        {
            if (items.Selected == true)
            {
                SessionNos += (items.Value).Split('-')[1] + ',';
            }
        }
        if (SessionNos.Length < 1)
        {
            IsSessionSelected = false;
        }
        else if (SessionNos.Length > 0)
        {
            IsSessionSelected = true;
        }
        return IsSessionSelected;
    }


    private string GetSelectedCollegeIds()
    {
        string CollegeIds = string.Empty;
        foreach (ListItem items in ddlCollege.Items)
        {
            if (items.Selected == true)
            {
                CollegeIds += (items.Value).Split('-')[0] + ',';
            }
        }
        if (CollegeIds.Length > 1)
        {
            CollegeIds = CollegeIds.Remove(CollegeIds.Length - 1);
        }
        return CollegeIds;
    }

    private Boolean PreparingForInsertRecord(SessionActivity sessionActiity, ref string branch, ref string semester, ref string degreeno, ref string UserTypes, int College_Ids)
    {
        Boolean IsValid = false;
        DataSet ds = new DataSet();
        DataSet dsCDB = new DataSet();
        DataRow[] drSessionforClg;
        DataRow[] drCDB;
        if (ViewState["dsSessionData"] != null)
        {
            ds = (DataSet)ViewState["dsSessionData"];
        }
        if (ViewState["dsClgDegBrn"] != null)
        {
            dsCDB = (DataSet)ViewState["dsClgDegBrn"];
        }

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                drSessionforClg = ds.Tables[0].Select("OrganizationId = " + Session["OrgId"].ToString() + " and COLLEGE_ID = " + College_Ids + " AND SESSIONNO = " + sessionActiity.SessionNo);
                if (drSessionforClg.Length > 0)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                    return IsValid;
                }
            }
        }
        degrees = "";
        for (int i = 0; i < chkDegreeList.Items.Count; i++)
        {
            if (chkDegreeList.Items[i].Selected)
            {
                drCDB = dsCDB.Tables[0].Select("OrganizationId = " + Session["OrgId"].ToString() + " and COLLEGE_ID = " + College_Ids + " AND DEGREENO = " + chkDegreeList.Items[i].Value);
                if (drCDB.Length > 0)
                {
                    degrees += chkDegreeList.Items[i].Value + ",";
                }
            }
        }
        branches = "";
        string degreess = degrees.TrimEnd(',');
        if (string.IsNullOrEmpty(degreess)) { degreess = "0"; }
        for (int i = 0; i < chkBranchList.Items.Count; i++)
        {
            if (chkBranchList.Items[i].Selected)
            {
                drCDB = dsCDB.Tables[0].Select("OrganizationId = " + Session["OrgId"].ToString() + " and COLLEGE_ID = " + College_Ids + " AND DEGREENO IN (" + degreess.TrimEnd(',') + ") AND BRANCHNO = " + chkBranchList.Items[i].Value);
                if (drCDB.Length > 0)
                {
                    branches += chkBranchList.Items[i].Value + ",";
                }
            }
        }

        if (!string.IsNullOrEmpty(branches))
        {
            branches = branches.Substring(0, branches.Length - 1);
        }
        if (!string.IsNullOrEmpty(degrees))
        {
            degrees = degrees.Substring(0, degrees.Length - 1);
        }

        return IsValid;
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;
            int recordId = int.Parse(btnEditRecord.CommandArgument);
            DataSet ds = activityController.GetDefinedSessionActivities(recordId);
            ViewState["CollegeSession"] = ds;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                MultipleCollegeBind();
                string College_Ids = ds.Tables[0].Rows[0]["SESSION_NO"].ToString();

                if (!College_Ids.Contains("-"))
                {
                    College_Ids = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString() + "-" + ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                }
                for (int j = 0; j < College_Ids.ToString().Length; j++)
                {
                    for (int i = 0; i < ddlCollege.Items.Count; i++)
                    {
                        if (College_Ids.ToString() == ddlCollege.Items[i].Value)
                        {
                            ddlCollege.Items[i].Selected = true;
                        }
                    }
                }

                string collegenos = College_Ids.Split('-')[0];

                if (collegenos.Length > 0)
                {
                    BindDegreeWithMultipleCollege(collegenos);
                }


                Session["College_IDS"] = Convert.ToInt32(collegenos);

                ddlExamPattern.SelectedValue = dr["PATTERNNO"].ToString();
                ddlExamPattern_SelectedIndexChanged(sender, e);
                ddlExamNo.SelectedValue = dr["EXAMNO"].ToString();
                ddlExamNo_SelectedIndexChanged(sender, e);

                string ss = string.IsNullOrEmpty(dr["SUBEXAMNO"].ToString()) ? "" : dr["SUBEXAMNO"].ToString();
                if (!string.IsNullOrEmpty(ss))
                {
                    if (ddlSubExamNo.Items.Count == 1)
                    {
                        ddlSubExamNo.SelectedValue = "0";
                    }
                    else
                    {
                        ddlSubExamNo.SelectedValue = string.IsNullOrEmpty(dr["SUBEXAMNO"].ToString()) ? "0" : dr["SUBEXAMNO"].ToString();

                        ddlSubExamNo.SelectedValue = dr["SUBEXAMNO"].ToString();
                    }
                }
                txtStartDate.Text = ((dr["START_DATE"].ToString() != string.Empty) ? ((DateTime)dr["START_DATE"]).ToShortDateString() : string.Empty);
                txtEndDate.Text = ((dr["END_DATE"].ToString() != string.Empty) ? ((DateTime)dr["END_DATE"]).ToShortDateString() : string.Empty);
                if (!string.IsNullOrEmpty(dr["ACTIVITY_NO"].ToString()))
                {
                    ddlActivity.SelectedValue = dr["ACTIVITY_NO"].ToString();
                }

                char delimiterChars = ',';

                int count = 0;
                int vcount = 0;
                PopulateDegreeList();

                degrees = dr["DEGREENO"].ToString();
                string[] deg = degrees.Split(delimiterChars);

                count = 0;
                vcount = 0;

                for (int j = 0; j < deg.Length; j++)
                {
                    for (int i = 0; i < chkDegreeList.Items.Count; i++)
                    {
                        if (deg[j] == chkDegreeList.Items[i].Value)
                        {
                            chkDegreeList.Items[i].Selected = true;
                        }
                    }
                }
                foreach (ListItem Item in chkDegreeList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }
                if (count == vcount)
                {
                    chkDegree.Checked = true;
                }

                PopulateBranchList();
                PopulateSemesterList();
                PopulatUserRightsList();


                branches = dr["BRANCH"].ToString();
                string[] branch = branches.Split(delimiterChars);
                count = 0;
                vcount = 0;
                for (int j = 0; j < branch.Length; j++)
                {
                    for (int i = 0; i < chkBranchList.Items.Count; i++)
                    {
                        if (branch[j] == chkBranchList.Items[i].Value)
                        {
                            chkBranchList.Items[i].Selected = true;
                        }
                    }
                }

                foreach (ListItem Item in chkBranchList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }

                if (count == vcount)
                {
                    chkBranch.Checked = true;
                }

                semester = dr["SEMESTER"].ToString();
                string[] sem = semester.Split(delimiterChars);
                count = 0;
                vcount = 0;
                for (int j = 0; j < sem.Length; j++)
                {
                    for (int i = 0; i < chkSemesterList.Items.Count; i++)
                    {
                        if (sem[j] == chkSemesterList.Items[i].Value)
                        {
                            chkSemesterList.Items[i].Selected = true;
                        }
                    }
                }
                foreach (ListItem Item in chkSemesterList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }
                if (count == vcount)
                {
                    chkSemester.Checked = true;
                }

                UserTypes = dr["UserType"].ToString();
                string[] utype = UserTypes.Split(delimiterChars);
                count = 0;
                vcount = 0;
                for (int j = 0; j < utype.Length; j++)
                {
                    for (int i = 0; i < chkUserRightsList.Items.Count; i++)
                    {
                        if (utype[j] == chkUserRightsList.Items[i].Value)
                        {
                            chkUserRightsList.Items[i].Selected = true;
                        }
                    }
                }
                foreach (ListItem Item in chkUserRightsList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }
                if (count == vcount)
                {
                    chkUserRights.Checked = true;
                }
                if (dr["STARTED"].ToString() == "Started")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(false);", true);
                }

                if (dr["SHOW_STATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
                }

                ViewState["sessionactivityno"] = dr["SESSION_ACTIVITY_NO"].ToString();
            }
            ddlCollege.Attributes.Add("disabled", "disabled");
            ddlActivity.Enabled = false;
            ddlExamPattern.Enabled = false;
            ddlExamNo.Enabled = false;
            ddlSubExamNo.Enabled = false;

            ViewState["action"] = "edit";
            ViewState["EDIT_HIT"] = "1";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}