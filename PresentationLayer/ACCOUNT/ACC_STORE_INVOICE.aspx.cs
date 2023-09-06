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
using System.Xml;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.IO;


public partial class ACCOUNT_ACC_STORE_INVOICE : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountTransactionController objPC1 = new AccountTransactionController();

    int Arows = 0;
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
    string isVoucherTypeWise = string.Empty;
    string AllowVoucherNoReset = string.Empty;

    string back = string.Empty;

    DataSet dsTax = new DataSet();


    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    BindInvoice();

                }
            }

        }

    }
    protected void BindInvoice()
    {
        //objCommon.FillDropDownList(ddlInvoice, "STORE_INVOICE", "INVTRNO", "INVNO", "", "INVTRNO DESC");
        DataSet ds = objPC1.GetInvoiceDetails();
        GridInvoice.DataSource = ds;
        GridInvoice.DataBind();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        //DataSet ds = objPC1.GetInvoiceDetails();
        //GridInvoice.DataSource = ds;
        //GridInvoice.DataBind();
    }


    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAcc(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAgainstAccLedger(prefixText);
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
    public static List<string> GetAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAccountEntryCashBank(prefixText);
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
    protected void txtAccountLedger_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (checkCrDrAmount() == 0)
        {


            objCommon.DisplayUserMessage(upStoreInvoice, "Total Cr : " + ViewState["Dr"].ToString() + " and Total Cr : " + ViewState["Cr"].ToString() + " is not Matched ", this.Page);
            return;
        }
        BillVouchercreation();
        upd_ModalPopupExtender1.Show();
    }
    public int checkCrDrAmount()
    {

        decimal SumAccountAmount = 0;
        decimal SumPartyAmount = 0;
        decimal vendorAmount, AccountAmount = 0;
        foreach (ListViewItem item in lvTaxList.Items)
        {
            Label Mode = item.FindControl("lblMode") as Label;
            TextBox Amount = item.FindControl("txtAmount") as TextBox;
            if (Mode.Text == "Dr")
            {
                SumAccountAmount = SumAccountAmount + Convert.ToDecimal(Amount.Text);
            }
            if (Mode.Text == "Cr")
            {
                SumPartyAmount = SumPartyAmount + Convert.ToDecimal(Amount.Text);
            }
        }
        AccountAmount = Convert.ToDecimal(SumAccountAmount + Convert.ToDecimal(txtItemAmount.Text));
        vendorAmount = Convert.ToDecimal(SumPartyAmount + Convert.ToDecimal(txtPartyAmount.Text));
        if (chkTDSApplicable.Checked)
        {
            vendorAmount = vendorAmount + Convert.ToDecimal(txtTDSAmount.Text);
        }
        if (chkIGST.Checked)
        {
            AccountAmount = AccountAmount + Convert.ToDecimal(txtIGSTAMT.Text);
        }
        if (chkGST.Checked)
        {
            AccountAmount = AccountAmount + Convert.ToDecimal(txtCGSTAMT.Text) + Convert.ToDecimal(txtSGSTAMT.Text);
        }
        DataSet dsTax = (DataSet)Session["DataSet"];
        for (int j = 1; j <= 4; j++)
        {
            if (Convert.ToBoolean(dsTax.Tables[1].Rows[0]["FLAG" + j]) == true)
            {
                vendorAmount += Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT" + j]);
            }
            else
            {
                AccountAmount += Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT" + j]);
            }
        }
        if (Convert.ToDecimal(ViewState["Amount"].ToString()) != vendorAmount || Convert.ToDecimal(ViewState["Amount"].ToString()) != AccountAmount)
        {
            ViewState["Cr"] = vendorAmount;
            ViewState["Dr"] = AccountAmount;
            return 0;
        }

        return 1;


    }
    public void Clear()
    {
        pnlVoucher.Visible = false;
        pnl.Visible = true;
        txtAccountLedger.Text = txtCashBankLedger.Text = txtPartyName.Text = txtPanNo.Text = txtGSTNNO.Text = string.Empty;
        txtNarration.Text = txtNatureService.Text = txtChqNo2.Text = string.Empty;
        txtE1Ledger.Text = txtE2Ledger.Text = txtE3Ledger.Text = txtE4Ledger.Text = txtEAmt.Text = txtEAmt1.Text = string.Empty;
        txtEAmt2.Text = txtEAmt3.Text = txtEcharge.Text = txtEcharge1.Text = txtEcharge2.Text = txtEcharge3.Text = string.Empty;
        txtEPer.Text = txtEPer1.Text = txtEPer2.Text = txtEPer3.Text = string.Empty;

        txtTamount.Text = txtTDSAmount.Text = txtTDSLedger.Text = txtTDSPer.Text = string.Empty;
        txtSGTSPer.Text = txtSGSTAMT.Text = txtSGSTAMOUNT.Text = txtSGST.Text = txtCGST.Text = txtCGSTAMOUNT1.Text = txtCGSTAMT.Text = txtCGSTPER.Text = string.Empty;
        txtIGST.Text = txtIGSTAMOUNT.Text = txtIGSTAMT.Text = txtIGSTPER.Text = string.Empty;

        chkTDSApplicable.Checked = false;
        chkGST.Checked = false;
        chkIGST.Checked = false;

        divIgst.Visible = false;
        divgst.Visible = false;
        divcgst.Visible = false;
        dvTDS.Visible = false;


        divAd1.Visible = false;
        divAd2.Visible = false;
        divAd3.Visible = false;
        divAd4.Visible = false;

        divadditional.Visible = false;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void txtCashBankLedger_TextChanged(object sender, EventArgs e)
    {

    }

    private void SetParameters()
    {
        objCommon = new Common();
        DataSet ds = new DataSet();

        ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
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
    public void calcListviewtaxamount(DataSet dsTax)
    {
        decimal AccountAmount = 0;
        decimal PartyAmount = 0;
        for (int i = 0; i < dsTax.Tables[0].Rows.Count; i++)
        {
            if (dsTax.Tables[0].Rows[i]["MODE"].ToString().Equals("Dr"))
            {
                AccountAmount = AccountAmount + Convert.ToDecimal(dsTax.Tables[0].Rows[i]["AMT"].ToString());
            }
            if (dsTax.Tables[0].Rows[i]["MODE"].ToString().Equals("Cr"))
            {
                PartyAmount = PartyAmount + Convert.ToDecimal(dsTax.Tables[0].Rows[i]["AMT"].ToString());
            }
        }
        for (int j = 1; j <= 4; j++)
        {
            if (Convert.ToBoolean(dsTax.Tables[1].Rows[0]["FLAG" + j]) == true)
            {
                PartyAmount += Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT" + j]);
            }
            else
            {
                AccountAmount += Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT" + j]);
            }
        }
        txtPartyAmount.Text = Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) - PartyAmount).ToString();
        hdnVendorAmount.Value = Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) - PartyAmount).ToString();
        txtItemAmount.Text = Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) - AccountAmount).ToString();
        hdnTotalAmount.Value = Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) - AccountAmount).ToString();
    }
    protected void lnkselect_Click(object sender, EventArgs e)
    {
        pnlVoucher.Visible = true;
        pnl.Visible = false;
        this.upd_ModalPopupExtender1.Hide();
        LinkButton lnkbutton = sender as LinkButton;
        ViewState["Invtrno"] = lnkbutton.CommandName;
        ViewState["Invno"] = objCommon.LookUp("STORE_INVOICE", "INVNO", "INVTRNO=" + ViewState["Invtrno"].ToString());
        ViewState["BudgetNo"] = lnkbutton.CommandArgument;
        ViewState["Amount"] = String.Format("{0:0.00}", lnkbutton.ToolTip);
        hdnTotalAmount.Value = lblBillAmount.Text = String.Format("{0:0.00}", lnkbutton.ToolTip);
        Session["DataSet"] = null;
        dsTax = objPC1.GetInvoiceTaxDetails(Convert.ToInt32(ViewState["Invtrno"].ToString()));
        Session["DataSet"] = dsTax;
        lvTaxList.DataSource = dsTax;
        lvTaxList.DataBind();
        hdnTaxRowCount.Value = dsTax.Tables[0].Rows.Count.ToString();
        calcListviewtaxamount(dsTax);
        txtChequeDt2.Text = Convert.ToDateTime(DateTime.Now.Date).ToString("dd/MM/yyyy");
        Arows = 0;
        if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT1"].ToString()) > 0)
        {
            Arows += 1;
            divadditional.Visible = true;
            divAd1.Visible = true;
            txtEPer.Text = dsTax.Tables[1].Rows[0]["EP1"].ToString();
            txtEcharge.Text = dsTax.Tables[1].Rows[0]["ECHG1"].ToString();
            chkDiscount.Checked = Convert.ToBoolean(dsTax.Tables[1].Rows[0]["FLAG1"]);
            txtEAmt.Text = dsTax.Tables[1].Rows[0]["EAMT1"].ToString();
            lblMode.Text = chkDiscount.Checked == true ? "Cr" : "Dr";

        }
        if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT2"].ToString()) > 0)
        {
            Arows += 1;
            divadditional.Visible = true;
            divAd2.Visible = true;
            txtEPer1.Text = dsTax.Tables[1].Rows[0]["EP2"].ToString();
            txtEcharge1.Text = dsTax.Tables[1].Rows[0]["ECHG2"].ToString();
            chkDiscount1.Checked = Convert.ToBoolean(dsTax.Tables[1].Rows[0]["FLAG2"]);
            txtEAmt1.Text = dsTax.Tables[1].Rows[0]["EAMT2"].ToString();
            lblMode1.Text = chkDiscount1.Checked == true ? "Cr" : "Dr";
        }
        if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT3"].ToString()) > 0)
        {
            Arows += 1;
            divadditional.Visible = true;
            divAd3.Visible = true;
            txtEPer2.Text = dsTax.Tables[1].Rows[0]["EP3"].ToString();
            txtEcharge2.Text = dsTax.Tables[1].Rows[0]["ECHG3"].ToString();
            chkDiscount2.Checked = Convert.ToBoolean(dsTax.Tables[1].Rows[0]["FLAG3"]);
            txtEAmt2.Text = dsTax.Tables[1].Rows[0]["EAMT3"].ToString();
            lblMode2.Text = chkDiscount2.Checked == true ? "Cr" : "Dr";
        }
        if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT4"].ToString()) > 0)
        {

            divadditional.Visible = true;
            divAd4.Visible = true;
            txtEPer3.Text = dsTax.Tables[1].Rows[0]["EP4"].ToString();
            txtEcharge3.Text = dsTax.Tables[1].Rows[0]["ECHG4"].ToString();
            chkDiscount3.Checked = Convert.ToBoolean(dsTax.Tables[1].Rows[0]["FLAG4"]);
            txtEAmt3.Text = dsTax.Tables[1].Rows[0]["EAMT4"].ToString();
            lblMode3.Text = chkDiscount3.Checked == true ? "Cr" : "Dr";

        }
        //else
        //{
        //    divadditional.Visible = false;bil
        //}

    }

    #region Voucher Creation

    private void BillVouchercreation()
    {
        int rows = 0;
        XmlDocument objXMLDoc = new XmlDocument();

        ViewState["VoucherNo"] = string.Empty;

        FinanceCashBookController objCBC = new FinanceCashBookController();

        XmlDeclaration xmlDeclaration = objXMLDoc.CreateXmlDeclaration("1.0", null, null);

        // Create the root element
        XmlElement rootNode = objXMLDoc.CreateElement("tables");
        objXMLDoc.InsertBefore(xmlDeclaration, objXMLDoc.DocumentElement);
        objXMLDoc.AppendChild(rootNode);

        var RowCount = lvTaxList.Items.Count();

        try
        {



            HiddenField hdnparty = new HiddenField();


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
            IsGSTApplicable.InnerText = "0";
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";
            IsTDSApplicable.InnerText = "0";
            AmtWithoutGST.InnerText = ViewState["Amount"].ToString();
            Section.InnerText = "0";
            TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy").Trim();

            //ViewState["Amount"] = txtTotalBillAmt.Text.Trim().ToString();
            GSTPercent.InnerText = "0";
            SUBTR_NO.InnerText = "0";


            XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
            TRANSACTION_TYPE.InnerText = "J";
            HiddenField hdnOpParty = new HiddenField();

            hdnOpParty.Value = Convert.ToInt32(txtCashBankLedger.Text.Split('*')[1]).ToString();
            hdnparty.Value = Convert.ToInt32(txtAccountLedger.Text.Split('*')[1]).ToString();


            OPARTY.InnerText = hdnOpParty.Value;
            PARTY_NO.InnerText = hdnparty.Value;

            TRAN.InnerText = "Dr";
            AMOUNT.InnerText = Convert.ToDecimal(txtItemAmount.Text).ToString();


            XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
            DEGREE_NO.InnerText = "0";

            XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");
            TRANSFER_ENTRY.InnerText = "3";
            XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
            CBTYPE_STATUS.InnerText = "H";
            XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
            CBTYPE.InnerText = "TF";
            XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");
            RECIEPT_PAYMENT_FEES.InnerText = "J";
            XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
            REC_NO.InnerText = "0";
            XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
            XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");

            CHQ_NO.InnerText = "0";
            ViewState["CHQ_NO"] = "0";
            CHQ_DATE.InnerText = Convert.ToDateTime(txtChequeDt2.Text).ToString("dd-MMM-yyyy");
            ViewState["CHQ_DATE"] = Convert.ToDateTime(txtChequeDt2.Text).ToString("dd-MMM-yyyy");

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
            if (ViewState["BudgetNo"].ToString() == "0" || ViewState["BudgetNo"].ToString() == "")
            {
                BudgetNo.InnerText = "0";
            }
            else
            {
                BudgetNo.InnerText = ViewState["BudgetNo"].ToString();
            }

            //Added by Nokhlal Kumar As per requirement to add TDS


            //Added by Nokhlal Kumar As per requirement as on Date :- 2018-11-16
            XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
            if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
            {
                PARTY_NAME.InnerText = "-";
            }
            else
            {
                PARTY_NAME.InnerText = txtPartyName.Text.Trim().ToString();
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
            if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
            {
                NATURE_SERVICE.InnerText = "-";
            }
            else
            {
                NATURE_SERVICE.InnerText = txtNatureService.Text.Trim().ToString();
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
            DepartmentId.InnerText = ViewState["DeptNo"].ToString();
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
            SGSTApplicable.InnerText = "0";
            CGSTper.InnerText = "0";
            CGSTamount.InnerText = "0";
            CGSTonamount.InnerText = "0";
            SGSTper.InnerText = "0";
            SGSTamount.InnerText = "0";
            SGSTonamount.InnerText = "0";
            XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");
            if (txtGSTNNO.Text != "" || txtGSTNNO.Text != string.Empty)
            {
                GSTIN_NO.InnerText = txtGSTNNO.Text;
            }
            GSTIN_NO.InnerText = "-";
            string voucherNo1 = string.Empty;
            if (objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
            {
                voucherNo1 = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='P'");
                VOUCHER_NO.InnerText = voucherNo1;
                ViewState["VoucherNo"] = voucherNo1;
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherNo1;//  txtVoucherNo.Text.ToString().Trim();
            }
            STR_CB_VOUCHER_NO.InnerText = "0";
            ProjectId.InnerText = "0";
            HiddenField hdnSubproject = new HiddenField();
            hdnSubproject.Value = "";
            ProjectSubId.InnerText = hdnSubproject.Value == "" ? "0" : hdnSubproject.Value;
            BILL_ID.InnerText = "0";


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
            objElement.AppendChild(CC_ID);
            objElement.AppendChild(BudgetNo);
            objElement.AppendChild(IsTDSApplicable);
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


            ViewState["TotalAmt"] = ViewState["Amount"].ToString();
            foreach (ListViewItem item in lvTaxList.Items)
            {
                HiddenField Fno = (HiddenField)item.FindControl("hdnFno");
                Label Per = (Label)item.FindControl("lblper");
                TextBox Ledger = (TextBox)item.FindControl("txtTaxLedger");
                TextBox Amount = (TextBox)item.FindControl("txtAmount");
                Label Mode = (Label)item.FindControl("lblMode");
                if (Amount.Text != "0")
                {

                    objXMLDoc = AddTaxtable(objXMLDoc, ViewState["VoucherNo"].ToString(), Amount.Text, Per.Text, Ledger.Text, Mode.Text, ViewState["Amount"].ToString(), Fno.Value);
                }
            }
            dsTax = (DataSet)Session["DataSet"];
            if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT1"].ToString()) > 0)
            {
                string Mode = chkDiscount.Checked == true ? "Cr" : "Dr";
                objXMLDoc = AddTaxtable(objXMLDoc, ViewState["VoucherNo"].ToString(), txtEAmt.Text, txtEPer.Text, txtE1Ledger.Text, Mode, ViewState["Amount"].ToString(), "0");
            }
            if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT2"].ToString()) > 0)
            {



                string Mode = chkDiscount1.Checked == true ? "Cr" : "Dr";

                objXMLDoc = AddTaxtable(objXMLDoc, ViewState["VoucherNo"].ToString(), txtEAmt1.Text, txtEPer1.Text, txtE2Ledger.Text, Mode, ViewState["Amount"].ToString(), "0");

            }
            if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT3"].ToString()) > 0)
            {

                string Mode = chkDiscount2.Checked == true ? "Cr" : "Dr";
                objXMLDoc = AddTaxtable(objXMLDoc, ViewState["VoucherNo"].ToString(), txtEAmt2.Text, txtEPer2.Text, txtE3Ledger.Text, Mode, ViewState["Amount"].ToString(), "0");

            }
            if (Convert.ToDecimal(dsTax.Tables[1].Rows[0]["EAMT4"].ToString()) > 0)
            {
                string Mode = chkDiscount3.Checked == true ? "Cr" : "Dr";
                objXMLDoc = AddTaxtable(objXMLDoc, ViewState["VoucherNo"].ToString(), txtEAmt3.Text, txtEPer3.Text, txtE4Ledger.Text, Mode, ViewState["Amount"].ToString(), "0");

            }
            if (chkGST.Checked)
            {
                objXMLDoc = AddGSTGrid(objXMLDoc, ViewState["VoucherNo"].ToString());
            }
            if (chkIGST.Checked)
            {
                objXMLDoc = AddIGSTGrid(objXMLDoc, ViewState["VoucherNo"].ToString());
            }
            if (chkTDSApplicable.Checked)
            {
                objXMLDoc = AddTDSGrid(objXMLDoc, ViewState["VoucherNo"].ToString());
            }


            objXMLDoc = ConsolidateTransactionEntry1(objXMLDoc, ViewState["VoucherNo"].ToString());


            string IsModify = string.Empty;
            int VoucherSqn = 0;
            int voucherno;
            IsModify = "N";
            VoucherSqn = 0;
            voucherno = 0;
            int Party_Number = Convert.ToInt32(txtCashBankLedger.Text.ToString().Split('*')[1].ToString());
            voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), "J");
            int i = objPC1.AddBudgetTransactionForStore(Session["comp_code"].ToString().Trim(), Convert.ToString(voucherno), "J", Convert.ToInt32(ViewState["Invtrno"].ToString()), Convert.ToInt32(ViewState["Pno"].ToString()), txtPartyAmount.Text.ToString(), Party_Number);
            if (i > 0)
            {
                //objCommon.DisplayUserMessage(upStoreInvoice, "Transaction Performed Successfully", this.Page);
                //Clear();
                //BindInvoice();
                //return;

            }
            else
            {

            }
            Session["vchno"] = voucherno.ToString();
            DataSet dsResult = objPC1.GetTransactionResult(voucherno, Session["comp_code"].ToString(), "J");
            if (objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_config", "PARAMETER", "CONFIGDESC='ENABLE CHEQUE PRINTING'") == "N")
            {
                // btnchequePrint.Visible = false;
            }
            else
            {
                string tranno = dsResult.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                string partyName = dsResult.Tables[0].Rows[1]["LEDGER"].ToString();
                partyName = ViewState["BankName"].ToString();
                string chqno2 = "0";
                string amount = string.Empty;
                if (Convert.ToDecimal(dsResult.Tables[0].Rows[0]["DEBIT"].ToString()) > 0)
                    amount = dsResult.Tables[0].Rows[0]["DEBIT"].ToString();
                else
                    amount = dsResult.Tables[0].Rows[0]["CREDIT"].ToString();

                string CHQ_NO1 = dsResult.Tables[0].Rows[0]["CHQ_NO"].ToString();

                btnchequePrint.Attributes.Add("onclick", "ShowChequePrintingTran('" + chqno2.ToString() + "','" + tranno + "','" + partyName + "','" + amount + "','0','" + CHQ_NO1 + "')");
            }

            if (dsResult != null)
            {
                if (dsResult.Tables[0].Rows.Count != 0)
                {
                    lvGrp.Visible = true;
                    lvGrp.DataSource = dsResult.Tables[0];
                    lvGrp.DataBind();

                    //    if (isvoucher_Cheque_Print == "Y")

                    this.upd_ModalPopupExtender1.Show();
                    objPC1.UpdateBalanceAllLedger();

                }
                else
                {
                    objCommon.DisplayUserMessage(upStoreInvoice, "Transaction Not Performed Successfully", this.Page);
                }

            }
            //   txtApproveRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(upStoreInvoice, "Transaction Not Performed Successfully", this.Page);
        }

    }

    private XmlDocument ConsolidateTransactionEntry1(XmlDocument objXMLDoc, string voucherno)
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

        IsGSTApplicable.InnerText = "0";
        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPartyName.Text.Trim().ToString();
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
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureService.Text.Trim().ToString();
        }

        ViewState["BankName"] = txtCashBankLedger.Text.ToString().Split('*')[0].ToString();
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
        OPARTY.InnerText = Convert.ToInt32(txtAccountLedger.Text.ToString().Split('*')[1]).ToString();
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "J";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        PARTY_NO.InnerText = Convert.ToInt32(txtCashBankLedger.Text.Split('*')[1]).ToString();

        //AMOUNT.InnerText = (Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(ViewState["TDSAmount"].ToString())).ToString();
        AMOUNT.InnerText = Convert.ToDouble(txtPartyAmount.Text).ToString();
        AmtWithoutGST.InnerText = "0";
        GSTPercent.InnerText = "0";
        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = "0";

        STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "3";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "J";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

        CHQ_NO.InnerText = "0";

        CHQ_DATE.InnerText = Convert.ToDateTime(txtChequeDt2.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");

        if (ViewState["BudgetNo"].ToString() == "0" || ViewState["BudgetNo"].ToString() == "")
        {
            BudgetNo.InnerText = "0";
        }
        else
        {
            BudgetNo.InnerText = ViewState["BudgetNo"].ToString();
        }


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
        DepartmentId.InnerText = ViewState["DeptNo"].ToString();



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
        SGSTApplicable.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";


        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTNNO.Text != "" || txtGSTNNO.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = "0";

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

    private XmlDocument AddTaxtable(XmlDocument objXMLDoc, string voucherno, string Amount, string Per, string Ledger, string Mode, string TransAmount, string Fno)
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


        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");

        if (ViewState["BudgetNo"].ToString() == "0" || ViewState["BudgetNo"].ToString() == "")
        {
            BudgetNo.InnerText = "0";
        }
        else
        {
            BudgetNo.InnerText = ViewState["BudgetNo"].ToString();
        }




        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPartyName.Text.Trim().ToString();
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
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureService.Text.Trim().ToString();
        }

        ViewState["BankName"] = txtCashBankLedger.Text.ToString().Split('*')[0].ToString();
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
        OPARTY.InnerText = Convert.ToInt32(txtCashBankLedger.Text.ToString().Split('*')[1]).ToString();

        TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "J";

        TRAN.InnerText = Mode;

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();



        PARTY_NO.InnerText = Convert.ToInt32(Ledger.ToString().Split('*')[1]).ToString();

        AMOUNT.InnerText = Convert.ToDouble(Amount).ToString();
        if (Mode == "Cr")
        {
            ViewState["TotalAmt"] = Convert.ToDouble(Convert.ToDouble(ViewState["TotalAmt"].ToString()) - Convert.ToDouble(Amount));
        }
        else
        {
            ViewState["TotalAmt"] = Convert.ToDouble(Convert.ToDouble(ViewState["TotalAmt"].ToString()) + Convert.ToDouble(Amount));
        }
        AmtWithoutGST.InnerText = TransAmount.ToString();



        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = "0";

        STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "3";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "J";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

        CHQ_NO.InnerText = "0";

        CHQ_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";



        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
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
        DepartmentId.InnerText = ViewState["DeptNo"].ToString();

        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IsIGSTApplicable.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTPER.InnerText = "0";
        IGSTonAmount.InnerText = "0";

        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");

        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");

        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");
        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        GSTPercent.InnerText = "0";
        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";
        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        IsTDSApllicalbe.InnerText = "0";
        IsGSTApplicable.InnerText = "0";
        Section.InnerText = "0";
        SGSTApplicable.InnerText = "0";
        if (Fno == "1")
        {
            IsGSTApplicable.InnerText = "1";
            Section.InnerText = "0";
            SGSTApplicable.InnerText = "1";
            SGSTper.InnerText = Per;
            SGSTamount.InnerText = Amount;
            SGSTonamount.InnerText = TransAmount;
        }

        if (Fno == "2")
        {
            IsGSTApplicable.InnerText = "1";
            CGSTper.InnerText = Per;
            CGSTamount.InnerText = Amount;
            CGSTonamount.InnerText = TransAmount;
            SGSTApplicable.InnerText = "1";
        }

        if (Fno == "3")
        {
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = Amount;
            TDPersentage.InnerText = Per.ToString();
            IsTDSApllicalbe.InnerText = "1";
        }



        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTNNO.Text != "" || txtGSTNNO.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = "0";

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


    #endregion

    #region AddTaxes
    private XmlDocument AddGSTGrid(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        for (int i = 0; i < 2; i++)
        {
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

            IsGSTApplicable.InnerText = "1";
            Section.InnerText = "0";

            //Added by Nokhlal Kumar for Party Name and PAN Number......
            XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
            if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
            {
                PARTY_NAME.InnerText = "-";
            }
            else
            {
                PARTY_NAME.InnerText = txtPartyName.Text.Trim().ToString();
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
            if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
            {
                NATURE_SERVICE.InnerText = "-";
            }
            else
            {
                NATURE_SERVICE.InnerText = txtNatureService.Text.Trim().ToString();
            }

            ViewState["BankName"] = txtCashBankLedger.Text.ToString().Split('*')[0].ToString();

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
            OPARTY.InnerText = Convert.ToInt32(txtCashBankLedger.Text.Split('*')[1]).ToString();

            TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy").Trim();
            TRANSACTION_TYPE.InnerText = "J";

            TRAN.InnerText = "Dr";

            //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
            XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
            XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
            XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";

            PARTY_NO.InnerText = i == 0 ? Convert.ToInt32(txtSGST.Text.ToString().Split('*')[1]).ToString() : Convert.ToInt32(txtCGST.Text.ToString().Split('*')[1]).ToString(); ;


            //AMOUNT.InnerText = (Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(ViewState["TDSAmount"].ToString())).ToString();
            AMOUNT.InnerText = i == 0 ? Convert.ToDecimal(txtSGSTAMT.Text).ToString() : Convert.ToDecimal(txtCGSTAMT.Text).ToString();
            AmtWithoutGST.InnerText = "0";
            GSTPercent.InnerText = "0";
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";

            DEGREE_NO.InnerText = "0";

            VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

            //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
            STR_CB_VOUCHER_NO.InnerText = "0";

            STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

            TRANSFER_ENTRY.InnerText = "3";
            CBTYPE_STATUS.InnerText = "H";
            CBTYPE.InnerText = "TF";
            RECIEPT_PAYMENT_FEES.InnerText = "J";
            REC_NO.InnerText = "0";
            //objPC.CHQ_NO = "0";

            CHQ_NO.InnerText = "0";

            CHQ_DATE.InnerText = Convert.ToDateTime(txtChequeDt2.Text).ToString("dd-MMM-yyyy");

            CHALLAN.InnerText = "false";
            CAN.InnerText = "false";

            XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
            DCR_NO.InnerText = "0";

            XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
            CC_ID.InnerText = "0";

            XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");

            if (ViewState["BudgetNo"].ToString() == "0" || ViewState["BudgetNo"].ToString() == "")
            {
                BudgetNo.InnerText = "0";
            }
            else
            {
                BudgetNo.InnerText = ViewState["BudgetNo"].ToString();
            }


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
            DepartmentId.InnerText = ViewState["DeptNo"].ToString();



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
            SGSTApplicable.InnerText = "1";

            SGSTper.InnerText = i == 0 ? txtSGTSPer.Text : "0";
            SGSTamount.InnerText = i == 0 ? txtSGSTAMT.Text : "0";
            SGSTonamount.InnerText = i == 0 ? ViewState["Amount"].ToString() : "0";
            CGSTper.InnerText = i == 1 ? txtCGSTPER.Text : "0";
            CGSTamount.InnerText = i == 1 ? txtCGSTAMT.Text : "0";
            CGSTonamount.InnerText = i == 1 ? ViewState["Amount"].ToString() : "0";







            XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


            if (txtGSTNNO.Text != "" || txtGSTNNO.Text != string.Empty)
            {
                GSTIN_NO.InnerText = txtGSTNNO.Text;
            }
            GSTIN_NO.InnerText = "-";


            ProjectId.InnerText = "0";
            ProjectSubId.InnerText = "0";
            BILL_ID.InnerText = "0";

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

        }
        return objXMLDoc;
    }

    private XmlDocument AddIGSTGrid(XmlDocument objXMLDoc, string voucherno)
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

        IsGSTApplicable.InnerText = "1";
        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPartyName.Text.Trim().ToString();
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
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureService.Text.Trim().ToString();
        }

        ViewState["BankName"] = txtCashBankLedger.Text.ToString().Split('*')[0].ToString();

        string narration = txtNarration.Text.Trim().Replace("'", "''");
        if (txtNarration.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }


        OPARTY.InnerText = Convert.ToInt32(txtCashBankLedger.Text.Split('*')[1]).ToString();
        // OPARTY.InnerText = ViewState["OPartyNo"].ToString();
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "J";

        TRAN.InnerText = "Dr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        PARTY_NO.InnerText = Convert.ToInt32(txtIGST.Text.ToString().Split('*')[1]).ToString();


        //AMOUNT.InnerText = (Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(ViewState["TDSAmount"].ToString())).ToString();
        AMOUNT.InnerText = Convert.ToDecimal(txtIGSTAMT.Text).ToString();
        AmtWithoutGST.InnerText = "0";
        GSTPercent.InnerText = "0";
        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";

        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = "0";

        STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "3";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "J";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

        CHQ_NO.InnerText = "0";

        CHQ_DATE.InnerText = Convert.ToDateTime(txtChequeDt2.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");

        if (ViewState["BudgetNo"].ToString() == "0" || ViewState["BudgetNo"].ToString() == "")
        {
            BudgetNo.InnerText = "0";
        }
        else
        {
            BudgetNo.InnerText = ViewState["BudgetNo"].ToString();
        }


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
        DepartmentId.InnerText = ViewState["DeptNo"].ToString();



        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "1";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = Convert.ToDecimal(txtIGSTPER.Text).ToString();
        IGSTAMOUNT.InnerText = Convert.ToDecimal(txtIGSTAMT.Text).ToString();
        IGSTonAmount.InnerText = Convert.ToDecimal(ViewState["Amount"].ToString()).ToString();
        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";







        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTNNO.Text != "" || txtGSTNNO.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = "0";

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
    }

    private XmlDocument AddTDSGrid(XmlDocument objXMLDoc, string voucherno)
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

        IsGSTApplicable.InnerText = "0";
        Section.InnerText = "0";

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPartyName.Text.Trim().ToString();
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
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureService.Text.Trim().ToString();
        }

        ViewState["BankName"] = txtCashBankLedger.Text.ToString().Split('*')[0].ToString();

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
        OPARTY.InnerText = Convert.ToInt32(txtCashBankLedger.Text.Split('*')[1]).ToString();
        // OPARTY.InnerText = ViewState["OPartyNo"].ToString();
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "J";

        TRAN.InnerText = "Cr";

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = ddlSection.SelectedValue;
        TDSAMOUNT.InnerText = Convert.ToDecimal(txtTDSAmount.Text).ToString();
        TDPersentage.InnerText = "0";

        PARTY_NO.InnerText = Convert.ToInt32(txtTDSLedger.Text.ToString().Split('*')[1]).ToString();


        //AMOUNT.InnerText = (Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(ViewState["TDSAmount"].ToString())).ToString();
        AMOUNT.InnerText = Convert.ToDecimal(txtTDSAmount.Text).ToString();
        AmtWithoutGST.InnerText = "0";
        GSTPercent.InnerText = "0";
        //TDSSection.InnerText = "0";
        //TDSAMOUNT.InnerText = "0";
        //TDPersentage.InnerText = "0";

        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = "0";

        STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "3";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "J";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

        CHQ_NO.InnerText = "0";

        CHQ_DATE.InnerText = Convert.ToDateTime(txtChequeDt2.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");

        if (ViewState["BudgetNo"].ToString() == "0" || ViewState["BudgetNo"].ToString() == "")
        {
            BudgetNo.InnerText = "0";
        }
        else
        {
            BudgetNo.InnerText = ViewState["BudgetNo"].ToString();
        }


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
        DepartmentId.InnerText = ViewState["DeptNo"].ToString();



        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "1";
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
        SGSTApplicable.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";







        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTNNO.Text != "" || txtGSTNNO.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }
        GSTIN_NO.InnerText = "-";


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = "0";

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
    }
    #endregion

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            string voucherNo = "0";
            voucherNo = Session["vchno"].ToString();

            ViewState["isModi"] = "N";


            isFourSign = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
            isBankCash = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

            if (isFourSign == "N")
            {
                ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", "Payment", voucherNo, isBankCash);
            }
            else if (isFourSign == "Y")
            {
                ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Format2.rpt", "Journal", voucherNo);
            }


            upd_ModalPopupExtender1.Show();
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
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", Script, true);
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Clear();
        BindInvoice();
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
        btnback_Click(sender, e);
    }
    protected void btnchequePrint_Click(object sender, EventArgs e)
    {
        Session["comp_code"] = Session["comp_code"].ToString();
        upd_ModalPopupExtender1.Show();
    }
    private void ShowVoucherCashBankReport(string reportTitle, string rptFileName, String TransactionType, string VchNo, string isBankCash)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            string Comp_Name = objCommon.LookUp("ACC_Company", "COMPANY_NAME", "COMPANY_CODE ='" + Session["comp_code"].ToString() + "'");

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
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim() + ",BankORCashName=" + isBankCash;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upStoreInvoice, upStoreInvoice.GetType(), "Script", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "acc_store_invoice.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    public void HideTax()
    {
        divgst.Visible = false;
        divcgst.Visible = false;
        divIgst.Visible = false;
        //dvTDS.Visible = false;
    }
    public void MakeEnableFalseinTaxListview(string TaxType)
    {
        foreach (ListViewItem item in lvTaxList.Items)
        {
            Label Head = (Label)item.FindControl("lblTaxHead");
            TextBox Amount = (TextBox)item.FindControl("txtAmount");
            Label Mode = (Label)item.FindControl("lblMode");
            TextBox Ledger = (TextBox)item.FindControl("txtTaxLedger");

            if (TaxType.ToString() == "GST")
            {
                Amount.Enabled = false;
                Ledger.Enabled = false;
            }

        }
    }


    private int chekTaxExist(string TaxType)
    {

        int Result = 0;

        decimal Gst = 0, Igst = 0, Tds = 0;
        foreach (ListViewItem item in lvTaxList.Items)
        {
            Label Head = (Label)item.FindControl("lblTaxHead");
            TextBox Amount = (TextBox)item.FindControl("txtAmount");
            Label Mode = (Label)item.FindControl("lblMode");
            if (TaxType.ToString() == "GST")
            {
                if (Head.Text == "SGST" || Head.Text == "CGST")
                {
                    //    Gst = Gst + Convert.ToDecimal(Amount.Text);

                    //}
                    //if (Gst > 0)
                    //{
                    Result = 1;
                }
            }
            if (TaxType.ToString() == "IGST")
            {
                if (Head.Text == "IGST")
                {

                    //}
                    //if (Igst > 0)
                    //{
                    Result = 1;
                }
            }
            if (TaxType.ToString() == "Tds")
            {
                if (Head.Text == "TDS")
                {
                    //    Tds = Convert.ToDecimal(Amount.Text);
                    //}
                    //if (Tds > 0)
                    //{
                    Result = 1;
                }
            }

        }
        return Result;
    }
    protected void chkIGST_CheckedChanged(object sender, EventArgs e)
    {
        HideTax();
        if (chekTaxExist("IGST") > 0)
        {
            objCommon.DisplayUserMessage(upStoreInvoice, "IGST is Already Exists for this Invoice", this.Page);
            chkIGST.Checked = false;
            return;
        }

        if (chkIGST.Checked)
        {
            chkGST.Checked = false;
            divIgst.Visible = true;

            MakeZeroTaxAmount(2);
            MakeEnableFalseinTaxListview("GST");

        }
        else
        {
            divIgst.Visible = false;
            txtIGST.Text = txtIGSTAMOUNT.Text = txtIGSTAMT.Text = txtIGSTPER.Text = string.Empty;
            BindTax();
        }

    }
    protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    {
        if (chekTaxExist("Tds") > 0)
        {
            objCommon.DisplayUserMessage(upStoreInvoice, "TDS is Already Exists for this Invoice", this.Page);
            return;
        }


        if (chkTDSApplicable.Checked)
        {
            dvTDS.Visible = true;
            objCommon.FillDropDownList(ddlSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
            MakeZeroTaxAmount(3);
        }
        else
        {
            dvTDS.Visible = false;
            txtTDSPer.Text = string.Empty;
            txtTDSLedger.Text = string.Empty;
            txtTDSAmount.Text = string.Empty;
            ddlSection.SelectedValue = "0";
            BindTax();
        }
    }
    protected void chkGST_CheckedChanged(object sender, EventArgs e)
    {
        HideTax();
        if (chekTaxExist("GST") > 0)
        {
            objCommon.DisplayUserMessage(upStoreInvoice, "GST is Already Exists for this Invoice", this.Page);
            chkGST.Checked = false;
            return;
        }

        if (chkGST.Checked)
        {
            chkIGST.Checked = false;
            divgst.Visible = true;
            divcgst.Visible = true;
            //MakeEnableFalseinTaxListview("GST");


        }
        else
        {
            divgst.Visible = false;
            divcgst.Visible = false;
            txtCGST.Text = txtCGSTAMOUNT1.Text = txtCGSTAMT.Text = txtCGSTPER.Text = string.Empty;
            txtSGST.Text = txtSGSTAMOUNT.Text = txtSGSTAMT.Text = txtSGTSPer.Text = string.Empty;
            BindTax();

        }

    }
    public void BindTax()
    {
        dsTax = objPC1.GetInvoiceTaxDetails(Convert.ToInt32(ViewState["Invtrno"].ToString()));
        Session["DataSet"] = dsTax;
        lvTaxList.DataSource = dsTax;
        lvTaxList.DataBind();
        calcListviewtaxamount(dsTax);
    }

    public void MakeZeroTaxAmount(int TaxType)
    {
        DataSet dt = (DataSet)Session["DataSet"];
        if (dt.Tables[0].Rows.Count > 0)
        {
            if (TaxType == 1)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (dt.Tables[0].Rows[i]["FNAME"].ToString() == "SGST" || dt.Tables[0].Rows[i]["FNAME"].ToString() == "CGST")
                    {
                        dt.Tables[0].Rows[i]["AMT"] = "0";
                        dt.Tables[0].Rows[i].AcceptChanges();
                    }
                }
            }
            if (TaxType == 2)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (dt.Tables[0].Rows[i]["FNAME"].ToString() == "SGST" || dt.Tables[0].Rows[i]["FNAME"].ToString() == "CGST" || dt.Tables[0].Rows[i]["FNAME"].ToString() == "IGST")
                    {
                        dt.Tables[0].Rows[i]["AMT"] = "0";
                        dt.Tables[0].Rows[i].AcceptChanges();
                    }
                }
            }
            if (TaxType == 3)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (dt.Tables[0].Rows[i]["FNAME"].ToString() == "TDS")
                    {
                        dt.Tables[0].Rows[i]["AMT"] = "0";
                        dt.Tables[0].Rows[i].AcceptChanges();
                    }
                }
            }
            lvTaxList.DataSource = dt;
            lvTaxList.DataBind();
            calcListviewtaxamount(dt);
        }
    }

    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtIGST_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtTDSLedger_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTDSPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlSection.SelectedValue);
        decimal TDSOnAmount = Convert.ToDecimal(ViewState["Amount"].ToString()) / 100;
        decimal Per = txtTDSPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTDSPer.Text);
        decimal Total = TDSOnAmount * Per;
        txtTDSAmount.Text = Total.ToString();
        txtPartyAmount.Text = Convert.ToDouble(Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(txtTDSAmount.Text)).ToString();
    }
    protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTDSPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlSection.SelectedValue);
        decimal TDSOnAmount = Convert.ToDecimal(ViewState["Amount"].ToString()) / 100;
        decimal Per = txtTDSPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTDSPer.Text);
        decimal Total = TDSOnAmount * Per;
        txtTDSAmount.Text = Total.ToString();
        txtPartyAmount.Text = Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"].ToString()) - Convert.ToDecimal(txtTDSAmount.Text)).ToString();
    }

    public object gst { get; set; }
    protected void GridInvoice_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int rowid = (e.Item.ItemIndex);
        Label PartyName = (Label)GridInvoice.Items[rowid].FindControl("lblVendorName");
        HiddenField DeptNo = (HiddenField)GridInvoice.Items[rowid].FindControl("hdndept");
        HiddenField Pno = (HiddenField)GridInvoice.Items[rowid].FindControl("hdnPno");

        ViewState["DeptNo"] = DeptNo.Value == "" ? "0" : DeptNo.Value;
        lblPname.Text = " " + PartyName.Text;
        ViewState["Pno"] = Pno.Value == "" ? "0" : Pno.Value;

    }
}