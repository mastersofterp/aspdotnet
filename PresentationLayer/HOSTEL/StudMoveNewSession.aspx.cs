using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class HOSTEL_StudMoveNewSession : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelSessionController HSController = new HostelSessionController();

    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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

                    // Fill Dropdown lists 
                    PopulateDropDownList();

                    ViewState["action"] = "add";
                }
            }
            // divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StudMoveNewSession.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudMoveNewSession.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudMoveNewSession.aspx");
        }
    }
    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCurSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1 AND HOSTEL_SESSION_NO IN(SELECT MAX(HOSTEL_SESSION_NO) FROM ACD_HOSTEL_SESSION WHERE FLOCK=1)", "HOSTEL_SESSION_NO desc");

            objCommon.FillDropDownList(ddlCurSession, "ACD_HOSTEL_SESSION", "TOP 2 HOSTEL_SESSION_NO", "SESSION_NAME", " HOSTEL_SESSION_NO > 0", "HOSTEL_SESSION_NO DESC");

            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 ", "HOSTEL_NO");

            //objCommon.FillDropDownList(ddlNewSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK !=1 AND HOSTEL_SESSION_NO IN((SELECT MAX(HOSTEL_SESSION_NO) FROM ACD_HOSTEL_SESSION WHERE FLOCK !=1),(SELECT MAX(HOSTEL_SESSION_NO)+1 FROM ACD_HOSTEL_SESSION WHERE FLOCK=1)) ", "HOSTEL_SESSION_NO desc");

            //objCommon.FillDropDownList(ddlNewSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK !=1 AND HOSTEL_SESSION_NO > (SELECT HOSTEL_SESSION_NO FROM ACD_HOSTEL_SESSION WHERE FLOCK=1) AND END_DATE > GETDATE()", "HOSTEL_SESSION_NO desc");
            objCommon.FillDropDownList(ddlNewSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK = 1 ", "HOSTEL_SESSION_NO DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StudMoveNewSession.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStudents()
    {
        try
        {
            DataSet ds = HSController.GetRoomAllotedStudent(Convert.ToInt32(ddlCurSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue));
            // int pro = 0;
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                // ddlCurSession.Enabled = false;
                ddlHostel.Enabled = false;
                ddlCurSession.Enabled = false;
                //txtFromdate.Enabled = true;
                //txtTodate.Enabled = true;
                ddlNewSession.Enabled = true;
                btnSubmit.Enabled = true;

                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    Label lblStatus = lvItem.FindControl("lblStatus") as Label;
                    HiddenField hdfhost = lvItem.FindControl("hdfhost") as HiddenField;
                    CheckBox chkbx = lvItem.FindControl("chkhostel") as CheckBox;
                    HiddenField hdfIdid = lvItem.FindControl("hdfidno") as HiddenField;

                    if (hdfhost.Value == "1")
                    {
                        chkbx.Checked = true;
                        chkbx.Enabled = false;
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = "DONE";
                    }
                    else
                    {
                        chkbx.Enabled = true;
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "PENDING";
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updPnl, "Students not found.", this.Page);
                Clear();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HOSTEL_StudMoveNewSession.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowStudents();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string userIds = string.Empty;
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkb = lvItem.FindControl("chkhostel") as CheckBox;
                HiddenField hdfIdid = lvItem.FindControl("hdfIdno") as HiddenField;
                HiddenField hdfhost = lvItem.FindControl("hdfhost") as HiddenField;
                if (chkb.Checked == true && hdfhost.Value == "1")
                {

                }
                else if (chkb.Checked == true)
                {
                    userIds += hdfIdid.Value.ToString() + ",";
                }
                else { }
            }
            if (userIds != "")
            {
                if (userIds.Substring(userIds.Length - 1) == ",")
                    userIds = userIds.Substring(0, userIds.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updPnl, "Please select at least one student.", this.Page);
                return;
            }
            int cs = HSController.PromoteStudentNewSession(Convert.ToInt32(ddlCurSession.SelectedValue), Convert.ToInt32(ddlNewSession.SelectedValue), userIds.ToString().TrimEnd(','), Convert.ToDateTime(txtFromdate.Text.Trim()), Convert.ToDateTime(txtTodate.Text.Trim()), Convert.ToInt32(Session["userno"]));

            if (cs == 1)
            {
                objCommon.DisplayMessage(this.updPnl, "New Session Promoted Done Successfully.", this.Page);
                ddlNewSession.SelectedIndex = 0;
                txtFromdate.Text = "";
                txtTodate.Text = "";
                ddlNewSession.Enabled = false;
                txtFromdate.Enabled = false;
                txtTodate.Enabled = false;
            }
            else
            {
                objCommon.DisplayMessage(this.updPnl, "Error Occured Session Promotion.", this.Page);
                return;
            }
            this.ShowStudents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HOSTEL_StudMoveNewSession.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        lvStudents.Visible = false;
        lvStudents.DataSource = null;
        ddlCurSession.SelectedIndex = 0;
        ddlHostel.SelectedIndex = 0;
        ddlNewSession.SelectedIndex = 0;
        txtFromdate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        ddlCurSession.Enabled = true;
        ddlHostel.Enabled = true;
        ddlNewSession.Enabled = false;
        txtTodate.Enabled = false;
        txtFromdate.Enabled = false;
        btnSubmit.Enabled = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {          
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StudMoveNewSession.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlNewSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNewSession.SelectedIndex > 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_SESSION", "CONVERT(VARCHAR,START_DATE,103)STARTDATE", "CONVERT(VARCHAR,END_DATE,103)ENDDATE", "HOSTEL_SESSION_NO=" + ddlNewSession.SelectedValue + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFromdate.Text = ds.Tables[0].Rows[0]["STARTDATE"].ToString();
                txtTodate.Text = ds.Tables[0].Rows[0]["ENDDATE"].ToString();
                txtFromdate.Enabled = false;
                txtTodate.Enabled = false;
            }
        }
        else
        {
            txtFromdate.Enabled = true;
            txtTodate.Enabled = true;
            txtFromdate.Text = string.Empty;
            txtTodate.Text = string.Empty;
        }
    }
}