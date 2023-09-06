//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE PAY BILL                        
// CREATION DATE : 22-MAY-2010                                                          
// CREATED BY    : G.V.S.KIRAN KUMAR                                                  
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
public partial class PAYROLL_REPORTS_Pay_PayBill : System.Web.UI.Page
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
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = string.Empty;

            if (radAllEmployees.Checked)
            {
                idno = "0";
            }
            else
            {   

                if (radSelectedEmployees.Checked)
                {
                    string employeelist = string.Empty;
                
                    for (int i = 0; i < lstEmployee.Items.Count; i++)
                    {
                        if (lstEmployee.Items[i].Selected)
                        {
                            employeelist += lstEmployee.Items[i].Value + ",";
                        }
                    }

                    employeelist = employeelist.Substring(0, employeelist.Length - 1);
                    idno = employeelist;
                }
                
            }

            string param = this.GetParams(idno,ddlMonthYear.SelectedValue,Convert.ToInt32(ddlStaffNo.SelectedValue));
            this.ShowReport(param,"Employee_PayBill","Pay_PayBill.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?Pay_PayBill.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PayBill.aspx");
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
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL MONTH YEAR 
            objCommon.FillSalfileDropDownList(ddlMonthYear);

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //select PAYHEAD,PAYSHORT from payroll_payhead  where srno>15 and payshort<>'' or payshort<>null

            //objCommon.FillDropDownList(ddlPayhead, "payroll_payhead", "PAYHEAD", "PAYSHORT", "srno>15 and payshort<>'' or payshort<>null", "PAYHEAD");

            //FILL EMPLOYEE
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO>0", "IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillEmployee()
    {
        try
        {
            objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", "'[' + convert(nvarchar(50),idno) +']'+ ' ' + isnull(fname,'') + isnull(mname,'') + isnull(lname,'') as employeename", "STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue), "IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void radSelectedEmployees_CheckedChanged(object sender, EventArgs e)
    {
        tremployee.Visible = true;
        FillEmployee();
    }

    protected void radAllEmployees_CheckedChanged(object sender, EventArgs e)
    {
        tremployee.Visible = false;
    }

    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            //string pfileNo = objCommon.LookUp("payroll_empmas", "pfileno", "idno=" + Convert.ToInt32(ddlEmployee.SelectedValue));
            //string IP = Request.ServerVariables["REMOTE_HOST"];
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PayRoll," + rptFileName;
            url +=  "&param="+param;
            ScriptManager.RegisterClientScriptBlock(updmain, updmain.GetType(), "Message", " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetParams(string idno, string monYear, int staffno)
    {
        string param;
        param = string.Empty;

        //param += "@P_IDNO=" + idno + "*rptPaybillSummaryReport.rpt" + ",@P_MON_YEAR=" + monYear + "*rptPaybillSummaryReport.rpt" +",@P_STAFF_NO=" + staffno + "*rptPaybillSummaryReport.rpt";

        param += "@P_IDNO=" + idno + "*Pay_Paybill_Earnings.rpt" + ",@P_MON_YEAR=" + monYear + "*Pay_Paybill_Earnings.rpt" + ",@P_STAFF_NO=" + staffno + "*Pay_Paybill_Earnings.rpt";

        //param += ",@P_IDNO=" + idno + "*rptPaybillInstructionsReport.rpt" + ",@P_MON_YEAR=" + monYear + "*rptPaybillInstructionsReport.rpt" + ",@P_STAFF_NO=" + staffno + "*rptPaybillInstructionsReport.rpt";

        return param;
    }

}
