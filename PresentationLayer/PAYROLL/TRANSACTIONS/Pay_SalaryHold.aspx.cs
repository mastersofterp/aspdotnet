using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class PAYROLL_TRANSACTIONS_Pay_SalaryHold : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        CheckRef();

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                pnlAttendance.Visible = false;
                pnlNote.Visible = false;
                FillDropdown();
            }
        }
    }

    //protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
    //    lblerror.Text = string.Empty;
    //    lblmsg.Text = string.Empty;
    //}

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void enablelistview(int index)
    {
        int showHoldEmp;
        int orderByIdno;
        if (!(Convert.ToInt32(index) == 0))
        {
            if (chkAbsent.Checked)
                showHoldEmp = 1;
            else
                showHoldEmp = 0;

            if (chkIdno.Checked)
                orderByIdno = 1;
            else
                orderByIdno = 0;

            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), ddlMonth.SelectedItem.Text, showHoldEmp, orderByIdno);
        }
        else
        {
            pnlAttendance.Visible = false;
        }

    }

    private void BindListViewList(int staffno, string month, int showHoldEmp, int orderByIdno)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {

                pnlAttendance.Visible = true;
                DataSet ds = objAttendance.GetEmployeeForSalaryHold(staffno, month, showHoldEmp, orderByIdno);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    pnlNote.Visible = false;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    pnlNote.Visible = true;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }

                lvAttendance.DataSource = ds;
                lvAttendance.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvAttendance.Items)
            {
                CheckBox txt = lvitem.FindControl("chkHoldSalary") as CheckBox;
                CustomStatus cs = (CustomStatus)objAttendance.UpdateEmployeeHoldStatus(ddlMonth.SelectedItem.Text, txt.Checked, Convert.ToInt32(txt.ToolTip));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }

            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);

            }

            enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        foreach (ListViewDataItem lvitem in lvAttendance.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            txt.Text = string.Empty;
        }
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlAttendance.Visible = false;

    }

    protected void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlMonth, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "", "convert(datetime,monyear,103) desc");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkIdno_CheckedChanged(object sender, EventArgs e)
    {
        enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void chkAbsent_CheckedChanged(object sender, EventArgs e)
    {
        enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
    }

    protected void CheckRef()
    {
        string LWP_PDay;
        LWP_PDay = objCommon.LookUp("PAYROLL_PAY_REF", "LWP_PDay", "");
        if (LWP_PDay == "1")
        {
            tdAbsentDays.Visible = false;
            tdPresentDays.Visible = true;
        }
        else
        {
            tdAbsentDays.Visible = true;
            tdPresentDays.Visible = false;
        }
    }
}