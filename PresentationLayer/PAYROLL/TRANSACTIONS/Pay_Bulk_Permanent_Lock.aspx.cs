//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : pay_lock_salary.aspx                                                  
// CREATION DATE : 24-Aug-2009                                                        
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

public partial class PAYROLL_TRANSACTIONS_Pay_Bulk_Permanent_Lock : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    lockUlockSalaryController objLokSalCon = new lockUlockSalaryController();
    string Status = "";

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
                FillStaff();
                BindListViewList();
                PnlLockPermantely.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=pay_lock_salary.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=pay_lock_salary.aspx");
        }
    }

    private void BindListViewList()
    {
        int Collegeno;
        try
        {
            if (ddlCollegeFilter.SelectedIndex > 0)
            {
                Collegeno = Convert.ToInt32(ddlCollegeFilter.SelectedValue);
            }
            else
            {
                Collegeno = 0;
            }
            pnlLockUnlock.Visible = true;
            lvLockUnlock.Visible = true;

            DataSet ds = objLokSalCon.GetStaffSalFileBulk(Collegeno);
            lvLockUnlock.DataSource = ds;
            lvLockUnlock.DataBind();
            if (ds.Tables[0].Rows.Count == 0)
            {
                btnLockSalary.Visible = false;
                btnSave.Visible = false;
            }
            else
            {
                btnLockSalary.Visible = true;
                btnSave.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (ListViewDataItem lvitem in lvLockUnlock.Items)
            {
                TextBox txt = lvitem.FindControl("txtYesNo") as TextBox;

                CustomStatus cs = (CustomStatus)objLokSalCon.UpdateUnlockSalary(Convert.ToInt32(txt.ToolTip), txt.Text);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
                if (count == 1)
                {
                    //lblerror.Text = null;
                    //lblmsg.Text = "Record Updated Successfully";
                    objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);

                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.btnSub_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnLockSalary_Click(object sender, EventArgs e)
    {
        lblmsg.Text = null;
        lblerror.Text = null;
        PnlLockPermantely.Visible = true;
        pnlLockUnlock.Visible = false;


    }

    protected void butLockSalaryPermanentlynew_Click(object sender, EventArgs e)
    {

        try
        {
            int count1 = 0;
            int count2 = 0;
            foreach (ListViewDataItem lvitem in lvLockUnlock.Items)
            {
                CheckBox Chkitem = lvitem.FindControl("Chkitem") as CheckBox;
                TextBox txtEditFieldDT = lvitem.FindControl("txtEditFieldDT") as TextBox;
                if (Chkitem.Checked == true)
                {
                    count2 = count2 + 1;
                    if (txtEditFieldDT.Text == string.Empty || txtEditFieldDT.Text == "")
                    {
                        count1 = count1 + 1;
                    }
                }
            }
            if (count2 <= 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please select atleast single salary for lock !", this);
                return;
            }
            if (count1 > 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Enter Salary Deposited Date !", this);
                return;
            }
            


            int count = 0;
            foreach (ListViewDataItem lvitem in lvLockUnlock.Items)
            {
                HiddenField hdnSalNo = lvitem.FindControl("hdnSalNo") as HiddenField;
                HiddenField hdnStaffno = lvitem.FindControl("hdnStaffno") as HiddenField;
                HiddenField hdnCollegeNo = lvitem.FindControl("hdnCollegeNo") as HiddenField;
                TextBox txtEditFieldDT = lvitem.FindControl("txtEditFieldDT") as TextBox;
                HiddenField hdnMonthYear = lvitem.FindControl("hdnMonthYear") as HiddenField;
                CheckBox Chkitem = lvitem.FindControl("Chkitem") as CheckBox;

                string dates = Convert.ToDateTime(hdnMonthYear.Value).ToString("MM/yyyy");

                if (Chkitem.Checked == true)
                {
                    CustomStatus cs = (CustomStatus)objLokSalCon.UpdatelockSalaryBulk(Convert.ToInt32(hdnStaffno.Value), dates, txtEditFieldDT.Text, Convert.ToInt32(hdnCollegeNo.Value), Convert.ToInt32(hdnSalNo.Value));

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
                    if (count == 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Salary locked permanently successfully", this);
                        //pnlLockUnlock.Visible = true;
                        //PnlLockPermantely.Visible = false;
                        // ClearControls();
                        BindListViewList();

                    }
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.btnSub_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butLockSalaryPermanently_Click(object sender, EventArgs e)
    {

        try
        {
            if (checkSalaryProcess() == 0)
            {
                //lblerror.Text = "Salary is not processed for" + " " + MonthYear();
                objCommon.DisplayMessage(UpdatePanel1, "Salary is not processed for" + " " + MonthYear() + " " + "successfully", this);

            }
            else
            {

                CustomStatus cs = (CustomStatus)objLokSalCon.UpdatelockSalary(Convert.ToInt32(ddlStaff.SelectedValue), txtMonthYear.Text, txtDepDate.Text, Convert.ToInt32(ddlCollege.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //lblerror.Text = null;
                    //lblmsg.Text = "Salary Locked Permanently for" + " " + MonthYear();
                    objCommon.DisplayMessage(UpdatePanel1, "Salary locked permanently for" + " " + MonthYear() + " " + "successfully", this);
                    pnlLockUnlock.Visible = true;
                    PnlLockPermantely.Visible = false;
                    ClearControls();
                    BindListViewList();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.butLockSalaryPermanently_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void ClearControls()
    {
        txtMonthYear.Text = string.Empty;
        txtDepDate.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;

    }
    protected void butBacknew_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }



    protected void butBack_Click(object sender, EventArgs e)
    {
        PnlLockPermantely.Visible = false;
        pnlLockUnlock.Visible = true;
        lblerror.Text = string.Empty;
    }

    protected void FillStaff()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            //objCommon.FillDropDownList(ddlCollegeFilter, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollegeFilter, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected string MonthYear()
    {
        string monYear = string.Empty;
        try
        {

            monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).Year.ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.MonthYear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return monYear;
    }

    protected int checkSalaryProcess()
    {
        int count = 0;
        try
        {
            count = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + MonthYear() + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue)));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_lock_salary.checkSalaryProcess-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return count;
    }


    protected void ddlCollegeFilter_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlCollegeFilter.SelectedValue != "0")
        {
            BindListViewList();

        }
        else if (ddlCollegeFilter.SelectedValue == "0")
        {
            BindListViewList();
        }
        else
        {
            lvLockUnlock.Visible = false;
            btnLockSalary.Visible = false;
            btnSave.Visible = false;
        }
    }
    //protected void txtYesNo1_TextChanged(object sender, EventArgs e)
    //{
    //    //string Status = "";
    //    //ListView lvitem = (ListView)lvLockUnlock;
    //    //TextBox TXTSTATUS = lvitem.FindControl("txtYesNo1") as TextBox;
    //    //Status = TXTSTATUS.Text.ToString();
    //    //foreach (ListViewDataItem lvitems in lvitem.Items)
    //    //{
    //    //    TextBox txtstatus = lvitems.FindControl("txtYesNo") as TextBox;
    //    //    txtstatus.Text = Status;
    //    //}

    //}s
    protected void txtEditFieldDT_TextChanged(object sender, EventArgs e)
    {
        try
        {

            foreach (ListViewDataItem lvitem in lvLockUnlock.Items)
            {

                TextBox txtEditFieldDT = lvitem.FindControl("txtEditFieldDT") as TextBox;

                txtEditFieldDT.Text = txtEditFieldDT.Text;

            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}