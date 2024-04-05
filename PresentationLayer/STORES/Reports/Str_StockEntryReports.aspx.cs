
//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : STOCK_REPORT
// CREATION DATE : 01.08.2021
// CREATED BY    : TANU BALGOTE
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY STOCK  REPORT BY DATE-WISE
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================


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
using IITMS.UAIMS;
using IITMS;

public partial class STORES_Reports_Str_StockEntryReports : System.Web.UI.Page
{
    Common ObjComman = new Common();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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

                    }
                }


            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Payroll_LIC_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Payroll_LIC_Report.aspx");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReport.SelectedIndex == 0)
            {
                MessageBox("Please Select the Report Type.");
                return;
            }
            if (txtFromDate.Text.ToString() != string.Empty && txtFromDate.Text.ToString() != "__/__/____" && txtToDate.Text.ToString() != string.Empty && txtToDate.Text.ToString() != "__/__/____")
            {
                DateTime fromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
                DateTime toDate = Convert.ToDateTime(txtToDate.Text.ToString());
                if (toDate < fromDate)
                {
                    MessageBox("To Date Should Be Greater Than Or Equals To From Date");
                    return;
                }
            }
            ShowReport("Report", ddlReport.SelectedValue);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string fromDate = "";
        string toDate = "";
        if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            //fromDate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
            //fromDate = fromDate.Substring(0, 10);
            //toDate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text)));
            //toDate = toDate.Substring(0, 10);
            fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MMM-dd");
            toDate = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MMM-dd");
        }
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_FROM_DATE=" + fromDate + "," + "@P_TO_DATE=" + toDate;
            url += "&param=@P_FROM_DATE=" + fromDate + "," + "@P_TO_DATE=" + toDate;//+ ", @P_COLLEGE_CODE=" + Convert.ToInt32(Session["college_nos"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Stock_Reports.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;//Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
        txtToDate.Text = string.Empty;//DateTime.Now.ToString("dd/MM/yyyy");
        ddlReport.SelectedIndex = 0;
    }
}