//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Quotation_Str_Tender_Entry_Form.aspx                                                  
// CREATION DATE : 03-Oct-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
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
using IITMS.NITPRM;
public partial class Stores_Transactions_Quotation_Str_Tender_Entry_Form : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Quotation_Tender_Controller objQuotTender = new Str_Quotation_Tender_Controller();
    VendorController objvendor = new VendorController();
    NewsPaperController objNews = new NewsPaperController();
    DataSet dsFields;
    DataSet dsNews;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";
      

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        // PostBackTrigger triger = new PostBackTrigger();
        // triger.EventName = "Click";
        // triger.ControlID = butSaveTender.UniqueID.ToString();
        // triger. = UpdatePanel1;
        //UpdatePanel1.Triggers.Add(triger);
        ScriptManager scrmn = ScriptManager.GetCurrent(this);
        scrmn.RegisterPostBackControl(butSaveTender);
        // PostBackTrigger triger = new PostBackTrigger();
        // triger.EventName = "Click";
        // triger.ControlID = butSaveTender.UniqueID.ToString();
        // triger. = UpdatePanel1;
        //UpdatePanel1.Triggers.Add(triger);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtSalesTax.Text = "0";
            txtEMDofRs.Text = "0";
            txtPerSecurity.Text = "0";
            ddltender.Visible = false;
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // lbluser.Text = Session["userfullname"].ToString();
                //lblDept.Text = Session["strdeptname"].ToString();
                this.BindListView_IndentDetails('A');
                pnlFields.Visible = false;
                //Tabs.ActiveTabIndex = 0;
                this.FillBudget();
                this.bindTender();
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

    private void BindListView_IndentDetails(char Flag)
    {
        DataSet ds = null;
        try
        {

            ds = objQuotTender.GetIndentForQuotationTender(Convert.ToInt32(Session["strdeptcode"].ToString()), 'T', Flag);


            if (ds.Tables[0].Rows.Count > 0)
            {
                //butIndent.Visible = true;
                // divEnterTenderDetails.Visible = true;
            }
            else
            {
                //butIndent.Visible =false;
                // divEnterTenderDetails.Visible = false;
            }

            grdIndList.DataSource = ds;
            grdIndList.DataBind();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Tender_Entry_Form.BindListView_DeptRequestedList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView_Fields()
    {
        DataSet ds = null;
        try
        {
            ds = objQuotTender.GetFieldsQuotationTender();
            lvIndiaCalculative.DataSource = ds.Tables[1];
            lvIndiaInformative.DataSource = ds.Tables[0];
            lvForeignCalculative.DataSource = ds.Tables[2];
            lvForeignInformative.DataSource = ds.Tables[3];
            lvIndiaInformative.DataBind();
            lvIndiaCalculative.DataBind();
            lvForeignCalculative.DataBind();
            lvForeignInformative.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Tender_Entry_Form.BindListView_Fields() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView_Newspaper()
    {
        DataSet ds = null;
        try
        {
            ds = objNews.GetAllNewPaper();
            lvNewspaper.DataSource = ds;
            lvNewspaper.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Quotation_Entry_Form.BindListView_VendorCategory() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckMainStoreUser()
    {
        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
            return true;
        else
            return false;
    }
    string INDNO = string.Empty;
    protected void butIndent_Click(object sender, EventArgs e)
    {
        
        //string INDNO = string.Empty;
        DataSet ds = null;
        foreach (GridViewRow row in grdIndList.Rows)
        {
            RadioButton chk = row.FindControl("CheckIndent") as RadioButton;
            if (chk.Checked)
            {
                INDNO = INDNO + chk.ToolTip;

            }
        }
        this.InitilizeFieldGrid();
        ds = objQuotTender.GetItemsForQuotationTender(INDNO);
        lvitems.DataSource = ds;
        lvitems.DataBind();
        FillBudget();

        pnlFields.Visible = true;
        pnlNewsPaper.Visible = true;
        this.BindListView_Fields();
        this.BindListView_Newspaper();
        this.GenerateTenderNo(INDNO);
        //Tabs.ActiveTabIndex = 1;
        pnlitems.Visible = true;

    }

    private void FillBudget()
    {
        try
        {
            objCommon.FillDropDownList(ddlBudgetHead, "STORE_BUDGET_HEAD B,STORE_BUDGETHEAD_Alloction A", "A.BHALNO", "B.BHNAME", "A.BHNO=B.BHNO", "BHNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillBudget() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void butSaveTender_Click(object sender, EventArgs e)
    {
        double PSAmt = 0;
        Str_Quotation_Tender objQT = new Str_Quotation_Tender();
        try
        {
            if (ViewState["action"].Equals("edit") && ddltender.SelectedIndex == 0)
            {
                DisplayMessage("Please Select Tender Number.");
                return;
            }
            DataSet dtNews = (DataSet)ViewState["dsnews"];
            if (dtNews == null || dtNews.Tables[0].Rows.Count == 0)
            {
                DisplayMessage("Please Add At Least One News Paper");
                return;
            }
            for (int i = 0; i < grdIndList.Rows.Count; i++)
            {
                RadioButton rd = (RadioButton)grdIndList.Rows[i].FindControl("CheckIndent");
                if (rd.Checked)
                {
                    objQT.INDENTNO = rd.ToolTip;
                }
            }
            if (ViewState["action"].Equals("add"))
            {
                objQT.TENDERNO = txtTenderNumber.Text;
            }
            else
            {               
                objQT.TENDERNO = ddltender.SelectedItem.Text;
            }
            objQT.TDRNO = txtReferenceNumber.Text;
            objQT.LDATESALE = Convert.ToDateTime(txtLasteDateForSaleTime.Text);
            objQT.LTIMESALE = Convert.ToDateTime(txtLasteDateTime.Text);
            objQT.SUBMITDATE = Convert.ToDateTime(txtLastDateForSubmission.Text);
            objQT.SUBMITTIME = Convert.ToDateTime(txtLastDateForSubmissionTime.Text);
            objQT.TDODATE = Convert.ToDateTime(txtTendorOpeningDate.Text);
            objQT.TDOTIME = Convert.ToDateTime(txtTendorOpeningDateTime.Text);
            objQT.SDATE = Convert.ToDateTime(txtSendingDate.Text);
            objQT.EMD = txtEMDofRs.Text == "" ? 0 : Convert.ToDouble(txtEMDofRs.Text);
            objQT.STAX = txtSalesTax.Text == "" ? 0 : Convert.ToDecimal(txtSalesTax.Text);
            objQT.TDAMT = txtTenderAmt.Text == "" ? 0 : Convert.ToDouble(txtTenderAmt.Text);
            objQT.TOTAMT = objQT.EMD + Convert.ToDouble(objQT.STAX) + objQT.TDAMT;
            objQT.SUBJECT = txtSubject.Text;
            objQT.BHALNO = Convert.ToInt32(ddlBudgetHead.SelectedValue);
            PSAmt = Convert.ToDouble(txtPerSecurity.Text);
            objQT.SPECI = txtSpecification.Text;

            //string specdoc = Convert.ToString(objCommon.ReadTextFile(FileUpload3));
            //if (specdoc == "-2" || specdoc == "-3")
            //{

            //    if (Convert.ToString(objCommon.ReadTextFile(FileUpload3)) == "-2")
            //        DisplayMessage("Please Uplaod Text File only");
            //    else
            //        DisplayMessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");


            //}
            //else
            //{
            //    if (ViewState["action"].Equals("add"))
            //    {
            //        if (specdoc == "-1")
            //        { objQT.SPECI = null; }
            //        else
            //        {
            //            objQT.SPECI = specdoc;
            //        }
            //    }
            //    else
            //    {
            //        objQT.SPECI = txtSpecification.Text;
            //    }
            //}

            if (ViewState["action"].Equals("add"))
            {
                if (objQT.INDENTNO == "" || objQT.INDENTNO == null)
                {
                    DisplayMessage("Please Select Indent For Tender");
                }
                else
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_TENDER", " count(*)", "TENDERNO='" + objQT.TENDERNO + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)SaveTender(objQT, PSAmt, Session["colcode"].ToString());
                        // SaveNewsForTender(objQT.TENDERNO, Session["colcode"].ToString());
                        //cs = (CustomStatus)SaveFieldsForTender(objQT.TENDERNO, Session["colcode"].ToString());
                        DisplayMessage("Tender Saved Successfully");

                        ViewState["action"] = "add";
                        ClearItems();
                    }
                    else
                    {
                        DisplayMessage("Tender Number Already Exist");
                    }
                }
            }
            else
            {
                CustomStatus cs = (CustomStatus)objQuotTender.UpdateTenderEntry(objQT, PSAmt, Session["colcode"].ToString());
               // cs = (CustomStatus)SaveFieldsForTender(objQT.TENDERNO, Session["colcode"].ToString());
                cs = (CustomStatus)SaveNewsForTender(objQT.TENDERNO, Session["colcode"].ToString());
                DisplayMessage("Tender Updated Successfully");
                //txtSpecification.Visible = false;
               // FileUpload3.Visible = true;
                ClearItems();
                ViewState["action"] = "add";
                ClearItems();
            }
        }



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Str_Tender_Entry_Form.save Tender.butSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        this.bindTender();

    }

  

    int SaveTender(Str_Quotation_Tender objQt, double PSAmt, string colcode)
    {
        dsNews = (DataSet)ViewState["dsnews"];
        int retst = 0;
        if (dsNews!= null && dsNews.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsNews.Tables[0].Rows.Count; i++)
            {

                retst = objQuotTender.AddTender(objQt, PSAmt, Convert.ToInt32(dsNews.Tables[0].Rows[i]["NPNO"].ToString()), colcode);
            }
        }
        else
        {
            retst = objQuotTender.AddTender(objQt, PSAmt, 0, colcode);
        }
        ViewState["dsnews"] = null;
        return retst;
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "edit";
        hdnAction.Value = "1";
        // this.FillBudget();
        objCommon.FillDropDownList(ddltender, "STORE_TENDER", "TNO", "TENDERNO", "ISLOCK!=1", "TNO DESC");
        txtTenderNumber.Visible = false;
        ddltender.Visible = true;
        FillBudget();
        BindListView_Fields();
        BindListView_Newspaper();
        pnlFields.Visible = true;
        pnlNewsPaper.Visible = true;
        butIndent.Visible = false;
        //txtReferenceNumber.Enabled = true;
       // txtSpecification.Visible = true;
       // FileUpload3.Visible = false;

    }

    protected void ddltender_SelectedIndexChanged(object sender, EventArgs e)
    {
        //

        ShowTenderDetails(ddltender.SelectedItem.Text);
        grdField.Visible = true;
        dsFields = objQuotTender.GetFieldsByTenderNo(ddltender.SelectedItem.Text);
        Session["ds"] = dsFields;
        DataColumn[] primarykeycol = { dsFields.Tables[0].Columns["FNO"] };
        dsFields.Tables[0].PrimaryKey = primarykeycol;
        grdField.DataSource = dsFields.Tables[0];
        grdField.DataBind();
        //
        dsNews = objQuotTender.GetNewsPaperByTenderNo(ddltender.SelectedItem.Text);
        DataColumn[] primNews = { dsNews.Tables[0].Columns["NPNO"] };
        dsNews.Tables[0].PrimaryKey = primNews;
        ViewState["dsnews"] = dsNews;
        grdNews.DataSource = dsNews.Tables[0];
        grdNews.DataBind();
        grdNews.Visible = true;
        hdnAction.Value = "1";

    }

    void ShowTenderDetails(string tenderno)
    {
        DataSet ds = objQuotTender.GetSingleTenderByTenderNo(ddltender.SelectedItem.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtReferenceNumber.Text = ds.Tables[0].Rows[0]["TDRNO"].ToString();
            txtLasteDateForSaleTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LDATESALE"].ToString()).ToShortDateString();
            txtLasteDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LTIMESALE"].ToString()).ToString("hh:mm tt");
            txtLastDateForSubmission.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SUBMITDATE"].ToString()).ToShortDateString();
            txtLastDateForSubmissionTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SUBMITTIME"].ToString()).ToString("hh:mm tt");
            txtTendorOpeningDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["TDODATE"].ToString()).ToShortDateString();
            txtTendorOpeningDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["TDOTIME"].ToString()).ToString("hh:mm tt");
            txtSendingDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SDATE"].ToString()).ToShortDateString();
            string val = ds.Tables[0].Rows[0]["BHALNO"].ToString().Trim();
            ddlBudgetHead.SelectedValue = val;
            BindListView_IndentDetails('M');
            string IndNo = ds.Tables[0].Rows[0]["INDNO"].ToString();
            txtSpecification.Text = ds.Tables[0].Rows[0]["SPECI"].ToString();


            for (int i = 0; i < grdIndList.Rows.Count; i++)
            {
                RadioButton chk = grdIndList.Rows[i].FindControl("CheckIndent") as RadioButton;
                if (chk.ToolTip == IndNo)
                {
                    chk.Checked = true;
                    lvitems.DataSource = objQuotTender.GetItemsForQuotationTender(chk.ToolTip);
                    lvitems.DataBind();

                }
            }
            txtPerSecurity.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["SPAMT"]).ToString();
            txtEMDofRs.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["EMD"]).ToString();
            txtSalesTax.Text = ds.Tables[0].Rows[0]["STAX"].ToString();
            txtTotalAmt.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAMT"]).ToString();
            txtTenderAmt.Text = ds.Tables[0].Rows[0]["TDAMT"].ToString();
            txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
            pnlitems.Visible = true;
            lvitems.Visible = true;
        }
    }
    void ClearItems()
    {
        ddltender.Visible = false;
        txtReferenceNumber.Text = string.Empty;
        txtLasteDateForSaleTime.Text = string.Empty;
        txtLasteDateTime.Text = string.Empty;
        txtLastDateForSubmission.Text = string.Empty;
        txtLastDateForSubmissionTime.Text = string.Empty;
        txtTendorOpeningDate.Text = string.Empty;
        txtTendorOpeningDateTime.Text = string.Empty;
        txtSendingDate.Text = string.Empty;
        txtEMDofRs.Text = string.Empty;
        txtPerSecurity.Text = string.Empty;
        txtSalesTax.Text = string.Empty;
        txtTotalAmt.Text = string.Empty;
        txtTenderAmt.Text = string.Empty;
        txtSubject.Text = string.Empty;
        ddlBudgetHead.SelectedValue = "0";
        txtTenderNumber.Text = string.Empty;
        Session["ds"] = null;
        grdField.DataSource = null;
        grdField.DataBind();
        ViewState["dsnews"] = null;
        grdNews.DataSource = null;
        grdNews.DataBind();
        butIndent.Visible = true;

        pnlFields.Visible = false;
        this.BindListView_IndentDetails('A');
        pnlNewsPaper.Visible = false;
        pnlitems.Visible = false;
        txtTenderNumber.Visible = true;
        ddltender.Visible = false;
        ViewState["action"] = "add";
        hdnAction.Value = "0";
        //txtReferenceNumber.Enabled = false;

        txtSpecification.Text = string.Empty;
       // txtSpecification.Visible = false;
        //FileUpload3.Visible = true;
    }

    protected void grdField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            LinkButton btndel = (LinkButton)e.CommandSource;
            dsFields = (DataSet)Session["ds"];
            //objCommon.ConfirmMessage(null, "Are you Sure To Remove this Field", Page);
            DataRow dr = dsFields.Tables[0].Rows.Find(e.CommandArgument);
            dsFields.Tables[0].Rows.Remove(dr);
            grdField.DataSource = dsFields.Tables[0];
            grdField.DataBind();
           // Tabs.ActiveTabIndex = 2;

        }
    }

    protected void grdField_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnAddFields_Click(object sender, EventArgs e)
    {
        dsFields = (DataSet)Session["ds"];
        //DataTable tbFields=new DataTable() ;

        StoreMasterController objstr = new StoreMasterController();
        // DataSet dsblank = objstr.GetSingleRecordField(0);
        // tbFields = dsblank.Tables[0].Copy();
        #region Search Fields

        for (int i = 0; i < lvIndiaCalculative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvIndiaCalculative.Items[i].FindControl("ChkIndentDetails");
            if (chk.Checked)
            {
                DataSet ds = objstr.GetSingleRecordField(Convert.ToInt32(chk.ToolTip));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!dsFields.Tables[0].Rows.Contains(chk.ToolTip))
                    {
                        dsFields.Tables[0].ImportRow(dr);
                    }
                    else
                    {
                        DisplayMessage("Selected IndiaCalculative Field Already In List");
                    }
                    chk.Checked = false;
                }

            }
        }

        for (int i = 0; i < lvIndiaInformative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvIndiaInformative.Items[i].FindControl("ChkIndentDetails");
            if (chk.Checked)
            {
                DataSet ds = objstr.GetSingleRecordField(Convert.ToInt32(chk.ToolTip));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!dsFields.Tables[0].Rows.Contains(chk.ToolTip))
                    {
                        dsFields.Tables[0].ImportRow(dr);
                        chk.Checked = false;
                    }
                    else
                    {
                        DisplayMessage("Selected IndiaInformative Field Already In List");
                    }
                    chk.Checked = false;
                }
                // objQuotTender.UpdateTenderFieldEntry(objQt.TENDERNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
            }
        }

        for (int i = 0; i < lvForeignCalculative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvForeignCalculative.Items[i].FindControl("ChkIndentDetails");
            if (chk.Checked)
            {
                DataSet ds = objstr.GetSingleRecordField(Convert.ToInt32(chk.ToolTip));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!dsFields.Tables[0].Rows.Contains(chk.ToolTip))
                    {
                        dsFields.Tables[0].ImportRow(dr);
                        chk.Checked = false;
                    }
                    else
                    {
                        DisplayMessage("Selected ForeignCalculative Field Already In List");
                    }
                    chk.Checked = false;
                }
                // objQuotTender.UpdateTenderFieldEntry(objQt.TENDERNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
            }
        }

        for (int i = 0; i < lvForeignInformative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvForeignInformative.Items[i].FindControl("ChkIndentDetails");
            if (chk.Checked)
            {
                DataSet ds = objstr.GetSingleRecordField(Convert.ToInt32(chk.ToolTip));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!dsFields.Tables[0].Rows.Contains(chk.ToolTip))
                    {
                        dsFields.Tables[0].ImportRow(dr);
                        chk.Checked = false;
                    }
                    else
                    {
                        DisplayMessage("Selected ForeignInformative Field Already In List");
                    }
                    chk.Checked = false;
                }
                //objQuotTender.UpdateTenderFieldEntry(objQt.TENDERNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
            }
        }
        grdField.DataSource = dsFields.Tables[0];
        grdField.DataBind();
        if (grdField.Rows.Count > 0)
        {
            grdField.Visible = true;

        }
        else
        {
            DisplayMessage("Select atleast one field to add");
        }
        Session["ds"] = dsFields;
        #endregion
    }

    private int SaveFieldsForTender(string tenderno, string colcode)
    {

        DataSet dsFieldsFromDB = objQuotTender.GetFieldsByTenderNo(tenderno);

        dsFields = (DataSet)Session["ds"];

        foreach (DataRow drdb in dsFieldsFromDB.Tables[0].Rows)
        {
            if (!CheckFieldsInDB(dsFields, drdb["TFNO"].ToString()))
            {
                objQuotTender.DeleteTenderFieldEntry(tenderno, Convert.ToInt32(drdb["TFNO"].ToString()));
            }

        }
        dsFieldsFromDB = objQuotTender.GetFieldsByTenderNo(tenderno);
        foreach (DataRow dr in dsFields.Tables[0].Rows)
        {
            if (!CheckFieldsInDB(dsFieldsFromDB, dr["TFNO"].ToString()))
            {
                objQuotTender.UpdateTenderFieldEntry(tenderno, Convert.ToInt32(dr["FNO"].ToString()), colcode);
            }

        }

        return 0;
    }

    private int SaveNewsForTender(string tenderno, string Colcode)
    {
        DataSet dsNewsFromDB = objQuotTender.GetNewsPaperByTenderNo(tenderno);

        dsNews = (DataSet)ViewState["dsnews"];

        foreach (DataRow drdb in dsNewsFromDB.Tables[0].Rows)
        {
            if (!CheckNewsInDB(dsNews, drdb["TNPNO"].ToString()))
            {
                objQuotTender.DeleteTenderNewsPaper(tenderno, Convert.ToInt32(drdb["TNPNO"].ToString()));
            }

        }
        dsNewsFromDB = objQuotTender.GetNewsPaperByTenderNo(tenderno);
        foreach (DataRow dr in dsNews.Tables[0].Rows)
        {
            if (!CheckNewsInDB(dsNewsFromDB, dr["TNPNO"].ToString()))
            {
                objQuotTender.UpdateTenderNewsPaper(tenderno, Convert.ToInt32(dr["NPNO"].ToString()), Colcode);
            }

        }

        return 0;
    }

    bool CheckFieldsInDB(DataSet dsFields, string TFNO)
    {
        bool retstat = false;
        foreach (DataRow dr in dsFields.Tables[0].Rows)
        {
            if (TFNO == dr["TFNO"].ToString().Trim())
            {
                retstat = true;
            }


        }
        return retstat;
    }

    bool CheckNewsInDB(DataSet dsnews, string TNPNO)
    {
        bool retstat = false;
        foreach (DataRow dr in dsnews.Tables[0].Rows)
        {
            if (TNPNO == dr["TNPNO"].ToString().Trim())
            {
                retstat = true;
            }


        }
        return retstat;
    }

    void InitilizeFieldGrid()
    {
        dsFields = objQuotTender.GetFieldsByTenderNo("-999");
        DataColumn[] primcolmun = { dsFields.Tables[0].Columns["FNO"] };
        dsFields.Tables[0].PrimaryKey = primcolmun;
        Session["ds"] = dsFields;
        dsNews = objQuotTender.GetNewsPaperByTenderNo("-9999");
        DataColumn[] primNews = { dsNews.Tables[0].Columns["NPNO"] };
        dsNews.Tables[0].PrimaryKey = primNews;
        ViewState["dsnews"] = dsNews;
    }

    protected void btnaddNews_Click(object sender, EventArgs e)
    {
        dsNews = (DataSet)ViewState["dsnews"];
        //DataTable tbFields=new DataTable() ;

        StoreMasterController objstr = new StoreMasterController();
        #region Search NewsPaper
        for (int i = 0; i < lvNewspaper.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvNewspaper.Items[i].FindControl("ChkNewspaper");
            if (chk.Checked)
            {
                //DataSet ds = objstr.GetSingleNewPaper(Convert.ToInt32(chk.ToolTip));
                DataSet ds = objCommon.FillDropDown("STORE_NEWSPAPER A,STORE_CITY B", "NPNO,NPNAME,A.CITYNO", "B.CITY,A.COLLEGE_CODE", "NPNO=" + chk.ToolTip.ToString() + " AND A.CITYNO=B.CITYNO", "");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!dsNews.Tables[0].Rows.Contains(chk.ToolTip))
                    {
                        dsNews.Tables[0].ImportRow(dr);

                    }
                    else
                    {
                        DisplayMessage("Selected NewsPaper Already In List");
                    }
                    chk.Checked = false;
                }

            }
        }
        
        if (dsNews != null && dsNews.Tables[0].Rows.Count > 0)
        {
            grdNews.DataSource = dsNews.Tables[0];
            grdNews.DataBind();
        }

        if (grdNews.Rows.Count < 1)
        {
            DisplayMessage("Select atleast one Newspaper to add");

        }
        else if (grdNews.Rows.Count >= Convert.ToInt32(lvNewspaper.Items.Count))
        {
            grdNews.Visible = true;
            DisplayMessage("All newspapers are added. No more Newspapers to add");
        }
        else
        {
            grdNews.Visible = true;
        }
        ViewState["dsnews"] = dsNews;
        #endregion
    }

    protected void grdNews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            LinkButton btndel = (LinkButton)e.CommandSource;
            dsNews = (DataSet)ViewState["dsnews"];



            //objCommon.ConfirmMessage(null, "Are you Sure To Remove this Field", Page);
            DataRow dr = dsNews.Tables[0].Rows.Find(e.CommandArgument);
            dsNews.Tables[0].Rows.Remove(dr);
            grdNews.DataSource = dsNews.Tables[0];
            grdNews.DataBind();
            //Tabs.ActiveTabIndex = 3;

        }
    }

    protected void grdNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ChkIndentDetails_CheckChanged(object sender, EventArgs e)
    {
        this.InitilizeFieldGrid();
        if (ViewState["action"].Equals("add"))
        {
            foreach (GridViewRow oldrow in grdIndList.Rows)
            {
                ((RadioButton)oldrow.FindControl("CheckIndent")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("CheckIndent")).Checked = true;
            lvitems.DataSource = objQuotTender.GetItemsForQuotationTender(rb.ToolTip).Tables[0];
            lvitems.DataBind();
            BindListView_Fields();
            pnlFields.Visible = true;
            BindListView_Newspaper();
            pnlNewsPaper.Visible = true;
            //this.InitilizeFieldGrid();
            this.FillBudget();

            GenerateTenderNo(rb.ToolTip);
        }
        else
        {
            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("CheckIndent")).Checked = false;
            this.DisplayMessage("You Can Not Change Indent No. During Modification");
        }
    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
        
    }
    //void DisplayMessage(string Message)
    //{

    //    string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
    //    string message = string.Format(prompt, Message);
    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearItems();

    }
    void GenerateTenderNo(string  INDNO)
    {
        char status = Convert.ToChar(objCommon.LookUp("STORE_INDENT_MAIN", "TQSTATUS", "INDNO='" + INDNO + "'"));

        DataSet ds = new DataSet();
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ds = objQuotTender.GenrateTenderNo(mdno, status,Convert.ToInt32(Session["OrgId"].ToString()));  //10-03-2022 GAYATRI
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtTenderNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["TENDERNO"].ToString());
        }
        DataSet ds1 = new DataSet();
        ds1 = objQuotTender.GenrateTenderRefNo(mdno, status, Convert.ToInt32(Session["OrgId"].ToString()));  //10-03-2022 GAYATRI
        if (ds1.Tables[0].Rows.Count > 0)
        {
            txtReferenceNumber.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TENDERREFNO"].ToString());
        }
    }

    protected void lstTender_SelectedIndexChanged(object sender, EventArgs e)
    {

        //btnRpt.Visible = true;
        //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Str_Tender.rpt&param=@P_TENDERNO=" + (lstTender.SelectedValue), "UAIMS");
        //Tabs.ActiveTabIndex = 5;
    }
    private void bindTender()
    {
        DataSet ds = new DataSet();
        ds = objQuotTender.GetAllTender();
        lstTender.DataSource = ds.Tables[0];
        lstTender.DataTextField = "TDRNO";
        lstTender.DataValueField = "TENDERNO";
        lstTender.DataBind();

    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        ShowReport("Tender_Report", "Str_Tender.rpt");

    }

    protected void btnLtdTender_Click(object sender, EventArgs e)
    {
        ShowReport("Tender_Report", "Str_Tender_Limited.rpt");

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
            url += "&param=@P_TENDERNO=" + lstTender.SelectedValue;


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnTendeNews_Click(object sender, EventArgs e)
    {
        //ShowReport("Tender_Report", "Str_Tender_News.rpt");
        ShowReport("Tender_Report", "Str_Tender_Single_Bid.rpt");
    }
    protected void btnTenderNotice_Click(object sender, EventArgs e)
    {
        ShowReport("Tender_Report", "Str_Tender_Notice.rpt");
    }


}
