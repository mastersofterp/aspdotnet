    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using IITMS;
    using IITMS.UAIMS;
    using IITMS.UAIMS.BusinessLayer.BusinessLogic;

    public partial class ADMINISTRATION_EventCalendar : System.Web.UI.Page
    {
        Common objCommon = new Common();
        UAIMS_Common objUaimsCommon = new UAIMS_Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    // Check User Session
                    if (Session["userno"] == null || Session["username"] == null ||
                        Session["usertype"] == null || Session["userfullname"] == null)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                   
                    }
                
                }
           
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "EventCalender.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }

   
    }

