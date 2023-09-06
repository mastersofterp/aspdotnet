//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : RESIDENT TYPE
// CREATION DATE : 28-NOV-2009
// CREATED BY    : GAURAV S SONI
// MODIFIED BY   : 
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

public partial class Hostel_Masters_ResidentType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ResidentTypeController rtController = new ResidentTypeController();

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
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                }
                this.ShowAllResidentTypes();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_ResidentType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }
    #endregion

    #region Actions
    private void ShowAllResidentTypes()
    {
        try
        {
            DataSet ds = rtController.GetAllResidentTypes();
            lvResidentType.DataSource = ds;
            lvResidentType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_ResidentType.ShowAllRooms() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void dpRooms_PreRender(object sender, EventArgs e)
    {
        this.ShowAllResidentTypes();
    }

    private void ClearControlContents()
    {
        txtResidentType.Text = string.Empty;
        chkIsStudent.Checked = false;
        this.ShowAllResidentTypes();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string residentType = txtResidentType.Text.Trim();
            bool isStudent = chkIsStudent.Checked;
            string collegeCode = Session["colcode"].ToString();
            
            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add Resident Type
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                        return;
                    }
                    cs = (CustomStatus)rtController.AddResidentType(residentType, isStudent, collegeCode);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ShowMessage("Record Saved Successfully!!!");
                    }
                }

                /// Update Resident Type
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    int residentTypeNo = (GetViewStateItem("ResidentTypeNo") != string.Empty ? int.Parse(GetViewStateItem("ResidentTypeNo")) : 0);
                    if (CheckDuplicateEntryUpdate(residentTypeNo) == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);//Added by Saurabh L on 24/05/2022  CheckDuplicateEntryUpdate() function
                        return;
                    }
                    cs = (CustomStatus)rtController.UpdateResidentType(residentTypeNo, residentType, isStudent);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ShowMessage("Record Updated Successfully!!!");
                    }
                    ViewState["action"] = "add";
                }

                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");
                else
                    this.ShowAllResidentTypes();
            }
            this.ClearControlContents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_ResidentType.btnAllotRoom_Click() --> " + ex.Message + " " + ex.StackTrace);
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
        try
        {
            ImageButton editButton = sender as ImageButton;
            int residentTypeNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = rtController.GetResidentTypeByNo(residentTypeNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtResidentType.Text = ds.Tables[0].Rows[0]["RESIDENT_TYPE_NAME"].ToString();
                chkIsStudent.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_STUDENT"].ToString());
                ViewState["ResidentTypeNo"] = ds.Tables[0].Rows[0]["RESIDENT_TYPE_NO"].ToString();
                ViewState["action"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_ResidentType.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {

            string sessionon = objCommon.LookUp("ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME='" + txtResidentType.Text.Trim() + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Shubham B on 15/09/2022
            if (sessionon != null && sessionon != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Room.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int Resident)
    {
        bool flag = false;
        try
        {
            string Residentid = objCommon.LookUp("ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NO !=" + Resident + " AND RESIDENT_TYPE_NAME='" + txtResidentType.Text.Trim() + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Shubham B on 15/09/2022
            if (Residentid != null && Residentid != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Room.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    #endregion
}