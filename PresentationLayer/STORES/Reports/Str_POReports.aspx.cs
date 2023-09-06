
//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : PO_REPORT
// CREATION DATE : 01.08.2021
// CREATED BY    : TANU BALGOTE
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY PO  REPORT 
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

public partial class STORES_Reports_Str_POReports : System.Web.UI.Page
{
    Common ObjComman = new Common();


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

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReport.SelectedIndex == 0)
            {
                MessageBox("Please Select the PO Type.");
                return;
            }
            if (txtFromDate.Text.ToString() != string.Empty && txtFromDate.Text.ToString() != "__/__/____" && txtToDate.Text.ToString() != string.Empty && txtToDate.Text.ToString() != "__/__/____")
            {
                DateTime fromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
                DateTime toDate = Convert.ToDateTime(txtToDate.Text.ToString());
                if (toDate < fromDate)
                {
                    MessageBox("To Date Should Be Greater Than Or Equals To From Date");
                    return;
                }
            }

            ShowReport("Report", "StrPOReportsADND.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string CommandType = string.Empty;
        string SubCommandType = string.Empty;
        string fromDate = string.Empty;
        string toDate = string.Empty;

        if (txtFromDate.Text != "")
            fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MMM-dd");

        if (txtToDate.Text != "")
            toDate = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MMM-dd");

       

        if (ddlReport.SelectedValue == "1")
        {
            CommandType = "All";

        }
        else if (ddlReport.SelectedValue == "2")
        {
            CommandType = "Delivered";
            if (rblDelivered.SelectedValue == "1")
                SubCommandType = "Delivered All";
            else if (rblDelivered.SelectedValue == "2")
                SubCommandType = "Late Delivered";
        }
        else if (ddlReport.SelectedValue == "3")
        {
            CommandType = "Not Delivered";
            if (rblNotDelivered.SelectedValue == "1")
                SubCommandType = "Not Delivered All";
            else if (rblNotDelivered.SelectedValue == "2")
                SubCommandType = "Delivered Date Crossed";
        }


        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CommandType=" + CommandType.ToString() + "," + "@P_SubCommandType=" + SubCommandType.ToString();
            url += "&param=@P_CommandType=" + CommandType.ToString() + "," + "@P_SubCommandType=" + SubCommandType.ToString() + "," + "@P_FromDate=" + fromDate + "," + "@P_ToDate=" + toDate;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReport.SelectedValue == "2")
        {
            divDelivered.Visible = true;
            divNotDelivered.Visible = false;
        }
        else if (ddlReport.SelectedValue == "3")
        {
            divDelivered.Visible = false;
            divNotDelivered.Visible = true;
        }
        else
        {
            divDelivered.Visible = false;
            divNotDelivered.Visible = false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rblDelivered.SelectedValue = "1";
        rblNotDelivered.SelectedValue = "1";
        ddlReport.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }

}