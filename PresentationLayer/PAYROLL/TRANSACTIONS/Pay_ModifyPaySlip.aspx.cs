//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL
// PAGE NAME     : Pay_ModifyPaySlip.ASPX                                                    
// CREATION DATE : 27-JAN-2016                                                        
// CREATED BY    : ZUBAIR AHMAD                                                         
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

public partial class PAYROLL_TRANSACTIONS_Pay_ModifyPaySlip : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Pay_ModifyPaySlipController ObjMPSlip = new Pay_ModifyPaySlipController();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                pnlMonthlyChanges.Visible = false;
                cetxtStartDate.SelectedDate = System.DateTime.Today;               
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ModifyPaySlip.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ModifyPaySlip.aspx");
        }
    }


    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {           
            string PayHeadValues = this.GetPayHeadValues();
            CustomStatus cs = (CustomStatus)ObjMPSlip.UpdatePaySlip(Convert.ToInt32(ddlEmployeeNo.SelectedValue), txtMonthYear.Text, PayHeadValues);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                lblerror.Text = null;              
                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);               
            }     
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private string GetPayHeadValues()
    {

        string PayHeadValues = string.Empty;
        string earningHeads = string.Empty;
        string deductionHeads = string.Empty;          
       
        string ehAmount = string.Empty;
        string dhAmount = string.Empty;
        try
        {
            TextBox txtBasicPay = lvEarningHeads.FindControl("txtBasicPay") as TextBox;
            TextBox txtGrade = lvEarningHeads.FindControl("txtGrade") as TextBox;
            foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
            {                
                TextBox txtEHAmount = lvitem.FindControl("txtEAmount") as TextBox;
                Label lblEarHead = lvitem.FindControl("lblEarHead") as Label;               
                    
                    {
                        ehAmount = ehAmount + lblEarHead.ToolTip + "=" + txtEHAmount.Text + ",";
                    }
            }

            foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
            {
                TextBox txtdAmount = lvitem.FindControl("txtDAmount") as TextBox;
                Label lblDedHead = lvitem.FindControl("lblDedHead") as Label;
                   
                    {
                        dhAmount = dhAmount + lblDedHead.ToolTip + "=" + txtdAmount.Text + ",";
                    }
            }
            decimal Pay = Convert.ToDecimal(txtBasicPay.Text.Trim()) + Convert.ToDecimal(txtGrade.Text.Trim());
            PayHeadValues = ehAmount + dhAmount + txtBasicPay.ToolTip + "=" + txtBasicPay.Text.Trim() + "," + txtGrade.ToolTip + "=" + txtGrade.Text.Trim() + ",PAY=" + Pay + ",GS=" + txtGrossPay.Text.Trim() + ",TOT_DED=" + txtTotalDeduction.Text.Trim() + ",NET_PAY="+txtNetPay.Text.Trim();
            //PayHeadValues = PayHeadValues.Substring(0, PayHeadValues.Length - 1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.GetPayHeadValues-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return PayHeadValues;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ddlStaff.SelectedIndex = 0;
        ddlEmployeeNo.SelectedIndex = 0;
        txtGrossPay.Text = string.Empty;
        txtTotalDeduction.Text = string.Empty;
        txtNetPay.Text = string.Empty;
        cetxtStartDate.SelectedDate = System.DateTime.Today;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlCollege.SelectedIndex = 0;

        if (lvEarningHeads.Items.Count > 0)
        {
            foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
            {
                TextBox txtEAmount = lvitem.FindControl("txtEAmount") as TextBox;
                TextBox txtBasicPay = lvitem.FindControl("txtBasicPay") as TextBox;
                TextBox txtGrade = lvitem.FindControl("txtGrade") as TextBox;
                txtEAmount.Text = string.Empty;
            }
        }

        if (lvDeductionHeads.Items.Count > 0)
        {
            foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
            {
                TextBox txtDAmount = lvitem.FindControl("txtDAmount") as TextBox;
                txtDAmount.Text = string.Empty;
            }
        }
          
    }

    protected void FillStaff()
    {
        try
        {
           objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
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
            if (checkSalaryProcess() == 0)
            {               
                objCommon.DisplayMessage(UpdatePanel1, "Salary is not processed for " + " " + MonthYear(), this);
                pnlMonthlyChanges.Visible = false;
            }
            else
            {
                if (checkSalaryLocked() == "Y")
                {
                    this.BindListViewEarningHeads();
                    this.BindListViewDeductionHeads();
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
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListViewEarningHeads()
    {
        try
        {
            DataSet ds = ObjMPSlip.GetAllPayHeads(MonthYear(), Convert.ToInt32(ddlEmployeeNo.SelectedValue), 'I');
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblEmpName.Text = ds.Tables[0].Rows[0]["NAME"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["NAME"].ToString();
                lblDOI.Text = ds.Tables[0].Rows[0]["DOI"].ToString() == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["DOI"]).ToString("MMM") + "/" + Convert.ToDateTime(ds.Tables[0].Rows[0]["DOI"]).Year.ToString();
                lblScale.Text = ds.Tables[0].Rows[0]["SCALE"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["SCALE"].ToString();
                lblAccNo.Text = ds.Tables[0].Rows[0]["BANKACC_NO"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["BANKACC_NO"].ToString();
                lblDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                lblBasic.Text = ds.Tables[0].Rows[0]["BASIC"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["BASIC"].ToString();
                txtGrossPay.Text = ds.Tables[0].Rows[0]["GS"].ToString() == "" ? "" : Convert.ToInt32(ds.Tables[0].Rows[0]["GS"]).ToString();
                txtTotalDeduction.Text = ds.Tables[0].Rows[0]["TOT_DED"].ToString() == "" ? "" : Convert.ToInt32(ds.Tables[0].Rows[0]["TOT_DED"]).ToString();
                txtNetPay.Text = ds.Tables[0].Rows[0]["NET_PAY"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["NET_PAY"].ToString();

                lvEarningHeads.DataSource = ds;
                lvEarningHeads.DataBind();
                hidEarningRecordsCount.Value = Convert.ToString(lvEarningHeads.Items.Count);

                TextBox txtBasicPay = lvEarningHeads.FindControl("txtBasicPay") as TextBox;
                TextBox txtGrade = lvEarningHeads.FindControl("txtGrade") as TextBox;
                txtBasicPay.Text = ds.Tables[0].Rows[0]["BASIC"].ToString() == "" ? "" : Convert.ToInt32(ds.Tables[0].Rows[0]["BASIC"]).ToString();
                txtGrade.Text = ds.Tables[0].Rows[0]["GRADEPAY"].ToString() == "" ? "" : Convert.ToInt32(ds.Tables[0].Rows[0]["GRADEPAY"]).ToString();
                pnlMonthlyChanges.Visible = true;

                lblEmpName.ForeColor = System.Drawing.Color.White;
                lblDOI.ForeColor = System.Drawing.Color.White;
                lblScale.ForeColor = System.Drawing.Color.White;
                lblAccNo.ForeColor = System.Drawing.Color.White;
                lblDesignation.ForeColor = System.Drawing.Color.White;
                lblBasic.ForeColor = System.Drawing.Color.White;
            }       
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_ModifyPaySlip.BindListViewEarningHeads()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewDeductionHeads()
    {
        try
        {
            DataSet ds = ObjMPSlip.GetAllPayHeads(MonthYear(), Convert.ToInt32(ddlEmployeeNo.SelectedValue), 'D');
            lvDeductionHeads.DataSource = ds;
            lvDeductionHeads.DataBind();
            hidDeductionRecordsCount.Value = Convert.ToString(lvDeductionHeads.Items.Count);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_ModifyPaySlip.BindListViewDeductionHeads()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected int checkSalaryProcess()
    {
        int count = 0;
        try
        {
            count = Convert.ToInt32(objCommon.LookUp("payroll_salfile", "count(*)", "monyear='" + MonthYear() + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue)));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.checkSalaryProcess-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return count;
    }

    protected string checkSalaryLocked()
    {
        string result = string.Empty;
        try
        {
            Boolean sallock;
            string status;
            sallock = Convert.ToBoolean(objCommon.LookUp("payroll_salfile", "sallock", "monyear='" + MonthYear() + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue)));
            status = objCommon.LookUp("payroll_salfile", "status", "monyear='" + MonthYear() + "' and staffno=" + Convert.ToInt32(ddlStaff.SelectedValue));

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
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.checkSalaryLocked-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return result;
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
                objUCommon.ShowError(Page, "Pay_ModifyPaySlip.MonthYear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return monYear;
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME +' '+ '['+ convert(nvarchar(15),PFILENO) + ']'", "COLLEGE_NO=" + ddlCollege.SelectedValue + " AND STAFFNO=" + ddlStaff.SelectedValue, "");
        }
        catch (Exception ex)
        {
        
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  //FILL EMPLOYEE
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME +' '+ '['+ convert(nvarchar(15),PFILENO) + ']'", "COLLEGE_NO=" + ddlCollege.SelectedValue, "");
        }
        catch (Exception ex)
        {

        }
    }
}
