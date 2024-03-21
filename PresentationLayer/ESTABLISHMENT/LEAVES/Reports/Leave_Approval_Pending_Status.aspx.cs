using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;

public partial class ESTABLISHMENT_LEAVES_Reports_Leave_Approval_Pending_Status : System.Web.UI.Page
{

    string date = "";
    int counter = 0;
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();


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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;
                tremp.Visible = false;
                //this.FillEmployee();
                FillCollege();
                FillDepartment();
                FillStaffType();
            }
        }
    }
    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1

            objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0", "DEPT.SUBDEPT");
            //objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()), "DEPT.SUBDEPT");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffTypeType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillStaffType()
    {
        try
        {

            // objCommon.FillDropDownList(ddlstafftype, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_STAFFTYPE S ON (S.STNO = E.STNO)", " DISTINCT S.STNO", "S.STAFFTYPE", "S.STNO<>0 ", "STAFFTYPE");
            objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
            //objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) , "STAFFTYPE");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffTypeType ->" + ex.Message + "  " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void rblAllParticular_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAllParticular.SelectedValue == "1")
        {
            tremp.Visible = true;
            ddlEmp.Visible = true;
            this.FillEmployee();
            divnote.Visible = true;
        }
        else
        {
            tremp.Visible = false;
            this.FillEmployee();
            divnote.Visible = false;
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()), "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }
    public void FillEmployee()
    {
        //if (chkDept.Checked == true)
        //{
        //     if (chkstaff.Checked == true)
        //               objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND SUBDEPTNO=" + ddldept.SelectedValue+"AND STAFFNO="+ddlstafftype.SelectedValue, "FNAME");
        //     else
        //         objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND SUBDEPTNO=" + ddldept.SelectedValue , "FNAME");

        //}
        //else
        //{
        //    if (chkstaff.Checked == true)
        //     objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND STAFFNO="+ddlstafftype.SelectedValue, "FNAME");
        //    else
        //        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0", "FNAME");
        //}

        //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "E.FNAME + ' ' + E.MNAME + ' ' + E.LNAME + '['+ convert(nvarchar(150),E.IDNO) + ']'", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND  (E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " OR " + Convert.ToInt32(ddldept.SelectedValue) + "=0) AND (E.stno =" + Convert.ToInt32(ddlstafftype.SelectedValue) + " OR " + Convert.ToInt32(ddlstafftype.SelectedValue) + "=0) AND E.OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()), "E.FNAME");
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "E.FNAME + ' ' + E.MNAME + ' ' + E.LNAME + '['+ convert(nvarchar(150),E.IDNO) + ']'", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND  (E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " OR " + Convert.ToInt32(ddldept.SelectedValue) + "=0) AND (E.stno =" + Convert.ToInt32(ddlstafftype.SelectedValue) + " OR " + Convert.ToInt32(ddlstafftype.SelectedValue) + "=0)", "E.FNAME");

        //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND SUBDEPTNO=" + ddldept.SelectedValue + " AND STNO=" + ddlstafftype.SelectedValue + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "FNAME");

    }

    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int empno = 0;
            int deptno = 0;
            int staffno = 0;
            int Collegeno = 0;
            string status;
            if (rblAllParticular.SelectedValue == "0")
            {
                empno = 0;
            }
            else
            {
                empno = Convert.ToInt32(ddlEmp.SelectedValue);
            }
            if (ddlCollege.SelectedIndex > 0)
            {
                Collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                Collegeno = 0;
            }
            if (ddldept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }
            if (ddlstafftype.SelectedIndex > 0)
            {
                staffno = Convert.ToInt32(ddlstafftype.SelectedValue);
            }
            else
            {
                staffno = 0;
            }
            if (rdbleavestatus.SelectedValue == "0")
            {
                status = "A";
            }
            else if (rdbleavestatus.SelectedValue == "2")
            {
                status = "R";
            }
            else
            {
                status = "P";
            }

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_NO=" + Collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + ",@P_STATUS=" + status.ToString().Trim() + ",@P_EMPNO=" + empno + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            url += "&param=@P_COLLEGE_NO=" + Collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + ",@P_STATUS=" + status.ToString().Trim() + ",@P_EMPNO=" + empno + "," + "@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + "";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";



        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("LeaveApproval", "LEAVE_APPROVED_PENDING.rpt");
    }

    //protected void chkDept_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkDept.Checked)
    //    {
    //        trddldept.Visible = true;
    //        ddldept.Visible = true;
    //        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
    //    }
    //    else
    //        trddldept.Visible = false;
    //}
    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {
        //butAttendanceProcess.Text = "Leave Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        btnReport.Text = "Leave Process For" + " " + Convert.ToDateTime(txtTodt.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    }


    //protected void chkstaff_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkstaff.Checked)
    //    {
    //        trddlstaff.Visible = true;
    //        ddlstafftype.Visible = true;
    //        objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0", "STAFFTYPE");
    //    }
    //    else
    //        trddlstaff.Visible = false;
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void Clear()
    {
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        rblAllParticular.SelectedIndex = 0;
        rdbleavestatus.SelectedIndex = 0;
        //chkDept.Checked = false;
        //chkstaff.Checked = false;
        //ddldept.Visible = false;
        //ddlstafftype.Visible = false;
        ddlCollege.SelectedIndex = 0;
        ddlstafftype.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        ddlEmp.Visible = false;
        //trddldept.Visible = false;
        //trddlstaff.Visible = false;
        tremp.Visible = false;
        divnote.Visible = false;
    }
    protected void ddlstafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        FillStaffType();
        FillEmployee();
    }
    protected void txtTodt_TextChanged1(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtTodt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtTodt.Text != string.Empty && txtTodt.Text != "__/__/____" && txtFromdt.Text != string.Empty && txtFromdt.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromdt.Text);
                DtTo = Convert.ToDateTime(txtTodt.Text);
                if (DtTo < DtFrom)
                {
                    MessageBox("To Date Should be Greater than  or equal to From Date");
                    txtTodt.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtTodt.Text = string.Empty;
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnReportnew_Click(object sender, EventArgs e)
    {

        ShowReportformat1("LeaveApproval", "LeaveStatusAuthoeity.rpt");
    }

    private void ShowReportformat1(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int empno = 0;
            int deptno = 0;
            int staffno = 0;
            int Collegeno = 0;
            string status;
            if (rblAllParticular.SelectedValue == "0")
            {
                empno = 0;
            }
            else
            {
                empno = Convert.ToInt32(ddlEmp.SelectedValue);
            }
            if (ddlCollege.SelectedIndex > 0)
            {
                Collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                Collegeno = 0;
            }
            if (ddldept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }
            if (ddlstafftype.SelectedIndex > 0)
            {
                staffno = Convert.ToInt32(ddlstafftype.SelectedValue);
            }
            else
            {
                staffno = 0;
            }
            if (rdbleavestatus.SelectedValue == "0")
            {
                status = "A";
            }
            else if (rdbleavestatus.SelectedValue == "2")
            {
                //
                status = "R";
            }
            else
            {
                status = "P";
            }

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + ",@P_STATUS=" + status.ToString().Trim() + ",@P_EMPNO=" + empno + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            //url += "&param=@P_COLLEGE_NO=" + Collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + ",@P_EMPNO=" + empno + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_STATUS=" + status.ToString().Trim() + "";
            url += "&param=@P_COLLEGE_NO=" + Collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + ",@P_EMPNO=" + empno + "," + "@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STATUS=" + status.ToString().Trim() + "";
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";



        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
}
