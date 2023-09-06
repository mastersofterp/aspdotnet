//=================================================================================
// PROJECT NAME  : NITGOA (RF-CAMPUS)                                                          
// MODULE NAME   : EXAMINATION                                     
// CREATION DATE : 25-MAR-2014
// CREATED BY    : UMESH GANORKAR
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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

public partial class ACADEMIC_REPORTS_StudentMarksReport : System.Web.UI.Page
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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");

            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        //Response.Write("Local Machine Host name is " + Dns.GetHostName() + "<br>");
        //string hostname = Dns.GetHostName();
        //IPHostEntry ipEntry = Dns.GetHostByName(hostname);
        //IPAddress[] addr = ipEntry.AddressList;
        //for (int i = 0; i < addr.Length; i++)
        //{
        //    Response.Write(string.Format("IP Address {0}: {1} ", i, addr[i].ToString()) + "<br>");
        //}
        string strHostName = System.Net.Dns.GetHostName();
        string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        // Label1.Text = GetIP4Address();
        divMsg.InnerHtml = string.Empty;
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
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));

        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;

        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + "", "SCHEMENAME");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON SR.SEMESTERNO = S.SEMESTERNO", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SEMESTERNO > 0" + "AND SESSIONNO=" + ddlSession.SelectedValue, "SR.SEMESTERNO");
    }
    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SCHEME S ON (SR.SCHEMENO = S.SCHEMENO) ", "DISTINCT SR.COURSENO", "(SR.CCODE+' - '+SR.COURSENAME) AS COURSENAME", "SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " ", "SR.COURSENO");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_EXAMNO=" + ddlTest.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16( ddlTest.SelectedValue ) == 8)
        {
        //ShowReport("Student_Marks", "rptStudentsExamMarks.rpt");
            ShowReport("Student_Marks", "rptStudentMarksReportWithAttendance.rpt");
        }
        else
        {
            ShowReport("Student_Marks_With_Attendance", "rptStudentMarksReport.rpt");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
