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
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

public partial class PAYROLL_REPORTS_AllGrossReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                FillDropdown();
                FillListBoxStaff();
                if (ua_type != 1)
                {

                    //ddlEmployeeNo.SelectedIndex = 1;
                }
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPayHead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void FillDropdown()
    {
        //FILL EMPLOYEE
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        //objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        objCommon.FillDropDownList(ddlPayHead, "payroll_PayHead", "PAYHEAD", "PAYSHORT", "PAYSHORT is not null", "");

    }



    protected void btnGrossExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVGrossExcelReport = new GridView();
            string ContentType = string.Empty;
            //string monyear = ddlMonth.SelectedItem.Text;
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            Fdate = Fdate.Substring(0, 10);

            string Todate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Todate = Todate.Substring(0, 10);



            //string head = ddlPayHead.SelectedValue.ToString();

            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            string staffno = listStaff();

            DataSet ds = objpay.YearlyGrossExportExcelReport(Convert.ToDateTime(txtFromDt.Text).ToString(), Convert.ToDateTime(txtToDt.Text).ToString(), collegeNo, staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
                // string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + staffno);
                // string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                // string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");
                string colleAddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);


                GVGrossExcelReport.DataSource = ds;
                GVGrossExcelReport.DataBind();
                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = collename;
                HeaderCell.ColumnSpan = 3;
                //  HeaderCell.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVGrossExcelReport.Controls[0].Controls.AddAt(0, HeaderGridRow);


                // Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = colleAddress;
                HeaderCell1.ColumnSpan = 3;
                // HeaderCell1.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell1.ForeColor = System.Drawing.Color.;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVGrossExcelReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);





                string attachment = "attachment; filename=GrossExcelReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVGrossExcelReport.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AllGrossReport.btnGrossExcelReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEmployeeYrGrossReport_Click(object sender, EventArgs e)
    {
        try
        {
           
            GridView GVYearlyHeadwiseExcelReport = new GridView();
            string ContentType = string.Empty;
            //string monyear = ddlMonth.SelectedItem.Text;
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            Fdate = Fdate.Substring(0, 10);

            string Todate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Todate = Todate.Substring(0, 10);

            string head = "GS";

            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            string staffno = listStaff();

            DataSet ds = objpay.YearlyHeadwiseExportExcelReport(Convert.ToDateTime(txtFromDt.Text).ToString(), Convert.ToDateTime(txtToDt.Text).ToString(), head, collegeNo, staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
                //string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + staffno);
                // string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                // string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");

                string Fromdate = Convert.ToDateTime(txtFromDt.Text).ToString("MMMM yyyy");      //(String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
                string Todates = Convert.ToDateTime(txtToDt.Text).ToString("MMMM yyyy");     //(String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));

                string colleAddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);


                GVYearlyHeadwiseExcelReport.DataSource = ds;
                GVYearlyHeadwiseExcelReport.DataBind();

                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = collename;
                HeaderCell.ColumnSpan = 6;
                //  HeaderCell.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVYearlyHeadwiseExcelReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

                //Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = colleAddress;
                HeaderCell1.ColumnSpan = 6;
                // HeaderCell1.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell1.ForeColor = System.Drawing.Color.;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVYearlyHeadwiseExcelReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

                // Header Row 3
                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell1.Text = "YEARLY EMPLOYEE WISE GROSS SALARY DETAILS " + Fromdate + " TO " + Todates;
                HeaderCell1.ColumnSpan = 6;
                // HeaderCell1.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell1.ForeColor = System.Drawing.Color.;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVYearlyHeadwiseExcelReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

                string attachment = "attachment; filename=YearlyEmployeewiseGrossExcelReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVYearlyHeadwiseExcelReport.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AllGrossReport.btnYearlyHeadwiseExcelReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void FillListBoxStaff()
    {
        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

        //SqlParameter[] objParams = new SqlParameter[2];

        SqlParameter[] objParams = new SqlParameter[1];
        objParams[0] = new SqlParameter("@P_MONYEAR", "0");

        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_FILL_STAFF", objParams);
        lstStaffFill.Items.Clear();
       // lstStaffFill.Items.Add(new ListItem("Please Select", "0"));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstStaffFill.DataSource = ds;
            lstStaffFill.DataTextField = "STAFF";
            lstStaffFill.DataValueField = "STAFFNO";
            lstStaffFill.DataBind();
            lstStaffFill.SelectedIndex = 0;
        }
    }

    //listStaff()
    private string listStaff()
    {
        string listOfMonth = string.Empty;
        int count = 0;

        try
        {
            
                for (int i = 0; i < lstStaffFill.Items.Count; i++)
                {
                    //if (lstStaffFill.SelectedIndex == 0)
                    //{
                    //    listOfMonth = string.Empty;
                    //    return listOfMonth;
                    //}
                    //else
                    //{
                    //    if (lstStaffFill.Items[i].Selected)
                    //    {
                    //        listOfMonth += lstStaffFill.Items[i].Value + "$";
                    //    }
                    //}

                    if (lstStaffFill.Items[i].Selected)
                    {
                        listOfMonth += lstStaffFill.Items[i].Value + "$";
                        count++;
                    }
                    else
                    {

                    }
                }
                if (count == 0)
                {
                    listOfMonth = "";
                }
                else
                {
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

    protected void btnYearlyHeadwiseExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayHead.SelectedIndex == 0)
            {
                this.MessageBox("Please Select Payhead");
                return;
            }

            GridView GVYearlyHeadwiseExcelReport = new GridView();
            string ContentType = string.Empty;
            //string monyear = ddlMonth.SelectedItem.Text;
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            Fdate = Fdate.Substring(0, 10);

            string Todate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Todate = Todate.Substring(0, 10);

            string head = ddlPayHead.SelectedValue.ToString();

            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            string staffno = listStaff();

            DataSet ds = objpay.YearlyHeadwiseExportExcelReport(Convert.ToDateTime(txtFromDt.Text).ToString(), Convert.ToDateTime(txtToDt.Text).ToString(), head, collegeNo, staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
              //  string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + staffno);
                // string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                // string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");

                string Fromdate = Convert.ToDateTime(txtFromDt.Text).ToString("MMMM yyyy");      //(String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
                string Todates = Convert.ToDateTime(txtToDt.Text).ToString("MMMM yyyy");     //(String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));

                string colleAddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);


                GVYearlyHeadwiseExcelReport.DataSource = ds;
                GVYearlyHeadwiseExcelReport.DataBind();

                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = collename;
                HeaderCell.ColumnSpan = 6;
                //  HeaderCell.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVYearlyHeadwiseExcelReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

                //Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = colleAddress;
                HeaderCell1.ColumnSpan = 6;
                // HeaderCell1.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell1.ForeColor = System.Drawing.Color.;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVYearlyHeadwiseExcelReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

                // Header Row 3
                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell1.Text = ddlPayHead.SelectedItem.Text+" SALARY DETAILS " + Fromdate + " TO " + Todates;
                HeaderCell1.ColumnSpan = 6;
                // HeaderCell1.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell1.ForeColor = System.Drawing.Color.;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 14;
                HeaderCell1.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GVYearlyHeadwiseExcelReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

                string attachment = "attachment; filename=YearlyHeadwiseExcelReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVYearlyHeadwiseExcelReport.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AllGrossReport.btnYearlyHeadwiseExcelReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnDepatmentWiseYearlyGrossSalaryReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDepatmentWiseYearlyGrossSalaryReport = new GridView();
            string ContentType = string.Empty;
            //string monyear = ddlMonth.SelectedItem.Text;
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            Fdate = Fdate.Substring(0, 10);

            string Todate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Todate = Todate.Substring(0, 10);

            string head = ddlPayHead.SelectedValue.ToString();

            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            string staffno = listStaff();

            DataSet ds = objpay.DepatmentWiseYearlyGrossSalaryReport(Convert.ToDateTime(txtFromDt.Text).ToString(), Convert.ToDateTime(txtToDt.Text).ToString(), collegeNo, staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
               // string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + staffno);
                // string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
                // string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");

                string Fromdate = Convert.ToDateTime(txtFromDt.Text).ToString("MMMM yyyy");      //(String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
                string Todates = Convert.ToDateTime(txtToDt.Text).ToString("MMMM yyyy");     //(String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));

                // string colleAddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);


                GVDepatmentWiseYearlyGrossSalaryReport.DataSource = ds;
                GVDepatmentWiseYearlyGrossSalaryReport.DataBind();

                // Header Row 1
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = " GROSS SALARY DETAILS DEPARTMENT WISE " + Fromdate + " TO " + Todates;

                HeaderCell.ColumnSpan = 3;

                //  HeaderCell.BackColor = System.Drawing.Color.Navy;
                //   HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 16;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVDepatmentWiseYearlyGrossSalaryReport.Controls[0].Controls.AddAt(0, HeaderGridRow);




                string attachment = "attachment; filename=YearlyHeadwiseExcelReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDepatmentWiseYearlyGrossSalaryReport.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AllGrossReport.btnDepatmentWiseYearlyGrossSalaryReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}