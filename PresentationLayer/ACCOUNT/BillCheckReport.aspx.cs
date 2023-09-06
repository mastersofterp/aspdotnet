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
public partial class ACCOUNT_BillCheckReport : System.Web.UI.Page
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
                //if (Session["comp_code"] == null)
                //{
                //    Session["comp_set"] = "NotSelected";

                //    Response.Redirect("~/Account/selectCompany.aspx");
                //}
                //else
                //{
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                //divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                BindAccount();

                //objCommon.FillDropDownList(ddlBank, "ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "(UPPER(PARTY_NAME) + '*' + CAST(ACC_CODE AS NVARCHAR(20))) AS PARTY_NAME", "PAYMENT_TYPE_NO = 2", "PARTY_NAME");
                //objCommon.FillDropDownList(ddlRequestNo, "ACC_BILL_CHECK_APPROVE", "Distinct SEQ_NO", "CHKBILL_APPRNO", "COMPANY_CODE = '" + Session["comp_code"].ToString() + "'", "SEQ_NO");
                //}
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        if (ddlBank.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updChkBill, "Please select bank", this.Page);
            return;
        }
        if (ddlRequestNo.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updChkBill, "Please select Request no.", this.Page);
            return;
        }

        ShowReport("Bill Check Report", "ChequeRequestReport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int bankno = Convert.ToInt32(ddlBank.SelectedValue);
            string BankACCNo = objCommon.LookUp("ACC_" + Session["Chq_Comp_Code"].ToString() + "_PARTY", "BANKACCOUNTNO", "PARTY_NO = " + bankno);
            string BankAccName = objCommon.LookUp("ACC_" + Session["Chq_Comp_Code"].ToString() + "_BANKAC", "ACCNAME", "ACCNO = '" + BankACCNo + "'");

            Session["CHQ_Comp_Name"] = objCommon.LookUp("ACC_COMPANY", "COMPANY_NAME + '-' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(10)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(10))", "COMPANY_CODE ='" + Session["Chq_Comp_Code"].ToString() + "'");
            int seqno = Convert.ToInt32(ddlRequestNo.SelectedValue);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMP_CODE=" + Session["Chq_Comp_Code"].ToString() + "," + "@P_BANKNO=" + bankno + "," + "@P_SEQNO=" + seqno + "," + "@P_COMPANY_NAME=" + Session["CHQ_Comp_Name"].ToString().Trim().ToUpper() + "," + "@P_BANKACCNO=" + BankACCNo.ToString() + "," + "@BANKACCNAME=" + BankAccName.ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updChkBill, updChkBill.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BillCheckReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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

            objCommon.FillDropDownList(ddlBank, "ACC_" + Session["Chq_Comp_Code"].ToString() + "_PARTY", "PARTY_NO", "(UPPER(PARTY_NAME) + '*' + CAST(ACC_CODE AS NVARCHAR(20))) AS PARTY_NAME", "PAYMENT_TYPE_NO = 2", "PARTY_NAME");

            objCommon.FillDropDownList(ddlRequestNo, " ACC_BILL_CHECK_APPROVE C INNER JOIN ACC_RAISING_PAYMENT_BILL B ON (C.BILL_NO = B.SERIAL_NO)", "Distinct SEQ_NO", "CHKBILL_APPRNO", "B.[STATUS] = 'A' AND B.TRANS_BANKID =" + Convert.ToInt32(ddlBank.SelectedValue) + " AND C.COMPANY_CODE = '" + Session["Chq_Comp_Code"].ToString() + "'", "SEQ_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNTS_BillCheckReport.BindAccount() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void ddlCompAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        string compcode = ddlCompAccount.SelectedItem.Text.ToString().Split('*')[2].ToString();
        Session["Chq_Comp_Code"] = compcode;

        objCommon.FillDropDownList(ddlBank, "ACC_" + Session["Chq_Comp_Code"].ToString() + "_PARTY", "PARTY_NO", "(UPPER(PARTY_NAME) + '*' + CAST(ACC_CODE AS NVARCHAR(20))) AS PARTY_NAME", "PAYMENT_TYPE_NO = 2", "PARTY_NAME");

        objCommon.FillDropDownList(ddlRequestNo, " ACC_BILL_CHECK_APPROVE C INNER JOIN ACC_RAISING_PAYMENT_BILL B ON (C.BILL_NO = B.SERIAL_NO)", "Distinct SEQ_NO", "CHKBILL_APPRNO", "B.[STATUS] = 'A' AND B.TRANS_BANKID=" + Convert.ToInt32(ddlBank.SelectedValue) + " AND C.COMPANY_CODE = '" + Session["Chq_Comp_Code"].ToString() + "'", "SEQ_NO");
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlRequestNo, " ACC_BILL_CHECK_APPROVE C INNER JOIN ACC_RAISING_PAYMENT_BILL B ON (C.BILL_NO = B.SERIAL_NO)", "Distinct SEQ_NO", "CHKBILL_APPRNO", "B.[STATUS] = 'A' AND B.TRANS_BANKID =" + Convert.ToInt32(ddlBank.SelectedValue) + " AND C.COMPANY_CODE = '" + Session["Chq_Comp_Code"].ToString() + "'", "SEQ_NO");
    }
}