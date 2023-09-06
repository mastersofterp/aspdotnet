//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : COMPLAINT ITEMS USED REPORT                                                 
// CREATION DATE : 08-02-2016                                                        
// CREATED BY    : MRUNAL SINGH                                                
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Complaints_REPORT_ComplaintItems : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Check browser and set pnlContainer width
                if (Request.Browser.Browser.ToLower().Equals("opera"))
                    pnlMain.Width = Unit.Percentage(100);
                else
                    pnlMain.Width = Unit.Percentage(90);

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
                //txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                objCommon.FillDropDownList(ddlRMDept,"COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", "", "DEPTID DESC");
                objCommon.FillDropDownList(ddlComplaint, "COMPLAINT_REGISTER", "COMPLAINTID", "COMPLAINT", "", "COMPLAINTID");
            }
            Session["reportdata"] = null;
        }

        if (Session["reportdata"] != null)
        {
            //crViewer.ReportSource = Session["reportdata"] as ReportDocument;
            //crViewer.DataBind();
        }
        divMsg.InnerHtml = string.Empty;

        //objCommon.ReportPopUp(btnSubmit, "pagetitle=PRM(Daily Workout Report)&path=~" + "," + "Reports" + "," + "REPAIR AND MAINTENANCE" + "," + "rptdaily_workout.rpt&param=@P_USERID=" + Session["userno"].ToString() + "," + "@P_SDATE=" + Convert.ToDateTime(txtSDate.Text) + "," + "@P_EDATE=" + Convert.ToDateTime(txtEndDate.Text) + "," + "@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString(), "PRM");
    }
    private void FillDepartment()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", string.Empty, "DEPTID DESC");
            //DataSet ds = objCommon.GetDropDownData("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_DEPARTMENT");
            ddlRMDept.DataSource = ds;
            ddlRMDept.DataValueField = ds.Tables[0].Columns["DeptId"].ToString();
            ddlRMDept.DataTextField = ds.Tables[0].Columns["Deptname"].ToString();
            ddlRMDept.DataBind();
            ddlRMDept.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_R_CreateUser.FillEntryFor-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ShowReport("Daily Workout Report", "ComplaintItems.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_DEPTID=" + ddlRMDept.SelectedValue + "," + "@P_COMPLAINTID=" + ddlComplaint.SelectedValue + "," + "@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        //Set Report to Null
        Session["reportdata"] = null;
        //crViewer.ReportSource = null;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void ddlRMDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRMDept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlComplaint, "COMPLAINT_REGISTER CR INNER JOIN CELL_COMPLAINT_ALLOTMENT CCA ON (CR.COMPLAINTID = CCA.COMPLAINTID)", "CR.COMPLAINTID", "CR.COMPLAINT", "CCA.DEPTID=" + ddlRMDept.SelectedValue ,"CR.COMPLAINTID");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}