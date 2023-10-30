//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : Attendance_lock.aspx                                                  
// CREATION DATE : 25-05-2011                                                        
// CREATED BY    : Kumar Premankit
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_Attendance_lock : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    lockUlockAttendanceController objLokSalCon = new lockUlockAttendanceController();

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

                BindListViewList();
                this.FillDept();
                this.FillStaffDropdown();
                FillCollege();
                PnlLockPermantely.Visible = false;

                btnLockAttendance.Visible = true;
                btnSave.Visible = true;
                butLockAttendancePermanently.Visible = false;
                butBack.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=Attendance_lock.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Attendance_lock.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");



    }

    private void BindListViewList()
    {
        try
        {
            pnlLockUnlock.Visible = true;
            DataSet ds = objLokSalCon.GetStaffAttendFile();
            lvLockUnlock.DataSource = ds;
            lvLockUnlock.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                btnLockAttendance.Visible = false;
                btnSave.Visible = false;
            }
            else
            {
                btnLockAttendance.Visible = true;
                btnSave.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butBack_Click(object sender, EventArgs e)
    {
        PnlLockPermantely.Visible = false;
        pnlLockUnlock.Visible = true;
        lblerror.Text = string.Empty;

        btnLockAttendance.Visible = true;
        btnSave.Visible = true;
        butLockAttendancePermanently.Visible = false;
        butBack.Visible = false;

        txtMonthYear.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        ddlstaff.SelectedIndex = 0;
    }

    protected void btnLockAttendance_Click(object sender, EventArgs e)
    {
        lblmsg.Text = null;
        lblerror.Text = null;
        PnlLockPermantely.Visible = true;
        pnlLockUnlock.Visible = false;

        btnLockAttendance.Visible = false;
        btnSave.Visible = false;
        butLockAttendancePermanently.Visible = true;
        butBack.Visible = true;
        
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (ListViewDataItem lvitem in lvLockUnlock.Items)
            {
                TextBox txt = lvitem.FindControl("txtYesNo") as TextBox;

                CustomStatus cs = (CustomStatus)objLokSalCon.UpdateUnlockAttendnace(Convert.ToInt32(txt.ToolTip), txt.Text);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
                if (count == 1)
                {
                    //lblerror.Text = null;
                    //lblmsg.Text = "Record Updated Successfully";
                    objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);

                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.btnSub_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butLockAttendancePermanently_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkAttendProcess() == 0)
            {
                //lblerror.Text = "Salary is not processed for" + " " + MonthYear();
                objCommon.DisplayMessage(UpdatePanel1, "Attendance is not processed for" + " " + MonthYear(), this);

            }
            else
            {

                //CustomStatus cs = (CustomStatus)objLokSalCon.UpdatelockAttendance(txtMonthYear.Text, Convert.ToDateTime(System.DateTime.Now).Date.ToString(),Convert.ToInt32(ddldeptLock.SelectedValue));
                CustomStatus cs = (CustomStatus)objLokSalCon.UpdatelockAttendance(txtMonthYear.Text, Convert.ToDateTime(System.DateTime.Now).Date.ToString(), Convert.ToInt32(ddlstaff.SelectedValue),Convert.ToInt32(ddlCollege.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //lblerror.Text = null;
                    //lblmsg.Text = "Salary Locked Permanently for" + " " + MonthYear();
                    objCommon.DisplayMessage(UpdatePanel1, "Attendance locked permanently for" + " " + MonthYear(), this);
                    //pnlLockUnlock.Visible = true;
                    //PnlLockPermantely.Visible = false;
                    //BindListViewList();

                    //btnLockAttendance.Visible = true;
                    //btnSave.Visible = true;
                    //butLockAttendancePermanently.Visible = false;
                    //butBack.Visible = false;

                    //txtMonthYear.Text = string.Empty;
                    ddlCollege.SelectedIndex = 0;
                    ddlstaff.SelectedIndex = 0;
                    
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.butLockSalaryPermanently_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected string MonthYear()
    {
        string monYear = string.Empty;
        try
        {

            monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_Attend.MonthYear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return monYear;
    }

    protected int checkAttendProcess()
    {
        int count = 0;
        try
        {
            //count = Convert.ToInt32(objCommon.LookUp("payroll_attendfile", "count(*)", "monyear='" + MonthYear() + "' "));

            string monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();
            count = Convert.ToInt32(objCommon.LookUp("payroll_attendfile", "count(*)", "monyear='" + monYear + "' AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STNO=" + Convert.ToInt32(ddlstaff.SelectedValue)));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_attend.checkattendProcess-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return count;
    }

    protected void FillDept()
    {
        try
        {
            objCommon.FillDropDownList(ddldeptLock , "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_attendanceProcess.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void FillStaffDropdown()
    {
        try
        {

            //objCommon.FillDropDownList(ddlstaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlstaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
