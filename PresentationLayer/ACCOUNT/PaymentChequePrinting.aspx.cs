//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYMENT CHEQUE PRINTING
// CREATION DATE : 30-04-2010                                               
// CREATED BY    : JITENDRA CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using IITMS.NITPRM;


public partial class PaymentChequePrinting : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    public static DataTable dt1 = new DataTable();
    string back = string.Empty;
    string space1 = "     ".ToString();
    string space2 = "          ".ToString();
    string space3 = string.Empty;
    DataTable dt = new DataTable();
    public static int RowIndex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["WithoutCashBank"] = "N";
        btnGo.Attributes.Add("onClick", "return CheckFields();");
        if (!Page.IsPostBack)
        {
            SetDataColumn();
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
                    ViewState["action"] = "add";
                }
            }
            SetFinancialYear();
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
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();


    }
    private void SetDataColumn()
    {

        DataColumn dc = new DataColumn();
        dc.ColumnName = "Date";
        dt.Columns.Add(dc);

        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "Particulars";
        dt.Columns.Add(dc1);

        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "VchType";
        dt.Columns.Add(dc2);


        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "VchNo";
        dt.Columns.Add(dc3);


        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "Debit";
        dt.Columns.Add(dc4);

        DataColumn dc5 = new DataColumn();
        dc5.ColumnName = "Credit";
        dt.Columns.Add(dc5);

        DataColumn dc6 = new DataColumn();
        dc6.ColumnName = "PartyNo";
        dt.Columns.Add(dc6);

        DataColumn dc7 = new DataColumn();
        dc7.ColumnName = "ReconcileDate";
        dt.Columns.Add(dc7);

        DataColumn dc8 = new DataColumn();
        dc8.ColumnName = "ChqNo";
        dt.Columns.Add(dc8);

        Session["DatatableMod"] = dt;


    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaymentChequePrinting.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaymentChequePrinting.aspx");
        }
    }


    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (Session["DatatableMod"] != null)
        {
            dt = Session["DatatableMod"] as DataTable;
            dt.Clear();
            Session["DatatableMod"] = dt;

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
        AddGridEntry();
        dt = Session["DatatableMod"] as DataTable;
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                DataTable dtTemp = Session["VchDatatable"] as DataTable;
                DataView dvt = dtTemp.DefaultView;
                // dvt.Sort = "VOUCHER_NO";
                dvt.RowFilter = "Transaction_Type in ('Payment','Contra')";
                DataTable dtTemp1 = dvt.ToTable();
                dtTemp = dvt.ToTable();
                dtTemp.AcceptChanges();
                //-----------------------------
                DataTable ResultentDt = new DataTable();
                DataTable dtVno = new DataTable();
                dtVno.Columns.Add("voucher_no");
                dtVno.Columns.Add("transaction_type");
                dtVno.Columns.Add("STR_VOUTCHER_NO");
                //dtVno = dtTemp;
                DataView dvfilter = dtTemp.DefaultView;

                RptData.DataSource = dtTemp;
                //---------------------------------------------------

                //RptData.DataSource = dtTemp;
                RptData.DataBind();
                if (dtTemp.Rows.Count == 0)
                {
                    objCommon.DisplayMessage(UPDLedger, "No Cheque Available At This Moment.", this);
                    return;
                }
                int i = 0;
                //====change dtTemp
                for (i = 0; i < dtTemp.Rows.Count; i++)//RptData.Items.Count; i++)
                {
                    DataView dv = new DataView();
                    dv = dt.DefaultView;
                    //dv.Sort = "VchNo";
                    //dv.RowFilter = "VchNo=" + dtVno.Rows[i]["VOUCHER_NO"].ToString().Trim() + " and ChqNo=0";
                    string a = "VchNo='" + dtTemp.Rows[i]["VOUCHER_NO"].ToString().Trim() + "' and VchType='" + dtTemp.Rows[i]["transaction_type"].ToString().ToUpper() + "'";
                    dv.RowFilter = a;
                    DataTable dtContain = dv.ToTable();
                    ListView grd = RptData.Items[i].FindControl("lvGrp") as ListView;
                    if (grd != null)
                    {
                        grd.DataSource = dtContain;
                        grd.DataBind();

                        if (grd.Items.Count > 0)
                        {
                            int c = 0;
                            for (c = 0; c < grd.Items.Count; c++)
                            {
                                LinkButton lnkledger = grd.Items[c].FindControl("lnkledger") as LinkButton;
                                HiddenField hdnparty = grd.Items[c].FindControl("hdnparty") as HiddenField;

                                if (lnkledger != null)
                                {
                                    if (dtContain.Rows[c]["VchType"].ToString().ToLower() == "payment")
                                    {
                                        string party = txtAcc.Text.ToString().Split('*')[0].ToString();
                                        lnkledger.Attributes.Add("onClick", "return ShowChequePrinting('" + dtContain.Rows[c]["Date"].ToString().Trim() + "','" + dtContain.Rows[c]["TRANSACTION_NO"].ToString().Trim() + "','" + party + "','" + dtContain.Rows[c]["Credit"].ToString().Trim() + "','" + hdnparty.Value.ToString().Trim() + "','" + dtContain.Rows[c]["ChqNo"].ToString().Trim() + "');");
                                    }
                                    else if (dtContain.Rows[c]["VchType"].ToString().ToLower() == "contra" && dtContain.Rows[c]["Credit"].ToString().Trim() != "")
                                    {
                                        string party = txtAcc.Text.ToString().Split('*')[0].ToString();
                                        lnkledger.Attributes.Add("onClick", "return ShowChequePrinting('" + dtContain.Rows[c]["Date"].ToString().Trim() + "','" + dtContain.Rows[c]["TRANSACTION_NO"].ToString().Trim() + "','" + party + "','" + dtContain.Rows[c]["Credit"].ToString().Trim() + "','" + hdnparty.Value.ToString().Trim() + "','" + dtContain.Rows[c]["ChqNo"].ToString().Trim() + "');");
                                    }
                                    else if (dtContain.Rows[c]["VchType"].ToString().ToLower() == "contra" && dtContain.Rows[c]["Debit"].ToString().Trim() != "")
                                    {
                                        string party = txtAcc.Text.ToString().Split('*')[0].ToString();
                                        lnkledger.Attributes.Add("onClick", "return ShowChequePrinting('" + dtContain.Rows[c]["Date"].ToString().Trim() + "','" + dtContain.Rows[c]["TRANSACTION_NO"].ToString().Trim() + "','" + party + "','" + dtContain.Rows[c]["Debit"].ToString().Trim() + "','" + hdnparty.Value.ToString().Trim() + "','" + dtContain.Rows[c]["ChqNo"].ToString().Trim() + "');");
                                    }
                                }
                            }
                        }
                    }


                }

                double dr = 0;
                double cr = 0;
                int l = 0;
                for (l = 0; l < dt.Rows.Count; l++)
                {
                    if (dt.Rows[l]["Debit"].ToString().Trim() != "")
                    {
                        dr = dr + Convert.ToDouble(dt.Rows[l]["Debit"]);
                    }
                    else
                    {
                        cr = cr + Convert.ToDouble(dt.Rows[l]["Credit"]);
                    }
                }
            }
        }
    }

    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }

    private void AddGridEntry()
    {
        dt = Session["DatatableMod"] as DataTable;
        if (dt.Columns.Contains("TRANSACTION_NO"))
        { }
        else
            dt.Columns.Add("TRANSACTION_NO", typeof(int));
        DataRow row;
        string[] PartyId = txtAcc.Text.ToString().Trim().Split('*');
        string partyId1 = string.Empty;

        partyId1 = objCommon.FillDropDown("acc_" + Session["comp_code"] + "_party", "PARTY_NO", "PARTY_NO", "ACC_CODE='" + PartyId[1].ToString().Trim() + "'", "").Tables[0].Rows[0][0].ToString();
        int id = 0;
        if (PartyId != null)
        {
            if (IsNumeric(partyId1.Trim()) == false)
            {

                objCommon.DisplayMessage(UPDLedger, "Invalid Ledger No.", this);
                txtAcc.Focus();
                return;
            }



            id = Convert.ToUInt16(partyId1.Trim());

            //=============new opening balance============


            DataSet dso = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_trans", "amount", "[tran]", "party_no=" + id.ToString().Trim() + " and transaction_type='OB' and transaction_date >= '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "'", "party_no");
            if (dso != null)
            {
                if (dso.Tables[0].Rows.Count > 0)
                {
                    if (dso.Tables[0].Rows[0][1].ToString().Trim() == "Cr")
                    {

                        //lblOb.Text = dso.Tables[0].Rows[0]["Amount"].ToString().Trim();

                    }
                    else
                    {
                        //lblOb.Text = dso.Tables[0].Rows[0]["Amount"].ToString().Trim();
                        //if (dso.Tables[0].Rows[0]["Amount"].ToString().Trim().IndexOf('-') == -1)
                        //{
                        //    lblOb.Text = "-" + dso.Tables[0].Rows[0]["Amount"].ToString().Trim();
                        //}
                        //else
                        //{
                        //    lblOb.Text = dso.Tables[0].Rows[0]["Amount"].ToString().Trim();
                        //}
                    }


                }
                else
                {
                    TrialBalanceReportController otr = new TrialBalanceReportController();

                    DateTime frmdate = Convert.ToDateTime(txtFrmDate.Text);
                    frmdate = frmdate.AddDays(-1);
                    DataSet dsOp = otr.GetOpeningBalance(Session["comp_code"].ToString().ToString().Trim(), Convert.ToDateTime(frmdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(frmdate).ToString("dd-MMM-yyyy"), Convert.ToInt16(id.ToString().Trim()));
                    if (dsOp != null)
                    {
                        if (dsOp.Tables[0].Rows.Count > 0)
                        {
                            //lblOb.Text = dsOp.Tables[0].Rows[0][0].ToString().Trim();
                        }
                        else
                        {
                            //lblOb.Text = "0";

                        }

                    }
                    else
                    {
                        //lblOb.Text = "0";
                    }


                }

            }
            else
            {

                TrialBalanceReportController otr = new TrialBalanceReportController();

                DateTime frmdate = Convert.ToDateTime(txtFrmDate.Text);
                frmdate = frmdate.AddDays(-1);
                DataSet dsOp = otr.GetOpeningBalance(Session["comp_code"].ToString().ToString().Trim(), Convert.ToDateTime(frmdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(frmdate).ToString("dd-MMM-yyyy"), Convert.ToInt16(id.ToString().Trim()));
                if (dsOp != null)
                {
                    if (dsOp.Tables[0].Rows.Count > 0)
                    {
                        //lblOb.Text = dsOp.Tables[0].Rows[0][0].ToString().Trim();
                    }
                    else
                    {
                        //lblOb.Text = "0";

                    }

                }
                else
                {
                    //lblOb.Text = "0";
                }


            }



            //=============================================

            //DataSet dso = objCommon.FillDropDown("ACC_"+ Session["comp_code"].ToString() + "_PARTY", "*", "", "party_no=" + id.ToString().Trim(), "party_no");
            //if (dso.Tables[0].Rows.Count > 0)
            //{
            //    if (dso.Tables[0].Rows[0]["STATUS"].ToString().Trim() == "D")
            //    {
            //        if (dso.Tables[0].Rows[0]["OPBALANCE"].ToString().Trim() == "")
            //        {
            //            lblOb.Text = Convert.ToString(-Convert.ToDouble(0)).ToString().Trim();
            //        }
            //        else
            //        {
            //            lblOb.Text = Convert.ToString(-Convert.ToDouble(dso.Tables[0].Rows[0]["OPBALANCE"])).ToString().Trim();  

            //        }




            //    }
            //    else
            //    {
            //        lblOb.Text = dso.Tables[0].Rows[0]["OPBALANCE"].ToString().Trim();

            //    }


            //}




        }

        //DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "*", "", "party_no=" + id.ToString().Trim() + " and transaction_type <> 'OB' and isnull(CHQ_NO,0)=0 and transaction_date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "'", "VOUCHER_NO");
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "*", "", "party_no=" + id.ToString().Trim() + " and transaction_type <> 'OB' and transaction_date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "'", "VOUCHER_NO");
        if (ds == null)
        {
            ClearRecord();
            txtAcc.Focus();
            objCommon.DisplayMessage("Record Not Available.", this);
            return;

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            ClearRecord();
            txtAcc.Focus();
            objCommon.DisplayMessage("Record Not Available.", this);
            return;

        }
        AccountTransactionController actr = new AccountTransactionController();


        if (ds != null)
        {
            DataSet dsvch = actr.GetVoucherByCheakPrinting(Session["comp_code"].ToString().Trim(), id.ToString().Trim(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));

            // DataSet dsvch = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "DISTINCT VOUCHER_NO", "Transaction_Type", "party_no=" + id.ToString().Trim() + " and transaction_type <> 'OB' and isnull(CHQ_NO,0)=0   and transaction_date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "'", string.Empty);

            Session["VchDatatable"] = null;
            if (dsvch != null)
            {
                if (dsvch.Tables[0].Rows.Count != 0)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "STR_VOUTCHER_NO";
                    dsvch.Tables[0].Columns.Add(dc);
                    int y = 0;
                    for (y = 0; y < dsvch.Tables[0].Rows.Count; y++)
                    {
                        if (dsvch.Tables[0].Rows[y]["Transaction_type"].ToString().Trim() == "R")
                        {
                            dsvch.Tables[0].Rows[y]["Transaction_type"] = "Receipt";

                        }
                        else if (dsvch.Tables[0].Rows[y]["Transaction_type"].ToString().Trim() == "P")
                        {
                            dsvch.Tables[0].Rows[y]["Transaction_type"] = "Payment";

                        }
                        else if (dsvch.Tables[0].Rows[y]["Transaction_type"].ToString().Trim() == "C")
                        {
                            dsvch.Tables[0].Rows[y]["Transaction_type"] = "Contra";

                        }
                        else
                        {
                            dsvch.Tables[0].Rows[y]["Transaction_type"] = "Journal";

                        }
                        //dsvch.Tables[0].Rows[y]["STR_VOUTCHER_NO"] = Session["comp_code"].ToString().Trim() + dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                        dsvch.Tables[0].Rows[y]["STR_VOUTCHER_NO"] = dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                        dsvch.Tables[0].Rows[y].AcceptChanges();

                    }


                    Session["VchDatatable"] = dsvch.Tables[0];



                }




            }



            int i = 0;
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["OPARTY"].ToString().Trim() != "")
                {

                    string[] oPartyId = ds.Tables[0].Rows[i]["OPARTY"].ToString().Trim().Split(',');
                    if (oPartyId != null)
                    {
                        int j = 0;
                        for (j = 0; j < oPartyId.Length; j++)
                        {

                            string Ledgers = string.Empty;
                            string Date = string.Empty;
                            string VchType = string.Empty;
                            double Debit = 0.00;
                            double Credit = 0.00;
                            string VchNo = "";
                            AccountTransactionController objRet = new AccountTransactionController();
                            DataSet ds1 = new DataSet();
                            if (oPartyId[j].ToString().Trim() != "" || oPartyId[j].ToString().Trim() != "0")
                            {
                                ds1 = objRet.GetTransactionForModification(Convert.ToInt16(oPartyId[j]), ds.Tables[0].Rows[i]["VOUCHER_NO"].ToString(), Session["comp_code"].ToString(), ds.Tables[0].Rows[i]["Transaction_type"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
                            }





                            // DataSet ds1 = objCommon.FillDropDown("ACC_TRANS_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "*", "", "party_no=" + oPartyId[j] + "  and transaction_type <> 'OB' and  VOUCHER_NO=" + ds.Tables[0].Rows[i]["VOUCHER_NO"], "VOUCHER_NO");
                            if (ds1 != null)
                            {

                                if (ds1.Tables[0].Rows.Count > 0)
                                {

                                    int k = 0;

                                    DataView dvTemp = new DataView();
                                    dvTemp = dt.DefaultView;
                                    dvTemp.RowFilter = "VchNo='" + ds1.Tables[0].Rows[k]["VOUCHER_NO"].ToString().Trim() + "'";
                                    DataTable dtTemp = dvTemp.ToTable();


                                    DataView dv = new DataView(dtTemp, "Particulars <>''", "Particulars", DataViewRowState.CurrentRows);

                                    DataRowView[] drv = dv.FindRows(ds1.Tables[0].Rows[0]["LEDGER"].ToString().Trim());



                                    if (drv.Length == 0)
                                    {

                                        for (k = 0; k < ds1.Tables[0].Rows.Count; k++)
                                        {

                                            row = dt.NewRow();
                                            Date = Convert.ToDateTime(ds1.Tables[0].Rows[k]["TRANSACTION_DATE"]).ToString("dd/MM/yyyy");
                                            VchType = ds1.Tables[0].Rows[k]["Vch_Type"].ToString().Trim();
                                            VchNo = ds1.Tables[0].Rows[k]["VOUCHER_NO"].ToString().Trim();
                                            //here intentionally reverse the Credit-Debit for showing purpose

                                            if (ds1.Tables[0].Rows[k]["DEBIT"].ToString().Trim() == "0.00")
                                            {

                                                Credit = Convert.ToDouble(ds1.Tables[0].Rows[k]["CREDIT"].ToString().Trim());
                                                row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(Credit.ToString().Trim()));
                                                row["Credit"] = "";



                                            }
                                            else
                                            {



                                                Debit = Convert.ToDouble(ds1.Tables[0].Rows[k]["DEBIT"].ToString().Trim());

                                                row["Debit"] = "";
                                                row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(Debit.ToString().Trim()));

                                            }


                                            Ledgers = ds1.Tables[0].Rows[k]["LEDGER"].ToString().Trim() + "  " + "<br />" + ds1.Tables[0].Rows[k]["parti"].ToString().Trim();




                                            row["Date"] = Date.ToString().Trim();
                                            row["Particulars"] = Ledgers.ToString().Trim();
                                            row["VchType"] = VchType;



                                            row["VchNo"] = VchNo.ToString().Trim();

                                            row["PartyNo"] = ds1.Tables[0].Rows[k]["Party_No"].ToString();
                                            row["TRANSACTION_NO"] = ds1.Tables[0].Rows[k]["TRANSACTION_NO"].ToString();
                                            int rec_flag = 0;
                                            //string Recdate
                                            rec_flag = Convert.ToInt16(ds1.Tables[0].Rows[k]["rec_no"]);//.ToString().Substring(10).ToString().Trim();
                                            //if (rec_flag == 1)
                                            //{
                                            //    row["ReconcileDate"] = ds1.Tables[0].Rows[k]["Recon_date"].ToString().Trim().Substring(0, 11);
                                            //}
                                            //else
                                            //{
                                            //    row["ReconcileDate"] = "";
                                            //}
                                            if (rec_flag == 1)
                                            {
                                                if (ds1.Tables[0].Rows[k]["Recon_date"].ToString().Trim() == "")
                                                    row["ReconcileDate"] = ds1.Tables[0].Rows[k]["Recon_date"].ToString().Trim();
                                                else
                                                    row["ReconcileDate"] = ds1.Tables[0].Rows[k]["Recon_date"].ToString().Trim().Substring(0, 11);
                                            }
                                            else
                                            {
                                                row["ReconcileDate"] = "";
                                            }

                                            row["ChqNo"] = ds1.Tables[0].Rows[k]["chq_no"].ToString().Trim();

                                            var foundAuthors = dt.Select("TRANSACTION_NO = '" + ds1.Tables[0].Rows[k]["TRANSACTION_NO"].ToString() + "'");
                                            if (foundAuthors.Length == 0)
                                            {
                                                dt.Rows.Add(row);
                                            }


                                            Session["DatatableMod"] = dt;

                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ClearRecord()
    {
        // SetDataColumn();
        Session["VchDatatable"] = null;
        RptData.DataSource = null;
        RptData.DataBind();
        txtAcc.Focus();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAcc.Text = "";
        SetDataColumn();
        Session["VchDatatable"] = null;
        RptData.DataSource = null;
        RptData.DataBind();
        SetFinancialYear();
        txtAcc.Focus();
    }

    protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label lbvch = e.Item.FindControl("lblvchtype") as Label;
        if (lbvch != null)
        {
            string cmd = e.CommandArgument.ToString();
            if (e.CommandName.ToString().Trim() == "VoucherPrint")
            {
                if (lbvch.Text.ToString().Trim() == "Payment" || lbvch.Text.ToString().Trim() == "Receipt")
                {
                    ShowVoucherPrintReport("Voucher", "PmtRcptCashVoucherRpt.rpt", lbvch.Text.ToString().Trim(), cmd);
                }
                else
                {
                    ShowVoucherPrintReport("Voucher", "JvContraVoucherReport.rpt", lbvch.Text.ToString().Trim(), cmd);
                }

            }
            else
            {
                Response.Redirect("AccountingVouchers.aspx?obj=config," + cmd.ToString().Trim() + "," + lbvch.Text.ToString().Trim() + "," + txtFrmDate.Text.ToString().Trim() + "," + txtUptoDate.Text.ToString().Trim());

            }

        }
    }

    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));


            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_VCH_NO=" + VchNo.ToString().Trim() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + VchNo;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() != "")
        {
            btnGo_Click(sender, e);
        }
        txtAcc.Focus();

    }

    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() != "")
        {
            btnGo_Click(sender, e);
        }
        txtUptoDate.Focus();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        btnCancel_Click(sender, e);
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetLegerName(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetChequePrintingData(prefixText);
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


