
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Web;
using System.IO;
using System.Data;

public partial class VEHICLE_MAINTENANCE_Reports_StudentTransportStatusReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    GridView gvStudentTransportReport = new GridView();
    VMController ObjCon = new VMController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                }
                FillDropDown();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }
    protected void FillDropDown()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "DEGREENO<>0", "DEGREENO");
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO<>0 ", "SEMESTERNO");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO<>0", "BRANCHNO");
        objCommon.FillDropDownList(ddlyear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR<>0", "YEAR");
        Session["Semester"]     = 0;
        Session["Degree"]       = 0;
        Session["Branch"]       = 0;
        Session["Year"]         = 0;
        Session["Status"]       = 0;

    }
    private void ShowTransportReport(string reportTitle, string rptFileName, string exporttype)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            if (exporttype == "xls")
            {
                url += "exporttype=" + exporttype;
                url += "&filename=StudentTransportReport";
            }
            else
            {
                url += "pagetitle=" + reportTitle;
            }

            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@SEMESTERNO=" + Session["Semester"] + ",@BRANCHNO=" + Session["Branch"] + ",@DEGREENO=" + Session["Degree"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@YEAR=" + Session["Year"].ToString() + ",@STATUS=" + Session["Status"].ToString();
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["Degree"] = 0;
        Session["Degree"] = ddlDegree.SelectedValue;


    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Branch"] = 0;
        Session["Branch"] = ddlBranch.SelectedValue;

    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Semester"] = 0;
        Session["Semester"] = ddlSem.SelectedValue;

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowTransportReport("Studetnt Transport Status Report", "rptStudentTransportStatusReport.rpt","doc");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Year"] = 0;
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO<>0 AND YEARNO=" + ddlyear.SelectedValue + "OR " + ddlyear.SelectedValue + "=0 AND SEMESTERNO<>0 ", "SEMESTERNO");
        Session["Year"] = ddlyear.SelectedValue;
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Status"] = 0;
        Session["Status"] = ddlstatus.SelectedValue;

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //ShowTransportReport("Studetnt Transport Status Report", "rptStudentTransportStatusReport.rpt", "xls");
        ExportExcel();
    }


    private void ExportExcel()
    {
        DataSet ds = null;
        try
        {     
            ds = ObjCon.GetStudentTransportStatus(Convert.ToInt32(Session["Semester"]),Convert.ToInt32(Session["Branch"]),Convert.ToInt32(Session["Degree"]),Convert.ToInt32(Session["Year"]),Convert.ToInt32(Session["Status"]));

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvStudentTransportReport.RowDataBound += new GridViewRowEventHandler(gvStudentTransportReport_RowDataBound);

                    gvStudentTransportReport.DataSource = ds.Tables[0];
                    gvStudentTransportReport.DataBind();

                    //AddHeader();
                    AddReportHeader("Student", gvStudentTransportReport);
                    string FileName = "Report_StudentTransportReport.xls";
                    string attachment = "attachment; filename=" + FileName;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", attachment);
                    Response.AppendHeader("Refresh", ".5; StudentTransportStatusReport.aspx");
                    Response.Charset = "";
                    Response.ContentType = "application/" + ContentType;
                    StringWriter sw1 = new StringWriter();
                    HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
                    gvStudentTransportReport.RenderControl(htw1);
                    Response.Output.Write(sw1.ToString());
                    HttpContext.Current.Response.Flush();

                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    gvStudentTransportReport.DataSource = null;
                    gvStudentTransportReport.DataBind();
                }
            }
           

            //AddHeader();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvStudentTransportReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvStudentTransportReport.HeaderRow.Cells.Count; i++)
                {
                    string Header = gvStudentTransportReport.HeaderRow.Cells[i].Text;
                    if (Header.ToString().Contains("/") || Header.ToString().Contains("Bill Booked") || Header.ToString().Contains("Amount Paid") || Header.ToString().Contains("Out") || Header.ToString().Contains("SumBillBooked") || Header.ToString().Contains("SumAmountPaid") || Header.ToString().Contains("Amount"))
                    {
                        //e.Row.Cells[i].Text = String.Format("{0:N2}", e.Row.Cells[i].Text == "&nbsp;" ? 0.00 : Convert.ToDouble(e.Row.Cells[i].Text));
                        e.Row.Cells[i].Text = String.Format("{0:N2}", Convert.ToDouble(e.Row.Cells[i].Text));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void AddReportHeader(string HeaderType, GridView gv)
    {
        try
        {
            if (HeaderType == "Student")
            {
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Header1Cell = new TableCell();

                Header1Cell.Text = "Sri Venkateswara College of Engineering";
                Header1Cell.ColumnSpan = 6;
                Header1Cell.Font.Size = 14;
                Header1Cell.Font.Bold = true;
                Header1Cell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(Header1Cell);
                gv.Controls[0].Controls.AddAt(0, HeaderGridRow1);

                GridViewRow HeaderGridRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Header2Cell = new TableCell();

                Header2Cell.Text = "Sriperumbudur Tk, Kancheepuram District, Tamilnadu, India - 602117";
                Header2Cell.ColumnSpan = 6;
                Header2Cell.Font.Size = 12;
                Header2Cell.Font.Bold = true;
                Header2Cell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(Header2Cell);
                gv.Controls[0].Controls.AddAt(1, HeaderGridRow2);

                GridViewRow HeaderGridRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Header3Cell = new TableCell();

                Header3Cell.Text = "STUDENT TRANSPORT STATUS REPORT";
                Header3Cell.ColumnSpan = 6;
                Header3Cell.Font.Size = 12;
                Header3Cell.Font.Bold = true;
                Header3Cell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow3.Cells.Add(Header3Cell);
                gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

                
            }
            
            gv.FooterStyle.Font.Bold = true;
            gv.FooterStyle.Font.Size = 12;
            //
            //gv.FooterRow.Font.Bold = true;
            //gv.FooterRow.Font.Size = 12;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}