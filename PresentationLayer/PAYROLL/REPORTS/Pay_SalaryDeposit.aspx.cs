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
using System.Data.SqlClient;

public partial class PAYROLL_REPORTS_Pay_SalaryDeposit : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    
    //ConnectionStrings
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

                //Populate DropdownList
                PopulateDropDownList();

                //Fill Listbox
               
                //Select list particular Head
               

                //Get focus on Textbox
                txtFromDate.Focus();

                //Staff No. DropdownList Enabled False
               // ddlStaffNo.Enabled = false;
                ddlEmployeeNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    DateTime dt1 = Convert.ToDateTime(txtFromDate.Text.Trim());
        //    DateTime dt2 = Convert.ToDateTime(txtToDate.Text.Trim());
        //    TimeSpan ts = dt1.Subtract(dt2);
        //    int diffMonth = Math.Abs((dt2.Year - dt1.Year) * 12 + dt1.Month - dt2.Month);

        //    if (diffMonth > 12)
        //    {
        //        ShowMessage("Date difference not more than 12 month.");
        //    }
            

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.txtToDate_TextChanged() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
    }
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoParticularEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select Employee.");
                return;
                ddlStaffNo.Enabled = false;
            }
            if (rdoAllEmployee.Checked && ddlStaffNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select Staff.");
                return;
                ddlEmployeeNo.Enabled = false;
            }
            //ShowReport Method to Display Report
            // ShowReportColumnPayHead("Particular_Column_Payhead", "rptSelected_Field1.rpt");

            ShowReport(" ", "Report_For_SalaryDeposit", "rptSalaryDeposit.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SalaryDeposit.btnShowReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
        // clear();
    }
    protected void PopulateDropDownList()
    {
        try
        {
            //FILL COLLEGE
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            
            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
           
            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO>0", "IDNO");

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?Pay_SalaryDeposit.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SalaryDeposit.aspx");
        }
    }
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
