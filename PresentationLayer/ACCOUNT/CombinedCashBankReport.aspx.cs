//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : 
// CREATION DATE : 06-APR-2012                                               
// CREATED BY    : KAPIL BUDHLANI                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and print the combined report of cashbook and bankbook
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

public partial class Acc_combinedCashBank : System.Web.UI.Page
{
    //UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon =new Common ();
    CombinedCashBankBook objCcbb;
    CombinedCashBankBookController objCcbc;
    public int q = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCcbc = new CombinedCashBankBookController();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        //Session["WithoutCashBank"] = "N";
       
        if (!Page.IsPostBack)
        {
            CheckPageAuthorization();
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

                    objCommon.DisplayUserMessage(UPDLedger,"Select company/cash book.", this);

                    //objCommon.DisplayUserMessage(UPDLedger,"Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    txtFrmDate.Text = DateTime.Now.ToShortDateString();
                    //txtUptoDate.Text = DateTime.Now.ToShortDateString();
                    
                    //txtUptoDate.Text = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                    //Filling Bank
                    lstBank.Items.Clear();
                    DataSet dsBank = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "payment_type_no=2", "PARTY_NO");
                    lstBank.DataTextField = "PARTY_NAME";
                    lstBank.DataValueField = "PARTY_NO";
                    lstBank.DataSource = dsBank.Tables[0];
                    lstBank.DataBind();

