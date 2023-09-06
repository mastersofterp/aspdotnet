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

public partial class HOSTEL_Default : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objconfig = new ConfigController();
    HostelController objhostelcon = new HostelController();

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
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                pnlradoselect.Visible = true;
                pnlPay_master.Visible = true;
                pnlPayConfig.Visible = false;
            }
            ViewState["action"] = "add";
            BindCollege();
            BindListView();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PG_Configuration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PG_Configuration.aspx");
        }
    }
    protected void rblpg_config_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblpg_config.SelectedValue == "1")
            {
                pnlradoselect.Visible = true;
                pnlPay_master.Visible = true;
                pnlPayConfig.Visible = false;
            }
            else if (rblpg_config.SelectedValue == "2")
            {
                pnlradoselect.Visible = true;
                pnlPay_master.Visible = false;
                pnlPayConfig.Visible = true;
                ShowData();
                objCommon.FillDropDownList(ddlpayment, "ACD_PAYMENT_GATEWAY", "PAYID", "PAY_GATEWAY_NAME", "PAYID > 0", "PAYID DESC");
                objCommon.FillDropDownList(ddlActivityname, "ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME", "ACTIVITYNO > 0", "ACTIVITYNO");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D LEFT OUTER JOIN  ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEGREENO=CDB.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]), "DEGREENO"); // + "AND CDB.COLLEGE_ID IN (" + Session["college_nos"] + ")"
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PG_Configuration.rblpg_config_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objconfig.GetPaymentGateway_master();
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
                objUCommon.ShowError(Page, "PG_Configuration.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            //if (chkstatus.Checked)
            //{
            //    status = 1;
            //}
            //else
            //{
            //    status = 0;
            //}

            //Added By Rishabh on Dated 01/11/2021
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
                CustomStatus cs = (CustomStatus)objconfig.UpdatePayment_config(payid, txtPaymentGName.Text, status);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {

                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", this.Page);
                    btnSave.Text = "Submit";
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
                CustomStatus cs = (CustomStatus)objconfig.Addpayment_config(txtPaymentGName.Text, status);
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PG_Configuration.btnSave_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //lvPayMaster.DataSource = null;
        //lvPayMaster.DataBind();
    }
    private void Clear()
    {
        txtPaymentGName.Text = string.Empty;
        ddlActivityname.SelectedIndex = ddlDegree.SelectedIndex = 0;

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSave.Text = "Update";
            ImageButton btnEditRecord = sender as ImageButton;
            int Payid = int.Parse(btnEditRecord.CommandArgument);
            DataSet ds = objconfig.GetEditPaymentGateway_master(Payid);
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlpayment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlinstance_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int organizationid = Convert.ToInt32(Session["OrgId"]);
            int Active_status = 0;
            //Added By Rishabh on Dated 01/11/2021
            if (hfdActive1.Value == "true")
            {
                Active_status = 1;
            }
            else
            {
                Active_status = 0;
            }
            if (ViewState["ACTION"] != null && ViewState["ACTION"].Equals("Edit"))
            {
                int congno = Convert.ToInt32(ViewState["Configid"]);
                CustomStatus cs = (CustomStatus)objhostelcon.UpdateadhostelPaymentgateway_config(congno, Convert.ToInt32(ddlpayment.SelectedValue), txtmerchantid.Text,
                                                                                txtaccesscode.Text, txtchecksumkey.Text, txtrequesturl.Text, txtresponseurl.Text,
                                                                                Convert.ToInt32(ddlinstance.SelectedValue), Active_status,
                                                                                organizationid, Convert.ToInt32(ddlActivityname.SelectedValue), txtHashSequence.Text,
                                                                                txtPgPageUrl.Text, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
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

                btnSubmit.Text = "Submit";
                //  ViewState["ACTION"] = null;
            }
            else
            {
                int congfigno = 0;
                //string receipt_code = ddlReceiptType.SelectedItem.Text;
                CustomStatus cs = (CustomStatus)objhostelcon.InserthostelPaymentgateway_config(Convert.ToInt32(ddlpayment.SelectedValue), txtmerchantid.Text, txtaccesscode.Text,
                                                                                      txtchecksumkey.Text, txtrequesturl.Text, txtresponseurl.Text, Convert.ToInt32(ddlinstance.SelectedValue),
                                                                                      Active_status, organizationid, Convert.ToInt32(ddlActivityname.SelectedValue),
                                                                                      txtHashSequence.Text, txtPgPageUrl.Text, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration.btnSubmit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    private void ShowData()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objhostelcon.GetData_HOSTEL_Payment_gateway_config1();
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
        ddlActivityname.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
    }
    protected void btnEditrecords_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            ImageButton editrecord = sender as ImageButton;
            int configid = int.Parse(editrecord.CommandArgument);

            DataSet ds = objhostelcon.GetData_Hostel_Payment_gateway_config(configid);
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
                ddlActivityname.SelectedValue = dr["ACTIVITY_NO"] == null ? string.Empty : dr["ACTIVITY_NO"].ToString();
                ddlDegree.SelectedValue = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                ddlCollege.SelectedValue = dr["COLLEGE_ID"] == null ? string.Empty : dr["COLLEGE_ID"].ToString();
                ViewState["Configid"] = dr["CONFIG_ID"].ToString();
                ViewState["ACTION"] = "Edit";
                //if (dr["ACTIVESTATUS"].ToString() == "Active")
                //{
                //    checkstatus.Checked = true;
                //}
                //else
                //{
                //    checkstatus.Checked = false;
                //}
                //Added By Rishabh on Dated 01/11/2021
                if (dr["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive1(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive1(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PG_Configuration.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
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
                ddlCollege.DataSource = ds;
                //ddlCollege.DataBind();
                ddlCollege.DataTextField = "COLLEGE_NAME";
                ddlCollege.DataValueField = "COLLEGE_ID";
                ddlCollege.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlActivityname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lvPayConfig_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lvPayMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}