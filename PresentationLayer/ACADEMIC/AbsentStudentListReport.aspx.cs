using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_AbsentStudentListReport : System.Web.UI.Page
{
    #region Page Evnets
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();


    //CONNECTIONSTRING
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


    //USED FOR INITIALSING THE MASTER PAGE
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceReportByFaculty.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region "General"
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " ", "COSCHNO");
            //objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "DISTINCT SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO>0", "SLOTTYPENO");

            ddlSession.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add("Please Select");
        ddlsemester.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsemester.DataSource = ds;
            ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlsemester.DataBind();
            ddlsemester.SelectedIndex = 0;
        }
    }


    #endregion "General"

    #region "Selected Index Changed"

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
     {
        if (ddlSchoolInstitute.SelectedIndex > 0)
        {

            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString() + " AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            }
            else
            {
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0  AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            }
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO = SM.SESSIONNO AND SM.COLLEGE_ID=CT.COLLEGE_ID)", "DISTINCT SM.SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3 AND SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND ISNULL(CT.CANCEL,0)=0", "SM.SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");

        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

        this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]));
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", " SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
        //objCommon.FillDropDownList(ddlSubjectType, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO=C.COURSENO INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and OC.SCHEMENO = " + ddlScheme.SelectedValue + " AND OC.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");

        if (ddlSem.SelectedIndex > 0)
        {
            //bind Subject name in drop down list
            objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue, "O.COURSENO");
            //ddlSubject.Focus();
        }

        ddlSubject.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        if (ddlSem.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SCHEMENO = " + ddlScheme.SelectedValue + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue + " AND C.SUBID  = " + ddlSubjectType.SelectedValue, "O.COURSENO");
            objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue, "O.COURSENO");

            //ddlSubject.Focus();
        }

        ddlSection.SelectedIndex = 0;
        // ddlSubject.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        //ddlSubjectType.SelectedIndex = 0;
        ddlStudents.SelectedIndex = 0;
        ddlSection.Focus();
        //if (ddlSem.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
        //}
        //ddlSection.Focus();


    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtTodate.Text = "";
        //txtPercentage.Text = "0";
        try
        {
            objCommon.FillDropDownList(ddlStudents, "ACD_STUDENT", " IDNO", "REGNO +' - '+ STUDNAME", "COLLEGE_ID = " + ViewState["college_id"] + "  AND SEMESTERNO  = " + ddlSem.SelectedValue + " AND SECTIONNO = " + ddlSection.SelectedValue + " and isnull(ADMCAN,0)=0", "REGNO");
            ddlStudents.SelectedIndex = 0;
            ddlSubject.Focus();

        }
        catch { }
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlStudents.SelectedIndex = 0;
        //txtFromDate.Text = "";
        //txtTodate.Text = "";
    }



    #endregion "Selected Index Changed"

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnAbsentStudentList_Click(object sender, EventArgs e)
    {
        try
        { 
            //if((txtFromDate.Text.ToString())<=txtTodate.Text)
           if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtTodate.Text))
            {
                if (rdoReportType.SelectedValue == "xls")
                {
                    //ShowReportinFormate(rdoReportType.SelectedValue, "rptAbsentStudentAttendance.rpt");
                    GridView gv = new GridView();
                    int COLLEGEID = Convert.ToInt32(ViewState["college_id"]);
                    int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    int Semesterno = Convert.ToInt32(ddlSem.SelectedValue);
                    int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
                    int Courseno = Convert.ToInt32(ddlSubject.SelectedValue);
                    int Idno = Convert.ToInt32(ddlStudents.SelectedValue);
                    string fromdate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
                    string Todate = Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd");

                    DataSet ds = objAttC.GetAcademicstudentListExcelReportData(COLLEGEID, Sessionno, Semesterno, Sectionno, Courseno, Idno, fromdate, Todate);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();

                        string Attachment = "Attachment; filename=" + "AbsentStudentListReport.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", Attachment);
                        Response.ContentType = "application/" + "ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gv.HeaderStyle.Font.Bold = true;
                        gv.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                else
               
                {
                    objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
                }
                }
                    else
                    {
                        ShowReport("Absent Student List", "rptAbsentStudentAttendance.rpt");
                    }
                }
              
                //else
                //{
                //    //function call to show the report
                //    ShowReport("Absent Student List", "rptAbsentStudentAttendance.rpt");
                //}
            
           else
           {
               objCommon.DisplayMessage(this.updSection, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
           }
        }
        catch
        {
        }
    }
    protected void btnAbsentStudentListReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtTodate.Text))
            {
                   if (rdoReportType.SelectedValue == "xls")
                   {
                    GridView gv = new GridView();
                    int COLLEGEID = Convert.ToInt32(ViewState["college_id"]);
                    int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    int Semesterno = Convert.ToInt32(ddlSem.SelectedValue);
                    int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
                    int Courseno = Convert.ToInt32(ddlSubject.SelectedValue);
                    int Idno = Convert.ToInt32(ddlStudents.SelectedValue);
                    string fromdate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
                    string Todate = Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd");

                    DataSet ds = objAttC.GetAcademicsubjectstudentwiseExcelReportData(COLLEGEID, Sessionno, Semesterno, Sectionno, Courseno, Idno, fromdate, Todate);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();

                        string Attachment = "Attachment; filename=" + "AbsentStudentListReport.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", Attachment);
                        Response.ContentType = "application/" + "ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gv.HeaderStyle.Font.Bold = true;
                        gv.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
                    }
                   }
                   else
                   {
                       ShowReport("Absent Student List", "rptAbsentStudentAttendanceNew.rpt");
                   }
            }
              
               

            else
            {
                objCommon.DisplayMessage(this.updSection, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
            }
        }
        catch
        {
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlSchoolInstitute.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlStudents.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlStudents.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentIncompleteAttendance.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            //string bname = objCommon.LookUp("acd_branch", "shortname", "branchno=" + Convert.ToInt32(ddlBranch.SelectedValue));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlSchoolInstitute.SelectedItem.Text + "_" + ddlSection.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlStudents.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlSchoolInstitute.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlStudents.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();           
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
