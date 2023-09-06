// ================================================================================
// CREATE BY   : MRUNAL SINGH
// CREATE DATE : 09-JUN-2017
// DESCRIPTION : USED TO SEE SUGGESTIONS ON AGENDA ITEMS GIVEN BY COMMITTEE MEMBERS 
// MODIFY DATE :    
// ================================================================================

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
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController OBJmc = new MeetingController();

    public static int pk_agenda_id;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public int InsertUpdate = 0;

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
                    FillCollege();
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
                   // objCommon.FillDropDownList(ddlCommitee, "TBL_MM_RELETIONMASTER RM INNER JOIN TBL_MM_MENBERDETAILS MD ON (RM.FK_MEMBER = MD.PK_CMEMBER) INNER JOIN Tbl_MM_COMITEE MC ON (RM.FK_COMMITEE = MC.ID)", "FK_COMMITEE", "MC.NAME", "MD.USERID = " + Convert.ToInt32(Session["idno"]), "");
                }              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
    }
   

    // This method is used to bind the list View.
    private void BindlistView()
    {
        try
        {
            //DataSet ds = OBJmc.GetAgendaToBind(Convert.ToInt32(ddlCommitee.SelectedValue), ddlpremeeting.SelectedItem.Text, Convert.ToInt32(Session["idno"]));          

            DataSet ds = OBJmc.GetListOfCommitteeMembers(Convert.ToInt32(ddlCommitee.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvMemberList.DataSource = ds;
                lvMemberList.DataBind();
                lvMemberList.Visible = true;
            }
            else
            {
                lvMemberList.DataSource = null;
                lvMemberList.DataBind();
                lvMemberList.Visible = false; 
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue), "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlpremeeting.Items.Clear();
            ddlpremeeting.Items.Add("Please Select");
            ddlpremeeting.SelectedItem.Value = "0";
            ddlpremeeting.DataTextField = "MEETING_CODE";
            ddlpremeeting.DataValueField = "MEETING_CODE";
            ddlpremeeting.DataSource = ds.Tables[0];
            ddlpremeeting.DataBind();
            ddlpremeeting.SelectedIndex = 0;
        }
        else
        {
            ddlpremeeting.Items.Clear();
            ddlpremeeting.DataSource = null;
            ddlpremeeting.DataBind();
        }
    }


 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    // This method is used to clear the controls.
    private void Clear()
    {       
        ddlCollege.SelectedIndex = 0;
        ddlCommitee.SelectedIndex = 0;
        ddlpremeeting.SelectedIndex = 0;
        lvMemberList.Visible = false;
        pnlList.Visible = false;

    }

    protected void ddlpremeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindlistView();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int UserId = int.Parse(btnEdit.CommandArgument);
            ViewState["userid"] = int.Parse(btnEdit.CommandArgument);
            int PkId = int.Parse(btnEdit.CommandName);
            ViewState["action"] = "edit";

            ShowDetails(UserId);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int UserId)
    {
        try
        {
            DataSet ds = OBJmc.GetSuggestionsOnAgenda(UserId, ddlpremeeting.SelectedItem.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAgenda.DataSource = ds;
                lvAgenda.DataBind();
                pnlList.Visible = true;
            }
            else
            {
                lvAgenda.DataSource = null;
                lvAgenda.DataBind();
                pnlList.Visible = false;
                objCommon.DisplayMessage(this.updActivity, "Suggestions On Agenda Is Not Given.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCommitee.SelectedIndex > 0 && ddlpremeeting.SelectedIndex > 0)
            {
                ShowReport("pdf", "AgendaSuggestionsReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Committee & Meeting.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=AgendaSuggestionsReport" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_COMMITTEEID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + ",@P_MEETING_CODE=" + ddlpremeeting.SelectedItem.Text;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaItemsSuggestedByMembers.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbCommitteeType.SelectedValue == "U")
            {
                trCollegeName.Visible = false;
                ddlCollege.SelectedIndex = 0;
                objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
            }
            else
            {
                trCollegeName.Visible = true;
                objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}