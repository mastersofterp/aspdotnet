using System;
using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Net;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_TRANSACTIONS_Pay_DailyWagesEmployee : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();
    int UATYPE;
    int UserNo,UserIdNo;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
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
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                UATYPE = Convert.ToInt32(Session["usertype"]);
                UserNo=Convert.ToInt32(Session["userno"]);
                //
                if (UATYPE == 1)
                {
                    FillDropdown();
                    btnunlock.Visible = true;
                    btnreport.Visible = false;
                }
              }
                pnlIncrement.Visible = false;
                pnlSelect.Visible = true;
            }
    }
    protected void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFF like '%Daily%'", "STAFFNO");
            objCommon.FillDropDownList(ddldepratment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", string.Empty, "SUBDEPTNO ASC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeTransfer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeTransfer.aspx");
        }
    }
    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddldepratment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        //if (txtMonthYear.Text != "" && ddlStaff.SelectedIndex > 0)
        //{
        //    this.BindListViewValidation();
        //}
        //else if (txtMonthYear.Text == "")
        //{
        //    objCommon.DisplayMessage(UpdatePanel1, "Enter Value In Month Year", this);
        //    return;
        //}
        //if (ddlStaff.SelectedIndex == 0)
        //{

        //    objCommon.DisplayMessage(UpdatePanel1, "Select Staff is Mandatory", this);
        //    return;
        //}
        BindListViewValidation();
    }
    protected void BindListViewValidation()
    {
        lblerror.Text = string.Empty;

        BindListViewList(0, Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToString(txtMonthYear.Text.Trim()), Convert.ToInt32(ddldepratment.SelectedValue));
    }

    private void BindListViewList(int collegeNo, int staffNo, string month, int DeptID)
    {
        try
        {
           if (Convert.ToInt32(ddlStaff.SelectedValue) > 0 && txtMonthYear.Text != "")
            {
                pnlIncrement.Visible = true;
                 DataSet ds = objECC.GetDailyWagesEmployeesNew(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue), txtMonthYear.Text.Trim(), Convert.ToInt32(ddldepratment.SelectedValue));
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }
                lvIncrement.DataSource = ds;
                lvIncrement.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvIncrement_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HiddenField hdnattendencelock1 = (HiddenField)e.Item.FindControl("hdnattendencelock");
            Label lblAttendecelock = (Label)e.Item.FindControl("lblattendencelock");
            TextBox txtfixamt = (TextBox)e.Item.FindControl("txtfixamt");
            TextBox txtMonth = (TextBox)e.Item.FindControl("txtMonth");
            TextBox txtpayabledays = (TextBox)e.Item.FindControl("txtpayabledays");
            TextBox txtBasic = (TextBox)e.Item.FindControl("txtBasic");
            TextBox txtadmissibledays = (TextBox)e.Item.FindControl("txtadmissibledays");
            TextBox txtpayableholidays = (TextBox)e.Item.FindControl("txtpayableholidays");
            TextBox txttotalnodays = (TextBox)e.Item.FindControl("txttotalnodays");
            TextBox txttotalpayabledays = (TextBox)e.Item.FindControl("txttotalpayabledays");
            CheckBox chkID = (CheckBox)e.Item.FindControl("chkID");
            if (lblAttendecelock.Text == "True")
            {
                txtfixamt.Enabled = false;
                txtMonth.Enabled = false;
                txtpayabledays.Enabled = false;
                txtadmissibledays.Enabled = false;
                txtpayableholidays.Enabled = false;
                txttotalnodays.Enabled = false;
                txttotalpayabledays.Enabled = false;
                txtBasic.Enabled = false;
                chkID.Checked = true;
            }
            else
            {
                if (txtpayabledays.Text != "0" && txtadmissibledays.Text != "0" && txtpayableholidays.Text != "0")
                {
                  //  txtfixamt.Enabled = false;
                   // txtMonth.Enabled = false;
                    txtfixamt.Enabled = true;
                   // txtMonth.Enabled = false;
                    txtpayabledays.Enabled = true;
                    txtadmissibledays.Enabled = true;
                    txtpayableholidays.Enabled = true;
                    txttotalnodays.Enabled = false;
                    txttotalpayabledays.Enabled = false;
                    txtBasic.Enabled = false;
                    chkID.Checked = false;
                }
                else
                {
                    txtfixamt.Enabled = false;
                    txtMonth.Enabled = false;
                    txtpayabledays.Enabled = true;
                    txtadmissibledays.Enabled = true;
                    txtpayableholidays.Enabled = true;
                    txttotalnodays.Enabled = false;
                    txttotalpayabledays.Enabled = false;
                    txtBasic.Enabled = false;
                    chkID.Checked = true;
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            double TotalPayabledays = 0;
            double Payabledays = 0;
            double FixedRemuneration = 0;
            double NewBasic = 0;
            int StaffId;
            DateTime MONTHYEAR;
            double TotalDaysinMonth = 0;
            bool Status;
            double AdmissibleWorkingDays = 0;
            double PayableHolidays = 0;
            double TotalDays = 0;
            double TotalPayableDays = 0;
            int DeptId = 0;
            if (checkCurrentMonthSalaryProcess() == 1)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Salary is Lock Permenantly,You Can not Re-enter Attendance", this);
            }
            //else if (checkPreviousMonthSalaryProcess() == 2)
            //{
            //    objCommon.DisplayMessage(UpdatePanel1, "Salary is Not Process for Previous Month,Future Date Month Attendance Not Allowed", this);
            //}
            else                     //if (checkSalaryProcess() == 0) 
            {
                foreach (ListViewDataItem lvitem in lvIncrement.Items)
                {
                    TextBox txtFixAmt = lvitem.FindControl("txtfixamt") as TextBox;
                    //TextBox txtInc = lvitem.FindControl("txtInc") as TextBox;
                    CheckBox chkbox = lvitem.FindControl("chkID") as CheckBox;
                    TextBox txtBasic = lvitem.FindControl("txtBasic") as TextBox;
                    TextBox txtDailyDays = lvitem.FindControl("txtMonth") as TextBox;
                    StaffId = Convert.ToInt32(ddlStaff.SelectedValue);  // STAFF ID
                    MONTHYEAR = Convert.ToDateTime(txtMonthYear.Text.Trim()); // MON YEAR
                    TotalDaysinMonth = Convert.ToDouble(txtDailyDays.Text.Trim()); // TOTAL DAYS IN MONTH
                    FixedRemuneration = Convert.ToDouble(txtFixAmt.Text);   // Daily Wages Amount
                    NewBasic = Convert.ToDouble(txtBasic.Text);   //   New Basic Amount
                    // Added new code here   Date: 02-05-2022
                    TextBox txtAdmssibleWorkDays = lvitem.FindControl("txtadmissibledays") as TextBox;
                    TextBox txtPayableHolidays = lvitem.FindControl("txtpayableholidays") as TextBox;
                    TextBox txttotalnodays = lvitem.FindControl("txttotalnodays") as TextBox;
                    TextBox txttotalpayabledays = lvitem.FindControl("txttotalpayabledays") as TextBox;
                    TextBox txtPayableDays = lvitem.FindControl("txtpayabledays") as TextBox;  //  
                    //TotalPayabledays = Convert.ToDouble(txtPayableDays.Text.Trim()); //  Payable days
                    Payabledays = Convert.ToDouble(txtPayableDays.Text.Trim()); //  Payable days
                    AdmissibleWorkingDays = Convert.ToDouble(txtAdmssibleWorkDays.Text.Trim());  // Admissible Weekly off
                    PayableHolidays = Convert.ToDouble(txtPayableHolidays.Text.Trim()); // Payable Holidays
                    TotalDays = Convert.ToDouble(txttotalnodays.Text.Trim());  //  Total Days
                    TotalPayableDays = Convert.ToDouble(txttotalpayabledays.Text.Trim()); // Total Payable Days
                    DeptId = Convert.ToInt32(ddldepratment.SelectedValue);
                    if (chkbox.Checked == true)
                    {
                        if (TotalDays > TotalDaysinMonth)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Total No of Days should not be greater than No of Days in Month!!!", this);
                            return;
                        }
                        else
                        {
                            Status = true;
                            //   int cs = UpdateDailyWagesBasicNew(0, Convert.ToDouble(txtFixAmt.ToolTip), Convert.ToDouble(txtBasic.Text), Convert.ToDouble(txtDailyDays.Text), Convert.ToDouble(txtDailyAmt.Text), StaffId, MONTHYEAR, TotalDaysinMonth, Payabledays, FixedRemuneration, NewBasic, Status, AdmissibleWorkingDays, PayableHolidays, TotalDays, TotalPayableDays, DeptId);
                            int cs = objECC.UpdateDailyWagesBasicNew(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToDouble(txtFixAmt.ToolTip), StaffId, MONTHYEAR, TotalDaysinMonth, Payabledays, FixedRemuneration, NewBasic, Status, AdmissibleWorkingDays, PayableHolidays, TotalDays, TotalPayableDays, DeptId);
                            if (cs == 2)
                            {
                                count = 1;
                            }
                        }
                    }
                }
                if (count == 1)
                {
                    BindListViewValidation();
                    //lblerror.Text = null;
                    //lblmsg.Text = "Record Updated Successfully";
                    objCommon.DisplayMessage(UpdatePanel1, "Daily Wages Attendance Saved Successfully", this);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected int checkCurrentMonthSalaryProcess()
    {
        string monYear;
        string CurrentMonYear;
        int count;
        monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
        CurrentMonYear = DateTime.Now.ToString("MMMyyyy");
        string NoDuesStatus1 = objECC.CheckMonYearExists(monYear);
        if (NoDuesStatus1 == "1")
        {
            count = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + monYear + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue) + " and SALLOCK=1 and SALNO>0"));
            return count;
        }
        else
        {
            count = 2;
            return count;
        }
    }
    protected int checkPreviousMonthSalaryProcess()
    {
        string monYear;
        int count;
        DateTime OldDate;
        OldDate = Convert.ToDateTime(txtMonthYear.Text);
        var newYear = OldDate.AddYears(-1);
        String mn = OldDate.Month.ToString();
        String yy = OldDate.Year.ToString();
        if (mn == "1")
        {
            var lastmonth1 = new DateTime(OldDate.Year - 1, 12, 1);
            monYear = Convert.ToDateTime(lastmonth1).ToString("MMM").ToUpper() + Convert.ToDateTime(lastmonth1).Year.ToString();
        }
        else
        {
            var lastmonth = new DateTime(OldDate.Year, OldDate.Month - 1, 1);
            monYear = Convert.ToDateTime(lastmonth).ToString("MMM").ToUpper() + Convert.ToDateTime(lastmonth).Year.ToString();
        }
        count = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + monYear + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue) + " and SALLOCK=1 and SALNO>0"));
        if (count == 0)
        {
            count = 2;
        }
        else
        {
            count = 3;
        }
        return count;
    }
    protected void btnunlock_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            double TotalPayabledays = 0;
            double Payabledays = 0;
            double FixedRemuneration = 0;
            double NewBasic = 0;
            int StaffId;
            DateTime MONTHYEAR;
            double TotalDaysinMonth = 0;
            bool Status;
            double AdmissibleWorkingDays = 0;
            double PayableHolidays = 0;
            double TotalDays = 0;
            double TotalPayableDays = 0;
            int DeptId = 0;
            foreach (ListViewDataItem lvitem in lvIncrement.Items)
            {
                TextBox txtFixAmt = lvitem.FindControl("txtfixamt") as TextBox;
                //TextBox txtInc = lvitem.FindControl("txtInc") as TextBox;
                CheckBox chkbox = lvitem.FindControl("chkID") as CheckBox;
                TextBox txtBasic = lvitem.FindControl("txtBasic") as TextBox;
                TextBox txtDailyDays = lvitem.FindControl("txtMonth") as TextBox;
                StaffId = Convert.ToInt32(ddlStaff.SelectedValue);  // STAFF ID
                MONTHYEAR = Convert.ToDateTime(txtMonthYear.Text.Trim()); // MON YEAR
                TotalDaysinMonth = Convert.ToDouble(txtDailyDays.Text.Trim()); // TOTAL DAYS IN MONTH
                FixedRemuneration = Convert.ToDouble(txtFixAmt.Text);   // Daily Wages Amount
                NewBasic = Convert.ToDouble(txtBasic.Text);   //   New Basic Amount
                // Added new code here   Date: 02-05-2022
                TextBox txtAdmssibleWorkDays = lvitem.FindControl("txtadmissibledays") as TextBox;
                TextBox txtPayableHolidays = lvitem.FindControl("txtpayableholidays") as TextBox;
                TextBox txttotalnodays = lvitem.FindControl("txttotalnodays") as TextBox;
                TextBox txttotalpayabledays = lvitem.FindControl("txttotalpayabledays") as TextBox;
                TextBox txtPayableDays = lvitem.FindControl("txtpayabledays") as TextBox;  //  
                //TotalPayabledays = Convert.ToDouble(txtPayableDays.Text.Trim()); //  Payable days
                Payabledays = Convert.ToDouble(txtPayableDays.Text.Trim()); //  Payable days
                AdmissibleWorkingDays = Convert.ToDouble(txtAdmssibleWorkDays.Text.Trim());  // Admissible Weekly off
                PayableHolidays = Convert.ToDouble(txtPayableHolidays.Text.Trim()); // Payable Holidays
                TotalDays = Convert.ToDouble(txttotalnodays.Text.Trim());  //  Total Days
                TotalPayableDays = Convert.ToDouble(txttotalpayabledays.Text.Trim()); // Total Payable Days
                DeptId = Convert.ToInt32(ddldepratment.SelectedValue);
                if (chkbox.Checked == true)
                {
                    Status = false;
                    //   int cs = UpdateDailyWagesBasicNew(0, Convert.ToDouble(txtFixAmt.ToolTip), Convert.ToDouble(txtBasic.Text), Convert.ToDouble(txtDailyDays.Text), Convert.ToDouble(txtDailyAmt.Text), StaffId, MONTHYEAR, TotalDaysinMonth, TotalPayabledays, FixedRemuneration, NewBasic, Status, AdmissibleWorkingDays, PayableHolidays, TotalDays, TotalPayableDays, DeptId);
                    int cs = objECC.UpdateDailyWagesBasicNew(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToDouble(txtFixAmt.ToolTip), StaffId, MONTHYEAR, TotalDaysinMonth, Payabledays, FixedRemuneration, NewBasic, Status, AdmissibleWorkingDays, PayableHolidays, TotalDays, TotalPayableDays, DeptId);
                    if (cs == 2)
                    {
                        count = 1;
                    }
                }
            }
            if (count == 1)
            {
                //lblerror.Text = null;
                BindListViewValidation();
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Unlock Attendance Successfully", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowDailWagesEmployeeAtt("Employee Attendence Report", "RptDailyWagesStaffAttendance.rpt");
    }
    private void ShowDailWagesEmployeeAtt(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;
            url += "&path=~,Reports,Payroll," + rptFileName;
            //if(ViewState["action"].Equals("salaryCertificate"))
            url += "&param=@P_MON_YEAR=" + (txtMonthYear.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaff.SelectedValue) + ",@P_IDNO=" + 0 + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPT_NO=" + Convert.ToInt32(ddldepratment.SelectedValue);
            //+ "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            //else
            //    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //Added college no in Report 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}