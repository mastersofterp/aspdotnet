
//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : STORES_Reports_Str_College_or_Dept_Wise_Item_Report
// CREATION DATE : 04.08.2021
// CREATED BY    : TANU BALGOTE
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY STORES_Reports_Str_College_or_Dept_Wise_Item_Report
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================


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
using IITMS.UAIMS;
using IITMS;

public partial class STORES_Reports_Str_ColDeptWise_Item_Report : System.Web.UI.Page
{

    Common ObjComman = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    this.filldropdown();
                    ObjComman.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "");
                }


            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void filldropdown()
    {

        ObjComman.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "");
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Payroll_LIC_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Payroll_LIC_Report.aspx");
        }
    }

    protected void rblItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        if (rblItemType.SelectedValue == "2")
        {
            divDepartment.Visible = true;
            divCollege.Visible = true;
        }
        else
        {
            divDepartment.Visible = false;
            divCollege.Visible = false;
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSummaryRpt_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblItemType.SelectedValue == "2")
            {
                if (ddlCollege.SelectedIndex == 0)
                {
                    MessageBox("Please Select College.");
                    return;
                }
                ShowSummaryReport("Report", "StrColDeptSummaryReport.rpt");
            }
            else
                ShowSummaryReport("Report", "MainStoreSummaryReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDetailRpt_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblItemType.SelectedValue == "2")
            {
                if (ddlCollege.SelectedIndex ==0)
                {
                    MessageBox("Please Select College");
                    return;
                }
            }
            ShowDetailReport("Report", "StrColDeptDetailReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowSummaryReport(string reportTitle, string rptFileName)
    {
        string fromDate = string.Empty;
        string toDate = string.Empty;
        if (txtFromDate.Text != "")
            fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");

        if (txtToDate.Text != "")
            toDate = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");

       

        string CommandType = string.Empty;
        if (rblItemType.SelectedValue == "1")
        {
            CommandType = "MainStore";
        }
        if (rblItemType.SelectedValue == "2")
        {
            //if (ddlCollege.SelectedIndex == 0)
            //{
            //    MessageBox("Please Select College");
            //    return;
            //}
            CommandType = "College";
        }

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@COMMANDTYPE=" + CommandType.ToString() + "," + "@COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "," + "@FROM_DATE=" + fromDate + "," + "@TO_DATE=" + toDate;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailReport(string reportTitle, string rptFileName)
    {
        string fromDate = string.Empty;
        string toDate = string.Empty;
        if (txtFromDate.Text != "")
        {
            fromDate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
            fromDate = fromDate.Substring(0, 10);
        }
        else
        {
            fromDate = string.Empty;
        }

        if (txtToDate.Text != "")
        {
            toDate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text)));
            toDate = toDate.Substring(0, 10);
        }
        else
        {
            toDate = string.Empty;
        }
        string CommandType = string.Empty;
        if (rblItemType.SelectedValue == "1")
        {
            CommandType = "MainStore";
        }
        if (rblItemType.SelectedValue == "2")
        {
            CommandType = "College";
        }
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@COMMANDTYPE=" + CommandType.ToString() + "," + "@COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "," + "@FROM_DATE=" + fromDate + "," + "@TO_DATE=" + toDate;

            //url += "&param=@COMMANDTYPE=" + CommandType.ToString() + "," + "@COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "," + "@FROM_DATE=" + Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy") + "," + "@TO_DATE=" + Convert.ToDateTime(toDate).ToString("dd/MM/yyyy");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        rblItemType.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        divDepartment.Visible = false;
        divCollege.Visible = false;
    }
  
}