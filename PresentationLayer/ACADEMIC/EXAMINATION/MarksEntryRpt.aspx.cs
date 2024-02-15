//=================================================================================
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
using System.Text;
using System.IO;
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
                //CheckPageAuthorization();
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
                // ADDED BY SHUBHAM FOR FACULTY LOGIN 
                string deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING  SC INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=SC.DEGREENO AND CDB.BRANCHNO=SC.BRANCHNO AND CDB.COLLEGE_ID=SC.COLLEGE_ID", "COSCHNO", "COL_SCHEME_NAME", "SC.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SC.COLLEGE_ID > 0 AND SC.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND CDB.DEPTNO IN (" + deptno + ")", "SC.COLLEGE_ID DESC");
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


    protected void btnInMrkPDF_Click(object sender, EventArgs e)
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

    protected void BtnExcelReport_Click(object sender, EventArgs e)
    {
<<<<<<< HEAD
        try
=======
        try 
>>>>>>> 466c108a ( [ENHANCEMENT] [53943] Add excel button on page)
        {
            GridView dg = new GridView();
            string SP_Name = "PKG_GET_INTERNAL_MARK_DETAILS_NOT_DONE_DATA";
            string SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue);
            DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dg.DataSource = ds1.Tables[0];
                    dg.DataBind();
                    //AddReportHeader(dg);
                    string attachment = "attachment; filename=" + "INTERNAL_MARK_DETAILS " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/" + "ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    dg.HeaderStyle.Font.Bold = true;
                    dg.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                }
            }
<<<<<<< HEAD
            else
            {
                objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
            }
        }
        catch (Exception ex)
=======
            else 
            {
                objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
            }
        }catch(Exception ex)
>>>>>>> 466c108a ( [ENHANCEMENT] [53943] Add excel button on page)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BtnExcelReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
<<<<<<< HEAD


    // added by shubham for excel data on 20-02-2024
    protected void btnIntExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int UA_NO;
            string UANO;
            DataSet ds1;
            GridView dg = new GridView();
            string SP_Name;
            string SP_Parameters;
            string Call_Values;
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
                    UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    SP_Name = "PKG_GET_INTERNAL_MARK_DATA_COURSEWISE_EXCEL";
                    SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO";

                    Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(UA_NO);
                    ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            dg.DataSource = ds1.Tables[0];
                            dg.DataBind();
                            //AddReportHeader(dg);
                            string attachment = "attachment; filename=" + "INTERNAL_MARK_DETAILS " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                    }
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
                    SP_Name = "PKG_GET_INTERNAL_MARK_DATA_COURSEWISE_EXCEL";
                    SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO";

                    Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(UA_NO);
                    ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            dg.DataSource = ds1.Tables[0];
                            dg.DataBind();
                            //AddReportHeader(dg);
                            string attachment = "attachment; filename=" + "INTERNAL_MARK_DETAILS " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "btnIntExcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void BtnExcelCW_Click(object sender, EventArgs e)
    {
        try
        {
            int UA_NO;
            string UANO;
            DataSet ds1;
            GridView dg = new GridView();
            string SP_Name;
            string SP_Parameters;
            string Call_Values;
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
                    UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    SP_Name = "PKG_CIA_MARK_REPORT_WITH_WEIGHTAGE_FOR_EXCEL_HITS";
                    SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_SCHEMENO,@P_COLLEGE_ID";

                    //Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(UA_NO);
                    Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(UA_NO) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ViewState["college_id"]);
                    ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            dg.DataSource = ds1.Tables[0];
                            dg.DataBind();
                            //AddReportHeader(dg);
                            string attachment = "attachment; filename=" + "INTERNAL_MARK_DETAILS " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                    }
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
                    SP_Name = "PKG_CIA_MARK_REPORT_WITH_WEIGHTAGE_FOR_EXCEL_HITS";
                    SP_Parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_SCHEMENO,@P_COLLEGE_ID";

                    Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(UA_NO) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ViewState["college_id"]);
                    ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            dg.DataSource = ds1.Tables[0];
                            dg.DataBind();
                            //AddReportHeader(dg);
                            string attachment = "attachment; filename=" + "INTERNAL_MARK_DETAILS " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "No Data Found for this selection.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BtnExcelCW_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
=======
>>>>>>> 466c108a ( [ENHANCEMENT] [53943] Add excel button on page)
}