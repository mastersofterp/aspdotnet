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

public partial class HOSTEL_GATEPASS_HostelGatePassRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    GatePassRequest objGatePass = new GatePassRequest();
    GatePassRequestController objGPR = new GatePassRequestController();

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
                PopulateDropDownList();
                BindListView();
                ViewState["action"] = "add";
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

    #region Action
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objGatePass.OutDate = DateTime.Parse(txtoutDate.Text.Trim());
            objGatePass.OutHourFrom = Convert.ToInt32(txtoutHourFrom.Text);
            objGatePass.OutMinFrom = Convert.ToInt32(txtoutMinFrom.Text.Trim());
            objGatePass.OutAMPM = DropDownList1.SelectedItem.Text;
            objGatePass.InDate = DateTime.Parse(txtinDate.Text.Trim());
            objGatePass.InHourFrom = Convert.ToInt32(txtinHourFrom.Text.Trim());
            objGatePass.InMinFrom = Convert.ToInt32(txtinMinFrom.Text.Trim());
            objGatePass.InAMPM = DropDownList2.SelectedItem.Text;
            objGatePass.PurposeID = Convert.ToInt32(ddlPurpose.SelectedIndex.ToString());
            objGatePass.PurposeOther = txtOther.Text.Trim();
            objGatePass.Remarks = txtRemark.Text;
            objGatePass.IDNO = Convert.ToInt32(Session["idno"]);
            objGatePass.CollegeCode = Session["colcode"].ToString();
            objGatePass.organizationid = Session["OrgId"].ToString();

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
                    
                    CustomStatus cs = (CustomStatus)objGPR.Insert_Update_GatePassRequest(objGatePass);
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
                    if (ViewState["gatepass_no"] != null)
                    {
                        objGatePass.GatePassNo = Convert.ToInt32(ViewState["gatepass_no"].ToString());

                        if (CheckDuplicateEntry() == true)
                        {
                            objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                            return;
                        }

                        CustomStatus cs = (CustomStatus)objGPR.Insert_Update_GatePassRequest(objGatePass);
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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            ImageButton btnEdit = sender as ImageButton;
            int gatepass_no = int.Parse(btnEdit.CommandArgument);
            ShowDetail(gatepass_no);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "GatePass.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPurpose.SelectedItem.Text.ToUpper() == "OTHERS")
        {
            txtOther.Visible = true;
        }
        else
        {
            txtOther.Visible = false;
        }
    }
    #endregion Action

    #region Private Methods
    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlPurpose, "ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME", "ISACTIVE=1", "PURPOSE_NO");
            ddlPurpose.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string gpr = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "HGP_ID", "OUTDATE=" + txtoutDate.Text + " and  IDNO ='" + Convert.ToInt32(Session["idno"]) + "'");
            if (gpr != null && gpr != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "GatePassRequest.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    private void Clear()
    {
        btnSubmit.Text = "Submit";

        txtoutDate.Text = string.Empty;
        txtoutHourFrom.Text = string.Empty;
        txtoutMinFrom.Text = string.Empty;
        DropDownList1.SelectedIndex = 0;
        txtinDate.Text = string.Empty;
        txtinHourFrom.Text = string.Empty;
        txtinMinFrom.Text = string.Empty;
        DropDownList2.SelectedIndex = 0;
        ddlPurpose.SelectedIndex = 0;
        txtOther.Text = string.Empty;
        txtRemark.Text = string.Empty;
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objGPR.GetAllGatePass();
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

    private void ShowDetail(int gatepass_no)
    {
        SqlDataReader dr = objGPR.GetGatePass(gatepass_no);

        //Show Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["gatepass_no"] = gatepass_no.ToString();
                txtoutDate.Text = dr["OUTDATE"] == null ? string.Empty : dr["OUTDATE"].ToString();
                txtoutHourFrom.Text = dr["OUT_HOUR_FROM"] == null ? string.Empty : dr["OUT_HOUR_FROM"].ToString();
                txtoutMinFrom.Text = dr["OUT_MIN_FROM"] == null ? string.Empty : dr["OUT_MIN_FROM"].ToString();
                DropDownList1.SelectedItem.Text = dr["OUT_AM_PM"] == null ? string.Empty : dr["OUT_AM_PM"].ToString();
                txtinDate.Text = dr["INDATE"] == null ? string.Empty : dr["INDATE"].ToString();
                txtinHourFrom.Text = dr["IN_HOUR_FROM"] == null ? string.Empty : dr["IN_HOUR_FROM"].ToString();
                txtinMinFrom.Text = dr["IN_MIN_FROM"] == null ? string.Empty : dr["IN_MIN_FROM"].ToString();
                DropDownList1.SelectedItem.Text = dr["IN_AM_PM"] == null ? string.Empty : dr["IN_AM_PM"].ToString();
                ddlPurpose.SelectedItem.Text = dr["PURPOSE_NAME"] == null ? string.Empty : dr["PURPOSE_NAME"].ToString();
                txtOther.Text = dr["PURPOSE_OTHER"] == null ? string.Empty : dr["PURPOSE_OTHER"].ToString();
                txtRemark.Text = dr["REMARKS"] == null ? string.Empty : dr["REMARKS"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }
    #endregion Private Methods


    
}