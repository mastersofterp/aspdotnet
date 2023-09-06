//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : GROUP VOUCHER REPORT                                                   
// CREATION DATE : 11-July-2015                                              
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
using IITMS.NITPRM;

public partial class ACCOUNT_GroupVoucher : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CombinedCashBankBookController objCcbc;

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
                    objCommon.FillDropDownList(ddlGroup, "ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NO", "MGRP_NAME", "MGRP_NO not in (1,30000)", "MGRP_NO");
                    SetFinancialYear();
                }
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
    protected void btnRP_Click(object sender, EventArgs e)
    {
        ShowReport("Group Voucher Report", "groupVoucherReport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string LedgerName = string.Empty;
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (rdbWithNarration.Checked)
                url += "&param=@P_COMP_CODE=" + Session["comp_code"].ToString()+ "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + ",@P_NV=0,@P_MGRPNO=" + ddlGroup.SelectedValue + ",@p_headName=" + ddlGroup.SelectedItem.Text;
            else
                url += "&param=@P_COMP_CODE=" + Session["comp_code"].ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + ",@P_NV=1,@P_MGRPNO=" + ddlGroup.SelectedValue + ",@p_headName=" + ddlGroup.SelectedItem.Text;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
