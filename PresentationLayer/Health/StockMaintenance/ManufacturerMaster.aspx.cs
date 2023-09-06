//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH                     
// CREATION DATE : 04-MARCH-2016                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_StockMaintenance_ManufacturerMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StockMaster objStock = new StockMaster();
    StockMaintnance objSController = new StockMaintnance();

    #region PageLoad Events
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
                    BindListView();                  
                    ViewState["action"] = "add";
                    txtMCode.Focus();
                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ManufacturerMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region User-Define Methods
    // This method is used to check page authorization.
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
    // this method is used to clear the controls.
    private void Clear()
    {
        ViewState["action"] = "add";
        ViewState["M_ID"] = null;        
        txtAddress.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtMCode.Text = string.Empty;
        txtMName.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtContPers.Text = string.Empty;
        BindListView();
        txtMCode.Focus();
    }
    // this method is used to display the entry list.
    private void BindListView()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("HEALTH_MANUFACTURER M", "M.MNO", "M.MCODE,M.MNAME,M.ADDRESS,M.EMAIL,M.REMARK,M.CONT_PERSON,", "MNO > 0", "MCODE");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvManufacture.DataSource = ds;
                lvManufacture.DataBind();
            }
            else
            {
                lvManufacture.DataSource = null;
                lvManufacture.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ManufacturerMaster.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // this method is used to show the details fetch from database.
    private void ShowDetails(int m_no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_MANUFACTURER", "MNO", "*", "MNO=" + m_no, "MNAME");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtPhone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                txtMCode.Text = ds.Tables[0].Rows[0]["MCODE"].ToString();
                txtContPers.Text = ds.Tables[0].Rows[0]["CONT_PERSON"].Equals(DBNull.Value) ? string.Empty : ds.Tables[0].Rows[0]["CONT_PERSON"].ToString();
                txtMName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ManufacturerMaster.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion
    #region Page Actions
    // this button is used to insert and update the section name.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objStock.MCODE = txtMCode.Text.Trim();
            objStock.MNAME = txtMName.Text.Trim();
            objStock.ADDRESS = txtAddress.Text.Trim();
            objStock.CONT_PERSON = txtContPers.Text;
            objStock.PHONE = txtPhone.Text.Trim();
            objStock.EMAIL = txtEmail.Text.Trim();
            objStock.USERID = Convert.ToInt32(Session["userno"].ToString());
            objStock.REMARK = txtRemark.Text.Trim();

            if (ViewState["action"] != null)
            {
                //checking viewstate status. If it is edit then records will be updated else it will be added
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objSController.AddManufacturer(objStock);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updActivity, "Record Save Successfully.", this.Page);
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updActivity, "Error while saving Record.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objStock.MNO = Convert.ToInt32(ViewState["MNO"].ToString());
                        CustomStatus cs = (CustomStatus)objSController.UpdateManufacturer(objStock);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(updActivity, "Record Updated Successfully.", this.Page);
                            Clear();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updActivity, "Error while Updating Record.", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message.ToString());
        }
    }
    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    // this button is used to brings you in modify mode.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int m_no = int.Parse(btnEdit.CommandArgument);
            ViewState["M_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(m_no);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ManufacturerMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

  
   
    #endregion
}