//=====================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 02-MAY-2017
//DESCRIPTION   : THIS FORM IS USED FOR ALLOTTMENT OF TEAMS FOR EVENT.    
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

public partial class Sports_Transaction_EventTeamAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //SportController objSportC = new SportController();
    //Sport objSport = new Sport();

    EventApprovalEnt objEA = new EventApprovalEnt();
    EventApprovalCon objEACon = new EventApprovalCon();

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
                    objCommon.FillDropDownList(ddlEventType, "SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "", "EVENT_TYPE_NAME");
                   // objCommon.FillDropDownList(ddlTeam, "SPRT_TEAM_MASTER", "TEAMID", "TEAMNAME", "", "TEAMID");
                    objCommon.FillDropDownList(ddlTeam, "SPRT_TEAM_MASTER TM LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID", "TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "", "TEAMNAME");
               
                    objCommon.FillDropDownList(ddlVenue, "SPRT_VENUE_MASTER", "VENUEID", "VENUENAME", "", "VENUENAME");
                }
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventTeamAllotment.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objEA.ETID = Convert.ToInt32(ddlEventType.SelectedValue);
            objEA.EVENTID = Convert.ToInt32(ddlEvent.SelectedValue);
            objEA.TEAMID = Convert.ToInt32(ddlTeam.SelectedValue);
            objEA.VENUEID = Convert.ToInt32(ddlVenue.SelectedValue);
            objEA.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["ETALLOTID"] == null)
            {
                objEA.ETALLOTID = 0;
                CustomStatus cs = (CustomStatus)objEACon.AddUpdateEventTeamAllotment(objEA);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
            }
            else
            {
                objEA.ETALLOTID = Convert.ToInt32(ViewState["ETALLOTID"].ToString());
                objEACon.AddUpdateEventTeamAllotment(objEA);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
            }
            Clear();
            BindlistView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventTeamAllotment.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int etAllotdid = int.Parse(btnEdit.CommandArgument);
            ViewState["ETALLOTID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(etAllotdid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventTeamAllotment.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int etAllotdid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_TEAM_ALLOTMENT", "*", "", "ETALLOTID=" + etAllotdid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["ETID"].ToString();
                objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID = " + Convert.ToInt32(ddlEventType.SelectedValue), "EVENTNAME");
                ddlEvent.SelectedValue = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                ddlTeam.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
                ddlVenue.SelectedValue = ds.Tables[0].Rows[0]["VENUEID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventTeamAllotment.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_TEAM_ALLOTMENT ETA INNER JOIN SPRT_EVENT_MASTER EM ON (ETA.EVENTID = EM.EVENTID)  INNER JOIN SPRT_TEAM_MASTER TM ON (ETA.TEAMID = TM.TEAMID)  INNER JOIN SPRT_VENUE_MASTER VM ON (ETA.VENUEID = VM.VENUEID) LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "ETA.ETALLOTID, EM.EVENTNAME, TM.TEAMNAME", "VM.VENUENAME, (CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS COLLEGE_NAME", "", "ETALLOTID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventTeam.DataSource = ds;
                lvEventTeam.DataBind();
            }
            else
            {
                lvEventTeam.DataSource = null;
                lvEventTeam.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventTeamAllotment.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {

        ddlTeam.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        ddlVenue.SelectedIndex = 0;
        ddlEvent.SelectedIndex = 0;       
       ViewState["ETALLOTID"] =  null;
    }

    protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID = " + Convert.ToInt32(ddlEventType.SelectedValue), "EVENTNAME");
    }
}