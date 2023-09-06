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


public partial class PayRoll_Annual_Salary_Detail : System.Web.UI.Page
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

                //Focus on From Date Textbox
                txtFromDate.Focus();

                //Enable Dropdown
                ddlStaffNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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

            //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO>0", "IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportAnnualSalaryDetail(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PayRoll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitleForAnnualSalaryDetail=" + reportTitle;
            url += "&pathForAnnualSalaryDetail=~,Reports,Payroll," + rptFileName + "&@P_FROM_DATE=" + txtFromDate.Text.Trim() + "&@P_TO_DATE=" + txtToDate.Text.Trim() + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONTH=" + (lstMonth.SelectedItem.Text);
            url += "&paramForAnnualSalaryDetail=username=" + Session["username"].ToString() + ",From_Date=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",To_Date=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",All_Month=" + (lstMonth.SelectedItem.ToString());
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.ShowReportAnnualSalaryDetail() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoParticularEmployee.Checked && ddlEmployeeNo.SelectedIndex==0)
            {
                ShowMessage("Please Select Employee");
                return;
            }
            if (rdoAllEmployee.Checked && ddlStaffNo.SelectedIndex == 0)
            {
                ShowMessage("Please Select Staff No.");
                return;
            }
            if (rdoParticularEmployee.Checked)
            {
                ShowReportAnnualSalaryDetail("Annual_Salary_Detail", "rptAnnual_Salary_Deatil_For_Single_Emp.rpt");
            }
            else
            {
                ShowReportAnnualSalaryDetail("Annual_Salary_Detail", "rptAnnual_Salary_Detail.rpt");
            }
        }
        catch (Exception ex)
        {
         if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.btnShowReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Annual_Salary_Detail.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Annual_Salary_Detail.aspx");
        }
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime dt2 = Convert.ToDateTime(txtToDate.Text.Trim());
            TimeSpan ts = dt1.Subtract(dt2);
            int diffMonth = Math.Abs((dt2.Year - dt1.Year) * 12 + dt1.Month - dt2.Month);
            if (diffMonth > 12)
            {
                ShowMessage("Date difference not more than 12 month.");
            }
            else
            {
                //Fill FillMonth()
                FillMonth();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.txtToDate_TextChanged() --> " + ex.Message + " " + ex.StackTrace);
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
                 objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillMonth()
    {
        try
        {
            //This method is for filling the Month in Pay_Annual_Salary_Detail Report page on From_Date to To_Date

            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            SqlParameter[] objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_FROM_DATE",Convert.ToDateTime(txtFromDate.Text.Trim()));
            objParams[1] = new SqlParameter("@P_TO_DATE", Convert.ToDateTime(txtToDate.Text.Trim()));
            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_FILL_MONTH", objParams);
            lstMonth.Items.Clear();
            lstMonth.Items.Add(new ListItem("Please Select", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lstMonth.DataSource = ds;
                lstMonth.DataTextField = "MONYEAR";
                lstMonth.DataValueField = "MONYEAR";
                lstMonth.DataBind();
                lstMonth.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.FillMonth() --> " + ex.Message + " " + ex.StackTrace);
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
