using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOSTEL_GATEPASS_HostelGatepassReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                objCommon.FillDropDownList(ddlPurpose, "ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME", "ISACTIVE=1", "PURPOSE_NO");
                //MoreDetails();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelGatepassReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {


            string Applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? "01/01/1999" : Convert.ToString(txtApplyDate.Text);

            //string dateFrom =Convert.ToDateTime(txtFromDate.Text.ToString()).ToShortDateString();

            int Purpose = Convert.ToInt32(ddlPurpose.SelectedValue);
            string Gatepassno = string.IsNullOrEmpty(txtGatePassCode.Text) ? "0" : txtGatePassCode.Text;
            string Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? "0" : ddlStatus.SelectedValue;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;  // ",@P_APPLYDATE=" + Applydate + ",@P_PURPOSE=" + Purpose +
            url += "&param=@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text + ",@P_APPLYDATE=" + Applydate + ",@P_PURPOSE=" + Purpose + ",@P_STATUS=" + Status + ",@P_GATEPASSCODE=" + Gatepassno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_REPORT_ApplyStudDatewiseReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=HostelGatepassReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelGatepassReport.aspx");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Gate Pass Report", "HostelGatepassDetailsReport.rpt");
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            //@P_APPLYDATE=" + Applydate + ",@P_PURPOSE=" + Purpose + ",@P_STATUS=" + Status + ",@P_GATEPASSCODE=" + Gatepassno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            string Applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? "01/01/1999" : Convert.ToString(txtApplyDate.Text);
            int Purpose = Convert.ToInt32(ddlPurpose.SelectedValue);
            string Gatepassno = string.IsNullOrEmpty(txtGatePassCode.Text) ? "0" : txtGatePassCode.Text;
            string Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? "0" : ddlStatus.SelectedValue;
            string getdate = DateTime.Now.ToString("dd/MMM/yyyy_hh:mm:ss");
            string res = string.Concat(getdate, "GatePassDetailsReport.xls");
            string attachment = "attachment; filename=" + res;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_FROMDATE", txtFromDate.Text),
               new SqlParameter("@P_TODATE", txtToDate.Text),
               new SqlParameter("@P_APPLYDATE",Applydate ),
               new SqlParameter("@P_PURPOSE",Purpose),
               new SqlParameter("@P_STATUS",Status),
               new SqlParameter("@P_GATEPASSCODE",Gatepassno),
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_REPORT_EXCEL", objParams);

            DataTable dt = dsfee.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {

            }
            DataGrid dg = new DataGrid();

            if (dsfee.Tables.Count > 0)
            {
                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();

            }
            dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            dg.HeaderStyle.BackColor = System.Drawing.Color.Yellow;
            dg.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}