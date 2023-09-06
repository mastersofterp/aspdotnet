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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;

using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;

public partial class AcdPBI_Dashboard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    //BusinessLogicLayer.BusinessLogic.PO

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
        if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

                OnRequestDownload();
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
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
        }
    }

    //Added by Abhinay Lad [05-10-2019]
    #region API CALL
    //private async void OnRequestDownload()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Id");
    //    dt.Columns.Add("DisplayName");
    //    var viewModel = await PbiEmbeddedManager.GetDashboards("");
    //    string FirstDashID = "";

    //    for (int i = 0; i < viewModel.Dashboards.Count; i++)
    //    {
    //        if (i == 0)
    //        {
    //            FirstDashID = Convert.ToString(viewModel.Dashboards[i].Id);
    //        }
    //        dt.Rows.Add(viewModel.Dashboards[i].Id, viewModel.Dashboards[i].DisplayName);
    //        //PostBackTrigger trigger = new PostBackTrigger();
    //        //trigger.ControlID = "Btn";
    //        ////trigger.EventName = "Click";
    //        //UpdatePanel1.Triggers.Add(trigger);
    //    }

    //    rpt_DashName.DataSource = dt;
    //    rpt_DashName.DataBind();

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Query", "$('.breadcrumb-menu').hide();", true);
    //    GetDash(FirstDashID);
    //}



    private async void OnRequestDownload()
    {
        #region Commented but VIMP
        //DataTable dt = new DataTable();
        //dt.Columns.Add("Id");
        //dt.Columns.Add("DisplayName");
        //var viewModel = await PbiEmbeddedManager.GetReports("");
        //string FirstDashID = "";

        //for (int i = 0; i < viewModel.Reports.Count; i++)
        //{
        //    if (i == 0)
        //    {
        //        FirstDashID = Convert.ToString(viewModel.Reports[i].Id);
        //    }
        //    dt.Rows.Add(viewModel.Reports[i].Id, viewModel.Reports[i].Name);
        //    //PostBackTrigger trigger = new PostBackTrigger();
        //    //trigger.ControlID = "Btn";
        //    ////trigger.EventName = "Click";
        //    //UpdatePanel1.Triggers.Add(trigger);
        //}

        //rpt_DashName.DataSource = dt;
        //rpt_DashName.DataBind();

        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Query", "$('.breadcrumb-menu').hide();", true);
        ////GetDash(FirstDashID);
        ///
        #endregion

        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns.Add("DisplayName");
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        var viewModel = await PbiEmbeddedManager1.GetDashboards("");
        string FirstDashID = "";
        for (int i = 0; i < viewModel.Dashboards.Count; i++)
        {
            if (i == 0)
                FirstDashID = Convert.ToString(viewModel.Dashboards[i].Id);
                dt.Rows.Add(viewModel.Dashboards[i].Id, viewModel.Dashboards[i].DisplayName);
        }
        rpt_DashName.DataSource = dt;
        rpt_DashName.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Query", "$('.breadcrumb-menu').hide();", true);
        GetDash(FirstDashID);
    }



    private async void GetDash(string DID)
    {
        var CurrentDashboard = await PbiEmbeddedManager1.GetCurrentDashboard(DID);
        hfd_accessToken.Value = CurrentDashboard.EmbedConfig.EmbedToken.Token;
        hfd_embedUrl.Value = CurrentDashboard.EmbedConfig.EmbedUrl;
        hfd_embedReportId.Value = CurrentDashboard.EmbedConfig.Id;

        string scrx = @"
                            var accessToken = '" + hfd_accessToken.Value + "'";
        scrx += @"
                            var embedUrl =  '" + hfd_embedUrl.Value + "'";
        scrx += @"
                            var embedReportId = '" + hfd_embedReportId.Value + "'";
        scrx += @"
                            var models = window['powerbi-client'].models;

                            var config = {
                                type: 'dashboard',
                                tokenType: models.TokenType.Embed,
                                accessToken: accessToken,
                                embedUrl: embedUrl,
                                id: embedReportId,
                                permissions: models.Permissions.All,
                                viewMode: models.ViewMode.Edit,
                                oDataFilter:" + '"' + "StudData/SocietyId eq '1258'" + '"';
        scrx += @",
                                settings:{
                                    filterPaneEnabled: false,
                                    navContentPaneEnabled: true
                                }
                            };
        var reportContainer = document.getElementById('dashboardContainer');
        var report = powerbi.embed(reportContainer, config);";

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", scrx, true);
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Key", scrx, true);
    }
    #endregion
    //END

    protected void lbtn_Dash_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        string DID = lbtn.CommandArgument;
        foreach (RepeaterItem item in rpt_DashName.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                var linkbtn = (LinkButton)item.FindControl("lbtn_Dash");
                linkbtn.CssClass = "label label-default";
            }
        }
        lbtn.CssClass = "label label-success";
        GetDash(DID);
    }
    protected void rpt_DashName_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int i = 0;
        foreach (RepeaterItem item in rpt_DashName.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                if (i == 0)
                {
                    var linkbtn = (LinkButton)item.FindControl("lbtn_Dash");
                    linkbtn.CssClass = "label label-success";
                }
                i++;
            }
        }
    }
}


