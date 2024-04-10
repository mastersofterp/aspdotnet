<<<<<<< HEAD
=======
﻿//==============================================================================
//PROJECT NAME  : RF-Common Code
//MODULE NAME   : ACADEMIC (ACADEMIC REPORTS)
//PAGE NAME     : ACADEMIC - ATTENDANCE NOT FILLED BY FACULTY REPORT
//CREATION DATE : 12-APR-2012     
//CREATED BY    : UMESH K. GANORKAR 
// Version      : 1.0.0
//=================================================================================
//-----------------------------------------------------------------------------------------------------------------------------------------------------                                                                  
//Version		Modified On		Modified By			     Purpose                                                                    
//----------------------------------------------------------------------------------------------------------------------------------------------------                                                                    
//1.0.1		    28-02-2024		Jay Takalkhede.			1. Added Date Filter Class Attendance Entry Report Of Faculty (TkNo.51716)
//1.0.2	        09-04-2024		Jay Takalkhede.			1. Added report in which get Global Elective Course Attendance Data (TkNo.56806)
//------------------------------------------- ----------------------------------------------------------------------------------------------------------
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;
//==============================================================================
//PROJECT NAME  : RF-Common Code
//MODULE NAME   : ACADEMIC (ACADEMIC REPORTS)
//PAGE NAME     : ACADEMIC - ATTENDANCE NOT FILLED BY FACULTY REPORT
//CREATION DATE : 12-APR-2012     
//CREATED BY    : UMESH K. GANORKAR 
// Version      : 1.0.0
//=================================================================================
//-----------------------------------------------------------------------------------------------------------------------------------------------------                                                                  
//Version		Modified On		Modified By			     Purpose                                                                    
//----------------------------------------------------------------------------------------------------------------------------------------------------                                                                    
//1.0.1		    28-02-2024		Jay Takalkhede.			1. Added Date Filter Class Attendance Entry Report Of Faculty (TkNo.51716)
//1.0.2	        09-04-2024		Jay Takalkhede.			1. Added report in which get Global Elective Course Attendance Data (TkNo.56806)
//------------------------------------------- ----------------------------------------------------------------------------------------------------------

