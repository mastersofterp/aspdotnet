//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : FINANCE
// CREATION DATE : 28-SEPTEMBER-2021                                               
// CREATED BY    : VIDISHA KAMATKAR                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and Save cheque date and cheque no of bulk Payment
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Data;
using System.Data.Linq;

public partial class ACCOUNT_BulkVoucherDetails : System.Web.UI.Page
{

    Common objCommon = new Common();
    AccountTransactionController objPC1 = new AccountTransactionController();

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
        //This procedure is called RPDATA to add in treeview for receipt entry or for payment entry
        //called proc GETRECPAYMENTDATA,ALLOTSEQNOTORPGROUP to allot sequence no. to group

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        //Session["WithoutCashBank"] = "N";

        if (!Page.IsPostBack)
        {
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

                    objCommon.DisplayUserMessage(UPDLedger, "Select company/cash book.", this);


                    //objCommon.DisplayUserMessage(UPDLedger,"Select company/cash book.", this);

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    //txtFrmDate.Text = DateTime.Now.ToShortDateString();
                    //txtUptoDate.Text = DateTime.Now.ToShortDateString();
                    SetFinancialYear();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();



                    //GETRECPAYMENTDATA();
                    //ALLOTSEQNOTORPGROUP();

                    ////'*** TO ADD IN TREEVIEW FOR RECEIPT ENTRY *********
                    ////TEMP_ACC_RPTRPDATA
                    //objrpc.DeletePartyFromRptrp(0, 3);
                    //sOp = "R";
                    //ADDOPBALANCE("1", tvRec);
                    //RPDATA(1, tvRec);
                    //CURBALANCE = 0;

                    ////   '*** TO ADD IN TREEVIEW FOR PAYMENT ENTRY *********
                    //RPDATA(2,tvPay); 
                    //sOp = "P";
                    //ADDOPBALANCE("CL", tvRec);
                    //CURBALANCE = 0;
                    ////    Check1.Value = 0

                }
            }
            CheckPageAuthorization();
            //SetFinancialYear();
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
            //txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            //txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();


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


    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }



    protected void btnReport_Click(object sender, EventArgs e)
    {
        //if (txtFrmDate.Text.ToString().Trim() == "")
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "Enter From Date", this);
        //    txtFrmDate.Focus();
        //    return;
        //}
        //if (txtUptoDate.Text.ToString().Trim() == "")
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "Enter Upto Date", this);
        //    txtUptoDate.Focus();
        //    return;
        //}

        //if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
        //    txtUptoDate.Focus();
        //    return;
        //}

        //DataSet rsfilter = objrpc.Get_RP_OPBAL(0, txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), 35);

        //for (int i = 0; i < rsfilter.Tables[0].Rows.Count; i++)
        //{
        //    int m1,m3,ii;

        //    if (Convert.ToInt32(rsfilter.Tables[0].Rows[i]["r"]) > Convert.ToInt32(rsfilter.Tables[0].Rows[i]["p"]))
        //    {
        //        m1 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["r"]) - Convert.ToInt32(rsfilter.Tables[0].Rows[i]["p"]);
        //        m3 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["psq"]);
        //        for (int j = 0; j < m1; j++)
        //        {
        //            m3 += 1;
        //            objrp.Rph_No = 2;
        //            objrp.Gr_No = m3;
        //            objrpc.InsRptrp(objrp, 5);
        //        }
        //    }
        //    else
        //    {
        //        m1 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["p"]) - Convert.ToInt32(rsfilter.Tables[0].Rows[i]["r"]);
        //        m3 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["rsq"]);
        //        for (int j = 0; j < m1; j++)
        //        {
        //            m3 += 1;
        //            objrp.Rph_No = 1;
        //            objrp.Gr_No = m3;
        //            objrpc.InsRptrp(objrp, 5);
        //        }
        //    }


        //}

        //if (rdbFormat1.Checked)
        //{
        //    ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_New.rpt");
        //}
        //else if (rdbFormat2.Checked)
        //{
        //    ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_NewFormt2.rpt");
        //}
        //else if (rdbFormat3.Checked)
        //{
        //    ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_NewFormt3.rpt");
        //}

    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAcc(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAgainstAccLedger(prefixText);
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

    public void BindListView()
    {
        int Party_no = 0;
        if (txtAgainstAcc.Text != string.Empty)
        {
            Party_no = Convert.ToInt32(txtAgainstAcc.Text.ToString().Trim().Split('*')[0].ToString());
        }
        else
        {
            Party_no = 0;
        }

        DataSet ds = new DataSet();
        ds = objPC1.GetBulkPaymentDetailsInfo(ddlVoucherType.SelectedValue, Convert.ToDateTime(txtFrmDate.Text), Session["comp_code"].ToString(), Party_no);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvBVoucher.DataSource = ds;
            lvBVoucher.DataBind();
            //dpinfo.Visible = true;
        }
        else
        {
            lvBVoucher.DataSource = null;
            lvBVoucher.DataBind();
            //dpinfo.Visible = false;
        }
    }

    public void BindVoucherListView()
    {
        int Party_no = 0;
        if (txtAgainstAcc.Text != string.Empty)
        {
            Party_no = Convert.ToInt32(txtAgainstAcc.Text.ToString().Trim().Split('*')[0].ToString());
        }
        else
        {
            Party_no = 0;
        }

        DataSet ds = new DataSet();
        ds = objPC1.GetBulkPaymentDetailsInfo(ddlVoucherType.SelectedValue, Convert.ToDateTime(txtFrmDate.Text), Session["comp_code"].ToString(), Party_no);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvVoucher.DataSource = ds;
            lvVoucher.DataBind();
            //dpinfo.Visible = true;
        }
        else
        {
            lvVoucher.DataSource = null;
            lvVoucher.DataBind();
            //dpinfo.Visible = false;
        }
    }

    public void BindEmployeeListView(string VoucherSqn)
    {
        DataSet ds = new DataSet();
        ds = objPC1.GetBulkPaymentEmployeeDetails(VoucherSqn, Session["comp_code"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvEmp.DataSource = ds;
            lvEmp.DataBind();
            //dpinfo.Visible = true;
        }
        else
        {
            lvEmp.DataSource = null;
            lvEmp.DataBind();
            //dpinfo.Visible = false;
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            //string ClMode;
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            // url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + ",@P_COMP_CODE=" + Session["comp_code"] + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnDetails_Click(object sender, EventArgs e)
    {
        Button btnDetail = sender as Button;
        string VchSqn = btnDetail.CommandArgument;
        BindEmployeeListView(VchSqn);
        btnSubmit.Visible = true;
        btnBack.Visible = true;
        pnlVch.Visible = false;
        pnlEmployee.Visible = true;
        divemp.Visible = true;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlVoucherType.SelectedValue == "BK")
        {
            divBulk.Visible = true;
            divOther.Visible = false;
            BindListView();

        }
        else
        {
            divOther.Visible = true;
            divBulk.Visible = false;
            BindVoucherListView();

        }
        btnSave.Visible = true;
    }
    protected void dpinfo_PreRender(object sender, EventArgs e)
    {

    }
    protected void dpDetail_PreRender(object sender, EventArgs e)
    {

    }

    public void BulkVoucher()
    {
        int i = 0;
        foreach (ListViewDataItem items in lvBVoucher.Items)
        {
            CheckBox chkSelect1 = items.FindControl("chkSelect") as CheckBox;
            if (chkSelect1.Checked)
            {
                i = 1;
                break;
            }
        }
        if (i == 0)
        {
            MessageBox("Please Select Atleast One Voucher");
            return;
        }

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add(new DataColumn("CHEQUENO", typeof(string)));
        dtVoucher.Columns.Add(new DataColumn("VOUCHER_SQN", typeof(string)));
        dtVoucher.Columns.Add(new DataColumn("VOUCHER_NO", typeof(string)));

        foreach (ListViewDataItem lvitem in lvBVoucher.Items)
        {
            HiddenField hdnVchNo = lvitem.FindControl("hdnVchNo") as HiddenField;
            HiddenField hdnVchSeq = lvitem.FindControl("hdnVchSeq") as HiddenField;
            TextBox txtChqNo = lvitem.FindControl("txtChqNo") as TextBox;
            CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;

            if (chkSelect.Checked)
            {
                DataRow dr = dtVoucher.NewRow();
                dr["CHEQUENO"] = txtChqNo.Text;
                dr["VOUCHER_SQN"] = hdnVchSeq.Value;
                dr["VOUCHER_NO"] = hdnVchNo.Value;

                dtVoucher.Rows.Add(dr);
                dtVoucher.AcceptChanges();
            }
        }
        DateTime chqDate = Convert.ToDateTime(txtChqDate.Text);
        int result = objPC1.InsertBulkVoucherChequeNo(dtVoucher, Session["comp_code"].ToString().Trim(), chqDate);
        if (result == 1)
        {
            MessageBox("Record Saved Successfully.");
            Clear();
        }
        else
        { MessageBox("Transaction Failed! Please Try Again Later"); }
    }

    public void OtherVoucher()
    {
        int i = 0;
        foreach (ListViewDataItem items in lvVoucher.Items)
        {
            CheckBox chkSelect1 = items.FindControl("chkSelect") as CheckBox;
            if (chkSelect1.Checked)
            {
                i = 1;
                break;
            }
        }
        if (i == 0)
        {
            MessageBox("Please Select Atleast One Voucher");
            return;
        }

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add(new DataColumn("CHEQUENO", typeof(string)));
        dtVoucher.Columns.Add(new DataColumn("VOUCHER_SQN", typeof(string)));
        dtVoucher.Columns.Add(new DataColumn("VOUCHER_NO", typeof(string)));

        foreach (ListViewDataItem lvitem in lvVoucher.Items)
        {
            HiddenField hdnVchNo = lvitem.FindControl("hdnVchNo") as HiddenField;
            HiddenField hdnVchSeq = lvitem.FindControl("hdnVchSeq") as HiddenField;
            TextBox txtChqNo = lvitem.FindControl("txtChqNo") as TextBox;
            CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;

            if (chkSelect.Checked)
            {
                DataRow dr = dtVoucher.NewRow();
                dr["CHEQUENO"] = txtChqNo.Text;
                dr["VOUCHER_SQN"] = hdnVchSeq.Value;
                dr["VOUCHER_NO"] = hdnVchNo.Value;

                dtVoucher.Rows.Add(dr);
                dtVoucher.AcceptChanges();
            }
        }

        DateTime chqDate = Convert.ToDateTime(txtChqDate.Text);

        int result = objPC1.InsertOtherVoucherChequeNo(dtVoucher, Session["comp_code"].ToString().Trim(), chqDate);
        if (result == 1)
        {
            MessageBox("Record Saved Successfully.");
            Clear();
        }
        else
        { MessageBox("Transaction Failed! Please Try Again Later"); }
    }
    public void Clear()
    {
        txtChqDate.Text = string.Empty;

        lvBVoucher.DataSource = null;
        lvBVoucher.DataBind();

        lvEmp.DataSource = null;
        lvEmp.DataBind();

        lvVoucher.DataSource = null;
        lvVoucher.DataBind();

        btnSave.Visible = false;
        btnSubmit.Visible = false;
        pnlVch.Visible = true; ;
        pnlEmployee.Visible = false;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlVoucherType.SelectedValue == "BK")
            {
                //BulkVoucher();
                int i = 0;
                foreach (ListViewDataItem items in lvBVoucher.Items)
                {
                    CheckBox chkSelect1 = items.FindControl("chkSelect") as CheckBox;
                    if (chkSelect1.Checked)
                    {
                        i = 1;
                        break;
                    }
                }
                if (i == 0)
                {
                    MessageBox("Please Select Atleast One Voucher");
                    return;
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add(new DataColumn("CHEQUENO", typeof(string)));
                dtVoucher.Columns.Add(new DataColumn("VOUCHER_SQN", typeof(string)));
                dtVoucher.Columns.Add(new DataColumn("VOUCHER_NO", typeof(string)));

                foreach (ListViewDataItem lvitem in lvBVoucher.Items)
                {
                    HiddenField hdnVchNo = lvitem.FindControl("hdnVchNo") as HiddenField;
                    HiddenField hdnVchSeq = lvitem.FindControl("hdnVchSeq") as HiddenField;
                    TextBox txtChqNo = lvitem.FindControl("txtChqNo") as TextBox;
                    CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        if (txtChqNo.Text.Trim() == "")
                        {
                            MessageBox("Please Enter Cheque Number For All Selected Voucher");
                            return;
                        }
                        DataRow dr = dtVoucher.NewRow();
                        dr["CHEQUENO"] = txtChqNo.Text;
                        dr["VOUCHER_SQN"] = hdnVchSeq.Value;
                        dr["VOUCHER_NO"] = hdnVchNo.Value;

                        dtVoucher.Rows.Add(dr);
                        dtVoucher.AcceptChanges();
                    }
                }
                DateTime chqDate = Convert.ToDateTime(txtChqDate.Text);
                int result = objPC1.InsertBulkVoucherChequeNo(dtVoucher, Session["comp_code"].ToString().Trim(), chqDate);
                if (result == 1)
                {
                   MessageBox("Record Saved Successfully.");
                    Clear();
                    BindListView();
                }
                else
                { MessageBox("Transaction Failed! Please Try Again Later"); }
            }
            else
            {
                //OtherVoucher();
                int i = 0;
                foreach (ListViewDataItem items in lvVoucher.Items)
                {
                    CheckBox chkSelect1 = items.FindControl("chkSelect") as CheckBox;
                    if (chkSelect1.Checked)
                    {
                        i = 1;
                        break;
                    }
                }
                if (i == 0)
                {
                    MessageBox("Please Select Atleast One Voucher");
                    return;
                }

                

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add(new DataColumn("CHEQUENO", typeof(string)));
                dtVoucher.Columns.Add(new DataColumn("VOUCHER_SQN", typeof(string)));
                dtVoucher.Columns.Add(new DataColumn("VOUCHER_NO", typeof(string)));

                foreach (ListViewDataItem lvitem in lvVoucher.Items)
                {
                    HiddenField hdnVchNo = lvitem.FindControl("hdnVchNo") as HiddenField;
                    HiddenField hdnVchSeq = lvitem.FindControl("hdnVchSeq") as HiddenField;
                    TextBox txtChqNo = lvitem.FindControl("txtChqNo") as TextBox;
                    CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        if (txtChqNo.Text.Trim() == "")
                        {
                            MessageBox("Please Enter Cheque Number For All Selected Voucher");
                            return;
                        }
                        DataRow dr = dtVoucher.NewRow();
                        dr["CHEQUENO"] = txtChqNo.Text;
                        dr["VOUCHER_SQN"] = hdnVchSeq.Value;
                        dr["VOUCHER_NO"] = hdnVchNo.Value;

                        dtVoucher.Rows.Add(dr);
                        dtVoucher.AcceptChanges();
                    }
                }

                DateTime chqDate = Convert.ToDateTime(txtChqDate.Text);

                int result = objPC1.InsertOtherVoucherChequeNo(dtVoucher, Session["comp_code"].ToString().Trim(), chqDate);
                if (result == 1)
                {
                    MessageBox("Record Saved Successfully.");                    
                    Clear();
                    BindVoucherListView();
                }
                else
                { MessageBox("Transaction Failed! Please Try Again Later"); }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        foreach (ListViewDataItem items in lvEmp.Items)
        {
            TextBox txtChqNo = items.FindControl("txtChqNo") as TextBox;
            if (txtChqNo.Text.Trim() == string.Empty)
            {
                MessageBox("Please Enter Cheque Number For All Employee");
                return;
            }
        }


        int voucherno = 0;
        int voucherSeq = 0;
        DateTime chqDate = Convert.ToDateTime(txtChDate.Text);
        DataTable dtPayee = new DataTable();
        dtPayee.Columns.Add(new DataColumn("CHEQUENO", typeof(string)));
        dtPayee.Columns.Add(new DataColumn("EMPID", typeof(int)));

        foreach (ListViewDataItem lvitem in lvEmp.Items)
        {
            HiddenField hdnVchNo = lvitem.FindControl("hdnVchNo") as HiddenField;
            HiddenField hdnVchSeq = lvitem.FindControl("hdnVchSeq") as HiddenField;
            HiddenField hdnEmp = lvitem.FindControl("hdnEmp") as HiddenField;
            TextBox txtChqNo = lvitem.FindControl("txtChqNo") as TextBox;

            DataRow dr = dtPayee.NewRow();
            dr["CHEQUENO"] = txtChqNo.Text.Trim();
            dr["EMPID"] = Convert.ToInt32(hdnEmp.Value);

            dtPayee.Rows.Add(dr);
            dtPayee.AcceptChanges();

            voucherno = Convert.ToInt32(hdnVchNo.Value);
            voucherSeq = Convert.ToInt32(hdnVchSeq.Value);
        }

        int result = objPC1.InsertBulkEmployeeVoucherChequeNo(dtPayee, voucherSeq, voucherno, Session["comp_code"].ToString().Trim(), chqDate);
        if (result == 1)
        {
            MessageBox("Record Saved Successfully.");
            //Clear();
            btnSubmit.Visible = false;
            btnBack.Visible = false;
            pnlVch.Visible = true;
            pnlEmployee.Visible = false;
            divemp.Visible = false;
            BindListView();
        }
        else
        { MessageBox("Transaction Failed! Please Try Again Later"); }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnSubmit.Visible = false;
        pnlVch.Visible = true; ;
        pnlEmployee.Visible = false;
    }
    protected void dpVch_PreRender(object sender, EventArgs e)
    {

    }
}