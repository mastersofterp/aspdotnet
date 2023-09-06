//=================================================================================
// PROJECT NAME  : CCMS                                                          
// MODULE NAME   : CHEQUE PRINTING PAGE                                                     
// CREATION DATE : 29-04-2010                                               
// CREATED BY    : Ashish Thakre                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using System.Transactions;
using System.IO.Ports;
//using System.Transactions;
using IITMS.NITPRM;

public partial class ChequePrinting : System.Web.UI.Page
{
   // UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon=new Common ()  ;
    public string back=string.Empty;
    string[] ReceiveParameters;
    string ChequeModify = string.Empty;
    int bankid = 0;
    DataSet dsBank = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

        if (Request.QueryString["obj"] != null)
        {
            ReceiveParameters = Request.QueryString["obj"].Split(',');

            if (ReceiveParameters[0].ToString().Trim() == "ChequePrinting")
            {
               ViewState["oparty"] = ReceiveParameters[5].ToString().Trim();
            }
            else
            {
                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                //if (Session["masterpage"] != null)
                //    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                //else
                //    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {
            if (Request.QueryString["objMod"] != null)
            {
                ReceiveParameters = Request.QueryString["objMod"].Split(',');
                if (ReceiveParameters[0].ToString().Trim() == "ChequePrinting")
                {
                    ViewState["oparty"] = ReceiveParameters[2].ToString().Trim();
                    ChequeModify = "Y";
                   // objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");
                }
                else
                {
                    //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                    //if (Session["masterpage"] != null)
                    //    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                    //else
                    //    objCommon.SetMasterPage(Page, "");
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

        btnModify.Visible = false;
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //objCommon = new Common();

        }
        else
        {
            Response.Redirect("~/Default.aspx");

        }

        txtAmount.Attributes.Add("onblur", "return SetAmountToWord();");
        //btnModify.Attributes.Add("onClick", "return OpenChequeModifyWindow();");
                
        if (!Page.IsPostBack)
        {
            btnGenerate.Enabled = false;
        
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
              //  Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null )
                {
                    Session["comp_set"] = "NotSelected";
                    
                    objCommon.DisplayMessage("Select company/cash book.", this);

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    Session["comp_set"] = "";
                    //Page Authorization
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                   // Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    //if (Request.QueryString["pageno"] != null)
                    //{
                    //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //}

                    //PopulateDropDown();
        

                    ViewState["action"] = "add";
                }
               // PopulateListBox();
            }
            if (ChequeModify != "Y")
            {
                SetInitialData();
            }
            else
            {
                SetChequeDataForModification();
            
            }
            if (dsBank != null)
            {
                if (dsBank.Tables[0].Rows.Count > 0)
                {
                    if (dsBank.Tables[0].Rows[0]["ccurno"].ToString() != "0" && dsBank.Tables[0].Rows[0]["cfrno"].ToString() != "0" && dsBank.Tables[0].Rows[0]["ctono"].ToString() != "0")
                    {
                        txtChqNo.Enabled = false;
                        txtChqDate.Enabled = false;
                        txtInWords.Enabled = false;
                        txtVrDate.Enabled = false;
                        txtVrNo.Enabled = false;
                        txtAccNo.Enabled = false;
                        txtAmount.Enabled = false;
                        txtBnkAccName.Enabled = false;
                        txtBank.Enabled = false;
                    }
                    else if ((dsBank.Tables[0].Rows[0]["ccurno"].ToString() != "0" && dsBank.Tables[0].Rows[0]["cfrno"].ToString() == "0" && dsBank.Tables[0].Rows[0]["ctono"].ToString() == "0") || (dsBank.Tables[0].Rows[0]["ccurno"].ToString() == "0" && dsBank.Tables[0].Rows[0]["cfrno"].ToString() == "0" && dsBank.Tables[0].Rows[0]["ctono"].ToString() == "0"))
                    {
                        txtChqNo.Enabled = true;
                        txtChqDate.Enabled = true;
                        txtInWords.Enabled = false;
                        txtVrDate.Enabled = false;
                        txtVrNo.Enabled = false;
                        txtAccNo.Enabled = false;
                        txtAmount.Enabled = false;
                        txtBnkAccName.Enabled = false;
                        txtBank.Enabled = false;
                    }
                    else
                    {
                        txtChqNo.Enabled = true;
                        txtChqDate.Enabled = true;
                        txtInWords.Enabled = false;
                        txtVrDate.Enabled = false;
                        txtVrNo.Enabled = false;
                        txtAccNo.Enabled = false;
                        txtAmount.Enabled = false;
                        txtBnkAccName.Enabled = false;
                        txtBank.Enabled = false;
                    }
                }
            }
        }
    }

    private void SetChequeDataForModification()
    {
        if (ReceiveParameters.Length > 0)
        {

         DataSet dsch = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_CHECK_PRINT", "*", "", "ctrno=" + ReceiveParameters[1].ToString().Trim(), "");
         if (dsch != null)
         {
             if (dsch.Tables[0].Rows.Count > 0)
             {
                 txtChqDate.Enabled = true;
                 txtChqNo.Enabled = true;

                 txtChqDate.Text = Convert.ToDateTime(dsch.Tables[0].Rows[0]["CHECKDT"]).ToString("dd/MM/yyyy");
                 //txtChqNo.Text = dsch.Tables[0].Rows[0]["CHECKNO"].ToString().Trim();
                 txtChqNo.Text = ReceiveParameters[6].ToString().Trim();
                 txtChqDate.Enabled = false;
                 txtChqNo.Enabled = false;

                 ViewState["bankid"] = dsch.Tables[0].Rows[0]["BANKNO"].ToString().Trim();

                 txtVrNo.Text = dsch.Tables[0].Rows[0]["VNO"].ToString().Trim();
                 txtdept.Text = dsch.Tables[0].Rows[0]["DEPT"].ToString().Trim();
                 if (dsch.Tables[0].Rows[0]["STAMP"].ToString().Trim() == "AcPayee")
                 {
                     chkStamp.Checked = true;
                 }
                 else
                 {
                     chkStamp.Checked = false;
                 }
                 
                 DataSet dsp = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_PAYEE", "*", "", "ACCNO='" + dsch.Tables[0].Rows[0]["PARTYACCOUNTNO"].ToString().Trim() +"'", "");
                 if (dsp != null)
                 {
                     if (dsp.Tables[0].Rows.Count > 0)
                     {
                         txtPartyName.Text = dsp.Tables[0].Rows[0]["IDNO"].ToString().Trim() + "*" + dsp.Tables[0].Rows[0]["PARTYNAME"].ToString().Trim();
                         txtAddress.Text = dsp.Tables[0].Rows[0]["ADDRESS"].ToString().Trim();
                        txtAccountCode.Text = dsch.Tables[0].Rows[0]["PARTYACCOUNTNO"].ToString().Trim();
                     }
                 }

                DataSet dsvr = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "*", "", "VOUCHER_NO=" + txtVrNo.Text.ToString().Trim(), "");
                 if (dsvr != null)
                 {
                     if (dsvr.Tables[0].Rows.Count > 0)
                     {
                         txtVrDate.Text = Convert.ToDateTime(dsvr.Tables[0].Rows[0]["TRANSACTION_DATE"]).ToString("dd/MM/yyyy");
                     }
                 }
                 txtAmount.Text = dsch.Tables[0].Rows[0]["AMOUNT"].ToString().Trim();
                 txtInWords.Text = dsch.Tables[0].Rows[0]["REASON1"].ToString().Trim();

                 AccountTransactionController oto = new AccountTransactionController();
                 int u = 0;
                 for (u = 0; u < dsvr.Tables[0].Rows.Count; u++)
                 {
                    DataSet dstemp= objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY", "*", "", "party_no=" + dsvr.Tables[0].Rows[u]["PARTY_NO"].ToString().Trim() +" and PAYMENT_TYPE_NO=2", "");
                    if (dstemp != null)
                    {
                        if (dstemp.Tables[0].Rows.Count > 0)
                        {

                            DataSet dsBank = oto.GetbankChequeDetails(Convert.ToInt16(dstemp.Tables[0].Rows[0]["PARTY_NO"].ToString().Trim()), Session["comp_code"].ToString().Trim());
                            if (dsBank != null)
                            {
                                if (dsBank.Tables[0].Rows.Count > 0)
                                {
                                    txtBnkAccName.Text = dsBank.Tables[0].Rows[0]["accname"].ToString().Trim();
                                    txtBank.Text = dsBank.Tables[0].Rows[0]["bankname"].ToString().Trim();
                                    txtAccNo.Text = dsBank.Tables[0].Rows[0]["accno"].ToString().Trim();
                                    //txtChqDate.Text = DateTime.Now.ToString("dd/MM/yyyy").ToString();// Convert.ToDateTime(dsBank.Tables[0].Rows[0]["cissuedt"].ToString().Trim()).ToString("dd/MM/yyyy").ToString();
                                    //txtChqNo.Text = dsBank.Tables[0].Rows[0]["ccurno"].ToString().Trim();
                                    if (dsBank.Tables[0].Rows[0]["bankno"].ToString().Trim() != "0")
                                    {
                                        ViewState["bankid"] = Convert.ToString(dsBank.Tables[0].Rows[0]["bankno"].ToString().Trim());
                                    }
                                }
                           }
                        }
                    }
                 }
              
            }

         }
               //Session["comp_code"].ToString().Trim()

       }
    }

