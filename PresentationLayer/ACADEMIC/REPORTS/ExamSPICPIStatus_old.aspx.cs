using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_REPORTS_MarksEntryNotDone : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                ddlSession.SelectedIndex = 1;
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
               
            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
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
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + "AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENAME");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {      
        
    }
    

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "SEMESTERNO>0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "SEMESTERNO");        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GridView GVStatus = new GridView();
        string degree = ddlDegree.SelectedItem.Text;
        string scheme = ddlScheme.SelectedItem.Text;
        string ContentType = string.Empty;
        DataSet ds = objMarksEntry.Get_SPI_CPI_Status(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER I (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER II (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER III (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER IV (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER V (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER VI (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER VII (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER VIII (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER IX (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add("SEMESTER X (Checker Sign)");
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();
            //ds.Tables[0].Rows.Add();

            GVStatus.DataSource = ds;
            GVStatus.DataBind();

            string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + scheme.Replace(" ", "_") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStatus.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            GVStatus.DataSource = ds;
            GVStatus.DataBind();
            objCommon.DisplayMessage("No record found...", this.Page);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ConsolidatedReport", "rptConsolidateSPICPIreport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //sb.Append(@"window.open('" + url + "','','" + features + "');");
        //ScriptManager.RegisterClientScriptBlock(this.consolidated, this.consolidated.GetType(), "controlJSScript", sb.ToString(), true);
    }
}
