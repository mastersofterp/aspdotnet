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
using IITMS.NITPRM;

public partial class ACCOUNT_ACC_SchMaster : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    IncomeExpenBalanceSheetController objIEBS = new IncomeExpenBalanceSheetController();
    IncomeExpenBalanceSheet objIEBSEntity = new IncomeExpenBalanceSheet();
    string space1 = "     ".ToString();
    string space2 = "          ".ToString();
    string space3 = string.Empty;

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
        if (!Page.IsPostBack)
        {
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

                    // divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    PopulateSheduleList();
                    PopulateLedgerForSchedule();
                    ViewState["action"] = "add";
                }
            }
        }
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

        }
        dtr.Close();


    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportType.SelectedValue == "IE")
        {
            ddlSchType.Items.Clear();
            ListItem LSP = new ListItem("--Please Select--", "0");
            ddlSchType.Items.Add(LSP);
            ListItem LSI = new ListItem("INCOME", "I");
            ListItem LSE = new ListItem("EXPENDITURE", "E");
            ddlSchType.Items.Add(LSI);
            ddlSchType.Items.Add(LSE);


        }
        else if (ddlReportType.SelectedValue == "BS")
        {
            ddlSchType.Items.Clear();
            ListItem LSP = new ListItem("--Please Select--", "0");
            ddlSchType.Items.Add(LSP);
            ListItem LSI = new ListItem("ASSET", "A");
            ListItem LSE = new ListItem("LIABILITIES", "L");
            ddlSchType.Items.Add(LSI);
            ddlSchType.Items.Add(LSE);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    void clear()
    {
        ddlReportType.SelectedValue = "0";
        ddlSchType.SelectedValue = "0";
        txtSchName.Text = string.Empty;
        ViewState["action"] = "add";
        trLedgerSelection.Visible = false;
    }

    private void PopulateSheduleList()
    {
        try
        {
            DataSet dsShedule = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_SchMaster", "Sch_ID", "Sch_name", "", "");
            if (dsShedule.Tables.Count > 0)
            {
                if (dsShedule.Tables[0].Rows.Count > 0)
                {
                    lstSchedule.DataTextField = "Sch_name";
                    lstSchedule.DataValueField = "Sch_ID";
                    lstSchedule.DataSource = dsShedule.Tables[0];
                    lstSchedule.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACC_SchMaster.PopulateSheduleList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateLedgerList()
    {
        try
        {
            DataSet dsLedger = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "PARTY_NO > 0", "PARTY_NAME");// "PARTY_NAME");

            if (dsLedger.Tables.Count > 0)
            {
                if (dsLedger.Tables[0].Rows.Count > 0)
                {
                    lstLedgerName.Items.Clear();
                    lstLedgerName.DataTextField = "PARTY_NAME";
                    lstLedgerName.DataValueField = "PARTY_NO";
                    lstLedgerName.DataSource = dsLedger.Tables[0];
                    lstLedgerName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACC_SchMaster.PopulateLedgerList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateSelectedLedger()
    {
        try
        {
            DataSet dsLedger = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_Sheduled_Ledger", "Party_No", "UPPER(Ledger_Name) AS PARTY_NAME", "Sch_ID =" + ViewState["SchID"].ToString(), "PARTY_NAME");// "PARTY_NAME");

            if (dsLedger.Tables.Count > 0)
            {
                if (dsLedger.Tables[0].Rows.Count > 0)
                {
                    lstSelectedLedger.Items.Clear();
                    lstSelectedLedger.DataTextField = "PARTY_NAME";
                    lstSelectedLedger.DataValueField = "Party_No";
                    lstSelectedLedger.DataSource = dsLedger.Tables[0];
                    lstSelectedLedger.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACC_SchMaster.PopulateLedgerList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }


    private void PopulateLedgerForSchedule()
    {
        try
        {
            DataSet dsLedger = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "party_no not in (select Party_no from ACC_" + Session["comp_code"].ToString() + "_Sheduled_Ledger)", "PARTY_NAME");// "PARTY_NAME");

            if (dsLedger.Tables.Count > 0)
            {
                if (dsLedger.Tables[0].Rows.Count > 0)
                {
                    lstLedgerName.Items.Clear();
                    lstLedgerName.DataTextField = "PARTY_NAME";
                    lstLedgerName.DataValueField = "PARTY_NO";
                    lstLedgerName.DataSource = dsLedger.Tables[0];
                    lstLedgerName.DataBind();


                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACC_SchMaster.PopulateLedgerList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < lstSchedule.Items.Count; i++)
            {
                if (txtSchName.Text == lstSchedule.Items[i].Text)
                {
                    objCommon.DisplayUserMessage(updpan, "Schedule Name Is Already Exist", this.Page);
                    return;
                }
            }



            objIEBSEntity.SchduleName = txtSchName.Text.ToUpper();
            objIEBSEntity.Schtype = ddlSchType.SelectedValue.ToString();
            objIEBSEntity.BS_IE = ddlReportType.SelectedValue.ToString();
            objIEBSEntity.AuditDate = DateTime.Now;
            objIEBSEntity.User_Id = Session["userno"].ToString().Trim();
            string comp_code = Session["comp_code"].ToString().Trim();
            objIEBSEntity.FinancialYr = Session["fin_yr"].ToString().Trim();

            if (ViewState["action"].ToString() == "add")
            {
                objIEBSEntity.Sch_id = 0;
                int retStatus = objIEBS.AddShedule(objIEBSEntity, comp_code);
                if (retStatus == 1)
                {
                    objCommon.DisplayUserMessage(updpan, "Schedule Saved Successfully", this.Page);
                }
            }
            else
            {
                objIEBSEntity.Sch_id = Convert.ToInt32(ViewState["SchID"].ToString());
                int retStatus = objIEBS.AddShedule(objIEBSEntity, comp_code);
                if (retStatus == 1)
                {
                    objCommon.DisplayUserMessage(updpan, "Schedule Updated Successfully", this.Page);
                }
            }
            PopulateSheduleList();
            clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACC_SchMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void lstSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsSchedule = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_SchMaster", "*", "", "Sch_ID=" + lstSchedule.SelectedValue.ToString(), "");
        ddlReportType.SelectedValue = dsSchedule.Tables[0].Rows[0]["BalanceSheet_IncomeExp"].ToString();
        if (ddlReportType.SelectedValue == "IE")
        {
            ddlSchType.Items.Clear();
            ListItem LSP = new ListItem("--Please Select--", "0");
            ddlSchType.Items.Add(LSP);
            ListItem LSI = new ListItem("INCOME", "I");
            ListItem LSE = new ListItem("EXPENDITURE", "E");
            ddlSchType.Items.Add(LSI);
            ddlSchType.Items.Add(LSE);


        }
        else if (ddlReportType.SelectedValue == "BS")
        {
            ddlSchType.Items.Clear();
            ListItem LSP = new ListItem("--Please Select--", "0");
            ddlSchType.Items.Add(LSP);
            ListItem LSI = new ListItem("ASSET", "A");
            ListItem LSE = new ListItem("LIABILITYIES", "L");
            ddlSchType.Items.Add(LSI);
            ddlSchType.Items.Add(LSE);
        }
        ddlSchType.SelectedValue = dsSchedule.Tables[0].Rows[0]["Sch_Type"].ToString();
        txtSchName.Text = dsSchedule.Tables[0].Rows[0]["Sch_name"].ToString();
        trLedgerSelection.Visible = true;
        ViewState["action"] = "Update";
        ViewState["SchID"] = lstSchedule.SelectedValue;
        lstSelectedLedger.Items.Clear();
        PopulateSelectedLedger();
        PopulateLedgerForSchedule();
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PARTY_NAME like '" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "PARTY_NAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstLedgerName.DataTextField = "PARTY_NAME";
                lstLedgerName.DataValueField = "PARTY_NO";
                lstLedgerName.DataSource = ds.Tables[0];
                lstLedgerName.DataBind();

            }
        }

        txtSearch.Focus();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstLedgerName.Items.Count; i++)
        {
            if (lstLedgerName.Items[i].Selected)
            {
                ListItem lsi = new ListItem(lstLedgerName.Items[i].Text, lstLedgerName.Items[i].Value);
                lstSelectedLedger.Items.Add(lsi);

                lstLedgerName.Items.Remove(lsi);
            }

        }

        //PopulateLedgerList();
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string a = lstSelectedLedger.SelectedItem.ToString();
        string b = lstSelectedLedger.SelectedItem.Value;
        lstSelectedLedger.Items.Remove(lstSelectedLedger.SelectedItem);
        ListItem lsi = new ListItem(a, b);
        lstLedgerName.Items.Add(lsi);


    }

    protected DataSet GetChildRecord(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }

    protected void InsertSubParentEntry(Int16 mgrpno, Int16 prono, Int16 partyno, DataSet dsSp, Int16 fano)
    {

        if (dsSp != null)
        {
            if (dsSp.Tables[0].Rows.Count > 0)
            {


                int k = 0;
                TrialBalanceReport oEntity = new TrialBalanceReport();
                //space1 = space1.ToString() + "  ";
                for (k = 0; k < dsSp.Tables[0].Rows.Count; k++)
                {
                    if (mgrpno == Convert.ToInt16(dsSp.Tables[0].Rows[k]["prno"]) && Convert.ToInt16(dsSp.Tables[0].Rows[k]["party_no"]) == 0)
                    {
                        oEntity.PartyName = space1.ToString() + dsSp.Tables[0].Rows[k]["PARTYNAME"].ToString().Trim();
                        oEntity.MGRPNO = Convert.ToInt16(dsSp.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim());
                        oEntity.PRNO = Convert.ToInt16(dsSp.Tables[0].Rows[k]["PRNO"].ToString().Trim());
                        oEntity.PARTYNO = Convert.ToInt16(dsSp.Tables[0].Rows[k]["PARTY_NO"].ToString().Trim());
                        oEntity.OPBALANCE = Convert.ToDouble(dsSp.Tables[0].Rows[k]["OP_BALANCE"].ToString().Trim());
                        oEntity.CLBALANCE = Convert.ToDouble(dsSp.Tables[0].Rows[k]["CL_BALANCE"].ToString().Trim());
                        oEntity.DEBIT = Convert.ToDouble(dsSp.Tables[0].Rows[k]["DEBIT"].ToString().Trim());
                        oEntity.CREDIT = Convert.ToDouble(dsSp.Tables[0].Rows[k]["CREDIT"].ToString().Trim());
                        oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                        oEntity.FANO = fano;// Convert.ToInt16(dsSp.Tables[0].Rows[k]["FA_NO"].ToString().Trim());
                        TrialBalanceReportController oTran = new TrialBalanceReportController();
                        oTran.AddTrialBalanceReportFormat(oEntity);

                        DataSet dsC = GetChildRecord(Convert.ToInt16(dsSp.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim()));
                        if (dsC != null)
                        {
                            if (dsC.Tables[0].Rows.Count > 0)
                            {
                                int x = 0;

                                TrialBalanceReport oEntity1 = new TrialBalanceReport();
                                space2 = space2.ToString() + "  ";
                                for (x = 0; x < dsC.Tables[0].Rows.Count; x++)
                                {


                                    oEntity1.PartyName = space2.ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                                    oEntity1.MGRPNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["MGRP_NO"].ToString().Trim());
                                    oEntity1.PRNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PRNO"].ToString().Trim());
                                    oEntity1.PARTYNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PARTY_NO"].ToString().Trim());
                                    oEntity1.OPBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["OP_BALANCE"].ToString().Trim());
                                    oEntity1.CLBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["CL_BALANCE"].ToString().Trim());
                                    oEntity1.DEBIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["DEBIT"].ToString().Trim());
                                    oEntity1.CREDIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["CREDIT"].ToString().Trim());
                                    oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                    oEntity1.FANO = fano;// Convert.ToInt16(dsC.Tables[0].Rows[x]["FA_NO"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity1);


                                }

                                space2 = "          ".ToString();



                            }

                        }


                    }


                }

                space1 = "      ".ToString();

            }
        }

    }

    protected void GenerateTrialBalanceFormatNew2()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "PARTYNAME <> ''", string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    TrialBalanceReport oEntity = new TrialBalanceReport();
                    int i = 0;

                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            space1 = space1.ToString() + "  ";
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {
                                    //oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.PartyName = space1.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);
                                    space1 = space1.ToString() + "  ";
                                    InsertSubParentEntry(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"].ToString().Trim()), Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim()), dsLdg, Convert.ToInt16(oEntity.FANO));
                                    DataSet dsC = GetChildRecord(Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim()));
                                    if (dsC != null)
                                    {
                                        if (dsC.Tables[0].Rows.Count > 0)
                                        {
                                            int x = 0;
                                            TrialBalanceReport oEntity1 = new TrialBalanceReport();
                                            space2 = space2.ToString() + "  ";
                                            for (x = 0; x < dsC.Tables[0].Rows.Count; x++)
                                            {

                                                // oEntity1.PartyName = "          ".ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                                                oEntity1.PartyName = space2.ToString() + dsC.Tables[0].Rows[x]["PARTYNAME"].ToString().Trim();
                                                oEntity1.MGRPNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["MGRP_NO"].ToString().Trim());
                                                oEntity1.PRNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PRNO"].ToString().Trim());
                                                oEntity1.PARTYNO = Convert.ToInt16(dsC.Tables[0].Rows[x]["PARTY_NO"].ToString().Trim());
                                                oEntity1.OPBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["OP_BALANCE"].ToString().Trim());
                                                oEntity1.CLBALANCE = Convert.ToDouble(dsC.Tables[0].Rows[x]["CL_BALANCE"].ToString().Trim());
                                                oEntity1.DEBIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["DEBIT"].ToString().Trim());
                                                oEntity1.CREDIT = Convert.ToDouble(dsC.Tables[0].Rows[x]["CREDIT"].ToString().Trim());
                                                oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                                oEntity1.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                                TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                                oTran2.AddTrialBalanceReportFormat(oEntity1);


                                            }
                                            space2 = "          ".ToString();

                                        }

                                    }

                                }
                                else
                                {
                                    if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["prno"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                    {
                                        TrialBalanceReport oEntity3 = new TrialBalanceReport();
                                        oEntity3.PartyName = space2.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                        //oEntity3.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                        oEntity3.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                        oEntity3.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                        oEntity3.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                        oEntity3.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                        oEntity3.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                        oEntity3.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                        oEntity3.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                        oEntity3.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                        oEntity3.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                        TrialBalanceReportController oTran3 = new TrialBalanceReportController();
                                        oTran3.AddTrialBalanceReportFormat(oEntity3);

                                    }

                                }


                            }
                            space1 = "     ".ToString();

                        }




                    }


                }


            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    protected void UpdateTotalForLedgerNew()
    {
        try
        {

            DataSet dtl = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", string.Empty, string.Empty);
            if (dtl != null)
            {

                if (dtl.Tables[0].Rows.Count > 0)
                {

                    int i = 0;
                    for (i = 0; i < dtl.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"].ToString().Trim()) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim()) == 0)
                        {
                            //call 

                            TrialBalanceReportController tbrc = new TrialBalanceReportController();
                            DataSet dsres = tbrc.GetLedgerTotal(Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"].ToString().Trim()));
                            if (dsres != null)
                            {
                                int CID = 0;
                                if (dsres.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt16(dsres.Tables[0].Rows[1]["CNT"]) > 0)
                                    {

                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[0]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[0]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[0]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[0]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[1]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);
                                    }
                                    else
                                    {
                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[1]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[1]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[1]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[1]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[0]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);
                                    }


                                }


                            }



                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.UpdateTotalForLedgerNew -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    protected void UpdateTotalForMainLedger()
    {
        try
        {

            DataSet dtl = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", string.Empty, string.Empty);
            if (dtl != null)
            {

                if (dtl.Tables[0].Rows.Count > 0)
                {

                    int i = 0;
                    for (i = 0; i < dtl.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"].ToString().Trim()) == 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim()) == 0)
                        {
                            //call 

                            TrialBalanceReportController tbrc = new TrialBalanceReportController();
                            DataSet dsres = tbrc.GetMainLedgerTotal(Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim()));
                            if (dsres != null)
                            {
                                int CID = 0;
                                if (dsres.Tables[0].Rows.Count > 1)
                                {
                                    if (Convert.ToInt16(dsres.Tables[0].Rows[0]["CNT"]) > 0)
                                    {


                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[1]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[1]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[1]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[1]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[0]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);
                                    }
                                    else
                                    {
                                        double OP = Convert.ToDouble(dsres.Tables[0].Rows[0]["OP"]);
                                        double CL = Convert.ToDouble(dsres.Tables[0].Rows[0]["CL"]);
                                        double DR = Convert.ToDouble(dsres.Tables[0].Rows[0]["DR"]);
                                        double CR = Convert.ToDouble(dsres.Tables[0].Rows[0]["CR"]);
                                        CID = Convert.ToInt16(dsres.Tables[0].Rows[1]["CNT"]);
                                        TrialBalanceReportController tbc = new TrialBalanceReportController();
                                        tbc.UpdateAllTrialBalanceAmount(CID, CR, DR, OP, CL);

                                    }

                                }


                            }



                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TrialBalanceReport.UpdateTotalForMainLedger -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstSelectedLedger.Items.Count > 0)
            {
                bool IsFalse = true;
                TrialBalanceReportController od = new TrialBalanceReportController();
                od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
                od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy"), Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy"));
                //GenerateTrialBalanceFormat("N");
                GenerateTrialBalanceFormatNew2();
                od.OrderTrialBalanceReport();
                UpdateTotalForLedgerNew();
                UpdateTotalForMainLedger();
                od.DeleteTrialBalanceZEROAmount();


                objIEBS.DeleteLedger(Convert.ToInt32(ViewState["SchID"].ToString()), Session["comp_code"].ToString());

                for (int i = 0; i < lstSelectedLedger.Items.Count; i++)
                {
                    string[] ledgerName = lstSelectedLedger.Items[i].Text.Split('[');

                    objIEBSEntity.Sch_id = Convert.ToInt32(ViewState["SchID"].ToString());

                    objIEBSEntity.LedgerName = ledgerName[0].Trim();
                    objIEBSEntity.Party_NO = Convert.ToInt32(lstSelectedLedger.Items[i].Value);
                    string comp_code = Session["comp_code"].ToString().Trim();
                    objIEBSEntity.FinancialYr = Session["fin_yr"].ToString().Trim();
                    objIEBSEntity.User_Id = Session["userno"].ToString().Trim();
                    objIEBSEntity.College_Code = Session["colcode"].ToString();
                    int ret = objIEBS.AddLedgerForSchedule(objIEBSEntity, comp_code);
                    if (ret != 1)
                    {
                        IsFalse = false;
                    }

                }

                if (IsFalse != false)
                {
                    objCommon.DisplayUserMessage(updpan, "Ledger Mapped Successfully", this.Page);

                    clear();
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updpan, "Ledger Not Available To Map", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACC_SchMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void btnCancelLedger_Click(object sender, EventArgs e)
    {
        lstSelectedLedger.Items.Clear();
        PopulateLedgerForSchedule();
    }

    protected void txtSchName_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < lstSchedule.Items.Count; i++)
        {
            if (txtSchName.Text == lstSchedule.Items[i].Text)
            {
                objCommon.DisplayUserMessage(updpan, "Schedule Name Is Already Exist", this.Page);
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
