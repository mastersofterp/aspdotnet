//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 03-01-2019                                               
// CREATED BY    : PRASHANT WANKAR                                                 
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

public partial class ACCOUNT_SupplimentaryLedgerHeadMapping : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    OracleConnection ocon = new OracleConnection("Data Source=VNITFEES;UID=WCEPAY;PWD=WCEPAY");
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

                    //Fill dropdown list
                    PopulateSTAFDropdown();
                    PopulatePartyDDL();
                    GetCollege();
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }


    private void CheckPageAuthorization()
    {
        //Check for Authorization of Page
        //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
        //{
        //    Response.Redirect("~/notauthorized.aspx?page=BankEntry.aspx");
        //}
    }

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        DataTable dtPAYhead = new DataTable();
        dtPAYhead = GetPayHeadNameAndNoo();

        if (dtPAYhead.Rows.Count == 0)
        {
            GridData.DataBind();
            objCommon.DisplayMessage(UPDLedger, "DATA NOT AVAILABLE", this);
            btnSave.Enabled = false;
            return;
        }

       
        GridData.DataSource = dtPAYhead;
        GridData.DataBind();

        //Filling dropdown lists 
        if (GridData.Rows.Count > 0)
        {
            int x = 0;
            for (x = 0; x < GridData.Rows.Count; x++)
            {
                DropDownList ddlledger = GridData.Rows[x].FindControl("ddlleagerHead") as DropDownList;
                if (ddlledger != null)
                {
                    objCommon.FillDropDownList(ddlledger, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");
                }
            }
            btnSave.Enabled = true;
        }
        else
        {
            btnSave.Enabled = false;
        }
        DataSet dsFeeLedgerHead = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYROLL_SUPPLIHEAD_MAPPING", "*", "", "STAFF_NO=" + ddlDegree.SelectedValue, "");

        if (dsFeeLedgerHead.Tables[0].Rows.Count > 0)
        {
            if (dsFeeLedgerHead.Tables[0].Rows.Count > 0)
            {
                if (GridData.Rows.Count > 0)
                {
                    int y = 0;
                    for (y = 0; y < GridData.Rows.Count; y++)
                    {
                        Label lblfeehd = GridData.Rows[y].FindControl("lblPayHeadsNo") as Label;
                        if (lblfeehd != null)
                        {
                            for (int i = 0; i < dsFeeLedgerHead.Tables[0].Rows.Count; i++)
                            {
                                if (lblfeehd.Text.ToString().Trim() == dsFeeLedgerHead.Tables[0].Rows[i]["SUPLHNO"].ToString().Trim())
                                {
                                    DropDownList ddlledger = GridData.Rows[y].FindControl("ddlleagerHead") as DropDownList;
                                    if (ddlledger != null)
                                    {
                                        ddlledger.SelectedValue = dsFeeLedgerHead.Tables[0].Rows[i]["LEDGER_NO"].ToString().Trim();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Get pay PAYNAME And  no
    /// </summary>
    /// <param name="rpt_Type"></param>
    /// <returns></returns>
    public DataTable GetPayHeadNameAndNoo()
    {
        DataTable dtpAYsheads = new DataTable();
        try
        {
            AccountTransactionController ATC = new AccountTransactionController();
            string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            DataSet dsPayHead = ATC.GetSupliPayHead("33");

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

    protected void btnSave0_Click(object sender, EventArgs e)
    {
    }

    protected void SaveRecord()
    {
        AccountTransactionController objAccTran = new AccountTransactionController();
        int i = 0;
        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;
            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {
                int StaffNo = Convert.ToInt32(ddlDegree.SelectedValue.ToString().Trim());
                int bankNo = Convert.ToInt32(ddlPARTY.SelectedValue.ToString().Trim());
                Label lblPHeads = GridData.Rows[Icount].FindControl("lblPayHeads") as Label;
                string strPayHeads = lblPHeads.Text;
                Label lblPhdNo = GridData.Rows[Icount].FindControl("lblPayHeadsNo") as Label;
                string strPayHdNo = lblPhdNo.Text;
                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                int LgNO = Convert.ToInt32(ddlFH.SelectedValue.ToString());
                int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
                if (ddlFH.SelectedItem.ToString() != "Please Select")
                {
                    i = objAccTran.AddSupplimentaryLedgerMaping(strPayHdNo, strPayHeads, LgNO, bankNo, StaffNo, Session["comp_code"].ToString().Trim(), 0, 0, 0, CollegeId);
                }
            }
        }
        if (gvEmployer.Rows.Count > 0)
        {
            for (int j = 0; j < gvEmployer.Rows.Count; j++)
            {
                int StaffNo = Convert.ToInt32(ddlDegree.SelectedValue.ToString().Trim());
                int bankNo = Convert.ToInt32(ddlPARTY.SelectedValue.ToString().Trim());

                Label lblPHeads = gvEmployer.Rows[j].FindControl("lblPayHeads") as Label;
                string strPayHeads = lblPHeads.Text;

                Label lblPhdNo = gvEmployer.Rows[j].FindControl("lblPayHeadsNo") as Label;
                string strPayHdNo = lblPhdNo.Text;

                DropDownList ddlCr = gvEmployer.Rows[j].FindControl("ddlCr") as DropDownList;
                DropDownList ddlDr = gvEmployer.Rows[j].FindControl("ddlDr") as DropDownList;
                int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
                if (ddlCr.SelectedValue != "0" && ddlDr.SelectedValue != "0")
                {
                    i = objAccTran.AddSupplimentaryLedgerMaping(strPayHdNo, strPayHeads, 0, bankNo, StaffNo, Session["comp_code"].ToString().Trim(), 1, Convert.ToInt32(ddlCr.SelectedValue), Convert.ToInt32(ddlDr.SelectedValue), CollegeId);
                }
            }
        }
        if (i == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);
            ClearAll();
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Record Not Saved ", this);

        }
    }

    protected void UpdateRecord()
    {
        AccountTransactionController objAccTran = new AccountTransactionController();
        int i = 0;
        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;
            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {
                int StaffNo = Convert.ToInt32(ddlDegree.SelectedValue.ToString().Trim());
                int bankNo = Convert.ToInt32(ddlPARTY.SelectedValue.ToString().Trim());

                Label lblPHeads = GridData.Rows[Icount].FindControl("lblPayHeads") as Label;
                string strPayHeads = lblPHeads.Text;

                Label lblPhdNo = GridData.Rows[Icount].FindControl("lblPayHeadsNo") as Label;
                string strPayHdNo = lblPhdNo.Text;

                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                int LgNO = Convert.ToInt32(ddlFH.SelectedValue.ToString());

                if (ddlFH.SelectedItem.ToString() != "Please Select")
                {
                    i = objAccTran.UpdatePayRollLedgerMaping(strPayHdNo, strPayHeads, LgNO, bankNo, StaffNo, Session["comp_code"].ToString().Trim());
                }
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

    protected void btnEmpShare_Click(object sender, EventArgs e)
    {
        DataTable dtEmpShare = new DataTable();
        if (ViewState["dtEmpShare"] != null)
            dtEmpShare = (DataTable)ViewState["dtEmpShare"];

        if (!dtEmpShare.Columns.Contains("PAYHEAD"))
            dtEmpShare.Columns.Add("PAYHEAD");

        if (!dtEmpShare.Columns.Contains("FULLNAME"))
            dtEmpShare.Columns.Add("FULLNAME");

        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            CheckBox chkPayHeadNo = GridData.Rows[i].FindControl("chkPayHeadNo") as CheckBox;
            if (chkPayHeadNo.Checked == true)
            {
                DataRow dr = dtEmpShare.NewRow();
                Label lblPayHeadsNo = GridData.Rows[i].FindControl("lblPayHeadsNo") as Label;
                Label lblPayHeads = GridData.Rows[i].FindControl("lblPayHeads") as Label;
                dr["PAYHEAD"] = lblPayHeadsNo.Text.Trim();
                dr["FULLNAME"] = lblPayHeads.Text;
                dtEmpShare.Rows.Add(dr);
                chkPayHeadNo.Checked = false;
            }
        }
        gvEmployer.DataSource = dtEmpShare;
        gvEmployer.DataBind();
        ViewState["dtEmpShare"] = dtEmpShare;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry...Data Not Present. Click on Show Data", this);
            return;
        }
        if (gvEmployer.Rows.Count > 0)
        {
            for (int i = 0; i < gvEmployer.Rows.Count; i++)
            {
                DropDownList ddlCr = gvEmployer.Rows[i].FindControl("ddlCr") as DropDownList;
                DropDownList ddlDr = gvEmployer.Rows[i].FindControl("ddlDr") as DropDownList;
                if (ddlCr.SelectedValue == "0" && ddlDr.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Ledger Head for Employee share", this);
                    return;
                }
            }
        }

        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;
            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {
                int StaffNo = Convert.ToInt32(ddlDegree.SelectedValue.ToString().Trim());
                int bankNo = Convert.ToInt32(ddlPARTY.SelectedValue.ToString().Trim());

                Label lblPHeads = GridData.Rows[Icount].FindControl("lblPayHeads") as Label;
                string strPayHeads = lblPHeads.Text;

                Label lblPhdNo = GridData.Rows[Icount].FindControl("lblPayHeadsNo") as Label;
                string strPayHdNo = lblPhdNo.Text;

                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                int LgNO = Convert.ToInt32(ddlFH.SelectedValue.ToString());

                if (lblPHeads.Text == "TAX DEDUCTION HEAD" && lblPhdNo.Text == "T" && ddlFH.SelectedItem.ToString() == "Please Select")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Map Tax Deduction Head...", this);
                    return;
                }

                if (lblPHeads.Text == "CONVENCE HEAD" && lblPhdNo.Text == "C" && ddlFH.SelectedItem.ToString() == "Please Select")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Map Convence Head...", this);
                    return;
                }
            }
        }

        bool resultChkEntry = checkForEntry();
        if (resultChkEntry == true)
        {
            string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            string DelQry = "Delete From ACC_" + Session["comp_code"].ToString() + "_PAYROLL_SUPPLIHEAD_MAPPING where STAFF_NO='" + ddlDegree.SelectedValue.ToString() + "' and COLLEGE_ID='" + ddlCollege.SelectedValue.ToString() + "'";
            SqlConnection sqlcon = new SqlConnection(_UAIMS_constr);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(DelQry, sqlcon);
            cmd.ExecuteNonQuery();
            sqlcon.Close();
            SaveRecord();
        }
        else
        {
            SaveRecord();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        GridData.DataSource = null;
        GridData.DataBind();
        btnSave.Enabled = false;
    }

    public void PopulateSTAFDropdown()
    {
        try
        {
            string temp = " ";
            AccountTransactionController ATC = new AccountTransactionController();
            string _con = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            DataSet dsStaff = ATC.PopulateEmpPayroll(_con);

            if (dsStaff.Tables[0].Rows.Count > 0)
            {
                ddlDegree.DataTextField = "STAFF";
                ddlDegree.DataValueField = "STAFFNO";
                ddlDegree.DataSource = dsStaff;
                ddlDegree.DataBind();
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

    public void PopulatePartyDDL()
    {
        DataSet dsLH = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NAME", "PARTY_NO", "PAYMENT_TYPE_NO =2", "");
        ddlPARTY.DataSource = dsLH.Tables[0];
        ddlPARTY.DataTextField = "PARTY_NAME";
        ddlPARTY.DataValueField = "PARTY_NO";
        ddlPARTY.DataBind();

    }

    //inserting if new legerhead Found 
    public void AddSinglerecord(string newLedgerhead)
    {
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
    }
    protected void ddlPARTY_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    public bool checkForEntry()
    {
        bool result = false;
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYROLL_ACCOUNT_MAPPING", "*", "", "STAFF_NO='" + ddlDegree.SelectedValue.ToString() + "' ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;
        }
        return result;
    }
    protected void gvEmployer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            DataTable dtEmpShare = new DataTable();
            if (ViewState["dtEmpShare"] != null)
                dtEmpShare = (DataTable)ViewState["dtEmpShare"];

            if (!dtEmpShare.Columns.Contains("PAYHEAD"))
                dtEmpShare.Columns.Add("PAYHEAD");

            if (!dtEmpShare.Columns.Contains("FULLNAME"))
                dtEmpShare.Columns.Add("FULLNAME");

            DataView dv = dtEmpShare.DefaultView;
            dv.RowFilter = "PAYHEAD<>'" + e.CommandArgument + "'";
            dtEmpShare = dv.ToTable();
            dtEmpShare.AcceptChanges();
            gvEmployer.DataSource = dtEmpShare;
            gvEmployer.DataBind();
            ViewState["dtEmpShare"] = dtEmpShare;
        }
    }

    protected void gvEmployer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCr = e.Row.FindControl("ddlCr") as DropDownList;
            objCommon.FillDropDownList(ddlCr, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");

            DropDownList ddlDr = e.Row.FindControl("ddlDr") as DropDownList;
            objCommon.FillDropDownList(ddlDr, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");
        }
    }
    protected void gvEmployer_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}