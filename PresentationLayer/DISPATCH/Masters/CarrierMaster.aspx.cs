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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Dispatch_Masters_CarrierMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CarrierMaster objCM = new CarrierMaster();
    CarrierController objCMController = new CarrierController();


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
                    //Bind the ListView with Case Types
                    BindListViewCarrier();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_CarrierMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            objCM.carrierName = txtCarrierName.Text;
            objCM.college_code = Session["colcode"].ToString();
            if (ViewState["CARRIER_NO"] == null)
            {
                CustomStatus cs = (CustomStatus)objCMController.AddUpdateCarrier(objCM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListViewCarrier();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                }
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                    return;
                }
            }
            else
            {
                objCM.carrierNo = Convert.ToInt32(ViewState["CARRIER_NO"].ToString());
                CustomStatus cs = (CustomStatus)objCMController.AddUpdateCarrier(objCM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListViewCarrier();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                }
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_CarrierMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int carrierNo = int.Parse(btnEdit.CommandArgument);
            ViewState["CARRIER_NO"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(carrierNo);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_CarrierMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewCarrier()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ADMN_IO_CARRIER", "*", "", "STATUS=0", "CARRIERNO");
            lvCarrier.DataSource = ds;
            lvCarrier.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_CarrierMaster.BindListViewCaseType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int carrierNo)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ADMN_IO_CARRIER", "*", "", "CARRIERNO=" + carrierNo, "");

            //to show created user details
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["carrier_no"] = ds.Tables[0].Rows[0]["CARRIERNO"].ToString();
                    txtCarrierName.Text = ds.Tables[0].Rows[0]["CARRIERNAME"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_CarrierMaster.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtCarrierName.Text = "";
        ViewState["CARRIER_NO"] = null;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int carrierNo = int.Parse(btnDelete.CommandArgument);
            ViewState["CARRIER_NO"] = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN", "*", "", "CARRIERNO=" + carrierNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This carrier can not delete, it is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objCMController.DeleteCarrier(carrierNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindListViewCarrier();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Masters_CarrierMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}