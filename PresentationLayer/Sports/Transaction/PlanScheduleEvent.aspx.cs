//======================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS AND EVENT MANAGEMENT
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 24-APR-2017
//DESCRIPTION   : THIS FORM IS USED TO CREATE PLAN & SCHEDULE FOR EVENTS.    
//=======================================================================
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

public partial class Sports_Transaction_PlanScheduleEvent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport = new Sport();

    PlanSchedule objPS = new PlanSchedule();
    PlanScheduleCon objPSCon = new PlanScheduleCon();

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
                    objCommon.FillDropDownList(ddlEventType, "SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "", "ETID DESC");
                   // objCommon.FillDropDownList(ddlTeam, "SPRT_TEAM_MASTER", "TEAMID", "TEAMNAME", "", "TEAMNAME");
                    objCommon.FillDropDownList(ddlTeam, "SPRT_TEAM_MASTER TM LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID", "TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "", "TEAMNAME");
                    BindlistView();
                    FillCollege();
                    
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_PlanScheduleEvent.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFrmDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date...!!');", true);
                    txtFrmDt.Text = string.Empty;
                    txtToDt.Text = string.Empty;
                    txtFrmDt.Focus();
                    return;
                }
            }

            objPS.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objPS.ETID = Convert.ToInt32(ddlEventType.SelectedValue);
            objPS.EVENTID = Convert.ToInt32(ddlEvent.SelectedValue);
            objPS.FROMDATE = Convert.ToDateTime(txtFrmDt.Text); 
            objPS.TODATE = Convert.ToDateTime(txtToDt.Text);
            objPS.TEAMID = Convert.ToInt32(ddlTeam.SelectedValue);           
            objPS.USERID = Convert.ToInt32(Session["userno"]);
            objPS.COLLEGE_CODE = Session["colcode"].ToString();
            if (txtAAPath.Text.Equals(string.Empty))
            {
                objPS.PAPNO = 0;
            }
            else
            {
                objPS.PAPNO = Convert.ToInt32(ViewState["papno"]);
                
            }
          
        

            if (ViewState["PSID"] == null)
            {
                objPS.PSID = 0;
                CustomStatus cs = (CustomStatus)objPSCon.AddUpdatePlanSchedule(objPS);
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

                objPS.PSID = Convert.ToInt32(ViewState["PSID"].ToString());
                CustomStatus cs = (CustomStatus)objPSCon.AddUpdatePlanSchedule(objPS);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
            }
            Clear();
            BindlistView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_PlanScheduleEvent.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int psid = int.Parse(btnEdit.CommandArgument);
            ViewState["PSID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(psid);
           
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_PlanScheduleEvent.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            //Modified by Saahil Trivedi 09-02-2022
            DataSet ds = objCommon.FillDropDown("SPRT_PLAN_SCHEDULE PS INNER JOIN SPRT_EVENT_MASTER EC ON (PS.EVENTID = EC.EVENTID) INNER JOIN SPRT_TEAM_MASTER TM ON (PS.TEAMID = TM.TEAMID) INNER JOIN ACD_COLLEGE_MASTER ACM ON (PS.COLLEGE_NO = ACM.COLLEGE_ID)", "PS.PSID, PS.FROM_DATE, PS.TO_DATE", "EC.EVENTNAME, TM.TEAMNAME, ACM.COLLEGE_NAME,(CASE PS.[STATUS] WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' ELSE 'Pending' END) AS ASTATUS", "", "PSID DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEvent.DataSource = ds;
                lvEvent.DataBind();
                lvEvent.Visible = true;
                ViewState["PSID"] = null;
            }
            else
            {
                lvEvent.DataSource = null;
                lvEvent.DataBind();
                lvEvent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_PlanScheduleEvent.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int psid)
    {
        try
        {
            DataSet dss = objCommon.FillDropDown("SPRT_PLAN_SCHEDULE PS INNER JOIN SPRT_EVENT_MASTER EC ON (PS.EVENTID = EC.EVENTID) INNER JOIN SPRT_TEAM_MASTER TM ON (PS.TEAMID = TM.TEAMID) INNER JOIN ACD_COLLEGE_MASTER ACM ON (PS.COLLEGE_NO = ACM.COLLEGE_ID)", "PS.PSID, PS.FROM_DATE, PS.TO_DATE", "EC.EVENTNAME, TM.TEAMNAME, ACM.COLLEGE_NAME, PS.[STATUS]", "PSID=" + psid, "PSID DESC");
            string status = dss.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Approved Record Cannot Be Modify.');", true);
                return;
            }


            DataSet ds = objCommon.FillDropDown("SPRT_PLAN_SCHEDULE", "*", "", "PSID=" + psid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["ETID"].ToString();
                objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID=" + Convert.ToInt32(ddlEventType.SelectedValue) + "AND COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue), "EVENTID DESC");
                ddlEvent.SelectedValue = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                ddlTeam.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
                trEventName.Visible = true;
                GetApprovalAuthorityPath();


            }
            else
            {
                trEventName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_PlanScheduleEvent.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlEvent.SelectedIndex = 0;
        trEventName.Visible = false;
        txtFrmDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        ViewState["PSID"] = null;
        ddlTeam.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        txtAAPath.Text = string.Empty;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Plan & Schedule Event", "PlanScheduleEventReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Masters_EventMaster.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=PlanScheduleEventReport.pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_PSID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID=" + Convert.ToInt32(ddlEventType.SelectedValue) + "AND COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue), "EVENTID DESC");
            trEventName.Visible = true;
        }
        else
        {
            trEventName.Visible = false;
        }
    }


    protected void GetApprovalAuthorityPath()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();
            DataSet dsAuth = new DataSet();
            int useridno = Convert.ToInt32(Session["idno"]);

            //int collegeno = Convert.ToInt32(ViewState["COLLEGE_NO"]);
            //dsAuth = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno + "  AND COLLEGE_NO=" + collegeno + " ", "");


            //DataSet dsdept = new DataSet();
            //dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
            //string dept = dsdept.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();

            DataSet dspath = new DataSet();

            dspath = objCommon.FillDropDown("SPRT_APPROVAL_AUTHORITY_PATH", "*", "", " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND EVENTID =" + Convert.ToInt32(ddlEvent.SelectedValue) + "", "");


            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();


                string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
                string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
                string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
                string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
                string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();


                string uano1 = string.Empty;
                string uano2 = string.Empty;
                string uano3 = string.Empty;
                string uano4 = string.Empty;
                string uano5 = string.Empty;
                string paname1 = string.Empty;
                string paname2 = string.Empty;
                string paname3 = string.Empty;
                string paname4 = string.Empty;
                string paname5 = string.Empty;

                DataSet dsauth1 = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
                if (dsauth1.Tables[0].Rows.Count > 0)
                {
                    uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth2 = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
                if (dsauth2.Tables[0].Rows.Count > 0)
                {
                    uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth3 = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
                if (dsauth3.Tables[0].Rows.Count > 0)
                {
                    uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth4 = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
                if (dsauth4.Tables[0].Rows.Count > 0)
                {
                    uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth5 = objCommon.FillDropDown("SPRT_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
                if (dsauth5.Tables[0].Rows.Count > 0)
                {
                    uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();
                }


                if (userno == uano1)
                {
                    path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano2)
                {
                    path = paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano3)
                {
                    path = paname4 + "->" + paname5;
                }
                else if (userno == uano4)
                {
                    path = paname5;
                }
                else if (userno == uano5)
                {
                    path = paname5;
                }
                else
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                txtAAPath.Text = path;

            }
            else
            {
                MessageBox("Sorry! Approval Authority Not found");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_PlanScheduleEvent.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetApprovalAuthorityPath();
    }
}