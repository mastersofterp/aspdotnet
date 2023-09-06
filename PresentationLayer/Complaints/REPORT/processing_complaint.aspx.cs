//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : PROCESSING COMPLAINT REPORT                                          
// CREATION DATE : 20-April-2009                                                        
// CREATED BY    : SANJAY RATNAPARKHI                                                   
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


public partial class Estate_processing_complaint : System.Web.UI.Page
{
    Common objCommon = new Common();

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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
                txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            Session["reportdata"] = null;
        }
        if (Session["reportdata"] != null)
        {
            //crViewer.ReportSource = Session["reportdata"] as ReportDocument;
            //crViewer.DataBind();
        }
        //objCommon.ReportPopUp(btnSubmit, "pagetitle=PRM(Pending Complaints Report)&path=~" + "," + "Reports" + "," + "Estate" + "," + "rptPendingComplaint.rpt&param=@P_USERID=" + Session["userno"].ToString() + "," + "@P_SDATE=" + Convert.ToDateTime(txtSDate.Text) + "," + "@P_EDATE=" + Convert.ToDateTime(txtEndDate.Text) + "," + "@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString(), "PRM");
        //objCommon.ReportPopUp(btnSubmit,"pagetitle=PRM(Processing Complaints Report)&path=~" + "," + "Reports" + "," + "Estate" + "," + "rptProcessing_Complaint.rpt&param=@P_USERID=" + Session["userno"].ToString() + "," + "@P_SDATE=" + Convert.ToDateTime(txtSDate.Text) + "," + "@P_EDATE=" + Convert.ToDateTime(txtEndDate.Text) + "," + "@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString(), "PRM");
        divMsg.InnerHtml = string.Empty;
    }

    // For Crystal Report 
    //private void ShowProcessingComplaintReport()
    //{
    //    //Set Report
    //    ReportDocument customerReport = new ReportDocument();
    //    string reportPath = Server.MapPath("~\\Reports\\Estate\\" + "rptProcessing_Complaint.rpt");
    //    customerReport.Load(reportPath);

    //    ConfigureCrystalReports(customerReport);
    //    Session["reportdata"] = customerReport;
    //    crViewer.ReportSource = customerReport;

    //    //Parameter to Report Document
    //    customerReport.SetParameterValue("@CollegeName", Session["coll_name"].ToString());
    //    customerReport.SetParameterValue("@P_USERID", Convert.ToInt32(Session["userno"].ToString()));
    //    customerReport.SetParameterValue("@P_USERNAME", Session["userfullname"].ToString());
    //    customerReport.SetParameterValue("@P_SDATE", Convert.ToDateTime(txtSDate.Text));
    //    customerReport.SetParameterValue("@P_EDATE", Convert.ToDateTime(txtEndDate.Text));
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        ShowReport("Processing Complaints Report", "rptProcessing_Complaint.rpt");
    }

    //private void ConfigureCrystalReports(ReportDocument customerReport)
    //{
    //    ////SET Login Details & DB DETAILS
    //    ConnectionInfo connectionInfo = Common.GetCrystalConnection();
    //    Common.SetDBLogonForReport(connectionInfo, customerReport);
    //}



    private void ShowReport(string reportTitle, string rptFileName)
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
        //Session["reportdata"] = null;
        //crViewer.ReportSource = null;
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

}