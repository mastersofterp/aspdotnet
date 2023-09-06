//======================================================================================
// PROJECT NAME  : NITA                                                                
// MODULE NAME   : PAYROLL MULTIPLE MONTH SALARY                       
// CREATION DATE : 10-May-2011                                                          
// CREATED BY    : ANKIT AGRAWAL                                               
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


public partial class MultipleMonthsSalaryReport : System.Web.UI.Page
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
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
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

                if (ua_type != 1)
                {

                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    ddlStaffNo.SelectedIndex = 1;
                    ddlEmployeeNo.SelectedIndex = 1;

                }
                else
                {
                    PopulateDropDownList();
                }
                //Populate DropdownList
                //PopulateDropDownList();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME+' '+MNAME+' '+LNAME)", "IDNO>0 and STAFFNO ="+ddlStaffNo.SelectedValue, "IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownListForFaculty()
    {
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

        try
        {


            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");

            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?MultipleMonthsSalaryReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MultipleMonthsSalaryReport.aspx");
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
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
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
            url += "&param=@Username=" + Session["username"].ToString() + ",@P_FROMDATE=" + (txtFromDate.Text) + ",@P_TODATE=" + (txtToDate.Text) + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_PageWiseTotal=1";
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ChkDateDiff() == false)
            {
                ShowReportEmployeeAbstractSalary("MultipleMonthsSalaryReport", "Pay_Salary_StatementReport.rpt");
            }
            else
            {
                objUCommon.ShowError(Page, "Difference Between From Date And To Date Cannot Be More Than 12 Months");
            }
        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.btnRegisterWithAbstract_Click() --> " + ex.Message + " " + ex.StackTrace);
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
    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME+' '+MNAME+' '+LNAME)", "IDNO>0 and STAFFNO =" + ddlStaffNo.SelectedValue, "IDNO");
    }
}
