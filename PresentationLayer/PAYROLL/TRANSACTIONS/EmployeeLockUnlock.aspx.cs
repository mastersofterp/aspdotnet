using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_EmployeeLockUnlock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();
    EmployeeLockUnlockController objEmpLockUnlock = new EmployeeLockUnlockController();
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

        // CheckRef();

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
               // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                pnlMonthlyChanges.Visible = false;

                FillStaff();
                FillDepartment();
            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EmployeeLockUnlock.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EmployeeLockUnlock.aspx");
        }
    }

    private void BindListViewList(int staffNo, string Dept, int collegeNo)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {
                string orderby;

                if (ddlorderby.SelectedValue == "0")
                {
                    orderby = "IDNO";
                }
                else
                {
                    if (ddlorderby.SelectedValue == "1")
                        orderby = "IDNO";
                    else
                        orderby = "SEQ_NO";

                }


                pnlMonthlyChanges.Visible = true;
                DataSet ds = objEmpLockUnlock.GetEmployeesForLockUnlock(staffNo, orderby, Dept, collegeNo);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }

                lvMonthlyChanges.DataSource = ds;
                lvMonthlyChanges.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        CheckBox chk = lvMonthlyChanges.Items[i].FindControl("chkEmployeeLockUnlock") as CheckBox;

                        Boolean checktrue = Convert.ToBoolean(ds.Tables[0].Rows[i]["EMPLOYEE_LOCK"] .ToString());

                        if (checktrue)
                            chk.Checked = true;
                        else
                            chk.Checked = false;
                    }
                }
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

            foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
            {
                //TextBox txt = lvitem.FindControl("txtDays") as TextBox;
                CheckBox chk = lvitem.FindControl("chkEmployeeLockUnlock") as CheckBox;
                bool chklock;
                if (chk.Checked)
                {
                    chklock = true;
                }
                else
                {
                    chklock = false;
                }

                CustomStatus cs = (CustomStatus)objEmpLockUnlock.UpdateEmployeeLockUnlock(chklock, Convert.ToInt32(chk.ToolTip));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }

            if (count == 1)
            {
                objCommon.DisplayMessage("Record Updated Successfully", this);
            }

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
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlCollege.SelectedIndex = 0; //add
        ddlDeptmon.SelectedIndex = 0; //add
        ddlorderby.SelectedIndex = 0; //add
    }

   

    protected void FillStaff()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDeptmon, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPTNO ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;
        //int value = Convert.ToInt32(ddlpayruleselect.SelectedValue);
        BindListViewList(Convert.ToInt32(ddlStaff.SelectedValue), ddlDeptmon.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue));

        //to display employee count in footer
        txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChanges.Items.Count);


       // this.TotalPayheadAmount();
    }


    protected void TotalPayheadAmount()
    {
        decimal totalPayheadAmount = 0;

        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            totalPayheadAmount = totalPayheadAmount + Convert.ToDecimal(txt.Text);
        }

        txtAmount.Text = totalPayheadAmount.ToString();

    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            message = message.Replace("'", "\'");
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

  
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStaff.SelectedIndex = 0;
        ddlDeptmon.SelectedIndex = 0;
        // ddlorderby.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeptmon.SelectedIndex = 0;
        //ddlorderby.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlDeptmon_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ddlorderby.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
    }
    
}
