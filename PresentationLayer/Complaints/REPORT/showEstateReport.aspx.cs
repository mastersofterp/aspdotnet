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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS.UAIMS;


public partial class Estate_showEstateReport : System.Web.UI.Page
{
    Common objCommon = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                ShowItemStockReport();
                
                //ShowReport("Show Estate Report", "rptitemmaster_stock.rpt");
            }
            //divMsg.InnerHtml = string.Empty;
        //}
    }

    // For Crystal Report 


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

    
    
    
    private void ShowItemStockReport()
    {
        ////Set Report
        ReportDocument customerReport = new ReportDocument();
        string reportPath = Server.MapPath("~\\Reports\\Complaints\\" + "rptitemmaster_stock.rpt");
        customerReport.Load(reportPath);

        ConfigureCrystalReports(customerReport);
        Session["reportdata"] = customerReport;
        crViewer.ReportSource = customerReport;

        //Parameter to Report Document
        customerReport.SetParameterValue("@CollegeName", Session["coll_name"].ToString());
        customerReport.SetParameterValue("@P_USERNAME", Session["userfullname"]);
    }

    private void ConfigureCrystalReports(ReportDocument customerReport)
    {
        ////SET Login Details & DB DETAILS
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customerReport);
    }
}
