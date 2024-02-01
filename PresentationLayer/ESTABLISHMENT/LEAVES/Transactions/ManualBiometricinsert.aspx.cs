using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using System.Globalization;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_ManualBiometricinsert : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BioMetricsController bio = new BioMetricsController();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    pnlSelect.Visible = true;
                    pnlAttendance.Visible = false;
                    FillCollege();
                    FillStaffType();
                    FillDepartment();
                    //cetxtStartDate.SelectedDate = System.DateTime.Today;
                    //string txt;
                    //txt = System.DateTime.Today.ToString("MM/yyyy");

                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Attendance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Attendance.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        

    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");

            // objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO <> 0", "SUBDEPTNO");
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
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillUser();
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillUser();
        }
        catch (Exception ex)
        {

        }
    }
    //protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            int IDNO = 0;
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtMonthYear.Text)));
            Fdate = Fdate.Substring(0, 10);
            //string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtMonthYear.Text)));
            //Ldate = Ldate.Substring(0, 10);
            DateTime fromdatenew = Convert.ToDateTime(txtMonthYear.Text);

            string todt = new DateTime(fromdatenew.Year, fromdatenew.Month, 1).AddMonths(1).AddDays(-1).ToString();

            todt = (String.Format("{0:u}", Convert.ToDateTime(todt)));
            todt = todt.Substring(0, 10);

            IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
            DataSet ds = null;
            ds = bio.GetManuallogBiometric(Convert.ToDateTime(Fdate), Convert.ToDateTime(todt), IDNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int islock = Convert.ToInt32(ds.Tables[0].Rows[0]["salprocess"].ToString());

                if (islock == 0)
                {
                    pnlAttendance.Visible = false;
                    MessageBox("For Select Employee salary not Process and Locked for select Month and Year");
                    btnSubmit.Visible = false;
                    return;
                }
                else
                {
                    pnlAttendance.Visible = true;
                    lvAttendance.Visible = true;
                    lvAttendance.DataSource = ds.Tables[0];
                    lvAttendance.DataBind();
                    btnSubmit.Visible = true;
                }
            }
            else
            {
                MessageBox("No Records found.");
                btnSubmit.Visible = false;
                return;
            }



        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtAppRecord = new DataTable();
            dtAppRecord.Columns.Add("RFID");
            dtAppRecord.Columns.Add("DATE");
            dtAppRecord.Columns.Add("INTTIME");
            dtAppRecord.Columns.Add("OUTTTIME");



            foreach (ListViewDataItem items in lvAttendance.Items)
            {
                CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
                TextBox txtDate = items.FindControl("txtDate") as TextBox;
                TextBox txtInTime = items.FindControl("txtInTime") as TextBox;
                TextBox txtOutTime = items.FindControl("txtOutTime") as TextBox;

                int Rfidno = Convert.ToInt32(chkSelect.ToolTip);

                if (chkSelect.Checked)
                {
                    if ((txtInTime.Text != string.Empty && txtInTime.Text != "__:__:__" && txtInTime.Text.ToString().Trim() != "00:00:00".ToString().Trim()) && (txtOutTime.Text != string.Empty && txtOutTime.Text != "__:__:__" && txtOutTime.Text.ToString().Trim() != "00:00:00".ToString().Trim()))
                    {
                        DataRow dr = dtAppRecord.NewRow();
                        dr["RFID"] = chkSelect.ToolTip.ToString();
                        string dt = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd").Trim();
                        dr["DATE"] = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd").Trim();
                        dr["INTTIME"] = Convert.ToDateTime(dt + (txtInTime.Text.Trim() == "" ? " 00:00:00" : " " + txtInTime.Text.Trim())).ToString("yyyy-MM-dd hh:mm:ss tt");
                        dr["OUTTTIME"] = Convert.ToDateTime(dt + (txtOutTime.Text.Trim() == "" ? " 00:00:00" : " " + txtOutTime.Text.Trim())).ToString("yyyy-MM-dd hh:mm:ss tt");


                        dtAppRecord.Rows.Add(dr);
                        dtAppRecord.AcceptChanges();
                    }
                }
            }

            string Userid = Session["userno"].ToString();
            if (dtAppRecord.Rows.Count > 0)
            {
                int cs = Convert.ToInt32(bio.LoginDetailsUpdation(dtAppRecord, Userid));//here Session_service_srno will create & insert through this procedure
                if (cs == 1)
                {
                    objCommon.DisplayMessage("Records Updated Sucessfully", this);
                    MessageBox("Records Added Successfully");
                    clear();

                }
            }
            else
            {
                MessageBox("Please select atleast one Employee");
                return;
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected int checkSalaryProcess()
    {
        string monYear;
        int count = 0;
        string count1 = "";
        DateTime Test;
        if (DateTime.TryParseExact(txtMonthYear.Text, "MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtMonthYear.Text != string.Empty && txtMonthYear.Text != "__/__/____")
            {
                monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
                int Staffno = Convert.ToInt32(objCommon.LookUp("Payroll_empmas", "staffno", "idno='" + Convert.ToInt32(ddlEmployee.SelectedValue)));
                count = Convert.ToInt32(objCommon.LookUp("" + monYear + " N Inner Join payroll_salfile S ON N.StaffNo=S.StaffNo", "count(*)", "monyear='" + monYear + "' and staffno=" + Staffno + "'and IDNO=" + Convert.ToInt32(ddlEmployee.SelectedValue)));
            }
        }
        else
        {
            txtMonthYear.Text = string.Empty;
        }
        return count;
    }

    private void FillUser()
    {
        try
        {
            int STNO, DEPTNO = 0;
            if (ddlStafftype.SelectedIndex > 0)
            {
                STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            }
            else
            {
                STNO = 0;
            }
            if (ddlDepartment.SelectedIndex > 0)
            {
                DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            else
            {
                DEPTNO = 0;
            }


            if (ddlCollege.SelectedIndex >0 && ddlStafftype.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'')+' - ' +isnull(convert(nvarchar(20),RFIDNO),'') as ENAME", "STNO=" + ddlStafftype.SelectedValue + " and SUBDEPTNO=" + DEPTNO + " and COLLEGE_NO=" + ddlCollege.SelectedValue + "", "ENAME");

            }
            else if (ddlCollege.SelectedIndex >0 && ddlStafftype.SelectedIndex > 0 && ddlDepartment.SelectedIndex == 0)
            {
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'')+' - ' +isnull(convert(nvarchar(20),RFIDNO),'') as ENAME", "STNO=" + ddlStafftype.SelectedValue + " and COLLEGE_NO=" + ddlCollege.SelectedValue + "", "ENAME");
            }
            else if (ddlCollege.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'')+' - ' +isnull(convert(nvarchar(20),RFIDNO),'') as ENAME", "SUBDEPTNO=" + ddlDepartment.SelectedValue + " and COLLEGE_NO=" + ddlCollege.SelectedValue + "", "ENAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportEmployeeBiometric("Employee_Attendance_Report", "logindetailreport.rpt");
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowReportEmployeeBiometric(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = 0;
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtMonthYear.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);

            DateTime fromdatenew = Convert.ToDateTime(txtMonthYear.Text);
            string todt = new DateTime(fromdatenew.Year, fromdatenew.Month, 1).AddMonths(1).AddDays(-1).ToString();
            string todate = (String.Format("{0:u}", Convert.ToDateTime(todt)));
            todt = todate.Substring(0, 10);

            if (ddlEmployee.SelectedIndex > 0)
            {
                IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                MessageBox("Please Select Employee ");
                return;
            }



            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;

            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;


            //url += "&param=@P_FROMDATE=" + Fdate + ",@P_TODATE=" + todt + ",@P_IDNO=" + IDNO + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_FROMDATE=" + Fdate + ",@P_TODATE=" + todt + ",@P_IDNO=" + IDNO + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void clear()
    {
        try
        {
            ddlCollege.SelectedIndex = 0;
            ddlStafftype.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlEmployee.SelectedIndex = 0;
            txtMonthYear.Text = string.Empty;

            pnlAttendance.Visible = false;
            lvAttendance.DataSource = null;
        }
        catch (Exception ex)
        {
        }

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillUser();
        }
        catch (Exception ex)
        {
        }
    }
}