                    //Filling Cash
                    lstCash.Items.Clear();
                    DataSet dsCash = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "payment_type_no=1", "PARTY_NO");
                    lstCash.DataTextField = "PARTY_NAME";
                    lstCash.DataValueField = "PARTY_NO";
                    lstCash.DataSource = dsCash.Tables[0];
                    lstCash.DataBind();
                }
            }
            SetFinancialYear();
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
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            //txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
            DateTime frmDate = Convert.ToDateTime(txtFrmDate.Text);
            string lastDate = DateTime.DaysInMonth(frmDate.Year, frmDate.Month).ToString();
            txtUptoDate.Text = Convert.ToDateTime(lastDate + "/" + frmDate.Month + "/" + frmDate.Year).ToString("dd/MM/yyyy");
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
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }
        
        //if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
        //    txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
        //    txtUptoDate.Focus();
        //    return;
        //}

        //if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
        //    txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
        //    txtFrmDate.Focus();
        //    return;
        //}

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
        
        ////Prepare Data
        //double OBALANCE = 0.0;
        //q = 1;
        //string bcode="0";
        //int bnkCount = 0;

        //for (int i = 0; i < lstBank.Items.Count; i++)
        //{
        //    if (lstBank.Items[i].Selected)
        //    {
        //        bnkCount += 1;
        //    }
        //}

        ////if (bnkCount > 1)
        ////{
        //    for (int i = 0; i < lstBank.Items.Count; i++)
        //    {
        //        if (lstBank.Items[i].Selected)
        //        {
        //            bcode += "," + lstBank.Items[i].Value;
        //        }
        //    }

        //    for (int i = 0; i < lstCash.Items.Count; i++)
        //    {
        //        if (lstCash.Items[i].Selected)
        //        {
        //            q = q + 1;
        //        }
        //    }

        //    objCcbc.LoadDataInDummyTbl(txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), bcode);
        //    ReceiptSideCashBook();
        //    ReceiptSideBankBook();
        //    cashInHand();
        //    BankOpeningClosing(OBALANCE);
        ////}
        ////else if (bnkCount==1)
        ////{
        ////    objCcbc.LoadDataInDummyTbl(txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), lstBank.Items[lstBank.SelectedIndex].Value );
        ////    ReceiptSideCashBookSingle();
        ////    ReceiptSideBankBookSingle();
        ////    cashInHandSingle();
        ////    BankOpeningClosingSingle(OBALANCE);
        ////}

        //SET CASHNO
        int count = 0;
        string cashNo = string.Empty;
        for (int i = 0; i < lstCash.Items.Count; i++)
        {
            
            if (count == 0)
            {
                if (lstCash.Items[i].Selected)
                {
                    cashNo = lstCash.Items[i].Value;
                    count = count + 1;
                }
            }
            else
            {
                if (lstCash.Items[i].Selected)
                {
                    cashNo = cashNo+"."+lstCash.Items[i].Value;
                }
            }
        }

        if (count == 0)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select Cash Book", this.Page);
            return;
        }
        

        //SET BANKNO
        int countBank = 0;
        string bankNo = string.Empty;
        for (int i = 0; i < lstBank.Items.Count; i++)
        {

            if (countBank == 0)
            {
                if (lstBank.Items[i].Selected)
                {
                    bankNo = lstBank.Items[i].Value;
                    countBank = count + 1;
                }
            }
            else
            {
                if (lstBank.Items[i].Selected)
                {
                    bankNo =bankNo+"."+lstBank.Items[i].Value;
                    countBank++;
                }
            }
        }
        if (countBank == 0)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select Bank Book", this.Page);
            return;
        }

        ViewState["CashNo"] = cashNo;
        ViewState["BankNo"] = bankNo;
            objCcbc.PrepareData(txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(),cashNo,bankNo);
        //ShowReport("Combined Cash & Bank Book Report", "CombinedTesting.rpt");
        ShowReport("Combined Cash & Bank Book Report", "Combined_.rpt");
    }

    public void ReceiptSideCashBook()
    {
        DataSet rscbook1 = objCommon.FillDropDown("TEMP_ACC_BCWDUMMY1", "*", "", "status='Dr' and book='C'", "DATE1,TR_NO");

        for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        {
            DataSet dsPname = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "PARTY_NO=" + Convert.ToInt32(rscbook1.Tables[0].Rows[i]["PARTY"]), "");
            
            string PNAME = string.Empty;
            if (dsPname.Tables[0].Rows.Count > 0)
            PNAME =Convert.ToString(dsPname.Tables[0].Rows[0]["PARTY_NAME"]);

            //DataSet rscbook2 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "TRANSACTION_DATE", "TRANSACTION_NO", "(RECIEPT = 0 Or RECIEPT IS NULL )And (RECIEPTB = 0 Or RECIEPTB IS NULL) AND TRANSACTION_DATE="+rscbook1.Tables[0].Rows[i]["TRANSACTION_DATE"] , "TRANSACTION_DATE,TRANSACTION_NO");
            DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_CWDUMMY3", "DATE1", "TR_NO", "(RECIEPT = 0 Or RECIEPT IS NULL )And (RECIEPTB = 0 Or RECIEPTB IS NULL) AND DATE1=convert(datetime,'" +Convert.ToDateTime( rscbook1.Tables[0].Rows[i]["DATE1"]).ToShortDateString()+"',103)", "TR_NO");

            if (rscbook2.Tables[0].Rows.Count <1)
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime( rscbook1.Tables[0].Rows[i]["date1"]),0,Convert.ToString( rscbook1.Tables[0].Rows[i]["particular"]),Convert.ToDouble( rscbook1.Tables[0].Rows[i]["AMOUNT"]),PNAME,Convert.ToInt32 (  rscbook1.Tables[0].Rows[i]["vno"]),Convert.ToInt32 ( rscbook1.Tables[0].Rows[i]["subtr_no"]),Convert.ToString ( rscbook1.Tables[0].Rows[i]["tr_type"]),Convert.ToInt32 ( rscbook1.Tables[0].Rows[i]["tentry"]), 1,1);
            }
            else
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), Convert.ToInt32(rscbook1.Tables[0].Rows[0]["tr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 2,1);
            }
        }
    }

    public void ReceiptSideBankBook()
    {

        //DataSet rscbook1 = objCommon.FillDropDown("TEMP_ACC_BCWDUMMY1", "*", "", "bcwdummy1.status='D' and bcwdummy1.book='B'", "TRANSACTION_DATE,TRANSACTION_NO");
        DataSet rscbook1 = objCommon.FillDropDown("TEMP_ACC_BCWDUMMY1", "*", "", "status='Dr' and book='B'", "DATE1,TR_NO");

        for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        {
            DataSet rs = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "PARTY_NO=" + Convert.ToInt32(rscbook1.Tables[0].Rows[i]["party"]), "");

            string PNAME = string.Empty;
            if (rs.Tables[0].Rows.Count > 0)
                PNAME = Convert.ToString(rs.Tables[0].Rows[0]["PARTY_NAME"]);

            DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_cwdummy3", "DATE1", "TR_NO", "(RECIEPT = 0 Or RECIEPT IS NULL )And (RECIEPTB = 0 Or RECIEPTB IS NULL) AND DATE1=" + Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["DATE1"]).ToShortDateString(), "TR_NO");

            if (rscbook2.Tables[0].Rows.Count < 1)
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), 0, Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 1,2);
            }
            else
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 2,2);
            }
        }
    }

    public double  cashInHand()
    {
       double  OPAMOUNT = 0.0;
       double  OBALANCE=0.0;
        for(int i=0; i<lstCash.Items.Count; i++  )
        {
            if (lstCash.Items[i].Selected)
            {
                //DataSet rscom = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "SUM((case when transaction_type='OB'then(case when PARTY_no=25 then (case when [TRAN]='C'then -AMOUNT else (case when [tran]='D'then amount else 0 end)end )else 0 end )else 0 end ) ) as OBALANCE", "", "", "");
                DataSet rscom = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[i].Value), 0, 0);

                if (rscom.Tables[0].Rows.Count < 1)
                {
                    OPAMOUNT = rscom.Tables[0].Rows[0]["OBALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0]["OBALANCE"]);
                }
                //DataSet rscom1 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "SUM(case when transaction_type='OB'then 0 else (case when PARTY_NO=25 then (case when [TRAN]='C'then AMOUNT else 0 end)else 0 end)end) + SUM ((case when transaction_type='OB'then 0 else (case when OPARTY='25' then (case when OTRAN='C'then AMOUNT else 0 end)else 0 end)end)) AS CREDIT,SUM((case when transaction_type='OB'then 0  else (case when PARTY_NO=25 then (case when [TRAN]='D'then AMOUNT else 0 end)else 0 end)end)) + SUM((case when transaction_type='OB'then 0 else (case when OPARTY='25' then (case when OTRAN='D'then AMOUNT else 0  end)else 0 end)end)) AS DEBIT, SUM((case when transaction_type='OB'then (case when PARTY_NO=25 then (case when [TRAN]='C'then-AMOUNT else(case when [tran]='D'then amount else 0 end)end)else 0 end)else 0 end))as OBALANCE", "", "", "");

                DataSet rscom1 = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[i].Value), Convert.ToInt32(lstCash.Items[i].Value), 1);
                OBALANCE = OPAMOUNT + (rscom1.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0][0])) - (rscom.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0][0]));
            }
        }
        return OBALANCE;
    }

    public void BankOpeningClosing(double OBALANCE)
    {
        int q = 1;              //for bank closing & opening balance
        double OPBALANCEB = 0.0;
        double  amt1 = 0; 
        double  bamt1 = 0;
        double  amt2 = 0;
        double  bamt2 = 0;
        double cbalance = 0;
        string qstr=string.Empty;
        double OPBANKBAL = 0;
        double[,] mOPBBAL = new double[2, 15];
        double[,] mCLBBAL = new double[2, 15];
        double  CLBALANCEB=0;
                
        for(int i=0;i<lstBank.Items.Count;i++)
        {
            if (lstBank.Items[i].Selected)
            {
                double OPAMOUNT = 0;

                mOPBBAL[0,q] = 0;
                mCLBBAL[0, q] = 0;
                mOPBBAL[1, q] = Convert.ToInt32(lstBank.Items[i].Value);
                mCLBBAL[1, q] = Convert.ToDouble(lstBank.Items[i].Value);

               DataSet  rscom =  objCcbc.Get_OPBAL(Convert.ToInt32(lstBank.Items[i].Value),0,0);
                
               if (rscom.Tables[0].Rows.Count >0)
               {
                   OPAMOUNT = rscom.Tables[0].Rows[0]["OBALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0]["OBALANCE"]);
               }
               
               DataSet rscom1 = objCcbc.Get_OPBAL(Convert.ToInt32(lstBank.Items[i].Value), Convert.ToInt32(lstBank.Items[i].Value), 1);

                //OPBANKBAL = OPAMOUNT + (rscom1.Tables[0].Rows[0]["debit"] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0]["debit"])) - (rscom1.Tables[0].Rows[0]["credit"] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0]["credit"]));
               //temp
               //OPBANKBAL =OPBANKBAL+ OPAMOUNT + (rscom1.Tables[0].Rows[0]["obalance"] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0]["obalance"])) - (rscom1.Tables[0].Rows[0]["obalance"] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0]["obalance"]));
               
                //OPBANKBAL = OPBANKBAL + OPAMOUNT + (rscom1.Tables[0].Rows[0]["debit"] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0]["debit"])) - (rscom1.Tables[0].Rows[0]["credit"] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0]["credit"]));

               //OPBANKBAL = OPAMOUNT + Convert.ToDouble(rscom1.Tables[0].Rows[0]["debit"]) - Convert.ToDouble(rscom1.Tables[0].Rows[0]["credit"]);
                OPBANKBAL = OPBANKBAL + OPAMOUNT + 0 - 0;

                mOPBBAL[0,q] = OPBANKBAL;
                mCLBBAL[0, q] = OPBANKBAL;
                OPAMOUNT = 0;

                for (int j = 0; j < (mOPBBAL.Length/2); j++)
                {
                    OPAMOUNT = OPAMOUNT + mOPBBAL[0, j];
                }
                
                if (q >= 2 && q <= 15)
                {
                    CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                    CombinedCashBankBook objCombi = new CombinedCashBankBook();

                    if (q == 1)
                        objCombi.OpBalNo = "";
                    else
                        objCombi.OpBalNo = Convert.ToString(q);
                      
                    objCombi.OpBalb = OPBANKBAL;
                    objCombi.ClBalb = OPBANKBAL;
                    objCombi.Trb = OPAMOUNT;
                    objCombi.BankNm = lstBank.Items[i].Text.Trim()  ;
                    objCombi.Tr_Date = Convert.ToDateTime(txtFrmDate.Text);
                    objAdd.InsUpdateBankOpClBal(objCombi, "", 2);
                    qstr = Convert.ToString(q);
                    q = q + 1;
                }
                else
                {
                    //insert
                    CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                    CombinedCashBankBook objCombi = new CombinedCashBankBook();

                    objCombi.Tr_Date = Convert.ToDateTime(txtFrmDate.Text);

                    if (q == 1)
                        objCombi.OpBalNo = "";
                    else
                        objCombi.OpBalNo = Convert.ToString(q);

                    //objCombi.OpBalNo =Convert.ToString( q);
                    objCombi.OpBal = OBALANCE;
                    objCombi.ClBal = OBALANCE;
                    objCombi.Tr = OBALANCE;
                    objCombi.OpBalb = OPBANKBAL;
                    objCombi.ClBalb = OPBANKBAL;
                    objCombi.Trb = OPAMOUNT;
                    objCombi.BankNm = lstBank.Items[i].Text.Trim();
                    objAdd.InsUpdateBankOpClBal(objCombi, "", 1);

                    //update
                    CombinedCashBankBookController objAdd1 = new CombinedCashBankBookController();
                    CombinedCashBankBook objCombi1 = new CombinedCashBankBook();

                    objCombi1.Tr_Date = Convert.ToDateTime(txtFrmDate.Text);
                    if(q==1)
                        objCombi1.OpBalNo = "";
                    else
                    objCombi1.OpBalNo =Convert.ToString( q);

                    objCombi1.OpBalb = OPBANKBAL;
                    objCombi1.ClBalb = OPBANKBAL;
                    objCombi1.Trb = OPAMOUNT;
                    objCombi1.Tr_Date = Convert.ToDateTime(txtFrmDate.Text);
                    objCombi1.BankNm = lstBank.Items[i].Text.Trim();
                    objAdd1.InsUpdateBankOpClBal(objCombi1, "", 2);
                    qstr = "";
                    q = q + 1;
                }
                OPBALANCEB = OPBALANCEB + OPBANKBAL;
            }
        }

         double  mPCD1 =999999;

         DataSet rscbook1 = objCcbc.Get_bcwdummy1(1);    //for payment_type_no=1--cash

        for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        { 
            amt1 = 0; bamt1 = 0;
            amt2 = 0; bamt2 = 0;
            amt1 =Convert.ToDouble( rscbook1.Tables[0].Rows[i]["debit"]);
            amt2 = Convert.ToDouble(rscbook1.Tables[0].Rows[i]["credit"]);
            cbalance = OBALANCE + amt1 - amt2;
            
            //insert
            CombinedCashBankBook objCcbb = new CombinedCashBankBook();
            objCcbb.OpBal=OBALANCE ;
            objCcbb.ClBal=cbalance;
            objCcbb.Tr =OBALANCE+amt1 ;
            objCcbb.Tr_Date =Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["Date1"]);
            //objCcbb.BankNm = lstBank.Items[i].Text.Trim();
            objCcbc.InsUpdateBankOpClBal(objCcbb, "",11);//UPDATE CASH OPCL BAL

            //update
            objCcbb = new CombinedCashBankBook();
            objCcbb.OpBal = cbalance;
            //objCcbb.ClBal = cbalance;
            objCcbb.Tr = OBALANCE + amt1;
            objCcbb.Tr_Date = Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["Date1"]);
            //objCcbb.BankNm = lstBank.Items[i].Text.Trim(); //1605
            objCcbc.InsUpdateBankOpClBal(objCcbb, "", 12);

            OBALANCE = cbalance;
        }
        qstr = "";
        q = 1;
        OPBANKBAL = 0;

        DataSet rscbook11 = objCcbc.Get_bcwdummy1(2);//for payment_type_no=2 --bank

        for (int j = 0; j < rscbook11.Tables[0].Rows.Count; j++)
        { 
            amt1 = 0; bamt1 = 0;
            amt2 = 0; bamt2 = 0;
            OPBANKBAL = 0;
            
            for (int i = 0; i < mOPBBAL.Length / 2; i++)
            {
                OPBANKBAL = OPBANKBAL + mOPBBAL[0, i];
            }

            for (int p = 0; p < ((mOPBBAL.Length) / 2); p++)
            {
                //if (mOPBBAL[1, p] == Convert.ToInt32(rscbook11.Tables[0].Rows[0]["Pcd1"]))
                if (mOPBBAL[1, p] == Convert.ToInt32(rscbook11.Tables[0].Rows[j]["Pcd1"]))
                {
                    q = p;
                    break;
                }
            }
            OPBALANCEB = mOPBBAL[0,q];
           // qstr = q > 1? Convert.ToString( q) : "0";
            //q += 1;
            qstr = q > 1 ? Convert.ToString(q) : "";
            bamt1 =Convert.ToDouble( rscbook11.Tables[0].Rows[j]["debit"]);
            bamt2 =Convert.ToDouble( rscbook11.Tables[0].Rows[j]["credit"]);
            //bamt2 =bamt2 + Convert.ToDouble(rscbook11.Tables[0].Rows[j]["credit"]);
            CLBALANCEB = OPBALANCEB + bamt1 - bamt2;

            //update
             objCcbb = new CombinedCashBankBook();
             objCcbb.OpBalNo = qstr;
             objCcbb.OpBalb =OPBALANCEB  ;
             objCcbb.ClBalb  =CLBALANCEB  ;
             objCcbb.Trb = OPBANKBAL;
             objCcbb.BankNm = Convert.ToString(rscbook11.Tables[0].Rows[j]["Party_name"]);
             objCcbb.Tr_Date =Convert.ToDateTime(rscbook11.Tables[0].Rows[j]["Date1"]);
             objCcbc.InsUpdateBankOpClBal(objCcbb, "", 22);

            mOPBBAL[0, q] = CLBALANCEB;
            OPBANKBAL = 0;
            
            for (int k = 0; k < mOPBBAL.Length / 2; k++)
            {
                OPBANKBAL = OPBANKBAL + mOPBBAL[0, k];
            }

            objCcbb = new CombinedCashBankBook();
            objCcbb.OpBalNo = qstr;
            objCcbb.OpBalb = CLBALANCEB ;
            //objCcbb.Trb = OPBANKBAL;//check + bamt2 temp
            objCcbb.Trb = OPBANKBAL + amt1;
            objCcbb.Tr_Date = Convert.ToDateTime(rscbook11.Tables[0].Rows[j]["Date1"]);
            objCcbb.BankNm = Convert.ToString(rscbook11.Tables[0].Rows[j]["Party_name"]);
            objCcbc.InsUpdateBankOpClBal(objCcbb, "", 23);

        }
        //LAST QUERY
        //objCcbb = new CombinedCashBankBook();
        //objCcbb.Tr_Date = DateTime.Now; 
        //objCcbc.InsUpdateBankOpClBal(objCcbb, "", 14);
    }


    public void ReceiptSideCashBookSingle()
    {
        DataSet rscbook1 = objCommon.FillDropDown("TEMP_ACC_BCWDUMMY1", "*", "", "status='Dr' and book='C'", "DATE1,TR_NO");

        for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        {
            DataSet dsPname = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "PARTY_NO=" + Convert.ToInt32(rscbook1.Tables[0].Rows[i]["PARTY"]), "");

            string PNAME = string.Empty;
            if (dsPname.Tables[0].Rows.Count > 0)
                PNAME = Convert.ToString(dsPname.Tables[0].Rows[0]["PARTY_NAME"]);

            //DataSet rscbook2 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "TRANSACTION_DATE", "TRANSACTION_NO", "(RECIEPT = 0 Or RECIEPT IS NULL )And (RECIEPTB = 0 Or RECIEPTB IS NULL) AND TRANSACTION_DATE="+rscbook1.Tables[0].Rows[i]["TRANSACTION_DATE"] , "TRANSACTION_DATE,TRANSACTION_NO");
            DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_CWDUMMY3", "DATE1", "TR_NO", "(RECIEPT = 0 Or RECIEPT IS NULL )And (RECIEPTB = 0 Or RECIEPTB IS NULL) AND DATE1=convert(datetime,'" + Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["DATE1"]).ToShortDateString() + "',103)", "TR_NO");

            if (rscbook2.Tables[0].Rows.Count < 1)
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), 0, Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 1, 1);
            }
            else
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), Convert.ToInt32(rscbook1.Tables[0].Rows[0]["tr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 2, 1);
            }
        }
    }

    public void ReceiptSideBankBookSingle()
    {

        //DataSet rscbook1 = objCommon.FillDropDown("TEMP_ACC_BCWDUMMY1", "*", "", "bcwdummy1.status='D' and bcwdummy1.book='B'", "TRANSACTION_DATE,TRANSACTION_NO");
        DataSet rscbook1 = objCommon.FillDropDown("TEMP_ACC_BCWDUMMY1", "*", "", "status='Dr' and book='B'", "DATE1,TR_NO");

        for (int i = 0; i < rscbook1.Tables[0].Rows.Count; i++)
        {
            DataSet rs = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NAME", "PARTY_NO", "PARTY_NO=" + Convert.ToInt32(rscbook1.Tables[0].Rows[i]["party"]), "");

            string PNAME = string.Empty;
            if (rs.Tables[0].Rows.Count > 0)
                PNAME = Convert.ToString(rs.Tables[0].Rows[0]["PARTY_NAME"]);

            DataSet rscbook2 = objCommon.FillDropDown("TEMP_ACC_cwdummy3", "DATE1", "TR_NO", "(RECIEPT = 0 Or RECIEPT IS NULL )And (RECIEPTB = 0 Or RECIEPTB IS NULL) AND DATE1=" + Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["DATE1"]).ToShortDateString(), "TR_NO");

            if (rscbook2.Tables[0].Rows.Count < 1)
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), 0, Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 1, 2);
            }
            else
            {
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                objAdd.AddReceiptCashBook(Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["date1"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["particular"]), Convert.ToDouble(rscbook1.Tables[0].Rows[i]["AMOUNT"]), PNAME, Convert.ToInt32(rscbook1.Tables[0].Rows[i]["vno"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["subtr_no"]), Convert.ToString(rscbook1.Tables[0].Rows[i]["tr_type"]), Convert.ToInt32(rscbook1.Tables[0].Rows[i]["tentry"]), 2, 2);
            }
        }
    }

    public double cashInHandSingle()
    {
        double OPAMOUNT = 0.0;
        double OBALANCE = 0.0;
        for (int i = 0; i < lstCash.Items.Count; i++)
        {
            if (lstCash.Items[i].Selected)
            {
                //DataSet rscom = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "SUM((case when transaction_type='OB'then(case when PARTY_no=25 then (case when [TRAN]='C'then -AMOUNT else (case when [tran]='D'then amount else 0 end)end )else 0 end )else 0 end ) ) as OBALANCE", "", "", "");
                DataSet rscom = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[i].Value), 0, 0);

                if (rscom.Tables[0].Rows.Count < 1)
                {
                    OPAMOUNT = rscom.Tables[0].Rows[0]["OBALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0]["OBALANCE"]);
                }
                //DataSet rscom1 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "SUM(case when transaction_type='OB'then 0 else (case when PARTY_NO=25 then (case when [TRAN]='C'then AMOUNT else 0 end)else 0 end)end) + SUM ((case when transaction_type='OB'then 0 else (case when OPARTY='25' then (case when OTRAN='C'then AMOUNT else 0 end)else 0 end)end)) AS CREDIT,SUM((case when transaction_type='OB'then 0  else (case when PARTY_NO=25 then (case when [TRAN]='D'then AMOUNT else 0 end)else 0 end)end)) + SUM((case when transaction_type='OB'then 0 else (case when OPARTY='25' then (case when OTRAN='D'then AMOUNT else 0  end)else 0 end)end)) AS DEBIT, SUM((case when transaction_type='OB'then (case when PARTY_NO=25 then (case when [TRAN]='C'then-AMOUNT else(case when [tran]='D'then amount else 0 end)end)else 0 end)else 0 end))as OBALANCE", "", "", "");

                DataSet rscom1 = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[i].Value), Convert.ToInt32(lstCash.Items[i].Value), 1);
                OBALANCE = OPAMOUNT + (rscom1.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(rscom1.Tables[0].Rows[0][0])) - (rscom.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0][0]));
            }
        }
        return OBALANCE;
    }

    public void BankOpeningClosingSingle(double OBALANCE)
    {
        int q = 1;              //for bank closing & opening balance
        double OPBALANCEB = 0.0;
        double amt1 = 0;
        double bamt1 = 0;
        double amt2 = 0;
        double bamt2 = 0;
        double cbalance = 0;
        string qstr = string.Empty;
        double OPBANKBAL = 0;
        double[,] mOPBBAL = new double[2, 15];
        double[,] mCLBBAL = new double[2, 15];
        double CLBALANCEB = 0;

        for (int i = 0; i < lstCash.Items.Count; i++)
        {
            if (lstCash.Items[i].Selected)
            {
                double OPAMOUNT = 0;

                DataSet rscom = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[i].Value), 0, 0);

                if (rscom.Tables[0].Rows.Count > 0)
                {
                    OPAMOUNT = rscom.Tables[0].Rows[0]["OBALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0]["OBALANCE"]);
                }

                DataSet rscom1 = objCcbc.Get_OPBAL(Convert.ToInt32(lstCash.Items[i].Value), Convert.ToInt32(lstCash.Items[i].Value), 1);

                OBALANCE = OBALANCE + OPAMOUNT + 0 - 0;
                                
            }
        }
        
        for (int i = 0; i < lstBank.Items.Count; i++)
        {
            if (lstBank.Items[i].Selected)
            {
                double OPAMOUNT = 0;

                DataSet rscom = objCcbc.Get_OPBAL(Convert.ToInt32(lstBank.Items[i].Value), 0, 0);

                if (rscom.Tables[0].Rows.Count > 0)
                {
                    OPAMOUNT = rscom.Tables[0].Rows[0]["OBALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(rscom.Tables[0].Rows[0]["OBALANCE"]);
                }

                DataSet rscom1 = objCcbc.Get_OPBAL(Convert.ToInt32(lstBank.Items[i].Value), Convert.ToInt32(lstBank.Items[i].Value), 1);
                                
                OPBANKBAL = OPBANKBAL + OPAMOUNT + 0 - 0;
                
                
                CombinedCashBankBookController objAdd = new CombinedCashBankBookController();
                CombinedCashBankBook objCombi = new CombinedCashBankBook();

                    objCombi.Tr_Date = Convert.ToDateTime(txtFrmDate.Text);
                    objCombi.OpBalNo =Convert.ToString( q);
                    objCombi.OpBal = OBALANCE;
                    objCombi.ClBal = OBALANCE;
                    objCombi.Tr = OBALANCE;
                    objCombi.OpBalb = OPBANKBAL;
                    objCombi.ClBalb = OPBANKBAL;
                    objCombi.Trb = OPAMOUNT;
                    objCombi.BankNm = lstBank.Items[i].Text.Trim();
                    objAdd.InsUpdateBankOpClBal(objCombi, "", 1);
                                
            }
        }

      DataSet rscbook1  =objCommon.FillDropDown("temp_acc_bcwdummy1","distinct date1","null as a","","");
        for (int i=0;i<rscbook1.Tables[0].Rows.Count ;i++ )
        {
        amt1 = 0; bamt1 = 0;
        amt2 = 0; bamt2 = 0;
        
        DataSet rscbook2 = objCommon.FillDropDown("temp_acc_bcwdummy1", "*", "date1", "date1=convert(datetime,'" + Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["DATE1"]).ToShortDateString()+"',103)", "");

            for(int j=0;j<rscbook2.Tables[0].Rows.Count ;j++)
            {
                if(Convert.ToString( rscbook2.Tables[0].Rows[j]["BOOK"]).ToUpper()=="C")
                {
                    if(Convert.ToString( rscbook2.Tables[0].Rows[j]["Status"]).ToLower()=="dr")
                        amt1 = amt1 + Convert.ToDouble( rscbook2.Tables[0].Rows[j]["AMOUNT"]);
                    else if (Convert.ToString( rscbook2.Tables[0].Rows[j]["Status"]).ToLower()=="cr")
                        amt2 = amt2 + Convert.ToDouble( rscbook2.Tables[0].Rows[j]["AMOUNT"]);

                }
                else if (Convert.ToString( rscbook2.Tables[0].Rows[j]["BOOK"]).ToUpper()=="B")
                {
                    if(Convert.ToString( rscbook2.Tables[0].Rows[j]["Status"]).ToLower()=="dr")
                            amt1 = amt1 + Convert.ToDouble( rscbook2.Tables[0].Rows[j]["AMOUNT"]);
                        else if (Convert.ToString( rscbook2.Tables[0].Rows[j]["Status"]).ToLower()=="cr")
                            amt2 = amt2 + Convert.ToDouble( rscbook2.Tables[0].Rows[j]["AMOUNT"]);
                }                           
            }

            
        cbalance = OBALANCE + amt1 - amt2;
        CLBALANCEB = OPBALANCEB + bamt1 - bamt2;

        objCcbb = new CombinedCashBankBook();
        objCcbb.OpBal = OBALANCE;
        objCcbb.ClBal = cbalance;
        objCcbb.Tr = OBALANCE + amt1;
        objCcbb.OpBalb = OPBALANCEB;
        objCcbb.ClBalb = CLBALANCEB;
        objCcbb.Trb = OPBALANCEB + bamt1;
        objCcbb.Tr_Date = Convert.ToDateTime(rscbook1.Tables[0].Rows[i]["Date1"]);
        //objCcbb.BankNm = Convert.ToString(rscbook1.Tables[0].Rows[i]["Party_name"]);
        objCcbc.InsUpdateBankOpClBal(objCcbb, "", 13);//single bank
               
        OBALANCE = cbalance;
        OPBALANCEB = CLBALANCEB;
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
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " To " + txtUptoDate.Text.ToString().Trim() + ",@COL_CODE=" + Session["comp_code"].ToString() + ",@P_FROMDT=" + txtFrmDate.Text.Trim().ToString() + ",@P_TODT=" + txtUptoDate.Text.Trim().ToString() + ",@P_cashNo=" + ViewState["CashNo"].ToString() + ",@P_bankNo=" + ViewState["BankNo"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
            
        }
        catch (Exception ex)
        {

        }
    }
    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        DateTime frmDate = Convert.ToDateTime(txtFrmDate.Text);
        string lastDate = DateTime.DaysInMonth(frmDate.Year, frmDate.Month).ToString();
        //string uptoDate = lastDate + "/" + frmDate.Month + "/" + frmDate.Year;
        txtUptoDate.Text = Convert.ToDateTime(lastDate + "/" + frmDate.Month + "/" + frmDate.Year).ToString("dd/MM/yyyy");
    }
}