//========================================
// CREATED BY    : MRUNAL SINGH
// CREATION DATE : 11-10-2019
// DESCRIPTION   : TO GET THE GRIEVANCE SUMMARY.
//========================================
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
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class GrievanceRedressal_Report_GrievanceSummary : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();


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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
                //txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                objCommon.FillDropDownList(ddlGrivType, "GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "", "GRIV_ID");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNO");
            }
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=dailyworkout.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=dailyworkout.aspx");
        }
    }


    protected void btnShowReport_Click(object sender, EventArgs e)
    {        
        try
        {
            DataSet ds = null;
            objGrivE.DEPARTMENT_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
            objGrivE.GRIV_ID = Convert.ToInt32(ddlGrivType.SelectedValue);

            string FromDate = string.Empty;
            string ToDate = string.Empty;
           

            if (txtSDate.Text != string.Empty)
            {
               // objGrivE.FROM_DATE = Convert.ToDateTime(txtSDate.Text).ToString("yyyy-MM-dd");
                FromDate = Convert.ToDateTime(txtSDate.Text).ToString("yyyy-MM-dd");
            }            

            if (txtEndDate.Text != string.Empty)
            {
                //objGrivE.TO_DATE = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd");
                ToDate = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd");
            }


            ds = objGrivC.GetGrievanceSummaryList(objGrivE, FromDate, ToDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGrApplication.DataSource = ds;
                lvGrApplication.DataBind();
                lvGrApplication.Visible = true;
            }
            else
            {

                lvGrApplication.DataSource = null;
                lvGrApplication.DataBind();
                lvGrApplication.Visible = false;    
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["GAID"] = int.Parse(btnPrint.CommandName);
           ShowReport("GrievanceSummaryReport", "rptGrievanceSummary.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("GrievanceRedressal")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,GrievanceRedressal," + rptFileName;
            url += "&param=@P_GAID=" + Convert.ToInt32(ViewState["GAID"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() ;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSDate.Text = Common.reportStartDate.ToString("dd-MMM-yyyy");
        txtEndDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        
    }

}