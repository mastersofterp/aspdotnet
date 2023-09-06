//======================================================================================
// PROJECT NAME  : RFC COMMON                                                          
// MODULE NAME   : Affilation Type Master                    
// CREATION DATE : 21-SEPT-2021                                                         
// CREATED BY    : S.PATIL
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class RFC_CONFIG_Masters_AffilationType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigAffilationTypeController objBC = new ConfigAffilationTypeController();
    ConfigAffilationType objInsti = new ConfigAffilationType();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //if (Session["masterpage"] != null)
        //    objCommon.SetMasterPage(Page, "ConfigSiteMasterPage.master");
        //else
        //    objCommon.SetMasterPage(Page, "ConfigSiteMasterPage.master");


        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
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
                CheckPageAuthorization();
                //Set the Page Title
                //Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
              
            }
            BindListView();
            ViewState["action"] = "add";
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion Page Events

    #region Check Authorization
    //Page Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AffilationType.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AffilationType.aspx");
        }
    }
    #endregion Check Authorization

    #region Button Click Events
    //To Save Data
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objInsti.AffilationName = txtAffilationName.Text.Trim();
            if (hfdStat.Value == "true")
            {
                objInsti.ActiveStatus = true;
            }
            else
            {
                objInsti.ActiveStatus = false;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                    if (ViewState["AffilationTypeId"] != null)
                    {
                        objInsti.AffilationTypeId = Convert.ToInt32(ViewState["AffilationTypeId"]);
                    }
                    CustomStatus cs = (CustomStatus)objBC.SaveAffilationTypeData(objInsti);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        objCommon.DisplayMessage(this.updBatch, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        Clear();
                        objCommon.DisplayMessage(this.updBatch, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updBatch, "Record Already exist", this.Page);
                    }
                
               
                BindListView();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //To Clear Fields
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //To Edit Data
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int editno = int.Parse(btnEdit.CommandArgument);
            ShowDetail(editno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  
    #endregion Button Click Events

    #region Methods
    //To Show Edit Record Details
    private void ShowDetail(int id)
    {
        DataSet ds = null;
        ds = objBC.GetAffilationTypeData(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["AffilationTypeId"] = id.ToString();
                txtAffilationName.Text = ds.Tables[0].Rows[0]["AffilationName"] == null ? string.Empty : ds.Tables[0].Rows[0]["AffilationName"].ToString();
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "Active")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
    }
    //To Clear Fields
    private void Clear()
    {
        txtAffilationName.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["AffilationTypeId"] = null;
    }
    //To Bind listview
    private void BindListView()
    {
        try
        {
            DataSet ds = objBC.GetAffilationTypeData(0);
            lvAffilation.DataSource = ds;
            lvAffilation.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
   

    #endregion Methods
}