//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 31-DEC-2014
//DESCRIPTION   : THIS FORM IS USED TO CREATE EVENT WISE SPORTS.  
//MODIFY DATE   : 02-MAY-2017  
//================================================================================================  
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

public partial class Sports_Transaction_EventDetails : System.Web.UI.Page
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
                    objCommon.FillDropDownList(ddlEventType, "SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "", "EVENT_TYPE_NAME");
                    objCommon.FillDropDownList(ddlSportType, "SPRT_SPORT_TYPE", "TYPID", "GAME_TYPE", "", "GAME_TYPE");
                    objCommon.FillDropDownList(ddlTeam, "SPRT_TEAM_MASTER TM LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID", "TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "", "TEAMNAME");
                }
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventDetails.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            objSport.EVENTID = Convert.ToInt32(ddlEvent.SelectedValue);
            objSport.SPID = Convert.ToInt32(ddlSportName.SelectedValue);
            objSport.TEAMID = Convert.ToInt32(ddlTeam.SelectedValue);
            objSport.USERID = Convert.ToInt32(Session["userno"]);
            objSport.ETID = Convert.ToInt32(ddlEventType.SelectedValue);
            objSport.TYPID = Convert.ToInt32(ddlSportType.SelectedValue);

            if (ViewState["EDID"] == null)
            {

                objSport.EDID = 0;

                CustomStatus cs = (CustomStatus)objSportC.AddUpdate_EventDetails(objSport);
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
                objSport.EDID = Convert.ToInt32(ViewState["EDID"].ToString());
                objSportC.AddUpdate_EventDetails(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);

            }
            Clear();
            BindlistView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventDetails.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //BindlistView();
    }

   

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int edid = int.Parse(btnEdit.CommandArgument);
            ViewState["EDID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(edid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventDetails.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int edid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_DETAILS", "*", "", "EDID=" + edid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["ETID"].ToString();
                objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID = " + Convert.ToInt32(ddlEventType.SelectedValue), "EVENTNAME");
                ddlEvent.SelectedValue = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                ddlSportType.SelectedValue = ds.Tables[0].Rows[0]["TYPID"].ToString();
                objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID = " + Convert.ToInt32(ddlSportType.SelectedValue), "SPID");
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();
                ddlTeam.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventDetails.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_DETAILS ED INNER JOIN [dbo].[SPRT_EVENT_TYPE_MASTER] ET ON (ED.ETID = ET.ETID) INNER JOIN SPRT_EVENT_MASTER EM ON (ED.EVENTID = EM.EVENTID) INNER JOIN SPRT_SPORT_TYPE ST ON (ED.TYPID = ST.TYPID) INNER JOIN SPRT_SPORT_MASTER SM ON (ED.SPID = SM.SPID) INNER JOIN SPRT_TEAM_MASTER TM ON (ED.TEAMID = TM.TEAMID) LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "ED.EDID,ET.EVENT_TYPE_NAME,EM.EVENTNAME, ST.GAME_TYPE,SM.SNAME, TM.TEAMNAME", "(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS COLLEGE_NAME", "", "EDID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventDetails.DataSource = ds;
                lvEventDetails.DataBind();
            }
            else
            {
                lvEventDetails.DataSource = null;
                lvEventDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventDetails.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    private void Clear()
    {
        ddlSportName.SelectedIndex = 0;
        //ddlVenue.SelectedIndex = 0;
        ddlEvent.SelectedIndex = 0;       
        ViewState["EDID"] = null;
        ddlSportType.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        ddlTeam.SelectedIndex = 0;
    }

    protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID = " + Convert.ToInt32(ddlEventType.SelectedValue), "EVENTNAME");
    }

    protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID = " + Convert.ToInt32(ddlSportType.SelectedValue), "SPID");
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("Sport&TeamAllotment", "SportTeamAllotmentDetails.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + ",@P_EDID=0";

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

}
