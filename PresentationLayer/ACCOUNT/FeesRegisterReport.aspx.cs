//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : RECEIPT PAYMENT REPORT                                                    
// CREATION DATE : 24-MAY-2010                                               
// CREATED BY    : Nitin Meshram                                                 
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

public partial class ACCOUNT_FeesRegisterReport : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CombinedCashBankBookController objCcbc;
    AccountTransactionController objTrans = new AccountTransactionController();
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
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //PopulateDropDown();
                    //PopulateListBox();
                    string IsCCMS = objCommon.LookUp("ACC_REF_CONFIG", "PARAMETER", "CONFIGDESC='IS CCMS'");
                    if (IsCCMS == "Y")
                    {
                        row18.Visible = false;

                        Session["IsCCMS"] = IsCCMS;
                        PopulateReceptTypeDropdown();
                    }
                    else
                    {
                        // Filling Degrees
                        PopulateDegreeDropdown();
                        Session["IsCCMS"] = IsCCMS;
                        //Filling Recept list
                        PopulateReceptTypeDropdown();
                        row18.Visible = true;


                    }
                    SetFinancialYear();
                }
            }  
        }
    }

    public void PopulateReceptTypeDropdown()
    {
        try
        {
            objCommon = new Common();
            DataSet ds = new DataSet();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                ds = objTrans.GetReceiptTypeForCCMS(_CCMS);
            }
            else
            {               
                ds = objTrans.PopulateReceiptType(_CCMS);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRecept.Items.Clear();
                ddlRecept.Items.Add("Please Select");
                ddlRecept.SelectedItem.Value = "0";
                ddlRecept.DataTextField = "RECIEPT_TITLE";
                ddlRecept.DataValueField = "RECIEPT_CODE";
                ddlRecept.DataSource = ds.Tables[0];
                ddlRecept.DataBind();
                ddlRecept.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.PopulateRecept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void PopulateDegreeDropdown()
    {
        try
        {
            objCommon = new Common();
           
            DataSet ds = objTrans.PopulateDegreeFromRF(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataSource = ds.Tables[0]; ;
                    ddlDegree.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnRP_Click(object sender, EventArgs e)
    {
        try
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


            ShowFeesRegister("Fees Register", "FeesRegister.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeesRegisterReport.btnRP_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void ShowFeesRegister(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string LedgerName = string.Empty;
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if(rdoGenralFees.Checked==true)
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString().Trim().ToUpper() + ",@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_RECIEPT_TYPE=" + ddlRecept.SelectedValue + ",@P_Degree=" + ddlDegree.SelectedItem.Text + ",@P_RECIEPTNAME="+ddlRecept.SelectedItem.Text;            
            else
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString().Trim().ToUpper() + ",@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + ",@P_DEGREENO=0,@P_RECIEPT_TYPE=MF,@P_Degree=0,@P_RECIEPTNAME=Miscellaneous Fees";            
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeesRegisterReport.ShowFeesRegister -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdoGenralFees_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        row4.Visible = true;
        if (Session["IsCCMS"].ToString() != "Y")
        {
            row18.Visible = true;
        }
    }

    protected void rdoMiscFees_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        row18.Visible = false;
        row4.Visible = false;
    }

    private void ClearAll()
    {
        if (Session["IsCCMS"].ToString() == "Y")
        {
            row18.Visible = false;

        }
        else
        {
            row18.Visible = true;
            row4.Visible = true;
        }      
        
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ddlRecept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        row4.Visible = true;
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
