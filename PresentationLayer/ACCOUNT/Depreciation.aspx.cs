//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : Depreciation Calculation
// CREATION DATE : 17-July-2014                                                  
// CREATED BY    : Nitin Meshram
//=================================================================================
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Depreciation : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    IITMS.UAIMS.BusinessLayer.BusinessEntities.Depreciation objDepreciation = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Depreciation();
    DepreciationController objDepreciationController = new DepreciationController();
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
                clear();
                PopulateLedger();
                
                                          
            }
            btnReport.Enabled = false;
            CheckPageAuthorization();
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
            
        }
        dtr.Close();
    }

    private void PopulateLedger()
    {
        DataSet dsLedger = objCommon.FillDropDown("acc_" + Session["comp_code"].ToString() + "_party", "PARTY_NAME", "PARTY_NO", "MGRP_NO=10016", "PARTY_NO");
        grdDepriciation.DataSource = dsLedger;
        grdDepriciation.DataBind();

        DataSet dsRate = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_DEPRECIATION", "*", "", "", "");
        if (dsRate != null)
        {
            for (int i = 0; i < grdDepriciation.Rows.Count; i++)
            {
                TextBox txtRate = grdDepriciation.Rows[i].FindControl("txt_rate") as TextBox;
                HiddenField hdnPartyNo = grdDepriciation.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                if (hdnPartyNo.Value != null || hdnPartyNo.Value!="0")
                {
                    txtRate.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_DEPRECIATION", "Rate", "Party_No=" + hdnPartyNo.Value);
                }
            }
        }
    }

    private void clear()
    {
        for (int i = 0; i < grdDepriciation.Rows.Count; i++)
        {
            TextBox txtRate = grdDepriciation.Rows[i].FindControl("txt_rate") as TextBox;
            txtRate.Text = "0";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int RetValBlank = objDepreciationController.SetDepreciationBlank(Session["comp_code"].ToString());
        for (int i = 0; i < grdDepriciation.Rows.Count; i++)
        {
            HiddenField hdnPartyNo = grdDepriciation.Rows[i].FindControl("hdnPartyNo") as HiddenField;
            TextBox txtRate = grdDepriciation.Rows[i].FindControl("txt_rate") as TextBox;
            objDepreciation.Party_No = Convert.ToInt32(hdnPartyNo.Value.ToString());
            if (txtRate.Text != string.Empty)
            {
                objDepreciation.Rate = Convert.ToInt32(txtRate.Text.ToString());
            }
            else
            {
                objDepreciation.Rate = 0;
            }
            objDepreciation.FromDate = Convert.ToDateTime(Session["fin_date_from"].ToString());
            string ToDate = "30/09/" + Convert.ToDateTime(Session["fin_date_from"].ToString()).Year;
            objDepreciation.ToDate = Convert.ToDateTime(ToDate);
            string SecondDate = "31/03/" + Convert.ToDateTime(Session["fin_date_to"].ToString()).Year;
            objDepreciation.SecondDate = Convert.ToDateTime(SecondDate);
            
            if (RetValBlank == 1)
            {
                int Retval = objDepreciationController.DepreciationAdd(objDepreciation, Session["comp_code"].ToString());
                if (Retval == 1)
                {
                    objCommon.DisplayUserMessage(upd, "Record Save Successfully", this.Page);
                    btnReport.Enabled = true;
                }
                else
                {
                    objCommon.DisplayUserMessage(upd, "Record Not Saved", this.Page);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(upd, "Record Not Saved", this.Page);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Depreciation Report", "DepreciationCalculation.rpt");
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
            url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_COMPCODE=" + Session["comp_code"];

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {

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

}
