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
using IITMS.NITPRM;

public partial class Itle_Assignment_Result_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Load

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                    Response.Redirect("~/Itle/selectCourse.aspx");


                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               
                FillDropdown();

            }
        }
    }

    #endregion

    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Assignment_Result_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Assignment_Result_Report.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlStudent, "dbo.ITLE_TESTRESULT TR, UAIMSACAD.ACAD_STUDENT S", "distinct TR.IDNO", "S.STUDNAME", "TR.IDNO=S.IDNO AND TR.IDNO>0", "TR.IDNO");
            objCommon.FillDropDownList(ddCourse, "ACD_COURSE C JOIN ACD_IASSIGNMASTER ASM ON (C.COURSENO=ASM.COURSENO)", "distinct C.COURSENO", "C.COURSE_NAME", "ASM.UA_NO=" + Session["userno"] + " AND ASM.SESSIONNO=" + Session["SessionNo"], "C.COURSE_NAME");


        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.FillDropdown->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            if (rbtnReportType.SelectedValue == "0")
            {
                if (ddCourse.SelectedValue != "0")
                {
                    string Script = string.Empty;
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,ITLE," + rptFileName;
                    url += "&param=@P_COURSENO=" + ddCourse.SelectedValue;
                    //url += "&param=username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_COURSENO=" + ddCourse.SelectedValue;
                    Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "Report", Script, true);
                }
                else
                {
                    objCommon.DisplayUserMessage(updpnl, "please select Course name !", this.Page);
                }


            }
            else if (rbtnReportType.SelectedValue == "1")
            {
                if (ddCourse.SelectedValue != "0" && ddlAssignment.SelectedValue != "0")
                {
                    string Script = string.Empty;
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,ITLE," + rptFileName;
                    url += "&param=@P_COURSENO=" + ddCourse.SelectedValue + ",@P_ASSIGNMENT_NO=" + ddlAssignment.SelectedValue;
                    //url += "&param=SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_COURSENO=" + ddCourse.SelectedValue + ",@P_ASSIGNMENT_NO=" + ddlAssignment.SelectedValue;
                    Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "Report", Script, true);
                }
                else
                {
                    objCommon.DisplayUserMessage(updpnl, "please select Course and Assignment name !", this.Page);
                }

            }
            else if (rbtnReportType.SelectedValue == "2")
            {
                if (ddCourse.SelectedValue != "0" && ddlStudent.SelectedValue != "0")
                {
                    string Script = string.Empty;
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,ITLE," + rptFileName;
                    url += "&param=@P_COURSENO=" + ddCourse.SelectedValue + ",@P_IDNO=" + ddlStudent.SelectedValue;
                    //url += "&param=SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",@P_COURSENO=" + ddCourse.SelectedValue + ",@P_IDNO=" + ddlStudent.SelectedValue;
                    Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "Report", Script, true);
                }
                else
                {
                    objCommon.DisplayUserMessage(updpnl, "please select Course and Student name !", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void ShowReport1(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",COURSENAME=" + Session["ICourseName"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void btnAllReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport1("Itle_Assignment_Result_For_All_Student", "Assignment_result_All_Student.rpt");
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "Itle_Assignment_Result.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnReportType.SelectedValue == "0")
            {
                ShowReport("Itle_Assignment_Result", "Assignment_result.rpt");
            }

            else if (rbtnReportType.SelectedValue == "1")
            {
                ShowReport("Itle_Assignment_Result", "Itle_Single_AssignmentReport.rpt");
            }
            
            else if(rbtnReportType.SelectedValue == "2")
            {
                ShowReport("Itle_Assignment_Result", "Itle_Student_Assignment_Report.rpt");
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "Itle_Assignment_Result.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void rbtnReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnReportType.SelectedValue == "0")
        {
            trAssignment.Visible = false;
            trStudent.Visible = false;
            ddlAssignment.SelectedValue = "0";
            ddlStudent.SelectedValue = "0";
        }

        if (rbtnReportType.SelectedValue == "1")
        {
            trAssignment.Visible = true;
            trStudent.Visible = false;
            ddCourse.SelectedValue = "0";
            ddlAssignment.SelectedValue = "0";
            ddlStudent.SelectedValue = "0";
        }

        if (rbtnReportType.SelectedValue == "2")
        {
            trAssignment.Visible = false;
            trStudent.Visible = true;
            ddCourse.SelectedValue = "0";
            ddlAssignment.SelectedValue = "0";
            // objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S INNER JOIN ACD_ISTUDASSIGNMENT SA ON(S.IDNO=SA.IDNO) INNER JOIN ACD_IASSIGNMASTER ASM ON(SA.AS_NO=ASM.AS_NO) INNER JOIN ACD_STUDENT_RESULT SR ON (ASM.COURSENO=SR.COURSENO)", "DISTINCT S.IDNO", "S.STUDNAME", "ASM.COURSENO=" + ddCourse.SelectedValue + "AND SA.AS_NO=" + ddlAssignment.SelectedValue + "AND SA.CHECKED = 1", "S.IDNO");


        }
    }

    protected void ddCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnReportType.SelectedValue == "1")
        {
            objCommon.FillDropDownList(ddlAssignment, "ACD_IASSIGNMASTER", "DISTINCT AS_NO", "SUBJECT", "COURSENO=" + ddCourse.SelectedValue, "AS_NO");
        }

        else if (rbtnReportType.SelectedValue == "2")
        {
            // Modified by  =  Sagar hiratkar 
            // Purpose      =  To bind roll no with the Name of Student
            // Date         =  28/03/2016
             // objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S INNER JOIN ACD_ISTUDASSIGNMENT SA ON(S.IDNO=SA.IDNO) INNER JOIN ACD_IASSIGNMASTER ASM ON(SA.AS_NO=ASM.AS_NO) INNER JOIN ACD_STUDENT_RESULT SR ON (ASM.COURSENO=SR.COURSENO)", "DISTINCT S.IDNO", "S.ROLLNO+'-'+S.STUDNAME", "ASM.COURSENO=" + ddCourse.SelectedValue + "AND SA.CHECKED = 1", "S.IDNO");

             objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S INNER JOIN ACD_ISTUDASSIGNMENT SA ON(S.IDNO=SA.IDNO) INNER JOIN ACD_IASSIGNMASTER ASM ON(SA.AS_NO=ASM.AS_NO) INNER JOIN ACD_STUDENT_RESULT SR ON (ASM.COURSENO=SR.COURSENO)", "DISTINCT S.IDNO", "ISNull(S.ROLLNO,0)+'-'+IsNull(S.STUDNAME,'')", "ASM.COURSENO=" + ddCourse.SelectedValue + "AND SA.CHECKED = 1", "S.IDNO");
        }

    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        rbtnReportType.SelectedValue = "0";
        ddCourse.ClearSelection();
        // ddCourse.SelectedIndex = 0;
        ddlAssignment.ClearSelection();
        //ddlAssignment.SelectedIndex = 0;
        ddlStudent.ClearSelection();
        //ddlStudent.SelectedIndex = 0;
        tr1.Visible = true;
        trAssignment.Visible = false;
        trStudent.Visible = false;
    }    

    #endregion    
}
