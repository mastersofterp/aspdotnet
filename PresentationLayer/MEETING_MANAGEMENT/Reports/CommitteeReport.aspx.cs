//=============================================
//MODIFIED BY    : MRUNAL SINGH
//MODIFIED DATE  : 14-01-2015
//DESCRIPTION    : NOT ALLOW TO TAKE EPORT IN CRYATAL VIEWER. 
//=============================================
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

public partial class MEETING_MANAGEMENT_Reports_CommitteeReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController OBJmc = new MeetingController();

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
                    ViewState["action"] = "add";

                    Fill_DropDown();                 
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

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
    // this button is used to get the Committee list report.
    protected void btnCLReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowCommitteeListReport("pdf", "CommitteeList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this button is used to get the Committee Member list report.
    protected void btnCMLReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCommittee.SelectedIndex > 0)
            {
                ShowCommitteeMembersReport("pdf", "CommitteeMemberList.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Committee.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this button is used to get All Member list report.
    protected void btnAllMember_Click(object sender, EventArgs e)
    {
        try
        {
            ShowAllMembersReport("pdf", "AllMemberList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this button is used to get the meeting details report.
    protected void btnMeetingDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCommittee.SelectedIndex > 0 && ddlMeeting.SelectedIndex > 0)
            {
                ShowMeetingDetailReport("pdf", "meetingDetails.rpt");
            }
            
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please select meeting", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
   
    private void Clear()
    {
        ddlCommittee.SelectedIndex = 0;
        ddlMeeting.SelectedIndex = 0;
    }

    public void Fill_DropDown()
    {
        //objCommon.FillDropDownList(ddlCommittee, "Tbl_MM_COMITEE", "ID", "NAME","", "ID");    
        //objCommon.FillDropDownList(ddlCommittee, "Tbl_MM_COMITEE", "ID", "NAME", "STATUS=0 AND (DEPTNO=" + Session["UA_EmpDeptNo"] + " OR " + Session["UA_EmpDeptNo"] + "=0)", "");
        //objCommon.FillDropDownList(ddlCommittee, "Tbl_MM_COMITEE", "ID", "NAME", "STATUS=0 AND (DEPTNO=" + Session["UA_EmpDeptNo"] + " OR " + Session["UA_EmpDeptNo"] + "=0)", "");
   //shaikh juned (30-06-2022)    // objCommon.FillDropDownList(ddlCommittee, "Tbl_MM_COMITEE", "ID", "NAME", "STATUS=0 AND (DEPTNO=" + Session["userEmpDeptno"] + " OR " + Session["userEmpDeptno"] + "=0)", "");

        if (Convert.ToInt32(Session["usertype"]) == 1)  //shaikh juned 30-06-2022 ---start
        {
            objCommon.FillDropDownList(ddlCommittee, "Tbl_MM_COMITEE", "ID", "NAME", "STATUS=0 AND (DEPTNO= 0 or 0=0 )", "");
        }
        else
        {
            objCommon.FillDropDownList(ddlCommittee, "Tbl_MM_COMITEE", "ID", "NAME", "STATUS=0 AND (DEPTNO=" + Session["userEmpDeptno"] + " OR " + Session["userEmpDeptno"] + "=0)", "");

        }               //shaikh juned 30-06-2022 ---end
        
        
        //if (Convert.ToInt32(Session["usertype"]) == 1)
        //    objCommon.FillDropDownList(ddlCommittee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
        //else
        //    objCommon.FillDropDownList(ddlCommittee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");
    }
    protected void ddlCommittee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCommittee.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlMeeting, "TBL_MM_AGENDA AG", "DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommittee.SelectedValue) + "' AND AG.MEETING_CODE IN(SELECT DISTINCT METTINGCODE FROM TBL_MM_MEETINGDETAILS WHERE FK_COMMITTE='" + Convert.ToInt32(ddlCommittee.SelectedValue) + "' AND LOCK_MEET='Y')", "");
            trMeeting.Visible = true;
        }
        else    //Shaikh Juned 08-09-2022
        {
            ddlMeeting.SelectedValue = "0";
            trMeeting.Visible = false;
        }
    }
    
    private void ShowCommitteeListReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));         
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=CommitteeList" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;           
            //url += "&param=@p_college_code=" + Session["colcode"].ToString();
            url += "&param=@P_DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]);
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.ShowCommitteeListReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowCommitteeMembersReport(string exporttype, string rptFileName)
    {
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=CommitteeMemberList" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_COMMITTEE_TYPE=" + ddlCommittee.SelectedValue;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.ShowCommitteeMembersReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowAllMembersReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=AllMembersList" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.ShowAllMembersReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowMeetingDetailReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=MinutesOfMeeting" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_MEETINGCODE=" + ddlMeeting.SelectedItem.Text + ",@P_COMMITTEE=" + ddlCommittee.SelectedValue;
          //  url += "&param=@P_MEETINGCODE=" + ddlMeeting.SelectedItem.Text + ",@P_COMMITTEE=" + ddlCommittee.SelectedValue + ",@P_MEETINGCODE=" + ddlMeeting.SelectedItem.Text + ",@P_COMMITTEE=" + ddlCommittee.SelectedValue + ",@P_MEETINGCODE=" + ddlMeeting.SelectedItem.Text + ",@P_COMMITTEE=" + ddlCommittee.SelectedValue;
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.ShowMeetingDetailReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // this button is used to get the meeting details report.
    protected void btnMeetingReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("pdf", "meetingReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.btnMeetingReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string exporttype, string rptFileName)
    {
        if (ddlCommittee.SelectedValue == "0" || ddlMeeting.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(this.Page, "Please select committee and meeting.", this.Page);
            return;
        }
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=MeetingReport" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_MEETINGCODE=" + ddlMeeting.SelectedItem.Text + ",@P_COMMITTEE=" + ddlCommittee.SelectedValue;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Reports_CommitteeReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }   
}
