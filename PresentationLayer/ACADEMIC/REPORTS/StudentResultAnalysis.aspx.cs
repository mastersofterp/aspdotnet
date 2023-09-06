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
using System.Web.UI.WebControls;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;



public partial class ACADEMIC_REPORTS_StudentResultList : System.Web.UI.Page
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

                PopulateDropDownList();
                ExamWise();
                ddlClgname.Focus();

                //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE", "courseno", "course_name", "", "");

                //btnsemester.visible = false;
                //btninstitue.visible = false;
                //divscheme.visible = false;
                //divsection.visible = false;
                //btnmodelexam.visible = false;
                //btninternalmarkreg.visible = false;
                //btnconsolidatetestmarkreprt.visible = btnconsolidatedinternaltestmarkreport.visible = btnconsohrreport.visible = btnrank.visible = btncorrelationanalysis.visible = false;

                // ADDED BY NARESH BEERLA ON DT 28042022 AS PER THE ISSUE RELATED TO UA_DEPTNO

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

                if (Convert.ToInt32(Session["OrgId"]) == 2) // Crescent University
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        btnCategoryWise.Visible = true;
                        btnOverAllPercentage.Visible = true;
                        btnOverAllSubjectPercentage.Visible = true;
                        btnBranchSemAnalysis.Visible = true;
                        btnResultAnalysis.Visible = true;
                        btnFailStudentList.Visible = true;
                        btnCourceWiseFailStudList.Visible = true;
                        btnGetGpaReport.Visible = true;
                        btnBranchWiseResultAnalysis.Visible = false;
                        pre_fourteen.Visible = false;
                        btnCGPAReport.Visible = true;
                    }
                    else
                    {
                        btnCategoryWise.Visible = false;
                        btnOverAllPercentage.Visible = false;
                        btnOverAllSubjectPercentage.Visible = false;
                        btnBranchSemAnalysis.Visible = false;
                        btnResultAnalysis.Visible = false;
                        btnFailStudentList.Visible = false;
                        btnCourceWiseFailStudList.Visible = false;
                        btnGetGpaReport.Visible = false;
                        btnBranchWiseResultAnalysis.Visible = false;
                        pre_fourteen.Visible = false;
                        btnCGPAReport.Visible = true;
                    }
                }

                if (Convert.ToInt32(Session["OrgId"]) == 1) // R C Patel Institute
                {
                    btnFaculty.Visible = true;
                    btnAnalysis.Visible = true;
                    btnGradesheet.Visible = false;
                    btnGraderpt.Visible = false;
                    btnExamFeesPaid.Visible = false;
                    btnBranchWiseResultAnalysis.Visible = true;  //Button only visible for RCPIT 
                    pre_fourteen.Visible = true;
                    btnCGPAReport.Visible = false;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2) // Crescent University
                {
                    btnGraderpt.Visible = true;
                    btnFaculty.Visible = false;
                    btnAnalysis.Visible = false;
                    btnGradesheet.Visible = true;
                    btnExamFeesPaid.Visible = true;
                    btnBranchWiseResultAnalysis.Visible = false;
                    pre_fourteen.Visible = false;
                    btnCGPAReport.Visible = true;

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6) // RCPIPER
                {
                    btnGradesheet.Visible = false;
                    btnBranchWiseResultAnalysis.Visible = false;
                    btnCGPAReport.Visible = false;
                    btnGraderpt.Visible = false;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4) // Career Point University Kota/Career Point University Hamirpur
                {
                    btnGradesheet.Visible = false;
                    btnAnalysis.Visible = false;
                    btnGraderpt.Visible = false;
                    btnExamFeesPaid.Visible = false;
                    btnBranchWiseResultAnalysis.Visible = false;
                    btnCGPAReport.Visible = false;
                }
                else
                {
                    btnBranchWiseResultAnalysis.Visible = false;
                    pre_fourteen.Visible = false;
                    btnGraderpt.Visible = false;
                }
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

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 and COLLEGE_ID="+ Convert.ToInt32(Session["college_id"].ToString())+"","SESSIONNO DESC");


            //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 ", "SECTIONNO"); // Commented By Sagar Mankar 31082023

            // objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "DISTINCT EXAMNO", " EXAMNAME", "EXAMNO > 0 and PATTERNNO=1", "EXAMNO DESC");

            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME E, ACD_SCHEME S", "DISTINCT EXAMNO", " EXAMNAME", "EXAMNO > 0 and S.PATTERNNO=E.PATTERNNO AND S.OrganizationId=E.OrganizationId AND S.COLLEGE_CODE=E.COLLEGE_CODE  AND ISNULL(E.EXAMNAME,'')<>'' AND ISNULL(E.ACTIVESTATUS,0)=1", "EXAMNO DESC");

            // objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME E, ACD_EXAM_PATTERN P", "DISTINCT EXAMNO", " EXAMNAME", "EXAMNO > 0 and E.PATTERNNO=P.PATTERNNO ", "EXAMNO DESC");


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



    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ddlScheme.SelectedIndex = 0;
    //        ddlScheme.Items.Clear();
    //        ddlScheme.Items.Add(new System.Web.UI.WebControls.ListItem("Please Select", "0"));
    //        if (ddlBranch.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "branchno = " + ddlBranch.SelectedValue, "SCHEMENO");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlScheme.SelectedIndex > 0)
        //{
        //    ddlSem.Items.Clear();
        //    objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        //    ddlSem.Focus();
        //}
        //else
        //{

        //}
    }
    #endregion

    //private void ClearControls()
    //{
    //    ddlBranch.Items.Clear();
    //    ddlBranch.Items.Add(new System.Web.UI.WebControls.ListItem("Please Select", "0"));

    //}

    private void ExamWise()
    {
        divStudType.Visible = false;
        //btnSubWiseRslt.Visible = false;
        //btnStatistical.Visible = false;
        //btnSGPA.Visible = false;
        //btnSemester.Visible = false;
        //btnInstitue.Visible = false;

        divSession.Visible = true;
        //divClg.Visible = true;
        //divDegree.Visible = true;
        //divBranch.Visible = true;
        //divScheme.Visible = true;
        divSemester.Visible = true;
        divSection.Visible = true;
        divexam.Visible = true;
        divFromDate.Visible = true;
        divToDate.Visible = true;
        //btnInternalMarkReg.Visible = true;
        // btnModelExam.Visible = true;
        ClearAllDropDowns();

        //btnConsolidateTestMarkReprt.Visible = btnConsolidatedInternalTestMarkReport.Visible = btnConsoHrReport.Visible = btnRank.Visible = btnCorrelationAnalysis.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        ddlClgname.SelectedIndex = 0;
        ddlClgname.Focus();
        ddlSem.Items.Clear();
        ddlSem.Items.Add("Please Select");
        ddlSem.SelectedItem.Value = "0";
        ddlSession.Items.Clear();
        ddlSession.Items.Add("Please Select");
        ddlSession.SelectedItem.Value = "0";
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add("Please Select");
        ddlcourse.SelectedItem.Value = "0";


        ddlSection.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlStudType.SelectedIndex = 0;


    }

    private void ClearAllDropDowns()
    {
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        //ddlCollege.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new System.Web.UI.WebControls.ListItem("Please Select", "0"));
        ddlSection.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlStudType.SelectedIndex = 0;

        //ddlSection.Items.Clear();
        //ddlSection.Items.Add("Please Select");
        //ddlSection.SelectedItem.Value = "0";
        //ddlExam.Items.Clear();
        //ddlExam.Items.Add("Please Select");
        //ddlExam.SelectedItem.Value = "0";
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add("Please Select");
        ddlcourse.SelectedItem.Value = "0";
        //ddlStudType.Items.Clear();
        //ddlStudType.Items.Add("Please Select");
        //ddlStudType.SelectedItem.Value = "0";

        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND isnull(cancel,0)=0  and SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
            //objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR) As COURSENO", "C.CCODE+'-'+COURSE_NAME As COURSENAME", "R.SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND R.SCHEMENO=" + ViewState["schemeno"] + "", "COURSENO");
            //ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0 AND
            ddlSem.Focus();
        }
    }

    protected void btnInternalMarkReg_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ConsolidatedReportModel').modal('show');</script>", false);
        updPopUp_6.Update();
    }



    protected void btnConsolidatedInternalTestMarkReport_Click(object sender, EventArgs e)
    {
        //if (txtFromDate.Text == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select From Date');", true);
        //    txtFromDate.Focus();
        //    return;
        //}
        //else if (txtToDate.Text == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select To Date');", true);
        //    txtToDate.Focus();
        //    return;
        //}

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ConsolidatedModel_2').modal('show');</script>", false);
        updPopUp_2.Update();
    }




    protected void btnCATInternalMarks_Click(object sender, EventArgs e)
    {

        if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
            ddlSem.Focus();
            return;
        }
        else if (ddlSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
            ddlSection.Focus();
            return;
        }
        int StudType = Convert.ToInt32(ddlStudType.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlStudType.SelectedValue);
        int subType = 0;
        if (rbtnOpenEle.Checked)
            subType = 1;
        //if (ddlStudType.SelectedValue == "-1")
        //int StudType = 0;

        string reportTitle = "CAT_INTERNAL_MARK_REPORT";
        //string rptFileName = "rptConsolidatedInternalMarks.rpt";rptConsolidateTestMark_InternalNewReport
        string rptFileName = "rptConsolidateTestMark_InternalNewReport.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAMNO=" + ddlExam.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + ",@P_EXAM=" + ddlExam.SelectedValue;

            //",@P_SUB_TYPE=" + subType +
            //",@P_EXMTYPE=" + Convert.ToInt32(StudType) + 
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        //string reportTitle = "CAT_INTERNAL_MARK_REPORT";
        //string rptFileName = "rptConsolidatedInternalMarks.rpt";
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    //url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //    url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_EXMTYPE=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_SUB_TYPE=" + Convert.ToInt32(rbtnOpenEle.Checked) + ",@P_EXAMNO=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    divMsg.InnerHtml += " </script>";
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}


    }





    protected void lbtn_ConsoPrint_Internal_Click(object sender, EventArgs e)
    {
        string reportTitle = "CONSOLIDATED_INTERNAL_TEST_MARK_REPORT";
        string rptFileName = "rptConsolidateTestMark_Internal.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void lbtn_ConsoExcel_Internal_Click(object sender, EventArgs e)
    {
        //string SP_Name = "PKG_EXAM_CONSOLIDATE_TEST_MARK_REPORT_INTERNAL";
        //string SP_Parameters = "@P_SCHEMENO,@P_SEMESTERNO,@P_SESSIONNO,@P_SECTIONNO,@P_EXAM,@P_FROMDATE,@P_TODATE";
        //string Call_Values = ""+ddlScheme.SelectedValue+", "+ddlSem.SelectedValue+" , "+ddlSession.SelectedValue+" , "+ddlSection.SelectedValue+" , "+ddlExam.SelectedValue+"  ," + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + " ," + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + "";

        //DataSet ds = objCommon.DynamicSPCall(SP_Name, SP_Parameters, Call_Values);
        //ExportToExcel(ds, "Consolidated_Internal_Test_Mark");
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ConsolidatedModel_2').modal('hide');</script>", false);
        //updPopUp_2.Update();


        try
        {
            string exporttype = "xls";
            string rptFileName = "rptConsolidateTestMark_Internal_Without_Header.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Consolidated_Internal_Test_Mark.xls";
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + "";


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }



    //////////////////////////////////////////////////////////////////////////////////////////////////






    protected void lbtn_ConsoReport_Print_Click(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
            ddlSem.Focus();
            return;
        }
        else if (ddlSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
            ddlSection.Focus();
            return;
        }
        int StudType = Convert.ToInt32(ddlStudType.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlStudType.SelectedValue);
        int subType = 0;
        if (rbtnOpenEle.Checked)
            subType = 1;
        //if (ddlStudType.SelectedValue == "-1")
        //int StudType = 0;

        string reportTitle = "Internal_Mark_Register";
        string rptFileName = "rptInternalMarkRegister.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAMNO=1,@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString());

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lbtn_ConsoReport_Excel_Click(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester');", true);
            ddlSem.Focus();
            return;
        }

        try
        {
            int StudType = Convert.ToInt32(ddlStudType.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlStudType.SelectedValue);
            int subType = 0;
            if (rbtnOpenEle.Checked)
                subType = 1;

            string exporttype = "xls";
            string rptFileName = "rptInternalMarkRegister_Excel.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=InternalMarkRegister_Excel.xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAMNO=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //",@P_SUB_TYPE=" + subType +
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }







    /*
    private void ShowReportBlankTR(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_EXMTYPE=" + ddlStudType.SelectedValue;
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

    private void ShowReportResultAnalysis_Degreewise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList_Degreewise.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReporttAnalysis(string reportTitle, string rptFileName, int param)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (param == 1)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else if (param == 2)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else if (param == 3)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else if (param == 4)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            }          

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentResultList_Degreewise.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
    protected void btnAnalysis_Click(object sender, EventArgs e)
    {

        try
        {
            Report Show For BE-PTDP/MTECH students 
            ShowReportResultAnalysis_Degreewise("RESULT_DEGREEWISE", "rptresultanalysisReportForMtech.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList_Degreewise.btnAnalysis_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnStatistical_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportBlankTR("RESULT_ANALYSIS", "rptMaleFemaleCount.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnAnalysis_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        ddlBranch.Items.Clear();
    //        if (ddlDegree.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
    //            ddlBranch.Focus();
    //        }
    //        else
    //        {
    //            ddlDegree.SelectedIndex = 0;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

  

    protected void rblSelectCretiria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (rblSelectCretiria.SelectedValue == "1")
            //{

            //    divStudType.Visible = false;
            //    btnStatistical.Visible = true;
            //    btnSGPA.Visible = true;
            //    btnSemester.Visible = false;
            //    btnInstitue.Visible = false;
            //    btnInternalMarkReg.Visible = false;
            //    btnModelExam.Visible = false;

            //    divBranch.Visible = true;
            //    divDegree.Visible = true;
            //    divClg.Visible = true;
            //    divScheme.Visible = false;
            //    divSection.Visible = false;
            //    divexam.Visible = false;
            //    divFromDate.Visible = false;
            //    divToDate.Visible = false;

            //    btnConsolidateTestMarkReprt.Visible = btnConsolidatedInternalTestMarkReport.Visible = btnConsoHrReport.Visible = btnRank.Visible = btnCorrelationAnalysis.Visible = false;

            //    ClearAllDropDowns();
            //}
            //else if (rblSelectCretiria.SelectedValue == "2")
            //{

            //    btnSubWiseRslt.Visible = false;
            //    btnStatistical.Visible = false;
            //    btnSGPA.Visible = false;
            //    btnSemester.Visible = true;
            //    btnInstitue.Visible = false;
            //    btnInternalMarkReg.Visible = false;
            //    btnModelExam.Visible = false;

            //    divBranch.Visible = false;
            //    divStudType.Visible = false;
            //    divDegree.Visible = true;
            //    divClg.Visible = true;
            //    divScheme.Visible = false;
            //    divSection.Visible = false;
            //    divexam.Visible = false;
            //    divFromDate.Visible = false;
            //    divToDate.Visible = false;
            //    ClearAllDropDowns();

            //    btnConsolidateTestMarkReprt.Visible = btnConsolidatedInternalTestMarkReport.Visible = btnConsoHrReport.Visible = btnRank.Visible = btnCorrelationAnalysis.Visible = false;
            //}
            //else if (rblSelectCretiria.SelectedValue == "3")
            //{
            //    divStudType.Visible = false;
            //    btnSubWiseRslt.Visible = false;
            //    btnStatistical.Visible = false;
            //    btnSGPA.Visible = false;
            //    btnSemester.Visible = false;
            //    btnInstitue.Visible = true;
            //    btnInternalMarkReg.Visible = false;
            //    btnModelExam.Visible = false;

            //    divScheme.Visible = false;
            //    divBranch.Visible = false;
            //    divDegree.Visible = false;
            //    divClg.Visible = false;
            //    divScheme.Visible = false;
            //    divSection.Visible = false;
            //    divexam.Visible = false;
            //    divFromDate.Visible = false;
            //    divToDate.Visible = false;
            //    ClearAllDropDowns();

            //    btnConsolidateTestMarkReprt.Visible = btnConsolidatedInternalTestMarkReport.Visible = btnConsoHrReport.Visible = btnRank.Visible = btnCorrelationAnalysis.Visible = false;
            //}
            // if (rblSelectCretiria.SelectedValue == "4")
            //{
            //    divStudType.Visible = false;
            //    btnSubWiseRslt.Visible = false;
            //    btnStatistical.Visible = false;
            //    btnSGPA.Visible = false;
            //    btnSemester.Visible = false;
            //    btnInstitue.Visible = false;

            //    divSession.Visible = true;
            //    divClg.Visible = true;
            //    divDegree.Visible = true;
            //    divBranch.Visible = true;
            //    divScheme.Visible = true;
            //    divSemester.Visible = true;
            //    divSection.Visible = true;
            //    divexam.Visible = true;
            //    divFromDate.Visible = true;
            //    divToDate.Visible = true;
            //    btnInternalMarkReg.Visible = true;
            //    btnModelExam.Visible = true;
            //    ClearAllDropDowns();

            //    btnConsolidateTestMarkReprt.Visible = btnConsolidatedInternalTestMarkReport.Visible = btnConsoHrReport.Visible = btnRank.Visible = btnCorrelationAnalysis.Visible = true;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnSubWiseRslt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubWiseRslt_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReporttAnalysis("SubjectWise_Result_Analysis", "rptSubjectWiseResultAnalysis.rpt", 1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnSubWiseRslt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSGPA_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReporttAnalysis("SGPA Result Analysis", "rptSGPAResultAnalysis.rpt", 2);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnSGPA_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSemester_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReporttAnalysis("Semester Result Analysis", "rptSemesterWiseResultAnalysis.rpt", 3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnSemester_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnInstitue_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReporttAnalysis("College Result Analysis", "rptCollegeWiseResultAnalysis.rpt", 4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnInstitue_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
   
    protected void btnExamWiseReport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReportResultAnalysis_Examwise("EXAM_WISE_RESULT_ANALYSIS", "rptConsolidatedMarkReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList_Degreewise.btnAnalysis_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowReportResultAnalysis_Examwise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM="+Convert.ToInt32(ddlExam.SelectedValue)+"";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnConsolidateTestMarkReprt_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key","alert('Please Select From Date');", true);
            txtFromDate.Focus();
            return;
        }
        else if (txtToDate.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select To Date');", true);
            txtToDate.Focus();
            return;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ConsolidatedModel_1').modal('show');</script>", false);
        updPopUp_1.Update();
    }

   
    protected void lbtn_ConsoPrint_Click(object sender, EventArgs e)
    {
        string reportTitle = "CONSOLIDATED_TEST_MARK_REPORT";
        string rptFileName = "rptConsolidateTestMark.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lbtn_ConsoExcel_Click(object sender, EventArgs e)
    {
        //string SP_Name="PKG_EXAM_CONSOLIDATE_TEST_MARK_REPORT";
        //string SP_Parameters = "@P_SCHEMENO,@P_SEMESTERNO,@P_SESSIONNO,@P_SECTIONNO,@P_EXAM,@P_FROMDATE,@P_TODATE";
        //string Call_Values = "" + ddlScheme.SelectedValue + ", " + ddlSem.SelectedValue + " , " + ddlSession.SelectedValue + " , " + ddlSection.SelectedValue + " , " + ddlExam.SelectedValue + "  ," + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + " ," + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + "";

        //DataSet ds = objCommon.DynamicSPCall(SP_Name, SP_Parameters, Call_Values);
        //ExportToExcel(ds, "Consolidated_Test_Mark");
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ConsolidatedModel_1').modal('hide');</script>", false);
        //updPopUp_1.Update();

        try
        {
            string exporttype = "xls";
            string rptFileName = "rptConsolidateTestMark_Without_Header.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Consolidated_Test_Mark.xls";
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_EXMTYPE="+ddlStudType.SelectedValue+",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + "";


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
  

    void ExportToExcel(DataSet ds, string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView dgv = new GridView();
        dgv.DataSource = null;
        dgv.DataBind();
        dgv.DataSource = ds.Tables[0];
        dgv.DataBind();

        for (int i = 0; i < dgv.Rows.Count; i++)
        {
            dgv.Rows[i].Attributes.Add("class", "textmode");
        }
        dgv.RenderControl(hw);
        //style to format numbers to string
        string style = @"<style> .textmode { mso-number-format:\@;} </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btnConsoHrReport_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select From Date');", true);
            txtFromDate.Focus();
            return;
        }
        else if (txtToDate.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select To Date');", true);
            txtToDate.Focus();
            return;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ConsolidatedModel_3').modal('show');</script>", false);
        updPopUp_3.Update();
    }

    protected void lbtn_ConsoPrint_HR_Click(object sender, EventArgs e)
    {
        string reportTitle = "CONSOLIDATED_HOUR_WISE_TEST_MARK_REPORT";
        string rptFileName = "rptConsolidateTestMark_Hour.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lbtn_ConsoExcel_HR_Click(object sender, EventArgs e)
    {
        try
        {
            string exporttype = "xls";
            string rptFileName = "rptConsolidateTestMark_Hour_Without_Header.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Consolidated_Hour_Wise_Test_Mark.xls";
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXAM=" + Convert.ToInt32(ddlExam.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy/MM/dd") + "";


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRank_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#RankModel').modal('show');</script>", false);
        updPopUp_4.Update();
    }

    protected void lbtn_Rank_Print_Click(object sender, EventArgs e)
    {
        string reportTitle = "RankReport";
        string rptFileName = "RankGeneration.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lbtn_Rank_Excel_Click(object sender, EventArgs e)
    {

    }

   

    protected void btnModelExam_Click(object sender, EventArgs e)
    {
        //string reportTitle = "Model_Exam_Mark";
        //string rptFileName = "rptInternalMarkRegister_MODELEXAM.rpt";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Modelexam').modal('show');</script>", false);
        updPopUp_5.Update();
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

        //    //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    //divMsg.InnerHtml += " </script>";
        //    string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

        //    ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    protected void lbtn_model_Print_Click(object sender, EventArgs e)
    {
        string reportTitle = "Model_Exam_Mark";
        string rptFileName = "rptInternalMarkRegister_MODELEXAM.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_EXMTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lbtn_model_Excel_Click(object sender, EventArgs e)
    {
        try
        {
            string exporttype = "xls";
            string rptFileName = "rptInternalMarkRegister_MODELEXAM.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=InternalMarkRegister_ModelExam_Mark.xls";
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {           
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCorrelationAnalysis_Click(object sender, EventArgs e)
    {
       // ShowReport("CorrelationAnalysis", "rptCAT_ESE_Marks_Analysis");

        string reportTitle = "Correlation_Analysis";
        string rptFileName = "rptCAT_ESE_Marks_Analysis.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
       
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnOverallPercentage_Click(object sender, EventArgs e)
    {
        ShowReportOverall("OverAll_Percentage","rptOverAllPercentage.rpt");
    }

    private void ShowReportOverall(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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

    protected void btnOverallSubpercentage_Click(object sender, EventArgs e)
    {
        ShowOverallSubpercentage("Subject_OverAll_Percentage", "rptsubjOverallPercentage.rpt");
    }

    private void ShowOverallSubpercentage(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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

    private void ShowReportHandling(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubjectFaculty_Click(object sender, EventArgs e)
    {
        ShowReportHandling("Subject_Handling_Faculty", "rptSubjectHandlingFaculty.rpt");
    }

   
    
    protected void btnResultRemark_Click(object sender, EventArgs e)
    {
        string reportTitle = "StudentResultRemark";
        string rptFileName = "rptStudentResultRemark.rpt";
        try
        {
            string fromdate = "";
            string todate = "";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_FROMDATE=" + fromdate.ToString() + ",@P_TODATE=" + todate.ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }*/
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add("Please Select");
        ddlSem.SelectedItem.Value = "0";
        ddlSection.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlStudType.SelectedIndex = 0;
        //ddlSection.Items.Clear();
        //ddlSection.Items.Add("Please Select");
        //ddlSection.SelectedItem.Value = "0";
        //ddlExam.Items.Clear();
        //ddlExam.Items.Add("Please Select");
        //ddlExam.SelectedItem.Value = "0";
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add("Please Select");
        ddlcourse.SelectedItem.Value = "0";
        //ddlStudType.Items.Clear();
        //ddlStudType.Items.Add("Please Select");
        //ddlStudType.SelectedItem.Value = "0";


        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 and ISNULL(IS_ACTIVE,0)=1  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            }

            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";
            objCommon.DisplayMessage("Please Select " + lblDYddlColgScheme.Text + "", this.Page);
            ddlClgname.Focus();
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlExam.SelectedIndex = 0;
        ddlStudType.SelectedIndex = 0;

        //ddlExam.Items.Clear();
        //ddlExam.Items.Add("Please Select");
        //ddlExam.SelectedItem.Value = "0";

        //ddlcourse.Items.Clear();
        //ddlcourse.Items.Add("Please Select");
        //ddlcourse.SelectedItem.Value = "0";

        //ddlStudType.Items.Clear();
        //ddlStudType.Items.Add("Please Select");
        //ddlStudType.SelectedItem.Value = "0";

        if (ddlSection.SelectedIndex > 0)
        {
            // objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "EXAMNO > 0", "EXAMNO DESC");
            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME E INNER JOIN ACD_SCHEME S ON (S.PATTERNNO=E.PATTERNNO AND S.SCHEMENO= " + ViewState["schemeno"].ToString() + ")", "DISTINCT EXAMNO", " EXAMNAME", "EXAMNO > 0 AND ISNULL(E.EXAMNAME,'')<>'' AND ISNULL(E.ACTIVESTATUS,0)=1", "EXAMNO DESC"); // BY SACHIN A
            ddlExam.Focus();
        }
    }

    //added by prafull 010322
    protected void btnExelrpt_Click(object sender, EventArgs e)
    {

        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + "');", true);
            ddlSem.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Session.');", true);
            ddlSection.Focus();
            return;
        }
        else if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
            ddlSection.Focus();
            return;
        }
        //else if (ddlSection.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
        //    ddlSection.Focus();
        //    return;
        //}


        GridView GVStudData = new GridView();

        DataSet ds = objCommon.GetInternalMarksExcel(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStudData.DataSource = ds;
                GVStudData.DataBind();

                string attachment = "attachment;filename=InternalMarks.xls";
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
            objCommon.DisplayMessage("No Data Found", this.Page);
        }

    }
    protected void btnGraderpt_Click(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
            ddlSem.Focus();
            return;
        }
        else if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
            ddlSection.Focus();
            return;
        }
        else if (ddlStudType.SelectedIndex == -1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Student Type.');", true);
            ddlStudType.Focus();
            return;

        }

        //if (ddlStudType.SelectedValue == "-1")
        //int StudType = 0;

        string reportTitle = "Course_wise Grade";
        string rptFileName = "rptCourseWise_GradeForFaculty.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) +
                ",@P_UANO=" + Session["userno"] + ",@P_STUDENTTYPE=" + ddlStudType.SelectedValue;

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";@P_DEGREENO @P_STUDENTTYPE
            //divMsg.InnerHtml += " </script>";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void btnGradesheet_Click(object sender, EventArgs e)
    //{
    //    if (ddlSem.SelectedIndex == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
    //        ddlSem.Focus();
    //        return;
    //    }
    //    else if (ddlClgname.SelectedIndex == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select college name.');", true);
    //        ddlClgname.Focus();
    //        return;
    //    }
    //    else if (ddlSection.SelectedIndex == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
    //        ddlSection.Focus();
    //        return;
    //    }
    //    //else if (ddlStudType.SelectedIndex == 0)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Student Type.');", true);
    //    //    ddlStudType.Focus();
    //    //    return;

    //    //}

    //    //if (ddlStudType.SelectedValue == "-1")
    //    //int StudType = 0;

    //    string reportTitle = "Analysis Report";
    //    string rptFileName = "rptCourseWise_Result_Analysis.rpt";
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONO=" + Convert.ToInt32(ddlSection.SelectedValue) +
    //            ",@P_UA_NO=" + Session["userno"] + ",@P_UA_TYPE=" + Session["usertype"];

    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";@P_DEGREENO @P_STUDENTTYPE
    //        //divMsg.InnerHtml += " </script>";
    //        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }

    //}

    protected void btnGradesheet_Click(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
            ddlSem.Focus();
            return;
        }
        else if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select college name.');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Section.');", true);
            ddlSection.Focus();
            return;
        }
        if (Session["OrgId"] == "1")
        {
            string reportTitle = "Result Analysis";
            string rptFileName = "rptResultAnalysis.rpt";
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            string reportTitle = "Analysis Report";
            string rptFileName = "rptCourseWise_Result_Analysis_RCPIPER.rpt";
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONO=" + Convert.ToInt32(ddlSection.SelectedValue) +
                    ",@P_UA_NO=" + Session["userno"] + ",@P_UA_TYPE=" + Session["usertype"];

                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";@P_DEGREENO   @P_STUDENTTYPE
                //divMsg.InnerHtml += " </script>";
                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }


        }
        else
        {
            string reportTitle = "Analysis Report";
            string rptFileName = "rptCourseWise_Result_Analysis.rpt";
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONO=" + Convert.ToInt32(ddlSection.SelectedValue) +
                    ",@P_UA_NO=" + Session["userno"] + ",@P_UA_TYPE=" + Session["usertype"];

                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";@P_DEGREENO   @P_STUDENTTYPE
                //divMsg.InnerHtml += " </script>";
                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
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

    protected void btntrexcel_Click(object sender, EventArgs e)
    {

        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select college & scheme name.');", true);
            ddlSem.Focus();
            return;
        }
        else if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Session.');", true);
            ddlSection.Focus();
            return;
        }

        try
        {
            GridView GVTrReport = new GridView();
            string ContentType = string.Empty;

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);

            string SP_Name = "PKG_EXAM_MARKS_DETAILS_GRADE";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_UA_NO";
            string Call_Values = "" + Sessionno + "," + schemeno + "," + semesterno + "," + userno + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVTrReport.DataSource = ds;
                GVTrReport.DataBind();

                string attachment = "attachment; filename=TR_GradeReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVTrReport.RenderControl(htw);
                //lvStudApplied.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnExamFeesPaid_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVTrReport = new GridView();
            string ContentType = string.Empty;

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);

            string SP_Name = "PKG_ACD_GET_STUDENT_FEES_PAID_LIST_FOR_EXCEL_CLS_ADVISOR";
            string SP_Parameters = "@P_SESSIONNO,@P_UA_NO";
            string Call_Values = "" + Sessionno + "," + userno + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVTrReport.DataSource = ds;
                GVTrReport.DataBind();

                string attachment = "attachment; filename=ExaminationFeePaidExcel.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVTrReport.RenderControl(htw);
                //lvStudApplied.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentResultAnalysis.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCategoryWise_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        else if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSemester.Text + ".');", true);
            ddlSem.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int colcode = Convert.ToInt32(Session["colcode"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_ACD_GET_NHA_NSA_RESULT";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_SEMESTERNO";
            string Call_Values = "" + Sessionno + "," + degreeno + "," + semesterno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=StudentResultNSA_NHA_Report";
                url += "&path=~,Reports,Academic," + "NHA_NSAResultAnalysis.rpt";

                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;

                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

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
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnGradeCard_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnOverAllPercentage_Click(object sender, EventArgs e)
    {
        ShowReportOverall("OverAll_Percentage", "rptOverAllPercentage.rpt");
    }

    private void ShowReportOverall(string reportTitle, string rptFileName)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PRC_OVERALL_PASSPERCENTAGE_BATCHSEMESTER_WISE_NEW";
            string SP_Parameters = "@P_DEGREENO,@P_BRANCHNO";
            string Call_Values = "" + degreeno + "," + branchno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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

    protected void btnOverAllSubjectPercentage_Click(object sender, EventArgs e)
    {
        ShowOverallSubpercentage("Subject_OverAll_Percentage", "rptsubjOverallPercentage.rpt");
    }

    private void ShowOverallSubpercentage(string reportTitle, string rptFileName)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int SCHEMENO = Convert.ToInt32(ViewState["schemeno"]); // ddlClgname.SelectedValue
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_GET_OVERALL_PASS_PERCENTAGE_BATCH_WISE_2";
            //string SP_Name = "PKG_GET_OVERALL_PASS_PERCENTAGE_BATCH_WISE_2_BKUP_FA";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO";
            string Call_Values = "" + Sessionno + "," + SCHEMENO + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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
    protected void btnBranchSemAnalysis_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int SCHEMENO = Convert.ToInt32(ddlClgname.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_ACD_SEMESTER_WISE_PASS_COUNT";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO";
            string Call_Values = "" + Sessionno + "," + degreeno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + "Branch-Semesterwise Result Analysis";
                url += "&path=~,Reports,Academic," + "rptSemesterResultAnalysis.rpt";

                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREETYPE=" + Convert.ToInt32(ddlDegreeType.SelectedValue);
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_DEGREETYPE=" + Convert.ToInt32(ddlDegreeType.SelectedValue);
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"];

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
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnResultAnalysis_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportResult("Result Analysis Report", "rptGenderwiseResultAnalysis.rpt", 1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnGradeCard_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportResult(string reportTitle, string rptFileName, int reportno)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);
            int SCHEMENO = Convert.ToInt32(ddlClgname.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_ACD_GENDERWISE_RESULT_ANALYSIS";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_UA_NO";
            string Call_Values = "" + Sessionno + "," + degreeno + "," + userno + "";

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
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_DEGREENO=" + ViewState["degreeno"];
                }
                else if (reportno == 2)
                {
                    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
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
        catch
        {

        }
    }
    protected void btnFailStudentList_Click(object sender, EventArgs e)
    {
        ShowTR("FailStudentList", "rptStudentFailedList.rpt", 3);
    }

    private void ShowTR(string reportTitle, string rptFileName, int param)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_EXAM_STUDENT_FAILED_REPORT";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO";
            string Call_Values = "" + Sessionno + "," + degreeno + "," + branchno + "," + semesterno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                if (param == 1)
                {
                    //url += "&param=@P_COLLEGEID=" + ddlClgname.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",	@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (param == 2)
                {
                    //string abc = Session["colcode"].ToString();
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlClgname.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (param == 3)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
                }
                else if (param == 4)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
                }
                else if (param == 5)
                {
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlClgname.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    //private string GetIDNO()
    //{
    //    string retIDNO = string.Empty;
    //    foreach (ListViewDataItem item in lvStudent.Items)
    //    {
    //        CheckBox chk = item.FindControl("chkStudent") as CheckBox;
    //        Label lblStudname = item.FindControl("lblStudname") as Label;

    //        if (chk.Checked)
    //        {
    //            if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
    //            else
    //                retIDNO += "$" + lblStudname.ToolTip.ToString();
    //        }
    //    }

    //    if (retIDNO.Equals(""))
    //    {
    //        idno = 0;
    //        return "0";
    //    }
    //    else
    //    {
    //        idno = 1;
    //        return retIDNO;
    //    }
    //}

    protected void btnGetGpaReport_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(Session["OrgId"]) == 1)
        //{
        //    ShowGpaReport("GPA_CGPA", "rptSgpaCgpa_RCPIT.rpt");
        //}
        //else if (Convert.ToInt32(Session["OrgId"]) == 2)
        //{
        //    ShowGpaReport("GPA_CGPA", "rptSgpaCgpa_CRESCENT.rpt");
        //}
        //else
        //{
        //    ShowGpaReport("GPA_CGPA", "rptSgpaCgpa.rpt");
        //}

        ShowGpaReport("GPA_CGPA", "rptSgpaCgpa.rpt");
    }

    private void ShowGpaReport(string reportTitle, string rptFileName)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_GET_GPA_CGPA_BATCH_WISE";
            string SP_Parameters = "@P_BRANCHNO";
            string Call_Values = "" + branchno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_BRANCHNO=" + ViewState["branchno"];
                url += "&param=@P_BRANCHNO=" + ViewState["branchno"];
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

    protected void btnBranchWiseResultAnalysis_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(Session["OrgId"]) == 1)
        //{
        //    BranchWiseResultAnalysis("Branch Wise Result Analysis", "rptBranchWiseResultAnalysis_RCPIT.rpt");
        //}
        //else if (Convert.ToInt32(Session["OrgId"]) == 2)
        //{
        //    BranchWiseResultAnalysis("Branch Wise Result Analysis", "rptBranchWiseResultAnalysis_CRESCENT.rpt");
        //}
        //else
        //{
        //    BranchWiseResultAnalysis("Branch Wise Result Analysis", "rptBranchWiseResultAnalysis_AllClient.rpt");
        //}

        //BranchWiseResultAnalysis("Branch Wise Result Analysis", "rptBranchWiseResultAnalysis_AllClient.rpt");  //MADE CHANGE AS PER REQUIREMENT 28-04-23
        BranchWiseResultAnalysis("Branch Wise Result Analysis", "rptBranchWiseResultAnalysis.rpt");
    }

    private void BranchWiseResultAnalysis(string reportTitle, string rptFileName)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        else if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSemester.Text + ".');", true);
            ddlSem.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            //string SP_Name = "PKG_GET_ELIGIBLE_TO_GET_DEGREE";
            //string SP_Name = "PKG_BRANCH_WISE_RESULT_ANALYSIS_REPORT";
            //string SP_Name = "PKG_BRANCH_WISE_RESULT_ANALYSIS_REPORT_STATUS";
            string SP_Name = "PKG_BRANCH_WISE_RESULT_ANALYSIS_REPORT_STATUS_RCPIT";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_SEMESTERNO";
            string Call_Values = "" + Sessionno + "," + degreeno + "," + semesterno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_BRANCHNO=" + ViewState["branchno"];

                //if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 2)
                //{
                //    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + "";
                //}
                //else
                //{
                //    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
                //}

                //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + "";
                //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + "";
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + "";





                //,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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

    protected void btnCourceWiseFailStudList_Click(object sender, EventArgs e)
    {
        ShowTRFailedList("CoursewiseFailStudentList", "rptFGradeResultAnalysis.rpt", 4);
    }

    private void ShowTRFailedList(string reportTitle, string rptFileName, int param)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        else if (ddlcourse.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Course.');", true);
            ddlcourse.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int course = Convert.ToInt32(ddlcourse.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);

            string SP_Name = "PKG_ACD_GRADE_WISE_RESULT_ANALYSIS";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_COURSENO,@P_SCHEMENO";
            string Call_Values = "" + Sessionno + "," + degreeno + "," + branchno + "," + semesterno + "," + course + "," + schemeno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                if (param == 1)
                {
                    //url += "&param=@P_COLLEGEID=" + ddlClgname.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",	@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (param == 2)
                {
                    //string abc = Session["colcode"].ToString();
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlClgname.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (param == 3)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
                }
                else if (param == 4)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_COURSENO=" + ddlcourse.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ""; // @P_COLLEGE_CODE=" + Session["colcode"].ToString() + "
                }
                else if (param == 5)
                {
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlClgname.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStudType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    protected void btnFaculty_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        else if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSemester.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        //else if (ddlSection.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSection.Text + ".');", true);
        //    ddlSection.Focus();
        //    return;
        //}

        string reportTitle = "Faculty_Wise_Result_Analysis";
        string rptFileName = "rptResultAnalysisNew_Faculty.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);


            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";@P_DEGREENO @P_STUDENTTYPE
            //divMsg.InnerHtml += " </script>";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnAnalysis_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlClgname.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        else if (ddlSem.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSemester.Text + ".');", true);
            ddlSession.Focus();
            return;
        }
        else if (ddlSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSection.Text + ".');", true);
            ddlSection.Focus();
            return;
        }

        string reportTitle = "Result_Analysis";
        string rptFileName = "rptResultAnalysisNew.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);


            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";@P_DEGREENO @P_STUDENTTYPE
            //divMsg.InnerHtml += " </script>";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void AddReportHeader(GridView gv)  //Added by Tejas Thakre 12_06_2023
    //{
    //    try
    //    {


    //        GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell Header1Cell = new TableCell();


    //        Header1Cell.Text = "SGPA";
    //        Header1Cell.ColumnSpan = 10;
    //        Header1Cell.Font.Size = 11;
    //        Header1Cell.Font.Bold = true;

    //        Header1Cell.HorizontalAlign = HorizontalAlign.Center;
    //        HeaderGridRow1.Cells.Add(Header1Cell);
    //        gv.Controls[0].Controls.AddAt(0, HeaderGridRow1);

    //        gv.HeaderRow.Visible = true;
    //        HeaderGridRow1.Cells.Add(Header1Cell);


    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}


    protected void btnCGPAReport_Click(object sender, EventArgs e) //Added by Tejas Thakre 12_06_2023
    {

        try
        {

            if (ddlClgname.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select College & Scheme.');", true);
                ddlClgname.Focus();
                return;
            }
            else if (ddlSession.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Session.');", true);
                ddlSession.Focus();
                return;
            }
            else if (ddlSem.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester.');", true);
                ddlSem.Focus();
                return;
            }


            //int AdmBatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT ADMBATCH", "BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])));

            int AdmBatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_TRRESULT TR ON (TR.IDNO=S.IDNO) INNER JOIN RESULT_PUBLISH_DATA PD ON (PD.IDNO=TR.IDNO AND TR.SEMESTERNO=PD.SEMESTERNO AND TR.SESSIONNO=PD.SESSIONNO)", "DISTINCT ISNULL(S.ADMBATCH,0)", "S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "AND S.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "AND TR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

            string sp_procedure = "PKG_GET_GPA_CGPA_BATCH_WISE";
            string sp_parameters = "@P_ADMBATCH,@P_BRANCHNO,@P_SCHEMENO,@P_UA_NO";
            string sp_callValues = "" + AdmBatch + "," + Convert.ToInt32(ViewState["branchno"]) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(Session["userno"]) + "";



            DataSet dsMarkchk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (dsMarkchk != null && dsMarkchk.Tables[0].Rows.Count > 0)
            {

                GridView GV = new GridView();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    GV.DataSource = dsMarkchk;
                    GV.DataBind();

                    //AddReportHeader(GV);
                    string Attachment = "Attachment ; filename=CGPAReoprte.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", Attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.HeaderStyle.Font.Bold = true;
                    GV.HeaderStyle.Font.Name = "Times New Roman";
                    GV.RowStyle.Font.Name = "Times New Roman";
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION AC ON (SR.SECTIONNO=AC.SECTIONNO)", "DISTINCT AC.SECTIONNO", "AC.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND AC.SECTIONNO > 0 AND SR.SCHEMENO=" + ViewState["schemeno"], "AC.SECTIONNO"); // Added By Sagar Mankar 31082023
            objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR) As COURSENO", "C.CCODE+'-'+COURSE_NAME As COURSENAME", "R.SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND R.SCHEMENO=" + ViewState["schemeno"] + " AND R.SEMESTERNO=" + ddlSem.SelectedValue, "COURSENO");
            //ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0 AND
            ddlSection.Focus();
        }
        else
        {
            ddlcourse.Items.Clear();
            ddlcourse.Items.Add("Please Select");
            ddlcourse.SelectedItem.Value = "0";

            ddlSem.Focus();
        }
    }
}

