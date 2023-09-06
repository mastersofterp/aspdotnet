//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 19-MAY-2010                                               
// CREATED BY    : ASHISH THAKRE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
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
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Data.OracleClient;
using IITMS.NITPRM;

public partial class PayRollDeleteTransfer : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    OracleConnection ocon = new OracleConnection("Data Source=VNITFEES;UID=WCEPAY;PWD=WCEPAY");
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);
    AccountTransactionController objAccount = new AccountTransactionController();
    string CCMS = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString.ToString();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {




        //btnSave.Enabled = false;


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

                    //Fill dropdown list


                    // Filling First ddl
                    PopulateSTAFDropdown(); 

                    //FILL Month DDL
                    PopulateMonthDDL();
                    GetBankNo_Name();
                    GetCollege();
                    //Added by vijay andoju for department wise filter on 16092020
                    PopulateDepartment();


                    
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void PopulateDepartment()
    {
        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPT<>''", "SUBDEPT");

    }
    private void GetCollege()
    {
        DataSet ds = objAccount.GetCollegeForPayHeadmapping(CCMS);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCollege.Items.Clear();
            ddlCollege.Items.Insert(0, "Please Select");
            ddlCollege.DataTextField = "COLLEGE_NAME";
            ddlCollege.DataValueField = "COLLEGE_ID";
            ddlCollege.DataSource = ds.Tables[0]; ;
            ddlCollege.DataBind();
        }
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


    protected void btnShowData_Click(object sender, EventArgs e)
    {

     
        int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
        DataSet ds = objSQLHelper.ExecuteDataSet("select convert(nvarchar(50),a.TRANSACTION_DATE,105) TRANSACTION_DATE,b.PARTY_NAME,VOUCHER_NO,case when [TRAN]='Dr' then cast(AMOUNT as nvarchar(50)) else '' end Debit,case when [TRAN]='Cr' then cast(AMOUNT as nvarchar(50)) else '' end Credit,a.VOUCHER_SQN,a.PAGENO,CBTYPE from ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS a inner join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY b on (a.PARTY_NO=b.PARTY_NO) where TRANSACTION_TYPE<>'OB' and transfer_entry=2 and a.PAGENO='" + ddlMon.SelectedItem.ToString() + "' and a.CBTYPE='" + ddlDegree.SelectedItem.Text + "' and a.COLLEGE_CODE='" + CollegeId + "' order by TRANSACTION_DATE desc,transaction_no desc");
        GridData.DataSource = ds;
        GridData.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
            btnDelete.Enabled = true;
            
        else
            objCommon.DisplayMessage(UPDLedger, "Data not available", this.Page);

    }


    /// <summary>
    /// Get pay PAYNAME And  no
    /// </summary>
    /// <param name="rpt_Type"></param>
    /// <returns></returns>
    public DataTable GetPayHeadNameAndNoo()
    {
        DataTable dtpAYsheads = new DataTable();
        //dtFeesheads =null;
        try
        {
            //AND FEE_TITLE !='" + temp + "'
            string temp = " ";
            string SelectStr = "SELECT PAYHEAD,FULLNAME FROM PAYHEAD ORDER BY SRNO ";

            OracleDataAdapter DApayhead = new OracleDataAdapter(SelectStr, ocon);
            DApayhead.Fill(dtpAYsheads);



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return dtpAYsheads;


    }
    protected void btnSave0_Click(object sender, EventArgs e)
    {
    }

    protected void SaveRecordByEmployee()
    {


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {



    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();

    }
    private void ClearAll()
    {

    }
    public void PopulateSTAFDropdown()
    {
        try
        {
            string temp = " ";
            AccountTransactionController ATC = new AccountTransactionController();
            string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            DataSet DTDstaff = ATC.PopulateEmpPayroll(_con);
            ddlDegree.DataTextField = "STAFF";
            ddlDegree.DataValueField = "STAFFNO";
            ddlDegree.DataSource = DTDstaff;
            ddlDegree.DataBind();

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_PayRollLedgerHeadMapping.PopulateSTAFDropdown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    public void PopulateMonthDDL()
    {
        //AccountTransactionController ATC = new AccountTransactionController();
        //string _con = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
        //DataSet dsStaff = ATC.PopulateEmpPayroll(_con);
        //DataSet dsLH = ATC.GetMonth(_con);
        //ddlMon.DataSource = dsLH.Tables[0];
        //ddlMon.DataTextField = "MONYEAR";
        //ddlMon.DataValueField = "";
        //ddlMon.DataBind();
        objCommon.FillDropDownList(ddlMon, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");
    }
    public void GetBankNo_Name()
    {
        string sqlSelect = "select distinct  M.BANK_NO ,P.PARTY_NAME   from dbo.ACC_" + Session["comp_code"].ToString() + "_PAYROLL_ACCOUNT_MAPPING M,dbo.ACC_" + Session["comp_code"].ToString() + "_PARTY P where M.STAFF_NO='" + ddlDegree.SelectedValue.ToString() + "' AND M.BANK_NO=P.PARTY_NO";
        SqlConnection sqlcon = new SqlConnection(_nitprm_constr);
        SqlDataAdapter da = new SqlDataAdapter(sqlSelect, sqlcon);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count == 0)
        {
            lblBank.Text = "BANK NOT FOUND";
        }
        else
        {

            lblBank.Text = dt.Rows[0][1].ToString().Trim();
            hidfBankNo.Value = dt.Rows[0][0].ToString().Trim();
        }
    }
    //inserting if new legerhead Found 
    public void AddSinglerecord(string newLedgerhead)
    {
    }
    protected void ddlRecept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        GetBankNo_Name();
    }
    protected void ddlPARTY_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    public bool checkForEntry()
    {
        bool result = false;
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYROLL_ACCOUNT_MAPPING", "*", "", "BANK_NO='" + ddlMon.SelectedValue.ToString() + "' AND STAFF_NO='" + ddlDegree.SelectedValue.ToString() + "'", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;

    }
    protected void BtnShowEmp_Click(object sender, EventArgs e)
    {


    }

    //New Update-------------------------------------------------------------------------

    public DataTable FiiEmployeeGrid()
    {
        DataTable dtEmp = new DataTable();
        //dtFeesheads =null;
        try
        {
            //AND FEE_TITLE !='" + temp + "'
            string temp = " ";
            string SelectStr = "SELECT NAME,SUBDESIG,net_pay From " + ddlMon.SelectedItem.ToString().Trim() + " where STAFFNO=" + ddlDegree.SelectedValue.ToString().Trim() + "ORDER BY NAME";

            OracleDataAdapter DAemp = new OracleDataAdapter(SelectStr, ocon);
            DAemp.Fill(dtEmp);



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return dtEmp;

    }

    protected void btnRecalculate_Click(object sender, EventArgs e)
    {


    }
    protected void btnRecalculate_Click1(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
    protected void btnEmpSalTrans_Click(object sender, EventArgs e)
    {
        SaveRecordByEmployee();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        AccountTransactionController objAccTran = new AccountTransactionController();
        string MonYear = Convert.ToDateTime(ddlMon.SelectedValue).ToString("yyyy-MM-dd");  //Added by gopal anthati on 04/01/2020     
        //int i = objAccTran.DeletePayRollTransfer(ddlMon.SelectedValue.ToString(), ddlDegree.SelectedItem.ToString(), Session["comp_code"].ToString().Trim(),ddldept.SelectedValue);
        int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
        int i = objAccTran.DeletePayRollTransfer(MonYear, ddlDegree.SelectedItem.ToString(), Session["comp_code"].ToString().Trim(), ddldept.SelectedValue, CollegeId);
        //SQLHelper SQLH = new SQLHelper(_nitprm_constr);
        //int i = SQLH.ExecuteNonQuery("DELETE from ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS where PAGENO='" + ddlMon.SelectedValue.ToString() + "' AND CBTYPE='" + ddlDegree.SelectedItem.ToString() + "' AND TRANSFER_ENTRY=2");
        if (i > 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Deleted Successfully", this);
            GridData.DataSource = null;
            GridData.DataBind();
            btnDelete.Enabled = false;
            //lblCashTotal.Text = String.Format("{0:0.00}", Math.Abs(0.00));
        }
        else if (i == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Try Again! ", this);

        }
    }
}
