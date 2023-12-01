//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STUDENT COURSE REGISTRATION REPORT                                   
// CREATION DATE : 22-AUG-2011                                                       
// CREATED BY    :                                                    
// MODIFIED DATE : 20-AUG-2012 
// MODIFIED BY   : Pawan Mourya                                                                     
// MODIFIED DESC :                                                                      
//======================================================================================


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
using System.IO;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;





using System.Web.Configuration;
using System.Data.SqlClient;

using SendGrid.SmtpApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;

using System.Net.NetworkInformation;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using SendGrid;
using mastersofterp_MAKAUAT;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogic;



public partial class Exam_Registration_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    StudentController objSC = new StudentController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
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
                //CheckPageAuthorization();
                PopulateDropDown();

                //ddlCollege.SelectedIndex = 0;
                ////Set the Page Title
                //Page.Title = Session["coll_name"].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

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
                Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Fill Dropdown College
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            ////objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //Fill Dropdown Degree
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "DEGREENAME");
            //Fill Dropdown Session
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            // Fill Semester Dropdown
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
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
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
        if (ddlScheme.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlScheme.SelectedIndex = 0;
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
            int examtype = 0;
            string orderBy = string.Empty;
            //if (ddlExamType.SelectedValue == "-1")
            //   examtype = 0 ;
            //else
            examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            if (ddlOrderBy.SelectedValue == "0")
            {
                orderBy = "ROLL_NO";
            }
            else
            {
                orderBy = "REGNO";
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_ORDER_BY=" + orderBy + ",@P_EXAMTYPE=" + examtype;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

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

    private void ShowReportStudAttendance(string reportTitle, string rptFileName)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);

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

    private void ShowExamNotRegistration(string reportTitle, string rptFileName)
    {
        try
        {
            //string count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            //if (count == "0")
            //{
            //    objCommon.DisplayMessage("Record Not Found!!", this.Page);
            //}
            //else
            //{
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            int examtype = 0;
            //if (ddlExamType.SelectedValue == "-1")
            //   examtype = 0 ;
            //else
            examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_EXAMTYPE=" + examtype + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
            //}
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
                count = objCommon.LookUp("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            else
                count = objCommon.LookUp("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND S.PRO=1");

            if (count == "0")
            {
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue;
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
        //Fill Dropdown Scheme
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");
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
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
        // fill branch according degree selection
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

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
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
        //Fill Dropdown Course
        ///objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SEMESTER S ON (C.SEMESTERNO = S.SEMESTERNO)", "DISTINCT(C.COURSENO)","C.COURSE_NAME", "C.COURSENO > 0 AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "COURSENO");
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
        ddlCourse.Focus();

    }

    private void ShowExamStudentCountReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_EXAMTYPE=" + ddlExamType.SelectedValue;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
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
        Panel1.Visible = false;
        btnSendSMS.Visible = false;

        //Course Registered Student

        if (rdbReport.SelectedValue == "1" || rdbReport.SelectedValue == "15")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = true;
            //  rfvCollege.Enabled = true;

            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;

            //rfvSession.Enabled = true;
            //rfvScheme.Enabled = true;
            //Clear();
        }
        else if (rdbReport.SelectedValue == "4")
        {
            //rfvSemester.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvCourse.Enabled = true;
            //rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = true;
            //rfvCollege.Enabled = true;
            //rfvDegree.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvScheme.Enabled = true;
            //  Clear();
        }
        //if (rdbReport.SelectedValue == "5")
        //{
        //  //  rfvSemester.Enabled = true;
        //    spDegree.Visible = true;
        //    spBranch.Visible = true;
        //    spScheme.Visible = true;
        //    spSemester.Visible = true;
        //    spCourse.Visible = true;
        //    spStudenttype.Visible = true;
        //    spInstitue.Visible = true;
        //    btnReport.Enabled = true;
        //    rfvCollege.Enabled = true;
        //    //rfvCourse.Enabled = true;
        //    //rfvDegree.Enabled = true;
        //    //rfvBranch.Enabled = true;
        //    //rfvSession.Enabled = true;
        //    //rfvSemester.Enabled = true;
        //    ////rfvExamType.Enabled = true;
        //    //rfvScheme.Enabled = true;
        //    Clear();
        //}
        if (rdbReport.SelectedValue == "6")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = false;
            // rfvCollege.Enabled = true;
            ////rfvCourse.Enabled = true;
            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvScheme.Enabled = true;
            // Clear(); 
        }
        else if (rdbReport.SelectedValue == "7")
        {
            //spDegree.Visible = false;
            //spBranch.Visible = false;
            //spScheme.Visible = false;
            //spSemester.Visible = false;
            //spCourse.Visible = false;
            //spStudenttype.Visible = false;
            //spInstitue.Visible = false;
            btnReport.Enabled = false;
            BtnShow.Visible = false;
            //rfvCollege.Enabled = false;
            //rfvDegree.Enabled = false;
            //rfvBranch.Enabled = false;
            //rfvSession.Enabled = false;
            //rfvSemester.Enabled = false;
            ////rfvExamType.Enabled = false;
            //rfvScheme.Enabled = false;
        }
        else if (rdbReport.SelectedValue == "8")
        {
            //spDegree.Visible = false;
            //spBranch.Visible = false;
            //spScheme.Visible = false;
            //spSemester.Visible = false;
            //spCourse.Visible = false;
            //spStudenttype.Visible = false;
            //spInstitue.Visible = false;
            btnReport.Enabled = false;
            BtnShow.Visible = false;
            //rfvCollege.Enabled = false;
            //rfvDegree.Enabled = false;
            //rfvBranch.Enabled = false;
            //rfvSession.Enabled = false;
            //rfvSemester.Enabled = false;
            ////rfvExamType.Enabled = false;
            //rfvScheme.Enabled = false;
            // Clear();
        }
        else if (rdbReport.SelectedValue == "3")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = true;
            // rfvCollege.Enabled = true;
            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvScheme.Enabled = true;
            // Clear();
        }
        else if (rdbReport.SelectedValue == "9")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = true;
            // rfvCollege.Enabled = true;
            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvScheme.Enabled = true;
            // Clear();
        }
        else if (rdbReport.SelectedValue == "10")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = true;
            // rfvCollege.Enabled = true;
            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvScheme.Enabled = true;
            // Clear();
        }
        else if (rdbReport.SelectedValue == "11")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = true;
            // rfvCollege.Enabled = true;
            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvScheme.Enabled = true;
            // Clear();
        }
        else if (rdbReport.SelectedValue == "12")
        {
            // rfvSemester.Enabled = true;
            //spDegree.Visible = true;
            //spBranch.Visible = true;
            //spScheme.Visible = true;
            //spSemester.Visible = true;
            //spCourse.Visible = true;
            //spStudenttype.Visible = true;
            //spInstitue.Visible = true;
            btnReport.Enabled = true;
            BtnShow.Visible = false;
            // rfvCollege.Enabled = true;
            //rfvDegree.Enabled = true;
            //rfvBranch.Enabled = true;
            //rfvSession.Enabled = true;
            //rfvSemester.Enabled = true;
            ////rfvExamType.Enabled = true;
            //rfvScheme.Enabled = true;
            // Clear();
        }
        else
        {
            if (rdbReport.SelectedValue == "2")
            {
                //  rfvSemester.Enabled = true;
                //spDegree.Visible = true;
                //spBranch.Visible = true;
                //spScheme.Visible = true;
                //spSemester.Visible = true;
                //spCourse.Visible = true;
                //spStudenttype.Visible = true;
                //spInstitue.Visible = true;
                btnReport.Enabled = true;
                BtnShow.Visible = true;
                // rfvCollege.Enabled = true;
                //rfvDegree.Enabled = true;
                //rfvBranch.Enabled = true;
                //rfvSession.Enabled = true;
                //rfvSemester.Enabled = true;
                //rfvExamType.Enabled = true;
                //rfvScheme.Enabled = true;
                // Clear();
            }
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divSendOptions.Visible = false;
        rbNet.SelectedIndex = -1;
    }

    public void Clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlExamType.SelectedIndex = -1;
        ddlOrderBy.SelectedIndex = 0;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //Course Registered Student

            if (rdbReport.SelectedValue == "1")
            {
                // ShowReport("Exam_Wise_Registration_Report", "rptExamWiseStudRegistration.rpt");
                ShowExamNotRegistration("Student_Exam_Registration_Report", "rptStudentExamRegisteredList.rpt");
            }
            else if (rdbReport.SelectedValue == "2")
            {
                ShowExamNotRegistration("Exam_NotRegistration_List", "ExamNotRegistrationList.rpt");
            }
            else if (rdbReport.SelectedValue == "3")
            {
                ShowExamNotRegistration("Confirmed_Exam_Registration_List", "rptConfirmedExamRegistrationList.rpt");
            }
            else if (rdbReport.SelectedValue == "4")
            {
                ShowExamNotRegistration("Not_Confirmed_Exam_Registration_List", "rptNotConfirmedExamRegistrationList.rpt");
            }

            //else if (rdbReport.SelectedValue == "5")
            //{
            //    ShowReportStudAttendance("Student_Attendance_List", "rptExamStudAttendanceSheet.rpt");
            //}

            else if (rdbReport.SelectedValue == "6")
            {
                ShowExamStudentCountReport("ExamwiseStudentCount", "rptExamwiseStudentCount.rpt");
            }
            else if (rdbReport.SelectedValue == "9")
            {
                ShowExamStudentCountReport("Studentwhoyettodownloadadmitcard", "rptStudentnotDownloadAdmitCard.rpt");
            }
            else if (rdbReport.SelectedValue == "10")
            {
                ShowExamStudentCountReport("studentwhodownloadadmitcard", "rptStudentDownloadAdmitCard.rpt");
            }
            else if (rdbReport.SelectedValue == "11")
            {
                ShowExamStudentCountReport("studentwhoareDetained", "rptDetainedStudent.rpt");
            }
            else if (rdbReport.SelectedValue == "12")
            {
                ShowExamStudentCountReport("ExamwiseStudentStatus", "rptExamwiseStudentStatus.rpt");
            }
            else if (rdbReport.SelectedValue == "13")
            {
                ShowExamNotRegistration("ApprovedStudentStatus", "rptApprovedExamRegistrationList.rpt");
            }
            else if (rdbReport.SelectedValue == "14")
            {
                ShowExamNotRegistration("NotApprovedStudentStatus", "rptNotApprovedExamRegistrationList.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowExamRegistration(string reportTitle, string rptFileName)
    {
        try
        {
            //string count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            //if (count == "0")
            //{
            //    objCommon.DisplayMessage("Record Not Found!!", this.Page);
            //}
            //else
            //{
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            int examtype = 0;
            //if (ddlExamType.SelectedValue == "-1")
            //   examtype = 0 ;
            //else
            examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_EXAMTYPE=" + examtype;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
            //}
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
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        int examtype = Convert.ToInt32(ddlExamType.SelectedValue);
        int college_id = Convert.ToInt32(ddlCollege.SelectedValue);

        //Done
        if (rdbReport.SelectedValue == "1")
        {
            //ShowReportinFormate("xls", "rptExamWiseStudRegistration.rpt");
            //ShowReportinFormate("xls", "rptStudentExamRegisteredList.rpt");

            string filename = "Student_Filled_exam_form_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentFilledExamForm(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }

        }
        //Done
        else if (rdbReport.SelectedValue == "2" || rdbReport.SelectedValue == "15")
        {
            //ShowReportinFormate("xls", "ExamNotRegistrationList.rpt");
            string filename = "Student_Not_Filled_exam_form_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentNotFilledExamForm(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        //Done
        else if (rdbReport.SelectedValue == "3")
        {
            //  ShowReportinFormate("xls", "ExamRegistrationList.rpt");
            //ShowReportinFormate("xls", "rptConfirmedExamRegistrationList.rpt");
            //ShowReportinFormate("xls", "ExamNotRegistrationList.rpt");
            string filename = "Student_Payment_is_confirmed_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentPaymentConfirm(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        //Done
        else if (rdbReport.SelectedValue == "4")
        {
            //ShowReportinFormate("xls", "rptNotConfirmedExamRegistrationList.rpt");
            string filename = "Student_Payment_is_Not_confirmed_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentPaymentNotConfirm(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        else if (rdbReport.SelectedValue == "5")
        {
            ShowReportinFormate("xls", "rptExamStudAttendanceSheet.rpt");
        }
        //Done
        else if (rdbReport.SelectedValue == "6")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }
            if (ddlCollege.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select School/Institute Name.", this.Page);
                ddlSession.Focus();
                return;
            }
            //ShowReportinFormate("xls", "rptExamwiseStudentCount.rpt");
            string filename = "Exam_form_fill_and_Exam_Registration_Summary_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentExamRegistrationSummary(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        //Done
        else if (rdbReport.SelectedValue == "7")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }

            DataSet ds = objCC.GetSessionwiseExamRegistrationCount(Convert.ToInt32(ddlSession.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "SessionwiseExamRegistrationCount");

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        //Done
        else if (rdbReport.SelectedValue == "8")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }

            DataSet ds = objCC.GetSessionwiseExamRegistrationDetails(Convert.ToInt32(ddlSession.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "SessionwiseRegistrationDetails");

            }
        }
        //Done
        else if (rdbReport.SelectedValue == "9")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }


            DataSet ds = objCC.GetExamDownloadAdmitCardDetails(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "SessionwiseRegistrationDetails");

            }
        }
        //Done
        else if (rdbReport.SelectedValue == "10")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }

            DataSet ds = objCC.GetExamNotDownloadAdmitCardDetails(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "SessionwiseRegistrationDetails");

            }
        }
        //Done
        else if (rdbReport.SelectedValue == "12")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }
            if (ddlCollege.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select School/Institute Name.", this.Page);
                ddlSession.Focus();
                return;
            }
            //ShowReportinFormate("xls", "rptExamwiseStudentCount.rpt");
            string filename = "Exam_form_fill_and_Exam_Registration_Status_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentExamRegistrationStatus(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
        }
        //Done
        else if (rdbReport.SelectedValue == "13")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }

            string filename = "Exam_form_Approved_by_department_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentExamFormApprovedByDepartment(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
        }
        //Done
        else if (rdbReport.SelectedValue == "14")
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session.", this.Page);
                ddlSession.Focus();
                return;
            }

            string filename = "Exam_form_Not_Approved_by_department_" + ddlSession.SelectedItem.Text + ".xls";
            DataSet ds = objCC.GetStudentExamFormNotApprovedByDepartment(sessionno, schemeno, semesterno, degreeno, branchno, examtype, college_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, filename);

            }
        }
    }

    private void ExcelReport(DataSet ds, string Title)
    {
        GridView GV = new GridView();
        string ContentType = string.Empty;

        GV.DataSource = ds;
        GV.DataBind();
        string attachment = "attachment; filename=" + Title + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GV.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            int examtype = 0;
            string orderBy = string.Empty;
            examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            if (ddlOrderBy.SelectedValue == "0")
            {
                orderBy = "ROLL_NO";
            }
            else
            {
                orderBy = "REGNO";
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlCollege.SelectedItem.Text + "_" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSemester.SelectedItem.Text + "_" + ddlCourse.SelectedValue + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            //if (rdbReport.SelectedValue == "1")
            //{
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_ORDER_BY=" + orderBy + ",@P_EXAMTYPE=" + examtype;
            //}
            //else if (rdbReport.SelectedValue == "2")
            //{
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;//,@P_COLLEGE_ID=" + ddlCollege.SelectedValue + "
            //}
            //else if (rdbReport.SelectedValue == "3")
            //{

            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_EXAMTYPE=" + examtype;
            //}
            //else if (rdbReport.SelectedValue == "4")
            //{

            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_ORDER_BY=" + orderBy + ",@P_EXAMTYPE=" + examtype;
            //}
            // else if (rdbReport.SelectedValue == "5")
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_EXAMTYPE=" + examtype + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_EXAMTYPE=" + examtype;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_ORDER_BY=" + orderBy + ",@P_EXAMTYPE=" + examtype;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO desc");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK), ACD_COLLEGE_DEGREE C WITH (NOLOCK), ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND (C.COLLEGE_ID=" + ddlCollege.SelectedValue + " OR " + ddlCollege.SelectedValue + "= 0)", "DEGREENO");
            //ddlDegree.Focus();
        }
        else
        {
            ddlCollege.SelectedIndex = 0;
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
        //lblStatus.Text = string.Empty;
        // Fill Dropdown section
        //objCommon.FillDropDownList(ddlSemester, "ACD_COURSE C,ACD_SEMESTER SEM", "DISTINCT C.SEMESTERNO", "SEM.SEMESTERNAME", "SEM.SEMESTERNO=C.SEMESTERNO AND  SEM.SEMESTERNO > 0 AND  SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) , "SEC.SECTIONNAME");

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlCollege.Items.Clear();
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID"); 

        //ddlDegree.Items.Clear();
        // ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("PLease Select", "0"));
        Panel1.Visible = false;
        btnSendSMS.Visible = false;
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        int Sessionno = ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
        int College_id = ddlCollege.SelectedIndex > 0 ? Convert.ToInt32(ddlCollege.SelectedValue) : 0;
        int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        int Schemeno = ddlScheme.SelectedIndex > 0 ? Convert.ToInt32(ddlScheme.SelectedValue) : 0;
        int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        int ExamType = ddlExamType.SelectedIndex > 0 ? Convert.ToInt32(ddlExamType.SelectedValue) : -1;

        //int ReportType = 1;
        divSendOptions.Visible = true;

        divSubject.Style.Add("display", "none");
        divMessage.Style.Add("display", "none");

        if (rdbReport.SelectedValue == "1")
        {
            int ReportType = 1;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "2")
        {
            int ReportType = 2;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "3")
        {
            int ReportType3 = 3;
            DataSet ds3 = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType3);

            if (ds3.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds3;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
                divSendOptions.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "4")
        {
            int ReportType4 = 4;
            DataSet ds4 = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType4);

            if (ds4.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds4;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "9")
        {
            int ReportType = 9;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "10")
        {
            int ReportType = 10;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "11")
        {
            int ReportType = 11;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "13")
        {
            int ReportType = 13;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        if (rdbReport.SelectedValue == "14")
        {
            int ReportType = 14;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        #region Students who have not to filled exam form

        // Add on date 27/02/2023 Students who have not to filled exam form for RCPIPER created By Sagar Mankar

        if (rdbReport.SelectedValue == "15")
        {
            int ReportType = 15;
            DataSet ds = objSC.StudentExamRegisteredList(Sessionno, College_id, Degreeno, Branchno, Schemeno, Semesterno, ExamType, ReportType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //hdncount2.Value = ds.Tables[0].Rows.Count.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                Panel1.Visible = true;
                hdncount1.Value = lvStudents.Items.Count.ToString();

                btnSendSMS.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                divSendOptions.Visible = false;
                divSendOptions.Visible = false;
            }
        }

        //

        #endregion

        //else if (rdbReport.SelectedValue == "7")
        //{
        //
        //
        //    DataSet ds = objSC.StudentSessionWiseExamRegisteredCount(Convert.ToInt16(ddlSession.SelectedValue));
        //
        //    if (ds.Tables[0].Rows.Count > 0)
        //    { Panel1.Visible = true;
        //        lvStudents.DataSource = ds;
        //        lvStudents.DataBind();
        //        hdncount1.Value = lvStudents.Items.Count.ToString();
        //        btnSendSMS.Visible = true;
        //    }
        //    else
        //    {
        //
        //        objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
        //
        //    }
        //
        //}
        //else if (rdbReport.SelectedValue == "8")
        //{
        //    DataSet ds = objSC.StudentSessionWiseExamRegisteredDetails(Convert.ToInt16(ddlSession.SelectedValue));
        //
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //       Panel1.Visible = true;
        //        lvStudents.DataSource = ds;
        //        lvStudents.DataBind();
        //        hdncount1.Value = lvStudents.Items.Count.ToString();
        //        btnSendSMS.Visible = true;
        //    
        //    }
        //    else
        //    {
        //
        //        objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
        //
        //    }
        //
        //}
    }

    protected void rbNet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbNet.SelectedValue == "1")
        {
            btnSendSMS.Visible = true;
            btnSendSMS.Enabled = true;
            // txtSubject.Visible = false;
            btnSendSMS.Text = "Send SMS";
            divSubject.Visible = false;
            txtSubject.Text = "";
            txtMatter.Text = "";

            txtMatter.Focus();

            divSubject.Style.Add("display", "none");

            divMessage.Style.Add("display", "block");
            txtMatter.Style.Add("display", "block");
        }
        else if (rbNet.SelectedValue == "2")
        {
            btnSendSMS.Enabled = true;
            btnSendSMS.Text = "Send Email";
            divSubject.Visible = true;

            txtSubject.Text = "";
            txtMatter.Text = "";

            txtSubject.Focus();

            divSubject.Style.Add("display", "block");
            divMessage.Style.Add("display", "block");
            txtMatter.Style.Add("display", "block");
            txtSubject.Style.Add("display", "block");
        }

        btnSendSMS.Style.Add("display", "block");
    }

    //ADDED BY SAFAL GUPTA 18052021 FOR MAIL SENT 
    //static async Task<int> Execute(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;

    //    try
    //    {

    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
    //        var toAddress = new MailAddress(toEmailId, "");

    //        var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //        var client = new SendGridClient(apiKey);
    //        var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
    //        var subject = sub;
    //        var to = new EmailAddress(toEmailId, "");
    //        var plainTextContent = "";

    //        //string file = Session["FileName"].ToString();
    //        ////If want to send attachment in email
    //        //if (file != string.Empty || file != "")  
    //        //{
    //        //    if (.HasFile)
    //        //    {
    //        //        //MemoryStream stream = new MemoryStream(data.attachment);
    //        //        myMessage.AddAttachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());
    //        //    }
    //        //}

    //        var htmlContent = Message;
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        var response = await client.SendEmailAsync(msg);
    //        // var myMessage = new SendGridMessage();

    //        string res = Convert.ToString(response.StatusCode);
    //        if (res == "Accepted")
    //        {
    //            ret = 1;
    //        }
    //        else
    //        {
    //            ret = 0;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}


    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;

            //dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_NAME", "OrganizationId=2" , string.Empty);  //Convert.ToInt32(Session["OrgId"])
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    //ADDED BY SAFAL GUPTA 18052021
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            //string folderPath = Server.MapPath("~/TempDocument/");
            ////string folderPath = @"E:\Images\"; // Your path Where you want to save other than Server.MapPath
            ////Check whether Directory (Folder) exists.
            //if (!Directory.Exists(folderPath))
            //{
            //    //If Directory (Folder) does not exists. Create it.
            //    Directory.CreateDirectory(folderPath);
            //}

            ////Save the File to the Directory (Folder).
            //Session["FileName"] = fuAttachment.FileName;
            //if (Session["FileName"] != string.Empty || Session["FileName"] != "")  
            //{
            //    fuAttachment.SaveAs(folderPath + Path.GetFileName(fuAttachment.FileName));
            //}

            if (rbNet.SelectedValue == "1")
            {
                if (txtMatter.Text == "")
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter the Message", this.Page);
                }
            }
            else if (rbNet.SelectedValue == "2")
            {
                if (txtSubject.Text == "")
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter the Subject", this.Page);
                    txtSubject.Focus();
                }
                //else if (txtMatter.Text == "")
                //{
                //    objCommon.DisplayMessage(this.Page, "Please Enter Message", this.Page);
                //}
                else
                {
                    try
                    {
                        DataSet dsconfig = null;
                        dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                        //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
                        string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                        string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

                        string StudRegNO = string.Empty;
                        int count = 0;
                        int status = 0;

                        foreach (ListViewDataItem item in lvStudents.Items)
                        {
                            CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                            Label lblMobile = item.FindControl("lblMobile") as Label;
                            Label lblEmail = item.FindControl("lblEmail") as Label;

                            //int status = 0;

                            if (chek.Checked == true)
                            {
                                count++;
                                StudRegNO += chek.ToolTip + "$";

                                //if (Convert.ToInt32(SENDGRID_STATUS) == 1)
                                //{
                                //    Task<int> task = Execute(txtMatter.Text, lblEmail.Text, txtSubject.Text + " " + SUBJECT_OTP + " ERP || OTP for reset password");
                                //    status = task.Result;
                                //}
                                //else
                                //{
                                //    status = sendEmail(txtMatter.Text, lblEmail.Text, txtSubject.Text + " " + SUBJECT_OTP + " ERP");
                                //}

                                //status = objSendEmail.SendEmail(txtMatter.Text, lblEmail.Text, txtSubject.Text + " " + SUBJECT_OTP + " ERP "); //Calling Method
                                status = objSendEmail.SendEmail(lblEmail.Text, txtMatter.Text, "" + SUBJECT_OTP + " ERP || " + txtSubject.Text + ""); //Calling Method
                            }
                        }

                        //if (count != 0)
                        if (status == 1)
                        {
                            objCommon.DisplayMessage(this.Page, "Mail Sent Successfully For Selected Students.", this.Page);
                            txtMatter.Text = "";
                            txtSubject.Text = "";

                            return;
                        }
                        else
                        {
                            Showmessage("Failed to send email.");
                            return;
                        }

                        if (count == 0)
                        {
                            // objCommon.DisplayMessage("Please Select at least one Student!", this.Page);
                            objCommon.DisplayMessage(this.Page, "Please Select at least one Student.", this.Page);
                            return;
                        }
                    }
                    catch
                    {

                    }

                    #region Comment

                    //////////////////////////***********************/////////////////////////////

                    //string useremail = txtEmail.Text.Trim().Replace("'", "");
                    //string mail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_EMAIL='" + useremail + "' ");

                    //if (mail == useremail && txtEmail.Text.Trim() != "")
                    //{
                    //lblotp2.Visible = true;
                    //txtOtp2.Visible = true;
                    //btnVerifyEmailOtp.Visible = true;
                    //try
                    //{
                    //    int status = 0;
                    //    //.Visible = true;
                    //    //string otp = GenerateOTP(5);
                    //    //ViewState["EmailOTP"] = otp;

                    //    //added by Tejas jaiswal 16/12/2021
                    //    DataSet dsconfig = null;
                    //    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "SUBJECT_OTP", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                    //    //int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
                    //    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                    //    string SUBJECT_OTP = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString();

                    //    //string StudName = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_EMAIL='" + useremail + "' ");
                    //    //string message = "<b>Dear " + StudName + "," + "</b><br />";
                    //    //message += "Your One Time Password (OTP) for Reset password is " + otp;
                    //    //message += "<br /><br /><br />Thank You<br />";
                    //    //message += "<b>Team " + SUBJECT_OTP + "," + "</b><br />";//added by Tejas jaiswal 16/12/2021
                    //    //message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                    //    //if (txtEmail.Text != string.Empty)
                    //    //{
                    //    try
                    //    {


                    //        //added by Tejas jaiswal 16/12/2021
                    //        if (Convert.ToInt32(SENDGRID_STATUS) == 1)
                    //        {
                    //            //status = SendMailBYSendgrid(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
                    //            //status = sendEmail(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
                    //            Task<int> task = Execute(txtMatter.Text, "sagarmankar7472@gmail.com", SUBJECT_OTP + " ERP || OTP for reset password");
                    //            status = task.Result;

                    //        }
                    //        else
                    //        {

                    //            status = sendEmail(txtMatter.Text, "HI HELLO", SUBJECT_OTP + " ERP || OTP for reset password");

                    //        }
                    //        if (status == 1)
                    //        {
                    //            Showmessage("OTP has been send on Your Email Id, Enter To Continue Reset Password Process.");

                    //        }
                    //        else
                    //        {
                    //            Showmessage("Failed to send email");
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {
                    //        Showmessage("Failed to send email");
                    //    }
                    //    //}

                    //}

                    //catch (Exception)
                    //{
                    //    //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
                    //    return;
                    //}

                    //}
                    //else
                    //{

                    //    Showmessage("Sorry,Your Entered Email Address is Not Registerd..Please Contact to Admin");
                    //    lblotp2.Visible = false;
                    //    txtOtp2.Visible = false;
                    //    txtEmail.Text = string.Empty;
                    //    return;

                    //}

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Exam_Registration_Report.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    public int sendEmail(string message, string emailid, string subject)
    {
        int ret = 0;
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        //SmtpClient SmtpServer = new SmtpClient("smtp-relay.gmail.com");



        DataSet dsconfig = null;
        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName,SUBJECT_OTP", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
        string emailfrom = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
        string emailpass = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
        //int fascility = Convert.ToInt32(dsconfig.Tables[0].Rows[0]["FASCILITY"].ToString());
        //if (fascility == 1 || fascility == 3)
        //{
        if (emailfrom != "" && emailpass != "")
        {

            mail.From = new MailAddress(emailfrom);
            string MailFrom = emailfrom;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, emailpass);
            SmtpServer.EnableSsl = true;
            string aa = string.Empty;
            mail.Subject = subject;
            mail.To.Clear();
            mail.To.Add(emailid);

            mail.IsBodyHtml = true;
            mail.Body = message;
            SmtpServer.UseDefaultCredentials = false;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };


            SmtpServer.Send(mail);

            if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
            {
                return ret = 1;

                //Storing the details of sent email


            }

        }
        return ret = 0;
        // }
        // return ret = 0;
    }



    //public int sendEmail(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName,SUBJECT_OTP", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var toAddress = new MailAddress(toEmailId, "");
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //        var smtp = new SmtpClient
    //        {
    //            Host = "smtp.gmail.com",
    //            Port = 587,
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //        };
    //        using (var message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = sub,
    //            Body = Message,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true
    //        })
    //        {
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(message);
    //            return ret = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
}