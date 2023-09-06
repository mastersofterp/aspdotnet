//===========================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 04-09-2014
// DESCRIPTION   : USED TO CREATE TEAM NAME
//MODIFY DATE    : 03-05-2017
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

public partial class Sports_Masters_TeamMaster : System.Web.UI.Page
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
                    FillCollege();
                    BindlistView(Convert.ToChar(rdbTeamType.SelectedValue));
                    PopulateAcadYear();   
                }
                            
            }          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSportType.SelectedIndex > 0)
        {
            PopulateSportName();
            ddlSportName.Focus();
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }

    }

    private void PopulateAcadYear()
    {
        try
        {
            objCommon.FillDropDownList(ddlAcadYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.PopulateAcadYear()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateSportName()
    {
        try
        {
            objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID=" + ddlSportType.SelectedValue , "SPID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.PopulateSportName()-> " + ex.Message + " " + ex.StackTrace);
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

    private bool checkTeamExist()
    {
        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_TEAM_MASTER", "TEAMID", "TEAMNAME", "TEAMNAME='" + txtTeamName.Text + "'", "TEAMID");
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
            objSport.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSport.COLLEGE_NAME = txtCollegeName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtCollegeName.Text);
            objSport.ACAD_YEAR = Convert.ToInt32(ddlAcadYear.SelectedValue);
            objSport.SPID = Convert.ToInt32(ddlSportName.SelectedValue);
            objSport.TEAMNAME = txtTeamName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtTeamName.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);
            objSport.TEAM_TYPE = Convert.ToChar(rdbTeamType.SelectedValue);
            objSport.TYPID = Convert.ToInt32(ddlSportType.SelectedValue);

            int ret = Convert.ToInt32(objCommon.LookUp("SPRT_TEAM_MASTER", "count(*)", "COLLEGE_NO =" + objSport.COLLEGE_NO + "and TEAMNAME='"+objSport.TEAMNAME+"' and SPID=" + objSport.SPID + "and ACAD_YEAR=" + objSport.ACAD_YEAR + " and TYPID=" + objSport.TYPID));
           // int ret = Convert.ToInt32(objCommon.LookUp("SPRT_TEAM_MASTER", "count(*)", "TEAMNAME =" + objSport.TEAMNAME));
            if (ret == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                return;
            }

            if (ViewState["TEAMID"] == null)
            {
                    objSportC.AddUpdateTeamMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Team Name Submitted Successfully.');", true);
                    Clear();
                
            }
            else
            {
                objSport.TEAMID = Convert.ToInt32(ViewState["TEAMID"].ToString());
                objSportC.AddUpdateTeamMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Team Name Updated Successfully.');", true);
                Clear();
            }
            BindlistView(Convert.ToChar(rdbTeamType.SelectedValue));
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlAcadYear.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        txtTeamName.Text = string.Empty;
        ddlSportType.SelectedIndex = 0;
        ViewState["TEAMID"] = null;
        ddlCollege.SelectedIndex = 0;
        txtCollegeName.Text = string.Empty;
      //  rdbTeamType.SelectedIndex = 0;
      //  trCollegeName.Visible = false;
        //BindlistView(Convert.ToChar(rdbTeamType.SelectedValue));
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
            int team_id = int.Parse(btnEdit.CommandArgument);
            ViewState["TEAMID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(team_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int team_id)
    {
        try
        {            
            DataSet ds = objCommon.FillDropDown("SPRT_TEAM_MASTER T INNER JOIN SPRT_SPORT_MASTER S ON (T.SPID = S.SPID) LEFT JOIN ACD_COLLEGE_MASTER CM ON (T.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID,T.ACAD_YEAR,T.COLLEGE_NO,TEAM_TYPE", "TEAMNAME, T.SPID, T.TYPID,(CASE T.COLLEGE_NO WHEN 0 THEN T.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS COLLEGE_NAME", "TEAMID=" + team_id, "");
           if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TEAM_TYPE"].ToString() == "U")
                {
                    ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                    trCollegeName.Visible = false;
                }
                else
                {
                    txtCollegeName.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    trCollegeName.Visible = true;
                }

                ddlAcadYear.SelectedValue = ds.Tables[0].Rows[0]["ACAD_YEAR"].ToString();
                ddlSportType.SelectedValue = ds.Tables[0].Rows[0]["TYPID"].ToString();
                objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID=" + ddlSportType.SelectedValue, "SPID DESC");
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();               
                txtTeamName.Text = ds.Tables[0].Rows[0]["TEAMNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView(char Team_Type)
    {
        try
        {           
            DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER ST INNER JOIN SPRT_TEAM_MASTER TM ON (ST.SPID = TM.SPID)  INNER JOIN ACD_ADMBATCH B ON (TM.ACAD_YEAR = B.BATCHNO)  LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "ST.SNAME,TM.TEAMNAME,TM.TEAMID,TM.ACAD_YEAR,B.BATCHNAME", "(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS COLLEGE_NAME", "TM.TEAM_TYPE ='" + Convert.ToChar(rdbTeamType.SelectedValue) +"'", "TM.TEAMID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTeam.DataSource = ds;
                lvTeam.DataBind();
            }
            else
            {
                lvTeam.DataSource = null;
                lvTeam.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindlistView(Convert.ToInt32(ddlCollege.SelectedValue));
    }
    protected void rdbTeamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbTeamType.SelectedValue == "O")
            {
                trCollegeNo.Visible = false;
                trCollegeName.Visible = true;               
            }
            else
            {
                trCollegeNo.Visible = true;
                trCollegeName.Visible = false;              
            }
            BindlistView(Convert.ToChar(rdbTeamType.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.rdbTeamType_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("TeamList", "TeamList.rpt");
    }
    
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + "," + "@P_TEAMID=0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
