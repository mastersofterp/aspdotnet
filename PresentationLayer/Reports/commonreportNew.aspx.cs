using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.UI;

using System.Data.SqlClient;
//using IITMS.Cloud_1;
//using IITMS.Cloud_1.BL_ACD;
//using IITMS.CSMS.BAL_AUTHO;

using System.Web.Services;

using System.IO;
using System;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Globalization;

using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using System.Text;
using System.Configuration;

public partial class Reports_commonreportNew : System.Web.UI.Page
{
    ReportDocument customReport;

   

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //added by tanu 29/10/2022
        //Request.ServerVariables["HTTP_REFERER"]:- Returns a string containing the URL of the page that referred the request to the current page .
        //                                          If the page is redirected, HTTP_REFERER is empty

        string referer = Request.ServerVariables["HTTP_REFERER"];
        if (string.IsNullOrEmpty(referer))
        {
            Response.Redirect("~/notauthorized.aspx?");
        }

        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx?");
            return;
        }

        if (!Page.IsPostBack)
        {
            Session["reportdata"] = null;

            if ((Request.QueryString.Count != 0))
            {


                //General Reports
                if (Request.QueryString["path"] != null & Request.QueryString["page"] == null && Request.QueryString["pagetitle"] != null)
                {
                    if (Request.QueryString["format"] != null)
                    {
                        ShowGeneralReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString(), Request.QueryString["format"].ToString());
                    }
                    else
                    {
                        ShowGeneralReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString());
                    }

                    Page.Title = Request.QueryString["pagetitle"].ToString();
                    return;
                }



                ////Employee Abstract Salary Report ***Added on 7 dec2010***

                //if (Request.QueryString["pathForEmployeeAbstractSalary"] != null && Request.QueryString["paramForEmployeeAbstractSalary"] != null)
                //{
                //    ShowReportEmployeeAbstractSalary(Request.QueryString["pathForEmployeeAbstractSalary"].ToString(), Request.QueryString["paramForEmployeeAbstractSalary"].ToString());
                //    Page.Title = Request.QueryString["pagetitleForEmployeeAbstractSalary"].ToString();
                //    return;
                //}
                ////Employee Cummulative Abstract Report

                //if (Request.QueryString["pathForEmployeeCummulativeAbstractSalary"] != null && Request.QueryString["paramForEmployeeCummulativeAbstractSalary"] != null)
                //{
                //    ShowReportForEmployeePaySlip(Request.QueryString["pathForEmployeeCummulativeAbstractSalary"].ToString(), Request.QueryString["paramForEmployeeCummulativeAbstractSalary"].ToString());
                //    Page.Title = Request.QueryString["pagetitleForEmployeeCummulativeAbstractSalary"].ToString();
                //    return;
                //}
            }
        }
        else
        {
            if (Session["reportdata"] != null)
            {
                crViewer.ReportSource = Session["reportdata"] as ReportDocument;
                crViewer.DataBind();

            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["USERID"] != null && Session["COLL_ID"] != null && Session["SOC_ID"] != null))
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
        }

    }

    protected void crViewer_Unload(object sender, EventArgs e)
    {
        crViewer.Dispose();
        //customReport.Close();
        if (Session["format"] != null)
        {
            if (Convert.ToString(Session["format"]).Trim() != "cry")
            {
                customReport.Dispose();
                GC.Collect();
                Session["format"] = null;
            }

        }
        else
        {
            customReport.Dispose();
            GC.Collect();
        }
        //customReport.Dispose();
        //  GC.Collect();
    }

    protected void crViewer_Init(object sender, EventArgs e)
    {
        var _with1 = crViewer;
        _with1.HasDrillUpButton = true;
        _with1.HasExportButton = true;
        _with1.HasGotoPageButton = true;
        _with1.HasPageNavigationButtons = true;
        _with1.HasPrintButton = true;
        _with1.HasRefreshButton = true;
        _with1.HasSearchButton = true;
        _with1.HasToggleGroupTreeButton = true;
        //_with1.HasViewList = true;
        _with1.HasZoomFactorList = true;
    }



    private void ShowGeneralReport(string path, string paramString)
    {

        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));

        customReport.Load(reportPath);
        customReport.Refresh();


        ///////// if report data source exist in the session then assign it to report
        ///////// and set session variable to null
        //////if (Session["rptDataSource"] != null && ((DataSet)Session["rptDataSource"]) != null)
        //////{
        //////    customReport.SetDataSource((DataSet)Session["rptDataSource"]);
        //////    Session["rptDataSource"] = null;
        //////}

        /// Assign parameters to report document        
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                /// Each array in val contains string in following format.
                /// ParamName=Value*ReportName
                /// Here report name is the name of report from which this parameter belongs.
                /// if parameter belongs to main report then report name is equal to MainRpt
                /// else if parameter belongs to subreport then report name is equal to name of subreport.
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');

                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";

                paramName = val[i].Substring(0, indexOfEql);

                /// if report name is not passed with the parameter(means indexOfSlash will be -1) then 
                /// handle the scenario to work properly.
                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {

                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        /// set login details & db details for report document
        this.ConfigureCrystalReports(customReport);

        /// set login details & db details for each subreport 
        /// inside main report document.
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }
        //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Test");
        //crViewer.RefreshReport();
        //Show Report directly in Acrobat Reader, but the browser window will not be closed
        MemoryStream oStream = null;
        Session["reportdata"] = customReport;
        if (Session["Report"].ToString() == "")
        {
            crViewer.ReportSource = customReport;
            crViewer.DataBind();
        }
        else if (Session["Report"].ToString() == "pdf")
        {
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
        }
        else if (Session["Report"].ToString() == "Excel")
        {
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-Excel";
            Session["Report"] = "";
        }
        else if (Session["Report"].ToString() == "Word")
        {
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.RichText);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-Word";
            Session["Report"] = "";
        }
        //Response.BinaryWrite(oStream.ToArray());
        //Response.End();

        //Session["reportdata"] = (MemoryStream)oStream;

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("commonreport")));
        //Response.Redirect(url + "report.aspx");

    }



    private void ShowGeneralReport(string path, string paramString, string format)
    {
        try
        {


            /// Set Report
            /// 

            customReport = new ReportDocument();
            string reportPath = Server.MapPath(path.Replace(",", "\\"));

            customReport.Load(reportPath);
            customReport.Refresh();

            ///////// if report data source exist in the session then assign it to report
            ///////// and set session variable to null
            //////if (Session["rptDataSource"] != null && ((DataSet)Session["rptDataSource"]) != null)
            //////{
            //////    customReport.SetDataSource((DataSet)Session["rptDataSource"]);
            //////    Session["rptDataSource"] = null;
            //////}


            /// set login details & db details for report document
            this.ConfigureCrystalReports(customReport);

            /// set login details & db details for each subreport 
            /// inside main report document.
            for (int i = 0; i < customReport.Subreports.Count; i++)
            {
                ConfigureCrystalReports(customReport.Subreports[i]);
            }


            /// Assign parameters to report document        
            char ch = ',';
            string[] val = paramString.Split(ch);
            if (customReport.ParameterFields.Count > 0)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    /// Each array in val contains string in following format.
                    /// ParamName=Value*ReportName
                    /// Here report name is the name of report from which this parameter belongs.
                    /// if parameter belongs to main report then report name is equal to MainRpt
                    /// else if parameter belongs to subreport then report name is equal to name of subreport.
                    int indexOfEql = val[i].IndexOf('=');
                    int indexOfStar = val[i].IndexOf('*');

                    string paramName = string.Empty;
                    string value = string.Empty;
                    string reportName = "MainRpt";

                    paramName = val[i].Substring(0, indexOfEql);

                    /// if report name is not passed with the parameter(means indexOfSlash will be -1) then 
                    /// handle the scenario to work properly.
                    if (indexOfStar > 0)
                    {
                        value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                        reportName = val[i].Substring(indexOfStar + 1);
                    }
                    else
                    {

                        value = val[i].Substring(indexOfEql + 1);
                    }

                    if (reportName == "MainRpt")
                    {
                        if (value == "null")
                        {
                            customReport.SetParameterValue(paramName, null);
                        }
                        else
                            customReport.SetParameterValue(paramName, value);
                    }
                    else
                        customReport.SetParameterValue(paramName, value, reportName);
                }
            }

            if (Session["DIRECT_PRINTING"] == null || Session["DIRECT_PRINTING"] == "")
            {
                Session["DIRECT_PRINTING"] = false;
            }
            //if (Convert.ToBoolean(Session["DIRECT_PRINTING"]) == true)
            //{
            //    crViewer.ReportSource = customReport;
            //    Session["reportdata"] = customReport;


            //    crViewer.ToolPanelView = ToolPanelViewType.None;
            //    crViewer.HasToggleGroupTreeButton = false;
            //    crViewer.HasToggleParameterPanelButton = false;
            //    crViewer.DisplayToolbar = false;
            //    crViewer.DisplayStatusbar = false;
            //    crViewer.HasDrilldownTabs = false;
            //    crViewer.HasDrillUpButton = false;

            //   Page.RegisterStartupScript("anykey", "<script>window.print();</script>");



            //}
            //else
            //{
            //crViewer.ReportSource = customReport;
            //Session["reportdata"] = customReport;
            ////crViewer.ReportSource = customReport;
            ////Session["reportdata"] = customReport;
            //crViewer.DisplayToolbar = true;
            //crViewer.SeparatePages = true;
            MemoryStream oStream = null;
            if (format == "cry")
            {
                crViewer.ReportSource = customReport;
                crViewer.DataBind();
                Session["reportdata"] = customReport;
                Session["format"] = "cry";
            }
            else if (format == "No")
            {

                //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();

                //Session["reportdata"] = (MemoryStream)oStream;




                var outputStream = new MemoryStream();
                var pdfReader = new PdfReader(customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                var pdfStamper = new PdfStamper(pdfReader, outputStream);
                //Add the auto-print javascript
                if (Convert.ToBoolean(Session["DIRECT_PRINTING"]) == true)
                {
                    var writer = pdfStamper.Writer;
                    writer.AddJavaScript(GetAutoPrintJs());
                }
                pdfStamper.Close();
                var content = outputStream.ToArray();
                outputStream.Close();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(content);
                //Response.AddHeader("Refresh", "0; url=" + Request.RawUrl + ""); // this is used to refresh the browser
                Response.End();
                // HttpContext.Current.ApplicationInstance.CompleteRequest();
                outputStream.Close();
                outputStream.Dispose();



                // Session["reportdata"] = (MemoryStream)oStream;


            }
            else if (format == "pdf")
            {
                //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();

                //Session["reportdata"] = (MemoryStream)oStream;


                var outputStream = new MemoryStream();
                var pdfReader = new PdfReader(customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                var pdfStamper = new PdfStamper(pdfReader, outputStream);
                //Add the auto-print javascript
                if (Convert.ToBoolean(Session["DIRECT_PRINTING"]) == true)
                {
                    var writer = pdfStamper.Writer;
                    writer.AddJavaScript(GetAutoPrintJs());
                }
                pdfStamper.Close();
                var content = outputStream.ToArray();
                outputStream.Close();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(content);
                //Response.AddHeader("Refresh", "0; url=" + Request.RawUrl + ""); // this is used to refresh the browser
                Response.End();
                outputStream.Close();
                outputStream.Dispose();


            }
            else if (format == "Excel")
            {
                oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/ms-Excel";
                Session["Report"] = "";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();

                Session["reportdata"] = (MemoryStream)oStream;
            }
            else if (format == "Word")
            {
                oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.RichText);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/ms-Word";
                Session["Report"] = "";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();

                Session["reportdata"] = (MemoryStream)oStream;
            }



        }
        catch (Exception ex)
        {

            // throw;
        }

    }

    protected string GetAutoPrintJs()
    {
        var script = new StringBuilder();
        script.Append("var pp = getPrintParams();");
        script.Append("pp.interactive= pp.constants.interactionLevel.full;");
        script.Append("print(pp);");
        return script.ToString();
    }

    protected void StreamPdf(Stream pdfSource)
    {
        var outputStream = new MemoryStream();
        var pdfReader = new PdfReader(pdfSource);
        var pdfStamper = new PdfStamper(pdfReader, outputStream);
        //Add the auto-print javascript
        var writer = pdfStamper.Writer;
        writer.AddJavaScript(GetAutoPrintJs());
        pdfStamper.Close();
        var content = outputStream.ToArray();
        outputStream.Close();
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(content);
        Response.AddHeader("Refresh", "0; url=" + Request.RawUrl + ""); // this is used to refresh the browser
        Response.End();
        outputStream.Close();
        outputStream.Dispose();
    }


    private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        //ConnectionInfo connectionInfo = CSMS_COMMON.GetCrystalConnection(Session["DataBase"].ToString().Trim());
        ConnectionInfo connectionInfo = GetCrystalConnectionFromClass();
        SetDBLogonForReport(connectionInfo, customReport);
        // customReport.VerifyDatabase();
    }

    public static void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
    {
        Tables tables = reportDocument.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
        {
            TableLogOnInfo tableLogonInfo = table.LogOnInfo;

            tableLogonInfo.ConnectionInfo = connectionInfo;
            table.ApplyLogOnInfo(tableLogonInfo);

        }
        //   reportDocument.VerifyDatabase();
    }



    public static ConnectionInfo GetCrystalConnectionFromClass()
    {


        //DataInfo objdata = new DataInfo();
        ConnectionInfo connectionInfo = new ConnectionInfo();
        //string strcon = connectionInfo.ServerName;
        //string[] CON;
        //if (HttpContext.Current.Session["SOC_CODE"].ToString() == "")
        //{
        //    HttpContext.Current.Session["SOC_CODE"] = "NA";
        //    CON = objdata.SERLOGIN(HttpContext.Current.Session["SOC_CODE"].ToString()).Split(';');
        //}
        //else
        //{
        //    CON = objdata.SERLOGIN(HttpContext.Current.Session["SOC_CODE"].ToString()).Split(';');
        //}
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        builder.ConnectionString = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString; ;

        string server = builder["SERVER"] as string;
        string database = builder["DataBase"] as string;
        string UserID = builder["User ID"] as string;
        string password = builder["Password"] as string;



        //Following for Remote Server
        connectionInfo.UserID = UserID; //System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString();//"sa";
        connectionInfo.Password = password;//CON[0].Split('=').ToString(); ;// System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();//"M@ster$oftware";
        connectionInfo.ServerName = server;//CON[2].Split('=').ToString(); ;// "" + HttpContext.Current.Session["Server"].ToString().Trim() + "";// System.Configuration.ConfigurationManager.AppSettings["Server"].ToString();
        connectionInfo.DatabaseName = database;// CON[3].Split('=').ToString(); ;// CollegeCode;// System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();


        return connectionInfo;
    }


    protected void crViewer_Unload1(object sender, EventArgs e)
    {

    }
}