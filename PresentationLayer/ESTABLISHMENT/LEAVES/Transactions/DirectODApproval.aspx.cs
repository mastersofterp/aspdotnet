using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_DirectODApproval : System.Web.UI.Page
{
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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlApprove.Visible = false;
                //txtFromdt.Text = DateTime.Now.ToString();
                FillCollege();
                FillDepartment();
                //FillLeave();
                this.FillDropDown();
                BindLVLeaveApplPendingList();
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
                Response.Redirect("~/notauthorized.aspx?page=DirectLeaveApproval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DirectLeaveApproval.aspx");
        }
    }
    protected void BindLVLeaveApplPendingList()
    {
        try
        {
            int deptno = 0;
            int collegeno = 0;
            string dt = string.Empty;
            if (txtFromdt.Text != string.Empty)
            {
                dt = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                dt = "9999-12-31";
            }

            if (ddldept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            if (ddlCollege.SelectedIndex > 0)
            {
                collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            int staffno = Convert.ToInt32(ddlStaff.SelectedValue);
            //int lno = Convert.ToInt32(ddlLeaveType.SelectedValue);
            DataSet ds = objApp.GetPendListforODDirectApproval(deptno, staffno, dt, collegeno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //dpPager.Visible = true;
                //dpPager.Visible = true;
                lvPendingList.DataSource = ds;
                lvPendingList.DataBind();
                pnlList.Visible = true;

            }
            else
            {
                lvPendingList.DataSource = null;
                lvPendingList.DataBind();
                // dpPager.Visible = false;
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


    public void FillDropDown()
    {
        //objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "stafftype");

    }

    //protected void FillLeave()
    //{
    //    objCommon.FillDropDownList(ddlLeaveType, "PAYROLL_LEAVE_NAME", "LVNO", "Leave_Name", "", "LVNO");
    //}

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlLeaveType, "PAYROLL_LEAVE", "LNO", "LEAVENAME", "STNO=" + Convert.ToInt32(ddlStaff.SelectedValue), "LEAVENAME");
        BindLVLeaveApplPendingList();
    }
    protected void txtFromdt_TextChanged(object sender, EventArgs e)
    {
        BindLVLeaveApplPendingList();
    }


    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int ODTRNO = int.Parse(btnApproval.CommandArgument);

            ShowDetails(ODTRNO);
            lvPendingList.Visible = false;
            pnlAdd.Visible = false;
            pnlApprove.Visible = true;
            ViewState["action"] = "edit";
            btnShow.Visible = false;
            btnCancelAdd.Visible = false;
            btnBulkApprove.Visible = false;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    private void ShowDetails(Int32 ODTRNO)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objApp.GetODApplDetailDirectApproval(ODTRNO);

            //int YR = DateTime.Now.Year;
            //DataSet ds1 = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), YR, 0);//Session["idno"]
            //ds.Tables[1] = objApp.GetLeaveApplStatus(LETRNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ODTRNO"] = ODTRNO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                //lblLeaveName.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                lblFromdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                lblTodt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                lblNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                lblJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                lblReason.Text = ds.Tables[0].Rows[0]["PURPOSE"].ToString();
                lblOdCriteria.Text = ds.Tables[0].Rows[0]["ODCriteria"].ToString();
                int idno = Convert.ToInt32(ds.Tables[0].Rows[0]["EMPNO"]);
                lblPlace.Text = ds.Tables[0].Rows[0]["PLACE"].ToString();
                lblEvent.Text = ds.Tables[0].Rows[0]["PROGRAM_TYPE"].ToString();
                lblTopic.Text = ds.Tables[0].Rows[0]["TOPIC"].ToString();

            }

            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {

        BindLVLeaveApplPendingList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ODTRNO = Convert.ToInt32(ViewState["ODTRNO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
            string Remarks = txtRemarks.Text.ToString();
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntryDirectApprvlOD(ODTRNO, UA_NO, Status, Remarks, Aprdate, 0);
                    //cs = Convert.ToInt32(objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        lvPendingList.Visible = true;
                        pnlAdd.Visible = true;
                        pnlApprove.Visible = false;
                        ViewState["action"] = null;
                        objCommon.DisplayMessage("Record Updated Successfully", this);
                        BindLVLeaveApplPendingList();
                        clear();
                        btnShow.Visible = true;
                        btnCancelAdd.Visible = true;
                        btnBulkApprove.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = ddlStaff.SelectedIndex = ddldept.SelectedIndex = 0;
        txtFromdt.Text = string.Empty;
        BindLVLeaveApplPendingList();
        pnlList.Visible = true;
    }

    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //pnlAdd.Visible = true; pnlList.Visible = true;
        //pnlApprove.Visible = false;
        //ViewState["action"] = null;
        pnlList.Visible = true;
        lvPendingList.Visible = true;
        pnlAdd.Visible = true;
        pnlApprove.Visible = false;

        clear();
        pnlApprove.Visible = false;
        pnlAdd.Visible = true;
        btnShow.Visible = true;
        btnCancelAdd.Visible = true;
        btnBulkApprove.Visible = true;
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLVLeaveApplPendingList();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDepartment();
        BindLVLeaveApplPendingList();

    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

    }
    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            //if (ddlStaff.SelectedIndex > 0 && ddlCollege.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStaff.SelectedValue) + "", "DEPT.SUBDEPT");
            //}
            //if (ddlCollege.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            //}
            objCommon.FillDropDownList(ddldept, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVLeaveApplPendingList();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLVLeaveApplPendingList();
    }
    protected void btnBulkApprove_Click(object sender, EventArgs e)
    {
        int ODTRNO = 0;
        string Remarks;
        //int cs = 0;
        int checkcount = 0;
        try
        {

            foreach (ListViewDataItem items in lvPendingList.Items)
            {
                CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;

                int UA_NO = Convert.ToInt32(Session["userno"]);
                string Status;
                Status = Status = "A".ToString().Trim(); //txtRemarks.Text.ToString();
                //lvPendingList.FindControl 
                DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);

                Remarks = "Approved";

                if (chkSelect.Checked && chkSelect != null)
                {
                    checkcount = checkcount + 1;
                    ODTRNO = Convert.ToInt32(chkSelect.ToolTip);
                    // cs = Convert.ToInt32(objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0));
                    CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntryDirectApprvlOD(ODTRNO, UA_NO, Status, Remarks, Aprdate, 0);
                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Leave for Approval");
                return;
            }
            if (checkcount > 0)
            {
                MessageBox("Record Updated Successfully");

            }

            BindLVLeaveApplPendingList();
        }
        catch (Exception ex)
        {
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}