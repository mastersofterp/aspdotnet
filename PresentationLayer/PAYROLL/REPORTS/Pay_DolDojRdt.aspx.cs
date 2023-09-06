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

public partial class PAYROLL_REPORTS_Pay_DolDojRdt : System.Web.UI.Page
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
            int ua_type = Convert.ToInt32(Session["usertype"]);
            int IDNO = Convert.ToInt32(Session["idno"]);
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

                //Populate DropdownList
                //Populate DropdownList
                //if (ua_type != 1)
                //{
                //    PopulateDropDownListEmployee();
                //}
                //else
                //{
                    PopulateDropDownList();
                //}
                txtFromDate.Focus();
                //ddlStaffNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
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

    //populate drop down for particular employee
    protected void PopulateDropDownListEmployee()
    {
        try
        {
            int IDNO = Convert.ToInt32(Session["idno"]);
            //FILL STAFF
            //objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            //trrdoemployee.Visible = false;
            trStaff.Visible = false;
            //trorder.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //populate drop down for particular employee end


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_DolDojRdt.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_DolDojRdt.aspx");
        }
    }

    private void ShowReportAnnualSummary(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd")));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd")));
            Tdate = Tdate.Substring(0, 10);

            string orderby = string.Empty;

            //if (ddlOrderBy.SelectedValue == "1")
            //{
            //    orderby = "IDNO";
            //}
            //else if (ddlOrderBy.SelectedValue == "2")
            //{
            //    orderby = "NAME";
            //}
            //else if (ddlOrderBy.SelectedValue == "3")
            //{
            //    orderby = "SEQ_NO";
            //}
            //else if (ddlOrderBy.SelectedValue == "4")
            //{
            //    orderby = "PFILENO";
            //}

            int IDNO = Convert.ToInt32(Session["idno"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;

            url += "&path=~,Reports,Payroll," + rptFileName;
            
            //url += "&param=@P_FDATE=" + Fdate + "&@P_TODATE=" + Tdate + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_SELECT=" + ddlReport.SelectedValue;
            //url += "&param=@P_SELECT=" + ddlReport.SelectedValue + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_FDATE=" + Convert.ToDateTime(txtFromDate.Text) + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text) + ",@P_COLLEGE_CODE=" + 17; 
            url += "&param=@P_SELECT=" + ddlReport.SelectedValue + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_FDATE=" + Fdate + ",@P_TODATE=" + Tdate + ",@P_COLLEGE_CODE=" + 24; 
           
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
               ShowReportAnnualSummary("Annual_Summary_Report", "Pay_DojDolRdt.rpt");
         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //    protected void rdoParticularEmployee_CheckedChanged(object sender, EventArgs e)
    //    {
    //        try
    //        {
    //            ddlStaffNo.SelectedIndex = 0;
    //            ddlStaffNo.Enabled = false;
    //            ddlEmployeeNo.Enabled = true;
    //        }
    //        catch (Exception ex)
    //        {
    //           if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.rdoParticularEmployee_CheckedChanged()-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //    protected void rdoAllEmployee_CheckedChanged(object sender, EventArgs e)
    //    {
    //        try
    //        {
    //            ddlStaffNo.Enabled = true;
    //            ddlEmployeeNo.SelectedIndex = 0;
    //            ddlEmployeeNo.Enabled = false;
    //        }
    //        catch (Exception ex)
    //        {
    //         if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.rdoAllEmployee_CheckedChanged()-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    protected void rdoAllEmployee_CheckedChanged(object sender, EventArgs e)
    {
        //trorder.Visible = true;
    }

    private void ShowReportEmployeeAbstractSalary(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            int IDNO = Convert.ToInt32(Session["idno"]);
     
            url += "&param=@Username=" + Session["username"].ToString() + ",@P_FROMDATE=" + (txtFromDate.Text) + ",@P_TODATE=" + (txtToDate.Text) + ",@P_STAFFNO=" + 0 + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PageWiseTotal=1";
         
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected bool ChkDateDiff()
    {
        bool outofrange = true;
        DateTime dt1 = Convert.ToDateTime(txtFromDate.Text.Trim());
        DateTime dt2 = Convert.ToDateTime(txtToDate.Text.Trim());
        int diffMonth = Math.Abs((dt2.Year - dt1.Year) * 12);
        if (diffMonth > 0)
        {
            diffMonth += dt2.Month - dt1.Month + 1;
        }
        if (diffMonth > 12)
        {
            outofrange = true;
        }
        else
        {
            outofrange = false;
        }
        return outofrange;
    }
}
