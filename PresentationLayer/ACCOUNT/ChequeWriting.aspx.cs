//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :Bill Check Approval                                                  
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

public partial class ACCOUNT_ChequeWriting : System.Web.UI.Page
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

                //divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                BindAccount();
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

    private void BindAccount()
    {
        DataSet ds = null;
        try
        {
            ds = objRPBController.GetCompAccount(Convert.ToInt32(Session["userno"].ToString()), "Y");

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

            Session["Chq_Comp_Code"] = ddlCompAccount.SelectedItem.Text.Split('*')[2].ToString();

            //objCommon.FillDropDownList(ddlBank, "ACC_" + Session["Chq_Comp_Code"].ToString() + "_PARTY", "PARTY_NO", "(UPPER(PARTY_NAME) + '*' + CAST(ACC_CODE AS NVARCHAR(20))) AS PARTY_NAME", "PAYMENT_TYPE_NO = 2", "PARTY_NAME");

            //objCommon.FillDropDownList(ddlRequestNo, " ACC_BILL_CHECK_APPROVE C INNER JOIN ACC_RAISING_PAYMENT_BILL B ON (C.BILL_NO = B.SERIAL_NO)", "Distinct SEQ_NO", "CHKBILL_APPRNO", "B.[STATUS] = 'A' AND B.TRANS_BANKID =" + Convert.ToInt32(ddlBank.SelectedValue) + " AND C.COMPANY_CODE = '" + Session["Chq_Comp_Code"].ToString() + "'", "SEQ_NO");
            //DataSet ds11 = null;
            try
            {
                //ds11 = objRPBController.GetRequestNoDetails(Session["Chq_Comp_Code"].ToString(), Convert.ToInt32(ddlBank.SelectedValue), Convert.ToInt32(ddlCompAccount.SelectedItem.Text.Split('*')[0].ToString()));

                //if (ds11.Tables[0].Rows.Count > 0)
                //{
                //    rptRequestno.DataSource = ds11.Tables[0];
                //    rptRequestno.DataBind();
                //}
                //else
                //{
                //    //objCommon.DisplayMessage(updChkBill, "No Data found", this.Page);
                //    rptRequestno.DataSource = null;
                //    rptRequestno.DataBind();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            pnlChqList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNTS_BillCheckReport.BindAccount() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void clear()
    {
        rptChqPrintList.DataSource = null;
        rptChqPrintList.DataBind();

        rptPrint.DataSource = null;
        rptPrint.DataBind();

        rptRequestno.DataSource = null;
        rptRequestno.DataBind();

        ddlBank.Items.Clear();

        BindAccount();

        pnlChqList.Visible = false;
    }

    protected void ddlCompAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCompAccount.SelectedValue) > 0)
        {
            string compcode = ddlCompAccount.SelectedItem.Text.ToString().Split('*')[2].ToString();
            Session["Chq_Comp_Code"] = compcode;

            objCommon.FillDropDownList(ddlBank, "ACC_" + Session["Chq_Comp_Code"].ToString() + "_PARTY", "PARTY_NO", "(UPPER(PARTY_NAME) + '*' + CAST(ACC_CODE AS NVARCHAR(20))) AS PARTY_NAME", "PAYMENT_TYPE_NO = 2", "PARTY_NAME");

            //objCommon.FillDropDownList(ddlRequestNo, " ACC_BILL_CHECK_APPROVE C INNER JOIN ACC_RAISING_PAYMENT_BILL B ON (C.BILL_NO = B.SERIAL_NO)", "Distinct SEQ_NO", "CHKBILL_APPRNO", "B.[STATUS] = 'A' AND B.TRANS_BANKID=" + Convert.ToInt32(ddlBank.SelectedValue) + " AND C.COMPANY_CODE = '" + Session["Chq_Comp_Code"].ToString() + "'", "SEQ_NO");

            DataSet ds = null;
            try
            {
                if (Convert.ToInt32(ddlBank.SelectedValue) > 0)
                {
                    ds = objRPBController.GetRequestNoDetails(Session["Chq_Comp_Code"].ToString(), Convert.ToInt32(ddlBank.SelectedValue), Convert.ToInt32(ddlCompAccount.SelectedItem.Text.Split('*')[0].ToString()));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rptRequestno.DataSource = ds.Tables[0];
                        rptRequestno.DataBind();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updChkBill, "No Data found", this.Page);
                        rptRequestno.DataSource = null;
                        rptRequestno.DataBind();
                    }
                }
                else
                {
                    rptRequestno.DataSource = null;
                    rptRequestno.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            rptRequestno.DataSource = null;
            rptRequestno.DataBind();
            ddlBank.Items.Clear();
        }
        pnlChqList.Visible = false;
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlRequestNo, " ACC_BILL_CHECK_APPROVE C INNER JOIN ACC_RAISING_PAYMENT_BILL B ON (C.BILL_NO = B.SERIAL_NO)", "Distinct SEQ_NO", "CHKBILL_APPRNO", "B.[STATUS] = 'A' AND B.TRANS_BANKID =" + Convert.ToInt32(ddlBank.SelectedValue) + " AND C.COMPANY_CODE = '" + Session["Chq_Comp_Code"].ToString() + "'", "SEQ_NO");

        DataSet ds = null;
        try
        {
            if (Convert.ToInt32(ddlBank.SelectedValue) > 0)
            {
                ds = objRPBController.GetRequestNoDetails(Session["Chq_Comp_Code"].ToString(), Convert.ToInt32(ddlBank.SelectedValue), Convert.ToInt32(ddlCompAccount.SelectedItem.Text.Split('*')[0].ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptRequestno.DataSource = ds.Tables[0];
                    rptRequestno.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updChkBill, "No Data found", this.Page);
                    rptRequestno.DataSource = null;
                    rptRequestno.DataBind();
                }
            }
            else
            {
                rptRequestno.DataSource = null;
                rptRequestno.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        pnlChqList.Visible = false;
    }
    protected void ddlRequestNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSavePrint_Click(object sender, EventArgs e)
    {
        ChequePrintMaster cpm = new ChequePrintMaster();
        AccountTransactionController oatc = new AccountTransactionController();

        int res = 0;

        Button btnSavePrint1 = sender as Button;
        int ChqId = Convert.ToInt32(btnSavePrint1.CommandArgument);

        ///cpm.VNO = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Convert.ToInt32(ViewState["TRANSACTION_NO"]));
        //String a = (ViewState["vchno"]).ToString();
        //cpm.VNO = a;
        //int res = oatc.DeleteChequeEntryDetails_New(cpm.VNO, Session["comp_code"].ToString().Trim());
        for (int i = 0; i < rptChqPrintList.Items.Count; i++)
        {
            Label lblVoucherNo = rptChqPrintList.Items[i].FindControl("lblVoucherNo") as Label;
            Label lblPayeeName = rptChqPrintList.Items[i].FindControl("lblPayeeName") as Label;
            Label lblNatureService = rptChqPrintList.Items[i].FindControl("lblNatureService") as Label;
            Label lblNetAmount = rptChqPrintList.Items[i].FindControl("lblNetAmount") as Label;
            Label lblDepartment = rptChqPrintList.Items[i].FindControl("lblDepartment") as Label;
            TextBox txtChqNo = rptChqPrintList.Items[i].FindControl("txtChqNo") as TextBox;
            HiddenField hdnBillNo = rptChqPrintList.Items[i].FindControl("hdnBillNo") as HiddenField;

            Button btnSavePrint = rptChqPrintList.Items[i].FindControl("btnSavePrint") as Button;

            if (ChqId == Convert.ToInt32(btnSavePrint.CommandArgument))
            {
                cpm.VNO = lblVoucherNo.Text;
                cpm.PARTYNAME = lblPayeeName.Text.ToString().Trim();

                string VchDate = objCommon.LookUp("ACC_" + Session["Chq_Comp_Code"].ToString().Trim() + "_TRANS", "TRANSACTION_DATE", "BILL_ID =" + Convert.ToInt32(hdnBillNo.Value) + " AND SUBTR_NO <> 0");

                ViewState["TRANSACTION_NO"] = objCommon.LookUp("ACC_" + Session["Chq_Comp_Code"].ToString().Trim() + "_TRANS", "TRANSACTION_NO", "BILL_ID =" + Convert.ToInt32(hdnBillNo.Value) + " AND SUBTR_NO <> 0");

                ViewState["VchDate"] = VchDate;
                cpm.VDT = Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy");
                cpm.AMOUNT = Convert.ToDouble(lblNetAmount.Text.ToString().Trim());
                string bankno = objCommon.LookUp("ACC_" + Session["Chq_Comp_Code"].ToString().Trim() + "_PARTY a inner join ACC_" + Session["Chq_Comp_Code"].ToString().Trim() + "_BANKAC b on (a.BANKACCOUNTNO=b.ACCNO)", "b.BNO", "a.PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                if (bankno == "")
                {
                    objCommon.DisplayUserMessage(updChkBill, "Please map bank name with ledger", this.Page);
                    return;
                }
                cpm.BANKNO = Convert.ToInt32(bankno);//Convert.ToUInt16(ViewState["BankNo"].ToString().Trim());
                if (txtChqNo.Text == "" || txtChqNo.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(updChkBill, "Please Enter Cheque Number", this.Page);
                    txtChqNo.Focus();
                    return;
                }
                if (txtChqNo.Text != "")
                    cpm.CHECKNO = txtChqNo.Text.ToString().Trim();
                else
                    cpm.CHECKNO = "0";

                string lblDate = objCommon.LookUp("ACC_BILL_CHECK_APPROVE", "CHECKDATE", "BILL_NO =" + Convert.ToInt32(hdnBillNo.Value));

                if (lblDate.ToString() != "")
                    cpm.CHECKDT = Convert.ToDateTime(lblDate.ToString()).ToString("dd-MMM-yyyy");
                else
                    cpm.CHECKDT = "1";
                cpm.USERNAME = Session["username"].ToString().Trim();
                cpm.COMPANY_CODE = Session["Chq_Comp_Code"].ToString().Trim();

                cpm.STAMP = "AcPayee";

                NumberWords nw = new NumberWords();
                cpm.REASON1 = nw.changeToWords(lblNetAmount.Text.ToString(), true);
                res = oatc.AddChequeEntryDetails_New(cpm, Session["Chq_Comp_Code"].ToString().Trim());
                if (res == 2)
                {
                    objCommon.DisplayUserMessage(updChkBill, "Cheque No. Is Invalid.", this);
                    return;
                }
                else
                {
                    //objCommon.DisplayUserMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);
                    //objCommon.DisplayMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);
                }
                //btnGenerate.Visible = true;
                //btnGenerate.Focus();
                ViewState["VchNo"] = cpm.VNO;
            }
        }

        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_REF_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + Session["Chq_Comp_Code"] + "_CHECK_PRINT", "VNO,PARTYNAME,CHECKNO,CONVERT(nvarchar(20),CHECKDT,103)CHECKDT,STAMP,AMOUNT,CTRNO", "", "VNO='" + ViewState["VchNo"].ToString() + "' and VDT='" + Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy") + "'", "");

        if (ds != null)
        {
            rptPrint.DataSource = ds.Tables[0];
            rptPrint.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                upd_ModalPopupExtender1.Show();
            }
        }

        upd_ModalPopupExtender1.Show();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        Button btnShow = sender as Button;
        string Reqno = btnShow.CommandName.ToString();
        ViewState["reqno"] = Reqno;
        int reqno = Convert.ToInt32(btnShow.CommandArgument);

        lblReqNo.Text = Reqno.ToUpper().ToString();
        DataSet ds = null;
        try
        {
            //string Reqno = ddlRequestNo.SelectedItem.Text;
            int bankid = Convert.ToInt32(ddlBank.SelectedValue);
            ViewState["BankNo"] = bankid;
            int CompAccount = Convert.ToInt32(ddlCompAccount.SelectedItem.Text.ToString().Split('*')[0].ToString());

            ds = objRPBController.GetChequeList(Reqno, Session["chq_Comp_Code"].ToString(), bankid, CompAccount);

            if (ds.Tables[0].Rows.Count > 0)
            {
                rptChqPrintList.DataSource = ds.Tables[0];
                rptChqPrintList.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(updChkBill, "No Data Found", this.Page);
                rptChqPrintList.DataSource = null;
                rptChqPrintList.DataBind();
            }

            pnlChqList.Visible = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Button btnPrint = sender as Button;
        int CTRNO = Convert.ToInt32(btnPrint.CommandArgument);

        int CHKID = Convert.ToInt32(btnPrint.CommandName);
        DataSet ds = objCommon.FillDropDown("ACC_BILL_CHECK_APPROVE A INNER JOIN ACC_" + Session["chq_Comp_Code"].ToString() + "_TRANS T ON A.BILL_NO = T.BILL_ID", "T.VOUCHER_NO", "T.TRANSACTION_NO,T.TRANSACTION_DATE,T.PARTY_NO,T.OPARTY", "T.SUBTR_NO <> 0 AND A.CHKID =" + CHKID, "");
        ViewState["VchDate"] = ds.Tables[0].Rows[0]["TRANSACTION_DATE"].ToString();
        ViewState["VchNo"] = ds.Tables[0].Rows[0]["VOUCHER_NO"].ToString();

        ViewState["BankNo"] = ds.Tables[0].Rows[0]["PARTY_NO"].ToString();
        ViewState["TRANSACTION_NO"] = ds.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();

        //string VchNo = objCommon.LookUp()

        string PartyName = string.Empty;
        DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_CHECK_PRINT", "PARTYNAME AS PARTYNAME,AMOUNT,CHECKDT,CHECKNO,BANKNO,CTRNO,STAMP", "", "VNO='" + ViewState["VchNo"].ToString().Trim() + "' and VDT='" + Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy").Trim() + "' and CTRNO='" + CTRNO + "'", "");
        if (ds11 != null)
        {
            if (ds11.Tables[0] != null)
            {
                //ViewState["BankNo"]
                for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                {
                    if (ds11.Tables[0].Rows[i][0].ToString() != "")
                    {
                        // Session["comp_code"] = Session["chq_Comp_Code"].ToString();
                        PartyName = "0" + "*" + ds11.Tables[0].Rows[i][0].ToString();
                        //PartyName = "'" + "0" + "*" + ds11.Tables[0].Rows[i][0].ToString() + "'";
                        string Script = string.Empty;
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
                        string reportTitle = "ChequePrint";
                        string bankno = objCommon.LookUp("ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_PARTY a inner join ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_BANKAC b on (a.BANKACCOUNTNO=b.ACCNO)", "b.BNO", "a.PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                        string can = string.Empty;

                        //Added by Nokhlal Kumar for Signature
                        string signature1 = "Finance Officer";
                        string signature2 = string.Empty;
                        //if (rdbRegistrar.Checked)
                        //{
                        signature2 = "Registrar";
                        //}
                        //else if (rdbPrincipal.Checked)
                        //{
                        //signature2 = "Principal";
                        //}
                        //else if (rdbDean.Checked)
                        //{
                        //    signature2 = "Dean";
                        //}

                        can = "false";
                        string accno = objCommon.LookUp("acc_" + Session["chq_Comp_Code"].ToString().Trim() + "_party", "BANKACCOUNTNO", "PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                        string CheckOrientation = objCommon.LookUp("ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_CONFIG", "PARAMETER", "CONFIGDESC='CHEQUE ORIENTATION(TRUE-HORIZONTAL,FALSE-VERTICAL)'");
                        if (CheckOrientation == "N")
                        {
                            url += "Reports/Cheque_Vertical_New.aspx?";
                        }
                        else
                        {
                            url += "Reports/Cheque_New.aspx?";
                        }
                        string vchSqn = objCommon.LookUp("ACC_" + Session["chq_Comp_Code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Convert.ToInt32(ViewState["TRANSACTION_NO"]));
                        url += "obj=" + bankno + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim().Replace(',', '$') + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + "," + accno + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + vchSqn + "," + can + "," + "0" + "," + signature1 + "," + signature2;
                        //Session["chqprint"] = ViewState["BankNo"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim() + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + ",0" + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + ViewState["VchNo"].ToString().Trim() + "," + can + "," + "0";
                        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        ScriptManager.RegisterClientScriptBlock(this.updChkBill, updChkBill.GetType(), "Report", Script, true);
                    }
                }
            }
        }
        //Session["comp_code"] = string.Empty;
    }
    protected void btnReturnChq_Click(object sender, EventArgs e)
    {
        Button btnReturnChq = sender as Button;
        string Billno = btnReturnChq.CommandArgument.ToString();
        string CHKID = btnReturnChq.CommandName;
        int ctrno = Convert.ToInt32(btnReturnChq.ToolTip);
        ViewState["CTRNO"] = ctrno.ToString();
        string requestNo = objCommon.LookUp("ACC_BILL_CHECK_APPROVE", "CHKBILL_APPRNO", "BILL_NO =" + Convert.ToInt32(Billno) + " AND CHKID =" + Convert.ToInt32(CHKID));
        string PayeeName = objCommon.LookUp("ACC_BILL_CHECK_APPROVE", "PAYEE_NAME", "BILL_NO =" + Convert.ToInt32(Billno) + " AND CHKID =" + Convert.ToInt32(CHKID));
        string voucherNo = objCommon.LookUp("ACC_BILL_CHECK_APPROVE", "VOUCHER_NO", "BILL_NO =" + Convert.ToInt32(Billno) + " AND CHKID =" + Convert.ToInt32(CHKID));

        lblBillNoPopup.Text = Billno;
        lblReqNoPopup.Text = requestNo.ToString();
        lblVoucherNoPopup.Text = voucherNo.ToString();
        lblPayeeNamePopup.Text = PayeeName.ToString();

        ViewState["Billno"] = Billno;
        ViewState["requestNo"] = requestNo;
        ViewState["voucherNo"] = voucherNo;
        ViewState["PayeeName"] = PayeeName;

        upd_ModelPopupReturn.Show();
    }
    protected void rptChqPrintList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TextBox txtChqNo = (TextBox)e.Item.FindControl("txtChqNo");
            Button btnSavePrint = (Button)e.Item.FindControl("btnSavePrint");

            if (txtChqNo.Text == "" || txtChqNo.Text == string.Empty)
            {

            }
            else
            {
                btnSavePrint.Enabled = false;
                txtChqNo.Enabled = false;
                txtChqNo.ToolTip = "Cannot be Edited";
                btnSavePrint.ToolTip = "This cheque is already Saved but you can print.";
            }
        }
    }
    protected void rptPrint_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandname = e.CommandName.ToString();
        if (commandname == "Cancel")
        {
            upd_ModalPopupExtender1.Hide();
            return;
        }
        string PartyName = string.Empty;
        DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_CHECK_PRINT", "PARTYNAME AS PARTYNAME,AMOUNT,CHECKDT,CHECKNO,BANKNO,CTRNO,STAMP", "", "VNO='" + ViewState["VchNo"].ToString().Trim() + "' and VDT='" + Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy").Trim() + "' and CTRNO='" + e.CommandArgument.ToString() + "'", "");
        if (ds11 != null)
        {
            if (ds11.Tables[0] != null)
            {
                for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                {
                    if (ds11.Tables[0].Rows[i][0].ToString() != "")
                    {
                        PartyName = "0" + "*" + ds11.Tables[0].Rows[i][0].ToString();
                        //PartyName = "'" + "0" + "*" + ds11.Tables[0].Rows[i][0].ToString() + "'";
                        string Script = string.Empty;
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
                        string reportTitle = "ChequePrint";
                        string bankno = objCommon.LookUp("ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_PARTY a inner join ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_BANKAC b on (a.BANKACCOUNTNO=b.ACCNO)", "b.BNO", "a.PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                        string can = string.Empty;

                        //Added by Nokhlal Kumar for Signature
                        string signature1 = "Finance Officer";
                        string signature2 = string.Empty;
                        //if (rdbRegistrar.Checked)
                        //{
                        signature2 = "Registrar";
                        //}
                        //else if (rdbPrincipal.Checked)
                        //{
                        //    signature2 = "Principal";
                        //}
                        //else if (rdbDean.Checked)
                        //{
                        //    signature2 = "Dean";
                        //}

                        can = "false";
                        string accno = objCommon.LookUp("acc_" + Session["chq_Comp_Code"].ToString().Trim() + "_party", "BANKACCOUNTNO", "PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                        string CheckOrientation = objCommon.LookUp("ACC_" + Session["chq_Comp_Code"].ToString().Trim() + "_CONFIG", "PARAMETER", "CONFIGDESC='CHEQUE ORIENTATION(TRUE-HORIZONTAL,FALSE-VERTICAL)'");
                        if (CheckOrientation == "N")
                        {
                            url += "Reports/Cheque_Vertical_New.aspx?";
                        }
                        else
                        {
                            url += "Reports/Cheque_New.aspx?";
                        }
                        string vchSqn = objCommon.LookUp("ACC_" + Session["chq_Comp_Code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Convert.ToInt32(ViewState["TRANSACTION_NO"]));
                        url += "obj=" + bankno + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim().Replace(',', '$') + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + "," + accno + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + vchSqn + "," + can + "," + "0" + "," + signature1 + "," + signature2;
                        //Session["chqprint"] = ViewState["BankNo"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim() + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + ",0" + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + ViewState["VchNo"].ToString().Trim() + "," + can + "," + "0";
                        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        ScriptManager.RegisterClientScriptBlock(this.updChkBill, updChkBill.GetType(), "Report", Script, true);
                    }
                }
            }
            upd_ModalPopupExtender1.Show();
        }
    }
    protected void rptChqPrintList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            ddlCompAccount.SelectedValue = "0";
            ddlBank.SelectedValue = "0";
            //string Reqno = ddlRequestNo.SelectedItem.Text;
            if (Convert.ToInt32(ddlCompAccount.SelectedValue) > 0 && Convert.ToInt32(ddlBank.SelectedValue) > 0)
            {
                string Reqno = ViewState["reqno"].ToString();
                int bankid = Convert.ToInt32(ddlBank.SelectedValue);
                ViewState["BankNo"] = bankid;
                int CompAccount = Convert.ToInt32(ddlCompAccount.SelectedItem.Text.ToString().Split('*')[0].ToString());

                ds = objRPBController.GetChequeList(Reqno, Session["chq_Comp_Code"].ToString(), bankid, CompAccount);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptChqPrintList.DataSource = ds.Tables[0];
                    rptChqPrintList.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updChkBill, "No Data Found", this.Page);
                    rptChqPrintList.DataSource = null;
                    rptChqPrintList.DataBind();
                }
            }
            else
            {
                rptRequestno.DataSource = null;
                rptRequestno.DataBind();
                ddlBank.SelectedValue = "0";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        upd_ModalPopupExtender1.Hide();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //ViewState["Billno"] = Billno;
        //ViewState["requestNo"] = requestNo;
        //ViewState["voucherNo"] = voucherNo;
        //ViewState["PayeeName"] = PayeeName;

        if (txtReturnRemarks.Text == "" || txtReturnRemarks.Text == string.Empty)
        {
            objCommon.DisplayMessage(updChkBill, "Please Enter Remarks for Bill Return", this.Page);
            txtReturnRemarks.Focus();
            upd_ModelPopupReturn.Show();
            return;
        }
        else
        {
            string retRemark = txtReturnRemarks.Text.ToString();
            int BillNo = Convert.ToInt32(ViewState["Billno"].ToString());
            int userno = Convert.ToInt32(Session["userno"].ToString());
            string PayeeName = ViewState["PayeeName"].ToString();
            int voucherNo = Convert.ToInt32(ViewState["voucherNo"].ToString());
            int CTRNO = 0;
            if (ViewState["CTRNO"].ToString() == "" || ViewState["CTRNO"].ToString() == string.Empty)
                CTRNO = 0;
            else
                CTRNO = Convert.ToInt32(ViewState["CTRNO"].ToString());

            int retstatus = objRPBController.ReturnChequeBill(retRemark, userno, BillNo, PayeeName, voucherNo, Session["Chq_Comp_Code"].ToString(), CTRNO);

            if (retstatus == 2)
            {

                lblBillNoPopup.Text = "";
                lblPayeeNamePopup.Text = "";
                lblReqNoPopup.Text = "";
                lblVoucherNoPopup.Text = "";
                txtReturnRemarks.Text = "";

                // btnCancel_Click(sender, e);

                clear();
                pnlChqList.Visible = false;

                objCommon.DisplayMessage(updChkBill, "Cheque Bill Return Successfully!", this.Page);

                upd_ModelPopupReturn.Hide();
            }
            else
            {
                objCommon.DisplayMessage(updChkBill, "Exception occur, Please try Again", this.Page);
                return;
            }
        }
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        lblBillNoPopup.Text = "";
        lblPayeeNamePopup.Text = "";
        lblReqNoPopup.Text = "";
        lblVoucherNoPopup.Text = "";
        txtReturnRemarks.Text = "";

        upd_ModelPopupReturn.Hide();
    }
}