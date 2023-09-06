//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :RAISING PAYMENT BILL                                                   
// CREATION DATE :05-DEC-2018                                              
// CREATED BY    :Nokhlal Kumar                                       
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Forms;
using IITMS;
using IITMS.NITPRM;
using System.Web;
using System.IO;

public partial class ACCOUNT_RaisingPaymentBillApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();

    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    string isVoucherAutoCashBank = string.Empty;//auto generated voucher no but different for cash and banks
    string isMessagingEnabled = string.Empty;
    string isFourSign = string.Empty;
    string isBankCash = string.Empty;
    string isvoucher_Cheque_Print = string.Empty;
    string IsSponsorProject = "";

    string back = string.Empty;

    private DataTable dt = new DataTable();
    public string[] para;
    public static string isEdit;
    public static int RowIndex = -1;

    string isVoucherTypeWise = string.Empty;
    string AllowVoucherNoReset = string.Empty;
    public static string tranTypeForStrVno;
    public static string StrVno = "abc/111";
    public static string TransactionDateToUpdate = string.Empty;
    AccountTransactionController objPC1 = new AccountTransactionController();

    //For XML Doc

    XmlDocument xmlDoc = new XmlDocument();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                BindCompAccount();

                lvPendingList.Visible = false;

                //int serialno = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0) + 1", ""));
                //ViewState["RaisePayNo"] = serialno.ToString();
                //lblSerialNo.Text = ViewState["RaisePayNo"].ToString();

                objCommon.FillDropDownList(ddlDeptBranch, "Payroll_subdept", "SubDeptNo", "SubDept", "SubDeptNo<>0", "SubDept");

                int usernock = Convert.ToInt32(Session["userno"]);
                //BindPendingBillList();
                BindComnpany();
                dvbuttons.Visible = false;

                //SetParameters();
                GetGstValue();
                //GetTdsAmt();
                //GetTdsOnIgstAmt();
                GetTdsOnSGstAmt();
                GetTdsOnCGstAmt();
                GetTdsAmt();
                GetTdsOnIGstAmt();
                GetNetAmount();
                filldropdown();
            }
        }
    }

    private void filldropdown()
    {
        objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "NATURE_NAME");
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') + '['+ convert(nvarchar(150),EmployeeId) + ']' AS NAME", "IDNO > 0", "FNAME");

    }

    private void GetTdsAmt()
    {

        if (ddlSection.SelectedValue == "0" || ddlSection.SelectedValue == "")
        {
            txtTDSPer.Text = "0";
            //txtTdsOnAmt.Text = "0";
            txtTDSAmt.Text = "0";
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSPer.Text != "")
                txtTDSAmt.Text = (Convert.ToDouble(txtTdsOnAmt.Text) * Convert.ToDouble(txtTDSPer.Text) / 100).ToString();

        }
    }
    protected void GetGstValue()
    {
        double gstAmount = Convert.ToDouble(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text) + Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text);

        txtGSTAmount.Text = gstAmount.ToString();
        if (txtBillAmt.Text != "")
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();

    }
    private void GetTdsOnIGstAmt()
    {

        if (ddlTDSonGSTSection.SelectedValue == "0" || ddlTDSonGSTSection.SelectedValue == "")
        {
            txtTDSonGSTPer.Text = "0";
            //txtTDSGSTonAmount.Text = "0";
            txtTDSonGSTAmount.Text = "0";
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSonGSTPer.Text != "")
                txtTDSonGSTAmount.Text = (Convert.ToDouble(txtTDSGSTonAmount.Text) * Convert.ToDouble(txtTDSonGSTPer.Text) / 100).ToString();

        }
    }

    private void GetTdsOnSGstAmt()
    {

        if (ddlTDSonSGSTSection.SelectedValue == "0" || ddlTDSonSGSTSection.SelectedValue == "")
        {
            txtTDSonSGSTPer.Text = "0";
            //txtTDSonSGSTAmount.Text = "0";
            txtTDSonSGSTAmount.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSonSGSTPer.Text != "")
                txtTDSonSGSTAmount.Text = (Convert.ToDouble(txtTDSSGSTonAmount.Text) * Convert.ToDouble(txtTDSonSGSTPer.Text) / 100).ToString();

        }
    }
    private void GetTdsOnCGstAmt()
    {

        if (ddlTDSonCGSTSection.SelectedValue == "0" || ddlTDSonCGSTSection.SelectedValue == "")
        {
            txtTDSonCGSTPer.Text = "0";
            txtTDSonCGSTAmount.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSonCGSTPer.Text != "")
                txtTDSonCGSTAmount.Text = (Convert.ToDouble(txtTDSCGSTonAmount.Text) * Convert.ToDouble(txtTDSonCGSTPer.Text) / 100).ToString();

        }
    }

    private void GetTdsOnIgstAmt()
    {
        if (ddlTDSonGSTSection.SelectedValue == "0" || ddlTDSonGSTSection.SelectedValue == "")
        {
            txtTDSonGSTPer.Text = "";
            txtTDSonGSTAmount.Text = string.Empty;
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSPer.Text = "";
            }
        }
    }

    private void GetNetAmount()
    {
        if (!chkGST.Checked && !chkIGST.Checked)
        {
            txtGSTAmount.Text = "0";
        }

        if (txtBillAmt.Text != "")
        {
            Double NetAmount = Convert.ToDouble(txtBillAmt.Text);
            Double TotalDedution = 0;
            Double GrossAmount = Convert.ToDouble(txtBillAmt.Text);

            if (chkTDSApplicable.Checked)
            {
                if (txtTDSAmt.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSAmt.Text);
                    TotalDedution = Convert.ToDouble(txtTDSAmt.Text);
                }
            }
            if (chkTdsOnCGSTSGST.Checked)
            {
                if (txtTDSonCGSTAmount.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSonCGSTAmount.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtTDSonCGSTAmount.Text);
                }
                if (txtTDSonSGSTAmount.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSonSGSTAmount.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtTDSonSGSTAmount.Text);
                }

            }
            if (chkTDSOnGst.Checked)
            {
                if (txtTDSonGSTAmount.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSonGSTAmount.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtTDSonGSTAmount.Text);
                }
            }
            if (chkSecurity.Checked)
            {
                if (txtSecurityAmt.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtSecurityAmt.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtSecurityAmt.Text);
                }
            }
            if (chkIGST.Checked)
            {
                if (txtIgstAmount.Text != "")
                {
                    NetAmount = NetAmount + Convert.ToDouble(txtIgstAmount.Text);
                    GrossAmount = GrossAmount + Convert.ToDouble(txtIgstAmount.Text);
                }
            }
            if (chkGST.Checked)
            {
                if (txtCgstAmount.Text != "")
                {
                    NetAmount = NetAmount + Convert.ToDouble(txtCgstAmount.Text);
                    GrossAmount = GrossAmount + Convert.ToDouble(txtCgstAmount.Text);
                }
                if (txtSgstAmount.Text != "")
                {
                    NetAmount = NetAmount + Convert.ToDouble(txtSgstAmount.Text);
                    GrossAmount = GrossAmount + Convert.ToDouble(txtSgstAmount.Text);
                }
            }

            txtNetAmt.Text = NetAmount.ToString();
            txtTotalTDSAmt.Text = TotalDedution.ToString();
            txtTotalBillAmt.Text = GrossAmount.ToString();
            //txtTotalTDSAmt.Text = 
            //txtNetAmt.Text = (Convert.ToDouble(txtTotalBillAmt.Text) - Convert.ToDouble(txtTotalTDSAmt.Text)).ToString();
        }


    }
    private void BindCompAccount()
    {
        int userno = Convert.ToInt32(Session["userno"].ToString());

        DataSet ds = null;
        ds = objRPBController.GetCompAccount(userno);

        ddlCompAccount.Items.Clear();
        ddlCompAccount.Items.Add("Please Select");
        ddlCompAccount.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCompAccount.DataSource = ds;
            ddlCompAccount.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlCompAccount.DataTextField = ds.Tables[0].Columns[2].ToString();
            ddlCompAccount.DataBind();
            ddlCompAccount.SelectedIndex = 0;
        }
    }

    private void SetParameters()
    {
        objCommon = new Common();
        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_"+Session["comp_code"]+"_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
        int i = 0;
        if (ds != null)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "SINGLE MODE PAYMENT/RECEIPT/CONTRA ENTRY")
                {
                    isSingleMode = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "PER ENTRY NARRATION REQUIRED")
                {
                    isPerNarration = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "AUTOGENERATED VOUCHER NO. REQUIRED")
                {
                    isVoucherAuto = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "MOBILE MESSAGING ENABLED")
                {
                    isMessagingEnabled = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "AUTO GEN VOUCHERNO FOR CASH BANK")
                {
                    isVoucherAutoCashBank = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "VOUCHER NO SEPRATE FOR RCPT,PAY,CONT,JOUN")
                {
                    isVoucherTypeWise = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ASK PRINT VOUCHER OR PRINT CHEQUE ?")
                {
                    isvoucher_Cheque_Print = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW SPONSOR PROJECT")
                {
                    IsSponsorProject = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW VOUCHER NO RESETTING")
                {
                    AllowVoucherNoReset = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    private void BindComnpany()
    {

        int IsExist = Convert.ToInt32(objCommon.LookUp("acc_usercashbook", "ISNULL(COUNT(*),0)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
        if (IsExist > 0)
        {
            objCommon.FillDropDownList(ddlSelectCompany, "ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + Session["userno"].ToString() + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlSelectCompany, "Acc_Company", "Company_no", "Company_Name + ' - ' + Cast(Year(COMPANY_FINDATE_FROM) as nvarchar(10))+ ' - ' + Cast(Year(COMPANY_FINDATE_TO) As nvarchar(10))", "Drop_Flag='N'", "Company_Name");
        }

    }

    private void BindBudget()
    {
        if (Convert.ToInt32(ddlSelectCompany.SelectedValue) > 0)
        {
            //  objCommon.FillDropDownList(ddlBudgethead, "ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");
            //objCommon.FillDropDownList(ddlBudgethead, "ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");
            //ADDED BY VIJAY ANDOJU FOR NEW BUDGETHEAD ON 02092020
            CostCenterController objbud = new CostCenterController();
            objbud.BindBudgetHead(ddlBudgethead);
        }
        else
        {

        }
    }

    private void BindPendingBillList()
    {
        try
        {
            int Compacc = Convert.ToInt32(ddlCompAccount.SelectedItem.Text.ToString().Split('*')[0].ToString());
            string compcode = ddlCompAccount.SelectedItem.Text.ToString().Split('*')[2].ToString();
            DataSet ds = objRPBController.GetPendingBillListforCaseWorker(Convert.ToInt32(Session["userno"].ToString()), Compacc, compcode);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(UPDLedger, "You Are Not Authorised To Approve The Bill...", this.Page);
                return;
            }

            //if (ds.Tables[0].Rows.Count <= 0)
            //{
            //    //dpPager.Visible = false;
            //}
            //else
            //{
            //    //dpPager.Visible = true;
            //}

            lvPendingList.DataSource = ds;
            lvPendingList.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_PenidingBillForward.BindPendingBillList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void BindDropDown()
    {
        objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlCgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlSgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlIgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");

        objCommon.FillDropDownList(ddlTDSonCGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonSGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
    }

    private void ShowDetails(int BillNo)
    {
        DataSet ds = new DataSet();

        try
        {
            string compcode = ddlCompAccount.SelectedItem.Text.ToString().Split('*')[2].ToString();

            ds = objRPBController.GetSingleRecordsPendingBill(BillNo, Convert.ToInt32(Session["userno"].ToString()), compcode);    //Session["Comp_Code"].ToString()

            string Bill_Status = objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "BILL_STATUS", "RAISE_PAY_NO =" + BillNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BillNo"] = BillNo;
                lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();

                Session["BillComp_Code"] = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();
                BindDropDown();
                //FILL Account Dropdown....
                ddlAccount.Items.Clear();
                int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_Comp_Account_Master", "Count(*)", ""));
                if (count > 0)
                {
                    objCommon.FillDropDownList(ddlAccount, "ACC_" + Session["BillComp_Code"].ToString() + "_Comp_Account_Master", "Acc_Id", "Account_Name", "", "Acc_Id");
                    BindBudget();
                }
                else
                {
                    ddlAccount.Items.Clear();
                }

                objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");

                ddlAccount.SelectedValue = ds.Tables[0].Rows[0]["ACCOUNT"].ToString();
                ddlDeptBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();

                int EMPNO = Convert.ToInt32(ds.Tables[0].Rows[0]["EMPNO"].ToString());
                string Path = "";
                if (Bill_Status == "R")
                {
                    Path = "->Finance Branch->AO->FO";
                }
                else
                {
                    Path = "AO->FO";
                }

                if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
                {
                    if (EMPNO == 0)
                    {
                        string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "TOP 1 PAPATH", "DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue) + " GROUP BY PAPATH ORDER BY COUNT(PAPATH) DESC");
                        lblAuthorityPath.Text = PaPath.ToString() + Path.ToString();
                        lblAuthorityPath.Font.Bold = true;
                        dvApproval.Visible = true;
                        dvAuthorityPath.Visible = true;
                        txtApprovalNo.Focus();
                    }
                    else
                    {
                        string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "PAPATH", "idno = " + EMPNO + " AND DEPTNO = " + Convert.ToInt32(ds.Tables[0].Rows[0]["DEPT_ID"].ToString()));
                        lblAuthorityPath.Text = PaPath.ToString() + Path.ToString();
                        lblAuthorityPath.Font.Bold = true;

                        dvApproval.Visible = true;
                        dvAuthorityPath.Visible = true;
                        txtApprovalNo.Focus();
                    }
                }
                else
                {
                    lblAuthorityPath.Text = "";
                    lblAuthorityPath.Font.Bold = true;
                    dvApproval.Visible = false;
                    dvAuthorityPath.Visible = false;
                }

                txtApprovalNo.Text = ds.Tables[0].Rows[0]["APPROVAL_NO"].ToString();
                txtApprovalDate.Text = ds.Tables[0].Rows[0]["APPROVAL_DATE"].ToString();
                lblApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["BUDGET_NO"].ToString()) > 0)
                {
                    ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                    string company_code = objCommon.LookUp("acc_company", "COMPANY_CODE", "COMPANY_NO=" + ddlSelectCompany.SelectedValue);
                 DataSet   ds2 = objRPBController.GetBudegetBalanceNEW(Convert.ToInt32(ddlDeptBranch.SelectedValue), Convert.ToInt32(ddlBudgethead.SelectedValue), DateTime.Today);
                 if (ds2 != null)
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds2.Tables[0].Rows[0]["BALANCE"].ToString()));
                        }
                        else
                        {
                            lblBudgetClBal.Text = "0.00";
                        }
                    }
                   
                }
                else
                {
                    ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                }                

                DataSet ds1 = null;
                ds1 = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD BH Left Join ACC_" + Session["BillComp_Code"].ToString() + "_TRANS T ON BH.BUDG_NO = T.BudgetNo", "BH.BUDG_NO,BH.BUDG_NAME", "(ISNULL(BH.BUD_AMT,0) - ISNULL(SUM(Case when T.[TRAN] = 'Dr' Then T.AMOUNT When T.[TRAN] = 'Cr' Then -(T.AMOUNT) End),0)) As BUD_BAL_AMT", "BH.BUDG_NO =" + Convert.ToInt32(ddlBudgethead.SelectedValue) + " Group By BH.BUDG_NO,BH.BUDG_NAME,BH.BUD_AMT", "");
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDouble(ds1.Tables[0].Rows[0]["BUD_BAL_AMT"].ToString()) >= 0)
                        {
                            lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds1.Tables[0].Rows[0]["BUD_BAL_AMT"].ToString())) + " Dr";
                        }
                        else
                        {
                            lblBudgetClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(ds1.Tables[0].Rows[0]["BUD_BAL_AMT"].ToString())) + " Cr";
                        }
                    }
                    else
                    {
                        lblBudgetClBal.Text = "0.00";
                    }
                }

                string PartyNo = ds.Tables[0].Rows[0]["LEDGER_NO"].ToString();
                if (Convert.ToInt32(PartyNo) > 0)
                {
                    string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + PartyNo);
                    txtLedgerHead.Text = partyname.ToString();
                 
                    string partyNo = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtLedgerHead.Text.ToString().Trim().Split('*')[1] + "'");
                    PartyController objPC = new PartyController();
                    DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["BillComp_Code"].ToString());
                    if (dtr.Read())
                    {

                        if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
                        {
                            lblLedgerClBal.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
                            //lblCur2.Text = "Dr";
                        }
                        else
                        {
                            lblLedgerClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(dtr["BALANCE"].ToString().Trim())) + " Cr";
                            //lblCur2.Text = "Cr";
                        }
                        //ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                    }
                }
                else
                {
                    string partyname = "";
                    txtLedgerHead.Text = partyname.ToString();
                    lblLedgerClBal.Text = "0";
                }


                string ExpenseLedgerNo = ds.Tables[0].Rows[0]["EXPENSE_LEDGER_NO"].ToString();
                if (Convert.ToInt32(ExpenseLedgerNo) > 0)
                {
                    string ExpenseLedgerName = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + ExpenseLedgerNo);
                    txtExpenseLedger.Text = ExpenseLedgerName.ToString();

                    string expenseledgerno = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtExpenseLedger.Text.ToString().Trim().Split('*')[1] + "'");
                    PartyController objPC = new PartyController();
                    DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(expenseledgerno), Session["BillComp_Code"].ToString());
                    if (dtr.Read())
                    {

                        if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
                        {
                            lblExpenseLedger.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";

                        }
                        else
                        {
                            lblExpenseLedger.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(dtr["BALANCE"].ToString().Trim())) + " Cr";

                        }

                    }
                }
                else
                {
                    string partyname = "";
                    txtLedgerHead.Text = partyname.ToString();
                    lblLedgerClBal.Text = "0";
                }


                rdbBillList.SelectedValue = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                txtNatureOfService.Text = ds.Tables[0].Rows[0]["NATURE_SERVICE"].ToString();
                txtPayeeNameAddress.Text = ds.Tables[0].Rows[0]["PAYEE_NAME_ADDRESS"].ToString();
                txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                txtGSTINNo.Text = ds.Tables[0].Rows[0]["GSTIN_NO"].ToString();



                //added by Akshay Dixit 29/08/2022 for tag user

                if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                {
                    Diveservices.Visible = true;
                   
                    txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();      //for old bill 
                }
                else
                {
                    Diveservices.Visible = false;
                  

                }
                if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                {
                    divProviderType.Visible = false;
                 
                    ddlEmpType.SelectedValue = "0";
                }
                else
                {
                    divProviderType.Visible = true;
                 
                    ddlEmpType.SelectedValue = ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString();
                }

                if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                {
                    divEmployee1.Visible = false;
                    divEmployee2.Visible = false;
                    //divPayeeNature.Visible = false;                 
                    //divPayee.Visible = false;
                  
                }
                else
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                    {
                        divEmployee1.Visible = true;
                        divEmployee2.Visible = true;
                    }
                    else
                    {
                        divEmployee1.Visible = false;
                        divEmployee2.Visible = false;
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                    {
                        //divPayeeNature.Visible = true;                     
                        //divPayee.Visible = true;
                      
                    }
                    else
                    {
                        //divPayeeNature.Visible = false;
                      
                        //divPayee.Visible = false;
                        
                    }
                }

                if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                {
                    divEmployee1.Visible = false;
                    divEmployee2.Visible = false;
                 //   divPayeeNature.Visible = false;
              
                  //  divPayee.Visible = false;
                  
                }
                else
                {

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                    {
                        ddlPayeeNature.SelectedValue = "0";
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                    {
                        ddlPayeeNature.SelectedValue = ds.Tables[0].Rows[0]["PAYEE_NATURE"].ToString();
                    }
                    else
                    {
                        ddlPayeeNature.SelectedValue = "0";
                    }
                    objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                    {
                        ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                    {
                        ddlPayee.SelectedValue = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                    }
                    else
                    {
                        ddlEmployee.SelectedValue = "0";
                        ddlPayee.SelectedValue = "0";
                    }

                }

                txtGSTINNo.Text = ds.Tables[0].Rows[0]["GSTIN_NO"].ToString();

                string date = ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"].ToString();
                if (date != "" || date != string.Empty)
                {
                    txtInvoiceDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtInvoiceDate.Text = "";
                }
                txtInvoiceNo.Text = ds.Tables[0].Rows[0]["BILL_INVOICE_NO"].ToString();

                txtBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["BILL_AMT"]).ToString();
                int IsTDS = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDS"].ToString());
                txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                if (IsTDS == 1)
                {
                    chkTDSApplicable.Checked = true;
                    dvSection.Visible = true;
                    txtTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_AMT"]).ToString();
                    txtTdsOnAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_ON_AMT"]).ToString();
                    ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION_NO"].ToString();
                    txtTDSPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_PERCENT"]).ToString();
                    //txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                }
                else
                {
                    chkTDSApplicable.Checked = false;
                    dvSection.Visible = false;
                    txtTDSAmt.Text = "";
                    txtTdsOnAmt.Text = "";
                    //ddlSection.SelectedValue = "0";
                    txtTDSPer.Text = "";
                    //txtPanNo.Text = "";
                }
                //Added by vijay andoju
                int isigst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISIGST"].ToString());
                int igst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISGST"].ToString());
                if (igst == 1)
                {
                    chkGST.Checked = true;
                    txtCgstAmount.Text = ds.Tables[0].Rows[0]["CGST_AMOUNT"].ToString();
                    txtCGSTPER.Text = ds.Tables[0].Rows[0]["CGST_PER"].ToString();
                    ddlCgstSection.SelectedValue = ds.Tables[0].Rows[0]["CGST_SECTIONNO"].ToString();

                    txtSgstAmount.Text = ds.Tables[0].Rows[0]["SGST_AMOUNT"].ToString();
                    txtSgstPer.Text = ds.Tables[0].Rows[0]["SGST_PER"].ToString();
                    ddlSgstSection.SelectedValue = ds.Tables[0].Rows[0]["SGST_SECTIONNO"].ToString();
                    dvCgst.Visible = true;
                    dvSgst.Visible = true;
                    dvGSTLedger.Visible = true;
                }
                else
                {
                    dvCgst.Visible = false;
                    dvSgst.Visible = false;
                    dvGSTLedger.Visible = false;
                    chkGST.Checked = false;
                }
                if (isigst == 1)
                {

                    chkIGST.Checked = true;
                    txtIgstPer.Text = ds.Tables[0].Rows[0]["IGST_PER"].ToString();
                    txtIgstAmount.Text = ds.Tables[0].Rows[0]["IGST_AMOUNT"].ToString();
                    ddlIgstSection.SelectedValue = ds.Tables[0].Rows[0]["IGST_SECTIONNO"].ToString();
                    dvIGST.Visible = true;
                    dvIgstledger.Visible = true;

                }
                else
                {
                    dvIGST.Visible = false;
                    dvIgstledger.Visible = false;
                    chkIGST.Checked = false;
                }
                //Added by gopal anthati on 28042021
                int isTdsonGst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONGST"].ToString());
                if (isTdsonGst == 1)
                {
                    chkTDSOnGst.Checked = true;
                    txtTDSonGSTPer.Text = ds.Tables[0].Rows[0]["TDSONGSTPER"].ToString();
                    txtTDSonGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONGST_AMOUNT"].ToString();
                    txtTDSGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSGST_ON_AMT"].ToString();
                    ddlTDSonGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONGST_SECTION"].ToString();
                    divTdsOnGst.Visible = true;
                    divTdsonGstLedger.Visible = true;

                }
                else
                {
                    //divTdsOnGst.Visible = false;
                    //divTdsonGstLedger.Visible = false;
                    chkTDSOnGst.Checked = false;
                    divTdsOnGst.Visible = false;
                    txtTDSonGSTAmount.Text = "";
                    txtTDSGSTonAmount.Text = "";                    
                    txtTDSonGSTPer.Text = "";                  
                }

                //
                int isTdsonCGSTIGST = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONCGSTSGST"].ToString());
                if (isTdsonCGSTIGST == 1)
                {
                    chkTdsOnCGSTSGST.Checked = true;
                    txtTDSonCGSTPer.Text = ds.Tables[0].Rows[0]["TDSONCGSTPER"].ToString();
                    txtTDSonCGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONCGST_AMOUNT"].ToString();
                    txtTDSCGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSCGST_ON_AMT"].ToString();
                    ddlTDSonCGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONCGST_SECTION"].ToString();
                    divTdsOnCGST.Visible = true;
                    divTdsonCGstLedger.Visible = true;

                    txtTDSonSGSTPer.Text = ds.Tables[0].Rows[0]["TDSONSGSTPER"].ToString();
                    txtTDSonSGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONSGST_AMOUNT"].ToString();
                    txtTDSSGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSSGST_ON_AMT"].ToString();
                    ddlTDSonSGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONSGST_SECTION"].ToString();
                    divTDSOnSGST.Visible = true;
                    divTdsonSGstLedger.Visible = true;

                }
                else
                {                    
                    chkTdsOnCGSTSGST.Checked = false;
                    divTdsOnCGST.Visible = false;
                    txtTDSonCGSTAmount.Text = "";
                    txtTDSCGSTonAmount.Text = "";
                    txtTDSonCGSTPer.Text = "";
                   
                    divTDSOnSGST.Visible = false;
                    txtTDSonSGSTAmount.Text = "";
                    txtTDSSGSTonAmount.Text = "";
                    txtTDSonSGSTPer.Text = "";
                }
                int isTdsonSecurity = Convert.ToInt32(ds.Tables[0].Rows[0]["ISSECURITY"].ToString());
                if (isTdsonSecurity == 1)
                {

                    chkSecurity.Checked = true;
                    txtSecurityPer.Text = ds.Tables[0].Rows[0]["SECURITY_PER"].ToString();
                    txtSecurityAmt.Text = ds.Tables[0].Rows[0]["SECURITY_AMOUNT"].ToString();
                    
                    divSecurity.Visible = true;
                    divSecurityLedger.Visible = true;

                }
                else
                {
                    divSecurity.Visible = false;
                    divSecurityLedger.Visible = false;
                }
                //

                txtTotalBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_BILL_AMT"]).ToString();
                txtTotalTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_TDS_AMT"]).ToString();
                txtGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GST_AMT"]).ToString();
                txtNetAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_AMT"]).ToString();

                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                //---Commented by Gopal Anthati on 11-08-2021
                //string TransDate = ds.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                //if (TransDate == "" || TransDate == string.Empty)
                //{
                //    txtTransDate.Text = "";
                //}
                //else
                //{
                //    txtTransDate.Text = Convert.ToDateTime(TransDate).ToString("dd/MM/yyyy");
                //}
                //--Added by Gopal Anthati on 11-08-2021 to show current date as transaction date 
                txtTransDate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");

                int TDSID = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_TDSID"].ToString());
                int BankId = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_BANKID"].ToString());
                string Narration = ds.Tables[0].Rows[0]["TRANS_NARRATION"].ToString();

                int TRANS_TDSONGSTID = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_TDSONGSTID"].ToString());
                int TRANS_TDSONCGSTID = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_TDSONCGSTID"].ToString());
                int TRANS_TDSONSGSTID = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_TDSONSGSTID"].ToString());
                int TRANS_SECURITYID = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_SECURITYID"].ToString());


                int CGST = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_CGST_ID"].ToString());
                int SGST = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_SGST_ID"].ToString());
                int IGST = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANS_IGST_ID"].ToString());

                if (TRANS_TDSONGSTID > 0)
                {
                    string TDSonGstParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + TRANS_TDSONGSTID);
                    txtTDSonGSTLedger.Text = TDSonGstParty.ToString();
                }
                else
                {
                    txtTDSonGSTLedger.Text = "";
                }
                if (TRANS_TDSONCGSTID > 0)
                {
                    string TDSonCGstParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + TRANS_TDSONCGSTID);
                    txtTDSonCGSTLedger.Text = TDSonCGstParty.ToString();
                }
                else
                {
                    txtTDSonCGSTLedger.Text = "";
                }
                if (TRANS_TDSONSGSTID > 0)
                {
                    string TDSonSGstParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + TRANS_TDSONSGSTID);
                    txtTDSonSGSTLedger.Text = TDSonSGstParty.ToString();
                }
                else
                {
                    txtTDSonCGSTLedger.Text = "";
                }
                if (CGST > 0)
                {
                    string CGSTParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + CGST);
                    txtCGSTLedger.Text = CGSTParty.ToString();
                }
                else
                {
                    txtCGSTLedger.Text = "";
                }
                if (SGST > 0)
                {
                    string SGSTParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + SGST);
                    txtSgstLedger.Text = SGSTParty.ToString();
                }
                else
                {
                    txtSgstLedger.Text = "";
                }
                if (IGST > 0)
                {
                    string IGSTParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + IGST);
                    txtIGSTLedger.Text = IGSTParty.ToString();
                }
                else
                {
                    txtIGSTLedger.Text = "";
                }



                if (TDSID > 0)
                {
                    string TDSParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + TDSID);
                    txtTDSLedger.Text = TDSParty.ToString();
                }
                else
                {
                    txtTDSLedger.Text = "";
                }

                if (BankId > 0)
                {
                    string BankParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + BankId);
                    txtBankLedger.Text = BankParty.ToString();
                }
                else
                {
                    txtBankLedger.Text = "";
                }

                if (TRANS_SECURITYID > 0)
                {
                    string SecurityParty = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO =" + TRANS_SECURITYID);
                    txtSecurityLedger.Text = SecurityParty.ToString();
                }
                else
                {
                    txtSecurityLedger.Text = "";
                }

                txtNarration.Text = Narration.ToString();

                string strCombinefirst = stringCombine();
                ViewState["strCombine"] = strCombinefirst;

                pnlBillDetails.Visible = true;
                pnlBillList.Visible = false;
                dvbuttons.Visible = true;

                dvLedgers.Visible = true;
                if (chkTDSApplicable.Checked)
                {
                    tdslabel.Visible = true;
                    tdstextbox.Visible = true;
                }
                else
                {
                    tdslabel.Visible = false;
                    tdstextbox.Visible = false;
                }

                DataSet dsAOFO = objCommon.FillDropDown("ACC_MAIN_CONFIGURATION", "AO", "FO", "", "");
                if (dsAOFO.Tables[0].Rows.Count > 0)
                {                   
                    if (Convert.ToInt32(dsAOFO.Tables[0].Rows[0]["FO"]) == Convert.ToInt32(Session["userno"].ToString()))
                    {
                        ddlSelect.Items.Clear();
                        ddlSelect.Items.Insert(0, new ListItem("Approve & Final Submit", "A"));
                        ddlSelect.Items.Insert(1, new ListItem("Return", "R"));
                        divPaymode.Visible = true;
                        //ddlSelect.SelectedValue = "A";
                        //ddlSelect.Items.RemoveAt(1);
                    }
                    else
                    {
                        ddlSelect.Items.Clear();
                        ddlSelect.Items.Insert(0, new ListItem("Forward To Next Authority(Recommended)", "F"));
                        ddlSelect.Items.Insert(1, new ListItem("Return", "R"));
                        divPaymode.Visible = false;
                        //ddlSelect.SelectedValue = "F";
                        //ddlSelect.Items.RemoveAt(0);
                    }
                }

                //Added by Vidisha on 13-05-2021 for multiple bill download
                DataSet dsDoc = objCommon.FillDropDown("ACC_BILL_RAISED_UPLOAD_DOCUMENT", "*", "", "RAISE_PAY_NO =" + BillNo, "Raise_Pay_No");
                if (Convert.ToInt32(dsDoc.Tables[0].Rows.Count) > 0)
                {
                    lvNewFiles.DataSource = dsDoc.Tables[0];
                    lvNewFiles.DataBind();
                    pnlNewFiles.Visible = true;
                }
                else
                {
                    pnlNewFiles.Visible = false;
                    lvNewFiles.DataSource = null;
                    lvNewFiles.DataBind();
                }
                //end
                this.Disable();

            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Something went wrong! Try after sometimes...", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_RaisingPaymentBillApproval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string stringCombine()
    {
        string strCombine = string.Empty;

        strCombine = lblSerialNo.Text + ddlSelectCompany.SelectedItem.Text + ddlAccount.SelectedItem.Text + ddlDeptBranch.SelectedItem.Text + txtApprovalNo.Text;
        strCombine = strCombine + Convert.ToDateTime(txtApprovalDate.Text).ToString("yyyy-MM-dd") + lblApprovedBy.Text + ddlBudgethead.SelectedItem.Text + txtLedgerHead.Text;
        strCombine = strCombine + rdbBillList.SelectedValue + txtInvoiceNo.Text + txtInvoiceDate.Text + txtNatureOfService.Text + txtServiceName.Text + txtPayeeNameAddress.Text + txtGSTINNo.Text;
        strCombine = strCombine + txtBillAmt.Text + (chkTDSApplicable.Checked ? "1" : "0").ToString() + ddlSection.SelectedItem.Text + txtTDSPer.Text + txtTDSAmt.Text + txtPanNo.Text;
        strCombine = strCombine + txtGSTAmount.Text + txtTotalBillAmt.Text + txtNetAmt.Text + txtRemark.Text;
        strCombine = strCombine + txtTransDate.Text + txtTDSLedger.Text + txtBankLedger.Text + txtNarration.Text;
        //strCombine = strCombine + txtBillAmt.Text + (chkTDSOnGst.Checked ? "1" : "0").ToString() + ddlTDSonGSTSection.SelectedItem.Text + txtTDSonGSTPer.Text + txtTDSonGSTAmount.Text;

        return strCombine;
    }

    private void Enable()
    {
        ddlDeptBranch.Enabled = true;
        rdbBillList.Enabled = true;
        txtNatureOfService.Enabled = true;
        txtServiceName.Enabled = true;
        txtPayeeNameAddress.Enabled = true;
        txtPanNo.Enabled = true;
        txtGSTINNo.Enabled = true;
        txtBillAmt.Enabled = true;
        txtRemark.Enabled = true;
        txtGSTAmount.Enabled = true;
        txtTotalBillAmt.Enabled = true;
    }

    private void Disable()
    {
        ddlDeptBranch.Enabled = false;
        rdbBillList.Enabled = false;
        txtNatureOfService.Enabled = false;
        txtServiceName.Enabled = false;
        txtPayeeNameAddress.Enabled = false;
        txtPanNo.Enabled = false;
        txtGSTINNo.Enabled = false;
        txtBillAmt.Enabled = false;
        txtRemark.Enabled = false;
        txtGSTAmount.Enabled = false;
        txtTotalBillAmt.Enabled = false;
    }


    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //BindPendingBillList();
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int BillNo = int.Parse(btnApproval.CommandArgument);
            Session["BillNo"] = BillNo;

            string compcode = ddlCompAccount.SelectedItem.Text.ToString().Split('*')[2].ToString();

            DataSet ds = objRPBController.GetSingleRecordsPendingBill(BillNo, Convert.ToInt32(Session["userno"].ToString()), compcode);  // Session["Comp_Code"].ToString()
            if (ds.Tables.Count > 0)
            {

            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Selected Bill is not approved or forwarded by previous authority than you..!", this.Page);
                return;
            }
          
            
            ShowDetails(BillNo);

            SetParameters();
            if (IsSponsorProject == "Y")
            {
                trSponsor.Visible = true;
                objCommon.FillDropDownList(ddlSponsor, "Acc_" + Session["BillComp_Code"].ToString() + "_Project", "ProjectId", "ProjectName", "", "");
            }
            else
            {
                //trSponsor.Visible = false;
                trSponsor.Attributes.Add("style", "display:none");
                trSubHead.Attributes.Add("style", "display:none");
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectId"].ToString()) > 0)
            {
                ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
            }
            else
            {
                ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
            }
            objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["BillComp_Code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["BillComp_Code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "A.ProjectId=" + ddlSponsor.SelectedValue, "");
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectSubId"].ToString()) > 0)
            {
                ddlProjSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
                ddlProjSubHead_SelectedIndexChanged(sender, e);
            }
            else
            {
                ddlProjSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
            }
           
           // AddGstAmount();
            ViewState["action"] = "edit";

            //string Authority = objCommon.LookUp("ACC_MAIN_CONFIGUATION","AO","");
            //if (Authority == Session["userno"].ToString())
            //    divPaymode.Visible = false;            
            //else
            //    divPaymode.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNTS_RaisingPaymentBillApproval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        
        pnlBillDetails.Visible = false;
        pnlBillList.Visible = true;
        dvbuttons.Visible = false;
        lvPendingList.DataSource = null;
        lvPendingList.DataBind();
        ddlCompAccount.SelectedIndex = 0;
        ViewState["strCombine"] = string.Empty;
        //ddlSelect.SelectedValue = "A";
        txtTransDate.Text = "";
        txtTDSLedger.Text = "";
        txtBankLedger.Text = "";
        ddlPaymentMode.SelectedIndex = 0;
        txtNarration.Text = "";
       
        chkGST.Checked = false;
        chkIGST.Checked = false;
        chkTDSApplicable.Checked = false;        
        chkTDSOnGst.Checked = false;
        divTdsonGstLedger.Visible = false;
        txtTDSonGSTLedger.Text = string.Empty;

        dvGSTLedger.Visible = false;
        dvIgstledger.Visible = false;
        dvCgst.Visible = false;
        dvSgst.Visible = false;
        dvIGST.Visible = false;
        txtIgstAmount.Text = txtIgstPer.Text = txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = string.Empty;
    }
    protected void ddlSelectCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Comp_code = objCommon.LookUp("Acc_Company", "Company_Code", "Company_No = " + Convert.ToInt32(ddlSelectCompany.SelectedValue));
        Session["BillComp_Code"] = Comp_code;
        ddlAccount.Items.Clear();
        int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Comp_code + "_Comp_Account_Master", "Count(*)", ""));
        if (count > 0)
        {
            objCommon.FillDropDownList(ddlAccount, "ACC_" + Comp_code + "_Comp_Account_Master", "Acc_Id", "Account_Name", "", "Acc_Id");
            BindBudget();
        }
        else
        {
            ddlAccount.Items.Clear();
        }
        objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
    }
    protected void ddlDeptBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
        {
            string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
            lblAuthorityPath.Text = PaPath.ToString();
            lblAuthorityPath.Font.Bold = true;

            string FirstAuth = objCommon.LookUp("ACC_PASSING_AUTHORITY APA Inner Join ACC_PASSING_AUTHORITY_PATH APAP ON APA.PANO = APAP.PAN01 Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PANAME", "UA.UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
            lblApprovedBy.Text = FirstAuth.ToString();
            lblApprovedBy.Font.Bold = true;

            dvApproval.Visible = true;
            dvAuthorityPath.Visible = true;
            txtApprovalNo.Focus();
        }
        else
        {
            lblAuthorityPath.Text = "";
            lblAuthorityPath.Font.Bold = true;
            dvApproval.Visible = false;
            dvAuthorityPath.Visible = false;
        }
    }
    protected void rdbTDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbTDS.SelectedValue == "1")
        {
            dvSection.Visible = true;
        }
        else
        {
            dvSection.Visible = false;
        }
    }
    //protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    //{
    //    double CalcTDS = 0;
    //    if (chkTDSApplicable.Checked)
    //    {
    //        dvSection.Visible = true;
    //        if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
    //        {
    //            if (ddlSection.SelectedValue != "0")
    //            {
    //                CalcTDS = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSPer.Text)) * 0.01);
    //                txtTDSAmt.Text = CalcTDS.ToString();
    //            }
    //            if (txtGSTAmount.Text != "")
    //            {
    //                txtTotalBillAmt.Text = Math.Round((Convert.ToDouble(txtBillAmt.Text) + Convert.ToDouble(txtGSTAmount.Text)) - Convert.ToDouble(txtTDSAmt.Text)).ToString();
    //            }
    //        }
    //    }
    //    else
    //    {

    //        txtNetAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtNetAmt.Text == "" ? "0" : txtNetAmt.Text)) + (Convert.ToDouble(txtTDSAmt.Text == "" ? "0" : txtTDSAmt.Text))).ToString();
    //        dvSection.Visible = false;
    //        txtTDSAmt.Text = txtTDSPer.Text = txtPanNo.Text = string.Empty;
    //        ddlSection.SelectedValue = "0";


    //    }
    //}
    //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSection.SelectedValue == "2")
    //    {
    //        txtTDSPer.Text = Convert.ToDouble("10").ToString();
    //    }
    //    else
    //    {
    //        txtTDSPer.Text = "";
    //    }
    //}

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            //AutoCompleteController objAutocomplete = new AutoCompleteController();
            RaisingPaymentBillController objPayBill = new RaisingPaymentBillController();
            ds = objPayBill.GetAccountEntryLedger(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            //AutoCompleteController objAutocomplete = new AutoCompleteController();
            RaisingPaymentBillController objPayBill = new RaisingPaymentBillController();
            ds = objPayBill.GetAccountEntryCashBank(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Added by gopal anthati on 20-08-2021 To catch the cleared values after postback
            //if (hdnTDSonCGSTAmount.Value != "" || hdnTDSonCGSTAmount.Value != "0")
            //    txtTDSonSGSTAmount.Text = hdnTDSonCGSTAmount.Value;
            //if (hdnGSTAmount.Value != "" || hdnGSTAmount.Value != "0")
            //    txtGSTAmount.Text = hdnGSTAmount.Value;

            // To avoid resend action/duplicate  entry added by gopal anthati on 18-08-2021
            int Count = Convert.ToInt32(objCommon.LookUp("ACC_"+Session["BillComp_Code"].ToString()+"_TRANS", "Count(*)", "BILL_ID=" + Convert.ToInt32(lblSerialNo.Text)+" AND TRANSACTION_TYPE <> 'J'"));
            if (Count > 0)
            {
                Response.Redirect(Request.Url.ToString());
            }

            if (ddlSelect.SelectedValue == "A")
            {
                if (chkTDSApplicable.Checked)
                {
                    if (txtTDSLedger.Text == "" || txtTDSLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select TDS Ledger...!", this.Page);
                        txtTDSLedger.Focus();
                        return;
                    }
                }
                if (chkTDSOnGst.Checked)
                {
                    if (txtTDSonGSTLedger.Text == "" || txtTDSonGSTLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select TDS On GST Account...!", this.Page);
                        txtTDSonGSTLedger.Focus();
                        return;
                    }
                }
                if (txtBankLedger.Text == "" || txtBankLedger.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Cash/Bank Ledger...!", this.Page);
                    txtBankLedger.Focus();
                    return;
                }
                if (ddlPaymentMode.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Pay Mode", this.Page);                    
                    return;
                }

                if (txtLedgerHead.Text == "" || txtLedgerHead.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Other Ledger...!", this.Page);
                    txtLedgerHead.Focus();
                    return;
                }
               
                if (txtBankLedger.Text == "" || txtBankLedger.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Cash/Bank Ledger...!", this.Page);
                    txtBankLedger.Focus();
                    return;
                }
                if (chkGST.Checked)
                {
                    if (txtCGSTLedger.Text == "" || txtCGSTLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select CGST Ledger...!", this.Page);
                        txtCGSTLedger.Focus();
                        return;
                    }
                    if (txtSgstLedger.Text == "" || txtSgstLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select SGST Ledger...!", this.Page);
                        txtSgstLedger.Focus();
                        return;
                    }
                }
                if (chkIGST.Checked)
                {
                    if (txtIGSTLedger.Text == "" || txtIGSTLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select IGST Ledger...!", this.Page);
                        txtIGSTLedger.Focus();
                        return;
                    }
                }
                if (chkTdsOnCGSTSGST.Checked)
                {
                    if (txtTDSonCGSTLedger.Text == "" || txtTDSonCGSTLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select TDS On CGST Account...!", this.Page);
                        txtTDSonCGSTLedger.Focus();
                        return;
                    }
                }
                if (chkTdsOnCGSTSGST.Checked)
                {
                    if (txtTDSonSGSTLedger.Text == "" || txtTDSonSGSTLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select TDS On SGST Account...!", this.Page);
                        txtTDSonSGSTLedger.Focus();
                        return;
                    }
                }
                if (chkSecurity.Checked)
                {
                    if (txtSecurityLedger.Text == "" || txtSecurityLedger.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select Security Ledger...!", this.Page);
                        txtSecurityLedger.Focus();
                        return;
                    }
                }

                if (chkTDSApplicable.Checked)
                {
                    if (txtTDSAmt.Text == "" || txtTDSAmt.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Amount", this.Page);
                        return;
                    }
                    if (txtTdsOnAmt.Text == "" || txtTdsOnAmt.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter TDS On Amount", this.Page);
                        return;
                    }
                    if (txtTDSPer.Text == "" || txtTDSPer.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Percentage", this.Page);
                        return;
                    }
                    if (ddlSection.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Select Tds Section.", this.Page);
                        return;
                    }
                    if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter PAN Number", this.Page);
                        return;
                    }
                    if (txtPanNo.Text.Length != 10)
                    {
                        objCommon.DisplayMessage(UPDLedger, "PAN No. should be 10 character!!", this.Page);
                        return;
                    }
                }     
            }

            this.Enable();

            ObjRPB.SERIAL_NO = Convert.ToInt32(lblSerialNo.Text);
            ObjRPB.COMPANY_NO = Convert.ToInt32(ddlSelectCompany.SelectedValue);
            ObjRPB.ACCOUNT = ddlAccount.SelectedValue;
            ObjRPB.DEPT_ID = Convert.ToInt32(ddlDeptBranch.SelectedValue);
            ObjRPB.APPROVAL_NO = txtApprovalNo.Text;
            ObjRPB.APPROVAL_DATE = Convert.ToDateTime(txtApprovalDate.Text);
            ObjRPB.APPROVED_BY = lblApprovedBy.Text;
            ObjRPB.SUPPLIER_NAME = txtServiceName.Text.ToString();

            // added by Akshay Dixit for tag user  30-08-2022

            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.SUPPLIER_NAME = ddlEmployee.SelectedItem.Text;
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                ObjRPB.SUPPLIER_NAME = ddlPayee.SelectedItem.Text;
            }
            else if (txtServiceName.Text != "" || txtServiceName.Text != string.Empty)
            {
                ObjRPB.SUPPLIER_NAME = txtServiceName.Text.ToString();
            }

            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.SUPPLIER_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                ObjRPB.SUPPLIER_ID = Convert.ToInt32(ddlPayee.SelectedValue);
            }
            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.PROVIDER_TYPE = Convert.ToInt32(ddlEmpType.SelectedValue);
            }
            else
            {
                ObjRPB.PROVIDER_TYPE = Convert.ToInt32(ddlEmpType.SelectedValue);
            }
            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.PAYEE_NATURE = 0;
            }
            else
            {
                ObjRPB.PAYEE_NATURE = Convert.ToInt32(ddlPayeeNature.SelectedValue);
            }

            ObjRPB.PAYEE_NAME_ADDRESS = txtPayeeNameAddress.Text.ToString();
            ObjRPB.NATURE_SERVICE = txtNatureOfService.Text.ToString();
            ObjRPB.GSTIN_NO = txtGSTINNo.Text.ToString();
            ObjRPB.BILL_AMT = Convert.ToDouble(txtBillAmt.Text);
            ObjRPB.GST_AMT = Convert.ToDouble(txtGSTAmount.Text);
            ObjRPB.TOTAL_BILL_AMT = Convert.ToDouble(txtTotalBillAmt.Text);
            ObjRPB.COMPANY_CODE = Session["BillComp_Code"].ToString();
            ObjRPB.NET_AMT = Convert.ToDouble(txtNetAmt.Text);
            ObjRPB.INVOICEDATE = txtInvoiceDate.Text;
            ObjRPB.INVOICENO = txtInvoiceNo.Text;
            ObjRPB.BUDGET_NO = Convert.ToInt32(ddlBudgethead.SelectedValue);
            //Added by gopal anthati on 19-08-2021
            ObjRPB.ProjectId = Convert.ToInt32(ddlSponsor.SelectedValue);
            ObjRPB.ProjectSubId = Convert.ToInt32(ddlProjSubHead.SelectedValue);

            if (txtLedgerHead.Text == "" || txtLedgerHead.Text == string.Empty)
            {
                ObjRPB.LEDGER_NO = 0;
            }
            else
            {
                ObjRPB.LEDGER_NO = Convert.ToInt32(txtLedgerHead.Text.Trim().ToString().Split('*')[1].ToString());
            }

            ObjRPB.BILL_TYPE = Convert.ToInt32(rdbBillList.SelectedValue);

            if (chkTDSApplicable.Checked)
            {
                ObjRPB.ISTDS = 1;
                ObjRPB.TDS_AMT = Convert.ToDouble(txtTDSAmt.Text);
                ObjRPB.TDS_ON_AMT = Convert.ToDouble(txtTdsOnAmt.Text);
                ObjRPB.TDS_PERCENT = Convert.ToDouble(txtTDSPer.Text);
                ObjRPB.SECTION_NO = Convert.ToInt32(ddlSection.SelectedValue);
                ObjRPB.PAN_NO = txtPanNo.Text.ToString();
            }
            else
            {
                ObjRPB.ISTDS = 0;
                ObjRPB.TDS_AMT = 0;
                ObjRPB.TDS_PERCENT = 0;
                ObjRPB.SECTION_NO = 0;
                ObjRPB.PAN_NO = string.Empty;
            }
            //Added by Gopal Anthti
            if (chkTDSOnGst.Checked)
            {
                ObjRPB.ISTDSONGST = 1;
                ObjRPB.TDS_ON_GST_AMT = Convert.ToDouble(txtTDSonGSTAmount.Text);
                ObjRPB.TDSGST_ON_AMT = Convert.ToDouble(txtTDSGSTonAmount.Text);
                ObjRPB.TDSGST_PERCENT = Convert.ToDouble(txtTDSonGSTPer.Text);
                ObjRPB.TDSGST_SECTION_NO = Convert.ToInt32(ddlTDSonGSTSection.SelectedValue);
            }
            else
            {
                ObjRPB.ISTDSONGST = 0;
                ObjRPB.TDS_ON_GST_AMT = 0;
                ObjRPB.TDSGST_ON_AMT = 0;
                ObjRPB.TDSGST_PERCENT = 0;
                ObjRPB.TDSGST_SECTION_NO = 0;
            }
            if (chkTdsOnCGSTSGST.Checked)
            {
                ObjRPB.ISTDSONCGSTSGST = 1;
                ObjRPB.TDS_ON_CGST_AMT = Convert.ToDouble(txtTDSonCGSTAmount.Text);
                ObjRPB.TDSCGST_ON_AMT = Convert.ToDouble(txtTDSCGSTonAmount.Text);
                ObjRPB.TDSCGST_PERCENT = Convert.ToDouble(txtTDSonCGSTPer.Text);
                ObjRPB.TDSCGST_SECTION_NO = Convert.ToInt32(ddlTDSonCGSTSection.SelectedValue);

                ObjRPB.TDS_ON_SGST_AMT = Convert.ToDouble(txtTDSonSGSTAmount.Text);
                ObjRPB.TDSSGST_ON_AMT = Convert.ToDouble(txtTDSSGSTonAmount.Text);
                ObjRPB.TDSSGST_PERCENT = Convert.ToDouble(txtTDSonSGSTPer.Text);
                ObjRPB.TDSSGST_SECTION_NO = Convert.ToInt32(ddlTDSonSGSTSection.SelectedValue);
            }
            else
            {
                ObjRPB.ISTDSONCGSTSGST = 0;
                ObjRPB.TDS_ON_CGST_AMT = 0;
                ObjRPB.TDSCGST_ON_AMT = 0;
                ObjRPB.TDSCGST_PERCENT = 0;
                ObjRPB.TDSCGST_SECTION_NO = 0;

                ObjRPB.TDS_ON_SGST_AMT = 0;
                ObjRPB.TDSSGST_ON_AMT = 0;
                ObjRPB.TDSSGST_PERCENT = 0;
                ObjRPB.TDSSGST_SECTION_NO = 0;
            }
            if (chkSecurity.Checked)
            {
                ObjRPB.ISSECURITY = 1;
                ObjRPB.SECURITY_AMT = Convert.ToDouble(txtSecurityAmt.Text); 
            }
            else
            {
                ObjRPB.ISSECURITY = 0;
                ObjRPB.SECURITY_AMT = 0;
            }
            //Added by vijay andoju on 08092020
            if (chkIGST.Checked)
            {
                ObjRPB.ISIGST = 1;
                ObjRPB.IGST_AMT = Convert.ToDouble(txtIgstAmount.Text);
                ObjRPB.IGST_PER = Convert.ToDouble(txtIgstPer.Text);
                ObjRPB.IGST_SECTION = Convert.ToInt32(ddlIgstSection.SelectedValue);

            }
            else
            {
                ObjRPB.ISIGST = 0;
                ObjRPB.IGST_AMT = 0;
                ObjRPB.IGST_PER = 0;
                ObjRPB.IGST_SECTION = 0;
            }
            if (chkGST.Checked)
            {
                ObjRPB.ISGST = 1;
                ObjRPB.CGST_AMT = Convert.ToDouble(txtCgstAmount.Text);
                ObjRPB.CGST_PER = Convert.ToDouble(txtCGSTPER.Text);
                ObjRPB.CGST_SECTION = Convert.ToInt32(ddlCgstSection.SelectedValue);

                ObjRPB.SGST_AMT = Convert.ToDouble(txtSgstAmount.Text);
                ObjRPB.SGST_PER = Convert.ToDouble(txtSgstPer.Text);
                ObjRPB.SGST_SECTION = Convert.ToInt32(ddlSgstSection.SelectedValue);
            }
            else
            {
                ObjRPB.ISGST = 0;
                ObjRPB.CGST_AMT = 0;
                ObjRPB.CGST_PER = 0;
                ObjRPB.CGST_PER = 0;

                ObjRPB.SGST_AMT = 0;
                ObjRPB.SGST_PER = 0;
                ObjRPB.SGST_SECTION = 0;
            }



            ObjRPB.REMARK = txtRemark.Text.ToString();

            ObjRPB.PAYMENT_MODE = Convert.ToChar(ddlPaymentMode.SelectedValue);

            string CHEQUENO = string.Empty;
            if (txtChqNo2.Text.ToString().Trim() == "" || txtChqNo2.Text.ToString() == string.Empty)
            {
                CHEQUENO = "0";
            }
            else
            {
                CHEQUENO = txtChqNo2.Text.ToString().Trim();
            }

           


            string strCombineSecond = stringCombine();
            string status = "", remarks = "";
            int intUpd = 0;

            if (strCombineSecond != ViewState["strCombine"].ToString())
            {
                int objret = objRPBController.UpdateBillByAuthority(ObjRPB, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["colcode"].ToString()));
                if (objret == 2)
                {
                    intUpd = 1;
                }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "Cannot be Approve or Forward, something is wrong..!", this.Page);
                    intUpd = 0;
                    return;
                }
                status = ddlSelect.SelectedValue;
                remarks = txtApproveRemarks.Text;
            }
            else
            {
                intUpd = 1;
                status = ddlSelect.SelectedValue;
                remarks = txtApproveRemarks.Text;
            }

            if (intUpd == 1)
            {
                string Transdate = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");
                int billno = Convert.ToInt32(Session["BillNo"].ToString());
                int TDSId = 0, bankid = 0, Cgst = 0, Sgst = 0, Igst = 0;
                if (txtTDSLedger.Text == "" || txtTDSLedger.Text == string.Empty)
                {
                    TDSId = 0;
                }
                else
                {
                    TDSId = Convert.ToInt32(txtTDSLedger.Text.ToString().Split('*')[1].ToString());
                }
                if (txtBankLedger.Text == "" || txtBankLedger.Text == string.Empty)
                {
                    bankid = 0;
                }
                else
                {
                    bankid = Convert.ToInt32(txtBankLedger.Text.ToString().Split('*')[1].ToString());
                }

                //Added by vijay on18092020
                if (txtCGSTLedger.Text == "" || txtBankLedger.Text == string.Empty)
                {
                    Cgst = 0;
                }
                else
                {
                    Cgst = Convert.ToInt32(txtCGSTLedger.Text.ToString().Split('*')[1].ToString());
                }
                if (txtSgstLedger.Text == "" || txtBankLedger.Text == string.Empty)
                {
                    Sgst = 0;
                }
                else
                {
                    Sgst = Convert.ToInt32(txtSgstLedger.Text.ToString().Split('*')[1].ToString());
                }
                if (txtIGSTLedger.Text == "" || txtBankLedger.Text == string.Empty)
                {
                    Igst = 0;
                }
                else
                {
                    Igst = Convert.ToInt32(txtIGSTLedger.Text.ToString().Split('*')[1].ToString());
                }
                int TDSonGSTId, TDSonCGSTId, TDSonSGSTId, SECURITYID;
                if (txtTDSonGSTLedger.Text == "" || txtTDSonGSTLedger.Text == string.Empty)
                {
                    TDSonGSTId = 0;
                }
                else
                {
                    TDSonGSTId = Convert.ToInt32(txtTDSonGSTLedger.Text.ToString().Split('*')[1].ToString());
                }
                if (txtTDSonCGSTLedger.Text == "" || txtTDSonCGSTLedger.Text == string.Empty)
                {
                    TDSonCGSTId = 0;
                }
                else
                {
                    TDSonCGSTId = Convert.ToInt32(txtTDSonCGSTLedger.Text.ToString().Split('*')[1].ToString());
                }
                if (txtTDSonSGSTLedger.Text == "" || txtTDSonSGSTLedger.Text == string.Empty)
                {
                    TDSonSGSTId = 0;
                }
                else
                {
                    TDSonSGSTId = Convert.ToInt32(txtTDSonSGSTLedger.Text.ToString().Split('*')[1].ToString());
                }
                if (txtSecurityLedger.Text == "" || txtSecurityLedger.Text == string.Empty)
                {
                    SECURITYID = 0;
                }
                else
                {
                    SECURITYID = Convert.ToInt32(txtSecurityLedger.Text.ToString().Split('*')[1].ToString());
                }


                string tranns_Narration = txtNarration.Text;
                //  int updret = objRPBController.UpdateTransDetails(billno, Transdate, TDSId, bankid, tranns_Narration);
                int updret = objRPBController.UpdateTransDetails(billno, Transdate, TDSId, bankid, tranns_Narration, Cgst, Sgst, Igst, TDSonGSTId, TDSonCGSTId, TDSonSGSTId, SECURITYID, CHEQUENO);


                 int ret = 0;
                string IsSigleAuth = objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='IS SINGLE AUTHORITY'");

                if (IsSigleAuth == "Y")
                {
                    string compcode = ddlCompAccount.SelectedItem.Text.ToString().Split('*')[2].ToString();
                     ret = objRPBController.ApprovePendingBillByCaseWorker(ObjRPB, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["colcode"].ToString()), status, remarks, compcode);
                }
                else
                {
                    ret = objRPBController.ApprovePendingBillByCaseWorker(ObjRPB, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["colcode"].ToString()), status, remarks);

                }              

                if (ret == 1)
                {
                    upd_ModalPopupExtender1.Hide();
                    objCommon.DisplayMessage(UPDLedger, "Bill Forwarded to Next Authority Successfully!", this.Page);

                    BindPendingBillList();
                    pnlBillDetails.Visible = false;
                    pnlBillList.Visible = true;
                    dvbuttons.Visible = false;
                    txtApproveRemarks.Text = string.Empty;
                }
                else if (ret == 2)
                {
                    BillVouchercreation();

                    objCommon.DisplayMessage(UPDLedger, "Record Approved Successfully!", this.Page);

                    upd_ModalPopupExtender1.Show();

                   // Session["comp_code"] = null;
                    

                    BindPendingBillList();
                    pnlBillDetails.Visible = false;
                    pnlBillList.Visible = true;
                    dvbuttons.Visible = false;
                    txtApproveRemarks.Text = string.Empty;

                }
                else if (ret == 3)
                {
                    upd_ModalPopupExtender1.Hide();

                    BindPendingBillList();
                    pnlBillDetails.Visible = false;
                    pnlBillList.Visible = true;
                    dvbuttons.Visible = false;
                    txtApproveRemarks.Text = string.Empty;

                    objCommon.DisplayMessage(UPDLedger, "Bill Return Successfully!", this.Page);
                }
                
            }
           
            //if (ViewState["Edit"].ToString() == "Y")
            //{
            //    ObjRPB.RAISE_PAY_NO = Convert.ToInt32(ViewState["RaisePayNo"].ToString());
            //}
            //else
            //{
            //    ObjRPB.RAISE_PAY_NO = 0;
            //}
            txtGSTAmount.Text = string.Empty;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_RasingPaymentBillApproval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelect.SelectedValue == "A")
        {
            dvLedgers.Visible = true;
            if (chkTDSApplicable.Checked)
            {
                tdslabel.Visible = true;
                tdstextbox.Visible = true;
            }
            else
            {
                tdslabel.Visible = false;
                tdstextbox.Visible = false;
            }

            txtTransDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
        else
        {
            lblReturn.Visible = true;
            lblRemark.Visible = false;
            dvLedgers.Visible = false;
            tdslabel.Visible = false;
            tdstextbox.Visible = false;
        }
    }


    private void BillVouchercreation()
    {
        int rows = 0;
        XmlDocument objXMLDoc = new XmlDocument();

        ViewState["VoucherNo"] = string.Empty;

        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Convert.ToInt32(ddlSelectCompany.SelectedValue).ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            //Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            //Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
        }
        dtr.Close();
        //ReadXML("N");
        XmlDeclaration xmlDeclaration = objXMLDoc.CreateXmlDeclaration("1.0", null, null);

        // Create the root element
        XmlElement rootNode = objXMLDoc.CreateElement("tables");
        objXMLDoc.InsertBefore(xmlDeclaration, objXMLDoc.DocumentElement);
        objXMLDoc.AppendChild(rootNode);
        try
        {
            rows = 1;

            if (chkTDSApplicable.Checked)
            {
                rows = 2;

            }
          

            HiddenField hdnparty = new HiddenField();
           
            for (int i = 0; i < rows; i++)
            {
                XmlElement objElement = objXMLDoc.CreateElement("Table");
                XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
                XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");
                XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
                XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");
                XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
                XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
                XmlElement Section = objXMLDoc.CreateElement("SECTION");
                XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
                XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
                XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");

                XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
                XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
                XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");
                XmlElement IsTDSApplicable = objXMLDoc.CreateElement("IsTDSApplicable");

                XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
                XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
                XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
                XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

                XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");

                XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
                XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
                XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");
               
                XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
                XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
                XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

                XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");
                XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");
                

                IsGSTApplicable.InnerText = "0";
                TDSSection.InnerText = "0";
                TDSAMOUNT.InnerText = "0";
                TDPersentage.InnerText = "0";
                
                IsTDSonGSTApplicable.InnerText = "0";

                IsTDSonCGSTSGSTApplicable.InnerText = "0";
                IsSecurityApplicable.InnerText = "0";


                //ViewState["Amount"] = txtTotalBillAmt.Text.Trim().ToString();

                ViewState["Amount"] = txtNetAmt.Text.Trim().ToString();
                ViewState["TDSAmount"] = txtTDSAmt.Text.Trim().ToString();
                ViewState["TDSonCGSTAmount"] = txtTDSonCGSTAmount.Text.Trim().ToString();
                ViewState["TDSonSGSTAmount"] = txtTDSonSGSTAmount.Text.Trim().ToString();
                if(i==0)               
                {
                    Section.InnerText = "0";
                    IsTDSApplicable.InnerText = "0";
                    //ViewState["Amount"] = txtTotalBillAmt.Text.Trim().ToString();
                    ViewState["Amount"] = txtNetAmt.Text.Trim().ToString();
                    ViewState["TDSAmount"] = 0;

                    TDSSection.InnerText ="0";
                    TDSAMOUNT.InnerText = "0.00";
                    TDPersentage.InnerText = "0.00";
                   
                    AmtWithoutGST.InnerText = "0";
                }
                else if (chkTDSApplicable.Checked)
                {
                    Section.InnerText = Convert.ToInt32(ddlSection.SelectedValue).ToString();
                    IsTDSApplicable.InnerText = "1";
                    //  ViewState["Amount"] = txtTotalBillAmt.Text.Trim().ToString();
                    ViewState["Amount"] = txtNetAmt.Text.Trim().ToString();
                    ViewState["TDSAmount"] = txtTDSAmt.Text.Trim().ToString();
                    AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();

                    TDSSection.InnerText = ddlSection.SelectedValue.ToString();
                    TDSAMOUNT.InnerText = txtTdsOnAmt.Text.ToString();
                    TDPersentage.InnerText = txtTDSPer.Text.ToString();
                }
                GSTPercent.InnerText = "0";
                SUBTR_NO.InnerText = "0";
                if (txtTransDate.Text.ToString().Trim() != "")
                {
                    DateTime TranDate = Convert.ToDateTime(txtTransDate.Text);
                    TRANSACTION_DATE.InnerText = TranDate.ToString("dd-MMM-yyyy");
                    ViewState["TRANDATE"] = Convert.ToDateTime(txtTransDate.Text);
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Enter The Transaction Date.", this);
                    txtTransDate.Focus();
                    return;
                }

                XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
                TRANSACTION_TYPE.InnerText = "P";
                HiddenField hdnOpParty = new HiddenField();
                if (i == 0)
                {
                    hdnOpParty.Value = Convert.ToInt32(txtBankLedger.Text.Split('*')[1]).ToString();
                }
                else
                {
                    hdnOpParty.Value = Convert.ToInt32(txtLedgerHead.Text.Split('*')[1]).ToString();
                }
                if (hdnOpParty != null)
                {
                    OPARTY.InnerText = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                    ViewState["OPartyNo"] = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                }


                if (i == 0)
                {
                    hdnparty.Value = Convert.ToInt32(txtLedgerHead.Text.Split('*')[1]).ToString();
                }
                else if (chkTDSApplicable.Checked)
                {

                    hdnparty.Value = Convert.ToInt32(txtTDSLedger.Text.Split('*')[1] == "" ? "0" : txtTDSLedger.Text.Split('*')[1]).ToString();
                    ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + hdnparty.Value;
                }
              
                if (hdnparty != null)
                {
                    PARTY_NO.InnerText = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                    ViewState["PartyNo"] = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                }
                else { return; }

                if (i == 0)
                {
                    TRAN.InnerText = "Dr";
                    AMOUNT.InnerText = txtBillAmt.Text.Trim().ToString();
                  //  AMOUNT.InnerText = txtNetAmt.Text.Trim().ToString();
                  
                }
                else
                {
                    if (chkTDSApplicable.Checked)
                    {
                        TRAN.InnerText = "Cr";
                      
                        AMOUNT.InnerText = txtTDSAmt.Text.Trim().ToString();
                      
                    }
                    //if (chkGST.Checked)
                    //{
                    //    TRAN.InnerText = "Dr";

                    //    AMOUNT.InnerText = txtTDSAmt.Text.Trim().ToString();
                    //}
                    //if (chkIGST.Checked)
                    //{
                    //}
                   }



                XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
                DEGREE_NO.InnerText = "0";

                XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");
                TRANSFER_ENTRY.InnerText = "0";
                XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
                CBTYPE_STATUS.InnerText = "H";
                XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
                CBTYPE.InnerText = "TF";
                XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");
                RECIEPT_PAYMENT_FEES.InnerText = "P";
                XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
                REC_NO.InnerText = "0";
                XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
                XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");

              //  CHQ_NO.InnerText = "0";


                if (txtChqNo2.Text.ToString().Trim() == string.Empty)
                {

                    CHQ_NO.InnerText = "0";
                   
                }
                else
                {
                    CHQ_NO.InnerText = txtChqNo2.Text.Trim();
                }





                ViewState["CHQ_NO"] = "0";
                CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");
                ViewState["CHQ_DATE"] = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

                XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
                CHALLAN.InnerText = "false";
                XmlElement CAN = objXMLDoc.CreateElement("CAN");
                CAN.InnerText = "false";
                XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
                DCR_NO.InnerText = "0";

                //Commented and Add by Nakul Chawre @28052016 to add cost center
                XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
                CC_ID.InnerText = "0";

                XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
                if (ddlBudgethead.SelectedValue == "0")
                {
                    BudgetNo.InnerText = "0";
                }
                else
                {
                    BudgetNo.InnerText = ddlBudgethead.SelectedValue;
                }

                //Added by Nokhlal Kumar As per requirement to add TDS
              

                //Added by Nokhlal Kumar As per requirement as on Date :- 2018-11-16
                XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
                if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
                {
                    PARTY_NAME.InnerText = "-";
                }
                else
                {
                    PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
                }

                XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
                if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
                {
                    PAN_NO.InnerText = "-";
                }
                else
                {
                    PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
                }

                XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
                if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
                {
                    NATURE_SERVICE.InnerText = "-";
                }
                else
                {
                    NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
                }

                XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
                CASH_BANK_NO.InnerText = "0";
                XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
                ADVANCE_REFUND_NONE.InnerText = "N";
                XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
                PAGENO.InnerText = "0";
                XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");

                string narration = txtNarration.Text.Replace("'", "''");
                if (txtNarration.Text != string.Empty)
                {
                    PARTICULARS.InnerText = narration;
                }
                else
                {
                    PARTICULARS.InnerText = "-";
                }

                XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
                COLLEGE_CODE.InnerText = Session["colcode"].ToString();

                XmlElement USER = objXMLDoc.CreateElement("USER");
                USER.InnerText = Session["userno"].ToString().Trim();

                XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
                CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");
                XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
                XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");

                XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
                XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

                XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
                XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
                DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();


                XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
                IsIGSTApplicable.InnerText = "0";
                XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
                XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
                XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

                IGSTPER.InnerText ="0";
                IGSTAMOUNT.InnerText = "0";
                IGSTonAmount.InnerText ="0";
                XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
                XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
                XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
                XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
                XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
                XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

                XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
                SGSTApplicable.InnerText =  "0";
                CGSTper.InnerText = "0";
                CGSTamount.InnerText = "0";
                CGSTonamount.InnerText = "0";


                SGSTper.InnerText = "0";
                SGSTamount.InnerText = "0";
                SGSTonamount.InnerText = "0";

                TDSonGSTSection.InnerText = "0";
                TDSGSTonAMOUNT.InnerText = "0";
                TDSonGSTPersentage.InnerText = "0";

                TDSonCGSTSection.InnerText = "0";
                TDSCGSTonAMOUNT.InnerText = "0";
                TDSonCGSTPercentage.InnerText = "0";

                TDSonSGSTSection.InnerText = "0";
                TDSSGSTonAMOUNT.InnerText = "0";
                TDSonSGSTPercentage.InnerText = "0";

                SecurityAmt.InnerText = "0";
                

                XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

                if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
                {
                    GSTIN_NO.InnerText = txtGSTINNo.Text;
                }
                GSTIN_NO.InnerText = "-";


                string voucherNo1 = string.Empty;

                if (objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
                {
                    voucherNo1 = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='P'");

                    VOUCHER_NO.InnerText = voucherNo1;
                    ViewState["VoucherNo"] = voucherNo1;

                    STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherNo1;//  txtVoucherNo.Text.ToString().Trim();
                }

                STR_CB_VOUCHER_NO.InnerText = StrVno;

                ProjectId.InnerText = "0";

                HiddenField hdnSubproject = new HiddenField();
                hdnSubproject.Value = "";

                ProjectSubId.InnerText = hdnSubproject.Value == "" ? "0" : hdnSubproject.Value;

                BILL_ID.InnerText = lblSerialNo.Text.ToString();
               

                objElement.AppendChild(SUBTR_NO);
                objElement.AppendChild(TRANSACTION_DATE);
                objElement.AppendChild(TRANSACTION_TYPE);
                objElement.AppendChild(OPARTY);
                objElement.AppendChild(PARTY_NO);
                objElement.AppendChild(TRAN);
                objElement.AppendChild(AMOUNT);
                objElement.AppendChild(DEGREE_NO);
                objElement.AppendChild(TRANSFER_ENTRY);
                objElement.AppendChild(CBTYPE_STATUS);
                objElement.AppendChild(CBTYPE);
                objElement.AppendChild(RECIEPT_PAYMENT_FEES);
                objElement.AppendChild(REC_NO);
                objElement.AppendChild(CHQ_NO);
                objElement.AppendChild(CHQ_DATE);
                objElement.AppendChild(CHALLAN);
                objElement.AppendChild(CAN);
                objElement.AppendChild(DCR_NO);
                //objElement.AppendChild(IDF_NO);
                objElement.AppendChild(CC_ID);
                objElement.AppendChild(BudgetNo);
                objElement.AppendChild(IsTDSApplicable);
                objElement.AppendChild(IsTDSonGSTApplicable);
                objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
                objElement.AppendChild(IsSecurityApplicable);
                objElement.AppendChild(AmtWithoutGST);
                objElement.AppendChild(GSTPercent);
                objElement.AppendChild(IsGSTApplicable);
                objElement.AppendChild(Section);
                objElement.AppendChild(PARTY_NAME);
                objElement.AppendChild(PAN_NO);
                objElement.AppendChild(NATURE_SERVICE);
                objElement.AppendChild(CASH_BANK_NO);
                objElement.AppendChild(ADVANCE_REFUND_NONE);
                objElement.AppendChild(PAGENO);
                objElement.AppendChild(PARTICULARS);
                objElement.AppendChild(COLLEGE_CODE);
                objElement.AppendChild(USER);
                objElement.AppendChild(CREATED_MODIFIED_DATE);
                objElement.AppendChild(VOUCHER_NO);
                objElement.AppendChild(STR_VOUCHER_NO);
                objElement.AppendChild(STR_CB_VOUCHER_NO);
                objElement.AppendChild(ProjectId);
                objElement.AppendChild(ProjectSubId);
                objElement.AppendChild(BILL_ID);

                objElement.AppendChild(TDSSection);
                objElement.AppendChild(TDSAMOUNT);
                objElement.AppendChild(TDPersentage);
                objElement.AppendChild(TDSonGSTSection);
                objElement.AppendChild(TDSGSTonAMOUNT);
                objElement.AppendChild(TDSonGSTPersentage);

                objElement.AppendChild(TDSonCGSTSection);
                objElement.AppendChild(TDSCGSTonAMOUNT);
                objElement.AppendChild(TDSonCGSTPercentage);

                objElement.AppendChild(TDSonSGSTSection);
                objElement.AppendChild(TDSSGSTonAMOUNT);
                objElement.AppendChild(TDSonSGSTPercentage);

                objElement.AppendChild(SecurityAmt);

                
                objElement.AppendChild(DepartmentId);

                objElement.AppendChild(IsIGSTApplicable);
                objElement.AppendChild(IGSTAMOUNT);
                objElement.AppendChild(IGSTPER);
                objElement.AppendChild(IGSTonAmount);
                objElement.AppendChild(CGSTamount);
                objElement.AppendChild(CGSTper);
                objElement.AppendChild(CGSTonamount);
                objElement.AppendChild(SGSTamount);
                objElement.AppendChild(SGSTper);
                objElement.AppendChild(SGSTonamount);
                objElement.AppendChild(SGSTApplicable);
                objElement.AppendChild(GSTIN_NO);


                objXMLDoc.DocumentElement.AppendChild(objElement);
                //WriteXML(objXMLDoc);
                //string path = "D:\\Account\\VSS_ACC_Finance\\UAIMS\\PresentationLayer\\ACCOUNT\\ArrangeData.xml";
            }
            if (chkIGST.Checked)
            {
                objXMLDoc = AddIgsTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            }
            if (chkGST.Checked)
            {
                objXMLDoc = AddSGsttable(objXMLDoc, ViewState["VoucherNo"].ToString());
                objXMLDoc = AddCGsttable(objXMLDoc, ViewState["VoucherNo"].ToString());
            }
            if (chkTDSOnGst.Checked)
            {
                objXMLDoc = AddTDSonGSTTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            }
            if (chkTdsOnCGSTSGST.Checked)
            {
                objXMLDoc = AddTDSonCGSTTable(objXMLDoc, ViewState["VoucherNo"].ToString());
                objXMLDoc = AddTDSonSGSTTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            }
            if (chkSecurity.Checked)
            {
                objXMLDoc = AddSecurityTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            }

            objXMLDoc = ConsolidateTransactionEntry1(objXMLDoc, ViewState["VoucherNo"].ToString());
            isAllreadySet = "";

            string IsModify = string.Empty;
            int VoucherSqn = 0;
            int voucherno;
            IsModify = "N";
            VoucherSqn = 0;
            //voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify);
            voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["BillComp_Code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), "P");

            Session["vchno"] = voucherno.ToString();

            if(ddlBudgethead.SelectedIndex > 0)
            {
                objPC1.AddBudgetTransaction(Session["comp_code"].ToString().Trim(), Convert.ToString(voucherno), "P");
            }



            DataSet dsResult = objPC1.GetTransactionResult(voucherno, Session["BillComp_Code"].ToString(), "P");
            if (objCommon.LookUp("acc_" + Session["BillComp_Code"].ToString() + "_config", "PARAMETER", "CONFIGDESC='ENABLE CHEQUE PRINTING'") == "N")
            {
                btnchequePrint.Visible = false;
            }
            else
            {
                string tranno = dsResult.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                string partyName = dsResult.Tables[0].Rows[0]["LEDGER"].ToString();
                partyName = ViewState["BankName"].ToString();
                string chqno2 = "0";
                string amount = string.Empty;
                if (Convert.ToDecimal(dsResult.Tables[0].Rows[0]["DEBIT"].ToString()) > 0)
                    amount = dsResult.Tables[0].Rows[0]["DEBIT"].ToString();
                else
                    amount = dsResult.Tables[0].Rows[0]["CREDIT"].ToString();

                string CHQ_NO = dsResult.Tables[0].Rows[0]["CHQ_NO"].ToString();

                btnchequePrint.Attributes.Add("onclick", "ShowChequePrintingTran('" + chqno2.ToString() + "','" + tranno + "','" + partyName + "','" + amount + "','0','" + CHQ_NO + "')");
            }

            if (dsResult != null)
            {
                if (dsResult.Tables[0].Rows.Count != 0)
                {
                    //lblMsg.Text = "";
                    //rowMsg.Style["Display"] = "none";
                    lvGrp.DataSource = dsResult.Tables[0];
                    lvGrp.DataBind();
                    //lblTotal.Text = "0.00";
                    // lblTotalCredit.Text = "0.00";
                    //lblTotalDebit.Text = "0.00";
                    if (isvoucher_Cheque_Print == "Y")
                        upd_ModalPopupExtender1.Show();
                    objPC1.UpdateBalanceAllLedger();
                    //Added by Nakul Chawre
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Performed Successfully", this.Page);
                }

            }
            txtApproveRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Performed Successfully", this.Page);
        }

    }

    private XmlDocument ConsolidateTransactionEntry1(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "1";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        TDSonCGSTSection.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTPercentage.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTPercentage.InnerText = "0";
        SecurityAmt.InnerText = "0";

        IsTDSonGSTApplicable.InnerText = "0";
        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";
      
            IsGSTApplicable.InnerText = "0";
            Section.InnerText = "0";
       
        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();
       // OPARTY.InnerText = ViewState["OPartyNo"].ToString(); 
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        PARTY_NO.InnerText = Convert.ToInt32(txtBankLedger.Text.Split('*')[1]).ToString();
        if (chkTDSApplicable.Checked)
        {
            //AMOUNT.InnerText = (Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(ViewState["TDSAmount"].ToString())).ToString();
            AMOUNT.InnerText = Convert.ToDouble(ViewState["Amount"].ToString()).ToString();
            AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
            GSTPercent.InnerText = txtTDSPer.Text.ToString();
            TDSSection.InnerText = ddlSection.SelectedValue.ToString();
            TDSAMOUNT.InnerText = ViewState["TDSAmount"].ToString();
            TDPersentage.InnerText = txtTDSPer.Text.ToString();
        }
        else
        {
            AMOUNT.InnerText = Convert.ToDouble(ViewState["Amount"].ToString()).ToString();
            AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
            GSTPercent.InnerText = "0";
        }
        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

      //  CHQ_NO.InnerText = "0";

        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";
          
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }


        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();



        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = chkIGST.Checked == true ? "1" : "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = chkIGST.Checked == true ? txtIgstPer.Text : "0";
        IGSTAMOUNT.InnerText = chkIGST.Checked == true ? txtIgstAmount.Text : "0";
        IGSTonAmount.InnerText = chkIGST.Checked == true ? txtBillAmt.Text : "0";
        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = chkGST.Checked == true ? "1" : "0";
        CGSTper.InnerText = chkGST.Checked == true ? txtCGSTPER.Text : "0";
        CGSTamount.InnerText = chkGST.Checked == true ? txtCgstAmount.Text : "0";
        CGSTonamount.InnerText = chkGST.Checked == true ? txtBillAmt.Text : "0";


        SGSTper.InnerText = chkGST.Checked == true ? txtSgstPer.Text : "0";
        SGSTamount.InnerText = chkGST.Checked == true ? txtSgstAmount.Text : "0";
        SGSTonamount.InnerText = chkGST.Checked == true ? txtBillAmt.Text : "0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);
        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);
        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddIgsTable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";
        IsTDSonGSTApplicable.InnerText = "0";

        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";

        TDSonCGSTSection.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTPercentage.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTPercentage.InnerText = "0";
        SecurityAmt.InnerText = "0";
       
            IsGSTApplicable.InnerText = "0";
           
            Section.InnerText = "0";
       
        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
       OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();
    
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Dr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        PARTY_NO.InnerText = Convert.ToInt32(txtIGSTLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
        DEGREE_NO.InnerText = "0";
        AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
        GSTPercent.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

       // CHQ_NO.InnerText = "0";

        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";
           
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }


        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();

        AMOUNT.InnerText = Convert.ToDouble(txtIgstAmount.Text).ToString();

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = chkIGST.Checked == true ? "1" : "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = chkIGST.Checked == true ? txtIgstPer.Text : "0";
        IGSTAMOUNT.InnerText = chkIGST.Checked == true ? txtIgstAmount.Text : "0";
        IGSTonAmount.InnerText = chkIGST.Checked == true ? txtBillAmt.Text : "0";

        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText =  "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText =  "0";


        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText =  "0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);
        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);
        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddCGsttable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        IsTDSonGSTApplicable.InnerText = "0";
        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";

        TDSonCGSTSection.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTPercentage.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTPercentage.InnerText = "0";
        SecurityAmt.InnerText = "0";
 
        IsGSTApplicable.InnerText = "1";
            Section.InnerText = "0";
       

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();
       
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Dr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        PARTY_NO.InnerText = Convert.ToInt32(txtCGSTLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
            AMOUNT.InnerText =  Convert.ToDouble(txtCgstAmount.Text).ToString();
            AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
            GSTPercent.InnerText = "0";
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";
        
        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

       // CHQ_NO.InnerText = "0";

        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";
           
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }


        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();



        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";
        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = chkGST.Checked == true ? "1" : "0";
        CGSTper.InnerText = chkGST.Checked == true ? txtCGSTPER.Text : "0";
        CGSTamount.InnerText = chkGST.Checked == true ? txtCgstAmount.Text : "0";
        CGSTonamount.InnerText = chkGST.Checked == true ? txtBillAmt.Text : "0";


        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);
        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);
        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddSGsttable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        IsTDSonGSTApplicable.InnerText = "0";
        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";

        TDSonCGSTSection.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTPercentage.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTPercentage.InnerText = "0";
        SecurityAmt.InnerText = "0";

            IsGSTApplicable.InnerText = "1";
            Section.InnerText = ddlSection.SelectedValue;
      
        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();
     
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Dr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        PARTY_NO.InnerText = Convert.ToInt32(txtSgstLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
        AMOUNT.InnerText = Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text).ToString(); ;
            AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
            GSTPercent.InnerText = txtSgstPer.Text.ToString();
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";
      
       
        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

       // CHQ_NO.InnerText = "0";

        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";
            ViewState["CHQ_NO"] = "0";
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }


        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();



        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = chkIGST.Checked == true ? "1" : "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText ="0";
        IGSTonAmount.InnerText =  "0";
        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = chkGST.Checked == true ? "1" : "0";

        SGSTper.InnerText = chkGST.Checked == true ? txtSgstPer.Text : "0";
        SGSTamount.InnerText = chkGST.Checked == true ? txtSgstAmount.Text : "0";
        SGSTonamount.InnerText = chkGST.Checked == true ? txtBillAmt.Text : "0";

        CGSTper.InnerText = "0";
        CGSTamount.InnerText =  "0";
        CGSTonamount.InnerText ="0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);

        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);
        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddTDSonGSTTable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");
        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");
        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        TDSonCGSTSection.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTPercentage.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTPercentage.InnerText = "0";
        SecurityAmt.InnerText = "0";

        IsTDSonGSTApplicable.InnerText = chkTDSOnGst.Checked == true ? "1" : "0";       

        IsGSTApplicable.InnerText = "0";

        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();

        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        PARTY_NO.InnerText = Convert.ToInt32(txtTDSonGSTLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
        DEGREE_NO.InnerText = "0";
        AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
        GSTPercent.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

        //CHQ_NO.InnerText = "0";

        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";
           
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }


        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();

        //AMOUNT.InnerText = Convert.ToDouble(txtTdsOnAmt.Text).ToString();
        AMOUNT.InnerText = Convert.ToDouble(txtTDSonGSTAmount.Text).ToString();
        ViewState["TDSonGST"] = Convert.ToDouble(txtTDSonGSTAmount.Text).ToString();

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText =  "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText =  "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText =  "0";

        TDSonGSTPersentage.InnerText = chkTDSOnGst.Checked == true ? txtTDSonGSTPer.Text : "0";
        TDSGSTonAMOUNT.InnerText = chkTDSOnGst.Checked == true ? txtTDSGSTonAmount.Text : "0";
        TDSonGSTSection.InnerText = chkTDSOnGst.Checked == true ? ddlSection.SelectedValue : "0";

        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";


        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);

        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);
        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddTDSonCGSTTable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");       


        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = chkTdsOnCGSTSGST.Checked == true ? "1" : "0";
        IsTDSonGSTApplicable.InnerText ="0";

        IsGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();

        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        PARTY_NO.InnerText = Convert.ToInt32(txtTDSonCGSTLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
        DEGREE_NO.InnerText = "0";
        AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
        GSTPercent.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

       // CHQ_NO.InnerText = "0";

        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";
          
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }


        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();
        
        AMOUNT.InnerText = Convert.ToDouble(txtTDSonCGSTAmount.Text).ToString();
        ViewState["TDSonCGST"] = Convert.ToDouble(txtTDSonCGSTAmount.Text).ToString();

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";

        TDSonGSTPersentage.InnerText =  "0";
        TDSGSTonAMOUNT.InnerText =  "0";
        TDSonGSTSection.InnerText = "0";

        TDSonCGSTPercentage.InnerText = chkTdsOnCGSTSGST.Checked == true ? txtTDSonCGSTPer.Text : "0";
        TDSCGSTonAMOUNT.InnerText = chkTdsOnCGSTSGST.Checked == true ? txtTDSCGSTonAmount.Text : "0";
        TDSonCGSTSection.InnerText = chkTdsOnCGSTSGST.Checked == true ? ddlTDSonCGSTSection.SelectedValue : "0";

        TDSonSGSTPercentage.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";

        SecurityAmt.InnerText = "0";

        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";


        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);

        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);


        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddTDSonSGSTTable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");


        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = chkTdsOnCGSTSGST.Checked == true ? "1" : "0";
        IsTDSonGSTApplicable.InnerText = "0";

        IsGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();

        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        PARTY_NO.InnerText = Convert.ToInt32(txtTDSonSGSTLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
        DEGREE_NO.InnerText = "0";
        AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
        GSTPercent.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

       // CHQ_NO.InnerText = "0";
        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {
            CHQ_NO.InnerText = "0";
        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }

        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();

        AMOUNT.InnerText = Convert.ToDouble(txtTDSonSGSTAmount.Text).ToString();
        ViewState["TDSonSGST"] = Convert.ToDouble(txtTDSonSGSTAmount.Text).ToString();

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";

        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";

        TDSonCGSTPercentage.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTSection.InnerText = "0";

        TDSonSGSTPercentage.InnerText = chkTdsOnCGSTSGST.Checked == true ? txtTDSonSGSTPer.Text : "0";
        TDSSGSTonAMOUNT.InnerText = chkTdsOnCGSTSGST.Checked == true ? txtTDSSGSTonAmount.Text : "0";
        TDSonSGSTSection.InnerText = chkTdsOnCGSTSGST.Checked == true ? ddlTDSonSGSTSection.SelectedValue : "0";

        SecurityAmt.InnerText = "0";

        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";


        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);

        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);


        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument AddSecurityTable(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "0";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");


        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsTDSonGSTApplicable.InnerText = "0";

        IsGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = chkSecurity.Checked == true ? "1" : "0";

        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.ToString().Split('*')[1]).ToString();

        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "P";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        PARTY_NO.InnerText = Convert.ToInt32(txtSecurityLedger.Text.Split('*')[1]).ToString();
        ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + PARTY_NO.InnerText;
        DEGREE_NO.InnerText = "0";
        AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
        GSTPercent.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

       // CHQ_NO.InnerText = "0";
        if (txtChqNo2.Text.ToString().Trim() == string.Empty)
        {

            CHQ_NO.InnerText = "0";

        }
        else
        {
            CHQ_NO.InnerText = txtChqNo2.Text.Trim();
        }

        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();

        AMOUNT.InnerText = Convert.ToDouble(txtSecurityAmt.Text).ToString();
        ViewState["SecurityAmt"] = Convert.ToDouble(txtSecurityAmt.Text).ToString();

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";

        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";

        TDSonCGSTPercentage.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTSection.InnerText = "0";

        TDSonSGSTPercentage.InnerText =  "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";

        SecurityAmt.InnerText = chkSecurity.Checked == true ? txtSecurityAmt.Text : "0";

        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";


        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = lblSerialNo.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);

        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);


        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private void SetDataColumn()
    {
        DataSet dtTmp = objCommon.FillDropDown("Acc_Temp_Voucher_Transaction", "*", "", "IpAddress='" + Session["ComputerIDAddress"].ToString().Trim() + "'", string.Empty);
        if (dtTmp.Tables[0].Rows.Count > 0)
        {
            dt = dtTmp.Tables[0];
            DataColumn dc11 = new DataColumn();
            dc11.ColumnName = "CCID";
            dt.Columns.Add(dc11);

            DataColumn dc12 = new DataColumn();
            dc12.ColumnName = "BILL_ID";
            dt.Columns.Add(dc12);

            dt = dtTmp.Tables[0];
            DataColumn dc13 = new DataColumn();
            dc13.ColumnName = "BudgetNo";
            dt.Columns.Add(dc13);

            dt = dtTmp.Tables[0];
            DataColumn dc14 = new DataColumn();
            dc14.ColumnName = "IsTDS";
            dt.Columns.Add(dc14);

            Session["Datatable"] = dt;
            if (dt.Rows[0]["TranMode"].ToString().Trim() == "J")
            {
                isSingleMode = "N";
            }
            if (isSingleMode == "Y")
            {
                txtBankLedger.Text = dt.Rows[0]["Particulars"].ToString().Trim();
                //lblCurbal1.Text = dt.Rows[0]["Balance"].ToString().Trim();
                //hdnCurBalAg.Value = dt.Rows[0]["Balance"].ToString().Trim();
                //lblCrDr1.Text = dt.Rows[0]["Mode"].ToString().Trim();
                ViewState["EditBal"] = dt.Rows[0]["Balance"].ToString().Trim();
                //dt.Rows[0].Delete();
                //dt.AcceptChanges();
                //Session["Datatable"] = dt;

            }
            //ddlTranType.SelectedValue = dt.Rows[0]["TranMode"].ToString().Trim();
            dt.Rows[0].Delete();
            dt.AcceptChanges();
            Session["Datatable"] = dt;
            //ddlTranType.Enabled = false;
            //DataTableToGrid();
        }

        else
        {
            //ddlTranType.Enabled = true;
            if (dt == null)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = "Particulars";
                dt.Columns.Add(dc);

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "Balance";
                dt.Columns.Add(dc1);

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "Narration";
                dt.Columns.Add(dc2);

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "Amount";
                dt.Columns.Add(dc3);

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "Debit";
                dt.Columns.Add(dc4);

                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "Credit";
                dt.Columns.Add(dc5);

                DataColumn dc6 = new DataColumn();
                dc6.ColumnName = "Mode";
                dt.Columns.Add(dc6);

                DataColumn dc7 = new DataColumn();
                dc7.ColumnName = "Id";
                dt.Columns.Add(dc7);

                DataColumn dc8 = new DataColumn();
                dc8.ColumnName = "ChqNo";
                dt.Columns.Add(dc8);

                DataColumn dc9 = new DataColumn();
                dc9.ColumnName = "ChqDate";
                dt.Columns.Add(dc9);

                DataColumn dc10 = new DataColumn();
                dc10.ColumnName = "OppParty";
                dt.Columns.Add(dc10);

                DataColumn dc11 = new DataColumn();
                dc11.ColumnName = "CCID";
                dt.Columns.Add(dc11);

                DataColumn dc12 = new DataColumn();
                dc12.ColumnName = "ProjectSubId";
                dt.Columns.Add(dc12);

                DataColumn dc13 = new DataColumn();
                dc13.ColumnName = "BILL_ID";
                dt.Columns.Add(dc13);

                DataColumn dc14 = new DataColumn();
                dc14.ColumnName = "BudgetNo";
                dt.Columns.Add(dc14);

                DataColumn dc15 = new DataColumn();
                dc15.ColumnName = "IsTDS";
                dt.Columns.Add(dc15);

                Session["Datatable"] = dt;

            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Particulars";
                        dt.Columns.Add(dc);

                        DataColumn dc1 = new DataColumn();
                        dc1.ColumnName = "Balance";
                        dt.Columns.Add(dc1);

                        DataColumn dc2 = new DataColumn();
                        dc2.ColumnName = "Narration";
                        dt.Columns.Add(dc2);

                        DataColumn dc3 = new DataColumn();
                        dc3.ColumnName = "Amount";
                        dt.Columns.Add(dc3);

                        DataColumn dc4 = new DataColumn();
                        dc4.ColumnName = "Debit";
                        dt.Columns.Add(dc4);

                        DataColumn dc5 = new DataColumn();
                        dc5.ColumnName = "Credit";
                        dt.Columns.Add(dc5);

                        DataColumn dc6 = new DataColumn();
                        dc6.ColumnName = "Mode";
                        dt.Columns.Add(dc6);

                        DataColumn dc7 = new DataColumn();
                        dc7.ColumnName = "Id";
                        dt.Columns.Add(dc7);

                        DataColumn dc8 = new DataColumn();
                        dc8.ColumnName = "ChqNo";
                        dt.Columns.Add(dc8);

                        DataColumn dc9 = new DataColumn();
                        dc9.ColumnName = "ChqDate";
                        dt.Columns.Add(dc9);

                        DataColumn dc10 = new DataColumn();
                        dc10.ColumnName = "OppParty";
                        dt.Columns.Add(dc10);

                        DataColumn dc11 = new DataColumn();
                        dc11.ColumnName = "CCID";
                        dt.Columns.Add(dc11);

                        DataColumn dc12 = new DataColumn();
                        dc12.ColumnName = "ProjectSubId";
                        dt.Columns.Add(dc12);

                        DataColumn dc13 = new DataColumn();
                        dc13.ColumnName = "BILL_ID";
                        dt.Columns.Add(dc13);

                        DataColumn dc14 = new DataColumn();
                        dc14.ColumnName = "BudgetNo";
                        dt.Columns.Add(dc14);

                        DataColumn dc15 = new DataColumn();
                        dc15.ColumnName = "IsTDS";
                        dt.Columns.Add(dc15);
                    }
                }
            }
            Session["Datatable"] = dt;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //DataSet dsVoucherDetails = (DataSet)Session["VoucherDetail"];
            //if (ViewState["isFirst"] == null || ViewState["isFirst"].ToString() == "Y")
            //{
            //VoucherNoSetting();
            //SetVoucherNo();
            string voucherNo = "0";
            voucherNo = Session["vchno"].ToString();
            //if (ViewState["isModi"] != null)
            //{
            //    if (isVoucherAuto == "Y")
            //    {
            //        if (ViewState["isModi"].ToString().Trim() == "Y")
            //        {
            //            voucherNo = ViewState["VNO"].ToString();
            //        }
            //        else
            //        {
            //            voucherNo = ViewState["VNO"].ToString();
            //        }

            //        //voucherNo = (Convert.ToInt16(txtVoucherNo.Text) - 1).ToString().Trim();
            //    }
            //    else
            //    {
            //        if (ViewState["isModi"].ToString().Trim() == "Y")
            //        {
            //            voucherNo = ViewState["VNO"].ToString();
            //        }btn
            //        else
            //        {
            //            voucherNo = ViewState["VNO"].ToString();OFF2742015
            //        }
            //    }
            //}
            //else
            //{
            //    if (isVoucherAuto == "Y")
            //    {
            //        voucherNo = ViewState["VNO"].ToString();
            //    }
            //    else
            //    {
            //        voucherNo = ViewState["VNO"].ToString();
            //    }
            //}
            ViewState["isModi"] = "N";


            isFourSign = objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
            isBankCash = objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

            if (isFourSign == "N")
            {
                ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", "Payment", voucherNo, isBankCash);
            }
            else if (isFourSign == "Y")
            {
                ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2.rpt", "Payment", voucherNo, isBankCash);
            }


            upd_ModalPopupExtender1.Show();
            //btnPrint.Text = "Print Cheque";
            //ViewState["isFirst"] = "N";
            //hdnBack.Value = "1";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "AskCheque()", true);
            //}
            //else
            //{


            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "AskCheque()", true);
            //    if (hdnBack.Value == "1")
            //    {
            //        string Script = string.Empty;
            //        string url = string.Empty;
            //        Script = "ShowChequePrinting('" + ViewState["TRANDATE"].ToString() + "','" + ViewState["VoucherNo"].ToString() + "','" + ViewState["OPartyNo"].ToString() + "','" + ViewState["Amount"].ToString() + "','" + ViewState["PartyNo"].ToString() + "','" + ViewState["CHQ_NO"].ToString() + "')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", Script, true);
            //        ViewState["isFirst"] = "Y";
            //        btnPrint.Text = "Print Voucher";
            //    }
            //}
            //// ShowReport("Transaction", "TransDetail.rpt");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchers.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        lvGrp.DataSource = null;
        lvGrp.DataBind();
        ViewState["isModi"] = "N";
        ViewState["isFirst"] = "Y";
        btnPrint.Text = "Print Voucher";

        if (hdnBack.Value == "1")
        {
            string Script = string.Empty;
            string url = string.Empty;
            //lnkledger.Attributes.Add("onClick", "return ShowChequePrinting('" + dtContain.Rows[c]["Date"].ToString().Trim() + "','" + dtContain.Rows[c]["VchNo"].ToString().Trim() + "','" + party + "','" + dtContain.Rows[c]["Credit"].ToString().Trim() + "','" + hdnparty.Value.ToString().Trim() + "');");

            Script = "ShowChequePrinting('" + ViewState["TRANDATE"].ToString().Trim() + "','" + ViewState["VoucherNo"].ToString() + "','" + ViewState["OPartyNo"].ToString() + "','" + ViewState["Amount"].ToString() + "','" + ViewState["PartyNo"].ToString() + "','" + ViewState["CHQ_NO"].ToString() + "')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", Script, true);
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        btnBack_Click(sender, e);
    }
    protected void btnchequePrint_Click(object sender, EventArgs e)
    {
        Session["comp_code"] = Session["BillComp_Code"].ToString();
        upd_ModalPopupExtender1.Show();
    }


    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["BillComp_Code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["BillComp_Code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    private void ShowVoucherCashBankReport(string reportTitle, string rptFileName, String TransactionType, string VchNo, string isBankCash)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            string Comp_Name = objCommon.LookUp("ACC_Company", "COMPANY_NAME", "COMPANY_CODE ='" + Session["BillComp_Code"].ToString() + "'");

            Session["comp_name"] = Comp_Name.ToString();

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["BillComp_Code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["BillComp_Code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim() + ",BankORCashName=" + isBankCash;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlCompAccount.SelectedValue == "0")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Account..!", this.Page);
            ddlCompAccount.Focus();
            return;
        }

        BindPendingBillList();
        lvPendingList.Visible = true;
    }
    protected void ddlBudgethead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string Amount = string.Empty;
            DataSet ds = null;

            string company_code = objCommon.LookUp("acc_company", "COMPANY_CODE", "COMPANY_NO=" + ddlSelectCompany.SelectedValue);
            ds = objRPBController.GetBudegetBalanceNEW(Convert.ToInt32(ddlDeptBranch.SelectedValue), Convert.ToInt32(ddlBudgethead.SelectedValue), DateTime.Today);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["BALANCE"].ToString()));
                }
                else
                {
                    lblBudgetClBal.Text = "0.00";
                }
            }
            txtLedgerHead.Focus();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void txtLedgerHead_TextChanged(object sender, EventArgs e)
    {
        //Added by vijay andoju on 27102020 for validation for ledger head selection
        if (CheckValidLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Ledger From List..!", this.Page);
            txtLedgerHead.Text = lblLedgerClBal.Text = string.Empty;
            return;
        }
        string partyNo = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtLedgerHead.Text.ToString().Trim().Split('*')[1] + "'");
        PartyController objPC = new PartyController();
        DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["BillComp_Code"].ToString());
        if (dtr.Read())
        {

            if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
            {
                lblLedgerClBal.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
                //lblCur2.Text = "Dr";
            }
            else
            {
                lblLedgerClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(dtr["BALANCE"].ToString().Trim())) + " Cr";
                //lblCur2.Text = "Cr";
            }
            //ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
        }
        txtInvoiceNo.Focus();
    }
    //Addede by vijay andoju on 27102020
    protected int CheckValidLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtLedgerHead.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected void chkGST_CheckedChanged(object sender, EventArgs e)
    {
        txtIgstAmount.Text = txtIgstPer.Text = "0";
        txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = "0";
        txtTDSGSTonAmount.Text = txtTDSonGSTPer.Text = txtTDSonGSTAmount.Text = "0";
        ddlTDSonGSTSection.SelectedIndex = 0;
        txtGSTAmount.Text = "0";

        if (chkGST.Checked == true)
        {
            chkIGST.Checked = false;
            chkTDSOnGst.Checked = false;
            divTdsOnGst.Visible = false;
            dvSgst.Visible = true;
            dvCgst.Visible = true;
            dvIGST.Visible = false;
            dvGSTLedger.Visible = true;
            dvIgstledger.Visible = false;
            double gstAmount = Convert.ToDouble(Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text));
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();
            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        else
        {
            dvSgst.Visible = false;
            dvCgst.Visible = false;
            dvGSTLedger.Visible = false;
            chkTdsOnCGSTSGST.Checked = false;
            divTDSOnSGST.Visible = false;
            divTdsOnCGST.Visible = false;
            txtTDSCGSTonAmount.Text = txtTDSonCGSTPer.Text = txtTDSonCGSTAmount.Text = "0";
            txtTDSSGSTonAmount.Text = txtTDSonSGSTPer.Text = txtTDSonSGSTAmount.Text = "0";
            ddlTDSonCGSTSection.SelectedIndex = 0;
            ddlTDSonSGSTSection.SelectedIndex = 0;

            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        AddGstAmount();
    }
    protected void chkIGST_CheckedChanged(object sender, EventArgs e)
    {
        txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = txtGSTAmount.Text = "0";
        txtTDSSGSTonAmount.Text = txtTDSonSGSTPer.Text = txtTDSonSGSTAmount.Text = "0";
        txtIgstPer.Text = txtIgstAmount.Text = "0";
        ddlTDSonSGSTSection.SelectedIndex = 0;
        txtGSTAmount.Text = "0";
        if (chkIGST.Checked == true)
        {
            chkGST.Checked = false;
            chkTdsOnCGSTSGST.Checked = false;
            divTdsOnCGST.Visible = false;
            divTDSOnSGST.Visible = false;
            dvSgst.Visible = false;
            dvCgst.Visible = false;
            dvIGST.Visible = true;
            dvIgstledger.Visible = true;
            dvGSTLedger.Visible = false;
            double gstAmount = Convert.ToDouble(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text);
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();

            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        else
        {
            dvIGST.Visible = false;
            dvIgstledger.Visible = false;
            chkTDSOnGst.Checked = false;
            divTdsOnGst.Visible = false;
            txtTDSGSTonAmount.Text = txtTDSonGSTPer.Text = txtTDSonGSTAmount.Text = "0";
            ddlTDSonGSTSection.SelectedIndex = 0;

            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        AddGstAmount();
    }
    protected void AddGstAmount()
    {
        double gstAmount = Convert.ToDouble(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text) + Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text);
        double Tdsamount = Convert.ToDouble(txtTDSAmt.Text == "" ? "0" : txtTDSAmt.Text);
        double amount = Convert.ToDouble(txtBillAmt.Text == "" ? "0" : txtBillAmt.Text);

        double TdsonGstamount = Convert.ToDouble(txtTDSonGSTAmount.Text == "" ? "0" : txtTDSonGSTAmount.Text);
        double TdsonCGSTamount = Convert.ToDouble(txtTDSonCGSTAmount.Text == "" ? "0" : txtTDSonCGSTAmount.Text);
        double TdsonSGSTamount = Convert.ToDouble(txtTDSonSGSTAmount.Text == "" ? "0" : txtTDSonSGSTAmount.Text);
        double Securityamount = Convert.ToDouble(txtSecurityAmt.Text == "" ? "0" : txtSecurityAmt.Text);
        txtNetAmt.Text = Convert.ToDouble(amount - (Tdsamount + TdsonGstamount + TdsonCGSTamount + TdsonSGSTamount + Securityamount) + gstAmount).ToString();
        txtTotalTDSAmt.Text = Convert.ToDouble(Tdsamount + TdsonGstamount + TdsonCGSTamount + TdsonSGSTamount + Securityamount).ToString();
        txtGSTAmount.Text = gstAmount.ToString();
    }


    protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    {
        txtTDSAmt.Text = txtTDSPer.Text = "0";
        double CalcTDS = 0;
        if (chkTDSApplicable.Checked)
        {
            txtTdsOnAmt.Text = txtBillAmt.Text;
            dvSection.Visible = true;
            tdslabel.Visible = true;
            tdstextbox.Visible = true;
            txtPanNo.Enabled = true;

            if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlSection.SelectedValue != "0")
                {
                    CalcTDS = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSPer.Text)) * 0.01);
                    txtTDSAmt.Text = CalcTDS.ToString();
                    // txtTdsOnAmt.Text = CalcTDS.ToString();



                }
                //if (txtGSTAmount.Text != "")
                //{
                //    txtNetAmt.Text = Math.Round((Convert.ToDouble(txtBillAmt.Text) + Convert.ToDouble(txtGSTAmount.Text)) - Convert.ToDouble(txtTDSAmt.Text)).ToString();
                //}
            }
        }
        else
        {
            dvSection.Visible = false;
            tdslabel.Visible = false;
            tdstextbox.Visible = false;
            ddlSection.SelectedValue = "0";
        }

        AddGstAmount();
    }


    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSection.SelectedValue == "0")
        {
            txtTDSPer.Text = "0";
            txtTDSAmt.Text = "0";
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSPer.Text = "0";
            }
        }
        AddGstAmount();

    }
    protected void ddlCgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCgstSection.SelectedValue == "0")
        {
            txtCGSTPER.Text = "";
        }
        else
        {
            double per = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlCgstSection.SelectedValue)));
            if (per > 0)
            {
                txtCGSTPER.Text = per.ToString();
            }
            else
            {
                txtCGSTPER.Text = "";
            }
        }
    }
    protected void ddlSgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSgstSection.SelectedValue == "0")
        {
            txtSgstPer.Text = "";
        }
        else
        {
            double per = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSgstSection.SelectedValue)));
            if (per > 0)
            {
                txtSgstPer.Text = per.ToString();
            }
            else
            {
                txtSgstPer.Text = "";
            }
        }
    }
    protected void ddlIgstSect_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlIgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIgstSection.SelectedValue == "0")
        {
            txtIgstPer.Text = "";
        }
        else
        {
            double per = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlIgstSection.SelectedValue)));
            if (per > 0)
            {
                txtIgstPer.Text = per.ToString();
            }
            else
            {
                txtIgstPer.Text = "";
            }
        }
    }

    protected void ddlTDSonGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTDSonGSTSection.SelectedValue == "0")
        {
            txtTDSonGSTPer.Text = "0";
            txtTDSonGSTAmount.Text = "0";
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSPer.Text = "0";
            }
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void chkTDSOnGst_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkTDSApplicable.Checked == false)
        //{
        //    MessageBox("Please Check TDS (Y/N)");
        //    return;
        //}
        if (chkTDSOnGst.Checked)
        {
            if (chkIGST.Checked == false)
            {
                MessageBox("Please Check IGST (Y/N)");
                chkTDSOnGst.Checked = false;
                return;
            }
        }

        double CalcTDSOnGST = 0;
        txtTDSGSTonAmount.Text = txtTDSonGSTAmount.Text = txtTDSonGSTPer.Text = "0";
        txtTDSonGSTLedger.Text = string.Empty;

        if (chkTDSOnGst.Checked)
        {
            txtTDSGSTonAmount.Text = txtBillAmt.Text;
            divTdsOnGst.Visible = true;
            divTdsonGstLedger.Visible = true;

            chkTdsOnCGSTSGST.Checked = false;
            divTdsOnCGST.Visible = false;
            divTDSOnSGST.Visible = false;
            divTdsonCGstLedger.Visible = false;
            divTdsonSGstLedger.Visible = false;
            txtTDSonCGSTAmount.Text = txtTDSonSGSTAmount.Text = txtTDSonCGSTPer.Text = txtTDSonSGSTPer.Text = "0";
            txtTDSonCGSTLedger.Text = txtTDSonSGSTLedger.Text = string.Empty;

            //if (txtTDSonGSTPer.Text != "" && txtBillAmt.Text != "")
            //{
            //    if (ddlSection.SelectedValue != "0")
            //    {
            //        CalcTDSOnGST = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSonGSTPer.Text)) * 0.01);
            //        txtTDSAmt.Text = CalcTDSOnGST.ToString();
            //        // txtTdsOnAmt.Text = CalcTDS.ToString();
            //    }

            //}
        }
        else
        {
            divTdsOnGst.Visible = false;
            ddlTDSonGSTSection.SelectedValue = "0";
            divTdsonGstLedger.Visible = false;
        }

        AddGstAmount();
    }

    protected void chkTdsOnCGSTSGST_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkTDSApplicable.Checked == false)
        //{
        //    MessageBox("Please Check TDS (Y/N)");
        //    return;
        //}
        if (chkTdsOnCGSTSGST.Checked)
        {
            if (chkGST.Checked == false)
            {
                MessageBox("Please Check GST (Y/N)");
                chkTdsOnCGSTSGST.Checked = false;
                return;
            }
        }
        txtTDSonGSTLedger.Text = txtTDSonCGSTLedger.Text = txtTDSonSGSTLedger.Text = string.Empty;
        txtTDSonCGSTAmount.Text = txtTDSonSGSTAmount.Text = txtTDSonCGSTPer.Text = txtTDSonSGSTPer.Text = txtTDSonGSTAmount.Text = txtTDSonGSTPer.Text = "0";


        double CalcTDSOnCGSTSGST = 0;
        if (chkTdsOnCGSTSGST.Checked)
        {
            txtTDSCGSTonAmount.Text = txtTDSSGSTonAmount.Text = txtBillAmt.Text;
            divTDSOnSGST.Visible = true;
            divTdsOnCGST.Visible = true;

            divTdsonCGstLedger.Visible = true;
            divTdsonSGstLedger.Visible = true;

            chkTDSOnGst.Checked = false;
            divTdsOnGst.Visible = false;
            divTdsonGstLedger.Visible = false;

            if (txtTDSonCGSTPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlTDSonCGSTSection.SelectedValue != "0")
                {
                    CalcTDSOnCGSTSGST = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSonCGSTPer.Text)) * 0.01);
                    txtTDSonCGSTAmount.Text = CalcTDSOnCGSTSGST.ToString();
                    // txtTdsOnAmt.Text = CalcTDS.ToString();
                }

            }
            if (txtTDSonSGSTPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlTDSonSGSTSection.SelectedValue != "0")
                {
                    CalcTDSOnCGSTSGST = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSonSGSTPer.Text)) * 0.01);
                    txtTDSonSGSTAmount.Text = CalcTDSOnCGSTSGST.ToString();
                }

            }
        }
        else
        {
            ddlTDSonCGSTSection.SelectedValue = "0";
            ddlTDSonSGSTSection.SelectedValue = "0";
            divTdsOnCGST.Visible = false;
            divTDSOnSGST.Visible = false;
            divTdsonCGstLedger.Visible = false;
            divTdsonSGstLedger.Visible = false;
        }
    }
    protected void chkSecurity_CheckedChanged(object sender, EventArgs e)
    {
        txtSecurityAmt.Text = txtSecurityPer.Text = "0";

        if (chkSecurity.Checked == true)
        {
            divSecurity.Visible = true;
            divSecurityLedger.Visible = true;
        }
        else
        {
            divSecurity.Visible = false;
            divSecurityLedger.Visible = false;           
            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        AddGstAmount();
    }
    protected void ddlTDSonCGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTDSonCGSTSection.SelectedValue == "0")
        {
            txtTDSonCGSTPer.Text = "0";
            txtTDSonCGSTAmount.Text = "0";
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonCGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonCGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonCGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSonCGSTPer.Text = "0";
            }
        }
    }
    protected void ddlTDSonSGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTDSonSGSTSection.SelectedValue == "0")
        {
            txtTDSonSGSTPer.Text = "0";
            txtTDSonSGSTAmount.Text = "0";
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonSGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonSGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonSGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSonSGSTPer.Text = "";
            }
        }
    }

    protected void ddlSponsor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["BillComp_Code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["BillComp_Code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "A.ProjectId=" + ddlSponsor.SelectedValue, "");
    }
    protected void ddlProjSubHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRemainAmt.Text = objRPBController.GetSponsorProjectBalance(Convert.ToInt32(ddlSponsor.SelectedValue), Convert.ToInt32(ddlProjSubHead.SelectedValue), Session["BillComp_Code"].ToString());
    }

    //protected void txtExpenseLedger_TextChanged(object sender, EventArgs e)
    //{
    //    //Added by gopal anthati on 19-08-2021 for validation for Expense ledger head selection
    //    if (CheckValidExpenseLedger() == 0)
    //    {
    //        objCommon.DisplayMessage(UPDLedger, "Please Select Expense Ledger From List..!", this.Page);
    //        txtExpenseLedger.Text = lblExpenseLedger.Text = string.Empty;
    //        return;
    //    }
    //    string partyNo = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtExpenseLedger.Text.ToString().Trim().Split('*')[1] + "'");
    //    PartyController objPC = new PartyController();
    //    DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["BillComp_Code"].ToString());
    //    if (dtr.Read())
    //    {

    //        if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
    //        {
    //            lblExpenseLedger.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
    //        }
    //        else
    //        {
    //            lblExpenseLedger.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(dtr["BALANCE"].ToString().Trim())) + " Cr";
    //        }
    //    }
    //    txtInvoiceNo.Focus();
    //}
    //protected int CheckValidExpenseLedger()
    //{
    //    List<string> Ledger = new List<string>();
    //    DataSet ds = new DataSet();
    //    int RetVal = 1;
    //    try
    //    {


    //        ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
    //        }
    //        if (!Ledger.Contains(txtExpenseLedger.Text))
    //        {
    //            RetVal = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ds.Dispose();
    //    }
    //    return RetVal;
    //}


    #region Document Download
    private void BindNewFiles(string PATH)
    {
        try
        {
            if (PATH != null && PATH != string.Empty && PATH != "")
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
                if (System.IO.Directory.Exists(PATH))
                {
                    System.IO.FileInfo[] files = dir.GetFiles();

                    if (Convert.ToBoolean(files.Length))
                    {
                        lvNewFiles.DataSource = files;
                        lvNewFiles.DataBind();
                        // ViewState["FILE"] = files;
                        pnlNewFiles.Visible = true;
                    }
                    else
                    {
                        lvNewFiles.DataSource = null;
                        lvNewFiles.DataBind();
                    }
                }
            }
            else
            {
                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;
        f_name = obj.ToString();
        return f_name;
    }

    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.CommandArgument, btn.ToolTip);
    }

    public void DownloadFile(string path, string fileName)
    {
        try
        {

            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
    #endregion

    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {

        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
       // divPayee.Visible = false;  
       // divPayeeNature.Visible = false;
        ddlEmployee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;
        ddlPayee.SelectedIndex = 0;

        if (ddlEmpType.SelectedValue == "1")
        {
            divEmployee1.Visible = true;
            divEmployee2.Visible = true;

         //   divPayee.Visible = false;          
         //   divPayeeNature.Visible = false;
         
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
           // divPayee.Visible = true;    
          //  divPayeeNature.Visible = true;
        
            divEmployee1.Visible = false;
            divEmployee2.Visible = false;
        }

    }

    //added by 15/12/2021  for Provide payee & employee tagging functionality 
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlPayeeNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");
    }

    protected void ddlPayee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click1(object sender, ImageClickEventArgs e)
    {

        string isFourSign = string.Empty;
        string isBankCash = string.Empty;
        ImageButton btnPrintv = sender as ImageButton;
        int BILL_NO = Convert.ToInt32(btnPrintv.CommandArgument.ToString());
        string voucherNo = "0";
        string VoucherSqn = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + BILL_NO + " AND TRANSACTION_TYPE = 'J'");


        voucherNo = VoucherSqn.ToString();


        isFourSign = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
        isBankCash = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

        if (isFourSign == "N")
        {
            ShowVoucherPrintReport("Journal Voucher", "JvContraVoucherReport.rpt", "Journal", voucherNo);
        }
        else if (isFourSign == "Y")
        {
            ShowVoucherPrintReport("Journal Voucher", "JvContraVoucherReport_Format2_Pending.rpt", "Journal", voucherNo);
        }
    }
}