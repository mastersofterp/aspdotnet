//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FINAL REPORT FARMAT FOR SALARY PURPOSES                       
// CREATION DATE : 26-September-2014                                                          
// CREATED BY    : ZUBAIR AHMAD                                                  
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
using System.Data.SqlClient;

public partial class PAYROLL_REPORTS_PayFinalSalaryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objFS = new Leaves();

    //ConnectionStrings
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                   

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
                    int prevyr = System.DateTime.Today.AddYears(-1).Year;
                    int month = System.DateTime.Today.Month;
                    int year = System.DateTime.Today.Year;
                    string frmdt = null;
                    if (month == 1)
                    {
                        frmdt = "01" + "/" + month + "/" + prevyr.ToString();
                    }
                    else
                    {
                        frmdt = "01" + "/" + month + "/" + year.ToString();
                    }


                    //string todt = "20" + "/" + month.ToString() + "/" + year.ToString();
                    string todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();

                    txtFromDate.Text = frmdt;
                    txtToDate.Text = todt;
                    CheckPageAuthorization();

                }

                //Populate DropdownList
                PopulateDropDownList();

                //Focus on From Date Textbox
                txtFromDate.Focus();

                this.FillCollege();
                this.FillStaffType();
                

                //Enable Dropdown
                
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Final_Salary_Report.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Final_Salary_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Final_Salary_Report.aspx");
        }
    }

    private void FillCollege()
    {
        //Added by Saket Singh on 13-Dec-2016 .
        //to Fill College.For filtering college.
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }

    private void FillStaffType()
    {
        //Added by Saket Singh on 13-Dec-2016 .
        //to Fill Staff Type.For filtering staff type.
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

    protected void PopulateDropDownList()
    {
        try
        {
           

            //FILL DEPARTMENT
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Final_Salary_Report.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportFinalSalaryDetail(string reportTitle, string rptFileName)
    {
        try
        {
            int deptno = 0;
            if (ddlDepartment.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            else
            {
                deptno = 0;
            }

            string rept_type = "FINAL";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
           // url += "&param=username=" + Session["username"].ToString() + ",@P_FSDATE=" + txtFromDate.Text.Trim() + ",@P_FEDATE=" + txtToDate.Text.Trim() + ",@P_DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue); //+ ",FSDATE=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",FEDATE=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy")
            //url += "&param=username=" + Session["username"].ToString() + ",@P_FSDATE=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_FEDATE=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue)+",@P_REPORT_TYPE="+rept_type ; 

            url += "&param=username=" + Session["username"].ToString() + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue) + ",@P_FSDATE=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_FEDATE=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_DEPT=" + deptno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Final_Salary_Report.ShowReportFinalSalaryDetail() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlDepartment.SelectedIndex == 0)
            //{
            //    ShowMessage("Please Select Department");
            //    return;
            //}
            objFS.FROMDT = Convert.ToDateTime(txtFromDate.Text);
            objFS.TODT = Convert.ToDateTime(txtToDate.Text);
            objFS.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objFS.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            //objFS.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            int ret = Convert.ToInt32(objApp.CheckAttendanceProcess(objFS));
            if (ret == 1)
            {
                ShowReportFinalSalaryDetail("Final Report For Salary", "Pay_Final_Salary_Report.rpt");
            }
            else if (ret == 0)
            {
                objCommon.DisplayMessage("Sorry! Attendance Not Processed", this);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Final_Salary_Report.btnShowReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime dt2 = Convert.ToDateTime(txtToDate.Text.Trim());
            TimeSpan ts = dt1.Subtract(dt2);
           // int diffMonth = Math.Abs((dt2.Year - dt1.Year) * 12 + dt1.Month - dt2.Month);
            int diffDay = Math.Abs(dt2.Month - dt1.Month); //+ dt1.Month - dt2.Month)
            if (diffDay > 1)
            {
                ShowMessage("Date difference not more than 1 Month.");
            }
            else
            {
                
               
            }
            ddlDepartment.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Final_Salary_Report.txtToDate_TextChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
                objUCommon.ShowError(Page, "Pay_Final_Salary_Report.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

 


    protected void rdoParticularColumn_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    
}
