
//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : Fixed Asset Report
// CREATION DATE : 04.08.2021
// CREATED BY    : TANU BALGOTE
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY Fixed Asset Report BY DATE-WISE
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


public partial class STORES_Reports_Fixed_Asset_Report : System.Web.UI.Page
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
                //if (rblAssestType.SelectedValue == "1")
                //{
                //    divFromDate.Visible = true;
                //    divToDate.Visible = true;
                //    divToFields.Visible = false;
                //}

                ObjComman.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO = 1", "MIGNAME");
                ddlCategory.SelectedValue = "1";
                ObjComman.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");

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

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjComman.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=" + ddlCategory.SelectedValue, "MISGNAME");
    }


    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjComman.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
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
            if (rblAssestType.SelectedValue == "1")
            {
                ShowReport("Report", "Str_Stock_Report.rpt");
            }
            if (rblAssestType.SelectedValue == "2")
            {
                ShowReport("Report", "AssestReport.rpt");
            }
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

        string fromDate = string.Empty;
        string toDate = string.Empty;
        if (txtFromDate.Text != "")
            fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MMM-dd");

        if (txtToDate.Text != "")
            toDate = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MMM-dd");


        try
        {
            //if (rblAssestType.SelectedValue == "1")
            //{

                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

                url += "Reports/commonreport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,STORES," + rptFileName;
                url += "&param=@P_ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + "," + "@P_MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "," + "@P_FROM_DATE=" + fromDate + "," + "@P_TO_DATE=" + toDate;
                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //}
            //if (rblAssestType.SelectedValue == "2")
            //{
            //    string Script = string.Empty;
            //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            //    url += "Reports/commonreport.aspx?";
            //    url += "pagetitle=" + reportTitle;
            //    url += "&path=~,Reports,STORES," + rptFileName;
            //    url += "&param=@ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue);
            //    Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //}

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
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        rblAssestType.SelectedValue = "1";
    }

}