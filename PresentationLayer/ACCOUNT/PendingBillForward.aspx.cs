﻿//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :RAISING PAYMENT BILL FORWARD                                                  
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


public partial class ACCOUNT_PendingBillForward : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    #region Page Load

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

                int serialno = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0) + 1", ""));
                ViewState["RaisePayNo"] = serialno.ToString();
                lblSerialNo.Text = ViewState["RaisePayNo"].ToString();

                // Added By Akshay Dixit On 05-05-2022
                int bill_id = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(bill_id),0)+ 1", ""));
                ViewState["bill_id"] = bill_id.ToString();
                lblBillId.Text = ViewState["bill_id"].ToString();

                objCommon.FillDropDownList(ddlDeptBranch, "Payroll_subdept", "SubDeptNo", "SubDept", "SubDeptNo<>0", "SubDept");

                int usernock = Convert.ToInt32(Session["userno"]);
                BindPendingBillList();
                BindComnpany();
                dvbuttons.Visible = false;
            }
        }
    }

    #endregion

    #region Events Click

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int BillNo = int.Parse(btnApproval.CommandArgument);
            Session["BillNo"] = BillNo;

            string Comp_code = objCommon.LookUp("Acc_Company", "Company_Code", "Company_No = " + Convert.ToInt32(ddlSelectCompany.SelectedValue));

            DataSet ds = objRPBController.GetSingleRecordsPendingBill(BillNo, Convert.ToInt32(Session["userno"].ToString()), Comp_code);
            if (ds.Tables.Count > 0)
            {

            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Selected Bill is not forwarded by previous authority than you..!", this.Page);
                return;
            }

            ShowDetails(BillNo);

            AddGstAmount();
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNTS_PendingBillApproval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
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

        if (Convert.ToInt32(ddlSelectCompany.SelectedValue) > 0)
        {
            BindDropDown();
        }
    }

    protected void BindDropDown()
    {
        objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlCgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlSgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlIgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
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


    protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    {
        double CalcTDS = 0;
        if (chkTDSApplicable.Checked)
        {
            dvSection.Visible = true;
            txtPanNo.Enabled = true;
            if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlSection.SelectedValue != "0")
                {
                    
                    CalcTDS = Convert.ToDouble(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSPer.Text)) * 0.01);
                    txtTDSAmt.Text = Convert.ToDecimal(CalcTDS).ToString();
                }
                //if (txtGSTAmount.Text != "")
                //{
                //    txtNetAmt.Text = Math.Round((Convert.ToDecimal(txtBillAmt.Text) + Convert.ToDecimal(txtGSTAmount.Text)) - Convert.ToDecimal(txtTDSAmt.Text)).ToString();
                //}
            }
        }
        else
        {
            txtTDSAmt.Text = txtTDSPer.Text = txtPanNo.Text = string.Empty;
            dvSection.Visible = false;

            ddlSection.SelectedValue = "0";
        }

        AddGstAmount();

    }

    //protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    //{
    //    decimal CalcTDS = 0;
    //    if (chkTDSApplicable.Checked)
    //    {
    //        dvSection.Visible = true;
    //        if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
    //        {
    //            if (ddlSection.SelectedValue != "0")
    //            {
    //                CalcTDS = Math.Round(Convert.ToDecimal(txtBillAmt.Text) * (Convert.ToDecimal(txtTDSPer.Text)) * 0.01);
    //                txtTDSAmt.Text = CalcTDS.ToString();
    //            }
    //            if (txtGSTAmount.Text != "")
    //            {
    //                txtTotalBillAmt.Text = Math.Round((Convert.ToDecimal(txtBillAmt.Text) + Convert.ToDecimal(txtGSTAmount.Text)) - Convert.ToDecimal(txtTDSAmt.Text)).ToString();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        txtNetAmt.Text = Math.Round(Convert.ToDecimal(Convert.ToDecimal(txtNetAmt.Text == "" ? "0" : txtNetAmt.Text)) + (Convert.ToDecimal(txtTDSAmt.Text == "" ? "0" : txtTDSAmt.Text))).ToString();
    //        dvSection.Visible = false;
    //        txtTDSAmt.Text = txtTDSPer.Text = txtPanNo.Text = string.Empty;
    //        ddlSection.SelectedValue = "0";

    //    }
    //}

    //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
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
            txtTDSPer.Text = "";
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
                txtTDSPer.Text = "";
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlSelect.SelectedValue == "A")
            //{
            //    if (chkTDSApplicable.Checked)
            //    {
            //        if (txtTDSLedger.Text == "" || txtTDSLedger.Text == string.Empty)
            //        {
            //            objCommon.DisplayMessage(UPDLedger, "Please Select TDS Ledger...!", this.Page);
            //            txtTDSLedger.Focus();
            //            return;
            //        }
            //    }
            //    if (txtBankLedger.Text == "" || txtBankLedger.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage(UPDLedger, "Please Select Cash/Bank Ledger...!", this.Page);
            //        txtBankLedger.Focus();
            //        return;
            //    }

            //    if (txtLedgerHead.Text == "" || txtLedgerHead.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage(UPDLedger, "Please Select Other Ledger...!", this.Page);
            //        txtLedgerHead.Focus();
            //        return;
            //    }
            //}

            this.Enable();

            ObjRPB.SERIAL_NO = Convert.ToInt32(lblSerialNo.Text);
            ObjRPB.COMPANY_NO = Convert.ToInt32(ddlSelectCompany.SelectedValue);
            ObjRPB.ACCOUNT = ddlAccount.SelectedValue;
            ObjRPB.DEPT_ID = Convert.ToInt32(ddlDeptBranch.SelectedValue);
            ObjRPB.APPROVAL_NO = txtApprovalNo.Text;
            ObjRPB.APPROVAL_DATE = Convert.ToDateTime(txtApprovalDate.Text);
            ObjRPB.APPROVED_BY = lblApprovedBy.Text;
            ObjRPB.SUPPLIER_NAME = txtServiceName.Text.ToString();
            ObjRPB.PAYEE_NAME_ADDRESS = txtPayeeNameAddress.Text.ToString();
            ObjRPB.NATURE_SERVICE = txtNatureOfService.Text.ToString();
            ObjRPB.GSTIN_NO = txtGSTINNo.Text.ToString() == string.Empty ? "-" : txtGSTINNo.Text.ToString();
            ObjRPB.BILL_AMT = Convert.ToDouble(txtBillAmt.Text);
            ObjRPB.GST_AMT = Convert.ToDouble(txtGSTAmount.Text);
            ObjRPB.TOTAL_BILL_AMT = Convert.ToDouble(txtTotalBillAmt.Text);
            ObjRPB.COMPANY_CODE = Session["BillComp_Code"].ToString();
            ObjRPB.NET_AMT = Convert.ToDouble(txtNetAmt.Text);
            ObjRPB.INVOICEDATE = txtInvoiceDate.Text;
            ObjRPB.INVOICENO = txtInvoiceNo.Text;
            ObjRPB.BUDGET_NO = Convert.ToInt32(ddlBudgethead.SelectedValue);

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

                if (txtTDSonGSTLedger.Text == "" || txtTDSonGSTLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonGST Ledger", this.Page);
                    return;
                    //ObjRPB.TDSonGSTLedgerId = 0;
                }
                else
                {
                    ObjRPB.TDSonGSTLedgerId = Convert.ToInt32(txtTDSonGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                }
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

            ObjRPB.REMARK = txtRemark.Text.ToString();

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
                    objCommon.DisplayMessage(UPDLedger, "Cannot be Forwarded, something is wrong..!", this.Page);
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
                int ret = objRPBController.ApprovePendingBill(ObjRPB, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(Session["colcode"].ToString()), status, remarks);

                if (ret == 1)
                {
                    objCommon.DisplayMessage(UPDLedger, "Bill Forwarded to Next Authority Successfully!", this.Page);

                    BindPendingBillList();
                    pnlBillDetails.Visible = false;
                    pnlBillList.Visible = true;
                    dvbuttons.Visible = false;
                }
                else if (ret == 2)
                {
                    objCommon.DisplayMessage(UPDLedger, "Record Approved Successfully!", this.Page);

                    BindPendingBillList();
                    pnlBillDetails.Visible = false;
                    pnlBillList.Visible = true;
                    dvbuttons.Visible = false;

                }
                else if (ret == 3)
                {
                    objCommon.DisplayMessage(UPDLedger, "Bill Return Successfully!", this.Page);

                    BindPendingBillList();
                    pnlBillDetails.Visible = false;
                    pnlBillList.Visible = true;
                    dvbuttons.Visible = false;
                }
            }
            txtApproveRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_PendingBillApproval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlBillDetails.Visible = false;
        pnlBillList.Visible = true;
        dvbuttons.Visible = false;
        ViewState["strCombine"] = string.Empty;
        txtApproveRemarks.Text = string.Empty;
    }

    #endregion

    #region private Methods

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //BindPendingBillList();
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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PendingBillForward.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PendingBillForward.aspx");
        }
    }

    private void BindPendingBillList()
    {
        try
        {
            DataSet ds = objRPBController.GetPendingBillList(Convert.ToInt32(Session["userno"].ToString()),Session["Comp_code"].ToString());
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                // dpPager.Visible = true;
            }
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

    private string stringCombine()
    {
        string strCombine = string.Empty;

        strCombine = lblSerialNo.Text + ddlSelectCompany.SelectedItem.Text + ddlAccount.SelectedItem.Text + ddlDeptBranch.SelectedItem.Text + txtApprovalNo.Text;
        strCombine = strCombine + Convert.ToDateTime(txtApprovalDate.Text).ToString("yyyy-MM-dd") + lblApprovedBy.Text + ddlBudgethead.SelectedItem.Text + txtLedgerHead.Text;
        strCombine = strCombine + rdbBillList.SelectedValue + txtInvoiceNo.Text + txtInvoiceDate.Text + txtNatureOfService.Text + txtServiceName.Text + txtPayeeNameAddress.Text + txtGSTINNo.Text;
        strCombine = strCombine + txtBillAmt.Text + (chkTDSApplicable.Checked ? "1" : "0").ToString() + ddlSection.SelectedItem.Text + txtTDSPer.Text + txtTDSAmt.Text + txtPanNo.Text;
        strCombine = strCombine + txtGSTAmount.Text + txtTotalBillAmt.Text + txtNetAmt.Text + txtRemark.Text;

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
        txtNetAmt.Enabled = true;
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
        txtNetAmt.Enabled = false;
    }

    private void BindBudget()
    {
        if (Convert.ToInt32(ddlSelectCompany.SelectedValue) > 0)
        {
            //objCommon.FillDropDownList(ddlBudgethead, "ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");
            //ADDED BY VIJAY ANDOJU FOR NEW BUDGETHEAD ON 02092020
            CostCenterController objbud = new CostCenterController();
            objbud.BindBudgetHead(ddlBudgethead);



        }
        else
        {

        }
    }

    private void ShowDetails(int BillNo)
    {
        DataSet ds = new DataSet();


        try
        {
            string Comp_code = objCommon.LookUp("Acc_Company", "Company_Code", "Company_No = " + Convert.ToInt32(ddlSelectCompany.SelectedValue));

            ds = objRPBController.GetSingleRecordsPendingBill(BillNo, Convert.ToInt32(Session["userno"].ToString()), Comp_code);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BillNo"] = BillNo;
                lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                lblBillId.Text = ds.Tables[0].Rows[0]["bill_id"].ToString();
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
                //string Path = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "PAPATH", "idno = " + EMPNO + " AND DEPTNO = " + Convert.ToInt32(ds.Tables[0].Rows[0]["DEPT_ID"].ToString()));
                string Path = "->Finance Branch->AO->FO";

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

                        //string FirstAuth = objCommon.LookUp("ACC_PASSING_AUTHORITY APA Inner Join ACC_PASSING_AUTHORITY_PATH APAP ON APA.PANO = APAP.PAN01 Inner Join User_Acc UA ON APAP.idno = UA.UA_IDNo", "PANAME", "UA.UA_NO =" + Convert.ToInt32(Session["userno"]) + " And APAP.DEPTNO =" + Convert.ToInt32(ddlDeptBranch.SelectedValue));
                        //lblApprovedBy.Text = FirstAuth.ToString();
                        //lblApprovedBy.Font.Bold = true;

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

                //lblAuthorityPath.Text = Path;
                //dvAuthorityPath.Visible = true;
                //dvApproval.Visible = true;
                txtApprovalNo.Text = ds.Tables[0].Rows[0]["APPROVAL_NO"].ToString();
                txtApprovalDate.Text = ds.Tables[0].Rows[0]["APPROVAL_DATE"].ToString();
                lblApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();

                ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();


                string company_code = objCommon.LookUp("acc_company", "COMPANY_CODE", "COMPANY_NO=" + ddlSelectCompany.SelectedValue);
               DataSet ds1 = objRPBController.GetBudegetBalanceNEW(Convert.ToInt32(ddlDeptBranch.SelectedValue), Convert.ToInt32(ddlBudgethead.SelectedValue), DateTime.Today);
               if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds1.Tables[0].Rows[0]["PRICE"].ToString()));
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

                        if (Convert.ToDecimal(dtr["BALANCE"].ToString()) >= 0)
                        {
                            lblLedgerClBal.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
                            //lblCur2.Text = "Dr";
                        }
                        else
                        {
                            lblLedgerClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDecimal(dtr["BALANCE"].ToString().Trim())) + " Cr";
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

                rdbBillList.SelectedValue = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                txtNatureOfService.Text = ds.Tables[0].Rows[0]["NATURE_SERVICE"].ToString();
                txtPayeeNameAddress.Text = ds.Tables[0].Rows[0]["PAYEE_NAME_ADDRESS"].ToString();
                txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
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

                if (IsTDS == 1)
                {
                    chkTDSApplicable.Checked = true;
                    dvSection.Visible = true;
                    txtTdsOnAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_ON_AMT"]).ToString(); // Added By Akshay On 27-06-2022
                    txtTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_AMT"]).ToString();
                    ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION_NO"].ToString();
                    txtTDSPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_PERCENT"]).ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                }
                else
                {
                    chkTDSApplicable.Checked = false;
                    dvSection.Visible = false;
                    txtTDSAmt.Text = "";
                    //ddlSection.SelectedValue = "0";
                    txtTDSPer.Text = "";
                    txtPanNo.Text = "";
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
                BindSection();
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

                }
                txtTDSCGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSCGST_ON_AMT"].ToString();
                ddlTDSonCGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONCGST_SECTION"].ToString();
                txtTDSonCGSTPer.Text = ds.Tables[0].Rows[0]["TDSONCGSTPER"].ToString();
                txtTDSonCGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONCGST_AMOUNT"].ToString();

                txtTDSSGSTonAmount.Text = ds.Tables[0].Rows[0]["TDSSGST_ON_AMT"].ToString();
                ddlTDSonSGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONSGST_SECTION"].ToString();
                txtTDSonSGSTPer.Text = ds.Tables[0].Rows[0]["TDSONSGSTPER"].ToString();
                txtTDSonSGSTAmount.Text = ds.Tables[0].Rows[0]["TDSONSGST_AMOUNT"].ToString();
                txtTotalTDSAmt.Text = ds.Tables[0].Rows[0]["TOTAL_TDS_AMT"].ToString();

                //divTdsOnCGST.Visible = true;
                //divTDSOnSGST.Visible = true;
                
             //-------------------------------------------------------------------------------------------------

                txtTotalBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_BILL_AMT"]).ToString();
                txtGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GST_AMT"]).ToString();
                txtNetAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_AMT"]).ToString();

                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();

                string strCombinefirst = stringCombine();
                ViewState["strCombine"] = strCombinefirst;

                pnlBillDetails.Visible = true;
                pnlBillList.Visible = false;
                dvbuttons.Visible = true;

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

    //protected void ddlBudgethead_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataSet ds = null;
    //        ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD BH Left Join ACC_" + Session["BillComp_Code"].ToString() + "_TRANS T ON BH.BUDG_NO = T.BudgetNo", "BH.BUDG_NO,BH.BUDG_NAME", "(ISNULL(BH.BUD_AMT,0) - ISNULL(SUM(Case when T.[TRAN] = 'Dr' Then T.AMOUNT When T.[TRAN] = 'Cr' Then -(T.AMOUNT) End),0)) As BUD_BAL_AMT", "BH.BUDG_NO =" + Convert.ToInt32(ddlBudgethead.SelectedValue) + " Group By BH.BUDG_NO,BH.BUDG_NAME,BH.BUD_AMT", "");

    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                if (Convert.ToDecimal(ds.Tables[0].Rows[0]["BUD_BAL_AMT"].ToString()) >= 0)
    //                {
    //                    lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["BUD_BAL_AMT"].ToString())) + " Dr";
    //                }
    //                else
    //                {
    //                    lblBudgetClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDecimal(ds.Tables[0].Rows[0]["BUD_BAL_AMT"].ToString())) + " Cr";
    //                }
    //            }
    //            else
    //            {
    //                lblBudgetClBal.Text = "0.00";
    //            }
    //        }
    //        txtLedgerHead.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
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
                    lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["BALANCE"].ToString()));
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

            if (Convert.ToDecimal(dtr["BALANCE"].ToString()) >= 0)
            {
                lblLedgerClBal.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
                //lblCur2.Text = "Dr";
            }
            else
            {
                lblLedgerClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDecimal(dtr["BALANCE"].ToString().Trim())) + " Cr";
                //lblCur2.Text = "Cr";
            }
            //ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
        }
        txtInvoiceNo.Focus();
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

    #endregion

    #region Web services

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

    #endregion

    protected void chkGST_CheckedChanged(object sender, EventArgs e)
    {
        txtIgstAmount.Text = txtIgstPer.Text = string.Empty;
        if (chkGST.Checked == true)
        {
            chkIGST.Checked = false;
            dvSgst.Visible = true;
            dvCgst.Visible = true;
            dvIGST.Visible = false;
        }
        else
        {
            dvSgst.Visible = false;
            dvCgst.Visible = false;
            txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = string.Empty;
            AddGstAmount();
        }

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
        txtNetAmt.Text = Convert.ToDecimal(amount - Tdsamount + gstAmount).ToString();
        txtGSTAmount.Text = gstAmount.ToString();
    }
    protected void ddlCgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCgstSection.SelectedValue == "0")
        {
            txtCGSTPER.Text = "";
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlCgstSection.SelectedValue)));
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
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSgstSection.SelectedValue)));
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
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlIgstSection.SelectedValue)));
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

    // ------------------------Added By Akshay Dixit On 03-05-2022-----------------------------

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void BindSection()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
            objCommon.FillDropDownList(ddlCgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
            objCommon.FillDropDownList(ddlSgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
            objCommon.FillDropDownList(ddlIgstSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
            objCommon.FillDropDownList(ddlTDSonGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
            objCommon.FillDropDownList(ddlTDSonCGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
            objCommon.FillDropDownList(ddlTDSonSGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");


        }
        catch (Exception ex)
        {
            throw ex;
        }

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

    //--------------------------------------------------------------------------------------------
}