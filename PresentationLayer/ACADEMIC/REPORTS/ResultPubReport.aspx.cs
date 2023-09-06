using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_REPORTS_ResultPubReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryDetailReportController objMEC = new MarksEntryDetailReportController();

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
                //Load Page Help
                PopulateDropDownList();

                //comment 290622
                //if (Session["usertype"].ToString() != "1")
                //{
                //    divDegree.Visible = true;
                //    divBranch.Visible = true;
                //    divDept.Visible = true;
                //    rfvDept.Enabled = true;
                //    rfvDegree.Enabled = true;
                //    btnDeclartionPending.Visible = false;
                //}
                //else 
                //{
                //    divDegree.Visible = false;
                //    divBranch.Visible = false;
                //    divDept.Visible = false;
                //    rfvDept.Enabled = false;
                //    rfvDegree.Enabled = false;
                //}
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
                Response.Redirect("~/notauthorized.aspx?page=ResultPubReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultPubReport.aspx");
        }
    }

    #region Drop Down Bind Process with Page Selected Index Changed Event.

    private void PopulateDropDownList()
    {
        try
        {
            //added on 290622
            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");


         //added comment 290622   objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S WITH (NOLOCK) INNER JOIN RESULT_PUBLISH_DATA TR WITH (NOLOCK) ON (S.SESSIONNO=TR.SESSIONNO)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "TR.SESSIONNO > 0", "S.SESSIONNO DESC");


            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
           
            ddlSession.Focus();
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_ResultPubReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
        //comment on 290622
        //ddlScheme.Items.Clear();
        //ddlScheme.Items.Add(new ListItem("Please Select", "0"));

        //ddlSem.Items.Clear();
        //ddlSem.Items.Add(new ListItem("Please Select", "0"));

        //if (ddlSession.SelectedIndex > 0)
        //{
        //    if (Session["usertype"].ToString() != "1")
        //    {
        //        objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");

        //    }
        //    else
        //    {
        //        objCommon.FillDropDownList(ddlColg, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON(TR.IDNO=S.IDNO) INNER JOIN ACD_COLLEGE_MASTER C WITH (NOLOCK) ON (S.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN C.LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(C.LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND TR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "C.COLLEGE_ID");
        //    }
        //        ddlColg.Focus();
        //}
        //else
        //{
        //    ddlColg.Items.Clear();
        //    ddlColg.Items.Add(new ListItem("Please Select", "0"));
        //}
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

        if (ddlColg.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
            {
                //divDept.Visible = true;
                objCommon.FillDropDownList(ddlDept, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON(U.UA_DEPTNO=D.DEPTNO)", "DISTINCT D.DEPTNO", "D.DEPTNAME", "UA_TYPE=8 AND D.DEPTNO =" + Convert.ToInt32(Session["userdeptno"]), "DEPTNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_TRRESULT R WITH (NOLOCK) ON(S.SCHEMENO=R.SCHEMENO) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (R.IDNO=ST.IDNO)", "DISTINCT S.SCHEMENO", "S.SCHEMENAME", "ST.COLLEGE_ID=" + ddlColg.SelectedValue + " AND R.SESSIONNO=" + ddlSession.SelectedValue, "S.SCHEMENO");
            }
            ddlScheme.Focus();
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO > 0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue, "A.SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    #endregion Drop Down Bind Process with Page Selected Index Changed Event.

    #region Report Button Event

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnPrint_OnClick(object sender, EventArgs e)
    {

        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

        int count = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT TR INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON(S.IDNO=TR.IDNO) INNER JOIN RESULT_PUBLISH_DATA PD ON(TR.IDNO = PD.IDNO AND TR.SESSIONNO = PD.SESSIONNO  AND TR.SEMESTERNO = PD.SEMESTERNO AND TR.SCHEMENO = PD.SCHEMENO) ", "count(*)", "TR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND (TR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " OR " + Convert.ToInt32(ViewState["schemeno"]) + "=0) AND (TR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " OR " + Convert.ToInt32(ddlSem.SelectedValue) + "=0)"));
        if (count > 0)
        {

            this.ShowReport(ddlClgname.SelectedItem.Text.Replace(" ", "_"), "ResultPublicationReport.rpt");
        }
        else
        {
            objCommon.DisplayMessage(updResultpublicationreport, "No Data Found for current selection.", this.Page);
        }
    }

    protected void btnExcelReport_OnClick(object sender, EventArgs e)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            GridView GV = new GridView();
            DataSet ds = null;
            ds = objMEC.GetResultPublicationDetailInExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GV.DataSource = ds;
                GV.DataBind();

                string attachment = "attachment; filename=" + ddlClgname.SelectedItem.Text.Replace(" ", "_") + "Result_Publication_Report.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updResultpublicationreport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_ResultPubReport.btnExcelReport_OnClick() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion Report Button Event

    #region User Define Function

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {



            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle + "_Result_Publication_Report";
            url += "&path=~,Reports,Academic," + rptFileName;

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlColg.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_BRANCHNO=" + ViewState["branchno"];

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updResultpublicationreport, this.updResultpublicationreport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_ResultPubReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion User Define Function
    protected void btnDeclartionPending_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            try
            {
                GridView GV = new GridView();
                DataSet ds = null;
                ds = objMEC.GetResultPublicationPendingDetailInExcel(Convert.ToInt32(ddlSession.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].Columns.RemoveAt(3);
                    GV.DataSource = ds;
                    GV.DataBind();

                    string attachment = "attachment; filename=" + ddlSession.SelectedItem.Text.Replace(" ", "_") + "Result_Publication_Pending_Report.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(updResultpublicationreport, "No Data Found for current selection.", this.Page);
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_REPORTS_ResultPubReport.btnExcelReport_OnClick() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else
        {
            objCommon.DisplayMessage(updResultpublicationreport, "Please Select Session", this.Page);
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "COLLEGE_ID=" + ddlColg.SelectedValue + " AND DEPTNO=" + ddlDept.SelectedValue, "D.DEGREENO");
        ddlDegree.Focus();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.BRANCHNO = B.BRANCHNO", "DISTINCT B.BRANCHNO", "B.LONGNAME", "COLLEGE_ID=" + ddlColg.SelectedValue + " AND CDB.DEPTNO=" + ddlDept.SelectedValue + " AND CDB.DEGREENO =" + ddlDegree.SelectedValue, "B.BRANCHNO");
            }
           
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=S.DEGREENO AND CDB.BRANCHNO = S.BRANCHNO AND CDB.DEPTNO = S.DEPTNO", "DISTINCT S.SCHEMENO", "S.SCHEMENAME", "COLLEGE_ID=" + ddlColg.SelectedValue + " AND S.DEPTNO=" + ddlDept.SelectedValue + " AND CDB.DEGREENO =" + ddlDegree.SelectedValue, "S.SCHEMENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + ddlDegree.SelectedValue + " AND DEPTNO=" + ddlDept.SelectedValue, "SCHEMENO");
            }
        }
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

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S WITH (NOLOCK) INNER JOIN RESULT_PUBLISH_DATA TR WITH (NOLOCK) ON (S.SESSIONNO=TR.SESSIONNO)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "TR.SESSIONNO > 0 and isnull(is_active,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONNO DESC");

            }
        }
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
       

    }




    
}