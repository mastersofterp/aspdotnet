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
public partial class AbstractBillVouchers : System.Web.UI.Page
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
             objCommon.FillDropDownList(ddlDeductionHead, "ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT", "ACC_ID", "Acc_Name", "", "");
             if (Request.QueryString["BillNo"] != null)
             {
                 getTransaction(Convert.ToInt32(Request.QueryString["BillNo"].ToString()), Request.QueryString["Voucher_Type"].ToString());
                 btnSave.Enabled = true;
                 ddlVoucherType.Enabled = false;
             }
             //txtPer.Enabled = false;
         }
    }

    private DataTable setcolum()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DeductHeadName");
        dt.Columns.Add("DeductAmount");
        dt.Columns.Add("DeductHeadId");
        dt.Columns.Add("DeductPercent");
        Session["DeductionGrid"] = dt;
        return dt;
    }

    private void getTransaction(int billNo,string voucher_type)
    {
        DataSet dsTransaction = null;
        if (voucher_type == "AAPV")
        {
            TrAdvance.Visible = true;
            dsTransaction = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + voucher_type + " a inner join acc_" + Session["comp_code"].ToString() + "_party b on (a.AccountID=b.party_no) inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "a.VOUCHER_TYPE,a.AccountID,a.GROSS_AMOUNT,b.party_Name ,c.Acc_Name as DeductHeadName,a.AMOUNT as DeductAmount,a.Head_AccountId as DeductHeadId,a.TRAN_TYPE,case when a.TaxPercent is null then 0 else a.TaxPercent end DeductPercent,a.ADVANCE_AMOUNT", "a.Tot_Payable,a.Narration", "ABS_BILL_NO=" + billNo, "");

        }
        else
            dsTransaction = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + voucher_type + " a inner join acc_" + Session["comp_code"].ToString() + "_party b on (a.AccountID=b.party_no) inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "a.VOUCHER_TYPE,a.AccountID,a.GROSS_AMOUNT,b.party_Name ,c.Acc_Name as DeductHeadName,a.AMOUNT as DeductAmount,a.Head_AccountId as DeductHeadId,a.TRAN_TYPE,case when a.TaxPercent is null then 0 else a.TaxPercent end DeductPercent", "a.Tot_Payable,a.Narration", "ABS_BILL_NO=" + billNo, "");
        if (dsTransaction.Tables[0].Rows.Count > 0)
        {
            ddlVoucherType.SelectedValue = dsTransaction.Tables[0].Rows[0]["VOUCHER_TYPE"].ToString();
            txtAcc.Text = dsTransaction.Tables[0].Rows[0]["AccountID"].ToString() + "*" + dsTransaction.Tables[0].Rows[0]["party_Name"].ToString();
            txtGAmount.Text = dsTransaction.Tables[0].Rows[0]["GROSS_AMOUNT"].ToString();
            ddlDeductionHead.SelectedValue = "0";
            txtAmount.Text = string.Empty;
            txtPer.Text = dsTransaction.Tables[0].Rows[0]["DeductPercent"].ToString();
            if (voucher_type == "AAPV")
            {
                txtAdvanceTaken.Text = dsTransaction.Tables[0].Rows[0]["ADVANCE_AMOUNT"].ToString();
            }
            GridData.DataSource = dsTransaction;
            GridData.DataBind();
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                DropDownList ddlTranType = GridData.Rows[i].FindControl("ddlTran") as DropDownList;
                ddlTranType.SelectedValue = dsTransaction.Tables[0].Rows[i]["TRAN_TYPE"].ToString();
            }

            lblAmountPayble.Text = dsTransaction.Tables[0].Rows[0]["Tot_Payable"].ToString();

            txtNarration.Text = dsTransaction.Tables[0].Rows[0]["Narration"].ToString();
            Session["DeductionGrid"] = dsTransaction.Tables[0];
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int retStatus;
        string billNo=string.Empty;
        if (Request.QueryString["BillNo"] != null)
        {
            objTranController.deleteBillNoForUpdate(Convert.ToInt32(Request.QueryString["BillNo"]), Session["comp_code"].ToString(),ddlVoucherType.SelectedValue.ToString());
            billNo = Request.QueryString["BillNo"].ToString();
        }
            try{
                   for(int i=0;i<GridData.Rows.Count;i++)
                    {
                        objTran.VOUCHER_TYPE = ddlVoucherType.SelectedValue;
                        Session["Voucher_Type"] = objTran.VOUCHER_TYPE.ToString();
                        objTran.GROSSAMOUNT=Convert.ToDouble(txtGAmount.Text);
                        DropDownList ddlTranType=GridData.Rows[i].FindControl("ddlTran") as DropDownList;
                        objTran.TRANSACTION_TYPE=ddlTranType.SelectedValue;
                        Label lblAmount=GridData.Rows[i].FindControl("lblAmount") as Label;
                        if (lblAmount.Text != string.Empty)
                            objTran.AMOUNT = Convert.ToDouble(lblAmount.Text);
                        else
                            objTran.AMOUNT = 0.00;
                        string[] headAccountID = txtAcc.Text.Split('*');
                        objTran.HEADACC=Convert.ToInt32(headAccountID[0]).ToString();
                        objTran.GROSSAMOUNT = Convert.ToDouble(txtGAmount.Text);
                        objTran.NARRATION = txtNarration.Text;
                        HiddenField hdnAccountID=GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                        objTran.HEADACCOUNTID = hdnAccountID.Value.ToString();
                        objTran.TOTALPAYBLE = lblAmountPayble.Text;
                        objTran.DisplayName = txDisplayName.Text;
                        if (txtPer.Text != "")
                        {
                            objTran.TaxPer = Convert.ToDecimal(txtPer.Text);
                        }
                        else
                            objTran.TaxPer = 0;
                        if (Request.QueryString["BillNo"] == null)
                        {
                            if (i == 0)
                            {
                                billNo = "0";
                            }
                            else
                            {
                                billNo = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_"+ddlVoucherType.SelectedValue.ToString(), "max(CAST([ABS_BILL_NO] AS INT))", "");
                            }
                        }

                        objTran.AdvanceAmount = Convert.ToDouble(txtAdvanceTaken.Text);
                        //objTran.BillAmount = Convert.ToDouble(txtBillAmount.Text);

                        retStatus=objTranController.AddAbstractBill(objTran, Session["comp_code"].ToString(),billNo);
                        if (retStatus == 1)
                        {
                            if (Request.QueryString["BillNo"] != null)
                            {
                                Response.Redirect("AbstractBillModification.aspx?Voucher_Type=" + Request.QueryString["Voucher_Type"].ToString());
                            }
                            else
                            {
                                objCommon.DisplayUserMessage(UPDLedger, "Transaction Save Successfully", this.Page);
                            }
                        }
                    }
                    clear();
                    Session["BillNo"] = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + objTran.VOUCHER_TYPE, "max(CAST([ABS_BILL_NO] AS INT))", "");
                    btnPrint.Enabled = true;
                    
                 
            }
        catch(Exception ex)
        {
            //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.GetFeeHeadAndNo-> " + ex.ToString());
            objCommon.DisplayUserMessage(UPDLedger,ex.Message,this.Page);

        }

    }

    protected void btnSaveDeduction_Click(object sender, EventArgs e)
    {
        DataTable dt =Session["DeductionGrid"] as DataTable;
        string isDuplicate = "N";
        double totalDeduction = 0.0;


        if (dt.Rows.Count == 0)
        {
            int count = dt.Rows.Count;
            dt.Rows.Add();
            dt.Rows[count]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
            dt.Rows[count]["DeductAmount"] = txtAmount.Text;
            dt.Rows[count]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
            dt.Rows[count]["DeductPercent"] = txtPer.Text;
            dt.AcceptChanges();
            //Label lblPayble = GridData.Rows[count].FindControl("lblPaybleAmount") as Label;

            //lblPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text)-Convert.ToDouble(txtAmount.Text));
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HiddenField hdnDeductID = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                if (hdnDeductID.Value == ddlDeductionHead.SelectedValue)
                {
                    dt.Rows[i]["DeductHeadName"] = ddlDeductionHead.SelectedItem.Text;
                    dt.Rows[i]["DeductAmount"] = txtAmount.Text;
                    dt.Rows[i]["DeductHeadId"] = ddlDeductionHead.SelectedValue;
                    dt.Rows[i]["DeductPercent"] = txtPer.Text;
                    dt.AcceptChanges();
                    isDuplicate = "Y";
                    GridData.DataSource = dt;
                    GridData.DataBind();
                    btnSave.Enabled = true;
                    
                    break;
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
                dt.AcceptChanges();
            }
        }
        GridData.DataSource = dt;
        GridData.DataBind();
        btnSave.Enabled = true;

        
        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            Label lblAmount = GridData.Rows[i].FindControl("lblAmount") as Label;
            if (lblAmount.Text!= string.Empty)
                totalDeduction = totalDeduction + Convert.ToDouble(lblAmount.Text);
            else
                totalDeduction = 0.00;
        }

        lblAmountPayble.Text = Convert.ToString(Convert.ToDouble(txtGAmount.Text) - totalDeduction);
        ddlVoucherType.Enabled = false;
        txtAcc.Enabled = false;
        txtGAmount.Enabled = false;

    }

    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {
        string[] ledgerName = txtAcc.Text.Split('¯');
        txtAcc.Text = ledgerName[0].ToString();
    }

    private void clear()
    {
        ddlVoucherType.SelectedValue = "0";
        txtAcc.Text = string.Empty;
        txtGAmount.Text = string.Empty;
        ddlDeductionHead.SelectedValue = "0";
        txtAmount.Text = string.Empty;
        GridData.DataSource = null;
        GridData.DataBind();
        txtNarration.Text = string.Empty;
        setcolum();
        lblAmountPayble.Text = "0.00";
        ddlVoucherType.Enabled = true;
        txtAcc.Enabled = true;
        txtGAmount.Enabled = true;
        btnSave.Enabled = false;
        TrAdvance.Visible = false;
        //TrBillAmount.Visible = false;
        txDisplayName.Text = string.Empty;
        txtPer.Text = string.Empty;
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
        if (Session["Voucher_Type"].ToString() != "AAPV")
        {
            ShowReport("Abstract Bill Voucher", "VoucherPrintNewFormat.rpt");
        }
        else
        {
            ShowReport("Abstract Bill Voucher", "VoucherPrintNewFormatAAPV.rpt");
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
