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
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic;
using System.IO;


public partial class PayRoll_Abstract_Salary : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //ConnectionStrings
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    int OrganizationId;
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
                    OrganizationId = Convert.ToInt32(Session["OrgId"]);
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Populate DropdownList
                PopulateDropDownList();
                FillListBoxStaff();

                if (OrganizationId == 5)
                {
                    btnSalRegActualRateExport.Visible = true;
                }
                else
                {
                    btnSalRegActualRateExport.Visible = false;
                }

            }
            divMsg.InnerHtml = string.Empty;
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
            //objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "SALNO", "MONYEAR", "SALNO>0", "SALNO");


            //objCommon.FillSalfileDropDownList(ddlMonthYear);
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");

            //FILL STAFF
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
            objCommon.FillListBox(ddlStaffNo1, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
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



    #region Sachin edit


    private void ShowReportEmployeeAbstractSalary(string reportTitle, string rptFileName)
    {
        try
        {
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Payroll")));
            //url += "Reports/CommonReport.aspx?";
            if (chkAbstarct.Checked)
            {
                url += "pagetitleForEmployeeCummulativeAbstractSalary=" + reportTitle;
                //url += "&pathForEmployeeCummulativeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_IDNO=0"; // Original
                url += "&pathForEmployeeCummulativeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + stafflist + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_IDNO=0";
                url += "&paramForEmployeeCummulativeAbstractSalary=username=" + Session["username"].ToString();
                url += "&paramForEmployeeCummulativeAbstractSalary=&@P_COLLEGE_CODE=" + Session["colcode"].ToString() + " ";
            }
            else
            {
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Payroll," + rptFileName;
                // url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue)+",username=Admin";
                // url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",username=Admin,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + " ";
                url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",username=Admin,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + " ";
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
                //old 
                //url += "pagetitleForEmployeeAbstractSalary=" + reportTitle;
                //url += "&pathForEmployeeAbstractSalary=~,Reports,Payroll," + rptFileName + ",@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_IDNO=0";
                //url += "&paramForEmployeeAbstractSalary=username=" + Session["username"].ToString();
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


    private void ShowReportEmployeeAbstractSalaryDeptWise(string reportTitle, string rptFileName)
    {
        try
        {

            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            //url += "Reports/CommonReport.aspx?";

            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName;
            //url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";



            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";

            if (chkAbstarct.Checked)
            {
                url += "pagetitleForEmployeeCummulativeAbstractSalary=" + reportTitle;
                //url += "&pathForEmployeeCummulativeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_IDNO=0";
                url += "&pathForEmployeeCummulativeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + stafflist + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_IDNO=0";
                url += "&paramForEmployeeCummulativeAbstractSalary=username=" + Session["username"].ToString();
            }
            else
            {
               // url += "pagetitleForEmployeeAbstractSalary=" + reportTitle;
               // // url += "&pathForEmployeeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + "&@P_IDNO=0";
               //// url += "&pathForEmployeeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + stafflist + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + "&@P_IDNO=0";
               // url += "&paramForEmployeeAbstractSalary=username=" + Session["username"].ToString();

                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Payroll," + rptFileName;
                // url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue)+",username=Admin";
                // url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",username=Admin,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + " ";
                url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",username=Admin ";
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            
            
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

    #endregion




    protected void btnRegisterWithAbstractRCPIT_Click(object sender, EventArgs e)
    {
        try
        {
            OrganizationId = Convert.ToInt32(Session["OrgId"]);

            string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "OrganizationId=" + OrganizationId + " AND IdType=3");


            if (chkAbstarct.Checked)
            {
                ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Cummulative.rpt");
            }
            else if (chkAbstractSummary.Checked)
            {
                ShowReport(listStaff(), "Employee_Abstract_Salary", "Pay_Salary_Summary_Staff_Ycce.rpt");
            }
            else
            {
                //ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary_RCPIT.rpt");
                //  ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary.rpt");
                // ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", ReportName);
                // ShowReportEmployeeAbstractSalary("EmployeeAbstractSalary", "rptAbstract_Salary_New.rpt");
                if (ReportName == "")
                {
                    ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary.rpt");
                }
                else
                {
                    ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", ReportName);
                }
            }
            if (ReportName == "")
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



    protected void btnRegisterWithAbstract_Click(object sender, EventArgs e)
    {
        try
        {
             OrganizationId = Convert.ToInt32(Session["OrgId"]);
            string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "OrganizationId=" + OrganizationId+"   AND IdType=3");

            if (chkAbstarct.Checked)
            {
                ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Cummulative.rpt");
            }
            else if (chkAbstractSummary.Checked)
            {
                ShowReport(listStaff(), "Employee_Abstract_Salary", "Pay_Salary_Summary_Staff_Ycce.rpt");
            }
            else
            {
                if (ReportName == "")
                {
                    ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary.rpt");
                }
                else
                {
                    ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", ReportName);
                }
                // ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary.rpt");
                // ShowReportEmployeeAbstractSalary("EmployeeAbstractSalary", "rptAbstract_Salary_New.rpt");
            }
            if (ReportName == "")
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

    protected void btnRegisterWithDept_Click(object sender, EventArgs e)
    {
        try
        {
            OrganizationId = Convert.ToInt32(Session["OrgId"]);

            string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "OrganizationId=" + OrganizationId + " AND IdType=4");

            if (chkAbstarct.Checked)
            {
                if (ReportName == "")
                {
                     ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary_Dept", "rptAbstract_Cummulative.rpt");
                }
                else
                {
                     ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary_Dept", ReportName);
                }
            }
            else
            {
                if (ReportName == "")
                {
                    ShowReportEmployeeAbstractSalaryDeptWise("Employee_Abstract_Salary_Dept", "rptAbstract_Salary_DeptWise.rpt");
                }
                else
                {
                    ShowReportEmployeeAbstractSalaryDeptWise("Employee_Abstract_Salary_Dept", ReportName);
                }
                // ShowReportEmployeeAbstractSalaryDeptWise("Employee_Abstract_Salary_Dept", "rptAbstract_Salary_DeptWise_New.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.btnRegisterWithDept_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void FillListBoxStaff()
    {
        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

        //SqlParameter[] objParams = new SqlParameter[2];

        SqlParameter[] objParams = new SqlParameter[1];
        objParams[0] = new SqlParameter("@P_MONYEAR", "0");

        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_FILL_STAFF", objParams);
        lstStaffFill.Items.Clear();
        lstStaffFill.Items.Add(new ListItem("Please Select", "0"));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstStaffFill.DataSource = ds;
            lstStaffFill.DataTextField = "STAFF";
            lstStaffFill.DataValueField = "STAFFNO";
            lstStaffFill.DataBind();
            lstStaffFill.SelectedIndex = 0;
        }
    }
    protected void chkAbstractSummary_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkAbstractSummary.Checked == true)
        //{
        //    trmultistaff.Visible = true;
        //    trstaff.Visible = false;
        //}
        //else
        //{
        //    trmultistaff.Visible = false;
        //    trstaff.Visible = true;
        //}
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@V_TABLE=" + ddlMonthYear.SelectedItem.Text + ",@V_STAFFNO=" + param + ",@V_COLLEGE_NO=" + ddlCollege.SelectedValue;
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

    private string listStaff()
    {
        string listOfMonth = string.Empty;
        try
        {
            {
                for (int i = 0; i < lstStaffFill.Items.Count; i++)
                {
                    if (lstStaffFill.SelectedIndex == 0)
                    {
                        listOfMonth = string.Empty;
                        return listOfMonth;
                    }
                    else
                    {
                        if (lstStaffFill.Items[i].Selected)
                        {
                            listOfMonth += lstStaffFill.Items[i].Value + ".";
                        }
                    }

                }
                listOfMonth = listOfMonth.Substring(0, listOfMonth.Length - 1);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.listMonth() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return listOfMonth;

    }
    private void ShowSalarySummaryReport(string reportTitle, string rptFileName)
    {
        try
        {

            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }
            if (count == 0)
            {
                stafflist = "";
            }
            if (count == 0)
            {
                stafflist = "0";
                ShowMessage("Select One Staff Name");
                return;
            }
            else if (count == 1)
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
            }
            else
            {
                ShowMessage("Select Only One Staff Name");
                return;
            }


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MON_YEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGENO=" + ddlCollege.SelectedValue;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MON_YEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGENO=" + ddlCollege.SelectedValue;
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
    private void ShowSupplementaryReport(string reportTitle, string rptFileName)
    {
        try
        {
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
         //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MON_YEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + ddlStaffNo.SelectedValue + ",@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_IDNO=" + 0;
           url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MON_YEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_IDNO=" + 0;
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
    private void ShowSupplementaryReport2(string reportTitle, string rptFileName)
    {
        try
        {
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONYEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFFNO=" + ddlStaffNo.SelectedValue + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONYEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_STAFF_NO=" + stafflist;
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
    //SELECT  ONLY ONE STAFF FOR GENERATING THIS REPORT
    protected void btnSalarySummary_Click(object sender, EventArgs e)
    {
        ShowSalarySummaryReport("Employee_Salary_Summary", "rptAbstract_Summary_Report.rpt");
    }
    protected void btnSuppliBill_Click(object sender, EventArgs e)
    {
        ShowSupplementaryReport("Employee_Supplementary_Report", "Pay_SuppliBillREport.rpt");
    }
    //Sachin ghagre 05 June 2017

    #region Export to Excel

    protected void btnShowAbstractExcel_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Month Year", this.Page);
            return;
        }
        div_ExportToExcel.Visible = true;
        BindListViewReport();

    }
    private void BindListViewReport()
    {
        try
        {
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }

            Payroll_Report_Controller objPC = new Payroll_Report_Controller();

            DataSet ds = objPC.GetAtstractRegisterExcel(ddlMonthYear.SelectedItem.Text, stafflist, Convert.ToInt32(ddlEmployeeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Session["colcode"].ToString());
            DataTable dt = ds.Tables[0];


            grdSelectFieldReport.DataSource = dt;
            grdSelectFieldReport.DataBind();

            //EARNING HEADS
            Label lblh1 = grdSelectFieldReport.FindControl("lblEH1") as Label; lblh1.Text = ds.Tables[0].Rows[0]["HEADEARNING1"].ToString();
            Label lblh2 = grdSelectFieldReport.FindControl("lblEH2") as Label; lblh2.Text = ds.Tables[0].Rows[0]["HEADEARNING2"].ToString();
            Label lblh3 = grdSelectFieldReport.FindControl("lblEH3") as Label; lblh3.Text = ds.Tables[0].Rows[0]["HEADEARNING3"].ToString();
            Label lblh4 = grdSelectFieldReport.FindControl("lblEH4") as Label; lblh4.Text = ds.Tables[0].Rows[0]["HEADEARNING4"].ToString();
            Label lblh5 = grdSelectFieldReport.FindControl("lblEH5") as Label; lblh5.Text = ds.Tables[0].Rows[0]["HEADEARNING5"].ToString();
            Label lblh6 = grdSelectFieldReport.FindControl("lblEH6") as Label; lblh6.Text = ds.Tables[0].Rows[0]["HEADEARNING6"].ToString();
            Label lblh7 = grdSelectFieldReport.FindControl("lblEH7") as Label; lblh7.Text = ds.Tables[0].Rows[0]["HEADEARNING7"].ToString();
            Label lblh8 = grdSelectFieldReport.FindControl("lblEH8") as Label; lblh8.Text = ds.Tables[0].Rows[0]["HEADEARNING8"].ToString();
            Label lblh9 = grdSelectFieldReport.FindControl("lblEH9") as Label; lblh9.Text = ds.Tables[0].Rows[0]["HEADEARNING9"].ToString();
            Label lblh10 = grdSelectFieldReport.FindControl("lblEH10") as Label; lblh10.Text = ds.Tables[0].Rows[0]["HEADEARNING10"].ToString();
            Label lblh11 = grdSelectFieldReport.FindControl("lblEH11") as Label; lblh11.Text = ds.Tables[0].Rows[0]["HEADEARNING11"].ToString();
            Label lblh12 = grdSelectFieldReport.FindControl("lblEH12") as Label; lblh12.Text = ds.Tables[0].Rows[0]["HEADEARNING12"].ToString();
            Label lblh13 = grdSelectFieldReport.FindControl("lblEH13") as Label; lblh13.Text = ds.Tables[0].Rows[0]["HEADEARNING13"].ToString();
            Label lblh14 = grdSelectFieldReport.FindControl("lblEH14") as Label; lblh14.Text = ds.Tables[0].Rows[0]["HEADEARNING14"].ToString();
            Label lblh15 = grdSelectFieldReport.FindControl("lblEH15") as Label; lblh15.Text = ds.Tables[0].Rows[0]["HEADEARNING15"].ToString();

            //DEDUCTION HEADS
            Label lblh16 = grdSelectFieldReport.FindControl("lblDH1") as Label; lblh16.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND1"].ToString();
            Label lblh17 = grdSelectFieldReport.FindControl("lblDH2") as Label; lblh17.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND2"].ToString();
            Label lblh18 = grdSelectFieldReport.FindControl("lblDH3") as Label; lblh18.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND3"].ToString();
            Label lblh19 = grdSelectFieldReport.FindControl("lblDH4") as Label; lblh19.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND4"].ToString();
            Label lblh20 = grdSelectFieldReport.FindControl("lblDH5") as Label; lblh20.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND5"].ToString();
            Label lblh21 = grdSelectFieldReport.FindControl("lblDH6") as Label; lblh21.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND6"].ToString();
            Label lblh22 = grdSelectFieldReport.FindControl("lblDH7") as Label; lblh22.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND7"].ToString();
            Label lblh23 = grdSelectFieldReport.FindControl("lblDH8") as Label; lblh23.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND8"].ToString();
            Label lblh24 = grdSelectFieldReport.FindControl("lblDH9") as Label; lblh24.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND9"].ToString();
            Label lblh25 = grdSelectFieldReport.FindControl("lblDH10") as Label; lblh25.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND10"].ToString();
            Label lblh26 = grdSelectFieldReport.FindControl("lblDH11") as Label; lblh26.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND11"].ToString();
            Label lblh27 = grdSelectFieldReport.FindControl("lblDH12") as Label; lblh27.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND12"].ToString();
            Label lblh28 = grdSelectFieldReport.FindControl("lblDH13") as Label; lblh28.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND13"].ToString();
            Label lblh29 = grdSelectFieldReport.FindControl("lblDH14") as Label; lblh29.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND14"].ToString();
            Label lblh30 = grdSelectFieldReport.FindControl("lblDH15") as Label; lblh30.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND15"].ToString();

            Label lblh31 = grdSelectFieldReport.FindControl("lblDH16") as Label; lblh31.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND16"].ToString();
            Label lblh32 = grdSelectFieldReport.FindControl("lblDH17") as Label; lblh32.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND17"].ToString();
            Label lblh33 = grdSelectFieldReport.FindControl("lblDH18") as Label; lblh33.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND18"].ToString();
            Label lblh34 = grdSelectFieldReport.FindControl("lblDH19") as Label; lblh34.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND19"].ToString();
            Label lblh35 = grdSelectFieldReport.FindControl("lblDH20") as Label; lblh35.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND20"].ToString();
            Label lblh36 = grdSelectFieldReport.FindControl("lblDH21") as Label; lblh36.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND21"].ToString();
            Label lblh37 = grdSelectFieldReport.FindControl("lblDH22") as Label; lblh37.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND22"].ToString();
            Label lblh38 = grdSelectFieldReport.FindControl("lblDH23") as Label; lblh38.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND23"].ToString();
            Label lblh39 = grdSelectFieldReport.FindControl("lblDH24") as Label; lblh39.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND24"].ToString();
            Label lblh40 = grdSelectFieldReport.FindControl("lblDH25") as Label; lblh40.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND25"].ToString();
            Label lblh41 = grdSelectFieldReport.FindControl("lblDH26") as Label; lblh41.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND26"].ToString();
            Label lblh42 = grdSelectFieldReport.FindControl("lblDH27") as Label; lblh42.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND27"].ToString();
            Label lblh43 = grdSelectFieldReport.FindControl("lblDH28") as Label; lblh43.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND28"].ToString();
            Label lblh44 = grdSelectFieldReport.FindControl("lblDH29") as Label; lblh44.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND29"].ToString();
            Label lblh45 = grdSelectFieldReport.FindControl("lblDH30") as Label; lblh45.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND30"].ToString();



            // grdSelectFieldReport.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void imgbutExporttoexcel_Click(object sender, ImageClickEventArgs e)
    {
        if (grdSelectFieldReport == null)
        {
            objCommon.DisplayMessage(this.Page, "Please select Month and Staff", this.Page);
            return;
        }
        this.Export("Excel");

    }
    private void Export(string type)
    {
        try
        {
            string filename = string.Empty;
            string ContentType = string.Empty;
            filename = "AbstractRegister.xls";
            ContentType = "ms-excel";
            string attachment = "attachment; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + ContentType;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdSelectFieldReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        catch (Exception ex)
        {

        }
    }

    #endregion


    //Sachin ghagre 24 Apr 2018
    //SELECT  ONLY ONE STAFF FOR GENERATING THIS REPORT
    protected void btnGrossDiff_Click(object sender, EventArgs e)
    {
        ShowGrossDifferenceReport("GrossDiffernce Report", "Payroll_GrossDiffReport.rpt");
    }


    private void ShowGrossDifferenceReport(string reportTitle, string rptFileName)
    {
        try
        {

            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }
            if (count == 0)
            {
                stafflist = "0";
                ShowMessage("Select One Staff Name");
                return;
            }
            else if (count == 1)
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
            }
            else
            {
                ShowMessage("Select Only One Staff Name");
                return;
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TABNAME=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_EMPTYPENO=" + ddlEmployeeType.SelectedValue;
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TABNAME=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_EMPTYPENO=" + ddlEmployeeType.SelectedValue;
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
    protected void btnSuppBill2_Click(object sender, EventArgs e)
    {
        ShowSupplementaryReport2("Employee Supplementary Report", "Pay_Handled_SupplementaryBill.rpt");
    }
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void btnSalRegActualRateExport_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            ShowMessage("Please Select Month");
            return;
        }

        int count = 0;
        string stafflist = string.Empty;
        for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
        {
            if (ddlStaffNo1.Items[i].Selected)
            {
                stafflist += ddlStaffNo1.Items[i].Value + "$";
                count++;
            }
            else
            {
            }
        }
        if (count == 0)
        {
            stafflist = "";
        }
        else if (count == 1)
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }
        else
        {
            ShowMessage("Select Only One Staff Name");
            return;
        }
        // new code here
        string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.ToString();
        int EmpTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
        string StaffNo = stafflist;
        //int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int IDNO = 0;
        DataSet ds = EmployeeSalaryRegisterActualRate(monyear, EmpTypeNo, StaffNo, CollegeNo, IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");


            GridView GVEmpChallan = new GridView();
            //ds.Tables[0].Columns.RemoveAt(3);
            GVEmpChallan.DataSource = ds;
            GVEmpChallan.DataBind();
            GVEmpChallan.AllowSorting = true;
            GVEmpChallan.AllowPaging = true;

            // Header Row 1
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = "Report Name : Company Payout Report";
            HeaderCell.ColumnSpan = 38;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 16;
            HeaderCell.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow.Cells.Add(HeaderCell);
            GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


            // Header Row 2
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = collename + "  Pay Period:  " + Month + "    " + Year + "    ";
            HeaderCell1.ColumnSpan = 38;
            HeaderCell1.BackColor = System.Drawing.Color.Navy;
            HeaderCell1.ForeColor = System.Drawing.Color.White;
            HeaderCell1.Font.Bold = true;
            HeaderCell1.Font.Size = 14;
            HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);


            // GVEmpChallan.HeaderRow.Parent.Controls.AddAt(0, HeaderGridRow);
            string attachment = "attachment; filename=EmployeeSalaryRegisterWithGrossRate.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVEmpChallan.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
    protected void btnexporttoexcelsalaryreg_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            ShowMessage("Please Select Month");
            return;
        }

        if (chkAbstractSummary.Checked == true)
        {
            // new code here
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }

            string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string ContentType = string.Empty;
            string monyear = ddlMonthYear.SelectedItem.ToString();
            int EmpTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
            string StaffNo = stafflist;
           //int StaffNo = Convert.ToInt32(lstStaffFill.SelectedValue);
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int IDNO = 0;
            DataSet ds = EmployeeSalaryRegisterWithAbstract(monyear, StaffNo, CollegeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
               // string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
                string StaffName="";
                string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");
                GridView GVEmpChallan = new GridView();
                //ds.Tables[0].Columns.RemoveAt(3);
                GVEmpChallan.DataSource = ds;
                GVEmpChallan.DataBind();


                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = collename;
                HeaderCell.ColumnSpan = 38;
                HeaderCell.BackColor = System.Drawing.Color.Navy;
                HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


                // Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = StaffName + "  Salary  " + Month + "    " + Year + "    ";
                HeaderCell1.ColumnSpan = 38;
                HeaderCell1.BackColor = System.Drawing.Color.Navy;
                HeaderCell1.ForeColor = System.Drawing.Color.White;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);

                // string attachment = "attachment; filename=EmployeeSalaryRegisterWithAbstract.xls";
                string attachment = "attachment; filename=EmployeeSalaryRegisterWithAbstract.xls";

                Response.ClearContent();
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.Charset = "utf-8";
                Response.Buffer = true;
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel"; // Original
                // Response.ContentType = "application/ms-excel";
                // Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVEmpChallan.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('No Record Found for Selected Options')", true);
            }
        }
        else
        {

            // new code here
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + "$";
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                stafflist = "";
            }

            string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string ContentType = string.Empty;
            string monyear = ddlMonthYear.SelectedItem.ToString();
            int EmpTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
           // int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
            string StaffNo = stafflist;
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int IDNO = 0;
            DataSet ds = EmployeeSalaryRegister(monyear, EmpTypeNo, StaffNo, CollegeNo, IDNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
                //string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
                string StaffName = "";
                string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");


                GridView GVEmpChallan = new GridView();
                //ds.Tables[0].Columns.RemoveAt(3);
                GVEmpChallan.DataSource = ds;
                GVEmpChallan.DataBind();
                GVEmpChallan.AllowSorting = true;
                GVEmpChallan.AllowPaging = true;

                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = collename;
                HeaderCell.ColumnSpan = 38;
                HeaderCell.BackColor = System.Drawing.Color.Navy;
                HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


                // Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = StaffName + "  Salary  " + Month + "    " + Year + "    ";
                HeaderCell1.ColumnSpan = 38;
                HeaderCell1.BackColor = System.Drawing.Color.Navy;
                HeaderCell1.ForeColor = System.Drawing.Color.White;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);


                // GVEmpChallan.HeaderRow.Parent.Controls.AddAt(0, HeaderGridRow);
                string attachment = "attachment; filename=EmployeeSalaryRegister.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVEmpChallan.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

    }

    protected void btnPaybillIncome_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            ShowMessage("Please Select Month");
            return;
        }

        int count = 0;
        string stafflist = string.Empty;
        for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
        {
            if (ddlStaffNo1.Items[i].Selected)
            {
                stafflist += ddlStaffNo1.Items[i].Value + "$";
                count++;
            }
            else
            {
            }
        }
        if (count == 0)
        {
            stafflist = "";
        }

        // new code here
        string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.ToString();
        int EmpTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
        //int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
        string StaffNo = stafflist;
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int IDNO = 0;
        DataSet ds = PayBillIncome(monyear, EmpTypeNo, StaffNo, CollegeNo, IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
           // string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");


            GridView GVEmpChallan = new GridView();
            //ds.Tables[0].Columns.RemoveAt(3);
            GVEmpChallan.DataSource = ds;
            GVEmpChallan.DataBind();

            // Header Row 1
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = collename;
            HeaderCell.ColumnSpan = 10;
            //HeaderCell.BackColor = System.Drawing.Color.Navy;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 16;
            HeaderCell.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow.Cells.Add(HeaderCell);
            GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


            // Header Row 2
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "  PAY BILL EARNING -" + Month + "    " + Year + "    ";
            HeaderCell1.ColumnSpan = 10;
            //HeaderCell1.BackColor = System.Drawing.Color.Navy;
            //HeaderCell1.ForeColor = System.Drawing.Color.White;
            HeaderCell1.Font.Bold = true;
            HeaderCell1.Font.Size = 14;
            HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);


            // GVEmpChallan.HeaderRow.Parent.Controls.AddAt(0, HeaderGridRow);
            string attachment = "attachment; filename=PayBill_Earnings_" + monyear + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVEmpChallan.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
    }

    protected void BtnPaybillRec_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            ShowMessage("Please Select Month");
            return;
        }
        int count = 0;
        string stafflist = string.Empty;
        for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
        {
            if (ddlStaffNo1.Items[i].Selected)
            {
                stafflist += ddlStaffNo1.Items[i].Value + "$";
                count++;
            }
            else
            {
            }
        }
        if (count == 0)
        {
            stafflist = "";
        }


        // new code here
        string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.ToString();
        int EmpTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
        //int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
        string StaffNo = stafflist;
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int IDNO = 0;
        DataSet ds = PayBillRecovery(monyear, EmpTypeNo, StaffNo, CollegeNo, IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            //string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");


            GridView GVEmpChallan = new GridView();
            //ds.Tables[0].Columns.RemoveAt(3);
            GVEmpChallan.DataSource = ds;
            GVEmpChallan.DataBind();

            // Header Row 1
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = collename;
            HeaderCell.ColumnSpan = 20;
            //HeaderCell.BackColor = System.Drawing.Color.Navy;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 16;
            HeaderCell.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow.Cells.Add(HeaderCell);
            GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


            // Header Row 2
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "  PAY BILL RECOVERY  " + Month + "    " + Year + "    ";
            HeaderCell1.ColumnSpan = 20;
            //HeaderCell1.BackColor = System.Drawing.Color.Navy;
            //HeaderCell1.ForeColor = System.Drawing.Color.White;
            HeaderCell1.Font.Bold = true;
            HeaderCell1.Font.Size = 14;
            HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);


            // GVEmpChallan.HeaderRow.Parent.Controls.AddAt(0, HeaderGridRow);
            string attachment = "attachment; filename=PayBill_Recovery_" + monyear + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVEmpChallan.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
    }



    public DataSet EmployeeSalaryRegisterWithAbstract(string monyear, string StaffNo, int CollegeNo)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@V_TABLE ", monyear);
            objParams[1] = new SqlParameter("@V_STAFFNO", StaffNo);
            objParams[2] = new SqlParameter("@V_COLLEGE_NO", CollegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_SALARY_SUMMARY_STAFFWISE_REPORT", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }
    public DataSet EmployeeSalaryRegister(string monyear, int EmpTypeNo, string StaffNo, int CollegeNo, int IDNO)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_MON_YEAR", monyear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
            objParams[3] = new SqlParameter("@P_IDNO ", IDNO);
            objParams[4] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_DAYNAMIC_PAYSLIP", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }


    public DataSet EmployeeSalaryRegisterActualRate(string monyear, int EmpTypeNo, string StaffNo, int CollegeNo, int IDNO)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_MON_YEAR", monyear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
            objParams[3] = new SqlParameter("@P_IDNO ", IDNO);
            objParams[4] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_DAYNAMIC_PAYSLIP_ACTUAL_RATE", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

    public DataSet PayBillRecovery(string monyear, int EmpTypeNo, string  StaffNo, int CollegeNo, int IDNO)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_MON_YEAR", monyear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
            objParams[3] = new SqlParameter("@P_IDNO ", IDNO);
            objParams[4] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_DAYNAMIC_PAYSLIP_DEDUCTION_HEAD", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

    public DataSet PayBillIncome(string monyear, int EmpTypeNo, string StaffNo, int CollegeNo, int IDNO)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_MON_YEAR", monyear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
            objParams[3] = new SqlParameter("@P_IDNO ", IDNO);
            objParams[4] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_DAYNAMIC_PAYSLIP_INCOME_HEAD", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

}
