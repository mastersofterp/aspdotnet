//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE   CERTIFICATE                        
// CREATION DATE : 23-NOVEMBER-2021                                                          
// CREATED BY    : PURVA RAUT                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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
using IITMS.SQLServer.SQLDAL;


public partial class PAYROLL_REPORTS_EmployeeCertificateReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    int employeelogin = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        if (ua_type != 1)
        {
            employeelogin = 1;
        }
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                if (ua_type != 1)
                {
                    //trCertificate.Visible = false;
                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    ddlCollege.SelectedIndex = 1;
                    ddlStaffNo.SelectedIndex = 1;
                    ddlEmployeeNo.SelectedIndex = 1;
                }
                else
                {
                    PopulateDropDownList();
                }

            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?Pay_PaySlip.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PaySlip.aspx");
        }
    }
    protected void PopulateDropDownList()
    {
        try
        {
           objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
           objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
           objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

         }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void PopulateDropDownListForFaculty()
    {
        ddlCollege.Enabled = false;
        ddlStaffNo.Enabled = false;
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);
        int emptypeno = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "EMPTYPENO", "idno=" + IDNO));
        int collegeNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + IDNO));

        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID=" + collegeNo, "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),EmployeeId) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");
         }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCharacterCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex < 0 && ddlStaffNo.SelectedIndex < 0 && ddlEmployeeNo.SelectedIndex  < 0)
            {
                objCommon.DisplayMessage("Please Select College,Staff  and Employee", this.Page);
            }
            else
            {
                ShowEmployeeCharacterCertificate("Employee Character Certificate", "PayEmpCharacterCertificateReport.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmployeeCertificateReports.btnCharacterCertificate_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }  
    }
    private void ShowEmployeeCharacterCertificate(string reportTitle, string rptFileName)
    {
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@StaffId=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@CollegeNo=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",Puporse=" + txtforpurpose.Text.Trim();
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //sb.Append(@"window.open('" + url + "','','" + features + "');");

        //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }
    protected void btnAddressCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex < 0 && ddlStaffNo.SelectedIndex < 0 && ddlEmployeeNo.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select College,Staff  and Employee", this.Page);
            }
            else
            {
                ShowEmployeeAddressCertificate("Employee Character Certificate", "PayEmpAddressCertificateReport.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmployeeCertificateReports.btnAddressCertificate_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        } 
    }


    protected void btnExperienceCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex < 0 && ddlStaffNo.SelectedIndex < 0 && ddlEmployeeNo.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select College,Staff  and Employee", this.Page);
            }
            else
            {
                ShowEmployeeExperienceCertificate("Employee Character Certificate", "PayEmpExperienceCertificateReport.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmployeeCertificateReports.btnExperienceCertificate_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnNoObjectionCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex < 0 && ddlStaffNo.SelectedIndex < 0 && ddlEmployeeNo.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select College,Staff  and Employee", this.Page);
            }
            else
            {
                ShowEmployeeNoObjectionCertificate("Employee Character Certificate", "PayEmpNoObjectionReport.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmployeeCertificateReports.btnNoObjectionCertificate_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO="+ddlStaffNo.SelectedValue+" AND  COLLEGE_NO="+ddlCollege.SelectedValue,"FNAME" );
    }
    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),EmployeeId) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND  COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

    }
    protected void ddlEmployeeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    private void ShowEmployeeExperienceCertificate(string reportTitle, string rptFileName)
    {


        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) +",@StaffId=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@CollegeNo=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
      
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //sb.Append(@"window.open('" + url + "','','" + features + "');");

        //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    private void ShowEmployeeAddressCertificate(string reportTitle, string rptFileName)
    {


        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@StaffId=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@CollegeNo=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",Puporse="+txtforpurpose.Text.Trim();
      
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //sb.Append(@"window.open('" + url + "','','" + features + "');");

        //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }
 
    private void ShowEmployeeNoObjectionCertificate(string reportTitle, string rptFileName)
    {
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@StaffId=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@CollegeNo=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",Puporse=" + txtforpurpose.Text.Trim()+",Remark="+txtremark.Text.Trim();     
  

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //sb.Append(@"window.open('" + url + "','','" + features + "');");

        //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnEmployeeCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex < 0 && ddlStaffNo.SelectedIndex < 0 && ddlEmployeeNo.SelectedIndex < 0)
            {
                objCommon.DisplayMessage("Please Select College,Staff  and Employee", this.Page);
            }
            else
            {
                ShowEmployeeCertificate("Employee Character Certificate", "PayEmployeeCertificateReport.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmployeeCertificateReports.btnEmployeeCertificate_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowEmployeeCertificate(string reportTitle, string rptFileName)
    {


        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + ",@StaffId=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@CollegeNo=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",Puporse=" + txtforpurpose.Text.Trim();     

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

}