//============================================
//CREATED BY: SWATI GHATE
//CREATED DATE: 05-08-2014
//PURPOSE: TO RESTORE THE PENDING & APPROVED LEAVE (LEAVE CANCELLATION)
//============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Leave_Cancel : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    Leaves objLeaveMaster = new Leaves();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }
    protected void Page_Load(object sender, EventArgs e)
    {// btnSave.Attributes.Add("onClick", "fun();");
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
                FillStaffType();
                FillDepartment();
                FillCollege();

                //FillLeave();
                FillUser();
                ddlEmp.SelectedIndex = 0;
                lvEmployees.Visible = false;
                CheckPageAuthorization();

            }
            //
        }
       
        //btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Leave_Cancel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Leave_Cancel.aspx");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

    }
    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");

            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO <> 0", "SUBDEPTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
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
       
        ddlStafftype.SelectedIndex =ddlCollege.SelectedIndex= 0;
        ddldept.SelectedIndex = 0;
        ddlEmp.SelectedIndex = 0;
       // pnlEmpList.Visible = false;
        lvEmployees.Visible = false;
        txtFromdt.Text = string.Empty;
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlStafftype.SelectedIndex != 0)
        //{
        //    objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON (E.SUBDEPTNO=D.SUBDEPTNO)", "E.IDNO", "(ISNULL(FNAME,'') +'  '+ISNULL(MNAME,'') +'  '+ ISNULL(LNAME,''))AS NAME", "(E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " OR " + Convert.ToInt32(ddldept.SelectedValue) + " = 0) AND STNO=  " + Convert.ToInt32(ddlStafftype.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "NAME");

        //    lvEmployees.DataSource = null;
        //    lvEmployees.DataBind();

        //}
        //else
        //{
        //    ddlEmp.SelectedIndex = 0;
        //    lvEmployees.DataSource = null;
        //    lvEmployees.DataBind();


        //}
        //function will call here
       // btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");
        FillUser();

    }

    
   
   

    //private void FillUser()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') as ENAME", "", "IDNO");

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillUser ->" + ex.Message + "  " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void FillUser()
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0 && ddldept.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "ENAME");
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue) + " AND E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex == 0 && ddlStafftype.SelectedIndex > 0 && ddldept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex == 0 && ddlStafftype.SelectedIndex == 0 && ddldept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0 && ddldept.SelectedIndex == 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex == 0 && ddldept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex == 0 && ddldept.SelectedIndex == 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "ENAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "", "ENAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()      
    {
        if ( ddlEmp.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0 && txtFromdt.Text != string.Empty)
        {
            objLeaveMaster.EMPNO = Convert.ToInt32(ddlEmp.SelectedValue);
            objLeaveMaster.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
            
            objLeaveMaster.FROMDT = Convert.ToDateTime(txtFromdt.Text.ToString());

            DataSet ds = objLeave.GetAllLeavesByEmp(objLeaveMaster);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmployees.DataSource = ds;
                lvEmployees.DataBind();
                lvEmployees.Visible = true;
                btnRestore.Enabled = true;

            }
            else
            {
                objCommon.DisplayMessage("Record Not found", this);
                lvEmployees.DataSource = null;
                lvEmployees.DataBind();
                btnRestore.Enabled = false;
                return;
            }
        }
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            BindListView();
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.ddlEmp_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillUser();
    }

    protected void btnRestore_Click(object sender, EventArgs e)
    {
      
        int cs = 0;
        int i = 0;
        int LeaveTrNo = 0;
        int eno = 0, lno = 0;
        objLeaveMaster.ENO = Convert.ToInt32(ddlEmp.SelectedValue);


        foreach (ListViewDataItem items in lvEmployees.Items)
        {
            CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked)
            {
                i = 1;
                break;
            }

        }
        if (i == 0)
        {
            objCommon.DisplayMessage("Please Select At Least One Leave To Restore", this.Page);
            return;
        }
        foreach (ListViewDataItem items in lvEmployees.Items)
        {
            CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
            HiddenField hdnLeaveTrNo = items.FindControl("hdnLeaveTrNo") as HiddenField;
            HiddenField hdnFdt = items.FindControl("hdnFdt") as HiddenField;
            HiddenField hdnTdt = items.FindControl("hdnTdt") as HiddenField;
            TextBox txtlvclreason = items.FindControl("txtlvcancelRemark") as TextBox;

            if (chkSelect.Checked)
            {
               objLeaveMaster.LETRNO = Convert.ToInt32(hdnLeaveTrNo.Value);
               objLeaveMaster.LNO = 0;//not used yet

               objLeaveMaster.FROMDT =Convert.ToDateTime(hdnFdt.Value);
               objLeaveMaster.TODT = Convert.ToDateTime(hdnTdt.Value);


               string DtFrom = Convert.ToDateTime(hdnFdt.Value).ToString("yyyy-MM-dd");
               string DtTo = Convert.ToDateTime(hdnTdt.Value).ToString("yyyy-MM-dd");
               objLeaveMaster.LVCANCELRMARK = txtlvclreason.Text;
                //cs = Convert.ToInt32(objLeave.CreditLeaves(Period, Year, StNo, Lno, ad, ad, pd, Convert.ToString(Session["colcode"]), Empid, prvlno, deptno, leaveno));


                cs = Convert.ToInt32(objLeave.LeaveCancel(objLeaveMaster));
            }
        }
        if (cs == -99)
        {
            objCommon.DisplayMessage("Records Not Updated", this);
           // BindListView();
            return;
        }
        else
        {
            objCommon.DisplayMessage("Records Updated Successfully", this);
            BindListView();
            Clear();

        }
        Clear();
        btnRestore.Enabled = true;
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlStafftype.SelectedIndex = 0;
        //ddlEmp.Items.Clear();
        ////ddlEmp.SelectedIndex = 0;
        //lvEmployees.DataSource = null;
        //lvEmployees.DataBind();
        //lvEmployees.Visible = false;
        //txtFromdt.Text = string.Empty;
        FillUser();
    }
    protected void txtFromdt_TextChanged(object sender, EventArgs e)
    {
      
        ddlEmp.SelectedIndex = 0;
        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
       
    }
}
