//=================================================
// CREATED : Shubham Barke
// Date : 09-06-2023
//=================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_EXAMINATION_FacultyDairyReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
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
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {

                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }

                else
                {
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID) INNER JOIN ACD_COURSE_TEACHER CT ON(CT.SESSIONNO = S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "S.SESSIONNO > 0 AND ( UA_NO=" + Session["userno"].ToString() + " OR  ADTEACHER=" + Session["userno"].ToString() + ") AND ISNULL(S.IS_ACTIVE,0)=1", "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlSessionAcad, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID) INNER JOIN ACD_COURSE_TEACHER CT ON(CT.SESSIONNO = S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "S.SESSIONNO > 0 AND ( UA_NO=" + Session["userno"].ToString() + " OR  ADTEACHER=" + Session["userno"].ToString() + ") AND ISNULL(S.IS_ACTIVE,0)=1", "SESSIONNO DESC");
                }
            }
            divMsg.InnerHtml = string.Empty;
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            if (Convert.ToInt32(ddlSession.SelectedIndex) > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", "DISTINCT R.SUBID", "SUBNAME", "S.SUBID > 0 AND (UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " OR UA_NO_PRAC=" + Convert.ToInt32(Session["userno"].ToString()) + ") AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "");
            }
            else
            {
                ddlSubjectType.SelectedIndex = 0;
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        { 
            if(ddlSubjectType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, " ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON (R.CCODE = C.CCODE ) ", "DISTINCT R.COURSENO", "C.COURSE_NAME", " SESSIONNO = " + Convert.ToInt16(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0) = 0 AND C.SUBID = " + Convert.ToInt16(ddlSubjectType.SelectedValue) + " AND ((UA_NO = " + Convert.ToInt16(Session["userno"]) + ") OR (UA_NO_PRAC = " + Convert.ToInt16(Session["userno"]) + "))", "R.COURSENO");
            }else
            {
                ddlCourse.SelectedIndex = 0;
            }
        }catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.ddlSubjectType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnIClear_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
    }
    protected void btnInternalExamrpt_Click(object sender, EventArgs e)
    {
        try 
        {
            string SP_Name = "PKG_RESULT_ANALYSIS_REPORTS_RCPIT";
            string SP_Parameters = "@P_USERNO,@P_COURSENO,@P_SESSIONNO,@P_SUBID";
            string Call_Values = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ShowReport("InternalResultAnalysis", "rptStudentWiseAssessment_RCPIT.rpt");
            }
            else 
            {
                 objCommon.DisplayMessage(updatePanel2, "No Data Found for current selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.btnInternalExamrpt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnFinalExamrpt_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name = "PKG_RESULT_ANALYSIS_REPORTS_EXTERMARKS_RCPIT";
            string SP_Parameters = "@P_USERNO,@P_COURSENO,@P_SESSIONNO,@P_SUBID";
            string Call_Values = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ShowReport("ExternalResultAnalysis", "rptExternalResultAnalysis_RCPIT.rpt");
            }
            else
            {
                objCommon.DisplayMessage(updatePanel2, "No Data Found for current selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.btnFinalExamrpt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int clg_id = Convert.ToInt32(ViewState["college_id"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_USERNO=" + Convert.ToInt32(Session["userno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue);
            
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportACADEMIC(string reportTitle, string rptFileName)
    {
        try
        {
            int clg_id = Convert.ToInt32(ViewState["college_id"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSessionAcad.SelectedValue);

            string Script = string.Empty;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.ShowReportACADEMIC() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnAttendanceReport_Click(object sender, EventArgs e)
    {
        ShowReportACADEMIC("FacultyteachingPlanReport", "rptPlanned_Executed_Teachingplan_Faculty_Diary.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnAttReport_Click(object sender, EventArgs e)
    {
        ShowReportACADEMIC_Attendance("FacultyAttendanceReport", "rptCoursewiseAttendanceofStudent.rpt");
    }

    private void ShowReportACADEMIC_Attendance(string reportTitle, string rptFileName)
    {
        try
        {
            int clg_id = Convert.ToInt32(ViewState["college_id"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSessionAcad.SelectedValue);

            string Script = string.Empty;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.ShowReportACADEMIC() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnExamReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name = "PKG_EXAM_STUDENT_WISE_ASSESSMENT_REPORT";
            string SP_Parameters = "@P_USERNO,@P_COURSENO,@P_SESSIONNO,@P_SUBID";
            string Call_Values = "" + Convert.ToInt32(Session["userno"]) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ShowReport("Exams_Reports", "rptStudentWiseAssessment_RCPIT.rpt");
            }
            else
            {
                objCommon.DisplayMessage(updatePanel2, "No Data Found for current selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FacultyDairyReport.btnInternalExamrpt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}