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

public partial class VEHICLE_MAINTENANCE_Master_VehicleMaster : System.Web.UI.Page
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
                    BindListView();
                    pnlPersInfo.Visible = false;
                    objCommon.FillDropDownList(ddlVType, "VEHICLE_TYPEMASTER", "VTID", "VTNAME", "", "VTNAME");
                    pnlAddHis.Visible = true;
                    pnlButtons.Visible = false;
                }
               
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (rdblistStatus.SelectedValue == "1")
            {
                if (!txtRCDate.Text.Equals(string.Empty))
                {

                    if (Convert.ToDateTime(txtRCDate.Text) < Convert.ToDateTime(System.DateTime.Now))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('RC Expiry Date Should be Greater than Todays Date.');", true);
                        txtRCDate.Focus();
                        return;
                    }
                }

                if (!txtPUCDt.Text.Equals(string.Empty))
                {

                    if (Convert.ToDateTime(txtPUCDt.Text) < Convert.ToDateTime(System.DateTime.Now))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('PUC Expiry Date Should be Greater than Todays Date.');", true);
                        txtPUCDt.Focus();
                        return;
                    }
                }

                if (!txtRCDate.Text.Equals(string.Empty))
                {

                    if (Convert.ToDateTime(txtRCDate.Text) < Convert.ToDateTime(txtVPurchsDt.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('RC Expiry Date Should be Greater than Purchase Date.');", true);
                        txtRCDate.Focus();
                        return;
                    }
                }

                if (!txtPUCDt.Text.Equals(string.Empty))
                {

                    if (Convert.ToDateTime(txtPUCDt.Text) < Convert.ToDateTime(txtVPurchsDt.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('PUC Expiry Date Should be Greater than Purchase Date.');", true);
                        txtRCDate.Focus();
                        return;
                    }
                }




            }
            objVM.VNAME = txtVName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVName.Text.Trim());
            objVM.MAKE = txtVMake.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVMake.Text.Trim());
            objVM.MODEL = txtVModel.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVModel.Text.Trim());
            objVM.PURCHASEDT = Convert.ToDateTime(txtVPurchsDt.Text);
            objVM.VENDORNAME = txtVendorName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVendorName.Text.Trim());
            objVM.VENDORADD = txtVendorAdd.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVendorAdd.Text.Trim());
            objVM.REGDT = Convert.ToDateTime(txtVRegDate.Text);           
            objVM.ENGINENO = txtVEngineNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVEngineNo.Text.Trim());
            objVM.CHASISNO = txtVChasis.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVChasis.Text.Trim());
            objVM.REGNO = txtVRegNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVRegNo.Text.Trim());
            objVM.YROFMAKE = txtVMake.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVMake.Text.Trim());
            objVM.COKOR = txtVColor.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVColor.Text.Trim());            
            objVM.ALLINDIAPERMIT = txtVIPermit.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVIPermit.Text.Trim());


            objVM.RCBOOKNO = txtVRCBookNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVRCBookNo.Text.Trim());
            objVM.RCVALIDITY = txtRCValidity.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(txtRCValidity.Text);

            if (txtPUCDt.Text == string.Empty)
            {
                objVM.PUCDT = DateTime.MinValue;
            }
            else
            {
                objVM.PUCDT = Convert.ToDateTime(txtPUCDt.Text);
            }
            if (txtRCDate.Text == string.Empty)
            {
                objVM.RCVALIDITYDT = DateTime.MinValue;
            }
            else
            {
                objVM.RCVALIDITYDT = Convert.ToDateTime(txtRCDate.Text);
            }
            objVM.STATPERMIT = txtVPermit.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVPermit.Text.Trim());
          
            //by mrunal            
            objVM.DRIVE_TA = txtDvrTA.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDecimal(txtDvrTA.Text);
            objVM.KM_RATE = txtHireRate.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDecimal(txtHireRate.Text);
            objVM.WAITCHARGE = txtWaitCharge.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDecimal(txtWaitCharge.Text);
            objVM.LOGNO = txtStrtLgNo.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(txtStrtLgNo.Text);

            objVM.VTID = Convert.ToInt32(ddlVType.SelectedValue);
            objVM.VTAC = Convert.ToInt32(ddlVehicleTypeAC.SelectedValue);
            objVM.VEHICLE_NUMBER = txtVehNumber.Text.Trim().Equals(string.Empty) ? string.Empty : txtVehNumber.Text.Trim();
            objVM.Status = Convert.ToInt32(rdblistStatus.SelectedValue);
            
           

           
            

            if (ViewState["action"]!=null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)objVMC.AddVehicleMaster(objVM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                        clear();
                        pnlAddHis.Visible = false;
                        pnlButtons.Visible = true;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    if (ViewState["VIDNO"] != null)
                    {
                        objVM.VIDNO = Convert.ToInt32(ViewState["VIDNO"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.UpdVehicleMaster(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindListView();
                            clear();
                            objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                            ViewState["action"] = "add";
                            pnlAddHis.Visible = false;
                            pnlButtons.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleMaster.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }


    protected void BindListView()
    {
        DataSet ds = null;
        try
        {
            ds = objVMC.GetVehicleMasterAll();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDesg.DataSource = ds;
                lvDesg.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleMaster.BindListView -> " + ex.Message + " " + ex.StackTrace);
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
        txtDvrTA.Text = string.Empty;
        txtHireRate.Text = string.Empty;
        txtStrtLgNo.Text = string.Empty;
        txtVChasis.Text = string.Empty;
        txtVColor.Text = string.Empty;
        txtVendorAdd.Text = string.Empty;
        txtVendorName.Text = string.Empty;
        txtVEngineNo.Text = string.Empty;
        txtVIPermit.Text = string.Empty;
        txtVMake.Text = string.Empty;
        txtVModel.Text = string.Empty;
        txtVName.Text = string.Empty; 
        txtVPermit.Text = string.Empty;
        txtVPurchsDt.Text = string.Empty;
        txtVRCBookNo.Text = string.Empty;
        txtVRegDate.Text = string.Empty;
        txtVRegNo.Text = string.Empty;
        txtWaitCharge.Text = string.Empty;
        txtPUCDt.Text = string.Empty;
        txtRCDate.Text = string.Empty;
        txtRCValidity.Text = string.Empty;
        ddlVType.SelectedIndex = 0;
        ViewState["VIDNO"] = null;
        ViewState["action"] = "add";
        ddlVehicleTypeAC.SelectedIndex = 0;
        txtVehNumber.Text = string.Empty;//ADDED NANCY SHARMA
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlPersInfo.Visible = false;
        pnlList.Visible = true;
        pnlAddHis.Visible = true;
        pnlButtons.Visible = false;

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int no = int.Parse(btnEdit.CommandArgument);
            ViewState["VIDNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(no);

            pnlAddHis.Visible = false;
            pnlButtons.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int IdNo)
    {
        try
        {
            objVM.VIDNO = Convert.ToInt32(IdNo);
            DataSet ds = objVMC.GetVehicleMasterByINSIDNO(objVM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlAddHis.Visible = false;
                pnlPersInfo.Visible = true;
                pnlList.Visible = false;
                txtDvrTA.Text = ds.Tables[0].Rows[0]["DRIVE_TA"].ToString();
                txtHireRate.Text = ds.Tables[0].Rows[0]["KM_RATE"].ToString();
                txtStrtLgNo.Text = ds.Tables[0].Rows[0]["LOGNO"].ToString();
                txtVChasis.Text = ds.Tables[0].Rows[0]["CHASISNO"].ToString();
                txtVColor.Text = ds.Tables[0].Rows[0]["COLOR"].ToString();
                txtVendorAdd.Text = ds.Tables[0].Rows[0]["VENDORADD"].ToString();
                txtVendorName.Text = ds.Tables[0].Rows[0]["VENDORNAME"].ToString();
                txtVEngineNo.Text = ds.Tables[0].Rows[0]["ENGINENO"].ToString();
                txtVIPermit.Text = ds.Tables[0].Rows[0]["ALLINDIAPERMIT"].ToString();
                txtVMake.Text = ds.Tables[0].Rows[0]["YROFMAKE"].ToString();
                txtVModel.Text = ds.Tables[0].Rows[0]["MODEL"].ToString();
                txtVName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtVPermit.Text = ds.Tables[0].Rows[0]["STATPERMIT"].ToString();
                txtVPurchsDt.Text = ds.Tables[0].Rows[0]["PURCHASEDT"].ToString();
                txtVRCBookNo.Text = ds.Tables[0].Rows[0]["RCBOOKNO"].ToString();
                txtVRegDate.Text = ds.Tables[0].Rows[0]["REGDT"].ToString();
                txtVRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                txtWaitCharge.Text = ds.Tables[0].Rows[0]["WAITCHARGE"].ToString();
                if (ds.Tables[0].Rows[0]["YROFRCVALIDITY"].ToString() == "0")
                {
                    txtRCValidity.Text = string.Empty;
                }
                else
                {
                    txtRCValidity.Text = ds.Tables[0].Rows[0]["YROFRCVALIDITY"].ToString();
                }
                txtRCDate.Text = ds.Tables[0].Rows[0]["RCEXPIRYDT"].ToString();
                txtPUCDt.Text = ds.Tables[0].Rows[0]["PUCDT"].ToString();
                ddlVType.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_TYPE"].ToString();
                ddlVehicleTypeAC.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_AC_NONAC"].ToString();
                txtVehNumber.Text = ds.Tables[0].Rows[0]["VEHICLE_NUMBER"].ToString();
                rdblistStatus.SelectedValue = ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString(); 
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_VehicleMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        pnlPersInfo.Visible = true;
        pnlList.Visible = false;
        pnlAddHis.Visible = false;
        pnlButtons.Visible = true;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "rptRCValidityExpiry.rpt");
    }

    private void ShowReport(string exporttype, string rptFileName)
    {

        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=DriverLicenceExpiry" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;


            url += "&param=@P_EXPIRY_FOR=RC_VALIDITY,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value;



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
   
}
