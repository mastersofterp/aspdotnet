//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Quotation_Entry_Form.aspx                                                  
// CREATION DATE : 11-Sept-2009                                 

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

public partial class Stores_Transactions_Quotation_Str_Quotation_Entry_Form : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Quotation_Tender_Controller objQuotTender = new Str_Quotation_Tender_Controller();
    Str_Vendor_Quotation_Entry_Controller objVQtEntry = new Str_Vendor_Quotation_Entry_Controller();
    VendorController objvendor = new VendorController();

    string QUOT_INDNO;

    double QUOT_AMT;
    string authority;

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
            ViewState["action"] = "add";
            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                this.BindListView_IndentDetails('A');

                //this.bindbugethead();
                this.bindquotation();
                //Tabs.ActiveTabIndex = 0;
            }
        }
        //String hiddenFieldValue = hidLastTab.Value;
        //System.Text.StringBuilder js = new System.Text.StringBuilder();
        //js.Append("<script type='text/javascript'>");
        //js.Append("var previouslySelectedTab = ");
        //js.Append(hiddenFieldValue);
        //js.Append(";</script>");
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "acttab", js.ToString());

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


    private void bindquotation()
    {
        DataSet ds = new DataSet();
        ds = objQuotTender.GetAllQuotationEntry(Convert.ToInt32(Session["strdeptcode"].ToString()));
        lstquot.DataSource = ds.Tables[0];
        lstquot.DataTextField = "REFNO";
        lstquot.DataValueField = "QUOTNO";
        lstquot.DataBind();
    }

    private void bindbugethead()//string Indno
    {
        objCommon.FillDropDownList(ddlBudgetHead, "[DBO].[ACC_BUDGET_HEAD_NEW] BP INNER JOIN  [DBO].[ACC_BUDGET_HEAD_NEW] BC ON (BP.BUDGET_NO = BC.PARENT_ID)", "BC.BUDGET_NO", "BP.BUDGET_HEAD+' - '+BC.BUDGET_HEAD+'('+BC.BUDGET_CODE+')' as BUDGET_HEAD", "", "BP.BUDGET_HEAD");
        // objCommon.FillDropDownList(ddlBudgetHead, "STORE_BUDGET_HEAD", "BHNO", "BHNAME", "", "BHNAME");
        //DataSet ds = new DataSet();
        //ds = objQuotTender.GetBudgetHead(Indno);
        //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlBudgetHead.DataSource = ds.Tables[0];
        //    ddlBudgetHead.DataTextField = ds.Tables[0].Columns["BUDGET_HEAD"].ToString();
        //    ddlBudgetHead.DataValueField = ds.Tables[0].Columns["BUDGET_NO"].ToString();
        //    ddlBudgetHead.DataBind();

        //}
    }

    private void BindListView_IndentDetails(char Flag)
    {
        DataSet ds = null;
        try
        {
            ds = objQuotTender.GetIndentForQuotationTender(Convert.ToInt32(Session["strdeptcode"].ToString()), 'Q', Flag);

            if (ds.Tables[0].Rows.Count > 0)
            {
                butIndent.Visible = true;
                //btnReqListNext.Visible = false;
            }
            else
            {
                butIndent.Visible = false;
                //btnReqListNext.Visible = true;
            }

            grdIndList.DataSource = ds;
            grdIndList.DataBind();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Quotation_Entry_Form.BindListView_DeptRequestedList() --> " + ex.Message + " " + ex.StackTrace);
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
            lvForeignCalculative.DataSource = ds.Tables[3];
            lvForeignInformative.DataSource = ds.Tables[2];
            lvIndiaInformative.DataBind();
            lvIndiaCalculative.DataBind();
            lvForeignCalculative.DataBind();
            lvForeignInformative.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Quotation_Entry_Form.BindListView_Fields() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView_VendorCategory()
    {
        DataSet ds = null;
        try
        {
            ds = objvendor.GetAllParyCategory();
            lvCategory.DataSource = ds;
            lvCategory.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Quotation_Str_Quotation_Entry_Form.BindListView_VendorCategory() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStore";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND APLNO=" + deptStoreUser);

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStore";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    protected void butIndent_Click(object sender, EventArgs e)
    {
        string INDNO = string.Empty;
        DataSet ds = null;

        foreach (GridViewRow row in grdIndList.Rows)
        {
            RadioButton chk = row.FindControl("CheckIndent") as RadioButton;
            if (chk.Checked)
            {
                INDNO = INDNO + chk.ToolTip + ",";
                QUOT_INDNO = chk.ToolTip;
            }
        }

        if (INDNO != "")
        {
            INDNO = INDNO.Substring(0, INDNO.Length - 1);
            ds = objQuotTender.GetItemsForQuotationTender(INDNO);
            lvitems.DataSource = ds;
            lvitems.DataBind();

        }
        else
        {
            this.DisplayMessage("Please Select Indent.");
            return;
        }
       
        this.BindListView_Fields();
        this.BindListView_VendorCategory();
        //this.FillBudget();
        this.bindbugethead();//INDNO
        this.GenerateQuotNo();
        divItemDetails.Visible = true;
        divIndentList.Visible = false;
        btnReqListNext.Visible = false;
    }



    protected void butSaveQuotation_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtLasteDateofReciptTime.Text == string.Empty)
            {
                this.DisplayMessage("Please Enter Last Date of Receipt.");
                return;
            }
            string mesg = txtLasteDateTime.ToolTip.ToString();
            if (mesg.Equals("time is invalid"))
            {
                this.DisplayMessage("Invalid Time! please correct the time");

            }
            else
            {
                foreach (GridViewRow row in grdIndList.Rows)
                {
                    RadioButton chk = row.FindControl("CheckIndent") as RadioButton;
                    if (chk.Checked)
                    {
                        QUOT_INDNO = chk.ToolTip;
                    }
                }
                for (int i = 0; i < lvitems.Items.Count; i++)
                {
                    // CheckBox chk = lvitems.Items[i].FindControl("ChkitemDetails") as CheckBox;

                    Label lbl = (Label)lvitems.Items[i].FindControl("lblAmt");
                    Label lblItemSpeci = (Label)lvitems.Items[i].FindControl("lblItemSpeci");

                    if (lbl.Text == "")
                    {
                        QUOT_AMT += 0;
                    }
                    else
                    {
                        QUOT_AMT += Convert.ToDouble(lbl.Text);
                    }


                }
                foreach (ListViewItem lv in lvitems.Items)
                {
                    CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
                    //chkSelect.Checked = false;

                    if( chkSelect.Checked ==false)
                    {
                        DisplayMessage("Please Select Atleast One Item From Item Details.");
                        return;
                    }
                }

                Str_Quotation_Tender objQT = new Str_Quotation_Tender();
                if (!ViewState["action"].Equals("add"))
                {
                    objQT.QUOTNO = ddlQuotno.SelectedItem.Text;
                }
                else
                    objQT.QUOTNO = txtQuotationNumber.Text;
                objQT.REFNO = txtReferenceNumber.Text;
                objQT.QUOTAMT = QUOT_AMT;
                objQT.INDNO = QUOT_INDNO;
                objQT.BHALNO = Convert.ToInt32(ddlBudgetHead.SelectedValue);
                objQT.ODATE = Convert.ToDateTime(txtQuotationOpeningDate.Text);

                if (txtQuotationOpeningDateTime.Text != "")
                {
                    objQT.OTIME = Convert.ToDateTime(txtQuotationOpeningDateTime.Text);
                }
                else
                {
                    objQT.OTIME = DateTime.MinValue;
                }
                objQT.LDATE = Convert.ToDateTime(txtLasteDateofReciptTime.Text);
                if (txtLasteDateTime.Text != "")
                {
                    objQT.LTIME = Convert.ToDateTime(txtLasteDateTime.Text);
                }
                else
                {
                    objQT.LTIME = DateTime.MinValue;
                }
                objQT.SDATE = Convert.ToDateTime(txtSendingDate.Text);
                objQT.RTVALID = Convert.ToInt32(txtRateValidupto.Text);
                objQT.RTVALIDUNIT = ddlRateValidupto.SelectedItem.Text;
                authority = rblAuthority.SelectedValue.ToString();

                objQT.FLAG = "F";
                objQT.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());


                //string specdoc = Convert.ToString(objCommon.ReadTextFile(FileUpload1));
                //if (specdoc == "-2" || specdoc == "-3")
                //{

                //    if (Convert.ToString(objCommon.ReadTextFile(FileUpload1)) == "-2")
                //        DisplayMessage("Please Uplaod Text File only");
                //    else
                //        DisplayMessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");


                //}
                //else
                //{
                //    if (ViewState["action"].Equals("add"))
                //    {
                //        if (specdoc == "-1")
                //            objQT.MATTER = null;

                //        else
                //            objQT.MATTER = specdoc;
                //    }
                //    else
                //    {
                //        objQT.MATTER = txtTopMatter.Text;
                //    }
                //}


                //string specdoc1 = Convert.ToString(objCommon.ReadTextFile(FileUpload2));
                //if (specdoc1 == "-2" || specdoc1 == "-3")
                //{

                //    if (Convert.ToString(objCommon.ReadTextFile(FileUpload2)) == "-2")
                //        DisplayMessage("Please Uplaod Text File only");
                //    else
                //        DisplayMessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");


                //}
                //else
                //{
                //    if (ViewState["action"].Equals("add"))
                //    {
                //        if (specdoc1 == "-1")
                //        {
                //            objQT.TERM = null;
                //        }
                //        else
                //        {
                //            objQT.TERM = specdoc1;
                //        }
                //    }
                //    else
                //    {
                //        objQT.TERM = txtTermsCondition.Text;
                //    }

                //}

                objQT.SUBJECT = txtSubject.Text;
                objQT.TOPSPECI = txtSpecification.Text;
                objQT.TERM = txtTermsCondition.Text;
                objQT.MATTER = txtTopMatter.Text;

                if (ViewState["action"].Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_QUOTENTRY", " count(*)", "quotno='" + objQT.QUOTNO + "'"));                  
                    if (duplicateCkeck == 0)
                    {
                        int count = 0;
                        for (int i = 0; i < lvVendors.Items.Count; i++)
                        {
                            CheckBox chk = (CheckBox)lvVendors.Items[i].FindControl("ChkVendorDetails");
                            if (chk.Checked)
                            {
                                count++;
                            }
                        }

                        if (count > 0)
                        {
                            CustomStatus cs = (CustomStatus)objQuotTender.AddQuotationTender(objQT, Session["colcode"].ToString(), authority);
                            SaveQuotationItemEntry(lvitems, objQT.QUOTNO, objQT.INDNO, Session["colcode"].ToString());
                            SaveQuotationPartyEntry(objQT);

                            //SaveQuotationFeild(objQT);
                            //SaveQuotationItemEntry(objQuotTender.GetItemsForQuotationTender(objQT.INDNO).Tables[0], objQT.QUOTNO, objQT.INDNO, Session["colcode"].ToString());

                            objCommon.DisplayUserMessage(updatePanel1, "Quotation Saved Successfully", Page);
                            // this.DisplayMessage("Quotation Saved Successfully");
                            bindquotation();
                            this.ClearItems();
                            SetDefaultFileUpload();
                        }

                        else
                        {
                            DisplayMessage("Please Select Vendor(s)");
                        }
                        //}
                    }
                    else
                    {
                        this.DisplayMessage("Quotation Already Exist");
                    }
                }
                else
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_QUOTENTRY", " count(*)", "quotno='" + objQT.QUOTNO + "'and quotno <> '" + objQT.QUOTNO + "'"));

                    if (duplicateCkeck == 0)
                    {
                         int count = 0;
                        for (int i = 0; i < lvVendors.Items.Count; i++)
                        {
                            CheckBox chk = (CheckBox)lvVendors.Items[i].FindControl("ChkVendorDetails");
                            if (chk.Checked)
                            {
                                count++;
                            }
                        }

                        if (count > 0)
                        {
                            CustomStatus cs = (CustomStatus)objQuotTender.UpdateQuotationEntry(objQT, Session["colcode"].ToString(), authority);
                            objCommon.DeleteRow("STORE_PARTYENTRY", "QUOTNO='" + ddlQuotno.SelectedItem.Text + "'");
                            objCommon.DeleteRow("STORE_QUOTATION_ITEMENTRY", "QUOTNO='" + ddlQuotno.SelectedItem.Text + "'");
                            objCommon.DeleteRow("STORE_QUOTFIELDENTRY", "QUOTNO='" + ddlQuotno.SelectedItem.Text + "'");

                            SaveQuotationPartyEntry(objQT);
                            SaveQuotationItemEntry(lvitems, objQT.QUOTNO, objQT.INDNO, Session["colcode"].ToString());
                            //SaveQuotationFeild(objQT);

                            //objCommon.DisplayUserMessage(updatePanel1,"Quotation updated Successfully",Page);
                            this.DisplayMessage("Quotation Updated Successfully");
                            ViewState["action"] = "add";
                            this.ClearItems();
                            SetDefaultFileUpload();
                        }
                        else
                        {
                            DisplayMessage("Please Select Vendor(s)");
                        }
                    }
                    else
                    {
                        this.DisplayMessage("Quotation Already Exist");
                    }
                }
               
                

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Str_Quotation_Entry_Form.save quotation.butSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }


    private void FillBudget()
    {
        try
        {
            objCommon.FillDropDownList(ddlBudgetHead, "STORE_BUDGETHEAD B,STORE_BUDGETHEAD_Allocate A", "A.BHALNO", "B.BHNAME", "A.BHNO=B.BHNO", "BHNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillBudget() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }


    void SaveQuotationFeild(Str_Quotation_Tender objQT)
    {
        int count = 0;
        for (int i = 0; i < lvIndiaCalculative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvIndiaCalculative.Items[i].FindControl("chkIndiaCalculative");

            if (chk.Checked)
            {
                count++;
            }
        }

        if (count > 0)
        {
            for (int i = 0; i < lvIndiaCalculative.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lvIndiaCalculative.Items[i].FindControl("chkIndiaCalculative");

                if (chk.Checked)
                {
                    objQuotTender.UpdateQuotationFieldEntry(objQT.QUOTNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
                }
            }

        }
        else
        {
            objQuotTender.UpdateQuotationFieldEntry(objQT.QUOTNO, 1, Session["colcode"].ToString());
        }
        for (int i = 0; i < lvIndiaInformative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvIndiaInformative.Items[i].FindControl("chkIndiaInformative");
            if (chk.Checked)
            {
                objQuotTender.UpdateQuotationFieldEntry(objQT.QUOTNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
            }
        }

        for (int i = 0; i < lvForeignCalculative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvForeignCalculative.Items[i].FindControl("chkForeignCalculativeDetails");
            if (chk.Checked)
            {
                objQuotTender.UpdateQuotationFieldEntry(objQT.QUOTNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
            }
        }

        for (int i = 0; i < lvForeignInformative.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvForeignInformative.Items[i].FindControl("chkForeignInformativeDetails");
            if (chk.Checked)
            {
                objQuotTender.UpdateQuotationFieldEntry(objQT.QUOTNO, Convert.ToInt32(chk.ToolTip), Session["colcode"].ToString());
            }
        }
    }

    void SaveQuotationPartyEntry(Str_Quotation_Tender objQT)
    {
        for (int i = 0; i < lvVendors.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lvVendors.Items[i].FindControl("ChkVendorDetails");
            if (chk.Checked)
            {
                objQuotTender.UpdateQuotationPartyEntry(objQT.QUOTNO, Convert.ToInt32(chk.ToolTip), Convert.ToInt32(Session["strdeptcode"].ToString()), "", Session["colcode"].ToString());
            }

        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        //txtTermsCondition.Visible = true;
        //txtTopMatter.Visible = true;
       // FileUpload1.Visible = false;
       // FileUpload2.Visible = false;

        ClearItems();
        ViewState["action"] = "edit";
        //this.FillBudget();
        this.bindbugethead();
        objCommon.FillDropDownList(ddlQuotno, "STORE_QUOTENTRY", "QNO", "QUOTNO", "ISLOCK is null AND MDNO =" + Session["strdeptcode"].ToString(), "");

       // objCommon.FillDropDownList(ddlQuotno, "STORE_QUOTENTRY", "QNO", "QUOTNO", "ISLOCK!=1 AND MDNO ="+Session["strdeptcode"].ToString(), "");
        ddlQuotno.Visible = true;
        txtQuotationNumber.Visible = false;
        txtReferenceNumber.Enabled = true;
        pnlCategory.Visible = false;
        txtReferenceNumber.Text = string.Empty;
        rblAuthority.ClearSelection();

    }

    protected void ddlQuotno_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindListView_IndentDetails('M');
        btnModify.Visible = false;

        //BindListView_Fields();
        BindListView_VendorCategory();
        ShowDetailQuot(ddlQuotno.SelectedItem.Text);
        BindListView_VendorParty();
        //BindListView_VendorField();
        BindListView_ItemForUpdate();

        pnlCategory.Visible = true;


    }

    private void BindListView_VendorField()
    {
        
        DataSet dsField = null; // for party category No
        dsField = objQuotTender.GetFieldEntryForQuotation(ddlQuotno.SelectedItem.Text);

        string FieldNo = string.Empty;
        for (int i = 0; i < dsField.Tables[0].Rows.Count; i++)
        {

            string Fno = dsField.Tables[0].Rows[i]["FNO"].ToString();

            foreach (ListViewDataItem lvIF in lvIndiaInformative.Items)
            {
                CheckBox chk = lvIF.FindControl("chkIndiaInformative") as CheckBox;

                if (chk.ToolTip == Fno)
                {
                    chk.Checked = true;
                    FieldNo = FieldNo + Fno + ",";
                }
            }
        }

        for (int i = 0; i < dsField.Tables[1].Rows.Count; i++)
        {

            string Fno = dsField.Tables[1].Rows[i]["FNO"].ToString();

            foreach (ListViewDataItem lvIC in lvIndiaCalculative.Items)
            {
                CheckBox chk = lvIC.FindControl("chkIndiaCalculative") as CheckBox;

                if (chk.ToolTip == Fno)
                {
                    chk.Checked = true;
                    FieldNo = FieldNo + Fno + ",";
                }
            }
        }

        for (int i = 0; i < dsField.Tables[3].Rows.Count; i++)
        {

            string Fno = dsField.Tables[3].Rows[i]["FNO"].ToString();

            foreach (ListViewDataItem lvFC in lvForeignCalculative.Items)
            {
                CheckBox chk = lvFC.FindControl("chkForeignCalculativeDetails") as CheckBox;

                if (chk.ToolTip == Fno)
                {
                    chk.Checked = true;
                    FieldNo = FieldNo + Fno + ",";
                }
            }
        }

        for (int i = 0; i < dsField.Tables[2].Rows.Count; i++)
        {

            string Fno = dsField.Tables[2].Rows[i]["FNO"].ToString();

            foreach (ListViewDataItem lvFI in lvForeignInformative.Items)
            {
                CheckBox chk = lvFI.FindControl("ChkForeignInformativeDetails") as CheckBox;

                if (chk.ToolTip == Fno)
                {
                    chk.Checked = true;
                    FieldNo = FieldNo + Fno + ",";
                }
            }
        }
    }

    protected void BindListView_VendorParty()
    {
        DataSet dsPcno = null; // for party category No
        dsPcno = objCommon.FillDropDown("STORE_PARTY P INNER JOIN STORE_PARTYENTRY QP ON (P.PNO=QP.PNO) INNER JOIN STORE_PARTY_CATEGORY PC ON P.PCNO = PC.PCNO ", " P.PNAME, P.PCNO,PC.PCNAME ", "P.PNO", "QP.QUOTNO='" + ddlQuotno.SelectedItem.Text + "'", "");

        string categoryNo = string.Empty;
        for (int i = 0; i < dsPcno.Tables[0].Rows.Count; i++)
        {

            string pcno = dsPcno.Tables[0].Rows[i]["PCNO"].ToString();

            foreach (ListViewDataItem lvCat in lvCategory.Items)
            {
                CheckBox chk = lvCat.FindControl("ChkCategory") as CheckBox;

                if (chk.ToolTip == pcno)
                {
                    chk.Checked = true;
                    categoryNo = categoryNo + pcno + ",";
                }
            }
        }


        DataSet dsPno = null; // For Party PNO
        if (categoryNo != "")
        {
            categoryNo = categoryNo.Substring(0, categoryNo.Length - 1);
            dsPno = objQuotTender.GetVendorsForQuotation(categoryNo);
            lvVendors.DataSource = dsPno;
            lvVendors.DataBind();
            pnlVendors.Visible = true;
        }


      
        for (int i = 0; i < dsPcno.Tables[0].Rows.Count; i++)
        {

            string pno = dsPcno.Tables[0].Rows[i]["PNO"].ToString();

            foreach (ListViewDataItem lvVendor in lvVendors.Items)
            {
                CheckBox chk = lvVendor.FindControl("ChkVendorDetails") as CheckBox;

                if (chk.ToolTip == pno)
                {
                    chk.Checked = true;

                }
            }
        }


        //nlVendors.Visible = true;

    }


    protected void BindListView_ItemForUpdate()
    {
        DataSet dsItem = null; // for Item
        dsItem = objCommon.FillDropDown("STORE_QUOTENTRY QUOT INNER JOIN STORE_QUOTATION_ITEMENTRY QUOTITEM ON(QUOT.QUOTNO=QUOTITEM.QUOTNO)", " QUOTITEM.ITEM_NO", "QINO", "QUOTITEM.QUOTNO='" + ddlQuotno.SelectedItem.Text + "'", "");


        for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
        {

            string itemno = dsItem.Tables[0].Rows[i]["ITEM_NO"].ToString();

            foreach (ListViewDataItem lvItem in lvitems.Items)
            {
                CheckBox chk = lvItem.FindControl("chkSelect") as CheckBox;

                if (chk.ToolTip == itemno)
                {
                    chk.Checked = true;

                }
            }
        }

    }

    void ShowDetailQuot(string Quot)
    {
        DataSet ds = objQuotTender.GetSingleQuotation(Quot);
        txtReferenceNumber.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();
        txtQuotationOpeningDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ODATE"].ToString()).ToShortDateString();
        txtSendingDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SDATE"].ToString()).ToShortDateString();
        txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
        txtLasteDateofReciptTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LDATE"].ToString()).ToShortDateString();
        txtRateValidupto.Text = ds.Tables[0].Rows[0]["RTVALID"].ToString();
        txtTopMatter.Text = ds.Tables[0].Rows[0]["MATTER"].ToString();
        txtTermsCondition.Text = ds.Tables[0].Rows[0]["TERMS"].ToString();
        txtSpecification.Text = ds.Tables[0].Rows[0]["TOPSPECI"].ToString();
        string ddlvalue = ds.Tables[0].Rows[0]["RTVALIDUNIT"].ToString();
        if (ddlvalue.ToLower() == "day(s)" || ddlvalue.ToLower() == "days")
        {
            ddlvalue = "1";
        }
        else if (ddlvalue.ToLower() == "month")
        {
            ddlvalue = "2";
        }
        else if (ddlvalue.ToLower() == "week")
        {
            ddlvalue = "3";
        }
        else if (ddlvalue.ToLower() == "year")
        {
            ddlvalue = "4";
        }
        else
            ddlvalue = "0";

        ddlRateValidupto.SelectedValue = ddlvalue;
        rblAuthority.SelectedValue = ds.Tables[0].Rows[0]["AUTHORITY"].ToString();
        ddlBudgetHead.SelectedValue = ds.Tables[0].Rows[0]["BHALNO"].ToString();
        //txtQuotationOpeningDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["OTIME"].ToString()).ToString("hh:mm:ss tt");
        //txtLasteDateTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LTIME"].ToString()).ToString("hh:mm:ss tt");
        string IndNo = ds.Tables[0].Rows[0]["INDNO"].ToString();
        //pnlVendors.Visible = false;

        for (int i = 0; i < grdIndList.Rows.Count; i++)
        {
            RadioButton chk = grdIndList.Rows[i].FindControl("CheckIndent") as RadioButton;
            if (chk.ToolTip == IndNo)
            {
                chk.Checked = true;
               // bindListvie_ItemDetails(IndNo);       //-------22/11/2022
                GetItemsForQuotationOnModify(IndNo);       //-------22/11/2022
            }
        }


    }

    void bindListvie_ItemDetails(String INDNO)
    {
        DataSet ds;
        ds = objQuotTender.GetItemsForQuotationTender(INDNO);
        lvitems.DataSource = ds;
        lvitems.DataBind();
    }

    void GetItemsForQuotationOnModify(String INDNO)    //-------22/111/2022 added method to modify the quotation item
    {
        DataSet ds;
        ds = objQuotTender.GetItemsForQuotationOnModify(INDNO,'M');
        lvitems.DataSource = ds;
        lvitems.DataBind();
    }
    void ClearItems()
    {
        //lvitems.Visible = false;
        btnModify.Visible = true;
        txtTermsCondition.Text = string.Empty;
        txtTopMatter.Text = string.Empty;
        txtSpecification.Text = string.Empty;

        txtQuotationNumber.Text = string.Empty;
        txtQuotationOpeningDate.Text = string.Empty;
        txtQuotationOpeningDateTime.Text = string.Empty;
        txtRateValidupto.Text = string.Empty;
        txtReferenceNumber.Text = string.Empty;
        txtSendingDate.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtLasteDateofReciptTime.Text = string.Empty;
        ddlQuotno.SelectedValue = "0";
        ddlBudgetHead.SelectedValue = "0";
        txtLasteDateTime.Text = string.Empty;
        txtQuotationNumber.Visible = true;
        txtReferenceNumber.Enabled = false;
        ddlQuotno.Visible = false;
        ddlRateValidupto.SelectedValue = "0";
        ViewState["action"] = "add";
        BindListView_IndentDetails('A');

        pnlCategory.Visible = false;
        
        lvitems.DataSource = null;
        lvitems.DataBind();
        rblAuthority.ClearSelection();
        this.GenerateQuotNo();
        lvVendors.DataSource = null;
        lvVendors.DataBind();
        pnlVendors.Visible = false;
        lvCategory.DataSource = null;
        lvCategory.DataBind();
        
        rblAuthority.SelectedValue = "0";

    }

    void SaveQuotationItemEntry(ListView lvitems, string quotno, string indno, string colcode)
    {

        foreach (ListViewDataItem item in lvitems.Items)
        {
            CheckBox chk = item.FindControl("chkSelect") as CheckBox;

            if (chk.Checked == true)
            {

                TextBox txtQty = item.FindControl("txtQuantity") as TextBox;
                int indQty = Convert.ToInt32(Convert.ToDouble(txtQty.Text));
                int itemno = Convert.ToInt32(chk.ToolTip);
                Label lblItemSpeci = item.FindControl("lblItemSpeci") as Label;
                string ItemSpecification = lblItemSpeci.Text.ToString();
                int QINO = 0;


                objQuotTender.SaveQuotItemEntry(quotno, itemno, indno, indQty, colcode, ItemSpecification, QINO);
            }
        }
    }

    protected void ChkIndentDetails_CheckChanged(object sender, EventArgs e)
    {
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
            bindListvie_ItemDetails(rb.ToolTip);
            //BindListView_Fields();

            BindListView_VendorCategory();
            pnlCategory.Visible = true;
            //this.FillBudget();
            this.bindbugethead();
            this.GenerateQuotNo();
        }
        else
        {
            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("CheckIndent")).Checked = false;
            this.DisplayMessage("You Cannot Changed Indent No During Modification");
        }
        //pnlVendors.Visible = false;
    }
    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
    }

    void SetDefaultFileUpload()
    {
        //txtTermsCondition.Visible = false;
        //txtTopMatter.Visible = false;
        //FileUpload1.Visible = true;
        //FileUpload2.Visible = true;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SetDefaultFileUpload();

        ClearItems();
    }

    void GenerateQuotNo()
    {
        DataSet ds = new DataSet();
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ds = objQuotTender.GenrateQuotNo(mdno, Convert.ToInt32(Session["OrgId"].ToString()));  //10-03-2022  GAYATRI
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtQuotationNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["QUOTNO"].ToString());
        }
        DataSet ds1 = new DataSet();
        ds1 = objQuotTender.GenrateQuotRefNo(mdno);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            txtReferenceNumber.Text = Convert.ToString(ds1.Tables[0].Rows[0]["REFNO"].ToString());
        }
    }
    protected void lstquot_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btnRpt.Visible = true;
        //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Str_Quotation.rpt&param=@P_QUOTNO=" + (lstquot.SelectedValue), "UAIMS");
        //Tabs.ActiveTabIndex = 5;
    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        if (lstquot.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Quotation To Display The Report", this.Page);
            return;
        }
        //ShowReport("quotation_report", "Str_Quotation.rpt"); 
        ShowReport("quotation_report", "Str_Quotation_svce.rpt");
    }
    //To Show Quotation report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_QUOTNO=" + lstquot.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();


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

    private void ShowQuotReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_QUOTNO=" + (lstquot.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            //"," + "@P_COLLEGE_CODE=" + 4 + 
            //url += "&param=@username=" + Session["userfullname"].ToString() + "," + "@INDTRNO=" + Convert.ToInt32(lstIndent.SelectedValue) + "," + "@P_INDTRNO=" + Convert.ToInt32(lstIndent.SelectedValue);


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

    protected void btnNoticeRpt_Click(object sender, EventArgs e)
    {
        ShowQuotReport("quotation_report", "Str_Quotation_Notice.rpt");
    }
    protected void btnSampleQuotRpt_Click(object sender, EventArgs e)
    {
        ShowQuotReport("quotation_report", "Str_Quotation_Main.rpt");
    }
    protected void btnSearchRep_Click(object sender, EventArgs e)
    {
       
        if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            string DtFrom = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
            string DtTo = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");

            objCommon.FillDropDownList(lstquot, "STORE_QUOTENTRY", "QUOTNO", "REFNO", "ODATE between '" + DtFrom + "' and '" + DtTo + "'", "QNO DESC");
        }
        else
        {
            DisplayMessage("Please Select Start Date and End Date.");
            return;
        }
    }
    protected void btnGetVender_Click(object sender, EventArgs e)
    {
        string categoryNo = string.Empty;
        DataSet ds = null;
        foreach (ListViewDataItem lvCat in lvCategory.Items)
        {
            CheckBox chk = lvCat.FindControl("ChkCategory") as CheckBox;
            if (chk.Checked)
            {
                categoryNo = categoryNo + chk.ToolTip + ",";
            }
        }
        try
        {
            categoryNo = categoryNo.Substring(0, categoryNo.Length - 1);
            ds = objQuotTender.GetVendorsForQuotation(categoryNo);

            lvVendors.DataSource = ds;
            lvVendors.DataBind();
            pnlVendors.Visible = true;
        }
        catch (Exception ex)
        {
            //throw;
            this.DisplayMessage("Please Select Vendor Category From List");
        }
    }

    protected void btnvenderGetNew_Click(object sender, EventArgs e)
    {
        string categoryNo = string.Empty;
        DataSet ds = null;
        foreach (ListViewDataItem lvCat in lvCategory.Items)
        {
            CheckBox chk = lvCat.FindControl("ChkCategory") as CheckBox;
            if (chk.Checked)
            {
                categoryNo = categoryNo + chk.ToolTip + ",";

            }
        }
        try
        {
            categoryNo = categoryNo.Substring(0, categoryNo.Length - 1);
            ds = objQuotTender.GetVendorsForQuotation(categoryNo);
        }
        catch (Exception ex)
        {
            this.DisplayMessage("Select Vendor Category From List");
        }
        lvVendors.DataSource = ds;
        lvVendors.DataBind();
        pnlVendors.Visible = true;
    }


    #region BackNext Buttons

    protected void btnReqListNext_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = true;
        btnReqListNext.Visible = false;
    }


    protected void btnDivTwoBack_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = true;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        btnReqListNext.Visible = true;
       
    }
    protected void btnDivTwoNext_Click(object sender, EventArgs e)
    {
        //divIndentList.Visible = false;
        //divItemDetails.Visible = false;
        //divFields.Visible = true;
        //divFieldDetails.Visible = true;
        divIndentList.Visible = false;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = true;
    }
    protected void btnFieldsBack_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = true;
        divFields.Visible = false;
        divFieldDetails.Visible = false;

    }
    protected void btnFieldsNext_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = true;
    }
    protected void btnVendorBack_Click(object sender, EventArgs e)
    {
        //divIndentList.Visible = false;
        //divItemDetails.Visible = false;
        //divFields.Visible = true;
        //divFieldDetails.Visible = true;
        //divVendorDetails.Visible = false;
        divIndentList.Visible = false;
        divItemDetails.Visible = true;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = false;
    }
    protected void btnVendorNext_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = false;
        divQuotationDetails.Visible = true;
    }
    protected void btnQuoDetailsBack_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = true;
        divQuotationDetails.Visible = false;
        divSearch.Visible = false;
        divReport.Visible = false;
    }
    protected void btnQuoDetailsNext_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = false;
        divQuotationDetails.Visible = false;
        divSearch.Visible = true;
        divReport.Visible = true;
    }

    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        divIndentList.Visible = false;
        divItemDetails.Visible = false;
        divFields.Visible = false;
        divFieldDetails.Visible = false;
        divVendorDetails.Visible = false;
        divQuotationDetails.Visible = true;
        divSearch.Visible = false;
        divReport.Visible = false;
    }
}