public partial class ACADEMIC_REPORTS_FacultyAttendanceNotFilled : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    ResultProcessing objResult = new ResultProcessing();
    AcdAttendanceController acdatt = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();

    #region Page Evnets
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
                //Added By Jay T. On dated 28022024 tkno.51716
                DivLabel2.Visible = false;
                DivLabel.Visible = true;
                //Set the Page Title
                this.Page.Title = Session["coll_name"].ToString();
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    if (ua_type == "8")
                    {
                        divHOD.Visible = true;
                        divAdmin.Visible = false;
                        divAdminMain.Visible = false;
                        divHODMain.Visible = true;
                        btnAttReport.Visible = false;
                        btnAttTracker.Visible = false;
                        btnAttRegister.Visible = false;
                        btnConAtt.Visible = false;
                        btnExcel.Visible = false;
                        divAdminMainButton.Visible = false;
                        divHODMainButton.Visible = true;
                        divSessionHOD.Visible = true;
                        //divMClg.Visible = true;
                        divSDHOD.Visible = true;
                        divSDHOD1.Visible = true;
                    }
                    else
                    {
                        divAdmin.Visible = true;
                        divHOD.Visible = false;
                        divAdminMain.Visible = true;
                        divHODMain.Visible = false;
                        divAdminMainButton.Visible = true;
                        divHODMainButton.Visible = false;
                        //calender1.Visible = true;
                    }
                FillDropDownList();
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
        }
    }
    public void HiddenItem()
    {
        string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        if (ua_type == "8")
        {

            foreach (ListItem item in this.rdbReports.Items)
            {
                if (item.Value == "1" || item.Value == "2" || item.Value == "3" || item.Value == "4")
                {
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
        else
        {
            foreach (ListItem item in this.rdbReports.Items)
            {
                if (item.Value == "5")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ResultReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultReport.aspx");
        }
    }
<<<<<<< HEAD
=======

>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])

    private void FillDropDownList()
    {
        try
        {
            divCollege.Visible = true;
            divDegree.Visible = true;
            divBranch.Visible = true;
            divSem.Visible = true;
            divsession.Visible = false;
            btnAttTracker.Visible = true;
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "DISTINCT SECTIONNO", "SECTIONNAME", "SECTIONNO>0 and ISNULL(ACTIVESTATUS,0)=1", "SECTIONNO");
            objCommon.FillDropDownList(ddlSessionn, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_PNAME", "SESSIONID>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONID");
            objCommon.FillDropDownList(ddlSessionHOD, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_PNAME", "SESSIONID>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONID");
            try
            {
                if (Session["usertype"].ToString() != "1") // prog co-ordinator / faculty
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                }

                //lboddlCollege.ClearSelection();
                //objCommon.FillListBox(lboddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                //lboddlCollege.Focus();
            }
            catch
            {
                throw;
            }
            AcademinDashboardController objADEController = new AcademinDashboardController();
            DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
            ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSession1.DataSource = ds;
                ddlSession1.DataValueField = "SESSIONNO";
                ddlSession1.DataTextField = "COLLEGE_SESSION";
                ddlSession1.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #endregion

    #region Page comment
    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "4")
    //    {
    //        // objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
    //        objCommon.FillDropDownList(ddlDept, "ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH C WITH (NOLOCK) ON B.BRANCHNO=C.BRANCHNO", "C.BRANCHNO", "C.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
    //    }
    //    else if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3"))
    //    {
    //        objCommon.FillDropDownList(ddlDept, "ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH C WITH (NOLOCK) ON B.BRANCHNO=C.BRANCHNO", "C.BRANCHNO", "C.LONGNAME", "B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + "AND D.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"] + ") ", "B.BRANCHNO");
    //    }
    //    else
    //    {
    //        objCommon.FillDropDownList(ddlDept, "ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH C WITH (NOLOCK) ON B.BRANCHNO=C.BRANCHNO", "C.BRANCHNO", "C.LONGNAME", "B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + "AND D.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO=" + Session["userdeptno"].ToString(), "C.LONGNAME");
    //    }
    //    this.FillMonth();
    //    //if (Session["usertype"].ToString() == "1")  //For Principal/admin
    //    //{
    //    //    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_BRANCH B ON D.DEPTNO = B.DEPTNO", "DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "D.DEPTNO");
    //    //    ddlDept.Focus();
    //    //}
    //    //else                                        //For HOD
    //    //{
    //    //    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_BRANCH B ON D.DEPTNO = B.DEPTNO", "DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO IN(" + Session["deptno"].ToString() + ") AND B.DEGREENO = " + ddlDegree.SelectedValue, "D.DEPTNO");
    //    //    ddlDept.Items.RemoveAt(0);
    //    //}
    //    //objCommon.FillDropDownList("Please Select", ddlDivision, "ACD_COURSE_TEACHER A,ACD_DIVISION B", "DISTINCT B.DIVISIONNO", "B.DIVISIONNAME", "A.SECTIONNO = B.DIVISIONNO AND A.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.DEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND A.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "B.DIVISIONNAME");
    //    ////ddlDivision.Focus();
    //}
    //private void FillMonth()
    //{
    //   // DataSet date = objCommon.FillDropDown("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSION_STDATE", "SESSION_ENDDATE", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO=" + ddlSession.SelectedValue, "SESSION_STDATE");
    //    DataSet date = objCommon.FillDropDown("ACD_ATTENDANCE_CONFIG AC WITH (NOLOCK)", "START_DATE", "END_DATE", "SESSIONNO>0 AND ISNULL(ACTIVE,0)=1 AND SESSIONNO=" + ddlSession.SelectedValue +" AND COLLEGE_ID="+ddlCollege.SelectedValue+" AND DEGREENO="+ddlDegree.SelectedValue, "START_DATE");
    //    if (date != null && date.Tables.Count > 0 && date.Tables[0].Rows.Count > 0)
    //    {
    //        string[] st = date.Tables[0].Rows[0].ItemArray[0].ToString().Split('/');
    //        string[] end = date.Tables[0].Rows[0].ItemArray[1].ToString().Split('/');
    //        string[] month = { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
    //        int i = Convert.ToInt16(st[1]);
    //        ddlMonth.Items.Clear();
    //        ddlMonth.Items.Add(new ListItem("Please Select", "0"));
    //        do
    //        {
    //            ddlMonth.Items.Add(new ListItem(month[i - 1], i.ToString()));
    //            i++;
    //        }
    //        while (i <= Convert.ToInt16(end[1]));
    //    }
    //}
    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   // FillMonth();
    //    ddlDegree.Focus();
    //}
    //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlMonth.Focus();
    //    //objCommon.FillDropDownList("Please Select", ddlDivision, "ACD_COURSE_TEACHER A,ACD_DIVISION B", "DISTINCT B.DIVISIONNO", "B.DIVISIONNAME", "A.SECTIONNO = B.DIVISIONNO AND A.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.DEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND A.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "B.DIVISIONNAME");
    //    // ddlDivision.Focus();
    //}
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.ToString());
    //}
    //protected void btnAttReport_Click(object sender, EventArgs e)
    //{
    //    AcdAttendanceController objAC = new AcdAttendanceController();
    //    if (ddlSession.SelectedIndex <= 0 || ddlDegree.SelectedIndex <= 0 || ddlMonth.SelectedIndex <= 0)
    //    {
    //        objCommon.DisplayMessage(this.updAtt, "Please Select Term/Degree/Department/Month", this.Page);
    //        return;
    //    }
    //    else
    //    {
    //        int days = rbDays.SelectedValue == "" ? 0 : Convert.ToInt32(rbDays.SelectedValue);
    //        DataSet ds = objAC.Pending_Attendance_Details(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(days));
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvPendingAttendance.DataSource = ds;
    //            lvPendingAttendance.DataBind();
    //            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPendingAttendance);//Set label -
    //            divlvStudentHeading.Visible = true;
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updAtt, "No data found", this.Page);
    //            divlvStudentHeading.Visible = false;
    //        }
    //    }
    //     //   this.ShowAttendanceNotFilled("AttendanceNotFilledByFaculty", "rptFacultyAttNotFilled.rpt");
    //}
    //private void ShowAttendanceNotFilled(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_MONTH=" + Convert.ToInt32(ddlMonth.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",Username=" + Session["username"].ToString() + ",@P_DIVISIONNO=" + ddlDivision.SelectedValue + ",@P_MONTH_DAYS=" + rbDays.SelectedValue;

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";

    //        string Script = string.Empty;
    //        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        ScriptManager.RegisterClientScriptBlock(this.updAtt, updAtt.GetType(), "Report", Script, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }

    //}
    //protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //ddlMonth.Focus();
    //}
    //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    rbDays.Focus();
    //}

    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCollege.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO DESC");
    //    }
    //    else
    //    {
    //        ddlSession.Items.Clear();
    //        ddlSession.Items.Add(new ListItem("Please Select", "0"));
    //    }
    //}
    #endregion

    #region Clear
    private void ClearControls()
    {
        ddlSession1.ClearSelection();
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
    }
    private void ClearControls1()
    {
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlSchool.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Clear
<<<<<<< HEAD

    #region Attendance Marked & Not-Marked
    protected void btnAttReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                TimeSpan dt = Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text);
                if (dt.TotalDays > 31)
                {
                    objCommon.DisplayMessage(this, "You can only select dates within 31 days", this.Page);
                    return;
                }
                else
                {
                    if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartDate.Text))
                    {
                        objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                        return;
                    }
                    else
                    {
                        string AttendanceStartDate = txtStartDate.Text;
                        string AttendanceEndDate = txtEndDate.Text;
                        int Sessionnos = Convert.ToInt32(ddlSession.SelectedValue);
                        int College_code = Convert.ToInt32(ddlSchool.SelectedValue);
                        DataSet ds = acdatt.RetrieveStudentAttDetailsMarkedExcel(AttendanceStartDate, AttendanceEndDate, Sessionnos, College_code);
                        DataGrid dg = new DataGrid();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string attachment = "attachment; filename= AttendanceDetails Attendance Marked-Not Marked Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.DataSource = ds.Tables[0];
                            dg.DataBind();
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Record Not Found!!", this.Page);
                            return;
                        }
                        ClearControls();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Attendance Marked & Not-Marked
