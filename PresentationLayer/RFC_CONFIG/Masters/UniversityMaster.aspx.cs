//======================================================================================
// PROJECT NAME  : RFC_common                                                                
// MODULE NAME   : MAPPING CREATION                        
// CREATION DATE : 14-10-2021                                                       
// CREATED BY    : S.Patil
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;


public partial class RFC_CONFIG_Masters_UniversityMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UniversityController objUC = new UniversityController();
    University objUNI = new University();

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
                //objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlQualification, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
            }
            BindListView();
            ViewState["action"] = "add";


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
                Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_UniversityMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RFC_CONFIG_Masters_UniversityMaster.aspx");
        }
    }
    #endregion Check Authorization

    #region Button Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUniversityName.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(updBatch, "Please Enter University Name", this.Page);
            }
            else if (ddlState.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updBatch, "Please Select State", this.Page);
            }
            else
            {

                objUNI.UniversityName = txtUniversityName.Text.Trim();
                objUNI.Stateid = Convert.ToInt32(ddlState.SelectedValue);
                if (hfdStat.Value == "true")
                {
                    objUNI.Status = true;
                }
                else
                {
                    objUNI.Status = false;
                }
                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    
                        if (ViewState["uniid"] != null)
                        {
                            objUNI.Universityid = Convert.ToInt32(ViewState["uniid"]);
                        }
                        CustomStatus cs = (CustomStatus)objUC.SaveUniversityMasterData(objUNI);
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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_UniversityMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
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
            int universityno = int.Parse(btnEdit.CommandArgument);
            ShowDetail(universityno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_UniversityMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Button Click Events

    #region Methods

    private void ShowDetail(int id)
    {
        DataSet ds = null;
        ds = objUC.GetUniversityMasterDateByid(id);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["uniid"] = id.ToString();
                txtUniversityName.Text = ds.Tables[0].Rows[0]["UNIVERSITYNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["UNIVERSITYNAME"].ToString();
                //objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
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
    private void BindListView()
    {
        try
        {
            DataSet ds = objUC.GetUniversityMasterDateByid(0);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvUniversity.DataSource = ds.Tables[0];
                lvUniversity.DataBind();
            }
            else
            {
                lvUniversity.DataSource = null;
                lvUniversity.DataBind();
            }

            if (ds.Tables[1]!= null && ds.Tables[1].Rows.Count > 0)
            {
                ddlState.DataSource = ds.Tables[1];
                ddlState.DataTextField = ds.Tables[1].Columns["STATENAME"].ToString();
                ddlState.DataValueField = ds.Tables[1].Columns["STATENO"].ToString();
                ddlState.DataBind();
            }
            else
            {
                ddlState.DataSource = null;
                ddlState.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RFC_CONFIG_Masters_UniversityMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtUniversityName.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["uniid"] = null;
    }
    #endregion Methods

}