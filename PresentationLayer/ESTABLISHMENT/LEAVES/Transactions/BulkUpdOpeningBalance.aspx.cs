//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ESTABLISHMENT                                                             
// PAGE NAME     : Bulk update opening balance                                         
// CREATION DATE : 11-SEP-2011                                                         
// CREATED BY    :                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;


public partial class ACADEMIC_BulkUpdPermanentRegNo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    LeavesController objleave = new LeavesController();

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
        try
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
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (ViewState["action"] == null)
                        ViewState["action"] = "add";

                    this.FillStaff();
                    this.FillYear();

                    FillCollege();

                    FillPeriod();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkUpdOpeningBalance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkUpdOpeningBalance.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        //ListItem removeItem = ddlCollege.Items.FindByValue("0");
        // ddlCollege.Items.Remove(removeItem);
    }
    private void FillLeave()
    {
        try
        {
            //objCommon.FillDropDownList(ddlLeave, "payroll_leave", "LNO", "LEAVENAME", "stno="+ Convert.ToInt32(ddlStaffType.SelectedValue)+"AND PERIOD="+ Convert.ToInt32(ddlPeriod.SelectedValue), "LEAVENAME");
            objCommon.FillDropDownList(ddlLeave, "payroll_leave", "LNO", "LEAVENAME", "stno=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " and PERIOD=" + Convert.ToInt32(ddlPeriod.SelectedValue) + " ", "LEAVENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaffType, "payroll_stafftype", "stno", "stafftype", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "stafftype");
            //objCommon.FillDropDownList(ddldept, "payroll_subdept", "subdeptno", "subdept", "subdeptno NOT IN(0)", "subdept");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=BulkUpdPermanentRegNo.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=BulkUpdPermanentRegNo.aspx");
    //    }
    //}

    private void FillEmployeeListView()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("payroll_empmas E INNER JOIN PAYROLL_STAFFTYPE S ON ( S.STNO=E.STNO)", "IDNO", "(Fname+' '+MNAME+' '+LNAME)AS NAME", "S.STNO="+ Convert.ToInt32(ddlStaffType.SelectedValue), "IDNO");
            if (ddlStaffType.SelectedIndex != 0 && ddlLeave.SelectedIndex != 0 && ddlPeriod.SelectedIndex != 0)
            {

                Leaves objLM = new Leaves();




                objLM.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);

                int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + ddlLeave.SelectedValue));
                objLM.LEAVENO = leaveno;
                objLM.FROMDT = Convert.ToDateTime(txtFromDt.Text);
                objLM.TODT = Convert.ToDateTime(txtToDt.Text);


                objLM.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
                objLM.LNO = Convert.ToInt32(ddlLeave.SelectedValue);
                objLM.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
                objLM.TRANNO = Convert.ToInt32(ddlState.SelectedValue);
                objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                int leave_session_srno = objleave.AddGetLeaveSessionDetails(objLM);
                objLM.SESSION_SRNO = leave_session_srno;
                // objLM.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);

                //DataSet ds = objleave.GetEmployeesForBulkOpeningBalance(Convert.ToInt32(ddlStaffType.SelectedValue), Convert.ToInt32(ddlLeave.SelectedValue),
                //    Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue),leave_session_srno);
                DataSet ds = objleave.GetEmployeesForBulkOpeningBalance(objLM);
                int stfno = Convert.ToInt32(ddlStaffType.SelectedValue);
                int prdno = Convert.ToInt32(ddlPeriod.SelectedValue);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvLeave.DataSource = ds;
                        lvLeave.DataBind();
                        lvLeave.Visible = true;
                        pnlEmployee.Visible = true;
                    }
                    else
                    {
                        lvLeave.DataSource = null;
                        lvLeave.DataBind();
                        lvLeave.Visible = false;
                        pnlEmployee.Visible = false;
                    }
                }
                else
                {
                    lvLeave.DataSource = null;
                    lvLeave.DataBind();
                    lvLeave.Visible = false;
                    pnlEmployee.Visible = false;
                }
            }
            else
            {
                lvLeave.DataSource = null;
                lvLeave.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BulkUpdPermanentRegNo.FillStudentListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private static Mutex mutex = new Mutex();

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            mutex.WaitOne();
            Leaves objLM = new Leaves();
            CustomStatus cs = new CustomStatus();
            DataTable dtAppRecord = new DataTable();
            dtAppRecord.Columns.Add("IDNO");
            dtAppRecord.Columns.Add("LEAVES");

            //=======================================

            int records = 0;
            int updated = 0;

            objLM.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
            objLM.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);
            objLM.LNO = Convert.ToInt32(ddlLeave.SelectedValue);
            objLM.FROMDT = Convert.ToDateTime(txtFromDt.Text);
            objLM.TODT = Convert.ToDateTime(txtToDt.Text);
            objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objLM.TRANNO = Convert.ToInt32(ddlState.SelectedValue);

            int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + ddlLeave.SelectedValue));
            objLM.LEAVENO = leaveno;
            int leave_session_srno = objleave.AddGetLeaveSessionDetails(objLM);
            objLM.SESSION_SRNO = leave_session_srno;
            foreach (ListViewDataItem dataItem in lvLeave.Items)
            {
                //foreach (RepeaterItem ri in lvLeave.Items)
                //{

                records += 1;

                TextBox PrNo = dataItem.FindControl("txtOpBal") as TextBox;

                //Get Hidden Data..
                HiddenField hdfidno = dataItem.FindControl("hdfidno") as HiddenField;

                int idno = Convert.ToInt32(hdfidno.Value);

                //int leave = Convert.ToInt32(PrNo.Text);
                // string le = PrNo.Text.ToString().Substring(0,0);

                double leave;
                leave = Convert.ToDouble(PrNo.Text);

                objLM.EMPNO = idno;
                objLM.NO_DAYS = leave;


                //=================
                //if (leave != 0)
                if (leave >= 0) // Added by Sonal Banode to allow to update 0 balance leave                
                {
                    DataRow dr = dtAppRecord.NewRow();
                    dr["IDNO"] = idno;
                    dr["LEAVES"] = Convert.ToDouble(leave);

                    dtAppRecord.Rows.Add(dr);
                    dtAppRecord.AcceptChanges();
                }

            }
            if (ViewState["action"].ToString().Equals("add"))
            {

                cs = (CustomStatus)objleave.Add_Update_LEAVE(objLM, dtAppRecord);
                FillEmployeeListView();

            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                cs = (CustomStatus)objleave.Add_Update_LEAVE(objLM, dtAppRecord);
                FillEmployeeListView();

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Records Saved Successfully", this);
                clear();
                pnlEmployee.Visible = false;
                //return;


            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Records Updated Successfully", this);
                clear();
                pnlEmployee.Visible = false;
                //return;

            }
            else
                objCommon.DisplayMessage("Leave Opening Entry Not submitted", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BulkUpdOpeningBalance.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }


    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDepartment();
        FillEmployeeListView();
        this.FillLeave();
        // txtYear.Text = string.Empty;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillEmployeeListView();
        this.FillLeave();

        FillDepartment();
        ddldept.SelectedIndex = 0;


    }

    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            if (ddlStaffType.SelectedIndex > 0 && ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "", "DEPT.SUBDEPT");
            }
            else if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_BulkUpdOpeningBalance.FillDepartment ->" + ex.Message + "  " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlLeave_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeListView();
        pnlEmployee.Visible = true;
    }


    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = ddlState.SelectedValue;
        ViewState["action"] = "edit";
        FillEmployeeListView();
    }


    //protected void txtYear_TextChanged(object sender, EventArgs e)
    //{
    //    FillEmployeeListView();
    //}

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlPeriod.SelectedValue == "1")
            //{
            //    DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 7, 1);
            //    DateTime todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 12, 31);
            //    txtFromDt.Text = frmdt.ToString();
            //    txtToDt.Text = todt.ToString();
            //}
            //else if (ddlPeriod.SelectedValue == "2")
            //{
            //    DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 1, 1);
            //    DateTime todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 6, 30);
            //    txtFromDt.Text = frmdt.ToString();
            //    txtToDt.Text = todt.ToString();
            //}
            //else if (ddlPeriod.SelectedValue == "3")
            //{
            //    int year = Convert.ToInt32(ddlYear.SelectedValue);
            //    year = year + 1;
            //    DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 7, 1);
            //    DateTime todt = new DateTime(Convert.ToInt32(year), 6, 30);
            //    txtFromDt.Text = frmdt.ToString();
            //    txtToDt.Text = todt.ToString();
            //}
            //else
            //{
            //    txtFromDt.Text = System.DateTime.Now.ToString();
            //    txtToDt.Text = System.DateTime.Now.ToString();
            //}
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
            FillEmployeeListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BulkUpdPermanentRegNo.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //PAYROLL_LEAVE_PERIOD
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
    }

    private void FillYear()
    {
        int Yr = DateTime.Now.Year;
        ddlYear.Items.Add("Please Select");
        ddlYear.Items.Add(Convert.ToString(Yr - 1));
        ddlYear.Items.Add(Convert.ToString(Yr));
        ddlYear.Items.Add(Convert.ToString(Yr + 1));
        ddlYear.SelectedValue = (Convert.ToString(Yr));
    }
    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

        // FillEmployeeListView();
        if (ddlLeave.SelectedIndex > 0)
        {
            ddlLeave.SelectedIndex = 0;
        }
        //ddlYear.SelectedIndex = 0;

        lvLeave.DataSource = null;
        lvLeave.DataBind();
        lvLeave.Visible = false;
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
            txtFromDt.Text = frmdt.ToString("dd/MM/yyyy");
            txtToDt.Text = todt.ToString("dd/MM/yyyy");
        }
        this.FillLeave();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlStaffType.SelectedIndex = 0;
        lvLeave.DataSource = null;
        lvLeave.DataBind();
        pnlEmployee.Visible = false;
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        ddlLeave.SelectedIndex = 0;
        ddlPeriod.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlStaffType.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        txtFromDt.Text = txtToDt.Text = string.Empty;
        pnlEmployee.Visible = false;
    }

    private void clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlStaffType.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlPeriod.SelectedIndex = 0;
        ddlLeave.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        txtFromDt.Text = txtToDt.Text = string.Empty;
    }
}
