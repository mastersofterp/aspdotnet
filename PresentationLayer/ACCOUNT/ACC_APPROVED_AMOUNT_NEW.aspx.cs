//=================================================================================
// PROJECT NAME  :  UAIMS - RFC-SVCEC                                                          
// MODULE NAME   :  BUDGET_APPROVE                                                   
// CREATION DATE :  7-NOV-2019                                               
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


public partial class ACC_APPROVED_AMOUNT_NEW : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    static int bankId = 0;
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

            PopulateDropDownList();
            ViewState["action"] = "add";
            SetFinancialYear();
            divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
            Page.Title = Session["coll_name"].ToString();

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

    #region Private Mothods

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "");
        objCommon.FillDropDownList(ddlbudgetparent, "ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "BUDGET_HEAD", "PARENT_ID=0", "");
    }
    private void Showbudget()
    {

        DataSet ds = null;
        ObjEnt.FROM_DATE = Convert.ToDateTime(txtFromDate.Text);
        ObjEnt.TO_DATE = Convert.ToDateTime(txttodate.Text);
        ObjEnt.Dept_id = Convert.ToInt32(ddldept.SelectedValue);
        ds = ObjCon.Get_Data_By_Deptid(ObjEnt);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                if (ddlbudgetparent.SelectedValue == "0")
                {
                    ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_CODE," + "BUDGET_NO," + "0.00 AS DEPT_AMOUNT," + "0 AS APPROVED_AMOUNT,0 AS REV_DEPT_AMOUNT,0 AS REV_APPROVED_AMOUNT,0 AS NXTYR_DEPT_AMOUNT,0 AS NXTYR_APPROVED_AMOUNT", "BUDGET_HEAD", "BUDGET_NO NOT IN (SELECT DISTINCT PARENT_ID FROM ACC_BUDGET_HEAD_NEW)", "BUDGET_NO asc");
                }
                else
                {
                    ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_CODE," + "BUDGET_NO," + "0.00 AS DEPT_AMOUNT," + "0 AS APPROVED_AMOUNT,0 AS REV_DEPT_AMOUNT,0 AS REV_APPROVED_AMOUNT,0 AS NXTYR_DEPT_AMOUNT,0 AS NXTYR_APPROVED_AMOUNT", "BUDGET_HEAD", "PARENT_ID=" + Convert.ToInt32(ddlbudgetparent.SelectedValue), "BUDGET_NO asc");
                }
            }
        }
        Rptbudgethead.DataSource = ds;
        Rptbudgethead.DataBind();
        foreach (ListViewDataItem lvItem in Rptbudgethead.Items)
        {
            TextBox txtamount = lvItem.FindControl("txtbudgetallocation") as TextBox;
            Label Status = lvItem.FindControl("lblStatus") as Label;

            if (Status.Text == "APPROVED")
            {
                txtamount.Enabled = false;
            }
            else if (Status.Text == "PENDING")
            {
                txtamount.Enabled = true;
            }

        }

        ds = null;

        chkRevised.Checked = false;
        chkProposed.Checked = false;
    }
    //private void chckdept()
    //{
    //    if (ddldept.SelectedIndex == 0)
    //    {
    //        pnlBudgetApprove.Visible = false;
    //        btnsubmit.Visible = false;
    //        return;
    //    }
    //}
    public void Clear()
    {

        ddldept.SelectedValue = "0";
        ddlbudgetparent.SelectedValue = "0";
        txtFromDate.Text = string.Empty;
        txttodate.Text = string.Empty;
        pnlBudgetApprove.Visible = false;
        btnsubmit.Visible = false;
        btnRevied.Visible = false;
        btnProposed.Visible = false;
        txtstatus.Visible = false;
        divstatus.Visible = false; 
        SetFinancialYear();
        //txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
        //txttodate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
        Rptbudgethead.DataSource = null;
        Rptbudgethead.DataBind();
        chkRevised.Checked = false;
        chkProposed.Checked = false;

    }
    //private void ChechDept_id()
    //{
    //    int dept_id = Convert.ToInt32(objCommon.LookUp("ACC_BUDGET_ALLOCATION_NEW", "ISNULL(MAX(DEPT_ID),0)", "DEPT_ID =" + Convert.ToInt32(ddldept.SelectedValue) + ""));
    //    if (dept_id > 0)
    //    {
    //        Session["Budget_id"] = dept_id.ToString();
    //    }

    //    else
    //        Session["Budget_id"] = 0;
    //}
    private void AddtoTable()
    {
        foreach (ListViewDataItem gdb in Rptbudgethead.Items)
        {

            string txtamount = (gdb.FindControl("txtbudgetallocation") as TextBox).Text;
            string budgetid = (gdb.FindControl("hdnbudgetno") as HiddenField).Value;
            CheckBox chkselect = gdb.FindControl("chkSelect") as CheckBox;

            if (txtamount != "0.00" || txtamount != "0" || txtamount != null)
            {
                btnsubmit.Enabled = true;
            }
            DataTable dt = new DataTable();
            try
            {
                if (chkselect.Checked == true)
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
                            dr["APPROVED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());


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
                            dr["APPROVED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());

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
                            dr["APPROVED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());

                            dt.Rows.Add(dr);

                            Session["BUDTBL"] = dt;

                            ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                        }
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

    private void AddRevisedTable()
    {
        foreach (ListViewDataItem gdb in Rptbudgethead.Items)
        {
            string txtamount = (gdb.FindControl("txtRevised") as TextBox).Text;
            string budgetid = (gdb.FindControl("hdnbudgetno") as HiddenField).Value;
            CheckBox chkselect = gdb.FindControl("chkSelect") as CheckBox;

            if (txtamount != "0.00" || txtamount != "0" || txtamount != null)
            {
               btnsubmit.Enabled = true;
            }
            DataTable dt = new DataTable();
            try
            {
              if (chkselect.Checked == true) { 

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
                        dr["REVISED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());


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
                        dr["REVISED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());

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
                        dr["REVISED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());

                        dt.Rows.Add(dr);

                        Session["BUDTBL"] = dt;

                        ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                    }
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

    private void AddProposedTable()
    {
        foreach (ListViewDataItem gdb in Rptbudgethead.Items)
        {

            string txtamount = (gdb.FindControl("txtProposed") as TextBox).Text;
            string budgetid = (gdb.FindControl("hdnbudgetno") as HiddenField).Value;
            CheckBox chkselect = gdb.FindControl("chkSelect") as CheckBox;

            if (txtamount != "0.00" || txtamount != "0" || txtamount != null)
            {
                btnsubmit.Enabled = true;
            }
            DataTable dt = new DataTable();
            try
            {
                if (chkselect.Checked == true)
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
                            dr["PROPOSED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());


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
                            dr["PROPOSED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());

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
                            dr["PROPOSED_AMOUNT"] = Convert.ToDouble(txtamount.ToString());

                            dt.Rows.Add(dr);

                            Session["BUDTBL"] = dt;

                            ViewState["BUDGET_NO"] = Convert.ToInt32(ViewState["BUDGET_NO"]) + 1;

                        }
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
        txtstatus.Visible = false;
        Session["Budget_id"] = 0;
        //txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
        //txttodate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
        SetFinancialYear();
        Rptbudgethead.DataSource = null;
        Rptbudgethead.DataBind();
    }

    public void chechdate()
    {
        if (txtFromDate.Text == null || txtFromDate.Text == "" || txttodate.Text == null || txttodate.Text == "")
        {
            Session["to_date"] = null;
            Session["f_date"] = null;
        }
        else
            Session["to_date"] = txttodate.Text;
        Session["f_date"] = txtFromDate.Text;

    }
    private DataTable CreateBudgetTable()
    {
        DataTable dtCo = new DataTable();

        dtCo.Columns.Add(new DataColumn("BUDGET_ALLOCDETAIL_ID", typeof(int)));
        dtCo.Columns.Add(new DataColumn("DEPT_ID", typeof(int)));
        dtCo.Columns.Add(new DataColumn("FROM_DATE", typeof(DateTime)));
        dtCo.Columns.Add(new DataColumn("TO_DATE", typeof(DateTime)));
        dtCo.Columns.Add(new DataColumn("APPROVED_AMOUNT", typeof(double)));
        dtCo.Columns.Add(new DataColumn("REVISED_AMOUNT", typeof(double)));
        dtCo.Columns.Add(new DataColumn("PROPOSED_AMOUNT", typeof(double)));
        dtCo.Columns.Add(new DataColumn("BUDGET_NO", typeof(int)));

        return dtCo;


    }


    private void ChechDept_id()
    {
        int dept_id = Convert.ToInt32(objCommon.LookUp("ACC_BUDGET_ALLOCATION_NEW", "BUDGET_ALLOCTION_ID", "DEPT_ID =" + Convert.ToInt32(ddldept.SelectedValue) + ""));
        if (dept_id > 0)
        {
            Session["Budget_id"] = dept_id.ToString();
        }

        else
            Session["Budget_id"] = 0;
    }

    #endregion

    #region Events

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        AddtoTable();
        ChechDept_id();
        DataTable dt = null;
        try
        {
            ObjEnt.BUDGETALLOCTION_ID = Convert.ToInt32(Session["Budget_id"].ToString());
            ObjEnt.COLLEGE_ID = Convert.ToInt32(Session["colcode"].ToString());
            ObjEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
            ObjEnt.TO_DATE = Convert.ToDateTime(Session["to_date"].ToString());
            ObjEnt.FROM_DATE = Convert.ToDateTime(Session["f_date"].ToString());
            //   ObjEnt.DEPT_AMOUNT = Convert.ToDouble(txtamount.Text);
            ObjEnt.Dept_id = Convert.ToInt32(ddldept.SelectedValue);
            //DataTable dt = null;
            dt = (DataTable)Session["BUDTBL"];

            if (dt == null)
            {
                //Response.Write("<script>alert('Sorry This Department is not Applied..')</script>");
                objCommon.DisplayUserMessage(UPBudgetApprove,"Please select Atleast one checkbox to Approve Budget Amount.", this.Page);
                return;
            }
            else
            {
                ObjEnt.BudgetApproveTable = dt;
            }


            if (ViewState["action"].ToString() == "add")
            {

                CustomStatus cs = (CustomStatus)ObjCon.ApproveBudgetAmount(ObjEnt);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(UPBudgetApprove,"Budget Amount Approved Successfully ", this.Page);
                }
                else
                {

                    objCommon.DisplayUserMessage(UPBudgetApprove,"Budget Amount Approved Successfully", this.Page);

                }

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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        //chechdate();
        //ObjEnt.Parent_id = Convert.ToInt32(ddlbudgetparent.SelectedValue);
        //pnlBudgetApprove.Visible = true;
        //btnsubmit.Visible = true;
        //txtstatus.Visible = true;
        //Showbudget();
        if (ddldept.SelectedIndex == 0)
        {
            return;
        }
        else
        {

            Session["to_date"] = txttodate.Text;
            Session["f_date"] = txtFromDate.Text;

            ObjEnt.Parent_id = Convert.ToInt32(ddlbudgetparent.SelectedValue);
            divstatus.Visible = false;        
            ObjEnt.FROM_DATE = Convert.ToDateTime(txttodate.Text);
            ObjEnt.TO_DATE = Convert.ToDateTime(txtFromDate.Text);
            pnlBudgetApprove.Visible = true;
            btnsubmit.Visible = true;
            btnRevied.Visible = true;
            btnProposed.Visible = true;
            Showbudget();
        }
    }

    protected void ddlbudgetparent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjEnt.FROM_DATE = Convert.ToDateTime(txtFromDate.Text);
        ObjEnt.TO_DATE = Convert.ToDateTime(txttodate.Text);
        ObjEnt.Parent_id = Convert.ToInt32(ddlbudgetparent.SelectedValue);
        Showbudget();
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //chckdept();
        txtstatus.Visible = false;
        if (ddldept.SelectedIndex == 0)
        {
            clearall();
            return;

        }
        //#region
        //DataSet ds = objCommon.FillDropDown("ACC_BUDGET_ALLOCATION_NEW", "FROM_DATE", "TO_DATE," + "IS_APPROVE", " DEPT_ID =" + Convert.ToInt32(ddldept.SelectedValue), "");
        //if (ds.Tables.Count > 0)
        //{
        //    Showbudget();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
        //        //txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
        //        string status = ds.Tables[0].Rows[0]["IS_APPROVE"].ToString();
        //        if (status.Trim() == "1")
        //        {
        //            txtstatus.Visible = true;
        //            txtstatus.Text = "Budget is approved for this department";

        //        }
        //        else
        //        {
        //            txtstatus.Text = "";
        //        }
        //        //Showbudget();
        //    }
        //    else
        //    {

        //        //Showbudget();
        //        txtFromDate.Text = string.Empty;
        //        txttodate.Text = string.Empty;
        //        //pnlBudgetApprove.Visible = false;
        //        //btnsubmit.Visible = false;
        //    }
        //    ddlbudgetparent.SelectedValue = "0";
        //    ds = null;

        //}
        //#endregion
    }

    protected void txttodate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void chkRevised_CheckedChanged(object sender, EventArgs e)
    {
        if (Rptbudgethead.Items.Count > 0)
        {
            if (chkRevised.Checked == true && chkProposed.Checked == true)
            {
                foreach (ListViewDataItem item in Rptbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label lblRevStatus = item.FindControl("lblRevStatus") as Label;
                    Label lblPropStatus = item.FindControl("lblPropStatus") as Label;
                    
                        
                    txtProposed.Enabled = true;
                    txtProposed.Visible = true;
                    txtRevised.Enabled = true;
                    txtRevised.Visible = true;
                    lblRevStatus.Visible = true;
                    lblPropStatus.Visible = true;
                }
            }
            else if (chkRevised.Checked == true)
            {
                foreach (ListViewDataItem item in Rptbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label lblRevStatus = item.FindControl("lblRevStatus") as Label;
                    lblRevStatus.Visible = true;
                    txtRevised.Enabled = true;
                    txtProposed.Enabled = false;
                    txtRevised.Visible = true;

                }
            }
            else if (chkProposed.Checked == true)
            {
                foreach (ListViewDataItem item in Rptbudgethead.Items)
                {
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    Label lblPropStatus = item.FindControl("lblPropStatus") as Label;
                    lblPropStatus.Visible = true;
                    txtRevised.Enabled = false;
                    txtProposed.Visible = true;
                    txtProposed.Enabled = true;
                }
            }

            else if (chkRevised.Checked == false && chkProposed.Checked == false)
            {
                foreach (ListViewDataItem item in Rptbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    txtRevised.Visible = false;
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label lblRevStatus = item.FindControl("lblRevStatus") as Label;
                    Label lblPropStatus = item.FindControl("lblPropStatus") as Label;
                    lblRevStatus.Visible = false;
                    lblPropStatus.Visible = false;
                    txtProposed.Visible = false;
                }
            }
            else if (chkRevised.Checked == false)
            {
                foreach (ListViewDataItem item in Rptbudgethead.Items)
                {
                    TextBox txtRevised = item.FindControl("txtRevised") as TextBox;
                    Label lblRevStatus = item.FindControl("lblRevStatus") as Label;
                    txtRevised.Visible = false;
                    lblRevStatus.Visible = false;
                }
            }
            //if (chkProposed.Checked == false)
            else
            {
                foreach (ListViewDataItem item in Rptbudgethead.Items)
                {
                    TextBox txtProposed = item.FindControl("txtProposed") as TextBox;
                    Label lblPropStatus = item.FindControl("lblPropStatus") as Label;
                    txtProposed.Visible = false;
                    lblPropStatus.Visible = false;
                }
            }
        }
        else
        {
            chkRevised.Checked = false;
            chkProposed.Checked = false;
        }


    }
    #endregion



    protected void btnRevied_Click(object sender, EventArgs e)
    {
        if (chkRevised.Checked == true)
        {
            AddRevisedTable();
            ChechDept_id();
            DataTable dt = null;
            try
            {
                ObjEnt.BUDGETALLOCTION_ID = Convert.ToInt32(Session["Budget_id"].ToString());
                ObjEnt.COLLEGE_ID = Convert.ToInt32(Session["colcode"].ToString());
                ObjEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
                ObjEnt.TO_DATE = Convert.ToDateTime(Session["to_date"].ToString());
                ObjEnt.FROM_DATE = Convert.ToDateTime(Session["f_date"].ToString());
                //   ObjEnt.DEPT_AMOUNT = Convert.ToDouble(txtamount.Text);
                ObjEnt.Dept_id = Convert.ToInt32(ddldept.SelectedValue);
                //DataTable dt = null;
                dt = (DataTable)Session["BUDTBL"];

                if (dt == null)
                {
                    //Response.Write("<script>alert('Sorry This Department is not Applied..')</script>");
                    objCommon.DisplayUserMessage(UPBudgetApprove, "Please select Atleast one checkbox to Approve Revised Amount.", this.Page);
                    return;
                }
                else
                {
                    ObjEnt.BudgetApproveTable = dt;
                }


                if (ViewState["action"].ToString() == "add")
                {

                    CustomStatus cs = (CustomStatus)ObjCon.ApproveBudgetAmount(ObjEnt);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayUserMessage(UPBudgetApprove, "Revised Amount Approved Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPBudgetApprove, "Revised Amount Approved Successfully.", this.Page);
                    }

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
        else {
            objCommon.DisplayUserMessage(UPBudgetApprove, "If you want to Approved Revised Amount? Revised Budget Checked First.!", this.Page);
        
        }


    }
    protected void btnProposed_Click(object sender, EventArgs e)
    {
       if(chkProposed.Checked == true){
        AddProposedTable();
        ChechDept_id();
        DataTable dt = null;
        try
        {
            ObjEnt.BUDGETALLOCTION_ID = Convert.ToInt32(Session["Budget_id"].ToString());
            ObjEnt.COLLEGE_ID = Convert.ToInt32(Session["colcode"].ToString());
            ObjEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
            ObjEnt.TO_DATE = Convert.ToDateTime(Session["to_date"].ToString());
            ObjEnt.FROM_DATE = Convert.ToDateTime(Session["f_date"].ToString());
            //   ObjEnt.DEPT_AMOUNT = Convert.ToDouble(txtamount.Text);
            ObjEnt.Dept_id = Convert.ToInt32(ddldept.SelectedValue);
            //DataTable dt = null;
            dt = (DataTable)Session["BUDTBL"];

            if (dt == null)
            {
                //Response.Write("<script>alert('Sorry This Department is not Applied..')</script>");
                objCommon.DisplayUserMessage(UPBudgetApprove, "Please select Atleast one checkbox to Approve Proposed Amount.", this.Page);
                return;
            }
            else
            {
                ObjEnt.BudgetApproveTable = dt;
            }


            if (ViewState["action"].ToString() == "add")
            {

                CustomStatus cs = (CustomStatus)ObjCon.ApproveBudgetAmount(ObjEnt);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                   objCommon.DisplayUserMessage(UPBudgetApprove,"Proposed Amount Approved Successfully.", this.Page);
                }
                else
                {
                   objCommon.DisplayUserMessage(UPBudgetApprove,"Proposed Amount Approved Successfully.", this.Page);
                }

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
        else {
            objCommon.DisplayUserMessage(UPBudgetApprove, "If you want to Approved Proposed Amount? Proposed Budget Checked First.!", this.Page);
        
        }

    }
}