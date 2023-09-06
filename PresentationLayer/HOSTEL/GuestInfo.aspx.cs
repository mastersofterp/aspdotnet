//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : GUEST INFO                                                           
// CREATION DATE : 9-DEC-2009                                                          
// CREATED BY    : GAURAV SONI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Hostel_GuestInfo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    GuestInfoController objGuestInfoController = new GuestInfoController();

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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    divSerchstaff.Visible = false;
                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddlResidentType, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "IS_STUDENT !=1", "RESIDENT_TYPE_NAME");  //"IS_STUDENT !=1" this condition added by Saurabh L on 14/09/2022
             
                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                }
                this.ShowAllGuestInfo();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
        private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GuestInfo.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GuestInfo.aspx");
        }
    }
    #endregion

    #region Actions
            protected void btnSubmit_Click(object sender, EventArgs e)
            {
                try
                {
                    GuestInfo objGuestInfo = this.BindDataFromControls();

                    /// check form action whether add or update
                    if (ViewState["action"] != null)
                    {
                        CustomStatus cs = new CustomStatus();

                        /// Add GuestInfo
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            if (CheckDuplicateEntry() == true)
                            {
                                objCommon.DisplayMessage("Record Already Exist.", this.Page);
                                return;
                            }
                            cs = (CustomStatus)objGuestInfoController.AddGuestInfo(objGuestInfo);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                                this.cleartxt();
                            }
                        }
                        /// Update GuestInfo
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            int guestno = Convert.ToInt32(ViewState["GuestNo"].ToString());
                            objGuestInfo.GuestNo = (GetViewStateItem("GuestNo") != string.Empty ? int.Parse(GetViewStateItem("GuestNo")) : 0);
                            if (CheckDuplicateEntryUpdate(guestno) == true)
                            {
                                objCommon.DisplayMessage("Record Already Exist.", this.Page);
                                return;
                            }
                            cs = (CustomStatus)objGuestInfoController.UpdateGuestInfo(objGuestInfo);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                                this.cleartxt();
                            }
                            ViewState["action"] = "add";
                            btnSubmit.Text = "SUBMIT";
                            ddlResidentType.Enabled = true;
                            ddlSearchStaff.Enabled = true;
                            txtGuestName.Enabled = true;
                        }

                        if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                            this.ShowMessage("Unable to complete the operation.");
                        else
                            this.ShowAllGuestInfo();
                    }
                    this.ClearControlContents();
                    this.cleartxt();
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUaimsCommon.ShowError(Page, "Server Unavailable");
                }
            }

            protected void btnCancel_Click(object sender, EventArgs e)
            {
                Response.Redirect(Request.Url.ToString());
            }

            protected void btnEdit_Click(object sender, EventArgs e)
            {
                btnSubmit.Text = "UPDATED";
                ddlResidentType.Enabled = false;
                divSerchstaff.Visible = false;
                txtGuestName.Enabled = false;
                try
                {
                    ImageButton editButton = sender as ImageButton;
                    int guestNo = Int32.Parse(editButton.CommandArgument);

                    DataSet ds = objGuestInfoController.GetGuestInfoByNo(guestNo);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        BindDataToControls(ds.Tables[0].Rows[0]);

                        ViewState["action"] = "edit";
                        ViewState["GuestNo"] = ds.Tables[0].Rows[0]["GUEST_NO"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUaimsCommon.ShowError(Page, "Server Unavailable");
                }
            }
            private void BindDataToControls(DataRow dr)
          {
        try
        {
            if (dr["RESIDENT_TYPE_NO"].ToString() != null &&
                ddlResidentType.Items.FindByValue(dr["RESIDENT_TYPE_NO"].ToString()) != null)
                ddlResidentType.SelectedValue = dr["RESIDENT_TYPE_NO"].ToString();

            if (dr["GUEST_NAME"].ToString() != null)
                txtGuestName.Text = dr["GUEST_NAME"].ToString();

            if (dr["GUEST_ADDRESS"].ToString() != null)
                txtGuestAddress.Text = dr["GUEST_ADDRESS"].ToString();
            
            if (dr["CONTACT_NO"].ToString() != null)
                txtContactNo.Text = dr["CONTACT_NO"].ToString();
            
            if (dr["PURPOSE"].ToString() != null)
                txtPurpose.Text = dr["PURPOSE"].ToString();

            if (dr["COMPANY_NAME"].ToString() != null)
                txtCompanyName.Text = dr["COMPANY_NAME"].ToString();
            
            if (dr["COMPANY_ADDRESS"].ToString() != null)
                txtCompanyAddress.Text = dr["COMPANY_ADDRESS"].ToString();
            
            if (dr["COMPANY_CONTACT_NO"].ToString() != null)
                txtCContactNo.Text = dr["COMPANY_CONTACT_NO"].ToString();
            
            if (dr["FROM_DATE"].ToString() != null)
                txtFromDate.Text = dr["FROM_DATE"].ToString();
            
            if (dr["TO_DATE"].ToString() != null)
                txtToDate.Text = dr["TO_DATE"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.BindDataToControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

            private GuestInfo BindDataFromControls()
           {
        GuestInfo objGuestInfo = new GuestInfo();
        try
        {
            if (ddlResidentType.SelectedValue != null && ddlResidentType.SelectedIndex > 0)
                objGuestInfo.ResidentTypeNo = (ddlResidentType.SelectedValue != string.Empty ? int.Parse(ddlResidentType.SelectedValue) : 0);
                objGuestInfo.GuestName = txtGuestName.Text.Trim();
                objGuestInfo.GuestAddress = txtGuestAddress.Text.Trim();
                objGuestInfo.ContactNo = txtContactNo.Text.Trim();
                objGuestInfo.Purpose = txtPurpose.Text.Trim();
                objGuestInfo.CompanyName = txtCompanyName.Text.Trim();
                objGuestInfo.CompanyAddress = txtCompanyAddress.Text.Trim();
                objGuestInfo.CompanyContactNo = txtCContactNo.Text.Trim();
                objGuestInfo.FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                objGuestInfo.ToDate = Convert.ToDateTime(txtToDate.Text.Trim());
                objGuestInfo.CollegeCode = Session["colcode"].ToString();
                objGuestInfo.OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.BindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
        return objGuestInfo;
    }
    #endregion
    
    #region Private Methods
    private void ShowAllGuestInfo()
    {
        try
        {
            DataSet ds = objGuestInfoController.GetAllGuestInfo();
            lvGuest.DataSource = ds;
            lvGuest.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.ShowAllGuestInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void dpGuest_PreRender(object sender, EventArgs e)
    {
        ShowAllGuestInfo();
    }

    private void ClearControlContents()
    {
        ClearControlsRecursive(Page);
    }
    private void ClearControlsRecursive(Control root)
    {
        if (root is TextBox)
        {
            ((TextBox)root).Text = string.Empty;
        }
        if (root is DropDownList)
        {
            ((DropDownList)root).SelectedIndex = 0;
        }
        foreach (Control child in root.Controls)
        {
            ClearControlsRecursive(child);
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
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

    //added by shubham
    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            //string Guestno = objCommon.LookUp("ACD_HOSTEL_GUEST_INFO", "GUEST_NAME", "GUEST_NAME = '" + txtGuestName.Text + "' AND CONTACT_NO= '" + txtContactNo.Text + "' OR COMPANY_NAME='" + txtCompanyName.Text + "' OR COMPANY_CONTACT_NO= '" + txtCContactNo.Text + "'");
            string Guestno = objCommon.LookUp("ACD_HOSTEL_GUEST_INFO", "GUEST_NAME", "(GUEST_NAME = '" + txtGuestName.Text + "' AND CONTACT_NO= '" + txtContactNo.Text + "' AND COMPANY_NAME='" + txtCompanyName.Text + "' AND COMPANY_CONTACT_NO= '" + txtCContactNo.Text + "')OR (GUEST_NAME = '" + txtGuestName.Text + "' AND CONTACT_NO= '" + txtContactNo.Text + "') OR (COMPANY_NAME='" + txtCompanyName.Text + "' AND COMPANY_CONTACT_NO= '" + txtCContactNo.Text + "')");
            if (Guestno != null && Guestno != string.Empty)
            {
                flag = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int guestno)
    {
        bool flag = false;
        try
        {
            //string Guestno = objCommon.LookUp("ACD_HOSTEL_GUEST_INFO", "GUEST_NAME", "GUEST_NAME = '" + txtGuestName.Text + "' AND CONTACT_NO= '" + txtContactNo.Text + "' OR COMPANY_NAME='" + txtCompanyName.Text + "' OR COMPANY_CONTACT_NO= '" + txtCContactNo.Text + "'");
            string Guestno = objCommon.LookUp("ACD_HOSTEL_GUEST_INFO", "GUEST_NAME", "GUEST_NO !=" + guestno.ToString() + " and ((GUEST_NAME = '" + txtGuestName.Text + "' AND CONTACT_NO= '" + txtContactNo.Text + "' AND COMPANY_NAME='" + txtCompanyName.Text + "' AND COMPANY_CONTACT_NO= '" + txtCContactNo.Text + "')OR (GUEST_NAME = '" + txtGuestName.Text + "' AND CONTACT_NO= '" + txtContactNo.Text + "') OR (COMPANY_NAME='" + txtCompanyName.Text + "' AND COMPANY_CONTACT_NO= '" + txtCContactNo.Text + "'))");
            if (Guestno != null && Guestno != string.Empty)
            {
                flag = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    #endregion
    
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != string.Empty && txtFromDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtToDate.Text) > Convert.ToDateTime(txtFromDate.Text))
            {

            }
            else
            {
                objCommon.DisplayMessage("To Date should be Greater than From Date.", this.Page);
                txtToDate.Text = string.Empty;
                txtToDate.Focus();
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage("Please select from date related with selected Month.", this.Page);
            txtToDate.Text = string.Empty;
            txtToDate.Focus();
            return;
        }
    }
    //Added By Preeti A date 09/03/23
    protected void ddlResidentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.cleartxt();
        string var = ddlResidentType.SelectedItem.Text;
        if (var == "Staff")
        {
            divSerchstaff.Visible = true;
            ddlSearchStaff.Focus();
            this.objCommon.FillDropDownList(ddlSearchStaff, "User_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (3,5)", "UA_FULLNAME");      
        }
        else
        {
            divSerchstaff.Visible = false;
        }
    }
   
    protected void ddlSearchStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedStaffUA_NO = 0;

        if (ddlSearchStaff.SelectedValue.ToString() != "")
            selectedStaffUA_NO = Convert.ToInt32(ddlSearchStaff.SelectedValue);
        else
            objCommon.DisplayMessage("UA NO not get of Selected staff user.", this.Page);

        string selectedStaffName = objCommon.LookUp("User_Acc", "UA_FULLNAME", "UA_NO=" + selectedStaffUA_NO);

        txtGuestName.Text = selectedStaffName;
        txtCompanyName.Text = "Crescent";
        txtCompanyAddress.Text = "Crescent college ,120 Seethakathi Estate, Grand Southern Trunk Rd, Vandalur, Tamil Nadu 600048";
        txtGuestAddress.Text = txtCompanyAddress.Text;
        txtContactNo.Text = objCommon.LookUp("User_Acc", "UA_MOBILE", "UA_NO=" + selectedStaffUA_NO);
        txtCContactNo.Text = txtContactNo.Text;
        txtPurpose.Text = "Staff Room Allotted.";
    }
    
    public void cleartxt()
    {
        txtToDate.Text = "";
        txtFromDate.Text = "";
        txtCContactNo.Text = "";
        txtContactNo.Text = "";
        txtCompanyName.Text = "";
        txtGuestName.Text = "";
        txtPurpose.Text = "";
        txtCompanyAddress.Text = "";
    }
    //END 
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text == string.Empty)
        {
            string var = ddlResidentType.SelectedItem.Text;
            if (var == "Staff")
            {
                DateTime d3 = Convert.ToDateTime(txtFromDate.Text);
                DateTime d2 = d3.AddYears(1);
                txtToDate.Text = d2.ToString();
            }
        }
        else
        {
        }
        //Added By Preeti A date 09/03/23
        if (txtToDate.Text != string.Empty && txtFromDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
            {
                objCommon.DisplayMessage("From Date should be less than To Date.", this.Page);
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
                return;
            }
        }
        else if (txtToDate.Text != string.Empty)
        {
        }
        else if (txtToDate.Text == string.Empty)
        {
        }
        else
        {
            objCommon.DisplayMessage("Please select from date related with selected Month.", this.Page);
            txtFromDate.Text = string.Empty;
            txtFromDate.Focus();
            return;
        }      
    }
}
