//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL ANNUAL PAY HEAD WISE REPORT                       
// CREATION DATE : 07-02-2023                                                         
// CREATED BY    : Purva Raut                                                 
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


public partial class PAYROLL_REPORTS_PayAnnualPayHeadReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
        int ua_type = Convert.ToInt32(Session["usertype"]);
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

                //Populate DropdownList
                if (ua_type != 1)
                {
                    //trCertificate.Visible = false;
                    trRadioBtnEmployee.Visible = false;
                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    //Fill Listbox
                    FillPayhead();
                    ddlCollege.SelectedIndex = 1;
                    ddlStaffNo.SelectedIndex = 1;
                    ddlEmployeeNo.SelectedIndex = 1;

                }
                else
                {
                    PopulateDropDownList();
                    //Fill Listbox
                    FillPayhead();
                }
                txtFromDate.Focus();
               // ddlStaffNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_PayAnnualPayHeadReport.aspx.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?PayAnnualPayHeadReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayAnnualPayHeadReport.aspx");
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
            int clgcode;
            clgcode = Convert.ToInt32(Session["colcode"]);
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
    protected void PopulateDropDownListForFaculty()
    {
        ddlCollege.Enabled = false;
        ddlStaffNo.Enabled = false;

        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
        int collegeNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + IDNO));

        try
        {
         //   objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO=" + collegeNo, "COLLEGE_NO ASC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
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
                objUCommon.ShowError(Page, "PayRoll_PayAnnualPayHeadReport.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            //if (rdoParticularEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
            //{
            //    ShowMessage("Please Select Employee.");
            //    return;
            //}
            //if (rdoAllEmployee.Checked && ddlStaffNo.SelectedIndex == 0)
            //{
            //    ShowMessage("Please Select Select.");
            //    return;
            //}

            int itemselectcnt = 0;
            if (lstParticularColumn.Items.Count > 0)
            {
                for (int i = 0; i < lstParticularColumn.Items.Count; i++)
                {
                    if (lstParticularColumn.Items[i].Selected)
                    {
                        itemselectcnt = itemselectcnt + 1;
                    }
                }
                if (itemselectcnt == 1)
                {
                  //  ShowReportPayhead("Report_For_Particular_Column_Payhead", "Other_Selected_Fields_Single_Payhead.rpt");
                    ShowReportSinglePayHead("Report_For_Particular_Column_Payhead", "Other_Selected_Fields_Single_Payhead_Annual.rpt");
                }
                else if (itemselectcnt == 2)
                {
                   // ShowReport("Report_For_Particular_Column_Payhead", "Other_Selected_Fields.rpt");
                    ShowReportMultiplePayHead("Report_For_Particular_Multiple_Payhead", "Other_Selected_Fields_Multiple_Payhead_Annual.rpt");
                }
                else if (itemselectcnt == 3)
                {
                    ShowMessage("Select Maximum Two Heads");
                    return;
                }
                else if (itemselectcnt == 0)
                {
                    ShowMessage("Select Atleast One Pay Head");
                    return;
                }
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
    private void ShowReportSinglePayHead(string reportTitle, string rptFileName)
    {
        try
        {
            string selectedItem = string.Empty;
            string selectedText = string.Empty;
            int d = 0;
            string b = "0";
            string c = "0";
            int count = 0;
            string shoreport = string.Empty;
            if (lstParticularColumn.Items.Count > 0)
            {
                for (int i = 0; i < lstParticularColumn.Items.Count; i++)
                {
                    if (lstParticularColumn.Items[i].Selected)
                    {
                        //selectedItem =selectedItem + lstParticularColumn.Items[i].Text.Trim() + ",";
                        selectedItem = selectedItem + lstParticularColumn.Items[i].Value.ToString().Trim() + "$";
                        selectedText = selectedText + lstParticularColumn.Items[i].Text.Trim() + ",";
                        //insert command
                        d = d + 1;
                        count++;
                    }
                }
                selectedItem = selectedItem.Remove(selectedItem.Length - 1);
                selectedText = selectedText.Remove(selectedText.Length - 1);
                string[] a = selectedText.Split(',');
                if (d == 1)
                {
                    b = a[0];
                }
                else if (d == 2)
                {
                    b = a[0];
                    c = a[1];
                }
                else if (d == 3)
                {
                    ShowMessage("Select Maximum Two Heads");
                    return;
                }
                if (count == 0)
                {
                    ShowMessage("Select Maximum Two Heads");
                    return;
                }
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Payroll," + rptFileName;
                //url += "&param=@Username=" + Session["username"].ToString() + ",@P_FROMDATE=" + (txtFromDate.Text) + ",@P_TODATE=" + (txtToDate.Text) + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPLOYEETYPE_NO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",@P_PageWiseTotal=1";
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_PAYHEADS=" + selectedItem + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) +",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue)+",@FROM_DATE=" + (txtFromDate.Text) + ",@TO_DATE=" + (txtToDate.Text)+",@P_HEAD1="+b+",Username=" + Session["username"].ToString();
                 divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportMultiplePayHead(string reportTitle, string rptFileName)
    {
        try
        {
            string selectedItem = string.Empty;
            string selectedText = string.Empty;
            int d = 0;
            string b = "0";
            string c = "0";
            int count = 0;
            string shoreport = string.Empty;
            if (lstParticularColumn.Items.Count > 0)
            {
                for (int i = 0; i < lstParticularColumn.Items.Count; i++)
                {
                    if (lstParticularColumn.Items[i].Selected)
                    {
                        //selectedItem =selectedItem + lstParticularColumn.Items[i].Text.Trim() + ",";
                        selectedItem = selectedItem + lstParticularColumn.Items[i].Value.ToString().Trim() + "$";
                        selectedText = selectedText + lstParticularColumn.Items[i].Text.Trim() + ",";
                        //insert command
                        d = d + 1;
                        count++;
                    }
                }
                selectedItem = selectedItem.Remove(selectedItem.Length - 1);
                selectedText = selectedText.Remove(selectedText.Length - 1);
                string[] a = selectedText.Split(',');
                if (d == 1)
                {
                    b = a[0];
                }
                else if (d == 2)
                {
                    b = a[0];
                    c = a[1];
                }
                else if (d == 3)
                {
                    ShowMessage("Please Select Atleast Two Heads");
                    return;
                }
                if (count == 0)
                {
                    ShowMessage("Please Select Atleast Two Heads");
                    return;
                }
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Payroll," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PAYHEADS=" + selectedItem + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@FROM_DATE=" + (txtFromDate.Text) + ",@TO_DATE=" + (txtToDate.Text) + ",@P_HEAD1=" + b +",@P_HEAD2="+ c +",Username=" + Session["username"].ToString();
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MultipleMonthsSalaryReport.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
}