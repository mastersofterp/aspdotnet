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
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;
using ClosedXML.Excel;
//using Microsoft.Office.Interop.Excel;





public partial class ACADEMIC_EXAMINATION_TabulationChart : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();

    bool IsDataPresent = false;

    int flag;
    string ids = string.Empty;

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                // this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();

                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropdownList();
                ddlCollege.Focus();
            }

            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                tab_year.Visible = false;
                btncoursegrade.Visible = true;
                btngraderange.Visible = true;
                btnExcel.Visible = true;
                btnExamFeesPaid.Visible = true;
                btnConvocationExcelReport.Visible = false; //Added as per open ticket 54493

                btnResultStatistics.Visible = false;
                pre_eight.Visible = false;

                btnGradeRegister.Visible = false;
                btnGrace.Visible = false;
                Yearid.Visible = false;
                btnConsolidateGradeCard.Visible = false;
                Dateissue.Visible = true;
                DatePublish.Visible = true;
                btnConsoli.Visible = true;
                btnConsoliA4.Visible = true;
                btnLedgerReport.Visible = false;

                btnProgrssionrpt.Visible = false;
                pre_eleven.Visible = false;
                btnSRNo.Visible = true;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = false;

                divprint.Visible = true;    //Added on 07112023
                btnCertificate.Visible = true; //Added on 07112023
                btnConsolidtedMPHRAM.Visible = false;


            }
            else if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                tab_year.Visible = true;
                btncoursegrade.Visible = false;
                btngraderange.Visible = false;
                btnExcel.Visible = false;
                btnExamFeesPaid.Visible = false;
                btnConvocationExcelReport.Visible = false;

                btnResultStatistics.Visible = true;
                pre_eight.Visible = true;

                btnGradeRegister.Visible = true;
                btnGrace.Visible = true;
                Yearid.Visible = true;
                BindYear();
                btnConsolidateGradeCard.Visible = true;
                Dateissue.Visible = true;
                DatePublish.Visible = false;
                btnConsoli.Visible = false;
                btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;

                btnProgrssionrpt.Visible = true;
                pre_eleven.Visible = true;
                btnSRNo.Visible = false;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = true;
                btnLedger.Visible = true; // ADDED BY SHUBHM ON 18-08-2023
                btnGradeCardIssueRegister.Visible = true;
                // divYear.Visible = true;
                btnElibilityReport.Visible = true;
                btnConsolidtedMPHRAM.Visible = false;
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 8)
            {
                tab_year.Visible = true;
                btnResultStatistics.Visible = false;
                pre_eight.Visible = false;

                btnLedgerReport.Visible = true;
                btnConsoli.Visible = false;
                btnConsoliA4.Visible = false;
                btnConsolidateGradeCard.Visible = true;
                btnProgrssionrpt.Visible = false;
                pre_eleven.Visible = false;

                btnCount.Visible = true;
                DatePublish.Visible = false;
                btnSRNo.Visible = false;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = false;
                btnConsolidtedMPHRAM.Visible = false;


            }
            else if (Convert.ToInt32(Session["OrgId"]) == 5)
            {
                tab_year.Visible = true;
                btncoursegrade.Visible = false;
                btngraderange.Visible = false;
                btnExcel.Visible = false;
                btnExamFeesPaid.Visible = false;
                btnConvocationExcelReport.Visible = false;
                Yearid.Visible = false;
                btnConsolidateGradeCard.Visible = false;
                Dateissue.Visible = false;
                btnConsoli.Visible = false;
                btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;

                btnProgrssionrpt.Visible = false;
                pre_eleven.Visible = false;

                DatePublish.Visible = false;

                btnResultStatistics.Visible = false;
                pre_eight.Visible = false;
                btnSRNo.Visible = false;
                btnGradesheet.Visible = true;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = true;
                lblScrutinized.Visible = true;
                btnufm.Visible = false;
                btnConsolidtedMPHRAM.Visible = false;

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 3)
            {
                btnProvisionalDegree.Visible = true;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = false;
                btnConsolidtedMPHRAM.Visible = false;

            }
            #region For RCPIPER Grade Card added on 25/07/2023 by shubham
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                Yearid.Visible = true;
                btnConsolidateGradeCard.Visible = true;
                Dateissue.Visible = true;
                tab_year.Visible = true;
                BindYear();
                btncoursegrade.Visible = false;
                btngraderange.Visible = false;
                btnExcel.Visible = false;
                btnExamFeesPaid.Visible = false;
                btnConvocationExcelReport.Visible = false;
                btnConsoli.Visible = false;
                btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;
                btnProgrssionrpt.Visible = false;
                pre_eleven.Visible = false;
                DatePublish.Visible = false;
                btnResultStatistics.Visible = false;
                pre_eight.Visible = false;
                btnSRNo.Visible = false;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = false;
                btnConsolidtedMPHRAM.Visible = true;//added by tejas thakre as on 16-12-2023
            }
            #endregion
            #region For ADCET added on 22/01/2024 by Tejas as on 22-01-2024
            else if (Convert.ToInt32(Session["OrgId"]) == 22)
            {
                btnCount.Visible = true;
                tab_year.Visible = true;
                btncoursegrade.Visible = false;
                btngraderange.Visible = false;
                btnExcel.Visible = false;
                btnExamFeesPaid.Visible = false;
                btnConvocationExcelReport.Visible = false;
                Yearid.Visible = false;
                btnConsolidateGradeCard.Visible = false;
                Dateissue.Visible = false;
                btnConsoli.Visible = false;
                btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;

                btnProgrssionrpt.Visible = false;
                pre_eleven.Visible = false;

                DatePublish.Visible = false;

                btnResultStatistics.Visible = false;
                pre_eight.Visible = false;
                btnSRNo.Visible = false;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = false;
                btnConsolidtedMPHRAM.Visible = false;

            }
            #endregion
            else
            {
                tab_year.Visible = true;
                btncoursegrade.Visible = false;
                btngraderange.Visible = false;
                btnExcel.Visible = false;
                btnExamFeesPaid.Visible = false;
                btnConvocationExcelReport.Visible = false;
                Yearid.Visible = false;
                btnConsolidateGradeCard.Visible = false;
                Dateissue.Visible = false;
                btnConsoli.Visible = false;
                btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;

                btnProgrssionrpt.Visible = false;
                pre_eleven.Visible = false;

                DatePublish.Visible = false;

                btnResultStatistics.Visible = false;
                pre_eight.Visible = false;
                btnSRNo.Visible = false;
                btnProvisionalDegree.Visible = false;
                txtScrutinized.Visible = false;
                lblScrutinized.Visible = false;
                btnufm.Visible = false;
                btnConsolidtedMPHRAM.Visible = false;


            }
        }

        //ddlYear.CssClass = "mytest";
    }

    #endregion

    #region Other Events


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  ClearAllDropDowns();
            //ClearPanel();
            ddlBranch.Items.Clear();
            // ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                //ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStuType.SelectedIndex = 0;

            txtDateOfIssue.Text = string.Empty;
            txtDeclareDate.Text = string.Empty;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudent.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));

            // ClearAllDropDowns();
            if (ddlBranch.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedValue == "99")
                //    objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", " SCHEMETYPE = 1 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "schemename");
                //else
                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                //ddlScheme.Focus();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT_HIST SR WITH (NOLOCK), ACD_SEMESTER S WITH (NOLOCK)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
                ddlSemester.SelectedIndex = 0;
            }
            ddlStuType.SelectedIndex = 0;
            txtDateOfIssue.Text = string.Empty;
            txtDeclareDate.Text = string.Empty;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudent.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlStuType.Focus();
            ddlStuType.SelectedIndex = 0;
            txtDateOfIssue.Text = string.Empty;
            txtDeclareDate.Text = string.Empty;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudent.Visible = false;
            //ddlStuType.Items.Clear();
            //ddlStuType.Items.Add(new ListItem("Please Select", ""));
            //ddlStuType.Items.Add(new ListItem("Regular", "0"));
            //ddlStuType.Items.Add(new ListItem("Backlog", "1"));
            //ddlStuType.Items.Add(new ListItem("Revaluation", "2"));


            //   Bindlist();

            //if (ddlBranch.SelectedValue == "99")
            //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SC.SECTIONNO > 0 AND SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
            //else
            //      objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SC.SECTIONNO > 0 AND SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Click Events
    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    SummaryResultSheet("SummaryResultSheet", "rptSummaryResultSheet.rpt"); 
    //}

    public void BindYear()
    {
        try
        {

            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", " YEAR", "YEAR > 0", "");
            //ddlYear.Focus();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.BindYear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ddlCollege.Focus();
    }
    #endregion

    #region User Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
        }
    }

    private void ShowDate()
    {

        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
        DataSet ds = null;
        ////ds = objCommon.FillDropDown("ACD_TRRESULT TR INNER JOIN ACD_STUDENT S ON(TR.IDNO = S.IDNO)", "RESULT_DECLARED_DATE", " ", "TR.SESSIONNO=" + ddlSession.SelectedValue + " and S.COLLEGE_ID=" + ddlCollege.SelectedValue + " and S.DEGREENO =" + ddlDegree.SelectedValue + "and S.BRANCHNO=" + ddlBranch.SelectedValue + " and TR.SEMESTERNO=" + ddlSemester.SelectedValue + "", "");        
        ds = objCommon.FillDropDown("ACD_TRRESULT TR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON(TR.IDNO = S.IDNO)", "RESULT_DECLARED_DATE", "TR_RESULT_NO", "TR.SESSIONNO=" + ddlSession.SelectedValue + " and S.COLLEGE_ID=" + ViewState["college_id"] + " and S.DEGREENO =" + ViewState["degreeno"] + "and S.BRANCHNO=" + ViewState["branchno"] + " and TR.SEMESTERNO=" + ddlSemester.SelectedValue + "", "");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDeclareDate.Text = ds.Tables[0].Rows[0]["RESULT_DECLARED_DATE"].ToString();
            }
            else
            {
                txtDeclareDate.Text = string.Empty;
            }
            if (txtDeclareDate.Text != string.Empty)
            {
                lblmsg.Text = "For this criteria result already declared on mentioned date.";
            }
        }
    }

    private void FillDropdownList()
    {

        //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"]) + "))", "");
        //Fill College&Scheme Dropdown
        //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");

        //**objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"]) + ") OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");


        string deptno = string.Empty;
        if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
            deptno = "0";
        else
            deptno = Session["userdeptno"].ToString();

        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");



        //Fill Dropdown session 
        //**objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc"); //--AND FLOCK = 1
        ////objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ") ", "C.COLLEGE_ID");
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_ID");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlYears, "ACD_YEAR WITH (NOLOCK)", "YEAR", "YEARNAME", "YEAR > 0 and isnull(ACTIVESTATUS,0)=1", "YEAR");
        //objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
    }

    private void ClearAllDropDowns()
    {
        //ddlScheme.Items.Clear();
        //ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
    }

    private void ClearPanel()
    {
        //pnlStudents.Visible = false;
        //lvCourse.Visible = false;
        //divMsg.InnerHtml = string.Empty;
        //btnSave.Visible = false;
    }

    private void ShowReport(string reportTitle, string rptFileName, string idno)
    {
        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString() + ",@P_DEGREE=" + ddlDegree.SelectedItem.Text;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString() + ",@P_DEGREE=" + ViewState["degreeno"];
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

    }
    string admbatch = string.Empty;
    private void Bindlist()
    {


        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
        DataSet ds = null;
        if (ddlSession.SelectedIndex > 0 && ddlSemester.SelectedIndex > 0 && Convert.ToInt32(ViewState["branchno"]) > 0 && Convert.ToInt32(ViewState["branchno"]) > 0)
        {
            //Regular

            // ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R INNER JOIN ACD_STUDENT T ON (T.IDNO = R.IDNO)  INNER JOIN ACD_SCHEME S ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR ON(TR.IDNO=R.IDNO)", "DISTINCT R.IDNO", "DBO.FN_DESC('NAME',R.IDNO)STUDNAME,R.REGNO", "R.IDNO <> 0 AND R.SESSIONNO =" + ddlSession.SelectedValue + " AND R.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue + "and (T.CAN=0 OR T.CAN IS NULL)  AND R.EXAM_REGISTERED=1 and RESULT_TYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + ddlCollege.SelectedValue + " or T.COLLEGE_ID= 0 ) " + " ", "R.REGNO");// AND R.PREV_STATUS=" + ddlStuType.SelectedValue + "
            //ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R INNER JOIN ACD_STUDENT T ON (T.IDNO = R.IDNO)  INNER JOIN ACD_SCHEME S ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR ON(TR.IDNO=R.IDNO)", "DISTINCT R.IDNO", "T.STUDNAME,R.REGNO,T.ADMBATCH,T.EMAILID_INS", "TR.IDNO <> 0 AND TR.SESSIONNO =" + ddlSession.SelectedValue + " AND TR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue + "and (T.CAN=0 OR T.CAN IS NULL)  AND R.EXAM_REGISTERED=1 and R.PREV_STATUS=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + ddlCollege.SelectedValue + " or T.COLLEGE_ID= 0 ) " + " ", "R.REGNO");
            // hdnadmbatch = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R WITH (NOLOCK) INNER JOIN ACD_STUDENT T WITH (NOLOCK) ON (T.IDNO = R.IDNO) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=R.IDNO AND R.SESSIONNO = TR.SESSIONNO  AND R.SEMESTERNO = TR.SEMESTERNO)", "DISTINCT R.IDNO", "T.STUDNAME,R.REGNO,T.ADMBATCH,T.EMAILID_INS", "TR.IDNO <> 0 AND TR.SESSIONNO =" + ddlSession.SelectedValue + " AND TR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND R.schemeno=" + Convert.ToInt32(ViewState["schemeno"]) + " AND R.EXAM_REGISTERED=1 and R.PREV_STATUS=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " or T.COLLEGE_ID= 0 ) " + " ", "R.REGNO");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    pnlStudent.Visible = true;
                    foreach (ListViewDataItem lvHead in lvStudent.Items)
                    {
                        HiddenField hdn_fld = lvHead.FindControl("hdnadmbatch") as HiddenField;
                        stuadmbatch.Value = hdn_fld.Value;
                    }
                    //ShowDate();
                    Session["studcnt"] = ds.Tables[0].Rows.Count;   //Added dt on 06102023
                }
                else
                    if (ddlClgname.SelectedIndex == 0)
                    {
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        pnlStudent.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnlExam, "No Record Found For Current Selection!!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        pnlStudent.Visible = false;
                        lblmsg.Visible = false;
                    }
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlStudent.Visible = false;
                objCommon.DisplayMessage(this.updpnlExam, "Error!", this.Page);

            }
        }
    }

    //private void GenerateQrCode()
    //{
    //    string idno = GetIDNO();
    //    //string RegNo = txtEnrollmentSearch.Text.ToString();
    //    //string idno = objCommon.LookUp("ACD_STUDENT", "idno", "REGNO='" + RegNo + "' AND ISNULL(ADMCAN,0)=0");
    //    if (!string.IsNullOrEmpty(idno))
    //    {
    //        int Idno = Convert.ToInt32(idno);

    //        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON S.DEGREENO=D.DEGREENO INNER JOIN ACD_BRANCH B ON S.BRANCHNO=B.BRANCHNO", "DEGREENAME,LONGNAME BRANCH,*", "", "REGNO='" + RegNo + "'", "REGNO");

    //        //string BranchName = objCommon.LookUp("ACD_BRANCH","SHORTNAME","BRANCHNO="+ ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Trim()+"");
    //        //SELECT @V_DURATION = DURATION FROM ACD_COLLEGE_DEGREE_BRANCH WHERE COLLEGE_ID = @V_COLLEGE_ID AND DEGREENO = @V_DEGREENO AND BRANCHNO = @V_BRANCHNO
    //        int Duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO=" + ds.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND BRANCHNO=" + ds.Tables[0].Rows[0]["BRANCHNO"].ToString()));
    //        int finalSemester = Duration * 2;
    //        DataSet ds1 = objQrC.GetStudentResultData(Idno);
    //        //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";

    //        string Qrtext = "Student Name:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Enrollment No.:" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; Degree:" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString().Trim() + "; Branch:" + ds.Tables[0].Rows[0]["BRANCH"].ToString().Trim() + "; CGPA:" +
    //            // ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS1"].ToString().Trim() + "; S2=" +
    //            // ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS2"].ToString().Trim() + "; S3=" +
    //             ds1.Tables[0].Rows[0]["CGPA"].ToString().Trim();
    //        //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS4"].ToString().Trim() + "; S5=" +
    //        //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS5"].ToString().Trim() + "; S6=" +
    //        //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS6"].ToString().Trim() + "; S7=" +
    //        //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS7"].ToString().Trim() + "; S8=" +
    //        //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS8"].ToString().Trim() + "; S9=" +
    //        //ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS9"].ToString().Trim() + ";";
    //        Session["qr"] = Qrtext.ToString();

    //        QRCodeEncoder encoder = new QRCodeEncoder();
    //        encoder.QRCodeVersion = 10;
    //        Bitmap img = encoder.Encode(Session["qr"].ToString());


    //        string imagepath = Server.MapPath("~/") + @"img.Jpeg";
    //        img.Save(imagepath);

    //        ViewState["File"] = imageToByteArray(imagepath);
    //        byte[] QR_IMAGE = ViewState["File"] as byte[];
    //        long ret = objQrC.AddUpdateQrCode(Idno, QR_IMAGE);
    //    }
    //    else
    //    {
    //        //objCommon.DisplayMessage(this.updpnlExam, txtEnrollmentSearch.Text.Trim() + " details not found Please check Admission Status.", this);
    //    }
    //}

    public void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlStuType.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        txtDateOfIssue.Text = "";
        txtDeclareDate.Text = "";
        ddlgradecardfor.SelectedIndex = 0;
    }
    private string GetIDNO()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            if (chk.Checked)
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "$" + lblStudname.ToolTip.ToString();
            }
        }
        if (retIDNO.Equals("")) return "0";
        else return retIDNO;
        //return retIDNO;
    }


    private string GetIDNO1()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            if (chk1.Checked)
            {
                //if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                //else
                //    retIDNO += "$" + lblStudname.ToolTip.ToString();
                retIDNO = "0";
            }
            else if (chk.Checked)
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "$" + lblStudname.ToolTip.ToString();
            }
        }
        if (retIDNO.Equals("")) return "0";
        else return retIDNO;
    }


    #region  using datatable xml by Injamam Ansari


    private DataTable Add_Datatable_IDNO()
    {
        DataTable dt = CreateDatatable_IDNO();
        int count = 0, count1 = 0;
        try
        {

            int rowIndex = 0;
            foreach (var item in lvStudent.Items)
            {
                count++;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;
                if (chk.Checked)
                {
                    DataRow dRow = dt.NewRow();
                    count1++;
                    dRow["IDNO"] = lblStudname.ToolTip.ToString();

                    rowIndex += 1;
                    dt.Rows.Add(dRow);
                }
                //Do stuff
            }
            //if (count == count1)
            //{
            //    DataRow dRow = dt.NewRow();
            //    dt.Rows.Clear();
            //    dRow["IDNO"] = "0";
            //    dt.Rows.Add(dRow);
            //}
            //else if (dt.Rows.Count == 0)
            //{
            //    DataRow dRow = dt.NewRow();
            //    dRow["IDNO"] = "0";
            //    dt.Rows.Add(dRow);
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.Add_Datatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }

    private DataTable CreateDatatable_IDNO()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "Student_IDNO";
            dt.Columns.Add("IDNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.CreateDatatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }

    #endregion


    private string GetIDNOFGenerateGradeNo()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            if (chk.Checked)
            {
                //if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                // else
                retIDNO += lblStudname.ToolTip.ToString() + "$";
            }
        }
        if (retIDNO.Equals("")) return "0";
        else return retIDNO;
        //return retIDNO;
    }
    #endregion

    protected void btnGrade_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = GetIDNO();
            if (idno == "")
            {
                objCommon.DisplayMessage(updpnlExam, "Please Select At least one Student!!", this.Page);
                return;
            }
            else
            {
                int degreeno = Convert.ToInt32(ViewState["degreeno"]);//Convert.ToInt32(ddlDegree.SelectedValue);
                int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                //          B.tech                                  M.Tech.(App. Geology)           B. Arch.                        MCA                                 M. tech
                if ((degreeno == 1 && semesterno == 8) || (degreeno == 2 && semesterno == 4) || (degreeno == 3 && semesterno == 10) || (degreeno == 4 && semesterno == 6) || (degreeno == 5 && semesterno == 4))
                    ShowReport("Grade_Card_Report", "rptTabulationRegistarMrk.rpt", idno == "" ? "0" : idno);
                else
                    ShowReport("Grade_Card_Report_Semester", "rptGradeCard_Semester.rpt", idno == "" ? "0" : idno);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.btnGrade_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    //sending from mail aayushi gupta
    protected void btnSendEmail_Click1(object sender, EventArgs e)
    {
        {
            try
            {
                DataSet d = new DataSet();
                string studentIds = string.Empty;
                ReportDocument customReport = new ReportDocument();
                //DataSet ds = objCommon.FillDropDown("Reff", "EMAILSVCID", "EMAILSVCPWD",string.Empty,string.Empty);
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    HiddenField hdfuserno = item.FindControl("hidIdNo") as HiddenField;
                    HiddenField hdfAppli = item.FindControl("hdfAppliid") as HiddenField;
                    HiddenField Hdfemail = item.FindControl("Hdfemail") as HiddenField;
                    HiddenField HiddenFieldenroll = item.FindControl("HiddenFieldenroll") as HiddenField;

                    if (chk.Checked == true && chk.Enabled == true)
                    {
                        if (ddlgradecardfor.SelectedIndex == 1)
                        {
                            studentIds += hdfuserno.Value + "$";

                            string reportPath = Server.MapPath(@"~,Reports,Academic, .rpt".Replace(",", "\\"));

                            customReport.Load(reportPath);
                        }

                        else if (ddlgradecardfor.SelectedIndex == 2)
                        {
                            studentIds += hdfuserno.Value + "$";

                            string reportPath = Server.MapPath(@"~,Reports,Academic,rptGradeCardReportPGNEW.rpt".Replace(",", "\\"));

                            customReport.Load(reportPath);
                        }
                        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                        ConnectionInfo crConnectionInfo = new ConnectionInfo();
                        Tables CrTables;

                        crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
                        crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                        crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                        crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                        CrTables = customReport.Database.Tables;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                        {
                            crtableLogoninfo = CrTable.LogOnInfo;
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                            CrTable.ApplyLogOnInfo(crtableLogoninfo);
                        }



                        customReport.SetParameterValue("@P_COLLEGEID", Convert.ToInt32(ddlCollege.SelectedValue));
                        customReport.SetParameterValue("@P_SESSIONNO", Convert.ToInt32(ddlSession.SelectedValue));
                        customReport.SetParameterValue("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedValue));
                        customReport.SetParameterValue("@P_BRANCHNO", Convert.ToInt32(ddlBranch.SelectedValue));
                        customReport.SetParameterValue("@P_SEMESTERNO", Convert.ToInt32(ddlSemester.SelectedValue));
                        customReport.SetParameterValue("@P_ADMBATCHNO", stuadmbatch.Value);
                        customReport.SetParameterValue("@P_RESULTDECLAREDDATE", txtDeclareDate.Text.ToString());
                        customReport.SetParameterValue("@P_DATE_OF_ISSUE", txtDateOfIssue.Text.ToString());
                        customReport.SetParameterValue("@P_IDNO", hdfuserno.Value);
                        customReport.SetParameterValue("@P_USER_FUll_NAME", Session["userfullname"].ToString());
                        customReport.SetParameterValue("@P_COLLEGE_CODE", 0);

                        string path = Server.MapPath("~/GradeCardReport\\");
                        if (!(Directory.Exists(path)))
                            Directory.CreateDirectory(path);
                        customReport.ExportToDisk(ExportFormatType.PortableDocFormat, path + hdfAppli.Value + ".pdf");


                        DataSet ds = objCommon.FillDropDown("REFF WITH (NOLOCK)", "EMAILSVCID", "EMAILSVCPWD", string.Empty, string.Empty);
                        var fromAddress = ds.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                        //const string fromPassword = "MUadmission2016";
                        string fromPassword = ds.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                        if (Hdfemail.Value == "")
                        {
                            objCommon.DisplayMessage("Kindly check Email Id .", this.Page);
                        }
                        else
                        {
                            string EmailTemplate = "<html><body>" +
                                                "<div align=\"center\">" +
                                                "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                                 "<tr>" +
                                                 "<td>" + "</tr>" +
                                                 "<tr>" +
                                                "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><B>Regards,<br/><br/><b>Controller Of Examination <br/>Indus University</td>" +
                                                "</tr>" +
                                                "</table>" +
                                                "</div>" +
                                                "</body></html>";
                            StringBuilder mailBody = new StringBuilder();
                            mailBody.AppendFormat("<h1>Greetings !!</h1>");
                            mailBody.AppendFormat("Dear " + objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_FULLNAME", "UA_IDNO=" + hdfuserno.Value));
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            //mailBody.AppendFormat("<p>Your Admit Card is Generated.</p>");
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                            string sessionnoname = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSION_NAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                            mailBody.AppendFormat("<p>Please find the following attachment for the Grade Card Report of END-SEM Examination <b>" + sessionnoname + "</b> .</p>");
                            mailBody.AppendFormat("<br />");
                            string Mailbody = mailBody.ToString();
                            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                            //msg.From = new MailAddress(HttpUtility.HtmlEncode(sendersemailid));
                            msg.From = new MailAddress(HttpUtility.HtmlEncode(fromAddress));
                            msg.To.Add(Hdfemail.Value);
                            msg.Body = nMailbody;
                            msg.Attachments.Add(new Attachment(path + hdfAppli.Value + ".pdf"));
                            msg.IsBodyHtml = true;
                            msg.Subject = "Grade Card For ESE " + sessionnoname + " Result:-" + HiddenFieldenroll.Value;
                            SmtpClient smt = new SmtpClient();
                            smt.Host = "smtp.gmail.com";
                            smt.Port = 587;
                            smt.UseDefaultCredentials = true;
                            smt.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);

                            smt.EnableSsl = true;
                            // smt.Send(msg);
                            //SmtpClient smt = new SmtpClient("smtp.gmail.com");
                            //smt.Port = 587;
                            //smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromAddress), HttpUtility.HtmlEncode(fromPassword));
                            //smt.EnableSsl = true;
                            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                            smt.Send(msg);
                            objCommon.DisplayMessage(Page, "Mail Sent Successfully For Selected Student(s)!!", this);
                            //string script = "<script>alert('Mail Sent Successfully')</script>";
                            //ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
                            msg.Attachments.Dispose();


                            // BindListView();
                        }
                        if (File.Exists(path + hdfAppli.Value + ".pdf"))
                        {
                            File.Delete(path + hdfAppli.Value + ".pdf");
                        }

                    }

                }
                if (studentIds.Equals(""))
                {
                    objCommon.DisplayMessage("Please Select at least one Student!", this.Page);

                }

                clear();
                lvStudent.DataSource = null;
                lvStudent.DataBind();

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "AdmitCard.btnSendSMS_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }

        }
    }
    private void ShowReport_GradeCard(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowGradeCardFinal(string reportTitle, string rptFileName)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + "";
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

    protected void btnGradeCard_Click1(object sender, EventArgs e)
    {


        #region RCPIT Grad Card
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            ids = ids.TrimEnd('.');

            this.ShowGradeCardNew("Grade Card", "MarksGrade_RCPIT.rpt", ids);

        }
        #endregion
        #region For Crescent

        else if (Convert.ToInt32(Session["OrgId"]) == 2)
        {

            //string ids = string.Empty;
            //foreach (ListViewDataItem item in lvStudent.Items)
            //{
            //    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            //    Label lblStudname = item.FindControl("lblStudname") as Label;

            //    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
            //    if (chk.Checked)
            //    {
            //        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

            //        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
            //    }
            //}


            //if (ddlStuType.SelectedIndex < 0)
            //{
            //    objCommon.DisplayMessage("Please Select Student Type", this.Page);
            //}

            //MarksEntryController objMarkEntry = new MarksEntryController();
            //int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int College_id = Convert.ToInt32(ViewState["college_id"]);
            //int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            //int Branchno = Convert.ToInt32(ViewState["branchno"]);
            //int ua_no = Convert.ToInt32(Session["userno"].ToString());
            //int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //string idnos = GetIDNOFGenerateGradeNo();
            //objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
            //int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            //if (duration == Semesterno)
            //{
            //    this.ShowGradeCard("Grade_Card", "rptGradeCardFinalReportPG.rpt");
            //}
            //else
            //{
            //    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG.rpt");
            //}

            //added on 05122022

            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }


            if (ddlStuType.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select Student Type", this.Page);
            }

            MarksEntryController objMarkEntry = new MarksEntryController();
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            string idnos = GetIDNOFGenerateGradeNo();
            //objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            //  int NEW_BRANCH = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO",  "BRANCHNO =" + Convert.ToInt32(ViewState["branchno"])));
            //if (duration == Semesterno)
            //{
            //    this.ShowGradeCard("Grade_Card", "rptGradeCardFinalReportPG.rpt");
            //}
            //**************************added on 070922*********************************
            int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            if (Branchno == 1)
            {
                if (DurationCheck == 5)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_New.rpt"); //added on 070922 for only show 10 semester
                }
                else if (DurationCheck == 2)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_II.rpt"); //added on 070922  for only show 4 semester
                }
                else if (DurationCheck == 3)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_III.rpt"); //added on 070922 for only show 6 semester
                }
                else
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_IV.rpt"); //added on 070922 for only show 8 semester
                }
            }
            else
            {
                if (DurationCheck == 5)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_Crescent.rpt");
                    //rptGradeCardReportPG_Consoli.rpt");//rptGradeCardReportPG_New.rpt //rptGradeCardReportPG_Trans
                }
                else if (DurationCheck == 2)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_sec.rpt"); //added on 070922 for only show 4 semester
                }
                else if (DurationCheck == 3)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_third.rpt"); //added on 070922 for only show 6 semester
                }
                else
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_fourth.rpt"); //added on 070922 for only show 8 semester
                }

            }

        }
        #endregion
        #region For JECRC Grad Card

        else if (Convert.ToInt32(Session["OrgId"]) == 5)
        {
            // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
            // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");



            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                }
            }


            if (ddlStuType.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select Student Type", this.Page);
            }

            MarksEntryController objMarkEntry = new MarksEntryController();
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            string idnos = GetIDNOFGenerateGradeNo();
            objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            Session["Scrutinized"] = txtScrutinized.Text;

            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_jecrc.rpt", ids);


        }
        #endregion
        #region For Atlas
        else if (Convert.ToInt32(Session["OrgId"]) == 9)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                }
            }


            if (ddlStuType.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select Student Type", this.Page);
            }

            MarksEntryController objMarkEntry = new MarksEntryController();
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            string idnos = GetIDNOFGenerateGradeNo();
            objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_ATLAS_Grade_Card.rpt", ids);

        }
        #endregion
        #region MIT Grad Card
        else if (Convert.ToInt32(Session["OrgId"]) == 8)
        {

            // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
            // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            ids = ids.TrimEnd('.');

            this.ShowGradeCardNew("Grade Card", "MIT_GRADECARD.rpt", ids);

        }
        #endregion
        #region Rajagiri Grad Card
        else if (Convert.ToInt32(Session["OrgId"]) == 7) //Added By Tejas Thakre on 03_01_2023 
        {
            if (Convert.ToInt32(ViewState["college_id"]) == 2)
            {
                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_Rajagiri.rpt", ids);
            }


            if (Convert.ToInt32(ViewState["degreeno"]) == 4)  //Added By Tejas Thakre on 19-01-2023 for MCA Degree 
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_MCA_Degree.rpt", ids);
            }

            if (Convert.ToInt32(ViewState["degreeno"]) == 8) //Added by Tejas Thakre on on 19-01-2023 for PGDCSW Garde Card
            {
                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_PGDCSW.rpt", ids);
            }


            if (Convert.ToInt32(ViewState["degreeno"]) == 11) //Added by Tejas Thakre on on 19-01-2023 for BCOM Garde Card
            {
                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_BCOM.rpt", ids);
            }

            if (Convert.ToInt32(ViewState["degreeno"]) == 1)   //Added by Tejas Thakre on on 19-01-2023 for BBA Garde Card
            {
                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_BBA.rpt", ids);
                //this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_BCOM.rpt", ids); 
            }


            if (Convert.ToInt32(ViewState["degreeno"]) == 6)  //Added by Tejas Thakre on on 19-01-2023 for BSW Garde Card
            {
                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_BSW.rpt", ids);
            }

            if (Convert.ToInt32(ViewState["degreeno"]) == 12)  //Added by Tejas Thakre on on 19-01-2023 for BSC Garde Card
            {
                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutTitle("Grade Card", "rptMarkCumGradeCard_Rajagiri_BSC.rpt", ids);
            }

        }
        #endregion
        #region For RCPIPER Grade Card added on 17/01/2023 by shubham

        else if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            ids = ids.TrimEnd('.');

            this.ShowGradeCardNew("Grade Card", "MarksGrade_RCPIPER.rpt", ids);


        }
        #endregion
        #region For CPUH Grad Card Added By Tejas Thakre on 08/04/2023

        else if (Convert.ToInt32(Session["OrgId"]) == 4)
        {

            if (Convert.ToInt32(ViewState["degreeno"]) == 9) //For BSC.Honors Grade Card Added By Tejas Thakre on 11_04_2023
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    }
                }


                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));


                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUH_BSC_Hors.rpt", ids);

            }
            else if (Convert.ToInt32(ViewState["degreeno"]) == 13) //DPHARM Grade Card Added By Tejas Thakre on 28-09-2023
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    }
                }


                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));


                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUH_Annual_Card_Dpharm.rpt", ids);
            }
            else
            {


                // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
                // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    }
                }


                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));


                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUH.rpt", ids);
            }


        }
        #endregion
        #region For CPUKOTA Grad Card Added By Tejas Thakre on 12/04/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 3)
        {

            int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT ADMBATCH", "BRANCHNO=" + ViewState["branchno"] + "AND DEGREENO=" + ViewState["degreeno"]));

            string grade = objCommon.LookUp("ACD_SCHEME", "DISTINCT GRADEMARKS", "BRANCHNO=" + ViewState["branchno"] + "AND DEGREENO=" + ViewState["degreeno"] + "AND ADMBATCH=" + admbatch);


            if (grade == "M")
            {

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                    CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    //if (chk.Checked == true)
                    //{
                    //    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    //    cntlength++;
                    //}
                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                    if (ddlStuType.SelectedIndex < 0)
                    {
                        objCommon.DisplayMessage("Please Select Student Type", this.Page);
                    }


                    if (DurationCheck == 5)
                    {
                        //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_New.rpt"); //added on 070922 for only show 10 semester
                        this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Fifth_Year.rpt", ids);
                    }
                    else if (DurationCheck == 2)
                    {
                        if (Convert.ToInt32(ViewState["degreeno"]) == 40)
                        {
                            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Dpharm.rpt", ids);
                        }
                        else if (Convert.ToInt32(ViewState["degreeno"]) == 5)
                        {
                            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Msc_Phy.rpt", ids);
                        }
                        else if (Convert.ToInt32(ViewState["degreeno"]) == 24)
                        {
                            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_LLM.rpt", ids);
                        }
                        else
                        {
                            //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_II.rpt"); //added on 070922  for only show 4 semester
                            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Second_Year.rpt", ids);
                        }
                    }
                    else if (DurationCheck == 3)
                    {
                        if (Convert.ToInt32(ViewState["degreeno"]) == 23)
                        {
                            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_BA.rpt", ids);
                        }
                        else
                        {
                            //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_III.rpt"); //added on 070922 for only show 6 semester
                            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card.rpt", ids);
                        }
                    }
                    else if (DurationCheck == 2)
                    {
                        //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_IV.rpt"); //added on 070922 for only show 8 semester
                        this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Second_Year.rpt", ids);

                    }
                    else if (DurationCheck == 4)
                    {
                        this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Four_Year.rpt", ids);
                    }
                }
            }
            else
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_CPUKOTA.rpt", ids);

            }
        }

        #endregion

        #region For UTKAL Grad Card Added By Tejas Thakre on 03/07/2023

        else if (Convert.ToInt32(Session["OrgId"]) == 17)
        {

            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            ids = ids.TrimEnd('.');

            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_UTKAL.rpt", ids);
        }
        #endregion

        #region For DAIICT Grad Card Added By Tejas Thakre on 14/08/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 15)
        {
            #region logic1
            string stdids = string.Empty;
            int cntlength = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                if (chk.Checked == true)
                {
                    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    cntlength++;
                }
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            }
            int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
            if (cntid == cntlength)
            {
                stdids = "0";
            }
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_DAIICT.rpt", stdids);
            #endregion
        }
        #endregion

        #region For HITS Grad Card Added By Tejas Thakre on 04/09/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 18)
        {

            string stdids = string.Empty;
            int cntlength = 0;

            MarksEntryController objMarkEntry = new MarksEntryController();
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            string idnos = GetIDNOFGenerateGradeNo();
            objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                if (chk.Checked == true)
                {
                    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    cntlength++;
                }
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }


            }
            int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
            if (cntid == cntlength)
            {
                stdids = "0";
            }
            if (DurationCheck == 5)
            {
                //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_New.rpt"); //added on 070922 for only show 10 semester
                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_HITS_V.rpt", stdids);
            }
            else if (DurationCheck == 2)
            {
                //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_II.rpt"); //added on 070922  for only show 4 semester
                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_HITS_II.rpt", stdids);
            }
            else if (DurationCheck == 3)
            {
                //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_III.rpt"); //added on 070922 for only show 6 semester
                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_HITS_III.rpt", stdids);
            }
            else
            {
                //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_IV.rpt"); //added on 070922 for only show 8 semester
                this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_HITS.rpt", stdids);

            }

        }
        #endregion

        #region For PCEN Grad Card Added By Tejas Thakre on 30/10/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 19)
        {

            string stdids = string.Empty;
            int cntlength = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                if (chk.Checked == true)
                {
                    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    cntlength++;
                }
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            }
            int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
            if (cntid == cntlength)
            {
                stdids = "0";
            }
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_PCEN.rpt", stdids);
        }
        #endregion
        #region For JLOCE Grad Card Added By Tejas Thakre on 17/11/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 20)
        {

            string stdids = string.Empty;
            int cntlength = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                if (chk.Checked == true)
                {
                    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    cntlength++;
                }
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            }
            int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
            if (cntid == cntlength)
            {
                stdids = "0";
            }
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_JLOCE.rpt", stdids);
        }
        #endregion
        #region For PRMITR Grad Card Added By Tejas Thakre on 29/11/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 10)
        {

            string stdids = string.Empty;
            int cntlength = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                if (chk.Checked == true)
                {
                    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    cntlength++;
                }
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            }
            int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
            if (cntid == cntlength)
            {
                stdids = "0";
            }
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_PRMITR.rpt", stdids);
        }
        #endregion
        #region For TGPCET  Grad Card Added By Tejas Thakre on 01/12/2023
        else if (Convert.ToInt32(Session["OrgId"]) == 21)
        {
            string stdids = string.Empty;
            int cntlength = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                if (chk.Checked == true)
                {
                    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    cntlength++;
                }
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            }
            int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
            if (cntid == cntlength)
            {
                stdids = "0";
            }
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_TGPCET.rpt", stdids);
        }
        #endregion
        #region For MAHER Grade Card Added By Sagar Mankar on Date 21112023

        else if (Convert.ToInt32(Session["OrgId"]) == 16)
        {
            string ids = string.Empty;

            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }

            ids = ids.TrimEnd('.');
            this.ShowGradeCardNew("Grade Card", "rptGradeCardReport_MAHER.rpt", ids);
        }

        #endregion
        else
        {

            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            //GenerateQrCode(ids);  //Added by Deepali on 27/08/2020
            //if (ddlSemester.SelectedValue == "6")
            //{
            //    this.ShowGradeCardFinal("Grade_Card_Final", "rptGradeCardReportFinalPG.rpt"); //commented by reena
            //} 
            //else
            //{

            if (ddlStuType.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select Student Type", this.Page);
            }
            //else
            //{
            //    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG.rpt");
            //}
            // if (ddlgradecardfor.SelectedIndex==1)
            //{
            //    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG.rpt");
            //}
            // else if (ddlgradecardfor.SelectedIndex == 2)
            // {
            //     this.ShowGradeCard("Grade_Card", "rptGradeCardReportPGNEW.rpt");
            // }
            // else
            // {
            //     this.ShowGradeCard("StatementOfMarks", "rptStatmentOfMarks.rpt");
            // }

            //}

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            MarksEntryController objMarkEntry = new MarksEntryController();
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            string idnos = GetIDNOFGenerateGradeNo();
            objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
            if (duration == Semesterno)
            {
                this.ShowGradeCard("Grade_Card", "rptGradeCardFinalReportPG.rpt");
            }
            else
            {
                this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG.rpt");
            }
        }

    }



    private void ShowGradeCardNew(string reportTitle, string rptFileName, string ids)
    {
        try
        {
            //added on 20-02-2020 by Vaishali
            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {
                    count++;
                }
            }

            if (count > 0)
            {

                DateTime dateofissue = DateTime.MinValue;
                string dateofIssue = txtDateOfIssue.Text == "" ? string.Empty : (txtDateOfIssue.Text);
                if (dateofIssue == string.Empty)
                {
                    dateofIssue = "";
                    dateofissue = DateTime.Today;
                }
                else
                // DateTime dateofissue = Convert.ToDateTime(txtDateofIssue.Text);
                {

                    if (txtDateOfIssue.Text != "")
                    {
                        dateofissue = Convert.ToDateTime(txtDateOfIssue.Text);
                        txtDateOfIssue.Text = dateofissue.ToString("dd-MMM-yyyy");
                        // dateofissue =Convert.ToDateTime( txtDateofIssue.Text);
                    }
                    else
                    {
                        DateTime? dtt = null;
                        dateofissue = DateTime.Today;
                    }
                }
                string spec = string.Empty;
                string Result = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;

                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@User_Name=" + Session["userfullname"].ToString() + ",@Scrutinized=" + Session["Scrutinized"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 9)//Add for atlas org=9
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 7)//Add for rajagiri org=7 added by tejas thakre on 03_01_2022
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 8)//MITAOE
                {
                    ReportDocument report = new ReportDocument();
                    DataTable dt = Add_Datatable_IDNO();
                    DataSet ds = new DataSet();
                    // Encode the XML data as a URL parameter
                    ds.Tables.Add(dt);
                    Session["TabulationChart"] = ds;
                    string xmlData = ds.GetXml().ToString();
                    xmlData = xmlData.Trim();
                    xmlData = string.Empty;
                    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;
                    url += "&pagename=TabulationChart&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_xml=" + xmlData;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6) // rcpiper added by shubham on 17/01/2023
                {
                    url += "&param=@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_DATEOFISSUE=" + DateTime.Today.Date + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue);
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 4) // CPUH added by Tejas Thakre on 08/04/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 3) // CPUKOTA added by Tejas Thakre on 12/04/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 17) // UTKAL added by Tejas Thakre on 03/07/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 15) // DAIICT added by Tejas Thakre on 08/04/2023
                {

                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + ids + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 18) // HITS added by Tejas Thakre on 04/09/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + ids + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 19) // PCEN added by Tejas Thakre on 30/10/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + ids + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 20) //JLOCE added by Tejas Thakre on 17/11/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + ids + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 10) //PRMITR  added by Tejas Thakre 29/11/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + ids + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 21) //TGPCET  added by Tejas Thakre 01/12/2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + ids + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 16) // For MAHER Grade Card Added By Sagar Mankar on Date 21112023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowGradeCardWithoutTitle(string reportTitle, string rptFileName, string ids)
    {
        try
        {
            //added on 20-02-2020 by Vaishali
            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {
                    count++;
                }
            }

            if (count > 0)
            {

                DateTime dateofissue = DateTime.MinValue;
                string dateofIssue = txtDateOfIssue.Text == "" ? string.Empty : (txtDateOfIssue.Text);
                if (dateofIssue == string.Empty)
                {
                    dateofIssue = "";
                    dateofissue = DateTime.Today;
                }
                else
                // DateTime dateofissue = Convert.ToDateTime(txtDateofIssue.Text);
                {

                    if (txtDateOfIssue.Text != "")
                    {
                        dateofissue = Convert.ToDateTime(txtDateOfIssue.Text);
                        txtDateOfIssue.Text = dateofissue.ToString("dd-MMM-yyyy");
                        // dateofissue =Convert.ToDateTime( txtDateofIssue.Text);
                    }
                    else
                    {
                        DateTime? dtt = null;
                        dateofissue = DateTime.Today;
                    }
                }
                string spec = string.Empty;
                string Result = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                if (Convert.ToInt32(Session["OrgId"]) == 7)//Add for rajagiri org=7 added by tejas thakre on 03_01_2022
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6) //For RCIPIPER Added By Tejas Thakre on 05-06-2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_DATEOFISSUE=" + DateTime.Today.Date + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_IDNO=" + ids;
                }


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }
    //Added by Deepali on 27/08/2020 For QR Code on Grade card Report
    //This Method Generate QR-CODE & also  save image in ACD_STUD_PHOTO Table & QR-Code Files Folder.
    private void GenerateQrCode(string idno)
    {
        string declaredDate = string.Empty;
        string dateOfIssue = string.Empty;
        declaredDate = txtDeclareDate.Text;
        dateOfIssue = txtDateOfIssue.Text;
        DataSet ds1 = objQrC.GetStudentDataForQRCode(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlCollege.SelectedValue), Convert.ToInt16(ddlDegree.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), (stuadmbatch.Value).ToString(), declaredDate, dateOfIssue, idno, Session["userfullname"].ToString());
        if (ds1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                string Qrtext = "SerialNo: " + ds1.Tables[0].Rows[i]["GRADE_SR_NO"] +
                       "; StudName: " + ds1.Tables[0].Rows[i]["STUDNAME"].ToString().Trim() +
                       "; RegNo: " + ds1.Tables[0].Rows[i]["REGNO"] +
                       "; RollNo: " + ds1.Tables[0].Rows[i]["ROLLNO"] +
                       "; Programme: " + ds1.Tables[0].Rows[i]["BRANCH"] +
                       "; Semester: " + ds1.Tables[0].Rows[i]["SEMESTER"] +
                       "; SGPA: " + ds1.Tables[0].Rows[i]["SGPA"] +
                        "";

                Session["qr"] = Qrtext.ToString();
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeVersion = 10;
                Bitmap img = encoder.Encode(Session["qr"].ToString());
                //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
                img.Save(Server.MapPath("~\\img.Jpeg"));
                ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");
                //img.Save(Server.MapPath("~\\img.Jpeg"));
                byte[] QR_IMAGE = ViewState["File"] as byte[];
                //  long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds1.Tables[0].Rows[i]["IDNO"].ToString().Trim()), QR_IMAGE);
            }
        }
        else
        {
            IsDataPresent = true;
            objCommon.DisplayUserMessage(updpnlExam, "Grade card can not be generated as nobody passed in selected examination!", this.Page);
            return;
        }
    }

    private void ShowGradeCard(string reportTitle, string rptFileName)
    {
        try
        {
            //added on 20-02-2020 by Vaishali
            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();

                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    //string date1 = Convert.ToDateTime(txtDateOfIssue.Text).ToString("dd/MM/yyyy");
                    DateTime date = DateTime.ParseExact(txtDateOfIssue.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string date1 = date.ToString("dd MMM yyyy"); //added on 29032023
                    string date2 = Convert.ToDateTime(txtDateOfPublish.Text).ToString("dd/MM/yyyy"); // 03042023

                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue + ",@P_EXAM_DATE=" + date1 + ",@P_PUBLISH_DATE=" + date2; //added on 05122022

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value);
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW() + ",@P_ADMBATCHNO=" + Convert.ToString(ViewState["schemeno"]);//GetIDNOS_NEW
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@USERNAME=" + Session["userfullname"].ToString() + ",@Scrutinized=" + Session["Scrutinized"].ToString();

                }
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;

                }
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
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

    private void ShowGradeCardWithoutHeader(string reportTitle, string rptFileName)
    {
        try
        {
            //added on 20-02-2020 by Vaishali
            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                string Result = string.Empty;
                string spec = string.Empty;

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;


                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + stuadmbatch.Value + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;

                    url += "&param=@P_IDNO=" + GetIDNO() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_YEAR=" + ddlYear.SelectedValue + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + stuadmbatch.Value + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@USERNAME=" + Session["userfullname"].ToString() + ",@Scrutinized=" + Session["Scrutinized"].ToString();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 9)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();//",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 4) //for CPUH Added By Tejas Thakre on 08-04-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 3) //for CPUKOTA Added By Tejas Thakre on 12-04-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 17) //for UTKAL Added By Tejas Thakre on 03-07-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 15) //for DAIICT Added By Tejas Thakre on 17-08-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW();  //GetIDNO1();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 18) //for HITS Added By Tejas Thakre on 04-09-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW(); // +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 19) // for PCEN Added by Tejas Thakre on 30-10-2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + GetIDNOS_NEW();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 20) // for JLOCE Added by Tejas Thakre on 17-11-2023
                {
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + GetIDNOS_NEW();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 10) // For PRMITR by Tejas Thakre on 29-11-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW();  //GetIDNO1();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 21) // For TGPCET   by Tejas Thakre on 01-02-2023
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW();  //GetIDNO1();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + stuadmbatch.Value + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
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

    //protected void btnRankList_Click(object sender, EventArgs e)
    //{
    //    ShowResultSheet("RankList", "rptRanklist.rpt"); 
    //}
    //private void ShowResultSheetBrancwise(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue  + ",@P_BRANCHNO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnBranchwise_Click(object sender, EventArgs e)
    //{
    //    ShowResultSheetBrancwise("ResultSheetBRanchwise", "rptResultAnalysisReportForMtech.rpt");

    //}


    protected void btntabulation_Click(object sender, EventArgs e)
    {
        if (ddlStuType.SelectedValue == "-1")
        {
            objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
        }
        else
        {
            ShowTR("ResultSheet", "rptTabulationReport.rpt", 1);
        }

    }
    //private void ShowResult(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}


    ////private void ShowTR(string reportTitle, string rptFileName, int param)
    ////{
    ////    try
    ////    {
    ////        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    ////        url += "Reports/CommonReport.aspx?";
    ////        url += "pagetitle=" + reportTitle;
    ////        url += "&path=~,Reports,Academic," + rptFileName;
    ////        //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

    ////        if (param == 1)
    ////        {
    ////            url += "&param=@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    ////        }
    ////        else if (param == 2)
    ////        {
    ////            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    ////        }
    ////        else if (param == 3)
    ////        {
    ////            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_STUDENT_TYPE=" + Convert.ToInt32(ddlStuType.SelectedValue);
    ////        }
    ////        else if (param == 4)
    ////        {
    ////            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_STUDENT_TYPE=" + Convert.ToInt32(ddlStuType.SelectedValue);
    ////        }
    ////        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    ////        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    ////        sb.Append(@"window.open('" + url + "','','" + features + "');");

    ////        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        if (Convert.ToBoolean(Session["error"]) == true)
    ////            objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    ////        else
    ////            objUCommon.ShowError(Page, "Server Unavailable.");
    ////    }
    ////}

    private void ShowTR(string reportTitle, string rptFileName, int param)
    {
        try
        {
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            if (param == 1)
            {
                url += "&param=@P_COLLEGEID=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",	@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            else if (param == 2)
            {

                if (Convert.ToInt32(Session["OrgId"]) == 8)
                {

                    ReportDocument report = new ReportDocument();
                    DataTable dt = Add_Datatable_IDNO();
                    DataSet ds = new DataSet();
                    // Encode the XML data as a URL parameter
                    ds.Tables.Add(dt);
                    Session["TabulationChart"] = ds;
                    string xmlData = ds.GetXml().ToString();
                    xmlData = xmlData.Trim();
                    xmlData = Regex.Replace(xmlData, @">\s+<", "><");
                    xmlData = string.Empty;
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PUBLISH_DATE=" + txtDateOfIssue.Text + ",@P_HELD_IN_DATE=" + txtDeclareDate.Text;//Modify by Maithili [24/01/2020]

                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                    url += "&pagename=TabulationChart&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + 0 + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_XML=" + xmlData + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue +",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_XML=" + xmlData + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                }
                else
                {
                    if (Convert.ToInt32(Session["OrgId"]) == 15)
                    {
                        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else if (Convert.ToInt32(Session["OrgId"]) == 20) //Added By Tejas Thakre for JLCOE as on 29012024
                    {
                        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNOS_NEW() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
                    }
                    else if (Convert.ToInt32(Session["OrgId"]) == 21) //Added By Tejas Thakre for TGPCET as on 01022024
                    {
                        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else if (Convert.ToInt32(Session["OrgId"]) == 19) //Added By Tejas Thakre for PCEN as on 05022024
                    {
                        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");
                    }
                }




            }
            else if (param == 3)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            }
            else if (param == 4)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            }
            else if (param == 5)
            {
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PUBLISH_DATE=" + txtDateOfIssue.Text + ",@P_HELD_IN_DATE=" + txtDeclareDate.Text + ",@P_UA_NO=" + Session["userno"].ToString();//Modify by Maithili [24/01/2020]
            }

            //Added by Ashish 04052022
            else if (param == 6)
            {
                //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PUBLISH_DATE=" + txtDateOfIssue.Text + ",@P_HELD_IN_DATE=" + txtDeclareDate.Text;//Modify by Maithili [24/01/2020]
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
            }


            //Added by prafull 23012023
            else if (param == 7)
            {

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue);
            }

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


    //protected void brnphdresultSheet_Click(object sender, EventArgs e)
    //{
    //    ShowResultSheet("PhdResultSheet", "rptPhdResultSheet.rpt"); 
    //}
    protected void btnGradeCardHeader_Click(object sender, EventArgs e)
    {
        //For RCPIT REPORT ADDEDE BY TEJAS tHAKRE 
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";
                    //count++;
                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
                else if (chk1.Checked)
                {
                    ids = "0";
                }
                else
                {

                }


            }

            ids = ids.TrimEnd('.');

            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "MarksGrade_RCPIT_Without_Header.rpt");

        }
        else if (Convert.ToInt32(Session["OrgId"]) == 2)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

            }
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);

            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                if (duration == Semesterno)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardFinalReportPG_WithoutHeader.rpt");
                }
                else
                {
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportPG_WithoutHeader.rpt");
                }


                //this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportWithoutHeaderPG.rpt");
            }


        }
        //For JECRC REPORT 
        else if (Convert.ToInt32(Session["OrgId"]) == 5)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

            }
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);

            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                Session["Scrutinized"] = txtScrutinized.Text;


                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                if (duration == Semesterno)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_jecrc_without_header.rpt");
                }
                else
                {
                    //  this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportPG_WithoutHeader.rpt");
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_jecrc_without_header.rpt");

                }


                //this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportWithoutHeaderPG.rpt");
            }
        }
        // For ATLAS REPORT  
        else if (Convert.ToInt32(Session["OrgId"]) == 9)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

            }
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);

            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                //if (duration == Semesterno)
                //{
                //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                //}
                //else
                //{
                this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_ATLAS_Without_Header.rpt");
                // }
            }
        }
        // For CPUH REPORT  
        else if (Convert.ToInt32(Session["OrgId"]) == 4)
        {

            if (Convert.ToInt32(ViewState["degreeno"]) == 9) //For BSC.Honors Grade Card Added By Tejas Thakre on 11_04_2023
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    //string 
                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                }
                //if (ids != string.Empty)
                //    GenerateQrCode(ids);

                if (ddlStuType.SelectedValue == "-1")
                {
                    objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
                }
                else
                {


                    MarksEntryController objMarkEntry = new MarksEntryController();
                    int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    int College_id = Convert.ToInt32(ViewState["college_id"]);
                    int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                    int Branchno = Convert.ToInt32(ViewState["branchno"]);
                    int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                    int ua_no = Convert.ToInt32(Session["userno"].ToString());
                    string idnos = GetIDNOFGenerateGradeNo();
                    objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                    int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                    //if (duration == Semesterno)
                    //{
                    //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                    //}
                    //else
                    //{
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUH_BSC_Hors_Without_Header.rpt");
                    // }
                }
            }
            else if (Convert.ToInt32(ViewState["degreeno"]) == 13) //DPHARM Grade Card Added By Tejas Thakre on 28-09-2023
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    }
                }


                if (ddlStuType.SelectedIndex < 0)
                {
                    objCommon.DisplayMessage("Please Select Student Type", this.Page);
                }

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));


                this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUH_Annual_Card_Without_Header_Dpharm.rpt");
            }
            else
            {

                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    //string 
                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                }
                //if (ids != string.Empty)
                //    GenerateQrCode(ids);

                if (ddlStuType.SelectedValue == "-1")
                {
                    objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
                }
                else
                {


                    MarksEntryController objMarkEntry = new MarksEntryController();
                    int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    int College_id = Convert.ToInt32(ViewState["college_id"]);
                    int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                    int Branchno = Convert.ToInt32(ViewState["branchno"]);
                    int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                    int ua_no = Convert.ToInt32(Session["userno"].ToString());
                    string idnos = GetIDNOFGenerateGradeNo();
                    objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                    int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                    //if (duration == Semesterno)
                    //{
                    //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                    //}
                    //else
                    //{
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUH_Without_Header.rpt");
                    // }
                }
            }
        }
        // For CPUKOTA REPORT  
        else if (Convert.ToInt32(Session["OrgId"]) == 3)
        {

            int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT ADMBATCH", "BRANCHNO=" + ViewState["branchno"] + "AND DEGREENO=" + ViewState["degreeno"]));

            string grade = objCommon.LookUp("ACD_SCHEME", "DISTINCT GRADEMARKS", "BRANCHNO=" + ViewState["branchno"] + "AND DEGREENO=" + ViewState["degreeno"] + "AND ADMBATCH=" + admbatch);


            if (grade == "M")
            {

                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                    CheckBox chkHead1 = lvStudent.FindControl("chkheader") as CheckBox;
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    //if (chk.Checked == true)
                    //{
                    //    stdids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                    //    cntlength++;
                    //}
                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                    if (ddlStuType.SelectedIndex < 0)
                    {
                        objCommon.DisplayMessage("Please Select Student Type", this.Page);
                    }


                    if (DurationCheck == 5)
                    {
                        //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_New.rpt"); //added on 070922 for only show 10 semester
                        this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Without_Header_Fifth_Year.rpt");
                    }
                    else if (DurationCheck == 2)
                    {
                        if (Convert.ToInt32(ViewState["degreeno"]) == 40)
                        {
                            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Without_Header_Dpharm.rpt");
                        }
                        else if (Convert.ToInt32(ViewState["degreeno"]) == 5)
                        {
                            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Without_Header_Msc_Phy.rpt");
                        }
                        else if (Convert.ToInt32(ViewState["degreeno"]) == 24)
                        {
                            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Without_Header_LLM.rpt");
                        }
                        else
                        {
                            //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_II.rpt"); //added on 070922  for only show 4 semester
                            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Without_Header_Second_Year.rpt");
                        }
                    }
                    else if (DurationCheck == 3)
                    {
                        if (Convert.ToInt32(ViewState["degreeno"]) == 23)
                        {
                            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Without_Header_BA.rpt");
                        }
                        else
                        {
                            //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_III.rpt"); //added on 070922 for only show 6 semester
                            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Without_Header.rpt");
                        }
                    }
                    else if (DurationCheck == 2)
                    {
                        //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_IV.rpt"); //added on 070922 for only show 8 semester
                        this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Without_Header_Second_Year.rpt");

                    }
                    else if (DurationCheck == 4)
                    {
                        this.ShowGradeCardWithoutHeader("Grade Card", "rptGradeCardReport_CPUKOTA_Annual_Card_Card_Without_Header_Four_Year.rpt");
                    }
                }
            }
            else
            {
                string ids = string.Empty;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    Label lblStudname = item.FindControl("lblStudname") as Label;

                    string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                    if (chk.Checked)
                    {
                        ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                        //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                    }
                }
                ids = ids.TrimEnd('.');

                this.ShowGradeCardWithoutHeader("Grade Card", "rptGradeCardReport_CPUKOTA_Without_Header.rpt");

            }

        }
        // For RCIPIPER REPORT  
        else if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            string ids = string.Empty;
            //CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }
            ids = ids.TrimEnd('.');
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);



            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, ids, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                //if (duration == Semesterno)
                //{
                //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                //}
                //else
                //{
                this.ShowGradeCardWithoutTitle("Grade_Card_Without_header", "MarksGrade_RCPIPER_Without_Header.rpt", ids);
                // }
            }
        }
        // For UTKAL REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 17)
        {
            string ids = string.Empty;
            //CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }
            ids = ids.TrimEnd('.');
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);



            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, ids, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                //if (duration == Semesterno)
                //{
                //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                //}
                //else
                //{
                this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_UTKAL.rpt");
                // }
            }
        }
        // For DAIICT REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 15)
        {
            string ids = string.Empty;
            //CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }
            ids = ids.TrimEnd('.');
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);



            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, ids, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                //if (duration == Semesterno)
                //{
                //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                //}
                //else
                //{
                this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_DAIICT_Withour_Header.rpt");
                // }
            }
        }
        // For HITS REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 18)
        {

            string ids = string.Empty;
            //CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }
            ids = ids.TrimEnd('.');
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);



            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, ids, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

                if (DurationCheck == 5)
                {
                    //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_New.rpt"); //added on 070922 for only show 10 semester
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_HITS_Withour_Header_V.rpt");
                }
                else if (DurationCheck == 2)
                {
                    //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_II.rpt"); //added on 070922  for only show 4 semester
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_HITS_Without_Header_II.rpt");
                }
                else if (DurationCheck == 3)
                {
                    //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_III.rpt"); //added on 070922 for only show 6 semester
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_HITS_Without_Header_III.rpt");
                }
                else
                {
                    //this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW_IV.rpt"); //added on 070922 for only show 8 semester
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_HITS_Without_Header.rpt");
                }

            }
        }
        // For PCEN REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 19)
        {

            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";
                    //count++;
                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }

            ids = ids.TrimEnd('.');

            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_PCEN_Without_Header.rpt");

        }
        // For JLOCE REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 20)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";
                    //count++;
                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }

            ids = ids.TrimEnd('.');

            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_JLOCE_Without_Header.rpt");
        }
        // For PRMITR REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 10)
        {
            string ids = string.Empty;
            //CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }
            ids = ids.TrimEnd('.');
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);



            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {


                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, ids, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                //if (duration == Semesterno)
                //{
                //    this.ShowGradeCard("Grade_Card", "rptGradeCardReport_ATLAS_Without_Header");
                //}
                //else
                //{
                this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_PRMITR_Without_Header.rpt");
                // }
            }
        }
        //For TGPCET  REPORT
        else if (Convert.ToInt32(Session["OrgId"]) == 21)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";
                    //count++;
                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }

            }

            ids = ids.TrimEnd('.');

            this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_TGPCET_Without_Header");
        }
        else
        {

            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                ////if (chk.Checked)
                ////{
                ////    GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                ////}
                //if (chk.Checked)
                //{
                //    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";
                //    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                //}
            }
            //if (ids != string.Empty)
            //    GenerateQrCode(ids);

            if (ddlStuType.SelectedValue == "-1")
            {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
            }
            else
            {

                //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                MarksEntryController objMarkEntry = new MarksEntryController();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int College_id = Convert.ToInt32(ViewState["college_id"]);
                int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int Branchno = Convert.ToInt32(ViewState["branchno"]);
                int Semesterno = Convert.ToInt16(ddlSemester.SelectedValue);
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                string idnos = GetIDNOFGenerateGradeNo();
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                if (duration == Semesterno)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardFinalReportPG_WithoutHeader.rpt");
                }
                else
                {
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportPG_WithoutHeader.rpt");
                }

            }
        }
    }

    //protected void btnStudentFailedList_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReportFailList("Fail_Student_List", "rptFailRollList.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentResultList.btnFail_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    //this.ShowStudentfailedList("Student_Failed_List", "rptStudentFailedList.rpt");
    //}
    //private void ShowReportFailList(string reportTitle, string rptFileName)
    //{

    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SECTIONNO=0,@P_PREV_STATUS=0,@P_ABSORPTION_STATUS=0";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";

    //        //To open new window from Updatepanel
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_StudentResultList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowStudentfailedList(string reportTitle, string rptFileName)
    {
        try
        {
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
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

    //protected void ddlStuType_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void btnGradeCardBackReport_Click(object sender, EventArgs e)
    //{
    //    foreach (ListViewDataItem item in lvStudent.Items)
    //    {
    //        CheckBox chk = item.FindControl("chkStudent") as CheckBox;
    //        Label lblStudname = item.FindControl("lblStudname") as Label;

    //        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
    //        if (chk.Checked)
    //        {
    //            int Admbatch = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + "")));

    //            if (Admbatch < 5)
    //            {
    //                //this.ShowGradeCardBack("Grade_Card_Back_Report", "rptGradeCardBackReport.rpt")
    //                this.ShowTranscriptBack((((item.FindControl("lblStudname")) as Label).ToolTip), "Transcript_Back_Report", "rptGradeCardBackReportBelow2013-2014.rpt");
    //            }
    //            else
    //            {
    //                //this.ShowGradeCardBack("Grade_Card_Back_Report", "rptGradeCardBackReport.rpt")
    //                this.ShowTranscriptBack((((item.FindControl("lblStudname")) as Label).ToolTip), "Transcript_Back_Report", "rptGradeCardBackReportAbove2013-2014.rpt");
    //            }
    //        }
    //    }
    //}

    //private void ShowTranscriptBack(string idno,string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + "";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowGradeCardBack(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            ddlDegree.SelectedIndex = 0;
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE C WITH (NOLOCK) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON (C.DEGREENO=D.DEGREENO)", "C.DEGREENO", "DEGREENAME", "C.DEGREENO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "DEGREENO");
            //  objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID > 0", "D.DEGREENO");
            ddlDegree.Focus();
        }
        ddlDegree.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlStuType.SelectedIndex = 0;
        txtDateOfIssue.Text = string.Empty;
        txtDeclareDate.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlStudent.Visible = false;

    }

    private void ShowPassedStudents(string reportTitle, string rptFileName)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
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

    protected void btnPassedStud_Click(object sender, EventArgs e)
    {
        this.ShowPassedStudents("Passed Students", "PassedStudentsReport.rpt");
    }

    //protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlAdmbatch.SelectedIndex > 0)
    //    {

    //    }
    //}

    protected void btnExcelPassed_Click(object sender, EventArgs e)
    {
        this.Export("xls", "PassedStudentsReport.rpt");

    }

    protected void btnWordPassed_Click(object sender, EventArgs e)
    {
        this.Export("doc", "PassedStudentsReport.rpt");
    }

    private void ShowStudentResultList(string reportTitle, string rptFileName)
    {
        try
        {
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_STUDTYPE=" + ddlStuType.SelectedValue;
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

    private void Export(string exporttype, string rptFileName)
    {
        try
        {
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + "Passed Student List";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnResultStatement_Click(object sender, EventArgs e)
    {
        if (ddlStuType.SelectedValue == "-1")
        {
            objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
        }
        else
        {
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                ShowTR("ResultSheet", "rptTabulationPG_Atlas.rpt", 2);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 8)
            {
                ShowTR("ResultSheet", "rptTabulationPG_MIT.rpt", 2);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 15) //Added by Tejas Thakre
            {
                ShowTR("ResultSheet", "rptTabulationPG_DAIICT.rpt", 2);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20) //Added by Tejas Thakre as on 18_09_2023
            {
                ShowTR("ResultSheet", "rptTabulationPG_PJOLCE.rpt", 2);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 21) // Added by Tejas for TGPCET as on 01_02_2023
            {
                ShowTR("ResultSheet", "rptTabulationPG_TGPCET.rpt", 2);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 19) // Added by Tejas Thakre PCEN as on 05_02_2024
            {
                ShowTR("ResultSheet", "rptTabulationPG_PCEN.rpt", 2);
            }
            else
            {
                ShowTR("ResultSheet", "rptTabulationPG.rpt", 2);
            }


        }
    }
    protected void btnGradeCardBackReport_Click(object sender, EventArgs e)
    {

    }
    protected void btnCourceWiseFailStudList_Click(object sender, EventArgs e)
    {

        ShowTR("CoursewiseFailStudentList", "rptFGradeResultAnalysis.rpt", 4);

    }
    protected void btnFailStudentList_Click(object sender, EventArgs e)
    {

        ShowTR("FailStudentList", "rptStudentFailedList.rpt", 3);
    }
    protected void btnTabulationNew_Click(object sender, EventArgs e)
    {
        if (ddlStuType.SelectedValue == "-1")
        {
            objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
        }
        else
        {
            ShowTR("ResultSheet", "rptTabulationReportNewTab.rpt", 2);
        }
    }

    protected void btnResultGazette_Click(object sender, EventArgs e)
    {
        if (ddlStuType.SelectedValue == "-1")
        {
            objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
        }
        else
        {
            ShowTR("ResultSheet", "rptResultGazette.rpt", 5);
        }
    }

    protected void showstudents_Click(object sender, EventArgs e)
    {
        try
        {
            this.Bindlist();
            //GenerateQrCode();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "showstudents_Click.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            ddlSemester.Focus();


        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));


        }

        //ddlSemester.Items.Clear();
        //ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlStuType.SelectedIndex = 0;
        txtDateOfIssue.Text = string.Empty;
        txtDeclareDate.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlStudent.Visible = false;

    }

    protected void ddlStuType_SelectedIndexChanged(object sender, EventArgs e)
    {

        // txtDateOfIssue.Text = string.Empty;
        // txtDeclareDate.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlStudent.Visible = false;
        showstudents.Focus();
    }

    protected void btnResultExcel_Click(object sender, EventArgs e)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;

            DataSet ds = null;
            ds = objQrC.GetStudentDataForDisplayInExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ddlSemester.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=ResultReport" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
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

    protected void btnStatementMarks_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            //CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            //Label lblStudname = item.FindControl("lblStudname") as Label;

            //string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));


            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;
            //string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));



            if (chk.Checked == true)
            {
                count++;
                // this.ShowGradeCard("StatementOfMarks", "rptStatmentOfMarks.rpt");
            }
            //else if (chk.Checked == false)
            //{
            //    objCommon.DisplayMessage(updpnlExam, "Please Select atleast one student.", this.Page);

            //}
        }
        if (count == 0)
        {
            objCommon.DisplayMessage(updpnlExam, "Please Select atleast one student.", this.Page);

        }
        else
        {
            this.ShowGradeCard("StatementOfMarks", "rptStatmentOfMarks.rpt");
        }

    }

    //protected void btnBacklogExcel1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridView GVDayWiseAtt = new GridView();
    //        string ContentType = string.Empty;




    //        DataSet ds = null;
    //        ds = objQrC.GetBacklogStudentDataForDisplayInExcel();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            //ds.Tables[0].Columns.RemoveAt(3);
    //            GVDayWiseAtt.DataSource = ds;
    //            GVDayWiseAtt.DataBind();

    //            string attachment = "attachment; filename=OverallBacklogReport.xls";
    //            Response.ClearContent();
    //            Response.AddHeader("content-disposition", attachment);
    //            Response.ContentType = "application/vnd.MS-excel";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            GVDayWiseAtt.RenderControl(htw);
    //            //lvStudApplied.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //added by Deepali
    protected void btnBacklogExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;




            DataSet ds = null;
            ds = objQrC.GetBacklogStudentDataForDisplayInExcel();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=OverallBacklogReport" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
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


    protected void btnTRExcel_Click(object sender, EventArgs e)
    {
        //if (ddlSession.SelectedIndex > 0)
        //{
        try
        {
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            GridView GVTrReport = new GridView();
            string ContentType = string.Empty;

            int Sessionno = ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
            //int College_id = ddlCollege.SelectedIndex > 0 ? Convert.ToInt32(ViewState["college_id"]) : 0;
            //int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ViewState["degreeno"]) : 0;
            //int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ViewState["branchno"]) : 0;
            int College_id = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["college_id"]) : 0;
            int Degreeno = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["degreeno"]) : 0;
            //  int Branchno = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["branchno"]) : 0;
            int Branchno = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["schemeno"]) : 0;
            int student_type = ddlStuType.SelectedIndex > 0 ? Convert.ToInt32(ddlStuType.SelectedValue) : 0;
            int semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;

            DataSet ds = null;
            ds = objQrC.GetTrReportStudentDetails(Sessionno, College_id, Degreeno, Branchno, student_type, semesterno); //Added by lalit on date 13-0-2023

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVTrReport.DataSource = ds;
                GVTrReport.DataBind();

                string attachment = "attachment; filename=TRReport" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
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
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updpnlExam, "Please Select Session", this.Page);
        //}
    }

    protected void btnResultStatistics_Click(object sender, EventArgs e)
    {

        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int Prev_Status = Convert.ToInt32(ddlStuType.SelectedValue);
        DataSet ds = objQrC.GetResultStatistics(Sessionno, Prev_Status);
        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView gv = new GridView();
            gv.DataSource = ds;
            gv.DataBind();
            string attachment = "attachment;filename= ResultStatistics" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "No Data Found", this.Page);
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {

            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlStuType.SelectedIndex = 0;
        txtDateOfIssue.Text = string.Empty;
        txtDeclareDate.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlStudent.Visible = false;
        ddlSession.SelectedIndex = 0;
        ddlYears.SelectedIndex = 0;

    }

    protected void btnGradeRegister_Click(object sender, EventArgs e)
    {
        //    int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //  //  int Prev_Status = Convert.ToInt32(ddlStuType.SelectedValue);
        //    int schemeno = Convert.ToInt32(ddlClgname.SelectedValue);
        //    int sem = Convert.ToInt32(ddlSemester.SelectedValue);

        if (ddlSemester.SelectedValue == "-1")
        {
            objCommon.DisplayUserMessage(updpnlExam, "Please Select Semester!", this.Page);
        }
        else
        {

            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                ShowGradeRegister("GradeCardRegister", "rptGradeCardRegister_Atlas.rpt", 2);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                ShowGradeRegister("GradeCardRegister", "rptGradeCardRegister_RCPIT.rpt", 2);
            }
            else
            {
                ShowGradeRegister("GradeCardRegister", "rptGradeCardRegister.rpt", 2);
            }

        }
    }


    private void ShowGradeRegister(string reportTitle, string rptFileName, int param)
    {
        try
        {
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //url += "&param=@P_COLLEGEID=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + schemeno;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + schemeno + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

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


    protected void btnGrace_Click(object sender, EventArgs e)
    {

        //if (ddlStuType.SelectedValue == "-1")
        //{
        //    objCommon.DisplayUserMessage(updpnlExam, "Please Select Student Type", this.Page);
        //}
        //else
        //{
        if (Convert.ToInt32(Session["OrgId"]) == 9)
        {
            ShowTR("GraceStudents", "rptGraceStudent_Atlas.rpt", 6);
        }
        else
        {
            ShowTR("GraceStudents", "rptGraceStudent.rpt", 6);
        }

        //}

    }


    protected void btncoursegrade_Click(object sender, EventArgs e)
    {
        //ShowTR("Course_wise_Grade", "rptCourse_wise_Grade.rpt", 6);
        ShowTR("Course_wise_Grade", "rptCourseWise_Grade.rpt", 7);
    }


    //private void ShowTR(string reportTitle, string rptFileName, int param)
    //{
    //    try
    //    {
    //        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
    //        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
    //        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
    //        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

    //        if (param == 1)
    //        {
    //            url += "&param=@P_COLLEGEID=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",	@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    //        }
    //        else if (param == 2)
    //        {
    //            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PUBLISH_DATE=" + txtDateOfIssue.Text + ",@P_HELD_IN_DATE=" + txtDeclareDate.Text;//Modify by Maithili [24/01/2020]
    //        }
    //        else if (param == 3)
    //        {
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
    //        }
    //        else if (param == 4)
    //        {
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
    //        }
    //        else if (param == 5)
    //        {
    //            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PUBLISH_DATE=" + txtDateOfIssue.Text + ",@P_HELD_IN_DATE=" + txtDeclareDate.Text + ",@P_UA_NO=" + Session["userno"].ToString();//Modify by Maithili [24/01/2020]
    //        }
    //        else if (param == 6)
    //        {
    //            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue);// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();//Modify by Maithili [24/01/2020] ViewState["schemeno"]
    //        }
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void btngraderange_Click(object sender, EventArgs e)
    {
        DataSet ds;
        if (ddlClgname.SelectedValue == "0")
        {
            ViewState["degreeno"] = "0";
        }

        string proc_name = "GRADE_RANGE_REPORT";
        string parameter = "@P_SESSIONNO,@P_DEGREENO,@P_SEMESTERNO";
        string Call_values = "" + ddlSession.SelectedValue + "," + ViewState["degreeno"].ToString() + "," + ddlSemester.SelectedValue + "";
        ds = objCommon.DynamicSPCall_Select(proc_name, parameter, Call_values);

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView gv = new GridView();
            gv.DataSource = ds;
            gv.DataBind();
            string attachment = "attachment;filename= GradeRange" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "No Data Found", this.Page);
        }

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        //string   param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue);// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();//Modify by Maithili [24/01/2020] ViewState["schemeno"]


        DataSet ds = new DataSet();
        if (ddlClgname.SelectedValue == "0")
        {
            ViewState["degreeno"] = "0";
            ViewState["schemeno"] = "0";
        }

        string proc_name = "PKG_COURSEWISE_GRADE_FOR_EXCEL";
        string parameter = "@P_SESSIONNO,@P_SCHEMENO,@P_STUDENTTYPE,@P_SEMESTERNO";
        string Call_values = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + +Convert.ToInt32(ddlStuType.SelectedValue) + "," + ddlSemester.SelectedValue + "";
        ds = objCommon.DynamicSPCall_Select(proc_name, parameter, Call_values);
        // DataTable dt = new DataTable();
        //  dt = ds.Tables[1];
        //// DataSet ds3 = new DataSet();
        //  ds.Tables[0].Merge(dt);

        // return;
        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView gv = new GridView();
            gv.DataSource = ds.Tables[0];
            gv.DataBind();
            string attachment = "attachment;filename= FormatIIExcel" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "No Data Found", this.Page);
        }

    }

    protected void btnExamFeesPaid_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        string proc_name = "PKG_ACD_GET_STUDENT_FEES_PAID_LIST_FOR_EXCEL";
        string parameter = "@P_SESSIONNO";
        string Call_values = "" + ddlSession.SelectedValue + "";
        ds = objCommon.DynamicSPCall_Select(proc_name, parameter, Call_values);
        // DataTable dt = new DataTable();
        //  dt = ds.Tables[1];
        //// DataSet ds3 = new DataSet();
        //  ds.Tables[0].Merge(dt);

        // return;
        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView gv = new GridView();
            gv.DataSource = ds.Tables[0];
            gv.DataBind();
            string attachment = "attachment;filename= ExamFeePaidExcelReport" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "No Data Found", this.Page);
        }
    }

    protected void btnConvocationExcelReport_Click1(object sender, EventArgs e)     //Added Convocation Report dt on 11012022
    {
        bool CompleteRequest = false;
        try
        {
            SQLHelper objsql = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            DataSet ds = objsql.ExecuteDataSetSP("PKG_CONVOCATION_REPORT_NEW", objParams);

            GridView GVStatus = new GridView();
            string ContentType = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStatus.DataSource = ds;
                GVStatus.DataBind();

                string attachment = "attachment; filename= ConvocationExcelReport" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());

                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDataSessionWise_btnExcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        if (CompleteRequest)
            Response.End();

    }


    protected void btnConsolidateGradeCard_Click(object sender, EventArgs e)
    {
        //#region RCPIT Grad Card
        //if (Convert.ToInt32(Session["OrgId"]) == 1)
        //{

        //   // int count = 0;
        //    string ids = string.Empty;
        //    foreach (ListViewDataItem item in lvStudent.Items)
        //    {
        //        CheckBox chk = item.FindControl("chkStudent") as CheckBox;
        //        CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
        //        Label lblStudname = item.FindControl("lblStudname") as Label;

        //        string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
        //        if (chk.Checked)
        //        {
        //            ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";
        //            //count++;
        //            //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
        //        }
        //        else if (chk1.Checked)
        //        {
        //            ids = "0";
        //        }
        //        else
        //        {

        //        }


        //    }

        //    ids = ids.TrimEnd('.');

        //    this.ShowconsolitedGradeCardNew("Consolidate Grade Card", "MarksGradenew.rpt", ids);

        //}
        //#endregion



        #region RCPIT Grad Card
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {

            // int count = 0;

            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
                if (chk1.Checked)
                {
                    ids = "0";
                    flag = 1;
                    this.ShowconsolitedGradeCardNew("Consolidate Grade Card", "MarksGradenew.rpt", ids);
                    return;
                }


                CheckBox chk = item.FindControl("chkStudent") as CheckBox;

                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

                if (chk.Checked == true)
                {

                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                }

            }

            ids = ids.TrimEnd('.');

            this.ShowconsolitedGradeCardNew("Consolidate Grade Card", "MarksGradenew.rpt", ids);

        }

        #endregion



        #region For RCPIPER Grade Card added on 25/07/2023 by shubham

        else if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;


                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            ids = ids.TrimEnd('.');

            //this.ShowGradeCardNew("Consolidate Grade Card", "MarksGrade_RCPIPER.rpt", ids);

            this.ShowconsolitedGradeCardNew("Consolidate Grade Card", "MarksGrade_C_RCPIPER.rpt", ids);
        }
        #endregion


        #region For MIT Grade Card added on 18/09/2023 by Tejas Thakre
        else if (Convert.ToInt32(Session["OrgId"]) == 8)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }
            ids = ids.TrimEnd('.');

            //this.ShowGradeCardNew("Consolidate Grade Card", "MarksGrade_RCPIPER.rpt", ids);

            this.ShowconsolitedGradeCardNew("Consolidate Grade Card", "Consolidated_Grade_Card_MIT.rpt", ids);
        }
        #endregion
    }

    private void ShowconsolitedGradeCardNew(string reportTitle, string rptFileName, string ids)
    {
        try
        {
            //added on 20-02-2020 by Vaishali
            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {
                    count++;
                }
            }

            if (count > 0)
            {

                DateTime dateofissue = DateTime.MinValue;
                string dateofIssue = txtDateOfIssue.Text == "" ? string.Empty : (txtDateOfIssue.Text);
                if (dateofIssue == string.Empty)
                {
                    dateofIssue = "";
                    dateofissue = DateTime.Today;
                }
                else
                // DateTime dateofissue = Convert.ToDateTime(txtDateofIssue.Text);
                {

                    if (txtDateOfIssue.Text != "")
                    {
                        dateofissue = Convert.ToDateTime(txtDateOfIssue.Text);
                        txtDateOfIssue.Text = dateofissue.ToString("dd-MMM-yyyy");
                        // dateofissue =Convert.ToDateTime( txtDateofIssue.Text);
                    }
                    else
                    {
                        DateTime? dtt = null;
                        dateofissue = DateTime.Today;
                    }
                }
                string spec = string.Empty;
                string Result = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0 + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0 + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@DateofIssue=" + DateTime.Today.Date;
                }
                #region For RCPIPER Grade Card added on 25/07/2023 by shubham
                else if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0 + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;
                }
                #endregion
                #region For MIT Grade Card added on 18/09/2023 by Tejas Thakre
                else if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0 + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_IDNO=" + GetIDNOS_NEW();
                }
                #endregion
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowConsoli(string reportTitle, string rptFileName)
    {
        try
        {
            //added on 20-02-2020 by Vaishali
            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {

                    count++;

                }
            }

            if (count > 0)
            {
                //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                string date2 = Convert.ToDateTime(txtDateOfPublish.Text).ToString("dd/MM/yyyy"); // 03042023
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic"))); //ddlSemester.SelectedValue
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + 0 + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDeclareDate.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue + ",@P_PUBLISH_DATE=" + date2;
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
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

    protected void btnConsoli_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 2)
        {


            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //  string idnos = GetIDNOFGenerateGradeNo();

            int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            if (DurationCheck == 4)
            {

                this.ShowConsoli("Grade_Card", "rptGradeCardReportPG_Consoli.rpt");
            }
            else if (DurationCheck == 5)
            {
                this.ShowConsoli("Grade_Card", "rptGradeCardReportPG_Consoli_V.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Only Applicable for 4 and 5 year Programme Consolidated(B4) Grade Sheet !!!!", this.Page);
            }
        }
    }


    protected void btnConsoliA4_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 2)
        {


            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                    ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
            }

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int College_id = Convert.ToInt32(ViewState["college_id"]);
            int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int Branchno = Convert.ToInt32(ViewState["branchno"]);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //  string idnos = GetIDNOFGenerateGradeNo();

            int DurationCheck = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));

            if (DurationCheck == 2)
            {

                this.ShowConsoli("Grade_Card", "rptGradeCardReportPG_Consoli_IIA4.rpt");
            }
            else if (DurationCheck == 3)
            {
                this.ShowConsoli("Grade_Card", "rptGradeCardReportPG_Consoli_IIIA4.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Only Applicable for 2 and 3 year Programme Consolidated(A4) Grade Sheet !!!!", this.Page);
            }
        }

    }

    protected void btnLedgerReport_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(Session["OrgId"]) == 8)
        {

            this.ShowGradeCard("LEDGER_REPORT", "rptLedgerReport.rpt");

        }
    }

    protected void btnProvisionalDegree_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 3)
        {

            int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT ADMBATCH", "BRANCHNO=" + ViewState["branchno"] + "AND DEGREENO=" + ViewState["degreeno"]));

            string grade = objCommon.LookUp("ACD_SCHEME", "DISTINCT GRADEMARKS", "BRANCHNO=" + ViewState["branchno"] + "AND DEGREENO=" + ViewState["degreeno"] + "AND ADMBATCH=" + admbatch);

            if (Convert.ToInt32(ViewState["degreeno"]) == 13)
            {
                ShowPrvisionalDegree("Provisional Degree", "Provisional_Degree_Certificate_CPUKOTA_PHD.rpt");
            }
            else
            {
                if (grade == "G")
                {
                    ShowPrvisionalDegree("Provisional Degree", "Provisional_Degree_Certificate_CPUKOTA.rpt");
                }
                else
                {
                    ShowPrvisionalDegree("Provisional Degree", "Provisional_Degree_Certificate_CPUKOTA_MARK.rpt");
                }
            }

        }
    }

    private void ShowPrvisionalDegree(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            if (Convert.ToInt32(ViewState["degreeno"]) == 13)
            {
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"].ToString() + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            else
            {
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"].ToString() + ",@P_IDNO=" + GetIDNO();
            }

            //ViewState["schemeno"].ToString()
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


    protected void btnProgrssionrpt_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GDProgession = new GridView();
            string ContentType = string.Empty;
            DataSet ds = null;
            //ds = objQrC.GetBacklogStudentDataForDisplayInExcel();
            //    string proc_name = "GRADE_RANGE_REPORT";
            //string parameter = "@P_SESSIONNO,@P_DEGREENO,@P_SEMESTERNO";
            //string Call_values = "" + ddlSession.SelectedValue + "," + ViewState["degreeno"].ToString() + "," + ddlSemester.SelectedValue + "";

            string proc_name = "PKG_ACD_STUDENT_PROGRESSION_REPORT";
            string param = "@P_SCHEMENO,@P_YEAR";
            string call_values = "" + ViewState["schemeno"].ToString() + "," + ddlYear.SelectedValue + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);


            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GDProgession.DataSource = ds;
                GDProgession.DataBind();

                string attachment = "attachment; filename=ProgressionReport" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GDProgession.RenderControl(htw);
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


    protected void btnCount_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 8)  // for MIT CLIENT
        {
            ShowCountMaleFemale("Male_Female_Count", "rptMaleFemaleCount_MIT.rpt");
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 22) // for ADCET CLIENT
        {
            ShowCountMaleFemale("Male_Female_Count", "rptMaleFemaleCount_ADCET.rpt");
        }
    }

    private void ShowCountMaleFemale(string reportTitle, string rptFileName)
    {
        try
        {
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int Degree = Convert.ToInt32(ViewState["degreeno"].ToString());
            int Branch = Convert.ToInt32(ViewState["branchno"].ToString());

            string SP_Name = "PKG_ACAD_REPORT_MALE_FEMALE_COUNT_MIT";
            string SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_EXMTYPE,@P_SCHEMENO";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Degree + "," + Branch + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlStuType.SelectedValue) + "," + schemeno + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + schemeno + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_DEGREENO=" + Degree + ",@P_BRANCHNO=" + Branch + ",@P_EXMTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue);

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


    protected void btnSRNo_Click(object sender, EventArgs e)
    {
        GridView GVTrReport = new GridView();
        DataSet ds = null;

        //ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R WITH (NOLOCK) INNER JOIN ACD_STUDENT T WITH (NOLOCK) ON (T.IDNO = R.IDNO) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=R.IDNO AND R.SESSIONNO = TR.SESSIONNO  AND R.SEMESTERNO = TR.SEMESTERNO) inner join acd_COLLEGE_MASTER ACM on (ACM.college_id=T.college_id) inner join ACD_SCHEME AH on (AH.SCHEMENO=R.SCHEMENO) inner join acd_SESSION_MASTER ASM on (ASM.SESSIONNO=R.SESSIONNO) left join ACD_GRADE_SHEET AGS on (AGS.SCHEMENO=R.SCHEMENO and AGS.SESSIONNO=TR.SESSIONNO and AGS.SEMESTERNO = TR.SEMESTERNO and T.COLLEGE_ID=AGS.COLLEGE_ID and AGS.IDNO=R.IDNO)", "DISTINCT  R.IDNO, T.STUDNAME,R.REGNO,ACM.COLLEGE_NAME,AH.SCHEMENAME", "ASM.SESSION_NAME,R.SEMESTERNO AS SEMESTER,AGS.GRADE_SRNO,AGS.CONSOLIDATE_SRNO", "TR.IDNO <> 0 AND TR.SESSIONNO =" + ddlSession.SelectedValue + " AND TR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND R.EXAM_REGISTERED=1 and R.PREV_STATUS=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " or T.COLLEGE_ID= 0 ) AND (T.ADMCAN=0 or T.ADMCAN is null) " + " ", "R.REGNO");

        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R WITH (NOLOCK) INNER JOIN ACD_STUDENT T WITH (NOLOCK) ON (T.IDNO = R.IDNO) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=R.IDNO AND R.SESSIONNO = TR.SESSIONNO  AND R.SEMESTERNO = TR.SEMESTERNO) inner join acd_COLLEGE_MASTER ACM on (ACM.college_id=T.college_id) inner join ACD_SCHEME AH on (AH.SCHEMENO=R.SCHEMENO) inner join acd_SESSION_MASTER ASM on (ASM.SESSIONNO=R.SESSIONNO) left join ACD_GRADE_SHEET AGS on (AGS.SCHEMENO=R.SCHEMENO and AGS.SESSIONNO=TR.SESSIONNO and AGS.SEMESTERNO = TR.SEMESTERNO and T.COLLEGE_ID=AGS.COLLEGE_ID and AGS.IDNO=R.IDNO)", "DISTINCT  R.IDNO, T.STUDNAME,R.REGNO,ACM.COLLEGE_NAME,AH.SCHEMENAME", "ASM.SESSION_NAME,R.SEMESTERNO AS SEMESTER,AGS.GRADE_SRNO,AGS.CONSOLIDATE_SRNO", "TR.IDNO <> 0 AND S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND R.EXAM_REGISTERED=1  AND (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " or T.COLLEGE_ID= 0 ) AND (T.ADMCAN=0 or T.ADMCAN is null) " + " ", "R.REGNO");

        if (ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            //ds.Tables[0].Columns.RemoveAt(3);
            GVTrReport.DataSource = ds;
            GVTrReport.DataBind();
            string attachment = "attachment; filename=SerialNumber" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVTrReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
        }
    }
    protected void btnGradesheet_Click(object sender, EventArgs e)
    {
        try
        {

            string proc_name = "PKG_COURSEWISE_GRADE_FOR_EXCEL_JECRC";
            string param = "@P_SCHEMENO,@P_SESSIONNO,@P_SEMESTERNO,@P_STUDENTTYPE";
            string call_values = "" + ViewState["schemeno"].ToString() + "," + ddlSession.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlStuType.SelectedValue + "";
            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();
            GridView dg = new GridView();
            if (ds.Tables.Count > 0)
            {
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                AddReportHeader(dg);
                string attachment = "attachment; filename=" + "Result_Display_Sheet" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
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
                objCommon.DisplayMessage(this.updpnlExam, "No Data Found for this selection.", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void AddReportHeader(GridView gv)
    {
        try
        {


            //string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd-MM-yyyy");  //ToString("yyyy-MMyyyy");

            string finalsemster = string.Empty;
            string proc_name = "PKG_COURSEWISE_GRADE_FOR_EXCEL_JECRC_GET_UPPER_DATA";
            string param = "@P_SCHEMENO,@P_SESSIONNO,@P_SEMESTERNO,@P_STUDENTTYPE";
            string call_values = "" + ViewState["schemeno"].ToString() + "," + ddlSession.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlStuType.SelectedValue + "";
            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);

            int Semester = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString());
            if (Semester == 2 || Semester == 4 || Semester == 6 || Semester == 8 || Semester == 10)
            {
                finalsemster = "EVEN SEMESTERS EXAM";
            }
            else
            {
                finalsemster = "ODD SEMESTERS EXAM";
            }
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header1Cell = new TableCell();

            Header1Cell.Text = "JECRC UNIVERSITY";
            Header1Cell.ColumnSpan = 16;
            Header1Cell.Font.Size = 14;
            Header1Cell.Font.Bold = true;
            Header1Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow1.Cells.Add(Header1Cell);
            gv.Controls[0].Controls.AddAt(0, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header2Cell = new TableCell();

            Header2Cell.Text = "Academic Year:-" + ds.Tables[0].Rows[0]["ACADEMIC_YEAR"].ToString() + "," + "Academic Session:-" + ds.Tables[0].Rows[0]["SESSION_NAME"].ToString() + "," + "Exam :-" + finalsemster + "" + ds.Tables[0].Rows[0]["ACADEMIC_YEAR"].ToString();
            Header2Cell.ColumnSpan = 16;
            Header2Cell.Font.Size = 12;
            Header2Cell.Font.Bold = true;
            Header2Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(Header2Cell);
            gv.Controls[0].Controls.AddAt(1, HeaderGridRow2);


            GridViewRow HeaderGridRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header3Cell = new TableCell();

            Header3Cell.Text = "DISPLAY:-" + ds.Tables[0].Rows[0]["BRANCH"].ToString() + "    " + "" + ds.Tables[0].Rows[0]["ACADEMIC_YEAR"].ToString();
            Header3Cell.ColumnSpan = 16;
            Header3Cell.Font.Size = 12;
            Header3Cell.Font.Bold = true;
            Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow3.Cells.Add(Header3Cell);
            gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);



            GridViewRow HeaderGridRow4 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header4Cell = new TableCell();

            //Header3Cell.Text = "Report : Students Strength " + ddlHostelNo.SelectedItem.Text;
            //Header3Cell.ColumnSpan = 10;
            //Header3Cell.Font.Size = 12;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            gv.HeaderRow.Visible = true;

            //GridViewRow HeaderGridRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell Header3Cell = new TableCell();


            //Header3Cell.Text = "BLOCK";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "I YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "II YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "II (LE)";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "III YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "IV YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "TOTAL STRENGTH";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnufm_Click(object sender, EventArgs e)
    {
        try
        {
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            DataSet ds;
            string proc_name = "PKG_GET_STUD_UFM_STUDENT_PAGE";
            string parameter = "@P_SESSION_NO,@P_SCHEME_NO,@P_SEMESTER_NO";
            string Call_values = "" + ddlSession.SelectedValue + "," + schemeno + "," + ddlSemester.SelectedValue + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, parameter, Call_values);

            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView gv = new GridView();
                gv.DataSource = ds;
                gv.DataBind();
                string attachment = "attachment;filename= UFMList" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found", this.Page);
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

    // added by shubham on 18-08-2023
    protected void btnLedger_Click(object sender, EventArgs e)
    {
        try
        {
            ShowLedgerRegister("Student_Ledger_Report", "rptledger_RCPIT.rpt", 2);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.btnLedger_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowLedgerRegister(string reportTitle, string rptFileName, int param)
    {
        try
        {
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //url += "&param=@P_COLLEGEID=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + schemeno;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + schemeno;

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

    protected void btnGradeCardIssueRegister_Click(object sender, EventArgs e)
    {
        try
        {
            string proc_name = "PKG_GET_GRADE_CARD_ISSUE_REGISTER_REPORT";
            string param = "@P_SCHEMENO,@P_SESSIONNO,@P_SEMESTERNO";
            string call_values = "" + ViewState["schemeno"].ToString() + "," + ddlSession.SelectedValue + "," + ddlSemester.SelectedValue + "";

            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();
            GridView dg = new GridView();
            if (ds.Tables.Count > 0)
            {
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                //AddReportHeader(dg);
                string attachment = "attachment; filename=" + "GradeCardIssueRegister" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
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
                objCommon.DisplayMessage(this.updpnlExam, "No Data Found for this selection.", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private string GetIDNOS_NEW()
    {
        string retIDNO = string.Empty;
        int cntlength = 0;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk1 = lvStudent.Controls[0].FindControl("chkheader") as CheckBox;
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;

            if (chk.Checked)
            {
                if (retIDNO.Length == 0)
                {
                    retIDNO = lblStudname.ToolTip.ToString();
                }
                else
                {
                    retIDNO += "$" + lblStudname.ToolTip.ToString();
                }
                cntlength++;
            }
        }

        int cntid = Convert.ToInt32(Session["studcnt"]) == null ? 0 : Convert.ToInt32(Session["studcnt"]);
        if (cntid == cntlength)
        {
            retIDNO = "0";
        }
        if (retIDNO.Equals("")) return "0";
        else return retIDNO;

    }


    protected void btnElibilityReport_Click(object sender, EventArgs e)
    {
        try
        {
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int Yearno = Convert.ToInt32(ddlYear.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + "Eligibility Report";
            if (Yearno == 1)
            {
                url += "&path=~,Reports,Academic," + "rptSemPromotion_I.rpt";
            }
            else if (Yearno == 2)
            {
                url += "&path=~,Reports,Academic," + "rptSemPromotion_II.rpt";
            }
            else if (Yearno == 3)
            {
                url += "&path=~,Reports,Academic," + "rptSemPromotion_III.rpt";

            }
            else
            {
                url += "&path=~,Reports,Academic," + "rptSemPromotion_IV.rpt";
            }

            url += "&param=@P_SCHEMENO=" + schemeno + ",@P_YEAR	=" + Yearno;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.btnElibilityReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }



    }
    #region For Crescent Various Degree Certificate
    protected void btnCertificate_Click(object sender, EventArgs e)
    {

        try
        {
            string idno = GetIDNO();
            if (idno == "" || idno == "0")
            {
                objCommon.DisplayMessage(updpnlExam, "Please Select At least one Student!!", this.Page);
                return;
            }
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            string date3 = Convert.ToDateTime(txtprint.Text).ToString("MM/dd/yyyy");
            string date4 = Convert.ToDateTime(txtprint.Text).ToString("dd/MM/yyyy");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + "Degree Certificate";
            if (ViewState["degreeno"] == "2" || ViewState["degreeno"] == "4" || ViewState["degreeno"] == "6" || ViewState["degreeno"] == "7" || ViewState["degreeno"] == "11" || ViewState["degreeno"] == "17" || ViewState["degreeno"] == "19" || ViewState["degreeno"] == "19" || ViewState["degreeno"] == "20")
            {
                url += "&path=~,Reports,Academic," + "rptDegreeCertificate.rpt";//as per ticket number 49655 (Template 1)
            }
            else if (ViewState["degreeno"] == "5" || ViewState["degreeno"] == "8" || ViewState["degreeno"] == "9")
            {
                url += "&path=~,Reports,Academic," + "rptDegreeCertificate_Three.rpt";//as per ticket number 49655 (Template 3)
            }
            else
            {
                url += "&path=~,Reports,Academic," + "rptDegreeCertificate_Two.rpt";//as per ticket number 49655 (Template 2)
            }
            url += "&param=@P_SCHEMENO=" + schemeno + ",@P_DATE=" + date3 + ",@P_IDNO=" + GetIDNO() + ",@V_DATE=" + date4;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.btnCertificate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    #endregion

    #region For RCIPIPER Consolidated Grade Card M.PHARM 
    protected void btnConsolidtedMPHRAM_Click(object sender, EventArgs e)
    {
        string ids = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;


            string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
            if (chk.Checked)
            {
                ids += ((item.FindControl("lblStudname")) as Label).ToolTip + "$";

                //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
            }
        }
        ids = ids.TrimEnd('.');

   
        this.ShowconsolitedGradeCardNewRcipiperMPHARM("Consolidate Grade Card", "MarksGrade_C_MPHRAM_RCPIPER.rpt", ids);
    }
    #endregion

    private void ShowconsolitedGradeCardNewRcipiperMPHARM(string reportTitle, string rptFileName, string ids) //Added by Tejas Thakre as on 16_12_2023
    {
        try
        {

            int count = 0;
            foreach (ListViewItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
                {
                    count++;
                }
            }

            if (count > 0)
            {

                DateTime dateofissue = DateTime.MinValue;
                string dateofIssue = txtDateOfIssue.Text == "" ? string.Empty : (txtDateOfIssue.Text);
                if (dateofIssue == string.Empty)
                {
                    dateofIssue = "";
                    dateofissue = DateTime.Today;
                }
                else
                // DateTime dateofissue = Convert.ToDateTime(txtDateofIssue.Text);
                {

                    if (txtDateOfIssue.Text != "")
                    {
                        dateofissue = Convert.ToDateTime(txtDateOfIssue.Text);
                        txtDateOfIssue.Text = dateofissue.ToString("dd-MMM-yyyy");
                        // dateofissue =Convert.ToDateTime( txtDateofIssue.Text);
                    }
                    else
                    {
                        DateTime? dtt = null;
                        dateofissue = DateTime.Today;
                    }
                }
                string spec = string.Empty;
                string Result = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_IDNO=" + ids + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@DateofIssue=" + DateTime.Today.Date;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select at least one student !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //added by Shubham for CPUK AND CPUH Degree  Certificate 
    private void ShowReportDC(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            url += "&param=@P_IDNO=" + param;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.ShowReportDC() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // added by shubham on 04-01-2024
    protected void btnCIAExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string proc_name = "PKG_ACD_CIA_RESULT_ANALYSIS_REPORT_HITS";
            string param = "@P_SCHEMENO,@P_SEMESTERNO";
            string call_values = "" + ViewState["schemeno"].ToString() + "," + ddlSemester.SelectedValue + "";

            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);
            GridView dg = new GridView();
           if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dg.DataSource = ds.Tables[0];
                    dg.DataBind();
                    //AddReportHeader(dg);
                    string attachment = "attachment; filename=" + "CIA_Result_Analysis " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
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
                    objCommon.DisplayMessage(this.updpnlExam, "No Data Found for this selection.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "No Data Found for this selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.btnCIAExcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }

}
