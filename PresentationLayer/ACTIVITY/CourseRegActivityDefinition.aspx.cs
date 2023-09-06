using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.IO;

public partial class ACTIVITY_CourseRegActivityDefinition : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    NotificationController objNF = new NotificationController();
    SessionActivityController activityController = new SessionActivityController();
    SessionActivity sessionActivity = new SessionActivity();
    AcademinDashboardController AcadDash = new AcademinDashboardController(); // add by maithili [07-09-2022]
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["sessionactivityno"] = "0";
                    ViewState["ipAddress"] = GetUserIPAddress();
                    // PopulateActivity();

                    //modified by nehal on 22/03/2023

                    //MultipleCollegeBind();
                    SessionBind();
                    ViewState["edit"] = "add";

                    objCommon.FillDropDownList(ddlActivityName, "ACD_COURSE_ACTIVITY_TYPE_MASTER WITH (NOLOCK)", "CRS_ACTIVITY_NO", "CRS_ACTIVITY_NAME", "ISNULL(ISACTIVE,0)=1", "CRS_ACTIVITY_NO");
                    objCommon.FillDropDownList(ddlStudentIDType, "ACD_IDTYPE WITH (NOLOCK)", "IDTYPENO", "IDTYPEDESCRIPTION", "ISNULL(ACTIVESTATUS,0)=1", "IDTYPENO");               
                }
                //PopulatedInstituteList();               
                PopulateSemesterList();
                PopulatUserRightsList();
               // txtEligiblityForCrsReg.Visible = false;
                //ScriptManager.RegisterStartupScript(this, GetType(), "EligibilityCrsReg", "ShowEligibleForCrsRegistration();", true);
            }

            this.LoadDefinedSessionActivities();
            ddlCollege.Focus();
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
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
        }
        return User_IPAddress;
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkBranchList.Items.Clear();
        chkSemesterList.Items.Clear();
        chkDegreeList.Items.Clear();
        if (ddlCollege.SelectedIndex > 0)
        {
            ddlSession.Focus();
            PopulateDegreeList();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
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

    private void PopulateDegreeList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");

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
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionActivityDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionActivityDefinition.aspx");
        }
    }

    private void LoadDefinedSessionActivities()
    {
        try
        {
            //ViewState["DS_ACD_COURSE_REG_CONFIG_ACTIVITY"] = objCommon.FillDropDown("ACD_COURSE_REG_CONFIG_ACTIVITY WITH(NOLOCK)", "*", "", "", "");                  
            DataSet ds = activityController.GetCourseRegConfigActivityData();
            ViewState["DS_ACD_COURSE_REG_CONFIG_ACTIVITY"] = ds;
            if (ds != null && ds.Tables.Count > 0)
            {
                lvSessionActivities.DataSource = ds;
                lvSessionActivities.DataBind();
            }

            //if (Convert.ToInt16(ddlCoursePattern.SelectedValue) < 2)
            //{
            //    dvChoiseFor.Visible = false;
            //    txtChoiseFor.Text = string.Empty;
            //}
            string CoursePattern = string.Empty;
            for (int i = 0; i < ckhCoursePatternList.Items.Count; i++)
            {
                if (ckhCoursePatternList.Items[i].Selected)
                    CoursePattern += ckhCoursePatternList.Items[i].Value + ",";
            }

            CoursePattern = CoursePattern.TrimEnd(',');
            if (CoursePattern.Contains("2") || CoursePattern.Contains('3'))
            {
                dvChoiseFor.Visible = true;
            }
            else
                dvChoiseFor.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Clear()
    {
        ckhCoursePatternList.ClearSelection();
        ckhCoursePattern.Checked = false;
        // ddlCoursePattern.SelectedIndex = 0;       
        ddlCollege.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlSessionCollege.SelectedIndex = 0;
        //ddlSession.SelectedIndex = 0;
        MultipleCollegeBind();
        ddlCollege.ClearSelection();
        ddlSession.Items.Clear();
        chkDegreeList.Items.Clear();
        chkDegree.Checked = false;
        ViewState["sessionactivityno"] = "0";
        chkBranchList.Items.Clear();
        chkBranch.Checked = false;
        chkSemesterList.ClearSelection();
        chkSemester.Checked = false;
        chkUserRightsList.ClearSelection();
        chkUserRights.Checked = false;
        txtTo.Text = string.Empty;
        txtfrom.Text = string.Empty;
        ddlStudentIDType.SelectedIndex = 0;
    }

    // string regIds = "cPpIdXYCCOw:APA91bHHbHslRvJJv53XPmjE9lHP7DNYNxp8TX5qy_Cb8h2CbYbMbjNmTsupM_765x1W33gDPucf8pE_HCc-wRdL4mng2LEiiN51mUwVIC5sIpI_ntdm0asFzK5MxRd09LLPQToX8b1r";

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string UserRights = string.Empty;
            string Sessionnos = string.Empty;
            string Degreenos = string.Empty;
            string Branchnos = string.Empty;
            string Semesternos = string.Empty;
            string CollegeIds = string.Empty;

            Sessionnos = "";
            CollegeIds = GetSelectedCollegeIds();

            //for (int i = 0; i < chkDegreeList.Items.Count; i++)
            //{
            //    if (chkDegreeList.Items[i].Selected)
            //        Degreenos += chkDegreeList.Items[i].Value + ",";
            //}

            //for (int i = 0; i < chkBranchList.Items.Count; i++)
            //{
            //    if (chkBranchList.Items[i].Selected)
            //        Branchnos += chkBranchList.Items[i].Value + ",";
            //}

            //for (int i = 0; i < chkSemesterList.Items.Count; i++)
            //{
            //    if (chkSemesterList.Items[i].Selected)
            //        Semesternos += chkSemesterList.Items[i].Value + ",";
            //}

            Degreenos = GetSelectedDegreeIds();
            Branchnos = GetSelectedBranchIds();
            Semesternos = GetSelectedSemesterIds();

            if (CollegeIds == string.Empty || CollegeIds == "" || CollegeIds == null)
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Atleast one College!", this.Page);
                return;
            }
            if (Degreenos == string.Empty || Degreenos == "" || Degreenos == null)
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Atleast one Degree!", this.Page);
                return;
            }
            if (Branchnos == string.Empty || Branchnos == "" || Branchnos == null)
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Atleast one Branch!", this.Page);
                return;
            }
            if (Semesternos == string.Empty || Semesternos == "" || Semesternos == null)
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Atleast one Semester!", this.Page);
                return;
            }

            //if (!CheckSessionSelected())
            //{
            //    objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Session!", this.Page);
            //    return;
            //}
            int userno = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = CustomStatus.Others;
            //sessionActivityList = new List<SessionActivity>();
            //sessionActivityList = this.BindDataMultiple();

            SessionActivity sessionActivity = new SessionActivity();
            sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
            sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
            sessionActivity.IsStarted = hfdActive.Value == "true" ? true : false;
            sessionActivity.ShowStatus = hfdShowStatus.Value == "true" ? 1 : 0;

            //getChk();
            DataSet ds = (DataSet)ViewState["DS_ACD_COURSE_REG_CONFIG_ACTIVITY"];

            foreach (ListItem liUserRights in chkUserRightsList.Items)
            {
                if (liUserRights.Selected)
                    UserRights += Convert.ToString(Convert.ToInt32(liUserRights.Value) + ",");
            }
            UserRights = UserRights.TrimEnd(',');
            if (UserRights == string.Empty || UserRights == "" || UserRights == null)
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Atleast one User Rights!", this.Page);
                return;
            }
            bool PAYMENT_APPLICABLE_FOR_SEM_WISE = hfdPaymentApplicableForSemWise.Value == "true" ? true : false;
            int choiceFor = 0;

            string CoursePattern = string.Empty;
            for (int i = 0; i < ckhCoursePatternList.Items.Count; i++)
            {
                if (ckhCoursePatternList.Items[i].Selected)
                    CoursePattern += ckhCoursePatternList.Items[i].Value + ",";
            }
            CoursePattern = CoursePattern.TrimEnd(',');
            //if (CoursePattern == string.Empty || CoursePattern == "" || CoursePattern == null)
            //{
            //    objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Atleast one Course Pattern!", this.Page);
            //    return;
            //}
            if (CoursePattern.Contains("2") || CoursePattern.Contains('3'))
                choiceFor = string.IsNullOrEmpty(txtChoiseFor.Text) ? 0 : Convert.ToInt32(txtChoiseFor.Text);

            string StartTime = txtfrom.Text;
            string EndTime = txtTo.Text;

            //if (StartTime == "" || StartTime == string.Empty || StartTime == null)
            //{
            //    objCommon.DisplayUserMessage(this.updSesActivity, "Please Enter Start Time!", this.Page);
            //    return;
            //}
            //if (EndTime == "" || EndTime == string.Empty || EndTime == null)
            //{
            //    objCommon.DisplayUserMessage(this.updSesActivity, "Please Enter End Time!", this.Page);
            //    return;
            //}


            //add by nehal on 22/03/2023
            int sessionid = Convert.ToInt32(ddlSessionCollege.SelectedValue);
            int eligibilityForCrsReg = hfdEligibilityForCrsReg.Value == "true" ? 1 : 0; // added eligibilityForCrsReg by Shailendra K. on dated 20.062023 as per T-44816
            sessionActivity.ActivityNo = Convert.ToInt32(ddlActivityName.SelectedValue);

            // added by Shailendra K. on dated 17.08.2023 as per T-47551
            int studIDType = 1;
            if (ddlStudentIDType.SelectedIndex > 1)
                studIDType = Convert.ToInt16(ddlStudentIDType.SelectedValue);


            if (ViewState["edit"].ToString() == "add")
            {
                int groupid = 0;
                cs = (CustomStatus)activityController.CourseRegistraionActivityInsert(sessionActivity, Sessionnos, CollegeIds, Degreenos, Branchnos, Semesternos,
                    UserRights, CoursePattern, choiceFor, PAYMENT_APPLICABLE_FOR_SEM_WISE, groupid, StartTime, EndTime, sessionid, eligibilityForCrsReg, studIDType); //added on 22/03/23 //Commented on 2022 Aug 30
                // added eligibilityForCrsReg by Shailendra K. on dated 20.062023 as per T-44816
            }
            else if (ViewState["edit"].ToString() == "edit")
            {
                cs = (CustomStatus)activityController.CourseRegistraionActivityInsert(sessionActivity, Sessionnos, CollegeIds, Degreenos, Branchnos, Semesternos,
                    UserRights, CoursePattern, choiceFor, PAYMENT_APPLICABLE_FOR_SEM_WISE, Convert.ToInt32(ViewState["sessionactivityno"]), StartTime, EndTime, sessionid,
                    eligibilityForCrsReg, studIDType);  //Commented on 2022 Aug 30
                // added eligibilityForCrsReg by Shailendra K. on dated 20.062023 as per T-44816
            }


            #region commented code on dated 10.11.2022

            #endregion

            Clear();

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Course Registration Activity Definition Saved Successfully!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Record Already Exists!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Course Registration Activity Definition Updated Successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Error!", this.Page);
            }

            LoadDefinedSessionActivities();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
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
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkBranchList.Visible = true;
                        chkBranchList.DataTextField = "SHORTNAME";
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
            int totCount = 0;
            string values = string.Empty;
            foreach (ListItem Item in chkDegreeList.Items)
            {
                totCount++;
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
            chkDegree.Checked = (totCount == count) ? true : false;
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    //Code Added for multiple College selection and session selection on 2022 Aug 30

    // add by nehal [22-03-2023]
    private void SessionBind()
    {
        try
        {
            objCommon.FillDropDownList(ddlSessionCollege, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC"); // ADDED ORDER BY DESC CLAUSE BY SHAILENDRA K. ON DATED 20.06.2023
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSessionCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultipleCollegeBind();
    }
    private void MultipleCollegeBind()
    {
        try
        {
            // modified by nehal [22-03-2023]
            DataSet ds = null;
            ds = AcadDash.Get_CollegeID_BySession(Convert.ToInt32(ddlSessionCollege.SelectedValue));

            ddlCollege.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.DataSource = ds;
                ddlCollege.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlCollege.DataTextField = ds.Tables[0].Columns[3].ToString();
                ddlCollege.DataBind();
            }

            //// add by maithili [07-09-2022]
            //DataSet ds = null;
            //ds = AcadDash.Get_CollegeID_Sessionno(1, "");

            //ddlCollege.Items.Clear();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlCollege.DataSource = ds;
            //    ddlCollege.DataValueField = ds.Tables[0].Columns[0].ToString();
            //    ddlCollege.DataTextField = ds.Tables[0].Columns[4].ToString();
            //    ddlCollege.DataBind();
            //}
            ////end 

            ////objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch
        {
            throw;
        }
    }

    private void MultipleSessionBind(string CollgeIds)
    {
        try
        {
            objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollgeIds + "',',')) AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged1(object sender, EventArgs e)
    {
        chkBranchList.Items.Clear();
        chkSemesterList.Items.Clear();
        chkDegreeList.Items.Clear();
        string collegenose = string.Empty;
        //
        string collegenos = string.Empty;
        //string collegenames = string.Empty; ;;;
        foreach (ListItem items in ddlCollege.Items)
        {
            if (items.Selected == true)
                collegenos += (items.Value).Split('-')[0] + ',';
        }
        collegenos = collegenos.TrimEnd(',');

        if (collegenos.Length > 0)
        {
            MultipleSessionBind(collegenos);
            ddlSession.Focus();
            //PopulateDegreeList();
            BindDegreeWithMultipleCollege(collegenos);
            LoadDefinedSessionActivities();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
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
        ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "DISTINCT B.BRANCHNO", "B.SHORTNAME +' ('+D.CODE+')' SHORTNAME", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "SHORTNAME");
        return ds;
    }

    private Boolean CheckSessionSelected()
    {
        Boolean IsSessionSelected = false;

        string SessionNos = string.Empty;
        //foreach (ListItem items in ddlSession.Items)
        foreach (ListItem items in ddlCollege.Items)//add by maithili [08-09-2022]
        {
            if (items.Selected == true)
            {
                //SessionNos += items.Value + ',';
                SessionNos += (items.Value).Split('-')[1] + ','; //add by maithili [08-09-2022]
            }
        }

        if (SessionNos.Length < 1)
            IsSessionSelected = false;
        else if (SessionNos.Length > 0)
            IsSessionSelected = true;

        return IsSessionSelected;
    }

    private string GetSelectedSessionIds()
    {
        string SessionIds = string.Empty;
        foreach (ListItem items in ddlCollege.Items)
        {
            if (items.Selected == true)
            {
                //CollegeIds += items.Value + ',';
                SessionIds += (items.Value).Split('-')[1] + ','; // Add by maithili [08-09-2022]
            }
        }
        if (SessionIds.Length > 1)
        {
            SessionIds = SessionIds.Remove(SessionIds.Length - 1);
        }
        return SessionIds;
    }
    
    private string GetSelectedCollegeIds()
    {
        string CollegeIds = string.Empty;
        foreach (ListItem items in ddlCollege.Items)
        {
            if (items.Selected == true)
            {
                //CollegeIds += items.Value + ',';
                CollegeIds += (items.Value).Split('-')[0] + ','; // Add by maithili [08-09-2022]
            }
        }
        if (CollegeIds.Length > 1)
        {
            CollegeIds = CollegeIds.Remove(CollegeIds.Length - 1);
        }
        return CollegeIds;
    }

    private string GetSelectedDegreeIds()
    {

        string DegreeIds = string.Empty;

        //foreach (ListItem itemsdegree in chkDegreeList.Items)
        //{
        //    if (itemsdegree.Selected == true)
        //    {
        //        // added by shailendra K on dated 21.06.2023 as per T-44837
        //        if (!string.IsNullOrEmpty(DegreeIds))
        //        {
        //            if (!DegreeIds.Contains(itemsdegree.Value))
        //                DegreeIds += itemsdegree.Value + ',';
        //        }
        //        else
        //            DegreeIds += itemsdegree.Value + ',';
        //    }
        //}
        foreach (ListItem item1 in chkDegreeList.Items)
        {
            if (item1.Selected == true)
            {
                DegreeIds += item1.Value + ',';
            }
        }
        if (DegreeIds != string.Empty)
        {
            DegreeIds = DegreeIds.Substring(0, DegreeIds.Length - 1);
        }

        //if (DegreeIds.Length > 1)
        //{
        //    DegreeIds = DegreeIds.Remove(DegreeIds.Length - 1);
        //}
        DegreeIds = DegreeIds.TrimEnd(',');
        return DegreeIds;
    }

    private string GetSelectedBranchIds()
    {
        string BranchIds = string.Empty;
        //foreach (ListItem items in chkBranchList.Items)
        //{
        //    if (items.Selected == true)
        //    {
        //        // added by shailendra K on dated 21.06.2023 as per T-44837
        //        if (!string.IsNullOrEmpty(BranchIds))
        //        {
        //            if (!BranchIds.Contains(items.Value))
        //                BranchIds += (items.Value).Split('-')[0] + ',';
        //        }
        //        else
        //            BranchIds += (items.Value).Split('-')[0] + ',';

        //        //CollegeIds += items.Value + ',';
        //        // BranchIds += (items.Value).Split('-')[0] + ','; // Add by maithili [08-09-2022]
        //    }
        //}
        foreach (ListItem item1 in chkBranchList.Items)
        {
            if (item1.Selected == true)
            {
                BranchIds += item1.Value + ',';
            }
        }
        if (BranchIds != string.Empty)
        {
            BranchIds = BranchIds.Substring(0, BranchIds.Length - 1);
        }
        //if (BranchIds.Length > 1)
        //{
        //    BranchIds = BranchIds.Remove(BranchIds.Length - 1);
        //}
        BranchIds = BranchIds.TrimEnd(',');
        return BranchIds;
    }

    private string GetSelectedSemesterIds()
    {
        string SemesterIds = string.Empty;
        foreach (ListItem items in chkSemesterList.Items)
        {
            if (items.Selected == true)
            {
                //CollegeIds += items.Value + ',';
                SemesterIds += (items.Value).Split('-')[0] + ','; // Add by maithili [08-09-2022]
            }
        }
        if (SemesterIds.Length > 1)
        {
            SemesterIds = SemesterIds.Remove(SemesterIds.Length - 1);
        }
        return SemesterIds;
    }

    private List<SessionActivity> BindDataMultiple()
    {
        try
        {
            string sessionnos = string.Empty;
            //foreach (ListItem Sessionitem in ddlSession.Items)
            foreach (ListItem Sessionitem in ddlCollege.Items) //Add by maithili [08-09-2022]
            {

                if (Sessionitem.Selected == true)
                {
                    List<int> lstDegreeClgWise = new List<int>();
                    DataSet dsDegree = objCommon.FillDropDown("ACD_COLLEGE_DEGREE", "DISTINCT DEGREENO", "", " COLLEGE_ID=" + Convert.ToInt32((Sessionitem.Value).Split('-')[0]), "DEGREENO");

                    foreach (DataRow dr in dsDegree.Tables[0].Rows)
                        lstDegreeClgWise.Add(Convert.ToInt32(dr["DEGREENO"].ToString()));

                    foreach (ListItem liDgr in chkDegreeList.Items)
                    {
                        if (liDgr.Selected && lstDegreeClgWise.Contains(Convert.ToInt32(liDgr.Value)))
                        {
                            List<int> lstBranchDegreeClgWise = new List<int>();
                            DataSet dsBranches = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "", " COLLEGE_ID=" + Convert.ToInt32((Sessionitem.Value).Split('-')[0]) + " AND DEGREENO=" + Convert.ToInt32(liDgr.Value), "BRANCHNO");

                            foreach (DataRow dr in dsBranches.Tables[0].Rows)
                                lstBranchDegreeClgWise.Add(Convert.ToInt32(dr["BRANCHNO"].ToString()));

                            foreach (ListItem liBrn in chkBranchList.Items)
                            {
                                if (liBrn.Selected && lstBranchDegreeClgWise.Contains(Convert.ToInt32(liBrn.Value)))
                                {
                                    sessionActivity = new SessionActivity();
                                    sessionnos += Sessionitem.Value.Split('-')[1] + ',';
                                    sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());

                                    sessionActivity.SessionNo = Convert.ToInt32((Sessionitem.Value).Split('-')[1]);
                                    sessionActivity.College_Id = Convert.ToInt32((Sessionitem.Value).Split('-')[0]);

                                    sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
                                    sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);

                                    sessionActivity.IsStarted = hfdActive.Value == "true" ? true : false;
                                    sessionActivity.ShowStatus = hfdShowStatus.Value == "true" ? 1 : 0;

                                    sessionActivity.Session_Name = Sessionitem.Text;
                                    sessionActivity.Degree = Convert.ToInt32(liDgr.Value);
                                    sessionActivity.Branch = 0;
                                    sessionActivity.Branch = Convert.ToInt32(liBrn.Value);
                                    sessionActivityList.Add(sessionActivity);
                                }
                            }
                        }
                    }


                }
            }
            if (sessionnos.Length > 1)
                sessionnos = sessionnos.TrimEnd(',');
        }
        catch (Exception ex)
        {
            throw;
        }
        return sessionActivityList;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;
            int recordId = int.Parse(btnEditRecord.CommandArgument);
            //DataSet ds = (DataSet)ViewState["DS_ACD_COURSE_REG_CONFIG_ACTIVITY"];

            //DataRow[] dr1 = ds.Tables[0].Select("COURSE_REG_CONFIG_NO = " + recordId);
            ViewState["edit"] = "edit";

            this.BindCoursesRegistrationActvityEdit(recordId);


        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void BindCoursesRegistrationActvityEdit(int groupid)
    {
        try
        {
            DataSet ds = activityController.GetCourseRegConfigActivityDataDetailsEdit(groupid);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                int capacity = Convert.ToInt32(ds.Tables[0].Rows[0]["CHOICE_FOR"]);
                int activeStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["STARTED"]);
                ViewState["SESSIONID"] = Convert.ToString(ds.Tables[0].Rows[0]["SESSIONID"]);
                ViewState["degreenos"] = Convert.ToString(ds.Tables[0].Rows[0]["DEGREENO"]);
                ViewState["branchnos"] = Convert.ToString(ds.Tables[0].Rows[0]["BRANCHNO"]);
                ViewState["semesternos"] = Convert.ToString(ds.Tables[0].Rows[0]["SEMESTER"]);
                ViewState["usertype"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_TYPE"]);
                ViewState["coursetype"] = Convert.ToString(ds.Tables[0].Rows[0]["CORE_ELECT_GLOBAL_COURSE_TYPE_NO"]);
                ViewState["COLLEGE_ID"] = Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                SessionBind();
                ddlSessionCollege.SelectedValue = Convert.ToString(ViewState["SESSIONID"]);
                ddlActivityName.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CRS_ACTIVITY_NO"]);
                MultipleCollegeBind();

                if (ViewState["COLLEGE_ID"] != null && ViewState["COLLEGE_ID"] != "")
                {
                    string Collegeid = ViewState["COLLEGE_ID"].ToString();
                    string[] subs = Collegeid.Split(',');

                    foreach (ListItem collegeitems in ddlCollege.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            if (subs[i].ToString().Trim() == collegeitems.Value)
                                collegeitems.Selected = true;
                        }
                    }
                }

                //string collegenos = string.Empty;
                ////string collegenames = string.Empty; ;;;
                //foreach (ListItem items in ddlCollege.Items)
                //{
                //    if (items.Selected == true)
                //        collegenos += (items.Value).Split('-')[0] + ',';
                //}
                //collegenos = collegenos.TrimEnd(',');

                //if (collegenos.Length > 0)
                //{
                //    MultipleSessionBind(collegenos);
                //    ddlSession.Focus();
                //    BindDegreeWithMultipleCollege(collegenos);
                //}
                //else
                //{
                //    ddlSession.Items.Clear();
                //    ddlSession.Items.Add(new ListItem("Please Select", "0"));
                //}


                txtStartDate.Text = ((ds.Tables[0].Rows[0]["START_DATE"].ToString() != string.Empty) ? ((DateTime)ds.Tables[0].Rows[0]["START_DATE"]).ToShortDateString() : string.Empty);
                txtEndDate.Text = ((ds.Tables[0].Rows[0]["END_DATE"].ToString() != string.Empty) ? ((DateTime)ds.Tables[0].Rows[0]["END_DATE"]).ToShortDateString() : string.Empty);
                txtfrom.Text = ((ds.Tables[0].Rows[0]["STARTTIME"].ToString() != string.Empty) ? (ds.Tables[0].Rows[0]["STARTTIME"]).ToString() : string.Empty);
                txtTo.Text = ((ds.Tables[0].Rows[0]["ENDTIME"].ToString() != string.Empty) ? (ds.Tables[0].Rows[0]["ENDTIME"]).ToString() : string.Empty);


                //PopulateDegreeList();
                BindDegreeWithMultipleCollege(Convert.ToString(ViewState["COLLEGE_ID"]));
                if (ViewState["degreenos"] != null && ViewState["degreenos"] != "")
                {
                    string Degreenos = ViewState["degreenos"].ToString();
                    string[] subs = Degreenos.Split(',');

                    foreach (ListItem degreeitems in chkDegreeList.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            //if (subs[i].Contains(degreeitems.Value))
                            //{
                            //    degreeitems.Selected = true;
                            //}
                            if (subs[i].ToString().Trim() == degreeitems.Value)
                            {
                                degreeitems.Selected = true;
                            }
                        }
                    }
                }

                PopulateBranchList();
                PopulateSemesterList();
                PopulatUserRightsList();


                if (ViewState["branchnos"] != null && ViewState["branchnos"] != "")
                {
                    string Branchnos = ViewState["branchnos"].ToString();
                    string[] subs = Branchnos.Split(',');

                    foreach (ListItem branchitems in chkBranchList.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            //if (subs[i].Contains(branchitems.Value))
                            //{
                            //    branchitems.Selected = true;
                            //}
                            if (subs[i].ToString().Trim() == branchitems.Value)
                            {
                                branchitems.Selected = true;
                            }
                        }
                    }
                }
                if (ViewState["semesternos"] != null && ViewState["semesternos"] != "")
                {
                    string Semesternos = ViewState["semesternos"].ToString();
                    string[] subs = Semesternos.Split(',');

                    foreach (ListItem semetseritems in chkSemesterList.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            //if (subs[i].Contains(semetseritems.Value))
                            //{
                            //    semetseritems.Selected = true;
                            //}
                            if (subs[i].ToString().Trim() == semetseritems.Value)
                            {
                                semetseritems.Selected = true;
                            }
                        }
                    }
                }
                if (ViewState["usertype"] != null && ViewState["usertype"] != "")
                {
                    string UserType = ViewState["usertype"].ToString();
                    string[] subs = UserType.Split(',');

                    foreach (ListItem useritems in chkUserRightsList.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            //if (subs[i].Contains(useritems.Value))
                            //{
                            //    useritems.Selected = true;
                            //}
                            if (subs[i].ToString().Trim() == useritems.Value)
                            {
                                useritems.Selected = true;
                            }
                        }
                    }
                }

                if (ViewState["coursetype"] != null && ViewState["coursetype"] != "")
                {
                    string CourseType = ViewState["coursetype"].ToString();
                    string[] subs = CourseType.Split(',');

                    foreach (ListItem coursetypeitems in ckhCoursePatternList.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            if (subs[i].ToString().Trim() == coursetypeitems.Value)
                                coursetypeitems.Selected = true;
                        }
                    }
                }
                else
                {
                    foreach (ListItem coursetypeitems in ckhCoursePatternList.Items)
                        coursetypeitems.Selected = false;
                }
                dvCoursePattern.Visible = Convert.ToInt32(ddlActivityName.SelectedValue) != 2 ? true : false;

                string coursePattern = ds.Tables[0].Rows[0]["CORE_ELECT_GLOBAL_COURSE_TYPE_NO"].ToString();
                if (coursePattern.Contains("2") || coursePattern.Contains('3'))
                {
                    dvChoiseFor.Visible = true;
                    txtChoiseFor.Text = ds.Tables[0].Rows[0]["CHOICE_FOR"].ToString();
                    lblAttempt.InnerText = "Max Login Attempt To Submit the Courses";
                }
                else
                {
                    dvChoiseFor.Visible = false;
                    txtChoiseFor.Text = string.Empty;
                }


                if (ds.Tables[0].Rows[0]["STARTED"].ToString().ToUpper() == "TRUE")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(false);", true);
                }

                if (ds.Tables[0].Rows[0]["SHOW_STATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
                }

                if (ds.Tables[0].Rows[0]["PAYMENT_APPLICABLE_FOR_SEM_WISE"].ToString().ToUpper() == "TRUE")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "SetStatPaymentApplicableForSemWise(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "SetStatPaymentApplicableForSemWise(false);", true);
                }

                if (ds.Tables[0].Rows[0]["ELIGIBILITY_FOR_CRS_REG"].ToString() == "1")
                    ScriptManager.RegisterStartupScript(this, GetType(), "EligibleCrsReg", "SetEligibleForCrsReg(true);", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "EligibleCrsReg", "SetEligibleForCrsReg(false);", true);

                ViewState["sessionactivityno"] = groupid.ToString();
                ddlStudentIDType.SelectedValue = ds.Tables[0].Rows[0]["STUD_IDTYPE"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.BindOfferedGlobalCoursesEdit -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCoursePattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        //dvChoiseFor.Visible = Convert.ToInt32(ddlCoursePattern.SelectedValue) > 1 ? true : false;
        //lblAttempt.InnerText = "Max Login Attempt To Submit the " + ddlCoursePattern.SelectedItem;
        try
        {
            int count = 0;
            int totCount = 0;
            string CoursePattern = string.Empty;
            for (int i = 0; i < ckhCoursePatternList.Items.Count; i++)
            {
                totCount++;
                if (ckhCoursePatternList.Items[i].Selected)
                {
                    count++;
                    CoursePattern += ckhCoursePatternList.Items[i].Value + ",";
                }
            }
            ckhCoursePattern.Checked = (totCount == count) ? true : false;

            if (CoursePattern.Contains("2") || CoursePattern.Contains('3'))
            {
                dvChoiseFor.Visible = true;
                txtChoiseFor.Text = string.Empty;
                lblAttempt.InnerText = "Max Login Attempt To Submit the Courses";
            }
            else
            {
                dvChoiseFor.Visible = false;
                lblAttempt.InnerText = string.Empty;
            }

            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatMandat('" + hfdShowStatus.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatActive('" + hfdActive.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatPaymentApplicableForSemWise('" + hfdPaymentApplicableForSemWise.Value + "');", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lvSessionActivities_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ImageButton btnEdit = dataitem.FindControl("btnEdit") as ImageButton;
        Label lblActive = (e.Item.FindControl("lblActive")) as Label;
        if (lblActive.Text == "STARTED")
            lblActive.ForeColor = System.Drawing.Color.Green;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            DataRow dr = ((DataRowView)e.Item.DataItem).Row;
            Label lblCoursePattern = (e.Item.FindControl("lblCoursePattern")) as Label;

            string coursePtrn = dr["CORE_ELECT_GLOBAL_COURSE_TYPE_NO"].ToString();
            if (!string.IsNullOrEmpty(coursePtrn))
            {
                string[] crsptrn = coursePtrn.Split(',');
                foreach (string cp in crsptrn)
                    lblCoursePattern.Text += (cp == "1") ? " Core Course," : (cp == "2") ? " Elective Course," : " Global Elective Course,";

                //if (cp == "2")
                //    lblCoursePattern.Text += "Elective Course,";
                //if (cp == "3")
                //    lblCoursePattern.Text += "Global Elective Course,";  

                lblCoursePattern.Text = lblCoursePattern.Text.TrimEnd(',');
            }
        }
        int gropuid = Convert.ToInt32(btnEdit.CommandArgument);
        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {
            DataSet ds = activityController.GetCourseRegConfigActivityDataDetails(gropuid);
            lv.DataSource = ds;
            lv.DataBind();

        }
        catch { }

    }
    protected void chkBranchList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int totCount = 0;

            foreach (ListItem Item in chkBranchList.Items)
            {
                totCount++;
                if (Item.Selected) count++;
            }
            chkBranch.Checked = (totCount == count) ? true : false;

            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatMandat('" + hfdShowStatus.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatActive('" + hfdActive.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatPaymentApplicableForSemWise('" + hfdPaymentApplicableForSemWise.Value + "');", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void chkSemesterList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int totCount = 0;

            foreach (ListItem Item in chkSemesterList.Items)
            {
                totCount++;
                if (Item.Selected) count++;
            }
            chkSemester.Checked = (totCount == count) ? true : false;
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatMandat('" + hfdShowStatus.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatActive('" + hfdActive.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatPaymentApplicableForSemWise('" + hfdPaymentApplicableForSemWise.Value + "');", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void chkUserRightsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int totCount = 0;

            foreach (ListItem Item in chkUserRightsList.Items)
            {
                totCount++;
                if (Item.Selected) count++;
            }
            chkUserRights.Checked = (totCount == count) ? true : false;
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatMandat('" + hfdShowStatus.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatActive('" + hfdActive.Value + "');", true);
            //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatPaymentApplicableForSemWise('" + hfdPaymentApplicableForSemWise.Value + "');", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ckhCoursePattern_CheckedChanged(object sender, EventArgs e)
    {
        if (ckhCoursePattern.Checked)
        {
            dvChoiseFor.Visible = true;
            lblAttempt.InnerText = "Max Login Attempt To Submit the Courses";
        }
        else
        {
            dvChoiseFor.Visible = false;
            txtChoiseFor.Text = string.Empty;
        }
        //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatMandat('" + hfdShowStatus.Value + "');", true);
        //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatActive('" + hfdActive.Value + "');", true);
        //ScriptManager.RegisterClientScriptBlock(updSesActivity, updSesActivity.GetType(), "Src", "SetStatPaymentApplicableForSemWise('" + hfdPaymentApplicableForSemWise.Value + "');", true);

    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        if (txtStartDate.Text != "")
        {
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            if (EndDate < StartDate)
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select End date is greater than the Start date!", this.Page);
                txtEndDate.Text = "";
                txtEndDate.Focus();
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updSesActivity, "Please Enter Start Date!", this.Page);
            txtStartDate.Focus();
        }
    }
    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string text = txtfrom.Text;
            text = text.Remove(text.Length - 3);
            int tm = Convert.ToInt32(text.Split(':').LastOrDefault());

            string[] separatingStrings = { 
                                             ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
                                           ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
                                           ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
                                            ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
                                           ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
                                          ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
                                          ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
                                          ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
                                           ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
                                            ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
                                             ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
                                             ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
                                             ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
                                             ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
                                             ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
                                             ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
                                             ":97", ":97", ":98", ":98", ":99", ":99"
                                         };

            string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
          

            int FromTime = Convert.ToInt32(words[0]);
         
            if (FromTime > 12)
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtfrom.Text = string.Empty;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtfrom.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtfrom.Text = string.Empty;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string text = txtTo.Text;
            text = text.Remove(text.Length - 3);
            int tm = Convert.ToInt32(text.Split(':').LastOrDefault());

            string[] separatingStrings = { 
                                             ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
                                           ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
                                           ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
                                            ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
                                           ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
                                          ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
                                          ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
                                          ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
                                           ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
                                            ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
                                             ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
                                             ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
                                             ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
                                             ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
                                             ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
                                             ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
                                             ":97", ":97", ":98", ":98", ":99", ":99"
                                         };



            string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            
            int FromTime = Convert.ToInt32(words[0]);
           
            if (FromTime > 12)
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtTo.Text = string.Empty;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtTo.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtTo.Text = string.Empty;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GridView GVCreditDef = new GridView();
        string sp_Name = string.Empty; string sp_Paramters = string.Empty; string sp_Values = string.Empty;
        sp_Name = "PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA_DETAILS_EXCEL";
        sp_Paramters = "@P_OUT";
        sp_Values = "0";
        DataSet ds = objCommon.DynamicSPCall_Select(sp_Name, sp_Paramters, sp_Values);
        if (ds != null && ds.Tables.Count > 0)
        {
            GVCreditDef.DataSource = ds;
            GVCreditDef.DataBind();

            string attachment = "attachment; filename=" + "CourseRegActivityDataReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVCreditDef.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found!!!", this.Page);
        }

       
    }
}