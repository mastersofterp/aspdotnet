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

public partial class VEHICLE_MAINTENANCE_Master_TripType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();    

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
                    ViewState["action"] = "add";
                    BindlistView();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



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

            objVM.TRIPTYPENAME = txtTripTypeName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtTripTypeName.Text.Trim());
            objVM.CHARGEABLE = chkChargeable.Checked;
            objVM.ACTIVE = chkActive.Checked;
            objVM.COLLEGE_CODE = Convert.ToInt32(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objVM.TTID = 0;
                    
                    CustomStatus cs = (CustomStatus)objVMC.TripTypeInsertUpdate(objVM);
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
                        objCommon.DisplayMessage(this.updActivity, "Records Save Successfully.", this.Page);
                    }
                    

                }
                else
                {
                    if (ViewState["TTID"] != null)
                    {
                        objVM.TTID = Convert.ToInt32(ViewState["TTID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.TripTypeInsertUpdate(objVM);
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
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                            Clear();
                        }

                    }
             
                }  
            
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int TTID = int.Parse(btnEdit.CommandArgument);
            ViewState["TTID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(TTID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objVMC.GetTripTypeAll();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTripType.DataSource = ds;
                lvTripType.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int TTID)
    {
        try
        {
            DataSet ds = objVMC.GetTripTypeById(TTID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtTripTypeName.Text = ds.Tables[0].Rows[0]["TRIPTYPENAME"].ToString();
                chkChargeable.Checked = Convert.ToBoolean ( ds.Tables[0].Rows[0]["CHARGEABLE"]);
                chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ACTIVE"]);
                //txtDrvrAdd2.Text = ds.Tables[0].Rows[0]["DADD2"].ToString();
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
        txtTripTypeName.Text = string.Empty;
        chkChargeable.Checked = false;
        chkActive.Checked = true;
        ViewState["action"] = "add";
        ViewState["TTID"] = null;
    }
    protected void txtDrvrCntNo_TextChanged(object sender, EventArgs e)
    {

    }



}
