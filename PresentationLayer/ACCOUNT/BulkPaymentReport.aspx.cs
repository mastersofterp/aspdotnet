//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : FINANCE
// CREATION DATE : 28-SEPTEMBER-2021                                              
// CREATED BY    : VIDISHA KAMATKAR                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and print the Bulk Payment Report
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using System.Transactions;
using System.Text.RegularExpressions;
using IITMS.NITPRM;

public partial class ACCOUNT_BulkPaymentReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    //CombinedCashBankBook objCcbb;
   // ReceiptPayment objrp = new ReceiptPayment();
    //CombinedCashBankBookController objCcbc;
    ReceiptPaymentController objrpc;
    //public int q = 0;

    //double CURBALANCE = 0.0;  //Variable is used to store current balance
    //string GRNO;           // used to store group no
    //int SEQNO1;            //used to store sequence no.
    //string sOp;             //used to store opening balance

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
            //objCcbc = new CombinedCashBankBookController();
            objrpc = new ReceiptPaymentController();

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

                    FillPaymentMode();

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

    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    if (txtFrmDate.Text.ToString().Trim() == "")
    //    {
    //        objCommon.DisplayUserMessage(UPDLedger, "Enter From Date", this);
    //        txtFrmDate.Focus();
    //        return;
    //    }
    //    if (txtUptoDate.Text.ToString().Trim() == "")
    //    {
    //        objCommon.DisplayUserMessage(UPDLedger, "Enter Upto Date", this);
    //        txtUptoDate.Focus();
    //        return;
    //    }

    //    if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
    //    {
    //        objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //        txtUptoDate.Focus();
    //        return;
    //    }

    //    //DataSet rsfilter = objrpc.Get_RP_OPBAL(0, txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), 35);

    //    //for (int i = 0; i < rsfilter.Tables[0].Rows.Count; i++)
    //    //{
    //    //    int m1,m3,ii;

    //    //    if (Convert.ToInt32(rsfilter.Tables[0].Rows[i]["r"]) > Convert.ToInt32(rsfilter.Tables[0].Rows[i]["p"]))
    //    //    {
    //    //        m1 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["r"]) - Convert.ToInt32(rsfilter.Tables[0].Rows[i]["p"]);
    //    //        m3 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["psq"]);
    //    //        for (int j = 0; j < m1; j++)
    //    //        {
    //    //            m3 += 1;
    //    //            objrp.Rph_No = 2;
    //    //            objrp.Gr_No = m3;
    //    //            objrpc.InsRptrp(objrp, 5);
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        m1 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["p"]) - Convert.ToInt32(rsfilter.Tables[0].Rows[i]["r"]);
    //    //        m3 = Convert.ToInt32(rsfilter.Tables[0].Rows[i]["rsq"]);
    //    //        for (int j = 0; j < m1; j++)
    //    //        {
    //    //            m3 += 1;
    //    //            objrp.Rph_No = 1;
    //    //            objrp.Gr_No = m3;
    //    //            objrpc.InsRptrp(objrp, 5);
    //    //        }
    //    //    }


    //    //}

    //    //if (rdbFormat1.Checked)
    //    //{
    //    //    ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_New.rpt");
    //    //}
    //    //else if (rdbFormat2.Checked)
    //    //{
    //    //    ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_NewFormt2.rpt");
    //    //}
    //    //else if (rdbFormat3.Checked)
    //    //{
    //    ShowReport("BULK PAYMENT REPORT", "BulkPaymentrRportWithCheque.rpt");
    //    //}

    //}
    private void FillPaymentMode()
    {
        objCommon.FillDropDownList(ddlPaymentMode, "ACC_PAYMODE", "PAYMODE_CODE", "PAYMODE", "", "");
    }

    private void ShowReport(string reportTitle, string rptFileName, int VOUCHER_SQN)
    {
        try
        {
            string status = string.Empty; 
            if (rblGroup.SelectedValue == "1")
            {
                status = "Y";
            }
            else if (rblGroup.SelectedValue == "2")
            {
                status = "N";
            }

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
           
            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@P_COMP_CODE=" + Session["comp_code"] + "," + "@P_VOUCHER_SQN=" + VOUCHER_SQN + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_VOUCHER_SQN=" + VOUCHER_SQN + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COMP_CODE=" + Session["comp_code"] + "," + "@_IS_APPROVED=" + status;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void dpinfo_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
    public void BindListView()
    {
        if (txtFrmDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Required... ", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "To Date Required... ", this);
            txtUptoDate.Focus();
            return;
        }

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFrmDate.Text)));
            Fdate = Fdate.Substring(0,10);

         string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtUptoDate.Text)));
            Ldate = Ldate.Substring(0,10) ;


        DataSet ds = new DataSet();


        //ADDED BY TANU 07/12/2021 

        string status = string.Empty; 
        if (rblGroup.SelectedValue == "1")
        {
            status = "Y";
        }
        else if (rblGroup.SelectedValue == "2")
        {
            status = "N";
        }

       // ds = objrpc.GetBulkPaymentReportData(ddlPaymentMode.SelectedValue, Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text), Session["comp_code"].ToString());


        ds = objrpc.GetBulkAprovedPaymentReportData(ddlPaymentMode.SelectedValue, Fdate, Ldate, Session["comp_code"].ToString(), status);
       
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvBVoucher.DataSource = ds;
            lvBVoucher.DataBind();
            dpinfo.Visible = true;
        }
        else
        {
            lvBVoucher.DataSource = null;
            lvBVoucher.DataBind();
            dpinfo.Visible = false;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnrpt = sender as Button;
            int VOUCHER_SQN = int.Parse(btnrpt.CommandArgument);
            ShowReport("BULK PAYMENT REPORT", "BulkPaymentrRportWithCheque.rpt", VOUCHER_SQN);
        }
        catch (Exception ex)
        {

        }
    }
}