=======
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])

    #region Attendance Tracker
    protected void btnAttTracker_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                TimeSpan dt = Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text);
                if (dt.TotalDays > 62)
                {
                    objCommon.DisplayMessage(this, "You can select Start date & End date within 2 month", this.Page);
                    return;
                }
                else
                {
                    if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartDate.Text))
                    {
                        objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                        return;
                    }
                    else
                    {
                        string AttendanceStartDate = txtStartDate.Text;
                        string AttendanceEndDate = txtEndDate.Text;
                        objAttModel.College_code = Session["colcode"].ToString();
                        DataSet dsStudList = acdatt.RetrieveStudentAttTracker(AttendanceStartDate, AttendanceEndDate, Convert.ToInt32(ddlSchool.SelectedValue.ToString()),
                                             Convert.ToInt32(ddlDegree.SelectedValue.ToString()), Convert.ToInt32(ddlBranch.SelectedValue.ToString()),
                                             Convert.ToInt32(ddlsemester.SelectedValue.ToString()));
                        DataGrid dg = new DataGrid();

                        //if (dsStudList.Tables[0].Rows.Count > 0)
                        //{
                        //    string attachment = "attachment; filename= AttendanceDetails Attendance Tracker Report.xls";

                        //    Response.ClearContent();
                        //    Response.AddHeader("content-disposition", attachment);
                        //    Response.ContentType = "application/" + "ms-excel";
                        //    StringWriter sw = new StringWriter();
                        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
                        //    dg.DataSource = dsStudList.Tables[0];
                        //    dg.DataBind();
                        //    dg.HeaderStyle.Font.Bold = true;
                        //    dg.RenderControl(htw);
                        //    Response.Write(sw.ToString());
                        //    Response.End();
                        //}
                        if (dsStudList.Tables[0].Rows.Count > 0 || dsStudList.Tables[1].Rows.Count > 0)
                        {

                            dsStudList.Tables[0].TableName = "For Faculty Wise ";
                            dsStudList.Tables[1].TableName = "For Date Wise ";
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                foreach (System.Data.DataTable dm in dsStudList.Tables)
                                {
                                    //Add System.Data.DataTable as Worksheet.
                                    if (dm != null && dm.Rows.Count > 0)
                                        wb.Worksheets.Add(dm);
                                }

                                //Export the Excel file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=AttendanceDetails Attendance Tracker Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Record Not Found!!", this.Page);
                            return;
                        }
                        ClearControls1();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Attendance Tracker

<<<<<<< HEAD
=======
    #region Attendance Marked & Not-Marked
    protected void btnAttReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                TimeSpan dt = Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text);
                if (dt.TotalDays > 31)
                {
                    objCommon.DisplayMessage(this, "You can only select dates within 31 days", this.Page);
                    return;
                }
                else
                {
                    if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartDate.Text))
                    {
                        objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                        return;
                    }
                    else
                    {
                        string AttendanceStartDate = txtStartDate.Text;
                        string AttendanceEndDate = txtEndDate.Text;
                        int Sessionnos = Convert.ToInt32(ddlSession.SelectedValue);
                        int College_code = Convert.ToInt32(ddlSchool.SelectedValue);
                        DataSet ds = acdatt.RetrieveStudentAttDetailsMarkedExcel(AttendanceStartDate, AttendanceEndDate, Sessionnos, College_code);
                        DataGrid dg = new DataGrid();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string attachment = "attachment; filename= AttendanceDetails Attendance Marked-Not Marked Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.DataSource = ds.Tables[0];
                            dg.DataBind();
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Record Not Found!!", this.Page);
                            return;
                        }
                        ClearControls();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion  Attendance Marked & Not-Marked

