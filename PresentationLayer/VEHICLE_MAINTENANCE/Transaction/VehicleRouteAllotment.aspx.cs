//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 17-JUN-2019
// DESCRIPTION   : USE TO ALLOT USER WITH THE ROUTES.
//=========================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  
using System.Net.NetworkInformation;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment : System.Web.UI.Page
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
                    ViewState["action"] = "add";                   
                    BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                    FillDropDown();
                    objVM.IPADDRESS = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (objVM.IPADDRESS == null || objVM.IPADDRESS == "")
                        objVM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    objVM.MACADDRESS = nics[0].GetPhysicalAddress().ToString();

                    objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME+' :-> '+ROUTEPATH AS ROUTENAME", "", "ROUTEID");
                    objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "DRIVER_CON_TYPE=1", "DNO");
                    Session["RecTbl"] = null;
                  //  lvDeparture.Visible = false;

                    //trRoute.Visible = false;
                    //trRouteDrop.Visible = true;
                    //trDeparture.Visible = true;
                    btnAdd.Visible = false;                   
                   
                    //MEVDate.Visible = true;
                    //MaskedEditValidator2.Visible = true;
                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to check the page authority.
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
    // This method is used to fill Vehicle of College.
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "");
    }
    // This method is used to fill Vehicles which are hired.
    private void FillDropDownHire()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0 AND ACTIVE_STATUS=1", "");
    }
    // This method is used to bind the list of vahicle allotment.
    private void BindlistView(int VEH_CAT)
    {
        try
        {
            DataSet ds = null;
            if (VEH_CAT == 1)
            {
                ds = objCommon.FillDropDown("VEHICLE_ROUTEALLOTMENT RA INNER JOIN VEHICLE_MASTER VM ON ( RA.VIDNO = VM.VIDNO) LEFT OUTER JOIN VEHICLE_ROUTEMASTER RM ON (RA.ROUTEID = RM.ROUTEID) LEFT JOIN VEHICLE_DRIVERMASTER D ON (RA.DRIVER_ID = D.DNO)", "RA.RAID, RA.FDATE,RA.TDAT,(CASE WHEN RA.ROUTEID=0 THEN NULL ELSE DTIME END) AS DTIME, D.DNAME", " VM.REGNO +':'+VM.NAME AS VEHNAME, (CASE WHEN RA.ROUTEID=0 THEN RA.ROUTENAME ELSE RM.ROUTENAME+': '+RM.ROUTEPATH END) AS ROUTENAME", "RA.STATUS=0 AND VEHICLE_CATEGORY='C'", "RA.RAID");
            }
            else
            {
                ds = objCommon.FillDropDown("VEHICLE_ROUTEALLOTMENT RA INNER JOIN VEHICLE_HIRE_MASTER VHM ON (RA.VIDNO = VHM.VEHICLE_ID) LEFT OUTER JOIN VEHICLE_ROUTEMASTER RM ON (RA.ROUTEID = RM.ROUTEID) LEFT JOIN VEHICLE_DRIVERMASTER D ON (RA.DRIVER_ID = D.DNO)", "RA.RAID, RA.FDATE,RA.TDAT,(CASE WHEN RA.ROUTEID=0 THEN NULL ELSE DTIME END) AS DTIME, D.DNAME", " VHM.VEHICLE_NAME AS VEHNAME,(CASE WHEN RA.ROUTEID=0 THEN RA.ROUTENAME ELSE RM.ROUTENAME+': '+RM.ROUTEPATH END) AS ROUTENAME", "RA.STATUS=0 AND VEHICLE_CATEGORY='H'", "RA.RAID");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvVRAllotment.Visible = true;
                lvVRAllotment.DataSource = ds;
                lvVRAllotment.DataBind();
            }
            else
            {
                lvVRAllotment.DataSource = null;
                lvVRAllotment.DataBind();
                lvVRAllotment.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This action button save the vehicle-driver-route allotment.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (!txtFrmDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                    txtFrmDt.Focus();
                    return;
                }
            }
            string DepartureTime = string.Empty;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = (DataTable)Session["RecTbl"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (DepartureTime.Trim().Equals(string.Empty))
                    {
                        DepartureTime = dr["DEPARTURE_TIME"].ToString();
                    }
                    else
                        DepartureTime += " - " + dr["DEPARTURE_TIME"].ToString();
                }
            }
            else
            {
                if (txtDepTime.Text == "")
                {
                    DepartureTime = "12:00 AM";
                }
                else
                {
                    DepartureTime = Convert.ToDateTime(txtDepTime.Text).ToString("HH:mm tt");
                }
            }
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                objVM.VEHICLECAT = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLECAT = Convert.ToString('H');
            }
            objVM.VIDNO = Convert.ToInt32(ddlVehicle.SelectedValue);
            objVM.ROUTENO = Convert.ToInt32(ddlRoute.SelectedValue);
            if (txtFrmDt.Text == "")
            {
                objVM.FDATE = DateTime.MinValue;
            }
            else
            {
                objVM.FDATE = Convert.ToDateTime(txtFrmDt.Text);
            }

            if (txtFrmDt.Text == "")
            {
                objVM.TDATE = DateTime.MinValue;
            }
            else
            {
                objVM.TDATE = Convert.ToDateTime(txtToDt.Text);
            }
            
            // objVM.LDTIME = Convert.ToDateTime(Convert.ToDateTime(txtDepTime.Text).ToString("HH:mm tt"));
            objVM.LDTIME = DepartureTime;
            objVM.DNO = Convert.ToInt32(ddlDriver.SelectedValue);
            objVM.ROUTENAME = ""; // txtRoute.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRoute.Text.Trim());

           

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //--======start===Shaikh Juned 5-09-2022
                    DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEALLOTMENT", "VIDNO", "ROUTEID,DRIVER_ID", "VIDNO='" + ddlVehicle.SelectedValue + "'AND ROUTEID ='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' AND DRIVER_ID ='" + Convert.ToInt32(ddlDriver.SelectedValue) + "' ", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string vehicle_name = dr["VIDNO"].ToString();
                            string Rout_name = dr["ROUTEID"].ToString();
                            string driver_name = dr["DRIVER_ID"].ToString();
                            if (vehicle_name == ddlVehicle.SelectedValue & Rout_name == ddlRoute.SelectedValue & driver_name == ddlDriver.SelectedValue)
                            {
                                objCommon.DisplayMessage(this.Page, "Vehicle Route Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }
                    //---========end=====
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateVehicleRouteAllotment(objVM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                    }
                    //if (cs.Equals(CustomStatus.RecordExist))
                    //{
                    //    Clear();
                    //    objCommon.DisplayMessage("Record Already Exist.", this.Page);
                    //    return;
                    //}

                   
                }
                else
                {
                    if (ViewState["RAID"] != null)
                    {
                        objVM.RANO = Convert.ToInt32(ViewState["RAID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateVehicleRouteAllotment(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                            ViewState["action"] = "add";                         
                            objCommon.DisplayMessage("Record Update Successfully.", this.Page);                        
                            Clear();
                        }
                        //if (cs.Equals(CustomStatus.RecordExist))
                        //{
                        //    Clear();
                        //    objCommon.DisplayMessage("Record Already Exist.", this.Page);
                        //    return;
                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This action button is use to clear the last selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //This action button is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int RANO = int.Parse(btnEdit.CommandArgument);
            ViewState["RAID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(RANO);
            btnAdd.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This action button is used to delete the unwanted record.
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int RANO = int.Parse(btnDelete.CommandArgument);
            ViewState["RAID"] = int.Parse(btnDelete.CommandArgument);

            objVM.RANO = int.Parse(btnDelete.CommandArgument);
            CustomStatus CS = (CustomStatus)objVMC.DeleteVehicleRouteAllotment(objVM);
            if (CS.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record Deleted Successfully.", this.Page);
                BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            }
        }    
       catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This method is used to show the details of the selected record.
    private void ShowDetails(int RANO)
    {
        try
        {
         //   lvDeparture.Visible = false;
            DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEALLOTMENT", "FDATE,TDAT,DRIVER_ID", "VIDNO,ROUTEID ,DTIME, ROUTENAME", "RAID=" + RANO, "RAID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string RouteNo = ds.Tables[0].Rows[0]["ROUTEID"].ToString();
                if (RouteNo == "0")
                {
                    //trRoute.Visible = true;
                    //trRouteDrop.Visible = false;
                   // trDeparture.Visible = false;
                    btnAdd.Visible = false; 
                    //MEVDate.Visible = false;
                    //MaskedEditValidator2.Visible = false;
                    ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                    //txtRoute.Text =  ds.Tables[0].Rows[0]["ROUTENAME"].ToString();
                    ddlDriver.SelectedValue = ds.Tables[0].Rows[0]["DRIVER_ID"].ToString();
                    txtFrmDt.Text = ds.Tables[0].Rows[0]["FDATE"].ToString();
                    txtToDt.Text = ds.Tables[0].Rows[0]["TDAT"].ToString();
                }
                else
                {
                    //trRoute.Visible = false;
                    //trRouteDrop.Visible = true;
                  //  trDeparture.Visible = true;
                    btnAdd.Visible = false;
                  
                    //MEVDate.Visible = true;
                    //MaskedEditValidator2.Visible = true;


                    ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                    ddlRoute.SelectedValue = ds.Tables[0].Rows[0]["ROUTEID"].ToString();

                    txtFrmDt.Text = ds.Tables[0].Rows[0]["FDATE"].ToString();
                    txtToDt.Text = ds.Tables[0].Rows[0]["TDAT"].ToString();
                    txtDepTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DTIME"]).ToString("hh:mm tt");
                    ddlDriver.SelectedValue = ds.Tables[0].Rows[0]["DRIVER_ID"].ToString();

                    
                }
               
            }
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear all the controls.
    private void Clear()
    {
        ddlVehicle.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;
        txtFrmDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        txtDepTime.Text = string.Empty;
        ViewState["RANO"] = null;
        ViewState["action"] = "add";
        //lvDeparture.DataSource = null;
        //lvDeparture.DataBind();
        //lvDeparture.Visible = false;
        Session["RecTbl"] = null;
        ddlDriver.SelectedIndex = 0;
        //----------------
        //trRoute.Visible = false;
        //trRouteDrop.Visible = true;
       // trDeparture.Visible = true;
        btnAdd.Visible = false;      
       
        //txtRoute.Text = string.Empty;
        //MEVDate.Visible = true;
        //MaskedEditValidator2.Visible = true;
        lblVType.Text = "";
        lvPanel.Visible = false;
        ViewState["RAID"] = null;
       
    }

    // This method is use to generate the list of Vehicle-Driver-Route allotment.
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=VehicleRouteAllotmentReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ROUTEID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    } 
  
  // This action button is used to generate the allotment report.
    protected void btnReport_Click(object sender, EventArgs e)
    { 
       try
       {
          // lvDeparture.Visible = false;
           ShowReport("Vehicle Route Allotment", "rptVehicleRouteAllotment.rpt");
       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
           else
               objCommon.ShowError(Page, "Server UnAvailable");
       }
    }

    // This action buttons give the specific type of vehicles.
    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
        {           
            FillDropDown();           
            Clear();
            ViewState["action"] = "add";
            BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
        }
        else
        {         
            FillDropDownHire();          
            Clear();
            ViewState["action"] = "add";
            BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
        }
    }

    // This method is used to create data table.
    private DataTable CreateTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("DEPARTURE_TIME", typeof(string)));      
        return dt;
    }

    // This action button is used to add various departure times.
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            if (!txtFrmDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                    txtFrmDt.Focus();
                   // lvDeparture.Visible = false;
                    return;
                }
            }
           // lvDeparture.Visible = false;
            lvPanel.Visible = false;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();
                if (CheckDuplicateStopName(dt, txtDepTime.Text.Trim()))
                {
                    txtDepTime.Text = string.Empty;    
                    objCommon.DisplayMessage(this.updActivity, "This Departure Time Already Exist.", this.Page);
                    return;
                }                
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["DEPARTURE_TIME"] = txtDepTime.Text.Trim() == string.Empty ? string.Empty : txtDepTime.Text.Trim();            
              
                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                //lvDeparture.DataSource = dt;
                //lvDeparture.DataBind();
                //lvDeparture.Visible = true;
                txtDepTime.Text = string.Empty;    
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {                
                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["DEPARTURE_TIME"] = txtDepTime.Text.Trim() == string.Empty ? string.Empty : txtDepTime.Text.Trim();  
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                txtDepTime.Text = string.Empty;    
                Session["RecTbl"] = dt;
                //lvDeparture.DataSource = dt;
                //lvDeparture.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnDeleteRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];

                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
               
                Session["RecTbl"] = dt;
                //lvDeparture.DataSource = dt;
                //lvDeparture.DataBind();
                //lvDeparture.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnDeleteRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;                  
                    break;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    // it check the duplicacy of departure time.
    private bool CheckDuplicateStopName(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DEPARTURE_TIME"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.CheckDuplicateStopName() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string RouteNo = string.Empty;
            if (ddlVehicle.SelectedIndex > 0)
            {
                DataSet ds = null;
                if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
                {
                    ds = objCommon.FillDropDown("VEHICLE_MASTER V INNER JOIN VEHICLE_TYPEMASTER VT ON (V.VEHICLE_TYPE = VT.VTID)", "VT.VTNAME", "ROUTE_TYPE_NO", "V.VIDNO=" + ddlVehicle.SelectedValue, "");
                }
                else
                {
                    ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER V INNER JOIN VEHICLE_TYPEMASTER VT ON (V.VEHICLE_TYPE = VT.VTID)", "VT.VTNAME", "ROUTE_TYPE_NO", "V.VEHICLE_ID=" + ddlVehicle.SelectedValue, "");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblVType.Text = ds.Tables[0].Rows[0]["VTNAME"].ToString();
                    RouteNo = ds.Tables[0].Rows[0]["ROUTE_TYPE_NO"].ToString();
                    if (RouteNo == "1")
                    {
                        //trRoute.Visible = true;
                        //trRouteDrop.Visible = false;
                      //  trDeparture.Visible = false;
                        btnAdd.Visible = false;   
                        //MEVDate.Visible = false;
                        //MaskedEditValidator2.Visible = false;
                    }
                    else
                    {
                        //trRoute.Visible = false;
                        //trRouteDrop.Visible = true;
                      //  trDeparture.Visible = true;
                        btnAdd.Visible = false;     
                        //MEVDate.Visible = true;
                        //MaskedEditValidator2.Visible = true;
                        return;
                    }
                }                   
            }
        }
        catch(Exception ex)
        {
             if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.ddlVehicle_SelectedIndexChanged() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "STARTING_TIME", "ROUTEID", "ROUTEID=" + Convert.ToInt32(ddlRoute.SelectedValue), "ROUTEID");
           if (ds.Tables[0].Rows.Count > 0)
           {
               txtDepTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTING_TIME"]).ToString("hh:mm tt");
           }
        }
        catch(Exception ex)
        {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.ddlRoute_SelectedIndexChanged() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindlistView(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
    }
}