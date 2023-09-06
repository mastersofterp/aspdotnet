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
using IITMS.NITPRM;

public partial class ACCOUNT_BudgetReport : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenterController objCostCenterController = new CostCenterController();

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
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else { Session["comp_set"] = ""; }

                SetFinancialYear();
                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();

                //objCommon.FillDropDownList(ddlBudgetList, "ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");
                objCommon.FillDropDownList(ddlBudgetList, "ACC_BUDGET_HEAD_NEW a", "isnull(BUDGET_NO,0) BUDGET_NO", "BUDGET_HEAD", "not exists (select BUDGET_PRAPOSAL from ACC_BUDGET_HEAD_NEW b where a.BUDGET_NO=b.BUDGET_PRAPOSAL)", "BUDGET_HEAD");
            }
        }
    }

    private void SetFinancialYear()
    {
        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFrmDate.Text = Session["fin_date_from"].ToString();
            txtUptoDate.Text = Session["fin_date_to"].ToString();
        }
        dtr.Close();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            //string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (rdbHeadWise.Checked == true)
            {
                url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_Period=" + txtFrmDate.Text.ToString() + " To " + txtUptoDate.Text.ToString() + "," + "@P_COMPCODE=" + Session["comp_code"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text.ToString()).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text.ToString()).ToString("dd-MMM-yyyy") + "," + "@P_BUDNO=" + ddlBudgetList.SelectedValue;
            }
            else
            {
                url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_Period=" + txtFrmDate.Text.ToString() + " To " + txtUptoDate.Text.ToString() + "," + "@P_COMPCODE=" + Session["comp_code"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text.ToString()).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text.ToString()).ToString("dd-MMM-yyyy");
            }
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btndb_Click(object sender, EventArgs e)
    {
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
        DateTime fromdate = Convert.ToDateTime(txtFrmDate.Text);
        DateTime todate = Convert.ToDateTime(txtUptoDate.Text);

        if (rdbHeadWise.Checked == true)
        {
            if (ddlBudgetList.SelectedValue == "0")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Select any Budget Head", this);
                ddlBudgetList.Focus();
                return;
            }
            ShowReport("Budget HeadWise Report", "BudgetTransactionHeadWiseReport.rpt");
        }
        else
        {
            ShowReport("Budget Report", "BudgetTransactionReport.rpt");
        }
    }

    protected void rdbHeadWise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbHeadWise.Checked == true)
        {
            dvBudget.Visible = true;
        }
        else
        {
            dvBudget.Visible = false;
        }
    }
    protected void rdbAllBudget_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAllBudget.Checked == true)
        {
            dvBudget.Visible = false;
        }
        else
        {
            dvBudget.Visible = true;
        }
    }
    protected void BTNCANCEL_Click(object sender, EventArgs e)
    {
        rdbAllBudget.Checked = true;
        rdbHeadWise.Checked = false;
        SetFinancialYear();
        ddlBudgetList.SelectedIndex = 0;
    }
}
