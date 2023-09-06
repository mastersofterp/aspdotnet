//=================================================================================
// PROJECT NAME  : NITGOA (RF-CAMPUS)                                                          
// MODULE NAME   : EXAMINATION                                     
// CREATION DATE : 25-MAR-2014
// CREATED BY    : UMESH GANORKAR
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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
using System.Drawing;
using System.IO;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_REPORTS_BacklogExamRegReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //TO SET THE MASTERPAGE
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CHECK SESSION
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                this.CheckPageAuthorization();
                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();
                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    this.FillDropdownList();


                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header

                if ((Convert.ToInt32(Session["OrgId"])) == 6)
                {
                    ReportType.Visible = false;
                    //btnBackLogExamRegFeesPaid.Visible = true;
                    btnoverallbacklog.Visible = false;
                    btnBacklogSubjects.Visible = false;
                    btnsubjectArrearCount.Visible = false;
                    btnArrearReport.Visible = false;
                }
                //else
                //{
                //    ReportType.Visible = true;
                //    //btnExcel.Visible = false;
                //    //btnSubjectWiseMarkEntry.Visible = false;
                //}

                ddlClgname.Focus();
            }
        }
    }
    #endregion
    #region User Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BacklogExamRegReport.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=BacklogExamRegReport.aspx");
        }
    }
    private void FillDropdownList()
    {


        string deptno = string.Empty;
        if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
            deptno = "0";
        else
            deptno = Session["userdeptno"].ToString();
        //objCommon.FillDropDownList(ddlClgname, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "DISTINCT R.SESSIONNO", "SESSION_NAME +' - '+ C.COLLEGE_NAME SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 ", "S.SEMESTERNO");
        ddlSession.Focus();
    }
    #endregion
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            if (ddlDegree.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "BRANCHNO");
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0", "B.LONGNAME");
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BacklogExamRegReport.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            if (ddlBranch.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedValue == "99")
                //    objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", " SCHEMETYPE = 1 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "schemename");
                //else
                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                //ddlScheme.Focus();

                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "branchno = " + ddlBranch.SelectedValue, "SCHEMENO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Focus();
        }
        else
        {
            ddlSession.Focus();

            ddlSemester.SelectedIndex = 0;

            //ddlSemester.Items.Clear();
            //ddlSemester.Items.Add("Please Select");
            //ddlSemester.SelectedItem.Value = "0";

            ddlCourses.Items.Clear();
            ddlCourses.Items.Add("Please Select");
            ddlCourses.SelectedItem.Value = "0";
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

        ddlClgname.Focus();
    }
    private void ShowBacklogStudentStatus(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + "";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlCourses.SelectedValue + "";
            ////divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            ////divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ////divMsg.InnerHtml += " </script>";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowBacklogRegList(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;



            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlCourses.SelectedValue + "";


            //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlCourses.SelectedValue + "";

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //if (ddlCourses.SelectedIndex == 0)
        //{
        ShowBacklogRegList("Student_Backlog_Reg_List", "rptBacklogRegList.rpt");
        //}
        //if (ddlCourses.SelectedIndex != 0)
        //{
        //    ShowBacklogRegList("Student_Backlog_Reg_List", "rptBacklogRegListWithCourse.rpt");
        //}
    }

    private void ShowBacklogRegList(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int semester = Convert.ToInt32(ddlSemester.SelectedValue);
            int course = Convert.ToInt32(ddlCourses.SelectedValue);
            string SP_Name = "PKG_ACAD_GET_STUDENT_BACKLOGS";
            string SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO";
            string Call_Values = "" + Sessionno + "," + semester + "," + course + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                if (reportno == 1)
                {
                    if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by gaurav 19-10-2022
                    {

                        int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "C.COLLEGE_ID", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND M.SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue)));
                        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + clg_id + "";   //                 return;

                    }

                    else
                    {

                        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                    }
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            ddlCourses.Items.Clear();
            ddlCourses.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlCourses, "ACD_STUDENT_RESULT H INNER JOIN ACD_STUDENT S ON (S.IDNO = H.IDNO)", "DISTINCT H.COURSENO", "H.CCODE +'-'+ H.COURSENAME AS COURSENAME", "H.SEMESTERNO =" + ddlSemester.SelectedValue + " AND H.SESSIONNO =" + ddlSession.SelectedValue + " AND H.PREV_STATUS = 1", "H.COURSENO");

            ddlCourses.Focus();
        }
        else
        {
            //ddlCourses.Items.Clear();
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Focus();

            ddlCourses.Items.Clear();
            ddlCourses.Items.Add("Please Select");
            ddlCourses.SelectedItem.Value = "0";
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            if (ddlScheme.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedValue == "99")
                //    objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", " SCHEMETYPE = 1 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "schemename");
                //else
                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                //ddlScheme.Focus();
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + "AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0", "S.SEMESTERNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnBacklogStuStatus_Click(object sender, EventArgs e)
    ////{
    ////    if (ddlCourses.SelectedIndex == 0)
    ////    {
    ////        ShowBacklogStudentStatus("Student_Backlog_Status_Report", "rptBacklogStatusReport.rpt");
    ////    }
    ////}
    protected void btnBacklogStuStatus_Click(object sender, EventArgs e)
    {
        //if (ddlCourses.SelectedIndex > 0)
        //{
        ShowBacklogStudentStatus("Student_Backlog_Status_Report", "rptBacklogStatusReport.rpt");
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updpnlExam, "Please Select Course", Page);
        //}
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {

            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            }

            ddlSession.SelectedIndex = 0;
            ddlSession.Focus();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
        }
        else
        {
            ddlClgname.SelectedIndex = 0;
            ddlClgname.Focus();

            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";

            ddlSemester.SelectedIndex = 0;

            //ddlSemester.Items.Clear();
            //ddlSemester.Items.Add("Please Select");
            //ddlSemester.SelectedItem.Value = "0";

            ddlCourses.Items.Clear();
            ddlCourses.Items.Add("Please Select");
            ddlCourses.SelectedItem.Value = "0";
        }
    }
    protected void btnBacklogSubjects_Click(object sender, EventArgs e)
    {
        //if (ddlClgname.SelectedIndex == 0)
        //{

        //    objCommon.DisplayMessage("Please Select College & Session.", this.Page);
        //    return;
        //}
        if (rdopdf.Checked)
        {
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                ShowBacklogRegList("StudentBacklogList", "rptStudentBacklogs_Atlas.rpt", 1);
            }
            else
            {
                ShowBacklogRegList("StudentBacklogList", "rptStudentBacklogs.rpt", 1);
            }
        }
        else
        {
            GridView GVStudData = new GridView();


            DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACAD_GET_STUDENT_BACKLOGS", "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO	", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "," + ddlCourses.SelectedValue + "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStudData.DataSource = ds;
                GVStudData.DataBind();

                string attachment = "attachment;filename=StudentBacklogListReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage("No Data Found", this.Page);
            }
        }
    }
    protected void btnsubjectArrearCount_Click(object sender, EventArgs e)
    {

        if (rdoexcel.Checked)
        {
            GridView GVStudData = new GridView();


            DataSet ds = objCommon.DynamicSPCall_Select("PKG_EXAM_BACKLOG_REGISTARTION_COURSEWISE_COUNT_REPORT", "@P_SESSIONNO,@P_SEMESTERNO", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStudData.DataSource = ds;
                GVStudData.DataBind();

                string attachment = "attachment;filename=SubjectArrearListReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage("No Data Found", this.Page);
            }

        }
        else
        {


            if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by gaurav 19-10-2022
            {
                ShowReport("rptArrearListReport_Count_Atlas.rpt", "Subject_wise_arrear_Count");
                // int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "C.COLLEGE_ID", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND M.SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue)));
            }
            else
            {
                ShowReport("rptArrearListReport_Count.rpt", "Subject_wise_arrear_Count");
            }
        }
    }
    private void ShowReport(string rptFileName, string reportTitle)
    {
        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int semester = Convert.ToInt32(ddlSemester.SelectedValue);
            int scheme = Convert.ToInt32(ViewState["schemeno"]);
            string SP_Name = "PKG_EXAM_BACKLOG_REGISTARTION_COURSEWISE_COUNT_REPORT";
            string SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO";
            string Call_Values = "" + Sessionno + "," + semester + "," + scheme + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + "Arrear List Report";
                url += "&path=~,Reports,Academic," + rptFileName;

                if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by gaurav 19-10-2022
                {
                    int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "C.COLLEGE_ID", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND M.SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue)));
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + clg_id;
                }
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
                }
                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnArrearReport_Click(object sender, EventArgs e)
    {
        if (rdopdf.Checked)
        {
            try
            {
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int semester = Convert.ToInt32(ddlSemester.SelectedValue);
                int course = Convert.ToInt32(ddlCourses.SelectedValue);

                string SP_Name = "PKG_EXAM_BACKLOG_REGISTARTION_COUNT_REPORT";
                string SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO";
                string Call_Values = "" + Sessionno + "," + semester + "," + course + "";

                DataSet ds = null;
                ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + "Arrear List Report";
                    if (Convert.ToInt32(Session["OrgId"]) == 9)// added by gaurav 19_10_2022
                    {
                        int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "C.COLLEGE_ID", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND M.SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue)));

                        url += "&path=~,Reports,Academic," + "rptArrearListReport_Atlas.rpt";
                        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + clg_id;
                    }
                    else
                    {
                        url += "&path=~,Reports,Academic," + "rptArrearListReport.rpt";

                        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                    }


                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
                }
                else
                {
                    objCommon.DisplayMessage(updpnlExam, "No  Data Found for current selection.", this.Page);
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else
        {
            GridView GVStudData = new GridView();


            DataSet ds = objCommon.DynamicSPCall_Select("PKG_EXAM_BACKLOG_REGISTARTION_COUNT_REPORT", "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO	", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "," + ddlCourses.SelectedValue + "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStudData.DataSource = ds;
                GVStudData.DataBind();

                string attachment = "attachment;filename=ExamRegistrationListReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage("No Data Found", this.Page);
            }

        }
    }
    protected void btnBackLogExamRegFeesPaid_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            if ((Convert.ToInt32(Session["OrgId"])) == 6)
            {
                GridView GVStudData = new GridView();

                DataSet ds = objCommon.DynamicSPCall_Select("PKG_EXAM_REGISTRARTION_STATUS_REPORT_RCPIPER", "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_SCHEMENO", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "," + ddlCourses.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVStudData.DataSource = ds;
                    GVStudData.DataBind();

                    string attachment = "attachment;filename=ExamRegistrationListReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.Charset = "";
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVStudData.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
                else
                {
                    objCommon.DisplayMessage("No Data Found", this.Page);
                }
            }
            else
            {
                if (rdopdf.Checked)
                {
                    try
                    {
                        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
                        int course = Convert.ToInt32(ddlCourses.SelectedValue);

                        string SP_Name = "PKG_EXAM_REGISTRARTION_STATUS_REPORT";
                        string SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO";
                        string Call_Values = "" + Sessionno + "," + semester + "," + course + "";

                        DataSet ds = null;
                        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                            url += "Reports/CommonReport.aspx?";
                            url += "pagetitle=" + "Exam Registration List Report";
                            if (Convert.ToInt32(Session["OrgId"]) == 9)// added by gaurav 19_10_2022
                            {
                                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "C.COLLEGE_ID", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND M.SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue)));
                                url += "&path=~,Reports,Academic," + "rptExamregistrationStatusReport_Atlas.rpt";
                                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + clg_id;
                            }
                            else
                            {
                                url += "&path=~,Reports,Academic," + "rptExamregistrationStatusReport.rpt";
                                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                            }
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                            sb.Append(@"window.open('" + url + "','','" + features + "');");
                            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExam, "No BackLog Data Found for current selection.", this.Page);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Convert.ToBoolean(Session["error"]) == true)
                            objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                        else
                            objUCommon.ShowError(Page, "Server Unavailable.");
                    }

                }
                else
                {

                    GridView GVStudData = new GridView();


                    DataSet ds = objCommon.DynamicSPCall_Select("PKG_EXAM_REGISTRARTION_STATUS_REPORT", "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO	", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "," + ddlCourses.SelectedValue + "");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GVStudData.DataSource = ds;
                        GVStudData.DataBind();

                        string attachment = "attachment;filename=ExamRegistrationListReport.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.Charset = "";
                        Response.ContentType = "application/ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        GVStudData.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();

                    }
                    else
                    {
                        objCommon.DisplayMessage("No Data Found", this.Page);
                    }

                }
            }

        }

    }
    protected void btnResitReport_Click(object sender, EventArgs e)
    {


        if (ddlClgname.SelectedIndex > 0)
        {
            // if (rdopdf.Checked)
            // {
            // try
            // {
            //     string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //     url += "Reports/CommonReport.aspx?";
            //     url += "pagetitle=" + "Exam Registration List Report";
            //     if (Convert.ToInt32(Session["OrgId"]) == 9)// added by gaurav 19_10_2022
            //     {
            //         int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "C.COLLEGE_ID", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND M.SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue)));
            //         url += "&path=~,Reports,Academic," + "rptExamregistrationStatusReport_Atlas.rpt";
            //         url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ",@P_COLLEGE_CODE=" + clg_id;
            //     }
            //     else
            //     {
            //         url += "&path=~,Reports,Academic," + "rptExamregistrationStatusReport.rpt";
            //         url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlClgname.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + "";
            //     }
            //     System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //     string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //     sb.Append(@"window.open('" + url + "','','" + features + "');");
            //     ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            // }
            // catch (Exception ex)
            // {
            //     if (Convert.ToBoolean(Session["error"]) == true)
            //         objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //     else
            //         objUCommon.ShowError(Page, "Server Unavailable.");
            // }

            // }
            // else
            // {

            GridView GVStudData = new GridView();


            DataSet ds = objCommon.DynamicSPCall_Select("PKG_EXAM_REGISTRARTION_STATUS_FOR_RESIT_REPORT", "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO	", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "," + ddlCourses.SelectedValue + "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStudData.DataSource = ds;
                GVStudData.DataBind();

                string attachment = "attachment;filename=ExamRegistrationListReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage("No Data Found", this.Page);
            }

            //}

        }

    }

    protected void rdoexcel_CheckedChanged(object sender, EventArgs e)
    {
        btnResitReport.Visible = true;

    }
    protected void rdopdf_CheckedChanged(object sender, EventArgs e)
    {
        btnResitReport.Visible = false;
    }
    protected void btnoverallbacklog_Click(object sender, EventArgs e)
    {
        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);

            string SP_Name = "PKG_EXAM_OVERALL_BACKLOG_REPORT";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO";
            string Call_Values = "" + Sessionno + "," + SCHEMENO + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + "Arrear List Report";
                url += "&path=~,Reports,Academic," + "rpt_Current_Backlog_StudentList.rpt";
                url += "&param=@P_SESSIONNO=" + Sessionno + ",@P_SCHEMENO=" + SCHEMENO + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No BackLog Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
}
