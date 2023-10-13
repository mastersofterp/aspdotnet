//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL ABSTRACT SALARY REPORT                      
// CREATION DATE : 31-December-2019                                                          
// CREATED BY    : SHRIKANT AMBONE                                                
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

using System.Drawing;

public partial class PAYROLL_TRANSACTIONS_Abstract_SalaryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    int currentId = 0;
    decimal subTotalBasic = 0;
    decimal totalBasic = 0;
    decimal subTotalDAAMT = 0;
    decimal totalDAAMT = 0;
    decimal subTotalGRADEPAY = 0;
    decimal totalGRADEPAY = 0;
    decimal subTotalPAY = 0;
    decimal totalPAY = 0;
    decimal subTotalGS = 0;
    decimal totalGS = 0;
    decimal subTotalDed = 0;
    decimal totalDed = 0;
    decimal subTotalNetPay = 0;
    decimal totalNetPay = 0;
    decimal subTotalI1 = 0;
    decimal subTotalI2 = 0;
    decimal subTotalI3 = 0;
    decimal subTotalI4 = 0;
    decimal subTotalI5 = 0;
    decimal subTotalI6 = 0;
    decimal subTotalI7 = 0;
    decimal subTotalI8 = 0;
    decimal subTotalI9 = 0;
    decimal subTotalI10 = 0;
    decimal subTotalI11 = 0;
    decimal subTotalI12 = 0;
    decimal subTotalI13 = 0;
    decimal subTotalI14 = 0;
    decimal subTotalI15 = 0;
    decimal subTotalD1 = 0;
    decimal subTotalD2 = 0;
    decimal subTotalD3 = 0;
    decimal subTotalD4 = 0;
    decimal subTotalD5 = 0;
    decimal subTotalD6 = 0;
    decimal subTotalD7 = 0;
    decimal subTotalD8 = 0;
    decimal subTotalD9 = 0;
    decimal subTotalD10 = 0;
    decimal subTotalD11 = 0;
    decimal subTotalD12 = 0;
    decimal subTotalD13 = 0;
    decimal subTotalD14 = 0;
    decimal subTotalD15 = 0;
    decimal subTotalD16 = 0;
    decimal subTotalD17 = 0;
    decimal subTotalD18 = 0;
    decimal subTotalD19 = 0;
    decimal subTotalD20 = 0;
    decimal subTotalD21 = 0;
    decimal subTotalD22 = 0;
    decimal subTotalD23 = 0;
    decimal subTotalD24 = 0;
    decimal subTotalD25 = 0;
    decimal subTotalD26 = 0;
    decimal subTotalD27 = 0;
    decimal subTotalD28 = 0;
    decimal subTotalD29 = 0;
    decimal subTotalD30 = 0;


    decimal TotalI1 = 0;
    decimal TotalI2 = 0;
    decimal TotalI3 = 0;
    decimal TotalI4 = 0;
    decimal TotalI5 = 0;
    decimal TotalI6 = 0;
    decimal TotalI7 = 0;
    decimal TotalI8 = 0;
    decimal TotalI9 = 0;
    decimal TotalI10 = 0;
    decimal TotalI11 = 0;
    decimal TotalI12 = 0;
    decimal TotalI13 = 0;
    decimal TotalI14 = 0;
    decimal TotalI15 = 0;
    decimal TotalD1 = 0;
    decimal TotalD2 = 0;
    decimal TotalD3 = 0;
    decimal TotalD4 = 0;
    decimal TotalD5 = 0;
    decimal TotalD6 = 0;
    decimal TotalD7 = 0;
    decimal TotalD8 = 0;
    decimal TotalD9 = 0;
    decimal TotalD10 = 0;
    decimal TotalD11 = 0;
    decimal TotalD12 = 0;
    decimal TotalD13 = 0;
    decimal TotalD14 = 0;
    decimal TotalD15 = 0;
    decimal TotalD16 = 0;
    decimal TotalD17 = 0;
    decimal TotalD18 = 0;
    decimal TotalD19 = 0;
    decimal TotalD20 = 0;
    decimal TotalD21 = 0;
    decimal TotalD22 = 0;
    decimal TotalD23 = 0;
    decimal TotalD24 = 0;
    decimal TotalD25 = 0;
    decimal TotalD26 = 0;
    decimal TotalD27 = 0;
    decimal TotalD28 = 0;
    decimal TotalD29 = 0;
    decimal TotalD30 = 0;
    int Count = 0;

    int subTotalRowIndex = 0;


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
                    //if (Request.QueryString["pageno"] != null)
                    //{
                    //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //}
                }
                //Populate DropdownList
                PopulateDropDownList();
                FillListBoxStaff();

            }
            divMsg.InnerHtml = string.Empty;
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
            //objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "SALNO", "MONYEAR", "SALNO>0", "SALNO");


            //objCommon.FillSalfileDropDownList(ddlMonthYear);
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");

            //FILL STAFF
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
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
                Response.Redirect("~/notauthorized.aspx?Abstract_Salary.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Abstract_Salary.aspx");
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



    #region Sachin edit
    
  
   


   

    #endregion








    
    
    protected void FillListBoxStaff()
    {
        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

        //SqlParameter[] objParams = new SqlParameter[2];

        SqlParameter[] objParams = new SqlParameter[1];
        objParams[0] = new SqlParameter("@P_MONYEAR","0");

        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_FILL_STAFF", objParams);
        lstStaffFill.Items.Clear();
        lstStaffFill.Items.Add(new ListItem("Please Select", "0"));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstStaffFill.DataSource = ds;
            lstStaffFill.DataTextField = "STAFF";
            lstStaffFill.DataValueField = "STAFFNO";
            lstStaffFill.DataBind();
            lstStaffFill.SelectedIndex = 0;
        }
    }
    

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@V_TABLE=" + ddlMonthYear.SelectedItem.Text + ",@V_STAFFNO=" + param + ",@V_COLLEGE_NO=" + ddlCollege.SelectedValue;
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

    private string listStaff()
    {
        string listOfMonth = string.Empty;
        try
        {
            {
                for (int i = 0; i < lstStaffFill.Items.Count; i++)
                {
                    if (lstStaffFill.SelectedIndex == 0)
                    {
                        listOfMonth = string.Empty;
                        return listOfMonth;
                    }
                    else
                    {
                        if (lstStaffFill.Items[i].Selected)
                        {
                            listOfMonth += lstStaffFill.Items[i].Value + ".";
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
    private void ShowSalarySummaryReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MON_YEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + ddlStaffNo.SelectedValue + ",@P_COLLEGENO=" + ddlCollege.SelectedValue;
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
    private void ShowSupplementaryReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MON_YEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + ddlStaffNo.SelectedValue + ",@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_IDNO=" + 0;
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

    private void ShowSupplementaryReport2(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONYEAR=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFFNO=" + ddlStaffNo.SelectedValue + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue;
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

    protected void btnSalarySummary_Click(object sender, EventArgs e)
    {
        ShowSalarySummaryReport("Employee_Salary_Summary", "rptAbstract_Summary_Report.rpt");
    }
    protected void btnSuppliBill_Click(object sender, EventArgs e)
    {
        ShowSupplementaryReport("Employee_Supplementary_Report", "Pay_SuppliBillREport.rpt");
    }





    //Sachin ghagre 05 June 2017

    #region Export to Excel

    protected void btnShowAbstractExcel_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Month Year", this.Page);
            return;
        }
        //div_ExportToExcel.Visible = true;
        //BindListViewReport();

    }

    protected void btnShowAbstractExcelwithGrpTotal_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Month Year", this.Page);
            return;
        }
        divpnlGroupTotal.Visible = true;
        BindListViewReportwithGrpTotal();

    }
    //private void BindListViewReport()
    //{
    //    try
    //    {
    //        Payroll_Report_Controller objPC = new Payroll_Report_Controller();

    //        DataSet ds = objPC.GetAtstractRegisterExcel(ddlMonthYear.SelectedItem.Text, Convert.ToInt32(ddlStaffNo.SelectedValue),Convert.ToInt32(ddlEmployeeType.SelectedValue),Convert.ToInt32(ddlCollege.SelectedValue), Session["colcode"].ToString());
    //        DataTable dt = ds.Tables[0];


    //        grdSelectFieldReport.DataSource = dt;
    //        grdSelectFieldReport.DataBind();

    //        //EARNING HEADS
    //        Label lblh1 = grdSelectFieldReport.FindControl("lblEH1") as Label; lblh1.Text = ds.Tables[0].Rows[0]["HEADEARNING1"].ToString();
    //        Label lblh2 = grdSelectFieldReport.FindControl("lblEH2") as Label; lblh2.Text = ds.Tables[0].Rows[0]["HEADEARNING2"].ToString();
    //        Label lblh3 = grdSelectFieldReport.FindControl("lblEH3") as Label; lblh3.Text = ds.Tables[0].Rows[0]["HEADEARNING3"].ToString();
    //        Label lblh4 = grdSelectFieldReport.FindControl("lblEH4") as Label; lblh4.Text = ds.Tables[0].Rows[0]["HEADEARNING4"].ToString();
    //        Label lblh5 = grdSelectFieldReport.FindControl("lblEH5") as Label; lblh5.Text = ds.Tables[0].Rows[0]["HEADEARNING5"].ToString();
    //        Label lblh6 = grdSelectFieldReport.FindControl("lblEH6") as Label; lblh6.Text = ds.Tables[0].Rows[0]["HEADEARNING6"].ToString();
    //        Label lblh7 = grdSelectFieldReport.FindControl("lblEH7") as Label; lblh7.Text = ds.Tables[0].Rows[0]["HEADEARNING7"].ToString();
    //        Label lblh8 = grdSelectFieldReport.FindControl("lblEH8") as Label; lblh8.Text = ds.Tables[0].Rows[0]["HEADEARNING8"].ToString();
    //        Label lblh9 = grdSelectFieldReport.FindControl("lblEH9") as Label; lblh9.Text = ds.Tables[0].Rows[0]["HEADEARNING9"].ToString();
    //        Label lblh10 = grdSelectFieldReport.FindControl("lblEH10") as Label; lblh10.Text = ds.Tables[0].Rows[0]["HEADEARNING10"].ToString();
    //        Label lblh11 = grdSelectFieldReport.FindControl("lblEH11") as Label; lblh11.Text = ds.Tables[0].Rows[0]["HEADEARNING11"].ToString();
    //        Label lblh12 = grdSelectFieldReport.FindControl("lblEH12") as Label; lblh12.Text = ds.Tables[0].Rows[0]["HEADEARNING12"].ToString();
    //        Label lblh13 = grdSelectFieldReport.FindControl("lblEH13") as Label; lblh13.Text = ds.Tables[0].Rows[0]["HEADEARNING13"].ToString();
    //        Label lblh14 = grdSelectFieldReport.FindControl("lblEH14") as Label; lblh14.Text = ds.Tables[0].Rows[0]["HEADEARNING14"].ToString();
    //        Label lblh15 = grdSelectFieldReport.FindControl("lblEH15") as Label; lblh15.Text = ds.Tables[0].Rows[0]["HEADEARNING15"].ToString();

    //        //DEDUCTION HEADS
    //        Label lblh16 = grdSelectFieldReport.FindControl("lblDH1") as Label; lblh16.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND1"].ToString();
    //        Label lblh17 = grdSelectFieldReport.FindControl("lblDH2") as Label; lblh17.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND2"].ToString();
    //        Label lblh18 = grdSelectFieldReport.FindControl("lblDH3") as Label; lblh18.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND3"].ToString();
    //        Label lblh19 = grdSelectFieldReport.FindControl("lblDH4") as Label; lblh19.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND4"].ToString();
    //        Label lblh20 = grdSelectFieldReport.FindControl("lblDH5") as Label; lblh20.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND5"].ToString();
    //        Label lblh21 = grdSelectFieldReport.FindControl("lblDH6") as Label; lblh21.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND6"].ToString();
    //        Label lblh22 = grdSelectFieldReport.FindControl("lblDH7") as Label; lblh22.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND7"].ToString();
    //        Label lblh23 = grdSelectFieldReport.FindControl("lblDH8") as Label; lblh23.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND8"].ToString();
    //        Label lblh24 = grdSelectFieldReport.FindControl("lblDH9") as Label; lblh24.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND9"].ToString();
    //        Label lblh25 = grdSelectFieldReport.FindControl("lblDH10") as Label; lblh25.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND10"].ToString();
    //        Label lblh26 = grdSelectFieldReport.FindControl("lblDH11") as Label; lblh26.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND11"].ToString();
    //        Label lblh27 = grdSelectFieldReport.FindControl("lblDH12") as Label; lblh27.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND12"].ToString();
    //        Label lblh28 = grdSelectFieldReport.FindControl("lblDH13") as Label; lblh28.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND13"].ToString();
    //        Label lblh29 = grdSelectFieldReport.FindControl("lblDH14") as Label; lblh29.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND14"].ToString();
    //        Label lblh30 = grdSelectFieldReport.FindControl("lblDH15") as Label; lblh30.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND15"].ToString();

    //        Label lblh31 = grdSelectFieldReport.FindControl("lblDH16") as Label; lblh31.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND16"].ToString();
    //        Label lblh32 = grdSelectFieldReport.FindControl("lblDH17") as Label; lblh32.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND17"].ToString();
    //        Label lblh33 = grdSelectFieldReport.FindControl("lblDH18") as Label; lblh33.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND18"].ToString();
    //        Label lblh34 = grdSelectFieldReport.FindControl("lblDH19") as Label; lblh34.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND19"].ToString();
    //        Label lblh35 = grdSelectFieldReport.FindControl("lblDH20") as Label; lblh35.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND20"].ToString();
    //        Label lblh36 = grdSelectFieldReport.FindControl("lblDH21") as Label; lblh36.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND21"].ToString();
    //        Label lblh37 = grdSelectFieldReport.FindControl("lblDH22") as Label; lblh37.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND22"].ToString();
    //        Label lblh38 = grdSelectFieldReport.FindControl("lblDH23") as Label; lblh38.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND23"].ToString();
    //        Label lblh39 = grdSelectFieldReport.FindControl("lblDH24") as Label; lblh39.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND24"].ToString();
    //        Label lblh40 = grdSelectFieldReport.FindControl("lblDH25") as Label; lblh40.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND25"].ToString();
    //        Label lblh41 = grdSelectFieldReport.FindControl("lblDH26") as Label; lblh41.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND26"].ToString();
    //        Label lblh42 = grdSelectFieldReport.FindControl("lblDH27") as Label; lblh42.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND27"].ToString();
    //        Label lblh43 = grdSelectFieldReport.FindControl("lblDH28") as Label; lblh43.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND28"].ToString();
    //        Label lblh44 = grdSelectFieldReport.FindControl("lblDH29") as Label; lblh44.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND29"].ToString();
    //        Label lblh45 = grdSelectFieldReport.FindControl("lblDH30") as Label; lblh45.Text = ds.Tables[0].Rows[0]["HEADDEDUCTIOND30"].ToString();



    //        // grdSelectFieldReport.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
       // specified ASP.NET server control at run time.
    }


    private void BindListViewReportwithGrpTotal()
    {
        try
        {
           // Payroll_Report_Controller objPC = new Payroll_Report_Controller();

            DataSet ds = GetAtstractRegisterDepartmentWiseExcel(ddlMonthYear.SelectedItem.Text, Convert.ToInt32(ddlStaffNo.SelectedValue), Convert.ToInt32(ddlEmployeeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Session["colcode"].ToString());
            DataTable dt = ds.Tables[0];

            grdSalarywithGrpTotal.DataSource = dt;
            grdSalarywithGrpTotal.DataBind();

            string filename = string.Empty;
            string ContentType = string.Empty;
            filename = "AbstractRegisterDeptGroupTotal.xls";
            ContentType = "ms-excel";
            string attachment = "attachment; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + ContentType;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdSalarywithGrpTotal.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            //if (grdSalarywithGrpTotal.Rows.Count > 0)
            //{
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=Jv entries.xlsx");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //    StringWriter sw = new StringWriter();
            //    grdSalarywithGrpTotal.HeaderRow.Style.Add("background-color", "#fff");
            //    grdSalarywithGrpTotal.HeaderRow.Style.Add("color", "#000");
            //    grdSalarywithGrpTotal.HeaderRow.Style.Add("font-weight", "bold");

            //    for (int i = 0; i < grdSalarywithGrpTotal.Rows.Count; i++)
            //    {
            //        GridViewRow grow = grdSalarywithGrpTotal.Rows[i];
            //        grow.BackColor = System.Drawing.Color.White;
            //        grow.Attributes.Add("class", "textmode");
            //    }

            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {
            //        grdSalarywithGrpTotal.RenderControl(hw);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }

            //}


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public DataSet GetAtstractRegisterDepartmentWiseExcel(string monthYear, int StaffNo, int EmpTypeNo, int CollegeNo, string collCode)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_MON_YEAR", monthYear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
            objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
            objParams[3] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
            objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collCode);

            ds = objHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_PAYSLIP_DEPARTMENTWISE_TOTAL", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetAtstractRegister-> " + ex.ToString());

        }
        return ds;
    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            subTotalBasic = 0;
            subTotalDAAMT = 0;
            subTotalGRADEPAY = 0;
            subTotalPAY = 0;
            subTotalI1 = 0;
            subTotalI2 = 0;
            subTotalI3 = 0;
            subTotalI4 = 0;
            subTotalI5 = 0;
            subTotalI6 = 0;
            subTotalI7 = 0;
            subTotalI8 = 0;
            subTotalI9 = 0;
            subTotalI10 = 0;
            subTotalI11 = 0;
            subTotalI12 = 0;
            subTotalI13 = 0;
            subTotalI14 = 0;
            subTotalI15 = 0;
            subTotalGS = 0;
            subTotalD1 = 0;
            subTotalD2 = 0;
            subTotalD3 = 0;
            subTotalD4 = 0;
            subTotalD5 = 0;
            subTotalD6 = 0;
            subTotalD7 = 0;
            subTotalD8 = 0;
            subTotalD9 = 0;
            subTotalD10 = 0;
            subTotalD11 = 0;
            subTotalD12 = 0;
            subTotalD13 = 0;
            subTotalD14 = 0;
            subTotalD15 = 0;
            subTotalD16 = 0;
            subTotalD17 = 0;
            subTotalD18 = 0;
            subTotalD19 = 0;
            subTotalD20 = 0;
            subTotalD21 = 0;
            subTotalD22 = 0;
            subTotalD23 = 0;
            subTotalD24 = 0;
            subTotalD25 = 0;
            subTotalD26 = 0;
            subTotalD27 = 0;
            subTotalD28 = 0;
            subTotalD29 = 0;
            subTotalD30 = 0;
            subTotalDed = 0;
            subTotalNetPay = 0;
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                int SUBDEPTNO = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["SUBDEPTNO"]);
                totalBasic += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["BASIC"]);
                string department = dt.Rows[e.Row.RowIndex]["SUBDEPT"].ToString();
               // totalDAAMT += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["DAAMT"]);
                totalGRADEPAY += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["GRADEPAY"]);
                totalPAY += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["PAY"]);

                TotalI1 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I1"]);
                TotalI2 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I2"]);
                TotalI3 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I3"]);
                TotalI4 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I4"]);
                TotalI5 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I5"]);
                TotalI6 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I6"]);
                TotalI7 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I7"]);
                TotalI8 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I8"]);
                TotalI9 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I9"]);
                TotalI10 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I10"]);
                TotalI11 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I11"]);
                TotalI12 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I12"]);
                TotalI13 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I13"]);
                TotalI14 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I14"]);
                TotalI15 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["I15"]);
                totalGS += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["GS"]);
                TotalD1 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D1"]);
                TotalD2 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D2"]);
                TotalD3 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D3"]);
                TotalD4 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D4"]);
                TotalD5 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D5"]);
                TotalD6 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D6"]);
                TotalD7 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D7"]);
                TotalD8 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D8"]);
                TotalD9 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D9"]);
                TotalD10 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D10"]);
                TotalD11 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D11"]);
                TotalD12 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D12"]);
                TotalD13 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D13"]);
                TotalD14 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D14"]);
                TotalD15 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D15"]);
                TotalD16 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D16"]);
                TotalD17 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D17"]);
                TotalD18 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D18"]);
                TotalD19 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D19"]);
                TotalD20 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D20"]);
                TotalD21 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D21"]);
                TotalD22 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D22"]);
                TotalD23 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D23"]);
                TotalD24 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D24"]);
                TotalD25 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D25"]);
                TotalD26 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D26"]);
                TotalD27 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D27"]);
                TotalD28 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D28"]);
                TotalD29 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D29"]);
                TotalD30 += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["D30"]);
                totalDed += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["TOT_DED"]);
                totalNetPay += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["NET_PAY"]);

                if (e.Row.RowIndex == 0)
                {
                    this.AddTotalRow(department);
                }

                
                if (SUBDEPTNO != currentId)
                {
                    

                    if (e.Row.RowIndex > 0)
                    {

                       Count = 0;
                        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                        {

                            subTotalBasic += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[8].Text);
                           // subTotalDAAMT += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[6].Text);                        
                            subTotalGRADEPAY += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[9].Text);                   
                            subTotalPAY += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[10].Text);
                            subTotalI1 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[11].Text);
                            subTotalI2 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[12].Text);
                            subTotalI3 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[13].Text);
                            subTotalI4 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[14].Text);
                            subTotalI5 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[15].Text);
                            subTotalI6 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[16].Text);
                            subTotalI7 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[17].Text);
                            subTotalI8 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[18].Text);
                            subTotalI9 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[19].Text);
                            subTotalI10 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[20].Text);
                            subTotalI11 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[21].Text);
                            subTotalI12 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[22].Text);
                            subTotalI13 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[23].Text);
                            subTotalI14 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[24].Text);
                            subTotalI15 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[25].Text);
                            subTotalGS += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[26].Text);
                            subTotalD1 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[27].Text);
                            subTotalD2 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[28].Text);
                            subTotalD3 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[29].Text);
                            subTotalD4 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[30].Text);
                            subTotalD5 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[31].Text);
                            subTotalD6 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[32].Text);
                            subTotalD7 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[33].Text);
                            subTotalD8 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[34].Text);
                            subTotalD9 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[35].Text);
                            subTotalD10 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[36].Text);
                            subTotalD11 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[37].Text);
                            subTotalD12 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[38].Text);
                            subTotalD13 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[39].Text);
                            subTotalD14 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[40].Text);
                            subTotalD15 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[41].Text);
                            subTotalD16 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[42].Text);
                            subTotalD17 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[43].Text);
                            subTotalD18 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[44].Text);
                            subTotalD19 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[45].Text);
                            subTotalD20 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[46].Text);
                            subTotalD21 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[47].Text);
                            subTotalD22 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[48].Text);
                            subTotalD23 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[49].Text);
                            subTotalD24 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[50].Text);
                            subTotalD25 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[51].Text);
                            subTotalD26 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[52].Text);
                            subTotalD27 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[53].Text);
                            subTotalD28 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[54].Text);
                            subTotalD29 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[55].Text);
                            subTotalD30 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[56].Text);
                            subTotalDed += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[57].Text);
                            subTotalNetPay += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[58].Text);
                            Count = Count + 1;
                        }

                        //if (e.Row.RowIndex == 0)
                        //{
                        //    this.AddTotalRow(department + "(" + Count + ")");
                        //}
                        this.AddTotalRow("Staff Total", subTotalBasic.ToString("N2"), subTotalDAAMT.ToString("N2"), subTotalGRADEPAY.ToString("N2"), subTotalPAY.ToString("N2"), subTotalI1.ToString("N2"), subTotalI2.ToString("N2"), subTotalI3.ToString("N2"), subTotalI4.ToString("N2"), subTotalI5.ToString("N2"), subTotalI6.ToString("N2"), subTotalI7.ToString("N2"), subTotalI8.ToString("N2"), subTotalI9.ToString("N2"), subTotalI10.ToString("N2"), subTotalI11.ToString("N2"), subTotalI12.ToString("N2"), subTotalI13.ToString("N2"), subTotalI14.ToString("N2"), subTotalI15.ToString("N2"), subTotalGS.ToString("N2"), subTotalD1.ToString("N2"), subTotalD2.ToString("N2"), subTotalD3.ToString("N2"), subTotalD4.ToString("N2"), subTotalD5.ToString("N2"), subTotalD6.ToString("N2"), subTotalD7.ToString("N2"), subTotalD8.ToString("N2"), subTotalD9.ToString("N2"), subTotalD10.ToString("N2"), subTotalD11.ToString("N2"), subTotalD12.ToString("N2"), subTotalD13.ToString("N2"), subTotalD14.ToString("N2"), subTotalD15.ToString("N2"), subTotalD16.ToString("N2"), subTotalD17.ToString("N2"), subTotalD18.ToString("N2"), subTotalD19.ToString("N2"), subTotalD20.ToString("N2"), subTotalD21.ToString("N2"), subTotalD22.ToString("N2"), subTotalD23.ToString("N2"), subTotalD24.ToString("N2"), subTotalD25.ToString("N2"), subTotalD26.ToString("N2"), subTotalD27.ToString("N2"), subTotalD28.ToString("N2"), subTotalD29.ToString("N2"), subTotalD30.ToString("N2"), subTotalDed.ToString("N2"), subTotalNetPay.ToString("N2"));
                        subTotalRowIndex = e.Row.RowIndex;
                        
                          //  this.AddTotalRow("");
                        
                        if (SUBDEPTNO != currentId)
                        {
                            this.AddTotalRow(department);
                        }


                    }
                    currentId = SUBDEPTNO;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Selected_Filed_Report.BindListViewReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void AddTotalRow(string labelText, string ssubTotalBasic, string subTotalDAAMT, string subTotalGRADEPAY, string subTotalPAY, string subTotalI1, string subTotalI2, string subTotalI3, string subTotalI4, string subTotalI5, string subTotalI6, string subTotalI7, string subTotalI8, string subTotalI9, string subTotalI10, string subTotalI11, string subTotalI12, string subTotalI13, string subTotalI14, string subTotalI15, string subTotalGS, string subTotalD1, string subTotalD2, string subTotalD3, string subTotalD4, string subTotalD5, string subTotalD6, string subTotalD7, string subTotalD8, string subTotalD9, string subTotalD10, string subTotalD11, string subTotalD12, string subTotalD13, string subTotalD14, string subTotalD15, string subTotalD16, string subTotalD17, string subTotalD18, string subTotalD19, string subTotalD20, string subTotalD21, string subTotalD22, string subTotalD23, string subTotalD24, string subTotalD25, string subTotalD26, string subTotalD27, string subTotalD28, string subTotalD29, string subTotalD30, string subTotalDed, string subTotalNetPay)
    {
        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        //row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
        //row.Cells.AddRange(new TableCell[3] { new TableCell (), //Empty Cell
        //                                new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
        //                                new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        //GridView1.Controls[0].Controls.Add(row);

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
     //   row.BackColor = ColorTranslator.FromHtml("#D6ED17FF");
        row.Font.Bold=true;
        row.Cells.AddRange(new TableCell[59] { 
                                         //new TableCell (),
                                        new TableCell (), //Empty Cell
                                        new TableCell (), 
                                        
                                        //new TableCell (),
                                        new TableCell (),
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell { Text = ssubTotalBasic, HorizontalAlign = HorizontalAlign.Right }, 
                                        //new TableCell { Text = subTotalDAAMT  , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalGRADEPAY	 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalPAY, HorizontalAlign = HorizontalAlign.Right },                 
                                        new TableCell { Text = subTotalI1 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI2, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI3, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI4, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI5, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI6, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI7, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI8, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI9, HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI10 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI11 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI12 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI13 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI14 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalI15 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalGS , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD1 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD2 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD3 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD4 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD5 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD6 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD7 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD8 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD9 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD10 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD11 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD12 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD13 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD14 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD15 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD16 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD17 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD18 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD19 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD20 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD21 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD22 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD23 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD24 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD25 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD26 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD27 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD28 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD29 , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalD30 , HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = subTotalDed , HorizontalAlign = HorizontalAlign.Right }, 
                                        new TableCell { Text = subTotalNetPay , HorizontalAlign = HorizontalAlign.Right }
                                                });

        grdSalarywithGrpTotal.Controls[0].Controls.Add(row);
    }

    private void AddTotalRow(string Department)
    {
        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        //row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
        //row.Cells.AddRange(new TableCell[3] { new TableCell (), //Empty Cell
        //                                new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
        //                                new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        //GridView1.Controls[0].Controls.Add(row);

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
       // row.BackColor = ColorTranslator.FromHtml("#9933FF");
        row.Font.Bold = true;
        row.Cells.AddRange(new TableCell[59] { 
                                        new TableCell (), //Empty Cell
                                        new TableCell (), //Empty Cell
                                       // new TableCell (), 
                                       
                                        new TableCell (),
                                         new TableCell { Text = Department, HorizontalAlign = HorizontalAlign.Right},
                                        new TableCell (), 
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (), 
                                        new TableCell (), 
                                        new TableCell (),              
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                                });

        grdSalarywithGrpTotal.Controls[0].Controls.Add(row);
    }



    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = subTotalRowIndex; i < grdSalarywithGrpTotal.Rows.Count; i++)
        {
              subTotalBasic += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[8].Text);
                           // subTotalDAAMT += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[6].Text);                        
                            subTotalGRADEPAY += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[9].Text);                   
                            subTotalPAY += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[10].Text);
                            subTotalI1 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[11].Text);
                            subTotalI2 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[12].Text);
                            subTotalI3 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[13].Text);
                            subTotalI4 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[14].Text);
                            subTotalI5 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[15].Text);
                            subTotalI6 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[16].Text);
                            subTotalI7 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[17].Text);
                            subTotalI8 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[18].Text);
                            subTotalI9 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[19].Text);
                            subTotalI10 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[20].Text);
                            subTotalI11 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[21].Text);
                            subTotalI12 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[22].Text);
                            subTotalI13 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[23].Text);
                            subTotalI14 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[24].Text);
                            subTotalI15 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[25].Text);
                            subTotalGS += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[26].Text);
                            subTotalD1 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[27].Text);
                            subTotalD2 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[28].Text);
                            subTotalD3 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[29].Text);
                            subTotalD4 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[30].Text);
                            subTotalD5 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[31].Text);
                            subTotalD6 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[32].Text);
                            subTotalD7 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[33].Text);
                            subTotalD8 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[34].Text);
                            subTotalD9 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[35].Text);
                            subTotalD10 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[36].Text);
                            subTotalD11 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[37].Text);
                            subTotalD12 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[38].Text);
                            subTotalD13 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[39].Text);
                            subTotalD14 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[40].Text);
                            subTotalD15 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[41].Text);
                            subTotalD16 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[42].Text);
                            subTotalD17 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[43].Text);
                            subTotalD18 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[44].Text);
                            subTotalD19 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[45].Text);
                            subTotalD20 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[46].Text);
                            subTotalD21 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[47].Text);
                            subTotalD22 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[48].Text);
                            subTotalD23 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[49].Text);
                            subTotalD24 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[50].Text);
                            subTotalD25 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[51].Text);
                            subTotalD26 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[52].Text);
                            subTotalD27 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[53].Text);
                            subTotalD28 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[54].Text);
                            subTotalD29 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[55].Text);
                            subTotalD30 += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[56].Text);
                            subTotalDed += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[57].Text);
                            subTotalNetPay += Convert.ToDecimal(grdSalarywithGrpTotal.Rows[i].Cells[58].Text);
        }
        this.AddTotalRow("Staff Total", subTotalBasic.ToString("N2"), subTotalDAAMT.ToString("N2"), subTotalGRADEPAY.ToString("N2"), subTotalPAY.ToString("N2"), subTotalI1.ToString("N2"), subTotalI2.ToString("N2"), subTotalI3.ToString("N2"), subTotalI4.ToString("N2"), subTotalI5.ToString("N2"), subTotalI6.ToString("N2"), subTotalI7.ToString("N2"), subTotalI8.ToString("N2"), subTotalI9.ToString("N2"), subTotalI10.ToString("N2"), subTotalI11.ToString("N2"), subTotalI12.ToString("N2"), subTotalI13.ToString("N2"), subTotalI14.ToString("N2"), subTotalI15.ToString("N2"), subTotalGS.ToString("N2"), subTotalD1.ToString("N2"), subTotalD2.ToString("N2"), subTotalD3.ToString("N2"), subTotalD4.ToString("N2"), subTotalD5.ToString("N2"), subTotalD6.ToString("N2"), subTotalD7.ToString("N2"), subTotalD8.ToString("N2"), subTotalD9.ToString("N2"), subTotalD10.ToString("N2"), subTotalD11.ToString("N2"), subTotalD12.ToString("N2"), subTotalD13.ToString("N2"), subTotalD14.ToString("N2"), subTotalD15.ToString("N2"), subTotalD16.ToString("N2"), subTotalD17.ToString("N2"), subTotalD18.ToString("N2"), subTotalD19.ToString("N2"), subTotalD20.ToString("N2"), subTotalD21.ToString("N2"), subTotalD22.ToString("N2"), subTotalD23.ToString("N2"), subTotalD24.ToString("N2"), subTotalD25.ToString("N2"), subTotalD26.ToString("N2"), subTotalD27.ToString("N2"), subTotalD28.ToString("N2"), subTotalD29.ToString("N2"), subTotalD30.ToString("N2"), subTotalDed.ToString("N2"), subTotalNetPay.ToString("N2"));
        this.AddTotalRow("Total", totalBasic.ToString("N2"), totalDAAMT.ToString("N2"), totalGRADEPAY.ToString("N2"), totalPAY.ToString("N2"), TotalI1.ToString("N2"), TotalI2.ToString("N2"), TotalI3.ToString("N2"), TotalI4.ToString("N2"), TotalI5.ToString("N2"), TotalI6.ToString("N2"), TotalI7.ToString("N2"), TotalI8.ToString("N2"), TotalI9.ToString("N2"), TotalI10.ToString("N2"), TotalI11.ToString("N2"), TotalI12.ToString("N2"), TotalI13.ToString("N2"), TotalI14.ToString("N2"), TotalI15.ToString("N2"), totalGS.ToString("N2"), TotalD1.ToString("N2"), TotalD2.ToString("N2"), TotalD3.ToString("N2"), TotalD4.ToString("N2"), TotalD5.ToString("N2"), TotalD6.ToString("N2"), TotalD7.ToString("N2"), TotalD8.ToString("N2"), TotalD9.ToString("N2"), TotalD10.ToString("N2"), TotalD11.ToString("N2"), TotalD12.ToString("N2"), TotalD13.ToString("N2"), TotalD14.ToString("N2"), TotalD15.ToString("N2"), TotalD16.ToString("N2"), TotalD17.ToString("N2"), TotalD18.ToString("N2"), TotalD19.ToString("N2"), TotalD20.ToString("N2"), TotalD21.ToString("N2"), TotalD22.ToString("N2"), TotalD23.ToString("N2"), TotalD24.ToString("N2"), TotalD25.ToString("N2"), TotalD26.ToString("N2"), TotalD27.ToString("N2"), TotalD28.ToString("N2"), TotalD29.ToString("N2"), TotalD30.ToString("N2"), totalDed.ToString("N2"), totalNetPay.ToString("N2"));
     
    }

    protected void grdSalarywithGrpTotal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "ISNULL(PAYSHORT,'') <>''", "SRNO");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I1")
                    e.Row.Cells[11].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I2")
                    e.Row.Cells[12].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I3")
                    e.Row.Cells[13].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I4")
                    e.Row.Cells[14].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I5")
                    e.Row.Cells[15].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I6")
                    e.Row.Cells[16].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I7")
                    e.Row.Cells[17].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I8")
                    e.Row.Cells[18].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I9")
                    e.Row.Cells[19].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I10")
                    e.Row.Cells[20].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I11")
                    e.Row.Cells[21].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I12")
                    e.Row.Cells[22].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I13")
                    e.Row.Cells[23].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I14")
                    e.Row.Cells[24].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "I15")
                    e.Row.Cells[25].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D1")
                    e.Row.Cells[27].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D2")
                    e.Row.Cells[28].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D3")
                    e.Row.Cells[29].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D4")
                    e.Row.Cells[30].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D5")
                    e.Row.Cells[31].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D6")
                    e.Row.Cells[32].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D7")
                    e.Row.Cells[33].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D8")
                    e.Row.Cells[34].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D9")
                    e.Row.Cells[35].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D10")
                    e.Row.Cells[36].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D11")
                    e.Row.Cells[37].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D12")
                    e.Row.Cells[38].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D13")
                    e.Row.Cells[39].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D14")
                    e.Row.Cells[40].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D15")
                    e.Row.Cells[41].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D16")
                    e.Row.Cells[42].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D17")
                    e.Row.Cells[43].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D18")
                    e.Row.Cells[44].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D19")
                    e.Row.Cells[45].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D20")
                    e.Row.Cells[46].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D21")
                    e.Row.Cells[47].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D22")
                    e.Row.Cells[48].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D23")
                    e.Row.Cells[49].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D24")
                    e.Row.Cells[50].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D25")
                    e.Row.Cells[51].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D26")
                    e.Row.Cells[52].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D27")
                    e.Row.Cells[53].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D28")
                    e.Row.Cells[54].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D29")
                    e.Row.Cells[55].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();

                if (ds.Tables[0].Rows[i]["PAYHEAD"].ToString().Trim() == "D30")
                    e.Row.Cells[56].Text = ds.Tables[0].Rows[i]["PAYSHORT"].ToString().Trim();
                                
            }

            //DataView dv = new DataView(ds.Tables[0]);

            //for (int i = 0; i < e.Row.Cells.Count; i++)
            //{
            //    if (string.Compare(e.Row.Cells[i].Text, "I1", true) == 0)
            //    {
            //         dv.RowFilter = "PAYHEAD < ''I1'"; 
            //        e.Row.Cells[i].Text = dv.Table.["PAYSHORT"].tostring();
            //    }
            //}
        }
    }



    protected void imgbutExporttoexcelGrpTotal_Click(object sender, ImageClickEventArgs e)
    {
        if (grdSalarywithGrpTotal == null)
        {
            objCommon.DisplayMessage(this.Page, "Please select Month and Staff", this.Page);
            return;
        }
        this.ExportGrpTotal("Excel");

    }

    private void ExportGrpTotal(string type)
    {
        try
        {
            //string filename = string.Empty;
            //string ContentType = string.Empty;
            //filename = "AbstractRegisterDeptGroup.xls";
            //ContentType = "ms-excel";
            //string attachment = "attachment; filename=" + filename;
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/" + ContentType;
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdSalarywithGrpTotal.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();

            if (grdSalarywithGrpTotal.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Jv entries.xlsx");
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                StringWriter sw = new StringWriter();
                grdSalarywithGrpTotal.HeaderRow.Style.Add("background-color", "#fff");
                grdSalarywithGrpTotal.HeaderRow.Style.Add("color", "#000");
                grdSalarywithGrpTotal.HeaderRow.Style.Add("font-weight", "bold");

                for (int i = 0; i < grdSalarywithGrpTotal.Rows.Count; i++)
                {
                    GridViewRow grow = grdSalarywithGrpTotal.Rows[i];
                    grow.BackColor = System.Drawing.Color.White;
                    grow.Attributes.Add("class", "textmode");
                }

                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    grdSalarywithGrpTotal.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
         

        }
        catch (Exception ex)
        {

        }
    }

    protected void imgbutExporttoexcel_Click(object sender, ImageClickEventArgs e)
    {
        //if (grdSelectFieldReport == null)
        //{
        //    objCommon.DisplayMessage(this.Page, "Please select Month and Staff", this.Page);
        //    return;
        //}
        this.Export("Excel");

    }

    private void Export(string type)
    {
        try
        {
            string filename = string.Empty;
            string ContentType = string.Empty;
            filename = "AbstractRegister.xls";
            ContentType = "ms-excel";
            string attachment = "attachment; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + ContentType;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdSelectFieldReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        catch (Exception ex)
        {

        }
    }

    #endregion


    //Sachin ghagre 24 Apr 2018

    protected void btnGrossDiff_Click(object sender, EventArgs e)
    {
        ShowGrossDifferenceReport("GrossDiffernce Report", "Payroll_GrossDiffReport.rpt");
    }


    private void ShowGrossDifferenceReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TABNAME=" + ddlMonthYear.SelectedItem.Text + ",@P_STAFF_NO=" + ddlStaffNo.SelectedValue + ",@P_COLLEGE_NO=" + ddlCollege.SelectedValue + ",@P_EMPTYPENO=" + ddlEmployeeType.SelectedValue;
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
    protected void btnSuppBill2_Click(object sender, EventArgs e)
    {
        ShowSupplementaryReport2("Employee Supplementary Report", "Pay_Handled_SupplementaryBill.rpt");
    }
   
}