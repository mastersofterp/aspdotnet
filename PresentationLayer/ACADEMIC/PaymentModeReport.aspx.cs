//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PAYMENT MODE REPORT
// CREATION DATE : 12-AUG-2019
// CREATED BY    : RITA MUNDE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Text;
using System.Data;

public partial class ACADEMIC_PaymentModeReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    DailyFeeCollectionController objdfc = new DailyFeeCollectionController();

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
        try
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_PaymentModeReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    public void PopulateDropDownList()
    {
        this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME DESC");
        this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
        this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO NOT IN (9,10)", "RCPTTYPENO");
    }
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {      
        //try
        //{
        //    string reportTitle = string.Empty;
        //    string rptFileName = string.Empty;
        //    this.SetReportFileAndTitle(ref reportTitle, ref rptFileName);

        //    DailyFeeCollectionRpt dcrReport = GetReportCriteria();
        //    this.ShowReport(dcrReport, reportTitle, rptFileName);
        //}
        //catch (Exception ex)
        //{

        //}
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        DailyFeeCollectionRpt dcrReport = GetReportCriteria();
        DataSet ds = objdfc.GetPaymodeReport(dcrReport);
        if (ds.Tables[0].Rows.Count > 0)
        {

            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment;filename=PaymentmodeReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
        }


    }
    private void ShowReport(DailyFeeCollectionRpt dcrRpt, string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
           // url += "&param=" + this.GetReportParameters(dcrRpt);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + "TF" + 
            //    ",@P_BRANCHNO=" + Convert.ToInt32("0") + 
            //    ",@P_DEGREENO=" + Convert.ToInt32("0") + 
            //    ",@P_SEMESTERNO=" + Convert.ToInt32("0") +
            //    ",@P_REC_FROM_DT=" + Convert.ToDateTime("06/08/2019 00:00:00") +
            //    ",@P_REC_TO_DATE=" + Convert.ToDateTime("13/08/2019 00:00:00") + 
            //    ",@P_PAYMODE=" + "1";

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue +
                ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) +
                ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                ",@P_REC_FROM_DT=" + Convert.ToDateTime(txtFromDate.Text) +
                ",@P_REC_TO_DATE=" + Convert.ToDateTime(txtToDate.Text) +
                ",@P_PAYMODE=" + ddlPaymentMode.SelectedValue; //+
               // ",@P_YERNO=" + Convert.ToInt32(ddlYear.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DCR_ReportUI.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void SetReportFileAndTitle(ref string reportTitle, ref string rptFileName)
    {
        reportTitle = "Payment_Mode_Report";
        rptFileName = "rptPaymetModeReport.rpt";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlReceiptType.SelectedIndex = 0;
        ddlPaymentMode.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtToDate.Text = "";

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "A.SHORTNAME");
    }

    private DailyFeeCollectionRpt GetReportCriteria()
    {
         DailyFeeCollectionRpt dcrReport = new DailyFeeCollectionRpt();
         try
         {
             dcrReport.FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
             dcrReport.ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;
             dcrReport.PaymentMode = (ddlPaymentMode.SelectedIndex > 0) ? ddlPaymentMode.SelectedValue : string.Empty;
             dcrReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
             dcrReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
             dcrReport.YearNo = (ddlYear.SelectedIndex > 0) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
             dcrReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
             dcrReport.ReceiptTypes = (ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedValue : string.Empty;
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUaimsCommon.ShowError(Page, "Academic_DCR_ReportUI.GetReportCriteria() --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUaimsCommon.ShowError(Page, "Server Unavailable.");
         }
         return dcrReport;
    }
    private string GetReportParameters(DailyFeeCollectionRpt dcrRpt)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            //param.Append("UserName=" + Session["userfullname"].ToString());         
            param.Append(",@P_RECIEPTCODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
            param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString() + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
            param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
            param.Append(",@P_PAYTYPENO=" + dcrRpt.PaymentMode.ToString() + "");

            param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString() + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
            param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
            param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
            param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
            param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
            param.Append(",@P_COLLEGE_CODE=" + Session["colcode"].ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Payment_Mode_ReportUI.GetReportParameters() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return param.ToString();
    }
}