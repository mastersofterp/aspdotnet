//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : ConsolidatedDSRReport.aspx.cs                                                 
// CREATION DATE : 31-July-2020                                                       
// CREATED BY    : Gopal Anthati                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Collections.Generic;

public partial class STORES_Reports_ConsolidatedDSRReport : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_DSR_Entry_Controller objDSR = new Str_DSR_Entry_Controller();
    StoreMasterController objStrMaster = new StoreMasterController();
    Str_Invoice_Entry_Controller objInvoice = new Str_Invoice_Entry_Controller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ////Page Authorization
                CheckPageAuthorization();
                CheckMainStoreUser();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.BindDepartment();
                if (ViewState["StoreUser"] != "MainStoreUser")
                {
                    Session["MDNO"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));//Added by Vijay 03-06-2020
                    ddlDepartment.SelectedValue = Session["MDNO"].ToString();//Added by Vijay 03-06-2020
                    ddlDepartment.Enabled = false;
                }
                //this.BindDSR();//Added by Vijay 03-06-2020
            }

        }
        divMsg.InnerHtml = string.Empty;
    }
    private bool CheckMainStoreUser()
    {
        try
        {
            if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
            {
                ViewState["StoreUser"] = "MainStoreUser";
                return true;
            }
            else
            {
                this.CheckDeptStoreUser();
                return false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DSR_Report.aspx.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            return false;
        }
    } //added by vijay 03-06-2020
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    } //added by vijay 03-06-2020
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }
    void BindDepartment()
    {
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add("Please Select");
        ddlDepartment.SelectedItem.Value = "0";
        DataSet ds = objDSR.GetAllDept();
        ddlDepartment.DataSource = ds;
        ddlDepartment.DataTextField = "MDNAME";
        ddlDepartment.DataValueField = "MDNO";
        ddlDepartment.DataBind();
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != string.Empty && txtTodate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtTodate.Text))
            {
                MessageBox("To Date Should Be Greater Than Or Equal To From Date ");
                return;
            }
        }
        ShowReport("Consolidated DSR Report", "ConsolidatedDSRReport.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        this.BindDepartment();
       
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        //string fromDate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
        //fromDate = fromDate.Substring(0, 10);
        //string toDate = (String.Format("{0:u}", Convert.ToDateTime(txtTodate.Text)));
        //toDate = toDate.Substring(0, 10);

        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +",@P_MDNO=" + ddlDepartment.SelectedValue;

            if (txtFromDate.Text.Trim() != string.Empty && txtTodate.Text.Trim() != string.Empty)
            {
                url += ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FROMDATE=null,@P_TODATE=null";
            }

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
       
    }
    //private void ShowFuelRegister(string exporttype, string rptFileName)
    //{
    //    try
    //    {
    //        string Script = string.Empty;
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "exporttype=" + exporttype;
    //        url += "&filename=FuelRegisterReport" + "." + rdoReportType.SelectedValue;
    //        url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_FROMDATE=" + fromDate + "," + "@P_TODATE=" + toDate + " @P_MDNO=" + ddlDepartment.SelectedValue;



    //        if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
    //        {
    //            url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
    //        }
    //        else
    //        {
    //            url += ",@P_FDATE=null,@P_TDATE=null";
    //        }



    //        // To open new window from Updatepanel
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
}