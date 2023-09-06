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


public partial class ACCOUNT_CostcenterReport : System.Web.UI.Page
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

                objCommon.FillDropDownList(ddlCostCentre, "ACC_" + Session["comp_code"].ToString() + "_CostCenter " + "a inner join Acc_" + Session["comp_code"].ToString() + "_CostCategory b on a.Cat_ID=b.Cat_ID", "CC_ID", "A.CCNAME + ' (' +b.Category+' )'", "", "");


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

    protected void btndb_Click(object sender, EventArgs e)
    {
        DateTime fromdate = Convert.ToDateTime(txtFrmDate.Text);
        DateTime todate = Convert.ToDateTime(txtUptoDate.Text);
        //int retVal= objCostCenterController.CostCenterReport(Session["comp_code"].ToString(), fromdate, todate);
        //if (retVal == 1)
        //{
        //    ShowReport("Cost Center", "costCenterReport.rpt");
        //}
        //ShowReport("Cost Center", "CostCentreWise_Rpt.rpt");
        ShowReport("Cost Center", "CostCenterReport_New.rpt");
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
            url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_Period=" + txtFrmDate.Text.ToString() + " To " + txtUptoDate.Text.ToString() + "," + "@P_COMPCODE=" + Session["comp_code"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text.ToString()).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text.ToString()).ToString("dd-MMM-yyyy") + "," + "@P_CATID=" + Convert.ToInt32(ddlCostCentre.SelectedValue.ToString());

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SetFinancialYear();
        ddlCostCentre.SelectedIndex = 0;
    }
}
