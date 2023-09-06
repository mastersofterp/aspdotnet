using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using IITMS.UAIMS;

public partial class ESTABLISHMENT_LEAVES_Transactions_DoubleDutyApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ShiftManagementController objSC = new ShiftManagementController();
    ShiftManagement objSM = new ShiftManagement();

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
                CheckPageAuthorization();
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                pnllistPending.Visible = true;
                FillCollege();
                FillStaffType();

                int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
                int prevyr = System.DateTime.Today.AddYears(-1).Year;
                int month = System.DateTime.Today.Month;
                int year = System.DateTime.Today.Year;
                string frmdt = null;
                //if (month == 1)
                //{
                //    frmdt = "21" + "/" + "12" + "/" + prevyr.ToString();
                //}
                //else
                //{
                //    frmdt = "21" + "/" + prevmonth.ToString() + "/" + year.ToString();
                //}


                //string todt = "20" + "/" + month.ToString() + "/" + year.ToString();

                //txtFromDt.Text = frmdt;
                //txtToDt.Text = todt;

                BindListView();
            }
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
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
    //To popup the messagebox.
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }
    public void FillStaffType()
    {
        objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
    }
    protected void BindListView()
    {
        try
        {
            if (txtFromDt.Text != string.Empty)
            {
                objSM.FROMDATE = Convert.ToDateTime(txtFromDt.Text);
            }
            if (txtToDt.Text != string.Empty)
            {
                objSM.TODATE = Convert.ToDateTime(txtToDt.Text);
            }

            objSM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSM.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            DataSet ds = objSC.GetEmployeeListforDoubleDutyApproval(objSM);

            lvPendingList.DataSource = ds;
            lvPendingList.DataBind();

            pnllistPending.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DoubleDutyApproval.BindListView ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    //btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");
            dtEmpRecord.Columns.Add("SHIFTDATE");
            dtEmpRecord.Columns.Add("SHIFTNO");
            dtEmpRecord.Columns.Add("INTIME");
            dtEmpRecord.Columns.Add("OUTTIME");
            dtEmpRecord.Columns.Add("STATUS");
            int checkcount = 0;
            objSM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSM.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objSM.FROMDATE = Convert.ToDateTime(txtFromDt.Text);
            objSM.TODATE = Convert.ToDateTime(txtToDt.Text);
            foreach (ListViewDataItem lvItem in lvPendingList.Items)
            {
                CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
                Label lblShiftName = lvItem.FindControl("lblShiftName") as Label;
                Label lblShiftDate = lvItem.FindControl("lblShiftDate") as Label;

                Label lblInTime = lvItem.FindControl("lblInTime") as Label;
                Label lblOutTime = lvItem.FindControl("lblOutTime") as Label;

                if (chk.Checked == true)
                {
                    checkcount += 1;

                    DataRow dr = dtEmpRecord.NewRow();
                    dr["IDNO"] = Convert.ToInt32(chk.ToolTip);

                    dr["SHIFTDATE"] = Convert.ToDateTime(lblShiftDate.Text).ToString("yyyy-MM-dd");
                    dr["SHIFTNO"] = lblShiftDate.ToolTip.ToString().Trim();

                    dr["INTIME"] = lblInTime.Text.ToString().Trim();
                    dr["OUTTIME"] = lblOutTime.Text.ToString().Trim();
                    dr["STATUS"] = "A".ToString().Trim();

                    dtEmpRecord.Rows.Add(dr);
                    dtEmpRecord.AcceptChanges();

                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Employee");
                return;
            }
            CustomStatus cs = (CustomStatus)objSC.AddUpdateDoubleDutyApproval(dtEmpRecord);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record saved successfully");
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DoubleDutyApproval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = ddlStaffType.SelectedIndex = 0;
        txtFromDt.Text = txtToDt.Text = string.Empty;
        pnllistPending.Visible = false;
        lvPendingList.DataSource = null;
        lvPendingList.DataBind();

    }
}