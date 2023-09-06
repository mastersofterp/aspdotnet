//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : 
// CREATION DATE : 01.08.2021
// CREATED BY    : TANU BALGOTE
// MODIFIED DESC : 
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

public partial class STORES_Reports_Str_ConsumbleItem_Report : System.Web.UI.Page
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
            if (rblAssestType.SelectedValue == "1")
            {
                ShowReport("Report", "Str_ConsumbleItem_Report.rpt");
            }
            //if (rblAssestType.SelectedValue == "2")
            //{
            //    ShowReport("Report", "Str_ConsumbleItemExhausting_Report.rpt");
            //}
            if (rblAssestType.SelectedValue == "3")
            {
                ShowReport1("Report", "Str_Non_ConsumbleItem_Report.rpt");
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
        
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=";
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

    //-------------------------------------16/06/2023 Generate non consumable report added---Start-
    private void ShowReport1(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=";
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
    //--------------------------end------------------------------------------

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rblAssestType.SelectedValue = "1";
    }
}