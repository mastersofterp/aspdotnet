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


public partial class VEHICLE_MAINTENANCE_Transaction_logbook : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM objVM = new VM();
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
                    FillDropDown();
                    BindList();
                    txtTotalKM.Attributes.Add("readonly", "readonly");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Logbook.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindList();
    }



    private void BindList()
    {
        try
        {
            DataSet ds = objVMC.GetLogBookAll();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFitness.DataSource = ds;
                lvFitness.DataBind();
            }
            else
            {
                lvFitness.DataSource = null;
                lvFitness.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Logbook.BindList -> " + ex.Message + " " + ex.StackTrace);
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

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "VIDNO");
        objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "", "DNAME");
        objCommon.FillDropDownList(ddlTripTypr, "VEHICLE_TRIPTYPE", "TTID", "TRIPTYPENAME", "ACTIVE='TRUE'", "TRIPTYPENAME");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int no = int.Parse(btnDelete.CommandArgument);
        objVM.LLOGBOOKID = Convert.ToInt32(no);
        //CustomStatus CS = (CustomStatus)objVMC.DeleteFitnessByFID(objVM);
        CustomStatus CS = (CustomStatus)objVMC.DeleteLogBookByFID(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
            BindList();
        }
    }
    private void clear()
    {
        ddlVehicle.SelectedValue = "0";
        txtTourDate.Text = string.Empty;
        txtDepTime.Text = string.Empty;
        txtArrTime.Text = string.Empty;
        txtFromLoc.Text = string.Empty;
        txtToLoc.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlTripTypr.SelectedValue = "0";
        ddlDriver.SelectedValue = "0";
        lblDriverTA.Text = "";
        lblHireRate.Text = "";
        lblTotalAmount.Text = "";
        lblWaitingCharge.Text = "";
        txtSMeterReading.Text = "";
        txtEmeterReading.Text = "";
        txtTotalKM.Text = "";
        txtTourDetails.Text = string.Empty;
        txtPassengerName.Text = string.Empty;

        ViewState["LOGBOOKID"] = null;
        ViewState["action"] = "add";
        
    }

    protected void btnEdit_Click(object sender, EventArgs e)
        {
        ImageButton imgBtn = sender as ImageButton;
        int no = int.Parse(imgBtn.CommandArgument);
        ViewState["LOGBOOKID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(no);
    }
    private void ShowDetails(int NO)
    {
        try
        {
            objVM.FID = Convert.ToInt32(NO);
            DataSet ds = objVMC.GetLogBookByLogBookId(NO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    ddlVehicle.SelectedValue = dr["VIDNO"].ToString();
                    ddlTripTypr.SelectedValue = dr["TTID"].ToString();
                    txtTourDate.Text = Convert.ToDateTime(dr["TOURDATE"]).ToString("dd/MM/yyyy");
                    txtDepTime.Text = Convert.ToDateTime(dr["DTIME"]).ToString("hh:mm tt");
                    txtArrTime.Text = Convert.ToDateTime(dr["ATIME"]).ToString("hh:mm tt");
                    txtFromLoc.Text = dr["FROM_LOCATION"].ToString();
                    txtToLoc.Text = dr["TO_LOCATION"].ToString();
                    txtRemark.Text = dr["REMARK"].ToString();
                    txtSMeterReading.Text =  Convert.ToDouble(dr["SMREADING"]).ToString("0.00");
                    txtEmeterReading.Text = Convert.ToDouble(dr["EMREADING"]).ToString("0.00");
                    txtTotalKM.Text = Convert.ToDouble(dr["TOTALKM"]).ToString("0.00");
                    lblWaitingCharge.Text = Convert.ToDouble(dr["WAITING_CHARGE"]).ToString("0.00");
                    lblHireRate.Text = Convert.ToDouble(dr["HRATE_PER_KM"]).ToString("0.00");
                    lblDriverTA.Text = Convert.ToDouble (dr["DRIVER_TA"]).ToString("0.00");
                    lblTotalAmount.Text = Convert.ToDouble(dr["TOTALAMT"]).ToString("0.00");
                    txtTourDetails.Text = dr["TOUR_DETAILS"].ToString();
                    ddlDriver.SelectedValue = dr["DNO"].ToString();
                    txtPassengerName.Text = dr["PASENGER_NAME"].ToString();
                    // LOGBOOKID
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_LogBook.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public bool Validate()
    {
        bool res = true;

        if (ddlVehicle.SelectedValue == "0")
        {
            objCommon.DisplayMessage("Please Select Vehicle", this.Page);
            res = false;
        }

        if (ddlTripTypr.SelectedValue == "0")
        {
            objCommon.DisplayMessage("Please Select Trip Type", this.Page);
            res = false;
        }


        if (txtEmeterReading.Text.Trim() != "" && txtSMeterReading.Text.Trim() != "")
        {

            if (Convert.ToDouble( txtSMeterReading.Text) > Convert.ToDouble( txtEmeterReading.Text))
            {
                objCommon.DisplayMessage("Start Meter Reading Is Always Less Than End Meter Reading", this.Page);
                res = false;
            }
        }
        return res;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (Validate())
            {
                objVM.LVIDNO = Convert.ToInt32(ddlVehicle.SelectedValue);
                objVM.LTTID = Convert.ToInt32(ddlTripTypr.SelectedValue);
                objVM.LTOURDATE = Convert.ToDateTime(txtTourDate.Text);
                objVM.LDTIMEL = Convert.ToDateTime(Convert.ToDateTime(txtDepTime.Text).ToString("HH:mm tt"));
                objVM.LATIME = Convert.ToDateTime(Convert.ToDateTime(txtArrTime.Text).ToString("HH:mm tt"));
                objVM.LFROMLOCATION = txtFromLoc.Text.Trim();
                objVM.LTOLOCATION = txtToLoc.Text.Trim();
                objVM.LREMARK = txtRemark.Text.Trim();
                objVM.LSTARTMETERREADING = Convert.ToDouble(txtSMeterReading.Text);
                objVM.LENDMETERREADING = Convert.ToDouble(txtEmeterReading.Text);
                objVM.LWAITINGCHARGES = Convert.ToDouble(lblWaitingCharge.Text);
                objVM.LHIRERATEPERKM = Convert.ToDouble(lblHireRate.Text);
                objVM.LDRIVERTA = Convert.ToDouble(lblDriverTA.Text);
                if (lblTotalAmount.Text == string.Empty)
                {
                    objVM.LTOTALAMT = 0.00 ;
                }
                else
                {
                    objVM.LTOTALAMT = Convert.ToDouble(lblTotalAmount.Text);
                }

                objVM.LTOURDETAILS = txtTourDetails.Text.Trim();
                objVM.LDNO = Convert.ToInt32(ddlDriver.SelectedValue);
                objVM.PASSENGERNAME = txtPassengerName.Text.Trim();
                if (txtTotalKM.Text == string.Empty)
                {
                    objVM.LTOTALKM = 0.00;
                }
                else
                {
                    objVM.LTOTALKM = Convert.ToDouble(txtTotalKM.Text);
                }
               // objVM.LTOTALKM = Convert.ToDouble(txtTotalKM.Text.Trim());

     

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //--======start===Shaikh Juned 30-08-2022

                        //  DateTime date=Convert.ToDateTime(txtTourDate.Text);
                        //string date = txtTourDate.Text.ToString("MM-dd-yyyy");
                        string date = Convert.ToDateTime(txtTourDate.Text).ToString("yyyy-MM-dd");
                        DataSet ds = objCommon.FillDropDown("vehicle_logbook", "VIDNO", "TOURDATE", "VIDNO='" + Convert.ToInt32(ddlVehicle.SelectedValue) + "'And TOURDATE='" + date + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                int vehicleid = Convert.ToInt32(dr["VIDNO"]);
                                //string toredate = dr["TOURDATE"].ToString();
                                string toredate = Convert.ToDateTime(dr["TOURDATE"]).ToString("yyyy-MM-dd");
                                if (vehicleid == Convert.ToInt32(ddlVehicle.SelectedValue) && toredate == date)
                                {
                                    objCommon.DisplayMessage(this.Page, "This Trip Is Already Exist.", this.Page);
                                    return;
                                }

                            }
                        }
                        //---========end=====
                        objVM.LLOGBOOKID = 0;
                        CustomStatus cs = (CustomStatus)objVMC.LogBookInsertUpdate(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {                           
                            clear();                           
                            BindList();
                           // objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                            objCommon.DisplayUserMessage(updActivity, "Record Save Successfully.", this.Page);
                        }
                    }
                    else
                    {
                        if (ViewState["LOGBOOKID"] != null)
                        {
                            //--======start===Shaikh Juned 30-08-2022

                            //  DateTime date=Convert.ToDateTime(txtTourDate.Text);
                            //string date = txtTourDate.Text.ToString("MM-dd-yyyy");
                            string date = Convert.ToDateTime(txtTourDate.Text).ToString("yyyy-MM-dd");
                            DataSet ds = objCommon.FillDropDown("vehicle_logbook", "VIDNO", "TOURDATE", "VIDNO!='" + Convert.ToInt32(ddlVehicle.SelectedValue) + "'And TOURDATE='" + date + "'", "");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    int vehicleid = Convert.ToInt32(dr["VIDNO"]);
                                    //string toredate = dr["TOURDATE"].ToString();
                                    string toredate = Convert.ToDateTime(dr["TOURDATE"]).ToString("yyyy-MM-dd");
                                    if (vehicleid == Convert.ToInt32(ddlVehicle.SelectedValue) && toredate == date)
                                    {
                                        objCommon.DisplayMessage(this.Page, "This Trip Is Already Exist.", this.Page);
                                        return;
                                    }

                                }
                            }
                            //---========end=====
                            objVM.LLOGBOOKID = Convert.ToInt32(ViewState["LOGBOOKID"].ToString());
                            CustomStatus cs = (CustomStatus)objVMC.LogBookInsertUpdate(objVM);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                clear();
                               // objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                                objCommon.DisplayUserMessage(updActivity, "Record Updated Successfully.", this.Page);
                                BindList();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_LogBook.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }
    public void Fill_Hire_Charge()
    {
        bool chargeable = false;
        DataSet dsTripType = objCommon.FillDropDown("VEHICLE_TRIPTYPE", "TTID", "CHARGEABLE", "TTID=" + ddlTripTypr.SelectedValue, "");
        if (dsTripType.Tables[0].Rows.Count > 0)
        {
            chargeable = Convert.ToBoolean(dsTripType.Tables[0].Rows[0]["CHARGEABLE"]);
        }
        DataSet dsHireCharge;
        if (chargeable)
        {
            dsHireCharge = objCommon.FillDropDown("VEHICLE_MASTER","VIDNO", "ISNULL(KM_RATE,0) AS KM_RATE, ISNULL(DRIVE_TA,0) DRIVE_TA,ISNULL(WAITCHARGE,0) WAITCHARGE",  "VIDNO=" + ddlVehicle.SelectedValue,"");
            if (dsHireCharge.Tables[0].Rows.Count > 0)
            {
               if(dsHireCharge.Tables[0].Rows[0]["DRIVE_TA"].ToString().Trim().Equals(""))
               {
                   lblDriverTA.Text = "0.00";
               }
               else
               {
                   lblDriverTA.Text = dsHireCharge.Tables[0].Rows[0]["DRIVE_TA"].ToString();
               }

               if (dsHireCharge.Tables[0].Rows[0]["KM_RATE"].ToString().Trim().Equals(string.Empty))
               {
                   lblHireRate.Text = "0.00";
               }
               else
               {
                   lblHireRate.Text = dsHireCharge.Tables[0].Rows[0]["KM_RATE"].ToString();
               }

               if (dsHireCharge.Tables[0].Rows[0]["WAITCHARGE"].ToString().Trim().Equals(string.Empty))
               {
                   lblWaitingCharge.Text = "0.00";
               }
               else
               {
                   lblWaitingCharge.Text = dsHireCharge.Tables[0].Rows[0]["WAITCHARGE"].ToString();
               }               
            }
        }
        else
        {
            lblDriverTA.Text = "0.00";
            lblHireRate.Text = "0.00";
            lblWaitingCharge.Text = "0.00";
        }

    }




    protected void ddlTripTypr_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Hire_Charge();
    }
    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Hire_Charge();
    }

    protected void txtSMeterReading_TextChanged(object sender, EventArgs e)
    {
        //if (txtEmeterReading.Text.Trim() != string.Empty && txtSMeterReading.Text.Trim() != string.Empty)
        //{
        //    double ERead = Convert.ToDouble(txtEmeterReading.Text);
        //    double SRead = Convert.ToDouble(txtSMeterReading.Text);
        //    txtTotalKM.Text = Convert.ToDouble(ERead - SRead).ToString("0.00");
        //    //if (lblHireRate.Text == string.Empty)
        //    //{
        //    //    lblHireRate.Text = "1";
        //    //}
        //    //if (lblDriverTA.Text == string.Empty)
        //    //{
        //    //    lblDriverTA.Text = "0";
        //    //}
        //    //if (lblWaitingCharge.Text == string.Empty)
        //    //{
        //    //    lblWaitingCharge.Text = "0";
        //    //}
        //    //double totalAmount = (Convert.ToDouble(lblHireRate.Text) * Convert.ToDouble(txtTotalKM.Text)) + Convert.ToDouble(lblDriverTA.Text) + Convert.ToDouble(lblWaitingCharge.Text);
        //    //lblTotalAmount.Text = totalAmount.ToString("0.00");
            

        //    txtEmeterReading.Focus();
        //}

    }
    protected void txtEmeterReading_TextChanged(object sender, EventArgs e)
    {
        //if (txtEmeterReading.Text.Trim() != string.Empty && txtSMeterReading.Text.Trim() != string.Empty)
        //{
            
        //    double ERead = Convert.ToDouble(txtEmeterReading.Text);
        //    double SRead = Convert.ToDouble(txtSMeterReading.Text);
        //    if (SRead > ERead)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Start Meter Reading Can Not Be Greater Than to End Meter Reading...!!');", true);
        //        txtSMeterReading.Text = string.Empty;
        //        txtEmeterReading.Text = string.Empty;
        //        txtTotalKM.Text = string.Empty;
        //        txtEmeterReading.Focus();

        //    }
        //    else
        //    {
        //        txtTotalKM.Text = Convert.ToDouble(ERead - SRead).ToString("0.00");
        //        //double totalAmount = (Convert.ToDouble(lblHireRate.Text) * Convert.ToDouble(txtTotalKM.Text)) + Convert.ToDouble(lblDriverTA.Text) + Convert.ToDouble(lblWaitingCharge.Text);
        //        //lblTotalAmount.Text = totalAmount.ToString("0.00");
        //    }
        //}
    }
}
