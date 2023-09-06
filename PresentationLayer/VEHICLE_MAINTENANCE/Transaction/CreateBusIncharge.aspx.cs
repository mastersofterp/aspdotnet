//================================
//CREATED BY : GOPAL ANTHATI
//CREATED DATE : 12-02-2021
//================================
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

public partial class VEHICLE_MAINTENANCE_Transaction_CreateBusIncharge : System.Web.UI.Page
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
                    BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));              
                }
                
               
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

    private void BindList(int vehicle_cat)
    {
        try
        {
            DataSet ds = objVMC.GetBusInchargeAll(vehicle_cat);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBusIncharge.DataSource = ds;
                lvBusIncharge.DataBind();
            }
            else
            {
                lvBusIncharge.DataSource = null;
                lvBusIncharge.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.BindList -> " + ex.Message + " " + ex.StackTrace);
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
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "TITLE+' '+FNAME+' '+' '+MNAME+' '+LNAME AS EMPNAME", "IDNO>0", "FNAME");
        if (rdblistVehicleType.SelectedValue == "1")
        {
            objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "VIDNO");
        }
        else
        {
            objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0 AND ACTIVE_STATUS=1", "VEHICLE_ID");
        }
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int no = int.Parse(btnDelete.CommandArgument);
        objVM.INSIDNO = Convert.ToInt32(no);
        CustomStatus CS = (CustomStatus)objVMC.DeleteInsuranceByInsIdNo(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
        }
    }
    private void clear()
    {
        ddlVehicle.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["BUS_INC_ID"] = null;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int Bus_Inc_Id = int.Parse(imgBtn.CommandArgument);
        ViewState["BUS_INC_ID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(Bus_Inc_Id);
    }

    private void ShowDetails(int Bus_Inc_Id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_BUS_INCHARGE", "VIDNO", "IDNO,VEHICLE_CATEGORY", "BUS_INC_ID =" + Convert.ToInt32(Bus_Inc_Id), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
                if (ds.Tables[0].Rows[0]["VEHICLE_CATEGORY"].ToString() == Convert.ToString('C'))
                {
                    rdblistVehicleType.SelectedValue = "1";
                }
                else
                {
                    rdblistVehicleType.SelectedValue = "2";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objVM.VIDNO = Convert.ToInt32(ddlVehicle.SelectedValue);
            objVM.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue); 
            if (rdblistVehicleType.SelectedValue == "1")
            {
                objVM.VEHICLECAT = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLECAT = Convert.ToString('H');
            }
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objVM.BUS_INC_ID = 0;
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdBusIncharge(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        clear();
                        objCommon.DisplayMessage("Record Already Exist.", this.Page);
                        return;
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                        clear();
                        ViewState["action"] = "add";
                        BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                       
                    }
                }
                else
                {
                    if (ViewState["BUS_INC_ID"] != null)
                    {
                        objVM.BUS_INC_ID = Convert.ToInt32(ViewState["BUS_INC_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdBusIncharge(objVM);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            clear();
                            objCommon.DisplayMessage("Record Already Exist.", this.Page);
                            return;
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                            ViewState["action"] = "add";
                            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                           
                        }
                    }
                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }  
   
    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDown();
        if (rdblistVehicleType.SelectedValue == "1")
        { 
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            clear();
            ViewState["action"] = "add";

        }
        else
        {
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            clear();
            ViewState["action"] = "add";
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Bus Incharge Report", "rptBusIncharge.rpt");
    }
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=ItemIssueDetailReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.ShowItemIssueDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}