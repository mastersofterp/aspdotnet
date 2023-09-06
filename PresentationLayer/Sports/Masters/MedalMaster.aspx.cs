//===========================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 03-09-2014
// DESCRIPTION   : USED TO CREATE MEDAL NAME
//===========================================
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

public partial class Sports_Masters_MedalMaster : System.Web.UI.Page
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
                    objCommon.FillDropDownList(ddlSportType, "SPRT_SPORT_TYPE", "TYPID", "GAME_TYPE", "", "GAME_TYPE");
                  
                }
                //BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_MedalMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private bool checkPostExist()
    {
        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_MEDAL_MASTER", "MEDALID", "MEDALNAME", "MEDALNAME='" + txtMedalName.Text + "' AND SPID = " + Convert.ToInt32(ddlSportName.SelectedValue), "MEDALID");
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
            objSport.MEDALNAME = txtMedalName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtMedalName.Text);
            objSport.TYPID = Convert.ToInt32(ddlSportType.SelectedValue);
            objSport.USERID = Convert.ToInt32(Session["userno"]);
            objSport.SPID = Convert.ToInt32(ddlSportName.SelectedValue);

            if (ViewState["MEDALID"] == null)
            {
                if (checkPostExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Medal Name Already Exist...!!');", true);
                    Clear();
                    return;
                }
                else
                {
                    objSportC.AddUpdateMedalMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Medal Name Submitted Successfully...!!');", true);
                    Clear();
                    BindlistView();
                }
            }
            else
            {
                objSport.MEDALID = Convert.ToInt32(ViewState["MEDALID"].ToString());
                objSportC.AddUpdateMedalMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Medal Name Updated Successfully...!!');", true);
                Clear();
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_MedalMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        BindlistView();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int medal_id = int.Parse(btnEdit.CommandArgument);
            ViewState["MEDALID"] = int.Parse(btnEdit.CommandArgument);
            //int spid = int.Parse(btnEdit.CommandArgument);
            //ViewState["SPID"] = int.Parse(btnEdit.CommandArgument);
            DataSet ds = null;
            ds = objCommon.FillDropDown("SPRT_SPORT_MASTER", "SPID", "", "SNAME='" + ddlSportName.SelectedValue + "'", "");
            
            ShowDetails(medal_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_MedalMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("SPRT_VENUE_MASTER", "*", "", "", "VENUEID DESC");
            DataSet ds = objCommon.FillDropDown("SPRT_MEDAL_MASTER MM INNER JOIN SPRT_SPORT_TYPE ST ON (MM.TYPID=ST.TYPID) INNER JOIN SPRT_SPORT_MASTER SM ON (MM.SPID = SM.SPID)", "MM.MEDALID, MM.MEDALNAME", "ST.GAME_TYPE,ST.TYPID, MM.SPID, SM.SNAME", "MM.TYPID='" + Convert.ToInt32(ddlSportType.SelectedValue) + "'", "MEDALID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvMedal.DataSource = ds;
                lvMedal.DataBind();
            }
            else
            {
                lvMedal.DataSource = null;
                lvMedal.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_MedalMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int medal_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_MEDAL_MASTER", "*", "", "MEDALID=" + medal_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtMedalName.Text = ds.Tables[0].Rows[0]["MEDALNAME"].ToString();
                ddlSportType.SelectedValue = ds.Tables[0].Rows[0]["TYPID"].ToString();
               
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_MedalMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlSportType.SelectedIndex = 0;
        txtMedalName.Text = string.Empty;        
        ViewState["MEDALID"] = null;
        ddlSportName.SelectedIndex = 0;
    }

    protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSportType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID=" + ddlSportType.SelectedValue, "SPID DESC");
            BindlistView();
        }
        else
        {
            ddlSportName.SelectedValue = "0";
            lvMedal.DataSource = null;
            lvMedal.DataBind();
        }
    }
}
