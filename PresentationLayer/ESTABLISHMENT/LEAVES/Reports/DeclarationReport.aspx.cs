//======================================================================================
// PROJECT NAME  : UAIMS                                                             
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : DeclarationReport.aspx.cs                                             
// CREATION DATE :                                                    
// CREATED BY    :     
// Modified By   : Swati Ghate
// MODIFIED DATE : 08-04-2016
// MODIFIED DESC : 
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class DeclarationReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();  
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

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillCollege();
                FillStaff();
                this.FillEmployee();
            }
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");
        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }
    private void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaffType, "payroll_staffTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void rblAllParticular_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAllParticular.SelectedValue == "1")
            tremp.Visible = true;
        else
            tremp.Visible = false;
    }
    public void FillEmployee()
    {      
        if (ddlCollege.SelectedValue!="0"  && ddlStaffType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.ACTIVE='Y' AND EM.college_no =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND EM.STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " ", "ENAME");
        }
        else if (ddlCollege.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.ACTIVE='Y' AND EM.college_no =" + Convert.ToInt32(ddlCollege.SelectedValue) + " ", "ENAME");
        }
    }
    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int empno = 0;
            //if (rblAllParticular.SelectedValue == "0")
            //{
            //    empno = 0;
            //}
            //else
            //{
            //    empno = Convert.ToInt32(ddlEmp.SelectedValue);
            //}
            empno = Convert.ToInt32(ddlEmp.SelectedValue);
            string Script = string.Empty;
            var Frm_Year = DateTime.Parse(txtFromdt.Text).Year;
            var To_Year = DateTime.Parse(txtTodate.Text).Year;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_YEAR=" + Frm_Year + ",@P_TO_YEAR=" + To_Year + ",@P_IDNO=" + empno;                    

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rblReport.SelectedValue == "1")
        {
            ShowReport("ODLeaveSummary", "ESTB_DeclarationReport.rpt");
        }
        else if (rblReport.SelectedValue == "0")
        {
            ShowReport("Faculty Report", "ESTB_DeclarationFacultyReport.rpt");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    private void Clear()
    {

        ddlEmp.SelectedValue = ddlCollege.SelectedValue = ddlStaffType.SelectedValue = "0";
        rblAllParticular.SelectedValue = "0";
        txtFromdt.Text = txtTodate.Text = string.Empty;
        ddlEmp.Items.Clear();
        // tremp.Visible = false;
    }
}
