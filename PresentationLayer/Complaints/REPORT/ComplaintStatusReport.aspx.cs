//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : COMPLAINT STATUS REPORT                                                 
// CREATION DATE : 17-FEB-2018                                                        
// CREATED BY    : MRUNAL SINGH                                                   
// MODIFIED DATE :
// MODIFIED DESC :
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System.Linq;
using System.Xml.Linq;
using System.IO;
using BusinessLogicLayer.BusinessLogic;

public partial class Complaints_REPORT_ComplaintStatusReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Complaint objCT = new Complaint();
    ComplaintController objCC = new ComplaintController();

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
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
                txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                objCommon.FillDropDownList(ddlOfficer, "COMPLAINT_WORKOUT CW INNER JOIN USER_ACC UA ON (CW.EMPID = UA.UA_NO)", "DISTINCT CW.EMPID", "UA.UA_FULLNAME", "", "");

            }
            Session["reportdata"] = null;
        }      
        divMsg.InnerHtml = string.Empty;        
    }

   
    // show report
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlComplaintStatus.SelectedValue) == 1) // Pending 
            {
                ShowReport("Pending Complaints Report", "rptPendingComplaintDetails.rpt");
            }
            else if (Convert.ToInt32(ddlComplaintStatus.SelectedValue) == 2) // In-Process
            {
                ShowReport("Processing Complaints Report", "rptProcessingComplaint.rpt");
            }
            else if (Convert.ToInt32(ddlComplaintStatus.SelectedValue) == 3) // Complete
            {
                ShowReport("Daily Workout Report", "rptCompleteComplaints.rpt");
            }
            else if (Convert.ToInt32(ddlComplaintStatus.SelectedValue) == 4) // Allotted
            {
                ShowReport("Allotted Complaint Report", "rptAllottedComplaint.rpt");
            }
            else if (Convert.ToInt32(ddlComplaintStatus.SelectedValue) == 5) // Declined
            {
                ShowReport("Declined Complaint Report", "rptDeclinedComplaintDetails.rpt");
            }
            else
            {
                ShowWithoutStatusReport("All Complaints Status Report", "rptAllStatusReport.rpt");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_USERID=" + Session["userno"].ToString() + "," + "@P_SDATE=" + txtSDate.Text + "," + "@P_EDATE=" + txtEndDate.Text + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_OFFICER_IN_CHARGE=" + Convert.ToInt32(ddlOfficer.SelectedValue) + ",@P_STATUS=" + Convert.ToInt32(ddlComplaintStatus.SelectedValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    private void ShowWithoutStatusReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_USERID=" + Session["userno"].ToString() + "," + "@P_SDATE=" + txtSDate.Text + "," + "@P_EDATE=" + txtEndDate.Text + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }




    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowConsolidateReport("ConsolidateWorkoutDetails", "rptConsolidateComplaint.rpt");
    }
    // Develop by Sheru 

    protected void btnFeedback_Click(object sender, EventArgs e)
    {
        ShowFeedbackReport("ComplaintFeedbackDetails", "complaint_feedback.rpt");
    }

    //By sheru for feedback show report
    private void ShowFeedbackReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ShowConsolidateReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_USERID=" + Session["userno"].ToString() + "," + "@P_SDATE=" + txtSDate.Text + "," + "@P_EDATE=" + txtEndDate.Text + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
        txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        //Set Report to Null
        Session["reportdata"] = null;
        ddlOfficer.SelectedIndex = 0;
        ddlComplaintStatus.SelectedIndex = 0;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }


    #region Export to Excel
    //export to excel
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindListViewReport();
    }

    private void BindListViewReport()
    {
        try
        {

            //DataSet ds = objCC.GetAllStatusReport();
            //DataTable dt = ds.Tables[0];

            //grdPFMSReport.DataSource = dt;
            //grdPFMSReport.DataBind();

            //this.Export_PFMS("Excel");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void Export_PFMS(string type)
    //{
    //    try
    //    {
    //        string filename = string.Empty;
    //        string ContentType = string.Empty;
    //        filename = "PFMSExcelExport.xls";
    //        ContentType = "ms-excel";
    //        string attachment = "attachment; filename=" + filename;
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", attachment);
    //        Response.ContentType = "application/" + ContentType;
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        grdPFMSReport.RenderControl(htw);
    //        Response.Write(sw.ToString());
    //        Response.End();

    //    }
    //    catch
    //    {

    //    }
    //}
    #endregion
}