//======================================================================================
// PROJECT NAME   : JSS                                                      
// MODULE NAME    : ACADEMIC                                                            
// PAGE NAME      : Coursewise Consolidate Report
// CREATION DATE  : 19/04/2018                                                         
// CREATED BY     : Snehal Wankhede                                                
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
//======================================================================================


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
using System.Data.SqlClient;
using System.IO;


using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Coursewise_Consolidate_Report : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController excol = new ExamController();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEGREENAME");
                else
                    // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

                objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                ddlSession.Focus();


                if (Session["usertype"].ToString() != "1")
                    //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");
                    objCommon.FillDropDownList(ddlColgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
                else

                    objCommon.FillDropDownList(ddlColgScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        // divMsg.InnerHtml = string.Empty;

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlsectionno.Items.Clear();
        ddlsectionno.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlColgScheme.SelectedIndex = 0;
    }
    protected void btnReport1_Click(object sender, EventArgs e)
    {
        //if (rdbExporttyye.SelectedIndex == 0)
        //{
        try
        {
            ShowReport("REPORT", "rptTabulationPG_New.rpt");
        }
        catch
        {
            throw;
        }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.updTeacher, "Please Select PDF as Export Type!!", this.Page);
        //}
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);//+ ",username=" + Session["username"].ToString();
            //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_DEGREENAME=" + ddlDegree.SelectedItem.Text;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        }
        ddlBranch.Focus();
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_STUDENT B WITH (NOLOCK) ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "A.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND B.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "A.SEMESTERNO ASC");
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlscheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.DEPTNO =" + Session["userdeptno"].ToString(), "B.BRANCHNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlscheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
            }
            //  ddlSemester.Focus();
        }
        else
        {
            objCommon.DisplayMessage("Please Select Branch!", this.Page);
            ddlBranch.Focus();
        }
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlscheme.SelectedValue) > 0)
        {
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_STUDENT B WITH (NOLOCK) ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND B.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND A.SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue), "A.SEMESTERNO ASC");
        }
    }
    protected void btnconmksrpt_Click(object sender, EventArgs e)
    {
        //if (rdbExporttyye.SelectedIndex == 0)
        //{
        //    try
        //    {
        //        this.ShowReportConsolidated("CONSOLIDATEDREPORT", "rptConsolidatedsemwisereport.rpt");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //else
        //{
        try
        {
            this.ExportConsolidatedandMarksDataExcel();
        }
        catch
        {
            throw;
        }
        //}
    }
    private void ShowReportConsolidated(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlsectionno.SelectedValue);
            //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_DEGREENAME=" + ddlDegree.SelectedItem.Text;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ExportConsolidatedandMarksDataExcel()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = excol.GetConsolidatedMarksandAttendanceList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlsectionno.SelectedValue));
            if (ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this.updTeacher, "No Record Found For Your Selection!", this.Page);
            }
            else
            {
                dt = ds.Tables[0];
                if (dt.Columns.Contains("ROLNO"))
                {
                    dt.Columns.Remove("ROLNO");
                }
                if (dt.Columns.Contains("ENROLLMENT_NO"))
                {
                    dt.Columns.Remove("ENROLLMENT_NO");
                }
                if (dt.Columns.Contains("COURSEGROUP"))
                {
                    dt.Columns.Remove("COURSEGROUP");
                }
                GridView GVDayWiseAtt = new GridView();
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=StudentAttendance_Marks_Excel_Report" + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnconmksrptexcel_Click(object sender, EventArgs e)
    {
        this.ExportConsolidatedandMarksDataExcel();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSemester.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlsectionno, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SECTION SC WITH (NOLOCK) ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SECTIONNO > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SC.SECTIONNAME");
        }
        else
        {
            ddlsectionno.Items.Clear();
            ddlsectionno.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlColgScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlColgScheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlColgScheme.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlColgScheme.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlsectionno.Items.Clear();
            ddlsectionno.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_STUDENT B WITH (NOLOCK) ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND B.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "AND B.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "AND A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "A.SEMESTERNO ASC");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlsectionno.Items.Clear();
            ddlsectionno.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}