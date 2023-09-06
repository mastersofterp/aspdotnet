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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACCOUNT_store_pass_order : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    storeIntegration objStore = new storeIntegration();
    storeIntegrationController objSIC = new storeIntegrationController();
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
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
        { }
        else
            Response.Redirect("~/Default.aspx");

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


                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                    //Fill dropdown list
                    // Filling Degrees

                    //Filling Recept list
                    SetFinancialYear();
                    Row20.Visible = false;
                    trPaid.Visible = false;
                }
            }

        }

        divMsg.InnerHtml = string.Empty;
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
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtTodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }

    private void fillGrid()
    {
        DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime toDate = Convert.ToDateTime(txtTodate.Text);
        if (rdbUnPaid.Checked == true)
        {
            trUnpaidPO.Visible = true;
            trPaid.Visible = false;
            DataSet dsPO = objSIC.getPassOrderDetail(_CCMS, fromDate.ToString("dd-MMM-yyyy"), toDate.ToString("dd-MMM-yyyy"));
            GridData.DataSource = dsPO;
            GridData.DataBind();

            int j = 2;
            for (int i = 0; i < GridData.Rows.Count; i++)
            {

                TextBox txtVoucherNo = GridData.Rows[i].FindControl("txtVoucherNo") as TextBox;
                if (i < 8)
                    txtVoucherNo.Attributes.Add("onblur", "return checkVoucherNoValid(this,ctl00_ContentPlaceHolder1_GridData_ctl0" + j + "_lblPayAmount,'Y');");
                else
                    txtVoucherNo.Attributes.Add("onblur", "return checkVoucherNoValid(this,ctl00_ContentPlaceHolder1_GridData_ctl" + j + "_lblPayAmount,'Y');");
                j++;
            }
        }
        else
        {
            trUnpaidPO.Visible = false;
            trPaid.Visible = true;
            DataSet dsPO = objSIC.getPaidPassOrderDetail(_CCMS, fromDate.ToString("dd-MMM-yyyy"), toDate.ToString("dd-MMM-yyyy"));
            gridDataPaid.DataSource = dsPO;
            gridDataPaid.DataBind();


            int j = 2;
            for (int i = 0; i < gridDataPaid.Rows.Count; i++)
            {
                ImageButton btnEdit = gridDataPaid.Rows[i].FindControl("btnEdit") as ImageButton;
                TextBox txtVoucherNo = gridDataPaid.Rows[i].FindControl("txtVoucherNo") as TextBox;
                if (i < 8)
                {
                    txtVoucherNo.Attributes.Add("onblur", "return checkVoucherNoValid(this,ctl00_ContentPlaceHolder1_gridDataPaid_ctl0" + j + "_lblPayAmount,'N');");
                    btnEdit.Attributes.Add("onClick", "return EnableTextbox(ctl00_ContentPlaceHolder1_gridDataPaid_ctl0" + j + "_txtVoucherNo)");
                    txtVoucherNo.Enabled = false;
                }
                else
                {
                    txtVoucherNo.Attributes.Add("onblur", "return checkVoucherNoValid(this,ctl00_ContentPlaceHolder1_gridDataPaid_ctl" + j + "_lblPayAmount,'N');");
                    btnEdit.Attributes.Add("onClick", "return EnableTextbox(ctl00_ContentPlaceHolder1_gridDataPaid_ctl" + j + "_txtVoucherNo)");
                    txtVoucherNo.Enabled = false;
                }
                j++;
            }
        }


    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        fillGrid();
        Row20.Visible = true;

    }

    protected void GridData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            ShowReport("Pass Order", "Str_passorder_report.rpt", e.CommandArgument.ToString());
        }
    }


    private void ShowReport(string reportTitle, string rptFileName, string PassOrderNo)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;
            string[] database = _CCMS.Split('=');
            string databaseName = database[4].ToString();
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            url += "&param=@P_PASSORD_TRNO=" + PassOrderNo + ",@P_DATABASE=" + databaseName;


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pas order -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string LedgerName = string.Empty;
        string[] database = _CCMS.Split('=');
        string databaseName = database[4].ToString();
        int retVal = 0;
        if (rdbUnPaid.Checked == true)
        {
            for (int i = 0; i < GridData.Rows.Count; i++)
            {

                HiddenField hdnPASSORD_TRNO = GridData.Rows[i].FindControl("hdnPASSORD_TRNO") as HiddenField;
                TextBox txtVoucherNo = GridData.Rows[i].FindControl("txtVoucherNo") as TextBox;
                if (txtVoucherNo.Text != string.Empty)
                {
                    retVal = objSIC.setPaymentStatus(Convert.ToInt32(hdnPASSORD_TRNO.Value), txtVoucherNo.Text, databaseName);
                    if (retVal == 1)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Passorders is paid", this.Page);
                    }
                }


            }
        }
        else
        {
            for (int i = 0; i < gridDataPaid.Rows.Count; i++)
            {

                HiddenField hdnPASSORD_TRNO = gridDataPaid.Rows[i].FindControl("hdnPASSORD_TRNO") as HiddenField;
                TextBox txtVoucherNo = gridDataPaid.Rows[i].FindControl("txtVoucherNo") as TextBox;
                if (txtVoucherNo.Text != string.Empty)
                {
                    retVal = objSIC.setPaymentStatus(Convert.ToInt32(hdnPASSORD_TRNO.Value), txtVoucherNo.Text, databaseName);
                    if (retVal == 1)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Passorders is paid", this.Page);
                    }
                }


            }
        }
        fillGrid();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        GridData.DataSource = null;
        GridData.DataBind();
        gridDataPaid.DataSource = null;
        gridDataPaid.DataBind();
        Row20.Visible = false;
    }



    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string IsValid(string VoucherNo, string amount, string compcode,string state)
    {
        storeIntegrationController objSIC = new storeIntegrationController();
        string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
        Common objCommon = new Common();
        double Amount1 = 0.00;
        string status = "N";
        if (VoucherNo != string.Empty)
        {
            status = "Y";
            string[] VchNo = VoucherNo.ToString().Trim().Split(',');
            for (int i = 0; i < VchNo.Length; i++)
            {
                DataSet dsVCHNO = objSIC.getVocherNo(_CCMS);
                if (state == "Y")
                {
                    for (int j = 0; j < dsVCHNO.Tables[0].Rows.Count; j++)
                    {
                        if (Convert.ToString(dsVCHNO.Tables[0].Rows[j][0]).Contains(",") == true)
                        {
                            string[] POVCHNO = Convert.ToString(dsVCHNO.Tables[0].Rows[j][0]).Split(',');
                            for (int k = 0; k <= POVCHNO.Length; k++)
                            {
                                if (VchNo[i].ToString() == POVCHNO[k].ToString())
                                {
                                    return "Voucher no is already used";
                                }
                            }
                        }
                        else
                        {
                            if (VchNo[i].ToString() == Convert.ToString(dsVCHNO.Tables[0].Rows[j][0]))
                            {
                                return "Voucher no is already used";
                            }
                        }
                    }
                }
                string amt = objCommon.LookUp("acc_" + compcode + "_trans", "AMOUNT", "TRANSACTION_TYPE='P' and VOUCHER_NO=" + VchNo[i] + " and SUBTR_NO<>0");
                if (amt != string.Empty)
                    Amount1 = Amount1 + Convert.ToDouble(amt);
            }
        }

        if (Convert.ToDouble(amount) == Amount1 && status == "Y")
        {
            return "Valid";
        }
        else if (status == "Y")
        {
            return "Either voucher no is Invalid or Total Amount is not equal to Payment amount";
        }
        else
            return "0";
    }
}
