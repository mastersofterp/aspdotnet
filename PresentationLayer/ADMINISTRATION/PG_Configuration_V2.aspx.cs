//======================================================================================
// PROJECT NAME    : RFC COMMON
// MODULE NAME     : ACADEMIC
// PAGE NAME       : PG Configuration V2
// CREATION DATE   : 28-AUG-2023
// CREATED BY      : GOPAL MANDAOGADE
// MODIFIED BY     : 
// MODIFIED DATE   : 
// MODIFIED DESC   : 
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_PG_Configuration_V2_aspx : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objconfig = new ConfigController();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               
            }
            ViewState["action"] = "add";
            BindListView();


            // Tap2 
            ShowData();
            objCommon.FillDropDownList(ddlpayment, "ACD_PAYMENT_GATEWAY", "PAYID", "PAY_GATEWAY_NAME", "PAYID > 0", "PAYID DESC");
            objCommon.FillDropDownList(ddlActivitynameV2, "ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME", "ACTIVITYNO > 0", "ACTIVITYNO");
             //objCommon.FillDropDownList(ddlSemesterV2, "ACD_SEMESTER D LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEGREENO=CDB.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "DEGREENO");
            
            //Tab3
            BindCollege();
            objCommon.FillDropDownList(ddlReciepttypeV2, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_CODE", "RCPTTYPENO > 0", "RCPTTYPENO ASC");
            objCommon.FillDropDownList(ddlpaymentV2, "ACD_PAYMENT_GATEWAY", "PAYID", "PAY_GATEWAY_NAME", "PAYID > 0", "PAYID DESC");
            objCommon.FillListBox(ddlDegreeV2, "ACD_DEGREE D LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEGREENO=CDB.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "DEGREENO"); // + "AND CDB.COLLEGE_ID IN (" + Session["college_nos"] + ")"
            objCommon.FillListBox(ddlBranchV2, "ACD_BRANCH B LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON B.BRANCHNO=CDB.BRANCHNO", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "BRANCHNO");
            objCommon.FillListBox(ddlSemesterV2, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO> 0 ", "SEMESTERNO");
            ShowDataMappingV2();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PG_Configuration_V2.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PG_Configuration_V2.aspx");
        }
    }

  
    #region Tab1 method events
    private void BindListView()
    {
        try
        {
            DataSet ds = objconfig.GetPaymentGateway_masterV2();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPayMaster.DataSource = ds;
                lvPayMaster.DataBind();
            }
            else
            {
                lvPayMaster.DataSource = null;
                lvPayMaster.DataBind();
                //objCommon.DisplayMessage(Page, "Activity is not found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PG_Configuration_V2.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSaveV2_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPaymentGName.Text))
            {

                int status = 0;

                if (hfdActive.Value == "true")
                {
                    status = 1;
                }
                else
                {
                    status = 0;
                }
                if (ViewState["ACTION"] != null && ViewState["ACTION"].Equals("edit"))
                {

                    int payid = Convert.ToInt32(ViewState["PAYID"]);
                    CustomStatus cs = (CustomStatus)objconfig.UpdatePayment_configV2(payid, txtPaymentGName.Text, status);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", this.Page);
                        btnSaveV2.Text = "Submit";
                        BindListView();
                        Clear();
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(Page, "Record Already Exist", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(Page, "Transaction Failed..!", this.Page);
                    }
                }
                else
                {
                    int payid = 0;
                    CustomStatus cs = (CustomStatus)objconfig.Addpayment_configV2(txtPaymentGName.Text, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(Page, "Record Added Successfully!", this.Page);
                        Clear();
                        BindListView();
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(Page, "Record Already Exist", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(Page, "Transaction Failed..!", this.Page);
                    }

                }

            }
            else
            {
                objCommon.DisplayMessage(Page, "Please Enter Payment Gateway Name!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PG_Configuration_V2.btnSaveV2_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelV2_Click(object sender, EventArgs e)
    {
        Clear();
        //lvPayMaster.DataSource = null;
        //lvPayMaster.DataBind();
    }

    private void Clear()
    {
        txtPaymentGName.Text = string.Empty;
        ddlDegreeV2.SelectedIndex = 0;
        btnSaveV2.Text = "Submit";
        ViewState["ACTION"] = null;
    }

    protected void btnEditV2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSaveV2.Text = "Update";
            ImageButton btnEditRecord = sender as ImageButton;
            int Payid = int.Parse(btnEditRecord.CommandArgument);
            DataSet ds = objconfig.GetEditPaymentGateway_masterV2(Payid);   // changes name
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtPaymentGName.Text = dr["PAY_GATEWAY_NAME"] == null ? string.Empty : dr["PAY_GATEWAY_NAME"].ToString();
                ViewState["ACTION"] = "edit";
                ViewState["PAYID"] = dr["PAYID"].ToString();
               
                //foreach (ListViewDataItem lv in lvPayMaster.Items)
                //{
                //if (dr["ACTIVESTATUS"].ToString() == "Active")
                //{
                //    chkstatus.Checked = true;
                //    // lblstatus.ForeColor = System.Drawing.Color.Green;
                //}
                //else
                //{
                //    chkstatus.Checked = false;
                //    //lblstatus.ForeColor = System.Drawing.Color.Red;
                //}

                //Added By Rishabh on Dated 01/11/2021
                if (dr["ACTIVESTATUS"].ToString() == "Active")
                {
                    hfdActive.Value = "true";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatusActive(true);", true);
                }
                else
                {
                    hfdActive.Value = "false";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatusActive(false);", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration_V2.btnEditV2_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Tab2 method events
    protected void btnSubmitV2_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlpayment.SelectedValue != "0" && ddlpayment.SelectedValue != "")
            {
                if (ddlinstance.SelectedValue != "0" && ddlinstance.SelectedValue != "")
                {
                    if (!string.IsNullOrEmpty(txtmerchantid.Text))
                    {
                        if (!string.IsNullOrEmpty(txtaccesscode.Text))
                        {
                            if (!string.IsNullOrEmpty(txtchecksumkey.Text))
                            {
                                if (!string.IsNullOrEmpty(txtrequesturl.Text))
                                {
                                    if (!string.IsNullOrEmpty(txtresponseurl.Text))
                                    {
                                        if (!string.IsNullOrEmpty(txtPgPageUrl.Text))
                                        {

                                            #region logic
                                            int collegeid = 0;
                                            int degreeno = 0;
                                            int organizationid = Convert.ToInt32(Session["OrgId"]);
                                            int Active_status = 0;
                                            if (hfdActive1.Value == "true")
                                            {
                                                Active_status = 1;
                                            }
                                            else
                                            {
                                                Active_status = 0;
                                            }

                                            
                                           
                                            if (ViewState["ACTION2"] != null && ViewState["ACTION2"].Equals("Edit"))
                                            {
                                                int congno = Convert.ToInt32(ViewState["Configid"]);
                                                CustomStatus cs = (CustomStatus)objconfig.UpdateadmFeeDeductionV2(congno, Convert.ToInt32(ddlpayment.SelectedValue), txtmerchantid.Text,
                                                                                                                txtaccesscode.Text, txtchecksumkey.Text, txtrequesturl.Text, txtresponseurl.Text,
                                                                                                                Convert.ToInt32(ddlinstance.SelectedValue), Active_status,
                                                                                                                organizationid, Convert.ToInt32(ddlActivitynameV2.SelectedValue), txtHashSequence.Text,
                                                                                                                txtPgPageUrl.Text, Convert.ToInt32(degreeno), Convert.ToInt32(collegeid), txtsubmerchantid.Text, txtbankfeetype.Text);
                                                if (cs.Equals(CustomStatus.RecordUpdated))
                                                {
                                                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this.Page);
                                                    ShowData();
                                                    ClearAll();
                                                }
                                                else if (cs.Equals(CustomStatus.RecordExist))
                                                {
                                                    objCommon.DisplayMessage(this.Page, "Record Already Exists", this.Page);
                                                    ClearAll();
                                                }
                                                else if (cs.Equals(CustomStatus.TransactionFailed))
                                                {
                                                    objCommon.DisplayMessage(this.Page, "Transaction Failed", this.Page);
                                                }

                                                btnSubmitV2.Text = "Submit";
                                                //  ViewState["ACTION"] = null;
                                            }
                                            else
                                            {
                                                int congfigno = 0;
                                                //string receipt_code = ddlReceiptType.SelectedItem.Text;
                                                CustomStatus cs = (CustomStatus)objconfig.InsertPaymentgateway_configV2(Convert.ToInt32(ddlpayment.SelectedValue), txtmerchantid.Text, txtaccesscode.Text,
                                                                                                                      txtchecksumkey.Text, txtrequesturl.Text, txtresponseurl.Text, Convert.ToInt32(ddlinstance.SelectedValue),
                                                                                                                      Active_status, organizationid, Convert.ToInt32(ddlActivitynameV2.SelectedValue),
                                                                                                                      txtHashSequence.Text, txtPgPageUrl.Text, Convert.ToInt32(degreeno), Convert.ToInt32(collegeid), txtsubmerchantid.Text, txtbankfeetype.Text);

                                                if (cs.Equals(CustomStatus.RecordSaved))
                                                {
                                                    objCommon.DisplayMessage(this.Page, "Record Added Successfully", this.Page);
                                                    ShowData();
                                                    ClearAll();
                                                }
                                                else if (cs.Equals(CustomStatus.RecordExist))
                                                {
                                                    objCommon.DisplayMessage(this.Page, "Record Already Exists", this.Page);
                                                    ClearAll();

                                                }
                                                else if (cs.Equals(CustomStatus.TransactionFailed))
                                                {
                                                    objCommon.DisplayMessage(this.Page, "Transaction Failed", this.Page);
                                                }
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(Page, "Please Enter PG Page URL!", this.Page);
                                        }

                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(Page, "Please Enter Response URL!", this.Page);
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(Page, "Please Enter Request URL/Base Url!", this.Page);
                                }

                            }
                            else
                            {
                                objCommon.DisplayMessage(Page, "Please Enter Checksum Key/Working Key/Salt key/Sign Key!", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(Page, "Please Enter Access Code/Encryption Key!", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(Page, "Please Enter Merchant Id/Key!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(Page, "Please Select Instance!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(Page, "Please Select Payment Gateway Name!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration_V2.btnSubmitV2_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnclearV2_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ShowData()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objconfig.GetData_Payment_gateway_configV2();
            if (ds != null && ds.Tables.Count > 0)
            {
                lvPayConfig.DataSource = ds;
                lvPayConfig.DataBind();
                pnlPGCongig.Visible = true;
            }
            else
            {
                lvPayConfig.DataSource = null;
                lvPayConfig.DataBind();
                pnlPGCongig.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration.ShowData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void ClearAll()
    {
        ddlpayment.SelectedIndex = 0;
        txtmerchantid.Text = string.Empty;
        txtaccesscode.Text = string.Empty;
        txtchecksumkey.Text = string.Empty;
        ddlinstance.SelectedIndex = 0;
        txtrequesturl.Text = string.Empty;
        txtresponseurl.Text = string.Empty;
        txtPgPageUrl.Text = string.Empty;
        txtHashSequence.Text = string.Empty;
        ddlActivitynameV2.SelectedIndex = 0;
        txtsubmerchantid.Text = string.Empty;
        txtbankfeetype.Text = string.Empty;

        btnSubmitV2.Text = "Submit";
        ViewState["ACTION2"] = null;
    }

    protected void btnEditrecords_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmitV2.Text = "Update";
            ImageButton editrecord = sender as ImageButton;
            int configid = int.Parse(editrecord.CommandArgument);

            DataSet ds = objconfig.GetData_Payment_gateway_configV2(configid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objCommon.FillDropDownList(ddlpayment, "ACD_PAYMENT_GATEWAY", "PAYID", "PAY_GATEWAY_NAME", "PAYID > 0", "PAYID DESC");
                ddlpayment.SelectedValue = ((dr["PAY_ID"].ToString() != string.Empty) ? (dr["PAY_ID"].ToString()) : string.Empty);
                txtmerchantid.Text = dr["MERCHANT_ID"] == null ? string.Empty : dr["MERCHANT_ID"].ToString();
                txtaccesscode.Text = dr["ACCESS_CODE"] == null ? string.Empty : dr["ACCESS_CODE"].ToString();
                txtchecksumkey.Text = dr["CHECKSUM_KEY"] == null ? string.Empty : dr["CHECKSUM_KEY"].ToString();
                txtrequesturl.Text = dr["REQUEST_URL"] == null ? string.Empty : dr["REQUEST_URL"].ToString();
                txtresponseurl.Text = dr["RESPONSE_URL"] == null ? string.Empty : dr["RESPONSE_URL"].ToString();
                txtHashSequence.Text = dr["HASH_SEQUENCE"] == null ? string.Empty : dr["HASH_SEQUENCE"].ToString();
                txtPgPageUrl.Text = dr["PGPAGE_URL"] == null ? string.Empty : dr["PGPAGE_URL"].ToString();
                ddlinstance.SelectedValue = dr["INSTANCE"] == null ? string.Empty : dr["INSTANCE"].ToString();
                //ddlActivitynameV2.SelectedValue = dr["ACTIVITY_NO"] == null ? string.Empty : dr["ACTIVITY_NO"].ToString();
                //ddlDegreeV2.SelectedValue = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                //ddlCollegeV2.SelectedValue = dr["COLLEGE_ID"] == null ? string.Empty : dr["COLLEGE_ID"].ToString();
                txtsubmerchantid.Text = dr["SUBMERCHANT_ID"] == null ? string.Empty : dr["SUBMERCHANT_ID"].ToString();
                txtbankfeetype.Text = dr["BANKFEE_TYPE"] == null ? string.Empty : dr["BANKFEE_TYPE"].ToString();
                ViewState["Configid"] = dr["CONFIG_ID"].ToString();
                ViewState["ACTION2"] = "Edit";
                //if (dr["ACTIVESTATUS"].ToString() == "Active")
                //{
                //    checkstatus.Checked = true;
                //}
                //else
                //{
                //    checkstatus.Checked = false;
                //}
               
                if (dr["ACTIVESTATUS"].ToString() == "Active")
                {
                    hfdActive1.Value = "true";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatusActive1(true);", true);
                }
                else
                {
                    hfdActive1.Value = "false";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatusActive1(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration_V2.btnEditV2_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void BindCollege()
    {
        try
        {
            DataSet ds = null;
            ds = objconfig.Bind_College_By_OrgId(Convert.ToInt32(Session["OrgId"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollegeV2.DataSource = ds;
                //ddlCollegeV2.DataBind();
                ddlCollegeV2.DataTextField = "COLLEGE_NAME";
                ddlCollegeV2.DataValueField = "COLLEGE_ID";
                ddlCollegeV2.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Tab3 method events
    protected void btnSubmitPayConfiMappV2_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlpaymentV2.SelectedValue != "0" && ddlpaymentV2.SelectedValue != "")
            {
                if (ddlActivitynameV2.SelectedValue != "0" && ddlActivitynameV2.SelectedValue != "")
                {
                    if (ddlReciepttypeV2.SelectedValue != "0" && ddlReciepttypeV2.SelectedValue != "")
                    {
                        if (ddlPayGatewayConfigV2.SelectedValue != "0" && ddlPayGatewayConfigV2.SelectedValue != "")
                        {

                            #region logic

                            int organizationid = Convert.ToInt32(Session["OrgId"]);
                            bool Active_status = false;
                          
                            if (hfdActive2.Value == "true")
                            {
                                Active_status = true;
                            }
                            else
                            {
                                Active_status = false;
                            }

                            var collegeid = ddlCollegeV2.SelectedValue;
                            if (collegeid == "" || collegeid == null) 
                            {
                                collegeid = "0";
                            }
                            var degreeno = ddlDegreeV2.SelectedValue;
                            if (degreeno == "" || degreeno == null)
                            {
                                degreeno = "0";
                            }
                            var branchno = ddlBranchV2.SelectedValue;
                            if (branchno == "" || branchno == null)
                            {
                                branchno = "0";
                            }
                            var semesterno = ddlSemesterV2.SelectedValue;
                            if (semesterno == "" || semesterno == null)
                            {
                                semesterno = "0";
                            }

                            // multi-select semesterNo
                            var semesternos = "";
                            foreach (ListItem items in ddlSemesterV2.Items)
                            {
                                if (items.Selected == true)
                                    semesternos += (items.Value).Split('-')[0] + ',';
                            }
                            semesternos = semesternos.TrimEnd(',');

                            if (ViewState["ACTION3"] != null && ViewState["ACTION3"].Equals("Edit"))
                            {
                                int PayConfig_Id = Convert.ToInt32(ViewState["PaymentGatewayMapp_ID"]);
                                CustomStatus cs = new CustomStatus();
                                cs = (CustomStatus)UpdatePaymentGatewayMappingV2(PayConfig_Id, Convert.ToInt32(ddlpaymentV2.SelectedValue),
                                                                                              ddlReciepttypeV2.SelectedItem.Text,
                                                                                               Convert.ToInt32(ddlActivitynameV2.SelectedValue),
                                                                                               Convert.ToInt32(ddlPayGatewayConfigV2.SelectedValue),
                                                                                               Convert.ToInt32(collegeid),
                                                                                               Convert.ToInt32(degreeno),
                                                                                               Convert.ToInt32(branchno),
                                                                                               semesternos,
                                                                                               Active_status,
                                                                                               organizationid,
                                                                                               Convert.ToInt32(Session["userno"])
                                                                                               );

                            
                                if (cs.Equals(CustomStatus.RecordUpdated))
                                {
                                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this.Page);
                                    ShowDataMappingV2();
                                    ClearAllMappingV2();
                                }
                                else if (cs.Equals(CustomStatus.RecordExist))
                                {
                                    objCommon.DisplayMessage(this.Page, "Record Already Exists", this.Page);
                                    ClearAllMappingV2();
                                }
                                else if (cs.Equals(CustomStatus.TransactionFailed))
                                {
                                    objCommon.DisplayMessage(this.Page, "Transaction Failed", this.Page);
                                }

                                btnSubmitPayConfiMappV2.Text = "Submit";
                                //  ViewState["ACTION"] = null;
                            }
                            else
                            {
                                int congfigno = 0;
                                CustomStatus cs = new CustomStatus();
                                cs = (CustomStatus)InsertPaymentGatewayMappingV2(Convert.ToInt32(ddlpaymentV2.SelectedValue),
                                                                                                ddlReciepttypeV2.SelectedItem.Text,
                                                                                                Convert.ToInt32(ddlActivitynameV2.SelectedValue),
                                                                                                Convert.ToInt32(ddlPayGatewayConfigV2.SelectedValue),
                                                                                                 Convert.ToInt32(collegeid),
                                                                                                Convert.ToInt32(degreeno),
                                                                                                Convert.ToInt32(branchno),
                                                                                                semesternos,
                                                                                                Active_status,
                                                                                                organizationid,
                                                                                                Convert.ToInt32(Session["userno"])
                                                                                                );

                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayMessage(this.Page, "Record Added Successfully", this.Page);
                                    ShowDataMappingV2();
                                    ClearAllMappingV2();
                                }
                                else if (cs.Equals(CustomStatus.RecordExist))
                                {
                                    objCommon.DisplayMessage(this.Page, "Record Already Exists", this.Page);
                                    ClearAllMappingV2();

                                }
                                else if (cs.Equals(CustomStatus.TransactionFailed))
                                {
                                    objCommon.DisplayMessage(this.Page, "Transaction Failed", this.Page);
                                }
                            }

                            #endregion

                        }
                        else
                        {
                            objCommon.DisplayMessage(Page, "Please Select Payment Gateway Configuration!", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(Page, "Please Select Reciept Type!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(Page, "Please Select Activity Name!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(Page, "Please Select Payment Gateway Name!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration_V2.btnSubmitV2_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnSubmitPayConfiMappClearV2_Click(object sender, EventArgs e)
    {
        ClearAllMappingV2();
    }

    protected void ddlpaymentV2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int payId = (Convert.ToInt32(ddlpaymentV2.SelectedValue));
        objCommon.FillDropDownList(ddlPayGatewayConfigV2, "ACD_PG_CONFIGURATION PGC INNER JOIN ACD_PAYMENT_GATEWAY PG ON PGC.PAY_ID = PG.PAYID", "PGC.CONFIG_ID", "CONCAT_WS('-',MERCHANT_ID,SUBMERCHANT_ID,BANKFEE_TYPE,ACCESS_CODE) AS PAY_NAME", "PGC.PAY_ID > 0  AND PGC.ACTIVE_STATUS = 1 AND  PGC.PAY_ID=" + payId, "CONFIG_ID DESC"); // + "AND CDB.COLLEGE_ID IN (" + Session["college_nos"] + ")"
    }

    private void ShowDataMappingV2()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objconfig.GetData_PaymentGatewayMappingV2();
            if (ds != null && ds.Tables.Count > 0)
            {
                lvPaymentGatewayMapping.DataSource = ds;
                lvPaymentGatewayMapping.DataBind();
                div_PaymentGatewayMappingList.Visible = true;
            }
            else
            {
                lvPaymentGatewayMapping.DataSource = null;
                lvPaymentGatewayMapping.DataBind();
                div_PaymentGatewayMappingList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration_V2.ShowDataMappingV2() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void ClearAllMappingV2()
    {
       
        ddlpaymentV2.SelectedIndex = 0;
        ddlReciepttypeV2.SelectedIndex = 0;
        ddlActivitynameV2.SelectedIndex = 0;
        ddlPayGatewayConfigV2.SelectedIndex = 0;
        ddlDegreeV2.SelectedIndex = -1;
        ddlCollegeV2.SelectedIndex = -1;
        ddlBranchV2.SelectedIndex = -1;
        ddlSemesterV2.SelectedIndex = -1;
        btnSubmitPayConfiMappV2.Text = "Submit";
        ViewState["ACTION3"] = null;

    }

    protected void btnEditMappingV2_Click(object sender, ImageClickEventArgs e)
    {
       
        try
        {
            btnSubmitPayConfiMappV2.Text = "Update";
            ImageButton editrecord = sender as ImageButton;
            int Pay_MappingId = int.Parse(editrecord.CommandArgument);
            DataSet ds = objconfig.GetEdit_PaymentGatewayMappingV2(Pay_MappingId);
          
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objCommon.FillDropDownList(ddlpaymentV2, "ACD_PAYMENT_GATEWAY", "PAYID", "PAY_GATEWAY_NAME", "PAYID > 0", "PAYID DESC");
                ddlpaymentV2.SelectedValue = ((dr["PAY_ID"].ToString() != string.Empty) ? (dr["PAY_ID"].ToString()) : string.Empty);
                var recieptTyp  = dr["RECIEPT_TYPE"] == null ? string.Empty : dr["RECIEPT_TYPE"].ToString();


                objCommon.FillDropDownList(ddlReciepttypeV2, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_CODE", "RCPTTYPENO > 0", "RCPTTYPENO ASC");

                int reciptId = Convert.ToInt32(objCommon.LookUp("ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_CODE='" + recieptTyp + "'"));
                ddlReciepttypeV2.SelectedValue = reciptId.ToString();

                ddlActivitynameV2.SelectedValue = dr["ACTIVITY_NO"] == null ? string.Empty : dr["ACTIVITY_NO"].ToString();
                objCommon.FillDropDownList(ddlPayGatewayConfigV2, "ACD_PG_CONFIGURATION PGC INNER JOIN ACD_PAYMENT_GATEWAY PG ON PGC.PAY_ID = PG.PAYID", "PGC.CONFIG_ID", "CONCAT_WS('-',MERCHANT_ID,SUBMERCHANT_ID,BANKFEE_TYPE,ACCESS_CODE) AS PAY_NAME", "PGC.PAY_ID > 0  AND PGC.ACTIVE_STATUS = 1 AND  PGC.PAY_ID=" + Convert.ToInt32(ddlpaymentV2.SelectedValue), "CONFIG_ID DESC");
                ddlPayGatewayConfigV2.SelectedValue = ((dr["PAYMENT_CONFIG_ID"].ToString() != string.Empty) ? (dr["PAYMENT_CONFIG_ID"].ToString()) : string.Empty);
             
                if (dr["COLLEGE_ID"].ToString() == "0" || dr["COLLEGE_ID"] == null)
                {
                    //BindCollege();
                 }
                else 
                {
                    //BindCollege();
                    ddlCollegeV2.SelectedValue = dr["COLLEGE_ID"] == null ? string.Empty : dr["COLLEGE_ID"].ToString();
                }

                if (dr["DEGREENO"].ToString() == "0" || dr["DEGREENO"] == null)
                {
                    objCommon.FillListBox(ddlDegreeV2, "ACD_DEGREE D LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEGREENO=CDB.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "DEGREENO"); // + "AND CDB.COLLEGE_ID IN (" + Session["college_nos"] + ")"
               }
                else
                {
                    objCommon.FillListBox(ddlDegreeV2, "ACD_DEGREE D LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEGREENO=CDB.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "DEGREENO"); // + "AND CDB.COLLEGE_ID IN (" + Session["college_nos"] + ")"
                    ddlDegreeV2.SelectedValue = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                }

                if (dr["BRANCHNO"].ToString() == "0" || dr["BRANCHNO"] == null)
                {
                    objCommon.FillListBox(ddlBranchV2, "ACD_BRANCH B LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON B.BRANCHNO=CDB.BRANCHNO", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "BRANCHNO");
                 }
                else
                {
                    objCommon.FillListBox(ddlBranchV2, "ACD_BRANCH B LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON B.BRANCHNO=CDB.BRANCHNO", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "BRANCHNO");
                    ddlBranchV2.SelectedValue = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
                }

                if (dr["SEMESTERNO"].ToString() == "0" || dr["SEMESTERNO"] == null)
                {
                    objCommon.FillListBox(ddlSemesterV2, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO> 0 ", "SEMESTERNO");
                }
                else
                {
                    objCommon.FillListBox(ddlSemesterV2, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO> 0 ", "SEMESTERNO");
                    var selected_sem = dr["SEMESTERNO"].ToString();                 
                    var sems = selected_sem.Split(',');
                  
                    // multi-select semesterNos bind
                    foreach (ListItem Semesteritem in ddlSemesterV2.Items)
                    {
                        string[] semesterValues = Semesteritem.Value.Split(',');
                        foreach (string sem in sems)
                        {
                            if (semesterValues[0].Trim() == sem.Trim())
                            {
                                Semesteritem.Selected = true;
                                break;
                            }
                        }

                    }

                }
                //ddlDegreeV2.SelectedValue = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                //ddlCollegeV2.SelectedValue = dr["COLLEGE_ID"] == null ? string.Empty : dr["COLLEGE_ID"].ToString();
                //ddlBranchV2.SelectedValue = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
                //ddlSemesterV2.SelectedValue = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();

                ViewState["PaymentGatewayMapp_ID"] = dr["PGM_ID"].ToString();
                ViewState["ACTION3"] = "Edit";
               
                if (dr["ACTIVESTATUS"].ToString() == "Active")
                {
                    hfdActive2.Value = "true";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatusActive2(true);", true);
                }
                else
                {
                    hfdActive2.Value = "false";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatusActive2(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration_V2.btnEditMappingV2_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Controller Method
    public int InsertPaymentGatewayMappingV2(int payid, string reciepttype, int activity, int payment_config_Id, int collegeId, int degreeNo, int branchNo, string semesterNo, bool active_status, int organi_id, int created_by)
    {
        int status = Convert.ToInt32(CustomStatus.Others);
        try
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;

            objParams = new SqlParameter[12];
            objParams[0] = new SqlParameter("@P_PAY_ID", payid);
            objParams[1] = new SqlParameter("@P_RECIEPT_TYPE", reciepttype);
            objParams[2] = new SqlParameter("@P_ACTIVITY_NO", activity);
            objParams[3] = new SqlParameter("@P_PAYMENT_CONFIG_ID", payment_config_Id);
            objParams[4] = new SqlParameter("@P_COLLEGE_ID", collegeId);
            objParams[5] = new SqlParameter("@P_DEGREENO", degreeNo);
            objParams[6] = new SqlParameter("@P_BRANCHNO", branchNo);
            objParams[7] = new SqlParameter("@P_SEMESTERNO", semesterNo);
            objParams[8] = new SqlParameter("@P_ACTIVE_STATUS", active_status);
            objParams[9] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
            objParams[10] = new SqlParameter("@P_CREATED_BY", created_by);
            objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[11].Direction = System.Data.ParameterDirection.Output;

            object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PG_INSERTPAYMENT_GATEWAY_MAPPING_V2", objParams, true);

            if (obj != null && obj.ToString() == "1")
                status = Convert.ToInt32(CustomStatus.RecordSaved);
            else if (obj != null && obj.ToString() == "-99")
                status = Convert.ToInt32(CustomStatus.RecordExist);
            else
                status = Convert.ToInt32(CustomStatus.Error);
        }
        catch (Exception ex)
        {
            status = Convert.ToInt32((CustomStatus.Error));
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentGatewayMappingV2-> " + ex.ToString());
        }
        return status;
    }

    public int UpdatePaymentGatewayMappingV2(int pgmid, int payid, string reciepttype, int activity, int payment_config_Id, int collegeId, int degreeNo, int branchNo, string semesterNo, bool active_status, int organi_id, int created_by)
    {
        int status = Convert.ToInt32(CustomStatus.Others);
        try
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;

            objParams = new SqlParameter[13];
            objParams[0] = new SqlParameter("@P_PGM_ID", pgmid);
            objParams[1] = new SqlParameter("@P_PAY_ID", payid);
            objParams[2] = new SqlParameter("@P_RECIEPT_TYPE", reciepttype);
            objParams[3] = new SqlParameter("@P_ACTIVITY_NO", activity);
            objParams[4] = new SqlParameter("@P_PAYMENT_CONFIG_ID", payment_config_Id);
            objParams[5] = new SqlParameter("@P_COLLEGE_ID", collegeId);
            objParams[6] = new SqlParameter("@P_DEGREENO", degreeNo);
            objParams[7] = new SqlParameter("@P_BRANCHNO", branchNo);
            objParams[8] = new SqlParameter("@P_SEMESTERNO", semesterNo);
            objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", active_status);
            objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
            objParams[11] = new SqlParameter("@P_CREATED_BY", created_by);
            objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[12].Direction = System.Data.ParameterDirection.Output;

            object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PG_UPDATEPAYMENT_GATEWAY_MAPPING_V2", objParams, true);

            if (obj != null && obj.ToString() == "2")
                status = Convert.ToInt32(CustomStatus.RecordUpdated);
            else if (obj != null && obj.ToString() == "-99")
                status = Convert.ToInt32(CustomStatus.RecordExist);
            else
                status = Convert.ToInt32(CustomStatus.Error);
        }
        catch (Exception ex)
        {
            status = Convert.ToInt32((CustomStatus.Error));
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentGatewayMappingV2-> " + ex.ToString());
        }
        return status;
    }

    #endregion


}