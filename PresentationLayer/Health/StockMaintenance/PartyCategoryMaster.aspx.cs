//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Stock Maintenance)     
// CREATION DATE : 26-FEB-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class Health_StockMaintenance_PartyCategoryMaster : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common(); 
    StockMaster objStock = new StockMaster();
    StockMaintnance objSController = new StockMaintnance();

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
            }
            //Set vendor category Report Parameters
            //objCommon.ReportPopUp(btnshowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Vendor_Category_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() , "UAIMS");
            //Set vendor Master Report Parameters
            //objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Vendor_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() , "UAIMS");
        }
        divMsg.InnerHtml = string.Empty;
    }

    #region VENDOR_CATEGORY

    private void BindListViewPartyCategory()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllParyCategory();
            ds = objCommon.FillDropDown("HEALTH_PARTY_CATEGORY", "PCNAME,PCSHORTNAME", "PCNO", "", "");
            lvCategoryName.DataSource = ds;
            lvCategoryName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.BindListViewPartyCategory-> " + ex.Message + " " + ex.StackTrace);
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
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_PARTY_CATEGORY", " count(*)", "PCNAME= '" + Convert.ToString(txtCategoryName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objSController.AddPartyCategory(txtCategoryName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString(), txtCategoryShortName.Text);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtCategoryName.Text = string.Empty;
                            txtCategoryShortName.Text = string.Empty;
                            MessageBox("Record Saved successfully");
                            BindListViewPartyCategory();
                            FillCategory();
                        }
                    }
                    else
                    {
                        MessageBox("Record already exist");
                    }
                }
                else
                {
                    if (ViewState["cNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_PARTY_CATEGORY", " count(*)", "PCNAME= '" + Convert.ToString(txtCategoryName.Text) + "' and PCNO <> " + Convert.ToInt32(ViewState["cNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus cs = (CustomStatus)objSController.UpdatePartyCategory(txtCategoryName.Text, Convert.ToInt32(ViewState["cNo"].ToString()), Session["colcode"].ToString(), Session["userfullname"].ToString(), txtCategoryShortName.Text);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                txtCategoryName.Text = string.Empty;
                                txtCategoryShortName.Text = string.Empty;
                                ViewState["action"] = "add";
                                MessageBox("Record Updated successfully");
                                BindListViewPartyCategory();
                                FillCategory();
                            }
                        }
                        else
                        {
                            MessageBox("Record already exist");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.butCategoryName_Click-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.btnEditPartyCategory_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsPartyCategory(int cNo)
    {
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("HEALTH_PARTY_CATEGORY", "PCNAME,PCSHORTNAME", "PCNO", "PCNO=" + cNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCategoryName.Text = ds.Tables[0].Rows[0]["PCNAME"].ToString();
                txtCategoryShortName.Text = ds.Tables[0].Rows[0]["PCSHORTNAME"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.ShowEditDetailsPartyCategory-> " + ex.Message + " " + ex.StackTrace);
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

        //Response.Redirect(Request.Url.ToString());
        txtCategoryName.Text = string.Empty;
        txtCategoryShortName.Text = string.Empty;
        ViewState["cNo"] = null;
        ViewState["action"] = "add";
    }

    //protected void dpPagerCategoryName_PreRender(object sender, EventArgs e)
    //{
    //    BindListViewPartyCategory();
    //}

    #endregion

    #region VENDOR

    private void BindListViewParty()
    {
        try
        {
            //FillCategory(); 
            DataSet ds = objSController.GetAllParty(Session["colcode"].ToString());
            lvParty.DataSource = ds;
            lvParty.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.BindListViewParty-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnParty_Click(object sender, EventArgs e)
    {
        try
        {

            objStock.PCODE = txtVendorCode.Text;
            objStock.PNAME = txtVendorName.Text;
            objStock.PHONE = txtPhone.Text;
            objStock.EMAIL = txtEmail.Text;
            objStock.STATENO = Convert.ToInt32(ddlState.SelectedValue);
            objStock.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            objStock.ADDRESS = txtAddress.Text;
            objStock.PCNO = Convert.ToInt32(ddlCategory.SelectedValue);
            objStock.CST = txtCST.Text;
            objStock.BST = txtBST.Text;
            objStock.REMARK = txtRemark.Text;
            objStock.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_PARTY", " count(*)", "PCODE='" + Convert.ToString(txtVendorCode.Text) + " ' "));

                    if (duplicateCkeck == 0)
                    {

                        CustomStatus cs = (CustomStatus)objSController.AddParty(objStock, Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Save Successfully");
                            BindListViewParty();
                            Clear();
                        }
                    }
                    else
                    {
                        MessageBox("Record already exist");
                    }
                }
                else
                {

                    if (ViewState["pNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_PARTY", " count(*)", "PCODE= '" + Convert.ToString(txtVendorCode.Text) + "' and PNO<> " + Convert.ToInt32(ViewState["pNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            objStock.PNO = Convert.ToInt32(ViewState["pNo"].ToString());
                            CustomStatus cs = (CustomStatus)objSController.UpdateParty(objStock, Session["userfullname"].ToString());
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                MessageBox("Record Updated Successfully");
                                BindListViewParty();
                                Clear();
                            }
                        }
                        else
                        {
                            MessageBox("Record already exist");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.butParty_Click-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.btnEditParty_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsParty(int pNo)
    {
        DataSet ds = null;
        try
        {
            ds = objSController.GetSingleRecordParty(pNo);
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
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.ShowEditDetailsPartyCategory-> " + ex.Message + " " + ex.StackTrace);
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

    }

    protected void dpPagerParty_PreRender(object sender, EventArgs e)
    {
        BindListViewParty();
    }

    protected void FillCategory()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "HEALTH_PARTY_CATEGORY", "PCNO", "PCNAME", "", "PCNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.FillCategory-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.FillCity-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillState()
    {
        try
        {
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "", "STATENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_PartyCategoryMaster.FillState-> " + ex.Message + " " + ex.StackTrace);
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
        ds = objSController.GenratePartyCode(pcno);
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


    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        //objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Vendor_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString(), "UAIMS");
        ShowReport("Vendor", "Vendor_List_Report.rpt");
    }

    protected void btnshowrpt1_Click(object sender, EventArgs e)
    {
        //objCommon.ReportPopUp(btnshowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Vendor_Category_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString(), "UAIMS");
        ShowReport("VendorCategory", "Vendor_Category_report.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HEALTH")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            // url += "&param=@P_DRID=0,username=" + Session["userfullname"].ToString() + ",IP_ADDRESS=" + IPAddress + ",MAC_ADDRESS=" + MacAddress + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            ScriptManager.RegisterClientScriptBlock(updatePanel3, updatePanel3.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Master_DoctorMaster.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void dpVendorCategory_PreRender(object sender, EventArgs e)
    {
        BindListViewPartyCategory();
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}