>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])
    #region Bind DDL
    protected void rdbReports_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbReports.SelectedValue == "1")
        {
            divsession.Visible = false;
            divCollege.Visible = true;
            divDegree.Visible = true;
            divBranch.Visible = true;
            divSem.Visible = true;
            btnAttReport.Visible = false;
            btnAttTracker.Visible = true;
            ddlSession1.ClearSelection();
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            DivSession1.Visible = false;
            divClg.Visible = false;
            divTeacher.Visible = false;
            divcourse.Visible = false;
            divSection.Visible = false;
            btnAttRegister.Visible = false;
            btnConAtt.Visible = false;
            ddlSchool.SelectedIndex = 0;
            calender1.Visible = true;
            txtEndDate.Visible = true;
            //Added By Jay T. On dated 28022024 tkno.51716
            DivLabel2.Visible = false;
            DivLabel.Visible = true;
            dvSession.Visible = false;
            btnExcel.Visible = false;
<<<<<<< HEAD
            btnGlobalAtt.Visible = false;
=======
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])
            //HiddenItem();

        }
        else if (rdbReports.SelectedValue == "2")
        {
            divsession.Visible = true;
            divDegree.Visible = false;
            divBranch.Visible = false;
            divSem.Visible = false;
            btnAttReport.Visible = true;
            btnAttTracker.Visible = false;
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            ddlSchool.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlsemester.SelectedIndex = 0;
            calender1.Visible = true;
            txtEndDate.Visible = true;
            DivSession1.Visible = true;
            divClg.Visible = false;
            divTeacher.Visible = false;
            divcourse.Visible = false;
            divSection.Visible = false;
            btnAttRegister.Visible = false;
            btnConAtt.Visible = false;
            divCollege.Visible = true;
            dvSession.Visible = false;
            btnExcel.Visible = false;
            //Added By Jay T. On dated 28022024 tkno.51716
            DivLabel2.Visible = false;
            DivLabel.Visible = true;
<<<<<<< HEAD
            btnGlobalAtt.Visible = false;
=======
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])
            //HiddenItem();
        }
        else if (rdbReports.SelectedValue == "3")
        {
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            divCollege.Visible = false;
            divDegree.Visible = false;
            divBranch.Visible = false;
            divSem.Visible = false;
            divsession.Visible = false;
            btnAttTracker.Visible = false;
            DivSession1.Visible = true;
            divClg.Visible = true;
            divTeacher.Visible = true;
            divcourse.Visible = true;
            divSection.Visible = true;
            btnAttReport.Visible = false;
            btnAttRegister.Visible = true;
            btnConAtt.Visible = true;
            dvSession.Visible = false;
            btnExcel.Visible = false;
            ddlSession.SelectedIndex = 0;
            //Added By Jay T. On dated 28022024 tkno.51716
            DivLabel2.Visible = false;
            calender1.Visible = true;
            txtEndDate.Visible = true;
            DivLabel.Visible = true;
<<<<<<< HEAD
            btnGlobalAtt.Visible = false;
=======
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])
            //HiddenItem();
        }
        else if (rdbReports.SelectedValue == "4")
        {
            divsession.Visible = true;
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            divCollege.Visible = false;
            divDegree.Visible = false;
            divBranch.Visible = false;
            divSem.Visible = false;
            divsession.Visible = false;
            btnAttTracker.Visible = false;
            DivSession1.Visible = false;
            divClg.Visible = false;
            divTeacher.Visible = false;
            divcourse.Visible = false;
            divSection.Visible = false;
            btnAttReport.Visible = false;
            btnAttRegister.Visible = false;
            btnConAtt.Visible = false;
            txtEndDate.Visible = false;
            calender1.Visible = false;
            dvSession.Visible = true;
            btnExcel.Visible = true;
            ddlSession.SelectedIndex = 0;
            //Added By Jay T. On dated 28022024 tkno.51716
            DivLabel.Visible = false;
            calender.Visible = true;
            DivLabel2.Visible = true;
            txtStartDate.Visible = true;
<<<<<<< HEAD
            btnGlobalAtt.Visible = false;
            //HiddenItem();
        }
        else if (rdbReports.SelectedValue == "5")
        {
            divsession.Visible = true;
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            divCollege.Visible = false;
            divDegree.Visible = false;
            divBranch.Visible = false;
            divSem.Visible = false;
            divsession.Visible = false;
            btnAttTracker.Visible = false;
            DivSession1.Visible = false;
            divClg.Visible = false;
            divTeacher.Visible = false;
            divcourse.Visible = false;
            divSection.Visible = false;
            btnAttReport.Visible = false;
            btnAttRegister.Visible = false;
            btnConAtt.Visible = false;
            txtEndDate.Visible = true;
            calender1.Visible = true;
            dvSession.Visible = true;
            btnExcel.Visible = false;
            ddlSession.SelectedIndex = 0;
            //Added By Jay T. On dated 28022024 tkno.51716
            DivLabel.Visible = true;
            calender.Visible = true;
            DivLabel2.Visible = false;
            txtStartDate.Visible = true;
            btnGlobalAtt.Visible = true;
        }
