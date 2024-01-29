//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :ACCOUNTING VOUCHERS                                                     
// CREATION DATE :27-Aug-2014                                              
// CREATED BY    :Nitin Meshram                                        
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
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;


//using System.Windows;
//using System.Windows.Forms;

public partial class AccountingVouchers : System.Web.UI.Page
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
    public static string tranTypeForStrVno;
    public static string StrVno = "abc/111";
    public static string TransactionDateToUpdate = string.Empty;

    string IsDepartmentForAll = string.Empty;

    string IsBudgetDeptForJournal = string.Empty;
    string IsBudgetDeptForReceipt = string.Empty;
    string IsBudgetDeptForPayment = string.Empty;
    string IsBudgetDeptForContra = string.Empty;

    string IsTagForJournal = string.Empty;
    string IsTagForRecipt = string.Empty;
    string IsMultipalCostCenter = string.Empty;
    string IsInvoice = string.Empty;
    AccountTransactionController objPC1 = new AccountTransactionController();
    AccountTransaction objATEnt = new AccountTransaction();

    //For XML Doc

    XmlDocument xmlDoc = new XmlDocument();

    //added by tanu 13/12/2021 for upload bill files
    public string Docpath = HttpContext.Current.Server.MapPath("~/FILEUPLOAD/upload_files/");
    public string path = string.Empty;
    DataTable dtc = new DataTable("DocTbl");

    // For Makaut
    public static string isTempVoucher = string.Empty;

    // added by tanu 02/03/2022
    // string isTempVoucher = string.Empty;

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
                // CheckPageAuthorization();

                ViewState["RowIndex"] = -1;
            ViewState["TDS"] = string.Empty;

            ViewState["IsTdsOnGst"] = string.Empty;
            ViewState["IsTdsOnIgst"] = string.Empty;
            ViewState["IsSecurity"] = string.Empty;

            ViewState["IsIGST"] = "0";
            ViewState["IsGST"] = "0";
          
            // added by tanu 02/03/2022
            //isTempVoucher = "Y";        // Commented By Akshay Dixit On 11-07-2022
            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

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
                        //trSubHead.Attributes.Add("style", "display:none");
                    }
                }

                //if (isSingleMode == "Y")
                //{
                //    // txtAgainstAcc.Focus();
                //}
                //else
                //{
                //    txtAcc.Focus();
                //}
            }

            // Added by Akshay Dixit On 07/04/2022
            int IsCompanyLock = Convert.ToInt32(objCommon.LookUp("ACC_COMPANY", "Lock_Status", "COMPANY_CODE='" + (Session["Comp_Code"]).ToString() + "'"));

            if (IsCompanyLock == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "This Company Is Locked Please Select Current Company", this);

                //if (IsCompanyLock == 1)
                //{


                //    Response.Redirect("~/Account/selectCompany.aspx");
                //}                           
            }

            //added by tanu 14/12/2021

            ViewState["DOCS"] = null;
            DeleteDirecPath(Docpath + "VOUCHER_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]) + "\\BillNo" + txtVoucherNo.Text.Trim());



            Session["ComputerIDAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            //RowIndex = -1;
            //SetDataColumn();
            ViewState["RowIndex"] = -1;
            txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtChequeDt2.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtinvoicedate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
            OnOffBudgetHeadDept();
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
                        //commit by tanu 13/12/2021

                        //  txtAgainstAcc.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() + "*" + ds.Tables[0].Rows[0]["ACC_CODE"].ToString();
                        //  lblCurbal1.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                    }
                }
            }
            txtAgainstAcc.Attributes.Add("OnChange", "javascript:return DoPostBack()");
            objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "NATURE_NAME");
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') + '['+ convert(nvarchar(150),EmployeeId) + ']' AS NAME", "IDNO > 0", "FNAME");
            if (ddlPaymentMode.Items.Count == 0)
            {
                objCommon.FillDropDownList(ddlPaymentMode, "ACC_PAYMODE", "PAYMODE_CODE", "PAYMODE", "", "PAYMODE");
            }
          //  objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
           // OnOffBudgetHeadDept();
        }
        // added by tanu 02/03/2022
        // isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

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
            chkGST.Visible = true;
            chkIGST.Visible = true;
            chkTdsOnGST.Visible = true;
            chkTdsOnIGST.Visible = true;
            chkSecurity.Visible = true;
            DivInvoicedateno.Visible = true;
        }
        else
        {
            chkTDSApplicable.Visible = false;
            chkGST.Visible = false;
            chkIGST.Visible = false;

            chkTdsOnGST.Visible = false;
            chkTdsOnIGST.Visible = false;
            chkSecurity.Visible = false;
            DivInvoicedateno.Visible = false;
        }



    }

   

    private void OnOffBudgetHeadDept()
    {
        if (IsDepartmentForAll == "Y")
        {
            divDepartment.Visible = true;
        }
        else
        {
            divDepartment.Visible = false;
        }

        if (IsBudgetDeptForJournal == "Y")
        {
            ViewState["IsBudgetHead"] = "Yes";
            divDepartment.Visible = true;
            divDeptBudget.Visible = true;
            objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
            //objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

        }
        else if (IsBudgetDeptForReceipt == "Y")
        {
            ViewState["IsBudgetHead"] = "Yes";
            divDepartment.Visible = true;
            divDeptBudget.Visible = true;
            objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
            // objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

        }
        else if (IsBudgetDeptForPayment == "Y")
        {
            ViewState["IsBudgetHead"] = "Yes";
            divDepartment.Visible = true;
            divDeptBudget.Visible = true;
            objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
            //   objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

        }
        else if (IsBudgetDeptForContra == "Y")
        {
            ViewState["IsBudgetHead"] = "Yes";
            divDepartment.Visible = true;
            divDeptBudget.Visible = true;
            objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
            //  objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

        }
        else
        {
            divDeptBudget.Visible = false;
            ViewState["IsBudgetHead"] = "No";

        }



        //if (ddlTranType.SelectedValue == "P")
        //{
        //    //int IsBudgetHead = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISBudgetHead as int),0) ISBudgetHead", "Party_No=" + hdnOpartyManual.Value));
        //    //if (IsBudgetHead == 1)
        //    //{
        //    ViewState["IsBudgetHead"] = "Yes";

        //    divDeptBudget.Visible = true;
        //    //ADDED BY VIJAY ANDOJU ON
        //    objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
        //    objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

        //    //}
        //    //else
        //    //{
        //    //    ViewState["IsBudgetHead"] = "No";               
        //    //    divDeptBudget.Visible = false;                
        //    //}
        //}
        //else
        //{
        //    divDeptBudget.Visible = false;
        //    ViewState["IsBudgetHead"] = "No";
        //}
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
            Session["WithoutCashBank"] = "YN"; 
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
        var thisPage = new AccountingVouchers();
        sVo = thisPage.VoucherNoSettingForCBVoucherNo(Username);
        return sVo;
        //}
        //catch (Exception ex)
        //{
        //    return sVo;
        //}

    }

    //private void VoucherNoSettingForCBVoucherNo(string type)
    //{
    //    if (isVoucherAutoCashBank == "Y" && type != string.Empty)
    //    {
    //        AccountConfigurationController objvch = new AccountConfigurationController();
    //        DataTableReader dtr = objvch.GetMaxVoucherNoSegregated(Session["comp_code"].ToString().Trim(), type);// + "_" + Session["fin_yr"].ToString().Trim());

    //        if (dtr.HasRows == true)
    //        {
    //            if (dtr.Read() == true)
    //            {
    //                lblVoucherNo.Text = dtr["V_NO"].ToString().Trim().ToUpper();
    //                ViewState["STR_CB_VNO"] = type;
    //            }
    //        }

    //    }
    //}

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
        // added by tanu 02/03/2022
        //isTempVoucher = "Y";  //Commented By Akshay Dixit On 11-07-2022
        //isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

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
                {
                    if (isTempVoucher == "Y")
                    {
                        dtr = objvch.GetTempMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                    }
                    else
                    {
                        dtr = objvch.GetTempMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                    }
                }
                else
                {
                    if (isTempVoucher == "Y")
                    {
                        dtr = objvch.GetTempVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                    }
                    else
                    {
                        dtr = objvch.GetTempVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
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
                        if (isTempVoucher == "Y")
                        {
                            dtr = objvch.GetTempMaxVoucherNo("ALL_TEMP", Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            dtr = objvch.GetTempMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                        }
                    }
                    else
                    {
                        if (isTempVoucher == "Y")
                        {
                            dtr = objvch.GetTempVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            dtr = objvch.GetTempVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
                        }
                    }
                    //if (isVoucherTypeWise == "N")
                    //    dtr = objvch.GetMaxVoucherNo(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));// + "_" + Session["fin_yr"].ToString().Trim());
                    //else
                    //    dtr = objvch.GetVoucherNo(Session["comp_code"].ToString().Trim(), VCH_TYPE, Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy"));
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
        {
            txtVoucherNo.Enabled = true; 
        }

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
                    else
                    {
                        isSingleMode = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim(); 
                    }

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
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "DEPARTMENT VISIBLE FOR  VOUCHER TRAN")
                {
                    IsDepartmentForAll = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }

                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "BUDGET VISIBLE FOR JOURNAL")
                {
                    IsBudgetDeptForJournal = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "BUDGET VISIBLE FOR RECEIPT")
                {
                    IsBudgetDeptForReceipt = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "BUDGET VISIBLE FOR PAYMENT")
                {
                    IsBudgetDeptForPayment = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "BUDGET VISIBLE CONTRA")
                {
                    IsBudgetDeptForContra = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }

                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ADVANCED ADJUSTMENT FOR JOURNAL")
                {
                    IsTagForJournal = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ADVANCED ADJUSTMENT FOR RECEIPT")
                {
                    IsTagForRecipt = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "IS MULTIPAL COST CENTER")
                {
                    IsMultipalCostCenter = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }
                else if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "IS INVOICE NO AND INVOICE DATE")
                {
                    IsInvoice = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }

            }
        }
    }

    private void UpdateDataRow(int Index, DataTable dtupd)
    {
        UpdateRowNew(Index);
        Session["Datatable"] = dt;
        AddDeleteId();

    }

    private void UpdateDataRow_Journal(int Index, DataTable dtupd)
    {
        UpdateRowNew(Index);
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

        if (Convert.ToDouble(txtTranAmt.Text) < 0)
        {
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Dr";
            }
            else
            {
                TranMode = "Cr";
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToDouble(dt.Rows[i]["IGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["SGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["CGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TDSpercentage"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TdsOnCgstPer"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TdsOnSgstPer"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TdsOnIgstPer"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["SecurityAmt"].ToString()) == 0.00 && dt.Rows[i]["Mode"].ToString().Trim() == "Dr")
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
                    dt.Rows[i]["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                    dt.Rows[i]["Credit"] = "0.00";
                    dt.Rows[i]["Mode"] = "Dr";
                    dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) + Convert.ToDouble(txtTranAmt.Text));
                }
                else
                {
                    dt.Rows[i]["Debit"] = "0.00";
                    dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                    dt.Rows[i]["Mode"] = "Cr";
                }
                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim()))); ;





                if (ddlCostCenter.SelectedIndex > 0)
                {

                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtAcc.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";

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

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }


            if (Convert.ToDouble(dt.Rows[i]["IGSTper"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtIGST.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();


                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtIGST.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) + Convert.ToDouble(txtIGSTAMT.Text));
                dt.Rows[i]["Credit"] = "0.00";
                dt.Rows[i]["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Dr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));


                if (ddlCostCenter.SelectedIndex > 0)
                {

                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }

                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtIGST.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";
                //added by vijay andoju on 26082020 fro IGST
                dt.Rows[i]["IGSTAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
                dt.Rows[i]["IGSTper"] = Convert.ToDouble(txtIGSTPER.Text).ToString();
                dt.Rows[i]["IGSTonAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIgstOnAmount.Text.ToString().Trim())));


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }
            if (Convert.ToDouble(dt.Rows[i]["SGSTper"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtSGST.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();


                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtSGST.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) + Convert.ToDouble(txtSGSTAMT.Text));
                dt.Rows[i]["Credit"] = "0.00";
                dt.Rows[i]["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Dr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));

                if (ddlCostCenter.SelectedIndex > 0)
                {

                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }


                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtSGST.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";
                //added by vijay andoju on 26082020 fro IGST
                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
                dt.Rows[i]["SGSTper"] = Convert.ToDouble(txtSGTSPer.Text).ToString();
                dt.Rows[i]["SGSTonAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSgstOnAmount.Text.ToString().Trim())));

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }
            if (Convert.ToDouble(dt.Rows[i]["CGSTper"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtCGST.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();


                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtCGST.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) + Convert.ToDouble(txtCGSTAMT.Text));

                dt.Rows[i]["Credit"] = "0.00";
                dt.Rows[i]["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Dr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim())));



                if (ddlCostCenter.SelectedIndex > 0)
                {
                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtCGST.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";
                //added by vijay andoju on 26082020 fro IGST
                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim())));
                dt.Rows[i]["CGSTper"] = Convert.ToDouble(txtCgstPer.Text).ToString();
                dt.Rows[i]["CGSTonAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCgstOnAmount.Text.ToString().Trim())));

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) - Convert.ToDouble(txtTDSAmount.Text));

                dt.Rows[i]["Debit"] = "0.00";
                dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Cr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));


                if (ddlCostCenter.SelectedIndex > 0)
                {
                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtTDSLedger.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                dt.Rows[i]["TDSsection"] = ddlTdsSection.SelectedValue;
                dt.Rows[i]["TDSpercentage"] = Convert.ToDouble(txtTDSPer.Text).ToString();
                dt.Rows[i]["TDSonAmount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnAmount.Text.ToString().Trim()))); ;
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

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }
            //Added by gopal anthati on 31-08-2021
            if (Convert.ToDouble(dt.Rows[i]["TdsOnCgstPer"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtTdsOnCgstAcc.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtTdsOnCgstAcc.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) - Convert.ToDouble(txtTdsOnCgstAmt.Text));

                dt.Rows[i]["Debit"] = "0.00";
                dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Cr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));


                if (ddlCostCenter.SelectedIndex > 0)
                {
                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtTdsOnCgstAcc.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0.00";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";


                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
                dt.Rows[i]["TdsOnCgstSection"] = ddlTdsOnCgstSection.SelectedValue;
                dt.Rows[i]["TdsOnCgstPer"] = Convert.ToDouble(txtTdsOnCgstPer.Text).ToString();
                dt.Rows[i]["TdsCgstOnAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsCgstOnAmt.Text.ToString().Trim())));

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }

            if (Convert.ToDouble(dt.Rows[i]["TdsOnSgstPer"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtTdsOnSgstAcc.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtTdsOnSgstAcc.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) - Convert.ToDouble(txtTdsOnSgstAmt.Text));

                dt.Rows[i]["Debit"] = "0.00";
                dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Cr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));


                if (ddlCostCenter.SelectedIndex > 0)
                {
                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtTdsOnSgstAcc.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0.00";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";


                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
                dt.Rows[i]["TdsOnSgstSection"] = ddlTdsOnSgstSection.SelectedValue;
                dt.Rows[i]["TdsOnSgstPer"] = Convert.ToDouble(txtTdsOnSgstPer.Text).ToString();
                dt.Rows[i]["TdsSgstOnAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsSgstOnAmt.Text.ToString().Trim())));

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }

            if (Convert.ToDouble(dt.Rows[i]["TdsOnIgstPer"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtTdsOnIgstAcc.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtTdsOnIgstAcc.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) - Convert.ToDouble(txtTdsOnIgstAmt.Text));

                dt.Rows[i]["Debit"] = "0.00";
                dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Cr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));


                if (ddlCostCenter.SelectedIndex > 0)
                {
                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtTdsOnIgstAcc.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0.00";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";


                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
                dt.Rows[i]["TdsOnIgstSection"] = ddlTdsOnIgstSection.SelectedValue;
                dt.Rows[i]["TdsOnIgstPer"] = Convert.ToDouble(txtTdsOnIgstPer.Text).ToString();
                dt.Rows[i]["TdsIgstOnAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));

                dt.Rows[i]["SecurityAmt"] = "0.00";

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows[i]["DepartmentId"] = ddldepartment.SelectedValue;
                dt.Rows[i].AcceptChanges();
            }

            if (Convert.ToDouble(dt.Rows[i]["SecurityAmt"].ToString()) > 0)
            {
                dt.Rows[i]["Particulars"] = txtSecurityAcc.Text.ToString().Trim();
                dt.Rows[i]["Narration"] = txtPerNarration.Text.ToString().Trim();

                DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(txtSecurityAcc.Text.Split('*')[1].ToString()), Session["comp_code"].ToString());

                if (dtr.Read())
                {
                    AMOUNT = dtr["BALANCE"].ToString().Trim();

                }
                dt.Rows[i]["Balance"] = Convert.ToString(Convert.ToDouble(AMOUNT) - Convert.ToDouble(txtSecurityAmt.Text));

                dt.Rows[i]["Debit"] = "0.00";
                dt.Rows[i]["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
                dt.Rows[i]["Mode"] = "Cr";

                dt.Rows[i]["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));


                if (ddlCostCenter.SelectedIndex > 0)
                {
                    dt.Rows[i]["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    dt.Rows[i]["CCID"] = "0";
                }
                dt.Rows[i]["BudgetNo"] = "0";

                dt.Rows[i]["Id"] = Convert.ToInt32(txtSecurityAcc.Text.Split('*')[1].ToString());
                // dt.Rows[i]["OppParty"] = Convert.ToInt32(txtAgainstAcc.Text.Split('*')[1].ToString());

                dt.Rows[i]["ChqNo"] = txtChqNo2.Text.ToString().Trim();
                dt.Rows[i]["ChqDate"] = txtChequeDt2.Text.ToString().Trim();

                dt.Rows[i]["TDSAMOUNT"] = "0.00";
                dt.Rows[i]["TDSsection"] = "0.00";
                dt.Rows[i]["TDSpercentage"] = "0.00";
                dt.Rows[i]["TDSonAmount"] = "0.00";


                dt.Rows[i]["IGSTAmount"] = "0.00";
                dt.Rows[i]["IGSTper"] = "0.00";
                dt.Rows[i]["IGSTonAmount"] = "0.00";


                dt.Rows[i]["CGSTAmount"] = "0.00";
                dt.Rows[i]["CGSTper"] = "0.00";
                dt.Rows[i]["CGSTonAmount"] = "0.00";

                dt.Rows[i]["SGSTAmount"] = "0.00";
                dt.Rows[i]["SGSTper"] = "0.00";
                dt.Rows[i]["SGSTonAmount"] = "0.00";

                //added by gopal anthati on 31-08-2021
                dt.Rows[i]["TdsOnCgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnCgstSection"] = "0.00";
                dt.Rows[i]["TdsOnCgstPer"] = "0.00";
                dt.Rows[i]["TdsCgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnSgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnSgstSection"] = "0.00";
                dt.Rows[i]["TdsOnSgstPer"] = "0.00";
                dt.Rows[i]["TdsSgstOnAmt"] = "0.00";

                dt.Rows[i]["TdsOnIgstAmt"] = "0.00";
                dt.Rows[i]["TdsOnIgstSection"] = "0.00";
                dt.Rows[i]["TdsOnIgstPer"] = "0.00";
                dt.Rows[i]["TdsIgstOnAmt"] = "0.00";

                dt.Rows[i]["SecurityAmt"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));

                if (ddlEmpType.SelectedValue == "1")
                {
                    dt.Rows[i]["IsEmployee"] = "1";
                    dt.Rows[i]["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    dt.Rows[i]["IsEmployee"] = "2";
                    dt.Rows[i]["TagEmpIdno"] = ddlPayee.SelectedValue;
                    dt.Rows[i]["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    dt.Rows[i]["IsEmployee"] = "";
                    dt.Rows[i]["TagEmpIdno"] = "";
                    dt.Rows[i]["TagEmployee"] = string.Empty;
                }
                dt.Rows[i]["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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

        if (Convert.ToDouble(txtTranAmt.Text) < 0)
        {
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Dr";
            }
            else
            {
                TranMode = "Cr";
            }
        }
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
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim()))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                // txtAgainstAcc.Focus();
                return;
            }
        }
        else
        {
            if (ViewState["Balance2"].ToString().Trim() != "")
            {
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTranAmt.Text))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                //  txtAgainstAcc.Focus();
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
            row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
        }
        else
        {
            GridData.Columns[4].Visible = false;
            GridData.Columns[5].Visible = true;
            GridData.Columns[6].Visible = true;

            if (TranMode.ToString().Trim() == "Dr")
            {
                row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                row["Credit"] = "0.00";
            }
            else
            {
                row["Debit"] = "0.00";
                row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
            }

            row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));// "0.00";

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
            if (ddlCostCenter.SelectedIndex > 0)
            {

                row["CCID"] = ddlCostCenter.SelectedValue;
            }
            else
            {
                row["CCID"] = 0;
            }
        }
        else
        {
            row["CCID"] = 0;
        }

        if (divDeptBudget.Visible == true)
        {
            row["BudgetNo"] = ddlBudgetHead.SelectedValue;
            // row["Departmentid"] = ddldepartment.SelectedValue;
        }
        else
        {
            row["BudgetNo"] = 0;
            // row["Departmentid"] = 0;
        }
        row["Departmentid"] = ddldepartment.SelectedValue;
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
        //added by Gopal Anthati on 01-09-2021
        row["TdsOnCgstAmt"] = "0";
        row["TdsOnCgstSection"] = "0";
        row["TdsOnCgstPer"] = "0";
        row["TdsCgstOnAmt"] = "0";

        row["TdsOnSgstAmt"] = "0";
        row["TdsOnSgstSection"] = "0";
        row["TdsOnSgstPer"] = "0";
        row["TdsSgstOnAmt"] = "0";

        row["TdsOnIgstAmt"] = "0";
        row["TdsOnIgstSection"] = "0";
        row["TdsOnIgstPer"] = "0";
        row["TdsIgstOnAmt"] = "0";

        row["SecurityAmt"] = "0";

        if (ddlEmpType.SelectedValue == "1")
        {
            row["IsEmployee"] = "1";
            row["TagEmpIdno"] = ddlEmployee.SelectedValue;
            row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
            row["IsEmployee"] = "2";
            row["TagEmpIdno"] = ddlPayee.SelectedValue;
            row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
        }
        else
        {
            row["IsEmployee"] = "";
            row["TagEmpIdno"] = "";
            row["TagEmployee"] = string.Empty;
        }
        row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
        //-------------
        dt.Rows.Add(row);

        Session["Datatable"] = dt;
        if (chkGST.Checked == true)
        {
            AddGSTGrid();
        }
        if (chkIGST.Checked == true)
        {
            AddIGSTGrid();
        }

        if (chkTDSApplicable.Checked == true)
        {
            if (txtTDSAmount.Text != "" || txtTDSAmount.Text != string.Empty || txtTDSPer.Text != string.Empty || txtTDSPer.Text != "")
            {
                AddTDSGrid();
            }
        }

        if (chkTdsOnGST.Checked == true)
        {
            if (txtTdsOnCgstAmt.Text != "" || txtTdsOnCgstAmt.Text != string.Empty || txtTdsOnCgstPer.Text != string.Empty || txtTdsOnCgstPer.Text != "")
            {
                AddTdsOnCgstGrid();
            }
            if (txtTdsOnSgstAmt.Text != "" || txtTdsOnSgstAmt.Text != string.Empty || txtTdsOnSgstPer.Text != string.Empty || txtTdsOnSgstPer.Text != "")
            {
                AddTdsOnSgstGrid();
            }
        }

        if (chkTdsOnIGST.Checked == true)
        {
            if (txtTdsOnIgstAmt.Text != "" || txtTdsOnIgstAmt.Text != string.Empty || txtTdsOnIgstPer.Text != string.Empty || txtTdsOnIgstPer.Text != "")
            {
                AddTdsOnIgstGrid();
            }
        }
        if (chkSecurity.Checked == true)
        {
            if (txtSecurityAmt.Text != "" || txtSecurityAmt.Text != string.Empty)
            {
                AddSecurityAmtGrid();
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

    private void AddTdsOnCgstGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkTdsOnGST.Checked == false)
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance2"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        //   txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
                    }

                    row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtTdsOnCgstAcc.Text.Trim().ToString().Split('*')[1]).ToString();
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    //  row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                row["Section"] = 0;
                //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
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

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = txtTdsOnCgstAmt.Text;
                row["TdsOnCgstSection"] = ddlTdsOnCgstSection.SelectedValue;
                row["TdsOnCgstPer"] = txtTdsOnCgstPer.Text;
                row["TdsCgstOnAmt"] = txtTdsCgstOnAmt.Text;

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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
                row["Particulars"] = txtTdsOnCgstAcc.Text.ToString().Trim();
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTdsOnCgstAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance3"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    //row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    //  row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = "0";
                row["TDSsection"] = "0";
                row["TDSpercentage"] = "0";

                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = txtTdsOnCgstAmt.Text;
                row["TdsOnCgstSection"] = ddlTdsOnCgstSection.SelectedValue;
                row["TdsOnCgstPer"] = txtTdsOnCgstPer.Text;
                row["TdsCgstOnAmt"] = txtTdsCgstOnAmt.Text;

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }
    }

    private void AddTdsOnSgstGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkTdsOnGST.Checked == false)
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance2"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        //  txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
                    }

                    row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtTdsOnSgstAcc.Text.Trim().ToString().Split('*')[1]).ToString();
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    //row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    //row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                row["Section"] = 0;
                //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
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

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = txtTdsOnSgstAmt.Text;
                row["TdsOnSgstSection"] = ddlTdsOnSgstSection.SelectedValue;
                row["TdsOnSgstPer"] = txtTdsOnSgstPer.Text;
                row["TdsSgstOnAmt"] = txtTdsSgstOnAmt.Text;

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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
                row["Particulars"] = txtTdsOnSgstAcc.Text.ToString().Trim();
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTdsOnSgstAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance3"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        //txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    //row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    // row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = "0";
                row["TDSsection"] = "0";
                row["TDSpercentage"] = "0";

                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = txtTdsOnSgstAmt.Text;
                row["TdsOnSgstSection"] = ddlTdsOnSgstSection.SelectedValue;
                row["TdsOnSgstPer"] = txtTdsOnSgstPer.Text;
                row["TdsSgstOnAmt"] = txtTdsSgstOnAmt.Text;

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }
    }

    private void AddTdsOnIgstGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkTdsOnIGST.Checked == false)
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance2"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
                    }

                    row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtTdsOnIgstAcc.Text.Trim().ToString().Split('*')[1]).ToString();
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    //row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                row["Section"] = 0;
                //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
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

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = txtTdsOnIgstAmt.Text;
                row["TdsOnIgstSection"] = ddlTdsOnIgstSection.SelectedValue;
                row["TdsOnIgstPer"] = txtTdsOnIgstPer.Text;
                row["TdsIgstOnAmt"] = txtTdsIgstOnAmt.Text;

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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
                row["Particulars"] = txtTdsOnIgstAcc.Text.ToString().Trim();
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTdsOnIgstAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        //  txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance3"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    //row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    //row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = "0";
                row["TDSsection"] = "0";
                row["TDSpercentage"] = "0";

                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = txtTdsOnIgstAmt.Text;
                row["TdsOnIgstSection"] = ddlTdsOnIgstSection.SelectedValue;
                row["TdsOnIgstPer"] = txtTdsOnIgstPer.Text;
                row["TdsIgstOnAmt"] = txtTdsIgstOnAmt.Text;

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }
    }

    private void AddSecurityAmtGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkTdsOnIGST.Checked == false)
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance2"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtSecurityAmt.Text))).ToString().Trim();
                        ViewState["Balance2"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        //txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
                    }

                    row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtSecurityAcc.Text.Trim().ToString().Split('*')[1]).ToString();
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    //row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    // row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                //row["Section"] = 0;
                //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
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

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = txtSecurityAmt.Text;

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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
                row["Particulars"] = txtSecurityAcc.Text.ToString().Trim();
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtSecurityAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
                        return;
                    }
                }
                else
                {
                    if (ViewState["Balance3"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtSecurityAmt.Text))).ToString().Trim();
                        ViewState["Balance3"] = row["Balance"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                        // txtAgainstAcc.Focus();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    // row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = "0";
                row["TDSsection"] = "0";
                row["TDSpercentage"] = "0";

                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = txtSecurityAmt.Text;

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }
    }

    protected void UpdateRowNew(int Index)
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

        if (Convert.ToDouble(txtTranAmt.Text) < 0)
        {
            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Dr";
            }
            else
            {
                TranMode = "Cr";
            }
        }
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
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim()))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                // txtAgainstAcc.Focus();
                return;
            }
        }
        else
        {
            if (ViewState["Balance2"].ToString().Trim() != "")
            {
                row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTranAmt.Text))).ToString().Trim();
                ViewState["Balance2"] = row["Balance"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                // txtAgainstAcc.Focus();
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
            row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
        }
        else
        {
            GridData.Columns[4].Visible = false;
            GridData.Columns[5].Visible = true;
            GridData.Columns[6].Visible = true;

            if (TranMode.ToString().Trim() == "Dr")
            {
                row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
                row["Credit"] = "0.00";
            }
            else
            {
                row["Debit"] = "0.00";
                row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));
            }

            row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTranAmt.Text.ToString().Trim())));// "0.00";

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
            if (ddlCostCenter.SelectedIndex > 0)
            {

                row["CCID"] = ddlCostCenter.SelectedValue;
            }
            else
            {
                row["CCID"] = 0;
            }
        }
        else
        {
            row["CCID"] = 0;
        }

        if (divDeptBudget.Visible == true)
        {
            row["BudgetNo"] = ddlBudgetHead.SelectedValue;
            // row["Departmentid"] = ddldepartment.SelectedValue;
        }
        else
        {
            row["BudgetNo"] = 0;
            // row["Departmentid"] = 0;
        }
        row["Departmentid"] = ddldepartment.SelectedValue;
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

        //added by gOPAL ANTHATI on 31-08-2021 
        row["TdsOnCgstAmt"] = "0";
        row["TdsOnCgstSection"] = "0";
        row["TdsOnCgstPer"] = "0";
        row["TdsCgstOnAmt"] = "0";

        row["TdsOnSgstAmt"] = "0";
        row["TdsOnSgstSection"] = "0";
        row["TdsOnSgstPer"] = "0";
        row["TdsSgstOnAmt"] = "0";

        row["TdsOnIgstAmt"] = "0";
        row["TdsOnIgstSection"] = "0";
        row["TdsOnIgstPer"] = "0";
        row["TdsIgstOnAmt"] = "0";

        row["SecurityAmt"] = "0";

        if (ddlEmpType.SelectedValue == "1")
        {
            row["IsEmployee"] = "1";
            row["TagEmpIdno"] = ddlEmployee.SelectedValue;
            row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
            row["IsEmployee"] = "2";
            row["TagEmpIdno"] = ddlPayee.SelectedValue;
            row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
        }
        else
        {
            row["IsEmployee"] = "";
            row["TagEmpIdno"] = "";
            row["TagEmployee"] = string.Empty;
        }

        row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
        dt.Rows.Add(row);

        #endregion

        #region GSTROW
        if (ViewState["IsGST"].ToString() == "Yes")
        {

            row = dt.NewRow();
            row["Particulars"] = txtCGST.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";
            }
            else
            {
                TranMode = "Dr";
            }

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Cr";
                }
                else
                {
                    TranMode = "Dr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtCGST.Text.ToString().Trim().Split('*')[1] + "'");
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance2"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    //  txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance2"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtCGSTAMT.Text))).ToString().Trim();
                    ViewState["Balance2"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    // txtAgainstAcc.Focus();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim() == "" ? "0" : txtCGSTAMT.Text.ToString().Trim())));
                }

                row["Amount"] = "0.00";

            }
            row["Mode"] = TranMode.ToString().Trim();
            //row["Id"] = hdnIdEditParty.Value;
            row["Id"] = partyNo;
            row["OppParty"] = Convert.ToInt32(txtCGST.Text.Trim().ToString().Split('*')[1]).ToString();
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            row["CGSTAmount"] = txtCGSTAMT.Text;
            row["CGSTper"] = txtCgstPer.Text;
            row["CGSTonAmount"] = txtCgstOnAmount.Text;

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
            dt.Rows.Add(row);
            Session["Datatable"] = dt;

            row = dt.NewRow();
            row["Particulars"] = txtSGST.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";

            }
            else
            {
                TranMode = "Dr";
            }

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtSGST.Text.ToString().Trim().Split('*')[1] + "'");
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    // txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtSGSTAMT.Text))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    //  txtAgainstAcc.Focus();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            row["SGSTAmount"] = txtSGSTAMT.Text;
            row["SGSTper"] = txtSGTSPer.Text;
            row["SGSTonAmount"] = txtSgstOnAmount.Text;

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
            dt.Rows.Add(row);
            Session["Datatable"] = dt;
        }
        #endregion

        #region IGSTROW
        if (ViewState["IsIGST"].ToString() == "Yes")
        {

            row = dt.NewRow();
            row["Particulars"] = txtIGST.Text.ToString().Trim();
            row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

            if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
            {
                TranMode = "Cr";


            }
            else
            {
                TranMode = "Dr";

            }

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtIGST.Text.ToString().Trim().Split('*')[1] + "'");
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    //  txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtIGSTAMT.Text))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    //  txtAgainstAcc.Focus();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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
            row["IGSTAmount"] = txtIGSTAMT.Text;
            row["IGSTper"] = txtIGSTPER.Text;
            row["IGSTonAmount"] = txtIgstOnAmount.Text;

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;

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

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    // txtAgainstAcc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["Balance3"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
                    ViewState["Balance3"] = row["Balance"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Invalid Ledger Or Balance,Please verify.", this);
                    //  txtAgainstAcc.Focus();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
            row["TDSAMOUNT"] = txtTDSAmount.Text;
            row["TDSsection"] = ddlTdsSection.SelectedValue;
            row["TDSpercentage"] = txtTDSPer.Text;

            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
            dt.Rows.Add(row);
            Session["Datatable"] = dt;
        }
        #endregion

        #region TDS On GST row
        if (ViewState["IsTdsOnGst"].ToString() == "Yes")
        {
            ////////////////Tds On Cgst Row
            row = dt.NewRow();
            row["Particulars"] = txtTdsOnCgstAcc.Text.ToString().Trim();
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

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTdsOnCgstAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                string TdsOnCgstAmt = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance3"] = TdsOnCgstAmt.ToString();// dtr["BALANCE"].ToString().Trim();
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim()))).ToString().Trim();
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text))).ToString().Trim();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnCgstAmt.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            row["TDSAMOUNT"] = "0";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0";

            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = txtTdsOnCgstAmt.Text;
            row["TdsOnCgstSection"] = ddlTdsOnCgstSection.SelectedValue;
            row["TdsOnCgstPer"] = txtTdsOnCgstPer.Text;
            row["TdsCgstOnAmt"] = txtTdsCgstOnAmt.Text;

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;

            dt.Rows.Add(row);
            Session["Datatable"] = dt;
            /////////////////

            ////////////////Tds On Sgst Row
            row = dt.NewRow();
            row["Particulars"] = txtTdsOnSgstAcc.Text.ToString().Trim();
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

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTdsOnSgstAcc.Text.ToString().Trim().Split('*')[1] + "'");
            count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == partyNo)
                {
                    count++;
                }
            }

            dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

            if (dtr.Read())
            {
                string TdsOnSgstAmt = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance5"] = TdsOnSgstAmt.ToString();// dtr["BALANCE"].ToString().Trim();
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
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance5"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance5"] = balance;
                        }
                        else
                        {
                            balance = Convert.ToString(Convert.ToDouble(ViewState["Balance5"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                            ViewState["Balance5"] = balance;
                        }

                    }
                }
            }

            if (TranMode.ToString().Trim() == "Dr")
            {
                if (ViewState["Balance5"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance5"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim()))).ToString().Trim();
                    ViewState["Balance5"] = row["Balance"].ToString();
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
                if (ViewState["Balance5"].ToString().Trim() != "")
                {
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance5"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text))).ToString().Trim();
                    ViewState["Balance5"] = row["Balance"].ToString();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnSgstAmt.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            row["TDSAMOUNT"] = "0";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0";

            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = txtTdsOnSgstAmt.Text;
            row["TdsOnSgstSection"] = ddlTdsOnSgstSection.SelectedValue;
            row["TdsOnSgstPer"] = txtTdsOnSgstPer.Text;
            row["TdsSgstOnAmt"] = txtTdsSgstOnAmt.Text;

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
            dt.Rows.Add(row);
            Session["Datatable"] = dt;
            /////////

        }
        #endregion

        #region TDS On IGST row
        if (ViewState["IsTdsOnIgst"].ToString() == "Yes")
        {

            row = dt.NewRow();
            row["Particulars"] = txtTdsOnIgstAcc.Text.ToString().Trim();
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

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtTdsOnIgstAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                string TdsOnIgstAmt = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance3"] = TdsOnIgstAmt.ToString();// dtr["BALANCE"].ToString().Trim();
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim()))).ToString().Trim();
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text))).ToString().Trim();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTdsOnIgstAmt.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            row["TDSAMOUNT"] = "0";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0";

            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = txtTdsOnIgstAmt.Text;
            row["TdsOnIgstSection"] = ddlTdsOnIgstSection.SelectedValue;
            row["TdsOnIgstPer"] = txtTdsOnIgstPer.Text;
            row["TdsIgstOnAmt"] = txtTdsIgstOnAmt.Text;

            row["SecurityAmt"] = "0";

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
            dt.Rows.Add(row);
            Session["Datatable"] = dt;
        }
        #endregion

        #region Security row
        if (ViewState["IsSecurity"].ToString() == "Yes")
        {
            row = dt.NewRow();
            row["Particulars"] = txtSecurityAcc.Text.ToString().Trim();
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

            if (Convert.ToDouble(txtTranAmt.Text) < 0)
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Dr";
                }
                else
                {
                    TranMode = "Cr";
                }
            }
            partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtSecurityAcc.Text.ToString().Trim().Split('*')[1] + "'");
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
                string TdsOnIgstAmt = dtr["BALANCE"].ToString().Trim();
                ViewState["Balance3"] = TdsOnIgstAmt.ToString();// dtr["BALANCE"].ToString().Trim();
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim()))).ToString().Trim();
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
                    row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtSecurityAmt.Text))).ToString().Trim();
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
                row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
            }
            else
            {
                GridData.Columns[4].Visible = false;
                GridData.Columns[5].Visible = true;
                GridData.Columns[6].Visible = true;

                if (TranMode.ToString().Trim() == "Dr")
                {
                    row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
                    row["Credit"] = "0.00";
                }
                else
                {
                    row["Debit"] = "0.00";
                    row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSecurityAmt.Text.ToString().Trim())));
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
                if (ddlCostCenter.SelectedIndex > 0)
                {

                    row["CCID"] = ddlCostCenter.SelectedValue;
                }
                else
                {
                    row["CCID"] = 0;
                }
            }
            else
            {
                row["CCID"] = 0;
            }

            if (divDeptBudget.Visible == true)
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

            row["TDSAMOUNT"] = "0";
            row["TDSsection"] = "0";
            row["TDSpercentage"] = "0";

            row["IGSTAmount"] = "0.00";
            row["IGSTper"] = "0";
            row["IGSTonAmount"] = "0.00";

            row["CGSTAmount"] = "0.00";
            row["CGSTper"] = "0.00";
            row["CGSTonAmount"] = "0.00";

            row["SGSTAmount"] = "0.00";
            row["SGSTper"] = "0.00";
            row["SGSTonAmount"] = "0.00";

            //added by gOPAL ANTHATI on 31-08-2021 
            row["TdsOnCgstAmt"] = "0";
            row["TdsOnCgstSection"] = "0";
            row["TdsOnCgstPer"] = "0";
            row["TdsCgstOnAmt"] = "0";

            row["TdsOnSgstAmt"] = "0";
            row["TdsOnSgstSection"] = "0";
            row["TdsOnSgstPer"] = "0";
            row["TdsSgstOnAmt"] = "0";

            row["TdsOnIgstAmt"] = "0";
            row["TdsOnIgstSection"] = "0";
            row["TdsOnIgstPer"] = "0";
            row["TdsIgstOnAmt"] = "0";

            row["SecurityAmt"] = txtSecurityAmt.Text;

            if (ddlEmpType.SelectedValue == "1")
            {
                row["IsEmployee"] = "1";
                row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                row["IsEmployee"] = "2";
                row["TagEmpIdno"] = ddlPayee.SelectedValue;
                row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
            }
            else
            {
                row["IsEmployee"] = "";
                row["TagEmpIdno"] = "";
                row["TagEmployee"] = string.Empty;
            }
            row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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

        //yaha delete hota hai grid se data
        DataRow[] drr = dt.Select("Dld_id>0 and Dld_id<9 and Dld_id<>5");
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
            if ((Convert.ToDouble(dt.Rows[i]["TDSPercentage"].ToString()) > 0))
            {
                if (ViewState["TDS"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "4";
                }
            }

            //Added by gopal anthati on 31-08-2021
            if ((Convert.ToDouble(dt.Rows[i]["TdsOnCgstPer"].ToString()) > 0) || Convert.ToDouble(dt.Rows[i]["TdsOnSgstPer"].ToString()) > 0)
            {
                if (ViewState["IsTdsOnGst"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "6";
                }
            }

            if ((Convert.ToDouble(dt.Rows[i]["TdsOnIgstPer"].ToString()) > 0))
            {
                if (ViewState["IsTdsOnIgst"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "7";
                }
            }
            if ((Convert.ToDouble(dt.Rows[i]["SecurityAmt"].ToString()) > 0))
            {
                if (ViewState["IsSecurity"].ToString() == "Yes")
                {
                    dt.Rows[i]["Dld_id"] = "8";
                }
            }
            //

            if (Convert.ToDouble(dt.Rows[i]["IGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["SGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["CGSTper"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TDSpercentage"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TdsOnCgstPer"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TdsOnSgstPer"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["TdsOnIgstPer"].ToString()) == 0.00 && Convert.ToDouble(dt.Rows[i]["SecurityAmt"].ToString()) == 0.00)
            {
                if (dt.Rows[i]["Mode"].ToString() == "Cr")
                {
                    dt.Rows[i]["Dld_id"] = "5";
                }
                if (dt.Rows[i]["Mode"].ToString() == "Dr")
                {
                    dt.Rows[i]["Dld_id"] = "1";
                }

                dt.Rows[i].AcceptChanges();
            }
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

            row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
            row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
            row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
            row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

            row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
            row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
            row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
            row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

            row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
            row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
            row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
            row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();

            row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();

            row["IsEmployee"] = dtRec.Rows[i]["IsEmployee"].ToString().Trim();
            row["TagEmpIdno"] = dtRec.Rows[i]["TagEmpIdno"].ToString().Trim();
            row["TagEmployee"] = dtRec.Rows[i]["TagEmployee"].ToString().Trim();
            row["PrevAdvanceId"] = dtRec.Rows[i]["PrevAdvanceId"].ToString().Trim();

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
                    txtinvoiceNo.Text = dtContain.Rows[0]["INVOICE_NO"].ToString().Trim();
                    txtinvoicedate.Text = dtContain.Rows[0]["INVOICE_DATE"].ToString().Trim();
                    if (ddlPaymentMode.Items.Count == 0)
                    {
                        objCommon.FillDropDownList(ddlPaymentMode, "ACC_PAYMODE", "PAYMODE_CODE", "PAYMODE", "", "PAYMODE");
                    }
                    ddlPaymentMode.SelectedValue = dtContain.Rows[0]["PAYMENT_MODE"].ToString();

                    ddlBudgetHead.SelectedValue = dtContain.Rows[0]["BudgetNo"].ToString();
                    //objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
                    ddldepartment.SelectedValue = dtContain.Rows[0]["DEPT_ID"].ToString();


                    txtPartyName.Text = dtContain.Rows[0]["ACC_PARTY_NAME"].ToString();
                    txtPanNo.Text = dtContain.Rows[0]["PAN_NO"].ToString();
                    txtNatureService.Text = dtContain.Rows[0]["NATURE_SERVICE"].ToString();

                    //Added by gopal anthati on 31-08-2021

                    ViewState["IsSecurity"] = dtContain.Rows[0]["IsSecurity"].ToString() == "1" ? "Yes" : "No";
                    ViewState["IsTdsOnGst"] = dtContain.Rows[0]["IsTdsOnGst"].ToString() == "1" ? "Yes" : "No";
                    ViewState["IsTdsOnIgst"] = dtContain.Rows[0]["IsTdsOnIgst"].ToString() == "1" ? "Yes" : "No";


                    //if (Convert.ToInt32(ViewState["Isbudget"].ToString()) >0)
                    //{
                    //   trBudgetHead.Visible = true;
                    //    divDeptBudget.Visible = true;
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
                txtinvoicedate.Text = dtRec.Rows[i]["INVOICE_DATE"].ToString().Trim() == string.Empty ? DateTime.Now.Date.ToString("dd/MM/yyyy") : dtContain.Rows[0]["INVOICE_DATE"].ToString().Trim();
                txtinvoiceNo.Text = dtRec.Rows[i]["INVOICE_NO"].ToString().Trim();
                ddlBillNo.SelectedValue = dtRec.Rows[i]["BILL_ID"].ToString().Trim();
                ddlSponsor.SelectedValue = dtRec.Rows[i]["ProjectId"].ToString().Trim();
                objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
                ddlProjSubHead.SelectedValue = dtRec.Rows[i]["ProjectSubId"].ToString().Trim();
                ViewState["EditBal"] = dtContain.Rows[0]["BALANCE"].ToString().Trim();
                int TDS = Convert.ToInt32(dtContain.Rows[0]["TDS"].ToString().Trim());
                ViewState["IsIGST"] = dtContain.Rows[0]["IsIGSTApplicable"].ToString() == "1" ? "Yes" : "No";
                ViewState["IsGST"] = dtContain.Rows[0]["IsSGST"].ToString() == "1" ? "Yes" : "No";
                ViewState["Isbudget"] = dtContain.Rows[0]["BudgetNo"].ToString();
                txtGSTNNO.Text = dtContain.Rows[0]["GSTIN_NO"].ToString();
                txtinvoiceNo.Text = dtContain.Rows[0]["INVOICE_NO"].ToString().Trim();
                txtinvoicedate.Text = dtContain.Rows[0]["INVOICE_DATE"].ToString().Trim();
                
                if (ddlPaymentMode.Items.Count == 0)
                {
                    objCommon.FillDropDownList(ddlPaymentMode, "ACC_PAYMODE", "PAYMODE_CODE", "PAYMODE", "", "PAYMODE");
                    ddlPaymentMode.SelectedValue = dtContain.Rows[0]["PAYMENT_MODE"].ToString();
                }


                ddlBudgetHead.SelectedValue = dtContain.Rows[0]["BudgetNo"].ToString();
                // objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
                ddldepartment.SelectedValue = dtContain.Rows[0]["DEPT_ID"].ToString();

                txtPartyName.Text = dtContain.Rows[0]["ACC_PARTY_NAME"].ToString();
                txtPanNo.Text = dtContain.Rows[0]["PAN_NO"].ToString();
                txtNatureService.Text = dtContain.Rows[0]["NATURE_SERVICE"].ToString();

                //Added by gopal anthati on 31-08-2021

                ViewState["IsSecurity"] = dtContain.Rows[0]["IsSecurity"].ToString() == "1" ? "Yes" : "No";
                ViewState["IsTdsOnGst"] = dtContain.Rows[0]["IsTdsOnGst"].ToString() == "1" ? "Yes" : "No";
                ViewState["IsTdsOnIgst"] = dtContain.Rows[0]["IsTdsOnIgst"].ToString() == "1" ? "Yes" : "No";
                //
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
                    rowgrid.Visible = true;
                    //uncomment by ShirkantA on 20-12-2020
                    AddTotalAmount();
                }
                return;
            }
            //Added by gopal anthati on 31-08-2021
            int TdsCount = 0;
            if (ViewState["TDS"].ToString() == "Yes")
            {
                UpdateAllTdsRows(dtContain, dtRec, i, TdsCount);
                TdsCount = 1;
            }
            if (ViewState["IsTdsOnGst"].ToString() == "Yes")
            {
                UpdateAllTdsRows(dtContain, dtRec, i, TdsCount);
                TdsCount = 2;
            }
            if (ViewState["IsTdsOnIgst"].ToString() == "Yes")
            {
                UpdateAllTdsRows(dtContain, dtRec, i, TdsCount);
                TdsCount = 3;
            }
            if (ViewState["IsSecurity"].ToString() == "Yes")
            {
                UpdateAllTdsRows(dtContain, dtRec, i, TdsCount);
                TdsCount = 4;
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

                    row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
                    row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
                    row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
                    row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

                    row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
                    row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
                    row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
                    row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

                    row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
                    row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
                    row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
                    row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();

                    row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();


                    row["IsEmployee"] = dtRec.Rows[i]["IsEmployee"].ToString().Trim();
                    row["TagEmpIdno"] = dtRec.Rows[i]["TagEmpIdno"].ToString().Trim();
                    row["TagEmployee"] = dtRec.Rows[i]["TagEmployee"].ToString().Trim();
                    row["PrevAdvanceId"] = dtRec.Rows[i]["PrevAdvanceId"].ToString().Trim();

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
                rowgrid.Visible = true;
                AddTotalAmount();
            }
        }

    }

    private void UpdateAllTdsRows(System.Data.DataTable dtContain, System.Data.DataTable dtRec, int i, int Count)
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
        //Added/Modified by gopal anthati on 31-08-2021     
        if (ViewState["TDS"].ToString() == "Yes" && Count != 1)
        {
            //Added by vijay andoju on 08-07-2020 
            row["TDSsection"] = dtRec.Rows[i]["TDSSection"].ToString().Trim();
            row["TDSAMOUNT"] = dtRec.Rows[i]["TDSAmount"].ToString().Trim();
            row["TDSpercentage"] = dtRec.Rows[i]["TDSPercent"].ToString().Trim();
            dt.Rows.Add(row);
        }
        if (ViewState["IsTdsOnGst"].ToString() == "Yes" && Count != 2)
        {
            row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
            row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
            row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
            row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

            row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
            row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
            row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
            row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

            dt.Rows.Add(row);
        }
        if (ViewState["IsTdsOnIgst"].ToString() == "Yes" && Count != 3)
        {
            row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
            row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
            row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
            row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();
            dt.Rows.Add(row);
        }
        if (ViewState["IsSecurity"].ToString() == "Yes" && Count != 4)
        {
            row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();
            dt.Rows.Add(row);
        }
        //
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
        objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "a.ProjectId=" + dtContain.Rows[0]["ProjectId"].ToString().Trim(), "");
        ddlProjSubHead.SelectedValue = dtRec.Rows[i]["ProjectSubId"].ToString().Trim();
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

                row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
                row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
                row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
                row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

                row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
                row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
                row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
                row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

                row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
                row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
                row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
                row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();

                row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();


                row["IsEmployee"] = dtRec.Rows[i]["IsEmployee"].ToString().Trim();
                row["TagEmpIdno"] = dtRec.Rows[i]["TagEmpIdno"].ToString().Trim();
                row["TagEmployee"] = dtRec.Rows[i]["TagEmployee"].ToString().Trim();
                row["PrevAdvanceId"] = dtRec.Rows[i]["PrevAdvanceId"].ToString().Trim();

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

                //added by gopal Anthati on 31-08-2021 

                row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
                row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
                row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
                row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

                row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
                row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
                row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
                row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

                row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
                row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
                row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
                row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();

                row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();

                row["IsEmployee"] = dtRec.Rows[i]["IsEmployee"].ToString().Trim();
                row["TagEmpIdno"] = dtRec.Rows[i]["TagEmpIdno"].ToString().Trim();
                row["TagEmployee"] = dtRec.Rows[i]["TagEmployee"].ToString().Trim();
                row["PrevAdvanceId"] = dtRec.Rows[i]["PrevAdvanceId"].ToString().Trim();

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

                //added by gopal Anthati on 31-08-2021 

                row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
                row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
                row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
                row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

                row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
                row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
                row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
                row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

                row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
                row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
                row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
                row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();

                row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();

                row["IsEmployee"] = dtRec.Rows[i]["IsEmployee"].ToString().Trim();
                row["TagEmpIdno"] = dtRec.Rows[i]["TagEmpIdno"].ToString().Trim();
                row["TagEmployee"] = dtRec.Rows[i]["TagEmployee"].ToString().Trim();
                row["PrevAdvanceId"] = dtRec.Rows[i]["PrevAdvanceId"].ToString().Trim();

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

                //added by gopal Anthati on 31-08-2021 

                row["TdsOnCgstSection"] = dtRec.Rows[i]["TdsOnCgstSection"].ToString().Trim();
                row["TdsOnCgstAmt"] = dtRec.Rows[i]["TdsOnCgstAmt"].ToString().Trim();
                row["TdsOnCgstPer"] = dtRec.Rows[i]["TdsOnCgstPer"].ToString().Trim();
                row["TdsCgstOnAmt"] = dtRec.Rows[i]["TdsCgstOnAmt"].ToString().Trim();

                row["TdsOnSgstSection"] = dtRec.Rows[i]["TdsOnSgstSection"].ToString().Trim();
                row["TdsOnSgstAmt"] = dtRec.Rows[i]["TdsOnSgstAmt"].ToString().Trim();
                row["TdsOnSgstPer"] = dtRec.Rows[i]["TdsOnSgstPer"].ToString().Trim();
                row["TdsSgstOnAmt"] = dtRec.Rows[i]["TdsSgstOnAmt"].ToString().Trim();

                row["TdsOnIgstSection"] = dtRec.Rows[i]["TdsOnIgstSection"].ToString().Trim();
                row["TdsOnIgstAmt"] = dtRec.Rows[i]["TdsOnIgstAmt"].ToString().Trim();
                row["TdsOnIgstPer"] = dtRec.Rows[i]["TdsOnIgstPer"].ToString().Trim();
                row["TdsIgstOnAmt"] = dtRec.Rows[i]["TdsIgstOnAmt"].ToString().Trim();

                row["SecurityAmt"] = dtRec.Rows[i]["SecurityAmt"].ToString().Trim();


                row["IsEmployee"] = dtRec.Rows[i]["IsEmployee"].ToString().Trim();
                row["TagEmpIdno"] = dtRec.Rows[i]["TagEmpIdno"].ToString().Trim();
                row["TagEmployee"] = dtRec.Rows[i]["TagEmployee"].ToString().Trim();
                row["PrevAdvanceId"] = dtRec.Rows[i]["PrevAdvanceId"].ToString().Trim();

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
            ddlTranType.Enabled = true;
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

                //Tds On CGst Added by Gopal Anthati on 31-08-2021 
                DataColumn dc30 = new DataColumn();
                dc30.ColumnName = "TdsOnCgstSection";
                dt.Columns.Add(dc30);

                DataColumn dc31 = new DataColumn();
                dc31.ColumnName = "TdsOnCgstAmt";
                dt.Columns.Add(dc31);

                DataColumn dc32 = new DataColumn();
                dc32.ColumnName = "TdsCgstOnAmt";
                dt.Columns.Add(dc32);

                DataColumn dc33 = new DataColumn();
                dc33.ColumnName = "TdsOnCgstPer";
                dt.Columns.Add(dc33);

                //Tds On SGst
                DataColumn dc34 = new DataColumn();
                dc34.ColumnName = "TdsOnSgstSection";
                dt.Columns.Add(dc34);

                DataColumn dc35 = new DataColumn();
                dc35.ColumnName = "TdsOnSgstAmt";
                dt.Columns.Add(dc35);

                DataColumn dc36 = new DataColumn();
                dc36.ColumnName = "TdsSgstOnAmt";
                dt.Columns.Add(dc36);

                DataColumn dc37 = new DataColumn();
                dc37.ColumnName = "TdsOnSgstPer";
                dt.Columns.Add(dc37);

                //Tds On IGst
                DataColumn dc38 = new DataColumn();
                dc38.ColumnName = "TdsOnIgstSection";
                dt.Columns.Add(dc38);

                DataColumn dc39 = new DataColumn();
                dc39.ColumnName = "TdsOnIgstAmt";
                dt.Columns.Add(dc39);

                DataColumn dc40 = new DataColumn();
                dc40.ColumnName = "TdsIgstOnAmt";
                dt.Columns.Add(dc40);

                DataColumn dc41 = new DataColumn();
                dc41.ColumnName = "TdsOnIgstPer";
                dt.Columns.Add(dc41);

                //Security
                DataColumn dc42 = new DataColumn();
                dc42.ColumnName = "SecurityAmt";
                dt.Columns.Add(dc42);

                DataColumn dc43 = new DataColumn();
                dc43.ColumnName = "IsEmployee";
                dt.Columns.Add(dc43);

                DataColumn dc44 = new DataColumn();
                dc44.ColumnName = "TagEmpIdno";
                dt.Columns.Add(dc44);

                DataColumn dc45 = new DataColumn();
                dc45.ColumnName = "TagEmployee";
                dt.Columns.Add(dc45);

                DataColumn dc46 = new DataColumn();
                dc46.ColumnName = "PrevAdvanceId";
                dt.Columns.Add(dc46);

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

                        //Tds On CGst Added by Gopal Anthati on 31-08-2021 
                        DataColumn dc30 = new DataColumn();
                        dc30.ColumnName = "TdsOnCgstSection";
                        dt.Columns.Add(dc30);

                        DataColumn dc31 = new DataColumn();
                        dc31.ColumnName = "TdsOnCgstAmt";
                        dt.Columns.Add(dc31);

                        DataColumn dc32 = new DataColumn();
                        dc32.ColumnName = "TdsCgstOnAmt";
                        dt.Columns.Add(dc32);

                        DataColumn dc33 = new DataColumn();
                        dc33.ColumnName = "TdsOnCgstPer";
                        dt.Columns.Add(dc33);

                        //Tds On SGst
                        DataColumn dc34 = new DataColumn();
                        dc34.ColumnName = "TdsOnSgstSection";
                        dt.Columns.Add(dc34);

                        DataColumn dc35 = new DataColumn();
                        dc35.ColumnName = "TdsOnSgstAmt";
                        dt.Columns.Add(dc35);

                        DataColumn dc36 = new DataColumn();
                        dc36.ColumnName = "TdsSgstOnAmt";
                        dt.Columns.Add(dc36);

                        DataColumn dc37 = new DataColumn();
                        dc37.ColumnName = "TdsOnSgstPer";
                        dt.Columns.Add(dc37);

                        //Tds On IGst
                        DataColumn dc38 = new DataColumn();
                        dc38.ColumnName = "TdsOnIgstSection";
                        dt.Columns.Add(dc38);

                        DataColumn dc39 = new DataColumn();
                        dc39.ColumnName = "TdsOnIgstAmt";
                        dt.Columns.Add(dc39);

                        DataColumn dc40 = new DataColumn();
                        dc40.ColumnName = "TdsIgstOnAmt";
                        dt.Columns.Add(dc40);

                        DataColumn dc41 = new DataColumn();
                        dc41.ColumnName = "TdsOnIgstPer";
                        dt.Columns.Add(dc41);

                        //Security
                        DataColumn dc42 = new DataColumn();
                        dc42.ColumnName = "SecurityAmt";
                        dt.Columns.Add(dc42);

                        DataColumn dc43 = new DataColumn();
                        dc43.ColumnName = "IsEmployee";
                        dt.Columns.Add(dc43);

                        DataColumn dc44 = new DataColumn();
                        dc44.ColumnName = "TagEmpIdno";
                        dt.Columns.Add(dc44);

                        DataColumn dc45 = new DataColumn();
                        dc45.ColumnName = "TagEmployee";
                        dt.Columns.Add(dc45);

                        DataColumn dc46 = new DataColumn();
                        dc46.ColumnName = "PrevAdvanceId";
                        dt.Columns.Add(dc46);

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
                    chkIGST.Visible = true;
                    chkGST.Visible = true;

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

        OnOffBudgetHeadDept();

        //if (isSingleMode == "Y")
        //    txtAgainstAcc.Focus();
        //else
        //    txtAcc.Focus();

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
        if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "J")
        {
            txtinvoicedate.Text = txtDate.Text;
        }
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
            rowgrid.Visible = true;
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
            rowgrid.Visible = false;
        }
        txtTranAmt.Text = "";
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

    // Added by Pawan Nikhare
    public void getGstNo(string code)
    {
        //string gstno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "GSTNO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1] + "'");
        string gstno =objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "GSTNO", "ACC_CODE='" + code + "'");  
        if (txtGSTNNO.Text==string.Empty)
        {
        txtGSTNNO.Text = gstno;
        }
    }
    //end

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //led[1] = led[1].ToString().Replace("¯", "");
        rowgrid.Visible = true;

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

             // Added by Pawan Nikhare         
            getGstNo(txtAcc.Text.Trim().Split('*')[1]);

            //end

            if (ddlTranType.SelectedValue != "J")
                hdnPartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAgainstAcc.Text.Trim().Split('*')[1] + "'");
                hdnOpartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.Trim().Split('*')[1] + "'");

            //}
            //else
            //{
            //    hdnPartyManual.Value = txtAgainstAcc.Text.Trim().Split('*')[0];
            //}




             if (Convert.ToDouble(txtTranAmt.Text) == 0.00 || txtTranAmt.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Enter Transaction Amount", this.Page);
                return;
            }

            if (ddlEmpType.SelectedValue == "1")
            {
                if (ddlEmployee.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Employee.", this);
                    return;
                }
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                if (ddlPayeeNature.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Payee Nature.", this);
                    return;
                }
                else if (ddlPayeeNature.SelectedIndex > 0 && ddlPayee.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Payee Name.", this);
                    return;
                }
            }

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
                    if (txtTDSPer.Text == "0" || txtTDSPer.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter TDS Percentage 2", this);
                        txtTDSPer.Focus();
                        return;
                    }

                    if (txtTDSAmount.Text == "0" || txtTDSAmount.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "TDS Amount Cant't Be Empty!", this);
                        txtTDSAmount.Focus();
                        return;
                    }
                }
                if (chkTdsOnGST.Checked == true)
                {
                    if (txtTdsOnCgstAcc.Text == string.Empty || txtTdsOnCgstAcc.Text == "")
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter TDS On GST Account", this);
                        txtTdsOnCgstAcc.Focus();
                        return;
                    }
                    if (txtTdsOnCgstPer.Text == "0" || txtTdsOnCgstPer.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter Percentage For TDS On GST", this);
                        txtTdsOnCgstPer.Focus();
                        return;
                    }

                    if (txtTdsOnCgstAmt.Text == "0" || txtTdsOnCgstAmt.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "TDS On GST Amount Cant't Be Empty!", this);
                        txtTdsOnCgstAmt.Focus();
                        return;
                    }
                }
                if (chkTdsOnIGST.Checked == true)
                {
                    if (txtTdsOnIgstAcc.Text == string.Empty || txtTdsOnIgstAcc.Text == "")
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please Enter TDS On IGST Account", this);
                        txtTdsOnIgstAcc.Focus();
                        return;
                    }
                    if (txtTdsOnIgstPer.Text == "0" || txtTdsOnIgstPer.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter Percentage For TDS On IGST", this);
                        txtTdsOnIgstPer.Focus();
                        return;
                    }

                    if (txtTdsOnIgstAmt.Text == "0" || txtTdsOnIgstAmt.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "TDS On GST Amount Cant't Be Empty!", this);
                        txtTdsOnIgstAmt.Focus();
                        return;
                    }
                }
                if (chkSecurity.Checked == true)
                {
                    if (txtSecurityAmt.Text == "0" || txtSecurityAmt.Text == "")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Enter Security Amount", this);
                        txtSecurityAmt.Focus();
                        return;
                    }
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
                    rowgrid.Visible = true;

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
                    rowgrid.Visible = false;
                }
                txtTranAmt.Text = "";
                txtAcc.Text = "";
                txtPerNarration.Text = "";
                //txtChequeDt1.Text = "";
                //txtChqNo1.Text = "";
                lblCurBal2.Text = "";
                ddlProjSubHead.SelectedValue = "0";
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
            //trBudgetHead.Visible = false;
            // divDeptBudget.Visible = false;
            ddlcrdr.Enabled = true;
            lblCrDr2.Text = string.Empty;
            txtTDSPer.Text = string.Empty;
            txtTDSLedger.Text = string.Empty;
            txtTDSAmount.Text = string.Empty;
            chkTDSApplicable.Checked = false;
            dvTDS.Visible = false;
            chkGST.Checked = false;
            chkIGST.Checked = false;
            txtIGST.Text = txtIgstOnAmount.Text = txtIGSTAMT.Text = txtIGSTPER.Text = string.Empty;
            txtCGST.Text = txtCgstOnAmount.Text = txtCGSTAMT.Text = txtCgstPer.Text = string.Empty;
            txtSGST.Text = txtSgstOnAmount.Text = txtSGSTAMT.Text = txtSGTSPer.Text = string.Empty;
            divgst.Visible = false;
            divIgst.Visible = false;
            divcgst.Visible = false;

            //Added by gopal anthati on 01-09-2021
            txtTdsOnCgstAcc.Text = txtTdsCgstOnAmt.Text = txtTdsOnCgstAmt.Text = txtTdsOnCgstPer.Text = string.Empty;
            txtTdsOnSgstAcc.Text = txtTdsSgstOnAmt.Text = txtTdsOnSgstAmt.Text = txtTdsOnSgstPer.Text = string.Empty;
            txtTdsOnIgstAcc.Text = txtTdsIgstOnAmt.Text = txtTdsOnIgstAmt.Text = txtTdsOnIgstPer.Text = string.Empty;
            txtSecurityAmt.Text = txtSecurityAcc.Text = string.Empty;

            divTdsOnCgst.Visible = false;
            divTdsOnSgst.Visible = false;
            divTdsOnIgst.Visible = false;
            divSecurity.Visible = false;
            chkTdsOnGST.Checked = false;
            chkTdsOnIGST.Checked = false;
            chkSecurity.Checked = false;

            ddlEmpType.SelectedIndex = 0;
            ddlEmployee.SelectedIndex = 0;
            ddlPayee.SelectedIndex = 0;
            ddlPayeeNature.SelectedIndex = 0;
            divEmployee1.Visible = false;
            divEmployee2.Visible = false;

            divPayeeNature1.Visible = false;
            divPayeeNature2.Visible = false;

            divPayee1.Visible = false;
            divPayee2.Visible = false;

            divPrevAdv1.Visible = false;
            ddlPrevAdv.SelectedIndex = 0;
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
                    GridData.Columns[4].Visible = true;
                    GridData.Columns[3].Visible = false;
                    GridData.Columns[5].Visible = false;
                    GridData.Columns[6].Visible = false;
                    if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Payment")
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
                    GridData.Columns[3].Visible = false;
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

                    lblTotalDiff.Text = (TotDr - TotCr).ToString();
                }
            }
            lblTotal.Text = String.Format("{0:0.00}", Convert.ToDouble(Tot.ToString().Trim()));
            lblTotalDebit.Text = String.Format("{0:0.00}", Convert.ToDouble(TotDr.ToString().Trim()));
            lblTotalCredit.Text = String.Format("{0:0.00}", Convert.ToDouble(TotCr.ToString().Trim()));
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
                    GridData.Columns[4].Visible = true;
                    GridData.Columns[5].Visible = false;
                    GridData.Columns[6].Visible = false;
                    if (ddlTranType.SelectedItem.Text.ToString().Trim() == "Payment")
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
        //txtAcc.Text = GridData.Rows[e.NewSelectedIndex].Cells[1].Text;
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
                txtTranAmt.Text = OpLedger[0]["Amount"].ToString() == "0.00" ? OpLedger[0]["Credit"].ToString() : OpLedger[0]["Amount"].ToString();
                ddlcrdr.SelectedValue = OpLedger[0]["Mode"].ToString();

                for (int i = 0; i < OpLedger.Length; i++)
                    dt.Rows.Remove(OpLedger[i]);
                dt.AcceptChanges();

                AddTotalAmountJurnal(txtTranAmt.Text);

                return;
            }
        }

        ViewState["RowIndex"] = e.NewSelectedIndex;

        DataRow[] LedgerRow = dt.Select("SGSTPER=0.00 AND CGSTPER=0.00 AND IGSTPER=0.00 AND TDSpercentage=0.00 AND Dld_id=1");
        // DataRow[] LedgerRow = dt.Select("Particulars like'%CONSUMABLES%'");
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
                txtTranAmt.Text = Convert.ToDouble(LedgerRow[0]["Amount"].ToString()).ToString();// GridData.Rows[e.NewSelectedIndex].Cells[4].Text;
            }
            else
            {
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    txtTranAmt.Text = LedgerRow[0]["Credit"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[6].Text;

                }
                else
                {
                    txtTranAmt.Text = LedgerRow[0]["Debit"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[5].Text;

                }

            }

            txtAcc.Text = LedgerRow[0]["Particulars"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[1].Text.Replace("&amp;", "&");
            ViewState["TDSONAMOUNT"] = txtTranAmt.Text;
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
                    // TDSamount = Convert.ToDouble(TDSROW[0]["Amount"].ToString());
                    TDSamount = Convert.ToDouble(ViewState["TDSONAMOUNT"].ToString());
                    TdsTamount = Convert.ToDouble(TDSROW[0]["TDSAMOUNT"].ToString());
                    TDSPer = Convert.ToDouble(TDSROW[0]["TDSpercentage"].ToString());
                    section = Convert.ToInt32(TDSROW[0]["TDSsection"].ToString());

                }
            }
            else
            {

                DataRow[] TDSROW = dt.Select("Particulars like '%TDS%'");
                if (TDSROW.Length > 0)
                {
                    txtTDSLedger.Text = TDSROW[0]["Particulars"].ToString();
                    //  TDSamount = Convert.ToDouble(TDSROW[0]["Amount"].ToString());
                    TDSamount = Convert.ToDouble(ViewState["TDSONAMOUNT"].ToString());
                    TdsTamount = Convert.ToDouble(TDSROW[0]["TDSAMOUNT"].ToString());
                    TDSPer = Convert.ToDouble(TDSROW[0]["TDSpercentage"].ToString());
                    section = Convert.ToInt32(TDSROW[0]["TDSsection"].ToString());

                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }


            txtTDSPer.Text = TDSPer.ToString();
            txtTDSAmount.Text = TdsTamount.ToString();
            txtTdsOnAmount.Text = TDSamount.ToString();
            ddlTdsSection.SelectedValue = section.ToString();
            dvTDS.Visible = true;
        }

        if (ViewState["IsIGST"].ToString() == "Yes")
        {
            double Amount = 0;
            chkIGST.Checked = true;

            if (isSingleMode == "Y")
            {
                DataRow[] IGSTROW = dt.Select("Particulars like '%IGST%'");
                if (IGSTROW.Length > 0)
                {
                    txtIGST.Text = IGSTROW[0]["Particulars"].ToString();
                    txtIgstOnAmount.Text = Convert.ToDouble(IGSTROW[0]["IGSTonAmount"].ToString()).ToString();
                    txtIGSTPER.Text = Convert.ToDouble(IGSTROW[0]["IGSTper"].ToString()).ToString();
                    txtIGSTAMT.Text = Convert.ToDouble(IGSTROW[0]["IGSTAmount"].ToString()).ToString();
                }
            }
            else
            {
                DataRow[] IGSTROW = dt.Select("Particulars like '%IGST%'");
                if (IGSTROW.Length > 0)
                {
                    txtIGST.Text = IGSTROW[0]["Particulars"].ToString();
                    txtIgstOnAmount.Text = Convert.ToDouble(IGSTROW[0]["IGSTonAmount"].ToString()).ToString();
                    txtIGSTPER.Text = Convert.ToDouble(IGSTROW[0]["IGSTper"].ToString()).ToString();
                    txtIGSTAMT.Text = Convert.ToDouble(IGSTROW[0]["IGSTAmount"].ToString()).ToString();
                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }
            divIgst.Visible = true;
        }
        if (ViewState["IsGST"].ToString() == "Yes")
        {

            chkGST.Checked = true;
            double IGSTamount = 0, IGSTTamount = 0;
            double Amount = 0; double IGTSPer = 0;
            // int section = 0;

            if (isSingleMode == "Y")
            {
                DataRow[] RowSGST = dt.Select("Particulars like '%SGST%'");
                if (RowSGST.Length > 0)
                {
                    txtSGST.Text = RowSGST[0]["Particulars"].ToString();
                    txtSgstOnAmount.Text = Convert.ToDouble(RowSGST[0]["SGSTonAmount"].ToString()).ToString();
                    txtSGTSPer.Text = Convert.ToDouble(RowSGST[0]["SGSTper"].ToString()).ToString();
                    txtSGSTAMT.Text = Convert.ToDouble(RowSGST[0]["SGSTAmount"].ToString()).ToString();
                }


                DataRow[] RowCGST = dt.Select("Particulars like '%CGST%'");
                if (RowCGST.Length > 0)
                {
                    txtCGST.Text = RowCGST[0]["Particulars"].ToString();
                    txtCgstOnAmount.Text = Convert.ToDouble(RowCGST[0]["CGSTonAmount"].ToString()).ToString();
                    txtCgstPer.Text = Convert.ToDouble(RowCGST[0]["CGSTper"].ToString()).ToString();
                    txtCGSTAMT.Text = Convert.ToDouble(RowCGST[0]["CGSTAmount"].ToString()).ToString();
                }

            }
            else
            {
                DataRow[] RowSGST = dt.Select("Particulars like '%SGST%'");
                if (RowSGST.Length > 0)
                {
                    txtSGST.Text = RowSGST[0]["Particulars"].ToString();
                    txtSgstOnAmount.Text = Convert.ToDouble(RowSGST[0]["SGSTonAmount"].ToString()).ToString();
                    txtSGTSPer.Text = Convert.ToDouble(RowSGST[0]["SGSTper"].ToString()).ToString();
                    txtSGSTAMT.Text = Convert.ToDouble(RowSGST[0]["SGSTAmount"].ToString()).ToString();
                }


                DataRow[] RowCGST = dt.Select("Particulars like '%CGST%'");
                if (RowCGST.Length > 0)
                {
                    txtCGST.Text = RowCGST[0]["Particulars"].ToString();
                    txtCgstOnAmount.Text = Convert.ToDouble(RowCGST[0]["CGSTonAmount"].ToString()).ToString();
                    txtCgstPer.Text = Convert.ToDouble(RowCGST[0]["CGSTper"].ToString()).ToString();
                    txtCGSTAMT.Text = Convert.ToDouble(RowCGST[0]["CGSTAmount"].ToString()).ToString();
                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }
            divgst.Visible = true;
            divcgst.Visible = true;
        }

        ///Added by Gopal anthati on 13-09-2021
        if (ViewState["IsTdsOnGst"].ToString() == "Yes")
        {

            chkTdsOnGST.Checked = true;
            double Amount = 0;


            if (isSingleMode == "Y")
            {
                DataRow[] TdsOnCgst = dt.Select("Particulars like '%TDS%'");
                if (TdsOnCgst.Length > 0)//&& TdsOnCgst[0]["TdsOnCgstSection"].ToString() == "1"
                {
                    for (int i = 0; i < TdsOnCgst.Length; i++)
                    {
                        if (TdsOnCgst[i]["TdsOnCgstSection"].ToString() != "0")
                        {
                            txtTdsOnCgstAcc.Text = TdsOnCgst[i]["Particulars"].ToString();
                            txtTdsCgstOnAmt.Text = Convert.ToDouble(TdsOnCgst[i]["TdsCgstOnAmt"].ToString()).ToString();
                            txtTdsOnCgstPer.Text = Convert.ToDouble(TdsOnCgst[i]["TdsOnCgstPer"].ToString()).ToString();
                            txtTdsOnCgstAmt.Text = Convert.ToDouble(TdsOnCgst[i]["TdsOnCgstAmt"].ToString()).ToString();
                            ddlTdsOnCgstSection.SelectedValue = TdsOnCgst[i]["TdsOnCgstSection"].ToString();
                        }
                    }
                }

                DataRow[] TdsOnSgst = dt.Select("Particulars like '%TDS%'");
                if (TdsOnSgst.Length > 0)// && TdsOnSgst[1]["TdsOnSgstSection"].ToString() == "1"
                {
                    for (int i = 0; i < TdsOnSgst.Length; i++)
                    {
                        if (TdsOnSgst[i]["TdsOnSgstSection"].ToString() != "0")
                        {
                            txtTdsOnSgstAcc.Text = TdsOnSgst[i]["Particulars"].ToString();
                            txtTdsSgstOnAmt.Text = Convert.ToDouble(TdsOnSgst[i]["TdsSgstOnAmt"].ToString()).ToString();
                            txtTdsOnSgstPer.Text = Convert.ToDouble(TdsOnSgst[i]["TdsOnSgstPer"].ToString()).ToString();
                            txtTdsOnSgstAmt.Text = Convert.ToDouble(TdsOnSgst[i]["TdsOnSgstAmt"].ToString()).ToString();
                            ddlTdsOnSgstSection.SelectedValue = TdsOnSgst[i]["TdsOnSgstSection"].ToString();
                        }
                    }
                }

            }
            else
            {

                DataRow[] TdsOnCgst = dt.Select("Particulars like '%TDS%'");
                if (TdsOnCgst.Length > 0)//&& TdsOnCgst[0]["TdsOnCgstSection"].ToString() == "1"
                {
                    for (int i = 0; i < TdsOnCgst.Length; i++)
                    {
                        if (TdsOnCgst[i]["TdsOnCgstSection"].ToString() != "0")
                        {
                            txtTdsOnCgstAcc.Text = TdsOnCgst[i]["Particulars"].ToString();
                            txtTdsCgstOnAmt.Text = Convert.ToDouble(TdsOnCgst[i]["TdsCgstOnAmt"].ToString()).ToString();
                            txtTdsOnCgstPer.Text = Convert.ToDouble(TdsOnCgst[i]["TdsOnCgstPer"].ToString()).ToString();
                            txtTdsOnCgstAmt.Text = Convert.ToDouble(TdsOnCgst[i]["TdsOnCgstAmt"].ToString()).ToString();
                            ddlTdsOnCgstSection.SelectedValue = TdsOnCgst[i]["TdsOnCgstSection"].ToString();
                        }
                    }

                }
                DataRow[] TdsOnSgst = dt.Select("Particulars like '%TDS%'");
                if (TdsOnSgst.Length > 0)//&& TdsOnSgst[1]["TdsOnSgstSection"].ToString() == "1"
                {
                    for (int i = 0; i < TdsOnSgst.Length; i++)
                    {
                        if (TdsOnSgst[i]["TdsOnSgstSection"].ToString() != "0")
                        {
                            txtTdsOnSgstAcc.Text = TdsOnSgst[i]["Particulars"].ToString();
                            txtTdsSgstOnAmt.Text = Convert.ToDouble(TdsOnSgst[i]["TdsSgstOnAmt"].ToString()).ToString();
                            txtTdsOnSgstPer.Text = Convert.ToDouble(TdsOnSgst[i]["TdsOnSgstPer"].ToString()).ToString();
                            txtTdsOnSgstAmt.Text = Convert.ToDouble(TdsOnSgst[i]["TdsOnSgstAmt"].ToString()).ToString();
                            ddlTdsOnSgstSection.SelectedValue = TdsOnSgst[i]["TdsOnSgstSection"].ToString();
                        }
                    }
                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }
            divTdsOnSgst.Visible = true;
            divTdsOnCgst.Visible = true;
        }

        if (ViewState["IsTdsOnIgst"].ToString() == "Yes")
        {

            //   DataRow[] RowTds=dt.Select("")
            //   txtTDSLedger.Text = GridData.Rows[GridData.Rows.Count - 1].Cells[1].Text.Replace("&amp", "&");
            chkTdsOnIGST.Checked = true;
            double Amount = 0;

            if (isSingleMode == "Y")
            {
                DataRow[] TdsOnIgst = dt.Select("Particulars like '%TDS%'");
                if (TdsOnIgst.Length > 0)
                {
                    for (int i = 0; i < TdsOnIgst.Length; i++)
                    {
                        if (TdsOnIgst[i]["TdsOnIgstSection"].ToString() != "0")
                        {
                            txtTdsOnIgstAcc.Text = TdsOnIgst[i]["Particulars"].ToString();
                            txtTdsIgstOnAmt.Text = Convert.ToDouble(TdsOnIgst[i]["TdsIgstOnAmt"].ToString()).ToString();
                            txtTdsOnIgstPer.Text = Convert.ToDouble(TdsOnIgst[i]["TdsOnIgstPer"].ToString()).ToString();
                            txtTdsOnIgstAmt.Text = Convert.ToDouble(TdsOnIgst[i]["TdsOnIgstAmt"].ToString()).ToString();
                            ddlTdsOnIgstSection.SelectedValue = TdsOnIgst[i]["TdsOnIgstSection"].ToString();
                        }
                    }
                }
            }
            else
            {

                DataRow[] TdsOnIgst = dt.Select("Particulars like '%TDS%'");
                if (TdsOnIgst.Length > 0)
                {
                    for (int i = 0; i < TdsOnIgst.Length; i++)
                    {
                        if (TdsOnIgst[i]["TdsOnIgstSection"].ToString() != "0")
                        {
                            txtTdsOnIgstAcc.Text = TdsOnIgst[i]["Particulars"].ToString();
                            txtTdsIgstOnAmt.Text = Convert.ToDouble(TdsOnIgst[i]["TdsIgstOnAmt"].ToString()).ToString();
                            txtTdsOnIgstPer.Text = Convert.ToDouble(TdsOnIgst[i]["TdsOnIgstPer"].ToString()).ToString();
                            txtTdsOnIgstAmt.Text = Convert.ToDouble(TdsOnIgst[i]["TdsOnIgstAmt"].ToString()).ToString();
                            ddlTdsOnIgstSection.SelectedValue = TdsOnIgst[i]["TdsOnIgstSection"].ToString();
                        }
                    }
                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }

            divTdsOnIgst.Visible = true;
        }

        if (ViewState["IsSecurity"].ToString() == "Yes")
        {

            //   DataRow[] RowTds=dt.Select("")
            //   txtTDSLedger.Text = GridData.Rows[GridData.Rows.Count - 1].Cells[1].Text.Replace("&amp", "&");
            chkSecurity.Checked = true;
            double Amount = 0;

            if (isSingleMode == "Y")
            {
                DataRow[] Security = dt.Select("Particulars like '%Security%'");
                if (Security.Length > 0)
                {
                    txtSecurityAcc.Text = Security[0]["Particulars"].ToString();
                    txtSecurityAmt.Text = Convert.ToDouble(Security[0]["SecurityAmt"].ToString()).ToString();

                }
            }
            else
            {

                DataRow[] Security = dt.Select("Particulars like '%Security%'");
                if (Security.Length > 0)
                {
                    txtSecurityAcc.Text = Security[0]["Particulars"].ToString();
                    txtSecurityAmt.Text = Convert.ToDouble(Security[0]["SecurityAmt"].ToString()).ToString();

                }
                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[6].Text);
                }
                else { Amount = Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[5].Text); }

            }

            divSecurity.Visible = true;
        }
        if (dt.Rows.Count == 0)
        {
            return;
        }
        if ((GridData.Rows[e.NewSelectedIndex].Cells[8].Text) != "")
        {
            if ((GridData.Rows[e.NewSelectedIndex].Cells[9].Text) == "1")
            {
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') + '['+ convert(nvarchar(150),EmployeeId) + ']' AS NAME", "IDNO > 0", "FNAME");

                ddlEmpType.SelectedValue = "1";
                ddlEmployee.SelectedValue = Convert.ToInt32(GridData.Rows[e.NewSelectedIndex].Cells[10].Text).ToString();//dt.Rows[0]["TagEmpIdno"].ToString();
                divEmployee1.Visible = true;
                divEmployee2.Visible = true;
                divPayee1.Visible = false;
                divPayee2.Visible = false;
                if (ddlTranType.SelectedValue == "P")
                {
                    isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                    if (isTempVoucher == "Y")
                    {
                        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                        divPrevAdv1.Visible = true;
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                        divPrevAdv1.Visible = true;
                    }
                    //divPrevAdv2.Visible = true;
                }
                else if (ddlTranType.SelectedValue == "R")
                {
                    if (IsTagForRecipt == "Y")
                    {
                        isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                        if (isTempVoucher == "Y")
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                            divPrevAdv1.Visible = true;
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                            divPrevAdv1.Visible = true;
                        }
                    }
                }
                else if (ddlTranType.SelectedValue == "J")
                {
                    if (IsTagForJournal == "Y")
                    {
                        isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                        if (isTempVoucher == "Y")
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='P'", "");
                            divPrevAdv1.Visible = true;
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='P'", "");
                            divPrevAdv1.Visible = true;
                        }
                    }
                }
                if ((GridData.Rows[e.NewSelectedIndex].Cells[11].Text) != "" && Convert.ToInt32((GridData.Rows[e.NewSelectedIndex].Cells[11].Text)) > 0)
                {
                    ddlPrevAdv.SelectedValue = Convert.ToInt32(GridData.Rows[e.NewSelectedIndex].Cells[11].Text).ToString();//dt.Rows[0]["PrevAdvanceId"].ToString();
                    divPrevAdv1.Visible = true;
                }
                else
                {
                    ddlPrevAdv.SelectedIndex = 0;
                    divPrevAdv1.Visible = false;
                }
            }
            else if ((GridData.Rows[e.NewSelectedIndex].Cells[9].Text) == "2")
            {
                objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "NATURE_NAME");
                objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "", "PARTYNAME");
                ddlEmpType.SelectedValue = "2";
                ddlPayee.SelectedValue = Convert.ToInt32(GridData.Rows[e.NewSelectedIndex].Cells[10].Text).ToString();//dt.Rows[0]["TagEmpIdno"].ToString();
                divPayee1.Visible = true;
                divPayee2.Visible = true;
                if (ddlTranType.SelectedValue == "P")
                {
                    isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                    if (isTempVoucher == "Y")
                    {
                        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                        divPrevAdv1.Visible = true;
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                        divPrevAdv1.Visible = true;
                    }
                }
                else if (ddlTranType.SelectedValue == "R")
                {
                    if (IsTagForRecipt == "Y")
                    {
                        isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                        if (isTempVoucher == "Y")
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                            divPrevAdv1.Visible = true;
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                            divPrevAdv1.Visible = true;
                        }
                    }
                }
                else if (ddlTranType.SelectedValue == "J")
                {
                    if (IsTagForJournal == "Y")
                    {
                        isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                        if (isTempVoucher == "Y")
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='P'", "");
                            divPrevAdv1.Visible = true;
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='P'", "");
                            divPrevAdv1.Visible = true;
                        }
                    }
                }
                divPayeeNature1.Visible = true;
                divPayeeNature2.Visible = true;

                divEmployee1.Visible = false;
                divEmployee2.Visible = false;

                if ((GridData.Rows[e.NewSelectedIndex].Cells[11].Text) != "" && Convert.ToInt32((GridData.Rows[e.NewSelectedIndex].Cells[11].Text)) > 0)
                {
                    ddlPrevAdv.SelectedValue = Convert.ToInt32(GridData.Rows[e.NewSelectedIndex].Cells[11].Text).ToString();//dt.Rows[0]["PrevAdvanceId"].ToString();
                    divPrevAdv1.Visible = true;
                }
                else
                {
                    ddlPrevAdv.SelectedIndex = 0;
                    divPrevAdv1.Visible = false;
                }
            }
            else
            {
                divEmployee1.Visible = false;
                divEmployee2.Visible = false;
                divPayee1.Visible = false;
                divPayee2.Visible = false;
                ddlEmployee.SelectedIndex = 0;
                ddlPayee.SelectedIndex = 0;
                ddlEmpType.SelectedIndex = 0;
                ddlPrevAdv.SelectedIndex = 0;
                divPrevAdv1.Visible = false;
            }
        }
        /////
        ///Calculate balance
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
                lblCurbal1.Text = (Convert.ToDouble(lblCurbal1.Text) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
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


        HiddenField hdncn = LedgerRow[0]["ChqNo"] as HiddenField;
        HiddenField hdncd = LedgerRow[0]["ChqDate"] as HiddenField;
        HiddenField hdnCostCenter = LedgerRow[0]["CCID"] as HiddenField;
        HiddenField hdnBudgetHeadId = LedgerRow[0]["BudgetNo"] as HiddenField;

        HiddenField hdnDepartmentId = LedgerRow[0]["DepartmentId"] as HiddenField;



        //Change by Nakul Chawre @12042016 to update Cost Center
        string ISAPPLICABLE = objCommon.LookUp("Acc_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISCCApplicable as int),0) ISCCApplicable", "Party_No=" + ViewState["OldLedgerId"].ToString());
        if (Convert.ToInt32(dt.Rows[0]["CCID"]) > 0 || Convert.ToInt32(ISAPPLICABLE) == 1)
        {
            if (IsMultipalCostCenter == "Y")
            {
                trMultiCostCenter.Visible = true;
               // objCommon.FillDropDownList(ddlmuliicostcenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
                objCommon.FillDropDownList(ddlmuliicostcenter, "Acc_" + Session["comp_code"] + "_CostCenter ACC inner join  Acc_" + Session["comp_code"] + "_CostCategory ACAT on (ACC.Cat_ID=ACAT.Cat_ID)", "isnull(ACC.CC_ID,0) CC_ID", "ACC.CCNAME + ' ( '+ACAT.Category+' ) ' as CCNAME", "", "");
          
            }
            else
            {
                trCostCenter.Visible = true;
                objCommon.FillDropDownList(ddlCostCenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
                ddlCostCenter.SelectedValue = LedgerRow[0]["CCID"].ToString() == "" ? "0" : LedgerRow[0]["CCID"].ToString();
            }
            // trCostCenter.Visible = true;
            // objCommon.FillDropDownList(ddlCostCenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
            //ddlCostCenter.SelectedValue = hdnCostCenter.Value == "" ? "0" : hdnCostCenter.Value;
            //ddlCostCenter.SelectedValue = LedgerRow[0]["CCID"].ToString() == "" ? "0" : LedgerRow[0]["CCID"].ToString();
        }

        //Added for Budget Integration Modification
        string BudgetNo = objCommon.LookUp("Acc_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISBudgetHead as int),0) ISBudgetHead", "Party_No=" + ViewState["OldLedgerId"].ToString());
        objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");
        if (Convert.ToInt32(dt.Rows[0]["BudgetNo"]) > 0 || Convert.ToInt32(BudgetNo) == 1)
        {

            if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "R")
            {
                //trBudgetHead.Visible = true;
                divDeptBudget.Visible = true;
                objCostCenterController.BindBudgetHead(ddlBudgetHead);//Added by vijay 08-07-2020
                //ddlBudgetHead.SelectedValue = Convert.ToString(hdnBudgetHeadId.Value == "" ? "0" : hdnBudgetHeadId.Value);
                ddlBudgetHead.SelectedValue = Convert.ToString(LedgerRow[0]["BudgetNo"].ToString() == "" ? "0" : LedgerRow[0]["BudgetNo"].ToString());
            }




        }
        ddldepartment.SelectedValue = Convert.ToString(LedgerRow[0]["DepartmentId"].ToString() == "" ? "0" : LedgerRow[0]["DepartmentId"].ToString());
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
            txtChqNo2.Text = LedgerRow[0]["ChqNo"].ToString().Trim();
            if (LedgerRow[0]["ChqDate"].ToString().Trim() != "")
            { txtChequeDt2.Text = Convert.ToDateTime(LedgerRow[0]["ChqDate"].ToString()).ToString("dd/MM/yyyy"); }
            else txtChequeDt2.Text = "";
        }

        if (isSingleMode == "Y")
        {
            txtTranAmt.Text = Convert.ToDouble(LedgerRow[0]["Amount"].ToString()).ToString(); ;//GridData.Rows[e.NewSelectedIndex].Cells[4].Text;

            if (lblCurbal1.Text.ToString().Trim() != "")
            {
                if (Convert.ToDouble(lblCurbal1.Text) >= Convert.ToDouble(txtTranAmt.Text))
                {
                    ViewState["MOriginalBal"] = (Convert.ToDouble(lblCurbal1.Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
                }
                else
                {
                    ViewState["MOriginalBal"] = (Convert.ToDouble(lblCurbal1.Text) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
                }
            }
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
                txtTranAmt.Text = LedgerRow[0]["Credit"].ToString();// GridData.Rows[e.NewSelectedIndex].Cells[6].Text;
            }
            else
            {
                txtTranAmt.Text = LedgerRow[0]["Debit"].ToString();//GridData.Rows[e.NewSelectedIndex].Cells[5].Text; }

            }
            //if (Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[2].Text) >= Convert.ToDouble(txtTranAmt.Text))
            if (Convert.ToDouble(LedgerRow[0]["Balance"].ToString()) >= Convert.ToDouble(txtTranAmt.Text))
            {
                //ViewState["OriginalBal"] = (Convert.ToDouble(GridData.Rows[e.NewSelectedIndex].Cells[2].Text) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
                ViewState["OriginalBal"] = (Convert.ToDouble((LedgerRow[0]["Balance"].ToString())) - Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();

            }
            else
            {
                ViewState["OriginalBal"] = (Convert.ToDouble((LedgerRow[0]["Balance"].ToString())) + Convert.ToDouble(txtTranAmt.Text)).ToString().Trim();
            }

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
                    rowgrid.Visible = true;
                }
                //&& dt.Rows.Count > 0
                else
                {
                    GridData.DataSource = null;
                    GridData.DataBind();
                    rowgrid.Visible = false;
                }
            }
            else
            {
                GridData.DataSource = null;
                GridData.DataBind();
                rowgrid.Visible = false;
            }
        }

        //TrialBalanceReportController ode2 = new TrialBalanceReportController();
        //ode2.DeleteTemporaryStorageBack(Convert.ToInt16(txtAcc.Text.ToString().Trim().Split('*')[0].ToString()), Session["ComputerIDAddress"].ToString().Trim());     
        txtAcc.Text = "";
        txtPerNarration.Text = "";
        txtTranAmt.Text = "";
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
        txtTranAmt.Text = "";

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
        try
        {
            //btnSave.Enabled = true;
            ClearAll();
            clearTax();
            clearGst();
            //ddlTranType.SelectedIndex = 0;
            // added by tanu 02/03/2022
            //isTempVoucher = "Y";  //Commented By Akshay Dixit On 11-07-2022
            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022


            ddlTranType_SelectedIndexChanged(sender, e);
            if (isSingleMode == "Y")
            {
                txtAgainstAcc.Focus();
            }
            else { txtAcc.Focus(); }


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
            ddlTranType.Enabled = true;
            trCostCenter.Visible = false;
            //if (ViewState["isEdit"].ToString().Trim() == "Y")
            //{
            //    ViewState["isEdit"] = "N";
            //    Response.Redirect("AccVoucherModify.aspx");
            //}
            if (Request.QueryString["obj"] != null && Request.QueryString["ledger"] == null)
            {
                if (isTempVoucher == "Y")
                {

                    Response.Redirect("AccAllVocherModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                }
                else
                {
                    string a = ViewState["MPartyNo"].ToString();
                    string b = ViewState["MFromDate"].ToString();
                    string c = ViewState["MToDate"].ToString();
                    string d = Request.QueryString["pageno"];

                    // Response.Redirect("AccVoucherModify.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"],false);
                    Response.Redirect("AccountingVouchersModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
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
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Exception occur please try again", this.Page);
            return;
        }
    }

    private void ClearAll()
    {
       Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        lblRemainAmt.Text = "";
        ddlSponsor.SelectedValue = "0";
        ddlProjSubHead.SelectedValue = "0";
        isAllreadySet = "";
        GridData.DataSource = null;
        GridData.DataBind();
        rowgrid.Visible = false;
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
            txtTranAmt.Text = "";
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
        txtTDSAmount.Text = string.Empty;
        txtTDSLedger.Text = string.Empty;
        txtTDSPer.Text = string.Empty;
        chkTDSApplicable.Checked = false;
        chkGST.Checked = false;
        chkIGST.Checked = false;
        dvTDS.Visible = false;
        ViewState["Balance3"] = "0";
        txtDate.Text = string.Empty;
        txtPartyName.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtNatureService.Text = string.Empty;

        txtCGSTAMT.Text = txtCgstPer.Text = txtCgstOnAmount.Text = txtCGST.Text = string.Empty;

        txtSGST.Text = txtSgstOnAmount.Text = txtSGSTAMT.Text = txtSGTSPer.Text = string.Empty;

        txtIGST.Text = txtIGSTPER.Text = txtIgstOnAmount.Text = txtIGSTAMT.Text = string.Empty;

        txtTdsOnCgstAcc.Text = txtTdsCgstOnAmt.Text = txtTdsOnCgstAmt.Text = txtTdsOnCgstPer.Text = string.Empty;
        txtTdsOnSgstAcc.Text = txtTdsSgstOnAmt.Text = txtTdsOnSgstAmt.Text = txtTdsOnSgstPer.Text = string.Empty;
        txtTdsOnIgstAcc.Text = txtTdsIgstOnAmt.Text = txtTdsOnIgstAmt.Text = txtTdsOnIgstPer.Text = string.Empty;
        txtSecurityAcc.Text = txtSecurityAmt.Text = string.Empty;

        txtGSTNNO.Text = string.Empty;
        ddlPaymentMode.SelectedIndex = 0;
        txtinvoicedate.Text = string.Empty;
        txtinvoiceNo.Text = string.Empty;

        ViewState["IsIGST"] = "0";
        ViewState["IsGST"] = "0";

        divDeptBudget.Visible = false;
        // trBudgetHead.Visible = false;
        // ddlBudgetHead.SelectedValue = ddlCostCenter.SelectedValue = ddldepartment.SelectedValue = "0";
        lblBudgetBal.Text = "0";
        txtAgainstAcc.Text = string.Empty;

        ddlEmpType.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        ddlPayee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;
        ddlPrevAdv.SelectedIndex = 0;

        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
        divPayee1.Visible = false;
        divPayee2.Visible = false;
        divPrevAdv1.Visible = false;
        divPayeeNature1.Visible = false;
        divPayeeNature2.Visible = false;
        ddlmuliicostcenter.SelectedIndex = 0;
        ViewState["RecTblMultiCCActivity"] = null;
        ViewState["CC_ID"] = 0;
        lvMultiCC.DataSource = null;
        lvMultiCC.DataBind();
        lvMultiCC.Visible = false;


        lnkupload.Attributes.Add("onClick", "return ShowVoucherWindow('do'," + GridData.Rows.Count.ToString() + ");");
        lnkView.Attributes.Add("onClick", "return ShowVoucherWindow('no'," + GridData.Rows.Count.ToString() + ");");
    }

    private void ModifyVoucherTransaction()
    {
        if (para[2].ToString().Trim() == "Payment")
        {
            ddlTranType.SelectedValue = "P";
            Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        }
        else if (para[2].ToString().Trim() == "Receipt")
        {
            ddlTranType.SelectedValue = "R";
            Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        }
        else if (para[2].ToString().Trim() == "Contra")
        {
            ddlTranType.SelectedValue = "C";
            Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        }
        else
        {
            ddlTranType.SelectedValue = "J";
            isSingleMode = "N";
            Session["BANKCASHCONTRA"] = ddlTranType.SelectedValue;
        }
        // added by tanu 02/03/2022
        //isTempVoucher = "Y"; // Commented By Akshay Dixit On 11-07-2022

        isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

        SetTransactionType();
        //Added by vijay on 08-07-2020
        objCommon.FillDropDownList(ddlTdsSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
        objCommon.FillDropDownList(ddlTdsOnCgstSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
        objCommon.FillDropDownList(ddlTdsOnIgstSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
        string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");

        if (isTempVoucher == "Y")
        {
            ViewState["VOUCHERNUMBER"] = objCommon.LookUp("ACC_ALL_TEMP_TRANS", "top 1 VOUCHER_NO", "TRANSACTION_DATE BETWEEN '" + FinancialDate + "' and  VOUCHER_SQN=" + para[1].ToString().Trim());
        }
        else
        {
            ViewState["VOUCHERNUMBER"] = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "top 1 VOUCHER_NO", "TRANSACTION_DATE BETWEEN '" + FinancialDate + "' and  VOUCHER_SQN=" + para[1].ToString().Trim());
        }

        txtVoucherNo.Text = ViewState["VOUCHERNUMBER"].ToString();
        if (ViewState["isCopy"] != null)
            ViewState["VOUCHERNUMBER"] = null;
        AccountTransactionController objRet = new AccountTransactionController();
        DataSet ds1 = null;
        if (isTempVoucher == "Y")
        {
            ds1 = objRet.GetTransactionForModificationMakaut(Convert.ToInt16(para[1]), Session["comp_code"].ToString(), ddlTranType.SelectedValue);// + "_" + Session["fin_yr"].ToString().Trim());
        }
        else
        {
            //ds1 = objRet.GetTransactionForModification(Convert.ToInt16(para[1]), Session["comp_code"].ToString(), ddlTranType.SelectedValue);// + "_" + Session["fin_yr"].ToString().Trim());
            ds1 = objRet.GetTransactionForVoucherModification(Convert.ToInt16(para[1]), Session["comp_code"].ToString(), ddlTranType.SelectedValue);// + "_" + Session["fin_yr"].ToString().Trim());
        }


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
        }
        else
        {
            pnlNewBills.Visible = false;
            lvNewBills.DataSource = null;
            lvNewBills.DataBind();
        }
        ////end

        //MulticostCenter
        if (IsMultipalCostCenter == "Y")
        {
            DataSet dsMCC = null;
           // objCommon.FillDropDownList(ddlmuliicostcenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
            objCommon.FillDropDownList(ddlmuliicostcenter, "Acc_" + Session["comp_code"] + "_CostCenter ACC inner join  Acc_" + Session["comp_code"] + "_CostCategory ACAT on (ACC.Cat_ID=ACAT.Cat_ID)", "isnull(ACC.CC_ID,0) CC_ID", "ACC.CCNAME + ' ( '+ACAT.Category+' ) ' as CCNAME", "", "");

            if (isTempVoucher == "Y")
            {
                dsMCC = objCostCenterController.GetCostCenterdata(Convert.ToInt32(para[1].ToString().Trim()), Session["comp_code"].ToString(), "Y");
            }
            else
            {
                dsMCC = objCostCenterController.GetCostCenterdata(Convert.ToInt32(para[1].ToString().Trim()), Session["comp_code"].ToString(), "N");
            }

            if (dsMCC.Tables[0].Rows.Count > 0)
            {
                trMultiCostCenter.Visible = true;
                ViewState["RecTblMultiCCActivity"] = dsMCC.Tables[0];
                DataTable dtMuliccactivity = (DataTable)ViewState["RecTblMultiCCActivity"];
                lvMultiCC.DataSource = dsMCC;
                lvMultiCC.DataBind();
            }
            else
            {
                trMultiCostCenter.Visible = false;
                lvMultiCC.DataSource = null;
                lvMultiCC.DataBind();
            }
        }
        else
        {
            trMultiCostCenter.Visible = false;
            lvMultiCC.DataSource = null;
            lvMultiCC.DataBind();
        }
        //EndMultiCostCenter


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
            rowgrid.Visible = true;
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
                objCommon.ShowError(Page, "AccVoucherModify.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
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
            {
                VCH_TYPE = "P";
            }
            else if (TransactionType == "Receipt")
            {
                VCH_TYPE = "R";
            }
            else if (TransactionType == "Contra")
            {
                VCH_TYPE = "C";
            }
            else if (TransactionType == "Journal")
            {
                VCH_TYPE = "J";
            }
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
                objCommon.ShowError(Page, "AccVoucherModify.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetEmployeeName(string prefixText, string contextKey)
    {
        List<string> EmployeeName = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            VMController objVMCon = new VMController();
            // int studSelectionby = Convert.ToInt32(ddlorderby.SelectedValue);
            ds = objVMCon.FillEmployeeName(prefixText, contextKey);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[0]["Type"].ToString() == "E")
                    EmployeeName.Add(ds.Tables[0].Rows[i]["IDNO"].ToString() + "---------*" + ds.Tables[0].Rows[i]["NAME"].ToString());
                if (ds.Tables[0].Rows[0]["Type"].ToString() == "S")
                    EmployeeName.Add(ds.Tables[0].Rows[i]["IDNO"].ToString() + "---------*" + ds.Tables[0].Rows[i]["NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return EmployeeName;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // added by tanu 02/03/2022
            // isTempVoucher = "Y"; // commented By Akshay Dixit On 11-07-2022

            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

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

            if (txtPartyName.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Enter Party Name", this.Page);
                return;
            }

            // Added by Akshay Dixit On 07/04/2022
            int IsCompanyLock = Convert.ToInt32(objCommon.LookUp("ACC_COMPANY", "Lock_Status", "COMPANY_CODE='" + (Session["Comp_Code"]).ToString() + "'"));

            if (IsCompanyLock == 1)
            {
                // objCommon.DisplayUserMessage(UPDLedger, "This Company Is Locked Please Select Current Company", this);

                Response.Redirect("~/Account/selectCompany.aspx");

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
                if (Convert.ToDouble(lblTotal.Text) <= 0.00)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Total amount must be greater than 0", this);
                    return;
                }
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



                if (ddlPaymentMode.SelectedIndex == 0)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Pay Mode", this);
                    ddlPaymentMode.Focus();
                    return;
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
                if (ViewState["TDS"].ToString() == "Yes" || ViewState["IsTdsOnGst"].ToString() == "Yes" || ViewState["IsTdsOnIgst"].ToString() == "Yes" || ViewState["IsSecurity"].ToString() == "Yes")
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

                    //XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
                    //if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
                    //    PARTY_NAME.InnerText = "-";
                    //else
                    //    PARTY_NAME.InnerText = txtPartyName.Text.ToString();



                    XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");

                    //added by tanu 16_02_2022
                    string PartyName = txtPartyName.Text.Replace("'", "''");
                    if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
                        PARTY_NAME.InnerText = "-";
                    else
                        PARTY_NAME.InnerText = PartyName;

                    //started 
                    //string gstno;
                    //gstno =Convert.ToString( objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_PARTY", "GSTNO", "", "GSTNO='" + txtGSTNNO.Text+"'", ""));

                    

                    //ended



                    XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
                    if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
                        PAN_NO.InnerText = "-";
                    else
                        PAN_NO.InnerText = txtPanNo.Text.ToString();

                    XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");

                    //added by tanu 16_02_2022
                    string NatureService = txtNatureService.Text.Replace("'", "''");
                    if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
                        NATURE_SERVICE.InnerText = "-";
                    else
                        NATURE_SERVICE.InnerText = NatureService;

                    // NATURE_SERVICE.InnerText = txtNatureService.Text;

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

                    if (GridData.Columns[4].Visible == true)
                    {
                        TRAN.InnerText = GridData.Rows[i].Cells[7].Text.ToString().Trim();
                        AMOUNT.InnerText = GridData.Rows[i].Cells[4].Text.ToString().Trim();
                        ViewState["Amount"] = AMOUNT.InnerText;
                    }
                    else if (GridData.Columns[4].Visible == false)
                    {
                        if (GridData.Rows[i].Cells[5].Text.ToString().Trim() == "0.00")
                        {
                            TRAN.InnerText = "Cr";
                            AMOUNT.InnerText = GridData.Rows[i].Cells[6].Text.ToString().Trim();
                            ViewState["Amount"] = AMOUNT.ToString();
                        }
                        else
                        {
                            TRAN.InnerText = "Dr";
                            AMOUNT.InnerText = GridData.Rows[i].Cells[5].Text.ToString().Trim();
                            ViewState["Amount"] = AMOUNT.ToString();
                        }
                    }
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
                    XmlElement INVOICE_NO = objXMLDoc.CreateElement("INVOICE_NO");
                    XmlElement INVOICE_DATE = objXMLDoc.CreateElement("INVOICE_DATE");

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

                    if (txtinvoiceNo.Text.ToString().Trim() == string.Empty)
                    {
                        INVOICE_NO.InnerText = "0";
                        //INVOICE_DATE.InnerText = "";
                    }
                    else
                    {
                        INVOICE_NO.InnerText = txtinvoiceNo.Text.Trim();

                       
                    }

                    if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "J")
                    {
                        DateTime INVOICE_DATE1 = Convert.ToDateTime(txtinvoicedate.Text);
                        INVOICE_DATE.InnerText = INVOICE_DATE1.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        DateTime INVOICE_DATE1 = Convert.ToDateTime(DateTime.Now);
                        INVOICE_DATE.InnerText = INVOICE_DATE1.ToString("dd-MMM-yyyy");
                    }

                    HiddenField hdn1 = GridData.Rows[i].FindControl("hdnChqDate") as HiddenField;

                    if (txtChequeDt2.Text != "")
                    {

                        DateTime CHQ_DATE1 = Convert.ToDateTime(txtChequeDt2.Text);
                        CHQ_DATE.InnerText = CHQ_DATE1.ToString("dd-MMM-yyyy");

                        ViewState["CHQ_DATE"] = CHQ_DATE1.ToString("dd-MMM-yyyy");
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
                    if (hdnDepartmentId.Value != "" || hdnDepartmentId.Value != "0" || Convert.ToInt32(hdnDepartmentId.Value) > 0)
                    {
                        if (hdnDepartmentId.Value != "")
                        {
                            DepartmentId.InnerText = hdnDepartmentId.Value.ToString();
                        }
                        else
                        {
                            DepartmentId.InnerText = "0";
                        }
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
                    //CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    CREATED_MODIFIED_DATE.InnerText = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
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
                    //Adde by Gopsl Anthati On 02-09-2021
                    XmlElement IsTdsOnGst = objXMLDoc.CreateElement("IsTdsOnGst");
                    IsTdsOnGst.InnerText = ViewState["IsTdsOnGst"].ToString() == "Yes" ? "1" : "0";


                    XmlElement TdsOnCgstSection = objXMLDoc.CreateElement("TdsOnCgstSection");
                    XmlElement TdsOnCgstAmt = objXMLDoc.CreateElement("TdsOnCgstAmt");
                    XmlElement TdsOnCgstPer = objXMLDoc.CreateElement("TdsOnCgstPer");

                    XmlElement TdsOnSgstSection = objXMLDoc.CreateElement("TdsOnSgstSection");
                    XmlElement TdsOnSgstAmt = objXMLDoc.CreateElement("TdsOnSgstAmt");
                    XmlElement TdsOnSgstPer = objXMLDoc.CreateElement("TdsOnSgstPer");

                    if (ViewState["IsTdsOnGst"].ToString() == "Yes")
                    {
                        //HiddenField hdnTdsOnSgstSection = GridData.Rows[i].FindControl("hdnTdsOnSgstSection") as HiddenField;
                        //HiddenField hdnTdsOnSgstAmt = GridData.Rows[i].FindControl("hdnTdsOnSgstAmt") as HiddenField;

                        HiddenField hdnTdsOnCgstSection = GridData.Rows[i].FindControl("hdnTdsOnCgstSection") as HiddenField;
                        HiddenField hdnTdsOnCgstAmt = GridData.Rows[i].FindControl("hdnTdsOnCgstAmt") as HiddenField;
                        HiddenField hdnTdsOnCgstPer = GridData.Rows[i].FindControl("hdnTdsOnCgstPer") as HiddenField;

                        HiddenField hdnTdsOnSgstSection = GridData.Rows[i].FindControl("hdnTdsOnSgstSection") as HiddenField;
                        HiddenField hdnTdsOnSgstAmt = GridData.Rows[i].FindControl("hdnTdsOnSgstAmt") as HiddenField;
                        HiddenField hdnTdsOnSgstPer = GridData.Rows[i].FindControl("hdnTdsOnSgstPer") as HiddenField;

                        TdsOnCgstSection.InnerText = hdnTdsOnCgstSection.Value == "" ? "0" : hdnTdsOnCgstSection.Value;
                        TdsOnCgstAmt.InnerText = hdnTdsOnCgstAmt.Value == "" ? "0" : hdnTdsOnCgstAmt.Value;
                        TdsOnCgstPer.InnerText = hdnTdsOnCgstPer.Value == "" ? "0" : hdnTdsOnCgstPer.Value;

                        TdsOnSgstSection.InnerText = hdnTdsOnSgstSection.Value == "" ? "0" : hdnTdsOnSgstSection.Value;
                        TdsOnSgstAmt.InnerText = hdnTdsOnSgstAmt.Value == "" ? "0" : hdnTdsOnSgstAmt.Value;
                        TdsOnSgstPer.InnerText = hdnTdsOnSgstPer.Value == "" ? "0" : hdnTdsOnSgstPer.Value;
                    }
                    else
                    {
                        TdsOnCgstSection.InnerText = "0";
                        TdsOnCgstAmt.InnerText = "0";
                        TdsOnCgstPer.InnerText = "0";

                        TdsOnSgstSection.InnerText = "0";
                        TdsOnSgstAmt.InnerText = "0";
                        TdsOnSgstPer.InnerText = "0";
                    }
                    XmlElement IsTdsOnIgst = objXMLDoc.CreateElement("IsTdsOnIgst");
                    IsTdsOnIgst.InnerText = ViewState["IsTdsOnIgst"].ToString() == "Yes" ? "1" : "0";

                    XmlElement TdsOnIgstSection = objXMLDoc.CreateElement("TdsOnIgstSection");
                    XmlElement TdsOnIgstAmt = objXMLDoc.CreateElement("TdsOnIgstAmt");
                    XmlElement TdsOnIgstPer = objXMLDoc.CreateElement("TdsOnIgstPer");

                    if (ViewState["IsTdsOnIgst"].ToString() == "Yes")
                    {
                        HiddenField hdnTdsOnIgstSection = GridData.Rows[i].FindControl("hdnTdsOnIgstSection") as HiddenField;
                        HiddenField hdnTdsOnIgstAmt = GridData.Rows[i].FindControl("hdnTdsOnIgstAmt") as HiddenField;
                        HiddenField hdnTdsOnIgstPer = GridData.Rows[i].FindControl("hdnTdsOnIgstPer") as HiddenField;

                        TdsOnIgstSection.InnerText = hdnTdsOnIgstSection.Value == "" ? "0" : hdnTdsOnIgstSection.Value;
                        TdsOnIgstAmt.InnerText = hdnTdsOnIgstAmt.Value == "" ? "0" : hdnTdsOnIgstAmt.Value;
                        TdsOnIgstPer.InnerText = hdnTdsOnIgstPer.Value == "" ? "0" : hdnTdsOnIgstPer.Value;
                    }
                    else
                    {
                        TdsOnIgstSection.InnerText = "0";
                        TdsOnIgstAmt.InnerText = "0";
                        TdsOnIgstPer.InnerText = "0";
                    }

                    XmlElement IsSecurity = objXMLDoc.CreateElement("IsSecurity");
                    IsSecurity.InnerText = ViewState["IsSecurity"].ToString() == "Yes" ? "1" : "0";

                    XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

                    if (ViewState["IsSecurity"].ToString() == "Yes")
                    {
                        HiddenField hdnSecurityAmt = GridData.Rows[i].FindControl("hdnSecurityAmt") as HiddenField;
                        SecurityAmt.InnerText = hdnSecurityAmt.Value == "" ? "0" : hdnSecurityAmt.Value;
                    }
                    else
                    {
                        SecurityAmt.InnerText = "0";
                    }
                    //

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

                    XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

                    if (txtGSTNNO.Text == "" || txtGSTNNO.Text == string.Empty || txtGSTNNO.Text == null)
                    {
                        GSTIN_NO.InnerText = "-";
                    }
                    else
                    {
                        GSTIN_NO.InnerText = txtGSTNNO.Text;
                    }
                    XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
                    PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

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
                            //voucherNo1 = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='" + ddlTranType.SelectedValue.ToString() + "'");
                            // VOUCHER_NO.InnerText = voucherNo1;
                            //  ViewState["VoucherNo"] = voucherNo1;

                            //Added by tanu  at 23-03-2022  for Voucher No Modification
                            if (isEdit == "Y")
                            {
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

                            }
                            else
                            {
                                voucherNo1 = txtVoucherNo.Text.Trim();
                                VOUCHER_NO.InnerText = txtVoucherNo.Text.Trim();
                                ViewState["VoucherNo"] = txtVoucherNo.Text.Trim();
                            }

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
                    //Added by Gopal Anthati On 02-09-2021
                    objElement.AppendChild(IsTdsOnGst);
                    objElement.AppendChild(TdsOnCgstSection);
                    objElement.AppendChild(TdsOnCgstPer);
                    objElement.AppendChild(TdsOnCgstAmt);
                    objElement.AppendChild(TdsOnSgstSection);
                    objElement.AppendChild(TdsOnSgstPer);
                    objElement.AppendChild(TdsOnSgstAmt);
                    objElement.AppendChild(IsTdsOnIgst);
                    objElement.AppendChild(TdsOnIgstSection);
                    objElement.AppendChild(TdsOnIgstPer);
                    objElement.AppendChild(TdsOnIgstAmt);
                    objElement.AppendChild(IsSecurity);
                    objElement.AppendChild(SecurityAmt);
                    objElement.AppendChild(PAYMENT_MODE);
                    objElement.AppendChild(INVOICE_NO);
                    objElement.AppendChild(INVOICE_DATE);
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

                int VoucherSqnNum = 0;
                // added by tanu 02/03/2022
                //isTempVoucher = "Y";   // Commented By Akshay Dixit On 11-07-2022

                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

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
                    if (isTempVoucher == "Y")
                    {
                        voucherno = objPC1.AddTransactionWithXMLMakaut(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), ddlTranType.SelectedValue.ToString());
                    }
                    else
                    {
                        voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["comp_code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), ddlTranType.SelectedValue.ToString());

                    }
                    VoucherSqnNum = voucherno;
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
                    VoucherSqnNum = voucherno;
                }

                //added by tanu 13/12/2021 for add multipal bill fill 
                // added by tanu 02/03/2022
                // isTempVoucher = "Y"; // Commented By Akshay Dixit On 11-07-2022

                // isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

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
                            objATEnt.VOUCHER_SQN = Convert.ToInt32(VoucherSqnNum);
                            if (isTempVoucher == "Y")
                            {
                                objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_ALL_TEMP_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));

                            }
                            else
                            {
                                objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));
                            }
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

                            objATEnt.VOUCHER_SQN = Convert.ToInt32(VoucherSqnNum);

                            if (isTempVoucher == "Y")
                            {
                                objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_ALL_TEMP_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));
                            }
                            else
                            {
                                objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));
                            }

                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                objATEnt.FILEPATH = ViewState["DESTINATION_PATH"].ToString() + "\\BillNo_" + objATEnt.VOUCHER_NO; ;
                            }
                        }
                        //end
                        int ret = 0;
                        if (isTempVoucher == "Y")
                        {
                            string ConfigIsTempVoucher = "Y";
                            ret = objPC1.AddBillVoucherCreation(objATEnt, Convert.ToInt32(Session["userno"].ToString()), dtD, Session["comp_code"].ToString().Trim(), Convert.ToInt32(Session["comp_no"].ToString().Trim()), ConfigIsTempVoucher);
                        }
                        else
                        {
                            string ConfigIsTempVoucher = "N";
                            ret = objPC1.AddBillVoucherCreation(objATEnt, Convert.ToInt32(Session["userno"].ToString()), dtD, Session["comp_code"].ToString().Trim(), Convert.ToInt32(Session["comp_no"].ToString().Trim()), ConfigIsTempVoucher);
                        }
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
                // added by tanu 02/03/2022
                //isTempVoucher = "Y";  // Commented By  Akshay Dixit On 11-07-2022

                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

                ViewState["VoucherNo"] = voucherno;
                ViewState["VNO"] = ViewState["VoucherNo"].ToString();
                ViewState["Date"] = txtDate.Text;
                //if (ddlTranType.SelectedValue == "P" )
                //{
                for (int k = 0; k < GridData.Rows.Count; k++)
                {

                    //Added by gopal anthati on 09092021
                    HiddenField hdnParticulars = (HiddenField)GridData.Rows[k].FindControl("hdnParticulars");
                    HiddenField hdnBalance = (HiddenField)GridData.Rows[k].FindControl("hdnBalance");
                    HiddenField hdnAmount = (HiddenField)GridData.Rows[k].FindControl("hdnAmount");
                    HiddenField hdnMode = (HiddenField)GridData.Rows[k].FindControl("hdnMode");
                    HiddenField hdnIsEmployee = (HiddenField)GridData.Rows[k].FindControl("hdnIsEmployee");
                    HiddenField hdnTagEmpIdno = (HiddenField)GridData.Rows[k].FindControl("hdnTagEmpIdno");
                    HiddenField hdnPrevAdvId = (HiddenField)GridData.Rows[k].FindControl("hdnPrevAdvId");
                    if (hdnIsEmployee.Value != "0" || hdnIsEmployee.Value != "")
                    {
                        if (hdnIsEmployee.Value != "")
                        {
                            if (Convert.ToInt32(hdnIsEmployee.Value) != 0)
                            {
                                //if (hdnMode.Value == "Dr")
                                /// {
                                /// }
                                /// else
                                {
                                    //added by tanu  04_02_2022
                                    if (ViewState["isModi"].ToString() == "Y")
                                    {
                                        objATEnt.OLD_VOUCHER_SQN = Convert.ToInt32(para[1].ToString().Trim());
                                        //objATEnt.OLD_VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_ALL_TEMP_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));
                                    }
                                    else
                                    {
                                        objATEnt.OLD_VOUCHER_SQN = '0';
                                    }

                                    objATEnt.ISEMPLOYEE = hdnIsEmployee.Value == "" ? 0 : Convert.ToInt32(hdnIsEmployee.Value);
                                    objATEnt.IDNO = hdnTagEmpIdno.Value == "" ? 0 : Convert.ToInt32(hdnTagEmpIdno.Value);
                                    objATEnt.TRANSACTION_TYPE = ddlTranType.SelectedValue;
                                    objATEnt.TRAN = hdnMode.Value;
                                    objATEnt.PARENTID = hdnPrevAdvId.Value == "" ? 0 : Convert.ToInt32(hdnPrevAdvId.Value);
                                    objATEnt.PARTY_NO = Convert.ToInt32(hdnParticulars.Value.Trim().ToString().Split('*')[1].ToString());
                                    objATEnt.AMOUNT = Convert.ToDouble(hdnAmount.Value);
                                    if (objATEnt.PARENTID > 0)
                                    {

                                        DataSet dtparent = null;
                                        if (isTempVoucher == "Y")
                                        {
                                            dtparent = objCommon.FillDropDown("ACC_VOUCHER_TAG_USER", "TOP 1 BALANCE_AMOUNT", "", "PARENT_ID=" + objATEnt.PARENTID, "");
                                            if (dtparent.Tables[0].Rows.Count > 0)
                                            {
                                                double dtAmount = Convert.ToDouble(dtparent.Tables[0].Rows[0]["BALANCE_AMOUNT"]);
                                                 if(dtAmount>0)
                                                 {
                                                     objATEnt.BALANCEAMOUNT = Convert.ToInt32(dtAmount) - Convert.ToInt32(objATEnt.AMOUNT);
                                                 }
                                                 else
                                                 {
                                                     double AmountB = Convert.ToDouble(objCommon.LookUp("ACC_VOUCHER_TAG_USER_TEMP", "AMOUNT", "TAG_USER_ID=" + objATEnt.PARENTID));
                                                     objATEnt.BALANCEAMOUNT = Convert.ToInt32(AmountB) - Convert.ToInt32(objATEnt.AMOUNT);
                                                    
                                                 }
                                            }
                                            else
                                            {
                                                double Amount = Convert.ToDouble(objCommon.LookUp("ACC_VOUCHER_TAG_USER_TEMP", "AMOUNT", "TAG_USER_ID=" + objATEnt.PARENTID));
                                                objATEnt.BALANCEAMOUNT = Convert.ToInt32(Amount) - Convert.ToInt32(objATEnt.AMOUNT);//Convert.ToDouble(hdnBalance.Value); 
                                            }
                                        }
                                        else
                                        {

                                            dtparent = objCommon.FillDropDown("ACC_VOUCHER_TAG_USER", "TOP 1 BALANCE_AMOUNT", "", "PARENT_ID=" + objATEnt.PARENTID, "");
                                            if (dtparent.Tables[0].Rows.Count > 0)
                                            {
                                                double dtAmount = Convert.ToDouble(dtparent.Tables[0].Rows[0]["BALANCE_AMOUNT"]);
                                                if(dtAmount>0)
                                                {
                                                objATEnt.BALANCEAMOUNT = Convert.ToInt32(dtAmount) - Convert.ToInt32(objATEnt.AMOUNT);
                                                }
                                                else
                                                {
                                                     double Amountc = Convert.ToDouble(objCommon.LookUp("ACC_VOUCHER_TAG_USER_TEMP", "AMOUNT", "TAG_USER_ID=" + objATEnt.PARENTID));
                                                     objATEnt.BALANCEAMOUNT = Convert.ToInt32(Amountc) - Convert.ToInt32(objATEnt.AMOUNT);
                                                    
                                                }
                                            }
                                            else
                                            {

                                                double Amount = Convert.ToDouble(objCommon.LookUp("ACC_VOUCHER_TAG_USER", "AMOUNT", "TAG_USER_ID=" + objATEnt.PARENTID));
                                                objATEnt.BALANCEAMOUNT = Convert.ToInt32(Amount) - Convert.ToInt32(objATEnt.AMOUNT);//Convert.ToDouble(hdnBalance.Value); 
                                            }
                                        }
                                        //Modified By Akshay >>> converted both into to int.

                                    }
                                    else
                                    {
                                        objATEnt.BALANCEAMOUNT = 0;
                                    }
                                    objATEnt.NARRATION = txtNarration.Text;
                                    objATEnt.COMPANY_CODE = Session["comp_code"].ToString();
                                    objATEnt.TRANSACTION_DATE = Convert.ToDateTime(txtDate.Text);
                                    objATEnt.CREATEDBY = Convert.ToInt32(Session["userno"]);
                                    objATEnt.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
                                    objATEnt.VOUCHER_SQN = Convert.ToInt32(VoucherSqnNum);
                                    objATEnt.VOUCHER_NO = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "DISTINCT VOUCHER_NO", "VOUCHER_SQN=" + Convert.ToInt32(VoucherSqnNum)));
                                    if (isTempVoucher == "Y")
                                    {
                                        int j = objPC1.TagUserForVoucherEntry(objATEnt, ViewState["isEdit"].ToString());
                                    }
                                    else
                                    {
                                        int j = objPC1.TagUserForDirectVoucherEntry(objATEnt, ViewState["isEdit"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    //


                    AccountTransaction objPC = new AccountTransaction();
                    HiddenField hdnCCID = GridData.Rows[k].FindControl("hdnCostCenterID") as HiddenField;
                    if (hdnCCID.Value == string.Empty)
                    {
                        hdnCCID.Value = "0";
                    }
                    // added by tanu 02/03/2022
                    //  isTempVoucher = "Y"; // Commented By Akshay Dixit On 11-07-2022

                    isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022
                    if (IsMultipalCostCenter == "N")
                    {
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
                }
                // multipal Cost Center

                if (IsMultipalCostCenter == "Y")
                {

                    int retval = 0;
                    if (ViewState["isEdit"].ToString().Trim() == "Y")
                    {
                        if (isTempVoucher == "Y")
                        {
                            retval = objCostCenterController.DeleteMultipalCostCenterVoucher(Convert.ToInt32(para[1].ToString().Trim()), ddlTranType.SelectedValue.ToString(), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString(), "Y");
                        }
                        else
                        {
                            retval = objCostCenterController.DeleteMultipalCostCenterVoucher(Convert.ToInt32(para[1].ToString().Trim()), ddlTranType.SelectedValue.ToString(), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString(), "N");
                        }
                    }
                    else
                    {
                        if (isTempVoucher == "Y")
                        {
                            retval = objCostCenterController.DeleteMultipalCostCenterVoucher(voucherno, ddlTranType.SelectedValue.ToString(), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString(), "Y");
                        }
                        else
                        {
                            retval = objCostCenterController.DeleteMultipalCostCenterVoucher(voucherno, ddlTranType.SelectedValue.ToString(), Session["comp_code"].ToString().Trim(), Convert.ToDateTime(txtDate.Text.Trim()).ToString(), "N");
                        }
                    }

                    if (retval == 1)
                    {
                        foreach (ListViewDataItem lvItem in lvMultiCC.Items)
                        {
                            Label lblAmount = lvItem.FindControl("lblAmount") as Label;
                            HiddenField CCID = lvItem.FindControl("hdMulticcID") as HiddenField;
                            HiddenField PartyNo = lvItem.FindControl("hdMulticcPartyId") as HiddenField;

                            objCostCenter.VCHNO = voucherno;
                            objCostCenter.PARTY_NO = Convert.ToInt32(PartyNo.Value);
                            objCostCenter.CCVHDATE = Convert.ToDateTime(txtDate.Text.Trim());
                            objCostCenter.CC_ID = Convert.ToInt32(CCID.Value);
                            string CatID = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_CostCenter", "Cat_ID", "CC_ID=" + Convert.ToString(CCID.Value));
                            objCostCenter.CATID = Convert.ToInt32(CatID);
                            objCostCenter.VCHTYPE = ddlTranType.SelectedValue.ToString();
                            double CCAmount = Convert.ToDouble(lblAmount.Text);
                            objCostCenter.AMOUNT = Convert.ToInt32(CCAmount);

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

                // end multipal Cost Center




                // added by tanu 02/03/2022
                //isTempVoucher = "Y";  // Commmented By Akshay Dixit On 11-07-2022

                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

                DataSet dsResult = null;
                if (isTempVoucher == "Y")
                {
                    //ithe gela
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
                                //Response.Redirect("AccVoucherModify.aspx");
                                // Response.Redirect("AccVoucherModify.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                                Response.Redirect("AccountingVouchersModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);

                                //btnSave.Enabled = true;
                            }
                        }
                        if (Request.QueryString["obj"] != null && Request.QueryString["ledger"] == null)
                        {
                            if (isTempVoucher == "Y")
                            {
                                Response.Redirect("AccAllVocherModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                                btnReset_Click(sender, e);
                            }
                            else
                            {
                                //Response.Redirect("AccVoucherModify.aspx");
                                //Added by Nakul Chawre
                                //  Response.Redirect("AccVoucherModify.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);
                                Response.Redirect("AccountingVouchersModifications.aspx?obj=" + ViewState["MPartyNo"].ToString() + "," + ViewState["MFromDate"].ToString() + "," + ViewState["MToDate"].ToString() + "&pageno=" + Request.QueryString["pageno"], false);

                                btnReset_Click(sender, e);
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

        XmlElement INVOICE_NO = objXMLDoc.CreateElement("INVOICE_NO");
        XmlElement INVOICE_DATE = objXMLDoc.CreateElement("INVOICE_DATE");

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

        //XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        //if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
        //    PARTY_NAME.InnerText = "-";
        //else
        //    PARTY_NAME.InnerText = txtPartyName.Text.ToString();


        //added by tanu 16_02_2022
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        string PartyName = txtPartyName.Text.Replace("'", "''");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
            PARTY_NAME.InnerText = "-";
        else
            PARTY_NAME.InnerText = PartyName;

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
            PAN_NO.InnerText = "-";
        else
            PAN_NO.InnerText = txtPanNo.Text.ToString();

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        //if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
        //    NATURE_SERVICE.InnerText = "-";
        //else
        //    NATURE_SERVICE.InnerText = txtNatureService.Text.ToString();


        string NatureService = txtNatureService.Text.Replace("'", "''");
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
            NATURE_SERVICE.InnerText = "-";
        else
            NATURE_SERVICE.InnerText = NatureService;



        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");



        int i = 0;

        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");





        HiddenField DeptId = GridData.Rows[i].FindControl("hdnDepartmentId") as HiddenField;
        HiddenField BudgetN = GridData.Rows[i].FindControl("hdnBudgetHeadId") as HiddenField;
        HiddenField CCid = GridData.Rows[i].FindControl("hdnCostCenterID") as HiddenField;
        if (DeptId.Value != "")
        {
            DepartmentId.InnerText = DeptId.Value;
        }
        else
        {
            DepartmentId.InnerText = "0";
        }
        if (BudgetN.Value != "")
        {
            BudgetNo.InnerText = BudgetN.Value;
        }
        else
        {
            BudgetNo.InnerText = "0";
        }
        if (CCid.Value != "")
        {
            CC_ID.InnerText = CCid.Value;
        }
        else
        {
            CC_ID.InnerText = "0";
        }
        if (txtinvoiceNo.Text.ToString().Trim() == string.Empty)
        {
            INVOICE_NO.InnerText = "0";
            //INVOICE_DATE.InnerText = "";
        }
        else
        {
            INVOICE_NO.InnerText = txtinvoiceNo.Text.Trim();


        }

        if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "J")
        {
            DateTime INVOICE_DATE1 = Convert.ToDateTime(txtinvoicedate.Text);
            INVOICE_DATE.InnerText = INVOICE_DATE1.ToString("dd-MMM-yyyy");
        }
        else
        {
            DateTime INVOICE_DATE1 = Convert.ToDateTime(DateTime.Now);
            INVOICE_DATE.InnerText = INVOICE_DATE1.ToString("dd-MMM-yyyy");
        }

        //ADDEDE BY VIJAY ON 26082020 FOR GST&IGST
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = ViewState["IsIGST"].ToString() == "Yes" ? "1" : "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");
        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";

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
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";


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
                //Commented By Akshay Dixit on 05-04-2022
                //HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                //if (opartystring == string.Empty)
                //{ opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
                //else
                //{
                //    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
                //}

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



        //Adde by Gopal Anthati On 02-09-2021
        XmlElement IsTdsOnGst = objXMLDoc.CreateElement("IsTdsOnGst");
        IsTdsOnGst.InnerText = ViewState["IsTdsOnGst"].ToString() == "Yes" ? "1" : "0";

        XmlElement TdsOnCgstSection = objXMLDoc.CreateElement("TdsOnCgstSection");
        XmlElement TdsOnCgstAmt = objXMLDoc.CreateElement("TdsOnCgstAmt");
        XmlElement TdsOnCgstPer = objXMLDoc.CreateElement("TdsOnCgstPer");

        XmlElement TdsOnSgstSection = objXMLDoc.CreateElement("TdsOnSgstSection");
        XmlElement TdsOnSgstAmt = objXMLDoc.CreateElement("TdsOnSgstAmt");
        XmlElement TdsOnSgstPer = objXMLDoc.CreateElement("TdsOnSgstPer");

        if (ViewState["IsTdsOnGst"].ToString() == "Yes")
        {
            opartystring = hdnIdEditParty.Value.ToString();

            for (int j = 0; j < GridData.Rows.Count; j++)
            {

                HiddenField hdnTdsOnCgstSection = GridData.Rows[j].FindControl("hdnTdsOnCgstSection") as HiddenField;
                HiddenField hdnTdsOnCgstAmt = GridData.Rows[j].FindControl("hdnTdsOnCgstAmt") as HiddenField;
                HiddenField hdnTdsOnCgstPer = GridData.Rows[j].FindControl("hdnTdsOnCgstPer") as HiddenField;

                HiddenField hdnTdsOnSgstSection = GridData.Rows[j].FindControl("hdnTdsOnSgstSection") as HiddenField;
                HiddenField hdnTdsOnSgstAmt = GridData.Rows[j].FindControl("hdnTdsOnSgstAmt") as HiddenField;
                HiddenField hdnTdsOnSgstPer = GridData.Rows[j].FindControl("hdnTdsOnSgstPer") as HiddenField;

                TdsOnCgstSection.InnerText = hdnTdsOnCgstSection.Value == "" ? "0" : hdnTdsOnCgstSection.Value;
                TdsOnCgstAmt.InnerText = hdnTdsOnCgstAmt.Value == "" ? "0" : hdnTdsOnCgstAmt.Value;
                TdsOnCgstPer.InnerText = hdnTdsOnCgstPer.Value == "" ? "0" : hdnTdsOnCgstPer.Value;

                TdsOnSgstSection.InnerText = hdnTdsOnSgstSection.Value == "" ? "0" : hdnTdsOnSgstSection.Value;
                TdsOnSgstAmt.InnerText = hdnTdsOnSgstAmt.Value == "" ? "0" : hdnTdsOnSgstAmt.Value;
                TdsOnSgstPer.InnerText = hdnTdsOnSgstPer.Value == "" ? "0" : hdnTdsOnSgstPer.Value;
            }
            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
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
            TdsOnCgstSection.InnerText = "0";
            TdsOnCgstAmt.InnerText = "0";
            TdsOnCgstPer.InnerText = "0";

            TdsOnSgstSection.InnerText = "0";
            TdsOnSgstAmt.InnerText = "0";
            TdsOnSgstPer.InnerText = "0";

            for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
            {
                //Commented By Akshay Dixit on 05-04-2022
                //HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                //if (opartystring == string.Empty)
                //{ opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
                //else
                //{
                //    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
                //}

                //updated by naresh warbhe for showing naration in cash in hand for other ledger
                if (isPerNarration == "Y")
                {
                    PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
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
        XmlElement IsTdsOnIgst = objXMLDoc.CreateElement("IsTdsOnIgst");
        IsTdsOnIgst.InnerText = ViewState["IsTdsOnIgst"].ToString() == "Yes" ? "1" : "0";

        XmlElement TdsOnIgstSection = objXMLDoc.CreateElement("TdsOnIgstSection");
        XmlElement TdsOnIgstAmt = objXMLDoc.CreateElement("TdsOnIgstAmt");
        XmlElement TdsOnIgstPer = objXMLDoc.CreateElement("TdsOnIgstPer");

        if (ViewState["IsTdsOnIgst"].ToString() == "Yes")
        {
            opartystring = hdnIdEditParty.Value.ToString();
            for (int j = 0; j < GridData.Rows.Count; j++)
            {

                HiddenField hdnTdsOnIgstSection = GridData.Rows[j].FindControl("hdnTdsOnIgstSection") as HiddenField;
                HiddenField hdnTdsOnIgstAmt = GridData.Rows[j].FindControl("hdnTdsOnIgstAmt") as HiddenField;
                HiddenField hdnTdsOnIgstPer = GridData.Rows[j].FindControl("hdnTdsOnIgstPer") as HiddenField;

                TdsOnIgstSection.InnerText = hdnTdsOnIgstSection.Value == "" ? "0" : hdnTdsOnIgstSection.Value;
                TdsOnIgstAmt.InnerText = hdnTdsOnIgstAmt.Value == "" ? "0" : hdnTdsOnIgstAmt.Value;
                TdsOnIgstPer.InnerText = hdnTdsOnIgstPer.Value == "" ? "0" : hdnTdsOnIgstPer.Value;
            }
            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
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
            TdsOnIgstSection.InnerText = "0";
            TdsOnIgstAmt.InnerText = "0";
            TdsOnIgstPer.InnerText = "0";

            for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
            {
                //Commented By Akshay Dixit on 05-04-2022
                //HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                //if (opartystring == string.Empty)
                //{ opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
                //else
                //{
                //    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
                //}

                //updated by naresh warbhe for showing naration in cash in hand for other ledger
                if (isPerNarration == "Y")
                {
                    PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
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

        XmlElement IsSecurity = objXMLDoc.CreateElement("IsSecurity");
        IsSecurity.InnerText = ViewState["IsSecurity"].ToString() == "Yes" ? "1" : "0";

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        if (ViewState["IsSecurity"].ToString() == "Yes")
        {
            HiddenField hdnSecurityAmt = GridData.Rows[0].FindControl("hdnSecurityAmt") as HiddenField;
            SecurityAmt.InnerText = hdnSecurityAmt.Value == "" ? "0" : hdnSecurityAmt.Value;

            if (isPerNarration == "Y")
            {
                PARTICULARS.InnerText = GridData.Rows[0].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
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
            SecurityAmt.InnerText = "0";
            for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
            {
                //Commented By Akshay Dixit on 05-04-2022
                //HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
                //if (opartystring == string.Empty)
                //{ opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim(); }
                //else
                //{
                //    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
                //}

                //updated by naresh warbhe for showing naration in cash in hand for other ledger
                if (isPerNarration == "Y")
                {
                    PARTICULARS.InnerText = GridData.Rows[i].Cells[3].Text.ToString().Trim().Replace("'", "''");//""; // "Consiladated Entry For Voucher No:" + txtVoucherNo.Text.ToString().Trim();
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
        //





        for (i = 0; i < GridData.Rows.Count; i++)//start of for loop 1
        {
            HiddenField hdnparty = GridData.Rows[i].FindControl("hdnPartyNo") as HiddenField;
            if (opartystring == string.Empty)
            {
                opartystring = Convert.ToInt32(hdnparty.Value).ToString().Trim();
            }
            else
            {
                if (Convert.ToInt32(hdnparty.Value).ToString() != opartystring)
                {
                    opartystring = opartystring + "," + Convert.ToInt32(hdnparty.Value).ToString().Trim();
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
        //CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");

        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

        if (txtGSTNNO.Text == "" || txtGSTNNO.Text == string.Empty || txtGSTNNO.Text == null)
        {
            GSTIN_NO.InnerText = "-";
        }
        else
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }
        XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
        PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";

        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";

        ProjectId.InnerText = ddlSponsor.SelectedValue;
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = ddlBillNo.SelectedValue;

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
        objElement.AppendChild(INVOICE_NO);
        objElement.AppendChild(INVOICE_DATE);

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
        //Added by Gopal Anthati On 02-09-2021
        objElement.AppendChild(IsTdsOnGst);
        objElement.AppendChild(TdsOnCgstSection);
        objElement.AppendChild(TdsOnCgstPer);
        objElement.AppendChild(TdsOnCgstAmt);
        objElement.AppendChild(TdsOnSgstSection);
        objElement.AppendChild(TdsOnSgstPer);
        objElement.AppendChild(TdsOnSgstAmt);
        objElement.AppendChild(IsTdsOnIgst);
        objElement.AppendChild(TdsOnIgstSection);
        objElement.AppendChild(TdsOnIgstPer);
        objElement.AppendChild(TdsOnIgstAmt);
        objElement.AppendChild(IsSecurity);
        objElement.AppendChild(SecurityAmt);
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

        //XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        //if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
        //    PARTY_NAME.InnerText = "-";
        //else
        //    PARTY_NAME.InnerText = txtPartyName.Text.ToString();

        //added by tanu 16_02_2022
        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        string PartyName = txtPartyName.Text.Replace("'", "''");
        if (txtPartyName.Text == "" || txtPartyName.Text == string.Empty)
            PARTY_NAME.InnerText = "-";
        else
            PARTY_NAME.InnerText = PartyName;

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
            PAN_NO.InnerText = "-";
        else
            PAN_NO.InnerText = txtPanNo.Text.ToString();

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        //if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
        //    NATURE_SERVICE.InnerText = "-";
        //else
        //    NATURE_SERVICE.InnerText = txtNatureService.Text.ToString();

        string NatureService = txtNatureService.Text.Replace("'", "''");
        if (txtNatureService.Text == "" || txtNatureService.Text == string.Empty)
            NATURE_SERVICE.InnerText = "-";
        else
            NATURE_SERVICE.InnerText = NatureService;


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
        //CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");

        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

        if (txtGSTNNO.Text == "" || txtGSTNNO.Text == string.Empty || txtGSTNNO.Text == null)
        {
            GSTIN_NO.InnerText = "-";
        }
        else
        {
            GSTIN_NO.InnerText = txtGSTNNO.Text;
        }

        XmlElement PAYMENT_MODE = objXMLDoc.CreateElement("PAYMENT_MODE");
        PAYMENT_MODE.InnerText = ddlPaymentMode.SelectedValue;

        ProjectId.InnerText = ddlSponsor.SelectedValue;
        ProjectSubId.InnerText = "0";
        BILL_ID.InnerText = ddlBillNo.SelectedValue;



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
        rowgrid.Visible = true;

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

        XmlElement INVOICE_NO = objXMLDoc.CreateElement("INVOICE_NO");
        XmlElement INVOICE_DATE = objXMLDoc.CreateElement("INVOICE_DATE");

      
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
        //CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");

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
        objElement.AppendChild(INVOICE_NO);
        objElement.AppendChild(INVOICE_DATE);

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
            // added by tanu 02/03/2022
            // isTempVoucher = "Y"; // Commented By Akshay Dixit On 11-07-2022

            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", ""); //Added By Akshay Dixit On 11-07-2022

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
                        // ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRptMakaut.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                        //added by tanu 24/01/2021
                        ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherPending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                    }
                    else
                    {
                        //orignal 
                        // ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                        //added by tanu 24/01/2021    changes in report
                        ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                    }

                }
                else if (isTempVoucher == "Y")
                {
                    //orignal 
                    // ShowVoucherPrintReport("Voucher", "JvContraVoucherReportMakaut.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                    //added by tanu 24/01/2021
                    ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Pending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                }
                else
                {
                    //orignal
                    // ShowVoucherPrintReport("Voucher", "JvContraVoucherReport.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                    //added by tanu 24/01/2021
                    ShowVoucherPrintReport("Voucher", "JvContraVoucherReport.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                }

            }
            else if (isFourSign == "Y")
            {
                if (ViewState["Voucher"].ToString().Trim() == "Payment" || ViewState["Voucher"].ToString().Trim() == "Receipt")
                {
                    //orignal
                    // ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                    //added by tanu 24/01/2021
                    ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2_Pending.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo, isBankCash);
                }
                else
                {
                    //orignal
                    // ShowVoucherPrintReport("Voucher", "JvContraVoucherReport_Format2.rpt", ViewState["Voucher"].ToString().Trim(), voucherNo);
                    //added by tanu 24/01/2021
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
        lvMultiCC.DataSource = null;
        lvMultiCC.DataBind();
        ViewState["RecTblMultiCCActivity"] = null;
        ViewState["CC_ID"] = 0;

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
        txtinvoicedate.Text = txtDate.Text;
    }

    protected void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ViewState["OrigData"] = e.Row.Cells[3].Text;
            GridViewRow row = e.Row;
            HiddenField hdnDld = row.FindControl("hdnDld") as HiddenField;
            if (hdnDld.Value.ToString() == "5" || hdnDld.Value.ToString() == "1")
            {
                e.Row.Cells[0].Enabled = true;
            }
            else
            {
                e.Row.Cells[0].Enabled = false;
            }

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
                if (IsMultipalCostCenter == "Y")
                {
                    trMultiCostCenter.Visible = true;
                  //  objCommon.FillDropDownList(ddlmuliicostcenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
                    objCommon.FillDropDownList(ddlmuliicostcenter, "Acc_" + Session["comp_code"] + "_CostCenter ACC inner join  Acc_" + Session["comp_code"] + "_CostCategory ACAT on (ACC.Cat_ID=ACAT.Cat_ID)", "isnull(ACC.CC_ID,0) CC_ID", "ACC.CCNAME + ' ( '+ACAT.Category+' ) ' as CCNAME", "", "");

                }
                else
                {
                    trCostCenter.Visible = true;
                    objCommon.FillDropDownList(ddlCostCenter, "Acc_" + Session["comp_code"] + "_CostCenter", "isnull(CC_ID,0) CC_ID", "CCNAME", "", "");
                }
            }
            else
            {
                
                trCostCenter.Visible = false;
                trMultiCostCenter.Visible = false;
            }

            //Commented by gopal anthati on 15-09-2021
            //if (ddlTranType.SelectedValue == "P" || ddlTranType.SelectedValue == "R")
            //{
            //    int IsBudgetHead = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "isnull(cast(ISBudgetHead as int),0) ISBudgetHead", "Party_No=" + hdnOpartyManual.Value));
            //    if (IsBudgetHead == 1)
            //    {
            //        ViewState["IsBudgetHead"] = "Yes";
            //        //trBudgetHead.Visible = true;
            //        divDeptBudget.Visible = true;
            //        //trBudgetHead.Style.Add("display","block");
            //        //  objCommon.FillDropDownList(ddlBudgetHead, "ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");

            //        //ADDED BY VIJAY ANDOJU ON
            //        objCostCenterController.BindBudgetHead(ddlBudgetHead);//Bind Budget Head
            //        objCommon.FillDropDownList(ddldepartment, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0 AND SUBDEPT<>''", "SUBDEPTNO");

            //    }
            //    else
            //    {
            //        ViewState["IsBudgetHead"] = "No";
            //        //trBudgetHead.Visible = false;
            //        divDeptBudget.Visible = false;
            //        // trBudgetHead.Style.Add("display", "none");
            //    }
            //}
            //else
            //{
            //    ViewState["IsBudgetHead"] = "No";
            //}
            string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtAcc.Text.ToString().Trim().Split('*')[1] + "'");
            PartyController objPC = new PartyController();
            DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());
            if (dtr.Read())
            {

                if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
                {
                    lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                    lblCur2.Text = "Dr";
                }
                else
                {
                    lblCurBal2.Text = dtr["BALANCE"].ToString().Trim();
                    lblCur2.Text = "Cr";
                }
                //ViewState["Balance2"] = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
            }

            txtTranAmt.Focus();
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

        DataSet ds = objPC1.BindLedgers(Convert.ToInt32(ddlSponsor.SelectedValue), Convert.ToInt32(ddlProjSubHead.SelectedValue), Session["comp_code"].ToString());
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtAgainstAcc.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString();
            //hdnAgainstPartyId.Value = txtAgainstAcc.Text.ToString().Split('*')[1].ToString();
            //hdnOpartyManual.Value = txtAgainstAcc.Text.ToString().Split('*')[1].ToString();
            lblCurbal1.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();

            txtAgainstAcc_TextChanged1(sender, e);
        }
        if (ds != null && ds.Tables[1].Rows.Count > 0)
        {
            txtAcc.Text = ds.Tables[1].Rows[0]["PARTY_NAME"].ToString();
            //hdnPartyManual.Value = txtAcc.Text.ToString().Split('*')[1].ToString();
            lblCurBal2.Text = ds.Tables[1].Rows[0]["BALANCE"].ToString();

            txtAcc_TextChanged(sender, e);
        }

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
            rowgrid.Visible = true;
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

    protected void ddlBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsParty = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "BALANCE", "PARTY_NO =(select Party_No from ACC_BUDGET_HEAD_NEW where BUDGET_NO=" + Convert.ToInt32(ddlBudgetHead.SelectedValue) + ")", "");
            if (dsParty.Tables[0].Rows.Count > 0 && dsParty != null)
            {
                txtAcc.Text = dsParty.Tables[0].Rows[0]["PARTY_NAME"].ToString();
                txtAcc_TextChanged(sender, e);
            }
            string Amount = string.Empty;
            DataSet ds = null;
            if (ddlBudgetHead.SelectedIndex > 0)
            {
                ds = objPC1.GetBudegetBalanceNEW(Convert.ToInt32(ddldepartment.SelectedValue), Convert.ToInt32(ddlBudgetHead.SelectedValue), DateTime.Today);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            lblBudgetBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[6].Rows[0]["BALANCE"].ToString()));
                        }
                        else
                        {
                            lblBudgetBal.Text = "0.00";
                        }
                    }
                   
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
        string[] Party = txtAgainstAcc.Text.ToString().Split('*');
        if (Party.Length > 1)
        {
            int partyId = Convert.ToInt32(txtAgainstAcc.Text.ToString().Split('*')[1].ToString());

            double PartyAgainstBal = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_Party", "BALANCE", "Party_No=" + partyId));
            lblCurbal1.Text = String.Format("{0:0.00}", Convert.ToDouble(PartyAgainstBal.ToString()));
            txtAcc.Focus();
        }
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
    protected void chkTDSApplicable_CheckedChanged(object sendeddlSectionr, EventArgs e)
    {
        if (chkTDSApplicable.Checked == true)
        {

            if (txtTranAmt.Text == "")
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Enter Amount", this);
                chkTDSApplicable.Checked = false;
                return;
            }
            else
            {
                dvTDS.Visible = true;
                // txtTDSLedger.Focus();
                txtTdsOnAmount.Text = Convert.ToDouble(txtTranAmt.Text).ToString();
                objCommon.FillDropDownList(ddlTdsSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
                ViewState["TDS"] = "Yes";
            }
        }
        else
        {
            dvTDS.Visible = false;
            txtTDSLedger.Text = string.Empty;
            txtTDSPer.Text = string.Empty;
            txtTDSAmount.Text = string.Empty;
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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    // row["Departmentid"] = "0";
                }

                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                row["Section"] = 0;
                //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
                row["TDSAMOUNT"] = txtTDSAmount.Text;
                row["TDSsection"] = ddlTdsSection.SelectedValue;
                row["TDSpercentage"] = txtTDSPer.Text;



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

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                    //ViewState["TagUser"] = "Yes";
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                    //ViewState["TagUser"] = "Yes";
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                    //ViewState["TagUser"] = "No";
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;

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

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    //  row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = txtTdsOnAmount.Text;
                row["TDSsection"] = ddlTdsSection.SelectedValue;
                row["TDSpercentage"] = txtTDSPer.Text;

                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }


    }
    private void AddIGSTGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkIGST.Checked == false)
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
                    TranMode = "Cr";
                }
                else
                {
                    TranMode = "Dr";
                }

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim()))).ToString().Trim();
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance2"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtTDSAmount.Text))).ToString().Trim();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtTDSAmount.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    //  row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                row["Section"] = 0;
                //row["TDSAMOUNT"] = txtTdsOnAmount.Text;
                //row["TDSsection"] = ddlTdsSection.SelectedValue;
                //row["TDSpercentage"] = txtTDSPer.Text;

                row["IGSTAmount"] = txtIGSTAMT.Text;
                row["IGSTper"] = txtIGSTPER.Text;
                row["IGSTonAmount"] = "0";


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
                row["Particulars"] = txtIGST.Text.ToString().Trim();
                row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Cr";


                }
                else
                {
                    TranMode = "Dr";

                }

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtIGST.Text.ToString().Trim().Split('*')[1] + "'");
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim()))).ToString().Trim();
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
                        row["Balance"] = (Convert.ToDouble(ViewState["Balance3"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtIGSTAMT.Text))).ToString().Trim();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtIGSTAMT.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    // row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020

                row["TDSAMOUNT"] = "0";
                row["TDSsection"] = "0";
                row["TDSpercentage"] = "0";

                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = txtIGSTAMT.Text;
                row["IGSTper"] = txtIGSTPER.Text;
                row["IGSTonAmount"] = txtIgstOnAmount.Text;

                row["CGSTAmount"] = "0";
                row["CGSTper"] = "0";
                row["CGSTonAmount"] = "0";

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";
                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }


    }

    private void AddGSTGrid()
    {
        for (int j = 0; j < 2; j++)
        {
            if (j == 0 && chkGST.Checked == true)
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
                row["Particulars"] = txtCGST.Text.ToString().Trim();
                row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Cr";
                }
                else
                {
                    TranMode = "Dr";
                }

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Cr";
                    }
                    else
                    {
                        TranMode = "Dr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtCGST.Text.ToString().Trim().Split('*')[1] + "'");
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Id"].ToString() == partyNo)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    PartyController objPC = new PartyController();
                    DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["comp_code"].ToString());

                    if (dtr.Read())
                    {
                        ViewState["BalanceCgst"] = dtr["BALANCE"].ToString().Trim();
                        // = lblCurBal2.Text;// dtr["BALANCE"].ToString().Trim();
                    }

                    string balance = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Id"].ToString() == partyNo)
                        {

                            if (dt.Rows[i]["Mode"].ToString() == "Dr")
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["BalanceCgst"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["BalanceCgst"] = balance;
                            }
                            else
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["BalanceCgst"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["BalanceCgst"] = balance;
                            }

                        }
                    }
                }

                if (TranMode.ToString().Trim() == "Dr")
                {
                    if (ViewState["BalanceCgst"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["BalanceCgst"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["BalanceCgst"] = row["Balance"].ToString();
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
                    if (ViewState["BalanceCgst"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["BalanceCgst"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtCGSTAMT.Text))).ToString().Trim();
                        ViewState["BalanceCgst"] = row["Balance"].ToString();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtCGSTAMT.Text.ToString().Trim())));
                    }

                    row["Amount"] = "0.00";

                }
                row["Mode"] = TranMode.ToString().Trim();
                //row["Id"] = hdnIdEditParty.Value;
                row["Id"] = partyNo;
                row["OppParty"] = Convert.ToInt32(txtCGST.Text.Trim().ToString().Split('*')[1]).ToString();
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;
                }
                else
                {
                    row["BudgetNo"] = 0;
                    // row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
                row["ProjectSubId"] = ddlProjSubHead.SelectedValue;
                //Added by Vijay andoju 07-07-2020
                //row["Section"] = 0;

                row["TDSAMOUNT"] = "0";
                row["TDSsection"] = "0";
                row["TDSpercentage"] = "0";

                //added by vijay andoju on 26082020 fro IGST
                row["IGSTAmount"] = "0";
                row["IGSTper"] = "0";
                row["IGSTonAmount"] = "0";

                row["CGSTAmount"] = txtCGSTAMT.Text;
                row["CGSTper"] = txtCgstPer.Text;
                row["CGSTonAmount"] = txtCgstOnAmount.Text;

                row["SGSTAmount"] = "0";
                row["SGSTper"] = "0";
                row["SGSTonAmount"] = "0";

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
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
                row["Particulars"] = txtSGST.Text.ToString().Trim();
                row["Narration"] = txtPerNarration.Text.ToString().Trim().Replace("¯", "");

                if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                {
                    TranMode = "Cr";

                }
                else
                {
                    TranMode = "Dr";
                }

                if (Convert.ToDouble(txtTranAmt.Text) < 0)
                {
                    if (ddlcrdr.SelectedValue.ToString().Trim() == "Cr")
                    {
                        TranMode = "Dr";
                    }
                    else
                    {
                        TranMode = "Cr";
                    }
                }
                string partyNo = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtSGST.Text.ToString().Trim().Split('*')[1] + "'");
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
                    ViewState["BalanceSgst"] = tdsAmt.ToString();// dtr["BALANCE"].ToString().Trim();
                }
                if (count == 0)
                {
                    string balance = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Id"].ToString() == partyNo)
                        {

                            if (dt.Rows[i]["Mode"].ToString() == "Dr")
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["BalanceSgst"].ToString()) + Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["BalanceSgst"] = balance;
                            }
                            else
                            {
                                balance = Convert.ToString(Convert.ToDouble(ViewState["BalanceSgst"].ToString()) - Convert.ToDouble(dt.Rows[i]["Amount"].ToString()));
                                ViewState["BalanceSgst"] = balance;
                            }

                        }
                    }
                }

                if (TranMode.ToString().Trim() == "Dr")
                {
                    if (ViewState["BalanceSgst"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["BalanceSgst"].ToString().Trim()) + Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim()))).ToString().Trim();
                        ViewState["BalanceSgst"] = row["Balance"].ToString();
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
                    if (ViewState["BalanceSgst"].ToString().Trim() != "")
                    {
                        row["Balance"] = (Convert.ToDouble(ViewState["BalanceSgst"].ToString().Trim()) - Math.Abs(Convert.ToDouble(txtSGSTAMT.Text))).ToString().Trim();
                        ViewState["BalanceSgst"] = row["Balance"].ToString();
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
                    row["Amount"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                }
                else
                {
                    GridData.Columns[4].Visible = false;
                    GridData.Columns[5].Visible = true;
                    GridData.Columns[6].Visible = true;

                    if (TranMode.ToString().Trim() == "Dr")
                    {
                        row["Debit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
                        row["Credit"] = "0.00";
                    }
                    else
                    {
                        row["Debit"] = "0.00";
                        row["Credit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(txtSGSTAMT.Text.ToString().Trim())));
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
                    if (ddlCostCenter.SelectedIndex > 0)
                    {

                        row["CCID"] = ddlCostCenter.SelectedValue;
                    }
                    else
                    {
                        row["CCID"] = 0;
                    }
                }
                else
                {
                    row["CCID"] = 0;
                }

                if (divDeptBudget.Visible == true)
                {
                    row["BudgetNo"] = ddlBudgetHead.SelectedValue;
                    // row["Departmentid"] = ddldepartment.SelectedValue;

                }
                else
                {
                    row["BudgetNo"] = 0;
                    //row["Departmentid"] = "0";
                }
                row["Departmentid"] = ddldepartment.SelectedValue;
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


                row["SGSTAmount"] = txtSGSTAMT.Text;
                row["SGSTper"] = txtSGTSPer.Text;
                row["SGSTonAmount"] = txtSgstOnAmount.Text;

                //added by Gopal Anthati on 01-09-2021
                row["TdsOnCgstAmt"] = "0";
                row["TdsOnCgstSection"] = "0";
                row["TdsOnCgstPer"] = "0";
                row["TdsCgstOnAmt"] = "0";

                row["TdsOnSgstAmt"] = "0";
                row["TdsOnSgstSection"] = "0";
                row["TdsOnSgstPer"] = "0";
                row["TdsSgstOnAmt"] = "0";

                row["TdsOnIgstAmt"] = "0";
                row["TdsOnIgstSection"] = "0";
                row["TdsOnIgstPer"] = "0";
                row["TdsIgstOnAmt"] = "0";

                row["SecurityAmt"] = "0";

                if (ddlEmpType.SelectedValue == "1")
                {
                    row["IsEmployee"] = "1";
                    row["TagEmpIdno"] = ddlEmployee.SelectedValue;
                    row["TagEmployee"] = ddlEmployee.SelectedItem.Text.Trim();
                }
                else if (ddlEmpType.SelectedValue == "2")
                {
                    row["IsEmployee"] = "2";
                    row["TagEmpIdno"] = ddlPayee.SelectedValue;
                    row["TagEmployee"] = ddlPayee.SelectedItem.Text.Trim();
                }
                else
                {
                    row["IsEmployee"] = "";
                    row["TagEmpIdno"] = "";
                    row["TagEmployee"] = string.Empty;
                }
                row["PrevAdvanceId"] = ddlPrevAdv.SelectedValue == "" ? "0" : ddlPrevAdv.SelectedValue;
                dt.Rows.Add(row);
                Session["Datatable"] = dt;
            }
        }


    }

    protected void txtTDSPer_TextChanged(object sender, EventArgs e)
    {
        if (txtTranAmt.Text != string.Empty || txtTranAmt.Text != "")
        {
            if (chkTDSApplicable.Checked == true)
            {
                if (txtTDSPer.Text != string.Empty || txtTDSPer.Text != "")
                {
                    if (Convert.ToDouble(txtTDSPer.Text) >= 100)
                    {
                        objCommon.DisplayMessage(UPDLedger, "TDS Percentage Can't be greater than or equal to 100%", this.Page);
                        txtTDSAmount.Text = string.Empty;
                        txtTDSPer.Text = string.Empty;
                        txtTDSPer.Focus();
                        return;
                    }
                    double TransAmt = Convert.ToDouble(txtTranAmt.Text);
                    double tdsPer = Convert.ToDouble(txtTDSPer.Text);

                    string calc = String.Format("{0:0.00}", Convert.ToDouble(TransAmt * tdsPer * 0.01));
                    txtTDSAmount.Text = calc.ToString();
                }
                else
                {
                    txtTDSAmount.Text = string.Empty;
                    txtTDSPer.Text = string.Empty;
                    txtTDSPer.Focus();
                }
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
            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for TDS which was already selected.", this);
            txtTDSLedger.Text = string.Empty;
            txtTDSLedger.Focus();
            return;
        }
    }
    protected void txtTranAmt_TextChanged(object sender, EventArgs e)
    {

        if (txtTranAmt.Text != string.Empty || txtTranAmt.Text != "")
        {
            //addeed by vijay on 1309200
            clearTax();
            if (chkTDSApplicable.Checked == true)
            {
                txtTdsOnAmount.Text = Convert.ToDouble(txtTranAmt.Text).ToString();
                if (txtTDSPer.Text != string.Empty || txtTDSPer.Text != "")
                {

                    if (Convert.ToDouble(txtTDSPer.Text) >= 100)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "TDS Percentage Can't be greater than or equal to 100%", this);
                        txtTDSPer.Text = string.Empty;
                        txtTDSLedger.Text = string.Empty;
                        txtTDSPer.Focus();
                        return;
                    }
                    double TransAmt = Convert.ToDouble(txtTranAmt.Text);
                    double tdsPer = Convert.ToDouble(txtTDSPer.Text);

                    string calc = String.Format("{0:0.00}", Convert.ToDouble(TransAmt * tdsPer * 0.01));
                    txtTDSAmount.Text = calc.ToString();
                }
                else
                {
                    txtTDSAmount.Text = string.Empty;
                    txtTDSPer.Text = string.Empty;
                    txtTDSPer.Focus();
                }
            }
            if (chkGST.Checked == true)
            {

                txtCgstOnAmount.Text = txtSgstOnAmount.Text = Convert.ToDouble(txtTranAmt.Text).ToString();
            }
            if (chkIGST.Checked == true)
            {

                txtIgstOnAmount.Text = Convert.ToDouble(txtTranAmt.Text).ToString();

            }
        }
    }
    //added by vijay andoju for clearing the all Tax
    protected void clearTax()
    {
        txtTDSPer.Text = txtTdsOnAmount.Text =
        txtCGSTAMT.Text = txtCgstPer.Text =
        txtSGSTAMT.Text = txtSGTSPer.Text =
        txtIGSTAMT.Text = txtIGSTPER.Text = string.Empty;
        ddlTdsSection.SelectedValue = "0";
    }
    protected void ddlTdsSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTDSPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlTdsSection.SelectedValue);
        // decimal TDSOnAmount = Convert.ToDecimal(txtTdsOnAmount.Text) / 100;
        int TDSOnAmount = Convert.ToInt32(txtTdsOnAmount.Text) / 100;
        decimal Per = txtTDSPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTDSPer.Text);
        decimal Total = TDSOnAmount * Per;
        txtTDSAmount.Text = Total.ToString();

    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // objCostCenterController.BindBudgetHead(ddlBudgetHead);


    }
    protected void chkGst_CheckedChanged(object sender, EventArgs e)
    {
        chkIGST.Checked = false;
        chkTdsOnIGST.Checked = false;
        clearGst();
        if (chkGST.Checked == true)
        {
            // divgstno.Visible = true;
            divgst.Visible = true;
            divcgst.Visible = true;
            txtCgstOnAmount.Text = txtSgstOnAmount.Text = Convert.ToDouble(txtTranAmt.Text == string.Empty ? "0" : txtTranAmt.Text).ToString();
            //  txtSGST.Focus();
            ViewState["IsIGST"] = "No";
            ViewState["IsGST"] = "Yes";

        }
        else
        {
            divgst.Visible = false;
            divcgst.Visible = false;
            ViewState["IsGST"] = "No";

        }


    }
    protected void chkIGST_CheckedChanged(object sender, EventArgs e)
    {
        chkGST.Checked = false;
        chkTdsOnGST.Checked = false;
        clearGst();

        chkTdsOnIGST.Checked = false;
        divTdsOnIgst.Visible = false;

        if (chkIGST.Checked == true)
        {
            // divgstno.Visible = true;
            divIgst.Visible = true;
            txtIgstOnAmount.Text = Convert.ToDouble(txtTranAmt.Text == string.Empty ? "0" : txtTranAmt.Text).ToString();
            ViewState["IsIGST"] = "Yes";
            ViewState["IsGST"] = "No";
        }
        else
        {
            divIgst.Visible = false;
            ViewState["IsIGST"] = "No";

        }


    }
    protected void txtCGSTPER_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtSGTSPer_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtCGST_TextChanged1(object sender, EventArgs e)
    {

    }
    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {

    }

    public void clearGst()
    {
        divgst.Visible = false;
        divIgst.Visible = false;
        divcgst.Visible = false;
        divTdsOnCgst.Visible = false;
        divTdsOnSgst.Visible = false;
        divTdsOnCgst.Visible = false;
        divTdsOnSgst.Visible = false;
        divTdsOnIgst.Visible = false;
        divSecurity.Visible = false;
    }


    protected void chkTdsOnGST_CheckedChanged(object sender, EventArgs e)
    {
        txtTdsOnCgstAmt.Text = txtTdsCgstOnAmt.Text = string.Empty;
        txtTdsOnSgstAmt.Text = txtTdsSgstOnAmt.Text = string.Empty;
        txtTdsOnCgstPer.Text = txtTdsOnSgstPer.Text = string.Empty;
        if (chkGST.Checked == true)
        {
            if (chkTdsOnGST.Checked)
            {
                txtTdsCgstOnAmt.Text = Convert.ToDouble(txtTranAmt.Text == string.Empty ? "0" : txtTranAmt.Text).ToString();
                txtTdsSgstOnAmt.Text = Convert.ToDouble(txtTranAmt.Text == string.Empty ? "0" : txtTranAmt.Text).ToString();
                objCommon.FillDropDownList(ddlTdsOnCgstSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
                divTdsOnCgst.Visible = true;
                objCommon.FillDropDownList(ddlTdsOnSgstSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
                divTdsOnSgst.Visible = true;
                // txtTdsOnCgstAcc.Focus();
                ViewState["IsTdsOnGst"] = "Yes";
            }
            else
            {
                divTdsOnCgst.Visible = false;
                divTdsOnSgst.Visible = false;
                ViewState["IsTdsOnGst"] = "No";
            }
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select GST(Y/N)", this);
            return;
        }
    }
    protected void chkTdsOnIGST_CheckedChanged(object sender, EventArgs e)
    {
        txtTdsOnIgstAmt.Text = txtTdsIgstOnAmt.Text = txtTdsOnIgstPer.Text = string.Empty;
        if (chkIGST.Checked == true)
        {
            if (chkTdsOnIGST.Checked)
            {
                objCommon.FillDropDownList(ddlTdsOnIgstSection, "acc_" + Session["comp_code"].ToString() + "_section", "SECTION_NO", "SECTION_NAME", "", "");
                divTdsOnIgst.Visible = true;
                txtTdsIgstOnAmt.Text = Convert.ToDouble(txtTranAmt.Text == string.Empty ? "0" : txtTranAmt.Text).ToString();
                // txtTdsOnIgstAcc.Focus();
                ViewState["IsTdsOnIgst"] = "Yes";
            }
            else
            {
                divTdsOnIgst.Visible = false;
                ViewState["IsTdsOnIgst"] = "No";
            }

        }
        else
        {
            divTdsOnIgst.Visible = false;
            objCommon.DisplayMessage(UPDLedger, "Please Select IGST(Y/N)", this);
            chkTdsOnIGST.Checked = false;
        }
    }
    protected void chkSecurity_CheckedChanged(object sender, EventArgs e)
    {
        txtSecurityAmt.Text = "0";
        if (chkSecurity.Checked == true)
        {
            // divgstno.Visible = true;
            divSecurity.Visible = true;
            //  txtSecurityAmt.Focus();
            ViewState["IsSecurity"] = "Yes";
        }
        else
        {
            divSecurity.Visible = false;
            ViewState["IsSecurity"] = "No";

        }
    }
    protected void txtTdsOnCgstAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text == txtTdsOnCgstAcc.Text)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for TDS On GST which was already selected ", this);
            txtTdsOnCgstAcc.Text = string.Empty;
            // txtTdsOnCgstAcc.Focus();
            return;
        }
    }
    protected void txtTdsOnSgstAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text == txtTdsOnSgstAcc.Text)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for TDS On SGST which was already selected ", this);
            txtTdsOnSgstAcc.Text = string.Empty;
            // txtTdsOnSgstAcc.Focus();
            return;
        }
    }
    protected void ddlTdsOnCgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTdsOnCgstPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlTdsOnCgstSection.SelectedValue);
        decimal TDSCgstOnAmount = Convert.ToDecimal(txtTdsCgstOnAmt.Text) / 100;
        decimal Per = txtTdsOnCgstPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTdsOnCgstPer.Text);
        decimal Total = TDSCgstOnAmount * Per;
        txtTdsOnCgstAmt.Text = Total.ToString();
    }
    protected void ddlTdsOnSgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTdsOnSgstPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlTdsOnSgstSection.SelectedValue);
        decimal TDSSgstOnAmount = Convert.ToDecimal(txtTdsSgstOnAmt.Text) / 100;
        decimal Per = txtTdsOnSgstPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTdsOnSgstPer.Text);
        decimal Total = TDSSgstOnAmount * Per;
        txtTdsOnSgstAmt.Text = Total.ToString();
    }
    protected void txtTdsOnIgstAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text == txtTdsOnIgstAcc.Text)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for TDS On Gst which was already selected ", this);
            txtTdsOnIgstAcc.Text = string.Empty;
            //  txtTdsOnIgstAcc.Focus();
            return;
        }
    }
    protected void ddlTdsOnIgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTdsOnIgstPer.Text = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO=" + ddlTdsOnIgstSection.SelectedValue);
        decimal TDSIGstOnAmount = Convert.ToDecimal(txtTdsIgstOnAmt.Text) / 100;
        decimal Per = txtTdsOnIgstPer.Text == string.Empty ? Convert.ToDecimal("0") : Convert.ToDecimal(txtTdsOnIgstPer.Text);
        decimal Total = TDSIGstOnAmount * Per;
        txtTdsOnIgstAmt.Text = Total.ToString();
    }
    protected void txtSecurityAcc_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text == txtSecurityAcc.Text)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Can't be same Ledger for Security which was already selected ", this);
            txtSecurityAcc.Text = string.Empty;
            //  txtSecurityAcc.Focus();
            return;
        }
    }
    protected void ddlPayeeNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");
    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divPrevAdv1.Visible = false;
        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
        divPayee1.Visible = false;
        divPayee2.Visible = false;
        divPayeeNature1.Visible = false;
        divPayeeNature2.Visible = false;

        ddlEmployee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;
        ddlPayee.SelectedIndex = 0;
        ddlPrevAdv.SelectedIndex = 0;
        if (ddlEmpType.SelectedValue == "1")
        {
            divEmployee1.Visible = true;
            divEmployee2.Visible = true;

            divPayee1.Visible = false;
            divPayee2.Visible = false;
            divPayeeNature1.Visible = false;
            divPayeeNature2.Visible = false;
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
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
        divPrevAdv1.Visible = false;
        //if (ddlTranType.SelectedValue == "J" || ddlTranType.SelectedValue == "R")
        //{
        //    isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

        //    if (isTempVoucher == "Y")
        //    {
        //        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='P'", "");
        //        divPrevAdv1.Visible = true;
        //    }
        //    else
        //    {
        //        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='P'", "");
        //        divPrevAdv1.Visible = true;
        //    }
        //    //divPrevAdv2.Visible = true;
        //}
        if (ddlTranType.SelectedValue == "P")
        {
            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

            if (isTempVoucher == "Y")
            {
                objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                divPrevAdv1.Visible = true;
            }
            else
            {
                objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                divPrevAdv1.Visible = true;
            }
            //divPrevAdv2.Visible = true;
        }
        else if (ddlTranType.SelectedValue == "R")
        {
            if (IsTagForRecipt == "Y")
            {
                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                if (isTempVoucher == "Y")
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                    divPrevAdv1.Visible = true;
                }
                else
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='J'", "");
                    divPrevAdv1.Visible = true;
                }
            }
        }
        else if (ddlTranType.SelectedValue == "J")
        {
            if (IsTagForJournal == "Y")
            {
                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                if (isTempVoucher == "Y")
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='P'", "");
                    divPrevAdv1.Visible = true;
                }
                else
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlEmployee.SelectedValue + " AND IS_EMPLOYEE=1 AND TRANSACTION_TYPE='P'", "");
                    divPrevAdv1.Visible = true;
                }
            }
        }
        else
        {
            divPrevAdv1.Visible = false;
            //divPrevAdv2.Visible = false;
        }
    }
    protected void ddlPayee_SelectedIndexChanged(object sender, EventArgs e)
    {
        divPrevAdv1.Visible = false;
        //if (ddlTranType.SelectedValue == "J" || ddlTranType.SelectedValue == "R")
        //{
        //    isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

        //    if (isTempVoucher == "Y")
        //    {
        //        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='P'", "");
        //        divPrevAdv1.Visible = true;
        //    }
        //    else
        //    {
        //        objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='P'", "");
        //        divPrevAdv1.Visible = true;
        //    }


        //    //divPrevAdv2.Visible = true;
        //}
        if (ddlTranType.SelectedValue == "P")
        {
            isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

            if (isTempVoucher == "Y")
            {
                objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                divPrevAdv1.Visible = true;
            }
            else
            {
                objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                divPrevAdv1.Visible = true;
            }
        }
        else if (ddlTranType.SelectedValue == "R")
        {
            if (IsTagForRecipt == "Y")
            {
                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                if (isTempVoucher == "Y")
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                    divPrevAdv1.Visible = true;
                }
                else
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='J'", "");
                    divPrevAdv1.Visible = true;
                }
            }
        }
        else if (ddlTranType.SelectedValue == "J")
        {
            if (IsTagForJournal == "Y")
            {
                isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");

                if (isTempVoucher == "Y")
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER_TEMP", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='P'", "");
                    divPrevAdv1.Visible = true;
                }
                else
                {
                    objCommon.FillDropDownList(ddlPrevAdv, "ACC_VOUCHER_TAG_USER", "TAG_USER_ID", "'VoucherNo'+'*'+ cast(VOUCHER_NO as nvarchar(16))+'-------'+cast(AMOUNT  as nvarchar(16))", "IDNO=" + ddlPayee.SelectedValue + " AND IS_EMPLOYEE=2 AND TRANSACTION_TYPE='P'", "");
                    divPrevAdv1.Visible = true;
                }
            }
        }
        else
        {
            divPrevAdv1.Visible = false;
            //divPrevAdv2.Visible = false;
        }
    }
    protected void ddlPrevAdv_SelectedIndexChanged(object sender, EventArgs e)
    {
        isTempVoucher = objCommon.LookUp("ACC_MAIN_CONFIGURATION", "IsTempVoucher", "");
        if (isTempVoucher == "Y")
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACC_VOUCHER_TAG_USER_TEMP", "AMOUNT", "", "TAG_USER_ID=" + ddlPrevAdv.SelectedValue, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dt = null;
                dt = objCommon.FillDropDown("ACC_VOUCHER_TAG_USER_TEMP", "sum(AMOUNT)", "sum(BALANCE_AMOUNT)", "PARENT_ID=" + ddlPrevAdv.SelectedValue, "");
                if (dt.Tables[0].Rows.Count > 0)
                {
                    if (dt.Tables[0].Rows[0]["AMOUNT"] != DBNull.Value && ds.Tables[0].Rows[0]["AMOUNT"] != DBNull.Value)
                    {

                        double dtAmount = Convert.ToDouble(dt.Tables[0].Rows[0]["AMOUNT"]);
                        double dsAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["AMOUNT"]);
                        if (dtAmount == dsAmount)
                        {
                            LblBalanceAmount.Text = null;
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "OnPrevAdv('" + 1 + "')", true);
                        }
                        else if (dtAmount > dsAmount)
                        {
                            LblBalanceAmount.Text = null;
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "OnPrevAdv('" + 1 + "')", true);
                        }
                        else
                        {
                            LblBalanceAmount.Visible = true;
                            if (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString() != "")
                            {
                                LblBalanceAmount.Text = (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString());
                            }
                            else
                            {
                                LblBalanceAmount.Text = (ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                            }
                        }
                    }
                    else
                    {
                        LblBalanceAmount.Visible = true;
                        if (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString() != "")
                        {
                            LblBalanceAmount.Text = (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString());
                        }
                        else
                        {
                            LblBalanceAmount.Text = (ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                        }
                    }
                }
            }
        }
        else
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACC_VOUCHER_TAG_USER", "AMOUNT", "", "TAG_USER_ID=" + ddlPrevAdv.SelectedValue, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dt = null;
                dt = objCommon.FillDropDown("ACC_VOUCHER_TAG_USER", "sum(isnull(AMOUNT,0))AMOUNT", "sum(isnull(BALANCE_AMOUNT,0))BALANCE_AMOUNT", "PARENT_ID=" + ddlPrevAdv.SelectedValue, "");
                if (dt.Tables[0].Rows.Count > 0)
                {
                    if (dt.Tables[0].Rows[0]["AMOUNT"] != DBNull.Value && ds.Tables[0].Rows[0]["AMOUNT"] != DBNull.Value)
                    {

                        double dtAmount = Convert.ToDouble(dt.Tables[0].Rows[0]["AMOUNT"]);
                        double dsAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["AMOUNT"]);
                        if (dtAmount == dsAmount)
                        {
                            LblBalanceAmount.Text = null;
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "OnPrevAdv('" + 1 + "')", true);
                        }
                        else if (dtAmount > dsAmount)
                        {
                            LblBalanceAmount.Text = null;
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "OnPrevAdv('" + 1 + "')", true);
                        }
                        else
                        {
                            LblBalanceAmount.Visible = true;
                            if (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString() != "")
                            {
                                LblBalanceAmount.Text = (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString());
                            }
                            else
                            {
                                LblBalanceAmount.Text = (ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                            }
                        }
                    }
                    else
                    {
                        LblBalanceAmount.Visible = true;
                        if (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString() != "")
                        {
                            LblBalanceAmount.Text = (dt.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString());
                        }
                        else
                        {
                            LblBalanceAmount.Text = (ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                        }
                    }
                }

            }
        }

    }
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
    //added by tanu 13/12/2021 for add multipal bill files 
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


    #region MultiCostCenter
    protected void clearMultiCCActivity()
    {
        ddlmuliicostcenter.SelectedIndex = 0;
        txtccAmount.Text = string.Empty;

    }

    private DataTable CreateTabelMultiCCActivity()
    {
        DataTable dtotheractivity = new DataTable();
        dtotheractivity.Columns.Add(new DataColumn("CC_ID", typeof(int)));
        dtotheractivity.Columns.Add(new DataColumn("CostCenter", typeof(string)));
        dtotheractivity.Columns.Add(new DataColumn("CostCenterID", typeof(int)));
        dtotheractivity.Columns.Add(new DataColumn("PartyID", typeof(int)));
        dtotheractivity.Columns.Add(new DataColumn("PartyName", typeof(string)));
        dtotheractivity.Columns.Add(new DataColumn("Amount", typeof(decimal)));

        return dtotheractivity;
    }

    protected void btnaddmulticostcenter_Click(object sender, EventArgs e)
    {

        if (txtAcc.Text == "")
        {
            if (ViewState["isModi"].ToString() == "Y")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Ledger.');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Ledger.');", true);
                return;
            }
        }

        if (ddlmuliicostcenter.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Cost Center.');", true);
            return;
        }

        if (txtccAmount.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Amount.');", true);
            return;
        }
        if (txtTranAmt.Text != "")
        {
            if (Convert.ToDouble(txtccAmount.Text) > Convert.ToDouble(txtTranAmt.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The Cost Center Amount Should be Less than or Equal to Transaction Amount.');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Amount.');", true);
            return;
        }
        foreach (ListViewDataItem lvItem in lvMultiCC.Items)
        {
            double totalCCAmount = 0;
            HiddenField PartyNo = lvItem.FindControl("hdMulticcPartyId") as HiddenField;
            Label lblAmount = lvItem.FindControl("lblAmount") as Label;
            if (txtAcc.Text != "")
            {
                if(Convert.ToInt32(txtAcc.Text.ToString().Trim().Split('*')[1])==Convert.ToInt32(PartyNo.Value))
                {
                    totalCCAmount = totalCCAmount + Convert.ToInt32(lblAmount.Text) + Convert.ToDouble(txtccAmount.Text);
                    if (Convert.ToInt32(totalCCAmount) > Convert.ToDouble(txtTranAmt.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The Total Cost Center Amount Should be Less than or Equal to Transaction Amount.');", true);
                        return;
                    }
                }
            }
        }

        lvMultiCC.Visible = true;
        if (ViewState["RecTblMultiCCActivity"] != null && ((DataTable)ViewState["RecTblMultiCCActivity"]) != null)
        {
            int maxVals = 0;
            DataTable dtMuliccactivity = (DataTable)ViewState["RecTblMultiCCActivity"];
            DataRow dr = dtMuliccactivity.NewRow();
            if (dr != null)
            {
                maxVals = Convert.ToInt32(dtMuliccactivity.AsEnumerable().Max(row => row["CC_ID"]));
            }

            dr["CC_ID"] = maxVals + 1;
            dr["CostCenter"] = ddlmuliicostcenter.SelectedItem.ToString();
            dr["CostCenterID"] = ddlmuliicostcenter.SelectedValue;
            dr["PartyID"] = Convert.ToInt32(txtAcc.Text.ToString().Trim().Split('*')[1]);
            dr["PartyName"] = (txtAcc.Text.ToString());
            dr["Amount"] = Convert.ToDouble(txtccAmount.Text);


            dtMuliccactivity.Rows.Add(dr);
            ViewState["RecTblMultiCCActivity"] = dtMuliccactivity;
            lvMultiCC.DataSource = dtMuliccactivity;
            lvMultiCC.DataBind();
            clearMultiCCActivity();

            ViewState["CC_ID"] = Convert.ToInt32(ViewState["CC_ID"]) + 1;
        }
        else
        {

            DataTable dtMuliccactivity = this.CreateTabelMultiCCActivity();
            DataRow dr = dtMuliccactivity.NewRow();
            dr["CC_ID"] = Convert.ToInt32(ViewState["CC_ID"]) + 1;
            dr["CostCenter"] = ddlmuliicostcenter.SelectedItem.ToString();
            dr["CostCenterID"] = ddlmuliicostcenter.SelectedValue;
            dr["PartyID"] = Convert.ToInt32(txtAcc.Text.ToString().Trim().Split('*')[1]);
            dr["PartyName"] = (txtAcc.Text.ToString());
            dr["Amount"] = Convert.ToDouble(txtccAmount.Text);

            ViewState["CC_ID"] = Convert.ToInt32(ViewState["CC_ID"]) + 1;
            dtMuliccactivity.Rows.Add(dr);
            clearMultiCCActivity();
            ViewState["RecTblMultiCCActivity"] = dtMuliccactivity;
            lvMultiCC.DataSource = dtMuliccactivity;
            lvMultiCC.DataBind();
        }
    }

    private DataRow GetEditableDatarowMultiCCactivity(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CC_ID"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromStudentAttendance -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    protected void btneditMultCC_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btneditMultCC = sender as ImageButton;
        DataTable dtMuliccactivity;

        dtMuliccactivity = ((DataTable)ViewState["RecTblMultiCCActivity"]);
        if (dtMuliccactivity != null)
        {
            DataRow dr = this.GetEditableDatarowMultiCCactivity(dtMuliccactivity, btneditMultCC.CommandArgument);


            dtMuliccactivity.Rows.Remove(dr);
            ViewState["RecTblMultiCCActivity"] = dtMuliccactivity;
            lvMultiCC.DataSource = dtMuliccactivity;
            lvMultiCC.DataBind();
        }
        else
        {
            ViewState["RecTblMultiCCActivity"] = null;
            lvMultiCC.DataSource = null;
            lvMultiCC.DataBind();
        }
    }

    #endregion

}
