//================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 15-09-2014
// DESCRIPTION   : USED TO CREATE STAFF AND PLAYER 
//                 AGAINST TEAM NAME.
// MODIFIED DATE :
// MODIFIED BY   :
//================================================
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

public partial class Sports_Masters_TeamDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport = new Sport();

    # region PageEvent
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
                    //objCommon.FillDropDownList(ddlTeamName, "SPRT_TEAM_MASTER TM INNER JOIN SPRT_SPORT_MASTER SM ON(SM.SPID = TM.SPID)", "TEAMID", "SM.SNAME+' - '+TM.TEAMNAME AS TEAMNAME", "", "TM.TEAMNAME");
                    objCommon.FillDropDownList(ddlTeamName, "SPRT_TEAM_MASTER TM LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID", "TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "", "TEAMNAME");
               
                    PopulatePostName();
                   // PopulatePlayerName();
                    PopulateRoleName();
                   // BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
# endregion

    #region User-Methods
    // This method is used to check page authority.
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
    // This method is used to get the list of post names.
    private void PopulatePostName()
    {
        try
        {
            objCommon.FillDropDownList(ddlPost, "SPRT_STAFF_MASTER", "POSTID", "POSTNAME", "", "POSTID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.PopulatePostName()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to get list of player name.
    private void PopulatePlayerName()
    {
        try
        {
            objCommon.FillDropDownList(ddlPlayer, "SPRT_PLAYER_MASTER PM INNER JOIN SPRT_SPORT_MASTER SM ON(SM.SPID = PM.SPID)  LEFT JOIN ACD_COLLEGE_MASTER ACM ON (PM.COLLEGE_NO = ACM.COLLEGE_ID) ", "PM.PLAYERID", "SM.SNAME+' - '+PM.PLAYERNAME +' - '+ (CASE PM.COLLEGE_NO WHEN 0 THEN PM.COLLEGE_NAME ELSE ACM.COLLEGE_NAME END)  AS PLAYERNAME", "PM.SPID = (SELECT SPID FROM SPRT_TEAM_MASTER WHERE TEAMID =" + Convert.ToInt32(ddlTeamName.SelectedValue) + ")", "PM.PLAYERID DESC");
            // where PM.COLLEGE_NO  = (SELECT COLLEGE_NO FROM SPRT_TEAM_MASTER WHERE TEAMID = 6)
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.PopulatePlayerName()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to get the list of role names.
    private void PopulateRoleName()
    {
        try
        {
            objCommon.FillDropDownList(ddlRole, "SPRT_ROLE_MASTER RM INNER JOIN SPRT_SPORT_MASTER SM ON(RM.SPID = SM.SPID)", "RM.ROLEID", "SM.SNAME+' - '+RM.ROLENAME AS ROLENAME ", "", "RM.ROLEID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.PopulateRoleName()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //// This method is used to check alread exist player.
    //private bool checkPlayerExist()
    //{
    //    bool retVal = false;
    //    DataSet ds = objCommon.FillDropDown("SPRT_TEAM_DETAILS", "TDID", "TEAMID", "TEAMID='" + ddlTeamName.SelectedValue + "' AND PLAYERID='" + ddlPlayer.SelectedValue + "' AND ROLEID='" + ddlRole.SelectedValue + "'", "TDID");
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            retVal = true;
    //        }
    //    }
    //    return retVal;
    //}
    // This method is used to check alread exist staff. 
    private bool checkStaffExist()
    {
        int TeamId=Convert.ToInt32(ddlTeamName.SelectedValue);
        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_TEAM_DETAILS", "TDID", "TEAMID", "POSTID='" + ddlPost.SelectedValue + "'", "TDID");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    // This method is used to check alread exist Player. 
    private bool checkPlayerExist()
    {
        bool retVal = false;
        int TeamId = Convert.ToInt32(ddlTeamName.SelectedValue);
        DataSet ds = objCommon.FillDropDown("SPRT_TEAM_DETAILS", "TDID", "TEAMID", "PLAYERID=" + Convert.ToInt32(ddlPlayer.SelectedValue) + " AND ROLEID=" + Convert.ToInt32(ddlRole.SelectedValue) + " And TEAMID="+Convert.ToInt32(ddlTeamName.SelectedValue)+"", "TDID");
        
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }
   
    // This method is used toshow details of module.
    private void ShowDetails(int tmDetail_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_TEAM_DETAILS", "*", "", "TDID=" + tmDetail_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (rdblistPlayerType.SelectedValue == "1")
                {
                    ddlTeamName.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
                    ddlPost.SelectedValue = ds.Tables[0].Rows[0]["POSTID"].ToString();
                }
                else
                {
                    ddlTeamName.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
                    ddlPlayer.SelectedValue = ds.Tables[0].Rows[0]["PLAYERID"].ToString();
                    ddlRole.SelectedValue = ds.Tables[0].Rows[0]["ROLEID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to bind the list.
    private void BindList(int player_post, int teamid)
    {
        try
        {
            DataSet ds = objSportC.GetListPlayerPost(player_post, teamid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (rdblistPlayerType.SelectedItem.Text.Equals("Staff Members") )
                {
                    lvTeamPost.DataSource = ds;
                    lvTeamPost.DataBind();
                }
                else
                {
                    lvTeamPlayer.DataSource = ds;
                    lvTeamPlayer.DataBind();
                }
            }
            else
            {
                lvTeamPost.DataSource = null;
                lvTeamPost.DataBind();

                lvTeamPlayer.DataSource = null;
                lvTeamPlayer.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.Bindlist -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear the controls.
    private void Clear()
    {
        ddlTeamName.SelectedIndex = 0;
        ddlPost.SelectedIndex = 0;
        Post.Visible = true;
        Player.Visible = false;
        ddlPlayer.SelectedIndex = 0;
        Role.Visible = false;
        ddlRole.SelectedIndex = 0;
        rdblistPlayerType.SelectedValue = "1";
        BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()), Convert.ToInt32(ddlTeamName.SelectedValue));
        ViewState["TDID"] = null;
    }
    #endregion


    #region Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objSport.TEAMID = Convert.ToInt32(ddlTeamName.SelectedValue.ToString());

            if (rdblistPlayerType.SelectedItem.Text.Equals("Staff Members"))
            {
                objSport.POSTID = Convert.ToInt32(ddlPost.SelectedValue.ToString());
                objSport.PLAYERID = 0;
                objSport.ROLEID = 0;
            }
            else
            {
                objSport.PLAYERID = Convert.ToInt32(ddlPlayer.SelectedValue.ToString());
                objSport.ROLEID = Convert.ToInt32(ddlRole.SelectedValue.ToString());
                objSport.POSTID = 0;
            }
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["TDID"] == null)
            {
                if (checkStaffExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Staff Member Already Exist.');", true);
                    Clear();
                    return;
                }
                else if (checkPlayerExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Player Already Exist...!!');", true);
                    Clear();
                    return;
                }
                else
                {
                    objSportC.AddUpdateTeamDetails(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Team Details Submitted Successfully.');", true);
                    Clear();
                    BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()), Convert.ToInt32(ddlTeamName.SelectedValue));
                }
            }
            else
            {
                objSport.TDID = Convert.ToInt32(ViewState["TDID"].ToString());
                objSportC.AddUpdateTeamDetails(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Team Details Updated Successfully.');", true);
                Clear();
                BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()), Convert.ToInt32(ddlTeamName.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int tmDetail_id = int.Parse(btnEdit.CommandArgument);
            ViewState["TDID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(tmDetail_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdblistPlayerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistPlayerType.SelectedItem.Text.Equals("Staff Members"))
        {
            Player.Visible = false;
            Role.Visible = false;
            Post.Visible = true;
            BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()),Convert.ToInt32(ddlTeamName.SelectedValue));
            ddlPost.SelectedIndex = 0;
            lvTeamPlayer.Visible = false;
            lvTeamPost.Visible = true;
        }
        else
        {
            Player.Visible = true;
            Role.Visible = true;
            Post.Visible = false;
            ddlPlayer.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()),Convert.ToInt32(ddlTeamName.SelectedValue));
            lvTeamPost.Visible = false;
            lvTeamPlayer.Visible = true;
        }
    }
    protected void ddlTeamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTeamName.SelectedIndex > 0)
        {
            BindList(Convert.ToInt32(rdblistPlayerType.SelectedValue.ToString()), Convert.ToInt32(ddlTeamName.SelectedValue));
            PopulatePlayerName();
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("TeamDetails", "TeamDetailsReport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + ",@P_TEAMID=0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamDetails.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    
}
