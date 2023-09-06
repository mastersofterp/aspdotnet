//=======================================================================================
//CREATED BY -  IFTAKHAR KHAN
//DATED -       3-ARPIL-2014
//MODIFIED ON - Jay Takalkhede
//APPROVED BY - 25-08-2023
//PURPOSE -     THIS PAGE IS USED TO GENERATE REPORT OF DIFFERENT FORMAT AND CANCEL/REPRINT OF RECEIPT
// Version      :- 1) RFC.bug.Minor.1 (04-09-2023)
//=======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.IO;
using System.ComponentModel;
using ClosedXML.Excel;

public partial class ACADEMIC_Miscellaneous_Collection_Register : System.Web.UI.Page
{

    #region Page Load
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentSelectFieldController objStudContrl = new StudentSelectFieldController();
    StudentController objSC = new StudentController();
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
                //CheckPageAuthorization();
            }
            objCommon.FillDropDownList(ddlCashbook, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "BELONGS_TO='M'", "");

            ddlHead.Visible = false;
            lblHead.Visible = false;
            pnlhead.Visible = false;
            lblCounter.Visible = false;
            Pnlreportbtn.Visible = false;
            txtCounter.Visible = false;
            ImageButton3.Visible = true;
            btnSummaryReport.Visible = true;
            // btnBulkReport.Visible = true;
            // ImageButton1.Visible = true;
            ddlPaytype.SelectedValue = "R";
            this.objCommon.FillDropDownList(ddlSearchPanel, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID IN(13,9,2)  ", "SRNO");
            ddlSearchPanel.SelectedIndex = 0;
            ddlSearchPanel_SelectedIndexChanged(sender, e);
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
                if (rdbReports.SelectedValue == "1")
                {
                    btnExcelRcpit.Visible = false;
                    btnDCRreport.Visible = true;
                    ImageButton1.Visible = false;
                }
            }
            else
            {
                rdbReports.Items.RemoveAt(3);
                if (rdbReports.SelectedValue == "1")
                {
                    ImageButton1.Visible = true;
                    btnExcelRcpit.Visible = false;
                    btnDCRreport.Visible = true;
                    ImageButton3.Visible = true;

                }
            }
        }
        else
        {
            if (rdbReports.SelectedValue == "5")
            {
                Pnlreportbtn.Visible = true;
            }
            else
                Pnlreportbtn.Visible = false;
        }

    }
    #endregion Page Load


    #region //This section is used to generate report in differet format

    private void Export(string type)
    {

        string filename = string.Empty;
        string ContentType = string.Empty;

        if (type == "Excel")
        {
            filename = "SelectedFieldReport.xls";       //USED TO GENERATE IN EXCEL FORMAT
            ContentType = "ms-excel";
        }
        else if (type == "Word")
        {
            filename = "SelectedFieldReport.doc";       //USED TO GENERATE IN WORD FORMAT
            ContentType = "vnd.word";
        }

        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + ContentType;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void imgbutExporttoexcel_Click(object sender, EventArgs e)
    {
        //this.Export("Excel");
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
        }
        else if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
        {
            //RFC.bug.Minor.1 (04-09-2023) Added By Jay Takalkhede On dated 04092023 (TktNo.47732)
            objCommon.DisplayMessage(this.updpnlMain, "To Date should be greater than From Date", this.Page);
            return;
        }
        else if (rdbReports.SelectedValue == "1")
        {
            ShowReport("xls", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
        }
        else if (rdbReports.SelectedValue == "2")
        {
            ShowReport("xls", "Miscellanious Summary Fees", "rptMiscSummary.rpt");
        }
        else if (rdbReports.SelectedValue == "3")
        {
            if (ddlHead.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updpnlMain, "Please Select Head", this.Page);
                return;
            }
            ShowReport("xls", "Miscellanious HeadWise Report", "rptMiscHeadwise.rpt");
        }
        else if (rdbReports.SelectedValue == "4" || rdbReports.SelectedValue == "6")
        {
            ShowReport("xls", "Miscellanious BankWise Report", "rptMiscBankWiseCollection.rpt");
        }


    }
    protected void imgbutExporttoWord_Click(object sender, EventArgs e)
    {
        // --- Excel btn patch -- added by Hemanth G on 18042019
        int ChkExport = Convert.ToInt32((ConfigurationManager.AppSettings["ExcelExport"] == null || ConfigurationManager.AppSettings["ExcelExport"].ToString() == "") ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["ExcelExport"]));

        if (ChkExport == 1)
        {
            //this.Export("Word");rptMiscReport_duplicate
            if (txtFromDate.Text == "" || txtTodate.Text == "")
            {
                objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
            }
            else if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
            {      //RFC.bug.Minor.1 (04-09-2023) Added By Jay Takalkhede On dated 04092023 (TktNo.47732)
                objCommon.DisplayMessage(this.updpnlMain, "To Date should be greater than From Date", this.Page);
                return;
            }
            else if (rdbReports.SelectedValue == "1")
            {
                ShowReport("doc", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
            }
            else if (rdbReports.SelectedValue == "2")
            {
                ShowReport("doc", "Miscellanious Summary Fees", "rptMiscSummary.rpt");
            }
            else if (rdbReports.SelectedValue == "3")
            {
                if (ddlHead.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this.updpnlMain, "Please Select Head", this.Page);
                    return;
                }
                ShowReport("doc", "Miscellanious HeadWise Report", "rptMiscHeadwise.rpt");
            }
            else
            {
                ShowReport("doc", "Miscellanious BankWise Report", "rptMiscBankWiseCollection.rpt");
            }
        }
        else
        {
            string description = Common.ToDescriptionString(CustomStatus.ExportExcel);
            objCommon.DisplayMessage(this.updpnlMain, description, this.Page);
            return;
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void rdbReports_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbReports.SelectedValue == "1")
        {
            pnlhead.Visible = false;
            ddlHead.Visible = false;
            lblHead.Visible = false;
            pnldate.Visible = true;
            ddlCashbook.SelectedIndex = 0;
            ddlPaytype.SelectedIndex = 0;
            txtCounter.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            clearfield();
            ddlPaytype.SelectedValue = "R";
            usnno.Visible = false;
            txtCounter.Visible = false;
            Panelrdocancel.Visible = true;
            btnBulkReport.Visible = false;
            btnSummaryReport.Visible = true;
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
                if (rdbReports.SelectedValue == "1")
                {
                    btnExcelRcpit.Visible = false;
                    btnDCRreport.Visible = true;
                    ImageButton1.Visible = false;
                    ImageButton3.Visible = true;
                    btnSummaryReport.Visible = true;

                }
            }
            else
            {
                if (rdbReports.SelectedValue == "1")
                {
                    ImageButton1.Visible = true;
                    // ImageButton2.Visible = true;
                    btnExcelRcpit.Visible = false;
                    btnDCRreport.Visible = true;
                    //   Panelsortby.Visible = false;
                    ImageButton3.Visible = true;
                    btnSummaryReport.Visible = true;

                }
            }
        }
        else if (rdbReports.SelectedValue == "2")
        {
            pnlhead.Visible = false;
            ddlHead.Visible = false;
            lblHead.Visible = false;
            pnldate.Visible = true;
            txtCounter.Text = string.Empty;
            ddlCashbook.SelectedIndex = 0;
            ddlPaytype.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            clearfield();
            usnno.Visible = false;
            txtCounter.Visible = false;
            Panelrdocancel.Visible = false;
            //    Panelsortby.Visible = false;
            ImageButton3.Visible = false;
            btnBulkReport.Visible = false;
            btnSummaryReport.Visible = false;

        }
        else if (rdbReports.SelectedValue == "3")
        {

            pnlhead.Visible = true;
            ddlHead.Visible = true;
            txtCounter.Text = string.Empty;
            lblHead.Visible = true;
            pnldate.Visible = true;
            ddlCashbook.SelectedIndex = 0;
            //ddlPaytype.SelectedIndex = 0;
            ImageButton1.Visible = false;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            txtCounter.Visible = false;
            clearfield();
            ddlPaytype.SelectedValue = "R";
            usnno.Visible = false;
            btnExcelRcpit.Visible = false;
            btnDCRreport.Visible = false;
            //   Panelsortby.Visible = false;
            ImageButton3.Visible = true;
            Panelrdocancel.Visible = false;
            ImageButton3.Visible = true;
            btnBulkReport.Visible = false;
            btnSummaryReport.Visible = false;
        }
        else if (rdbReports.SelectedValue == "5")
        {
            pnlbutton.Visible = true;
            ddlCashbook.SelectedIndex = 0;
            ddlPaytype.SelectedIndex = 0;
            txtCounter.Text = string.Empty;
            ddlHead.Visible = false;
            pnlhead.Visible = false;
            lblHead.Visible = false;
            lblCounter.Visible = true;
            txtCounter.Visible = true;
            // btnShow.Visible = true;
            //ddlPaytype.Enabled = false;
            btnCancel.Enabled = true;
            btnCancel.Visible = true;
            btnBulkReport.Visible = false;
            btnReprint.Enabled = true;
            btnReprint.Visible = true;
            pnldate.Visible = false;
            Pnlreportbtn.Visible = true;
            pnlReport.Visible = false;
            ddlPaytype.Visible = true;
            pnlPaymenttype.Visible = true;
            ImageButton1.Visible = false;
            ddlPaytype.SelectedValue = "R";
            Panelrdocancel.Visible = false;
            //  Panelsortby.Visible = false;
            //  usnno.Visible = true;
            // divpanel.Visible = true;
            btnSummaryReport.Visible = false;
        }
        else if (rdbReports.SelectedValue == "4")
        {
            pnlhead.Visible = false;
            ddlHead.Visible = false;
            lblHead.Visible = false;
            pnldate.Visible = true;
            btnBulkReport.Visible = false;
            ddlCashbook.SelectedIndex = 0;
            ddlPaytype.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtCounter.Text = string.Empty;
            txtCounter.Visible = false;
            txtTodate.Text = string.Empty;
            clearfield();
            usnno.Visible = false;
            Panelrdocancel.Visible = false;
            //  Panelsortby.Visible = false;

        }
        else if (rdbReports.SelectedValue == "6")
        {
            pnlhead.Visible = false;
            ddlHead.Visible = false;
            lblHead.Visible = false;
            pnldate.Visible = true;
            usnno.Visible = false;
            ddlCashbook.SelectedIndex = 0;
            ddlPaytype.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            clearfield();
            txtCounter.Text = string.Empty;
            txtCounter.Visible = false;
            Panelrdocancel.Visible = false;
            ImageButton3.Visible = false;
            btnBulkReport.Visible = true;
            btnDCRreport.Visible = false;
            btnSummaryReport.Visible = false;
        }
    }
    #endregion


    #region// THIS SECTION IS USED TO GENERATE REPORT ON PDF BUTTON CLICK
    protected void imgbutExporttoPdf_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
            return;
        }
        else if (ddlCashbook.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select CashBook Type", this.Page);
            return;
        }
        //RFC.bug.Minor.1 (04-09-2023) Added By Jay Takalkhede On dated 04092023 (TktNo.47732)
        else if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
        {
            objCommon.DisplayMessage(this.updpnlMain, "To Date should be greater than From Date", this.Page);
            return;
        }
        else if (rdbReports.SelectedValue == "1")
        {
            if (rdocancel.SelectedValue == "1")
            {
                ShowReport("pdf", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
            }
            else if (rdocancel.SelectedValue == "2")
            {
                ShowReport("pdf", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
            }
            else
            {
                ShowReport("pdf", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
            }
            //ShowReport("pdf", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
        }
        else if (rdbReports.SelectedValue == "2")
        {
            ShowReport("pdf", "Miscellanious Summary Report", "rptMiscSummary.rpt");
        }
        else if (rdbReports.SelectedValue == "3")
        {
            if (ddlHead.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updpnlMain, "Please Select Head", this.Page);
                return;
            }
            ShowReport("pdf", "Miscellanious HeadWise Report", "rptMiscHeadwise.rpt");
        }
        else
        {
            ShowReport("pdf", "Miscellanious BankWise Report", "rptMiscBankWiseCollection.rpt");
        }


    }
    private void ShowReport(string exporttype, string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            string recoon = string.Empty;
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + exporttype;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            if (rdbReports.SelectedValue == "1" || rdbReports.SelectedValue == "2")
            {
                if (rdocancel.SelectedValue == "")
                {
                    recoon = "0";
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd") + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_CBOOKSRNO=" + ddlCashbook.SelectedValue + ",@P_PAYTYPE=" + ddlPaytype.SelectedValue + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_RECCAN=" + recoon + "";

                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd") + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_CBOOKSRNO=" + ddlCashbook.SelectedValue + ",@P_PAYTYPE=" + ddlPaytype.SelectedValue + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_RECCAN=" + rdocancel.SelectedValue + "";
                }
            }
            else if (rdbReports.SelectedValue == "4")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd") + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_CBOOKSRNO=" + ddlCashbook.SelectedValue + ",@P_PAYTYPE=" + ddlPaytype.SelectedValue + ",@P_VERSION=" + 1 + ",@P_USERNAME=" + Session["userfullname"].ToString() + "";
            }
            else if (rdbReports.SelectedValue == "6")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd") + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_CBOOKSRNO=" + ddlCashbook.SelectedValue + ",@P_PAYTYPE=" + ddlPaytype.SelectedValue + ",USERNAME=" + Session["userfullname"].ToString() + "";
            }
            else if (rdbReports.SelectedValue == "3")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd") + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_CBOOKSRNO=" + ddlCashbook.SelectedValue + ",@P_PAYTYPE=" + ddlPaytype.SelectedValue + ",@P_HEAD=" + ddlHead.SelectedValue + ",@P_USERNAME=" + Session["userfullname"].ToString() + "";
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport1(string reportTitle, string rptFileName)
    {
        int miscdcrno = Convert.ToInt32(txtCounter.Text);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + miscdcrno + ",@P_COPY=2,@P_USERNAME=" + Session["userfullname"].ToString() + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport_rcpit(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + ViewState["OUTPUT"] + " ";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport_rcpit() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport2(string reportTitle, string rptFileName)
    {
        int miscdcrno = Convert.ToInt32(txtCounter.Text);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + miscdcrno + ",@P_COPY=3,@P_USERNAME=" + Session["userfullname"].ToString() + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport_Summary(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd") + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy/MM/dd") + ",@P_CBOOKSRNO=" + ddlCashbook.SelectedValue + ",@P_PAYTYPE=" + ddlPaytype.SelectedValue + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport_rcpit() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void binddata(DataRow dr)
    {
        txtCounter.Text = dr["RECNO"].ToString();
    }
    #endregion

    #region  Commented
    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    pnldate.Visible = false;
    //    //THIS DATA SET IS USED TO BIND LISTVIEW 
    //    DataSet ds = objCommon.FillDropDown("MISCDCR", "MISCDCRSRNO", "NAME,AUDITDATE,PAY_TYPE,CHDDAMT,(CASE WHEN RECCAN= 1 THEN 'CANCELLED' ELSE '-'END)STATUS,COUNTR,RECNO", "(PAY_TYPE='" + ddlPaytype.SelectedValue + "'OR '" + ddlPaytype.SelectedValue + "'= '')AND CBOOKSRNO=" + ddlCashbook.SelectedValue + " AND RECNO LIKE '%" + txtCounter.Text + "%'", "MISCDCRSRNO DESC");
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        lvPaidReceipts.DataSource = ds;
    //        lvPaidReceipts.DataBind();
    //        lvPaidReceipts.Visible = true;
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.updpnlMain, "Receipt No is not Found", this.Page);
    //    }



    //    {
    //        if (ddlCashbook.SelectedIndex == 0)
    //        {
    //            objCommon.DisplayMessage(this.updpnlMain, "Please Select Cash Book", this.Page);
    //            pnldate.Visible = false;
    //            lvPaidReceipts.Visible = false;
    //        }
    //        else
    //        {

    //        }
    //    }
    //}
    #endregion  Commented

    #region Cancel
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCashbook.SelectedIndex = 0;
        txtCounter.Text = string.Empty;
        lvPaidReceipts.Visible = false;
        ddlPaytype.SelectedIndex = 0;
        pnldate.Visible = false;
        txtSearchPanel.Text = string.Empty;
        ddlPaytype.SelectedValue = "R";
        usnno.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string check = string.Empty;
        string countno = string.Empty;
        string counterno = string.Empty;
        counterno = objCommon.LookUp("MISCDCR", "RECNO", "MISCDCRSRNO ='" + txtCounter.Text + "'");
        FeeCollectionController fcc = new FeeCollectionController();
        {
            if (hdnCount.Value == "0")
            {
                objCommon.DisplayMessage(this.updpnlMain, "Please select receipt", this.Page);
                pnldate.Visible = false;
                return;

            }
            else
            {
                countno = Convert.ToString(objCommon.LookUp("MISCDCR", "ISNULL(RECCAN,0)", "MISCDCRSRNO ='" + txtCounter.Text.Trim() + "'"));
                if (countno == "False")
                {
                    fcc.updateMiscdcr(Convert.ToInt32(txtCounter.Text));
                    objCommon.DisplayMessage(this.updpnlMain, "Receipt Cancelled successfully", this.Page);
                    pnldate.Visible = false;
                    lvPaidReceipts.Visible = false;
                    lblCounter.Visible = true;
                    ShowReport2("Miscellaneous cancelled Report", "rptMiscReport.rpt");
                    //clear();
                    return;
                }
                else if (countno == "True")
                {
                    objCommon.DisplayMessage(this.updpnlMain, "Receipt is already cancelled ", this.Page);
                    lvPaidReceipts.Visible = false;
                    pnldate.Visible = false;
                    txtCounter.Text = string.Empty;
                    usnno.Visible = false;
                    ddlCashbook.SelectedValue = "0";
                    return;
                }
            }
        }
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void clear()
    {
        pnldate.Visible = false;
        ddlCashbook.SelectedIndex = 0;
        ddlHead.Visible = false;
        ddlPaytype.Enabled = false;
        txtCounter.Text = string.Empty;
        txtFromDate.Enabled = false;
        txtTodate.Enabled = false;
        lblCounter.Visible = false;
        lblHead.Visible = false;
        pnlhead.Visible = false;
        // divpanel.Visible = false;
        ddlSearchPanel.SelectedIndex = 0;
        txtSearchPanel.Text = string.Empty;
    }
    private void clearfield()
    {
        lblCounter.Visible = false;
        //  btnShow.Visible = false;
        btnCancel.Visible = false;
        btnReprint.Visible = false;
        txtFromDate.Visible = true;
        txtTodate.Visible = true;
        ddlPaytype.Visible = true;
        pnlbutton.Visible = true;
        pnlReport.Visible = true;
        lvPaidReceipts.Visible = false;
        ddlPaytype.SelectedValue = "R";
        // usnno.Visible = false;
    }
    protected void ClearSelection()
    {
        txtSearchPanel.Text = "";
        ddlDropdown.SelectedIndex = -1;
    }

    #endregion Cancel

    #region Report
    protected void btnReprint_Click(object sender, EventArgs e)
    {
        string receiptNo = objCommon.LookUp("MISCDCR", "RECNO", "MISCDCRSRNO = '" + txtCounter.Text + "'");
        string MISCDCRSRNO = txtCounter.Text;
        ViewState["OUTPUT"] = MISCDCRSRNO;
        if (receiptNo == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Valid Receipt Number", this.Page);
            pnldate.Visible = false;
        }
        else
        {
            // hdnCount.Value = receiptNo;
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {

                //ShowReport_rcpit("Miscellanious Fees", "rptMiscReport_RCPIT.rpt");
                string STATUS = objCommon.LookUp("MISCDCR", "STUDSTATUS", "MISCDCRSRNO=" + ViewState["OUTPUT"] + "");
                if (STATUS == "1")
                {
                    ShowReport_rcpit("Miscellanious Fees", "rptMiscReport_EXTERNAL_RCPIT.rpt");
                    txtCounter.Text = string.Empty;
                }
                else
                {
                    ShowReport_rcpit("Miscellanious Fees", "rptMiscReport_RCPIT.rpt");
                    txtCounter.Text = string.Empty;
                }
            }
            else
            {
                this.ShowReport1("Miscellaneous Reprint Report", "rptMiscReport_duplicate.rpt");
            }
            pnldate.Visible = false;
        }
    }
    protected void btnBulkReport_Click1(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
            return;
        }
        else if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
        {   //RFC.bug.Minor.1 (04-09-2023) Added By Jay Takalkhede On dated 04092023 (TktNo.47732)
            objCommon.DisplayMessage(this.updpnlMain, "To Date should be greater than From Date", this.Page);
            return;
        }
        else
        {
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
                ShowReport("pdf", "Bulk Miscellaneous Fees Report", "BulkMiscFeesReceiptRcpit.rpt");
            }
            else
            {
            }

        }

    }
    protected void btnSummaryReport_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
            return;
        }
        else if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
        {   //RFC.bug.Minor.1 (04-09-2023) Added By Jay Takalkhede On dated 04092023 (TktNo.47732)
            objCommon.DisplayMessage(this.updpnlMain, "To Date should be greater than From Date", this.Page);
            return;
        }
        else
        {
            ShowReport_Summary("Fee Collection Summary Report", "MiscFeeCollectionSummeryReport.rpt");
        }
    }
    protected void btnDCRreport_Click(object sender, EventArgs e)
    {

        DataSet ds = null;
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
            return;
        }
        else if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
        {   //RFC.bug.Minor.1 (04-09-2023) Added By Jay Takalkhede On dated 04092023 (TktNo.47732)
            objCommon.DisplayMessage(this.updpnlMain, "To Date should be greater than From Date", this.Page);
            return;
        }
        else
        {
            string RECCON = string.Empty;
            if (rdocancel.SelectedValue == "")
            {
                RECCON = string.Empty;
            }
            else if (rdocancel.SelectedValue == "1")
            {
                RECCON = "1";
            }
            else if (rdocancel.SelectedValue == "2")
            {
                RECCON = "0";
            }
            if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                ds = objSC.RetrieveMiscFeesDataForExcel(Convert.ToInt32(ddlCashbook.SelectedValue), ddlPaytype.SelectedValue, Convert.ToDateTime(txtTodate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString()), RECCON);
            }
            else
            {

                ds = objSC.RetrieveMiscFeesDataForExcelRcpit(Convert.ToInt32(ddlCashbook.SelectedValue), ddlPaytype.SelectedValue, Convert.ToDateTime(txtTodate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString()), RECCON);
            }

        }
        if (ds.Tables.Count > 0)
        {
            if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {

                    ds.Tables[0].TableName = "For Internal Student";
                    ds.Tables[1].TableName = "For External Student";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                        {
                            //Add System.Data.DataTable as Worksheet.
                            if (dt != null && dt.Rows.Count > 0)
                                wb.Worksheets.Add(dt);
                        }

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Misc_Fees_DCR_Short_report.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            else
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataGrid dg = new DataGrid();

                    string attachment = "attachment; filename=Misc_Fees_DCR_Short_report.xls";

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/" + "ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    dg.DataSource = ds.Tables[0];
                    dg.DataBind();
                    dg.HeaderStyle.Font.Bold = true;
                    dg.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
            }

        }
        else
        {
            objCommon.DisplayMessage(this.updpnlMain, "Record Not Found", this.Page);

        }

    }
    private void excel()
    {

    }
    protected void btnExcelRcpit_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
            objCommon.DisplayMessage(this.updpnlMain, "Please Select Proper Date", this.Page);
        }
        else
        {
            ShowReport("xls", "Miscellanious Collection Fees", "rptMiscCollection.rpt");
        }
    }
    #endregion Report

    #region Bind 
    protected void btnSearchPanel_Click(object sender, EventArgs e)
    {
        if (ddlCashbook.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updpnlMain, "Please Enter Cash Book ", this.Page);
        }
        else
        {
            if (ddlSearchPanel.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updpnlMain, "Please Select Search Criteria ", this.Page);
            }
            else
            {
                if (txtSearchPanel.Text == "")
                {
                    objCommon.DisplayMessage(updpnlMain, "Please Enter Search String ", this.Page);
                }
                else
                {
                    string value = string.Empty;
                    if (ddlDropdown.SelectedIndex > 0)
                    {
                        value = ddlDropdown.SelectedValue;
                    }
                    else
                    {
                        value = txtSearchPanel.Text;
                    }
                    bindlist(ddlSearchPanel.SelectedItem.Text, value);
                }
            }
        }
    }
    private void bindlist(string category, string searchtext)
    {
        DataSet ds = objSC.RetrieveStudentDetailsRecieptCansel(Convert.ToInt32(ddlCashbook.SelectedValue), ddlPaytype.SelectedValue, searchtext, category);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvPaidReceipts.Visible = true;
            lvPaidReceipts.Visible = true;
            lvPaidReceipts.DataSource = ds;
            lvPaidReceipts.DataBind();
            // lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaidReceipts);//Set label - 
        }
        else
        {
            ddlSearchPanel.ClearSelection();
            objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
            lvPaidReceipts.Visible = false;
            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
        }
    }
    #endregion Bind

    #region DDL
    protected void ddlCashbook_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlHead, "MISCHEAD_MASTER", "MISCHEADSRNO", "MISCHEAD", "CBOOKSRNO=" + ddlCashbook.SelectedValue + "", "MISCHEAD");
        if (rdbReports.SelectedValue == "5")
        {
            //ddlSearchPanel.SelectedIndex = 0;
            // txtSearchPanel.Text = string.Empty;
            // pnltextbox.Visible = false;
            // divtxt.Visible = false;
            if (ddlCashbook.SelectedIndex != 0)
            {
                usnno.Visible = true;
            }
            else
            {
                usnno.Visible = false;
            }
        }
    }
    protected void ddlSearchPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSearchPanel.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearchPanel.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearchPanel.Visible = false;
                        pnlDropdown.Visible = true;
                        divpanel.Attributes.Add("style", "display:block");
                        divDropDown.Attributes.Add("style", "display:block");
                        divtxt.Attributes.Add("style", "display:none");
                        //divtxt.Visible = false;
                        lblDropdown.Text = ddlSearchPanel.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearchPanel.Visible = true;
                        divDropDown.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:block");
                        divpanel.Attributes.Add("style", "display:block");
                    }
                }
            }
            else
            {
                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
                divpanel.Attributes.Add("style", "display:none");
            }
            ClearSelection();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlPaytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCounter.Text = string.Empty;
        if (rdbReports.SelectedValue == "5")
        {
            pnldate.Visible = false;
        }
        else
            pnldate.Visible = true;
        lvPaidReceipts.DataSource = null;
    }
    protected void rbDate_CheckedChanged(object sender, EventArgs e)
    {
        pnldate.Visible = true;
        pnlpaytype.Visible = false;
        ImageButton3.Visible = true;
    }
    protected void rbpayment_CheckedChanged(object sender, EventArgs e)
    {
        pnldate.Visible = false;
        pnlpaytype.Visible = true;
        ImageButton3.Visible = false;
    }
    protected void rbreceiptnumber_CheckedChanged(object sender, EventArgs e)
    {
        pnldate.Visible = false;
        pnlpaytype.Visible = false;
        ImageButton3.Visible = false;
    }
    #endregion DDL

}


