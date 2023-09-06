//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : vehicle
// PAGE NAME     : studentTransport.aspx                                                
// CREATION DATE : 01-MAR-2020                                                        
// CREATED BY    : ANDPOJ VIJAY                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Web;
using System.IO;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class VEHICLE_MAINTENANCE_Transaction_StudentTransportCancellation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController ObjCon = new VMController();
    VM ObjEnt = new VM();

 
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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    Clear();
                    FillDropDwon();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_calibration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void FillDropDwon()
    {
        try
        {
            objCommon.FillDropDownList(ddlyear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR<>0", "YEARNAME");
            objCommon.FillDropDownList(ddlsem, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO<>0", "SEMFULLNAME");
            objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO<>0", "LONGNAME");
            objCommon.FillDropDownList(ddldegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO<>0", "DEGREENAME");
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_calibration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
 
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
  
    protected void btnrpt_Click(object sender, EventArgs e)
    {
        ShowTransportReport("StudentTransportCancellation", "rptStudentTransportCancellation.rpt");
    }
   
    
    public void Clear()
    {
        ddlsem.SelectedValue = "0";
        ddlyear.SelectedValue = "0";
        ddlbranch.SelectedValue = "0";
        ddldegree.SelectedValue = "0";
        rdotrasnsporttyepe.SelectedValue = "C";
     
        Session["Degree"] =0;
        Session["Sem"]    =0;
        Session["Year"]   =0;
        Session["Branch"] = 0;
        FillDropDwon();
    }

    private void ShowTransportReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue)+ ",@P_BRANCHNO=" + Session["Branch"].ToString() + ",@P_SEMESTERNO=" + Session["Sem"].ToString() + ",@P_YEAR=" + Session["Year"].ToString() + ",@P_CANCELLED_STATUS=" + rdotrasnsporttyepe.SelectedValue ;

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
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lvstudent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
protected void btnEdit_Click(object sender, ImageClickEventArgs e)
{
    ImageButton imgBtn = sender as ImageButton;
    int no = int.Parse(imgBtn.CommandArgument);
    ViewState["SRNO"] = int.Parse(imgBtn.CommandArgument);
    ShowDetails(ViewState["SRNO"].ToString());
    
}
private void ShowDetails(string no)
{
    DataSet ds = objCommon.FillDropDown("VEHICLE_STUDENT_TRANSPORT_CANCELLATION", "*", "", "SRNO=" + no, "SRNO");
    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    {
        FillDropDwon();
        ddlyear.SelectedValue = ds.Tables[0].Rows[0]["S_YEAR"].ToString();
        ddlsem.SelectedValue = ds.Tables[0].Rows[0]["S_SEMESTER"].ToString();
        ddlbranch.SelectedValue = ds.Tables[0].Rows[0]["S_BRANCH"].ToString();
        ddldegree.SelectedValue = ds.Tables[0].Rows[0]["S_DEGREENO"].ToString();
      //  ddlstudent.SelectedValue = ds.Tables[0].Rows[0]["S_IDNO"].ToString();
    }
}
protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
{
    Session["Branch"] = 0;
    Session["Branch"] = ddlbranch.SelectedValue;
  //  BindFillDreopDwon();
}
protected void btncancel_Click(object sender, EventArgs e)
{
    Clear();
}
protected void btnHostelerReport_Click(object sender, EventArgs e)
{
    try
    {
        ShowReport("Hosteler_Regular_Type", "rptHostelerRegularVehicleReport.rpt");
    }
    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "Academic_RoomAllotmentStatus.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
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
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["username"].ToString() + ",@P_HOSTREG_TYPE=0" ;

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
            objUCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server Unavailable.");
    }
}
}