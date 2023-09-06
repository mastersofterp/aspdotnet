//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : LEDGER HEAD                                                     
// CREATION DATE : 02-SEPTEMBER-2009                                               
// CREATED BY    : NIRAJ D. PHALKE                                                 
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;
using System.Collections.Generic;

public partial class Account_ledgerhead : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenter objCostCenter = new CostCenter();
    CostCenterController objCostCenterController = new CostCenterController();
    public string back = string.Empty;
    string IsAutogenratedLedgerAccountCode = string.Empty;
    string accountCode = "00";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");
            }
            else
            {
                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
                //Response.Redirect("~/Account/selectCompany.aspx");
            }
        }
        else
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
            //Response.Redirect("~/Account/selectCompany.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //tvHierarchy.ExpandDepth = 5;
        tvHierarchy.ExpandAll();
        lnkBtnBankAcc.Attributes.Add("onClick", "return ShowLedger();");
        btnBack.Attributes.Add("onClick", "return CloseMe();");
        btnRecPayGrp.Attributes.Add("onClick", "return ShowRecPayGroup()");
        btnRecPayGrp1.Attributes.Add("onClick", "return ShowRecPayGroup()");
        btnMainGrp.Attributes.Add("onClick", "return ShowMainGroup()");
        if (Request.QueryString["obj"] != null)   /// Eknath
        { back = Request.QueryString["obj"].ToString().Trim(); }
        //txtContactNo.Attributes.Add("onkeydown", "return CheckNumeric(this);");

        //txtStatus.Attributes.Add("onfocus", "return getFocus(this);");
        if (Session["comp_code"] != null)
        {
            SetParameters();
            //ChangeThePageLayout();
        }

        if (!Page.IsPostBack)
        {
            trbnkacc.Style["Display"] = "none";
            txtLedgerName.Focus();
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
                    //Response.Redirect("~/ACCOUNT/selectCompany.aspx");

                    string PageUrl = HttpContext.Current.Request.Url.ToString().Split('/')[HttpContext.Current.Request.Url.ToString().Split('/').Length - 1];
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx?Page=" + PageUrl);
                }
                else
                {

                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    int value = Convert.ToInt32(Session["comp_no"]);
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    string ISCOSTCENTER = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='COST CENTER'");
                    if (ISCOSTCENTER == "Y")
                    {
                        chkCostCenter.Visible = true;
                    }
                    else
                    {
                        chkCostCenter.Visible = false;
                    }
                    string BudgetHead = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='ALLOW BUDGET HEAD'");
                    if (BudgetHead == "Y")
                    {
                        chkBudgetHead.Visible = true;
                    }
                    else
                    {
                        chkBudgetHead.Visible = false;
                    }

                    string CompanyList = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='ALLOW LEDGER CREATION OVER COMPANIES'");

                    if (CompanyList == "Y")
                    {
                        pnlCompany.Visible = true;
                        divpnl.Visible = true;
                    }
                    else
                    {
                        pnlCompany.Visible = false;
                        divpnl.Visible = false;
                    }
                    ChangeThePageLayout();
                    PopulateDropDown();
                    PopulateListBox();
                    PopulateCompanyList();

                    ViewState["action"] = "add";
                }
            }
        }
        chkDefault.Visible = false;
        //divMsg.InnerHtml = string.Empty;
        GetTotalOpeningBalances();
        //EnabledDisabledStatusDrCr();

    }


    private void PopulateCompanyList()
    {
        try
        {
            int value = Convert.ToInt32(Session["comp_no"]);
            DataSet dsCompany = objCommon.FillDropDown("ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + Session["userno"].ToString() + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "COMPANY_CODE,(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N' and COMPANY_NO NOT IN" + "(" + value + ")", "COMPANY_NAME");
            if (dsCompany.Tables.Count > 0)
            {
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    lvCompany.DataSource = dsCompany;
                    lvCompany.DataBind();
                    lvCompany.Visible = true;
                    Session["id"] = "0";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_selectCompany.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //used to auto increament the ledger account code based on the configuration set
    private void AutoGeneratedLedgerAccountCode()
    {
        //objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
        objCommon = new Common();
        string lookupAccountCode = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "" + "_party", "ISNULL(max(party_no),0)", "");
        int increamentedAccountCode = Convert.ToInt32(lookupAccountCode);
        increamentedAccountCode++;
        txtAccountCode.Text = accountCode + increamentedAccountCode;
        if (IsAutogenratedLedgerAccountCode == "Y")
            txtAccountCode.Enabled = false;
        else
            txtAccountCode.Enabled = true;
    }

    private void EnabledDisabledStatusDrCr()
    {
        if (txtOpenBalance.Text == "" || txtOpenBalance.Text == string.Empty)
        {
            rfvStatus.Enabled = false;
            ddlDrCr.Enabled = false;
        }
        else
        {
            if (Convert.ToDouble(txtOpenBalance.Text) > 0)
            {
                rfvStatus.Enabled = true;
                ddlDrCr.Enabled = true;
            }
            else
            {
                rfvStatus.Enabled = false;
                ddlDrCr.Enabled = false;
            }
        }
    }

    protected void chkall_OnCheckedChanged(object sender, EventArgs e)
    {
        if (Session["id"].ToString() == "0")
        {
            Session["id"] = "1";
        }
        else
        {
            Session["id"] = "0";
        }

        foreach (ListViewItem lst in lvCompany.Items)
        {
            CheckBox chk = lst.FindControl("chkall") as CheckBox;
            CheckBox chksingle = lst.FindControl("chk") as CheckBox;

            if (Session["id"] == "1")
            {
                chksingle.Checked = true;
            }
            else
            {
                chksingle.Checked = false;
            }
        }
    }

    private void GetTotalOpeningBalances()
    {
        PartyController ob = new PartyController();
        string code_year = Session["comp_code"].ToString();
        DataSet dsOp = ob.GetTotalOpeningBalances(code_year);
        if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {

                lblTotDr.Text = dsOp.Tables[0].Rows[0][1].ToString().Trim() + " Dr";
                lblTotCr.Text = dsOp.Tables[0].Rows[0][0].ToString().Trim() + " Cr";
                lblDiff.Text = (Convert.ToDouble(dsOp.Tables[0].Rows[0][0].ToString().Trim()) - Convert.ToDouble(dsOp.Tables[0].Rows[0][1].ToString().Trim())).ToString("0.00");
                if (Convert.ToDouble(lblDiff.Text) <= 0)
                {
                    lblDiff.Text = Math.Abs(Convert.ToDouble(lblDiff.Text)).ToString().Trim() + " Dr";

                }
                else
                {
                    lblDiff.Text = Math.Abs(Convert.ToDouble(lblDiff.Text)).ToString().Trim() + " Cr";

                }

            }

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = new CustomStatus();
            if (ViewState["action"].ToString().Equals("add"))
            {
                if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "count(*)", "ACC_CODE='" + txtAccountCode.Text.Trim() + "'")) > 0)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Account Code already in used", this.Page);
                    return;
                }
            }
            PartyController objPC = new PartyController();
            Party objParty = new Party();
            Trans objTrans = new Trans();

            //Party
            objParty.Party_Name = txtLedgerName.Text.Trim().ToUpper();
            objParty.Account_Code = txtAccountCode.Text.Trim();
            objParty.Party_Address = txtAddress.Text.Trim();

            DataSet dsgrp = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "*", "", "MGRP_NAME='" + ddlFAGroup.SelectedItem.Text.ToString().Trim() + "'", "");
            if (dsgrp != null)
            {
                if (dsgrp.Tables[0].Rows.Count > 0)
                {
                    objParty.Payment_Type_No = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["PAYMENT_TYPE_NO"].ToString().Trim());
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Payment type no error occured.", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Payment type no error occured.", this);
                return;
            }

            if (chkBudgetHead.Checked == true)
            {
                objParty.IsBudgetHead = true;
            }
            else
            {
                objParty.IsBudgetHead = false;
            }

            //objParty.Status = (ddlDrCr.SelectedValue.ToString().Trim() != "0"? ddlDrCr.SelectedValue.ToString().Trim() : null);
            objParty.Status = ddlDrCr.SelectedValue.ToString().Trim();
            objParty.Work_Nature = txtNatureWork.Text;
            // txtStatus.Text.ToUpper();     
            if (txtOpenBalance.Text.Trim() == "" || txtOpenBalance.Text.Trim() == string.Empty)
            {
                objParty.OpeningBalance = 0.00;
            }
            else
            {
                objParty.OpeningBalance = Convert.ToDouble(txtOpenBalance.Text);
            }

            objParty.Mgrp_No = Convert.ToInt16(ddlFAGroup.SelectedValue);
            objParty.RP_No = Convert.ToInt16(ddlRPGroup.SelectedValue);
            objParty.PGrp_No = Convert.ToInt16(ddlPGroup.SelectedValue);
            //objParty.Debit = Convert.ToInt16(ddlFADr.SelectedValue);
            //objParty.Credit = Convert.ToInt16(ddlFACr.SelectedValue);

            objParty.Freeze = false;// cbFreeze.Checked;
            objParty.StopOB = false;
            if (trbnkacc.Style["Display"] == "bound")
            {
                if (txtBankAc.Text.ToString() == "")
                {
                    //objCommon.DisplayUserMessage(UPDLedger, "Please Enter Bank Account Name", this);
                    // txtBankAc.Focus();
                    // return;
                    objParty.Bank_Account_No = "0";
                }
                else
                {
                    if (txtBankAc.Text.ToString().IndexOf('*') == -1)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter Valid Bank Account Name", this);
                        txtBankAc.Focus();
                        return;
                    }
                    else
                    {
                        objParty.Bank_Account_No = txtBankAc.Text.ToString().Split('*')[0].ToString();
                    }
                }
            }
            else
            {
                objParty.Bank_Account_No = "0";
            }

            objParty.Account_Code = txtAccountCode.Text.Trim();
            objParty.Party_Contact = txtContactNo.Text.ToString();
            objParty.College_Code = Session["colcode"].ToString();
            objParty.PANNO = txtPanNo.Text;
            objParty.TINNO = txtTinNo.Text;

            if (chkDefault.Checked)
                objParty.SetDefault = 1;
            else
                objParty.SetDefault = 0;
            //Trans

            if (ddlDrCr.SelectedValue.ToString().Trim() == "O")
            {
                if ((txtOpenBalance.Text != "0") && (txtOpenBalance.Text != "") || txtOpenBalance.Text != string.Empty)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Dr Or Cr Type.", this);
                    ddlDrCr.Focus();
                    return;
                }
            }

            objTrans.Tran = ddlDrCr.SelectedValue.ToString().Trim();

            objTrans.Transaction_Date = Convert.ToDateTime(Session["fin_date_from"]);
            objTrans.Transaction_Type = "OB";   //Opening Balance

            string code_year = Session["comp_code"].ToString().Trim();// +"_" + Session["fin_yr"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int i = 0;
                    for (i = 0; i < lstLedgerName.Items.Count; i++)
                    {
                        string strLedgerName = lstLedgerName.Items[i].Text;
                        int index = strLedgerName.IndexOf('[');
                        string strLedgernameToCompare = strLedgerName.Remove(index);
                        if (txtLedgerName.Text.ToString().Trim().ToUpper() == strLedgernameToCompare.Trim().ToUpper())
                        {
                            objCommon.DisplayUserMessage(UPDLedger, "Ledger Allready Exists, Please Try Another Ledger Name.", this);
                            txtLedgerName.Focus();
                            return;
                        }
                    }

                    string CompanyList = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='ALLOW LEDGER CREATION OVER COMPANIES'");

                    if (CompanyList == "Y")
                    {
                        string code_year_Add = string.Empty;
                        string OpeningBal = string.Empty;
                        string Status_ADD = string.Empty;
                        int count = 0;

                        foreach (ListViewItem Company in lvCompany.Items)
                        {
                            CheckBox chk = Company.FindControl("chk") as CheckBox;
                            Label lblCompanyName = Company.FindControl("lblCompanyName") as Label;
                            TextBox txtOpeningBal = Company.FindControl("txtOpeningBal") as TextBox;
                            DropDownList ddlDebitCredit = Company.FindControl("grdddlDrCr") as DropDownList;

                            if (chk.Checked == true)
                            {
                                count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_MAIN_GROUP", "count(*)", "MGRP_NO=" + ddlFAGroup.SelectedValue));
                                if (count > 0)
                                {
                                    code_year_Add += chk.ToolTip + ",";

                                    if (ddlDebitCredit.SelectedValue == "D")
                                    {
                                        Status_ADD += "D" + ",";
                                    }
                                    else
                                    {
                                        Status_ADD += "C" + ",";
                                    }
                                    OpeningBal += txtOpeningBal.Text + ",";
                                }
                            }
                        }
                        string OPBal = OpeningBal.ToString().Trim(',');
                        string CCode = code_year_Add.ToString().Trim(',');
                        string Status = Status_ADD.ToString().Trim(',');

                        cs = (CustomStatus)objPC.AddPartyWithMultipleCompanies(objParty, objTrans, code_year, Convert.ToInt32(chkCostCenter.Checked), OPBal, Status, CCode);
                    }
                    else
                    {
                        cs = (CustomStatus)objPC.AddParty(objParty, objTrans, code_year, Convert.ToInt32(chkCostCenter.Checked));
                    }

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        if (IsAutogenratedLedgerAccountCode == "Y")
                        {
                            AutoGeneratedLedgerAccountCode();
                        }
                        if (CompanyList == "Y")
                        {
                            pnlCompany.Visible = true;
                            divpnl.Visible = true;
                        }
                        objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully!", this);
                        PopulateListBox();
                        PopulateCompanyList();
                        tvHierarchy.Nodes.Clear();
                        //tvHierarchy.DataBind();
                        tdFAgroup.Visible = true;
                    }
                    else
                        objCommon.DisplayMessage(UPDLedger, "Record Not Saved!", this);
                }
                else
                {
                    if (ViewState["id"] != null)
                    {
                        objParty.Party_No = Convert.ToInt16(ViewState["id"]);

                        cs = (CustomStatus)objPC.UpdateParty(objParty, objTrans, code_year, Convert.ToInt32(chkCostCenter.Checked));

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            if (IsAutogenratedLedgerAccountCode == "Y")
                            {
                                AutoGeneratedLedgerAccountCode();
                            }
                            PopulateDropDown();
                            PopulateListBox();
                            objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully!!", this);
                            tvHierarchy.Nodes.Clear();
                            //tvHierarchy.DataBind();
                            tdFAgroup.Visible = true;
                        }
                        else
                            objCommon.DisplayMessage(UPDLedger, "Record Not Saved!", this);
                    }
                }
            }
            ViewState["action"] = "add";
            ViewState["id"] = null;
            txtLedgerName.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_ledgerhead.lstLedgerName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        GetTotalOpeningBalances();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        //Response.Redirect(Request.Url.ToString());

        //txtNatureWork.Text = "";
        //txtTinNo.Text = "";
        //txtPanNo.Text = "";


        //txtLedgerName.Text = "";
        //txtAccountCode.Text = "";
        //txtContactNo.Text = "";
        //txtAddress.Text = "";
        //txtOpenBalance.Text = "";
        //ddlDrCr.SelectedValue = "D";
        //ddlFAGroup.SelectedIndex = 0;
        //txtBankAc.Text = "";
        //trbnkacc.Style["Display"] = "none";
        //ddlRPGroup.SelectedIndex = 0;
        //ddlPGroup.SelectedIndex = 0;
        //chkBudgetHead.Checked = true;
        ////ddlType.SelectedIndex = 0;
        //ViewState["action"] = "add";
        //ViewState["id"] = null;
        //if (IsAutogenratedLedgerAccountCode == "Y")
        //    txtAccountCode.Enabled = false;
        //else
        //    txtAccountCode.Enabled = true;

        //if (IsAutogenratedLedgerAccountCode == "Y")
        //{
        //    AutoGeneratedLedgerAccountCode();
        //}

        //GetTotalOpeningBalances();
        //txtLedgerName.Focus();
        //btnSubmit.Attributes.Remove("onClick");
        ////tdFAgroup.Visible = false;
        //tvHierarchy.Nodes.Clear();
        //tdFAgroup.Visible = true;

        Response.Redirect(Request.Url.ToString());
    }

    protected void btnPanReport_Click(object sender, EventArgs e)
    {
        ShowReport("PAN/TIN No Report", "LedgerPanTinReport.rpt");
    }


    protected void lstLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //very important 
            string id = Request.Form[lstLedgerName.UniqueID].ToString();
            pnlCompany.Visible = false;
            divpnl.Visible = false;
            if (lstLedgerName.SelectedItem.Text != "PROFIT & LOSS A/c")
            {
                if (id != "" | id != string.Empty)
                {
                    Clear();
                    ViewState["action"] = "edit";
                    ViewState["id"] = id.ToString();

                    //Show Details 
                    PartyController objPC = new PartyController();
                    string code_year = Session["comp_code"].ToString().Trim(); // +"_" + Session["fin_yr"].ToString();

                    DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(id), code_year);
                    if (dtr.Read())
                    {
                        txtLedgerName.Text = dtr["PARTY_NAME"].ToString();
                        txtAccountCode.Text = dtr["ACC_CODE"] == DBNull.Value ? string.Empty : dtr["ACC_CODE"].ToString();
                        if (IsAutogenratedLedgerAccountCode == "Y")
                            txtAccountCode.Enabled = false;
                        else
                            txtAccountCode.Enabled = true;

                        txtAddress.Text = dtr["PARTY_ADDRESS"] == DBNull.Value ? string.Empty : dtr["PARTY_ADDRESS"].ToString();
                        txtPanNo.Text = dtr["PANNO"] == DBNull.Value ? string.Empty : dtr["PANNO"].ToString();
                        txtTinNo.Text = dtr["TINNO"] == DBNull.Value ? string.Empty : dtr["TINNO"].ToString();
                        //ddlType.SelectedValue = dtr["PAYMENT_TYPE_NO"] == DBNull.Value ? "0" : dtr["PAYMENT_TYPE_NO"].ToString();
                        ddlDrCr.SelectedValue = dtr["STATUS"].ToString().Trim();
                        //if (dtr["OPBALANCE"].ToString() == "0.00" || dtr["OPBALANCE"] == DBNull.Value)
                        //{
                        //    txtOpenBalance.Text = string.Empty;
                        //}
                        //else
                        //{
                        txtNatureWork.Text = dtr["Work_Nature"].ToString();

                        if (dtr["ISBudgetHead"].ToString().Trim() != null)
                        {
                            if (Convert.ToInt32(dtr["ISBudgetHead"].ToString().Trim()) == 1)
                            {
                                chkBudgetHead.Checked = true;
                            }
                            else
                            {
                                chkBudgetHead.Checked = false;
                            }
                        }

                        if (dtr["ISCCApplicable"].ToString().Trim() != null)
                        {
                            if (Convert.ToInt32(dtr["ISCCApplicable"].ToString().Trim()) == 1)
                            {
                                chkCostCenter.Checked = true;
                            }
                            else
                            {
                                chkCostCenter.Checked = false;
                            }
                        }

                        if (dtr["SetDefault"].ToString().Trim() != null)
                        {
                            if (Convert.ToInt32(dtr["SetDefault"].ToString().Trim()) == 1)
                            {
                                chkDefault.Checked = true;
                            }
                            else
                            {
                                chkDefault.Checked = false;
                            }
                        }
                        txtOpenBalance.Text = dtr["OPBALANCE"].ToString();
                        //}
                        // txtOpenBalance.Text = dtr["OPBALANCE"] == DBNull.Value ? string.Empty : dtr["OPBALANCE"].ToString();
                        if (dtr["BANKACCOUNTNO"].ToString().Trim() == "0")
                        {
                            trbnkacc.Style["Display"] = "none";

                        }
                        else
                        {
                            //trbnkacc.Style["Display"] = "block";
                            trbnkacc.Style["Display"] = "bound";

                            DataSet dsbnk = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC", "accno", "accname", "accno='" + dtr["BANKACCOUNTNO"].ToString().Trim() + "'", "");
                            if (dsbnk != null)
                            {
                                if (dsbnk.Tables[0].Rows.Count > 0)
                                {
                                    txtBankAc.Text = dsbnk.Tables[0].Rows[0]["accno"].ToString().Trim() + "*" + dsbnk.Tables[0].Rows[0]["accname"].ToString().Trim();


                                }

                            }
                        }

                        ddlFAGroup.SelectedValue = dtr["MGRP_NO"] == DBNull.Value ? "0" : dtr["MGRP_NO"].ToString();
                        ddlRPGroup.SelectedValue = dtr["RP_NO"] == DBNull.Value ? "0" : dtr["RP_NO"].ToString();
                        ddlPGroup.SelectedValue = dtr["PGRP_NO"] == DBNull.Value ? "0" : dtr["PGRP_NO"].ToString();
                        string PyamentTypeNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PAYMENT_TYPE_NO", "Party_No=" + id.ToString());
                        if (PyamentTypeNo != null && PyamentTypeNo == "2")
                            chkDefault.Visible = true;
                        tdFAgroup.Visible = true;
                        DataTable dtPRNO = objPC.getParentNo(Session["comp_code"].ToString(), ddlFAGroup.SelectedValue.ToString()).Tables[0];
                        tvHierarchy.Nodes.Clear();
                        populatetreeview(dtPRNO, 0, null);

                        tvHierarchy.ExpandAll();
                    }
                    dtr.Close();


                    DataSet dscnt = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTYCONTACT_DIRECTORY", "*", "", "PartyId = '" + Convert.ToString(id.ToString()).Trim().ToUpper() + "' ", string.Empty);
                    txtContactNo.Text = "";
                    if (id != null)
                    {
                        if (dscnt.Tables[0].Rows.Count > 0)
                        {

                            txtContactNo.Text = dscnt.Tables[0].Rows[0][1].ToString().Trim();


                        }


                    }

                    //string ISAPLICABLE = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CCLEDGER", "CCAplicable", "Party_No=" + id.ToString());
                    //if (ISAPLICABLE.Trim() == "Y")
                    //{
                    //    chkCostCenter.Checked = true;
                    //}
                    //else
                    //{
                    //    chkCostCenter.Checked = false;
                    //}

                    int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "COUNT(*)", "TRANSACTION_TYPE<>'OB' and PARTY_NO=" + id.ToString()));
                    if (count > 0)
                        btnSubmit.Attributes.Add("onClick", "return TransactionCheck()");
                    else
                        btnSubmit.Attributes.Remove("onClick");

                }
                else
                {
                    ViewState["action"] = "add";
                    ViewState["id"] = null;
                }
            }
            else
            { objCommon.DisplayMessage(UPDLedger, "This Leadger is UnEditable!", this); }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_ledgerhead.lstLedgerName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    //protected void cbTB_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (cbTB.Checked == true)
    //        cbTB.Text = "Yes";
    //    else
    //        cbTB.Text = "No";

    //    cbFreeze.Focus();
    //}
    //protected void cbFreeze_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (cbFreeze.Checked == true)
    //        cbFreeze.Text = "Yes";
    //    else
    //        cbFreeze.Text = "No";
    //}

    #region User Defined Methods

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["obj"] != null)
    //    {
    //        if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
    //        {
    //            back = "AccountingVouchers.aspx";

    //            btnBack.Visible = true;

    //        }
    //    }
    //    else
    //    {

    //        btnBack.Visible = false;

    //        if (Request.QueryString["pageno"] != null)
    //        {
    //            //Check for Authorization of Page
    //            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //            {
    //                Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
    //            }
    //        }
    //        else
    //        {
    //            //Even if PageNo is Null then, don't show the page
    //            Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
    //        }

    //    }
    //}

    private void PopulateDropDown()
    {
        try
        {



            //objCommon.FillDropDownList(ddlType, "ACC_PAYMENT_TYPE_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "PAYMENT_TYPE_NO", "UPPER(PAYMENT_TYPE_NAME) AS PAYMENT_TYPE_", "PAYMENT_TYPE_NO > 0", "PAYMENT_TYPE_NAME");
            //objCommon.FillDropDownList(ddlFAGroup, Session["DataBase"].ToString()+ "."+"ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "MGRP_NO > 0", "MGRP_NAME");
            objCommon.FillDropDownList(ddlFAGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME) AS MGRP_NAME", "MGRP_NO > 0", "MGRP_NAME");
            objCommon.FillDropDownList(ddlRPGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "RECIEPT_PRINT_GROUP", "RP_NO", "UPPER(RP_NAME) AS RP_NAME", "RP_NO > 0 AND RPH_NO=1", "RP_NAME");
            objCommon.FillDropDownList(ddlPGroup, "ACC_" + Session["comp_code"].ToString() + "_" + "RECIEPT_PRINT_GROUP", "RP_NO", "UPPER(RP_NAME) AS RP_NAME", "RP_NO > 0 AND RPH_NO=2", "RP_NAME");

            string ISGROUP = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='RECEIPT PAYMENT GROUP'");
            if (ISGROUP == "N")
            {
                ddlRPGroup.SelectedValue = "1";
                ddlPGroup.SelectedValue = "2";
                trRPGroup.Visible = false;
                trPGroup.Visible = false;
            }
            else
            {
                ddlRPGroup.Visible = true;
                ddlPGroup.Visible = true;
            }
            //string SalePurchase = objCommon.LookUp("ACC_"_CONFIG", "PARAMETER", "CONFIGDESC='SALE PURCHASE CONFIGURATION'");

            //if (SalePurchase == "N")
            //{
            //    ddlSalePurchase.SelectedValue = "0";
            //    trSalePurchase.Visible = false;
            //}
            //else
            //{
            //    ddlSalePurchase.Visible = true;
            //}

            //objCommon.FillDropDownList(ddlRPGroup, Session["DataBase"].ToString() + "." +"ACC_" + Session["comp_code"].ToString() + "_" + "RECIEPT_PRINT_GROUP", "RP_NO", "UPPER(RP_NAME) AS RP_NAME", "RP_NO > 0", "RP_NAME");
            //objCommon.FillDropDownList(ddlFADr, "ACC_TB_GROUPS_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "TBG_NO", "UPPER(TBG_NAME) AS TBG_NAME", "TBG_NO > 0 AND DC = 'D'", "TBG_NAME");
            //objCommon.FillDropDownList(ddlFACr, "ACC_TB_GROUPS_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "TBG_NO", "UPPER(TBG_NAME) AS TBG_NAME", "TBG_NO > 0 AND DC = 'C'", "TBG_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_ledgerhead.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");


            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    private void PopulateListBox()
    {
        try
        {
            // lstLedgerName
            // DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "PARTY_NO > 0", "PARTY_NO");// "PARTY_NAME");
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "UPPER(PARTY_NAME)+'  '+ '[  '+ acc_code+'  ]' AS PARTY_NAME", "PARTY_NO > 0", "PARTY_NAME");// "PARTY_NAME");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstLedgerName.Items.Clear();
                    lstLedgerName.DataTextField = "PARTY_NAME";
                    lstLedgerName.DataValueField = "PARTY_NO";
                    lstLedgerName.DataSource = ds.Tables[0];
                    lstLedgerName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_ledgerhead.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    private void Clear()
    {
        txtContactNo.Text = string.Empty;
        txtLedgerName.Text = string.Empty;
        txtAccountCode.Text = string.Empty;
        txtTinNo.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtOpenBalance.Text = string.Empty;
        ddlDrCr.SelectedValue = "D";
        if (IsAutogenratedLedgerAccountCode == "Y")
            txtAccountCode.Enabled = false;
        else
            txtAccountCode.Enabled = true;
        chkBudgetHead.Checked = true;
        ddlFAGroup.SelectedIndex = 0;
        ddlRPGroup.SelectedIndex = 0;
        ddlPGroup.SelectedIndex = 0;
        ddlDrCr.SelectedIndex = 0;
        lstLedgerName.SelectedIndex = -1;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
        txtBankAc.Text = "";
        txtNatureWork.Text = string.Empty;
        trbnkacc.Style["Display"] = "none";
        ViewState["id"] = null;
        string ISGROUP = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='RECEIPT PAYMENT GROUP'");
        if (ISGROUP == "N")
        {
            ddlRPGroup.SelectedValue = "1";
            ddlPGroup.SelectedValue = "2";
            trRPGroup.Visible = false;
            trPGroup.Visible = false;
        }
        else
        {
            ddlRPGroup.Visible = true;
            ddlPGroup.Visible = true;
        }

        chkCostCenter.Checked = false;
        chkDefault.Checked = false;
        chkDefault.Visible = false;
        divpnl.Visible = false;
        // EnabledDisabledStatusDrCr();
        //tdFAgroup.Visible = false;
    }

    private void RemoveChk()
    {
        try
        {
            foreach (ListViewItem lst in lvCompany.Items)
            {
                CheckBox chk = lst.FindControl("chkall") as CheckBox;
                CheckBox chksingle = lst.FindControl("chk") as CheckBox;

                //chk.Checked = false;
                chksingle.Checked = false;
            }
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception)
        {

        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    #endregion

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_CODE_YEAR=" + Session["comp_code"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(back + ".aspx?obj=config");

    }
    protected void hdnPartyNo_ValueChanged(object sender, EventArgs e)
    {
        try
        {
            //very important 
            string id = hdnPartyNo.Value.ToString().Trim();

            if (id != "" | id != string.Empty)
            {
                Clear();
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();

                //Show Details 
                PartyController objPC = new PartyController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
                string code_year = Session["comp_code"].ToString() + "_" + Session["fin_yr"].ToString();

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(id), code_year);
                if (dtr.Read())
                {
                    txtLedgerName.Text = dtr["PARTY_NAME"].ToString();
                    txtAccountCode.Text = dtr["ACC_CODE"] == DBNull.Value ? string.Empty : dtr["ACC_CODE"].ToString();
                    txtAddress.Text = dtr["PARTY_ADDRESS"] == DBNull.Value ? string.Empty : dtr["PARTY_ADDRESS"].ToString();
                    txtTinNo.Text = dtr["TINNO"] == DBNull.Value ? string.Empty : dtr["TINNO"].ToString();
                    txtPanNo.Text = dtr["PANNO"] == DBNull.Value ? string.Empty : dtr["PANNO"].ToString();
                    //ddlType.SelectedValue = dtr["PAYMENT_TYPE_NO"] == DBNull.Value ? "0" : dtr["PAYMENT_TYPE_NO"].ToString();
                    ddlDrCr.SelectedValue = dtr["STATUS"].ToString().Trim();
                    txtOpenBalance.Text = dtr["OPBALANCE"] == DBNull.Value ? string.Empty : dtr["OPBALANCE"].ToString();

                    ddlFAGroup.SelectedValue = dtr["MGRP_NO"] == DBNull.Value ? "0" : dtr["MGRP_NO"].ToString();
                    ddlRPGroup.SelectedValue = dtr["RP_NO"] == DBNull.Value ? "0" : dtr["RP_NO"].ToString();
                    ddlPGroup.SelectedValue = dtr["PGRP_NO"] == DBNull.Value ? "0" : dtr["PGRP_NO"].ToString();
                    //ddlFACr.SelectedValue = dtr["CREDIT"] == DBNull.Value ? "0" : dtr["CREDIT"].ToString();
                    //ddlFADr.SelectedValue = dtr["DEBIT"] == DBNull.Value ? "0" : dtr["DEBIT"].ToString();
                    //cbTB.Checked = dtr["STOPOB"] == DBNull.Value ? false : Convert.ToBoolean(dtr["STOPOB"]);
                    //cbTB.Text = cbTB.Checked == true ? "Yes" : "No";
                    //cbFreeze.Checked = dtr["FREEZE"] == DBNull.Value ? false : Convert.ToBoolean(dtr["FREEZE"]);
                    //cbFreeze.Text = cbFreeze.Checked == true ? "Yes" : "No";
                }
                dtr.Close();
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
                objUCommon.ShowError(Page, "Account_ledgerhead.lstLedgerName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }


    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PARTY_NAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "PARTY_NAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstLedgerName.DataTextField = "PARTY_NAME";
                lstLedgerName.DataValueField = "PARTY_NO";
                lstLedgerName.DataSource = ds.Tables[0];
                lstLedgerName.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Sorry! Record Not Found,Try Again...", this.Page);
            }
        }
        txtSearch.Focus();
    }
    protected void ddlFAGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        tvHierarchy.ExpandDepth = 5;
        PartyController objPC = new PartyController();
        DataSet dsFa = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "PAYMENT_TYPE_NO", "MGRP_NAME", "MGRP_NO=" + ddlFAGroup.SelectedValue.ToString().Trim(), "");
        if (dsFa != null)
        {
            if (dsFa.Tables[0].Rows.Count > 0)
            {
                if (dsFa.Tables[0].Rows[0][0].ToString().Trim() == "2")
                {
                    trbnkacc.Style["Display"] = "bound";
                    chkDefault.Visible = true;
                  

                }
                else
                {
                    trbnkacc.Visible = false;
                    trbnkacc.Style["visibility"]="hidden";
                    trbnkacc.Style["Display"] = "none";
                    chkDefault.Visible = false;
                   
                }

            }

        }
        ddlFAGroup.Focus();
        tdFAgroup.Visible = true;
        //DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=0", "").Tables[0];
        DataTable dtPRNO = objPC.getParentNo(Session["comp_code"].ToString(), ddlFAGroup.SelectedValue.ToString()).Tables[0];
        tvHierarchy.Nodes.Clear();
        populatetreeview(dtPRNO, 0, null);
        tvHierarchy.ExpandAll();
    }
    protected void lnkBtnBankAcc_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// code to check account number is exist. if yes then do not allow the same account code
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtAccountCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string accountCode = string.Empty;
            accountCode = txtAccountCode.Text.Trim();
            //PartyController objPC1 = new PartyController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
            PartyController objPC1 = new PartyController();
            ds = objPC1.GetPartyAccountCode(accountCode, Session["comp_code"].ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAccountCode.Text = string.Empty;
                    objCommon.DisplayUserMessage(UPDLedger, "Account Code Is Already Exist..Please Enter Another Account Code", this);
                    txtAccountCode.Focus();
                    return;

                }
                else
                    txtAddress.Focus();

            }
            else
                txtAddress.Focus();


        }
        catch (Exception ex)
        {

        }
    }
    //NEWLY ADDED FOR AUTOGENERATION OF LEDGER ACCOUNT CODE
    private void SetParameters()
    {
        //objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
        objCommon = new Common();
        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_REF_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
        int i = 0;
        if (ds != null)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {

                if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW AUTOGENERATED LEDGER HEAD ACCOUNT CODE")
                {
                    IsAutogenratedLedgerAccountCode = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();

                }

            }
        }
    }
    private void ChangeThePageLayout()
    {
        if (IsAutogenratedLedgerAccountCode == "Y" && ViewState["action"] == null)
        {
            txtAccountCode.Enabled = false;
            AutoGeneratedLedgerAccountCode();
        }
        else
        {
            txtAccountCode.Enabled = true;
        }

    }


    protected void btnRecPayGrp_Click(object sender, EventArgs e)
    {
        PopulateDropDown();
    }
    protected void btnRecPayGrp1_Click(object sender, EventArgs e)
    {
        PopulateDropDown();
    }
    protected void btnMainGrp_Click(object sender, EventArgs e)
    {
        PopulateDropDown();
    }

    private void populatetreeview(DataTable dt, int ParentID, TreeNode TreeNode)
    {
        foreach (DataRow row in dt.Rows)
        {
            TreeNode treeChild = new TreeNode()
            {
                Text = row["MGRP_NAME"].ToString(),
                Value = row["MGRP_NO"].ToString()
            };
            if (ParentID == 0)
            {
                tvHierarchy.Nodes.Add(treeChild);
                DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
            else
            {
                TreeNode.ChildNodes.Add(treeChild);

                DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
        }
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
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/selectCompany.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
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
        //else
        //{
        //    //Check for Authorization of Page
        //    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/selectCompany.aspx'")) == false)
        //    {
        //        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
        //    }
        //}
    }
    //Added By Vijay on 13-07-2020

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAcc(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            Account_ledgerhead objCommon = new Account_ledgerhead();

            ds = objCommon.GetBanks(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["accno"].ToString().Trim() + "*" + ds.Tables[0].Rows[i]["accname"].ToString().Trim());

            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    public DataSet GetBanks(string Search)//Added by Vijay Andoju 13-07-2020
    {
        DataSet dsbnk = null;
        try
        {
            dsbnk = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC", "ACCNO", "ACCNAME", "ACCNAME LIKE '%" + Search + "%'", "");
        }
        catch (Exception ex)
        {
            dsbnk.Dispose();
        }
        return dsbnk;
    }
    protected void txtBankAc_TextChanged(object sender, EventArgs e)
    {

    }
}
