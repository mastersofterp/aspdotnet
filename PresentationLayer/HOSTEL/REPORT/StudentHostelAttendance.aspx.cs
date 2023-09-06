//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : STUDENT ATTENDANCE REPORT                                            
// CREATION DATE : 04-DEC-2010                                                          
// CREATED BY    : GAURAV SONI                                                      
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;


public partial class Academic_StudentAttendanceReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = "";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentHostelAttendance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentHostelAttendance.aspx");
        }
    } 
    #endregion

    #region Form Events
    protected void btnSubmitFirst_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_Attendance_Report", "StudentAttendanceReportDays1-7.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentAttendanceReport.btnSubmitFirst_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentAttendanceReport.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmitSecond_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_Attendance_Report", "StudentAttendanceReportDays8-14.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentAttendanceReport.btnSubmitSecond_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitThird_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_Attendance_Report", "StudentAttendanceReportDays15-21.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentAttendanceReport.btnSubmitThird_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitFourth_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_Attendance_Report", "StudentAttendanceReportDays22-28.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentAttendanceReport.btnSubmitFourth_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnSubmitFifth_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_Attendance_Report", "StudentAttendanceReportDays29-31.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentAttendanceReport.btnSubmitFifth_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    #endregion

    #region Private Methods
    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN HOSTEL SESSION NO.
            objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0", "HOSTEL_SESSION_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            //url += "&param=username=" + Session["username"].ToString() + ",monthname=" + ddlMonth.SelectedValue + " - " + (DateTime.Now.Year).ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=username=" + Session["username"].ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",MONTH="+ddlMonth.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    } 
    #endregion

   
}
