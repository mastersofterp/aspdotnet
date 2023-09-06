//======================================================================================
// PROJECT NAME  : RFC_common                                                                
// MODULE NAME   : DEGREE TYPE MASTER                        
// CREATION DATE : 06-10-2021                                                       
// CREATED BY    : S.Patil
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

public partial class RFC_CONFIG_Masters_InstituteType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    InstituteTypeController objBC = new InstituteTypeController();
    InstituteType objInsti = new InstituteType();
   
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
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlQualification, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
            }
            BindListView();
            ViewState["action"] = "add";
            //trQualLevel.Visible = false;
            //lblQuaLevel.Visible = false;
            //ddlQualification.Visible = false;

        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion Page Events

    #region Check Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_InstituteType.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_InstituteType.aspx");
        }
    }
    #endregion Check Authorization

    #region Button Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objInsti.InstituteTypeName = txtInstituteTypeName.Text.Trim();
            if (hfdStat.Value == "true")
            {
                objInsti.IsActive = true;
            }
            else
            {
                objInsti.IsActive = false;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                    if (ViewState["InstitutionTypeId"] != null)
                    {
                        objInsti.InstituteTypeNo = Convert.ToInt32(ViewState["InstitutionTypeId"]);
                    }
                    CustomStatus cs = (CustomStatus)objBC.SaveInstituteTypeData(objInsti);
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
                        objCommon.DisplayMessage(this.updBatch,"Record already exist", this.Page);
                    }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_InstituteType.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

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
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_InstituteType.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    #endregion Button Click Events

    #region Methods
    private void ShowDetail(int id)
    {
        DataSet ds = null;
        ds = objBC.GetInstituteTypeDataById(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["InstitutionTypeId"] = id.ToString();
                txtInstituteTypeName.Text = ds.Tables[0].Rows[0]["InstitutionTypeName"] == null ? string.Empty : ds.Tables[0].Rows[0]["InstitutionTypeName"].ToString();

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

    private void Clear()
    {
        txtInstituteTypeName.Text = string.Empty;
        //IsActive.Checked = true;
        ViewState["action"] = "add";
        ViewState["InstitutionTypeId"] = null;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objBC.GetInstituteTypeDataById(0);
            lvInstituteType.DataSource = ds;
            lvInstituteType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_InstituteType.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  
    #endregion Methods
   
}