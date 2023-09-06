//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : FIXED DEPOSITE ENTRY                                                    
// CREATION DATE : 20-FEB-2019                                               
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
using System.Web;
using System.Xml;
using IITMS.NITPRM;

public partial class ACCOUNT_FixedDepositTransaction : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();
    AccountTransactionController objATC = new AccountTransactionController();

    FixedDepositeClass objFDC = new FixedDepositeClass();

    public static string StrVno = "abc/111";
    public int _idnoEmp;

   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string aa = flupld.FileName;
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                ViewState["action"] = "add";
                Session["dt"] = null;
                ViewState["FDID"] = "0";                

                string serialNo = objCommon.LookUp("ACC_FIXED_DEPOSIT_ENTRY", "ISNULL(MAX(FD_SRNO),0) + 1", "");
                ViewState["FD_SrNo"] = serialNo.ToString();
                txtserialNo.Text = serialNo.ToString();

                BindCompany();
                BindFDRList();

                string Comp_code = "";
                if (Convert.ToInt32(ddlCompany.SelectedValue) > 0)
                {
                    int companyno = Convert.ToInt32(ddlCompany.SelectedValue);
                    Comp_code = objCommon.LookUp("ACC_COMPANY", "COMPANY_CODE", "DROP_FLAG = 'N' AND COMPANY_NO = " + companyno);
                }
                Session["FDR_COMP_CODE"] = Comp_code.ToString();

                pnlDetails.Visible = false;
                pnlOtherDetils.Visible = false;
                dvButtons.Visible = false;
                pnlList.Visible = true;

                if (Session["serviceIdNo"] != null)
                {
                    _idnoEmp = Convert.ToInt32(Session["userno"].ToString().Trim());
                }
            }
        }
       
    }

    private void BindCompany()
    {
        if (Convert.ToInt32(Session["userno"]) == 1)
        {
            objCommon.FillDropDownList(ddlCompany, "ACC_COMPANY", "COMPANY_NO", "COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(20)) + ' - ' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(20)) COMPANY_NAME", "DROP_FLAG = 'N'", "COMPANY_NO");
        }
        else
        {
            objCommon.FillDropDownList(ddlCompany, "ACC_COMPANY", "COMPANY_NO", "COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(20)) + ' - ' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(20)) COMPANY_NAME", "DROP_FLAG = 'N'", "COMPANY_NO");
        }
    }

    private void BindFDRList()
    {
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("ACC_FIXED_DEPOSIT_ENTRY", "CONVERT(NVARCHAR(20),INVESTMENT_DATE,106) INV_DATE,CONVERT(NVARCHAR(20),MATURITY_DATE,106) MAT_DATE", "ISNULL(ISCLOSED,0) ISCLOSED,ISNULL(FD_STATUS,'') FD_STATUS,*", "", "FD_SRNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptFDRList.DataSource = ds.Tables[0];
                rptFDRList.DataBind();
            }
            else
            {
                rptFDRList.DataSource = null;
                rptFDRList.DataBind();
            }

            foreach (RepeaterItem items in rptFDRList.Items)
            {
                ImageButton btnFDREdit = (ImageButton)items.FindControl("btnFDREdit");
                Button btnFDRClose = (Button)items.FindControl("btnFDRClose");
                HiddenField hdnIsclosed = (HiddenField)items.FindControl("hdnIsclosed");
                HiddenField hdnFDStatus = (HiddenField)items.FindControl("hdnFDStatus");

                if (Convert.ToInt32(hdnIsclosed.Value) == 1 && hdnFDStatus.Value.ToString() == "C")
                {
                    btnFDREdit.Enabled = false;
                    btnFDRClose.Enabled = false;
                    btnFDRClose.Text = "Closed";
                    btnFDRClose.CssClass = "btn btn-danger";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool CheckDuplicateRow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EMAILID"].ToString().ToLower() == value.ToLower())
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.checkDuplicateAdministrationRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtMobileNo.Text == "" && txtEmailId.Text == "")
        {
            objCommon.DisplayMessage(this.Page, "Please Enter atleast anyone(Email or Mobile No.)", this.Page);
            return;
        }

        DataTable dt = new DataTable();
        dt = new DataTable();
        if (Session["dt"] != null)
            dt = (DataTable)Session["dt"];



        DataTable dtDup = (DataTable)Session["dt"];

        if (CheckDuplicateRow(dtDup, txtEmailId.Text.Trim()))
        {
            rptOtherDetails.DataSource = dtDup;
            rptOtherDetails.DataBind();
            txtEmailId.Text=string.Empty;
            MessageBox("This Email Id Is Already Exist.");
            return;
        }

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

        dr["EMAILID"] = txtEmailId.Text.Trim().ToString();
        dr["MOBILENO"] = txtMobileNo.Text.Trim().ToString();

        dt.Rows.Add(dr);

        rptOtherDetails.DataSource = dt;
        rptOtherDetails.DataBind();

        DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
        ViewState["SRNO"] = Convert.ToInt32(lastRow["SRNO"]);

        Session["dt"] = dt;
        txtEmailId.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        ViewState["SerialNo"] = null;
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
            ds = objAutocomplete.GetFDRLedger(prefixText, HttpContext.Current.Session["FDR_Comp_Code"].ToString());
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

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAcc(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetFD_BankLedger(prefixText);
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

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Comp_code = "";
        if (Convert.ToInt32(ddlCompany.SelectedValue) > 0)
        {
            int companyno = Convert.ToInt32(ddlCompany.SelectedValue);
            Comp_code = objCommon.LookUp("ACC_COMPANY", "COMPANY_CODE", "DROP_FLAG = 'N' AND COMPANY_NO = " + companyno);
        }
        Session["FDR_COMP_CODE"] = Comp_code.ToString();

        txtBankLedger.Text = string.Empty;
        txtLedger.Text = string.Empty;
    }

    public bool CheckFileTypeAndSize()
    {
        try
        {
            //if (fu.HasFile)
            if (flupld.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flupld.FileName);
                HttpPostedFile file = flupld.PostedFile;
                if ((file != null) && (file.ContentLength > 0))
                {
                    int iFileSize = file.ContentLength;
                    if (iFileSize > 5242880)  // 40kb 5120
                    {
                        MessageBox("Please Select valid document file(upto 5 MB)");
                        return false;
                    }
                }

                string[] ValidExt = { ".PDF", ".DOC", ".DOCX", ".Pdf", ".pdf", ".doc", ".docx", ".JPEG", ".JPG", ".jpg", ".jpeg" };
                Boolean valid = false;
                foreach (string vext in ValidExt)
                {
                    if (ext.ToUpper() == vext)
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                {
                    MessageBox("Please Select valid document file(e.g. pdf,doc)");
                    return false;
                }
                else
                {

                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = CheckFileTypeAndSize();
            if (flag == false)
            {
                return;
            }

            DataTable dtDetails = new DataTable();
            DataTable dtnew = new DataTable();
            this.Enable();

            if (!dtDetails.Columns.Contains("EMAILID"))
                dtDetails.Columns.Add("EMAILID");
            if (!dtDetails.Columns.Contains("MOBILENO"))
                dtDetails.Columns.Add("MOBILENO");

            if (txtserialNo.Text == "" || txtserialNo.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Serial Number", this.Page);
                txtserialNo.Focus();
                return;
            }
            if (ddlCompany.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.Page, "Please Select Company", this.Page);
                ddlCompany.Focus();
                return;
            }
            if (txtBankLedger.Text == "" || txtBankLedger.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Bank Ledger", this.Page);
                txtBankLedger.Focus();
                return;
            }
            else
            {
                string[] bank = txtBankLedger.Text.Split('*');
                if (bank.Length != 2)
                {
                    objCommon.DisplayMessage(this.Page, "Invalid Selected Bank Ledger", this.Page);
                    txtBankLedger.Focus();
                    return;
                }
            }
            if (txtLedger.Text == "" || txtLedger.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Ledger", this.Page);
                txtLedger.Focus();
                return;
            }
            else
            {
                string[] Ledger = txtLedger.Text.Split('*');
                if (Ledger.Length != 2)
                {
                    objCommon.DisplayMessage(this.Page, "Invalid Selected Ledger", this.Page);
                    txtLedger.Focus();
                    return;
                }
            }
            if (txtCustomerId.Text == "" || txtCustomerId.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Customer Id", this.Page);
                txtCustomerId.Focus();
                return;
            }
            if (txtFDRNo.Text == "" || txtFDRNo.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter FDR No.", this.Page);
                txtFDRNo.Focus();
                return;
            }
            if (txtInvestmentDate.Text == "" || txtInvestmentDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Investment Date", this.Page);
                txtInvestmentDate.Focus();
                return;
            }
            if (txtMaturityDate.Text == "" || txtMaturityDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Maturity Date", this.Page);
                txtMaturityDate.Focus();
                return;
            }
            if (txtInvestedAmt.Text == "" || txtInvestedAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Invested Amount", this.Page);
                txtInvestedAmt.Focus();
                return;
            }
            if (txtMaturityAmt.Text == "" || txtMaturityAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Maturity Amount", this.Page);
                txtMaturityAmt.Focus();
                return;
            }
            if (txtRateofInterest.Text == "" || txtRateofInterest.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Rate of Interest", this.Page);
                txtRateofInterest.Focus();
                return;
            }
            if (txtPANNo.Text == "" || txtPANNo.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter PAN Number", this.Page);
                txtPANNo.Focus();
                return;
            }

            objFDC.Serial_No = txtserialNo.Text.Trim();
            objFDC.Comp_No = Convert.ToInt32(ddlCompany.SelectedValue);
            objFDC.Comp_code = Session["FDR_COMP_CODE"].ToString();
            objFDC.Bank_Id = Convert.ToInt32(txtBankLedger.Text.Split('*')[1].ToString());
            objFDC.PARTY_NO = Convert.ToInt32(txtLedger.Text.Trim().Split('*')[1].ToString()).ToString();
            objFDC.Customer_Id = txtCustomerId.Text.Trim();
            objFDC.FDR_NO = txtFDRNo.Text.Trim();

            if ((ViewState["action"].ToString() == "add" && ViewState["FD_STATUS"].ToString() == "addnew") || ViewState["FD_STATUS"].ToString() == "renew")
            {
                int FDR_No = Convert.ToInt32(objCommon.LookUp("ACC_FIXED_DEPOSIT_ENTRY", "Count(*)", "FDR_NO = '" + txtFDRNo.Text.Trim() + "'"));
                if (FDR_No > 0)
                {
                    objCommon.DisplayMessage(this.Page, "FDR Number is Already Exist!", this.Page);
                    txtFDRNo.Focus();
                    return;
                }
            }

            objFDC.Investment_Date = Convert.ToDateTime(txtInvestmentDate.Text.Trim()).ToString("dd-MMM-yyyy");
            objFDC.Maturity_Date = Convert.ToDateTime(txtMaturityDate.Text.Trim()).ToString("dd-MMM-yyyy");
            objFDC.Invested_Amt = Convert.ToDouble(txtInvestedAmt.Text.Trim());
            objFDC.Maturity_Amt = Convert.ToDouble(txtMaturityAmt.Text.Trim());
            objFDC.RateOfIntrest = Convert.ToDouble(txtRateofInterest.Text.Trim());
            objFDC.PAN_No = txtPANNo.Text.Trim().ToString();
            objFDC.Period_From_Date = Convert.ToDateTime(txtPeriodFrom.Text.Trim()).ToString("dd-MMM-yyyy");
            objFDC.Period_To_Date = Convert.ToDateTime(txtPeriodTo.Text.Trim()).ToString("dd-MMM-yyyy");
            objFDC.Scheme = txtScheme.Text.Trim().ToString();
            objFDC.Account_Holder = txtAccHolder.Text.Trim().ToString();
            objFDC.Address = txtAddress.Text.Trim().ToString();
            objFDC.Nomination_For = txtNomination.Text.Trim().ToString();
            objFDC.Remark = txtRemark.Text.ToString();

            objFDC.BankAddress = txtBankAddress.Text.ToString();
            objFDC.Fd_Duration = txtFDDuration.Text.ToString();
            objFDC.Reference = txtReference.Text.ToString();
            objFDC.RegisterBookNo = txtRegisterBookNo.Text.ToString();
            objFDC.FdWithdrawnAmount = Convert.ToDouble(txtFDWithDrawAmt.Text.Trim());
            objFDC.AccumulatedInterest = Convert.ToDouble(txtInterestAccumlated.Text.Trim());
            if (flupld.HasFile)
            {
                objFDC.FdAdviseAttachment = Convert.ToString(flupld.PostedFile.FileName.ToString());
            }
            else
            {
                if (ViewState["attachment"] != null)
                {
                    objFDC.FdAdviseAttachment = ViewState["attachment"].ToString();
                }
                else
                {
                    objFDC.FdAdviseAttachment = string.Empty;
                }

            }

            DataTable dtContact = new DataTable();
            dtContact = (DataTable)Session["dt"];

            if (ViewState["action"].ToString() == "edit")
            {
                objFDC.FDID = Convert.ToInt32(ViewState["FDID"].ToString());
            }
            else if (ViewState["FD_STATUS"].ToString() == "close")
            {
                objFDC.FDID = Convert.ToInt32(ViewState["FDID"].ToString());
            }
            else
            {
                objFDC.FDID = 0;
            }

            if (rptOtherDetails.Items.Count > 0)
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
                objCommon.DisplayMessage(this.Page, "Please Add atleast one Contact Details(Email & Phone No.)", this.Page);
                txtEmailId.Focus();
                return;
            }

            int ret = 0;
            if (ViewState["FD_STATUS"].ToString() == "close")
            {
                objFDC.IsClosed = 1;
            }
            else
            {
                objFDC.IsClosed = 0;
            }
            ret = objAVC.InsertUpdateFD(objFDC, dtDetails, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["colcode"]));

            if (ViewState["action"].ToString() == "edit")
            {
                objAVC.update_upload("FIXED_DEPOSIT", objFDC.FDID, ViewState["attachment"].ToString(), _idnoEmp, "ACC_", flupld);
            }
            else
            {
                objAVC.upload_new_files("FIXED_DEPOSIT", _idnoEmp, "FDID", "ACC_FIXED_DEPOSIT_ENTRY", "ACC_", flupld);
            }
            if (ret == 1)
            {
                string msg = "";
                msg = "Record saved successfully!";
                if (ViewState["FD_STATUS"].ToString() == "renew")
                {
                    InsertVoucherByFD();
                    ViewState["FD_STATUS"] = "addnew";
                    msg = "Record renew successfully!";
                }
                InsertVoucherByFD();
                objCommon.DisplayMessage(this.Page, msg, this.Page);
                BindFDRList();
                Clear();

                pnlDetails.Visible = false;
                pnlOtherDetils.Visible = false;
                dvButtons.Visible = false;
                pnlList.Visible = true;
            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(this.Page, "Record updated successfully!", this.Page);
                BindFDRList();
                Clear();

                pnlDetails.Visible = false;
                pnlOtherDetils.Visible = false;
                dvButtons.Visible = false;
                pnlList.Visible = true;
            }
            else if (ret == 3)
            {
                InsertVoucherByFD();
                objCommon.DisplayMessage(this.Page, "Record Closed successfully!", this.Page);
                BindFDRList();
                Clear();

                pnlDetails.Visible = false;
                pnlOtherDetils.Visible = false;
                dvButtons.Visible = false;
                pnlList.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dtDetails = new DataTable();
    //        DataTable dtnew = new DataTable();
    //        this.Enable();

    //        if (!dtDetails.Columns.Contains("EMAILID"))
    //            dtDetails.Columns.Add("EMAILID");
    //        if (!dtDetails.Columns.Contains("MOBILENO"))
    //            dtDetails.Columns.Add("MOBILENO");

    //        if (txtserialNo.Text == "" || txtserialNo.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Serial Number", this.Page);
    //            txtserialNo.Focus();
    //            return;
    //        }
    //        if (ddlCompany.SelectedValue == "0")
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Select Company", this.Page);
    //            ddlCompany.Focus();
    //            return;
    //        }
    //        if (txtBankLedger.Text == "" || txtBankLedger.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Bank Ledger", this.Page);
    //            txtBankLedger.Focus();
    //            return;
    //        }
    //        else
    //        {
    //            string[] bank = txtBankLedger.Text.Split('*');
    //            if (bank.Length != 2)
    //            {
    //                objCommon.DisplayMessage(this.Page, "Invalid Selected Bank Ledger", this.Page);
    //                txtBankLedger.Focus();
    //                return;
    //            }
    //        }
    //        if (txtLedger.Text == "" || txtLedger.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Ledger", this.Page);
    //            txtLedger.Focus();
    //            return;
    //        }
    //        else
    //        {
    //            string[] Ledger = txtLedger.Text.Split('*');
    //            if (Ledger.Length != 2)
    //            {
    //                objCommon.DisplayMessage(this.Page, "Invalid Selected Ledger", this.Page);
    //                txtLedger.Focus();
    //                return;
    //            }
    //        }
    //        if (txtCustomerId.Text == "" || txtCustomerId.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Customer Id", this.Page);
    //            txtCustomerId.Focus();
    //            return;
    //        }
    //        if (txtFDRNo.Text == "" || txtFDRNo.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter FDR No.", this.Page);
    //            txtFDRNo.Focus();
    //            return;
    //        }
    //        if (txtInvestmentDate.Text == "" || txtInvestmentDate.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Investment Date", this.Page);
    //            txtInvestmentDate.Focus();
    //            return;
    //        }
    //        if (txtMaturityDate.Text == "" || txtMaturityDate.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Maturity Date", this.Page);
    //            txtMaturityDate.Focus();
    //            return;
    //        }
    //        if (txtInvestedAmt.Text == "" || txtInvestedAmt.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Invested Amount", this.Page);
    //            txtInvestedAmt.Focus();
    //            return;
    //        }
    //        if (txtMaturityAmt.Text == "" || txtMaturityAmt.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Maturity Amount", this.Page);
    //            txtMaturityAmt.Focus();
    //            return;
    //        }
    //        if (txtRateofInterest.Text == "" || txtRateofInterest.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter Rate of Interest", this.Page);
    //            txtRateofInterest.Focus();
    //            return;
    //        }
    //        if (txtPANNo.Text == "" || txtPANNo.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Enter PAN Number", this.Page);
    //            txtPANNo.Focus();
    //            return;
    //        }

    //        objFDC.Serial_No = txtserialNo.Text.Trim();
    //        objFDC.Comp_No = Convert.ToInt32(ddlCompany.SelectedValue);
    //        objFDC.Comp_code = Session["FDR_COMP_CODE"].ToString();
    //        objFDC.Bank_Id = Convert.ToInt32(txtBankLedger.Text.Split('*')[1].ToString());
    //        objFDC.PARTY_NO = Convert.ToInt32(txtLedger.Text.Trim().Split('*')[1].ToString()).ToString();
    //        objFDC.Customer_Id = txtCustomerId.Text.Trim();
    //        objFDC.FDR_NO = txtFDRNo.Text.Trim();

    //        if ((ViewState["action"].ToString() == "add" && ViewState["FD_STATUS"].ToString() == "addnew") || ViewState["FD_STATUS"].ToString() == "renew")
    //        {
    //            int FDR_No = Convert.ToInt32(objCommon.LookUp("ACC_FIXED_DEPOSIT_ENTRY", "Count(*)", "FDR_NO = '" + txtFDRNo.Text.Trim() + "'"));
    //            if (FDR_No > 0)
    //            {
    //                objCommon.DisplayMessage(this.Page, "FDR Number is Already Exist!", this.Page);
    //                txtFDRNo.Focus();
    //                return;
    //            }
    //        }

    //        objFDC.Investment_Date = Convert.ToDateTime(txtInvestmentDate.Text.Trim()).ToString("dd-MMM-yyyy");
    //        objFDC.Maturity_Date = Convert.ToDateTime(txtMaturityDate.Text.Trim()).ToString("dd-MMM-yyyy");
    //        objFDC.Invested_Amt = Convert.ToDouble(txtInvestedAmt.Text.Trim());
    //        objFDC.Maturity_Amt = Convert.ToDouble(txtMaturityAmt.Text.Trim());
    //        objFDC.RateOfIntrest = Convert.ToDouble(txtRateofInterest.Text.Trim());
    //        objFDC.PAN_No = txtPANNo.Text.Trim().ToString();
    //        objFDC.Period_From_Date = Convert.ToDateTime(txtPeriodFrom.Text.Trim()).ToString("dd-MMM-yyyy");
    //        objFDC.Period_To_Date = Convert.ToDateTime(txtPeriodTo.Text.Trim()).ToString("dd-MMM-yyyy");
    //        objFDC.Scheme = txtScheme.Text.Trim().ToString();
    //        objFDC.Account_Holder = txtAccHolder.Text.Trim().ToString();
    //        objFDC.Address = txtAddress.Text.Trim().ToString();
    //        objFDC.Nomination_For = txtNomination.Text.Trim().ToString();
    //        objFDC.Remark = txtRemark.Text.ToString();

    //        DataTable dtContact = new DataTable();
    //        dtContact = (DataTable)Session["dt"];

    //        if (ViewState["action"].ToString() == "edit")
    //        {
    //            objFDC.FDID = Convert.ToInt32(ViewState["FDID"].ToString());
    //        }
    //        else if (ViewState["FD_STATUS"].ToString() == "close")
    //        {
    //            objFDC.FDID = Convert.ToInt32(ViewState["FDID"].ToString());
    //        }
    //        else
    //        {
    //            objFDC.FDID = 0;
    //        }

    //        if (rptOtherDetails.Items.Count > 0)
    //        {
    //            dtnew = (DataTable)Session["dt"];
    //            for (int i = 0; i < dtnew.Rows.Count; i++)
    //            {
    //                DataRow drDetails = dtDetails.NewRow();

    //                drDetails["EMAILID"] = dtnew.Rows[i]["EMAILID"].ToString();
    //                drDetails["MOBILENO"] = dtnew.Rows[i]["MOBILENO"].ToString();

    //                dtDetails.Rows.Add(drDetails);
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.Page, "Please Add atleast one Contact Details(Email & Phone No.)", this.Page);
    //            txtEmailId.Focus();
    //            return;
    //        }

    //        int ret = 0;
    //        if (ViewState["FD_STATUS"].ToString() == "close")
    //        {
    //            objFDC.IsClosed = 1;
    //        }
    //        else
    //        {
    //            objFDC.IsClosed = 0;
    //        }
    //        ret = objAVC.InsertUpdateFD(objFDC, dtDetails, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["colcode"]));

    //        if (ret == 1)
    //        {
    //            string msg = "";
    //            msg = "Record saved successfully!";
    //            if (ViewState["FD_STATUS"].ToString() == "renew")
    //            {
    //                InsertVoucherByFD();
    //                ViewState["FD_STATUS"] = "addnew";
    //                msg = "Record renew successfully!";
    //            }
    //            InsertVoucherByFD();
    //            objCommon.DisplayMessage(this.Page, msg, this.Page);
    //            BindFDRList();
    //            Clear();

    //            pnlDetails.Visible = false;
    //            pnlOtherDetils.Visible = false;
    //            dvButtons.Visible = false;
    //            pnlList.Visible = true;
    //        }
    //        else if (ret == 2)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Record updated successfully!", this.Page);
    //            BindFDRList();
    //            Clear();

    //            pnlDetails.Visible = false;
    //            pnlOtherDetils.Visible = false;
    //            dvButtons.Visible = false;
    //            pnlList.Visible = true;
    //        }
    //        else if (ret == 3)
    //        {
    //            InsertVoucherByFD();
    //            objCommon.DisplayMessage(this.Page, "Record Closed successfully!", this.Page);
    //            BindFDRList();
    //            Clear();

    //            pnlDetails.Visible = false;
    //            pnlOtherDetils.Visible = false;
    //            dvButtons.Visible = false;
    //            pnlList.Visible = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void Clear()
    {
        string serialNo = objCommon.LookUp("ACC_FIXED_DEPOSIT_ENTRY", "ISNULL(MAX(FD_SRNO),0) + 1", "");
        ViewState["FD_SrNo"] = serialNo.ToString();
        txtserialNo.Text = serialNo.ToString();

        BindCompany();
        txtBankLedger.Text = string.Empty;
        txtLedger.Text = string.Empty;
        txtCustomerId.Text = string.Empty;
        txtFDRNo.Text = string.Empty;
        txtInvestedAmt.Text = string.Empty;
        txtMaturityDate.Text = string.Empty;
        txtInvestmentDate.Text = string.Empty;
        txtMaturityAmt.Text = string.Empty;
        txtRateofInterest.Text = string.Empty;
        txtPANNo.Text = string.Empty;
        txtPeriodFrom.Text = string.Empty;
        txtPeriodTo.Text = string.Empty;
        txtScheme.Text = string.Empty;
        txtAccHolder.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtNomination.Text = string.Empty;
        txtRemark.Text = string.Empty;


        txtBankAddress.Text = string.Empty;
        txtFDDuration.Text = string.Empty;
        txtFDWithDrawAmt.Text = string.Empty;
        txtInterestAccumlated.Text = string.Empty;
        txtReference.Text = string.Empty;
        txtRegisterBookNo.Text = string.Empty;
        ViewState["attachment"] = null;

        rptOtherDetails.DataSource = null;
        rptOtherDetails.DataBind();

        Session["dt"] = null;
        ViewState["FDID"] = "0";
        ddlCompany.Enabled = true;

        Session["FDR_COMP_CODE"] = string.Empty;
        ViewState["FD_STATUS"] = "addnew";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmailId.Text != string.Empty || txtMobileNo.Text != string.Empty)
        {
            objCommon.DisplayMessage(this.Page, "Sorry! Cannot Edit, already one record is selected!", this.Page);
            return;
        }

        ImageButton btnFDRContactEdit = sender as ImageButton;

        DataTable dtTran = new DataTable();
        if (Session["dt"] != null)
            dtTran = (DataTable)Session["dt"];

        DataView dv = dtTran.DefaultView;
        dv.RowFilter = "SRNO=" + btnFDRContactEdit.CommandArgument;
        DataTable dtRow = dv.ToTable();
        if (dtRow.Rows[0]["SRNO"].ToString() == "1")
        {
            ViewState["SerialNo"] = 1;
            txtEmailId.Text = dtRow.Rows[0]["EMAILID"].ToString();
            txtMobileNo.Text = dtRow.Rows[0]["MOBILENO"].ToString();
        }
        else
        {
            ViewState["SerialNo"] = dtRow.Rows[0]["SRNO"].ToString();
            txtEmailId.Text = dtRow.Rows[0]["EMAILID"].ToString();
            txtMobileNo.Text = dtRow.Rows[0]["MOBILENO"].ToString();
        }

        dv = null;
        DataView dvrow1 = dtTran.DefaultView;
        dvrow1.RowFilter = "SRNO<>" + btnFDRContactEdit.CommandArgument;
        dtTran = dvrow1.ToTable();
        rptOtherDetails.DataSource = dtTran;
        rptOtherDetails.DataBind();
        Session["dt"] = dtTran;
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnContactDelete = sender as ImageButton;

        DataTable dtTran = new DataTable();
        if (Session["dt"] != null)
            dtTran = (DataTable)Session["dt"];

        DataView dvrow1 = dtTran.DefaultView;
        dvrow1.RowFilter = "SRNO<>" + btnContactDelete.CommandArgument;
        dtTran = dvrow1.ToTable();
        rptOtherDetails.DataSource = dtTran;
        rptOtherDetails.DataBind();

        //if (rptOtherDetails.Items.Count > 1)
        //{
        //    DataRow lastRow = dtTran.Rows[dtTran.Rows.Count - 1];
        //    ViewState["SRNO"] = Convert.ToInt32(lastRow["SRNO"]);
        //}

        Session["dt"] = dtTran;
    }

    private void InsertVoucherByFD()
    {
        try
        {
            int PaymentTypeNo = 0;
            HiddenField hdnPartyManual = new HiddenField();

            ViewState["VoucherNo"] = string.Empty;

            hdnPartyManual.Value = objCommon.LookUp("ACC_" + Session["FDR_COMP_CODE"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtBankLedger.Text.Trim().Split('*')[1] + "'");
            string party_no = hdnPartyManual.Value.ToString();
            //if (ddlTranType.SelectedValue != "J")
            //{
            PaymentTypeNo = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["FDR_COMP_CODE"] + "_PARTY", "PAYMENT_TYPE_NO", "PARTY_NO=" + Convert.ToInt16(party_no)));
            //}
            if (ViewState["FD_STATUS"].ToString() == "close")
            {
                ViewState["PaymentType"] = "R";
                ViewState["PaymentTypeNo"] = 2;
            }
            else if (ViewState["FD_STATUS"].ToString() == "renew")
            {
                ViewState["PaymentType"] = "J";
                ViewState["PaymentTypeNo"] = 3;
            }
            else
            {
                ViewState["PaymentType"] = "P";
                ViewState["PaymentTypeNo"] = 2;
            }

            string save_date;
            if (ViewState["FD_STATUS"].ToString() == "close")
                save_date = DateTime.Now.ToString("dd/MM/yyyy");
            else if (ViewState["FD_STATUS"].ToString() == "renew")
                save_date = Convert.ToDateTime(ViewState["TRANS_DATE"]).ToString("dd/MM/yyyy");
            else
                save_date = txtInvestmentDate.Text;
            AccountConfigurationController objvch = new AccountConfigurationController();
            DataSet dsVoucher = new DataSet();
            int voucherno = 0;

            if (DateTime.Compare(Convert.ToDateTime(save_date), DateTime.Now.Date) == 1)
            {
                objCommon.DisplayUserMessage(this.Page,"Can Not Make Future Entry.", this);
                //txtInvestmentDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtInvestmentDate.Focus();
                return;
            }

            FinanceCashBookController objCBC = new FinanceCashBookController();
            int Comp_No = Convert.ToInt32(objCommon.LookUp("ACC_COMPANY", "COMPANY_NO", "COMPANY_CODE = '" + Session["FDR_COMP_CODE"].ToString() + "'"));
            DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Comp_No.ToString().Trim());
            if (dtr.Read())
            {
                Session["FDR_comp_code"] = dtr["COMPANY_CODE"];
                Session["FDR_fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
                Session["FDR_fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
                Session["FDR_fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            }
            dtr.Close();

            if (DateTime.Compare(Convert.ToDateTime(save_date), Convert.ToDateTime(Session["FDR_fin_date_to"])) == 1)
            {
                objCommon.DisplayUserMessage(this.Page, "Transaction Should Be In The Financial Year Range. ", this);
                //txtInvestmentDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtInvestmentDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(Session["FDR_fin_date_from"]), Convert.ToDateTime(save_date)) == 1)
            {
                objCommon.DisplayUserMessage(this.Page, "Transaction Should Be In The Financial Year Range. ", this);
                //txtInvestmentDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtInvestmentDate.Focus();
                return;
            }


            XmlDocument objXMLDoc = new XmlDocument();

            //ReadXML("N");
            XmlDeclaration xmlDeclaration = objXMLDoc.CreateXmlDeclaration("1.0", null, null);

            // Create the root element
            XmlElement rootNode = objXMLDoc.CreateElement("tables");
            objXMLDoc.InsertBefore(xmlDeclaration, objXMLDoc.DocumentElement);
            objXMLDoc.AppendChild(rootNode);

            XmlElement objElement = objXMLDoc.CreateElement("Table");
            XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
            XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");
            XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
            XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");
            XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
            XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");

            XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
            AmtWithoutGST.InnerText = "0";

            XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
            GSTPercent.InnerText = "0";

            XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
            IsGSTApplicable.InnerText = "0";

            XmlElement Section = objXMLDoc.CreateElement("SECTION");
            Section.InnerText = "0";

            XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
            if (txtAccHolder.Text == "" || txtAccHolder.Text == string.Empty)
                PARTY_NAME.InnerText = "-";
            else
                PARTY_NAME.InnerText = txtAccHolder.Text.ToString();

            XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
            if (txtPANNo.Text == "" || txtPANNo.Text == string.Empty)
                PAN_NO.InnerText = "-";
            else
                PAN_NO.InnerText = txtPANNo.Text.ToString();

            XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
            if (txtNomination.Text == "" || txtNomination.Text == string.Empty)
                NATURE_SERVICE.InnerText = "-";
            else
                NATURE_SERVICE.InnerText = txtNomination.Text.ToString();

            SUBTR_NO.InnerText = "0";

            if (txtInvestmentDate.Text.ToString().Trim() != "")
            {
                if (ViewState["FD_STATUS"].ToString() == "close")
                {
                    DateTime TranDate = Convert.ToDateTime(DateTime.Now.ToString());
                    TRANSACTION_DATE.InnerText = TranDate.ToString("dd-MMM-yyyy");
                    ViewState["TRANDATE"] = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                }
                else if (ViewState["FD_STATUS"].ToString() == "renew")
                {
                    DateTime TranDate = Convert.ToDateTime(ViewState["TRANS_DATE"].ToString());
                    TRANSACTION_DATE.InnerText = TranDate.ToString("dd-MMM-yyyy");
                    ViewState["TRANDATE"] = Convert.ToDateTime(ViewState["TRANS_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    DateTime TranDate = Convert.ToDateTime(txtInvestmentDate.Text);
                    TRANSACTION_DATE.InnerText = TranDate.ToString("dd-MMM-yyyy");
                    ViewState["TRANDATE"] = Convert.ToDateTime(txtInvestmentDate.Text);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter The Investment Date.", this);
                txtInvestmentDate.Focus();
                return;
            }

            XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
            if (ViewState["FD_STATUS"].ToString() == "close")
            {
                TRANSACTION_TYPE.InnerText = "R";
            }
            else if (ViewState["FD_STATUS"].ToString() == "renew")
            {
                TRANSACTION_TYPE.InnerText = "J";
            }
            else
            {
                TRANSACTION_TYPE.InnerText = "P";
            }

            HiddenField hdnOpParty = new HiddenField();
            HiddenField hdnparty = new HiddenField();
            if (ViewState["FD_STATUS"].ToString() == "renew")
            {
                hdnOpParty.Value = ViewState["OTHERLEDGER"].ToString().Split('*')[1].ToString();
                OPARTY.InnerText = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                ViewState["OPartyNo"] = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();

                hdnparty.Value = txtLedger.Text.Trim().ToString().Split('*')[1].ToString();
                PARTY_NO.InnerText = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                ViewState["PartyNo"] = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
            }
            else
            {
                hdnOpParty.Value = txtBankLedger.Text.Split('*')[1].ToString();
                if (hdnOpParty != null)
                {
                    OPARTY.InnerText = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                    ViewState["OPartyNo"] = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                }


                if (hdnparty != null)
                {
                    hdnparty.Value = txtLedger.Text.Trim().ToString().Split('*')[1].ToString();
                    PARTY_NO.InnerText = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                    ViewState["PartyNo"] = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                }
                else { return; }
            }

            if (ViewState["FD_STATUS"].ToString() == "close")
            {
                TRAN.InnerText = "Cr";
                AMOUNT.InnerText = txtMaturityAmt.Text.Trim().ToString();
            }
            else if (ViewState["FD_STATUS"].ToString() == "renew")
            {
                TRAN.InnerText = "Dr";
                AMOUNT.InnerText = Convert.ToDouble(ViewState["TRANS_AMT"].ToString()).ToString();
            }
            else
            {
                TRAN.InnerText = "Dr";
                AMOUNT.InnerText = txtInvestedAmt.Text.Trim().ToString();
            }

            XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
            DEGREE_NO.InnerText = "0";

            XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");
            TRANSFER_ENTRY.InnerText = "0";
            XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
            CBTYPE_STATUS.InnerText = "H";
            XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
            CBTYPE.InnerText = "TF";
            XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");
            RECIEPT_PAYMENT_FEES.InnerText = "P";
            XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
            REC_NO.InnerText = "0";
            XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
            XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");

            CHQ_NO.InnerText = "0";
            ViewState["CHQ_NO"] = "0";
            if (ViewState["FD_STATUS"].ToString() == "close")
            {
                CHQ_DATE.InnerText = Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
                ViewState["CHQ_DATE"] = Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
            }
            else if (ViewState["FD_STATUS"].ToString() == "renew")
            {
                CHQ_DATE.InnerText = Convert.ToDateTime(ViewState["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy");
                ViewState["CHQ_DATE"] = Convert.ToDateTime(ViewState["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy");
            }
            else
            {
                CHQ_DATE.InnerText = Convert.ToDateTime(txtInvestmentDate.Text).ToString("dd-MMM-yyyy");
                ViewState["CHQ_DATE"] = Convert.ToDateTime(txtInvestmentDate.Text).ToString("dd-MMM-yyyy");
            }

            XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
            CHALLAN.InnerText = "false";
            XmlElement CAN = objXMLDoc.CreateElement("CAN");
            CAN.InnerText = "false";
            XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
            DCR_NO.InnerText = "0";

            //Commented and Add by Nakul Chawre @28052016 to add cost center
            XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
            CC_ID.InnerText = "0";

            XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
            //if (ddlBudgethead.SelectedValue == "0")
            //{
            BudgetNo.InnerText = "0";
            //}
            //else
            //{
            //    BudgetNo.InnerText = ddlBudgethead.SelectedValue;
            //}

            //Added by Nokhlal Kumar As per requirement to add TDS
            XmlElement IsTDSApplicable = objXMLDoc.CreateElement("IsTDSApplicable");
            IsTDSApplicable.InnerText = "0";

            XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
            CASH_BANK_NO.InnerText = "0";
            XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
            ADVANCE_REFUND_NONE.InnerText = "N";
            XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
            PAGENO.InnerText = "0";
            XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");

            string narration = "";
            if (ViewState["FD_STATUS"].ToString() == "renew")
            {
                narration = "Journal Transaction for FD";
            }
            else
            {
                narration = txtRemark.Text.Replace("'", "''");
            }
            if (txtRemark.Text != string.Empty)
            {
                PARTICULARS.InnerText = narration;
            }
            else
            {
                PARTICULARS.InnerText = "-";
            }

            XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
            COLLEGE_CODE.InnerText = Session["colcode"].ToString();

            XmlElement USER = objXMLDoc.CreateElement("USER");
            USER.InnerText = Session["userno"].ToString().Trim();

            XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
            CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");
            XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
            XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");

            XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
            XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

            XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
            BILL_ID.InnerText = "0";

            XmlElement FDR_ID = objXMLDoc.CreateElement("FDR_ID");
            if (ViewState["FD_STATUS"].ToString() == "renew")
                FDR_ID.InnerText = ViewState["FD_SRNO_RENEW"].ToString();
            else
                FDR_ID.InnerText = txtserialNo.Text.ToString().Trim();

            string voucherNo1 = string.Empty;

            if (objCommon.LookUp("ACC_" + Session["FDR_COMP_CODE"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
            {
                if (ViewState["FD_STATUS"].ToString() == "close")
                {
                    voucherNo1 = objCommon.LookUp("ACC_" + Session["FDR_COMP_CODE"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='R'");
                }
                else if (ViewState["FD_STATUS"].ToString() == "renew")
                {
                    voucherNo1 = objCommon.LookUp("ACC_" + Session["FDR_COMP_CODE"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='J'");
                }
                else
                {
                    voucherNo1 = objCommon.LookUp("ACC_" + Session["FDR_COMP_CODE"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(txtInvestmentDate.Text).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='P'");
                }

                VOUCHER_NO.InnerText = voucherNo1;
                ViewState["VoucherNo"] = voucherNo1;

                if (ViewState["FD_STATUS"].ToString() == "close")
                {
                    STR_VOUCHER_NO.InnerText = Session["FDR_COMP_CODE"].ToString().Trim() + "/R" + voucherNo1;
                }
                else if (ViewState["FD_STATUS"].ToString() == "renew")
                {
                    STR_VOUCHER_NO.InnerText = Session["FDR_COMP_CODE"].ToString().Trim() + "/J" + voucherNo1;
                }
                else
                {
                    STR_VOUCHER_NO.InnerText = Session["FDR_COMP_CODE"].ToString().Trim() + "/P" + voucherNo1;//  txtVoucherNo.Text.ToString().Trim();
                }
            }

            STR_CB_VOUCHER_NO.InnerText = StrVno;

            ProjectId.InnerText = "0";

            HiddenField hdnSubproject = new HiddenField();
            hdnSubproject.Value = "";

            ProjectSubId.InnerText = hdnSubproject.Value == "" ? "0" : hdnSubproject.Value;

            BILL_ID.InnerText = "0";
            IsTDSApplicable.InnerText = "0";
            Section.InnerText = "0";
            IsGSTApplicable.InnerText = "0";
            GSTPercent.InnerText = "0";
            AmtWithoutGST.InnerText = "0";

            objElement.AppendChild(SUBTR_NO);
            objElement.AppendChild(TRANSACTION_DATE);
            objElement.AppendChild(TRANSACTION_TYPE);
            objElement.AppendChild(OPARTY);
            objElement.AppendChild(PARTY_NO);
            objElement.AppendChild(TRAN);
            objElement.AppendChild(AMOUNT);
            objElement.AppendChild(DEGREE_NO);
            objElement.AppendChild(TRANSFER_ENTRY);
            objElement.AppendChild(CBTYPE_STATUS);
            objElement.AppendChild(CBTYPE);
            objElement.AppendChild(RECIEPT_PAYMENT_FEES);
            objElement.AppendChild(REC_NO);
            objElement.AppendChild(CHQ_NO);
            objElement.AppendChild(CHQ_DATE);
            objElement.AppendChild(CHALLAN);
            objElement.AppendChild(CAN);
            objElement.AppendChild(DCR_NO);
            //objElement.AppendChild(IDF_NO);
            objElement.AppendChild(CC_ID);
            objElement.AppendChild(BudgetNo);
            objElement.AppendChild(IsTDSApplicable);
            objElement.AppendChild(AmtWithoutGST);
            objElement.AppendChild(GSTPercent);
            objElement.AppendChild(IsGSTApplicable);
            objElement.AppendChild(Section);
            objElement.AppendChild(PARTY_NAME);
            objElement.AppendChild(PAN_NO);
            objElement.AppendChild(NATURE_SERVICE);
            objElement.AppendChild(CASH_BANK_NO);
            objElement.AppendChild(ADVANCE_REFUND_NONE);
            objElement.AppendChild(PAGENO);
            objElement.AppendChild(PARTICULARS);
            objElement.AppendChild(COLLEGE_CODE);
            objElement.AppendChild(USER);
            objElement.AppendChild(CREATED_MODIFIED_DATE);
            objElement.AppendChild(VOUCHER_NO);
            objElement.AppendChild(STR_VOUCHER_NO);
            objElement.AppendChild(STR_CB_VOUCHER_NO);
            objElement.AppendChild(ProjectId);
            objElement.AppendChild(ProjectSubId);
            objElement.AppendChild(BILL_ID);
            objElement.AppendChild(FDR_ID);
            objXMLDoc.DocumentElement.AppendChild(objElement);

            objXMLDoc = ConsolidateTransactionEntry1(objXMLDoc, ViewState["VoucherNo"].ToString());

            string IsModify = string.Empty;
            int VoucherSqn = 0;
            IsModify = "N";
            VoucherSqn = 0;
            //voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify);
            voucherno = objATC.AddTransactionWithXMLforFIXEDDEPOSIT(objXMLDoc, Session["FDR_COMP_CODE"].ToString().Trim(), IsModify, VoucherSqn, Session["FDR_fin_yr"].ToString().Trim(), "P");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private XmlDocument ConsolidateTransactionEntry1(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        if (ViewState["FD_STATUS"].ToString() == "renew")
        {
            SUBTR_NO.InnerText = "0";
        }
        else
        {
            SUBTR_NO.InnerText = "1";
        }
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");
        //if (chkTDSApplicable.Checked)
        //{
        //    IsGSTApplicable.InnerText = "1";
        //    Section.InnerText = ddlSection.SelectedValue;
        //}
        //else
        //{
        IsGSTApplicable.InnerText = "0";
        Section.InnerText = "0";
        //}

        //Added by Nokhlal Kumar for Party Name and PAN Number......
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtAccHolder.Text == "" || txtAccHolder.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtAccHolder.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPANNo.Text == "" || txtPANNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPANNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNomination.Text == "" || txtNomination.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNomination.Text.Trim().ToString();
        }

        ViewState["BankName"] = txtBankLedger.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = "";
        if (ViewState["FD_STATUS"].ToString() == "renew")
        {
            narration = "Journal Transaction for FD";
        }
        else
        {
            narration = txtRemark.Text.Trim().Replace("'", "''");
        }
        if (txtRemark.Text != string.Empty)
        {
            PARTICULARS.InnerText = narration;
        }
        else
        {
            PARTICULARS.InnerText = "-";
        }

        //OPARTY.InnerText = opartystring.ToString().Trim();
        OPARTY.InnerText = Convert.ToInt32(txtLedger.Text.ToString().Split('*')[1]).ToString();

        if (ViewState["FD_STATUS"].ToString() == "close")
        {
            TRANSACTION_TYPE.InnerText = "R";
            TRAN.InnerText = "Dr";
            TRANSACTION_DATE.InnerText = Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy").Trim();
        }
        else if (ViewState["FD_STATUS"].ToString() == "renew")
        {
            TRANSACTION_TYPE.InnerText = "J";
            TRAN.InnerText = "Cr";
            TRANSACTION_DATE.InnerText = Convert.ToDateTime(ViewState["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy").Trim();
        }
        else
        {
            TRANSACTION_TYPE.InnerText = "P";
            TRAN.InnerText = "Cr";
            TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtInvestmentDate.Text).ToString("dd-MMM-yyyy").Trim();
        }

        //hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();
        if (ViewState["FD_STATUS"].ToString() == "renew")
        {
            PARTY_NO.InnerText = Convert.ToInt32(ViewState["OTHERLEDGER"].ToString().Split('*')[1]).ToString();
        }
        else
        {
            PARTY_NO.InnerText = Convert.ToInt32(txtBankLedger.Text.Split('*')[1]).ToString();
        }
        //if (chkTDSApplicable.Checked)
        //{
        //    AMOUNT.InnerText = (Convert.ToDouble(ViewState["Amount"].ToString()) - Convert.ToDouble(ViewState["TDSAmount"].ToString())).ToString();
        //    AmtWithoutGST.InnerText = txtBillAmt.Text.ToString();
        //    GSTPercent.InnerText = txtTDSPer.Text.ToString();
        //}
        //else
        //{
        if (ViewState["FD_STATUS"].ToString() == "close")
        {
            AMOUNT.InnerText = Convert.ToDouble(txtMaturityAmt.Text.ToString()).ToString();
            STR_VOUCHER_NO.InnerText = Session["FDR_COMP_CODE"].ToString().Trim() + "/R" + voucherno.ToString();
            CHQ_DATE.InnerText = Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
        }
        else if (ViewState["FD_STATUS"].ToString() == "renew")
        {
            AMOUNT.InnerText = Convert.ToDouble(ViewState["TRANS_AMT"].ToString()).ToString();
            STR_VOUCHER_NO.InnerText = Session["FDR_COMP_CODE"].ToString().Trim() + "/J" + voucherno.ToString();
            CHQ_DATE.InnerText = Convert.ToDateTime(ViewState["TRANS_DATE"].ToString()).ToString("dd-MMM-yyyy");
        }
        else
        {
            AMOUNT.InnerText = Convert.ToDouble(txtInvestedAmt.Text.ToString()).ToString();
            STR_VOUCHER_NO.InnerText = Session["FDR_COMP_CODE"].ToString().Trim() + "/P" + voucherno.ToString();
            CHQ_DATE.InnerText = Convert.ToDateTime(txtInvestmentDate.Text).ToString("dd-MMM-yyyy");
        }
        AmtWithoutGST.InnerText = "0";
        GSTPercent.InnerText = "0";
        //}
        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());

        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        // txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";

        CHQ_NO.InnerText = "0";

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";


        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");


        ProjectId.InnerText = "0";
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = "0";

        XmlElement FDR_ID = objXMLDoc.CreateElement("FDR_ID");
        if (ViewState["FD_STATUS"].ToString() == "renew")
        {
            FDR_ID.InnerText = ViewState["FD_SRNO_RENEW"].ToString();
        }
        else
        {
            FDR_ID.InnerText = txtserialNo.Text.ToString().Trim();
        }

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        //objElement.AppendChild(IDF_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);
        objElement.AppendChild(FDR_ID);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ViewState["FD_STATUS"] = "addnew";
        try
        {
            Clear();
            pnlList.Visible = false;
            pnlOtherDetils.Visible = true;
            pnlDetails.Visible = true;
            dvButtons.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnFDREdit_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["action"] = "edit";
        ViewState["FD_STATUS"] = "addnew";
        Session["dt"] = null;
        DataSet dsFD = null;
        try
        {
            ImageButton btnFDREdit = sender as ImageButton;
            int FDR_Id = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());

            ViewState["FDID"] = FDR_Id;

            dsFD = objAVC.GetFDDetails(FDR_Id);
            if (dsFD.Tables[0].Rows.Count > 0)
            {
                string FD_status = dsFD.Tables[0].Rows[0]["FD_STATUS"].ToString();
                int Isclosed = Convert.ToInt32(dsFD.Tables[0].Rows[0]["ISCLOSED"].ToString());
                if (Isclosed == 1 && FD_status == "C")
                {
                    objCommon.DisplayMessage(this.Page, "Sorry! you cannot edit this record, because it is closed already!", this.Page);
                    return;
                }
                txtserialNo.Text = dsFD.Tables[0].Rows[0]["FD_SRNO"].ToString();
                BindCompany();

                ddlCompany.SelectedValue = dsFD.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                Session["FDR_COMP_CODE"] = dsFD.Tables[0].Rows[0]["COMPANY_CODE"].ToString();
                ddlCompany.Enabled = false;

                txtLedger.Text = dsFD.Tables[0].Rows[0]["LEDGER"].ToString();
                txtBankLedger.Text = dsFD.Tables[0].Rows[0]["BANKLEDGER"].ToString();
                txtCustomerId.Text = dsFD.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                txtFDRNo.Text = dsFD.Tables[0].Rows[0]["FDR_NO"].ToString();
                txtInvestmentDate.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["INVESTMENT_DATE"]).ToString("dd/MM/yyyy");
                txtMaturityDate.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["MATURITY_DATE"]).ToString();
                txtInvestedAmt.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["AMT_INVESTED"]).ToString();
                txtMaturityAmt.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["MATURITY_AMT"]).ToString();
                txtRateofInterest.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["ROI"]).ToString();
                txtPANNo.Text = dsFD.Tables[0].Rows[0]["PAN_NO"].ToString();
                txtPeriodFrom.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["PERIOD_FROM"]).ToString();
                txtPeriodTo.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["PERIOD_TO"]).ToString();

                txtScheme.Text = dsFD.Tables[0].Rows[0]["SCHEME"].ToString();
                txtAccHolder.Text = dsFD.Tables[0].Rows[0]["ACCOUNT_HOLDER"].ToString();
                txtAddress.Text = dsFD.Tables[0].Rows[0]["CUSTOMER_ADDRESS"].ToString();
                txtNomination.Text = dsFD.Tables[0].Rows[0]["NOMINATION"].ToString();
                txtRemark.Text = dsFD.Tables[0].Rows[0]["Remark"].ToString();

                txtBankAddress.Text = dsFD.Tables[0].Rows[0]["BANK_ADDRESS"].ToString();
                txtRegisterBookNo.Text = dsFD.Tables[0].Rows[0]["REGISTER_BOOK_NO"].ToString();
                txtReference.Text = dsFD.Tables[0].Rows[0]["REFERENCE"].ToString();
                txtFDWithDrawAmt.Text = dsFD.Tables[0].Rows[0]["FD_WITHDRAWN_AMOUNT"].ToString();
                txtInterestAccumlated.Text = dsFD.Tables[0].Rows[0]["ACCUMULATED_INTEREST"].ToString();
                txtFDDuration.Text = dsFD.Tables[0].Rows[0]["FD_DURATION"].ToString();
                ViewState["attachment"] = dsFD.Tables[0].Rows[0]["FD_ADVISE_ATTACHMENT"].ToString();

                pnlDetails.Visible = true;
                pnlOtherDetils.Visible = true;
                pnlList.Visible = false;
                dvButtons.Visible = true;
            }

            if (dsFD.Tables[1].Rows.Count > 0)
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

                for (int i = 0; i < dsFD.Tables[1].Rows.Count; i++)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["SRNO"] = dsFD.Tables[1].Rows[i]["SRNO"].ToString();
                    drnew["EMAILID"] = dsFD.Tables[1].Rows[i]["EMAILID"].ToString();
                    drnew["MOBILENO"] = dsFD.Tables[1].Rows[i]["MOBILENO"].ToString();

                    dt.Rows.Add(drnew);
                }
                Session["dt"] = dt;
                rptOtherDetails.DataSource = dsFD.Tables[1];
                rptOtherDetails.DataBind();
            }
            else
            {
                rptOtherDetails.DataSource = null;
                rptOtherDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnFDRClose_Click(object sender, EventArgs e)
    {
        ViewState["FD_STATUS"] = "close";
        Session["dt"] = null;
        DataSet dsFD = null;
        try
        {
            Button btnFDRClose = sender as Button;
            int FDR_Id = Convert.ToInt32(btnFDRClose.CommandArgument.ToString());

            ViewState["FDID"] = FDR_Id;

            dsFD = objAVC.GetFDDetails(FDR_Id);
            if (dsFD.Tables[0].Rows.Count > 0)
            {
                string FD_status = dsFD.Tables[0].Rows[0]["FD_STATUS"].ToString();
                int Isclosed = Convert.ToInt32(dsFD.Tables[0].Rows[0]["ISCLOSED"].ToString());
                if (Isclosed == 1 && FD_status == "C")
                {
                    objCommon.DisplayMessage(this.Page, "Sorry! you cannot close this record, because it is closed already!", this.Page);
                    return;
                }
                txtserialNo.Text = dsFD.Tables[0].Rows[0]["FD_SRNO"].ToString();
                BindCompany();

                ddlCompany.SelectedValue = dsFD.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                Session["FDR_COMP_CODE"] = dsFD.Tables[0].Rows[0]["COMPANY_CODE"].ToString();
                ddlCompany.Enabled = false;

                txtLedger.Text = dsFD.Tables[0].Rows[0]["LEDGER"].ToString();
                txtBankLedger.Text = dsFD.Tables[0].Rows[0]["BANKLEDGER"].ToString();
                txtCustomerId.Text = dsFD.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                txtFDRNo.Text = dsFD.Tables[0].Rows[0]["FDR_NO"].ToString();
                txtInvestmentDate.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["INVESTMENT_DATE"]).ToString("dd/MM/yyyy");
                txtMaturityDate.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["MATURITY_DATE"]).ToString();
                txtInvestedAmt.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["AMT_INVESTED"]).ToString();
                txtMaturityAmt.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["MATURITY_AMT"]).ToString();
                txtRateofInterest.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["ROI"]).ToString();
                txtPANNo.Text = dsFD.Tables[0].Rows[0]["PAN_NO"].ToString();
                txtPeriodFrom.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["PERIOD_FROM"]).ToString();
                txtPeriodTo.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["PERIOD_TO"]).ToString();

                txtScheme.Text = dsFD.Tables[0].Rows[0]["SCHEME"].ToString();
                txtAccHolder.Text = dsFD.Tables[0].Rows[0]["ACCOUNT_HOLDER"].ToString();
                txtAddress.Text = dsFD.Tables[0].Rows[0]["CUSTOMER_ADDRESS"].ToString();
                txtNomination.Text = dsFD.Tables[0].Rows[0]["NOMINATION"].ToString();
                //txtRemark.Text = dsFD.Tables[0].Rows[0]["Remark"].ToString();

                txtBankAddress.Text = dsFD.Tables[0].Rows[0]["BANK_ADDRESS"].ToString();
                txtRegisterBookNo.Text = dsFD.Tables[0].Rows[0]["REGISTER_BOOK_NO"].ToString();
                txtReference.Text = dsFD.Tables[0].Rows[0]["REFERENCE"].ToString();
                txtFDWithDrawAmt.Text = dsFD.Tables[0].Rows[0]["FD_WITHDRAWN_AMOUNT"].ToString();
                txtInterestAccumlated.Text = dsFD.Tables[0].Rows[0]["ACCUMULATED_INTEREST"].ToString();
                txtFDDuration.Text = dsFD.Tables[0].Rows[0]["FD_DURATION"].ToString();
                ViewState["attachment"] = dsFD.Tables[0].Rows[0]["FD_ADVISE_ATTACHMENT"].ToString();

                pnlDetails.Visible = true;
                pnlOtherDetils.Visible = true;
                pnlList.Visible = false;
                dvButtons.Visible = true;

                this.Disable();
            }

            if (dsFD.Tables[1].Rows.Count > 0)
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

                for (int i = 0; i < dsFD.Tables[1].Rows.Count; i++)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["SRNO"] = dsFD.Tables[1].Rows[i]["SRNO"].ToString();
                    drnew["EMAILID"] = dsFD.Tables[1].Rows[i]["EMAILID"].ToString();
                    drnew["MOBILENO"] = dsFD.Tables[1].Rows[i]["MOBILENO"].ToString();

                    dt.Rows.Add(drnew);
                }
                Session["dt"] = dt;
                rptOtherDetails.DataSource = dsFD.Tables[1];
                rptOtherDetails.DataBind();

                foreach (RepeaterItem item in rptOtherDetails.Items)
                {
                    ImageButton btnEdit = (ImageButton)item.FindControl("btnEdit");
                    btnEdit.Enabled = false;
                }
            }
            else
            {
                rptOtherDetails.DataSource = null;
                rptOtherDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnFDRRenew_Click(object sender, EventArgs e)
    {
        ViewState["FD_STATUS"] = "renew";
        Session["dt"] = null;
        DataSet dsFD = null;
        try
        {
            Button btnFDRRenew = sender as Button;
            int FDR_Id = Convert.ToInt32(btnFDRRenew.CommandArgument.ToString());

            ViewState["FDID"] = FDR_Id;

            dsFD = objAVC.GetFDDetails(FDR_Id);
            if (dsFD.Tables[0].Rows.Count > 0)
            {
                //string FD_status = dsFD.Tables[0].Rows[0]["FD_STATUS"].ToString();
                //int Isclosed = Convert.ToInt32(dsFD.Tables[0].Rows[0]["ISCLOSED"].ToString());
                //if (Isclosed == 1 && FD_status == "C")
                //{
                //    objCommon.DisplayMessage(this.Page, "Sorry! you cannot close this record, because it is closed already!", this.Page);
                //    return;
                //}
                ViewState["FD_SRNO_RENEW"] = dsFD.Tables[0].Rows[0]["FD_SRNO"].ToString();

                string serialNo = objCommon.LookUp("ACC_FIXED_DEPOSIT_ENTRY", "ISNULL(MAX(FD_SRNO),0) + 1", "");
                ViewState["FD_SrNo"] = serialNo.ToString();
                txtserialNo.Text = serialNo.ToString();
                BindCompany();

                ddlCompany.SelectedValue = dsFD.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                Session["FDR_COMP_CODE"] = dsFD.Tables[0].Rows[0]["COMPANY_CODE"].ToString();
                ddlCompany.Enabled = false;

                ViewState["OTHERLEDGER"] = dsFD.Tables[0].Rows[0]["LEDGER"].ToString();
                txtLedger.Text = dsFD.Tables[0].Rows[0]["LEDGER"].ToString();
                txtBankLedger.Text = dsFD.Tables[0].Rows[0]["BANKLEDGER"].ToString();
                txtCustomerId.Text = dsFD.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                txtFDRNo.Text = string.Empty;

                ViewState["TRANS_DATE"] = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["MATURITY_DATE"]).ToString("dd/MM/yyyy");
                txtInvestmentDate.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["MATURITY_DATE"]).ToString("dd/MM/yyyy");
                txtMaturityDate.Text = string.Empty;

                txtInvestedAmt.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["MATURITY_AMT"]).ToString();
                ViewState["TRANS_AMT"] = Convert.ToDouble(dsFD.Tables[0].Rows[0]["MATURITY_AMT"]).ToString();
                txtMaturityAmt.Text = string.Empty;

                txtRateofInterest.Text = Convert.ToDouble(dsFD.Tables[0].Rows[0]["ROI"]).ToString();
                txtPANNo.Text = dsFD.Tables[0].Rows[0]["PAN_NO"].ToString();
                txtPeriodFrom.Text = Convert.ToDateTime(dsFD.Tables[0].Rows[0]["PERIOD_TO"]).ToString();
                txtPeriodTo.Text = string.Empty;

                txtScheme.Text = dsFD.Tables[0].Rows[0]["SCHEME"].ToString();
                txtAccHolder.Text = dsFD.Tables[0].Rows[0]["ACCOUNT_HOLDER"].ToString();
                txtAddress.Text = dsFD.Tables[0].Rows[0]["CUSTOMER_ADDRESS"].ToString();
                txtNomination.Text = dsFD.Tables[0].Rows[0]["NOMINATION"].ToString();
                //txtRemark.Text = dsFD.Tables[0].Rows[0]["Remark"].ToString();

                txtBankAddress.Text = dsFD.Tables[0].Rows[0]["BANK_ADDRESS"].ToString();
                txtRegisterBookNo.Text = dsFD.Tables[0].Rows[0]["REGISTER_BOOK_NO"].ToString();
                txtReference.Text = dsFD.Tables[0].Rows[0]["REFERENCE"].ToString();
                txtFDWithDrawAmt.Text = dsFD.Tables[0].Rows[0]["FD_WITHDRAWN_AMOUNT"].ToString();
                txtInterestAccumlated.Text = dsFD.Tables[0].Rows[0]["ACCUMULATED_INTEREST"].ToString();
                txtFDDuration.Text = dsFD.Tables[0].Rows[0]["FD_DURATION"].ToString();
                ViewState["attachment"] = dsFD.Tables[0].Rows[0]["FD_ADVISE_ATTACHMENT"].ToString();

                pnlDetails.Visible = true;
                pnlOtherDetils.Visible = true;
                pnlList.Visible = false;
                dvButtons.Visible = true;

                // this.Disable();
            }

            if (dsFD.Tables[1].Rows.Count > 0)
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

                for (int i = 0; i < dsFD.Tables[1].Rows.Count; i++)
                {
                    DataRow drnew = dt.NewRow();
                    drnew["SRNO"] = dsFD.Tables[1].Rows[i]["SRNO"].ToString();
                    drnew["EMAILID"] = dsFD.Tables[1].Rows[i]["EMAILID"].ToString();
                    drnew["MOBILENO"] = dsFD.Tables[1].Rows[i]["MOBILENO"].ToString();

                    dt.Rows.Add(drnew);
                }
                Session["dt"] = dt;
                rptOtherDetails.DataSource = dsFD.Tables[1];
                rptOtherDetails.DataBind();
            }
            else
            {
                rptOtherDetails.DataSource = null;
                rptOtherDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ViewState["FD_STATUS"] = "addnew";
        ViewState["FDID"] = "0";
        try
        {
            this.Enable();
            Clear();
            pnlList.Visible = true;
            pnlOtherDetils.Visible = false;
            pnlDetails.Visible = false;
            dvButtons.Visible = false;

            BindFDRList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = "add";
        ViewState["FDID"] = "0";
        ViewState["FD_STATUS"] = "addnew";
        this.Enable();
    }

    private void Disable()
    {
        txtserialNo.Enabled = false;
        ddlCompany.Enabled = false;
        txtLedger.Enabled = false;
        txtBankLedger.Enabled = false;
        txtCustomerId.Enabled = false;
        txtFDRNo.Enabled = false;
        txtInvestmentDate.Enabled = false;
        txtMaturityDate.Enabled = false;
        txtInvestedAmt.Enabled = false;
        txtMaturityAmt.Enabled = false;
        txtRateofInterest.Enabled = false;
        txtPeriodFrom.Enabled = false;
        txtPeriodTo.Enabled = false;
        txtScheme.Enabled = false;
        txtAccHolder.Enabled = false;
        txtAddress.Enabled = false;
        txtNomination.Enabled = false;
        txtPANNo.Enabled = false;
        pnlOtherDetils.Enabled = false;

        txtBankAddress.Enabled = false;
        txtFDDuration.Enabled = false;
        txtFDWithDrawAmt.Enabled = false;
        txtInterestAccumlated.Enabled = false;
        txtReference.Enabled = false;
        txtRegisterBookNo.Enabled = false;

        imgInvestDate.Visible = false;
        imgMaturityDate.Visible = false;
        //imgPeriodFrom.Visible = false;


    }

    private void Enable()
    {
        txtserialNo.Enabled = true;
        ddlCompany.Enabled = true;
        txtLedger.Enabled = true;
        txtBankLedger.Enabled = true;
        txtCustomerId.Enabled = true;
        txtFDRNo.Enabled = true;
        txtInvestmentDate.Enabled = true;
        txtMaturityDate.Enabled = true;
        txtInvestedAmt.Enabled = true;
        txtMaturityAmt.Enabled = true;
        txtRateofInterest.Enabled = true;
        txtPeriodFrom.Enabled = true;
        txtPeriodTo.Enabled = true;
        txtScheme.Enabled = true;
        txtAccHolder.Enabled = true;
        txtAddress.Enabled = true;
        txtNomination.Enabled = true;
        txtPANNo.Enabled = true;
        pnlOtherDetils.Enabled = true;

        txtBankAddress.Enabled = true;
        txtFDDuration.Enabled = true;
        txtFDWithDrawAmt.Enabled = true;
        txtInterestAccumlated.Enabled = true;
        txtReference.Enabled = true;
        txtRegisterBookNo.Enabled = true;

        imgInvestDate.Visible = true;
        imgMaturityDate.Visible = true;
        //imgPeriodFrom.Visible = true;
    }
}