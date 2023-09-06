// ============================================================================
// CREATE BY   :      
// CREATE DATE : 03/09/2014
// MODIFY DATE : 15/09/2014
// MODIFY BY   : MRUNAL SINGH
// DESCRIPTION : USED TO INSERT UPDATE AGENDA INFORMATION FOR THE COMMITEE
//               BACK DATED ENTRIES SHOULD BE ACCEPT WHEN CREATING AGENDA. 
// (06-JUN-2017) ADD SEND NOTIFICATION FUNCTIONALITY TO THE COMMITTEE MEMBERS
// =============================================================================
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
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;


public partial class MEETING_MANAGEMENT_EmailReceived : System.Web.UI.Page
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
                //if (Session["userno"] == null || Session["username"] == null ||
                //    Session["usertype"] == null || Session["userfullname"] == null)
                //{
                //    Response.Redirect("~/default.aspx");
                //}
                //else
                //{
                //    //Page Authorization
                //   // this.CheckPageAuthorization();

                //    //Set the Page Title
                //   // Page.Title = Session["coll_name"].ToString();

                //    //Load Page Help
                //    //if (Request.QueryString["pageno"] != null)
                //    //{
                //    //    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //    //}

                //    ViewState["action"] = "add";
                //}

                // lblName.Text = "Thank You Very Much For Giving Confirmation Of Receiving Of Mail.";
                if (Request.QueryString["uid"] != null)
                {
                    int USERID = Convert.ToInt32(Request.QueryString["uid"]);
                    int COMMITTEEID = Convert.ToInt32(Request.QueryString["CommitteeId"]);
                    string RECEIVE_TYPE = Request.QueryString["receivedType"].ToString();

                   // string MEETING_CODE = Request.QueryString["MeetingCode"].ToString();
                    OBJmc.UpdateEmailReceivedConfirmation(USERID, COMMITTEEID, RECEIVE_TYPE);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_EmailReceived.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}