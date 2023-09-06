//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL ABSTRACT SALARY                       
// CREATION DATE : 27-September-2018                                                          
// CREATED BY    : ROHIT MASKE                                                  
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

public partial class PAYROLL_REPORTS_Pay_AstractSalary_ForMultipleStaff : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string itemselectcnt = string.Empty;

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
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Populate DropdownList
                PopulateDropDownList();
            }
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
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");
            // College Name
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            //FILL STAFF
            //  FillStaff();
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //ListBox for Staff 
    //private void FillStaff()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO<>0", "STAFFNO");
    //        lstStaffName.Items.Clear();
    //        //lstStaffName.Items.Add(new ListItem("Please Select", "0"));

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lstStaffName.DataSource = ds;
    //            lstStaffName.DataTextField = "STAFF";
    //            lstStaffName.DataValueField = "STAFFNO";
    //            lstStaffName.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Pay_SubPayHead_Reort.FillPayhead() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

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

    protected void btnRegisterWithAbstract_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary_MultipleStaff.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_AstractSalary_ForMultipleStaff.btnRegisterWithAbstract_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnKannadaReport_Click(object sender, EventArgs e)
    {
        ShowReportEmployeeAbstractSalaryKannada("Employee_Abstract_Salary", "rptAbstract_Salary_UNICODE_BEC.rpt");
    }

    private void ShowReportEmployeeAbstractSalaryKannada(string reportTitle, string rptFileName)
    {
        try
        {
            // Get selected Staff from List
            // GetStaff();
            int college_code = Convert.ToInt32(objCommon.LookUp("reff", "College_code", ""));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;


            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + ddlStaffNo.SelectedValue + ",@P_IDNO=" + 0 + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_COLLEGE_CODE=" + college_code + ",@P_REPORTHEADING=" + txtReportHeadingKannada.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_AstractSalary_ForMultipleStaff.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // Non Plan,85% and 15% Gross report
    protected void btnNonPlan_Click(object sender, EventArgs e)
    {
        ShowNonPlanGrossReport("NonPlanReport", "PAY_NON_PLAN_REPORT.rpt");
    }

    protected void btn85Per_Click(object sender, EventArgs e)
    {
        ShowNonPlanGrossReport("NonPlanReport", "PAY_EIGHTY_FIVE_PER_GROSS_REPORT.rpt");
    }

    protected void btn15Per_Click(object sender, EventArgs e)
    {
        ShowNonPlanGrossReport("NonPlanReport", "PAY_FIFTEEN_PER_GROSS_REPORT.rpt");
    }

    private void ShowNonPlanGrossReport(string reportTitle, string rptFileName)
    {
        try
        {
            // Get selected Staff from List
            // GetStaff();
            int college_code = Convert.ToInt32(objCommon.LookUp("reff", "College_code", ""));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_COLLEGE_CODE=" + college_code + ",@P_STAFF_NO=" + ddlStaffNo.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_AstractSalary_ForMultipleStaff.ShowNonPlanGrossReport() --> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportEmployeeAbstractSalary(string reportTitle, string rptFileName)
    {
        try
        {
            // Get selected Staff from List
            // GetStaff();
            int college_code = Convert.ToInt32(objCommon.LookUp("reff", "College_code", ""));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";

            url += "pagetitleForEmployeeAbstractSalaryMultipleStaff=" + reportTitle;
            //url += "&pathForEmployeeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_IDNO=0";
            url += "&pathForEmployeeAbstractSalaryMultipleStaff=~,Reports,Payroll," + rptFileName + "&@P_COLLEGE_CODE=" + college_code + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_IDNO=" + "0" + "&@P_COLLEGE_NO=" + ddlCollege.SelectedValue + "";
            url += "&paramForEmployeeAbstractSalaryMultipleStaff=username=" + Session["username"].ToString();

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

    //private void GetStaff()
    //{
    //    try
    //    {
    //        if (lstStaffName.Items.Count > 0)
    //        {
    //            for (int i = 0; i < lstStaffName.Items.Count; i++)
    //            {
    //                if (lstStaffName.Items[i].Selected)
    //                {
    //                    itemselectcnt += lstStaffName.Items[i].Value + ",";
    //                }
    //            }
    //        }

    //        itemselectcnt = itemselectcnt.TrimEnd(',');
    //        itemselectcnt = itemselectcnt.Replace(',', '_');
    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}

    //Export to Excel Add by Rohit Maske 04-10-2018

    #region Export to Excel

    protected void btnExcel_Click(object sender, EventArgs e)
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
            lblCaption.Text = "Salary Register Kannada";
            Payroll_Report_Controller objPC = new Payroll_Report_Controller();
            int idno = 0;
            DataSet ds = objPC.GetSalaryRegisterExcel(ddlMonthYear.SelectedItem.Text, Convert.ToInt32(ddlStaffNo.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Session["colcode"].ToString(), idno, txtReportHeadingKannada.Text);
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
            //Label lblh7 = grdSelectFieldReport.FindControl("lblEH7") as Label; lblh7.Text = ds.Tables[0].Rows[0]["HEADEARNING7"].ToString();
            Label lblh8 = grdSelectFieldReport.FindControl("lblEH8") as Label; lblh8.Text = ds.Tables[0].Rows[0]["HEADEARNING8"].ToString();
            //Label lblh9 = grdSelectFieldReport.FindControl("lblEH9") as Label; lblh9.Text = ds.Tables[0].Rows[0]["HEADEARNING9"].ToString();
            //Label lblh10 = grdSelectFieldReport.FindControl("lblEH10") as Label; lblh10.Text = ds.Tables[0].Rows[0]["HEADEARNING10"].ToString();
            //Label lblh11 = grdSelectFieldReport.FindControl("lblEH11") as Label; lblh11.Text = ds.Tables[0].Rows[0]["HEADEARNING11"].ToString();
            //Label lblh12 = grdSelectFieldReport.FindControl("lblEH12") as Label; lblh12.Text = ds.Tables[0].Rows[0]["HEADEARNING12"].ToString();
            //Label lblh13 = grdSelectFieldReport.FindControl("lblEH13") as Label; lblh13.Text = ds.Tables[0].Rows[0]["HEADEARNING13"].ToString();
            //Label lblh14 = grdSelectFieldReport.FindControl("lblEH14") as Label; lblh14.Text = ds.Tables[0].Rows[0]["HEADEARNING14"].ToString();
            //Label lblh15 = grdSelectFieldReport.FindControl("lblEH15") as Label; lblh15.Text = ds.Tables[0].Rows[0]["HEADEARNING15"].ToString();

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
            //Label lblh36 = grdSelectFieldReport.FindControl("lblDH21") as Label; lblh36.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND21"].ToString();
            //Label lblh37 = grdSelectFieldReport.FindControl("lblDH22") as Label; lblh37.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND22"].ToString();
            //Label lblh38 = grdSelectFieldReport.FindControl("lblDH23") as Label; lblh38.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND23"].ToString();
            //Label lblh39 = grdSelectFieldReport.FindControl("lblDH24") as Label; lblh39.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND24"].ToString();
            //Label lblh40 = grdSelectFieldReport.FindControl("lblDH25") as Label; lblh40.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND25"].ToString();
            //Label lblh41 = grdSelectFieldReport.FindControl("lblDH26") as Label; lblh41.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND26"].ToString();
            //Label lblh42 = grdSelectFieldReport.FindControl("lblDH27") as Label; lblh42.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND27"].ToString();
            //Label lblh43 = grdSelectFieldReport.FindControl("lblDH28") as Label; lblh43.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND28"].ToString();
            //Label lblh44 = grdSelectFieldReport.FindControl("lblDH29") as Label; lblh44.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND29"].ToString();
            //Label lblh45 = grdSelectFieldReport.FindControl("lblDH30") as Label; lblh45.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND30"].ToString();



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
            filename = "SalaryRegister.xls";
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

    //Rohit Maske 16-11-2018
    #region Export to Excel File 15% Gross 
    protected void btn15PerExcel_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Month Year", this.Page);
            return;
        }
        div_ExportToExcel15.Visible = true;
        BindListViewReport15PerExcel();
      //  this.Export15("Excel");
    }


    private void BindListViewReport15PerExcel()
    {
        try
        {
            lblCaption.Text = "15 % Gross";
            Payroll_Report_Controller objPC = new Payroll_Report_Controller();

            DataSet ds = objPC.GetSalaryRegisterExcel15Per(ddlMonthYear.SelectedItem.Text, Convert.ToInt32(ddlStaffNo.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Session["colcode"].ToString());
            DataTable dt = ds.Tables[0];


            grdSelectFieldReport15.DataSource = dt;
            grdSelectFieldReport15.DataBind();
            this.Export15("Excel");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport15PerExcel-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (grdSelectFieldReport15 == null)
        {
            objCommon.DisplayMessage(this.Page, "Please select Month and Staff", this.Page);
            return;
        }
        this.Export15("Excel");
    }

    private void Export15(string type)
    {
        try
        {

            string filename = string.Empty;
            string ContentType = string.Empty;
            filename = "SalaryRegister.xls";
            ContentType = "ms-excel";
            string attachment = "attachment; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + ContentType;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdSelectFieldReport15.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        catch (Exception ex)
        {

        }
    }
    #endregion


    
}