//======================================================================================
// PROJECT NAME  : UAIMS [NITRAIPUR]                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : GRADE ALLOTMENT                                                      
// ADDED DATE    : 19-JUNE-2012                                                          
// CREATED BY    : NIRAJ D. PHALKE                                                      
// MODIFIED DATE :                                                                      
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Threading;

public partial class ACADEMIC_EXAMINATION_GradeAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    ResultProcessing objResultProcessing = new ResultProcessing();
   
    Exam objExam = new Exam();

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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    this.FillDropdown();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GradeAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GradeAllotment.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO=" + Session["currentsession"].ToString(), "SESSIONNO");
           // ddlSession.Items.Add(new ListItem(Session["sessionname"].ToString(), Session["currentsession"].ToString()));
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0" , "SESSIONNO desc");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_ELECTGROUP G ON C.GROUPNO = G.GROUPNO INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO ", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME  AS COURSE_NAME", " C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "C.CCODE");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO ", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME  AS COURSE_NAME", " C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "C.CCODE");
            ddlCourse.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Branch Name
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            ddlBranch.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedValue == "1" && ddlBranch.SelectedValue == "99")
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.Items.Add(new ListItem("FIRST YEAR[AUTONOMOUS]", "1"));
                ddlScheme.Focus();
            }
            else
            {
                // Scheme Name
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMETYPE = 1 AND B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue), "B.BRANCHNO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            char ch = '-';
            string [] ccode = ddlCourse.SelectedItem.Text.Split(ch);
            //SectionNo
            objExamController.UpdateMarkTot(Convert.ToInt32(ddlSession.SelectedValue), ccode[0].Trim(), Convert.ToInt32(ddlSection.SelectedValue));       

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION SN INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SECTIONNO = SN.SECTIONNO)", "DISTINCT SN.SECTIONNO", "SN.SECTIONNAME", "SESSIONNO = " + ddlSession.SelectedValue + " AND CCODE = '" + ccode[0].Trim() + "'", "SECTIONNO");
            ddlSection.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InternalMarksAutonomous.ddlScheme_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SemesterNo
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);

            int courseno = 0;
            if (ddlDegree.SelectedValue == "1" && (ddlSemester.SelectedValue == "1" || ddlSemester.SelectedValue == "2"))
                courseno = 0;
            else
                courseno = Convert.ToInt32(ddlCourse.SelectedValue);

            CustomStatus ret = (CustomStatus)objExamController.GradeAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(txtMaxMarks.Text), Convert.ToInt32(txtMinMarks.Text),courseno, ccode[0].Trim(), Convert.ToInt32(ddlSection.SelectedValue));
            if (ret == CustomStatus.RecordSaved)
                objCommon.DisplayMessage("Grade Alloted Successfully", this.Page);
            else
                objCommon.DisplayMessage("Error...", this.Page);

            this.BindStudentList();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void BindStudentList()
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int prev_status = 0;//prev_status is not required in case of autonomous, but the same procedure is used for rtm where it is required, hence need to be send.

            DataSet dsStaus = objResultProcessing.GetMarkEntryStatus_BeforeResultProcess(sessionno, schemeno, semesterno, prev_status);
            if (dsStaus.Tables[0].Rows.Count > 0)
            {
                lvMarksNotEntered.DataSource = dsStaus;
                lvMarksNotEntered.DataBind();
                btnSubmit.Visible = false;
                btnSubmit2.Visible = false;
                if (dsStaus.Tables[0].Rows.Count > 0)
                    lblMarksNotEnteredStudents.Text = "Marks Not Entered Students : " + dsStaus.Tables[0].Rows.Count.ToString();

            }
            else
            {
                btnSubmit.Visible = true;
                btnSubmit2.Visible = true; 
                lblMarksNotEnteredStudents.Text = string.Empty;
                lvMarksNotEntered.DataSource = null;
                lvMarksNotEntered.DataBind();
            }

            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (SR.IDNO = S.IDNO) INNER JOIN ACD_SECTION SN ON (SR.SECTIONNO = SN.SECTIONNO)", "DISTINCT ROLL_NO,SN.SECTIONNAME,SN.SECTIONNO,(S.STUDNAME + ' ' + S.FATHERNAME + ' ' + S.LASTNAME) STUDNAME", "MARKTOT,GRADE", "(DETAIND IS NULL OR DETAIND = 0) AND (CANCEL IS NULL OR CANCEL = 0) AND SESSIONNO = " + ddlSession.SelectedValue + " AND CCODE = '" + ccode[0].ToString().Trim() + "'" + (ddlSection.SelectedIndex == 0 ? string.Empty : " AND SR.SECTIONNO = " + ddlSection.SelectedValue), "SN.SECTIONNO,ROLL_NO");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    lblStudents.Text = "Total No. of Students : " + ds.Tables[0].Rows.Count.ToString();

                }
                else
                {
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblStudents.Text = "Total No. of Students : 0";

                }
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                lblStudents.Text = "Total No. of Students : 0";

            }


           // DataSet dsGrades = objCommon.FillDropDown("ACD_GRADE_POINT", "GRADE_NAME", "MINMARK,MAXMARK,TOTAL_STU", "SESSIONNO = " + ddlSession.SelectedValue + " AND CCODE = '" + ccode[0].ToString().Trim() + "'" + (ddlSection.SelectedIndex == 0 ? string.Empty : " AND SECTIONNO = " + ddlSection.SelectedValue), "POINT DESC");
            DataSet dsGrades = objCommon.FillDropDown("ACD_GRADE_POINT", "GRADE_NAME", "MINMARK,MAXMARK,TOTAL_STU", "SESSIONNO = " + ddlSession.SelectedValue +" AND COURSENO="+ddlCourse.SelectedValue , "POINT DESC");
            if (dsGrades != null && dsGrades.Tables.Count > 0)
            {
                if (dsGrades.Tables[0].Rows.Count > 0)
                {
                    lvGrades.DataSource = dsGrades;
                    lvGrades.DataBind();
                }
                else
                {
                    lvGrades.DataSource = null;
                    lvGrades.DataBind();
                }
            }
            else
            {
                lvGrades.DataSource = null;
                lvGrades.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindStudentList();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        char ch = '-';
        string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);

        objExamController.UpdateMarkTot(Convert.ToInt32(ddlSession.SelectedValue), ccode[0].Trim(), Convert.ToInt32(ddlSection.SelectedValue));
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        char ch = '-';
        string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);

        objExamController.UpdateMarkTot(Convert.ToInt32(ddlSession.SelectedValue), ccode[0].Trim(), Convert.ToInt32(ddlSection.SelectedValue));

        this.ShowReport("", "rptMarksRange.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int courseno = 0;
            if (ddlDegree.SelectedValue == "1" && (ddlSemester.SelectedValue == "1" || ddlSemester.SelectedValue == "2"))
                courseno = 0;
            else
                courseno = Convert.ToInt32(ddlCourse.SelectedValue);

            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + courseno + ",@P_CCODE=" + ccode[0].Trim() + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_CNT=10";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updGradeAllotment, this.updGradeAllotment.GetType(), "controlJSScript", sb.ToString(), true);

            //To open new window 
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportAllMarks(string reportTitle, string rptFileName)
    {
        try
        {
            int courseno = 0;
            if (ddlDegree.SelectedValue == "1" && (ddlSemester.SelectedValue == "1" || ddlSemester.SelectedValue == "2"))
                courseno = 0;
            else
                courseno = 1;

            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + courseno + ",@P_CCODE=" + ccode[0].Trim() + ",@P_SECTIONNO=" + ddlSection.SelectedValue;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updGradeAllotment, this.updGradeAllotment.GetType(), "controlJSScript", sb.ToString(), true);

            //To open new window 
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport2_Click(object sender, EventArgs e)
    {
        this.ShowReportAllMarks("", "rptAllMarksRange.rpt");
    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOffset.Text.Trim() == string.Empty)
                txtOffset.Text = "0";

            //Start the Process
            lock (Session.SyncRoot)
            {
                Session["complete"] = false;
                Session["status"] = "";
            }

            Thread t = new Thread(new ParameterizedThreadStart(ThreadProcess));
            t.Start(Session);

            //Switch the View            
            timUpdate.Enabled = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.btnSubmit2_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnGraph_Click(object sender, EventArgs e)
    {
        this.ShowReportGraph("", "rptBellGraph.rpt");
    }

    private void ShowReportGraph(string reportTitle, string rptFileName)
    {
        try
        {
            int courseno = 0;
            if (ddlDegree.SelectedValue == "1" && (ddlSemester.SelectedValue == "1" || ddlSemester.SelectedValue == "2"))
                courseno = 0;
            else
                courseno = 1;

            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + courseno + ",@P_CCODE=" + ccode[0].Trim() + ",@P_SECTIONNO=" + ddlSection.SelectedValue;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updGradeAllotment, this.updGradeAllotment.GetType(), "controlJSScript", sb.ToString(), true);

            //To open new window 
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {

    }

    private void BindAnalysisList()
    {
        try
        {
            int factor = 0;
            if (txtFactor.Text.Trim() != string.Empty)
                factor = Convert.ToInt32(txtFactor.Text);

            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_GRADE_POINT G ON (R.SESSIONNO = G.SESSIONNO AND R.CCODE = G.CCODE) INNER JOIN ACD_SECTION SN ON (SN.SECTIONNO = R.SECTIONNO)", "SN.SECTIONNO,SN.SECTIONNAME", "R.ROLL_NO,CAST(R.S1MARK AS INT) TAE,CAST(R.S2MARK AS INT)CAE,G.MINMARK,G.GRADE_NAME ", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.CCODE = '" + ccode[0].Trim() + "' AND R.MARKTOT = (G.MINMARK - " + factor + ") AND (CANCEL IS NULL OR CANCEL = 0)", "R.SECTIONNO,R.ROLL_NO,R.GDPOINT");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvAnalysis.DataSource = ds;
                    lvAnalysis.DataBind();
                }
                else
                {
                    lvAnalysis.DataSource = null;
                    lvAnalysis.DataBind();
                }
            }
            else
            {
                lvAnalysis.DataSource = null;
                lvAnalysis.DataBind();
            }


            DataSet dsGrades = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_GRADE_POINT G ON (R.SESSIONNO = G.SESSIONNO AND R.CCODE = G.CCODE)", "COUNT(*) [COUNT]", "G.MINMARK ,G.GRADE_NAME", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.CCODE = '" + ccode[0].ToString().Trim() + "' AND R.MARKTOT = (G.MINMARK - " + factor + ") GROUP BY G.MINMARK ,G.GRADE_NAME", string.Empty);
            if (dsGrades != null && dsGrades.Tables.Count > 0)
            {
                if (dsGrades.Tables[0].Rows.Count > 0)
                {
                    lvGrade.DataSource = dsGrades;
                    lvGrade.DataBind();
                }
                else
                {
                    lvGrade.DataSource = null;
                    lvGrade.DataBind();
                }
            }
            else
            {
                lvGrade.DataSource = null;
                lvGrade.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnAnalysis_Click(object sender, EventArgs e)
    {
        this.BindAnalysisList();
    }

    private void ThreadProcess(object data)
    {
        try
        {
            System.Web.SessionState.HttpSessionState s = (System.Web.SessionState.HttpSessionState)data;

            int courseno = 0;
            if (ddlDegree.SelectedValue == "1" && (ddlSemester.SelectedValue == "1" || ddlSemester.SelectedValue == "2"))
                courseno = 0;
            else
                courseno = Convert.ToInt32(ddlCourse.SelectedValue);


            char ch = '-';
            string[] ccode = ddlCourse.SelectedItem.Text.Split(ch);

            CustomStatus ret = (CustomStatus)objExamController.GradeAllotmentNew(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode[0].Trim(), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(txtOffset.Text.Trim()));
            if (ret == CustomStatus.RecordSaved)
            {
                timUpdate.Enabled = false;
                this.BindStudentList();
                this.BindAnalysisList();
                objCommon.DisplayMessage(this.updGradeAllotment, "Grade Alloted Successfully", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updGradeAllotment, "Error...", this.Page);

            lock (s.SyncRoot)
            {
                s["complete"] = true;
                timUpdate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.ThreadProcess --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void timUpdate_Tick1(object sender, EventArgs e)
    {
        if (Session["complete"] as bool? == true)
        {
            timUpdate.Enabled = false;
            objCommon.DisplayMessage(this.updGradeAllotment, "Grade Alloted Successfully", this.Page);
        }
    }
}
