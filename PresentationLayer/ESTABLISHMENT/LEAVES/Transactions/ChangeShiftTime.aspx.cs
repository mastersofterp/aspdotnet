//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : ChangeShiftTime.aspx                                                   
// CREATION DATE : 6 july 2012
// CREATED BY    :                                      
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.Globalization;


public partial class ESTABLISHMENT_LEAVES_Transactions_ChangeShiftTime : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objShift = new LeavesController();


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
                //pnlAdd.Visible = false;
                //pnlList.Visible = true;
                //BindListViewEmployees();
                CheckPageAuthorization();
                FillCollege();
                FillDepartment();                
                this.FillStaffType();                
                BindListViewDays();

                //trdept.Visible = false;
              //  BindListViewEmployees(Convert.ToInt32(rblEmp.SelectedValue) ,0);
            }


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
                Response.Redirect("~/notauthorized.aspx?page=ChangeShiftTime.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ChangeShiftTime.aspx");
        }
    }


    protected void BindListViewDays()
    {
        try
        {

            DataSet ds = objShift.RetrieveWeekDays();

            lvDays.DataSource = ds.Tables[0];
            lvDays.DataBind();
          //  lvDays.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void BindListViewEmployees(int collegeno, int deptno, int StNo, int tranno)    
    {
        try
        {
            DataSet ds = objShift.RetrieveAllEmployee(collegeno, deptno, StNo, tranno);
            lvEmpList.DataSource = ds.Tables[0];
            lvEmpList.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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

    private void Clear()
    {
        ddlDept.SelectedValue = ddlCollege.SelectedValue = ddlStafftype.SelectedValue = "0";
        txtFromDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        txtInTime.Text = string.Empty;
        txtOutTime.Text = string.Empty;
        lvEmpList.DataSource = null;
        //lvEmpList.DataBind();
        foreach (ListViewDataItem lvDay in lvDays.Items)
        {
            CheckBox chk = lvDay.FindControl("chkDay") as CheckBox;
            if (chk.Checked)
            {
                chk.Checked = false;
            }
        }
       // rblEmp.SelectedValue = "0";
        ViewState["selectedDates"] = null;
       pnlList.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       /* try
        {
            int checkcount = 0;
            int instCount = 0;
            string selectedIDs = string.Empty;
            if (Convert.ToDateTime(txtFromDt.Text) > Convert.ToDateTime(txtToDt.Text))
            {
                MessageBox("From Date Should be less than to date");
                return;
            }
            foreach (ListViewDataItem lvItem in lvEmpList.Items)
            {
                CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
                string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
                if (chk.Checked == true)
                {
                    checkcount += 1;
                    selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";

                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Employee");
                return;
            }


            selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
            string idno = selectedIDs.Replace('$', ',');

            string[] strTitleNo = idno.Trim().Split(',');

            int i = 0;
            for (i = 0; i <= strTitleNo.Length - 1; i++)
            {
                string id = strTitleNo[i];
                Shifts objShifts = new Shifts();
                objShifts.IDNO = Convert.ToInt32(id);
                objShifts.INTIME1  = txtInTime.Text;
                objShifts.OUTTIME1 = txtOutTime.Text;
                objShifts.FROMDATE = Convert.ToDateTime(txtFromDt.Text);
                objShifts.TODATE = Convert.ToDateTime(txtToDt.Text);

                DataTable dtDays = new DataTable("obserTbl");
                // dtDays.Columns.Add("DAYNAME", typeof(string));
                dtDays.Columns.Add("DAYNO", typeof(int));

                DataTable dtEmpRecord = new DataTable("obserTbl");
                dtEmpRecord.Columns.Add("IDNO", typeof(int));

                DataRow dr = null;
                foreach (ListViewDataItem lvDay in lvDays.Items)
                {

                    CheckBox chk = lvDay.FindControl("chkDay") as CheckBox;

                    dr = dtDays.NewRow();
                    if (chk.Checked == true)
                    {
                        dr["DAYNO"] = chk.ToolTip;

                        dtDays.Rows.Add(dr);
                        dtDays.AcceptChanges();
                    }
                }

                foreach (ListViewDataItem lvItem in lvEmpList.Items)
                {
                    CheckBox chkID = lvItem.FindControl("chkID") as CheckBox;

                    if (chkID.Checked == true)
                    {
                        dr = dtEmpRecord.NewRow();

                        dr["IDNO"] = chkID.ToolTip;

                        dtEmpRecord.Rows.Add(dr);
                        dtEmpRecord.AcceptChanges();

                    }
                }
                
                //objShifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

                //DateTime changedate = Convert.ToDateTime(txtFromDt.Text);
                //while (changedate <= Convert.ToDateTime(txtToDt.Text))
                //{
                    //objShifts.DATE = changedate;
                    //string intime = txtInTime.Text;
                    //string dt = changedate.ToString("yyyy/MM/dd");
                    //string date = dt + " " + intime;
                    //DateTime dtRet = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(5, 2)), Convert.ToInt32(date.Substring(8, 2)), Convert.ToInt32(date.Substring(11, 2)), Convert.ToInt32(date.Substring(14, 2)), 0);
                    //objShifts.DATE = dtRet;
                    CustomStatus cs = (CustomStatus)objShift.AddChangeShiftTime(objShifts, dtDays,dtEmpRecord);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        instCount = 1;
                        ViewState["action"] = "add";
                        //changedate = changedate.AddDays(1);
                        
                    }
                //}

                if (instCount == 1)
                {
                    MessageBox("Record saved successfully");
                    Clear();
                }
            }
           
               
            


        } */
        // Added By SHrikant Bharne. 02-12-2019
        try
        {
            if (Convert.ToDateTime(txtFromDt.Text) > Convert.ToDateTime(txtToDt.Text))
            {
                MessageBox("From Date Should be less than to date");
                return;
            }
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");
            int checkcount = 0;

            string selectedIDs = string.Empty;
            foreach (ListViewDataItem lvItem in lvEmpList.Items)
            {
                CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
                string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
                if (chk.Checked == true)
                {
                    checkcount = 1;
                    DataRow dr = dtEmpRecord.NewRow();
                    dr["IDNO"] = chk.ToolTip;
                    dtEmpRecord.Rows.Add(dr);
                    dtEmpRecord.AcceptChanges();

                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Employee");
                return;
            }

            Shifts objShifts = new Shifts();
            objShifts.INTIME1 = txtInTime.Text;
            objShifts.OUTTIME1 = txtOutTime.Text;
            objShifts.FROMDATE = Convert.ToDateTime(txtFromDt.Text);
            objShifts.TODATE = Convert.ToDateTime(txtToDt.Text);
            objShifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            CustomStatus cs = (CustomStatus)objShift.AddChangeShiftTime(objShifts, dtEmpRecord);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record saved successfully");
                Clear();
                pnlList.Visible = false;
            }
        }
            //



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex >= 0 && ddlDept.SelectedIndex > 0)
        {
            BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue),3);
        }
        else
        {
            pnlList.Visible = true;
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0)
        {
            BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 3);
        }
        else
        {
            pnlList.Visible = true;
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
        }
    }
    private void FillDepartment()
    {
        try
        {
            //if (ddlCollege.SelectedIndex >=0)
            //{
            //    objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Added on 15-Dec-2016 by Saket singh
        if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0)
        {
            BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue),3);
        }
        else
        {
            pnlList.Visible = true;
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
        }

    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDt.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDt.Text).ToString("yyyy-MM-dd");
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ChangeShiftTime..ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Change_Shift_Time_Report", "Change_Shift_Time_Report.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Payroll_LIC_Report.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {      

        DateTime Test;
        if (DateTime.TryParseExact(txtToDt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtToDt.Text != string.Empty && txtToDt.Text != "__/__/____" && txtFromDt.Text != string.Empty && txtFromDt.Text != "__/__/____")
            {
                DateTime from = Convert.ToDateTime(txtFromDt.Text.ToString());
                DateTime todate = Convert.ToDateTime(txtToDt.Text.ToString());

                if (todate < from)
                {
                    MessageBox("To Date is Must be Greater Then or Equal to From Date");
                    //txtTodt.Text = string.Empty;
                    txtToDt.Text = string.Empty;
                    return;
                }
            }
        }
    }
}
