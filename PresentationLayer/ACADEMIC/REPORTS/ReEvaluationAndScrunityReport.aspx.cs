using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Data;
using System.Data.SqlClient;


public partial class ACADEMIC_EXAMINATION_ReEvoluationAndScrunityReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSR = new StudentRegistration();
    string section = string.Empty;
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Fill DropDown List
                PopulateDropDownList();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentStrength.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
        }
    }

    //Fill DropdownList
    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN COLLEGE
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            // FILL DROPDOWN Session
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");
            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
           
            //Fill Dropdown Degree

            section = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_SECTION", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + section + ")", "DEGREENAME");
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "BRANCHNO>0", "SHORTNAME");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentStrength.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedValue == "0")
        {
            //Fill Dropdown Degree
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
        }
        else
        {
            section = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_SECTION", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB WITH (NOLOCK) ON CDB.DEGREENO=A.DEGREENO", "DISTINCT A.DEGREENO", "A.DEGREENAME", "B.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "AND CDB.UGPGOT IN (" + section + ")", "A.degreeno");
            ddlDegree.Focus();
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
        ddlBranch.Focus();
    }
    protected void btnRevalReport_Click(object sender, EventArgs e)
    {
        ShowReportPayment("Revaluation Registered Payment List ", "rptRevaluationPaymentStatus.rpt");
    }

    private void ShowReportPayment(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_TYPE="+Convert.ToInt32(ddlreportfor.SelectedValue);
                }
                else
                {
                    ddlBranch.SelectedValue = "0";
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TYPE=" + Convert.ToInt32(ddlreportfor.SelectedValue);
                }
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_rptRevaluationAndScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("Revoluation And Scrunity", "rptRevaluationAndScrutiny.rpt");
        if (ddlcourse.SelectedIndex > 0)
        {
            ShowReport("Revoluation CourseWise", "rptRevaluation_COURSEWISE.rpt", 1);
        }
        else
        {
            ShowReport("Revoluation Registered Student List", "rptRevaluationAndScrutiny.rpt", 2);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int param)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (param == 1)
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlcourse.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_TYPE="+Convert.ToInt32(ddlreportfor.SelectedValue);// +",@P_AAPTYPE=" + ddlType.SelectedValue.Trim();
                }
                else
                {
                    ddlBranch.SelectedValue = "0";
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlcourse.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TYPE=" + Convert.ToInt32(ddlreportfor.SelectedValue);// +",@P_AAPTYPE=" + ddlType.SelectedValue.Trim();
                }
            }
            else
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TYPE=" + Convert.ToInt32(ddlreportfor.SelectedValue);// +",@P_AAPTYPE=" + ddlType.SelectedValue.Trim();
                }
                else
                {
                    ddlBranch.SelectedValue = "0";
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TYPE=" + Convert.ToInt32(ddlreportfor.SelectedValue);// +",@P_AAPTYPE=" + ddlType.SelectedValue.Trim();
                }
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_rptRevaluationAndScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Cancel();
    }
    private void Cancel()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        //ddlType.SelectedIndex = 0;
        
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            string attachment = string.Empty;
            if (ddlcourse.SelectedIndex > 0)
            {
                if (ddlBranch.SelectedIndex < 0)
                {
                    ddlBranch.SelectedValue = "0";
                }
                DataSet dsfee = objSR.GetRevalRegisteredStudentLists_CourseWise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlcourse.SelectedValue),Convert.ToInt32(ddlreportfor.SelectedValue));

                if (dsfee.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = dsfee;
                    GV.DataBind();
                    Response.Clear();
                    Response.Buffer = true;
                    if (Convert.ToInt32(ddlreportfor.SelectedValue) == 1)
                    {
                        attachment = "attachment; filename=RedressalRegistered.xls";
                    }
                    else if (Convert.ToInt32(ddlreportfor.SelectedValue) == 2)
                    {
                        attachment = "attachment; filename=PaperSeeingRegistered.xls";
                    }
                     
                   
                    Response.AddHeader("content-disposition", attachment);
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                if (ddlBranch.SelectedIndex < 0)
                {
                    ddlBranch.SelectedValue = "0";
                }
                DataSet dsfee = objSR.GetRevalRegisteredStudentLists(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlreportfor.SelectedValue));

                if (dsfee.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = dsfee;
                    GV.DataBind();
                    Response.Clear();
                    Response.Buffer = true;

                    if (Convert.ToInt32(ddlreportfor.SelectedValue) == 1)
                    {
                        attachment = "attachment; filename=RedressalRegistered.xls";
                    }
                    else if (Convert.ToInt32(ddlreportfor.SelectedValue) == 2)
                    {
                        attachment = "attachment; filename=PaperSeeingRegistered.xls";
                    }
                   
                    Response.AddHeader("content-disposition", attachment);
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.xls";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_UserMeritList.btnexport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
       
    }


    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() ;
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_REPORTS_rptRevaluationAndScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT_HIST SR WITH (NOLOCK) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON(SC.SCHEMENO=SR.SCHEMENO) INNER JOIN  ACD_SEMESTER S WITH (NOLOCK) ON(S.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND S.SEMESTERNO > 0 AND SC.BRANCHNO = " + ddlBranch.SelectedValue + "", "SEMESTERNO");
            }
            else
            {
                // FILL DROPDOWN SEMESTER
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlcourse, " ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON (C.SCHEMENO = SC.SCHEMENO) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (C.SCHEMENO =SR.SCHEMENO AND C.COURSENO =SR.COURSENO ) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (S.IDNO =SR.IDNO )", "DISTINCT C.COURSENO", " C.CCODE +' '+  C.COURSE_NAME  AS COURSE_NAME", "SC.DEGREENO =" + ddlDegree.SelectedValue + " AND (SC.BRANCHNO=" + ddlBranch.SelectedValue + " OR  " + ddlBranch.SelectedValue + "=0) AND C.SEMESTERNO= " + ddlSemester.SelectedValue + " AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "C.COURSENO");// AND C.SCHEMENO= " + ddlScheme.SelectedValue + "
    }
}