=======
            //HiddenItem();
        }
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])

    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbReports.SelectedValue == "1")
        {
            ddlsemester.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            if (ddlSchool.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "DISTINCT (CDB.DEGREENO)", "D.DEGREENAME", "CDB.COLLEGE_ID=" + ddlSchool.SelectedValue + " AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CDB.DEGREENO");
            }
        }
        else if (rdbReports.SelectedValue == "2")
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + ddlSchool.SelectedValue, "SESSIONNO DESC");
            ddlSession.Focus();
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT (CDB.BRANCHNO)", "B.LONGNAME", "CDB.COLLEGE_ID=" + ddlSchool.SelectedValue + " AND CDB.DEGREENO =" + ddlDegree.SelectedValue + " AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CDB.BRANCHNO");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbReports.SelectedValue == "3")
            {
                if (ddlSession.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_COURSE_TEACHER SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(CANCEL,0)=0 AND SR.SCHEMENO=" + ViewState["schemeno"], "SR.COURSENO");
                }
            }
            else
            {
            }
        }
        catch
        {
            throw;
        }

    }
    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
                }
            }
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
            ddlSession.Focus();
        }
        catch
        {
            throw;
        }
    }
    #endregion Bind DDL

    #region Attendance register & Consolidated Attendance
    protected void btnAttRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
                {
                    objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                    return;
                }
                else
                {
                    DateTime AttendanceStartDate = Convert.ToDateTime(txtStartDate.Text);
                    DateTime AttendanceEndDate = Convert.ToDateTime(txtEndDate.Text);
                    int Clgname = Convert.ToInt32(ddlClgname.SelectedValue);
                    int session = Convert.ToInt32(ddlSession.SelectedValue);
                    int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
                    int section = Convert.ToInt32(ddlSection.SelectedValue);
                    int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                    DataSet DS = acdatt.RetrieveAttRegister_Report(AttendanceStartDate, AttendanceEndDate, Convert.ToInt32(Session["userno"]), schemeno, session, courseno, section);
                    DataGrid dg = new DataGrid();

                    if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                    {
                        string attachment = "attachment; filename= STUDENT_ATTENDANCE_REGISTER_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/" + "ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        dg.DataSource = DS.Tables[0];
                        dg.DataBind();
                        dg.HeaderStyle.Font.Bold = true;
                        dg.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Record Not Found!!", this.Page);
                        return;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnConAtt_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
                {
                    objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                    return;
                }
                else
                {
                    string AttendanceStartDate = txtStartDate.Text;
                    string AttendanceEndDate = txtEndDate.Text;
                    int Clgname = Convert.ToInt32(ddlClgname.SelectedValue);
                    int session = Convert.ToInt32(ddlSession.SelectedValue);
                    int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
                    int section = Convert.ToInt32(ddlSection.SelectedValue);
                    int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                    DataSet DS = acdatt.RETRIEVE_COURSEWISE_CONSOLIDATED_REPORT(AttendanceStartDate, AttendanceEndDate, Convert.ToInt32(Session["userno"]), schemeno, session, courseno, section);
                    DataGrid dg = new DataGrid();

                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string attachment = "attachment; filename= COURSEWISE_CONSOLIDATED_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/" + "ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        dg.DataSource = DS.Tables[0];
                        dg.DataBind();
                        dg.HeaderStyle.Font.Bold = true;
                        dg.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Record Not Found!!", this.Page);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Attendance register & Consolidated Attendance

    #region class Attendance Entry Report Of Faculty

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (ddlSessionn.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this, "Please Select Session.", this.Page);
            return;
        }
        else
        {
            DateTime AttendanceStartDate = Convert.ToDateTime(txtStartDate.Text);
            DataSet DS = acdatt.GetFacultyLectureCount(Convert.ToInt32(ddlSessionn.SelectedValue), AttendanceStartDate);
            DataGrid dg = new DataGrid();
            // Added two tab excel report By Jay takalkhede On Dated 28022024 (TkNo.51716)
            // Added Date Filter Class Attendance Entry Report Of Faculty 
            if (DS.Tables[0].Rows.Count > 0 || DS.Tables[1].Rows.Count > 0)
            {
                DS.Tables[0].TableName = "Faculty wise Lecture Status";
                DS.Tables[1].TableName = "Course wise Lecture  Status";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in DS.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }
                    //string attachment = "attachment; filename= Faculty_Lecture_Count_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                    //Response.ClearContent();
                    //Response.AddHeader("content-disposition", attachment);
                    //Response.ContentType = "application/" + "ms-excel";
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter htw = new HtmlTextWriter(sw);
                    //dg.DataSource = DS.Tables[0];
                    //dg.DataBind();
                    //dg.HeaderStyle.Font.Bold = true;
                    //dg.RenderControl(htw);
                    //Response.Write(sw.ToString());
                    //Response.End();
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Faculty_Lecture_Count_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Not Found!!", this.Page);
                return;
            }
        }
    }

    #endregion

