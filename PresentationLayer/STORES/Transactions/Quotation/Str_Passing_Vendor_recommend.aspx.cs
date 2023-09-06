//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Recommandation.aspx                                      
// CREATION DATE : 19-march-2010                                                    
// CREATED BY    : chaitanya Bhure                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Transactions_Quotation_Str_Passing_Vendor_recommend : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Recommandaion_Controller ObjStrRecom = new Str_Recommandaion_Controller();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
               CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                objCommon.FillDropDownList(ddlQuotation, "(select DISTINCT QENTRY.QNO,IR.QUOTNO from STORE_ITEM_RECOMMAND IR INNER JOIN STORE_QUOTENTRY QENTRY ON (IR.QUOTNO=QENTRY.QUOTNO) inner join STORE_RECOMM_APP_STATUS recomstat on (QENTRY.QNO=recomstat.QNO) where STATUS IS NOT NULL and recomstat.USERID=" + Session["userno"].ToString() + ") a inner join (select count(1) a,qno from STORE_RECOMM_APP_STATUS where STATUS<>'A' group by qno,PNO having COUNT(qno)>0) b on (a.QNO=b.QNO)", "a.QNO", "a.QUOTNO", "", "");
                //if (ddlQuotation.Items.Count <= 1)
                //    objCommon.DisplayMessage("No Quotation available for approval", this.Page);

                objCommon.FillDropDownList(ddlQuotationFund, "(select DISTINCT QENTRY.QNO,IR.QUOTNO from STORE_ITEM_RECOMMAND IR INNER JOIN STORE_QUOTENTRY QENTRY ON (IR.QUOTNO=QENTRY.QUOTNO) inner join STORE_FUND_APP_STATUS recomstat on (QENTRY.QNO=recomstat.QNO) where STATUS IS NOT NULL and recomstat.USERID=" + Session["userno"].ToString() + ") a inner join (select count(1) a,qno from STORE_FUND_APP_STATUS where STATUS<>'A' group by qno,PNO having COUNT(qno)>0) b on (a.QNO=b.QNO)", "a.QNO", "a.QUOTNO", "", "");
                //if (ddlQuotation.Items.Count <= 1)
                //    objCommon.DisplayMessage("No Quotation available for approval", this.Page);                
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowReport("RECOMMANDATION_REPORT", "reccomendationQuotation.rpt");
    }

    //To Show RECOMENDATION report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            if(Tabs.ActiveTabIndex.ToString()=="0")
            url += "&param=@P_QUOTNO=" + ddlQuotation.SelectedItem.Text + "," + "@P_PNO=" + ddlVendor.SelectedValue + "," + "@username=" + Session["userfullname"].ToString();
            else
                url += "&param=@P_QUOTNO=" + ddlQuotationFund.SelectedItem.Text + "," + "@P_PNO=" + ddlVendorFund.SelectedValue + "," + "@username=" + Session["userfullname"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "--> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowApproval()
    {
        DataSet ds = objCommon.FillDropDown("STORE_RECOMM_APP_STATUS a inner join user_acc b on (a.USERID=b.UA_NO)", "b.UA_FULLNAME", "case when status='A' THEN 'APPROVE' ELSE CASE WHEN STATUS='R' THEN 'REJECT' ELSE CASE WHEN STATUS='P' THEN 'PENDING' ELSE '' END END END STATUS", "QNO=" + ddlQuotation.SelectedValue + " AND PNO=" + ddlVendor.SelectedValue, "");
        lvPAPath.DataSource = ds;
        lvPAPath.DataBind();
    }

    private void ShowApprovalFund()
    {
        DataSet ds = objCommon.FillDropDown("STORE_FUND_APP_STATUS a inner join user_acc b on (a.USERID=b.UA_NO)", "b.UA_FULLNAME", "case when status='A' THEN 'APPROVE' ELSE CASE WHEN STATUS='R' THEN 'REJECT' ELSE CASE WHEN STATUS='P' THEN 'PENDING' ELSE '' END END END STATUS", "QNO=" + ddlQuotationFund.SelectedValue + " AND PNO=" + ddlVendorFund.SelectedValue, "");
        lvBudgetStatus.DataSource = ds;
        lvBudgetStatus.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;

            ret = ObjStrRecom.RecommendApprove(Convert.ToInt32(ddlQuotation.SelectedValue),Convert.ToInt32(ddlVendor.SelectedValue), ddlStatus.SelectedValue.ToString(),Convert.ToInt32(Session["userno"].ToString()));
            if (ret > 0)
            {
                objCommon.DisplayMessage("Proposal is approved", this.Page);
                ShowApproval();
                clear();
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
            
    }
    protected void ddlQuotation_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlVendor, "STORE_ITEM_RECOMMAND IR INNER JOIN STORE_PARTY SP ON (IR.PNO=SP.PNO)", "distinct SP.PNO", "SP.PNAME", "QUOTNO='" + ddlQuotation.SelectedItem.Text + "'", "");
    }

    private void clear()
    {
        ddlQuotation.SelectedValue = "0";
        ddlVendor.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
    }

    private void clearFund()
    {
        ddlQuotationFund.SelectedValue = "0";
        ddlVendorFund.SelectedValue = "0";
        ddlStatusFund.SelectedValue = "0";
        lblBudget.Text = string.Empty;
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowApproval();
    }
    protected void ddlQuotationFund_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlVendorFund, "STORE_ITEM_RECOMMAND IR INNER JOIN STORE_PARTY SP ON (IR.PNO=SP.PNO)", "distinct SP.PNO", "SP.PNAME", "QUOTNO='" + ddlQuotationFund.SelectedItem.Text + "'", "");
    }
    protected void btnProposal_Click(object sender, EventArgs e)
    {
        ShowReport("RECOMMANDATION_REPORT", "reccomendationQuotation.rpt");
    }
    protected void btnSubmitFund_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;

            ret = ObjStrRecom.RecommendApproveFund(Convert.ToInt32(ddlQuotationFund.SelectedValue), Convert.ToInt32(ddlVendorFund.SelectedValue), ddlStatusFund.SelectedValue.ToString(), Convert.ToInt32(Session["userno"].ToString()));
            if (ret > 0)
            {
                objCommon.DisplayMessage("Proposal is approved", this.Page);
                ShowApprovalFund();
                clearFund();
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlVendorFund_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("STORE_BUDGET_ALLOCATION_FINANCE A INNER JOIN STORE_BUDGETHEAD_ALLOCTION B ON (A.BHALNO=B.BHALNO) INNER JOIN STORE_BUDGET_HEAD C ON (B.BHNO=C.BHNO)", "A.BHALNO", "C.BHNAME", "A.QNO=" + ddlQuotationFund.SelectedValue + " AND PNO=" + ddlVendorFund.SelectedValue, "");
        lblBudget.Text = ds.Tables[0].Rows[0]["BHNAME"].ToString();
        ShowApprovalFund();
    }
}