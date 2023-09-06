//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : CONSOLIDATE BALANCESHEET REPORT
// CREATION DATE : 06-FEB-2019                                               
// CREATED BY    : NOKHLAL KUMAR
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.UI.HtmlControls;

public partial class ACCOUNT_ConsolidateBalanceSheetReport : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    string back = string.Empty;

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
            //CheckPageAuthorization();

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            BindCashBook();
        }
        SetFinancialYear();
    }

    private void BindCashBook()
    {
        DataSet ds = objCommon.FillDropDown("ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + Session["userno"].ToString() + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
        chkCashbook.DataSource = ds;
        chkCashbook.DataValueField = ds.Tables[0].Columns[0].ToString();
        chkCashbook.DataTextField = ds.Tables[0].Columns[1].ToString();
        chkCashbook.ToolTip = ds.Tables[0].Columns[0].ToString();
        chkCashbook.DataBind();
    }

    private void SetFinancialYear()
    {
        string year = Convert.ToDateTime(DateTime.Now.ToString()).Year.ToString();
        string month = Convert.ToDateTime(DateTime.Now.ToString()).Month.ToString();
        string FromDate = "01/04/";
        string ToDate = "31/03/";

        if (Convert.ToInt32(month) < 4)
        {
            FromDate = FromDate + (Convert.ToInt32(year.ToString()) - 1).ToString();
            ToDate = ToDate + year.ToString();
        }
        else
        {
            FromDate = FromDate + year.ToString();
            ToDate = ToDate + (Convert.ToInt32(year.ToString()) + 1).ToString();
        }

        txtFrmDate.Text = Convert.ToDateTime(FromDate).ToString("dd/MM/yyyy");
        txtUptoDate.Text = Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string compno = string.Empty;
        //get selected users
        foreach (ListItem ChkCashBook in chkCashbook.Items)
        {
            //CheckBox ChkCashbook = chkCashbook.FindControl("chkCashbook") as CheckBox;
            if (ChkCashBook.Selected == true)
            {
                if (compno == string.Empty)
                    compno += ChkCashBook.Value;
                else
                    compno += "$" + ChkCashBook.Value;
            }
        }

        if (compno == string.Empty)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select At least one cash book", this.Page);
            return;
        }

        if (txtFrmDate.Text == "" || txtFrmDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter From Date!", this.Page);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text == "" || txtUptoDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter Upto Date!", this.Page);
            txtUptoDate.Focus();
            return;
        }

        ShowReport("Consolidate Balancesheet report", "ConsolidateBalanceSheetReport.rpt", compno);
    }

    private void ShowReport(string reportTitle, string rptFileName, string compno)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@Comp_No=" + compno.ToString() + "," + "@FromDate=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@ToDate=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            //url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ConsolidateBalanceSheetReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}