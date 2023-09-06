//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : LEDGER HEAD                                                     
// CREATION DATE : 10-AUGUST-2016                                               
// CREATED BY    : NAKUL A. CHAWRE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class ACCOUNT_IsolatedChequePrint : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenter objCostCenter = new CostCenter();
    CostCenterController objCostCenterController = new CostCenterController();
    DataTable dtCheque = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountMast")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");
            }
            else
            {
                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

        if (Session["comp_code"] != null)
        {
            //ChangeThePageLayout();
        }

        if (!Page.IsPostBack)
        {
            txtLedgerName.Focus();
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                //  Response.Redirect("~/default.aspx");
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
                    ViewState["action"] = "add";
                }
            }
            hfHiddenFieldID.Value = "0";
            txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            getNames();
        }
    }

    #region Private Methods

    private void Clear()
    {
        txtAmount.Text = string.Empty;
        txtCHQNo.Text = string.Empty;
        txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtLedgerName.SelectedValue = "0";
        txtAccount.SelectedValue = "0";
        txtCHQNo.Enabled = true;
        IsAccountPayee.Checked = false;
    }

    private void GetChequeNo()
    {
        if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "count(*)", "'" + txtCHQNo.Text + "' BETWEEN CFRNO AND CTONO")) < 0)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please enter", this);
            return;
        }
    }

    private void getNames()
    {
        objCommon.FillDropDownList(txtAccount, "Acc_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "ACCNAME", "", "ACCNAME");
        objCommon.FillDropDownList(txtLedgerName, "Acc_" + Session["comp_code"].ToString() + "_PAYEE", "IDNO", "PARTYNAME", "", "PARTYNAME");
    }


    #endregion Private Methods

    protected void txtLedgerName_TextChanged(object sender, EventArgs e)
    {
        int retVal = 0;
        string PayeeName = txtLedgerName.SelectedItem.Text;

        if (txtLedgerName.Text.Split('*')[0].Length > 1)
        {
            if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PAYEE", "count(*)", "PARTYNAME='" + txtLedgerName.Text + "'")) == 0)
            {
                int IDNO = 0;
                string PARTYNAME = txtLedgerName.Text;
                retVal = objCostCenterController.AddPayee(IDNO, PARTYNAME, Session["comp_code"].ToString());
                txtLedgerName.Text = retVal + "*" + PARTYNAME;
            }
        }
        txtLedgerName.Text = txtLedgerName.Text;
    }

    protected void txtAccount_TextChanged(object sender, EventArgs e)
    {
        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "[STATUS]", "TRNO='" + txtAccount.Text.Split('*')[0] + "'") == "0")
        {
            txtCHQNo.Enabled = false;
            string bankno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC b", "b.BNO", "TRNO='" + txtAccount.SelectedValue + "'");
            int Count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "count(*)", "BANKNO='" + bankno + "'"));
            string ChqNo1 = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CHECK_PRINT C INNER JOIN ACC_" + Session["comp_code"] + "_BANKAC B ON (C.BANKNO = B.BNO)", "TOP 1 CAST(C.CHECKNO AS INT) + 1 AS CHQNO ", "B.BNO='" + bankno + "' ORDER BY CHQNO DESC").ToString();
            int ChqNo = 0;
            if (ChqNo1 == string.Empty || ChqNo1 == null)
                ChqNo = 1;
            else
                ChqNo = Convert.ToInt32(ChqNo1);
            if (gvChqDetails.Rows.Count > 0)
            {
                txtCHQNo.Text = ViewState["ChqNo"].ToString();
            }
            else if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "CAST(CTONO AS INT)", "TRNO='" + txtAccount.SelectedValue + "'")) < ChqNo)
            {
                txtCHQNo.Text = objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "CAST(CCURNO AS INT) + 1", "TRNO='" + txtAccount.SelectedValue + "'");
            }
            else if (Count > 0)
            {
                txtCHQNo.Text = ChqNo.ToString();//objCommon.LookUp("ACC_" + Session["comp_code"] + "_CHECK_PRINT C INNER JOIN ACC_" + Session["comp_code"] + "_BANKAC B ON (C.BANKNO = B.BNO)", "CAST(C.CHECKNO AS INT) + 1", "B.BNO='" + bankno + "'");
            }
            else
                txtCHQNo.Text = objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "CAST(CCURNO AS INT) + 1", "TRNO='" + txtAccount.SelectedValue + "'");

            //if (gvChqDetails.Rows.Count > 0)
            //{
            //    txtCHQNo.Text = ViewState["ChqNo"].ToString();
            //}

            if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "CAST(CTONO AS INT)", "TRNO='" + txtAccount.SelectedValue + "'")) < Convert.ToInt32(txtCHQNo.Text))
            {
                objCommon.DisplayUserMessage(UPDLedger, "Cheque no not Valid, Please Change Cheque Book.", this);
                return;
            }
        }
        else
        {
            txtCHQNo.Enabled = true;
            txtCHQNo.Text = string.Empty;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dtCheque = new DataTable();
            if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_BANKAC", "CAST(CTONO AS INT)", "TRNO='" + txtAccount.SelectedValue + "'")) < Convert.ToInt32(txtCHQNo.Text))
            {
                objCommon.DisplayUserMessage(UPDLedger, "Cheque no not Valid, Please Change Cheque Book.", this);
                return;
            }

            if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_CHECK_PRINT", "COUNT(*)", "CHECKNO='" + txtCHQNo.Text + "'")) >= 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Cheque no Already exist, please enter different Cheque no", this);
                txtCHQNo.Text = string.Empty;
                txtCHQNo.Focus();
                return;
            }

            else
            {
                if (Session["dtCheque"] != null)
                    dtCheque = (DataTable)Session["dtCheque"];

                if (!(dtCheque.Columns.Contains("AccId")))
                    dtCheque.Columns.Add("AccId");
                if (!(dtCheque.Columns.Contains("AccName")))
                    dtCheque.Columns.Add("AccName");
                if (!(dtCheque.Columns.Contains("PayeeId")))
                    dtCheque.Columns.Add("PayeeId");
                if (!(dtCheque.Columns.Contains("PayeeName")))
                    dtCheque.Columns.Add("PayeeName");
                if (!(dtCheque.Columns.Contains("ChequeNo")))
                    dtCheque.Columns.Add("ChequeNo");
                if (!(dtCheque.Columns.Contains("ChequeDate")))
                    dtCheque.Columns.Add("ChequeDate");
                if (!(dtCheque.Columns.Contains("Amount")))
                    dtCheque.Columns.Add("Amount");
                if (!(dtCheque.Columns.Contains("AmountinWrd")))
                    dtCheque.Columns.Add("AmountinWrd");
                if (!(dtCheque.Columns.Contains("Status")))
                    dtCheque.Columns.Add("Status");
                if (!(dtCheque.Columns.Contains("BankNo")))
                    dtCheque.Columns.Add("BankNo");
                //Adde by vijay andoju 15-07-2020
                if (!(dtCheque.Columns.Contains("ACCNO")))
                    dtCheque.Columns.Add("ACCNO");

                DataRow drCheque = dtCheque.NewRow();

                drCheque["AccId"] = txtAccount.SelectedValue;
                drCheque["AccName"] = txtAccount.SelectedItem.Text;
                drCheque["PayeeId"] = txtLedgerName.SelectedValue;
                drCheque["PayeeName"] = txtLedgerName.SelectedItem.Text;
                drCheque["ChequeNo"] = txtCHQNo.Text;
                ViewState["ChqNo"] = Convert.ToInt32(txtCHQNo.Text) + 1;
                drCheque["ChequeDate"] = Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy");
                drCheque["Amount"] = txtAmount.Text;
                NumberWords nw = new NumberWords();
                drCheque["AmountinWrd"] = nw.changeToWords(txtAmount.Text.ToString(), true);
                drCheque["BankNo"] = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC b", "b.BNO", "TRNO='" + txtAccount.SelectedValue + "'");
               
                //Added by vijay on 16-07-2020 to get the account number
                drCheque["ACCNO"] = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC b", "b.ACCNO", "TRNO='" + txtAccount.SelectedValue + "'");
                if (IsAccountPayee.Checked == true)
                    drCheque["Status"] = "AcPayee";
                else
                    drCheque["Status"] = "Bearer";

                dtCheque.Rows.Add(drCheque);
                gvChqDetails.DataSource = dtCheque;
                gvChqDetails.DataBind();
                Session["dtCheque"] = dtCheque;
                Clear();
                btnSave.Visible = true;
                btncancel2.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
            objCommon.DisplayMessage("Error Occured! Record Not Saved", this.Page);
        }
    }

    protected void gvChqDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Session["dtCheque"] != null)
            dtCheque = (DataTable)Session["dtCheque"];

        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lblAmount = gvChqDetails.Rows[index].FindControl("lblAmt") as Label;
        Label lblDate = gvChqDetails.Rows[index].FindControl("lblDate") as Label;
        Label lblChqNo = gvChqDetails.Rows[index].FindControl("lblChqNo") as Label;
        Label lblParty = gvChqDetails.Rows[index].FindControl("lblParty") as Label;
        Label lblAccName = gvChqDetails.Rows[index].FindControl("lblAccName") as Label;
        Label lblStatus = gvChqDetails.Rows[index].FindControl("lblStatus") as Label;
        HiddenField hfAccNo = gvChqDetails.Rows[index].FindControl("hfAccNo") as HiddenField;
        HiddenField hfPartyNo = gvChqDetails.Rows[index].FindControl("hfPartyNo") as HiddenField;

        txtAmount.Text = lblAmount.Text;
        txtCHQNo.Text = lblChqNo.Text;
        txtDate.Text = Convert.ToDateTime(lblDate.Text).ToString("dd/MM/yyyy");
        txtLedgerName.SelectedValue = hfPartyNo.Value;
        txtAccount.SelectedValue = hfAccNo.Value;
        if (lblStatus.Text == "AcPayee")
            IsAccountPayee.Checked = true;
        else
            IsAccountPayee.Checked = false;
        ViewState["ChkEdit"] = "Y";
        ViewState["index"] = index.ToString();

        DataView dvItems = dtCheque.DefaultView;
        dvItems.RowFilter = "ChequeNo<>" + lblChqNo.Text;

        dtCheque = dvItems.ToTable();
        Session["dtCheque"] = dtCheque;
        gvChqDetails.DataSource = dtCheque;
        gvChqDetails.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;

            ret = Convert.ToInt32(objCostCenterController.AddIsolatedCheque(Session["comp_code"].ToString(), Session["username"].ToString(), (DataTable)Session["dtCheque"]));
            if (ret == 1)
            {
                objCommon.DisplayMessage("Data Saved Successfully", this.Page);
                objCommon.DisplayUserMessage(UPDLedger, "Data Saved Successfully", this);
                upd_ModalPopupExtender1.Show();
                GvPrintDetails.DataSource = Session["dtCheque"];
                GvPrintDetails.DataBind();
                Session["dtCheque"] = null;
                gvChqDetails.DataSource = null;
                gvChqDetails.DataBind();
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
            objCommon.DisplayMessage("Error Occured! Record Not Saved", this.Page);
        }
    }

    protected void GvPrintDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        //index = index - 1;
        Label lblAmt = GvPrintDetails.Rows[index].FindControl("lblAmt") as Label;
        Label lblDate = GvPrintDetails.Rows[index].FindControl("lblDate") as Label;
        Label lblChqNo = GvPrintDetails.Rows[index].FindControl("lblChqNo") as Label;
        Label lblParty = GvPrintDetails.Rows[index].FindControl("lblParty") as Label;
        Label lblAccName = GvPrintDetails.Rows[index].FindControl("lblAccName") as Label;
        Label lblStatus = GvPrintDetails.Rows[index].FindControl("lblStatus") as Label;
        HiddenField hfAccNo = GvPrintDetails.Rows[index].FindControl("hfAccNo") as HiddenField;
        HiddenField hfPartyNo = GvPrintDetails.Rows[index].FindControl("hfPartyNo") as HiddenField;
        HiddenField hfAmtinwrd = GvPrintDetails.Rows[index].FindControl("hfAmtinwrd") as HiddenField;
        HiddenField hfAccnumber = GvPrintDetails.Rows[index].FindControl("hfAccnumber") as HiddenField;


        string bankno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC b", "b.BNO", "TRNO='" + hfAccNo.Value + "'");
        string CheckOrientation = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_CONFIG", "PARAMETER", "CONFIGDESC='CHEQUE ORIENTATION(TRUE-HORIZONTAL,FALSE-VERTICAL)'");
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
        string Script = string.Empty;
        string reportTitle = "ChequePrint";
        if (CheckOrientation == "N")
        {
            url += "Reports/Cheque_Vertical.aspx?";
        }
        else
        {
            url += "Reports/Cheque.aspx?";
        }
       // url += "obj=" + bankno + "," + lblChqNo.Text + "," + Convert.ToDateTime(lblDate.Text).ToString() + "," + hfPartyNo.Value + "*" + lblParty.Text + "," + lblAmt.Text + "," + lblAccName.Text + "," + 0 + "," + 0 + "," + false + "," + "0";

        //Added by vijay Andoju 16-07-2020
        url += "obj=" + bankno + "," + lblChqNo.Text + "," + Convert.ToDateTime(lblDate.Text).ToString() + "," + hfPartyNo.Value + "*" + lblParty.Text + "," + lblAmt.Text + "," + hfAccnumber.Value + "," + 0 + "," + 0 + "," + false + "," + "0";
        
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        upd_ModalPopupExtender1.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }


    protected void Cancel2_Click(object sender, EventArgs e)
    {

        Session["dtCheque"] = null;
        gvChqDetails.DataSource = null;
        gvChqDetails.DataBind();
        btnSave.Visible = false;
        btncancel2.Visible = false;
    }
}
