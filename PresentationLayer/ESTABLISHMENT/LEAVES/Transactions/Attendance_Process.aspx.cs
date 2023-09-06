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
using System.Threading;

public partial class ESTABLISHMENT_LEAVES_Transactions_Attendance_Process : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaveAtd = new LeavesController();

    #region PageLoad
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
        divMsg.InnerHtml = string.Empty;
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 180000;
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
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

            //int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
            //int prevyr = System.DateTime.Today.AddYears(-1).Year;
            //int month = System.DateTime.Today.Month;
            //int year = System.DateTime.Today.Year;
            //string frmdt = null;
            //if (month == 1)
            //{
            //    frmdt = "21" + "/" + "12" + "/" + prevyr.ToString();
            //}
            //else
            //{
            //    frmdt = "21" + "/" + prevmonth.ToString() + "/" + year.ToString();
            //}


            //string todt = "20" + "/" + month.ToString() + "/" + year.ToString();

            //txtMonthYear.Text = frmdt;
            //txtTodt.Text = todt;
            txtMonthYear.Text = System.DateTime.Now.ToString("MM/yyyy");

            butAttendanceProcess.Text = "Leave Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";

            //CeTodt.SelectedDate = System.DateTime.Today;
            //string txt;
            //txt = System.DateTime.Today.ToString("MMMyyyy");
            //butAttendanceProcess.Text = "Attendance Process For" + " " + txt + " " + " Month ";
            //trspace.Visible = false;
            trselection.Visible = true;
            this.FillCollege();
            this.FillStaffType();
            FillDepartment();
            //lbltemp.Visible = false;


        }
        else
        {
            ceMonthYear.SelectedDate = null;
        }

    }
    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    lbltemp.Text = Convert.ToString(Session["STATUS"]);
    //    //Session["STATUS"] = null;
    //}
    #endregion
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Attendance_Process.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Attendance_Process.aspx");
        }
    }

    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {
        butAttendanceProcess.Text = "Leave Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    }

    #region Methods

    //To Get Caption On Attendance process Button of Selecting respective month.
    //protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    //{
    //    //butAttendanceProcess.Text = "Leave Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    //}

    protected void radSelectedEmployees_CheckedChanged(object sender, EventArgs e)
    {
        tremployee.Visible = true;
        FillEmployee();
    }

    protected void radAllEmployees_CheckedChanged(object sender, EventArgs e)
    {
        tremployee.Visible = false;
    }

    protected void butAttendanceProcess_Click(object sender, EventArgs e)
    {
        try
        {
            //string message = CheckEmployee();
            //if (message != "")
            //{
            //    if (radSelectedEmployees.Checked)
            //    {

            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage(message + " is not submitted. Please submit it for attendance process for selected college and staff type.", this);
            //        return;
            //    }
            //}

            string status = string.Empty;
            ////DateTime frmdt = Convert.ToDateTime(txtMonthYear.Text);
            ////string[] dt = frmdt.GetDateTimeFormats();

            ////int frmmonth = frmdt.Month;
            ////int frmyr = frmdt.Year;
            ////DateTime  dtt = frmdt.Date;


            lblProcess.Visible = true;
            // trprocess.Visible = true;
            //string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtMonthYear.Text)));
            //Fdate = Fdate.Substring(0, 10);
            //string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            //Ldate = Ldate.Substring(0, 10);
            string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
            DateTime dt = Convert.ToDateTime(ToDate);
            int month = dt.Month;
            int year = dt.Year;
            string frmdt = null;
            string todt = null;
            int ProcessFromDay = 0;
            int ProcessToDay = 0;


            DataSet Ds = objCommon.FillDropDown("PAYROLL_LEAVE_CONFIGURATION", "isnull(ProcessFromDay,0) as ProcessFromDay", "isnull(ProcessToDay,0) as ProcessToDay", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "AND STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "");


            if (Ds.Tables[0].Rows.Count > 0)
            {

                ProcessFromDay = Convert.ToInt32(Ds.Tables[0].Rows[0]["ProcessFromDay"].ToString());
                ProcessToDay = Convert.ToInt32(Ds.Tables[0].Rows[0]["ProcessToDay"].ToString());

                if (ProcessFromDay != 0 && ProcessToDay != 0)
                {

                    //if (month == 1)
                    //{
                    //    // frmdt = "01" + "/" + month + "/" + year.ToString();
                    //    frmdt = ProcessFromDay + "/" + month + "/" + year.ToString();
                    //}
                    if (ProcessFromDay == 1)
                    {
                        frmdt = "01" + "/" + month + "/" + year.ToString();
                    }
                    else
                    {
                        //frmdt = "01" + "/" + month + "/" + year.ToString();
                        frmdt = ProcessFromDay + "/" + month + "/" + year.ToString();
                    }
                    // string todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString();
                    //string todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();

                    if (ProcessToDay != 31)
                    {
                        //todt = new DateTime(year, month, ProcessToDay).AddMonths(1).AddDays(-1).ToString();
                        todt = new DateTime(year, month, ProcessToDay).AddMonths(1).AddDays(0).ToString();
                    }
                    else
                    {
                        todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();
                    }


                    string Fdate = (String.Format("{0:u}", Convert.ToDateTime(frmdt)));
                    Fdate = Fdate.Substring(0, 10);
                    string Tdate = (String.Format("{0:u}", Convert.ToDateTime(todt)));
                    Tdate = Tdate.Substring(0, 10);



                    //int ret=checkLastMonthAttendProcess();
                    //if (ret == 1)
                    //{
                    //    lblProcess.Visible = false;
                    //    return;
                    //}
                    lblerror.Text = "";
                    if (checkAttendProcess() > 0)
                    {
                        if (radSelectedEmployees.Checked)
                        {
                            string employeelist = string.Empty;
                            for (int i = 0; i < lstEmployee.Items.Count; i++)
                            {
                                if (lstEmployee.Items[i].Selected)
                                {
                                    employeelist += lstEmployee.Items[i].Value + ",";
                                }
                            }
                            employeelist = employeelist.Substring(0, employeelist.Length - 1);

                            status = objLeaveAtd.LeaveAttendanceProcessnew(employeelist, Convert.ToInt32(Session["idno"].ToString()), Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
                            //Added above line by Saket Singh on 13-Dec-2016
                            //status = objLeaveAtd.LeaveAttendanceProcess(employeelist, Convert.ToInt32(Session["idno"].ToString()), Fdate, Ldate, Convert.ToInt32(ddlDept.SelectedValue), Session["colcode"].ToString());
                            if (status != null || status != "" || status != string.Empty)
                            {
                                //MessageBox(status);
                                //objCommon.DisplayMessage(UpdatePanel1, status, this.Page);
                                //MessageBox.Show(status);
                                lblProcess.Visible = false;
                                // trprocess.Visible = false;
                                objCommon.DisplayMessage(status, this);
                                Clear();
                            }
                        }
                        else
                        {
                            status = objLeaveAtd.LeaveAttendanceProcessnew("0", Convert.ToInt32(Session["idno"].ToString()), Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
                            //Added above line by Saket Singh on 13-Dec-2016
                            //status = objLeaveAtd.LeaveAttendanceProcess("0", Convert.ToInt32(Session["idno"].ToString()), Fdate, Ldate, Convert.ToInt32(ddlDept.SelectedValue), Session["colcode"].ToString());

                            if (status != null || status != "" || status != string.Empty)
                            {

                                //MessageBox(status);
                                //objCommon.DisplayMessage(UpdatePanel1, status, this.Page);
                                //MessageBox("ATTENDANCE PROCESS COMPLETED SUCCESSFULLY!!");
                                //MessageBox.Show(status);
                                lblProcess.Visible = false;
                                //trprocess.Visible = false;
                                objCommon.DisplayMessage(status, this);
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        status = objLeaveAtd.LeaveAttendanceProcessnew("0", Convert.ToInt32(Session["idno"].ToString()), Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
                        //Added above line by Saket Singh on 13-Dec-2016
                        //status = objLeaveAtd.LeaveAttendanceProcess("0", Convert.ToInt32(Session["idno"].ToString()), Fdate, Ldate, Convert.ToInt32(ddlDept.SelectedValue), Session["colcode"].ToString());

                        if (status != null || status != "" || status != string.Empty)
                        {
                            //objUCommon.ShowError(Page, status);
                            // objCommon.DisplayMessage(UpdatePanel1, status, this.Page);
                            // MessageBox.Show(status);
                            lblProcess.Visible = false;
                            //trprocess.Visible = false;
                            objCommon.DisplayMessage(status, this);
                            Clear();
                        }
                    }
                    System.Threading.Thread.Sleep(5000);
                }
                else
                {
                    MessageBox("Please Check Attendance Configuration !!");
                    return;
                }
            }
            else
            {
                MessageBox("Attendance Configuration Not Set !!");
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected int checkAttendProcess()
    {
        int count = 0;
        try
        {
            string monYear = string.Empty;
            monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
            count = Convert.ToInt32(objCommon.LookUp("payroll_attendfile", "count(*)", "monyear='" + monYear + "' "));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_attendanceProcess.checkAttendProcess-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return count;
    }

    //protected void txtTodt_TextChanged(object sender, EventArgs e)
    //{
    //    butAttendanceProcess.Text = "Leave Process For" + " " + Convert.ToDateTime(txtTodt.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    //}

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        tremployee.Visible = false;
        radAllEmployees.Checked = true;
        radSelectedEmployees.Checked = false;

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)  //Added by Saket Singh on 13-Dec-2016 .
    {
        FillDepartment();
        radAllEmployees.Checked = true;
        radSelectedEmployees.Checked = false;
        tremployee.Visible = false;
    }


    #endregion

    #region Actions

    //Fill Dropdown with Employee .
    protected void FillEmployee()
    {
        try
        {
            // objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS E inner JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "'[' + convert(nvarchar(50),E.idno) +']'+ ' ' + isnull(E.fname,'') + ' ' + isnull(E.mname,'') + ' ' + isnull(E.lname,'') as employeename", "E.IDNO>0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and (E.SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)" + " AND E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "employeename");
            string todt = string.Empty;
            if (txtMonthYear.Text != string.Empty && txtMonthYear.Text.ToString() != "__/__/____")
            {
                todt = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
                objCommon.FillListBox(lstEmployee, "PAYROLL_EMPMAS", "IDNO", "'[' + convert(nvarchar(50),idno) +']'+ ' ' + isnull(fname,'') + ' ' + isnull(mname,'') + ' ' + isnull(lname,'') as employeename", "IDNO>0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and (SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)" + " AND STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "IDNO");
            }
            else
            {

                lstEmployee.Items.Clear();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_attendanceProcess.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    //Added by Mrunal Bansod .
    //to Fill Department.For filtering Employees.

    private void FillDepartment()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCollege()
    {
        //Added by Saket Singh on 13-Dec-2016 .
        //to Fill College.For filtering college.
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }

    private void FillStaffType()
    {
        //Added by Saket Singh on 13-Dec-2016 .
        //to Fill Staff Type.For filtering staff type.
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    //To popup the messagebox.
    //public void MessageBox(string msg)
    //{
    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    //}


    // Added By Shrikant Bharne.
    protected int checkLastMonthAttendProcess()
    {
        //int count = 0;
        int ret = 0;
        try
        {
            string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
            DateTime dt = Convert.ToDateTime(ToDate);
            int month = dt.Month;
            int previousmonth = month - 1;

            DateTime dtDate = new DateTime(2000, previousmonth, 1);
            string sMonthName = dtDate.ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();

            string monYear = string.Empty;
            // monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
            monYear = sMonthName;
            // bool count = Convert.ToInt32(objCommon.LookUp("PAYROLL_ATTENDFILE", "ATTENDLOCK", "monyear='" + monYear + "' AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue)));           
            bool count = Convert.ToBoolean(objCommon.LookUp("PAYROLL_ATTENDFILE", "ATTENDLOCK", "monyear='" + monYear + "' AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue)));
            //return count;

            if (count == false)
            {
                MessageBox("Please Permanent Lock Previous Month Attendance");
                ret = 1;
            }
            else
            {
                ret = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_attendanceProcess.checkAttendProcess-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        //return count;
        return ret;
    }


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }

    #region Check remaining employee
    /*protected string CheckEmployee()
    {
        string Msg = string.Empty;
        //int i = 0; 
        int count_record = 0;
        try
        {
            DataSet ds = null;

            int month = System.DateTime.Today.Month;
            int year = System.DateTime.Today.Year;
            string frmdt = null;

            if (month == 1)
            {
                frmdt = "01" + "/" + month + "/" + year.ToString();
            }
            else
            {
                frmdt = "01" + "/" + month + "/" + year.ToString();
            }
            string todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString();


            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(frmdt)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(todt)));
            Tdate = Tdate.Substring(0, 10);
            ds = objLeaveAtd.GetThumbPrblmList(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //i++;
                Msg = "Forgot Punch of General Employee";
            }
            ds = objLeaveAtd.GetThumbPrblmList_Shift(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // i++;
                if (Msg == "")
                    Msg = "Forgot Punch of Shift Module Employee";
                else
                    Msg = Msg + ", " + "Forgot Punch of Shift Module Employee";
            }
            ds = objLeaveAtd.GetThumbPrblmList_NonRegistered(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // i++;
                if (Msg == "")
                    Msg = "Not Registered of General Employee";
                else
                    Msg = Msg + ", " + "Not Registered of General Employee";
            }
            ds = objLeaveAtd.GetThumbPrblmList_NonRegistered_Shift(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //i++;
                if (Msg == "")
                    Msg = "Not Registered of Shift Module Employee";
                else
                    Msg = Msg + ", " + "Not Registered of Shift Module Employee";
            }
            //ds = objLeaveAtd.GetLateComersEmpList(Convert.ToDateTime(txtMonthYear.Text), Convert.ToDateTime(txtTodt.Text), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), count_record);
            ds = objLeaveAtd.GetLoginInfoByDate(Convert.ToDateTime(frmdt), Convert.ToDateTime(todt), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //i++;
                if (Msg == "")
                    Msg = "Late Coming of General Employee";
                else
                    Msg = Msg + ", " + "Late Coming of General Employee";
            }
            ds = objLeaveAtd.GetLateComersEmpList_Shift(Convert.ToDateTime(frmdt), Convert.ToDateTime(todt), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //i++;
                if (Msg == "")
                    Msg = "Late Coming of Shift Module Employee";
                else
                    Msg = Msg + ", " + "Late Coming of Shift Module Employee";
            }
            ds = objLeaveAtd.GetEarlyGoingEmpList(Fdate, Tdate, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), count_record);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //i++;
                if (Msg == "")
                    Msg = "Early going of General Employee";
                else
                    Msg = Msg + ", " + "Early going of General Employee";
            }
            ds = objLeaveAtd.GetEarlyGoingEmpList_Shift(Fdate, Tdate, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), count_record);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //i++;
                if (Msg == "")
                    Msg = "Early going of Shift Module Employee";
                else
                    Msg = Msg + ", " + "Early going of Shift Module Employee";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Attendance_Process.CheckEmployee ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
        return Msg;
    }*/
    #endregion

    private void Clear()
    {
        ddlStafftype.SelectedIndex = ddlCollege.SelectedIndex = 0;
        txtMonthYear.Text = System.DateTime.Now.ToString("MM/yyyy");
        radAllEmployees.Checked = true;
        radSelectedEmployees.Checked = false;
        tremployee.Visible = false;
    }

}