<<<<<<< HEAD
    #region Attendance Report For Global Elective
    protected void btnGlobalAtt_Click(object sender, EventArgs e)
    {
        //string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        //if (ua_type == "8")
        //{
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
                {
                    objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                    return;
                }
                else
                {
                    string AttendanceStartDate = txtStartDate.Text;
                    string AttendanceEndDate = txtEndDate.Text;
                    //College = College.Substring(0, College.Length - 1);
                    int session = Convert.ToInt32(ddlSessionn.SelectedValue);
                    DataSet DS = RETRIEVE_GLOBAL_ELECTIVE_ATT_REPORT(AttendanceStartDate, AttendanceEndDate, Convert.ToInt32(Session["userno"]), session);
                    DataGrid dg = new DataGrid();

                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string attachment = "attachment; filename= GLOBALELECTIVE_ATT_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/" + "ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        dg.DataSource = DS.Tables[0];
                        dg.DataBind();
                        dg.HeaderStyle.Font.Bold = true;
                        dg.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Record Not Found!!", this.Page);
                        return;
                    }
                }
            }
    }
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //Added by jay takalkhede on dated 10042024 Added report in which get Global Elective Course Attendance Data (TkNo.56806)
    public DataSet RETRIEVE_GLOBAL_ELECTIVE_ATT_REPORT(string AttendanceStartDate, string AttendanceEndDate, int UANO, int session)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_SESSION_ID", session);
            objParams[1] = new SqlParameter("@P_FROMDATE", AttendanceStartDate);
            objParams[2] = new SqlParameter("@P_TODATE", AttendanceEndDate);
            objParams[3] = new SqlParameter("@P_UA_NO", UANO);
            ds = objSQLHelper.ExecuteDataSetSP("ATTENDANCE_REPORT_FOR_GLOBAL_ELECTIVE_OF_HOD", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.RetrieveStudentAttDetailsExcel-> " + ex.ToString());
        }
        return ds;
    }
    #endregion Attendance Report For Global Elective
