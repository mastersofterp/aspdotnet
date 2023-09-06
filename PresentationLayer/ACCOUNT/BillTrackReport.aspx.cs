//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :BILL TRACKING REPORT                                                
// CREATION DATE :21-08-2021                                            
// CREATED BY    :GOPAL ANTHATI                                      
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
using IITMS.NITPRM;
using System.IO;
using System.Configuration;
using System.Web;


public partial class ACCOUNT_BillTrackReport : System.Web.UI.Page
{
    Common objCommon = new Common();
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
        if (!Page.IsPostBack)
        {
            //Check Page Authoriztion
            CheckPageAuthorization();

            //BindBillList();   
            if (Session["comp_code"] == null)
            {
                Session["comp_set"] = "NotSelected";

                Response.Redirect("~/Account/selectCompany.aspx");
            }
            SetFinancialYear();

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
            txtFromDate.Text = Session["fin_date_from"].ToString();
            txtToDate.Text = Session["fin_date_to"].ToString();
        }
        dtr.Close();
    }
    private void BindBillList()
    {
        DataSet ds = null;
        if (txtFromDate.Text != "" && txtToDate.Text != "")
            ds = objRPBController.GetBillList(ddlBillStatus.SelectedValue, Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd"), Session["comp_code"].ToString());
        else
            ds = objRPBController.GetBillList(ddlBillStatus.SelectedValue, "", "", Session["comp_code"].ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvBillList.DataSource = ds.Tables[0];
            lvBillList.DataBind();
            lvBillList.Visible = true;
        }
        else
        {
            lvBillList.DataSource = ds.Tables[0];
            lvBillList.DataBind();
            lvBillList.Visible = false;
            MessageBox("No Records Found.");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindBillList();
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
           
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;

            //if (txtFromDate.Text != "" && txtToDate.Text != "")
            //    url += "&param=@P_STATUS=" + ddlBillStatus.SelectedValue + " @P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();
            //else
            //    url += "&param=@P_STATUS=" + ddlBillStatus.SelectedValue + " @P_FROM_DATE=" + txtFromDate.Text + "," + "@P_TO_DATE=" + txtToDate.Text + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

            ////To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);



            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            if (txtFromDate.Text != "" && txtToDate.Text != "")
                url += "&param=@P_STATUS=" + ddlBillStatus.SelectedValue + "," + "@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();
            else
                url += "&param=@P_STATUS=" + ddlBillStatus.SelectedValue + "," + "@P_FROM_DATE=" + txtFromDate.Text + "," + "@P_TO_DATE=" + txtToDate.Text + "," + "@P_COMP_CODE=" + Session["comp_code"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                MessageBox("To Date Should Be Greater Than Or Equal To From Date ");
                return;
            }
        }
        ShowReport("BillReport", "TrackBillReport.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlBillStatus.SelectedIndex = 0;
        lvBillList.Visible = false;
        lvBillList.DataSource = null;
        lvBillList.DataBind();
    }
}