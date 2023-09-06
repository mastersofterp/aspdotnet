//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayRoll_pay_modify_salary.ASPX                                                    
// CREATION DATE : 24-AUG-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_pay_modify_salary : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ModifySalaryController ObjModySalControl = new ModifySalaryController();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                pnlMonthlyChanges.Visible = false;
                cetxtStartDate.SelectedDate = System.DateTime.Today;
                FillPayHead(Convert.ToInt32(Session["userno"].ToString()));
                FillCollege();
                FillStaff();

            }
        }
        else
        {

            cetxtStartDate.SelectedDate = null;
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=pay_modify_salary.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=pay_modify_salary.aspx");
        }
    }

    private void BindListViewList(string payHead, int staffNo)
    {
        try
        {
            if (checkSalaryProcess() == 0)
            {
                //lblerror.Text = "Salary is not processed for " + " " + MonthYear();
                objCommon.DisplayMessage(UpdatePanel1, "Salary is not processed for " + " " + MonthYear(), this);
                pnlMonthlyChanges.Visible = false;
            }
            else
            {

                if (checkSalaryLocked() == "Y")
                {
                    if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
                    {

                        pnlMonthlyChanges.Visible = true;
                        string orderby;

                if (ddlorderby.SelectedValue == "0")
                {
                    orderby = "IDNO";
                }
                else if (ddlorderby.SelectedValue == "3")
                {
                    orderby = "PFILENO";
                }
                else if (ddlorderby.SelectedValue == "4")
                {
                    orderby = "FNAME";
                }
                else
                {
                    if (ddlorderby.SelectedValue == "1")
                        orderby = "IDNO";
                    else
                        orderby = "SEQ_NO";

                }

                        DataSet ds = ObjModySalControl.GetEmpMonthFile(payHead,Convert.ToInt32(ddlCollege.SelectedValue), staffNo, MonthYear(),Convert.ToInt32(ddlEmployeeType.SelectedValue), orderby);
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
                    }
                }
                else
                {
                    //lblerror.Text = checkSalaryLocked();
                    objCommon.DisplayMessage(UpdatePanel1, this.checkSalaryLocked(), this);
                    pnlMonthlyChanges.Visible = false;
                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
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
                TextBox txt = lvitem.FindControl("txtDays") as TextBox;
                if (txt.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UpdatePanel1,"Please enter amount for Idno " + txt.ToolTip + "", this.Page);
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)ObjModySalControl.UpdateSalaryMonFile(ddlPayhead.SelectedValue, Convert.ToDecimal(txt.Text), Convert.ToInt32(txt.ToolTip), txtMonthYear.Text);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
                }
            }

            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
            }

            // enablelistview(Convert.ToInt32(ddlStaff.SelectedIndex));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            txt.Text = string.Empty;
        }
        ddlStaff.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlPayhead.SelectedIndex = 0;

    }

    protected void FillPayHead(int uaNo)
    {
        try
        {  
            PayHeadPrivilegesController objPayHead = new PayHeadPrivilegesController();
            DataSet ds = null;
            ds = objPayHead.EditPayHeadUser(uaNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPayhead.DataSource = ds;
                ddlPayhead.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlPayhead.DataTextField = ds.Tables[0].Columns[2].ToString();
                ddlPayhead.DataBind();
                ddlPayhead.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillCollege()
    {
        try
        {
           // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = string.Empty;
            lblerror.Text = string.Empty;
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue));

            //to display employee count in footer
            txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChanges.Items.Count);

            //Used in javascript to display payhead desc
            hidPayhead.Value = ddlPayhead.SelectedItem.ToString();

            //display the total amount of payhead in footer 
            txtPayheadName.Text = "Total Amount of " + ddlPayhead.SelectedItem.ToString() + " = ";
            this.TotalPayheadAmount();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void TotalPayheadAmount()
    {
        try
        {
            decimal totalPayheadAmount = 0;

            foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
            {
                TextBox txt = lvitem.FindControl("txtDays") as TextBox;
                totalPayheadAmount = totalPayheadAmount + Convert.ToDecimal(txt.Text);
            }

            txtAmount.Text = totalPayheadAmount.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.TotalPayheadAmount-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected int checkSalaryProcess()
    {  
        int count=0;
        try
        {

            count = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + MonthYear() + "' and college_no='"+ Convert.ToInt32(ddlCollege.SelectedValue) +"' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue)));
           // count = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + MonthYear() + "' and college_no='" + Convert.ToInt32(ddlCollege.SelectedValue) + "' and staffno<>" + 6000));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.checkSalaryProcess-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return count;
    }

    protected string checkSalaryLocked()
    {
        string result=string.Empty;
        try
        {
            Boolean sallock;
            string status;
            sallock = Convert.ToBoolean(objCommon.LookUp("payroll_salfile", "sallock", "monyear='" + MonthYear() + "' and college_no='"+Convert.ToInt32(ddlCollege.SelectedValue) +"' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue)));
            status = objCommon.LookUp("payroll_salfile", "status", "monyear='" + MonthYear() + "' and college_no='" + Convert.ToInt32(ddlCollege.SelectedValue) + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue));

            if (sallock == true && status == "Y")
            {
                result = "Salary is processed and locked permanentely for" + " " + MonthYear().ToUpper();
            }
            else if (status == "Y" && sallock == false)
            {
                result = "Salary is processed and locked for" + " " + MonthYear().ToUpper();
            }
            else
            {
                result = "Y";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.checkSalaryLocked-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return result;
    }
 
    protected string MonthYear()
    {
        string monYear=string.Empty;
        try
        {
            monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.MonthYear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return monYear;
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStaff.SelectedIndex = 0;
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMonthlyChanges.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
}
