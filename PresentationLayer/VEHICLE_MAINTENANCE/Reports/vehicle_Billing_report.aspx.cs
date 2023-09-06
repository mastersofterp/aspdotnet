//=============================================
//MODIFIED BY    : MRUNAL SINGH
//MODIFIED DATE  : 14-01-2015
//DESCRIPTION    : NOT ALLOW TO TAKE EPORT  IN CRYATAL VIEWER. 
//                 SOLVE THE DESIGN ISSUE.    
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

public partial class VEHICLE_MAINTENANCE_Reports_vehicle_Billing_report : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM  objVM = new VM();
    VMController objVMC = new VMController();  


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
                    FillDriverDropDown();
                    //BindlistView();
                    Driver.Visible = false;
                    Supplier.Visible = false;
                }

            }
            divMsg.InnerHtml = string.Empty;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTripType.SelectedValue == "5")
            {
                if (rdblistVehicleType.SelectedValue =="1")
                {
                    ShowReportVehDetail(rdoReportType.SelectedValue, "rptVehicleDetails.rpt");
                }
                else
                {
                    ShowReportVehDetail(rdoReportType.SelectedValue, "rptHireVehicleDetails.rpt");
                }
              
            }
            else
            {
                if (!txtFDate.Text.Equals(string.Empty))
                {
                    if (DateTime.Compare(Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text)) == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);


                        txtFDate.Focus();
                        txtFDate.Text = string.Empty;
                        txtTDate.Text = string.Empty;
                        return;
                    }
                }
                if (ddlTripType.SelectedValue == "3" || ddlTripType.SelectedValue == "4")
                {
                    ShowContractHODCarsReport(rdoReportType.SelectedValue, "rptContractHODCarsReport.rpt");
                }  
              

                if (ddlTripType.SelectedValue == "2")
                {

                    ShowVehicleBillCollegeTourReport(rdoReportType.SelectedValue, "Vehicle_College_Tour_Bill_Rpeort.rpt");

                }
                if (ddlTripType.SelectedValue == "6")
                {
                    ShowVehicleDailyAttendanceReport(rdoReportType.SelectedValue, "vehicle_Attendance_Report.rpt");

                }
                if (ddlTripType.SelectedValue == "7")
                {
                    //ShowTADAReport(rdoReportType.SelectedValue, "rptDriverTADAReport.rpt");
                    ShowTADAReport(rdoReportType.SelectedValue, "rptDriverTADANAMEReport.rpt");

                }
               
                else
                {
                    ShowLogBookReport(rdoReportType.SelectedValue, "Vehicle_College_Bus_Bill_Rpeort.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
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
            int TTID = int.Parse(btnEdit.CommandArgument);
            ViewState["TTID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            //ShowDetails(TTID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        if (panSupplierDetails.Visible == true)
        {
            ddlSupplier.SelectedValue = "0";
            ddlTripType.SelectedValue = "0";
            txtFDate.Text = string.Empty;
            txtTDate.Text = string.Empty;
            Supplier.Visible = true;
            panSupplierDetails.Visible = true;
            panVehicleDetails.Visible = false;
            ddlDriverName.SelectedValue = "0";
            ddlVehicle.SelectedValue = "0";
        }
        else
        {
            ddlVehicle.SelectedValue = "0";
            rdblistVehicleType.SelectedIndex = 0;
            panSupplierDetails.Visible = false;
            panVehicleDetails.Visible = true;
            Fill_DropDown();
        }

    }

    public void Fill_DropDown()
    {
        objCommon.FillDropDownList(ddlSupplier, "VEHICLE_SUPPILER_MASTER", "SUPPILER_ID", "SUPPILER_NAME", "", "SUPPILER_NAME");
        
    }
    public void FillDriverDropDown()
    {
        objCommon.FillDropDownList(ddlDriverName, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "DCATEGORY=1", "DNAME");
  
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    private void ShowReportVehDetail(string reportTitle, string rptFileName)
    {
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));           
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=VehicalDetails.'" +reportTitle+ "'";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_VIDNO=" + ddlVehicle.SelectedValue;
           
            if (rdblistVehicleType.SelectedValue == "1")
            {

                url += ",@P_VEHICLE_TYPE=COLLEGE";
            }
            else
            {
                url += ",@P_VEHICLE_TYPE=HIRE";
            }

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void  ShowTADAReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Driver TA Report."+exporttype;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_DRIVER_ID=" + ddlDriverName.SelectedValue;

            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                //url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
                url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
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
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowLogBookReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlTripType.SelectedItem.Text+"."+ exporttype; // ddlSupplier.SelectedItem.Text ;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_SUPPILER_ID=" + ddlSupplier.SelectedValue;


         

            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                //url += ",@P_FDATE=" + (txtFDate.Text).ToString("MM/dd/yyyy") + ",@P_TDATE=" + txtTDate.Text;
                url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
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
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowVehicleBillCollegeTourReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlSupplier.SelectedItem.Text +"."+exporttype;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_SUPPILER_ID=" + ddlSupplier.SelectedValue;


            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                //url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
                url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
                
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
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
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowVehicleDailyAttendanceReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Daily Attendance Report."+exporttype;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_SUPPILER_ID=" + ddlSupplier.SelectedValue;


            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                //url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
                url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
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
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void rdoReportType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTripType_SelectedIndexChanged(object sender, EventArgs e)
  {
        if (Convert.ToInt32(ddlTripType.SelectedItem.Value) == 1)  // college Bus Bill
        {
            panSupplierDetails.Visible = true;
            Driver.Visible = false;
            Supplier.Visible = true;
            panVehicleDetails.Visible = false;

        }
        else if(Convert.ToInt32(ddlTripType.SelectedItem.Value) == 2) // college Tour Bill
        {
            panSupplierDetails.Visible = true;
            Supplier.Visible = true;
            Driver.Visible = false;
            panVehicleDetails.Visible = false;
        }
        else if (Convert.ToInt32(ddlTripType.SelectedItem.Value) == 6) // Vehicle Daily Attendance
        {
            panSupplierDetails.Visible = true;
            Supplier.Visible = true;
            Driver.Visible = false;
            panVehicleDetails.Visible = false;
        }
        else if (Convert.ToInt32(ddlTripType.SelectedItem.Value) == 7) // Driver TA 
        {
            Supplier.Visible = false;
            Driver.Visible = true;
            panSupplierDetails.Visible = true;
            ddlDriverName.SelectedValue = "0";
          
            panVehicleDetails.Visible = false;
        }
        else if (Convert.ToInt32(ddlTripType.SelectedItem.Value) == 5) // Vehicle Details
        {
          
            panVehicleDetails.Visible = true;
            panSupplierDetails.Visible = false;
            btnSubmit.Text = "Vehicle Report";
            FillDropDown();

        }
        else if (Convert.ToInt32(ddlTripType.SelectedItem.Value) == 3 || Convert.ToInt32(ddlTripType.SelectedItem.Value) == 4)  // college Bus Bill
        {
            panSupplierDetails.Visible = true;
            Driver.Visible = false;
            Supplier.Visible = true;
            panVehicleDetails.Visible = false;

        }
        else
        {
            panVehicleDetails.Visible = false;
            panSupplierDetails.Visible = true;
            btnSubmit.Text = "Report";
      
        }


    }
    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
        {
            FillDropDown();
            panVehicleDetails.Visible = true;

        }
        else
        {
            FillDropDownHire();
            panVehicleDetails.Visible = true;
        }
    }
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0", "VIDNO");
    }
    private void FillDropDownHire()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0", "VEHICLE_ID");
    }
    protected void btnVehicle_Click(object sender, EventArgs e)
    {

    }


    private void ShowContractHODCarsReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlTripType.SelectedItem.Text +"."+exporttype;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_SUPPILER_ID=" + ddlSupplier.SelectedValue + ",@P_TRIP_TYPE=" + ddlTripType.SelectedValue;


            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {               
                url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
            }

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}
