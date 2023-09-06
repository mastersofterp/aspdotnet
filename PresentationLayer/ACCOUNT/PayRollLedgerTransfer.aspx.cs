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
using IITMS.NITPRM;


public partial class PayRollLedgerTransfer : System.Web.UI.Page
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

            btnSave.Enabled = false;
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

        //if (lblBank.Text == "BANK NOT FOUND")
        //{
        //    objCommon.DisplayMessage(UPDLedger, "Sorry...BANK NOT AVAILABLE", this);
        //    return;
        //}
       // int bnkNo = Convert.ToInt32(hidfBankNo.Value.ToString().Trim());

     //   string sqlStr = "SELECT M.PAY_HEAD_NO,M.PAY_HEAD_NAME,P.PARTY_NAME FROM dbo.ACC_" + Session["comp_code"].ToString() + "_PAYROLL_ACCOUNT_MAPPING M,dbo.ACC_" + Session["comp_code"].ToString() + "_PARTY P WHERE M.COLLEGE_ID=" + ddlCollege.SelectedValue.ToString().Trim() + " and M.STAFF_NO='" + ddlDegree.SelectedValue.ToString() + "' AND M.SUBDEPT_NO=" + ddldept.SelectedValue + " AND M.LEDGER_NO=P.PARTY_NO ORDER BY M.INDEX_NO ";
        string sqlStr = "SELECT M.PAY_HEAD_NO,M.PAY_HEAD_NAME,P.PARTY_NAME FROM dbo.ACC_" + Session["comp_code"].ToString() + "_PAYROLL_ACCOUNT_MAPPING M,dbo.ACC_" + Session["comp_code"].ToString() + "_PARTY P WHERE M.COLLEGE_ID=" + ddlCollege.SelectedValue.ToString().Trim() + " and M.STAFF_NO='" + ddlDegree.SelectedValue.ToString() + "'  AND M.LEDGER_NO=P.PARTY_NO ORDER BY M.INDEX_NO ";
        SqlConnection sqlcon = new SqlConnection(_nitprm_constr);
        SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlcon);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridData.DataSource = dt;
        GridData.DataBind();
        if (GridData.Rows.Count > 0)
        {
            // panOne.Visible = true;
            btnSave.Enabled = true;
            trDetail.Visible = true;
            Tr8.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "No Data Found", this);
            return;
        }

        AccountTransactionController ATC = new AccountTransactionController();
        string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        //string strSum = "Select ROUND(sum(I1),0) as I1,ROUND(sum(I2),0) as I2,ROUND(sum(I3),0) as I3,ROUND(sum(I4),0) as I4,ROUND(sum(I5),0) as I5,ROUND(sum(I6),0) as I6,ROUND(sum(I7),0) as I7,ROUND(sum(I8),0) as I8,ROUND(sum(I9),0) as I9,ROUND(sum(I10),0) as I10,ROUND(sum(I11),0) as I11,ROUND(sum(I12),0) as I12,ROUND(sum(I13),0) as I13,ROUND(sum(I14),0) as I14,ROUND(sum(I15),0) as I15,ROUND(sum(D1),0) as D1,ROUND(sum(D2),0) as D2,ROUND(sum(D3),0) as D3,ROUND(sum(D4),0) as D4,ROUND(sum(D5),0) as D5,ROUND(sum(D6),0) as D6,ROUND(sum(D7),0) as D7,ROUND(sum(D8),0) as D8,ROUND(sum(D9),0) as D9,ROUND(sum(D10),0) as D10,ROUND(sum(D11),0) as D11,ROUND(sum(D12),0) as D12,ROUND(sum(D13),0) as D13,ROUND(sum(D14),0) as D14,ROUND(sum(D15),0) as D15,ROUND(sum(D16),0) as D16,ROUND(sum(D17),0) as D17,ROUND(sum(D18),0) as D18,ROUND(sum(D19),0) as D19,ROUND(sum(D21),0) as D20,ROUND(sum(D21),0) as D21,ROUND(sum(D22),0) as D22,ROUND(sum(D23),0) as D23,ROUND(sum(D24),0) as D24,ROUND(sum(D25),0) as D25,ROUND(sum(D26),0) as D26,ROUND(sum(D27),0) as D27,ROUND(sum(D28),0) as D28,ROUND(sum(D29),0) as D29,ROUND(sum(D30),0) as D30,ROUND(sum(NET_PAY),0) as NET_PAY,ROUND(sum(PAY),0) as PAY From " + ddlMon.SelectedItem.ToString().Trim() + " where STAFFNO=" + ddlDegree.SelectedValue.ToString().Trim() + " and COLLEGE_NO=" + ddlCollege.SelectedValue.ToString().Trim() + "";
        //string strSum = "Select ISNULL(ROUND(sum(I1),0),0) as I1, ISNULL(ROUND(sum(I2),0),0) as I2, ISNULL(ROUND(sum(I3),0),0) as I3,ISNULL(ROUND(sum(I4),0),0) as I4,ISNULL(ROUND(sum(I5),0),0) as I5,ISNULL(ROUND(sum(I6),0),0) as I6,ISNULL(ROUND(sum(I7),0),0) as I7,ISNULL(ROUND(sum(I8),0),0) as I8,ISNULL(ROUND(sum(I9),0),0) as I9,ISNULL(ROUND(sum(I10),0),0) as I10,ISNULL(ROUND(sum(I11),0),0) as I11,ISNULL(ROUND(sum(I12),0),0) as I12,ISNULL(ROUND(sum(I13),0),0) as I13,ISNULL(ROUND(sum(I14),0),0) as I14,ISNULL(ROUND(sum(I15),0),0) as I15,ISNULL(ROUND(sum(D1),0),0) as D1,ISNULL(ROUND(sum(D2),0),0) as D2,ISNULL(ROUND(sum(D3),0),0) as D3,ISNULL(ROUND(sum(D4),0),0) as D4,ISNULL(ROUND(sum(D5),0),0) as D5,ISNULL(ROUND(sum(D6),0),0) as D6,ISNULL(ROUND(sum(D7),0),0) as D7,ISNULL(ROUND(sum(D8),0),0) as D8,ISNULL(ROUND(sum(D9),0),0) as D9,ISNULL(ROUND(sum(D10),0),0) as D10,ISNULL(ROUND(sum(D11),0),0) as D11,ISNULL(ROUND(sum(D12),0),0) as D12,ISNULL(ROUND(sum(D13),0),0) as D13,ISNULL(ROUND(sum(D14),0),0) as D14,ISNULL(ROUND(sum(D15),0),0) as D15,ISNULL(ROUND(sum(D16),0),0) as D16,ISNULL(ROUND(sum(D17),0),0) as D17,ISNULL(ROUND(sum(D18),0),0) as D18,ISNULL(ROUND(sum(D19),0),0) as D19,ISNULL(ROUND(sum(D21),0),0) as D20,ISNULL(ROUND(sum(D21),0),0) as D21,ISNULL(ROUND(sum(D22),0),0) as D22,ISNULL(ROUND(sum(D23),0),0) as D23,ISNULL(ROUND(sum(D24),0),0) as D24,ISNULL(ROUND(sum(D25),0),0) as D25,ISNULL(ROUND(sum(D26),0),0) as D26,ISNULL(ROUND(sum(D27),0),0) as D27,ISNULL(ROUND(sum(D28),0),0) as D28,ISNULL(ROUND(sum(D29),0),0) as D29,ISNULL(ROUND(sum(D30),0),0) as D30, ISNULL(ROUND(sum(NET_PAY),0),0) as NET_PAY, ISNULL(ROUND(sum(PAY),0),0) as PAY  From " + ddlMon.SelectedItem.ToString().Trim() + " where STAFFNO=" + ddlDegree.SelectedValue.ToString().Trim() + " and COLLEGE_NO=" + ddlCollege.SelectedValue.ToString().Trim() + " AND SUBDEPTNO="+ddldept.SelectedValue;

        string strSum = "Select ISNULL(ROUND(sum(I1),0),0) as I1, ISNULL(ROUND(sum(I2),0),0) as I2, ISNULL(ROUND(sum(I3),0),0) as I3,ISNULL(ROUND(sum(I4),0),0) as I4,ISNULL(ROUND(sum(I5),0),0) as I5,ISNULL(ROUND(sum(I6),0),0) as I6,ISNULL(ROUND(sum(I7),0),0) as I7,ISNULL(ROUND(sum(I8),0),0) as I8,ISNULL(ROUND(sum(I9),0),0) as I9,ISNULL(ROUND(sum(I10),0),0) as I10,ISNULL(ROUND(sum(I11),0),0) as I11,ISNULL(ROUND(sum(I12),0),0) as I12,ISNULL(ROUND(sum(I13),0),0) as I13,ISNULL(ROUND(sum(I14),0),0) as I14,ISNULL(ROUND(sum(I15),0),0) as I15,ISNULL(ROUND(sum(D1),0),0) as D1,ISNULL(ROUND(sum(D2),0),0) as D2,ISNULL(ROUND(sum(D3),0),0) as D3,ISNULL(ROUND(sum(D4),0),0) as D4,ISNULL(ROUND(sum(D5),0),0) as D5,ISNULL(ROUND(sum(D6),0),0) as D6,ISNULL(ROUND(sum(D7),0),0) as D7,ISNULL(ROUND(sum(D8),0),0) as D8,ISNULL(ROUND(sum(D9),0),0) as D9,ISNULL(ROUND(sum(D10),0),0) as D10,ISNULL(ROUND(sum(D11),0),0) as D11,ISNULL(ROUND(sum(D12),0),0) as D12,ISNULL(ROUND(sum(D13),0),0) as D13,ISNULL(ROUND(sum(D14),0),0) as D14,ISNULL(ROUND(sum(D15),0),0) as D15,ISNULL(ROUND(sum(D16),0),0) as D16,ISNULL(ROUND(sum(D17),0),0) as D17,ISNULL(ROUND(sum(D18),0),0) as D18,ISNULL(ROUND(sum(D19),0),0) as D19,ISNULL(ROUND(sum(D21),0),0) as D20,ISNULL(ROUND(sum(D21),0),0) as D21,ISNULL(ROUND(sum(D22),0),0) as D22,ISNULL(ROUND(sum(D23),0),0) as D23,ISNULL(ROUND(sum(D24),0),0) as D24,ISNULL(ROUND(sum(D25),0),0) as D25,ISNULL(ROUND(sum(D26),0),0) as D26,ISNULL(ROUND(sum(D27),0),0) as D27,ISNULL(ROUND(sum(D28),0),0) as D28,ISNULL(ROUND(sum(D29),0),0) as D29,ISNULL(ROUND(sum(D30),0),0) as D30, ISNULL(ROUND(sum(NET_PAY),0),0) as NET_PAY, ISNULL(ROUND(sum(PAY),0),0) as PAY  From " + ddlMon.SelectedItem.ToString().Trim() + " where STAFFNO=" + ddlDegree.SelectedValue.ToString().Trim() + " and COLLEGE_NO=" + ddlCollege.SelectedValue.ToString().Trim();
        SqlConnection sqlcon1 = new SqlConnection(_con);
        SqlDataAdapter da1 = new SqlDataAdapter(strSum, sqlcon1);
        DataTable dtSum = new DataTable();
        da1.Fill(dtSum);



        double cashAmt = 0;
        //GridData.Rows[i].Cells[0].Text.ToString().Trim()
        if (GridData.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // string temp=  dt.Rows[i]["PAY_HEAD_No"].ToString();

                for (int j = 0; j < dtSum.Columns.Count; j++)
                {
                    //checking 
                    string tycOL = dtSum.Columns[j].ToString();
                    string QW = dt.Rows[i]["PAY_HEAD_No"].ToString();

                    if (dt.Rows[i]["PAY_HEAD_No"].ToString() == dtSum.Columns[j].ToString())
                    {


                        Label lblCash = GridData.Rows[i].FindControl("lblAmt") as Label;
                        if (lblCash != null)
                        {
                            //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtSum.Rows[0][j].ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                            lblCash.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtSum.Rows[0][j].ToString().Trim())));
                            if (lblCash.Text == "")
                            {
                                lblCash.Text = "0.0";

                            }
                            cashAmt = cashAmt + Convert.ToDouble(lblCash.Text);
                            //cashAmt = String.Format("{0:0.00}", Math.Abs(cashAmt));
                        }
                    }
                }
            }
        }

        string databaseName = _con.Split('=')[4].ToString();
        DataSet dsEmpShare = ATC.GetPayHeadEmployer(Session["comp_code"].ToString(), ddlMon.SelectedItem.Text, databaseName);

       // DataSet dsEmpPayHead = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PAYROLL_ACCOUNT_MAPPING A INNER JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY B ON (A.CRLEDGERNO=B.PARTY_NO) INNER JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY C ON (A.DRLEDGERNO=C.PARTY_NO)", "PAY_HEAD_NO,PAY_HEAD_NAME", "B.PARTY_NAME PARTY_NAME_CR,C.PARTY_NAME PARTY_NAME_DR", "A.STAFF_NO=" + ddlDegree.SelectedValue+" and A.COLLEGE_ID=" + ddlCollege.SelectedValue.ToString().Trim()+"  AND A.SUBDEPT_NO=" + ddldept.SelectedValue , "");

        DataSet dsEmpPayHead = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PAYROLL_ACCOUNT_MAPPING A INNER JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY B ON (A.CRLEDGERNO=B.PARTY_NO) INNER JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY C ON (A.DRLEDGERNO=C.PARTY_NO)", "PAY_HEAD_NO,PAY_HEAD_NAME", "B.PARTY_NAME PARTY_NAME_CR,C.PARTY_NAME PARTY_NAME_DR", "A.STAFF_NO=" + ddlDegree.SelectedValue + " and A.COLLEGE_ID=" + ddlCollege.SelectedValue.ToString().Trim(), "");
      
        gvEmpShare.DataSource = dsEmpPayHead;
        gvEmpShare.DataBind();

        double cashAmtEmpShare = 0;
        if (gvEmpShare.Rows.Count > 0)
        {
            for (int i = 0; i < dsEmpPayHead.Tables[0].Rows.Count; i++)
            {
                // string temp=  dt.Rows[i]["PAY_HEAD_No"].ToString();

                for (int j = 0; j < dsEmpShare.Tables[0].Columns.Count; j++)
                {
                    //checking 
                    string tycOL = dsEmpShare.Tables[0].Columns[j].ToString();
                    string QW = dsEmpPayHead.Tables[0].Rows[i]["PAY_HEAD_No"].ToString();

                    if (dsEmpPayHead.Tables[0].Rows[i]["PAY_HEAD_No"].ToString() == dsEmpShare.Tables[0].Columns[j].ToString())
                    {


                        Label lblCash = gvEmpShare.Rows[i].FindControl("lblAmt") as Label;
                        if (lblCash != null)
                        {
                            //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtSum.Rows[0][j].ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                            lblCash.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dsEmpShare.Tables[0].Rows[0][j].ToString().Trim())));
                            if (lblCash.Text == "")
                            {
                                lblCash.Text = "0.0";

                            }
                            cashAmtEmpShare = cashAmtEmpShare + Convert.ToDouble(lblCash.Text);
                            //cashAmt = String.Format("{0:0.00}", Math.Abs(cashAmt));
                        }
                    }
                }
            }
        }
        //display amt
        lblTamt.ForeColor = System.Drawing.Color.Red;
        lblTamt.Text = String.Format("{0:0.00}", Math.Abs(cashAmt));

        lblEmpShareAmt.ForeColor = System.Drawing.Color.Red;
        lblEmpShareAmt.Text = String.Format("{0:0.00}", Math.Abs(cashAmtEmpShare));
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



    protected void btnSave0_Click(object sender, EventArgs e)
    {






    }

    protected void SaveRecord()
    {

        AccountTransactionController objAccTran = new AccountTransactionController();

        string StaffNo = (ddlDegree.SelectedValue.ToString().Trim());
        string monYr = (ddlMon.SelectedItem.ToString().Trim());
        int uno = Convert.ToInt32(Session["userno"].ToString().Trim());
        string compCode = Session["comp_code"].ToString();
        DateTime DateTime1 = Convert.ToDateTime(txtDate.Text);
        string Date = DateTime1.ToString("dd-MMM-yyyy");
        int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
    //    int CollegeId = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_MASTER", "CODE", "COLLEGE_ID="+Convert.ToInt32(ddlCollege.SelectedValue)));
        int DepartmentId=Convert.ToInt32(ddldept.SelectedValue);

        int i = 0;
        //objAccTran.AddPayRollLedgerTransfer(
        string[] database = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString.Split('=');
        string databaseName ="["+ database[4].ToString()+"]";
      //  DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "", "PAGENO='" + ddlMon.SelectedValue.ToString() + "' AND CBTYPE='" + ddlDegree.SelectedItem.ToString() + "' AND TRANSFER_ENTRY=2 AND COLLEGE_CODE='" + ddlCollege.SelectedValue + "' AND DEPT_ID="+ddldept.SelectedValue, "");
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "", "PAGENO='" + ddlMon.SelectedValue.ToString() + "' AND CBTYPE='" + ddlDegree.SelectedItem.ToString() + "' AND TRANSFER_ENTRY=2 AND COLLEGE_CODE='" + ddlCollege.SelectedValue + "'", "");
       
        // if (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) < 1)
        if (Convert.ToInt32(ds.Tables[0].Rows.Count) < 1)
        {
            i = objAccTran.AddPayRollLedgerTransfer(StaffNo, monYr, uno, compCode, databaseName, Date, CollegeId, DepartmentId);
            if (i == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Transfer Successfully", this);

                ClearAll();

            }
            else if (i == 0)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Already Transfered ! ", this);
                ClearAll();
            }
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Record Already Transfered ! ", this);

        }

    }


    protected void SaveRecordByEmployee()
    {


    }









    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry...Data Not Present. Click on Show Data", this);
            return;

        }

        SaveRecord();



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
        btnSave.Enabled = false;
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
            string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            DataSet DTDstaff = ATC.PopulateEmpPayroll(_con);
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
        //ClearAll();
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
