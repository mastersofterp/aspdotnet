//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Recommandation.aspx                                      
// CREATION DATE : 07-Sept-2015                                                    
// CREATED BY    : Nitin Meshram                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Transactions_Quotation_Str_BudgetAllocationFinance : System.Web.UI.Page
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

                objCommon.FillDropDownList(ddlQuotation, "(select DISTINCT QENTRY.QNO,IR.QUOTNO from STORE_ITEM_RECOMMAND IR INNER JOIN STORE_QUOTENTRY QENTRY ON (IR.QUOTNO=QENTRY.QUOTNO) inner join STORE_RECOMM_APP_STATUS recomstat on (QENTRY.QNO=recomstat.QNO) where STATUS IS NOT NULL) a inner join (select count(1) a,qno from STORE_RECOMM_APP_STATUS where STATUS='A' group by qno,PNO having COUNT(qno)=(select count(1) from Store_Recom_Approval_Path)) b on (a.QNO=b.QNO)", "a.QNO", "a.QUOTNO", "a.QNO not in (select qno from STORE_BUDGET_ALLOCATION_FINANCE)", "");
                if (ddlQuotation.Items.Count <= 1)
                    objCommon.DisplayMessage("No Quotation available for Budget Allocation", this.Page);

                objCommon.FillDropDownList(ddlBudget, "STORE_BUDGET_HEAD a inner join STORE_BUDGETHEAD_ALLOCTION b on (a.bhno=b.BHNO)", "distinct b.BHALNO", "a.BHNAME", "A.BHNO<>0", "");
                showBudgetAllocation();
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
            url += "&param=@P_QUOTNO=" + ddlQuotation.SelectedItem.Text + "," + "@P_PNO=" + ddlVendor.SelectedValue + "," + "@username=" + Session["userfullname"].ToString();
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

    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            if (ViewState["BANO"] == null)
            {
                ret = ObjStrRecom.SaveBudgetAllocation(0,Convert.ToInt32(ddlBudget.SelectedValue), Convert.ToInt32(ddlQuotation.SelectedValue), Convert.ToInt32(ddlVendor.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));
            }
            else
            {
                ret = ObjStrRecom.SaveBudgetAllocation(Convert.ToInt32(ViewState["BANO"].ToString()),Convert.ToInt32(ddlBudget.SelectedValue), Convert.ToInt32(ddlQuotation.SelectedValue), Convert.ToInt32(ddlVendor.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));
            }
            if (ret > 0)
            {
                objCommon.DisplayMessage("Budget Is successfully allocate", this.Page);
                showBudgetAllocation();
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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int BANO = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("STORE_BUDGET_ALLOCATION_FINANCE", "BHALNO,QNO", "PNO", "BANO=" + BANO, "");
            ddlBudget.SelectedValue = ds.Tables[0].Rows[0]["BHALNO"].ToString();
            ddlQuotation.SelectedValue = ds.Tables[0].Rows[0]["QNO"].ToString();
            ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["PNO"].ToString();
            ViewState["BANO"] = BANO;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Transaction Failed", this.Page);
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
        ddlBudget.SelectedValue = "0";
    }

    private void showBudgetAllocation()
    {
        DataSet ds = objCommon.FillDropDown("STORE_BUDGET_ALLOCATION_FINANCE BAF inner join STORE_BUDGETHEAD_ALLOCTION BA ON (BAF.BHALNO=BA.BHALNO) INNER JOIN STORE_BUDGET_HEAD BH ON (BA.BHNO=BH.BHNO) INNER JOIN STORE_QUOTENTRY QUOT ON (BAF.QNO=QUOT.QNO) INNER JOIN STORE_PARTY PARTY ON (BAF.PNO=PARTY.PNO)", "BANO,QUOT.QUOTNO", "PARTY.PNAME,BH.BHNAME", "", "");
        lvPAPath.DataSource = ds;
        lvPAPath.DataBind();

    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}