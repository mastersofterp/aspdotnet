//======================================================================================
// PROJECT NAME  : UAIMS [RAIPUR]                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : TIME TABLE REPORT
// CREATION DATE : 30 OCT 2012                                                          
// CREATED BY    :                                                 
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;


public partial class ACADEMIC_EXAMINATION_TimeTableReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
   
    //string undo_DetainRemark = string.Empty;
    //string undoDetainDate = string.Empty;
   
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "4") || (Session["usertype"].ToString() == "1"))
                    this.FillDropdown();
                //else
                //    objCommon.DisplayMessage(this.updDetained,"You are not authorized to view this page!!", this.Page);
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }

    }
   
    #region User Defined Methods
  
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
        }
    }
   
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region Common Methods
   
    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSessionReport, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlDegreeReport, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "count(1)", "sessionno=" + Convert.ToInt32(ddlSessionReport.SelectedValue) + "and degreeno=" + Convert.ToInt32(ddlDegreeReport.SelectedValue) + "and branchno=" + Convert.ToInt32(ddlBranchReport.SelectedValue)) == "0")
        {
            objCommon.DisplayMessage(updDetained, "Record not found", this);
            return;
        }
        else
        {
            if (Convert.ToInt32(Session["OrgId"]) == 9) //Atlas
            {
                this.ShowReport("TimeTable_Report", "rptTimeTable_atlas.rpt");
            }
            else
            {
                Show("TimeTable_Report", "rptTimeTable.rpt");
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +//+ ddlCollege.SelectedValue + 
                ",@P_SESSIONNO=" + ddlSessionReport.SelectedValue +
                ",@P_DEGREENO=" + ddlDegreeReport.SelectedValue +
                ",@P_BRANCHNO=" + ddlBranchReport.SelectedValue +
                ",@P_EXAMNAME=" + ddlExamName.SelectedItem.Text +
                ",@P_SCHEMENO=" + ddlScheme.SelectedValue +
                ",@P_EXAM_NO=" + ddlExamName.SelectedValue +
                ",@P_SEMESTERNO=" + ddlSemester.SelectedValue +
                ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + "";
               // ",@P_INSTITUTE=" + ddlCollege.SelectedItem.Text + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updDetained, this.updDetained.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void Show(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSessionReport.SelectedValue) +
                    ",@P_DEGREENO=" + Convert.ToInt32(ddlDegreeReport.SelectedValue) +
                 ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchReport.SelectedValue) +
                  ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                   ",@P_SECTIONNO=" + 0 +
                 ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) +
                 ",@P_EXAM_NO=" + Convert.ToInt32(ddlExamName.SelectedValue) +
                 ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) +
                 ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updDetained, this.updDetained.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.Show() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                ddlDegreeReport.Items.Clear();
                objCommon.FillDropDownList(ddlDegreeReport, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENAME");
                ddlDegreeReport.Focus();
            }
            else
            {
                ddlDegreeReport.Items.Clear();
                ddlDegreeReport.Items.Add(new ListItem("Please Select", "0"));
                objCommon.DisplayMessage("Please select College Name.", this.Page);
            }
            ddlBranchReport.SelectedIndex = -1;
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
            ddlExamName.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_TimeTableReport.ddlCollege_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void ddlDegreeReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegreeReport.SelectedIndex > 0)
            {
                ddlBranchReport.Items.Clear();
                objCommon.FillDropDownList(ddlBranchReport, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH D WITH (NOLOCK) ON (B.BRANCHNO=D.BRANCHNO)", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND D.DEGREENO = " + ddlDegreeReport.SelectedValue, "B.BRANCHNO");
                ddlBranchReport.Focus();
            }
            else
            {
                ddlBranchReport.Items.Clear();
                ddlBranchReport.Items.Add(new ListItem("Please Select", "0"));
                objCommon.DisplayMessage("Please select Degree Name.", this.Page);
            }
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
            ddlExamName.SelectedIndex = -1;
        }
        //objCommon.FillDropDownList(ddlBranchReport, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegreeReport.SelectedValue), "B.BRANCHNO");
       catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_TimeTableReport.ddlBranchReport_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
       
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnTheoryReport_Click(object sender, EventArgs e)
    {
        if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "count(1)", "sessionno=" + Convert.ToInt32(ddlSessionReport.SelectedValue) + "and degreeno=" + Convert.ToInt32(ddlDegreeReport.SelectedValue) + "and branchno=" + Convert.ToInt32(ddlBranchReport.SelectedValue)) == "0")
        {
            objCommon.DisplayMessage(updDetained, "Record not found", this);
            return;
        }
        else
        {
            ShowReport("TimeTable_Report", "rptTimeTableTheory.rpt");
        }
    }
    protected void ddlBranchReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranchReport.SelectedIndex > 0)
            {
                ddlScheme.Items.Clear();
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranchReport.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegreeReport.SelectedValue), "S.SCHEMENAME DESC");
                ddlScheme.Focus();
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
              
            }
            ddlSemester.SelectedIndex = -1;
            ddlExamName.SelectedIndex = -1;
        }
        //objCommon.FillDropDownList(ddlBranchReport, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegreeReport.SelectedValue), "B.BRANCHNO");
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_TimeTableReport.ddlBranchReport_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
        
    }
    private void ShowReportAttendanceSheet(string reportTitle, string rptFileName,int elect)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + 
                ",@P_SESSIONNO=" + ddlSessionReport.SelectedValue +
                ",@P_DEGREENO=" + ddlDegreeReport.SelectedValue +
                ",@P_BRANCHNO=" + ddlBranchReport.SelectedValue +
                ",@P_SEMESTERNO=" + ddlSemester.SelectedValue +
                ",@P_EXAM_NO=" + ddlExamName.SelectedValue +
                //",@P_DEGREENAME=" + ddlDegreeReport.SelectedItem.Text +  
                //",@P_BRANCHNAME=" + ddlBranchReport.SelectedItem.Text +
                ",@P_EXAMNAME=" + ddlExamName.SelectedItem.Text +
                 ",@P_SCHEMENO=" + ddlScheme.SelectedValue +
                 ",@P_PREV_STATUS=" + rdReg_Ex.SelectedValue+
                 ",@P_ELECT=" + elect + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updDetained, this.updDetained.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnStudAttndance_Click(object sender, EventArgs e)
    {
        int elect = 0;
        
            elect = 0;
        DataSet ds = objExamController.GetStudAttendanceSheetData(Convert.ToInt32(ddlSessionReport.SelectedValue),
            Convert.ToInt32(ddlDegreeReport.SelectedValue),
            Convert.ToInt32(ddlBranchReport.SelectedValue),
            Convert.ToInt32(ddlSemester.SelectedValue),
            Convert.ToInt32(ddlExamName.SelectedValue),
            Convert.ToInt32(ddlScheme.SelectedValue),
            Convert.ToInt32(rdReg_Ex.SelectedValue),
            elect
            );

        if (ds.Tables[0].Rows.Count > 0)
        {
            ShowReportAttendanceSheet("AttendanceSheet_Report", "rptStudAttendanceSheet.rpt", elect);
        }
        else
        {
            objCommon.DisplayMessage(updDetained, "No Data Found for current selection.", this.Page);
        }

    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_EXAM_DATE E WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON(S.SEMESTERNO=E.SEMESTERNO)", " DISTINCT E.SEMESTERNO", " S.SEMESTERNAME", "E.BRANCHNO=" + Convert.ToInt32(ddlBranchReport.SelectedValue) + " and E.degreeno=" + Convert.ToInt32(ddlDegreeReport.SelectedValue) + "AND E.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and E.SESSIONNO=" + Convert.ToInt32(ddlSessionReport.SelectedValue), "E.SEMESTERNO");
            ddlSemester.Focus();

            ddlExamName.Items.Clear();
            objCommon.FillDropDownList(ddlExamName, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " ED.FLDNAME IN('S3','EXTERMARK') AND EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(ddlBranchReport.SelectedValue) + " and degreeno=" + Convert.ToInt32(ddlDegreeReport.SelectedValue), "EXAMNAME");
            ddlExamName.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlExamName.Items.Clear();
            ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        }
        ddlExamName.SelectedIndex = -1;
    }
    protected void btnElectiveAttendanceSheet_Click(object sender, EventArgs e)
    {
        int elect = 0;
            elect = 1;
        DataSet ds = objExamController.GetStudAttendanceSheetData(Convert.ToInt32(ddlSessionReport.SelectedValue),
            Convert.ToInt32(ddlDegreeReport.SelectedValue),
            Convert.ToInt32(ddlBranchReport.SelectedValue),
            Convert.ToInt32(ddlSemester.SelectedValue),
            Convert.ToInt32(ddlExamName.SelectedValue),
            Convert.ToInt32(ddlScheme.SelectedValue),
            Convert.ToInt32(rdReg_Ex.SelectedValue),
            elect
            );

        if (ds.Tables[0].Rows.Count > 0)
        {
            ShowReportAttendanceSheet("Elective_AttendanceSheet_Report", "rptStudAttendanceSheet.rpt", elect);
        }
        else
        {
            objCommon.DisplayMessage(updDetained, "No Data Found for current selection.", this.Page);
        }
    }

    protected void btnTimeTable_Click(object sender, EventArgs e)
    {
        DateTime FromDate=Convert.ToDateTime(txtFromDate.Text.Trim());
        DateTime ToDate = Convert.ToDateTime(txtTodate.Text.Trim());
        if (FromDate > ToDate)
        {
            objCommon.DisplayMessage(this.Page, "From Date Should be Smaller Than To Date", this.Page);
            return;
        }


        GridView GvStudent = new GridView();
        DataSet ds = objExamController.GetDateWiseTimeTable(Convert.ToInt32(ddlSessionReport.SelectedValue), FromDate, ToDate);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = ds;
            GvStudent.DataBind();
            string attachment = "attachment; filename=DateWiseTimeTable.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            return;
        }
    }
               



     
    
}