    private void SetInitialData()
    {
        if (ReceiveParameters.Length > 0)
        {
            txtVrDate.Text = ReceiveParameters[1].ToString().Trim();
            txtVrNo.Text = ReceiveParameters[2].ToString().Trim();
            txtAmount.Text = ReceiveParameters[4].ToString().Trim();
            NumberWords nw = new NumberWords();
            txtInWords.Text= nw.changeToWords(txtAmount.Text.ToString(), true);

            //Added By Nitin For Cheque No of Voucher
            txtChqNo.Text = ReceiveParameters[6].ToString().Trim();

            string PartyNo = ReceiveParameters[5].ToString().Trim();
            txtPartyName.Text = objCommon.LookUp("acc_" + Session["comp_code"].ToString().Trim() + "_party", "PARTY_NAME", "party_no=" + PartyNo);
            AccountTransactionController oto = new AccountTransactionController();
           dsBank=  oto.GetbankChequeDetails(Convert.ToInt16(ReceiveParameters[5].ToString().Trim()), Session["comp_code"].ToString().Trim());
          if (dsBank != null)
          {
              if (dsBank.Tables[0].Rows.Count > 0)
              {
                  txtBnkAccName.Text = dsBank.Tables[0].Rows[0]["accname"].ToString().Trim();
                  txtBank.Text = dsBank.Tables[0].Rows[0]["bankname"].ToString().Trim();
                  txtAccNo.Text = dsBank.Tables[0].Rows[0]["accno"].ToString().Trim();
                  txtChqDate.Text = DateTime.Now.ToString("dd/MM/yyyy").ToString();// Convert.ToDateTime(dsBank.Tables[0].Rows[0]["cissuedt"].ToString().Trim()).ToString("dd/MM/yyyy").ToString();

                  //txtChqNo.Text = dsBank.Tables[0].Rows[0]["ccurno"].ToString().Trim();
                  //if (dsBank.Tables[0].Rows[0]["ccurno"].ToString() == "0")
                  //{
                  //    txtChqNo.Text = string.Empty;
                  //}
                  //else if (dsBank.Tables[0].Rows[0]["ccurno"].ToString() != "0")
                  //{
                  //    txtChqNo.Text = dsBank.Tables[0].Rows[0]["ccurno"].ToString().Trim();
                  //}
                  if (dsBank.Tables[0].Rows[0]["bankno"].ToString().Trim() != "0")
                  {
                      ViewState["bankid"] = Convert.ToString(dsBank.Tables[0].Rows[0]["bankno"].ToString().Trim()); 
                      
                  
                  }
                  
              }
          
          
          }
            
        }
    }
    //private void PopulateListBox()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "UPPER(ACCNAME) AS ACCOUNTNAME", "TRNO > 0", "TRNO");// "PARTY_NAME");
    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lstBankName.Items.Clear();
    //                lstBankName.DataTextField = "ACCOUNTNAME";
    //                lstBankName.DataValueField = "TRNO";
    //                lstBankName.DataSource = ds.Tables[0];
    //                lstBankName.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "AccountMaster.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    
    //}

