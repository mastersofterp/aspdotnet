//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Sports  (Stock Maintenance)     
// CREATION DATE : 18-MAY-2017
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class Sports_StockMaintanance_SportsVendor : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventKitEnt objEK = new EventKitEnt();
    KitController objKCon = new KitController();   

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
                  //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                FillCity();
                FillState();
                FillCategory();
            }
            //Set vendor category Report Parameters
            //objCommon.ReportPopUp(btnshowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Sports" + "," + "Vendor_Category_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString(), "UAIMS");
            ////Set vendor Master Report Parameters
            //objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Sports" + "," + "Vendor_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString(), "UAIMS");
        }
    }

    #region VENDOR_CATEGORY

    private void BindListViewPartyCategory()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("SPRT_PARTY_CATEGORY", "PCNAME, PCSHORTNAME", "PCNO", "", "");
            lvCategoryName.DataSource = ds;
            lvCategoryName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.BindListViewPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCategoryName_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_PARTY_CATEGORY", " count(*)", "PCNAME= '" + Convert.ToString(txtCategoryName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objKCon.AddPartyCategory(txtCategoryName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString(), txtCategoryShortName.Text);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtCategoryName.Text = string.Empty;
                            txtCategoryShortName.Text = string.Empty;
                            objCommon.DisplayMessage(updatePanel3, "Record Saved successfully", this);
                            BindListViewPartyCategory();
                            FillCategory();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel3, "Record already exist", this);
                    }
                }
                else
                {
                    if (ViewState["cNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_PARTY_CATEGORY", " count(*)", "PCNAME= '" + Convert.ToString(txtCategoryName.Text) + "' and PCNO <> " + Convert.ToInt32(ViewState["cNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus cs = (CustomStatus)objKCon.UpdatePartyCategory(txtCategoryName.Text, Convert.ToInt32(ViewState["cNo"].ToString()), Session["colcode"].ToString(), Session["userfullname"].ToString(), txtCategoryShortName.Text);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                txtCategoryName.Text = string.Empty;
                                txtCategoryShortName.Text = string.Empty;
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(updatePanel3, "Record Updated successfully", this);
                                BindListViewPartyCategory();
                                FillCategory();
                                //Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel3, "Record already exist", this);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.butCategoryName_Click-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.btnEditPartyCategory_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsPartyCategory(int cNo)
    {
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("SPRT_PARTY_CATEGORY", "PCNAME, PCSHORTNAME", "PCNO", "PCNO=" + cNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCategoryName.Text = ds.Tables[0].Rows[0]["PCNAME"].ToString();
                txtCategoryShortName.Text = ds.Tables[0].Rows[0]["PCSHORTNAME"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.ShowEditDetailsPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void btnPartyCategoryCancel_Click(object sender, EventArgs e)
    {
        txtCategoryName.Text = string.Empty;
        txtCategoryShortName.Text = string.Empty;
        ViewState["cNo"] = null;
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
            DataSet ds = objKCon.GetAllParty(Session["colcode"].ToString());
            lvParty.DataSource = ds;
            lvParty.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.BindListViewParty-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnParty_Click(object sender, EventArgs e)
    {
        try
        {

            objEK.PCODE = txtVendorCode.Text;
            objEK.PNAME = txtVendorName.Text;
            objEK.PHONE = txtPhone.Text;
            objEK.EMAIL = txtEmail.Text;
            objEK.STATENO = Convert.ToInt32(ddlState.SelectedValue);
            objEK.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            objEK.ADDRESS = txtAddress.Text;
            objEK.PCNO = Convert.ToInt32(ddlCategory.SelectedValue);
            objEK.CST = txtCST.Text;
            objEK.BST = txtBST.Text;
            objEK.GST = txtGST.Text;
            objEK.PANNO = txtPanNumber.Text;
            objEK.REMARK = txtRemark.Text;
            objEK.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_PARTY", " count(*)", "PCODE='" + Convert.ToString(txtVendorCode.Text) + " ' "));

                    if (duplicateCkeck == 0)
                    {

                        CustomStatus cs = (CustomStatus)objKCon.AddParty(objEK, Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            objCommon.DisplayMessage(updatePanel1, "Record Save Successfully", this);
                            BindListViewParty();
                            Clear();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel1, "Record already exist", this);
                    }
                }
                else
                {

                    if (ViewState["pNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_PARTY", " count(*)", "PCODE= '" + Convert.ToString(txtVendorCode.Text) + "' and PNO<> " + Convert.ToInt32(ViewState["pNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            objEK.PNO = Convert.ToInt32(ViewState["pNo"].ToString());
                            CustomStatus cs = (CustomStatus)objKCon.UpdateParty(objEK, Session["userfullname"].ToString());
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(updatePanel1, "Record Update Successfully", this);
                                //BindListViewParty();
                                Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel1, "Record already exist", this);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.butParty_Click-> " + ex.Message + " " + ex.StackTrace);
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
            ViewState["action"] = "edit";
            ShowEditDetailsParty(Convert.ToInt32(ViewState["pNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.btnEditParty_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsParty(int pNo)
    {
        DataSet ds = null;
        try
        {
            ds = objKCon.GetSingleRecordParty(pNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtVendorCode.Text = ds.Tables[0].Rows[0]["PCODE"].ToString();
                txtVendorName.Text = ds.Tables[0].Rows[0]["PNAME"].ToString();
                txtPhone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
                ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["PCNO"].ToString();
                txtCST.Text = ds.Tables[0].Rows[0]["CST"].ToString();
                txtBST.Text = ds.Tables[0].Rows[0]["BST"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                txtGST.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                txtPanNumber.Text = ds.Tables[0].Rows[0]["PANNO"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.ShowEditDetailsPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void btnPartyCancel_Click(object sender, EventArgs e)
    {
        Clear();

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
        ViewState["pNo"] = null;
        ViewState["action"] = "add";
        txtGST.Text = string.Empty;
        txtPanNumber.Text = string.Empty;

    }

    protected void dpPagerParty_PreRender(object sender, EventArgs e)
    {
        BindListViewParty();
    }

    protected void FillCategory()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "SPRT_PARTY_CATEGORY", "PCNO", "PCNAME", "", "PCNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.FillCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillCity()
    {
        try
        {
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO <> 0", "CITY");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.FillCity-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillState()
    {
        try
        {
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO <> 0", "STATENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.FillState-> " + ex.Message + " " + ex.StackTrace);
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
        ds = objKCon.GenratePartyCode(pcno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtVendorCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["VENDORNO"].ToString());
        }
    }

    protected void tab_activetabindexchanged(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
    }

    #endregion

    // This method is used to check the page authority.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    // Vendor report
    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("VendorList", "VendorList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=" + reportTitle + ".pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // Vendor Category Report
    protected void btnCatReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("VendorCategoryList", "VendorCategoryList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}