//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : CarryForwardLeave.aspx
// CREATION DATE :                                                   
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




public partial class CarryForwardLeave : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLC = new LeavesController();
    Leaves objLM = new Leaves();

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
                CheckPageAuthorization();
                FillCollege();


                FillStaffType();
                FillYear();
                FillPeriod();
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
                Response.Redirect("~/notauthorized.aspx?page=CarryForwardLeave.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CarryForwardLeave.aspx");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillYear()
    {
        try
        {
            int Yr = DateTime.Now.Year;

            ddlYear.Items.Add(Convert.ToString(Yr - 1));
            ddlYear.Items.Add(Convert.ToString(Yr));
            ddlYear.Items.Add(Convert.ToString(Yr + 1));
            ddlYear.SelectedValue = (Convert.ToString(Yr - 1));

            ddlNewYear.Items.Add(Convert.ToString(Yr - 1));
            ddlNewYear.Items.Add(Convert.ToString(Yr));
            ddlNewYear.Items.Add(Convert.ToString(Yr + 1));
            ddlNewYear.SelectedValue = (Convert.ToString(Yr));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.FillYear ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

    }
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD_NAME");
        objCommon.FillDropDownList(ddlNewPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD_NAME");
    }
    protected void FillLeaves(int stno, int period)
    {
        try
        {
            //select * from payroll_leave where leaveno=3 and period=1
            objCommon.FillDropDownList(ddlLeavename, "PAYROLL_LEAVE", "LEAVENO", "LEAVENAME", "PERIOD=" + Convert.ToInt32(period) + "and isnull(CARRY,0)=1  and stno=" + Convert.ToInt32(stno) + "", "LEAVENAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.FillLeaves->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }




    protected void btnShow_Click(object sender, EventArgs e)
    {

        try
        {
            if (rblType.SelectedValue == "0")
            {
                btnSave.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
            }
            objLM.YEAR = Convert.ToInt32(ddlNewYear.SelectedValue);
            objLM.PERIOD = Convert.ToInt32(ddlNewPeriod.SelectedValue);

            objLM.LEAVENO = Convert.ToInt32(ddlLeavename.SelectedValue);

            objLM.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            objLM.FROMDT = Convert.ToDateTime(txtFromDt.Text);
            objLM.TODT = Convert.ToDateTime(txtToDt.Text);

            //objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            int exist_period = Convert.ToInt32(ddlPeriod.SelectedValue);
            int exist_year = Convert.ToInt32(ddlYear.SelectedValue);

            DataSet ds = objLC.GetCarryForwordLeave(objLM, exist_period, exist_year, Convert.ToInt32(rblType.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmployees.DataSource = ds;
                lvEmployees.DataBind();
                lvEmployees.Visible = true;
                pnlEmpList.Visible = true;

            }
            else
            {
                lvEmployees.DataSource = null;
                lvEmployees.DataBind();
                pnlEmpList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.btnShow_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Leaves objLM = new Leaves();
        LeavesController objLC = new LeavesController();
        try
        {
            objLM.YEAR = Convert.ToInt32(ddlNewYear.SelectedValue);
            objLM.PERIOD = Convert.ToInt32(ddlNewPeriod.SelectedValue);

            // objLM.LEAVENO = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + Convert.ToInt32(ddlLeavename.SelectedValue) + "")); 
            objLM.LEAVENO = Convert.ToInt32(ddlLeavename.SelectedValue); //Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + Convert.ToInt32(ddlLeavename.SelectedValue) + "")); 

            objLM.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            objLM.FROMDT = Convert.ToDateTime(txtFromDt.Text);
            objLM.TODT = Convert.ToDateTime(txtToDt.Text);

            //objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            int exist_period = Convert.ToInt32(ddlPeriod.SelectedValue);
            int exist_year = Convert.ToInt32(ddlYear.SelectedValue);
            //CustomStatus cs = (CustomStatus)objLC.AddCarryForwordLeave(objLM);
            objLM.TRANNO = 1;
            CustomStatus cs = (CustomStatus)objLC.AddCarryForwordLeave(objLM, exist_period, exist_year, Convert.ToInt32(rblType.SelectedValue));

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Transfered Successfully");
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.btnSave_click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlYear.Items.Clear(); ddlNewYear.Items.Clear();
        FillYear();
        Clear();
    }
    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        try
        {
            ddlCollege.SelectedIndex = 0;
            ddlLeavename.SelectedIndex =0;
            ddlStafftype.SelectedIndex = 0;
            ddlPeriod.SelectedIndex = 0;
            ddlNewPeriod.SelectedIndex = 0;
            ddlNewYear.SelectedIndex = 0;
            lvEmployees.DataSource = null;
            lvEmployees.DataBind();
            pnlEmpList.Visible = false;
            rblType.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.Clear ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlLeavename_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindLVLeaveApplPendingList();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillDepartment();

    }


    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlPeriod.SelectedIndex != 0)
            //{
            //    FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue));
            //    lvEmployees.Visible = false;
            //}
            //else
            //{
            //    ddlLeavename.Items.Clear();
            //}
            if (ddlPeriod.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
            {
                DateTime frmdt; DateTime todt;
                int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
                int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
                int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), to);
                //int days = DateTime.DaysInMonth(year, month);
                int year = Convert.ToInt32(ddlYear.SelectedValue);
                year = year + 1;
                frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), from, 1);
                if (to < from)
                {
                    todt = new DateTime(Convert.ToInt32(year), to, days);
                }
                else
                {
                    todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), to, days);
                }
                txtFromDt.Text = frmdt.ToString();
                txtToDt.Text = todt.ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.ddlPeriod_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPeriod.SelectedIndex > 0)
            {
                DateTime frmdt; DateTime todt;
                int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
                int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
                int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), to);
                //int days = DateTime.DaysInMonth(year, month);
                int year = Convert.ToInt32(ddlYear.SelectedValue);
                year = year + 1;
                frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), from, 1);
                if (to < from)
                {
                    todt = new DateTime(Convert.ToInt32(year), to, days);
                }
                else
                {
                    todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), to, days);
                }
                txtFromDt.Text = frmdt.ToString();
                txtToDt.Text = todt.ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_CarryForwardLeave.ddlYear_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlNewPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStafftype.SelectedIndex > 0 && ddlNewPeriod.SelectedIndex > 0)
        {
            FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue), Convert.ToInt32(ddlNewPeriod.SelectedValue));
        }
        if (ddlNewPeriod.SelectedIndex > 0 && ddlNewYear.SelectedIndex > 0)
        {
            DateTime frmdt; DateTime todt;
            int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlNewPeriod.SelectedValue) + ""));
            int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlNewPeriod.SelectedValue) + ""));
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlNewYear.SelectedValue), to);
            //int days = DateTime.DaysInMonth(year, month);
            int year = Convert.ToInt32(ddlNewYear.SelectedValue);
            year = year + 1;
            frmdt = new DateTime(Convert.ToInt32(ddlNewYear.SelectedValue), from, 1);
            if (to < from)
            {
                todt = new DateTime(Convert.ToInt32(year), to, days);
            }
            else
            {
                todt = new DateTime(Convert.ToInt32(ddlNewYear.SelectedValue), to, days);
            }
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }
    }
    protected void ddlNewYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStafftype.SelectedIndex > 0 && ddlNewPeriod.SelectedIndex > 0)
        {
            FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue), Convert.ToInt32(ddlNewPeriod.SelectedValue));
        }
        if (ddlNewPeriod.SelectedIndex > 0)
        {
            DateTime frmdt; DateTime todt;
            int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlNewPeriod.SelectedValue) + ""));
            int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlNewPeriod.SelectedValue) + ""));
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlNewYear.SelectedValue), to);
            //int days = DateTime.DaysInMonth(year, month);
            int year = Convert.ToInt32(ddlNewYear.SelectedValue);
            year = year + 1;
            frmdt = new DateTime(Convert.ToInt32(ddlNewYear.SelectedValue), from, 1);
            if (to < from)
            {
                todt = new DateTime(Convert.ToInt32(year), to, days);
            }
            else
            {
                todt = new DateTime(Convert.ToInt32(ddlNewYear.SelectedValue), to, days);
            }
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }
    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblType.SelectedValue == "0")//already carried
        {
            btnSave.Visible = false;
        }
        else
        {
            btnSave.Visible = true;
        }
    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStafftype.SelectedIndex > 0 && ddlNewPeriod.SelectedIndex > 0)
        {
            FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue), Convert.ToInt32(ddlNewPeriod.SelectedValue));
        }

    }
}
