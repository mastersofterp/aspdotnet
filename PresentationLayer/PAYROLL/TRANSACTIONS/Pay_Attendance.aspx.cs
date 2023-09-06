//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Attendance.ASPX                                                    
// CREATION DATE : 22-JULY-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
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

public partial class PayRoll_Pay_Attendance : System.Web.UI.Page
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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                pnlSelect.Visible = true;                
                pnlAttendance.Visible = false;
                pnlNote.Visible = false;
                FillDropdown();
                FillDepartment();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Attendance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Attendance.aspx");
        }
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), Convert.ToInt32(chkAbsent.Checked), Convert.ToInt32(chkIdno.Checked), Convert.ToInt32(ddlDepartment.SelectedValue.ToString()), Convert.ToInt32(ddlCollege.SelectedValue.ToString()));

        //enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        //lblerror.Text = string.Empty;
        //lblmsg.Text = string.Empty;
    }

    protected void enablelistview(int index)
    {
        int absent;
        int orderByIdno;
        //if (!(Convert.ToInt32(index) == 0))
        //{
            if (chkAbsent.Checked)
                absent = 1;
            else
                absent = 0;

            if (chkIdno.Checked)
                orderByIdno = 1;
            else
                orderByIdno = 0;

            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), absent, Convert.ToInt32(ddlorderby.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue.ToString()), Convert.ToInt32(ddlCollege.SelectedValue.ToString()));
        //}
        //else
        //{
        //    pnlAttendance.Visible = false;
        //}

    }
    
    private void BindListViewList(int staffno, int showAbsent, int orderByIdno,int Dept, int collegeNo)
    {
       
        try
        {
            //if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            //{

                pnlAttendance.Visible = true;
                DataSet ds = objAttendance.GetAttendanceOfEmployee(staffno, showAbsent, orderByIdno, Dept, Convert.ToInt32(ddlEmployeeType.SelectedValue) ,collegeNo);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    
                    pnlNote.Visible = false;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    
                  //  lblcount.Text = ds.Tables[0].Rows.Count.ToString();
                    
                    pnlNote.Visible = true;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }
                
                lvAttendance.DataSource = ds;
                lvAttendance.DataBind();
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), Convert.ToInt32(chkAbsent.Checked), Convert.ToInt32(chkIdno.Checked), Convert.ToInt32(ddlDepartment.SelectedValue.ToString()), Convert.ToInt32(ddlCollege.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
        }
    }
    
    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvAttendance.Items)
            {
                TextBox txt = lvitem.FindControl("txtDays") as TextBox;
                TextBox txtOverTime = lvitem.FindControl("txtOverTime") as TextBox;
                if (txt.Text == string.Empty || txt.Text == "") { txt.Text = "0"; }
                if (txtOverTime.Text == string.Empty || txtOverTime.Text == "") { txtOverTime.Text = "0"; }
                CustomStatus cs = (CustomStatus)objAttendance.UpdateAbsentDays(Convert.ToDecimal(txt.Text), Convert.ToInt32(txt.ToolTip), Convert.ToDecimal(txtOverTime.Text));
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
            TextBox txtOverTime = lvitem.FindControl("txtOverTime") as TextBox;
            txt.Text = "0";
            txtOverTime.Text = "0";
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
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
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

    protected void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPTNO ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        enablelistview(Convert.ToInt32(ddlDepartment.SelectedIndex));
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;


        //this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), absent, Convert.ToInt32(ddlorderby.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue.ToString()), Convert.ToInt32(ddlCollege.SelectedValue.ToString()));
        

    }



    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int absent = 0;

            if (chkAbsent.Checked)
                absent = 1;
            else
                absent = 0;
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), absent, Convert.ToInt32(ddlorderby.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue.ToString()), Convert.ToInt32(ddlCollege.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int absent = 0;

            if (chkAbsent.Checked)
                absent = 1;
            else
                absent = 0;
            this.BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue.ToString()), absent, Convert.ToInt32(ddlorderby.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue.ToString()), Convert.ToInt32(ddlCollege.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
