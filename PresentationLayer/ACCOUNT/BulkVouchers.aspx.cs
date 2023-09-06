//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :Bulk VOUCHERS                                                     
// CREATION DATE :18-june-2021                                              
// CREATED BY    :  TANU BALGOTE                                    
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Data;
using System.Data.Linq;
using System.Web;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

//using System.Windows;
//using System.Windows.Forms;
public partial class BulkVouchers : System.Web.UI.Page
{
    Common objCommon = new Common();
    CostCenter objCostCenter = new CostCenter();
    CostCenterController objCostCenterController = new CostCenterController();
    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    string isVoucherAutoCashBank = string.Empty;//auto generated voucher no but different for cash and banks
    string isMessagingEnabled = string.Empty;
    string isFourSign = string.Empty;
    string isBankCash = string.Empty;
    string isvoucher_Cheque_Print = string.Empty;
    string IsSponsorProject = "";
    string IsBudeget = string.Empty;
    string back = string.Empty;

    private DataTable dt = new DataTable();
    public string[] para;
    public static string isEdit;
    public static int RowIndex = -1;

    string isVoucherTypeWise = string.Empty;
    string AllowVoucherNoReset = string.Empty;
    string IsSingleAuthority = string.Empty;
    string IsTempVoucher = string.Empty;
    public static string tranTypeForStrVno;
    public static string StrVno = "abc/111";
    public static string TransactionDateToUpdate = string.Empty;
    AccountTransactionController objPC1 = new AccountTransactionController();
    AccountTransaction objATEnt = new AccountTransaction();


    //added by tanu 13/12/2021 for upload bill files
    public string Docpath = HttpContext.Current.Server.MapPath("~/FILEUPLOAD/upload_files/");
    public string path = string.Empty;
    DataTable dtc = new DataTable("DocTbl");


    //For XML Doc

    XmlDocument xmlDoc = new XmlDocument();

    // For Makaut
    public static string isTempVoucher = string.Empty;

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
            //objCommon = new Common();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

        lnkLedger.Attributes.Add("onClick", "return ShowLedger();");
        lnkGroup.Attributes.Add("onClick", "return ShowGroup();");
        //btnSave.Attributes.Add("onClick", "return AskSave();");

        lblCurbal1.Text = hdnCurBalAg.Value.ToString().Trim();
        lblCurBal2.Text = hdnCurBal.Value.ToString().Trim();
        if (Session["comp_code"] != null)
        {
            SetParameters();

            if (isEdit != "Y")
            {
                //VoucherNoSetting();
                //SetVoucherNo();
            }
            ChangeThePageLayout();
        }
        Session["ComputerIDAddress"] = Request.ServerVariables["REMOTE_ADDR"];


        if (!Page.IsPostBack)
        {
            if (Request.QueryString["obj"] == null)
                //  CheckPageAuthorization();

                ViewState["RowIndex"] = -1;
            ViewState["TDS"] = string.Empty;
            //ViewState["IsIGST"] = "0";
            //ViewState["IsGST"] = "0";
            ViewState["RecTblbuklvouchr"] = null;
            ViewState["SRNO"] = null;
            ViewState["RecTblEmpData"] = null;
            ViewState["RecTblTotAmount"] = null;
            //isTempVoucher = "Y";
            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");
            FillPaymentMode();  // Added by Vidisha
            // btnAdd.Enabled = true ;//oldkk
            objCommon = new Common();
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

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    SetCommunication();
                    Session["comp_set"] = "";
                    //Page Authorization
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    //PopulateDropDown();
                    //PopulateListBox();
                    ViewState["action"] = "add";
                    Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
                    ChangeThePageLayout();
                    if (IsSponsorProject == "Y")
                    {
                        trSponsor.Visible = true;
                        objCommon.FillDropDownList(ddlSponsor, "Acc_" + Session["comp_code"] + "_Project", "ProjectId", "ProjectName", "", "");
                    }
                    else
                    {
                        //trSponsor.Visible = false;
                        trSponsor.Attributes.Add("style", "display:none");
                        // trSubHead.Attributes.Add("style", "display:none");
                    }
                }

