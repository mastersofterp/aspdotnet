//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 10-FEB-2021
// DESCRIPTION   : USE TO MAINTAIN TOUR/ EVENT DETAILS OF VEHICLES 
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
using System.Collections.Generic;

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleTourEventDetails : System.Web.UI.Page
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
                    FillDropDown();
                    BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleTourEventDetails.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "VIDNO");
        objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "", "DNAME");
    }

    private void FillDropDownHire()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0 AND ACTIVE_STATUS=1", "VEHICLE_ID");
    }

    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
        {
            FillDropDown();
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            clear();
            ViewState["action"] = "add";
        }
        else
        {
            FillDropDownHire();
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            clear();
            ViewState["action"] = "add";
        }
    }

    private void BindList(int vehicle_cat)
    {
        try
        {
            DataSet ds = objVMC.GetTourEventAll(vehicle_cat);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTourEvent.DataSource = ds;
                lvTourEvent.DataBind();
            }
            else
            {
                lvTourEvent.DataSource = null;
                lvTourEvent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleTourEventDetails.BindList -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           
            objVM.TOUREVENTDATE = Convert.ToDateTime(txtTourEventDate.Text);
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                objVM.VEHICLECAT = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLECAT = Convert.ToString('H');
            }
            objVM.VIDNO = Convert.ToInt32(ddlVehicle.SelectedValue);
            objVM.OUTTIME = Convert.ToDateTime(Convert.ToDateTime(txtOUTTime.Text).ToString("HH:mm tt"));
            objVM.OUTKM = txtOUTkm.Text == string.Empty ? 0 : Convert.ToDouble(txtOUTkm.Text);
            objVM.PURPOSE = txtPurpose.Text == string.Empty ? string.Empty : txtPurpose.Text.Trim();
            objVM.LDNO = Convert.ToInt32(ddlDriver.SelectedValue);
            objVM.INTIME = Convert.ToDateTime(Convert.ToDateTime(txtINTime.Text).ToString("HH:mm tt"));
            objVM.INKM = txtINkm.Text == string.Empty ? 0 : Convert.ToDouble(txtINkm.Text);
            objVM.MALE_COUNT = txtMale.Text == string.Empty ? 0 : Convert.ToInt32(txtMale.Text.Trim());
            objVM.FEMALE_COUNT = txtFemale.Text == string.Empty ? 0 : Convert.ToInt32(txtFemale.Text.Trim());
            objVM.CHILDREN_COUNT = txtChild.Text == string.Empty ? 0 : Convert.ToInt32(txtChild.Text.Trim());
            objVM.INFANT_COUNT = txtINCount.Text == string.Empty ? 0 : Convert.ToInt32(txtINCount.Text.Trim());
            objVM.TOTAL_NO_PATIENT = (Convert.ToInt32(txtMale.Text) + Convert.ToInt32(txtFemale.Text) + Convert.ToInt32(txtChild.Text) + Convert.ToInt32(txtINCount.Text)); //txtTotalPatient.Text == string.Empty ? 0 : Convert.ToInt32(txtTotalPatient.Text.Trim());
            objVM.TOTAL_KM = (Convert.ToDouble(txtINkm.Text) - Convert.ToDouble(txtOUTkm.Text));  // txtTotKm.Text == string.Empty ? 0 : Convert.ToDouble(txtTotKm.Text.Trim());
            objVM.PLACE = txtPlace.Text == string.Empty ? string.Empty : txtPlace.Text.Trim();
            objVM.USERNO = Convert.ToInt32(Session["userno"]);


          

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objVM.TEID = 0;
                    //--======start===Shaikh Juned 5-09-2022
                    String toureventdate = Convert.ToDateTime(txtTourEventDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    DataSet ds = objCommon.FillDropDown("VEHICLE_TOUR_EVENT_DETAILS", "TOUREVENTDATE", "VIDNO", "TOUREVENTDATE='" + toureventdate + "'AND VIDNO ='" + Convert.ToInt32(ddlVehicle.SelectedValue) + "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string tour_event_detail = Convert.ToDateTime(dr["TOUREVENTDATE"]).ToString("dd/MM/yyyy");
                            string vehicle_name = dr["VIDNO"].ToString();
                            if (tour_event_detail == txtTourEventDate.Text & vehicle_name == ddlVehicle.SelectedValue)
                            {
                                objCommon.DisplayMessage(this.Page, "Vehicle Tour/Event Detail Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }
                    //---========end=====
                    CustomStatus cs = (CustomStatus)objVMC.TourEventInsertUpdate(objVM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        clear();
                        BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));                      
                        Showmessage("Record Save Successfully.");
                    }
                }
                else
                {
                    if (ViewState["TEID"] != null)
                    {
                        objVM.TEID = Convert.ToInt32(ViewState["TEID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.TourEventInsertUpdate(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            Showmessage("Record Updated Successfully.");
                            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleTourEventDetails.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
//rdblistVehicleType.SelectedValue = "1";
        ddlVehicle.SelectedValue = "0";
        txtOUTTime.Text = string.Empty;
        txtOUTkm.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        ddlDriver.SelectedValue = "0";
        txtINTime.Text = string.Empty;
        txtINkm.Text = string.Empty;
        txtMale.Text = string.Empty;
        txtFemale.Text = string.Empty;
        txtChild.Text = string.Empty;
        txtINCount.Text = string.Empty;
        txtTotalPatient.Text = string.Empty;
        txtTotKm.Text = string.Empty;
        ViewState["TEID"] = null;
        ViewState["action"] = "add";
        txtTourEventDate.Text = string.Empty;
        txtPlace.Text = string.Empty;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int teid = int.Parse(imgBtn.CommandArgument);
        ViewState["TEID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(teid);
    }
    private void ShowDetails(int teid)
    {
        try
        {
            DataSet ds = objVMC.GetTourEventByTEId(teid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlVehicle.SelectedValue = dr["VIDNO"].ToString();
                    if (dr["VEHICLE_CATEGORY"].ToString() == "C")
                    {
                        rdblistVehicleType.SelectedValue = "1";
                    }
                    else
                    {
                        rdblistVehicleType.SelectedValue = "2";
                    }
                                      
                    txtTourEventDate.Text = Convert.ToDateTime(dr["TOUREVENTDATE"]).ToString("dd/MM/yyyy");
                    txtOUTTime.Text = Convert.ToDateTime(dr["OUTTIME"]).ToString("hh:mm tt");
                    txtOUTkm.Text = Convert.ToDouble(dr["OUT_KM"]).ToString("0.00");
                    ddlDriver.SelectedValue = dr["DNO"].ToString();
                    txtINTime.Text = Convert.ToDateTime(dr["INTIME"]).ToString("hh:mm tt"); 
                    txtINkm.Text = Convert.ToDouble(dr["IN_KM"]).ToString("0.00");
                    txtMale.Text = dr["NO_OF_MALES"].ToString();
                    txtFemale.Text = dr["NO_OF_FEMALES"].ToString();
                    txtChild.Text = dr["NO_OF_CHILDREN"].ToString();
                    txtINCount.Text = dr["NO_OF_INFANT"].ToString();
                    txtTotalPatient.Text = dr["TOTAL_PATIENTS"].ToString();
                    txtTotKm.Text = Convert.ToDouble(dr["TOTAL_KM"]).ToString("0.00");
                    txtPlace.Text = dr["PLACE"].ToString();
                    txtPurpose.Text = dr["PURPOSE"].ToString();                    
                } 
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleTourEventDetails.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}