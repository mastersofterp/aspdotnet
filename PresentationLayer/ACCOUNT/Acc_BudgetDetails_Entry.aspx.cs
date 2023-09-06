//=================================================================================
// PROJECT NAME  :  UAIMS - RFC-SVCEC                                                          
// MODULE NAME   :  BUDGET_DETAILS_ENTRY                                                    
// CREATION DATE :  20-OCT-2019                                               
// CREATED BY    :  ANDOJU VIJAY                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.Web.Caching;
using System.Drawing;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections;
using IITMS.SQLServer.SQLDAL;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;


public partial class ACCOUNT_Acc_BudgetDetails_Entry : System.Web.UI.Page
{

    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    // static int bankId = 0;
    Acc_BudgetDetailsEnrty_Entity ObjEnt = new Acc_BudgetDetailsEnrty_Entity();
    Acc_BudgetDetailsEnrty_Controller ObjCon = new Acc_BudgetDetailsEnrty_Controller();

    #region PageLoad Event
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
            objCommon = new Common();
        }
        else
        {
            //Response.Redirect("Default.aspx");
            Response.Redirect("~/Default.aspx");

        }


        objCommon = new Common();
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");

            }

            PopulateDropDownList();
            if (Convert.ToInt32(Session["usertype"].ToString()) == 8)
            {
                ddldept.SelectedValue = Session["UA_EmpDeptNo"].ToString();
                ddldept.Enabled = false;
            }
            else
            {
                ddldept.Enabled = true;
            }
           
            #region Comment
            //else
            //{
            //    if (Session["comp_code"] == null)
            //    {
            //        Session["comp_set"] = "NotSelected";

            //        objCommon.DisplayUserMessage(updBank, "Select company/cash book.", this);

            //        Response.Redirect("~/Account/selectCompany.aspx");
            //    }
            //    else
            //    {

            //        Session["comp_set"] = "";
            //        //Page Authorization
            //        //CheckPageAuthorization();

            //        divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
            //        // Page.Title = Session["coll_name"].ToString();



            //        //DataSet ds = new DataSet();

            //        //if (HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()] == null)
            //        //{

            //        //    ds = objCommon.FillDropDown("CCMS_HELP_client", "HELPDESC", "PAGENAME", "PAGENAME='" + objCommon.GetCurrentPageName() + "'", "");
            //        //}
            //        //else
            //        //{
            //        //    ds = (DataSet)HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()];
            //        //    DataView dv = ds.Tables[0].DefaultView;
            //        //    dv.RowFilter = "PAGENAME='" + objCommon.GetCurrentPageName() + "'";
            //        //    ds.Tables.Remove("Table");
            //        //    ds.Tables.Add(dv.ToTable());

            //        //}
            //        //if (ds.Tables[0].Rows.Count > 0)
            //        //{
            //        //    lblHelp.Text = ds.Tables[0].Rows[0]["HELPDESC"].ToString();
            //        //}
            //        //else
            //        //{
            //        //    lblHelp.Text = "No Help Present!";

            //        //}
            //    }
            //}

            //objCommon.FillDropDownList(ddlLedgerName, TableName, "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO=2", "PARTY_NO");
            #endregion

            SetFinancialYear();
            divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
            Page.Title = Session["coll_name"].ToString();
                  
            //  PopulateDropDownList();
            ViewState["action"] = "add";
            //txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
            //txttodate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
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
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txttodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
            //txtDate.Text = DateTime.Now.Date.ToShortDateString();
        }
        dtr.Close();


    }
    #endregion

    #region PrivateMethods

    private void PopulateDropDownList()
    {

        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "");

        // ddldept.Enabled = false;
        objCommon.FillDropDownList(ddlbudgetparent, "ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "BUDGET_HEAD", "PARENT_ID=0", "");

        ////  string FndDept = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPTNO=" + Session["userdeptno"].ToString());
        //  if (FndDept.ToString()!=null)
        //  {
        //      ddldept.SelectedValue = FndDept;
        //    //  ddldept.Enabled = tr;


        if (ddldept.SelectedIndex == 0)
        {
            clearall();
            return;
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("ACC_BUDGET_ALLOCATION_NEW", "FROM_DATE", "TO_DATE", " DEPT_ID =" + Convert.ToInt32(ddldept.SelectedValue), "");
            // DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW B INNER JOIN ACC_BUDGET_ALLOCATION_DETAILS_NEW A ON(A.BUDGET_NO=B.BUDGET_NO)", "DISTINCT A.FROM_DATE,A.TO_DATE", "B.PARENT_ID", "A.DEPT_ID=" + Convert.ToInt32(ddldept.SelectedValue) + " AND  A.APPROVED_SATUS='A'", "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();

                    //Showbudget();
                }
                else
                { // Showbudget();
                    txtFromDate.Text = string.Empty;
                    txttodate.Text = string.Empty;
                }
                ds = null;
                ddlbudgetparent.SelectedValue = "0";
                btnsubmit.Enabled = true;

            }
            Showbudget();
            //      }
        }
    }
    //private void Getdates()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACC_BUDGET_ALLOCATION_NEW", "FROM_DATE", "TO_DATE", " DEPT_ID =" + Session["userdeptno"].ToString(), "");
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
    //            txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
    //            //Showbudget();
    //        }
    //        else
    //        {

    //            // Showbudget();
    //            txtFromDate.Text = string.Empty;
    //            txttodate.Text = string.Empty;
    //        }
    //        //  ds = null;
    //        ddlbudgetparent.SelectedValue = "0";
    //        btnsubmit.Enabled = true;
    //        ddldept.Enabled = false;
    //    }
    //}

    //private void PopulateDropDownList()
    //{


    //    objCommon.FillDropDownList(ddldept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO=" + Session["userdeptno"].ToString(), "");
    //    Getdates();
    //    objCommon.FillDropDownList(ddlbudgetparent, "ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "BUDGET_HEAD", "PARENT_ID=0", "");

    //}
    private void Showbudget()
    {
        checkbudget.Visible = true;
        DataSet ds = null;
        ObjEnt.FROM_DATE = Convert.ToDateTime(txtFromDate.Text);
        ObjEnt.TO_DATE = Convert.ToDateTime(txttodate.Text);
        ObjEnt.Dept_id = Convert.ToInt32(ddldept.SelectedValue);
        ds = ObjCon.Get_Data_By_Deptid_ForEnt(ObjEnt);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
               // btnsubmit.Visible = true;
            }

            else
            {
                if (ddlbudgetparent.SelectedValue == "0")
                {
                    ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_CODE," + "BUDGET_NO," + "0.00 AS DEPT_AMOUNT," + "0.00 AS REV_DEPT_AMOUNT,NULL AS REV_APPROVED_SATUS,0.00 AS NXTYR_DEPT_AMOUNT,NULL AS NXTYR_APPROVED_SATUS,NULL AS APPROVED_SATUS", "BUDGET_HEAD", "BUDGET_NO NOT IN (SELECT DISTINCT PARENT_ID FROM ACC_BUDGET_HEAD_NEW) AND PARENT_ID<>0", "BUDGET_NO asc");
                }

                else
                {
                    ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_CODE," + "BUDGET_NO," + "0.00 AS DEPT_AMOUNT," + "0.00 AS REV_DEPT_AMOUNT,NULL AS REV_APPROVED_SATUS,0.00 AS NXTYR_DEPT_AMOUNT,NULL AS NXTYR_APPROVED_SATUS,NULL AS APPROVED_SATUS", "BUDGET_HEAD", "PARENT_ID=" + Convert.ToInt32(ddlbudgetparent.SelectedValue), "BUDGET_NO asc");
                }
            }
        }

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    foreach (DataTable data in ds.Tables)
        //    {
        //        foreach (DataRow dk in data.Rows)
        //        {
        //            int PARENT_ID = Convert.ToInt32(dk["BUDGET_NO"].ToString());
        //            if (PARENT_ID != null)
        //            {
        //                DataSet dr = null;
        //                dr = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_HEAD", "BUDGET_NO", "PARENT_ID=" +PARENT_ID, "");

        //            }
        //        }
        //    }

        //}
        

        lvbudgethead.DataSource = ds;
        lvbudgethead.DataBind();
        ds = null;

        chkRevised.Checked = false;
        chkProposed.Checked = false;
    }
    public void Clear()
    {
        checkbudget.Visible = false;
            ddldept.SelectedValue = "0";
            ddlbudgetparent.SelectedValue = "0";


            ddlbudgetparent.SelectedIndex = 0;
            pnlBudget.Visible = false;
            btnsubmit.Visible = false;
            lvbudgethead.DataSource = null;
            lvbudgethead.DataBind();
            chkRevised.Checked = false;
            chkProposed.Checked = false;
      
    }
    private void ChechDept_id()
    {
        int dept_id = Convert.ToInt32(objCommon.LookUp("ACC_BUDGET_ALLOCATION_NEW", "ISNULL(MAX(DEPT_ID),0)", "DEPT_ID =" + Convert.ToInt32(ddldept.SelectedValue) + ""));
        if (dept_id > 0)
        {
            Session["Budget_id"] = dept_id.ToString();
        }

        else
            Session["Budget_id"] = 0;
    }
    private void AddtoTable()
    {
        foreach (ListViewDataItem gdb in lvbudgethead.Items)
        {

            TextBox txtamount = gdb.FindControl("txtbudgetallocation") as TextBox;
            TextBox txtRevised = gdb.FindControl("txtRevised") as TextBox;
            TextBox txtProposed = gdb.FindControl("txtProposed") as TextBox;

            string budgetid = (gdb.FindControl("hdnbudgetno") as HiddenField).Value;

            DataTable dt = new DataTable();
            try
            {
                if (chkRevised.Checked == true && chkProposed.Checked == true)              //Revised AND Proposed budget
                {
                    if (txtRevised.Enabled == true && txtProposed.Enabled == true)
                    {
                        if (ViewState["actionCo"] == null)
                        {
                            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                            {
                                int maxVal = 0;
                                dt = (DataTable)Session["BUDTBL"];
                                DataRow dr = dt.NewRow();
                                if (dr != null)
                                {
                                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["BUDGET_NO"]));
                                }

                                dr["BUDGET_ALLOCDETAIL_ID"] = maxVal + 1;
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(txttodate.Text);
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                                dt.Rows.Add(dr);
                                Session["BUDTBL"] = dt;

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                            }
                            else
                            {

                                dt = this.CreateBudgetTable();
                                DataRow dr = dt.NewRow();
                                dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                                dt.Rows.Add(dr);

                                Session["BUDTBL"] = dt;


                            }
                        }
                        else
                        {
                            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                            {
                                dt = (DataTable)Session["BUDTBL"];
                                DataRow dr = dt.NewRow();

                                dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["EDIT_BUDGET_NO_BUDGET"]);
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());
                                dt.Rows.Add(dr);

                                Session["BUDTBL"] = dt;

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                            }
                        }
                    }
                }
                else if (chkRevised.Checked == true)                                          //Revised Budget
                {                                              
                    if (txtRevised.Enabled == true)
                    {
                        if (ViewState["actionCo"] == null)
                        {
                            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                            {
                                int maxVal = 0;
                                dt = (DataTable)Session["BUDTBL"];
                                DataRow dr = dt.NewRow();
                                if (dr != null)
                                {
                                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["BUDGET_NO"]));
                                }

                                dr["BUDGET_ALLOCDETAIL_ID"] = maxVal + 1;
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(txttodate.Text);
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                                dt.Rows.Add(dr);
                                Session["BUDTBL"] = dt;

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                            }
                            else
                            {

                                dt = this.CreateBudgetTable();
                                DataRow dr = dt.NewRow();
                                dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                                dt.Rows.Add(dr);

                                Session["BUDTBL"] = dt;


                            }
                        }
                        else
                        {
                            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                            {
                                dt = (DataTable)Session["BUDTBL"];
                                DataRow dr = dt.NewRow();

                                dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["EDIT_BUDGET_NO_BUDGET"]);
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());
                                dt.Rows.Add(dr);

                                Session["BUDTBL"] = dt;

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                            }
                        }
                    }
                }
                else if (chkProposed.Checked == true)                                      //Proposed Budget
                {
                    if (txtProposed.Enabled == true)
                    {
                        if (ViewState["actionCo"] == null)
                        {
                            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                            {
                                int maxVal = 0;
                                dt = (DataTable)Session["BUDTBL"];
                                DataRow dr = dt.NewRow();
                                if (dr != null)
                                {
                                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["BUDGET_NO"]));
                                }

                                dr["BUDGET_ALLOCDETAIL_ID"] = maxVal + 1;
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(txttodate.Text);
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                                dt.Rows.Add(dr);
                                Session["BUDTBL"] = dt;

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                            }
                            else
                            {

                                dt = this.CreateBudgetTable();
                                DataRow dr = dt.NewRow();
                                dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                                dt.Rows.Add(dr);

                                Session["BUDTBL"] = dt;


                            }
                        }
                        else
                        {
                            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                            {
                                dt = (DataTable)Session["BUDTBL"];
                                DataRow dr = dt.NewRow();

                                dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["EDIT_BUDGET_NO_BUDGET"]);
                                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                                dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                                dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                                dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                                dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());
                                dt.Rows.Add(dr);

                                Session["BUDTBL"] = dt;

                                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                            }
                        }
                    }
                }
                    //ADDED BY TANU 29/11/2021
                if (ViewState["actionCo"] == null)
                {
                    if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                    {
                        int maxVal = 0;
                        dt = (DataTable)Session["BUDTBL"];
                        DataRow dr = dt.NewRow();
                        if (dr != null)
                        {
                            maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["BUDGET_NO"]));
                        }

                        dr["BUDGET_ALLOCDETAIL_ID"] = maxVal + 1;
                        dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                        dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                        dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                        dr["TO_DATE"] = Convert.ToDateTime(txttodate.Text);
                        dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                        dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                        dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                        dt.Rows.Add(dr);
                        Session["BUDTBL"] = dt;

                        ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                    }
                    else
                    {

                        dt = this.CreateBudgetTable();
                        DataRow dr = dt.NewRow();
                        dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                        dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                        dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                        dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                        dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                        dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                        dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                        dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());

                        ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
                        dt.Rows.Add(dr);

                        Session["BUDTBL"] = dt;


                    }
                }
                else
                {
                    if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
                    {
                        dt = (DataTable)Session["BUDTBL"];
                        DataRow dr = dt.NewRow();

                        dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ViewState["EDIT_BUDGET_NO_BUDGET"]);
                        dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
                        dr["BUDGET_NO"] = Convert.ToInt32(budgetid.ToString());
                        dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
                        dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
                        dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text.Trim());
                        dr["REV_DEPT_AMT"] = Convert.ToDouble(txtRevised.Text.Trim());
                        dr["NXTYR_DEPT_AMT"] = Convert.ToDouble(txtProposed.Text.Trim());
                        dt.Rows.Add(dr);

                        Session["BUDTBL"] = dt;

                        ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                    }
                }

            }

            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "STORES_Transactions_Quotation_DirectIndent.btnAddC_Click() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }

        }

    }
    public void clearall()
    {
       
        // GridData.DataSource = null;
        //GridData.DataBind();
        Clear();
        txtFromDate.Text = string.Empty;
        txttodate.Text = string.Empty;
        ddldept.SelectedValue = "0";
        ddlbudgetparent.SelectedValue = "0";
        Session["BUDTBL"] = null;
        ViewState["action"] = "add";
        ViewState["BUDGET_NO"] = null;
        Session["BUDTBL"] = null;
        ViewState["actionCo"] = null;

        Session["Budget_id"] = 0;
        SetFinancialYear();

        //txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
        //txttodate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());

        lvbudgethead.DataSource = null;

        lvbudgethead.DataBind();

    }
    private DataTable CreateBudgetTable()
    {
        DataTable dtCo = new DataTable();

        dtCo.Columns.Add(new DataColumn("BUDGET_ALLOCDETAIL_ID", typeof(int)));
        dtCo.Columns.Add(new DataColumn("DEPT_ID", typeof(int)));
        dtCo.Columns.Add(new DataColumn("FROM_DATE", typeof(DateTime)));
        dtCo.Columns.Add(new DataColumn("TO_DATE", typeof(DateTime)));
        dtCo.Columns.Add(new DataColumn("DEPT_AMOUNT", typeof(double)));
        dtCo.Columns.Add(new DataColumn("BUDGET_NO", typeof(int)));
        dtCo.Columns.Add(new DataColumn("REV_DEPT_AMT", typeof(double)));
        dtCo.Columns.Add(new DataColumn("NXTYR_DEPT_AMT", typeof(double)));

        return dtCo;


    }
    //private int CmpFDate_TDate()
    //{
    //    DateTime from_date, To_date;
    //    from_date = DateTime.Parse(Convert.ToDateTime(txtFromDate.Text).ToShortDateString());
    //    To_date = DateTime.Parse(Convert.ToDateTime(txttodate.Text).ToShortDateString());
    //    if (from_date > To_date)
    //    {

    //    }
    //}

    #endregion

    #region Events

    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        DateTime frmdate, todate;
        if (txttodate.Text == null || txttodate.Text == "" || txtFromDate.Text == "" || txtFromDate.Text == null)
        {
            Showbudget();
        }
        else
        {
            frmdate = Convert.ToDateTime(txtFromDate.Text);
            todate = Convert.ToDateTime(txttodate.Text);
            if (frmdate > todate)
            {
                txttodate.Text = string.Empty;
                objCommon.DisplayUserMessage(UPBudget, "To Date Must be Greter then From Date ", this.Page);
            }
        }


    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        AddtoTable();
        ChechDept_id();
        DataTable dt = null;
        try
        {
            string bgid = Session["Budget_id"].ToString();
            ObjEnt.BUDGETALLOCTION_ID = Convert.ToInt32(Session["Budget_id"].ToString());
            ObjEnt.COLLEGE_ID = Convert.ToInt32(Session["colcode"].ToString());
            ObjEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
            ObjEnt.TO_DATE = Convert.ToDateTime(Session["to_date"].ToString());
            ObjEnt.FROM_DATE = Convert.ToDateTime(Session["f_date"].ToString());
            //   ObjEnt.DEPT_AMOUNT = Convert.ToDouble(txtamount.Text);
            ObjEnt.Dept_id = Convert.ToInt32(ddldept.SelectedValue);
            //DataTable dt = null;
            dt = (DataTable)Session["BUDTBL"];
            ObjEnt.Budgettable = dt;


            if (ViewState["action"].ToString() == "add")
            {

                CustomStatus cs = (CustomStatus)ObjCon.Ins_Upd_BudgetDetailsDep(ObjEnt);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(UPBudget,"This Budget Record is Updated Successfully", this.Page);
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(UPBudget,"This Budget Record is Saved Successfully", this.Page);
                }
                //if (cs.Equals(CustomStatus.RecordExist))
                //{
                //    objCommon.DisplayUserMessage(UPBudget, "Approved Amount Can't be Change/Update", this.Page);
                //}
            }
            dt = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_DirectIndent.btnEditCommittee_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            dt = null;
        }

        clearall();
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddldept.SelectedIndex == 0)
        {
            return;
        }
        else
        {

            Session["to_date"] = txttodate.Text;
            Session["f_date"] = txtFromDate.Text;
            ObjEnt.Parent_id = Convert.ToInt32(ddlbudgetparent.SelectedValue);
            pnlBudget.Visible = true;
            btnsubmit.Visible = true;
            Showbudget();
        }
        chkRevised.Checked = false;
        chkProposed.Checked = false;
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldept.SelectedIndex == 0)
        {
            clearall();
            return;
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("ACC_BUDGET_ALLOCATION_NEW", "FROM_DATE", "TO_DATE", " DEPT_ID =" + Convert.ToInt32(ddldept.SelectedValue), "");
            // DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW B INNER JOIN ACC_BUDGET_ALLOCATION_DETAILS_NEW A ON(A.BUDGET_NO=B.BUDGET_NO)", "DISTINCT A.FROM_DATE,A.TO_DATE", "B.PARENT_ID", "A.DEPT_ID=" + Convert.ToInt32(ddldept.SelectedValue) + " AND  A.APPROVED_SATUS='A'", "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    //txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                    SetFinancialYear();
                    //Showbudget();
                }
                else
                {
                    // Showbudget();
                    //txtFromDate.Text = string.Empty;
                    ////txttodate.Text = string.Empty;
                    //txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
                    //txttodate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
                    SetFinancialYear();
                }
                ds = null;
                ddlbudgetparent.SelectedValue = "0";
                btnsubmit.Enabled = true;

            }
            Showbudget();
        }
    }
    protected void ddlbudgetparent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjEnt.Parent_id = Convert.ToInt32(ddlbudgetparent.SelectedValue);
        Showbudget();

    }


    #endregion

    #region Commented

    //protected void btnadd_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        #region comments

    //        //  string Srno = string.Empty;

    //        //  DataTable dtTran = new DataTable();
    //        //  if (Session["dtTran"] != null)
    //        //      dtTran = (DataTable)Session["dtTran"];

    //        //  if (!dtTran.Columns.Contains("BUDGET_NO"))
    //        //      dtTran.Columns.Add("BUDGET_NO");

    //        //  if (!dtTran.Columns.Contains("BUDGET_ALLOCDETAIL_ID"))
    //        //      dtTran.Columns.Add("BUDGET_ALLOCDETAIL_ID");

    //        //  if (!dtTran.Columns.Contains("DEPT_ID"))
    //        //      dtTran.Columns.Add("DEPT_ID");

    //        //  if (!dtTran.Columns.Contains("FROM_DATE"))
    //        //      dtTran.Columns.Add("FROM_DATE");

    //        //  if (!dtTran.Columns.Contains("TO_DATE"))
    //        //      dtTran.Columns.Add("TO_DATE");

    //        //  if (!dtTran.Columns.Contains("DEPT_AMOUNT"))
    //        //      dtTran.Columns.Add("DEPT_AMOUNT");

    //        //  int SerialNumber = Convert.ToInt32(ViewState["SerialNumber"] == null ? "0" : ViewState["SerialNumber"]);


    //        //  DataRow dr = dtTran.NewRow();
    //        //  if (ViewState["SerialNumber"] != null)
    //        //  {
    //        //      dr["BUDGET_NO"] = ViewState["SerialNumber"].ToString();
    //        //  }
    //        //  else
    //        //  {
    //        //      if (ViewState["BUDGET_NO"] != null)
    //        //          dr["BUDGET_NO"] = 1 + Convert.ToInt32(ViewState["BUDGET_NO"] == null ? "0" : ViewState["BUDGET_NO"]);
    //        //      else
    //        //          dr["BUDGET_NO"] = Convert.ToInt32(SerialNumber) + 1;
    //        //  }
    //        ////  dr["VoucherNo"] = "0";
    //        //  //dr["PartyId"] = txtAccount.Text.Split('*')[0].ToString() + '*' + dr["Srno"];
    //        //  dr["BUDGET_ALLOCDETAIL_ID"] = ddlBudgetHead.SelectedValue.ToString();
    //        //  dr["DEPT_ID"] = ddldept.SelectedValue.ToString();
    //        //  dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.txt);
    //        //  dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
    //        //  dr["DEPT_AMOUNT"] =Convert.ToDouble(txtamount.Text);
    //        //  //dr["Transaction"] = ddlcrdr.SelectedItem.Text;
    //        //  //dr["OrderPaymentReceiptId"] = ddlOrder.SelectedValue.ToString();
    //        //  dtTran.Rows.Add(dr);
    //        //  GridData.DataSource = dtTran;
    //        //  GridData.DataBind();

    //        //  DataRow lastRow = dtTran.Rows[dtTran.Rows.Count - 1];
    //        //  ViewState["BUDGET_NO"] = Convert.ToInt32(lastRow["BUDGET_NO"]);

    //        //  caltotalamount(dtTran);
    //        // // ClearAccEntry();
    //        //  Session["dtTran"] = dtTran;
    //        //  //txtAccount.Focus();
    //        #endregion
    //        // -------------------------------------------------------------------------------------------------------------------------
    //        if (ViewState["actionCo"] == null)
    //        {
    //            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
    //            {
    //                int maxVal = 0;
    //                dt = (DataTable)Session["BUDTBL"];
    //                DataRow dr = dt.NewRow();
    //                if (dr != null)
    //                {
    //                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["BUDGET_NO"]));
    //                }

    //                dr["BUDGET_NO"] = maxVal + 1;
    //                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
    //             //   dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ddlBudgetHead.SelectedValue);
    //                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
    //                dr["TO_DATE"] = Convert.ToDateTime(txttodate.Text);
    //               // dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text);
    //                //dr["DESIGNATION"] = ddlDesignation.SelectedItem.Text;

    //                dt.Rows.Add(dr);
    //                Session["BUDTBL"] = dt;
    //                GridData.DataSource = dt;
    //                GridData.DataBind();
    //                //ClearCommittee();
    //                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
    //            }
    //            else
    //            {

    //                dt = this.CreateBudgetTable();
    //                DataRow dr = dt.NewRow();
    //                dr["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
    //                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
    //               // dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ddlBudgetHead.SelectedValue);
    //                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
    //                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
    //              //  dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text);

    //                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;
    //                dt.Rows.Add(dr);
    //                //ClearCommittee();
    //                Session["BUDTBL"] = dt;
    //                GridData.DataSource = dt;
    //                GridData.DataBind();

    //            }
    //        }
    //        else
    //        {
    //            if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
    //            {
    //                dt = (DataTable)Session["BUDTBL"];
    //                DataRow dr = dt.NewRow();

    //                dr["BUDGET_NO"] = Convert.ToInt32(ViewState["EDIT_BUDGET_NO_BUDGET"]);
    //                dr["DEPT_ID"] = Convert.ToInt32(ddldept.SelectedValue);
    //                //dr["BUDGET_ALLOCDETAIL_ID"] = Convert.ToInt32(ddlBudgetHead.SelectedValue);
    //                dr["FROM_DATE"] = Convert.ToDateTime(txtFromDate.Text);
    //                dr["TO_DATE"] = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text));
    //              //  dr["DEPT_AMOUNT"] = Convert.ToDouble(txtamount.Text);

    //                dt.Rows.Add(dr);

    //                Session["BUDTBL"] = dt;
    //                GridData.DataSource = dt;
    //                GridData.DataBind();
    //                //.ClearCommittee();
    //                ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

    //            }
    //        }

    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "STORES_Transactions_Quotation_DirectIndent.btnAddC_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    caltotalamount(dt);

    //    Session["to_date"] =Convert.ToDateTime(txttodate.Text);
    //    Session["f_date"] = Convert.ToDateTime(txtFromDate.Text);
    //    Clear();
    //}

    //protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    //{
    //    #region Comment
    //    // ImageButton btnEdit = sender as ImageButton;
    //    //// int SerialNo = 0;

    //    //     DataTable dtTran = new DataTable();
    //    //     if (Session["dtTran"] != null)
    //    //         dtTran = (DataTable)Session["dtTran"];

    //    //     DataView dv = dtTran.DefaultView;
    //    //     dv.RowFilter = "BUDGET_NO=" + btnEdit.ToolTip;
    //    //     DataTable dtRow = dv.ToTable();
    //    //     txtamount.Text = dtRow.Rows[0]["DEPT_AMOUNT"].ToString();
    //    //     ddldept.SelectedValue = dtRow.Rows[0]["DEPT_ID"].ToString();
    //    //     Convert.ToDateTime(txtFromDate.txt) = dtRow.Rows[0]["FROM_DATE"].ToString();
    //    //     Convert.ToDateTime(Convert.ToDateTime(txttodate.Text)) = dtRow.Rows[0]["TO_DATE"].ToString();
    //    //     ddlBudgetHead.SelectedValue = dtRow.Rows[0]["BUDGET_ALLOCDETAIL_ID"].ToString();
    //    //     ViewState["BUDGET_NO"] = dtRow.Rows[0]["BUDGET_NO"].ToString();
    //    #endregion
    //    try
    //    {
    //        ViewState["EDIT_BUDGET_NO_BUDGET"] = string.Empty;
    //        ImageButton btnEditCommittee = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
    //        {
    //            dt = ((DataTable)Session["BUDTBL"]);
    //            ViewState["EDIT_BUDGET_NO_BUDGET"] = btnEditCommittee.ToolTip;
    //            DataRow dr = this.GetEditableDatarowItem(dt, ViewState["EDIT_BUDGET_NO_BUDGET"].ToString());
    //            ddldept.SelectedValue = dr["DEPT_ID"].ToString();
    //            //ddlBudgetHead.SelectedValue = dr["BUDGET_ALLOCDETAIL_ID"].ToString();
    //            txtFromDate.Text = dr["FROM_DATE"].ToString();
    //            txttodate.Text = dr["TO_DATE"].ToString();
    //          //  txtamount.Text = dr["DEPT_AMOUNT"].ToString();
    //            dt.Rows.Remove(dr);
    //            Session["BUDTBL"] = dt;
    //            GridData.DataSource = dt;
    //            GridData.DataBind();
    //            ViewState["actionCo"] = "edit";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["erreor"]) == true)
    //            objCommon.ShowError(Page, "STORES_Transactions_Quotation_DirectIndent.btnEditCommittee_Click -->" + ex.Message + "" + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}



    //protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton btndelete = sender as ImageButton;
    //    DataTable dt;
    //    if (Session["BUDTBL"] != null && ((DataTable)Session["BUDTBL"]) != null)
    //    {
    //        dt = ((DataTable)Session["BUDTBL"]);
    //        ViewState["EDIT_BUDGET_NO_BUDGET"] = btndelete.CommandArgument;

    //        DataRow dr = this.GetEditableDatarowItem(dt, btndelete.ToolTip);
    //        dt.Rows.Remove(dr);
    //        GridData.DataSource = dt;
    //        GridData.DataBind();
    //        caltotalamount(dt);
    //    }
    //}

    #endregion

    //protected void lvbudgethead_ItemCommand1(object source, RepeaterCommandEventArgs e)
    //{
    //    int id = (int)e.CommandArgument;
    //    //RepeaterItem item = e.Item;

    //    if (id==1)
    //    {
    //        foreach (RepeaterItem item in lvbudgethead.Items)
    //        {
    //            TextBox lab = item.FindControl("txtbudgetallocation") as TextBox;
    //            lab.Enabled = false;
    //        }


    //    }

    //}
    //protected void lvbudgethead_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        TextBox txtamt = e.Item.FindControl("txtbudgetallocation") as TextBox;
    //        HiddenField isac = e.Item.FindControl("lnkEdit") as HiddenField;
    //        if (isac.ToString() == "1")
    //        {
    //            txtamt.Enabled = false;
    //        }
    //    }
    //}

    protected void lvbudgethead_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        // int a = 0;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            // Added By Mohd. Faraz

            //Label label12 = (Label)e.Item.FindControl("lnkEdit");
            //TextBox txtbudget = (TextBox)e.Item.FindControl("txtbudgetallocation");
            //if (label12.Text.Trim() == "A")
            //{
            //    txtbudget.Enabled = false;
            //  //  btnsubmit.Visible = false;
            //}
            //else
            //{
            //    txtbudget.Enabled = true;
            //}

            
            Label lblRevStatus = (Label)e.Item.FindControl("lblRevStatus");
            TextBox txtRevised = (TextBox)e.Item.FindControl("txtRevised");
            // string s = label12.Text;
            if (lblRevStatus.Text.Trim() == "A")
            {
                txtRevised.Enabled = false;
                //btnsubmit.Visible = false;
            }
            else
            {
                txtRevised.Enabled = true;
            }

            Label lblPropStatus = (Label)e.Item.FindControl("lblPropStatus");
            TextBox txtProposed = (TextBox)e.Item.FindControl("txtProposed");
            // string s = label12.Text;
            if (lblPropStatus.Text.Trim() == "A")
            {
                txtProposed.Enabled = false;
                //btnsubmit.Visible = false;
            }
            else
            {
                txtProposed.Enabled = true;
            }
            // Comment End By Mohd. Faraz
        }

    }

    protected void chkRevised_CheckedChanged(object sender, EventArgs e)
    {
        if (lvbudgethead.Items.Count > 0)
        {
            if (chkRevised.Checked == true && chkProposed.Checked == true)
            {
                foreach (ListViewDataItem item in lvbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label RevStatus = item.FindControl("lblRevStatus") as Label;
                    Label PropStatus = item.FindControl("lblPropStatus") as Label;

                    if (RevStatus.Text.Trim() == "A")
                    {
                        txtRevised.Enabled = false;
                        //btnsubmit.Visible = false;
                    }
                    else
                    {
                        txtRevised.Enabled = true;
                    }

                    if (PropStatus.Text.Trim() == "A")
                    {
                        txtProposed.Enabled = false;
                        //btnsubmit.Visible = false;
                    }
                    else
                    {
                        txtProposed.Enabled = true;
                    }
                    //txtProposed.Enabled = true;
                    txtProposed.Visible = true;
                    //txtRevised.Enabled = true;
                    txtRevised.Visible = true;
                }
            }
            else if (chkRevised.Checked == true)
            {
                foreach (ListViewDataItem item in lvbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label RevStatus = item.FindControl("lblRevStatus") as Label;

                    if (RevStatus.Text.Trim() == "A")
                    {
                        txtRevised.Enabled = false;
                        //btnsubmit.Visible = false;
                    }
                    else
                    {
                        txtRevised.Enabled = true;
                    }
                    //txtRevised.Enabled = true;
                    txtProposed.Enabled = false;
                    txtRevised.Visible = true;
                    
                }
            }
            else if (chkProposed.Checked == true)
            {
                foreach (ListViewDataItem item in lvbudgethead.Items)
                {
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    Label PropStatus = item.FindControl("lblPropStatus") as Label;
                    if (PropStatus.Text.Trim() == "A")
                    {
                        txtProposed.Enabled = false;
                        //btnsubmit.Visible = false;
                    }
                    else
                    {
                        txtProposed.Enabled = true;
                    }
                    txtRevised.Enabled = false;
                    txtProposed.Visible = true;
                    //txtProposed.Enabled = true;
                }
            }
            
            else if (chkRevised.Checked == false && chkProposed.Checked == false)
            {
                foreach (ListViewDataItem item in lvbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label RevStatus = item.FindControl("lblRevStatus") as Label;
                    Label PropStatus = item.FindControl("lblPropStatus") as Label;

                    txtRevised.Visible = false;
                    txtProposed.Visible = false;
                }
            }
            else if (chkRevised.Checked == false)
            {
                foreach (ListViewDataItem item in lvbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    Label RevStatus = item.FindControl("lblRevStatus") as Label;
                    Label PropStatus = item.FindControl("lblPropStatus") as Label;

                    txtRevised.Visible = false;
                }
            }
            //if (chkProposed.Checked == false)
            else
            {
                foreach (ListViewDataItem item in lvbudgethead.Items)
                {
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label RevStatus = item.FindControl("lblRevStatus") as Label;
                    Label PropStatus = item.FindControl("lblPropStatus") as Label;

                    txtProposed.Visible = false;
                }
            }
        }
        else
        {
            chkRevised.Checked = false;
            chkProposed.Checked = false;
        }

            
    }
}