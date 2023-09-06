//==============================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 01-JUL-2019
// DESCRIPTION   : TO ADD VEHICLE SCHEDULE.
//===============================================================

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

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleSchedule : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();

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
                    BindlistView();
                }               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleSchedule.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to check the authorization of page.
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

    // This event is used to Submit the details of Hired vehicle.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.SCHEDULEID = 0;
            if (txtDate.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Date');", true);
                return; 
            }
            objVM.SCHEDULE_DATE = Convert.ToDateTime(txtDate.Text);
            objVM.MORNING_TRIP = txtMorTrip.Text.Trim().Equals(string.Empty) ? string.Empty : txtMorTrip.Text.Trim();
            objVM.SPECIAL_TRIP = txtSpeTrip.Text.Trim().Equals(string.Empty) ? string.Empty : txtSpeTrip.Text.Trim();
            objVM.EVENING_TRIP = txtEveTrip.Text.Trim().Equals(string.Empty) ? string.Empty : txtEveTrip.Text.Trim();
            objVM.LATE_TRIP = txtLateTrip.Text.Trim().Equals(string.Empty) ? string.Empty : txtLateTrip.Text.Trim();
            objVM.USERNO = Convert.ToInt32(Session["userno"]);


            if (ViewState["SCHEDULEID"] == null)
            {
                CustomStatus cs = (CustomStatus)objVMC.AddUpdateSchedule(objVM);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);                  
                    return;
                }
                BindlistView();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                Clear();
            }
            else
            {
                objVM.SCHEDULEID = Convert.ToInt32(ViewState["SCHEDULEID"].ToString());
                CustomStatus cs = (CustomStatus)objVMC.AddUpdateSchedule(objVM);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                    return;
                }
                BindlistView();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                Clear();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleSchedule.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This event is used to cancel the last selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //This event is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ScheduleId = int.Parse(btnEdit.CommandArgument);
            ViewState["SCHEDULEID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(ScheduleId);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleSchedule.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to bind the list 
    private void BindlistView()
    {      
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_SCHEDULE", "SCHEDULEID,SCHEDULE_DATE, MORNING_TRIP, SPECIAL_TRIP, EVENING_TRIP", "LATE_TRIP", "", "SCHEDULEID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSchedule.DataSource = ds;
                lvSchedule.DataBind();                
            }
            else
            {
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleSchedule.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This method is used to show the details of the selected record.
    private void ShowDetails(int ScheduleId)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_SCHEDULE", "*", "", "SCHEDULEID=" + ScheduleId, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDate.Text = ds.Tables[0].Rows[0]["SCHEDULE_DATE"].ToString();
                txtMorTrip.Text = ds.Tables[0].Rows[0]["MORNING_TRIP"].ToString();
                txtSpeTrip.Text = ds.Tables[0].Rows[0]["SPECIAL_TRIP"].ToString();
                txtEveTrip.Text = ds.Tables[0].Rows[0]["EVENING_TRIP"].ToString();
                txtLateTrip.Text = ds.Tables[0].Rows[0]["LATE_TRIP"].ToString();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleSchedule.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear the controles.
    private void Clear()
    {
        txtDate.Text = string.Empty;
        txtMorTrip.Text = string.Empty;
        txtSpeTrip.Text = string.Empty;
        ViewState["SCHEDULEID"] = null;
        txtEveTrip.Text = string.Empty;
        txtLateTrip.Text = string.Empty;     
    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        
        ShowReport("VehicleSchedule", "rptVehicleScheduleDetails.rpt");
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=VehicleSchedule.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_SCHEDULEID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleSchedule.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }   
}