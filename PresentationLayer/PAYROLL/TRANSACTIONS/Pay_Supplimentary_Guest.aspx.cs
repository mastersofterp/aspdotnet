//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Supplimentary_Guest.ASPX                                                    
// CREATION DATE : 10-OCTOBER-2009                                                        
// CREATED BY    : ROHIT MASKE                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_Pay_Supplimentary_Guest : System.Web.UI.Page
{

    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //controller object
    Pay_Supplimentary_Bill_Guest_Controller objSuppliGuestCon = new Pay_Supplimentary_Bill_Guest_Controller();
    //Entity object
    Pay_Supplimentary_Bill_Guest objSuppliGuestEntity = new Pay_Supplimentary_Bill_Guest();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                //  Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                FillDropDown();
                ViewState["action"] = "add";
                hdnTotAmt.Value = "0";
            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }
    }

    private void FillDropDown()
    {
        Fill_BANK();
        Fill_FinancialYear();
        FillDESIGNATION();
        FillSupliBillHead();
        FillDEPART();
        Fill_Section();
    }

    //Fill Financial Year
    private void Fill_FinancialYear()
    {
        try
        {
            Session["colcode"] = objCommon.LookUp("reff", "college_code", string.Empty);
            objCommon.FillDropDownList(ddlFinYear, "PAYROLL_FINYEAR", "FINYEARID", "FINANCIAL_YEAR", "COLLEGE_CODE IN(" + Session["colcode"] + ") ", "FINYEARID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.Fill_FinancialYear ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Fill Supplimenty Head
    private void FillSupliBillHead()
    {
        try
        {
            Session["colcode"] = objCommon.LookUp("reff", "college_code", string.Empty);
            objCommon.FillDropDownList(ddlSupliBillHead, "PAYROLL_SUPPLIMENTARY_HEAD", "SUPLHNO", "SUPLHEAD", "COLLEGE_CODE IN(" + Session["colcode"] + ") ", "SUPLHNO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.Fill_SUPPLIMENTARY_HEAD ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //function use for DESIGNATION
    private void FillDESIGNATION()
    {
        try
        {
            int collegeCode = Convert.ToInt32(objCommon.LookUp("reff", "college_code", string.Empty));
            objCommon.FillDropDownList(ddlDesignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "", "SUBDESIGNO ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.Fill_SUBDESIG ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //function use for DEPARTMENT
    private void FillDEPART()
    {
        try
        {

            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPTNO ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.FillDEPART ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //function use for BANK FOR Supplimentary_Guest
    private void Fill_BANK()
    {
        try
        {
            Session["colcode"] = objCommon.LookUp("reff", "college_code", string.Empty);
            objCommon.FillDropDownList(ddlBank, "PAYROLL_BANK", "BANKNO", "BANKNAME", "", "BANKNO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.Fill_BANK ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //Funcation use for Section Fetch
    private void Fill_Section()
    {
        try
        {
            objCommon.FillDropDownList(ddlsection, "ACC_TEST1_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.Fill_Section ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_SupplimentaryBill.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SupplimentaryBill.aspx");
        }
    }

    private void GetDetails()
    {
        objSuppliGuestEntity.EMPLOYEENAME = txtEmpName.Text;
        objSuppliGuestEntity.SUPLHEAD = ddlSupliBillHead.SelectedItem.Text;
        objSuppliGuestEntity.SUPLHNO = Convert.ToInt32(ddlSupliBillHead.SelectedValue);
        objSuppliGuestEntity.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);
        objSuppliGuestEntity.DEPARTMENTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
        objSuppliGuestEntity.REMARK = txtRemark.Text;
        objSuppliGuestEntity.CODE = txtCode.Text;
        objSuppliGuestEntity.ORDNO = txtOrderNo.Text;
        objSuppliGuestEntity.SBDATE = Convert.ToDateTime(txtSupliBillDate.Text);
        objSuppliGuestEntity.PANNO = txtPanNo.Text;
        objSuppliGuestEntity.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
        objSuppliGuestEntity.EMAILID = txtEmail.Text;
        objSuppliGuestEntity.MOBILENO = txtMobNo.Text;
        objSuppliGuestEntity.ACCNO = txtAccNo.Text;
        objSuppliGuestEntity.IFSCCODE = txtIFSC.Text;
        objSuppliGuestEntity.TOTAL_AMOUNT = Convert.ToDecimal(txtNetlAmount.Text);
        objSuppliGuestEntity.TDS_PER = Convert.ToDecimal(txtPersentage.Text);
        objSuppliGuestEntity.TDS_AMOUNT = Convert.ToDecimal(txtTDSAmt.Text);
        objSuppliGuestEntity.NET_AMOUNT = Convert.ToDecimal(txtAmount.Text);
        objSuppliGuestEntity.FINYEARID = Convert.ToInt32(ddlFinYear.SelectedValue);
        objSuppliGuestEntity.TDS_NETAMOUNT = Convert.ToDecimal(txtTDSPaidAmt.Text);
        objSuppliGuestEntity.COLLEGE_CODE = objCommon.LookUp("reff", "college_code", string.Empty);
        objSuppliGuestEntity.SECTIONID = Convert.ToInt32(ddlsection.SelectedValue);
    }



    private void clear()
    {
        txtAccNo.Text = "";
        txtAmount.Text = "";
        txtCode.Text = "";
        txtEmail.Text = "";
        txtEmpName.Text = "";
        txtIFSC.Text = "";
        txtMobNo.Text = "";
        txtNetlAmount.Text = "";
        txtOrderNo.Text = "";
        txtPanNo.Text = "";
        txtPersentage.Text = "";
        txtRemark.Text = "";
        hdnTotAmt.Value = "0";
        txtSupliBillDate.Text = "";
        txtTDSAmt.Text = "0";
        ddlBank.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        ddlFinYear.SelectedIndex = 0;
        txtTDSPaidAmt.Text = "0";
        ddlSupliBillHead.SelectedIndex = 0;
        ViewState["Status"] = "N";
        lvSuplBillDtl.DataSource = null;
        ddlsection.SelectedIndex = 0;
        lvSuplBillDtl.Items.Clear();
        lvSuplBillDtl.DataBind();
    }

    private void clearinfo()
    {
        txtAccNo.Text = "";
        txtAmount.Text = "";
        txtCode.Text = "";
        txtEmail.Text = "";
        txtEmpName.Text = "";
        txtIFSC.Text = "";
        txtMobNo.Text = "";
        txtNetlAmount.Text = "";
        txtOrderNo.Text = "";
        txtPersentage.Text = "";
        txtRemark.Text = "";
        hdnTotAmt.Value = "0";
        txtSupliBillDate.Text = "";
        txtTDSAmt.Text = "0";
        ddlBank.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        txtTDSPaidAmt.Text = "0";
        ddlSupliBillHead.SelectedIndex = 0;
        ViewState["Status"] = "N";
        lvSuplBillDtl.DataSource = null;
        lvSuplBillDtl.DataBind();

    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if ((!string.IsNullOrEmpty(txtPanNo.Text)) || ddlFinYear.Items.Count < 1)
            {
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    double tempAMt = ((Convert.ToDouble(txtAmount.Text)) + (Convert.ToDouble(hdnTotAmt.Value)));

                    if (txtAmount.Text != "0")
                    {
                        txtNetlAmount.Text = ((Convert.ToDouble(txtAmount.Text)) + (Convert.ToDouble(hdnTotAmt.Value))).ToString();
                    }
                    else
                    {
                        txtTDSPaidAmt.Text = txtAmount.Text;
                    }

                    if (txtPersentage.Text == "")
                    { txtPersentage.Text = "0"; }

                    //if ((Convert.ToDecimal(txtAmount.Text)) >= 30000)
                    //{
                    //    txtTDSAmt.Enabled = true;
                    //    txtPersentage.Enabled = true;
                    //}
                    //else 
                    if (tempAMt >= 30000)
                    {
                        txtTDSPaidAmt.Text = txtAmount.Text;
                        txtTDSAmt.Enabled = true;
                        txtPersentage.Enabled = true;
                        ddlsection.Enabled = true;
                    }
                    else if (ViewState["Status"].ToString() == "Y")
                    {
                        txtTDSAmt.Enabled = true;
                        txtPersentage.Enabled = true;
                        ddlsection.Enabled = true;
                    }
                    //else if (Convert.ToDouble(txtNetlAmount.Text) >= 30000)
                    //{
                    //    double Calculate = 0, TotAmt = 0;
                    //    TotAmt = Convert.ToDouble(txtNetlAmount.Text);
                    //    Calculate = (TotAmt / 100);
                    //    txtTDSAmt.Text = Convert.ToString(Calculate * Convert.ToDouble(txtPersentage.Text));
                    //    txtTDSAmt.Enabled = true;
                    //    txtPersentage.Enabled = true;
                    //}
                    else
                    {
                        txtTDSPaidAmt.Text = txtAmount.Text;
                        txtNetlAmount.Text = ((Convert.ToDouble(txtAmount.Text)) + (Convert.ToDouble(hdnTotAmt.Value))).ToString();
                        txtTDSAmt.Enabled = false;
                        txtPersentage.Enabled = false;
                        ddlsection.Enabled = false;
                        ddlsection.SelectedIndex = 0;
                        txtTDSAmt.Text = "0";
                        txtPersentage.Text = "0";
                    }
                }
            }
            else
            {
                ShowMessage("Please Enter Pan No. OR Select Financial Year");
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.txtAmount_TextChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtNetlAmount_TextChanged(object sender, EventArgs e)
    {

    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {

        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                if (txtPersentage.Enabled == true)
                {
                    if (Convert.ToInt32(txtPersentage.Text) == 0)
                    {
                        // ShowMessage("Please Enter Precentage Greater than 0 .");
                        objCommon.DisplayMessage(UpdatePanel1, "Enter Numeric Value only and Value should be greater than zero", this);
                        return;
                    }
                    else if ((Convert.ToInt32(ddlsection.SelectedValue)) == 0)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Please Select Section ", this);
                        return;
                    }
                }

                GetDetails();
                CustomStatus cs = (CustomStatus)objSuppliGuestCon.AddSupplimentryGuest(objSuppliGuestEntity);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                    clear();
                    fillSupplimentryBillForGuest();
                }

            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                GetDetails();
                objSuppliGuestEntity.SUPLGUESTID = Convert.ToInt32(ViewState["SUPLGUEST_ID"]);
                CustomStatus cs = (CustomStatus)objSuppliGuestCon.UpdateSupplimentryGuest(objSuppliGuestEntity);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record Update Successfully", this);
                    clear();
                    fillSupplimentryBillForGuest();
                }
            }

        }
    }

    private void fillSupplimentryBillForGuest()
    {
        if (!string.IsNullOrEmpty(ddlFinYear.Text) && !string.IsNullOrEmpty(txtPanNo.Text))
        {
            objSuppliGuestEntity.PANNO = txtPanNo.Text;
            objSuppliGuestEntity.FINYEARID = Convert.ToInt32(ddlFinYear.SelectedValue);
            ViewState["Status"] = "N";
            DataSet ds = objSuppliGuestCon.GetSingleSupplimentaryGuest(objSuppliGuestEntity);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtOrderNo.Text = ds.Tables[0].Rows[0]["ORDNO"].ToString();
                    txtEmpName.Text = ds.Tables[0].Rows[0]["EMPLOYEENAME"].ToString();
                    ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();
                    ddlSupliBillHead.SelectedValue = ds.Tables[0].Rows[0]["SUPLHNO"].ToString();
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPARTMENTNO"].ToString();
                    txtCode.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                    ddlBank.SelectedValue = ds.Tables[0].Rows[0]["BANKNO"].ToString();
                    txtAccNo.Text = ds.Tables[0].Rows[0]["ACCNO"].ToString();
                    txtIFSC.Text = ds.Tables[0].Rows[0]["IFSCCODE"].ToString();
                    txtMobNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    if (ds.Tables[0].Rows[0]["FINYEARID"].ToString() == ddlFinYear.SelectedValue)
                    {
                        txtNetlAmount.Text = ds.Tables[0].Rows[0]["NET_AMOUNT"].ToString();
                        // txtSupliBillDate.Text = ds.Tables[0].Rows[0]["SBDATE"].ToString();
                        string Status = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        // ViewState["Status"] = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        Decimal tempAmt = 0;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["STATUS"].ToString() == "N")
                            {
                                tempAmt = tempAmt + Convert.ToDecimal(ds.Tables[0].Rows[i]["NET_AMOUNT"].ToString());
                                Status = ds.Tables[0].Rows[i]["STATUS"].ToString();
                            }
                            else
                            {
                                //tempAmt = tempAmt + Convert.ToDecimal(ds.Tables[0].Rows[i]["NET_AMOUNT"].ToString());
                                Status = ds.Tables[0].Rows[i]["STATUS"].ToString();
                            }

                        }
                        ViewState["Status"] = Status;
                        hdnTotAmt.Value = tempAmt.ToString();
                        txtNetlAmount.Text = tempAmt.ToString();
                        lvSuplBillDtl.DataSource = ds;
                        lvSuplBillDtl.DataBind();
                    }
                    else
                    {
                        txtNetlAmount.Text = "0";
                        // txtSupliBillDate.Text = ds.Tables[0].Rows[0]["SBDATE"].ToString();
                        string Status = "N";
                        // ViewState["Status"] = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        string tempAmt = "0";
                        ViewState["Status"] = Status;
                        hdnTotAmt.Value = tempAmt;
                        txtNetlAmount.Text = tempAmt;
                        lvSuplBillDtl.DataSource = null;
                        lvSuplBillDtl.DataBind();
                    }

                }
                else
                {
                    clearinfo();
                }

            }
            else
            {
                clearinfo();
            }
        }
    }


    protected void txtPanNo_TextChanged(object sender, EventArgs e)
    {
        fillSupplimentryBillForGuest();
    }

    protected void btnEditSuplBill_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SUPLGUEST_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ViewState["SUP_ID"] = SUPLGUEST_ID;
            DataSet dsSuppliGuest = objSuppliGuestCon.GetAllSupplimentaryGuest(SUPLGUEST_ID);
            if (dsSuppliGuest != null)
            {
                // txtOrderNo.Text = dsSuppliGuest.Tables[0].Rows[0]["ORDNO"].ToString();
                txtEmpName.Text = dsSuppliGuest.Tables[0].Rows[0]["EMPLOYEENAME"].ToString();
                ddlDesignation.SelectedItem.Text = dsSuppliGuest.Tables[0].Rows[0]["SUBDESIG"].ToString();
                // ddlSupliBillHead.SelectedItem.Text = dsSuppliGuest.Tables[0].Rows[0]["SUPLHEAD"].ToString();
                ddlDepartment.SelectedItem.Text = dsSuppliGuest.Tables[0].Rows[0]["SUBDEPT"].ToString();
                txtCode.Text = dsSuppliGuest.Tables[0].Rows[0]["CODE"].ToString();
                ddlBank.SelectedItem.Text = dsSuppliGuest.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtAccNo.Text = dsSuppliGuest.Tables[0].Rows[0]["ACCNO"].ToString();
                txtIFSC.Text = dsSuppliGuest.Tables[0].Rows[0]["IFSCCODE"].ToString();
                txtMobNo.Text = dsSuppliGuest.Tables[0].Rows[0]["MOBILENO"].ToString();
                txtEmail.Text = dsSuppliGuest.Tables[0].Rows[0]["EMAILID"].ToString();
                txtRemark.Text = dsSuppliGuest.Tables[0].Rows[0]["REMARK"].ToString();
                txtAmount.Text = dsSuppliGuest.Tables[0].Rows[0]["NET_AMOUNT"].ToString();
                txtNetlAmount.Text = dsSuppliGuest.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                txtSupliBillDate.Text = dsSuppliGuest.Tables[0].Rows[0]["SBDATE"].ToString();
                ViewState["SUPLGUEST_ID"] = dsSuppliGuest.Tables[0].Rows[0]["SUPLGUESTID"].ToString();
                //ViewState["Status"] = dsSuppliGuest.Tables[0].Rows[0]["STATUS"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.btnEditSuplBill ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        int a = 0;
    }

    private void CalculatePercentage()
    {
        string status = Convert.ToString(ViewState["Status"]);
        if (status == "N")
        {
            double Calculate = 0, TotAmt = 0;
            TotAmt = Convert.ToDouble(txtNetlAmount.Text);
            Calculate = (TotAmt / 100);
            txtTDSAmt.Text = Convert.ToString(Calculate * Convert.ToDouble(txtPersentage.Text));
            txtTDSPaidAmt.Text = Convert.ToString((Convert.ToDouble(txtAmount.Text)) - (Convert.ToDouble(txtTDSAmt.Text)));
        }
        else if (status == "Y")
        {
            double Calculate = 0, TotAmt = 0;
            TotAmt = Convert.ToDouble(txtAmount.Text);
            Calculate = (TotAmt / 100);
            txtTDSAmt.Text = Convert.ToString(Calculate * Convert.ToDouble(txtPersentage.Text));
            txtTDSPaidAmt.Text = Convert.ToString((Convert.ToDouble(txtAmount.Text)) - (Convert.ToDouble(txtTDSAmt.Text)));
        }

    }

    protected void txtPersentage_TextChanged(object sender, EventArgs e)
    {

        CalculatePercentage();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlFinYear.SelectedIndex > 0 || txtPanNo.Text !="")
            //{
            //    ShowMessage("Select Financial Year");
            //    return;
            //}
            //// ViewState["action"] = "payslip";

            ShowReportSupplimentryGuest("SUPPLIMENTRY GUEST", "SupplimentryBillExternal.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    private void ShowReportSupplimentryGuest(string reportTitle, string rptFileName)
    {
        try
        {
            int college_code = Convert.ToInt32(Session["colcode"]);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@finYearId=" + ddlFinYear.SelectedValue + ",@panNo=" + txtPanNo.Text + ",@P_COLLEGE_CODE=" + college_code;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Supplimentary_Guest.ShowReportSupplimentryGuest() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
}