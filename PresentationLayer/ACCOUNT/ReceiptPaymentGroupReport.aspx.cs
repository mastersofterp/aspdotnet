//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : FINANCE
// CREATION DATE : 21-MAY-2012                                               
// CREATED BY    : KAPIL BUDHLANI                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to view and print the Receipt Payment Group Report
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

public partial class ReceiptPaymentGroupReport : System.Web.UI.Page
{
    Common objCommon =new Common ();
    //CombinedCashBankBook objCcbb;
    ReceiptPayment objrp = new ReceiptPayment();
    CombinedCashBankBookController objCcbc;
    ReceiptPaymentController objrpc;
    public int q = 0;

    double  CURBALANCE=0.0;  //Variable is used to store current balance
    string  GRNO ;           // used to store group no
    int  SEQNO1 ;            //used to store sequence no.
    string  sOp;             //used to store opening balance

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
            objCcbc = new CombinedCashBankBookController();
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

                    objCommon.DisplayUserMessage(UPDLedger,"Select company/cash book.", this);


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
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
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
                        
        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }

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

        if (rdbFormat1.Checked)
        {
            ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_New.rpt");
        }
        else if (rdbFormat2.Checked)
        {
            ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_NewFormt2.rpt");
        }
        else if (rdbFormat3.Checked)
        {
            ShowReport("RECEIPT AND PAYMENT GROUP STATEMENT", "RecPayGrp_NewFormt3.rpt");
        }

    }

    

    public void GETRECPAYMENTDATA()
    {
        //Prepare Data


        int levelNo = 0;
        int lno = 0;
        string oType = string.Empty, clType = string.Empty;
        double totcr = 0.0, totdr = 0.0, OPBAL = 0.0, BALANCE = 0.0;
        levelNo = 100;

        objrpc.GETRECPAYMENTDATA(txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), levelNo);
        DataSet ds = objrpc.GetPartyWthCount(levelNo, 1);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            objrpc.DeletePartyFromRptrp(Convert.ToInt32(ds.Tables[0].Rows[i]["pcd"]), 1);
        }

        //'******CODE TO CALCULATE FOR OPENING BALANCE HEAD **********************************
        DataSet RSRECP = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY P INNER JOIN ACC_" + Session["comp_code"].ToString() + "_RECIEPT_PRINT_GROUP G ON P.RP_NO=G.RP_NO", "PARTY_NO,PARTY_NAME,balance,CREDIT,DEBIT", "P.RP_NO,LNO,OPBALANCE,[STATUS],RPH_NO", "(PAYMENT_TYPE_NO=1 OR PAYMENT_TYPE_NO=2) AND PARTY_NO>0", "");

        for (int j = 0; j < RSRECP.Tables[0].Rows.Count; j++)
        {
            totcr = 0; totdr = 0; OPBAL = 0; BALANCE = 0;
            oType = ""; clType = "";
            DataSet RSTR = objrpc.Get_RP_OPBAL(Convert.ToInt32(RSRECP.Tables[0].Rows[j][0]), txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), 31);

            if (RSTR.Tables[0].Rows.Count > 0)
            {
                OPBAL = string.IsNullOrEmpty(RSTR.Tables[0].Rows[0][0].ToString()) ? 0 : Convert.ToDouble(RSTR.Tables[0].Rows[0][0]);
            }
            RSTR = objrpc.Get_RP_OPBAL(Convert.ToInt32(RSRECP.Tables[0].Rows[j][0]), txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), 32);

            if (RSTR.Tables[0].Rows.Count > 0)
            {
                double val = string.IsNullOrEmpty(RSTR.Tables[0].Rows[0][1].ToString()) ? 0 : Convert.ToDouble(RSTR.Tables[0].Rows[0][1]);
                double val1 = string.IsNullOrEmpty(RSTR.Tables[0].Rows[0][0].ToString()) ? 0 : Convert.ToDouble(RSTR.Tables[0].Rows[0][0]);
                OPBAL = OPBAL + val - val1;
                if (OPBAL >= 0)
                    oType = "Dr";
                else
                    oType = "cr";
            }
            RSTR = objrpc.Get_RP_OPBAL(Convert.ToInt32(RSRECP.Tables[0].Rows[j][0]), txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), 33);

            if (RSTR.Tables[0].Rows.Count > 0)
            {
                totcr = string.IsNullOrEmpty(RSTR.Tables[0].Rows[0][0].ToString()) ? 0 : Convert.ToDouble(RSTR.Tables[0].Rows[0][0]);
                totdr = string.IsNullOrEmpty(RSTR.Tables[0].Rows[0][1].ToString()) ? 0 : Convert.ToDouble(RSTR.Tables[0].Rows[0][1]);
            }
            BALANCE = OPBAL + totdr - totcr;
            if (BALANCE >= 0)
                clType = "Dr";
            else
                clType = "Cr";

            //insert 

            objrp.Level_No = levelNo;
            objrp.Party_No = Convert.ToInt32(RSRECP.Tables[0].Rows[j]["PARTY_NO"]);
            objrp.Party_Name = Convert.ToString(RSRECP.Tables[0].Rows[j]["PARTY_NAME"]);
            objrp.OBalanace = OPBAL;
            objrp.Debit = totdr;
            objrp.Credit = totcr;
            objrp.CLBalanace = BALANCE;
            objrp.ClType = clType;
            objrp.Rp_No = Convert.ToInt32(RSRECP.Tables[0].Rows[j]["RP_NO"]);
            objrp.LNo = Convert.ToInt32(string.IsNullOrEmpty(RSRECP.Tables[0].Rows[j]["LNO"].ToString()) ? 1 : Convert.ToInt32(RSRECP.Tables[0].Rows[j]["LNO"]) + 1);
            objrp.PrvBalanace = Convert.ToString(RSRECP.Tables[0].Rows[j]["Status"]) == "Dr" ? Convert.ToDouble(RSRECP.Tables[0].Rows[j]["OPBALANCE"]) : -(Convert.ToDouble(RSRECP.Tables[0].Rows[j]["OPBALANCE"]));
            objrp.PrvType = Convert.ToString(RSRECP.Tables[0].Rows[j]["Status"]) == "Dr" ? "Dr" : "Cr";
            objrp.Rph_No = Convert.ToInt32(RSRECP.Tables[0].Rows[j]["RPH_NO"]);
            objrpc.InsRptrp(objrp, 1);

        }
        //'******CODE TO CALCULATE FOR ALL LEDGER **********************************
        do
        {
            if (levelNo == -1) break;

            RSRECP = objrpc.GetPartyWthCount(levelNo, 2);

            if (RSRECP.Tables[0].Rows.Count < 1)
                break;
            else
            {
                levelNo = levelNo - 1;
                lno = 0;
                for (int k = 0; k < RSRECP.Tables[0].Rows.Count; k++)
                {
                    DataSet RSTR = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_RECIEPT_PRINT_GROUP", "RPPRNO,RP_NAME", "LNO", "RP_NO=" + Convert.ToInt32(RSRECP.Tables[0].Rows[k]["fgroup"]), "");
                    if (RSTR.Tables[0].Rows.Count > 0)
                    {
                        objrp.Level_No = levelNo;
                        objrp.GrName = Convert.ToString(RSTR.Tables[0].Rows[0]["rp_name"]);
                        objrp.Gr_No = Convert.ToInt32(RSRECP.Tables[0].Rows[k]["FGROUP"]);
                        objrp.OBalanace = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[k][0])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[k][0]);
                        objrp.OType = (string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[k][0])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[k][0])) >= 0 ? "Dr" : "Cr";
                        objrp.Debit = Convert.ToDouble(RSRECP.Tables[0].Rows[k][1]);
                        objrp.Credit = Convert.ToDouble(RSRECP.Tables[0].Rows[k][2]);
                        objrp.CLBalanace = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[k][3])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[k][3]);
                        objrp.ClType = (string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[k][3])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[k][3])) >= 0 ? "Dr" : "Cr";
                        objrp.FGroup = string.IsNullOrEmpty(Convert.ToString(RSTR.Tables[0].Rows[0]["RPPRNO"])) ? 0 : Convert.ToInt32(RSTR.Tables[0].Rows[0]["RPPRNO"]);
                        objrp.LNo = string.IsNullOrEmpty(Convert.ToString(RSTR.Tables[0].Rows[0]["LNO"])) ? 1 : Convert.ToInt32(RSTR.Tables[0].Rows[0]["LNO"]);
                        objrp.PrvBalanace = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[k][4])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[k][4]);
                        objrp.PrvType = (string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[k][4])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[k][4])) >= 0 ? "Dr" : "Cr";

                        objrpc.InsRptrp(objrp, 2);
                    }
                }


            }


        }
        while (1 == 1);

        //'**** CODE FOR GROUPING ON FGROUPNO ********************
        //'*** CODE TO SUM THE AMOUNT FOR SAME GRNO ******************

        //TEMP_ACC_RPTRPMAIN
        objrpc.DeletePartyFromRptrp(0, 2);
        //insert is done in store proc
        //objrpc.InsRptrp(objrp, 6);
        RSRECP = objrpc.Get_RP_OPBAL(0, txtFrmDate.Text.Trim(), txtUptoDate.Text.Trim(), 34);

        for (int i = 0; i < RSRECP.Tables[0].Rows.Count; i++)
        {
            //objrp.Level_No = levelNo;
            objrp.Gr_No = string.IsNullOrEmpty(RSRECP.Tables[0].Rows[i]["GRNO"].ToString()) ? 0 : Convert.ToInt32(RSRECP.Tables[0].Rows[i]["GRNO"]);
            objrp.GrName = Convert.ToString(RSRECP.Tables[0].Rows[i]["GRNAME"]);
            objrp.OBalanace = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i][0])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[i][0]);
            objrp.OType = (string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i][0])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[i][0])) >= 0 ? "Dr" : "Cr";
            objrp.Debit = Convert.ToDouble(RSRECP.Tables[0].Rows[i][1]);
            objrp.Credit = Convert.ToDouble(RSRECP.Tables[0].Rows[i][2]);
            objrp.CLBalanace = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i][3])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[i][3]);
            objrp.ClType = (string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i][3])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[i][3])) >= 0 ? "Dr" : "Cr";
            objrp.FGroup = Convert.ToInt32(RSRECP.Tables[0].Rows[i]["fgroup"]);
            objrp.LNo = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i]["LNO"])) ? 1 : Convert.ToInt32(RSRECP.Tables[0].Rows[i]["LNO"]);
            objrp.PrvBalanace = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i][4])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[i][4]); ;
            objrp.PrvType = (string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i][4])) ? 0 : Convert.ToDouble(RSRECP.Tables[0].Rows[i][4])) >= 0 ? "Dr" : "Cr";
            objrp.Rph_No = string.IsNullOrEmpty(Convert.ToString(RSRECP.Tables[0].Rows[i]["RPHEAD"])) ? 0 : Convert.ToInt32 (RSRECP.Tables[0].Rows[i]["RPHEAD"]);

            objrpc.InsRptrp(objrp, 3);
        }
        // '*** CODE TO SUM THE AMOUNT FOR SAME GRNO ******************
    }

    public void ALLOTSEQNOTORPGROUP()
    { 
    //'ProcAim:-
    //'This procedure is used to assign sequence no. to group and subgroup in their hierarchy
        int x=0;
        DataSet RSTR = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_RECIEPT_PRINT_GROUP", "RP_NO", "RP_NAME", "(RPPRNO=0 OR RPPRNO IS NULL)", "RP_NO");
        for(int i=0;i<RSTR.Tables[0].Rows.Count;i++ )
        {
         x = SEQNO(Convert.ToInt32( RSTR.Tables[0].Rows[i][0]), x);
        }

        
    }

    //'******MAIN LOGIC TO GIVE SEQUENCE NO TO GROUPS AND SUBGROUPS *****
    public int SEQNO(int PRNO,int x)
    {
        int m = 0;
        int cnt=0,tot=0;
        int[] grno=new int[100]; 
       
        m = x + 1;
        objrpc.UpdSeqNo(m,PRNO,0,1);
        DataSet rscom = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NO", "MGRP_NAME", "PRNO=" + PRNO, "");
        if (rscom.Tables[0].Rows.Count >0)
        {
             cnt = 0;
             for(int i=0;i<rscom.Tables[0].Rows.Count ;i++)
             {
                 grno[cnt] = Convert.ToInt32(rscom.Tables[0].Rows[i][0]);
                 if (i == rscom.Tables[0].Rows.Count - 1)
                     cnt += 1;
             }
               
            tot = cnt;
            for(int i=0;i<cnt;i++)
            {
                m = SEQNO(grno[i], m);
            }
         }
            rscom = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "PCD", "PNAME", "FGROUP=" + PRNO + " AND LEVELNO=100", "");
            for (int i = 0; i < rscom.Tables[0].Rows.Count; i++)
            {
                objrpc.UpdSeqNo(m, 0, string.IsNullOrEmpty(rscom.Tables[0].Rows[i][0].ToString()) ? 0 : Convert.ToInt32(rscom.Tables[0].Rows[i][0]), 2);
            }
            
            return m;
           
    }

    public void ADDOPBALANCE(string OPORCL, TreeView TREECTL)
    { 
        string  DRCR =string .Empty ;
        double CURAMT = 0.0;
        TreeNode xx = null;
        
        DataSet RSFILL=objCommon.FillDropDown("ACC_DC_RECIEPT_PRINT_GROUP G,ACC_DC_RECIEPT_PRINT_HEADS H","RP_NO,RPH_NAME,RPH_TYPE","H.RPH_NO,RP_NAME","G.RPH_NO=H.RPH_NO AND G.RPPRNO=0 AND RP_NO=3","RP_NO");
        
        for (int i = 0; i < RSFILL.Tables[0].Rows.Count; i++)
        {
            DRCR = ""; CURAMT = 0;
            DataSet rsfilter = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "GRNO,OBALANCE,CLBALANCE,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LNO,FGROUP,LEVELNO", "GRNO="+Convert.ToInt32( RSFILL.Tables[0].Rows[i][0]), "");

            if (rsfilter.Tables[0].Rows.Count > 0)
            {
                if (OPORCL == "OP")
                {
                    CURBALANCE = CURBALANCE + Convert.ToDouble(rsfilter.Tables[0].Rows[i][1]);
                    //DRCR = Format(rsfilter(1), "##,###.00")
                    DRCR = Convert.ToString(rsfilter.Tables[0].Rows[i][1]);
                    CURAMT = Convert.ToDouble(rsfilter.Tables[0].Rows[i][1]);
                }
                else if (OPORCL == "CL")
                {
                    CURBALANCE = CURBALANCE + Convert.ToDouble(rsfilter.Tables[0].Rows[i][2]);
                    DRCR = Convert.ToString(rsfilter.Tables[0].Rows[i][2]);
                    CURAMT = Convert.ToDouble(rsfilter.Tables[0].Rows[i][2]);
                }

                //if (tvRec.Nodes.Count == 0)
                //{
                //    xx = new TreeNode();
                //    xx.Value = "S" + Convert.ToString(rsfilter.Tables[0].Rows[0][0]);
                //    xx.Text = (OPORCL == "OP" ? Convert.ToString(rsfilter.Tables[0].Rows[0]["name"]) : "CLOSING BALANCE") + DRCR;
                //    GRNO = "S" + Convert.ToString(rsfilter.Tables[0].Rows[0][0]);
                //    //tvRec.Nodes.Add(xx);
                //}
                //else
                //{
                //    xx = new TreeNode();
                //    xx.Value = "S" + Convert.ToString(rsfilter.Tables[0].Rows[0][0]);
                //    xx.Text = (OPORCL == "OP" ? Convert.ToString(rsfilter.Tables[0].Rows[0]["name"]) : "CLOSING BALANCE") + DRCR;
                //    GRNO = "S" + Convert.ToString(rsfilter.Tables[0].Rows[0][0]);
                //    //tvRec.Nodes.Add(xx);
                //}
                SEQNO1 += 1;

                //insert           
                objrp.Party_Name = Convert.ToString(rsfilter.Tables[0].Rows[0]["name"]);
                objrp.OBalanace = CURAMT;
                objrp.Rph_No = OPORCL == "OP" ? 1 : 2;
                objrp.LNo = Convert.ToInt32(rsfilter.Tables[0].Rows[0]["lno"]);
                objrp.FGroup = Convert.ToInt32(rsfilter.Tables[0].Rows[0]["fgroup"]);
                objrp.Level_No = string.IsNullOrEmpty(rsfilter.Tables[0].Rows[0]["levelno"].ToString()) ? 0 : Convert.ToInt32(rsfilter.Tables[0].Rows[0]["levelno"]);
                objrp.Gr_No = SEQNO1;
                objrpc.InsRptrp(objrp, 4);
            }
            else
            {
                if (TREECTL.Nodes.Count == 0)
                {
                    xx = new TreeNode();
                    xx.Value = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                    xx.Text = (OPORCL == "OP" ? Convert.ToString(RSFILL.Tables[0].Rows[i][4]) : "CLOSING BALANCE") + "(0.00)";
                    TREECTL.Nodes.Add(xx);
                    GRNO = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                }
                else
                {
                    xx = new TreeNode();
                    xx.Value = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                    xx.Text = (OPORCL == "OP" ? Convert.ToString(RSFILL.Tables[0].Rows[i][4]) : "CLOSING BALANCE") + "(0.00)";
                    TREECTL.Nodes.Add(xx);
                    GRNO = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                }
                SEQNO1 = SEQNO1 + 1;

                //insert           
                objrp.Party_Name = Convert.ToString(RSFILL.Tables[0].Rows[0][4]);
                objrp.OBalanace =0;
                objrp.Rph_No = OPORCL == "OP" ? 1 : 2;
                objrp.LNo = 1;
                objrp.FGroup = 0;
                objrp.Level_No = 0 ;
                objrp.Gr_No = SEQNO1;
                objrpc.InsRptrp(objrp, 4);

                FillRpOpCl(Convert.ToInt32(RSFILL.Tables[0].Rows[0][0]), TREECTL, OPORCL);
            }

        }
                
    }

    public void FillRpOpCl(int prNo,TreeView TREECTL,string OPORCL)
    {
        //This procedure is used to add subgroup or ledger for opening and closing balance
        TreeView ctl;
        int[] grNo=new int[100];
        int cnt=0,tot;
        string DRCR=string .Empty ;
        double CURAMT=0.0;
        TreeNode yy = null;
        int ii;
                
        DataSet rscom = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "GRNO,OBALANCE,CLBALANCE,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LNO,FGROUP,LEVELNO", "FGROUP=" + prNo + "AND GRNO<>0", "");
        for (int i = 0; i < rscom.Tables[0].Rows.Count; i++)
        { 
                if (OPORCL == "OP")
                {
                    CURBALANCE = CURBALANCE + Convert.ToDouble(rscom.Tables[0].Rows[i][1]);
                    //DRCR = Format(rsfilter(1), "##,###.00")
                    DRCR =Convert.ToString( rscom.Tables[0].Rows[i][1]);
                    CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][1]);
                }
                else if (OPORCL == "CL")
                {
                    CURBALANCE = CURBALANCE + Convert.ToDouble(rscom.Tables[0].Rows[i][2]);
                    DRCR = Convert.ToString(rscom.Tables[0].Rows[i][2]);
                    CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][2]);
                }
            
            yy=new TreeNode ();
            yy.Value ="S" + Convert.ToString( rscom.Tables[0].Rows[i][0]) ;
            yy.Text=Convert.ToString( rscom.Tables[0].Rows[i]["name"])+DRCR ;
            TREECTL.Nodes.Add(yy);

            SEQNO1 = SEQNO1 + 1;

            //insert           
            objrp.Party_Name = Convert.ToString(rscom.Tables[0].Rows[0]["name"]);
            objrp.OBalanace = CURAMT;
            objrp.Rph_No = OPORCL == "OP" ? 1 : 2;
            objrp.LNo = Convert.ToInt32(rscom.Tables[0].Rows[0]["lno"]);
            objrp.FGroup = Convert.ToInt32(rscom.Tables[0].Rows[0]["fgroup"]);
            objrp.Level_No = string.IsNullOrEmpty(rscom.Tables[0].Rows[0]["levelno"].ToString()) ? 0 : Convert.ToInt32(rscom.Tables[0].Rows[0]["levelno"]);
            objrp.Gr_No = SEQNO1;
            objrpc.InsRptrp(objrp, 4);

            grNo[cnt] = Convert.ToInt32(rscom.Tables[0].Rows[i][0]);
            cnt+=1;
                       
        }
        tot = cnt;
        for(int i=0;i<cnt;i++ )
        {
            FillRpOpCl(grNo[i],TREECTL,OPORCL );
        }

        rscom = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "PCD,OBALANCE,CLBALANCE,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LNO,FGROUP,LEVELNO", "FGROUP=" + prNo + "AND LEVELNO=100", "UPPER(PNAME)");

        ii = 1;
        for (int i = 0; i < rscom.Tables[0].Rows.Count; i++)
        {
            DRCR = ""; CURAMT = 0;

            if (OPORCL == "OP")
            {
                //DRCR = Format(rsfilter(1), "##,###.00")
                DRCR = Convert.ToString(rscom.Tables[0].Rows[i][1]);
                CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][1]);
            }
            else if (OPORCL == "CL")
            {
                DRCR = Convert.ToString(rscom.Tables[0].Rows[i][2]);
                CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][2]);
            }

            yy = new TreeNode();
            yy.Value = "L" + Convert.ToString(rscom.Tables[0].Rows[i][0]) + sOp + ii;
            yy.Text = Convert.ToString(rscom.Tables[0].Rows[i]["name"]) + DRCR;
            TREECTL.Nodes.Add(yy);
            SEQNO1 = SEQNO1 + 1;

            //insert           
            objrp.Party_Name = Convert.ToString(rscom.Tables[0].Rows[0]["name"]);
            objrp.OBalanace = CURAMT;
            objrp.Rph_No = OPORCL == "OP" ? 1 : 2;
            objrp.LNo = Convert.ToInt32(rscom.Tables[0].Rows[0]["lno"]);
            objrp.FGroup = Convert.ToInt32(rscom.Tables[0].Rows[0]["fgroup"]);
            objrp.Level_No = Convert.ToInt32 (rscom.Tables[0].Rows[0]["levelno"]);
            objrp.Gr_No = SEQNO1;
            objrpc.InsRptrp(objrp, 4);
            ii += 1;

        }
       
            
        
        
    }

    public void FILLRP(int PRNO, TreeView TREECTL ,int RECPAY)
    {
        //'This procedure is used to add subgroup or ledger under main head
        TreeView ctl;
        int[] grNo = new int[100];
        int cnt = 0, tot;
        string DRCR = string.Empty;
        double CURAMT = 0.0;
        TreeNode yy = null;

        DataSet rscom = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "GRNO,debit,credit,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LEVELNO,FGROUP,LNO", "FGROUP=" + PRNO + "AND GRNO <> 0 AND RPHEAD=" + RECPAY, "");
        for (int i = 0; i < rscom.Tables[0].Rows.Count; i++)
        {
            DRCR = ""; CURAMT = 0;
            if (RECPAY == 2)
            {
                DRCR = Convert.ToString(rscom.Tables[0].Rows[i][1]);
                CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][1]);
            }
            else if (RECPAY == 1)
            {
                DRCR = Convert.ToString(rscom.Tables[0].Rows[i][2]);
                CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][2]);
            }

            yy = new TreeNode();
            yy.Value = "S" + Convert.ToString(rscom.Tables[0].Rows[i][0]);
            yy.Text = Convert.ToString(rscom.Tables[0].Rows[i]["name"]) + DRCR;
            TREECTL.Nodes.Add(yy);

            SEQNO1 = SEQNO1 + 1;

            //insert           
            objrp.Party_Name = Convert.ToString(rscom.Tables[0].Rows[0]["name"]);
            objrp.OBalanace = CURAMT;
            objrp.Rph_No =RECPAY ;
            objrp.LNo = Convert.ToInt32(rscom.Tables[0].Rows[0]["lno"]);
            objrp.FGroup = Convert.ToInt32(rscom.Tables[0].Rows[0]["fgroup"]);
            objrp.Level_No = Convert.ToInt32(rscom.Tables[0].Rows[0]["levelno"]);
            objrp.Gr_No = SEQNO1;
            objrpc.InsRptrp(objrp, 4);

            grNo[cnt] = Convert.ToInt32(rscom.Tables[0].Rows[i][0]);
            cnt += 1;

        }
        tot = cnt;
        for (int i = 0; i < cnt; i++)
        {
            FILLRP (grNo[i], TREECTL, RECPAY );
        }

        rscom = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "PCD,debit,credit,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LNO,FGROUP,LEVELNO", "FGROUP=" + PRNO + "AND LEVELNO=100 AND RPHEAD="+RECPAY , "UPPER(PNAME)");

      
        for (int i = 0; i < rscom.Tables[0].Rows.Count; i++)
        {
            DRCR = ""; CURAMT = 0;

            if (RECPAY  == 1)
            {
                DRCR = Convert.ToString(rscom.Tables[0].Rows[i][1]);
                CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][1]);
            }
            else if (RECPAY  == 1)
            {
                DRCR = Convert.ToString(rscom.Tables[0].Rows[i][2]);
                CURAMT = Convert.ToDouble(rscom.Tables[0].Rows[i][2]);
            }

            yy = new TreeNode();
            yy.Value = "L" + Convert.ToString(rscom.Tables[0].Rows[i][0]);
            yy.Text = Convert.ToString(rscom.Tables[0].Rows[i]["name"]) + DRCR;
            TREECTL.Nodes.Add(yy);
            SEQNO1 = SEQNO1 + 1;

            //insert           
            objrp.Party_Name = Convert.ToString(rscom.Tables[0].Rows[0]["name"]);
            objrp.OBalanace = CURAMT;
            objrp.Rph_No = RECPAY ;
            objrp.LNo = Convert.ToInt32(rscom.Tables[0].Rows[0]["lno"]);
            objrp.FGroup = Convert.ToInt32(rscom.Tables[0].Rows[0]["fgroup"]);
            objrp.Level_No = Convert.ToInt32(rscom.Tables[0].Rows[0]["levelno"]);
            objrp.Gr_No = SEQNO1;
            objrpc.InsRptrp(objrp, 4);
           

        }

    }

    public void RPDATA(int RECORPAY,TreeView TREECTL)
    {
        //This procedure is used to prepare data for viewing or printing receipt/payment statement
        string DRCR = string.Empty; 
        double  CURAMT=0.0;
        TreeNode yy;

       DataSet  RSFILL = objCommon.FillDropDown("ACC_DC_RECIEPT_PRINT_GROUP G,ACC_DC_RECIEPT_PRINT_HEADS H", "RP_NO,RPH_NAME,RPH_TYPE", "RP_NAME,H.RPH_NO", "G.RPH_NO=H.RPH_NO AND G.RPPRNO=0 and (rp_no<>3 AND RP_NO<>" + RECORPAY + "AND H.rph_NO=" + RECORPAY + ")", "rp_name");

       for (int i = 0; i < RSFILL.Tables[0].Rows.Count; i++)
       {
           DRCR = ""; CURAMT = 0;

           DataSet rsfilter = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "GRNO,debit,credit,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LEVELNO,FGROUP,LNO,PCD ", "GRNO=" + Convert.ToInt32(RSFILL.Tables[0].Rows[i][0]), "");

           if (rsfilter.Tables[0].Rows.Count > 0)
           {
               if (RECORPAY == 2)
               {
                   CURBALANCE = CURBALANCE + Convert.ToDouble(rsfilter.Tables[0].Rows[i][1]);
                   //DRCR = Format(rsfilter(1), "##,###.00")
                   DRCR = Convert.ToString(rsfilter.Tables[0].Rows[i][1]);
                   CURAMT = Convert.ToDouble(rsfilter.Tables[0].Rows[i][1]);
               }
               else if (RECORPAY == 1)
               {
                   CURBALANCE = CURBALANCE + Convert.ToDouble(rsfilter.Tables[0].Rows[i][2]);
                   DRCR = Convert.ToString(rsfilter.Tables[0].Rows[i][2]);
                   CURAMT = Convert.ToDouble(rsfilter.Tables[0].Rows[i][2]);
               }

               if (TREECTL.Nodes.Count == 0)
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
                   yy.Text = Convert.ToString(rsfilter.Tables[0].Rows[i]["name"]) + DRCR;
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
               }
               else
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
                   yy.Text = Convert.ToString(rsfilter.Tables[0].Rows[i]["name"]) + DRCR;
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
               }


               SEQNO1 = SEQNO1 + 1;

               //insert           
               objrp.Party_Name = Convert.ToString(rsfilter.Tables[0].Rows[0]["name"]);
               objrp.OBalanace = CURAMT;
               objrp.Rph_No = Convert.ToInt32(RSFILL.Tables[0].Rows[i]["RPH_NO"]);
               objrp.LNo = Convert.ToInt32(rsfilter.Tables[0].Rows[0]["lno"]);
               objrp.FGroup = Convert.ToInt32(rsfilter.Tables[0].Rows[0]["fgroup"]);
               objrp.Level_No = string.IsNullOrEmpty(rsfilter.Tables[0].Rows[0]["levelno"].ToString()) ? 0 : Convert.ToInt32(rsfilter.Tables[0].Rows[0]["levelno"]);
               objrp.Gr_No = SEQNO1;
               objrpc.InsRptrp(objrp, 4);
           }
           else
           {
               if (TREECTL.Nodes.Count == 0)
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                   yy.Text ="S"+ Convert.ToString(RSFILL.Tables[0].Rows[i]["rph_name"]) + "(0.00)";
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
               }
               else
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                   yy.Text = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i]["rph_name"]) + "(0.00)";
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
               }
               

              FILLRP (Convert.ToInt32(RSFILL.Tables[0].Rows[0][4]), TREECTL, RECORPAY );
           }
                      
       }

       // '*** CODE TO ADD UNKNOWN HEAD AT BOTTOM **********
       RSFILL = objCommon.FillDropDown("ACC_DC_RECIEPT_PRINT_GROUP G,ACC_DC_RECIEPT_PRINT_HEADS H", "RP_NO,RPH_NAME,RPH_TYPE", "RP_NAME,H.RPH_NO", "G.RPH_NO=H.RPH_NO AND G.RPPRNO=0 and (rp_no=" + RECORPAY + " AND H.rph_NO=" + RECORPAY + ")", "rp_nO");

       for (int i = 0; i < RSFILL.Tables[0].Rows.Count; i++)
       {
           DRCR = ""; CURAMT = 0;

           DataSet rsfilter = objCommon.FillDropDown("TEMP_ACC_RPTRPMAIN", "GRNO,debit,credit,(CASE WHEN GRNAME='' or grname is null THEN PNAME ELSE GRNAME END) AS NAME", "LNO,FGROUP,LEVELNO ", "GRNO=" + Convert.ToInt32(RSFILL.Tables[0].Rows[i][0]), "");

           if (rsfilter.Tables[0].Rows.Count > 0)
           {
               if (RECORPAY == 2)
               {
                   CURBALANCE = CURBALANCE + Convert.ToDouble(rsfilter.Tables[0].Rows[i][1]);
                   //DRCR = Format(rsfilter(1), "##,###.00")
                   DRCR = Convert.ToString(rsfilter.Tables[0].Rows[i][1]);
                   CURAMT = Convert.ToDouble(rsfilter.Tables[0].Rows[i][1]);
               }
               else if (RECORPAY == 1)
               {
                   CURBALANCE = CURBALANCE + Convert.ToDouble(rsfilter.Tables[0].Rows[i][2]);
                   DRCR = Convert.ToString(rsfilter.Tables[0].Rows[i][2]);
                   CURAMT = Convert.ToDouble(rsfilter.Tables[0].Rows[i][2]);
               }

               if (TREECTL.Nodes.Count == 0)
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
                   yy.Text = Convert.ToString(rsfilter.Tables[0].Rows[i]["name"]) +"("+ DRCR+")";
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
               }
               else
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
                   yy.Text = Convert.ToString(rsfilter.Tables[0].Rows[i]["name"]) + "(" + DRCR + ")";
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(rsfilter.Tables[0].Rows[i][0]);
               }


               SEQNO1 = SEQNO1 + 1;

               //insert           
               objrp.Party_Name = Convert.ToString(rsfilter.Tables[0].Rows[0]["name"]);
               objrp.OBalanace = CURAMT;
               objrp.Rph_No = RECORPAY ;
               objrp.LNo = Convert.ToInt32(rsfilter.Tables[0].Rows[0]["lno"]);
               objrp.FGroup = Convert.ToInt32(rsfilter.Tables[0].Rows[0]["fgroup"]);
               objrp.Level_No = string.IsNullOrEmpty(rsfilter.Tables[0].Rows[0]["levelno"].ToString()) ? 0 : Convert.ToInt32(rsfilter.Tables[0].Rows[0]["levelno"]);
               objrp.Gr_No = SEQNO1;
               objrpc.InsRptrp(objrp, 4);

               FILLRP(Convert.ToInt32(RSFILL.Tables[0].Rows[0][0]), TREECTL, RECORPAY);
           }
           else
           {
               if (TREECTL.Nodes.Count == 0)
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                   yy.Text = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i]["rph_name"]) + "(0.00)";
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
               }
               else
               {
                   yy = new TreeNode();
                   yy.Value = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
                   yy.Text = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i]["rph_name"]) + "(0.00)";
                   TREECTL.Nodes.Add(yy);
                   GRNO = "S" + Convert.ToString(RSFILL.Tables[0].Rows[i][0]);
               }

               //'*** to call recursive function *****************************
               FILLRP(Convert.ToInt32(RSFILL.Tables[0].Rows[0][0]), TREECTL, RECORPAY);
           }

       }

    }

    public void FORMATTREE()
    { 
    //This procedure is used to arrange the nodes and subnodes of a tree
        //for (int i = 0; i < tvRec.Nodes.Count; i++)
        //{
        //    if (tvRec.Nodes[i].Value.ToString().Substring(0, 1) == "L")
        //    { 
           
        //    }
        //}

    //    For i = 1 To TreeView1(0).Nodes.Count
    //    If Mid(TreeView1(0).Nodes(i).Key, 1, 1) = "L" Then
    //        TreeView1(0).Nodes(i).ForeColor = vbBlue
    //    ElseIf Mid(TreeView1(0).Nodes(i).Key, 1, 1) = "S" Then
    //        'TreeView1(0).Nodes(i).Bold = True
    //    End If
    //Next
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
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@UserName=" + Session["userfullname"].ToString() + "," + "@P_PERIOD=" + txtFrmDate.Text.ToString().Trim() + " to " + txtUptoDate.Text.ToString().Trim() + ",@P_COMP_CODE=" + Session["comp_code"] + "," + "@P_FROMDATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + "," + "@P_TODATE=" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
            
        }
        catch (Exception ex)
        {

        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SetFinancialYear();
        rdbFormat1.Checked = true;
        rdbFormat2.Checked = false;
    }
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          