//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : Fixed Asset Report
// CREATION DATE : 04.08.2021
// CREATED BY    : Shabina Kausar
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY Item Sale Report BY DATE-WISE
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

public partial class STORES_Reports_Str_ItemSaleReport : System.Web.UI.Page
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

                ObjComman.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");
                //ObjComman.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO = 2", "MIGNAME");
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Str_ItemSaleReport.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
        //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlSubCategory.SelectedValue  = "0";
        ddlItem.SelectedValue = "0";


    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtFromDate.Text.ToString() != string.Empty && txtFromDate.Text.ToString() != "__/__/____" && txtToDate.Text.ToString() != string.Empty && txtToDate.Text.ToString() != "__/__/____")
            //{
            //    DateTime fromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
            //    DateTime toDate = Convert.ToDateTime(txtToDate.Text.ToString());
            //    if (toDate < fromDate)
            //    {
            //        MessageBox("To Date Should Be Greater Than Or Equals To From Date");
            //        return;
            //    }
            //}
            //if (rblAssestType.SelectedValue == "1")
            //{
            //    ShowReport("Report", "Str_Stock_Report.rpt");
            //}
            //if (rblAssestType.SelectedValue == "2")
            //{
            //    ShowReport("Report", "AssestReport.rpt");
            //}
            ShowReport("Report", "Str_ItemSale.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Str_ItemSaleReport.btnRpt_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {

        string fromDate = string.Empty;
        string toDate = string.Empty;
        if (txtFromDate.Text != "")
            fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy");

        if (txtToDate.Text != "")
            toDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");


        //string fromDate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
        //fromDate = fromDate.Substring(0, 10);
        //string toDate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text)));
        //toDate = toDate.Substring(0, 10);

        try
        {
            string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

                url += "Reports/commonreport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,STORES," + rptFileName;
               
                url += "&param=@P_SUBGROUPNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "," + "@P_ITEMNO=" + Convert.ToInt32(ddlItem.SelectedValue)+","+"@P_FROMDATE=" + fromDate + "," + "@P_TODATE=" + toDate;
                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
           
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Reports_Str_ItemSaleReport.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        ObjComman.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
    }
}