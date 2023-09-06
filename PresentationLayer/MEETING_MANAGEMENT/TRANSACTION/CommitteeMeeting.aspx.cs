//======================================================================
// MODIFY BY      : MRUNAL SINGH                              
// MODIFYING DATE : 01-12-2014                                          
// DESCRIPTION    : TO TAKE THE FACULTIES FROM PAYROLL AND TRY TO 
//                  GET THE IDNO OF FACULTY.(SMS, EMAIL)                   
// MODIFYING DATE : 16-02-2015                                          
// DESCRIPTION    : ADD DEPTNO OF THE USER. 
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
using System.Configuration;
using System.IO;

public partial class MEETING_MANAGEMENT_TRANSACTION_CommitteeMeeting : System.Web.UI.Page
{
   Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();
    public static int pk_agenda_id;   
   
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
                    FILL_DROPDOWN();
                }
            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Transaction_CommitteeMeeting.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    // This method is used for page authoization.
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
    // This method is used to fill Committee List.
    public void FILL_DROPDOWN()
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "DEPTNO=" + Session["userEmpDeptno"] + " OR " + Session["userEmpDeptno"] + "=0", "NAME");
    }    
    
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    // This method is used to clear the controls.
    private void clear()
    {
        ddlCommitee.SelectedIndex = 0;
        txtstartdate.Text = string.Empty;
        txtenddate.Text = string.Empty;        
    }    

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {           
            ShowReport("pdf", "CommitteeMeeting.rpt");
            clear();           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Transaction_CommitteeMeeting.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }        
    }
    // This method is used to Generate report.
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
           
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=CommitteeMeeting" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;

            if (txtstartdate.Text == string.Empty && txtenddate.Text == string.Empty)
            {
                url += "&param=@P_COMMITTEE=" + ddlCommitee.SelectedValue + ",@P_REPORTTYPE=1" + ",@P_FDATE=null"+",@P_TDATE=null";                
            }
            else
            {
                url += "&param=@P_COMMITTEE=" + ddlCommitee.SelectedValue + ",@P_FDATE=" + Convert.ToDateTime(txtstartdate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MM-dd") + ",@P_REPORTTYPE=2" ;
            }
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_Transaction_CommitteeMeeting.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   

}
