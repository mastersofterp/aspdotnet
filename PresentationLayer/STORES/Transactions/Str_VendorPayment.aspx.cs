
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

public partial class STORES_Transactions_Str_VendorPayment : System.Web.UI.Page
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
        if (ds != null)
        {
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
            //ddlAccNum.DataSource = dsFill.Tables[0];
            //ddlAccNum.DataValueField = "SPBD_ID";
            //ddlAccNum.DataTextField = "BANK_ACC_NO";
            //ddlAccNum.DataBind();
        }
        FillMultiSelectDropDown();
    }

    private void FillMultiSelectDropDown()
    {
        ddlPO.Items.Clear();
        DataSet ds = objVpCon.GetPODropdown();
        if (ds != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
        }
        ddlGRNNumber.Items.Clear();
        //DataSet dsgrn = objCommon.FillDropDown("STORE_GRN_MAIN", "GRNID", "GRN_NUMBER", "", "GRN_NUMBER");
        DataSet dsgrn = objVpCon.GetGRNDropdown();
        for (int i = 0; i < dsgrn.Tables[0].Rows.Count; i++)
        {
            ddlGRNNumber.Items.Add(new ListItem(Convert.ToString(dsgrn.Tables[0].Rows[i][1]), Convert.ToString(dsgrn.Tables[0].Rows[i][0])));
        }
        ddlInvoice.Items.Clear();
        //DataSet dsInv = objCommon.FillDropDown("STORE_INVOICE", "INVTRNO", "INVNO", "", "INVNO");
        DataSet dsInv = objVpCon.GetInvoiceDropdown();
        for (int i = 0; i < dsInv.Tables[0].Rows.Count; i++)
        {
            ddlInvoice.Items.Add(new ListItem(Convert.ToString(dsInv.Tables[0].Rows[i][1]), Convert.ToString(dsInv.Tables[0].Rows[i][0])));
        }
    }
    private void FillMultiSelectDropDownForEdit(int VpId)
    {
        ddlPO.Items.Clear();
        DataSet ds = objVpCon.GetMultiSelectDropDownForEdit(VpId);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
        }

        ddlGRNNumber.Items.Clear();       
        if (ds != null && ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                ddlGRNNumber.Items.Add(new ListItem(Convert.ToString(ds.Tables[1].Rows[i][1]), Convert.ToString(ds.Tables[1].Rows[i][0])));
            }
        }

        ddlInvoice.Items.Clear();
        if (ds != null && ds.Tables[2].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                ddlInvoice.Items.Add(new ListItem(Convert.ToString(ds.Tables[2].Rows[i][1]), Convert.ToString(ds.Tables[2].Rows[i][0])));
            }
        }
    }
    protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        string PoNumbers = GetPONumbers();
        DataSet ds = objVpCon.GetVPDetailsByPO(PoNumbers);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvVendorPay.DataSource = ds.Tables[0];
            lvVendorPay.DataBind();
            hdnRowCount.Value = ds.Tables[0].Rows.Count.ToString();
            divList.Visible = true;
            polist.Visible = true;
            grnlist.Visible = false;
            invlist.Visible = false;
            thStockDate.Text = "PO Date";
            thStockNum.Text = "PO Number";
            thStockAmt.Text = "PO Amount";
        }
        else
        {
            lvVendorPay.DataSource = null;
            lvVendorPay.DataBind();
            divList.Visible = false;

            thStockDate.Text = "";
            thStockNum.Text = "";
            thStockAmt.Text = "";
        }
        txtPaymentAmt.Text = "0.0";

    }
    private string GetPONumbers()
    {
        string PONumber = "";
        string PoValues = string.Empty;
        int count = 0;
        //  degreeNo = hdndegreeno.Value;
        //pnlFeeTable.Update();                          03/11/2021
        foreach (ListItem item in ddlPO.Items)
        {
            if (item.Selected == true)
            {
                PONumber += item.Value + ',';
                count = 1;
            }
        }
        if (count > 0)
        {
            PoValues = PONumber.Substring(0, PONumber.Length - 1);
            if (PoValues != "")
            {
                string[] degValue = PoValues.Split(',');

            }
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return PoValues;

    }
    protected void ddlGRNNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GRNNumbers = GetGRNNumbers();
        DataSet ds = objVpCon.GetVPDetailsByGrnId(GRNNumbers);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvVendorPay.DataSource = ds.Tables[0];
            lvVendorPay.DataBind();
            hdnRowCount.Value = ds.Tables[0].Rows.Count.ToString();
            grnlist.Visible = true;
            polist.Visible = false;
            invlist.Visible = false;
            divList.Visible = true;
            thStockDate.Text = "GRN Date";
            thStockNum.Text = "GRN Number";
            thStockAmt.Text = "GRN Amount";
        }
        else
        {
            lvVendorPay.DataSource = null;
            lvVendorPay.DataBind();
            divList.Visible = false;

            thStockDate.Text = "";
            thStockNum.Text = "";
            thStockAmt.Text = "";
        }
    }
    private string GetGRNNumbers()
    {
        string GRNNumber = "";
        string GRNValues = string.Empty;
        int count = 0;
        //pnlFeeTable.Update();                    03/11/2021
        foreach (ListItem item in ddlGRNNumber.Items)
        {
            if (item.Selected == true)
            {
                GRNNumber += item.Value + ',';
                count = 1;
            }
        }
        if (count > 0)
        {
            GRNValues = GRNNumber.Substring(0, GRNNumber.Length - 1);
            if (GRNValues != "")
            {
                string[] degValue = GRNValues.Split(',');

            }
        }
        return GRNValues;

    }
    protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        string InvoiceNumbers = GetInvoiceNumbers();
        DataSet ds = objVpCon.GetVPDetailsByInvtrno(InvoiceNumbers);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvVendorPay.DataSource = ds.Tables[0];
            lvVendorPay.DataBind();
            hdnRowCount.Value = ds.Tables[0].Rows.Count.ToString();
            divList.Visible = true;
            invlist.Visible = true;
            polist.Visible = false;
            grnlist.Visible = false;
            thStockDate.Text = "Invoice Date";
            thStockNum.Text = "Invoice Number";
            thStockAmt.Text = "Invoice Amount";
        }
        else
        {
            lvVendorPay.DataSource = null;
            lvVendorPay.DataBind();
            divList.Visible = false;
        }

    }

    private string GetInvoiceNumbers()
    {
        string InvNumbers = string.Empty;
        int Count = 0;
        foreach (ListItem item in ddlInvoice.Items)
        {
            if (item.Selected == true)
            {
                InvNumbers += item.Value + ',';
                Count++;
            }
        }
        if (Count > 0)
            InvNumbers = InvNumbers.Substring(0, InvNumbers.Length - 1);

        return InvNumbers;
    }
    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearDropDown();

        if (ddlPaymentType.SelectedValue == "1")
        {
            divPO.Visible = true;
            divGRN.Visible = false;
            divInvoice.Visible = false;

        }
        else if (ddlPaymentType.SelectedValue == "2")
        {
            divPO.Visible = false;
            divGRN.Visible = true;
            divInvoice.Visible = false;
        }
        else if (ddlPaymentType.SelectedValue == "3")
        {
            divPO.Visible = false;
            divGRN.Visible = false;
            divInvoice.Visible = true;
        }
        else
        {
            divPO.Visible = false;
            divGRN.Visible = false;
            divInvoice.Visible = false;
        }
    }

    private void ClearDropDown()
    {
        ddlPO.Items.Clear();
        DataSet ds = objVpCon.GetPODropdown();
        //DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A'", "PORDNO");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }

        ddlGRNNumber.Items.Clear();
        DataSet dsgrn = objVpCon.GetGRNDropdown();
        //DataSet dsgrn = objCommon.FillDropDown("STORE_GRN_MAIN", "GRNID", "GRN_NUMBER", "", "GRN_NUMBER");
        if (dsgrn != null)
        {
            for (int i = 0; i < dsgrn.Tables[0].Rows.Count; i++)
            {
                ddlGRNNumber.Items.Add(new ListItem(Convert.ToString(dsgrn.Tables[0].Rows[i][1]), Convert.ToString(dsgrn.Tables[0].Rows[i][0])));
            }
        }

        ddlInvoice.Items.Clear();
        DataSet dsInv = objVpCon.GetInvoiceDropdown();
        if (dsInv != null)
        {
            // DataSet dsInv = objCommon.FillDropDown("STORE_INVOICE", "INVTRNO", "INVNO", "", "INVNO");
            for (int i = 0; i < dsInv.Tables[0].Rows.Count; i++)
            {
                ddlInvoice.Items.Add(new ListItem(Convert.ToString(dsInv.Tables[0].Rows[i][1]), Convert.ToString(dsInv.Tables[0].Rows[i][0])));
            }
        }
        lvVendorPay.DataSource = null;
        lvVendorPay.DataBind();
        divList.Visible = false;
        txtPaymentAmt.Text = string.Empty;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string PONumbers = GetPONumbers();
        string GRNNumbers = GetGRNNumbers();
        string InvoiceNumbers = GetInvoiceNumbers();


        objVpEnt.VPDATE = Convert.ToDateTime(txtVPDate.Text);
        objVpEnt.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
        objVpEnt.PAYMENT_TYPE = Convert.ToInt32(ddlPaymentType.SelectedValue);
        objVpEnt.PORDNO = PONumbers;
        objVpEnt.GRNID = GRNNumbers;
        objVpEnt.INVTRNO = InvoiceNumbers;
        objVpEnt.PAYMENT_AMOUNT = txtPaymentAmt.Text == "" ? 0.0 : Convert.ToDouble(txtPaymentAmt.Text);
        objVpEnt.MODE_OF_PAY = Convert.ToInt32(ddlModeOfPay.SelectedValue);
        objVpEnt.BANK_ID = Convert.ToInt32(ddlBank.SelectedValue); ;
        objVpEnt.BRANCH_ID = Convert.ToInt32(ddlBranch.SelectedValue);
        objVpEnt.IFSC_ID = Convert.ToInt32(ddlIfscCode.SelectedValue);
        if (ddlAccNum.SelectedIndex > 0)
        {
            objVpEnt.BANKACCNO_ID = Convert.ToInt32(ddlAccNum.SelectedValue);
        }
        else
        {
            objVpEnt.BANKACCNO_ID = 0;
        }
        objVpEnt.PAYEE_NAME = txtPayeeName.Text;
        objVpEnt.REMARK = txtRemark.Text;
        objVpEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
        objVpEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);

        objVpEnt.VENDOR_PAYMENT_TABLE = this.AddPayTable();


        if (ViewState["Action"].ToString() == "Add")
        {
            objVpEnt.VP_NUMBER = GenerateVPNumber();
            objVpEnt.VPID = 0;

            CustomStatus cs = (CustomStatus)objVpCon.InsUpdateVendorPayment(objVpEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved & VP Number Generated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        else
        {
            objVpEnt.VP_NUMBER = txtVPNumber.Text;
            objVpEnt.VPID = Convert.ToInt32(ViewState["VPID"]);
            CustomStatus cs = (CustomStatus)objVpCon.InsUpdateVendorPayment(objVpEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Updated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
        }
        //ClearAll();
        divVPNumber.Visible = true;
        btnSubmit.Enabled = false;

    }

    private string GenerateVPNumber()
    {
        DataSet ds = objVpCon.GetVPNumber();
        txtVPNumber.Text = ds.Tables[0].Rows[0]["VP_NUMBER"].ToString();
        return txtVPNumber.Text;
    }

    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    if ((bool)Session["buttonAddNewPressed"] == false)
    //    {
    //        pnlProcesses.Controls.RemoveAt(pnlProcesses.Controls.Count - 1);
    //        Session["countDDL"] = int.Parse(Session["countDDL"].ToString()) - 1;
    //    }

    //}

    private DataTable AddPayTable()
    {
        DataTable dtPay = this.CreatePayTable();
        int MaxSrnoVal = 0;
        foreach (ListViewItem lv in lvVendorPay.Items)
        {
            MaxSrnoVal++;
            Label lblStockDate = lv.FindControl("lblStockDate") as Label;
            HiddenField hdnStockId = lv.FindControl("hdnStockId") as HiddenField;
            Label lblBillAmount = lv.FindControl("lblBillAmount") as Label;
            Label lblTotalPaidAmt = lv.FindControl("lblTotalPaidAmt") as Label;
            TextBox lblBalanceAmt = lv.FindControl("lblBalanceAmt") as TextBox;
            //HiddenField hdnBalanceAmt = lv.FindControl("hdnBalanceAmt") as HiddenField;
            TextBox txtPayNowAmt = lv.FindControl("txtPayNowAmt") as TextBox;
            TextBox txtPayRemark = lv.FindControl("txtPayRemark") as TextBox;

            //decimal payamt= Convert.ToDecimal(txtPayNowAmt);
            if (Convert.ToDouble(txtPayNowAmt.Text) == 0 || Convert.ToDouble(txtPayNowAmt.Text) == null)    // Modified by shabina 04-09-2021
            {

                MessageBox("Please enter amount in pay now.");
                //return;
            }
            //if (Convert.ToDouble(lblBillAmount.Text) < Convert.ToDouble(txtPayNowAmt.Text))
            //{

            //    MessageBox("Payment Amount is Incorrect.");
            //}

            DataRow dr = null;
            dr = dtPay.NewRow();
            dr["SRNO"] = MaxSrnoVal;
            dr["BILL_AMT"] = Convert.ToDouble(lblBillAmount.Text);
            dr["TOTAL_PAID_AMT"] = Convert.ToDouble(lblTotalPaidAmt.Text) + Convert.ToDouble(txtPayNowAmt.Text);
            dr["BALANCE_AMT"] = Convert.ToDouble(lblBalanceAmt.Text);
            dr["PAY_NOW_AMT"] = Convert.ToDouble(txtPayNowAmt.Text);
            dr["PAY_REMARK"] = txtPayRemark.Text;
            if (ddlPO.SelectedValue != "")
                dr["PORDNO"] = Convert.ToInt32(hdnStockId.Value);
            else
                dr["PORDNO"] = 0;

            if (ddlGRNNumber.SelectedValue != "")
                dr["GRNID"] = Convert.ToInt32(hdnStockId.Value);
            else
                dr["GRNID"] = 0;
            if (ddlInvoice.SelectedValue != "")
                dr["INVTRNO"] = Convert.ToInt32(hdnStockId.Value);
            else
                dr["INVTRNO"] = 0;

            dtPay.Rows.Add(dr);
        }
        return dtPay;
    }

    private DataTable CreatePayTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("BILL_AMT", typeof(double)));
        dt.Columns.Add(new DataColumn("TOTAL_PAID_AMT", typeof(double)));
        dt.Columns.Add(new DataColumn("BALANCE_AMT", typeof(double)));
        dt.Columns.Add(new DataColumn("PAY_NOW_AMT", typeof(double)));
        dt.Columns.Add(new DataColumn("PAY_REMARK", typeof(string)));
        dt.Columns.Add(new DataColumn("PORDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("GRNID", typeof(int)));
        dt.Columns.Add(new DataColumn("INVTRNO", typeof(int)));
        return dt;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ClearAll();
        ImageButton btnEdit = sender as ImageButton;
        int VpId = Convert.ToInt32(btnEdit.CommandArgument);

        ViewState["VPID"] = VpId;
        ViewState["Action"] = "Edit";
        FillMultiSelectDropDownForEdit(VpId);
        DataSet dsEdit = objVpCon.GetVPDetailsById(VpId);

        string s = dsEdit.Tables[0].Rows[0]["VP_STATUS"].ToString();
        if (s == "A")
        {
            //  objCommon.DisplayMessage("Approved Booking can not be modify", this.Page);
            MessageBox("Approved Record Can Not Be Modify.");
            lvVPEntry.Visible = true;
            btnAddN.Visible = true;
            return;
        }
        else if (s == "R")
        {
            MessageBox("Rejected Record Can Not Be Modify.");
            lvVPEntry.Visible = true;
            btnAddN.Visible = true;
            return;
        }


        txtVPNumber.Text = dsEdit.Tables[0].Rows[0]["VP_NUMBER"].ToString();
        divVPNumber.Visible = true;
        txtVPDate.Text = dsEdit.Tables[0].Rows[0]["VPDATE"].ToString();
        ddlVendor.SelectedValue = dsEdit.Tables[0].Rows[0]["PNO"].ToString();
        ddlVendor_SelectedIndexChanged(sender, e);
        ddlPaymentType.SelectedValue = dsEdit.Tables[0].Rows[0]["PAYMENT_TYPE"].ToString();

        if (dsEdit.Tables[0].Rows[0]["PORDNO"].ToString() != "0" && dsEdit.Tables[0].Rows[0]["PORDNO"].ToString() != "")
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
        else if (dsEdit.Tables[0].Rows[0]["GRNID"].ToString() != "0" && dsEdit.Tables[0].Rows[0]["GRNID"].ToString() != "")
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
            divInvoice.Visible = true;
            divPO.Visible = false;
            thStockDate.Text = "GRN Date";
            thStockNum.Text = "GRN Number";
            thStockAmt.Text = "GRN Amount";
            thStockDate.Text = "Invoice Date";
            thStockNum.Text = "Invoice Number";
            thStockAmt.Text = "Invoice Amount";
        }
        else if (dsEdit.Tables[0].Rows[0]["INVTRNO"].ToString() != "0" && dsEdit.Tables[0].Rows[0]["INVTRNO"].ToString() != "")
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
            divPO.Visible = false;
            divGRN.Visible = false;
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
        btnAddN.Visible = false;

    }

    private void ClearAll()
    {
        txtVPDate.Text = DateTime.Now.ToString();
        txtVPNumber.Text = string.Empty;
        ddlVendor.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        txtPaymentAmt.Text = string.Empty;
        ddlModeOfPay.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlIfscCode.SelectedIndex = 0;
        //ddlAccNum.SelectedIndex = 0;
        txtPayeeName.Text = string.Empty;
        txtRemark.Text = string.Empty;
        lvVendorPay.DataSource = null;
        lvVendorPay.DataBind();
        divList.Visible = false;
        btnSubmit.Enabled = true;

        divVPNumber.Visible = false;
        divPO.Visible = false;
        divGRN.Visible = false;
        divInvoice.Visible = false;

        ddlAccNum.Items.Clear();
        ddlAccNum.Items.Insert(0, new ListItem("Please Select","0"));
        FillMultiSelectDropDown();

        ViewState["Action"] = "Add";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        PnlVPEntry.Visible = false;
        lvVPEntry.Visible = true;
        btnAddN.Visible = true;
        BindListView();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        PnlVPEntry.Visible = true;
        lvVPEntry.Visible = false;
        btnAddN.Visible = false;
        polist.Visible = false;
        grnlist.Visible = false;
        invlist.Visible = false;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAccNum.Items.Clear();
        ddlAccNum.Items.Insert(0, new ListItem("Please Select", "0")); 

        DataSet dsFill = objCommon.FillDropDown("STORE_PARTY_BANK_DETAIL", "*", "", "PNO=" + Convert.ToInt32(ddlVendor.SelectedValue), "");
        if (dsFill.Tables[0].Rows.Count > 0)
        {


            ddlAccNum.DataSource = dsFill.Tables[0];
            ddlAccNum.DataValueField = "SPBD_ID";
            ddlAccNum.DataTextField = "BANK_ACC_NO";
            ddlAccNum.DataBind();
        }
        else
        {
            ddlAccNum.Items.Clear();
            ddlAccNum.Items.Insert(0, new ListItem("Please Select", "0"));            
        }
    }
    protected void ddlAccNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccNum.SelectedIndex > 0)
            ddlIfscCode.SelectedValue = ddlBranch.SelectedValue = ddlBank.SelectedValue = ddlAccNum.SelectedValue;
        else
            ddlIfscCode.SelectedIndex = ddlBranch.SelectedIndex = ddlBank.SelectedIndex = 0;
    }
    protected void ddlModeOfPay_SelectedIndexChanged(object sender, EventArgs e)            //---------------27/01/2023------------------//
    {
        if (ddlModeOfPay.SelectedValue == "1")
        {
           // divBankDetails.Visible = false;
            divBankAccNo.Visible = false;
            divBankName.Visible = false;
            divBranchName.Visible = false;
            divifsccode.Visible = false;
        }
        else
        {
          //  divBankDetails.Visible = true;
            divBankAccNo.Visible = true ;
            divBankName.Visible = true;
            divBranchName.Visible = true;
            divifsccode.Visible = true;
        }
    }
}