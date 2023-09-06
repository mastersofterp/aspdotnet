//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : VacationReport.aspx                                                   
// CREATION DATE : 29 nov 2017
// CREATED BY    : Sagar Hiratkar                                       
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;
using System.Collections;
using System.Web;


public partial class ESTABLISHMENT_LEAVES_Reports_VacationReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaveC = new LeavesController();
    Leaves objLeaveE = new Leaves();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                CheckPageAuthorization();
                FillCollege();
                FillDepartment();
                FillStaffType();

            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=VacationReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=VacationReport.aspx");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillDepartment()
    {
        try
        {
            // objCommon.FillDropDownList(ddlShift, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_master", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Vacation Report", "Pay_Vacation_Report.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=username=" + Session["username"].ToString() + ",@P_FSDATE=" + txtFromdt.Text.Trim() + ",@P_FEDATE=" + txtTodt.Text.Trim() + ",@P_DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue); //+ ",FSDATE=" + Convert.ToDateTime(txtFromdt.Text.Trim()).ToString("dd/MMM/yyyy") + ",FEDATE=" + Convert.ToDateTime(txtTodt.Text.Trim()).ToString("dd/MMM/yyyy")
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FSDATE=" + Convert.ToDateTime(txtFromdt.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_FEDATE=" + Convert.ToDateTime(txtTodt.Text.Trim()).ToString("yyyy-MM-dd");
            url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_FROMDT=" + Convert.ToDateTime(txtFromDt.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_TODT=" + Convert.ToDateTime(txtToDt.Text.Trim()).ToString("yyyy-MM-dd");

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();

            objLeaveE.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objLeaveE.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            objLeaveE.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
            objLeaveE.FROMDT = Convert.ToDateTime(txtFromDt.Text.Trim());
            objLeaveE.TODT = Convert.ToDateTime(txtToDt.Text.Trim());
            //Check whether entered date must not greater than todays date
            
                DataSet ds = null;

                ds = objLeaveC.GetVacationExcelReport(objLeaveE);             
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVDayWiseAtt.DataSource = ds;
                    GVDayWiseAtt.DataBind();
                    string attachment = "attachment; filename=Vacation.xls";
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
                    MessageBox("No Records found.");
                    return;
                }
            }


        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlCollege.SelectedIndex = 0;
            ddlStafftype.SelectedIndex = 0;
            ddlDept.SelectedIndex = 0;
            txtFromDt.Text = string.Empty;
            txtToDt.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        DateTime Test;
        if (DateTime.TryParseExact(txtToDt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtFromDt.Text.ToString() != string.Empty && txtFromDt.Text.ToString() != "__/__/____" && txtToDt.Text.ToString() != string.Empty && txtToDt.Text.ToString() != "__/__/____")
            {
                DateTime fromDate = Convert.ToDateTime(txtFromDt.Text.ToString());

                DateTime toDate = Convert.ToDateTime(txtToDt.Text.ToString());

                if (toDate < fromDate)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtToDt.Text = string.Empty;
                    return;
                }

            }
        }
        else
        {
            txtToDt.Text = string.Empty;
        }
    }
}