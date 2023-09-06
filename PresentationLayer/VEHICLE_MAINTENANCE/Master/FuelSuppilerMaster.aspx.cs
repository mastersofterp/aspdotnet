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

public partial class VEHICLE_MAINTENANCE_Master_FuelSuppilerMaster : System.Web.UI.Page
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

                    }

                    BindlistView();
                    ViewState["Action"] = "Add";
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

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_FUEL_SUPPILER_MASTER", "*", "", "IS_ACTIVE=1", "FUEL_SUPPILER_ID");
            ViewState["Table"] = ds;
            lvSuppiler.DataSource = ds;
            lvSuppiler.DataBind();
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
        try
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheackCode())
            {
                objCommon.DisplayUserMessage(updActivity, "Record Already Exist", this.Page);
                return;
            }
            else
            {

                objVM.FUEL_CONTACT_ADDRESS = txtSuppilerAddress.Text;
                objVM.FUEL_CONTACT_NUMBER = txtSuppilerCntNo.Text;
                objVM.FUEL_CONTACT_PERSON = txtSuppilerContactPerson.Text;
                objVM.FUEL_SUPPILER_NAME = txtSuppilerName.Text;
                // objVM.FUEL_SUPPLIED_DATE = Convert.ToDateTime(txtSupdate.Text);
                objVM.FUEL_IS_ACTIVE = true;

                if (ViewState["Action"].ToString().Equals("Edit"))
                {
                    objVM.FUEL_SUPPILER_ID = Convert.ToInt32(Session["FUEL_SUPPILER_ID"].ToString());
                }
                CustomStatus cs = (CustomStatus)objVMC.InsUpdFuelSupllier(objVM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                }

                Clear();
                BindlistView();
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


    private void Clear()
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Total", "<script type='text/javascript'>Clear();</script>", false);
            ViewState["Action"] = "Add";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public bool CheackCode()
    {
        bool result = false;

        DataSet dsCode = new DataSet();

        if (ViewState["Action"].ToString().Equals("Add"))
        {
         DataSet ds=objCommon.FillDropDown("VEHICLE_FUEL_SUPPILER_MASTER", "*","", "FUEL_CONTACT_NUMBER='" + txtSuppilerCntNo.Text+"'","");
           if (ds.Tables[0].Rows.Count > 0)
            {
                objCommon.DisplayMessage(this.updActivity, "This Contact Number Already Exist.", this.Page);
                txtSuppilerCntNo.Text = string.Empty;
                result = true;
            }
            else
            {
                return result;
            }
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_FUEL_SUPPILER_MASTER", "*", "", "FUEL_CONTACT_NUMBER='" + txtSuppilerCntNo.Text + "' AND FUEL_SUPPILER_ID<>" + Session["FUEL_SUPPILER_ID"].ToString(), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                objCommon.DisplayMessage(this.updActivity, "This Contact Number Already Exist.", this.Page);
                txtSuppilerCntNo.Text = string.Empty;
                result = true;
            }
            else
            {
                return result;
            }
        }
        return result;

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton Imgbtn = sender as ImageButton;
            DataSet ds = (DataSet)ViewState["Table"];
            DataTable dt = ds.Tables[0];
            DataRow[] foundrows = dt.Select("FUEL_SUPPILER_ID=" + Imgbtn.CommandArgument);
            if (foundrows.Length > 0)
            {
                DataTable newTable = foundrows.CopyToDataTable();
                if (newTable.Rows.Count > 0)
                {
                    txtSuppilerAddress.Text = newTable.Rows[0]["FUEL_CONTACT_ADDRESS"].ToString();
                    txtSuppilerCntNo.Text = newTable.Rows[0]["FUEL_CONTACT_NUMBER"].ToString();
                    txtSuppilerContactPerson.Text = newTable.Rows[0]["FUEL_CONTACT_PERSON"].ToString();
                    txtSuppilerName.Text = newTable.Rows[0]["FUEL_SUPPILER_NAME"].ToString();
                    Session["FUEL_SUPPILER_ID"] = Imgbtn.CommandArgument;
                    ViewState["Action"] = "Edit";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void txtSuppilerCntNo_TextChanged(object sender, EventArgs e)
    {
        //if (txtSuppilerCntNo.Text == string.Empty)
        //{
        //    return;
        //}
        //else
        //{
        //    DataSet ds = (DataSet)ViewState["Table"];
        //    DataRow[] foundrows = ds.Tables[0].Select("FUEL_CONTACT_NUMBER=" + txtSuppilerCntNo.Text);
        //    if (foundrows.Length > 0)
        //    {
        //        objCommon.DisplayMessage(this.updActivity, "This Contact Number Already Exist.", this.Page);
        //        txtSuppilerCntNo.Text = string.Empty;
        //    }
        //}
    }
}