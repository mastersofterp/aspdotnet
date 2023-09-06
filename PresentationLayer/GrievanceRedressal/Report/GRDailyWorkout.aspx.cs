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
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class GrievanceRedressal_Report_GRDailyWorkout : System.Web.UI.Page
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Check browser and set pnlContainer width
                if (Request.Browser.Browser.ToLower().Equals("opera"))
                    pnlMain.Width = Unit.Percentage(100);
                else
                    pnlMain.Width = Unit.Percentage(90);

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            //    txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
            //    txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            
        }
       
    }

    protected void Clear()
    {
        txtSDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ddlstatusG.SelectedIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
        //txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        Clear();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=dailyworkout.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=dailyworkout.aspx");
        }
    }

   

    protected void btnConsolidated_Click(object sender, EventArgs e)
    {
     ShowReportConsolidated("Grievances Detail", "ConsolidatedGR.rpt");

    }

    private void ShowReportConsolidated(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("GrievanceRedressal")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,GrievanceRedressal," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

   

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlstatusG.SelectedValue == "1")
            {
                ShowReport("Grievance Report", "rptdaily_workout.rpt", 'C');
            }
            else if (ddlstatusG.SelectedValue == "2")
            {

                ShowReport("Grievance Report", "rptdaily_workout.rpt", 'I');
            }
            else
            {
                ShowReport("Grievance Report", "rptdaily_workout.rpt", 'P');
            }      
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    private void ShowReport(string reportTitle, string rptFileName, char STATUS)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("GrievanceRedressal")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,GrievanceRedressal," + rptFileName;
            url += "&param=@P_STATUS=" + STATUS + ",@P_SDATE=" + txtSDate.Text + ",@P_EDATE=" + txtEndDate.Text + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}