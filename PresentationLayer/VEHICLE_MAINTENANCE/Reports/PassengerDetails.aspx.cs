//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PassengerDetails.aspx
// CREATION DATE : 06-AUG-2021
// CREATED BY    : MRUNAL SINGH
// DESCRIPTION   : PASSENGER DETAILS REPORT
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;

using System.Text;


public partial class VEHICLE_MAINTENANCE_Reports_PassengerDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();


    #region   Page Load Event

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
                    //this.GetTransportStatus();                    
                    FillDropDownList();
                    ViewState["action"] = "add";
                    //BindlistView();                    
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion
    #region UserDefined Methods

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

    protected void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;
    }

    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO >0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME", "", "ROUTEID");        
    }

    //private void BindlistView()
    //{
    //    try
    //    {
    //        DataSet dss = objVMC.GetTransportRequisitionApplicationList(Convert.ToInt32(Session["idno"]));
    //        if (dss.Tables[0].Rows.Count > 0)
    //        {
    //            //ViewState["GRIV_ID"] = dss.Tables[0].Rows[0]["GRIV_ID"].ToString();
    //            lvTRApplication.DataSource = dss;
    //            lvTRApplication.DataBind();
    //        }
    //        else
    //        {
    //            lvTRApplication.DataSource = null;
    //            lvTRApplication.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.BindlistView -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    //private void GetTransportStatus()
    //{
    //    try
    //    {
    //        string TransportStatus = objCommon.LookUp("ACD_STUDENT", "TRANSPORT", "IDNO=" + Convert.ToInt32(Session["idno"]));
    //        if (TransportStatus == "0")
    //        {
    //            btnSave.Enabled = false;
    //            objCommon.DisplayMessage(this.updApplication, "Transport status is not updated", this.Page);
    //        }
    //        else
    //        {
    //            btnSave.Enabled = true;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.ShowDetails() -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    #endregion


    #region Page Event

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
           ShowReport("Passenger Details", "rptVehiclePassangerReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.btnReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


   

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ROUTEID=" + Convert.ToInt32(ddlRoute.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) ;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.ShowReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnBusDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRoute.SelectedValue != "0")
            {
                ShowBusDetailsReport("Bus Details", "rptBusDetailsReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updApplication, "Please Select Route.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.btnBusDetails_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowBusDetailsReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ROUTEID=" + Convert.ToInt32(ddlRoute.SelectedValue);

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_PassengerDetails.ShowBusDetailsReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion






   
}