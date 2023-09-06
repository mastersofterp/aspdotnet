//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_SupplimentaryBill.ASPX                                                    
// CREATION DATE : 02-Nov-2009                                                        
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

public partial class PayRoll_Transactions_Pay_SupplimentaryBill : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Pay_SupplimentaryBill_Controller objSupBillCon = new Pay_SupplimentaryBill_Controller();
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
            butSubmit.Visible = false;
            butCancel.Visible = false;
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
                divOrdDetails.Visible = true;
                pnlSupplimentaryBillDetails.Visible = false;
                this.BindListViewSuplimentaryBillOrderDetails();
                
                //pnlSupplimentaryBillDetails.Visible = true;
                //this.BindListViewEarningHeads();
                //this.BindListViewDeductionHeads();
                //this.FillEmployee();
                //this.FillSupllimentaryHead();
                //ViewState["action"] = "add";
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_SupplimentaryBill.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SupplimentaryBill.aspx");
        }
    }

    private void BindListViewSuplimentaryBillOrderDetails()
    {
        try
        {
            DataSet ds = objSupBillCon.GetAllSuplimentaryBillOrderDetails();
            lvOrderDetails.DataSource = ds;
            lvOrderDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.BindListViewSuplimentaryBillOrderDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewSuplimentaryBillByOrderNo(string ordNo)
    {
        try
        {
            DataSet ds = objSupBillCon.GetSupplimentaryBillByOrderNo(ordNo);
            lvSuplBillDtl.Visible = true;
            lvSuplBillDtl.DataSource = ds;
            lvSuplBillDtl.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.BindListViewSuplimentaryBillOrderDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            objCommon.FillDropDownList(ddlEmpName, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+ ' ' + '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO IN(" + Session["college_nos"] + ")", "EM.IDNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillSupllimentaryHead()
    {
        try
        {
            objCommon.FillDropDownList(ddlSupliBillHead, "PAYROLL_SUPPLIMENTARY_HEAD", "SUPLHNO", "SUPLHEAD", "", "SUPLHNO");

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
        this.GetDepartmentDesignation(Convert.ToInt32(ddlEmpName.SelectedValue));
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            Pay_Supplimentary_Bill objSupBill = new Pay_Supplimentary_Bill();
            if (chkAddInIncomeTax.Checked)
                objSupBill.ADDIT = true;
            else
                objSupBill.ADDIT = false;


            if (chkAddInOthIncome.Checked)
                objSupBill.ADDOTHINCOME = true;
            else
                objSupBill.ADDOTHINCOME = false;

            objSupBill.APPOINTNO = GetCodesFromPaymas("APPOINTNO", Convert.ToInt32(ddlEmpName.SelectedValue));
            objSupBill.GS = Convert.ToDecimal(txtGrossSalary.Text);
            objSupBill.HP = GetBoolenValuesFromPaymas("HP", Convert.ToInt32(ddlEmpName.SelectedValue));
            objSupBill.GRADEPAY = Convert.ToDecimal(txtGradePay.Text);
            objSupBill.EXPAMT = 0;
            objSupBill.DPAMT = Convert.ToDecimal(txtDPay.Text);
            objSupBill.NET_PAY = Convert.ToDecimal(txtNetPay.Text);
            objSupBill.MONYEAR = Convert.ToDateTime(txtSupliBillDate.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtSupliBillDate.Text).ToString("yyyy").ToUpper();
            objSupBill.ORDNO = txtOrderNo.Text;
            objSupBill.PAY = 0;
            objSupBill.PAYDAYS = 0;
            objSupBill.PSTATUS = Convert.ToChar(objCommon.LookUp("payroll_paymas", "PSTATUS", "idno=" + Convert.ToInt32(ddlEmpName.SelectedValue)));
            objSupBill.REMARK = txtRemark.Text;
            objSupBill.SBDATE = Convert.ToDateTime(txtSupliBillDate.Text);
            objSupBill.SCALENO = GetCodesFromPaymas("SCALENO", Convert.ToInt32(ddlEmpName.SelectedValue));
            objSupBill.STAFFNO = GetCodesFromEmpmas("STAFFNO", Convert.ToInt32(ddlEmpName.SelectedValue));
            objSupBill.SUPLHEAD = Convert.ToString(ddlSupliBillHead.SelectedItem);
            objSupBill.SUPLHNO = Convert.ToInt32(ddlSupliBillHead.SelectedValue);
            objSupBill.SUPLSTATUS = 'S';
            objSupBill.TITLE = "N";
            objSupBill.TOT_DED = Convert.ToDecimal(txtTotalDeductions.Text);
            objSupBill.IDNO = Convert.ToInt32(ddlEmpName.SelectedValue);
            objSupBill.GS1 = Convert.ToDecimal(txtGrossSalary.Text);
            objSupBill.SUPLIPAY = Convert.ToDecimal(txtSupliPay.Text);
            objSupBill.COLLEGE_CODE = Session["colcode"].ToString();
           

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Duplicate check on orderno and empno
                    int count;
                    count = Convert.ToInt32(objCommon.LookUp("PAYROLL_SUPPLIMENTARY_BILL", "count(*)", "ordno='" + txtOrderNo.Text + "' and idno=" + Convert.ToInt32(ddlEmpName.SelectedValue)));
                    if (count <= 0)
                    {
                        CustomStatus cs = (CustomStatus)objSupBillCon.AddSupplimentaryBill(objSupBill, this.GetEarningHeadsAmount(), this.GetDeductionHeadsAmount());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            lblerror.Text = null;
                            this.BindListViewSuplimentaryBillByOrderNo(txtOrderNo.Text);
                            //ShowMessage("Record Saved Successfully");
                            objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                            //lblmsg.Text = "Record Saved Successfully";
                            clearAll();
                        }

                    }
                    else
                    {
                        lblmsg.Text = null;
                        objCommon.DisplayMessage(UpdatePanel1, "Record Already Exists", this);
                        //lblerror.Text = "Record Already Exists";

                    }

                }
                else
                {
                    objSupBill.SUPLTRXID = Convert.ToInt32(ViewState["suplTrxId"].ToString());
                    CustomStatus cs = (CustomStatus)objSupBillCon.UpdateSupplimentaryBill(objSupBill, this.GetEarningHeadsAmount(), this.GetDeductionHeadsAmount());
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        lblerror.Text = null;
                        this.BindListViewSuplimentaryBillByOrderNo(txtOrderNo.Text);
                        //ShowMessage("Record Updated Successfully");
                        //lblmsg.Text = "Record Updated Successfully";
                        objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
                        clearAll();
                    }
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

    }

    private void clearAll()
    {
      
        txtDPay.Text = "0";
        txtGradePay.Text = "0";
        txtGrossSalary.Text = "0";
        txtNetPay.Text = "0";
        txtOrderNo.Text = string.Empty;
        txtPersentage.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtSupliBillDate.Text = string.Empty;

        txtSupliPay.Text = "0.00";
        
        txtTotalDeductions.Text = "0";
        ddlEmpName.SelectedIndex = 0;
        ddlSupliBillHead.SelectedIndex = 0;
        lblDesignation.Text = string.Empty;
        lblDepartMent.Text = string.Empty;

        foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
        {
            TextBox txDeductionAmt = lvitem.FindControl("txDeductionAmt") as TextBox;
            txDeductionAmt.Text = string.Empty;
        }

        foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
        {
            TextBox txIncomeAmt = lvitem.FindControl("txEarningsAmt") as TextBox;
            txIncomeAmt.Text = string.Empty;
        }

        this.AddNew();
        this.BindListViewSuplimentaryBillByOrderNo("0");
        ViewState["action"] = "add";
        btnAdd.Visible = false;
        butSubmit.Visible = true;
        butCancel.Visible = true;

        BindListViewDeductionHeads();
        BindListViewEarningHeads();
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        butSubmit.Visible = false;
        butCancel.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.AddNew();
        this.BindListViewSuplimentaryBillByOrderNo("0");
        ViewState["action"] = "add";
        btnAdd.Visible = false;
        butSubmit.Visible = true;
        butCancel.Visible = true;
    }

    protected void btnEditOrderDetails_Click(object sender, ImageClickEventArgs e)
    {
        this.AddNew();
        ViewState["action"] = "add";
        ImageButton btnEditOrderDetails = sender as ImageButton;
        this.BindListViewSuplimentaryBillByOrderNo(btnEditOrderDetails.CommandArgument.ToString());
    }

    protected void btnEditSuplBill_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmsg.Text = null;
            ImageButton btnEditSuplBill = sender as ImageButton;
            int suplTrxId = int.Parse(btnEditSuplBill.CommandArgument);
            ShowDetails(suplTrxId);
            ViewState["action"] = "edit";
            butSubmit.Visible = true;
            butCancel.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int suplTrxId)
    {
        try
        {
            DataSet dsSuplDtl = objSupBillCon.GetSingleSupplimentaryBill(suplTrxId);
            DataSet dsEarHeads = objSupBillCon.GetAllEarningHeads();
            DataSet dsDudHeads = objSupBillCon.GetAllDeductionHeads();
            ViewState["suplTrxId"] = suplTrxId.ToString();

            if (dsSuplDtl != null)
            {
                txtSupliBillDate.Text = dsSuplDtl.Tables[0].Rows[0]["SBDATE"].ToString();
                txtDPay.Text = dsSuplDtl.Tables[0].Rows[0]["DPAMT"].ToString();
                txtGradePay.Text = dsSuplDtl.Tables[0].Rows[0]["GRADEPAY"].ToString();
                txtGrossSalary.Text = dsSuplDtl.Tables[0].Rows[0]["GS"].ToString();
                txtNetPay.Text = dsSuplDtl.Tables[0].Rows[0]["NET_PAY"].ToString();
                txtTotalDeductions.Text = dsSuplDtl.Tables[0].Rows[0]["TOT_DED"].ToString();
                txtSupliPay.Text = dsSuplDtl.Tables[0].Rows[0]["SUPLIPAY"].ToString();
                txtOrderNo.Text = dsSuplDtl.Tables[0].Rows[0]["ORDNO"].ToString();
                ddlEmpName.SelectedValue = dsSuplDtl.Tables[0].Rows[0]["IDNO"].ToString();
                ddlSupliBillHead.SelectedValue = dsSuplDtl.Tables[0].Rows[0]["SUPLHNO"].ToString();
                chkAddInIncomeTax.Checked = Convert.ToBoolean(dsSuplDtl.Tables[0].Rows[0]["ADDIT"].ToString());
                chkAddInOthIncome.Checked = Convert.ToBoolean(dsSuplDtl.Tables[0].Rows[0]["ISOTHERINCOME"].ToString());
                txtRemark.Text = dsSuplDtl.Tables[0].Rows[0]["REMARK"].ToString();
                this.GetDepartmentDesignation(Convert.ToInt32(dsSuplDtl.Tables[0].Rows[0]["IDNO"].ToString()));
                if (dsEarHeads.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dsEarHeads.Tables[0].Rows.Count - 1; i++)
                    {
                        TextBox txtEar = lvEarningHeads.Items[i].FindControl("txEarningsAmt") as TextBox;
                        txtEar.Text = dsSuplDtl.Tables[0].Rows[0]["" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                    }
                }
                if (dsDudHeads.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dsDudHeads.Tables[0].Rows.Count - 1; i++)
                    {
                        TextBox txtDud = lvDeductionHeads.Items[i].FindControl("txDeductionAmt") as TextBox;
                        txtDud.Text = dsSuplDtl.Tables[0].Rows[0]["" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                    }
                }
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

    private void GetDepartmentDesignation(int idNo)
    {
        if (idNo != 0)
        {
            //butSubmit.Visible = true;
            //butCancel.Visible = true;
            int deptNo, desigNo;
            deptNo = GetCodesFromEmpmas("subdeptno", idNo);
            desigNo = GetCodesFromEmpmas("subdesigno", idNo);
            lblDepartMent.Text = objCommon.LookUp("payroll_subdept", "SUBDEPT", "SUBDEPTNO=" + deptNo);
            lblDesignation.Text = objCommon.LookUp("payroll_subdesig", "SUBDESIG", "SUBDESIGNO=" + desigNo);
        }
        else
        {
            lblDepartMent.Text = string.Empty;
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
        return Convert.ToBoolean(objCommon.LookUp("payroll_paymas", "ISNULL(" + columName + ",0)", "idno=" + idNo));
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

    private string GetEarningHeadsAmount()
    {
        string earningsAmt;
        earningsAmt = string.Empty;

        foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
        {
            TextBox txEarningsAmt = lvitem.FindControl("txEarningsAmt") as TextBox;
            if (ViewState["action"].ToString() == "add")
                earningsAmt = earningsAmt + txEarningsAmt.Text + ",";
            else
                earningsAmt = earningsAmt + txEarningsAmt.ToolTip + "=" + txEarningsAmt.Text + ",";
        }
        earningsAmt = earningsAmt.Substring(0, earningsAmt.Length - 1);

        return earningsAmt;
    }

    private void PerCalculation()
    {

         double earningsAmt=0,TotAmt=0,calAmount=0,perAmt=0;
      
        foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
        {
            TextBox txEarningsAmt = lvitem.FindControl("txEarningsAmt") as TextBox;
            earningsAmt += Convert.ToDouble(txEarningsAmt.Text);                
        }   
   
       
        TotAmt = Convert.ToDouble(earningsAmt)+(Convert.ToDouble(txtSupliPay.Text));

        if (TotAmt >= 1)
        {
            calAmount = (TotAmt / 100);
            perAmt = calAmount * Convert.ToDouble(txtPersentage.Text);
            // amt.Text = Convert.ToString(perAmt);

            foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
            {
                TextBox txDeductionAmt = lvitem.FindControl("txDeductionAmt") as TextBox;
                if (txDeductionAmt.ToolTip == "D1")
                {
                    txDeductionAmt.Text = Convert.ToString(perAmt);
                }
            }
        }
        else
        {
            lblmsg.Text = "Enter Supli.Pay value Or Earning Heads value";
        }
    }

    private string GetDeductionHeadsAmount()
    {

        string deductionAmt;
        deductionAmt = string.Empty;

        foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
        {
            TextBox txDeductionAmt = lvitem.FindControl("txDeductionAmt") as TextBox;
            if (ViewState["action"].ToString() == "add")
                deductionAmt = deductionAmt + txDeductionAmt.Text + ",";
            else
                deductionAmt = deductionAmt + txDeductionAmt.ToolTip + "=" + txDeductionAmt.Text + ",";
        }

        deductionAmt = deductionAmt.Substring(0, deductionAmt.Length - 1);

        return deductionAmt;
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
        //divOrdDetails.Visible = false;
        pnlSupplimentaryBillDetails.Visible = true;
        pnl1.Visible = false;
        pnlSupplBillDetail.Visible = true;
        this.BindListViewEarningHeads();
        this.BindListViewDeductionHeads();
        this.FillEmployee();
        this.FillSupllimentaryHead();
        //to display employee count in footer
        hidEarningRecordsCount.Value = Convert.ToString(lvEarningHeads.Items.Count);
        hidTotalDeductionRecordsCount.Value = Convert.ToString(lvDeductionHeads.Items.Count);
    }

    protected void btnPersentage_Click(object sender, EventArgs e)
    {
        if (txtPersentage.Text!="")
        {
        
        PerCalculation();
        }
    }
}
