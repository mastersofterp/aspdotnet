//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 19-MAY-2010                                               
// CREATED BY    : ASHISH THAKRE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using IITMS.NITPRM;

public partial class ACCOUNT_SupplimentaryDeleteTransfer : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    AccountTransactionController objAccount = new AccountTransactionController();
    string CCMS = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString.ToString();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["comp_code"] == null)
        {
            Response.Redirect("~/Account/selectCompany.aspx");
        }
        // To Set the MasterPage
        else if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {




        //btnSave.Enabled = false;


        if (!Page.IsPostBack)
        {

            btnDelete.Enabled = false;
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
                    //Load Page Help
                    txtDate.Text = DateTime.Now.Date.ToString();
                    // Filling First ddl
                    PopulateSTAFDropdown();

                    //FILL Month DDL
                    PopulateMonthDDL();
                    GetBankNo_Name();
                    GetCollege();
                    SetFinancialYear();

                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void SetFinancialYear()
    {
        FinCashBookController objCBC = new FinCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtTodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PayRollLedgerTransfer.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayRollLedgerTransfer.aspx");
        }
    }

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblBank.Text == "BANK NOT FOUND")
            {
                objCommon.DisplayMessage(UPDLedger, "Sorry...BANK NOT AVAILABLE", this);
                return;
            }
            if (DateTime.Compare(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtFromDate.Text)) > 0)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date And Upto Date Is Not Valid ", this);
                txtFromDate.Focus();
                return;
            }
            GridData.DataSource = null;
            GridData.DataBind();

            DataTable dt1 = new DataTable();
            dt1 = GetPayHeadNameAndNoo();

            if (dt1.Rows.Count > 0)
            {
                GridData.DataSource = dt1;
                GridData.DataBind();
                if (GridData.Rows.Count > 0)
                {
                    // panOne.Visible = true;
                    btnDelete.Enabled = true;
                    trDetail.Visible = true;
                    Tr8.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "No Data Found", this);
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_SupplimentryTransfer.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    public DataTable GetPayHeadNameAndNoo()
    {

        DataTable dtpAYsheads = new DataTable();
        try
        {
            double cashAmt = 0, FinAmt = 0;
            AccountTransactionController ATC = new AccountTransactionController();
            string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            int bnkNo = Convert.ToInt32(hidfBankNo.Value.ToString().Trim());
            DataSet dsPayHead = ATC.GetSuplitransferDataDelete(Session["comp_code"].ToString(), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), ddlMon.SelectedItem.Text, bnkNo, Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy"), Convert.ToInt32(rdosuppli.SelectedValue));

            dtpAYsheads = dsPayHead.Tables[0];

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

    /// <summary>
    /// Get pay PAYNAME And  no
    /// </summary>
    /// <param name="rpt_Type"></param>
    /// <returns></returns>
    //public DataTable GetPayHeadNameAndNoo()
    //{
    //    DataTable dtpAYsheads = new DataTable();
    //    //dtFeesheads =null;
    //    try
    //    {
    //        //AND FEE_TITLE !='" + temp + "'
    //        string temp = " ";
    //        string SelectStr = "SELECT PAYHEAD,FULLNAME FROM PAYHEAD ORDER BY SRNO ";

    //        OracleDataAdapter DApayhead = new OracleDataAdapter(SelectStr, ocon);
    //        DApayhead.Fill(dtpAYsheads);



    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    return dtpAYsheads;


    //}

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            long res = 0;

            Button BtnReturn = sender as Button;
            RepeaterItem item = (RepeaterItem)(sender as Control).NamingContainer;

            TextBox txtReturnDate = (TextBox)item.FindControl("txtReturnDate");
            TextBox txtRturnRemark = (TextBox)item.FindControl("txtRturnRemark");
            Label lblCirculationIssueReturnId = (Label)item.FindControl("lblIssueReturnId");
            Label lblIssetype = (Label)item.FindControl("lblIssetype");
            Label Label15 = (Label)item.FindControl("Label15");

         


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_PayRollLedgerHeadMapping.PopulateSTAFDropdown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave0_Click(object sender, EventArgs e)
    {



    }

    protected void DeleteRecord(string SupId)
    {
        try
        {
            AccountTransactionController objAccTran = new AccountTransactionController();

            string compCode = Session["comp_code"].ToString();
           
            int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
            
            int i = 0;

            i = objAccTran.DeleteSuppliTransfer(compCode, SupId, ddlDegree.SelectedValue.ToString());

            if (i == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Deleted Successfully", this);
                ClearAll();
            }
            else if (i == 0)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Already Transfered ! ", this);
                ClearAll();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_PayRollLedgerHeadMapping.PopulateSTAFDropdown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void SaveRecordByEmployee()
    {


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry...Data Not Present. Click on Show Data", this);
            return;

        }

        if (GridData.Rows.Count <= 0)
        {
            objCommon.DisplayMessage(UPDLedger, "No Data Found!", this);
            return;
        }

        int counts = 0;
      //  StringBuilder StrId = new StringBuilder();
        string StrId = string.Empty;
        foreach (GridViewRow row in GridData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;

                //if (Convert.ToInt32(rdosuppli.SelectedValue) == 2)
                //{
                //    if (chkSelect.Checked == true)
                //    {
                //        if (counts == 0)
                //        {
                //            StrId.Append(chkSelect.ToolTip +"G");
                //        }
                //        else
                //        {
                //            StrId.Append("," + chkSelect.ToolTip + "G");
                //        }

                //        counts = counts + 1;
                //    }
                //}
                //else
                {
                    if (chkSelect.Checked == true)
                    {
                        StrId = chkSelect.ToolTip;
                        DeleteRecord(StrId.ToString());
                        //if (counts == 0)
                        //{
                        //    StrId.Append(chkSelect.ToolTip);
                        //}
                        //else
                        //{
                        //    StrId.Append("," + chkSelect.ToolTip);
                        //}

                        //counts = counts + 1;
                    }
                }
            }
        }
        if (counts <= 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please select one record from list", this);
            return;

        }

       // DeleteRecord(StrId.ToString());



    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
        //panOne.Visible = false;
    }
    private void ClearAll()
    {
        GridData.DataSource = null;
        GridData.DataBind();
        btnDelete.Enabled = false;
        lblTamt.Text = string.Empty;

        trDetail.Visible = false;
        Tr8.Visible = false;
        gvEmpShare.DataSource = null;
        gvEmpShare.DataBind();
    }

    public void PopulateSTAFDropdown()
    {
        try
        {
            string temp = " ";
            ////string OraSelectStr = "select * from ACD_staff WHERE STAFFNO <> 0";
            ////OracleDataAdapter ODAstaff = new OracleDataAdapter(OraSelectStr, ocon);
            ////DataTable DTDstaff = new DataTable();
            ////ODAstaff.Fill(DTDstaff);

            AccountTransactionController ATC = new AccountTransactionController();
            DataSet DTDstaff = ATC.PopulateEmpPayroll(CCMS);
            ddlDegree.DataTextField = "STAFF";
            ddlDegree.DataValueField = "STAFFNO";
            ddlDegree.DataSource = DTDstaff;
            ddlDegree.DataBind();

            ////if (DTDstaff.Rows.Count > 0)
            ////{
            ////    ddlDegree.DataTextField = "STAFF";
            ////    ddlDegree.DataValueField = "STAFFNO";
            ////    ddlDegree.DataSource = DTDstaff;
            ////    ddlDegree.DataBind();
            ////}
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
        ////string strOracle = "select distinct MONYEAR FROM SALFILE";
        ////DataTable dtMon = new DataTable();
        ////OracleDataAdapter Oda = new OracleDataAdapter(strOracle, ocon);
        ////Oda.Fill(dtMon);
        ////ddlMon.DataSource = dtMon;
        ////ddlMon.DataTextField = "MONYEAR";
        ////ddlMon.DataBind();
        //DataSet dsLH = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NAME", "PARTY_NO", "PAYMENT_TYPE_NO =2", "");
        //ddlPARTY.DataSource = dsLH.Tables[0];
        //ddlPARTY.DataTextField = "PARTY_NAME";
        //ddlPARTY.DataValueField = "PARTY_NO";
        //ddlPARTY.DataBind();

        AccountTransactionController ATC = new AccountTransactionController();
        string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        DataSet dsStaff = ATC.PopulateEmpPayroll(_con);
        DataSet dsLH = ATC.GetMonth(_con);
        ddlMon.DataSource = dsLH.Tables[0];
        ddlMon.DataTextField = "MONYEAR";
        ddlMon.DataValueField = "";
        ddlMon.DataBind();




    }


    //public void PopulatePartyDDL()
    //{
    //    DataSet dsLH = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NAME", "PARTY_NO", "PAYMENT_TYPE_NO =2", "");
    //    ddlPARTY.DataSource = dsLH.Tables[0];
    //    ddlPARTY.DataTextField = "PARTY_NAME";
    //    ddlPARTY.DataValueField = "PARTY_NO";
    //    ddlPARTY.DataBind();

    //}

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


        //select distinct  M.BANK_NO ,P.PARTY_NAME   from dbo.ACC_KML_PAYROLL_ACCOUNT_MAPPING M,dbo.ACC_kml_PARTY P where M.STAFF_NO=1 AND M.BANK_NO=P.PARTY_NO
    }

    //public void PopulateReceptTypeDropdown()
    //{
    //    ocon.Open();
    //    try
    //    {
    //        string OraSelectStr = "SELECT RECIEPT_CODE,RECIEPT_TITLE FROM RECIEPT_TYPE";
    //        OracleDataAdapter ODArcpt = new OracleDataAdapter(OraSelectStr, ocon);
    //        DataTable DTrcpt = new DataTable();
    //        ODArcpt.Fill(DTrcpt);

    //        if (DTrcpt.Rows.Count > 0)
    //        {
    //            ddlRecept.DataTextField = "RECIEPT_TITLE";
    //            ddlRecept.DataValueField = "RECIEPT_CODE";
    //            ddlRecept.DataSource = DTrcpt;
    //            ddlRecept.DataBind();
    //        }





    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.PopulateRecept-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }



    //}

    //inserting if new legerhead Found 
    public void AddSinglerecord(string newLedgerhead)
    {
        //FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
        //AccountTransactionController objAccTran = new AccountTransactionController();



        // if (GridData.Rows.Count > 0)
        //{
        //    int Icount = 0;
        //    for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
        //    {


        //        FLHMobj.COLLEGE = Convert.ToInt32(ddlDegree.SelectedValue.ToString());

        //        FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();

        //        Label lblHNO = GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
        //        FLHMobj.FEE_HEAD_NO = lblHNO.Text;

        //        DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
        //        FLHMobj.LEDGER_NO = Convert.ToInt32(ddlFH.SelectedValue.ToString());
        //        FLHMobj.CName = Session["username"].ToString();
        //        FLHMobj.CPass = Session["username"].ToString();


        //        //Label lbl =GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
        //        //string temp = lbl.Text;

        //        DropDownList ddlcas = GridData.Rows[Icount].FindControl("ddllCash") as DropDownList;
        //        FLHMobj.CASH_NO = Convert.ToInt32(ddlcas.SelectedValue.ToString());


        //        DropDownList ddlBank = GridData.Rows[Icount].FindControl("ddlBank") as DropDownList;
        //        FLHMobj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue.ToString());
        //        FLHMobj.LASTMODIFIER_DATE = DateTime.Now;
        //        FLHMobj.CREATE_DATE = DateTime.Now;
        //        FLHMobj.CREATER_NAME = Session["username"].ToString();
        //        FLHMobj.LASTMODIFIER = Session["username"].ToString();

        //        if (FLHMobj.FEE_HEAD_NO == newLedgerhead)
        //        {
        //            int iResult = objAccTran.AddFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim());
        //            if (iResult == 1)
        //            {
        //                objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);

        //            }
        //        }
        //    }



        //     // i = objAccTran.UpdateFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim());
        //        //if (i == 1)
        //        //{
        //        //    objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);

        //        //}     




        //    }


    }





    protected void GridData_RowCreated(object sender, GridViewRowEventArgs e)
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

    //public DataTable FiiEmployeeGrid()
    //{
    //    DataTable dtEmp = new DataTable();
    //    //dtFeesheads =null;
    //    try
    //    {
    //        //AND FEE_TITLE !='" + temp + "'
    //        string temp = " ";
    //        string SelectStr = "SELECT NAME,SUBDESIG,net_pay From " + ddlMon.SelectedItem.ToString().Trim() + " where STAFFNO=" + ddlDegree.SelectedValue.ToString().Trim() + "ORDER BY NAME";

    //        OracleDataAdapter DAemp = new OracleDataAdapter(SelectStr, ocon);
    //        DAemp.Fill(dtEmp);



    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    return dtEmp;

    //}
    public void CalCulateEmpAmount()
    {
    }
    protected void btnRecalculate_Click(object sender, EventArgs e)
    {
        CalCulateEmpAmount();

    }
    protected void btnRecalculate_Click1(object sender, EventArgs e)
    {
        CalCulateEmpAmount();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
    protected void btnEmpSalTrans_Click(object sender, EventArgs e)
    {
        SaveRecordByEmployee();
    }
}