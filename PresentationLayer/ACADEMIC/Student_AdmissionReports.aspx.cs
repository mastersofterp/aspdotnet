//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Student Admission Reports
// CREATION DATE : 04/04/2024
// CREATED BY    : VAISHNAVI BELEKAR
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using ClosedXML.Excel;
using System.IO;
using System.Data;

public partial class ACADEMIC_Student_AdmissionReports : System.Web.UI.Page
{

    Common objCommon = new Common();
    CourseTeacherAllotController objAllot = new CourseTeacherAllotController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    // ViewState["ReportType"] = null;
                    BindDropDown();

                }
                divMsg.InnerHtml = "";


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_AdmissionReports.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_AdmissionReports.aspx");
        }
    }

    

    protected void BindDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlReport, "ACD_STUDENT_ADMISSION_REPORT", "RID", "REPORT_NAME", "RID>0 AND ACTIVE_STATUS= 1", "RID DESC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ISNULL(ACTIVESTATUS,0) = 1", "YEAR");
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ACTIVESTATUS = 1", "DEGREENO ASC");

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindDropDown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }




    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlHideStuReport.Visible = false;
        Institute.Visible = false;
        Degree.Visible = false;
        Branch.Visible = false;
        Sessionn.Visible = false;
        Semester.Visible = false;
        Rdoaddress.Visible = false;
        RdoStuName.Visible = false;
        AdmBatch.Visible = false;
        StudentType.Visible = false;
        Acdyear.Visible = false;
        SummaryBy.Visible = false;
        Year.Visible = false;

        btnStudentCancel.Visible = false;
        btnShow.Visible = false;
        btnAdmSummaryReport.Visible = false;
        btnView.Visible = false;
        btnShowStrengthReports.Visible = false;
        btnStudentReport.Visible = false;
        btnSummaryrpt.Visible = false;
        btnAllYearExcel.Visible = false;
        btnAllYearView.Visible = false;
        btnEnrollFormExcel.Visible = false;
        btnEnrollForm.Visible = false;
        btnAdmRegister.Visible = false;
        btnEnrollRegister.Visible = false;
        btnAdmRegisterView.Visible = false;
        btnEnrollRegisterView.Visible = false;
        btnBYCStrength.Visible = false;
      

        ddlAcdYear.SelectedValue = "0";
        ddlAdmbatch.SelectedValue = "0";
        ddlDegree.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        ddlSession.SelectedValue = "0";
        ddlCollege.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";
        ddlYear.SelectedValue = "0";
        chkReportType.ClearSelection();
        txtSearchText.Text = string.Empty;
        ClearGridListView();
        ClgMandatory.Visible = false;
        DegreeMandatory.Visible = false;
        BranchMandatory.Visible = false;
        AcdMeandatory.Visible = false;

        if (ddlReport.SelectedValue == "1")   // STUDENT REPORT
        {

            pnlHideStuReport.Visible = true;
            pnlfooter.Visible = true;
            btnStudentCancel.Visible = true;
            //btnAdmSummaryReport.Visible = false;
            //btnView.Visible = false;
            //btnSummaryrpt.Visible = false;
            //btnShowStrengthReports.Visible = false;
            //BindListview();
            lvAllYearStudents.DataSource = null;
            lvAllYearStudents.DataBind();
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            lvStuEnrollForm.DataSource = null;
            lvStuEnrollForm.DataBind();
            lvStuAdmRegister.DataSource = null;
            lvStuAdmRegister.DataBind();
            lvStuEnrollRegister.DataSource = null;
            lvStuEnrollRegister.DataBind();
        }

        if (ddlReport.SelectedValue == "2") // ADMISSION SUMMARY
        {

            btnAdmSummaryReport.Visible = true;
            pnlfooter.Visible = true;
            btnStudentCancel.Visible = true;
            //btnStudentReport.Visible = false;
            Institute.Visible = true;
            Degree.Visible = true;
            Branch.Visible = true;
            Sessionn.Visible = true;

            StudentType.Visible = true;
            //btnShow.Visible = false;

            //btnView.Visible = false;
            //btnSummaryrpt.Visible = false;
            //btnShowStrengthReports.Visible = false;
            ClearGridListView();
            ClgMandatory.Visible = true;

        }
        if (ddlReport.SelectedValue == "3")  // STUDENT ADDRESS
        {

           // btnAdmSummaryReport.Visible = true;
            pnlfooter.Visible = true;
            btnStudentCancel.Visible = true;
            Institute.Visible = true;
            Degree.Visible = true;
            Branch.Visible = true;

            Semester.Visible = true;
            Rdoaddress.Visible = true;
            RdoStuName.Visible = true;

            //btnAdmSummaryReport.Visible = false;
            //btnStudentReport.Visible = false;
            btnShow.Visible = true;
            lstDetails.DataSource = null;
            lstDetails.DataBind();

            //btnView.Visible = false;
            //btnSummaryrpt.Visible = false;
            //btnShowStrengthReports.Visible = false;
            ClearGridListView();
            ClgMandatory.Visible = true;
            DegreeMandatory.Visible = true;
            BranchMandatory.Visible = true;
            //btnBYCStrength.Visible = false;
        }
        if (ddlReport.SelectedValue == "4")  // ADMISSION SUMMARY REPORT
        {
            //btnAdmSummaryReport.Visible = false;
            pnlfooter.Visible = true;
            btnStudentCancel.Visible = true;
            //btnStudentReport.Visible = false;
            //btnShow.Visible = false;
            AdmBatch.Visible = true;
            Institute.Visible = true;
            Degree.Visible = true;
          
            Branch.Visible = true;
            Semester.Visible = true;

            btnSummaryrpt.Visible = true;

            //btnView.Visible = false;
            //btnShowStrengthReports.Visible = false;
            ClearGridListView();

        }

        if (ddlReport.SelectedValue == "5")  // STUDENT STRENGTH SUMMARY REPORT
        {
            Sessionn.Visible = true;
            Institute.Visible = true;
            Acdyear.Visible = true;
            SummaryBy.Visible = true;
            pnlfooter.Visible = true;
            btnStudentCancel.Visible = true;
            //btnStudentReport.Visible = false;
            //btnSummaryrpt.Visible = false;
            //btnShow.Visible = false;
            btnShowStrengthReports.Visible = true;

            Sessionn.Visible = true;

            btnView.Visible = true;
            //btnAdmSummaryReport.Visible = false;
           
            //BindGridView();
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lvAllYearStudents.DataSource = null;
            lvAllYearStudents.DataBind();
            Panel1.Visible = false;
            ClgMandatory.Visible = true;
            lvStuEnrollForm.DataSource = null;
            lvStuEnrollForm.DataBind();
            lvStuAdmRegister.DataSource = null;
            lvStuAdmRegister.DataBind();
            lvStuEnrollRegister.DataSource = null;
            lvStuEnrollRegister.DataBind();
        }

        if (ddlReport.SelectedValue == "6")   // ADMISSION REGISTER
        {
            Acdyear.Visible = true;
            Year.Visible = true;
            pnlfooter.Visible = true;
            btnAdmRegister.Visible = true;
            ClearGridListView();
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            dvGrid.Visible = false;
            lvAllYearStudents.DataSource = null;
            lvAllYearStudents.DataBind();
            lvStuEnrollForm.DataSource = null;
            lvStuEnrollForm.DataBind();
            lvStuEnrollRegister.DataSource = null;
            lvStuEnrollRegister.DataBind();  
            
            btnStudentCancel.Visible = true;
            //btnStudentReport.Visible = false;
            //btnSummaryrpt.Visible = false;
            //btnShow.Visible = false;
            //btnAdmSummaryReport.Visible = false;
            //btnShowStrengthReports.Visible = false;
            AcdMeandatory.Visible = true;
            btnAdmRegisterView.Visible = true;
            Degree.Visible = true;
         

        }
        if (ddlReport.SelectedValue == "7")  // ENROLLMEMNT REGISTER
        {
            btnEnrollRegister.Visible = true;
            Acdyear.Visible = true;
            Year.Visible = true;
            pnlfooter.Visible = true;
            ClearGridListView();
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            dvGrid.Visible = false;
            lvAllYearStudents.DataSource = null;
            lvAllYearStudents.DataBind();
            lvStuEnrollForm.DataSource = null;
            lvStuEnrollForm.DataBind(); 
            lvStuAdmRegister.DataSource = null;
            lvStuAdmRegister.DataBind();
            Degree.Visible = true;
           

            
            btnStudentCancel.Visible = true;
            //btnStudentReport.Visible = false;
            //btnSummaryrpt.Visible = false;
            //btnShow.Visible = false;
            //btnAdmSummaryReport.Visible = false;
            //btnShowStrengthReports.Visible = false;
            AcdMeandatory.Visible = true;
            btnEnrollRegisterView.Visible = true;
        }

        if (ddlReport.SelectedValue == "8")  // ALL YEAR/SEMESTER ADMITTED STUDENTS LIST
        {
            pnlfooter.Visible = true;
            Acdyear.Visible = true;
            //Year.Visible = true;
            btnAllYearExcel.Visible = true;
            btnAllYearView.Visible = true;
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            lvStuAdmRegister.DataSource = null;
            lvStuAdmRegister.DataBind();
            lvStuEnrollRegister.DataSource = null;
            lvStuEnrollRegister.DataBind();
            lvStuEnrollForm.DataSource = null;
            lvStuEnrollForm.DataBind();
            //btnAdmSummaryReport.Visible = false;
            //btnShowStrengthReports.Visible = false;
            //btnStudentReport.Visible = false;
            //btnSummaryrpt.Visible = false;
            btnStudentCancel.Visible = true;
            AcdMeandatory.Visible = true;
            
        }
        if (ddlReport.SelectedValue == "9") // ENROLLMENT FORM 
        {
            pnlfooter.Visible = true;
            Acdyear.Visible = true;
            Year.Visible = true;
            btnEnrollFormExcel.Visible = true;
            btnEnrollForm.Visible = true;
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            lvAllYearStudents.DataSource = null;
            lvAllYearStudents.DataBind();
            lvStuAdmRegister.DataSource = null;
            lvStuAdmRegister.DataBind();
            lvStuEnrollRegister.DataSource = null;
            lvStuEnrollRegister.DataBind();
            //btnAdmSummaryReport.Visible = false;
            //btnShowStrengthReports.Visible = false;
            //btnStudentReport.Visible = false;
            //btnSummaryrpt.Visible = false;
            btnStudentCancel.Visible = true;
            AcdMeandatory.Visible = true;
            Degree.Visible = true;
          
        }
        if (ddlReport.SelectedValue == "10") // BRANCH YEAR CATEGORYWISE STRENGTH OF STUDENT
        {
            pnlfooter.Visible = true;
            Acdyear.Visible = true;
            Year.Visible = true;
            ClearGridListView();
            btnStudentCancel.Visible = true;
            btnBYCStrength.Visible = true;
            AcdMeandatory.Visible = true;
        }

    }

    protected void ClearGridListView()
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        Panel1.Visible = false;
        lstDetails.DataSource = null;
        lstDetails.DataBind();
        panell.Visible = false;
        dvGrid.Visible = false;
        lvAllYearStudents.DataSource = null;
        lvAllYearStudents.DataBind();
        Panel2.Visible = false;
        lvStuEnrollForm.DataSource = null;
        lvStuEnrollForm.DataBind();
        Panel3.Visible = false;
        lvStuAdmRegister.DataSource = null;
        lvStuAdmRegister.DataBind();
        Panel4.Visible = false;
        lvStuEnrollRegister.DataSource = null;
        lvStuEnrollRegister.DataBind();
        Panel5.Visible = false;

    }

    // Student Report 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindListview();
        }
       
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnShow_Click->() " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListview()
    {
        StudentController studCont = new StudentController();
        string searchText = txtSearchText.Text.Trim();
        string searchBy = (rdoEnrollmentNo.Checked ? "enrollmentno" : (rdoStudentName.Checked ? "name" : (rdoRollNo.Checked ? "regno" : "idno")));
        DataSet ds = studCont.RetrieveStudentDetails(searchText, searchBy);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Panel1.Visible = true;
                    lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataBind();
                }
            }
            else
            {
                Panel1.Visible = false;
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
                objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
            }
            }
        
    }

    protected void btnShowReport(object sender, EventArgs e)
    {
        if (rdoVerticalReport.Checked)
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowStuReportVr(param, "STUDENTADMISSIONDETAILS", "Admission_Slip_Confirm_PHD_General_New.rpt");
        }
        else
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowStuReportHz(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal_New.rpt");
        }
    }

    protected void btnStudentReport_Click(object sender, EventArgs e)
    {
        if (rdoVerticalReport.Checked)
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowStuReportVr(param, "STUDENTADMISSIONDETAILS", "Admission_Slip_Confirm_PHD_General_New.rpt");
        }
        else
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowStuReportHz(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal_New.rpt");
        }
    }

    protected void btnStudentCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlAcdYear.SelectedValue = "0";
            ddlAdmbatch.SelectedValue = "0";
            ddlDegree.SelectedValue = "0";
            ddlSemester.SelectedValue = "0";
            ddlSession.SelectedValue = "0";
            ddlCollege.SelectedValue = "0";
            ddlBranch.SelectedValue = "0";
            ddlYear.SelectedValue = "0";
            txtSearchText.Text = string.Empty;
            chkReportType.ClearSelection();
            ClearGridListView();
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.ShowStuReportVr()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowStuReportVr(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + param;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStuAdmission, this.updStuAdmission.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.ShowStuReportVr()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowStuReportHz(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + param;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStuAdmission, this.updStuAdmission.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.ShowStuReportHz()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string GetParamsForAllStudent()
    {
        return string.Empty;
    }

    private string GetParamsForSingleStudent(string idno)
    {
        string param = "@P_IDNO=" + idno;
        return param;
    }

    // Admission Summary

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + "AND B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND B.COLLEGE_ID > 0 AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.DEGREENO");
            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.SelectedIndex = 0;
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
        }
    }
    protected void btnAdmSummaryReport_Click(object sender, EventArgs e)
    {
        string check = objCommon.LookUp("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT R WITH (NOLOCK) ON (S.IDNO=R.IDNO)", "COUNT(DISTINCT S.IDNO)", "R.EXAM_REGISTERED=1 AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue));
        if (check == "0")
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found!!", this.Page);
        }
        else
        {
            ShowAdmSummary("StudentStrength", "StudentStrengthDetailsCount.rpt");
        }
    }


    private void ShowAdmSummary(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PREV_STATUS=" + ddlStudType.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue +",username=" + Session["username"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PREV_STATUS=" + ddlStudType.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue;

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStuAdmission, this.updStuAdmission.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.ShowAdmSummary()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // Student Address

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "SM.SEMESTERNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue, "SM.SEMESTERNAME");
        }
        else
        {
            ddlSemester.SelectedIndex = 0;

        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowStuAddressReport("STUDENT_ADDRESS_INFO", "StudentLocalAddress.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnShow_Click->() " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStuAddressReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_DEGREENO=" + ddlDegree.SelectedValue
                + ",@P_BRANCHNO=" + ddlBranch.SelectedValue
                + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue
                + ",@P_NAME=" + (rdoFatherName.Checked ? "2" : "1")
                + ",@P_ADDRESS=" + (rdoLocalAddress.Checked ? "1" : "2")
                + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_COLLEGEID=" + Convert.ToInt32(ViewState["college_id"]);


            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updStuAdmission, this.updStuAdmission.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.ShowStuAddressReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Admission Summary Report

    protected void btnSummaryrpt_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.GetAllAdmissionSummeryforExcel(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));

        GridView GV = new GridView();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=Admission_Summary_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                }
            }
        }

    }

    

    // Student Strength Summary Report

    private void Export()
    {
        ViewState["sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue));

        string attachment = "attachment; filename=" + "Student_Strength_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        ViewState["sessionno"] = "0";
        DataSet dsfee = objSC.GetStudentStrengthDetails(Convert.ToInt32(ViewState["sessionno"].ToString()), ViewState["ReportType"].ToString(), Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        ViewState["sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue));

        string ReportType = string.Empty;
        foreach (ListItem item in chkReportType.Items)
        {
            if (item.Selected == true)
            {
                ReportType = ReportType + item.Text.ToString() + ",";
            }
        }
        if (ReportType != "")
        {
            if (ReportType.Substring(ReportType.Length - 1) == ",")
            {
                ReportType = ReportType.Substring(0, ReportType.Length - 1);

            }
        }
        else
        {
            dvGrid.Visible = false;
            objCommon.DisplayUserMessage(updStuAdmission, "Please Select Atleast One Report Type!", this.Page);
            //btnShowStrengthReports.Visible = false;
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            return;
        }

        ViewState["ReportType"] = ReportType + " ";
        string check = ViewState["sessionno"].ToString();
        if (string.IsNullOrEmpty(check))
        {
            ViewState["sessionno"] = "0";

        }
        BindGridView();

    }

    protected void BindGridView()
    {
        DataSet ds = objSC.GetStudentStrengthDetails(Convert.ToInt32(ViewState["sessionno"].ToString()), ViewState["ReportType"].ToString(), Convert.ToInt32(ddlAcdYear.SelectedValue));
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
               if (ds.Tables[0].Rows.Count > 0)
                {
                    panell.Visible = true;
                    btnShowStrengthReports.Visible = true;
                    lstDetails.DataSource = ds;
                    lstDetails.DataBind();
                    dvGrid.Visible = true;
                }
                else
                {
                    panell.Visible = false;
                    btnShowStrengthReports.Visible = false;
                    lstDetails.DataSource = null;
                    lstDetails.DataBind();
                    dvGrid.Visible = false;
                    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                }
            }
        }
    }


    protected void btnShowStrengthReports_Click(object sender, EventArgs e)
    {
        try
        {
            string ReportType = string.Empty;
            foreach (ListItem item in chkReportType.Items)
            {
                if (item.Selected == true)
                {
                    ReportType = ReportType + item.Text.ToString() + ",";
                }
            }

            if (ReportType != "")
            {
                if (ReportType.Substring(ReportType.Length - 1) == ",")
                {
                    ReportType = ReportType.Substring(0, ReportType.Length - 1);

                }
            }
            else
            {
                dvGrid.Visible = false;
                objCommon.DisplayUserMessage(updStuAdmission, "Please Select Atleast One Report Type!", this.Page);
                btnShowStrengthReports.Visible = false;
                lstDetails.DataSource = null;
                lstDetails.DataBind();
                return;
            }
            ViewState["ReportType"] = ReportType + " ";
            dvGrid.Visible = true;
            Export();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnShowStrengthReports_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Admission Register 

    //protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlAcdYear.SelectedIndex > 0)
    //        {

    //            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlAcdYear.SelectedValue + "AND B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND B.COLLEGE_ID > 0 AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.DEGREENO");
    //        }
    //        else
    //        {
    //            ddlDegree.SelectedIndex = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnShowStrengthReports_Click()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}

    protected void btnAdmRegisterView_Click(object sender, EventArgs e)
    {
        try
        {
            BindListViewAdmRegister();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnShowStrengthReports_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void BindListViewAdmRegister()
    {
        try
        {

            DataSet ds = objSC.GetStudentAdmRegister(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Panel4.Visible = true;
                        lvStuAdmRegister.DataSource = ds;
                        lvStuAdmRegister.DataBind();

                    }
                    else
                    {
                        Panel4.Visible = false;
                        lvStuAdmRegister.DataSource = null;
                        lvStuAdmRegister.DataBind();
                        objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnAdmRegister_Click(object sender, EventArgs e)
    {
        GridView GV = new GridView();

        DataSet ds = objSC.GetStudentAdmRegister(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=Admission_Register_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                }
            }
        }
    }

    // Enrollment Register

    protected void btnEnrollRegisterView_Click(object sender, EventArgs e)
    {
        try
        {
            BindListViewEnrollRegister();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnShowStrengthReports_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void BindListViewEnrollRegister()
    {
        try
        {
            DataSet ds = objSC.GetStudentEnrollRegister(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Panel5.Visible = true;
                        lvStuEnrollRegister.DataSource = ds;
                        lvStuEnrollRegister.DataBind();

                    }
                    else
                    {
                        Panel5.Visible = false;
                        lvStuEnrollRegister.DataSource = null;
                        lvStuEnrollRegister.DataBind();
                        objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnEnrollRegister_Click(object sender, EventArgs e)
    {
        GridView GV = new GridView();

        DataSet ds = objSC.GetStudentEnrollRegister(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if ( ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=Enrollment_Register_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                }
            }
        }
    }

    // All Year/Semester Admitted Students List


    protected void btnAllYearView_Click(object sender, EventArgs e)
    {
        try
        {
            BindListViewAllYear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindListViewAllYear()
    {
        try
        {

            DataSet ds = objSC.GetAllYearStudents(Convert.ToInt32(ddlAcdYear.SelectedValue));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Panel2.Visible = true;

                        lvAllYearStudents.DataSource = ds;
                        lvAllYearStudents.DataBind();

                    }
                    else
                    {
                        Panel2.Visible = false;
                        lvAllYearStudents.DataSource = null;
                        lvAllYearStudents.DataBind();
                        objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnAllYearExcel_Click(object sender, EventArgs e)
    {

        GridView GV = new GridView();

        DataSet ds = objSC.GetAllYearStudents(Convert.ToInt32(ddlAcdYear.SelectedValue));
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if ( ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=All_Year/Semester_Admitted_Students_List_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                }
            }
        }
    }

    // Enrollment Form
    protected void btnEnrollForm_Click(object sender, EventArgs e)
    {
        try
        {
            BindListViewEnrollForm();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindListViewEnrollForm()
    {
        try
        {
            DataSet ds = objSC.GetStudentEnrollForm(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if ( ds.Tables[0].Rows.Count > 0)
                    {
                        Panel3.Visible = true;

                        lvStuEnrollForm.DataSource = ds;
                        lvStuEnrollForm.DataBind();

                    }
                    else
                    {
                        Panel3.Visible = false;
                        lvStuEnrollForm.DataSource = null;
                        lvStuEnrollForm.DataBind();
                        objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnEnrollFormExcel_Click(object sender, EventArgs e)
    {

        GridView GV = new GridView();

        DataSet ds = objSC.GetStudentEnrollForm(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=Enrollment_Form_A_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
                }
            }
        }
    }

    // Branch Year Category Wise Strength of Students


    protected void btnBYCStrength_Click(object sender, EventArgs e)
    {

        try
        {
            ShowBYCStrength("BYC Wise Strength of Students", "studStrength_v1_report.rpt"); 
            //if (Session["OrgId"].ToString() == "19")
            //{
            //    ShowBYCStrength("BYC Wise Strength of Students", "studStrength_v1_report.rpt"); // PCN
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
            //}
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.btnSummaryrpt_Click()->  " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private void ShowBYCStrength(string reportTitle, string rptFileName)
    {
        try
        {
            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ACADEMICYEAR=" + ddlAcdYear.SelectedValue + ",@P_YEAR=" + ddlYear.SelectedValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStuAdmission, this.updStuAdmission.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_AdmissionReports.ShowBYCStrength()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}