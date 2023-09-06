//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : StudentFeedBackReport.aspx                                               
// CREATION DATE : 04-06-2012                                                   
// CREATED BY    : Pawan Mourya                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Web.UI.WebControls;

public partial class ACADEMIC_StudentFeedBackReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
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
                this.CheckPageAuthorization();

                //fill dropdown
                FillDropDownList();
                //to clear all controls
                AllClear();
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }

    //function to fill all dropdown
    private void FillDropDownList()
    {
        if (Session["usertype"].ToString() != "1")
            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
        else

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0", "SECTIONNO");
    }


    //function to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackReport.aspx");
        }
    }

    //load branches according to selecetd degree 
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                ddlBranch.SelectedValue = "0";
                //ddlScheme.SelectedValue = "0";
                ddlSemester.SelectedValue = "0";
                if (Session["usertype"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B on (A.BRANCHNO = B.BRANCHNO)", " distinct B.BRANCHNO", "LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B on (A.BRANCHNO = B.BRANCHNO)", " distinct B.BRANCHNO", "LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + Convert.ToInt32(Session["userdeptno"]), "B.BRANCHNO");
                }
                ddlBranch.Focus();
            }
            else
            {
                ddlBranch.SelectedValue = "0";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackReport.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            ddlScheme.Focus();
            ddlSemester.Items.Clear();
            // ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Focus();



        }
    }


    //load exam names according to scheme
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //to get schemeno
            string schemeno = objCommon.LookUp("ACD_STUDENT", "DISTINCT SCHEMENO", " DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + "  AND  BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + "AND SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + "AND SECTIONNO = " + Convert.ToInt32(ddlSection.SelectedValue));

            //Fill DropDownList
            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "LEFT(EXAMNAME,5) EXAMNAME", "PATTERNNO=(SELECT PATTERNNO FROM ACD_SCHEME WHERE SCHEMENO= " + schemeno.ToString() + ") AND FLDNAME='S1'", "EXAMNO");

        }
        catch { }
    }


    //function to show report
    private void ShowReport(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        ////To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeed, this.updFeed.GetType(), "controlJSScript", sb.ToString(), true);


    }

    //feedback report
    protected void btnFeedbackReport_Click(object sender, EventArgs e)
    {
        string param = string.Empty;
        if (rdoReport.SelectedValue == "1")
        {
            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            ShowReport("CourseFeedbackReport", "rptFeedbackConsolidatedCourseAvg.rpt", param);
        }
        else if (rdoReport.SelectedValue == "2")
        {
            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            ShowReport("TeacherFeedbackReport", "rptFeedbackTeacherSummery.rpt", param);
        }
        else
        {
            param = "@P_SESSION_NO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            ShowReport("UniversityFeedbackReport", "rptFeedbackSummaryUniversity.rpt", param);
        }
    }

    //to clear all controls
    private void AllClear()
    {
        ddlSession.SelectedValue = "0";
        ddlDegree.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";
        ddlScheme.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        ddlSection.SelectedValue = "0";
        ddlExam.SelectedValue = "0";
        txtFromDate.Text = "";
        txtTodate.Text = "";
    }

    //to cancel report
    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        AllClear();
    }

    //radiobutton functionality
    protected void rdoReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoReport.SelectedValue == "1")
        {
            AllClear();
            Semester.Visible = true;
        }
        else if (rdoReport.SelectedValue == "2")
        {
            AllClear();
            Semester.Visible = true;
        }
        else
        {
            AllClear();
            Semester.Visible = false;
        }
    }


    protected void btnFeedback_Question_Click(object sender, EventArgs e)
    {
        string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SCHEMENO=0" + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=0" + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=0" + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
        ShowReport("Student_FeedBack_Count", "StudentFeedbackCount.rpt", param);
    }


    protected void btnNotfillcourse_Click(object sender, EventArgs e)
    {
        string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);

        ShowReport("Pending Student List", "rptStudentPendingFeedbackCourse.rpt", param);
    }




    //to get faculty feedback report
    protected void btnFacultyFeedbackReport_Click(object sender, EventArgs e)
    {
        string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_EXAMNO=" + Convert.ToInt32(ddlExam.SelectedValue);

        string schemetype = objCommon.LookUp("ACD_SCHEMETYPE", "SCHEMETYPE", "SCHEMETYPENO = (Select SchemeType from acd_Scheme where Schemeno = " + Convert.ToInt32(ViewState["schemeno"]) + ")");
        if (schemetype == "CBCS")
        {
            ShowReport("Student_FeedBack_Count", "StudentFacultyFeedbackCount.rpt", param);
        }
        else
        {
            ShowReport("Student_FeedBack_Count", "StudentFacultyFeedbackCountNonCBCS.rpt", param);
        }
    }



    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlClgname.Focus();
        }
    }



    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON (S.SEMESTERNO=R.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND ISNULL(PREV_STATUS,0)=0 and R.SESSIONNO=" + ddlSession.SelectedValue, "R.SEMESTERNO");
        }
        else
        { 
        
        }
    }
}
