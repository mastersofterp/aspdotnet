//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNTING VOUCHERS MODIFICATIONS                                                     
// CREATION DATE : 17-JUN-2014                                               
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


public partial class ACCOUNT_AbstractBillModification : System.Web.UI.Page
{

    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    public static DataTable dt1 = new DataTable();

    DataTable dt = new DataTable();
    public static int RowIndex = -1;

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
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //PopulateDropDown();
                    //PopulateListBox();
                    trGrid.Visible = false;
                    ViewState["action"] = "add";
                }

            }
            SetFinancialYear();

            if (Request.QueryString["Voucher_Type"] != null)
            {
                ddlVoucherType.SelectedValue = Request.QueryString["Voucher_Type"].ToString();
                btnGo_Click(sender, e);
            }

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
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }


    protected void btnGo_Click(object sender, EventArgs e)
    {

        trGrid.Visible = true;
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }


        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
        if (ddlVoucherType.SelectedValue != "PV")
        {
            DataSet dsGridData = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlVoucherType.SelectedValue + " PVT inner join acc_" + Session["comp_code"].ToString() + "_party party on(PVT.AccountID=party.party_no)", "distinct(ABS_BILL_NO),VOUCHER_TYPE,GROSS_AMOUNT,party.party_name", "Tot_Payable", "PVT.TRansaction_Date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "'", "");
            RptData.DataSource = dsGridData;
            RptData.DataBind();
            ViewState["Voucher_Type"] = ddlVoucherType.SelectedValue;

            for (int i = 0; i < RptData.Items.Count; i++)
            {
                ImageButton btnEdit = RptData.Items[i].FindControl("btnEdit") as ImageButton;
                DataSet dsLvGrp = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlVoucherType.SelectedValue + " A inner join ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT B on (A.Head_AccountId=B.ACC_ID) inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "convert(VARCHAR(11),TRansaction_Date,106)AS TRansaction_Date,b.Acc_Name as DeductHead,AMOUNT", "TRAN_TYPE", "ABS_BILL_NO=" + btnEdit.CommandArgument, "");

                ListView lvGrp = RptData.Items[i].FindControl("lvGrp") as ListView;
                lvGrp.DataSource = dsLvGrp;
                lvGrp.DataBind();
            }
        }
        else
        {
            DataSet dsGridData = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlVoucherType.SelectedValue + " PVT inner join acc_" + Session["comp_code"].ToString() + "_party party on(PVT.AccountID=party.party_no)", "distinct(ABS_BILL_NO),VOUCHER_TYPE,GROSS_AMOUNT,party.party_name,party.party_no", "Tot_Payable", "PVT.TRansaction_Date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "' and Head_AccountId is null", "");
            RptData.DataSource = dsGridData;
            RptData.DataBind();
            ViewState["Voucher_Type"] = ddlVoucherType.SelectedValue;

            for (int i = 0; i < RptData.Items.Count; i++)
            {
                ImageButton btnEdit = RptData.Items[i].FindControl("btnEdit") as ImageButton;
                DataSet dsLvGrp = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_PAYMENT_VOUCHER_TRANS_" + ddlVoucherType.SelectedValue + " A inner join ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT B on (A.Head_AccountId=B.ACC_ID) inner join acc_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT c on (a.Head_AccountId=c.ACC_ID)", "convert(VARCHAR(11),TRansaction_Date,106)AS TRansaction_Date,b.Acc_Name as DeductHead,AMOUNT", "TRAN_TYPE", "Accountid ='" + dsGridData.Tables[0].Rows[i]["party_no"].ToString() + "' and Head_AccountId is not null and ABS_BILL_NO=" + btnEdit.CommandArgument, "");

                ListView lvGrp = RptData.Items[i].FindControl("lvGrp") as ListView;
                lvGrp.DataSource = dsLvGrp;
                lvGrp.DataBind();
            }
        }
    }
    protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "VoucherView")
        {
            if (ViewState["Voucher_Type"].ToString() == "PV")
            {
                Response.Redirect("AbstractBillVouchersPayment.aspx?BillNo=" + e.CommandArgument.ToString() + "&Voucher_Type=" + ViewState["Voucher_Type"].ToString());
            }
            else
                Response.Redirect("AbstractBillVouchers.aspx?BillNo=" + e.CommandArgument.ToString() + "&Voucher_Type=" + ViewState["Voucher_Type"].ToString());
        }
        else if (e.CommandName == "VoucherPrint")
        {
            if (ViewState["Voucher_Type"].ToString() == "AAPV")
            {
                ShowReport("Abstract Bill Voucher", "VoucherPrintNewFormat.rpt", e.CommandArgument.ToString());
            }
            if (ViewState["Voucher_Type"].ToString() == "PV")
            {
                AccountTransaction objentityTran = new AccountTransaction();
                objentityTran.BILLNO = Convert.ToInt32(e.CommandArgument.ToString());
                objentityTran.VOUCHER_TYPE = "PV";
                objentityTran.COMPANY_CODE = Session["comp_code"].ToString();
                AccountTransactionController objTran = new AccountTransactionController();
                objTran.SetAbstractBillForPayment_Report(objentityTran);
                ShowReport("Abstract Bill Voucher", "VoucherPrintNewPaymentFormat.rpt", e.CommandArgument.ToString());
            }
            if (ViewState["Voucher_Type"].ToString() == "APV")
            {
                ShowReport("Abstract Bill Voucher", "VoucherPrintNewFormatAAPV.rpt", e.CommandArgument.ToString());
            }
            if (ViewState["Voucher_Type"].ToString() == "AV")
            {
                ShowReport("Abstract Bill Voucher", "VoucherPrintNewFormatAAPV.rpt", e.CommandArgument.ToString());
            }
        }
    }


    private void ShowReport(string reportTitle, string rptFileName, string billNo)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMP_CODE=" + Session["comp_code"].ToString() + "," + "@P_VOUCHER_NO=" + billNo + ",@P_VOUCHER_TYPE=" + ViewState["Voucher_Type"].ToString();

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RptData.DataSource = null;
        RptData.DataBind();
        ddlVoucherType.SelectedValue = "0";
        trGrid.Visible = false;
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
