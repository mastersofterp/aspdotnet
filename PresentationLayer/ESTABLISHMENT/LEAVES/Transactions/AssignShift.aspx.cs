//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : AssignShift.aspx                                                   
// CREATION DATE : 5 july 2012
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



public partial class ESTABLISHMENT_LEAVES_Transactions_AssignShift : System.Web.UI.Page
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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;
                //pnlList.Visible = true;
                //BindListViewEmployees();
                FillCollege();
                FillDepartment();
                //this.FillShift();
                this.FillStaffType();

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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void BindListViewEmployees(int collegeno, int deptno, int StNo, int tranno)
    {
        try
        {

            if (ddlCollege.SelectedIndex >= 0 && ddlStafftype.SelectedIndex >= 0)
            {
                DataSet ds = objShift.RetrieveAllEmployee(collegeno, deptno, StNo, tranno);
                lvEmpList.DataSource = ds.Tables[0];
                lvEmpList.DataBind();
                lvEmpList.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillStaffType()
    {
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
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //if (Session["usertype"].ToString() != "1")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}

    }
    private void FillShift()
    {
        try
        {
            //objCommon.FillDropDownList(ddlShift, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlShift, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0 and college_no=" + Convert.ToInt32(ddlCollege.SelectedValue), "SHIFTNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
        ddlDept.SelectedIndex = ddlCollege.SelectedIndex = ddlStafftype.SelectedIndex = 0;
        txtFromDt.Text = txtToDt.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        // rblEmp.SelectedValue = "0";
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();
        lvEmpList.Visible = false;

        ViewState["selectedDates"] = null;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int checkcount = 0;
            int instCount = 0;
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");
            string selectedIDs = string.Empty;
            //foreach (ListViewDataItem lvItem in lvEmpList.Items)
            //{
            if (Convert.ToDateTime(txtFromDt.Text) > Convert.ToDateTime(txtToDt.Text))
            {
                MessageBox("From Date Should be less than to date");
                return;
            }
                foreach (RepeaterItem ri in lvEmpList.Items)
                {
                    CheckBox chk = ri.FindControl("chkID") as CheckBox;
                    string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
                    if (chk.Checked == true)
                    {
                        checkcount += 1;
                        selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
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
            int cs = 0;
            objShifts.SHIFTNO = Convert.ToInt32(ddlShift.SelectedValue);
            objShifts.SHIFTNAME = ddlShift.SelectedItem.Text;
            objShifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objShifts.FROMDATE = Convert.ToDateTime(txtFromDt.Text);
            objShifts.TODATE = Convert.ToDateTime(txtToDt.Text);

            cs = objShift.AddAssignShift(objShifts, dtEmpRecord);
            if (cs == 1)
            {
                //objCommon.DisplayMessage("Record Saved Successfully", this.Page);
                MessageBox("Record Saved Successfully");

            }
            Clear();
            pnlList.Visible = false;
            //selectedIDs = selectedIDs.Substring(0, selectedIDs.Length - 1);
            //string idno = selectedIDs.Replace('$', ',');

            //string[] strTitleNo = idno.Trim().Split(',');

            //int i = 0;
            //for (i = 0; i <= strTitleNo.Length-1; i++)
            //{
            //    string id = strTitleNo[i];
            //Shifts objShifts = new Shifts();
            //objShifts.IDNO = Convert.ToInt32(id);
            //objShifts.SHIFTNO = Convert.ToInt32(ddlShift.SelectedValue);
            //objShifts.SHIFTNAME = ddlShift.SelectedItem.Text;
            //objShifts.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

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
            //if (instCount == 1)
            //{
            //    MessageBox("Record saved successfully");
            //}
        }
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
            ShowReport("Holidays_Entry", "");
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();
        FillShift();
    }

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Added by Saket Singh on 13-Dec-2016
        if (ddlCollege.SelectedIndex >= 0 && ddlStafftype.SelectedIndex >= 0)
        {

            BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 2);
        }
        else
        {
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
        }

    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Added by Saket Singh on 13-Dec-2016
        if (ddlCollege.SelectedIndex >= 0 && ddlStafftype.SelectedIndex >= 0)
        {
            //BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
            BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 2);
            pnlList.Visible = true;
        }
        else
        {
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Added by Saket Singh on 13-Dec-2016
        if (ddlCollege.SelectedIndex >= 0 && ddlStafftype.SelectedIndex >= 0)
        {
            //BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
            BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue), 2);
        }
        else
        {
            lvEmpList.DataSource = null;
            lvEmpList.DataBind();
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
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtToDt.Text = string.Empty;
                    return;
                }
            }
        }


    }
}
