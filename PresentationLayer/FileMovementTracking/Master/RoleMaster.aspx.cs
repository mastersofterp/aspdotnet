//=====================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 14-MAR-2018                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//=====================================================================
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.FileMovement;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Configuration;

public partial class FileMovementTracking_Master_RoleMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FileMovement objFMov = new FileMovement();
    FileMovementController objFController = new FileMovementController();

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
                    ViewState["action"] = "add";
                    BindlistView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_RoleMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("FILE_ROLE_MASTER", "*", "", "", "ROLE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRole.DataSource = ds;
                lvRole.DataBind();
                lvRole.Visible = true;
            }
            else
            {
                lvRole.DataSource = null;
                lvRole.DataBind();
                lvRole.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_RoleMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion


    #region Page Actions

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objFMov.ROLENAME = Convert.ToString(txtRoleName.Text);

            if (ViewState["action"] != null)
            {
                if (txtRoleName.Text == string.Empty)
                {
                    //objCommon.DisplayMessage(this.updPanel, "Please Enter Role Name.", this.Page); 15/11/2021
                    objCommon.DisplayMessage( "Please Enter Role Name.", this.Page);
                    return;
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        DataSet ds = objCommon.FillDropDown("FILE_ROLE_MASTER", "ROLE_ID", "ROLENAME", "ROLENAME='" + objFMov.ROLENAME + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //objCommon.DisplayMessage(this.updPanel, "Record Already Exist", this.Page);15/11/2021
                            objCommon.DisplayMessage("Record Already Exist", this.Page);
                            return;
                        }
                        else
                        {
                            objFMov.ROLE_ID = 0;
                            CustomStatus cs = (CustomStatus)objFController.AddUpdateRole(objFMov);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                //objCommon.DisplayMessage(this.updPanel, "Record Saved Successfully.", this.Page);15/11/2021
                                objCommon.DisplayMessage( "Record Saved Successfully.", this.Page);
                                Clear();
                                BindlistView();
                            }
                        }
                    }
                    else
                    {
                        objFMov.ROLE_ID = Convert.ToInt32(ViewState["ROLE_ID"]);

                        DataSet ds = objCommon.FillDropDown("FILE_ROLE_MASTER", "ROLE_ID", "ROLENAME", "ROLENAME ='" + objFMov.ROLENAME + "' AND ROLE_ID !=" + Convert.ToInt32(ViewState["ROLE_ID"]), "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //objCommon.DisplayMessage(this.updPanel, "Record Already Exist.", this.Page); 15/11/2021
                            objCommon.DisplayMessage( "Record Already Exist.", this.Page);
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objFController.AddUpdateRole(objFMov);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                //objCommon.DisplayMessage(this.updPanel, "Record Updated Successfully.", this.Page); 15/11/2021
                                objCommon.DisplayMessage( "Record Updated Successfully.", this.Page);
                                Clear();
                                BindlistView();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_RoleMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //msgcomp.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int ROLE_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["ROLE_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";

            ShowDetails(ROLE_ID);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_RoleMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
    // This function is used to show details of the particular record.
    private void ShowDetails(int ROLE_ID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("FILE_ROLE_MASTER", "*", "", "ROLE_ID=" + Convert.ToInt32(ViewState["ROLE_ID"]) + "", "ROLE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtRoleName.Text = Convert.ToString(dr["ROLENAME"]);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_RoleMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    // This function is used to clear the controls.
    private void Clear()
    {
        txtRoleName.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["ROLE_ID"] = null;
    }

    #endregion

}