//=====================================
//Created By : Gopal Anthati
//Created Date : 17/02/2021
//=====================================

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

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleRequisition : System.Web.UI.Page
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
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();                  

                }               
                FillDropDown();
                Session["dtGuestStaff"] = null;
                Session["dtVehicle"] = null;
                ViewState["Vehicle"] = null;
                ViewState["Action"] = "Add";
                BindListview();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRequisition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_EMPMAS", "IDNO", "TITLE+' '+FNAME+' '+MNAME+' '+LNAME AS NAME", "IDNO > 0", "FNAME");
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "REGNO +':'+VEHICLE_NAME", "VEHICLE_ID > 0 AND ACTIVE_STATUS=1", "VEHICLE_NAME");
        objCommon.FillDropDownList(ddlRInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");
    }

    public void Clear()
    {
        try
        {
            ddlInstitute.SelectedIndex = 0;           
            ddlVehicle.SelectedIndex = 0;
            rdlOneWay.SelectedValue = "1";            
            txtDateOfJourney.Text = string.Empty;
            ClearGuestStaff();
            ClearVehicle();
            Session["dtGuestStaff"] = null;
            Session["dtVehicle"] = null;
            ViewState["Vehicle"] = null;
            ViewState["Action"] = "Add";
            ViewState["SRNO_VEHICLE"] = null;
            ViewState["SRNO_GUESTSTAFF"] = null;
            lvGuestStaff.DataSource = null;
            lvGuestStaff.DataBind();
            lvGuestStaff.Visible = false;
            lvVehicle.DataSource = null;
            lvVehicle.DataBind();
            lvVehicle.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }

    private DataTable CreateArrivalTimeEntry()
    {

        DataTable dtATE = new DataTable();
        dtATE.Columns.Add(new DataColumn("ATID", typeof(int)));
        dtATE.Columns.Add(new DataColumn("ROUTEID", typeof(int)));
        dtATE.Columns.Add(new DataColumn("ARRIVAL_DATE_TIME", typeof(DateTime)));
        dtATE.Columns.Add(new DataColumn("REASON_ID", typeof(int)));
        dtATE.Columns.Add(new DataColumn("ROUTENAME", typeof(string)));
        dtATE.Columns.Add(new DataColumn("REASON", typeof(string)));

        return dtATE;

    }
    
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Date_Of_Journey = Convert.ToDateTime(txtDateOfJourney.Text).ToString("yyyy-MM-dd");
        int Veh_Req_Id = Convert.ToInt32(objCommon.LookUp("VEHICLE_REQUISITION", "COUNT(*)", "COLLEGE_ID =" + ddlInstitute.SelectedValue + " AND DATE_OF_JOURNEY='" + Date_Of_Journey + "'"));
        if (Veh_Req_Id > 0)
        {
            MessageBox("Record Already Exist For This Institution And Date Of Journey.");
            return;
        }

        if (Session["dtGuestStaff"] == null)
        {
            MessageBox("Please Add Details Of Guest/Staff");
            return;
        }
        if (Session["dtVehicle"] == null)
        {
            MessageBox("Please Add Vehicle Required");
            return;
        }


        DataTable dtVehicle = null;
        DataTable dtGuestStaff = null;
        try
        {
            ObjEnt.COLLEGE_ID = Convert.ToInt32(ddlInstitute.SelectedValue);
            ObjEnt.DATE_OF_JOURNEY = Convert.ToDateTime(txtDateOfJourney.Text);
            ObjEnt.ONE_WAY = Convert.ToInt32(rdlOneWay.SelectedValue);            
            ObjEnt.UANO = Convert.ToInt32(Session["userno"]);

            dtGuestStaff = (DataTable)Session["dtGuestStaff"];
            ObjEnt.VEHICLE_REQ_EMP_TBL = dtGuestStaff;

            dtVehicle = (DataTable)Session["dtVehicle"];
            ObjEnt.VEHICLE_REQ_VEH_TBL = dtVehicle;


            if (ViewState["Action"].ToString() == "Add")
            {
                ObjEnt.VEH_REQ_ID = 0;
                CustomStatus cs = (CustomStatus)ObjCon.InsUpdateVehicleRequisition(ObjEnt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    MessageBox("Record Saved Successfully.");
                }
                else
                {
                    MessageBox("Transaction Failed.");
                }
            }
            else
            {
                ObjEnt.VEH_REQ_ID = Convert.ToInt32(ViewState["VEH_REQ_ID"]);
                CustomStatus cs = (CustomStatus)ObjCon.InsUpdateVehicleRequisition(ObjEnt);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    MessageBox("Record Updated Successfully.");
                }
                else
                {
                    MessageBox("Transaction Failed.");
                }
            }
            BindListview();
            Clear(); 

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void BindListview()
    {
        DataSet ds = objCommon.FillDropDown("VEHICLE_REQUISITION A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)", "VEH_REQ_ID,DATE_OF_JOURNEY", "(CASE WHEN ONE_WAY=1 THEN 'One-Way' ELSE 'Two-Way' END) AS ONE_WAY,COLLEGE_NAME,(CASE  APPROVAL_STATUS WHEN 'A' THEN 'APPROVED' WHEN 'R' THEN 'REJECTED' ELSE 'PENDING' END) AS APPROVAL_STATUS", "", "VEH_REQ_ID DESC");
       lvVehicleReq.DataSource = ds;
       lvVehicleReq.DataBind();
       lvVehicleReq.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRequisition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }     

    private DataTable CreateGuestStaff()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO_EMP", typeof(int)));
        dt.Columns.Add(new DataColumn("ISGUEST", typeof(char)));
        dt.Columns.Add(new DataColumn("EMP_IDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("EMP_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("GUEST_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PIKUP_LOC", typeof(string)));
        dt.Columns.Add(new DataColumn("PIKUP_TIME", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("DROP_LOCATION", typeof(string)));
        dt.Columns.Add(new DataColumn("PHONE", typeof(string)));
        return dt;
    }

    private bool CheckDuplicateGuestStaffRow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PHONE"].ToString().ToLower() == value.ToLower())
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
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.checkDuplicateAdministrationRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void btnAddGuestStaff_Click(object sender, EventArgs e)
    {
        try
        {

            if (chkGuest.Checked)
            {
                if (txtGuestName.Text == string.Empty)
                {
                    MessageBox("Please Enter Guest Name");
                    divGuest.Style.Add("display", "block");
                    divStaff.Style.Add("display", "none");
                    return;
                }
            }
            else if (ddlStaff.SelectedIndex == 0)
            {
                MessageBox("Please Select Staff Name");
                return;
            }
            lvGuestStaff.Visible = true;
            DataTable dtGuestStaffDup = (DataTable)Session["dtGuestStaff"];

            if (CheckDuplicateGuestStaffRow(dtGuestStaffDup, txtPhone.Text.Trim()))
            {
                lvGuestStaff.DataSource = dtGuestStaffDup;
                lvGuestStaff.DataBind();
                txtPhone.Text = string.Empty;
                MessageBox("This Contact No. Is Already Exist.");
                return;
            }

            if (ViewState["GuestStaff"] == null)
            {
                if (Session["dtGuestStaff"] != null && ((DataTable)Session["dtGuestStaff"]) != null)
                {
                    int maxVal = 0;
                    DataTable dtGuestStaff = (DataTable)Session["dtGuestStaff"];
                    DataRow dr = dtGuestStaff.NewRow();

                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dtGuestStaff.AsEnumerable().Max(row => row["SRNO_EMP"]));
                    }
                    dr["SRNO_EMP"] = maxVal + 1;
                    dr["ISGUEST"] = chkGuest.Checked ? 'Y' : 'N';
                    dr["EMP_IDNO"] = Convert.ToInt32(ddlStaff.SelectedValue);
                    dr["EMP_NAME"] = ddlStaff.SelectedItem.Text.Trim();
                    dr["GUEST_NAME"] = txtGuestName.Text.Trim();
                    dr["PIKUP_LOC"] = txtPickupLoc.Text.Trim();
                    dr["PIKUP_TIME"] = txtPickupTime.Text.Trim() == "" ? string.Empty : Convert.ToDateTime(txtPickupTime.Text).ToString("hh:mm:ss");
                    dr["DROP_LOCATION"] = txtDropLoc.Text.Trim();
                    dr["PHONE"] = txtPhone.Text.Trim();

                    dtGuestStaff.Rows.Add(dr);

                    Session["dtGuestStaff"] = dtGuestStaff;
                    lvGuestStaff.DataSource = dtGuestStaff;
                    lvGuestStaff.DataBind();
                    ClearGuestStaff();
                    ViewState["SRNO_GUESTSTAFF"] = Convert.ToInt32(ViewState["SRNO_GUESTSTAFF"]) + 1;
                }
                else
                {
                    DataTable dtGuestStaff = this.CreateGuestStaff();
                    DataRow dr = dtGuestStaff.NewRow();
                    dr["SRNO_EMP"] = Convert.ToInt32(ViewState["SRNO_GUESTSTAFF"]) + 1;
                    dr["ISGUEST"] = chkGuest.Checked ? 'Y' : 'N';
                    dr["EMP_IDNO"] = Convert.ToInt32(ddlStaff.SelectedValue);
                    dr["EMP_NAME"] = ddlStaff.SelectedItem.Text.Trim();
                    dr["GUEST_NAME"] = txtGuestName.Text.Trim();
                    dr["PIKUP_LOC"] = txtPickupLoc.Text.Trim();
                    dr["PIKUP_TIME"] = txtPickupTime.Text.Trim() == "" ? string.Empty : Convert.ToDateTime(txtPickupTime.Text).ToString("hh:mm:ss");
                    dr["DROP_LOCATION"] = txtDropLoc.Text.Trim();
                    dr["PHONE"] = txtPhone.Text.Trim();

                    ViewState["SRNO_GUESTSTAFF"] = Convert.ToInt32(ViewState["SRNO_GUESTSTAFF"]) + 1;
                    dtGuestStaff.Rows.Add(dr);

                    ClearGuestStaff();
                    Session["dtGuestStaff"] = dtGuestStaff;
                    lvGuestStaff.DataSource = dtGuestStaff;
                    lvGuestStaff.DataBind();
                }
            }
            else
            {
                if (Session["dtGuestStaff"] != null && ((DataTable)Session["dtGuestStaff"]) != null)
                {
                    DataTable dtGuestStaff = (DataTable)Session["dtGuestStaff"];
                    DataRow dr = dtGuestStaff.NewRow();

                    dr["SRNO_EMP"] = Convert.ToInt32(ViewState["EDIT_SRNO_GUESTSTAFF"]);
                    dr["ISGUEST"] = chkGuest.Checked ? 'Y' : 'N';
                    dr["EMP_IDNO"] = Convert.ToInt32(ddlStaff.SelectedValue);
                    dr["EMP_NAME"] = ddlStaff.SelectedItem.Text.Trim();
                    dr["GUEST_NAME"] = txtGuestName.Text.Trim();
                    dr["PIKUP_LOC"] = txtPickupLoc.Text.Trim();
                    dr["PIKUP_TIME"] = txtPickupTime.Text.Trim() == "" ? string.Empty : Convert.ToDateTime(txtPickupTime.Text).ToString("hh:mm:ss");
                    dr["DROP_LOCATION"] = txtDropLoc.Text.Trim();
                    dr["PHONE"] = txtPhone.Text.Trim();

                    dtGuestStaff.Rows.Add(dr);
                    Session["dtGuestStaff"] = dtGuestStaff;
                    lvGuestStaff.DataSource = dtGuestStaff;
                    lvGuestStaff.DataBind();
                    lvGuestStaff.Enabled = true;
                    ClearGuestStaff();
                    ViewState["SRNO_GUESTSTAFF"] = Convert.ToInt32(ViewState["SRNO_GUESTSTAFF"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRequisition.btnAddGuestStaff_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearGuestStaff()
    {       
        ddlStaff.SelectedIndex = 0;
        chkGuest.Checked = false;
        txtDropLoc.Text = string.Empty;
        txtGuestName.Text = string.Empty;
        txtPickupLoc.Text = string.Empty;
        txtPickupTime.Text = string.Empty;
        txtPhone.Text = string.Empty;
        divGuest.Style.Add("display","none");
        divStaff.Style.Add("display", "block"); 
    }

    private DataRow GetEditDatarowGuestStaff(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO_EMP"].ToString() == value)
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

    protected void btnEditGuestStaff_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["EDIT_SRNO_GUESTSTAFF"] = string.Empty;
            ImageButton btnEditGuestStaff = sender as ImageButton;
            DataTable dtGuestStaff;
            if (Session["dtGuestStaff"] != null && ((DataTable)Session["dtGuestStaff"]) != null)
            {
                dtGuestStaff = ((DataTable)Session["dtGuestStaff"]);
                ViewState["EDIT_SRNO_GUESTSTAFF"] = btnEditGuestStaff.CommandArgument;

                DataRow dr = this.GetEditDatarowGuestStaff(dtGuestStaff, btnEditGuestStaff.CommandArgument);
                if(dr["ISGUEST"].ToString() == "Y")
                {
                    chkGuest.Checked=true;
                    divGuest.Style.Add("display","block");
                    divStaff.Style.Add("display", "none");
                    txtGuestName.Text = dr["GUEST_NAME"].ToString();
                }
                else{
                    chkGuest.Checked = false;
                    divStaff.Style.Add("display", "block");
                    divGuest.Style.Add("display", "none");
                    ddlStaff.SelectedValue = dr["EMP_IDNO"].ToString();  
                }
                txtPickupLoc.Text = dr["PIKUP_LOC"].ToString();
                txtPickupTime.Text = Convert.ToDateTime(dr["PIKUP_TIME"]).ToString("hh:mm");
                txtDropLoc.Text = dr["DROP_LOCATION"].ToString();
                txtPhone.Text = dr["PHONE"].ToString();

                dtGuestStaff.Rows.Remove(dr);
                lvGuestStaff.Enabled = false;
                Session["dtGuestStaff"] = dtGuestStaff;
                lvGuestStaff.DataSource = dtGuestStaff;
                lvGuestStaff.DataBind();
                ViewState["GuestStaff"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Appraisal_Proforma.btnEditAdministration_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancelGuestStaff_Click(object sender, EventArgs e)
    {
        ClearGuestStaff();
    }

    private bool CheckDuplicateVehRow(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["VNAME"].ToString().ToLower() == value.ToLower())
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
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalProforma.checkDuplicateAdministrationRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    protected void btnAddVeh_Click(object sender, EventArgs e)
    {
        try
        {
            lvVehicle.Visible = true;
            DataTable dtVehicleDup = (DataTable)Session["dtVehicle"];

            if (CheckDuplicateVehRow(dtVehicleDup, ddlVehicle.SelectedItem.Text.Trim()))
            {
                lvVehicle.DataSource = dtVehicleDup;
                lvVehicle.DataBind();
                ddlVehicle.SelectedIndex=0;
                MessageBox("This Vehicle Is Already Exist.");
                return;
            }

            if (ViewState["Vehicle"] == null)
            {
                if (Session["dtVehicle"] != null && ((DataTable)Session["dtVehicle"]) != null)
                {
                    int maxVal = 0;
                    DataTable dtVehicle = (DataTable)Session["dtVehicle"];
                    DataRow dr = dtVehicle.NewRow();

                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dtVehicle.AsEnumerable().Max(row => row["SRNO_VEH"]));
                    }
                    dr["SRNO_VEH"] = maxVal + 1;
                    dr["VEHICLE_ID"] = Convert.ToInt32(ddlVehicle.SelectedValue);
                    dr["VNAME"] = ddlVehicle.SelectedItem.Text; 
                    dr["VEHICLE_AC_NONAC"] = ddlVehicleTypeAC.SelectedItem.Text;                   

                    dtVehicle.Rows.Add(dr);

                    Session["dtVehicle"] = dtVehicle;
                    lvVehicle.DataSource = dtVehicle;
                    lvVehicle.DataBind();
                    ClearVehicle();
                    ViewState["SRNO_VEHICLE"] = Convert.ToInt32(ViewState["SRNO_VEHICLE"]) + 1;
                }
                else
                {
                    DataTable dtVehicle = this.CreateVehicle();
                    DataRow dr = dtVehicle.NewRow();
                    dr["SRNO_VEH"] = Convert.ToInt32(ViewState["SRNO_VEHICLE"]) + 1;
                    dr["VEHICLE_ID"] = Convert.ToInt32(ddlVehicle.SelectedValue);
                    dr["VNAME"] = ddlVehicle.SelectedItem.Text;
                    dr["VEHICLE_AC_NONAC"] = ddlVehicleTypeAC.SelectedItem.Text; 

                    ViewState["SRNO_VEHICLE"] = Convert.ToInt32(ViewState["SRNO_VEHICLE"]) + 1;
                    dtVehicle.Rows.Add(dr);

                    ClearVehicle();
                    Session["dtVehicle"] = dtVehicle;
                    lvVehicle.DataSource = dtVehicle;
                    lvVehicle.DataBind();
                }
            }
            else
            {
                if (Session["dtVehicle"] != null && ((DataTable)Session["dtVehicle"]) != null)
                {
                    DataTable dtVehicle = (DataTable)Session["dtVehicle"];
                    DataRow dr = dtVehicle.NewRow();

                    dr["SRNO_VEH"] = Convert.ToInt32(ViewState["EDIT_SRNO_VEH"]);
                    dr["VEHICLE_ID"] = Convert.ToInt32(ddlVehicle.SelectedValue);
                    dr["VNAME"] = ddlVehicle.SelectedItem.Text;
                    dr["VEHICLE_AC_NONAC"] = ddlVehicleTypeAC.SelectedItem.Text; 

                    dtVehicle.Rows.Add(dr);
                    Session["dtVehicle"] = dtVehicle;
                    lvVehicle.DataSource = dtVehicle;
                    lvVehicle.DataBind();
                    lvVehicle.Enabled = true;
                    ClearVehicle();
                    ViewState["SRNO_VEHICLE"] = Convert.ToInt32(ViewState["SRNO_VEHICLE"]) + 1;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRequisition.btnAddVeh_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable CreateVehicle()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO_VEH", typeof(int)));
        dt.Columns.Add(new DataColumn("VEHICLE_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("VNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("VEHICLE_AC_NONAC", typeof(string)));        
        return dt;
    }

    private void ClearVehicle()
    {
        ddlVehicle.SelectedIndex = 0;
        ddlVehicleTypeAC.SelectedIndex = 0;
    }
    protected void btnVehEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["EDIT_SRNO_VEH"] = string.Empty;
            ImageButton btnEditVehicle = sender as ImageButton;
            DataTable dtVehicle;
            if (Session["dtVehicle"] != null && ((DataTable)Session["dtVehicle"]) != null)
            {
                dtVehicle = ((DataTable)Session["dtVehicle"]);
                ViewState["EDIT_SRNO_VEH"] = btnEditVehicle.CommandArgument;

                DataRow dr = this.GetEditDatarowVehicle(dtVehicle, btnEditVehicle.CommandArgument);
                
                ddlVehicle.SelectedValue = dr["VEHICLE_ID"].ToString();
                if (dr["VEHICLE_AC_NONAC"].ToString() == "AC")
                    ddlVehicleTypeAC.SelectedValue = "1";
                else
                    ddlVehicleTypeAC.SelectedValue = "2";
                
               
                dtVehicle.Rows.Remove(dr);
                lvVehicle.Enabled = false;
                Session["dtVehicle"] = dtVehicle;
                lvVehicle.DataSource = dtVehicle;
                lvVehicle.DataBind();
                ViewState["Vehicle"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Appraisal_Proforma.btnEditAdministration_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditDatarowVehicle(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO_VEH"].ToString() == value)
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
    protected void btnCancelVeh_Click(object sender, EventArgs e)
    {
        ClearVehicle();
    }
    protected void btnEditReq_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton IMG = sender as ImageButton;
        int VehReqId = Convert.ToInt32(IMG.CommandArgument);
        ViewState["Action"] = "Edit";
        ViewState["VEH_REQ_ID"] = VehReqId;
        int Count = Convert.ToInt32(objCommon.LookUp("VEHICLE_REQUISITION_PASS_ENTRY", "COUNT(*)", "STATUS IN ('R','A') AND VEH_REQ_ID ="+VehReqId));
        if (Count > 0)
        {
            MessageBox("Approved or Rejected or Inprocess Requisitions Are Not Editable");
            return;
        }
        ShowReqDetails(VehReqId);
    }

    private void ShowReqDetails(int VehReqId)
    {
        DataSet ds = ObjCon.GetAllReqDetailsForEdit(VehReqId);        
        ddlInstitute.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        txtDateOfJourney.Text = ds.Tables[0].Rows[0]["DATE_OF_JOURNEY"].ToString();
        rdlOneWay.SelectedValue = ds.Tables[0].Rows[0]["ONE_WAY"].ToString();
                
        lvGuestStaff.DataSource = ds.Tables[1];
        lvGuestStaff.DataBind();
        Session["dtGuestStaff"] = ds.Tables[1];
        lvGuestStaff.Visible = true;

        lvVehicle.DataSource = ds.Tables[2];
        lvVehicle.DataBind();
        Session["dtVehicle"] = ds.Tables[2];
        lvVehicle.Visible = true;
    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "rptVehicleRequisition.rpt");     
    }
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Date_Of_Journey =  Convert.ToDateTime(txtDOJ.Text).ToString("yyyy-MM-dd");
            string Veh_Req_Id = objCommon.LookUp("VEHICLE_REQUISITION", "isnull(VEH_REQ_ID,0)", "COLLEGE_ID =" + ddlRInstitute.SelectedValue + " AND DATE_OF_JOURNEY='" + Date_Of_Journey + "'");
            if (Veh_Req_Id == "0" || Veh_Req_Id =="")
            {
                MessageBox("No Records Found");
                return;
            }

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=DrivingLicenceExpiry" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

            url += "&param=@P_VEH_REQ_ID=" +Convert.ToInt32(Veh_Req_Id);
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
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divReport.Visible = false;
        divList.Visible = true;
        divVehReq.Visible = true;
        ddlRInstitute.SelectedIndex = 0;
        txtDOJ.Text = string.Empty;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        divReport.Visible = true;
        divVehReq.Visible = false;
        //divddlinstitution.Visible = true;
        //divSateOdJourny.Visible = true;
        //btnRpt.Visible = true;
        //btnBack.Visible = true;
    }
}