//==============================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 07-OCT-2015
// DESCRIPTION   : TO ADD DETAILS OF HIRED VEHICLE.
//===============================================================

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

public partial class VEHICLE_MAINTENANCE_Master_VehicleHireMaster : System.Web.UI.Page
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
                    objCommon.FillDropDownList(ddlSuppiler, "VEHICLE_SUPPILER_MASTER", "SUPPILER_ID", "SUPPILER_NAME", "", "SUPPILER_NAME");
                    objCommon.FillDropDownList(ddlVType, "VEHICLE_TYPEMASTER", "VTID", "VTNAME", "", "VTNAME");
                    BindlistView();
                }
                //BindlistView();
            }  
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleHireMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   
    // This method is used to check the authorization of page.
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

   // This event is used to Submit the details of Hired vehicle.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.SUPPILER_ID = Convert.ToInt32(ddlSuppiler.SelectedValue.ToString());
            objVM.VEHICLE_NAME = txtVehicleName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVehicleName.Text);
            objVM.FROM_LOCATION = txtfromlocation.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtfromlocation.Text.Trim());
            objVM.TO_LOCATION = txtTolocation.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtTolocation.Text.Trim());
            objVM.IS_ACTIVE = true;
            objVM.HIRE_TYPE = Convert.ToInt32(ddlHireType.SelectedValue);
            objVM.HIRE_RATE = Convert.ToDecimal(txtHRate.Text=String.IsNullOrEmpty(txtHRate.Text)? "0" : txtHRate.Text);
            objVM.REGNO = txtRegNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRegNo.Text);
            objVM.FDATE = Convert.ToDateTime(txtFrmDt.Text.Trim());
            objVM.TDATE = Convert.ToDateTime(txtToDt.Text.Trim());
            objVM.VTID = Convert.ToInt32(ddlVType.SelectedValue);
            objVM.VTAC = Convert.ToInt32(ddlVehicleTypeAC.SelectedValue);
            objVM.VEHICLE_NUMBER = txtVehNumber.Text.Trim().Equals(string.Empty) ? string.Empty : txtVehNumber.Text.Trim();
            objVM.Status = Convert.ToInt32(rdblistStatus.SelectedValue);

            if (ViewState["VEHICLE_ID"] == null)
            {
                CustomStatus cs = (CustomStatus)objVMC.AddUpdateVehicleHireMaster(objVM);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    //Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                    //objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                Clear();
            }
            else
            {
                objVM.VEHICLE_ID = Convert.ToInt32(ViewState["VEHICLE_ID"].ToString());
                CustomStatus cs = (CustomStatus)objVMC.AddUpdateVehicleHireMaster(objVM);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist.');", true);
                    //objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                Clear();               
             }
          }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleHireMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This event is used to cancel the last selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //This event is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int vehicle_id = int.Parse(btnEdit.CommandArgument);
            ViewState["VEHICLE_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(vehicle_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleHireMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to bind the list of hired vehicles against the supplier.
    private void BindlistView()
    {
        int supplierID = Convert.ToInt32(ddlSuppiler.SelectedValue);
        try
        {
            //DataSet ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER V INNER JOIN VEHICLE_SUPPILER_MASTER S ON (V.SUPPILER_ID=S.SUPPILER_ID) INNER JOIN VEHICLE_TYPEMASTER VT ON (VT.VTID = V.VEHICLE_TYPE)", "*", "SUPPILER_NAME,VT.VTNAME", "V.SUPPILER_ID='" + Convert.ToInt32(ddlSuppiler.SelectedValue) + "'", "VEHICLE_ID");
            DataSet ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER V INNER JOIN VEHICLE_SUPPILER_MASTER S ON (V.SUPPILER_ID=S.SUPPILER_ID) INNER JOIN VEHICLE_TYPEMASTER VT ON (VT.VTID = V.VEHICLE_TYPE)", "V.VEHICLE_ID, V.SUPPILER_ID,	V.VEHICLE_NAME,	V.FROM_LOCATION, V.TO_LOCATION,	V.IS_ACTIVE,V.REGNO, V.HIRE_TYPE, V.HIRE_RATE", "V.FROM_DATE,	V.TO_DATE,	V.VEHICLE_TYPE,	V.VEHICLE_AC_NONAC,	V.VEHICLE_NUMBER,	ISNULL(V.ACTIVE_STATUS,1) as ACTIVESTATUS, S.SUPPILER_NAME,VT.VTNAME", "V.SUPPILER_ID='" + supplierID + "'", "V.VEHICLE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvHireVeh.DataSource = ds;
                lvHireVeh.DataBind();
                //lvHireVeh.Visible = true;
            }
            else
            {
                lvHireVeh.DataSource = null;
                lvHireVeh.DataBind();


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleHireMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to show the details of the selected record.
    private void ShowDetails(int vehicle_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_HIRE_MASTER", "VEHICLE_ID,	SUPPILER_ID,VEHICLE_NAME,FROM_LOCATION,	TO_LOCATION,IS_ACTIVE,REGNO,HIRE_TYPE,	HIRE_RATE", "FROM_DATE,	TO_DATE,	VEHICLE_TYPE,	VEHICLE_AC_NONAC,	VEHICLE_NUMBER,	ISNULL(ACTIVE_STATUS,1) as ACTIVESTATUS", "VEHICLE_ID=" + vehicle_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSuppiler.SelectedValue = ds.Tables[0].Rows[0]["SUPPILER_ID"].ToString();
                txtVehicleName.Text = ds.Tables[0].Rows[0]["VEHICLE_NAME"].ToString();              

                txtRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ddlHireType.SelectedValue = ds.Tables[0].Rows[0]["HIRE_TYPE"].ToString();
                txtHRate.Text = ds.Tables[0].Rows[0]["HIRE_RATE"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();

                txtfromlocation.Text = ds.Tables[0].Rows[0]["FROM_LOCATION"].ToString();
                txtTolocation.Text = ds.Tables[0].Rows[0]["TO_LOCATION"].ToString();
                ddlVType.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_TYPE"].ToString();
                ddlVehicleTypeAC.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_AC_NONAC"].ToString();
                txtVehNumber.Text = ds.Tables[0].Rows[0]["VEHICLE_NUMBER"].ToString();
                rdblistStatus.SelectedValue = ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString(); 
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleHireMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear the controles.
    private void Clear()
    {

        ddlSuppiler.SelectedIndex = 0; 
        txtVehicleName.Text = string.Empty;
        txtfromlocation.Text = string.Empty;
        txtTolocation.Text = string.Empty;
        lvHireVeh.DataSource = null;
        lvHireVeh.DataBind();
        ViewState["VEHICLE_ID"] = null;
        txtRegNo.Text = string.Empty;
        ddlHireType.SelectedIndex = 0;
        txtHRate.Text = string.Empty;
        txtFrmDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        ddlVType.SelectedIndex = 0;
        ddlVehicleTypeAC.SelectedIndex = 0;
        txtVehNumber.Text = string.Empty;
    }
    // This event is used to display the hired vehicle list according to the supplier.
    protected void ddlSuppiler_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSuppiler.SelectedIndex > 0)
        {
          
            BindlistView();
        }    
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        lvHireVeh.Visible = false;
        ShowHireDetails("Hire Vehicle Details", "rptHireDetails.rpt");
    }

    private void ShowHireDetails(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=HireVehicleDetailReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_HIRE_TYPE=" + ddlHireType.SelectedValue;
           
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Reports_HireVehicleDetail.ShowHireDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }  
          
     }      

   
}
