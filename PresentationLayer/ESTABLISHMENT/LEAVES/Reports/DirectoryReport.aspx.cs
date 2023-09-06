//=====================================================================
//CREATED BY: Swati Ghate
//CREATED DATE:29-09-2015
//PURPOSE: TO DISPLAY EMPLOYEE TELEPHONE DIRECTORY REPORT
//=====================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class DirectoryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaveName = new LeavesController();
    SchoolMaster objSM = new SchoolMaster();
    SchoolController objSC = new SchoolController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        
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

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = true;
            
                pnlbutton.Visible = true;

               
                FillDropDown();
             }

        }
    }
    private void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_EmployeeListReport.FillDropDown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
          
            objSM.DEPT_NO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objSM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            if (ddlDepartment.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "*", "", "SUBDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("List", "EmployeeDirectoryReport.rpt");
                }
                else
                {
                    MessageBox("Record Not Found!");
                    return;
                }
            }
            else
            { 
                ShowReport("List", "EmployeeDirectoryReport.rpt");
            }
           
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_EmployeeListReport.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
                 
            url += "&param=@P_DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString(); 
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void Clear()
    {
        ddlDepartment.SelectedIndex  = 0;  
    }    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }     
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
   
}