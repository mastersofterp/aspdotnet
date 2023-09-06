//==========================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 09-DEC-2014
//DESCRIPTION   : THIS FORM IS USED TO CREATE EVENT WISE SPORTS.    
//MODIFY DATE   : 03-MAY-2017
//===========================================================================  
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

public partial class Sports_Transaction_EventCreation : System.Web.UI.Page
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
                    objCommon.FillDropDownList(ddlAcadYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");                  
                    //objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "", "SPID DESC");                   
                    objCommon.FillDropDownList(ddlVenue, "SPRT_VENUE_MASTER", "VENUEID", "VENUENAME", "", "VENUENAME");

                    BindlistView();
                    FillCollege();
                }
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventCreation.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue) , "EVENTID");       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFrmDt.Text.Equals(string.Empty))
            {
                if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtToDt.Text) )
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date...!!');", true);
                    //txtFrmDt.Text = string.Empty;
                    //txtToDt.Text = string.Empty;
                    txtFrmDt.Focus();
                    return;
                }
            }


            objSport.ACAD_YEAR = Convert.ToInt32(ddlAcadYear.SelectedValue);
            objSport.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSport.EVENTID = Convert.ToInt32(ddlEvent.SelectedValue); 
            objSport.TYPID = Convert.ToInt32(ddlSportType.SelectedValue);
            objSport.SPID = Convert.ToInt32(ddlSportName.SelectedValue);
            objSport.VENUEID = Convert.ToInt32(ddlVenue.SelectedValue.ToString());
            objSport.EVENT_FROMDATE = Convert.ToDateTime(txtFrmDt.Text.Trim());
            objSport.EVENT_TODATE = Convert.ToDateTime(txtToDt.Text.Trim());           
            objSport.GAME_DETAILS = txtDetails.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDetails.Text);
            objSport.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);


            if (ViewState["EORGID"] == null)
            {

                objSport.EORGID = 0;

                CustomStatus cs = (CustomStatus)objSportC.AddUpdateEventOrganize(objSport);
                  if (cs.Equals(CustomStatus.RecordExist))
                    {

                       // Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);                    
             
            }
            else
            {
                objSport.EORGID = Convert.ToInt32(ViewState["EORGID"].ToString());
                objSportC.AddUpdateEventOrganize(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
               
            }
            Clear();
            BindlistView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventCreation.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int eOrgid = int.Parse(btnEdit.CommandArgument);
            ViewState["EORGID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(eOrgid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventCreation.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_ORGANIZATION EO INNER JOIN ACD_ADMBATCH AB ON (EO.BATCHNO = AB.BATCHNO) INNER JOIN ACD_COLLEGE_MASTER ACM ON (EO.COLLEGE_NO = ACM.COLLEGE_ID) INNER JOIN SPRT_EVENT_MASTER EM ON (EO.EVENTID = EM.EVENTID) INNER JOIN SPRT_SPORT_TYPE ST ON (EO.TYPID = ST.TYPID)  INNER JOIN SPRT_SPORT_MASTER SM ON (EO.SPID = SM.SPID) INNER JOIN SPRT_VENUE_MASTER VM ON (EO.VENUEID = VM.VENUEID)", "EORGID, AB.BATCHNAME, ACM.COLLEGE_NAME, EM.EVENTNAME, ST.GAME_TYPE, SM.SNAME, VM.VENUENAME", "EO.FROM_DATE, EO.TO_DATE, EO.EVENT_DETAILS, EO.REMARK", "", "EORGID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventOrganize.DataSource = ds;
                lvEventOrganize.DataBind();
            }
            else
            {
                lvEventOrganize.DataSource = null;
                lvEventOrganize.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventCreation.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int eOrgid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_ORGANIZATION", "*", "", "EORGID=" + eOrgid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAcadYear.SelectedValue = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue), "EVENTID");
                ddlEvent.SelectedValue = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                objCommon.FillDropDownList(ddlSportType, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_SPORT_TYPE ST ON (ED.TYPID = ST.TYPID)", "DISTINCT ED.TYPID", "ST.GAME_TYPE", "ED.EVENTID = " + Convert.ToInt32(ddlEvent.SelectedValue), "ED.TYPID"); 
                ddlSportType.SelectedValue = ds.Tables[0].Rows[0]["TYPID"].ToString();
                objCommon.FillDropDownList(ddlSportName, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_SPORT_MASTER SM ON (ED.SPID = SM.SPID)", "ED.SPID", "SM.SNAME", "ED.EVENTID = " + Convert.ToInt32(ddlEvent.SelectedValue) + " AND ED.TYPID = " + Convert.ToInt32(ddlSportType.SelectedValue), "EVENTID");  
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();
                ddlVenue.SelectedValue = ds.Tables[0].Rows[0]["VENUEID"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();               
                txtDetails.Text = ds.Tables[0].Rows[0]["EVENT_DETAILS"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlAcadYear.SelectedIndex = 0;
        ddlSportType.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        ddlVenue.SelectedIndex = 0;
        txtDetails.Text = string.Empty;   
        txtFrmDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        txtRemark.Text = string.Empty;        
        ViewState["EORGID"] = null;
        ddlCollege.SelectedIndex = 0;
        ddlEvent.SelectedIndex = 0;

    } 
    protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
    { 
        objCommon.FillDropDownList(ddlSportType, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_SPORT_TYPE ST ON (ED.TYPID = ST.TYPID)", "DISTINCT ED.TYPID", "ST.GAME_TYPE", "ED.EVENTID = " + Convert.ToInt32(ddlEvent.SelectedValue), "ED.TYPID"); 
    }

    protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSportName, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_SPORT_MASTER SM ON (ED.SPID = SM.SPID)", "DISTINCT ED.SPID", "SM.SNAME", "ED.EVENTID = " + Convert.ToInt32(ddlEvent.SelectedValue) + " AND ED.TYPID = " + Convert.ToInt32(ddlSportType.SelectedValue), "");  
    }


    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("EventActivities", "EventActivitiesDetails.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + ",@P_EORGID=0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_EventCreation.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