//CALL STARTS HERE
public class JsonWebToken1
{
    public string token_type { get; set; }
    public string resource { get; set; }
    public string scope { get; set; }
    public string expires_in { get; set; }
    public string expires_on { get; set; }
    public string not_before { get; set; }
    public string id_token { get; set; }
    public string access_token { get; set; }
    public string refresh_token { get; set; }

    public static JsonWebToken1 Deserialize(string json)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonWebToken1>(json);
    }

}

public class EmbedConfiguration1
{
    public string Id { get; set; }
    public string EmbedUrl { get; set; }
    public EmbedToken EmbedToken { get; set; }
    public int MinutesToExpiration
    {
        get
        {
            var minutesToExpiration = EmbedToken.Expiration.Value - DateTime.UtcNow;
            return minutesToExpiration.Minutes;
        }
    }
    public string ErrorMessage { get; internal set; }
}

public class PbiEmbeddedManager1
{
    static string aadAuthorizationEndpoint = "https://login.windows.net/common/oauth2/authorize";

    static string aadTokenEndpoint = "https://login.windows.net/common/oauth2/token";
    static string resourceUri = "https://analysis.windows.net/powerbi/api";
    static string urlPowerBiRestApiRoot = "https://api.powerbi.com/";

    static string clientId = ConfigurationManager.AppSettings["clientId"];
    static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
    static string redirectUri = ConfigurationManager.AppSettings["redirectUri"];

    static string pbiUserName = ConfigurationManager.AppSettings["pbiUserName"];
    static string pbiUserPassword = ConfigurationManager.AppSettings["pbiUserPassword"];

    static string appWorkspaceId = ConfigurationManager.AppSettings["appAcdWorkspaceId"];

    static string GetAccessTokenForNativeClient()
    {
        AuthenticationContext authContext = new AuthenticationContext(aadAuthorizationEndpoint);
        var userCredentials = new UserPasswordCredential(pbiUserName, pbiUserPassword);
        string token = authContext.AcquireTokenAsync(resourceUri, clientId, userCredentials).Result.AccessToken;
        return token;
    }
    static string GetAccessToken()
    {

        var client = new HttpClient();
        string urlTokenEndpoint = aadTokenEndpoint;
        client.BaseAddress = new Uri(urlTokenEndpoint);

        var content = new FormUrlEncodedContent(new[] {
        new KeyValuePair<string, string>("resource", resourceUri),
        new KeyValuePair<string, string>("client_id", clientId),
        new KeyValuePair<string, string>("client_secret", clientSecret),
        new KeyValuePair<string, string>("grant_type", "password"),
        new KeyValuePair<string, string>("username", pbiUserName),
        new KeyValuePair<string, string>("password", pbiUserPassword)
      });

        var responseText = client.PostAsync(urlTokenEndpoint, content).Result.Content.ReadAsStringAsync().Result;
        JsonWebToken1 jwt = JsonWebToken1.Deserialize(responseText);
        return jwt.access_token;
    }

