
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

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleBillEntry : System.Web.UI.Page
{
    CheckBox chkvehicleIdno = null;
    DataSet dsDATA;
    TextBox Bill_From_Date = null;
    TextBox Bill_To_Date = null;
    TextBox Hire_for = null;
    TextBox Bill_Amount = null;
    TextBox Bill_Extra_Amount = null;
    TextBox Hike_Price = null;
    TextBox Remark = null;

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
                    objCommon.FillDropDownList(ddlSuppiler, "VEHICLE_SUPPILER_MASTER", "SUPPILER_ID", "SUPPILER_NAME", "", "SUPPILER_NAME");
                }
                BindlistView(Convert.ToInt32(rdblisttourtype.SelectedValue.ToString()));
                pnlCommon.Visible = true;
                pnlCollegeBus.Visible = true;
                pnlcollegetour.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            objVM.SUPPILER_ID = Convert.ToInt32(ddlSuppiler.SelectedValue.ToString());
            objVM.VEHICLE_ID = Convert.ToInt32(ddlVehicle.SelectedValue.ToString());
            //objVM.BILL_AMOUNT = Convert.ToDouble(txtBillAmount.Text.Trim());
            objVM.BILL_FROM_DATE = Convert.ToDateTime(txtBillFromDate.Text.Trim());
            objVM.BILL_TO_DATE = Convert.ToDateTime(txtBillToDate.Text.Trim());

            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtBillFromDate.Text)));
            Fdate = Fdate.Substring(0, 10);
            objVM.BILL_FROM_DATE = Convert.ToDateTime(Fdate);

            objVM.HIRE_FOR = txtHireFor.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtHireFor.Text.Trim());
            objVM.HIRED_BY = txtHiredBy.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtHiredBy.Text.Trim());
            objVM.ROUTE_FROM = txtRouteFrom.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRouteFrom.Text.Trim());
            objVM.ROUTE_TO = txtRouteTo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRouteTo.Text.Trim());
            objVM.FROM_TIME = txttravellingfromTime.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txttravellingfromTime.Text.Trim());
            objVM.TO_TIME = txttravellingToTime.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txttravellingToTime.Text.Trim());
            objVM.TOUR_PURPOSE = txtTourPurpose.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtTourPurpose.Text.Trim());
            objVM.VISIT_PLACE = txtVisitPlace.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVisitPlace.Text.Trim());
            if (txtHikeAmount.Text.Trim().Equals(string.Empty))
            {
                objVM.HIKE_PRICE = 0.00;
            }
            else
            {
                objVM.HIKE_PRICE = Convert.ToDouble(txtHikeAmount.Text.Trim());

            }
            if (txtExtraAmount.Text.Trim().Equals(string.Empty))
            {
                objVM.EXTRA_AMOUNT = 0.00;
            }
            else
            {
                objVM.EXTRA_AMOUNT = Convert.ToDouble(txtExtraAmount.Text.Trim());

            }

            //if (rdblisttourtype.SelectedItem.Text.Equals("College Bus"))
            if (rdblisttourtype.SelectedValue.Equals("3"))
            {
                objVM.TRIP_TYPE = Convert.ToInt32(rdblisttourtype.SelectedValue.ToString());
                objVM.BALANCE_AMOUNT = 0.00;
                objVM.PAID_AMOUNT = 0.00;
                objVM.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text.Trim());
                if (txtBillAmount.Text.Trim().Equals(string.Empty))
                {
                    objVM.BILL_AMOUNT = 0.00;
                }
                else
                {
                    objVM.BILL_AMOUNT = Convert.ToDouble(txtBillAmount.Text.Trim());

                }
                objVM.EXTRA_DISTANCE_AMOUNT = 0.00;

                objVM.EXTRA_TIME_AMOUNT = 0.00;
                objVM.TOUR_TOTAL_AMOUNT = 0.00;
                objVM.TOUR_TOTAL_AMOUNT = 0.00;
            }
            else if (rdblisttourtype.SelectedValue.Equals("4"))
            {
                objVM.TRIP_TYPE = Convert.ToInt32(rdblisttourtype.SelectedValue.ToString());
                objVM.BALANCE_AMOUNT = 0.00;
                objVM.PAID_AMOUNT = 0.00;
                objVM.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text.Trim());
                if (txtBillAmount.Text.Trim().Equals(string.Empty))
                {
                    objVM.BILL_AMOUNT = 0.00;
                }
                else
                {
                    objVM.BILL_AMOUNT = Convert.ToDouble(txtBillAmount.Text.Trim());

                }
                objVM.EXTRA_DISTANCE_AMOUNT = 0.00;

                objVM.EXTRA_TIME_AMOUNT = 0.00;
                objVM.TOUR_TOTAL_AMOUNT = 0.00;
                objVM.TOUR_TOTAL_AMOUNT = 0.00;
            }
            else if (rdblisttourtype.SelectedValue.Equals("1"))
            {
                objVM.TRIP_TYPE = Convert.ToInt32(rdblisttourtype.SelectedValue.ToString());
                objVM.BALANCE_AMOUNT = 0.00;
                objVM.PAID_AMOUNT = 0.00;
                objVM.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text.Trim());
                if (txtBillAmount.Text.Trim().Equals(string.Empty))
                {
                    objVM.BILL_AMOUNT = 0.00;
                }
                else
                {
                    objVM.BILL_AMOUNT = Convert.ToDouble(txtBillAmount.Text.Trim());

                }

                objVM.EXTRA_DISTANCE_AMOUNT = 0.00;

                objVM.EXTRA_TIME_AMOUNT = 0.00;
                objVM.TOUR_TOTAL_AMOUNT = 0.00;
                objVM.TOUR_TOTAL_AMOUNT = 0.00;

            }
            else
            {
                if (txtExtraDistanceAmt.Text.Trim().Equals(string.Empty))
                {
                    objVM.EXTRA_DISTANCE_AMOUNT = 0.00;
                }
                else
                {
                    objVM.EXTRA_DISTANCE_AMOUNT = Convert.ToDouble(txtExtraDistanceAmt.Text.Trim());

                }

                if (txtTimeAmt.Text.Trim().Equals(string.Empty))
                {
                    objVM.EXTRA_TIME_AMOUNT = 0.00;
                }
                else
                {
                    objVM.EXTRA_TIME_AMOUNT = Convert.ToDouble(txtTimeAmt.Text.Trim());

                }

                if (txtBillAmt.Text.Trim().Equals(string.Empty))
                {
                    objVM.BILL_AMOUNT = 0.00;
                }
                else
                {
                    objVM.BILL_AMOUNT = Convert.ToDouble(txtBillAmt.Text.Trim());

                }
                if (hdnTotalTourAmount.Value.Trim().Equals(string.Empty))
                {
                    objVM.TOUR_TOTAL_AMOUNT = 0.00;
                }
                else
                {
                    objVM.TOUR_TOTAL_AMOUNT = Convert.ToDouble(hdnTotalTourAmount.Value.Trim());

                }
                objVM.TRIP_TYPE = Convert.ToInt32(rdblisttourtype.SelectedValue.ToString());
                if (txtpaidAmount.Text.Trim().Equals(string.Empty))
                {
                    objVM.PAID_AMOUNT = 0.00;
                }
                else
                {
                    objVM.PAID_AMOUNT = Convert.ToDouble(txtpaidAmount.Text.Trim());

                }
                if (hdnBalanceAmount.Value.Trim().Equals(string.Empty))
                {
                    objVM.BALANCE_AMOUNT = 0.00;
                }
                else
                {
                    objVM.BALANCE_AMOUNT = Convert.ToDouble(hdnBalanceAmount.Value.Trim());

                }
                objVM.REMARK = txtTourRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtTourRemark.Text.Trim());
            }
            objVM.AUDIT_DATE = System.DateTime.Now;

            if (ViewState["BILL_ID"] == null)
            {
                // MODIFIED BY : MRUNAL SINGH 
                //DESCRIPTION  : TO DISPLAY MSG OF RECORD ALREADY EXIST

                CustomStatus cs = (CustomStatus)objVMC.AddUpdateVehicleBillEntry(objVM);

                if (cs.Equals(CustomStatus.RecordExist))
                {

                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                    return;
                }
                BindlistView(Convert.ToInt32(rdblisttourtype.SelectedValue.ToString()));
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                Clear();
                rdblisttourtype_SelectedIndexChanged(null, null);
            }
            else
            {
                objVM.BILL_ID = Convert.ToInt32(ViewState["BILL_ID"].ToString());
                objVMC.AddUpdateVehicleBillEntry(objVM);
                BindlistView(Convert.ToInt32(rdblisttourtype.SelectedValue.ToString()));
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                Clear();
                rdblisttourtype_SelectedIndexChanged(null, null);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int bill_id = int.Parse(btnEdit.CommandArgument);
            ViewState["BILL_ID"] = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("VEHICLE_BILL_ENTRY ", "*", "", "BILL_ID=" + bill_id, "BILL_ID");
            objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "", "VEHICLE_ID");
            ddlSuppiler.SelectedValue = ds.Tables[0].Rows[0]["SUPPILER_ID"].ToString();
            ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_ID"].ToString();
            txtBillFromDate.Text = ds.Tables[0].Rows[0]["BILL_FROM_DATE"].ToString();
            txtBillToDate.Text = ds.Tables[0].Rows[0]["BILL_TO_DATE"].ToString();
            txtHireFor.Text = ds.Tables[0].Rows[0]["HIRE_FOR"].ToString();


            // if (rdblisttourtype.SelectedItem.Text.Equals("College Bus"))
            if (rdblisttourtype.SelectedValue == "1" || rdblisttourtype.SelectedValue == "3" || rdblisttourtype.SelectedValue == "4")
            {
                txtExtraAmount.Text = ds.Tables[0].Rows[0]["EXTRA_AMOUNT"].ToString();
                txtHikeAmount.Text = ds.Tables[0].Rows[0]["HIKE_PRICE"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                txtRouteFrom.Text = ds.Tables[0].Rows[0]["ROUTE_FROM"].ToString();
                txtRouteTo.Text = ds.Tables[0].Rows[0]["ROUTE_TO"].ToString();
                txtBillAmount.Text = ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString();
                //txtRemark.Text = ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString();
            }
            else
            {
                txttravellingfromTime.Text = ds.Tables[0].Rows[0]["FROM_TIME"].ToString();
                txttravellingToTime.Text = ds.Tables[0].Rows[0]["TO_TIME"].ToString();
                txtpaidAmount.Text = ds.Tables[0].Rows[0]["PAID_AMOUNT"].ToString();

                lblBalanceAmount.Style.Add("display", "inherit");

                lblBalanceAmount.Text = ds.Tables[0].Rows[0]["BALANCE_AMOUNT"].ToString();
                txtHiredBy.Text = ds.Tables[0].Rows[0]["HIRED_BY"].ToString();
                txtTourPurpose.Text = ds.Tables[0].Rows[0]["TOUR_PURPOSE"].ToString();
                txtVisitPlace.Text = ds.Tables[0].Rows[0]["VISIT_PLACE"].ToString();
                lblTotalBillAmt.Text = ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString();
                txtTourRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                txtExtraDistanceAmt.Text = ds.Tables[0].Rows[0]["EXTRA_DISTANCE_AMOUNT"].ToString();
                txtTimeAmt.Text = ds.Tables[0].Rows[0]["EXTRA_TIME_AMOUNT"].ToString();
                txtBillAmt.Text = ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString();
                lblTotalBillAmt.Text = ds.Tables[0].Rows[0]["TOUR_TOTAL_AMOUNT"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView(int Trip_Type)
    {
        try
        {
            lvBillEntryList.DataSource = null;
            lvBillEntryList.DataBind();

            DataSet ds = objVMC.GetBillEntryBySuppiler(Trip_Type);
            if (Trip_Type == 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvBillEntryList.DataSource = ds;
                    lvBillEntryList.DataBind();
                    pnlCollegeBillEntry.Visible = true;
                }
                else
                {
                    lvBillEntryList.DataSource = null;
                    lvBillEntryList.DataBind();
                    pnlCollegeBillEntry.Visible = false;
                }

            }
            else if (Trip_Type == 2)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCollegeTourEntry.DataSource = ds;
                    lvCollegeTourEntry.DataBind();
                    pnlCollegeTourEntry.Visible = true;
                }
                else
                {

                    lvCollegeTourEntry.DataSource = null;
                    lvCollegeTourEntry.DataBind();
                    pnlCollegeTourEntry.Visible = false;
                }

            }
            else if (Trip_Type == 3 || Trip_Type == 4)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvBillEntryList.DataSource = ds;
                    lvBillEntryList.DataBind();
                    pnlCollegeBillEntry.Visible = true;
                }
                else
                {
                    lvBillEntryList.DataSource = null;
                    lvBillEntryList.DataBind();
                    pnlCollegeBillEntry.Visible = false;
                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ViewState["BILL_ID"] = null;
        ddlSuppiler.SelectedIndex = 0;
        if (ddlSuppiler.SelectedValue != "0")
        {
            ddlVehicle.SelectedIndex = 0;
        }
        txtBillFromDate.Text = string.Empty;
        txtBillToDate.Text = string.Empty;
        txtHireFor.Text = string.Empty;
        //college bus 
        txtBillAmount.Text = string.Empty;
        txtExtraAmount.Text = string.Empty;
        txtHikeAmount.Text = string.Empty;
        txtRemark.Text = string.Empty;
        //college Tour
        txtBillAmt.Text = string.Empty;
        txtpaidAmount.Text = string.Empty;
        txtExtraDistanceAmt.Text = string.Empty;
        txtTimeAmt.Text = string.Empty;
        lblTotalBillAmt.Text = string.Empty;
        lblBalanceAmount.Text = string.Empty;
        txtRouteFrom.Text = string.Empty;
        txtRouteTo.Text = string.Empty;
        txttravellingfromTime.Text = string.Empty;
        txttravellingToTime.Text = string.Empty;
        txtHiredBy.Text = string.Empty;
        txtTourPurpose.Text = string.Empty;
        txtVisitPlace.Text = string.Empty;
        txtTourRemark.Text = string.Empty;
        ddlVehicle.SelectedValue = "0";

        if (rdblisttourtype.SelectedValue == "1")
        {
            pnlCommon.Visible = true;
            pnlCollegeBus.Visible = true;
            pnlcollegetour.Visible = false;
        }
        else if (rdblisttourtype.SelectedValue == "2")
        {
            pnlCommon.Visible = true;
            pnlCollegeBus.Visible = false;
            pnlcollegetour.Visible = true;
        }
        else
        {
            pnlCommon.Visible = true;
            pnlCollegeBus.Visible = true;
            pnlcollegetour.Visible = false;
        }


    }

    protected void ddlSuppiler_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSuppiler.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER ", "VEHICLE_ID", "VEHICLE_NAME", "ACTIVE_STATUS=1 AND SUPPILER_ID=" + Convert.ToInt32(ddlSuppiler.SelectedValue), "VEHICLE_ID");


        }

    }
    protected void rdblisttourtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rdblisttourtype.SelectedItem.Text.Equals("College Bus"))


        if (rdblisttourtype.SelectedValue == "1")
        {
            pnlCommon.Visible = true;
            pnlCollegeBus.Visible = true;
            pnlcollegetour.Visible = false;
            BindlistView(Convert.ToInt32(rdblisttourtype.SelectedValue.ToString()));
            pnlCollegeBillEntry.Visible = true;
            pnlCollegeTourEntry.Visible = false;

        }
        else if (rdblisttourtype.SelectedValue == "2")
        {
            pnlCommon.Visible = true;
            pnlCollegeBus.Visible = false;
            pnlcollegetour.Visible = true;
            BindlistView(Convert.ToInt32(rdblisttourtype.SelectedValue.ToString()));
            pnlCollegeTourEntry.Visible = true;
            pnlCollegeBillEntry.Visible = false;
            Clear();

        }
        else
        {
            pnlCommon.Visible = true;
            pnlCollegeBus.Visible = true;
            pnlcollegetour.Visible = false;
            pnlCollegeBillEntry.Visible = true;
            pnlCollegeTourEntry.Visible = false;
            BindlistView(Convert.ToInt32(rdblisttourtype.SelectedValue.ToString()));
            Clear();

        }



    }

    protected void txtBillAmt_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVehicle.SelectedIndex > 0)
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER", "*", "", "SUPPILER_ID=" + Convert.ToInt32(ddlSuppiler.SelectedValue) + "AND VEHICLE_ID=" + Convert.ToInt32(ddlVehicle.SelectedValue), "VEHICLE_ID");
            txtRouteFrom.Text = ds.Tables[0].Rows[0]["FROM_LOCATION"].ToString();
            txtRouteTo.Text = ds.Tables[0].Rows[0]["TO_LOCATION"].ToString();

        }

    }
}
