//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : HOSTEL FEE COLLECTION REPORT                     
// CREATION DATE : 18 MARCH 2013                                                     
// CREATED BY    : YAKIN UTANE                                       
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class HOSTEL_REPORT_HostelFeeCollectionReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MessBillController objMbc = new MessBillController();
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
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
                    //CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFeeCollectionReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelFineReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelFeeCollectionReport.aspx");
        }
    }
    #endregion
    protected void PopulateDropDownList()
    {
        try
        {
            //FILL DROPDOWN HOSTEL SESSION NO.
            objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO desc");
            ddlHostelSessionNo.SelectedIndex = 1;
            //objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0", "HOSTEL_SESSION_NO");
            if (Session["usertype"].ToString() == "1")
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
            else
                objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFeeCollectionReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        //DateTime fromdate = Convert.ToDateTime(txtFromdate.Text);
        //DateTime todate = Convert.ToDateTime(txtTodate.Text);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO= " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_FROMDATE=" + txtFromdate.Text + ",@P_TODATE=" + txtTodate.Text + ",@P_PAY_TYPE=" + ddlpaymenttype.SelectedValue + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO= " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromdate.Text) + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text) + ",@P_PAY_TYPE=" + ddlpaymenttype.SelectedValue + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO= " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_FROMDATE=" + fromdate.ToString("yyyy/MM/dd") + ",@P_TODATE=" + todate.ToString("yyyy/MM/dd") + ",@P_PAY_TYPE=" + ddlpaymenttype.SelectedValue + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
           
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelMessCaution.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnHostelwiseFee_Click(object sender, EventArgs e)
    {
        ShowReport("Hostel wise fee collection report","rptFeecollectionhostelwise.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnDatewiseFee_Click(object sender, EventArgs e)
    {
        ShowReport("Date wise fee collection report", "rptHostelFeedatewise.rpt");
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (txtFromdate.Text != string.Empty)
        {

            if (txtTodate.Text != string.Empty && txtFromdate.Text != string.Empty)
                {
                    if (Convert.ToDateTime(txtTodate.Text) > Convert.ToDateTime(txtFromdate.Text))
                    {
                    }
                    else
                    {
                        objCommon.DisplayMessage("From Date should be less than To Date.", this.Page);
                        txtFromdate.Text = string.Empty;
                        txtFromdate.Focus();
                        return;
                    }
                }           
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        if (txtTodate.Text != string.Empty)
        {

            if (txtTodate.Text != string.Empty && txtFromdate.Text != string.Empty)
                {
                    if (Convert.ToDateTime(txtTodate.Text) > Convert.ToDateTime(txtFromdate.Text))
                    {
                    }
                    else
                    {
                        objCommon.DisplayMessage("To Date should be greater than From Date.", this.Page);
                        txtFromdate.Text = string.Empty;
                        txtFromdate.Focus();
                        return;
                    }
                }
           
        }
    }
    protected void btnDateWiseExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=" + "DateWiseFeeCollectionReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSIONNO", Convert.ToInt32(ddlHostelSessionNo.SelectedValue)),
               new SqlParameter("@P_HOSTELNO", Convert.ToInt32(ddlHostelNo.SelectedValue)),
               new SqlParameter("@P_FROMDATE", txtFromdate.Text),
               new SqlParameter("@P_TODATE",txtTodate.Text),
               new SqlParameter("@P_PAY_TYPE",ddlpaymenttype.SelectedValue),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_MESS_FEE_DATEWISE_REPORT_EXCEL",objParams);

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
            dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnHostelWiseExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=" + "HostelWiseFeeCollectionReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSIONNO", Convert.ToInt32(ddlHostelSessionNo.SelectedValue)),
               new SqlParameter("@P_HOSTELNO", Convert.ToInt32(ddlHostelNo.SelectedValue)),
               new SqlParameter("@P_FROMDATE", txtFromdate.Text),
               new SqlParameter("@P_TODATE",txtTodate.Text),
               new SqlParameter("@P_PAY_TYPE",ddlpaymenttype.SelectedValue),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_MESS_FEE_HOSTELWISE_REPORT_EXCEL",objParams);

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
            dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
}
