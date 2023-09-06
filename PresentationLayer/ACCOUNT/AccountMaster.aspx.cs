//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT MANSTER PAGE FOR CHEQUE PRINTING MODULE                                                     
// CREATION DATE : 27-04-2010                                               
// CREATED BY    : JITENDRA M. CHILATE                                                 
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

//using IITMS;
using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using System.Data.SqlClient;
using IITMS.NITPRM;
using System.Data.Linq;
using System.Collections.Generic;

public partial class AccountMaster : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    public string back = string.Empty;
    string IsAutogenratedCheckNumber = string.Empty;
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

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

        objCommon = new Common();

        txtChqCurNo.Attributes.Add("onkeydown", "return CheckNumeric(this);");
        txtChqFrmNo.Attributes.Add("onkeydown", "return CheckNumeric(this);");
        txtChqToNo.Attributes.Add("onkeydown", "return CheckNumeric(this);");
        txtChqFrmNo.Attributes.Add("onblur", "return SetCurrentChequeNo(this);");
        if (Session["comp_code"] != null)
        {
            SetParameters();
            ChangeThePageLayout();
        }
        if (!Page.IsPostBack)
        {
            ViewState["id"] = "0";
            txtBank.Focus();

            txtChqDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                    objCommon.DisplayMessage(UPDLedger, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    // Page.Title = Session["coll_name"].ToString();
                    //#region Load Page Help


                    //DataSet ds = new DataSet();

                    //if (HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()] == null)
                    //{

                    //    ds = objCommon.FillDropDown("CCMS_HELP_client", "HELPDESC", "PAGENAME", "PAGENAME='" + objCommon.GetCurrentPageName() + "'", "");
                    //}
                    //else
                    //{
                    //    ds = (DataSet)HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()];
                    //    DataView dv = ds.Tables[0].DefaultView;
                    //    dv.RowFilter = "PAGENAME='" + objCommon.GetCurrentPageName() + "'";
                    //    ds.Tables.Remove("Table");
                    //    ds.Tables.Add(dv.ToTable());

                    //}
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    lblHelp.Text = ds.Tables[0].Rows[0]["HELPDESC"].ToString();
                    //}
                    //else
                    //{
                    //    lblHelp.Text = "No Help Present!";

                    //}
                    //#endregion

                    PopulateListBox();
                    ViewState["action"] = "add";
                }
            }
        }
    }
    private void SetParameters()
    {
        objCommon = new Common();
        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_"+Session["comp_code"]+"_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
        int i = 0;
        if (ds != null)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {

                //if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "SINGLE MODE PAYMENT/RECIEPT/CONTRA ENTRY")
                if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW AUGENERATED CHEQUE NUMBER")
                {
                    IsAutogenratedCheckNumber = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();

                }

            }
        }
    }
    private void ChangeThePageLayout()
    {
        if (IsAutogenratedCheckNumber == "Y")
        {
            rowChkFrom.Visible = true;
            rowChkTo.Visible = true;
            rowChkCurrent.Visible = true;
            rowChkIssueDate.Visible = true;

            rfvchqfrmno.Enabled = true;
            rfvchqtono.Enabled = true;
            rfvchqcurno.Enabled = true;
            rfvchqissuedate.Enabled = true;
        }
        else
        {
            rowChkFrom.Visible = false;
            rowChkTo.Visible = false;
            rowChkCurrent.Visible = false;
            rowChkIssueDate.Visible = false;

            rfvchqfrmno.Enabled = false;
            rfvchqtono.Enabled = false;
            rfvchqcurno.Enabled = false;
            rfvchqissuedate.Enabled = false;
        }
    }
    private void PopulateListBox()
    {
        objCommon = new Common();
        try
        {
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "UPPER(ACCNAME) AS ACCOUNTNAME", "TRNO > 0", "TRNO");// "PARTY_NAME");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstBankName.Items.Clear();
                    lstBankName.DataTextField = "ACCOUNTNAME";
                    lstBankName.DataValueField = "TRNO";
                    lstBankName.DataSource = ds.Tables[0];
                    lstBankName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountMaster.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {

                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/AccountMaster.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=AccountMaster.aspx");
                }

            }

        }
        else
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/AccountMaster.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AccountMaster.aspx");
            }

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objCommon = new Common();

        if (txtBank.Text.ToString().IndexOf("*") == -1)
        {
            objCommon.DisplayMessage(UPDLedger, "Invalid Bank Name.", this);
            txtBank.Focus();
            return;
        }

        if (IsAutogenratedCheckNumber == "Y")
        {
            if (Convert.ToInt32(txtChqFrmNo.Text) > Convert.ToInt32(txtChqCurNo.Text) || Convert.ToInt32(txtChqToNo.Text) < Convert.ToInt32(txtChqCurNo.Text))
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Current Cheque No Within Range...", this);
                txtChqCurNo.Focus();
                return;
            }
            if (txtChqFrmNo.Text.Length != 6)
            {
                objCommon.DisplayMessage(UPDLedger, "Cheque No Should Be SIX Digit...", this);
                return;
            }
            if (txtChqToNo.Text.Length != 6)
            {
                objCommon.DisplayMessage(UPDLedger, "Cheque No Should Be SIX Digit...", this);
                return;
            }
            if (txtChqCurNo.Text.Length != 6)
            {
                objCommon.DisplayMessage(UPDLedger, "Cheque No Should Be SIX Digit...", this);
                return;
            }
        }

        string bankno = txtBank.Text.ToString().Trim().Split('*')[0];
        string bankname = txtBank.Text.ToString().Trim().Split('*')[1];
        DataSet dsbnk = objCommon.FillDropDown("ACC_BANK_DETAIL", "*", "", "BANKNO=" + bankno.ToString() + " and BANKNAME='" + bankname.ToString().Trim() + "'", "");
        if (dsbnk == null)
        {
            objCommon.DisplayMessage(UPDLedger, "Invalid Bank Name.", this);
            txtBank.Focus();
            return;
        }
        if (dsbnk.Tables[0].Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Invalid Bank Name.", this);
            txtBank.Focus();
            return;
        }

        if (ViewState["id"].ToString() == "" || ViewState["id"].ToString() == "0")
        {
            if (txtChqFrmNo.Text == string.Empty || txtChqToNo.Text == string.Empty)
            {

            }
            else
            {
                DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "UPPER(ACCNAME) AS ACCOUNTNAME", "CFRNO between " + txtChqFrmNo.Text.ToString().Trim() + " and " + txtChqToNo.Text.ToString().Trim(), "TRNO");// "PARTY_NAME");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Cheque Book Range already Issued.", this);
                        //objCommon.DisplayUserMessage(UPDLedger, "Cheque Book Range Allready Issued.", this);
                        txtChqFrmNo.Focus();
                        return;
                    }
                }

                DataSet ds1 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "UPPER(ACCNAME) AS ACCOUNTNAME", "CTONO between " + txtChqFrmNo.Text.ToString().Trim() + " and " + txtChqToNo.Text.ToString().Trim(), "TRNO");// "PARTY_NAME");
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Cheque Book Range already Issued.", this);
                        txtChqToNo.Focus();
                        return;
                    }
                }

                if (Convert.ToInt64(txtChqCurNo.Text.ToString().Trim()) >= Convert.ToInt64(txtChqFrmNo.Text.ToString().Trim()) && Convert.ToInt64(txtChqCurNo.Text.ToString().Trim()) <= Convert.ToInt64(txtChqToNo.Text.ToString().Trim()))
                { }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "Invalid Current Cheque No., Does Not Belong To Cheque Range", this);
                    txtChqCurNo.Focus();
                    return;
                }
            }

            AccountTransactionController oacc = new AccountTransactionController();
            Account_Master obj = new Account_Master();
            obj.TRNO = 0;
            obj.BNO = Convert.ToInt16(bankno);
            obj.ACCNO = txtAccountCode.Text.ToString().Trim();
            obj.ACCNAME = txtAccountName.Text.ToString().Trim();

            obj.CCURNO = (txtChqCurNo.Text.ToString() != string.Empty && txtChqCurNo.Text.ToString() != "" ? txtChqCurNo.Text.ToString().Trim() : "0");
            obj.CFRNO = (txtChqFrmNo.Text.ToString() != string.Empty && txtChqFrmNo.Text.ToString() != "" ? txtChqFrmNo.Text.ToString().Trim() : "0");
            obj.CTONO = (txtChqToNo.Text.ToString() != string.Empty && txtChqToNo.Text.ToString() != "" ? txtChqToNo.Text.ToString().Trim() : "0");
            obj.CISSUEDT = (txtChqDate.Text.ToString() != string.Empty && txtChqDate.Text.ToString() != "" ? Convert.ToDateTime(txtChqDate.Text.ToString().Trim()) : DateTime.Now);

            if (chkstatus.Checked == true)
            {
                obj.STATUS = 0;
            }
            else
            {
                obj.STATUS = 1;
            }
            int res = oacc.AddAccountDetails(obj, Session["comp_code"].ToString().Trim());
            if (res == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Account Created successfully", this);
                ClearAll();
                PopulateListBox();
                txtBank.Focus();
                return;
            }
        }
        else
        {
            //updating the account

            AccountTransactionController oacc = new AccountTransactionController();
            Account_Master obj = new Account_Master();
            obj.TRNO = Convert.ToInt32(ViewState["id"].ToString());
            obj.BNO = Convert.ToInt16(bankno);
            obj.ACCNO = txtAccountCode.Text.ToString().Trim();
            obj.ACCNAME = txtAccountName.Text.ToString().Trim();

            obj.CCURNO = (txtChqCurNo.Text.ToString() != string.Empty && txtChqCurNo.Text.ToString() != "" ? txtChqCurNo.Text.ToString().Trim() : "0");
            obj.CFRNO = (txtChqFrmNo.Text.ToString() != string.Empty && txtChqFrmNo.Text.ToString() != "" ? txtChqFrmNo.Text.ToString().Trim() : "0");
            obj.CTONO = (txtChqToNo.Text.ToString() != string.Empty && txtChqToNo.Text.ToString() != "" ? txtChqToNo.Text.ToString().Trim() : "0");
            obj.CISSUEDT = (txtChqDate.Text.ToString() != string.Empty && txtChqDate.Text.ToString() != "" ? Convert.ToDateTime(txtChqDate.Text.ToString().Trim()) : DateTime.Now);

            if (chkstatus.Checked == true)
            {
                obj.STATUS = 1;
            }
            else
            {
                obj.STATUS = 0;
            }
            int res = oacc.UpdateAccountMaster(obj, Session["comp_code"].ToString().Trim());
            if (res == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Account Updated successfully", this);
                ClearAll();
                PopulateListBox();
                txtBank.Focus();
                return;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        txtChqToNo.Text = "";
        txtChqFrmNo.Text = "";
        txtSearch.Text = "";
        txtChqDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtChqCurNo.Text = "";
        txtBank.Text = "";
        txtAccountCode.Text = "";
        ViewState["id"] = "";
        txtAccountName.Text = "";
        txtBank.Focus();
        lstBankName.DataSource = null;
        lstBankName.DataBind();
        PopulateListBox();
        chkstatus.Checked = false;
    }

    protected void lstBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new Common();

        try
        {
            //very important 
            string id = Request.Form[lstBankName.UniqueID].ToString();

            if (id != "" | id != string.Empty)
            {
                ClearAll();
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();

                //Show Details 
                PartyController objPC = new PartyController();
                string code_year = Session["comp_code"].ToString().Trim();// +"_" + Session["fin_yr"].ToString();

                DataSet dscnt = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "*", "", "TRNO = '" + Convert.ToString(id.ToString()).Trim().ToUpper() + "' ", string.Empty);

                if (dscnt != null)
                {
                    if (dscnt.Tables[0].Rows.Count > 0)
                    {
                        int o = 0;
                        for (o = 0; o < dscnt.Tables[0].Rows.Count; o++)
                        {
                            txtAccountCode.Text = dscnt.Tables[0].Rows[o]["ACCNO"].ToString().Trim();
                            txtAccountName.Text = dscnt.Tables[0].Rows[o]["ACCNAME"].ToString().Trim();
                            if (Convert.ToInt32(dscnt.Tables[0].Rows[o]["CFRNO"].ToString()) == 0 && IsAutogenratedCheckNumber == "N")
                            {
                                rowChkFrom.Visible = false;
                                rfvchqfrmno.Enabled = false;
                            }
                            else
                            {
                                txtChqFrmNo.Text = dscnt.Tables[0].Rows[o]["CFRNO"].ToString().Trim();
                                rowChkFrom.Visible = true;
                                rfvchqfrmno.Enabled = true;
                            }
                            if (Convert.ToInt32(dscnt.Tables[0].Rows[o]["CTONO"].ToString()) == 0 && IsAutogenratedCheckNumber == "N")
                            {
                                rowChkTo.Visible = false;
                                rfvchqtono.Enabled = false;
                            }
                            else
                            {
                                txtChqToNo.Text = dscnt.Tables[0].Rows[o]["CTONO"].ToString().Trim();
                                rowChkTo.Visible = true;
                                rfvchqtono.Enabled = true;
                            }
                            if (Convert.ToInt32(dscnt.Tables[0].Rows[o]["CCURNO"].ToString()) == 0 && IsAutogenratedCheckNumber == "N")
                            {
                                rowChkCurrent.Visible = false;
                                rfvchqcurno.Enabled = false;
                            }
                            else
                            {
                                txtChqCurNo.Text = dscnt.Tables[0].Rows[o]["CCURNO"].ToString().Trim();
                                rowChkCurrent.Visible = true;
                                rfvchqcurno.Enabled = true;
                            }
                            if (dscnt.Tables[0].Rows[o]["CISSUEDT"].ToString() != null && Convert.ToInt32(dscnt.Tables[0].Rows[o]["CFRNO"].ToString()) == 0 && IsAutogenratedCheckNumber == "N")
                            {
                                rowChkIssueDate.Visible = false;
                                rfvchqissuedate.Enabled = false;
                            }
                            else
                            {
                                txtChqDate.Text = Convert.ToDateTime(dscnt.Tables[0].Rows[o]["CISSUEDT"].ToString().Trim()).ToString("dd/MM/yyyy").ToString();
                                rowChkIssueDate.Visible = true;
                                rfvchqissuedate.Enabled = true;
                            }
                            //txtChqCurNo.Text = dscnt.Tables[0].Rows[o]["CCURNO"].ToString().Trim();
                            // txtChqFrmNo.Text = dscnt.Tables[0].Rows[o]["CFRNO"].ToString().Trim();
                            //txtChqToNo.Text = dscnt.Tables[0].Rows[o]["CTONO"].ToString().Trim();
                            //txtChqDate.Text = Convert.ToDateTime(dscnt.Tables[0].Rows[o]["CISSUEDT"].ToString().Trim()).ToString("dd/MM/yyyy").ToString();
                            if (dscnt.Tables[0].Rows[o]["STATUS"].ToString().Trim() == "0")
                            {
                                chkstatus.Checked = false;

                            }
                            else
                            {
                                chkstatus.Checked = true;
                            }


                        }

                        DataSet dsbn = objCommon.FillDropDown("ACC_BANK_DETAIL", "BANKNO", "BANKNAME", "BANKNO = '" + Convert.ToString(dscnt.Tables[0].Rows[0]["BNO"].ToString().Trim()) + "' ", string.Empty);

                        if (dsbn != null)
                        {
                            if (dsbn.Tables[0].Rows.Count > 0)
                            {
                                txtBank.Text = dsbn.Tables[0].Rows[0]["BANKNO"].ToString().Trim() + "*" + dsbn.Tables[0].Rows[0]["BANKNAME"].ToString().Trim();


                            }

                        }





                    }




                }





            }
            else
            {
                ViewState["action"] = "add";
                ViewState["id"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountMaster.lstBankName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
        }



    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        objCommon = new Common();

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "ACCNAME", "ACCNAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "ACCNAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstBankName.DataTextField = "ACCNAME";
                lstBankName.DataValueField = "TRNO";
                lstBankName.DataSource = ds.Tables[0];
                lstBankName.DataBind();

            }
        }

        txtSearch.Focus();

    }
    //Added By Vijay on 27-06-2020
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetBankDetails(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AccountMaster objCommon = new AccountMaster();

            ds = objCommon.GetBanks(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["BANKNo"].ToString() + " * " + ds.Tables[0].Rows[i]["BANKNAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    public DataSet GetBanks(string Search)//Added by Vijay Andoju 27-06-2020
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACC_BANK_DETAIL", "*", "", "BANKNAME like'%" + Search.ToString().Trim() + "%'", "");
        return ds;
    }
    protected void txtBank_TextChanged(object sender, EventArgs e)
    {

    }
}
