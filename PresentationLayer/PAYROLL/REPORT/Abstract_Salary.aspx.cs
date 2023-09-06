//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL ABSTRACT SALARY                       
// CREATION DATE : 01-September-2009                                                          
// CREATED BY    : MANGESH BARMATE                                                  
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class PayRoll_Abstract_Salary : System.Web.UI.Page
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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Populate DropdownList
                PopulateDropDownList();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL MONTH YEAR 
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "SALNO", "MONYEAR", "SALNO>0", "SALNO");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Abstract_Salary.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Abstract_Salary.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }

        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportEmployeeAbstractSalary(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Payroll")));
            url += "Reports/CommonReport.aspx?";
            if (chkAbstarct.Checked)
            {
                url += "pagetitleForEmployeeCummulativeAbstractSalary=" + reportTitle;
                url += "&pathForEmployeeCummulativeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_IDNO=0";
                url += "&paramForEmployeeCummulativeAbstractSalary=username=" + Session["username"].ToString();
            }
            else
            {
                url += "pagetitleForEmployeeAbstractSalary=" + reportTitle;
                url += "&pathForEmployeeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_IDNO=0";
                url += "&paramForEmployeeAbstractSalary=username=" + Session["username"].ToString();
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRegisterWithAbstract_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkAbstarct.Checked)
            {
                ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Cummulative.rpt");
            }
            else
            {
                ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary.rpt");
            }
            
        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.btnRegisterWithAbstract_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
