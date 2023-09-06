using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text;
using System.IO;
using BusinessLogicLayer.BusinessLogic.Account;
using IITMS.NITPRM;

public partial class ACCOUNT_ModifyFeesRefundStudentwise : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    FeesRefundController objFRC = new FeesRefundController();
    FeesTransferStudentwiseController objFTS = new FeesTransferStudentwiseController();
    AccountTransaction objAccountTrans = new AccountTransaction();

    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    objCommon.DisplayUserMessage(updBank, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    txtFromDate.Focus();
                    PopulateDegreeDropdown();
                    PopulateVoucherNo();
                    SetFinancialYear();
                }
            }
        }

    }

    #region Private Event

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
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtTodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }


    private void PopulateDegreeDropdown()
    {
        try
        {
            objCommon = new Common();
            DataSet ds = objFTS.PopulateDegreeFromRF(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDegree.Items.Insert(0, "Please Select");
                    ddlDegree.SelectedValue = "0";
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
                objCommon.ShowError(Page, "FeeTransferStudentwise.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateVoucherNo()
    {
        try
        {
            objCommon = new Common();
            //
            string Database = connectionString.Split(';')[3];
            string DbName = Database.Split('=')[1];
            //

            DataSet ds = objFRC.BindRefundVoucherNo(_CCMS, Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString())), Session["comp_code"].ToString(), DbName);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVoucher.Items.Clear();
                    ddlVoucher.Items.Insert(0, "Please Select");
                    //ddlVoucher.SelectedValue = "0";
                    ddlVoucher.DataTextField = "VoucherNo";
                    ddlVoucher.DataValueField = "VchNo";
                    ddlVoucher.DataSource = ds.Tables[0];
                    ddlVoucher.DataBind();

                }
                else
                {
                    ddlVoucher.DataSource = null;
                    ddlVoucher.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FeeTransferStudentwise.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowAll()
    {
        try
        {
            FeesTransferStudentwiseController objFTS = new FeesTransferStudentwiseController();
            string REFUNDNO = string.Empty;
            DataSet ds = null;
            ds = objFRC.BindStudentByVoucherNo(_CCMS, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString())), Convert.ToInt32(ddlVoucher.SelectedValue.Split('*')[0]));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                TrFees.Visible = true;
                btnCollect.Visible = true;
                trTotal.Visible = true;
                Panel1.Visible = true;
                lvFeeTransfer.DataSource = ds;
                lvFeeTransfer.DataBind();
                lblChkCount.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACD_DCR.ShowAll -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        trTotal.Visible = false;
        txtFromDate.Text = Session["fin_date_from"].ToString();
        txtTodate.Text = Session["fin_date_to"].ToString();

        txtFromDate.Focus();
        lvFeeTransfer.DataSource = null;
        lvFeeTransfer.DataBind();
        lstFees.DataSource = null;
        lstFees.DataBind();
        ddlLedger.SelectedValue = "0";
        lblTotal.Text = "0";
        btnTransfer.Visible = false;
        divTransfer.Visible = false;
        btnCollect.Visible = false;
        btnTransfer.Visible = false;
        Panel1.Visible = false;
        ViewState["uanos"] = null;
        ViewState["VoucherNo"] = null;
        Session["dtFees"] = null;
    }

    #endregion Private Event

    #region Page Events

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        //PopulateVoucherNo();
    }
    protected void ddlVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        //PopulateVoucherNo();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowAll();

        objCommon.FillDropDownList(ddlLedger, "ACC_" + Session["comp_code"] + "_PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN(1,2)", "PARTY_NAME");
        ddlLedger.SelectedValue = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "PARTY_NO", "TRANSACTION_TYPE='P' AND VOUCHER_NO = '" + ddlVoucher.SelectedValue.Split('*')[0].ToString() + "' AND SUBTR_NO > 0");

        DataSet ds = objFTS.GetVoucherData(connectionString, Session["comp_code"].ToString(), ddlVoucher.SelectedValue.Split('*')[0].ToString());
        Session["VoucherData"] = ds.Tables[0];

        txtVoucherDate.Text = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "TRANSACTION_DATE", "TRANSACTION_TYPE='P' AND VOUCHER_NO = '" + ddlVoucher.SelectedValue.Split('*')[0].ToString() + "' AND SUBTR_NO > 0");
    }

    protected void btnCollect_Click(object sender, EventArgs e)
    {
        string uanos = string.Empty;
        ViewState["uanos"] = null;
        ViewState["VoucherNo"] = null;
        lstFees.DataSource = null;
        lstFees.DataBind();
        lblTotal.Text = "0";

        foreach (ListViewDataItem lvItem in lvFeeTransfer.Items)
        {
            CheckBox chkAccept = lvItem.FindControl("chkFeesTransfer") as CheckBox;
            Label lblDCRVoucherNo = lvItem.FindControl("lblDCRVoucherNo") as Label;
            if (chkAccept.Checked == true)
            {
                ViewState["uanos"] += chkAccept.ToolTip + "$";
                ViewState["VoucherNo"] += lblDCRVoucherNo.Text + ",";
            }
        }
        if (ViewState["uanos"] == null || ViewState["uanos"].ToString() == string.Empty || ViewState["uanos"].ToString() == "")
        {
            objCommon.DisplayMessage(updBank, "Please select Student Name", this);
            return;
        }
        else
        {
            ViewState["uanos"] = ViewState["uanos"].ToString().TrimEnd('$');
            DataSet ds = objFRC.BindRefundAmountFeeHeadWise(_CCMS, ddlVoucher.SelectedValue.Split('*')[1].ToString(), ViewState["uanos"].ToString());
            ViewState["uanos"] = ViewState["uanos"].ToString().Replace('$', ',').TrimEnd(',');

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnTransfer.Visible = true;
                divTransfer.Visible = true;
                btnTransfer.Visible = true;
                TrFees.Visible = true;
                lstFees.DataSource = ds;
                lstFees.DataBind();
                lblTotal.Text = "0";
                foreach (ListViewDataItem lvfees in lstFees.Items)
                {
                    Label lblAmount = lvfees.FindControl("lblAmount") as Label;
                    double Amount = Convert.ToDouble(lblTotal.Text);
                    Amount = Amount + Convert.ToDouble(lblAmount.Text);
                    lblTotal.Text = Amount.ToString();
                }
            }
            else
            {
                objCommon.DisplayMessage(updBank, "Please Map Ledger First", this);
                return;
            }
        }
    }

    #endregion

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["uanos"] == null || ViewState["uanos"].ToString() == string.Empty || ViewState["uanos"].ToString() == "")
            {
                objCommon.DisplayMessage(updBank, "Please select Student Name", this);
                return;
            }
            else
            {
                int ret = 0;
                Session["dtFees"] = null;
                DataTable dtFees = new DataTable();
                if (Session["dtFees"] != null)
                    dtFees = (DataTable)Session["dtFees"];
                if (!(dtFees.Columns.Contains("FEE_HEAD")))
                    dtFees.Columns.Add("FEE_HEAD");
                if (!(dtFees.Columns.Contains("FEE_LONGNAME")))
                    dtFees.Columns.Add("FEE_LONGNAME");
                if (!(dtFees.Columns.Contains("amount")))
                    dtFees.Columns.Add("amount");
                if (!(dtFees.Columns.Contains("TRANS")))
                    dtFees.Columns.Add("TRANS");

                foreach (ListViewDataItem lvfees in lstFees.Items)
                {
                    Label lblFeeHeadsNo = lvfees.FindControl("lblFeeHeadsNo") as Label;
                    Label lblFeeHeads = lvfees.FindControl("lblFeeHeads") as Label;
                    Label lblAmount = lvfees.FindControl("lblAmount") as Label;

                    DataRow drFees = dtFees.NewRow();
                    drFees["FEE_HEAD"] = lblFeeHeadsNo.Text;
                    drFees["FEE_LONGNAME"] = lblFeeHeads.Text;
                    drFees["amount"] = lblAmount.Text;
                    drFees["TRANS"] = "Dr";

                    dtFees.Rows.Add(drFees);
                }

                if (Session["dtFees"] == null)
                    Session["dtFees"] = dtFees;
                objAccountTrans.COMPANY_CODE = Session["comp_code"].ToString();
                objAccountTrans.OPARTY_NO = ddlLedger.SelectedValue;
                objAccountTrans.TRANSACTION_TYPE = "P";
                objAccountTrans.TRAN = "Cr";
                objAccountTrans.DEGREE_NO = Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString()));
                objAccountTrans.CBTYPE = ddlVoucher.SelectedValue.Split('*')[1].ToString();
                objAccountTrans.USER = Session["userno"].ToString();
                objAccountTrans.PARTICULARS = "Fees Refund for Voucher No:-" + ViewState["VoucherNo"].ToString().TrimEnd(',');
                string transdate = Convert.ToDateTime(txtVoucherDate.Text).ToString("dd-MMM-yyyy");
                objAccountTrans.TRANSACTION_DATE = Convert.ToDateTime(transdate);

                ret = objFRC.ModifyfeesRefundStudentWise(connectionString, objAccountTrans, (DataTable)Session["dtFees"], Convert.ToDouble(lblTotal.Text), ViewState["uanos"].ToString(), Convert.ToInt32(ddlVoucher.SelectedValue.Split('*')[0]));

                if (ret == 1)
                {
                    objCommon.DisplayMessage(updBank, "Fees Refund Successful.", this);
                    Clear();
                    PopulateDegreeDropdown();
                    PopulateVoucherNo();
                }
                else
                {
                    objCommon.DisplayMessage(updBank, "Exception occured, Please try again.", this);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACD_DCR.ShowAll -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlDegree.SelectedValue = "Please Select";
        ddlVoucher.SelectedValue = "Please Select";
        Clear();
    }
}