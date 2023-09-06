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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_REPORTS_StudentResultList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DataSet dsShowData = null;
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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();
                ddlSession.Focus();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
        }
    }
    
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResultList.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Fill DropDownList

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }

        //if (ddlBranch.SelectedValue == "99")
        //{
        //    ddlSection.Enabled = true;
        //    //rfvSection.EnableClientScript = false;
        //}
        //else
        //{
        //    ddlSection.Enabled = false;
        //    //rfvSection.EnableClientScript = true;
        //}
    }

    
   #endregion

    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add("Please Select");
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add("Please Select");
        ddlSem.Items.Clear();
        ddlSem.Items.Add("Please Select");
        ddlSection.Items.Clear();
        ddlSection.Items.Add("Please Select");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportBlankTR(GetStudentID(), "CONSOLIDATED_REPORT", "rptRTMBlankTR.rpt");
            //ShowReportBlankTR("CONSOLIDATED_REPORT", "rptInterMediateTrnew.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowReportBlankTRGazzette(string GetStudentID, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            if (chkCopyCase.Checked == true)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_COPYCASE=1" + ",@P_IDNOS=" + GetStudentID;
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_COPYCASE=0" + ",@P_IDNOS=" + GetStudentID;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportBlankTR1Gazzette(string GetStudentID,string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (chkCopyCase.Checked == true)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_COPYCASE=1" + ",@P_IDNOS=" + GetStudentID;
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_COPYCASE=0" + ",@P_IDNOS=" + GetStudentID;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportBlankTR(string GetStudentID, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_IDNOS=" + GetStudentID;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportBlankTR1(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportGradeCard(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_ABSORPTION_STATUS=" + ddlStatus.SelectedValue + ",@P_PREV_STATUS=" + ddlExam.SelectedValue;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlDegree.SelectedIndex = 0;
        //ClearControls();
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnTR_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowReportBlankTR("TABULATION_REGISTER", "rptTR.rpt");
            if(ddlBranch.SelectedValue == "99")
                ShowReportBlankTRGazzette(GetStudentID(), "TABULATION_REGISTER", "rptTRNewFirstYear.rpt");
            else
                ShowReportBlankTR1Gazzette(GetStudentID(), "TABULATION_REGISTER", "rptTRNew.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTR_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnGradeCard_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportGradeCard("Grade_Card", "rptgradecardsemesterwise.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnGradeCard_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnTRDisplay_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedValue == "99")
            {
                ShowReportBlankTRGazzette(GetStudentID(), "RESULT_GAZETTE", "rptTRDisplayNewFirstYear.rpt");
            }
            else
            {
                //ShowReportBlankTR("RESULT_GAZETTE", "rptTRDisplay.rpt");
                ShowReportBlankTR1Gazzette(GetStudentID(), "RESULT_GAZETTE", "rptTRDisplayNew.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTRDisplay_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAnalysis_Click(object sender, EventArgs e)
    {
        try
        {
            if(ddlBranch.SelectedValue == "99")
                ShowReportResultAnalysis("RESULT_ANALYSIS", "rptResultAnalysisFirstYear.rpt");
            else 
                ShowReportResultAnalysis("RESULT_ANALYSIS", "rptResultAnalysis.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnAnalysis_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnIntermediateReport_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowReportBlankTR("CONSOLIDATED_REPORT", "rptRTMBlankTR.rpt");
            ShowReportBlankTR(GetStudentID(), "CONSOLIDATED_REPORT", "rptRTMBlankTRNew.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnFail_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportFailList("Fail_Student_List", "rptFailRollList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnFail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReportResultAnalysis(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
     private void ShowReportFailList(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SECTIONNO="+ddlSection.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
     protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlExam.SelectedIndex > 0)
         {
             ddlSem.Items.Clear();
            // objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_STUDENT_RESULT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO > 0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND PREV_STATUS=" + ddlExam.SelectedValue + " AND PREV_STATUS=" + ddlExam.SelectedValue, "A.SEMESTERNO");
             objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
             ddlSem.Focus();
             ddlSection.Items.Clear();
             ddlSection.Items.Add(new ListItem("Please Select", "0"));
             ddlStatus.SelectedIndex = 0;
         }
         else
         {
             ddlSem.Items.Clear();
             ddlSem.Items.Add(new ListItem("Please Select", "0"));
             ddlSection.Items.Clear();
             ddlSection.Items.Add(new ListItem("Please Select", "0"));
             ddlStatus.SelectedIndex = 0;
             lvStudent.DataSource = null;
             lvStudent.DataBind();
         }
         lvStudent.DataSource = null;
         lvStudent.DataBind();
     }


     protected void btnCuttOff_Click(object sender, EventArgs e)
     {
         try
         {
             //if (ddlBranch.SelectedValue == "99")
             //    ShowReportResultAnalysis("RESULT_ANALYSIS", "rptResultAnalysisFirstYear.rpt");
             //else
             ShowReportResultAnalysis_cutoff("CUTOFF", "rptResultAnalysisFirstYear2.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnCuttOff_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }

     }

     private void ShowReportProvisional_Certificate(string reportTitle, string rptFileName)
     {
         try
         {
             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
             url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
             divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             divMsg.InnerHtml += " </script>";

             //To open new window from Updatepanel
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable.");
         }
     }
        
    private void ShowReportResultAnalysis_cutoff(string reportTitle, string rptFileName)
     {
         try
         {
             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue);
             divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             divMsg.InnerHtml += " </script>";

             //To open new window from Updatepanel
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable.");
         }
     }

    // show provisional certificate
     protected void btnProvisional_Click(object sender, EventArgs e)
     {

         try
         {
             ShowReportProvisional_Certificate("PROVISIONAL_CERTIFICATE", "rptProvisionalMarkSheet.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnProvisional_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }

     }

     protected void btnSpecialGazz_Click(object sender, EventArgs e)
     {
         try
         {
             if (ddlBranch.SelectedValue == "99")
             {
                 ShowReportBlankTRGazzette(GetStudentID(), "SPECIAL_RESULT_GAZETTE", "rptSpecialgazzeteFirstYear.rpt");
             }
             else
             {

                 ShowReportBlankTRGazzette(GetStudentID(), "SPECIAL_RESULT_GAZETTE", "rptTRDisplayNew_Special_Gazette.rpt");
                 //ShowReportBlankTR1Gazzette("SPECIAL_RESULT_GAZETTE", "rptTRDisplayNew.rpt");
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnSpecialGazz_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }
     protected void btnGraph_Click(object sender, EventArgs e)
     {
         try
         {
             ShowReportGraphical("Graphical_Student_Report", "rptResultAnalysisGraph.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnGraph_Click()-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }

     private void ShowReportGraphical(string reportTitle, string rptFileName)
     {
         try
         {
             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
             divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             divMsg.InnerHtml += " </script>";

             //To open new window from Updatepanel
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable.");
         }
     }







     private void ShowReportBlankAdditionalTRGazzette(string GetStudentID, string reportTitle, string rptFileName)
     {
         try
         {
             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
             if (chkCopyCase.Checked == true)
                 url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_COPYCASE=1" + ",@P_IDNOS=" + GetStudentID;
             else
                 url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue) + ",@P_COPYCASE=0" + ",@P_IDNOS=" + GetStudentID;
             divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             divMsg.InnerHtml += " </script>";

             //To open new window from Updatepanel
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable.");
         }
     }



     protected void btnShow_Click(object sender, EventArgs e)
     {
         ShowStudents();
     }

     private void ShowStudents()
     
     {
         try
         {
             StudentController objSC = new StudentController();
             dsShowData = objSC.GetStudentDetailsForConsolidated(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlExam.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
             if (dsShowData.Tables[0].Rows.Count > 0)
             {
                 lvStudent.DataSource = dsShowData;
                 lvStudent.DataBind();
                 //btnIntermediateReport.Enabled = true;
                 //btnTR.Enabled = true;
                 //btnTRDisplay.Enabled = true;
                 //btnGradeCard.Enabled = true;
                 //btnTrReport.Enabled = true;
                 //btnAddTr.Enabled = true;
                 //btnResultGzNew.Enabled = true;
                 //btnNewTR.Enabled = true;
                 //btnNotAppear.Enabled = true;
                 //btnRZWCgpa.Enabled = true;
                 hftot.Value = dsShowData.Tables[0].Rows.Count.ToString();
             }
             else
             {
                 lvStudent.DataSource = null;
                 lvStudent.DataBind();
                 //btnIntermediateReport.Enabled = false;
                 //btnTR.Enabled = false;
                 //btnTRDisplay.Enabled = false;
                 //btnGradeCard.Enabled = false;
                 //btnTrReport.Enabled = false;
                 //btnAddTr.Enabled = false;
                 //btnResultGzNew.Enabled = false;
                 //btnNewTR.Enabled = false;
                 //btnNotAppear.Enabled = false;
                 //btnRZWCgpa.Enabled = false;
                 hftot.Value = "0";
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_StudentResultList.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
             else
                 objCommon.ShowError(Page, "Server UnAvailable");
         }
     }

     private string GetStudentID()
     {
         string studentId = string.Empty;
         try
         {
             foreach (ListViewDataItem item in lvStudent.Items)
             {
                 CheckBox chk = item.FindControl("chkStudent") as CheckBox;

                 if (chk.Checked)
                 {
                     if (studentId.Length > 0)
                         studentId += ".";
                     studentId += chk.ToolTip;
                 }
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_StudentResultList.GetStudentID() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objCommon.ShowError(Page, "Server UnAvailable");
         }
         return studentId;
     }
     protected void btnTrReport_Click(object sender, EventArgs e)
     {
         try
         {
             //ShowReportBlankTR("CONSOLIDATED_REPORT", "rptRTMBlankTR.rpt");
             ShowReportBlankTR(GetStudentID(), "CONSOLIDATED_REPORT", "rptRTMBlankTRNewAddDetails.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTrReport_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }
     protected void btnAddTr_Click(object sender, EventArgs e)
     {
         try
         {
             if (ddlBranch.SelectedValue == "99")
             {
                 ShowReportBlankAdditionalTRGazzette(GetStudentID(), "TABULATION_REGISTER", "rptTRNewFirstYear.rpt");
             }

             else
             {
                 ShowReportBlankAdditionalTRGazzette(GetStudentID(), "TABULATION_REGISTER", "rptTRNewAddInfo.rpt");
             }

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnAddTr_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");

         }
     }
     protected void btnAddFail_Click(object sender, EventArgs e)
     {
         try
         {
             ShowReportFailList("Fail_Student_List", "rptFailRollListNew.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnAddFail_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }


     protected void btnFinalGrade_Click(object sender, EventArgs e)
     {
         try
         {
             if (Convert.ToInt32(ddlDegree.SelectedValue) == 1 && Convert.ToInt32(ddlSem.SelectedValue) == 8)
             {
                 ShowReportFinalGradeCard("Student_Final_Year_Grade_Card", "rptGradeCardFinalSemester.rpt");
             }
             else if (Convert.ToInt32(ddlDegree.SelectedValue) == 2 && Convert.ToInt32(ddlSem.SelectedValue) == 8)
             {
                 ShowReportFinalGradeCard("Student_Final_Year_Grade_Card", "rptGradeCardFinalSemester.rpt");
             }
             else if (Convert.ToInt32(ddlDegree.SelectedValue) == 3 && Convert.ToInt32(ddlSem.SelectedValue) == 4)
             {
                 ShowReportFinalGradeCard("Student_Final_Year_Grade_Card", "rptGradeCardFinalSemester.rpt");
             }
             else
             {
                 objCommon.DisplayMessage(updUpdate, "Please select degree B.E. with Sem-8 or B.E. P.T.D.P. with Sem-8  or M.Tech. with Sem-4 for Final Grade Report.", this.Page);
             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnFinalGrade_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }


     private void ShowReportFinalGradeCard(string reportTitle, string rptFileName)
     {
         try
         {
             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_ABSORPTION_STATUS=" + Convert.ToInt32(ddlStatus.SelectedValue);
             divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             divMsg.InnerHtml += " </script>";

             //To open new window from Updatepanel
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable.");

         }
     }

     protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
     {
         lvStudent.DataSource = null;
         lvStudent.DataBind();
         objCommon.FillDropDownList(ddlSection, "ACD_SECTION S,ACD_STUDENT_RESULT SR", "DISTINCT SR.SECTIONNO", "S.SECTIONNAME", "SR.SECTIONNO = S.SECTIONNO AND S.SECTIONNO > 0 AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.PREV_STATUS=" + ddlExam.SelectedValue, "S.SECTIONNAME");
     }


     protected void btnNewTR_Click(object sender, EventArgs e)
     {
         try
         {
            ShowReportBlankTR(GetStudentID(), "CONSOLIDATE_REPORT", "rptRTMBlankTRNewAddDetails_New.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTrReport_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }

     }


     protected void btnResultGzNew_Click(object sender, EventArgs e)
     {
         try
         {
             ShowReportBlankTR1Gazzette(GetStudentID(), "RESULT_GAZETTE", "rptTRDisplayNew.rpt");

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTrReport_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }
     protected void btnNotAppear_Click(object sender, EventArgs e)
     {
         try
         {
             ShowReportBlankTR(GetStudentID(), "Not_Appear_Student", "rptExamNotAppear.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTrReport_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }
     protected void btnRZWCgpa_Click(object sender, EventArgs e)
     {
         try
         {
             ShowReportBlankTR1Gazzette(GetStudentID(), "RESULT_GAZETTE", "rptTRDisplayNew_WithoutCGPA.rpt");

         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnTrReport_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
     }
     protected void btnPassed_Click(object sender, EventArgs e)
     {
         ShowReportPassed("Student_Passed_Report", "rptPassedStudentList.rpt");
     }
     private void ShowReportPassed(string reportTitle, string rptFileName)
     {
         try
         {
             string schemetype = objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SCHEMENO=" + ddlScheme.SelectedValue);

             if (schemetype == "1")
             {
                 schemetype = "CSVTU";
             }
             else
             {
                 schemetype = "NIT";
             }

             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             url += "&param=@P_COLLEGE_CODE="+Session["colcode"].ToString()+",@P_SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue)+",@P_SCHEMENO="+Convert.ToInt32(ddlScheme.SelectedValue)+",@P_SEMESTER_NO="+Convert.ToInt32(ddlSem.SelectedValue)+",@P_PREV_STATUS="+Convert.ToInt32(ddlStatus.SelectedValue)+",@P_DEGREE="+ddlDegree.SelectedItem.Text.Trim()+",@P_SCHEMETYPE="+schemetype+",@P_BRANCH="+ddlBranch.SelectedItem.Text.Trim();
             divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             divMsg.InnerHtml += " </script>";

             //To open new window from Updatepanel
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable.");

         }
     }

     protected void btnConsolidateReport_Click(object sender, EventArgs e)
     {
         ShowReport("ConsolidatedReport", "rptConsolidatedInternalMarksReport.rpt");
     }

     private void ShowReport(string reportTitle, string rptFileName)
     {
         string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
         url += "Reports/CommonReport.aspx?";
         url += "pagetitle=" + reportTitle;
         url += "&path=~,Reports,Academic," + rptFileName;
         url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_RECORD_STATUS="+Convert.ToInt32(ddlStatus.SelectedValue)+",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

         divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
         divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
         divMsg.InnerHtml += " </script>";
         System.Text.StringBuilder sb = new System.Text.StringBuilder();
         string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
         sb.Append(@"window.open('" + url + "','','" + features + "');");
         ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
     }
     private void ShowReportabsent(string reportTitle, string rptFileName)
     {
         string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
         url += "Reports/CommonReport.aspx?";
         url += "pagetitle=" + reportTitle;
         url += "&path=~,Reports,Academic," + rptFileName;
         url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue);
         System.Text.StringBuilder sb = new System.Text.StringBuilder();
         string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
         sb.Append(@"window.open('" + url + "','','" + features + "');");

         ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);

     }


     protected void btnAbsent_Click(object sender, EventArgs e)
     {
         if (ddlSession.SelectedIndex > 0)
         {
             if (ddlDegree.SelectedIndex > 0)
             {
                 if (ddlBranch.SelectedIndex > 0)
                 {
                     if (ddlScheme.SelectedIndex > 0)
                     {
                         if (ddlSem.SelectedIndex > 0)
                         {
                             ShowReportabsent("Absent List Report", "rptAbsentStudList.rpt");
                         }
                         else
                         {
                             objCommon.DisplayMessage(this.consolidated, "Please Select Semester!!", this.Page);
                         }
                     }
                     else
                     {
                         objCommon.DisplayMessage(this.consolidated, "Please Select Scheme!!", this.Page);
                     }
                 }
                 else
                 {
                     objCommon.DisplayMessage(this.consolidated, "Please Select Branch!!", this.Page);

                 }
             }
             else
             {
                 objCommon.DisplayMessage(this.consolidated, "Please Select Degree!!", this.Page);

             }
         }
         else
         {
             objCommon.DisplayMessage(this.consolidated, "Please Select Session!!", this.Page);
         }
     }
}

