﻿//=================================================================================
// PROJECT NAME  : RFCC                                                          
// MODULE NAME   : ACADEMIC - MARK ENTRY REPORT                                          
// CREATION DATE : 29/11/2023                                               
// CREATED BY    : SHUBHAM BARKE                                                 
// MODIFIED BY   :                                                     
// MODIFIED DESC : 
//=================================================================================


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

public partial class ACADEMIC_EXAMINATION_MarksEntryRpt : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

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
        if (Session["userno"] == null || Session["username"] == null ||
                 Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                this.PopulateDropDown();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("1"))
            {

                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
            }
            else
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_EndSemExamMarkEntry.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
        }
    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcollege.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    if (ddlcollege.SelectedIndex > 0)
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                        //ddlDegree.Focus();
                        ddlSession.Focus();
                    }
                    else
                    {
                        ddlcollege.Items.Clear();
                        ddlcollege.Items.Add(new ListItem("Please Select", "0"));
                        objCommon.DisplayMessage("Please select College/Scheme Name.", this.Page);
                    }

                }
            }
            else
            {
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryRpt.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue, "S.SEMESTERNO");
                ddlSession.Focus();
            }
            else
            {
                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryRpt.ddlSession_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlsemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");
                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryRpt.ddlsemester_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString().Equals("1"))
                {

                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + Convert.ToString(ViewState["schemeno"]) + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "", "COURSE_NAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + Convert.ToString(ViewState["schemeno"]) + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (SR.UA_NO= " + Convert.ToString(Session["userno"]) + " OR SR.UA_NO_PRAC=" + Convert.ToString(Session["userno"]) + ")", "COURSE_NAME");
                }

                ddlCourse.Focus();
            }
            else
            {

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryRpt.ddlSubjectType_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlCourse.SelectedIndex > 0)
    //        {
    //            DataSet ds = objMarksEntry.GetLevelMarksEntryCourseDetail(Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                Session["Pattern"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PATTERNNO"]);
    //            }

    //            DataSet dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME", " CAST(EXAMNO AS NVARCHAR)+'-'+ FLDNAME AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND EXAMTYPE=2 AND FLDNAME IN('EXTERMARK')", "EXAMNO");
    //            MainSubExamBind(ddlExam, dsMainExam);
    //            ddlExam.Focus();
    //        }
    //        else
    //        {
    //            ddlExam.Items.Clear();
    //            ddlExam.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "MarksEntryRpt.ddlCourse_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //private void MainSubExamBind(DropDownList ddlList, DataSet ds)
    //{
    //    ddlList.Items.Clear();
    //    ddlList.Items.Add("Please Select");
    //    ddlList.SelectedItem.Value = "0";

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlList.DataSource = ds;
    //        ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
    //        ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
    //        ddlList.DataBind();
    //        ddlList.SelectedIndex = 0;
    //    }
    //}

    protected void btnInMrkPDF_Click(object sender, EventArgs e)
    {
        int UA_NO;
        string UANO;
        if (Session["usertype"].ToString().Equals("1"))
        {

            UANO = objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(UA_NO,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SCHEME_NO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue));
            if (UANO == "")
            {
                objCommon.DisplayMessage(updpnl,"Data Not Found !!", this.Page);
                return;
            }else
            {
                UA_NO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(UA_NO,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SCHEME_NO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue)));
                ShowReport("SubjectWiseMarksEntryReport", "rptMarksCoursewise.rpt", UA_NO);
            }
            
        }
        else
        {
            UANO = objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(UA_NO,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SCHEME_NO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue));
            if (UANO == "")
            {
                objCommon.DisplayMessage(updpnl, "Data Not Found !!", this.Page);
                return;
            }
            else
            {
                UA_NO = Convert.ToInt32(Session["userno"].ToString());
                ShowReport("SubjectWiseMarksEntryReport", "rptMarksCoursewise.rpt", UA_NO);
            }
        }

    }
    protected void btnWeightarpt_Click(object sender, EventArgs e)
    {
        int UA_NO;
        string UANO;
        if (Session["usertype"].ToString().Equals("1"))
        {
            UANO = objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(UA_NO,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SCHEME_NO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue));
            if (UANO == "")
            {
                objCommon.DisplayMessage(updpnl, "Data Not Found !!", this.Page);
                return;
            }
            else
            {
                UA_NO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(UA_NO,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SCHEME_NO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue)));
                ShowReportWeightarpt("MarksListReport", "rpt_CIA_Report_Weightage_Wise.rpt", UA_NO);
            }
        }
        else
        {
            UANO = objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(UA_NO,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SCHEME_NO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue));
            if (UANO == "")
            {
                objCommon.DisplayMessage(updpnl, "Data Not Found !!", this.Page);
                return;
            }
            else
            {
                UA_NO = Convert.ToInt32(Session["userno"].ToString());
                ShowReportWeightarpt("MarksListReport", "rpt_CIA_Report_Weightage_Wise.rpt", UA_NO);
            }
        }
    }
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    private void ShowReport(string reportTitle, string rptFileName, int UA_NO)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_UA_NO=" + Convert.ToInt32(UA_NO) + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportWeightarpt(string reportTitle, string rptFileName, int UA_NO)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_UA_NO=" + Convert.ToInt32(UA_NO) + "";
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(UA_NO) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}