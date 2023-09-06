using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Transaction_CollegeVehicleAttendanceEntry : System.Web.UI.Page
{
    CheckBox chkvehicleIdno = null;
    //CheckBox chkdriver = null;
    DataSet dsDATA;
   

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

                }                
                txttravellingDate.Text = System.DateTime.Now.Date.ToString();
                hdnDate.Value = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "NAME");
                //DataSet ds = objCommon.FillDropDown("VEHICLE_MASTER ", "VIDNO", "NAME", "VIDNO>0", "VIDNO");
                objCommon.FillDropDownList(ddlDriverCond, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "DCATEGORY=1", "DNAME");

                ViewState["VehAttendance"] = null;
                ViewState["ATTENDANCE_SRNO"] = null;
                Session["RecTbAttendance"] = null;
                BindlistView();
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
                if (Session["RecTbAttendance"] == null)
                {
                  MessageBox("Please Add The Details");
                  return;
                }

                objVM.TRAVELLING_DATE = Convert.ToDateTime(txttravellingDate.Text.Trim());
                DataTable dtAttendance;
                dtAttendance = (DataTable)Session["RecTbAttendance"];
                objVM.VEHICLE_ATTENDANCE_DT = dtAttendance;
                
                if (ViewState["ATTENDANCE_ID"] == null)               
                { 
                    int count = Convert.ToInt32(objCommon.LookUp("VEHICLE_COLLEGE_DAILY_ATTENDANCE", "count(*)", "CONVERT (VARCHAR,TRAVELLING_DATE,103)='" + txttravellingDate.Text + "'"));
                if (count > 0)
                {
                    MessageBox("Record Already Exist");
                    BindlistView();
                    lvVehDetails.Visible = false;
                    return;
                }
                        objVMC.AddUpdCollegeVehicleDailyAttendanceEntry(objVM);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                        //lvVehDetails.Visible = false;
                        BindlistView();
                        Clear();
                }
                else
                {
                   
                    objVM.ATTENDANCE_ID = Convert.ToInt32(ViewState["ATTENDANCE_ID"].ToString());                   
                    objVMC.AddUpdCollegeVehicleDailyAttendanceEntry(objVM);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                    //lvVehDetails.Visible = false;
                    //txttravellingDate.Enabled = true;
                    Clear();
                   // cetravellingDate.Enabled = true;
                    
                }
                Session["RecTbAttendance"] = null;

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
            ViewState["ATTENDANCE_ID"] = 1;
            txttravellingDate.Enabled = false;
            cetravellingDate.Enabled = false;
            ddlDriverCond.SelectedIndex = 0;
            ddlVehicle.SelectedIndex = 0;
            txtamount.Text = string.Empty;
            txtBeta.Text = string.Empty;
            txtTotalAmt.Text = string.Empty;
            ImageButton btnEdit = sender as ImageButton;            
            String Travelling_date = Convert.ToDateTime(btnEdit.CommandArgument.ToString()).ToString("yyyy-MM-dd");
            ViewState["Travelling_date"] = Travelling_date;
            txttravellingDate.Text = Convert.ToDateTime(btnEdit.CommandArgument.ToString()).ToString("dd/MM/yyyy");
            //DataSet ds = objCommon.FillDropDown("VEHICLE_MASTER", "*", "", "", "VIDNO");
            DataSet ds = objVMC.GetCollegeVehAttendancedetails(Convert.ToDateTime(txttravellingDate.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvVehDetails.DataSource = ds.Tables[0];
                lvVehDetails.DataBind();
                lvVehDetails.Visible = true;
                Session["RecTbAttendance"] = ds.Tables[0];
                ViewState["ATTENDANCE_SRNO"] = Convert.ToInt32((ds.Tables[0].AsEnumerable().Max(row => row["SRNO"])));
                
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
            objVM.TRAVELLING_DATE = Convert.ToDateTime(txttravellingDate.Text);
            DataSet ds = objVMC.GetAttendanceEntryByCollegeVeh(objVM);
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
    private void ShowDetails(int suppiler_id, string from, string To)
    {
        try
        {
           
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
        txttravellingDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");        
        txttravellingDate.Enabled = true;
        cetravellingDate.Enabled = false;
        ViewState["ATTENDANCE_ID"] = null;
        ddlDriverCond.SelectedIndex = 0;
        ddlVehicle.SelectedIndex = 0;
        txtamount.Text = string.Empty;
        txtBeta.Text = string.Empty;
        lvVehDetails.DataSource = null;
        lvVehDetails.DataBind();
        lvVehDetails.Visible = false;
        txtamount.Enabled = false;
        txtBeta.Enabled = false;
        txtTotalAmt.Text = string.Empty;
    }
   
   
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #region AddClick

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
           
            lvVehDetails.Visible = true;
            lvVehDetails.Enabled = true;
            DataTable dtAttendanceDup = (DataTable)Session["RecTbAttendance"];
           
           
            //if (checkDuplicateRow(dtAttendanceDup, txtDoctoralName.Text.Trim()))
            //{
            //    lvVehDetails.DataSource = dtAttendanceDup;
            //    lvVehDetails.DataBind();
            //    txtDoctoralName.Text = string.Empty;
            //    DisplayMessage("This name is already exist.");
            //    return;
            //}
            if (ViewState["VehAttendance"] == null)
            {
                if (Session["RecTbAttendance"] != null && ((DataTable)Session["RecTbAttendance"]) != null)
                {
                    int maxVal = 0;
                    DataTable dtAttendance = (DataTable)Session["RecTbAttendance"];
                    DataRow dr = dtAttendance.NewRow();

                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dtAttendance.AsEnumerable().Max(row => row["SRNO"]));
                    }
                   		
                    dr["SRNO"] = maxVal + 1;                   
                    dr["VEHICLE_ID"] = ddlVehicle.SelectedValue == "0" ? "0" : ddlVehicle.SelectedValue;
                    dr["VEHICLE_NAME"] = ddlVehicle.SelectedItem.Text.Trim();
                    dr["ATTENDANCE_MARK"] = chkAttendance.Checked ? true : false;
                    dr["ATTENDANCE_STATUS"] = chkAttendance.Checked ? "PRESENT" : "ABSENT";
                    dr["DRIVER_ID"] = ddlDriverCond.SelectedValue == "0" ? "0" : ddlDriverCond.SelectedValue;
                    dr["DRIVER_NAME"] = ddlDriverCond.SelectedItem.Text.Trim();
                    dr["DRIVER_TA_APPLY"] = txtamount.Text.Trim() == "" ? false : true;
                    dr["DRIVER_TA_AMOUNT"] = txtamount.Text == "" ? "0.0" : txtamount.Text;
                    dr["BETA"] = txtBeta.Text.Trim() == "" ? "0.0" : Convert.ToString(txtBeta.Text.Trim());
                    dr["TOTAL_AMOUNT"] = Convert.ToDouble(dr["BETA"]) + Convert.ToDouble(dr["DRIVER_TA_AMOUNT"]);
                    ClearDT();
                    dtAttendance.Rows.Add(dr);
                   
                    Session["RecTbAttendance"] = dtAttendance;
                    lvVehDetails.DataSource = dtAttendance;
                    lvVehDetails.DataBind();
                    CreateTabelVehAttendance();
                    ViewState["ATTENDANCE_SRNO"] = Convert.ToInt32(ViewState["ATTENDANCE_SRNO"]) + 1;
                }
                else
                {
                    DataTable dtAttendance = this.CreateTabelVehAttendance();
                    DataRow dr = dtAttendance.NewRow();
                    dr["SRNO"] = Convert.ToInt32(ViewState["ATTENDANCE_SRNO"]) + 1;
                    dr["VEHICLE_ID"] = ddlVehicle.SelectedValue == "0" ? "0" : ddlVehicle.SelectedValue;
                    dr["VEHICLE_NAME"] = ddlVehicle.SelectedItem.Text.Trim();
                    dr["ATTENDANCE_MARK"] = chkAttendance.Checked ? true : false;
                    dr["ATTENDANCE_STATUS"] = chkAttendance.Checked ? "PRESENT" : "ABSENT";
                    dr["DRIVER_ID"] = ddlDriverCond.SelectedValue == "0" ? "0" : ddlDriverCond.SelectedValue;
                    dr["DRIVER_NAME"] = ddlDriverCond.SelectedItem.Text.Trim();
                    dr["DRIVER_TA_APPLY"] = txtamount.Text.Trim() == "" ? false : true;
                    dr["DRIVER_TA_AMOUNT"] = txtamount.Text == "" ? "0.0" : txtamount.Text;
                    dr["BETA"] = txtBeta.Text.Trim() == "" ? "0.0" : Convert.ToString(txtBeta.Text.Trim());
                    dr["TOTAL_AMOUNT"] = Convert.ToDouble(dr["BETA"]) + Convert.ToDouble(dr["DRIVER_TA_AMOUNT"]);
                    ClearDT();
                    ViewState["ATTENDANCE_SRNO"] = Convert.ToInt32(ViewState["ATTENDANCE_SRNO"]) + 1;
                    dtAttendance.Rows.Add(dr);
                    
                    CreateTabelVehAttendance();
                    Session["RecTbAttendance"] = dtAttendance;
                    lvVehDetails.DataSource = dtAttendance;
                    lvVehDetails.DataBind();
                }
            }
            else
            {
                if (Session["RecTbAttendance"] != null && ((DataTable)Session["RecTbAttendance"]) != null)
                {
                    DataTable dtAttendance = (DataTable)Session["RecTbAttendance"];
                    DataRow dr = dtAttendance.NewRow();

                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_ATTENDANCE_SRNO"]);
                    dr["VEHICLE_ID"] = ddlVehicle.SelectedValue == "0" ? "0" : ddlVehicle.SelectedValue;
                    dr["VEHICLE_NAME"] = ddlVehicle.SelectedItem.Text.Trim();
                    dr["ATTENDANCE_MARK"] = chkAttendance.Checked ? true : false;
                    dr["ATTENDANCE_STATUS"] = chkAttendance.Checked ? "PRESENT" : "ABSENT";
                    dr["DRIVER_ID"] = ddlDriverCond.SelectedValue == "0" ? "0" : ddlDriverCond.SelectedValue;
                    dr["DRIVER_NAME"] = ddlDriverCond.SelectedItem.Text.Trim();
                    dr["DRIVER_TA_APPLY"] = txtamount.Text == "" ? false : true;
                    dr["DRIVER_TA_AMOUNT"] = txtamount.Text == "" ? "0.0" : txtamount.Text;
                    dr["BETA"] = txtBeta.Text.Trim() == "" ? "0.0" : Convert.ToString(txtBeta.Text.Trim());
                    dr["TOTAL_AMOUNT"] = Convert.ToDouble(dr["BETA"]) + Convert.ToDouble(dr["DRIVER_TA_AMOUNT"]);
                    ClearDT();
                    
                    dtAttendance.Rows.Add(dr);
                    Session["RecTbAttendance"] = dtAttendance;
                    lvVehDetails.DataSource = dtAttendance;
                    lvVehDetails.DataBind();
                    CreateTabelVehAttendance();
                    ViewState["ATTENDANCE_SRNO"] = Convert.ToInt32(ViewState["ATTENDANCE_SRNO"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_CollegeVehicleAttendanceEntry.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearDT()
    {
        ddlDriverCond.SelectedIndex = 0;
        ddlVehicle.SelectedIndex = 0;
        txtamount.Text = string.Empty;
        txtBeta.Text = string.Empty;
        txtTotalAmt.Text = string.Empty;
        chkAttendance.Checked = false;
        txtamount.Enabled = false;
        txtBeta.Enabled = false;
    }

    private DataTable CreateTabelVehAttendance()
    {
        DataTable dtAttendance = new DataTable();
        dtAttendance.Columns.Add(new DataColumn("SRNO", typeof(int)));       
        dtAttendance.Columns.Add(new DataColumn("VEHICLE_ID", typeof(int)));
        dtAttendance.Columns.Add(new DataColumn("VEHICLE_NAME", typeof(string)));
        dtAttendance.Columns.Add(new DataColumn("ATTENDANCE_MARK", typeof(bool)));
        dtAttendance.Columns.Add(new DataColumn("ATTENDANCE_STATUS", typeof(string)));
        dtAttendance.Columns.Add(new DataColumn("DRIVER_ID", typeof(string)));
        dtAttendance.Columns.Add(new DataColumn("DRIVER_NAME", typeof(string)));
        dtAttendance.Columns.Add(new DataColumn("DRIVER_TA_APPLY", typeof(bool)));
        dtAttendance.Columns.Add(new DataColumn("DRIVER_TA_AMOUNT", typeof(double)));
        dtAttendance.Columns.Add(new DataColumn("BETA", typeof(double)));
        dtAttendance.Columns.Add(new DataColumn("TOTAL_AMOUNT", typeof(double)));
        return dtAttendance;
    }

    private bool checkDuplicateRow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["NAME"].ToString().ToLower() == value.ToLower())
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
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.checkDuplicateDoctResearchRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    #endregion
    protected void btnEditVeh_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["EDIT_ATTENDANCE_SRNO"] = string.Empty;
            ImageButton btnEditVeh = sender as ImageButton;
            DataTable dtAttendance;
            if (Session["RecTbAttendance"] != null && ((DataTable)Session["RecTbAttendance"]) != null)
            {
                dtAttendance = ((DataTable)Session["RecTbAttendance"]);
                ViewState["EDIT_ATTENDANCE_SRNO"] = btnEditVeh.CommandArgument;

                DataRow dr = this.GetEditableDatarow(dtAttendance, btnEditVeh.CommandArgument);
                
                ddlVehicle.SelectedValue = dr["VEHICLE_ID"].ToString();
                //ddlVehicle.SelectedItem.Text = dr["VEHICLE_NAME"].ToString();
                ddlDriverCond.SelectedValue = dr["DRIVER_ID"].ToString();
                //ddlVehicle.SelectedItem.Text = dr["VEHICLE_NAME"].ToString();
                if (Convert.ToBoolean(dr["ATTENDANCE_MARK"].ToString()) == true)
                {
                    chkAttendance.Checked = true;
                    txtamount.Enabled = true;
                    txtamount.Enabled = true;
                }
                else
                {
                    chkAttendance.Checked = false;
                    txtamount.Enabled = false;
                    txtamount.Enabled = false;
                }
                txtamount.Text = dr["DRIVER_TA_AMOUNT"].ToString();
                txtBeta.Text = dr["BETA"].ToString();
                txtTotalAmt.Text = dr["TOTAL_AMOUNT"].ToString();
                dtAttendance.Rows.Remove(dr);
                Session["RecTbAttendance"] = dtAttendance;
                lvVehDetails.DataSource = dtAttendance;
                lvVehDetails.DataBind();
                lvVehDetails.Enabled = false;
                ViewState["VehAttendance"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.btnEditDoctReserch_Click -->" + ex.Message + "" + ex.StackTrace);
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
                objCommon.ShowError(Page, "Appraisal_Proforma.GetEditableDatarowFromTOG -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
    protected void chkAttendance_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAttendance.Checked)
        {
            txtBeta.Enabled = true;
            txtamount.Enabled = true;
        }
        else
        {
            txtBeta.Enabled = false;
            txtamount.Enabled = false;
        }

    }
}