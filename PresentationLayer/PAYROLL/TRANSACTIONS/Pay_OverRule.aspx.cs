//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_OverRule.ASPX                                                    
// CREATION DATE : 05-Nov-2009                                                        
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_Transactions_Pay_OverRule : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    Pay_SupplimentaryBill_Controller objSupBillCon = new Pay_SupplimentaryBill_Controller();

    PayOverRuleController objOverRuleCon = new PayOverRuleController();

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillEmployee();
                this.BindListViewEarningHeads();
                this.BindListViewDeductionHeads();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_OverRule.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_OverRule.aspx");
        }
    }

    private void BindListViewEarningHeads()
    {
        try
        {
            DataSet ds = objSupBillCon.GetAllEarningHeads();
            lvEarningHeads.DataSource = ds;
            lvEarningHeads.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.BindListViewEarningHeads()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewDeductionHeads()
    {
        try
        {
            DataSet ds = objSupBillCon.GetAllDeductionHeads();
            lvDeductionHeads.DataSource = ds;
            lvDeductionHeads.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.BindListViewDeductionHeads()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEmployee()
    {
        try
        {
            objCommon.FillDropDownList(ddlEmpName, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+ ' ' + '['+ CONVERT(NVARCHAR(20),EM.PFILENO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO IN(" + Session["college_nos"] + ")", "EM.FNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.clear();
        this.GetDepartmentDesignation(Convert.ToInt32(ddlEmpName.SelectedValue));
        this.ShowDetails(Convert.ToInt32(ddlEmpName.SelectedValue));
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdate = "01/" + txtFromDate.Text;
            DateTime todate;
            todate = objOverRuleCon.GetMonthLastDate(Convert.ToDateTime("01/" + txtToDate.Text));

            string PayHeadValues = this.GetPayHeadValues();
            char status;
            if (chkActive.Checked)
                status = 'A';
            else
                status = 'N';


            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objOverRuleCon.AddOverRule(Convert.ToInt32(ddlEmpName.SelectedValue), Convert.ToDateTime(fromdate), todate, status, txtRemark.Text, Session["colcode"].ToString(), PayHeadValues);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        lblerror.Text = null;
                        //ShowMessage("Record Saved Successfully");
                        //lblmsg.Text = "Record Saved Successfully";
                        objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);

                    }
                    //clear();
                    // Updated on 11-01-2023
                    ClearControls();
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objOverRuleCon.UpdateOverRule(Convert.ToInt32(ViewState["idNo"].ToString()), Convert.ToDateTime(fromdate), todate, status, txtRemark.Text, Session["colcode"].ToString(), PayHeadValues);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        lblerror.Text = null;
                        //ShowMessage("Record Updated Successfully");
                        objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
                        //lblmsg.Text = "Record Updated Successfully";

                    }
                   // clear();
                    // Updated on 11-01-2023
                    ClearControls();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        this.ShowDetails(Convert.ToInt32(ddlEmpName.SelectedValue));

    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowDetails(int idNo)
    {
        try
        {
            //Duplicate checking
            int count = Convert.ToInt32(objCommon.LookUp("payroll_overrule", "count(*)", "status='A'and idno=" + Convert.ToInt32(ddlEmpName.SelectedValue)));

            if (count > 0)
            {
                //lblerror.Text = "Record already exists,if you want to modify the record then edit the record and click on submit button";
                objCommon.DisplayMessage(UpdatePanel1, "Record already exists,if you want to modify the record then edit the record and click on submit button", this);
                ViewState["action"] = "edit";
                DataSet dsOverRule = objOverRuleCon.GetSingleOverRule(idNo);
                DataSet dsEarHeads = objSupBillCon.GetAllEarningHeads();
                DataSet dsDudHeads = objSupBillCon.GetAllDeductionHeads();
                ViewState["idNo"] = idNo.ToString();

                if (dsOverRule != null)
                {

                    if (dsOverRule.Tables[0].Rows[0]["STATUS"].Equals("A"))
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;


                    txtRemark.Text = dsOverRule.Tables[0].Rows[0]["REMARK"].ToString();
                    cetxtFromDate.SelectedDate = Convert.ToDateTime(dsOverRule.Tables[0].Rows[0]["FROMDT"].ToString());
                    cetxtToDate.SelectedDate = Convert.ToDateTime(dsOverRule.Tables[0].Rows[0]["TODT"].ToString());

                    if (dsEarHeads.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsEarHeads.Tables[0].Rows.Count - 1; i++)
                        {
                            TextBox txEHDays = lvEarningHeads.Items[i].FindControl("txEHDays") as TextBox;
                            CheckBox chkEh = lvEarningHeads.Items[i].FindControl("chkEarningHead") as CheckBox;
                            DropDownList ddlCalEh = lvEarningHeads.Items[i].FindControl("ddlEarnings") as DropDownList;
                            TextBox txEHAmount = lvEarningHeads.Items[i].FindControl("txtEAmount") as TextBox;
                            txEHDays.Text = dsOverRule.Tables[0].Rows[0]["" + "D" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                            ddlCalEh.SelectedValue = dsOverRule.Tables[0].Rows[0]["" + "C" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                            chkEh.Checked = Convert.ToBoolean(dsOverRule.Tables[0].Rows[0]["" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString());
                            txEHAmount.Text = dsOverRule.Tables[0].Rows[0]["" + "A" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();


                            if (Convert.ToBoolean(dsOverRule.Tables[0].Rows[0]["" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString()))
                            {
                                ddlCalEh.Enabled = true;
                                if (ddlCalEh.SelectedValue == "6")
                                {
                                    txEHDays.Enabled = true;
                                }
                                if (ddlCalEh.SelectedValue == "7")
                                {
                                    txEHAmount.Enabled = true;
                                }
                            }
                        }
                    }

                    if (dsDudHeads.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsDudHeads.Tables[0].Rows.Count - 1; i++)
                        {
                            TextBox txDHDays = lvDeductionHeads.Items[i].FindControl("txDHDays") as TextBox;
                            CheckBox chkDh = lvDeductionHeads.Items[i].FindControl("chkDeductionHead") as CheckBox;
                            DropDownList ddlCalDh = lvDeductionHeads.Items[i].FindControl("ddlDeduction") as DropDownList;
                            TextBox txDHAmount = lvDeductionHeads.Items[i].FindControl("txtDAmount") as TextBox;
                            txDHDays.Text = dsOverRule.Tables[0].Rows[0]["" + "D" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                            ddlCalDh.SelectedValue = dsOverRule.Tables[0].Rows[0]["" + "C" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                            chkDh.Checked = Convert.ToBoolean(dsOverRule.Tables[0].Rows[0]["" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString());
                            
                            txDHAmount.Text = dsOverRule.Tables[0].Rows[0]["" + "A" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();

                            if (Convert.ToBoolean(dsOverRule.Tables[0].Rows[0]["" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString()))
                            {
                                ddlCalDh.Enabled = true;
                                if (ddlCalDh.SelectedValue == "6")
                                {
                                    txDHDays.Enabled = true;
                                }
                                if (ddlCalDh.SelectedValue == "7")
                                {
                                    txDHAmount.Enabled = true;
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                ViewState["action"] = "add";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ClearControls()
    {
        ddlEmpName.SelectedIndex = 0;
        lblDesignation.Text = string.Empty;
        lblAppointMent.Text = string.Empty;
        cetxtFromDate.SelectedDate = null;
        cetxtToDate.SelectedDate = null;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        this.BindListViewEarningHeads();
        this.BindListViewDeductionHeads();
    }
    private void GetDepartmentDesignation(int idNo)
    {
        if (idNo != 0)
        {
            int appointNo, desigNo;
            appointNo = GetCodesFromPaymas("APPOINTNO", idNo);
            desigNo = GetCodesFromEmpmas("subdesigno", idNo);
            lblAppointMent.Text = objCommon.LookUp("PAYROLL_APPOINT", "APPOINT", "APPOINTNO=" + appointNo);
            lblDesignation.Text = objCommon.LookUp("payroll_subdesig", "SUBDESIG", "SUBDESIGNO=" + desigNo);
        }
        else
        {
            lblAppointMent.Text = string.Empty;
            lblDesignation.Text = string.Empty;
        }
    }

    private int GetCodesFromEmpmas(string columName, int idNo)
    {
        return Convert.ToInt32(objCommon.LookUp("payroll_empmas", "" + columName + "", "idno=" + idNo));
    }

    private int GetCodesFromPaymas(string columName, int idNo)
    {
        return Convert.ToInt32(objCommon.LookUp("payroll_paymas", "" + columName + "", "idno=" + idNo));
    }

    private bool GetBoolenValuesFromPaymas(string columName, int idNo)
    {
        return Convert.ToBoolean(objCommon.LookUp("payroll_paymas", "" + columName + "", "idno=" + idNo));
    }

    private decimal GetDecimalValuesFromPaymas(string columName, int idNo)
    {
        return Convert.ToDecimal(objCommon.LookUp("payroll_paymas", "" + columName + "", "idno=" + idNo));
    }

    private string GetStringValueFromEmpMas(string columName, int idNo)
    {
        return objCommon.LookUp("payroll_empmas", "" + columName + "", "idno=" + idNo);
    }

    private string GetStringValueFromPaymas(string columName, int idNo)
    {
        return objCommon.LookUp("payroll_paymas", "" + columName + "", "idno=" + idNo);
    }

    private string GetPayHeadValues()
    {

        string PayHeadValues;
        string earningHeads;
        string deductionHeads;
        string ehCalculation;
        string dhCalculation;
        string ehdays;
        string dhdays;
        PayHeadValues = string.Empty;
        earningHeads = string.Empty;
        deductionHeads = string.Empty;
        ehCalculation = string.Empty;
        dhCalculation = string.Empty;
        ehdays = string.Empty;
        dhdays = string.Empty;

        string ehAmount = string.Empty;
        string dhAmount = string.Empty;

        try
        {
            foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
            {
                TextBox txEHDays = lvitem.FindControl("txEHDays") as TextBox;
                CheckBox chkEarningHead = lvitem.FindControl("chkEarningHead") as CheckBox;
                DropDownList ddlEarnings = lvitem.FindControl("ddlEarnings") as DropDownList;
                TextBox txtEHAmount = lvitem.FindControl("txtEAmount") as TextBox;
                if (chkEarningHead.Checked)
                {
                    if (ViewState["action"].ToString() == "add")
                    {
                        earningHeads = earningHeads + "1" + ",";
                        ehCalculation = ehCalculation + ddlEarnings.SelectedValue + ",";
                        ehdays = ehdays + txEHDays.Text + ",";
                        ehAmount = ehAmount + txtEHAmount.Text + ",";
                    }
                    else
                    {
                        earningHeads = earningHeads + chkEarningHead.ToolTip + "=1" + ",";
                        ehCalculation = ehCalculation + "C" + chkEarningHead.ToolTip + "=" + ddlEarnings.SelectedValue + ",";
                        ehdays = ehdays + "D" + chkEarningHead.ToolTip + "=" + txEHDays.Text + ",";
                        ehAmount = ehAmount + "A" + chkEarningHead.ToolTip + "=" + txtEHAmount.Text + ",";

                    }
                }
                else
                {
                    if (ViewState["action"].ToString() == "add")
                    {
                        earningHeads = earningHeads + "0" + ",";
                        ehCalculation = ehCalculation + "0" + ",";
                        ehdays = ehdays + "0" + ",";
                        ehAmount = ehAmount + "0" + ",";
                    }
                    else
                    {
                        earningHeads = earningHeads + chkEarningHead.ToolTip + "=0" + ",";
                        ehCalculation = ehCalculation + "C" + chkEarningHead.ToolTip + "=0" + ",";
                        ehdays = ehdays + "D" + chkEarningHead.ToolTip + "=0" + ",";
                        ehAmount = ehAmount + "A" + chkEarningHead.ToolTip + "=0" + ",";
                    }
                }

            }

            foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
            {
                TextBox txDHDays = lvitem.FindControl("txDHDays") as TextBox;
                CheckBox chkDeductionHead = lvitem.FindControl("chkDeductionHead") as CheckBox;
                DropDownList ddlDeduction = lvitem.FindControl("ddlDeduction") as DropDownList;
                TextBox txtdAmount = lvitem.FindControl("txtDAmount") as TextBox;

                if (chkDeductionHead.Checked)
                {
                    if (ViewState["action"].ToString() == "add")
                    {
                        deductionHeads = deductionHeads + "1" + ",";
                        dhCalculation = dhCalculation + ddlDeduction.SelectedValue + ",";
                        dhdays = dhdays + txDHDays.Text + ",";
                        dhAmount = dhAmount + txtdAmount.Text + ",";
                    }
                    else
                    {
                        deductionHeads = deductionHeads + chkDeductionHead.ToolTip + "=1" + ",";
                        dhCalculation = dhCalculation + "C" + chkDeductionHead.ToolTip + "=" + ddlDeduction.SelectedValue + ",";
                        dhdays = dhdays + "D" + chkDeductionHead.ToolTip + "=" + txDHDays.Text + ",";
                        dhAmount = dhAmount + "A" + chkDeductionHead.ToolTip + "=" + txtdAmount.Text + ",";

                    }
                }
                else
                {
                    if (ViewState["action"].ToString() == "add")
                    {
                        deductionHeads = deductionHeads + "0" + ",";
                        dhCalculation = dhCalculation + "0" + ",";
                        dhdays = dhdays + "0" + ",";
                        dhAmount = dhAmount + "0" + ",";
                    }
                    else
                    {
                        deductionHeads = deductionHeads + chkDeductionHead.ToolTip + "=0" + ",";
                        dhCalculation = dhCalculation + "C" + chkDeductionHead.ToolTip + "=0" + ",";
                        dhdays = dhdays + "D" + chkDeductionHead.ToolTip + "=0" + ",";
                        dhAmount = dhAmount + "A" + chkDeductionHead.ToolTip + "=0" + ",";
                    }
                }
            }

            PayHeadValues = earningHeads + deductionHeads + ehCalculation + dhCalculation + ehdays + dhdays + ehAmount + dhAmount;
            PayHeadValues = PayHeadValues.Substring(0, PayHeadValues.Length - 1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.GetPayHeadValues-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return PayHeadValues;
    }

    private string GetEarningHeadsCalculated()
    {
        string earningHeadsCalculated;
        earningHeadsCalculated = string.Empty;

        foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
        {
            DropDownList ddlEarnings = lvitem.FindControl("ddlEarnings") as DropDownList;
            TextBox txEHDays = lvitem.FindControl("txEHDays") as TextBox;

            //if (ViewState["action"].ToString() == "add")
            //    earningHeadsCalculated = earningHeadsCalculated +Convert.ToInt32(ddlEarnings.SelectedValue) + ",";
            // else
            earningHeadsCalculated = earningHeadsCalculated + "C" + txEHDays.ToolTip + "=" + Convert.ToInt32(ddlEarnings.SelectedValue) + ",";
        }
        earningHeadsCalculated = earningHeadsCalculated.Substring(0, earningHeadsCalculated.Length - 1);

        return earningHeadsCalculated;
    }

    private string GetEarningHeadsDays()
    {
        string earningHeadsDays;
        earningHeadsDays = string.Empty;

        foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
        {
            TextBox txEHDays = lvitem.FindControl("txEHDays") as TextBox;
            if (ViewState["action"].ToString() == "add")
                earningHeadsDays = earningHeadsDays + txEHDays.Text + ",";
            else
                earningHeadsDays = earningHeadsDays + "D" + txEHDays.ToolTip + "=" + txEHDays.Text + ",";
        }
        earningHeadsDays = earningHeadsDays.Substring(0, earningHeadsDays.Length - 1);

        return earningHeadsDays;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            message = message.Replace("'", "\'");
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void AddNew()
    {
        pnlSupplimentaryBillDetails.Visible = true;
        this.BindListViewEarningHeads();
        this.BindListViewDeductionHeads();
        this.FillEmployee();
        //to display employee count in footer
        hidEarningRecordsCount.Value = Convert.ToString(lvEarningHeads.Items.Count);
        hidTotalDeductionRecordsCount.Value = Convert.ToString(lvDeductionHeads.Items.Count);
    }

    private void clear()
    {

        try
        {
            cetxtFromDate.SelectedDate = null;
            cetxtToDate.SelectedDate = null;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            // lblAppointMent.Text = string.Empty;
            // lblDesignation.Text = string.Empty;
            lblerror.Text = string.Empty;
            lblmsg.Text = string.Empty;
            foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
            {
                TextBox txEHDays = lvitem.FindControl("txEHDays") as TextBox;
                CheckBox chkEarningHead = lvitem.FindControl("chkEarningHead") as CheckBox;
                DropDownList ddlEarnings = lvitem.FindControl("ddlEarnings") as DropDownList;
                TextBox txtEAmount = lvitem.FindControl("txtEAmount") as TextBox;
                txEHDays.Text = "0";
                txtEAmount.Text = "0";
                chkEarningHead.Checked = true;
                ddlEarnings.SelectedIndex = 0;
                txEHDays.Enabled = false;
                ddlEarnings.Enabled = true;
                txtEAmount.Enabled = false;

            }

            foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
            {
                TextBox txDHDays = lvitem.FindControl("txDHDays") as TextBox;
                CheckBox chkDeductionHead = lvitem.FindControl("chkDeductionHead") as CheckBox;
                DropDownList ddlDeduction = lvitem.FindControl("ddlDeduction") as DropDownList;
                TextBox txtDAmount = lvitem.FindControl("txtDAmount") as TextBox;
                txDHDays.Text = "0";
                txtDAmount.Text = "0";
                chkDeductionHead.Checked = true;
                ddlDeduction.SelectedIndex = 0;
                txDHDays.Enabled = false;
                ddlDeduction.Enabled = true;
                txtDAmount.Enabled = false;
            }

            //ddlEmpName.SelectedIndex = 0;
            //lblDesignation.Text = string.Empty;
            //lblAppointMent.Text = string.Empty;
            //cetxtFromDate.SelectedDate = null;
            //cetxtToDate.SelectedDate = null;
            //txtFromDate.Text = string.Empty;
            //txtToDate.Text = string.Empty;
            //txtRemark.Text = string.Empty;
            //this.BindListViewEarningHeads();
            //this.BindListViewDeductionHeads();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.clear()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }
}
