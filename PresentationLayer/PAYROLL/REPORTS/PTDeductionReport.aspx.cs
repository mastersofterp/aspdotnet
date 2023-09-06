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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;


public partial class PAYROLL_REPORTS_PTDeductionReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                FillDropdown();
                if (ua_type != 1)
                {
                   
                    //ddlEmployeeNo.SelectedIndex = 1;
                }              
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPayHead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void FillDropdown()
    {
        //FILL EMPLOYEE
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        objCommon.FillDropDownList(ddlPayHead, "payroll_PayHead", "PAYHEAD", "PAYSHORT", "Payhead like '%D%' and PAYSHORT is not null","");

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        

        if (!string.IsNullOrEmpty(txtFromDt.Text) && !string.IsNullOrEmpty(txtToDt.Text))
        {
            DateTime FromYear = Convert.ToDateTime(txtFromDt.Text);
            DateTime ToYear = Convert.ToDateTime(txtToDt.Text);

            int Years = ToYear.Year - FromYear.Year;
            int month = ToYear.Month - FromYear.Month;
            int TotalMonths = (Years * 12) + month;
            if (ToYear.Year >= FromYear.Year)
            {
                if (TotalMonths <= 12)
                {
                    //MessageBox("Check");

                    ShowReport("PTDeductionReport", "PTDeductionReports.rpt");
                }
                else
                {
                    MessageBox("Please Select Greater Than One Month Or Twelve Month");
                }
            }
            else
            {
                MessageBox("Please Select Proper Date");
            }


        }
        else
        {
            MessageBox("Please Select  Date");
        }
      
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
           
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;

            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_FROM_DATE=" + txtFromDt.Text+ ",@P_TO_DATE=" + txtToDt.Text+ ",@P_PT_HEAD=" +ddlPayHead.SelectedValue + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaff.SelectedValue);
       

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PTDeductionReports.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
}