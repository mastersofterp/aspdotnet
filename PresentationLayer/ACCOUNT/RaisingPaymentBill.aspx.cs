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
using IITMS.NITPRM;
//using System.Windows;
//using System.Windows.Forms;

public partial class ACCOUNT_RaisingPaymentBill : System.Web.UI.Page
{
    Common objCommon = new Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();
    AccountTransactionController objPC1 = new AccountTransactionController();
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
        if (!Page.IsPostBack)
        {
            //Check Page Authoriztion
            CheckPageAuthorization();

            int serialno = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0) + 1", ""));
            ViewState["RaisePayNo"] = serialno.ToString();
            lblSerialNo.Text = ViewState["RaisePayNo"].ToString();

            // Added By Akshay Dixit On 05-05-2022
            int bill_id = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(bill_id),0) + 1", ""));
            ViewState["bill_id"] = bill_id.ToString();
            lblBillId.Text = ViewState["bill_id"].ToString();

            BindDepartment();
            //SetDeptPath();

            //Binding Raising Payment Bill Into Repeater
            BindBill();
            BindComnpany();
            pnlUpdatePay.Visible = false;

            Page.Title = Session["coll_name"].ToString();
            ddlSelectCompany.Focus();

            ViewState["Edit"] = "N";

            BindBillList();
            GetNetAmount();

            Panel1.Visible = false;

        }
    }


    private void SetDeptPath()
    {
        //objCommon.FillDropDownList(ddlDeptBranch, "Payroll_subdept", "SubDeptNo", "SubDept", "SubDeptNo<>0", "SubDept");
        txtApprovalDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

        if (Convert.ToInt32(Session["usertype"].ToString()) != 1)
        {
            //String DEPTNO = objCommon.LookUp("User_ACC UA Inner Join PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "SUBDEPTNO", "UA_NO = " + Convert.ToInt32(Session["userno"].ToString()));
            string DEPTNO = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "DEPTNO", "UA_NO =" + Convert.ToInt32(Session["userno"]));
            
            ViewState["DEPTNO"] = DEPTNO.ToString();
            ddlDeptBranch.SelectedValue = DEPTNO;
            ddlDeptBranch.Enabled = false;
            string Path = "->Finance Branch->AO->FO";

            if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
            {
                string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                lblAuthorityPath.Text = PaPath.ToString() + Path.ToString();
                lblAuthorityPath.Font.Bold = true;

                //string FirstAuth = objCommon.LookUp("ACC_PASSING_AUTHORITY APA Inner Join ACC_PASSING_AUTHORITY_PATH APAP ON APA.PANO = APAP.PAN01 Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PANAME", "UA.UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                //lblApprovedBy.Text = FirstAuth.ToString();
                //lblApprovedBy.Font.Bold = true;

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
    }

    private void BindDepartment()
    {
        try
        {
            // ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "DEPTNO", "UA_NO =" + Convert.ToInt32(Session["userno"]));
           objCommon.FillDropDownList(ddlDeptBranch, "ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo inner join payroll_subdept D ON D.subdeptno=APAP.deptno", "SubDeptNo", "SubDept", "SubDeptNo<>0 AND UA_NO =" + Convert.ToInt32(Session["userno"]), "SubDept");
        }
        catch (Exception)
        {
            
            throw;
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

    private void BindBillList()
    {
        string EMPNO = "";
        DataSet ds = null;
        if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
        {
            EMPNO = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0) UA_IDNO", "UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));
            ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO), "R");
        }
        else
        {
            EMPNO = objCommon.LookUp("USER_ACC UA INNER JOIN PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "EMP.IDNO", "UA.UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));

            if (EMPNO.ToString() == null || EMPNO.ToString() == "" || EMPNO.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Sorry! you Cannot request the bill.", this.Page);
                Response.Redirect("~/notauthorized.aspx?page=RaisingPaymentBill.aspx");
                return;
            }
            else
            {
                ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO), "R");
            }
        }


        if (ds.Tables[0].Rows.Count > 0)
        {
            lvBillList.DataSource = ds.Tables[0];
            lvBillList.DataBind();
        }
        else
        {
            lvBillList.DataSource = null;
            lvBillList.DataBind();
        }
    }

    private void BindComnpany()
    {

        int IsExist = Convert.ToInt32(objCommon.LookUp("acc_usercashbook", "ISNULL(COUNT(*),0)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
        if (IsExist > 0)
        {
            objCommon.FillDropDownList(ddlSelectCompany, "ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + Session["userno"].ToString() + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'AND COMPANY_CODE = '" + Session["comp_code"].ToString() + "'", "COMPANY_NAME");
        }
        else
        {
          //  objCommon.FillDropDownList(ddlSelectCompany, "Acc_Company", "Company_no", "Company_Name + ' - ' + Cast(Year(COMPANY_FINDATE_FROM) as nvarchar(10))+ ' - ' + Cast(Year(COMPANY_FINDATE_TO) As nvarchar(10))", "Drop_Flag='N' and Company_no= 2", "Company_Name");
            objCommon.FillDropDownList(ddlSelectCompany, "Acc_Company", "Company_no", "Company_Name + ' - ' + Cast(Year(COMPANY_FINDATE_FROM) as nvarchar(10))+ ' - ' + Cast(Year(COMPANY_FINDATE_TO) As nvarchar(10))", "Drop_Flag='N'  AND COMPANY_CODE = '" + Session["comp_code"].ToString() + "'", "Company_Name");
        }


    }
    private void BindBill()
    {
        DataSet dsBill = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO", "SERIAL_NO,APPROVAL_NO", "", "RAISE_PAY_NO");
        if (dsBill != null)
        {
            if (dsBill.Tables[0].Rows.Count > 0)
            {
                rptBillList.DataSource = dsBill.Tables[0];
                rptBillList.DataBind();
            }
            else
            {
                rptBillList.DataSource = null;
                rptBillList.DataBind();
            }
        }
        else
        {
            rptBillList.DataSource = null;
            rptBillList.DataBind();
        }
    }

    private void BindBudget()
    {
        if (Convert.ToInt32(ddlSelectCompany.SelectedValue) > 0)
        {
            CostCenterController objbud = new CostCenterController();
            objbud.BindBudgetHead(ddlBudgethead);
            // objCommon.FillDropDownList(ddlBudgethead, "ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");
        }
        else
        {

        }
    }

    protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    {
        txtTdsOnAmt.Text = txtBillAmt.Text; 

        double CalcTDS = 0;
        if (chkTDSApplicable.Checked)
        {
            dvSection.Visible = true;
            if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlSection.SelectedValue != "0")
                {
                    CalcTDS = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSPer.Text)) * 0.01);
                    txtTDSAmt.Text = CalcTDS.ToString();
                }
                //if (txtGSTAmount.Text != "")
                //{
                //    txtNetAmt.Text = Math.Round((Convert.ToDouble(txtBillAmt.Text) + Convert.ToDouble(txtGSTAmount.Text)) - Convert.ToDouble(txtTDSAmt.Text)).ToString();
                //}
            }
        }
        else
        {
            txtTDSAmt.Text = txtTDSPer.Text = txtPanNo.Text = string.Empty;
            dvSection.Visible = false;
            AddGstAmount();

            ddlSection.SelectedValue = "0";
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblSerialNo.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Serial is not available, Cannot able to save..!", this.Page);
                return;
            }

            if (ddlAccount.SelectedValue == "0")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Select Account..!", this.Page);
                return;
            }
            if (ddlDeptBranch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Select Department", this.Page);
                return;
            }
            if (lblAuthorityPath.Text == "" || lblAuthorityPath.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Passing Path cannot be blank!", this.Page);
                return;
            }
            if (txtServiceName.Text == "" || txtServiceName.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Supplier's/Service Provider Name", this.Page);
                return;
            }
            if (txtApprovalDate.Text == "" || txtApprovalDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Approval Date", this.Page);
                return;
            }
            if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Nature of Service /Goods etc", this.Page);
                return;
            }
            if (txtBillAmt.Text == "" || txtBillAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
                return;
            }
            if (chkTDSApplicable.Checked)
            {
                if (txtTDSAmt.Text == "" || txtTDSAmt.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Amount", this.Page);
                    return;
                }
                if (txtTDSPer.Text == "" || txtTDSPer.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Percentage", this.Page);
                    return;
                }
                if (ddlSection.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Section", this.Page);
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
            //if (txtGSTAmount.Text == "" || txtGSTAmount.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Enter GST Amount", this.Page);
            //    return;
            //}
            //if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
            //{
            //    if (txtGSTINNo.Text.Length != 15)
            //    {
            //        objCommon.DisplayMessage(UPDLedger, "GST No. should be 15 character!!", this.Page);
            //        return;
            //    }
            //}
            if (txtNetAmt.Text == "" || txtNetAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Net Amount is Empty", this.Page);
            }
            if (txtTotalBillAmt.Text == "" || txtTotalBillAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Total Bill Amount is Empty", this.Page);
                return;
            }
            if (ddlSelectCompany.SelectedValue == "0")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Select any Company", this.Page);
                return;
            }

            ObjRPB.SERIAL_NO = Convert.ToInt32(ViewState["RaisePayNo"].ToString());
          
            ObjRPB.ACCOUNT = ddlAccount.SelectedValue;
            ObjRPB.DEPT_ID = Convert.ToInt32(ddlDeptBranch.SelectedValue);
            ObjRPB.APPROVAL_NO = txtApprovalNo.Text.ToString();
            ObjRPB.APPROVAL_DATE = Convert.ToDateTime(txtApprovalDate.Text);
            //ObjRPB.APPROVED_BY = Convert.ToInt32(ddlApprovedBy.SelectedValue).ToString();
            ObjRPB.APPROVED_BY = lblApprovedBy.Text;
            ObjRPB.SUPPLIER_NAME = txtServiceName.Text.ToString();
            ObjRPB.PAYEE_NAME_ADDRESS = txtPayeeNameAddress.Text.ToString();
            ObjRPB.NATURE_SERVICE = txtNatureOfService.Text.ToString();
            ObjRPB.GSTIN_NO = txtGSTINNo.Text.ToString();
            ObjRPB.BILL_AMT = Convert.ToDouble(txtBillAmt.Text);
            ObjRPB.GST_AMT = Convert.ToDouble(txtGSTAmount.Text);  // == "" ? "0" : "0"
            ObjRPB.TOTAL_BILL_AMT = Convert.ToDouble(txtTotalBillAmt.Text);
            ObjRPB.NET_AMT = Convert.ToDouble(txtNetAmt.Text);
            ObjRPB.INVOICEDATE = txtInvoiceDate.Text;
            ObjRPB.INVOICENO = txtInvoiceNo.Text;
            ObjRPB.COMPANY_CODE = Session["BillComp_Code"].ToString();
            ObjRPB.BUDGET_NO = Convert.ToInt32(ddlBudgethead.SelectedValue);
            ObjRPB.REMARK = txtRemark.Text;
            ObjRPB.TOTAL_TDS_AMT = Convert.ToDouble(txtTotalTDSAmt.Text == "" ? "0" : txtTotalTDSAmt.Text); // Added By Akshay On 04-05-2022
            ObjRPB.TDS_ON_AMT = Convert.ToDouble(txtTdsOnAmt.Text == "" ? "0" : txtTdsOnAmt.Text);          // Added By Akshay On 27-06-2022             

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
//------------------------------------------------------------------------------------------------------------------
            //Added by Akshay Dixit on 03/05/2022
            if (chkTDSOnGst.Checked)
            {
                ObjRPB.ISTDSONGST = 1;


                // ObjRPB.TDS_ON_GST_AMT = Convert.ToDouble(txtTDSonGSTAmount.Text);
                ObjRPB.TDS_ON_GST_AMT = Convert.ToDouble(txtTDSGSTonAmount.Text) * Convert.ToDouble(txtTDSonGSTPer.Text) / 100;
                ObjRPB.TDSGST_ON_AMT = Convert.ToDouble(txtTDSGSTonAmount.Text);
                ObjRPB.TDSGST_PERCENT = Convert.ToDouble(txtTDSonGSTPer.Text);
                ObjRPB.TDSGST_SECTION_NO = Convert.ToInt32(ddlTDSonGSTSection.SelectedValue);

                //if (txtTDSonGSTLedger.Text == "" || txtTDSonGSTLedger.Text == string.Empty)
                //{
                //    //added by tanu 24/01/2022
                //    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonGST Ledger", this.Page);
                //    return;
                //    //ObjRPB.TDSonGSTLedgerId = 0;
                //}
                //else
                //{
                //    ObjRPB.TDSonGSTLedgerId = Convert.ToInt32(txtTDSonGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                //}
            }
            else
            {
                ObjRPB.ISTDSONGST = 0;
                ObjRPB.TDS_ON_GST_AMT = 0;
                ObjRPB.TDSGST_PERCENT = 0;
                ObjRPB.TDSGST_SECTION_NO = 0;
                ObjRPB.TDSonGSTLedgerId = 0;
            }

            if (chkTdsOnCGSTSGST.Checked)
            {
                ObjRPB.ISTDSONCGSTSGST = 1;
                ObjRPB.TDS_ON_CGST_AMT = Convert.ToDouble(txtTDSonCGSTAmount.Text);
                ObjRPB.TDS_ON_SGST_AMT = Convert.ToDouble(txtTDSonSGSTAmount.Text);
                ObjRPB.TDSCGST_ON_AMT = Convert.ToDouble(txtTDSCGSTonAmount.Text);
                ObjRPB.TDSSGST_ON_AMT = Convert.ToDouble(txtTDSSGSTonAmount.Text);
                ObjRPB.TDSCGST_PERCENT = Convert.ToDouble(txtTDSonCGSTPer.Text);
                ObjRPB.TDSSGST_PERCENT = Convert.ToDouble(txtTDSonSGSTPer.Text);
                ObjRPB.TDSCGST_SECTION_NO = Convert.ToInt32(ddlTDSonCGSTSection.SelectedValue);
                ObjRPB.TDSSGST_SECTION_NO = Convert.ToInt32(ddlTDSonSGSTSection.SelectedValue);

                //if (txtTDSonCGSTLedger.Text == "" || txtTDSonCGSTLedger.Text == string.Empty)
                //{
                //    //added by tanu 24/01/2022
                //    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonCGST Ledger", this.Page);
                //    return;
                //    // ObjRPB.TDSonCGSTLedgerId = 0;
                //}
                //else
                //{
                //    ObjRPB.TDSonCGSTLedgerId = Convert.ToInt32(txtTDSonCGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                //}
                //if (txtTDSonSGSTLedger.Text == "" || txtTDSonSGSTLedger.Text == string.Empty)
                //{
                //    //added by tanu 24/01/2022
                //    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonSGST Ledger", this.Page);
                //    return;
                //    // ObjRPB.TDSonSGSTLedgerId = 0;
                //}
                //else
                //{
                //    ObjRPB.TDSonSGSTLedgerId = Convert.ToInt32(txtTDSonSGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                //}
            }
            else
            {
                ObjRPB.ISTDSONCGSTSGST = 0;
                ObjRPB.TDS_ON_CGST_AMT = 0;
                ObjRPB.TDSCGST_PERCENT = 0;
                ObjRPB.TDSCGST_SECTION_NO = 0;
                ObjRPB.TDSonCGSTLedgerId = 0;


                ObjRPB.TDS_ON_SGST_AMT = 0;
                ObjRPB.TDSSGST_PERCENT = 0;
                ObjRPB.TDSSGST_SECTION_NO = 0;
                ObjRPB.TDSonSGSTLedgerId = 0;
            }


//-----------------------------------------------------------------------------------------------------------------------------------------


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


            if (ViewState["Edit"].ToString() == "Y")
            {
                ObjRPB.RAISE_PAY_NO = Convert.ToInt32(ViewState["RaisePayNo"].ToString());
            }
            else
            {
                ObjRPB.RAISE_PAY_NO = 0;
            }
            int ret = objRPBController.AddRaisingPaymentBill(ObjRPB, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(Session["colcode"].ToString()));

            if (ret == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Inserted Successfully!", this.Page);
                //lblSerialNo.Text = (Convert.ToInt32(lblSerialNo.Text) + 1).ToString();
                BindBill();
                BindBillList();
                //btnUpdate.Visible = true;
                Panel1.Visible = false;
                pnlBillList.Visible = true;
                Clear();
            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully!", this.Page);
                //lblSerialNo.Text = (Convert.ToInt32(lblSerialNo.Text) + 1).ToString();
                BindBill();
                BindBillList();
                //btnUpdate.Visible = true;
                Panel1.Visible = false;
                pnlBillList.Visible = true;
                Clear();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "RaisingPaymentBill.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
        {
            lblAuthorityPath.Text = string.Empty;
            dvApproval.Visible = false;
            dvAuthorityPath.Visible = false;
        }
        else
        {
           // String DEPTNO = objCommon.LookUp("User_ACC UA Inner Join PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "SUBDEPTNO", "UA_NO = " + Convert.ToInt32(Session["userno"].ToString()));
            string DEPTNO = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "DEPTNO", "UA_NO =" + Convert.ToInt32(Session["userno"]));
            ViewState["DEPTNO"] = DEPTNO.ToString();
            ddlDeptBranch.SelectedValue =Convert.ToString(DEPTNO);
            string Path = "->Finance Branch->AO->FO";

            if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
            {
                string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                lblAuthorityPath.Text = PaPath.ToString() + Path.ToString();
                lblAuthorityPath.Font.Bold = true;

                //string FirstAuth = objCommon.LookUp("ACC_PASSING_AUTHORITY APA Inner Join ACC_PASSING_AUTHORITY_PATH APAP ON APA.PANO = APAP.PAN01 Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PANAME", "UA.UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                //lblApprovedBy.Text = FirstAuth.ToString();
                //lblApprovedBy.Font.Bold = true;

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
       txtApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");     
        txtApprovalNo.Text = string.Empty;
        txtBillAmt.Text = string.Empty;
        txtGSTAmount.Text = string.Empty;
        txtGSTINNo.Text = string.Empty;
        txtNatureOfService.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtPayeeNameAddress.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtServiceName.Text = string.Empty;
        txtTDSAmt.Text = string.Empty;
        txtTDSPer.Text = string.Empty;
        txtTotalBillAmt.Text = string.Empty;
        txtNetAmt.Text = string.Empty;
        txtInvoiceDate.Text = string.Empty;
        txtInvoiceNo.Text = string.Empty;
        ddlDeptBranch.SelectedValue = "0";
        ddlAccount.SelectedValue = "0";
        ddlApprovedBy.SelectedValue = "0";
        chkTDSApplicable.Checked = false;

        chkGST.Checked = false;
        chkIGST.Checked = false;
        ddlSgstSection.SelectedValue = "0";
        ddlCgstSection.SelectedValue = "0";
        ddlIgstSection.SelectedValue = "0";
        ddlSection.SelectedValue = "0";

        txtCgstAmount.Text = txtCGSTPER.Text = string.Empty;

        txtSgstAmount.Text = txtSgstPer.Text = string.Empty;

        txtIgstAmount.Text = txtIgstPer.Text = string.Empty;


        dvCgst.Visible = false;
        dvIGST.Visible = false;
        dvSgst.Visible = false;

        lblBudgetClBal.Text = string.Empty;
        lblLedgerClBal.Text = string.Empty;


        ddlAccount.Focus();
        dvSection.Visible = false;
        int serialno = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0) + 1", ""));
        ViewState["RaisePayNo"] = serialno.ToString();
        lblSerialNo.Text = ViewState["RaisePayNo"].ToString();
        ViewState["Edit"] = "N";
        // btnUpdate.Visible = true;
        ddlBudgethead.SelectedValue = "0";
        txtLedgerHead.Text = string.Empty;
        //dvApproval.Visible = false;
        //dvAuthorityPath.Visible = false;

        //------------------------ Added By Akshay Dixit 04-05-2022-----To Clear TDSONGST Part-----------------------------


        txtTotalTDSAmt.Text = string.Empty;
        txtTDSSGSTonAmount.Text = string.Empty;
        ddlTDSonSGSTSection.SelectedValue = "0";
        txtTDSonSGSTPer.Text = string.Empty;
        txtTDSonSGSTAmount.Text = string.Empty;

        txtTDSCGSTonAmount.Text = string.Empty;
        ddlTDSonCGSTSection.SelectedValue = "0";
        txtTDSonCGSTPer.Text = string.Empty;
        txtTDSonCGSTAmount.Text = string.Empty;
        chkTdsOnCGSTSGST.Checked = false;

        ddlSelectCompany.SelectedValue = "0";

        chkTDSOnGst.Visible = false;
        divTdsOnCGST.Visible = false;
        divTDSOnSGST.Visible = false;

        chkTDSOnGst.Checked = false;
        txtTDSGSTonAmount.Text = string.Empty;
        ddlTDSonGSTSection.SelectedValue = "0";
        txtTDSonGSTPer.Text = string.Empty;
        txtTDSonGSTAmount.Text = string.Empty;
        divTdsOnGst.Visible = false;

    
        //--------------------------------------------------------------------------------------------


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        dvSection.Visible = false;
        // btnUpdate.Visible = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        pnlUpdatePay.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        pnlUpdatePay.Visible = false;
        pnlBillList.Visible = true;

        //lvNewBills.DataSource = null;
        //lvNewBills.DataBind();
        //pnlNewBills.Visible = false;
        //DeleteDirecPath(Docpath + "TEMPUPLOAD_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]));
        ViewState["letrno"] = null;
        ViewState["DESTINATION_PATH"] = null;
        ViewState["DOCS"] = null;
        ViewState["RaisePayNo"] = null;


        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        //btnfillSave.Visible = false;
        btnBack.Visible = false;


        //Diveservices.Visible = false;
        //DivServicesText.Visible = false;
    }
    protected void rptBillList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int PayId = Convert.ToInt32(e.CommandArgument.ToString());
        DataSet ds = null;
        if (e.CommandName == "Edit")
        {
            ds = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO AS PAYID", "*", "RAISE_PAY_NO=" + PayId, "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["RaisePayNo"] = ds.Tables[0].Rows[0]["PAYID"].ToString();
                    lblSerialNo.Text = ViewState["RaisePayNo"].ToString();
                    ddlAccount.SelectedValue = ds.Tables[0].Rows[0]["ACCOUNT"].ToString();
                    ddlDeptBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();
                    txtApprovalNo.Text = ds.Tables[0].Rows[0]["APPROVAL_NO"].ToString();
                    txtApprovalDate.Text = ds.Tables[0].Rows[0]["APPROVAL_DATE"].ToString();
                    ddlApprovedBy.SelectedValue = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
                    txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                    txtPayeeNameAddress.Text = ds.Tables[0].Rows[0]["PAYEE_NAME_ADDRESS"].ToString();
                    txtNatureOfService.Text = ds.Tables[0].Rows[0]["NATURE_SERVICE"].ToString();
                    txtGSTINNo.Text = ds.Tables[0].Rows[0]["GSTIN_NO"].ToString();
                    txtBillAmt.Text = ds.Tables[0].Rows[0]["BILL_AMT"].ToString();
                    chkTDSApplicable.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISTDS"]);
                    ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION_NO"].ToString();
                    txtTDSPer.Text = ds.Tables[0].Rows[0]["TDS_PERCENT"].ToString();
                    txtTDSAmt.Text = ds.Tables[0].Rows[0]["TDS_AMT"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                    txtGSTAmount.Text = ds.Tables[0].Rows[0]["GST_AMT"].ToString();
                    txtTotalBillAmt.Text = ds.Tables[0].Rows[0]["TOTAL_BILL_AMT"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();

                    if (chkTDSApplicable.Checked)
                    {
                        dvSection.Visible = true;
                    }
                    pnlUpdatePay.Visible = false;
                    Panel1.Visible = true;
                    btnUpdate.Visible = false;

                    ViewState["Edit"] = "Y";
                }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "Data Not found..!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Data Not found..!", this.Page);
                return;
            }
        }
        else if (e.CommandName == "delete")
        {

        }
    }
    //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)  // commmented by Akshay on 02-05-2022
    //{

    //    if (ddlSection.SelectedValue == "0")
    //    {
    //        txtTDSPer.Text = "";
    //    }
    //    else
    //    {
    //        decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSection.SelectedValue)));
    //        if (per > 0)
    //        {
    //            txtTDSPer.Text = per.ToString();
    //            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 1 + ");</script>", false);
    //        }
    //        else
    //        {
    //            txtTDSPer.Text = "";
    //        }
    //    }

    //}

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSection.SelectedValue == "0")
        {
            txtTDSPer.Text = "0";
            txtTDSAmt.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
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

    }

    protected void ddlSelectCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Comp_code = objCommon.LookUp("Acc_Company", "Company_Code", "Company_No = " + Convert.ToInt32(ddlSelectCompany.SelectedValue));
        Session["BillComp_Code"] = Comp_code;
        BindSection();
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
    }
    protected void ddlDeptBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Path = "->Finance Branch->AO->FO";
        if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
        {
            //if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
            //{
            //    string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "TOP 1 PAPATH", "DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue) + " GROUP BY PAPATH ORDER BY COUNT(PAPATH) DESC");
            //    if (PaPath.Length > 0)
            //    {
            //        lblAuthorityPath.Text = PaPath.ToString() + Path;
            //        lblAuthorityPath.Font.Bold = true;
            //        txtApprovalNo.Focus();

            //        dvApproval.Visible = true;
            //        dvAuthorityPath.Visible = true;
            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage(UPDLedger, "Approval Authority Path is not defined for selected department. Please contact to Administrator!", this.Page);
            //        ddlDeptBranch.SelectedValue = "0";
            //        dvApproval.Visible = false;
            //        dvAuthorityPath.Visible = false;
            //        return;
            //    }
            //}
            //else
            //{
            string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
            if (PaPath.ToString().Length > 0)
            {
                lblAuthorityPath.Text = PaPath.ToString() + Path;
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
                objCommon.DisplayMessage(UPDLedger, "Approval Authority Path is not defined for selected department. Please contact to Administrator!", this.Page);
                ddlDeptBranch.SelectedValue = "0";
                dvApproval.Visible = false;
                dvAuthorityPath.Visible = false;
                return;
            }
        }
        //}
        //else
        //{
        //    lblAuthorityPath.Text = "";
        //    lblAuthorityPath.Font.Bold = true;
        //    dvApproval.Visible = false;
        //    dvAuthorityPath.Visible = false;
        //}

        BindBudget();
        lblBudgetClBal.Text = string.Empty;
        txtLedgerHead.Text = string.Empty;
        lblLedgerClBal.Text = string.Empty;
    }

    private void BindSection()
    {
        objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlCgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlSgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlIgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonCGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonSGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
       
    }

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


    protected void btnAddNew_Click(object sender, EventArgs e)
    {

        //string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
        string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]));
       
        if (PaPath.ToString().Length > 0)
        {
            Panel1.Visible = true;
            pnlBillList.Visible = false;
            //SetDeptPath();
            BindComnpany();
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Approval authority path is not defined for this user so, you cannot raise the bill!", this.Page);
            return;
        }
        //Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Edit"] = "Y";

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int RaisePayno = Convert.ToInt32(btnEdit.CommandArgument.ToString());
            string statuscode = btnEdit.CommandName.ToString();

            if (statuscode != "P")
            {
                objCommon.DisplayMessage(UPDLedger, "Sorry! You cannot edit this record, Because it is forwarded/approved by authority", this.Page);
                return;
            }
            else
            {
                DataSet ds = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO a", "*", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    // Added By Akshay on 04-05-2022
                      ViewState["bill_Id"] = ds.Tables[0].Rows[0]["bill_id"].ToString();
                    lblBillId.Text = ds.Tables[0].Rows[0]["bill_id"].ToString();


                    ViewState["BillNo"] = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    ViewState["RaisePayNo"] = ds.Tables[0].Rows[0]["RAISE_PAY_NO"].ToString();
                    lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();

                    Session["BillComp_Code"] = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();

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
                    BindSection();
                    //lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    //ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                    ddlAccount.SelectedValue = ds.Tables[0].Rows[0]["ACCOUNT"].ToString();
                    ddlDeptBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();
                    ddlDeptBranch.Enabled = true;


                    string Path = "->Finance Branch->AO->FO";

                    if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
                    {
                        if (Convert.ToInt32(Session["usertype"].ToString()) == 0)
                        {
                            string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "TOP 1 PAPATH", "DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue) + " GROUP BY PAPATH ORDER BY COUNT(PAPATH) DESC");
                            if (PaPath.Length > 0)
                            {
                                lblAuthorityPath.Text = PaPath.ToString() + Path;
                                lblAuthorityPath.Font.Bold = true;
                                txtApprovalNo.Focus();

                                dvApproval.Visible = true;
                                dvAuthorityPath.Visible = true;
                            }
                            else
                            {
                                objCommon.DisplayMessage(UPDLedger, "Approval Authority is not defined for selected department, Please contact to administrator!", this.Page);
                                dvApproval.Visible = false;
                                dvAuthorityPath.Visible = false;
                                return;
                            }
                        }
                        else
                        {
                            string PaPath = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH APAP Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PAPATH", "UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                            if (PaPath.Length > 0)
                            {
                                lblAuthorityPath.Text = PaPath.ToString() + Path.ToString();
                                lblAuthorityPath.Font.Bold = true;

                                //string FirstAuth = objCommon.LookUp("ACC_PASSING_AUTHORITY APA Inner Join ACC_PASSING_AUTHORITY_PATH APAP ON APA.PANO = APAP.PAN01 Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PANAME", "UA.UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                                //lblApprovedBy.Text = FirstAuth.ToString();
                                //lblApprovedBy.Font.Bold = true;

                                dvApproval.Visible = true;
                                dvAuthorityPath.Visible = true;
                                txtApprovalNo.Focus();
                            }
                            else
                            {
                                objCommon.DisplayMessage(UPDLedger, "Approval Authority is not defined for selected department, Please contact to administrator!", this.Page);
                                dvApproval.Visible = false;
                                dvAuthorityPath.Visible = false;
                                return;
                            }
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
                    txtApprovalDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["APPROVAL_DATE"]).ToString("dd/MM/yyyy");
                    lblApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["BUDGET_NO"].ToString()) > 0)
                    {
                        ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                        ddlBudgethead_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        ddlBudgethead.SelectedValue = "0";
                    }


                    string PartyNo = ds.Tables[0].Rows[0]["LEDGER_NO"].ToString();
                    if (Convert.ToInt32(PartyNo) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + PartyNo);
                        txtLedgerHead.Text = partyname.ToString();

                        txtLedgerHead_TextChanged(sender, e);
                    }
                    else
                    {
                        string partyname = "";
                        txtLedgerHead.Text = partyname.ToString();
                    }

                    rdbBillList.SelectedValue = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                    txtInvoiceNo.Text = ds.Tables[0].Rows[0]["BILL_INVOICE_NO"].ToString();
                    string date = ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"].ToString();
                    if (date != "" || date != string.Empty)
                    {
                        txtInvoiceDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtInvoiceDate.Text = "";
                    }
                    txtNatureOfService.Text = ds.Tables[0].Rows[0]["NATURE_SERVICE"].ToString();
                    txtPayeeNameAddress.Text = ds.Tables[0].Rows[0]["PAYEE_NAME_ADDRESS"].ToString();
                    txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                    txtGSTINNo.Text = ds.Tables[0].Rows[0]["GSTIN_NO"].ToString();

                    txtBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["BILL_AMT"]).ToString();
                    int IsTDS = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDS"].ToString());

                    if (IsTDS == 1)
                    {
                        chkTDSApplicable.Checked = true;
                        dvSection.Visible = true;
                        txtTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_AMT"]).ToString();
                        ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION_NO"].ToString();
                        txtTDSPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_PERCENT"]).ToString();
                        txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                    }
                    else
                    {
                        chkTDSApplicable.Checked = false;
                        dvSection.Visible = false; ;
                        txtTDSAmt.Text = "";
                        //  ddlSection.SelectedValue = "0";
                        txtTDSPer.Text = "";
                        txtPanNo.Text = "";
                    }

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
                    }
                    else
                    {
                        dvCgst.Visible = false;
                        dvSgst.Visible = false;
                    }
                    if (isigst == 1)
                    {
                        chkIGST.Checked = true;
                        txtIgstPer.Text = ds.Tables[0].Rows[0]["IGST_PER"].ToString();
                        txtIgstAmount.Text = ds.Tables[0].Rows[0]["IGST_AMOUNT"].ToString();
                        ddlIgstSection.SelectedValue = ds.Tables[0].Rows[0]["IGST_SECTIONNO"].ToString();
                        dvIGST.Visible = true;
                    }
                    else
                    {
                        dvIGST.Visible = false;
                    }

                    //-----------------Added By Akshay Dixit on 04-05-2022---------------------------------------------
                    int ISTDSONCGSTSGST = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONCGSTSGST"].ToString());
                    if (ISTDSONCGSTSGST == 1)
                    {
                        chkTdsOnCGSTSGST.Checked = true;

                        txtCgstAmount.Text = ds.Tables[0].Rows[0]["CGST_AMOUNT"].ToString();
                        txtCGSTPER.Text = ds.Tables[0].Rows[0]["CGST_PER"].ToString();
                        ddlCgstSection.SelectedValue = ds.Tables[0].Rows[0]["CGST_SECTIONNO"].ToString();

                        txtSgstAmount.Text = ds.Tables[0].Rows[0]["SGST_AMOUNT"].ToString();
                        txtSgstPer.Text = ds.Tables[0].Rows[0]["SGST_PER"].ToString();
                        ddlSgstSection.SelectedValue = ds.Tables[0].Rows[0]["SGST_SECTIONNO"].ToString();

                        dvCgst.Visible = true;
                        dvSgst.Visible = true;

                        txtTDSCGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSCGST_ON_AMT"].ToString();
                        ddlTDSonCGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONCGST_SECTION"].ToString();
                        txtTDSonCGSTPer.Text = ds.Tables[0].Rows[0]["TDSONCGSTPER"].ToString();
                        txtTDSonCGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONCGST_AMOUNT"].ToString();

                        txtTDSSGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSSGST_ON_AMT"].ToString();
                        ddlTDSonSGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONSGST_SECTION"].ToString();
                        txtTDSonSGSTPer.Text = ds.Tables[0].Rows[0]["TDSONSGSTPER"].ToString();
                        txtTDSonSGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONSGST_AMOUNT"].ToString();
                        txtTotalTDSAmt.Text = ds.Tables[0].Rows[0]["TOTAL_TDS_AMT"].ToString();

                        divTdsOnCGST.Visible = true;
                        divTDSOnSGST.Visible = true;
                    }
                    //-------------------------------------------------------------------------------------------------

                    txtTotalBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_BILL_AMT"]).ToString();
                    txtGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GST_AMT"]).ToString();
                    txtNetAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_AMT"]).ToString();

                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    Panel1.Visible = true;
                    pnlBillList.Visible = false;
                    AddGstAmount();
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lvBillList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            HiddenField hdnStatusCode = (HiddenField)e.Item.FindControl("hdnStatusCode");
            HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");

            //if (hdnStatusCode.Value == "F" || hdnStatusCode.Value == "A")
            //{
            //    btnEdit.ToolTip = "Sorry! You cannot Edit,because it is already " + hdnStatus.Value;
            //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>EditUpdate(" + btnEdit.ToolTip.ToString()+");</script>", false);
            //    btnEdit.Enabled = false;
            //}
        }
    }
    //protected void btnBack_Click1(object sender, EventArgs e)
    //{
    //    Panel1.Visible = false;
    //    pnlBillList.Visible = true;
    //    Clear();
    //}
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
                    lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["PRICE"].ToString()));  //BALANCE
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


    protected void ddlgst_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dddddlgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void chkGST_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtIgstAmount.Text = txtIgstPer.Text = string.Empty;
    //    AddGstAmount();
    //    if (chkGST.Checked == true)
    //    {
    //        chkIGST.Checked = false;
    //        dvSgst.Visible = true;
    //        dvCgst.Visible = true;
    //        dvIGST.Visible = false;
    //    }
    //    else
    //    {
    //        dvSgst.Visible = false;
    //        dvCgst.Visible = false;
    //        txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = string.Empty;
    //        AddGstAmount();
    //    }

    //}

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
           // dvGSTLedger.Visible = true;
            dvIgstledger.Visible = false;
            double gstAmount = Convert.ToDouble(Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text));
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();
            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        else
        {
            dvSgst.Visible = false;
            dvCgst.Visible = false;
           // dvGSTLedger.Visible = false;
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
        txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = string.Empty;
        AddGstAmount();
        if (chkIGST.Checked == true)
        {
            chkGST.Checked = false;
            dvSgst.Visible = false;
            dvCgst.Visible = false;
            dvIGST.Visible = true;
        }
        else
        {
            dvIGST.Visible = false;
            txtIgstAmount.Text = txtIgstPer.Text = string.Empty;
            AddGstAmount();
        }
    }
    protected void AddGstAmount()
    {
        decimal gstAmount = Convert.ToDecimal(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text) + Convert.ToDecimal(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDecimal(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text);
        decimal Tdsamount = Convert.ToDecimal(txtTDSAmt.Text == "" ? "0" : txtTDSAmt.Text);
        decimal amount = Convert.ToDecimal(txtBillAmt.Text == "" ? "0" : txtBillAmt.Text);
        txtGSTAmount.Text = gstAmount.ToString();
        txtNetAmt.Text = Convert.ToDecimal(amount - Tdsamount + gstAmount).ToString();

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
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 3 + ");</script>", false);

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
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 4 + ");</script>", false);

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
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 2 + ");</script>", false);

            }
            else
            {
                txtIgstPer.Text = "";
            }
        }
    }
    //protected void txtBillAmt_TextChanged(object sender, EventArgs e)
    //{
    //    txtNetAmt.Text = txtTotalBillAmt.Text = txtBillAmt.Text;
    //}
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Reimbursement Bill Report", "ReimbursementBillReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            //string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            DateTime date = Convert.ToDateTime("9999-12-31");
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPT_NO=" + Session["UA_EmpDeptNo"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(date).ToString("yyyy-MM-dd");

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnStatus_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

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

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void chkTdsOnCGSTSGST_CheckedChanged(object sender, EventArgs e)  // Addd By Akshay Dixit On 18-08-2022
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
            BindSection();
            //divTdsonCGstLedger.Visible = true;
            //divTdsonSGstLedger.Visible = true;

            //chkTDSOnGst.Checked = false;
            //divTdsOnGst.Visible = false;
            //divTdsonGstLedger.Visible = false;

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
            //divTdsonCGstLedger.Visible = false;
            //divTdsonSGstLedger.Visible = false;
        }
        AddGstAmount();
    }

    protected void ddlTDSonGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if (txtTDSonGSTAmount.Text == "0" || txtTDSonGSTAmount.Text == "" || txtTDSonGSTAmount.Text == "0.00" || txtTDSonGSTAmount.Text == "0.0")
        // {
        if (ddlTDSonGSTSection.SelectedValue == "0")
        {
            txtTDSonGSTPer.Text = "0";
            txtTDSonGSTAmount.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
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
        // }
    }

    protected void ddlTDSonCGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTDSonCGSTSection.SelectedValue == "0")
        {
            txtTDSonCGSTPer.Text = "0";
            txtTDSonCGSTAmount.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
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
            txtNetAmt.Text = txtBillAmt.Text;
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
                txtTDSonSGSTPer.Text = "0";
            }
        }
    }

    protected void chkTDSOnGst_CheckedChanged(object sender, EventArgs e)
    {
        //GetTdsAmt();
        //GetTdsOnIgstAmt();
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
            divTdsonGstLedger.Visible = false;  // Changed on 06-05-2022 Akshay Dixit

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
}