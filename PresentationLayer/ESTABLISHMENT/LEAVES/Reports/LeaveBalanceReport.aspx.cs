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
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Reports_LeaveBalanceReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BioMetricsController objBioMetric = new BioMetricsController();
    LeavesController objleave = new LeavesController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //divMsg.InnerHtml = string.Empty;
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (!Page.IsPostBack)
        {
            CheckPageAuthorization();
            FillCollege();
            FillDepartmentStaff();
            FillDropDown();

            if (ua_type != 1)
            {
                trEmp.Visible = true;
                //lblEmployee.Visible = true;
                //ddlEmployee.Visible = true;
                trdept.Visible = tr1.Visible = trcollege.Visible = false;
                trsearchtype.Visible = false;
                rblSelect.SelectedValue = "1";
                //this.FillEmployeeIdno();
                //ddlEmployee.SelectedIndex = 1;
                btnCancel.Visible = false;
            }
            else
            {
                trdept.Visible = tr1.Visible = trcollege.Visible = true;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "CODE");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    protected void FillEmployee()
    {
        if (ddlStaff.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.IDNO=P.IDNO AND E.STNO = " + Convert.ToInt32(ddlStaff.SelectedValue) + " ", "ENAME");
        }
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.IDNO=P.IDNO AND E.STNO = " + Convert.ToInt32(ddlCollege.SelectedValue) + " ", "ENAME");
        }
        if (ddldept.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.IDNO=P.IDNO AND E.STNO = " + Convert.ToInt32(ddldept.SelectedValue) + " ", "ENAME");
        }
        if (ddlStaff.SelectedIndex == 0 && ddlCollege.SelectedIndex == 0 && ddldept.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.IDNO=P.IDNO", "ENAME");
        }
    }

    //protected void FillEmployeeIdno()
    //{
    //    int IDNO = 0;
    //    // string employeeid = Request.QueryString["EmployeeId"].ToString().Trim();
    //    if (Request.QueryString["EmployeeId"] == null)//07-12-2016
    //    {
    //        IDNO = Convert.ToInt32(Session["idno"]);
    //    }
    //    else
    //    {
    //        IDNO = Convert.ToInt32(Request.QueryString["EmployeeId"]);
    //    }


    //    //if (Request.QueryString["EmployeeId"].ToString() != null)
    //    //{
    //    //    IDNO = Convert.ToInt32(Request.QueryString["EmployeeId"]);
    //    //}
    //    //else
    //    //{
    //    //    IDNO = Convert.ToInt32(Session["idno"]);
    //    //}      


    //    int ua_type = Convert.ToInt32(Session["usertype"]);

    //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME", "IDNO=" + IDNO, "");
    //    ddlEmployee.SelectedValue = IDNO.ToString();
    //    DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO", "stno,college_no", "IDNO=" + IDNO + "", "");
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        int subdeptno = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBDEPTNO"]);
    //        int STAFFNO = Convert.ToInt32(ds.Tables[0].Rows[0]["stno"]);
    //        int college_no = Convert.ToInt32(ds.Tables[0].Rows[0]["college_no"]);
    //        ddldept.SelectedValue = subdeptno.ToString();

    //        ddlStaff.SelectedValue = STAFFNO.ToString();
    //        ddlCollege.SelectedValue = college_no.ToString();

    //    }
    //}

    private void FillDepartmentStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
            // objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0", "STAFFTYPE");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDropDown()
    {
        try
        {
            //To fill Year
            int chkYear = DateTime.Now.Year;
            ddlYear.Items.Add((chkYear - 1).ToString());
            ddlYear.Items.Add(chkYear.ToString());
            ddlYear.Items.Add((chkYear + 1).ToString());

            objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rblSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlEmployee.SelectedValue = "0";
        // ddlEmployee.SelectedItem.Text = "Please Select";
        //txtDate.Text = string.Empty;
        if (ddlCollege.SelectedIndex >= 0)
        {
            if (rblSelect.SelectedValue == "1")
            {
                ddlEmployee.Visible = true;
                //lblEmployee.Visible = true;
                //lbl.Visible = true;
                trEmp.Visible = true;
                FillEmployee();
            }
            else if (rblSelect.SelectedValue == "0")
            {

                ddlEmployee.Visible = false;
                //lblEmployee.Visible = false;
                // lbl.Visible = false;
                trEmp.Visible = false;
            }
        }
        else
        {
        }
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Leave Balance Report", "LeaveBalanceReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Login_details_Time_Interval.aspx.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int idno = 0;
            int lvno = 0;
            int stno = 0;
            int deptno = 0;
            int periodno = 0;

            if (ddlStaffType.SelectedIndex > 0)
            {
                stno = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                stno = 0;
            }
            if (ddldept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }
            if (ddlPeriod.SelectedIndex > 0)
            {
                periodno = Convert.ToInt32(ddlPeriod.SelectedValue);
            }
            else
            {
                periodno = 0;
            }
            //establishment//")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("biometrics")));//BIOMETRICS
            //int url1 = Request.Url.ToString().ToLower().IndexOf("establishment");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            if (rblSelect.SelectedValue == "0")
            {
                idno = 0;
            }
            else
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    idno = Convert.ToInt32(ddlEmployee.SelectedValue);

                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Select Employee", this);
                    return;
                }
            }
            string Script = string.Empty;
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
                {
                    url += "&param=@P_IDNO=" + idno + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_PERIOD=" + periodno + ",@P_LVNO=" + lvno + ",@P_STNO=" + stno + "," + "@P_Deptno=" + deptno + "," + "@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue);
                }
                else
                {
                    url += "&param=@P_IDNO=" + idno + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_PERIOD=" + periodno + ",@P_LVNO=" + lvno + ",@P_STNO=" + stno + "," + "@P_Deptno=" + deptno + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
            }
            else
            {
                url += "&param=@P_IDNO=" + idno + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_PERIOD=" + periodno + ",@P_LVNO=" + lvno + ",@P_STNO=" + stno + "," + "@P_Deptno=" + deptno + "," + "@P_COLLEGE_CODE=" + Session["college_nos"].ToString();
            }

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Report", Script, true);



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Login_details_Time_Interval.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        DataSet ds = null;

        int Year = Convert.ToInt32(ddlYear.SelectedValue);
        int idno = 0;
        int lvno = 0;
        int stno = 0;
        int deptno = 0;
        int periodno = 0;

        if (ddlStaffType.SelectedIndex > 0)
        {
            stno = Convert.ToInt32(ddlStaffType.SelectedValue);
        }
        else
        {
            stno = 0;
        }
        if (ddldept.SelectedIndex > 0)
        {
            deptno = Convert.ToInt32(ddldept.SelectedValue);
        }
        else
        {
            deptno = 0;
        }
        if (ddlEmployee.SelectedIndex > 0)
        {
            idno = Convert.ToInt32(ddlEmployee.SelectedValue);
        }
        else
        {
            idno = 0;
        }
        if (ddlPeriod.SelectedIndex > 0)
        {
            periodno = Convert.ToInt32(ddlPeriod.SelectedValue);
        }
        else
        {
            periodno = 0;
        }


        ds = objleave.LeaveEmployeeBalanceForExport(idno, lvno, Year, periodno, stno, deptno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView gv_ExcelData = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_ExcelData.DataSource = ds;
                gv_ExcelData.DataBind();
                string attachment = "attachment; filename=EmployeeLeaveBalanceReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv_ExcelData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found", this.Page);
            return;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void rblemptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillEmployee();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPeriod.SelectedIndex > 0)
        {
            DateTime frmdt; DateTime todt;
            int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
            int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), to);
            //int days = DateTime.DaysInMonth(year, month);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            year = year + 1;
            frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), from, 1);
            if (to < from)
            {
                todt = new DateTime(Convert.ToInt32(year), to, days);
            }
            else
            {
                todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), to, days);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Login_details_Time_Interval.aspx?pageno=1314");
        //ddlStaffType.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        txtFdate.Text = string.Empty;
        txtDate.Text = string.Empty;
        if (rblSelect.SelectedValue == "1")
        {
          ddlEmployee.SelectedIndex = 0;
          rblSelect.SelectedIndex = 0;
        }
        trEmp.Visible = false;
        ddlPeriod.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
    }
}