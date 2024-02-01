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

public partial class ACADEMIC_REPORTS_InternalAssesmentReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }


                ddlClgname.Focus();

                string deptno = string.Empty;
                if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                    deptno = "0";
                else
                    deptno = Session["userdeptno"].ToString();
                // ENDS HERE BY NARESH BEERLA ON DT 28042022 AS PER THE ISSUE RELATED TO UA_DEPTNO

                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
                //AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
                else

                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaperSetFacultyAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaperSetFacultyAllotment.aspx");
        }
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0  AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                    ddlSession.Focus();
                }
            }
            else
            {
                ddlClgname.SelectedIndex = 0;
                ddlSession.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.ddlClgname_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), " S.SEMESTERNO");
                ddlSemester.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.ddlSession_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["Deptno"]) == 6)
            {
                //string SP_Name = "PKG_INTERNAL_ASSESSMENT_FORMATS_B_M_TECH_FOR_RCPIT_AND_RCPIPER";
                //string SP_Parameters = "@P_SCHEMENO,@P_SESSIONNO,@P_COURSENO";
                //string Call_Values = "" + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";

                //DataSet ds = null;
                //ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                    ShowReportPharm("Internal Marks Assessment Report", "rptIntAsmentReportBTECH_RCPIPER.rpt");
                //}
                //else
                //{
                //    objCommon.DisplayMessage(updpaper, "No Data Found for current selection.", this.Page);
                //}
            }
            else if (Convert.ToInt32(ViewState["Deptno"]) == 8)
            {
                ShowReportPharm("Internal Marks Assessment Report", "rptIntAsmentReportMBA_RCPIPER.rpt");
            }
            else
            {
                //string SP_Name = "PKG_INTERNAL_ASSESSMENT_FORMATS_FOR_RCPIT_AND_RCPIPER";
                //string SP_Parameters = "@P_SCHEMENO,@P_SESSIONNO,@P_COURSENO";
                //string Call_Values = "" + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";

                //DataSet ds = null;
                //ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                    ShowReportPharm("Internal Marks Assessment Report", "rptItnalAsmentReport_RCPIPER.rpt");
                //}
                //else
                //{
                //    objCommon.DisplayMessage(updpaper, "No Data Found for current selection.", this.Page);
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }

    private void ShowReportPharm(string reportTitle, string rptFileName)
    {
        try
        {
            //int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "FLOCK = 1 AND IS_ACTIVE = 1 AND SESSIONID= " + ddlSession.SelectedValue));
            int clg_id = Convert.ToInt32(ViewState["college_id"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            }
            else
            {
                url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            }
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpaper, updpaper.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //ddlClgname.Items.Clear();
            ddlClgname.SelectedIndex = 0;
            ddlSession.Items.Clear();
            ddlCourse.Items.Clear();
            ddlSemester.Items.Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex > 0)
            {
                string Dept = objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]));
                ViewState["Deptno"] = Dept;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.ddlCourse_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_SCHEME S  WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT ES ON (ES.COURSENO = C.COURSENO AND ES.SESSIONNO = R.SESSIONNO) INNER JOIN ACD_STUDENT_TEST_MARK TM ON (TM.COURSENO = C.COURSENO AND TM.SESSIONNO = R.SESSIONNO) INNER JOIN ACD_SUBEXAM_NAME SN WITH (NOLOCK) ON (S.PATTERNNO   = SN.PATTERNNO AND R.SUBID = SN.SUBEXAM_SUBID AND ES.SUBEXAMNO = SN.SUBEXAMNO)", "DISTINCT CAST(C.COURSENO AS VARCHAR) As COURSENO", "C.CCODE+'-'+COURSE_NAME As COURSENAME", "R.SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND  SN.FLDNAME NOT LIKE '%EXTER%' AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND R.SCHEMENO=" + ViewState["schemeno"] + " AND  R.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(ES.CANCLE,0)=0", "COURSENO");
                ddlCourse.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.ddlSemester_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportMBAPharm(string reportTitle, string rptFileName)
    {
        try
        {
            //int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "FLOCK = 1 AND IS_ACTIVE = 1 AND SESSIONID= " + ddlSession.SelectedValue));
            int clg_id = Convert.ToInt32(ViewState["college_id"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            }
            else
            {
                url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            }
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpaper, updpaper.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_InternalAssesmentReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}