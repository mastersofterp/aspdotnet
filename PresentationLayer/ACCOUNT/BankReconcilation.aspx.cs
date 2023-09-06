//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : BANK RECONCILATION
// CREATION DATE : 26-04-2010                                               
// CREATED BY    : JITENDRA CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

using System.Collections.Generic;

public partial class BankReconcilation : System.Web.UI.Page
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

        if (hdnBal.Value != "")
        {

            lblCurBal.Text = hdnBal.Value;
            // lblmode.Text = hdnMode.Value;
        }

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

                    Page.Title = Session["coll_name"].ToString();


                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    //PopulateDropDown();
                    //PopulateListBox();

                    ViewState["action"] = "add";
                }



            }

            trGrid.Visible = false;
            trBalances.Visible = false;
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
            //txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            //txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");

            string Fdate = DateTime.Now.ToString();
            Fdate = "01/" + DateTime.Parse(Fdate).Month.ToString() + "/" + DateTime.Parse(Fdate).Year.ToString();
            txtFrmDate.Text = Convert.ToDateTime(Fdate).ToString("dd/MM/yyyy");

            if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
            {
                txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                txtFrmDate.Focus();
                Fdate = txtFrmDate.Text;
            }

            string Todate = DateTime.Parse(Fdate).AddMonths(1).ToString();
            Todate = DateTime.Parse(Todate).AddDays(-1).ToString();
            txtUptoDate.Text = Convert.ToDateTime(Todate).ToString("dd/MM/yyyy");
            ViewState["Todate"] = txtUptoDate.Text;

            if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
                txtUptoDate.Focus();
                ViewState["Todate"] = txtUptoDate.Text;
            }
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

        Session["DatatableMod"] = dt;


    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BankReconcilation.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BankReconcilation.aspx");
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (txtAcc.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter Ledger Name", this);
            return;
        }

        if (txtAcc.Text.Split('*').Length > 1)
        {
            trGrid.Visible = true;
            trBalances.Visible = true;
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

            lblCr.Text = "0.00";
            lblDr.Text = "0.00";
            lblOb.Text = "0.00";
            lblclose.Text = "0.00";
            lblmode.Text = "Dr";

            //AddGridEntry();
            //GetBankBookBalance();

            DataSet ds = null;
            TrialBalanceReportController objTBR = new TrialBalanceReportController();

            ds = objTBR.GetBankReconciledData(Session["comp_code"].ToString(), Convert.ToInt32(txtAcc.Text.Split('*')[1]), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstData.DataSource = ds.Tables[0];
                lstData.DataBind();
                trButton.Visible = true;
            }


            dt = Session["DatatableMod"] as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {



                    //RptData.DataSource = Session["VchDatatable"] as DataTable;
                    //RptData.DataBind();

                    DataTable dtTemp = Session["VchDatatable"] as DataTable;
                    DataView dvt = dtTemp.DefaultView;
                    dvt.Sort = "transaction_date1";
                    //dvt.RowFilter = "Transaction_Type='Payment'";
                    DataTable dtTemp1 = dvt.ToTable();
                    dtTemp = dvt.ToTable();
                    dtTemp.AcceptChanges();
                    if (dtTemp.Rows.Count > 0)
                    {
                        trButton.Visible = true;
                        RptData.DataSource = dtTemp;
                        RptData.DataBind();
                        RptHeading.Visible = true;
                        int c = 0;
                        for (c = 0; c < RptData.Items.Count; c++)
                        {

                            TextBox txtbnkdate = RptData.Items[c].FindControl("txtbankdate") as TextBox;
                            Image imgbutton = RptData.Items[c].FindControl("imgCal10") as Image;
                            Image imgEdit = RptData.Items[c].FindControl("btnEdit") as Image;
                            if (txtbnkdate != null)
                            {
                                if (txtbnkdate.Text.ToString().Trim() != "")
                                {
                                    txtbnkdate.Enabled = false;
                                    imgbutton.Visible = false;
                                    imgEdit.Visible = true;
                                }
                                else
                                {
                                    txtbnkdate.Enabled = true;
                                    imgbutton.Visible = true;
                                    imgEdit.Visible = false;
                                }

                            }

                        }


                    }
                    else
                    {
                        objCommon.DisplayMessage(UPDLedger, "Record Not Available", this);
                        btnGo.Focus();
                        return;

                    }


                    //DataTable dtTemp=Session["VchDatatable"] as DataTable;
                    //DataView dvt = dtTemp.DefaultView;
                    //dvt.Sort="VOUCHER_NO";
                    //DataTable dtTemp1 = dvt.ToTable();
                    //dtTemp = dvt.ToTable();
                    //dtTemp.AcceptChanges();
                    int i = 0;
                    for (i = 0; i < dtTemp.Rows.Count; i++)
                    {

                        DataView dv = new DataView();

                        dv = dt.DefaultView;
                        dv.RowFilter = "";
                        //dv.RowFilter = "VchNo=" + dtTemp.Rows[i]["VOUCHER_NO"].ToString().Trim();
                        string a = "VchNo='" + dtTemp.Rows[i]["VOUCHER_NO"].ToString().Trim() + "' and VchType='" + dtTemp.Rows[i]["Transaction_Type"].ToString().Trim().ToUpper() + "'";
                        // dv.RowFilter = "VchNo=" + dtTemp.Rows[i]["VOUCHER_NO"].ToString().Trim();
                        dv.RowFilter = a;
                        DataTable dtContain = dv.ToTable();
                        ListView grd = RptData.Items[i].FindControl("lvGrp") as ListView;
                        if (grd != null)
                        {
                            grd.DataSource = dtContain;
                            grd.DataBind();

                            if (grd.Items.Count > 0)
                            {
                                // Commented By shrikant Ambone.

                                //int c = 0;
                                //for (c = 0; c < grd.Items.Count; c++)
                                //{

                                //   TextBox txtbnkdate= grd.Items[c].FindControl("txtbankdate") as TextBox;
                                //   Image imgbutton = grd.Items[c].FindControl("imgCal10") as Image;
                                //   if (txtbnkdate != null)
                                //   {
                                //       if (txtbnkdate.Text.ToString().Trim() != "")
                                //       {
                                //           txtbnkdate.Enabled = false;
                                //           imgbutton.Visible = false;

                                //       }
                                //       else
                                //       {
                                //           txtbnkdate.Enabled = true;
                                //           imgbutton.Visible = true;
                                //       }

                                //   }

                                //}


                            }


                        }


                    }

                    double dr = 0;
                    double cr = 0;
                    int l = 0;
                    DataTable dt1 = new DataTable();
                    for (int j1 = 0; j1 < dt.Columns.Count; j1++)
                    {
                        dt1.Columns.Add(dt.Columns[j1].ToString());
                    }
                    //dt1.Rows.Clear();
                    for (l = 0; l < dt.Rows.Count; l++)
                    {
                        if (dt.Rows[l]["Debit"].ToString().Trim() != "")
                        {
                            dr = dr + Convert.ToDouble(dt.Rows[l]["Debit"]);
                        }
                        else
                        {

                            cr = cr + Convert.ToDouble(dt.Rows[l]["Credit"]);

                            dt1.Rows.Add(dt.Rows[l][0], dt.Rows[l][1], dt.Rows[l][2], dt.Rows[l][3], dt.Rows[l][4], dt.Rows[l][5], dt.Rows[l][6], dt.Rows[l][7]);

                        }

                    }


                    lblCr.Text = String.Format("{0:0.00}", Convert.ToDouble(cr.ToString().Trim()));
                    lblDr.Text = String.Format("{0:0.00}", Convert.ToDouble(dr.ToString().Trim()));

                    if (lblOb.Text.ToString().Trim() == "")
                    {
                        lblOb.Text = "0.00";

                    }
                    if (lblCr.Text.ToString().Trim() == "")
                    {
                        lblCr.Text = "0.00";

                    }
                    if (lblDr.Text.ToString().Trim() == "")
                    {
                        lblDr.Text = "0.00";

                    }
                    //lblclose.Text = String.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(lblOb.Text) + (Convert.ToDouble(lblCr.Text) - Convert.ToDouble(lblDr.Text))).ToString().Trim()));     //lblCurBal.Text.ToString().Trim(); //
                    //lblclose.Text = String.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(lblOb.Text) + (Convert.ToDouble(lblDr.Text) - Convert.ToDouble(lblCr .Text))).ToString().Trim()));
                    if (hdnMode.Value.ToString().Trim() == "Dr")
                    {
                        lblmode.Text = "Dr";

                    }
                    else
                    {
                        lblmode.Text = "Cr";
                    }


                }

            }

        }
        else
        {

        }
    }

    private void GetBankBookBalance()
    {
        DataSet _BankBalance = null;
        string[] PartyId = txtAcc.Text.ToString().Trim().Split('*');
        int id = 0;
        string partyno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + PartyId[1] + "'");
        id = Convert.ToUInt16(partyno);
        TrialBalanceReportController otr = new TrialBalanceReportController();
        _BankBalance = otr.GetBankClosingBalance(Session["comp_code"].ToString().ToString().Trim(), Convert.ToDateTime(txtUptoDate.Text).AddDays(1).ToString("dd-MMM-yyyy"), Convert.ToInt16(id.ToString().Trim()));
        if (_BankBalance != null)
        {
            if (_BankBalance.Tables[0] != null)
            {
                if (_BankBalance.Tables[0].Rows.Count > 0)
                {
                    if (_BankBalance.Tables[0].Rows[0][0].ToString() != "")
                    {
                        lblclose.Text = _BankBalance.Tables[0].Rows[0][0].ToString().Trim();
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
        //if (txtAcc.Text == string.Empty)
        //{

        //}
        //else
        //{
        dt = Session["DatatableMod"] as DataTable;
        DataRow row;
        string[] PartyId = txtAcc.Text.ToString().Trim().Split('*');
        string partyno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + PartyId[1] + "'");
        int id = 0;
        if (PartyId != null)
        {
            if (IsNumeric(partyno) == false)
            {
                objCommon.DisplayMessage(UPDLedger, "Invalid Ledger No.", this);
                txtAcc.Focus();
                return;
            }

            id = Convert.ToUInt16(partyno);

            //=============new opening balance============


            DataSet dso = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_trans", "amount", "[tran]", "party_no=" + id.ToString().Trim() + " and transaction_type='OB' and transaction_date >= '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "'", "party_no");
            if (dso != null)
            {
                if (dso.Tables[0].Rows.Count > 0)
                {
                    if (dso.Tables[0].Rows[0][1].ToString().Trim() == "Cr")
                    {

                        lblOb.Text = dso.Tables[0].Rows[0]["Amount"].ToString().Trim();

                    }
                    else
                    {
                        lblOb.Text = dso.Tables[0].Rows[0]["Amount"].ToString().Trim();
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
                            lblOb.Text = dsOp.Tables[0].Rows[0][0].ToString().Trim();
                        }
                        else
                        {
                            lblOb.Text = "0";

                        }

                    }
                    else
                    {
                        lblOb.Text = "0";
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
                        lblOb.Text = dsOp.Tables[0].Rows[0][0].ToString().Trim();
                    }
                    else
                    {
                        lblOb.Text = "0";

                    }

                }
                else
                {
                    lblOb.Text = "0";
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

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "*", "", "party_no=" + id.ToString().Trim() + "and transaction_type <> 'OB' and transaction_date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "' and transaction_type in ('R','P','C')", "transaction_date");
        if (ds == null)
        {
            objCommon.DisplayMessage("Record Not Available.", this);
            ClearRecord();
            txtAcc.Focus();
            return;

        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            objCommon.DisplayMessage("Record Not Available.", this);
            ClearRecord();
            txtAcc.Focus();
            return;

        }


        if (ds != null)
        {

            //DataSet dsvch = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "VOUCHER_NO", "Transaction_Type,Convert(nvarchar(20),transaction_date,103)transaction_date,CHQ_NO,Convert(nvarchar(20),CHQ_DATE ,103)CHQ_DATE,RECON_DATE ", "party_no=" + id.ToString().Trim() + " and transaction_type <> 'OB' and transaction_date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "'", "transaction_date");
            DataSet dsvch = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS a left join ACC_" + Session["comp_code"].ToString() + "_CHECK_PRINT b on (a.VOUCHER_NO=b.VNO)", "VOUCHER_NO,CASE WHEN TRANSACTION_TYPE='P' THEN 'PAYMENT'ELSE CASE WHEN TRANSACTION_TYPE='R' THEN 'RECEIPT' ELSE CASE WHEN TRANSACTION_TYPE='C' THEN 'CONTRA' ELSE '' END END END AS TRANSACTION_TYPE", "Convert(nvarchar(20),transaction_date,103)transaction_date,IIF(TRANSACTION_TYPE IN ('R','C'),CHQ_NO,IIF(b.CHECKNO is not null,b.CHECKNO,CHQ_NO)) AS Chq_no,IIF(TRANSACTION_TYPE IN ('R','C'),Convert(nvarchar(20),CHQ_DATE ,103),IIF(b.CHECKNO is not null,Convert(nvarchar(20),CHECKDT ,103),Convert(nvarchar(20),CHQ_DATE ,103)))CHQ_DATE,IIF(TRANSACTION_TYPE IN ('R','C'),a.RECON_DATE,IIF(b.CHECKNO is not null,b.RECONDATE,a.RECON_DATE)) RECON_DATE,IIF(TRANSACTION_TYPE IN ('R','C'),a.Amount,IIF(b.CHECKNO is not null,b.AMOUNT,a.Amount)) CHEQ_AMOUNT,'" + Session["comp_code"].ToString() + "/'+TRANSACTION_TYPE+'/'+A.VOUCHER_NO STR_VOUCHER_NO,a.voucher_sqn,transaction_date transaction_date1", "party_no=" + id.ToString().Trim() + " and transaction_type in ('P','R','C') and a.SUBTR_NO <> 0 and transaction_date between '" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "'", "transaction_date1,VOUCHER_NO,TRANSACTION_TYPE");
            Session["VchDatatable"] = null;
            if (dsvch != null)
            {
                if (dsvch.Tables[0].Rows.Count != 0)
                {
                    //DataColumn dc = new DataColumn();
                    //dc.ColumnName = "STR_VOUCHER_NO";
                    //dsvch.Tables[0].Columns.Add(dc);
                    //int y = 0;
                    //for (y = 0; y < dsvch.Tables[0].Rows.Count; y++)
                    //{
                    //    if (dsvch.Tables[0].Rows[y]["Transaction_type"].ToString().Trim() == "R")
                    //    {
                    //        dsvch.Tables[0].Rows[y]["STR_VOUCHER_NO"] = Session["comp_code"].ToString().Trim() + "/" + dsvch.Tables[0].Rows[y]["Transaction_type"].ToString() + "/" + dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                    //        dsvch.Tables[0].Rows[y]["Transaction_type"] = "Receipt";

                    //    }
                    //    else if (dsvch.Tables[0].Rows[y]["Transaction_type"].ToString().Trim() == "P")
                    //    {
                    //        dsvch.Tables[0].Rows[y]["STR_VOUCHER_NO"] = Session["comp_code"].ToString().Trim() + "/" + dsvch.Tables[0].Rows[y]["Transaction_type"].ToString() + "/" + dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                    //        dsvch.Tables[0].Rows[y]["Transaction_type"] = "Payment";

                    //    }
                    //    else if (dsvch.Tables[0].Rows[y]["Transaction_type"].ToString().Trim() == "C")
                    //    {
                    //        dsvch.Tables[0].Rows[y]["STR_VOUCHER_NO"] = Session["comp_code"].ToString().Trim() + "/" + dsvch.Tables[0].Rows[y]["Transaction_type"].ToString() + "/" + dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                    //        dsvch.Tables[0].Rows[y]["Transaction_type"] = "Contra";

                    //    }
                    //    else
                    //    {
                    //        dsvch.Tables[0].Rows[y]["STR_VOUCHER_NO"] = Session["comp_code"].ToString().Trim() + "/" + dsvch.Tables[0].Rows[y]["Transaction_type"].ToString() + "/" + dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                    //        dsvch.Tables[0].Rows[y]["Transaction_type"] = "Journal";

                    //    }
                    //    //dsvch.Tables[0].Rows[y]["STR_VOUCHER_NO"] = Session["comp_code"].ToString().Trim() + dsvch.Tables[0].Rows[y]["VOUCHER_NO"].ToString().Trim();
                    //    dsvch.Tables[0].Rows[y].AcceptChanges();

                    //}

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
                            HashSet<string> set = new HashSet<string>(oPartyId);
                            string[] result = new string[set.Count];
                            set.CopyTo(result);
                            oPartyId = result;
                            string Ledgers = string.Empty;
                            string Date = string.Empty;
                            string VchType = string.Empty;
                            double Debit = 0.00;
                            double Credit = 0.00;
                            string VchNo = "0";
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
                                    //string a = "VchNo=" + dtTemp.Rows[i]["VOUCHER_NO"].ToString().Trim() + " and VchType='" + dtTemp.Rows[i]["Transaction_Type"].ToString().Trim().ToUpper() + "'";

                                    dvTemp.RowFilter = "VchNo='" + ds1.Tables[0].Rows[k]["VOUCHER_NO"].ToString().Trim() + "' and VchType='" + ds1.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() + "'";
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


                                            if (j == 0)
                                                Ledgers = ds1.Tables[0].Rows[k]["LEDGER"].ToString().Trim() + "  " + "<br />" + ds1.Tables[0].Rows[k]["parti"].ToString().Trim();
                                            else
                                                Ledgers = ds1.Tables[0].Rows[k]["LEDGER"].ToString().Trim();


                                            row["Date"] = Date.ToString().Trim();
                                            row["Particulars"] = Ledgers.ToString().Trim();
                                            row["VchType"] = VchType;



                                            row["VchNo"] = VchNo.ToString().Trim();

                                            row["PartyNo"] = ds1.Tables[0].Rows[k]["Party_No"].ToString();

                                            int rec_flag = 0;
                                            //string Recdate
                                            // if (ds1.Tables[0].Rows[k]["rec_no"].ToString().Trim() != "NA")// || ds1.Tables[0].Rows[k]["rec_no"].ToString().Trim() != string.Empty  || ds1.Tables[0].Rows[k]["rec_no"].ToString().Trim() != "")
                                            rec_flag = Convert.ToInt16(ds1.Tables[0].Rows[k]["rec_no"]);//.ToString().Substring(10).ToString().Trim();
                                            // else
                                            // rec_flag = 0;

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



                                            dt.Rows.Add(row);

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
        //}
    }

    private void ClearRecord()
    {
        // SetDataColumn();
        Session["VchDatatable"] = null;
        RptData.DataSource = null;
        RptData.DataBind();
        RptHeading.Visible = false;
        txtAcc.Focus();
        trGrid.Visible = false;
        trBalances.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rdbWithNarration.Checked = false;
        rdbWithoutNarration.Checked = true;
        lblCr.Text = "0.00";
        lblDr.Text = "0.00";
        lblOb.Text = "0.00";
        lblclose.Text = "0.00";
        lblmode.Text = "Dr";
        lblCurBal.Text = "0.00";
        txtAcc.Text = "";
        SetDataColumn();
        Session["VchDatatable"] = null;
        RptData.DataSource = null;
        RptData.DataBind();
        SetFinancialYear();
        txtAcc.Focus();
        RptHeading.Visible = false;
        trGrid.Visible = false;
        trBalances.Visible = false;
        trButton.Visible = false;

        lstData.DataSource = null;
        lstData.DataBind();

       
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //ImageButton btnEdit = sender as ImageButton;
        ////int ScheduleNo = int.Parse(btnEdit.CommandArgument);
        //TextBox txtbankdate = RptData.FindControl("txtbankdate") as TextBox;
        //txtbankdate.ReadOnly = false;
    }


    protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //Label lbvch = e.Item.FindControl("lblvchtype") as Label;
        //if (lbvch != null)
        // {
        //string cmd = e.CommandArgument.ToString();
        //if (e.CommandName.ToString().Trim() == "VoucherPrint")
        //{
        //    if (lbvch.Text.ToString().Trim() == "Payment" || lbvch.Text.ToString().Trim() == "Reciept")
        //    {
        //        ShowVoucherPrintReport("Voucher", "PmtRcptCashVoucherRpt.rpt", lbvch.Text.ToString().Trim(), cmd);
        //    }
        //    else
        //    {
        //        ShowVoucherPrintReport("Voucher", "JvContraVoucherReport.rpt", lbvch.Text.ToString().Trim(), cmd);
        //    }

        //}
        //else
        //{
        //Response.Redirect("AccountingVouchers.aspx?obj=config," + cmd.ToString().Trim() + "," + lbvch.Text.ToString().Trim() + "," + txtFrmDate.Text.ToString().Trim() + "," + txtUptoDate.Text.ToString().Trim());
        if (e.CommandName.ToString().Trim() == "BankDateEdit")
        {
            TextBox txtBankDate = e.Item.FindControl("txtbankdate") as TextBox;
            Image img = e.Item.FindControl("imgCal10") as Image;
            //Image imgEdit = e.Item.FindControl("btnEdit") as Image;
            txtBankDate.Enabled = true;
            txtBankDate.Focus();
            img.Visible = true;

        }
        //}

        //}


    }
    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;

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
    private void ShowReport(string reportTitle, string rptFileName, string Param)
    {
        try
        {
            //int voucherNo = 0;
            //if (isVoucherAuto == "Y")
            //    voucherNo = Convert.ToInt16(txtVoucherNo.Text) - 1;
            //else
            //    voucherNo = Convert.ToInt16(txtVoucherNo.Text);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            ClMode = lblmode.Text.ToString().Trim() == "" ? "Dr" : lblmode.Text.ToString().Trim();
            string LedgerName = string.Empty;
             
            string closeAmt = string.Empty;
            if (ClMode == "Dr")
            {
                closeAmt = lblCurBal.Text.ToString().Trim().Replace("Dr", "").Replace("Cr", "") == "" ? "0.00" : lblCurBal.Text.ToString().Trim().Replace("Dr", "").Replace("Cr", "");
            }
            if (ClMode == "Cr")
            {
                closeAmt = lblCurBal.Text.ToString().Trim().Replace("Dr", "").Replace("Cr", "") == "" ? "0.00" : lblCurBal.Text.ToString().Trim().Replace("Dr", "").Replace("Cr", "");
            }
            //string closeAmt = lblclose.Text.ToString().Trim() == "" ? "0.00" : lblclose.Text.ToString().Trim();
           // string closeAmt = lblCurBal.Text.ToString().Trim() == "" ? "0.00" : lblCurBal.Text.ToString().Trim();
            
            //if (txtAcc.Text.ToString().Trim().Split('*')[0].ToString() == "1")
            //{
            //    LedgerName = "Cash Book";

            //}
            //else if (txtAcc.Text.ToString().Trim().Split('*')[0].ToString() == "2")
            //{
            //    LedgerName = "Bank Book";

            //}
            //else
            //{
            LedgerName = txtAcc.Text.ToString().Trim().Split('*')[0].ToString();

            // }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            if (rdbWithNarration.Checked)
            {
                //url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + lblclose.Text.ToString().Trim() + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_Param=" + Param + ",@P_NV=" + 0;
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + closeAmt + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_Param=" + Param + ",@P_NV=" + 0;
            }
            else
            {
                //url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + lblclose.Text.ToString().Trim() + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_Param=" + Param + ",@P_NV=" + 1;
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_LEDGER=" + LedgerName.ToString() + "," + "@P_CompanyName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Period=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@ClosingBalance=" + closeAmt + "," + "@P_ClosBalMode=" + ClMode.ToString().Trim() + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_Param=" + Param + ",@P_NV=" + 1;
            }
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAcc.Text.ToString().Trim() == "")
            {
                //objCommon.DisplayMessage("Enter Ledger Name.", this);
                ShowMessage("Enter Ledger Name.");
                txtAcc.Focus();
                return;
            }
            //if (RptData.Items.Count == 0)
            //{
            //    objCommon.DisplayMessage("Record Not Available.", this);
            //    txtAcc.Focus();
            //    return;
            //}


            ShowReport("LedgerBook", "BankReconcileStatement_3.rpt", "1");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowLedgerListReport(string reportTitle, string rptFileName)
    {
        try
        {


            if (rptFileName.ToString().Trim() == "TrialBalanceReport.rpt")
            {

                TrialBalanceReportController obj = new TrialBalanceReportController();
                DataSet dsPLOC = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
                double op = 0;
                double cl = 0;
                double plDr = 0;
                double plCr = 0;

                if (dsPLOC != null)
                {

                    if (dsPLOC.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["Opening"]).Trim() == "")
                        {
                            op = 0;
                        }
                        else
                        {
                            op = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                        }
                        if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                        {
                            cl = 0;
                        }
                        else
                        {
                            cl = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]);
                        }
                    }
                }


                DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                if (dsPLDC != null)
                {
                    if (dsPLDC.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                        {
                            plDr = 0;
                        }
                        else
                        {

                            plDr = Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]);
                        }

                        if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                        {
                            plCr = 0;
                        }
                        else
                        {
                            plCr = Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]);
                        }

                    }
                }


                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
                string LedgerName = string.Empty;

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,ACCOUNT," + rptFileName;
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + "@PLOpening=" + op.ToString() + "," + "@PLClosing=" + cl.ToString() + "," + "@PLDebit=" + plDr.ToString() + "," + "@PLCredit=" + plCr.ToString();
                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
            }
            else
            {

                string Script = string.Empty;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

                string LedgerName = string.Empty;

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,ACCOUNT," + rptFileName;
                url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString();

                Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowLedgerListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowBalanceSheet(string reportTitle, string rptFileName)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_CODE_YEAR=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_UpToDate=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowLedgerListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnShowList_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
    //        od.GenerateLedgerList(Session["comp_code"].ToString());
    //        GenerateLedgerListFormat();

    //        ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowList_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}
    protected void GenerateLedgerListFormat()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE", "*", "", string.Empty, string.Empty);
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
                            oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() != dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {
                                    if (dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() == "0")
                                    {

                                        if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                        {
                                            oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                            oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                            TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                            oTran8.AddTrialBalanceReportFormat(oEntity);

                                        }
                                    }

                                }


                            }


                        }
                        else if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }



                            }


                        }




                    }


                }


            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //RECENTLY ADDED===============
    protected void GenerateTrialBalanceFormatNew(string IsBalanceSheet)
    {
        try
        {
            int pName = 0;
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    double TotalDr5 = 0;
                    double TotalCr5 = 0;
                    int LedgerIndex = 0;
                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {

                        double TotalDr = 0;
                        double TotalCr = 0;
                        TrialBalanceReport oEntity = new TrialBalanceReport();
                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                            oEntity.LEDGERINDEX = LedgerIndex;
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);
                            double TotalDr1 = 0;
                            double TotalCr1 = 0;
                            DataSet dsLdg1 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "PRNO=" + dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() + " and party_no = 0", string.Empty);
                            if (dsLdg1 != null)
                            {
                                if (dsLdg1.Tables[0].Rows.Count > 0)
                                {

                                    int j = 0;

                                    for (j = 0; j < dsLdg1.Tables[0].Rows.Count; j++)
                                    {
                                        TrialBalanceReport oEntity1 = new TrialBalanceReport();
                                        oEntity1.PartyName = "     " + dsLdg1.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim().Trim();
                                        oEntity1.MGRPNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                        oEntity1.PRNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                        oEntity1.PARTYNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                        oEntity1.OPBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                        oEntity1.CLBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                        oEntity1.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                        oEntity1.DEBIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                        oEntity1.CREDIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                        oEntity1.FANO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["FA_NO"].ToString().Trim());
                                        oEntity1.LEDGERINDEX = LedgerIndex;
                                        TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                        oTran1.AddTrialBalanceReportFormat(oEntity1);

                                        DataSet dsLdg2 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim() + " and prno=" + dsLdg1.Tables[0].Rows[j]["prno"].ToString().Trim() + " and party_no <> 0", string.Empty);
                                        if (dsLdg2 != null)
                                        {
                                            int k = 0;
                                            for (k = 0; k < dsLdg2.Tables[0].Rows.Count; k++)
                                            {

                                                TrialBalanceReport oEntity2 = new TrialBalanceReport();
                                                oEntity2.PartyName = "          " + dsLdg2.Tables[0].Rows[k]["PARTYNAME"].ToString().Trim().Trim();
                                                oEntity2.MGRPNO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim());
                                                oEntity2.PRNO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["PRNO"].ToString().Trim());
                                                oEntity2.PARTYNO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["PARTY_NO"].ToString().Trim());
                                                oEntity2.OPBALANCE = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["OP_BALANCE"].ToString().Trim());
                                                oEntity2.CLBALANCE = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["CL_BALANCE"].ToString().Trim());
                                                oEntity2.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                                                oEntity2.DEBIT = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["DEBIT"].ToString().Trim());
                                                oEntity2.CREDIT = Convert.ToDouble(dsLdg2.Tables[0].Rows[k]["CREDIT"].ToString().Trim());
                                                oEntity2.FANO = Convert.ToInt16(dsLdg2.Tables[0].Rows[k]["FA_NO"].ToString().Trim());
                                                oEntity2.LEDGERINDEX = LedgerIndex;
                                                TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                                oTran2.AddTrialBalanceReportFormat(oEntity2);


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

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateTrialBalanceFormatNew -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //END RECENTLY ADDED======================================


    //SECOND RECENTLY ADDED===========
    protected void GenerateTrialBalanceFormatNew1()
    {
        try
        {
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
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
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() != dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {
                                    if (dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() == "0")
                                    {

                                        if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                        {
                                            oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                            TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                            oTran8.AddTrialBalanceReportFormat(oEntity);

                                        }
                                    }

                                }


                            }


                        }
                        else if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            int j = 0;
                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++)
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "          ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);
                                }



                            }


                        }




                    }


                }


            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    //END SECOND RECENTLY ADDED=======


    //add on date 02/03/2010
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

                                //    space2 = space2.ToString() + "  ";

                            }

                        }
                        //InsertSubParentEntry(Convert.ToInt16(dsSp.Tables[0].Rows[k]["MGRP_NO"].ToString().Trim()), Convert.ToInt16(dsSp.Tables[0].Rows[k]["prno"].ToString().Trim()), Convert.ToInt16(dsSp.Tables[0].Rows[k]["party_no"].ToString().Trim()), dsSp, fano);

                    }


                }
                //space1 = space1.ToString() + "  ";
                space1 = "      ".ToString();
                // space1 = "  ".ToString();
            }
        }

    }
    protected DataSet GetChildRecord(Int16 Prono)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + Prono.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }
    protected DataSet GetParentChildRecord(Int16 mgrp, Int16 prno)
    {
        DataSet dsres = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", "MGRP_NO=" + mgrp.ToString().Trim() + " and  prno=" + prno.ToString().Trim() + " and party_no <> 0 ", string.Empty);
        return dsres;

    }
    protected void ArrangeFormat(DataSet dsLdg1, DataSet dsLdg)
    {
        try
        {


            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    ArrayList t = new ArrayList();
                    int i = 0;
                    DataView dv = dsLdg.Tables[0].DefaultView;
                    dt1 = dv.ToTable();
                    for (i = 0; i < dt1.Rows.Count; i++)
                    {
                        TrialBalanceReport oEntity = new TrialBalanceReport();


                        int j = 0;
                        if (t.Contains(dt1.Rows[i]["MGRP_NO"].ToString().Trim()) == false)
                        {
                            for (j = 0; j < dsLdg1.Tables[0].Rows.Count; j++)
                            {

                                if (dt1.Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                {
                                    oEntity.PartyName = dsLdg1.Tables[0].Rows[j]["PARTYNAME"].ToString();
                                    //oEntity.PartyName = space1.ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg1.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    oEntity.FANO = Convert.ToInt16(dsLdg1.Tables[0].Rows[j]["fano"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);
                                }
                            }
                        }
                        t.Add(dt1.Rows[i]["MGRP_NO"].ToString().Trim());


                    }

                }


            }

        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.ArrangeFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateLedgerListFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    // end of 02/03/2010
    protected void GenerateTrialBalanceFormat(string IsBalanceSheet)
    {
        try
        {
            int pName = 0;
            TrialBalanceReport oEntity = new TrialBalanceReport();
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    double TotalDr5 = 0;
                    double TotalCr5 = 0;
                    int LedgerIndex = 0;
                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {

                        double TotalDr = 0;
                        double TotalCr = 0;


                        if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "CAPITAL ACCOUNT")
                            {
                                LedgerIndex = 1;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "FIXED ASSETS")
                            {
                                LedgerIndex = 2;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "LOANS (LIABILITY)")
                            {
                                LedgerIndex = 3;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "INVESTMENTS")
                            {
                                LedgerIndex = 4;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "CURRENT LIABLITIES")
                            {
                                LedgerIndex = 5;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "CURRENT ASSETS")
                            {
                                LedgerIndex = 6;
                            }
                            else if (dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().ToUpper() == "DIRECT EXPENSES")
                            {
                                LedgerIndex = 7;
                            }




                            oEntity.PartyName = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim().Trim();
                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim());
                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"].ToString().Trim());
                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"].ToString().Trim());
                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["OP_BALANCE"].ToString().Trim());
                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim());
                            oEntity.ISPARTY = 0;// Convert.ToInt16(dsLdg.Tables[0].Rows[i]["IS_PARTY"].ToString().Trim());
                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["DEBIT"].ToString().Trim());
                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CREDIT"].ToString().Trim());
                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                            oEntity.LEDGERINDEX = LedgerIndex;
                            double tot = oEntity.CREDIT - oEntity.DEBIT;
                            if (tot > 0)
                            {
                                oEntity.CREDIT = tot;
                                oEntity.DEBIT = 0;
                            }
                            else
                            {
                                oEntity.CREDIT = 0;
                                oEntity.DEBIT = Math.Abs(tot);

                            }

                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            int j = 0;
                            double TotalDr1 = 0;
                            double TotalCr1 = 0;





                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++) //2 for loop
                            {


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    if (pName != 0)
                                    {
                                        TrialBalanceReportController oUpd5 = new TrialBalanceReportController();
                                        TrialBalanceReport oEnUpd5 = new TrialBalanceReport();
                                        oEnUpd5.MGRPNO = pName;
                                        double tot11 = TotalCr5 - TotalDr5;
                                        if (tot11 > 0)
                                        {
                                            oEnUpd5.CREDIT = tot11;
                                            oEnUpd5.DEBIT = 0.00;
                                        }
                                        else
                                        {
                                            oEnUpd5.DEBIT = Math.Abs(tot11);
                                            oEnUpd5.CREDIT = 0.00;
                                        }



                                        oUpd5.UpdateTrialBalanceAmount(oEnUpd5);

                                        TotalDr5 = 0;
                                        TotalCr5 = 0;
                                    }



                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    pName = oEntity.MGRPNO;
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                    oEntity.LEDGERINDEX = LedgerIndex;
                                    double tot1 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot1 > 0)
                                    {
                                        oEntity.CREDIT = tot1;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot1);

                                    }

                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() != dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {
                                    if (dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() == "0")
                                    {

                                        if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim())
                                        {
                                            oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                            oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                            oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                            oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                            oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                            oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                            oEntity.ISPARTY = 1;// Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                            oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                            oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                            oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                            oEntity.LEDGERINDEX = LedgerIndex;
                                            double tot2 = oEntity.CREDIT - oEntity.DEBIT;
                                            if (tot2 > 0)
                                            {
                                                oEntity.CREDIT = tot2;
                                                oEntity.DEBIT = 0;
                                            }
                                            else
                                            {
                                                oEntity.CREDIT = 0;
                                                oEntity.DEBIT = Math.Abs(tot2);

                                            }


                                            TotalDr1 = TotalDr1 + oEntity.DEBIT;
                                            TotalCr1 = TotalCr1 + oEntity.CREDIT;


                                            TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                            oTran8.AddTrialBalanceReportFormat(oEntity);

                                        }
                                    }

                                }
                                else if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    double tot3 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot3 > 0)
                                    {
                                        oEntity.CREDIT = tot3;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot3);

                                    }

                                    TotalDr1 = TotalDr1 + oEntity.DEBIT;
                                    TotalCr1 = TotalCr1 + oEntity.CREDIT;

                                    TotalDr5 = TotalDr5 + oEntity.DEBIT;
                                    TotalCr5 = TotalCr5 + oEntity.CREDIT;                                                //TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    //oTran2.AddTrialBalanceReportFormat(oEntity);
                                }


                            }// End of 2 for loop
                            TrialBalanceReportController oUpd = new TrialBalanceReportController();
                            TrialBalanceReport oEnUpd = new TrialBalanceReport();
                            oEnUpd.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]);

                            double tot12 = TotalCr1 - TotalDr1;
                            if (tot12 > 0)
                            {
                                oEnUpd.CREDIT = tot12;
                                oEnUpd.DEBIT = 0.00;
                            }
                            else
                            {
                                oEnUpd.DEBIT = Math.Abs(tot12);
                                oEnUpd.CREDIT = 0.00;
                            }


                            oUpd.UpdateTrialBalanceAmount(oEnUpd);

                            if (pName != 0)
                            {
                                TrialBalanceReportController oUpd7 = new TrialBalanceReportController();
                                TrialBalanceReport oEnUpd7 = new TrialBalanceReport();
                                oEnUpd7.MGRPNO = pName;
                                double tot13 = TotalCr5 - TotalDr5;
                                if (tot13 > 0)
                                {
                                    oEnUpd7.CREDIT = tot13;
                                    oEnUpd7.DEBIT = 0.00;
                                }
                                else
                                {
                                    oEnUpd7.DEBIT = Math.Abs(tot13);
                                    oEnUpd7.CREDIT = 0.00;
                                }


                                oUpd7.UpdateTrialBalanceAmount(oEnUpd7);

                                //TotalDr5 = 0;
                                //TotalCr5 = 0;
                            }


                        }
                        else if (Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PRNO"]) != 0 && Convert.ToInt16(dsLdg.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            int j = 0;
                            double TotalDr2 = 0;
                            double TotalCr2 = 0;
                            double TotalDr6 = 0;
                            double TotalCr6 = 0;

                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++) // for loop 3
                            {

                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {

                                    oEntity.PartyName = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    pName = oEntity.MGRPNO;
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;//Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    double tot4 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot4 > 0)
                                    {
                                        oEntity.CREDIT = tot4;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot4);

                                    }
                                    oEntity.FANO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["FA_NO"].ToString().Trim());
                                    oEntity.LEDGERINDEX = LedgerIndex;
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {

                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    double tot5 = oEntity.CREDIT - oEntity.DEBIT;
                                    if (tot5 > 0)
                                    {
                                        oEntity.CREDIT = tot5;
                                        oEntity.DEBIT = 0;
                                    }
                                    else
                                    {
                                        oEntity.CREDIT = 0;
                                        oEntity.DEBIT = Math.Abs(tot5);

                                    }
                                    TotalDr2 = TotalDr2 + oEntity.DEBIT;
                                    TotalCr2 = TotalCr2 + oEntity.CREDIT;
                                    TotalDr6 = TotalDr6 + oEntity.DEBIT;
                                    TotalCr6 = TotalCr6 + oEntity.CREDIT;


                                }



                            }// end of for loop 3


                        }




                    }


                }



                oEntity.PartyName = "Diff. in Opening Balances".ToString().Trim().Trim();
                oEntity.MGRPNO = 0;
                oEntity.PRNO = 0;
                oEntity.PARTYNO = 0;
                oEntity.OPBALANCE = 0;
                oEntity.CLBALANCE = 0;
                double TotalOpeningBalance = 0;
                PartyController op = new PartyController();
                DataSet dsOp = op.GetTotalOpeningBalances(Session["comp_code"].ToString());
                if (dsOp != null)
                {
                    if (dsOp.Tables[0].Rows.Count > 0)
                    {

                        TotalOpeningBalance = Convert.ToDouble(dsOp.Tables[0].Rows[0]["CREDIT"]) - Convert.ToDouble(dsOp.Tables[0].Rows[0]["DEBIT"]);

                    }

                }
                if (TotalOpeningBalance < 0)
                {
                    oEntity.DEBIT = Math.Abs(TotalOpeningBalance);

                }
                else
                {
                    oEntity.CREDIT = Math.Abs(TotalOpeningBalance);

                }
                oEntity.ISPARTY = 1;
                oEntity.FANO = 0;
                oEntity.LEDGERINDEX = 10;
                TrialBalanceReportController oTran4 = new TrialBalanceReportController();
                oTran4.AddTrialBalanceReportFormat(oEntity);

                //For Income-Expenses Transaction Amount=========================
                if (IsBalanceSheet == "Y")
                {

                    oEntity.PartyName = "Income Expenses A/c".ToString();
                    oEntity.MGRPNO = 0;
                    oEntity.PRNO = 0;
                    oEntity.PARTYNO = 0;
                    oEntity.OPBALANCE = 0;
                    oEntity.CLBALANCE = 0;
                    double TotalDrExpensesAmount = 0;
                    double TotalCrExpensesAmount = 0;
                    double NetExpensAmount = 0;
                    DataSet dsOp1 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "Debit", "Credit", "IS_PARTY=0 AND FA_NO=4", "FA_NO");
                    if (dsOp1 != null)
                    {
                        if (dsOp1.Tables[0].Rows.Count > 0)
                        {
                            int K = 0;
                            for (K = 0; K < dsOp1.Tables[0].Rows.Count; K++)
                            {
                                TotalDrExpensesAmount = TotalDrExpensesAmount + Convert.ToDouble(dsOp1.Tables[0].Rows[K]["Debit"].ToString().Trim());
                                TotalCrExpensesAmount = TotalCrExpensesAmount + Convert.ToDouble(dsOp1.Tables[0].Rows[K]["Credit"].ToString().Trim());

                            }

                            NetExpensAmount = Convert.ToDouble(TotalCrExpensesAmount) - Convert.ToDouble(TotalDrExpensesAmount);

                        }

                    }

                    double TotalDrIncomeAmount = 0;
                    double TotalCrIncomeAmount = 0;
                    double NetIncomeAmount = 0;
                    DataSet dsOp2 = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "Debit", "Credit", "IS_PARTY=0 AND FA_NO=3", "FA_NO");
                    if (dsOp2 != null)
                    {
                        if (dsOp2.Tables[0].Rows.Count > 0)
                        {
                            int l = 0;
                            for (l = 0; l < dsOp2.Tables[0].Rows.Count; l++)
                            {
                                TotalDrIncomeAmount = TotalDrIncomeAmount + Convert.ToDouble(dsOp2.Tables[0].Rows[l]["Debit"].ToString().Trim());
                                TotalCrIncomeAmount = TotalCrIncomeAmount + Convert.ToDouble(dsOp2.Tables[0].Rows[l]["Credit"].ToString().Trim());

                            }

                            NetIncomeAmount = Convert.ToDouble(TotalCrIncomeAmount) - Convert.ToDouble(TotalDrIncomeAmount);

                        }

                    }

                    double NetDeficitAmount = NetIncomeAmount - NetExpensAmount;






                    if (NetDeficitAmount < 0)
                    {
                        oEntity.DEBIT = Math.Abs(NetDeficitAmount);

                    }
                    else
                    {
                        oEntity.CREDIT = Math.Abs(NetDeficitAmount);

                    }



                    oEntity.ISPARTY = 1;
                    oEntity.FANO = 0;
                    oEntity.LEDGERINDEX = 11;
                    TrialBalanceReportController oTran5 = new TrialBalanceReportController();
                    oTran5.AddTrialBalanceReportFormat(oEntity);
                }
                //===============================================================

                TrialBalanceReportController oDelete = new TrialBalanceReportController();
                oDelete.DeleteTrialBalanceAmount();

            }


            // ShowLedgerListReport("LedgerList", "LedgerListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.GenerateTrialBalanceFormat -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    protected void UpdateTotalForLedger()
    {
        try
        {
            DataSet dtl = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", string.Empty, string.Empty);
            if (dtl != null)
            {

                if (dtl.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    double Cr1 = 0;
                    double Dr1 = 0;
                    double Cr2 = 0;
                    double Dr2 = 0;
                    double Op1 = 0;
                    double Cl1 = 0;
                    double Op2 = 0;
                    double Cl2 = 0;
                    int mgrpno1 = 0;
                    int mgrpno2 = 0;
                    int pno1 = 0;
                    int pno2 = 0;
                    for (i = 0; i < dtl.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]) != 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]) == 0 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {

                            if (Math.Abs(Cr1) > 0 || Math.Abs(Dr1) > 0 || Math.Abs(Op1) > 0 || Math.Abs(Cl1) > 0)
                            {
                                //update total
                                TrialBalanceReportController tbc = new TrialBalanceReportController();
                                tbc.UpdateAllTrialBalanceAmount(mgrpno1, Cr1, Dr1, Op1, Cl1);

                                Cr1 = 0;
                                Dr1 = 0;
                                Op1 = 0;
                                Cl1 = 0;

                            }



                            mgrpno1 = Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]);


                        }
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]) == mgrpno1 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"]) == 0)
                        {
                            if (Math.Abs(Cr2) > 0 || Math.Abs(Dr2) > 0 || Math.Abs(Op2) > 0 || Math.Abs(Cl2) > 0)
                            {
                                //update total
                                TrialBalanceReportController tbc1 = new TrialBalanceReportController();
                                tbc1.UpdateAllTrialBalanceAmount(mgrpno2, Cr2, Dr2, Op2, Cl2);

                                Cr1 = Cr1 + Cr2;
                                Dr1 = Dr1 + Dr2;
                                Op1 = Op1 + Op2;
                                Cl1 = Cl1 + Cl2;
                                Cr2 = 0;
                                Dr2 = 0;
                                Op2 = 0;
                                Cl2 = 0;

                            }

                            mgrpno2 = Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]);
                            pno2 = Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]);


                        }
                        if (Convert.ToInt16(dtl.Tables[0].Rows[i]["MGRP_NO"]) == mgrpno2 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PRNO"]) == pno2 && Convert.ToInt16(dtl.Tables[0].Rows[i]["PARTY_NO"]) != 0)
                        {
                            Cr2 = Cr2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["CREDIT"]);
                            Dr2 = Dr2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["DEBIT"]);
                            Op2 = Op2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["OP_BALANCE"]);
                            Cl2 = Cl2 + Convert.ToDouble(dtl.Tables[0].Rows[i]["CL_BALANCE"]);



                        }





                    }








                }



            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.UpdateTotalForLedger -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.UpdateTotalForLedgerNew -> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "AccountingVouchersModifications.UpdateTotalForMainLedger -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void btnShowBalanceSheet_Click(object sender, EventArgs e)
    {
        try
        {

            // btnShowTrialBalance_Click(sender, e);


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

            TrialBalanceReportController od = new TrialBalanceReportController();
            od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
            od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
            //GenerateTrialBalanceFormat("N");
            GenerateTrialBalanceFormatNew2();
            od.OrderTrialBalanceReport();
            UpdateTotalForLedgerNew();
            UpdateTotalForMainLedger();
            od.DeleteTrialBalanceZEROAmount();

            Response.Redirect("BalanceSheetConfiguration.aspx?obj=" + txtFrmDate.Text.ToString().Trim() + "," + txtUptoDate.Text.ToString().Trim() + "");

            //TrialBalanceReportController od = new TrialBalanceReportController();
            //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
            //od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
            //GenerateTrialBalanceFormat("Y");
            //od.OrderTrialBalanceReport();
            //ShowBalanceSheet("BalanceSheet", "BalanceSheet1.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }
    protected void btnPL_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (txtFrmDate.Text.ToString().Trim() == "")
        //    {
        //        objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
        //        txtFrmDate.Focus();
        //        return;
        //    }
        //    if (txtUptoDate.Text.ToString().Trim() == "")
        //    {
        //        objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
        //        txtUptoDate.Focus();
        //        return;
        //    }


        //    if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        //    {
        //        objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
        //        txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
        //        txtUptoDate.Focus();
        //        return;
        //    }

        //    if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        //    {
        //        objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
        //        txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
        //        txtFrmDate.Focus();
        //        return;
        //    }

        //    if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        //    {
        //        objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
        //        txtUptoDate.Focus();
        //        return;
        //    }
        //    TrialBalanceReportController od = new TrialBalanceReportController();
        //    od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
        //    od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
        //    GenerateTrialBalanceFormat("Y");
        //    od.OrderTrialBalanceReport();
        //    ShowBalanceSheet("ProfitLoss", "IncomeExpensesMainReport.rpt");
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "AccountingVouchersModifications.btnPL_Click -> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");

        //}


        try
        {

            // btnShowTrialBalance_Click(sender, e);


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

            TrialBalanceReportController od = new TrialBalanceReportController();
            od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
            od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
            //GenerateTrialBalanceFormat("N");
            GenerateTrialBalanceFormatNew2();
            od.OrderTrialBalanceReport();
            UpdateTotalForLedgerNew();
            UpdateTotalForMainLedger();
            od.DeleteTrialBalanceZEROAmount();

            Response.Redirect("IncomeExpenditureConfiguration.aspx?obj=" + txtFrmDate.Text.ToString().Trim() + "," + txtUptoDate.Text.ToString().Trim() + "");

            //TrialBalanceReportController od = new TrialBalanceReportController();
            //od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());// + "_" + Session["fin_yr"].ToString().Trim());
            //od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
            //GenerateTrialBalanceFormat("Y");
            //od.OrderTrialBalanceReport();
            //ShowBalanceSheet("BalanceSheet", "BalanceSheet1.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnPL_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void btnRP_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtAcc.Text.ToString().Trim() == "")
            //{
            //    objCommon.DisplayMessage("Enter Ledger Name.", this);
            //    txtAcc.Focus();
            //    return;

            //}
            //if (RptData.Items.Count == 0)
            //{
            //    objCommon.DisplayMessage("Record Not Available.", this);
            //    txtAcc.Focus();
            //    return;

            //}
            DeleteReceiptPaymentFormat();
            GenerateReceiptPaymentFormat();
            ShowReportReceiptPayment("ReceiptPayment", "ReceiptPayment.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnRP_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReportReceiptPayment(string reportTitle, string rptFileName)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            ClMode = lblmode.Text.ToString().Trim();
            string LedgerName = string.Empty;

            // LedgerName = txtAcc.Text.ToString().Trim().Split('*')[1].ToString();

            // }

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowReportReceiptPayment -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void DeleteReceiptPaymentFormat()
    {

        AccountTransactionController objtrn1 = new AccountTransactionController();
        objtrn1.DeleteReceiptPaymentData(Session["comp_code"].ToString().Trim());

    }
    public void GenerateReceiptPaymentFormat()
    {
        DataSet dso = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "*", "", "payment_type_no in ('1','2') ", "party_no");
        if (dso.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            int id = 1;
            double DrOpBalance = 0;
            double CrOpBalance = 0;
            for (i = 0; i < dso.Tables[0].Rows.Count; i++)
            {
                if (dso.Tables[0].Rows[i]["STATUS"].ToString().Trim() == "D")
                {
                    DrOpBalance = DrOpBalance + Convert.ToDouble(dso.Tables[0].Rows[i]["OPBALANCE"]);

                }
                else
                {
                    CrOpBalance = CrOpBalance + Convert.ToDouble(dso.Tables[0].Rows[i]["OPBALANCE"]);

                }

            }

            //insert for ope5ning balances
            if (DrOpBalance > 0)
            {
                InsertReceiptPayment(id, "R", DrOpBalance, "OPENING BALANCE");
            }
            if (CrOpBalance > 0)
            {
                if (DrOpBalance > 0)
                {
                    id = id + 1;
                }
                else
                {
                    id = 1;
                }
                InsertReceiptPayment(id, "P", CrOpBalance, "OPENING BALANCE");
            }
            //End of insert for opening balances

            int j = 0;
            for (j = 0; j < dso.Tables[0].Rows.Count; j++)
            {

                //call proccedure
                AccountTransactionController objtrn = new AccountTransactionController();
                DataSet dsResult = objtrn.GetReceiptPaymentResult(Session["comp_code"].ToString().Trim(), dso.Tables[0].Rows[j]["PARTY_NAME"].ToString().Trim(), Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"));
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    int k = 0;
                    for (k = 0; k < dsResult.Tables[0].Rows.Count; k++)
                    {
                        if (DrOpBalance > 0)
                        {
                            id = id + 1;

                        }
                        else if (CrOpBalance > 0)
                        {
                            id = id + 1;

                        }

                        if (dsResult.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() == "Contra")
                        {
                            if (Convert.ToDouble(dsResult.Tables[0].Rows[k]["DEBIT"]) > 0)
                            {
                                InsertReceiptPayment(id, "R", Convert.ToDouble(dsResult.Tables[0].Rows[k]["DEBIT"]), Convert.ToString(dso.Tables[0].Rows[j]["Party_Name"]).Trim());


                            }
                            if (Convert.ToDouble(dsResult.Tables[0].Rows[k]["CREDIT"]) > 0)
                            {
                                InsertReceiptPayment(id, "P", Convert.ToDouble(dsResult.Tables[0].Rows[k]["CREDIT"]), Convert.ToString(dso.Tables[0].Rows[j]["Party_Name"]).Trim());


                            }


                        }
                        else if (dsResult.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() == "Receipt")
                        {
                            InsertReceiptPayment(id, "R", Convert.ToDouble(dsResult.Tables[0].Rows[k]["DEBIT"]), Convert.ToString(dsResult.Tables[0].Rows[k]["LEDGER"]).Trim());
                        }
                        else if (dsResult.Tables[0].Rows[k]["Vch_Type"].ToString().Trim() == "Payment")
                        {
                            InsertReceiptPayment(id, "P", Convert.ToDouble(dsResult.Tables[0].Rows[k]["CREDIT"]), Convert.ToString(dsResult.Tables[0].Rows[k]["LEDGER"]).Trim());
                        }




                    }


                }


            }


        }


    }
    public void InsertReceiptPayment(int index, string RPFlag, double Amount, string Ledger)
    {
        AccountTransactionController objAdd = new AccountTransactionController();
        objAdd.AddReceiptPaymentFormat(index, Ledger, Amount, RPFlag);

    }
    //protected void btndb_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        if (txtFrmDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //            txtFrmDate.Focus();
    //            return;
    //        }
    //        if (txtUptoDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }


    //        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //            txtUptoDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //            txtFrmDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //            txtUptoDate.Focus();
    //            return;
    //        }
    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteDayBookRecord();
    //        GetDayBookReportFormat();
    //        OrderDaybook();
    //        ShowDayBook("DayBook", "DayBook.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }
    //}
    private void ShowDayBook(string reportTitle, string rptFileName)
    {
        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            if (txtFrmDate.Text.ToString().Trim() == txtUptoDate.Text.ToString().Trim())
            {
                url += "&param=@P_CompanyName=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + "For " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();
            }
            else
            {
                url += "&param=@P_CompanyName=" + Session["comp_code"].ToString() + "," + "@P_PERIOD=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy").Trim() + " to " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd/MM/yyyy").Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim();

            }


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowLedgerListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void OrderDaybook()
    {
        TrialBalanceReportController ot = new TrialBalanceReportController();
        ot.OrderDaybookReportFormat();

    }
    //public void GetDayBookReportFormat()
    //{
    //    int id = 0;
    //    int i = 0;
    //    TrialBalanceReportController od1 = new TrialBalanceReportController();
    //    DataSet ds = od1.GetDistinctVoucherNo(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString());
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                TrialBalanceReportController od2 = new TrialBalanceReportController();
    //                DataSet ds1 = od2.GetDayBookRecord(Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy"), Session["comp_code"].ToString(),Convert.ToInt32(ds.Tables[0].Rows[i]["VOUCHER_NO"]));
    //                if (ds1.Tables[0].Rows.Count > 0)
    //                { 
    //                    //int k=0;
    //                    //for(k=0;k< ds1.Tables[0].Rows.Count;k++)
    //                    //{
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "J")
    //                        {
    //                           id= AddJournalVoucherEntry(ds1.Tables[0],id);

    //                        }
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "P")
    //                        {
    //                            id = AddPaymentVoucherEntry(ds1.Tables[0], id);

    //                        }
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "C")
    //                        {
    //                            id = AddContraVoucherEntry(ds1.Tables[0], id);

    //                        }
    //                        if (Convert.ToString(ds1.Tables[0].Rows[0]["Transaction_type"]).Trim() == "R")
    //                        {
    //                            id = AddReceiptVoucherEntry(ds1.Tables[0], id);

    //                        }

    //                   // }



    //                }

    //            }


    //        }

    //    }


    //}
    //    private int AddPaymentVoucherEntry(DataTable dtp,int id) //function for Payment
    //{
    //    int l = 0;
    //    for (l = 0; l < dtp.Rows.Count; l++)
    //    {
    //        if (Convert.ToString(dtp.Rows[l]["Tran"]).Trim() == "Dr")
    //        {
    //           AccountTransactionController od7 = new AccountTransactionController();
    //           od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Payment", dtp.Rows[l]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1);
    //           id = id + 1;        
    //        }

    //    }
    //    int m = 0;
    //    for (m = 0; m < dtp.Rows.Count; m++)
    //    {
    //        if (Convert.ToString(dtp.Rows[m]["Tran"]).Trim() == "Cr")
    //        {
    //            AccountTransactionController od8 = new AccountTransactionController();
    //            od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "", dtp.Rows[m]["str_voucher_no"].ToString().Trim(),0 , Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;
    //        }

    //    }


    //    int n = 0;
    //    for (n = 0; n < dtp.Rows.Count; n++)
    //    {
    //        if (Convert.ToString(dtp.Rows[n]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od9 = new AccountTransactionController();
    //            if (Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() != "")
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtp.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }
    //            else
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }





    //            id = id + 1;
    //        }

    //    }



    //    return id;

    //}// End Of function for Payment
    //    private int AddContraVoucherEntry(DataTable dtp, int id) //function for Contra
    //{
    //    int l = 0;
    //    for (l = 0; l < dtp.Rows.Count; l++)
    //    {
    //        if (Convert.ToString(dtp.Rows[l]["Tran"]).Trim() == "Dr")
    //        {
    //            AccountTransactionController od7 = new AccountTransactionController();
    //            od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[l]["Party_Name"].ToString().Trim(), "Contra", dtp.Rows[l]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtp.Rows[l]["Amount"].ToString().Trim()), 0, 1);
    //            id = id + 1;
    //        }

    //    }
    //    int m = 0;
    //    for (m = 0; m < dtp.Rows.Count; m++)
    //    {
    //        if (Convert.ToString(dtp.Rows[m]["Tran"]).Trim() == "Cr")
    //        {
    //            AccountTransactionController od8 = new AccountTransactionController();
    //            od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtp.Rows[m]["Party_Name"].ToString().Trim(), "", dtp.Rows[m]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtp.Rows[m]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;
    //        }

    //    }


    //    int n = 0;
    //    for (n = 0; n < dtp.Rows.Count; n++)
    //    {
    //        if (Convert.ToString(dtp.Rows[n]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od9 = new AccountTransactionController();
    //            if (Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() != "")
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtp.Rows[n]["CHQ_NO"]).Trim() + " DT. " + Convert.ToDateTime(dtp.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }
    //            else
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtp.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtp.Rows[n]["Particulars"]).Trim(), "", dtp.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }





    //            id = id + 1;
    //        }

    //    }





    //    return id;

    //}// End Of function for contra
    //    private int AddReceiptVoucherEntry(DataTable dtr, int id) //function for Receipt
    //{
    //    int l = 0;
    //    for (l = 0; l < dtr.Rows.Count; l++)
    //    {
    //        if (Convert.ToString(dtr.Rows[l]["Tran"]).Trim() == "Cr")
    //        {
    //            AccountTransactionController od7 = new AccountTransactionController();
    //            od7.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[l]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[l]["Party_Name"].ToString().Trim(), "Receipt", dtr.Rows[l]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtr.Rows[l]["Amount"].ToString().Trim()), 1);
    //            id = id + 1;
    //        }

    //    }
    //    int m = 0;
    //    for (m = 0; m < dtr.Rows.Count; m++)
    //    {
    //        if (Convert.ToString(dtr.Rows[m]["Tran"]).Trim() == "Dr")
    //        {
    //            AccountTransactionController od8 = new AccountTransactionController();
    //            od8.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[m]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtr.Rows[m]["Party_Name"].ToString().Trim(), "", dtr.Rows[m]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtr.Rows[m]["Amount"].ToString().Trim()), 0, 1);
    //            id = id + 1;
    //        }

    //    }


    //    int n = 0;
    //    for (n = 0; n < dtr.Rows.Count; n++)
    //    {
    //        if (Convert.ToString(dtr.Rows[n]["Particulars"]).Trim() != "")
    //        {
    //            AccountTransactionController od9 = new AccountTransactionController();
    //            if (Convert.ToString(dtr.Rows[n]["CHQ_NO"]).Trim() != "")
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), "Ch. No. :" + Convert.ToString(dtr.Rows[n]["CHQ_NO"]).Trim() + " DT. " +Convert.ToDateTime(dtr.Rows[n]["CHQ_DATE"]).ToString("dd/MM/yyyy") + " " + Convert.ToString(dtr.Rows[n]["Particulars"]).Trim(), "", dtr.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }
    //            else
    //            {
    //                od9.AddDayBookReportFormat(id, Convert.ToDateTime(dtr.Rows[n]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtr.Rows[n]["Particulars"]).Trim(), "", dtr.Rows[n]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //            }





    //            id = id + 1;
    //        }

    //    }




    //    return id;

    //}// End Of function for Receipt
    //    private int AddJournalVoucherEntry(DataTable dtj, int id)//function for JV
    //    {

    //        int u = 0;
    //        double Amt = 0;
    //        string vtype = string.Empty;
    //        string vno = string.Empty;
    //        string name = string.Empty;
    //        string TrnDate = string.Empty;
    //        string trntype = string.Empty;
    //        int ind = 0;
    //        int bold = 0;

    //        DataTable dtTemp;
    //        for (u = 0; u < dtj.Rows.Count; u++)
    //        {
    //            if (Convert.ToDouble(dtj.Rows[u]["Amount"]) > Amt)
    //            {
    //                Amt = Convert.ToDouble(dtj.Rows[u]["Amount"]);
    //                vtype = "Journal";
    //                vno = Convert.ToString(dtj.Rows[u]["str_voucher_no"]).Trim();
    //                name = Convert.ToString(dtj.Rows[u]["party_name"]).Trim();
    //                trntype = Convert.ToString(dtj.Rows[u]["tran"]).Trim();
    //                TrnDate = Convert.ToString(Convert.ToDateTime(dtj.Rows[u]["transaction_date"]).ToString("dd/MMM/yyyy"));
    //                bold = 1;
    //                ind = u;

    //            }


    //        }


    //        AccountTransactionController od4 = new AccountTransactionController();
    //        if (trntype == "Dr")
    //        {
    //            od4.AddDayBookReportFormat(id, TrnDate, name, vtype, vno, Amt, 0, 1);
    //            id = id + 1;
    //        }
    //        else
    //        {
    //            od4.AddDayBookReportFormat(id, TrnDate, name, vtype, vno, 0, Amt, 1);
    //            id = id + 1;
    //        }
    //        dtTemp = dtj;
    //        dtj.Rows[ind].Delete();
    //        dtj.AcceptChanges();

    //        int y = 0;
    //        for (y = 0; y < dtj.Rows.Count; y++)
    //        {
    //            AccountTransactionController od5 = new AccountTransactionController();

    //            if (Convert.ToString(dtj.Rows[y]["tran"]).Trim() == "Dr")
    //            {
    //                od5.AddDayBookReportFormat(id, Convert.ToDateTime(dtj.Rows[y]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtj.Rows[y]["Party_Name"].ToString().Trim(), "", dtj.Rows[y]["str_voucher_no"].ToString().Trim(), Convert.ToDouble(dtj.Rows[y]["Amount"].ToString().Trim()), 0, 1);
    //                id = id + 1;
    //            }
    //            else
    //            {
    //                od5.AddDayBookReportFormat(id, Convert.ToDateTime(dtj.Rows[y]["Transaction_Date"]).ToString("dd-MMM-yyyy"), dtj.Rows[y]["Party_Name"].ToString().Trim(), "", dtj.Rows[y]["str_voucher_no"].ToString().Trim(), 0, Convert.ToDouble(dtj.Rows[y]["Amount"].ToString().Trim()), 1);
    //                id = id + 1;

    //            }
    //        }


    //        int o = 0;
    //        for (o = 0; o < dtTemp.Rows.Count; o++)
    //        {
    //            if (Convert.ToString(dtTemp.Rows[o]["Particulars"]).Trim() != "")
    //            {
    //                AccountTransactionController od10 = new AccountTransactionController();
    //                od10.AddDayBookReportFormat(id, Convert.ToDateTime(dtTemp.Rows[o]["Transaction_Date"]).ToString("dd-MMM-yyyy"), Convert.ToString(dtTemp.Rows[o]["Particulars"]).Trim(), "", dtTemp.Rows[o]["str_voucher_no"].ToString().Trim(), 0, 0, 0);
    //                id = id + 1;
    //            }

    //        }

    //        return id;



    //    }// End Of function for JV
    protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(ViewState["Todate"])) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "The period of From Date and UpTo Date should not be more than one month", this.Page);
            txtUptoDate.Text = Convert.ToDateTime(ViewState["Todate"]).ToString("dd/MM/yyyy");
            ViewState["Todate"] = txtUptoDate.Text;
            txtUptoDate.Focus();
            return;
        }
        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            ViewState["Todate"] = txtUptoDate.Text;
            return;
        }
        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date.", this);
            SetFinancialYear();
            txtUptoDate.Focus();
            return;
        }
        if (txtAcc.Text.ToString().Trim() != "")
        {
           // btnGo_Click(sender, e);
        }
        txtAcc.Focus();

    }
    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date should be in financial year", this.Page);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }
        string fDate = txtFrmDate.Text;
        string ToDate = DateTime.Parse(fDate).AddMonths(1).ToString();
        ToDate = DateTime.Parse(ToDate).AddDays(-1).ToString();

        txtUptoDate.Text = Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy");
        ViewState["Todate"] = txtUptoDate.Text;
        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            ViewState["Todate"] = txtUptoDate.Text;
            return;
        }
        
        if (txtAcc.Text.ToString().Trim() != "")
        {
           // btnGo_Click(sender, e);
        }
        txtUptoDate.Focus();
    }
    protected void btnReconcile_Click(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() == "")
        {

            objCommon.DisplayMessage(UPDLedger, "Enter The Proper Ledger.", this);
            txtAcc.Focus();
            return;

        }

        if (lstData.Items.Count > 0)
        {
            int i = 0;
            for (i = 0; i < lstData.Items.Count; i++)
            {
                string reconcileDate = string.Empty;
                TextBox txtRecDate = lstData.Items[i].FindControl("txtbankdate") as TextBox;
                HiddenField voucherSqn = lstData.Items[i].FindControl("voucherSqn") as HiddenField;
                HiddenField HDNCHQNO = lstData.Items[i].FindControl("HDNCHQNO") as HiddenField;
                if (txtRecDate != null)
                {
                    if (txtRecDate.Text.ToString().Trim() != "" && txtRecDate.Enabled == true)
                    {
                        reconcileDate = Convert.ToDateTime(txtRecDate.Text).ToString("dd-MMM-yyyy");
                        int partyno = 0;
                        string TranDate = string.Empty;

                        HiddenField hdnTranDate = lstData.Items[i].FindControl("hdnTranDate") as HiddenField;
                        if (hdnTranDate != null)
                        {
                            if (hdnTranDate.Value.ToString().Trim() != "")
                            {
                                TranDate = Convert.ToDateTime(hdnTranDate.Value).ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        string voucherNo = "";

                        HiddenField hdnVchNo = lstData.Items[i].FindControl("hdnVchNo") as HiddenField;
                        if (hdnVchNo != null)
                        {
                            if (hdnVchNo.Value.ToString().Trim() != "")
                            {
                                voucherNo = hdnVchNo.Value.ToString().Trim();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        string voucherType = string.Empty;

                        HiddenField hdnVchType = lstData.Items[i].FindControl("hdnVchType") as HiddenField;
                        if (hdnVchType != null)
                        {
                            if (hdnVchType.Value.ToString().Trim() != "")
                            {
                                if (hdnVchType.Value.ToString().ToLower().Trim() == "receipt")
                                    voucherType = "R";
                                else if (hdnVchType.Value.ToString().ToLower().Trim() == "payment")
                                    voucherType = "P";
                                else if (hdnVchType.Value.ToString().ToLower().Trim() == "journal")
                                    voucherType = "J";
                                else if (hdnVchType.Value.ToString().ToLower().Trim() == "contra")
                                    voucherType = "C";
                            }
                            else
                            {
                                return;

                            }
                        }
                        else
                        {
                            return;
                        }

                        try
                        {
                            AccountTransactionController objacc = new AccountTransactionController();
                            if (reconcileDate != "")
                                objacc.UpdateBankReconcilation(partyno, reconcileDate, TranDate, Session["comp_code"].ToString().Trim(), voucherNo, voucherType, Convert.ToInt32(voucherSqn.Value), HDNCHQNO.Value.ToString());
                                objCommon.DisplayMessage(UPDLedger, "Bank Reconcilation Succeeded.", this);

                            btnGo_Click(sender, e);
                        }
                        catch (Exception)
                        {
                            objCommon.DisplayMessage(UPDLedger, "Error occurred while updating reconcile date.", this);
                        }
                    }
                }
            }
        }

        if (1 == 2)
        {
            if (RptData.Items.Count > 0)
            {

                int i = 0;
                for (i = 0; i < RptData.Items.Count; i++)
                {
                    ListView lvitem = RptData.Items[i].FindControl("lvGrp") as ListView;

                    string reconcileDate = string.Empty;
                    TextBox txtRecDate = RptData.Items[i].FindControl("txtbankdate") as TextBox;
                    HiddenField voucherSqn = RptData.Items[i].FindControl("voucherSqn") as HiddenField;
                    HiddenField HDNCHQNO = RptData.Items[i].FindControl("HDNCHQNO") as HiddenField;
                    if (txtRecDate != null)
                    {
                        if (txtRecDate.Text.ToString().Trim() != "" && txtRecDate.Enabled == true)
                        {
                            reconcileDate = Convert.ToDateTime(txtRecDate.Text).ToString("dd-MMM-yyyy");

                            if (lvitem != null)
                            {
                                if (lvitem.Items.Count > 0)
                                {
                                    int k = 0;
                                    for (k = 0; k < lvitem.Items.Count; k++)
                                    {

                                        TextBox txtbank = lvitem.Items[k].FindControl("txtbankdate") as TextBox;
                                        if (txtbank != null)
                                        {
                                            if (txtbank.Text.ToString().Trim() == "")
                                            {

                                                objCommon.DisplayMessage(UPDLedger, "Enter The bank Reconcillation Date.", this);

                                                //txtbank.Focus();
                                                //return;

                                            }

                                        }
                                        //update reconcillation date to transaction table
                                        int partyno = 0;
                                        HiddenField hdnparty = lvitem.Items[k].FindControl("hdnparty") as HiddenField;
                                        if (hdnparty != null)
                                        {
                                            if (Convert.ToInt16(hdnparty.Value) > 0)
                                            {
                                                partyno = Convert.ToInt16(hdnparty.Value);
                                            }
                                            else
                                            {

                                                //return;

                                            }
                                        }
                                        else
                                        {
                                            //return;
                                        }
                                        string TranDate = string.Empty;

                                        HiddenField hdnTranDate = lvitem.Items[k].FindControl("hdnTranDate") as HiddenField;
                                        if (hdnTranDate != null)
                                        {
                                            if (hdnTranDate.Value.ToString().Trim() != "")
                                            {
                                                TranDate = Convert.ToDateTime(hdnTranDate.Value).ToString("dd-MMM-yyyy");

                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }

                                        //string reconcileDate = string.Empty;
                                        //TextBox txtRecDate = lvitem.Items[k].FindControl("txtbankdate") as TextBox;
                                        //if (txtRecDate != null)
                                        //{
                                        //    if (txtRecDate.Text.ToString().Trim() != "")
                                        //    {
                                        //        reconcileDate = Convert.ToDateTime(txtRecDate.Text).ToString("dd-MMM-yyyy");

                                        //    }
                                        //    else
                                        //    {

                                        //       // return;
                                        //    }
                                        //}
                                        //else {
                                        //    //return;
                                        //}

                                        string voucherNo = "";

                                        HiddenField hdnVchNo = lvitem.Items[k].FindControl("hdnVchNo") as HiddenField;
                                        if (hdnVchNo != null)
                                        {
                                            if (hdnVchNo.Value.ToString().Trim() != "")
                                            {
                                                voucherNo = hdnVchNo.Value.ToString().Trim();

                                            }
                                            else
                                            {
                                                return;

                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }

                                        string voucherType = string.Empty;

                                        //HiddenField hdnVchType = lvitem.Items[k].FindControl("lblHeadVoucherType") as HiddenField;
                                        //if (hdnVchType != null)
                                        //{
                                        //    if (hdnVchType.Value.ToString().Trim() != "")
                                        //    {
                                        //        if (hdnVchType.Value.ToString().Trim() == "Reciept")
                                        //            voucherType = "R";
                                        //        else if (hdnVchType.Value.ToString().Trim() == "Payment")
                                        //            voucherType = "P";
                                        //        else if (hdnVchType.Value.ToString().Trim() == "Journal")
                                        //            voucherType = "J";
                                        //        else if (hdnVchType.Value.ToString().Trim() == "Contra")
                                        //            voucherType = "C";
                                        //    }
                                        //    else
                                        //    {
                                        //        return;

                                        //    }
                                        //}
                                        HiddenField hdnVchType = lvitem.Items[k].FindControl("hdnVchType") as HiddenField;
                                        if (hdnVchType != null)
                                        {
                                            if (hdnVchType.Value.ToString().Trim() != "")
                                            {
                                                if (hdnVchType.Value.ToString().ToLower().Trim() == "receipt")
                                                    voucherType = "R";
                                                else if (hdnVchType.Value.ToString().ToLower().Trim() == "payment")
                                                    voucherType = "P";
                                                else if (hdnVchType.Value.ToString().ToLower().Trim() == "journal")
                                                    voucherType = "J";
                                                else if (hdnVchType.Value.ToString().ToLower().Trim() == "contra")
                                                    voucherType = "C";
                                            }
                                            else
                                            {
                                                return;

                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }

                                        AccountTransactionController objacc = new AccountTransactionController();
                                        if (reconcileDate != "")
                                            objacc.UpdateBankReconcilation(partyno, reconcileDate, TranDate, Session["comp_code"].ToString().Trim(), voucherNo, voucherType, Convert.ToInt32(voucherSqn.Value), HDNCHQNO.Value.ToString());

                                        break;

                                    }
                                }
                            }
                        }
                        else
                        {
                            // return;
                        }
                    }
                    else
                    {
                        //return;
                    }

                }
                objCommon.DisplayMessage(UPDLedger, "Bank Reconcilation Succeded.", this);
                btnGo_Click(sender, e);
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Generate The Bank Reconcilation Data.", this);
                btnGo.Focus();
                return;

            }
        }
        //RptData.Items[0]


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        btnCancel_Click(sender, e);
    }
    protected void btnReconVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAcc.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage("Enter Ledger Name.", this);
                txtAcc.Focus();
                return;

            }
            if (RptData.Items.Count == 0)
            {
                objCommon.DisplayMessage("Record Not Available.", this);
                txtAcc.Focus();
                return;

            }


            ShowReport("LedgerBook", "BankReconcileStatement_4.rpt", "1");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnAllVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAcc.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage("Enter Ledger Name.", this);
                txtAcc.Focus();
                return;

            }
            if (RptData.Items.Count == 0)
            {
                objCommon.DisplayMessage("Record Not Available.", this);
                txtAcc.Focus();
                return;

            }


            ShowReport("LedgerBook", "BankReconcileStatement_4.rpt", "0");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetReconciliationLedger(string prefixText)
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
    protected void txtAcc_TextChanged1(object sender, EventArgs e)
    {
        if (txtAcc.Text.Split('*').Length > 1)
        {
            int partyId = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE=" + txtAcc.Text.Split('*')[1].ToString()));
            double Balance = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "BALANCE", "PARTY_NO=" + partyId));
            if (Balance > 0)
                lblCurBal.Text = Balance.ToString() + " Dr";
            else
            {
                Balance = Balance * -1;
                lblCurBal.Text = Balance.ToString() + " Cr";
            }
        }
        else
        {
            objCommon.DisplayMessage("Enter Ledger Name.", this);
            txtAcc.Focus();
        }
    }
    protected void lstData_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem || e.Item.ItemType == ListViewItemType.DataItem)
        {
            TextBox txtbnkdate = e.Item.FindControl("txtbankdate") as TextBox;
            //Image imgbutton = e.Item.FindControl("imgCal10") as Image;
            Image imgEdit = e.Item.FindControl("btnEdit") as Image;
            if (txtbnkdate != null)
            {
                if (txtbnkdate.Text.ToString().Trim() != "")
                {
                    txtbnkdate.Enabled = false;
                    //imgbutton.Visible = false;
                   // imgEdit.Visible = true;
                }
                else
                {
                    txtbnkdate.Enabled = true;
                    //imgbutton.Visible = true;
                   // imgEdit.Visible = false;
                }

            }
        }
    }
    protected void lstData_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        DataSet ds = null;
        if (e.CommandName.ToString().Trim() == "View")
        {
            LinkButton btnPartyName = e.Item.FindControl("lnkbtnPartyName") as LinkButton;
            int VchSqn = Convert.ToInt32(btnPartyName.CommandArgument.ToString());

            TrialBalanceReportController objTBR = new TrialBalanceReportController();
            ds = objTBR.GetReconDataVoucherWise(Session["comp_code"].ToString(), VchSqn);

            upd_ModalPopupExtender1.Show();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvGrp1.DataSource = ds.Tables[0];
                    lvGrp1.DataBind();
                }
            }

        }
        if (e.CommandName.ToString().Trim() == "BankDateEdit")
        {
            TextBox txtBankDate = e.Item.FindControl("txtbankdate") as TextBox;
            Image img = e.Item.FindControl("imgCal10") as Image;
            //Image imgEdit = e.Item.FindControl("btnEdit") as Image;
            txtBankDate.Enabled = true;
            txtBankDate.Focus();
            img.Visible = true;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //upd_ModalPopupExtender1.Hide();
    }

    public void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

}

