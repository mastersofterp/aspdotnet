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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
using IITMS.NITPRM;
public partial class STORES_Transactions_Quotation_Str_Technical_Bid : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Vendor_Tender_Entry_Controller objVenTnd = new Str_Vendor_Tender_Entry_Controller();
    Str_Vendor_Quotation_Entry_Controller objVQtEntry = new Str_Vendor_Quotation_Entry_Controller();

    GridView gvBudgetReport = new GridView();
    ArrayList arrlist = new ArrayList();
    ArrayList alPno = new ArrayList();

    int Reporttvno;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Tabs.ActiveTabIndex = 0;
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


                this.bindTendor();
                objCommon.FillDropDownList(ddlTender, "store_tender", "TNO", "TDRNO", "", "TNO DESC");
                objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
                this.panelshow();
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    void bindTendor()
    {
        lstTendor.Items.Clear();
        lstTendor.Items.Insert(0, new ListItem("Please Select", "0"));
        DataSet ds = objVenTnd.GetAllTendor();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstTendor.DataSource = ds.Tables[0];
            lstTendor.DataValueField = "TNO";
            lstTendor.DataTextField = "TDRNO";
            lstTendor.DataBind();
        }

    }
    protected void lstTendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearItem();
        if (ViewState["action"].Equals("add"))
        {
            //this.filldetails();
            objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
            // objCommon.FillDropDownList(ddlVendor, "store_tender_party", "tvno", "vendorname", "tenderno=" + Convert.ToInt32(lstTendor.SelectedValue) + "", "tvno");
            this.filldetails();
            this.panelshow();
            //objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNO");
        }
        else
        {
            // objCommon.FillDropDownList(ddlVendor, "store_tender_party", "tvno", "vendorname", "tenderno=" + Convert.ToInt32(lstTendor.SelectedValue) + "", "tvno");
            objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
            this.filldetails();
            this.panelshow();

        }
        BindItemList(Convert.ToInt32(lstTendor.SelectedValue));
    }

    // added for get tender items
    private void BindItemList(int tenderno)
    {
        DataSet dsItems = objVenTnd.GetTenderBidItemByTenderNo(tenderno);
        grdItemList.DataSource = dsItems.Tables[0];
        string[] DataKeyNames = { "ITEM_NO" };
        grdItemList.DataKeyNames = DataKeyNames;
        grdItemList.DataBind();
    }


    protected void btnsave_Click(object sender, EventArgs e)
    {
        STR_TENDER_VENDOR objvendor = new STR_TENDER_VENDOR();
        try
        {
            if (Convert.ToInt32(lstTendor.SelectedValue) > 0)
            {
                if (ViewState["action"].Equals("add"))
                {
                    int Count = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_PARTY", "count(*)", "TENDERNO=" + Convert.ToInt32(lstTendor.SelectedValue) + " AND PNO=" + Convert.ToInt32(ddlVendor.SelectedValue)));
                    if (Count > 0)
                    {
                        DisplayMessage("Tech.Specification For This Vendor Is Already Done.");
                        return;
                    }
                    int pno = Convert.ToInt32(ddlVendor.SelectedValue);
                    objvendor.NAME = txtvnd.Text;
                    objvendor.ADDRESS = txtvndadd.Text;
                    objvendor.CONTACT = txtContact.Text;
                    objvendor.REMARK = txtremark.Text;
                    objvendor.TENDERNO = Convert.ToInt32(lstTendor.SelectedValue);
                    objvendor.COLLEGE_CODE = (Session["colcode"].ToString());
                    objvendor.VENDORCODE = txtVCode.Text;
                    objvendor.STATUS = 'N';
                    objvendor.EMAIL = txtEmail.Text;
                    objvendor.CST = txtcst.Text;
                    objvendor.BST = txtbst.Text;
                    objvendor.OTHERTECH = txtSpec.Text;
                    string specdoc = Convert.ToString(objCommon.ReadTextFile(fldtechspech));
                    if (specdoc == "-2" || specdoc == "-3")
                    {
                        if (specdoc == "-2")
                            DisplayMessage("Please Uplaod Text File only");
                        else
                            DisplayMessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");
                    }
                    else
                    {
                        if (specdoc == "-1")
                            objvendor.TECHSPEC = null;
                        else
                            objvendor.TECHSPEC = specdoc;
                    }
                    objVenTnd.AddParty(objvendor, pno);
                    SaveTenderItems();
                    //DisplayMessage("Vendor Technical Entry Saved Succesfully");
                }
                else
                {
                    updateparty((Session["colcode"].ToString()));
                    DisplayMessage("Vendor Technical Entry Updated Succesfully");
                }
            }
            else
            {
                DisplayMessage("Please Select Vendor Name.");
            }
            ClearItem();

        }

        catch (Exception ex)
        {
            objCommon.ShowError(Page, ex.Message);
        }
    }
    //added for save tender items
    public void SaveTenderItems()
    {
        STR_TENDER_ITEM_ENTRY objvientry = new STR_TENDER_ITEM_ENTRY();
        STR_TENDER_VENDOR objvendor = new STR_TENDER_VENDOR();
        try
        {
            if (ViewState["action"].Equals("add"))
            {
                int TRVNO = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_PARTY", "MAX(TVNO)", "").ToString());
                objvendor.TVNO = TRVNO;
                //objvendor.TVNO = Convert.ToInt32(ddlVendor.SelectedValue);
                for (int i = 0; i < grdItemList.Rows.Count; i++)
                {
                    objvientry.ITEM_NO = (int)grdItemList.DataKeys[i].Value;
                    Label lblUnit = (Label)grdItemList.Rows[i].FindControl("lblUnit");
                    DropDownList ddlStatus = (DropDownList)grdItemList.Rows[i].FindControl("ddlStatus");
                    //string status = txtstatus.Text;
                    objvientry.TECHSPEC = ddlStatus.SelectedValue;
                    TextBox txtPerticular = (TextBox)grdItemList.Rows[i].FindControl("txtPerticular");
                    objvientry.ITEMDETAIL = txtPerticular.Text;


                    objvientry.TENDERNO = objCommon.LookUp("STORE_TENDER", "TENDERNO", "TNO = " + Convert.ToInt32(lstTendor.SelectedValue)).ToString();
                    objvientry.TINO = Convert.ToInt32(lstTendor.SelectedValue);
                    objvientry.COLLEGE_CODE = (Session["colcode"].ToString());
                    objvientry.TVNO = TRVNO;//Convert.ToInt32(ddlVendor.SelectedValue);
                    string colcode = (Session["colcode"].ToString());
                    int ret = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_ITEM", "count(*)", "item_no =" + objvientry.ITEM_NO + "and TENDERNO='" + objvientry.TENDERNO + "' and TVNO=" + objvientry.TVNO));
                    if (ret == 0)
                    {
                        objVenTnd.SavePartyItemsTechBidding(objvientry, colcode);
                    }
                    else
                    {
                        // objVenTnd.UpdatePartyItemsEntry(objvientry, colcode);

                    }
                }


                DisplayMessage("Vendor Entry Saved Successfully");

            }

            else
            {
                this.updatePartyEntry(Session["colcode"].ToString());
                // this.SavePartyFields(Session["colcode"].ToString(), Convert.ToInt32(lstVendor.SelectedValue));
                DisplayMessage("Vendor Entry Updated  Successfully");
            }
            ClearItem();

        }

        catch (Exception ex)
        {
            objCommon.ShowError(Page, ex.Message);
        }
    }
    void updatePartyEntry(string colcode)
    {
        STR_TENDER_ITEM_ENTRY objPIEntry = new STR_TENDER_ITEM_ENTRY();
        foreach (GridViewRow row in grdItemList.Rows)
        {
            int TINO = (int)grdItemList.DataKeys[row.DataItemIndex].Value;

            TextBox txtPerticular = (TextBox)row.FindControl("txtPerticular");
            objPIEntry.ITEMDETAIL = txtPerticular.Text;


            string specdoc = Convert.ToString(objCommon.ReadTextFile(fldtechspech));

            objPIEntry.TINO = TINO;
            objVenTnd.UpdatePartyItemsEntry(objPIEntry, colcode);
        }


    }
    protected void btnMod_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "edit";
        Tabs.ActiveTabIndex = 0;
        bindTendor();
        this.panelshow();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearItem();
        ViewState["action"] = "add";
        bindTendor();
    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVendoDetails(Convert.ToInt32(ddlVendor.SelectedValue));
        //txtContact.Enabled = true;
        //txtremark.Enabled = true;
        //txtvnd.Enabled = true;
        //txtvndadd.Enabled = true;
        //txtbst.Enabled = true;
        //txtcst.Enabled = true;
        //txtEmail.Enabled = true;
        //txtVCode.Enabled = true;
        Tabs.ActiveTabIndex = 1;
    }
    protected void imgFieldNext_Click(object sender, ImageClickEventArgs e)
    {
        Tabs.ActiveTabIndex = 1;
    }
    protected void ClearItem()
    {

        Tabs.ActiveTabIndex = 0;
        txtvndadd.Text = string.Empty;
        txtvnd.Text = string.Empty;
        txtremark.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtbst.Text = string.Empty;
        txtcst.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtSpec.Text = string.Empty;
        txtVCode.Text = string.Empty;
        lblTotal.Text = string.Empty;
        lblTREFNO.Text = string.Empty;
        lblTOpen.Text = string.Empty;
        lblTAMT.Text = string.Empty;
        lblSalesTax.Text = string.Empty;
        lblEMD.Text = string.Empty;
        ddlVendor.SelectedIndex = 0;
        this.panelhide();
        grdItemList.DataSource = null;
        grdItemList.DataBind();
        //for (int i = 0; i < grdItemList.Rows.Count; i++)
        //{           
        //    TextBox txtPerticular = (TextBox)grdItemList.Rows[i].FindControl("txtPerticular");
        //    txtPerticular.Text = string.Empty;
        //}
    }

    private void filldetails()
    {
        DataSet ds = objVenTnd.GetTenderDetailsByTenderNo(Convert.ToInt32(lstTendor.SelectedValue));

        lblEMD.Text = ds.Tables[0].Rows[0]["EMD"].ToString();
        lblSalesTax.Text = ds.Tables[0].Rows[0]["STAX"].ToString();
        lblTAMT.Text = ds.Tables[0].Rows[0]["TDAMT"].ToString();
        lblTOpen.Text = ds.Tables[0].Rows[0]["TDODATE"].ToString();
        lblTotal.Text = ds.Tables[0].Rows[0]["TOTAMT"].ToString();
        lblTREFNO.Text = ds.Tables[0].Rows[0]["TENDERNO"].ToString();



    }

    void panelhide()
    {
        //pnlmodify.Visible = true;
        txtvndadd.Enabled = true;
        txtvnd.Enabled = true;
        txtremark.Enabled = true;
        txtContact.Enabled = true;
        txtVCode.Enabled = true;
        txtEmail.Enabled = true;
        txtcst.Enabled = true;
        txtbst.Enabled = true;

    }
    void panelshow()
    {
        //pnlmodify.Visible = true;
        txtvndadd.Enabled = false;
        txtvnd.Enabled = false;
        txtremark.Enabled = false;
        txtContact.Enabled = false;
        txtVCode.Enabled = false;
        txtEmail.Enabled = false;
        txtcst.Enabled = false;
        txtbst.Enabled = false;
    }

    public void DisplayMessage(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    void updateparty(string colcode)
    {
        STR_TENDER_VENDOR objvendor = new STR_TENDER_VENDOR();
        objvendor.TVNO = Convert.ToInt32(ddlVendor.SelectedValue);
        objvendor.NAME = txtvnd.Text;
        objvendor.ADDRESS = txtvndadd.Text;
        objvendor.CONTACT = txtContact.Text;
        objvendor.REMARK = txtremark.Text;
        objvendor.TENDERNO = Convert.ToInt32(lstTendor.SelectedValue);
        objvendor.COLLEGE_CODE = (Session["colcode"].ToString());
        objvendor.VENDORCODE = txtVCode.Text;
        objvendor.STATUS = 'N';
        objvendor.EMAIL = txtEmail.Text;
        objvendor.CST = txtcst.Text;
        objvendor.BST = txtbst.Text;
        objvendor.OTHERTECH = txtSpec.Text;
        string specdoc = Convert.ToString(objCommon.ReadTextFile(fldtechspech));
        if (specdoc == "-2" || specdoc == "-3")
        {
            if (specdoc == "-2")
                DisplayMessage("Please Uplaod Text File only");
            else
                DisplayMessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");
        }
        else
        {
            if (specdoc == "-1")
                objvendor.TECHSPEC = null;
            else
                objvendor.TECHSPEC = specdoc;
        }
        objVenTnd.UpdateParty(objvendor, Convert.ToInt32(ddlVendor.SelectedValue));

    }
    void bindVendoDetails(int tvno)
    {
        DataSet ds = new DataSet();
        int pno = Convert.ToInt32(ddlVendor.SelectedValue);
        ds = objVenTnd.getPartyForTender(pno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtVCode.Text = ds.Tables[0].Rows[0]["PCODE"].ToString();

            txtcst.Text = ds.Tables[0].Rows[0]["CST"].ToString();
            txtbst.Text = ds.Tables[0].Rows[0]["BST"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
            txtvnd.Text = ds.Tables[0].Rows[0]["PNAME"].ToString();
            txtContact.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
            txtvndadd.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
            txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
        }
        //else
        //{

        //    txtContact.Text = string.Empty;
        //    txtremark.Text = string.Empty;
        //    txtvnd.Text = string.Empty;
        //    txtvndadd.Text = string.Empty;
        //    txtVCode.Text = string.Empty;
        //    txtEmail.Text = string.Empty;
        //    txtcst.Text = string.Empty;
        //    txtbst.Text = string.Empty;
        //}
    }
    protected void ddlTender_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        ds = objVenTnd.getPartyDetails(Convert.ToInt32(ddlTender.SelectedValue));
        grdVendor.DataSource = ds.Tables[0];
        grdVendor.DataBind();
        for (int i = 0; i < grdVendor.Rows.Count; i++)
        {
            DataRow dr = ds.Tables[0].Rows[i];
            char status = Convert.ToChar(dr["STATUS"].ToString());
            CheckBox chkVendor = (CheckBox)grdVendor.Rows[i].FindControl("chkVendor");
            if (status == 'Y')
            {
                chkVendor.Checked = true;
            }
        }
        //objCommon.ReportPopUp(btnReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "TechnicalReccomend.rpt&param=@P_TENDERNO=" + Convert.ToInt32(ddlTender.SelectedValue) , "UAIMS");


        Tabs.ActiveTabIndex = 2;


    }
    protected void btnRSave_Click(object sender, EventArgs e)
    {

        STR_TENDER_VENDOR objvendor = new STR_TENDER_VENDOR();
        try
        {
            int count1 = 0;
            int count2 = 0;
            if (grdVendor.Rows.Count > 0)
            {
                for (int i = 0; i < grdVendor.Rows.Count; i++)
                {
                    CheckBox chkVendor = (CheckBox)grdVendor.Rows[i].FindControl("chkVendor");
                    if (chkVendor.Checked)
                    {
                        int tvno = Convert.ToInt32(chkVendor.ToolTip);
                        objVenTnd.ReccomandParty(Convert.ToInt32(ddlTender.SelectedValue), tvno);
                        DisplayMessage("Vendor's Recommended Sucessfully");
                        count1 = count1 + 1;
                    }
                    else
                    {
                        int tvno = Convert.ToInt32(chkVendor.ToolTip);
                        objVenTnd.DReccomandParty(Convert.ToInt32(ddlTender.SelectedValue), tvno);
                        count2 = count2 + 1;
                    }

                }
            }
            else
            {
                DisplayMessage("No Vendor To Reccommend.");
            }
            if (count1 == 0 && count2 == 0)
            {
                DisplayMessage("Select Vendor For Recommendation First");
            }
            //if (ViewState["action"].Equals("edit"))
            //{
            //    objCommon.FillDropDownList(ddlTender, "store_tender", "TNO", "TDRNO", " flag<>'t'or flag is  null ", "TNO");

            //}
            ViewState["action"] = "add";
            grdVendor.DataSource = null;
            grdVendor.DataBind();

        }

        catch (Exception ex)
        {
            objCommon.ShowError(Page, ex.Message);
        }

    }
    protected void btnRCancel_Click(object sender, EventArgs e)
    {


        ddlTender.SelectedIndex = 0;
        grdVendor.DataSource = null;
        grdVendor.DataBind();
        Tabs.TabIndex = 2;
        //objCommon.FillDropDownList(ddlTender, "store_tender", "TNO", "TDRNO", " flag<>'t'or flag is  null ", "TNO");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            //url += "&param=@username=" + Session["userfullname"].ToString() + "," + "@P_TENDERNO=" + lstTender.SelectedValue  + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_TENDERNO=" + ddlTender.SelectedValue;



            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void grdVendor_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Reporttvno = Convert.ToInt32(grdVendor.DataKeys[e.NewSelectedIndex].Value.ToString());
        //objCommon.ReportPopUp(grdVendor, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "TenderSpecificationDoc.rpt&param=@P_TVNO=" + Reporttvno, "UAIMS");
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=Tendor Specification";
        url += "&path=~,Reports,STORES,TenderSpecificationDoc.rpt";
        //url += "&param=@username=" + Session["userfullname"].ToString() + "," + "@P_TENDERNO=" + lstTender.SelectedValue  + "," + "@P_TVNO=" + Session["colcode"].ToString();
        url += "&param=@P_TVNO=" + Reporttvno;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void grdVendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = new DataSet();
        grdVendor.PageIndex = e.NewPageIndex;
        ds = objVenTnd.getPartyDetails(Convert.ToInt32(ddlTender.SelectedValue));
        grdVendor.DataSource = ds.Tables[0];
        grdVendor.DataBind();
    }


    protected void btnReport1_Click(object sender, EventArgs e)
    {
        if (ddlTender.SelectedIndex == 0)
        {
            DisplayMessage("Please Select Tender From List");
            return;
        }
        ShowReport("Tender_Report", "TechnicalReccomend.rpt");
    }
    protected void btnCmpRpt_Click(object sender, EventArgs e)
    {
        if (ddlTender.SelectedIndex > 0)
        {
            DataTable VendorDt = objVenTnd.GetVendorForTechCmpRpt(Convert.ToInt32(ddlTender.SelectedValue)).Tables[0];
            if (VendorDt.Rows.Count == 0)
            {
                DisplayMessage("Please Save All The Items For At Least One Vendor");
                return;
            }
            //Added by vijay andoju For getting Proper Structure for Showing Report
            DataTable dtBind = Datateble(VendorDt);

            //gvBudgetReport.RowDataBound += new GridViewRowEventHandler(gvBudgetReport_RowDataBound);

            gvBudgetReport.DataSource = dtBind;
            gvBudgetReport.DataBind();

            int ColumnCount = dtBind.Columns.Count;
            if (ColumnCount < 6)
                ColumnCount = 6;

            int rowCount = dtBind.Rows.Count;

            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=ComparativeStatement.xls";

            AddHeader(ColumnCount, rowCount);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", attachment);
            Response.AppendHeader("Refresh", ".5; BudgetReportNew.aspx");
            Response.Charset = "";
            Response.ContentType = "application/" + ContentType;
            StringWriter sw1 =

                new StringWriter();
            HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
            gvBudgetReport.HeaderRow.Visible = false;
            gvBudgetReport.RenderControl(htw1);
            Response.Output.Write(sw1.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            DisplayMessage("Please Selct Tender From List");
            return;
            //Tabs.ActiveTabIndex = 3;
        }
    }

    protected void gvBudgetReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvBudgetReport.HeaderRow.Cells.Count; i++)
                {
                    string Header = gvBudgetReport.HeaderRow.Cells[i].Text;
                    if (Header.ToString().Contains("/") || Header.ToString().Contains("Price/Unit") || Header.ToString().Contains("Value"))
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;")
                        {
                            //e.Row.Cells[i].Text = String.Format("{0:N2}", e.Row.Cells[i].Text == "&nbsp;" ? 0.00 : Convert.ToDouble(e.Row.Cells[i].Text));
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text;
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "";
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

    private void AddHeader(int colspan, int Row)
    {
        int rows = Row + 9;

        string[] Col = { "Specification" };

        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell.Text = "Maulana Abul Kalam Azad University of Technology, West Bengal";
        HeaderCell.ColumnSpan = colspan;
        HeaderCell.Font.Size = 14;
        HeaderCell.Font.Bold = true;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.ForeColor = System.Drawing.Color.Black;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        gvBudgetReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "SIMHAT, HARINGHATA, NADIA, WEST  BENGAL, INDIA - 741249.";
        HeaderCell1.ColumnSpan = colspan;
        HeaderCell1.Font.Size = 9;
        HeaderCell1.Font.Bold = true;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        gvBudgetReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = "";//For FT/GN/20/08/01.04.19
        HeaderCell2.ColumnSpan = colspan;
        HeaderCell2.Font.Size = 10;
        HeaderCell2.Font.Bold = true;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        gvBudgetReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "TECHNICAL COMPARATIVE STATEMENT";
        HeaderCell3.ColumnSpan = colspan;
        HeaderCell3.Font.Size = 14;
        HeaderCell3.Font.Bold = true;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        gvBudgetReport.Controls[0].Controls.AddAt(3, HeaderGridRow3);

        GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell4 = new TableCell();
        HeaderCell4 = new TableCell();
        HeaderCell4.Text = "Guidelines for preparation of Comparative Statement";
        HeaderCell4.ColumnSpan = colspan;
        HeaderCell4.Font.Size = 9;
        HeaderCell4.Font.Bold = true;
        HeaderCell4.BackColor = System.Drawing.Color.White;
        HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow4.Cells.Add(HeaderCell4);
        gvBudgetReport.Controls[0].Controls.AddAt(4, HeaderGridRow4);

        GridViewRow HeaderGridRow5 = new GridViewRow(5, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell5 = new TableCell();
        HeaderCell5 = new TableCell();
        HeaderCell5.Text = "* The final approval form should be prepared supplier wise";
        HeaderCell5.ColumnSpan = colspan;
        HeaderCell5.Font.Size = 10;
        HeaderCell5.Font.Bold = true;
        HeaderCell5.BackColor = System.Drawing.Color.White;
        HeaderCell5.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow5.Cells.Add(HeaderCell5);
        gvBudgetReport.Controls[0].Controls.AddAt(5, HeaderGridRow5);

        #region Headder

        GridViewRow HeaderGridRow6 = new GridViewRow(6, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell Header6Cell = new TableCell();
        Header6Cell.Text = "Sr.No.";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        Header6Cell = new TableCell();
        Header6Cell.Text = "Description";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        TableCell HeaderN = new TableCell();

        for (int i = 0; i < arrlist.Count; i++)
        {
            for (int j = i; j == i; j++)
            {
                HeaderN = new TableCell();
                HeaderN.Text = arrlist[j].ToString();
                HeaderN.ColumnSpan = 1;
                HeaderN.RowSpan = 1;
                HeaderN.Font.Size = 10;
                HeaderN.Font.Bold = true;
                HeaderN.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow6.Cells.Add(HeaderN);
                gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

            }
        }

        GridViewRow HeaderGridRow7 = new GridViewRow(7, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell Header7Cell = new TableCell();

        for (int i = 1; i <= arrlist.Count; i++)
        {
            for (int j = 0; j < Col.Length; j++)
            {
                TableCell HeaderN1 = new TableCell();
                HeaderN1.Text = Col[j].ToString();
                HeaderN1.ColumnSpan = 1;
                HeaderN1.RowSpan = 1;
                HeaderN1.Font.Size = 10;
                HeaderN1.Font.Bold = true;
                HeaderN1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow7.Cells.Add(HeaderN1);
                gvBudgetReport.Controls[0].Controls.AddAt(7, HeaderGridRow7);
            }
        }

        #endregion

        #region SignatureHead
        // string[] FooterHead = { "Prepared by", "Head of the Department", "Accounts Section", "Principal", "Secretary", "Treasurer" };
        string[] FooterHead = { "Prepared by" };
        //GridViewRow HeaderGridRow12 = new GridViewRow(12, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //for (int i = 0; i < 6; i++)
        //{
        //    TableCell HeaderCell11 = new TableCell();
        //    HeaderCell11.Text = "";
        //    HeaderCell11.ColumnSpan = 1;
        //    HeaderCell11.RowSpan = 1;
        //    HeaderCell11.Font.Size = 10;
        //    HeaderCell11.Font.Bold = true;
        //    HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow12.Cells.Add(HeaderCell11);
        //    gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow12);
        //}
        GridViewRow HeaderGridRow11 = new GridViewRow(13, 0, DataControlRowType.Header, DataControlRowState.Insert);
        for (int i = 0; i < FooterHead.Length; i++)
        {
            TableCell HeaderCell11 = new TableCell();
            HeaderCell11.Text = FooterHead[i].ToString();
            HeaderCell11.ColumnSpan = 2;
            HeaderCell11.RowSpan = 3;
            HeaderCell11.Font.Size = 10;
            HeaderCell11.Font.Bold = true;
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow11.Cells.Add(HeaderCell11);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + (1), HeaderGridRow11);
        }
        #endregion

        gvBudgetReport.FooterStyle.Font.Bold = true;
        gvBudgetReport.FooterStyle.Font.Size = 19;
    }
    private DataTable Datateble(DataTable dtvendor)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Sl.No", typeof(string));
        dt.Columns.Add("Description", typeof(string));

        //---------------------------------------------

        //Creating Column names for Vendor wise
        for (int i = 0; i < dtvendor.Rows.Count; i++)
        {
            dt.Columns.Add("Specification" + i, typeof(string));
            arrlist.Add(dtvendor.Rows[i]["PNAME"].ToString());
            alPno.Add(dtvendor.Rows[i]["PNO"].ToString());
        }
        //---------------------------------------------------


        //Inserting the Itemname Qty in table

        DataTable QuotItemDt = objVenTnd.GetItemsByTendernoForTechCmpRpt(Convert.ToInt32(ddlTender.SelectedValue)).Tables[0];
        for (int j = 0; j < QuotItemDt.Rows.Count; j++)
        {
            DataRow Row1;
            Row1 = dt.NewRow();
            Row1["Sl.No"] = j + 1;
            Row1["Description"] = QuotItemDt.Rows[j]["Item_name"].ToString();
            // Row1["Specification"] = QuotItemDt.Rows[j]["TECH_SPECIFICATION"].ToString();            
            dt.Rows.Add(Row1);

            for (int k = 0; k < dtvendor.Rows.Count; k++)
            {
                DataTable RateByVendorandItemDt = objVenTnd.GetItemsForTenderVendorTechCmpRpt(Convert.ToInt32(ddlTender.SelectedValue), Convert.ToInt32(dtvendor.Rows[k]["PNO"].ToString()), Convert.ToInt32(QuotItemDt.Rows[j]["ITEM_NO"].ToString())).Tables[0];
                if (RateByVendorandItemDt.Rows.Count > 0)
                {
                    dt.Rows[j]["Specification" + k] = RateByVendorandItemDt.Rows[0]["Tech_Specification"].ToString();
                    dt.Rows[j].AcceptChanges();
                }
                else
                {
                    dt.Rows[j]["Specification" + k] = "";
                    dt.Rows[j].AcceptChanges();
                }
            }

        }
        //--------------------------------------------------------------------------------------------------
        int RoCount1 = dt.Rows.Count - 1;
        return dt;
    }

}
