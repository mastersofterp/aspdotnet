using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.UI.HtmlControls;


public partial class Acc_IncExpBalGrid : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    static string fromdate = string.Empty;
    static string todate = string.Empty;
    static string isNetSurplus = string.Empty;
    public bool IsShowMsg = true;
    string space1 = "     ".ToString();
    DataSet dsIncomeExp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        fromdate = Request.QueryString["fromDate"].ToString().Trim();
        todate = Request.QueryString["Todate"].ToString().Trim();

        if (!Page.IsPostBack)
        {
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

            }
            bindGrid();
        }
    }

    private void bindGrid()
    {
        TrialBalanceReportController objTb = new TrialBalanceReportController();
        int Ret = objTb.GenerateTrialBalance_DateWise(Session["comp_code"].ToString(), fromdate, todate, 0);

        lblLedger.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO=" + Request.QueryString["MainGrpNo"].ToString().Trim());
        lblFrm.Text = fromdate;
        lblTo.Text = todate;

        
        DataSet dsFinalHead = objTb.BindIncExpBalsheetGrid(Session["comp_code"].ToString(), Request.QueryString["MainGrpNo"].ToString().Trim());

        for (int i = 0; i < dsFinalHead.Tables[0].Rows.Count; i++)
        {
            dsFinalHead.Tables[0].Rows[i]["PARTYNAME"] = dsFinalHead.Tables[0].Rows[i]["PARTYNAME"].ToString().Replace(" ", "&nbsp;");
        }
        RptExpense.DataSource = dsFinalHead;
        RptExpense.DataBind();

        for (int i = 0; i < RptExpense.Items.Count; i++)
        {
            ImageButton btnEdit = RptExpense.Items[i].FindControl("btnEdit") as ImageButton;
            HtmlTableCell trPartyName = RptExpense.Items[i].FindControl("trPartyName") as HtmlTableCell;
            Label lblParty = RptExpense.Items[i].FindControl("lblParty") as Label;
            if (btnEdit.CommandArgument == "0" || btnEdit.ToolTip == "PROFIT & LOSS A/c")
            {
                btnEdit.Visible = false;
                btnEdit.Attributes.Add("class", "altitem");
                lblParty.Font.Bold = true;

            }
            else
            {
                //lnkLedgerReport.Attributes.Add("onclick", "return ShowledgerReport('" + lnkLedgerReport.ToolTip.Trim() + "','" + lnkLedgerReport.CommandArgument + "');");
                lblParty.Attributes.Add("onclick", "ShowledgerReport('" + btnEdit.ToolTip.Trim() + "','" + btnEdit.CommandArgument + "','" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy") + "');");
                lblParty.Attributes.Add("onmouseout", "this.style.backgroundColor='ThreeDFace'");
                lblParty.Attributes.Add("onmouseover", "this.style.backgroundColor='#81BEF7'");
                lblParty.Attributes.Add("style", "width: 100%;cursor:pointer;");

            }
        }
    }

}