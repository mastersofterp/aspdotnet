//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 29-04-2014                                               
// CREATED BY    : Danish Ali                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
// PURPOSE       : TO DELETE TRANSFERED FEES FROM ACEDEMIC MODULE
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
//using System.Data.SqlClient;
using System.IO.Ports;
//using System.Data.OracleClient;
using IITMS.NITPRM;

public partial class DeleteTransferedFees : System.Web.UI.Page
{
    //Under development:- Conform Message box when we transfer
    //private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //private string _Fees = System.Configuration.ConfigurationManager.ConnectionStrings["FEE"].ConnectionString;
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountTransactionController objTrans = new AccountTransactionController();
    //OracleConnection ocon = new OracleConnection("Data Source=VNITFEES;UID=WCEFEES;PWD=WCEFEES");
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnTrans.Enabled = false;

        //if (txtFromDate.Text.ToString().Trim() != "")
        //{
        //    bool result = CheckEntry();
        //    if (result == true)
        //    {
        //        btnTrans.Attributes.Add("onClick", "return ShowConfirm();");
        //    }
        //}

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        { }
        else
            Response.Redirect("~/Default.aspx");

        if (!Page.IsPostBack)
        {
            //btnTrans.Attributes.Add("onclick", "return confirm('Record Allread Present Are U sure want to Replace records?')");
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

                    //txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyy");
                    // Filling Degrees
                    string IsCCMS = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='IS CCMS'");
                    if (IsCCMS == "Y")
                    {
                        row18.Visible = false;

                        Session["IsCCMS"] = IsCCMS;
                        PopulateReceptTypeDropdown();
                    }
                    else
                    {
                        // Filling Degrees
                        PopulateDegreeDropdown();
                        Session["IsCCMS"] = IsCCMS;
                        //Filling Recept list
                        PopulateReceptTypeDropdown();
                        row18.Visible = true;


                    }

                    //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //txtTodate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    ViewState["Operation"] = "Submit";

                    SetFinancialYear();
                }
            }
        }
        //if (txtFromDate.Text.ToString().Trim() != "")
        //{
        //    bool result = CheckEntry();
        //    if (result == true)
        //    {
        //        btnTrans.Attributes.Add("onClick", "return ShowConfirm();");
        //    }
        //    else
        //    {
        //        btnTrans.Attributes.Add("onClick", "return NotShowConfirm();");
        //    }
        //}
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
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtTodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
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
   
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        //SqlConnection sqlcon = new SqlConnection(_Fees);
        if (txtFromDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Transfer Date Required... ", this);
            txtFromDate.Focus();
            return;
        }
        if (txtFromDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Upto Date Required... ", this);
            txtFromDate.Focus();
            return;
        }
        if (DateTime.Compare(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtFromDate.Text)) > 0)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date And Upto Date Is Not Valid ", this);
            txtFromDate.Focus();
            return;
        }

        string CBtype = string.Empty;
        string compCode = Session["comp_code"].ToString().Trim();
        int degNo = 0;
        if (rdoMiscFees.Checked)
        {
            CBtype = "MF";
            degNo = 0;
        }
        else if (rdoGenralFees.Checked)
        {
            CBtype = ddlRecept.SelectedValue.ToString().Trim();
            if (Session["IsCCMS"].ToString() == "N")
                degNo = Convert.ToInt32(ddlDegree.SelectedValue);
            else
                degNo = 0;
        }
            
        //SQLHelper objSqlhelper = new SQLHelper(_nitprm_constr);
        DataSet DsFeelegHd = new DataSet();
        DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH,ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO,LH.LEDGERNO", "LH.FEE_HEAD_NAME, PT.PARTY_NAME", "LH.LEDGERNO=PT.PARTY_NO AND LH.DEGREENO=" + degNo + " AND LH.RECIEPT_TYPE='" + CBtype + "' AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'", "");

        if (DsFeelegHd != null && DsFeelegHd.Tables[0].Rows.Count > 0)
        {
            GridData.DataSource = DsFeelegHd;
            GridData.DataBind();
        }
        if (GridData.Rows.Count == 0)
        {
            //objCommon.DisplayMessage(UPDLedger, "DATA NOT AVAILABLE", this);
            //return;
        }
        DataSet dsCashAmt = new DataSet();
        double cashAmt = 0;
        dsCashAmt = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_trans", "isnull(sum(isnull(amount,0)),0)", "", "TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'  and TRANSFER_ENTRY=1 and CBTYPE='" + CBtype + "' and DEGREE_NO='" + degNo + "'", "");
        if (dsCashAmt.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToDouble(dsCashAmt.Tables[0].Rows[0][0]) > 0 && dsCashAmt.Tables[0].Rows[0][0].ToString() != "")
            {
                cashAmt = Convert.ToDouble(dsCashAmt.Tables[0].Rows[0][0]) / 2;
            }
        }
        GridData.Visible = false;
        lblCashTotal.ForeColor = System.Drawing.Color.Red;
        lblCashTotal.Width = 200;
        lblCashTotal.Height = 100;
        lblCashTotal.Text = String.Format("{0:0.00}", Math.Abs(cashAmt));
        if (cashAmt == 0)
            btnDelete.Enabled = false;
        else
            btnDelete.Enabled = true;
    }
    /// <summary>
    /// Get Fees title And Head no
    /// </summary>
    /// <param name="rpt_Type"></param>
    /// <returns></returns>
    public DataSet GetFeeHeadAndNo(string rpt_Type)
    {
        DataSet dtFeesheads = new DataSet();
        //dtFeesheads =null;
        try
        {
            string temp = " ";
            objCommon = new Common();
            dtFeesheads = objCommon.FillDropDown("FEE_TITLE", "FEE_HEAD_NO", "FEE_TITLE", "RECIEPT_TYPE='" + rpt_Type + "' AND FEE_TITLE !='" + temp + "'", "");

            if (dtFeesheads != null && dtFeesheads.Tables[0].Rows.Count > 0)
            {
                return dtFeesheads;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return dtFeesheads;


    }
    protected void btnSave0_Click(object sender, EventArgs e)
    {

    }
    protected void UpdateRecord()
    {
        FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
        AccountTransactionController objAccTran = new AccountTransactionController();

        int i = 0;

        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;
            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {
                FLHMobj.COLLEGE = Convert.ToInt32(ddlDegree.SelectedValue.ToString());

                FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();

                Label lblHNO = GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                FLHMobj.FEE_HEAD_NO = lblHNO.Text;

                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                FLHMobj.LEDGER_NO = Convert.ToInt32(ddlFH.SelectedValue.ToString());



                //Label lbl =GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                //string temp = lbl.Text;

                DropDownList ddlcas = GridData.Rows[Icount].FindControl("ddllCash") as DropDownList;
                FLHMobj.CASH_NO = Convert.ToInt32(ddlcas.SelectedValue.ToString());

                DropDownList ddlBank = GridData.Rows[Icount].FindControl("ddlBank") as DropDownList;
                FLHMobj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue.ToString());
                FLHMobj.LASTMODIFIER_DATE = DateTime.Now;
                //FLHMobj.CREATE_DATE = DateTime.Now;
                //FLHMobj.CREATER_NAME = Session["username"].ToString();
                FLHMobj.LASTMODIFIER = Session["username"].ToString();
                i = objAccTran.UpdateFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim(),0,"0");
            }
        }
        if (i == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully", this);
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Record Not Updated ", this);
        }
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
        // Added By Akshay Dixit On 06-07-2022
        ddlDegree.SelectedValue = "0";   
        ddlRecept.SelectedValue = "0";
        SetFinancialYear();
        lblCashTotal.Text = string.Empty;
        lblBankTotal.Text = string.Empty;


        GridData.DataSource = null;
        GridData.DataBind();
        btnDelete.Enabled = false;
        //txtFromDate.Text = string.Empty;
        //txtFromDate.Text = string.Empty;
        //txtFromDate.Focus();
        //lblCashTotal.Text = string.Empty;
        lblBankTotal.Text = string.Empty;
        lblCashTotal.Text = "0.0";
    }
   

    public void PopulateDegreeDropdown()
    {
        try
        {
            objCommon = new Common();
            //DataSet ds = objCommon.FillDropDown("acd_degree", "DEGREENO", "DEGREENAME", "DEGREENAME !='" + temp + "' AND DEGREENO>0", "");
            DataSet ds = objTrans.PopulateDegreeFromRF(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataSource = ds.Tables[0]; ;
                    ddlDegree.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    public void PopulateReceptTypeDropdown()
    {

        try
        {
            objCommon = new Common();
            DataSet ds = new DataSet();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                ds = objTrans.GetReceiptTypeForCCMS(_CCMS);
            }
            else
            {
                //ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
                ds = objTrans.PopulateReceiptType(_CCMS);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRecept.Items.Clear();
                ddlRecept.Items.Add("Please Select");
                ddlRecept.SelectedItem.Value = "0";
                ddlRecept.DataTextField = "RECIEPT_TITLE";
                ddlRecept.DataValueField = "RECIEPT_CODE";
                ddlRecept.DataSource = ds.Tables[0];
                ddlRecept.DataBind();
                ddlRecept.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.PopulateRecept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GridData_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearAll();
    }
    protected void ddlRecept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearAll();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string CBtype = string.Empty;
        string compCode = Session["comp_code"].ToString().Trim();
        int degNo = 0;
        if (rdoMiscFees.Checked)
        {
            CBtype = "MF";
            degNo = 0;
        }
        else if (rdoGenralFees.Checked)
        {
            CBtype = ddlRecept.SelectedValue.ToString().Trim();
            if (Session["IsCCMS"].ToString() == "N")
                degNo = Convert.ToInt32(ddlDegree.SelectedValue);
            else
                degNo = 0;
        }
        DataSet DsEntrys = new DataSet();
        DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=" + degNo + " AND CBTYPE='" + CBtype + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'", "TRANSACTION_DATE");
        if (DsEntrys.Tables[0].Rows.Count > 0)
        {
            //objCommon.DisplayMessage(UPDLedger, "Fees already transfered on date "+Convert.ToDateTime(DsEntrys.Tables[0].Rows[0]["TRANSACTION_DATE"]).ToString("dd-MMM-yyyy"), this);
            //return;
            DeleteTransfer();
            ClearAll();
        }

    }
    //not required 16052013
    public bool CheckEntry()
    {
        bool result = false;
        try
        {
            DataSet DsEntrys = new DataSet();
            //objCommon.FillDropDown("ACC_FEE_"+ Session["comp_code"].ToString().Trim() +"_LEDERHEAD", "*", "", "RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "' AND COLLEGE_CODE='" + ddlDegree.SelectedValue.ToString() + "'", "");
            //DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSACTION_DATE='" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "' AND TRANSFER_ENTRY='1' AND DEGREE_NO="+Convert.ToInt32( ddlDegree.SelectedValue)+" AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "'", "");
            DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'", "TRANSACTION_DATE");
            if (DsEntrys.Tables[0].Rows.Count > 0)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.CheckEntry-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return result;
    }
    protected void Transfer()
    {
        AccountTransactionController objAccTran = new AccountTransactionController();

        string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
        //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
        string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");
        //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
        int Uno = Convert.ToInt32(Session["userno"].ToString().Trim());
        string compCode = Session["comp_code"].ToString().Trim();
        int degNo = Convert.ToInt32(ddlDegree.SelectedValue);
        string retype = ddlRecept.SelectedValue.ToString().Trim();
        string collegeCode = ddlDegree.SelectedValue.ToString().Trim();
        string DegreeName = ddlDegree.SelectedItem.ToString().Trim();
        string RecieptType = ddlRecept.SelectedItem.ToString().Trim();


    }
    public void DeleteTransfer()
    {
        //Code To Check Whole Head Is Map Or Not
        //Added By Danish Ali on Date 08-05-2014
        try
        {
            AccountTransactionController objAccTran = new AccountTransactionController();
            string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");
            string CBtype=string.Empty;
            string compCode = Session["comp_code"].ToString().Trim();
            int degNo = 0;
            if (rdoMiscFees.Checked)
            {
                CBtype = "MF";
                degNo = 0;
            }
            else if (rdoGenralFees.Checked)
            {
                CBtype = ddlRecept.SelectedValue.ToString().Trim();
                if (Session["IsCCMS"].ToString() == "N")
                    degNo = Convert.ToInt32(ddlDegree.SelectedValue);
                else
                    degNo = 0;
            }
            

            int i = objAccTran.deleteTransactionForTransfer(DateFrom, DateTo, compCode, degNo, CBtype);
            if (i > 0)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Fees Successfully Deleted",this.Page);
            }
        }
            
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.DeleteTransfer-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdoMiscFees_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        row18.Visible = false;
        row4.Visible = false;
    }
    protected void rdoGenralFees_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        row4.Visible = true;
        if (Session["IsCCMS"].ToString() != "Y")
        {
            row18.Visible = true;
        }
    }
}

