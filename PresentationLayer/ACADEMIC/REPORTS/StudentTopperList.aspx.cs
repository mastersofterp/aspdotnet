// PROJECT NAME  : UAIMS                                                   
// MODULE NAME   : Exam Topper List                                       
// CREATION DATE : 07/09/2012                                                    
// CREATED BY    : ASHISH DHAKATE                                
// MODIFIED BY   :  UMESH G.                                                   
// MODIFIED DESC :  AS PER NIT GOA REQUIREMENT      
//=================================================================================

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
using System.IO;

public partial class ACADEMIC_REPORTS_StudentTopperList : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DataSet dsShowData = null;


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
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                Populatedropdownlist();
                ddlSession.Focus();
                divAdmission.Visible = false;
                rfvBranch.Enabled = false;
                //rfvSession.Enabled = false;  //commented on 22-04-2020 by Vaishali

                //added on 22-04-2020 by Vaishali
                spBranch.Visible = false;
                spScheme.Visible = false;
                rfvSession.Enabled = true;  //modified on 22-04-2020 by Vaishali
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
                Response.Redirect("~/notauthorized.aspx?page=StudentTopperList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentTopperList.aspx");
        }
    }
    private void Populatedropdownlist()
    {
        try
        {
            //***objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO"); // commented on 22-04-2020 by Vaishali
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO < 9", "SEMESTERNO");
            //**objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_ID");
            //***objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT C.COLLEGE_ID", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");

            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResultList.Populatedropdownlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //ddlSem.Items.Clear();
            //ddlSem.Items.Add(new ListItem("Please Select", "0"));           
            ddlBranch.Items.Clear();

            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
                ddlBranch.Focus();
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT_HIST SR WITH (NOLOCK), ACD_SEMESTER S WITH (NOLOCK)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO"); // added on 22-04-2020 by Vaishali
            }
            else
            {
                ddlDegree.SelectedIndex = 0;
            }

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
            //ddlSem.Items.Clear();
            //ddlSem.Items.Add(new ListItem("Please Select", "0"));           
            if (ddlBranch.SelectedIndex > 0)
            {

                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT_HIST SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + "AND BRANCHNO = " + ddlBranch.SelectedValue + " AND SCHEMENO <> 0", "SCHEMENO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlScheme.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_STUDENT_RESULT_HIST B ON (A.SEMESTERNO = B.SEMESTERNO)", "DISTINCT B.SEMESTERNO", "A.SEMESTERNAME", "B.SEMESTERNO > 0 AND B.SESSIONNO=" + ddlSession.SelectedValue + " AND B.SCHEMENO=" + ddlScheme.SelectedValue + " AND PREV_STATUS=0", "B.SEMESTERNO");
        //    ddlSem.Focus();

        //}
        //else
        //{
        //    ddlSem.Items.Clear();
        //    ddlSem.Items.Add(new ListItem("Please Select", "0"));

        //}
    }

    private void ClearAllDropDowns()
    {

        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlMeritList.SelectedIndex = 0;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(rdoPurpose.SelectedValue) == 1)
            {
                ShowReport(rdoReportType.SelectedValue, "rptBranchwiseTopperList.rpt");
            }

            else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 2)
            {
                MeritShowReport(rdoReportType.SelectedValue, "rptTopperMeritList.rpt");
            }
            else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 3)
            {
                BranchMeritShowReport(rdoReportType.SelectedValue, "rptBranchTopperMeritList.rpt");

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_REPORT_STUDENTTOPPERLIST.btnReport_Click->" + ex.Message + " " + ex.Message);
            }
            else
            {
                objCommon.ShowError(Page, "Server UnAvailable");

            }
        }
    }




    //GridView GVStudData = new GridView();


    //DataSet ds = objCommon.DynamicSPCall_Select("PKG_EXAM_BACKLOG_REGISTARTION_COURSEWISE_COUNT_REPORT", "@P_SESSIONNO,@P_SEMESTERNO", "" + Convert.ToInt32(ddlClgname.SelectedValue) + "," + ddlSemester.SelectedValue + "");

    //if (ds.Tables[0].Rows.Count > 0)
    //{
    //    GVStudData.DataSource = ds;
    //    GVStudData.DataBind();

    //    string attachment = "attachment;filename=SubjectArrearListReport.xls";
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.Charset = "";
    //    Response.ContentType = "application/ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    GVStudData.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();

    //Method for Show topper list Report 
    private void ShowReport(string exporttype, string rptFileName)
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
            url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue;

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

            //Response.End();
            //Response.Flush();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Method for Show Convocation topper list Report 
    private void ConveShowReport(string exporttype, string rptFileName)
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
            url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text;


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Method for Show Merit topper list Report 
    private void MeritShowReport(string exporttype, string rptFileName)
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
            url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;//+ ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" 
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    //Method for Show Branch wise Merit topper list Report 
    private void BranchMeritShowReport(string exporttype, string rptFileName)
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
            url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    //Method for Show convocation label report
    private void ConvLabelShowReport(string exporttype, string rptFileName)
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
            url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void rdoReportType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rdoPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rdoPurpose.SelectedValue) == 1)
        {
            rfvScheme.Enabled = false;
            //rfvSession.Enabled = false; //commented on 22-04-2020 by Vaishali
            rfvSession.Enabled = true;  //modified on 22-04-2020 by Vaishali
            rfvBranch.Enabled = false;
            rfvMeritList.Enabled = true;

            divBranch.Visible = true;
            divScheme.Visible = true;
            divSemester.Visible = true;
            divReport.Visible = true;
            // divAdmission.Visible = true;
            divSession.Visible = true;

            //added on 22-04-2020 by Vaishali
            spBranch.Visible = false;
            spScheme.Visible = false;

            divAdmission.Visible = false;
            ClearAllDropDowns();
        }

        else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 2)
        {
            rfvScheme.Enabled = false;
            rfvSession.Enabled = true;

            divBranch.Visible = false;
            divScheme.Visible = false;
            divSemester.Visible = true;
            divReport.Visible = true;
            divAdmission.Visible = true;
            divSession.Visible = true;

            //divAdmission.Visible = false;
            ClearAllDropDowns();
        }
        else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 3)
        {
            rfvScheme.Enabled = false;
            rfvSession.Enabled = true;

            divBranch.Visible = false;
            divScheme.Visible = false;
            divSemester.Visible = true;
            divReport.Visible = true;
            divAdmission.Visible = true;
            divSession.Visible = true;

            //divAdmission.Visible = false;
            ClearAllDropDowns();
        }

    }

    // added on 22-04-2020 by Vaishali
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON A.DEGREENO = B.DEGREENO", "DISTINCT B.DEGREENO", "DEGREENAME", "A.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND B.DEGREENO > 0", "B.DEGREENO");
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {
            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            }
        }





    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlSession.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            ddlSem.Focus();
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }





    }
}
