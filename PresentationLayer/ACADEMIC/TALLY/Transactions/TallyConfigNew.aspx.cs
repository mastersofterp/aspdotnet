#region NAMESPACES
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using System.Net;
using IITMS.NITPRM;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using MSXML2;
using System.Text;
#endregion

public partial class ACADEMIC_TALLY_Transactions_TallyConfigNew : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    Con_Acd_TallyConfig ObjTC = new Con_Acd_TallyConfig();
    Ent_Acd_TallyConfig ObjTCM = new Ent_Acd_TallyConfig();

    Con_Acd_CompanyConfig OBJCON = new Con_Acd_CompanyConfig();
    Ent_Acd_CompanyConfig ObjCCM = new Ent_Acd_CompanyConfig();

    Con_Acd_TallyFeeHeads ObjTFC = new Con_Acd_TallyFeeHeads();
    Ent_Acd_TallyFeeHeads ObjTFM = new Ent_Acd_TallyFeeHeads();

    string Message = string.Empty;
    string UsrStatus = string.Empty;

    //string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;

    #region PAGE LOAD EVENTS

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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlQualification, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
            }


            BindTallyConfigurationNew();
            BindTallyConfiguration();
            FillDropDown();
            BindTallyCompany();
            BindDropDowns();
            //ViewState["TallyCompanyConfigId"] = "add";




        }


        divMsg.InnerHtml = string.Empty;
    }




    #endregion


    #region Server Configuration

    protected void btnEdit_click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            btnSubmit.ToolTip = "Click To Update";
            ImageButton btnEdit = sender as ImageButton;
            int BankId = int.Parse(btnEdit.CommandArgument);
            ViewState["TallyConfigId"] = BankId;
            ObjTCM.TallyConfigId = BankId;
            ObjTCM.CommandType = "BindTallyConfigId";
            ObjTCM.CollegeId = Convert.ToInt32(Session["colcode"]);

            DataSet ds = ObjTC.GetAllDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtServerIp.Text = ds.Tables[0].Rows[0]["ServerName"].ToString();
                    txtPortNumber.Text = ds.Tables[0].Rows[0]["PortNumber"].ToString();
                    //txtBankAddress.Text = ds.Tables[0].Rows[0]["BankAddress"].ToString();


                    chkIsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);


                }
                else
                {

                    objCommon.DisplayMessage(upDetails, "Record Not Found", this.Page);
                    return;
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearConfiguration()
    {

        txtServerIp.Text = string.Empty;
        //txtBankAddress.Text = string.Empty;
        txtPortNumber.Text = string.Empty;
        chkIsActive.Checked = true;

        ddlServer.Items.Clear();
        ddlServer.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlReceiptBook.SelectedIndex = 0;
        txtTallyCompany.Text = string.Empty;

        ViewState["TallyConfigId"] = null;
        btnSubmit.Text = "Submit";
        btnSubmit.ToolTip = "Click To Submit";
        lvRep_Company.DataSource = null;
        lvRep_Company.DataBind();

        BindTallyConfigurationNew();
        //     Response.Redirect(Request.Url.ToString());


    }
    public void BindTallyConfigurationNew()
    {
        try
        {

            ObjTCM.CommandType = "BindTallyConfig";
            ObjTCM.CollegeId = Convert.ToInt32(Session["colcode"]);

            DataSet ds = ObjTC.GetAllDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlconfiguration.Visible = true;
                    lvRep_Company.DataSource = ds;
                    lvRep_Company.DataBind();
                }
                else
                {
                    pnlconfiguration.Visible = false;
                    lvRep_Company.DataSource = null;
                    lvRep_Company.DataBind();
                }
            }
            else
            {
                pnlconfiguration.Visible = false;
                lvRep_Company.DataSource = null;
                lvRep_Company.DataBind();
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            long res = 0;
            ObjTCM.ServerName = txtServerIp.Text.Trim();
            ObjTCM.PortNumber = Convert.ToInt32(txtPortNumber.Text);


            ObjTCM.IsActive = chkIsActive.Checked;


            ObjTCM.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjTCM.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjTCM.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
            ObjTCM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTCM.MACAddress = Convert.ToString("0");
            ObjTCM.CollegeId = Convert.ToInt32(Session["colcode"].ToString());

            if (ViewState["TallyConfigId"] == null)
            {
                res = ObjTC.AddUpdateTallyConfig(ObjTCM, ref Message);
            }
            else
            {
                ObjTCM.TallyConfigId = Convert.ToInt32(ViewState["TallyConfigId"].ToString());
                res = ObjTC.AddUpdateTallyConfig(ObjTCM, ref Message);
            }
            if (res == -99)
            {

                objCommon.DisplayMessage(upDetails, "Exception Occure", this.Page);
                FillDropDown();
                return;

            }
            if (res == 0)
            {

                objCommon.DisplayMessage(upDetails, "Record Already Exists", this.Page);
                FillDropDown();
                return;

            }
            if (res <= 0)
            {

                objCommon.DisplayMessage(upDetails, "Record is Not Save", this.Page);
                FillDropDown();
            }
            else
            {
                if (ViewState["TallyConfigId"] == null)
                {
                    objCommon.DisplayMessage(upDetails, "Record Save Successfully", this.Page);
                    ClearConfiguration();
                    BindTallyConfiguration();
                    BindTallyConfigurationNew();
                    
                    FillDropDown();

                }
                else
                {

                    objCommon.DisplayMessage(upDetails, "Record Updated Successfully", this.Page);
                    ClearConfiguration();
                    //  BindTallyConfiguration();
                    BindTallyConfigurationNew();
                   
                    FillDropDown();

                }
            }
        }
        catch (System.Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearConfiguration();

    }

    public void BindTallyConfiguration()
    {
        try
        {
            ObjTCM.CollegeId = Convert.ToInt32(Session["colcode"]);
            ObjTCM.CollegeId = 6;
            ObjTCM.CommandType = "BindTallyConfig";
            DataSet ds = ObjTC.GetAllDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtServerIp.Text = Convert.ToString(dr["ServerName"]);
                        txtPortNumber.Text = Convert.ToString(dr["PortNumber"]);
                        chkIsActive.Checked = Convert.ToBoolean(dr["IsActive"]);
                        ViewState["TallyConfigId"] = Convert.ToInt32(dr["TallyConfigId"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion Server Configuration



    #region Tally Company Configuration
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlReceiptBook, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
        //objCommon.FillDropDownList(ddlServer, "TallyConfig", "TallyConfigId", "ServerName+':'+Convert(varchar(10),PortNumber)ServerName", "TallyConfigId > 0 ", "TallyConfigId");
        //    objCommon.FillDropDownList(ddlServer, "TallyConfig", "TallyConfigId", "ServerName+':'+Convert(varchar(10),PortNumber)ServerName", "TallyConfigId > 0 AND IsActive = 1 AND EndTime = '9999-12-31'", "TallyConfigId");
    }


    public void BindTallyCompany()
    {
        try
        {


            ObjCCM.CommandType = "BindTallyCompany";
            ObjCCM.CollegeId = Convert.ToInt32(Session["colcode"]);

            DataSet ds = OBJCON.GetAllDetails(ObjCCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlCompany.Visible = true;
                    lvRep_CompanyConfiguration.DataSource = ds;
                    lvRep_CompanyConfiguration.DataBind();
                }
                else
                {
                    pnlCompany.Visible = false;
                    lvRep_CompanyConfiguration.DataSource = null;
                    lvRep_CompanyConfiguration.DataBind();
                }
            }
            else
            {
                pnlCompany.Visible = false;
                lvRep_CompanyConfiguration.DataSource = null;
                lvRep_CompanyConfiguration.DataBind();
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }

    }
    private void ClearCompanyConfiguration()
    {

        ddlReceiptBook.SelectedValue = "0";
        //txtBankAddress.Text = string.Empty;
        //   ddlServer.SelectedValue = "0";
        txtTallyCompany.Text = string.Empty;
        chkIsActive.Checked = true;
        ddlServer.Items.Clear();
        ddlServer.Items.Insert(0, new ListItem("Please Select", "0"));

        ViewState["TallyCompanyConfigId"] = null;
        btnSubmit.Text = "Submit";
        btnSubmit.ToolTip = "Click To Submit";
        //DivCompany.Visible = false;


    }

    protected void btnEdittallycompany_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            btnSubmit.ToolTip = "Click To Update";
            ImageButton btnEdit = sender as ImageButton;
            int TallyCompanyConfigId = int.Parse(btnEdit.CommandArgument);
            ViewState["TallyCompanyConfigId"] = TallyCompanyConfigId;
            ObjCCM.TallyCompanyConfigId = TallyCompanyConfigId;
            ObjCCM.CommandType = "BindTallyCompanyId";
            ObjCCM.CollegeId = Convert.ToInt32(Session["colcode"]);
            DataSet dstallyconfigid = objCommon.FillDropDown("TallyConfig", "TallyConfigId", "ServerName+':'+Convert(varchar(10),PortNumber)ServerName", "TallyConfigId = 3 AND IsActive = 1 AND EndTime = '9999-12-31'", "TallyConfigId");

            int tallyConfigId = Convert.ToInt32(objCommon.LookUp("TallyCompanyConfig", "TallyConfigId", "TallyCompanyConfigId=" + TallyCompanyConfigId + ""));
            objCommon.FillDropDownList(ddlServer, "TallyConfig", "TallyConfigId", "ServerName+':'+Convert(varchar(10),PortNumber)ServerName", "TallyConfigId = " + tallyConfigId + " AND IsActive = 1 AND EndTime = '9999-12-31'", "TallyConfigId");
            DataSet ds = OBJCON.GetAllDetails(ObjCCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlReceiptBook.SelectedValue = ds.Tables[0].Rows[0]["CashBookid"].ToString();
                    ddlServer.SelectedValue = ds.Tables[0].Rows[0]["TallyConfigId"].ToString();
                    txtTallyCompany.Text = ds.Tables[0].Rows[0]["TallyCompanyName"].ToString();
                    chkIsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);


                }
                else
                {
                    //objCommon.ShowErrorMessage(Panel_Warning, Label_Warning, CLOUD_COMMON.Message.NoFound, CLOUD_COMMON.MessageType.Alert);
                    objCommon.DisplayMessage("Record Not Found", this);
                    return;
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Error);
        }
    }
    protected void btnsubmittallycompany_Click(object sender, EventArgs e)
    {
        try
        {

            long res = 0;
            ObjCCM.CashBookId = Convert.ToInt32(ddlReceiptBook.SelectedValue);
            ObjCCM.TallyConfigId = Convert.ToInt32(ddlServer.SelectedValue);
            ObjCCM.TallyCompanyName = Convert.ToString(txtTallyCompany.Text.Trim());


            ObjCCM.IsActive = chkIsActive.Checked;


            ObjCCM.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjCCM.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjCCM.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
            ObjCCM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjCCM.MACAddress = Convert.ToString("1");
            ObjCCM.CollegeId = Convert.ToInt32(Session["colcode"].ToString());

            if (ViewState["TallyCompanyConfigId"] == null)
            {
                res = OBJCON.AddUpdateTallyConfig(ObjCCM, ref Message);
            }
            else
            {
                ObjCCM.TallyCompanyConfigId = Convert.ToInt32(ViewState["TallyCompanyConfigId"].ToString());
                res = OBJCON.AddUpdateTallyConfig(ObjCCM, ref Message);
            }
            if (res == -99)
            {
                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
                objCommon.DisplayMessage(upDetails, "Exception Occured", this);
                return;


            }
            if (res == 0)
            {
                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.DuplicateEntry, CLOUD_COMMON.MessageType.Alert);

                //return;/
                objCommon.DisplayMessage(upDetails, "Record Already Exists", this);
                return;
            }
            if (res <= 0)
            {
                // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.NotSaved, CLOUD_COMMON.MessageType.Alert);
                objCommon.DisplayMessage(upDetails, "Record Not Saved", this);
                return;

            }
            else
            {
                if (ViewState["TallyCompanyConfigId"] == null)
                {

                    // objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, CLOUD_COMMON.Message.Saved, CLOUD_COMMON.MessageType.Success);
                    objCommon.DisplayMessage(upDetails, "Record Saved Successfully", this);
                    BindTallyCompany();
                    ClearCompanyConfiguration(); ;
                }
                else
                {
                    //  objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, CLOUD_COMMON.Message.Updated, CLOUD_COMMON.MessageType.Success);
                    objCommon.DisplayMessage(upDetails, "Record Updated Successfully", this);
                    BindTallyCompany();
                    ClearCompanyConfiguration(); ;
                }
            }
        }
        catch (System.Exception ex)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            throw;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ServerXMLHTTP30 serverHTTP = new ServerXMLHTTP30();
        string responseStr = "";
        string StrRequestXML = "";
        DataSet dsCompany = new DataSet();

        StrRequestXML = "<ENVELOPE>" +
                        "  <HEADER>" +
                        "    <TALLYREQUEST>Export Data</TALLYREQUEST>" +
                        "  </HEADER>" +
                        "  <BODY>" +
                        "    <EXPORTDATA>" +
                        "      <REQUESTDESC>" +
                        "        <REPORTNAME>List of Companies</REPORTNAME>" +
                        "        <STATICVARIABLES>" +
                        "          <SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                        "        </STATICVARIABLES>" +
                        "    </REQUESTDESC>" +
                        "    </EXPORTDATA>" +
                        "  </BODY>" +
                        "</ENVELOPE>";


        string selectedCompany = "";



        try
        {

            string Address = "http://" + ddlServer.SelectedItem.Text;
            serverHTTP.open("POST", Address, false, null, null);
            serverHTTP.send(StrRequestXML);
            responseStr = serverHTTP.responseText;
            dsCompany.ReadXml(new StringReader(responseStr));

            if (dsCompany.Tables.Count > 0)
            {
                DataRow dr1 = dsCompany.Tables[0].NewRow();
                dr1[0] = "Please Select";
                dsCompany.Tables[0].Rows.InsertAt(dr1, 0);
                foreach (DataRow drCompany in dsCompany.Tables[0].Rows)
                {
                    selectedCompany = drCompany[0].ToString();
                }
            }

            if (selectedCompany != "")
            {
                txtTallyCompany.Text = selectedCompany;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Exception from HRESULT: 0x80072EFD")
            {
                Showmessage("Connection Not Possible! \\n Either Tally server is not running on remote machine or Machine is not in network.");
                return;
            }
            else
            {
                throw;
            }
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("bank.aspx")));
            url += "../Reports/Commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&format=No";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@CollegeId=" + Session["COLL_ID"] + "";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            //return;
            throw;
        }
    }
    protected void btnReporttallycompany_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Bank Report", "BankMasterReport.rpt");
            ClearCompanyConfiguration();
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            //return;
            throw;
        }
    }
    protected void btncanceltallycompany_Click(object sender, EventArgs e)
    {
        ClearCompanyConfiguration();
    }
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    #endregion Tally Company Configuration


    #region FeeHeads Configuration
    public void BindDropDowns()
    {

        objCommon.FillDropDownList(ddlCashBook, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
    }

    protected void ddlCashBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            repFeeHeads.DataSource = null;
            repFeeHeads.DataBind();

            //  ObjTFM.CommandType = "BindGrid";
            ObjTFM.CashBookName = (ddlCashBook.SelectedValue);
            ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
            //       DataSet ds = ObjTFC.GetFeesHeads(ObjTFM);
            DataSet ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE a inner join ACD_FEE_TITLE b on(a.RECIEPT_CODE = b.RECIEPT_CODE) ", "b.FEE_HEAD, b.FEE_SHORTNAME, b.FEE_LONGNAME", "b.BankLedgerName, b.CashLedgerName", "a.RECIEPT_CODE = '" + ddlCashBook.SelectedValue + "' AND FEE_LONGNAME <> '' AND FEE_LONGNAME IS NOT NULL", "b.FEE_TITLE_NO");

            //   DataSet ds = ObjTFC.GetAllDetails(ObjTFM);
            repFeeHeads.DataSource = ds;
            repFeeHeads.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlFeeHeads.Visible = true;
            }
            else
            {
                pnlFeeHeads.Visible = false;
                objCommon.DisplayMessage(upDetails, "Record Not Found", this.Page);
            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void repFeeHeads_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRowView rowView = (DataRowView)dataItem.DataItem;
                if (ViewState["Ledgers"] == null)
                {


                    ObjTFM.CommandType = "GetServer";
                    ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
                    // DataSet ds = ObjTFC.GetAllDetails(ObjTFM);

                    DataSet ds = objCommon.FillDropDown("ACD_FEE_TITLE", "FEE_TITLE_NO,FEE_HEAD,FEE_SHORTNAME,FEE_LONGNAME", "ISNULL(CashLedgerName,'0')CashLedgerName,	ISNULL(BankLedgerName,'0')BankLedgerName", "RECIEPT_CODE='" + ddlCashBook.SelectedValue + "'", "FEE_TITLE_NO");

                    DataSet dsServer = objCommon.FillDropDown("TallyConfig", "ServerName+':'+Convert(varchar(10),PortNumber) as ServerName", "Convert(varchar(10),PortNumber) portnumber", "IsActive = 1 and CollegeId = " + Session["colcode"] + " and EndTime = '9999-12-31'", "");   //05092022 collegeid=6


                    if (dsServer.Tables[0].Rows[0]["ServerName"] == "")
                    {
                        Showmessage("Server Not Found!. \\n Server Name or Port Number Not Specified.");
                        return;
                    }
                    DataTable dt = GetLedgers(Convert.ToString(dsServer.Tables[0].Rows[0]["ServerName"]));
                    ViewState["Ledgers"] = dt;

                }

                DropDownList ddlCashLedgerName = (DropDownList)e.Item.FindControl("ddlCashLedgerName");
                DropDownList ddlBankLedgerName = (DropDownList)e.Item.FindControl("ddlBankLedgerName");


                DataTable dtLedger = (DataTable)ViewState["Ledgers"];


                ddlCashLedgerName.DataSource = dtLedger;
                ddlCashLedgerName.DataTextField = "Name_Text";
                ddlCashLedgerName.DataValueField = "Name_Text";
                ddlCashLedgerName.DataBind();

                //   ddlCashLedgerName.SelectedValue = Convert.ToString(rowView["CashLedgerName"]);

                ddlCashLedgerName.SelectedValue = Convert.ToString(rowView["CashLedgerName"]);


                ddlBankLedgerName.DataSource = dtLedger;
                ddlBankLedgerName.DataTextField = "Name_Text";
                ddlBankLedgerName.DataValueField = "Name_Text";
                ddlBankLedgerName.DataBind();

                ddlBankLedgerName.SelectedValue = Convert.ToString(rowView["BankLedgerName"]);

            }
        }
        catch (Exception ex)
        {

        }
    }



    public DataTable GetLedgers(string serverName)
    {
        DataTable dt = new DataTable();
        try
        {
            ServerXMLHTTP30 serverHTTP = new ServerXMLHTTP30();
            string responseStr = "";
            StringBuilder StrRequestXML = new StringBuilder();
            string nl = System.Environment.NewLine;
            //StrRequestXML.AppendLine("<ENVELOPE>");
            //StrRequestXML.AppendLine("   <HEADER>");
            //StrRequestXML.AppendLine("       <VERSION>1</VERSON>");
            //StrRequestXML.AppendLine("  I     <TALLYREQUEST>Export</TALLYREQUEST>");
            //StrRequestXML.AppendLine("       <TYPE>Data</TYPE>");
            //StrRequestXML.AppendLine("       <ID>List of Ledgers</ID>");
            //StrRequestXML.AppendLine("   </HEADER>");
            //StrRequestXML.AppendLine("   <BODY>");
            //StrRequestXML.AppendLine("      <DESC>");
            //StrRequestXML.AppendLine("        <STATICVARIABLES>");
            //StrRequestXML.AppendLine("          <EXPLODEFLAG>Yes</EXPLODEFLAG>");
            //StrRequestXML.AppendLine("          <SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>");
            //StrRequestXML.AppendLine("        </STATICVARIABLES>");
            //StrRequestXML.AppendLine("      </DESC>");
            //StrRequestXML.AppendLine("  </BODY>");
            //StrRequestXML.AppendLine("</ENVELOPE>");

            StrRequestXML.AppendLine("<ENVELOPE>");
            StrRequestXML.AppendLine("<HEADER>");
            StrRequestXML.AppendLine("<VERSION>1</VERSION>");
            StrRequestXML.AppendLine("<TALLYREQUEST>Export</TALLYREQUEST>");
            StrRequestXML.AppendLine("<TYPE>Data</TYPE>");
            StrRequestXML.AppendLine("<ID>List of Ledgers</ID>");
            StrRequestXML.AppendLine("</HEADER>");
            StrRequestXML.AppendLine("<BODY>");
            StrRequestXML.AppendLine("<DESC>");
            StrRequestXML.AppendLine("<STATICVARIABLES>");
            StrRequestXML.AppendLine("<EXPLODEFLAG>Yes</EXPLODEFLAG>");
            StrRequestXML.AppendLine("<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>");
            StrRequestXML.AppendLine("</STATICVARIABLES>");
            StrRequestXML.AppendLine("</DESC>");
            StrRequestXML.AppendLine("</BODY>");
            StrRequestXML.AppendLine("</ENVELOPE>");

            DataSet dsLedgers = new DataSet();

            // string Address = "http://" + IpAddress + ":" + portNumber;
            string Address = "http://" + serverName;
            serverHTTP.open("POST", Address, false, null, null);
            serverHTTP.send(StrRequestXML.ToString());   
            responseStr = serverHTTP.responseText;
            string tempstring = responseStr.Replace(Convert.ToString((char)0xFFFF), "");    //Added only for temporarily only
            dsLedgers.ReadXml(new StringReader(tempstring));
            if (dsLedgers.Tables.Count > 0)
            {
                DataRow dr = dsLedgers.Tables[0].NewRow();
                dr[0] = "Please Select";
                dsLedgers.Tables[0].Rows.InsertAt(dr, 0);
                dt = dsLedgers.Tables[0];
            }

        }
        catch (Exception ex)
        {

        }


        return dt;
    }

    protected void btnSubmitFeehead_Click(object sender, EventArgs e)
    {
        try
        {


            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("FeeHeadId", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CashLedgerName", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("BankLedgerName", typeof(string));
            dt.Columns.Add(dc);

            DataRow dr;

            foreach (ListViewDataItem itm in repFeeHeads.Items)
            {
                HiddenField hdnFeeHeadId = (HiddenField)itm.FindControl("hdnFeeHeadId");
                DropDownList ddlCashLedgerName = (DropDownList)itm.FindControl("ddlCashLedgerName");
                DropDownList ddlBankLedgerName = (DropDownList)itm.FindControl("ddlBankLedgerName");
                if (hdnFeeHeadId != null && ddlCashLedgerName != null && ddlBankLedgerName != null)
                {
                    dr = dt.NewRow();
                    dr["FeeHeadId"] = (hdnFeeHeadId.Value);
                    dr["CashLedgerName"] = ddlCashLedgerName.SelectedValue;
                    dr["BankLedgerName"] = ddlBankLedgerName.SelectedValue;
                    dt.Rows.Add(dr);
                }
                else
                {
                    objCommon.DisplayMessage("Exception Occure", this.Page);
                    return;
                }
            }


            ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
            ObjTFM.CashBookName = Convert.ToString(ddlCashBook.SelectedValue);

            ObjTFM.ModifiedBy = Convert.ToInt32(Session["userno"]);
            ObjTFM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTFM.MACAddress = Convert.ToString("0");
            ObjTFM.FeeHeadTally = dt;
            long res = ObjTFC.UpdateFeeHeadsTally(ObjTFM, ref Message);

            if (res == -99)
            {

                objCommon.DisplayMessage(upDetails, "Exception Occure", this.Page);

                return;

            }
            else if (res == 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Already Exsits", this.Page);

                return;

            }
            else if (res <= 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Not Save", this.Page);
                return;
            }
            else if (res > 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Save Successfully", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btncancelFeehead_Click(object sender, EventArgs e)
    {
        try
        {
            ddlCashBook.SelectedIndex = 0;
            pnlFeeHeads.Visible = false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }




    #endregion FeeHeads Configuration






    protected void ddlReceiptBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlServer.Items.Clear();
        ddlServer.Items.Insert(0, new ListItem("Please Select", "0"));
        if (ddlReceiptBook.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlServer, "TallyConfig", "TallyConfigId", "ServerName+':'+Convert(varchar(10),PortNumber)ServerName", "TallyConfigId > 0 AND IsActive = 1 AND EndTime = '9999-12-31'", "TallyConfigId");
        }

    }
}

