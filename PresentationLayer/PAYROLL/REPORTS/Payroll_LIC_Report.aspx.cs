//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL LIC REPORT
// CREATION DATE : 25-September-2009                                                          
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
using System.IO;

public partial class PayRoll_Payroll_LIC_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

                //Populate DropdownList
                PopulateDropDownList();

                //Fill FillMonth
                 FillMonth();
                 this.FillCollege();
                //Staff Wise Dropdown Enabled False
                 //ddlStaffNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEMployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Payroll_LIC_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Payroll_LIC_Report.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL EMPLOYEE
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO>0", "IDNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillMonth()
    {
        try
        {
            //This method is for filling the Month dropdownList. 
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            SqlParameter[] objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@P_COLLEGE_CODE", Session["colcode"].ToString());
            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LOGFILE_REPORT_DROPDOWN_FILL_MONTH", objParams);
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem("Please Select", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMonth.DataSource = ds;
                ddlMonth.DataTextField = "MONYEAR";
                ddlMonth.DataValueField = "MONYEAR";
                ddlMonth.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.FillMonth()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    // This is used to get the LIC Report in Excel.
    protected void btnLICExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
            string monyear = ddlMonth.SelectedItem.Text;
            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);

            DataSet ds = objpay.LICExportExcel(monyear, collegeNo, staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=LICExcelReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                ShowMessage("No data found for current selection");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICExcel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnEPFCexcel_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        int employeetype = 0;
        string monyear = ddlMonth.SelectedItem.Text;
        int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);


        DataSet ds = objpay.EPFStatementExcel(monyear, collegeNo, staffno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment; filename=EPFStatement.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            ShowMessage("No data found for current selection");
            return;
        }
    }
    protected void btnEPFExcelNew_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        int employeetype = 0;
        string monyear = ddlMonth.SelectedItem.Text;
        int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);


        DataSet ds = objpay.EPFStatementExcelNew(monyear, collegeNo, staffno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment; filename=EPFStatement.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            ShowMessage("No data found for current selection");
            return;
        }
    }
    protected void btnEPFWord_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        int employeetype = 0;
        string monyear = ddlMonth.SelectedItem.Text;
        int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);


        DataSet ds = objpay.EPFStatementWord(monyear, collegeNo, staffno);

        string filename = "EPFStatement.txt";


        if (ds.HasErrors == false)
        {
            ExportDataTabletoFile(ds.Tables[0], "    ", true, Server.MapPath("~/Images/" + filename));
        }
        else
        {
            ShowMessage("No data found for current selection");
            return;
        }


        Response.ContentType = "application/octet-stream";

        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);

        string aaa = Server.MapPath("~/images/" + filename);

        Response.TransmitFile(Server.MapPath("~/images/" + filename));

        HttpContext.Current.ApplicationInstance.CompleteRequest();

        Response.End();

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    //ds.Tables[0].Columns.RemoveAt(3);
        //    GVDayWiseAtt.DataSource = ds;
        //    GVDayWiseAtt.DataBind();

        //    string attachment = "attachment; filename=EPFStatement.txt";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/vnd.txt";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    GVDayWiseAtt.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
        //else
        //{

        //}
    }
    public void ExportDataTabletoFile(DataTable datatable, string delimited, bool exportcolumnsheader, string file)
    {

        StreamWriter str = new StreamWriter(file, false, System.Text.Encoding.Default);

        //if (exportcolumnsheader)
        //{
        //    string Columns = string.Empty;

        //    foreach (DataColumn column in datatable.Columns)
        //    {
        //        Columns += column.ColumnName + delimited;
        //    }
        //    str.WriteLine(Columns.Remove(Columns.Length - 1, 1));

        //}

        foreach (DataRow datarow in datatable.Rows)
        {
            string row = string.Empty;

            foreach (object items in datarow.ItemArray)
            {
                row += items.ToString() + delimited;
            }
            str.WriteLine(row.Remove(row.Length - 1, 1));
        }
        str.Flush();
        str.Close();
    }

    
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;

           // url += "&param=@P_TABNAME=" + ddlMonth.SelectedItem.Text + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_COLLEGNO="+ddlCollege.SelectedValue;
           
            if (ddlCollege.SelectedIndex > 0)
            {
                url += "&param=@P_TABNAME=" + ddlMonth.SelectedItem.Text + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_CODE=" + ddlCollege.SelectedValue + ",@P_COLLEGNO=" + ddlCollege.SelectedValue;
            }
            else
            {
                url += "&param=@P_TABNAME=" + ddlMonth.SelectedItem.Text + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_CODE=" + ddlCollege.SelectedValue + ",@P_COLLEGNO=" + ddlCollege.SelectedValue;
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnLICReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("LIC_Report", "Pay_LICReport.rpt");
        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnRdReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("RD_Report", "rptEmployee_RD_Report.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnRdReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page Url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillCollege()
    {
        try
        {

            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPTReport_Click(object sender, EventArgs e)
    {
        try
        {
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "OrganizationId=" + OrganizationId + " and IDCardType='PTAmountDeduction'");
            if (ReportName == "")
            {
                ShowReport("PT_Report", "Pay_PTAmoutDeduction.rpt");
            }
            else
            {
                ShowReport("PT_Report", ReportName);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEPFReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("EPF_Report", "Pay_EPF_ECR.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnGrossTDSReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("GrossTDS_Report", "Pay_GrossTDS.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPTExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
            string monyear = ddlMonth.SelectedItem.Text;
            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);

            DataSet ds = objpay.PTExportExcel(monyear, collegeNo, staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
                //string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
                string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = collename;
                HeaderCell.ColumnSpan = 9;
                //HeaderCell.BackColor = System.Drawing.Color.Navy;
                //HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVDayWiseAtt.Controls[0].Controls.AddAt(0, HeaderGridRow);


                // Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "  P.Tax Salary Details of " + Month + "    " + Year + "    ";
                HeaderCell1.ColumnSpan = 9;
                //HeaderCell1.BackColor = System.Drawing.Color.Navy;
                //HeaderCell1.ForeColor = System.Drawing.Color.White;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVDayWiseAtt.Controls[0].Controls.AddAt(1, HeaderGridRow1);



                string attachment = "attachment; filename=PTExcelExport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                ShowMessage("No data found for current selection");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICExcel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
