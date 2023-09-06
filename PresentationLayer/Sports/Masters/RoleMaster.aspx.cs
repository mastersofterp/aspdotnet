//====================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 11-09-2014
// DESCRIPTION   : USED TO CREATE ROLES OF THE PLAYER
//=====================================================
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

public partial class Sports_Masters_RoleMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport = new Sport();

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
                    objCommon.FillDropDownList(ddlSportType, "SPRT_SPORT_TYPE", "TYPID", "GAME_TYPE", "", "TYPID"); 
                }
               BindlistView();
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    private void PopulateSportName()
    {
        try
        {
            objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID = " + Convert.ToInt32(ddlSportType.SelectedValue), "SPID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.PopulateSportName()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool checkRoleExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_ROLE_MASTER", "ROLEID", "ROLENAME", "ROLENAME='" + txtRoleName.Text + "' AND SPID=" + ddlSportName.SelectedValue  , "ROLEID");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                retVal = true;
            }

        }

        return retVal;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objSport.SPID = Convert.ToInt32(ddlSportName.SelectedValue.ToString());
            objSport.ROLENAME = txtRoleName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRoleName.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["ROLEID"] == null)
            {
                if (checkRoleExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Role Name Already Exist...!!');", true);
                   // Clear();
                    txtRoleName.Text = string.Empty;   //01/04/02022 jun
                    BindlistView();//01/04/02022 junaid

                }
                else
                {
                    objSportC.AddUpdateRoleMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Role Name Submitted Successfully...!!');", true);
                    Clear();
                    //BindlistView();
                }
            }
            else
            {

                objSport.ROLEID = Convert.ToInt32(ViewState["ROLEID"].ToString());

                objSportC.AddUpdateRoleMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Role Name Updated Successfully...!!');", true);
                Clear();

            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int role_id = int.Parse(btnEdit.CommandArgument);
            ViewState["ROLEID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(role_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //private void BindlistView()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER SM INNER JOIN SPRT_ROLE_MASTER RM ON (SM.SPID=RM.SPID)", "RM.ROLEID,RM.ROLENAME,RM.SPID", "SM.SNAME,SM.SPID", "RM.SPID='" + Convert.ToInt32(ddlSportName.SelectedValue) + "'", "RM.ROLEID DESC");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvRole.DataSource = ds;
    //            lvRole.DataBind();
    //            pnlList.Visible = true;
    //        }
    //        else
    //        {
    //            lvRole.DataSource = null;
    //            lvRole.DataBind();
    //            pnlList.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void BindlistView()  //Shaikh Juned (02/04/2022)---Start--
    {
        try
        {

            if (ddlSportName.SelectedItem != null)
            {
                DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER SM INNER JOIN SPRT_ROLE_MASTER RM ON (SM.SPID=RM.SPID)", "RM.ROLEID,RM.ROLENAME,RM.SPID", "SM.SNAME,SM.SPID", "RM.SPID='" + Convert.ToInt32(ddlSportName.SelectedValue) + "'", "RM.ROLEID DESC");


                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvRole.DataSource = ds;
                    lvRole.DataBind();
                    pnlList.Visible = true;
                }
                else
                {
                    lvRole.DataSource = null;
                    lvRole.DataBind();
                    pnlList.Visible = false;
                }
            }
            else
            {
                // DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER SM INNER JOIN SPRT_ROLE_MASTER RM ON (SM.SPID=RM.SPID)", "RM.ROLEID,RM.ROLENAME,RM.SPID", "SM.SNAME,SM.SPID", "RM.SPID='" + Convert.ToInt32(ddlSportName.SelectedValue) + "'", "RM.ROLEID DESC");
                DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER SM INNER JOIN SPRT_ROLE_MASTER RM ON (SM.SPID=RM.SPID)", "RM.ROLEID,RM.ROLENAME,RM.SPID", "SM.SNAME,SM.SPID", "", "RM.ROLEID DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvRole.DataSource = ds;
                    lvRole.DataBind();
                    pnlList.Visible = true;
                }
                else
                {
                    lvRole.DataSource = null;
                    lvRole.DataBind();
                    pnlList.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   //Shaikh Juned (02/04/2022)---end--


    private void ShowDetails(int role_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_ROLE_MASTER", "*", "", "ROLEID=" + role_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();
                txtRoleName.Text = ds.Tables[0].Rows[0]["ROLENAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_RoleMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        //Modified by Saahil Trivedi 23-02-2022
        ddlSportType.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        txtRoleName.Text = string.Empty;
        lvRole.DataSource = null;
        lvRole.DataBind();
        ViewState["ROLEID"] = null;

    }
  protected void ddlSportName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSportName.SelectedIndex > 0)
        {
            BindlistView();
        }
    }
  protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
  {
      PopulateSportName();
  }
}
