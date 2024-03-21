//==================================================================//
// Created By   : Shrikant Bharne.
// Created Date : 09-08-2019.
// Purpose      : To Show Late & Earily Employee.
//==================================================================//
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using IITMS.SQLServer.SQLDAL;


public partial class ESTABLISHMENT_LEAVES_Reports_LateEarilyReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();
    DataTable dtBefore = new DataTable();
    DataTable dtAfter = new DataTable();
    static int reason_no = 0;

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillCollege();
                FillDepartment();
                this.FillDesignation();
                //trdept.Visible = false;
                CheckPageAuthorization();
            }

            dtBefore = setGridViewDataset(dtBefore, "before").Clone();
            ViewState["dtBefore"] = dtBefore;
            dtAfter = setGridViewDataset(dtAfter, "after").Clone();
            ViewState["dtAfter"] = dtAfter;

            int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
            int prevyr = System.DateTime.Today.AddYears(-1).Year;
            int month = System.DateTime.Today.Month;
            int year = System.DateTime.Today.Year;
            string frmdt = null;
            if (month == 1)
            {
               // frmdt = "21" + "/" + "12" + "/" + prevyr.ToString();
            }
            else
            {
              //  frmdt = "21" + "/" + prevmonth.ToString() + "/" + year.ToString();
            }


          //  string todt = "20" + "/" + month.ToString() + "/" + year.ToString();

           // txtFromDt.Text = frmdt;
           // txtToDt.Text = todt;


        }
        else
        {
            dtBefore = (DataTable)ViewState["dtBefore"];
            dtBefore.Clear();
            dtAfter = (DataTable)ViewState["dtAfter"];
            dtAfter.Clear();

        }
        //blank div tag
        divMsg.InnerHtml = string.Empty;
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }
    private void FillDepartment()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillDesignation()
    {
        try
        {
            objCommon.FillDropDownList(ddlDesig, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIG");
            //objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void Clear()
    {
        ddlDept.SelectedIndex = 0;
        txtFromDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        ddlStaffType.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        lvEmpEarly.DataSource = null;
        lvEmpEarly.DataBind();
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();
        lvLateComers.DataSource = null;
        lvLateComers.DataBind();
        rblcondn.ClearSelection();
        ViewState["selectedDates"] = null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblcondn.SelectedValue == "0")
                this.SaveThumbPrblmApproval();
            else if (rblcondn.SelectedValue == "1")
                this.SaveLateComersApproval();
            else if (rblcondn.SelectedValue == "2")
                this.SaveEarlyGoingApproval();
            else if (rblcondn.SelectedValue == "3")
                this.SaveThumbPrblmApproval_NonRegistered();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //Method to save thumb forgot problem approval of employee
    protected void SaveThumbPrblmApproval()
    {
        Leaves objLeave = new Leaves();

        int checkcount = 0;
        int instCount = 0;
        string selectedIDs = string.Empty;
        foreach (ListViewDataItem lvItem in lvEmpList.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            Label lbldt = lvItem.FindControl("lbldate") as Label;
            Label lblintime = lvItem.FindControl("lblIntime") as Label;
            Label lblouttime = lvItem.FindControl("lblOutTime") as Label;
            DropDownList ddlwrk = lvItem.FindControl("ddlWorkType") as DropDownList;
            DropDownList ddlAllow = lvItem.FindControl("ddlAllow") as DropDownList;
            DropDownList ddlReason = lvItem.FindControl("ddlReason") as DropDownList;

            string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
            if (chk.Checked == true)
            {
                checkcount += 1;
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";

                objLeave.EMPNO = Convert.ToInt32(chk.ToolTip);
                objLeave.DATE = Convert.ToDateTime(lbldt.Text);
                objLeave.INTIME = lblintime.Text;
                objLeave.OUTTIME = lblouttime.Text;
                objLeave.EMPNAME = chk.Text.ToString();
                //objLeave.WTNO = Convert.ToInt32(ddlwrk.SelectedValue);
                objLeave.STATUS = ddlAllow.SelectedValue;
                objLeave.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                if (ddlAllow.SelectedValue == "N")
                {
                    string resno = objCommon.LookUp("PAYROLL_LEAVE_REASON", "REASONNO", "REASON='REJECTED'");
                    ddlReason.SelectedValue = resno;
                }
                if (ddlReason.SelectedIndex == 0)
                {
                    MessageBox("Please Select Allow Reason For Selected Record");
                    return;
                }
                else
                {
                    objLeave.RESNO = Convert.ToInt32(ddlReason.SelectedValue);
                }
                CustomStatus cs = (CustomStatus)objApp.AddThumbPrblm(objLeave);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    instCount = 1;
                }

            }



        }
        if (checkcount == 0)
        {
            MessageBox("Please Select Atleast One Employee");
            return;
        }


        selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
        string idno = selectedIDs.Replace('$', ',');

        //string[] strTitleNo = idno.Trim().Split(',');

        //int i = 0;
        //for (i = 0; i <= strTitleNo.Length - 1; i++)
        //{
        //    string id = strTitleNo[i];
        //    Leaves  objLeave= new Leaves();
        //    objLeave.EMPNO = Convert.ToInt32(id);
        //    objLeave.EMPNAME = 
        //    objShifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

        //    DateTime changedate = Convert.ToDateTime(txtFromDt.Text);
        //    while (changedate <= Convert.ToDateTime(txtToDt.Text))
        //    {
        //        objShifts.DATE = changedate;
        //        CustomStatus cs = (CustomStatus)objShift.AddAssignShift(objShifts);
        //        if (cs.Equals(CustomStatus.RecordSaved))
        //        {
        //            instCount = 1;
        //            ViewState["action"] = "add";
        //            changedate = changedate.AddDays(1);
        //        }
        //    }

        //}
        if (instCount == 1)
        {
            MessageBox("Record saved successfully");
        }
        BindListViewModifyThumb();

    }
    //Method to save thumb Non Registered approval of employee
    protected void SaveThumbPrblmApproval_NonRegistered()
    {
        Leaves objLeave = new Leaves();

        int checkcount = 0;
        int instCount = 0;
        string selectedIDs = string.Empty;
        foreach (ListViewDataItem lvItem in lvNREmpList.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            Label lbldt = lvItem.FindControl("lbldate") as Label;
            Label lblintime = lvItem.FindControl("lblIntime") as Label;
            Label lblouttime = lvItem.FindControl("lblOutTime") as Label;
            DropDownList ddlwrk = lvItem.FindControl("ddlWorkType") as DropDownList;
            DropDownList ddlAllow = lvItem.FindControl("ddlAllow") as DropDownList;
            DropDownList ddlReason = lvItem.FindControl("ddlReason") as DropDownList;

            string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
            if (chk.Checked == true)
            {
                checkcount += 1;
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";

                objLeave.EMPNO = Convert.ToInt32(chk.ToolTip);
                objLeave.DATE = Convert.ToDateTime(lbldt.Text);
                objLeave.INTIME = lblintime.Text;
                objLeave.OUTTIME = lblouttime.Text;
                objLeave.EMPNAME = chk.Text.ToString();
                objLeave.WTNO = Convert.ToInt32(ddlwrk.SelectedValue);
                objLeave.STATUS = ddlAllow.SelectedValue;
                objLeave.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                if (ddlAllow.SelectedValue == "N")
                {
                    string resno = objCommon.LookUp("PAYROLL_LEAVE_REASON", "REASONNO", "REASON='REJECTED'");
                    ddlReason.SelectedValue = resno;
                }
                if (ddlReason.SelectedIndex == 0)
                {
                    MessageBox("Please Select Allow Reason For Selected Record");
                    return;
                }
                else
                {
                    objLeave.RESNO = Convert.ToInt32(ddlReason.SelectedValue);
                }
                CustomStatus cs = (CustomStatus)objApp.AddThumbPrblm_NonRegistered(objLeave);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    instCount = 1;
                }

            }



        }
        if (checkcount == 0)
        {
            MessageBox("Please Select Atleast One Employee");
            return;
        }


        selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
        string idno = selectedIDs.Replace('$', ',');

        //string[] strTitleNo = idno.Trim().Split(',');

        //int i = 0;
        //for (i = 0; i <= strTitleNo.Length - 1; i++)
        //{
        //    string id = strTitleNo[i];
        //    Leaves  objLeave= new Leaves();
        //    objLeave.EMPNO = Convert.ToInt32(id);
        //    objLeave.EMPNAME = 
        //    objShifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

        //    DateTime changedate = Convert.ToDateTime(txtFromDt.Text);
        //    while (changedate <= Convert.ToDateTime(txtToDt.Text))
        //    {
        //        objShifts.DATE = changedate;
        //        CustomStatus cs = (CustomStatus)objShift.AddAssignShift(objShifts);
        //        if (cs.Equals(CustomStatus.RecordSaved))
        //        {
        //            instCount = 1;
        //            ViewState["action"] = "add";
        //            changedate = changedate.AddDays(1);
        //        }
        //    }

        //}
        if (instCount == 1)
        {
            MessageBox("Record saved successfully");
        }
        BindListViewModifyThumb_NR();

    }

    //Method to save thumb Late Comers approval of employee
    protected void SaveLateComersApproval()
    {
        Leaves objLeave = new Leaves();

        int checkcount = 0;
        int instCount = 0;
        string selectedIDs = string.Empty;
        foreach (ListViewDataItem lvItem in lvLateComers.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            Label lbldt = lvItem.FindControl("lbldate") as Label;
            Label lblintime = lvItem.FindControl("lblIntime") as Label;
            Label lblShiftIntime = lvItem.FindControl("lblShiftIntime") as Label;
            Label lblouttime = lvItem.FindControl("lblOutTime") as Label;
            Label lblhrs = lvItem.FindControl("lblhrs") as Label;
            Label lbllateby = lvItem.FindControl("lblLateby") as Label;

            DropDownList ddlwrk = lvItem.FindControl("ddlWorkType") as DropDownList;
            DropDownList ddlAllow = lvItem.FindControl("ddlAllow") as DropDownList;
            DropDownList ddlReason = lvItem.FindControl("ddlReason") as DropDownList;
            string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
            if (chk.Checked == true)
            {
                checkcount += 1;
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";

                objLeave.EMPNO = Convert.ToInt32(chk.ToolTip);
                objLeave.DATE = Convert.ToDateTime(lbldt.Text);
                objLeave.INTIME = lblintime.Text;
                objLeave.SHIFT_INTIME = lblShiftIntime.Text;
                objLeave.OUTTIME = lblouttime.Text;
                objLeave.EMPNAME = chk.Text.ToString();
                objLeave.HOURS = lblhrs.Text;
                objLeave.LATEBY = lbllateby.Text;
                // objLeave.WTNO = Convert.ToInt32(ddlwrk.SelectedValue);
                objLeave.STATUS = ddlAllow.SelectedValue;
                objLeave.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                if (ddlAllow.SelectedValue == "N")
                {
                    string resno = objCommon.LookUp("PAYROLL_LEAVE_REASON", "REASONNO", "REASON='REJECTED'");
                    ddlReason.SelectedValue = resno;
                }
                if (ddlReason.SelectedIndex == 0)
                {
                    MessageBox("Please Select Allow Reason For Selected Record");
                    return;
                }
                else
                {
                    objLeave.RESNO = Convert.ToInt32(ddlReason.SelectedValue);
                }
                CustomStatus cs = (CustomStatus)objApp.AddLateComersAllow(objLeave);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    instCount = 1;
                }

            }



        }
        if (checkcount == 0)
        {
            MessageBox("Please Select Atleast One Employee");
            return;
        }


        selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
        string idno = selectedIDs.Replace('$', ',');


        if (instCount == 1)
        {
            MessageBox("Record saved successfully");
        }
        //BindLateComersList();
        BindListViewModifyLate();

    }

    //Method to save thumb Late Comers approval of employee
    protected void SaveEarlyGoingApproval()
    {
        Leaves objLeave = new Leaves();

        int checkcount = 0;
        int instCount = 0;
        string selectedIDs = string.Empty;
        foreach (ListViewDataItem lvItem in lvEmpEarly.Items)
        {
            CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
            Label lbldt = lvItem.FindControl("lbldate") as Label;
            Label lblintime = lvItem.FindControl("lblIntime") as Label;
            Label lblouttime = lvItem.FindControl("lblOutTime") as Label;
            Label lblShiftOuttime = lvItem.FindControl("lblShiftOuttime") as Label;
            Label lblLeaveType = lvItem.FindControl("lblLeaveType") as Label;

            DropDownList ddlwrk = lvItem.FindControl("ddlWorkType") as DropDownList;
            DropDownList ddlAllow = lvItem.FindControl("ddlAllow") as DropDownList;
            DropDownList ddlReason = lvItem.FindControl("ddlReason") as DropDownList;
            string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
            if (chk.Checked == true)
            {
                checkcount += 1;
                selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";

                objLeave.EMPNO = Convert.ToInt32(chk.ToolTip);
                objLeave.DATE = Convert.ToDateTime(lbldt.Text);
                objLeave.INTIME = lblintime.Text;
                objLeave.OUTTIME = lblouttime.Text;
                objLeave.EMPNAME = chk.Text.ToString();
                objLeave.SHIFTOUTTIME = lblShiftOuttime.Text;
                objLeave.LEAVETYPE = lblLeaveType.Text;
                // objLeave.WTNO = Convert.ToInt32(ddlwrk.SelectedValue);
                objLeave.STATUS = ddlAllow.SelectedValue;
                objLeave.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                if (ddlAllow.SelectedValue == "N")
                {
                    string resno = objCommon.LookUp("PAYROLL_LEAVE_REASON", "REASONNO", "REASON='REJECTED'");
                    ddlReason.SelectedValue = resno;
                }
                if (ddlReason.SelectedIndex == 0)
                {
                    MessageBox("Please Select Allow Reason For Selected Record");
                    return;
                }
                else
                {
                    objLeave.RESNO = Convert.ToInt32(ddlReason.SelectedValue);
                }
                CustomStatus cs = (CustomStatus)objApp.AddEarlyGoingAllow(objLeave);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    instCount = 1;
                }

            }



        }
        if (checkcount == 0)
        {
            MessageBox("Please Select Atleast One Employee");
            return;
        }


        selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
        string idno = selectedIDs.Replace('$', ',');


        if (instCount == 1)
        {
            MessageBox("Record saved successfully");
        }
        // this.BindListViewEarly();
        this.BindListViewModifyEarly();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowReport("Holidays_Entry", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }



    protected void FillWorkType()
    {
        foreach (ListViewDataItem lvitem in lvEmpList.Items)
        {
            DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
            objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
            ddlWorkType.SelectedValue = "3";

        }
    }

    protected void BindListView()//Forget to Punch
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);



            DataSet ds = objApp.GetThumbPrblmList(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0,0);

            lvEmpList.DataSource = ds;
            lvEmpList.DataBind();
            lvEmpList.Visible = true;

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    //BindListViewNR
    protected void BindListViewNR()//Forget IN & OUT BOTH Punch
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);



            DataSet ds = objApp.GetThumbPrblmList_NonRegistered(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0,0);

            lvNREmpList.DataSource = ds;
            lvNREmpList.DataBind();
            lvNREmpList.Visible = true;
            foreach (ListViewDataItem lvitem in lvNREmpList.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                ddlWorkType.SelectedValue = "4";
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void rblcondn_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();

        lvLateComers.DataSource = null;
        lvLateComers.DataBind();


        lvEmpEarly.DataSource = null;
        lvEmpEarly.DataBind();

        lvNREmpList.DataSource = null;
        lvNREmpList.DataBind();

        lvEmpList.Visible = false; lvLateComers.Visible = false; lvEmpEarly.Visible = false; lvNREmpList.Visible = false;


        //if (rblcondn.SelectedValue == "0")
        //{
        //    this.BindListView();
        //    this.FillWorkType();
        //    lvLateComers.Visible = false;
        //    lvEmpList.Visible = true;
        //    lvEmpEarly.Visible = false;
        //}
        //else if (rblcondn.SelectedValue == "1")
        //{
        //    this.BindLateComersList();
        //    lvEmpList.Visible = false;
        //    lvLateComers.Visible = true;
        //    lvEmpEarly.Visible = false;
        //}
        //else
        //{
        //    this.BindListViewEarly();
        //    lvEmpList.Visible = false;
        //    lvLateComers.Visible = false;
        //    lvEmpEarly.Visible = true;
        //}

        //System.Threading.Thread.Sleep(5000);
    }

    //private void BindLateComersList()
    //{
    //    string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
    //    //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
    //    Fdate = Fdate.Substring(0, 10);
    //    string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
    //    Tdate = Tdate.Substring(0, 10);
    //    DataSet ds = null;
    //    int shiftno;
    //    if (Convert.ToDateTime(txtToDt.Text) <= System.DateTime.Now)
    //    {
    //        //if (Convert.ToDateTime(txtDate.Text).DayOfWeek != DayOfWeek.Sunday)
    //        //{
    //        ds = objApp.GetLoginInfoByDate(Convert.ToDateTime(txtFromDt.Text), Convert.ToDateTime(txtToDt.Text), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue));
    //        ds.Tables[0].Columns.Add("HOURS");
    //        ds.Tables[0].Columns.Add("SHIFT_INTIME");
    //        int rowCount = ds.Tables[0].Rows.Count;
    //        for (int i = 0; i < rowCount; i++)
    //        {
    //            int len = Convert.ToString(ds.Tables[0].Rows[i]["IDNO"]).Length;
    //            ds.Tables[0].Rows[i]["IDNO"] = Convert.ToString(ds.Tables[0].Rows[i]["IDNO"]);
    //            // ds.Tables[0].Rows[i]["REASON_NO"] = Convert.ToString(ds.Tables[0].Rows[i]["REASON_NO"]);
    //            string inTime = Convert.ToString(ds.Tables[0].Rows[i]["INTIME"]);
    //            string outTime = Convert.ToString(ds.Tables[0].Rows[i]["OUTTIME"]);
    //            string date = Convert.ToString(ds.Tables[0].Rows[i]["ENTDATE"]);
    //            string dt = (String.Format("{0:u}", Convert.ToDateTime(date)));
    //            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
    //            dt = dt.Substring(0, 10);
    //            if (inTime == string.Empty)
    //                ds.Tables[0].Rows[i]["INTIME"] = "00:00:00";
    //            if (outTime == string.Empty)
    //                ds.Tables[0].Rows[i]["OUTTIME"] = "00:00:00";
    //            if (inTime != string.Empty && outTime != string.Empty)
    //            {
    //                TimeSpan ts = (Convert.ToDateTime(outTime) - Convert.ToDateTime(inTime));
    //                ds.Tables[0].Rows[i]["HOURS"] = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00");
    //            }
    //            string name = ds.Tables[0].Rows[i]["USERNAME"].ToString();
    //            int idno = Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]);
    //            shiftno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SHIFTNO", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"])));
    //            if (shiftno == 0)
    //            {
    //                MessageBox("Shift Not Found For " + name + "(" + idno + ")");
    //                return;
    //            }
    //            int count = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_ASSIGNSHIFT_DAYWISE", "count(*)", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]) + " AND DT=" + "'" + dt + "'"));
    //            if (count > 0)
    //            {
    //                shiftno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_ASSIGNSHIFT_DAYWISE", "SHIFTNO", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]) + " AND DT=" + "'" + dt + "'"));
    //            }

    //            //string shiftintime = (objCommon.LookUp("PAYROLL_LEAVE_SHIFTMAS", "INTIME", "SHIFTNO=" + shiftno +"AND DAYNO="+2));
    //            string shiftintime = (objCommon.LookUp("PAYROLL_LEAVE_SHIFTMAS", "INTIME", "SHIFTNO=" + shiftno + "AND DAYNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"])));
    //            DateTime shfttime = Convert.ToDateTime(shiftintime);
    //            ds.Tables[0].Rows[i]["SHIFT_INTIME"] = shfttime.Hour.ToString("00") + ":" + shfttime.Minute.ToString("00");

    //            string time = shfttime.AddMinutes(10).ToString();

    //            string splittime = ds.Tables[0].Rows[i]["INTIME"].ToString();
    //            string[] intimechk = ds.Tables[0].Rows[i]["INTIME"].ToString().Split(':');
    //            if (intimechk[0].ToString() == "00")
    //                splittime = "12:" + intimechk[1].ToString() + ":" + intimechk[2].ToString();
    //            //shiftintim=splittime

    //            //ds.Tables[0].Rows[i]["SHIFT_INTIME"] = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00");

    //            if (Convert.ToDateTime(splittime) < Convert.ToDateTime(time))
    //            {
    //                if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00" || ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
    //                {
    //                    DateTime intimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]);
    //                    DateTime outtimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["OUTTIME"]);
    //                    DataRow dr = dtBefore.NewRow();
    //                    dr["IDNO"] = ds.Tables[0].Rows[i]["IDNO"];
    //                    dr["USERNAME"] = ds.Tables[0].Rows[i]["USERNAME"];
    //                    dr["DATE"] = ds.Tables[0].Rows[i]["ENTDATE"];
    //                    if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00")
    //                        dr["INTIME"] = intimeTemp.Hour.ToString("00") + ":" + intimeTemp.Minute.ToString("00");
    //                    else
    //                        dr["INTIME"] = "--";
    //                    if (ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
    //                        dr["OUTTIME"] = outtimeTemp.Hour.ToString("00") + ":" + outtimeTemp.Minute.ToString("00");
    //                    else
    //                        dr["OUTTIME"] = "--";
    //                    if (ds.Tables[0].Rows[i]["HOURS"] != DBNull.Value)
    //                        dr["HOURS"] = ds.Tables[0].Rows[i]["HOURS"];
    //                    else
    //                        dr["HOURS"] = "--";
    //                    TimeSpan tempdate = (Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]) - Convert.ToDateTime(shiftintime));
    //                    if (tempdate.Minutes >= 0 && tempdate.Hours >= 0)
    //                        dr["LATEBY"] = tempdate.Hours.ToString("00") + ":" + tempdate.Minutes.ToString("00");
    //                    else
    //                        dr["LATEBY"] = "--";
    //                    dr["WTNO"] = ds.Tables[0].Rows[i]["WTNO"];
    //                    dr["STATUS"] = ds.Tables[0].Rows[i]["STATUS"];
    //                    dr["REASON_NO"] = ds.Tables[0].Rows[i]["REASON_NO"];//25-feb-2015
    //                    dtBefore.Rows.Add(dr);
    //                    dtBefore.AcceptChanges();
    //                }
    //            }
    //            else if (Convert.ToDateTime(Convert.ToDateTime(splittime)) > Convert.ToDateTime(time))
    //            {
    //                if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00" || ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
    //                {
    //                    DateTime intimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]);
    //                    DateTime outtimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["OUTTIME"]);
    //                    DataRow dr = dtAfter.NewRow();
    //                    dr["IDNO"] = ds.Tables[0].Rows[i]["IDNO"];
    //                    dr["USERNAME"] = ds.Tables[0].Rows[i]["USERNAME"];
    //                    dr["DATE"] = ds.Tables[0].Rows[i]["ENTDATE"];
    //                    if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00")
    //                        dr["INTIME"] = intimeTemp.Hour.ToString("00") + ":" + intimeTemp.Minute.ToString("00");
    //                    else
    //                        dr["INTIME"] = "--";
    //                    if (ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
    //                        dr["OUTTIME"] = outtimeTemp.Hour.ToString("00") + ":" + outtimeTemp.Minute.ToString("00");
    //                    else
    //                        dr["OUTTIME"] = "--";
    //                    if (ds.Tables[0].Rows[i]["HOURS"] != DBNull.Value)
    //                        dr["HOURS"] = ds.Tables[0].Rows[i]["HOURS"];
    //                    else
    //                        dr["HOURS"] = "--";
    //                    TimeSpan tempdate = (Convert.ToDateTime(splittime) - Convert.ToDateTime(shiftintime));
    //                    dr["LATEBY"] = tempdate.Hours.ToString("00") + ":" + tempdate.Minutes.ToString("00");
    //                    dr["WTNO"] = ds.Tables[0].Rows[i]["WTNO"];
    //                    dr["STATUS"] = ds.Tables[0].Rows[i]["STATUS"];
    //                    dr["REASON_NO"] = ds.Tables[0].Rows[i]["REASON_NO"];//25-feb-2015




    //                    if (ds.Tables[0].Rows[i]["SHIFT_INTIME"] != DBNull.Value)
    //                        dr["SHIFT_INTIME"] = ds.Tables[0].Rows[i]["SHIFT_INTIME"];
    //                    else
    //                        dr["SHIFT_INTIME"] = "--";
    //                    dtAfter.Rows.Add(dr);
    //                    dtAfter.AcceptChanges();
    //                }
    //            }
    //        }
    //        if (dtAfter.Rows.Count == 0 && dtBefore.Rows.Count == 0)
    //            MessageBox("No record Found");
    //        else
    //        {


    //            // bool flag = objApp.BulkInsertDataTable("TEMP_LATECOMERS", dtAfter);
    //            lvLateComers.DataSource = dtAfter;
    //            lvLateComers.DataBind();
    //            lvLateComers.Visible = true;
    //            foreach (ListViewDataItem lvitem in lvLateComers.Items)
    //            {
    //                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
    //                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
    //                ddlWorkType.SelectedValue = "1";

    //            }
    //            foreach (ListViewDataItem lvitem in lvEmpEarly.Items)
    //            {
    //                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
    //                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
    //                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
    //                ddlAllow.DataValueField = "0";
    //                ddlAllow.DataTextField = "Please Select";
    //                ddlAllow.DataBind();

    //                //==========

    //                //DropDownList ddlReason = (DropDownList)e.Item.FindControl("ddlReason");
    //                //objCommon.FillDropDownList(ddlReason, "PAYROLL_LEAVE_REASON", "REASONNO", "REASON", "", "REASONNO");


    //            }
    //            //gvBefore10_15.DataSource = dtBefore;
    //            //gvBefore10_15.DataBind();
    //            //gvAfter10_15.DataSource = dtAfter;
    //            //gvAfter10_15.DataBind();
    //            //pnlRpt1.Visible = true;
    //            //pnlRpt2.Visible = true;
    //            //lblMessage.Text = "<b>" + "<u>" + "Employee Login details on " + txtDate.Text + "(" + Convert.ToDateTime(txtDate.Text).DayOfWeek + ")" + "</u>" + "</b>";
    //            //lblMessage.Visible = true;

    //        }
    //        //txtDate.Text = "";
    //        //}
    //        //else
    //        //    MessageBox("Office closed on sunday. So, no record found.");
    //    }
    //    else
    //        MessageBox("You have entered date beyond todays date. Please enter valid date.");
    //}

    private void BindLateComersList()
    {
        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
        Fdate = Fdate.Substring(0, 10);
        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
        Tdate = Tdate.Substring(0, 10);
        DataSet ds = null;

        if (Convert.ToDateTime(txtToDt.Text) <= System.DateTime.Now)
        {

            ds = objApp.GetLoginInfoByDate(Convert.ToDateTime(txtFromDt.Text), Convert.ToDateTime(txtToDt.Text), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0,0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLateComers.DataSource = ds;
                lvLateComers.DataBind();

                lvLateComers.Visible = true;

                foreach (ListViewDataItem lvitem in lvLateComers.Items)
                {
                    DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                    //ddlAllow.SelectedValue = "A";
                    //if (rbAllow.Checked == true)
                    //{
                    //    ddlAllow.SelectedValue = "A";
                    //}
                    //else
                    //{
                    //    ddlAllow.SelectedValue = "N";
                    //}
                    //DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                    //objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                    //ddlWorkType.SelectedValue = "1";

                    //DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                    //ddlAllow.SelectedValue = "N";
                }
            }
            else
            {
                lvLateComers.DataSource = null;
                lvLateComers.DataBind();
                MessageBox("Record Not Found!");
            }

        }
        else
            MessageBox("You have entered date beyond todays date. Please enter valid date.");
    }



    protected void BindListViewEarly()
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);

            DataSet ds = objApp.GetEarlyGoingEmpList(Fdate, Tdate, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0,0);

            lvEmpEarly.DataSource = ds;
            lvEmpEarly.DataBind();
            lvEmpEarly.Visible = true;
            foreach (ListViewDataItem lvitem in lvEmpEarly.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                ddlWorkType.SelectedValue = "2";
            }


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected DataTable setGridViewDataset(DataTable dt, string tabName)
    {
        dt.TableName.Equals(tabName);
        dt.Columns.Add("IDNO");
        dt.Columns.Add("DATE");
        dt.Columns.Add("USERNAME");
        dt.Columns.Add("INTIME"); dt.Columns.Add("SHIFT_INTIME");
        dt.Columns.Add("OUTTIME");
        dt.Columns.Add("HOURS");
        dt.Columns.Add("LATEBY");
        dt.Columns.Add("WTNO");
        dt.Columns.Add("STATUS");
        dt.Columns.Add("REASON_NO");//25-12-2015

        return dt;
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (rblcondn.SelectedValue == "0")
            this.BindListViewModifyThumb();
        else if (rblcondn.SelectedValue == "1")
            BindListViewModifyLate();
        else if (rblcondn.SelectedValue == "2")
            BindListViewModifyEarly();
        else if (rblcondn.SelectedValue == "3")
            BindListViewModifyThumb_NR();
    }

    protected void BindListViewModifyLate()
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);

            DataSet ds = objApp.GetLateComingAllowedEmpList(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0);
            lvLateComers.DataSource = ds;
            lvLateComers.DataBind();
            lvLateComers.Visible = true;
            foreach (ListViewDataItem lvitem in lvLateComers.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                ddlAllow.DataValueField = "0";
                ddlAllow.DataTextField = "Please Select";
                ddlAllow.DataBind();
            }

            foreach (ListViewDataItem lvitem in lvLateComers.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                DropDownList ddlReason = lvitem.FindControl("ddlReason") as DropDownList;
                HiddenField hidlv = lvitem.FindControl("hidWorkType") as HiddenField;
                HiddenField hidStatus = lvitem.FindControl("hidStatus") as HiddenField;
                HiddenField hidReason = lvitem.FindControl("hidReason") as HiddenField;
                ddlWorkType.SelectedValue = hidlv.Value.ToString();
                ddlAllow.SelectedValue = hidStatus.Value.ToString();
                ddlReason.SelectedValue = hidReason.Value.ToString();

            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void BindListViewModifyThumb()
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);

            DataSet ds = objApp.GetThumbPrblemAllowedEmpList(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0);
            lvEmpList.DataSource = ds;
            lvEmpList.DataBind();
            lvEmpList.Visible = true;
            foreach (ListViewDataItem lvitem in lvEmpList.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                ddlAllow.DataValueField = "0";
                ddlAllow.DataTextField = "Please Select";
                ddlAllow.DataBind();

            }

            foreach (ListViewDataItem lvitem in lvEmpList.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                DropDownList ddlReason = lvitem.FindControl("ddlReason") as DropDownList;
                HiddenField hidlv = lvitem.FindControl("hidWorkType") as HiddenField;
                HiddenField hidStatus = lvitem.FindControl("hidStatus") as HiddenField;
                HiddenField hidReason = lvitem.FindControl("hidReason") as HiddenField;
                ddlWorkType.SelectedValue = hidlv.Value.ToString();
                ddlAllow.SelectedValue = hidStatus.Value.ToString();
                ddlReason.SelectedValue = hidReason.Value.ToString();
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void BindListViewModifyThumb_NR()
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);

            DataSet ds = objApp.GetThumbPrblemAllowedEmpList_NonRegistered(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0);
            lvNREmpList.DataSource = ds;
            lvNREmpList.DataBind();
            lvNREmpList.Visible = true;
            foreach (ListViewDataItem lvitem in lvNREmpList.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                ddlAllow.DataValueField = "0";
                ddlAllow.DataTextField = "Please Select";
                ddlAllow.DataBind();

            }

            foreach (ListViewDataItem lvitem in lvNREmpList.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                DropDownList ddlReason = lvitem.FindControl("ddlReason") as DropDownList;
                HiddenField hidlv = lvitem.FindControl("hidWorkType") as HiddenField;
                HiddenField hidStatus = lvitem.FindControl("hidStatus") as HiddenField;
                HiddenField hidReason = lvitem.FindControl("hidReason") as HiddenField;
                ddlWorkType.SelectedValue = hidlv.Value.ToString();
                ddlAllow.SelectedValue = hidStatus.Value.ToString();
                ddlReason.SelectedValue = hidReason.Value.ToString();
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void BindListViewModifyEarly()
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);

            DataSet ds = objApp.GetEarlyGoingAllowedEmpList(Fdate, Tdate, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue),0);

            lvEmpEarly.DataSource = ds;
            lvEmpEarly.DataBind();
            lvEmpEarly.Visible = true;
            foreach (ListViewDataItem lvitem in lvEmpEarly.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                objCommon.FillDropDownList(ddlWorkType, "PAYROLL_WORKTYPE", "WTNO", "WORKTYPE", "WTNO>0", "WORKTYPE");
                ddlAllow.DataValueField = "0";
                ddlAllow.DataTextField = "Please Select";
                ddlAllow.DataBind();

            }

            foreach (ListViewDataItem lvitem in lvEmpEarly.Items)
            {
                DropDownList ddlWorkType = lvitem.FindControl("ddlWorkType") as DropDownList;
                DropDownList ddlAllow = lvitem.FindControl("ddlAllow") as DropDownList;
                DropDownList ddlReason = lvitem.FindControl("ddlReason") as DropDownList;
                //ddlReason
                HiddenField hidlv = lvitem.FindControl("hidWorkType") as HiddenField;
                HiddenField hidStatus = lvitem.FindControl("hidStatus") as HiddenField;
                HiddenField hidReason = lvitem.FindControl("hidReason") as HiddenField;
                //hidReason
                ddlWorkType.SelectedValue = hidlv.Value.ToString();
                ddlAllow.SelectedValue = hidStatus.Value.ToString();
                ddlReason.SelectedValue = hidReason.Value.ToString();
            }


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (rblcondn.SelectedValue == "0")
        {
            this.BindListView();
            this.FillWorkType();
            lvLateComers.Visible = false;
            lvEmpList.Visible = true;
            lvEmpEarly.Visible = false;
            lvNREmpList.Visible = false;
        }
        else if (rblcondn.SelectedValue == "1")
        {
            this.BindLateComersList();
            lvEmpList.Visible = false;
            lvLateComers.Visible = true;
            lvEmpEarly.Visible = false;
            lvNREmpList.Visible = false;
        }
        else if (rblcondn.SelectedValue == "2")
        {
            this.BindListViewEarly();
            lvEmpList.Visible = false;
            lvLateComers.Visible = false;
            lvEmpEarly.Visible = true;
            lvNREmpList.Visible = false;
        }
        else if (rblcondn.SelectedValue == "3")
        {
            this.BindListViewNR();
            lvNREmpList.Visible = true;
            lvEmpList.Visible = false;
            lvLateComers.Visible = false;
            lvEmpEarly.Visible = false;
        }
        // System.Threading.Thread.Sleep(5000);
    }

    //Function to Generate report Early Going
    private void ShowReportEarlyGoing(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);
            int idno = 0;

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEPTNAME=" + ddlDept.SelectedItem.Text + " ";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    private void ShowReportEarlyGoingnew(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = 0;
            int stno = 0;
            int idno = 0;

            if (ddlDept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddlDept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }
            if (ddlStaffType.SelectedIndex > 0)
            {
                stno = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                stno = 0;
            }
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROMDATE=" + 
                Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO="
                + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STNO=" + stno + " ";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //Function to Generate report latecomers
    private void TransferLatecomersDataToTable()
    {
        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
        Fdate = Fdate.Substring(0, 10);
        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
        Tdate = Tdate.Substring(0, 10);
        DataSet ds = null;
        int shiftno;
        if (Convert.ToDateTime(txtToDt.Text) <= System.DateTime.Now)
        {
            //if (Convert.ToDateTime(txtDate.Text).DayOfWeek != DayOfWeek.Sunday)
            //{
            ds = objApp.GetLoginInfoByDate(Convert.ToDateTime(txtFromDt.Text), Convert.ToDateTime(txtToDt.Text), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue));
            ds.Tables[0].Columns.Add("HOURS");
            int rowCount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                int len = Convert.ToString(ds.Tables[0].Rows[i]["IDNO"]).Length;
                ds.Tables[0].Rows[i]["IDNO"] = Convert.ToString(ds.Tables[0].Rows[i]["IDNO"]);
                string inTime = Convert.ToString(ds.Tables[0].Rows[i]["INTIME"]);
                string outTime = Convert.ToString(ds.Tables[0].Rows[i]["OUTTIME"]);
                string date = Convert.ToString(ds.Tables[0].Rows[i]["ENTDATE"]);
                string dt = (String.Format("{0:u}", Convert.ToDateTime(date)));
                //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
                dt = dt.Substring(0, 10);
                if (inTime == string.Empty)
                    ds.Tables[0].Rows[i]["INTIME"] = "00:00:00";
                if (outTime == string.Empty)
                    ds.Tables[0].Rows[i]["OUTTIME"] = "00:00:00";
                if (inTime != string.Empty && outTime != string.Empty)
                {
                    TimeSpan ts = (Convert.ToDateTime(outTime) - Convert.ToDateTime(inTime));
                    ds.Tables[0].Rows[i]["HOURS"] = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00");
                }

                shiftno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SHIFTNO", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"])));
                int count = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_ASSIGNSHIFT_DAYWISE", "count(*)", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]) + " AND DT=" + "'" + dt + "'"));
                if (count > 0)
                {
                    shiftno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_ASSIGNSHIFT_DAYWISE", "SHIFTNO", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]) + " AND DT=" + "'" + dt + "'"));
                }

                string shiftintime = (objCommon.LookUp("PAYROLL_LEAVE_SHIFTMAS", "INTIME", "SHIFTNO=" + shiftno + "AND DAYNO=" + Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"])));
                DateTime shfttime = Convert.ToDateTime(shiftintime);
                string time = shfttime.AddMinutes(10).ToString();

                string splittime = ds.Tables[0].Rows[i]["INTIME"].ToString();
                string[] intimechk = ds.Tables[0].Rows[i]["INTIME"].ToString().Split(':');
                if (intimechk[0].ToString() == "00")
                    splittime = "12:" + intimechk[1].ToString() + ":" + intimechk[2].ToString();


                if (Convert.ToDateTime(splittime) < Convert.ToDateTime(time))
                {
                    if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00" || ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
                    {
                        DateTime intimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]);
                        DateTime outtimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["OUTTIME"]);
                        DataRow dr = dtBefore.NewRow();
                        dr["IDNO"] = ds.Tables[0].Rows[i]["IDNO"];
                        dr["USERNAME"] = ds.Tables[0].Rows[i]["USERNAME"];
                        dr["DATE"] = ds.Tables[0].Rows[i]["ENTDATE"];
                        if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00")
                            dr["INTIME"] = intimeTemp.Hour.ToString("00") + ":" + intimeTemp.Minute.ToString("00");
                        else
                            dr["INTIME"] = "--";
                        if (ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
                            dr["OUTTIME"] = outtimeTemp.Hour.ToString("00") + ":" + outtimeTemp.Minute.ToString("00");
                        else
                            dr["OUTTIME"] = "--";
                        if (ds.Tables[0].Rows[i]["HOURS"] != DBNull.Value)
                            dr["HOURS"] = ds.Tables[0].Rows[i]["HOURS"];
                        else
                            dr["HOURS"] = "--";
                        TimeSpan tempdate = (Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]) - Convert.ToDateTime(shiftintime));
                        if (tempdate.Minutes >= 0 && tempdate.Hours >= 0)
                            dr["LATEBY"] = tempdate.Hours.ToString("00") + ":" + tempdate.Minutes.ToString("00");
                        else
                            dr["LATEBY"] = "--";
                        dr["WTNO"] = ds.Tables[0].Rows[i]["WTNO"];
                        dr["STATUS"] = ds.Tables[0].Rows[i]["STATUS"];
                        dtBefore.Rows.Add(dr);
                        dtBefore.AcceptChanges();
                    }
                }
                else if (Convert.ToDateTime(Convert.ToDateTime(splittime)) > Convert.ToDateTime(time))
                {
                    if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00" || ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
                    {
                        DateTime intimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["INTIME"]);
                        DateTime outtimeTemp = Convert.ToDateTime(ds.Tables[0].Rows[i]["OUTTIME"]);
                        DataRow dr = dtAfter.NewRow();
                        dr["IDNO"] = ds.Tables[0].Rows[i]["IDNO"];
                        dr["USERNAME"] = ds.Tables[0].Rows[i]["USERNAME"];
                        dr["DATE"] = ds.Tables[0].Rows[i]["ENTDATE"];
                        if (ds.Tables[0].Rows[i]["INTIME"].ToString() != "00:00:00")
                            dr["INTIME"] = intimeTemp.Hour.ToString("00") + ":" + intimeTemp.Minute.ToString("00");
                        else
                            dr["INTIME"] = "--";
                        if (ds.Tables[0].Rows[i]["OUTTIME"].ToString() != "00:00:00")
                            dr["OUTTIME"] = outtimeTemp.Hour.ToString("00") + ":" + outtimeTemp.Minute.ToString("00");
                        else
                            dr["OUTTIME"] = "--";
                        if (ds.Tables[0].Rows[i]["HOURS"] != DBNull.Value)
                            dr["HOURS"] = ds.Tables[0].Rows[i]["HOURS"];
                        else
                            dr["HOURS"] = "--";
                        TimeSpan tempdate = (Convert.ToDateTime(splittime) - Convert.ToDateTime(shiftintime));
                        dr["LATEBY"] = tempdate.Hours.ToString("00") + ":" + tempdate.Minutes.ToString("00");
                        dr["WTNO"] = ds.Tables[0].Rows[i]["WTNO"];
                        dr["STATUS"] = ds.Tables[0].Rows[i]["STATUS"];
                        dtAfter.Rows.Add(dr);
                        dtAfter.AcceptChanges();
                    }
                }
            }
            if (dtAfter.Rows.Count == 0 && dtBefore.Rows.Count == 0)
                MessageBox("No record Found");
            else
            {




                //////////////////

                CustomStatus cs = (CustomStatus)objApp.DeleteDataTableForLatecomers();
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    bool flag = objApp.BulkInsertDataTable("TEMP_LATECOMERS", dtAfter);
                    if (flag == true)
                    {
                        //ShowReportLateCome("Latecomers", "ESTB_LateComersNew.rpt");
                        ShowReportLateComers("LateComers", "ESTB_LateComersNew.rpt");
                    }
                }
                ///////////

            }
        }
        else
            MessageBox("You have entered date beyond todays date. Please enter valid date.");
    }
    private void ShowReportLateCome(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);
            int designo = 0;

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROM_DATE=" + Fdate.ToString().Trim() + ",@P_TO_DATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEPTNAME=" + ddlDept.SelectedItem.Text + ",@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " ";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    private void ShowReportLateCome1(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);
            int designo = 0;

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    /// <summary>
    /// To Show The reports of Late Comers, Early Going, and Thumb Problem Employees
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    //late comers report 
    private void ShowReportLateComersStatus(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);
            int designo = 0;

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_IDNO=" + 0 + ",@P_DEPTNO=" + deptno + " ";
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    private void ShowReportLateComers(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);
            int designo = 0;

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO="+ Convert.ToInt32(ddlCollege.SelectedValue)+",@P_STNO="+ Convert.ToInt32(ddlStaffType.SelectedValue);
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPTNAME=" + ddlDept.SelectedItem.Text + " ";
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Fdate.ToString().Trim() + ",@P_TO_DATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEPTNAME=" + ddlDept.SelectedItem.Text +" " ;


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    //late comers report end


    private void ShowReportLatecomerreport(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = 0;
            int stno = 0;
            int designo = 0;

            if (ddlDept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddlDept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }
            if (ddlStaffType.SelectedIndex > 0)
            {
                stno = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                stno = 0;
            }

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROM_DATE=" + Fdate.ToString().Trim() + ",@P_TO_DATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STNO=" + stno + ",@P_IDNO=" + 0;
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPTNAME=" + ddlDept.SelectedItem.Text + " ";
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Fdate.ToString().Trim() + ",@P_TO_DATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEPTNAME=" + ddlDept.SelectedItem.Text +" " ;


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (rblcondn.SelectedValue == "0")
        {
            //Forgot to punch
            ShowReportLateCome("LateComingThumbProblemApproval", "ESTB_LateComing_ThumbProblem.rpt");
        }
        else if (rblcondn.SelectedValue == "1")
        {
            //latecomers
            //TransferLatecomersDataToTable();
            //ShowReportLateComers("LateComingThumbProblemApproval", "ESTB_LateComersReport.rpt"); 
            ShowReportLatecomerreport("LateComingThumbProblemApproval", "ESTB_LateComersReport.rpt");

        }
        else if (rblcondn.SelectedValue == "2")
        {
            //earlygoing
           // ShowReportEarlyGoing("LateComingThumbProblemApproval", "ESTB_EarlyGoing_Employee.rpt");
            ShowReportEarlyGoingnew("LateComingThumbProblemApproval", "ESTB_EarlyGoing_EmployeeReportnew.rpt");
        }
        else if (rblcondn.SelectedValue == "3")
        {
            //Not Registerd 
            ShowReportNRreport("NotRegisteredReport", "ESTB_Not_Registered.rpt");
        }
    }

    private void ShowReportNRreport(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROM_DATE=" + Fdate.ToString().Trim() + ",@P_TO_DATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " ";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    protected void btnStatusRpt_Click(object sender, EventArgs e)
    {
        if (rblcondn.SelectedValue == "0")
        {
            ShowReportThumbProblemStatus("LateComingThumbProblemApproval", "Estab_ThumbProblem_Status.rpt");
        }
        else if (rblcondn.SelectedValue == "1")
        {
            ShowReportLateComersStatus("LateComingThumbProblemApproval", "Estab_LateComersStatus.rpt");
        }
        else if (rblcondn.SelectedValue == "2")
        {
            ShowReportEarlyGoing("LateComingThumbProblemApproval", "Estab_EarlyGoing_Status.rpt");
        }
        else if (rblcondn.SelectedValue == "3")
        {
            //Not Register Status
            ShowReportNRreport("NotRegisteredStatusReport", "ESTB_Not_Registered_Status.rpt");
        }

    }
    //SHOW REPORT THUMB PROBLEM STATUS
    private void ShowReportThumbProblemStatus(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int deptno = Convert.ToInt32(ddlDept.SelectedValue);
            int idno = 0;

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_IDNO=" + Convert.ToString(idno) + ",@P_DEPTNO=" + deptno + "";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    //SHOW REPORT THUMB PROBLEM STATUS END

    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlReason = sender as DropDownList;
        reason_no = Convert.ToInt32(ddlReason.SelectedValue);

    }
    protected void lvEmpList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        for (int i = 0; i <= lvEmpList.Items.Count; i++)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlReason = (DropDownList)e.Item.FindControl("ddlReason");
                objCommon.FillDropDownList(ddlReason, "PAYROLL_LEAVE_REASON", "REASONNO", "REASON", "", "REASONNO");
            }
        }
    }
    protected void lvNREmpList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        for (int i = 0; i <= lvNREmpList.Items.Count; i++)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlReason = (DropDownList)e.Item.FindControl("ddlReason");
                objCommon.FillDropDownList(ddlReason, "PAYROLL_LEAVE_REASON", "REASONNO", "REASON", "", "REASONNO");
            }
        }
    }
    protected void lvEmpEarly_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        for (int i = 0; i <= lvEmpEarly.Items.Count; i++)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlReason = (DropDownList)e.Item.FindControl("ddlReason");
                objCommon.FillDropDownList(ddlReason, "PAYROLL_LEAVE_REASON", "REASONNO", "REASON", "", "REASONNO");
            }
        }

    }

    protected void lvLateComers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        for (int i = 0; i <= lvLateComers.Items.Count; i++)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlReason = (DropDownList)e.Item.FindControl("ddlReason");
                objCommon.FillDropDownList(ddlReason, "PAYROLL_LEAVE_REASON", "REASONNO", "REASON", "", "REASONNO");
            }
        }
    }



    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtToDt.Text) < Convert.ToDateTime(txtFromDt.Text))
            {
                MessageBox("To Date must be greater than From date");
                txtToDt.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {

        }

    }
}