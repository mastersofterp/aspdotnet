//=====================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 29-AUG-2017                                                        
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


public partial class FileMovementTracking_Master_DocumentType : System.Web.UI.Page
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
                objUCommon.ShowError(Page, "FileMovementTracking_Master_DocumentType.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            DataSet ds = objCommon.FillDropDown("FILE_DOCUMENT_TYPE", "*", "", "STATUS=0", "DOCUMENT_TYPE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDocumentType.DataSource = ds;
                lvDocumentType.DataBind();
                lvDocumentType.Visible = true;
            }
            else
            {
                lvDocumentType.DataSource = null;
                lvDocumentType.DataBind();
                lvDocumentType.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_DocumentType.BindlistView -> " + ex.Message + " " + ex.StackTrace);
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
            objFMov.DOCUMENT_TYPE = Convert.ToString(txtDocumentType.Text);

            if (ViewState["action"] != null)
            {
                if (txtDocumentType.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Enter Document Type.", this.Page);
                    return;
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        DataSet ds = objCommon.FillDropDown("FILE_DOCUMENT_TYPE", "DOCUMENT_TYPE_ID", "DOCUMENT_TYPE", "STATUS = 0 AND DOCUMENT_TYPE='" + objFMov.DOCUMENT_TYPE + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);                          
                            return;
                        }
                        else
                        {
                            objFMov.DOCUMENT_TYPE_ID = 0;
                            CustomStatus cs = (CustomStatus)objFController.AddUpdateDocumentType(objFMov);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        objFMov.DOCUMENT_TYPE_ID = Convert.ToInt32(ViewState["DOCUMENT_TYPE_ID"]);

                        DataSet ds = objCommon.FillDropDown("FILE_DOCUMENT_TYPE", "DOCUMENT_TYPE_ID", "DOCUMENT_TYPE", "STATUS = 0 AND DOCUMENT_TYPE ='" + objFMov.DOCUMENT_TYPE + "' AND DOCUMENT_TYPE_ID !=" + Convert.ToInt32(ViewState["DOCUMENT_TYPE_ID"]), "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);                           
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objFController.AddUpdateDocumentType(objFMov);
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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_DocumentType.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int DOCUMENT_TYPE_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["DOCUMENT_TYPE_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";

            ShowDetails(DOCUMENT_TYPE_ID);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_DocumentType.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int DOCUMENT_TYPE_ID = int.Parse(btnDelete.CommandArgument);
            ViewState["DOCUMENT_TYPE_ID"] = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER", "*", "", "DOCUMENT_TYPE =" + DOCUMENT_TYPE_ID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Document Type can not delete, it is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objFController.DeleteDocumentType(DOCUMENT_TYPE_ID);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindlistView();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_DocumentType.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    // This function is used to show details of the particular record.
    private void ShowDetails(int DOCUMENT_TYPE_ID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("FILE_DOCUMENT_TYPE", "*", "", "DOCUMENT_TYPE_ID=" + Convert.ToInt32(ViewState["DOCUMENT_TYPE_ID"]) + "", "DOCUMENT_TYPE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtDocumentType.Text = Convert.ToString(dr["DOCUMENT_TYPE"]);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_DocumentType.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
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
        txtDocumentType.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["DOCUMENT_TYPE_ID"] = null;
    }

    #endregion

}