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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Exam_Registration_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
            //Fill Dropdown Degree
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
            //Fill Dropdown Session
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
            // Fill Semester Dropdown
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
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
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR,ACD_SEMESTER SEM", "DISTINCT SR.SEMESTERNO", "SEM.SEMESTERNAME","SEM.SEMESTERNO=SR.SEMESTERNO AND  SEM.SEMESTERNO > 0 AND  SR.SCHEMENO=" + ddlScheme.SelectedValue, "SEM.SEMESTERNAME");
            
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
            int examtype  = 0 ;
            //if (ddlExamType.SelectedValue == "-1")
            //   examtype = 0 ;
            //else
                examtype = Convert.ToInt32(ddlExamType.SelectedValue);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_EXAMTYPE=" + examtype;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
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
            if(rdbReport.SelectedValue=="3")
                count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            else
                count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue)+ " AND S.PRO=1");

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
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO="+Convert.ToInt32(ddlCourse.SelectedValue);
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
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
      
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Dropdown Scheme
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SCHEMENO");
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fill branch according degree selection
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Dropdown Course
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "CCODE + ' -- ' + COURSE_NAME", "SCHEMENO=" + ddlScheme.SelectedValue + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "COURSENO");

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        //Fill Dropdown section
        objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR,ACD_SECTION SEC", "DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SEC.SECTIONNO=SR.SECTIONNO AND  SEC.SECTIONNO > 0 AND  SR.SCHEMENO=" + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SEC.SECTIONNAME");

    }
    private void ShowCourseStudentCountReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName; ;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
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
            rfvSemester.Enabled = true;
        }
        else
            if (rdbReport.SelectedValue == "2")
            {
               // trreport.Visible = true;
                //rfvCourse.Enabled = false;
                rfvSemester.Enabled = false;

                rfvBranch.Enabled = false;
                rfvProgram.Enabled = false;
                rfvSemester.Enabled = false;
            }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {   
            //Course Registered Student
            if (rdbReport.SelectedValue == "1")
            {
                ShowReport("Exam_Wise_Registration_Report", "rptExamWiseStudRegistration.rpt");
            }
            else if (rdbReport.SelectedValue == "2")
            {
                ShowCourseStudentCountReport("ExamwiseStudentCount", "rptExamwiseStudentCount.rpt");
            }            
            else if (rdbReport.SelectedValue == "3")
            {
                ShowCourseRegistration("Exam_Registration_List", "ExamRegistrationList.rpt");
            }
            else if (rdbReport.SelectedValue == "4")
            {
                ShowReport("Student_Attendance_List", "rptExamStudPractAttendance.rpt");
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

    private void ShowCourseRegistration(string reportTitle, string rptFileName)
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
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_EXAMTYPE=" + examtype;
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
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
        if (rdbReport.SelectedValue == "1")
            ShowReportinFormate("xls", "rptExamWiseStudRegistration.rpt");
        else if (rdbReport.SelectedValue == "2")
        {
            ShowReportinFormate("xls", "rptExamwiseStudentCount.rpt");
        }
        else if (rdbReport.SelectedValue == "3")
        {
            ShowReportinFormate("xls", "ExamRegistrationList.rpt");
        }
        else if (rdbReport.SelectedValue == "4")
        {
            ShowReportinFormate("xls", "rptExamStudPractAttendance.rpt");
        }
    }
    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSemester.SelectedItem.Text + "_"+ddlCourse.SelectedValue+"_" + ddlSection.SelectedItem.Text + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            if(rdbReport.SelectedValue=="1")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue;
            else if (rdbReport.SelectedValue == "2")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            else if (rdbReport.SelectedValue == "3")
            {
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue;
            }
            else if (rdbReport.SelectedValue == "4")
            {
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue; 
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue;
            }
            else if (rdbReport.SelectedValue == "5")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
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