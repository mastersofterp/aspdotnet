﻿//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : Financial Year End
// CREATION DATE : 08-DEC-2009                                               
// CREATED BY    : JITENDRA CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class FinancialYearEnd_Test : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            btnSplit.Enabled = false;
            //Check Session
            txtActStartfinYear.Focus();
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
                    //CheckPageAuthorization();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //PopulateDropDown();
                    //PopulateListBox();
                    ViewState["action"] = "add";
                }
            }
            SetFinancialYear();
            tvLinks.Nodes.Clear();
            TrialBalanceReportController od = new TrialBalanceReportController();
            od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
            od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(lblFinYrStartDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(lblFinYrEndDate.Text).ToString("dd-MMM-yyyy"));
            GenerateTrialBalanceFormat(tvLinks);
            GenerateTrialBalanceFormat(tvLinks1);
            GetTotalAmount(true);
            //Fill_TreeLinksCurrentFinancialYear(objtree);

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
            lblFinYrStartDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy").ToString();
            lblFinYrEndDate.Text =  Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy").ToString();
            string []  fromdate=lblFinYrStartDate.Text.ToString().Split('/');
            string[] todate = lblFinYrEndDate.Text.ToString().Split('/');
            int StartYear = 0;
            int EndYear = 0;
            if (fromdate.Length == 3)
            {
                if (fromdate[0].ToString().Trim() == "01" && fromdate[1].ToString().Trim() == "04")
                {

                    StartYear = Convert.ToInt16(fromdate[2].ToString().Trim());
                }
                          
            }
            if (todate.Length == 3)
            {
                if (todate[0].ToString().Trim() == "31" && todate[1].ToString().Trim() == "03")
                {

                    EndYear = Convert.ToInt16(todate[2].ToString().Trim());
                }

            }

            int ActYear = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year;
            lblActFinYrEndDate.Text = Convert.ToDateTime(lblFinYrEndDate.Text).ToString("dd/MM/yyyy").ToString();
            txtActStartfinYear.Text = Convert.ToDateTime(lblFinYrStartDate.Text).ToString("dd/MM/yyyy").ToString();
            if (Convert.ToString(StartYear).Trim() == Convert.ToString(EndYear - 1).Trim())
            {
                txtActStartfinYear.Enabled = false;
                imgCal.Visible = false;
                //btnGo.Enabled = true;
                btnEndFin.Enabled = true;
                btnEndFin.Focus();

            }
            else
            {
                txtActStartfinYear.Enabled = true;
                imgCal.Visible = true;
                //btnGo.Enabled = false;
                btnEndFin.Enabled = false;
                txtActStartfinYear.Focus();
            }


            
            

        }
        dtr.Close();

    
    }
    private void CheckPageAuthorization()
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
    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }
    private void ClearRecord()
    {
       
    
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        

    }
    protected void GenerateTrialBalanceFormat( TreeView objtree)
    {
        try
        {
            TreeNode xx = null;
            TreeNode yy = null;
            TreeNode zz = null;
            string GlobalTrnMode = string.Empty;
            int pName = 0;
            TrialBalanceReport oEntity = new TrialBalanceReport();
            DataSet dsLdg = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "*", "", string.Empty, string.Empty);
            if (dsLdg != null)
            {
                if (dsLdg.Tables[0].Rows.Count > 0)
                {
                    double TotalDr5 = 0;
                    double TotalCr5 = 0;
                    double TotalClosingBalance5 = 0;

                    int i = 0;
                    for (i = 0; i < dsLdg.Tables[0].Rows.Count; i++)
                    {
                        double TotalDr = 0;
                        double TotalCr = 0;
                        double TotalClosingBalance = 0;
                       
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
                            TrialBalanceReportController oTran = new TrialBalanceReportController();
                            oTran.AddTrialBalanceReportFormat(oEntity);

                            xx = new TreeNode();  // this is defination of the node.
                            if (Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim()) <= 0)
                            {
                                GlobalTrnMode = "Dr";
                            }
                            else
                            { GlobalTrnMode = "Cr"; }
                            xx.Text = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim() + " ( " + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                            xx.ToolTip = dsLdg.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim() + " ( " + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[i]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                            xx.Value = dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();
                            xx.SelectAction = TreeNodeSelectAction.Expand;
                            // adding node to root
                            objtree.Nodes.Add(xx);


                            int j = 0;
                            double TotalDr1 = 0;
                            double TotalCr1 = 0;
                            double TotalClosingBalance1 = 0;

                            for (j = 0; j < dsLdg.Tables[0].Rows.Count; j++) //2 for loop
                            {
                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) == 0)
                                {


                                    if (pName != 0)
                                    {
                                        TrialBalanceReportController oUpd5 = new TrialBalanceReportController();
                                        TrialBalanceReport oEnUpd5 = new TrialBalanceReport();
                                        oEnUpd5.MGRPNO = pName;
                                        oEnUpd5.DEBIT = TotalDr5;
                                        oEnUpd5.CREDIT = TotalCr5;
                                        oUpd5.UpdateTrialBalanceAmount(oEnUpd5);
                                        double CloseBal = TotalClosingBalance5;//TotalCr5 + TotalDr5;
                                        string TrnMode = string.Empty;
                                        if (CloseBal <= 0)
                                        {
                                            TrnMode = "Dr";
                                        }
                                        else
                                        { TrnMode = "Cr"; }
                                         TotalDr5 = 0;
                                         TotalCr5 = 0;
                                         int y = 0;
                                         for (y = 0; y < objtree.Nodes.Count; y++)
                                         {
                                             int m = 0;
                                             if (objtree.Nodes[y].Value.ToString().Trim() == pName.ToString().Trim())
                                             {
                                                 if (CloseBal != 0)
                                                 {

                                                     objtree.Nodes[y].Text = objtree.Nodes[y].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(CloseBal).ToString() +" " + TrnMode.ToString() +"  ) ";
                                                     objtree.Nodes[y].ToolTip = objtree.Nodes[y].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(CloseBal).ToString() + " " + TrnMode.ToString() + "  ) ";
                                                     CloseBal = 0;
                                                     TotalClosingBalance5 = 0;
                                                 }
                                             }
                                             else
                                             {//
                                                 if (objtree.Nodes[y].ChildNodes.Count > 0)
                                                 {///
                                                     for (m = 0; m < objtree.Nodes[y].ChildNodes.Count; m++)
                                                     {

                                                         if (objtree.Nodes[y].ChildNodes[m].Value.ToString().Trim() == pName.ToString().Trim())
                                                         {
                                                             if (CloseBal != 0)
                                                             {
                                                                 objtree.Nodes[y].ChildNodes[m].Text = objtree.Nodes[y].ChildNodes[m].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(CloseBal).ToString() + " " + TrnMode.ToString() + " ) ";
                                                                 objtree.Nodes[y].ChildNodes[m].ToolTip = objtree.Nodes[y].ChildNodes[m].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(CloseBal).ToString() + " " + TrnMode.ToString() + " ) ";
                                                                 CloseBal = 0;
                                                                 TotalClosingBalance5 = 0;
                                                             }
                                                         }

                                                     }
                                                 }///
                                             }//
                                         
                                         
                                         }


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
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                    zz = new TreeNode();    // this is defination of the node.


                                    if (Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim()) <= 0)
                                    {
                                        GlobalTrnMode = "Dr";
                                    }
                                    else
                                    { GlobalTrnMode = "Cr"; }

                                    zz.Text = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    zz.ToolTip = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    zz.Value =dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim();
                                    zz.SelectAction = TreeNodeSelectAction.Expand;
                                    // adding node as child of node xx.
                                    xx.ChildNodes.Add(zz);
                                    xx.Expanded = false;


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

                                           TotalDr1 = TotalDr1 + oEntity.DEBIT;
                                           TotalCr1 = TotalCr1 + oEntity.CREDIT;
                                           TotalClosingBalance1 = TotalClosingBalance1 + oEntity.CLBALANCE;

                                           //TotalDr5 = TotalDr5 + oEntity.DEBIT;
                                           //TotalCr5 = TotalCr5 + oEntity.CREDIT;   
                                           TrialBalanceReportController oTran8 = new TrialBalanceReportController();
                                           oTran8.AddTrialBalanceReportFormat(oEntity);


                                           yy = new TreeNode();    // this is defination of the node.
                                           if (Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim()) <= 0)
                                           {
                                               GlobalTrnMode = "Dr";
                                           }
                                           else
                                           { GlobalTrnMode = "Cr"; }

                                           yy.Text = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode.ToString() + " " +" )";
                                           yy.ToolTip = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode.ToString() + " " + " )";
                                           yy.Value = dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim();
                                           yy.SelectAction = TreeNodeSelectAction.Expand;
                                           // adding node as child of node xx.
                                           xx.ChildNodes.Add(yy);
                                           xx.Expanded = false;
                                          
                                                                              
                                        }
                                     }
                                    
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
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    TotalDr1 = TotalDr1 + oEntity.DEBIT;
                                    TotalCr1 = TotalCr1 + oEntity.CREDIT;
                                    TotalClosingBalance1 = TotalClosingBalance1 + oEntity.CLBALANCE;

                                    TotalDr5 = TotalDr5 + oEntity.DEBIT;
                                    TotalCr5 = TotalCr5 + oEntity.CREDIT;
                                    TotalClosingBalance5 = TotalClosingBalance5 + oEntity.CLBALANCE;
                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();//TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);

                                    yy = new TreeNode();    // this is defination of the node.
                                    if (Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim()) <= 0)
                                    {
                                        GlobalTrnMode = "Dr";
                                    }
                                    else
                                    { GlobalTrnMode = "Cr"; }

                                    yy.Text = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    yy.ToolTip = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    yy.Value = dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim();
                                    yy.SelectAction = TreeNodeSelectAction.Expand;
                                    // adding node as child of node xx.
                                    zz.ChildNodes.Add(yy);
                                    zz.Expanded = false;

                                   
                                }
                                

                             }// End of 2 for loop
                            TrialBalanceReportController oUpd = new TrialBalanceReportController();
                            TrialBalanceReport oEnUpd = new TrialBalanceReport();
                            oEnUpd.MGRPNO =Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]);
                            oEnUpd.DEBIT = TotalDr1;
                            oEnUpd.CREDIT = TotalCr1;
                            oUpd.UpdateTrialBalanceAmount(oEnUpd);
                            double closebal1 = TotalClosingBalance1;
                            string TrnMode1 = string.Empty;
                            if (closebal1 <= 0)
                            {
                                TrnMode1 = "Dr";
                            }
                            else
                            { TrnMode1 = "Cr"; }
                            int k = 0;
                           
                            for (k = 0; k < objtree.Nodes.Count; k++)
                            {
                                if (objtree.Nodes[k].Value.ToString().Trim() == dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim())
                                {
                                    if (closebal1 != 0)
                                    {
                                        objtree.Nodes[k].Text = objtree.Nodes[k].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(closebal1).ToString() + " " + TrnMode1.ToString() + " ) ";
                                        objtree.Nodes[k].ToolTip = objtree.Nodes[k].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(closebal1).ToString() + " " + TrnMode1.ToString() + " ) ";
                                    }

                                }

                                
                            }

                            if (pName != 0)
                            {
                                TrialBalanceReportController oUpd7 = new TrialBalanceReportController();
                                TrialBalanceReport oEnUpd7 = new TrialBalanceReport();
                                oEnUpd7.MGRPNO = pName;
                                oEnUpd7.DEBIT = TotalDr5;
                                oEnUpd7.CREDIT = TotalCr5;
                                oUpd7.UpdateTrialBalanceAmount(oEnUpd7);

                                int l = 0;
                                int d = 0;
                                double closebal2 = TotalClosingBalance5;
                                string TrnMode2 = string.Empty;
                                if (closebal2 <= 0)
                                {
                                    TrnMode2 = "Dr";
                                }
                                else
                                { TrnMode2 = "Cr"; }
                                for (l = 0; l < objtree.Nodes.Count; l++)
                                {
                                    if (objtree.Nodes[l].Value.ToString().Trim() == pName.ToString().Trim())
                                    {
                                        if (closebal2 != 0)
                                        {
                                            objtree.Nodes[l].Text = objtree.Nodes[l].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(closebal2).ToString() + " " + TrnMode2.ToString() + " ) ";
                                            objtree.Nodes[l].ToolTip = objtree.Nodes[l].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(closebal2).ToString() + " " + TrnMode2.ToString() + " ) ";
                                        }

                                    }
                                    else
                                    {//
                                        if (objtree.Nodes[l].ChildNodes.Count > 0)
                                        {///
                                            for (d = 0; d < objtree.Nodes[l].ChildNodes.Count; d++)
                                            {

                                                if (objtree.Nodes[l].ChildNodes[d].Value.ToString().Trim() == pName.ToString().Trim())
                                                {
                                                    if (closebal2 != 0)
                                                    {
                                                        objtree.Nodes[l].ChildNodes[d].Text = objtree.Nodes[l].ChildNodes[d].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(closebal2).ToString() + " " + TrnMode2.ToString() + " ) ";
                                                        objtree.Nodes[l].ChildNodes[d].ToolTip = objtree.Nodes[l].ChildNodes[d].Text.Split('(')[0].ToString().Trim() + " ( " + Math.Abs(closebal2).ToString() + " " + TrnMode2.ToString() + " ) ";
                                                        closebal2 = 0;
                                                        TotalClosingBalance5 = 0;
                                                    }
                                                }

                                            }
                                        }///
                                    }//
                                   

                                }

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

                                  
                                    oEntity.PartyName =  dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    pName = oEntity.MGRPNO;
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    oEntity.ISPARTY = 0;//Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());
                                    TrialBalanceReportController oTran1 = new TrialBalanceReportController();
                                    oTran1.AddTrialBalanceReportFormat(oEntity);

                                    zz = new TreeNode();    // this is defination of the node.
                                    if (Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim()) <= 0)
                                    {
                                        GlobalTrnMode = "Dr";
                                    }
                                    else
                                    { GlobalTrnMode = "Cr"; }
                                    zz.Text = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    zz.ToolTip = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    zz.Value = dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim();
                                    zz.SelectAction = TreeNodeSelectAction.Expand;
                                    // adding node as child of node xx.
                                    xx.ChildNodes.Add(zz);
                                    xx.Expanded = false;


                                }


                                if (dsLdg.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim() == dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim() && Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"]) != 0)
                                {


                                    oEntity.PartyName = "     ".ToString() + dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim();
                                    oEntity.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim());
                                    oEntity.PRNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PRNO"].ToString().Trim());
                                    oEntity.PARTYNO = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["PARTY_NO"].ToString().Trim());
                                    oEntity.OPBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["OP_BALANCE"].ToString().Trim());
                                    oEntity.CLBALANCE = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim());
                                    oEntity.DEBIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["DEBIT"].ToString().Trim());
                                    oEntity.CREDIT = Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CREDIT"].ToString().Trim());
                                    TotalDr2 = TotalDr2 + oEntity.DEBIT;
                                    TotalCr2 = TotalCr2 + oEntity.CREDIT;
                                    TotalDr6 = TotalDr6 + oEntity.DEBIT;
                                    TotalCr6 = TotalCr6 + oEntity.CREDIT;

                                    oEntity.ISPARTY = Convert.ToInt16(dsLdg.Tables[0].Rows[j]["IS_PARTY"].ToString().Trim());

                                    TrialBalanceReportController oTran2 = new TrialBalanceReportController();
                                    oTran2.AddTrialBalanceReportFormat(oEntity);


                                    yy = new TreeNode();    // this is defination of the node.
                                    if (Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim()) <= 0)
                                    {
                                        GlobalTrnMode = "Dr";
                                    }
                                    else
                                    { GlobalTrnMode = "Cr"; }
                                    yy.Text = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    yy.ToolTip = dsLdg.Tables[0].Rows[j]["PARTYNAME"].ToString().Trim() + " (" + Math.Abs(Convert.ToDouble(dsLdg.Tables[0].Rows[j]["CL_BALANCE"].ToString().Trim())) + " " + GlobalTrnMode + " )";
                                    yy.Value = dsLdg.Tables[0].Rows[j]["MGRP_NO"].ToString().Trim();
                                    yy.SelectAction = TreeNodeSelectAction.Expand;
                                    // adding node as child of node xx.
                                    zz.ChildNodes.Add(yy);
                                    zz.Expanded = false;
                                }



                            }// end of for loop 3
                            //TrialBalanceReportController oUpd1 = new TrialBalanceReportController();

                            //TrialBalanceReport oEnUpd1 = new TrialBalanceReport();
                            //oEnUpd1.MGRPNO = Convert.ToInt16(dsLdg.Tables[0].Rows[i]["MGRP_NO"]);
                            //oEnUpd1.DEBIT = TotalDr2;
                            //oEnUpd1.CREDIT = TotalCr2;
                            //oUpd1.UpdateTrialBalanceAmount(oEnUpd1);


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
                 if (TotalOpeningBalance > 0)
                 {
                     oEntity.DEBIT = TotalOpeningBalance;

                 }
                 else
                 {
                     oEntity.CREDIT = Math.Abs(TotalOpeningBalance);
                   
                 }



                 oEntity.ISPARTY = 1;
                TrialBalanceReportController oTran4 = new TrialBalanceReportController();
                oTran4.AddTrialBalanceReportFormat(oEntity);

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
    //protected void btnShowTrialBalance_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        if (lblFinYrStartDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
    //            lblFinYrStartDate.Focus();
    //            return;
    //        }
    //        if (lblFinYrEndDate.Text.ToString().Trim() == "")
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
    //            lblFinYrEndDate.Focus();
    //            return;
    //        }


    //        if (DateTime.Compare(Convert.ToDateTime(lblFinYrEndDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
    //            lblFinYrEndDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
    //            lblFinYrEndDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(lblFinYrStartDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
    //            lblFinYrStartDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
    //            lblFinYrStartDate.Focus();
    //            return;
    //        }

    //        if (DateTime.Compare(Convert.ToDateTime(lblFinYrStartDate.Text), Convert.ToDateTime(lblFinYrEndDate.Text)) == 1)
    //        {
    //            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
    //            lblFinYrEndDate.Focus();
    //            return;
    //        }
    //        TrialBalanceReportController od = new TrialBalanceReportController();
    //        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString() + "_" + Session["fin_yr"].ToString().Trim());
    //        od.GenerateTrialBalance(Session["comp_code"].ToString() + "_" + Session["fin_yr"].ToString().Trim(), Convert.ToDateTime(lblFinYrStartDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(lblFinYrEndDate.Text).ToString("dd-MMM-yyyy"));
    //        GenerateTrialBalanceFormat();
    //      //  ShowLedgerListReport("TrialBalance", "TrialBalanceReport.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowTrialBalance_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");

    //    }




    //}
    protected void btnGo_Click(object sender, EventArgs e)
    {
       
        DateTime date2;
        if (!(DateTime.TryParse(txtActStartfinYear.Text, out date2)))
        {

            objCommon.DisplayMessage(UPDLedger, "Invalid Date Is Entered. ", this);
            txtActStartfinYear.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtActStartfinYear.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtActStartfinYear.Text), Convert.ToDateTime(lblActFinYrEndDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Actual Financial Year Start Date Can Not Be Greater Than Financial Year End Date.", this);
            txtActStartfinYear.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtActStartfinYear.Focus();
            return;
        
        }
        if (DateTime.Compare(Convert.ToDateTime(lblFinYrStartDate.Text), Convert.ToDateTime(txtActStartfinYear.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Actual Financial Year Start Date Can Not Be Less Than Current Financial Year Start Date.", this);
            txtActStartfinYear.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtActStartfinYear.Focus();
            return;

        }

        string[] fromdate = txtActStartfinYear.Text.ToString().Split('/');
        string[] todate = lblActFinYrEndDate.Text.ToString().Split('/');
        int StartYear = 0;
        int EndYear = 0;
        if (fromdate.Length == 3)
        {
            if (fromdate[0].ToString().Trim() == "01" && fromdate[1].ToString().Trim() == "04")
            {

                StartYear = Convert.ToInt16(fromdate[2].ToString().Trim());
            }
            else
            {
                txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
                btnGo.Focus();
                return;
            }

        }
        else
        {
            txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
            btnGo.Focus();
            return;
        }
        if (todate.Length == 3)
        {
            if (todate[0].ToString().Trim() == "31" && todate[1].ToString().Trim() == "03")
            {

                EndYear = Convert.ToInt16(todate[2].ToString().Trim());
            }
            else
            {
                txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
                btnGo.Focus();
                return;
            }
        }
        else
        {
            txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
            btnGo.Focus();
            return;
        }

        if (Convert.ToString(StartYear).Trim() != Convert.ToString(EndYear - 1).Trim())
        {
            txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();

        }

        lblFinYrEndDate.Text = "31/03/" + Convert.ToString(Convert.ToDateTime(txtActStartfinYear.Text).Year).Trim();

        tvLinks.Nodes.Clear();
        TrialBalanceReportController od1 = new TrialBalanceReportController();
        od1.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
        od1.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(lblFinYrStartDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(lblFinYrEndDate.Text).ToString("dd-MMM-yyyy"));
        GenerateTrialBalanceFormat(tvLinks);
        GetTotalAmount(true);

        tvLinks1.Nodes.Clear();
        TrialBalanceReportController od = new TrialBalanceReportController();
        od.DeleteTrialBalanceReportFormat(Session["comp_code"].ToString());
        od.GenerateTrialBalance(Session["comp_code"].ToString(), Convert.ToDateTime(txtActStartfinYear.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(lblActFinYrEndDate.Text).ToString("dd-MMM-yyyy"));
        GenerateTrialBalanceFormat(tvLinks1);
        GetTotalAmount(false);
        btnSplit.Enabled = true;
    }
    private void GetTotalAmount(Boolean isCurrentYear)
    {
       
        DataSet dstot = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_TRAN", "Sum(credit)Credit", "Sum(debit)Debit", string.Empty, string.Empty);
      if (dstot != null)
      {
          if (dstot.Tables[0].Rows.Count != 0)
          {
              if (isCurrentYear == true)
              {
                  lbltotCurrCr.Text = dstot.Tables[0].Rows[0]["Credit"].ToString().Trim();
                  lbltotCurrDr.Text = dstot.Tables[0].Rows[0]["Debit"].ToString().Trim();
                  lbltotCurrDiff.Text = (Convert.ToDouble(dstot.Tables[0].Rows[0]["Credit"])- Convert.ToDouble(dstot.Tables[0].Rows[0]["Debit"])).ToString().Trim();
                  if (Convert.ToDouble(lbltotCurrDiff.Text) > 0)
                  {
                      lbltotCurrDiff.Text = Convert.ToDouble(lbltotCurrDiff.Text).ToString("0.00").Trim() + " Cr";

                  }
                  else
                  {
                      lbltotCurrDiff.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(lbltotCurrDiff.Text))).ToString("0.00").Trim() + " Dr";
                  
                  }

                  lbltotActCr.Text = dstot.Tables[0].Rows[0]["Credit"].ToString().Trim();
                  lbltotActDr.Text = dstot.Tables[0].Rows[0]["Debit"].ToString().Trim();
                  lbltotActDiff.Text = (Convert.ToDouble(dstot.Tables[0].Rows[0]["Credit"]) - Convert.ToDouble(dstot.Tables[0].Rows[0]["Debit"])).ToString("0.00").Trim();
                  if (Convert.ToDouble(lbltotActDiff.Text) > 0)
                  {
                      lbltotActDiff.Text = lbltotActDiff.Text.ToString().Trim() + " Cr";

                  }
                  else
                  {
                      lbltotActDiff.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(lbltotActDiff.Text))).ToString("0.00").Trim() + " Dr";

                  }

              }
              else
              {

                  lbltotActCr.Text = dstot.Tables[0].Rows[0]["Credit"].ToString().Trim();
                  lbltotActDr.Text = dstot.Tables[0].Rows[0]["Debit"].ToString().Trim();
                  lbltotActDiff.Text = (Convert.ToDouble(dstot.Tables[0].Rows[0]["Credit"]) - Convert.ToDouble(dstot.Tables[0].Rows[0]["Debit"])).ToString("0.00").Trim();
                  if (Convert.ToDouble(lbltotActDiff.Text) > 0)
                  {
                      lbltotActDiff.Text = Convert.ToDouble(lbltotActDiff.Text).ToString("0.00").Trim() + " Cr";

                  }
                  else
                  {
                      lbltotActDiff.Text =Convert.ToDouble(Math.Abs(Convert.ToDouble(lbltotActDiff.Text))).ToString("0.00").Trim() + " Dr";

                  }

              
              }
          
          
          }
      
      }
    
    }

    protected void txtActStartfinYear_TextChanged(object sender, EventArgs e)
    {
        DateTime date2;
        if (!(DateTime.TryParse(txtActStartfinYear.Text, out date2)))
        {

            objCommon.DisplayMessage(UPDLedger, "Invalid Date Is Entered. ", this);
            txtActStartfinYear.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtActStartfinYear.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtActStartfinYear.Text), Convert.ToDateTime(lblActFinYrEndDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Actual Financial Year Start Date Can Not Be Greater Than Financial Year End Date.", this);
            txtActStartfinYear.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtActStartfinYear.Focus();
            return;

        }

        if (DateTime.Compare(Convert.ToDateTime(lblFinYrStartDate.Text), Convert.ToDateTime(txtActStartfinYear.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Actual Financial Year Start Date Can Not Be Less Than Current Financial Year Start Date.", this);
            txtActStartfinYear.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtActStartfinYear.Focus();
            return;

        }


        string[] fromdate = txtActStartfinYear.Text.ToString().Split('/');
        string[] todate = lblActFinYrEndDate.Text.ToString().Split('/');
        int StartYear = 0;
        int EndYear = 0;

        if (todate.Length == 3)
        {
            if (todate[0].ToString().Trim() == "31" && todate[1].ToString().Trim() == "03")
            {

                EndYear = Convert.ToInt16(todate[2].ToString().Trim());
            }
            else
            {
                txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
                btnGo.Focus();
                return;
            }
        }
        else
        {
            txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
            btnGo.Focus();
            return;
        }


        if (fromdate.Length == 3)
        {
            if (fromdate[0].ToString().Trim() == "01" && fromdate[1].ToString().Trim() == "04")
            {

                StartYear = Convert.ToInt16(fromdate[2].ToString().Trim());
            }
            else
            {
                txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
                btnGo.Focus();
                return;
            }

        }
        else
        {
            txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();
            btnGo.Focus();
            return;
        }
        
        if (Convert.ToString(StartYear).Trim() != Convert.ToString(EndYear - 1).Trim())
        {
            txtActStartfinYear.Text = "01/04/" + Convert.ToString(EndYear - 1).Trim();

        }
        btnSplit.Enabled = false;
        btnGo.Focus();

    }
    protected void btnSplit_Click(object sender, EventArgs e)
    {

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     