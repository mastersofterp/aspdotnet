//CREATED BY - IFTAKHAR KHAN
//DATED - 27-MARCH-2014
//MODIFIED ON - 1-APRIL-2014
//APPROVED BY - PIYUSH R
//PURPOSE - THIS PAGE IS USED TO ENTRY MISCELLANEOUS FEES DETAIL

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_MASTERS_MiscFeeDescription : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    // Fill Dropdown lists                
                    //this.objCommon.FillDropDownList(ddlCounterUser, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (1, 4)", "UA_FULLNAME");
                    objCommon.FillDropDownList(ddlMisc, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "BELONGS_TO='M'", "");
                    objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNAME <> ''", "BANKNO");
                    objCommon.FillDropDownList(ddlBankT, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNAME <> ''", "BANKNO");
                    this.objCommon.FillDropDownList(ddlSearchPanel, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                    ddlSearchPanel.SelectedIndex = 1;
                    ddlSearchPanel_SelectedIndexChanged(sender, e);
                    if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {                     
                        ddlPaymentType.SelectedValue = "R";
                        //ddlPaymentType.Enabled = false;                       
                    }
                    else
                    {
                        ddlPaymentType.SelectedValue = "R";
                    }
                    rbdstudent.SelectedValue = "0";
                    usnno.Visible = true;
                    RecieptTypeController recieptTypeController = new RecieptTypeController();
                    DataSet ds = recieptTypeController.GetRecieptTypes();
                    txtReceiptDate.Text = (System.DateTime.Now).ToShortDateString();
                    txtDDDate.Text = (System.DateTime.Now).ToShortDateString();
                    ViewState["action"] = "add";
                    Session["miscdesc"] = null;
                    ViewState["DDSRNO"] = null;
                    if (Request.QueryString["PaymentMode"] != null && Request.QueryString["PaymentMode"].ToString() != null)
                        ViewState["PaymentMode"] = Request.QueryString["PaymentMode"].ToString();
                    lblReceiptNo.ForeColor = System.Drawing.Color.Red;
                }
                //this.ShowAllCounters();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
        }
    }
    #endregion
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        //ViewState["idno"] = idno;
        
        if(ddlMisc.SelectedValue=="0")
        {
            objCommon.DisplayMessage(updpnl, "Please Select Cash Book ", this.Page);
            return;
        }
        else if (rbdstudent.SelectedValue == "")
        {
            objCommon.DisplayMessage(updpnl, "Please Select Internal or External Student", this.Page);
            return;
        }
        else if (txtTotalAmount.Text == "" || txtTotalAmount.Text == "0.00")
        {
            objCommon.DisplayMessage(updpnl, "Please Add Heads to Generate Receipt", this.Page);
            return;
        }
        else if (txtPayType.Text != string.Empty)
        {
            if (txtPayType.Text == "T")
            {
                if (txtTransReff.Text == "" || ddlBankT.SelectedItem.Text == "")
                {
                    objCommon.DisplayMessage(updpnl, "Please Add Transaction Id and BankName", this.Page);
                }
                else
                {
                    string idno;
                    if (rbdstudent.SelectedValue == "1")
                    {
                        idno = string.Empty;
                        submitData();
                    }
                    else
                    {
                        idno = hdfidno.Value;
                        if (idno == "")
                        {
                            objCommon.DisplayMessage(updpnl, "For internal student First select the student by using the selection criteria and then pay the fees  ", this.Page);
                        }
                        else
                        {
                            submitData();
                        }
                    } 
                }
            }
            else if (txtPayType.Text == "D")
            {
                if (txtDDNo.Text == "" || txtDDAmount.Text == "" || txtDDCity.Text == "" || txtDDDate.Text == "" || ddlBank.SelectedItem.Text == "")
                {
                    if (txtDDNo.Text == "")
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Demand Draft Number ", this.Page);
                    }
                    else if (txtDDAmount.Text == "")
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Demand Draft Amount ", this.Page);
                    }
                    else if (txtDDCity.Text == "")
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter City ", this.Page);
                    }
                    else if (txtDDDate.Text == "")
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Demand Draft Date ", this.Page);
                    }
                    else if (ddlBank.SelectedItem.Text == "")
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Bank Details ", this.Page);
                    }
                }
                else
                {
                    string idno;
                    if (rbdstudent.SelectedValue == "1")
                    {
                        idno = string.Empty;
                        submitData();
                    }
                    else
                    {
                        idno = hdfidno.Value;
                        if (idno == "")
                        {
                            objCommon.DisplayMessage(updpnl, "For internal student First select the student by using the selection criteria and then pay the fees ", this.Page);
                        }
                        else
                        {
                            submitData();
                        }
                    } 
                }
            }
            else if (txtPayType.Text == "C")
            {
                if (txtDDNo.Text == "" || txtDDAmount.Text == "" || txtDDCity.Text == "" || txtDDDate.Text == "" || ddlBank.SelectedItem.Text == "")
                {
                    if (txtTransReff.Text == "" || ddlBankT.SelectedItem.Text == "")
                    {
                        string idno;
                        if (rbdstudent.SelectedValue == "1")
                        {
                            idno = string.Empty;
                            submitData();
                        }
                        else
                        {
                            idno = hdfidno.Value;
                            if (idno == "")
                            {
                                objCommon.DisplayMessage(updpnl, "For internal student First select the student by using the selection criteria and then pay the fees ", this.Page);
                            }
                            else
                            {
                                submitData();
                            }
                        }
                    }
                   else
                    {
                        objCommon.DisplayMessage(updpnl, "Already selected Transaction Pay type as T ", this.Page);
                        txtPayType.Text = "T";
                        txtTransReff.Text = string.Empty;
                        ddlBankT.SelectedIndex = 0;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Already selected DD Pay type as D ", this.Page);
                    txtPayType.Text = "D";
                    txtDDNo.Text = string.Empty;
                    txtDDDate.Text = string.Empty;
                    txtDDAmount.Text = string.Empty;
                    txtDDCity.Text = string.Empty;
                    ddlBank.SelectedIndex = 0;
                }
            }
        }
    }
    private void submitData()
    {
        FeeCollectionController feeController = new FeeCollectionController();
        string idno;
        if (rbdstudent.SelectedValue == "1")
        {
            idno = string.Empty;
           // submitData();
        }
        else
        {
            idno = hdfidno.Value;
        }
        double amt = 0.00;
        double cgst = 0.00;
        double sgst = 0.00;
        if (chkgstpay.Checked)
        {
            amt = Convert.ToDouble(lblcgtotal.Text);

            cgst = (Convert.ToDouble(txtTotalAmount.Text) + (Convert.ToDouble(txtTotalAmount.Text) * Convert.ToDouble(ViewState["cgst"]) / 100)) - Convert.ToDouble(txtTotalAmount.Text);
            sgst = (Convert.ToDouble(txtTotalAmount.Text) + (Convert.ToDouble(txtTotalAmount.Text) * Convert.ToDouble(ViewState["sgst"]) / 100)) - Convert.ToDouble(txtTotalAmount.Text);

        }
        else
        {
            amt = (txtTotalAmount.Text != string.Empty) ? Convert.ToDouble(txtTotalAmount.Text) : 0.00;
        }
        DateTime ddDate = DateTime.MinValue;
        if (txtDDDate.Text != "")
        {
            ddDate = Convert.ToDateTime(txtDDDate.Text);
        }
        //added by JAY TAKALKHEDE ON DATE 15/07/2022
        int bankid;
        string bankname;
        if (txtPayType.Text == "T")
        {

            bankid = Convert.ToInt32(ddlBankT.SelectedValue);
            bankname = ddlBankT.SelectedItem.Text;
        }
        else if (txtPayType.Text == "D")
        {
            bankid = Convert.ToInt32(ddlBank.SelectedValue);
            bankname = ddlBank.SelectedItem.Text;
        }
        else
        {
            bankid = 0;
            bankname = " ";
        }
        string trnsreff=txtTransReff.Text;

        int id_no = (idno != string.Empty) ? Convert.ToInt32(idno) : 0;
        int output = feeController.AddMiscFeesDesc(id_no, txtStudName.Text, Convert.ToInt32(ddlMisc.SelectedValue), Convert.ToDateTime(txtReceiptDate.Text), lblReceiptNo.Text, txtNarration.Text, txtPayType.Text, txtDDNo.Text, amt, ddDate, bankid, bankname, txtDDCity.Text, Session["userno"].ToString(), Session["IPaddress"].ToString(), Session["colcode"].ToString(), ddlPaymentType.SelectedValue, cgst, sgst, Convert.ToInt32(Session["currentsession"]), trnsreff, Convert.ToInt32(rbdstudent.SelectedValue), Convert.ToInt32(Session["OrgId"]));
        ViewState["OUTPUT"] = output;
        if (output != -99)
        {
            foreach (ListViewDataItem dataitem in ListView1.Items)
            {
                Label lblhead = (dataitem.FindControl("lblHead")) as Label;
                Label lblAmount = (dataitem.FindControl("lblAmount")) as Label;
                Label lblheadcode = (dataitem.FindControl("lblHead")) as Label;
                feeController.AddMiscTransDesc(Convert.ToInt32(output), lblheadcode.ToolTip, lblhead.Text, Convert.ToDouble(lblAmount.Text), Session["userno"].ToString(), Session["IPaddress"].ToString(), Session["colcode"].ToString(), Convert.ToInt32(Session["OrgId"]));
            }
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
                string STATUS = objCommon.LookUp("MISCDCR", "STUDSTATUS", "MISCDCRSRNO=" + ViewState["OUTPUT"] +"");
                if (STATUS == "1")
                {
                    ShowReport_rcpit("Miscellanious Fees", "rptMiscReport_EXTERNAL_RCPIT.rpt");
                }
                else
                {
                    ShowReport_rcpit("Miscellanious Fees", "rptMiscReport_RCPIT.rpt");
                }
            }
            else
            {
                ShowReport("Miscellanious Fees", "rptMiscReport.rpt");
            }
        }
        objCommon.DisplayMessage(updpnl, "Data Saved Successfully", this.Page);
        this.clear();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlMisc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMisc.SelectedIndex != 0)
        {
            objCommon.FillDropDownList(ddlHead, "MISCHEAD_MASTER", "MISCHEADCODE", "MISCHEAD", "CBOOKSRNO=" + ddlMisc.SelectedValue + "", "MISCHEAD");
            //ddlPaymentType.SelectedValue = "C";
          //  usnno.Visible = true;
            lblReceiptNo.Text = string.Empty;
            lblReceiptNo.Text = this.GetNewReceiptNo();
           // txtStudIdno.Visible = true;
            txtStudName.Visible = true;
            divpanel.Attributes.Add("display", "none");
            //lblYear.Visible = true;
           // btnShow.Visible = true;
        }
        else
        {
           // usnno.Visible = false;
        }
    }
   
    private void bindData(DataRow dr)
    {
        if (dr["STUDNAME"].ToString() != null)
            txtStudName.Text = dr["STUDNAME"].ToString();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        trListview.Visible = false;

        int a = (Convert.ToInt32(ViewState["DDSRNO"]) + 1);
        //int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO =" + txtStudIdno.Text));
        try
        {
            foreach (ListViewDataItem data in ListView1.Items)
            {
                Label head = (data.FindControl("lblHead") as Label);
                if (head.ToolTip == ddlHead.SelectedValue)
                {
                    objCommon.DisplayMessage(updpnl, "Head is Already added....!", this.Page);
                    return;
                }
            }
            if (ddlHead.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updpnl, "Please Select Head ", this.Page);
            }
            else if (txtStudName.Text == "")
            {
                objCommon.DisplayMessage(updpnl, "Please Enter Name", this.Page);
            }
            else if (ddlPaymentType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updpnl, "Please Select Transaction Type", this.Page);
            }
            else if (txtAmount.Text == "")
            {
                objCommon.DisplayMessage(updpnl, "Please Enter Valid Amount", this.Page);
            }
            else
            {
                DataTable dt;
                if (Session["miscdesc"] != null && ((DataTable)Session["miscdesc"]) != null)
                {
                    dt = ((DataTable)Session["miscdesc"]);
                    DataRow dr = dt.NewRow();
                    dr["DD_NO"] = a;
                    dr["MISCHEAD"] = ddlHead.SelectedItem.Text;
                    dr["AMOUNT"] = txtAmount.Text;
                    dr["MISCHEADCODE"] = ddlHead.SelectedValue;
                    dt.Rows.Add(dr);
                    Session["miscdesc"] = dt;
                    txtTotalAmount.Text = (Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtTotalAmount.Text)).ToString();
                    this.bindListview(dt);
                    txtAmount.Text = string.Empty;
                    //txtPayType.Text = "C";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "BindAmt();", true);
                    if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                        txtDDCity.Text = "Shirpur";
                        ddlBankT.SelectedValue = "250";
                        ddlBank.SelectedValue = "250";
                    }
                    ddlHead.SelectedIndex = 0;
                    ViewState["DDSRNO"] = dr["DD_NO"];

                }
                else
                {
                    ViewState["DDSRNO"] = 1;
                    dt = this.getData();
                    //dt = ((DataTable)Session["miscdesc"]);
                    DataRow dr = dt.NewRow();
                    dr["DD_NO"] = Convert.ToInt32(ViewState["DDSRNO"]);
                    dr["MISCHEAD"] = ddlHead.SelectedItem.Text;
                    dr["AMOUNT"] = txtAmount.Text;
                    dr["MISCHEADCODE"] = ddlHead.SelectedValue;
                    dt.Rows.Add(dr);

                    //foreach (ListViewItem li in ListView1.Items)
                    //    sum += int.Parse(li.Subitmes[2].Text);
                    //txt.Text = txtAmount.Text;
                    txtTotalAmount.Text = (Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtTotalAmount.Text)).ToString();
                    Session["miscdesc"] = dt;
                    this.bindListview(dt);
                    txtAmount.Text = string.Empty;
                    //txtPayType.Text = "C";
                    if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                        txtDDCity.Text = "Shirpur";
                        ddlBankT.SelectedValue = "250";
                        ddlBank.SelectedValue = "250";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "BindAmt();", true);
                    divDDDetails.Visible = true;
                    ddlHead.SelectedIndex = 0;
                }

            }
            if (chkgstpay.Checked)
            {
                double totalgst = 0.00;
                if (ListView1.Items.Count > 0)
                {
                    DataSet ds = objCommon.FillDropDown("ACD_GST_HEAD_MASTER", "GSTNAME", "GSTPERCENTAGE", "GSTHEADNO>0", "");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lblcgper.Text = ds.Tables[0].Rows[0][1].ToString();
                        lblsgper.Text = ds.Tables[0].Rows[1][1].ToString();
                        totalgst = Convert.ToDouble(lblcgper.Text) + Convert.ToDouble(lblsgper.Text);
                        ViewState["cgst"] = lblcgper.Text;
                        ViewState["sgst"] = lblsgper.Text;
                    }

                    double totalcgst = Convert.ToDouble(txtTotalAmount.Text) + (Convert.ToDouble(txtTotalAmount.Text) * Convert.ToDouble(totalgst) / 100);

                    lblcgtotal.Text = totalcgst.ToString();

                    divcgst.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnEditDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDeleteDDInfo_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            DataTable dt;
            if (Session["miscdesc"] != null && ((DataTable)Session["miscdesc"]) != null)
            {
                dt = ((DataTable)Session["miscdesc"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["miscdesc"] = dt;
                this.bindListview(dt);
                txtTotalAmount.Focus();
                //ViewState["DDSRNO"] = dt;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DD_NO"].ToString() == value)
                {
                    dataRow = dr;
                    txtTotalAmount.Text = (Convert.ToDouble(txtTotalAmount.Text) - Convert.ToDouble(dr["amount"].ToString())).ToString();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void bindListview(DataTable dt)
    {
        try
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
            ListView1.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnEditDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable getData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DD_NO", typeof(int));
        dt.Columns.Add("MISCHEAD", typeof(string));
        dt.Columns.Add("AMOUNT", typeof(double));
        dt.Columns.Add("MISCHEADCODE", typeof(string));
        return dt;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    private string GetNewReceiptNo()
    {
        string recno = string.Empty;
        try
        {
            String FeesSessionStartDate;


            FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");
            //feesRecieptReset = Convert.ToDateTime(objCommon.LookUp("REFF", "Start_Year", ""));

            //string miscdcr = objCommon.LookUp("MISCDCR", "PAY_TYPE", "PAY_TYPE='" + ddlPaymentType.SelectedValue + "' AND CBOOKSRNO=" + ddlMisc.SelectedValue + " AND    MISCDCRSRNO >1774  AND USERID=" + Session["userno"] + " ");//AND  AUDITDATE >= '2015-04-01 00:00:00.000' 
            //string miscdcr = objCommon.LookUp("MISCDCR", "PAY_TYPE", "PAY_TYPE='" + ddlPaymentType.SelectedValue + "' AND CBOOKSRNO=" + ddlMisc.SelectedValue + " AND    USERID=" + Session["userno"] + " ");//AND  AUDITDATE >= '2016-04-01 00:00:00.000' 

            string PermisionType = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO=" + ddlMisc.SelectedValue + "");

            recno = objCommon.LookUp("MISCDCR", "('" + PermisionType + "' COLLATE Latin1_General_CI_AI+'/'+CAST('" + ddlPaymentType.SelectedValue + "' AS NVARCHAR(20))+'/'+ '" + FeesSessionStartDate + "' + '/' +CAST(count(ISNULL(miscdcrsrno,0))+1 AS NVARCHAR(30)))A", "CBOOKSRNO=" + ddlMisc.SelectedValue);

            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return recno;
    }


    private void clear()
    {
        //Response.Redirect(Request.Url.ToString());
        lblcgper.Text = string.Empty;
    //    ddlMisc.SelectedIndex = 0;
        lblReceiptNo.Text = string.Empty;
        txtStudName.Text = string.Empty;
        ddlHead.SelectedIndex = 0;
        txtDDNo.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDCity.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtAmount.Text = string.Empty;
        txtNarration.Text = string.Empty;
        ListView1.Visible = false;
        txtStudName.Text = string.Empty;
       // usnno.Visible = true;
        //btnShow.Visible = false;
        //txtPayType.Text = string.Empty;
        txtTotalAmount.Text = "0.00";
        ddlPaymentType.SelectedIndex = 0;
        txtPayType.Text = string.Empty;
        Session["miscdesc"] = null;
        ViewState["DDSRNO"] = null;
        ListView1.DataSource = null;
        ListView1.DataBind();
        divcgst.Visible = false;
        txtSearchPanel.Text = "";
        ddlDropdown.SelectedIndex = -1;
        ddlSearchPanel.ClearSelection();
        chkgstpay.Checked = false;
        txtTransReff.Text = string.Empty;
        ddlBankT.SelectedIndex = 0;
       // usnno.Visible = false;
        ddlPaymentType.SelectedValue = "R";
        txtSearchPanel.Text = string.Empty;
        lvStudentList.DataSource = null;
        lvStudentList.DataBind();
        lvStudentList.Visible = false;
        txtDDDate.Text = (System.DateTime.Now).ToShortDateString();
        rbdstudent.SelectedIndex = -1;
        rbdstudent.SelectedValue = "0";

    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblReceiptNo.Text = this.GetNewReceiptNo();
    }

    private void validation()
    {
        if (txtDDNo.Text == "" || txtDDAmount.Text == "" || txtDDDate.Text == "" || txtDDCity.Text == "" || ddlBank.SelectedIndex == 0 || txtTotalAmount.Text != txtDDAmount.Text)
        {
            objCommon.DisplayMessage(updpnl, "Please Enter Demand Draft Detail or Total amount is not matched...", this.Page);
        }
        else
        {

        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + ViewState["OUTPUT"] + ",@P_COPY=1,@P_USERNAME=" + Session["userfullname"].ToString() + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport_rcpit(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" +ViewState["OUTPUT"];
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport_rcpit() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Miscallenious Fees", "rptMiscReport.rpt");
    }

    //private void BindData()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON(S.BRANCHNO = B.BRANCHNO)", "REGNO", "S.STUDNAME,B.SHORTNAME", "REGNO LIKE '" + txtStudIdno.Text + "%'", "");
    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        lvStudentList.DataSource = ds;
    //        lvStudentList.DataBind();
    //    }
    //}

    protected void chkgstpay_CheckedChanged(object sender, EventArgs e)
    {
        double totalgst = 0.00;
        if (chkgstpay.Checked)
        {
            if (ListView1.Items.Count > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_GST_HEAD_MASTER", "GSTNAME", "GSTPERCENTAGE", "GSTHEADNO>0", "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblcgper.Text = ds.Tables[0].Rows[0][1].ToString();
                    lblsgper.Text = ds.Tables[0].Rows[1][1].ToString();
                    totalgst = Convert.ToDouble(lblcgper.Text) + Convert.ToDouble(lblsgper.Text);
                    ViewState["cgst"] = lblcgper.Text;
                    ViewState["sgst"] = lblsgper.Text;
                }

                double totalcgst = Convert.ToDouble(txtTotalAmount.Text) + (Convert.ToDouble(txtTotalAmount.Text) * Convert.ToDouble(totalgst) / 100);

                lblcgtotal.Text = totalcgst.ToString();

                divcgst.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please Select Atlease One Head", this.Page);
                chkgstpay.Checked = false;
            }
        }
        else
        {
            lblcgtotal.Text = string.Empty;
            divcgst.Visible = false;
        }
    }
    protected void ddlSearchPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //pnlLV.Visible = false;
          //  lblNoRecords.Visible = false;
            //lvStudentPanel.DataSource = null;
          //  lvStudentPanel.DataBind();
            if (ddlSearchPanel.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearchPanel.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearchPanel.Visible = false;
                        pnlDropdown.Visible = true;
                        divpanel.Attributes.Add("style", "display:block");
                        divDropDown.Attributes.Add("style", "display:block");
                        //divSearchPanel.Attributes.Add("style", "display:block");
                       // divStudInfo.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:none");
                        //divtxt.Visible = false;
                        lblDropdown.Text = ddlSearchPanel.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearchPanel.Visible = true;
                        // pnlDropdown.Visible = false;
                        divDropDown.Attributes.Add("style", "display:none");
                       // divStudInfo.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:block");
                        divpanel.Attributes.Add("style", "display:block");
                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
                divpanel.Attributes.Add("style", "display:none");

            }
            ClearSelection();
        }
        catch
        {
            throw;
        }
    }
    protected void ClearSelection()
    {
        txtSearchPanel.Text = "";
        ddlDropdown.SelectedIndex = -1;
        //pnltextbox.Visible = false;
        ////txtSearch.Visible = false;
        //pnlDropdown.Visible = false;
    }
    private void bindlist(string category, string searchtext)
    {
        
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(searchtext, category);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //pnlLV.Visible = true;
            trListview.Visible = true;
            lvStudentList.Visible = true;
            lvStudentList.DataSource = ds;
            lvStudentList.DataBind();
           // lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentList);//Set label - 
        }
        else
        {
            ddlSearchPanel.ClearSelection();
           // lblNoRecords.Text = "Total Records : 0";
            //objCommon.DisplayMessage("","");
            objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
            lvStudentList.Visible = false;
            lvStudentList.DataSource = null;
            lvStudentList.DataBind();
        }
    }
    protected void btnSearchPanel_Click(object sender, EventArgs e)
    {
       // lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearchPanel.Text;
        }
        bindlist(ddlSearchPanel.SelectedItem.Text, value);
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void rbdstudent_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (rbdstudent.SelectedValue == "0")
            {
                usnno.Visible = true;
                //rbdstudent.SelectedIndex = -1;
            }
            else
            {
                usnno.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.btnConfirmHead_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
