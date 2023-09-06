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
public partial class AbstractBillVouchersPayment : System.Web.UI.Page
{
    AccountTransactionController objTranController = new AccountTransactionController();
    AccountTransaction objTran = new AccountTransaction();
    Common objCommon = new Common();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        { }
        else
            Response.Redirect("~/Default.aspx");
        if (!Page.IsPostBack)
        {
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    objCommon.DisplayMessage("Select company/cash book.", this);
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                    //Fill dropdown list
                    // Filling Degrees

                    //Filling Recept list
                    ViewState["Operation"] = "Submit";
                }
            }
            Session["WithoutCashBank"] = "N";
            setcolum();
            setheadcolum();
            setcolumwithoutsession();
            ViewState["NewDeductGrid"] = Session["DeductionGrid"];
            objCommon.FillDropDownList(ddlDeductionHead, "ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT", "ACC_ID", "Acc_Name", "", "");
            //lblAmountPayble.Text = "0.00";
            //btnHeadAmountAdd.Visible = false;
            if (Request.QueryString["BillNo"] != null)
            {
                getTransaction(Convert.ToInt32(Request.QueryString["BillNo"].ToString()), Request.QueryString["Voucher_Type"].ToString());
                btnSave.Enabled = true;
                btnHeadAmountAdd.Visible = true;
                ddlVoucherType.Enabled = false;
            }
            //txtPer.Enabled = false;
            ViewState["CheckStatus"] = "N";
        }
    }
    private DataTable setcolum()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DeductHeadName");
        dt.Columns.Add("DeductAmount");
        dt.Columns.Add("DeductHeadId");
        dt.Columns.Add("DeductPercent");
        dt.Columns.Add("HeadId");
        Session["DeductionGrid"] = dt;

        return dt;
    }
    private DataTable setcolumwithoutsession()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DeductHeadName");
        dt.Columns.Add("DeductAmount");
        dt.Columns.Add("DeductHeadId");
        dt.Columns.Add("DeductPercent");
        dt.Columns.Add("HeadId");
        Session["GridSessionDeduction"] = dt;

        return dt;
    }
    private DataTable setViewstatecolumsession()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DeductHeadName");
        dt.Columns.Add("DeductAmount");
        dt.Columns.Add("DeductHeadId");
        dt.Columns.Add("DeductPercent");
        dt.Columns.Add("HeadId");
        ViewState["NewDeductGrid"] = dt;

        return dt;
    }
    private DataTable setheadcolum()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("HeadName");
        dt.Columns.Add("HeadAmount");
        dt.Columns.Add("HeadId");
        Session["HeadGrid"] = dt;
        return dt;
    }
    private void getTransaction(int billNo, string voucher_type)
    {
        DataSet dsHeadAccount = null; DataSet dsDeductAcc = null;
        dsHeadAccount = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + voucher_type + " a inner join acc_" + Session["comp_code"].ToString() + "_party b on (a.AccountID=b.party_no)", "a.AccountID HeadId,a.GROSS_AMOUNT,b.party_Name  HeadName,a.AMOUNT as HeadAmount,a.Narration ", "a.DisplayName_HeadAcc", "ABS_BILL_NO='" + billNo + "' and Head_AccountId is null", "");
        if (dsHeadAccount.Tables[0].Rows.Count > 0)
        {
            ViewState["CheckStatus"] = "Y";
            ddlVoucherType.SelectedValue = "PV";
            ddlVoucherType.Enabled = false;
            GridHeadData.DataSource = dsHeadAccount;
            GridHeadData.DataBind();
            for (int i = 0; i < GridHeadData.Rows.Count; i++)
            {
                GridView GridData = GridHeadData.Rows[i].FindControl("GridData") as GridView;
                HiddenField hdnHeadNo = GridHeadData.Rows[i].FindControl("hdnHeadNo") as HiddenField;

                dsDeductAcc = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + voucher_type + " a  inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "a.AccountID HeadId,c.Acc_Name as DeductHeadName,a.AMOUNT as DeductAmount,a.Head_AccountId as DeductHeadId,a.TRAN_TYPE,case when a.TaxPercent is null then 0 else a.TaxPercent end DeductPercent", "a.Tot_Payable,a.Narration", "ABS_BILL_NO='" + billNo + "' and AccountID='" + hdnHeadNo.Value + "'", "");
                GridData.DataSource = dsDeductAcc.Tables[0];
                GridData.DataBind();
            }

            //lblAmountPayble.Text = dsHeadAccount.Tables[0].Rows[0]["Tot_Payable"].ToString();
            txtGAmount.Text = dsHeadAccount.Tables[0].Rows[0]["GROSS_AMOUNT"].ToString();
            //lblAmountPayble.Text = dsHeadAccount.Tables[0].Rows[0]["GROSS_AMOUNT"].ToString();
            txtNarration.Text = dsHeadAccount.Tables[0].Rows[0]["Narration"].ToString();
            txDisplayName.Text = dsHeadAccount.Tables[0].Rows[0]["DisplayName_HeadAcc"].ToString();
            DataSet dsSessionHeadAccount = null;
            dsSessionHeadAccount = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + voucher_type + " a inner join acc_" + Session["comp_code"].ToString() + "_party b on (a.AccountID=b.party_no)", "a.AccountID HeadId,b.party_Name  HeadName,a.AMOUNT as HeadAmount", "", "ABS_BILL_NO='" + billNo + "' and Head_AccountId is null", "");
            Session["HeadGrid"] = dsSessionHeadAccount.Tables[0];
            ViewState["CheckStatus"] = "N";
        }
    }
    //protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "VoucherView")
    //    {
    //        if (ViewState["Voucher_Type"].ToString() == "PV")
    //        {
    //            Response.Redirect("AbstractBillVouchersPayment.aspx?BillNo=" + e.CommandArgument.ToString() + "&Voucher_Type=" + ViewState["Voucher_Type"].ToString());
    //        }
    //        else
    //            Response.Redirect("AbstractBillVouchers.aspx?BillNo=" + e.CommandArgument.ToString() + "&Voucher_Type=" + ViewState["Voucher_Type"].ToString());
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int retStatus;
        string billNo = string.Empty;
        if (Request.QueryString["BillNo"] == null)
        {
            billNo = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlVoucherType.SelectedValue.ToString(), "max(CAST([ABS_BILL_NO] AS INT))+1", "");
        }
        if (Request.QueryString["BillNo"] != null)
        {
            objTranController.deleteBillNoForUpdate(Convert.ToInt32(Request.QueryString["BillNo"]), Session["comp_code"].ToString(), ddlVoucherType.SelectedValue.ToString());
            billNo = Request.QueryString["BillNo"].ToString();
        }

        try
        {
            for (int i = 0; i < GridHeadData.Rows.Count; i++)
            {
                if (GridHeadData.Rows[i].RowType.Equals(DataControlRowType.DataRow))
                {

                    decimal DeductAmount = 0;
                    GridView GvDeductionGrid = GridHeadData.Rows[i].FindControl("GridData") as GridView;
                    Label lblGridHeadAmout = GridHeadData.Rows[i].FindControl("lblAmount") as Label;
                    objTran.VOUCHER_TYPE = ddlVoucherType.SelectedValue;
                    Session["Voucher_Type"] = objTran.VOUCHER_TYPE.ToString();
                    objTran.GROSSAMOUNT = Convert.ToDouble(lblAmountPayble.Text);
                    //DropDownList ddlTranType = GridHeadData.Rows[i].FindControl("ddlTran") as DropDownList;
                    //objTran.TRANSACTION_TYPE = ddlTranType.SelectedValue;
                    if (lblGridHeadAmout.Text != string.Empty)
                        objTran.AMOUNT = Convert.ToDouble(lblGridHeadAmout.Text);
                    else
                        objTran.AMOUNT = 0.00;
                    for (int j = 0; j < GvDeductionGrid.Rows.Count; j++)
                    {
                        if (GvDeductionGrid.Rows[j].RowType.Equals(DataControlRowType.DataRow))
                        {
                            Label lblDeductAmt = GvDeductionGrid.Rows[j].FindControl("lblAmount") as Label;
                            if (lblDeductAmt.Text != "")
                                DeductAmount = DeductAmount + Convert.ToDecimal(lblDeductAmt.Text);
                        }
                    }

                    //string[] headAccountID = txtAcc.Text.Split('*');
                    //objTran.HEADACC = Convert.ToInt32(headAccountID[0]).ToString();
                    objTran.NARRATION = txtNarration.Text;
                    HiddenField hdnAccountID = GridHeadData.Rows[i].FindControl("hdnHeadNo") as HiddenField;
                    string HeadAccId = hdnAccountID.Value;
                    objTran.HEADACC = HeadAccId;
                    objTran.TOTALPAYBLE = (Convert.ToDecimal(lblGridHeadAmout.Text) - DeductAmount).ToString();
                    objTran.DisplayName = txDisplayName.Text;






                   



                    retStatus = objTranController.AddAbstractBillForPayment(objTran, Session["comp_code"].ToString(), billNo);
                    if (retStatus == 1)
                    {

                        for (int j = 0; j < GvDeductionGrid.Rows.Count; j++)
                        {
                            if (GvDeductionGrid.Rows[j].RowType.Equals(DataControlRowType.DataRow))
                            {
                                DeductAmount = 0;
                                HiddenField DeductHeadId = GvDeductionGrid.Rows[j].FindControl("hdnPartyNo") as HiddenField;
                                Label DeductHeadAmount = GvDeductionGrid.Rows[j].FindControl("lblAmount") as Label;
                                HiddenField DeductPer = GvDeductionGrid.Rows[j].FindControl("hdnTaxPercent") as HiddenField;
                                objTran.VOUCHER_TYPE = ddlVoucherType.SelectedValue;
                                Session["Voucher_Type"] = objTran.VOUCHER_TYPE.ToString();
                                DropDownList ddlTranType = GvDeductionGrid.Rows[j].FindControl("ddlTran") as DropDownList;
                                objTran.TRANSACTION_TYPE = ddlTranType.SelectedValue;
                                if (DeductHeadAmount.Text != string.Empty)
                                    objTran.AMOUNT = Convert.ToDouble(DeductHeadAmount.Text);
                                else
                                    objTran.AMOUNT = 0.00;
                                objTran.HEADACC = HeadAccId;
                                objTran.HEADACCOUNTID = Convert.ToInt32(DeductHeadId.Value).ToString();
                                objTran.DisplayName = txDisplayName.Text;
                                objTran.TaxPer = Convert.ToDecimal(DeductPer.Value);
                                retStatus = objTranController.AddAbstractBillForPaymentDeduction(objTran, Session["comp_code"].ToString(), billNo);
                            }
                            
                        }

                        
                       
                    }
                }
            }

            if (Request.QueryString["BillNo"] != null)
            {
                Response.Redirect("AbstractBillModification.aspx?Voucher_Type=" + Request.QueryString["Voucher_Type"].ToString());
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Transaction Save Successfully", this.Page);
                clear();
                Session["BillNo"] = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + objTran.VOUCHER_TYPE, "max(CAST([ABS_BILL_NO] AS INT))", "");
                btnPrint.Enabled = true;
               clear();
            }
            


        }
        catch (Exception ex)
        {
            //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
            objCommon.DisplayUserMessage(UPDLedger, ex.Message, this.Page);

        }

    }
    protected void btnHeadAmountAdd_Click(object sender, EventArgs e)
    {

        #region Outer grid
        DataTable dt = Session["HeadGrid"] as DataTable;
        string isDuplicate = "N";
        ViewState["CheckStatus"] = "N";
        string[] HeadName = txtAcc.Text.Split('*');
        if (Request.QueryString["Billno"] != null)
        {
            if (ViewState["RowIndex"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("DeductHeadName");
                dt1.Columns.Add("DeductAmount");
                dt1.Columns.Add("DeductHeadId");
                dt1.Columns.Add("DeductPercent");
                dt1.Columns.Add("HeadId");
                for (int i = 0; i < GridHeadData.Rows.Count; i++)
                {
                   
                        GridView DeductGrid = GridHeadData.Rows[i].FindControl("GridData") as GridView;
                        for (int j = 0; j < DeductGrid.Rows.Count; j++)
                        {
                            HiddenField hdnPartyNo = DeductGrid.Rows[j].FindControl("hdnPartyNo") as HiddenField;
                            HiddenField hdnTaxPercent = DeductGrid.Rows[j].FindControl("hdnTaxPercent") as HiddenField;
                            HiddenField hdnHeadAccID = DeductGrid.Rows[j].FindControl("hdnHeadAccID") as HiddenField;
                            Label lblName = DeductGrid.Rows[j].FindControl("lblName") as Label;
                            Label lblAmount = DeductGrid.Rows[j].FindControl("lblAmount") as Label;
                            dt1.Rows.Add();
                            dt1.Rows[dt1.Rows.Count-1]["DeductHeadName"] = lblName.Text;
                            dt1.Rows[dt1.Rows.Count-1]["DeductAmount"] = lblAmount.Text;
                            dt1.Rows[dt1.Rows.Count-1]["DeductHeadId"] = hdnPartyNo.Value;
                            dt1.Rows[dt1.Rows.Count-1]["DeductPercent"] = hdnTaxPercent.Value;
                            dt1.Rows[dt1.Rows.Count-1]["HeadId"] = hdnHeadAccID.Value;
                        }
                   
                }
                ViewState["DeducGrid"] = dt1;
            }
            if (dt.Rows.Count == 0)
            {
                int count = dt.Rows.Count;

                dt.Rows.Add();
                dt.Rows[count]["HeadName"] = HeadName[1];
                dt.Rows[count]["HeadAmount"] = txtHeadAmount.Text;
                dt.Rows[count]["HeadId"] = Convert.ToInt32(HeadName[0].ToString()).ToString();

                dt.AcceptChanges();
                //Label lblPayble = GridData.Rows[count].FindControl("lblPaybleAmount") as Label;

                //lblPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text)-Convert.ToDouble(txtAmount.Text));
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (GridHeadData.Rows.Count > 0)
                    {
                        if (GridHeadData.Rows[i].RowType.Equals(DataControlRowType.DataRow))
                        {
                            if (ViewState["RowIndex"] != null)
                            {
                                if (Convert.ToInt32(ViewState["RowIndex"]) == i)
                                {
                                    HiddenField hdnHeadNo1 = GridHeadData.Rows[i].FindControl("hdnHeadNo") as HiddenField;
                                    if (Convert.ToInt32(hdnHeadNo1.Value.ToString()) == Convert.ToInt32(HeadName[0].ToString()))
                                    {
                                        dt.Rows[i]["HeadName"] = HeadName[1];
                                        dt.Rows[i]["HeadAmount"] = txtHeadAmount.Text;
                                        dt.Rows[i]["HeadId"] = HeadName[0].ToString();

                                        dt.AcceptChanges();
                                        isDuplicate = "Y";
                                        GridHeadData.DataSource = dt;
                                        GridHeadData.DataBind();


                                        break;
                                    }
                                }
                            }
                            else
                            {
                                HiddenField hdnHeadNo1 = GridHeadData.Rows[i].FindControl("hdnHeadNo") as HiddenField;
                                if (Convert.ToInt32(hdnHeadNo1.Value.ToString()) == Convert.ToInt32(HeadName[0].ToString()))
                                {
                                    dt.Rows[i]["HeadName"] = HeadName[1];
                                    dt.Rows[i]["HeadAmount"] = txtHeadAmount.Text;
                                    dt.Rows[i]["HeadId"] = HeadName[0].ToString();

                                    dt.AcceptChanges();
                                    isDuplicate = "Y";
                                    GridHeadData.DataSource = dt;
                                    GridHeadData.DataBind();


                                    break;
                                }
                            }
                        }
                    }
                }
                if (isDuplicate == "N")
                {
                    int count = dt.Rows.Count;
                    dt.Rows.Add();
                    dt.Rows[count]["HeadName"] = HeadName[1];
                    dt.Rows[count]["HeadAmount"] = txtHeadAmount.Text;
                    dt.Rows[count]["HeadId"] = HeadName[0].ToString();
                    dt.AcceptChanges();
                }
            }
        }
        else
        {
            if (dt.Rows.Count == 0)
            {
                int count = dt.Rows.Count;

                dt.Rows.Add();
                dt.Rows[count]["HeadName"] = txtAcc.Text;
                dt.Rows[count]["HeadAmount"] = txtHeadAmount.Text;
                dt.Rows[count]["HeadId"] = Convert.ToInt32(HeadName[0].ToString()).ToString();

                dt.AcceptChanges();
                //Label lblPayble = GridData.Rows[count].FindControl("lblPaybleAmount") as Label;
                
                //lblPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text)-Convert.ToDouble(txtAmount.Text));
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (GridHeadData.Rows.Count > 0)
                    {
                        if (GridHeadData.Rows[i].RowType.Equals(DataControlRowType.DataRow))
                        {
                            
                            HiddenField hdnHeadNo1 = GridHeadData.Rows[i].FindControl("hdnHeadNo") as HiddenField;
                            if (ViewState["RowIndex"] != null)
                            {
                                if (i != Convert.ToInt32(ViewState["RowIndex"]))
                                {
                                    DataSet ds = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + Request.QueryString["Voucher_Type"].ToString() + " a  inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "a.AccountID HeadId,c.Acc_Name as DeductHeadName,a.Head_AccountId as DeductHeadId,a.AMOUNT as DeductAmount,case when a.TaxPercent is null then 0 else a.TaxPercent end DeductPercent", "", "ABS_BILL_NO='" + Request.QueryString["Billno"].ToString() + "' and AccountID='" + hdnHeadNo1.Value.ToString() + "' and Head_AccountId is not null ", "");
                                    Session["DeductionGrid"] = ds.Tables[0];
                                }
                            }
                            if (Convert.ToInt32(hdnHeadNo1.Value.ToString()) == Convert.ToInt32(HeadName[0].ToString()))
                            {
                                dt.Rows[i]["HeadName"] = txtAcc.Text;
                                dt.Rows[i]["HeadAmount"] = txtHeadAmount.Text;
                                dt.Rows[i]["HeadId"] = HeadName[0].ToString();

                                dt.AcceptChanges();
                                isDuplicate = "Y";
                                GridHeadData.DataSource = dt;
                                GridHeadData.DataBind();


                                break;
                            }
                        }
                    }
                }
                if (isDuplicate == "N")
                {
                    int count = dt.Rows.Count;
                    dt.Rows.Add();
                    dt.Rows[count]["HeadName"] = txtAcc.Text;
                    dt.Rows[count]["HeadAmount"] = txtHeadAmount.Text;
                    dt.Rows[count]["HeadId"] = HeadName[0].ToString();
                    dt.AcceptChanges();
                }
            }
        }
        GridHeadData.DataSource = dt;
        GridHeadData.DataBind();



        //for (int i = 0; i < GridHeadData.Rows.Count; i++)
        //{
        //Label lblAmount = GridHeadData.Rows[i].FindControl("lblAmount") as Label;
        //if (txtHeadAmount.Text != string.Empty)
        //{
        //    if (txtGAmount.Text != string.Empty)
        //    {
        //        txtGAmount.Text = (Convert.ToDouble(txtGAmount.Text.Trim()) + Convert.ToDouble(txtHeadAmount.Text)).ToString();
        //    }
        //    else
        //        txtGAmount.Text = txtHeadAmount.Text;
        //}
        //else
        //    txtGAmount.Text = "0.00";
        //}

        //lblAmountPayble.Text = (Convert.ToDecimal(txtHeadAmount.Text) + Convert.ToDecimal(lblAmountPayble.Text)).ToString();

        ddlVoucherType.Enabled = false;
        txtGAmount.Enabled = false;
        txtAcc.Text = string.Empty;
        txtHeadAmount.Text = string.Empty;
        //txtGAmount.Text = string.Empty;
        ddlDeductionHead.SelectedIndex = -1;
        txtPer.Text = string.Empty;
        txtAmount.Text = string.Empty;
        GridDataDeduction.DataSource = null;
        GridDataDeduction.DataBind();
        DataTable dtClear = Session["GridSessionDeduction"] as DataTable;
        dtClear.Rows.Clear();
        Session["GridSessionDeduction"] = dtClear;
        //if (lblAmountPayble.Text == string.Empty)
        //    lblAmountPayble.Text = "0.00";
        if (txtAmount.Text == string.Empty)
            txtAmount.Text = "0.00";
        lblAmountPayble.Text = "0.00";
        for (int i = 0; i < GridHeadData.Rows.Count; i++)
        {
            Label lblAmount = GridHeadData.Rows[i].FindControl("lblAmount") as Label;
            lblAmountPayble.Text = (Convert.ToDecimal(lblAmount.Text) + Convert.ToDecimal(lblAmountPayble.Text)).ToString();
        }



        #endregion

    }
    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {
        string[] ledgerName = txtAcc.Text.Split('¯');
        txtAcc.Text = ledgerName[0].ToString();
    }
    private void clear()
    {
        //ddlVoucherType.SelectedValue = "0";
        txtAcc.Text = string.Empty;
        txtGAmount.Text = string.Empty;
        ddlDeductionHead.SelectedValue = "0";
        txtAmount.Text = string.Empty;
        //GridData.DataSource = null;
        //GridData.DataBind();
        txtNarration.Text = string.Empty;
        setcolum();
        setheadcolum();
        //lblAmountPayble.Text = "0.00";
        ddlVoucherType.Enabled = true;
        txtAcc.Enabled = true;
        txtGAmount.Enabled = true;
        btnSave.Enabled = false;
        TrAdvance.Visible = false;
        //TrBillAmount.Visible = false;
        txDisplayName.Text = string.Empty;
        txtPer.Text = string.Empty;
        lblAmountPayble.Text = "0.00";
        GridHeadData.DataSource = null;
        GridHeadData.DataBind();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMP_CODE=" + Session["comp_code"].ToString() + "," + "@P_VOUCHER_NO=" + Session["BillNo"].ToString().Trim() + "," + "@P_VOUCHER_TYPE=" + Session["Voucher_Type"].ToString().Trim();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        AccountTransaction objTranCont = new AccountTransaction(); AccountTransactionController objTran = new AccountTransactionController();
        objTranCont.VOUCHER_TYPE = Session["Voucher_Type"].ToString();
        objTranCont.BILLNO = Convert.ToInt32(Session["BillNo"].ToString().Trim());
        objTranCont.COMPANY_CODE = Session["comp_code"].ToString();
        objTran.SetAbstractBillForPayment_Report(objTranCont);
        if (Session["Voucher_Type"].ToString() == "PV")
        {

            ShowReport("Abstract Bill Voucher", "VoucherPrintNewPaymentFormat.rpt");
        }

        btnPrint.Enabled = false;
    }
    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVoucherType.SelectedValue == "AAPV")
        {
            TrAdvance.Visible = true;
            //TrBillAmount.Visible = true;

        }
        else
        {
            TrAdvance.Visible = false;
            //TrBillAmount.Visible = false;
        }
    }
    protected void ddlDeductionHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeductionHead.SelectedValue == "0")
        {
            txtPer.Enabled = false;
            txtPer.Text = "0";
        }
        else
        {
            txtPer.Enabled = true;
        }
    }
    protected void btnSaveDeduction_Click(object sender, EventArgs e)
    {
        #region HeadAmountAdd previous

        //DataTable dt = Session["HeadGrid"] as DataTable;
        //string isDuplicate = "N";
        //string[] HeadName = txtAcc.Text.Split('*');

        //if (dt.Rows.Count == 0)
        //{
        //    int count = dt.Rows.Count;

        //    dt.Rows.Add();
        //    dt.Rows[count]["HeadName"] = txtAcc.Text;
        //    dt.Rows[count]["HeadAmount"] = txtHeadAmount.Text;
        //    dt.Rows[count]["HeadId"] = Convert.ToInt32(HeadName[0].ToString()).ToString();

        //    dt.AcceptChanges();
        //    //Label lblPayble = GridData.Rows[count].FindControl("lblPaybleAmount") as Label;

        //    //lblPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text)-Convert.ToDouble(txtAmount.Text));
        //}
        //else
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        HiddenField hdnHeadNo1 = GridHeadData.Rows[i].FindControl("hdnHeadNo") as HiddenField;
        //        if (hdnHeadNo1.Value.ToString() == HeadName[0].ToString())
        //        {
        //            dt.Rows[i]["HeadName"] = txtAcc.Text;
        //            dt.Rows[i]["HeadAmount"]  = txtHeadAmount.Text;
        //            dt.Rows[i]["HeadId"] =  HeadName[0].ToString();

        //            dt.AcceptChanges();
        //            isDuplicate = "Y";
        //            GridHeadData.DataSource = dt;
        //            GridHeadData.DataBind();


        //            break;
        //        }
        //    }
        //    if (isDuplicate == "N")
        //    {
        //        int count = dt.Rows.Count;
        //        dt.Rows.Add();
        //        dt.Rows[count]["HeadName"] = txtAcc.Text;
        //        dt.Rows[count]["HeadAmount"] = txtHeadAmount.Text;
        //        dt.Rows[count]["HeadId"] = HeadName[0].ToString();
        //        dt.AcceptChanges();
        //    }
        //}
        //GridHeadData.DataSource = dt;
        //GridHeadData.DataBind();



        ////for (int i = 0; i < GridHeadData.Rows.Count; i++)
        ////{
        //    //Label lblAmount = GridHeadData.Rows[i].FindControl("lblAmount") as Label;
        //    if (txtHeadAmount.Text != string.Empty)
        //    {
        //        if (txtGAmount.Text != string.Empty)
        //        {
        //            txtGAmount.Text = (Convert.ToDouble(txtGAmount.Text.Trim()) + Convert.ToDouble(txtHeadAmount.Text)).ToString();
        //        }
        //        else
        //            txtGAmount.Text = txtHeadAmount.Text;
        //    }
        //    else
        //        txtGAmount.Text = "0.00";
        ////}


        //ddlVoucherType.Enabled = false;
        //txtGAmount.Enabled = false;
        //txtAcc.Text = string.Empty;
        //txtHeadAmount.Text = string.Empty;

        #endregion

        #region deduction add button

        string[] HeadName = txtAcc.Text.Split('*');
        DataTable dt = Session["DeductionGrid"] as DataTable;
        DataTable ViewDeductstate = ViewState["NewDeductGrid"] as DataTable;
        string isDuplicate = "N";
        DataTable Newdt = Session["GridSessionDeduction"] as DataTable;
        if (Request.QueryString["BillNo"] != null)
        {
            Newdt = Session["DeductionGrid"] as DataTable;
            
            if (dt.Rows.Count == 0)
            {
                int count = dt.Rows.Count;
                Newdt.Rows.Add();
                Newdt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                Newdt.Rows[count]["DeductAmount"] = txtAmount.Text;
                Newdt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                Newdt.Rows[count]["DeductPercent"] = txtPer.Text;
                Newdt.Rows[count]["HeadId"] = HeadName[0].ToString();
                Newdt.AcceptChanges();
                Session["GridSessionDeduction"] = Newdt;

            }
            else
            {
                for (int i = 0; i < Newdt.Rows.Count; i++)
                {
                    if (GridDataDeduction.Rows.Count > 0)
                    {
                        HiddenField hdnDeductID = GridDataDeduction.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                        HiddenField hdnHeadID = GridDataDeduction.Rows[i].FindControl("hdnHeadAccID") as HiddenField;
                        if (hdnDeductID.Value == ddlDeductionHead.SelectedValue)
                        {
                            Newdt.Rows[i]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                            Newdt.Rows[i]["DeductAmount"] = txtAmount.Text;
                            Newdt.Rows[i]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                            Newdt.Rows[i]["DeductPercent"] = txtPer.Text;
                            Newdt.Rows[i]["HeadId"] = hdnHeadID.Value;
                            Newdt.AcceptChanges();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j]["DeductHeadId"].ToString() == ddlDeductionHead.SelectedValue)
                                {
                                    dt.Rows[j]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                                    dt.Rows[j]["DeductAmount"] = txtAmount.Text;
                                    dt.Rows[j]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                                    dt.Rows[j]["DeductPercent"] = txtPer.Text;
                                    dt.Rows[j]["HeadId"] = hdnHeadID.Value;
                                    dt.AcceptChanges();
                                }
                            }
                            isDuplicate = "Y";
                            GridDataDeduction.DataSource = Newdt;
                            GridDataDeduction.DataBind();
                            btnSave.Enabled = true;

                            break;
                        }
                    }
                }

                if (isDuplicate == "N")
                {
                    int count = Newdt.Rows.Count;
                    Newdt.Rows.Add();
                    Newdt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                    Newdt.Rows[count]["DeductAmount"] = txtAmount.Text;
                    Newdt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                    Newdt.Rows[count]["DeductPercent"] = txtPer.Text;
                    Newdt.Rows[count]["HeadId"] = HeadName[0].ToString();
                    Newdt.AcceptChanges();
                    Session["GridSessionDeduction"] = Newdt;
                }

            }
        }
        else
        {
            if (dt.Rows.Count == 0)
            {
                int count = dt.Rows.Count;
                dt.Rows.Add();
                dt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                dt.Rows[count]["DeductAmount"] = txtAmount.Text;
                dt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                dt.Rows[count]["DeductPercent"] = txtPer.Text;
                dt.Rows[count]["HeadId"] = HeadName[0].ToString();
                dt.AcceptChanges();
                Session["GridSessionDeduction"] = dt;
                var rows = ViewDeductstate.Select("HeadId='" + HeadName[0].ToString() + "' AND DeductHeadid='" + ddlDeductionHead.SelectedValue + "'");
                foreach (DataRow dr in rows)
                {
                    ViewDeductstate.Rows.Remove(dr);
                    ViewDeductstate.AcceptChanges();
                } 
                count = ViewDeductstate.Rows.Count;
                ViewDeductstate.Rows.Add();
                ViewDeductstate.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                ViewDeductstate.Rows[count]["DeductAmount"] = txtAmount.Text;
                ViewDeductstate.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                ViewDeductstate.Rows[count]["DeductPercent"] = txtPer.Text;
                ViewDeductstate.Rows[count]["HeadId"] = HeadName[0].ToString();
                ViewDeductstate.AcceptChanges();

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (GridDataDeduction.Rows.Count > 0)
                    {
                        HiddenField hdnDeductID = GridDataDeduction.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                        HiddenField hdnHeadID = GridDataDeduction.Rows[i].FindControl("hdnHeadAccID") as HiddenField;
                        if (hdnDeductID.Value == ddlDeductionHead.SelectedValue)
                        {
                            dt.Rows[i]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                            dt.Rows[i]["DeductAmount"] = txtAmount.Text;
                            dt.Rows[i]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                            dt.Rows[i]["DeductPercent"] = txtPer.Text;
                            dt.Rows[i]["HeadId"] = hdnHeadID.Value;
                            dt.AcceptChanges();
                            isDuplicate = "Y";
                            GridDataDeduction.DataSource = dt;
                            GridDataDeduction.DataBind();
                            btnSave.Enabled = true;
                            for (int j = 0; j < ViewDeductstate.Rows.Count; j++)
                            {
                                if (ViewDeductstate.Rows[j]["DeductHeadId"].ToString() == ddlDeductionHead.SelectedValue)
                                {
                                    ViewDeductstate.Rows[j]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                                    ViewDeductstate.Rows[j]["DeductAmount"] = txtAmount.Text;
                                    ViewDeductstate.Rows[j]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                                    ViewDeductstate.Rows[j]["DeductPercent"] = txtPer.Text;
                                    ViewDeductstate.Rows[j]["HeadId"] = hdnHeadID.Value;
                                    ViewDeductstate.AcceptChanges();
                                }
                            }
                            break;
                        }
                    }
                }

                if (isDuplicate == "N")
                {
                    int count = dt.Rows.Count;
                    
                    dt.Rows.Add();
                    dt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                    dt.Rows[count]["DeductAmount"] = txtAmount.Text;
                    dt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                    dt.Rows[count]["DeductPercent"] = txtPer.Text;
                    dt.Rows[count]["HeadId"] = HeadName[0].ToString();
                    dt.AcceptChanges();
                    Session["GridSessionDeduction"] = dt;
                    var rows = ViewDeductstate.Select("HeadId='" + HeadName[0].ToString() + "' AND DeductHeadid='" + ddlDeductionHead.SelectedValue + "'");
                    foreach (DataRow dr in rows)
                    {
                        ViewDeductstate.Rows.Remove(dr);
                        ViewDeductstate.AcceptChanges();
                    }
                    int count1 = ViewDeductstate.Rows.Count;
                    ViewDeductstate.Rows.Add();
                    ViewDeductstate.Rows[count1]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                    ViewDeductstate.Rows[count1]["DeductAmount"] = txtAmount.Text;
                    ViewDeductstate.Rows[count1]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                    ViewDeductstate.Rows[count1]["DeductPercent"] = txtPer.Text;
                    ViewDeductstate.Rows[count1]["HeadId"] = HeadName[0].ToString();
                    ViewDeductstate.AcceptChanges();
                }

            }
        }
        if(Request.QueryString["BillNo"] != null)
        GridDataDeduction.DataSource = Newdt;
        else
            GridDataDeduction.DataSource = dt;
        GridDataDeduction.DataBind();
        btnSave.Enabled = true;
        btnHeadAmountAdd.Visible = true;

        ddlVoucherType.Enabled = false;
        
        #endregion
        
    }
    protected void GridHeadData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region previous Row data bind
        //DataRowView drview = e.Row.DataItem as DataRowView;
        // if (e.Row.RowType == DataControlRowType.DataRow)
        // {

        //     GridView GridData = e.Row.FindControl("GridData") as GridView;
        //     GridView GridData1 = e.Row.FindControl("GridData") as GridView;
        //     string[] HeadName = txtAcc.Text.Split('*');
        //     DataTable dt = Session["DeductionGrid"] as DataTable;
        //     string isDuplicate = "N";
        //     double totalDeduction = 0.0;


        //     if (dt.Rows.Count == 0)
        //     {
        //         int count = dt.Rows.Count;
        //         dt.Rows.Add();
        //         dt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
        //         dt.Rows[count]["DeductAmount"] = txtAmount.Text;
        //         dt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
        //         dt.Rows[count]["DeductPercent"] = txtPer.Text;
        //         dt.Rows[count]["HeadId"] = HeadName[0].ToString();
        //         dt.AcceptChanges();
        //         //Label lblPayble = GridData.Rows[count].FindControl("lblPaybleAmount") as Label;

        //         //lblPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text)-Convert.ToDouble(txtAmount.Text));
        //     }
        //     else
        //     {
        //         for (int i = 0; i < dt.Rows.Count; i++)
        //         {
        //             HiddenField hdnDeductID = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
        //             HiddenField hdnHeadID = GridData.Rows[i].FindControl("hdnHeadAccID") as HiddenField;
        //             if (hdnDeductID.Value == ddlDeductionHead.SelectedValue)
        //             {
        //                 dt.Rows[i]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
        //                 dt.Rows[i]["DeductAmount"] = txtAmount.Text;
        //                 dt.Rows[i]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
        //                 dt.Rows[i]["DeductPercent"] = txtPer.Text;
        //                 dt.Rows[i]["HeadId"] = hdnHeadID.Value;
        //                 dt.AcceptChanges();
        //                 isDuplicate = "Y";
        //                 GridData.DataSource = dt;
        //                 GridData.DataBind();
        //                 btnSave.Enabled = true;

        //                 break;
        //             }
        //         }
        //         if (isDuplicate == "N")
        //         {
        //             int count = dt.Rows.Count;
        //             dt.Rows.Add();
        //             dt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
        //             dt.Rows[count]["DeductAmount"] = txtAmount.Text;
        //             dt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
        //             dt.Rows[count]["DeductPercent"] = txtPer.Text;
        //             dt.Rows[count]["HeadId"] = HeadName[0].ToString();
        //             dt.AcceptChanges();
        //         }
        //     }
        //     GridData.DataSource = dt;
        //     GridData.DataBind();
        //     btnSave.Enabled = true;


        //     for (int i = 0; i < GridData.Rows.Count; i++)
        //     {
        //         Label lblAmount = GridData.Rows[i].FindControl("lblAmount") as Label;
        //         if (lblAmount.Text != string.Empty)
        //             totalDeduction = totalDeduction + Convert.ToDouble(lblAmount.Text);
        //         else
        //             totalDeduction = 0.00;
        //     }

        //     lblAmountPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text) - totalDeduction);
        //     ddlVoucherType.Enabled = false;
        //     txtAcc.Enabled = false;
        //     txtGAmount.Enabled = false;

        // }
        #endregion
        #region New Row data bind

        //DataRowView drview = e.Row.DataItem as DataRowView;
        if (Request.QueryString["BillNo"] == null || ViewState["CheckStatus"]=="Y")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView GridData = e.Row.FindControl("GridData") as GridView;
                HiddenField hdnHeadNo = e.Row.FindControl("hdnHeadNo") as HiddenField;
                DataTable dt = new DataTable();
                dt = ViewState["NewDeductGrid"] as DataTable;
                DataTable dtDeduction = new DataTable();
                dtDeduction.Columns.Add("DeductHeadName");
                dtDeduction.Columns.Add("DeductAmount");
                dtDeduction.Columns.Add("DeductHeadId");
                dtDeduction.Columns.Add("DeductPercent");
                dtDeduction.Columns.Add("HeadId");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(hdnHeadNo.Value) == Convert.ToInt32(dt.Rows[i]["HeadId"].ToString()))
                    {
                        dtDeduction.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], dt.Rows[i][3], dt.Rows[i][4]);
                    }
                }
                GridData.DataSource = dtDeduction;
                GridData.DataBind();
                btnSave.Enabled = true;
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["RowIndex"] != null)
                {
                    if (e.Row.RowIndex.ToString() == ViewState["RowIndex"].ToString())
                    {
                        GridView GridData = e.Row.FindControl("GridData") as GridView;
                        HiddenField hdnHeadNo = e.Row.FindControl("hdnHeadNo") as HiddenField;
                        DataTable dt = new DataTable();
                        dt = Session["DeductionGrid"] as DataTable;
                        DataTable dtDeduction = new DataTable();
                        dtDeduction.Columns.Add("DeductHeadName");
                        dtDeduction.Columns.Add("DeductAmount");
                        dtDeduction.Columns.Add("DeductHeadId");
                        dtDeduction.Columns.Add("DeductPercent");
                        dtDeduction.Columns.Add("HeadId");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(hdnHeadNo.Value) == Convert.ToInt32(dt.Rows[i]["HeadId"].ToString()))
                            {
                                dtDeduction.Rows.Add(dt.Rows[i][1], dt.Rows[i][3], dt.Rows[i][2], dt.Rows[i][4], dt.Rows[i][0]);
                            }
                        }
                        GridData.DataSource = dtDeduction;
                        GridData.DataBind();
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        DataTable DeductGrid = ViewState["DeducGrid"] as DataTable;
                        HiddenField hdnHeadNo = e.Row.FindControl("hdnHeadNo") as HiddenField;
                        GridView GridData = e.Row.FindControl("GridData") as GridView;
                        DataTable dtDeduction = DeductGrid.Select("HeadId = '" + hdnHeadNo.Value + "'").CopyToDataTable();
                        GridData.DataSource = dtDeduction;
                        GridData.DataBind();
                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    DataTable dt1 = ViewState["DeducGrid"] as DataTable;
                    HiddenField hdnHeadNo = e.Row.FindControl("hdnHeadNo") as HiddenField;
                    GridView GridData = e.Row.FindControl("GridData") as GridView;
                    //String sExpression = "HeadId = '" + hdnHeadNo.Value + "'";
                    //DataTable dtStatus = dt1.Select("HeadId = '" + hdnHeadNo.Value + "'").CopyToDataTable();
                    if (dt1.Select("HeadId = '" + hdnHeadNo.Value + "'").Length>0)
                    {
                        DataTable DeductGrid = ViewState["DeducGrid"] as DataTable;
                        DataTable dtDeduction = dt1.Select("HeadId = '" + hdnHeadNo.Value + "'").CopyToDataTable();
                        GridData.DataSource = dtDeduction;
                        GridData.DataBind();
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = Session["DeductionGrid"] as DataTable;
                        DataTable dtDeduction = new DataTable();
                        dtDeduction.Columns.Add("DeductHeadName");
                        dtDeduction.Columns.Add("DeductAmount");
                        dtDeduction.Columns.Add("DeductHeadId");
                        dtDeduction.Columns.Add("DeductPercent");
                        dtDeduction.Columns.Add("HeadId");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(hdnHeadNo.Value) == Convert.ToInt32(dt.Rows[i]["HeadId"].ToString()))
                            {
                                dtDeduction.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], dt.Rows[i][3], dt.Rows[i][4]);
                            }
                        }
                        GridData.DataSource = dtDeduction;
                        GridData.DataBind();
                        btnSave.Enabled = true;
                    }
                    
                    
                   
                }
            }
        }

        #endregion
    }



    protected void txtHeadAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtHeadAmount.Text != string.Empty)
        {
            if (txtGAmount.Text != string.Empty)
            {
                txtGAmount.Text = (Convert.ToDouble(txtGAmount.Text.Trim()) + Convert.ToDouble(txtHeadAmount.Text)).ToString();
            }
            else
                txtGAmount.Text = txtHeadAmount.Text;
        }

    }



    protected void GridHeadData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Modify")
        {
           
                GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                ViewState["RowIndex"] = RowIndex;
                HiddenField hdnAccountid = GridHeadData.Rows[RowIndex].FindControl("hdnHeadNo") as HiddenField;
                Label lblHeadname = GridHeadData.Rows[RowIndex].FindControl("lblName") as Label;
                DataTable dtDeduct = Session["DeductionGrid"] as DataTable;
                DataSet dsDeductAcc = new DataSet();

                if (Request.QueryString["Voucher_Type"] != null)
                {
                    dsDeductAcc = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + Request.QueryString["Voucher_Type"].ToString() + " a  inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "a.AccountID HeadId,c.Acc_Name as DeductHeadName,a.AMOUNT as DeductAmount,a.Head_AccountId as DeductHeadId,a.TRAN_TYPE,case when a.TaxPercent is null then 0 else a.TaxPercent end DeductPercent", "a.Tot_Payable,a.Narration", "ABS_BILL_NO='" + Request.QueryString["Billno"].ToString() + "' and AccountID='" + hdnAccountid.Value.ToString() + "' and Head_AccountId is not null ", "");
                }
                else
                {
                    setcolum();
                    dsDeductAcc.Tables.Add(Session["DeductionGrid"] as DataTable);

                    for (int i = 0; i < dtDeduct.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(hdnAccountid.Value) == Convert.ToInt32(dtDeduct.Rows[i]["HeadId"].ToString()))
                        {
                            dsDeductAcc.Tables[0].Rows.Add();
                            dsDeductAcc.Tables[0].Rows[i]["DeductHeadName"] = dtDeduct.Rows[i]["DeductHeadName"];
                            dsDeductAcc.Tables[0].Rows[i]["DeductAmount"] = dtDeduct.Rows[i]["DeductAmount"];
                            dsDeductAcc.Tables[0].Rows[i]["DeductHeadId"] = dtDeduct.Rows[i]["DeductHeadId"];
                            dsDeductAcc.Tables[0].Rows[i]["DeductPercent"] = dtDeduct.Rows[i]["DeductPercent"];
                            dsDeductAcc.Tables[0].Rows[i]["HeadId"] = dtDeduct.Rows[i]["HeadId"];
                            dsDeductAcc.Tables[0].AcceptChanges();
                        }
                    }

                    //DataView dv = dtDeduct.DefaultView;
                    //dv.RowFilter = "HeadId<>" + hdnAccountid.Value;
                    //dtDeduct = dv.ToTable();
                    Session["DeductionGrid"] = dtDeduct;
                }
                GridDataDeduction.DataSource = dsDeductAcc;
                GridDataDeduction.DataBind();
                txtAcc.Text = "00" + hdnAccountid.Value + "*" + lblHeadname.Text;
                Label lblHeadAmt = GridHeadData.Rows[RowIndex].FindControl("lblAmount") as Label;
                txtHeadAmount.Text = lblHeadAmt.Text;
                if (Request.QueryString["Voucher_Type"] != null)
                {
                    DataSet dsSessionDeductAcc = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + Request.QueryString["Voucher_Type"].ToString() + " a  inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "a.AccountID HeadId,c.Acc_Name as DeductHeadName,a.Head_AccountId as DeductHeadId,a.AMOUNT as DeductAmount,case when a.TaxPercent is null then 0 else a.TaxPercent end DeductPercent", "", "ABS_BILL_NO='" + Request.QueryString["Billno"].ToString() + "' and AccountID='" + hdnAccountid.Value.ToString() + "' and Head_AccountId is not null ", "");
                    Session["DeductionGrid"] = dsSessionDeductAcc.Tables[0];
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("DeductHeadName");
                dt.Columns.Add("DeductAmount");
                dt.Columns.Add("DeductHeadId");
                dt.Columns.Add("DeductPercent");
                dt.Columns.Add("HeadId");
                for (int i = 0; i < GridHeadData.Rows.Count; i++)
                {
                    if (Convert.ToInt32(ViewState["RowIndex"]) != i)
                    {
                        GridView DeductGrid = GridHeadData.Rows[i].FindControl("GridData") as GridView;
                        for (int j = 0; j < DeductGrid.Rows.Count; j++)
                        {
                            HiddenField hdnPartyNo = DeductGrid.Rows[j].FindControl("hdnPartyNo") as HiddenField;
                            HiddenField hdnTaxPercent = DeductGrid.Rows[j].FindControl("hdnTaxPercent") as HiddenField;
                            HiddenField hdnHeadAccID = DeductGrid.Rows[j].FindControl("hdnHeadAccID") as HiddenField;
                            Label lblName = DeductGrid.Rows[j].FindControl("lblName") as Label;
                            Label lblAmount = DeductGrid.Rows[j].FindControl("lblAmount") as Label;
                            dt.Rows.Add();
                            dt.Rows[j]["DeductHeadName"] = lblName.Text;
                            dt.Rows[j]["DeductAmount"] = lblAmount.Text;
                            dt.Rows[j]["DeductHeadId"] = hdnPartyNo.Value;
                            dt.Rows[j]["DeductPercent"] = hdnTaxPercent.Value;
                            dt.Rows[j]["HeadId"] = hdnHeadAccID.Value;
                        }
                    }
                }
                ViewState["DeducGrid"] = dt;
           
            
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }
}
