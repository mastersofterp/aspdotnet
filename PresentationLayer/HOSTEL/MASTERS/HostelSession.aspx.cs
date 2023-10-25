//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL SESSION MASTER                                                
// CREATION DATE : 25-NOV-2010                                                          
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Hostel_Masters_HostelSession : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelSessionController HSController = new HostelSessionController();

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

                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                }
            }
            this.ShowAllSession();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=HostelSession.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelSession.aspx");
        }
    }

    #endregion    
    
    #region Actions
    private void ShowAllSession()
    {
        try
        {
            DataSet ds = HSController.GetAllHostelSession();
            lvSession.DataSource = ds;
            lvSession.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelSession.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControlContents()
    {
        txtSessionName.Text = string.Empty;
        txtSessionStart.Text = string.Empty;
        txtSessionEnd.Text = string.Empty;
        chkIsShow.Checked = true;
        ViewState["action"] = "add";
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

    //protected void dpSession_PreRender(object sender, EventArgs e)
    //{
    //    ShowAllSession();
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int hostelSessionNo = 0;
            DateTime seesionStart = DateTime.Parse(txtSessionStart.Text.Trim());
            DateTime sessionEnd = DateTime.Parse(txtSessionEnd.Text.Trim());
            string sessionName = txtSessionName.Text.Trim();
            string collegeCode = Session["colcode"].ToString();
            int active = Convert.ToInt32(rdoActive.SelectedValue);
            int isshow = Convert.ToInt32(chkIsShow.Checked);

            if (sessionEnd < seesionStart)
            {
                ShowMessage("Session End Date is not greater than Session Start Date.");
                return;
            }
            
            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add Hostel Session
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                        return;
                    }

                    cs = (CustomStatus)HSController.AddHostelSession(sessionName, seesionStart, sessionEnd, collegeCode, active,isshow);
                   
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ShowMessage("Record Saved Successfully!!!.");
                    }
                }

                /// Update Asset
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    hostelSessionNo = (GetViewStateItem("HOSTEL_SESSION_NO") != string.Empty ? int.Parse(GetViewStateItem("HOSTEL_SESSION_NO")) : 0);
                    if (CheckDuplicateEntryUpdate(hostelSessionNo) == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);//Added by Saurabh L on 24/05/2022  CheckDuplicateEntryUpdate() function
                        return;
                    }
                    cs = (CustomStatus)HSController.UpdateHostelSession(sessionName, seesionStart, sessionEnd, hostelSessionNo, active, isshow);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ShowMessage("Record Updated Successfully!!!.");
                    }
                }

                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");
                else
                    this.ShowAllSession();
            }
            this.ClearControlContents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //this.ClearControlContents();
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int hostelSessionNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = HSController.GetHostelSessionByNo(hostelSessionNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtSessionName.Text = dr["SESSION_NAME"] == null ? string.Empty : dr["SESSION_NAME"].ToString();
                txtSessionStart.Text = dr["START_DATE"] == null ? string.Empty : dr["START_DATE"].ToString();
                txtSessionEnd.Text = dr["END_DATE"] == null ? string.Empty : dr["END_DATE"].ToString();
                rdoActive.SelectedValue = dr["FLOCK"].ToString();
                chkIsShow.Checked = Convert.ToBoolean(dr["IS_SHOW"]);
                ViewState["action"] = "edit";
                ViewState["HOSTEL_SESSION_NO"] = dr["HOSTEL_SESSION_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnEdit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {

            string sessionon = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME='" + txtSessionName.Text.Trim() + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Shubham B on 15/09/2022
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

    private bool CheckDuplicateEntryUpdate(int sessionid)
    {
        bool flag = false;
        try
        {
            string sessionon = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "HOSTEL_SESSION_NO !=" + sessionid + " AND SESSION_NAME ='" + txtSessionName.Text.Trim() + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Shubham B on 15/09/2022
            if (sessionon != null && sessionon != string.Empty)
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

    // Date Validation on Slient Side added by shubham On 28/10/2022 (Bugfix)
    protected void txtSessionEnd_TextChanged(object sender, EventArgs e)
    {
        if (txtSessionStart.Text != string.Empty && txtSessionEnd.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtSessionEnd.Text) < Convert.ToDateTime(txtSessionStart.Text))
            {
                objCommon.DisplayMessage("Session End Date should be greater than Session Start Date.", this.Page);
                txtSessionEnd.Text = string.Empty;
                txtSessionEnd.Focus();
                return;
            }
        }
        else if (txtSessionStart.Text == string.Empty && txtSessionEnd.Text != string.Empty)
        {
            objCommon.DisplayMessage("Please Select Session Start Date.", this.Page);
            txtSessionStart.Text = string.Empty;
            txtSessionStart.Focus();
            return;
        }
    }


    protected void txtSessionStart_TextChanged(object sender, EventArgs e)
    {
        if (txtSessionStart.Text != string.Empty && txtSessionEnd.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtSessionStart.Text) > Convert.ToDateTime(txtSessionEnd.Text))
            {
                objCommon.DisplayMessage("Session Start Date should be Less than Session End Date.", this.Page);
                txtSessionStart.Text = string.Empty;
                txtSessionStart.Focus();
                return;
            }
        }
    }
}
