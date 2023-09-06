//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ALL MODULES                                                     
// PAGE NAME     : TO CREATE REPORTS                                               
// CREATION DATE : 10-APRIL-2009                                                   
// CREATED BY    : UAIMS TEAM                                                      
// MODIFIED BY   :                                                                 
// MODIFIED DESC :                                                                 
//=================================================================================

using System;
using System.Data;
using System.IO;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;

public partial class LegalMatters_Crystalreport : System.Web.UI.Page
{
    ReportDocument customReport;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Add this line in commonreport.cs page inside protected void Page_PreInit(object sender, EventArgs e){},before the  if (!Page.IsPostBack)

        //START ADD BY ROSHAN PANNASE ON 20-11-2017
        //Request.ServerVariables["HTTP_REFERER"]:- Returns a string containing the URL of the page that referred the request to the current page .
        //                                          If the page is redirected, HTTP_REFERER is empty

        //Added By Deepali G. For Online Payment Receipt as session is getting null after payment, dated on 17/06/2021
        //START//
        string previousPageName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        if (previousPageName == "ccavAdmissionResponse.aspx")
        {
            if (Request.QueryString["path"] != null & Request.QueryString["page"] == null && Request.QueryString["pagetitle"] != null)
            {
                ShowGeneralReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString());
                Page.Title = Request.QueryString["pagetitle"].ToString();
                return;
            }
        }
        else
        {
            if (Session["userno"] == null)
            {
                Response.Redirect("~/notauthorized.aspx?");
                return;
            }
        }
        //END//

        string referer = Request.ServerVariables["HTTP_REFERER"];
        if (string.IsNullOrEmpty(referer))
        {
            Response.Redirect("~/default.aspx?");
        }

        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx?");
            return;
        }
        //END
        if (!Page.IsPostBack)
        {
            Session["reportdata"] = null;

            if ((Request.QueryString.Count != 0))
            {
                // rpt for student registration
                if (Request.QueryString["path"] != null && Request.QueryString["param"] != null && Request.QueryString["rpt"] != null)
                {
                    ShowStudRegistration(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString());
                    return;
                }
                // dept wise courses show
                if (Request.QueryString["rptdpt"] != null && Request.QueryString["path"] != null && Request.QueryString["param"] != null && Request.QueryString["deptno"] != null)
                {
                    ShowDeptWiseCourseReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["deptno"].ToString()));
                    return;
                }

                //All Roll List
                if (Request.QueryString["cat"] != null && Request.QueryString["userno"] != null)
                {
                    ShowRegistrationSlips(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString());
                    return;
                }

                //Display Report for registrationReport.aspx
                //CourseWise Statistics Report: Pre-Registration Module
                if (Request.QueryString["path"] != null && Request.QueryString["param"] != null && Request.QueryString["sessionno"] != null &&
                    Request.QueryString["schemeno"] != null && Request.QueryString["rdbtn"] != null && Request.QueryString["cat"] != null)
                {
                    if (Request.QueryString["cat"].ToString().Equals("summary"))
                        ShowCoursewiseStatistics(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["sessionno"]), Convert.ToInt32(Request.QueryString["schemeno"]), Convert.ToInt32(Request.QueryString["rdbtn"]), Convert.ToInt32(Request.QueryString["regstatus"]));
                    else
                        if (Request.QueryString["cat"].ToString().Equals("rolllist"))
                            ShowCoursewiseStudents(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["sessionno"]), Convert.ToInt32(Request.QueryString["schemeno"]), Convert.ToInt32(Request.QueryString["rdbtn"]), Convert.ToInt32(Request.QueryString["subid"]));
                    return;
                }
                //assignFacultyAdvisor.aspx
                //assignFacultyAdvisor.aspx
                if (Request.QueryString["path"] != null & Request.QueryString["FAC"] != null &
                    Request.QueryString["param"] != null & Request.QueryString["BRANCHNO"] != null & Request.QueryString["sem"] != null & Request.QueryString["DEGREENO"] != null)
                {
                    ShowFacultyReport(Convert.ToInt32(Request.QueryString["FAC"].ToString()), Convert.ToInt32(Request.QueryString["BRANCHNO"].ToString()), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["sem"].ToString()), Convert.ToInt32(Request.QueryString["DEGREENO"].ToString()));
                    //  commented by reena //ShowFacultyReport(Convert.ToInt32(Request.QueryString["FAC"].ToString()), Convert.ToInt32(Request.QueryString["DEPT"].ToString()), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["sem"].ToString()));
                    //ShowFacultyReport(Convert.ToInt32(Request.QueryString["DEGREENO"].ToString()), Convert.ToInt32(Request.QueryString["FAC"].ToString()), Convert.ToInt32(Request.QueryString["DEPT"].ToString()), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["sem"].ToString()));
                    return;
                }
                //offered course
                if (Request.QueryString["path"] != null & Request.QueryString["scheme"] != null &
                    Request.QueryString["param"] != null)
                {
                    ShowOfferedReport(Convert.ToInt32(Request.QueryString["scheme"].ToString()), Request.QueryString["param"].ToString(), Request.QueryString["path"].ToString());
                    return;
                }


                // added by yograj
                if (Request.QueryString["pagename"] != null && Request.QueryString["path"] != null && Request.QueryString["param"] != null)
                {
                    ShowTabulationChartReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString());
                    return;
                }


                //General Reports
                if (Request.QueryString["path"] != null & Request.QueryString["page"] == null && Request.QueryString["pagetitle"] != null)
                {
                    ShowGeneralReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString());
                    Page.Title = Request.QueryString["pagetitle"].ToString();
                    return;
                }

                //Dynamic Master Reports
                if (Request.QueryString["path"] == null & Request.QueryString["page"] != null)
                {
                    ShowMasterReport(Request.QueryString["page"].ToString(), Request.QueryString["param"].ToString());
                    return;
                }

                ////ShowLCReport(int idno, string param)

                //if (Request.QueryString["rptdpt"] != null && Request.QueryString["path"] != null && Request.QueryString["param"] != null && Request.QueryString["idno"] != null)
                //{
                //    ShowLCReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString(), Convert.ToInt32(Request.QueryString["idno"].ToString()));
                //    return;
                //}

                //Student Subjects Offered Report
                if (Request.QueryString["pathForStudentSubjects"] != null && Request.QueryString["paramForStudentSubjects"] != null)
                {
                    ShowStudentSubjectsOffered(Request.QueryString["pathForStudentSubjects"].ToString(), Request.QueryString["paramForStudentSubjects"].ToString());
                    Page.Title = Request.QueryString["pagetitle"].ToString();
                    return;
                }

                // Male Female Total Report

                if (Request.QueryString["pathForDegree"] != null && Request.QueryString["paramForDegree"] != null && Request.QueryString["dgno"] != null)
                {
                    ShowMaleFemaleTotalOnDegree(Request.QueryString["pathForDegree"].ToString(), Request.QueryString["paramForDegree"].ToString());
                    return;
                }

                //Show Report For Selected FeeItem

                if (Request.QueryString["pathForSelectedFeeItems"] != null && Request.QueryString["paramForSelectedFeeItemsRpt"] != null)
                {
                    ShowReportForSelectedFeeItem(Request.QueryString["pathForSelectedFeeItems"].ToString(), Request.QueryString["paramForSelectedFeeItemsRpt"].ToString());
                    Page.Title = Request.QueryString["pagetitleForSelectedFeeItem"].ToString();
                    return;
                }
                //Employee Abstract Salary Report

                if (Request.QueryString["pathForEmployeeAbstractSalary"] != null && Request.QueryString["paramForEmployeeAbstractSalary"] != null)
                {
                    ShowReportEmployeeAbstractSalary(Request.QueryString["pathForEmployeeAbstractSalary"].ToString(), Request.QueryString["paramForEmployeeAbstractSalary"].ToString());
                    Page.Title = Request.QueryString["pagetitleForEmployeeAbstractSalary"].ToString();
                    return;
                }

                //Employee Cummulative Abstract Report

                if (Request.QueryString["pathForEmployeeCummulativeAbstractSalary"] != null && Request.QueryString["paramForEmployeeCummulativeAbstractSalary"] != null)
                {
                    ShowReportForEmployeePaySlip(Request.QueryString["pathForEmployeeCummulativeAbstractSalary"].ToString(), Request.QueryString["paramForEmployeeCummulativeAbstractSalary"].ToString());
                    Page.Title = Request.QueryString["pagetitleForEmployeeCummulativeAbstractSalary"].ToString();
                    return;
                }


                //Show Report For caution money report

                if (Request.QueryString["PageTitleForSelectedCautionMoneyFeeItem"] != null)
                {
                    ShowReportForSelectedCautionMoney(Request.QueryString["Path"].ToString(), Request.QueryString["Param"].ToString());
                    Page.Title = Request.QueryString["PageTitleForSelectedCautionMoneyFeeItem"].ToString();
                    return;
                }


                //Show Report For blankmarksheet report
                if (Request.QueryString["PageTitleForBlankMarksheet"] != null)
                {
                    ShowReportForBlankMarksheet(Request.QueryString["Path"].ToString(), Request.QueryString["Param"].ToString());
                    Page.Title = Request.QueryString["PageTitleForBlankMarksheet"].ToString();
                    return;
                }

                //Student Annual Summary Report

                if (Request.QueryString["pathForAnnualSummary"] != null && Request.QueryString["paramForAnnualSummary"] != null)
                {
                    ShowReportAnnualSalarySummary(Request.QueryString["pathForAnnualSummary"].ToString(), Request.QueryString["paramForAnnualSummary"].ToString());
                    Page.Title = Request.QueryString["pagetitleForAnnualSummary"].ToString();
                }

                //Employee PaySlip Report

                if (Request.QueryString["pathForEmployeePaySlip"] != null && Request.QueryString["paramForEmployeePaySlip"] != null)
                {
                    ShowReportForEmployeePaySlip(Request.QueryString["pathForEmployeePaySlip"].ToString(), Request.QueryString["paramForEmployeePaySlip"].ToString());
                    Page.Title = Request.QueryString["pagetitleForEmployeePaySlip"].ToString();
                    return;
                }

                //Annual Salary Detail

                if (Request.QueryString["pathForAnnualSalaryDetail"] != null && Request.QueryString["paramForAnnualSalaryDetail"] != null)
                {
                    ShowReportAnnualSalaryDetail(Request.QueryString["pathForAnnualSalaryDetail"].ToString(), Request.QueryString["paramForAnnualSalaryDetail"].ToString());
                    Page.Title = Request.QueryString["pagetitleForAnnualSalaryDetail"].ToString();
                    return;
                }

                //General Report in selected format
                if (Request.QueryString["path"] != null & Request.QueryString["page"] == null && Request.QueryString["exporttype"] != null && Request.QueryString["filename"] != null)
                {
                    ShowGeneralExportReport(Request.QueryString["path"].ToString(), Request.QueryString["param"].ToString(), Request.QueryString["exporttype"].ToString(), Request.QueryString["filename"].ToString());
                    return;
                }

                //Employee Abstract Salary Report for multiple staff

                if (Request.QueryString["pathForEmployeeAbstractSalaryMultipleStaff"] != null && Request.QueryString["paramForEmployeeAbstractSalaryMultipleStaff"] != null)
                {
                    ShowReportEmployeeAbstractSalaryMultipleStaff(Request.QueryString["pathForEmployeeAbstractSalaryMultipleStaff"].ToString(), Request.QueryString["paramForEmployeeAbstractSalaryMultipleStaff"].ToString());
                    Page.Title = Request.QueryString["pagetitleForEmployeeAbstractSalaryMultipleStaff"].ToString();
                    return;
                }



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

    /// For Crystal Report 
    private void ShowGeneralReport(string path, string paramString)
    {
        /// Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

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

        crViewer.ReportSource = customReport;
        //Show Report directly in Acrobat Reader, but the browser window will not be closed
        //MemoryStream oStream;
        //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(oStream.ToArray());
        //Response.End();

        ////Export to PDF
        //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report"); 


        byte[] byteArray = null;
        Stream oStream;
        oStream = (Stream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        byteArray = new byte[oStream.Length];
        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        //Response.BinaryWrite(oStream.ToArray()); 
        Response.BinaryWrite(byteArray);
        Response.End();

    }


    private void ShowReportAnnualSalarySummary(string path, string param)
    {
        ////Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        Payroll_Report_Controller objPayrollContr = new Payroll_Report_Controller();
        try
        {
            // DataSet ds = objPayrollContr.RetrieveAnnualSalaryReportDetails(Request.QueryString["@P_FROM_DATE"].ToString(), Request.QueryString["@P_TO_DATE"].ToString(), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Request.QueryString["@P_ORDERBY"].ToString());
            DataSet ds = objPayrollContr.RetrieveAnnualSalaryReportDetails(Request.QueryString["@P_FROM_DATE"].ToString(), Request.QueryString["@P_TO_DATE"].ToString(), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_COLLEGE_NO"].ToString()));
            //DataSet ds = objPayrollContr.RetrieveAnnualSalaryReportDetails(Request.QueryString["@P_FROM_DATE"].ToString(), Request.QueryString["@P_TO_DATE"].ToString(), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()));
            //DataSet ds = objPayrollContr.RetrieveAnnualSalarySummaryReport(Request.QueryString["@P_FROM_DATE"].ToString(), Request.QueryString["@P_TO_DATE"].ToString(), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()));
            if (ds.Tables.Count <= 0)
                return;

            customReport.SetDataSource(ds.Tables[0]);

            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();

            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }
            //customReport.PrintToPrinter(1,false,0,0);
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");

            ////Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowReportAnnualSalaryDetail(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller objPayrollContr = new BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller();
        try
        {
            DataSet ds = objPayrollContr.RetrieveAnnual_Detail_Report(Request.QueryString["@P_FROM_DATE"].ToString(), Request.QueryString["@P_TO_DATE"].ToString(), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Request.QueryString["@P_MONTH"].ToString());

            if (ds.Tables.Count <= 0)
                return;

            customReport.SetDataSource(ds.Tables[0]);

            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();

            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }
            //customReport.PrintToPrinter(1,false,0,0);
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");

            ////Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowReportEmployeeAbstractSalary(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller objPayrollContr = new BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller();
        try
        {
            // DataSet ds = objPayrollContr.RetrieveEmployeePayslipDetails(Request.QueryString["@P_MON_YEAR"].ToString(), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()));
            DataSet ds = objPayrollContr.RetrieveEmployeePayslipDetails(Request.QueryString["@P_MON_YEAR"].ToString(), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_COLLEGE_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_EMPTYPENO"].ToString()));

            if (ds.Tables.Count <= 0)
                return;

            customReport.SetDataSource(ds.Tables[0]);

            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();

            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }

            ////Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////customReport.PrintToPrinter(1,false,0,0);
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowReportForEmployeePaySlip(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller objPayrollContr = new BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller();
        try
        {
            //DataSet ds = objPayrollContr.RetrieveEmployeePayslipDetails(Request.QueryString["@P_MON_YEAR"].ToString(), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()));
            DataSet ds = objPayrollContr.RetrieveEmployeePayslipDetails(Request.QueryString["@P_MON_YEAR"].ToString(), Convert.ToInt32(Request.QueryString["@P_STAFF_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_COLLEGE_NO"].ToString()), Convert.ToInt32(Request.QueryString["@P_EMPTYPENO"].ToString()));
            //Session["colcode"].ToString());

            if (ds.Tables.Count <= 0)
                return;

            customReport.SetDataSource(ds.Tables[0]);

            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();

            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }
            ////Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            //customReport.PrintToPrinter(1,false,0,0);
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");


        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }


    private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }

    private void ShowMasterReport(string page, string param)
    {
        //Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath("~\\Reports\\Masters\\" + "rptMasters.rpt");
        customReport.Load(reportPath);

        string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
        Masters objMasters = new Masters(page);

        //get columns
        string colnames = string.Empty;
        string[] fields = objMasters.captions[0, 5].Split(',');
        for (int i = 1; i < fields.Length - 1; i++)
        {
            colnames += fields[i] + " AS Column" + i.ToString() + ",";
        }
        colnames = colnames.Substring(0, colnames.Length - 1);

        string whereCondition = " where " + fields[0] + ">0";

        string sql = "SELECT " + colnames + " FROM " + page + whereCondition;

        sql = sql + "SELECT COLLEGENAME,COLLEGE_ADDRESS,COLLEGE_LOGO FROM REFF";

        System.Data.DataSet ds = objSqlHelper.ExecuteDataSet(sql);

        //ds.WriteXml(Server.MapPath("~") +  "\\test.xml");
        customReport.SetDataSource(ds);
        //customReport.SetDataSource(ds.Tables[0]);
        //customReport.SetDataSource(ds.Tables["Table1"]);
        crViewer.ReportSource = customReport;
        crViewer.DataBind();

        Session["reportdata"] = customReport;

        //Parameter to Report Document
        //============================
        //Extract Parameters from querystring
        char ch = ',';
        string[] val = param.Split(ch);
        for (int i = 0; i < val.Length; i++)
        {
            string par = val[i].Substring(0, val[i].IndexOf('='));
            string value = val[i].Substring(val[i].IndexOf('=') + 1);
            customReport.SetParameterValue("" + par + "", value);
        }

        //Set Column Parameters to empty values
        customReport.SetParameterValue("@Col1", "");
        customReport.SetParameterValue("@Col2", "");
        customReport.SetParameterValue("@Col3", "");
        customReport.SetParameterValue("@Col4", "");

        //Overwrite Column Parameters to values according to fields
        string col = string.Empty;
        for (int i = 0; i < int.Parse(objMasters.captions[0, 4]); i++)
        {
            col = "@Col" + (i + 1).ToString();
            customReport.SetParameterValue(col, objMasters.captions[i, 0]);
        }

        //Show Report directly in Acrobat Reader, but the browser window will not be closed
        MemoryStream oStream;
        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(oStream.ToArray());
        Response.End();

        ////Export to PDF
        //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        //((CrystalDecisions.CrystalReports.Engine.FieldObject)customReport.ReportDefinition.Sections[3].ReportObjects[2]).FieldFormat.CommonFormat;
        //CrystalDecisions.CrystalReports.Engine.TextObject txt = ((CrystalDecisions.CrystalReports.Engine.TextObject)customReport.ReportDefinition.Sections[2].ReportObjects["Text1"]);
        //((CrystalDecisions.CrystalReports.Engine.FieldObject)customReport.ReportDefinition.Sections[2].ReportObjects["Col11"]).Name = "TEST";

        //((CrystalDecisions.CrystalReports.Engine.FieldObject)customReport.ReportDefinition.Sections[3].ReportObjects["RecordNumber1"]).Left = 1;
    }

    private void ShowReportEmployeeAbstractSalaryMultipleStaff(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller objPayrollContr = new BusinessLogicLayer.BusinessLogic.Payroll_Report_Controller();
        try
        {
            DataSet ds = objPayrollContr.RetrieveEmployeeAbstractSalaryMultipleStaff(Convert.ToInt32(Request.QueryString["@P_COLLEGE_CODE"].ToString()), Request.QueryString["@P_MON_YEAR"].ToString(), Request.QueryString["@P_STAFF_NO"].ToString(), Convert.ToInt32(Request.QueryString["@P_IDNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_COLLEGE_NO"].ToString()));

            if (ds.Tables.Count <= 0)
                return;

            customReport.SetDataSource(ds.Tables[0]);

            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();

            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }

            ////Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////customReport.PrintToPrinter(1,false,0,0);
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowCoursewiseStatistics(string path, string param, int sessionno, int schemeno, int rdbt, int regstatus)
    {
        //Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        StudentRegistration objSregist = new StudentRegistration();

        try
        {
            DataSet ds = objSregist.GetRegistTotalStudents(sessionno, schemeno, rdbt, regstatus);

            customReport.SetDataSource(ds.Tables[0]);
            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowCoursewiseStudents(string path, string param, int sessionno, int schemeno, int rdbt, int subid)
    {
        //Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        StudentRegistration objSregist = new StudentRegistration();

        try
        {
            DataSet ds = objSregist.GetCourseWiseStudents(sessionno, schemeno, rdbt, subid);

            customReport.SetDataSource(ds.Tables[0]);
            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowRegistrationSlips(string path, string param)
    {
        //Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        try
        {
            customReport.Load(reportPath);

            //configurereport
            ConfigureCrystalReports(customReport);

            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //customReport.SetParameterValue("@P_SESSIONNO", sessionno);
            //customReport.SetParameterValue("@P_SCHEMENO", schemeno);
            //customReport.SetParameterValue("@P_FAC_ADVISOR", userno);

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowFacultyReport(int FAC, int DEPT, string param, int sem, int degreeno)
    {
        try
        {
            //Set Report
            customReport = new ReportDocument();
            string reportPath = Server.MapPath("~\\Reports\\Academic\\" + "rptFaculty_Advisor.rpt");
            customReport.Load(reportPath);
            StudentController objSC = new StudentController();
            //System.Data.DataSet ds = objSC.GetStudentForFaculty(FAC, BATCH, BRANCH, SEM, SECTIONNO, FLAG, RB);
            System.Data.DataSet ds = objSC.GetStudentForFaculty(FAC, DEPT, sem, degreeno);
            customReport.SetDataSource(ds.Tables[0]);
            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //Export to PDF
            customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {

            Response.Write("Please Select the proper data");
        }
    }
    private void ShowOfferedReport(int scheme, string param, string path)
    {
        //Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));

        try
        {
            customReport.Load(reportPath);

            //configurereport
            ConfigureCrystalReports(customReport);

            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //Set Column Parameters to empty values
            customReport.SetParameterValue("@P_SCHEMENO", scheme);
            //customReport.SetParameterValue("@Col2", "");

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {
            Response.Write("Please Select Proper Data for Report");
        }
    }

    // rpt for student registration
    private void ShowStudRegistration(string path, string param)
    {
        //Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));

        try
        {
            customReport.Load(reportPath);

            //configurereport
            ConfigureCrystalReports(customReport);

            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {
            //Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowDeptWiseCourseReport(string path, string param, int deptno)
    {
        try
        {
            //Set Report
            customReport = new ReportDocument();
            string reportPath = Server.MapPath("~\\Reports\\Academic\\" + "rptDeptwiseCourseName.rpt");
            customReport.Load(reportPath);
            CourseController objCC = new CourseController();
            System.Data.DataSet ds = objCC.GetDeptWiseCourse(deptno);

            string ccode = string.Empty;
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 14; i < 15; i++)
                {
                    string coursenos = ds.Tables[0].Rows[j][i].ToString() == "" ? "0" : ds.Tables[0].Rows[j][i].ToString();
                    ccode = string.Empty;
                    if (coursenos != "0")
                    {
                        char sp = ',';
                        string[] courses = coursenos.Split(sp);
                        for (int k = 0; k < courses.Length; k++)
                        {
                            ccode += objCC.GetCCodeByCourseno(Convert.ToInt16(courses[k])) + " ";
                        }
                        ds.Tables[0].Rows[j][i] = ccode;
                    }
                }
            }

            customReport.SetDataSource(ds.Tables[0]);
            crViewer.ReportSource = customReport;
            crViewer.DataBind();

            Session["reportdata"] = customReport;

            //Parameter to Report Document
            //============================
            //Extract Parameters from querystring
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue("" + par + "", value);
            }

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {

            Response.Write("Please Select the proper data");
        }

    }

    //private void ShowStudentSubjectsOffered(string path, string param)
    //{
    //    //Set Report 
    //    customReport = new ReportDocument();
    //    string reportPath = Server.MapPath(path.Replace(",", "\\"));
    //    customReport.Load(reportPath);
    //    StudentController objSController = new StudentController();
    //    try
    //    {
    //        DataSet ds = objSController.GetStudentSubjectsOffered(Convert.ToInt32(Request.QueryString["@P_SESSIONNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_SCHEMENO"].ToString()), Convert.ToInt32(Request.QueryString["@P_SEMESTERNO"].ToString()));
    //        customReport.SetDataSource(ds.Tables[0]);
    //        //crViewer.ReportSource = customReport;
    //        //crViewer.DataBind();

    //        //Session["reportdata"] = customReport;

    //        //Parameter to Report Document
    //        //================================
    //        //Extract Parameters From queryString
    //        char ch = ',';
    //        string[] val = param.Split(ch);
    //        for (int i = 0; i < val.Length; i++)
    //        {
    //            string par = val[i].Substring(0, val[i].IndexOf('='));
    //            string value = val[i].Substring(val[i].IndexOf('=') + 1);
    //            customReport.SetParameterValue(par, value);
    //        }

    //        //Show Report directly in Acrobat Reader, but the browser window will not be closed
    //        MemoryStream oStream;
    //        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.ContentType = "application/pdf";
    //        Response.BinaryWrite(oStream.ToArray());
    //        Response.End();


    //        ////Export to PDF
    //        //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("Please Select Proper Data for Report");
    //    }
    //}

    private void ShowStudentSubjectsOffered(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        StudentController objSController = new StudentController();
        try
        {
            DataSet ds = objSController.GetStudentSubjectsOffered(Convert.ToInt32(Request.QueryString["@P_SESSIONNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_SCHEMENO"].ToString()), Convert.ToInt32(Request.QueryString["@P_SEMESTERNO"].ToString()), Convert.ToInt32(Request.QueryString["@P_SECTIONNO"].ToString()));
            //DataSet ds2 = objSController.GetCollege_Logo_Information();
            //ds2 = ds1.Copy();
            customReport.SetDataSource(ds.Tables[0]);
            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();
            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }

            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();
            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");
        }
        catch (Exception ex)
        {
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowMaleFemaleTotalOnDegree(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        StudentController objSController = new StudentController();

        try
        {
            DataSet ds = objSController.GetMaleFemaleTotalOnDegree(Request.QueryString["dgno"].ToString());
            customReport.SetDataSource(ds.Tables[0]);

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }
            //Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            ////Export to PDF
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report");

        }
        catch (Exception ex)
        {
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowReportForSelectedFeeItem(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);
        DailyFeeCollectionController dfcController = new DailyFeeCollectionController();
        try
        {
            string[] feeHeads = new string[] 
            { 
                Request.QueryString["FeeHead1"].ToString(),
                Request.QueryString["FeeHead2"].ToString(),
                Request.QueryString["FeeHead3"].ToString(),
                Request.QueryString["FeeHead4"].ToString(),
                Request.QueryString["FeeHead5"].ToString(),
                Request.QueryString["FeeHead6"].ToString()
            };

            DailyFeeCollectionRpt rptParams = new DailyFeeCollectionRpt();
            rptParams.ReceiptTypes = Request.QueryString["ReceiptCode"].ToString();
            rptParams.FromDate = Convert.ToDateTime(Request.QueryString["FromDate"].ToString());
            rptParams.ToDate = Convert.ToDateTime(Request.QueryString["ToDate"].ToString());
            rptParams.SemesterNo = Convert.ToInt32(Request.QueryString["SemesterNo"].ToString());
            rptParams.BranchNo = Convert.ToInt32(Request.QueryString["BranchNo"].ToString());
            rptParams.DegreeNo = Convert.ToInt32(Request.QueryString["DegreeNo"].ToString());
            rptParams.YearNo = Convert.ToInt32(Request.QueryString["YearNo"].ToString());
            DailyFeeCollectionController dfcContr = new DailyFeeCollectionController();
            DataSet ds = dfcContr.GetSelectedFeeItem(rptParams, feeHeads);

            //if (ds.Tables.Count <= 0)
            //    return;

            customReport.SetDataSource(ds.Tables[0]);

            //crViewer.ReportSource = customReport;
            //crViewer.DataBind();

            //Session["reportdata"] = customReport;

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            char ch = ',';
            string[] val = param.Split(ch);
            for (int i = 0; i < val.Length; i++)
            {
                string par = val[i].Substring(0, val[i].IndexOf('='));
                string value = val[i].Substring(val[i].IndexOf('=') + 1);
                customReport.SetParameterValue(par, value);
            }
            ////Show Report directly in Acrobat Reader, but the browser window will not be closed
            MemoryStream oStream;
            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();

            //customReport.PrintToPrinter(1,false,0,0);
            //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }


    private void ShowReportForSelectedCautionMoney(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        DailyFeeCollectionController dfcController = new DailyFeeCollectionController();
        try
        {
            DailyFeeCollectionRpt rptParams = new DailyFeeCollectionRpt();
            rptParams.ReceiptTypes = Request.QueryString["@P_RECIEPT_CODE"];
            rptParams.ToDate = Convert.ToDateTime(Request.QueryString["@P_TO_DATE"]);
            rptParams.FromDate = Convert.ToDateTime(Request.QueryString["@P_FROM_DATE"]);
            rptParams.DegreeNo = Convert.ToInt32(Request.QueryString["@P_DEGREENO"]);
            rptParams.BranchNo = Convert.ToInt32(Request.QueryString["@P_BRANCHNO"]);
            rptParams.YearNo = Convert.ToInt32(Request.QueryString["@P_YEAR"]);
            rptParams.SemesterNo = Convert.ToInt32(Request.QueryString["@P_SEMESTERNO"]);
            rptParams.PaidAmount = Convert.ToBoolean(Request.QueryString["@P_PAIDAMOUNT"]);
            DataSet ds = dfcController.GetCautionMoneyReportData(rptParams, Request.QueryString["@P_FEEHEAD"]);

            if (ds != null && ds.Tables.Count > 0)
            {
                customReport.SetDataSource(ds.Tables[0]);
                customReport.SetParameterValue("UserName", param);
                customReport.SetParameterValue("FromDate", Request.QueryString["@P_FROM_DATE"]);
                customReport.SetParameterValue("ToDate", Request.QueryString["@P_TO_DATE"]);

                MemoryStream oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
            }
            else
            {
                Response.Write("Data Not Found!!");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }

    private void ShowReportForBlankMarksheet(string path, string param)
    {
        //Set Report 
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        try
        {
            //OLD
            //string schemeNo = Request.QueryString["@P_SCHEME_NO"];
            //string semesterNo = Request.QueryString["@P_SEMESTER_NO"];
            //string courseNo = Request.QueryString["@P_COURSE_NO"];
            //string controllsheetNo = Request.QueryString["@P_CONTROLSHEET_NO"];
            //string examNo = Request.QueryString["@P_EXAMNO"];

            string sessionnNo = Request.QueryString["@P_SESSIONNO"];
            string semesterNo = Request.QueryString["@P_SEMESTER_NO"];
            string deptNo = Request.QueryString["@P_DEPTNO"];
            string controllsheetNo = Request.QueryString["@P_CONTROLSHEET_NO"];
            string examNo = Request.QueryString["@P_EXAMNO"];

            StudentController objSController = new StudentController();

            //OLD
            //DataSet ds = objSController.GetStudentBlankMarksheet(schemeNo,semesterNo,courseNo,controllsheetNo,examNo);
            DataSet ds = objSController.GetStudentBlankMarksheet(sessionnNo, deptNo, semesterNo, controllsheetNo, examNo);
            if (ds != null && ds.Tables.Count > 0)
            {
                customReport.SetDataSource(ds.Tables[0]);
                //customReport.SetParameterValue("UserName", param);
                customReport.SetParameterValue("ExamName", param);

                MemoryStream oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
            }
            else
            {
                Response.Write("Data Not Found!!");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.Write("Please Select Proper Data for Report");
        }
    }

    //Added By Yograj
    private void ShowTabulationChartReport(string path, string paramString)
    {

        DataSet ds = (DataSet)Session["TabulationChart"];

        /// Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

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
                        //customReport.SetParameterValue(paramName, value);
                        if (paramName.ToLower() == "@P_xml".ToLower())
                        {
                            customReport.SetParameterValue(paramName, ds.GetXml());
                        }
                        else
                        {
                            customReport.SetParameterValue(paramName, value);
                        }

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

        crViewer.ReportSource = customReport;
        //Show Report directly in Acrobat Reader, but the browser window will not be closed
        //MemoryStream oStream;
        //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(oStream.ToArray());
        //Response.End();

        ////Export to PDF
        //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Report"); 


        byte[] byteArray = null;
        Stream oStream;
        oStream = (Stream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        byteArray = new byte[oStream.Length];
        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        //Response.BinaryWrite(oStream.ToArray()); 
        Response.BinaryWrite(byteArray);
        Response.End();

    }


    //private void ShowReportForRelieving(string path, string param)
    //{
    //    //Set Report 
    //    customReport = new ReportDocument();
    //    string reportPath = Server.MapPath(path.Replace(",", "\\"));
    //    customReport.Load(reportPath);
    //    //StudentController objSController = new StudentController();

    //    try
    //    {
    //        IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.StaffDevelopmentCellController objSDCC = new IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.StaffDevelopmentCellController();
    //        DataSet ds = objSDCC.GetFacultyTADA(Convert.ToInt32(Request.QueryString["@P_IDNO"]), Request.QueryString["@P_TYPE"].ToString());
    //        if (ds.Tables.Count <= 0)
    //            return;

    //        customReport.SetDataSource(ds.Tables[0]);

    //        //crViewer.ReportSource = customReport;
    //        //crViewer.DataBind();

    //        //Session["reportdata"] = customReport;

    //        //Parameter to Report Document
    //        //================================
    //        //Extract Parameters From queryString
    //        char ch = ',';
    //        string[] val = param.Split(ch);
    //        for (int i = 0; i < val.Length; i++)
    //        {
    //            string par = val[i].Substring(0, val[i].IndexOf('='));
    //            string value = val[i].Substring(val[i].IndexOf('=') + 1);
    //            customReport.SetParameterValue(par, value);
    //        }

    //        ////Show Report directly in Acrobat Reader, but the browser window will not be closed
    //        MemoryStream oStream;
    //        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.ContentType = "application/pdf";
    //        Response.BinaryWrite(oStream.ToArray());
    //        Response.End();

    //        ////customReport.PrintToPrinter(1,false,0,0);
    //        //customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Registration");

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.ToString());
    //        Response.Write("Please Select Proper Data for Report");
    //    }
    //}

    protected void crViewer_Unload(object sender, EventArgs e)
    {
        //customReport.Close();
        //customReport.Disposed();
    }

    //export to pdf/xls/doc
    private void ShowGeneralExportReport(string path, string paramString, string exportformat, string filename)
    {
        /// Set Report
        customReport = new ReportDocument();

        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

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

        crViewer.ReportSource = customReport;
        //Show Report directly in Acrobat Reader, but the browser window will not be closed
        MemoryStream oStream;



        ////export to pdf formate
        if (exportformat == "pdf")
        {

            byte[] byteArray = null;
            Stream oStream1;
            oStream1 = (Stream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename.Replace(" ", "_")));
            byteArray = new byte[oStream1.Length];
            oStream1.Read(byteArray, 0, Convert.ToInt32(oStream1.Length - 1));

            Response.Clear();

            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(byteArray);
            Response.End();

        }

        //export to execl formate
        else if (exportformat == "xls")
        {

            //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename.Replace(" ", "_")));
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel";

            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();

            byte[] byteArray = null;
            Stream Stream;
            Stream = (Stream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            byteArray = new byte[Stream.Length];
            Stream.Read(byteArray, 0, Convert.ToInt32(Stream.Length - 1));

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            //Response.BinaryWrite(oStream.ToArray()); 
            Response.BinaryWrite(byteArray);
            Response.End();

        }
        //export to word formate
        else if (exportformat == "doc")
        {

            oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename.Replace(" ", "_")));
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-word";

            Response.BinaryWrite(oStream.ToArray());
            Response.End();

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        try
        {
            customReport.Close();
            customReport.Dispose();
        }
        catch (Exception ex)
        {
        }
    }
}