=======
    #region Attendance Report For Global Elective Of HOD


    protected void btnGlobalAtt_Click(object sender, EventArgs e)
    {
        //string College = string.Empty;
        //int count = 0;
        //foreach (ListItem Item in lboddlCollege.Items)
        //{
        //    if (Item.Selected)
        //    {
        //        College += Item.Value + ",";
        //        count++;
        //    }
        //}
        //if (count > 0)
        //{

            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (ua_type == "8")
            {
                if (txtStartDateHOD.Text != string.Empty && txtEndDateHOD.Text != string.Empty)
                {
                    if (Convert.ToDateTime(txtEndDateHOD.Text) <= Convert.ToDateTime(txtStartDateHOD.Text))
                    {
                        objCommon.DisplayMessage(this, "End Date should be greater than Start Date", this.Page);
                        return;
                    }
                    else
                    {
                        string AttendanceStartDate = txtStartDateHOD.Text;
                        string AttendanceEndDate = txtEndDateHOD.Text;
                        //College = College.Substring(0, College.Length - 1);
                        int session = Convert.ToInt32(ddlSessionHOD.SelectedValue);
                        DataSet DS = acdatt.RETRIEVE_GLOBAL_ELECTIVE_ATT_REPORT(AttendanceStartDate, AttendanceEndDate, Convert.ToInt32(Session["userno"]), session);
                        DataGrid dg = new DataGrid();

                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            string attachment = "attachment; filename= GLOBALELECTIVE_ATT_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/" + "ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            dg.DataSource = DS.Tables[0];
                            dg.DataBind();
                            dg.HeaderStyle.Font.Bold = true;
                            dg.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Record Not Found!!", this.Page);
                            return;
                        }
                    }
                }
            }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this, "Please Select At least One College", this.Page);
        //    return;
        //    College = "0";

        //}

    }

    protected void rbdReportsHOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbdReportsHOD.SelectedValue == "1")
        {
            divSessionHOD.Visible = true;
            //divMClg.Visible = true;
            divSDHOD.Visible = true;
            divSDHOD1.Visible = true;
        }
    }
    protected void btnCancelHOD_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Attendance Report For Global Elective Of HOD
>>>>>>> e66642cc ([NEWREQUIRMENT] [57349] [Faculty Attendance Not Fielled])
}
