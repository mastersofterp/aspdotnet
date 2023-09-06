using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ESTABLISHMENT_LEAVES_Master_LocationMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaveName = new LeavesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                BindListViewLocation();

            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";
        btnAdd.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlList.Visible = true;

        btnAdd.Visible = true;

        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        Clear();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Leaves objLeaves = new Leaves();

            bool result = CheckPurpose();

            objLeaves.LOCATION = Convert.ToString(txtLocName.Text);
            objLeaves.LATITUDE = Convert.ToString(txtLatitude.Text);
            objLeaves.LONGITUDE = Convert.ToString(txtLongitude.Text);
            objLeaves.RADIUS = Convert.ToString(txtRadius.Text);
            objLeaves.LOCATION_ADDRESS = Convert.ToString(txtAddress.Text);
            objLeaves.CREATEDBY = Convert.ToInt32(Session["userno"].ToString());
            objLeaves.MODIFIEDBY = Convert.ToInt32(Session["userno"].ToString());
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        MessageBox("Record Already Exist");
                        Clear();
                        BindListViewLocation();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnBack.Visible = false;
                        btnAdd.Visible = true;
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objLeaveName.AddLocation(objLeaves);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            MessageBox("Record Saved Successfully");

                            ViewState["action"] = null;
                            Clear();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;
                            btnAdd.Visible = true;
                            BindListViewLocation();
                        }
                    }
                }
                else
                {
                    if (ViewState["LOCNO"] != null)
                    {
                        objLeaves.LOCNO = Convert.ToInt32(ViewState["LOCNO"].ToString());
                        CustomStatus cs = (CustomStatus)objLeaveName.UpdateLocation(objLeaves);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            MessageBox("Record Updated Successfully");

                            ViewState["action"] = null;
                            Clear();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;
                            btnAdd.Visible = true;
                            BindListViewLocation();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtLocName.Text = string.Empty;
        txtLatitude.Text = string.Empty;
        txtLongitude.Text = string.Empty;
        txtRadius.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ViewState["LOCNO"] = null;
        ViewState["action"] = "add";
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void BindListViewLocation()
    {
        try
        {
            DataSet ds = objLeaveName.GetAllLocationName();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //btnShowReport.Visible = false;
                lvLocation.DataSource = ds;
                lvLocation.DataBind();
            }
            else
            {
                //btnShowReport.Visible = true;
                lvLocation.DataSource = null;
                lvLocation.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int LOCNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(LOCNO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

            btnAdd.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 LOCNO)
    {
        DataSet ds = null;
        try
        {
            ds = objLeaveName.GetSingleLocation(LOCNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["LOCNO"] = LOCNO;
                txtLocName.Text = ds.Tables[0].Rows[0]["LOCATION_NAME"].ToString();
                txtLatitude.Text = ds.Tables[0].Rows[0]["LATITUDE"].ToString();
                txtLongitude.Text = ds.Tables[0].Rows[0]["LONGITUDE"].ToString();
                txtRadius.Text = ds.Tables[0].Rows[0]["RADIUS"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["LOCATION_ADDRESS"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {

        txtLocName.Text = string.Empty;
        txtLatitude.Text = string.Empty;
        txtLongitude.Text = string.Empty;
        txtRadius.Text = string.Empty;
        txtAddress.Text = string.Empty;
        //ViewState["action"] = null;
        // ViewState["LOCNO"] = null;
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_LOCATION_MASTER", "*", "", "LOCATION_NAME='" + txtLocName.Text + "' AND LATITUDE='" + txtLatitude.Text +
           "' AND LONGITUDE='" + txtLongitude.Text + "' AND  RADIUS='" + txtRadius.Text + "' AND  LOCATION_ADDRESS='" + txtAddress.Text + "'", "");

        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
}