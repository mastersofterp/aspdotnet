//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : LEDGER HEAD                                                     
// CREATION DATE : 02-SEPTEMBER-2009                                               
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;


public partial class ChequePrintingTransaction : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenter objCostCenter = new CostCenter();
    CostCenterController objCostCenterController = new CostCenterController();
    public string back = string.Empty;
    string IsAutogenratedLedgerAccountCode = string.Empty;
    string accountCode = "00";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");

            }
            else
            {
                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Menu aa = (Menu)Page.Master.FindControl("mainMenu");
        aa.Visible = false;

        if (Request.QueryString["obj"] != null)   /// Eknath
        { back = Request.QueryString["obj"].ToString().Trim(); }
        //txtContactNo.Attributes.Add("onkeydown", "return CheckNumeric(this);");
        //txtStatus.Attributes.Add("onfocus", "return getFocus(this);");
        if (Session["comp_code"] != null)
        {
            SetParameters();
            //ChangeThePageLayout();
        }

        if (!Page.IsPostBack)
        {

            txtLedgerName.Focus();
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                //  Response.Redirect("~/default.aspx");
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
                    ChangeThePageLayout();

                    PopulateListBox();
                    ViewState["ChkEdit"] = "N";
                    ViewState["action"] = "add";
                }
            }
            btnSave.Visible = false;
            //btnGenerate.Visible = false;
            //ViewState["Amount"] = "0";
            //Page.ClientScript.RegisterHiddenField("hfHiddenFieldID", ViewState["Amount"].ToString());
            hfHiddenFieldID.Value = "0";
            lblBankName.Text = Request.QueryString["obj"].Split(',')[3];
           //lblAmount.Text = Request.QueryString["obj"].Split(',')[4];
             lblAmount.Text = ViewState["Amount1"].ToString();
        }

        //divMsg.InnerHtml = string.Empty;


    }
    //used to auto increament the ledger account code based on the configuration set


    private void EnabledDisabledStatusDrCr()
    {

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            double amt1 = Convert.ToDouble(txtAmount.Text);
            double amt2 = Convert.ToDouble(lblAmount.Text);
            if (amt1 > amt2)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Amount Can not Greater than Actual Amount", this.Page);
                return;
            }

            if (Convert.ToInt32(txtCHQNo.Text) <= 0 || txtCHQNo.Text.Length < 6)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Cheque No. should be in 6 digits", this.Page);
                return;
            }

            if (Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"] + "_CHECK_PRINT", "count(*)", "CHECKNO='" + txtCHQNo.Text + "'")) > 0)
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Cheque No. already exist", this.Page);
                return;
            }
            if (ViewState["ChkEdit"].ToString() != "Y")
            {
                if (txtAmount.Text != "" && txtLedgerName.Text != "")
                {
                    if (gvChqDetails.Rows.Count == 0)
                    {
                        if (Convert.ToDecimal(txtAmount.Text) > 0)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("PARTY_NO", typeof(string));
                            dt.Columns.Add("VOUCHER_NO", typeof(string));
                            dt.Columns.Add("CHQ_NO", typeof(string));
                            dt.Columns.Add("CHQ_DATE", typeof(string));
                            dt.Columns.Add("PARTY_NAME", typeof(string));
                            dt.Columns.Add("AMOUNT", typeof(string));
                            dt.Columns.Add("Status", typeof(string));
                            if (chkStamp.Checked)
                                dt.Rows.Add(ViewState["PARTY_NO"], txtVCHNO.Text, txtCHQNo.Text, txtDate.Text, txtLedgerName.Text, txtAmount.Text, "True");

                            else
                                dt.Rows.Add(ViewState["PARTY_NO"], txtVCHNO.Text, txtCHQNo.Text, txtDate.Text, txtLedgerName.Text, txtAmount.Text, "False");
                            //if (Convert.ToDecimal(ViewState["Amount1"].ToString()) != Convert.ToDecimal(txtAmount.Text))
                            //{
                            //    dt.Rows.Add(ViewState["PARTY_NO"], txtVCHNO.Text, "", "", ViewState["Party_Name"].ToString(), Convert.ToDecimal(ViewState["Amount"].ToString()) - Convert.ToDecimal(txtAmount.Text), "False");
                            //}
                            if (ViewState["sumamount"] != null)
                                ViewState["sumamount"] = Convert.ToDecimal(ViewState["sumamount"].ToString()) + Convert.ToDecimal(txtAmount.Text);
                            else
                                ViewState["sumamount"] = Convert.ToDecimal("0.00") + Convert.ToDecimal(txtAmount.Text);

                            ViewState["datatable"] = dt;
                            gvChqDetails.DataSource = dt;
                            gvChqDetails.DataBind();
                            txtAmount.Text = (Convert.ToDecimal(ViewState["Amount1"].ToString()) - Convert.ToDecimal(ViewState["sumamount"].ToString())).ToString();

                            //hfHiddenFieldID.Value = txtAmount.Text;
                            //decimal sumObject = 0;
                            //sumObject=dt.Compute("Sum(AMOUNT)", "");

                            //foreach (DataRow dr in dt.Rows)
                            //{
                            //    sumObject += Convert.ToDecimal(dr["Amount"]);
                            //}
                            //ViewState["Amount1"] = Convert.ToDecimal(ViewState["Amount"].ToString()) - Convert.ToDecimal(sumObject);
                            //Clear();

                        }
                    }
                    else
                    {
                        DataTable dt = ViewState["datatable"] as DataTable;

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        if (Convert.ToDecimal(txtAmount.Text) > 0)
                        {
                            //dt.Rows[0]["Amount"] = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString()) - Convert.ToDecimal(txtAmount.Text);
                            dt.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), txtCHQNo.Text, txtDate.Text, txtLedgerName.Text, txtAmount.Text, "True");

                            ViewState["sumamount"] = Convert.ToDecimal(ViewState["sumamount"].ToString()) + Convert.ToDecimal(txtAmount.Text);

                            txtAmount.Text = (Convert.ToDecimal(ViewState["Amount1"].ToString()) - Convert.ToDecimal(ViewState["sumamount"].ToString())).ToString();
                            //decimal sumObject = 0;
                            ////sumObject=dt.Compute("Sum(AMOUNT)", "");

                            //foreach (DataRow dr in dt.Rows)
                            //{
                            //    sumObject += Convert.ToDecimal(dr["Amount"]);
                            //}
                            //ViewState["Amount1"] = Convert.ToDecimal(ViewState["Amount"].ToString()) - sumObject;

                        }
                        //}
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    if (dt.Rows[i]["Amount"].ToString() != "")
                        //    {
                        //        if (Convert.ToDecimal(dt.Rows[i]["Amount"].ToString()) == 0)
                        //        {
                        //            //dt.Rows[i].Delete();
                        //            DataRow dr = dt.Rows[i];
                        //            dt.Rows.Remove(dr);
                        //            dt.AcceptChanges();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        DataRow dr = dt.Rows[i];
                        //        dt.Rows.Remove(dr);
                        //        dt.AcceptChanges();
                        //    }
                        //}
                        gvChqDetails.DataSource = dt;
                        gvChqDetails.DataBind();
                        //hfHiddenFieldID.Value = dt.Rows[0]["Amount"].ToString();
                    }
                }
                txtLedgerName.Focus();
            }
            else
            {
                if (ViewState["ChkEdit"].ToString() == "Y")
                {
                    DataTable dt = ViewState["datatable"] as DataTable;
                    if (ViewState["index"].ToString() != "")
                    {
                        decimal sumObject = 0;
                        int index = Convert.ToInt32(ViewState["index"].ToString());
                        dt.Rows[index]["CHQ_NO"] = txtCHQNo.Text;
                        dt.Rows[index]["CHQ_DATE"] = txtDate.Text;
                        dt.Rows[index]["PARTY_NAME"] = txtLedgerName.Text;
                        dt.Rows[index]["AMOUNT"] = txtAmount.Text;
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    sumObject += Convert.ToDecimal(dr["Amount"]);
                        //}
                        //ViewState["Amount1"] = Convert.ToDecimal(ViewState["Amount"].ToString()) - sumObject;
                        if (chkStamp.Checked == true)
                        {
                            dt.Rows[index]["Status"] = "True";
                        }
                        else
                        {
                            dt.Rows[index]["Status"] = "False";
                        }
                        //if (index == 0)
                        //{
                        //    //if (Convert.ToDecimal(txtAmount.Text) < Convert.ToDecimal(ViewState["Amount"].ToString()))
                        //    //{
                        //    if (Convert.ToDecimal(ViewState["Amount1"].ToString()) != 0)
                        //    {
                        //dt.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), txtCHQNo.Text, txtDate.Text, txtLedgerName.Text, txtAmount.Text, "True");

                        ViewState["sumamount"] = Convert.ToDecimal(ViewState["sumamount"].ToString()) + Convert.ToDecimal(txtAmount.Text);

                        txtAmount.Text = (Convert.ToDecimal(ViewState["Amount1"].ToString()) - Convert.ToDecimal(ViewState["sumamount"].ToString())).ToString();
                        //}
                        //}
                        //}
                        //else
                        //{
                        //    ViewState["sumamount"] = Convert.ToDecimal(ViewState["sumamount"].ToString()) + Convert.ToDecimal(txtAmount.Text);

                        //    txtAmount.Text = (Convert.ToDecimal(ViewState["Amount1"].ToString()) - Convert.ToDecimal(ViewState["sumamount"].ToString())).ToString();
                        //}

                        gvChqDetails.DataSource = dt;
                        gvChqDetails.DataBind();
                        sumObject = 0;
                        //sumObject=dt.Compute("Sum(AMOUNT)", "");

                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    sumObject += Convert.ToDecimal(dr["Amount"]);
                        //}
                        //ViewState["Amount1"] = Convert.ToDecimal(ViewState["Amount"].ToString()) - sumObject;
                    }

                }
                ViewState["ChkEdit"] = "N";

            }

            if (gvChqDetails.Rows.Count > 0)
            {
                btnSave.Visible = true;
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_selectCompany.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        //Response.Redirect(Request.Url.ToString());

    }


    #region User Defined Methods

    private void PopulateListBox()
    {
        try
        {
            string vchSqn = string.Empty;
            DataSet ds = new DataSet();

            string istemp = objCommon.LookUp("acc_main_configuration", "IsTempVoucher", "");
            if (istemp =="N")
            {
                vchSqn = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Request.QueryString["obj"].Split(',')[2]);
            }
            else
            {
                vchSqn = objCommon.LookUp("ACC_ALL_TEMP_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Request.QueryString["obj"].Split(',')[2]);
            }
            ViewState["TRANSACTION_NO"] = Request.QueryString["obj"].Split(',')[2].ToString();
            // ds = objCommon.FillDropDown("ACC_"+Session["comp_code"]+"_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
            //ds = objCommon.FillDropDown(" ACC_" + Session["comp_code"] + "_TRANS a inner join ACC_" + Session["comp_code"] + "_PARTY b on (a.PARTY_NO=b.PARTY_NO)", "top(1) a.PARTY_NO,a.OPARTY,a.TRANSACTION_DATE,a.VOUCHER_NO,a.CHQ_NO,CONVERT(nvarchar(20),a.CHQ_DATE,103)CHQ_DATE,b.PARTY_NAME,(select AMOUNT from ACC_" + Session["comp_code"] + "_TRANS where VOUCHER_SQN=" + vchSqn + " and SUBTR_NO<>0 and TRANSACTION_DATE between '" + Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy") + "') AMOUNT ", "a.ACC_PARTY_NAME", "a.VOUCHER_SQN=" + vchSqn + " AND a.SUBTR_NO = 0", "a.TRANSACTION_NO");
            ds = objCommon.FillDropDown(" ACC_" + Session["comp_code"] + "_TRANS a inner join ACC_" + Session["comp_code"] + "_PARTY b on (a.PARTY_NO=b.PARTY_NO)", "top(1) a.TRANSACTION_DATE,a.VOUCHER_NO,a.CHQ_NO,CONVERT(nvarchar(20),a.CHQ_DATE,103)CHQ_DATE,b.PARTY_NAME,(select PARTY_NO from ACC_" + Session["comp_code"] + "_TRANS where VOUCHER_SQN=" + vchSqn + " and SUBTR_NO<>0 and TRANSACTION_DATE between '" + Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy") + "')OPARTY,(select OPARTY from ACC_" + Session["comp_code"] + "_TRANS where VOUCHER_SQN=" + vchSqn + " and SUBTR_NO<>0 and TRANSACTION_DATE between '" + Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy") + "') PARTY_NO,(select AMOUNT from ACC_" + Session["comp_code"] + "_TRANS where VOUCHER_SQN=" + vchSqn + " and SUBTR_NO<>0 and TRANSACTION_DATE between '" + Convert.ToDateTime(Session["fin_date_from"].ToString()).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Session["fin_date_to"].ToString()).ToString("dd-MMM-yyyy") + "') AMOUNT ", "a.ACC_PARTY_NAME", "a.VOUCHER_SQN=" + vchSqn + " AND a.SUBTR_NO = 0", "a.TRANSACTION_NO");
            txtVCHNO.Text = ds.Tables[0].Rows[0]["VOUCHER_NO"].ToString();
            string vchno = txtVCHNO.Text;
            ViewState["vchno"] = vchno;
            if (ds.Tables[0].Rows[0]["ACC_PARTY_NAME"].ToString() == "" || ds.Tables[0].Rows[0]["ACC_PARTY_NAME"].ToString() == "-" || ds.Tables[0].Rows[0]["ACC_PARTY_NAME"].ToString() == string.Empty)
            {
                txtLedgerName.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString();
            }
            else
            {
                txtLedgerName.Text = ds.Tables[0].Rows[0]["ACC_PARTY_NAME"].ToString();
            }
            txtDate.Text = ds.Tables[0].Rows[0]["CHQ_DATE"].ToString();
            txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
            txtCHQNo.Text = ds.Tables[0].Rows[0]["CHQ_NO"].ToString();
            ViewState["Party_no"] = ds.Tables[0].Rows[0]["PARTY_NO"].ToString();
            ViewState["Amount"] = ds.Tables[0].Rows[0]["Amount"].ToString();
            ViewState["Party_Name"] = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString(); ;
            ViewState["BankNo"] = ds.Tables[0].Rows[0]["OPARTY"].ToString();
            ViewState["VchDate"] = ds.Tables[0].Rows[0]["TRANSACTION_DATE"].ToString();
            ViewState["Amount1"] = ds.Tables[0].Rows[0]["Amount"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_selectCompany.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void Clear()
    {
        txtLedgerName.Text = "";
        txtDate.Text = "";
        //txtAmount.Text = "";
        txtCHQNo.Text = "";
        //chkStamp.Checked = false;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    #endregion



    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect(back + ".aspx?obj=config");

    }

    /// <summary>
    /// code to check account number is exist. if yes then do not allow the same account code
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    //NEWLY ADDED FOR AUTOGENERATION OF LEDGER ACCOUNT CODE
    private void SetParameters()
    {
        //objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
        objCommon = new Common();
        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_REF_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
        int i = 0;
        if (ds != null)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {

                if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW AUTOGENERATED LEDGER HEAD ACCOUNT CODE")
                {
                    IsAutogenratedLedgerAccountCode = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();

                }

            }
        }
    }
    private void ChangeThePageLayout()
    {
        if (IsAutogenratedLedgerAccountCode == "Y" && ViewState["action"] == null)
        {
        }

    }




    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {

                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/ledgerhead.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
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


    protected void gvChqDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lblAmount = gvChqDetails.Rows[index].FindControl("lblAmt") as Label;
        Label lblDate = gvChqDetails.Rows[index].FindControl("lblDate") as Label;
        Label lblChqNo = gvChqDetails.Rows[index].FindControl("lblChqNo") as Label;
        Label lblParty = gvChqDetails.Rows[index].FindControl("lblParty") as Label;
        Label lblVchNo = gvChqDetails.Rows[index].FindControl("lblVchNo") as Label;
        txtAmount.Text = lblAmount.Text;
        txtCHQNo.Text = lblChqNo.Text;
        txtDate.Text = lblDate.Text;
        txtLedgerName.Text = lblParty.Text;
        ViewState["sumamount"] = Convert.ToDecimal(ViewState["sumamount"].ToString()) - Convert.ToDecimal(lblAmount.Text);
        ViewState["ChkEdit"] = "Y";
        ViewState["index"] = index.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ChequePrintMaster cpm = new ChequePrintMaster();
        AccountTransactionController oatc = new AccountTransactionController();
        if (gvChqDetails.Rows.Count > 0)
        {
            if (Convert.ToDecimal(ViewState["sumamount"].ToString()) == Convert.ToDecimal(ViewState["Amount1"].ToString()))
            {
                // cpm.VNO = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Request.QueryString["obj"].Split(',')[2]);ViewState["TRANSACTION_NO"]
                cpm.VNO = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Convert.ToInt32(ViewState["TRANSACTION_NO"]));
                String a = (ViewState["vchno"]).ToString();
                cpm.VNO = a;
                int res = oatc.DeleteChequeEntryDetails_New(cpm.VNO, Session["comp_code"].ToString().Trim());
                for (int i = 0; i < gvChqDetails.Rows.Count; i++)
                {
                    Label lblAmount = gvChqDetails.Rows[i].FindControl("lblAmt") as Label;
                    Label lblDate = gvChqDetails.Rows[i].FindControl("lblDate") as Label;
                    Label lblChqNo = gvChqDetails.Rows[i].FindControl("lblChqNo") as Label;
                    Label lblParty = gvChqDetails.Rows[i].FindControl("lblParty") as Label;
                    Label lblVchNo = gvChqDetails.Rows[i].FindControl("lblVchNo") as Label;
                    cpm.PARTYNAME = lblParty.Text.ToString().Trim();
                    cpm.VDT = Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy");
                    cpm.AMOUNT = Convert.ToDouble(lblAmount.Text.ToString().Trim());
                    string bankno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY a inner join ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC b on (a.BANKACCOUNTNO=b.ACCNO)", "b.BNO", "a.PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                    if (bankno == "")
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please map bank name with ledger", this.Page);
                        return;
                    }
                    cpm.BANKNO = Convert.ToInt32(bankno);//Convert.ToUInt16(ViewState["BankNo"].ToString().Trim());
                    if (lblChqNo.Text != "")
                        cpm.CHECKNO = lblChqNo.Text.ToString().Trim();
                    else
                        cpm.CHECKNO = "0";
                    if (lblDate.Text != "")
                        cpm.CHECKDT = Convert.ToDateTime(lblDate.Text).ToString("dd-MMM-yyyy");
                    else
                        cpm.CHECKDT = "1";
                    cpm.USERNAME = Session["username"].ToString().Trim();
                    cpm.COMPANY_CODE = Session["comp_code"].ToString().Trim();

                    if (chkStamp.Checked == true)
                    {
                        cpm.STAMP = "AcPayee";
                    }
                    else
                    {
                        cpm.STAMP = "";
                    }



                    NumberWords nw = new NumberWords();
                    cpm.REASON1 = nw.changeToWords(lblAmount.Text.ToString(), true);
                    res = oatc.AddChequeEntryDetails_New(cpm, Session["comp_code"].ToString().Trim());
                    if (res == 2)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Cheque No. Is Invalid.", this);
                        return;

                    }
                    else
                    {

                        //objCommon.DisplayUserMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);
                        //objCommon.DisplayMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);


                    }
                    //btnGenerate.Visible = true;
                    //btnGenerate.Focus();
                    ViewState["VchNo"] = cpm.VNO;
                }

                DataSet ds = new DataSet();
                // ds = objCommon.FillDropDown("ACC_REF_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
                ds = objCommon.FillDropDown("ACC_" + Session["comp_code"] + "_CHECK_PRINT", "VNO,PARTYNAME,CHECKNO,CONVERT(nvarchar(20),CHECKDT,103)CHECKDT,STAMP,AMOUNT,CTRNO", "", "VNO='" + ViewState["VchNo"].ToString() + "' and VDT='" + Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy") + "'", "");

                if (ds != null)
                {
                    GvPrintDetails.DataSource = ds.Tables[0];
                    GvPrintDetails.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        upd_ModalPopupExtender1.Show();
                    }
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Sum of cheque amount should equal to voucher amount", this.Page);
            }
        }


    }
    protected void GvPrintDetails_RowCommand(object sender, EventArgs e)
    {

    }
    protected void btnGenrate_Click(object sender, EventArgs e)
    {
        //btnGenerate.Enabled = false;

        string PartyName = string.Empty;

        DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "PARTYNAME,AMOUNT,CHECKDT,CHECKNO,BANKNO,CTRNO,STAMP", "", "VNO='" + ViewState["VchNo"].ToString().Trim() + "' and VDT='" + Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy").Trim() + "'", "");
        if (ds11 != null)
        {
            if (ds11.Tables[0] != null)
            {
                for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                {
                    if (ds11.Tables[0].Rows[i][0].ToString() != "")
                    {
                        PartyName = "0" + "*" + ds11.Tables[0].Rows[i][0].ToString();
                        string Script = string.Empty;
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
                        string reportTitle = "ChequePrint";

                        string can = string.Empty;

                        can = "false";

                        string CheckOrientation = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_CONFIG", "PARAMETER", "CONFIGDESC='CHEQUE ORIENTATION(TRUE-HORIZONTAL,FALSE-VERTICAL)'");
                        if (CheckOrientation == "N")
                        {
                            url += "Reports/Cheque_Vertical.aspx?";
                        }
                        else
                        {
                            url += "Reports/Cheque.aspx?";
                        }
                        url += "obj=" + ViewState["BankNo"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim() + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + ",0" + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + ViewState["VchNo"].ToString().Trim() + "," + can + "," + "0";
                        Session["chqprint"] = ViewState["BankNo"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim() + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + ",0" + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + ViewState["VchNo"].ToString().Trim() + "," + can + "," + "0";
                        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, UpdatePanel1.GetType(), "Report", Script, true);
                    }
                }
            }

        }
    }
    protected void GvPrintDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GvPrintDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string PartyName = string.Empty;
        DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "PARTYNAME AS PARTYNAME,AMOUNT,CHECKDT,CHECKNO,BANKNO,CTRNO,STAMP", "", "VNO='" + ViewState["VchNo"].ToString().Trim() + "' and VDT='" + Convert.ToDateTime(ViewState["VchDate"].ToString()).ToString("dd-MMM-yyyy").Trim() + "' and CTRNO='" + e.CommandArgument.ToString() + "'", "");
        if (ds11 != null)
        {
            if (ds11.Tables[0] != null)
            {
                for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                {
                    if (ds11.Tables[0].Rows[i][0].ToString() != "")
                    {
                        PartyName = "0" + "*" + ds11.Tables[0].Rows[i][0].ToString();
                        //PartyName = "'" + "0" + "*" + ds11.Tables[0].Rows[i][0].ToString() + "'";
                        string Script = string.Empty;
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
                        string reportTitle = "ChequePrint";
                        string bankno = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY a inner join ACC_" + Session["comp_code"].ToString().Trim() + "_BANKAC b on (a.BANKACCOUNTNO=b.ACCNO)", "b.BNO", "a.PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                        string can = string.Empty;

                        //Added by Nokhlal Kumar for Signature
                        string signature1 = "Finance Officer";
                        string signature2 = string.Empty;
                        if (rdbRegistrar.Checked)
                        {
                            signature2 = "Registrar";
                        }
                        else if (rdbPrincipal.Checked)
                        {
                            signature2 = "Principal";
                        }
                        else if (rdbDean.Checked)
                        {
                            signature2 = "Dean";
                        }

                        can = "false";
                        string accno = objCommon.LookUp("acc_" + Session["comp_code"].ToString().Trim() + "_party", "BANKACCOUNTNO", "PARTY_NO=" + ViewState["BankNo"].ToString().Trim());
                        string CheckOrientation = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() + "_CONFIG", "PARAMETER", "CONFIGDESC='CHEQUE ORIENTATION(TRUE-HORIZONTAL,FALSE-VERTICAL)'");
                        if (CheckOrientation == "N")
                        {
                            url += "Reports/Cheque_Vertical.aspx?";
                        }
                        else
                        {
                            url += "Reports/Cheque.aspx?";
                        }
                        string vchSqn = objCommon.LookUp("ACC_" + Session["comp_code"] + "_TRANS", "VOUCHER_SQN", "TRANSACTION_NO=" + Convert.ToInt32(ViewState["TRANSACTION_NO"]));
                        url += "obj=" + bankno + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim().Replace(',','$') + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + "," + accno + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + vchSqn + "," + can + "," + "0" + "," + signature1 + "," + signature2;
                        //Session["chqprint"] = ViewState["BankNo"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKNO"].ToString().Trim() + "," + ds11.Tables[0].Rows[i]["CHECKDT"].ToString().Trim() + "," + PartyName.Trim() + "," + ds11.Tables[0].Rows[i]["AMOUNT"].ToString().Trim() + ",0" + "," + ds11.Tables[0].Rows[i]["CTRNO"].ToString().Trim() + "," + ViewState["VchNo"].ToString().Trim() + "," + can + "," + "0";
                        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, UpdatePanel1.GetType(), "Report", Script, true);
                    }
                }
            }
            upd_ModalPopupExtender1.Show();
        }
    }
}
