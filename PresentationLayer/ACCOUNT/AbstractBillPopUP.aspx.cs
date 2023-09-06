//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNTING VOUCHERS MODIFICATIONS                                                     
// CREATION DATE : 20-JUN-2014                                               
// CREATED BY    : NITIN MESHRAM                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;

public partial class ACCOUNT_AbstractBillPopUP : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();
    AccountTransaction objTran = new AccountTransaction();
    AccountTransactionController objTranController = new AccountTransactionController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["comp_code"] == null)
        {
            Response.Redirect("~/Account/selectCompany.aspx");
        }
        // To Set the MasterPage
        else if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["WithoutCashBank"] = "N";

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

                    objCommon.DisplayMessage("Select company/cash book.", this);

                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //PopulateDropDown();
                    //PopulateListBox();

                    ViewState["action"] = "add";
                }

            }
            SetFinancialYear();
           
            objCommon.FillDropDownList(ddlBank, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO=2", "");
            //if (Request.QueryString["Voucher_Type"].ToString() != "PV")
            //{

            //    objCommon.FillDropDownList(ddlleager, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");
            //}
            //else
            //    divledger.Visible = false;
            if (Request.QueryString["Voucher_Type"] != null)
            {
                trDate.Visible = true;
                FillData();

            }
            else
            {
                trDate.Visible = false;
            }
            if (lblBillNo.Text != "PV")
            {
                divledger.Visible = false;
            }
        }
    }
    private void FillData()
    {
        string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
        objCommon.FillDropDownList(ddlAbtractBillNo, "ACC_" + Session["comp_code"].ToString() + "_" + "PAYMENT_VOUCHER_TRANS_" + Request.QueryString["Voucher_Type"].ToString(), "distinct ABS_BILL_NO as BillValue", "ABS_BILL_NO", "TRansaction_Date between '" + FinancialDate + "'", "ABS_BILL_NO");
        DataSet DS = new DataSet();
        DS = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + Request.QueryString["Voucher_Type"].ToString(), "*", "", "ABS_BILL_NO=" + Request.QueryString["BillNo"].ToString() + " and Head_AccountId is null", "");
        ddlAbstractBillType.SelectedValue = Request.QueryString["Voucher_Type"].ToString();
        ddlAbtractBillNo.Text = Request.QueryString["BillNo"].ToString();
        lblBillNo.Text = Request.QueryString["BillNo"].ToString();
        ddlBank.SelectedValue = DS.Tables[0].Rows[0]["Mapped_Bank"].ToString();
        txtChequeNo.Text = DS.Tables[0].Rows[0]["ChequeNo"].ToString();
        if (DS.Tables[0].Rows[0]["chequeDate"].ToString() == null)
        {
            txtChequeDate.Text = Convert.ToDateTime(DS.Tables[0].Rows[0]["chequeDate"].ToString()).ToString("dd-MMM-yyyy");
        }

    }

    private void SetFinancialYear()
    {
        FinCashBookController objCBC = new FinCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();

        }
        dtr.Close();
    }

    private void showData()
    {
        lblBillNo.Text = ddlAbtractBillNo.SelectedItem.Text;
        //DataSet dsData = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlAbstractBillType.SelectedValue.ToString() + "", "A.TRANSACTION_NO,a.Head_AccountId as DeductHeadNo", "", "ABS_BILL_NO=" + lblBillNo.Text + " and Mapped_Bank is not null", "");
        
        DataSet dsLvGrp = null;
        if (lblBillNo.Text != "PV")
            dsLvGrp = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlAbstractBillType.SelectedValue.ToString() + " A inner join ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT B on (A.Head_AccountId=B.ACC_ID)", "A.TRANSACTION_NO,b.Acc_Name as DeductHead,a.Head_AccountId as DeductHeadNo", "", "ABS_BILL_NO=" + lblBillNo.Text + " and IsTransferable='true'", "");
        else
            dsLvGrp = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlAbstractBillType.SelectedValue.ToString() + " A inner join ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT B on (A.Head_AccountId=B.ACC_ID)", "A.TRANSACTION_NO, Acc_Name+' '+cast(TaxPercent as nvarchar(20))  as DeductHead,a.Head_AccountId as DeductHeadNo", "", "ABS_BILL_NO=" + lblBillNo.Text + " and IsTransferable='true' group by Acc_Name,Head_AccountId,TaxPercent", "");

        for (int i = 0; i < dsLvGrp.Tables[0].Rows.Count; i++)
        {
            if (dsLvGrp.Tables[0].Rows[0]["DeductHeadNo"].ToString() != "0")
            {
                GridData.DataSource = dsLvGrp;
                GridData.DataBind();
            }
        }
        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            DropDownList ddlLedger = GridData.Rows[i].FindControl("ddlleager") as DropDownList;
            objCommon.FillDropDownList(ddlLedger, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");

        }
      
    }

    private void clear()
    {
        ddlBank.SelectedValue = "0";
        ddlleager.SelectedValue = "0";
        txtChequeNo.Text = string.Empty;
        txtChequeDate.Text = string.Empty;
        GridData.DataSource = null;
        GridData.DataBind();
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int retStatus = 0;

            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                DropDownList ddlLedger = GridData.Rows[i].FindControl("ddlleager") as DropDownList;
                if (ddlLedger.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(upd, "Please Map All Ledger", this.Page);
                    return;
                }
            }

            if (GridData.Rows.Count == 0)
            {
                objTran.BILLNO = Convert.ToInt32(lblBillNo.Text);
                objTran.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue);
                objTran.JV_PARTY = Convert.ToInt32(ddlleager.SelectedValue);
                if (txtChequeNo.Text != string.Empty)
                    objTran.CHQ_NO = txtChequeNo.Text.ToString();
                else
                    objTran.CHQ_NO = "0";

                if (txtChequeDate.Text != string.Empty)
                    objTran.CHQ_DATE = Convert.ToDateTime(txtChequeDate.Text);

                objTran.HEADACCOUNTID = "0";
                objTran.VOUCHER_TYPE = ddlAbstractBillType.SelectedValue.ToString();
                objTran.PARTY_NO = 0;
                retStatus = objTranController.AddMapping(objTran, Session["comp_code"].ToString(),"");
            }

            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                objTran.BILLNO = Convert.ToInt32(lblBillNo.Text);
                objTran.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue);
                objTran.JV_PARTY = Convert.ToInt32(ddlleager.SelectedValue);
                if (txtChequeNo.Text != string.Empty)
                    objTran.CHQ_NO = txtChequeNo.Text.ToString();
                else
                    objTran.CHQ_NO = "0";
                if (txtChequeDate.Text != string.Empty)
                    objTran.CHQ_DATE = Convert.ToDateTime(txtChequeDate.Text);
                HiddenField hdnDeductHeadNo = GridData.Rows[i].FindControl("DeductHeadNo") as HiddenField;
                HiddenField hdnTRANSACTION_NO = GridData.Rows[i].FindControl("hdnTRANSACTION_NO") as HiddenField;
                objTran.HEADACCOUNTID = hdnDeductHeadNo.Value.ToString();
                DropDownList ddlLedger = GridData.Rows[i].FindControl("ddlleager") as DropDownList;
                objTran.PARTY_NO = Convert.ToInt32(ddlLedger.SelectedValue);
                objTran.VOUCHER_TYPE = ddlAbstractBillType.SelectedValue.ToString();
                retStatus = objTranController.AddMapping(objTran, Session["comp_code"].ToString(), hdnTRANSACTION_NO.Value);
            }
            if (retStatus == 1)
            {
                if (Request.QueryString["Voucher_Type"] != null)
                {
                    //objCommon.DisplayUserMessage(upd, "Ledger Mapped Successfully", this.Page);
                    retStatus = objTranController.AddAbsractTransferEntry(objTran, Session["comp_code"].ToString());
                    if (retStatus == 1)
                    {
                        objCommon.DisplayUserMessage(upd, "Bill Transfer Successfully", this.Page);
                        //Response.Redirect("AbstractBillApprove.aspx?Success=true&Voucher_Type=" + Request.QueryString["Voucher_Type"].ToString());
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(upd, "Bill Mapped Sucessfuly", this.Page);
                }
            }
            clear();
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(upd, "Error " + ex.Message, this.Page);
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
    protected void ddlAbstractBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
        if(ddlAbstractBillType.SelectedValue!="0")
            objCommon.FillDropDownList(ddlAbtractBillNo, "ACC_" + Session["comp_code"].ToString() + "_" + "PAYMENT_VOUCHER_TRANS_" + ddlAbstractBillType.SelectedValue.ToString(), "distinct ABS_BILL_NO as BillValue", "ABS_BILL_NO", "TRansaction_Date between '" + FinancialDate + "'", "ABS_BILL_NO");
        if(ddlAbstractBillType.SelectedValue=="PV")
            objCommon.FillDropDownList(ddlleager, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");
        
    }
    protected void ddlAbtractBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAbtractBillNo.SelectedItem.Text != "--Please Select--")
        showData();
    }
}
