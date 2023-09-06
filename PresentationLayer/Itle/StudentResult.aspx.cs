using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Itle_StudentResult : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestResultController TRC = new ITestResultController();

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                FillDropdown();
            }
        }
    }


    #region "General"
    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT, ACD_COURSE C", "CT.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) AS COURSENAME", "CT.COURSENO = C.COURSENO  AND UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND CT.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "CT.COURSENO");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT,ACD_COURSE C,ACD_SCHEME S,ACD_DEPARTMENT AD", "DISTINCT CT.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME + ' - '+ ISNULL(AD.DEPTNAME,''))  AS COURSENAME", "CT.COURSENO = C.COURSENO AND S.SCHEMENO=CT.SCHEMENO AND AD.DEPTNO =S.DEPTNO  AND (UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR ADTEACHER=" + Convert.ToInt32(Session["userno"]) + ") AND CT.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "CT.COURSENO");


        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);

        }
    }

    #endregion


    #region Action

    private void BindListView()
    {
        DataSet ds;
        try
        {

            TestResult objResult = new TestResult();
            objResult.SESSIONNO = Convert.ToInt32(Session["SessionNo"]);
            objResult.COURSENO = Convert.ToInt32(ddlCourse.SelectedValue);
            objResult.TESTNO = Convert.ToInt32(ddlTest.SelectedValue);
            int StudId = Convert.ToInt32(Session["ICourseno"]);
            if (chkAbsentStudent.Checked != true)
            {
                ds = TRC.GetTestResult(objResult, ddlOrderBy.SelectedValue);
            }
            else
            {
                ds = TRC.GetAbsentStudent(objResult, ddlOrderBy.SelectedValue);

            }

            lvRewsult.DataSource = ds;
            lvRewsult.DataBind();
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_Stud_View_Result.aspx.BindListView->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentResult.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentResult.aspx");
        }
    }


    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtTestType.SelectedValue == "O")
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER TM JOIN ACD_ITEST_RESULT TR ON (TM.TESTNO=TR.TESTNO)", "DISTINCT TM.TESTNO", "TM.TESTNAME", "TR.COURSENO=" + ddlCourse.SelectedValue + "AND TEST_TYPE='O'", "TESTNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER TM JOIN ACD_ITEST_RESULT TR ON (TM.TESTNO=TR.TESTNO)", "DISTINCT TM.TESTNO", "TM.TESTNAME", "TR.COURSENO=" + ddlCourse.SelectedValue + "AND TEST_TYPE='D'", "TESTNO");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();

    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlCourse.SelectedValue.ToString() == "0" || ddlTest.SelectedValue.ToString() == "0")
            {

                objCommon.DisplayUserMessage(updTestReport, "Please Select Course and Test Name", this.Page);
                return;

            }
            if (chkAbsentStudent.Checked != true)
            {
                ShowReport("Itle_Student_Result", "Itle_TestResult_Report.rpt");
            }
            else
            {
                ShowAbsentReport("Itle_Absent_Student_Report", "Itle_AbsentStudent_Report.rpt");
            }


        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "StudentResult.btnShowReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }


    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Session["SessionNo"] + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_TESTNO=" + ddlTest.SelectedValue + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue;
            //url += "&param=username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + "," + Session["SessionNo"] + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_TESTNO=" + ddlTest.SelectedValue + ",TESTNAME=" + ddlTest.SelectedItem.ToString() + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue + ""; //",COURSENAME=" + ddlCourse.SelectedItem.ToString() +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "StudentResult.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void ShowAbsentReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Session["SessionNo"] + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_TESTNO=" + ddlTest.SelectedValue + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue;
            //url += "&param=username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_SESSIONNO=" + Session["SessionNo"] + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_TESTNO=" + ddlTest.SelectedValue + ",TESTNAME=" + ddlTest.SelectedItem.ToString() + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue + ""; //",COURSENAME=" + ddlCourse.SelectedItem.ToString() +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "StudentResult.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentResult.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rbtTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtTestType.SelectedValue == "O")
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER TM JOIN ACD_ITEST_RESULT TR ON (TM.TESTNO=TR.TESTNO)", "DISTINCT TM.TESTNO", "TM.TESTNAME", "TR.COURSENO=" + ddlCourse.SelectedValue + "AND TEST_TYPE='O'", "TESTNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER TM JOIN ACD_ITEST_RESULT TR ON (TM.TESTNO=TR.TESTNO)", "DISTINCT TM.TESTNO", "TM.TESTNAME", "TR.COURSENO=" + ddlCourse.SelectedValue + "AND TEST_TYPE='D'", "TESTNO");

            }


        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ITLE_Ans_Sheet.rbtTestType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);

        }
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ITLE_Ans_Sheet.rbtTestType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);

        }
    }

    protected void chkAbsentStudent_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ITLE_Ans_Sheet.rbtTestType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);

        }
    }
}