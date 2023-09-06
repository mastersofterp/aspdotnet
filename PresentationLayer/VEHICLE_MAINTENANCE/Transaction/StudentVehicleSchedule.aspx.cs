using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VEHICLE_MAINTENANCE_Transaction_StudentVehicleSchedule : System.Web.UI.Page
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
                    lvRootDetails.Visible = false;
                    btnrootdetails.Visible = true;
                    btnBack.Visible = false;
                  
                 
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_StudentVehicleSchedule.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_SCHEDULE", "SCHEDULEID,SCHEDULE_DATE, MORNING_TRIP, SPECIAL_TRIP, EVENING_TRIP", "LATE_TRIP", "", "SCHEDULE_DATE DESC");
              if (ds.Tables[0].Rows.Count > 0)
                {
                    lvVehicleSchedule.DataSource = ds;
                    lvVehicleSchedule.DataBind();
                    lvVehicleSchedule.Visible = true;
                }
                else
                {
                    lvVehicleSchedule.DataSource = null;
                    lvVehicleSchedule.DataBind();
                    lvVehicleSchedule.Visible = true;
                }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_StudentVehicleSchedule.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    

    protected void btnRootDetails_Click(object sender, EventArgs e)
    {
        try
        {
            BindRootDetaillistView();
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_StudentVehicleSchedule.btnRootDetails_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        lvRootDetails.Visible = false;
        btnrootdetails.Visible = true;
        BindlistView();
        btnBack.Visible = false;
        lvVehicleSchedule.Visible = true;
       
        
    }

     private void BindRootDetaillistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "ROUTEID, ROUTENAME, SEQNO, ROUTEPATH", "ROUTECODE,	DISTANCE,	STARTING_TIME,	ROUTE_NUMBER,CASE WHEN VEHICLE_TYPE=1 THEN 'A/c' ELSE 'Non A/c' END AS VEHICLE_TYPE", "", "ROUTE_NUMBER DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRootDetails.Visible = true;
                lvRootDetails.DataSource = ds;
                lvRootDetails.DataBind();
                lvVehicleSchedule.Visible = false;
                btnrootdetails.Visible = false;
                btnBack.Visible = true;
                
            }
            else
            {
                lvRootDetails.DataSource = null;
                lvRootDetails.DataBind();
                lvRootDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_StudentVehicleSchedule.BindRootDetaillistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

     

}