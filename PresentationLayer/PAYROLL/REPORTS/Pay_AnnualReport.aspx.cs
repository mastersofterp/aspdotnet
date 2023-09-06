//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL ANNUAL REPORT                       
// CREATION DATE : 04-September-2009                                                          
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


public partial class PayRoll_Pay_AnnualReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int ua_type = 0;
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
        int IDNO = Convert.ToInt32(Session["idno"]);
        ua_type = Convert.ToInt32(Session["usertype"]);
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

                //Populate DropdownList
                if (ua_type != 1)
                {
                    //trCertificate.Visible = false;
                    trRadioBtnEmployee.Visible = false;
                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    ddlCollege.SelectedIndex = 1;
                    ddlStaffNo.SelectedIndex = 1;
                    ddlEmployeeNo.SelectedIndex = 1;
                    ddlEmployeeType.SelectedIndex = 1;
                   //Added on 20-02-2022////////////////////////////////////////
                    btnShowReport.Visible = false;
                    btnAnnualSummaryReport.Visible = false;
                    btnAnnualSumExport.Visible = false;
                    btnConsolidateAllHead.Visible = false;
                }
                else
                {
                    PopulateDropDownList();
                    btnShowReport.Visible = true;
                    btnAnnualSummaryReport.Visible = true;
                    btnAnnualSumExport.Visible = true;
                    btnConsolidateAllHead.Visible = true;
                }
                txtFromDate.Focus();
                ddlStaffNo.Enabled = false;
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

            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "'['+ convert(nvarchar(150),PFILENO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO>0", "IDNO");

            //FILL COLLEGE
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID");

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
                Response.Redirect("~/notauthorized.aspx?Pay_AnnualReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_AnnualReport.aspx");
        }
    }

    private void ShowReportAnnualSummary(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitleForAnnualSummary=" + reportTitle;
            url += "&pathForAnnualSummary=~,Reports,Payroll," + rptFileName + "&@P_FROM_DATE=" + txtFromDate.Text.Trim() + "&@P_TO_DATE=" + txtToDate.Text.Trim() + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            url += "&paramForAnnualSummary=username=" + Session["username"].ToString() + ",From_Date=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",To_Date=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy");
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


    private void ShowReportAnnualSummaryNew(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";

            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;

            url += "&param=@P_FROM_DATE=" + txtFromDate.Text.Trim() + ",@P_TO_DATE=" + txtToDate.Text.Trim() + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

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

    protected void btnConsoDedHead_Click(object sender, EventArgs e)
    {
        try
        {
            int IDNO;
            string ContentType = string.Empty;
            string frommonyear = txtFromDate.Text.Trim();
            string Tomonyear = txtToDate.Text.Trim();

            int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int EmployeeTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);

            if (ua_type != 1)
            {
                IDNO = Convert.ToInt32(Session["idno"]);
            }
            else
            {
               // IDNO = 0;
                if (ddlEmployeeNo.SelectedIndex > 0)
                {
                    IDNO = Convert.ToInt32(ddlEmployeeNo.SelectedValue);
                }
                else
                {
                    IDNO = 0;
                }
                
            }

            DataSet ds = PayConsolidateDeductionHeads(frommonyear, Tomonyear, IDNO, CollegeNo, StaffNo, EmployeeTypeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                GridView GVEmpChallan = new GridView();
               
                GVEmpChallan.DataSource = ds;
                GVEmpChallan.DataBind();

                string attachment = "attachment; filename=Pay_Consolidate_Deduction_Summary_.xls";
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnConsoAllHead_Click(object sender, EventArgs e)
    {
        try
        {

            int IDNO;
            string ContentType = string.Empty;
            string frommonyear = txtFromDate.Text.Trim();
            string Tomonyear = txtToDate.Text.Trim();

            int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int EmployeeTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);

            if (ddlEmployeeNo.SelectedIndex > 0)
            {
                IDNO = Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            }
            else
            {
                IDNO = 0;
            }

            DataSet ds = PayConsolidateAllHeads(frommonyear, Tomonyear, IDNO, CollegeNo, StaffNo, EmployeeTypeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView GVEmpChallan = new GridView();

                GVEmpChallan.DataSource = ds;
                GVEmpChallan.DataBind();

                string attachment = "attachment; filename=Pay_Consolidate_All_head_Summary_.xls";
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnAnnualSummaryExport_Click(object sender, EventArgs e)
    {
        try
        {

            int IDNO;
            string ContentType = string.Empty;
            string frommonyear = txtFromDate.Text.Trim();
            string Tomonyear = txtToDate.Text.Trim();

            int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int EmployeeTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
            if (ddlEmployeeNo.SelectedIndex > 0)
            {
                IDNO = Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            }
            else
            {
                IDNO = 0;
            }
            DataSet ds = PayEmpAnnualSummary(frommonyear, Tomonyear, IDNO, CollegeNo, StaffNo, EmployeeTypeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView GVEmpChallan = new GridView();

                GVEmpChallan.DataSource = ds;
                GVEmpChallan.DataBind();

                string attachment = "attachment; filename=Pay_Consolidate_Incomehead_Summary_.xls";
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnConsoIncomeHead_Click(object sender, EventArgs e)
    {
        try
        {
            int IDNO;
            string ContentType = string.Empty;
            string frommonyear = txtFromDate.Text.Trim();
            string Tomonyear = txtToDate.Text.Trim();

            int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int EmployeeTypeNo = Convert.ToInt32(ddlEmployeeType.SelectedValue);
            
            if (ua_type != 1)
            {
                IDNO = Convert.ToInt32(Session["idno"]);
            }
            else
            {
               // IDNO = 0;
                if (ddlEmployeeNo.SelectedIndex > 0)
                {
                    IDNO = Convert.ToInt32(ddlEmployeeNo.SelectedValue);
                }
                else
                {
                    IDNO = 0;
                }
            }
            DataSet ds = PayConsolidateIncomeHeads(frommonyear, Tomonyear, IDNO, CollegeNo, StaffNo,EmployeeTypeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView GVEmpChallan = new GridView();

                GVEmpChallan.DataSource = ds;
                GVEmpChallan.DataBind();

                string attachment = "attachment; filename=Pay_Consolidate_Incomehead_Summary_.xls";
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnAnnualSummaryReport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReportEmployeeAbstractSalaryNew("MultipleMonthsSalaryReport", "rptAnnual_Summary_Report_New.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoParticularEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select Employee.");
                return;
            }
            if (rdoAllEmployee.Checked && ddlStaffNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select StaffNo.");
                return;
            }
            //ShowReportAnnualSummary("Annual_Summary_Report", "rptAnnual_Summary_Report.rpt");
            //ShowReportAnnualSummaryNew("Annual_Summary_Report", "rptAnnual_Summary_Report_New.rpt");
            ShowReportEmployeeAbstractSalary("MultipleMonthsSalaryReport", "Pay_Salary_StatementReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_AnnualReport.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReportEmployeeAbstractSalary(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@Username=" + Session["username"].ToString() + ",@P_FROMDATE=" + (txtFromDate.Text) + ",@P_TODATE=" + (txtToDate.Text) + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPLOYEETYPE_NO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",@P_PageWiseTotal=1";
            // url += "&param=Remark=" + txtRemark.Text.ToString();
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

    private void ShowReportEmployeeAbstractSalaryNew(string reportTitle, string rptFileName)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_FROM_DATE=" + (txtFromDate.Text) + ",@P_TO_DATE=" + (txtToDate.Text) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_EMPLOYEETYPE_NO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue);
            // url += "&param=Remark=" + txtRemark.Text.ToString();
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

    protected void PopulateDropDownListForFaculty()
    {
        ddlCollege.Enabled = false;
        ddlStaffNo.Enabled = false;

        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
        int collegeNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + IDNO));
        int EmployeeTypeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "EMPTYPENO", "IDNO=" + IDNO));
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO=" + collegeNo, "COLLEGE_NO ASC");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO="+EmployeeTypeno, "EMPTYPENO");
            //FILL MONTH YEAR 
            //objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103)");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");

            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),PFILENO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public DataSet PayEmpAnnualSummary(string FromMonyear, string toMonyear, int idno, int CollegeNo, int StaffNo, int EmployeeTypeNo)
    {
        DataSet ds = null;
        try
        {

            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];

            objParams[0] = new SqlParameter("@P_FROM_DATE", FromMonyear);
            objParams[1] = new SqlParameter("@P_TO_DATE", toMonyear);
            objParams[2] = new SqlParameter("@P_IDNO", idno);
            objParams[3] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            objParams[4] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[5] = new SqlParameter("@P_EMPLOYEETYPE_NO", EmployeeTypeNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_EMPLOYEEWISE_ANNUAL_ALL_HEAD_SUMMARY", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

    public DataSet PayConsolidateAllHeads(string FromMonyear, string toMonyear, int idno, int CollegeNo, int StaffNo,int EmployeeTypeNo)
    {
        DataSet ds = null;
        try
        {

            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];

            objParams[0] = new SqlParameter("@P_FROM_DATE", FromMonyear);
            objParams[1] = new SqlParameter("@P_TO_DATE", toMonyear);
            objParams[2] = new SqlParameter("@P_IDNO", idno);
            objParams[3] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            objParams[4] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[5] = new SqlParameter("@P_EMPLOYEETYPE_NO", EmployeeTypeNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ANNUAL_ALL_HEAD_SUMMARY", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }


    public DataSet PayConsolidateIncomeHeads(string FromMonyear, string toMonyear, int idno, int CollegeNo, int StaffNo, int EmployeeTypeNo)
    {
        DataSet ds = null;
        try
        {

            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];

            objParams[0] = new SqlParameter("@P_FROM_DATE", FromMonyear);
            objParams[1] = new SqlParameter("@P_TO_DATE", toMonyear);
            objParams[2] = new SqlParameter("@P_IDNO", idno);
            objParams[3] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            objParams[4] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[5] = new SqlParameter("@P_EMPLOYEETYPE_NO", EmployeeTypeNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ANNUAL_INCOME_HEAD_SUMMARY", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

    public DataSet PayConsolidateDeductionHeads(string FromMonyear, string toMonyear, int idno, int CollegeNo, int StaffNo, int EmployeeTypeNo)
    {
        DataSet ds = null;
        try
        {

            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];

            objParams[0] = new SqlParameter("@P_FROM_DATE", FromMonyear);
            objParams[1] = new SqlParameter("@P_TO_DATE", toMonyear);
            objParams[2] = new SqlParameter("@P_IDNO", idno);
            objParams[3] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            objParams[4] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[5] = new SqlParameter("@P_EMPLOYEETYPE_NO", EmployeeTypeNo);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_ANNUAL_DEDUCTION_HEAD_SUMMARY", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }
}

