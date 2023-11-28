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
using BusinessLogicLayer.BusinessLogic.Academic.MentorMentee;

public partial class ACADEMIC_EXAMINATION_TabulationChart : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TabularChartController_MM objQrC = new TabularChartController_MM();
    bool IsDataPresent = false;

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
            }
            int orgID = Convert.ToInt32(objCommon.LookUp("REFF", "OrganizationId", ""));
            if (orgID == 3 || orgID == 4)
            {
                btnConsolidateGradeCard.Visible = false;
                btnGradeCard.Visible = false;
            }
            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                //btncoursegrade.Visible = true;
                //////btncoursegrade.Visible = true;
                //btnExcel.Visible = true;
                //btnExamFeesPaid.Visible = true;
                //btnConvocationExcelReport.Visible = true;
                btnResultStatistics.Visible = false;
                btnGradeRegister.Visible = false;
                btnGrace.Visible = false;
                Yearid.Visible = false;
                //btnConsolidateGradeCard.Visible = false;
                Dateissue.Visible = true;
                //btnConsoli.Visible = true;
                ////btnConsoliA4.Visible = true;
                btnLedgerReport.Visible = false;
                btnProgrssionrpt.Visible = false;
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                //btncoursegrade.Visible = false;
                //////btncoursegrade.Visible = false;
                //btnExcel.Visible = false;
                //btnExamFeesPaid.Visible = false;
                //btnConvocationExcelReport.Visible = false;
                btnResultStatistics.Visible = false;
                btnGradeRegister.Visible = false;
                btnGrace.Visible = false;
                Yearid.Visible = true;
                BindYear();
                //btnConsolidateGradeCard.Visible = true;
                Dateissue.Visible = true;
                //btnConsoli.Visible = false;
                ////btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;
                btnProgrssionrpt.Visible = false;

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 8)
            {

                btnLedgerReport.Visible = true;
                //btnConsoli.Visible = false;
                ////btnConsoliA4.Visible = false;
                btnProgrssionrpt.Visible = false;
                btnCount.Visible = true;

            }
            else
            {
                //btncoursegrade.Visible = false;
                //////btncoursegrade.Visible = false;
                //btnExcel.Visible = false;
                //btnExamFeesPaid.Visible = false;
                //btnConvocationExcelReport.Visible = false;
                Yearid.Visible = false;
                //btnConsolidateGradeCard.Visible = false;
                Dateissue.Visible = false;
                //btnConsoli.Visible = false;
                ////btnConsoliA4.Visible = false;
                btnLedgerReport.Visible = false;
                btnProgrssionrpt.Visible = false;
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
            ddlYear.Focus();


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


        string deptno = string.Empty;
        if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
            deptno = "0";
        else
            deptno = Session["userdeptno"].ToString();

        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
       // objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

    }

    private void ClearAllDropDowns()
    {
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

        DataSet ds = null;
        if (ddlSession.SelectedIndex > 0 && ddlSemester.SelectedIndex > 0 && Convert.ToInt32(ViewState["branchno"]) > 0 && Convert.ToInt32(ViewState["branchno"]) > 0)
        {

           // ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R WITH (NOLOCK) INNER JOIN ACD_STUDENT T WITH (NOLOCK) ON (T.IDNO = R.IDNO) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=R.IDNO AND R.SESSIONNO = TR.SESSIONNO  AND R.SEMESTERNO = TR.SEMESTERNO)", "DISTINCT R.IDNO", "T.STUDNAME,R.REGNO,T.ADMBATCH,T.EMAILID_INS", "TR.IDNO <> 0 AND TR.SESSIONNO =" + ddlSession.SelectedValue + " AND TR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND R.EXAM_REGISTERED=1 and R.PREV_STATUS=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " or T.COLLEGE_ID= 0 ) " + " ", "R.REGNO");

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R WITH (NOLOCK) INNER JOIN ACD_STUDENT T WITH (NOLOCK) ON (T.IDNO = R.IDNO) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=R.IDNO AND R.SESSIONNO = TR.SESSIONNO  AND R.SEMESTERNO = TR.SEMESTERNO)", "DISTINCT R.IDNO", "T.STUDNAME,R.REGNO,T.ADMBATCH,T.EMAILID", "TR.IDNO <> 0 AND TR.SESSIONNO =" + ddlSession.SelectedValue + " AND TR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND R.EXAM_REGISTERED=1 and R.PREV_STATUS=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.FAC_ADVISOR=" + (Session["userno"]) + " and (T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " or T.COLLEGE_ID= 0 ))" + "", "R.REGNO");

         //   ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST R WITH (NOLOCK) INNER JOIN ACD_STUDENT T WITH (NOLOCK) ON (T.IDNO = R.IDNO) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (S.SCHEMENO = R.SCHEMENO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=R.IDNO AND R.SESSIONNO = TR.SESSIONNO  AND R.SEMESTERNO = TR.SEMESTERNO)", "DISTINCT R.IDNO", "T.STUDNAME,R.REGNO,T.ADMBATCH,T.EMAILID_INS", "TR.IDNO <> 0 AND TR.SESSIONNO =" + ddlSession.SelectedValue + " AND TR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND S.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND R.EXAM_REGISTERED=1 and R.PREV_STATUS=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND (cancel=0 or cancel is null) and (T.FAC_ADVISOR=" + (Session["userno"]) + " and (T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " or T.COLLEGE_ID= 0 )" + "", "");
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

            this.ShowGradeCardNew("Grade Card", "MarksGrade_RCPIT_MM.rpt", ids);


        }
        #endregion
        #region For Crescent

        else if (Convert.ToInt32(Session["OrgId"]) == 2) //Comment for common code test orgid        
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
            objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);
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
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_NEW.rpt"); //added on 070922 for only show 10 semester
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
                    this.ShowGradeCard("Grade_Card", "rptGradeCardReportPG_third_MM.rpt"); //added on 070922 for only show 6 semester
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
                    url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["UserNo"]);
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

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
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6) // rcpiper added by shubham on 17/01/2023
                {
                    url += "&param=@P_IDNO=" + ids + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_STUDTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_DATEOFISSUE=" + DateTime.Today.Date + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue);
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
        //string declaredDate = string.Empty;
        //string dateOfIssue = string.Empty;
        //declaredDate = txtDeclareDate.Text;
        //dateOfIssue = txtDateOfIssue.Text;

        //DataSet ds1 = objQrC.GetStudentDataForQRCode(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlCollege.SelectedValue), Convert.ToInt16(ddlDegree.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), (stuadmbatch.Value).ToString(), declaredDate, dateOfIssue, idno, Session["userfullname"].ToString());
        //if (ds1.Tables[0].Rows.Count > 0)
        //{
        //    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
        //    {
        //        string Qrtext = "SerialNo: " + ds1.Tables[0].Rows[i]["GRADE_SR_NO"] +
        //               "; StudName: " + ds1.Tables[0].Rows[i]["STUDNAME"].ToString().Trim() +
        //               "; RegNo: " + ds1.Tables[0].Rows[i]["REGNO"] +
        //               "; RollNo: " + ds1.Tables[0].Rows[i]["ROLLNO"] +
        //               "; Programme: " + ds1.Tables[0].Rows[i]["BRANCH"] +
        //               "; Semester: " + ds1.Tables[0].Rows[i]["SEMESTER"] +
        //               "; SGPA: " + ds1.Tables[0].Rows[i]["SGPA"] +
        //                "";

        //        Session["qr"] = Qrtext.ToString();
        //        QRCodeEncoder encoder = new QRCodeEncoder();
        //        encoder.QRCodeVersion = 10;
        //        Bitmap img = encoder.Encode(Session["qr"].ToString());
        //        //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
        //        img.Save(Server.MapPath("~\\img.Jpeg"));
        //        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");
        //        //img.Save(Server.MapPath("~\\img.Jpeg"));
        //        byte[] QR_IMAGE = ViewState["File"] as byte[];
        //        //  long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds1.Tables[0].Rows[i]["IDNO"].ToString().Trim()), QR_IMAGE);
        //    }
        //}
        //else
        //{
        //    IsDataPresent = true;
        //    objCommon.DisplayUserMessage(updpnlExam, "Grade card can not be generated as nobody passed in selected examination!", this.Page);
        //    return;
        //}
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
                    string date1 = date.ToString("d MMM yyyy"); //added on 29032023
                    //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue + ",@P_EXAM_DATE=" + date1 + ", @P_FACTADVISOR = " + Convert.ToInt32(Session["UserNo"]);

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value);

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
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                //   url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + stuadmbatch.Value + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + stuadmbatch.Value + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + stuadmbatch.Value + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDateOfIssue.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
                }

                else if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();// +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 9)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO();//",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

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
                url += "&param=@P_COLLEGEID=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",	@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
            }
            else if (param == 2)
            {
                //url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PUBLISH_DATE=" + txtDateOfIssue.Text + ",@P_HELD_IN_DATE=" + txtDeclareDate.Text;//Modify by Maithili [24/01/2020]

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_STUDENTTYPE=" + Convert.ToInt32(ddlStuType.SelectedValue) + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_PUBLISH_DATE=" + DateTime.Now.ToString("MM/dd/yyyy");

            }
            else if (param == 3)
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_FACTADVISOR=" + Convert.ToInt32(Session["UserNo"]);
            }
            else if (param == 4)
            {
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_FACTADVISOR=" + Convert.ToInt32(Session["UserNo"]) + ",@P_COURSENO=0";
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

        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            string ids = string.Empty;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblStudname = item.FindControl("lblStudname") as Label;

                //string 
                string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));

            }


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
                objMarkEntry.GradeCardNumberGeneration(Sessionno, idnos, College_id, Degreeno, Branchno, Semesterno, ua_no);

                int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + Degreeno + " AND BRANCHNO =" + Branchno + " AND COLLEGE_ID=" + College_id));
                if (duration == Semesterno)
                {
                    this.ShowGradeCard("Grade_Card", "rptGradeCardFinalReportPG_WithoutHeader.rpt");
                }
                else
                {
                    //  this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReportPG_WithoutHeader.rpt");
                    this.ShowGradeCardWithoutHeader("Grade_Card_Without_header", "rptGradeCardReport_withoutheader_jecrc.rpt");

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

    protected void btnGradeCardBackReport_Click(object sender, EventArgs e)
    {

    }
    protected void btnCourceWiseFailStudList_Click(object sender, EventArgs e)
    {

        ShowTR("CoursewiseFailStudentList", "rptFGradeResultAnalysis_MM.rpt", 4);

    }
    protected void btnFailStudentList_Click(object sender, EventArgs e)
    {

        ShowTR("FailStudentList", "rptStudentFailedList_MM.rpt", 3);
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
    }
    protected void btnResultExcel_Click(object sender, EventArgs e)
    {
        //try
        //{


        //    GridView GVDayWiseAtt = new GridView();
        //    string ContentType = string.Empty;

        //    DataSet ds = null;
        //    ds = objQrC.GetStudentDataForDisplayInExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ddlSemester.SelectedValue));

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //ds.Tables[0].Columns.RemoveAt(3);
        //        GVDayWiseAtt.DataSource = ds;
        //        GVDayWiseAtt.DataBind();

        //        string attachment = "attachment; filename=ResultReport.xls";
        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", attachment);
        //        Response.ContentType = "application/vnd.MS-excel";
        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        GVDayWiseAtt.RenderControl(htw);
        //        //lvStudApplied.RenderControl(htw);
        //        Response.Write(sw.ToString());
        //        Response.End();
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
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
            ds = objQrC.GetBacklogStudentDataForDisplayInExcel(Convert.ToInt32(Session["UserNo"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=OverallBacklogReport.xls";
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
        if (ddlSession.SelectedIndex > 0)
        {
            try
            {

                GridView GVTrReport = new GridView();
                string ContentType = string.Empty;

                int Sessionno = ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;

                int College_id = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["college_id"]) : 0;
                int Degreeno = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["degreeno"]) : 0;
                int Branchno = ddlClgname.SelectedIndex > 0 ? Convert.ToInt32(ViewState["branchno"]) : 0;

                int student_type = ddlStuType.SelectedIndex > 0 ? Convert.ToInt32(ddlStuType.SelectedValue) : 0;
                int semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
                int FactAdvsor = Convert.ToInt32(Session["UserNo"]);
                DataSet ds = null;
                ds = objQrC.GetTrReportStudentDetails(Sessionno, College_id, Degreeno, Branchno, student_type, semesterno, FactAdvsor);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].Columns.RemoveAt(3);
                    GVTrReport.DataSource = ds;
                    GVTrReport.DataBind();

                    string attachment = "attachment; filename=TRReport.xls";
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
        else
        {
            objCommon.DisplayMessage(updpnlExam, "Please Select Session", this.Page);
        }
    }
    protected void btnResultStatistics_Click(object sender, EventArgs e)
    {

        //int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //int Prev_Status = Convert.ToInt32(ddlStuType.SelectedValue);
        //DataSet ds = objQrC.GetResultStatistics(Sessionno, Prev_Status);
        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    GridView gv = new GridView();
        //    gv.DataSource = ds;
        //    gv.DataBind();
        //    string attachment = "attachment;filename= ResultStatistics.xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    gv.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updpnlExam, "No Data Found", this.Page);
        //}
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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0  AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

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

    //protected void //btncoursegrade_Click(object sender, EventArgs e)
    //{
    //    //ShowTR("Course_wise_Grade", "rptCourse_wise_Grade.rpt", 6);
    //    ShowTR("Course_wise_Grade", "rptCourseWise_Grade.rpt", 7);
    //}
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

    protected void btncoursegrade_Click(object sender, EventArgs e)
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
            string attachment = "attachment;filename= GradeRange.xls";
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
            string attachment = "attachment;filename= FormatIIExcel.xls";
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
            string attachment = "attachment;filename= ExamFeePaidExcelReport.xls";
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

                string attachment = "attachment; filename= ConvocationExcelReport.xls";
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
                objUCommon.ShowError(Page, "ACADEMIC_StudentDataSessionWise_//btnExcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        if (CompleteRequest)
            Response.End();

    }

    protected void btnConsolidateGradeCard_Click(object sender, EventArgs e)
    {
        #region RCPIT Grad Card
        //if (Convert.ToInt32(Session["OrgId"]) == 1)
        //{

        // ShowReport_GradeCard("GradeCard", "rptGradeCardSemesterwise.rpt");
        // this.ShowGradeCard("Grade_Card", "rptTabulationRegistarStud.rpt");
        string ids = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;
            //if (chk.Checked == true)
            //{
            string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
            if (chk.Checked)
            {
                ids += ((item.FindControl("lblStudname")) as Label).ToolTip + ".";

                //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
            }
            // }
            //else
            //{
            //    objCommon.DisplayUserMessage(updpnlExam, "Please Select Student!", this.Page);
            //}

        }
        ids = ids.TrimEnd('.');

        this.ShowconsolitedGradeCardNew("Consolidate Grade Card", "MarksGradenew_MM.rpt", ids);

        //}
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
                    url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_IDNO=" + ids + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_SEMESTERNO=" + 0 + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@DateofIssue=" + DateTime.Today.Date + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["userno"]);

                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
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
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic"))); //ddlSemester.SelectedValue
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + 0 + ",@P_IDNO=" + GetIDNO() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToString(stuadmbatch.Value) + ",@P_RESULTDECLAREDDATE=" + txtDeclareDate.Text.ToString() + ",@P_DATE_OF_ISSUE=" + txtDeclareDate.Text.ToString() + ",@P_USER_FUll_NAME=" + Session["userfullname"] + ",@P_PREV_STATUS=" + ddlStuType.SelectedValue;
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

            this.ShowConsoli("Grade_Card", "rptGradeCardReportPG_Consoli.rpt");

        }


    }
    protected void btnConsoliA4_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 2)
        {

            this.ShowConsoli("Grade_Card", "rptGradeCardReportPG_Consoli_A4.rpt");


        }


    }
    protected void btnLedgerReport_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(Session["OrgId"]) == 8)
        {


            this.ShowGradeCard("LEDGER_REPORT", "rptLedgerReport.rpt");



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

                string attachment = "attachment; filename=OverallBacklogReport.xls";
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
        ShowCountMaleFemale("Male_Female_Count", "rptMaleFemaleCount_MIT.rpt");
    }

    private void ShowCountMaleFemale(string reportTitle, string rptFileName)
    {
        try
        {
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int Degree = Convert.ToInt32(ViewState["degreeno"].ToString());
            int Branch = Convert.ToInt32(ViewState["branchno"].ToString());

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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