    //private void CheckPageAuthorization()
    //{
    //    //if (Request.QueryString["pageno"] != null)
    //    //{
    //    //    //Check for Authorization of Page
    //    //    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //    //    {
    //    //        Response.Redirect("~/notauthorized.aspx?page=AccountMaster.aspx");
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    //Even if PageNo is Null then, don't show the page
    //    //    Response.Redirect("~/notauthorized.aspx?page=AccountMaster.aspx");
    //    //}
    //}
  
    //protected void lstBankName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //very important 
    //        string id = Request.Form[lstBankName.UniqueID].ToString();
    //        if (id != "" | id != string.Empty)
    //        {
    //            ClearAll();
    //            ViewState["action"] = "edit";
    //            ViewState["id"] = id.ToString();
    //            //Show Details 
    //            PartyController objPC = new PartyController();
    //            string code_year = Session["comp_code"].ToString().Trim();// +"_" + Session["fin_yr"].ToString();
    //            DataSet dscnt = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "*", "", "TRNO = '" + Convert.ToString(id.ToString()).Trim().ToUpper() + "' ", string.Empty);
    //            if (dscnt != null)
    //            {
    //                if (dscnt.Tables[0].Rows.Count > 0)
    //                {
    //                    int o = 0;
    //                    for (o = 0; o < dscnt.Tables[0].Rows.Count; o++)
    //                    {
    //                        txtAccountCode.Text = dscnt.Tables[0].Rows[o]["ACCNO"].ToString().Trim();
    //                        txtAccountName.Text = dscnt.Tables[0].Rows[o]["ACCNAME"].ToString().Trim();
    //                        txtChqCurNo.Text = dscnt.Tables[0].Rows[o]["CCURNO"].ToString().Trim();
    //                        txtChqFrmNo.Text = dscnt.Tables[0].Rows[o]["CFRNO"].ToString().Trim();
    //                        txtChqToNo.Text = dscnt.Tables[0].Rows[o]["CTONO"].ToString().Trim();
    //                        txtChqDate.Text = Convert.ToDateTime(dscnt.Tables[0].Rows[o]["CISSUEDT"].ToString().Trim()).ToString("dd/MM/yyyy").ToString();
    //                        if (dscnt.Tables[0].Rows[o]["STATUS"].ToString().Trim() == "0")
    //                        {
    //                            chkStamp.Checked = false;
    //                        }
    //                        else
    //                        {
    //                            chkStamp.Checked = true;
    //                        }
    //                    }
    //                    DataSet dsbn = objCommon.FillDropDown("ACC_BANK_DETAIL", "BANKNO", "BANKNAME", "BANKNO = '" + Convert.ToString(dscnt.Tables[0].Rows[0]["BNO"].ToString().Trim()) + "' ", string.Empty);
    //                    if (dsbn != null)
    //                    {
    //                        if (dsbn.Tables[0].Rows.Count > 0)
    //                        {
    //                            txtBank.Text = dsbn.Tables[0].Rows[0]["BANKNO"].ToString().Trim() + "*" + dsbn.Tables[0].Rows[0]["BANKNAME"].ToString().Trim();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ViewState["action"] = "add";
    //            ViewState["id"] = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "AccountMaster.lstBankName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    
    //}
    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BANKAC", "TRNO", "ACCNAME", "ACCNAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "ACCNAME");
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lstBankName.DataTextField = "ACCNAME";
    //            lstBankName.DataValueField = "TRNO";
    //            lstBankName.DataSource = ds.Tables[0];
    //            lstBankName.DataBind();
    //        }
    //    }
    //    txtSearch.Focus();
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //TransactionOptions to = new TransactionOptions();
        //to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        //to.Timeout = TimeSpan.FromDays(1);
        //TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,to,EnterpriseServicesInteropOption.Full)
        //using(ts) 
        //{
        // ts.Complete();
        //}


        //Comment on 22 April 2014

            ////DataSet dsz = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "", "CHQ_NO=" + txtChqNo.Text.ToString().Trim() + " and CHQ_DATE='" + Convert.ToDateTime(txtChqDate.Text).ToString("dd-MMM-yyyy") + "' and voucher_no=" + txtVrNo.Text.ToString().Trim() + " and can='false' and party_no=" + ViewState["oparty"].ToString().Trim(), "");
            ////DataSet dsz = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_check_print", "*", "", "CHECKNO=" + txtChqNo.Text.ToString().Trim(),"");
            //if (dsz != null)
            //{
            //    if (dsz.Tables[0].Rows.Count > 0)
            //    {
            //        objCommon.DisplayUserMessage(UPDLedger, "Cheque Is Allready Printed.", this);
            //        btnSubmit.Focus();
            //        return;
            //    }
            //}

        if (ChequeModify != "Y")
        {

            int id = 0;
            string countPayee=objCommon.LookUp("acc_" + Session["comp_code"].ToString().Trim() + "_PAYEE", "count(*)", "PARTYNAME='"+txtPartyName.Text.Trim()+"'");
            //if (txtPartyName.Text.ToString().IndexOf('*') == -1)
            if (countPayee=="0")
            {
                PayeeMasterClass objpayee = new PayeeMasterClass();
                objpayee.IDNO = 0;
                objpayee.PARTYNAME = txtPartyName.Text.ToString().Trim();
                objpayee.ADDRESS = txtAddress.Text.ToString().Trim();
                if (txtAccountCode.Text == string.Empty)
                {
                    objpayee.ACCNO = "0";
                }
                else
                {
                    objpayee.ACCNO = txtAccountCode.Text.ToString().Trim();
                }
                
                objpayee.CAN = 0;

                DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYEE", "*", "", "accno='" + txtAccountCode.Text.ToString().Trim() + "' ", "");
                if (ds11 != null)
                {
                    if (ds11.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Account No. Allready Exists, Try Another Account No.", this);
                        txtAccountCode.Focus();
                        return;
                    }

                }

                AccountTransactionController oatc = new AccountTransactionController();
                id = oatc.AddPayeeDetails(objpayee, Session["comp_code"].ToString().Trim());
            }
            else
            {
                if (txtAccountCode.Text.ToString().Trim() != "")
                {
                    DataSet ds10 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYEE", "*", "", "accno='" + txtAccountCode.Text.ToString().Trim() + "' and idno <> " + txtPartyName.Text.ToString().Trim().Split('*')[0].ToString().Trim(), "");
                    if (ds10 != null)
                    {
                        if (ds10.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayUserMessage(UPDLedger, "Account No. Allready Exists, Try Another Account No.", this);
                            txtAccountCode.Focus();
                            return;
                        }

                    }

                }
            
            }
            ChequePrintMaster cpm = new ChequePrintMaster();

            if (txtPartyName.Text.ToString().IndexOf('*') == -1)
            {
                cpm.PARTYNAME = txtPartyName.Text.ToString().Trim();//.Split('*')[1].ToString().Trim();
            }
            else
            {
                cpm.PARTYNAME = txtPartyName.Text.ToString().Trim().Split('*')[1].ToString().Trim();
            }

            //cpm.PARTYNAME = txtPartyName.Text.ToString().Trim().Split('*')[1].ToString().Trim();
            if (txtAccountCode.Text == string.Empty)
            {
                cpm.PARTYACCOUNTNO = "0";
            }
            else
            {
                cpm.PARTYACCOUNTNO = txtAccountCode.Text.ToString().Trim();
            }
            
            cpm.ADDRESS = txtAddress.Text.ToString().Trim();
            if (txtbilldate.Text.ToString().Trim() == "")
            {
                cpm.BILLDT = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                cpm.BILLDT = Convert.ToDateTime(txtbilldate.Text).ToString("dd-MMM-yyyy").Trim();
            }
            cpm.PARTYNO = Convert.ToInt32(ViewState["oparty"].ToString());
            cpm.BILLNO = txtbillno.Text.ToString().Trim();
            cpm.VNO = txtVrNo.Text.ToString().Trim();
            cpm.REASON1 = txtInWords.Text.ToString().Trim();
            if (txtVrDate.Text.ToString().Trim() == "")
            {
                cpm.VDT = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                cpm.VDT = Convert.ToDateTime(txtVrDate.Text).ToString("dd-MMM-yyyy").Trim();
            }

            cpm.AMOUNT = Convert.ToDouble(txtAmount.Text.ToString().Trim());
            cpm.DEPT = txtdept.Text.ToString().Trim();
            if (ViewState["bankid"] != null)
            {
                if (ViewState["bankid"].ToString().Trim() != "0")
                {
                    cpm.BANKNO = Convert.ToUInt16(ViewState["bankid"].ToString().Trim());// Convert.ToInt16(txtBank.Text.ToString().Trim().Split('*')[0].ToString());    
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Cheque Print Problem Occured Due To Bank No.", this);
                    return;
                }
            }

            cpm.CHECKNO = txtChqNo.Text.ToString().Trim();

            if (cpm.CHECKNO == "0")
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Enter Valid Cheque No.", this.Page);
                return;
            }

            if (txtChqDate.Text.ToString().Trim() == "")
            {
                cpm.CHECKDT = DateTime.Now.ToString("dd-MMM-yyyy");

            }
            else
            {
                cpm.CHECKDT = Convert.ToDateTime(txtChqDate.Text).ToString("dd-MMM-yyyy").Trim();

            }
            cpm.USERNAME = Session["username"].ToString().Trim();
            cpm.COPY1 = "";
            cpm.COPY2 = "";
            cpm.COPY3 = "";
            // cpm.REASON1 = "";
            cpm.REASON2 = "";
            cpm.REASON3 = "";
            cpm.COMPANY_CODE = Session["comp_code"].ToString().Trim();

            cpm.PRINTSTATUS = 0;

            if (chkStamp.Checked == false && chkstatus.Checked == false)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Check Stamp For Cheque Type.", this);
                chkStamp.Focus();
                return;

            }


            if (chkStamp.Checked == true)
            {
                cpm.STAMP = "AcPayee";

            }
            else
            {
                cpm.STAMP = "";
            }

            cpm.DEDSTATUS = 0;
            if (chkstatus.Checked == true)
            {
                cpm.CANCEL = 1;

            }
            else
            {

                cpm.CANCEL = 0;
            }

            cpm.ACCNAME = txtBnkAccName.Text.ToString().Trim();
            cpm.ACCNO = txtAccNo.Text.ToString().Trim();
            //if (CheckDuplicateCheck() == true)
            //{
            //    objCommon.DisplayUserMessage(UPDLedger, "This check number is already exist.Please enter another check number", this);
            //    txtChqNo.Focus();
            //    return;
            //}
            AccountTransactionController atc = new AccountTransactionController();
            int res = atc.AddChequeEntryDetails(cpm, Session["comp_code"].ToString().Trim());
            if (res == 2)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Cheque No. Is Invalid.", this);
                return;

            }
            if (res > 0)
            {
                ViewState["ctrno"] = res.ToString();
                int kes = UpdateChequeNo(Convert.ToUInt16(ViewState["bankid"].ToString().Trim()), txtAccNo.Text.ToString().Trim());
                if (kes == 2)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Cheque No. Is Invalid.", this);
                    return;

                }
                btnGenerate.Enabled = true;
                btnGenerate.Focus();
                //objCommon.DisplayUserMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);
                objCommon.DisplayMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);
                return;

            }
        }

        else
        {

            int id = 0;
            if (txtPartyName.Text.ToString().IndexOf('*') == -1)
            {
                PayeeMasterClass objpayee = new PayeeMasterClass();
                objpayee.IDNO = 0;
                objpayee.PARTYNAME = txtPartyName.Text.ToString().Trim();
                objpayee.ADDRESS = txtAddress.Text.ToString().Trim();
                objpayee.ACCNO = txtAccountCode.Text.ToString().Trim();
                objpayee.CAN = 0;
                DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYEE", "*", "", "accno='" + txtAccountCode.Text.ToString().Trim() + "' ", "");
                if (ds11 != null)
                {
                    if (ds11.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Account No. Allready Exists, Try Another Account No.", this);
                        txtAccountCode.Focus();
                        return;
                    }

                }

                AccountTransactionController oatc = new AccountTransactionController();
                oatc.AddPayeeDetails(objpayee, Session["comp_code"].ToString().Trim());
            }
            else
            {
                if (txtAccountCode.Text.ToString().Trim() != "")
                {
                    DataSet ds10 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYEE", "*", "", "accno='" + txtAccountCode.Text.ToString().Trim() + "' and idno <> " + txtPartyName.Text.ToString().Trim().Split('*')[0].ToString().Trim(), "");
                    if (ds10 != null)
                    {
                        if (ds10.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayUserMessage(UPDLedger, "Account No. Allready Exists, Try Another Account No.", this);
                            txtAccountCode.Focus();
                            return;
                        }
                    }
                }
            }

            ChequePrintMaster cpm = new ChequePrintMaster();
            if (txtPartyName.Text.ToString().IndexOf('*') == -1)
            {
                cpm.PARTYNAME = txtPartyName.Text.ToString().Trim();//.Split('*')[1].ToString().Trim();
            }
            else
            {
                cpm.PARTYNAME = txtPartyName.Text.ToString().Trim().Split('*')[1].ToString().Trim();
            }
             
            cpm.PARTYACCOUNTNO = txtAccountCode.Text.ToString().Trim();
            cpm.ADDRESS = txtAddress.Text.ToString().Trim();
            if (txtbilldate.Text.ToString().Trim() == "")
            {
                cpm.BILLDT = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                cpm.BILLDT = Convert.ToDateTime(txtbilldate.Text).ToString("dd-MMM-yyyy").Trim();
            }
            cpm.BILLNO = txtbillno.Text.ToString().Trim();
            cpm.VNO = txtVrNo.Text.ToString().Trim();
            cpm.REASON1 = txtInWords.Text.ToString().Trim();
            if (txtVrDate.Text.ToString().Trim() == "")
            {
                cpm.VDT = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                cpm.VDT = Convert.ToDateTime(txtVrDate.Text).ToString("dd-MMM-yyyy").Trim();
            }

            cpm.AMOUNT = Convert.ToDouble(txtAmount.Text.ToString().Trim());
            cpm.CTRNO = Convert.ToInt16(ReceiveParameters[1]);

            cpm.DEPT = txtdept.Text.ToString().Trim();
            if (ViewState["bankid"] != null)
            {
                if (ViewState["bankid"].ToString().Trim() != "0")
                {
                    cpm.BANKNO = Convert.ToUInt16(ViewState["bankid"].ToString().Trim());// Convert.ToInt16(txtBank.Text.ToString().Trim().Split('*')[0].ToString());    
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Cheque Print Problem Occured Due To Bank No.", this);
                    return;
                }
            }

            cpm.CHECKNO = txtChqNo.Text.ToString().Trim();
            if (txtChqDate.Text.ToString().Trim() == "")
            {
                cpm.CHECKDT = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                cpm.CHECKDT = Convert.ToDateTime(txtChqDate.Text).ToString("dd-MMM-yyyy").Trim();

            }
            cpm.USERNAME = Session["username"].ToString().Trim();
            cpm.COPY1 = "";
            cpm.COPY2 = "";
            cpm.COPY3 = "";
            // cpm.REASON1 = "";
            cpm.REASON2 = "";
            cpm.REASON3 = "";
            cpm.COMPANY_CODE = Session["comp_code"].ToString().Trim();
            cpm.PARTYNO = Convert.ToInt32(ViewState["oparty"].ToString());
            cpm.PRINTSTATUS = 0;

            if (chkStamp.Checked == false && chkstatus.Checked == false)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Please Check Stamp For Cheque Type.", this);
                chkStamp.Focus();
                return;

            }
            
            if (chkStamp.Checked == true)
            {
                cpm.STAMP = "AcPayee";

            }
            else
            {
                cpm.STAMP = "";
            }

            cpm.DEDSTATUS = 0;
            if (chkstatus.Checked == true)
            {
                cpm.CANCEL = 1;

            }
            else
            {
                cpm.CANCEL = 0;
            }

            cpm.ACCNAME = txtBnkAccName.Text.ToString().Trim();
            cpm.ACCNO = txtAccNo.Text.ToString().Trim();

            AccountTransactionController atc = new AccountTransactionController();
            int res = atc.UpdateChequeEntryDetails(cpm, Session["comp_code"].ToString().Trim());
            
            if (res > 0)
            {
                ViewState["ctrno"] = res.ToString();
                btnGenerate.Enabled = true;
                btnGenerate.Focus();

                objCommon.DisplayUserMessage(UPDLedger, "Cheque Has Been Configured Successfully.", this);
                return;
            }
        }

    }




    //private void PopulateListBox()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_CHECK_PRINT", "*", "", "TRNO=0", "PARTYNAME");// "PARTY_NAME");
    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lstBankName.Items.Clear();
    //                lstBankName.DataTextField = "PARTYACCOUNTNO" + "PARTYNAME";
    //                lstBankName.DataValueField = "CTRNO";
    //                lstBankName.DataSource = ds.Tables[0];
    //                lstBankName.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "AccountMaster.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}



    private int UpdateChequeNo(int bankno,string bankAccnountNo)
    {

        AccountTransactionController atc = new AccountTransactionController();
       return atc.IncrementChequeNo(bankno, bankAccnountNo, Session["comp_code"].ToString().Trim());
    
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        btnGenerate.Enabled = false;

        string PartyName = string.Empty;
        if (txtPartyName.Text.ToString().Trim() == "")
        {
            objCommon.DisplayUserMessage(UPDLedger, "Payee Name Should Not Be Blank.", this);
            txtPartyName.Focus();
            return;
        
        }
        //if (txtAddress.Text.ToString().Trim() == "")
        //{
        //    objCommon.DisplayUserMessage(UPDLedger, "Payee Address Should Not Be Blank.", this);
        //    txtAddress.Focus();
        //    return;

        //}
        if (txtPartyName.Text.ToString().Trim().Contains("*"))
        {
            PartyName = txtPartyName.Text.ToString().Trim();
        }
        else
        {
            DataSet ds11 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_PAYEE", "IDNO", "", "PARTYNAME='" + txtPartyName.Text.ToString().Trim() + "' ", "");
            if (ds11 != null)
            {
                if (ds11.Tables[0] != null)
                {
                    if (ds11.Tables[0].Rows.Count > 0)
                    {
                        if (ds11.Tables[0].Rows[0][0].ToString() != "")
                        {
                            PartyName = ds11.Tables[0].Rows[0][0].ToString().Trim() + "*" + txtPartyName.Text.Trim();
                        }
                    }
                }

            }
        }

        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
        string reportTitle = "ChequePrint";

        string can = string.Empty;
        if (chkstatus.Checked == true)
        {
            can = "true";
        }
        else
        {
            can = "false";
        }

        string CheckOrientation = objCommon.LookUp("ACC_" + Session["comp_code"].ToString().Trim() +"_CONFIG", "PARAMETER", "CONFIGDESC='CHEQUE ORIENTATION(TRUE-HORIZONTAL,FALSE-VERTICAL)'");
        if (CheckOrientation == "N")
        {
            url += "Reports/Cheque_Vertical.aspx?";
        }
        else
        {
            url += "Reports/Cheque.aspx?";
        }
        url += "obj=" + ViewState["bankid"].ToString().Trim() + "," + txtChqNo.Text.ToString().Trim() + "," + txtChqDate.Text.ToString().Trim() + "," + PartyName.Trim() + "," + txtAmount.Text.ToString().Trim() + "," + txtAccNo.Text.ToString().Trim() + "," + ViewState["ctrno"].ToString() + "," + txtVrNo.Text.ToString().Trim() + "," + can + "," + ViewState["oparty"].ToString();
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

        //Script = "window.open('http://localhost:1710/PresentationLayer/Reports/Cheque.aspx','ChequePrint','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        
        //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(),"Report", Script, true);


    }
    protected void hdnPartyNo_ValueChanged(object sender, EventArgs e)
    {

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

        Response.Redirect("ChequeEntryModifications.aspx?obj=ChequePrinting");

    }
    protected void txtChqNo_TextChanged(object sender, EventArgs e)
    {
        if (CheckDuplicateCheck() == true)
        {
            objCommon.DisplayUserMessage(UPDLedger, "This check number is already exist.Please enter another check number", this);
            txtChqNo.Focus();
            return;
        }
     }

    private bool CheckDuplicateCheck()
    {
        DataSet ds = null;
        DataSet ds1 = null;
        objCommon = new Common();
        //string lookupCheckNo = string.Empty;
        try
        {
            AccountTransactionController oto = new AccountTransactionController();
            ds1 = oto.GetbankChequeDetails(Convert.ToInt16(ReceiveParameters[3].ToString().Trim()), Session["comp_code"].ToString().Trim());
            ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "" + "_check_print", "*", "", "checkno='" + Convert.ToInt32(txtChqNo.Text) + "' and bankno='" + Convert.ToInt32(ds1.Tables[0].Rows[0]["bankno"].ToString()) + "'", "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    return true;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["obj"] != null)
    //    {
    //        if (Request.QueryString["obj"].ToString().Trim() != "config")
    //        {
    //            if (Request.QueryString["pageno"] != null)
    //            {
    //                //Check for Authorization of Page
    //                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //                {
    //                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
    //                }
    //            }
    //            else
    //            {
    //                //Even if PageNo is Null then, don't show the page
    //                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
    //            }

    //        }

    //    }
    //    else
    //    {
    //        if (Request.QueryString["pageno"] != null)
    //        {
    //            //Check for Authorization of Page
    //            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //            {
    //                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
    //            }
    //        }
    //        else
    //        {
    //            //Even if PageNo is Null then, don't show the page
    //            Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
    //        }
    //    }
    //}

    private void Clear()
    {
        txtPartyName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtbillno.Text = string.Empty;
        txtbilldate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtdept.Text = string.Empty;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         