//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE PAYMENT SLIP AND SALARY CERTIFICATE                        
// CREATION DATE : 28-AUGAST-2009                                                          
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

public partial class PayRoll_Pay_PaySlip : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int OrganizationId;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    int employeelogin = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (ua_type != 1)
        {
            employeelogin = 1;
        }
         
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
                if (ua_type != 1)
                {
                    //trCertificate.Visible = false;
                    trrbl.Visible = false;
                    btnSalaryCertificate.Visible = false;
                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    ddlCollege.SelectedIndex = 1;
                    ddlStaffNo.SelectedIndex = 1;
                    ddlEmployeeNo.SelectedIndex = 1;
                    ddlEmployeeType.SelectedIndex = 1;
                    OrganizationId = Convert.ToInt32(Session["OrgId"]);
                }
                else
                {
                    PopulateDropDownList();
                    OrganizationId = Convert.ToInt32(Session["OrgId"]);
                }

                //Populate DropdownList
                //PopulateDropDownList();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowWholeEmployeePayslip(string reportTitle, string rptFileName)
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
        else
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreportNew.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&format=No";
        url += "&path=~,Reports,Payroll," + rptFileName;
      //  url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=0,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
        url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=0,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Report", Script, true);


       
    }

    private void ShowWholeEmployeePayslipForCresent(string reportTitle, string rptFileName)
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
        else
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreportNew.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&format=No";
        url += "&path=~,Reports,Payroll," + rptFileName;
        //url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=0,@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=0,@P_COLLEGE_NO=0";
        url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=0,@P_COLLEGE_NO=0";
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Report", Script, true);

    }

    private void  ShowReportEmployeePayslip(string reportTitle, string rptFileName)
    {
        try
        {
           // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
           // //url += "Reports/CommonReport.aspx?";
           //// url += "Reports/commonreportNew.aspx?";
           // url += "path=" + reportTitle;
           // url += "&pathForEmployeePaySlip=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + "&@P_EMPTYPENO=0&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);

           // //if(ViewState["action"].Equals("salaryCertificate"))
           // url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString() + ",Bank_Name=" + txtBankName.Text.ToString();
           // //else
           // //    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();

           // divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
           // divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
           // divMsg.InnerHtml += " </script>";

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
            else 
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
            }

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/commonreportNew.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&format=No";
            url += "&path=~,Reports,Payroll," + rptFileName;
           // url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=0,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",username=" + Session["username"].ToString() + ",Bank_Name=" + txtBankName.Text.ToString();
            url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=0,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",username=" + Session["username"].ToString() + ",Bank_Name=" + txtBankName.Text.ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    //private void ShowReportEmployeePayslip(string reportTitle, string rptFileName)
    // {
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;       
    //        url += "&path=~,Reports,Payroll," + rptFileName;          
    //        url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "SalaryCerticaicate";
            string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "IDCardType='" + Type + "' AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
   
            if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select Employee.");
                return;
            }
           // ViewState["action"] = "payslip";
           // ShowReportEmployeePayslip("Employee_PaySlip","rptEmployee_SalarySlip.rpt");
           // ShowReportEmployeePayslip("Employee_PaySlip", "PayrollPaySlip.rpt");
           // ShowReportEmployeePayslip("Employee_PaySlip", "EmployeePaySlipNew.rpt");
            if (ReportName != "")
            {
                if (employeelogin == 1)
                {

                    int chksalaryprocess = Convert.ToInt32(objCommon.LookUp("" + ddlMonthYear.SelectedItem.Text + "", "COUNT(*)", "IDNO=" + ddlEmployeeNo.SelectedValue + ""));
                    if (chksalaryprocess == 0)
                    {
                        objCommon.DisplayMessage("Salary not processed for " + ddlMonthYear.SelectedItem.Text + " month.", this.Page);
                    }
                    else
                    {
                        int empstaffno =Convert.ToInt32(objCommon.LookUp("" + ddlMonthYear.SelectedItem.Text + "", "STAFFNO", "IDNO=" + ddlEmployeeNo.SelectedValue + ""));
                        int empcollegeno = Convert.ToInt32(objCommon.LookUp("" + ddlMonthYear.SelectedItem.Text + "", "COLLEGE_NO", "IDNO=" + ddlEmployeeNo.SelectedValue + ""));
                        bool salarylockstatus = Convert.ToBoolean(objCommon.LookUp("PAYROLL_SALFILE", "SALLOCK", "MONYEAR = '" + ddlMonthYear.SelectedItem.Text + "' AND COLLEGE_NO=" + empcollegeno + " AND STAFFNO = " + empstaffno + ""));
                        if (salarylockstatus == true)
                        {

                            // ShowWholeEmployeePayslip("Employee_PaySlip", "rptEmployee_SalarySlip.rpt");
                            ShowWholeEmployeePayslipForCresent("Employee_PaySlip", ReportName);

                        }
                        else
                        {
                            objCommon.DisplayMessage("Salary not locked for " + ddlMonthYear.SelectedItem.Text + " month.", this.Page);
                        }
                    }
                }
                else
                {
                    //  ShowWholeEmployeePayslip("Employee_PaySlip", "rptEmployee_SalarySlip.rpt");
                    ShowWholeEmployeePayslip("Employee_PaySlip", ReportName);
                }
            }
            else
            {
                //  ShowWholeEmployeePayslip("Employee_PaySlip", "rptEmployee_SalarySlip.rpt");
                ShowWholeEmployeePayslip("Employee_PaySlip", "rptEmployee_SalarySlip.rpt");
            }
          // ShowReportEmployeePayslip("Employee_PaySlip", "rptEmployee_SalarySlipNew.rpt");
            
        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?Pay_PaySlip.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PaySlip.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSalaryCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select Employee.");
                return; 
            }

            ShowReportEmployeePayslip("Employee_PaySlip", "rptSalaryCertificate.rpt");
           // ShowReportSalaryCertificate("Employee_PaySlip", "rptSalaryCertificateNew.rpt");
        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnSalaryCertificate_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowReportSalaryCertificate(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",Bank_Name=" + txtBankName.Text.ToString(); ;
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
    protected void PopulateDropDownList()
     {
        try
        {           

            //FILL MONTH YEAR 

            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");

            //objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "SALNO", "MONYEAR", "SALNO>0", "SALNO");

            // objCommon.FillSalfileDropDownList(ddlMonthYear);
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillListBox(ddlStaffNo1, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL EMPLOYEE
           // objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),PFILENO) + ']'", "IDNO>0 AND STAFFNO="+ddlStaffNo.SelectedValue+" AND COLLEGE_NO="+ddlCollege.SelectedValue, "FNAME");

            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void rdoAllEmployee_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rdoAllEmployee.Checked)
    //        {
    //            ddlEmployeeNo.SelectedIndex = 0;
    //            ddlEmployeeNo.Enabled = false;
    //        }
    //        else
    //        {
    //            ddlEmployeeNo.Enabled = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //      if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.rdoAllEmployee_CheckedChanged()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void rdoSelectEmployee_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rdoSelectEmployee.Checked)
    //        {
    //            ddlEmployeeNo.Enabled = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //       if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.rdoSelectEmployee_CheckedChanged()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void ddlEmployeeNo_SelectedIndexChanged(object sender, EventArgs e)
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
        ddlEmployeeType.Enabled = false;
        
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
        int emptypeno = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "EMPTYPENO", "idno=" + IDNO));
        int collegeNo =Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + IDNO));
        DateTime StartDate = Convert.ToDateTime(objCommon.LookUp("Payroll_pay_ref", "EmpPayslipShowFromDate",""));
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID=" + collegeNo, "COLLEGE_ID ASC");            

            //FILL MONTH YEAR 
           // objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103)");
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0 AND SALLOCK = 1 and Cast(monyear as date) >=cast('" + StartDate + "' as date) ", "convert(datetime,monyear,103) DESC");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");
            objCommon.FillListBox(ddlStaffNo1, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");

            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),EmployeeId) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");

            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO="+ emptypeno, "EMPTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStaffNo.SelectedValue == "0")
        {
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0  AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0", "FNAME");
            }
        }
        else
        {
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue, "FNAME");

            }
        }     
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedValue == "0")
        {
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + ",";
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
            if (count > 0)
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO in (" + stafflist + ")", "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0", "FNAME");
            }
        }
        else
        {
            int count = 0;
            string stafflist = string.Empty;
            for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
            {
                if (ddlStaffNo1.Items[i].Selected)
                {
                    stafflist += ddlStaffNo1.Items[i].Value + ",";
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
            if (count  > 0)
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO in ( " + stafflist + ") AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0  AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
        }

     
    }
    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (employeelogin == 0)
        //{
        //    if (ddlCollege.SelectedValue == "0")
        //    {
        //        if (Convert.ToInt32(ddlStaffNo.SelectedValue) > 0)
        //        {

        //            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue, "FNAME");

        //        }
        //        else
        //        {
        //            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0", "FNAME");
        //        }
        //    }
        //    else
        //    {
        //        if (Convert.ToInt32(ddlStaffNo.SelectedValue) == 0)
        //        {
        //            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

        //        }
        //        else if (Convert.ToInt32(ddlStaffNo.SelectedValue) > 0)
        //        {
        //            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
        //        }

        //    }
        //    if (Convert.ToInt32(ddlStaffNo.SelectedValue) == 0 && Convert.ToInt32(ddlCollege.SelectedValue) == 0)
        //    {
        //        objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0", "FNAME");
        //    }
        //}
       
    }
    protected void btnAnnualPaySlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
               // ShowMessage("Please Select Employee.");
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee.", this.Page);

                return;
            }
            if (txtFromDate.Text == string.Empty && txtToDate.Text == string.Empty)
            {
              //  ShowMessage("Please Select Date.");
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter From Date and To Date", this.Page);

                return;
            }


            ShowReportEmployeePayslipBtnDates("Employee_PaySlip", "rptEmployee_SalarySlip_Annual.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReportEmployeePayslipBtnDates(string reportTitle, string rptFileName)
    {
        try
        {

            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
            Fdate = Fdate.Substring(0, 10);

            string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text)));
            Ldate = Ldate.Substring(0, 10);


            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitleForEmployeePaySlipbtndt=" + reportTitle;
            //// url += "&pathForEmployeePaySlip=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            //url += "&pathForEmployeePaySlipbtndt=~,Reports,Payroll," + rptFileName + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_COLLEGE_NO="+Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + "&@FROM_DATE=" + Fdate + "&@TO_DATE=" + Ldate + "";
            ////if(ViewState["action"].Equals("salaryCertificate"))
            //url += "&paramForEmployeePaySlipbtndt=username=" + Session["username"].ToString() + ",Bank_Name=" + txtBankName.Text.ToString() + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_NO="+Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@FROM_DATE=" + Fdate + ",@TO_DATE=" + Ldate;
            ////else
            ////    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@FROM_DATE=" + Fdate + ",@TO_DATE=" + Ldate;
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
    protected void btnThreeMonthPaySlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex == 0)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select College", this.Page);
                return;
            }

            if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
                // ShowMessage("Please Select Employee.");
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee.", this.Page);

                return;
            }
           

            ShowThreeMonthSalaryStatements("Three Month Salary Statements", "rptEmployee_SalarySlip_ThreeMonth.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnThreeMonthPaySlip_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowThreeMonthSalaryStatements(string reportTitle, string rptFileName)
    {

        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreportNew.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&format=No";
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_MonthYear=" + (ddlMonthYear.SelectedItem.Text) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();   // @P_COLLEGE_CODE=" + Session["colcode"].ToString();
      
        
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Report", Script, true);
        
           //  Session["colcode"].ToString(); 
    }

    protected void btnSixmonthsPaySlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex == 0)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select College", this.Page);
                return;
            }
            if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            {
                // ShowMessage("Please Select Employee.");
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Employee.", this.Page);

                return;
            }
            

            ShowSixMonthSalaryStatements("Six Month Salary Statements", "rptEmployee_SalarySlip_SixMonth.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnSixmonthsPaySlip_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowSixMonthSalaryStatements(string reportTitle, string rptFileName)
    {
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreportNew.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&format=No";
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_MonthYear=" + (ddlMonthYear.SelectedItem.Text) + ",@P_COLLEGE_CODE="+Session["colcode"].ToString();
         //   + Session["colcode"].ToString(); // @P_COLLEGE_CODE=" + Session["colcode"].ToString();
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "Report", Script, true);
    }
    protected void ddlStaffNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        string stafflist = string.Empty;
        for (int i = 0; i < ddlStaffNo1.Items.Count; i++)
        {
            if (ddlStaffNo1.Items[i].Selected)
            {
                stafflist += ddlStaffNo1.Items[i].Value + ",";
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
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0  AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0", "FNAME");
            }
        }
        else if(count > 0)
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO IN (" + stafflist + ") AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO IN (" + stafflist + ")", "FNAME");
            }
        } 
    }
}
