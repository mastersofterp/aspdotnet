//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 28-JULY-2014                                              
// CREATED BY    : NITIN MESHRAM

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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACCOUNT_Store_Amount_transfer : System.Web.UI.Page
{
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    storeIntegration objStore = new storeIntegration();
    storeIntegrationController objSIC = new storeIntegrationController();
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
            //btnTrans.Attributes.Add("onclick", "return confirm('Record Allread Present Are U sure want to Replace records?')");
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
                    

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    objCommon.FillDropDownList(ddlVendor, "acc_" + Session["comp_code"].ToString() + "_store_mapping", "PNO", "PNAME", "", "");
                    SetFinancialYear();
                    btnTrans.Enabled = false;
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
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        objStore.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
        DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
        objStore.FromDate = fromDate.ToString("dd-MMM-yyyy");
        DateTime toDate = Convert.ToDateTime(txtTodate.Text);
        objStore.ToDate = toDate.ToString("dd-MMM-yyyy");

        DataSet dsInv = objSIC.getInvoiceNo(_CCMS, objStore);
        if (dsInv.Tables[0].Rows.Count>0)
        {
            GridData.DataSource = dsInv;
            GridData.DataBind();
        }
        else
        {
            objCommon.DisplayUserMessage(UPDLedger, "Data Not Found", this.Page);
            btnReset_Click(sender, e);
            return;
        }

        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            Label lblLedgerheadName = GridData.Rows[i].FindControl("lblLedgerheadName") as Label;
            Label lblExpence = GridData.Rows[i].FindControl("lblExpence") as Label;
            HiddenField hdnINVTRNO=GridData.Rows[i].FindControl("hdnINVTRNO") as HiddenField;
            lblLedgerheadName.Text = objCommon.LookUp("acc_" + Session["comp_code"] + "_store_mapping ASM inner join acc_" + Session["comp_code"] + "_party AP on(ASM.party_no=ap.party_no)", "AP.Party_name", "ASM.PNO=" + objStore.PNO);
            lblExpence.Text = objCommon.LookUp("acc_" + Session["comp_code"] + "_store_mapping ASM inner join acc_" + Session["comp_code"] + "_party AP on(ASM.party_no_expe=ap.party_no)", "AP.Party_name", "ASM.PNO=" + objStore.PNO);
        }
        btnTrans.Enabled = true;
    }

    protected void btnTrans_Click(object sender, EventArgs e)
    {
        int retVal = 0;
        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            Label lblNetAmt = GridData.Rows[i].FindControl("lblNetAmt") as Label;
            HiddenField hdnINVTRNO = GridData.Rows[i].FindControl("hdnINVTRNO") as HiddenField;
            objStore.Net_Amount = lblNetAmt.Text;
            objStore.INVTRNO = Convert.ToInt32(hdnINVTRNO.Value);
            objStore.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
            string[] database = _CCMS.Split('=');
            string databaseName = database[4].ToString();
            objStore.DATABASE = databaseName;
            objStore.College_code = Session["colcode"].ToString();
            objStore.UA_NO =Convert.ToInt32(Session["userno"].ToString());
            retVal = objSIC.MakeJVEntry(objStore, Session["comp_code"].ToString());
            if (retVal == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Amount Transfer Successfully", this.Page);
            }
        }
        btnReset_Click(sender, e);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        GridData.DataSource = null;
        GridData.DataBind();
        ddlVendor.SelectedValue = "0";
        btnTrans.Enabled = false;
    }
}
