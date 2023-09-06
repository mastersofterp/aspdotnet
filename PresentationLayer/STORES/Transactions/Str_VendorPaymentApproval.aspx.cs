
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_VendorPayment.aspx                                       
// CREATION DATE : 27-June-2021                                                 
// CREATED BY    : GOPAL ANTHATI                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class STORES_Transactions_Str_VendorPaymentApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_VendorPaymentEnt objVpEnt = new Str_VendorPaymentEnt();
    Str_VendorPaymentCon objVpCon = new Str_VendorPaymentCon();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "Add";


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {

                }
            }
            FillDropDownList();
            ViewState["Action"] = "Add";
            txtVPDate.Text = DateTime.Now.ToString();
            BindListView();
        }
        divMsg.InnerText = string.Empty;
    }

    private void BindListView()
    {
        DataSet ds = objVpCon.GetVPAllDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvVPEntry.DataSource = ds.Tables[0];
            lvVPEntry.DataBind();
            lvVPEntry.Visible = true;
        }
        else
        {
            lvVPEntry.DataSource = null;
            lvVPEntry.DataBind();
            lvVPEntry.Visible = false;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }
    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
        DataSet dsFill = objCommon.FillDropDown("STORE_PARTY_BANK_DETAIL", "*", "", "", "");
        if (dsFill.Tables[0].Rows.Count > 0)
        {
            //ddlBank.Items.Clear();
            ddlBank.DataSource = dsFill.Tables[0];
            ddlBank.DataValueField = "SPBD_ID";
            ddlBank.DataTextField = "BANK_NAME";
            ddlBank.DataBind();

            //ddlBranch.Items.Clear();
            ddlBranch.DataSource = dsFill.Tables[0];
            ddlBranch.DataValueField = "SPBD_ID";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();

            //ddlIfscCode.Items.Clear();
            ddlIfscCode.DataSource = dsFill.Tables[0];
            ddlIfscCode.DataValueField = "SPBD_ID";
            ddlIfscCode.DataTextField = "IFSC_CODE";
            ddlIfscCode.DataBind();

            //ddlAccNum.Items.Clear();
            ddlAccNum.DataSource = dsFill.Tables[0];
            ddlAccNum.DataValueField = "SPBD_ID";
            ddlAccNum.DataTextField = "BANK_ACC_NO";
            ddlAccNum.DataBind();
        }



        ddlPO.Items.Clear();
        DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A'", "PORDNO");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }

        ddlGRNNumber.Items.Clear();
        DataSet dsgrn = objCommon.FillDropDown("STORE_GRN_MAIN", "GRNID", "GRN_NUMBER", "", "GRN_NUMBER");
        for (int i = 0; i < dsgrn.Tables[0].Rows.Count; i++)
        {
            ddlGRNNumber.Items.Add(new ListItem(Convert.ToString(dsgrn.Tables[0].Rows[i][1]), Convert.ToString(dsgrn.Tables[0].Rows[i][0])));
        }

        ddlInvoice.Items.Clear();
        DataSet dsInv = objCommon.FillDropDown("STORE_INVOICE", "INVTRNO", "INVNO", "", "INVNO");
        for (int i = 0; i < dsInv.Tables[0].Rows.Count; i++)
        {
            ddlInvoice.Items.Add(new ListItem(Convert.ToString(dsInv.Tables[0].Rows[i][1]), Convert.ToString(dsInv.Tables[0].Rows[i][0])));
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button btnEdit = sender as Button;
        int VpId = Convert.ToInt32(btnEdit.CommandArgument);
        ViewState["VPID"] = VpId;
        ViewState["Action"] = "Edit";

        DataSet dsEdit = objVpCon.GetVPDetailsById(VpId);

        txtVPNumber.Text = dsEdit.Tables[0].Rows[0]["VP_NUMBER"].ToString();
        divVPNumber.Visible = true;
        txtVPDate.Text = dsEdit.Tables[0].Rows[0]["VPDATE"].ToString();
        ddlVendor.SelectedValue = dsEdit.Tables[0].Rows[0]["PNO"].ToString();
        ddlPaymentType.SelectedValue = dsEdit.Tables[0].Rows[0]["PAYMENT_TYPE"].ToString();

        //if (dsEdit.Tables[0].Rows[0]["PORDNO"].ToString() != "0" && dsEdit.Tables[0].Rows[0]["PORDNO"].ToString() != null)
        //{
        if (dsEdit.Tables[0].Rows[0]["PAYMENT_TYPE"].ToString() == "1")
        {
            string[] PoNumbers = dsEdit.Tables[0].Rows[0]["PORDNO"].ToString().Split(',');
            foreach (string Po in PoNumbers)
            {
                if (ddlPO.Items.FindByValue(Po) != null)
                {
                    ddlPO.Items.FindByValue(Po).Selected = true;
                }
            }
            divPO.Visible = true;
            divGRN.Visible = false;
            divInvoice.Visible = false;
            thStockDate.Text = "PO Date";
            thStockNum.Text = "PO Number";
            thStockAmt.Text = "PO Amount";
        }
        //else if (dsEdit.Tables[0].Rows[0]["GRNID"].ToString() != "0" && dsEdit.Tables[0].Rows[0]["GRNID"].ToString() != null)
        //{
        if (dsEdit.Tables[0].Rows[0]["PAYMENT_TYPE"].ToString() == "2")
        {
            string[] GRNNumbers = dsEdit.Tables[0].Rows[0]["GRNID"].ToString().Split(',');
            foreach (string Grn in GRNNumbers)
            {
                if (ddlGRNNumber.Items.FindByValue(Grn) != null)
                {
                    ddlGRNNumber.Items.FindByValue(Grn).Selected = true;
                }
            }


            divGRN.Visible = true;
            divPO.Visible = false;
            divInvoice.Visible = false;

            thStockDate.Text = "GRN Date";
            thStockNum.Text = "GRN Number";
            thStockAmt.Text = "GRN Amount";


        }
        //else if (dsEdit.Tables[0].Rows[0]["INVTRNO"].ToString() != "0" && dsEdit.Tables[0].Rows[0]["INVTRNO"].ToString() != null)
        //{
        if (dsEdit.Tables[0].Rows[0]["PAYMENT_TYPE"].ToString() == "3")
        {
            string[] InvoiceNumbers = dsEdit.Tables[0].Rows[0]["INVTRNO"].ToString().Split(',');
            foreach (string Inv in InvoiceNumbers)
            {
                if (ddlInvoice.Items.FindByValue(Inv) != null)
                {
                    ddlInvoice.Items.FindByValue(Inv).Selected = true;
                }
            }
            divInvoice.Visible = true;
       
            divGRN.Visible = false;
            divPO.Visible = false;


            thStockDate.Text = "Invoice Date";
            thStockNum.Text = "Invoice Number";
            thStockAmt.Text = "Invoice Amount";

        }
        txtPaymentAmt.Text = dsEdit.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString();
        ddlModeOfPay.SelectedValue = dsEdit.Tables[0].Rows[0]["MODE_OF_PAY"].ToString();
        ddlBank.SelectedValue = dsEdit.Tables[0].Rows[0]["BANK_ID"].ToString();
        ddlBranch.SelectedValue = dsEdit.Tables[0].Rows[0]["BRANCH_ID"].ToString();
        ddlIfscCode.SelectedValue = dsEdit.Tables[0].Rows[0]["IFSC_ID"].ToString();
        ddlAccNum.SelectedValue = dsEdit.Tables[0].Rows[0]["BANKACCNO_ID"].ToString();
        txtPayeeName.Text = dsEdit.Tables[0].Rows[0]["PAYEE_NAME"].ToString();
        txtRemark.Text = dsEdit.Tables[0].Rows[0]["REMARK"].ToString();

        if (dsEdit.Tables[1].Rows.Count > 0)
        {
            lvVendorPay.DataSource = dsEdit.Tables[1];
            lvVendorPay.DataBind();
            hdnRowCount.Value = dsEdit.Tables[1].Rows.Count.ToString();
            divList.Visible = true;
        }

        PnlVPEntry.Visible = true;
        lvVPEntry.Visible = false;
        pnlApprove.Visible = true;

        //rdlApprove.SelectedValue.
            rdlApprove.Items[0].Selected = false;
            rdlApprove.Items[1].Selected = false;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        BindListView();
        pnlApprove.Visible = false;
        PnlVPEntry.Visible = false;
        lvVPEntry.Visible = true;
       
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (rdlApprove.SelectedValue == "")
        {
            MessageBox("Please Select Approve Or Reject .");
            return;
        
        }
        CustomStatus cs = (CustomStatus)objVpCon.UpdateStatus(Convert.ToInt32(ViewState["VPID"]),Convert.ToChar(rdlApprove.SelectedValue));

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            if (rdlApprove.SelectedValue == "A")
                MessageBox("Vendor Payment Approved Successfully.");
            else
                MessageBox("Vendor Payment Rejected Successfully.");
        }
        else
        {
            MessageBox("Transaction Failed.");
        }
        // vishwas
        lvVPEntry.Visible = true;
        PnlVPEntry.Visible = false;
        pnlApprove.Visible = false;
        BindListView();
        
    }
}