    static PowerBIClient GetPowerBiClient()
    {
        var tokenCredentials = new TokenCredentials(GetAccessToken(), "Bearer");
        return new PowerBIClient(new Uri(urlPowerBiRestApiRoot), tokenCredentials);
    }

    //START
    public class HomeViewModel
    {
        public string WorkspaceName { get; set; }
        public string WorkspaceId { get; set; }
    }
    //END
    public static async Task<HomeViewModel> GetHomeView()
    {
        var client = GetPowerBiClient();
        var workspaces = (await client.Groups.GetGroupsAsync()).Value;
        var workspace = workspaces.Where(ws => ws.Id == appWorkspaceId).FirstOrDefault();
        var viewModel = new HomeViewModel
        {
            WorkspaceName = workspace.Name,
            WorkspaceId = workspace.Id
        };
        return viewModel;
    }

    //START
    public class ReportViewModel
    {
        public Report Report { get; set; }
        public EmbedConfiguration1 EmbedConfig { get; set; }
    }
    public class ReportsViewModel
    {
        public List<Report> Reports { get; set; }
        public ReportViewModel CurrentReport { get; set; }
    }

    public class DashboardsViewModel
    {
        public List<Dashboard> Dashboards { get; set; }
        public List<Report> Reports { get; set; }
    }
    public class DashboardViewModel
    {
        public Dashboard Dashboard { get; set; }
        public EmbedConfiguration1 EmbedConfig { get; set; }
    }
    //END
    //Reports//
    public static async Task<ReportsViewModel> GetReports(string reportId)
    {
        var client = GetPowerBiClient();
        var reports = (await client.Reports.GetReportsInGroupAsync(appWorkspaceId)).Value;

        var viewModel = new ReportsViewModel
        {
            Reports = reports.ToList()
        };
        
        return viewModel;
    }

    //public static async Task<DashboardViewModel> GetCurrentReport(string reportId)
    //{
    //    var client = GetPowerBiClient();
    //    var reports = (await client.Reports.GetReportsInGroupAsync(appWorkspaceId)).Value;
        
    //    Report report = reports.Where(r => r.Id == reportId).First();
    //    var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
    //    var token = client.Reports.GenerateTokenInGroupAsync(appWorkspaceId, report.Id, generateTokenRequestParameters).Result;

    //    var embedConfig = new EmbedConfiguration()
    //    {
    //        EmbedToken = token,
    //        EmbedUrl = report.EmbedUrl,
    //        Id = report.Id
    //    };


    //    var CurrentReport = new ReportViewModel
    //    {
    //        Report = report,
    //        EmbedConfig = embedConfig
    //    };

    //    CurrentReport;
    //}
    //END//

    public static async Task<DashboardsViewModel> GetDashboards(string dashboardId)
    {
        var client = GetPowerBiClient();
        var dashboards = (await client.Dashboards.GetDashboardsInGroupAsync(appWorkspaceId)).Value;

        var viewModel = new DashboardsViewModel
        {
            Dashboards = dashboards.ToList()
        };

        return viewModel;
    }

    public static async Task<DashboardViewModel> GetCurrentDashboard(string dashboardId)
    {
        var client = GetPowerBiClient();
        var dashboards = (await client.Dashboards.GetDashboardsInGroupAsync(appWorkspaceId)).Value;

        Dashboard dashboard = dashboards.Where(d => d.Id == dashboardId).First();
        var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
        var token = client.Dashboards.GenerateTokenInGroupAsync(appWorkspaceId, dashboard.Id, generateTokenRequestParameters).Result;

        var embedConfig = new EmbedConfiguration1()
        {
            EmbedToken = token,
            EmbedUrl = dashboard.EmbedUrl,
            Id = dashboard.Id
        };

        var CurrentDashboard = new DashboardViewModel
        {
            Dashboard = dashboard,
            EmbedConfig = embedConfig
        };
        return CurrentDashboard;
    }


}