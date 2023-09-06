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

public partial class VEHICLE_MAINTENANCE_Master_VehicleSuppilerMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                
                    BindlistView();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objVM.SUPPILER_NAME = txtSuppilerName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSuppilerName.Text.Trim());
            objVM.CONTACT_ADDRESS = txtSuppilerAddress.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSuppilerAddress.Text);
            objVM.CONTACT_NUMBER = txtSuppilerCntNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSuppilerCntNo.Text.Trim());
            objVM.CONTACT_PERSON = txtSuppilerContactPerson.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSuppilerContactPerson.Text.Trim());
            objVM.IS_ACTIVE = true;


            


            if (ViewState["SUPPILER_ID"] == null)
            {
                //--======start===Shaikh Juned 24-08-2022

                DataSet ds = objCommon.FillDropDown("VEHICLE_SUPPILER_MASTER", "SUPPILER_ID", "SUPPILER_NAME", "CONTACT_NUMBER='" + txtSuppilerCntNo.Text+"'", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    string phone = dr["PHONE"].ToString();
                    //    if (phone == txtSuppilerCntNo.Text)
                    //    {
                    objCommon.DisplayMessage(this.updActivity, "Contact No Is Already Exist.", this.Page);
                    return;
                    //    }

                    //}
                }
                //---========end=====

                CustomStatus cs = (CustomStatus)objVMC.AddUpdateSuppilerMaster(objVM);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                //Clear();
                //BindlistView();

                if (cs.Equals(CustomStatus.RecordExist))
                {

                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                    return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindlistView();
                    ViewState["action"] = "add";
                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                }
       
            }
            else
            {

                objVM.SUPPILER_ID = Convert.ToInt32(ViewState["SUPPILER_ID"].ToString());

                //--======start===Shaikh Juned 24-08-2022

                DataSet ds = objCommon.FillDropDown("VEHICLE_SUPPILER_MASTER", "SUPPILER_ID", "SUPPILER_NAME", "CONTACT_NUMBER=" + txtSuppilerCntNo.Text, "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int SUPPILERID = Convert.ToInt32(dr["SUPPILER_ID"]);
                        if (SUPPILERID != Convert.ToInt32(objVM.SUPPILER_ID))
                        {
                            objCommon.DisplayMessage(this.updActivity, "Contact No Is Already Exist.", this.Page);
                            return;
                        }

                    }
                }
                //---========end=====

                CustomStatus cs = (CustomStatus)objVMC.AddUpdateSuppilerMaster(objVM);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                //Clear();
                //BindlistView();
                //ViewState["SUPPILER_ID"] = null;    
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindlistView();
                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                    Clear();
                }
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int suppiler_id = int.Parse(btnEdit.CommandArgument);
            ViewState["SUPPILER_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(suppiler_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_SUPPILER_MASTER", "*", "", "", "SUPPILER_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSuppiler.DataSource = ds;
                lvSuppiler.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int suppiler_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_SUPPILER_MASTER", "*", "", "SUPPILER_ID=" + suppiler_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSuppilerName.Text = ds.Tables[0].Rows[0]["SUPPILER_NAME"].ToString();
                txtSuppilerAddress.Text = ds.Tables[0].Rows[0]["CONTACT_ADDRESS"].ToString();
                txtSuppilerCntNo.Text = ds.Tables[0].Rows[0]["CONTACT_NUMBER"].ToString();
                txtSuppilerContactPerson.Text = ds.Tables[0].Rows[0]["CONTACT_PERSON"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtSuppilerName.Text = string.Empty;
        txtSuppilerAddress.Text = string.Empty;
        txtSuppilerCntNo.Text = string.Empty;
        txtSuppilerContactPerson.Text = string.Empty;
        ViewState["SUPPILER_ID"] = null;
    }
}
