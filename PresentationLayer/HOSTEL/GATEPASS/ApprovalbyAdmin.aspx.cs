using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;

public partial class HOSTEL_GATEPASS_ApprovalbyAdmin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdminApprovalController objAAC = new AdminApprovalController();

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
                }
                
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Master_GatePassRequest.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Page Events

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ApprovalbyAdmin.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ApprovalbyAdmin.aspx");
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            bool isChecked = false;
            foreach (ListViewDataItem item in lvGatePass.Items)
            {
                CheckBox Ischeck = item.FindControl("chkApprove") as CheckBox;
                if (Ischeck.Checked)
                {
                    isChecked = true;
                    break;
                }
            }

            if (isChecked)
            {
                ViewState["action"] = "checked";
            }
            else
            {
                objCommon.DisplayMessage("Please select at least one record.", this.Page);
                ViewState["action"] = "not_checked";
            }

            if (ViewState["action"] == "checked")
            {
                int Approve = 0;
                int recid = 0;
                CustomStatus cs = new CustomStatus();
                foreach (ListViewDataItem item in lvGatePass.Items)
                {
                    CheckBox chkApprove = item.FindControl("chkApprove") as CheckBox;
                    HiddenField hidrecid = item.FindControl("hidrecid") as HiddenField;

                    if (chkApprove.Checked)
                    {
                        Approve = 1;
                    }
                    else
                    {
                        Approve = 0;
                    }

                    string Remark = txtRemark.Text;

                    recid = Convert.ToInt16(hidrecid.Value);
                    cs = (CustomStatus)objAAC.UpdateApproval(recid, Approve, Remark);
                }

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage("Direct approval for hostel gate pass done successfully.", this.Page);
                    Clear();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objAAC.GetAllGatePass();
            lvGatePass.DataSource = ds;
            lvGatePass.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelPurpose.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtRemark.Text = string.Empty;

        foreach (ListViewItem item in lvGatePass.Items)
        {
            CheckBox chkApprove = (CheckBox)item.FindControl("chkApprove");
            if (chkApprove != null)
            {
                chkApprove.Checked = false;
            }
        }
    }

    protected void lvGatePass_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            // Find the controls within the ListViewItem
            CheckBox chkApprove = (CheckBox)e.Item.FindControl("chkApprove");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");

            if (lblStatus != null && lblStatus.Text == "APPROVED")
            {
                chkApprove.Enabled = false;
            }
            else
            {
                chkApprove.Enabled = true;
            }
        }
    }
}