                if (isSingleMode == "Y")
                {
                    // txtAgainstAcc.Focus();
                }
                else
                {
                    txtAcc.Focus();
                }
            }

            objCommon.FillDropDownList(ddlName, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "", "  E.FNAME");
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') + '['+ convert(nvarchar(150),EmployeeId) + ']' AS NAME", "IDNO > 0", "FNAME");
          
            objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "");
            ViewState["NATURE_ID"] =
            Session["ComputerIDAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            //RowIndex = -1;
            //SetDataColumn();
            ViewState["RowIndex"] = -1;
            txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtChequeDt2.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            ddlTranType.SelectedIndex = 0;
            ViewState["isModi"] = "N";
            ViewState["Balance3"] = "0";
            if (Request.QueryString["obj"] != null)
            {
                para = Request.QueryString["obj"].ToString().Trim().Split(',');
                if (para[0] == "Copy")
                {
                    hdnvch.Value = "no";
                    ViewState["MFromDate"] = para[3].ToString().Trim();
                    ViewState["MToDate"] = para[4].ToString().Trim();
                    ViewState["MPartyNo"] = para[5].ToString().Trim();
                    isEdit = "N";
                    ViewState["isEdit"] = "N";
                    ViewState["isModi"] = "N";
                    ViewState["isCopy"] = "Y";
                    ModifyVoucherTransaction();
                    VoucherNoSetting();
                    //SetVoucherNo();
                }
                else if (para[1] != "back")
                {
                    hdnvch.Value = para[1].ToString().Trim();

                    isEdit = "Y";
                    ViewState["isEdit"] = "Y";
                    ViewState["isModi"] = "Y";
                    ModifyVoucherTransaction();
                    GetBalance();
                    GetRemainingAmount();
                    if (para[5] == "Verify")
                    {
                        Disable();
                        btnVerify.Visible = true;
                        btnVerify.Text = "Verify";
                        btnReturn.Visible = true;
                        trPaymentMode.Visible = false;
                        btnSave.Visible = false;
                        btnReset.Visible = false;

                        btnAddBill.Visible = false;
                        pnlupload.Enabled = false;

                        ViewState["AuthorityType"] = "Verify";
                        ViewState["VOUCHER_SQN"] = para[1].ToString().Trim();
                    }
                    else if (para[5] == "Approve")
                    {
                        Disable();
                        btnVerify.Visible = true;
                        btnVerify.Text = "Approve";
                        btnReturn.Visible = true;
                        trPaymentMode.Visible = false;
                        btnSave.Visible = false;
                        btnReset.Visible = false;

                        btnAddBill.Visible = false;
                        pnlupload.Enabled = false;

                        ViewState["AuthorityType"] = "Approve";
                        ViewState["VOUCHER_SQN"] = para[1].ToString().Trim();
                    }
                    else
                    {
                        Enable();
                        btnVerify.Visible = false;
                        btnReturn.Visible = false;
                        trPaymentMode.Visible = true;
                        btnSave.Visible = true;
                        btnReset.Visible = true;
                        ViewState["AuthorityType"] = null;
                        ViewState["VOUCHER_SQN"] = null;

                    }
                    //txtDate.Enabled = false;
                }
                else
                {

                    hdnvch.Value = "no";
                    isEdit = "N";
                    ViewState["isEdit"] = "N";
                    ViewState["isModi"] = "N";
                    VoucherNoSetting();
                    //SetVoucherNo();
                }

                if (para[0] == "configm")
                {
                    ViewState["MFromDate"] = para[3].ToString().Trim();
                    ViewState["MToDate"] = para[4].ToString().Trim();
                    ViewState["MPartyNo"] = para[5].ToString().Trim();
                }
            }
            else
            {
                hdnvch.Value = "no";
                ViewState["isEdit"] = "N";
                //ViewState["TRANDATE"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                VoucherNoSetting();
                //SetVoucherNo();
            }


            //added by tanu 14/12/2021

          //  ViewState["DOCS"] = null;
            DeleteDirecPath(Docpath + "VOUCHER_BILL\\EMPID_" + Convert.ToInt32(Session["userno"])+"\\BillNo"+txtVoucherNo.Text.Trim());

            
            SetDataColumn();
            //ddlTranType_SelectedIndexChanged(sender, e);
            ViewState["Date"] = "";
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_PARTY", "ACC_CODE,BALANCE", "PARTY_NAME", "SetDefault=1", "");
            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       // txtAgainstAcc.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() + "*" + ds.Tables[0].Rows[0]["ACC_CODE"].ToString();
                     //   lblCurbal1.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                    }
                }
            }
            txtAgainstAcc.Attributes.Add("OnChange", "javascript:return DoPostBack()");
        }

        tranTypeForStrVno = "P";
        SetWithoutCashBank();
        lnkupload.Attributes.Add("onClick", "return ShowVoucherWindow('do'," + GridData.Rows.Count.ToString() + ");");
        lnkView.Attributes.Add("onClick", "return ShowVoucherWindow('no'," + GridData.Rows.Count.ToString() + ");");
        txtChqNo2.Attributes.Add("onblur", "CheckDuplicate(this,'" + Session["comp_code"].ToString() + "')");
        divMsg.InnerHtml = string.Empty;
        if (Request.QueryString["obj"] != null)
        {
            para = Request.QueryString["obj"].ToString().Trim().Split(',');
            if (para[1] != "back")
            {
                ddlTranType.Enabled = false;
            }
        }
        if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "J")
        {
            chkTDSApplicable.Visible = true;
            //chkGST.Visible = true;
            //chkIGST.Visible = true;
        }
        else
        {
            chkTDSApplicable.Visible = false;
            //chkGST.Visible = false;
            //chkIGST.Visible = false;
        }
    }

    private void SetVoucherNo()
    {
        AccountTransactionController objTranController = new AccountTransactionController();
        txtVoucherNo.Text = Convert.ToString(objTranController.SetVoucherNo());
    }


    protected void SetCommunication()
    {
        if (isMessagingEnabled == "Y")
        {
            // comm = new GsmCommMain((int)CommSetting.Comm_Port, (int)CommSetting.Comm_BaudRate, (int)CommSetting.Comm_TimeOut);
            try
            {
                //comm.Open();
                //CommSetting.comm = this.comm;
                // btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                //CommSetting.comm.
                //comm.Close();
                // lblMsg.Text = ex.Message;
                // objCommon.DisplayUserMessage(UPDLedger,"Modem is not connected well, check modem", this);
                // btnSave.Enabled = false;
                // MessageBox.Show(this, "Connection error: " + ex.Message, "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        else
        {
            btnSave.Enabled = true;
        }
    }
    private void SetWithoutCashBank()
    {
        if (ddlTranType.SelectedValue.ToString().Trim() == "J")
        {
            Session["WithoutCashBank"] = "Y";
        }
        else if (ddlTranType.SelectedValue.ToString().Trim() == "C")
        {
            Session["WithoutCashBank"] = "YN"; //Only Cash-Bank Ledgers
        }
        else
        {
            if (isSingleMode == "N")
            {
                if (ddlTranType.SelectedValue.ToString().Trim() == "R" && ddlcrdr.SelectedItem.Text.ToString().Trim() == "Cr")
                {
                    Session["WithoutCashBank"] = "Y";
                }
                else if (ddlTranType.SelectedValue.ToString().Trim() == "P" && ddlcrdr.SelectedItem.Text.ToString().Trim() == "Dr")
                {
                    Session["WithoutCashBank"] = "Y";
                }
                else
                {
                    Session["WithoutCashBank"] = "N";
                }

            }
            else
            {
                Session["WithoutCashBank"] = "Y";
            }
        }
    }

    [WebMethod]
    public static string LookupProduct(string Username)
    {
        string sVo = string.Empty;
        //try
        //{
        var thisPage = new BulkVouchers();
        sVo = thisPage.VoucherNoSettingForCBVoucherNo(Username);
        return sVo;
        //}
        //catch (Exception ex)
        //{
        //    return sVo;
        //}

    }


    private string VoucherNoSettingForCBVoucherNo(string Username)
    {
        try
        {
            Common objCommon2 = new Common();
            string type = string.Empty;
            int cashBankId = 0;
            cashBankId = Convert.ToInt32(Username.Replace("¯", "").ToString().Trim().Split('*')[0].ToString());
            string payTypeNo = objCommon2.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PAYMENT_TYPE_NO", "PARTY_NO=" + cashBankId);

            if (Convert.ToInt32(payTypeNo) == 1 && tranTypeForStrVno == "P")
                type = "CP";
            else if (Convert.ToInt32(payTypeNo) == 1 && tranTypeForStrVno == "R")
                type = "CR";
            else if (Convert.ToInt32(payTypeNo) == 2 && tranTypeForStrVno == "P")
                type = "BP";
            else if (Convert.ToInt32(payTypeNo) == 2 && tranTypeForStrVno == "R")
                type = "BR";
            else if (tranTypeForStrVno == "C")
                type = "CT";


            if (type != string.Empty)
            {
                AccountConfigurationController objvch = new AccountConfigurationController();
                DataTableReader dtr = objvch.GetMaxVoucherNoSegregated(Session["comp_code"].ToString().Trim(), type);// + "_" + Session["fin_yr"].ToString().Trim());

                if (dtr.HasRows == true)
                {
                    if (dtr.Read() == true)
                    {
                        //lblVoucherNo.Text = dtr["V_NO"].ToString().Trim().ToUpper();
                        //ViewState["STR_CB_VNO"] = dtr["V_NO"].ToString().Trim().ToUpper();
                        //Session["STR_CB_VNO"] = dtr["V_NO"].ToString().Trim().ToUpper();
                        StrVno = dtr["V_NO"].ToString().Trim().ToUpper();
                        return dtr["V_NO"].ToString().Trim().ToUpper();
                    }
                }

            }
            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    private void VoucherNoSetting()
    {
        if (AllowVoucherNoReset == "Y")// && para[1] != "back")
        {
            if (isVoucherAuto == "Y")
            {
                string VCH_TYPE = ddlTranType.SelectedValue.ToString();
                DataTableReader dtr = null;
                if (txtDate.Text == "" || ViewState["isCopy"] != null)
                    txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                AccountConfigurationController objvch = new AccountConfigurationController();
                if (isVoucherTypeWise == "N")
                    dtr = objvch.GetMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                else
                    dtr = objvch.GetVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                if (dtr.HasRows == true)
                {
                    if (dtr.Read() == true)
                    {
                        txtVoucherNo.Text = dtr["V_NO"].ToString().Trim();
                        txtVoucherNo.Enabled = false;
                    }
                }
                else
                {
                    //need to show a message here
                    txtVoucherNo.Enabled = true;
                }
            }
            else
            {
                txtVoucherNo.Enabled = true;
                //txtVoucherNo.Text = string.Empty;
            }
        }
        else
        {
            if (ViewState["VOUCHERNUMBER"] == null)
            {
                if (isVoucherAuto == "Y")
                {
                    string VCH_TYPE = ddlTranType.SelectedValue.ToString();
                    DataTableReader dtr = null;
                    if (txtDate.Text == "" || ViewState["isCopy"] != null)
                        txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    AccountConfigurationController objvch = new AccountConfigurationController();


                    if (isVoucherTypeWise == "N")
                    {
                        if (isTempVoucher == "N")
                        {
                            dtr = objvch.GetMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));// + "_" + Session["fin_yr"].ToString().Trim());
                        }
                        else
                        {
                            dtr = objvch.GetVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                        }
                    }
                    else
                    {
                        if (isTempVoucher == "N")
                        {
                            dtr = objvch.GetMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));// + "_" + Session["fin_yr"].ToString().Trim());
                        }
                        else
                        {
                            dtr = objvch.GetVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                        }
                    
                    }
                   



                    if (dtr.HasRows == true)
                    {
                        if (dtr.Read() == true)
                        {
                            txtVoucherNo.Text = dtr["V_NO"].ToString().Trim();
                            txtVoucherNo.Enabled = false;
                        }
                    }
                    else
                    {
                        //need to show a message here
                        txtVoucherNo.Enabled = true;
                    }
                }
            }
            else
            {
                txtVoucherNo.Text = ViewState["VOUCHERNUMBER"].ToString();
                //txtDate.Text = ViewState["TRANDATE"].ToString();
            }
        }
        ViewState["isCopy"] = null;
    }

    private void ChangeThePageLayout()
    {
        //if (lblMsg.Text.ToString().Trim() != "")
        //{
        //    rowMsg.Visible = true;
        //    //rowMsg.Style["Display"] = "block";
        //}
        //else
        //{
        //    // rowMsg.Style["Display"] = "none"; 
        //    rowMsg.Visible = false;
        //}

        if (isSingleMode == "Y")
        {
            //Row1.Style["Display"] = "block";
            //Row2.Style["Display"] = "block";
            Row1.Visible = true;
            Row2.Visible = true;
            //Rowdrcr.Style["Display"] = "none";
            //RowTot.Style["Display"] = "block";
            Rowdrcr.Visible = false;
            RowTot.Visible = true;
            //ddlcrdr.Enabled = true;
            if (ddlTranType.SelectedValue != "J")
            {
                if (GridData.Rows.Count == 0)
                {
                    ddlcrdr.Enabled = false;
                }
            }
            lblTotalDiff.Visible = false;
            lblTotalDebit0.Visible = false;
        }
        else
        {
            Rowdrcr.Visible = true;
            RowTot.Visible = false;
            //Rowdrcr.Style["Display"] = "block";
            //RowTot.Style["Display"] = "none";
            Row1.Visible = false;
            Row2.Visible = false;
            //Row1.Style["Display"] = "none";
            //Row2.Style["Display"] = "none";
            if (ddlTranType.SelectedValue != "J")
            {
                if (GridData.Rows.Count == 0)
                {
                    ddlcrdr.Enabled = false;
                }
            }
            else { ddlcrdr.Enabled = true; }

            lblTotalDiff.Visible = true;
            lblTotalDebit0.Visible = true;
        }

        if (isPerNarration == "Y")
        {
            row3.Style["Display"] = "none";
            // row4.Style["Display"] = "block";
            row4.Visible = true;
        }
        else
        {
            row4.Style["Display"] = "none";
            row4.Visible = false;
            row3.Style["Display"] = "block";
        }
        if (isVoucherAuto == "Y")
        {
            txtVoucherNo.Enabled = false;
        }
        else
        { txtVoucherNo.Enabled = true; }

    }

    private void SetParameters()
    {
        objCommon = new Common();
        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_"+Session["comp_code"]+"_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
        int i = 0;
        if (ds != null)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "SINGLE MODE PAYMENT/RECEIPT/CONTRA ENTRY")
                {
                    if (ddlTranType.SelectedValue.ToString().Trim() == "J")
                    {
                        isSingleMode = "N";
                    }
                    else { isSingleMode = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim(); }

                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "PER ENTRY NARRATION REQUIRED")
                {
                    isPerNarration = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "AUTOGENERATED VOUCHER NO. REQUIRED")
                {
                    isVoucherAuto = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "MOBILE MESSAGING ENABLED")
                {
                    isMessagingEnabled = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "AUTO GEN VOUCHERNO FOR CASH BANK")
                {
                    isVoucherAutoCashBank = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "VOUCHER NO SEPRATE FOR RCPT,PAY,CONT,JOUN")
                {
                    isVoucherTypeWise = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ASK PRINT VOUCHER OR PRINT CHEQUE ?")
                {
                    isvoucher_Cheque_Print = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW SPONSOR PROJECT")
                {
                    IsSponsorProject = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW VOUCHER NO RESETTING")
                {
                    AllowVoucherNoReset = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "IS SINGLE AUTHORITY")
                {
                    IsSingleAuthority = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "IS TEMP VOUCHER")
                {
                    IsTempVoucher = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
            }
        }
    }

    private void UpdateDataRow(int Index, DataTable dtupd)
    {
        UpdateRowNew();


        Session["Datatable"] = dt;
        AddDeleteId();

    }


    private void UpdateDataRow_Journal(int Index, DataTable dtupd)
    {
        UpdateRowNew();
        AddDeleteId();
        string TranMode = string.Empty;
        string AMOUNT = string.Empty;
        PartyController objPC = new PartyController();
        if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
        {
            TranMode = "Cr";
        }
        else
        {
            TranMode = "Dr";
        }

        //if (Convert.ToDouble(txtTranAmt.Text) < 0)
        //{
        if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
        {
            TranMode = "Dr";
        }
        else
        {
            TranMode = "Cr";
        }
        // }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToDouble(dt.Rows[i]["IGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["SGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["CGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TDSpercentage"].ToString()) == 0.00 && dt.Rows[i]["Mode"].ToString().Trim() == "Dr")
            {
                //string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dt.Rows[i]["PARTY_NO"].ToString().Trim());
                dt.Rows[i]["Particulars"] = txtAcc.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();


                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtAcc.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());
                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }

                if (dt.Rows[i]["Mode"].ToString().Trim() == "Dr")
                {
                    //dt.Rows[i]["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                    dt.Rows[i]["Credit"] = "0.00";
                    dt.Rows[i]["Mode"] = "Dr";
                    // dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) + Convert.ToDouble(txtTranAmt.Text));
                }
                else
                {
                    dt.Rows[i]["Debit"] = "0.00";
                    // dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                    dt.Rows[i]["Mode"] = "Cr";
                }
                // dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim()))); ;



                dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtAcc.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0";
                dt.Rows[i]["TDSpercentage"] = "0.00";

                //added by vijay andoju on 26082020 fro IGST
                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }



            if (Convert.ToDouble(dt.Rows[i]["TDSpercentage"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtTDSLedger.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtTDSLedger.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = "0.00"; //Convert.ToString(Convert.ToDouble(AMOUNT) - Convert.ToDouble(txtTDSAmount.Text));

                dt.Rows[i]["Debit"] = "0.00";
                dt.Rows[i]["Credit"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Cr";

                dt.Rows[i]["Amount"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));

                dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtTDSLedger.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                // dt.Rows[i]["TDSAMOUNT"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                dt.Rows[i]["TDSsection"] = ddlSection.SelectedValue;
                // dt.Rows[i]["TDSpercentage"] = Convert.ToDouble(txtTDSPer.Text).ToString();
                //  dt.Rows[i]["TDSonAmount"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTamount.Text.ToString().Trim()))); ;
                //added by vijay andoju on 26082020 fro IGST
                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }
        }
        AMOUNT = "0";
        Session["Datatable"] = dt;
        // AddTotalAmount();
    }


    private void AddGridEntry()
    {

        dt = Session["Datatable"] as DataTable;


        string partyId = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PARTY", "party_no", "acc_code='" + txtAcc.Text.Split('*')[1] + "'");
        if (dt.Rows.Count == 0)
        {
            SetDataColumn();
        }
        //for restricting user to select same ledger in same voucher
        else
        {
            if (ViewState["RowIndex"].ToString() == "-1")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(partyId) == Convert.ToInt32(dt.Rows[i]["id"]))
                    {
                        string str = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "party_name", "party_no=" + Convert.ToInt32(hdnIdEditParty.Value));

                        objCommon.DisplayUserMessage(UPDLedger, "Please select different account ledger as entry for the " + str + " is already done..!!", this);
                        return;
                    }
                }
            }
            if (ViewState["RowIndex"].ToString() != "-1")
            {
                if ((objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "party_no", "acc_code='" + txtAcc.Text.Split('*')[1].ToString() + "'")) != (ViewState["PartyId"] == null ? "0" : ViewState["PartyId"].ToString()))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(partyId) == Convert.ToInt32(dt.Rows[i]["id"]))
                        {
                            string str = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "party_name", "party_no=" + Convert.ToInt32(hdnIdEditParty.Value));

                            objCommon.DisplayUserMessage(UPDLedger, "Please select different account ledger as entry for the " + str + " is already done..!!", this);
                            return;
                        }
                    }
                }
            }
        }
        if (dt.Rows.Count < 1)
        {
            //RowIndex = -1;
            ViewState["RowIndex"] = -1;
        }

        //if (RowIndex != -1)
        if (ViewState["RowIndex"].ToString() != "-1")
        {
            if (isSingleMode == "Y")
            {
                UpdateDataRow(Convert.ToInt32(ViewState["RowIndex"].ToString()), dt);
                return;
            }
            else
            {
                UpdateDataRow_Journal(Convert.ToInt32(ViewState["RowIndex"].ToString()), dt);
                return;
            }
        }
        string TranMode = string.Empty;

        btnAdd.Enabled = true;
        //lblMsg.Text = "";
        //rowMsg.Style["Display"] = "none";
        DataRow row;
        row = dt.NewRow();
        row["Particulars"] = txtAcc.Text.ToString().Trim();
        row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

        if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
        {
            TranMode = "Cr";
        }
        else
        {
            TranMode = "Dr";
        }


        //if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
        //{
        //    TranMode = "Dr";
        //}
        //else
        //{
        //    TranMode = "Cr";
        //}

        string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1] + "'");
        int count = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Id"].ToString() == partyNo)
            {
                count++;
            }
        }
        if (count > 0)
        {
            PartyController objPC = new PartyController();
            DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

            if (dtr.Read())
            {
                lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
            }

            string balance = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {

                    if (dt.Rows[i]["Mode"].ToString() == "Dr")
                    {
                        balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                        ViewState["Balance2"] = balance;
                    }
                    else
                    {
                        balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                        ViewState["Balance2"] = balance;
                    }

                }
            }
        }

        if (TranMode.ToString().Trim() == "Dr")
        {
            if (ViewState["Balance2"].ToString().Trim() != "")
            {
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + 0.00);// Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim()))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                txtAgainstAcc.Focus();
                return;
            }
        }
        else
        {
            if (ViewState["Balance2"].ToString().Trim() != "")
            {
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - 0.00);// - Math.Abs(Convert.ToDouble(txtTranAmt.Text))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                txtAgainstAcc.Focus();
                return;
            }
        }

        if (isSingleMode == "Y")
        {
            GridData.Columns[4].Visible = true;
            GridData.Columns[5].Visible = false;
            GridData.Columns[6].Visible = false;
            row["Debit"] = "0.00";
            row["Credit"] = "0.00";
            row["Amount"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
        }
        else
        {
            GridData.Columns[4].Visible = false;
            GridData.Columns[5].Visible = true;
            GridData.Columns[6].Visible = true;

            if (TranMode.ToString().Trim() == "Dr")
            {
                row["Debit"] = "0.00";//String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                row["Credit"] = "0.00";
            }
            else
            {
                row["Debit"] = "0.00";
                row["Credit"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
            }

            row["Amount"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));// "0.00";

        }
        row["Mode"] = TranMode.ToString().Trim();
        row["Id"] = hdnIdEditParty.Value;
        if (ddlTranType.SelectedValue != "J")
        {
            row["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());
        }
        if (isPerNarration == "Y")
        {
            row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
            row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
        }
        else
        {
            row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
            row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
        }

        if (trCostCenter.Visible == true)
        {
            row["CCID"] = ddlCostCenter.SelectedValue;
        }
        else
        {
            row["CCID"] = 0;
        }

        if (trBudgetHead.Visible == true)
        {
            row["BudgetNo"] = ddlBudgetHead.SelectedValue;
            row["Departmentid"] = ddldepartment.SelectedValue;
        }
        else
        {
            row["BudgetNo"] = 0;
            row["Departmentid"] = 0;
        }

        row["ProjectSubId"] = ddlProjSubHead.SelectedValue;


        row["TDSAMOUNT"] = "0";
        row["TDSsection"] = "0";
        row["TDSpercentage"] = "0";

        //added by vijay andoju on 26082020 fro IGST
        row["IGSTAmount"] = "0";
        row["IGSTper"] = "0";
        row["IGSTonAmount"] = "0";

        row["CGSTAmount"] = "0";
        row["CGSTper"] = "0";
        row["CGSTonAmount"] = "0";

        row["SGSTAmount"] = "0";
        row["SGSTper"] = "0";
        row["SGSTonAmount"] = "0";
        //-------------------------------------------




        dt.Rows.Add(row);

        Session["Datatable"] = dt;
        //if (chkGST.Checked == true)
        //{
        //    AddGSTGrid();
        //}
        //if (chkIGST.Checked == true)
        //{
        //    AddIGSTGrid();
        //}

        if (chkTDSApplicable.Checked == true)
        {
            // if (txtTDSAmount.Text != "" || txtTDSAmount.Text != string.Empty || txtTDSPer.Text != string.Empty || txtTDSPer.Text != "")
            {
                AddTDSGrid();
            }
        }



        if (isSingleMode == "Y")
        {
            if (Convert.ToDouble(lblCurbal1.Text == "" ? "0" : lblCurbal1.Text) > 0)
            {
                //lblCrDr1.Text = "Cr";
                lblCrDr1.Text = "Dr";
            }
            else
            {
                //lblCrDr1.Text = "Dr";
                lblCrDr1.Text = "Cr";
            }
        }

    }
    protected void UpdateRowNew()
    {

        PartyController objPC = new PartyController();
        string TranMode = string.Empty;


        #region FirstRow
        DataRow row;
        row = dt.NewRow();


        row["Particulars"] = txtAcc.Text.ToString().Trim();
        row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

        if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
        {
            TranMode = "Cr";
        }
        else
        {
            TranMode = "Dr";
        }

        //if (Convert.ToDouble(txtTranAmt.Text) < 0)
        //{
        if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
        {
            TranMode = "Dr";
        }
        else
        {
            TranMode = "Cr";
        }
        // }
        string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1] + "'");
        int count = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Id"].ToString() == partyNo)
            {
                count++;
            }
        }
        if (count > 0)
        {

            DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

            if (dtr.Read())
            {
                lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
            }

            string balance = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {

                    if (dt.Rows[i]["Mode"].ToString() == "Dr")
                    {
                        balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                        ViewState["Balance2"] = balance;
                    }
                    else
                    {
                        balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                        ViewState["Balance2"] = balance;
                    }

                }
            }
        }

        if (TranMode.ToString().Trim() == "Dr")
        {
            if (ViewState["Balance2"].ToString().Trim() != "")
            {
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()));// + Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim()))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                txtAgainstAcc.Focus();
                return;
            }
        }
        else
        {
            if (ViewState["Balance2"].ToString().Trim() != "")
            {
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim())); //- Math.Abs(Convert.ToDouble(txtTranAmt.Text))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                txtAgainstAcc.Focus();
                return;
            }
        }

        if (isSingleMode == "Y")
        {
            GridData.Columns[4].Visible = true;
            GridData.Columns[5].Visible = false;
            GridData.Columns[6].Visible = false;
            row["Debit"] = "0.00";
            row["Credit"] = "0.00";
            // row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
        }
        else
        {
            GridData.Columns[4].Visible = false;
            GridData.Columns[5].Visible = true;
            GridData.Columns[6].Visible = true;

            if (TranMode.ToString().Trim() == "Dr")
            {
                row["Debit"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                row["Credit"] = "0.00";
            }
            else
            {
                row["Debit"] = "0.00";
                row["Credit"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
            }

            row["Amount"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));// "0.00";

        }
        row["Mode"] = TranMode.ToString().Trim();
        row["Id"] = hdnIdEditParty.Value;
        if (ddlTranType.SelectedValue != "J")
        {
            row["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());
        }
        if (isPerNarration == "Y")
        {
            row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
            row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
        }
        else
        {
            row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
            row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
        }

        if (trCostCenter.Visible == true)
        {
            row["CCID"] = ddlCostCenter.SelectedValue;
        }
        else
        {
            row["CCID"] = 0;
        }

        if (trBudgetHead.Visible == true)
        {
            row["BudgetNo"] = ddlBudgetHead.SelectedValue;
            row["Departmentid"] = ddldepartment.SelectedValue;
        }
        else
        {
            row["BudgetNo"] = 0;
            row["Departmentid"] = 0;
        }

        row["ProjectSubId"] = ddlProjSubHead.SelectedValue;


        row["TDSAMOUNT"] = "0";
        row["TDSsection"] = "0";
        row["TDSpercentage"] = "0";

        //added by vijay andoju on 26082020 fro IGST
        row["IGSTAmount"] = "0";
        row["IGSTper"] = "0";
        row["IGSTonAmount"] = "0";

        row["CGSTAmount"] = "0";
        row["CGSTper"] = "0";
        row["CGSTonAmount"] = "0";

        row["SGSTAmount"] = "0";
        row["SGSTper"] = "0";
        row["SGSTonAmount"] = "0";





        dt.Rows.Add(row);

        #endregion

        #region GSTROW
        if (ViewState["IsGST"].ToString() == "Yes")
        {

            row = dt.NewRow();
            // row["Particulars"] = txtCGST.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";
            }
            else
            {
                TranMode = "Dr";
            }

            //if (Convert.ToDouble(txtTranAmt.Text) < 0)
            //{
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";
            }
            else
            {
                TranMode = "Dr";
            }
            // }
            // partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtCGST.Text.ToString().Trim().Split('*')[1] + "'");
            count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {
                    count++;
                }
            }
            if (count > 0)
            {

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                    ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                }

                string balance = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {

                        if (dt.Rows[i]["Mode"].ToString() == "Dr")
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance2"] = balance;
                        }
                        else
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance2"] = balance;
                        }

                    }
                }
            }

            if (TranMode.ToString().Trim() == "Dr")
            {
                if (ViewState["Balance2"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()));//+ Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance2"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance2"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()));// - Math.Abs(Convert.ToDouble(txtCGSTAMT.Text))).ToString().Trim();
                    ViewState["Balance2"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }

            if (isSingleMode == "Y")
            {
                GridData.Columns[4].Visible = true;
                GridData.Columns[5].Visible = false;
                GridData.Columns[6].Visible = false;
                row["Debit"] = "0.00";
                row["Credit"] = "0.00";
                //  row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    // row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    //  row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim())));
                }

                row["Amount"] = "0.00";

            }
            row["Mode"] = TranMode.ToString().Trim();
            //row["Id"] = hdnIdEditParty.Value;
            row["Id"] = partyNo;
            // row["OppParty"] = Convert.ToInt32(txtCGST.Text.Trim().ToString().Split('*')[1]).ToString();
            if (isPerNarration == "Y")
            {
                row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
            }
            else
            {
                row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
            }

            if (trCostCenter.Visible == true)
            {
                row["CCID"] = ddlCostCenter.SelectedValue;
            }
            else
            {
                row["CCID"] = 0;
            }

            if (trBudgetHead.Visible == true)
            {
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;
            }
            else
            {
                row["BudgetNo"] = 0;
            }

            row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
            //Added by Vijay andoju 07-07-2020
            //row["Section"] = 0;

            row["TDSAMOUNT"] = "0.00";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0.00";

            //added by vijay andoju on 26082020 fro IGST
            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0.00";
            row["IGSTonAmount"] = "0.00";

            //row["CGSTAmount"] = txtCGSTAMT.Text;
            //row["CGSTper"] = txtCGSTPER.Text;
            //row["CGSTonAmount"] = txtCGSTAMOUNT1.Text;

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00"; ;





            dt.Rows.Add(row);
            Session["Datatable"] = dt;




            row = dt.NewRow();
            //row["Particulars"] = txtSGST.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";

            }
            else
            {
                TranMode = "Dr";
            }

            //if (Convert.ToDouble(txtTranAmt.Text) < 0)
            //{
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Dr";
            }
            else
            {
                TranMode = "Cr";
            }
            // }
            // partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtSGST.Text.ToString().Trim().Split('*')[1] + "'");
            count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {
                    count++;
                }
            }

            DataTableReader dtr1 = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

            if (dtr1.Read())
            {
                string tdsAmt = dtr1["BALANCE"].ToString().Trim();
                ViewState["Balance3"] = tdsAmt.ToString();// dtr["BALANCE"].ToString().Trim();
            }
            if (count > 0)
            {
                string balance = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {

                        if (dt.Rows[i]["Mode"].ToString() == "Dr")
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance3"] = balance;
                        }
                        else
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance3"] = balance;
                        }

                    }
                }
            }

            if (TranMode.ToString().Trim() == "Dr")
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()));//+ Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()));// - Math.Abs(Convert.ToDouble(txtSGSTAMT.Text))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }

            if (isSingleMode == "Y")
            {
                GridData.Columns[4].Visible = true;
                GridData.Columns[5].Visible = false;
                GridData.Columns[6].Visible = false;
                row["Debit"] = "0.00";
                row["Credit"] = "0.00";
                // row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    //  row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    // row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
                }

                row["Amount"] = "0.00";

            }
            row["Mode"] = TranMode.ToString().Trim();
            //row["Id"] = hdnIdEditParty.Value;
            row["Id"] = partyNo;
            row["OppParty"] = Convert.ToInt32(txtAcc.Text.Trim().ToString().Split('*')[1]).ToString();
            if (isPerNarration == "Y")
            {
                row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
            }
            else
            {
                row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
            }

            if (trCostCenter.Visible == true)
            {
                row["CCID"] = ddlCostCenter.SelectedValue;
            }
            else
            {
                row["CCID"] = 0;
            }

            if (trBudgetHead.Visible == true)
            {
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;

            }
            else
            {
                row["BudgetNo"] = 0;
            }

            row["ProjectSubId"] = ddlProjSubHead.SelectedValue;


            row["TDSAMOUNT"] = "0.00";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0.00";

            //added by vijay andoju on 26082020 fro IGST
            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0.00";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";


            //row["SGSTAmount"] = txtSGSTAMT.Text;
            //row["SGSTper"] = txtSGTSPer.Text;
            //row["SGSTonAmount"] = txtSGSTAMOUNT.Text;

            dt.Rows.Add(row);
            Session["Datatable"] = dt;
        }
        #endregion


        #region IGSTROW
        if (ViewState["IsIGST"].ToString() == "Yes")
        {

            row = dt.NewRow();
            // row["Particulars"] = txtIGST.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";


            }
            else
            {
                TranMode = "Dr";

            }

            //if (Convert.ToDouble(txtTranAmt.Text) < 0)
            //{
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Dr";
            }
            else
            {
                TranMode = "Cr";
            }
            //}
            // partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtIGST.Text.ToString().Trim().Split('*')[1] + "'");
            count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {
                    count++;
                }
            }

            DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

            if (dtr.Read())
            {
                string tdsAmt = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance3"] = tdsAmt.ToString();// dtr["BALANCE"].ToString().Trim();
            }
            if (count > 0)
            {
                string balance = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {

                        if (dt.Rows[i]["Mode"].ToString() == "Dr")
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance3"] = balance;
                        }
                        else
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance3"] = balance;
                        }

                    }
                }
            }

            if (TranMode.ToString().Trim() == "Dr")
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()));// + Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()));//- Math.Abs(Convert.ToDouble(txtIGSTAMT.Text))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }

            if (isSingleMode == "Y")
            {
                GridData.Columns[4].Visible = true;
                GridData.Columns[5].Visible = false;
                GridData.Columns[6].Visible = false;
                row["Debit"] = "0.00";
                row["Credit"] = "0.00";
                //  row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    // row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    //row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
                }

                row["Amount"] = "0.00";

            }
            row["Mode"] = TranMode.ToString().Trim();
            //row["Id"] = hdnIdEditParty.Value;
            row["Id"] = partyNo;
            row["OppParty"] = Convert.ToInt32(txtAcc.Text.Trim().ToString().Split('*')[1]).ToString();
            if (isPerNarration == "Y")
            {
                row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
            }
            else
            {
                row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
            }

            if (trCostCenter.Visible == true)
            {
                row["CCID"] = ddlCostCenter.SelectedValue;
            }
            else
            {
                row["CCID"] = 0;
            }

            if (trBudgetHead.Visible == true)
            {
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;

            }
            else
            {
                row["BudgetNo"] = 0;
            }

            row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
            //Added by Vijay andoju 07-07-2020

            row["TDSAMOUNT"] = "0.00";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0.00";

            //added by vijay andoju on 26082020 fro IGST
            //row["IGSTAmount"] = txtIGSTAMT.Text;
            //row["IGSTper"] = txtIGSTPER.Text;
            //row["IGSTonAmount"] = txtIGSTAMOUNT.Text;

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";


            dt.Rows.Add(row);
            Session["Datatable"] = dt;
        }

        #endregion

        #region TDS row
        if (ViewState["TDS"].ToString() == "Yes")
        {

            row = dt.NewRow();
            row["Particulars"] = txtTDSLedger.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                //TranMode = "Cr";
                // Added by vijay andoju 07-07-2020
                TranMode = "Dr";

            }
            else
            {
                //TranMode = "Dr";
                //Added by vijay andoju 07-07-2020
                TranMode = "Cr";
            }

            //if (Convert.ToDouble(txtTranAmt.Text) < 0)
            //{
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Dr";
            }
            else
            {
                TranMode = "Cr";
            }
            // }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTDSLedger.Text.ToString().Trim().Split('*')[1] + "'");
            count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {
                    count++;
                }
            }

            DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

            if (dtr.Read())
            {
                string tdsAmt = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance3"] = tdsAmt.ToString();// dtr["BALANCE"].ToString().Trim();
            }
            if (count > 0)
            {
                string balance = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {

                        if (dt.Rows[i]["Mode"].ToString() == "Dr")
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance3"] = balance;
                        }
                        else
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance3"] = balance;
                        }

                    }
                }
            }

            if (TranMode.ToString().Trim() == "Dr")
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()));// + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()));//- Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    txtAgainstAcc.Focus();
                    return;
                }
            }

            if (isSingleMode == "Y")
            {
                GridData.Columns[4].Visible = true;
                GridData.Columns[5].Visible = false;
                GridData.Columns[6].Visible = false;
                row["Debit"] = "0.00";
                row["Credit"] = "0.00";
                row["Amount"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                }

                row["Amount"] = "0.00";

            }
            row["Mode"] = TranMode.ToString().Trim();
            //row["Id"] = hdnIdEditParty.Value;
            row["Id"] = partyNo;
            row["OppParty"] = Convert.ToInt32(txtAcc.Text.Trim().ToString().Split('*')[1]).ToString();
            if (isPerNarration == "Y")
            {
                row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
            }
            else
            {
                row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
            }

            if (trCostCenter.Visible == true)
            {
                row["CCID"] = ddlCostCenter.SelectedValue;
            }
            else
            {
                row["CCID"] = 0;
            }

            if (trBudgetHead.Visible == true)
            {
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                row["BudgetNo"] = ddlBudgetHead.SelectedValue;

            }
            else
            {
                row["BudgetNo"] = 0;
            }

            row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
            //Added by Vijay andoju 07-07-2020

            row["TDSAMOUNT"] = "0.00";
            row["TDSsection"] = ddlSection.SelectedValue;
            row["TDSpercentage"] = "0.00";

            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";
            dt.Rows.Add(row);
            Session["Datatable"] = dt;
        }
        #endregion

        #region Delete
        int rows = dt.Rows.Count;
        //dt = RowDelete("Dld_id=1");
        //dt = RowDelete("Dld_id=2");
        //dt = RowDelete("Dld_id=3");
        //dt = RowDelete("Dld_id=4");
        DataRow[] drr = dt.Select("Dld_id>0 and Dld_id<5");
        for (int i = 0; i < drr.Length; i++)
            dt.Rows.Remove(drr[i]);
        dt.AcceptChanges();


        Session["Datatable"] = dt;


        #endregion

    }

    protected DataTable RowDelete(string Condition)
    {
        var Deleterow = dt.Select(Condition);
        foreach (var row in Deleterow)
            row.Delete();
        //  dt.Rows[row].AcceptChanges();
        return dt;
    }

    protected void AddDeleteId()
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow recRow = dt.Rows[i];
            if ((Convert.ToDouble(dt.Rows[i]["IGSTper"].ToString()) > 0))
            {
                if (ViewState["IsIGST"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "3";
                }

            }
            if ((Convert.ToDouble(dt.Rows[i]["SGSTper"].ToString()) > 0))
            {
                if (ViewState["IsGST"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "2";
                }

            }
            if ((Convert.ToDouble(dt.Rows[i]["CGSTper"].ToString()) > 0))
            {
                if (ViewState["IsGST"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "2";
                }

            }
            //if ((Convert.ToDouble(dt.Rows[i]["TDSPercentage"].ToString()) > 0))
            //{
            //    if (ViewState["TDS"].ToString() == "Yes")
            //    {
            //        dt.Rows[i]["Dld_id"] = "4";
            //    }
            //}
            //if (Convert.ToDouble(dt.Rows[i]["IGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["SGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["CGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TDSpercentage"].ToString()) == 0.00)
            //{
            //    if (dt.Rows[i]["Mode"].ToString() == "Cr")
            //    {
            //        dt.Rows[i]["Dld_id"] = "5";
            //    }
            //    if (dt.Rows[i]["Mode"].ToString() == "Dr")
            //    {
            //        dt.Rows[i]["Dld_id"] = "1";
            //    }

            //    dt.Rows[i].AcceptChanges();
            //}
        }
        Session["Datatable"] = dt;
        dt.DefaultView.Sort = "Dld_id";
        dt = dt.DefaultView.ToTable();
        // AddTotalAmount();
    }


    protected void SetGridEntryAlteration_New(DataTable dtRec)
    {

        for (int i = 0; i < dtRec.Rows.Count; i++)
        {
            //if (ViewState["TDS"].ToString() == "Yes")
            //{
            //if (Convert.ToDouble(dtRec.Rows[i]["CGSTpercent"].ToString()) == 0.00 && Convert.ToDouble(dtRec.Rows[i]["SGSTpercent"].ToString()) == 0.00 && Convert.ToDouble(dtRec.Rows[i]["TDSpercent"].ToString()) == 0.00 )
            //{

            DataRow row;
            row = dt.NewRow();
            string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
            row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
            row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
            row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

            if (isSingleMode == "Y")
            {
                row["Debit"] = "0.00";
                row["Credit"] = "0.00";
                //double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()) + Convert.ToDouble(dtRec.Rows[i + 1]["AMOUNT"].ToString().Trim()));
                double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                row["Amount"] = String.Format("{0:0.00}", amount);         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                {
                    //double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()) + Convert.ToDouble(dtRec.Rows[i + 1]["AMOUNT"].ToString().Trim()));
                    double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                    row["Debit"] = String.Format("{0:0.00}", amount);
                    row["Credit"] = "0.00";
                }
                else
                {
                    //double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()) + Convert.ToDouble(dtRec.Rows[i + 1]["AMOUNT"].ToString().Trim()));
                    double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));

                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", amount);
                }
                row["Amount"] = "0.00";


            }
            row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
            row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
            row["Mode"] = dtRec.Rows[i]["TRAN"].ToString().Trim();
            row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
            row["OppParty"] = dtRec.Rows[i]["OPARTY"].ToString().Trim(); ;
            if (isPerNarration == "Y")
            {
                row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();

            }
            else
            {
                row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
            }
            //Added by vijay andoju on 08-07-2020
            row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();


            row["ProjectSubId"] = dtRec.Rows[i]["ProjectId"].ToString().Trim();


            row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
            row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
            row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();

            //added by vijay andoju on 26082020 fro IGST
            row["IGSTAmount"] = dtRec.Rows[i]["IGSTAmount"].ToString().Trim();
            row["IGSTper"] = dtRec.Rows[i]["IGSTPercent"].ToString().Trim();
            row["IGSTonAmount"] = dtRec.Rows[i]["IGSTonAmount"].ToString().Trim();


            row["CGSTAmount"] = dtRec.Rows[i]["CGSTAmount"].ToString().Trim();
            row["CGSTper"] = dtRec.Rows[i]["CGSTPercent"].ToString().Trim();
            row["CGSTonAmount"] = dtRec.Rows[i]["CGSTonAmount"].ToString().Trim();

            row["SGSTAmount"] = dtRec.Rows[i]["SGSTAmount"].ToString().Trim();
            row["SGSTper"] = dtRec.Rows[i]["SGSTPercent"].ToString().Trim();
            row["SGSTonAmount"] = dtRec.Rows[i]["SGSTonAmount"].ToString().Trim();

            row["DepartmentId"] = dtRec.Rows[i]["DEPT_ID"].ToString().Trim();

            dt.Rows.Add(row);

            Session["Datatable"] = dt;




            //if (ViewState["IsIGST"].ToString() == "Yes")
            //{
            //    UpdateISGSTData(dtRec);

            //}
            //if (ViewState["IsGST"].ToString() == "Yes")
            //{
            //    UpdateGSTData(dtRec);


            //}
            //if (ViewState["TDS"].ToString() == "Yes")
            //{
            //    UpdateTDSData(dtRec);


            //}



            if (dtRec.Rows.Count == dt.Rows.Count)
            {
                break;
            }
        }
    }

    private void SetGridEntryAlteration(DataSet dst)
    {
        SetDataColumn();
        dt = Session["Datatable"] as DataTable;

        //if (RowIndex != -1)
        //{
        //    UpdateDataRow(RowIndex, dt);
        //    return;

        //}
        if (dst.Tables[0].Rows.Count != 0)
        {
            btnAdd.Enabled = true;
            //lblMsg.Text = "";
            //rowMsg.Style["Display"] = "none";

            DataView dv = new DataView();
            dv = dst.Tables[0].DefaultView;
            if (ddlTranType.SelectedValue.ToString().Trim() == "R")
            {
                //if(dv.Table.Rows[]["TRANSFER_ENTRY"]!= true)
                //{
                //int i = 0;

                //if(dv.Table.Rows[i]["TRANSFER_ENTRY"]== false)
                //{
                //}
                if (dv.Table.Rows[0]["SUBTR_NO"].ToString().Contains("0"))
                {
                    dv.RowFilter = "Tran='Dr'";
                }
                else
                {
                    dv.RowFilter = "SUBTR_NO<>0";
                    if (Convert.ToInt32(dv.Table.Rows[0]["SUBTR_NO"].ToString()) != 0)
                    {
                        dv.RowFilter = "SUBTR_NO<>0";
                    }
                    else
                    {
                        //dv.RowFilter = "PAYMENT_TYPE_NO=1";
                    }
                }
            }
            else if (ddlTranType.SelectedValue.ToString().Trim() == "P")
            {
                dv.RowFilter = "SUBTR_NO<>0";
                if (Convert.ToInt32(dv.Table.Rows[0]["SUBTR_NO"].ToString()) != 0)
                {
                    dv.RowFilter = "SUBTR_NO<>0";
                }
                else
                {
                    //  dv.RowFilter = "PAYMENT_TYPE_NO=1";
                }
            }
            else if (ddlTranType.SelectedValue.ToString().Trim() == "C")
            {
                dv.RowFilter = "SUBTR_NO<>0";
                if (Convert.ToInt32(dv.Table.Rows[0]["SUBTR_NO"].ToString()) != 0)
                {
                    dv.RowFilter = "SUBTR_NO<>0";
                }
            }

            DataTable dtContain = dv.ToTable();

            if (dtContain.Rows.Count == 0)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Editable.", this);
                VoucherNoSetting();
                //SetVoucherNo();
                ChangeThePageLayout();
                return;
            }

            if (isSingleMode == "Y")
            {
                if (dtContain.Rows.Count > 0)
                {
                    int y = 0;
                    for (y = 0; y < dtContain.Rows.Count; y++)
                    {
                        int u = 0;
                        for (u = 0; u < dtContain.Rows.Count; u++)
                        {
                            if (dtContain.Rows[y]["PARTY_NO"].ToString().Trim() == dtContain.Rows[u]["PARTY_NO"].ToString().Trim())
                            {
                                dtContain.Rows[y]["AMOUNT"] = (Convert.ToDouble(Convert.ToString(dtContain.Rows[y]["AMOUNT"]).Trim()) + Convert.ToDouble(Convert.ToString(dtContain.Rows[u]["PARTY_NO"].ToString().Trim()))).ToString().Trim();
                                if (u != y)
                                {
                                    dtContain.Rows[u].Delete();
                                }

                                dtContain.AcceptChanges();
                            }

                        }

                    }//1st loop
                    if (dtContain.Rows.Count > 0)
                    {
                        DataView dv1 = dtContain.DefaultView;
                        dv1.Sort = "AMOUNT";
                        dtContain = dv1.ToTable();
                    }
                    string acc_code1 = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtContain.Rows[0]["PARTY_NO"].ToString().Trim());
                    txtAgainstAcc.Text = dtContain.Rows[0]["LEDGER"].ToString().Trim() + "*" + acc_code1;
                    lblCurbal1.Text = dtContain.Rows[0]["BALANCE"].ToString().Trim();
                    hdnCurBalAg.Value = dtContain.Rows[0]["BALANCE"].ToString().Trim();
                    txtNarration.Text = dtContain.Rows[0]["PARTICULARS"].ToString().Trim();
                    txtChequeDt2.Text = dtContain.Rows[0]["CHQ_DATE"].ToString().Trim() == string.Empty ? DateTime.Now.Date.ToString("dd/MM/yyyy") : dtContain.Rows[0]["CHQ_DATE"].ToString().Trim();
                    txtChqNo2.Text = dtContain.Rows[0]["CHQ_NO"].ToString().Trim();
                    ddlBillNo.SelectedValue = dtContain.Rows[0]["BILL_ID"].ToString().Trim();
                    ddlSponsor.SelectedValue = dtContain.Rows[0]["ProjectId"].ToString().Trim();
                    objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
                    ddlProjSubHead.SelectedValue = dtContain.Rows[0]["ProjectSubId"].ToString().Trim();
                    ViewState["EditBal"] = dtContain.Rows[0]["BALANCE"].ToString().Trim();
                    int TDS = Convert.ToInt32(dtContain.Rows[0]["TDS"].ToString().Trim());
                    ViewState["IsIGST"] = dtContain.Rows[0]["IsIGSTApplicable"].ToString() == "1" ? "Yes" : "No";
                    ViewState["IsGST"] = dtContain.Rows[0]["IsSGST"].ToString() == "1" ? "Yes" : "No";
                    ViewState["Isbudget"] = dtContain.Rows[0]["BudgetNo"].ToString();
                    txtGSTNNO.Text = dtContain.Rows[0]["GSTIN_NO"].ToString();

                    txtPartyName.Text = dtContain.Rows[0]["ACC_PARTY_NAME"].ToString();
                    txtPanNo.Text = dtContain.Rows[0]["PAN_NO"].ToString();
                    txtNatureService.Text = dtContain.Rows[0]["NATURE_SERVICE"].ToString();


                    //if (Convert.ToInt32(ViewState["Isbudget"].ToString()) >0)
                    //{
                    //   trBudgetHead.Visible = true;
                    //    divDepartment.Visible = true;
                    //    objCostCenterController.BindBudgetHead(ddlBudgetHead);
                    //    objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

                    //    ddlBudgetHead.SelectedValue = dtContain.Rows[0]["BudgetNo"].ToString();
                    //    ddldepartment.SelectedValue = dtContain.Rows[0]["DEPT_ID"].ToString();
                    //}

                    if (TDS == 1)
                    {
                        ViewState["TDS"] = "Yes";
                    }
                    else
                    {
                        ViewState["TDS"] = "No";
                    }


                    if (Convert.ToDouble(lblCurbal1.Text.ToString().Trim()) > 0)
                    {
                        lblCrDr1.Text = "Cr";

                    }
                    else
                    {
                        lblCrDr1.Text = "Dr";
                    }
                }
            }

            DataTable dtRec;
            if (isSingleMode == "Y")
            {
                DataView dv2 = dst.Tables[0].DefaultView;
                dv2.RowFilter = "PARTY_NO <> " + dtContain.Rows[0]["PARTY_NO"].ToString().Trim();
                dtRec = dv2.ToTable();
            }
            else
            {
                dtRec = dst.Tables[0];

            }
            int i = 0;
            if (i == 0)
            {
                SetGridEntryAlteration_New(dtRec);
                txtNarration.Text = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                txtChequeDt2.Text = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim() == string.Empty ? DateTime.Now.Date.ToString("dd/MM/yyyy") : dtContain.Rows[0]["CHQ_DATE"].ToString().Trim();
                txtChqNo2.Text = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                ddlBillNo.SelectedValue = dtRec.Rows[i]["BILL_ID"].ToString().Trim();
                ddlSponsor.SelectedValue = dtRec.Rows[i]["ProjectId"].ToString().Trim();
               
                //added by tanu 29_12_2021
               // objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
               // objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
                ddlProjSubHead.SelectedValue = dtRec.Rows[i]["ProjectSubId"].ToString().Trim();
                ViewState["EditBal"] = dtContain.Rows[0]["BALANCE"].ToString().Trim();
                int TDS = Convert.ToInt32(dtContain.Rows[0]["TDS"].ToString().Trim());
                ViewState["IsIGST"] = dtContain.Rows[0]["IsIGSTApplicable"].ToString() == "1" ? "Yes" : "No";
                ViewState["IsGST"] = dtContain.Rows[0]["IsSGST"].ToString() == "1" ? "Yes" : "No";
                ViewState["Isbudget"] = dtContain.Rows[0]["BudgetNo"].ToString();
                txtGSTNNO.Text = dtContain.Rows[0]["GSTIN_NO"].ToString();
                txtPartyName.Text = dtContain.Rows[0]["ACC_PARTY_NAME"].ToString();
                txtPanNo.Text = dtContain.Rows[0]["PAN_NO"].ToString();
                txtNatureService.Text = dtContain.Rows[0]["NATURE_SERVICE"].ToString();

                // ADDED BY TANU 18_12_2021  FOR PAYMODE_CODE
                if (ddlPaymentMode.Items.Count == 0)
                {
                    objCommon.FillDropDownList(ddlPaymentMode, "ACC_PAYMODE", "PAYMODE_CODE", "PAYMODE", "", "PAYMODE");
                }
                ddlPaymentMode.SelectedValue = dtContain.Rows[0]["PAYMENT_MODE"].ToString();


                if (TDS == 1)
                {
                    ViewState["TDS"] = "Yes";
                }
                else
                {
                    ViewState["TDS"] = "No";
                }



                if (dt.Rows.Count != 0)
                {
                    AddDeleteId();
                    if (Convert.ToInt32(ViewState["Isbudget"].ToString()) > 0)
                    {
                        ViewState["IsBudgetHead"] = "Yes";
                    }
                    else
                    {
                        ViewState["IsBudgetHead"] = "No";
                    }
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        dt.Rows[j]["Narration"] = " ";
                        dt.Rows[j].AcceptChanges();
                    }
                    GridData.DataSource = dt;
                    GridData.DataBind();
                    //uncomment by ShirkantA on 20-12-2020
                    AddTotalAmount();
                }
                return;
            }

            if (ViewState["TDS"].ToString() == "Yes")
            {

                DataRow row;
                row = dt.NewRow();
                string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
                row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
                row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

                if (isSingleMode == "Y")
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    //double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()) + Convert.ToDouble(dtRec.Rows[i + 1]["AMOUNT"].ToString().Trim()));
                    double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                    row["Amount"] = String.Format("{0:0.00}", amount);         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                    {
                        //double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()) + Convert.ToDouble(dtRec.Rows[i + 1]["AMOUNT"].ToString().Trim()));
                        double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                        row["Debit"] = String.Format("{0:0.00}", amount);
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        //double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()) + Convert.ToDouble(dtRec.Rows[i + 1]["AMOUNT"].ToString().Trim()));
                        double amount = (Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));

                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", amount);
                    }
                    row["Amount"] = "0.00";


                }
                row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
                row["Mode"] = dtRec.Rows[i]["TRAN"].ToString().Trim();
                row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                row["OppParty"] = dtRec.Rows[i]["OPARTY"].ToString().Trim(); ;
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }
                //Added by vijay andoju on 08-07-2020
                row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
                row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
                row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();
                dt.Rows.Add(row);

                Session["Datatable"] = dt;

                if (ViewState["IsIGST"].ToString() == "Yes")
                {
                    UpdateISGSTData(dtRec);
                }
                if (ViewState["IsGST"].ToString() == "Yes")
                {
                    UpdateGSTData(dtRec);
                }
                UpdateTDSData(dtRec);

                txtNarration.Text = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                txtChequeDt2.Text = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim() == string.Empty ? DateTime.Now.Date.ToString("dd/MM/yyyy") : dtContain.Rows[0]["CHQ_DATE"].ToString().Trim();
                txtChqNo2.Text = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                ddlBillNo.SelectedValue = dtRec.Rows[i]["BILL_ID"].ToString().Trim();
                ddlSponsor.SelectedValue = dtRec.Rows[i]["ProjectId"].ToString().Trim();
             //   objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
                ddlProjSubHead.SelectedValue = dtRec.Rows[i]["ProjectSubId"].ToString().Trim();

            }


            else
            {
                for (i = 0; i < dtRec.Rows.Count; i++)
                {
                    DataRow row;
                    row = dt.NewRow();
                    string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
                    row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
                    row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                    row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

                    if (isSingleMode == "Y")
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = "0.00";
                        row["Amount"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                    }
                    else
                    {
                        if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                        {
                            row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                            row["Credit"] = "0.00";
                        }
                        else
                        {
                            row["Debit"] = "0.00";
                            row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                        }
                        row["Amount"] = "0.00";


                    }
                    row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
                    row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
                    row["Mode"] = dtRec.Rows[i]["TRAN"].ToString().Trim();
                    row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                    row["OppParty"] = dtRec.Rows[i]["OPARTY"].ToString().Trim();
                    if (isPerNarration == "Y")
                    {
                        row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                        row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                    }
                    else
                    {
                        row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                        row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                    }

                    row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();


                    row["ProjectSubId"] = dtRec.Rows[i]["ProjectId"].ToString().Trim();


                    row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
                    row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
                    row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();

                    //added by vijay andoju on 26082020 fro IGST
                    row["IGSTAmount"] = dtRec.Rows[i]["IGSTAmount"].ToString().Trim();
                    row["IGSTper"] = dtRec.Rows[i]["IGSTPercent"].ToString().Trim();
                    row["IGSTonAmount"] = dtRec.Rows[i]["IGSTonAmount"].ToString().Trim();


                    row["CGSTAmount"] = dtRec.Rows[i]["CGSTAmount"].ToString().Trim();
                    row["CGSTper"] = dtRec.Rows[i]["CGSTPercent"].ToString().Trim();
                    row["CGSTonAmount"] = dtRec.Rows[i]["CGSTonAmount"].ToString().Trim();

                    row["SGSTAmount"] = dtRec.Rows[i]["SGSTAmount"].ToString().Trim();
                    row["SGSTper"] = dtRec.Rows[i]["SGSTPercent"].ToString().Trim();
                    row["SGSTonAmount"] = dtRec.Rows[i]["SGSTonAmount"].ToString().Trim();

                    row["DepartmentId"] = dtRec.Rows[i]["DEPT_ID"].ToString().Trim();
                    dt.Rows.Add(row);

                    Session["Datatable"] = dt;


                    txtNarration.Text = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                    txtChequeDt2.Text = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim() == string.Empty ? DateTime.Now.Date.ToString("dd/MM/yyyy") : dtContain.Rows[0]["CHQ_DATE"].ToString().Trim();
                    txtChqNo2.Text = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    ddlBillNo.SelectedValue = dtRec.Rows[i]["BILL_ID"].ToString().Trim();
                    ddlSponsor.SelectedValue = dtRec.Rows[i]["ProjectId"].ToString().Trim();
                    objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
                    ddlProjSubHead.SelectedValue = dtRec.Rows[i]["ProjectSubId"].ToString().Trim();

                }
            }
            if (dt.Rows.Count != 0)
            {
                AddDeleteId();
                if (Convert.ToInt32(ViewState["Isbudget"].ToString()) > 0)
                {
                    ViewState["IsBudgetHead"] = "Yes";
                }
                else
                {
                    ViewState["IsBudgetHead"] = "No";
                }
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    dt.Rows[i]["Narration"] = "";
                    dt.Rows[i].AcceptChanges();
                }
                GridData.DataSource = dt;
                GridData.DataBind();
                AddTotalAmount();
            }
        }

    }


    private void UpdateTDSData(DataTable dtRec)
    {
        dt = Session["Datatable"] as DataTable;
        int rcount = dt.Rows.Count;
        for (int i = 0; i < dtRec.Rows.Count; i++)
        {
            if (Convert.ToDouble(dtRec.Rows[i]["TDSpercent"].ToString()) > 0)
            {
                DataRow row;
                row = dt.NewRow();

                string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
                row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
                row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

                if (isSingleMode == "Y")
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    row["Amount"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                    }
                    row["Amount"] = "0.00";


                }
                row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
                row["Mode"] = "Cr";
                row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                row["OppParty"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }

                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();


                row["ProjectSubId"] = dtRec.Rows[i]["ProjectId"].ToString().Trim();


                row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
                row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
                row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();

                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = dtRec.Rows[i]["IGSTAmount"].ToString().Trim();
                row["IGSTper"] = dtRec.Rows[i]["IGSTPercent"].ToString().Trim();
                row["IGSTonAmount"] = dtRec.Rows[i]["IGSTonAmount"].ToString().Trim();


                row["CGSTAmount"] = dtRec.Rows[i]["CGSTAmount"].ToString().Trim();
                row["CGSTper"] = dtRec.Rows[i]["CGSTPercent"].ToString().Trim();
                row["CGSTonAmount"] = dtRec.Rows[i]["CGSTonAmount"].ToString().Trim();

                row["SGSTAmount"] = dtRec.Rows[i]["SGSTAmount"].ToString().Trim();
                row["SGSTper"] = dtRec.Rows[i]["SGSTPercent"].ToString().Trim();
                row["SGSTonAmount"] = dtRec.Rows[i]["SGSTonAmount"].ToString().Trim();

                row["DepartmentId"] = dtRec.Rows[i]["DEPT_ID"].ToString().Trim();
                dt.Rows.Add(row);

                Session["Datatable"] = dt;

            }
            //if (i == rcount)
            //{
            //    DataRow row;
            //    row = dt.NewRow();

            //    string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
            //    row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
            //    row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
            //    row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

            //    if (isSingleMode == "Y")
            //    {
            //        row["Debit"] = "0.00";
            //        row["Credit"] = "0.00";
            //        row["Amount"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            //    }
            //    else
            //    {
            //        if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
            //        {
            //            row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
            //            row["Credit"] = "0.00";
            //        }
            //        else
            //        {
            //            row["Debit"] = "0.00";
            //            row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
            //        }
            //        row["Amount"] = "0.00";


            //    }
            //    row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
            //    row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
            //    row["Mode"] = dtRec.Rows[i]["Tran"].ToString().Trim();

            //    row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
            //    row["OppParty"] = dtRec.Rows[i]["OPARTY"].ToString().Trim();
            //    if (isPerNarration == "Y")
            //    {
            //        row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
            //        row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
            //    }
            //    else
            //    {
            //        row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
            //        row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
            //    }
            //    row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
            //    row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
            //    row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();


            //    dt.Rows.Add(row);

            //    Session["Datatable"] = dt;
            //}
        }
    }

    //Added by vijay andoju vijay for gst igst
    private void UpdateISGSTData(DataTable dtRec)
    {
        dt = Session["Datatable"] as DataTable;
        for (int i = 0; i < dtRec.Rows.Count; i++)
        {
            if (Convert.ToDouble(dtRec.Rows[i]["IGSTpercent"].ToString()) > 0)
            {
                DataRow row;
                row = dt.NewRow();

                string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
                row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
                row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

                if (isSingleMode == "Y")
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    row["Amount"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                    }
                    row["Amount"] = "0.00";


                }
                row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
                row["Mode"] = "Cr";
                row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                row["OppParty"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }

                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();


                row["ProjectSubId"] = dtRec.Rows[i]["ProjectId"].ToString().Trim();


                row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
                row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
                row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();

                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = dtRec.Rows[i]["IGSTAmount"].ToString().Trim();
                row["IGSTper"] = dtRec.Rows[i]["IGSTPercent"].ToString().Trim();
                row["IGSTonAmount"] = dtRec.Rows[i]["IGSTonAmount"].ToString().Trim();


                row["CGSTAmount"] = dtRec.Rows[i]["CGSTAmount"].ToString().Trim();
                row["CGSTper"] = dtRec.Rows[i]["CGSTPercent"].ToString().Trim();
                row["CGSTonAmount"] = dtRec.Rows[i]["CGSTonAmount"].ToString().Trim();

                row["SGSTAmount"] = dtRec.Rows[i]["SGSTAmount"].ToString().Trim();
                row["SGSTper"] = dtRec.Rows[i]["SGSTPercent"].ToString().Trim();
                row["SGSTonAmount"] = dtRec.Rows[i]["SGSTonAmount"].ToString().Trim();

                row["DepartmentId"] = dtRec.Rows[i]["DEPT_ID"].ToString().Trim();
                dt.Rows.Add(row);


                Session["Datatable"] = dt;

            }

        }
    }

    private void UpdateGSTData(DataTable dtRec)
    {
        dt = Session["Datatable"] as DataTable;
        for (int i = 0; i < dtRec.Rows.Count; i++)
        {
            if (Convert.ToDouble(dtRec.Rows[i]["CGSTpercent"].ToString()) > 0)
            {
                DataRow row;
                row = dt.NewRow();

                string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
                row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
                row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

                if (isSingleMode == "Y")
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    row["Amount"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                    }
                    row["Amount"] = "0.00";


                }
                row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
                row["Mode"] = dtRec.Rows[i]["Tran"].ToString().Trim();
                row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                row["OppParty"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }


                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();


                row["ProjectSubId"] = dtRec.Rows[i]["ProjectId"].ToString().Trim();


                row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
                row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
                row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();

                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = dtRec.Rows[i]["IGSTAmount"].ToString().Trim();
                row["IGSTper"] = dtRec.Rows[i]["IGSTPercent"].ToString().Trim();
                row["IGSTonAmount"] = dtRec.Rows[i]["IGSTonAmount"].ToString().Trim();


                row["CGSTAmount"] = dtRec.Rows[i]["CGSTAmount"].ToString().Trim();
                row["CGSTper"] = dtRec.Rows[i]["CGSTPercent"].ToString().Trim();
                row["CGSTonAmount"] = dtRec.Rows[i]["CGSTonAmount"].ToString().Trim();

                row["SGSTAmount"] = dtRec.Rows[i]["SGSTAmount"].ToString().Trim();
                row["SGSTper"] = dtRec.Rows[i]["SGSTPercent"].ToString().Trim();
                row["SGSTonAmount"] = dtRec.Rows[i]["SGSTonAmount"].ToString().Trim();

                row["DepartmentId"] = dtRec.Rows[i]["DEPT_ID"].ToString().Trim();
                dt.Rows.Add(row);
                Session["Datatable"] = dt;

            }
            else if (Convert.ToDouble(dtRec.Rows[i]["SGSTpercent"].ToString()) > 0)
            {
                DataRow row;
                row = dt.NewRow();

                string acc_code = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "ACC_CODE", "PARTY_NO=" + dtRec.Rows[i]["PARTY_NO"].ToString().Trim());
                row["Particulars"] = dtRec.Rows[i]["LEDGER"].ToString().Trim() + "*" + acc_code;
                row["Narration"] = dtRec.Rows[i]["PARTICULARS"].ToString().Trim();
                row["Balance"] = dtRec.Rows[i]["BALANCE"].ToString().Trim();

                if (isSingleMode == "Y")
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    row["Amount"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    if (dtRec.Rows[i]["TRAN"].ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Convert.ToDouble(dtRec.Rows[i]["AMOUNT"].ToString().Trim()));
                    }
                    row["Amount"] = "0.00";


                }
                row["CCID"] = dtRec.Rows[i]["CC_ID"].ToString().Trim();
                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();
                row["Mode"] = dtRec.Rows[i]["Tran"].ToString().Trim();
                row["Id"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                row["OppParty"] = dtRec.Rows[i]["PARTY_NO"].ToString().Trim();
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = dtRec.Rows[i]["CHQ_NO"].ToString().Trim();
                    row["ChqDate"] = dtRec.Rows[i]["CHQ_DATE"].ToString().Trim();
                }


                row["BudgetNo"] = dtRec.Rows[i]["BudgetNo"].ToString().Trim();


                row["ProjectSubId"] = dtRec.Rows[i]["ProjectId"].ToString().Trim();


                row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
                row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
                row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();

                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = dtRec.Rows[i]["IGSTAmount"].ToString().Trim();
                row["IGSTper"] = dtRec.Rows[i]["IGSTPercent"].ToString().Trim();
                row["IGSTonAmount"] = dtRec.Rows[i]["IGSTonAmount"].ToString().Trim();


                row["CGSTAmount"] = dtRec.Rows[i]["CGSTAmount"].ToString().Trim();
                row["CGSTper"] = dtRec.Rows[i]["CGSTPercent"].ToString().Trim();
                row["CGSTonAmount"] = dtRec.Rows[i]["CGSTonAmount"].ToString().Trim();

                row["SGSTAmount"] = dtRec.Rows[i]["SGSTAmount"].ToString().Trim();
                row["SGSTper"] = dtRec.Rows[i]["SGSTPercent"].ToString().Trim();
                row["SGSTonAmount"] = dtRec.Rows[i]["SGSTonAmount"].ToString().Trim();

                row["DepartmentId"] = dtRec.Rows[i]["DEPT_ID"].ToString().Trim();
                dt.Rows.Add(row);
                Session["Datatable"] = dt;

            }
        }
    }

    private void SetDataColumn()
    {
        DataSet dtTmp = objCommon.FillDropDown("Acc_Temp_Voucher_Transaction", "*", "", "IpAddress='" + Session["ComputerIDAddress"].ToString().Trim() + "'", string.Empty);
        if (dtTmp.Tables[0].Rows.Count > 0)
        {
            dt = dtTmp.Tables[0];
            DataColumn dc11 = new DataColumn();
            dc11.ColumnName = "CCID";
            dt.Columns.Add(dc11);

            DataColumn dc12 = new DataColumn();
            dc12.ColumnName = "BILL_ID";
            dt.Columns.Add(dc12);

            dt = dtTmp.Tables[0];
            DataColumn dc13 = new DataColumn();
            dc11.ColumnName = "BudgetNo";
            dt.Columns.Add(dc13);


            Session["Datatable"] = dt;
            if (dt.Rows[0]["TranMode"].ToString().Trim() == "J")
            {
                isSingleMode = "N";
            }
            if (isSingleMode == "Y")
            {
                txtAgainstAcc.Text = dt.Rows[0]["Particulars"].ToString().Trim();
                lblCurbal1.Text = dt.Rows[0]["Balance"].ToString().Trim();
                hdnCurBalAg.Value = dt.Rows[0]["Balance"].ToString().Trim();
                lblCrDr1.Text = dt.Rows[0]["Mode"].ToString().Trim();
                ViewState["EditBal"] = dt.Rows[0]["Balance"].ToString().Trim();
                //dt.Rows[0].Delete();
                //dt.AcceptChanges();
                //Session["Datatable"] = dt;

            }
            ddlTranType.SelectedValue = dt.Rows[0]["TranMode"].ToString().Trim();
            dt.Rows[0].Delete();
            dt.AcceptChanges();
            Session["Datatable"] = dt;
            ddlTranType.Enabled = false;
            DataTableToGrid();
        }

        else
        {
            //ddlTranType.Enabled = true;  Vidisha
            if (dt == null)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = "Particulars";
                dt.Columns.Add(dc);

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "Balance";
                dt.Columns.Add(dc1);

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "Narration";
                dt.Columns.Add(dc2);

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "Amount";
                dt.Columns.Add(dc3);

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "Debit";
                dt.Columns.Add(dc4);

                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "Credit";
                dt.Columns.Add(dc5);

                DataColumn dc6 = new DataColumn();
                dc6.ColumnName = "Mode";
                dt.Columns.Add(dc6);

                DataColumn dc7 = new DataColumn();
                dc7.ColumnName = "Id";
                dt.Columns.Add(dc7);

                DataColumn dc8 = new DataColumn();
                dc8.ColumnName = "ChqNo";
                dt.Columns.Add(dc8);

                DataColumn dc9 = new DataColumn();
                dc9.ColumnName = "ChqDate";
                dt.Columns.Add(dc9);

                DataColumn dc10 = new DataColumn();
                dc10.ColumnName = "OppParty";
                dt.Columns.Add(dc10);

                DataColumn dc11 = new DataColumn();
                dc11.ColumnName = "CCID";
                dt.Columns.Add(dc11);

                DataColumn dc12 = new DataColumn();
                dc12.ColumnName = "ProjectSubId";
                dt.Columns.Add(dc12);

                DataColumn dc13 = new DataColumn();
                dc13.ColumnName = "BILL_ID";
                dt.Columns.Add(dc13);

                DataColumn dc14 = new DataColumn();
                dc14.ColumnName = "BudgetNo";
                dt.Columns.Add(dc14);


                DataColumn dc15 = new DataColumn();
                dc15.ColumnName = "TDSsection";
                dt.Columns.Add(dc15);

                DataColumn dc16 = new DataColumn();
                dc16.ColumnName = "TDSAMOUNT";
                dt.Columns.Add(dc16);

                DataColumn dc17 = new DataColumn();
                dc17.ColumnName = "TDSonAmount";
                dt.Columns.Add(dc17);

                DataColumn dc18 = new DataColumn();
                dc18.ColumnName = "TDSpercentage";
                dt.Columns.Add(dc18);

                DataColumn dc19 = new DataColumn();
                dc19.ColumnName = "DepartmentId";
                dt.Columns.Add(dc19);
                //added by vijay andoju on 26-08-2020 for GST & IGST functiionality
                DataColumn dc20 = new DataColumn();
                dc20.ColumnName = "IGSTAmount";
                dt.Columns.Add(dc20);

                DataColumn dc21 = new DataColumn();
                dc21.ColumnName = "IGSTonAmount";
                dt.Columns.Add(dc21);

                DataColumn dc22 = new DataColumn();
                dc22.ColumnName = "IGSTper";
                dt.Columns.Add(dc22);

                DataColumn dc23 = new DataColumn();
                dc23.ColumnName = "CGSTAmount";
                dt.Columns.Add(dc23);

                DataColumn dc24 = new DataColumn();
                dc24.ColumnName = "CGSTonAmount";
                dt.Columns.Add(dc24);

                DataColumn dc25 = new DataColumn();
                dc25.ColumnName = "CGSTper";
                dt.Columns.Add(dc25);

                DataColumn dc26 = new DataColumn();
                dc26.ColumnName = "SGSTAmount";
                dt.Columns.Add(dc26);

                DataColumn dc27 = new DataColumn();
                dc27.ColumnName = "SGSTonAmount";
                dt.Columns.Add(dc27);

                DataColumn dc28 = new DataColumn();
                dc28.ColumnName = "SGSTper";
                dt.Columns.Add(dc28);

                DataColumn dc29 = new DataColumn();
                dc29.ColumnName = "Dld_id";
                dt.Columns.Add(dc29);




                Session["Datatable"] = dt;

            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    if (dt.Columns.Count == 0)
                    {
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "Particulars";
                        dt.Columns.Add(dc);

                        DataColumn dc1 = new DataColumn();
                        dc1.ColumnName = "Balance";
                        dt.Columns.Add(dc1);

                        DataColumn dc2 = new DataColumn();
                        dc2.ColumnName = "Narration";
                        dt.Columns.Add(dc2);

                        DataColumn dc3 = new DataColumn();
                        dc3.ColumnName = "Amount";
                        dt.Columns.Add(dc3);

                        DataColumn dc4 = new DataColumn();
                        dc4.ColumnName = "Debit";
                        dt.Columns.Add(dc4);

                        DataColumn dc5 = new DataColumn();
                        dc5.ColumnName = "Credit";
                        dt.Columns.Add(dc5);

                        DataColumn dc6 = new DataColumn();
                        dc6.ColumnName = "Mode";
                        dt.Columns.Add(dc6);

                        DataColumn dc7 = new DataColumn();
                        dc7.ColumnName = "Id";
                        dt.Columns.Add(dc7);

                        DataColumn dc8 = new DataColumn();
                        dc8.ColumnName = "ChqNo";
                        dt.Columns.Add(dc8);

                        DataColumn dc9 = new DataColumn();
                        dc9.ColumnName = "ChqDate";
                        dt.Columns.Add(dc9);

                        DataColumn dc10 = new DataColumn();
                        dc10.ColumnName = "OppParty";
                        dt.Columns.Add(dc10);

                        DataColumn dc11 = new DataColumn();
                        dc11.ColumnName = "CCID";
                        dt.Columns.Add(dc11);

                        DataColumn dc12 = new DataColumn();
                        dc12.ColumnName = "ProjectSubId";
                        dt.Columns.Add(dc12);

                        DataColumn dc13 = new DataColumn();
                        dc13.ColumnName = "BILL_ID";
                        dt.Columns.Add(dc13);

                        DataColumn dc14 = new DataColumn();
                        dc14.ColumnName = "BudgetNo";
                        dt.Columns.Add(dc14);

                        DataColumn dc15 = new DataColumn();
                        dc15.ColumnName = "TDSsection";
                        dt.Columns.Add(dc15);

                        DataColumn dc16 = new DataColumn();
                        dc16.ColumnName = "TDSAMOUNT";
                        dt.Columns.Add(dc16);

                        DataColumn dc17 = new DataColumn();
                        dc17.ColumnName = "TDSonAmount";
                        dt.Columns.Add(dc17);

                        DataColumn dc18 = new DataColumn();
                        dc18.ColumnName = "TDSpercentage";
                        dt.Columns.Add(dc18);

                        DataColumn dc19 = new DataColumn();
                        dc19.ColumnName = "DepartmentId";
                        dt.Columns.Add(dc19);

                        DataColumn dc20 = new DataColumn();
                        dc20.ColumnName = "IGSTAmount";
                        dt.Columns.Add(dc20);

                        DataColumn dc21 = new DataColumn();
                        dc21.ColumnName = "IGSTonAmount";
                        dt.Columns.Add(dc21);

                        DataColumn dc22 = new DataColumn();
                        dc22.ColumnName = "IGSTper";
                        dt.Columns.Add(dc22);

                        DataColumn dc23 = new DataColumn();
                        dc23.ColumnName = "CGSTAmount";
                        dt.Columns.Add(dc23);

                        DataColumn dc24 = new DataColumn();
                        dc24.ColumnName = "CGSTonAmount";
                        dt.Columns.Add(dc24);

                        DataColumn dc25 = new DataColumn();
                        dc25.ColumnName = "CGSTper";
                        dt.Columns.Add(dc25);

                        DataColumn dc26 = new DataColumn();
                        dc26.ColumnName = "SGSTAmount";
                        dt.Columns.Add(dc26);

                        DataColumn dc27 = new DataColumn();
                        dc27.ColumnName = "SGSTonAmount";
                        dt.Columns.Add(dc27);

                        DataColumn dc28 = new DataColumn();
                        dc28.ColumnName = "SGSTper";
                        dt.Columns.Add(dc28);

                        DataColumn dc29 = new DataColumn();
                        dc29.ColumnName = "Dld_id";
                        dt.Columns.Add(dc29);


                    }
                }
            }
            Session["Datatable"] = dt;
        }
    }

    private void SetTransactionType()
    {
        if (isSingleMode == "N")
        {
            if (GridData.Rows.Count == 0)
            {
                if (ddlTranType.SelectedValue.ToString().Trim() == "P")
                {
                    ddlcrdr.SelectedIndex = 0;

                }
                else if (ddlTranType.SelectedValue.ToString().Trim() == "J")
                {
                    isSingleMode = "N";
                    ddlcrdr.SelectedIndex = 0;
                    ChangeThePageLayout();
                    //Added by vijay on 270820202
                    chkTDSApplicable.Visible = true;
                    //chkIGST.Visible = true;
                    //chkGST.Visible = true;

                }
                else if (ddlTranType.SelectedValue.ToString().Trim() == "C")
                {
                    ddlcrdr.SelectedIndex = 1;

                }
                else if (ddlTranType.SelectedValue.ToString().Trim() == "R")
                {
                    ddlcrdr.SelectedIndex = 1;

                }
                ddlcrdr.Enabled = true;
            }
            else
            { ddlcrdr.Enabled = true; }

        }
        else
        {

            ddlcrdr.Enabled = true;

            if (ddlTranType.SelectedValue.ToString().Trim() == "P")
            {
                ddlcrdr.SelectedIndex = 0;


            }
            else if (ddlTranType.SelectedValue.ToString().Trim() == "J")
            {
                ddlcrdr.SelectedIndex = 1;

            }
            else if (ddlTranType.SelectedValue.ToString().Trim() == "C")
            {
                ddlcrdr.SelectedIndex = 0;

            }
            else if (ddlTranType.SelectedValue.ToString().Trim() == "R")
            {
                ddlcrdr.SelectedIndex = 1;

            }
            ddlcrdr.Enabled = false;

        }


    }

    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCostCenterController.BindBudgetHead(ddlBudgetHead);
        objCommon.FillDropDownList(ddlBillNo, "ACC_" + Session["comp_code"].ToString() + "_BILL_FILE", "BILL_ID", "BILLNO", "BILLSTATUS='A'", "BILLDATE desc");
        objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
        objCommon.FillDropDownList(ddlCostCenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");

        ClearAll();
        clearGst();
        clearTax();
        SetTransactionType();
        SetWithoutCashBank();

        if (isSingleMode == "Y")
            txtAgainstAcc.Focus();
        else
            txtAcc.Focus();

        VoucherNoSetting();

        if (ddlTranType.SelectedIndex > 0)
            tranTypeForStrVno = ddlTranType.SelectedValue.ToString();
        if (ddlTranType.SelectedValue == "P")


            if (ddlTranType.SelectedValue == "C")
            {
                trSponsor.Visible = false;
                //trSubHead.Visible = false;
            }
            else
            {
                trSponsor.Visible = true;
                //trSubHead.Visible = true;
            }
        Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
    }

    private Boolean ValidateLedger(string Ledger, string IsAgainstLedger)
    {
        string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + Ledger.Trim().Split('*')[1] + "'");

        objCommon = new Common();
        string[] led = Ledger.ToString().Trim().Split('*');
        if (led.Length == 2)
        {
            //if (IsNumeric(led[0].ToString().Trim()) == true)
            //{
            led[1] = led[1].ToString().Replace("¯", "");
            DataSet ds;
            if (IsAgainstLedger == "Y")
            {

                ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PARTY_NO=" + partyNo + " AND PARTY_NAME ='" + led[0].ToString().Trim() + "' and PAYMENT_TYPE_NO IN ('1','2')", string.Empty);
            }
            else if (IsAgainstLedger == "N")
            {
                ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PARTY_NO=" + partyNo.Trim() + " AND PARTY_NAME ='" + led[0].ToString().Trim() + "' and PAYMENT_TYPE_NO NOT IN ('1','2')", string.Empty);
            }
            else
            {
                ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PARTY_NO=" + partyNo.Trim() + " AND PARTY_NAME ='" + led[0].ToString().Trim() + "' ", string.Empty);
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Entered.", this);
                    return false;
                }
            }
            else
            {
                return false;
            }
            //}
            //else
            //{
            //    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Entered.", this);
            //    return false;
            //}
        }
        else
        {
            objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Entered.", this);
            return false;
        }
        return true;
    }

    private void DataTableToGrid()
    {
        if (dt != null)
        {
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Narration"] = "";
                dt.Rows[i].AcceptChanges();
            }
            GridData.DataSource = dt;
            GridData.DataBind();
            if (dt.Rows.Count != 0)
            {
                if (isSingleMode == "Y")
                {
                    txtAgainstAcc.Enabled = false;
                }
            }
            else
            {
                txtAgainstAcc.Enabled = true;
            }

            AddTotalAmount();
            //RowIndex = -1;
            ViewState["RowIndex"] = -1;

            if (isPerNarration == "Y")
            {
                GridData.Columns[3].Visible = false;
            }
            else
            {
                GridData.Columns[3].Visible = false;
            }

            if (isSingleMode == "Y")
            {
                GridData.Columns[4].Visible = true;
                GridData.Columns[5].Visible = false;
                GridData.Columns[6].Visible = false;
                lblTotalDiff.Visible = false;
                lblTotalDebit0.Visible = false;
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;
                lblTotalDiff.Visible = true;
                lblTotalDebit0.Visible = true;
            }
        }
        else
        {
            GridData.DataSource = null;
            GridData.DataBind();
        }
        // txtTranAmt.Text = "";
        txtAcc.Text = "";
        txtPerNarration.Text = "";
        txtChequeDt1.Text = "";
        txtChqNo1.Text = "";
        lblCurBal2.Text = "";

        if (isSingleMode == "Y")
            txtAcc.Focus();
        else
            ddlcrdr.Focus();
    }

    private Boolean ValidateCashBankLedger(string Ledger)
    {
        DataSet dsCB;
        dsCB = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PARTY_NO IN (" + Ledger.ToString().Trim() + ") and PAYMENT_TYPE_NO IN ('1','2')", string.Empty);
        if (dsCB != null)
        {
            if (dsCB.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Cash Or Bank Ledger Not Available In Currrent Transaction.", this);
                return false;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(UPDLedger, "Cash Or Bank Ledger Not Available In Currrent Transaction.", this);
            return false;

        }
        //return true;
    }


    //code for bulk vouchar

    protected void ddlPayeeNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayeeNature.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlName, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + (ddlPayeeNature.SelectedValue), "");
        }
        else
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select Payee Nature. ", this);
        }
    }
    private DataTable CreateTabelbulkvouchr()
    {
        DataTable bulkvouchr = new DataTable();
        bulkvouchr.Columns.Add(new DataColumn("SRNO", typeof(int)));
        bulkvouchr.Columns.Add(new DataColumn("PARTYNAME", typeof(string)));
        bulkvouchr.Columns.Add(new DataColumn("EMPID", typeof(int)));
        bulkvouchr.Columns.Add(new DataColumn("ISEMP", typeof(int)));
        bulkvouchr.Columns.Add(new DataColumn("Remark", typeof(string)));
        bulkvouchr.Columns.Add(new DataColumn("ChequeNo", typeof(string)));
        bulkvouchr.Columns.Add(new DataColumn("ChequeDate", typeof(DateTime)));
        bulkvouchr.Columns.Add(new DataColumn("AMOUNT1", typeof(decimal)));
        bulkvouchr.Columns.Add(new DataColumn("AMOUNT2", typeof(decimal)));
        bulkvouchr.Columns.Add(new DataColumn("AMOUNT3", typeof(decimal)));

        return bulkvouchr;
    }

    private void AddExistingEmpData()
    {
        DataTable dtItem = this.CreateTabelbulkvouchr();
        DataRow lvdr = null;

        //DataTable dt = ((DataTable)Session["Datatable"]);
        //int datacount = dt.Rows.Count;

        foreach (ListViewItem i in lvFields.Items)
        {
            HiddenField hdnSRNO = i.FindControl("hdnSRNO") as HiddenField;
            HiddenField hdnFieldsCount = i.FindControl("hdnFieldsCount") as HiddenField;
            Label lblName = i.FindControl("lblName") as Label;

          
            //Label lblRemark = i.FindControl("lblRemark") as Label;
            //Label lblChequeNo = i.FindControl("lblChequeNo") as Label;
            //Label lblChequeDate = i.FindControl("lblChequeDate") as Label;
            TextBox txtamount = i.FindControl("txtamount") as TextBox;
            TextBox txtamount1 = i.FindControl("txtamount1") as TextBox;
            TextBox txtamount2 = i.FindControl("txtamount2") as TextBox;


            lvdr = dtItem.NewRow();


            lvdr["SRNO"] = hdnSRNO.Value;
            lvdr["PARTYNAME"] = lblName.Text;
            lvdr["EmpId"] = hdnFieldsCount.Value;
            lvdr["ISEMP"] = 1;
            //lvdr["Remark"] = lblRemark.Text;
            //lvdr["ChequeNo"] = lblChequeNo.Text;
            //lvdr["ChequeDate"] = lblChequeDate.Text;
            lvdr["Remark"] = "";
            lvdr["ChequeNo"] = "";
            lvdr["ChequeDate"] = "9999-12-31";

            //if (datacount == 1)

            //{
            lvdr["AMOUNT1"] = txtamount.Text == "" ? 0 : Convert.ToDecimal(txtamount.Text);
            lvdr["AMOUNT2"] = txtamount1.Text == "" ? 0 : Convert.ToDecimal(txtamount1.Text);
            lvdr["AMOUNT3"] = txtamount2.Text == "" ? 0 : Convert.ToDecimal(txtamount2.Text);


            //}
            dtItem.Rows.Add(lvdr);
        }

        ViewState["RecTblbuklvouchr"] = dtItem;
    }

    protected void btnAddvendor_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = ((DataTable)Session["Datatable"]);

            if (dt.Rows.Count == 0)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Add Ledger", this);
                return;
            }

            lvFields.Visible = true;
            AddExistingEmpData();

            DataTable bulkvouchrs = (DataTable)ViewState["RecTblbuklvouchr"];
            if (GetChekableDatarow(bulkvouchrs, ddlName.SelectedItem.Text))
            {
                lvFields.DataSource = bulkvouchrs;
                lvFields.DataBind();
                ddlName.SelectedItem.Text = string.Empty;
                objCommon.DisplayUserMessage(UPDLedger, "This Payee is already exist.", this.Page);
                return;
            }
            if (GetChekableDatarow(bulkvouchrs, ddlEmployee.SelectedItem.Text))
            {
                lvFields.DataSource = bulkvouchrs;
                lvFields.DataBind();
                ddlName.SelectedItem.Text = string.Empty;
                objCommon.DisplayUserMessage(UPDLedger, "This Employee is already exist.", this.Page);
                return;
            }
           
            if (ViewState["RecTblbuklvouchr"] != null && ((DataTable)ViewState["RecTblbuklvouchr"]) != null)
            {

                DataTable bulkvouchr = (DataTable)ViewState["RecTblbuklvouchr"];
                DataRow dr = bulkvouchr.NewRow();
                int count = bulkvouchr.Rows.Count;

                dr["SRNO"] = count + 1;   // = Convert.ToInt32(ViewState["SRNO"]) + 1;

                if (ddlEmpType.SelectedValue == "1")
                {
                    dr["PARTYNAME"] = ddlEmployee.SelectedItem.Text;
                    dr["EMPID"] = ddlEmployee.SelectedValue;
                    dr["ISEMP"] = 1;

                }
                if (ddlEmpType.SelectedValue == "2")
                {
                    dr["PARTYNAME"] = ddlName.SelectedItem.Text;
                    dr["EMPID"] = ddlName.SelectedValue;
                    dr["ISEMP"] = 0;

                }

                dr["Remark"] = txtRemark.Text.Trim() == null ? string.Empty : Convert.ToString(txtRemark.Text.Trim()).Replace(',', ' ');
                dr["ChequeNo"] = txtChqNo2.Text.Trim() == null ? string.Empty : Convert.ToString(txtChqNo2.Text.Trim()).Replace(',', ' ');
                dr["ChequeDate"] = txtChequeDt2.Text.Trim().Equals(string.Empty) ? "9999-12-31" : Convert.ToString(txtChequeDt2.Text.Trim()).Replace(',', ' ');

                dr["AMOUNT1"] = 0; // Convert.ToDouble(bulkvouchr.Rows[0]["AMOUNT1"].ToString()); // txtamount.Text == "" ? 0 : Convert.ToDecimal(txtamount.Text);
                dr["AMOUNT2"] = 0; // Convert.ToDouble(bulkvouchr.Rows[0]["AMOUNT2"].ToString()); // txtamount1.Text == "" ? 0 : Convert.ToDecimal(txtamount1.Text);
                dr["AMOUNT3"] = 0; // Convert.ToDouble(bulkvouchr.Rows[0]["AMOUNT3"].ToString()); //txtamount2.Text == "" ? 0 : Convert.ToDecimal(txtamount2.Text);


                bulkvouchr.Rows.Add(dr);

                ViewState["RecTblbuklvouchr"] = bulkvouchr;
                lvFields.DataSource = bulkvouchr;
                lvFields.DataBind();

                Clearbulkvouchr();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                OsCenterTable.Visible = true;
                divTotalAmt.Visible = true;
            }

            else
            {
                DataTable bulkvouchr = this.CreateTabelbulkvouchr();
                DataRow dr = bulkvouchr.NewRow();

                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
              //  dr["PARTYNAME"] = ddlName.SelectedItem.Text;
              //  dr["EmpId"] = ddlName.SelectedValue;

                if (ddlEmpType.SelectedValue == "1")
                {
                    dr["PARTYNAME"] = ddlEmployee.SelectedItem.Text;
                    dr["EMPID"] = ddlEmployee.SelectedValue;
                    dr["ISEMP"] = 1;

                }
                if (ddlEmpType.SelectedValue == "2")
                {
                    dr["PARTYNAME"] = ddlName.SelectedItem.Text;
                    dr["EMPID"] = ddlName.SelectedValue;
                    dr["ISEMP"] = 0;

                }

                dr["Remark"] = txtRemark.Text.Trim() == null ? string.Empty : Convert.ToString(txtRemark.Text.Trim()).Replace(',', ' ');
                dr["ChequeNo"] = txtChqNo2.Text.Trim() == null ? string.Empty : Convert.ToString(txtChqNo2.Text.Trim()).Replace(',', ' ');
                dr["ChequeDate"] = txtChequeDt2.Text.Trim().Equals(string.Empty) ? "9999-12-31" : Convert.ToString(txtChequeDt2.Text.Trim()).Replace(',', ' ');
                dr["AMOUNT1"] = 0;
                dr["AMOUNT2"] = 0;
                dr["AMOUNT3"] = 0;

                ViewState["RecTblbuklvouchr"] = Convert.ToInt32(ViewState["RecTblbuklvouchr"]) + 1;
                bulkvouchr.Rows.Add(dr);
                Clearbulkvouchr();
                ViewState["RecTblbuklvouchr"] = bulkvouchr;
                lvFields.DataSource = bulkvouchr;
                lvFields.DataBind();
                OsCenterTable.Visible = true;
                divTotalAmt.Visible = true;
            }
            lblAmount.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Transaction.btnADD_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void Clearbulkvouchr()
    {
        ddlName.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        txtChqNo2.Text = string.Empty;
        txtChequeDt2.Text = string.Empty;
        ddlEmployee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["SRNO"] = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            DataTable bulkvouchr;
            AddExistingEmpData();
            if (ViewState["RecTblbuklvouchr"] != null && ((DataTable)ViewState["RecTblbuklvouchr"]) != null)
            {
                ViewState["SRNO"] = btnEdit.CommandArgument;

                bulkvouchr = ((DataTable)ViewState["RecTblbuklvouchr"]);

                DataRow dr = this.GetEditableDatarow(bulkvouchr, btnEdit.CommandArgument);


                bulkvouchr.Rows.Remove(dr);
                ViewState["RecTblbuklvouchr"] = bulkvouchr;
                lvFields.DataSource = bulkvouchr;
                lvFields.DataBind();
            }

            GetTotalAmount();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "Transaction.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void GetTotalAmount()
    {
        int count = 0;
        double Totalamount = 0, Totalamount1 = 0, Totalamount2 = 0;
        foreach (ListViewDataItem item in lvFields.Items)
        {
            TextBox txtamount = item.FindControl("txtamount") as TextBox;
            TextBox txtamount1 = item.FindControl("txtamount1") as TextBox;
            TextBox txtamount2 = item.FindControl("txtamount2") as TextBox;

            double amount = txtamount.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtamount.Text.Trim());
            double amount1 = txtamount1.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtamount1.Text.Trim());
            double amount2 = txtamount2.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtamount2.Text.Trim());

            //double totalAmount = amount + amount1 + amount2;
            //txtCalculateAmt.Text = totalAmount.ToString();

            Totalamount = Totalamount + amount;
            Totalamount1 = Totalamount1 + amount1;
            Totalamount2 = Totalamount2 + amount2;

            count = 1;
        }
        lbltotalamount.Text = Totalamount.ToString();
        lbltotalamount1.Text = Totalamount1.ToString();
        lbltotalamount2.Text = Totalamount2.ToString();

        if (count == 0)
        {
            divTotalAmt.Visible = false;
            OsCenterTable.Visible = false;
            txtCalculateAmt.Text = "0";
        }
        else
        {
            divTotalAmt.Visible = true;
            OsCenterTable.Visible = true;
            GetFinalTotal();
        }

    }

    private void GetFinalTotal()
    {
        double val = hdnlblLedgerAmount.Value.Equals(string.Empty) ? 0 : Convert.ToDouble(hdnlblLedgerAmount.Value);
        double val1 = hdnlblLedgerAmount1.Value.Equals(string.Empty) ? 0 : Convert.ToDouble(hdnlblLedgerAmount1.Value);
        double val2 = hdnlblLedgerAmount2.Value.Equals(string.Empty) ? 0 : Convert.ToDouble(hdnlblLedgerAmount2.Value);

        double txtFirstNumberValue = lbltotalamount.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(lbltotalamount.Text.Trim());

        double totalAmt = 0;

        if (txtFirstNumberValue > 0)
        {
            totalAmt = txtFirstNumberValue;
        }
        if (val1 > 0)
        {
            double txtSecondNumberValue = lbltotalamount1.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(lbltotalamount1.Text.Trim());

            if (txtSecondNumberValue > 0)
            {
                string Mode1 = hdnLedgerMode1.Value;

                if (Mode1 == "Cr")
                {
                    totalAmt = txtFirstNumberValue - txtSecondNumberValue;
                }
                else if (Mode1 == "Dr")
                {
                    totalAmt = txtFirstNumberValue + txtSecondNumberValue;
                }
            }
        }
        if (val2 > 0)
        {
            double txtThirdNumberValue = lbltotalamount2.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(lbltotalamount2.Text.Trim());

            if (txtThirdNumberValue > 0)
            {
                string Mode2 = hdnLedgerMode2.Value;

                if (Mode2 == "Cr")
                {
                    totalAmt = totalAmt - txtThirdNumberValue;
                }
                else if (Mode2 == "Dr")
                {
                    totalAmt = totalAmt + txtThirdNumberValue;
                }
            }
        }
        txtCalculateAmt.Text = totalAmt.ToString();
    }

    private DataRow GetEditableDatarow(DataTable dt, string value)
    {

        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Transaction.GetEditableDatarow -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    private bool GetChekableDatarow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PARTYNAME"].ToString().ToLower() == value.ToLower())
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
                objCommon.ShowError(Page, "Transaction.GetEditableDatarow -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    private void addbulkvouchar()
    {

    }

    //end

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //led[1] = led[1].ToString().Replace("¯", "");


        if (txtAgainstAcc.Text == string.Empty && ddlTranType.SelectedValue != "J")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Please Select Against Account.", this);
            return;
        }
        else
        {
            //if (Convert.ToDouble(lblCurbal1.Text) < 0)
            //    objCommon.DisplayUserMessage(UPDLedger, "Alert..Negitive Balance..!", this.Page);

            if (txtAcc.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Select Account.", this);
                return;
            }
            txtAcc.Text = txtAcc.Text.Trim().Replace("¯", "");
            txtAgainstAcc.Text = txtAgainstAcc.Text.Trim().Replace("¯", "");
            //if (ViewState["isEdit"].ToString().Trim() == "N")
            //{
            if (ddlTranType.SelectedValue != "J")
                hdnPartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAgainstAcc.Text.Trim().Split('*')[1] + "'");

            //}
            //else
            //{
            //    hdnPartyManual.Value = txtAgainstAcc.Text.Trim().Split('*')[0];
            //}

            hdnOpartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'");
            ////if (Convert.ToDouble(txtTranAmt.Text) == 0.00 || txtTranAmt.Text == String.Empty)
            //{
            //    objCommon.DisplayUserMessage(UPDLedger, "Please Enter Transaction Amount", this.Page);
            //    return;
            //}
            if (isSingleMode == "Y")
            {
                if (ValidateLedger(txtAgainstAcc.Text.ToString().Trim(), "Y") == false)
                {
                    txtAgainstAcc.Focus();
                    return;
                }
                if (ddlTranType.SelectedValue.ToString().Trim() == "C")
                {
                    if (ValidateLedger(txtAcc.Text.ToString().Trim(), "Y") == false)
                    {
                        txtAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ValidateLedger(txtAcc.Text.ToString().Trim(), "N") == false)
                    {
                        txtAcc.Focus();
                        return;
                    }
                }
            }
            else
            {
                if (ViewState["isEdit"].ToString().Trim() == "Y")
                {
                    if (ValidateLedger(txtAcc.Text.ToString().Trim(), "YN") == false)
                    {
                        txtAcc.Focus();
                        return;
                    }
                }

                if (ddlTranType.SelectedValue.ToString().Trim() == "P" && ddlcrdr.SelectedItem.ToString().Trim() == "Cr")
                {
                    if (ValidateLedger(txtAcc.Text.ToString().Trim(), "YN") == false)
                    {
                        txtAcc.Focus();
                        return;
                    }
                }
                if (ddlTranType.SelectedValue.ToString().Trim() == "R" && ddlcrdr.SelectedItem.ToString().Trim() == "Dr")
                {
                    if (ValidateLedger(txtAcc.Text.ToString().Trim(), "YN") == false)
                    {
                        txtAcc.Focus();
                        return;
                    }
                }
                if (ddlTranType.SelectedValue.ToString().Trim() == "C" && ddlcrdr.SelectedItem.ToString().Trim() == "Dr")
                {
                    if (ValidateLedger(txtAcc.Text.ToString().Trim(), "YN") == false)
                    {
                        txtAcc.Focus();
                        return;
                    }
                }
            }

            //if (ddlTranType.SelectedValue == "P")
            //{
            //    if (chkTDSApplicable.Checked == true)
            //    {
            //        if (txtAcc.Text == txtTDSLedger.Text)
            //        {
            //            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for TDS which was already selected ", this);
            //            txtTDSLedger.Text = string.Empty;
            //            txtTDSLedger.Focus();
            //            return;
            //        }
            //        if (Convert.ToDouble(txtTDSPer.Text)>100 )
            //        {
            //            objCommon.DisplayUserMessage(UPDLedger, "TDS Percentage Can't be greater than or equal to 100%", this);
            //            txtTDSPer.Text = string.Empty;
            //            txtTDSLedger.Text = string.Empty;
            //            txtTDSPer.Focus();
            //            return;
            //        }
            //    }
            //}
            DateTime date2;
            if (!(DateTime.TryParse(txtDate.Text, out date2)))
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Date Is Entered. ", this);
                txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDate.Focus();
                return;
            }
            else
            {
                if (isPerNarration == "Y")
                {
                    if (txtChequeDt1.Text.ToString().Trim() != "")
                    {
                        DateTime date1;
                        if (!(DateTime.TryParse(txtChequeDt1.Text, out date1)))
                        {
                            objCommon.DisplayUserMessage(UPDLedger, "Invalid Date Is Entered. ", this);
                            txtChequeDt1.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    if (txtChequeDt2.Text.ToString().Trim() != "")
                    {
                        DateTime date1;
                        if (!(DateTime.TryParse(txtChequeDt2.Text, out date1)))
                        {
                            objCommon.DisplayUserMessage(UPDLedger, "Invalid Date Is Entered. ", this);
                            txtChequeDt2.Focus();
                            return;
                        }
                    }
                }

                if (chkTDSApplicable.Checked == true)
                {
                    if (txtTDSLedger.Text == string.Empty || txtTDSLedger.Text == "")
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Ledger", this);
                        txtTDSLedger.Focus();
                        return;
                    }
                    //  if (txtTDSPer.Text == string.Empty || txtTDSPer.Text == "")
                    {
                        //objCommon.DisplayUserMessage(UPDLedger, "Please Enter TDS Percentage 2", this);
                        //  txtTDSPer.Focus();
                        //  return;
                    }

                    //if (txtTDSAmount.Text == string.Empty || txtTDSAmount.Text == "")
                    //{
                    //    objCommon.DisplayUserMessage(UPDLedger, "TDS amount Cant't be Empty!", this);
                    //    txtTDSAmount.Focus();
                    //    return;
                    //}
                }

                if (isSingleMode == "Y")
                {
                    if (txtAgainstAcc.Text.ToString().Trim() == "")
                    {
                        //rowMsg.Style["Display"] = "block";
                        //lblMsg.Text = "Enter Against Account Name.";
                        objCommon.DisplayUserMessage(UPDLedger, "Enter Against Account Name.", this);
                        return;
                    }
                    //else { rowMsg.Style["Display"] = "none"; }

                    if (isAllreadySet != "Y")
                    {
                        Session["AgainstAcc"] = "Y";
                        SetDetails(txtAgainstAcc.Text.ToString().Trim());
                    }

                    Session["AgainstAcc"] = "N";
                    if (SetDetails(txtAcc.Text.ToString().Trim()) == "")
                    {
                        AddGridEntry();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Entered.", this);
                        txtAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (SetDetails(txtAcc.Text.ToString().Trim()) == "")
                    {
                        AddGridEntry();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Entered.", this);
                        txtAcc.Focus();
                        return;
                    }
                    ddlcrdr.Enabled = true;
                }

                AddDeleteId();
                if (dt != null)
                {
                    dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    GridData.DataSource = null;
                    GridData.DataBind();


                    GridData.DataSource = dt;
                    GridData.DataBind();

                    string a = GridData.Rows.Count.ToString();
                    if (dt.Rows.Count != 0)
                    {
                        if (isSingleMode == "Y")
                        {
                            txtAgainstAcc.Enabled = false;
                        }
                    }
                    else
                    {
                        txtAgainstAcc.Enabled = true;
                    }
                    if (ddlTranType.SelectedValue == "J")
                    {
                        AddTotalAmountJurnal("0");
                    }
                    else
                    {
                        AddTotalAmount();
                    }
                    //RowIndex = -1;
                    ViewState["RowIndex"] = -1;

                    if (isPerNarration == "Y")
                    {
                        GridData.Columns[3].Visible = false;
                    }
                    else
                    {
                        GridData.Columns[3].Visible = false;
                    }

                    if (isSingleMode == "Y")
                    {
                        GridData.Columns[4].Visible = false;
                        GridData.Columns[5].Visible = false;
                        GridData.Columns[6].Visible = false;
                        lblTotalDiff.Visible = false;
                        lblTotalDebit0.Visible = false;
                    }
                    else
                    {
                        GridData.Columns[4].Visible = false;
                        GridData.Columns[5].Visible = true;
                        GridData.Columns[6].Visible = true;
                        lblTotalDiff.Visible = true;
                        lblTotalDebit0.Visible = true;
                    }
                }
                else
                {
                    GridData.DataSource = null;
                    GridData.DataBind();
                }
                // txtTranAmt.Text = "";
                txtAcc.Text = "";
                txtPerNarration.Text = "";
                //txtChequeDt1.Text = "";
                //txtChqNo1.Text = "";
                lblCurBal2.Text = "";
                //ddlProjSubHead.SelectedValue = "0";
                if (hdnCurBalAg.Value.ToString().Trim() != "")
                {
                    lblCurbal1.Text = hdnCurBalAg.Value;
                }
                //lblCur1.Text = "";
                if (isSingleMode == "Y")
                    txtAcc.Focus();
                else
                    ddlcrdr.Focus();
            }
            hdnCurBalAg.Value = lblCurbal1.Text;

            SetWithoutCashBank();
            lnkupload.Attributes.Add("onClick", "return ShowVoucherWindow('do'," + GridData.Rows.Count.ToString() + ");");
            lnkView.Attributes.Add("onClick", "return ShowVoucherWindow('no'," + GridData.Rows.Count.ToString() + ");");

            //btnAdd.Enabled = false;  6dec//btn
            trCostCenter.Visible = false;
            //trBudgetHead.Visible = false;
            trBudgetHead.Visible = false;
            divDepartment.Visible = false;
            ddlcrdr.Enabled = true;
            lblCrDr2.Text = string.Empty;
            // txtTDSPer.Text = string.Empty;
            txtTDSLedger.Text = string.Empty;
            // txtTDSAmount.Text = string.Empty;
            chkTDSApplicable.Checked = false;
            dvTDS.Visible = false;
            //chkGST.Checked = false;
            //chkIGST.Checked = false;
            //txtIGST.Text = txtIGSTAMOUNT.Text = txtIGSTAMT.Text = txtIGSTPER.Text = string.Empty;
            //txtCGST.Text = txtCGSTAMOUNT1.Text = txtCGSTAMT.Text = txtCGSTPER.Text = string.Empty;
            //txtSGST.Text = txtSGSTAMOUNT.Text = txtSGSTAMT.Text = txtSGTSPer.Text = string.Empty;
            //divgst.Visible = false;
            //divIgst.Visible = false;
            //divcgst.Visible = false;

            txtCurBal.Visible = false;
            txtCurBal.Text = string.Empty;
            txtCurBal.Enabled = false;
        }
    }

    private void AddTotalAmount()
    {
        if (GridData.Rows.Count > 0)
        {
            int i;
            double TotDr = 0;
            double TotCr = 0;
            double Tot = 0;
            for (i = 0; i < GridData.Rows.Count; i++)
            {
                if (isSingleMode == "Y")
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[3].Visible = false;
                    GridData.Columns[5].Visible = false;
                    GridData.Columns[6].Visible = false;
                    if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Payment")
                    {
                        if (Convert.ToString(GridData.Rows[i].Cells[7].Text).Trim() == "Cr")
                        {
                            // Tot = Tot - Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                        else
                        {
                            //  Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                    }
                    else if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Receipt" || ddlTranType.SelectedItem.Text.ToString().Trim() == "Receipt")
                    {
                        if (Convert.ToString(GridData.Rows[i].Cells[7].Text).Trim() == "Cr")
                        {
                            ///    Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                        else
                        {
                            //   Tot = Tot - Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                    }

                    if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Contra")
                    {
                        if (Convert.ToString(GridData.Rows[i].Cells[7].Text).Trim() == "Cr")
                        {
                            //   Tot = Tot - Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                        else
                        {
                            //  Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                    }


                    //  Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                    //   lblTotalDiff.Visible = false;
                    //   lblTotalDebit0.Visible = false;
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[3].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;
                    lblTotalDiff.Visible = true;
                    lblTotalDebit0.Visible = true;
                    if (GridData.Rows[i].Cells[5].Text.ToString().Trim() == "")
                    {
                        // TotDr = TotDr + 0;
                    }
                    else
                    {
                        // TotDr = TotDr + Convert.ToDouble(GridData.Rows[i].Cells[5].Text);
                    }

                    if (GridData.Rows[i].Cells[6].Text.ToString().Trim() == "")
                    {
                        //  TotCr = TotCr + 0;
                    }
                    else
                    {
                        // TotCr = TotCr + Convert.ToDouble(GridData.Rows[i].Cells[6].Text);
                    }

                    //  lblTotalDiff.Text = (TotDr - TotCr).ToString();
                }
            }
            //  lblTotal.Text = String.Format("{0:0.00}", Convert.ToDouble(Tot.ToString().Trim()));
            //  lblTotalDebit.Text = String.Format("{0:0.00}", Convert.ToDouble(TotDr.ToString().Trim()));
            //  lblTotalCredit.Text = String.Format("{0:0.00}", Convert.ToDouble(TotCr.ToString().Trim()));
        }
        else
        {
            lblTotal.Text = "0.00";
            lblTotalDebit.Text = "0.00 Dr";
            lblTotalCredit.Text = "0.00 Cr";
        }
    }

    private void AddTotalAmountJurnal(string Amount)
    {
        if (GridData.Rows.Count > 0)
        {
            int i;
            double TotDr = 0;
            double TotCr = 0;
            double Tot = 0;
            for (i = 0; i < GridData.Rows.Count; i++)
            {
                if (isSingleMode == "Y")
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = false;
                    GridData.Columns[6].Visible = false;
                    if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Payment")
                    {
                        if (Convert.ToString(GridData.Rows[i].Cells[7].Text).Trim() == "Cr")
                        {
                            // Tot = Tot - Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                        else
                        {
                            Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                    }
                    else if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Receipt" || ddlTranType.SelectedItem.Text.ToString().Trim() == "Receipt")
                    {
                        if (Convert.ToString(GridData.Rows[i].Cells[7].Text).Trim() == "Cr")
                        {
                            Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                        else
                        {
                            Tot = Tot - Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                    }

                    if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Contra")
                    {
                        if (Convert.ToString(GridData.Rows[i].Cells[7].Text).Trim() == "Cr")
                        {
                            Tot = Tot - Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                        else
                        {
                            Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                        }
                    }


                    //Tot = Tot + Convert.ToDouble(GridData.Rows[i].Cells[4].Text);
                    lblTotalDiff.Visible = false;
                    lblTotalDebit0.Visible = false;
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;
                    lblTotalDiff.Visible = true;
                    lblTotalDebit0.Visible = true;
                    if (GridData.Rows[i].Cells[5].Text.ToString().Trim() == "")
                    {
                        TotDr = TotDr + 0;
                    }
                    else
                    {
                        TotDr = TotDr + Convert.ToDouble(GridData.Rows[i].Cells[5].Text);
                    }

                    if (GridData.Rows[i].Cells[6].Text.ToString().Trim() == "")
                    {
                        TotCr = TotCr + 0;
                    }
                    else
                    {
                        TotCr = TotCr + Convert.ToDouble(GridData.Rows[i].Cells[6].Text);
                    }

                    // lblTotalDiff.Text = (TotDr - Convert.ToDouble(Amount)).ToString();
                }
            }
            TotCr = TotCr - Convert.ToDouble(Amount);

            lblTotal.Text = String.Format("{0:0.00}", Convert.ToDouble(Tot.ToString().Trim()));
            lblTotalDebit.Text = String.Format("{0:0.00}", Convert.ToDouble(TotDr.ToString().Trim()));
            lblTotalCredit.Text = String.Format("{0:0.00}", Convert.ToDouble(TotCr.ToString().Trim()));
            lblTotalDiff.Text = String.Format("{0:0.00}", Convert.ToDouble(TotDr - TotCr));
        }
        else
        {
            lblTotal.Text = "0.00";
            lblTotalDebit.Text = "0.00 Dr";
            lblTotalCredit.Text = "0.00 Cr";
        }
    }

    protected void GridData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["Balance2"] = string.Empty;
        ViewState["Balance1"] = string.Empty;
        ViewState["Balance3"] = string.Empty;
        dt = Session["Datatable"] as DataTable;
        if (hdnCurBalAg.Value.ToString().Trim() != "")
        {
            lblCurbal1.Text = hdnCurBalAg.Value;
        }
        //HiddenField hdnPartyNo = GridData.Rows[e.NewSelectedIndex].FindControl("hdnDld") as HiddenField;
         txtAcc.Text = GridData.Rows[e.NewSelectedIndex].Cells[1].Text;
        //txtTranAmt.Text

        //if (hdnPartyNo.Value == "5")
        //{

        //}

        string Party = GridData.Rows[e.NewSelectedIndex].Cells[1].Text;
        int PartyId = Convert.ToInt32(Party.Split('*')[1].ToString());
        DataRow[] OpLedger = dt.Select("Dld_id='5' and Mode='Cr' and  Id='" + PartyId.ToString() + "'");
        if (OpLedger.Length > 0)
        {
            clearTax();
            clearGst();
            if (OpLedger[0]["Dld_id"].ToString() == "5")
            {

                txtAcc.Text = OpLedger[0]["Particulars"].ToString();
                //txtTranAmt.Text = OpLedger[0]["Amount"].ToString() == "0.00" ? OpLedger[0]["Credit"].ToString() : OpLedger[0]["Amount"].ToString();
                ddlcrdr.SelectedValue = OpLedger[0]["Mode"].ToString();

                for (int i = 0; i < OpLedger.Length; i++)
                    dt.Rows.Remove(OpLedger[i]);
                dt.AcceptChanges();

                // AddTotalAmountJurnal(txtTranAmt.Text);

                return;
            }
        }
        //DataTable dtjournal = SetDataColumn();

        //var result = from dt1 in dt.AsEnumerable()

        //             where dt1.Field<string>("Dld_id") == "5" && dt1.Field<string>("Id") == PartyId.ToString()
        //             select dtjournal.LoadDataRow(new object[]
        //                   {
        //                       dt1.Field<string>("Particulars"),
        //                     dt1.Field<string>("Amount"),
        //                         dt1.Field<string>("Mode"),
        //                         dt1.Field<string>("Credit")
        //                   }, false);

        //if (result.Count() > 0)
        //{
        //    dtjournal = result.CopyToDataTable();
        //    DataRow[] OpLedger = dtjournal.Select();
        //    txtAcc.Text = dtjournal.Rows[0]["Particulars"].ToString();
        //    txtTranAmt.Text = dtjournal.Rows[0]["Amount"].ToString() == "0.00" ? dtjournal.Rows[0]["Credit"].ToString() : dtjournal.Rows[0]["Amount"].ToString();
        //    ddlcrdr.SelectedValue = dtjournal.Rows[0]["Mode"].ToString();

        //    for (int i = 0; i < OpLedger.Length; i++)
        //        dt.Rows.Remove(OpLedger[i]);
        //    dt.AcceptChanges();

        //    return;
        //}




        //RowIndex = e.NewSelectedIndex;
        ViewState["RowIndex"] = e.NewSelectedIndex;
        DataRow[] LedgerRow = dt.Select("SGSTPER=0.00 AND CGSTPER=0.00 AND IGSTPER=0.00 AND TDSpercentage=0.00 AND Dld_id=1"); //AND Dld_id=1
        if (LedgerRow.Length > 0)
        {

            txtAcc.Text = LedgerRow[0]["Particulars"].ToString();

            // HiddenField hdnPartyNo = GridData.Rows[e.NewSelectedIndex].FindControl("hdnPartyNo") as HiddenField;

            ViewState["PartyId"] = LedgerRow[0]["Id"].ToString();

            if (ddlTranType.SelectedValue != "J")
            {
                if (e.NewSelectedIndex.ToString() == "0")
                {
                    ddlcrdr.Enabled = false;
                }
                else
                {
                    ddlcrdr.Enabled = true;
                }
            }
            ddlcrdr.SelectedValue = LedgerRow[0]["Mode"].ToString(); //GridData.Rows[e.NewSelectedIndex].Cells[7].Text;
            if (isSingleMode == "Y")
            {
                // txtTranAmt.Text = Convert.ToDouble(LedgerRow[0]["Amount"].ToString()).ToString();// GridData.Rows[e.NewSelectedIndex].Cells[4].Text;
            }
            else
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    //  txtTranAmt.Text = LedgerRow[0]["Credit"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[6].Text;

                }
                else
                {
                    //  txtTranAmt.Text = LedgerRow[0]["Debit"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[5].Text;

                }

            }

            txtAcc.Text = LedgerRow[0]["Particulars"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[1].Text.Replace("&amp;", "&");

        }

        //txtAcc.Text.ToString()
        //&#184;
        if (ViewState["TDS"].ToString() == "Yes")
        {

            //   DataRow[] RowTds=dt.Select("")
            //   txtTDSLedger.Text = GridData.Rows[GridData.Rows.Count - 1].Cells[1].Text.Replace("&amp", "&");
            chkTDSApplicable.Checked = true;
            double TDSamount = 0, TdsTamount = 0;
            double Amount = 0; double TDSPer = 0;
            int section = 0;
            if (isSingleMode == "Y")
            {
                DataRow[] TDSROW = dt.Select("Particulars like '%TDS%'");
                if (TDSROW.Length > 0)
                {
                    txtTDSLedger.Text = TDSROW[0]["Particulars"].ToString();
                    TDSamount = TDSROW[0]["TDSAMOUNT"].ToString().Equals(string.Empty) ? 0 : Convert.ToDouble(TDSROW[0]["TDSAMOUNT"].ToString());
                    TdsTamount = TDSROW[0]["Amount"].ToString().Equals(string.Empty)?0:Convert.ToDouble(TDSROW[0]["Amount"].ToString());
                    TDSPer = TDSROW[0]["TDSpercentage"].ToString().Equals(string.Empty) ? 0 : Convert.ToDouble(TDSROW[0]["TDSpercentage"].ToString());
                    section = TDSROW[0]["TDSsection"].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(TDSROW[0]["TDSsection"].ToString());

                }
            }
            else
            {

                DataRow[] TDSROW = dt.Select("Particulars like '%TDS%'");
                if (TDSROW.Length > 0)
                {
                    txtTDSLedger.Text = TDSROW[0]["Particulars"].ToString();
                    TDSamount = Convert.ToDouble(TDSROW[0]["TDSamount"].ToString());
                    TdsTamount = Convert.ToDouble(TDSROW[0]["Credit"].ToString());
                    TDSPer = Convert.ToDouble(TDSROW[0]["TDSpercentage"].ToString());
                    section = Convert.ToInt32(TDSROW[0]["TDSsection"].ToString());

                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }


            // txtTDSPer.Text = TDSPer.ToString();
            //txtTDSvvvText = TdsTamount.ToString();
            // txtTamount.Text = "0.00";// TDSamount.ToString();
            ddlSection.SelectedValue = section.ToString();
            dvTDS.Visible = true;
        }



        ///Calculate balance
        //string partyNo = "";
        //if (txtAcc.Text.Trim()!= "")
        //{
        //     partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'");
        //}
        //else 
        //{
        //     partyNo = "0";
        //}
        string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'");
        PartyController objPC = new PartyController();
        DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

        if (dtr.Read())
        {
            lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
            ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
        }

        string balance = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Id"].ToString() == partyNo)
            {
                if (i != e.NewSelectedIndex)
                {
                    if (dt.Rows[i]["Mode"].ToString() == "Dr")
                    {
                        balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                        ViewState["Balance2"] = balance;
                    }
                    else
                    {
                        if (dt.Rows[i]["Mode"].ToString() == "Dr")
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance2"] = balance;
                        }
                        else
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance2"] = balance;
                        }
                    }
                }
            }
        }

        ///Calculate balance

        ViewState["OldLedgerId"] = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + GridData.Rows[e.NewSelectedIndex].Cells[1].Text.ToString().Trim().Split('*')[1].ToString().Trim() + "'");
        if (ddlcrdr.SelectedValue.ToString().Trim() == "Dr" && ddlTranType.SelectedValue.ToString().Trim() == "P")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                //lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }

        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr" && ddlTranType.SelectedValue.ToString().Trim() == "P")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                // lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text)).ToString().Trim();
            }
        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Dr" && ddlTranType.SelectedValue.ToString().Trim() == "R")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                //lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }

        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr" && ddlTranType.SelectedValue.ToString().Trim() == "R")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                // lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }

        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Dr" && ddlTranType.SelectedValue.ToString().Trim() == "C")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                //lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }

        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr" && ddlTranType.SelectedValue.ToString().Trim() == "C")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                //lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }
        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Dr" && ddlTranType.SelectedValue.ToString().Trim() == "J")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                //lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }
        }
        else if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr" && ddlTranType.SelectedValue.ToString().Trim() == "J")
        {
            lblCurBal2.Text = ViewState["Balance2"].ToString();
            if (isSingleMode == "Y")
            {
                //lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }
        }

        hdnbal2.Value = lblCurBal2.Text;// GridData.Rows[e.NewSelectedIndex].Cells[2].Text;
        ViewState["LedgerBal"] = lblCurBal2.Text;
        hdnCurBalAg.Value = lblCurbal1.Text;

        //HiddenField hdncn = GridData.Rows[e.NewSelectedIndex].FindControl("hdnChqNo") as HiddenField;
        //HiddenField hdncd = GridData.Rows[e.NewSelectedIndex].FindControl("hdnChqDate") as HiddenField;
        //HiddenField hdnCostCenter = GridData.Rows[e.NewSelectedIndex].FindControl("hdnCostCenterID") as HiddenField;
        //HiddenField hdnBudgetHeadId = GridData.Rows[e.NewSelectedIndex].FindControl("hdnBudgetHeadId") as HiddenField;

        //HiddenField hdnDepartmentId = GridData.Rows[e.NewSelectedIndex].FindControl("hdnDepartmentId") as HiddenField;


       // HiddenField hdncn = LedgerRow[0]["ChqNo"] as HiddenField;
       // HiddenField hdncd = LedgerRow[0]["ChqDate"] as HiddenField;
       // HiddenField hdnCostCenter = LedgerRow[0]["CCID"] as HiddenField;
       // HiddenField hdnBudgetHeadId = LedgerRow[0]["BudgetNo"] as HiddenField;

        //HiddenField hdnDepartmentId = LedgerRow[0]["DepartmentId"] as HiddenField;



        //Change by Nakul Chawre @12042016 to update Cost Center
        string ISAPPLICABLE = objCommon.LookUp("Acc_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISCCApplicable as int),0) ISCCApplicable", "Party_No=" + ViewState["OldLedgerId"].ToString());
        if (Convert.ToInt32(dt.Rows[0]["CCID"]) > 0 || Convert.ToInt32(ISAPPLICABLE) == 1)
        {
            trCostCenter.Visible = true;
            objCommon.FillDropDownList(ddlCostCenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
            //ddlCostCenter.SelectedValue = hdnCostCenter.Value == "" ? "0" : hdnCostCenter.Value;
            ddlCostCenter.SelectedValue = LedgerRow[0]["CCID"].ToString() == "" ? "0" : LedgerRow[0]["CCID"].ToString();
        }

        //Added for Budget Integration Modification
        string BudgetNo = objCommon.LookUp("Acc_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISBudgetHead as int),0) ISBudgetHead", "Party_No=" + ViewState["OldLedgerId"].ToString());
        objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
        if (Convert.ToInt32(dt.Rows[0]["BudgetNo"]) > 0 || Convert.ToInt32(BudgetNo) == 1)
        {

            if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "R")
            {
                trBudgetHead.Visible = true;
                divDepartment.Visible = true;
                objCostCenterController.BindBudgetHead(ddlBudgetHead);//Added by vijay 08-07-2020
                //ddlBudgetHead.SelectedValue = Convert.ToString(hdnBudgetHeadId.Value == "" ? "0" : hdnBudgetHeadId.Value);
                ddlBudgetHead.SelectedValue = Convert.ToString(LedgerRow[0]["BudgetNo"].ToString() == "" ? "0" : LedgerRow[0]["BudgetNo"].ToString());
            }




        }
      //  ddldepartment.SelectedValue = Convert.ToString(LedgerRow[0]["DepartmentId"].ToString() == "" ? "0" : LedgerRow[0]["DepartmentId"].ToString());
        if (isPerNarration == "Y")
        {
            txtChqNo1.Text = LedgerRow[0]["ChqNo"].ToString().Trim();

            dt = Session["Datatable"] as DataTable;
            if (dt.Rows.Count != 0)
            {
                txtPerNarration.Text = dt.Rows[e.NewSelectedIndex]["Narration"].ToString().Trim();
            }
            else
            {
                txtPerNarration.Text = GridData.Rows[e.NewSelectedIndex].Cells[3].Text;
            }
            if (LedgerRow[0]["ChqDate"].ToString().Trim() != "")
            { txtChequeDt1.Text = Convert.ToDateTime(LedgerRow[0]["ChqDate"].ToString()).ToString("dd/MM/yyyy"); }
            else txtChequeDt1.Text = "";
        }
        else
        {
           // txtChqNo2.Text = LedgerRow[0]["ChqNo"].ToString().Trim();
            //if (LedgerRow[0]["ChqDate"].ToString().Trim() != "")
            //{ txtChequeDt2.Text = Convert.ToDateTime(LedgerRow[0]["ChqDate"].ToString()).ToString("dd/MM/yyyy"); }
            //else txtChequeDt2.Text = "";
        }

        if (isSingleMode == "Y")
        {
            //txtTranAmt.Text = Convert.ToDouble(LedgerRow[0]["Amount"].ToString()).ToString(); ;//GridData.Rows[e.NewSelectedIndex].Cells[4].Text;

            //if (lblCurbal1.Text.ToString().Trim() != "")
            //{
            //    if (Convert.ToDouble(lblCurbal1.Text) >= Convert.ToDouble(txtTranAmt.Text))
            //    {
            //        ViewState["MOriginalBal"] = (Convert.ToDouble(lblCurbal1.Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            //    }
            //    else
            //    {
            //        ViewState["MOriginalBal"] = (Convert.ToDouble(lblCurbal1.Text) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            //    }
            //}
        }
        else
        {
            //if (GridData.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Trim() == "")
            //{
            //    if (GridData.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Trim() == "0.00")
            //    {
            //        txtTranAmt.Text = GridData.Rows[e.NewSelectedIndex].Cells[6].Text;
            //    }
            //    txtTranAmt.Text = GridData.Rows[e.NewSelectedIndex].Cells[6].Text;
            //}
            //else { txtTranAmt.Text = GridData.Rows[e.NewSelectedIndex].Cells[5].Text; }

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                //txtTranAmt.Text = LedgerRow[0]["Credit"].ToString();// GridData.Rows[e.NewSelectedIndex].Cells[6].Text;
            }
            else
            {
                // txtTranAmt.Text = LedgerRow[0]["Debit"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[5].Text; }

            }
            //if (Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[2].Text) >= Convert.ToDouble(txtTranAmt.Text))
            //if (Convert.ToDouble(LedgerRow[0]["Balance"].ToString()) >= Convert.ToDouble(txtTranAmt.Text))
            //{
            //    //ViewState["OriginalBal"] = (Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[2].Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            //    ViewState["OriginalBal"] = (Convert.ToDouble((LedgerRow[0]["Balance"].ToString())) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();

            //}
            //else
            //{
            //    ViewState["OriginalBal"] = (Convert.ToDouble((LedgerRow[0]["Balance"].ToString())) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            //}

            //if (GridData.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Trim() == "&nbsp;")
            if (LedgerRow[0]["Narration"].ToString().Trim() == "&nbsp;")
            {
                txtPerNarration.Text = "";
            }
            else
            {
                if (txtPerNarration.Text.ToString().Trim() == "")
                {
                    txtPerNarration.Text = ViewState["OrigData"].ToString();
                }

            }// GridData.Rows[e.NewSelectedIndex].Cells[3].Text; }

            txtAcc.Focus();
            SetWithoutCashBank();


            btnAdd.Enabled = true;  //btn
            //ddlcrdr.Enabled = true;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (GridData.Rows.Count == 0)
        {
            if (isSingleMode == "Y")
            {
                txtAgainstAcc.Enabled = true;
            }
            return;
        }

        //if (RowIndex != -1)
        if (ViewState["RowIndex"].ToString() != "-1")
        {
            if (Session["Datatable"] != null)
            {
                dt = Session["Datatable"] as DataTable;

                dt.Rows[Convert.ToInt32(ViewState["RowIndex"].ToString())].Delete();

                dt.AcceptChanges();
                ViewState["isRowDeleted"] = "Y";

                if (dt != null)
                {
                    GridData.DataSource = dt;
                    GridData.DataBind();
                }
                else
                {
                    GridData.DataSource = null;
                    GridData.DataBind();
                }
            }
            else
            {
                GridData.DataSource = null;
                GridData.DataBind();
            }
        }

        //TrialBalanceReportController ode2 = new TrialBalanceReportController();
        //ode2.DeleteTemporaryStorageBack(Convert.ToInt16(txtAcc.Text.ToString().Trim().Split('*')[0].ToString()), Session["ComputerIDAddress"].ToString().Trim());

        txtAcc.Text = "";
        txtPerNarration.Text = "";
        // txtTranAmt.Text = "";
        lblCurBal2.Text = "";
        txtAcc.Focus();
        ViewState["RowIndex"] = -1;
        if (GridData.Rows.Count == 0)
        {
            SetTransactionType();
            SetWithoutCashBank();

            if (isSingleMode == "N")
            {
                ddlcrdr.Enabled = true;
            }
            else
            {
                txtAgainstAcc.Enabled = true;
            }
        }
        hdnCurBalAg.Value = lblCurbal1.Text;
        AddTotalAmount();
        lnkupload.Attributes.Add("onClick", "return ShowVoucherWindow('do'," + GridData.Rows.Count.ToString() + ");");
        lnkView.Attributes.Add("onClick", "return ShowVoucherWindow('no'," + GridData.Rows.Count.ToString() + ");");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAcc.Text = "";
        txtPerNarration.Text = "";
        // txtTranAmt.Text = "";

        lblCurBal2.Text = "";
        //lblCurbal1.Text = "";
        txtAcc.Focus();
        AddTotalAmount();

        txtChqNo1.Text = "";
        txtChequeDt1.Text = "";
        clearGst();
        clearTax();

        //btnAdd.Enabled = true ;//oldkk
    }

    //protected void txtAgainstAcc_TextChanged(object sender, EventArgs e)
    //{
    //    rowMsg.Style["Display"] = "none";
    //    DataSet ds=new DataSet();
    //    if (isSingleMode == "Y")
    //    {
    //        Session["AgainstAcc"] = "Y";
    //        ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "PARTY_NO > 0 and PARTY_NAME like '%" + Convert.ToString(txtAgainstAcc.Text).Trim().ToUpper() + "%' and PAYMENT_TYPE_NO IN ('1','2') ", "PARTY_NO");// "PARTY_NAME");
    //    }
    //    try
    //    {
    //        lstLedgerName.DataSource = null;
    //        lstLedgerName.DataBind();
    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lblMsg.Text = "";
    //                rowMsg.Style["Display"] = "none";
    //                lstLedgerName.Items.Clear();
    //                lstLedgerName.DataTextField = "PARTY_NAME";
    //                lstLedgerName.DataValueField = "PARTY_NO";
    //                lstLedgerName.DataSource = ds.Tables[0];
    //                lstLedgerName.DataBind();
    //                upd_ModalPopupExtender.Show();
    //            }
    //            else
    //            {
    //                rowMsg.Style["Display"] = "block";
    //                lblMsg.Text = "Ledger Does Not Exist!";
    //                objCommon.DisplayUserMessage(UPDLedger, "Ledger Does Not Exist!", this);
    //                txtAgainstAcc.Focus();
    //            }
    //        }
    //        else
    //        {
    //            rowMsg.Style["Display"] = "block";
    //            lblMsg.Text = "Ledger Does Not Exist!";
    //            objCommon.DisplayUserMessage(UPDLedger, "Ledger Does Not Exist!", this);
    //            txtAgainstAcc.Focus();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Account_VoucherCreations.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    protected void lstLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

        //btnSave.Enabled = true;
        ClearAll();
        clearTax();
        clearGst();
        //ddlTranType.SelectedIndex = 0;
        ddlTranType_SelectedIndexChanged(sender, e);
        if (isSingleMode == "Y")
        {
            txtAgainstAcc.Focus();
        }
        else { txtAcc.Focus(); }

        lblAmount.Text = string.Empty;
        if (ViewState["Date"] == "") { txtDate.Text = DateTime.Today.Date.ToString(); }
        else
        {
            txtDate.Text = ViewState["Date"].ToString();
        }
        VoucherNoSetting();
        //SetVoucherNo();
        isEdit = "N";

        TrialBalanceReportController odel = new TrialBalanceReportController();
        odel.DeleteTemporaryStorageBack(0, Session["ComputerIDAddress"].ToString().Trim());
        // ddlTranType.Enabled = true;  -- Vidisha
        trCostCenter.Visible = false;
        //if (ViewState["isEdit"].ToString().Trim() == "Y")
        //{
        //    ViewState["isEdit"] = "N";
        //    Response.Redirect("AccountingVouchersModifications.aspx");
        //}
        if (Request.QueryString["obj"] != null && Request.QueryString["ledger"] == null)
        {
            if (isTempVoucher == "Y")
            {
                Response.Redirect("AccAllVocherModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"]);
            }
            else
            {
               // Response.Redirect("AccountingVouchersModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"]);
              //added by tanu 28_12_2021
                Response.Redirect("AccAllVocherModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"]);
            }
        }
        if (Request.QueryString["ledger"] != null && Request.QueryString["party_no"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim().Split(',')[0] == "LedgerReport")
            {
                Response.Redirect("LedgerReport.aspx?ledger=" + Request.QueryString["ledger"].ToString() + "&party_no=" + Request.QueryString["party_no"].ToString() + "&fromDate=" + Request.QueryString["fromDate"].ToString() + "&Todate=" + Request.QueryString["Todate"].ToString());
                btnReset_Click(sender, e);
            }
            else
            {
                Response.Redirect("Acc_ledgerReportGrid.aspx?ledger=" + Request.QueryString["ledger"].ToString() + "&party_no=" + Request.QueryString["party_no"].ToString() + "&fromDate=" + Request.QueryString["fromDate"].ToString() + "&Todate=" + Request.QueryString["Todate"].ToString());
                btnReset_Click(sender, e);
            }
        }
        if (Request.QueryString["ISBACK"] != null)
        {
            if (Request.QueryString["ISBACK"].ToString() == "TRUE")
            {
                Response.Redirect("TrialbalanceReport_forGrid.aspx?pageno=" + Request.QueryString["pageno"]);
                btnReset_Click(sender, e);
            }
            else
            {

                Response.Redirect("Acc_ledgerReportGrid.aspx?ISBACK=FALSE");
                btnReset_Click(sender, e);
            }
        }
        txtCurBal.Visible = false;
        txtCalculateAmt.Text = string.Empty;
    }

    private void ClearAll()
    {
        OsCenterTable.Visible = false; // Added By Akshay On 04-07-2022
        divTotalAmt.Visible = false;   // Added By Akshay On 04-07-2022
        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
        lvNewBills.DataSource = null;
        lvNewBills.DataBind();
        TextBox1.Text = string.Empty;
        ddlPaymentMode.SelectedValue = "0";
        ddlEmpType.SelectedValue = "0";
        ddlEmployee.SelectedValue = "0";
        Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        lblRemainAmt.Text = "";
        ddlSponsor.SelectedValue = "0";
        ddlProjSubHead.SelectedValue = "0";
        isAllreadySet = "";
        GridData.DataSource = null;
        GridData.DataBind();
        dt.Clear();
        Session["Datatable"] = dt;
        if (dt == null)
        {
            SetDataColumn();
        }
        lblTotalDiff.Text = "0.00";
        lblTotalCredit.Text = "0.00";
        lblTotalDebit.Text = "0.00";
        lblCur1.Text = string.Empty;
        //txtAgainstAcc.Text = "";
        //lblMsg.Text = "";
        //rowMsg.Style["Display"] = "none";
        if (isSingleMode == "Y")
        {
            txtAgainstAcc.Enabled = true;
            txtPerNarration.Text = "";
            // txtTranAmt.Text = "";
            lblCrDr1.Text = "";
            lblCrDr2.Text = "";
            txtAcc.Text = "";
            lblCurBal2.Text = "";
        }
        txtNarration.Text = "";
        ChangeThePageLayout();
        if (ViewState["Date"] == "")
            //txtDate.Text = DateTime.Today.Date.ToString();

            txtChqNo1.Text = "";
        txtChequeDt1.Text = "";
        txtChqNo2.Text = string.Empty;
        lblSpan.InnerText = string.Empty;
        lblCurbal1.Text = "0.00";
        lblTotal.Text = "0.00";
        ViewState["TDS"] = string.Empty;
        //  txtTDSAmount.Text = string.Empty;
        txtTDSLedger.Text = string.Empty;
        // txtTDSPer.Text = string.Empty;
        chkTDSApplicable.Checked = false;
        //chkGST.Checked = false;
        //chkIGST.Checked = false;
        dvTDS.Visible = false;
        ViewState["Balance3"] = "0";
        txtDate.Text = string.Empty;
        txtPartyName.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtNatureService.Text = string.Empty;
        lvFields.DataSource = null;
        lvFields.DataBind();
        lvFields.Visible = false;
        ViewState["SRNO"] = null;
        ViewState["RecTblbuklvouchr"] = null;
        lbltotalamount.Text = string.Empty;
        lbltotalamount1.Text = string.Empty;
        lbltotalamount2.Text = string.Empty;
        ddlPayeeNature.SelectedValue = "0";
        //ddlName.SelectedValue = "0";
        ddlName.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        txtChqNo2.Text = string.Empty;
        txtChequeDt2.Text = string.Empty;
        //txtCGSTAMT.Text = txtCGSTPER.Text = txtCGSTAMOUNT1.Text = txtCGST.Text = string.Empty;

        //txtSGST.Text = txtSGSTAMOUNT.Text = txtSGSTAMT.Text = txtSGTSPer.Text = string.Empty;

        //txtIGST.Text = txtIGSTPER.Text = txtIGSTAMOUNT.Text = txtIGSTAMT.Text = string.Empty;

        txtGSTNNO.Text = string.Empty;

        ViewState["IsIGST"] = "0";
        ViewState["IsGST"] = "0";

        divDepartment.Visible = false;
        trBudgetHead.Visible = false;
        // ddlBudgetHead.SelectedValue = ddlCostCenter.SelectedValue = ddldepartment.SelectedValue = "0";
        lblBudgetBal.Text = "0";
        txtAgainstAcc.Text = string.Empty;


        lnkupload.Attributes.Add("onClick", "return ShowVoucherWindow('do'," + GridData.Rows.Count.ToString() + ");");
        lnkView.Attributes.Add("onClick", "return ShowVoucherWindow('no'," + GridData.Rows.Count.ToString() + ");");
    }

    private void ModifyVoucherTransaction()
    {
        if (para[2].ToString().Trim() == "Payment")
        {
            ddlTranType.SelectedValue = "P";
        }
        else if (para[2].ToString().Trim() == "Receipt")
        {
            ddlTranType.SelectedValue = "R";
        }
        else if (para[2].ToString().Trim() == "Contra")
        {
            ddlTranType.SelectedValue = "C";
        }
        else
        {
            ddlTranType.SelectedValue = "J";
            isSingleMode = "N";
        }

        SetTransactionType();
        //Added by vijay on 08-07-2020
        objCommon.FillDropDownList(ddlSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
        string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");

        if (isTempVoucher == "Y")
        {
            ViewState["VOUCHERNUMBER"] = objCommon.LookUp("ACC_ALL_TEMP_TRANS", "top 1 VOUCHER_NO", "TRANSACTION_DATE BETWEEN '" + FinancialDate + "' and  VOUCHER_SQN=" + para[1].ToString().Trim());
        }
        else
        {
            ViewState["VOUCHERNUMBER"] = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "top 1 VOUCHER_NO", "TRANSACTION_DATE BETWEEN '" + FinancialDate + "' and  VOUCHER_SQN=" + para[1].ToString().Trim());
        }
        //Added by tanu on 21-12-2021 for multiple bill upload

        {
            DataSet dsDoc = null;
            if (ViewState["VOUCHERNUMBER"] != null)
            {
                dsDoc = objCommon.FillDropDown("ACC_TEMP_VOUCHER_UPLOAD_DOCUMENT", "*", "", "VOUCHER_NO =" + ViewState["VOUCHERNUMBER"] + " and  VOUCHER_SQN=" + para[1].ToString().Trim() + " and IsTempVoucher='" + isTempVoucher.ToString() + "'", "");

            }
            else
            {
                dsDoc = objCommon.FillDropDown("ACC_TEMP_VOUCHER_UPLOAD_DOCUMENT", "*", "", "VOUCHER_SQN=" + para[1].ToString().Trim() + " and IsTempVoucher='" + isTempVoucher.ToString() + "'", "");
            } 
            if (Convert.ToInt32(dsDoc.Tables[0].Rows.Count) > 0)
            {
                int rowCount = dsDoc.Tables[0].Rows.Count;
                CreateTable();
                DataTable dtM = (DataTable)ViewState["DOCS"];
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();
                    dr["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                    dr["Filepath"] = dsDoc.Tables[0].Rows[i]["Filepath"].ToString();
                    dr["DisplayFileName"] = dsDoc.Tables[0].Rows[i]["DisplayFileName"].ToString();
                    dr["DocumentName"] = dsDoc.Tables[0].Rows[i]["DocumentName"].ToString();
                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["DOCS"] = dtM;
                    ViewState["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                }
                lvNewBills.DataSource = (DataTable)ViewState["DOCS"];
                lvNewBills.DataBind();
                pnlNewBills.Visible = true;
                pnlNewBills.Enabled = true;
            }
            else
            {

                pnlNewBills.Visible = false;
                lvNewBills.DataSource = null;
                lvNewBills.DataBind();
                pnlNewBills.Enabled = false;
            }
        }

        //end

        txtVoucherNo.Text = ViewState["VOUCHERNUMBER"].ToString();
        if (ViewState["isCopy"] != null)
            ViewState["VOUCHERNUMBER"] = null;
        AccountTransactionController objRet = new AccountTransactionController();
        DataSet ds1 = null;
        DataSet dt = null;
        if (isTempVoucher == "Y")
        {
            ds1 = objRet.GetTransactionForModificationMakaut(Convert.ToInt16(para[1]), Session["comp_code"].ToString(), ddlTranType.SelectedValue);// + "_" + Session["fin_yr"].ToString().Trim());
            dt = objRet.GetTransEmpDataForModification(Session["comp_code"].ToString(), Convert.ToInt16(para[1]));
            if (dt != null)
            {
                //   Session["RecTblEmpData"] = dt;
                DataTable dr = dt.Tables[0];
                DataTable de = dt.Tables[1];
                ViewState["RecTblEmpData"] = dr;
                ViewState["RecTblTotAmount"] = de;
                lvFields.DataSource = dt.Tables[0];
                lvFields.DataBind();
                //ViewState["RecTblbuklvouchr"] = dt.Tables[0];

                for (int i = 0; i < lvFields.Items.Count; i++)
                {
                    TextBox txtamounts = lvFields.Items[i].FindControl("txtamount") as TextBox;
                    TextBox txtamounts1 = lvFields.Items[i].FindControl("txtamount1") as TextBox;
                    TextBox txtamounts2 = lvFields.Items[i].FindControl("txtamount2") as TextBox;

                    txtamounts.Text = dt.Tables[0].Rows[i]["AMOUNT1"].ToString();
                    txtamounts1.Text = dt.Tables[0].Rows[i]["AMOUNT2"].ToString();
                    txtamounts2.Text = dt.Tables[0].Rows[i]["AMOUNT3"].ToString();

                }
                GetTotAmountForModifyVoucher();
            }

        }
        else
        {
            ds1 = objRet.GetTransactionForModification(Convert.ToInt16(para[1]), Session["comp_code"].ToString(), ddlTranType.SelectedValue);// + "_" + Session["fin_yr"].ToString().Trim());

            dt = objRet.GetTransEmpDataForModification(Session["comp_code"].ToString(), Convert.ToInt16(para[1]));
            if (dt != null)
            {
                //   Session["RecTblEmpData"] = dt;
                DataTable dr = dt.Tables[0];
                DataTable de = dt.Tables[1];
                ViewState["RecTblEmpData"] = dr;
                ViewState["RecTblTotAmount"] = de;
                lvFields.DataSource = dt.Tables[0];
                lvFields.DataBind();
                //ViewState["RecTblbuklvouchr"] = dt.Tables[0];

                for (int i = 0; i < lvFields.Items.Count; i++)
                {
                    TextBox txtamounts = lvFields.Items[i].FindControl("txtamount") as TextBox;
                    TextBox txtamounts1 = lvFields.Items[i].FindControl("txtamount1") as TextBox;
                    TextBox txtamounts2 = lvFields.Items[i].FindControl("txtamount2") as TextBox;

                    txtamounts.Text = dt.Tables[0].Rows[i]["AMOUNT1"].ToString();
                    txtamounts1.Text = dt.Tables[0].Rows[i]["AMOUNT2"].ToString();
                    txtamounts2.Text = dt.Tables[0].Rows[i]["AMOUNT3"].ToString();

                }
                GetTotAmountForModifyVoucher();
            }

        }


        txtDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["transaction_date"]).ToShortDateString();
        TransactionDateToUpdate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["transaction_date"]).ToShortDateString();
        //string ISAPPLICABLE = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CostCenter", "", "");
        txtDate.Enabled = true;
        if (isSingleMode == "Y")
        {
            SetSingleModeLayout(ds1);
        }
        else
        {
            SetDoubleModeLayout(ds1);
        }
    }


    private void GetTotAmountForModifyVoucher()
    {
        try
        {
            DataTable dr = ((DataTable)ViewState["RecTblEmpData"]);
            DataTable dt = ((DataTable)ViewState["RecTblTotAmount"]);
            int totalAmt = 0;
            if (dt.Rows.Count > 0)
            {
                OsCenterTable.Visible = true;
                divTotalAmt.Visible = true;

                double amount = 0;
                double amount1 = 0;
                double amount2 = 0;

                amount = Convert.ToDouble(dt.Rows[0]["AMOUNT1"].ToString());
                amount1 = Convert.ToDouble(dt.Rows[0]["AMOUNT2"].ToString());
                amount2 = Convert.ToDouble(dt.Rows[0]["AMOUNT3"].ToString());

                lbltotalamount.Text = amount.ToString();
                lbltotalamount1.Text = amount1.ToString();
                lbltotalamount2.Text = amount2.ToString();


                int txtFirstNumberValue = Convert.ToInt32(lbltotalamount.Text);
                int txtSecondNumberValue = Convert.ToInt32(lbltotalamount1.Text);
                int txtThirdNumberValue = Convert.ToInt32(lbltotalamount2.Text);

                int count = 0;
                int datacount = dt.Rows.Count;

                if (Convert.ToInt32(dr.Rows[0]["PARTY_NO3"].ToString()) > 1)
                {
                    count = 2;

                }
                else if (Convert.ToInt32(dr.Rows[0]["PARTY_NO2"].ToString()) > 1)
                {
                    count = 1;

                }
                else if (Convert.ToInt32(dr.Rows[0]["PARTY_NO1"].ToString()) > 1)
                {
                    count = 0;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (count >= 0)
                    {
                        string ledgerMode = dr.Rows[i]["TRAN1"].ToString();

                        if (txtFirstNumberValue > 0)
                        {
                            totalAmt = (txtFirstNumberValue);
                        }
                    }
                    if (count >= 1)
                    {
                        if (txtSecondNumberValue > 0)
                        {
                            string ledgerMode = dr.Rows[i]["TRAN2"].ToString();
                            if (ledgerMode == "Cr")
                            {
                                totalAmt = (txtFirstNumberValue) - (txtSecondNumberValue);
                            }
                            else if (ledgerMode == "Dr")
                            {
                                totalAmt = (txtFirstNumberValue) + (txtSecondNumberValue);
                            }
                        }
                    }
                    if (count == 2)
                    {
                        if (txtThirdNumberValue > 0)
                        {

                            string ledgerMode = dr.Rows[i]["TRAN3"].ToString();
                            if (ledgerMode == "Cr")
                            {
                                totalAmt = totalAmt - (txtThirdNumberValue);
                            }
                            else if (ledgerMode == "Dr")
                            {
                                totalAmt = totalAmt + (txtThirdNumberValue);
                            }
                        }
                    }

                }
                txtCalculateAmt.Text = totalAmt.ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IQAC_Transaction_PBAS_Proforma.btnPersonalSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void SetSingleModeLayout(DataSet dsSng)
    {
        SetGridEntryAlteration(dsSng);
    }

    private void SetDoubleModeLayout(DataSet dsDbl)
    {
        SetGridEntryAlteration(dsDbl);
        if (GridData.Rows.Count > 0)
            ddlcrdr.Enabled = true;
        else
            ddlcrdr.Enabled = true;
        SetWithoutCashBank();

    }

    protected void lnkConfig_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConfigurationSetting.aspx?obj=AccountingVouchers&pageno=" + Request.QueryString["pageno"]);
    }

    private void SetOppositPartyString()
    {
        string[] sArr = txtAgainstAcc.Text.Trim().Split('*');
        string opartystring = hdnPartyManual.Value.ToString();
        int i = 0;
        for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
        {

            DataTable dtbind = Session["Datatable"] as DataTable;

            if (dtbind.Rows.Count != 0)
            {
                dtbind.Rows[i]["OppParty"] = opartystring;
            }

            dtbind.Rows[i].AcceptChanges();
            Session["Datatable"] = dtbind;
            GridData.DataSource = dtbind;
            GridData.DataBind();
        }//end of for loop 1

    }

    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    private void ShowVoucherCashBankReport(string reportTitle, string rptFileName, String TransactionType, string VchNo, string isBankCash)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim() + ",BankORCashName=" + isBankCash;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    protected void  btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlPaymentMode.SelectedIndex == 0)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Select Pay Mode", this);
                ddlPaymentMode.Focus();
                return;
            }

            //btnSave.Enabled = false;
            if (txtVoucherNo.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please enter voucher no", this.Page);
                return;
            }
            if (txtAgainstAcc.Text == string.Empty && ddlTranType.SelectedValue != "J")
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please select against account", this.Page);
                return;
            }

            //if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISBudgetHead as int),0) ISBudgetHead", "Party_No=" + hdnOpartyManual.Value)) == 1)
            //{
            //    if (Convert.ToInt32(ddlBudgetHead.SelectedValue) > 0)
            //    {
            //        if (Convert.ToDouble(lblBudgetBal.Text) <= 0)
            //        {
            //            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "Confirm();", true);
            //if (lblconfirm.Text == "NO")
            //{
            //    return;
            //}
            // Configure the message box
            //string messageBoxText = "Selected Budget Head Has " + lblBudgetBal.Text + " Left, Do you want to save data?";
            //string caption = "Confirmation";
            //MessageBoxButtons button = MessageBoxButtons.YesNo;
            //MessageBoxIcon icon = MessageBoxIcon.Warning;

            //DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            //if (result == DialogResult.No)
            //{
            //    return;
            //}

            // ModalPopupExtender1.Show();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "Confirm();", true);
            //if (lblconfirm.Text == "NO")
            //{
            //    return;
            //}
            //        }
            //    }
            //}
            txtVoucherNo.Text = txtVoucherNo.Text.ToUpper();

            int PaymentTypeNo = 0;

            //if (ViewState["isEdit"].ToString().Trim() == "N")
            //{
            if (ddlTranType.SelectedValue != "J")
                hdnPartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAgainstAcc.Text.Trim().Split('*')[1] + "'");
            //}
            //else
            //{
            //    hdnPartyManual.Value = txtAgainstAcc.Text.Trim().Split('*')[0];
            //}

            string party_no = hdnPartyManual.Value.ToString();
            if (ddlTranType.SelectedValue != "J")
            {
                PaymentTypeNo = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "PAYMENT_TYPE_NO", "PARTY_NO=" + Convert.ToInt16(party_no)));
            }

            if ((ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "C") && PaymentTypeNo == 2)
            {
                ViewState["PaymentType"] = "P";
                ViewState["PaymentTypeNo"] = 2;
                //btnBack.Attributes.Add("onClick", "return AskCheque();");
            }
            else
            {
                ViewState["PaymentType"] = "N";
                ViewState["PaymentTypeNo"] = 1;
                //btnBack.Attributes.Remove("onClick");
            }



            string save_date = txtDate.Text;
            AccountConfigurationController objvch = new AccountConfigurationController();
            DataSet dsVoucher = new DataSet();
            int voucherno = 0;
            //DataTableReader dtr1 = objvch.GetMaxVoucherNo(Session["comp_code"].ToString().Trim());// + "_" + Session["fin_yr"].ToString().Trim());


            if (isPerNarration != "Y")
            {
                if (txtChequeDt2.Text.ToString().Trim() != "")
                {
                    DateTime date1;
                    if (!(DateTime.TryParse(txtChequeDt2.Text, out date1)))
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Date Is Entered. ", this);
                        txtChequeDt2.Focus();
                        return;
                    }
                }
            }

            if (DateTime.Compare(Convert.ToDateTime(txtDate.Text), DateTime.Now.Date) == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Can Not Make Future Entry. ", this);
                txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDate.Focus();
                return;
            }

            FinanceCashBookController objCBC = new FinanceCashBookController();
            DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
            if (dtr.Read())
            {
                Session["comp_code"] = dtr["COMPANY_CODE"];
                Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
                Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
                Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            }
            dtr.Close();
            if (ddlTranType.SelectedValue.ToString() != "J")
            {
                string hCalculateAmt = hdnCalculateAmt.Value;
                //if (Convert.ToDouble(lblTotal.Text) <= 0.00)
              // {
                    if (Convert.ToDouble(txtCalculateAmt.Text == "" ? "0" : txtCalculateAmt.Text) <= 0.00)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Total amount must be greater than 0", this);
                        return;
                    }
               // }
            }
            if (DateTime.Compare(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
                txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtDate.Text)) == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
                txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDate.Focus();
                return;
            }

            if (GridData.Rows.Count == 0)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Transaction Entries Are Not Available.", this);

                //rowMsg.Style["Display"] = "block";
                //lblMsg.Text = "Transaction Entries Are Not Available.";
                txtAcc.Focus();
                return;
            }
            else
            {
                int t = 0;
                string PartyIds = string.Empty;
                for (t = 0; t < GridData.Rows.Count; t++)
                {
                    HiddenField hdnpartyCB = GridData.Rows[t].FindControl("hdnPartyNo") as HiddenField;
                    if (hdnpartyCB != null)
                    {
                        if (PartyIds == string.Empty)

                            PartyIds = "'" + Convert.ToInt32(hdnpartyCB.Value.ToString().Trim()).ToString() + "'";
                        else
                            PartyIds = PartyIds + "," + "'" + Convert.ToInt32(hdnpartyCB.Value.ToString().Trim()).ToString() + "'";
                    }
                }
                if (isSingleMode == "N")
                {
                    if (ddlTranType.SelectedValue.ToString().Trim() != "J")
                    {
                        if (ValidateCashBankLedger(PartyIds) == false)
                        {
                            txtAcc.Focus();
                            return;
                        }
                    }
                }
            }

            if (hdnAskSave.Value == "1")
            {
                if (isSingleMode == "N")
                {
                    //if (lblTotalDiff.Text.ToString().Trim() != "0" || lblTotalDiff.Text.ToString().Trim() != "0.00")
                    //Updated by vijay andoju
                    if (Convert.ToDouble(lblTotalDiff.Text.ToString().Trim()) > 0)

                        if (lblTotalDiff.Text.ToString().Trim() != "0" || lblTotalDiff.Text.ToString().Trim() != "0.00")
                        {
                            //rowMsg.Style["Display"] = "block";
                            //lblMsg.Text = "Balancing is not match.";
                            objCommon.DisplayUserMessage(UPDLedger, "Balancing is not match.", this);
                            txtAcc.Focus();
                            return;
                        }
                    //else
                    //{
                    //    lblMsg.Text = "";
                    //    rowMsg.Style["Display"] = "none";
                    //}
                }

                int subtrno = 0;
                if (isSingleMode == "Y")
                {
                    if (txtAgainstAcc.Text == "")
                    {
                        //rowMsg.Style["Display"] = "block";
                        //lblMsg.Text = "Please Enter Against Account.";
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter Against Account.", this);
                        txtAgainstAcc.Focus();
                        return;
                    }
                    //else
                    //{
                    //    rowMsg.Style["Display"] = "none";
                    //}
                }
                //else { rowMsg.Style["Display"] = "none"; }
                if (ViewState["isEdit"].ToString().Trim() == "Y")
                {
                    string IsModify = string.Empty;
                    if (ViewState["isModi"] != null)
                        IsModify = "Y";
                    else
                        IsModify = "N";

                    //AccountTransactionController objPC5 = new AccountTransactionController();
                    //objPC5.DeleteTransactionResult(Convert.ToInt32(para[1].ToString().Trim()), Session["fin_yr"].ToString().Trim(), Session["comp_code"].ToString().Trim(), ddlTranType.SelectedValue.ToString(), IsModify);

                }
                if (ViewState["TDS"].ToString() == "Yes")
                {
                    //SetOppositPartyStringForJournal();
                }
                else
                {
                    SetOppositPartyString();
                }
                if (ddlTranType.SelectedValue.ToString().Trim() == "J")
                {
                    SetOppositPartyStringForJournal();
                }

                XmlDocument objXMLDoc = new XmlDocument();


                //ReadXML("N");
                XmlDeclaration xmlDeclaration = objXMLDoc.CreateXmlDeclaration("1.0", null, null);

                // Create the root element
                XmlElement rootNode = objXMLDoc.CreateElement("tables");
                objXMLDoc.InsertBefore(xmlDeclaration, objXMLDoc.DocumentElement);
                objXMLDoc.AppendChild(rootNode);



                DataTable dt = new DataTable();
                Session["SUBTR_NO"] = 0;
                for (int i = 0; i < GridData.Rows.Count; i++)
                {

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
                    if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
                        PARTY_NAME.InnerText = "-";
                    else
                        PARTY_NAME.InnerText = txtPartyName.Text.ToString();

                    XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
                    if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
                        PAN_NO.InnerText = "-";
                    else
                        PAN_NO.InnerText = txtPanNo.Text.ToString();

                    XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
                    if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
                        NATURE_SERVICE.InnerText = "-";
                    else
                        NATURE_SERVICE.InnerText = txtNatureService.Text.ToString();

                    SUBTR_NO.InnerText = "0";
                    Session["SUBTR_NO"] = i;
                    if (txtDate.Text.ToString().Trim() != "")
                    {

                        DateTime TranDate = Convert.ToDateTime(txtDate.Text);
                        TRANSACTION_DATE.InnerText = TranDate.ToString("dd-MMM-yyyy");
                        ViewState["TRANDATE"] = Convert.ToDateTime(txtDate.Text);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter The Transaction Date.", this);
                        txtDate.Focus();
                        return;
                    }

                    XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
                    TRANSACTION_TYPE.InnerText = ddlTranType.SelectedValue.ToString().Trim();
                    HiddenField hdnOpParty = GridData.Rows[i].FindControl("hdnOppParty") as HiddenField;
                    if (hdnOpParty != null)
                    {
                        OPARTY.InnerText = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                        ViewState["OPartyNo"] = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                    }

                    HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                    if (hdnparty != null)
                    {
                        PARTY_NO.InnerText = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                        ViewState["PartyNo"] = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                    }
                    else { return; }


                    if (hdnlblLedgerAmount.Value == hdnparty.Value.ToString().Trim())
                    {
                        TRAN.InnerText = hdnLedgerMode.Value.Trim();
                        AMOUNT.InnerText = lbltotalamount.Text.Trim();
                        ViewState["Amount"] = AMOUNT.ToString();
                    }

                    if (hdnlblLedgerAmount1.Value == hdnparty.Value.ToString().Trim())
                    {
                        TRAN.InnerText = hdnLedgerMode1.Value.Trim();
                        AMOUNT.InnerText = lbltotalamount1.Text.Trim();
                        ViewState["Amount"] = AMOUNT.ToString();
                    }

                    if (hdnlblLedgerAmount2.Value == hdnparty.Value.ToString().Trim())
                    {
                        TRAN.InnerText = hdnLedgerMode2.Value.Trim();
                        AMOUNT.InnerText = lbltotalamount2.Text.Trim();
                        ViewState["Amount"] = AMOUNT.ToString();
                    }

                    //if (GridData.Columns[4].Visible == true)
                    //{
                    //    TRAN.InnerText = GridData.Rows[i].Cells[7].Text.ToString().Trim();
                    //    AMOUNT.InnerText = GridData.Rows[i].Cells[4].Text.ToString().Trim();
                    //    ViewState["Amount"] = AMOUNT.InnerText;
                    //}
                    //else if (GridData.Columns[4].Visible == false)
                    //{
                    //    if (GridData.Rows[i].Cells[5].Text.ToString().Trim() == "0.00")
                    //    {
                    //        TRAN.InnerText = "Cr";
                    //        AMOUNT.InnerText = GridData.Rows[i].Cells[6].Text.ToString().Trim();
                    //        ViewState["Amount"] = AMOUNT.ToString();
                    //    }
                    //    else
                    //    {
                    //        TRAN.InnerText = "Dr";
                    //        AMOUNT.InnerText = GridData.Rows[i].Cells[5].Text.ToString().Trim();
                    //        ViewState["Amount"] = AMOUNT.ToString();
                    //    }
                    //}

                    XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
                    DEGREE_NO.InnerText = "0";

                    if (isVoucherAuto == "Y")
                    {
                        if (IsNumeric(txtVoucherNo.Text.ToString().Trim()) == false)
                        {
                            objCommon.DisplayUserMessage(UPDLedger, "Invalid Voucher No.", this);
                            txtVoucherNo.Focus();
                            return;
                        }
                    }


                    if (ViewState["isEdit"].ToString().Trim() != "Y")
                    {
                        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "N")
                        {
                            if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER NO SEPRATE FOR RCPT,PAY,CONT,JOUN'") == "Y")
                            {
                                if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "count(*)", "VOUCHER_NO='" + txtVoucherNo.Text.Trim() + "' and TRANSACTION_TYPE='" + ddlTranType.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN convert(datetime,'" + Session["fin_date_from"].ToString() + "',103) AND convert(datetime,'" + Session["fin_date_to"] + "',103)")) > 0)
                                {
                                    txtVoucherNo.Text = string.Empty;
                                    objCommon.DisplayUserMessage(UPDLedger, "Voucher No is already exist", this.Page);
                                    return;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "count(*)", "VOUCHER_NO='" + txtVoucherNo.Text.Trim() + "' AND TRANSACTION_DATE BETWEEN convert(datetime,'" + Session["fin_date_from"].ToString() + "',103) AND convert(datetime,'" + Session["fin_date_to"] + "',103)")) > 0)
                                {
                                    txtVoucherNo.Text = string.Empty;
                                    objCommon.DisplayUserMessage(UPDLedger, "Voucher No is already exist", this.Page);
                                    return;
                                }
                            }
                        }
                    }

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

                    if (txtChqNo2.Text.ToString().Trim() == string.Empty)
                    {

                        CHQ_NO.InnerText = "0";
                        ViewState["CHQ_NO"] = "0";
                    }
                    else
                    {
                        CHQ_NO.InnerText = txtChqNo2.Text.Trim();
                        ViewState["CHQ_NO"] = txtChqNo2.Text.Trim();
                    }

                    HiddenField hdn1 = GridData.Rows[i].FindControl("hdnChqDate") as HiddenField;

                    if (txtChequeDt2.Text != "")
                    {

                        DateTime CHQ_DATE1 = Convert.ToDateTime(txtChequeDt2.Text);
                        CHQ_DATE.InnerText = CHQ_DATE1.ToString("dd-MMM-yyyy");

                        ViewState["CHQ_DATE"] = CHQ_DATE1.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        DateTime CHQ_DATE2 = Convert.ToDateTime(txtDate.Text);
                        CHQ_DATE.InnerText = CHQ_DATE2.ToString("dd-MMM-yyyy");

                        ViewState["CHQ_DATE"] = CHQ_DATE2.ToString("dd-MMM-yyyy");

                    }

                    XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
                    CHALLAN.InnerText = "false";
                    XmlElement CAN = objXMLDoc.CreateElement("CAN");
                    CAN.InnerText = "false";
                    XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
                    DCR_NO.InnerText = "0";

                    //Commented and Add by Nakul Chawre @28052016 to add cost center
                    XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
                    HiddenField hdnCostCenter = GridData.Rows[i].FindControl("hdnCostCenterID") as HiddenField;
                    if (hdnCostCenter.Value != "")
                        CC_ID.InnerText = hdnCostCenter.Value.ToString();
                    else
                        CC_ID.InnerText = "0";

                    //Add by Nakul Chawre on @23062017 to add Budget Head
                    XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
                    HiddenField hdnBudgetHeadId = GridData.Rows[i].FindControl("hdnBudgetHeadId") as HiddenField;
                    if (hdnBudgetHeadId.Value != "")
                        BudgetNo.InnerText = hdnBudgetHeadId.Value.ToString();
                    else
                        BudgetNo.InnerText = "0";
                    //Added by vijay andoju 09-07-2020 to add Department 



                    XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
                    HiddenField hdnDepartmentId = GridData.Rows[i].FindControl("hdnDepartmentId") as HiddenField;
                    if (hdnDepartmentId.Value != "")
                    {
                        DepartmentId.InnerText = hdnDepartmentId.Value.ToString();
                    }
                    else
                    {
                        DepartmentId.InnerText = "0";
                    }
                    XmlElement IsTDSApplicable = objXMLDoc.CreateElement("IsTDSApplicable");
                    if (ViewState["TDS"].ToString() == "Yes")
                    {
                        IsTDSApplicable.InnerText = "1";
                    }
                    else
                    {
                        IsTDSApplicable.InnerText = "0";
                    }
                    XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
                    CASH_BANK_NO.InnerText = "0";
                    XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
                    ADVANCE_REFUND_NONE.InnerText = "N";
                    XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
                    PAGENO.InnerText = "0";
                    XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");

                    if (isPerNarration == "Y")
                    {
                        dt = Session["Datatable"] as DataTable;

                        if (dt.Rows.Count != 0)
                        {
                            PARTICULARS.InnerText = dt.Rows[i]["Narration"].ToString().Trim();// ViewState["OrigData"].ToString().Trim();//GridData.Rows[i].Cells[3].Text.ToString().Trim();
                        }
                        else
                        {
                            PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim();
                        }
                    }
                    else
                    {
                        string narration = txtNarration.Text.Replace("'", "''");
                        if (txtNarration.Text != string.Empty)
                        {
                            PARTICULARS.InnerText = narration;
                        }
                        else
                        {
                            PARTICULARS.InnerText = "-";
                        }
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

                    XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
                    XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
                    XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");
                    if (ViewState["TDS"].ToString() == "Yes")
                    {
                        HiddenField hdnsection = GridData.Rows[i].FindControl("hdnsection") as HiddenField;
                        HiddenField hdnTDSamount = GridData.Rows[i].FindControl("hdnTDSamount") as HiddenField;
                        HiddenField hdnTDSpercentage = GridData.Rows[i].FindControl("hdnTDSPersentage") as HiddenField;


                        TDSSection.InnerText = hdnsection.Value == "" ? "0" : hdnsection.Value;
                        TDSAMOUNT.InnerText = hdnTDSamount.Value == "" ? "0" : hdnTDSamount.Value;
                        TDPersentage.InnerText = hdnTDSpercentage.Value == "" ? "0" : hdnTDSpercentage.Value;
                    }
                    else
                    {
                        TDSSection.InnerText = "0";
                        TDSAMOUNT.InnerText = "0";
                        TDPersentage.InnerText = "0";
                    }

                    // added by Shrikant A on 22-04-2021
                    XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
                    XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
                    XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
                    //if (ViewState["TDSonGST"].ToString() == "Yes")
                    //{
                    //    HiddenField hdnTDSonGSTsection = GridData.Rows[i].FindControl("hdnTDSonGSTsection") as HiddenField;
                    //    HiddenField hdnTDSGSTonAmount = GridData.Rows[i].FindControl("hdnTDSGSTonAmount") as HiddenField;
                    //    HiddenField hdnTDSonGSTPersentage = GridData.Rows[i].FindControl("hdnTDSonGSTPersentage") as HiddenField;


                    //    TDSonGSTSection.InnerText = hdnTDSonGSTsection.Value == "" ? "0" : hdnTDSonGSTsection.Value;
                    //    TDSGSTonAMOUNT.InnerText = hdnTDSGSTonAmount.Value == "" ? "0" : hdnTDSGSTonAmount.Value;
                    //    TDSonGSTPersentage.InnerText = hdnTDSonGSTPersentage.Value == "" ? "0" : hdnTDSonGSTPersentage.Value;
                    //}
                    //else
                    //{
                    TDSonGSTSection.InnerText = "0";
                    TDSGSTonAMOUNT.InnerText = "0";
                    TDSonGSTPersentage.InnerText = "0";
                    //}


                    //ADDEDE BY VIJAY ON 26082020 FOR GST&IGST
                    XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
                    //IsIGSTApplicable.InnerText = ViewState["IsIGST"].ToString() == "Yes" ? "1" : "0";
                    IsIGSTApplicable.InnerText = "0";
                    XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
                    XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
                    XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

                    //if (ViewState["IsIGST"].ToString() == "Yes")
                    //{
                    //    HiddenField hdnIGSTper = GridData.Rows[i].FindControl("hdnIGSTper") as HiddenField;
                    //    HiddenField hdnIGSTAmountr = GridData.Rows[i].FindControl("hdnIGSTAMOUNT") as HiddenField;
                    //    HiddenField hdnIGSTonAmount = GridData.Rows[i].FindControl("hdnIGSTONAMOUNT") as HiddenField;
                    //    IGSTPER.InnerText = hdnIGSTper.Value;
                    //    IGSTAMOUNT.InnerText = hdnIGSTAmountr.Value;
                    //    IGSTonAmount.InnerText = hdnIGSTonAmount.Value;
                    //}
                    //else
                    //{
                    IGSTPER.InnerText = "0";
                    IGSTAMOUNT.InnerText = "0";
                    IGSTonAmount.InnerText = "0";
                    //}


                    XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
                    XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
                    XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
                    XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
                    XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
                    XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

                    XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");

                    //if (ViewState["IsGST"].ToString() == "Yes")
                    //{
                    //    HiddenField hdnCGSTper = GridData.Rows[i].FindControl("hdnCGSTper") as HiddenField;
                    //    HiddenField hdnCGSTAmountr = GridData.Rows[i].FindControl("hdnCGSTAMOUNT") as HiddenField;
                    //    HiddenField hdnCGSTonAmount = GridData.Rows[i].FindControl("hdnCGSTONAMOUNT") as HiddenField;

                    //    HiddenField hdnSGSTper = GridData.Rows[i].FindControl("hdnSGSTper") as HiddenField;
                    //    HiddenField hdnSGSTAmountr = GridData.Rows[i].FindControl("hdnSGSTAMOUNT") as HiddenField;
                    //    HiddenField hdnSGSTonAmount = GridData.Rows[i].FindControl("hdnSGSTONAMOUNT") as HiddenField;

                    //    CGSTper.InnerText = hdnCGSTper.Value;
                    //    CGSTamount.InnerText = hdnCGSTAmountr.Value;
                    //    CGSTonamount.InnerText = hdnCGSTonAmount.Value;

                    //    SGSTper.InnerText = hdnSGSTper.Value;
                    //    SGSTamount.InnerText = hdnSGSTAmountr.Value;
                    //    SGSTonamount.InnerText = hdnSGSTonAmount.Value;

                    //    SGSTApplicable.InnerText = "1";
                    //}
                    //else
                    //{
                    CGSTper.InnerText = "0";
                    CGSTamount.InnerText = "0";
                    CGSTonamount.InnerText = "0";

                    SGSTper.InnerText = "0";
                    SGSTamount.InnerText = "0";
                    SGSTonamount.InnerText = "0";
                    SGSTApplicable.InnerText = "0";
                    //}

                    XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

                    if (txtGSTNNO.Text == "" || txtGSTNNO.Text == string.Empty || txtGSTNNO.Text == null)
                    {
                        GSTIN_NO.InnerText = "-";
                    }
                    else
                    {
                        GSTIN_NO.InnerText = txtGSTNNO.Text;
                    }



                    DateTime todate = Convert.ToDateTime(txtDate.Text.Trim());
                    todate = todate.AddDays(1);
                    string voucherNo1 = string.Empty;
                    if (isEdit == "Y")
                    {
                        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
                        {
                            //Added by Nakul Chawre at 10-02-2017 for requirement of SVVV for Voucher No Modification
                            if (ViewState["isModi"].ToString() != "Y")
                            {
                                voucherNo1 = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='" + ddlTranType.SelectedValue.ToString() + "'");
                                VOUCHER_NO.InnerText = voucherNo1;
                                ViewState["VoucherNo"] = voucherNo1;
                            }
                            else
                            {
                                voucherNo1 = txtVoucherNo.Text.Trim();
                                VOUCHER_NO.InnerText = txtVoucherNo.Text.Trim();
                                ViewState["VoucherNo"] = txtVoucherNo.Text.Trim();
                            }

                            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherNo1;// txtVoucherNo.Text.ToString().Trim();
                            }
                            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + voucherNo1;//txtVoucherNo.Text.ToString().Trim();
                            }
                            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherNo1;//txtVoucherNo.Text.ToString().Trim();
                            }
                            else
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + voucherNo1;//txtVoucherNo.Text.ToString().Trim();
                            }
                        }
                        else
                        {
                            VOUCHER_NO.InnerText = txtVoucherNo.Text.Trim();
                            ViewState["VoucherNo"] = txtVoucherNo.Text.Trim();

                            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + txtVoucherNo.Text.Trim();// txtVoucherNo.Text.ToString().Trim();
                            }
                            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + txtVoucherNo.Text.Trim();//txtVoucherNo.Text.ToString().Trim();
                            }
                            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + txtVoucherNo.Text.Trim();//txtVoucherNo.Text.ToString().Trim();
                            }
                            else
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + txtVoucherNo.Text.Trim();//txtVoucherNo.Text.ToString().Trim();
                            }
                        }
                        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
                        STR_CB_VOUCHER_NO.InnerText = StrVno;
                    }
                    else
                    {
                        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
                        {
                            voucherNo1 = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='" + ddlTranType.SelectedValue.ToString() + "'");

                            VOUCHER_NO.InnerText = voucherNo1;
                            ViewState["VoucherNo"] = voucherNo1;

                            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherNo1;//  txtVoucherNo.Text.ToString().Trim();
                            }

                            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + voucherNo1;//txtVoucherNo.Text.ToString().Trim();
                            }
                            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherNo1;//txtVoucherNo.Text.ToString().Trim();
                            }
                            else
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + voucherNo1;//txtVoucherNo.Text.ToString().Trim();
                            }
                        }
                        else
                        {
                            VOUCHER_NO.InnerText = txtVoucherNo.Text.Trim();
                            ViewState["VoucherNo"] = txtVoucherNo.Text.Trim();

                            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + txtVoucherNo.Text.Trim();//  txtVoucherNo.Text.ToString().Trim();
                            }

                            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + txtVoucherNo.Text.Trim();//txtVoucherNo.Text.ToString().Trim();
                            }
                            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + txtVoucherNo.Text.Trim();//txtVoucherNo.Text.ToString().Trim();
                            }
                            else
                            {
                                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + txtVoucherNo.Text.Trim();//txtVoucherNo.Text.ToString().Trim();
                            }
                        }

                        //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();

                        STR_CB_VOUCHER_NO.InnerText = StrVno;
                    }

                    ProjectId.InnerText = ddlSponsor.SelectedValue;

                    HiddenField hdnSubproject = GridData.Rows[i].FindControl("hdnSubproject") as HiddenField;

                    ProjectSubId.InnerText = hdnSubproject.Value == "" ? "0" : hdnSubproject.Value;

                    BILL_ID.InnerText = ddlBillNo.SelectedValue;

                    XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
                    PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

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
                    //Added by vijay andoju 07-07-2020

                    objElement.AppendChild(TDSSection);
                    objElement.AppendChild(TDSAMOUNT);
                    objElement.AppendChild(TDPersentage);
                    // added by Shrikant A on 21-04-2021
                    objElement.AppendChild(TDSonGSTSection);
                    objElement.AppendChild(TDSGSTonAMOUNT);
                    objElement.AppendChild(TDSonGSTPersentage);
                    objElement.AppendChild(DepartmentId);
                    //ADDED BY VIJAY ANDOJU 26082020 FOR GST&IGST
                    objElement.AppendChild(IsIGSTApplicable);
                    objElement.AppendChild(IGSTAMOUNT);
                    objElement.AppendChild(IGSTPER);
                    objElement.AppendChild(IGSTonAmount);
                    objElement.AppendChild(CGSTamount);
                    objElement.AppendChild(CGSTper);
                    objElement.AppendChild(CGSTonamount);
                    objElement.AppendChild(SGSTamount);
                    objElement.AppendChild(SGSTper);
                    objElement.AppendChild(SGSTonamount);
                    objElement.AppendChild(SGSTApplicable);
                    objElement.AppendChild(GSTIN_NO);

                    objElement.AppendChild(PAYMENT_MODE);

                    objXMLDoc.DocumentElement.AppendChild(objElement);
                    //WriteXML(objXMLDoc);
                    //string path = "D:\\Account\\VSS_ACC_Finance\\UAIMS\\PresentationLayer\\ACCOUNT\\ArrangeData.xml";
                }


                if (isSingleMode == "Y")
                {
                    objXMLDoc = ConsolidateTransactionEntry1(objXMLDoc, ViewState["VoucherNo"].ToString());
                    isAllreadySet = "";
                }
                //objXMLDoc.AppendChild(xmlMain);
                ViewState["Voucher"] = ddlTranType.SelectedItem.ToString().Trim();
                //XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("~/ArrangeData.xml"));
                //XmlTextReader xmlreader = new XmlTextReader(objXMLDoc.ToString());
                //DataSet ds = new DataSet();

                //ds.ReadXml(xmlreader);

                //xmlreader.Close();

                if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "N")
                {
                    if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER NO SEPRATE FOR RCPT,PAY,CONT,JOUN'") == "Y")
                    {
                        if (ViewState["isModi"].ToString() == "N")
                        {
                            if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "count(*)", "VOUCHER_NO='" + txtVoucherNo.Text.Trim() + "' and TRANSACTION_TYPE='" + ddlTranType.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN convert(datetime,'" + Session["fin_date_from"].ToString() + "',103) AND convert(datetime,'" + Session["fin_date_to"] + "',103)")) > 0)
                            {
                                txtVoucherNo.Text = string.Empty;
                                objCommon.DisplayUserMessage(UPDLedger, "Voucher No is already exist", this.Page);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "count(*)", "VOUCHER_NO='" + txtVoucherNo.Text.Trim() + "' AND TRANSACTION_DATE BETWEEN convert(datetime,'" + Session["fin_date_from"].ToString() + "',103) AND convert(datetime,'" + Session["fin_date_to"] + "',103)")) > 0)
                        {
                            txtVoucherNo.Text = string.Empty;
                            objCommon.DisplayUserMessage(UPDLedger, "Voucher No is already exist", this.Page);
                            return;
                        }
                    }
                }


                if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
                {
                    string IsModify = string.Empty;
                    int VoucherSqn = 0;
                    if (ViewState["isModi"] != null && ViewState["isModi"].ToString() == "Y")
                    {
                        IsModify = "Y";
                        VoucherSqn = Convert.ToInt32(para[1].ToString().Trim());
                    }
                    else
                    {
                        IsModify = "N";
                        VoucherSqn = 0;
                    }
                    //voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify);
                    AddExistingEmpData();

                    if (isTempVoucher == "Y")
                    {
                        voucherno = objPC1.AddTransactionWithXMLMakautBulk(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), ddlTranType.SelectedValue.ToString());
                    }
                    else
                    {
                        voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), ddlTranType.SelectedValue.ToString());
                    }

                    int result;
                    if (isTempVoucher == "Y")
                    {
                        result = objPC1.InsertBulkEmployeeVoucherData((DataTable)ViewState["RecTblbuklvouchr"], Convert.ToInt32(hdnlblLedgerAmount.Value == "" ? "0" : hdnlblLedgerAmount.Value), hdnLedgerMode.Value, Convert.ToInt32(hdnlblLedgerAmount1.Value == "" ? "0" : hdnlblLedgerAmount1.Value), hdnLedgerMode1.Value, Convert.ToInt32(hdnlblLedgerAmount2.Value == "" ? "0" : hdnlblLedgerAmount2.Value), hdnLedgerMode2.Value, voucherno, Convert.ToDateTime(txtDate.Text), Session["comp_code"].ToString().Trim());
                    }
                    else
                    {
                        result = objPC1.InsertBulkEmployeeVoucherDataNew((DataTable)ViewState["RecTblbuklvouchr"], Convert.ToInt32(hdnlblLedgerAmount.Value == "" ? "0" : hdnlblLedgerAmount.Value), hdnLedgerMode.Value, Convert.ToInt32(hdnlblLedgerAmount1.Value == "" ? "0" : hdnlblLedgerAmount1.Value), hdnLedgerMode1.Value, Convert.ToInt32(hdnlblLedgerAmount2.Value == "" ? "0" : hdnlblLedgerAmount2.Value), hdnLedgerMode2.Value, voucherno, Convert.ToDateTime(txtDate.Text), Session["comp_code"].ToString().Trim());
                    }

                }
                else
                {
                    string IsModify = string.Empty;
                    int VoucherSqn = 0;
                    if (ViewState["isModi"].ToString() == "Y")
                    {
                        IsModify = "Y";
                        VoucherSqn = Convert.ToInt32(para[1].ToString().Trim());
                        //voucherno = objPC1.AddTransactionWithXMLForManual(objXMLDoc, Session["comp_code"].ToString().Trim(), VoucherSqn);

                    }

                    voucherno = objPC1.AddTransactionWithXMLForManual(objXMLDoc, Session["comp_code"].ToString().Trim(), VoucherSqn, IsModify, ddlTranType.SelectedValue.ToString());
                }

                int VoucherSqnNum = voucherno;
                  //added by tanu 13/12/2021 for add multipal bill fill 
               
                    {
                    DataTable dtD = null;
                    if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
                    {
                        dtD = (DataTable)ViewState["DOCS"];
                    }
                    else
                    {
                        CreateTable();
                        dtD = (DataTable)ViewState["DOCS"];
                    }
                    if (dtD.Rows.Count > 0)
                    {
                        if (ViewState["isModi"].ToString() == "Y")
                        {
                            objATEnt.OLD_VOUCHER_SQN = Convert.ToInt32(para[1].ToString().Trim());
                            //objATEnt.OLD_VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_ALL_TEMP_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));
                        }
                        else
                        {
                            objATEnt.OLD_VOUCHER_SQN = '0';
                        }
                        if (ViewState["isModi"].ToString() == "Y")
                        {
                            objATEnt.VOUCHER_SQN = Convert.ToInt32(voucherno);
                            objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_ALL_TEMP_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));


                            if (dtD.Rows.Count > 0)
                            {
                                objATEnt.FILEPATH = Docpath + "VOUCHER_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]) + "\\BillNo_" + objATEnt.VOUCHER_NO;
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    AddDocuments(objATEnt.VOUCHER_NO);
                                }
                            }
                            else
                            {
                                objATEnt.FILEPATH = "";
                            }
                        }
                        else
                        {

                            objATEnt.VOUCHER_SQN = Convert.ToInt32(voucherno);
                            objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_ALL_TEMP_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));

                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                objATEnt.FILEPATH = ViewState["DESTINATION_PATH"].ToString() + "\\BillNo_" + objATEnt.VOUCHER_NO;
                            }
                        }
                        //end
                        int ret = objPC1.AddBillBulkVoucherCreation(objATEnt, Convert.ToInt32(Session["userno"].ToString()), dtD, Session["comp_code"].ToString().Trim(), Convert.ToInt32(Session["comp_no"].ToString().Trim()));
                        if (ret == 1)
                        {
                        }
                        else if (ret == 2)
                        {
                            AddDocuments(objATEnt.VOUCHER_NO);
                        }

                        ViewState["DESTINATION_PATH"] = null;
                        ViewState["DOCS"] = null;
                        clearDoc();
                        ViewState["letrno"] = null;
                    }
                }





                //Added by vijay andoju 02092020
                if (ViewState["IsBudgetHead"].ToString() == "Yes")
                {
                    if (ddlTranType.SelectedValue.ToString() == "P" || ddlTranType.SelectedValue.ToString() == "R")
                    {
                        int i;

                        if (isTempVoucher == "Y")
                        {
                            i = objPC1.AddBudgetTransactionMakaut(Session["comp_code"].ToString().Trim(), Convert.ToString(voucherno), ddlTranType.SelectedValue.ToString());
                        }
                        else
                        {
                            i = objPC1.AddBudgetTransaction(Session["comp_code"].ToString().Trim(), Convert.ToString(voucherno), ddlTranType.SelectedValue.ToString());
                        }

                        if (i > 0)
                        {

                        }
                        else
                        {

                        }
                    }
                }

                ViewState["VoucherNo"] = voucherno;
                ViewState["VNO"] = ViewState["VoucherNo"].ToString();
                ViewState["Date"] = txtDate.Text;
                for (int j = 0; j < GridData.Rows.Count; j++)
                {
                    AccountTransaction objPC = new AccountTransaction();
                    HiddenField hdnCCID = GridData.Rows[j].FindControl("hdnCostCenterID") as HiddenField;
                    if (hdnCCID.Value == string.Empty)
                    {
                        hdnCCID.Value = "0";
                    }

                    if (hdnCCID.Value != "0")
                    {
                        int retval = 0;
                        if (ViewState["isEdit"].ToString().Trim() == "Y")
                        {
                            if (isTempVoucher == "Y")
                            {
                                retval = objCostCenterController.CostCenterVoucherDeleteMakaut(Convert.ToInt32(para[1].ToString().Trim()), ddlTranType.SelectedValue.ToString(), Convert.ToInt32(ViewState["PartyNo"].ToString()), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString());
                            }
                            else
                            {
                                retval = objCostCenterController.CostCenterVoucherDelete(Convert.ToInt32(para[1].ToString().Trim()), ddlTranType.SelectedValue.ToString(), Convert.ToInt32(ViewState["PartyNo"].ToString()), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString());
                            }
                        }
                        else
                        {
                            if (isTempVoucher == "Y")
                            {
                                retval = objCostCenterController.CostCenterVoucherDeleteMakaut(voucherno, ddlTranType.SelectedValue.ToString(), Convert.ToInt32(ViewState["PartyNo"].ToString()), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString());
                            }
                            else
                            {
                                retval = objCostCenterController.CostCenterVoucherDelete(voucherno, ddlTranType.SelectedValue.ToString(), Convert.ToInt32(ViewState["PartyNo"].ToString()), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString());
                            }
                        }
                        if (retval == 1)
                        {
                            objCostCenter.VCHNO = voucherno;
                            objCostCenter.PARTY_NO = Convert.ToInt32(ViewState["PartyNo"].ToString());
                            objCostCenter.CCVHDATE = Convert.ToDateTime(txtDate.Text.Trim());
                            objCostCenter.CC_ID = Convert.ToInt32(hdnCCID.Value);
                            string CatID = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_CostCenter", "Cat_ID", "CC_ID=" + Convert.ToString(hdnCCID.Value));
                            objCostCenter.CATID = Convert.ToInt32(CatID);
                            objCostCenter.VCHTYPE = ddlTranType.SelectedValue.ToString();

                            if (isTempVoucher == "Y")
                            {
                                objCostCenterController.CostCenterVoucherAddMakaut(objCostCenter, Session["comp_code"].ToString().Trim());
                            }
                            else
                            {
                                objCostCenterController.CostCenterVoucherAdd(objCostCenter, Session["comp_code"].ToString().Trim());
                            }

                        }
                    }
                }

                DataSet dsResult = null;
                if (isTempVoucher == "Y")
                {
                    dsResult = objPC1.GetTransactionResultMakaut(voucherno, Session["comp_code"].ToString(), ddlTranType.SelectedValue.ToString());
                }
                else
                {
                    dsResult = objPC1.GetTransactionResult(voucherno, Session["comp_code"].ToString(), ddlTranType.SelectedValue.ToString());
                }


                if (ViewState["PaymentType"].ToString() == "P" && ViewState["PaymentTypeNo"].ToString() == "2")
                {
                    if (objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_config", "PARAMETER", "CONFIGDESC='ENABLE CHEQUE PRINTING'") == "N")
                    {
                        btnchequePrint.Visible = false;
                    }
                    else
                    {
                        string tranno = dsResult.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                        string partyName = dsResult.Tables[0].Rows[0]["LEDGER"].ToString();
                        string amount = string.Empty;
                        if (Convert.ToDecimal(dsResult.Tables[0].Rows[0]["DEBIT"].ToString()) > 0)
                            amount = dsResult.Tables[0].Rows[0]["DEBIT"].ToString();
                        else
                            amount = dsResult.Tables[0].Rows[0]["CREDIT"].ToString();

                        string CHQ_NO = dsResult.Tables[0].Rows[0]["CHQ_NO"].ToString();

                        btnchequePrint.Attributes.Add("onclick", "ShowChequePrintingTran('" + txtChqNo2.Text + "','" + tranno + "','" + partyName + "','" + amount + "','0','" + CHQ_NO + "')");
                    }
                }
                else
                {
                    btnchequePrint.Visible = false;
                }
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count != 0)
                    {
                        //lblMsg.Text = "";
                        //rowMsg.Style["Display"] = "none";
                        lvGrp.DataSource = dsResult.Tables[0];
                        lvGrp.DataBind();
                        lblTotal.Text = "0.00";
                        lblTotalCredit.Text = "0.00";
                        lblTotalDebit.Text = "0.00";
                        if (isvoucher_Cheque_Print == "Y")
                            upd_ModalPopupExtender1.Show();
                        objPC1.UpdateBalanceAllLedger();
                        //Added by Nakul Chawre
                        if (ViewState["isCopy"] != null && Request.QueryString["ledger"] == null)
                        {
                            if (isTempVoucher == "Y")
                            {
                                Response.Redirect("AccAllVocherModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                            }
                            else
                            {
                                //Response.Redirect("AccountingVouchersModifications.aspx");
                                Response.Redirect("AccountingVouchersModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                                //btnSave.Enabled = true;
                            }
                        }
                        if (Request.QueryString["obj"] != null && Request.QueryString["ledger"] == null)
                        {
                            if (isTempVoucher == "Y")
                            {
                                Response.Redirect("AccAllVocherModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                            }
                            else
                            {
                                //Response.Redirect("AccountingVouchersModifications.aspx");
                                //Added by Nakul Chawre
                                Response.Redirect("AccountingVouchersModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                                //btnSave.Enabled = true;
                            }
                        }
                        if (Request.QueryString["ledger"] != null && Request.QueryString["party_no"] != null)
                        {
                            if (Request.QueryString["obj"].ToString().Trim().Split(',')[0] == "LedgerReport")
                            {
                                Response.Redirect("LedgerReport.aspx?ledger=" + Request.QueryString["ledger"].ToString() + "&party_no=" + Request.QueryString["party_no"].ToString() + "&fromDate=" + Request.QueryString["fromDate"].ToString() + "&Todate=" + Request.QueryString["Todate"].ToString());
                                btnReset_Click(sender, e);
                            }
                            else
                            {
                                Response.Redirect("Acc_ledgerReportGrid.aspx?ledger=" + Request.QueryString["ledger"].ToString() + "&party_no=" + Request.QueryString["party_no"].ToString() + "&fromDate=" + Request.QueryString["fromDate"].ToString() + "&Todate=" + Request.QueryString["Todate"].ToString());
                                btnReset_Click(sender, e);
                            }
                        }
                        if (Request.QueryString["ISBACK"] != null)
                        {
                            if (Request.QueryString["ISBACK"].ToString() == "TRUE")
                            {
                                Response.Redirect("TrialbalanceReport_forGrid.aspx?pageno=" + Request.QueryString["pageno"]);
                                btnReset_Click(sender, e);
                            }
                            else
                            {
                                Response.Redirect("Acc_ledgerReportGrid.aspx?ISBACK=FALSE");
                                btnReset_Click(sender, e);
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Performed Successfully", this.Page);
                    }


                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Performed Successfully", this.Page);
                }

            }
            btnReset_Click(sender, e);
            //VoucherNoSetting();
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Exception occur please try again", this.Page);
            //btnSave.Enabled = true;
        }
    }

    private XmlDocument ConsolidateTransactionEntry1(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        string opartytdsstring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        int subtrno = Convert.ToInt32(Session["SUBTR_NO"].ToString()) + 1;
        SUBTR_NO.InnerText = "1";
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
        AmtWithoutGST.InnerText = "0";

        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        GSTPercent.InnerText = "0";

        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        IsGSTApplicable.InnerText = "0";

        XmlElement Section = objXMLDoc.CreateElement("SECTION");
        Section.InnerText = "0";

        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
            PARTY_NAME.InnerText = "-";
        else
            PARTY_NAME.InnerText = txtPartyName.Text.ToString();

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
            PAN_NO.InnerText = "-";
        else
            PAN_NO.InnerText = txtPanNo.Text.ToString();

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
            NATURE_SERVICE.InnerText = "-";
        else
            NATURE_SERVICE.InnerText = txtNatureService.Text.ToString();

        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");

        int i = 0;

        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");





        HiddenField DeptId = GridData.Rows[i].FindControl("hdnDepartmentId") as HiddenField;
        HiddenField BudgetN = GridData.Rows[i].FindControl("hdnBudgetHeadId") as HiddenField;
        HiddenField CCid = GridData.Rows[i].FindControl("hdnCostCenterID") as HiddenField;

        DepartmentId.InnerText = DeptId.Value;
        BudgetNo.InnerText = BudgetN.Value;
        CC_ID.InnerText = CCid.Value;

        //ADDEDE BY VIJAY ON 26082020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        //IsIGSTApplicable.InnerText = ViewState["IsIGST"].ToString() == "Yes" ? "1" : "0";
        IsIGSTApplicable.InnerText = "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");
        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";

        //if (ViewState["IsIGST"].ToString() == "Yes")
        //{
        //    HiddenField hdnIGSTper = GridData.Rows[i].FindControl("hdnIGSTper") as HiddenField;
        //    HiddenField hdnIGSTAmountr = GridData.Rows[i].FindControl("hdnIGSTAMOUNT") as HiddenField;
        //    HiddenField hdnIGSTonAmount = GridData.Rows[i].FindControl("hdnIGSTONAMOUNT") as HiddenField;
        //    IGSTPER.InnerText = hdnIGSTper.Value;
        //    IGSTAMOUNT.InnerText = hdnIGSTAmountr.Value;
        //    IGSTonAmount.InnerText = hdnIGSTonAmount.Value;
        //}
        //else
        //{
        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";
        //}


        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";


        //if (ViewState["IsGST"].ToString() == "Yes")
        //{
        //    HiddenField hdnCGSTper = GridData.Rows[i].FindControl("hdnCGSTper") as HiddenField;
        //    HiddenField hdnCGSTAmountr = GridData.Rows[i].FindControl("hdnCGSTAMOUNT") as HiddenField;
        //    HiddenField hdnCGSTonAmount = GridData.Rows[i].FindControl("hdnCGSTONAMOUNT") as HiddenField;

        //    HiddenField hdnSGSTper = GridData.Rows[i].FindControl("hdnSGSTper") as HiddenField;
        //    HiddenField hdnSGSTAmountr = GridData.Rows[i].FindControl("hdnSGSTAMOUNT") as HiddenField;
        //    HiddenField hdnSGSTonAmount = GridData.Rows[i].FindControl("hdnSGSTONAMOUNT") as HiddenField;

        //    CGSTper.InnerText = hdnCGSTper.Value;
        //    CGSTamount.InnerText = hdnCGSTAmountr.Value;
        //    CGSTonamount.InnerText = hdnCGSTonAmount.Value;

        //    SGSTper.InnerText = hdnSGSTper.Value;
        //    SGSTamount.InnerText = hdnSGSTAmountr.Value;
        //    SGSTonamount.InnerText = hdnSGSTonAmount.Value;

        //    SGSTApplicable.InnerText = "1";
        //}
        //else
        //{
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";
        SGSTApplicable.InnerText = "0";
        //}


        if (ViewState["TDS"].ToString() == "Yes")
        {
           // opartystring = hdnIdEditParty.Value.ToString();
            HiddenField hdnsection = GridData.Rows[i].FindControl("hdnsection") as HiddenField;
            HiddenField hdnTDSamount = GridData.Rows[i].FindControl("hdnTDSamount") as HiddenField;
            HiddenField hdnTDSPersentage = GridData.Rows[i].FindControl("hdnTDSPersentage") as HiddenField;

            TDSSection.InnerText = hdnsection.Value;
            TDSAMOUNT.InnerText = hdnTDSamount.Value;
            TDPersentage.InnerText = hdnTDSPersentage.Value;

            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                // CHANGED BY DANISH 
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                //added by vijay andoju 07-07-2020

            }
            else
            {
                string narration = txtNarration.Text.Trim().Replace("'", "''");
                if (txtNarration.Text != string.Empty)
                {
                    PARTICULARS.InnerText = narration;
                }
                else
                {
                    PARTICULARS.InnerText = "-";
                }
            }

        }
        else
        {
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";
            for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
            {
                HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                if (opartystring == string.Empty)
                { opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
                else
                {
                    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
                }
                //updated by naresh warbhe for showing naration in cash in hand for other ledger
                if (isPerNarration == "Y")
                {
                    PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                    //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                    // CHANGED BY DANISH 
                    //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();

                }
                else
                {
                    string narration = txtNarration.Text.Trim().Replace("'", "''");
                    if (txtNarration.Text != string.Empty)
                    {
                        PARTICULARS.InnerText = narration;
                    }
                    else
                    {
                        PARTICULARS.InnerText = "-";
                    }
                }

            }
        }

        //if (ViewState["TDSonGST"].ToString() == "Yes")
        //{
        //    opartystring = hdnIdEditParty.Value.ToString();
        //    HiddenField hdnTDSonGSTsection = GridData.Rows[i].FindControl("hdnTDSonGSTsection") as HiddenField;
        //    HiddenField hdnTDSGSTonAmount = GridData.Rows[i].FindControl("hdnTDSGSTonAmount") as HiddenField;
        //    HiddenField hdnTDSonGSTPersentage = GridData.Rows[i].FindControl("hdnTDSonGSTPersentage") as HiddenField;

        //    TDSonGSTSection.InnerText = hdnTDSonGSTsection.Value;
        //    TDSGSTonAMOUNT.InnerText = hdnTDSGSTonAmount.Value;
        //    TDSonGSTPersentage.InnerText = hdnTDSonGSTPersentage.Value;

        //    if (isPerNarration == "Y")
        //    {
        //        PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
        //        //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
        //        // CHANGED BY DANISH 
        //        //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
        //        //added by vijay andoju 07-07-2020

        //    }
        //    else
        //    {
        //        string narration = txtNarration.Text.Trim().Replace("'", "''");
        //        if (txtNarration.Text != string.Empty)
        //        {
        //            PARTICULARS.InnerText = narration;
        //        }
        //        else
        //        {
        //            PARTICULARS.InnerText = "-";
        //        }
        //    }

        //}
        //else
        //{
        TDSonGSTSection.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTPersentage.InnerText = "0";
        for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
        {
            HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
            if (opartystring == string.Empty)
            { opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
            else
            {
                opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
            }
            //updated by naresh warbhe for showing naration in cash in hand for other ledger
            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                // CHANGED BY DANISH 
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();

            }
            else
            {
                string narration = txtNarration.Text.Trim().Replace("'", "''");
                if (txtNarration.Text != string.Empty)
                {
                    PARTICULARS.InnerText = narration;
                }
                else
                {
                    PARTICULARS.InnerText = "-";
                }
            }

        }
        // }


        //for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
        //{
        //    HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
        //    if (opartystring == string.Empty)
        //    { opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
        //    else
        //    {
        //        opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
        //    }
        //}

        OPARTY.InnerText = opartystring.ToString().Trim();


        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = ddlTranType.SelectedValue.ToString().Trim();
        if (ddlTranType.SelectedValue.ToString().Trim() == "P" || ddlTranType.SelectedValue.ToString().Trim() == "J" || ddlTranType.SelectedValue.ToString().Trim() == "C")
        {
            TRAN.InnerText = "Cr";
        }
        else
        { TRAN.InnerText = "Dr"; }


        hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();

        PARTY_NO.InnerText = Convert.ToInt32(hdnAgainstPartyId.Value).ToString();

        //AMOUNT.InnerText = lblTotal.Text.ToString().Trim();

        AMOUNT.InnerText =txtCalculateAmt.Text.Trim();

        DEGREE_NO.InnerText = "0";
        if (isEdit == "Y")
        {
            VOUCHER_NO.InnerText = voucherno.ToString().Trim();
            STR_CB_VOUCHER_NO.InnerText = StrVno;
            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();

            }
        }
        else
        {
            if (isVoucherAuto == "N")
            {
                VOUCHER_NO.InnerText = voucherno;
            }
            else
            {
                VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());
            }
            //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
            STR_CB_VOUCHER_NO.InnerText = StrVno;
            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
        }

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";
        if (ViewState["CHQ_NO"] != null)
            CHQ_NO.InnerText = ViewState["CHQ_NO"].ToString();
        else
            CHQ_NO.InnerText = "0";

        if (ViewState["CHQ_DATE"] != null)
            CHQ_DATE.InnerText = Convert.ToDateTime(ViewState["CHQ_DATE"]).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        if (ViewState["TDS"].ToString() == "Yes")
        {
            IsTDSApllicalbe.InnerText = "1";
        }
        else
        {
            IsTDSApllicalbe.InnerText = "0";
        }


        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");
        //if (ViewState["TDSonGST"].ToString() == "Yes")
        //{
        //    IsTDSonGSTApplicable.InnerText = "1";
        //}
        //else
        //{
        IsTDSonGSTApplicable.InnerText = "0";
        //}

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

        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

        if (txtGSTNNO.Text == "" || txtGSTNNO.Text == string.Empty || txtGSTNNO.Text == null)
        {
            GSTIN_NO.InnerText = "-";
        }
        else
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";

        ProjectId.InnerText = ddlSponsor.SelectedValue;
        //added by tanu 29_12_2021
       // ProjectSubId.InnerText = "0";

        ProjectSubId.InnerText = ddlProjSubHead.SelectedValue;
       
        BILL_ID.InnerText = ddlBillNo.SelectedValue;

        XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
        PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

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
        objElement.AppendChild(IsTDSonGSTApplicable);
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

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);
        objElement.AppendChild(DepartmentId);


        //ADDED BY VIJAY ANDOJU ON 26020202
        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);
        // Added by vidisha on 24-09-2021
        objElement.AppendChild(PAYMENT_MODE);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        //--  dummy(objXMLDoc);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private XmlDocument dummy(XmlDocument objXMLDoc)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        //XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        int subtrno = Convert.ToInt32(Session["SUBTR_NO"].ToString()) + 1;
        SUBTR_NO.InnerText = "0";
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
        AmtWithoutGST.InnerText = "0";

        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        GSTPercent.InnerText = "0";

        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        IsGSTApplicable.InnerText = "0";

        XmlElement Section = objXMLDoc.CreateElement("SECTION");
        Section.InnerText = "0";

        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
            PARTY_NAME.InnerText = "-";
        else
            PARTY_NAME.InnerText = txtPartyName.Text.ToString();

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
            PAN_NO.InnerText = "-";
        else
            PAN_NO.InnerText = txtPanNo.Text.ToString();

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
            NATURE_SERVICE.InnerText = "-";
        else
            NATURE_SERVICE.InnerText = txtNatureService.Text.ToString();

        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");



        int i = 0;



        //ADDEDE BY VIJAY ON 26082020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = ViewState["IsIGST"].ToString() == "Yes" ? "1" : "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        if (ViewState["IsIGST"].ToString() == "Yes")
        {
            HiddenField hdnIGSTper = GridData.Rows[i].FindControl("hdnIGSTper") as HiddenField;
            HiddenField hdnIGSTAmountr = GridData.Rows[i].FindControl("hdnIGSTAMOUNT") as HiddenField;
            HiddenField hdnIGSTonAmount = GridData.Rows[i].FindControl("hdnIGSTONAMOUNT") as HiddenField;
            IGSTPER.InnerText = hdnIGSTper.Value;
            IGSTAMOUNT.InnerText = hdnIGSTAmountr.Value;
            IGSTonAmount.InnerText = hdnIGSTonAmount.Value;
        }
        else
        {
            IGSTPER.InnerText = "0";
            IGSTAMOUNT.InnerText = "0";
            IGSTonAmount.InnerText = "0";
        }


        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        if (ViewState["IsGST"].ToString() == "Yes")
        {
            HiddenField hdnCGSTper = GridData.Rows[i].FindControl("hdnCGSTper") as HiddenField;
            HiddenField hdnCGSTAmountr = GridData.Rows[i].FindControl("hdnCGSTAMOUNT") as HiddenField;
            HiddenField hdnCGSTonAmount = GridData.Rows[i].FindControl("hdnCGSTONAMOUNT") as HiddenField;

            HiddenField hdnSGSTper = GridData.Rows[i].FindControl("hdnSGSTper") as HiddenField;
            HiddenField hdnSGSTAmountr = GridData.Rows[i].FindControl("hdnSGSTAMOUNT") as HiddenField;
            HiddenField hdnSGSTonAmount = GridData.Rows[i].FindControl("hdnSGSTONAMOUNT") as HiddenField;

            CGSTper.InnerText = hdnCGSTper.Value;
            CGSTamount.InnerText = hdnCGSTAmountr.Value;
            CGSTonamount.InnerText = hdnCGSTonAmount.Value;

            SGSTper.InnerText = hdnSGSTper.Value;
            SGSTamount.InnerText = hdnSGSTAmountr.Value;
            SGSTonamount.InnerText = hdnSGSTonAmount.Value;

            SGSTApplicable.InnerText = "1";
        }
        else
        {
            CGSTper.InnerText = "0";
            CGSTamount.InnerText = "0";
            CGSTonamount.InnerText = "0";

            SGSTper.InnerText = "0";
            SGSTamount.InnerText = "0";
            SGSTonamount.InnerText = "0";
            SGSTApplicable.InnerText = "0";
        }


        if (ViewState["TDS"].ToString() == "Yes")
        {
            opartystring = hdnIdEditParty.Value.ToString();
            HiddenField hdnsection = GridData.Rows[i].FindControl("hdnsection") as HiddenField;
            HiddenField hdnTDSamount = GridData.Rows[i].FindControl("hdnTDSamount") as HiddenField;
            HiddenField hdnTDSPersentage = GridData.Rows[i].FindControl("hdnTDSPersentage") as HiddenField;

            TDSSection.InnerText = hdnsection.Value;
            TDSAMOUNT.InnerText = hdnTDSamount.Value;
            TDPersentage.InnerText = hdnTDSPersentage.Value;

            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                // CHANGED BY DANISH 
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                //added by vijay andoju 07-07-2020

            }
            else
            {
                string narration = txtNarration.Text.Trim().Replace("'", "''");
                if (txtNarration.Text != string.Empty)
                {
                    PARTICULARS.InnerText = narration;
                }
                else
                {
                    PARTICULARS.InnerText = "-";
                }
            }

        }
        else
        {
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = "0";
            TDPersentage.InnerText = "0";
            for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
            {
                HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;


                if (opartystring == string.Empty)
                { opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
                else
                {
                    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
                }
                //updated by naresh warbhe for showing naration in cash in hand for other ledger
                if (isPerNarration == "Y")
                {
                    PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                    //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                    // CHANGED BY DANISH 
                    //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();

                }
                else
                {
                    string narration = txtNarration.Text.Trim().Replace("'", "''");
                    if (txtNarration.Text != string.Empty)
                    {
                        PARTICULARS.InnerText = narration;
                    }
                    else
                    {
                        PARTICULARS.InnerText = "-";
                    }
                }

            }
        }

        OPARTY.InnerText = opartystring.ToString().Trim();


        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = ddlTranType.SelectedValue.ToString().Trim();
        if (ddlTranType.SelectedValue.ToString().Trim() == "P" || ddlTranType.SelectedValue.ToString().Trim() == "J" || ddlTranType.SelectedValue.ToString().Trim() == "C")
        {
            TRAN.InnerText = "Cr";
        }
        else
        { TRAN.InnerText = "Dr"; }


        hdnAgainstPartyId.Value = hdnPartyManual.Value.ToString();

        PARTY_NO.InnerText = Convert.ToInt32(hdnAgainstPartyId.Value).ToString();
        AMOUNT.InnerText = lblTotal.Text.ToString().Trim();
        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = "0";
        STR_CB_VOUCHER_NO.InnerText = "0";


        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";
        if (ViewState["CHQ_NO"] != null)
            CHQ_NO.InnerText = ViewState["CHQ_NO"].ToString();
        else
            CHQ_NO.InnerText = "0";

        if (ViewState["CHQ_DATE"] != null)
            CHQ_DATE.InnerText = Convert.ToDateTime(ViewState["CHQ_DATE"]).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");

        DepartmentId.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        if (ViewState["TDS"].ToString() == "Yes")
        {
            IsTDSApllicalbe.InnerText = "1";
        }
        else
        {
            IsTDSApllicalbe.InnerText = "0";
        }
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

        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

        if (txtGSTNNO.Text == "" || txtGSTNNO.Text == string.Empty || txtGSTNNO.Text == null)
        {
            GSTIN_NO.InnerText = "-";
        }
        else
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }

        ProjectId.InnerText = ddlSponsor.SelectedValue;
        //addedd by tanu 29_12_2021
        ProjectSubId.InnerText = "0";
        
        BILL_ID.InnerText = ddlBillNo.SelectedValue;

        XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
        PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

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

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(DepartmentId);


        //ADDED BY VIJAY ANDOJU ON 26020202
        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objElement.AppendChild(PAYMENT_MODE);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
        // WriteXML(objXMLDoc);
    }

    private void WriteXML(XmlDocument objDoc)
    {

        objDoc.Save(Server.MapPath("~/ArrangeData.xml"));

    }

    private XmlDocument ReadXML(string isConsolidate)
    {
        XmlDocument xmlDoc = new XmlDocument();
        //string path = "D:\\Account\\VSS_ACC_Finance\\UAIMS\\PresentationLayer\\ACCOUNT\\ArrangeData.xml";
        //xmlDoc.Load(path);

        xmlDoc.Load(Server.MapPath("~/ArrangeData.xml"));
        if (isConsolidate == "N")
        {
            xmlDoc.DocumentElement.RemoveAll();
            string xml = xmlDoc.ToString();
        }
        return xmlDoc;
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AccountingVouchers.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AccountingVouchers.aspx");
        }
    }


    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ACCOUNT/ShowTransaction.aspx");
    }

    private void SetOppositPartyStringForJournal()
    {
        DataTable dtbind = Session["Datatable"] as DataTable;
        string opartystring = string.Empty;

        if (dtbind.Rows.Count != 0)
        {
            string Mode = string.Empty;
            string CrParty = string.Empty;
            string DrParty = string.Empty;

            for (int i = 0; i < dtbind.Rows.Count; i++)
            {
                if (CrParty == "")
                {
                    if (dtbind.Rows[i]["Mode"].ToString().Trim() == "Cr")
                    {
                        CrParty = dtbind.Rows[i]["Id"].ToString().Trim();
                    }
                }
                if (DrParty == "")
                {
                    if (dtbind.Rows[i]["Mode"].ToString().Trim() == "Dr")
                    {
                        DrParty = dtbind.Rows[i]["Id"].ToString().Trim();
                    }
                }
            }

            for (int i = 0; i < dtbind.Rows.Count; i++)
            {
                if (dtbind.Rows[i]["Mode"].ToString().Trim() == "Cr")
                {
                    dtbind.Rows[i]["OppParty"] = DrParty;
                }

                if (dtbind.Rows[i]["Mode"].ToString().Trim() == "Dr")
                {
                    dtbind.Rows[i]["OppParty"] = CrParty;
                }

                dtbind.Rows[i].AcceptChanges();
            }
        }

        Session["Datatable"] = dtbind;
        GridData.DataSource = dtbind;
        GridData.DataBind();

    }

    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }

    private void ConsolidateTransactionEntry(string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        XmlDocument objXMLDoc = ReadXML("Y");
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "1";
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

        int i = 0;
        for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
        {
            HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;

            if (opartystring == string.Empty)
            { opartystring = Convert.ToString(hdnparty.Value).Trim(); }
            else
            {
                opartystring = opartystring + "," + Convert.ToString(hdnparty.Value).Trim();
            }
            //updated by naresh warbhe for showing naration in cash in hand for other ledger
            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].ToolTip.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
                // CHANGED BY DANISH 
                //objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();

            }
            else
            {
                string narration = txtNarration.Text.Trim().Replace("'", "''");
                if (txtNarration.Text != string.Empty)
                {
                    PARTICULARS.InnerText = narration;
                }
                else
                {
                    PARTICULARS.InnerText = "-";
                }
            }

        }
        OPARTY.InnerText = opartystring.ToString().Trim();


        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = ddlTranType.SelectedValue.ToString().Trim();
        if (ddlTranType.SelectedValue.ToString().Trim() == "P" || ddlTranType.SelectedValue.ToString().Trim() == "J" || ddlTranType.SelectedValue.ToString().Trim() == "C")
        {
            TRAN.InnerText = "Cr";
        }
        else
        { TRAN.InnerText = "Dr"; }


        hdnAgainstPartyId.Value = txtAgainstAcc.Text.ToString().Trim().Split('*')[0].ToString();

        PARTY_NO.InnerText = hdnAgainstPartyId.Value.ToString();
        AMOUNT.InnerText = lblTotal.Text.ToString().Trim();
        DEGREE_NO.InnerText = "0";
        if (isEdit == "Y")
        {
            VOUCHER_NO.InnerText = voucherno.ToString().Trim();
            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();

            }
        }
        else
        {
            if (isVoucherAuto == "N")
            {
                VOUCHER_NO.InnerText = voucherno;
            }
            else
            {
                VOUCHER_NO.InnerText = voucherno;// Convert.ToInt16(txtVoucherNo.Text.ToString().Trim());
            }
            //objPC.STR_CB_VOUCHER_NO = lblVoucherNo.Text.Trim();
            STR_CB_VOUCHER_NO.InnerText = StrVno;
            if (ddlTranType.SelectedItem.Text.Trim() == "Payment")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Contra")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/C" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else if (ddlTranType.SelectedItem.Text.Trim() == "Journal")
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/J" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
            else
            {
                STR_VOUCHER_NO.InnerText = Session["comp_code"].ToString().Trim() + "/R" + voucherno.ToString();//txtVoucherNo.Text.ToString().Trim();
            }
        }

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "H";
        CBTYPE.InnerText = "TF";
        RECIEPT_PAYMENT_FEES.InnerText = "P";
        REC_NO.InnerText = "0";
        //objPC.CHQ_NO = "0";
        if (ViewState["CHQ_NO"] != null)
            CHQ_NO.InnerText = ViewState["CHQ_NO"].ToString();
        else
            CHQ_NO.InnerText = "0";

        if (ViewState["CHQ_DATE"] != null)
            CHQ_DATE.InnerText = Convert.ToDateTime(ViewState["CHQ_DATE"]).ToString("dd-MMM-yyyy");


        //objPC.CHQ_DATE ;
        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";
        // objPC.RECON_DATE = "";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";
        XmlElement IDF_NO = objXMLDoc.CreateElement("IDF_NO");
        IDF_NO.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";
        //if (isPerNarration == "Y")
        //{
        //    objPC.PARTICULARS = GridData.Rows[i].Cells[3].Text.ToString().Trim();//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
        //}
        //else { objPC.PARTICULARS = ""; }
        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");

        XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
        PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

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
        objElement.AppendChild(IDF_NO);
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

        objElement.AppendChild(PAYMENT_MODE);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        WriteXML(objXMLDoc);

    }

    //protected void txtAcc_TextChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    Session["AgainstAcc"] = "N";
    //    ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "PARTY_NO > 0 and PARTY_NAME like '%" + Convert.ToString(txtAcc.Text).Trim().ToUpper() + "%' ", "PARTY_NO");// "PARTY_NAME");//and PAYMENT_TYPE_NO IN ('1','2')
    //    try
    //    {
    //        lstLedgerName.DataSource = ds.Tables[0];
    //        lstLedgerName.DataBind();
    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lstLedgerName.Items.Clear();
    //                lstLedgerName.DataTextField = "PARTY_NAME";
    //                lstLedgerName.DataValueField = "PARTY_NO";
    //                lstLedgerName.DataSource = ds.Tables[0];
    //                lstLedgerName.DataBind();
    //            }
    //            else
    //            {
    //                objCommon.DisplayUserMessage(UPDLedger, "Ledger Does Not Exist.", this);
    //                txtAcc.Focus();
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayUserMessage(UPDLedger, "Ledger Does Not Exist.", this);
    //            txtAcc.Focus();
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Account_VoucherCreations.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    upd_ModalPopupExtender.Show();
    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int voucherNo = 0;
            if (isVoucherAuto == "Y")
                voucherNo = Convert.ToInt16(txtVoucherNo.Text) - 1;
            else
                voucherNo = Convert.ToInt16(txtVoucherNo.Text);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CODE_YEAR=" + Session["comp_code"].ToString() + "_" + Session["fin_yr"].ToString().Trim() + "," + "@P_VCH_NO=" + voucherNo.ToString().Trim() + "," + "@USER=" + Session["userfullname"].ToString();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchers.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    protected void btnchequePrint_Click(object sender, EventArgs e)
    {
        if (ViewState["PaymentType"].ToString() == "P" && ViewState["PaymentTypeNo"].ToString() == "2")
        {
            upd_ModalPopupExtender1.Show();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsVoucherDetails = (DataSet)Session["VoucherDetail"];
            //if (ViewState["isFirst"] == null || ViewState["isFirst"].ToString() == "Y")
            //{
            VoucherNoSetting();
            //SetVoucherNo();
            string voucherNo = "0";
            if (ViewState["isModi"] != null)
            {
                if (isVoucherAuto == "Y")
                {
                    if (ViewState["isModi"].ToString().Trim() == "Y")
                    {
                        voucherNo = ViewState["VNO"].ToString();
                    }
                    else
                    {
                        voucherNo = ViewState["VNO"].ToString();
                    }

                    //voucherNo = (Convert.ToInt16(txtVoucherNo.Text) - 1).ToString().Trim();
                }
                else
                {
                    if (ViewState["isModi"].ToString().Trim() == "Y")
                    {
                        voucherNo = ViewState["VNO"].ToString();
                    }
                    else
                    {
                        voucherNo = ViewState["VNO"].ToString();
                    }
                }
            }
            else
            {
                if (isVoucherAuto == "Y")
                {
                    voucherNo = ViewState["VNO"].ToString();
                }
                else
                {
                    voucherNo = ViewState["VNO"].ToString();
                }
            }
            ViewState["isModi"] = "N";


            isFourSign = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
            isBankCash = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

            if (isFourSign == "N")
            {
                if (ViewState["Voucher"].ToString().Trim() == "Payment" || ViewState["Voucher"].ToString().Trim() == "Receipt")
                {
                    if (isTempVoucher == "Y")
                    {
                        //orignal                               
                        //ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRptMakaut.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                        //added by tanu 24/01/2022
                        ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherPending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                    }
                    else
                    {

                        ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                    }

                }
                else if (isTempVoucher == "Y")
                {
                    //orignal
                    // ShowVoucherPrintReport("Voucher", "JvContraVoucherReportMakaut.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                    //added by tanu 24/01/2022
                    ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Pending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                }
                else
                {
                    ShowVoucherPrintReport("Voucher", "JvContraVoucherReport.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                }

            }
            else if (isFourSign == "Y")
            {
                if (ViewState["Voucher"].ToString().Trim() == "Payment" || ViewState["Voucher"].ToString().Trim() == "Receipt")
                {
                    //orignal
                    // ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                    //added by tanu 24/01/2022
                    ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2_Pending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                }
                else
                {
                    //orignal
                    // ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Format2.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                    //added by tanu 24/01/2022
                    ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Format2_Pending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                }
            }

            if (ViewState["PaymentType"].ToString() == "P" && ViewState["PaymentTypeNo"].ToString() == "2")
            {
                upd_ModalPopupExtender1.Show();
                //btnPrint.Text = "Print Cheque";
                //ViewState["isFirst"] = "N";
                //hdnBack.Value = "1";
            }
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "AskCheque()", true);
            //}
            //else
            //{


            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "AskCheque()", true);
            //    if (hdnBack.Value == "1")
            //    {
            //        string Script = string.Empty;
            //        string url = string.Empty;
            //        Script = "ShowChequePrinting('" + ViewState["TRANDATE"].ToString() + "','" + ViewState["VoucherNo"].ToString() + "','" + ViewState["OPartyNo"].ToString() + "','" + ViewState["Amount"].ToString() + "','" + ViewState["PartyNo"].ToString() + "','" + ViewState["CHQ_NO"].ToString() + "')";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", Script, true);
            //        ViewState["isFirst"] = "Y";
            //        btnPrint.Text = "Print Voucher";
            //    }
            //}
            //// ShowReport("Transaction", "TransDetail.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchers.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        lvGrp.DataSource = null;
        lvGrp.DataBind();
        ViewState["isModi"] = "N";
        ViewState["isFirst"] = "Y";
        btnPrint.Text = "Print Voucher";
        txtDate.Text = DateTime.Now.ToString();
        if (hdnBack.Value == "1")
        {
            string Script = string.Empty;
            string url = string.Empty;
            //lnkledger.Attributes.Add("onClick", "return ShowChequePrinting('" + dtContain.Rows[c]["Date"].ToString().Trim() + "','" + dtContain.Rows[c]["VchNo"].ToString().Trim() + "','" + party + "','" + dtContain.Rows[c]["Credit"].ToString().Trim() + "','" + hdnparty.Value.ToString().Trim() + "');");

            Script = "ShowChequePrinting('" + ViewState["TRANDATE"].ToString().Trim() + "','" + ViewState["VoucherNo"].ToString() + "','" + ViewState["OPartyNo"].ToString() + "','" + ViewState["Amount"].ToString() + "','" + ViewState["PartyNo"].ToString() + "','" + ViewState["CHQ_NO"].ToString() + "')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", Script, true);
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }

    }

    //protected void lnkLedger_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ledgerhead.aspx?obj=AccountingVouchers");
    //}

    private string SetDetails(string Ledger)
    {
        try
        {
            string id;
            string[] id1 = Ledger.Split('*');
            if (id1.Length == 2)
            {
                if (id1[1].ToString().Trim() != "")
                {
                    id = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + id1[1] + "'"); ;
                }
                else
                {
                    return "Invalid Ledger";
                }
            }
            else
            {
                return "Invalid Ledger";
            }

            if (id != "" | id != string.Empty)
            {
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();
                hdnIdEditParty.Value = id.ToString();

                PartyController objPC = new PartyController();
                string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

                if (IsNumeric(id) == false)
                {
                    return "Invalid Ledger";
                }
                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(id), code_year);
                if (dtr.HasRows == false)
                {
                    return "Invalid Ledger";
                }
                if (dtr.Read())
                {
                    DataTable dtBalance1 = Session["Datatable"] as DataTable;
                    int count = 0;
                    for (int i = 0; i < dtBalance1.Rows.Count; i++)
                    {
                        if (dtBalance1.Rows[i]["Id"].ToString() == id)
                        {
                            count++;
                        }
                    }


                    if (isSingleMode == "N")
                    {
                        // lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                        lblCurBal2.Text = hdnbal2.Value;

                        if (lblCurBal2.Text.ToString().Trim() != "")
                        {
                            if (count == 0)
                            {
                                ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                            }
                        }
                        else
                        {
                            if (count == 0)
                            {
                                lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                                ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                            }
                        }
                        if (Convert.ToDouble(lblCurBal2.Text) > 0)
                        {
                            ViewState["Tran2"] = "Cr";
                        }
                        else
                        {
                            ViewState["Tran2"] = "Dr";
                        }
                        lblCrDr2.Text = ViewState["Tran2"].ToString().Trim();

                        txtAcc.Focus();
                    }

                    if (Session["AgainstAcc"] == "N")
                    {
                        //txtAcc.Text = dtr["PARTY_NAME"].ToString();
                        //lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                        lblCurBal2.Text = hdnbal2.Value;
                        if (lblCurBal2.Text.ToString().Trim() != "")
                        {
                            if (count == 0)
                            {
                                ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                            }
                        }
                        else
                        {
                            if (count == 0)
                            {
                                lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                                ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                            }
                        }
                        if (Convert.ToDouble(lblCurBal2.Text) > 0)//oldk
                        {
                            ViewState["Tran2"] = "Cr";
                        }
                        else
                        {
                            ViewState["Tran2"] = "Dr";
                        }
                        lblCrDr2.Text = ViewState["Tran2"].ToString().Trim();
                        txtAcc.Focus();
                    }
                    else
                    {
                        isAllreadySet = "Y";
                        hdnAgParty.Value = id.ToString();
                        hdnAgainstPartyId.Value = hdnIdEditParty.Value.ToString().Trim();
                        // txtAgainstAcc.Text = dtr["PARTY_NAME"].ToString();
                        lblCurbal1.Text = dtr["BALANCE"].ToString().Trim();
                        //if (Convert.ToDouble(lblCurbal1.Text) < 0)
                        //    lblCurbal1.Text =Convert.ToString( -Convert.ToDouble(lblCurbal1.Text));
                        if (Convert.ToDouble(lblCurbal1.Text) > 0)
                        {
                            ViewState["Tran1"] = "Cr";

                        }
                        else
                        {
                            ViewState["Tran1"] = "Dr";

                        }

                        lblCrDr1.Text = ViewState["Tran1"].ToString().Trim();
                        txtAgainstAcc.Focus();
                    }
                    btnAdd.Enabled = true;
                }
                dtr.Close();
            }
            else
            {
                ViewState["action"] = "add";
                ViewState["id"] = null;
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "AccountingVouchers.SetDetails-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return "";
    }
    //[WebMethod]
    //public static string DateText_Change()
    //{
    //    DateTime date1; Common objCommon = new Common();
    //    if (!(DateTime.TryParse(txtDate.Text, out date1)))
    //    {
    //        //objCommon.DisplayUserMessage(UPDLedger, "Invalid Date Is Entered. ", this);
    //        //txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    //        //txtDate.Focus();
    //        return;
    //    }

    //    if (DateTime.Compare(Convert.ToDateTime(txtDate.Text), DateTime.Now.Date) == 1)
    //    {
    //        objCommon.DisplayUserMessage(UPDLedger, "Can Not Make Future Entry. ", this);
    //        txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    //        txtDate.Focus();
    //        return;
    //    }

    //    FinanceCashBookController objCBC = new FinanceCashBookController();
    //    DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
    //    if (dtr.Read())
    //    {
    //        Session["comp_code"] = dtr["COMPANY_CODE"];
    //        Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
    //        Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
    //        Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
    //    }
    //    dtr.Close();

    //    if (DateTime.Compare(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
    //    {
    //        objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
    //        txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    //        txtDate.Focus();
    //        return;
    //    }

    //    if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtDate.Text)) == 1)
    //    {
    //        objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
    //        txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    //        txtDate.Focus();
    //        return;
    //    }

    //    if (txtAgainstAcc.Text.Trim() != string.Empty)
    //        lblSpan.InnerText = StrVno;
    //}

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        DateTime date1;
        if (!(DateTime.TryParse(txtDate.Text, out date1)))
        {
            objCommon.DisplayUserMessage(UPDLedger, "Invalid Date Is Entered. ", this);
            //txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtDate.Text), DateTime.Now.Date) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Can Not Make Future Entry. ", this);
            txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDate.Focus();
            return;
        }


        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
        }
        dtr.Close();

        if (DateTime.Compare(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
            txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtDate.Text)) == 1)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
            txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDate.Focus();
            return;
        }

        if (txtAgainstAcc.Text.Trim() != string.Empty)
            lblSpan.InnerText = StrVno;

        //if (ViewState["isModi"] != null && ViewState["isModi"].ToString() != "Y")
        //{
        //    VoucherNoSetting();
        //}
        //else
        //{
        //    VoucherNoSetting();
        //}

        ViewState["Date"] = txtDate.Text;
        txtChequeDt2.Text = txtDate.Text;

    }

    protected void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ViewState["OrigData"] = e.Row.Cells[3].Text;
            GridViewRow row = e.Row;
            HiddenField hdnDld = row.FindControl("hdnDld") as HiddenField;
            //if (hdnDld.Value.ToString() == "5" || hdnDld.Value.ToString() == "1")
            //{
                e.Row.Cells[0].Enabled = true;
            //}
            //else
            //{
            //    e.Row.Cells[0].Enabled = false;
            //}

            if (e.Row.Cells[3].Text.Length >= 30) //Just change the value of 30 based on your requirements
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Trim();//.Substring(0, 30) + "...";
                e.Row.Cells[3].Attributes.Add("onmouseover", "return popUpToolTip('" + ViewState["OrigData"].ToString().Trim() + "');");
                e.Row.Cells[3].Attributes.Add("onmouseout", "return nd();");
                e.Row.Cells[3].ToolTip = ViewState["OrigData"].ToString();


            }
        }
    }
    //if (ViewState["TDS"].ToString() == "Yes")
    ////{
    //foreach (GridViewRow item in GridData.Rows)
    //{
    //    e.Row.Cells[0].Enabled = true;
    //    HiddenField hdnDld = item.FindControl("hdnDld") as HiddenField;

    //    if (GridData.Rows.Count == 0)
    //    {
    //        e.Row.Cells[0].Enabled = true;
    //    }
    //    else if (hdnDld.Value.ToString() == "5")
    //    {
    //        e.Row.Cells[0].Enabled = true;
    //    }
    //    else
    //    {
    //        e.Row.Cells[0].Enabled = false;
    //    }
    ////    HiddenField hdndelete=FindControl
    //}
    //}
    // }

    protected void ddlcrdr_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetWithoutCashBank();
        txtAcc.Focus();
    }

    protected void lnkLedger_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("ledgerhead.aspx?Name=Y");
        //  Response.Redirect("ledgerhead.aspx?""");
    }

    protected void txtAgainstAcc_TextChanged(object sender, EventArgs e)
    {
        //string[] balance = txtAgainstAcc.SelectedValue.ToString().Split(':');
        ////lblCur1.Text = balance[0];

        //string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAgainstAcc.Text.ToString().Trim().Split('*')[1] + "'");
        //PartyController objPC = new PartyController();
        //DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());
        //if (dtr.Read())
        //{
        //    if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
        //    {
        //        lblCurbal1.Text = dtr["BALANCE"].ToString().Trim();
        //        lblCrDr1.Text = "Dr";
        //    }
        //    else
        //    {
        //        lblCurbal1.Text = (-(Convert.ToDouble(dtr["BALANCE"].ToString().Trim()))).ToString();
        //        lblCrDr1.Text = "Cr";
        //    }
        //    //ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
        //}
    }

    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {

        string[] Party = txtAcc.Text.Trim().Split('*');
        if (Party.Length > 1)
        {
            hdnOpartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + Party[1] + "'");

            if (ddlTranType.SelectedValue.ToString() == "C")
            {
                string[] againstParty = txtAgainstAcc.Text.Trim().Split('*');
                if (againstParty.Length > 1)
                {
                    if (Party[1] == againstParty[1])
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Against party and party can not be same", this.Page);
                        txtAcc.Text = string.Empty;
                        return;
                    }
                }
            }

            int ISAPPLICABLE = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "isnull(ISCCApplicable,0) ISCCApplicable", "Party_No=" + hdnOpartyManual.Value));
            if (ISAPPLICABLE == 1)
            {
                trCostCenter.Visible = true;
                objCommon.FillDropDownList(ddlCostCenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
            }
            else
            {
                trCostCenter.Visible = false;
            }

            if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "R")
            {
                int IsBudgetHead = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISBudgetHead as int),0) ISBudgetHead", "Party_No=" + hdnOpartyManual.Value));
                if (IsBudgetHead == 1)
                {
                    ViewState["IsBudgetHead"] = "Yes";
                    trBudgetHead.Visible = true;
                    divDepartment.Visible = true;
                    //trBudgetHead.Style.Add("display","block");
                    //  objCommon.FillDropDownList(ddlBudgetHead, "ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");

                    //ADDED BY VIJAY ANDOJU ON
                    objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
                    objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

                }
                else
                {
                    ViewState["IsBudgetHead"] = "No";
                    trBudgetHead.Visible = false;
                    divDepartment.Visible = false;
                    // trBudgetHead.Style.Add("display", "none");
                }
            }
            else
            {
                ViewState["IsBudgetHead"] = "No";
            }
            string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1] + "'");
            PartyController objPC = new PartyController();
            DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());
            if (dtr.Read())
            {

                if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
                {
                    lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                    lblCur2.Text = "Dr";
                    txtCurBal.Text = dtr["BALANCE"].ToString().Trim();
                    txtCurBal.Visible = true;
                    lblCurBal2.Visible = false;
                    txtCurBal.Enabled = false;
                }
                else
                {
                    lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                    lblCur2.Text = "Cr";
                    txtCurBal.Text = dtr["BALANCE"].ToString().Trim();
                    txtCurBal.Visible = true;
                    lblCurBal2.Visible = false;
                    txtCurBal.Enabled = false;
                }
                //ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
            }

            // txtTranAmt.Focus();
        }
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string GetLedgerBalance(string PartyNO, string compcode)
    {
        Common objCommon = new Common();
        decimal AMOUNT = 0;
        if (PartyNO == "")
        { }
        else
            AMOUNT = Convert.ToDecimal(objCommon.LookUp("ACC_" + compcode + "_PARTY", "BALANCE", "' and CHQ_NO=" + PartyNO + ""));
        if (AMOUNT > 0)
        {
            return AMOUNT.ToString() + " Dr";
        }
        else
            return AMOUNT.ToString() + " Cr";
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string CheckDuplicateChequeNo(string ChequeNo, string compcode)
    {
        Common objCommon = new Common();
        int count = 0;
        if (ChequeNo == "")
        { }
        else
            count = Convert.ToInt32(objCommon.LookUp("ACC_" + compcode + "_TRANS", "COUNT(*)", "TRANSACTION_TYPE<>'OB' and CHQ_NO='" + ChequeNo.ToString() + "'"));
        if (count > 0)
            return "Available";
        else
            return "Not Available";
    }

    protected void txtVoucherNo_TextChanged(object sender, EventArgs e)
    {
        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "N")
        {
            if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER NO SEPRATE FOR RCPT,PAY,CONT,JOUN'") == "Y")
            {
                if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "count(*)", "VOUCHER_NO='" + txtVoucherNo.Text.Trim() + "' and TRANSACTION_TYPE='" + ddlTranType.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN convert(datetime,'" + Session["fin_date_from"].ToString() + "',103) AND convert(datetime,'" + Session["fin_date_to"] + "',103)")) > 0)
                {
                    txtVoucherNo.Text = string.Empty;
                    objCommon.DisplayUserMessage(UPDLedger, "Voucher No is already exist", this.Page);
                }
            }
            else
            {
                if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "count(*)", "VOUCHER_NO='" + txtVoucherNo.Text.Trim() + "' AND TRANSACTION_DATE BETWEEN convert(datetime,'" + Session["fin_date_from"].ToString() + "',103) AND convert(datetime,'" + Session["fin_date_to"] + "',103)")) > 0)
                {
                    txtVoucherNo.Text = string.Empty;
                    objCommon.DisplayUserMessage(UPDLedger, "Voucher No is already exist", this.Page);
                }
            }
        }
    }

    protected void ddlSponsor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + ddlSponsor.SelectedValue, "");
    }

    protected void ddlProjSubHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRemainAmt.Text = objPC1.GetSponsorProjectBalance(Convert.ToInt32(ddlSponsor.SelectedValue), Convert.ToInt32(ddlProjSubHead.SelectedValue), Session["comp_code"].ToString());
    }

    protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string TranMode = string.Empty;

        if (txtVoucherNo.Text == string.Empty)
        {
            ddlBillNo.SelectedValue = "0";
            objCommon.DisplayUserMessage(UPDLedger, "Please Enter Voucher Number", this.Page);
            return;
        }
        else
        {
            DataSet dsBillTran = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BILL_FILE_TRAN a inner join ACC_" + Session["comp_code"].ToString() + "_PARTY b on (a.PARTY_NO=b.PARTY_NO)", "ROW_NUMBER() over(order by BILL_TRAN_ID) SR_NO", "AMOUNT,a.PARTY_NO,b.ACC_CODE,b.PARTY_NAME as Particulars,a.CRDR as Mode, b.ACC_CODE,case when a.CRDR='Dr' then b.BALANCE+AMOUNT else b.BALANCE-AMOUNT end Balance", "BILL_ID='" + ddlBillNo.SelectedValue + "'", "");

            for (int i = 0; i < dsBillTran.Tables.Count; i++)
            {
                for (int j = 0; j < dsBillTran.Tables[i].Rows.Count; j++)
                {
                    if (!dt.Columns.Contains("Particulars"))
                        dt.Columns.Add("Particulars");

                    if (!dt.Columns.Contains("Narration"))
                        dt.Columns.Add("Narration");

                    if (!dt.Columns.Contains("Balance"))
                        dt.Columns.Add("Balance");

                    if (!dt.Columns.Contains("Debit"))
                        dt.Columns.Add("Debit");

                    if (!dt.Columns.Contains("Credit"))
                        dt.Columns.Add("Credit");

                    if (!dt.Columns.Contains("Amount"))
                        dt.Columns.Add("Amount");

                    if (!dt.Columns.Contains("ChqNo"))
                        dt.Columns.Add("ChqNo");

                    if (!dt.Columns.Contains("ChqDate"))
                        dt.Columns.Add("ChqDate");

                    if (!dt.Columns.Contains("ChqDate"))
                        dt.Columns.Add("ChqDate");

                    if (!dt.Columns.Contains("Mode"))
                        dt.Columns.Add("Mode");

                    if (!dt.Columns.Contains("Id"))
                        dt.Columns.Add("Id");

                    if (!dt.Columns.Contains("OppParty"))
                        dt.Columns.Add("OppParty");

                    if (!dt.Columns.Contains("CCID"))
                        dt.Columns.Add("CCID");

                    DataRow rowdt;
                    rowdt = dt.NewRow();

                    rowdt["Particulars"] = dsBillTran.Tables[i].Rows[j]["Particulars"].ToString() + "*" + dsBillTran.Tables[i].Rows[j]["ACC_CODE"].ToString();
                    rowdt["Balance"] = dsBillTran.Tables[i].Rows[j]["Balance"].ToString();
                    rowdt["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dsBillTran.Tables[i].Rows[j]["AMOUNT"].ToString())));
                    rowdt["Debit"] = 0.00;
                    rowdt["Credit"] = 0.00;
                    rowdt["Narration"] = "";
                    rowdt["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                    rowdt["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
                    rowdt["Mode"] = dsBillTran.Tables[i].Rows[j]["Mode"].ToString();
                    rowdt["Id"] = dsBillTran.Tables[i].Rows[j]["PARTY_NO"].ToString();
                    rowdt["OppParty"] = dsBillTran.Tables[i].Rows[j]["PARTY_NO"].ToString();
                    rowdt["CCID"] = 0;

                    dt.Rows.Add(rowdt);

                    Session["Datatable"] = dt;
                }
            }

            GridData.DataSource = dt;
            GridData.Columns[3].Visible = false;
            GridData.Columns[5].Visible = false;
            GridData.Columns[6].Visible = false;
            GridData.DataBind();
            AddTotalAmount();
            SetWithoutCashBank();
        }
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
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAgainstAccLedger(prefixText);
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
    public static List<string> GetAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAccountEntryCashBank(prefixText);
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
    //protected void ddlBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string Amount = string.Empty;
    //        DataSet ds = null;
    //        //ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD BH Left Join ACC_" + Session["comp_code"].ToString() + "_TRANS T ON BH.BUDG_NO = T.BudgetNo", "BH.BUDG_NO,BH.BUDG_NAME", "(ISNULL(BH.BUD_AMT,0) - ISNULL(SUM(Case when T.[TRAN] = 'Dr' Then T.AMOUNT When T.[TRAN] = 'Cr' Then -(T.AMOUNT) End),0)) As BUD_BAL_AMT", "BH.BUDG_NO =" + Convert.ToInt32(ddlBudgetHead.SelectedValue) + " Group By BH.BUDG_NO,BH.BUDG_NAME,BH.BUD_AMT", "");
    //        if (ddlcrdr.SelectedValue == "Cr")
    //        {
    //            Amount = "-" + txtTranAmt.Text;
    //        }
    //        else
    //        {
    //            Amount = txtTranAmt.Text;
    //        }


    //        ds = objPC1.GetBudegetBalanceNEW(ddldepartment.SelectedValue, ddlBudgetHead.SelectedValue, Session["comp_code"].ToString(), Amount);
    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lblBudgetBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["Balance"].ToString()));
    //            }
    //            else
    //            {
    //                lblBudgetBal.Text = "0.00";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void ddlBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string Amount = string.Empty;
            DataSet ds = null;
            ds = objPC1.GetBudegetBalanceNEW(Convert.ToInt32(ddldepartment.SelectedValue), Convert.ToInt32(ddlBudgetHead.SelectedValue), DateTime.Today);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblBudgetBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["BALANCE"].ToString()));
                }
                else
                {
                    lblBudgetBal.Text = "0.00";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtAgainstAcc_TextChanged1(object sender, EventArgs e)
    {
        int partyId = Convert.ToInt32(txtAgainstAcc.Text.ToString().Split('*')[1].ToString());

        double PartyAgainstBal = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_Party", "BALANCE", "Party_No=" + partyId));
        lblCurbal1.Text = String.Format("{0:0.00}", Convert.ToDouble(PartyAgainstBal.ToString()));
        txtAcc.Focus();
        TextBox1.Text = String.Format("{0:0.00}", Convert.ToDouble(PartyAgainstBal.ToString()));
    }
    protected void btnNoDel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        lblconfirm.Text = "NO";
        return;
    }
    protected void btnOkDel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        lblconfirm.Text = "YES";
        return;
    }
    protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTDSApplicable.Checked == true)
        {
            dvTDS.Visible = true;
            txtTDSLedger.Focus();
            //  txtTamount.Text = "0.00";// Convert.ToDouble(txtTranAmt.Text).ToString();
            objCommon.FillDropDownList(ddlSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
            ViewState["TDS"] = "Yes";

        }
        else
        {
            dvTDS.Visible = false;
            txtTDSLedger.Text = string.Empty;
            // txtTDSPer.Text = string.Empty;
            // txtTDSAmount.Text = string.Empty;
            btnAdd.Focus();
            ViewState["TDS"] = "No";
            Session["Datatable"] = null;
            SetDataColumn();
        }
    }

    private void AddTDSGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkTDSApplicable.Checked == false)
            {
                dt = Session["Datatable"] as DataTable;

                if (dt.Rows.Count < 1)
                {
                    //RowIndex = -1;
                    ViewState["RowIndex"] = -1;
                }

                string TranMode = string.Empty;

                btnAdd.Enabled = true;
                //lblMsg.Text = "";
                //rowMsg.Style["Display"] = "none";
                DataRow row;
                row = dt.NewRow();
                row["Particulars"] = txtAcc.Text.ToString().Trim();
                row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }

                //if (Convert.ToDouble(txtTranAmt.Text) < 0)
                //{
                //if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                //{
                //    TranMode = "Cr";
                //}
                //else
                //{
                //    TranMode = "Dr";
                //}
                // }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1] + "'");
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    PartyController objPC = new PartyController();
                    DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

                    if (dtr.Read())
                    {
                        lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                        ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                    }

                    string balance = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Id"].ToString() == partyNo)
                        {

                            if (dt.Rows[i]["Mode"].ToString() == "Dr")
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["Balance2"] = balance;
                            }
                            else
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["Balance2"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["Balance2"] = balance;
                            }

                        }
                    }
                }

                if (TranMode.ToString().Trim() == "Dr")
                {
                    if (ViewState["Balance2"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()));// + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance2"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()));//- Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        txtAgainstAcc.Focus();
                        return;
                    }
                }

                if (isSingleMode == "Y")
                {
                    GridData.Columns[4].Visible = true;
                    GridData.Columns[5].Visible = false;
                    GridData.Columns[6].Visible = false;
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    row["Amount"] = 0.00;//String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                    }

                    row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtTDSLedger.Text.Trim().ToString().Split('*')[1]).ToString();
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                    row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                    row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
                }

                if (trCostCenter.Visible == true)
                {
                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (trBudgetHead.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    row["Departmentid"] = "0";
                }

                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                row["Section"] = 0;
                row["TDSAMOUNT"] = 0;
                row["TDSsection"] = ddlSection.SelectedValue;
                //  row["TDSpercentage"] = txtTDSPer.Text;



                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";



                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
            else if (j == 1)
            {
                dt = Session["Datatable"] as DataTable;

                if (dt.Rows.Count < 1)
                {
                    //RowIndex = -1;
                    ViewState["RowIndex"] = -1;
                }

                string TranMode = string.Empty;

                btnAdd.Enabled = true;
                //lblMsg.Text = "";
                //rowMsg.Style["Display"] = "none";
                DataRow row;
                row = dt.NewRow();
                row["Particulars"] = txtTDSLedger.Text.ToString().Trim();
                row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    //TranMode = "Cr";
                    // Added by vijay andoju 07-07-2020
                    TranMode = "Dr";

                }
                else
                {
                    //TranMode = "Dr";
                    //Added by vijay andoju 07-07-2020
                    TranMode = "Cr";
                }

                //if (Convert.ToDouble(txtTranAmt.Text) < 0)
                //{
                //if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                //{
                //    TranMode = "Dr";
                //}
                //else
                //{
                //    TranMode = "Cr";
                //}
                //}
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTDSLedger.Text.ToString().Trim().Split('*')[1] + "'");
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {
                        count++;
                    }
                }
                PartyController objPC = new PartyController();
                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    string tdsAmt = dtr["BALANCE"].ToString().Trim();
                    ViewState["Balance3"] = tdsAmt.ToString();// dtr["BALANCE"].ToString().Trim();
                }
                if (count > 0)
                {
                    string balance = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Id"].ToString() == partyNo)
                        {

                            if (dt.Rows[i]["Mode"].ToString() == "Dr")
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["Balance3"] = balance;
                            }
                            else
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["Balance3"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["Balance3"] = balance;
                            }

                        }
                    }
                }

                if (TranMode.ToString().Trim() == "Dr")
                {
                    if (ViewState["Balance3"].ToString().Trim() != "")
                    {
                        row["Balance"] = 0.00; //(Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance3"].ToString().Trim() != "")
                    {
                        row["Balance"] = 0.00;// (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        txtAgainstAcc.Focus();
                        return;
                    }
                }

                if (isSingleMode == "Y")
                {
                    GridData.Columns[4].Visible = true;
                    GridData.Columns[5].Visible = false;
                    GridData.Columns[6].Visible = false;
                    row["Debit"] = "0.00";
                    row["Credit"] = "0.00";
                    //  row["Amount"] = 0.00;// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = "0.00"; //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = "0.00";// String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                    }

                    //   row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtAcc.Text.Trim().ToString().Split('*')[1]).ToString();
                if (isPerNarration == "Y")
                {
                    row["ChqNo"] = txtChqNo1.Text.ToString().Trim();
                    row["ChqDate"] = txtChequeDt1.Text.ToString().Trim();
                }
                else
                {
                    row["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                    row["ChqDate"] = txtChequeDt2.Text.ToString().Trim();
                }

                if (trCostCenter.Visible == true)
                {
                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (trBudgetHead.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    row["Departmentid"] = "0";
                }

                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = "0.00";// txtTamount.Text;
                row["TDSsection"] = ddlSection.SelectedValue;
                //   row["TDSpercentage"] = txtTDSPer.Text;

                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }


    }


    protected void txtTDSPer_TextChanged(object sender, EventArgs e)
    {
        //if (txtTranAmt.Text != string.Empty || txtTranAmt.Text != "")
        {
            if (chkTDSApplicable.Checked == true)
            {
                //if (txtTDSPer.Text != string.Empty || txtTDSPer.Text != "")
                //{
                //    if (Convert.ToDouble(txtTDSPer.Text) >= 100)
                //    {
                //        objCommon.DisplayMessage(UPDLedger, "TDS Percentage Can't be greater than or equal to 100%", this.Page);
                //        //    txtTDSAmount.Text = string.Empty;
                //        txtTDSPer.Text = string.Empty;
                //        txtTDSPer.Focus();
                //        return;
                //    }
                //    // double TransAmt = Convert.ToDouble(txtTranAmt.Text);
                //    double tdsPer = Convert.ToDouble(txtTDSPer.Text);

                //    string calc = String.Format("{0:0.00}", Convert.ToDouble(tdsPer * 0.01));
                //    txtTDSAmount.Text = "0.00";// calc.ToString();
                //}
                //else
                //{
                //    txtTDSAmount.Text = string.Empty;
                //    txtTDSPer.Text = string.Empty;
                //    txtTDSPer.Focus();
                //}
            }
        }
    }
    protected void GridData_DataBound(object sender, EventArgs e)
    {

        foreach (GridViewRow item in GridData.Rows)
        {




        }

    }
    protected void txtTDSLedger_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text == txtTDSLedger.Text)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for TDS which was already selected ", this);
            txtTDSLedger.Text = string.Empty;
            txtTDSLedger.Focus();
            return;
        }
    }
    protected void txtTranAmt_TextChanged(object sender, EventArgs e)
    {

        // if (txtTranAmt.Text != string.Empty || txtTranAmt.Text != "")
        {
            //addeed by vijay on 1309200
            clearTax();
            if (chkTDSApplicable.Checked == true)
            {
                // txtTamount.Text = Convert.ToDouble(txtTranAmt.Text).ToString();
                //if (txtTDSPer.Text != string.Empty || txtTDSPer.Text != "")
                //{

                //    if (Convert.ToDouble(txtTDSPer.Text) >= 100)
                //    {
                //        objCommon.DisplayUserMessage(UPDLedger, "TDS Percentage Can't be greater than or equal to 100%", this);
                //        txtTDSPer.Text = string.Empty;
                //        txtTDSLedger.Text = string.Empty;
                //        txtTDSPer.Focus();
                //        return;
                //    }
                //    double TransAmt = 0.00; //Convert.ToDouble(txtTamount.Text);
                //    double tdsPer = Convert.ToDouble(txtTDSPer.Text);

                //    string calc = String.Format("{0:0.00}", Convert.ToDouble(TransAmt * tdsPer * 0.01));
                //    txtTDSAmount.Text = calc.ToString();
                //}
                //else
                //{
                //    txtTDSAmount.Text = string.Empty;
                //    txtTDSPer.Text = string.Empty;
                //    txtTDSPer.Focus();
                //}
            }
            //if (chkGST.Checked == true)
            //{

            //    txtCGSTAMOUNT1.Text = txtSGSTAMOUNT.Text = Convert.ToDouble(txtTranAmt.Text).ToString();
            //}
            //if (chkIGST.Checked == true)
            //{

            //    txtIGSTAMOUNT.Text = Convert.ToDouble(txtTranAmt.Text).ToString();

            //}
        }
    }
    //added by vijay andoju for clearing the all Tax
    protected void clearTax()
    {
        //  txtTDSPer.Text = txtTamount.Text =
        //txtCGSTAMT.Text = txtCGSTPER.Text =
        //txtSGSTAMT.Text = txtSGTSPer.Text =
        //txtIGSTAMT.Text = txtIGSTPER.Text = string.Empty;
        ddlSection.SelectedValue = "0";
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtTDSPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlSection.SelectedValue);
        //decimal TDSOnAmount =Convert.ToDecimal(0.00) / 100;
        // decimal Per = txtTDSPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTDSPer.Text);
        //decimal Total = TDSOnAmount * Per;
        //  txtTDSAmount.Text = Total.ToString();
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // objCostCenterController.BindBudgetHead(ddlBudgetHead);


    }


    public void clearGst()
    {
        //divgst.Visible = false;
        //divIgst.Visible = false;
        //divcgst.Visible = false;
    }

    //    protected void txtIGST_TextChanged(object sender, EventArgs e)
    //    {

    //    }
    //    protected void txtIGSTPER_TextChanged(object sender, EventArgs e)
    //    {

    //    }

    //protected void txtamount_TextChanged(object sender, EventArgs e)
    //{

    //    DataTable dt = ((DataTable)ViewState["RecTblbuklvouchr"]);
    //    int datacount = dt.Rows.Count;

    //    double amount = 0;
    //    double amount1 = 0;
    //    double amount2 = 0;

    //    foreach (ListViewItem i in lvFields.Items)
    //    {

    //        TextBox txtamount = i.FindControl("txtamount") as TextBox;
    //        TextBox txtamount1 = i.FindControl("txtamount1") as TextBox;
    //        TextBox txtamount2 = i.FindControl("txtamount2") as TextBox;

    //        if (datacount == 1)
    //        {
    //            amount = amount + Convert.ToDouble(txtamount.Text == "" ? "0" : txtamount.Text.ToString().Trim());

    //        }
    //        else if (datacount == 2)
    //        {
    //            amount = amount + Convert.ToDouble(txtamount.Text == "" ? "0" : txtamount.Text.ToString().Trim());
    //            amount1 = amount1 + Convert.ToDouble(txtamount1.Text == "" ? "0" : txtamount1.Text.ToString().Trim());
    //        }
    //        else if (datacount == 3)
    //        {
    //            amount = amount + Convert.ToDouble(txtamount.Text == "" ? "0" : txtamount.Text.ToString().Trim());
    //            amount1 = amount1 + Convert.ToDouble(txtamount1.Text == "" ? "0" : txtamount1.Text.ToString().Trim());
    //            amount2 = amount2 + Convert.ToDouble(txtamount2.Text == "" ? "0" : txtamount2.Text.ToString().Trim());

    //        }
    //    }

    //    lbltotalamount.Text = amount.ToString();

    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    lbltotalamount.Text = dt.Rows[0]["Amount"].ToString();

    //    //}



    //}


    // 
    protected void lvFields_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        if (ViewState["isEdit"].ToString().Trim() == "Y")
        {
            DataTable dt = ((DataTable)ViewState["RecTblEmpData"]);
            string dtcounts = dt.Rows.Count.ToString();
            hdnFieldsCount.Value = dtcounts;

            if (dt.Rows.Count > 0)
            {


                int count = 0;


                if (Convert.ToInt32(dt.Rows[0]["PARTY_NO3"].ToString()) > 1)
                {
                    count = 2;

                }
                else if (Convert.ToInt32(dt.Rows[0]["PARTY_NO2"].ToString()) > 1)
                {
                    count = 1;

                }
                else if (Convert.ToInt32(dt.Rows[0]["PARTY_NO1"].ToString()) > 1)
                {
                    count = 0;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (count >= 0)
                    {
                        PlaceHolder lblLedgerAmount = (PlaceHolder)lvFields.FindControl("lblLedgerAmount");
                        string PartyNo = dt.Rows[i]["PARTY_NO1"].ToString();
                        string ledgerMode = dt.Rows[i]["TRAN1"].ToString();
                        //  amount = Convert.ToDouble(dt.Rows[i]["AMOUNT1"].ToString());
                        if (lblLedgerAmount != null /*&& phDynamicHdr.Controls.Count == 0*/)
                        {
                            hdnLedgerMode.Value = ledgerMode;
                            hdnlblLedgerAmount.Value = PartyNo;

                            //  txtAmount.Text = amount.ToString();
                            Literal ltrl = new Literal();
                            Literal hdltrl = new Literal();

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (e.Item.ItemType == ListViewItemType.DataItem)
                                {
                                    string ledger1 = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PARTY_NAME", "PARTY_NO=" + dt.Rows[0]["PARTY_NO1"].ToString().Trim());

                                    ltrl.Text = ledger1;
                                    //  hdltrl.Text = ledgerMode;

                                }

                            }

                            if (lblLedgerAmount.Controls.Count > 0)
                            {

                                if (!((Literal)lblLedgerAmount.Controls[0]).Text.Equals(ltrl.Text))
                                {

                                    //lblLedgerAmount.Controls.Remove(lblLedgerAmount.Controls[0]);

                                    lblLedgerAmount.Controls.Add(ltrl);
                                }
                            }
                            else
                            {
                                lblLedgerAmount.Controls.Add(ltrl);
                            }
                        }

                        //sum of tot

                        //if (txtFirstNumberValue > 0)
                        //{
                        //    totalAmt = (txtFirstNumberValue);
                        //}
                        //
                    }
                    if (count >= 1)
                    {
                        PlaceHolder lblLedgerAmount1 = (PlaceHolder)lvFields.FindControl("lblLedgerAmount1");
                        string PartyNo = dt.Rows[i]["PARTY_NO2"].ToString();
                        string ledgerMode = dt.Rows[i]["TRAN2"].ToString();
                        // amount1 = Convert.ToDouble(dt.Rows[i]["AMOUNT2"].ToString());

                        if (lblLedgerAmount1 != null)
                        {
                            Literal ltrl1 = new Literal();
                            Literal hdltrl1 = new Literal();
                            hdnLedgerMode1.Value = ledgerMode;
                            hdnlblLedgerAmount1.Value = PartyNo;
                            //   txtAmount1.Text = amount1.ToString();

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (e.Item.ItemType == ListViewItemType.DataItem)
                                // foreach (DataColumn dc in dt.Rows[0].Table.Columns)
                                {
                                    string ledger2 = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PARTY_NAME", "PARTY_NO=" + dt.Rows[0]["PARTY_NO2"].ToString().Trim());

                                    ltrl1.Text = ledger2;

                                }
                            }

                            if (lblLedgerAmount1.Controls.Count > 0)
                            {

                                if (!((Literal)lblLedgerAmount1.Controls[0]).Text.Equals(ltrl1.Text))
                                {

                                    //lblLedgerAmount.Controls.Remove(lblLedgerAmount.Controls[0]);

                                    lblLedgerAmount1.Controls.Add(ltrl1);
                                }
                            }
                            else
                            {
                                lblLedgerAmount1.Controls.Add(ltrl1);
                            }
                        }

                        //sum of tot
                        //if (txtSecondNumberValue > 0)
                        //{
                        //    if (ledgerMode == "Cr")
                        //    {
                        //        totalAmt = (txtFirstNumberValue) - (txtSecondNumberValue);
                        //    }
                        //    else if (ledgerMode == "Dr")
                        //    {
                        //        totalAmt = (txtFirstNumberValue) + (txtSecondNumberValue);
                        //    }
                        //}
                        //
                    }

                    if (count == 2)
                    {
                        PlaceHolder lblLedgerAmount2 = (PlaceHolder)lvFields.FindControl("lblLedgerAmount2");
                        string PartyNo = dt.Rows[i]["PARTY_NO3"].ToString();
                        string ledgerMode = dt.Rows[i]["TRAN3"].ToString();
                        //amount2 = Convert.ToDouble(dt.Rows[i]["AMOUNT3"].ToString());

                        if (lblLedgerAmount2 != null)
                        {
                            Literal ltrl2 = new Literal();
                            Literal hdltrl2 = new Literal();

                            hdnLedgerMode2.Value = ledgerMode;
                            hdnlblLedgerAmount2.Value = PartyNo;
                            // txtAmount2.Text = amount2.ToString();

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (e.Item.ItemType == ListViewItemType.DataItem)
                                // foreach (DataColumn dc in dt.Rows[0].Table.Columns)
                                {

                                    string ledger3 = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_party", "PARTY_NAME", "PARTY_NO=" + dt.Rows[0]["PARTY_NO3"].ToString().Trim());

                                    ltrl2.Text = ledger3;

                                }
                            }

                            if (lblLedgerAmount2.Controls.Count > 0)
                            {

                                if (!((Literal)lblLedgerAmount2.Controls[0]).Text.Equals(ltrl2.Text))
                                {

                                    //lblLedgerAmount.Controls.Remove(lblLedgerAmount.Controls[0]);

                                    lblLedgerAmount2.Controls.Add(ltrl2);
                                }
                            }
                            else
                                lblLedgerAmount2.Controls.Add(ltrl2);
                        }
                        //sum of tot
                        //if (txtThirdNumberValue > 0)
                        //{


                        //    if (ledgerMode == "Cr")
                        //    {
                        //        totalAmt = totalAmt - (txtThirdNumberValue);
                        //    }
                        //    else if (ledgerMode == "Dr")
                        //    {
                        //        totalAmt = totalAmt + (txtThirdNumberValue);
                        //    }
                        //}
                        //
                    }
                    //count++;

                    //GetTotAmountForModifyVoucher();
                }
                //  txtCalculateAmt.Text = totalAmt.ToString();
            }
        }
        else
        {
            DataTable dt = ((DataTable)Session["Datatable"]);

            string counts = dt.Rows.Count.ToString();
            hdnFieldsCount.Value = counts;

            if (dt.Rows.Count > 0)
            {
                int count = 0;
                int datacount = dt.Rows.Count;



                TextBox txtAmount = (TextBox)e.Item.FindControl("txtamount");
                TextBox txtAmount1 = (TextBox)e.Item.FindControl("txtamount1");
                TextBox txtAmount2 = (TextBox)e.Item.FindControl("txtamount2");

                if (datacount == 1)
                {
                    txtAmount1.Visible = false;
                    txtAmount2.Visible = false;
                    lbltotalamount1.Visible = false;
                    lbltotalamount2.Visible = false;

                }
                else if (datacount == 2)
                {
                    txtAmount2.Visible = false;
                    lbltotalamount2.Visible = false;
                }
                else if (datacount == 3)
                {
                    lbltotalamount1.Visible = true;
                    lbltotalamount1.Visible = true;
                    lbltotalamount2.Visible = true;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ledger = dt.Rows[i]["Particulars"].ToString();
                    string ledgerMode = dt.Rows[i]["Mode"].ToString();
                    string PartyNo = dt.Rows[i]["Id"].ToString();

                    if (count == 0)
                    {
                        PlaceHolder lblLedgerAmount = (PlaceHolder)lvFields.FindControl("lblLedgerAmount");

                        if (lblLedgerAmount != null /*&& phDynamicHdr.Controls.Count == 0*/)
                        {
                            hdnLedgerMode.Value = ledgerMode;
                            hdnlblLedgerAmount.Value = PartyNo;
                            Literal ltrl = new Literal();
                            Literal hdltrl = new Literal();

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (e.Item.ItemType == ListViewItemType.DataItem)
                                // foreach (DataColumn dc in dt.Rows[0].Table.Columns)
                                {
                                    //string ledger = dt.Rows[0]["Particulars"].ToString();

                                    {
                                        ltrl.Text = ledger;
                                        //  hdltrl.Text = ledgerMode;

                                    }

                                }
                            }

                            if (lblLedgerAmount.Controls.Count > 0)
                            {

                                if (!((Literal)lblLedgerAmount.Controls[0]).Text.Equals(ltrl.Text))
                                {

                                    //lblLedgerAmount.Controls.Remove(lblLedgerAmount.Controls[0]);

                                    lblLedgerAmount.Controls.Add(ltrl);
                                }
                            }
                            else
                                lblLedgerAmount.Controls.Add(ltrl);
                        }


                    }
                    if (count == 1)
                    {
                        PlaceHolder lblLedgerAmount1 = (PlaceHolder)lvFields.FindControl("lblLedgerAmount1");


                        if (lblLedgerAmount1 != null)
                        {
                            Literal ltrl1 = new Literal();
                            Literal hdltrl1 = new Literal();
                            hdnLedgerMode1.Value = ledgerMode;
                            hdnlblLedgerAmount1.Value = PartyNo;

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (e.Item.ItemType == ListViewItemType.DataItem)
                                // foreach (DataColumn dc in dt.Rows[0].Table.Columns)
                                {
                                    //string ledger = dt.Rows[1]["Particulars"].ToString();

                                    {
                                        ltrl1.Text = ledger;
                                        // hdltrl1.Text = ledgerMode;

                                    }
                                }
                            }

                            if (lblLedgerAmount1.Controls.Count > 0)
                            {

                                if (!((Literal)lblLedgerAmount1.Controls[0]).Text.Equals(ltrl1.Text))
                                {

                                    //lblLedgerAmount.Controls.Remove(lblLedgerAmount.Controls[0]);

                                    lblLedgerAmount1.Controls.Add(ltrl1);
                                }
                            }
                            else
                                lblLedgerAmount1.Controls.Add(ltrl1);
                        }

                    }

                    if (count == 2)
                    {
                        PlaceHolder lblLedgerAmount2 = (PlaceHolder)lvFields.FindControl("lblLedgerAmount2");

                        if (lblLedgerAmount2 != null)
                        {
                            Literal ltrl2 = new Literal();
                            Literal hdltrl2 = new Literal();

                            hdnLedgerMode2.Value = ledgerMode;
                            hdnlblLedgerAmount2.Value = PartyNo;

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (e.Item.ItemType == ListViewItemType.DataItem)
                                // foreach (DataColumn dc in dt.Rows[0].Table.Columns)
                                {
                                    //  string ledger = dt.Rows[3]["Particulars"].ToString();

                                    {
                                        ltrl2.Text = ledger;
                                        // hdltrl2.Text = ledgerMode;


                                    }
                                }
                            }

                            if (lblLedgerAmount2.Controls.Count > 0)
                            {

                                if (!((Literal)lblLedgerAmount2.Controls[0]).Text.Equals(ltrl2.Text))
                                {

                                    //lblLedgerAmount.Controls.Remove(lblLedgerAmount.Controls[0]);

                                    lblLedgerAmount2.Controls.Add(ltrl2);
                                }
                            }
                            else
                                lblLedgerAmount2.Controls.Add(ltrl2);
                        }
                    }
                    count++;


                }
            }
        }


    }


    // Added By Vidisha on 01-09-2021 For Voucher details shown from verify and approved voucher form
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        AccountingVouchersController objAVC = new AccountingVouchersController();
        objAVC.UpdateVoucherVerifyApprove(Convert.ToInt32(ViewState["VOUCHER_SQN"].ToString().Trim()), Convert.ToInt32(Session["userno"]), ViewState["AuthorityType"].ToString(), Session["comp_code"].ToString(), Convert.ToChar(ddlPaymentMode.SelectedValue));
        //Response.Redirect("AccVoucherVerifyApprove.aspx?Action=VerifyBack" + "&pageno=" + Request.QueryString["pageno"]);
        ClearControls();
        if (btnVerify.Text == "Verify")
        {
            Response.Redirect("AccVoucherVerifyApprove.aspx?Action=VerifyBack" + "&pageno=" + Request.QueryString["pageno"]);
        }
        else if (btnVerify.Text == "Approve")
        {
            Response.Redirect("AccVoucherVerifyApprove.aspx?Action=ApproveBack" + "&pageno=" + Request.QueryString["pageno"]);
        }
        else
        {
            Response.Redirect("AccVoucherVerifyApprove.aspx?Action=Back" + "&pageno=" + Request.QueryString["pageno"]);
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccVoucherVerifyApprove.aspx?Action=Back" + "&pageno=" + Request.QueryString["pageno"]);
        ClearControls();
    }

    private void Disable()
    {
        txtAgainstAcc.Enabled = false;
        ddlSponsor.Enabled = false;
        ddlProjSubHead.Enabled = false;
        btnAdd.Visible = false;
        btnDelete.Visible = false;
        btnAddvendor.Visible = false;
        txtNatureService.Enabled = false;
        txtNarration.Enabled = false;
        btnVerify.Visible = true;
        btnReturn.Visible = true;
        ddlPayeeNature.Enabled = false;
        ddlName.Enabled = false;

        foreach (ListViewDataItem item in lvFields.Items)
        {
            TextBox txtamount = item.FindControl("txtamount") as TextBox;
            TextBox txtamount1 = item.FindControl("txtamount1") as TextBox;
            TextBox txtamount2 = item.FindControl("txtamount2") as TextBox;

            txtamount.Enabled = false;
            txtamount1.Enabled = false;
            txtamount2.Enabled = false;
        }
    }

    private void Enable()
    {
        txtAgainstAcc.Enabled = true;
        ddlSponsor.Enabled = true;
        ddlProjSubHead.Enabled = true;
        btnAdd.Visible = true;
        btnDelete.Visible = true;
        btnAddvendor.Visible = true;
        txtNatureService.Enabled = true;
        txtNarration.Enabled = true;
        btnVerify.Visible = false;
        btnReturn.Visible = false;
        ddlPayeeNature.Enabled = true;
        ddlName.Enabled = true;

        foreach (ListViewDataItem item in lvFields.Items)
        {
            TextBox txtamount = item.FindControl("txtamount") as TextBox;
            TextBox txtamount1 = item.FindControl("txtamount1") as TextBox;
            TextBox txtamount2 = item.FindControl("txtamount2") as TextBox;

            txtamount.Enabled = true;
            txtamount1.Enabled = true;
            txtamount2.Enabled = true;
        }
    }

    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            lblAmount.Visible = true;
            ds = objPC1.GetEmployeeWiseAmountDetails(Session["comp_code"].ToString(), Convert.ToInt32(ddlName.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                double amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Total_Sum"].ToString());
                if (amount > 30000)
                {
                    // lblAmount.Font.Bold = True;
                    lblAmount.ForeColor = System.Drawing.Color.Red;
                    lblAmount.Text = ds.Tables[0].Rows[0]["Total_Sum"].ToString();
                }
                else
                {
                    lblAmount.Text = ds.Tables[0].Rows[0]["Total_Sum"].ToString();
                    lblAmount.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblAmount.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }

    }

    private void ClearControls()
    {
        lblAmount.Text = string.Empty;

        Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        lblRemainAmt.Text = "";
        ddlSponsor.SelectedValue = "0";
        ddlProjSubHead.SelectedValue = "0";
        isAllreadySet = "";
        GridData.DataSource = null;
        GridData.DataBind();
        dt.Clear();
        Session["Datatable"] = dt;

        lblTotalDiff.Text = "0.00";
        lblTotalCredit.Text = "0.00";
        lblTotalDebit.Text = "0.00";
        lblCur1.Text = string.Empty;

        if (isSingleMode == "Y")
        {
            txtAgainstAcc.Enabled = true;
            txtPerNarration.Text = "";

            lblCrDr1.Text = "";
            lblCrDr2.Text = "";
            txtAcc.Text = "";
            lblCurBal2.Text = "";
        }
        txtNarration.Text = "";

        txtChqNo1.Text = "";
        txtChequeDt1.Text = "";
        txtChqNo2.Text = string.Empty;
        lblSpan.InnerText = string.Empty;
        lblCurbal1.Text = "0.00";
        lblTotal.Text = "0.00";
        ViewState["TDS"] = string.Empty;

        txtTDSLedger.Text = string.Empty;

        chkTDSApplicable.Checked = false;

        dvTDS.Visible = false;
        ViewState["Balance3"] = "0";
        txtDate.Text = string.Empty;
        txtPartyName.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtNatureService.Text = string.Empty;
        lvFields.DataSource = null;
        lvFields.DataBind();
        lvFields.Visible = false;
        ViewState["SRNO"] = null;
        ViewState["RecTblbuklvouchr"] = null;
        lbltotalamount.Text = string.Empty;
        lbltotalamount1.Text = string.Empty;
        lbltotalamount2.Text = string.Empty;
        ddlPayeeNature.SelectedValue = "0";

        ddlName.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        txtChqNo2.Text = string.Empty;
        txtChequeDt2.Text = string.Empty;

        txtGSTNNO.Text = string.Empty;

        ViewState["IsIGST"] = "0";
        ViewState["IsGST"] = "0";

        divDepartment.Visible = false;
        trBudgetHead.Visible = false;

        lblBudgetBal.Text = "0";
        txtAgainstAcc.Text = string.Empty;
        ddlSection.SelectedValue = "0";

    }

    private void GetBalance()
    {
        int partyId = Convert.ToInt32(txtAgainstAcc.Text.ToString().Split('*')[1].ToString());

        double PartyAgainstBal = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_Party", "BALANCE", "Party_No=" + partyId));
        lblCurbal1.Text = String.Format("{0:0.00}", Convert.ToDouble(PartyAgainstBal.ToString()));
        txtAcc.Focus();
        TextBox1.Text = String.Format("{0:0.00}", Convert.ToDouble(PartyAgainstBal.ToString()));
    }

    private void GetRemainingAmount()
    {
        lblRemainAmt.Text = objPC1.GetSponsorProjectBalance(Convert.ToInt32(ddlSponsor.SelectedValue), Convert.ToInt32(ddlProjSubHead.SelectedValue), Session["comp_code"].ToString());
    }

    private void FillPaymentMode()
    {
        objCommon.FillDropDownList(ddlPaymentMode, "ACC_PAYMODE", "PAYMODE_CODE", "PAYMODE", "", "");
    }



    //added by tanu 13/12/2021 for add multipal bill files 
    #region Upload Bills
    //Check for Valid File 
    #region Upload Bills
    //Check for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".jpeg", ".JPEG", ".pdf", ".PDF", ".Pdf", ".png", ".docx", ".PNG", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    public void AddDocuments(int Voucher_No)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;
            DeletePath();
            int userno = Convert.ToInt32(Session["userno"]);

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\BillNo_" + Voucher_No;

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
            }
            DeleteDirectory(sourcePath);
        }
        catch (Exception ex)
        {

        }
    }
    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            //Delete all files from the Directory
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            //Delete all child Directories
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            //Delete a Directory
            Directory.Delete(path);
        }
    }
    //This is used to add new Bills if any.
    protected void btnAddBill_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["isEdit"].ToString().Trim() == "Y")
            {
                ModifyVoucherTransaction();
            }
            string file = string.Empty;
            int userno = Convert.ToInt32(Session["userno"]);

            if (FileUploadBill.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUploadBill.FileName)))
                {
                    if (FileUploadBill.FileContent.Length >= 5 * 1024 * 1024)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Bill Size Should Not Be Greater Than 5 Mb!", this.Page);
                        FileUploadBill.Dispose();
                        FileUploadBill.Focus();
                        return;
                    }

                    if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["DOCS"];
                        foreach (DataRow drow in dtM.Rows)
                        {
                            if (drow["DisplayFileName"].ToString() == FileUploadBill.FileName.Trim())
                            {
                                objCommon.DisplayMessage(UPDLedger, "This Bill Already Exists.", this.Page);
                                return;
                            }
                        }
                    }

                    //if (ViewState["action"] != null)
                    //{
                    file = Docpath + "TEMPUPLOAD_BILL\\EMPID_" + userno;
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "VOUCHER_BILL\\EMPID_" + userno;
                    ViewState["DESTINATION_PATH"] = PATH;
                    //}

                    if (!System.IO.Directory.Exists(file))
                    {
                        System.IO.Directory.CreateDirectory(file);
                    }

                    path = file + "\\" + FileUploadBill.FileName.Trim();
                    //CHECKING FOLDER EXISTS OR NOT file
                    if (!System.IO.Directory.Exists(path))
                    {
                        if (!File.Exists(path))
                        {
                            Addfieldstotbl();
                            FileUploadBill.PostedFile.SaveAs(path);
                        }
                        else
                        {
                            if (ViewState["DELETE_DOCS"] != null)
                            {
                                int d = 0;
                                DataTable DtDel = (DataTable)ViewState["DELETE_DOCS"];
                                foreach (DataRow row in DtDel.Rows)
                                {
                                    if (FileUploadBill.FileName.ToString() == row["FILENAME"].ToString())
                                    {
                                        Addfieldstotbl();
                                        d++;
                                    }
                                }
                                if (d == 0)
                                {

                                    objCommon.DisplayMessage(UPDLedger, "This Bill Already Exists.!", this.Page);
                                    return;
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                                objCommon.DisplayMessage(UPDLedger, "This Bill Already Exists.!", this.Page);
                                return;
                            }
                        }
                    }
                }
                else
                {

                    objCommon.DisplayMessage(UPDLedger, "Please Upload Valid Bill[.jpg,.pdf,.txt,.doc,.png,.xls]", this.Page);
                    FileUploadBill.Focus();
                }
            }
            else
            {

                objCommon.DisplayMessage(UPDLedger, "Please Select File!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void Addfieldstotbl()
    {
        int userno = Convert.ToInt32(Session["userno"]);
        if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
        {
            DataTable dt = (DataTable)ViewState["DOCS"];
            DataRow dr = dt.NewRow();
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["Filepath"] = Docpath + "TEMPUPLOAD_BILL\\EMPID_" + userno;
            dr["DisplayFileName"] = FileUploadBill.FileName.Trim();
            dr["DocumentName"] = txtBillName.Text.Trim();

            dt.Rows.Add(dr);
            ViewState["DOCS"] = dt;
            lvNewBills.DataSource = ViewState["DOCS"];
            lvNewBills.DataBind();
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            pnlNewBills.Visible = true;
            txtBillName.Text = string.Empty;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["DOCS"];
            DataRow dr = dt.NewRow();
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["Filepath"] = Docpath + "TEMPUPLOAD_BILL\\EMPID_" + userno;
            dr["DisplayFileName"] = FileUploadBill.FileName.Trim();
            dr["DocumentName"] = txtBillName.Text.Trim();

            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["DOCS"] = dt;
            lvNewBills.DataSource = (DataTable)ViewState["DOCS"];
            lvNewBills.DataBind();
            pnlNewBills.Visible = true;
            txtBillName.Text = string.Empty;
        }
    }
    public void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn("FUID", typeof(int));
        dt.Columns.Add(dc);
        dc = new DataColumn("Filepath", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("DocumentName", typeof(string));
        dt.Columns.Add(dc);
        ViewState["DOCS"] = dt;
    }
    protected void imgBill_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.CommandArgument, btn.ToolTip);
    }
    public void DownloadFile(string path, string fileName)
    {
        try
        {
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }
    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
    protected void btnDeleteBill_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string filepath = btnDelete.AlternateText;
            string filename = btnDelete.ToolTip;

            if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
            {
                DataTable dts = (DataTable)ViewState["DOCS"];
                dts.Rows.Remove(this.GetEditableDatarowBill(dts, filename));
                ViewState["DOCS"] = dts;
                lvNewBills.DataSource = dts;
                lvNewBills.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Deleted Successfully.');", true);

                if (ViewState["DELETE_DOCS"] != null && ((DataTable)ViewState["DELETE_DOCS"]) != null)
                {
                    DataTable dtD = (DataTable)ViewState["DELETE_DOCS"];
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = filepath;
                    dr["FILENAME"] = filename;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_DOCS"] = dtD;
                }
                else
                {
                    DataTable dtD = this.CreateTableBill();
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = filepath;
                    dr["FILENAME"] = filename;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_DOCS"] = dtD;
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    private DataTable CreateTableBill()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }
    private DataRow GetEditableDatarowBill(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["DisplayFileName"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return datRow;
    }
    private void DeleteDirecPath(string FilePath)
    {
        if (System.IO.Directory.Exists(FilePath))
        {
            try
            {
                System.IO.Directory.Delete(FilePath, true);
            }

            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    private void DeletePath()
    {
        if (ViewState["DELETE_DOCS"] != null && ((DataTable)ViewState["DELETE_DOCS"]) != null)
        {
            int i = 0;
            DataTable DtDel = (DataTable)ViewState["DELETE_DOCS"];
            foreach (DataRow Dr in DtDel.Rows)
            {
                string filename = DtDel.Rows[i]["FILENAME"].ToString();
                string filepath = DtDel.Rows[i]["FILEPATH"].ToString();

                if (File.Exists(filepath + "\\" + filename))
                {
                    //DELETING THE FILE
                    File.Delete(filepath + "\\" + filename);
                }
                i++;
            }
            ViewState["DELETE_DOCS"] = null;
        }
    }
    private void clearDoc()
    {
        lvNewBills.DataSource = null;
        lvNewBills.DataBind();
        pnlNewBills.Visible = false;

        ViewState["DESTINATION_PATH"] = null;
        ViewState["DOCS"] = null;
        DeleteDirecPath(Docpath + "VOUCHER_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]) + "\\BillNo" + txtVoucherNo.Text.Trim());

    }

    #endregion
    #endregion


    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
        divPayee1.Visible = false;
        divPayee2.Visible = false;
        divPayeeNature1.Visible = false;
        divPayeeNature2.Visible = false;

        ddlEmployee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;
        ddlName.SelectedIndex = 0;
       
        if (ddlEmpType.SelectedValue == "1")
        {
            ddlEmpType.Enabled = false;
            divEmployee1.Visible = true;
            divEmployee2.Visible = true;

            divPayee1.Visible = false;
            divPayee2.Visible = false;
            divPayeeNature1.Visible = false;
            divPayeeNature2.Visible = false;
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
            ddlEmpType.Enabled = false;
            divPayee1.Visible = true;
            divPayee2.Visible = true;
            divPayeeNature1.Visible = true;
            divPayeeNature2.Visible = true;
            divEmployee1.Visible = false;
            divEmployee2.Visible = false;
        }

    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            lblAmount.Visible = true;
            ds = objPC1.GetEmployeeWiseAmountDetails(Session["comp_code"].ToString(), Convert.ToInt32(ddlEmployee.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                double amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Total_Sum"].ToString());
                if (amount > 30000)
                {
                    // lblAmount.Font.Bold = True;
                    lblAmount.ForeColor = System.Drawing.Color.Red;
                    lblAmount.Text = ds.Tables[0].Rows[0]["Total_Sum"].ToString();
                }
                else
                {
                    lblAmount.Text = ds.Tables[0].Rows[0]["Total_Sum"].ToString();
                    lblAmount.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblAmount.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }

    }
   
}
