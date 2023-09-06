//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE ANNUAL PAYMENT SLIP                     
// CREATION DATE : 28-06-2022                                                       
// CREATED BY    : PURVA RAUT                                                
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

public partial class PAYROLL_REPORTS_PayAnnualSlipReport : System.Web.UI.Page
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
                   // CheckPageAuthorization();

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
                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    ddlCollege.SelectedIndex = 1;
                    ddlStaffNo.SelectedIndex = 1;
                    ddlEmployeeNo.SelectedIndex = 1;
                   
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
                objUCommon.ShowError(Page, "PayRoll_PayAnnualSlipReport.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
    protected void PopulateDropDownList()
    {
        try
        {

            //FILL MONTH YEAR 

             //objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "SALNO", "MONYEAR", "SALNO>0", "SALNO");

            // objCommon.FillSalfileDropDownList(ddlMonthYear);
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL EMPLOYEE
            // objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),PFILENO) + ']'", "IDNO>0 AND STAFFNO="+ddlStaffNo.SelectedValue+" AND COLLEGE_NO="+ddlCollege.SelectedValue, "FNAME");

            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

          }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void PopulateDropDownListForFaculty()
    {
        ddlCollege.Enabled = false;
        ddlStaffNo.Enabled = false;
      
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
        int emptypeno = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "EMPTYPENO", "idno=" + IDNO));
        int collegeNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + IDNO));
        DateTime StartDate = Convert.ToDateTime(objCommon.LookUp("Payroll_pay_ref", "EmpPayslipShowFromDate", ""));
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID=" + collegeNo, "COLLEGE_ID ASC");
            //FILL MONTH YEAR 
            // objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103)");
            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");
            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),EmployeeId) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");

         }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            if (Convert.ToInt32(ddlStaffNo.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue, "FNAME");

            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0", "FNAME");
  
            }
        }
        else
        {
            if (Convert.ToInt32(ddlStaffNo.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0  AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");
            }
        }

     
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
}