using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_CourseCodeWise_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
                PopulateDropDown();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            divMsg.InnerHtml = string.Empty;
        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Fill Dropdown Degree
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
            //Fill Dropdown Session
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            // Fill Semester Dropdown
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", " ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //Fill Dropdown semester
            try
            {
                if (ddlScheme.SelectedIndex > 0)
                {
                    //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                    objCommon.FillDropDownList(ddlSec, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION S ON (SR.SECTIONNO = S.SECTIONNO)", "DISTINCT SR.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND S.SECTIONNO>0 ", "SR.SECTIONNO");//AND SR.PREV_STATUS = 0

                    ddlSemester.Focus();
                }
                else
                {
                    ddlSemester.Items.Clear();
                    ddlScheme.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + ddlSec.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportElecCourseWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            char ch = '-';
            string[] splitccode = new string[2];
            splitccode = ddlCourse.SelectedItem.Text.Split(ch);
            string ccode = splitccode[0].Trim();

            //DataSet ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "SR.CCODE", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "CCODE");
            //ddlCourse.SelectedItem.Text = ds.Tables[0].ToString();
            //string CCODE = ddlCourse.SelectedItem.Text;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CCODE=" + ccode + ",username=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowStudentListReport(string reportTitle, string rptFileName)
    {
        try
        {
            //Check record found or not
            string count = string.Empty;
            if (rdbReport.SelectedValue == "3")
                count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            else
                count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND S.PRO=1");

            if (count == "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record Not Found!!", this.Page);
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSec.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";
                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetCoursewiseStudentsCount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));//,Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlSchemeType.SelectedValue),Convert.ToInt32(ddlSection.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
            }
            else
            {
                lblStatus.Text = "No Students for selected criteria";
                lvStudents.DataSource = null;
                lvStudents.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND (C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + " OR " + ddlCollegeName.SelectedValue + "= 0) AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "AND B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND B.COLLEGE_ID > 0", "A.DEGREENO");

        // objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEPTNAME");
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Dropdown Scheme
        if (ddlBranch.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlBranch.SelectedIndex = 0;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fill branch according degree selection
        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSemester, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID=" + ddlCollegeName.SelectedValue, "A.LONGNAME");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "DISTINCT CCODE", "COURSE_NAME", "OFFERED=1", "CCODE");
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "Case When ISNULL(GLOBALELE,0)=1 then (SR.CCODE + ' - ' + SR.COURSENAME +' [Global]') Else (SR.CCODE + ' - ' + SR.COURSENAME) End as COURSENAME", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "");
            objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT A INNER JOIN ACD_COURSE B ON A.COURSENO = B.COURSENO  ", "DISTINCT A.CCODE", " (B.CCODE + ' - ' + B.COURSE_NAME) COURSE", " OFFERED=1 AND A.SEMESTERNO = " + ddlSemester.SelectedValue + " AND A.SESSIONNO = " + ddlSession.SelectedValue, "A.CCODE");
        
        }
        else
        {
            ddlSubjectType.Items.Clear();
            ddlSemester.SelectedIndex = 0;
        }


        ddlSection.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;

    }

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue, "CCODE");
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "Case When ISNULL(GLOBALELE,0)=1 then (SR.CCODE + ' - ' + SR.COURSENAME +' [Global]') Else (SR.CCODE + ' - ' + SR.COURSENAME) End as COURSENAME", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "");

            ddlCourse.Focus();
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlScheme.SelectedIndex = 0;
        }

        ddlSection.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblStatus.Text = string.Empty;
        ////Fill Dropdown section
        //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR,ACD_SECTION SEC", "DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SEC.SECTIONNO=SR.SECTIONNO AND  SEC.SECTIONNO > 0 AND  SR.SCHEMENO=" + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SEC.SECTIONNAME");
    }

    private void ShowCourseStudentCountReport(string reportTitle, string rptFileName, int type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + ddlSec.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_TYPE=" + type + ",UserName=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowCourseRegCountReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + ddlSec.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",UserName=" + Session["username"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdbReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Course Registered Student
        if (rdbReport.SelectedValue == "1")
        {
            //trreport.Visible = false;
            //rfvCourse.Enabled = true;
            spbr.Visible = true;
            spsc.Visible = true;
            spse.Visible = true;
            spsub.Visible = true;
            //rfvBranch.Enabled = true;
            //rfvSubjectType.Enabled = true;
            //rfvSemester.Enabled = true;
            //rfvProgram.Enabled = true;
            rfvCourse.Enabled = false;
            spcourse.Visible = false;
        }
        else if (rdbReport.SelectedValue == "2")
        {
            // trreport.Visible = true;
            //rfvCourse.Enabled = false;
            spbr.Visible = false;
            spsc.Visible = false;
            spse.Visible = false;
            spsub.Visible = false;
            //rfvBranch.Enabled = false;
            //rfvSubjectType.Enabled = false;
            //rfvSemester.Enabled = false;
            //rfvProgram.Enabled = false;
            rfvCourse.Enabled = false;
            spcourse.Visible = false;
        }
        else if (rdbReport.SelectedValue == "5")
        {
            // trreport.Visible = true;
            //rfvCourse.Enabled = false;
            //rfvcollege.Enabled = false;
            rfvSemester.Enabled = false;
            //rfvBranch.Enabled = false;
            //rfvProgram.Enabled = false;
            //rfvSemester.Enabled = false;
            //rfvSubjectType.Enabled = false;
            rfvCourse.Enabled = false;
            spcourse.Visible = false;
        }
        else if (rdbReport.SelectedValue == "3")
        {
            spbr.Visible = true;
            spsc.Visible = true;
            spse.Visible = true;
            spsub.Visible = true;
            //rfvBranch.Enabled = true;
            //rfvSubjectType.Enabled = true;
            rfvSemester.Enabled = true;
            //rfvProgram.Enabled = true;
            //rfvSubjectType.Enabled = false;
            rfvCourse.Enabled = false;
            spcourse.Visible = false;
        }
        else if (rdbReport.SelectedValue == "4")
        {
            spbr.Visible = true;
            spsc.Visible = true;
            spse.Visible = true;
            spsub.Visible = true;
            //rfvBranch.Enabled = true;
            //rfvSubjectType.Enabled = true;
            rfvSemester.Enabled = true;
            //rfvProgram.Enabled = true;
            // rfvSubjectType.Enabled = false;
            // rfvSubjectType.Enabled = false;
            rfvCourse.Enabled = false;
            spcourse.Visible = false;
        }
        else if (rdbReport.SelectedValue == "6")
        {
            spbr.Visible = false;
            spsc.Visible = false;
            spse.Visible = false;
            spsub.Visible = false;
            //rfvBranch.Enabled = false;
            //rfvSubjectType.Enabled = false;
            rfvSemester.Enabled = false;
            //rfvProgram.Enabled = false;
            //rfvSubjectType.Enabled = false;
            //rfvSubjectType.Enabled = false;
            rfvCourse.Enabled = false;
            spcourse.Visible = false;
        }

        else if (rdbReport.SelectedValue == "7")
        {
            //trreport.Visible = false;

            spbr.Visible = true;
            spsc.Visible = true;
            spse.Visible = true;
            spsub.Visible = true;
            spcourse.Visible = true;
            //rfvBranch.Enabled = true;
            //rfvSubjectType.Enabled = true;
            rfvSemester.Enabled = true;
            //rfvProgram.Enabled = true;
            rfvCourse.Enabled = true;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReportElecCourseWise("CourseCodeWise_Registered_Count", "rptCourseCodeWiseStudRegistration.rpt");
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowCourseRegistration(string reportTitle, string rptFileName)
    {
        try
        {
            string count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            if (count == "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record Not Found!!", this.Page);
            }
            else
            {
                //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSec.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";
                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        
        {
            ExportinExcelforFeesCollegeWiseCashList();
        }
    }




    private void ExportinExcelforFeesCollegeWiseCashList()
    {
        char ch = '-';
        string[] splitccode = new string[2];
        splitccode = ddlCourse.SelectedItem.Text.Split(ch);
        string ccode = splitccode[0].Trim();
        string attachment = "attachment; filename=" + "CourseRegisteredList-" + ccode+".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        
        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        DataSet dsfee = objCourse.Get_COURSE_REGISTRATION_DATA_COURSE_CODE_WISE(sessionNo, semesterno, ccode);

        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {
            dsfee.Tables[0].Columns.Remove("SECTIONNO");
            dsfee.Tables[0].Columns.Remove("BRANCHNO");
            dsfee.Tables[0].Columns.Remove("TERM");
            dsfee.Tables[0].Columns.Remove("ENROLLNO");
            dsfee.Tables[0].Columns.Remove("CCODE");
            dsfee.Tables[0].Columns.Remove("SCHEME");
            dsfee.Tables[0].Columns.Remove("ROLL_NO");
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }
    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSemester.SelectedItem.Text + "_" + ddlCourse.SelectedValue + "_" + ddlSection.SelectedItem.Text + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            char ch = '-';
            string[] splitccode = new string[2];
            splitccode = ddlCourse.SelectedItem.Text.Split(ch);
            string ccode = splitccode[0].Trim();
            //if (rdbReport.SelectedValue == "1")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",Username=" + Session["username"].ToString();
            //else if (rdbReport.SelectedValue == "2")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_TYPE= 1,userName=" + Session["username"].ToString();
            //else if (rdbReport.SelectedValue == "3")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
            //else if (rdbReport.SelectedValue == "4")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
            //else if (rdbReport.SelectedValue == "5")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_TYPE=2";
            ////else if (rdbReport.SelectedValue == "6")
            ////    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue;
            //else if (rdbReport.SelectedValue == "6")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",UserName=" + Session["username"].ToString();


            //else if (rdbReport.SelectedValue == "7")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_CCODE=" + ccode + ",UserName=" + Session["username"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}

