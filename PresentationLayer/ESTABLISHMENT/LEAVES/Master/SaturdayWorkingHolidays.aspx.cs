using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Globalization;

public partial class ESTABLISHMENT_LEAVES_Master_SaturdayWorkingHolidays : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    Leaves objLeaves = new Leaves();

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
                pnlAdd.Visible = false;
                //pnlList.Visible = true;
                divworking.Visible = true;
                //pnlButton.Visible = false;
                //CheckPageAuthorization();
                btnSave.Visible = btnCancel.Visible = btnBack.Visible = false;
                FillStaffType();

                Boolean IsEmployeewiseSaturday = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(IsEmployeeWiseSatWorking,0) as IsEmployeeWiseSatWorking", ""));
                ViewState["IsEmployeewiseSaturday"] = IsEmployeewiseSaturday;
                if (IsEmployeewiseSaturday == true)
                {
                    divworking.Visible = false;
                    divworkingEmployee.Visible = true;
                    BindEmployeeListView();
                }
                else
                {
                    divworking.Visible = true;
                    divworkingEmployee.Visible = false;
                    BindListView();
                }
                // lvDept.Visible = false;
                //pnlAdd.Visible = true; pnlButton.Visible = true;
                btnReport.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=leaves.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=leaves.aspx");
        }
    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_STAFFTYPE S ON (S.STNO = E.STNO)", " DISTINCT S.STNO", "S.STAFFTYPE", "S.STNO<>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void txtdt_TextChanged(object sender, EventArgs e)
    {
        DateTime Test;
        if (DateTime.TryParseExact(txtdt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtdt.Text != string.Empty && txtdt.Text != "__/__/____")
            {
                string dayname = Convert.ToDateTime(txtdt.Text).DayOfWeek.ToString();
                //string count = objCommon.LookUp("Payroll_Holidays_Vacation", "Count(DATE) AS DATE", "CAST([DATE] AS DATE) = " + Convert.ToDateTime(txtdt.Text.Trim()).ToString("yyyy-MM-dd") + "AND STNO = " + ddlStafftype.SelectedValue);
                string count = objCommon.LookUp("Payroll_Holidays_Vacation", "DATE", "DATE = '" + Convert.ToDateTime(txtdt.Text.Trim()).ToString("yyyy-MM-dd") + "' AND STNO = " + ddlStafftype.SelectedValue);
                if (count == "" && dayname != "Saturday" && dayname != "Sunday")
                {
                    MessageBox("Selected day is not holiday.");
                    txtdt.Text = string.Empty;
                }
            }
        }
        else
        {
            txtdt.Text = string.Empty;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (chkEmp.Checked == true)
            if(Convert.ToBoolean(ViewState["IsEmployeewiseSaturday"]) == true)
            {
                //bool result = CheckPurpose();
                int count = 0; int count_record = 0; int single_count = 0;
                bool isworkingday;
                int swdid = 0;
                objLeaves.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
                objLeaves.DATE = Convert.ToDateTime(txtdt.Text);
                string date = (String.Format("{0:u}", Convert.ToDateTime(txtdt.Text).ToString("yyyy-MM-dd")));
                string remark = txtRemark.Text.Trim();
                objLeaves.CREATEDBY = Convert.ToInt32(Session["userno"]);
                objLeaves.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
                if (chkIsworking.Checked == true)
                {
                    isworkingday = true;
                }
                else
                {
                    isworkingday = false;
                }
                objLeaves.ISEMPWISESAT = true;
                DataTable dtEmpRecord = new DataTable();
                dtEmpRecord.Columns.Add(new DataColumn("IDNO", typeof(int)));

                int i = 0;
                foreach (ListViewDataItem items in lvEmployee.Items)
                {
                    CheckBox chkEmpSelect = items.FindControl("chkEmpSelect") as CheckBox;
                    HiddenField hdfidno = items.FindControl("hdfidno") as HiddenField;
                    if (chkEmpSelect.Checked == true)
                    {
                        count = count + 1;
                        single_count = single_count + 1;
                        objLeaves.EMPNO = Convert.ToInt32(chkEmpSelect.ToolTip);
                        DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_SATURDAY_AS_WORKING", "WORKINGDATE", "IDNO", "IDNO=" + objLeaves.EMPNO + " AND WORKINGDATE='" + date + "' ", "");
                        if (ds.Tables[0].Rows.Count <= 0)
                        {
                            count_record = count_record + 1;
                            DataRow dr = dtEmpRecord.NewRow();
                            dr["IDNO"] = Convert.ToInt32(hdfidno.Value);
                            dtEmpRecord.Rows.Add(dr);
                            dtEmpRecord.AcceptChanges();
                            //i = 1;
                        }
                    }
                }
                if (count == 0)
                {
                    objCommon.DisplayMessage("Please Select Atleast One Employee!", this.Page);
                    return;
                }
                //else if (single_count ==1)
                //{

                //    MessageBox("Sorry ! Record Already exists");
                //    return;
                //}
                //else if (count > 0 && count_record == 0)
                //{
                //    MessageBox("Sorry ! Record Already exists");
                //    return;
                //}

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                       // CustomStatus cs = (CustomStatus)objLeave.AddUpdateWorkingHolidayDetailsByID(objLeaves, remark, isworkingday, swdid, dtEmpRecord);
                        CustomStatus cs = (CustomStatus)objLeave.AddUpdateWorkingEmployeewiseHolidayDetailsByID(objLeaves, remark, isworkingday, swdid, dtEmpRecord);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            pnlAdd.Visible = false;
                            //pnlButton.Visible = false;
                            //this.BindListView();
                            BindEmployeeListView();
                            Clear();
                            //pnlList.Visible = true;
                            divworking.Visible = false;
                            divworkingEmployee.Visible = true;
                            btnSave.Visible = btnBack.Visible = btnCancel.Visible = false;
                            btnAdd.Visible = true;
                            btnReport.Visible = false;
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            MessageBox("Record Already Exist");
                            BindEmployeeListView();
                            Clear();
                        }
                        else
                        {
                            MessageBox("Record Saved Failed");
                            BindEmployeeListView();
                            Clear();
                        }
                    }
                    else
                    {
                        if (ViewState["SWDID"] != null)
                        {

                            swdid = Convert.ToInt32(ViewState["SWDID"].ToString());
                            //CustomStatus cs = (CustomStatus)objLeave.AddUpdateWorkingHolidayDetailsByID(objLeaves, remark, isworkingday, swdid, dtEmpRecord);
                            CustomStatus cs = (CustomStatus)objLeave.AddUpdateWorkingEmployeewiseHolidayDetailsByID(objLeaves, remark, isworkingday, swdid, dtEmpRecord);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Updated Successfully");
                                pnlAdd.Visible = false;
                                //pnlButton.Visible = false;
                                //this.BindListView();
                                ViewState["action"] = "add";
                                ViewState["SWDID"] = null;
                                BindEmployeeListView();
                                Clear();
                                //pnlList.Visible = true;
                                divworking.Visible = false;
                                divworkingEmployee.Visible = true;
                                btnSave.Visible = btnBack.Visible = btnCancel.Visible = false;
                                btnAdd.Visible = true;
                            }
                            else if (cs.Equals(CustomStatus.RecordExist))
                            {
                                MessageBox("Record Already Exist");
                                BindEmployeeListView();
                                Clear();
                            }
                            else
                            {
                                MessageBox("Record Saved Failed");
                                BindEmployeeListView();
                                Clear();
                            }
                        }
                    }
                }
            }
            else
            {
                bool result = CheckPurpose();

                bool isworkingday;
                int swdid = 0;
                objLeaves.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
                objLeaves.DATE = Convert.ToDateTime(txtdt.Text);
                string remark = txtRemark.Text.Trim();
                objLeaves.CREATEDBY = Convert.ToInt32(Session["userno"]);
                objLeaves.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
                if (chkIsworking.Checked == true)
                {
                    isworkingday = true;
                }
                else
                {
                    isworkingday = false;
                }
                objLeaves.ISEMPWISESAT = false;
                DataTable dtAppRecord = new DataTable();
                dtAppRecord.Columns.Add(new DataColumn("subdeptno", typeof(int)));

                int i = 0;
                foreach (ListViewDataItem items in lvDept.Items)
                {
                    CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
                    HiddenField hdfsubdeptno = items.FindControl("hdfsubdeptno") as HiddenField;
                    if (chkSelect.Checked)
                    {
                        DataRow dr = dtAppRecord.NewRow();
                        dr["SUBDEPTNO"] = Convert.ToInt32(hdfsubdeptno.Value);
                        dtAppRecord.Rows.Add(dr);
                        dtAppRecord.AcceptChanges();
                        i = 1;
                    }
                }
                if (i == 0)
                {
                    objCommon.DisplayMessage("Please Select Atleast One Department!", this.Page);
                    return;
                }

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        CustomStatus cs = (CustomStatus)objLeave.AddUpdateWorkingHolidayDetails(objLeaves, remark, isworkingday, swdid, dtAppRecord);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            pnlAdd.Visible = false;
                            //pnlButton.Visible = false;
                            this.BindListView();
                            Clear();
                            //pnlList.Visible = true;
                            divworking.Visible = true;
                            btnSave.Visible = btnBack.Visible = btnCancel.Visible = false;
                            btnAdd.Visible = true;

                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            MessageBox("Record Already Exist");
                            BindListView();
                            Clear();
                        }
                        else
                        {
                            MessageBox("Record Saved Failed");
                            BindListView();
                            Clear();
                        }
                    }
                    else
                    {
                        if (ViewState["SWDID"] != null)
                        {

                            swdid = Convert.ToInt32(ViewState["SWDID"].ToString());
                            CustomStatus cs = (CustomStatus)objLeave.AddUpdateWorkingHolidayDetails(objLeaves, remark, isworkingday, swdid, dtAppRecord);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Updated Successfully");
                                pnlAdd.Visible = false;
                                //pnlButton.Visible = false;
                                this.BindListView();
                                ViewState["action"] = "add";
                                ViewState["SWDID"] = null;
                                Clear();
                                //pnlList.Visible = true;
                                divworking.Visible = true;
                                btnSave.Visible = btnBack.Visible = btnCancel.Visible = false;
                                btnAdd.Visible = true;
                            }
                            else if (cs.Equals(CustomStatus.RecordExist))
                            {
                                MessageBox("Record Already Exist");
                                BindListView();
                                Clear();
                            }
                            else
                            {
                                MessageBox("Record Saved Failed");
                                BindListView();
                                Clear();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnSave_click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        //pnlList.Visible = true;
        //pnlButton.Visible = false;
        if (Convert.ToBoolean(ViewState["IsEmployeewiseSaturday"]) == true)
        {
            divworkingEmployee.Visible = true;
            divworking.Visible = false;

        }
        else
        {
            divworking.Visible = true;
            divworkingEmployee.Visible = false;
        }
        btnAdd.Visible = true;
        btnSave.Visible = btnBack.Visible = btnCancel.Visible = false;
        btnReport.Visible = false;
    }

    private void Clear()
    {
        ddlStafftype.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        txtdt.Text = string.Empty;

        pnlDeptList.Visible = false;
        lvDept.DataSource = null;
        lvDept.DataBind();

        ViewState["action"] = "add";
        ViewState["SWDID"] = null;
        pnlEmpList.Visible = false;
        lvEmployee.DataSource = null;
        lvEmployee.DataBind();
        //chkEmp.Checked = false;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SWDID = int.Parse(btnEdit.CommandArgument);

            ViewState["SWDID"] = SWDID;
            ShowDetails(SWDID);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true; //pnlButton.Visible = true;
            //pnlList.Visible = false;
            divworking.Visible = false;
            divworkingEmployee.Visible = false;
            btnAdd.Visible = false;
            btnSave.Visible = btnBack.Visible = btnCancel.Visible = true;
            chkEmp.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnEdit_click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 SWDID)
    {
        DataSet ds = null;
        DataSet ds1 = null;
        try
        {

            if (Convert.ToBoolean(ViewState["IsEmployeewiseSaturday"]) == false)
            {
                ds = objLeave.GetSaturdayWorkingDetailsById(SWDID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["SWDID"] = SWDID;
                    ddlStafftype.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                    txtdt.Text = ds.Tables[0].Rows[0]["WORKINGDATE"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    //chkIsworking.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAllowBeforeApplication"]);
                    //Boolean IsEmployeeWise = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISEMPWISESAT"].ToString());
                    //if (IsEmployeeWise == true)
                    //{
                    //    chkEmp.Checked = true;
                    //}
                    //else
                    //{
                    //    chkEmp.Checked = false;
                    //}
                    BindDepartment();
                    DataSet dsdept = objCommon.FillDropDown("PAYROLL_LEAVE_SATURDAY_AS_WORKING_DEPT", "*", "", "SWDID=" + SWDID, "");

                    foreach (DataRow dr in dsdept.Tables[0].Rows)
                    {
                        foreach (ListViewDataItem lvitem in lvDept.Items)
                        {
                            int deptno = Convert.ToInt32(dr["DEPTNO"].ToString());
                            CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;

                            if (Convert.ToInt32(chkSelect.ToolTip) == deptno)
                            {
                                chkSelect.Checked = true;
                            }
                        }
                    }
                    pnlDeptList.Visible = true;
                    pnlEmpList.Visible = false;

                }               //}
                //else
                //{
                //    chkEmp.Checked = true;                        
                //    BindEmployee(Convert.ToInt32(ddlStafftype.SelectedValue));
                //    DataSet dsidno = objCommon.FillDropDown("PAYROLL_LEAVE_SATURDAY_AS_WORKING_DEPT", "*", "", "SWDID=" + SWDID, "");

                //    foreach (DataRow dr in dsidno.Tables[0].Rows)
                //    {
                //        foreach (ListViewDataItem lvitem in lvEmployee.Items)
                //        {
                //            int idno = Convert.ToInt32(dr["IDNO"].ToString());
                //            CheckBox chkEmpSelect = lvitem.FindControl("chkEmpSelect") as CheckBox;

                //            if (Convert.ToInt32(chkEmpSelect.ToolTip) == idno)
                //            {
                //                chkEmpSelect.Checked = true;
                //            }
                //        }
                //    }
                //    pnlDeptList.Visible = false;
                //    pnlEmpList.Visible = true;
                //}
            }

            else
            {
                ds1 = objLeave.GetEmployeeSaturdayWorkingDetailsById(SWDID);

                ViewState["SWDID"] = SWDID;
                ddlStafftype.SelectedValue = ds1.Tables[0].Rows[0]["STNO"].ToString();
                txtdt.Text = ds1.Tables[0].Rows[0]["WORKINGDATE"].ToString();
                txtRemark.Text = ds1.Tables[0].Rows[0]["REMARK"].ToString();

                BindEmployee(Convert.ToInt32(ddlStafftype.SelectedValue));
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    foreach (ListViewDataItem lvitem in lvEmployee.Items)
                    {
                        int IDNO = Convert.ToInt32(dr["IDNO"].ToString());
                        CheckBox chkSelect = lvitem.FindControl("chkEmpSelect") as CheckBox;

                        if (Convert.ToInt32(chkSelect.ToolTip) == IDNO)
                        {
                            chkSelect.Checked = true;
                        }
                    }
                }
                pnlDeptList.Visible = false;
                pnlEmpList.Visible = true;



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowDetails->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindListView();
    //}

    private void BindListView()
    {
        try
        {
            DataSet ds = objLeave.GetSaturdayWorkingDetails(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvsatwrk.DataSource = ds.Tables[0];
                lvsatwrk.DataBind();
                //dpPager.Visible = true;
            }
            else
            {
                lvsatwrk.DataSource = null;
                lvsatwrk.DataBind();
                //dpPager.Visible = false;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CPDA_Master_CPDAType_Limit.BindCPDATYPE -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(ViewState["IsEmployeewiseSaturday"]) == true)
        {
            divworking.Visible = false;
            divworkingEmployee.Visible = false;
            btnReport.Visible = true;
        }
        else
        {
            divworking.Visible = false;
            divworkingEmployee.Visible = false;
            btnReport.Visible = false;
        }
        Clear();
        pnlAdd.Visible = true;
        //pnlList.Visible = false;
        //pnlButton.Visible = true;

        //Calendar1.SelectedDate="
        btnAdd.Visible = false;
        btnSave.Visible = btnBack.Visible = btnCancel.Visible = true;


        //txtFromDt.Text = System.DateTime.Today.ToString(); //    System.DateTime.Now.ToString();
        ViewState["action"] = "add";
        //btnReport.Visible = false;
        //chkEmp.Enabled = true;
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
    }



    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        int STNONEW = Convert.ToInt32(ddlStafftype.SelectedValue);
        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtdt.Text).ToString("yyyy-MM-dd")));
        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
        Fdate = Fdate.Substring(0, 10);

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_LEAVE_SATURDAY_AS_WORKING", "*", "", "WORKINGDATE='" + Fdate + "' AND STNO =" + STNONEW + " AND ISNULL(ISEMPWISESAT,0) =" + 0 + "", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (chkEmp.Checked == true)
        //{
        txtdt.Text = string.Empty;
        if (Convert.ToBoolean(ViewState["IsEmployeewiseSaturday"]) == true)
        {
            BindEmployee(Convert.ToInt32(ddlStafftype.SelectedValue));
            pnlDeptList.Visible = false;
            pnlEmpList.Visible = true;
            lvDept.Visible = false;
        }
        else
        {
            BindDepartment();
            pnlDeptList.Visible = true;
            pnlEmpList.Visible = false;
            lvDept.Visible = true;
        }
    }

    public void BindDepartment()
    {
        DataSet ds = objCommon.FillDropDown("payroll_empmas p inner join payroll_subdept d on d.subdeptno=p.subdeptno", "distinct (p.subdeptno)", "d.subdept", "stno=" + Convert.ToInt32(ddlStafftype.SelectedValue), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDept.DataSource = ds.Tables[0];
            lvDept.DataBind();
            pnlDeptList.Visible = true;
        }
        else
        {
            pnlDeptList.Visible = false;
        }
    }

    protected void chkEmp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEmp.Checked == true)
        {
            pnlDeptList.Visible = false;
            pnlEmpList.Visible = true;
            btnReport.Visible = true;
            lvEmployee.Visible = false;
            ddlStafftype.SelectedIndex = 0;
            lvDept.Visible = false;
        }
        else
        {
            pnlDeptList.Visible = true;
            pnlEmpList.Visible = false;
            btnReport.Visible = false;
            lvEmployee.Visible = false;
            ddlStafftype.SelectedIndex = 0;
            lvDept.Visible = false;
        }
    }

    public void BindEmployee(int stno)
    {
        try
        {
            DataSet ds = objLeave.GetEmployeeListForSaturday(Convert.ToInt32(ddlStafftype.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmployee.DataSource = ds.Tables[0];
                lvEmployee.DataBind();
                lvEmployee.Visible = true;
            }
            else
            {
                lvEmployee.DataSource = null;
                lvEmployee.DataBind();
                lvEmployee.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CPDA_Master_CPDAType_Limit.BindCPDATYPE -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlStafftype.SelectedIndex > 0)
        {

        }
        else
        {
            MessageBox("Please Select Staff Type.");
            return;
        }
        if (txtdt.Text == string.Empty)
        {
            MessageBox("Please Select valid Working Date.");
            return;
        }
        else
        {

        }
        ShowReport("EmployeeWiseSaturdayReport", "EmployeeWiseSaturday.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int stno = Convert.ToInt32(ddlStafftype.SelectedValue);
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;

            url += "&param=@P_WORKINGDATE=" + Fdate.ToString().Trim() + ",@P_STNO=" + stno + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    private void BindEmployeeListView()
    {
        try
        {
            DataSet ds = objLeave.GetEmployeeSaturdayWorkingDetails(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                LstEmployeewise.DataSource = ds.Tables[0];
                LstEmployeewise.DataBind();
                //dpPager.Visible = true;
            }
            else
            {
                LstEmployeewise.DataSource = null;
                LstEmployeewise.DataBind();
                //dpPager.Visible = false;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CPDA_Master_CPDAType_Limit.BindCPDATYPE -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 SWID = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objLeave.DeleteEmployeeSaturday(SWID);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully.");
                //return;
            }
            ViewState["action"] = null;

            BindEmployeeListView();
            
        }
        catch (Exception ex)
        {

        }
    }
}