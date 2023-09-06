//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TDS Report                                                     
// CREATION DATE : 23-OCT-2018                                               
// CREATED BY    : NOKHLAL KUMAR                                                
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
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using IITMS.NITPRM;

public partial class ACCOUNT_Acc_TDS_Report : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    SetFinancialYear();
                }
            }
        }
    }

    private void SetFinancialYear()
    {
        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFrmDt.Text = Session["fin_date_from"].ToString();
            txtTodt.Text = Session["fin_date_to"].ToString();
        }
        dtr.Close();
    }
    protected void txtLedger_TextChanged(object sender, EventArgs e)
    {
        int partyId = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE=" + txtLedger.Text.Split('*')[1].ToString()));
        double Balance = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "BALANCE", "PARTY_NO=" + partyId));
        if (Balance > 0)
            lblCurBal.Text = Balance.ToString() + " Dr";
        else
            lblCurBal.Text = Balance.ToString() + " Cr";
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //FinCashBookController objCBC = new FinCashBookController();
        if (txtFrmDt.Text == "" || txtTodt.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter From Date And To Date", this);
            return;
        }
        else if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
        {
            objCommon.DisplayMessage(UPDLedger, "To date should be greater than from date.", this);
            return;
        }

        string FromYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_FROM)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");
        string ToYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_TO)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");

        string quarter = string.Empty;
        string period = string.Empty;
        // int party_no = Convert.ToInt32(txtLedger.Text.ToString().Split('*')[1].ToString());
        int party_no = 0;
        if (Convert.ToInt32(ddlQuarter.SelectedValue) > 0)
        {
            quarter = ddlQuarter.SelectedValue;
        }

        if (quarter == "1")
        {
            period = "From 01.04." + FromYear + " to 30.06." + FromYear;
        }
        else if (quarter == "2")
        {
            period = "From 01.07." + FromYear + " to 30.09." + FromYear;
        }
        else if (quarter == "3")
        {
            period = "From 01.10." + FromYear + " to 31.12." + FromYear;
        }
        else if (quarter == "4")
        {
            period = "From 01.01." + ToYear + " to 31.03." + ToYear;
        }

        lblInstruction.Text = " TAX DEDUCTED AT SOURCE FOR " + ddlQuarter.SelectedItem.Text + " FOR THE YEAR " + FromYear + " - " + ToYear;// +" - " + period.ToString() + ".  ";

        DataSet ds = null;
        ds = objAVC.GetTDSRecords(Session["Comp_Code"].ToString(), party_no, Convert.ToDateTime(txtFrmDt.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd"));

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptTDSGrid.DataSource = ds.Tables[0];
                rptTDSGrid.DataBind();
                pnlTDSGrid.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
                rptTDSGrid.DataSource = null;
                rptTDSGrid.DataBind();
                pnlTDSGrid.Visible = false;
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
            rptTDSGrid.DataSource = null;
            rptTDSGrid.DataBind();
            pnlTDSGrid.Visible = false;
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rptTDSGrid.DataSource = null;
        rptTDSGrid.DataBind();
        ddlQuarter.SelectedValue = "0";
        txtLedger.Text = string.Empty;
        lblInstruction.Text = string.Empty;
        pnlTDSGrid.Visible = false;
    }
    protected void btnShowPDF_Click(object sender, EventArgs e)
    {
        if (txtFrmDt.Text == "" || txtTodt.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter From Date And To Date", this);
            return;
        }
        else if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
        {
            objCommon.DisplayMessage(UPDLedger, "To date should be greater than from date.", this);
            return;
        }

        string FromYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_FROM)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");
        string ToYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_TO)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");

        string quarter = string.Empty;
        string period = string.Empty;
        // int party_no = Convert.ToInt32(txtLedger.Text.ToString().Split('*')[1].ToString());
        int party_no = 0;
        if (Convert.ToInt32(ddlQuarter.SelectedValue) > 0)
        {
            quarter = ddlQuarter.SelectedValue;
        }

        if (quarter == "1")
        {
            period = "From 01.04." + FromYear + " to 30.06." + FromYear;
        }
        else if (quarter == "2")
        {
            period = "From 01.07." + FromYear + " to 30.09." + FromYear;
        }
        else if (quarter == "3")
        {
            period = "From 01.10." + FromYear + " to 31.12." + FromYear;
        }
        else if (quarter == "4")
        {
            period = "From 01.01." + ToYear + " to 31.03." + ToYear;
        }

        string Particular = " TAX DEDUCTED AT SOURCE FOR " + ddlQuarter.SelectedItem.Text + " FOR THE YEAR " + FromYear + " - " + ToYear + " - " + period.ToString() + ".  ";

        ShowReportTDS("TDS Report", "TDSReport.rpt", Particular);

    }

    private void ShowReportTDS(string reportTitle, string rptFileName, string Particular)
    {
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
        string LedgerName = string.Empty;

        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,ACCOUNT," + rptFileName;

        url += "&param=@P_COMP_CODE=" + Session["comp_code"].ToString() + "," + "@P_Particular=" + Particular + "," + "@P_Company_Name=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_Party_No=0" + "," + "@P_FROMDATE="+Convert.ToDateTime(txtFrmDt.Text).ToString("yyyy/MM/dd")+"" + "," + "@P_TODATE="+Convert.ToDateTime(txtTodt.Text).ToString("yyyy/MM/dd");
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

        ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
    }

    protected void btnShowExcel_Click(object sender, EventArgs e)
    {
        if (txtFrmDt.Text == "" || txtTodt.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter From Date And To Date", this);
            return;
        }
        else if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
        {
            objCommon.DisplayMessage(UPDLedger, "To date should be greater than from date.", this);
            return;
        }
        //if (txtLedger.Text == "" || txtLedger.Text == string.Empty || txtLedger.Text == null)
        //{
        //    objCommon.DisplayMessage(UPDLedger, "Please Select Any Ledger", this);
        //    return;
        //}
        ExportToExcel();
    }


    private void SetValue()
    {
        DataTable dtExcel = Session["dtExcel"] as DataTable;
        DataRow dr;
        dr = dtExcel.NewRow();
        for (int i = 0; i < dtExcel.Columns.Count; i++)
        {
            dr["srno"] = "1";
            dr["Party_no"] = "0";
            dr["Party_Name_Address"] = "5";
            dr["VOUCHER_NO"] = "2";
            dr["PANNO"] = "4";
            dr["SECTION_NAME"] = "8";
            dr["PARTICULARS"] = "9";
            //dr["Rate"] = "5";
            //dr["Work_Nature"] = "6";
            //dr["CHQ_NO"] = "7";
            dr["Pay_Date"] = "3";
            dr["Bill_Amt"] = "6";
            dr["TDS"] = "7";
            dr["SURCHARGE"] = "9";
            dr["EDCESS"] = "17";
            dr["TOTAL_TAX_DEPOSIT"] = "11";
           
            //dr["GST"] = "11";
            //dr["Paid_Amt"] = "12";
            dr["TDS_Date"] = "10";
            dr["CHALLAN_NO"] = "12";
            dr["CHALLAN_AMT"] = "13";
            dr["CHALLAN_DATE"] = "14";
           
            dr["BSRCODE"] = "15";
            dr["RECEIPT_NO"] = "16";
        }
        dtExcel.Rows.InsertAt(dr, 0);
        Session["dtExcel"] = dtExcel;
    }

    private void ExportToExcel()
    {
        string FromYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_FROM)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");
        string ToYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_TO)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");

        string quarter = string.Empty;
        string period = string.Empty;
        // int party_no = Convert.ToInt32(txtLedger.Text.ToString().Split('*')[1].ToString());
        int party_no = 0;
        if (Convert.ToInt32(ddlQuarter.SelectedValue) > 0)
        {
            quarter = ddlQuarter.SelectedValue;
        }

        if (quarter == "1")
        {
            period = "From 01.04." + FromYear + " to 30.06." + FromYear;
        }
        else if (quarter == "2")
        {
            period = "From 01.07." + FromYear + " to 30.09." + FromYear;
        }
        else if (quarter == "3")
        {
            period = "From 01.10." + FromYear + " to 31.12." + FromYear;
        }
        else if (quarter == "4")
        {
            period = "From 01.01." + ToYear + " to 31.03." + ToYear;
        }

        DataSet DsExcel = objAVC.GetTDSRecordsExcel(Session["Comp_Code"].ToString(), party_no,Convert.ToDateTime(txtFrmDt.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd"));

        DataView dvData = new DataView(DsExcel.Tables[0]);
        DataTable dtExcelData = new DataTable();
        dtExcelData = dvData.ToTable();
        DataRow Row = dtExcelData.NewRow();
        double GBillAmtTotal = 0, GTDSTotal = 0, GGSTTotal = 0, GPaidAmtTotal = 0;
        Row["Pay_Date"] = "Grand Total";

        if (DsExcel != null)
        {
            if (DsExcel.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DsExcel.Tables[0].Rows.Count; i++)
                {
                    GBillAmtTotal = GBillAmtTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["Bill_Amt"].ToString());
                    GTDSTotal = GTDSTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["TDS"].ToString());
                    // GGSTTotal = GGSTTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["GST"].ToString());
                    //   GPaidAmtTotal = GPaidAmtTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["Paid_Amt"].ToString());
                }
                Row["Bill_Amt"] = GBillAmtTotal.ToString();
                Row["TDS"] = GTDSTotal.ToString();
                //Row["GST"] = GGSTTotal.ToString();
                //Row["Paid_Amt"] = GPaidAmtTotal.ToString();
                Row["Party_no"] = "0";

                dtExcelData.Rows.Add(Row);
                //grdTDSExcel.DataSource = DsExcel.Tables[0];

                string PANno = (DsExcel.Tables[0].Rows[0]["Party_Name_Address"].ToString());
                string pannos = PANno.ToString();
                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                {
                    if ((dtExcelData.Rows[i]["Party_Name_Address"].ToString()).ToUpper() != PANno.ToUpper() && (dtExcelData.Rows[i]["Party_Name_Address"].ToString()) != string.Empty)
                    {
                        if (pannos == string.Empty)
                            pannos = PANno.ToString();
                        else
                            pannos = pannos + "$" + (dtExcelData.Rows[i]["Party_Name_Address"].ToString());
                        //break;
                        PANno = (dtExcelData.Rows[i]["Party_Name_Address"].ToString());
                    }
                    else
                    {
                        PANno = (dtExcelData.Rows[i]["Party_Name_Address"].ToString());
                    }
                }

                string[] temp = pannos.ToString().Split('$');
                string temppartyId = string.Empty;
                DataRow dr = null;

                int c = 0;
                for (int j = 0; j < temp.Length; j++)
                {
                    double BillAmtTotal = 0, TDSTotal = 0, GSTTotal = 0, PaidAmtTotal = 0;
                    temppartyId = (temp[j].ToString());
                    for (int i = 0; i < dtExcelData.Rows.Count; i++)
                    {
                        if ((dtExcelData.Rows[i]["Party_Name_Address"].ToString()).ToUpper() == temppartyId.ToUpper())
                        {
                            BillAmtTotal = BillAmtTotal + Convert.ToDouble(dtExcelData.Rows[i]["Bill_Amt"].ToString());
                            TDSTotal = TDSTotal + Convert.ToDouble(dtExcelData.Rows[i]["TDS"].ToString());
                            // GSTTotal = GSTTotal + Convert.ToDouble(dtExcelData.Rows[i]["GST"].ToString());
                            // PaidAmtTotal = PaidAmtTotal + Convert.ToDouble(dtExcelData.Rows[i]["Paid_Amt"].ToString());
                            c++;
                        }
                    }
                    dr = dtExcelData.NewRow();
                    dr["Pay_Date"] = "Total";
                    dr["Bill_Amt"] = BillAmtTotal.ToString();
                    dr["TDS"] = TDSTotal.ToString();
                    //dr["GST"] = GSTTotal.ToString();
                    //dr["Paid_Amt"] = PaidAmtTotal.ToString();
                    dr["Party_no"] = "0";
                    dtExcelData.Rows.InsertAt(dr, (c));
                    temppartyId = string.Empty;
                    c++;
                }

                Session["dtExcel"] = dtExcelData;
                SetValue();
                DataTable dtExcel = Session["dtExcel"] as DataTable;
                grdTDSExcel.DataSource = dtExcel;
                grdTDSExcel.DataBind();

                //GridViewRow lastrow = grdTDSExcel.Rows[(grdTDSExcel.Rows.Count) - 1];
                //for (int i = 0; i < lastrow.Cells.Count; i++)
                //{
                //    lastrow.Cells[i].Font.Bold = true;
                //}

                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                {
                    if (dtExcelData.Rows[i]["Party_no"].ToString() == "0")
                    {
                        grdTDSExcel.Rows[i].Font.Bold = true;
                    }
                }
            }
            else
            {

                objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
                grdTDSExcel.DataSource = null;
                grdTDSExcel.DataBind();
                pnlTDSGrid.Visible = false;
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
            grdTDSExcel.DataSource = null;
            grdTDSExcel.DataBind();
            pnlTDSGrid.Visible = false;
            return;
        }

        if (DsExcel.Tables[0].Rows.Count > 0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
            HeaderCell.ColumnSpan = 9;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);
            grdTDSExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Text = "TAX DEDUCTED AT SOURCE FOR " + ddlQuarter.SelectedItem.Text + " FOR THE YEAR " + FromYear + " - " + ToYear;// +" - " + period.ToString() + ". ";
            HeaderCell1.ColumnSpan = 9;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            grdTDSExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 9;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow2.Cells.Add(HeaderCell);
            grdTDSExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            string attachment = "attachment; filename=" + ddlQuarter.SelectedItem.Text.Replace(" ", "_") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdTDSExcel.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void SetDataColumn()
    {
        DataTable dt = new DataTable();
        //dt = null;
        if (dt.Rows.Count == 0)
        {
            if (dt.Columns.Count == 0)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = "VCH_SQN";
                dt.Columns.Add(dc);

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "VCH_NO";
                dt.Columns.Add(dc1);

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "PARTY_NO";
                dt.Columns.Add(dc2);

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "PARTY_NAME_ADDRESS";
                dt.Columns.Add(dc3);

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "PANNO";
                dt.Columns.Add(dc4);

                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "SECTION_NAME";
                dt.Columns.Add(dc5);

                DataColumn dc6 = new DataColumn();
                dc6.ColumnName = "RATE";
                dt.Columns.Add(dc6);

                DataColumn dc7 = new DataColumn();
                dc7.ColumnName = "WORK_NATURE";
                dt.Columns.Add(dc7);

                DataColumn dc8 = new DataColumn();
                dc8.ColumnName = "CHQ_NO";
                dt.Columns.Add(dc8);

                DataColumn dc9 = new DataColumn();
                dc9.ColumnName = "PAY_DATE";
                dt.Columns.Add(dc9);

                DataColumn dc10 = new DataColumn();
                dc10.ColumnName = "BILL_AMT";
                dt.Columns.Add(dc10);

                DataColumn dc11 = new DataColumn();
                dc11.ColumnName = "TDS";
                dt.Columns.Add(dc11);

                DataColumn dc12 = new DataColumn();
                dc12.ColumnName = "GST";
                dt.Columns.Add(dc12);

                DataColumn dc13 = new DataColumn();
                dc13.ColumnName = "PAID_AMT";
                dt.Columns.Add(dc13);

                DataColumn dc14 = new DataColumn();
                dc14.ColumnName = "TDS_DATE";
                dt.Columns.Add(dc14);

                DataColumn dc15 = new DataColumn();
                dc15.ColumnName = "CHALLAN_NO";
                dt.Columns.Add(dc15);

                DataColumn dc16 = new DataColumn();
                dc16.ColumnName = "BANK_NAME";
                dt.Columns.Add(dc16);

                DataColumn dc17 = new DataColumn();
                dc17.ColumnName = "BSRCODE";
                dt.Columns.Add(dc17);

                DataColumn dc18 = new DataColumn();
                dc18.ColumnName = "SURCHARGE";
                dt.Columns.Add(dc18);

                DataColumn dc19 = new DataColumn();
                dc19.ColumnName = "EDCESS";
                dt.Columns.Add(dc19);

                DataColumn dc20 = new DataColumn();
                dc20.ColumnName = "TOTAL_TAX_DEPOSIT";
                dt.Columns.Add(dc20);

                DataColumn dc21 = new DataColumn();
                dc21.ColumnName = "CHALLAN_AMT";
                dt.Columns.Add(dc21);

                DataColumn dc22 = new DataColumn();
                dc22.ColumnName = "CHALLAN_DATE";
                dt.Columns.Add(dc22);

                DataColumn dc23 = new DataColumn();
                dc23.ColumnName = "RECEIPT_NO";
                dt.Columns.Add(dc23);

                Session["Datatable"] = dt;
            }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtFrmDt.Text == "" || txtTodt.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter From Date And To Date", this);
            return;
        }
        else if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
        {
            objCommon.DisplayMessage(UPDLedger, "To date should be greater than from date.", this);
            return;
        }
        //if (txtLedger.Text == "" || txtLedger.Text == string.Empty || txtLedger.Text == null)
        //{
        //    objCommon.DisplayMessage(UPDLedger, "Please Select Any Ledger", this);
        //    return;
        //}

        SetDataColumn();
        DataTable dtTDS = new DataTable();
        dtTDS = Session["Datatable"] as DataTable;

        try
        {
            int i = 0;
            foreach (RepeaterItem rptItems in rptTDSGrid.Items)
            {
                HiddenField hdnVchSQN = (HiddenField)rptItems.FindControl("hdnVchSQN");
                HiddenField hdnVCH_No = (HiddenField)rptItems.FindControl("hdnVCH_No");
                HiddenField hdnPartyno = (HiddenField)rptItems.FindControl("hdnPartyno");
                HiddenField hdnPartyName_Address = (HiddenField)rptItems.FindControl("hdnPartyName_Address");
                HiddenField hdnPanno = (HiddenField)rptItems.FindControl("hdnPanno");
                HiddenField hdnSectionName = (HiddenField)rptItems.FindControl("hdnSectionName");
                HiddenField hdnRate = (HiddenField)rptItems.FindControl("hdnRate");
                HiddenField hdnWorkNature = (HiddenField)rptItems.FindControl("hdnWorkNature");
                HiddenField hdnChqNo = (HiddenField)rptItems.FindControl("hdnChqNo");
                HiddenField hdnPayDate = (HiddenField)rptItems.FindControl("hdnPayDate");
                HiddenField hdnBillDate = (HiddenField)rptItems.FindControl("hdnBillDate");
                HiddenField hdnTDS = (HiddenField)rptItems.FindControl("hdnTDS");
                //HiddenField hdnGST = (HiddenField)rptItems.FindControl("hdnGST");
                //HiddenField hdnPaidAmt = (HiddenField)rptItems.FindControl("hdnPaidAmt");
                HiddenField hdnTDSDate = (HiddenField)rptItems.FindControl("hdnTDSDate");
                TextBox txtChallanNo = (TextBox)rptItems.FindControl("txtChallanNo");
                TextBox txtBankName = (TextBox)rptItems.FindControl("txtBankName");
                TextBox txtBSRCode = (TextBox)rptItems.FindControl("txtBSRCode");

                TextBox txtSurcharge = (TextBox)rptItems.FindControl("txtSurcharge");
                TextBox txtEdcess = (TextBox)rptItems.FindControl("txtEdcess");
                TextBox txtTotalTaxDeposit = (TextBox)rptItems.FindControl("txtTotalTaxDeposit");
                TextBox txtChallanAmt = (TextBox)rptItems.FindControl("txtChallanAmt");
                TextBox txtChallanDate = (TextBox)rptItems.FindControl("txtChallanDate");
                TextBox txtReceiptNo = (TextBox)rptItems.FindControl("txtReceiptNo");


                if (txtChallanNo.Text != "" || txtBSRCode.Text != "")
                {
                    DataRow row;
                    row = dtTDS.NewRow();
                    row["VCH_SQN"] = (hdnVchSQN.Value).ToString();
                    row["VCH_NO"] = Convert.ToInt32(hdnVCH_No.Value);
                    row["PARTY_NO"] = Convert.ToInt32(hdnPartyno.Value);
                    row["PARTY_NAME_ADDRESS"] = (hdnPartyName_Address.Value).ToString();
                    row["PANNO"] = (hdnPanno.Value).ToString();
                    row["SECTION_NAME"] = (hdnSectionName.Value).ToString();
                    row["RATE"] = hdnRate.Value == "" ? 0 : Convert.ToDouble(hdnRate.Value);
                    row["WORK_NATURE"] = (hdnWorkNature.Value).ToString();
                    row["CHQ_NO"] = (hdnChqNo.Value).ToString();
                    row["PAY_DATE"] = Convert.ToDateTime(hdnPayDate.Value).ToString("dd-MMM-yyyy");
                    row["BILL_AMT"] = Convert.ToDouble(hdnBillDate.Value);
                    row["TDS"] = Convert.ToDouble(hdnTDS.Value);
                    row["GST"] = 0; //Convert.ToDouble(hdnGST.Value);
                    row["PAID_AMT"] = 0; //Convert.ToDouble(hdnPaidAmt.Value);
                    row["TDS_DATE"] = Convert.ToDateTime(hdnTDSDate.Value).ToString("dd-MMM-yyyy");
                    row["CHALLAN_NO"] = (txtChallanNo.Text).ToString();
                    row["BANK_NAME"] = (txtBankName.Text).ToString();
                    row["BSRCODE"] = (txtBSRCode.Text).ToString();

                    row["SURCHARGE"] = Convert.ToDouble(txtSurcharge.Text);
                    row["EDCESS"] = Convert.ToDouble(txtEdcess.Text);
                    row["TOTAL_TAX_DEPOSIT"] = Convert.ToDouble(txtTotalTaxDeposit.Text);
                    row["CHALLAN_AMT"] = Convert.ToDouble(txtChallanAmt.Text);


                    if (txtChallanDate.Text != string.Empty)
                    row["CHALLAN_DATE"] = Convert.ToDateTime(txtChallanDate.Text).ToString("dd-MMM-yyyy"); //(txtChallanDate.Text).ToString();
                    else
                        row["CHALLAN_DATE"] = DBNull.Value.ToString();
                    row["RECEIPT_NO"] = (txtReceiptNo.Text).ToString();

                    dtTDS.Rows.Add(row);
                    i++;
                }
            }
            if (i == 0)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Enter atleast one Challan N0 and BSRCode", this.Page);
                return;
            }
            int ret = objAVC.InsertTDSTran(dtTDS, Convert.ToInt32(Session["colcode"].ToString()), Session["comp_code"].ToString(), Convert.ToInt32(Session["userno"].ToString()));

            if (ret == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "TDS Details Inserted SuccessFully", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Transaction is not possible, Try After Sometime....!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Exception occur please try again", this.Page);
            //btnSave.Enabled = true;
        }
    }
    protected void btnClearGrid_Click(object sender, EventArgs e)
    {
        rptTDSGrid.DataSource = null;
        rptTDSGrid.DataBind();
        ddlQuarter.SelectedValue = "0";
        txtLedger.Text = string.Empty;
        lblInstruction.Text = string.Empty;
        pnlTDSGrid.Visible = false;
    }


    // added by tanu 09_12_2021 for show data in excel before submit
    protected void btnbeforeExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (txtFrmDt.Text == "" || txtTodt.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter From Date And To Date", this);
            return;
        }
        else if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
        {
            objCommon.DisplayMessage(UPDLedger, "To date should be greater than from date.", this);
            return;
        }

        ExportToExcelBeforeSubmition();
    }

    private void SetValueBeforeSubmit()
    {
        DataTable dtExcel = Session["dtExcel"] as DataTable;
        DataRow dr;
        dr = dtExcel.NewRow();
        for (int i = 0; i < dtExcel.Columns.Count; i++)
        {
            dr["srno"] = "1";
            dr["Party_no"] = "0";
            dr["VOUCHER_NO"] = "2";
            dr["PANNO"] = "4";
            dr["SECTION_NAME"] = "8";
            dr["PARTICULARS"] = "9";
          
            dr["Pay_Date"] = "3";
            dr["Bill_Amt"] = "6";
            dr["Party_Name_Address"] = "5";
            dr["TDS"] = "7";
            dr["SURCHARGE"] = "10";
    
        }
        dtExcel.Rows.InsertAt(dr, 0);
        Session["dtExcel"] = dtExcel;
    }

    private void ExportToExcelBeforeSubmition()
    {
        string FromYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_FROM)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");
        string ToYear = objCommon.LookUp("Acc_Company", "YEAR(COMPANY_FINDATE_TO)", "COMPANY_CODE='" + Session["Comp_Code"].ToString() + "'");

        string quarter = string.Empty;
        string period = string.Empty;
        // int party_no = Convert.ToInt32(txtLedger.Text.ToString().Split('*')[1].ToString());
        int party_no = 0;
        if (Convert.ToInt32(ddlQuarter.SelectedValue) > 0)
        {
            quarter = ddlQuarter.SelectedValue;
        }

        if (quarter == "1")
        {
            period = "From 01.04." + FromYear + " to 30.06." + FromYear;
        }
        else if (quarter == "2")
        {
            period = "From 01.07." + FromYear + " to 30.09." + FromYear;
        }
        else if (quarter == "3")
        {
            period = "From 01.10." + FromYear + " to 31.12." + FromYear;
        }
        else if (quarter == "4")
        {
            period = "From 01.01." + ToYear + " to 31.03." + ToYear;
        }

        DataSet DsExcel = objAVC.GetTDSRecords(Session["Comp_Code"].ToString(), party_no, Convert.ToDateTime(txtFrmDt.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd"));

        DataView dvData = new DataView(DsExcel.Tables[0]);
        DataTable dtExcelData = new DataTable();
        dtExcelData = dvData.ToTable();
        DataRow Row = dtExcelData.NewRow();
        double GBillAmtTotal = 0, GTDSTotal = 0, GGSTTotal = 0, GPaidAmtTotal = 0;
    //    Row["Pay_Date"] = "Grand Total";

        if (DsExcel != null)
        {
            if (DsExcel.Tables[0].Rows.Count > 0)
            {
                //for (int i = 0; i < DsExcel.Tables[0].Rows.Count; i++)
                //{
                //    GBillAmtTotal = GBillAmtTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["Bill_Amt"].ToString());
                //    GTDSTotal = GTDSTotal + Convert.ToDouble(DsExcel.Tables[0].Rows[i]["TDS"].ToString());
                    
                //}
                //Row["Bill_Amt"] = GBillAmtTotal.ToString();
                //Row["TDS"] = GTDSTotal.ToString();
                //Row["Party_no"] = "0";

                dtExcelData.Rows.Add(Row);
               
                string PANno = (DsExcel.Tables[0].Rows[0]["Party_Name_Address"].ToString());
                string pannos = PANno.ToString();
                //for (int i = 0; i < dtExcelData.Rows.Count; i++)
                //{
                //    if ((dtExcelData.Rows[i]["Party_Name_Address"].ToString()).ToUpper() != PANno.ToUpper() && (dtExcelData.Rows[i]["Party_Name_Address"].ToString()) != string.Empty)
                //    {
                //        if (pannos == string.Empty)
                //            pannos = PANno.ToString();
                //        else
                //            pannos = pannos + "$" + (dtExcelData.Rows[i]["Party_Name_Address"].ToString());
                      
                //        PANno = (dtExcelData.Rows[i]["Party_Name_Address"].ToString());
                //    }
                //    else
                //    {
                //        PANno = (dtExcelData.Rows[i]["Party_Name_Address"].ToString());
                //    }
                //}

                string[] temp = pannos.ToString().Split('$');
                string temppartyId = string.Empty;
                DataRow dr = null;

                int c = 0;
                //for (int j = 0; j < temp.Length; j++)
                //{
                //    double BillAmtTotal = 0, TDSTotal = 0, GSTTotal = 0, PaidAmtTotal = 0;
                //    temppartyId = (temp[j].ToString());
                //    for (int i = 0; i < dtExcelData.Rows.Count; i++)
                //    {
                //        if ((dtExcelData.Rows[i]["Party_Name_Address"].ToString()).ToUpper() == temppartyId.ToUpper())
                //        {
                //            BillAmtTotal = BillAmtTotal + Convert.ToDouble(dtExcelData.Rows[i]["Bill_Amt"].ToString());
                //            TDSTotal = TDSTotal + Convert.ToDouble(dtExcelData.Rows[i]["TDS"].ToString());
                          
                //            c++;
                //        }
                //    }
                //    dr = dtExcelData.NewRow();
                //    dr["Pay_Date"] = "Total";
                //    dr["Bill_Amt"] = BillAmtTotal.ToString();
                //    dr["TDS"] = TDSTotal.ToString();
                //    dr["Party_no"] = "0";
                //    dtExcelData.Rows.InsertAt(dr, (c));
                //    temppartyId = string.Empty;
                //    c++;
                //}

                Session["dtExcel"] = dtExcelData;
                SetValueBeforeSubmit();
                DataTable dtExcel = Session["dtExcel"] as DataTable;
                grdTDSExcel.DataSource = dtExcel;
                grdTDSExcel.DataBind();

             

                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                {
                    if (dtExcelData.Rows[i]["Party_no"].ToString() == "0")
                    {
                        grdTDSExcel.Rows[i].Font.Bold = true;
                    }
                }
            }
            else
            {

                objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
                grdTDSExcel.DataSource = null;
                grdTDSExcel.DataBind();
                pnlTDSGrid.Visible = false;
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Data Not Found", this);
            grdTDSExcel.DataSource = null;
            grdTDSExcel.DataBind();
            pnlTDSGrid.Visible = false;
            return;
        }

        if (DsExcel.Tables[0].Rows.Count > 0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
            HeaderCell.ColumnSpan = 9;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);
            grdTDSExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Text = "TAX DEDUCTED AT SOURCE FOR " + ddlQuarter.SelectedItem.Text + " FOR THE YEAR " + FromYear + " - " + ToYear;// +" - " + period.ToString() + ". ";
            HeaderCell1.ColumnSpan = 9;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            grdTDSExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 9;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow2.Cells.Add(HeaderCell);
            grdTDSExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            string attachment = "attachment; filename=" + ddlQuarter.SelectedItem.Text.Replace(" ", "_") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdTDSExcel.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

}