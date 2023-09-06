//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ALERT CONFIGURATION                                                    
// CREATION DATE : 17-JAN-2019                                               
// CREATED BY    : NOKHLAL KUMAR                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Reflection;
using System.Collections.Generic;

public partial class ACCOUNT_AlertConfiguration : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();
    AccountTransactionController objATC = new AccountTransactionController();
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
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    ViewState["action"] = "add";

                    Session["dt"] = null;

                    BindAlertList();
                }
            }
        }
    }

    private void BindAlertList()
    {
        DataSet dsAlert = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_ALERTLIST A INNER JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY P ON A.LEDGERNO = P.PARTY_NO", "A.ALERTID", "A.ALERTNAME,P.PARTY_NO,P.PARTY_NAME", "", "A.ALERTNAME");

        if (dsAlert.Tables[0].Rows.Count > 0)
        {
            rptList.DataSource = dsAlert.Tables[0];
            rptList.DataBind();
            pnlList.Visible = true;
        }
        else
        {
            rptList.DataSource = null;
            rptList.DataBind();
            pnlList.Visible = false;
        }
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMergeLedger(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetMergeData(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }
    protected void btnItemEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmail.Text != string.Empty || txtPhoneNo.Text != string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry! Cannot Edit, already one record is selected!", this.Page);
            return;
        }
        //if (ViewState["SerialNo"] != null)
        //{

        //}

        ImageButton btnItemEdit = sender as ImageButton;

        DataTable dtTran = new DataTable();
        if (Session["dt"] != null)
            dtTran = (DataTable)Session["dt"];

        DataView dv = dtTran.DefaultView;
        dv.RowFilter = "SRNO=" + btnItemEdit.CommandArgument;
        DataTable dtRow = dv.ToTable();
        if (dtRow.Rows[0]["SRNO"].ToString() == "1")
        {
            ViewState["SerialNo"] = 1;
            txtEmail.Text = dtRow.Rows[0]["EMAILID"].ToString();
            txtPhoneNo.Text = dtRow.Rows[0]["MOBILENO"].ToString();
        }
        else
        {
            ViewState["SerialNo"] = dtRow.Rows[0]["SRNO"].ToString();
            txtEmail.Text = dtRow.Rows[0]["EMAILID"].ToString();
            txtPhoneNo.Text = dtRow.Rows[0]["MOBILENO"].ToString();
        }

        dv = null;
        DataView dvrow1 = dtTran.DefaultView;
        dvrow1.RowFilter = "SRNO<>" + btnItemEdit.CommandArgument;
        dtTran = dvrow1.ToTable();
        RptAlertDetails.DataSource = dtTran;
        RptAlertDetails.DataBind();
        Session["dt"] = dtTran;
    }
    protected void btnItemDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnItemDelete = sender as ImageButton;

        DataTable dtTran = new DataTable();
        if (Session["dt"] != null)
            dtTran = (DataTable)Session["dt"];

        DataView dvrow1 = dtTran.DefaultView;
        dvrow1.RowFilter = "SRNO<>" + btnItemDelete.CommandArgument;
        dtTran = dvrow1.ToTable();
        RptAlertDetails.DataSource = dtTran;
        RptAlertDetails.DataBind();
        Session["dt"] = dtTran;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPhoneNo.Text == "" && txtEmail.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter atleast Anyone Alert To(Email or Phone No.)", this.Page);
            return;
        }

        DataTable dt = new DataTable();
        dt = new DataTable();
        if (Session["dt"] != null)
            dt = (DataTable)Session["dt"];

        if (!dt.Columns.Contains("SRNO"))
            dt.Columns.Add("SRNO");
        if (!dt.Columns.Contains("EMAILID"))
            dt.Columns.Add("EMAILID");
        if (!dt.Columns.Contains("MOBILENO"))
            dt.Columns.Add("MOBILENO");

        int SerialNumber = Convert.ToInt32(ViewState["SerialNo"] == null ? "0" : ViewState["SerialNo"]);

        DataRow dr = dt.NewRow();
        if (ViewState["SerialNo"] != null)
        {
            dr["SRNO"] = ViewState["SerialNo"].ToString();
        }
        else
        {
            if (ViewState["SRNO"] != null)
                dr["SRNO"] = 1 + Convert.ToInt32(ViewState["SRNO"] == null ? "0" : ViewState["SRNO"]);
            else
                dr["SRNO"] = Convert.ToInt32(SerialNumber) + 1;
        }

        dr["EMAILID"] = txtEmail.Text.Trim().ToString();
        dr["MOBILENO"] = txtPhoneNo.Text.Trim().ToString();

        dt.Rows.Add(dr);

        RptAlertDetails.DataSource = dt;
        RptAlertDetails.DataBind();

        DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
        ViewState["SRNO"] = Convert.ToInt32(lastRow["SRNO"]);

        Session["dt"] = dt;
        txtEmail.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        Session["dt"] = null;
        RptAlertDetails.DataSource = null;
        RptAlertDetails.DataBind();

        ImageButton btnEdit = sender as ImageButton;
        int alertId = Convert.ToInt32(btnEdit.CommandArgument.ToString());
        ViewState["AlertId"] = alertId;
        ViewState["action"] = "edit";
        //objCommon.DisplayMessage(UPDLedger, "Edit Id:-" + alertId, this.Page);

        DataSet dsList = objATC.GetAlertDetails(alertId, Session["comp_code"].ToString());

        if (dsList.Tables.Count > 0)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                txtAlertFor.Text = dsList.Tables[0].Rows[0]["ALERTNAME"].ToString();
                txtAcc.Text = dsList.Tables[0].Rows[0]["PARTY_NAME"].ToString();
            }
            if (dsList.Tables[1].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = new DataTable();
                if (Session["dt"] != null)
                    dt = (DataTable)Session["dt"];

                if (!dt.Columns.Contains("SRNO"))
                    dt.Columns.Add("SRNO");
                if (!dt.Columns.Contains("EMAILID"))
                    dt.Columns.Add("EMAILID");
                if (!dt.Columns.Contains("MOBILENO"))
                    dt.Columns.Add("MOBILENO");

                for (int i = 0; i < dsList.Tables[1].Rows.Count; i++)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["SRNO"] = dsList.Tables[1].Rows[i]["SRNO"].ToString();
                    drnew["EMAILID"] = dsList.Tables[1].Rows[i]["EMAILID"].ToString();
                    drnew["MOBILENO"] = dsList.Tables[1].Rows[i]["MOBILENO"].ToString();

                    dt.Rows.Add(drnew);
                }
                Session["dt"] = dt;

                RptAlertDetails.DataSource = dsList.Tables[1];
                RptAlertDetails.DataBind();
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtDetails = new DataTable();
            DataTable dtnew = new DataTable();

            if (!dtDetails.Columns.Contains("EMAILID"))
                dtDetails.Columns.Add("EMAILID");
            if (!dtDetails.Columns.Contains("MOBILENO"))
                dtDetails.Columns.Add("MOBILENO");

            string AlertName = "";
            int LedgerNo = 0;
            int userno = 0;
            int AlertId = 0;
            if (txtAcc.Text == string.Empty || txtAcc.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Select Any Ledger", this.Page);
                txtAcc.Focus();
                return;
            }
            if (txtAlertFor.Text == string.Empty || txtAlertFor.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Alert For", this.Page);
                txtAlertFor.Focus();
                return;
            }
            if (RptAlertDetails.Items.Count > 0)
            {
                dtnew = (DataTable)Session["dt"];
                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    DataRow drDetails = dtDetails.NewRow();

                    drDetails["EMAILID"] = dtnew.Rows[i]["EMAILID"].ToString();
                    drDetails["MOBILENO"] = dtnew.Rows[i]["MOBILENO"].ToString();

                    dtDetails.Rows.Add(drDetails);
                }
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Please Add atleast one Contact Details(Email & Phone No.)", this.Page);
                txtEmail.Focus();
                return;
            }

            AlertName = txtAlertFor.Text.Trim().ToString();
            string ledger = txtAcc.Text.Split('*')[1].ToString();
            LedgerNo = Convert.ToInt32(ledger);
            userno = Convert.ToInt32(Session["userno"].ToString());
            //ViewState["AlertId"] = 0;

            if (ViewState["action"].ToString() == "add")
            {
                AlertId = 0;
            }
            else
            {
                AlertId = Convert.ToInt32(ViewState["AlertId"].ToString());
            }

            int ret = objATC.InsertUpdAlertDetails(AlertId, Session["comp_code"].ToString(), AlertName, LedgerNo, dtDetails);

            if (ret == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Inserted Successfully!", this.Page);
                txtAcc.Text = string.Empty;
                txtAlertFor.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;

                RptAlertDetails.DataSource = null;
                RptAlertDetails.DataBind();
                Session["dt"] = null;
                ViewState["SRNO"] = null;
                ViewState["SerialNo"] = null;

                BindAlertList();
            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully!", this.Page);
                txtAcc.Text = string.Empty;
                txtAlertFor.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;

                RptAlertDetails.DataSource = null;
                RptAlertDetails.DataBind();
                Session["dt"] = null;
                ViewState["SRNO"] = null;
                ViewState["SerialNo"] = null;

                BindAlertList();
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "Something is wrong, Please try again after sometime!", this.Page);
                return;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AlertConfiguration.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAcc.Text = string.Empty;
        txtAlertFor.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;

        BindAlertList();

        RptAlertDetails.DataSource = null;
        RptAlertDetails.DataBind();
        Session["dt"] = null;
        ViewState["SRNO"] = null;
        ViewState["SerialNo"] = null;
    }
}