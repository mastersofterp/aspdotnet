using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_EmployeeLocationMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    Leaves objLM = new Leaves();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                FillCollege();
                FillDepartment();
                FillStaffType();

                ddlEmp.SelectedIndex = 0;
                lvEmployees.Visible = false;

                CheckPageAuthorization();
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
                Response.Redirect("~/notauthorized.aspx?page=Leave_Allotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Leave_Allotment.aspx");
        }
    }

    protected void radView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radView.SelectedValue == "0")
        {
            divLoc.Visible = true;
            tremp.Visible = false;
            FillLocation();
            pnlEmpUnMap.Visible = false;
            pnlLocUnMap.Visible = false;
            pnlEmpList.Visible = false;
            pnlLoc.Visible = false;
        }
        else
        {
            divLoc.Visible = false;
            tremp.Visible = true;
            pnlEmpUnMap.Visible = false;
            pnlLocUnMap.Visible = false;
            pnlEmpList.Visible = false;
            pnlLoc.Visible = false;
            FillEmployee();
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
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO <> 0", "SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
        pnlEmpUnMap.Visible = false;
        pnlLocUnMap.Visible = false;
        pnlEmpList.Visible = false;
        pnlLoc.Visible = false;
    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
        pnlEmpUnMap.Visible = false;
        pnlLocUnMap.Visible = false;
        pnlEmpList.Visible = false;
        pnlLoc.Visible = false;
    }

    private void FillEmployee()
    {
        try
        {
            if (ddldept.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0 && ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) INNER JOIN PAYROLL_STAFFTYPE ST ON (ST.STNO = E.STNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "(ISNULL(FNAME,'')+ ' ' +ISNULL(MNAME,'')+ ' ' +ISNULL(LNAME,'')+ ' - '+ISNULL(PFILENO,''))", "E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue) + " AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND P.PSTATUS='Y'" + "", "IDNO");
            }
            else if (ddldept.SelectedIndex > 0 && ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "(ISNULL(FNAME,'')+ ' ' +ISNULL(MNAME,'')+ ' ' +ISNULL(LNAME,'')+ ' - '+ISNULL(PFILENO,''))", "E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND P.PSTATUS='Y'" + "", "IDNO");
            }
            else if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "(ISNULL(FNAME,'')+ ' ' +ISNULL(MNAME,'')+ ' ' +ISNULL(LNAME,'')+ ' - '+ISNULL(PFILENO,''))", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND P.PSTATUS='Y'" + "", "IDNO");
            }
            else
            {
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

    private void FillLocation()
    {
        try
        {
            objCommon.FillDropDownList(ddlLoc, "PAYROLL_LOCATION_MASTER", "LOCNO", "LOCATION_NAME", "LOCNO<>0", "LOCNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindEmployee(int idno,int collegeno , int deptid , int stno)
    {
        try
        {
            DataSet ds = new DataSet();
            //ds = objLeave.GetEmployeeListLocationMapping(idno);
            ds = objLeave.GetEmployeeListLocationMapping(idno, collegeno, deptid, stno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLocUnMap.Visible = true;
                lvLocUnMap.DataSource = ds;
                lvLocUnMap.DataBind();
            }
            else
            {
                lvLocUnMap.DataSource = null;
                lvLocUnMap.DataBind();
                MessageBox("Sorry!Record Not Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindEmployee(Convert.ToInt32(ddlEmp.SelectedValue));
    }
    protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindLocation(Convert.ToInt32(ddlLoc.SelectedValue));
    }

    protected void BindLocation(int locno, int collegeno, int deptid, int stno)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objLeave.GetLocationWiseListLocationMapping(locno, collegeno, deptid, stno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpUnmap.Visible = true;
                lvEmpUnmap.DataSource = ds;
                lvEmpUnmap.DataBind();
            }
            else
            {
                lvEmpUnmap.DataSource = null;
                lvEmpUnmap.DataBind();
                MessageBox("Sorry!Record Not Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string COLLEGE_CODE = Convert.ToString(Session["colcode"]);
        DataTable dtMapRecord = new DataTable();
        dtMapRecord.Columns.Add("IDNO");
        dtMapRecord.Columns.Add("LOCNO");
        dtMapRecord.Columns.Add("SRNO");
        dtMapRecord.Columns["SRNO"].AutoIncrement = true; dtMapRecord.Columns["SRNO"].AutoIncrementSeed = 1; dtMapRecord.Columns["SRNO"].AutoIncrementStep = 1;
        int CREATEDBY = Convert.ToInt32(Session["userno"].ToString());
        int MODIFIEDBY = Convert.ToInt32(Session["userno"].ToString());
        objLM.LOCNO = Convert.ToInt32(ddlLoc.SelectedValue);
        objLM.IDNO = Convert.ToInt32(ddlEmp.SelectedValue);
        int Empid;
        int loc;
        if (radView.SelectedValue == "0")
        {
            int i = 0;
            foreach (ListViewDataItem items in lvEmployees.Items)
            {
                CheckBox chkSelect1 = items.FindControl("chkSelect") as CheckBox;
                if (chkSelect1.Checked)
                {
                    i = 1;
                    break;
                }

            }
            if (i == 0)
            {
                MessageBox("Please Select Atleast One Employee.");
                return;
            }

            foreach (ListViewDataItem items in lvEmployees.Items)
            {
                CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;

                if (chkSelect.Checked)
                {
                    Empid = Convert.ToInt32(chkSelect.ToolTip);

                    DataRow dr = dtMapRecord.NewRow();
                    dr["IDNO"] = Empid;
                    dr["LOCNO"] = objLM.LOCNO;
                    dtMapRecord.Columns["SRNO"].AutoIncrementStep = 1;
                    dtMapRecord.Rows.Add(dr);
                    dtMapRecord.AcceptChanges();

                }
            }
            CustomStatus cs = (CustomStatus)objLeave.AddEmpMapping(dtMapRecord, CREATEDBY, COLLEGE_CODE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved Successfully");
                Clear();
            }
        }
        else
        {
            int i = 0;
            foreach (ListViewDataItem items in lvLocation.Items)
            {
                CheckBox checkLoc1 = items.FindControl("checkLoc") as CheckBox;
                if (checkLoc1.Checked)
                {
                    i = 1;
                    break;
                }

            }
            if (i == 0)
            {
                MessageBox("Please Select Atleast One Employee.");
                return;
            }

            foreach (ListViewDataItem items in lvLocation.Items)
            {
                CheckBox checkLoc = items.FindControl("checkLoc") as CheckBox;

                if (checkLoc.Checked)
                {
                    loc = Convert.ToInt32(checkLoc.ToolTip);

                    DataRow dr = dtMapRecord.NewRow();
                    dr["IDNO"] = objLM.IDNO;
                    dr["LOCNO"] = loc;
                    dtMapRecord.Columns["SRNO"].AutoIncrementStep = 1;
                    dtMapRecord.Rows.Add(dr);
                    dtMapRecord.AcceptChanges();

                }
            }
            CustomStatus cs = (CustomStatus)objLeave.AddEmpMapping(dtMapRecord, CREATEDBY, COLLEGE_CODE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved Successfully");
                Clear();
            }
        }
        btnSaveUnMap.Enabled = true;
    }
    protected void btnUnMap_Click(object sender, EventArgs e)
    {
        if (radView.SelectedValue == "0")
        {
            BindUnmappedEmployeeForLocation(Convert.ToInt32(ddlLoc.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
            pnlEmpList.Visible = true;
            pnlLoc.Visible = false;
            pnlEmpUnMap.Visible = false;
            pnlLocUnMap.Visible = false;
        }
        else
        {
            BindUnMappedLocation(Convert.ToInt32(ddlEmp.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
            pnlEmpList.Visible = false;
            pnlLoc.Visible = true;
            pnlEmpUnMap.Visible = false;
            pnlLocUnMap.Visible = false;
        }
        btnSave.Enabled = true;
        btnSaveUnMap.Enabled = false;
    }

    protected void BindUnmappedEmployeeForLocation(int locno, int collegeno, int deptid, int stno)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objLeave.GetUnMappedEmployee(locno, collegeno, deptid, stno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmployees.Visible = true;
                lvEmployees.DataSource = ds;
                lvEmployees.DataBind();
            }
            else
            {
                lvEmployees.DataSource = null;
                lvEmployees.DataBind();
                MessageBox("Sorry!Record Not Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindUnMappedLocation(int idno, int collegeno, int deptid, int stno)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objLeave.GetUnMappedLocation(idno, collegeno, deptid, stno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLocation.Visible = true;
                lvLocation.DataSource = ds;
                lvLocation.DataBind();
            }
            else
            {
                lvLocation.DataSource = null;
                lvLocation.DataBind();
                MessageBox("Sorry!Record Not Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlEmp.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        ddlStafftype.SelectedIndex = 0;
        
        ddlLoc.SelectedIndex = 0;
        pnlEmpList.Visible = false;
        pnlLoc.Visible = false;
        radView.ClearSelection();
        divLoc.Visible = false;
        tremp.Visible = false;
        pnlEmpUnMap.Visible = false;
        pnlLocUnMap.Visible = false;

        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
        lvEmployees.Visible = false;

        lvLocUnMap.Visible = false;
        lvLocUnMap.DataSource = null;
        lvLocUnMap.DataBind();

        lvEmpUnmap.Visible = false;
        lvEmpUnmap.DataSource = null;
        lvEmpUnmap.DataBind();

        lvLocation.Visible = false;
        lvLocation.DataSource = null;
        lvLocation.DataBind();
    }
    protected void btnSaveUnMap_Click(object sender, EventArgs e)
    {
        
        DataTable dtMapRecord = new DataTable();
        dtMapRecord.Columns.Add("IDNO");
        dtMapRecord.Columns.Add("LOCNO");
        dtMapRecord.Columns.Add("SRNO");
        dtMapRecord.Columns["SRNO"].AutoIncrement = true; dtMapRecord.Columns["SRNO"].AutoIncrementSeed = 1; dtMapRecord.Columns["SRNO"].AutoIncrementStep = 1;
      
        objLM.LOCNO = Convert.ToInt32(ddlLoc.SelectedValue);
        objLM.IDNO = Convert.ToInt32(ddlEmp.SelectedValue);
        int Empid;
        int loc;
        if (radView.SelectedValue == "0")
        {
            int i = 0;
            foreach (ListViewDataItem items in lvEmpUnmap.Items)
            {
                CheckBox chkUN1 = items.FindControl("chkUN") as CheckBox;
                if (chkUN1.Checked)
                {
                    i = 1;
                    break;
                }

            }
            if (i == 0)
            {
                MessageBox("Please Select Atleast One Employee.");
                return;
            }

            foreach (ListViewDataItem items in lvEmpUnmap.Items)
            {
                CheckBox chkUN = items.FindControl("chkUN") as CheckBox;

                HiddenField hdnEmpMap = items.FindControl("hdnEmpMap") as HiddenField;


                if (chkUN.Checked)
                {
                    Empid = Convert.ToInt32(chkUN.ToolTip);
                    int map = Convert.ToInt32(hdnEmpMap.Value);

                    DataRow dr = dtMapRecord.NewRow();
                    dr["IDNO"] = Empid;
                    dr["LOCNO"] = objLM.LOCNO;
                    dtMapRecord.Columns["SRNO"].AutoIncrementStep = 1;
                    dtMapRecord.Rows.Add(dr);
                    dtMapRecord.AcceptChanges();

                    CustomStatus cs = (CustomStatus)objLeave.AddLocEmpUnMap(dtMapRecord, map);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //MessageBox("Record Saved Successfully");
                        //Clear();
                    }
                }
            }
            MessageBox("Record Saved Successfully");
            Clear();
           
        }
        else
        {
            int i = 0;
            foreach (ListViewDataItem items in lvLocUnMap.Items)
            {
                CheckBox checkUNLoc1 = items.FindControl("checkUNLoc") as CheckBox;
                if (checkUNLoc1.Checked)
                {
                    i = 1;
                    break;
                }

            }
            if (i == 0)
            {
                MessageBox("Please Select Atleast One Employee.");
                return;
            }

            foreach (ListViewDataItem items in lvLocUnMap.Items)
            {
                CheckBox checkUNLoc = items.FindControl("checkUNLoc") as CheckBox;

                HiddenField hdnLocMap = items.FindControl("hdnLocMap") as HiddenField;

                if (checkUNLoc.Checked)
                {
                    loc = Convert.ToInt32(checkUNLoc.ToolTip);
                    int locmap = Convert.ToInt32(hdnLocMap.Value);

                    DataRow dr = dtMapRecord.NewRow();
                    dr["IDNO"] = objLM.IDNO;
                    dr["LOCNO"] = loc;
                    dtMapRecord.Columns["SRNO"].AutoIncrementStep = 1;
                    dtMapRecord.Rows.Add(dr);
                    dtMapRecord.AcceptChanges();

                    CustomStatus cs = (CustomStatus)objLeave.AddLocEmpUnMap(dtMapRecord, locmap);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       // MessageBox("Record Saved Successfully");
                       // Clear();
                    }
                }
            }
            MessageBox("Record Saved Successfully");
            Clear();
        }
        btnSave.Enabled = true;
    }
    protected void btnMap_Click(object sender, EventArgs e)
    {
        if (radView.SelectedValue == "0")
        {
            BindLocation(Convert.ToInt32(ddlLoc.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
            pnlEmpUnMap.Visible = true;
            pnlLocUnMap.Visible = false;
            pnlEmpList.Visible = false;
            pnlLoc.Visible = false;
        }
        else
        {
            BindEmployee(Convert.ToInt32(ddlEmp.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue));
            pnlEmpUnMap.Visible = false;
            pnlLocUnMap.Visible = true;
            pnlEmpList.Visible = false;
            pnlLoc.Visible = false;
        }

        btnSave.Enabled = false;
        btnSaveUnMap.Enabled = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        
    }
    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillEmployee();
    //}
}