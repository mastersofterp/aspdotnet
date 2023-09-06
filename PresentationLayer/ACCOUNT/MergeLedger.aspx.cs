//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                  
// CREATION DATE : 21 SEPT 2011                                              
// CREATED BY    : NARESH WARBHE                                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using IITMS.NITPRM;


public partial class Account_MergeLedger : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    int party_no = 0;
    string strLedgerToMerge = string.Empty;
    string strLedgerToBeMerge = string.Empty;
    string strLedgerToMerge1 = string.Empty;
    string strLedgerToBeMerge1 = string.Empty;
    int partyNoToMergeIndex = 0;
    int partyNoToBeMergeIndex = 0;
    int paymentTypeNo1 = 0;
    int paymentTypeNo2 = 0;
    int flag = 0;
    string deleteStatus = string.Empty;
    DataSet dsPartyToMerge = null;
    DataSet dsPartyToBeMerge = null;

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
        if (Session["comp_code"] != null)
        {

        }
        if (!Page.IsPostBack)
        {
            ViewState["id"] = "0";
            //Check Session
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
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    ViewState["action"] = "add";
                }
            }
            Session["WithoutCashBank"] = "Y";
            btnMerge.Enabled = false;
        }



    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Validations() == false)
        {
            btnMerge.Enabled = false;
            return;
        }
        else
        {

            GetIndex();
            if (GetPaymentTypeNo() == false)
            {
                txtAcc.Enabled = true;
                txtAcc1.Enabled = true;
                lvLedgerToMerge.DataSource = null;
                lvLedgerToBeMerge.DataSource = null;
                return;
            }

            BindTransaction(Convert.ToInt32(strLedgerToMerge), lvLedgerToMerge, pnlLedgerToMerge);
            BindTransaction(Convert.ToInt32(strLedgerToBeMerge), lvLedgerToBeMerge, pnlLedgerToBeMerge);

            pnlLedgerToMerge.Visible = true;
            pnlLedgerToBeMerge.Visible = true;
            btnMerge.Enabled = true;
            chkDeleteLedger.Enabled = true;
            txtAcc.Enabled = false;
            txtAcc1.Enabled = false;

            if (!IsTransactionExist())
            {
                btnMerge.Enabled = true;
                //chkDeleteLedger.Enabled = false;
                return;
            }
            if (txtAcc.Text != string.Empty && txtAcc1.Text != string.Empty)
            {
                chkDeleteLedger.Enabled = true;
            }
            else
            {
                chkDeleteLedger.Enabled = false;
            }
        }
    }
    protected void btnMerge_Click(object sender, EventArgs e)
    {
        try
        {
            Validations();
            GetIndex();
            GetPaymentTypeNo();
            if (chkDeleteLedger.Checked == true)
            {
                deleteStatus = "Y";
            }
            else if (chkDeleteLedger.Checked == false)
            {
                deleteStatus = "N";
            }
            AccountTransactionController objvch = new AccountTransactionController();
            CustomStatus cs = new CustomStatus();
            cs = (CustomStatus)objvch.MergeLedger(Session["comp_code"].ToString().Trim(), Convert.ToInt32(strLedgerToMerge), Convert.ToInt32(strLedgerToBeMerge), deleteStatus);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (chkDeleteLedger.Checked)
                {
                    GetIndex();
                    deleteStatus = "Y";

                    cs = (CustomStatus)objvch.DeleteMergeLedger(Session["comp_code"].ToString().Trim(), Convert.ToInt32(strLedgerToMerge), deleteStatus);
                    if (cs.Equals(CustomStatus.RecordDeleted))
                    {
                        objCommon.DisplayMessage(UPDLedger, "Ledgers Deleted and Merged successfully..! ", this);
                        clear();
                    }
                    else
                    {

                    }
                }
                else
                    objCommon.DisplayMessage(UPDLedger, "Ledgers successfully Merged. ", this);
                clear();
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Ledgers successfully Merged. ", this);
            }
           

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void chkDeleteLedger_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDeleteLedger.Checked == true)
        {
            if (btnMerge.Enabled == false)
            {

            }
        }
    }
    public bool Validations()
    {
        try
        {
            if ((txtAcc.Text == "" || txtAcc.Text == string.Empty) && (txtAcc1.Text == "" || txtAcc1.Text == string.Empty))
            {
                objCommon.DisplayMessage(UPDLedger, "Ledger To Merge  And Ledger To Be Merge Should Not be left Blank. ", this);
                txtAcc.Focus();
                return false;
            }
            if (txtAcc.Text == "" || txtAcc.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Ledger To Merge Should Not be left Blank. ", this);
                txtAcc.Focus();
                return false;
            }
            if (txtAcc1.Text == "" || txtAcc1.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Ledger To Be Merge Should Not be left Blank.. ", this);
                txtAcc1.Focus();
                return false;
            }
            if (txtAcc.Text != "" && txtAcc1.Text != string.Empty)
            {
                //btnMerge.Enabled = true;
            }

            //strLedgerToMerge1 =Convert.ToInt32(txtAcc.Text.ToString().Trim().Split('*')[0].ToString()).ToString();
            //strLedgerToBeMerge1 = Convert.ToInt32(txtAcc1.Text.ToString().Trim().Split('*')[0].ToString()).ToString();
            strLedgerToMerge1 = txtAcc.Text;
            strLedgerToBeMerge1 = txtAcc1.Text;
            if (strLedgerToMerge1 == strLedgerToBeMerge1)
            {
                objCommon.DisplayMessage(UPDLedger, "Both Ledgers Should Not Be Same..", this);
                txtAcc.Focus();
                clear();
                return false;
            }
            if (!strLedgerToMerge1.Contains("*") && !strLedgerToBeMerge1.Contains("*"))
            {
                objCommon.DisplayMessage(UPDLedger, "Both Ledgers Not exists..", this);
                txtAcc.Focus();
                return false;
            }
            if (!strLedgerToMerge1.Contains("*"))
            {
                objCommon.DisplayMessage(UPDLedger, " Ledger To Merge Not exists..", this);
                txtAcc.Focus();
                return false;
            }
            if (!strLedgerToBeMerge1.Contains("*"))
            {
                objCommon.DisplayMessage(UPDLedger, "Ledger To Be Merge Not exists..", this);
                txtAcc1.Focus();
                return false;
            }

        }
        catch (Exception ex)
        {
        }
        return true;
    }
    private void GetIndex()
    {
        AccountTransactionController objvch = new AccountTransactionController();
        strLedgerToMerge1 = txtAcc.Text.ToString().Trim().Split('*')[0].ToString();
        strLedgerToBeMerge1 = txtAcc1.Text.ToString().Trim().Split('*')[0].ToString();

        partyNoToMergeIndex = txtAcc.Text.IndexOf('*');
        strLedgerToMerge = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1].ToString() + "'");

        partyNoToBeMergeIndex = txtAcc1.Text.IndexOf('*');
        strLedgerToBeMerge = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc1.Text.ToString().Trim().Split('*')[1].ToString() + "'");



        //objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "" + "_party", "", "", "", "");
    }
    private bool IsTransactionExist()
    {
        AccountTransactionController objvch = new AccountTransactionController();
        DataSet ds = null;
        try
        {
            ds = objvch.GetTransactionFromParty(Convert.ToInt32(strLedgerToMerge), Session["comp_code"].ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return false;
    }
    public bool GetPaymentTypeNo()
    {
        AccountTransactionController objvch = new AccountTransactionController();
        //getting payement type no for particular ledger
        dsPartyToMerge = objvch.GetPartynoForTransaction(Convert.ToInt32(strLedgerToMerge), Session["comp_code"].ToString());
        dsPartyToBeMerge = objvch.GetPartynoForTransaction(Convert.ToInt32(strLedgerToBeMerge), Session["comp_code"].ToString());
        //Based on the payment type no ledgers can be merge otherwise not.
        if (dsPartyToMerge != null && dsPartyToBeMerge != null)
        {
            if (dsPartyToMerge.Tables[0].Rows.Count > 0 && dsPartyToBeMerge.Tables[0].Rows.Count > 0)
            {
                if (dsPartyToMerge.Tables[0].Rows[0]["payment_type_no"].ToString() != dsPartyToBeMerge.Tables[0].Rows[0]["payment_type_no"].ToString())
                {
                    objCommon.DisplayMessage(UPDLedger, "Ledger Type Mismatched. Both the ledger should be of same category..", this);
                    clear();
                    txtAcc1.Focus();

                    return false;
                }
            }
            return true;
        }
        return true;
    }
    protected void BindTransaction(int party_no, ListView lv, Panel pnl)
    {
        AccountTransactionController acc = new AccountTransactionController();
        try
        {
            DataSet ds = null;
            ds = acc.GetTransactionFromParty(party_no, Session["comp_code"].ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lv.DataSource = ds;
                    lv.DataBind();
                    pnl.GroupingText = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString();

                    // btnMerge.Enabled = true;
                }
                else
                {

                    lv.DataSource = null;
                    lv.DataBind();
                    // btnMerge.Enabled = false;
                    pnl.GroupingText = "";
                    if (pnl.ID.Equals("pnlLedgerToMerge"))
                    {
                        pnl.GroupingText = strLedgerToMerge1;
                    }
                    else if (pnl.ID.Equals("pnlLedgerToBeMerge"))
                    {
                        pnl.GroupingText = strLedgerToBeMerge1;
                    }


                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void clear()
    {
        txtAcc.Text = string.Empty;
        txtAcc1.Text = string.Empty;
        btnMerge.Enabled = false;
        pnlLedgerToBeMerge.Visible = false;
        pnlLedgerToMerge.Visible = false;
        lvLedgerToBeMerge.DataSource = null;
        lvLedgerToBeMerge.DataBind();
        lvLedgerToMerge.DataSource = null;
        lvLedgerToMerge.DataBind();
        chkDeleteLedger.Enabled = false;
        chkDeleteLedger.Checked = false;
        txtAcc.Enabled = true;
        txtAcc1.Enabled = true;

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MergeLedger.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MergeLedger.aspx");
        }
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMergeLedger(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetMergeData(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

}
