//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL SELECTED FIELDS
// CREATION DATE : 16-September-2009                                                          
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


public partial class PayRoll_Pay_SelectedField : System.Web.UI.Page
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

                //Fill Listbox
                FillPayhead();

                //Select list particular Head
                lstParticularColumn.SelectedIndex = 0;

                //Get focus on Textbox
                txtFromDate.Focus();

                //Staff No. DropdownList Enabled False
                ddlStaffNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?Pay_SelectedField.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SelectedField.aspx");
        }
    }

    private void FillPayhead()
    {
        try
        {
            //This method is for filling the Payhead in Pay_SelectedField Report page on To_Date textbox Click
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_COLLEGE_CODE", Session["colcode"].ToString());
            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_DROPDOWN_PAYHEADS", objParams);
            lstParticularColumn.Items.Clear();
            lstParticularColumn.Items.Add(new ListItem("Please Select", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lstParticularColumn.DataSource = ds;
                lstParticularColumn.DataTextField = "PAYSHORT";
                lstParticularColumn.DataValueField = "PAYHEAD";
                lstParticularColumn.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.FillPayhead() --> " + ex.Message + " " + ex.StackTrace);
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
            int diffMonth = Math.Abs((dt2.Year - dt1.Year)*12 + dt1.Month - dt2.Month);
            
            if (diffMonth > 12)
            {
                ShowMessage("Date difference not more than 12 month.");
            }
            else
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_FROM_DATE", txtFromDate.Text.Trim());
                objParams[1] = new SqlParameter("@P_TO_DATE", txtToDate.Text.Trim());
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
            
        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.txtToDate_TextChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportColumnPayHead(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitleColumnPayhead=" + reportTitle;
            url += "&pathColumnPayhead=~,Reports,Payroll," + rptFileName + "&@P_FROM_DATE=" + txtFromDate.Text.Trim() + "&@P_TO_DATE=" + txtToDate.Text.Trim() + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_PAYHEAD=" + (lstParticularColumn.SelectedValue) + "&@P_MONTH=" + (lstMonth.SelectedItem.ToString());
            url += "&paramColumnPayhead=username=" + Session["username"].ToString() + ",From_Date=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",To_Date=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",Month1=" + "JAN" + ",Month2=" + "FEB" + ",Month3=" + "MAR" + ",Month4=" + "APR" + ",Month5=" + "MAY" + ",Month6=" + "JUN" + ",Month7=" + "JUL" + ",Month8=" + "AUG" + ",Month9=" + "SEP" + ",Month10=" + "OCT" + ",Month11=" + "NOV" + ",Month12=" + "DEC";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.ShowReportColumnPayHead() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string param,string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",From_Date=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",To_Date=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_PAYHEAD=" + lstParticularColumn.SelectedValue + ",@P_MONTH=" + param + ",MONTH1=" + "JAN" + ",MONTH2=" + "FEB" + ",MONTH3=" + "MAR" + ",MONTH4=" + "APR" + ",MONTH5=" + "MAY" + ",MONTH6=" + "JUN" + ",MONTH7=" + "JUL" + ",MONTH8=" + "AUG" + ",MONTH9=" + "SEP" + ",MONTH10=" + "OCT" + ",MONTH11=" + "NOV" + ",MONTH12=" + "DEC" + ",Payhead=" + lstParticularColumn.SelectedItem.Text;
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

    private string listMonth()
    {
        string listOfMonth = string.Empty;
        try
            {
                {
                    for (int i = 0; i < lstMonth.Items.Count; i++)
                    {
                        if (lstMonth.SelectedIndex==0)
                        {
                            listOfMonth = string.Empty;
                            return listOfMonth;
                        }
                        else
                        {
                            if (lstMonth.Items[i].Selected)
                            {
                                listOfMonth += lstMonth.Items[i].Value + ".";
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
                ShowMessage("Please Select Staff.");
                return;
            }
            //ShowReport Method to Display Report
           // ShowReportColumnPayHead("Particular_Column_Payhead", "rptSelected_Field1.rpt");

            ShowReport(listMonth(),"Report_For_Particular_Column_Payhead", "rptSelected_Field.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.btnShowReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lstMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
          if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.lstMonth_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
}
