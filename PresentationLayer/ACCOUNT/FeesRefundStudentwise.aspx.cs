﻿using System;
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
using BusinessLogicLayer.BusinessLogic.Account;
using IITMS.NITPRM;

public partial class ACCOUNT_FeesRefundStudentwise : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    FeesRefundController objFRC = new FeesRefundController();
    FeesTransferStudentwiseController objFTS = new FeesTransferStudentwiseController();
    AccountTransaction objAccountTrans = new AccountTransaction();
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    static string strManual = string.Empty;
    string ReceiptNo = string.Empty;

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
                    PopulateReceiptTypeDropdown();
                    SetFinancialYear();
                    txtVoucherDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    objCommon.FillDropDownList(ddlbatch, "acd_admbatch", "BATCHNO", "BATCHNAME", "BATCHNO<>0", "BATCHNO");
                    objCommon.FillDropDownList(ddsem, "acd_semester", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO<>0", "SEMESTERNO");
           
                }
            }
        }
    }

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
                    ddlDegree.Items.Clear();
                    ddlDegree.Items.Insert(0, "Please Select");
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

    private void PopulateReceiptTypeDropdown()
    {
        try
        {
            objCommon = new Common();
            DataSet ds = objFTS.PopulateReceiptType(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlReceipt.Items.Clear();
                    ddlReceipt.Items.Insert(0, "Please Select");
                    ddlReceipt.SelectedValue = "Please Select";
                    ddlReceipt.DataTextField = "RECIEPT_TITLE";
                    ddlReceipt.DataValueField = "RECIEPT_CODE";
                    ddlReceipt.DataSource = ds.Tables[0]; ;
                    ddlReceipt.DataBind();
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

    private void Clear()
    {
        trTotal.Visible = false;

        rdbTransferType.SelectedValue = "C";
        lvFeeTransfer.DataSource = null;
        lvFeeTransfer.DataBind();
        lstFees.DataSource = null;
        lstFees.DataBind();
        lblTotal.Text = "0";
      
        btnTransfer.Visible = false;
        divTransfer.Visible = false;
        btnCollect.Visible = false;
        btnTransfer.Visible = false;
        Panel1.Visible = false;
        ViewState["uanos"] = null;
        ViewState["ReceiptNo"] = null;
        Session["dtFees"] = null;
    }

    private void RdoClear()
    {
        trTotal.Visible = false;
        lvFeeTransfer.DataSource = null;
        lvFeeTransfer.DataBind();
        lstFees.DataSource = null;
        lstFees.DataBind();
        lblTotal.Text = "0";
        btnTransfer.Visible = false;
        divTransfer.Visible = false;
        btnCollect.Visible = false;
        btnTransfer.Visible = false;
        Panel1.Visible = false;
        ViewState["uanos"] = null;
        ViewState["ReceiptNo"] = null;
        Session["dtFees"] = null;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lvFeeTransfer.DataSource = null;
        lvFeeTransfer.DataBind();
        ShowAll();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        txtFromDate.Text = Session["fin_date_from"].ToString();
        txtTodate.Text = Session["fin_date_to"].ToString();
        ddlDegree.SelectedIndex = 0;
        txtFromDate.Focus();
        ddlReceipt.SelectedValue = "Please Select";
        ddlDegree.SelectedValue = "Please Select";
    }

    private void ShowAll()
    {

        DataSet ds = null;
        ds = objFRC.GetRefundStudentList(_CCMS, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString())), ddlReceipt.SelectedValue.ToString(), rdbTransferType.SelectedValue, Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddsem.SelectedValue), Convert.ToInt32(ddlbatch.SelectedValue));
       
        try
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvFeeTransfer.DataSource = ds;
                lvFeeTransfer.DataBind();
                Panel1.Visible = true;
                trTotal.Visible = true;
                TrFees.Visible = true;
                btnCollect.Visible = true;
                Div4.Visible = true;


            }
            else
            {
                objCommon.DisplayMessage(updBank, "No Data available.", this);
                divTransfer.Visible = false;
                btnCollect.Visible = false;
                TrFees.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACD_DCR.ShowAll -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        Panel1.Visible = true;
    }

    protected void lvFeeTransfer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // Clear();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

          //  Clear();
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) ", "DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + ddlDegree.SelectedValue, "B.LONGNAME");
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void rdbTransferType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RdoClear();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnCollect_Click1(object sender, EventArgs e)
    {
        if (Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"] + "_LEDERHEAD", "count(*)", "RECIEPT_TYPE='" + ddlReceipt.SelectedValue + "' AND DEGREENO=" + ddlDegree.SelectedValue + "  AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + " and  BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue))) > 0)
        {
            objCommon.FillDropDownList(ddlLedger, "ACC_" + Session["comp_code"] + "_PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO in (1,2)", "PARTY_NAME");
            ddlLedger.SelectedValue = objCommon.LookUp("ACC_FEE_" + Session["comp_code"] + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlReceipt.SelectedValue + "' AND DEGREENO=" + ddlDegree.SelectedValue + "  AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + " and  BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue));

            string uanos = string.Empty;
            ViewState["uanos"] = null;
            ViewState["lblDCRVoucherNo"] = null;
            lstFees.DataSource = null;
            lstFees.DataBind();
            lblTotal.Text = "0";
            int i = 0;
            foreach (ListViewDataItem lvItem in lvFeeTransfer.Items)
            {
                CheckBox chkAccept = lvItem.FindControl("chkFeesTransfer") as CheckBox;
                Label lblDCRVoucherNo = lvItem.FindControl("lblDCRVoucherNo") as Label;
                if (chkAccept.Checked == true)
                {
                    ViewState["uanos"] += chkAccept.ToolTip + "$";
                    ViewState["lblDCRVoucherNo"] += lblDCRVoucherNo.Text + ",";
                    i++;
                }
                if (i > 200)
                {
                    objCommon.DisplayMessage(updBank, "Please select maximum 200 Receipts", this);
                    return;
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
                DataSet ds = objFRC.BindRefundAmountFeeHeadWise(_CCMS, ddlReceipt.SelectedValue.ToString(), ViewState["uanos"].ToString());
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
                        Label lblFeeHeadsNo = lvfees.FindControl("lblFeeHeadsNo") as Label;
                        Label lblFeeHeads = lvfees.FindControl("lblFeeHeads") as Label;
                        Label lblAmount = lvfees.FindControl("lblAmount") as Label;
                        double Amount = Convert.ToDouble(lblTotal.Text);
                        if (lblFeeHeadsNo.Text == "CF")
                        {
                            Amount = Amount- Convert.ToDouble(lblAmount.Text);
                        }
                        else
                        {
                            Amount = Amount + Convert.ToDouble(lblAmount.Text);
                        }

                       
                        lblTotal.Text = Amount.ToString();

                      


                       
                    }
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(updBank, "Please Map Ledger First", this);
            return;
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["uanos"] == null || ViewState["uanos"].ToString() == string.Empty || ViewState["uanos"].ToString() == "")
            {
                objCommon.DisplayMessage(updBank, "Please select Student Name", this);
                lstFees.DataSource = null;
                lstFees.DataBind();
              //  lblTotal.Text = "0";
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
                    drFees["amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(lblAmount.Text.ToString().Trim())));   //(lblAmount.Text);
                    if (lblFeeHeadsNo.Text == "CF")
                    {
                    drFees["TRANS"] = "Cr";
                    }
                    else
                    {
                        drFees["TRANS"] = "Dr";
                    }

                    dtFees.Rows.Add(drFees);
                }

                if (Session["dtFees"] == null)
                    Session["dtFees"] = dtFees;
                objAccountTrans.COMPANY_CODE = Session["comp_code"].ToString();
                objAccountTrans.OPARTY_NO = ddlLedger.SelectedValue;
                objAccountTrans.TRANSACTION_TYPE = "P";
                objAccountTrans.TRAN = "Cr";
                objAccountTrans.DEGREE_NO = Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString()));
                objAccountTrans.CBTYPE = ddlReceipt.SelectedValue;
                objAccountTrans.USER = Session["userno"].ToString();
                objAccountTrans.PARTICULARS = "Fees Refund from Voucher No:-" + ViewState["lblDCRVoucherNo"].ToString().TrimEnd(',');
                string transdate = Convert.ToDateTime(txtVoucherDate.Text).ToString("dd-MMM-yyyy");
                objAccountTrans.TRANSACTION_DATE = Convert.ToDateTime(transdate);

                ret = objFRC.RefundfeesStudentWise(connectionString, objAccountTrans, (DataTable)Session["dtFees"], Convert.ToDouble(lblTotal.Text), ViewState["uanos"].ToString(), 0, Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddsem.SelectedIndex), Convert.ToInt32(ddlbatch.SelectedValue));

                if (ret == 1)
                {
                    objCommon.DisplayMessage(updBank, "Fees Refund Successful.", this);
                    Clear();
                    PopulateDegreeDropdown();
                    PopulateReceiptTypeDropdown();
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
}