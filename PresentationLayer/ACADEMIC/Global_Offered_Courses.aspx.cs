using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.IO;

public partial class ACADEMIC_Global_Offered_Courses : System.Web.UI.Page
{
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string courseNo = string.Empty;
    CourseController objCC = new CourseController();
    AcademinDashboardController objADEController = new AcademinDashboardController();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();
    StudentController objSC = new StudentController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ccode"] = string.Empty;
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Populate the DropDownList 
                PopulateDropDownList();

                Session["TimeSlotTbl"] = null;
                ViewState["globaledit"] = "add";
                
                int globalElectiveCTAllotment = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "IS_GLOBAL_ELECTIVE_CT_ALLOTMENT_REQUIRED", "ConfigNo>0"));
                ViewState["globalElectiveCTAllotment"] = globalElectiveCTAllotment;
                if (globalElectiveCTAllotment == 1)
                {
                    liCourseTeacher.Visible = true;
                    updCourseTeacher.Visible = true;
                    divAddtionalTeacherTS.Visible = false;
                }
                else
                {
                    liCourseTeacher.Visible = false;
                    updCourseTeacher.Visible = false;
                    divAddtionalTeacherTS.Visible = true;
                }
            }
            Session["reportdate"] = null;
        }

    }

    private void PopulateDropDownList()
    {
        DataSet dsSlotType = objCommon.FillDropDown("ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SLOTTYPENO");
        if (dsSlotType.Tables[0].Rows.Count > 0)
        {

            ddlSlotType.DataSource = dsSlotType;
            ddlSlotType.DataValueField = dsSlotType.Tables[0].Columns[0].ToString();
            ddlSlotType.DataTextField = dsSlotType.Tables[0].Columns[1].ToString();
            ddlSlotType.DataBind();


            ddlSlotTypeCancelTT.DataSource = dsSlotType;
            ddlSlotTypeCancelTT.DataValueField = dsSlotType.Tables[0].Columns[0].ToString();
            ddlSlotTypeCancelTT.DataTextField = dsSlotType.Tables[0].Columns[1].ToString();
            ddlSlotTypeCancelTT.DataBind();

            ddlSlotTypeReport.DataSource = dsSlotType;
            ddlSlotTypeReport.DataValueField = dsSlotType.Tables[0].Columns[0].ToString();
            ddlSlotTypeReport.DataTextField = dsSlotType.Tables[0].Columns[1].ToString();
            ddlSlotTypeReport.DataBind();

            ddlRevisedSlotType.DataSource = dsSlotType;
            ddlRevisedSlotType.DataValueField = dsSlotType.Tables[0].Columns[0].ToString();
            ddlRevisedSlotType.DataTextField = dsSlotType.Tables[0].Columns[1].ToString();
            ddlRevisedSlotType.DataBind();
        }
        else
        {
            ddlSlotType.DataSource = null;
            ddlSlotType.DataBind();

            ddlSlotTypeCancelTT.DataSource = null;
            ddlSlotTypeCancelTT.DataBind();

            ddlSlotTypeReport.DataSource = null;
            ddlSlotTypeReport.DataBind();

            ddlRevisedSlotType.DataSource = null;
            ddlRevisedSlotType.DataBind();
        }

        DataSet dsAllDay = objCommon.FillDropDown("ACD_DAY_MASTER", "DAY_NO", "DAY_NAME", "DAY_NO > 0", "DAY_NO");
        if (dsAllDay.Tables[0].Rows.Count > 0)
        {
            ddlAllDay.DataSource = dsAllDay;
            ddlAllDay.DataValueField = dsAllDay.Tables[0].Columns[0].ToString();
            ddlAllDay.DataTextField = dsAllDay.Tables[0].Columns[1].ToString();
            ddlAllDay.DataBind();

            ddlRevisedAllDay.DataSource = dsAllDay;
            ddlRevisedAllDay.DataValueField = dsAllDay.Tables[0].Columns[0].ToString();
            ddlRevisedAllDay.DataTextField = dsAllDay.Tables[0].Columns[1].ToString();
            ddlRevisedAllDay.DataBind();

        }
        else
        {
            ddlAllDay.DataSource = null;
            ddlAllDay.DataBind();

            ddlRevisedAllDay.DataSource = null;
            ddlRevisedAllDay.DataBind();

        }
        //objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SLOTTYPENO");
        //objCommon.FillDropDownList(ddlSlotTypeCancelTT, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SLOTTYPENO");
        //objCommon.FillDropDownList(ddlAllDay, "ACD_DAY_MASTER", "DAY_NO", "DAY_NAME", "DAY_NO > 0", "DAY_NO");
        //objCommon.FillDropDownList(ddlGlobalElectiveGroup, "ACD_GLOBAL_ELECTIVE_GROUP_MASTER", "GMID", "GROUP_NAME", "GMID > 0 AND ISNULL(ACTIVESTATUS,0)=1", "GMID");//
        objCommon.FillDropDownList(ddlGlobalElectiveGroup, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", " ISNULL(ACTIVESTATUS,0)=1 AND SECTIONNO > 0", "SECTIONNO");
        //ddlGlobalElectiveGroup.SelectedIndex = 1;
        //// objCommon.FillDropDownList(ddlToSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 and SEMESTERNO<9", "");
        ////PopulateSemesterList();
        ////Added by Nehal
        //objCommon.FillDropDownList(ddlRevisedSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SLOTTYPENO");
        //objCommon.FillDropDownList(ddlRevisedAllDay, "ACD_DAY_MASTER", "DAY_NO", "DAY_NAME", "DAY_NO > 0", "DAY_NO");
        //
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_COURSE C ON(SM.SCHEMENO = C.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "ISNULL(C.GLOBALELE,0) = 1 AND SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "COSCHNO");
            objCommon.FillDropDownList(ddlCollegeSchemeTimeTable, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_COURSE C ON(SM.SCHEMENO = C.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "ISNULL(C.GLOBALELE,0) = 1 AND SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "COSCHNO");
            objCommon.FillDropDownList(ddlCollegeSchemeAttConfig, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_COURSE C ON(SM.SCHEMENO = C.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "ISNULL(C.GLOBALELE,0) = 1 AND SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "COSCHNO");
        }
        else
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COURSE C ON(SM.SCHEMENO = C.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "ISNULL(C.GLOBALELE,0) = 1 AND COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            objCommon.FillDropDownList(ddlCollegeSchemeTimeTable, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COURSE C ON(SM.SCHEMENO = C.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "ISNULL(C.GLOBALELE,0) = 1 AND COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            objCommon.FillDropDownList(ddlCollegeSchemeAttConfig, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COURSE C ON(SM.SCHEMENO = C.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "ISNULL(C.GLOBALELE,0) = 1 AND COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
        }



        //DataSet ds = objADEController.Get_College_Session(3, Session["college_nos"].ToString());
        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    ddlCollegeSession.DataSource = ds;
        //    ddlCollegeSession.DataValueField = ds.Tables[0].Columns[0].ToString();
        //    ddlCollegeSession.DataTextField = ds.Tables[0].Columns[4].ToString();
        //    ddlCollegeSession.DataBind();

        //}
        DataSet ds = objADEController.Get_College_Session(4, Session["college_nos"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {

            ddlSession.DataSource = ds;
            ddlSession.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSession.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSession.DataBind();

            ddlSessionAttConfig.DataSource = ds;
            ddlSessionAttConfig.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSessionAttConfig.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSessionAttConfig.DataBind();

            ddlSessionTimeTable.DataSource = ds;
            ddlSessionTimeTable.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSessionTimeTable.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSessionTimeTable.DataBind();

            ddlSessionCT.DataSource = ds;
            ddlSessionCT.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSessionCT.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSessionCT.DataBind();


            ddlSessionCancelTT.DataSource = ds;
            ddlSessionCancelTT.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSessionCancelTT.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSessionCancelTT.DataBind();

            // Added by nehal
            ddlSessionRevisedTimeTable.DataSource = ds;
            ddlSessionRevisedTimeTable.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSessionRevisedTimeTable.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSessionRevisedTimeTable.DataBind();
            //

            //Report Session
            ddlSessionReport.DataSource = ds;
            ddlSessionReport.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlSessionReport.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlSessionReport.DataBind();
            //End Report Session

        }
        this.BindListViewAttConfig();
    }

    //private void PopulateSemesterList()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <>0", "SEMESTERNO");
    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                chkToSemesterList.DataTextField = "SEMESTERNAME";
    //                chkToSemesterList.DataValueField = "SEMESTERNO";
    //                chkToSemesterList.DataSource = ds.Tables[0];
    //                chkToSemesterList.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }



    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //chkToSemesterList.Items.Clear();
        ddlSemester.SelectedIndex = 0;
        //ddlToSemester.SelectedIndex = 0;
        //ddlSession.SelectedIndex = 0;
        //PopulateSemesterList();
        pnlCourse.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvGLobalOfferedCourses.DataSource = null;
        lvGLobalOfferedCourses.DataBind();
        lvGLobalOfferedCourses.Visible = false;
        pnlGLobalOfferedCourses.Visible = false;

        if (ddlCollege.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"].ToString()) + " and DEGREENO=" + Convert.ToInt32(ViewState["degreeno"].ToString()) + "and BRANCHNO=" + Convert.ToInt32(ViewState["branchno"].ToString()) + ""));
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON(S.SEMESTERNO = C.SEMESTERNO) ", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "ISNULL(C.GLOBALELE,0) = 1 AND S.SEMESTERNO>0 AND S.SEMESTERNO<=" + duration * 2 + " AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
                ddlSemester.Focus();
            }
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlCollege.Focus();
            this.clearFields();
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = null;
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            CourseController objStud = new CourseController();
            ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_ELECTGROUP EG ON(C.GROUPNO = EG.GROUPNO) ", "DISTINCT C.COURSENO", "C.COURSE_NAME,C.CCODE,C.CREDITS,C.BOS_DEPTNO,EG.GROUPNAME", "ISNULL(C.GLOBALELE,0) = 1 AND C.SCHEMENO= " + schemeno + " AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "C.COURSENO");
            ViewState["COURSENO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSENO"]).ToString();
            ViewState["BOS_DEPTNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BOS_DEPTNO"]).ToString();
            // ds = objStud.GetCourseOfferedGlobally(schemeno, Convert.ToInt32(ddlSemester.SelectedValue)); //, Convert.ToInt32(ddlToSemester.SelectedValue), Convert.ToInt32(0));
            DataSet ds1 = objCommon.FillDropDown("ACD_GLOBAL_OFFERED_COURSE", "*", "", "", "GLOBAL_OFFER_ID");
            ViewState["GLOBAL_OFFERED_COURSES_DATA"] = ds1;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label - 
                // DataSet dsbranch = objCommon.FillDropDown("ACD_BRANCH ", "DISTINCT BRANCHNO", "LONGNAME +' ('+SHORTNAME +')' AS BRANCHNAME", "BRANCHNO>0 and BRANCHNO<>" + ViewState["branchno"].ToString(), "BRANCHNO");
                DataSet dsbranch = objCommon.FillDropDown("ACD_BRANCH ", "DISTINCT BRANCHNO", "LONGNAME +' ('+SHORTNAME +')' AS BRANCHNAME", "BRANCHNO>0 ", "BRANCHNO");
                // DataSet dsCollegeSession = objStud.GetCollegeSession(1, ViewState["college_id"].ToString());
                DataSet dsCollegeSession = objCommon.FillDropDown("ACD_SESSION_MASTER SM INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "CM.COLLEGE_ID,SESSION_NAME,COLLEGE_NAME,CONCAT (COLLEGE_NAME , '-',SESSION_NAME) AS COLLEGE_SESSION ", "ISNULL(FLOCK,0)=1 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO,CM.COLLEGE_ID DESC");


                DataSet dsTeacher = objCommon.FillDropDown("User_Acc", "UA_NO", "UA_FULLNAME", Convert.ToInt32(ViewState["BOS_DEPTNO"]) + " IN (select value from DBO.Split(UA_DEPTNO,',')) AND UA_TYPE=3", "UA_FULLNAME");

                DataSet dsSemester = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <>0", "SEMESTERNO");


                foreach (ListViewDataItem itm in lvCourse.Items)
                {
                    ListBox lstBranch = (ListBox)itm.FindControl("lstBranch");
                    //lstBranch.Items.Clear();
                    //lstBranch.DataSource = dsbranch;
                    //lstBranch.DataValueField = "BRANCHNO";
                    //lstBranch.DataTextField = "BRANCHNAME";
                    //lstBranch.DataBind();
                    //lstBranch.SelectedIndex = -1;

                    ListBox lstCollegeSession = (ListBox)itm.FindControl("lstCollegeSession");
                    lstCollegeSession.Items.Clear();
                    lstCollegeSession.DataSource = dsCollegeSession;
                    lstCollegeSession.DataValueField = "SESSIONNO";
                    lstCollegeSession.DataTextField = "COLLEGE_SESSION";
                    //lstCollegeSession.ToolTip = "COLLEGE_ID";

                    lstCollegeSession.DataBind();
                    lstCollegeSession.SelectedIndex = -1;

                    //DropDownList ddlMainTeacher = (DropDownList)itm.FindControl("ddlMainTeacher");
                    //ddlMainTeacher.Items.Clear();
                    //ddlMainTeacher.Items.Add(new ListItem("Please Select", "0"));

                    //ddlMainTeacher.DataSource = dsTeacher;
                    //ddlMainTeacher.DataValueField = "UA_NO";
                    //ddlMainTeacher.DataTextField = "UA_FULLNAME";
                    //ddlMainTeacher.DataBind();



                    //DropDownList ddlAdditionalTeacher = (DropDownList)itm.FindControl("ddlAdditionalTeacher");
                    //ddlAdditionalTeacher.Items.Clear();
                    ////ddlAdditionalTeacher.Items.Add("Please Select");
                    //ddlAdditionalTeacher.Items.Add(new ListItem("Please Select", "0"));
                    //ddlAdditionalTeacher.DataSource = dsTeacher;
                    //ddlAdditionalTeacher.DataValueField = "UA_NO";
                    //ddlAdditionalTeacher.DataTextField = "UA_FULLNAME";
                    //ddlAdditionalTeacher.DataBind();


                    ListBox lstSemester = (ListBox)itm.FindControl("lstSemester");
                    lstSemester.Items.Clear();
                    lstSemester.DataSource = dsSemester;
                    lstSemester.DataValueField = "SEMESTERNO";
                    lstSemester.DataTextField = "SEMESTERNAME";
                    lstSemester.DataBind();
                    lstSemester.SelectedIndex = -1;

                }

                lvCourse.Visible = true;
                pnlCourse.Visible = true;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancel0_Click(object sender, EventArgs e)
    {
        this.clearFields();
    }
    protected void btnAd_Click(object sender, EventArgs e)
    {
        try
        {
            dvNote.Visible = false;
            int userno = 0;
            DataSet ds2 = objCommon.FillDropDown("ACD_GLOBAL_OFFERED_COURSE", "*", "", "", "GLOBAL_OFFER_ID");
            CourseController objCC = new CourseController();
            if (Session["userno"].ToString() != string.Empty)
                userno = int.Parse(Session["userno"].ToString());
            else
                Response.Redirect("~/default.aspx", false);
            CustomStatus cs = CustomStatus.Error;

            Boolean isBranchSelected = false;
            Boolean isCollegeSelected = false;
            Boolean isSemesterSelected = false;
            Boolean isBranchOk = false;
            Boolean isCollegeOk = false;
            Boolean isSemesterOk = false;
            //Boolean isMainteacher = false;
            //Boolean isBranchOk = false;

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkSelect") as CheckBox;
                TextBox txtMaxSeat = (TextBox)dataitem.FindControl("txtMaxSeat");
                Label lblCCODE = (Label)dataitem.FindControl("lblCCODE");

                if (chkBox.Checked && string.IsNullOrEmpty(txtMaxSeat.Text))
                {
                    objCommon.DisplayMessage(updPanel1, "Seats should not be Empty. When you ticked the Select Checkbox For Course Code : " + lblCCODE.Text, this.Page);
                    return;
                }
                ListBox lstBranch = (ListBox)dataitem.FindControl("lstBranch");
                ListBox lstCollegeSession = (ListBox)dataitem.FindControl("lstCollegeSession");
                ListBox lstSemester = (ListBox)dataitem.FindControl("lstSemester");
                //DropDownList ddlmainteacher = (DropDownList)dataitem.FindControl("ddlMainTeacher");

                foreach (ListItem item in lstCollegeSession.Items)
                {
                    if (item.Selected == true)
                    {
                        isCollegeSelected = true;
                        break;
                    }
                }
                foreach (ListItem itemB in lstBranch.Items)
                {
                    if (itemB.Selected == true)
                    {
                        isBranchSelected = true;
                        break;
                    }
                }

                foreach (ListItem itemC in lstSemester.Items)
                {
                    if (itemC.Selected == true)
                    {
                        isSemesterSelected = true;
                        break;
                    }
                }



                if (isBranchSelected)
                {
                    isBranchOk = true;
                    //break;
                }
                else
                {
                    isBranchSelected = false;
                }
                if (isCollegeSelected)
                {
                    isCollegeOk = true;
                    //break;
                }
                else
                {
                    isCollegeSelected = false;
                }
                if (isSemesterSelected)
                {
                    isSemesterOk = true;
                    //break;
                }
                else
                {
                    isSemesterSelected = false;
                }
                //if (ddlmainteacher.SelectedIndex != 0)
                //{
                //    isMainteacher = true;
                //    //break;

                //}
            }

            if (!isBranchOk && !isSemesterOk && !isCollegeOk)
            {
                objCommon.DisplayMessage(updPanel1, "Please Select at least One Offered to Semester, College/Session and Program/Branch for One Course", this.Page);
                return;
            }
            if (!isSemesterOk)
            {
                objCommon.DisplayMessage(updPanel1, "Please Select at least One Offered to Semester for One Course", this.Page);
                return;
            }
            if (!isCollegeOk)
            {
                objCommon.DisplayMessage(updPanel1, "Please Select at least One College/Session for One Course", this.Page);
                return;
            }
            if (!isBranchOk)
            {
                objCommon.DisplayMessage(updPanel1, "Please Select at least One Program/Branch for One Course", this.Page);
                return;
            }
            //if (!isMainteacher)
            //{
            //    objCommon.DisplayMessage(updPanel1, "Please Select Main Teacher", this.Page);
            //    return;
            //}



            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                TextBox txtMaxSeat = (TextBox)dataitem.FindControl("txtMaxSeat");
                ListBox lstBranch = (ListBox)dataitem.FindControl("lstBranch");
                ListBox lstCollegeSession = (ListBox)dataitem.FindControl("lstCollegeSession");
                ListBox lstSemester = (ListBox)dataitem.FindControl("lstSemester");
                string sessiono = string.Empty;
                string branchid = string.Empty;
                string toSem = string.Empty;
                DataSet colnos = null;

                foreach (ListItem itm in lstCollegeSession.Items)
                {
                    if (itm.Selected == true)
                    {
                        sessiono += itm.Value + ',';
                        colnos = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "", "SESSIONNO IN(" + sessiono.Remove(sessiono.Length - 1) + ")", "");
                    }
                }
                if (sessiono != string.Empty)
                {
                    sessiono = sessiono.Substring(0, sessiono.Length - 1);
                }

                foreach (ListItem item in lstBranch.Items)
                {
                    if (item.Selected == true)
                    {
                        branchid += item.Value + ',';
                    }
                }
                if (branchid != string.Empty)
                {
                    branchid = branchid.Substring(0, branchid.Length - 1);
                }


                //foreach (ListItem liSem in chkToSemesterList.Items)
                //{
                //    if (liSem.Selected)
                //    {
                //        toSem += liSem.Value + ",";
                //    }
                //}
                //toSem = toSem.TrimEnd(',');


                foreach (ListItem item1 in lstSemester.Items)
                {
                    if (item1.Selected == true)
                    {
                        toSem += item1.Value + ',';
                    }
                }
                if (toSem != string.Empty)
                {
                    toSem = toSem.Substring(0, toSem.Length - 1);
                }

                string collegeids = string.Empty;
                if (colnos != null && colnos.Tables.Count > 0 && colnos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < colnos.Tables[0].Rows.Count; i++)
                    {
                        collegeids = collegeids + colnos.Tables[0].Rows[i]["COLLEGE_ID"].ToString() + ",";
                    }
                    collegeids = collegeids.Remove(collegeids.Length - 1);
                }

                Label lblCCODE = (Label)dataitem.FindControl("lblCCODE");
                Label lblCrdeit = (Label)dataitem.FindControl("lblCrdeit");
                CheckBox chkActiveStatus = (CheckBox)dataitem.FindControl("chkActiveStatus");

                //DropDownList ddlmainteacher = (DropDownList)dataitem.FindControl("ddlMainTeacher");
                //DropDownList ddladittionalteacher = (DropDownList)dataitem.FindControl("ddlAdditionalTeacher");



                GlobalOfferedCourse objcls = new GlobalOfferedCourse();

                //objcls.MainFacultyno = Convert.ToInt32(ddlmainteacher.SelectedValue);
                //if (ddladittionalteacher.SelectedIndex > 0)
                //{
                //    objcls.AlternateFacultyno = Convert.ToInt32(ddladittionalteacher.SelectedValue);
                //}
                //else
                //{
                //    objcls.AlternateFacultyno = 0;
                //}

                objcls.Ua_no = userno;
                objcls.Global_offered = Convert.ToInt32(chkActiveStatus.Checked);
                objcls.Orgid = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                objcls.College_code = Convert.ToInt32(Session["colcode"].ToString());

                objcls.College_id = Convert.ToInt32(ViewState["college_id"].ToString());

                objcls.DegreeNo = Convert.ToInt32(ViewState["degreeno"].ToString());
                objcls.Schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
                objcls.To_semesterno = toSem;
                //objcls.BranchNo = Convert.ToInt32(branchNo);
                //objcls.SessionNo = Convert.ToInt32(clgSessionNo);
                objcls.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                //objcls.To_semesterno = Convert.ToInt32(ddlToSemester.SelectedValue);
                objcls.Courseno = Convert.ToInt32(lblCCODE.ToolTip);
                objcls.Credits = Convert.ToDouble(lblCrdeit.Text);
                objcls.CCODE = lblCCODE.Text;
                objcls.Capacity = !string.IsNullOrEmpty(txtMaxSeat.Text) ? Convert.ToInt32(txtMaxSeat.Text) : 0; // 0= max seats.
                objcls.GroupId = Convert.ToInt32(ViewState["groupid"]);
                if (sessiono != string.Empty && branchid != string.Empty)
                {
                    if (ViewState["globaledit"].ToString() == "edit")
                    {
                        cs = (CustomStatus)objCC.UpdateGloballyOfferedCourseModified(objcls, sessiono, branchid, collegeids);
                    }
                    else
                    {
                        cs = (CustomStatus)objCC.SaveGloballyOfferedCourseModified(objcls, sessiono, branchid, collegeids);
                    }
                }
                //DataRow[] dr = ds2.Tables[0].Select("COLLEGE_ID = " + objcls.College_id + " AND SESSIONNO =" + objcls.SessionNo + " AND DEGREENO = " + objcls.DegreeNo + " AND SCHEMENO = " + objcls.Schemeno + " AND COURSENO = " + objcls.Courseno + " AND BRANCHNO = " + objcls.BranchNo + " AND TO_SEMESTERNO=" + objcls.To_semesterno);
                //if (dr == null || dr.Count() <= 0)
                //    cs = (CustomStatus)objCC.UpdateGloballyOfferedCourse(objcls);
                //else
                //{
                //    string IsOffered = dr[0][11].ToString();
                //    if (!string.IsNullOrEmpty(IsOffered) && IsOffered == "0")
                //    {
                //        string GlobalOfrCrsID = dr[0][0].ToString();
                //        cs = (CustomStatus)objCC.ActiveGloballyOfferedCourse(Convert.ToInt32(GlobalOfrCrsID), objcls.Capacity);
                //    }
                //}
            }

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListView();
                BindOfferedGlobalCourses();
                objCommon.DisplayMessage(updPanel1, "Offered subjects saved successfully", this.Page);
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                BindOfferedGlobalCourses();
                objCommon.DisplayMessage(updPanel1, "Offered subjects updated successfully", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListView();
        BindOfferedGlobalCourses();
    }

    private void clearFields()
    {
        ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        // ddlToSemester.SelectedIndex = 0;
        //  ddlSession.SelectedIndex = 0;
        pnlCourse.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();

        lvGLobalOfferedCourses.DataSource = null;
        lvGLobalOfferedCourses.DataBind();
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            CheckBox chkBox = dataitem.FindControl("chkSelect") as CheckBox;
            TextBox txtMaxSeat = (TextBox)dataitem.FindControl("txtMaxSeat");
            if (chkBox.Checked)
                txtMaxSeat.Enabled = true;
            else
            {
                txtMaxSeat.Enabled = false;
                txtMaxSeat.Text = string.Empty;
            }
        }
    }
    protected void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            ListBox lstBranch = (ListBox)dataitem.FindControl("lstBranch");
            ListBox lstCollegeSession = (ListBox)dataitem.FindControl("lstCollegeSession");

            string val = lstBranch.SelectedValue;
            if (!string.IsNullOrEmpty(val))
                lstCollegeSession.Enabled = true;
            else
                lstCollegeSession.Enabled = false;
        }
    }
    protected void ddlToSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlToSemester.SelectedValue) > 0)
        //{
        //    courseNo = ViewState["ccode"].ToString();
        //    courseNo = courseNo.Remove(courseNo.Length - 1);
        //    // string whrCond = "G.SCHEMENO=" +  + " and G.TO_SEMESTERNO=" +  + " and G.COURSENO in  (SELECT VALUE FROM DBO.SPLIT('" + courseNo + "',','))"; //" and SESSIONNO="+102+ PKG_ACD_GET_COURSE_BRANCH_SESSION_FORGLOBALCOURSE
        //    DataSet ds3 = objCC.GetCourseBranchSessionForGlobalCourseDetails(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlToSemester.SelectedValue), courseNo); 
        //    string branch_Session_capacity_courseName = string.Empty;
        //    lblNote.Text = string.Empty;
        //    string cName = string.Empty;
        //    if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
        //    {
        //        lblNoteHeader.Text = "Courses are alreaady offered to below Branch and Session Combination.</br>";
        //        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
        //        {
        //            string cNo = ds3.Tables[0].Rows[i][0].ToString();
        //            string courseName = ds3.Tables[0].Rows[i][1].ToString();
        //            string bNo = ds3.Tables[0].Rows[i][2].ToString();
        //            string branch = ds3.Tables[0].Rows[i][3].ToString();
        //            string sNo = ds3.Tables[0].Rows[i][4].ToString();
        //            string Session = ds3.Tables[0].Rows[i][5].ToString();

        //            if (!string.IsNullOrEmpty(cName) && courseName == cName)
        //                branch_Session_capacity_courseName += "{" + branch + " : " + Session + "},   ";
        //            else
        //            {
        //                //"<span style='color: Green'>Fine</span><span style='color: Blue'>and</span><span style='color: Red'>you?</span>";
        //                cName = courseName;
        //                branch_Session_capacity_courseName += "<span style='color: DarkGreen'> " + cName + " :</span> </br> {" + branch + " : " + Session + "}, ";
        //            }
        //            #region Commented Code
        //            //foreach (ListViewDataItem dataitem in lvCourse.Items)
        //            //{
        //            //    CheckBox chkBox = dataitem.FindControl("chkSelect") as CheckBox;
        //            //    TextBox txtMaxSeat = (TextBox)dataitem.FindControl("txtMaxSeat");
        //            //    Label lblCCODE = (Label)dataitem.FindControl("lblCCODE");                      
        //            //    ListBox lstBranch = (ListBox)dataitem.FindControl("lstBranch");
        //            //    ListBox lstCollegeSession = (ListBox)dataitem.FindControl("lstCollegeSession");

        //            //    if (cNo == lblCCODE.ToolTip)
        //            //    {

        //            //        //bool aa = Convert.ToBoolean(lstBranch.Items.FindByValue(bNo));
        //            //        //if (aa) { 
        //            //        //lstBranch.Items.
        //            //        //} itemB.Selected = true;
        //            //        //foreach (ListItem itemB in lstBranch.Items)
        //            //        //{
        //            //        //    if (aa) itemB.Selected = true;
        //            //        //}

        //            //        //foreach (ListItem item in lstCollegeSession.Items)
        //            //        //{
        //            //        //    if (item.Value == sNo) item.Selected = true;
        //            //        //}
        //            //    }                        
        //            //}
        //            #endregion
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(branch_Session_capacity_courseName))
        //    {
        //        dvNote.Visible = true;
        //        lblNote.Text = branch_Session_capacity_courseName.Remove(branch_Session_capacity_courseName.Length - 1);
        //    }
        //    else
        //        dvNote.Visible = false;

        //}
        //else
        //    dvNote.Visible = false;
    }
    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblCCODE = (Label)dataItem.FindControl("lblCCODE");
            ViewState["ccode"] += lblCCODE.ToolTip + ",";
        }
    }

    private void BindOfferedGlobalCourses()
    {
        try
        {
            DataSet ds = null;
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            CourseController objCC = new CourseController();
            ds = objCC.GetOffredGlobalCoursesModified(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvGLobalOfferedCourses.DataSource = ds;
                lvGLobalOfferedCourses.DataBind();
                lvGLobalOfferedCourses.Visible = true;
                pnlGLobalOfferedCourses.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvGLobalOfferedCourses);//Set label - 

            }
            else
            {
                lvGLobalOfferedCourses.DataSource = null;
                lvGLobalOfferedCourses.DataBind();
                lvGLobalOfferedCourses.Visible = false;
                pnlGLobalOfferedCourses.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.BindOfferedGlobalCourses -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        try
        {

            int globalOfferedCourseID = Convert.ToInt32((sender as Button).ToolTip);
            CustomStatus cs = CustomStatus.Error;
            cs = (CustomStatus)objCC.InActiveGlobalOfferedCourses(globalOfferedCourseID);
            if (cs.Equals(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage(updPanel1, "Selected Global Offered subject is In-Activated successfully.", this.Page);
            else
                objCommon.DisplayMessage(updPanel1, "Error Occured.", this.Page);

            BindListView();
            BindOfferedGlobalCourses();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.btnInActive_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lstCollegeSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            ListBox lstCollegeSession = dataitem.FindControl("lstCollegeSession") as ListBox;
            ListBox lstBranch = dataitem.FindControl("lstBranch") as ListBox;
            Label lblCCODE = dataitem.FindControl("lblCCODE") as Label;
            int courseno = Convert.ToInt32(lblCCODE.ToolTip);
            string collegeidnos = string.Empty;
            string sessionno = "";
            DataSet colnos = null;
            foreach (ListItem items in lstCollegeSession.Items)
            {
                if (items.Selected == true)
                {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    sessionno += items.Value + ',';
                    //sessionno = sessionno.Remove(sessionno.Length - 1);
                    //collegeidnos = objCommon.LookUp("ACD_SESSION_MASTER", "COLLEGE_ID", "SESSIONNO IN(" + sessionno.Remove(sessionno.Length - 1) + ")");
                    colnos = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "", "SESSIONNO IN(" + sessionno.Remove(sessionno.Length - 1) + ")", "");
                }
            }
            string collegeids = string.Empty;
            if (colnos != null && colnos.Tables.Count > 0 && colnos.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < colnos.Tables[0].Rows.Count; i++)
                {
                    collegeids = collegeids + colnos.Tables[0].Rows[i]["COLLEGE_ID"].ToString() + ",";
                }
                collegeids = collegeids.Remove(collegeids.Length - 1);
                ViewState["COLLEGEIDS"] = collegeids;
            }


            string brancitemsnos = string.Empty;
            foreach (ListItem branchitems in lstBranch.Items)
            {
                if (branchitems.Selected == true)
                {
                    brancitemsnos = brancitemsnos + branchitems.Value + ",";
                }
            }
            if (brancitemsnos != string.Empty)
            {
                brancitemsnos = brancitemsnos.Remove(brancitemsnos.Length - 1);
            }
            ViewState["brancitemsnos"] = brancitemsnos;
            //if (collegeids != "")
            //{
            //collegeids = collegeids.Remove(collegeids.Length - 1);
            //ViewState["SessionNos"] = collegeids;
            //if (Convert.ToInt32(ViewState["COURSENO"]) == courseno)
            //{
            foreach (ListItem items in lstCollegeSession.Items)
            {
                if (items.Selected == true)
                {
                    DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.BRANCHNO=B.BRANCHNO INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SCH ON (SCH.COLLEGE_ID=CDB.COLLEGE_ID AND CDB.DEGREENO=SCH.DEGREENO AND CDB.BRANCHNO=SCH.BRANCHNO) INNER JOIN ACD_DEGREE D ON CDB.DEGREENO=D.DEGREENO", "DISTINCT CDBNO", "LONGNAME +' ('+SHORTNAME +') - '+D.CODE AS BRANCHNAME", "CDB.COLLEGE_ID IN (" + ViewState["COLLEGEIDS"] + ")", "CDBNO");
                    //DataSet ds = objCommon.FillDropDown("ACD_BRANCH ", "DISTINCT BRANCHNO", "LONGNAME +' ('+SHORTNAME +')' AS BRANCHNAME", "BRANCHNO>0 and BRANCHNO<>" + ViewState["branchno"].ToString(), "BRANCHNO");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            lstBranch.DataTextField = "BRANCHNAME";
                            lstBranch.DataValueField = "CDBNO";
                            //lstBranch.ToolTip = "BRANCHNO";
                            lstBranch.DataSource = ds.Tables[0];
                            lstBranch.DataBind();
                            // ddlDegree.SelectedIndex = 0;
                        }
                    }

                }
            }
            if (ViewState["brancitemsnos"] != null && ViewState["brancitemsnos"] != "")
            {
                string Program = ViewState["brancitemsnos"].ToString();
                string[] subs = Program.Split(',');

                foreach (ListItem branchitems in lstBranch.Items)
                {
                    for (int i = 0; i < subs.Count(); i++)
                    {
                        if (subs[i].ToString().Trim() == branchitems.Value)
                        {
                            branchitems.Selected = true;
                        }
                    }
                }
            }

            //}
            //}
        }
        //ViewState["brancitemsnos"] = null;
    }

    protected void ddlCollegeSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSemesterAT.Items.Clear();
        //ddlSemesterAT.Items.Add(new ListItem("Please Select", "0"));
        //int SessionNo = Convert.ToInt32(ddlCollegeSession.SelectedValue);
        //DataSet ds = objCommon.GetSemesterSessionWise(0, SessionNo,3);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    // ddlSession.SelectedValue = "";
        //    ddlSemesterAT.DataSource = ds;
        //    ddlSemesterAT.DataValueField = ds.Tables[0].Columns[0].ToString();
        //    ddlSemesterAT.DataTextField = ds.Tables[0].Columns[1].ToString();
        //    ddlSemesterAT.DataBind();
        //    //ddlSession.SelectedIndex = 0;
        //}
        if (ddlCollegeSession.SelectedIndex > 0)
        {
            ddlSubjectAT.Items.Clear();
            ddlSubjectAT.Items.Add(new ListItem("Please Select", "0"));
            int SessionNo = Convert.ToInt32(ddlCollegeSession.SelectedValue);
            DataSet ds = objCC.GetGlobalOfferedCourseList_Section(SessionNo, 0, 0, 1, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // ddlSession.SelectedValue = "";
                ddlSubjectAT.DataSource = ds;
                ddlSubjectAT.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSubjectAT.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSubjectAT.DataBind();
                //ddlSession.SelectedIndex = 0;
            }
        }
        else
        {
            ddlSubjectAT.Items.Clear();
            ddlSubjectAT.Items.Add(new ListItem("Please Select", "0"));
            lvStudents.DataSource = null;
            lvStudents.DataBind();
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
    protected void ddlSemesterAT_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        ddlTeacher.Items.Clear();
        ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
        int globalElectiveCTAllotment = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "IS_GLOBAL_ELECTIVE_CT_ALLOTMENT_REQUIRED", "ConfigNo>0"));
        if (globalElectiveCTAllotment == 0)
        {
            objCommon.FillDropDownList(ddlsection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", " ISNULL(ACTIVESTATUS,0)=1 AND SECTIONNO > 0", "SECTIONNO");
        }
        else
        {
            //objCommon.FillDropDownList(ddlsection, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SECTION S ON (CT.SECTIONNO =S.SECTIONNO)", "DISTINCT CT.SECTIONNO", "SECTIONNAME", " SESSIONNO IN (SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND COURSENO  = " + ddlSubjectAT.SelectedValue + " AND CT.SECTIONNO > 0 AND ISNULL(CT.CANCEL,0)=0", "CT.SECTIONNO");
            DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER CT INNER JOIN ACD_SECTION S ON (CT.SECTIONNO =S.SECTIONNO)", "DISTINCT CT.SECTIONNO", "SECTIONNAME", " SESSIONNO IN (SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND COURSENO  = " + ddlSubjectAT.SelectedValue + " AND CT.SECTIONNO > 0 AND ISNULL(CT.CANCEL,0)=0", "CT.SECTIONNO");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlsection.DataSource = ds;
                ddlsection.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlsection.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlsection.DataBind();
            }
            else
            {
                ddlsection.Items.Clear();
                ddlsection.Items.Add(new ListItem("Please Select", "0"));
                objCommon.DisplayMessage(this.UpdatePanel1, "Course Teacher Allotment Not Found for Selected Selection", this.Page);
            }
        }

        this.BindTeacherAllotmentListView();
    }
    protected void btnSubmitAllotment_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student_Acd objStudent = new Student_Acd();
        objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objStudent.CourseNo = Convert.ToInt32(ddlSubjectAT.SelectedValue);
        objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
        string Additionalteacher = "";
        if (Convert.ToInt32(ViewState["globalElectiveCTAllotment"]) == 0)
        {

            foreach (ListItem items in lstAdditionalTeacher.Items)
            {
                if (items.Selected == true)
                {
                    Additionalteacher += items.Value + ',';
                }
            }
            if (Additionalteacher.Length > 0)
            {
                objStudent.AdditionalTeacher = Additionalteacher.Remove(Additionalteacher.Length - 1);
            }
            else
            {
                objStudent.AdditionalTeacher = string.Empty;
            }
            if (Additionalteacher != "")
            {
                objStudent.isAdditionalFlag = 1;
            }
            else
            {
                objStudent.isAdditionalFlag = 0;
            }
        }
        else
        {
            objStudent.AdditionalTeacher = string.Empty;
            objStudent.isAdditionalFlag = 0;
        }

        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            if (chkBox.Checked == true)
                objStudent.StudId += chkBox.ToolTip + ",";
        }

        if (objStudent.StudId.Length <= 0)
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Student.", this.Page);
            return;
        }
        int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
        if (objSC.UpdateStudent_TeachAllotForGlobalElective_Modified(objStudent, OrgId, Convert.ToInt32(ddlsection.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordUpdated))
            objCommon.DisplayMessage(this.UpdatePanel1, "Teacher Alloted Successfully..", this.Page);
        else
            objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);

        this.BindTeacherAllotmentListView();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControlsAT();
    }
    private void ClearControlsAT()
    {
        ddlCollegeSession.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemesterAT.SelectedIndex = 0;
        ddlSubjectAT.SelectedIndex = 0;
        updPanel1.Update();
        //lvCourseTeacher.DataSource = null;
        //lvCourseTeacher.DataBind();
        lvStudents.DataSource = null;
        lvStudents.DataBind();

        UpdatePanel1.Update();

    }

    private void BindTeacherAllotmentListView()
    {
        try
        {
            //Fill Teacher DropDown
            //this.FillTeacher();

            StudentController objSC = new StudentController();
            //DataSet ds = objSC.GetStudentsForTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddltheorypractical.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
            DataSet ds = objSC.GetStudentsForGlobalCourseTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ddlSubjectAT.SelectedValue));
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label - 
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.UpdatePanel1, "Course Registration Not Found for Selected Selection", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }


    protected void ddlSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["TimeSlotTbl"] == null)
        {
            lvTimeSlotDetails.DataSource = null;
            lvTimeSlotDetails.DataBind();
        }
        ddlTimeSlot.Items.Clear();
        ddlTimeSlot.Items.Add(new ListItem("Please Select", "0"));
        string MSG = ddlSubjectTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();
        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();
        //int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON(C.SCHEMENO=S.SCHEMENO)", "DISTINCT S.DEGREENO", "COURSENO=" + Convert.ToInt32(ddlSubjectTimetable.SelectedValue)));
        if (ddlSlotType.SelectedIndex > 0)
        {
            //DataSet ds = objCommon.FillDropDown("ACD_SCHEME S INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO AND S.DEPTNO=CDB.DEPTNO) INNER JOIN ACD_COURSE C ON(C.SCHEMENO=S.SCHEMENO)", "CDB.COLLEGE_ID", "S.DEGREENO", "C.COURSENO=" + Convert.ToInt32(ddlSubjectTimetable.SelectedValue), "C.COURSENO");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //  objCommon.FillDropDownList(ddlTimeSlot, "ACD_TIME_SLOT T", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " and ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlSlotType.SelectedValue), "T.SLOTNO");

            //objCommon.FillDropDownList(ddlTimeSlot, "ACD_TIME_SLOT T INNER JOIN ACD_GLOBAL_OFFERED_COURSE GOC ON(T.COLLEGE_ID = GOC.COLLEGE_ID AND T.DEGREENO = GOC.DEGREENO)", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlSlotType.SelectedValue) + " AND GOC.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(ddlSessionTimeTable.SelectedValue) + ")", "T.SLOTNO");
            objCommon.FillDropDownList(ddlTimeSlot, "ACD_TIME_SLOT T INNER JOIN ACD_GLOBAL_OFFERED_COURSE GOC ON(T.COLLEGE_ID = GOC.COLLEGE_ID AND T.DEGREENO = GOC.DEGREENO) INNER JOIN ACD_SCHEME S ON(T.DEGREENO = S.DEGREENO)", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlSlotType.SelectedValue) + " AND GOC.COURSENO = " + courseno + " AND GOC.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(ddlSessionTimeTable.SelectedValue) + ")", "T.SLOTNO");

            //}
            //else
            //{
            //    ddlTimeSlot.Items.Clear();
            //    ddlTimeSlot.Items.Add(new ListItem("Please Select", "0"));

            //}
        }
        else
        {
            ddlTimeSlot.Items.Clear();
            ddlTimeSlot.Items.Add(new ListItem("Please Select", "0"));

        }

    }
    protected void ddlTimeSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRoomDropdown();

        if (Session["TimeSlotTbl"] == null)
        {
            lvTimeSlotDetails.DataSource = null;
            lvTimeSlotDetails.DataBind();
        }
        ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
    }

    private void FillRoomDropdown()
    {
        ddlRoom.Items.Clear();
        ddlRoom.Items.Add(new ListItem("Please Select", "0"));
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_ACADEMIC_ROOMMASTER RM INNER JOIN ACD_FLOOR_MASTER FM ON(RM.FLOORNO=FM.FLOORNO)", "ROOMNO", "CONCAT(ROOMNAME,'-',FLOORNAME) AS ROOMNAME", "ISNULL(RM.ACTIVESTATUS,0)=1", "ROOMNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRoom.DataSource = ds;
            ddlRoom.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlRoom.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlRoom.DataBind();
        }
        else
        {
            ddlRoom.Items.Clear();
            ddlRoom.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void btnClearTimeSlot_Click(object sender, EventArgs e)
    {
        ClearControls_TimeSlotDetails();
    }
    protected void btnAddTimeSlot_Click(object sender, EventArgs e)
    {
        string MSG = ddlSubjectTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();

        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();

        if (Session["TimeSlotTbl"] != null && ((DataTable)Session["TimeSlotTbl"]).Rows.Count > 0 && ((DataTable)Session["TimeSlotTbl"]) != null)
        {
            DataTable dt = (DataTable)Session["TimeSlotTbl"];
            //DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            //DataRow [] dr1;
            if (btnAddTimeSlot.Text != "Update")
            {
                string expression = string.Empty;
                expression = "DAYNO=" + ddlAllDay.SelectedValue + " AND SLOTNO=" + ddlTimeSlot.SelectedValue + " AND (ROOMNO=" + ddlRoom.SelectedValue + " OR " + ddlRoom.SelectedValue + "=0)";
                DataRow[] dr1 = dt.Select(expression);
                //dr1 = dt.Rows.Find(expression);
                if (dr1.Length > 0)
                {
                    lvTimeSlotDetails.DataSource = dt;
                    lvTimeSlotDetails.DataBind();
                    //ClearControls_QualDetails();
                    objCommon.DisplayMessage(this, "Day, Time Slot and Room already selected!", this.Page);
                    return;
                }
            }
            if (ddlAllDay.SelectedIndex > 0 && ddlTimeSlot.SelectedIndex > 0)
            {
                dr["SRNO"] = Convert.ToInt32(lvTimeSlotDetails.Items.Count) + 1;
                dr["DAYNO"] = Convert.ToInt32(ddlAllDay.SelectedValue);
                dr["DAYNAME"] = ddlAllDay.SelectedItem.Text;
                dr["SLOTNO"] = Convert.ToInt32(ddlTimeSlot.SelectedValue);
                dr["SLOTNAME"] = ddlTimeSlot.SelectedItem.Text;
                dr["ROOMNO"] = Convert.ToInt32(ddlRoom.SelectedValue);
                dr["ROOMNAME"] = ddlRoom.SelectedItem.Text;
                dr["COURSENO"] = Convert.ToInt32(courseno);
                dt.Rows.Add(dr);
                Session["TimeSlotTbl"] = dt;
                lvTimeSlotDetails.DataSource = dt;
                lvTimeSlotDetails.DataBind();
                ClearControls_TimeSlotDetails();
                // objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select Day and Time Slot", this.Page);
            }
        }

        else
        {
            DataTable dt = this.GetTimeSlotDetailsDataTable();
            DataRow dr = dt.NewRow();

            if (ddlAllDay.SelectedIndex > 0 && ddlTimeSlot.SelectedIndex > 0)
            {
                dr["SRNO"] = Convert.ToInt32(lvTimeSlotDetails.Items.Count) + 1;
                dr["DAYNO"] = Convert.ToInt32(ddlAllDay.SelectedValue);
                dr["DAYNAME"] = ddlAllDay.SelectedItem.Text;
                dr["SLOTNO"] = Convert.ToInt32(ddlTimeSlot.SelectedValue);
                dr["SLOTNAME"] = ddlTimeSlot.SelectedItem.Text;
                dr["ROOMNO"] = Convert.ToInt32(ddlRoom.SelectedValue);
                dr["ROOMNAME"] = ddlRoom.SelectedItem.Text;
                dr["COURSENO"] = Convert.ToInt32(courseno);
                dt.Rows.Add(dr);
                Session["TimeSlotTbl"] = dt;
                lvTimeSlotDetails.DataSource = dt;
                lvTimeSlotDetails.DataBind();
                ClearControls_TimeSlotDetails();
                // objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
            }
            else
            {
                if (Session["TimeSlotTbl"] == null)
                {
                    lvTimeSlotDetails.DataSource = null;
                    lvTimeSlotDetails.DataBind();
                }
                objCommon.DisplayMessage(this, " Please enter all details.", this.Page);

            }


        }

        btnAddTimeSlot.Text = "Add";
        ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
    }
    private DataTable GetTimeSlotDetailsDataTable()
    {
        DataTable objTimeSlot = new DataTable();
        objTimeSlot.Columns.Add(new DataColumn("SRNO", typeof(int)));
        objTimeSlot.Columns.Add(new DataColumn("DAYNO", typeof(int)));
        objTimeSlot.Columns.Add(new DataColumn("DAYNAME", typeof(string)));
        objTimeSlot.Columns.Add(new DataColumn("SLOTNO", typeof(int)));
        objTimeSlot.Columns.Add(new DataColumn("SLOTNAME", typeof(string)));
        objTimeSlot.Columns.Add(new DataColumn("ROOMNO", typeof(int)));
        objTimeSlot.Columns.Add(new DataColumn("ROOMNAME", typeof(string)));
        objTimeSlot.Columns.Add(new DataColumn("COURSENO", typeof(int)));


        return objTimeSlot;
    }

    private void ClearControls_TimeSlotDetails()
    {
        ddlTimeSlot.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
        ddlAllDay.SelectedIndex = 0;
        if (Session["TimeSlotTbl"] == null)
        {
            lvTimeSlotDetails.DataSource = null;
            lvTimeSlotDetails.DataBind();
        }

    }
    protected void btnEditTimeSlotDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillRoomDropdown();
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            //DataTable dt1;//***************
            if (btnAddTimeSlot.Text != "Update")
            {
                if (Session["TimeSlotTbl"] != null && ((DataTable)Session["TimeSlotTbl"]) != null)
                {
                    dt = ((DataTable)Session["TimeSlotTbl"]);
                    //dt1 = dt.Copy();//**********************************
                    DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                    //DataRow dr = this.GetEditableDataRow(dt1, btnEdit.CommandArgument);//**********

                    ddlAllDay.SelectedValue = dr["DAYNO"].ToString();
                    ddlTimeSlot.SelectedValue = dr["SLOTNO"].ToString();
                    string roomno = dr["ROOMNO"].ToString();
                    ddlRoom.SelectedValue = roomno;

                    dt.Rows.Remove(dr);
                    Session["TimeSlotTbl"] = dt;
                    this.BindListView_TImeSlotDetails(dt);
                    btnAddTimeSlot.Text = "Update";
                }
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnEditTimeSlotDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDeleteTimeSlotDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;

            DataTable dt;
            if (Session["TimeSlotTbl"] != null && ((DataTable)Session["TimeSlotTbl"]) != null)
            {
                dt = ((DataTable)Session["TimeSlotTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["TimeSlotTbl"] = dt;
                this.BindListView_TImeSlotDetails(dt);
                // objCommon.DisplayMessage(this, "Data deleted successfully!", this.Page);
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnDeleteTimeSlotDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }
    private void BindListView_TImeSlotDetails(DataTable dt)
    {
        try
        {
            lvTimeSlotDetails.DataSource = dt;
            lvTimeSlotDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancelTimeTable_Click(object sender, EventArgs e)
    {
        ClearControlsTimeTable();
    }
    protected void btnSubmitTimeTable_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["TimeSlotTbl"] == null)
        {
            objCommon.DisplayMessage(this.UpdatePanel2, "Please Add Time Table Slots !", this.Page);
            return;
        }
        dt = (DataTable)Session["TimeSlotTbl"];

        GlobalOfferedCourse objGOC = new GlobalOfferedCourse();
        //GlobalTimeTable[] Gtimetable = null;
        //this.BindTimetableSlotDetails(ref Gtimetable);
        //objGOC.Globaltimetable = Gtimetable;
        string MSG = ddlSubjectTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();

        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();
        string facultyno = repoarray[1].ToString();
        string alternateflag = repoarray[2].ToString();

        objGOC.Courseno = Convert.ToInt32(courseno);
        objGOC.MainFacultyno = Convert.ToInt32(facultyno);
        //objGOC.CTNO = Convert.ToInt32(ddlSubjectTimetable.SelectedValue);
        objGOC.SlotType = Convert.ToInt32(ddlSlotType.SelectedValue);
        objGOC.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
        objGOC.Orgid = Convert.ToInt32(Session["OrgId"]);
        objGOC.Ua_no = Convert.ToInt32(Session["userno"]);
        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage(this.UpdatePanel2, "Please select Start Date End Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];

        DateTime dtStartDate = DateTime.Parse(StartDate);
        string SDate = dtStartDate.ToString("yyyy/MM/dd");
        DateTime dtEndDate = DateTime.Parse(EndDate);
        string EDate = dtEndDate.ToString("yyyy/MM/dd");

        CustomStatus cs = (CustomStatus)objAttC.GlobalElective_TimeTableCreate(dt, objGOC, SDate, EDate, alternateflag, Convert.ToInt32(ddlTTSection.SelectedValue));

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.UpdatePanel2, "Time table added successfully", this.Page);
            BindGlobalTimeTable(Convert.ToInt32(ddlSessionTimeTable.SelectedValue), 0, 0);
            ClearAfterSaveControlsTimeTable();

        }
    }
    private void ClearControlsTimeTable()
    {
        ddlSessionTimeTable.SelectedIndex = 0;
        ddlTTSection.SelectedIndex = 0;
        ddlCollegeSchemeTimeTable.SelectedIndex = 0;
        ddlSubjectTimetable.SelectedIndex = 0;
        ddlSlotType.SelectedIndex = 0;
        Session["TimeSlotTbl"] = null;
        lvTimeSlotDetails.DataSource = null;
        lvTimeSlotDetails.DataBind();
        lvGlobalTimeTable.DataSource = null;
        lvGlobalTimeTable.DataBind();
    }
    private void ClearAfterSaveControlsTimeTable()
    {
        ddlTTSection.SelectedIndex = 0;
        ddlSubjectTimetable.SelectedIndex = 0;
        ddlSlotType.SelectedIndex = 0;
        Session["TimeSlotTbl"] = null;
        lvTimeSlotDetails.DataSource = null;
        lvTimeSlotDetails.DataBind();
    }
    private void BindTimetableSlotDetails(ref GlobalTimeTable[] Gtimetable)
    {
        DataTable dt;
        if (Session["TimeSlotTbl"] != null && ((DataTable)Session["TimeSlotTbl"]) != null)
        {
            int index = 0;
            dt = (DataTable)Session["TimeSlotTbl"];
            Gtimetable = new GlobalTimeTable[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                GlobalTimeTable objGlobalTT = new GlobalTimeTable();

                objGlobalTT.DayNo = Convert.ToInt32(dr["DAYNO"]);
                objGlobalTT.SlotNo = Convert.ToInt32(dr["SLOTNO"]);
                objGlobalTT.RoomNo = Convert.ToInt32(dr["ROOMNO"]);
                Gtimetable[index] = objGlobalTT;
                index++;
            }
        }
    }
    protected void ddlCollegeSchemeTimeTable_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCollegeSchemeTimeTable.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeSchemeTimeTable.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }

            int count = Convert.ToInt32(objCommon.LookUp("ACD_GLOBAL_OFFERED_COURSE AS S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_ATTENDANCE_CONFIG AC ON(AC.SESSIONNO = S.SESSIONNO AND AC.COLLEGE_ID = S.COLLEGE_ID)", "COUNT(1)", "S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND ISNULL(S.GLOBAL_OFFERED,0) = 1 AND ISNULL(SM.FLOCK,0)=1 AND ISNULL(SM.IS_ACTIVE,0)=1 AND ISNULL(AC.GLOBAL_ELECTIVE,0)=1 AND ISNULL(AC.ACTIVE,0) =1"));
            if (count == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel2, "Kindly do the Attedance Configuration.", this.Page);
                ddlSubjectTimetable.Items.Clear();
                ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
                lvGlobalTimeTable.DataSource = null;
                lvGlobalTimeTable.DataBind();
                lvGlobalTimeTable.Visible = false;
                pnlGLobalOfferedCourses.Visible = false;
                return;
            }
            else
            {
                ddlSubjectTimetable.Items.Clear();
                ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
                int SessionNo = Convert.ToInt32(ViewState["schemeno"]);
                BindCourseDropdown(SessionNo);
                //BindGlobalTimeTable();
            }
        }
        else
        {
            ddlSubjectTimetable.Items.Clear();
            ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));


        }
        Session["TimeSlotTbl"] = null;
        lvTimeSlotDetails.DataSource = null;
        lvTimeSlotDetails.DataBind();
    }

    public void BindCourseDropdown(int Schemeno)
    {
        DataSet ds1 = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(ViewState["schemeno"]), 0, 0, 3, 0);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            // ddlSession.SelectedValue = "";
            ddlSubjectTimetable.DataSource = ds1;
            ddlSubjectTimetable.DataValueField = ds1.Tables[0].Columns[0].ToString();
            ddlSubjectTimetable.DataTextField = ds1.Tables[0].Columns[1].ToString();
            ddlSubjectTimetable.DataBind();
            //ddlSession.SelectedIndex = 0;
        }
    }

    private void BindGlobalTimeTable(int sessionno, int courseno, int ua_no)
    {
        try
        {
            DataSet ds = null;
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            CourseController objCC = new CourseController();
            ds = objCC.GetGlobalCoursesTimeTableModified(sessionno, courseno, ua_no, Convert.ToInt32(ddlTTSection.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvGlobalTimeTable.DataSource = ds;
                lvGlobalTimeTable.DataBind();
                lvGlobalTimeTable.Visible = true;
                pnlGLobalOfferedCourses.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvGlobalTimeTable);//Set label - 

            }
            else
            {
                lvGlobalTimeTable.DataSource = null;
                lvGlobalTimeTable.DataBind();
                lvGlobalTimeTable.Visible = false;
                pnlGLobalOfferedCourses.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.BindGlobalTimeTable -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditTimeTable_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        //HiddenField hdfAlternate = sender as HiddenField;
        //HiddenField hdfStartEndDate = sender as HiddenField;
        int FacultyNo = Convert.ToInt32(btnEdit.CommandArgument);
        int Courseno = int.Parse(btnEdit.AlternateText);
        int alternateflag = int.Parse(btnEdit.ToolTip);
        ViewState["FacultyNo"] = int.Parse(btnEdit.CommandArgument);
        ViewState["alternateflag"] = int.Parse(btnEdit.ToolTip);
        ViewState["edit"] = "edit";
        string startenddate = btnEdit.CommandName;
        this.ShowDetails(FacultyNo, alternateflag, startenddate, Courseno);
    }
    private void ShowDetails(int FacultyNo, int alternateflag, string startenddate, int Courseno)
    {
        //int courseno=0;
        //int FacultyNo = 0;
        //string[] splitfaculty = Faculty.Split('-');
        //courseno = Convert.ToInt32(splitfaculty[0].Trim());
        //FacultyNo = Convert.ToInt32(splitfaculty[1].Trim());

        SqlDataReader dr = objCC.GetGlobalCoursesTimeTableEditModified(FacultyNo, alternateflag, Convert.ToInt32(ddlSessionTimeTable.SelectedValue), Courseno);
        if (dr != null)
        {
            if (dr.Read())
            {
                //hdnDate.Value = dr["START_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["START_DATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(dr["END_DATE"].ToString()).ToString("MMM dd, yyyy") : Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy");

                hdnDate.Value = dr["START_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["START_DATE"].ToString()).ToString("dd MMM, yyyy") + " - " + Convert.ToDateTime(dr["END_DATE"].ToString()).ToString("dd MMM, yyyy") : Convert.ToDateTime(DateTime.Now).ToString("dd MMM, yyyy") + " - " + Convert.ToDateTime(DateTime.Now).ToString("dd MMM, yyyy");
                ddlCollegeSchemeTimeTable.SelectedValue = dr["COSCHNO"].ToString();
                ddlSessionTimeTable.SelectedValue = dr["SESSIONID"].ToString();
                BindCourseDropdownTimeTable(Convert.ToInt32(ddlSessionTimeTable.SelectedValue));
                ddlSlotType.SelectedValue = dr["SLOTTYPE"].ToString();
                if (ddlSlotType.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlTimeSlot, "ACD_TIME_SLOT T INNER JOIN ACD_GLOBAL_OFFERED_COURSE GOC ON(T.COLLEGE_ID = GOC.COLLEGE_ID AND T.DEGREENO = GOC.DEGREENO)", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlSlotType.SelectedValue) + " AND GOC.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(ddlSessionTimeTable.SelectedValue) + ")", "T.SLOTNO");
                    //objCommon.FillDropDownList(ddlTimeSlot, "ACD_TIME_SLOT T", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " and ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlSlotType.SelectedValue), "T.SLOTNO");
                }
                else
                {
                    ddlTimeSlot.Items.Clear();
                    ddlTimeSlot.Items.Add(new ListItem("Please Select", "0"));
                }
                ddlSubjectTimetable.SelectedValue = dr["FACULTYNO"].ToString();
                string startDate = string.Empty; string endDate = string.Empty;
                string[] ttDates = startenddate.Split('-');
                startDate = ttDates[0].Trim();
                endDate = ttDates[1].Trim();
                DataSet ds = objCC.GetGlobalCoursesTimeTableDetailsSectionModified(FacultyNo, alternateflag, Convert.ToInt32(ddlSessionTimeTable.SelectedValue), startDate, endDate, Courseno);
                DataTable dt = ds.Tables[0];
                Session["TimeSlotTbl"] = dt;
                BindListView_TImeSlotDetails(dt);
                //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
        }
    }
    protected void lvGlobalTimeTable_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ImageButton FacultyNo = dataitem.FindControl("btnEditTimeTable") as ImageButton;
        HiddenField alternateflag = dataitem.FindControl("hdfalternateflag") as HiddenField;
        //Label lblsessionnm = dataitem.FindControl("lblSessionname") as Label;
        HiddenField hdfStartEndDate = dataitem.FindControl("hdfStartEndDate") as HiddenField;

        int facultyno = Convert.ToInt32(FacultyNo.CommandArgument);
        int courseno = Convert.ToInt32(FacultyNo.AlternateText);
        int alternate = Convert.ToInt32(alternateflag.Value);
        string startendate = hdfStartEndDate.Value;
        string[] ttDates = startendate.Split('-');
        string startDate; string endDate;
        startDate = ttDates[0].Trim();
        endDate = ttDates[1].Trim();
        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {

            DataSet ds = objCC.GetGlobalCoursesTimeTableDetailsSectionModified(facultyno, alternate, Convert.ToInt32(ddlSessionTimeTable.SelectedValue), startDate, endDate, courseno);
            lv.DataSource = ds;
            lv.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.GetGlobalCoursesTimeTableDetailsSection -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollegeSchemeAttConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeSchemeAttConfig.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeSchemeAttConfig.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["attdegreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["attbranchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["attcollege_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["attschemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            objCommon.FillListBox(lstSemesterAttConfig, "ACD_GLOBAL_OFFERED_COURSE AS s CROSS APPLY STRING_SPLIT(s.TO_SEMESTERNO, ',') AS f INNER JOIN ACD_SEMESTER AS c ON f.value = c.SEMESTERNO", "C.SEMESTERNO", "C.SEMESTERNAME", "s.SCHEMENO=" + Convert.ToInt32(ViewState["attschemeno"]) + " AND isnull(S.GLOBAL_OFFERED,0) = 1 GROUP BY C.SEMESTERNO,C.SEMESTERNAME", "C.SEMESTERNO");
        }

    }
    protected void btnSunmitAttConfig_Click(object sender, EventArgs e)
    {
        try
        {
            int srno = 0;
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
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

                    objAttModel.AttendanceStartDate = Convert.ToDateTime(txtStartDate.Text);
                    objAttModel.AttendanceEndDate = Convert.ToDateTime(txtEndDate.Text);
                    objAttModel.AttendanceLockDay = Convert.ToInt32(txtAttLockDay.Text);
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
    private void ClearAttConfigControls()
    {
        lstSemesterAttConfig.ClearSelection();
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtAttLockDay.Text = string.Empty;
        //txtAttLockHrs.Text = string.Empty;
        //rdoSMSNo.Checked = true;
        //rdoEmailNo.Checked = true;
        //rblCRegAfter.Checked = true;
        ViewState["attaction"] = null;
        ddlCollegeSchemeAttConfig.SelectedIndex = 0;
        ddlSessionAttConfig.SelectedIndex = 0;
    }

    protected void btnCancelAttConfig_Click(object sender, EventArgs e)
    {
        ClearAttConfigControls();
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


                    //ddlCollegeSchemeAttConfig.SelectedValue = dr["COSCHNO"] == DBNull.Value ? "0" : dr["COSCHNO"].ToString();
                    ddlSessionAttConfig.SelectedValue = dr["SESSIONID"] == DBNull.Value ? "0" : dr["SESSIONID"].ToString();

                    txtStartDate.Text = dr["START_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["START_DATE"].ToString()).ToString("dd/MM/yyyy");
                    txtEndDate.Text = dr["END_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["END_DATE"].ToString()).ToString("dd/MM/yyyy");
                    txtAttLockDay.Text = dr["LOCK_ATT_DAYS"] == null ? string.Empty : dr["LOCK_ATT_DAYS"].ToString();

                    //txtAttLockHrs.Text = dr["LOCK_ATT_HOURS"] == null ? string.Empty : dr["LOCK_ATT_HOURS"].ToString();
                    ViewState["sms"] = dr["SMS_FACILITY"].ToString();
                    if (dr["SMS_FACILITY"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatSms(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src2", "SetStatSms(false);", true);
                        //hfdSms.Value = "0";
                    }

                    if (dr["EMAIL_FACILITY"].ToString() == "Yes")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src3", "SetStatEmail(true);", true);
                        //ScriptManager.RegisterClientScriptBlock(updpnl, updpnl.GetType(), "script3", "SetStatEmail(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src4", "SetStatEmail(false);", true);
                        // ScriptManager.RegisterClientScriptBlock(updpnl, updpnl.GetType(), "script4", "SetStatEmail(false);", true);
                    }

                    if (dr["CREG_STATUS"].ToString() == "Before")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src5", "SetStatCourse(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src5", "SetStatCourse(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "src6", "SetStatCourse(false);", true);
                        // ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src6", "SetStatCourse(false);", true);
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Src", "SetStatCourse()", true);
                    }

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
    //protected void ddlMainTeacher_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)sender;
    //    ListViewItem item = (ListViewItem)ddl.NamingContainer;
    //    DropDownList ddlAdditionalTeacher = (DropDownList)item.FindControl("ddlAdditionalTeacher");


    //    // Bind ddl2 based on the selected value of ddl1
    //    int selectedValue = Convert.ToInt32(ddl.SelectedValue);
    //    DataSet dsTeacher = objCommon.FillDropDown("User_Acc", "UA_NO", "UA_FULLNAME", Convert.ToInt32(ViewState["BOS_DEPTNO"]) + " IN (select value from DBO.Split(UA_DEPTNO,',')) AND UA_TYPE=3 AND UA_NO !=" + selectedValue, "UA_FULLNAME");

    //    ddlAdditionalTeacher.Items.Clear();
    //    ddlAdditionalTeacher.Items.Add(new ListItem("Please Select", "0"));
    //    ddlAdditionalTeacher.DataSource = dsTeacher;
    //    ddlAdditionalTeacher.DataValueField = "UA_NO";
    //    ddlAdditionalTeacher.DataTextField = "UA_FULLNAME";
    //    ddlAdditionalTeacher.DataBind();

    //}
    protected void ddlSubjectAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        ddlTeacher.Items.Clear();
        ddlTeacher.Items.Add(new ListItem("Please Select", "0"));

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int groupid = int.Parse(btnEdit.CommandArgument);
            int courseno = int.Parse(btnEdit.ToolTip);
            ViewState["groupid"] = int.Parse(btnEdit.CommandArgument);
            ViewState["courseno"] = int.Parse(btnEdit.ToolTip);
            ViewState["globaledit"] = "edit";

            this.BindOfferedGlobalCoursesEdit(groupid, courseno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindOfferedGlobalCoursesEdit(int groupid, int courseno)
    {
        try
        {
            CourseController objCC = new CourseController();
            DataSet ds = objCC.GetOffredGlobalCoursesDetailsForEdit(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), groupid, courseno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();

                int capacity = Convert.ToInt32(ds.Tables[0].Rows[0]["CAPACITY"]);
                ViewState["sessionitemnos"] = Convert.ToString(ds.Tables[0].Rows[0]["SESSIONNO"]).ToString();
                ViewState["branchitemsnos"] = Convert.ToString(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["collegeitemnos"] = Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["semetseritemnos"] = Convert.ToString(ds.Tables[0].Rows[0]["TO_SEMESTERNO"]).ToString();
                int activeStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["GLOBAL_OFFERED"]);

                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label - 
                //DataSet dsbranch = objCommon.FillDropDown("ACD_BRANCH ", "DISTINCT BRANCHNO", "LONGNAME +' ('+SHORTNAME +')' AS BRANCHNAME", "BRANCHNO>0 ", "BRANCHNO");
                DataSet dsbranch = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.BRANCHNO=B.BRANCHNO INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SCH ON (SCH.COLLEGE_ID=CDB.COLLEGE_ID AND CDB.DEGREENO=SCH.DEGREENO AND CDB.BRANCHNO=SCH.BRANCHNO) INNER JOIN ACD_DEGREE D ON CDB.DEGREENO=D.DEGREENO", "DISTINCT CDBNO", "LONGNAME +' ('+SHORTNAME +') - '+D.CODE AS BRANCHNAME", "CDB.COLLEGE_ID IN (" + ViewState["collegeitemnos"] + ")", "CDBNO");

                DataSet dsCollegeSession = objCommon.FillDropDown("ACD_SESSION_MASTER SM INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "CM.COLLEGE_ID,SESSION_NAME,COLLEGE_NAME,CONCAT (COLLEGE_NAME , '-',SESSION_NAME) AS COLLEGE_SESSION ", "ISNULL(FLOCK,0)=1 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO,CM.COLLEGE_ID DESC");

                DataSet dsSemester = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <>0", "SEMESTERNO");


                foreach (ListViewDataItem itm in lvCourse.Items)
                {
                    CheckBox chkSelect = (CheckBox)itm.FindControl("chkSelect");
                    TextBox txtMaxSeat = (TextBox)itm.FindControl("txtMaxSeat");
                    CheckBox chkActivestatus = (CheckBox)itm.FindControl("chkActiveStatus");
                    if (capacity > 0)
                    {
                        chkSelect.Checked = true;
                        txtMaxSeat.Text = capacity.ToString();
                        txtMaxSeat.Enabled = true;
                    }
                    else
                    {
                        chkSelect.Checked = false;
                        txtMaxSeat.Text = "";
                        txtMaxSeat.Enabled = false;
                    }

                    if (activeStatus == 1)
                    {
                        chkActivestatus.Checked = true;
                    }
                    else
                    {
                        chkActivestatus.Checked = false;
                    }


                    ListBox lstCollegeSession = (ListBox)itm.FindControl("lstCollegeSession");
                    lstCollegeSession.Items.Clear();
                    lstCollegeSession.DataSource = dsCollegeSession;
                    lstCollegeSession.DataValueField = "SESSIONNO";
                    lstCollegeSession.DataTextField = "COLLEGE_SESSION";
                    lstCollegeSession.DataBind();
                    lstCollegeSession.SelectedIndex = -1;

                    ListBox lstBranch = (ListBox)itm.FindControl("lstBranch");
                    lstBranch.Items.Clear();
                    lstBranch.DataSource = dsbranch;
                    lstBranch.DataValueField = "CDBNO";
                    lstBranch.DataTextField = "BRANCHNAME";
                    lstBranch.DataBind();
                    lstBranch.SelectedIndex = -1;

                    ListBox lstSemester = (ListBox)itm.FindControl("lstSemester");
                    lstSemester.Items.Clear();
                    lstSemester.DataSource = dsSemester;
                    lstSemester.DataValueField = "SEMESTERNO";
                    lstSemester.DataTextField = "SEMESTERNAME";
                    lstSemester.DataBind();
                    lstSemester.SelectedIndex = -1;


                    if (ViewState["sessionitemnos"] != null && ViewState["sessionitemnos"] != "")
                    {
                        string Sessionnos = ViewState["sessionitemnos"].ToString();
                        string[] subs = Sessionnos.Split(',');

                        foreach (ListItem sessionsitems in lstCollegeSession.Items)
                        {
                            for (int i = 0; i < subs.Count(); i++)
                            {
                                if (subs[i].ToString().Trim() == sessionsitems.Value)
                                {
                                    sessionsitems.Selected = true;
                                }
                            }
                        }
                    }
                    if (ViewState["semetseritemnos"] != null && ViewState["semetseritemnos"] != "")
                    {
                        string Semesterno = ViewState["semetseritemnos"].ToString();
                        string[] subs = Semesterno.Split(',');

                        foreach (ListItem semesteritems in lstSemester.Items)
                        {
                            for (int i = 0; i < subs.Count(); i++)
                            {
                                if (subs[i].ToString().Trim() == semesteritems.Value)
                                {
                                    semesteritems.Selected = true;
                                }
                            }
                        }
                    }

                    if (ViewState["branchitemsnos"] != null && ViewState["branchitemsnos"] != "")
                    {
                        string Program = ViewState["branchitemsnos"].ToString();
                        string[] subs = Program.Split(',');

                        foreach (ListItem branchitems in lstBranch.Items)
                        {
                            for (int i = 0; i < subs.Count(); i++)
                            {
                                if (subs[i].ToString().Trim() == branchitems.Value)
                                {
                                    branchitems.Selected = true;
                                }
                            }
                        }
                    }


                }

                lvCourse.Visible = true;
                pnlCourse.Visible = true;
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
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {

            ddlSubjectAT.Items.Clear();
            ddlSubjectAT.Items.Add(new ListItem("Please Select", "0"));
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            DataSet ds = objCC.GetGlobalOfferedCourseList_Section(SessionNo, 0, 0, 5, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // ddlSession.SelectedValue = "";
                ddlSubjectAT.DataSource = ds;
                ddlSubjectAT.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSubjectAT.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSubjectAT.DataBind();
                //ddlSession.SelectedIndex = 0;
            }
        }
        else
        {

            ddlSubjectAT.Items.Clear();
            ddlSubjectAT.Items.Add(new ListItem("Please Select", "0"));
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lstAdditionalTeacher.ClearSelection();
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTeacher.SelectedIndex > 0)
        {
            objCommon.FillListBox(lstAdditionalTeacher, "USER_ACC U", "DISTINCT U.UA_NO", "UA_FULLNAME", "ISNULL(U.UA_STATUS,0) = 0 AND ISNULL(U.UA_TYPE,0)=3 AND (U.UA_DEPTNO IS NOT NULL OR U.UA_DEPTNO <> '' OR U.UA_DEPTNO <> 0) AND U.UA_NO NOT IN(" + Convert.ToString(ddlTeacher.SelectedValue) + ")", "U.UA_FULLNAME");
        }
        else
        {
            lstAdditionalTeacher.ClearSelection();
        }
    }
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
    protected void ddlSessionTimeTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionTimeTable.SelectedIndex > 0)
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_GLOBAL_OFFERED_COURSE AS S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_ATTENDANCE_CONFIG AC ON(AC.SESSIONNO = S.SESSIONNO AND AC.COLLEGE_ID = S.COLLEGE_ID)", "COUNT(1)", "SM.SESSIONID =" + Convert.ToInt32(ddlSessionTimeTable.SelectedValue) + " AND ISNULL(S.GLOBAL_OFFERED,0) = 1 AND ISNULL(AC.GLOBAL_ELECTIVE,0)=1 AND ISNULL(AC.ACTIVE,0) =1"));
            if (count == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel2, "Kindly do the Attedance Configuration.", this.Page);
                ddlSubjectTimetable.Items.Clear();
                ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
                lvGlobalTimeTable.DataSource = null;
                lvGlobalTimeTable.DataBind();
                lvGlobalTimeTable.Visible = false;
                pnlGLobalOfferedCourses.Visible = false;
                return;
            }
            else
            {
                ddlSubjectTimetable.Items.Clear();
                ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
                int SessionNo = Convert.ToInt32(ddlSessionTimeTable.SelectedValue);
                //BindCourseDropdownTimeTable(SessionNo);
                BindGlobalTimeTable(SessionNo, 0, 0);
                objCommon.FillDropDownList(ddlTTSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", " ISNULL(ACTIVESTATUS,0)=1 AND SECTIONNO > 0", "SECTIONNO");

            }
        }
        else
        {
            lvGlobalTimeTable.DataSource = null;
            lvGlobalTimeTable.DataBind();
            ddlTTSection.Items.Clear();
            ddlTTSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectTimetable.Items.Clear();
            ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));


        }
        Session["TimeSlotTbl"] = null;
        lvTimeSlotDetails.DataSource = null;
        lvTimeSlotDetails.DataBind();
    }

    public void BindCourseDropdownTimeTable(int SessionNo)
    {
        DataSet ds1 = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(SessionNo), 0, 0, 7, Convert.ToInt32(ddlTTSection.SelectedValue));
        if (ds1.Tables[0].Rows.Count > 0)
        {
            // ddlSession.SelectedValue = "";
            ddlSubjectTimetable.Items.Clear();
            ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectTimetable.DataSource = ds1;
            ddlSubjectTimetable.DataValueField = ds1.Tables[0].Columns[0].ToString();
            ddlSubjectTimetable.DataTextField = ds1.Tables[0].Columns[1].ToString();
            ddlSubjectTimetable.DataBind();
            //ddlSession.SelectedIndex = 0;
        }
        else
        {
            ddlSubjectTimetable.Items.Clear();
            ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSubjectTimetable_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["TimeSlotTbl"] = null;
        ClearControls_TimeSlotDetails();
        if (ddlSubjectTimetable.SelectedIndex > 0)
        {
            string MSG = ddlSubjectTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();
            string[] repoarray;
            repoarray = MSG.Split('-');
            string courseno = repoarray[0].ToString();
            string ua_no = repoarray[1].ToString();
            BindGlobalTimeTable(Convert.ToInt32(ddlSessionTimeTable.SelectedValue), Convert.ToInt32(courseno), Convert.ToInt32(ua_no));
        }
        else
        {
            BindGlobalTimeTable(Convert.ToInt32(ddlSessionTimeTable.SelectedValue), 0, 0);
        }
    }

    #region Course Teacher Allotment
    protected void ddlCourseCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            BindMainTeacherCT();
            ddlMainTeacherCT.SelectedIndex = 0;
            ddlGlobalElectiveGroup.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.ddlCourseCT_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindMainTeacherCT()
    {
        ddlMainTeacherCT.Items.Clear();
        ddlMainTeacherCT.Items.Add(new ListItem("Please Select", "0"));
        lstAdditionalTeacherCT.ClearSelection();
        //ddlGlobalElectiveGroup.SelectedIndex = 1;
        DataSet ds = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(ddlSessionCT.SelectedValue), Convert.ToInt32(ddlCourseCT.SelectedValue), 0, 6, 0);
        if (ds.Tables[0].Rows.Count > 0)
        {

            // ddlSession.SelectedValue = "";
            ddlMainTeacherCT.DataSource = ds;
            ddlMainTeacherCT.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlMainTeacherCT.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlMainTeacherCT.DataBind();
            //ddlSession.SelectedIndex = 0;

        }
        else
        {

        }
    }

    protected void ddlSessionCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlSessionCT.SelectedIndex > 0)
            {

                ddlCourseCT.Items.Clear();
                ddlCourseCT.Items.Add(new ListItem("Please Select", "0"));
                ddlMainTeacherCT.Items.Clear();
                ddlMainTeacherCT.Items.Add(new ListItem("Please Select", "0"));
                lvGlobalCourseTeacher.DataSource = null;
                lvGlobalCourseTeacher.DataBind();
                lstAdditionalTeacherCT.ClearSelection();
                //ddlGlobalElectiveGroup.SelectedIndex = 1;
                int SessionNo = Convert.ToInt32(ddlSessionCT.SelectedValue);
                DataSet ds = objCC.GetGlobalOfferedCourseList_Section(SessionNo, 0, 0, 5, 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // ddlSession.SelectedValue = "";
                    ddlCourseCT.DataSource = ds;
                    ddlCourseCT.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlCourseCT.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlCourseCT.DataBind();
                    //ddlSession.SelectedIndex = 0;
                }
                BindCourseTeacherAllotment();

            }
            else
            {

                ddlCourseCT.Items.Clear();
                ddlCourseCT.Items.Add(new ListItem("Please Select", "0"));
                ddlMainTeacherCT.Items.Clear();
                ddlMainTeacherCT.Items.Add(new ListItem("Please Select", "0"));
                lvGlobalCourseTeacher.DataSource = null;
                lvGlobalCourseTeacher.DataBind();
                lstAdditionalTeacherCT.ClearSelection();
                ddlGlobalElectiveGroup.SelectedIndex = 1;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.ddlSessionCT_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindCourseTeacherAllotment()
    {
        try
        {


            DataSet dsData = objSC.GetGlobalCourseTeacherAllotment(Convert.ToInt16(ddlSessionCT.SelectedValue));
            if (dsData != null & dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                lvGlobalCourseTeacher.Visible = true;
                lvGlobalCourseTeacher.DataSource = dsData;
                lvGlobalCourseTeacher.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvGlobalCourseTeacher);//Set label - 
            }
            else
            {
                lvGlobalCourseTeacher.Visible = false;
                lvGlobalCourseTeacher.DataSource = null;
                lvGlobalCourseTeacher.DataBind();
                objCommon.DisplayMessage(this.UpdatePanel1, "No Data Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.BindCourseTeacherAllotment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlMainTeacherCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlMainTeacherCT.SelectedIndex > 0)
            {
                objCommon.FillListBox(lstAdditionalTeacherCT, "USER_ACC U", "DISTINCT U.UA_NO", "UA_FULLNAME", "ISNULL(U.UA_STATUS,0) = 0 AND ISNULL(U.UA_TYPE,0)=3 AND (U.UA_DEPTNO IS NOT NULL OR U.UA_DEPTNO <> '' OR U.UA_DEPTNO <> 0) AND U.UA_NO NOT IN(" + Convert.ToString(ddlMainTeacherCT.SelectedValue) + ")", "U.UA_FULLNAME");
                //Session["CTuano"] = ddlMainTeacherCT.SelectedValue;
            }
            else
            {
                lstAdditionalTeacherCT.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.ddlMainTeacherCT_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitCT_Click(object sender, EventArgs e)
    {
        try
        {

            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            objStudent.SessionNo = Convert.ToInt32(ddlSessionCT.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourseCT.SelectedValue);
            //objStudent.UA_No = Convert.ToInt32(Session["CTuano"]);
            objStudent.UA_No = Convert.ToInt32(ddlMainTeacherCT.SelectedValue);
            string Additionalteacher = "";
            foreach (ListItem items in lstAdditionalTeacherCT.Items)
            {
                if (items.Selected == true)
                {
                    Additionalteacher += items.Value + ',';
                }
            }
            if (Additionalteacher.Length > 0)
            {
                objStudent.AdditionalTeacher = Additionalteacher.Remove(Additionalteacher.Length - 1);
            }
            else
            {
                objStudent.AdditionalTeacher = string.Empty;
            }
            if (Additionalteacher != "")
            {
                objStudent.isAdditionalFlag = 1;
            }
            else
            {
                objStudent.isAdditionalFlag = 0;
            }


            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            //if (objSC.UpdateCourseTeachAllotForGlobalElective(objStudent, OrgId, Convert.ToInt32(ddlGlobalElectiveGroup.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordUpdated))
            int output = objSC.UpdateCourseTeachAllotForGlobalElective(objStudent, OrgId, Convert.ToInt32(ddlGlobalElectiveGroup.SelectedValue));
            if (output == 1)
            {
                objCommon.DisplayMessage(this.updCourseTeacher, "Course Teacher Allotment Successfully..", this.Page);
            }
            else if (output == 2)
            {
                objCommon.DisplayMessage(this.updCourseTeacher, "Course Teacher Allotment Already Found For Selected Faculty", this.Page);
            }
            else if (output == 3)
            {
                objCommon.DisplayMessage(this.updCourseTeacher, "Course Teacher Allotment Already Found For Selected Section/Group", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updCourseTeacher, "Error Occured.", this.Page);
            }
            ddlCourseCT.SelectedIndex = 0;
            ddlMainTeacherCT.SelectedIndex = 0;
            updCourseTeacher.Update();
            lstAdditionalTeacherCT.ClearSelection();
            ddlGlobalElectiveGroup.SelectedIndex = 0;
            BindCourseTeacherAllotment();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.btnSubmitCT_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnClearCT_Click(object sender, EventArgs e)
    {
        ClearControlsCT();
    }

    private void ClearControlsCT()
    {
        ddlSessionCT.SelectedIndex = 0;
        ddlCourseCT.SelectedIndex = 0;
        ddlMainTeacherCT.SelectedIndex = 0;
        updCourseTeacher.Update();
        lstAdditionalTeacherCT.ClearSelection();
        lvGlobalCourseTeacher.DataSource = null;
        lvGlobalCourseTeacher.DataBind();
        ddlGlobalElectiveGroup.SelectedIndex = 0;
    }
    protected void btnInActiveCT_Click(object sender, EventArgs e)
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSessionCT.SelectedValue);
            int courseno = Convert.ToInt32((sender as Button).ToolTip);
            int ua_no = Convert.ToInt32((sender as Button).CommandArgument);
            string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            int Modifiedby = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = CustomStatus.Error;
            cs = (CustomStatus)objCC.InActiveGlobalOfferedCoursesTeacherAllotment(sessionno, courseno, ua_no, IpAddress, Modifiedby);
            if (cs.Equals(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage(updCourseTeacher, "Selected Global Course Teacher Allotment is In-Activated successfully.", this.Page);
            else
                objCommon.DisplayMessage(updCourseTeacher, "Error Occured.", this.Page);

            BindCourseTeacherAllotment();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.btnInActiveCT_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlGlobalElectiveGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMainTeacherCT.SelectedIndex = 0;
        lstAdditionalTeacherCT.ClearSelection();

    }

    #endregion

    #region Cancel Time Table
    protected void ddlSessionCancelTT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionCancelTT.SelectedIndex > 0)
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_TIME_TABLE_CONFIG TTC INNER JOIN ACD_COURSE_TEACHER CT ON(TTC.CTNO = CT.CT_NO) INNER JOIN ACD_SESSION_MASTER SM ON(CT.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_GLOBAL_OFFERED_COURSE GOC ON(CT.COURSENO = GOC.COURSENO)", "COUNT(1)", "SM.SESSIONID =" + Convert.ToInt32(ddlSessionCancelTT.SelectedValue) + " AND ISNULL(GOC.GLOBAL_OFFERED,0) = 1 AND ISNULL(TTC.CANCEL,0)=0"));
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updCancelTimeTable, "Time table not found for this session.", this.Page);
                ddlCourseCancelTT.Items.Clear();
                ddlCourseCancelTT.Items.Add(new ListItem("Please Select", "0"));
                return;
            }
            else
            {
                ddlCourseCancelTT.Items.Clear();
                ddlCourseCancelTT.Items.Add(new ListItem("Please Select", "0"));
                int SessionNo = Convert.ToInt32(ddlSessionCancelTT.SelectedValue);
                //BindCourseDropdownCancelTimeTable(SessionNo);
                objCommon.FillDropDownList(ddlCancelTTSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", " ISNULL(ACTIVESTATUS,0)=1 AND SECTIONNO > 0", "SECTIONNO");
            }
        }
        else
        {
            ddlCourseCancelTT.Items.Clear();
            ddlCourseCancelTT.Items.Add(new ListItem("Please Select", "0"));


        }
        lvTimeSlotDetails.DataSource = null;
        lvTimeSlotDetails.DataBind();
    }
    public void BindCourseDropdownCancelTimeTable(int SessionNo)
    {
        DataSet ds1 = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(SessionNo), 0, 0, 10, Convert.ToInt32(ddlCancelTTSection.SelectedValue));
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlCourseCancelTT.Items.Clear();
            ddlCourseCancelTT.Items.Add(new ListItem("Please Select", "0"));
            // ddlSession.SelectedValue = "";
            ddlCourseCancelTT.DataSource = ds1;
            ddlCourseCancelTT.DataValueField = ds1.Tables[0].Columns[0].ToString();
            ddlCourseCancelTT.DataTextField = ds1.Tables[0].Columns[1].ToString();
            ddlCourseCancelTT.DataBind();
            //ddlSession.SelectedIndex = 0;
        }
        else
        {
            ddlCourseCancelTT.Items.Clear();
            ddlCourseCancelTT.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnShowCancelTT_Click(object sender, EventArgs e)
    {
        BindCancelTimeTableRecord();
    }
    public void BindCancelTimeTableRecord()
    {
        string MSG = ddlCourseCancelTT.SelectedValue.ToString();// Request.Form["msg"].ToString();

        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();
        string facultyno = repoarray[1].ToString();
        DataSet ds = null;
        if (rdoCancelType.SelectedValue.ToString() == "0") //for time table
        {
            ds = objAttC.LoadTimeTableDetailsForCancelTT(Convert.ToInt32(ddlSessionCancelTT.SelectedValue), Convert.ToInt32(facultyno), Convert.ToInt32(courseno), Convert.ToInt16(ddlSlotTypeCancelTT.SelectedValue), Convert.ToDateTime(txtCancelTTStartDate.Text), Convert.ToDateTime(txtCancelTTEndDate.Text), Convert.ToInt32(ddlCancelTTSection.SelectedValue));
        }
        else
        {
            ds = objAttC.LoadAttendanceDetailsForCancelTT(Convert.ToInt32(ddlSessionCancelTT.SelectedValue), Convert.ToInt32(facultyno), Convert.ToInt32(courseno), Convert.ToInt16(ddlSlotTypeCancelTT.SelectedValue), Convert.ToDateTime(txtCancelTTStartDate.Text), Convert.ToDateTime(txtCancelTTEndDate.Text), Convert.ToInt32(ddlCancelTTSection.SelectedValue));
        }
        if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvCancelTimeTable.DataSource = ds;
            lvCancelTimeTable.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label - 
            if (rdoCancelType.SelectedValue.ToString() == "0") //for time table
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#Timetable').show();$('td:nth-child(15)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Timetable').show();$('td:nth-child(15)').show();});", true);

                foreach (ListViewDataItem item in lvCancelTimeTable.Items)
                {
                    HiddenField hdnAttno = item.FindControl("hdnAttno") as HiddenField;
                    Button btnInActiveCancelTT = item.FindControl("btnInActiveCancelTT") as Button;
                    if (hdnAttno.Value == "0")
                    {
                        btnInActiveCancelTT.Enabled = true;
                    }
                    else
                    {
                        btnInActiveCancelTT.Enabled = false;
                    }

                }
            }
            else if (rdoCancelType.SelectedValue.ToString() == "1") //for time table
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#Timetable').show();$('td:nth-child(15)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Timetable').show();$('td:nth-child(15)').show();});", true);

                foreach (ListViewDataItem item in lvCancelTimeTable.Items)
                {
                    HiddenField hdnAttno = item.FindControl("hdnAttno") as HiddenField;
                    Button btnInActiveCancelTT = item.FindControl("btnInActiveCancelTT") as Button;
                    if (hdnAttno.Value == "1")
                    {
                        btnInActiveCancelTT.Enabled = true;
                    }
                    else
                    {
                        btnInActiveCancelTT.Enabled = false;
                    }

                }
            }

        }
        else
        {
            lvCancelTimeTable.DataSource = null;
            lvCancelTimeTable.DataBind();
            objCommon.DisplayMessage(this.updCancelTimeTable, "No Data Found.", this.Page);
        }
    }
    protected void btnCancelTT_Click(object sender, EventArgs e)
    {
        ddlCancelTTSection.SelectedIndex = 0;
        ddlSlotTypeCancelTT.SelectedIndex = 0;
        ddlCourseCancelTT.SelectedIndex = 0;
        ddlSessionCancelTT.SelectedIndex = 0;
        txtCancelTTStartDate.Text = "";
        txtCancelTTEndDate.Text = "";
        txtCancelRemark.Text = "";
    }
    protected void btnInActiveCancelTT_Click(object sender, EventArgs e)
    {
        try
        {
            string MSG = ddlCourseCancelTT.SelectedValue.ToString();// Request.Form["msg"].ToString();

            string[] repoarray;
            repoarray = MSG.Split('-');
            string courseno = repoarray[0].ToString();
            string facultyno = repoarray[1].ToString();
            string ipAdress = Request.ServerVariables["REMOTE_ADDR"];
            string cancelRemark = txtCancelRemark.Text;
            string date = Convert.ToString((sender as Button).CommandArgument);
            int slotno = Convert.ToInt32((sender as Button).ToolTip);
            CustomStatus cs = CustomStatus.Error;
            cs = (CustomStatus)objCC.InActiveGlobalTimeTableDateWise(slotno, date, Convert.ToInt32(facultyno), Convert.ToInt32(courseno), Convert.ToInt32(ddlSessionCancelTT.SelectedValue), Convert.ToInt32(Session["userno"]), ipAdress, cancelRemark, Convert.ToInt32(rdoCancelType.SelectedValue), Convert.ToInt32(ddlCancelTTSection.SelectedValue));
            if (cs.Equals(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage(updCancelTimeTable, "Selected Time Table is In-Activated successfully.", this.Page);
            else
                objCommon.DisplayMessage(updCancelTimeTable, "Error Occured.", this.Page);
            BindCancelTimeTableRecord();
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    //Added by Nehal 18/03/2023
    #region Revised Time Table

    private void BindGlobalRevisedTimeTable(int sessionid, int courseno, int facultyno)
    {
        try
        {
            DataSet ds = null;
            CourseController objCC = new CourseController();
            ds = objCC.GetGlobalCoursesRevisedTimeTableModified(sessionid, courseno, facultyno, Convert.ToInt32(ddlRevisedTTSection.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvGlobalRevisedTimeTable.DataSource = ds;
                lvGlobalRevisedTimeTable.DataBind();
                lvGlobalRevisedTimeTable.Visible = true;
                pnlRevisedTT.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvGlobalRevisedTimeTable);//Set label - 

            }
            else
            {
                lvGlobalRevisedTimeTable.DataSource = null;
                lvGlobalRevisedTimeTable.DataBind();
                lvGlobalRevisedTimeTable.Visible = false;
                pnlRevisedTT.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.BindGlobalRevisedTimeTable -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSessionRevisedTimeTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionRevisedTimeTable.SelectedIndex > 0)
        {


            int SessionNo = Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue);
            //BindCourseDropdownRevisedTimeTable(SessionNo);
            BindGlobalRevisedTimeTable(Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue), 0, 0);
            //BindGlobalRevisedTimeTable();
            objCommon.FillDropDownList(ddlRevisedTTSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", " ISNULL(ACTIVESTATUS,0)=1 AND SECTIONNO > 0", "SECTIONNO");

        }
        else
        {
            ddlSubjectRevisedTimetable.Items.Clear();
            ddlSubjectRevisedTimetable.Items.Add(new ListItem("Please Select", "0"));
            ddlRevisedSlotType.SelectedIndex = 0;
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            txtRevisedStartDate.Text = "";
            txtRevisedEndDate.Text = "";
            lvGlobalRevisedTimeTable.DataSource = null;
            lvGlobalRevisedTimeTable.DataBind();
            pnlRevisedTT.Visible = false;
            txtRevisedRemark.Text = "";
        }
        Session["RevisedTimeSlotTbl"] = null;
        lvRevisedTimeSlotDetails.DataSource = null;
        lvRevisedTimeSlotDetails.DataBind();
    }
    public void BindCourseDropdownRevisedTimeTable(int SessionNo)
    {
        DataSet ds1 = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(SessionNo), 0, 0, 10, Convert.ToInt32(ddlRevisedTTSection.SelectedValue));
        if (ds1.Tables[0].Rows.Count > 0)
        {
            // ddlSession.SelectedValue = "";
            ddlSubjectRevisedTimetable.Items.Clear();
            ddlSubjectRevisedTimetable.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectRevisedTimetable.DataSource = ds1;
            ddlSubjectRevisedTimetable.DataValueField = ds1.Tables[0].Columns[0].ToString();
            ddlSubjectRevisedTimetable.DataTextField = ds1.Tables[0].Columns[1].ToString();
            ddlSubjectRevisedTimetable.DataBind();
            //ddlSession.SelectedIndex = 0;
        }
        else
        {
            ddlSubjectRevisedTimetable.Items.Clear();
            ddlSubjectRevisedTimetable.Items.Add(new ListItem("Please Select", "0"));

        }
    }
    protected void ddlSubjectRevisedTimetable_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["RevisedTimeSlotTbl"] = null;
        ClearControls_RevisedTimeSlotDetails();
        if (ddlSubjectRevisedTimetable.SelectedIndex > 0)
        {
            string MSG = ddlSubjectRevisedTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();
            string[] repoarray;
            repoarray = MSG.Split('-');
            string courseno = repoarray[0].ToString();
            string ua_no = repoarray[1].ToString();
            BindGlobalRevisedTimeTable(Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue), Convert.ToInt32(courseno), Convert.ToInt32(ua_no));
        }
        else
        {
            BindGlobalRevisedTimeTable(Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue), 0, 0);
        }
    }
    private void ClearControls_RevisedTimeSlotDetails()
    {
        ddlRevisedTimeSlot.SelectedIndex = 0;
        ddlRevisedRoom.SelectedIndex = 0;
        ddlRevisedAllDay.SelectedIndex = 0;
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            lvRevisedTimeSlotDetails.DataSource = null;
            lvRevisedTimeSlotDetails.DataBind();
        }


    }
    protected void ddlRevisedSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            lvRevisedTimeSlotDetails.DataSource = null;
            lvRevisedTimeSlotDetails.DataBind();
        }
        ddlRevisedTimeSlot.Items.Clear();
        ddlRevisedTimeSlot.Items.Add(new ListItem("Please Select", "0"));
        string MSG = ddlSubjectRevisedTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();
        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();
        if (ddlRevisedSlotType.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlRevisedTimeSlot, "ACD_TIME_SLOT T INNER JOIN ACD_GLOBAL_OFFERED_COURSE GOC ON(T.COLLEGE_ID = GOC.COLLEGE_ID AND T.DEGREENO = GOC.DEGREENO)", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlRevisedSlotType.SelectedValue) + " AND GOC.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue) + ")", "T.SLOTNO");

            objCommon.FillDropDownList(ddlRevisedTimeSlot, "ACD_TIME_SLOT T INNER JOIN ACD_GLOBAL_OFFERED_COURSE GOC ON(T.COLLEGE_ID = GOC.COLLEGE_ID AND T.DEGREENO = GOC.DEGREENO) INNER JOIN ACD_SCHEME S ON(T.DEGREENO = S.DEGREENO)", "DISTINCT T.SLOTNO", "(TIMEFROM + '-' + TIMETO) AS TIMESLOT", "ISNULL(ACTIVESTATUS,0)=1 AND SLOTTYPE=" + Convert.ToInt32(ddlRevisedSlotType.SelectedValue) + " AND GOC.COURSENO = " + courseno + " AND GOC.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue) + ")", "T.SLOTNO");

            LoadExisitingDates();
        }
        else
        {
            ddlRevisedTimeSlot.Items.Clear();
            ddlRevisedTimeSlot.Items.Add(new ListItem("Please Select", "0"));

        }
    }
    //to load existing dates
    public void LoadExisitingDates()
    {
        try
        {
            if (ddlSessionRevisedTimeTable.SelectedIndex > 0 && ddlSubjectRevisedTimetable.SelectedIndex > 0 && ddlRevisedSlotType.SelectedIndex > 0)
            {
                string MSG = ddlSubjectRevisedTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();

                string[] repoarray;
                repoarray = MSG.Split('-');
                string courseno = repoarray[0].ToString();
                string ua_no = repoarray[1].ToString();

                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;

                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_GLOBAL_OFFERED_COURSE GOC INNER JOIN ACD_COURSE C ON(GOC.COURSENO=C.COURSENO) INNER JOIN ACD_COURSE_TEACHER CT ON(GOC.COURSENO=CT.COURSENO AND GOC.SESSIONNO=CT.SESSIONNO) INNER JOIN ACD_TIME_TABLE_CONFIG TTC ON(TTC.CTNO = CT.CT_NO) INNER JOIN ACD_SESSION_MASTER SM ON(GOC.SESSIONNO= SM.SESSIONNO) INNER JOIN ACD_TIME_SLOT TTS ON(TTS.SLOTNO = TTC.SLOTNO) INNER JOIN ACD_SLOTTYPE ST ON(TTS.SLOTTYPE = ST.SLOTTYPENO)", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES ", "TTC.START_DATE,TTC.END_DATE,MONTH(TTC.START_DATE) as STARTDATEMONTH", "SM.SESSIONID =" + ddlSessionRevisedTimeTable.SelectedValue + "AND CT.GMID = " + ddlRevisedTTSection.SelectedValue + " AND ISNULL(GLOBAL_OFFERED,0)=1 AND ISNULL(TTC.CANCEL,0)=0 AND CT.COURSENO = " + Convert.ToInt32(courseno) + " AND (CT.UA_NO =" + Convert.ToInt32(ua_no) + " OR CT.ADTEACHER = " + Convert.ToInt32(ua_no) + ") AND TTS.SLOTTYPE =" + Convert.ToInt32(ddlRevisedSlotType.SelectedValue), "MONTH(TTC.START_DATE)");
                if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                {
                    ddlExistingDates.DataSource = dsGetExisitingDates.Tables[0];
                    ddlExistingDates.DataTextField = "EXISTINGDATES";
                    ddlExistingDates.DataBind();
                }
                else
                {
                    ddlExistingDates.DataSource = null;
                    ddlExistingDates.DataBind();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlRevisedTimeSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRevisedRoomDropdown();
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            lvRevisedTimeSlotDetails.DataSource = null;
            lvRevisedTimeSlotDetails.DataBind();
        }
    }
    private void FillRevisedRoomDropdown()
    {
        ddlRevisedRoom.Items.Clear();
        ddlRevisedRoom.Items.Add(new ListItem("Please Select", "0"));
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_ACADEMIC_ROOMMASTER RM INNER JOIN ACD_FLOOR_MASTER FM ON(RM.FLOORNO=FM.FLOORNO)", "ROOMNO", "CONCAT(ROOMNAME,'-',FLOORNAME) AS ROOMNAME", "ISNULL(RM.ACTIVESTATUS,0)=1", "ROOMNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRevisedRoom.DataSource = ds;
            ddlRevisedRoom.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlRevisedRoom.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlRevisedRoom.DataBind();
        }
        else
        {
            ddlRevisedRoom.Items.Clear();
            ddlRevisedRoom.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnRevisedAddTimeSlot_Click(object sender, EventArgs e)
    {
        string MSG = ddlSubjectRevisedTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();

        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();

        if (Session["RevisedTimeSlotTbl"] != null && ((DataTable)Session["RevisedTimeSlotTbl"]).Rows.Count > 0 && ((DataTable)Session["RevisedTimeSlotTbl"]) != null)
        {
            DataTable dt = (DataTable)Session["RevisedTimeSlotTbl"];
            //DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            //DataRow [] dr1;
            if (btnRevisedAddTimeSlot.Text != "Update")
            {
                string expression = string.Empty;
                expression = "DAYNO=" + ddlRevisedAllDay.SelectedValue + " AND SLOTNO=" + ddlRevisedTimeSlot.SelectedValue + " AND (ROOMNO=" + ddlRevisedRoom.SelectedValue + " OR " + ddlRevisedRoom.SelectedValue + "=0)";
                DataRow[] dr1 = dt.Select(expression);
                //dr1 = dt.Rows.Find(expression);
                if (dr1.Length > 0)
                {
                    lvRevisedTimeSlotDetails.DataSource = dt;
                    lvRevisedTimeSlotDetails.DataBind();
                    //ClearControls_QualDetails();
                    objCommon.DisplayMessage(this, "Day, Time Slot and Room already selected!", this.Page);
                    return;
                }
            }
            if (ddlRevisedAllDay.SelectedIndex > 0 && ddlRevisedTimeSlot.SelectedIndex > 0)
            {
                dr["SRNO"] = Convert.ToInt32(lvRevisedTimeSlotDetails.Items.Count) + 1;
                dr["DAYNO"] = Convert.ToInt32(ddlRevisedAllDay.SelectedValue);
                dr["DAYNAME"] = ddlRevisedAllDay.SelectedItem.Text;
                dr["SLOTNO"] = Convert.ToInt32(ddlRevisedTimeSlot.SelectedValue);
                dr["SLOTNAME"] = ddlRevisedTimeSlot.SelectedItem.Text;
                dr["ROOMNO"] = Convert.ToInt32(ddlRevisedRoom.SelectedValue);
                dr["ROOMNAME"] = ddlRevisedRoom.SelectedItem.Text;
                dr["COURSENO"] = Convert.ToInt32(courseno);
                dt.Rows.Add(dr);
                Session["RevisedTimeSlotTbl"] = dt;
                lvRevisedTimeSlotDetails.DataSource = dt;
                lvRevisedTimeSlotDetails.DataBind();
                ClearControls_RevisedTimeSlotDetails();
                // objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updReviseTimeTable, "Please Select Day and Time Slot", this.Page);
            }
        }

        else
        {
            DataTable dt = this.GetTimeSlotDetailsDataTable();
            DataRow dr = dt.NewRow();

            if (ddlRevisedAllDay.SelectedIndex > 0 && ddlRevisedTimeSlot.SelectedIndex > 0)
            {
                dr["SRNO"] = Convert.ToInt32(lvRevisedTimeSlotDetails.Items.Count) + 1;
                dr["DAYNO"] = Convert.ToInt32(ddlRevisedAllDay.SelectedValue);
                dr["DAYNAME"] = ddlRevisedAllDay.SelectedItem.Text;
                dr["SLOTNO"] = Convert.ToInt32(ddlRevisedTimeSlot.SelectedValue);
                dr["SLOTNAME"] = ddlRevisedTimeSlot.SelectedItem.Text;
                dr["ROOMNO"] = Convert.ToInt32(ddlRevisedRoom.SelectedValue);
                dr["ROOMNAME"] = ddlRevisedRoom.SelectedItem.Text;
                dr["COURSENO"] = Convert.ToInt32(courseno);
                dt.Rows.Add(dr);
                Session["RevisedTimeSlotTbl"] = dt;
                lvRevisedTimeSlotDetails.DataSource = dt;
                lvRevisedTimeSlotDetails.DataBind();
                ClearControls_RevisedTimeSlotDetails();
                // objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
            }
            else
            {
                if (Session["RevisedTimeSlotTbl"] == null)
                {
                    lvRevisedTimeSlotDetails.DataSource = null;
                    lvRevisedTimeSlotDetails.DataBind();
                }
                objCommon.DisplayMessage(this.updReviseTimeTable, " Please enter all details.", this.Page);

            }
        }
        btnRevisedAddTimeSlot.Text = "Add";

    }
    protected void btnRevisedClearTimeSlot_Click(object sender, EventArgs e)
    {
        ClearControls_RevisedTimeSlotDetails();
    }
    protected void btnEditRevisedTimeSlotDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillRevisedRoomDropdown();
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            //DataTable dt1;//***************
            if (btnRevisedAddTimeSlot.Text != "Update")
            {
                if (Session["RevisedTimeSlotTbl"] != null && ((DataTable)Session["RevisedTimeSlotTbl"]) != null)
                {
                    dt = ((DataTable)Session["RevisedTimeSlotTbl"]);
                    //dt1 = dt.Copy();//**********************************
                    DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                    //DataRow dr = this.GetEditableDataRow(dt1, btnEdit.CommandArgument);//**********

                    ddlRevisedAllDay.SelectedValue = dr["DAYNO"].ToString();
                    ddlRevisedTimeSlot.SelectedValue = dr["SLOTNO"].ToString();
                    string roomno = dr["ROOMNO"].ToString();
                    ddlRevisedRoom.SelectedValue = roomno;

                    dt.Rows.Remove(dr);
                    Session["RevisedTimeSlotTbl"] = dt;
                    this.BindListView_RevisedTImeSlotDetails(dt);
                    btnRevisedAddTimeSlot.Text = "Update";
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnEditTimeSlotDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListView_RevisedTImeSlotDetails(DataTable dt)
    {
        try
        {
            lvRevisedTimeSlotDetails.DataSource = dt;
            lvRevisedTimeSlotDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_RevisedTImeSlotDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDeleteRevisedTimeSlotDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDeleteRevised = sender as ImageButton;

            DataTable dt;
            if (Session["RevisedTimeSlotTbl"] != null && ((DataTable)Session["RevisedTimeSlotTbl"]) != null)
            {
                dt = ((DataTable)Session["RevisedTimeSlotTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDeleteRevised.CommandArgument));
                Session["RevisedTimeSlotTbl"] = dt;
                this.BindListView_RevisedTImeSlotDetails(dt);
                // objCommon.DisplayMessage(this, "Data deleted successfully!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnDeleteTimeSlotDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancelRevisedTimeTable_Click(object sender, EventArgs e)
    {
        ClearControlsRevisedTimeTable();
    }
    protected void btnSubmitRevisedTimeTable_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            objCommon.DisplayMessage(this.updReviseTimeTable, "Please Add Revised Time Table Slots !", this.Page);
            return;
        }
        dt = (DataTable)Session["RevisedTimeSlotTbl"];
        GlobalOfferedCourse objGOC = new GlobalOfferedCourse();
        //GlobalTimeTable[] Gtimetable = null;
        //this.BindTimetableSlotDetails(ref Gtimetable);
        //objGOC.Globaltimetable = Gtimetable;
        string MSG = ddlSubjectRevisedTimetable.SelectedValue.ToString();// Request.Form["msg"].ToString();

        string[] repoarray;
        repoarray = MSG.Split('-');
        string courseno = repoarray[0].ToString();
        string facultyno = repoarray[1].ToString();
        string alternateflag = repoarray[2].ToString();

        objGOC.Courseno = Convert.ToInt32(courseno);
        objGOC.MainFacultyno = Convert.ToInt32(facultyno);
        //objGOC.CTNO = Convert.ToInt32(ddlSubjectTimetable.SelectedValue);
        objGOC.SlotType = Convert.ToInt32(ddlRevisedSlotType.SelectedValue);
        objGOC.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
        objGOC.Orgid = Convert.ToInt32(Session["OrgId"]);
        objGOC.Ua_no = Convert.ToInt32(Session["userno"]);

        DateTime dtStartDate = DateTime.Parse(txtRevisedStartDate.Text);
        string SDate = dtStartDate.ToString("yyyy/MM/dd");
        DateTime dtEndDate = DateTime.Parse(txtRevisedEndDate.Text);
        string EDate = dtEndDate.ToString("yyyy/MM/dd");
        string revisedRemark = txtRevisedRemark.Text;
        CustomStatus cs = (CustomStatus)objAttC.GlobalElective_RevisedTimeTableCreate(dt, objGOC, SDate, EDate, alternateflag, revisedRemark, Convert.ToInt32(ddlRevisedTTSection.SelectedValue));

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updReviseTimeTable, "Time table Revised Successfully", this.Page);
            BindGlobalRevisedTimeTable(Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue), 0, 0);
            ClearAfterSaveControlsRevisedTimeTable();

        }
    }
    protected void lvGlobalRevisedTimeTable_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;

        HiddenField alternateflagRevised = dataitem.FindControl("hdfalternateflagRevised") as HiddenField;
        HiddenField RevisedFacultyNo = dataitem.FindControl("hdfRevisedFacultyNo") as HiddenField;
        HiddenField hdfRevisedStartEndDate = dataitem.FindControl("hdfRevisedStartEndDate") as HiddenField;
        HiddenField RevisedCourseNo = dataitem.FindControl("hdfRevisedCourseNo") as HiddenField;

        int facultyno = Convert.ToInt32(RevisedFacultyNo.Value);
        int courseno = Convert.ToInt32(RevisedCourseNo.Value);
        int alternate = Convert.ToInt32(alternateflagRevised.Value);
        string startendate = hdfRevisedStartEndDate.Value;
        string[] ttDates = startendate.Split('-');
        string startDate; string endDate;
        startDate = ttDates[0].Trim();
        endDate = ttDates[1].Trim();
        ListView lv = dataitem.FindControl("lvDetailsRevised") as ListView;
        try
        {

            DataSet ds = objCC.GetGlobalCoursesTimeTableDetailsSectionModified(facultyno, alternate, Convert.ToInt32(ddlSessionTimeTable.SelectedValue), startDate, endDate, courseno);
            lv.DataSource = ds;
            lv.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Global_Offered_Course.GetGlobalCoursesTimeTableDetailsSection -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditRevisedTimeTable_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlExistingDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            lvRevisedTimeSlotDetails.DataSource = null;
            lvRevisedTimeSlotDetails.DataBind();
        }
    }
    protected void txtRevisedStartDate_TextChanged(object sender, EventArgs e)
    {
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            lvRevisedTimeSlotDetails.DataSource = null;
            lvRevisedTimeSlotDetails.DataBind();
        }
    }
    protected void txtRevisedEndDate_TextChanged(object sender, EventArgs e)
    {
        if (Session["RevisedTimeSlotTbl"] == null)
        {
            lvRevisedTimeSlotDetails.DataSource = null;
            lvRevisedTimeSlotDetails.DataBind();
        }
    }

    private void ClearAfterSaveControlsRevisedTimeTable()
    {
        ddlRevisedTTSection.SelectedIndex = 0;
        ddlSubjectRevisedTimetable.SelectedIndex = 0;
        ddlRevisedSlotType.SelectedIndex = 0;
        Session["RevisedTimeSlotTbl"] = null;
        lvRevisedTimeSlotDetails.DataSource = null;
        lvRevisedTimeSlotDetails.DataBind();
        txtRevisedRemark.Text = "";
        ddlExistingDates.Items.Clear();
        ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
        ddlExistingDates.SelectedIndex = 0;
        txtRevisedStartDate.Text = "";
        txtRevisedEndDate.Text = "";
    }
    private void ClearControlsRevisedTimeTable()
    {
        ddlRevisedTTSection.SelectedIndex = 0;
        ddlSessionRevisedTimeTable.SelectedIndex = 0;
        ddlSubjectRevisedTimetable.SelectedIndex = 0;
        txtRevisedEndDate.Text = "";
        txtRevisedStartDate.Text = "";
        ddlExistingDates.SelectedIndex = 0;
        ddlRevisedSlotType.SelectedIndex = 0;
        Session["RevisedTimeSlotTbl"] = null;
        lvRevisedTimeSlotDetails.DataSource = null;
        lvRevisedTimeSlotDetails.DataBind();
        lvGlobalRevisedTimeTable.DataSource = null;
        lvGlobalRevisedTimeTable.DataBind();
        pnlRevisedTT.Visible = false;
        txtRevisedRemark.Text = "";
    }
    #endregion

    #region Report
    protected void ddlSlotTypeReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSessionReport.SelectedIndex > 0 && ddlSlotTypeReport.SelectedIndex > 0)
            {
                ddlExistingDatesReport.Items.Clear();
                ddlExistingDatesReport.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDatesReport.SelectedIndex = 0;

                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TT INNER JOIN  ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_COURSE CC ON CC.COURSENO = CT.COURSENO INNER JOIN ACD_SESSION_MASTER SM ON(CT.SESSIONNO = SM.SESSIONNO) INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES ", "START_DATE,END_DATE,MONTH(START_DATE) as STARTDATEMONTH", "ISNULL(TT.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND ISNULL(CC.GLOBALELE,0)=1 AND SM.SESSIONID=" + ddlSessionReport.SelectedValue + " and SLOTTYPE=" + ddlSlotTypeReport.SelectedValue, "MONTH(START_DATE) ");
                if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                {
                    ddlExistingDatesReport.DataSource = dsGetExisitingDates.Tables[0];
                    ddlExistingDatesReport.DataTextField = "EXISTINGDATES";
                    ddlExistingDatesReport.DataBind();
                }
                else
                {
                    ddlExistingDatesReport.DataSource = null;
                    ddlExistingDatesReport.DataBind();
                }
            }
            else
            {
                ddlExistingDatesReport.Items.Clear();
                ddlExistingDatesReport.Items.Add(new ListItem("Please Select", "0"));
                txtFromDateReport.Text = "";
                txtTodateReport.Text = "";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlExistingDatesReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExistingDatesReport.SelectedIndex > 0)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "test1();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {test1();});", true);
            string myStr = ddlExistingDatesReport.SelectedItem.ToString();
            string[] ssizes = myStr.Split(' ');
            string startdate = ssizes[0].ToString();
            string enddate = ssizes[2].ToString();
            txtFromDateReport.Text = startdate;
            txtTodateReport.Text = enddate;
        }
        else
        {
            txtFromDateReport.Text = "";
            txtTodateReport.Text = "";
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ""
                   + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSessionReport.SelectedValue)
                   + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"])
                   + ",@P_SLOTTYPE=" + ddlSlotTypeReport.SelectedValue + ",@P_FROMDATE=" + txtFromDateReport.Text + ",@P_TODATE=" + txtTodateReport.Text;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnTTReportFormate1_Click(object sender, EventArgs e)
    {
        ShowReport("TIME TABLE", "rptAcadGlobalElectiveTimeTableReport_New.rpt");
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        AcdAttendanceController objAttCon = new AcdAttendanceController();
        DataSet ds = null;
        int SessionNo = 0;
        DateTime FromDate, ToDate;
        SessionNo = Convert.ToInt32(ddlSessionReport.SelectedValue);

        if (!string.IsNullOrEmpty(txtFromDateReport.Text.Trim()))
            FromDate = Convert.ToDateTime(txtFromDateReport.Text.Trim());
        else
        {
            objCommon.DisplayMessage(updReport, "Please Enter From Date", this.Page);
            return;
        }

        if (!string.IsNullOrEmpty(txtTodateReport.Text.Trim()))
            ToDate = Convert.ToDateTime(txtTodateReport.Text.Trim());
        else
        {
            objCommon.DisplayMessage(updReport, "Please Enter To Date", this.Page);
            return;
        }



        ds = objAttCon.RetrieveStudentAttDetailsFormatIIIExcelGlobalElective(FromDate, ToDate, SessionNo);
        DataGrid dg = new DataGrid();

        if (ds.Tables[0].Rows.Count > 0)
        {
            //this.CallExcelIII();
            string attachment = "attachment; filename=GlobalElectiveMasterTimetable_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
            return;
        }
    }
    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        ddlSessionReport.SelectedIndex = 0;
        ddlSlotTypeReport.SelectedIndex = 0;
        ddlExistingDatesReport.Items.Clear();
        ddlExistingDatesReport.Items.Add(new ListItem("Please Select", "0"));
        txtFromDateReport.Text = "";
        txtTodateReport.Text = "";
    }
    #endregion
    protected void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        if (Convert.ToInt32(ddlsection.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ViewState["globalElectiveCTAllotment"]) == 1)
            {
                ds = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSubjectAT.SelectedValue), 0, 9, Convert.ToInt32(ddlsection.SelectedValue));
            }
            else
            {
                ds = objCC.GetGlobalOfferedCourseList_Section(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSubjectAT.SelectedValue), 0, 6, 0);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {

                // ddlSession.SelectedValue = "";
                ddlTeacher.Items.Clear();
                ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
                ddlTeacher.DataSource = ds;
                ddlTeacher.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlTeacher.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlTeacher.DataBind();
                //ddlSession.SelectedIndex = 0;


            }
            else
            {
                ddlTeacher.Items.Clear();
                ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
                //ddlTeacher.DataSource = null;
                //ddlTeacher.DataBind();
            }
        }
    }
    protected void ddlTTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["TimeSlotTbl"] == null)
        {
            lvTimeSlotDetails.DataSource = null;
            lvTimeSlotDetails.DataBind();
        }
        int SessionNo = Convert.ToInt32(ddlSessionTimeTable.SelectedValue);
        if (Convert.ToInt32(ddlTTSection.SelectedValue) > 0)
        {
            BindGlobalTimeTable(SessionNo, 0, 0);
            BindCourseDropdownTimeTable(SessionNo);
        }
        else
        {
            ddlSlotType.SelectedIndex = 0;
            BindGlobalTimeTable(SessionNo, 0, 0);
            ClearControls_TimeSlotDetails();
            ddlSubjectTimetable.Items.Clear();
            ddlSubjectTimetable.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlRevisedTTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRevisedTTSection.SelectedValue) > 0)
        {
            int SessionNo = Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue);
            BindCourseDropdownRevisedTimeTable(SessionNo);
            BindGlobalRevisedTimeTable(Convert.ToInt32(ddlSessionRevisedTimeTable.SelectedValue), 0, 0);
        }
        else
        {
            ddlSubjectRevisedTimetable.Items.Clear();
            ddlSubjectRevisedTimetable.Items.Add(new ListItem("Please Select", "0"));
            ddlRevisedSlotType.SelectedIndex = 0;
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            txtRevisedStartDate.Text = "";
            txtRevisedEndDate.Text = "";
            txtRevisedRemark.Text = "";
            Session["RevisedTimeSlotTbl"] = null;
            if (Session["RevisedTimeSlotTbl"] == null)
            {
                lvRevisedTimeSlotDetails.DataSource = null;
                lvRevisedTimeSlotDetails.DataBind();
            }
        }
    }
    protected void ddlCancelTTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCancelTTSection.SelectedIndex) > 0)
        {
            int SessionNo = Convert.ToInt32(ddlSessionCancelTT.SelectedValue);
            BindCourseDropdownCancelTimeTable(SessionNo);
        }
        else
        {
            ddlCourseCancelTT.Items.Clear();
            ddlCourseCancelTT.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotTypeCancelTT.SelectedIndex = 0;
            txtCancelTTStartDate.Text = "";
            txtCancelRemark.Text = "";
            txtCancelTTEndDate.Text = "";
        }
    }
    protected void rdoCancelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionCancelTT.SelectedIndex == 0 || ddlCancelTTSection.SelectedIndex == 0 || ddlCourseCancelTT.SelectedIndex == 0 || ddlSlotTypeCancelTT.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this, "Please Select Details", this);
        }
        else if (txtCancelTTStartDate.Text == "")
        {
            objCommon.DisplayMessage(this, "Please Enter Start Date", this);
            txtStartDate.Focus();
        }
        else if (txtCancelTTEndDate.Text == "")
        {
            objCommon.DisplayMessage(this, "Please Enter End Date", this);
            txtEndDate.Focus();
        }
        else
        {
            BindCancelTimeTableRecord();
        }
    }
    protected void btnEditCT_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var item = (ListViewItem)btn.NamingContainer;
        HiddenField hdnScheme = item.FindControl("hdfCTSection") as HiddenField;

        int sessionno = Convert.ToInt32(ddlSessionCT.SelectedValue);
        int courseno = Convert.ToInt32((sender as Button).ToolTip);
        int ua_no = Convert.ToInt32((sender as Button).CommandArgument);
        int sectionno = Convert.ToInt32(hdnScheme.Value);
        string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
        int Modifiedby = Convert.ToInt32(Session["userno"]);
        DataSet dsData = objSC.GetGlobalCourseTeacherAllotmentForEdit(Convert.ToInt16(ddlSessionCT.SelectedValue), courseno, ua_no, sectionno);
        if (dsData != null & dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
        {

            ddlSessionAttConfig.SelectedValue = dsData.Tables[0].Rows[0]["SESSIONID"] == DBNull.Value ? "0" : dsData.Tables[0].Rows[0]["SESSIONID"].ToString();
            ddlCourseCT.SelectedValue = dsData.Tables[0].Rows[0]["COURSENO"] == DBNull.Value ? "0" : dsData.Tables[0].Rows[0]["COURSENO"].ToString();
            ddlGlobalElectiveGroup.SelectedValue = dsData.Tables[0].Rows[0]["SECTIONNO"] == DBNull.Value ? "0" : dsData.Tables[0].Rows[0]["SECTIONNO"].ToString();
            BindMainTeacherCT();

            ddlMainTeacherCT.SelectedValue = dsData.Tables[0].Rows[0]["UA_NO"] == DBNull.Value ? "0" : dsData.Tables[0].Rows[0]["UA_NO"].ToString();
            Session["CTuano"] = dsData.Tables[0].Rows[0]["UA_NO"] == DBNull.Value ? "0" : dsData.Tables[0].Rows[0]["UA_NO"].ToString();
            //ddlMainTeacherCT.Enabled = false;
            lstAdditionalTeacherCT.Enabled = false;
            ViewState["additonalitems"] = Convert.ToString(dsData.Tables[0].Rows[0]["ADTEACHER"]).ToString();
            if (ddlMainTeacherCT.SelectedIndex > 0)
            {
                objCommon.FillListBox(lstAdditionalTeacherCT, "USER_ACC U", "DISTINCT U.UA_NO", "UA_FULLNAME", "ISNULL(U.UA_STATUS,0) = 0 AND ISNULL(U.UA_TYPE,0)=3 AND (U.UA_DEPTNO IS NOT NULL OR U.UA_DEPTNO <> '' OR U.UA_DEPTNO <> 0) AND U.UA_NO NOT IN(" + Convert.ToString(ddlMainTeacherCT.SelectedValue) + ")", "U.UA_FULLNAME");
            }
            else
            {
                lstAdditionalTeacherCT.ClearSelection();
            }
            if (ViewState["additonalitems"] != null && ViewState["additonalitems"] != "")
            {
                string Additonalilnos = ViewState["additonalitems"].ToString();
                string[] subs = Additonalilnos.Split(',');

                foreach (ListItem additonalitems in lstAdditionalTeacherCT.Items)
                {
                    for (int i = 0; i < subs.Count(); i++)
                    {
                        if (subs[i].ToString().Trim() == additonalitems.Value)
                        {
                            additonalitems.Selected = true;
                        }
                    }
                }
            }

        }
    }
}