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
 
            // added by shubham B on 02-03-2024
            //ddlCollege.SelectedIndex = 0;
            //ddlAdmBatch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlMeritList.SelectedIndex = 0;
            //end
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

            // added by shubham B on 02-03-2024
            //ddlCollege.SelectedIndex = 0;
            //ddlAdmBatch.SelectedIndex = 0;
            //ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlMeritList.SelectedIndex = 0;
            //end

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
        ddlClgname.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlMeritList.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        txtTopcnt.Text = string.Empty;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(txtTopcnt.Text) || Convert.ToInt32(txtTopcnt.Text) == 0)
            {
                ViewState["TopCnt"] = 0;
            }
            else 
            {
                ViewState["TopCnt"] = txtTopcnt.Text;
            }
            if (Convert.ToInt32(rdoPurpose.SelectedValue) == 1)
            {
                // Coded by Shubham on 140324 TID 56424
                if (rdoReportType.SelectedValue == "pdf")
                {
                    ShowReportPdf(rdoReportType.SelectedValue, "rptBranchwiseTopperList.rpt");
                }
                else if (rdoReportType.SelectedValue == "xls")
                {
                    ShowReport(rdoReportType.SelectedValue, "rptBranchwiseTopperList.rpt");
                }

               // ShowReport(rdoReportType.SelectedValue, "rptBranchwiseTopperList.rpt");
            }

            else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 2)
            {
                //MeritShowReport(rdoReportType.SelectedValue, "rptTopperMeritList.rpt");
                // Coded by Shubham on 140324 TID 56424
                if (rdoReportType.SelectedValue == "pdf")
                {
                    MeritShowReport1(rdoReportType.SelectedValue, "rptTopperMeritList.rpt");
                }
                else if (rdoReportType.SelectedValue == "xls")
                {
                    MeritShowReport(rdoReportType.SelectedValue, "rptTopperMeritList.rpt");
                }
                
                
            }
            else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 3)
            {
                // Coded by Shubham on 140324 TID 56424
                if (rdoReportType.SelectedValue == "pdf")
                {
                    BranchMeritShowReportPdf(rdoReportType.SelectedValue, "rptBranchTopperMeritList.rpt");
                }
                else if (rdoReportType.SelectedValue == "xls")
                {
                    BranchMeritShowReport(rdoReportType.SelectedValue, "rptBranchTopperMeritList.rpt");
                }

                //BranchMeritShowReport(rdoReportType.SelectedValue, "rptBranchTopperMeritList.rpt");

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
            url += "exporttype=" + exporttype; // comment by shubham 04032024 
            url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_TOPCNT=" + Convert.ToInt32(ViewState["TopCnt"]);

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

    private void ShowReportPdf(string reportTitle, string rptFileName)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            // url += "exporttype=" + exporttype; // comment by shubham 04032024 
            url += "pagetitle=" + reportTitle;
            // url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_TOPCNT=" + Convert.ToInt32(ViewState["TopCnt"]);

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
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text;


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
    // added by shubham on 14-03-2024 TID 56424
    private void MeritShowReport1(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_TOPCNT=" + Convert.ToInt32(ViewState["TopCnt"]) + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

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
            //url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_TOPCNT=" + Convert.ToInt32(ViewState["TopCnt"]) + "";
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

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
            //url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
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

    private void BranchMeritShowReportPdf(string reportTitle, string rptFileName)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
           // url += "exporttype=" + exporttype;
            url += "pagetitle=" + reportTitle;
            //url += "&filename=" + ddlClgname.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text + ",@P_BATCHNAME=" + ddlAdmBatch.SelectedItem.Text + ",@P_SESSIONNO=" + ddlSession.SelectedValue + "";
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
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_ORDER_BY=" + ddlMeritList.SelectedValue + ",@P_MERIT=" + ddlMeritList.SelectedItem.Text;
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
            divRange.Visible = true; // Added by shubham 
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
            divRange.Visible = true; // Added by shubham 
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
            divRange.Visible = false; // Added by shubham 
            //divAdmission.Visible = false;
            ClearAllDropDowns();
        }

    }

    // added on 22-04-2020 by Vaishali
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // added by shubham B on 02-03-2024
        ddlCollege.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlMeritList.SelectedIndex = 0;
        //end

        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON A.DEGREENO = B.DEGREENO", "DISTINCT B.DEGREENO", "DEGREENAME", "A.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND B.DEGREENO > 0", "B.DEGREENO");
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Common objCommon = new Common();

        // added by shubham B on 02-03-2024
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlMeritList.SelectedIndex = 0;
        // end

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
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSION_PNAME ", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            }
        }





    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        // added by shubham B on 02-03-2024
        ddlCollege.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlMeritList.SelectedIndex = 0;
        // end

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

    //added by shubham On 14-03-23 TID 56424
    private void ShowReportExcel(string exporttype, string rptFileName)
    {
        //try
        //{
        //    int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
        //    string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
        //    string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
        //    string attachment = "attachment; filename=" + "InvigilationDutyEntry" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/" + "ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //    SqlParameter[] objParams = new SqlParameter[] 
        //   { 
        //       new SqlParameter("@P_SESSIONNO", Convert.ToInt32(SessionNo)),
        //       new SqlParameter("@P_EXAM_NO", Convert.ToInt32(ddlExTTType.SelectedValue)),
        //       new SqlParameter("@P_EXAM_DATE", EXAMDATE),
              
        //   };
        //    DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_INVIGILATION_DUTY", objParams);
        //    if (dsfee.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable dt = dsfee.Tables[0];
        //        foreach (DataColumn dc in dt.Columns)
        //        {

        //        }
        //        DataGrid dg = new DataGrid();

        //        if (dsfee.Tables.Count > 0)
        //        {
        //            dg.DataSource = dsfee.Tables[0];
        //            dg.DataBind();

        //        }
        //        dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //        dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
        //        dg.HeaderStyle.Font.Bold = true;
        //        dg.RenderControl(htw);
        //        Response.Write(sw.ToString());
        //        Response.End();
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(updInvigDuty, "No Data Found for current selection.", this.Page);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    //added by shubham On 14-03-23 TID 56424
    private void ShowReportWord(string exporttype, string rptFileName)
    {
    //    try
    //    {
    //        int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
    //        string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
    //        string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
    //        string attachment = "attachment; filename=" + "InvigilationDutyEntry.doc";

    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", attachment);
    //        Response.ContentType = "application/" + "ms-excel";
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);

    //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //        SqlParameter[] objParams = new SqlParameter[] 
    //       { 
    //           new SqlParameter("@P_SESSIONNO", Convert.ToInt32(SessionNo)),
    //           new SqlParameter("@P_EXAM_NO", Convert.ToInt32(ddlExTTType.SelectedValue)),
    //           new SqlParameter("@P_EXAM_DATE",EXAMDATE) ,
              
    //       };
    //        DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_INVIGILATION_DUTY", objParams);
    //        if (dsfee.Tables[0].Rows.Count > 0)
    //        {
    //            DataTable dt = dsfee.Tables[0];
    //            foreach (DataColumn dc in dt.Columns)
    //            {

    //            }
    //            DataGrid dg = new DataGrid();

    //            if (dsfee.Tables.Count > 0)
    //            {
    //                dg.DataSource = dsfee.Tables[0];
    //                dg.DataBind();

    //            }
    //            dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //            dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
    //            dg.HeaderStyle.Font.Bold = true;
    //            dg.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updInvigDuty, "No Data Found for current selection.", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    }
}
