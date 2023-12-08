using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Facility_Status_Report : System.Web.UI.Page
{

    string date = "";
    int counter = 0;
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    //LeavesController objApp = new LeavesController();


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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                CheckPageAuthorization();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               
                FillDropdown();
              
               
                
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OD_Passing_Path.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OD_Passing_Path.aspx");
        }
    }

  

   

    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));          
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);                      
            string status;
            status = rdbleavestatus.SelectedValue.ToString().Trim();
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FACILITY_MANAGEMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FACILITY_MANAGEMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_STATUS=" + status.ToString().Trim() + ",@P_CentralizeDetailNo=" + Convert.ToInt32(ddlFacility.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue);

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);




        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Status_Report.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("FacilityStatus", "Facility_ApplicationStatus.rpt");
    }

   
  


   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
       
    }
    protected void Clear()
    {
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
      
        rdbleavestatus.SelectedIndex = 0;
       
        
        ddldept.SelectedIndex = ddlFacility.SelectedIndex = 0;
        rdbleavestatus.SelectedValue = "H".ToString().Trim();
    }  
  
    private void FillDropdown()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1

            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT DEPT", " DEPT.SUBDEPTNO", "DEPT.SUBDEPT", "", "DEPT.SUBDEPT");
            objCommon.FillDropDownList(ddlFacility, "Facility_CentralizeDetail", "CentralizeDetailNo","CenFacilityName","","CenFacilityName");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Status_Report.FillDropdown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
 
}
