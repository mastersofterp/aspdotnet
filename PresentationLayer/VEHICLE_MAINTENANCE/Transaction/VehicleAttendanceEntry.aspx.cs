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

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleAttendanceEntry : System.Web.UI.Page
{
    CheckBox chkvehicleIdno = null;
    //CheckBox chkdriver = null;
    DataSet dsDATA;
    TextBox driver_Paid_Amount = null;
    TextBox Hike_Price = null;
    DropDownList ddlDriver = null;
    TextBox Beta = null;
    TextBox Over_Time = null;
    TextBox Total_Amount = null;

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
                BindlistView();
                txttravellingDate.Text = System.DateTime.Now.Date.ToString();
                hdnDate.Value = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                //txtDate.Text = System.DateTime.Now.Date.ToString();



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

            ViewState["Suppiler_id"] = ddlSuppiler.SelectedValue.ToString();
            Boolean IsCheck = false;
            foreach (ListViewItem li in lvStudent.Items)
            {
                IsCheck = true;
                ddlDriver = li.FindControl("ddlDriver") as DropDownList;
                if (ddlDriver.SelectedValue=="0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Driver Conductor Name.');", true);
                    return;
                }
                CheckBox chkdriver = li.FindControl("chkDriver") as CheckBox;
                CheckBox chkattendance = li.FindControl("chkattendance") as CheckBox;

                if (chkattendance.Checked==false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Checked Attendance Mark.');", true);
                    return;
                }
                driver_Paid_Amount = li.FindControl("txtamount") as TextBox;
                Beta = li.FindControl("txtBeta") as TextBox;

               

                //Total_Amount = li.FindControl("txtTotalAmt") as TextBox;
                HiddenField Total_Amount = li.FindControl("hdTotalAmt") as HiddenField;

                //objVM.TRAVELLING_DATE = Convert.ToDateTime(txttravellingDate.Text.Trim());
                objVM.TRAVELLING_DATE = Convert.ToDateTime(txttravellingDate.Text.Trim());
                objVM.SUPPILER_ID = Convert.ToInt32(ViewState["Suppiler_id"].ToString());
                objVM.VEHICLE_ID = Convert.ToInt32(chkattendance.ToolTip.ToString());
                if (chkattendance.Checked == true)
                {
                    objVM.ATTENDANCE_MARK = true;
                }
                else
                {
                    objVM.ATTENDANCE_MARK = false;
                }
                objVM.DRIVER_ID = Convert.ToInt32(ddlDriver.SelectedValue.ToString());

                if (driver_Paid_Amount.Text != "")
                {
                    objVM.DRIVER_TA_APPLY = true;
                    objVM.DRIVER_TA_AMOUNT = Convert.ToDouble(driver_Paid_Amount.Text.Trim());
                }
                else
                {
                    objVM.DRIVER_TA_APPLY = false;
                    objVM.DRIVER_TA_AMOUNT = 0.00;
                }

                if (Beta.Text == "")
                {
                    objVM.BETA = 0.00;
                }
                else
                {
                    objVM.BETA = Convert.ToDouble(Beta.Text.Trim());
                }

                if (Total_Amount.Value == "")
                {
                    objVM.TOTAL_AMOUNT = 0.00;
                }
                else
                {
                    //objVM.TOTAL_AMOUNT = Convert.ToDouble(Total_Amount.Text.Trim());
                    objVM.TOTAL_AMOUNT = Convert.ToDouble(driver_Paid_Amount.Text) + Convert.ToDouble(Beta.Text);
                }



                if (ViewState["ATTENDANCE_ID"] == null)
                //if (ViewState["Suppiler_Id"] == null && ViewState["Travelling_date"]==null)
                {


                    objVMC.AddUpdateVehicleDailyAttendanceEntry(objVM);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                    //Clear();
                    //BindlistView();



                }
                else
                {
                    //String Travelling_date = Convert.ToDateTime(btnEdit.CommandArgument.ToString()).ToString("yyyy-MM-dd");

                    //objVM.TRAVELLING_DATE = Convert.ToDateTime(ViewState["Travelling_date"].ToString());
                    //objVM.SUPPILER_ID = Convert.ToInt32(ViewState["Suppiler_id"].ToString());

                    objVM.ATTENDANCE_ID = 1;
                    objVM.TRAVELLING_DATE = Convert.ToDateTime(ViewState["Travelling_date"].ToString());

                    objVMC.AddUpdateVehicleDailyAttendanceEntry(objVM);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                    Clear();
                    //BindlistView();
                    //ViewState["SUPPILER_ID"] = null;


                }
            }
            if (!IsCheck)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select atleast one Vehicle.');", true);


            }
            else
            {

                if (ViewState["BILL_ID"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                    // rdblisttourtype_SelectedIndexChanged(null, null);
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                    // rdblisttourtype_SelectedIndexChanged(null, null);
                    Clear();

                }

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
            ViewState["ATTENDANCE_ID"] = "1";
            trvehicledetails.Visible = true;
            txttravellingDate.Enabled = false;
            ImageButton btnEdit = sender as ImageButton;
            int suppiler_id = int.Parse(btnEdit.ToolTip);
            ViewState["Suppiler_Id"] = suppiler_id;
            String Travelling_date = Convert.ToDateTime(btnEdit.CommandArgument.ToString()).ToString("yyyy-MM-dd");
            ViewState["Travelling_date"] = Travelling_date;
            txttravellingDate.Text = Convert.ToDateTime(btnEdit.CommandArgument.ToString()).ToString("dd/MM/yyyy");
            DataSet ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER", "*", "", "SUPPILER_ID=" + suppiler_id, "VEHICLE_ID");

            lvStudent.DataSource = ds;
            lvStudent.DataBind();

            foreach (ListViewItem li in lvStudent.Items)
            {
                ddlDriver = li.FindControl("ddlDriver") as DropDownList;

                objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "DCATEGORY=1", "DNAME");
            }

            DataSet ds1 = objCommon.FillDropDown("VEHICLE_DAILY_ATTENDANCE", "dbo.FN_DESC('SUPPILER_NAME',SUPPILER_ID) as SUPPILER_NAME, SUPPILER_ID", "TRAVELLING_DATE, VEHICLE_ID, DRIVER_ID, ATTENDANCE_MARK, DRIVER_TA_AMOUNT, DRIVER_TA_APPLY, BETA, TOTAL_AMOUNT", "SUPPILER_ID=" + suppiler_id + "  AND  TRAVELLING_DATE ='" + Travelling_date + "'", "VEHICLE_ID");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlSuppiler.SelectedValue = ds.Tables[0].Rows[0]["SUPPILER_ID"].ToString();

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {

                    foreach (ListViewDataItem li in lvStudent.Items)
                    {
                        ddlDriver = li.FindControl("ddlDriver") as DropDownList;
                        CheckBox chkdriver = li.FindControl("chkDriver") as CheckBox;
                        CheckBox chkattendance = li.FindControl("chkattendance") as CheckBox;
                        driver_Paid_Amount = li.FindControl("txtamount") as TextBox;
                        Beta = li.FindControl("txtBeta") as TextBox;
                        Total_Amount = li.FindControl("txtTotalAmt") as TextBox;
                        //HiddenField Total_Amount = li.FindControl("hdTotalAmt") as HiddenField;


                        if (dr["VEHICLE_ID"].ToString() == chkattendance.ToolTip.ToString())
                        {
                            ddlDriver.SelectedValue = dr["DRIVER_ID"].ToString();

                            if (Convert.ToBoolean(dr["ATTENDANCE_MARK"].ToString()))
                            {
                                chkattendance.Checked = true;
                            }
                            else
                            {
                                chkattendance.Checked = false;
                            }


                            driver_Paid_Amount.Text = dr["DRIVER_TA_AMOUNT"].ToString();
                            Beta.Text = dr["BETA"].ToString();
                            Total_Amount.Text = dr["TOTAL_AMOUNT"].ToString();
                        }
                    }
                }
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

    private void BindlistView()
    {
        try
        {
            objVM.SUPPILER_ID = Convert.ToInt32(ddlSuppiler.SelectedValue);
            DataSet ds = objVMC.GetAttendanceEntryBySuppiler(objVM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBillEntryList.DataSource = ds;
                lvBillEntryList.DataBind();
            }
            else                                      // by mrunal 19/08/14
            {
                lvBillEntryList.DataSource = null;
                lvBillEntryList.DataBind();
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
    //private void BindlistView()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("VEHICLE_DAILY_ATTENDANCE", "dbo.FN_DESC('SUPPILER_NAME',SUPPILER_ID) as SUPPILER_NAME", "TRAVELLING_DATE,SUPPILER_ID", "", "group by TRAVELLING_DATE,SUPPILER_ID  TRAVELLING_DATE"); 
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvBillEntryList.DataSource = ds;
    //            lvBillEntryList.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}



    private void ShowDetails(int suppiler_id, string from, string To)
    {
        try
        {



            //DataSet ds = objCommon.FillDropDown("VEHICLE_BILL_ENTRY", "*", "", "SUPPILER_ID=" + suppiler_id + "  AND  BILL_FROM_DATE ='" + from + "'  AND  BILL_TO_DATE='" + To + "'", "");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlSuppiler.SelectedValue = ds.Tables[0].Rows[0]["SUPPILER_ID"].ToString();
            //    txtBillFromDate.Text = ds.Tables[0].Rows[0]["BILL_FROM_DATE"].ToString();
            //    txtBillToDate.Text = ds.Tables[0].Rows[0]["BILL_TO_DATE"].ToString();
            //    txtHireFor.Text = ds.Tables[0].Rows[0]["HIRE_FOR"].ToString();


            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        foreach (ListViewDataItem li in lvStudent.Items)
            //        {
            //            chkvehicleIdno = li.FindControl("chkIDNo") as CheckBox;
            //            Bill_Amount = li.FindControl("txtbillamount") as TextBox;
            //            Hike_Price = li.FindControl("txthikeprice") as TextBox;
            //            if (dr["VEHICLE_ID"].ToString() == chkvehicleIdno.ToolTip)
            //            {
            //                chkvehicleIdno.Checked = true;

            //            }

            //            Bill_Amount.Text = dr["BILL_AMOUNT"].ToString();
            //            Hike_Price.Text = dr["HIKE_PRICE"].ToString();
            //        }

            //    } 



            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {

        ddlSuppiler.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        trvehicledetails.Visible = false;
        //ViewState["Suppiler_id"] = null;
        //txttravellingDate.Text = System.DateTime.Now.Date.ToString();
        txttravellingDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
        //txtDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
        txttravellingDate.Enabled = true;

        lvBillEntryList.DataSource = null;
        lvBillEntryList.DataBind();


    }

    protected void chkattendance_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListViewItem li in lvStudent.Items)
        {           
            CheckBox chkattendance = li.FindControl("chkattendance") as CheckBox;
            driver_Paid_Amount = li.FindControl("txtamount") as TextBox;            
            Beta = li.FindControl("txtBeta") as TextBox;
            //Total_Amount = li.FindControl("txtTotalAmt") as TextBox;
            HiddenField Total_Amount = li.FindControl("hdTotalAmt") as HiddenField;
            if (chkattendance.Checked == true)
            {

            }



            //CheckBox chkdriver = li.FindControl("chkDriver") as CheckBox;
            //DropDownList ddlDriver = li.FindControl("ddlDriver") as DropDownList;
            //driver_Paid_Amount = li.FindControl("txtamount") as TextBox;
            //CheckBox chkattendance = li.FindControl("chkattendance") as CheckBox;

            //if (chkattendance.Checked == true)
            //{
            //    if (ddlDriver.SelectedIndex <= 0)
            //    {
            //        chkattendance.Checked = false;
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Driver.');", true);
            //    }

            //}
        }

    }

    protected void chkDriver_CheckedChanged(object sender, EventArgs e)
    {

        foreach (ListViewItem li in lvStudent.Items)
        {

            CheckBox chkdriver = li.FindControl("chkDriver") as CheckBox;
            driver_Paid_Amount = li.FindControl("txtamount") as TextBox;
            CheckBox chkattendance = li.FindControl("chkattendance") as CheckBox;

            if (chkattendance.Checked == true)
            {
                if (chkdriver.Checked == true)
                {
                    if (driver_Paid_Amount.Text.Equals(string.Empty))
                    {
                        driver_Paid_Amount.Enabled = true;
                    }
                    else
                    {
                        driver_Paid_Amount.Enabled = false;
                    }
                }
                else
                {
                    if (driver_Paid_Amount.Text.Equals(string.Empty))
                    {
                        driver_Paid_Amount.Enabled = false;
                    }
                    else
                    {
                        driver_Paid_Amount.Enabled = true;
                    }

                }

            }
            else
            {

                if (chkdriver.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bus not Running.');", true);
                    chkdriver.Checked = false;
                }


            }

        }


    }


    protected void ddlSuppiler_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSuppiler.SelectedIndex > 0)
        {
            BindlistView();
            trvehicledetails.Visible = true;
            //pnlAttendanceList.Visible = true;
            DataSet ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER ", "VEHICLE_ID,SUPPILER_ID,VEHICLE_NAME", "FROM_LOCATION,TO_LOCATION ", "ACTIVE_STATUS=1 AND SUPPILER_ID=" + Convert.ToInt32(ddlSuppiler.SelectedValue), "VEHICLE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
            }
            foreach (ListViewItem li in lvStudent.Items)
            {
                ddlDriver = li.FindControl("ddlDriver") as DropDownList;

                objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "DCATEGORY=1", "DNAME");
            }


        }




    }
    //protected void txttravellingDate_TextChanged(object sender, EventArgs e)
    //{
    //if( (Convert.ToDateTime(txttravellingDate.Text)) > DateTime.Now )
    //{
    //    txttravellingDate.Text = Convert.ToString(DateTime.Now);
    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You should not select future date...!!');", true);
    //}


    //}
}
