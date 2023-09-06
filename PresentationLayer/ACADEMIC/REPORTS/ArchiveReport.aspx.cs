using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;
using System.Net;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_REPORTS_ArchiveReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
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

               
                if (Convert.ToInt32(Session["usertype"]) == 3)
                {

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 63 AND FLOCK IS NULL ", "SESSIONNO DESC");
                    
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        trFaculty.Visible = true;
                        objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "DISTINCT UA_NO", "UPPER(UA_FULLNAME)UA_FULLNAME", "UA_NO > 0 and UA_TYPE=3", "UA_NO");
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 63 AND FLOCK IS NULL ", "SESSIONNO DESC");//AND EXAMTYPE=1

                    }
                    else
                    {
                        objCommon.DisplayMessage("This Page Only For Faculty and Admin Login!!", this.Page);
                    }
                }

            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        
        divMsg.InnerHtml = string.Empty;
    }

    private void ShowReportFaculty(string reportTitle, string rptFileName)
    {
        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            try
            {
                //Shows report Roomwise
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                if (Convert.ToInt16(Session["usertype"]) == 3)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_user_name=" + Session["userfullname"] + ",@P_SEMESTERNO=0,@P_SCHEMENO=0,@P_SECTIONNO=0,@V_VERSION=2,@P_UA_NO=" + Convert.ToInt16(Session["userno"]);
                }

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";


            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_RoomwiseTimetable.ShowReportFaculty() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else
        {
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                try
                {
                    //Shows report Roomwise
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;
                    if (Convert.ToInt16(Session["usertype"]) == 1)
                    {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_user_name=" + ddlFaculty.SelectedItem + ",@P_SEMESTERNO=0,@P_SCHEMENO=0,@P_SECTIONNO=0,@V_VERSION=2,@P_UA_NO=" + Convert.ToInt16(ddlFaculty.SelectedValue);
                    }

                    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                    divMsg.InnerHtml += " </script>";


                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_RoomwiseTimetable.ShowReportFaculty() --> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server Unavailable.");
                }
            }
        }
    }


    private void ShowReport(string reportTitle, string rptFileName,string Courseno)
    {
        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_COURSENO=" + Courseno + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + "";

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else
        {
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                try
                {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;
                    url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + ",@P_COURSENO=" + Courseno + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + "";

                    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                    divMsg.InnerHtml += " </script>";
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server Unavailable.");
                }
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            if (ddlSession.SelectedIndex > 0)
            {
                tblButton.Visible = true;
                DataSet ds = objCourse.GetCourseOfUanoDetails(Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSession.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlCourse.Visible = true;
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                    //btnReport2.Enabled = true;
                    //btnCancel.Enabled = true;
                    CHK1.Visible = true;
                    CHK2.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage("No Record Found For this Session!!", this.Page);
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Session!!", this.Page);
            }
        }
        else
        {
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                if (ddlSession.SelectedIndex > 0)
                {
                    tblButton.Visible = true;
                    DataSet ds = objCourse.GetCourseOfUanoDetails(Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pnlCourse.Visible = true;
                        lvCourse.DataSource = ds;
                        lvCourse.DataBind();
                        //btnReport2.Enabled = true;
                        //btnCancel.Enabled = true;
                        CHK1.Visible = true;
                        CHK2.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Record Found For this Session!!", this.Page);
                        lvCourse.DataSource = null;
                        lvCourse.DataBind();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Session!!", this.Page);
                }

            }
        }
    }
    protected void btnReport2_Click(object sender, EventArgs e)
    {
        string courseno = string.Empty;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            CheckBox chk1 = item.FindControl("chk1") as CheckBox;


            if (chk1.Checked)
            {
                //courseno += chk1.ToolTip + ",";
                if (courseno.Length == 0) courseno = chk1.ToolTip.ToString();
                else
                    courseno += "$" + chk1.ToolTip.ToString();
            }
        }
        if (courseno == "")
        {
            objCommon.DisplayMessage("Please Select At least one Course!!", this.Page);
            return;
        }
        else
        {
            if (CHK1.Checked == true)
            {
                ShowReport("REPORT-WITH-GRADE", "rptArchiveReportwithGrade.rpt", courseno);
            }
            else
            {
                if (CHK2.Checked == true)
                {
                    ShowReport("REPORT", "rptArchiveReport.rpt", courseno);
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Atleast one Report Option!!", this.Page);
                    return;
                }
            }
        }
    }
    protected void btnTimeTable_Click(object sender, EventArgs e)
    {
        ShowReportFaculty("TimeTable_Report", "rptFacultyTimetable.rpt");
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
           ddlSession.SelectedIndex = 0;
           pnlCourse.Visible = true;
           lvCourse.DataSource = null;
           lvCourse.DataBind();
           //btnReport2.Enabled = true;
           //btnCancel.Enabled = true;
           CHK1.Visible = false;
           CHK2.Visible = false;
          
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnFeedbackCount_Click(object sender, EventArgs e)
    {

        string courseno = string.Empty;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            CheckBox chk1 = item.FindControl("chk1") as CheckBox;

            if (chk1.Checked)
            {
                //courseno += chk1.ToolTip + ",";
                if (courseno.Length == 0) courseno = chk1.ToolTip.ToString();
                else
                    courseno += "$" + chk1.ToolTip.ToString();
            }
        }
        if (courseno == "")
        {
            courseno = "0";
            //objCommon.DisplayMessage("Please Select At least one Course!!", this.Page);
            //return;
            ShowReportCount("Student_FeedBack_Count", "StudentFeedbackCount.rpt", courseno);
        }
        else
        {
            
                //ShowReport("REPORT-WITH-GRADE", "rptArchiveReportwithGrade.rpt", courseno);
                ShowReportCount("Student_FeedBack_Count", "StudentFeedbackCount.rpt", courseno);
        }
        
    }

    private void ShowReportCount(string reportTitle, string rptFileName,string courseno)
    {
        try
        {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + 1 + ",@P_BRANCHNO=" + 0 + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + 0 + ",@P_COURSENO=" + courseno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=" + Session["userno"].ToString() + ",@P_COPY=" + 2 + "";
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackReport.btnFeedback_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
