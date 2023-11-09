using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class HOSTEL_GATEPASS_Purpose : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Purpose objPurpose = new Purpose();
    PurposeController objP = new PurposeController();

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
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                BindListView();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Master_PurposeMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Page Events

    #region Action
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objPurpose.PurposeName = txtPurposeName.Text.Trim();
            objPurpose.IsActive = chkIsActive.Checked;
            objPurpose.CollegeCode = Session["colcode"].ToString();
            objPurpose.organizationid = Session["OrgId"].ToString();

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                        return;
                    }
                    //Add Purpose
                    CustomStatus cs = (CustomStatus)objP.Insert_Update_Purpose(objPurpose);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["purpose_no"] != null)
                    {
                        objPurpose.PurposeNo = Convert.ToInt32(ViewState["purpose_no"].ToString());

                        if (CheckDuplicateEntry() == true)
                        {
                            objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                            return;
                        }
                        //Edit Purpose

                        CustomStatus cs = (CustomStatus)objP.Insert_Update_Purpose(objPurpose);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                        }
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        btnSubmit.Text = "Submit";
        chkIsActive.Checked = true;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            ImageButton btnEdit = sender as ImageButton;
            int purpose_no = int.Parse(btnEdit.CommandArgument);
            ShowDetail(purpose_no);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Purpose.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Action

    #region Private Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Purpose.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Purpose.aspx");
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string purname = objCommon.LookUp("ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME='" + txtPurposeName.Text + "'  and  ISACTIVE ='" + Convert.ToInt32(chkIsActive.Checked) + "'");
            if (purname != null && purname != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Purpose.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int purpose_no)
    {
        bool flag = false;
        try
        {
            string purposeno = objCommon.LookUp("ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NAME", "PURPOSE_NAME='" + txtPurposeName.Text + "';");
            if (purpose_no != null && purpose_no != 0)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Purpose.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void Clear()
    {
        txtPurposeName.Text = string.Empty;
        chkIsActive.Checked = false;
        ViewState["action"] = "add";
        BindListView();
    }

    private void BindListView()
    {
            try
            {
                DataSet ds = objP.GetAllPurpose();
                lvPurpose.DataSource = ds;
                lvPurpose.DataBind();
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelPurpose.BindListView --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server UnAvailable");
            }
    }

    private void ShowDetail(int purpose_no)
    {
        SqlDataReader dr = objP.GetPurpose(purpose_no);

        //Show Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["purpose_no"] = purpose_no.ToString();
                txtPurposeName.Text = dr["PURPOSE_NAME"] == null ? string.Empty : dr["PURPOSE_NAME"].ToString();
                chkIsActive.Checked = Convert.ToBoolean(dr["ISACTIVE"]);
            }
        }
        if (dr != null) dr.Close();
    }
    #endregion Private Methods
}