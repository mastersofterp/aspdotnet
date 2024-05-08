//======================================================================================
// PROJECT NAME  : UAIMS                                                 
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : CourseAllotment_Bulk.aspx.cs                                      
// CREATION DATE : 22/02/2019                                                       
// CREATED BY    : SATISH T    
// PAGE DESC     : USED TO ALLOT COURSE WISE TEACHER ALLOTMENT IN BULK FOR SEMESTER                                  
// MODIFIED DATE : 04/03/2019                                                                     
// MODIFIED DESC : RAJU BITODE                                                                     
//======================================================================================                                
// MODIFIED DATE : 08/07/2019                                                                     
// MODIFIED DESC : NEHA BARANWAL                                                                     
//======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.IO;


public partial class ACADEMIC_CourseAllotment_Bulk : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttendC = new AcdAttendanceController();
    AcdAttendanceModel objAttendModel = new AcdAttendanceModel();

    #region Page Event
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
                    //if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    this.FillDropdown();
                    //this.BindListView();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Dileep K. on 25/01/2022
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Dileep K. on 25/01/2022
                }
            }

            //this.BindListView();
            divMsg.InnerHtml = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=CourseAllotment_Bulk.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=CourseAllotment_Bulk.aspx");
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Private Methods
    private void FillDropdown()
    {
        try
        {
            //For Additional Teacher
            //For Main Teacher

            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                objCommon.FillDropDownList(ddlSchemeAT, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                objCommon.FillDropDownList(ddlSchemeCT, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");

                // objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
                // objCommon.FillDropDownList(ddlSchoolInstituteAT, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
                //objCommon.FillDropDownList(ddlSchoolInstituteCT, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
                // objCommon.FillDropDownList(ddlDeptAT, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
                // objCommon.FillDropDownList(ddlDepartmentCT, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
            }
            else
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                objCommon.FillDropDownList(ddlSchemeAT, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                objCommon.FillDropDownList(ddlSchemeCT, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "", "COSCHNO");

                // objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlSchoolInstituteAT, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlSchoolInstituteCT, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                // objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
            }

            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME asc");
            // objCommon.FillDropDownList(ddlDegreeCT, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME asc");
            // objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT D INNER JOIN ACD_sCHEME S ON D.DEPTNO=S.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0", "D.DEPTNAME asc");

        }
        catch
        {
            throw;
        }
    }

    private void ClearDropDown(DropDownList ddlList)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";
    }

    #endregion

    #region For Main Teacher

    //For Main Teacher
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlScheme.SelectedValue));
            //ViewState["degreeno"]

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }

            this.clearATListView();
            //ddlSemester.SelectedIndex = 0;
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubject);
            divOtherDepartment.Visible = false;
            chkDept.Checked = false;
            btnSave.Visible = false;
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSessionBulk, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                ddlSessionBulk.Focus();
            }

            //Comment & Added By Mahesh on Dated 18-02-2021
            //string _deptNo = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + ddlScheme.SelectedValue) == string.Empty ? "0" : objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + ddlScheme.SelectedValue);
            //ddldepartment.SelectedValue = _deptNo;
            // BindListViewMain(); 
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSessionBulk_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSchoolInstitute.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            ClearDropDown(ddlDegree);
            ClearDropDown(ddlBranch);
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubject);
            //ddlDegree.SelectedIndex = 0;
            //ddldepartment.SelectedIndex = 0;
            //ddlBranch.SelectedIndex = 0;
            //ddlScheme.SelectedIndex = 0;
            //ddlSemester.SelectedIndex = 0;
            btnSave.Visible = false;

            string odd_even = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(ddlSessionBulk.SelectedValue));
            string exam_type = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSessionBulk.SelectedValue));
            string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON B.BRANCHNO=A.BRANCHNO", "CAST(DURATION AS INT)*2 AS DURATION", "B.BRANCHNO=" + ViewState["branchno"].ToString() + " AND A.DEGREENO=" + ViewState["degreeno"].ToString());
            if (exam_type == "1" && odd_even != "3")
            {
                //Commented by As per Requirement Of Romal Saluja Sir

                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "S.COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNAME asc");

                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
                int SessionNo = Convert.ToInt32(ddlSessionBulk.SelectedValue);
                DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ddlSemester.DataSource = ds;
                    ddlSemester.DataTextField = "SEMESTERNAME";
                    ddlSemester.DataValueField = "SEMESTERNO";
                    ddlSemester.DataBind();
                }
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)",
                    "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ",
                    "S.COLLEGE_ID=" + ViewState["college_id"].ToString() + 
                    " AND SM.SEMESTERNO<=" + semCount + 
                    " AND ACTIVESTATUS=1 UNION SELECT DISTINCT SEMESTERNO,SEMESTERNAME FROM ACD_SEMESTER WHERE SEMESTERNAME like '%SUMMER%' AND ACTIVESTATUS=1", // added by Shailendra K. on dated 08.05.2024 as per Dr. Manoj sir Suggestion for summer semester.
                    "");

            }
            ddlSemester.Focus();
            this.clearATListView();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Session["usertype"].ToString() == "1")
        //{
        try
        {
            objCommon.FillDropDownList(ddlDegree, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + "", "B.DEGREENAME ASC");
            objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + "", "A.DEPTNAME ASC");
            // ClearDropDown(ddlDegree);
            ClearDropDown(ddlBranch);
            ClearDropDown(ddlScheme);
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubject);
            btnSave.Visible = false;
            this.clearATListView();
            divOtherDepartment.Visible = false;
            chkCheck.Visible = false;
            //}
            //else
            //{
            //    ddldepartment.SelectedIndex = 0;
            //    ClearDropDown(ddlDegree);
            //    ClearDropDown(ddlBranch);
            //    ClearDropDown(ddlScheme);
            //    ClearDropDown(ddlSemester);
            //    ClearDropDown(ddlSubject);
            //    btnSave.Visible = false;
            //    this.clearATListView();
            //    divOtherDepartment.Visible = false;
            //    chkCheck.Visible = true;
            //}
        }
        catch
        {
            throw;
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlDegree, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + " AND A.DEPTNO=" + ddldepartment.SelectedValue, "B.DEGREENAME ASC");

        //ClearDropDown(ddlBranch);
        //ClearDropDown(ddlScheme);
        //ClearDropDown(ddlSemester);
        //ClearDropDown(ddlSubject);
        //btnSave.Visible = false;
        this.clearATListView();
        divOtherDepartment.Visible = false;
        chkDept.Checked = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedIndex > 0)
        //{
        try
        {
            this.clearATListView();
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + " AND A.DEGREENO = " + ddlDegree.SelectedValue, "B.LONGNAME asc");
            //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + " AND A.DEPTNO=" + ddldepartment.SelectedValue + " AND A.DEGREENO = " + ddlDegree.SelectedValue, "B.LONGNAME asc");
            ddlBranch.Focus();
            //}

            ClearDropDown(ddlScheme);
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubject);
            //ddlBranch.SelectedIndex = 0;
            //ddlScheme.SelectedIndex = 0;
            //ddlSemester.SelectedIndex = 0;
            divOtherDepartment.Visible = false;
            chkDept.Checked = false;
            btnSave.Visible = false;
            this.clearATListView();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlBranch.SelectedIndex > 0)
            //{
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENAME asc");
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO=" + ddldepartment.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENAME asc");
            ddlScheme.Focus();
            //}

            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubject);
            //ddlSemester.SelectedIndex = 0;

            divOtherDepartment.Visible = false;
            chkDept.Checked = false;
            btnSave.Visible = false;
            this.clearATListView();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.clearATListView();
            btnSave.Visible = false;
            divOtherDepartment.Visible = false;
            chkDept.Checked = false;
            ClearDropDown(ddlSubject);
            //ddlSubject.SelectedIndex = 0;

            if (ddlSemester.SelectedIndex > 0)
            {
                DataSet ds = objAttendC.GetCourseforTeacherAllotment(Convert.ToInt32(ddlSessionBulk.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));
                //objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_OFFERED_COURSE O WITH (NOLOCK) ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", " O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSessionBulk.SelectedValue + " AND O.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND OFFERED=1 AND O.SEMESTERNO  = " + ddlSemester.SelectedValue, "O.CCODE,C.COURSE_NAME asc");
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSubject.DataSource = ds.Tables[0];
                    ddlSubject.DataValueField = ds.Tables[0].Columns["COURSENO"].ToString();
                    ddlSubject.DataTextField = ds.Tables[0].Columns["COURSENAME"].ToString();
                    ddlSubject.DataBind();
                }
                ddlSubject.Focus();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSubject_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.clearATListView();
            //string tutorial = objCommon.LookUp("ACD_COURSE", "ISNULL(THEORY,0)", "COURSENO=" + ddlSubject.SelectedValue);
            //if (Convert.ToDecimal(tutorial) > 0)
            //{
            //    divTutorial.Visible = true;
            //    ddlTutorial.SelectedValue = "1";
            //}
            //else
            //{
            //    divTutorial.Visible = false;
            //    ddlTutorial.SelectedValue = "1";
            //}
            if (ddlSubject.SelectedIndex > 0)
            {
                ShowTheoryPractical();
                //if (Session["usertype"].ToString() != "1")
                //{
                //objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
                objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");

                //}
                //else
                //{
                //    objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + ViewState["college_id"].ToString() + "", "A.DEPTNAME ASC");
                //}
                btnSave.Visible = false;
            }
            else
            {
                divTutorial.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    public void ShowTheoryPractical()
    {
        ddlTutorial.Items.Clear();
        DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(THEORY,0) AS THEORY,TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubject.SelectedValue, "");
        if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1"))
        {

            divTutorial.Visible = false;
            ddlTutorial.Items.Add(new ListItem("Theory", "1"));
            ddlTutorial.SelectedValue = "1";
        }
        else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1"))
        {
            if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
            {
                divTutorial.Visible = true;
                ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                ddlTutorial.Items.Add(new ListItem("Tutorial", "3"));
                ddlTutorial.SelectedValue = "1";
            }
            else
            {
                divTutorial.Visible = false;
                ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                ddlTutorial.SelectedValue = "1";
            }
        }
        else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2"))
        {

            divTutorial.Visible = false;
            ddlTutorial.Items.Add(new ListItem("Practical", "2"));
            ddlTutorial.SelectedValue = "2";
        }
        //else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
        else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2"))
        {
            if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
            {
                divTutorial.Visible = true;
                ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                ddlTutorial.Items.Add(new ListItem("Tutorial", "3"));
                ddlTutorial.SelectedValue = "2";
            }
            else
            {
                divTutorial.Visible = false;
                ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                ddlTutorial.SelectedValue = "2";
            }
        }
        else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
        {
            divTutorial.Visible = true;
            ddlTutorial.Items.Add(new ListItem("Theory", "1"));
            ddlTutorial.Items.Add(new ListItem("Practical", "2"));
            ddlTutorial.SelectedValue = "1";
        }
        else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
        {
            if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
            {
                divTutorial.Visible = true;
                ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                ddlTutorial.Items.Add(new ListItem("Tutorial", "3"));
                ddlTutorial.SelectedValue = "1";
            }
            else
            {
                divTutorial.Visible = true;
                ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                ddlTutorial.SelectedValue = "1";
            }
        }
    }

    protected void ddlOtherDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        this.clearATListView();
        btnSave.Visible = false;
    }

    protected void ddlTutorial_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.clearATListView();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            updPanel1.Update();
            this.BindListViewMain();
        }
        //ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        //ListView lv = (ListView)cph.FindControl("lvCourseTeacher");
        //foreach (ListViewDataItem dataitem in lv.Items)
        //{
        //    ListBox lstbx = dataitem.FindControl("lstbxSections") as ListBox;
        //    lstbx.Items[0].Text = "X";
        //    lstbx.Items[0].Selected = false;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#ms-opt-1').prop('checked', true);", true);
        //}
        catch
        {
            throw;
        }
    }

    private void BindListViewMain()
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSessionBulk.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int courseno = Convert.ToInt32(ddlSubject.SelectedValue);
            //int deptNo = (chkDept.Checked == true ? 1 : 0);
            int deptNo = 0;
            int ChkOtherDept = 0;
            if (ddldepartment.SelectedIndex > 0)
            {
                deptNo = Convert.ToInt32(ddldepartment.SelectedValue);
            }

            //Added Mahesh regarding Faculty assign for another Department purpose if not in Current Department
            if (chkDept.Checked == true)
            {
                ChkOtherDept = 1;
                if (ddlOtherDepartment.SelectedIndex > 0)
                {
                    deptNo = Convert.ToInt32(ddlOtherDepartment.SelectedValue);
                }
            }
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            int is_tutorial = Convert.ToInt32(ddlTutorial.SelectedValue);
            string subid = "0";
            DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubject.SelectedValue, "");

            DataSet ds = objAttendC.GetSubjectForCourseAllotment(sessionno, schemeno, semesterno, courseno, deptNo, Convert.ToInt32(ViewState["college_id"].ToString()), ChkOtherDept, is_tutorial, OrgId);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //if (ds.Tables[0].Rows[0]["SUBID"].ToString() == "1")//commented by dileep on 19022021 for Sessional COurse Type.
                ViewState["IS_TUTORIAL"] = ds.Tables[0].Rows[0]["TUTORIAL"].ToString();
                subid = ds.Tables[0].Rows[0]["SUBID"] != DBNull.Value ? ds.Tables[0].Rows[0]["SUBID"].ToString() : "0";
                //if ((subid != "2" && subid != "11" && OrgId == 1) || ((subid != "2" && subid != "4" && subid != "12" && subid != "15" && OrgId == 2)) || (subid != "2" && OrgId != 1 && OrgId != 2))  //added by dileep on 19022021 for Sessional COurse Type.
                //{
                //    if (ddlTutorial.SelectedValue == "2")//Added By Dileep Kare on 18.01.2022 this for Theory with Tutorial Subject
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('#BatchTheory1').text('Section - Tutorial Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('#BatchTheory1').text('Section - Tutorial Batch');$('td:nth-child(4)').show();});", true);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#Section1').hide();$('td:nth-child(3)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').hide();$('td:nth-child(3)').hide();});", true);
                //    }
                //    else//this code for Theory Subject
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('#BatchTheory1').text('Section - Batch');$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(4)').hide();});", true);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#Section1').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').show();$('td:nth-child(3)').show();});", true);
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('#BatchTheory1').text('Section - Practical Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('#BatchTheory1').text('Section - Practical Batch');$('td:nth-child(4)').show();});", true);
                //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#Section1').hide();$('td:nth-child(3)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').hide();$('td:nth-child(3)').hide();});", true);
                //}
                if (ddlTutorial.SelectedValue == "2")//Added by Rahul M. this for Theory with Practical Subject
                {
                    if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('#BatchTheory1').text('Section - Tutorial Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('#BatchTheory1').text('Section - Practical Batch');$('td:nth-child(4)').show();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#Section1').hide();$('td:nth-child(3)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').hide();$('td:nth-child(3)').hide();});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "$('#BatchTheory1').hide();$('#BatchTheory1').text('Section - Batch');$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(4)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "$('#Section1').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').show();$('td:nth-child(3)').show();});", true);
                    }
                }
                else if (ddlTutorial.SelectedValue == "3")//Added by Rahul M. this for Theory with Tutorial Subject
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey5", "$('#BatchTheory1').show();$('#BatchTheory1').text('Section - Tutorial Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('#BatchTheory1').text('Section - Tutorial Batch');$('td:nth-child(4)').show();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey6", "$('#Section1').hide();$('td:nth-child(3)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').hide();$('td:nth-child(3)').hide();});", true);
                }
                else//this code for Theory Subject
                {

                    if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey7", "$('#BatchTheory1').hide();$('#BatchTheory1').text('Section - Batch');$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(4)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey8", "$('#Section1').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').show();$('td:nth-child(3)').show();});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey9", "$('#BatchTheory1').show();$('#BatchTheory1').text('Section - Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('td:nth-child(4)').show();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey10", "$('#Section1').hide();$('td:nth-child(3)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').hide();$('td:nth-child(3)').hide();});", true);
                    }

                }
                lvCourseTeacher.DataSource = ds;
                lvCourseTeacher.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourseTeacher);//Set label 
                btnSave.Visible = true;

                updPanel2.Update();
                lvCourseTeacherAT.DataSource = null;
                lvCourseTeacherAT.DataBind();
                btnSaveAT.Visible = false;

                updCancelCT.Update();
                lvCourseTeacherCT.DataSource = null;
                lvCourseTeacherCT.DataBind();
                btnSubmitCT.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
                lvCourseTeacher.DataSource = null;
                lvCourseTeacher.DataBind();
            }

            foreach (ListViewDataItem dataitem in lvCourseTeacher.Items)
            {
                CheckBox chkTeacher = dataitem.FindControl("chkAccept") as CheckBox;
                HiddenField hdnAllo = dataitem.FindControl("hdnAllo") as HiddenField;
                if (hdnAllo.Value == "1")
                    chkTeacher.BackColor = System.Drawing.Color.Red;
                HiddenField hdnTeacher = dataitem.FindControl("hdnTeacher") as HiddenField;
                DropDownList ddlSection = dataitem.FindControl("ddlSection") as DropDownList;
                HiddenField hdnSection = dataitem.FindControl("hdnSection") as HiddenField;
                ddlSection.SelectedValue = hdnSection.Value;
                if (ddlSection.SelectedIndex == 0 && hdnTeacher.Value == "0")
                    ddlSection.Enabled = false;
            }

        }
        catch
        {
            throw;
        }
    }

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void lvCourseTeacher_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int deptNo = 0;
        //  DropDownList ddlTeacher = e.Item.FindControl("ddlTeacher") as DropDownList;
        try
        {
            deptNo = Convert.ToInt32(ddlDeptAT.SelectedValue);
            HiddenField hdnTeacher = e.Item.FindControl("hdnTeacher") as HiddenField;
            CheckBox chkBox = e.Item.FindControl("chkAccept") as CheckBox;

            deptNo = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])));



            ListBox lstbxBatch = e.Item.FindControl("lstbxBatch") as ListBox;
            HiddenField hdnSubid = e.Item.FindControl("hdnSubid") as HiddenField;
            SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
            DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(TH_PR,0) AS TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubject.SelectedValue, "");

            //  lstbxBatch.Attributes.Add("disabled", "true");

            //bind sections
            //DataSet ds = objsql.ExecuteDataSet("select SECTIONNO,SECTIONNAME from ACD_SECTION where SECTIONNO in(1,2,3,4)");
            DataSet ds = objsql.ExecuteDataSet("select SECTIONNO,SECTIONNAME from ACD_SECTION WHERE SECTIONNO>0 order by SECTIONNO"); // Above Commented line by Swapnil for remove where condition.

            ListBox lstbxSections = e.Item.FindControl("lstbxSections") as ListBox;
            lstbxSections.DataValueField = "SECTIONNO";
            lstbxSections.DataTextField = "SECTIONNAME";
            lstbxSections.DataSource = ds.Tables[0];
            lstbxSections.DataBind();
            int subid = Convert.ToInt32(hdnSubid.Value);
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());

            // if (Convert.ToInt32(hdnSubid.Value) == 1)//commented by dileep on 19022021 for Sessional COurse Type.
            //if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
            //{
            #region Practical Subject
            if (ddlTutorial.SelectedValue == "2")// Adde by Dileep Kare on 18.01.2022 this is for tutorial.
            {
                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                {
                    DataSet dsGetBatches = objsql.ExecuteDataSet("select (Cast(B.SECTIONNO as varchar)+'-'+cast(BATCHNO as Varchar)) as BATCHNO,SECTIONNAME+' - '+BATCHNAME BATCHNAME from ACD_BATCH B INNER JOIN ACD_SECTION S ON (B.SECTIONNO=S.SECTIONNO) where BATCHNO>0 order by SECTIONNAME");

                    lstbxBatch.DataValueField = "BATCHNO";
                    lstbxBatch.DataTextField = "BATCHNAME";
                    lstbxBatch.DataSource = dsGetBatches.Tables[0];
                    lstbxBatch.DataBind();

                    ListBox lstbxAdTeacher = e.Item.FindControl("lstbxAdTeacher") as ListBox;
                    lstbxAdTeacher.DataValueField = "BATCHNO";
                    lstbxAdTeacher.DataTextField = "BATCHNAME";
                    lstbxAdTeacher.DataSource = dsGetBatches.Tables[0];
                    lstbxAdTeacher.DataBind();


                    //to get selected sections
                    //DataSet dsGetSections = objsql.ExecuteDataSet("select S.SECTIONNO,SectionName from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0");
                    //DataSet dsGetSections = objsql.ExecuteDataSet("select S.SECTIONNO,SectionName from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END)");
                    DataSet dsGetSections = objsql.ExecuteDataSet("select S.SECTIONNO,SectionName from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + " AND ISNULL(IS_THPR_BOTH,0)=2 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=0 AND ISNULL(T.CANCEL,0)=0 ");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString());
                    // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                    for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < lstbxSections.Items.Count; j++)
                        {
                            if (lstbxSections.Items[j].Selected == true)
                            {
                                lstbxSections.Items[j].Selected = true;
                            }
                            if (lstbxSections.Items[j].Value.ToString() == dsGetSections.Tables[0].Rows[i]["SECTIONNO"].ToString())
                            {
                                lstbxSections.Items[j].Selected = true;

                            }
                        }
                    }

                    ////to get selected sections
                    //DataSet dsGetBatchnos = objsql.ExecuteDataSet("select S.BATCHNO,BATCHNAME from ACD_Course_TEACHER T inner join ACD_BATCH S on T.BATCHNO=S.BATCHNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END)"); // Commented by Rahul
                    DataSet dsGetBatchnos = objsql.ExecuteDataSet("select S.BATCHNO,BATCHNAME from ACD_Course_TEACHER T inner join ACD_BATCH S on T.BATCHNO=S.BATCHNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + " AND ISNULL(IS_THPR_BOTH,0)=2 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=0 AND ISNULL(T.CANCEL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString()); // Added By Rahul
                    // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                    for (int i = 0; i < dsGetBatchnos.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < lstbxBatch.Items.Count; j++)
                        {
                            if (lstbxBatch.Items[j].Value.ToString().Split('-')[1] == dsGetBatchnos.Tables[0].Rows[i]["BATCHNO"].ToString())
                            {
                                lstbxBatch.Items[j].Selected = true;

                            }
                        }
                    }
                    //to get selected additional teachers for batches
                    //DataSet dsGetBatchesADTeacher = objsql.ExecuteDataSet("select distinct T.BATCHNO,S.SECTIONNO,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_BATCH S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END)"); // Commented By Rahul 
                    DataSet dsGetBatchesADTeacher = objsql.ExecuteDataSet("select distinct T.BATCHNO,S.SECTIONNO,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_BATCH S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + " AND ISNULL(IS_THPR_BOTH,0)=2 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=0 AND ISNULL(T.CANCEL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString()); // Added By Rahul

                    //for sections and additional teachers
                    for (int k = 0; k < dsGetBatchesADTeacher.Tables[0].Rows.Count; k++)
                    {
                        for (int i = 0; i < lstbxBatch.Items.Count; i++)
                        {
                            if (lstbxBatch.Items[i].Selected == true)
                            {
                                if (lstbxBatch.Items[i].Value.ToString().Split('-')[1] == dsGetBatchesADTeacher.Tables[0].Rows[k]["BATCHNO"].ToString())
                                {
                                    if (dsGetBatchesADTeacher.Tables[0].Rows[k]["IS_ADTEACHER"].ToString() == "1")
                                    {
                                        lstbxAdTeacher.Items[i].Selected = true;
                                    }
                                }
                            }
                            else
                            {
                                lstbxAdTeacher.Items[i].Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    ListBox lstbxAdTeacher = e.Item.FindControl("lstbxAdTeacher") as ListBox;
                    lstbxAdTeacher.DataValueField = "SECTIONNO";
                    lstbxAdTeacher.DataTextField = "SECTIONNAME";
                    lstbxAdTeacher.DataSource = ds.Tables[0];
                    lstbxAdTeacher.DataBind();

                    //to get selected sections
                    DataSet dsGetSections = objsql.ExecuteDataSet("select S.SECTIONNO,SectionName,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_THPR_BOTH,0)=2 AND ISNULL(IS_TUTORIAL,0)=0");//(CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END) AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + "");
                    // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                    for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < lstbxSections.Items.Count; j++)
                        {
                            if (lstbxSections.Items[j].Value.ToString() == dsGetSections.Tables[0].Rows[i]["SECTIONNO"].ToString())
                            {
                                //lstbxSections_SelectedIndexChanged(this, EventArgs.Empty);
                                lstbxSections.Items[j].Selected = true;
                            }
                        }
                    }

                    //to get selected additional teachers //for sections
                    DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_THPR_BOTH,0)=2 AND ISNULL(IS_TUTORIAL,0)=0");//CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END)");
                    //for sections and additional teachers
                    for (int k = 0; k < dsGetADTeacher.Tables[0].Rows.Count; k++)
                    {
                        for (int i = 0; i < lstbxSections.Items.Count; i++)
                        {
                            if (lstbxSections.Items[i].Selected == true)
                            {
                                if (lstbxSections.Items[i].Value.ToString() == dsGetADTeacher.Tables[0].Rows[k]["SECTIONNO"].ToString())
                                {
                                    if (dsGetADTeacher.Tables[0].Rows[k]["IS_ADTEACHER"].ToString() == "1")
                                    {
                                        lstbxAdTeacher.Items[i].Selected = true;
                                    }
                                }
                            }
                            else
                            {
                                lstbxAdTeacher.Items[i].Enabled = false;
                            }
                        }
                    }
                }
            }
            #endregion
            #region Theory Subjact Type
            else if (ddlTutorial.SelectedValue == "1")
            {

                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                {
                    ListBox lstbxAdTeacher = e.Item.FindControl("lstbxAdTeacher") as ListBox;
                    lstbxAdTeacher.DataValueField = "SECTIONNO";
                    lstbxAdTeacher.DataTextField = "SECTIONNAME";
                    lstbxAdTeacher.DataSource = ds.Tables[0];
                    lstbxAdTeacher.DataBind();

                    //to get selected sections
                    DataSet dsGetSections = objsql.ExecuteDataSet("select S.SECTIONNO,SectionName,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_THPR_BOTH,0)=1 AND ISNULL(IS_TUTORIAL,0)=0");//(CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END) AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + "");
                    // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                    for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < lstbxSections.Items.Count; j++)
                        {
                            if (lstbxSections.Items[j].Value.ToString() == dsGetSections.Tables[0].Rows[i]["SECTIONNO"].ToString())
                            {
                                //lstbxSections_SelectedIndexChanged(this, EventArgs.Empty);
                                lstbxSections.Items[j].Selected = true;
                            }
                        }
                    }

                    //to get selected additional teachers //for sections
                    DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_THPR_BOTH,0)=1 AND ISNULL(IS_TUTORIAL,0)=0");//CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END)");
                    //for sections and additional teachers
                    for (int k = 0; k < dsGetADTeacher.Tables[0].Rows.Count; k++)
                    {
                        for (int i = 0; i < lstbxSections.Items.Count; i++)
                        {
                            if (lstbxSections.Items[i].Selected == true)
                            {
                                if (lstbxSections.Items[i].Value.ToString() == dsGetADTeacher.Tables[0].Rows[k]["SECTIONNO"].ToString())
                                {
                                    if (dsGetADTeacher.Tables[0].Rows[k]["IS_ADTEACHER"].ToString() == "1")
                                    {
                                        lstbxAdTeacher.Items[i].Selected = true;
                                    }
                                }
                            }
                            else
                            {
                                lstbxAdTeacher.Items[i].Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    DataSet dsGetBatches = objsql.ExecuteDataSet("select (Cast(B.SECTIONNO as varchar)+'-'+cast(BATCHNO as Varchar)) as BATCHNO,SECTIONNAME+' - '+BATCHNAME BATCHNAME from ACD_BATCH B INNER JOIN ACD_SECTION S ON (B.SECTIONNO=S.SECTIONNO) where BATCHNO>0 order by SECTIONNAME");

                    lstbxBatch.DataValueField = "BATCHNO";
                    lstbxBatch.DataTextField = "BATCHNAME";
                    lstbxBatch.DataSource = dsGetBatches.Tables[0];
                    lstbxBatch.DataBind();

                    ListBox lstbxAdTeacher = e.Item.FindControl("lstbxAdTeacher") as ListBox;
                    lstbxAdTeacher.DataValueField = "BATCHNO";
                    lstbxAdTeacher.DataTextField = "BATCHNAME";
                    lstbxAdTeacher.DataSource = dsGetBatches.Tables[0];
                    lstbxAdTeacher.DataBind();

                    DataSet dsGetBatchnos = objsql.ExecuteDataSet("select S.BATCHNO,BATCHNAME from ACD_Course_TEACHER T inner join ACD_BATCH S on T.BATCHNO=S.BATCHNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_THPR_BOTH,0)=1 AND ISNULL(IS_TUTORIAL,0)=0");//(CASE WHEN " + ddlTutorial.SelectedValue + "=2 THEN 1 ELSE 0 END) AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + "");
                    // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                    for (int i = 0; i < dsGetBatchnos.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < lstbxBatch.Items.Count; j++)
                        {
                            if (lstbxBatch.Items[j].Value.ToString().Split('-')[1] == dsGetBatchnos.Tables[0].Rows[i]["BATCHNO"].ToString())
                            {
                                lstbxBatch.Items[j].Selected = true;

                            }
                        }
                    }

                    DataSet dsGetBatchesADTeacher = objsql.ExecuteDataSet("select distinct T.BATCHNO,S.SECTIONNO,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_BATCH S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + " AND ISNULL(IS_THPR_BOTH,0)=1 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=0 AND ISNULL(T.CANCEL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString()); // Added By Rahul

                    //for sections and additional teachers
                    for (int k = 0; k < dsGetBatchesADTeacher.Tables[0].Rows.Count; k++)
                    {
                        for (int i = 0; i < lstbxBatch.Items.Count; i++)
                        {
                            if (lstbxBatch.Items[i].Selected == true)
                            {
                                if (lstbxBatch.Items[i].Value.ToString().Split('-')[1] == dsGetBatchesADTeacher.Tables[0].Rows[k]["BATCHNO"].ToString())
                                {
                                    if (dsGetBatchesADTeacher.Tables[0].Rows[k]["IS_ADTEACHER"].ToString() == "1")
                                    {
                                        lstbxAdTeacher.Items[i].Selected = true;
                                    }
                                }
                            }
                            else
                            {
                                lstbxAdTeacher.Items[i].Enabled = false;
                            }
                        }
                    }
                }
            }
            #endregion
            //}//close if
            #region Tutorial Subject
            else
            {

                DataSet dsGetBatches = objsql.ExecuteDataSet("select (Cast(B.SECTIONNO as varchar)+'-'+cast(BATCHNO as Varchar)) as BATCHNO,SECTIONNAME+' - '+BATCHNAME BATCHNAME from ACD_BATCH B INNER JOIN ACD_SECTION S ON (B.SECTIONNO=S.SECTIONNO) where BATCHNO>0 order by SECTIONNAME");

                lstbxBatch.DataValueField = "BATCHNO";
                lstbxBatch.DataTextField = "BATCHNAME";
                lstbxBatch.DataSource = dsGetBatches.Tables[0];
                lstbxBatch.DataBind();

                ListBox lstbxAdTeacher = e.Item.FindControl("lstbxAdTeacher") as ListBox;
                lstbxAdTeacher.DataValueField = "BATCHNO";
                lstbxAdTeacher.DataTextField = "BATCHNAME";
                lstbxAdTeacher.DataSource = dsGetBatches.Tables[0];
                lstbxAdTeacher.DataBind();


                //to get selected sections
                DataSet dsGetSections = objsql.ExecuteDataSet("select S.SECTIONNO,SectionName from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(IS_THPR_BOTH,0)=3 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=1 AND ISNULL(T.CANCEL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString());
                // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < lstbxSections.Items.Count; j++)
                    {
                        if (lstbxSections.Items[j].Selected == true)
                        {
                            lstbxSections.Items[j].Selected = true;
                        }
                        if (lstbxSections.Items[j].Value.ToString() == dsGetSections.Tables[0].Rows[i]["SECTIONNO"].ToString())
                        {
                            lstbxSections.Items[j].Selected = true;

                        }
                    }
                }

                ////to get selected sections
                DataSet dsGetBatchnos = objsql.ExecuteDataSet("select S.BATCHNO,BATCHNAME from ACD_Course_TEACHER T inner join ACD_BATCH S on T.BATCHNO=S.BATCHNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(IS_THPR_BOTH,0)=3 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=1 AND ISNULL(T.CANCEL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString());
                // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
                for (int i = 0; i < dsGetBatchnos.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < lstbxBatch.Items.Count; j++)
                    {
                        if (lstbxBatch.Items[j].Value.ToString().Split('-')[1] == dsGetBatchnos.Tables[0].Rows[i]["BATCHNO"].ToString())
                        {
                            lstbxBatch.Items[j].Selected = true;

                        }
                    }
                }

                #region Comment
                //to get selected batches
                //DataSet dsGetBatchesDetails = objsql.ExecuteDataSet("select distinct T.BATCHNO,S.SECTIONNO from ACD_Course_TEACHER T inner join ACD_BATCH S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0");
                //for (int i = 0; i < dsGetBatchesDetails.Tables[0].Rows.Count; i++)
                //{
                //    for (int j = 0; j < lstbxBatch.Items.Count; j++)
                //    {
                //        if (lstbxBatch.Items[j].Value.ToString().Split('-')[1] == dsGetBatchesDetails.Tables[0].Rows[i]["BATCHNO"].ToString())
                //        {
                //            lstbxBatch.Items[j].Selected = true;
                //            //if ("1" == dsGetBatchesDetails.Tables[0].Rows[i]["IS_ADTEACHER"].ToString())
                //            //{
                //            //    lstbxAdTeacher.Items[j].Selected = true;
                //            //}
                //        }
                //    }
                //}
                #endregion
                //to get selected additional teachers for batches
                //DataSet dsGetBatchesADTeacher = objsql.ExecuteDataSet("select distinct T.BATCHNO,S.SECTIONNO,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_BATCH S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(T.CANCEL,0)=0 AND T.TH_PR=" + dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() + " AND ISNULL(IS_TUTORIAL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString());
                DataSet dsGetBatchesADTeacher = objsql.ExecuteDataSet("select distinct T.BATCHNO,S.SECTIONNO,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_BATCH S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip + "AND ISNULL(IS_THPR_BOTH,0)=3 AND T.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SESSIONNO = " + Convert.ToInt32(ddlSessionBulk.SelectedValue) + " AND ISNULL(IS_TUTORIAL,0)=1 AND ISNULL(T.CANCEL,0)=0");// + dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString()); // Added By Rahul    
                //for sections and additional teachers
                for (int k = 0; k < dsGetBatchesADTeacher.Tables[0].Rows.Count; k++)
                {
                    for (int i = 0; i < lstbxBatch.Items.Count; i++)
                    {
                        if (lstbxBatch.Items[i].Selected == true)
                        {
                            if (lstbxBatch.Items[i].Value.ToString().Split('-')[1] == dsGetBatchesADTeacher.Tables[0].Rows[k]["BATCHNO"].ToString())
                            {
                                if (dsGetBatchesADTeacher.Tables[0].Rows[k]["IS_ADTEACHER"].ToString() == "1")
                                {
                                    lstbxAdTeacher.Items[i].Selected = true;
                                }
                            }
                        }
                        else
                        {
                            lstbxAdTeacher.Items[i].Enabled = false;
                        }
                    }
                }
            }//close else
            #endregion
        }
        catch
        {
            throw;
        }


    }

    CustomStatus cs;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int count1 = 0;
        int ADT = 0;
        DataTable dt_TEACHER_SEC = new DataTable("TEACHER_SECTION");
        dt_TEACHER_SEC.Columns.Add(new DataColumn("UA_NO", typeof(int)));
        dt_TEACHER_SEC.Columns.Add(new DataColumn("SECTIONNO", typeof(int)));
        dt_TEACHER_SEC.Columns.Add(new DataColumn("ADSTATUS", typeof(int)));

        DataTable dt_Batch_ADT = new DataTable("BATCH_ADT");
        dt_Batch_ADT.Columns.Add(new DataColumn("UA_NO", typeof(int)));
        dt_Batch_ADT.Columns.Add(new DataColumn("BATCHNO", typeof(int)));
        dt_Batch_ADT.Columns.Add(new DataColumn("ADSTATUS", typeof(int)));

        foreach (ListViewDataItem dataitem in lvCourseTeacher.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            if (cbRow.Checked == true)
                count1++;
        }
        if (count1 <= 0)
        {
            //ClearControls();
            objCommon.DisplayMessage(this, "Please Select select atleast one faculty", this);
            return;
        }
        try
        {
            DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubject.SelectedValue, "");
            string sectionNos = "", addTeachers = "", batchNos = "", TeacherNos = "";

            foreach (ListViewDataItem lvItem in lvCourseTeacher.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkAccept") as CheckBox;
                ListBox lstSection = lvItem.FindControl("lstbxSections") as ListBox;
                ListBox lstBatch = lvItem.FindControl("lstbxBatch") as ListBox;
                ListBox lstbxAdTeacher = lvItem.FindControl("lstbxAdTeacher") as ListBox;
                HiddenField _subid = lvItem.FindControl("hdnSubid") as HiddenField;
                HiddenField hdnTeacher = lvItem.FindControl("hdnTeacher") as HiddenField;

                objAttendModel.Sessionno = Convert.ToInt32(ddlSessionBulk.SelectedValue);
                objAttendModel.Schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
                objAttendModel.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                objAttendModel.CollegeCode = Convert.ToInt32(Session["colcode"]);
                objAttendModel.CourseNos = Convert.ToInt32(chkBox.ToolTip);
                int subid = Convert.ToInt32(_subid.Value);
                int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
                if (chkBox.Checked == true)
                //if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    // objAttendModel.CourseNos += chkBox.ToolTip + "$";

                    //check subid first
                    //if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
                    //{
                    //if subid=1

                    if (ddlTutorial.SelectedValue == "2")
                    {
                        if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                        {
                            for (int i = 0; i < lstBatch.Items.Count; i++)
                            {
                                if (lstBatch.Items[i].Selected == true)
                                {
                                    //for additional teachers
                                    if (lstbxAdTeacher.Items[i].Selected == true)
                                    {
                                        addTeachers += "1,";
                                        ADT = 1;
                                    }
                                    else
                                    {
                                        addTeachers += "0,";
                                        ADT = 0;
                                    }
                                    int batch;
                                    batchNos += lstBatch.Items[i].Value.ToString().Split('-')[1] + ",";
                                    batch = Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[1]);
                                    dt_Batch_ADT.Rows.Add(Convert.ToInt32(hdnTeacher.Value), batch, ADT);
                                    dt_TEACHER_SEC.Rows.Add(Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[0]), 0);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lstSection.Items.Count; i++)
                            {
                                if (lstSection.Items[i].Selected == true)
                                {
                                    //for additional teachers
                                    if (lstbxAdTeacher.Items[i].Selected == true)
                                    {
                                        addTeachers += "1,";
                                        ADT = 1;
                                    }
                                    else
                                    {
                                        addTeachers += "0,";
                                        ADT = 0;
                                    }

                                    sectionNos += lstSection.Items[i].Value + ",";
                                    dt_TEACHER_SEC.Rows.Add(Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(lstSection.Items[i].Value), ADT);
                                }
                            }
                        }
                    }
                    else if (ddlTutorial.SelectedValue == "3")
                    {
                        for (int i = 0; i < lstBatch.Items.Count; i++)
                        {
                            if (lstBatch.Items[i].Selected == true)
                            {
                                //for additional teachers
                                if (lstbxAdTeacher.Items[i].Selected == true)
                                {
                                    addTeachers += "1,";
                                    ADT = 1;
                                }
                                else
                                {
                                    addTeachers += "0,";
                                    ADT = 0;
                                }
                                int batch;
                                batchNos += lstBatch.Items[i].Value.ToString().Split('-')[1] + ",";
                                batch = Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[1]);
                                dt_Batch_ADT.Rows.Add(Convert.ToInt32(hdnTeacher.Value), batch, ADT);
                                dt_TEACHER_SEC.Rows.Add(Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[0]), 0);
                            }
                        }
                    }
                    else
                    {
                        batchNos = "0";
                        if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                        {
                            //for sections and additional teachers
                            for (int i = 0; i < lstSection.Items.Count; i++)
                            {
                                if (lstSection.Items[i].Selected == true)
                                {
                                    //for additional teachers
                                    if (lstbxAdTeacher.Items[i].Selected == true)
                                    {
                                        addTeachers += "1,";
                                        ADT = 1;
                                    }
                                    else
                                    {
                                        addTeachers += "0,";
                                        ADT = 0;
                                    }

                                    sectionNos += lstSection.Items[i].Value + ",";
                                    dt_TEACHER_SEC.Rows.Add(Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(lstSection.Items[i].Value), ADT);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lstBatch.Items.Count; i++)
                            {
                                if (lstBatch.Items[i].Selected == true)
                                {
                                    //for additional teachers
                                    if (lstbxAdTeacher.Items[i].Selected == true)
                                    {
                                        addTeachers += "1,";
                                        ADT = 1;
                                    }
                                    else
                                    {
                                        addTeachers += "0,";
                                        ADT = 0;
                                    }
                                    int batch;
                                    batchNos += lstBatch.Items[i].Value.ToString().Split('-')[1] + ",";
                                    batch = Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[1]);
                                    dt_Batch_ADT.Rows.Add(Convert.ToInt32(hdnTeacher.Value), batch, ADT);
                                    dt_TEACHER_SEC.Rows.Add(Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[0]), 0);
                                }
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    //if subid=2

                    //    for (int i = 0; i < lstSection.Items.Count; i++)
                    //    {
                    //        if (lstSection.Items[i].Selected == true)
                    //        {
                    //            sectionNos += lstSection.Items[i].Value + ",";
                    //        }
                    //    }

                    //    for (int i = 0; i < lstBatch.Items.Count; i++)
                    //    {
                    //        if (lstBatch.Items[i].Selected == true)
                    //        {
                    //            //for additional teachers
                    //            if (lstbxAdTeacher.Items[i].Selected == true)
                    //            {
                    //                addTeachers += "1,";
                    //                ADT = 1;
                    //            }
                    //            else
                    //            {
                    //                addTeachers += "0,";
                    //                ADT = 0;
                    //            }
                    //            int batch;
                    //            batchNos += lstBatch.Items[i].Value.ToString().Split('-')[1] + ",";
                    //            batch = Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[1]);
                    //            dt_Batch_ADT.Rows.Add(Convert.ToInt32(hdnTeacher.Value), batch, ADT);
                    //            dt_TEACHER_SEC.Rows.Add(Convert.ToInt32(hdnTeacher.Value), Convert.ToInt32(lstBatch.Items[i].Value.ToString().Split('-')[0]), 0);
                    //        }
                    //    }


                    //    batchNos = batchNos.TrimEnd(',') + "$";
                    //}




                    sectionNos = sectionNos.TrimEnd(',') + "$";


                    addTeachers = addTeachers.TrimEnd(',') + "$";

                    TeacherNos += hdnTeacher.Value.ToString() + "$";
                    objAttendModel.TeacherNos = TeacherNos.TrimEnd('$');

                    objAttendModel.BatchNos = batchNos.TrimEnd('$');
                    objAttendModel.SectionNos = sectionNos.TrimEnd('$');

                    objAttendModel.Is_ADTeacher = addTeachers.TrimEnd('$');
                    objAttendModel.College_Id = Convert.ToInt32(ViewState["college_id"].ToString());
                }
            }

            //if (Convert.ToInt32(Session["OrgId"]) == 2)
            //{

            //    int subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + ddlSubject.SelectedValue));

            //    if (subid == 1)
            //    {
            //        string sect = objAttendModel.SectionNos;
            //        if (sect == "")
            //        {
            //            objCommon.DisplayMessage(this, "Please select atleast one section", this);
            //            return;
            //        }
            //    }
            //}

            CustomStatus cs = (CustomStatus)objAttendC.AddCourseTeacherAllotmentBulk(objAttendModel, dt_TEACHER_SEC, dt_Batch_ADT, Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(Session["OrgId"]));

            this.BindListViewMain();

            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this, "Course Teacher alloted Successfully!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this, "Server Error", this.Page);
            }

        }

        #region commented
        //try
        //{
        //    int flag = 0;
        //    foreach (ListViewDataItem lvItem in lvCourseTeacher.Items)
        //    {
        //        CheckBox chkBox = lvItem.FindControl("chkAccept") as CheckBox;
        //        ListBox lstSection = lvItem.FindControl("lstbxSections") as ListBox;
        //        ListBox lstBatch = lvItem.FindControl("lstbxBatch") as ListBox;
        //        //DropDownList ddlRoom = lvItem.FindControl("ddlRoom") as DropDownList;
        //        if (chkBox.Checked == true)
        //        {
        //            flag = 1;
        //            if (lstSection.SelectedValue == "0" && lstBatch.SelectedValue == "0")
        //            {
        //                objCommon.DisplayMessage(this, "Please Select Section or Batch for the selected Teacher !!", this.Page);
        //                lstSection.Enabled = true;
        //                flag = 0;
        //                return;
        //            }
        //        }
        //    }
        //    if (flag == 0)
        //    {
        //        objCommon.DisplayMessage(this, "Please Select Teacher for the selected course !!", this.Page);
        //        return;
        //    }

        //    objAttendModel.Sessionno = Convert.ToInt32(ddlSessionBulk.SelectedValue);
        //    objAttendModel.Schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
        //    objAttendModel.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        //    objAttendModel.CollegeCode = Convert.ToInt32(Session["colcode"]);

        //    foreach (ListViewDataItem lvItem in lvCourseTeacher.Items)
        //    {
        //        CheckBox chkBox = lvItem.FindControl("chkAccept") as CheckBox;
        //        HiddenField hdnTeacher = lvItem.FindControl("hdnTeacher") as HiddenField;
        //        HiddenField _subid = lvItem.FindControl("hdnSubid") as HiddenField;
        //        ListBox lstSection = lvItem.FindControl("lstbxSections") as ListBox;
        //        ListBox lstBatch = lvItem.FindControl("lstbxBatch") as ListBox;
        //        if (chkBox.Checked == true  && chkBox.Enabled==true)
        //        {
        //            objAttendModel.CourseNos = chkBox.ToolTip;
        //            objAttendModel.TeacherNos += hdnTeacher.Value+",";//ddlTeacher.SelectedValue;


        //            if (Convert.ToInt32(_subid.Value) != 2)
        //                objAttendModel.BatchNos = "0";
        //            else
        //                objAttendModel.BatchNos = lstBatch.SelectedValue;



        //            string section, ADTeachers;
        //            ListBox lstbxSections = lvItem.FindControl("lstbxSections") as ListBox;
        //            ListBox lstbxAdTeacher = lvItem.FindControl("lstbxAdTeacher") as ListBox;
        //            for (int i = 0; i < lstbxSections.Items.Count; i++)
        //            {
        //                if (lstbxAdTeacher.Items[i].Selected == true)
        //                {
        //                    ADTeachers = "1";
        //                    objAttendModel.Is_ADTeacher = ADTeachers;
        //                }
        //                else
        //                {
        //                    ADTeachers = "0";
        //                    objAttendModel.Is_ADTeacher = ADTeachers;
        //                }
        //                if (lstbxSections.Items[i].Selected == true)
        //                {
        //                    section = lstbxSections.Items[i].Value.ToString();
        //                    objAttendModel.SectionNos = section;
        //                    cs = (CustomStatus)objAttendC.AddCourseTeacherAllotmentBulk(objAttendModel);
        //                }
        //            }
        //        }
        //    }

        //    if (objAttendModel.CourseNos.Length <= 0)
        //    {
        //        objCommon.DisplayMessage(this, "Please select Course", this.Page);
        //        return;
        //    }
        //   // CustomStatus cs = (CustomStatus)objAttendC.AddCourseTeacherAllotmentBulk(objAttendModel);
        //    this.BindListViewMain();

        //    if (cs == CustomStatus.RecordSaved)
        //    {
        //        objCommon.DisplayMessage(this, "Course Teacher alloted Successfully!!", this.Page);
        //    }
        //    else
        //        objCommon.DisplayMessage(this, "Server Error", this.Page);

       // }
        #endregion

        catch
        {
            throw;
        }
    }

    protected void lstbxSections_SelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (ListViewDataItem dataitem in lvCourseTeacher.Items)
        {
            ListBox lst_Section = dataitem.FindControl("lstbxSections") as ListBox;
            ListBox lst_AD_Teacher = dataitem.FindControl("lstbxAdTeacher") as ListBox;
            ListBox lstbxBatch = dataitem.FindControl("lstbxBatch") as ListBox;
            HiddenField _subid = dataitem.FindControl("hdnSubid") as HiddenField;
            int subid = Convert.ToInt32(_subid.Value);
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            //check subid first
            if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
            {

                int k = 0;
                while (k < lstbxBatch.Items.Count)
                {
                    lstbxBatch.Items[k].Enabled = false;
                    lst_AD_Teacher.Items[k].Enabled = false;
                    k++;
                }
                k = 0;
            }
            else
            {

                int k = 0;
                while (k < lst_AD_Teacher.Items.Count)
                {
                    lst_AD_Teacher.Items[k].Enabled = false;
                    k++;
                }
                k = 0;
            }



            int j = 0, batch = 0;

            for (int i = 0; i < lst_Section.Items.Count; i++)
            {
                if (lst_Section.Items[i].Selected == true)
                {

                    //check subid first
                    if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
                    {

                        while (j < lst_AD_Teacher.Items.Count)
                        {
                            lst_AD_Teacher.Items[j].Enabled = false;
                            j++;
                        }
                        lst_AD_Teacher.Items[i].Enabled = true;

                        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.abc').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.abc').hide();$('td:nth-child(4)').hide();});", true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(4)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#Section1').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').show();$('td:nth-child(3)').show();});", true);
                    }
                    else
                    {

                        while (batch < lstbxBatch.Items.Count)
                        {
                            if (lst_Section.Items[i].Value == lstbxBatch.Items[batch].Value.ToString().Split('-')[0])
                            {
                                lstbxBatch.Items[batch].Enabled = true;
                                lst_AD_Teacher.Items[batch].Enabled = true;
                            }
                            batch++;
                        }
                        batch = 0;
                        // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.abc').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.abc').show();$('td:nth-child(4)').show();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('td:nth-child(4)').show();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#Section1').hide();$('td:nth-child(3)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').hide();$('td:nth-child(3)').hide();});", true);

                    }
                }

            }

        }
    }

    protected void lstbxBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (ListViewDataItem dataitem in lvCourseTeacher.Items)
        {
            //  ListBox lst_Section = dataitem.FindControl("lstbxSections") as ListBox;
            ListBox lst_AD_Teacher = dataitem.FindControl("lstbxAdTeacher") as ListBox;
            ListBox lstbxBatch = dataitem.FindControl("lstbxBatch") as ListBox;
            HiddenField _subid = dataitem.FindControl("hdnSubid") as HiddenField;
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            int subid = Convert.ToInt32(_subid.Value);
            //check subid first
            if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
            {
                int k = 0;
                while (k < lst_AD_Teacher.Items.Count)
                {
                    lst_AD_Teacher.Items[k].Enabled = false;
                    k++;
                }
                k = 0;
            }


            int j = 0;

            for (int i = 0; i < lstbxBatch.Items.Count; i++)
            {
                if (lstbxBatch.Items[i].Selected == true)
                {

                    while (j < lst_AD_Teacher.Items.Count)
                    {
                        lst_AD_Teacher.Items[j].Enabled = false;
                        j++;
                    }
                    lst_AD_Teacher.Items[i].Enabled = true;
                }

            }

        }
        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.abc').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.abc').show();$('td:nth-child(4)').show();});", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('td:nth-child(4)').show();});", true);

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void chkDept_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkDept.Checked == true)
        {
            divOtherDepartment.Visible = true;
            objCommon.FillDropDownList(ddlOtherDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO NOT IN(" + ddldepartment.SelectedValue + ")", "DEPTNAME ASC");
        }
        else
        {
            divOtherDepartment.Visible = false;
            ddlOtherDepartment.Items.Clear();
            ddlOtherDepartment.Items.Add("Please Select");
            ddlOtherDepartment.SelectedItem.Value = "0";
        }
        this.clearATListView();
        btnSave.Visible = false;
    }

    private void ClearControls()
    {
        ddlSessionBulk.SelectedIndex = 0;
        ddlSchoolInstitute.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;

        chkDept.Checked = false;
        ddldepartment.SelectedIndex = 0;
        this.clearATListView();
        btnSave.Visible = false;
        chkCheck.Visible = false;
        chkDept.Checked = false;
        divOtherDepartment.Visible = false;
        ddlOtherDepartment.SelectedIndex = 0;
    }

    #endregion

    #region For Additional Teacher

    //For Additional Teacher
    protected void ddlSchemeAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchemeAT.SelectedValue));
            //ViewState["degreeno"]

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemenoat"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            this.clearATListView();
            ClearDropDown(ddlSemesterAT);
            ClearDropDown(ddlSubjectAT);
            ClearDropDown(ddlSectionAT);
            ClearDropDown(ddlBatchesAT);
            ClearDropDown(ddlSessionAT);
            ClearDropDown(ddlDeptAT);
            divDropOtherDepartmentAT.Visible = false;
            chkDeptAT.Checked = false;

            btnSaveAT.Visible = false;
            if (ddlSchemeAT.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSessionAT, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                ddlSessionAT.Focus();
            }
            //string _deptNo = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + ddlSchemeAT.SelectedValue) == string.Empty ? "0" : objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + ddlSchemeAT.SelectedValue);
            //ddlDeptAT.SelectedValue = _deptNo;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSessionAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSchoolInstituteAT.SelectedIndex = 0;
        ddlDeptAT.SelectedIndex = 0;
        divDropOtherDepartmentAT.Visible = false;
        chkDeptAT.Checked = false;

        ClearDropDown(ddlDegreeAT);
        ClearDropDown(ddlBranchAT);
        ClearDropDown(ddlSemesterAT);
        ClearDropDown(ddlSubjectAT);
        ClearDropDown(ddlSectionAT);
        ClearDropDown(ddlBatchesAT);
        btnSaveAT.Visible = false;
        //if (Session["usertype"].ToString() != "1")
        //{
        //    objCommon.FillDropDownList(ddlDeptAT, "ACD_DEPARTMENT DP WITH (NOLOCK)  INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEPTNO=DP.DEPTNO", "DISTINCT DP.DEPTNO", "DEPTNAME", "DP.DEPTNO>0 and DP.DEPTNO IN(" + Session["userdeptno"].ToString() + ") AND CDB.COLLEGE_ID IN(" + Session["college_nos"] + ")", "DEPTNAME ASC");
        //}
        //else
        //{
        objCommon.FillDropDownList(ddlDeptAT, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
        // objCommon.FillDropDownList(ddlDeptAT, "ACD_DEPARTMENT DP WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEPTNO=DP.DEPTNO", "DISTINCT DP.DEPTNO", "DEPTNAME", "DP.DEPTNO>0 AND ISNULL(DP.ACTIVESTATUS,0)=1 AND CDB.COLLEGE_ID="+ViewState["college_id"].ToString(), "DEPTNAME ASC");
        //}
        ddlDeptAT.Focus();
        this.clearATListView();
    }

    protected void ddlSchoolInstituteAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlDeptAT, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + ddlSchoolInstituteAT.SelectedValue + "", "A.DEPTNAME ASC");
            ClearDropDown(ddlDegreeAT);
            ClearDropDown(ddlBranchAT);
            ClearDropDown(ddlSchemeAT);
            ClearDropDown(ddlSemesterAT);
            ClearDropDown(ddlSubjectAT);
            ClearDropDown(ddlSectionAT);
            ClearDropDown(ddlBatchesAT);
            btnSaveAT.Visible = false;
            this.clearATListView();
            divDropOtherDepartmentAT.Visible = false;
            divChkDeptAT.Visible = false;
        }
        else
        {
            ddlDeptAT.SelectedIndex = 0;
            ClearDropDown(ddlDegreeAT);
            ClearDropDown(ddlBranchAT);
            ClearDropDown(ddlSchemeAT);
            ClearDropDown(ddlSemesterAT);
            ClearDropDown(ddlSubjectAT);

            btnSaveAT.Visible = false;
            this.clearATListView();

            divDropOtherDepartmentAT.Visible = false;
            divChkDeptAT.Visible = true;
            chkDeptAT.Checked = false;
        }


    }

    protected void ddlDeptAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlDegreeAT, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstituteAT.SelectedValue + " AND A.DEPTNO=" + ddlDeptAT.SelectedValue, "B.DEGREENAME ASC");
        if (Session["usertype"].ToString() != "1")
        {
        }
        else
        {
        }
        ClearDropDown(ddlBranch);
        //ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemesterAT);
        ClearDropDown(ddlSubjectAT);
        ClearDropDown(ddlSectionAT);
        ClearDropDown(ddlBatchesAT);
        btnSaveAT.Visible = false;
        this.clearATListView();

        divDropOtherDepartmentAT.Visible = false;
        chkDeptAT.Checked = false;

        string odd_even = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(ddlSessionAT.SelectedValue));
        string exam_type = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSessionAT.SelectedValue));
        string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON B.BRANCHNO=A.BRANCHNO", "CAST(DURATION AS INT)*2 AS DURATION", "B.BRANCHNO=" + ViewState["branchno"].ToString() + " AND A.DEGREENO=" + ViewState["degreeno"].ToString());

        if (exam_type == "1" && odd_even != "3")
        {
            //Commented by As per Requirement Of Romal Saluja Sir

            //objCommon.FillDropDownList(ddlSemesterAT, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "S.COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND  SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNAME asc");

            ddlSemesterAT.Items.Clear();
            ddlSemesterAT.Items.Add(new ListItem("Please Select", "0"));
            int Schemeno = Convert.ToInt32(ViewState["schemenoat"]);
            int SessionNo = Convert.ToInt32(ddlSessionAT.SelectedValue);
            DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlSemesterAT.DataSource = ds;
                ddlSemesterAT.DataTextField = "SEMESTERNAME";
                ddlSemesterAT.DataValueField = "SEMESTERNO";
                ddlSemesterAT.DataBind();
            }

        }
        else
        {
            objCommon.FillDropDownList(ddlSemesterAT, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)",
                "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ",
                "S.COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND SM.SEMESTERNO<=" + semCount +
                 " AND ACTIVESTATUS=1 UNION SELECT DISTINCT SEMESTERNO,SEMESTERNAME FROM ACD_SEMESTER WHERE SEMESTERNAME like '%SUMMER%' AND ACTIVESTATUS=1", // added by Shailendra K. on dated 08.05.2024 as per Dr. Manoj sir Suggestion for summer semester.
                "");
        }

        ddlSemesterAT.Focus();
    }

    protected void ddlDegreeAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegreeAT.SelectedIndex > 0)
        //{
        //    //lvCourseTeacher.DataSource = null;
        //    //   lvCourseTeacher.DataBind();
        objCommon.FillDropDownList(ddlBranchAT, "ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "A.COLLEGE_ID=" + ddlSchoolInstituteAT.SelectedValue + " AND A.DEPTNO=" + ddlDeptAT.SelectedValue + " AND DEGREENO = " + ddlDegreeAT.SelectedValue, "B.LONGNAME asc");
        ddlBranchAT.Focus();
        //}

        ClearDropDown(ddlSchemeAT);
        ClearDropDown(ddlSemesterAT);
        ClearDropDown(ddlSubjectAT);
        //ddlBranchAT.SelectedIndex = 0;
        //ddlSchemeAT.SelectedIndex = 0;
        //ddlSemesterAT.SelectedIndex = 0;
        btnSaveAT.Visible = false;
        //ddlSubjectAT.SelectedIndex = 0;
        //ddlDeptAT.SelectedIndex = 0;

        this.clearATListView();
        divDropOtherDepartmentAT.Visible = false;
        chkDeptAT.Checked = false;
    }

    protected void ddlBranchAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBranchAT.SelectedIndex > 0)
        //{
        objCommon.FillDropDownList(ddlSchemeAT, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegreeAT.SelectedValue + " AND DEPTNO=" + ddlDeptAT.SelectedValue + " AND BRANCHNO = " + ddlBranchAT.SelectedValue, "SCHEMENAME asc");
        ddlSchemeAT.Focus();
        //}

        ClearDropDown(ddlSemesterAT);
        ClearDropDown(ddlSubjectAT);
        ClearDropDown(ddlSectionAT);
        ClearDropDown(ddlBatchesAT);

        //ddlSemesterAT.SelectedIndex = 0;
        //ddlSubjectAT.SelectedIndex = 0;
        //ddlDeptAT.SelectedIndex = 0;

        btnSaveAT.Visible = false;
        this.clearATListView();

        divDropOtherDepartmentAT.Visible = false;
        chkDeptAT.Checked = false;
    }



    protected void ddlSemesterAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.clearATListView();
        ClearDropDown(ddlSubjectAT);
        ClearDropDown(ddlSectionAT);
        ClearDropDown(ddlBatchesAT);
        //ddlSubjectAT.SelectedIndex = 0;
        //ddlDeptAT.SelectedIndex = 0;
        btnSaveAT.Visible = false;
        divDropOtherDepartmentAT.Visible = false;
        chkDeptAT.Checked = false;

        if (ddlSemesterAT.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubjectAT, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_OFFERED_COURSE O WITH (NOLOCK) ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO) LEFT JOIN ACD_COURSE_TEACHER T ON (T.COURSENO=O.COURSENO AND T.SEMESTERNO = O.SEMESTERNO AND T.SCHEMENO=O.SCHEMENO AND T.SESSIONNO=O.SESSIONNO AND ISNULL(T.IS_ADTEACHER,0)=1)", "distinct O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND O.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND O.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0", "O.COURSENO");
            ddlBranch.Focus();
        }
    }

    protected void ddlSubjectAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown(ddlSectionAT);
            ClearDropDown(ddlBatchesAT);
            //decimal tutorial = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "ISNULL(THEORY,0)", "COURSENO=" + ddlSubjectAT.SelectedValue));
            //if (tutorial > 0)
            //{
            //    divTutorialAT.Visible = true;
            //    ddlTutorialAT.SelectedValue = "1";
            //}
            //else
            //{
            //    divTutorialAT.Visible = false;
            //    ddlTutorialAT.SelectedValue = "1";
            //}
            ddlTutorialAT.Items.Clear();
            DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(THEORY,0) AS THEORY,TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubjectAT.SelectedValue, "");
            //if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
            if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1"))
            {
                divTutorialAT.Visible = false;
                ddlTutorialAT.Items.Add(new ListItem("Theory", "1"));
                ddlTutorialAT.SelectedValue = "1";
            }
            //else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
            else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1"))
            {
                if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                {
                    divTutorialAT.Visible = true;
                    ddlTutorialAT.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorialAT.Items.Add(new ListItem("Tutorial", "3"));
                    ddlTutorialAT.SelectedValue = "1";
                }
                else
                {
                    divTutorialAT.Visible = false;
                    ddlTutorialAT.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorialAT.SelectedValue = "1";
                }
            }
            //else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
            else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2"))
            {
                divTutorialAT.Visible = false;
                ddlTutorialAT.Items.Add(new ListItem("Practical", "2"));
                ddlTutorialAT.SelectedValue = "2";
            }
            else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2"))
            {
                if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                {
                    divTutorialAT.Visible = true;
                    ddlTutorialAT.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorialAT.Items.Add(new ListItem("Tutorial", "3"));
                    ddlTutorialAT.SelectedValue = "2";
                }
                else
                {
                    divTutorialAT.Visible = false;
                    ddlTutorialAT.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorialAT.SelectedValue = "2";
                }
            }
            else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
            {
                divTutorialAT.Visible = true;
                ddlTutorialAT.Items.Add(new ListItem("Theory", "1"));
                ddlTutorialAT.Items.Add(new ListItem("Practical", "2"));
                ddlTutorialAT.SelectedValue = "1";
            }
            else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
            {
                if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                {
                    divTutorialAT.Visible = true;
                    ddlTutorialAT.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorialAT.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorialAT.Items.Add(new ListItem("Tutorial", "3"));
                    ddlTutorialAT.SelectedValue = "1";
                }
                else
                {
                    divTutorialAT.Visible = true;
                    ddlTutorialAT.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorialAT.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorialAT.SelectedValue = "1";
                }
            }
            // ddlBatchesAT.SelectedIndex = -1;
            if (ddlSubjectAT.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSectionAT, "ACD_SECTION  S WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON(T.SECTIONNO=S.SECTIONNO)  ", "DISTINCT T.SECTIONNO", "S.SECTIONNAME", "T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and T.COURSENO=" + ddlSubjectAT.SelectedValue + " AND ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue, "S.SECTIONNAME asc");
                ddlBranch.Focus();
            }

            int subID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "SUBID", "COURSENO=" + ddlSubjectAT.SelectedValue));
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            //if ((subID != 2 && subID != 11 && OrgId == 1) || ((subID != 2 && subID != 12 && subID != 4 && subID != 15 && OrgId == 2)) || (subID != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
            //{
            //    BatchAT.Visible = false;
            //}
            //else
            //{
            //    BatchAT.Visible = true;
            //    //if (ddlSubjectAT.SelectedIndex > 0)
            //    //{
            //    //    objCommon.FillDropDownList(ddlBatchesAT, "ACD_BATCH", "BATCHNO", "BATCHNAME", " BATCHNO>0", "BATCHNO");
            //    //    ddlBranch.Focus();
            //    //}
            //}
            BatchAT.Visible = false;
            divDropOtherDepartmentAT.Visible = false;
            chkDeptAT.Checked = false;
            this.clearATListView();
        }
        catch { throw; }
    }

    protected void ddlSectionAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlDeptAT.SelectedIndex = 0;

            divDropOtherDepartmentAT.Visible = false;
            chkDeptAT.Checked = false;

            this.clearATListView();
            if (ddlSubjectAT.SelectedValue != null)
            {
                int subID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "SUBID", "COURSENO=" + ddlSubjectAT.SelectedValue));

                int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
                //if ((subID != 2 && subID != 11 && OrgId == 1) || ((subID != 2 && subID != 12 && subID != 4 && subID != 15 && OrgId == 2)) || (subID != 2 && OrgId != 1 && OrgId != 2))
                //{
                DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubjectAT.SelectedValue, "");

                if (ddlTutorialAT.SelectedValue == "1")
                {
                    if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                    {
                        BatchAT.Visible = false;
                    }
                    else
                    {
                        BatchAT.Visible = true;
                        ddlBatchesAT.DataSource = null;
                        ddlBatchesAT.DataBind();
                        ddlBatchesAT.SelectedIndex = -1;
                        objCommon.FillDropDownList(ddlBatchesAT, "ACD_BATCH B WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON(T.BATCHNO=B.BATCHNO)", "distinct T.BATCHNO", "B.BATCHNAME", " B.BATCHNO>0 and T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"] + " AND T.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and T.COURSENO=" + ddlSubjectAT.SelectedValue + " and T.SectionNo=" + ddlSectionAT.SelectedValue + " AND ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorialAT.SelectedValue + "=3 THEN 1 ELSE 0 END) AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue, "B.BATCHNAME asc");
                        ddlBranch.Focus();
                    }
                }
                else if (ddlTutorialAT.SelectedValue == "2")
                {
                    if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                    {
                        BatchAT.Visible = true;
                        if (ddlSubjectAT.SelectedIndex > 0)
                        {
                            //ddlBatchesAT.Items.Clear();
                            ddlBatchesAT.DataSource = null;
                            ddlBatchesAT.DataBind();
                            ddlBatchesAT.SelectedIndex = -1;
                            objCommon.FillDropDownList(ddlBatchesAT, "ACD_BATCH B WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON(T.BATCHNO=B.BATCHNO)", "distinct T.BATCHNO", "B.BATCHNAME", " B.BATCHNO>0 and T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"] + " AND T.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and T.COURSENO=" + ddlSubjectAT.SelectedValue + " and T.SectionNo=" + ddlSectionAT.SelectedValue + " AND ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorialAT.SelectedValue + "=3 THEN 1 ELSE 0 END) AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue, "B.BATCHNAME asc");
                            ddlBranch.Focus();
                        }
                    }
                    else
                    {
                        BatchAT.Visible = false;
                    }
                }
                else if (ddlTutorialAT.SelectedValue == "3")
                {
                    BatchAT.Visible = true;
                    if (ddlSubjectAT.SelectedIndex > 0)
                    {
                        //ddlBatchesAT.Items.Clear();
                        ddlBatchesAT.DataSource = null;
                        ddlBatchesAT.DataBind();
                        ddlBatchesAT.SelectedIndex = -1;
                        objCommon.FillDropDownList(ddlBatchesAT, "ACD_BATCH B WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON(T.BATCHNO=B.BATCHNO)", "distinct T.BATCHNO", "B.BATCHNAME", " B.BATCHNO>0 and T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"] + " AND T.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and T.COURSENO=" + ddlSubjectAT.SelectedValue + " and T.SectionNo=" + ddlSectionAT.SelectedValue + " AND ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorialAT.SelectedValue + "=3 THEN 1 ELSE 0 END) AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue, "B.BATCHNAME asc");
                        ddlBranch.Focus();
                    }
                }
                //}
                //else
                //{
                //    BatchAT.Visible = true;
                //    if (ddlSubjectAT.SelectedIndex > 0)
                //    {
                //        //ddlBatchesAT.Items.Clear();
                //        ddlBatchesAT.DataSource = null;
                //        ddlBatchesAT.DataBind();
                //        ddlBatchesAT.SelectedIndex = -1;
                //        objCommon.FillDropDownList(ddlBatchesAT, "ACD_BATCH B WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON(T.BATCHNO=B.BATCHNO)", "distinct T.BATCHNO", "B.BATCHNAME", " B.BATCHNO>0 and T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"] + " AND T.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and T.COURSENO=" + ddlSubjectAT.SelectedValue + " and T.SectionNo=" + ddlSectionAT.SelectedValue + " AND ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0", "B.BATCHNAME asc");
                //        ddlBranch.Focus();
                //    }
                //}

            }
        }
        catch { }
    }

    protected void ddlBatchesAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDeptAT.SelectedIndex = 0;
        this.clearATListView();
    }

    protected void chkDeptAT_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkDeptAT.Checked == true)
        {
            divDropOtherDepartmentAT.Visible = true;
            objCommon.FillDropDownList(ddlOtherDepartmentAT, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO NOT IN(" + ddlDeptAT.SelectedValue + ")", "DEPTNAME ASC");
        }
        else
        {
            divDropOtherDepartmentAT.Visible = false;
            ddlOtherDepartmentAT.Items.Clear();
            ddlOtherDepartmentAT.Items.Add("Please Select");
            ddlOtherDepartmentAT.SelectedItem.Value = "0";
        }
        this.clearATListView();
        btnSaveAT.Visible = false;
    }

    protected void ddlOtherDepartmentAT_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        this.clearATListView();
        btnSaveAT.Visible = false;
    }

    protected void btnClearAT_Click(object sender, EventArgs e)
    {
        ClearControlsAT();
    }
    //for ADTeacher
    private void ClearControlsAT()
    {
        ddlSessionAT.SelectedIndex = 0;
        ddlSchoolInstituteAT.SelectedIndex = 0;
        ddlDegreeAT.SelectedIndex = 0;
        ddlBranchAT.SelectedIndex = 0;
        ddlSchemeAT.SelectedIndex = 0;
        ddlSemesterAT.SelectedIndex = 0;
        ddlSubjectAT.SelectedIndex = 0;
        ddlDeptAT.SelectedIndex = 0;
        ddlSectionAT.SelectedIndex = 0;
        ddlBatchesAT.SelectedIndex = 0;
        updPanel1.Update();
        lvCourseTeacher.DataSource = null;
        lvCourseTeacher.DataBind();
        btnSave.Visible = false;

        updPanel2.Update();
        lvCourseTeacherAT.DataSource = null;
        lvCourseTeacherAT.DataBind();
        btnSaveAT.Visible = false;

        divChkDeptAT.Visible = false;
        chkDeptAT.Checked = false;
        divDropOtherDepartmentAT.Visible = false;
        ddlOtherDepartmentAT.SelectedIndex = 0;
    }

    protected void lvCourseTeacherAT_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int deptNo = 0, subID = 0;
        deptNo = Convert.ToInt32(ddlDeptAT.SelectedValue);

        HiddenField hdnTeacherAT = e.Item.FindControl("hdnTeacherAT") as HiddenField;
        CheckBox chkBoxAT = e.Item.FindControl("chkAcceptAT") as CheckBox;
        DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(TH_PR,0) AS TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubjectAT.SelectedValue, "");
        SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
        //subID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + ddlSubjectAT.SelectedValue));
        //if (subID == 1)
        //{
        //to get selected sections and batchno=0
        if (ddlTutorialAT.SelectedValue == "1") //Theory
        {
            if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
            {
                DataSet dsGetSections = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=0 AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=0 AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue);
                for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                {
                    if (chkBoxAT.Enabled == false && dsGetSections.Tables[0].Rows[i]["Allot_AT"].ToString() == "1")
                    {
                        Label lblSectionAT = e.Item.FindControl("lblSectionAT") as Label;
                        lblSectionAT.Text = dsGetSections.Tables[0].Rows[i]["SECTIONNAME"].ToString();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();});", true);
            }
            else
            {
                DataSet dsGetSections = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,B.BATCHNAME,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON(B.BATCHNO = T.BATCHNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=" + ddlBatchesAT.SelectedValue + " AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=0  AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue);
                for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                {
                    if (chkBoxAT.Enabled == false && dsGetSections.Tables[0].Rows[i]["Allot_AT"].ToString() == "1")
                    {
                        Label lblSectionAT = e.Item.FindControl("lblSectionAT") as Label;
                        lblSectionAT.Text = dsGetSections.Tables[0].Rows[i]["SECTIONNAME"].ToString();

                        Label lblBatchAT = e.Item.FindControl("lblBatchAT") as Label;
                        lblBatchAT.Text = dsGetSections.Tables[0].Rows[i]["BATCHNAME"].ToString();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('td:nth-child(4)').show();});", true);

            }


        }
        else if (ddlTutorialAT.SelectedValue == "2") //Practical
        {
            if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
            {
                //to get selected sections and batches 
                DataSet dsGetSections = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,B.BATCHNAME,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON(B.BATCHNO = T.BATCHNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=" + ddlBatchesAT.SelectedValue + " AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=0  AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue);
                for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                {
                    if (chkBoxAT.Enabled == false && dsGetSections.Tables[0].Rows[i]["Allot_AT"].ToString() == "1")
                    {
                        Label lblSectionAT = e.Item.FindControl("lblSectionAT") as Label;
                        lblSectionAT.Text = dsGetSections.Tables[0].Rows[i]["SECTIONNAME"].ToString();

                        Label lblBatchAT = e.Item.FindControl("lblBatchAT") as Label;
                        lblBatchAT.Text = dsGetSections.Tables[0].Rows[i]["BATCHNAME"].ToString();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('td:nth-child(4)').show();});", true);
            }
            else
            {
                DataSet dsGetSections = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=0 AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=0 AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue);
                for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
                {
                    if (chkBoxAT.Enabled == false && dsGetSections.Tables[0].Rows[i]["Allot_AT"].ToString() == "1")
                    {
                        Label lblSectionAT = e.Item.FindControl("lblSectionAT") as Label;
                        lblSectionAT.Text = dsGetSections.Tables[0].Rows[i]["SECTIONNAME"].ToString();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();});", true);
            }
        }
        else //Tutorial
        {
            //to get selected sections and batches 
            DataSet dsGetSections = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,B.BATCHNAME,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON(B.BATCHNO = T.BATCHNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=" + ddlBatchesAT.SelectedValue + " AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=1 AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue);
            for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
            {
                if (chkBoxAT.Enabled == false && dsGetSections.Tables[0].Rows[i]["Allot_AT"].ToString() == "1")
                {
                    Label lblSectionAT = e.Item.FindControl("lblSectionAT") as Label;
                    lblSectionAT.Text = dsGetSections.Tables[0].Rows[i]["SECTIONNAME"].ToString();

                    Label lblBatchAT = e.Item.FindControl("lblBatchAT") as Label;
                    lblBatchAT.Text = dsGetSections.Tables[0].Rows[i]["BATCHNAME"].ToString();
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('td:nth-child(4)').show();});", true);

        }
        //}
        //else
        //{
        //    //to get selected sections and batches 
        //    DataSet dsGetSections = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,B.BATCHNAME,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON(B.BATCHNO = T.BATCHNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=" + ddlBatchesAT.SelectedValue + " AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 ");
        //    for (int i = 0; i < dsGetSections.Tables[0].Rows.Count; i++)
        //    {
        //        if (chkBoxAT.Enabled == false && dsGetSections.Tables[0].Rows[i]["Allot_AT"].ToString() == "1")
        //        {
        //            Label lblSectionAT = e.Item.FindControl("lblSectionAT") as Label;
        //            lblSectionAT.Text = dsGetSections.Tables[0].Rows[i]["SECTIONNAME"].ToString();

        //            Label lblBatchAT = e.Item.FindControl("lblBatchAT") as Label;
        //            lblBatchAT.Text = dsGetSections.Tables[0].Rows[i]["BATCHNAME"].ToString();
        //        }
        //    }

        //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('td:nth-child(4)').show();});", true);

        //}



    }

    protected void btnFilterAT_Click(object sender, EventArgs e)
    {
        updPanel2.Update();
        this.BindListViewMainAT();
    }

    private void BindListViewMainAT()
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSessionAT.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemenoat"].ToString());
            int semesterno = Convert.ToInt32(ddlSemesterAT.SelectedValue);
            int courseno = Convert.ToInt32(ddlSubjectAT.SelectedValue);
            int deptno = Convert.ToInt32(ddlDeptAT.SelectedValue);
            int sectionno = Convert.ToInt32(ddlSectionAT.SelectedValue);
            int batchhno = 0;
            int ChkOtherDept = 0;
            int Is_Tutorial = Convert.ToInt32(ddlTutorialAT.SelectedValue);
            int subID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "SUBID", "COURSENO=" + ddlSubjectAT.SelectedValue));
            //if (subID == 1)//commented by dileep on 19022021 for Sessional COurse Type.
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            //if ((subID != 2 && subID != 11 && OrgId == 1) || ((subID != 2 && subID != 12 && subID != 4 && subID != 15 && OrgId == 2)) || (subID != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for Sessional COurse Type.
            //{
            DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlSubjectAT.SelectedValue, "");
            batchhno = 0;
            if (ddlTutorialAT.SelectedValue == "2") // Added by Dileep Kare on 19.01.2022 for Tutorial subject allotment.
            {
                batchhno = Convert.ToInt32(ddlBatchesAT.SelectedValue);
                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#SectionAT').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#SectionAT').show();$('td:nth-child(3)').show();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Tutorial Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Practical Batch');$('td:nth-child(4)').show();});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#SectionAT').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#SectionAT').show();$('td:nth-child(3)').show();});", true);
                }
            }
            if (ddlTutorialAT.SelectedValue == "3") // Added by Dileep Kare on 19.01.2022 for Tutorial subject allotment.
            {
                batchhno = Convert.ToInt32(ddlBatchesAT.SelectedValue);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#SectionAT').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#SectionAT').show();$('td:nth-child(3)').show();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Tutorial Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Tutorial Batch');$('td:nth-child(4)').show();});", true);
            }
            else //this is for Theory subject allotment .
            {
                batchhno = Convert.ToInt32(ddlBatchesAT.SelectedValue);
                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').hide();$('td:nth-child(4)').hide();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#SectionAT').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#SectionAT').show();$('td:nth-child(3)').show();});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#SectionAT').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#SectionAT').show();$('td:nth-child(3)').show();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Tutorial Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Practical Batch');$('td:nth-child(4)').show();});", true);
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.BTAT').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.BTAT').hide();$('td:nth-child(4)').hide();});", true);
            }
            //}
            //else //this is for Practival subject allotment.
            //{
            //    batchhno = Convert.ToInt32(ddlBatchesAT.SelectedValue);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Practical Batch');$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheoryAT').show();$('#BatchTheoryAT').text('Practical Batch');$('td:nth-child(4)').show();});", true);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#SectionAT').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#SectionAT').show();$('td:nth-child(3)').show();});", true);

            //    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.BTAT').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.BTAT').hide();$('td:nth-child(4)').hide();});", true);
            //}

            // Added Mahesh regarding Faculty assign for another Department purpose if not in Current Department
            if (chkDeptAT.Checked == true)
            {
                ChkOtherDept = 1;
                if (ddlOtherDepartmentAT.SelectedIndex > 0)
                {
                    deptno = Convert.ToInt32(ddlOtherDepartmentAT.SelectedValue);
                }
            }
            // ---------------------------------------------------------------------------------
            OrgId = Convert.ToInt32(Session["OrgId"]);
            DataSet ds = objAttendC.GetSubjectForCourseAllotmentAT(sessionno, schemeno, semesterno, courseno, deptno, sectionno, batchhno, Convert.ToInt32(ViewState["college_id"].ToString()), ChkOtherDept, Is_Tutorial, OrgId);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvCourseTeacherAT.DataSource = ds;
                lvCourseTeacherAT.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourseTeacherAT);//Set label 
                btnSaveAT.Visible = true;

                updPanel1.Update();
                lvCourseTeacher.DataSource = null;
                lvCourseTeacher.DataBind();
                btnSave.Visible = false;

                updCancelCT.Update();
                lvCourseTeacherCT.DataSource = null;
                lvCourseTeacherCT.DataBind();
                btnSubmitCT.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
                lvCourseTeacherAT.DataSource = null;
                lvCourseTeacherAT.DataBind();
            }

            foreach (ListViewDataItem dataitem in lvCourseTeacherAT.Items)
            {
                //  DropDownList ddlSectionAT = dataitem.FindControl("ddlSectionAT") as DropDownList;
                HiddenField hdnSectionAT = dataitem.FindControl("hdnSectionAT") as HiddenField;
                HiddenField hdnAllotAT = dataitem.FindControl("hdnAllotAT") as HiddenField;

                CheckBox chkAcceptAT = dataitem.FindControl("chkAcceptAT") as CheckBox;

                if (hdnAllotAT.Value == "1")
                    chkAcceptAT.BackColor = System.Drawing.Color.Red;

                //ddlSectionAT.SelectedValue = hdnSectionAT.Value;
                //if (ddlSectionAT.SelectedIndex == 0 && Convert.ToInt32(hdnAllotAT.Value) != 1) //&& ddlTeacher.SelectedIndex == 0
                //    ddlSectionAT.Enabled = false;

                //DropDownList ddlBatchAT = dataitem.FindControl("ddlBatchAT") as DropDownList;
                //HiddenField hdnBatchAT = dataitem.FindControl("hdnBatchAT") as HiddenField;
                //ddlBatchAT.SelectedValue = hdnBatchAT.Value;
                //if (ddlBatchAT.SelectedIndex == 0 && Convert.ToInt32(hdnAllotAT.Value) != 1) //&& ddlTeacher.SelectedIndex == 0
                //    ddlBatchAT.Enabled = false;

                //DropDownList ddlRoomAT = dataitem.FindControl("ddlRoomAT") as DropDownList;
                //HiddenField hdnRoomAT = dataitem.FindControl("hdnRoomAT") as HiddenField;
                //ddlRoomAT.SelectedValue = hdnRoomAT.Value;
                //if (ddlRoomAT.SelectedIndex == 0 && Convert.ToInt32(hdnAllotAT.Value) != 1) //&& ddlTeacher.SelectedIndex == 0
                //    ddlRoomAT.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSaveAT_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            foreach (ListViewDataItem lvItem in lvCourseTeacherAT.Items)
            {
                CheckBox chkBoxAT = lvItem.FindControl("chkAcceptAT") as CheckBox;
                //DropDownList ddlSectionAT = lvItem.FindControl("ddlSectionAT") as DropDownList;
                //DropDownList ddlBatchAT = lvItem.FindControl("ddlBatchAT") as DropDownList;
                //DropDownList ddlRoomAT = lvItem.FindControl("ddlRoomAT") as DropDownList;
                Label lblSectionAT = lvItem.FindControl("lblSectionAT") as Label;
                Label lblBatchAT = lvItem.FindControl("lblBatchAT") as Label;
                if (chkBoxAT.Checked == true)
                {
                    flag = 1;
                    if (ddlSectionAT.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(this, "Please Select Section for the selected Teacher !!", this.Page);
                        // ddlSectionAT.Enabled = true;
                        flag = 0;
                        return;
                    }
                }
            }
            if (flag == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Additional Teacher for the selected course !!", this.Page);
                return;
            }

            objAttendModel.Sessionno = Convert.ToInt32(ddlSessionAT.SelectedValue);
            objAttendModel.Schemeno = Convert.ToInt32(ViewState["schemenoat"].ToString());
            objAttendModel.Semesterno = Convert.ToInt32(ddlSemesterAT.SelectedValue);
            objAttendModel.CollegeCode = Convert.ToInt32(Session["colcode"]);
            objAttendModel.College_Id = Convert.ToInt32(ViewState["college_id"].ToString());
            foreach (ListViewDataItem lvItem in lvCourseTeacherAT.Items)
            {
                CheckBox chkBoxAT = lvItem.FindControl("chkAcceptAT") as CheckBox;
                HiddenField hdnTeacherAT = lvItem.FindControl("hdnTeacherAT") as HiddenField;
                HiddenField hdnSectionAT = lvItem.FindControl("hdnSectionAT") as HiddenField;
                HiddenField hdnBatchAT = lvItem.FindControl("hdnBatchAT") as HiddenField;
                //DropDownList ddlSectionAT = lvItem.FindControl("ddlSectionAT") as DropDownList;
                //DropDownList ddlBatchAT = lvItem.FindControl("ddlBatchAT") as DropDownList;
                //DropDownList ddlRoomAT = lvItem.FindControl("ddlRoomAT") as DropDownList;
                objAttendModel.CourseNos = Convert.ToInt32(chkBoxAT.ToolTip);
                if (chkBoxAT.Checked == true && chkBoxAT.Enabled == true)
                {
                    if (objAttendModel.TeacherNos.Length > 0)
                        objAttendModel.TeacherNos += ",";
                    objAttendModel.TeacherNos += hdnTeacherAT.Value;


                    // if (objAttendModel.SectionNos.Length > 0)
                    objAttendModel.SectionNos = ddlSectionAT.SelectedValue;
                    objAttendModel.BatchNos = ddlBatchesAT.SelectedValue;
                    int subID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "SUBID", "COURSENO=" + ddlSubjectAT.SelectedValue));
                    //if (subID == 1) //commented by dileep on 19022021 for sessional course type
                    int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
                    //if ((subID != 2 && subID != 11 && OrgId == 1) || ((subID != 2 && subID != 12 && subID != 4 && subID != 15 && OrgId == 2)) || (subID != 2 && OrgId != 1 && OrgId != 2))   //added by dileep on 19022021 for sessional course type
                    //{
                    //    if (ddlTutorialAT.SelectedValue == "1")
                    //    {
                    //        objAttendModel.BatchNos = "0";
                    //    }
                    //    else
                    //    {
                    //        objAttendModel.BatchNos = ddlBatchesAT.SelectedValue;
                    //    }
                    //}
                    //else
                    //{
                    //    // if (objAttendModel.BatchNos.Length > 0)
                    //    objAttendModel.BatchNos = ddlBatchesAT.SelectedValue;
                    //}
                }
            }
            CustomStatus cs = (CustomStatus)objAttendC.AddCourseTeacherAllotmentBulkAT(objAttendModel, Convert.ToInt32(ddlTutorialAT.SelectedValue), Convert.ToInt32(Session["OrgId"]));
            this.BindListViewMainAT();
            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this, "Additional Teacher alloted Successfully!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this, "Server Error", this.Page);

        }
        catch
        {
            throw;
        }
    }

    private void clearATListView()
    {
        updCancelCT.Update();
        lvCourseTeacherCT.DataSource = null;
        lvCourseTeacherCT.DataBind();
        btnSubmitCT.Visible = false;

        updPanel1.Update();
        lvCourseTeacher.DataSource = null;
        lvCourseTeacher.DataBind();
        btnSave.Visible = false;

        updPanel2.Update();
        lvCourseTeacherAT.DataSource = null;
        lvCourseTeacherAT.DataBind();
        btnSaveAT.Visible = false;
    }

    #endregion

    protected void cbHead_CheckedChanged(object sender, EventArgs e)
    {
        //string check=string.Empty;
        //CheckBox chkHead = (CheckBox)lvCourseTeacher.FindControl("cbHead");
        //foreach (ListViewDataItem item in lvCourseTeacher.Items)
        //{
        //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;

        //    if (chkHead.Checked == true)
        //        chkAccept.Checked = true;
        //    else
        //        chkAccept.Checked = false;
        //}
    }

    protected void chkbxlstADTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    int _count = 0;
        //    CheckBoxList chk = sender as CheckBoxList;

        //    ListViewItem item = (ListViewItem)chk.NamingContainer;
        //    CheckBox chk1 = (CheckBox)item.FindControl("chkAccept");
        //    // CheckBox chk2 = (CheckBox)item.FindControl("chkAddTeacher");
        //    HiddenField hdnSection = (HiddenField)item.FindControl("hdnSection");
        //    HiddenField hdnAddTeacher = (HiddenField)item.FindControl("hdnAddTeacher");
        //    string cName = string.Empty;
        //    cName = objCommon.LookUp("ACD_COURSE", "CCODE+' - ['+COURSE_NAME+']'", "COURSENO=" + chk1.ToolTip);

        //    if ((chk1.Checked))
        //    {
        //        _count = objCommon.LookUp("ACD_COURSE_TEACHER", "COUNT(1)", "COURSENO=" + Convert.ToInt32(chk1.ToolTip) + " AND SESSIONNO =" + ddlSessionBulk.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSemester.SelectedValue + " AND SECTIONNO =" + hdnSection.Value) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "COUNT(1)", "COURSENO=" + Convert.ToInt32(chk1.ToolTip) + " AND SESSIONNO =" + ddlSessionBulk.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSemester.SelectedValue + " AND SECTIONNO =" + hdnSection.Value));
        //        if (Convert.ToInt32(_count) > 1)
        //        {
        //            if (hdnAddTeacher.Value == "1")
        //            {
        //                objCommon.DisplayMessage(this, "Additional Teacher already assigned to " + cName + " course, You cant change the Additional flag!", this.Page);
        //                //chk2.Checked = true;
        //            }
        //            return;
        //        }
        //    }
        //}
        //catch { }

    }

    protected void chkAddTeacher_CheckedChanged(object sender, EventArgs e)
    {
        //int _count = 0;
        //CheckBox chk = sender as CheckBox;

        //ListViewItem item = (ListViewItem)chk.NamingContainer;
        //CheckBox chk1 = (CheckBox)item.FindControl("chkAccept");
        //CheckBox chk2 = (CheckBox)item.FindControl("chkAddTeacher");
        //HiddenField hdnSection = (HiddenField)item.FindControl("hdnSection");
        //HiddenField hdnAddTeacher = (HiddenField)item.FindControl("hdnAddTeacher");
        //string cName = string.Empty;
        //cName = objCommon.LookUp("ACD_COURSE", "CCODE+' - ['+COURSE_NAME+']'", "COURSENO=" + chk2.ToolTip);

        //if ((chk1.Checked) && (!chk2.Checked))
        //{
        //    _count = objCommon.LookUp("ACD_COURSE_TEACHER", "COUNT(1)", "COURSENO=" + Convert.ToInt32(chk2.ToolTip) + " AND SESSIONNO =" + ddlSessionBulk.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSemester.SelectedValue + " AND SECTIONNO =" + hdnSection.Value) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "COUNT(1)", "COURSENO=" + Convert.ToInt32(chk2.ToolTip) + " AND SESSIONNO =" + ddlSessionBulk.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSemester.SelectedValue + " AND SECTIONNO =" + hdnSection.Value));
        //    if (Convert.ToInt32(_count) > 1)
        //    {
        //        if (hdnAddTeacher.Value == "1")
        //        {
        //            objCommon.DisplayMessage(this, "Additional Teacher already assigned to " + cName + " course, You cant change the Additional flag!", this.Page);
        //            chk2.Checked = true;
        //        }
        //        return;
        //    }
        //}

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            updCancelCT.Update();
            lvCourseTeacherCT.DataSource = null;
            lvCourseTeacherCT.DataBind();
            updPanel1.Update();
            lvCourseTeacher.DataSource = null;
            lvCourseTeacher.DataBind();
            updPanel2.Update();
            lvCourseTeacherAT.DataSource = null;
            lvCourseTeacherAT.DataBind();

            this.ShowReport("BulkCourseAllotment", "rptCourseAllotmentBulk.rpt");
        }
        catch
        {
            throw;
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
            url += "&param=@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + ",@P_SESSIONNO=" + ddlSessionBulk.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"].ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //+",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updPanel1, this.updPanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }

    #region Cancel Course Teacher Allotment

    protected void ddlSchemeCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown(ddlDegreeCT);
            ClearDropDown(ddlBranchCT);
            ClearDropDown(ddlsemesterCT);
            ClearDropDown(ddlDepartmentCT);
            ClearDropDown(ddlSessionCT);
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchemeCT.SelectedValue));
            //ViewState["degreeno"]

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            this.clearATListView();
            //ddlsemesterCT.SelectedIndex = 0;
            ClearDropDown(ddlsemesterCT);
            btnSubmitCT.Visible = false;
            if (ddlSchemeCT.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSessionCT, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                ddlSessionCT.Focus();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSessionCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSchoolInstituteCT.SelectedIndex = 0;
        ddlDepartmentCT.SelectedIndex = 0;
        ClearDropDown(ddlDegreeCT);
        ClearDropDown(ddlBranchCT);
        ClearDropDown(ddlsemesterCT);
        ClearDropDown(ddlDepartmentCT);
        btnSubmitCT.Visible = false;

        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlDepartmentCT, "ACD_DEPARTMENT DP WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON DP.DEPTNO=CDB.DEPTNO", "DISTINCT DP.DEPTNO", "DEPTNAME", "DP.DEPTNO>0 and DP.DEPTNO IN(" + Session["userdeptno"].ToString() + ") AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "DEPTNAME ASC");
        }
        else
        {
            objCommon.FillDropDownList(ddlDepartmentCT, "ACD_DEPARTMENT DP WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON DP.DEPTNO=CDB.DEPTNO", "DISTINCT DP.DEPTNO", "DEPTNAME", "DP.DEPTNO>0 AND ISNULL(DP.ACTIVESTATUS,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "DEPTNAME ASC");
        }
        ddlDepartmentCT.Focus();
        this.clearATListView();
    }

    protected void ddlDepartmentCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearDropDown(ddlSchemeCT);
        //objCommon.FillDropDownList(ddlDegreeCT, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstituteCT.SelectedValue + " AND A.DEPTNO=" + ddlDepartmentCT.SelectedValue, "B.DEGREENAME ASC");
        ClearDropDown(ddlBranchCT);
        ClearDropDown(ddlsemesterCT);
        btnSubmitCT.Visible = false;
        this.clearATListView();
        string odd_even = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(ddlSessionCT.SelectedValue));
        string exam_type = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSessionCT.SelectedValue));

        string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON B.BRANCHNO=A.BRANCHNO", "CAST(DURATION AS INT)*2 AS DURATION", "B.BRANCHNO=" + ViewState["branchno"].ToString() + " AND A.DEGREENO=" + ViewState["degreeno"].ToString());

        if (exam_type == "1" && odd_even != "3")
        {
            //Commented by As per Requirement Of Romal Saluja Sir

            //objCommon.FillDropDownList(ddlsemesterCT, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "S.COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNAME asc");
            ddlsemesterCT.Items.Clear();
            ddlsemesterCT.Items.Add(new ListItem("Please Select", "0"));
            int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int SessionNo = Convert.ToInt32(ddlSessionCT.SelectedValue);
            DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlsemesterCT.DataSource = ds;
                ddlsemesterCT.DataTextField = "SEMESTERNAME";
                ddlsemesterCT.DataValueField = "SEMESTERNO";
                ddlsemesterCT.DataBind();
            }
        }
        else
        {
            objCommon.FillDropDownList(ddlsemesterCT, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)",
                "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ",
                "S.COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND SM.SEMESTERNO<=" + semCount +
                " AND ACTIVESTATUS=1 UNION SELECT DISTINCT SEMESTERNO,SEMESTERNAME FROM ACD_SEMESTER WHERE SEMESTERNAME like '%SUMMER%' AND ACTIVESTATUS=1", // added by Shailendra K. on dated 08.05.2024 as per Dr. Manoj sir Suggestion for summer semester.
                "");
        }
        ddlsemesterCT.Focus();
    }

    protected void ddlSchoolInstituteCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlDepartmentCT, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + ddlSchoolInstituteCT.SelectedValue + "", "A.DEPTNAME ASC");
            ClearDropDown(ddlDegreeCT);
            ClearDropDown(ddlBranchCT);
            ClearDropDown(ddlSchemeCT);
            ClearDropDown(ddlsemesterCT);
            btnSubmitCT.Visible = false;
            this.clearATListView();
        }
        else
        {
            ddlDepartmentCT.SelectedIndex = 0;
            ClearDropDown(ddlDegreeCT);
            ClearDropDown(ddlBranchCT);
            ClearDropDown(ddlSchemeCT);
            ClearDropDown(ddlsemesterCT);

            btnSubmitCT.Visible = false;
            this.clearATListView();
        }
    }



    protected void ddlDegreeCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegreeCT.SelectedIndex > 0)
        //{
        //this.clearATListView();
        objCommon.FillDropDownList(ddlBranchCT, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "A.COLLEGE_ID=" + ddlSchoolInstituteCT.SelectedValue + " AND A.DEPTNO=" + ddlDepartmentCT.SelectedValue + " AND DEGREENO = " + ddlDegreeCT.SelectedValue, "B.LONGNAME asc");
        ddlBranchCT.Focus();
        //}

        ClearDropDown(ddlSchemeCT);
        ClearDropDown(ddlsemesterCT);

        //ddlBranchCT.SelectedIndex = 0;
        //ddlSchemeCT.SelectedIndex = 0;
        //ddlsemesterCT.SelectedIndex = 0;
        btnSubmitCT.Visible = false;
        this.clearATListView();
    }

    protected void ddlBranchCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBranchCT.SelectedIndex > 0)
        //{
        objCommon.FillDropDownList(ddlSchemeCT, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegreeCT.SelectedValue + " AND DEPTNO=" + ddlDepartmentCT.SelectedValue + " AND BRANCHNO = " + ddlBranchCT.SelectedValue, "SCHEMENAME asc");
        ddlSchemeCT.Focus();
        //}

        //ddlsemesterCT.SelectedIndex = 0;
        ClearDropDown(ddlsemesterCT);
        btnSubmitCT.Visible = false;
        this.clearATListView();
    }



    private void ClearControlsCT()
    {
        ddlSessionCT.SelectedIndex = 0;
        ddlDegreeCT.SelectedIndex = 0;
        ddlBranchCT.SelectedIndex = 0;
        ddlSchemeCT.SelectedIndex = 0;
        ddlsemesterCT.SelectedIndex = 0;
        updCancelCT.Update();
        lvCourseTeacherCT.DataSource = null;
        lvCourseTeacherCT.DataBind();
        updPanel1.Update();
        lvCourseTeacher.DataSource = null;
        lvCourseTeacher.DataBind();
        updPanel2.Update();
        lvCourseTeacherAT.DataSource = null;
        lvCourseTeacherAT.DataBind();

        btnSubmitCT.Visible = false;

        ddlSessionCT.Items.Clear();
        ddlSessionCT.Items.Add(new ListItem("Please Select", "0"));

        ddlDepartmentCT.Items.Clear();
        ddlDepartmentCT.Items.Add(new ListItem("Please Select", "0"));

        ddlsemesterCT.Items.Clear();
        ddlsemesterCT.Items.Add(new ListItem("Please Select", "0"));

    }

    protected void btnShowCT_Click(object sender, EventArgs e)
    {
        this.BindAllottedTeacherList();
    }

    private void BindAllottedTeacherList()
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSessionCT.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int semesterno = Convert.ToInt32(ddlsemesterCT.SelectedValue);

            DataSet ds = objAttendC.GetCourseAllottedList(sessionno, schemeno, semesterno, Convert.ToInt32(ViewState["college_id"].ToString()), Convert.ToInt32(ddlDepartmentCT.SelectedValue), Convert.ToInt32(Session["OrgId"]));
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('td:nth-child(4)').show();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "$('#Section1').show();$('td:nth-child(3)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Section1').show();$('td:nth-child(3)').show();});", true);

                updCancelCT.Update();
                lvCourseTeacherCT.DataSource = ds;
                lvCourseTeacherCT.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourseTeacherCT);//Set label 
                btnSubmitCT.Visible = true;

                updPanel2.Update();
                lvCourseTeacherAT.DataSource = null;
                lvCourseTeacherAT.DataBind();
                btnSaveAT.Visible = false;
                updPanel1.Update();
                lvCourseTeacher.DataSource = null;
                lvCourseTeacher.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
                lvCourseTeacherCT.DataSource = null;
                lvCourseTeacherCT.DataBind();
            }

        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmitCT_Click(object sender, EventArgs e)
    {

        try
        {
            int flag = 0;
            string _ctNos = string.Empty;
            foreach (ListViewDataItem lvItem in lvCourseTeacherCT.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkCanAllotTea") as CheckBox;

                if (chkBox.Checked == true)
                {
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Teacher to cancel the allotment!", this.Page);
                return;
            }

            foreach (ListViewDataItem lvItem in lvCourseTeacherCT.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkCanAllotTea") as CheckBox;
                HiddenField hdnCTNO = lvItem.FindControl("hdnCTNO") as HiddenField;


                if (chkBox.Checked == true)
                {
                    if (_ctNos.Length > 0)
                        _ctNos += ",";

                    _ctNos += chkBox.ToolTip;
                }
            }

            if (_ctNos.Length <= 0)
            {
                objCommon.DisplayMessage(this, "Please select atleast one teacher to cancel the allotment!", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objAttendC.CancelTeacherAllotment(ddlSessionCT.SelectedValue, _ctNos);
            this.BindAllottedTeacherList();
            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this, "Subject teachers allotment successfully cancelled !!", this.Page);
            }
            else
                objCommon.DisplayMessage(this, "Server Error", this.Page);

        }
        catch
        {
            throw;
        }
    }

    protected void btnCancelCT_Click(object sender, EventArgs e)
    {
        this.ClearControlsCT();
    }

    #endregion Cancel Course Teacher Allotment

    protected void btnReportCT_Click(object sender, EventArgs e)
    {
        try
        {
            lvCourseTeacher.DataSource = null;
            lvCourseTeacher.DataBind();
            lvCourseTeacherAT.DataSource = null;
            lvCourseTeacherAT.DataBind();
            lvCourseTeacherCT.DataSource = null;
            lvCourseTeacherCT.DataBind();
            this.ShowReport1("Cancel Allotment Teacher", "rptCourseAllotmentCancelList.rpt");
        }
        catch
        {
            throw;
        }
    }

    private void ShowReport1(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_COLLEGE_ID=" + ddlSchoolInstituteCT.SelectedValue + ",@P_SESSIONNO=" + ddlSessionCT.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"].ToString() + ",@P_SEMESTERNO=" + ddlsemesterCT.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //+",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updCancelCT, this.updCancelCT.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }

    // method used to check attendance entry available or not in attendance table..
    protected void chkCanAllotTea_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int _count = 0;
            int TimeTableCount = 0;
            CheckBox chk = sender as CheckBox;

            ListViewItem item = (ListViewItem)chk.NamingContainer;
            CheckBox chk1 = (CheckBox)item.FindControl("chkCanAllotTea");
            HiddenField hdnCourseNo = (HiddenField)item.FindControl("hdnCourseNoCT");
            HiddenField hdnSection = (HiddenField)item.FindControl("hdnSectionCT");
            HiddenField hdnBatchNo = (HiddenField)item.FindControl("hdnBatchCT");
            HiddenField hdnUANO = (HiddenField)item.FindControl("hdnTeacherCT");

            string cName = string.Empty;
            cName = objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "CCODE+' - ['+COURSE_NAME+']'", "COURSENO=" + Convert.ToInt32(hdnCourseNo.Value));
            int CTNO = Convert.ToInt32(chk1.ToolTip);
            string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
            if (chk1.Checked)
            {
                DataSet ds = new DataSet();
                SP_Name = "PKG_ACD_GET_TIME_TABLE_ATTENDANCE_EXIST_COUNT";
                SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_SECTIONNO,@P_UA_NO,@P_BATCHNO,@P_CTNO";
                Call_Values = "" + ddlSessionCT.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + ddlsemesterCT.SelectedValue + "," + Convert.ToInt32(hdnCourseNo.Value) + "," + Convert.ToInt32(hdnSection.Value) + "," + Convert.ToInt32(hdnUANO.Value) + "," + Convert.ToInt32(hdnBatchNo.Value) + "," + CTNO + "";
                ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
                {
                    objCommon.DisplayMessage(this.updCancelCT, "Allotment cant be Cancelled, as this faculty has already taken attendance for subject - " + cName, this.Page);
                    chk1.Checked = false;
                    return;
                }

                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    objCommon.DisplayMessage(this.updCancelCT, "Allotment cant be Cancelled, TimeTable is already created for this Faculty.", this.Page);
                    chk1.Checked = false;
                    return;
                }
              
                
                //_count = objCommon.LookUp("ACD_ATTENDANCE WITH (NOLOCK)", "COUNT(1)", "SESSIONNO =" + ddlSessionCT.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO =" + ViewState["schemeno"].ToString() + " AND SEMESTERNO =" + ddlsemesterCT.SelectedValue + " AND COURSENO=" + Convert.ToInt32(hdnCourseNo.Value) + " AND SECTIONNO=" + Convert.ToInt32(hdnSection.Value) + " AND UA_NO=" + Convert.ToInt32(hdnUANO.Value) + " AND (BATCHNO=" + Convert.ToInt32(hdnBatchNo.Value) + " OR " + Convert.ToInt32(hdnBatchNo.Value) + "=0)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(1)", "SESSIONNO =" + ddlSessionCT.SelectedValue + " AND SCHEMENO =" + ViewState["schemeno"].ToString() + " AND SEMESTERNO =" + ddlsemesterCT.SelectedValue + " AND COURSENO=" + Convert.ToInt32(hdnCourseNo.Value) + " AND SECTIONNO=" + Convert.ToInt32(hdnSection.Value) + " AND UA_NO=" + Convert.ToInt32(hdnUANO.Value) + " AND (BATCHNO=" + Convert.ToInt32(hdnBatchNo.Value) + " OR " + Convert.ToInt32(hdnBatchNo.Value) + "=0) AND ISNULL(CANCEL,0)=0"));
                //if (Convert.ToInt32(_count) > 0)
                //{
                //    objCommon.DisplayMessage(this.updCancelCT, "Allotment cant be Cancelled, as this faculty has already taken attendance for subject - " + cName, this.Page);
                //    chk1.Checked = false;
                //    return;
                //}

                //TimeTableCount = Convert.ToInt32(objCommon.LookUp("ACD_TIME_TABLE_CONFIG WITH (NOLOCK)", "COUNT(1)", "ISNULL(CANCEL,0)=0 AND CTNO=" + CTNO));
                //if (Convert.ToInt32(TimeTableCount) > 0)
                //{
                //    objCommon.DisplayMessage(this.updCancelCT, "Allotment cant be Cancelled, TimeTable is already created for this Faculty.", this.Page);
                //    chk1.Checked = false;
                //    return;
                //}
            }
        }
        catch { }

    }



    //protected void btnReportExcelCT_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        GridView GV = new GridView();
    //        string ContentType = string.Empty;

    //        DataSet ds = objAttendC.GetCourseTeacherExcelReport(Convert.ToInt32(ddlSessionBulk.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            GV.DataSource = ds;
    //            GV.DataBind();
    //            string attachment = "attachment; filename=CourseTeacherAllotmentExcel.xls";
    //            //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
    //            Response.ClearContent();
    //            Response.AddHeader("content-disposition", attachment);
    //            Response.ContentType = "application/vnd.MS-excel";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            GV.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();
    //        }
    //        else
    //        {
    //            objCommon.DisplayUserMessage(updPanel1, "No Record Found.", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    protected void lnkbatch_Click(object sender, EventArgs e)
    {
        LinkButton lnkbatch = sender as LinkButton;
        ListViewDataItem dataitem = lnkbatch.NamingContainer as ListViewDataItem;
        ListBox lstbxBatch = dataitem.FindControl("lstbxBatch") as ListBox;
        ListBox lstbxSection = dataitem.FindControl("lstbxSections") as ListBox;
        ListBox lst_AD_Teacher = dataitem.FindControl("lstbxAdTeacher") as ListBox;
        HiddenField _subid = dataitem.FindControl("hdnSubid") as HiddenField;

        #region comment
        //int lstseccount = 0;
        //for (int l = 0; l < lstbxSections.Items.Count; l++)
        //{
        //    if (lstbxSections.Items[0].Selected == true)
        //    {
        //        lstseccount++;
        //    }
        //}

        //if (lstseccount == 0)
        //{
        //    objCommon.DisplayMessage(this.Page, "Please select section.", this.Page);
        //    return;
        //}
        //lstbxBatch.Attributes.Remove("disabled");

        //  foreach (ListViewDataItem dataitem in lvCourseTeacher.Items)
        // {
        //  ListBox lst_Section = dataitem.FindControl("lstbxSections") as ListBox;

        //     //check subid first
        //     if (Convert.ToInt32(_subid.Value) == 2)
        //     {
        //         int k = 0;
        //         while (k < lst_AD_Teacher.Items.Count)
        //         {
        //           //  lst_AD_Teacher.Items[k].Enabled = false;
        //             k++;
        //         }
        //         k = 0;
        //     }


        //     int j = 0;

        //     for (int i = 0; i < lstbxBatch.Items.Count; i++)
        //     {
        //         if (lstbxBatch.Items[i].Selected == true)
        //         {

        //             while (j < lst_AD_Teacher.Items.Count)
        //             {
        //                 lst_AD_Teacher.Items[j].Enabled = false;
        //                 j++;
        //             }
        //             lst_AD_Teacher.Items[i].Enabled = true;
        //         }

        //     }

        //// }
        // //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.abc').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.abc').show();$('td:nth-child(4)').show();});", true);
        // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('td:nth-child(4)').show();});", true);
        // //ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "eye(" + lnkbatch.ClientID + ");", true);
        //// Button2.Attributes.Add("onclick", "javascript:return testButtonClick('Title 2','Clicaste botao 2','OK button 2','Cancel 2','" + this.lnkbatch.ClientID + "');");
        // //foreach (ListViewDataItem dataitem in lvCourseTeacher.Items)
        // //{
        #endregion
        int subid = Convert.ToInt32(_subid.Value);
        int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
        ListBox lst_Section = dataitem.FindControl("lstbxSections") as ListBox;
        lstbxBatch.Attributes.Remove("disabled");
        //check subid first
        //if (Convert.ToInt32(_subid.Value) == 2)

        if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))
        {

            int k = 0;
            while (k < lstbxBatch.Items.Count)
            {
                // lstbxBatch.Items[k].Enabled = false;
                //lst_AD_Teacher.Items[k].Enabled = false;
                k++;
            }
            k = 0;
        }
        else
        {

            int k = 0;
            while (k < lst_AD_Teacher.Items.Count)
            {
                lst_AD_Teacher.Items[k].Enabled = false;
                k++;
            }
            k = 0;
        }



        int j = 0, batch = 0;

        for (int i = 0; i < lst_Section.Items.Count; i++)
        {
            if (lst_Section.Items[i].Selected == true)
            {

                //check subid first
                if ((subid != 2 && subid != 11 && OrgId == 1) || ((subid != 2 && subid != 12 && subid != 4 && subid != 15 && OrgId == 2)) || (subid != 2 && OrgId != 1 && OrgId != 2))
                {

                    while (j < lst_AD_Teacher.Items.Count)
                    {
                        lst_AD_Teacher.Items[j].Enabled = false;
                        j++;
                    }
                    lst_AD_Teacher.Items[i].Enabled = true;

                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.abc').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.abc').hide();$('td:nth-child(4)').hide();});", true);

                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(4)').hide();});", true);
                }
                else
                {

                    while (batch < lstbxBatch.Items.Count)
                    {
                        if (lst_Section.Items[i].Value == lstbxBatch.Items[batch].Value.ToString().Split('-')[0])
                        {
                            lstbxBatch.Items[batch].Enabled = true;
                            lst_AD_Teacher.Items[batch].Enabled = true;
                        }
                        batch++;
                    }
                    batch = 0;
                    // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.abc').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.abc').show();$('td:nth-child(4)').show();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').show();$('td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').show();$('td:nth-child(4)').show();});", true);

                }
            }

        }

        // }

    }
    protected void ddlTutorialAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.clearATListView();
        if (ddlSubjectAT.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSectionAT, "ACD_SECTION  S WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON(T.SECTIONNO=S.SECTIONNO)  ", "DISTINCT T.SECTIONNO", "S.SECTIONNAME", "T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  = " + ddlSemesterAT.SelectedValue + " and T.COURSENO=" + ddlSubjectAT.SelectedValue + " AND ISNULL(T.IS_ADTEACHER,0)=1 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorialAT.SelectedValue + "=3 THEN 1 ELSE 0 END) AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorialAT.SelectedValue, "S.SECTIONNAME asc");
            ddlBranch.Focus();
        }
        if (ddlTutorialAT.SelectedValue == "2" || ddlTutorialAT.SelectedValue == "3")
        {
            BatchAT.Visible = true;
            ddlBatchesAT.SelectedIndex = -1;
        }
        else
        {
            BatchAT.Visible = false;
            ddlBatchesAT.SelectedIndex = -1;
        }
    }

}