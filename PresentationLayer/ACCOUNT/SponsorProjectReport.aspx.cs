//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : 
// CREATION DATE : 06-Oct-2012                                               
// CREATED BY    : KAPIL BUDHLANI                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and print the bankbook Report
//=================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACCOUNT_SponsorProjectReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //objCommon = new Common();
            //objCcbc = new CombinedCashBankBookController();
            //objrpc = new ReceiptPaymentController();

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        //Session["WithoutCashBank"] = "N";

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
                    objCommon.DisplayUserMessage(UPDLedger, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    //txtFrmDate.Text = DateTime.Now.ToShortDateString();
                    SetFinancialYear();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    objCommon.FillDropDownList(ddlProjName, "ACC_" + Session["comp_code"].ToString() + "_Project", "ProjectId", "ProjectName", "", "ProjectId");
                }
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
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();


    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }

        ShowReport("Sponsor Project", "SponsorProject.rpt");

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));



            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + ",@PCODE_YEAR=" + Session["comp_code"].ToString() + ",@P_ProjectId=" + ddlProjName.SelectedValue.ToString() + ",@PPROJECTID=" + ddlProjName.SelectedValue.ToString() + ",@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + ",@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@P_ProjectName=" + ddlProjName.SelectedItem.Text;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {

        }
    }

}