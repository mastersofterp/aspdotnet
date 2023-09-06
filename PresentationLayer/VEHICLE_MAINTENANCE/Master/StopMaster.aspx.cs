//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 18-AUG-2015
// DESCRIPTION   : USE TO CREATE VARIOUS STOP NAMES.
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

public partial class VEHICLE_MAINTENANCE_Master_StopMaster : System.Web.UI.Page
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
                    FillDropDownList();
                    BindlistView();
                    objVM.IPADDRESS = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (objVM.IPADDRESS == null || objVM.IPADDRESS == "")
                        objVM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                   NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    objVM.MACADDRESS = nics[0].GetPhysicalAddress().ToString();
                }               
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    private void FillDropDownList()
    {
        //objCommon.FillDropDownList(ddlCategory, "VEHICLE_CATEGORYMASTER", "VCID", "CATEGORYNAME", "IsActive=1 And EndTime='9999-12-31'", "CATEGORYNAME");
        objCommon.FillDropDownList(ddlCategory, "VEHICLE_CATEGORYMASTER", "VCID", "CATEGORYNAME", "", "CATEGORYNAME");
    }
    private void BindlistView()
    {
        try
        {
          //  DataSet ds = objCommon.FillDropDown("VEHICLE_STOPMASTER S LEFT JOIN VEHICLE_CATEGORYMASTER VC ON (VC.VCID=S.VCID and VC.IsActive=1 And VC.EndTime='9999-12-31')", "CATEGORYNAME", "S.*", "", "SEQNO");
            DataSet ds = objCommon.FillDropDown("VEHICLE_STOPMASTER S LEFT JOIN VEHICLE_CATEGORYMASTER VC ON (VC.VCID=S.VCID)", "CATEGORYNAME", "S.*", "", "SEQNO"); //12-07-2022 juned
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStop.DataSource = ds;
                lvStop.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objVM.STOPNAME = txtStopName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtStopName.Text.Trim());
            objVM.SEQNO = txtSeqNo.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDecimal(txtSeqNo.Text);
            objVM.STUDENT_FEE = txtStudentFee.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtStudentFee.Text);
            objVM.EMPLOYEE_FEE = txtEmployeeFee.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtEmployeeFee.Text);
            objVM.VCID = Convert.ToInt32(ddlCategory.SelectedValue);
        
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateStopMaster(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {

                        Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView();
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["STOPNO"] != null)
                    {
                        objVM.STOPNO = Convert.ToInt32(ViewState["STOPNO"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateStopMaster(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            ViewState["action"] = "add";

                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                            Clear();
                        }

                    }
                }
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int STOPNO = int.Parse(btnEdit.CommandArgument);
            ViewState["STOPNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(STOPNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int STOPNO)
    {
        try
        {
            FillDropDownList();
            DataSet ds = objCommon.FillDropDown("VEHICLE_STOPMASTER", "*", "", "STOPID=" + STOPNO, "STOPID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtStopName.Text = ds.Tables[0].Rows[0]["STOPNAME"].ToString();
                txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtStudentFee.Text = ds.Tables[0].Rows[0]["STUDENT_FEE"].ToString();
                txtEmployeeFee.Text = ds.Tables[0].Rows[0]["EMPLOYEE_FEE"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["VCID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtStopName.Text = string.Empty;
        txtSeqNo.Text = string.Empty;
        ViewState["STOPNO"] = null;
        ViewState["action"] = "add";
        txtStudentFee.Text = string.Empty;
        txtEmployeeFee.Text = string.Empty;
        ddlCategory.SelectedIndex = 0;
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {          
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=StopNameReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_STOPID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Stop Names", "rptStopNameReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_StopMaster.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}