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

public partial class VEHICLE_MAINTENANCE_Transaction_MonthlyFuelExpenseEntry : System.Web.UI.Page
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

                    }

                    BindlistView();
                    ViewState["Action"] = "Add";
                    objCommon.FillDropDownList(ddlSupplier, "VEHICLE_FUEL_SUPPILER_MASTER", "FUEL_SUPPILER_ID", "FUEL_SUPPILER_NAME", "IS_ACTIVE=1", "FUEL_SUPPILER_ID");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transport_Monthlyfuelexpense.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void BindlistView()
    {
        try
        {

            DataSet ds =  objCommon.FillDropDown("VEHICLE_MONTHLYFUELEXPENSEENTRY MF INNER JOIN VEHICLE_FUEL_SUPPILER_MASTER FS ON(FS.FUEL_SUPPILER_ID=MF.FUEL_SUPPILER_ID)", "MF.MFE_NO,MF.FROM_DATE,MF.TO_DATE", "MF.FUEL_SUPPILER_ID,MF.AMOUNT,FS.FUEL_SUPPILER_NAME", "", "");
           lvSuppiler.DataSource = ds;
            lvSuppiler.DataBind();
            ViewState["Table"] = ds;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transport_Monthlyfuelexpense.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        try
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transport_Monthlyfuelexpense.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.Fule_FromDate = Convert.ToDateTime(txtFromDate.Text);
            objVM.Fule_ToDate = Convert.ToDateTime(txtToDate.Text);
            objVM.FUEL_SUPPILER_ID = Convert.ToInt32(ddlSupplier.SelectedValue);
            objVM.Fuel_Amount = Convert.ToDouble(txtAmount.Text);
            if (ViewState["Action"].ToString().Equals("edit"))
            {
                objVM.MFE_NO = Convert.ToInt32(Session["MFE_NO"].ToString());
            }
            CustomStatus cs = (CustomStatus)objVMC.InsUpdMonthlyFuelExpense(objVM);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updActivity, "Record Update Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Record Already Exist on This Date   .", this.Page);
            }
            Clear();
            BindlistView();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transport_Monthlyfuelexpense.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void Clear()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Total", "<script type='text/javascript'>Clear();</script>", false);
        ViewState["Action"] = "Add";
        Session["MFE_NO"] = "0";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Img = sender as ImageButton;
        DataSet ds = (DataSet)ViewState["Table"];
        DataTable dt =ds.Tables[0] ;
        DataRow[] foundrows = dt.Select("MFE_NO=" + Img.CommandArgument);
        if (foundrows.Length > 0)
        {
            DataTable newTable = foundrows.CopyToDataTable();
            if (newTable.Rows.Count > 0)
            {
                txtAmount.Text = newTable.Rows[0]["AMOUNT"].ToString();
                txtFromDate.Text = newTable.Rows[0]["FROM_DATE"].ToString();
                txtToDate.Text = newTable.Rows[0]["TO_DATE"].ToString();
                ddlSupplier.SelectedValue = newTable.Rows[0]["FUEL_SUPPILER_ID"].ToString();
                ViewState["Action"] = "edit";
                Session["MFE_NO"] = Img.CommandArgument;
            }
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        DivReport.Visible = true;
        DivControl.Visible = false;
        
    }
    private void ShowLogBookReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtRFdate.Text).ToString("yyyy-MM-dd 00:00:00") + ",@P_TO_DATE=" + Convert.ToDateTime(txtRTdate.Text).ToString("yyyy-MM-dd 00:00:00") + ",@Fromdate=" + "From Date : " + txtRFdate.Text + ",@Todate=" + " - To Date : " + txtRTdate.Text;
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

    protected void btnShowRpt_Click(object sender, EventArgs e)
    {
        ShowLogBookReport("Monthly Fuel Expense Entry Report", "cryMonthlyFuelExpenseReport.rpt");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        DivControl.Visible = true;
        DivReport.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
}