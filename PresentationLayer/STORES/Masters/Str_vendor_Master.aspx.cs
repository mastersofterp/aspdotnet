//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_vendor_Master.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using System.Collections.Generic;

public partial class Stores_Masters_Str_vendor_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();

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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                FillCity();
                FillState();
                FillCategory();
                BindListViewParty();
                BindListViewPartyCategory();
                ViewState["dtBank"] = null;

            }
            //Set vendor category Report Parameters
            //objCommon.ReportPopUp(btnshowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Vendor_Category_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            //Set vendor Master Report Parameters
            //objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Vendor_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    #region VENDOR_CATEGORY

    private void BindListViewPartyCategory()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllParyCategory();
            ds = objCommon.FillDropDown("STORE_PARTY_CATEGORY", "PCNAME,PCSHORTNAME", "PCNO", "", "");
            lvCategoryName.DataSource = ds;
            lvCategoryName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.BindListViewPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butCategoryName_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_PARTY_CATEGORY", " count(*)", "pcname= '" + Convert.ToString(txtCategoryName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {


                        CustomStatus cs = (CustomStatus)objStrMaster.AddParyCategory(txtCategoryName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString(), txtCategoryShortName.Text);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtCategoryName.Text = string.Empty;
                            txtCategoryShortName.Text = string.Empty;
                            objCommon.DisplayMessage(updatePanel3, "Record Saved Successfully", this);
                            BindListViewPartyCategory();
                            FillCategory();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel3, "Record Already exist", this);
                    }
                }
                else
                {

                    if (ViewState["cNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_PARTY_CATEGORY", " count(*)", "pcname= '" + Convert.ToString(txtCategoryName.Text) + "' and pcno <> " + Convert.ToInt32(ViewState["cNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus cs = (CustomStatus)objStrMaster.UpdateParyCategory(txtCategoryName.Text, Convert.ToInt32(ViewState["cNo"].ToString()), Session["colcode"].ToString(), Session["userfullname"].ToString(), txtCategoryShortName.Text);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                                txtCategoryName.Text = string.Empty;
                                txtCategoryShortName.Text = string.Empty;
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(updatePanel3, "Record Updated Successfully", this);
                                BindListViewPartyCategory();
                                FillCategory();
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel3, "Record Already Exist", this);
                        }

                    }


                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.butCategoryName_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEditPartyCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["cNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsPartyCategory(Convert.ToInt32(ViewState["cNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.btnEditPartyCategory_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsPartyCategory(int cNo)
    {
        DataSet ds = null;

        try
        {
            //ds = objStrMaster.GetSingleRecordParyCategory(cNo);
            ds = objCommon.FillDropDown("STORE_PARTY_CATEGORY", "PCNAME,PCSHORTNAME", "PCNO", "PCNO=" + cNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtCategoryName.Text = ds.Tables[0].Rows[0]["PCNAME"].ToString();
                txtCategoryShortName.Text = ds.Tables[0].Rows[0]["PCSHORTNAME"].ToString();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.ShowEditDetailsPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void butPartyCategoryCancel_Click(object sender, EventArgs e)
    {

        //Response.Redirect(Request.Url.ToString());
        txtCategoryName.Text = string.Empty;
        txtCategoryShortName.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void dpPagerCategoryName_PreRender(object sender, EventArgs e)
    {
        BindListViewPartyCategory();
    }

    #endregion

    #region VENDOR

    private void BindListViewParty()
    {
        try
        {
            //FillCategory(); 
            DataSet ds = objStrMaster.GetAllParty();
            lvParty.DataSource = ds;
            lvParty.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.BindListViewParty-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butParty_Click(object sender, EventArgs e)
    {
        try
        {
            StoreMaster objStrMst = new StoreMaster();
            //if (ViewState["dtBank"] == null && (DataTable)ViewState["dtBank"] == null)      //-------its commented on 27/01/2023
            //{
            //    objCommon.DisplayMessage(updatePanel1, "Please Add Bank Details", this);
            //    return;
            //}
            objStrMst.PCODE = txtVendorCode.Text;
            objStrMst.PNAME = txtVendorName.Text;
            objStrMst.PHONE = txtPhone.Text;
            objStrMst.EMAIL = txtEmail.Text;
            objStrMst.STATENO = Convert.ToInt32(ddlState.SelectedValue);
            objStrMst.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            objStrMst.ADDRESS = txtAddress.Text;
            objStrMst.PCNO = Convert.ToInt32(ddlCategory.SelectedValue);

            objStrMst.CST = txtCST.Text;
            objStrMst.BST = txtBST.Text;
            objStrMst.REMARK = txtRemark.Text;
            objStrMst.COLLEGE_CODE = Session["colcode"].ToString();

            objStrMst.PARTY_BANK_DETAIL_TBL = (DataTable)ViewState["dtBank"];

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", " count(*)", "pcode='" + Convert.ToString(txtVendorCode.Text) + " ' "));

                    if (duplicateCkeck == 0)
                    {

                        CustomStatus cs = (CustomStatus)objStrMaster.AddParty(objStrMst, Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            objCommon.DisplayMessage(updatePanel1, "Record Saved Successfully", this);
                            BindListViewParty();
                            Clear();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel1, "Record Already Exist", this);
                    }
                }
                else
                {

                    if (ViewState["pNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", " count(*)", "pcode= '" + Convert.ToString(txtVendorCode.Text) + "' and pno<> " + Convert.ToInt32(ViewState["pNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            objStrMst.PNO = Convert.ToInt32(ViewState["pNo"].ToString());
                            CustomStatus cs = (CustomStatus)objStrMaster.UpdateParty(objStrMst, Session["userfullname"].ToString());
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(updatePanel1, "Record Updated Successfully", this);
                                BindListViewParty();
                                Clear();
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel1, "Record Already Exist", this);
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.butParty_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEditParty_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["pNo"] = int.Parse(btnEdit.CommandArgument);
            if (Convert.ToInt32(objCommon.LookUp("STORE_VENDOR_PAYMENT", "COUNT(*)", "PNO =" + Convert.ToInt32(ViewState["pNo"]))) > 0)
            {
                objCommon.DisplayMessage(updatePanel1, "Payment Entry Has Done Against This Vendor. So,You Can Not Modify.", this);
                return;
            }
            ViewState["action"] = "edit";
            ShowEditDetailsParty(Convert.ToInt32(ViewState["pNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.btnEditParty_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsParty(int pNo)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordParty(pNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtVendorCode.Text = ds.Tables[0].Rows[0]["PCODE"].ToString(); ;
                txtVendorName.Text = ds.Tables[0].Rows[0]["PNAME"].ToString(); ;
                txtPhone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString(); ;
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString(); ;
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString(); ;
                ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString(); ;
                txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString(); ;
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["PCNO"].ToString(); ;
                // txtOpeningBalanceValue.Text = ds.Tables[0].Rows[0]["POB"].ToString();
                txtCST.Text = ds.Tables[0].Rows[0]["CST"].ToString();
                txtBST.Text = ds.Tables[0].Rows[0]["BST"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lvBank.DataSource = ds.Tables[1];
                lvBank.DataBind();
                lvBank.Visible = true;
                ViewState["dtBank"] = ds.Tables[1];
            }
            else
            {
                lvBank.DataSource = null;
                lvBank.DataBind();
                lvBank.Visible = true;
                ViewState["dtBank"] = null;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.ShowEditDetailsPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        //finally
        //{
        //    ds.Clear();
        //    ds.Dispose();
        //}

    }

    protected void butPartyCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //ViewState["action"] = "add";
    }

    protected void Clear()
    {
        txtVendorCode.Text = string.Empty;
        txtVendorName.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlState.SelectedValue = "0";
        ddlCity.SelectedValue = "0";
        txtAddress.Text = string.Empty;
        ddlCategory.SelectedValue = "0";
        txtCST.Text = string.Empty;
        txtBST.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ViewState["action"] = "add";
        lvBank.DataSource = null;
        lvBank.DataBind();
        lvBank.Visible = false;
        ViewState["dtBank"] = null;
        ClearBank();

    }

    protected void dpPagerParty_PreRender(object sender, EventArgs e)
    {
        BindListViewParty();
    }

    protected void FillCategory()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "STORE_PARTY_CATEGORY", "pcno", "pcname", "", "pcname");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.FillCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillCity()
    {
        try
        {
            objCommon.FillDropDownList(ddlCity, "store_city", "cityno", "city", "", "city");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.FillCity-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillState()
    {
        try
        {
            objCommon.FillDropDownList(ddlState, "store_state", "stateno", "statename", "", "statename");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.FillState-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        generateCode();
    }

    protected void generateCode()
    {
        DataSet ds = new DataSet();
        int pcno = Convert.ToInt32(ddlCategory.SelectedValue);
        ds = objStrMaster.GenratePartyCode(pcno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtVendorCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["VENDORNO"].ToString());
        }
    }

    #endregion


    protected void tab_activetabindexchanged(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #region Add Bank detail
    private bool CheckDuplicateVehRow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["BANK_ACC_NO"].ToString().ToLower() == value.ToLower())
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.checkDuplicateAdministrationRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    protected void btnAddBankInfo_Click(object sender, EventArgs e)
    {
        try
        {
            lvBank.Visible = true;
            DataTable dtBankdup = (DataTable)ViewState["dtBank"];

            if (CheckDuplicateVehRow(dtBankdup, txtAccNum.Text.Trim()))
            {
                objCommon.DisplayMessage(updatePanel1, "This Bank Account Is Already Exist.", this);
                return;
            }

            DataTable dtBank = new DataTable();
            if (ViewState["dtBank"] == null && (DataTable)ViewState["dtBank"] == null)
            {
                dtBank = this.CreateBankTable();
            }
            else
            {
                dtBank = (DataTable)ViewState["dtBank"];
            }
            DataRow dr = null;
            int maxVal = 0;
            dr = dtBank.NewRow();
            if (dr != null)
            {
                maxVal = Convert.ToInt32(dtBank.AsEnumerable().Max(row => row["SRNO"]));
            }
            dr["SRNO"] = maxVal + 1;
            dr["BANK_NAME"] = txtBankName.Text.Trim();
            dr["BRANCH_NAME"] = txtBranchName.Text.Trim();
            dr["IFSC_CODE"] = txtIfscCode.Text.Trim();
            dr["BANK_ACC_NO"] = txtAccNum.Text.Trim();

            dtBank.Rows.Add(dr);

            ViewState["dtBank"] = dtBank;
            lvBank.DataSource = dtBank;
            lvBank.DataBind();
            ClearBank();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnAddVeh_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable CreateBankTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SRNO", typeof(int));
        dt.Columns.Add("BANK_NAME", typeof(string));
        dt.Columns.Add("BRANCH_NAME", typeof(string));
        dt.Columns.Add("IFSC_CODE", typeof(string));
        dt.Columns.Add("BANK_ACC_NO", typeof(string));
        return dt;
    }

    private void ClearBank()
    {
        txtBankName.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        txtIfscCode.Text = string.Empty;
        txtAccNum.Text = string.Empty;
    }
    #endregion


    protected void btnCancelBank_Click(object sender, EventArgs e)
    {
        ClearBank();
    }
    protected void btnDeleteBank_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DataTable dt = null;
        if (ViewState["dtBank"] != null && (DataTable)ViewState["dtBank"] != null)
        {
            dt = (DataTable)ViewState["dtBank"];
            dt.Rows.Remove(this.GetRowToDelete(dt, btn.CommandArgument));
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["SRNO"] = i + 1;
        }
        lvBank.DataSource = dt;
        lvBank.DataBind();
        if (dt.Rows.Count == 0)
            ViewState["dtBank"] = null;
        else
            ViewState["dtBank"] = dt;
    }

    private DataRow GetRowToDelete(DataTable dt, string Srno)
    {
        DataRow dtRow = null;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["SRNO"].ToString() == Srno)
            {
                dtRow = dr;
                break;
            }
        }
        return dtRow;
    }
    protected void btnshowrpt1_Click(object sender, EventArgs e)
    {

        ShowReport1("VendorMasterReport", "Vendor_Category_report.rpt");

    }






    private void ShowReport1(string reportTitle, string rptFileName)
    {


        try
        {
            //objCommon.ReportPopUp(btnshowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Vendor_Category_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");      
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["userfullname"].ToString();
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ReportName=" + reportTitle + ",@P_VENDORNO=" + vendorno + ",@P_VENDORWISE=" + rblSelectAllVendor.SelectedValue + ",@P_FDATE=" + Convert.ToDateTime(Fdate).ToString("dd-MMM-yyyy") + ",@P_TDATE=" + Convert.ToDateTime(Ldate).ToString("dd-MMM-yyyy") + ",@P_MDNO=" + Session["MDNO"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Tax_Master.ShowTaxMasterReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Vendor_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        ShowReport1("VendorMasterReport", "Vendor_List_Report.rpt");
    }


    protected void lkFeedback_Click(object sender, EventArgs e)
    {


        divAnnounce.Visible = false;
        divEmoji.Visible = true;
        divlkAnnouncement.Attributes.Remove("class");
        divlkFeed.Attributes.Add("class", "active");
    }
    protected void lkAnnouncement_Click(object sender, EventArgs e)
    {
        // ddlGrade.SelectedIndex = 0;

        divEmoji.Visible = false;
        divAnnounce.Visible = true;
        divlkFeed.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Add("class", "active");
    }
}
