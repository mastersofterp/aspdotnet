//=================================================================================
// PROJECT NAME  : RFC Common Code                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : FeedBackReport.aspx                                               
// CREATION DATE : 04/06/2022                                                 
// CREATED BY    : Rishabh Bajirao                              
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
//using System;
//using System.Web.UI;
//using IITMS;
//using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using System.Web.UI.WebControls;
//using System.Data;
//using System.IO;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;

public partial class ACADEMIC_CommonFeedbackReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
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
                //this.CheckPageAuthorization();

                //fill dropdown


                if (Convert.ToInt32(Session["OrgId"]) == 10 || Convert.ToInt32(Session["OrgId"]) == 11 || Convert.ToInt32(Session["OrgId"]) == 12 || Convert.ToInt32(Session["OrgId"]) == 13 || Convert.ToInt32(Session["OrgId"]) == 14)
                {
                    if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1 AND MODE_ID=2", "FEEDBACK_NO");
                        objCommon.FillDropDownList(ddlFeedbackType, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1 AND MODE_ID=2", "FEEDBACK_NO");

                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1", "FEEDBACK_NO");
                        objCommon.FillDropDownList(ddlFeedbackType, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1", "FEEDBACK_NO");
                    }
                }
                else
                {
                    objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1", "FEEDBACK_NO");
                    objCommon.FillDropDownList(ddlFeedbackType, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1", "FEEDBACK_NO");
                }
                PopulateDropDown();

                if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    objCommon.FillDropDownList(ddlFeedbackReportType, "ACD_FEEDBACK_REPORT_TYPE", "FDID", "FEEBACKREPORTNAME", "ISNULL(ACTIVESTATUS,0)=1 AND FDID=2", "FDID");
                }
                else
                {
                    objCommon.FillDropDownList(ddlFeedbackReportType, "ACD_FEEDBACK_REPORT_TYPE", "FDID", "FEEBACKREPORTNAME", "ISNULL(ACTIVESTATUS,0)=1", "FDID");
                }

                FillDropDownList();
                //to clear all controls
                AllClear();
                if (Session["OrgId"].ToString() == "2")
                {
                    sectiondv.Visible = true;
                    //rfvsection.Validate = true;
                    rdotcpartfull.Items.Add(new ListItem("Survey Report", "3"));
                }
                else
                {
                    sectiondv.Visible = false;
                }

                if (Session["OrgId"].ToString() == "1")
                {
                    btnEvalutionReport.Visible = true;
                }
                if (Session["OrgId"].ToString() == "19")
                {
                    btnShow.Visible = true;
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }

    //function to fill all dropdown
    private void FillDropDownList()
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            objCommon.FillDropDownList(ddlserveycolscheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        }
        else if (Session["usertype"].ToString() == "8")
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SC.DEPTNO IN(" + Session["userdeptno"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            objCommon.FillDropDownList(ddlserveycolscheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SC.DEPTNO IN(" + Session["userdeptno"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
        }
        else
        {
            //objCommon.FillDropDownList(ddlClgname, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SC ON (SC.SCHEMENO = CT.SCHEMENO AND CT.COLLEGE_ID=SC.COLLEGE_ID)", "DISTINCT SC.COSCHNO", "SC.COL_SCHEME_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "SC.COSCHNO");
            FillFaculty();
        }

        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND ISNULL(ACTIVESTATUS,0)=1", "SEMESTERNO");
    }

    public void FillFaculty()
    {
        ddlClgname.Items.Clear();
        ddlClgname.Items.Add(new ListItem("Please Select", "0"));
        ddlserveycolscheme.Items.Clear();
        ddlserveycolscheme.Items.Add(new ListItem("Please Select", "0"));

        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        DataSet ds = new DataSet();
        SP_Name = "PKG_ACD_GET_SCHEME_FOR_FEEDBACK";
        SP_Parameters = "@P_UA_NO";
        Call_Values = "" + Convert.ToInt32(Session["userno"]);
        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
        {
            ddlClgname.DataSource = ds;
            ddlClgname.DataTextField = "COL_SCHEME_NAME";
            ddlClgname.DataValueField = "COSCHNO";
            ddlClgname.DataBind();
            ddlserveycolscheme.DataSource = ds;
            ddlserveycolscheme.DataTextField = "COL_SCHEME_NAME";
            ddlserveycolscheme.DataValueField = "COSCHNO";
            ddlserveycolscheme.DataBind();
        }
        else
        {
            ddlClgname.DataSource = null;
            ddlClgname.DataBind();
            ddlserveycolscheme.DataSource = null;
            ddlserveycolscheme.DataBind();
        }
    }
    //function to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CommonFeedbackReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CommonFeedbackReport.aspx");
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            //Fill Dropdown Session 
            CourseController objStud = new CourseController();
            string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
            DataSet dsCollegeSession = objStud.GetCollegeSession(1, college_IDs);
            ddlCollege.Items.Clear();
            ddlCollege.DataSource = dsCollegeSession;
            ddlCollege.DataValueField = "SESSIONNO";
            ddlCollege.DataTextField = "COLLEGE_SESSION";
            ddlCollege.DataBind();

            // rdbReport.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //load exam names according to scheme
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlSection.SelectedIndex > 0)
        //    {
        //        objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0", "FEEDBACK_NO");
        //        ddlFeedbackTyp.Focus();
        //    }
        //    else
        //    {
        //        ddlFeedbackTyp.Items.Clear();
        //        ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
    }


    //function to show report
    private void ShowReport(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        if (Convert.ToInt32(Session["OrgId"]) == 2)
        {
            url += "&param=" + param + "";
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 10 || Convert.ToInt32(Session["OrgId"]) == 11 || Convert.ToInt32(Session["OrgId"]) == 12 || Convert.ToInt32(Session["OrgId"]) == 13 || Convert.ToInt32(Session["OrgId"]) == 14)
        {
            if (Convert.ToInt32(Session["usertype"]) == 3)
            {
                url += "&param=" + param + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]);
            }
            else
            {
                url += "&param=" + param + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
            }
        }
        else
        {
            url += "&param=" + param + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
        }
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        ////To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeed, this.updFeed.GetType(), "controlJSScript", sb.ToString(), true);
    }

    private void AllClear()
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.SelectedIndex = 0;
        ddlFeedbackTyp.SelectedIndex = 0;
    }

    //to cancel report
    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        AllClear();
        Response.Redirect(Request.Url.ToString());
    }

    //to get faculty feedback report
    protected void btnFacultyFeedbackReport_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = objSFBC.GetSubjectFeedbackCommonData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
            string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_FEEDBACK_TYPENO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue) + "";

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                //if (Convert.ToInt32(Session["OrgId"]) == 2)
                //{
                //    ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_rcpit.rpt", param);
                //}
                if (Convert.ToInt32(Session["OrgId"]) == 2)
                {

                    if (ddlSection.SelectedValue == "0")
                    {
                        objCommon.DisplayUserMessage(updFeed, "Please Select Section", this.Page);
                        return;
                    }
                    int feedbackmode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue)));
                    if (feedbackmode == 3)
                    {
                        ShowReport("Student_FeedBack_Count", "SubjectFacultyExitFeedbackCommon_Crescent.rpt", param);
                    }
                    else
                    {
                        ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_Crescent.rpt", param);
                    }
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 10 || Convert.ToInt32(Session["OrgId"]) == 11 || Convert.ToInt32(Session["OrgId"]) == 12 || Convert.ToInt32(Session["OrgId"]) == 13 || Convert.ToInt32(Session["OrgId"]) == 14)
                {
                    if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        ShowReport("Faculty_FeedBack_Report_Percentage_Wise", "PRMITR_Percentage_Report_1_Faculty.rpt", param);
                    }
                    else
                    {
                        ShowReport("Faculty_FeedBack_Report_Percentage_Wise", "PRMITR_Percentage_Report_1.rpt", param);
                    }
                }
                else
                {
                    ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon.rpt", param);
                }
            }
            else
            {
                objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlFeedbackTyp.Items.Clear();
            ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlClgname.Focus();
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON (S.SEMESTERNO=R.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND ISNULL(PREV_STATUS,0)=0 and R.SESSIONNO=" + ddlSession.SelectedValue, "R.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlFeedbackTyp.Items.Clear();
            ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlFeedbackTyp.Items.Clear();
            ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {


    }
    protected void rdotcpartfull_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdotcpartfull.SelectedValue == "1")
        {
            dvFeedbackReport.Visible = true;
            dvFaculttyFeedback.Visible = false;
            dvallfeedback.Visible = false;
            //divrdofeedback.Visible = false;
            ddlFeedbackReportType.SelectedValue = "0";
            divSurveyReport.Visible = false;
        }
        if (rdotcpartfull.SelectedValue == "2")
        {
            dvallfeedback.Visible = true;
            dvFaculttyFeedback.Visible = false;
            //divrdofeedback.Visible = false;
            dvFeedbackReport.Visible = false;
            divSurveyReport.Visible = false;
        }
        if (rdotcpartfull.SelectedValue == "3")
        {
            divSurveyReport.Visible = true;
            dvFeedbackReport.Visible = false;
            dvFaculttyFeedback.Visible = false;
            dvallfeedback.Visible = false;
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = -1;
        ddlFeedbackType.SelectedIndex = 0;
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        string SessionNo = string.Empty;
        foreach (ListItem itm in ddlCollege.Items)
        {
            if (itm.Selected != true)
                continue;
            SessionNo += itm.Value + ",";
        }

        SessionNo = SessionNo.Remove(SessionNo.Length - 1);

        int degree = 0;
        int scheme = 0;
        int branch = 0;
        int semester = 0;
        int course = 0;
        int feedbacktype = Convert.ToInt32(ddlFeedbackType.SelectedValue);
        DataSet ds = objSFBC.GetAllFeedbackReportData(SessionNo, degree, scheme, branch, semester, course, feedbacktype);


        ds.Tables[0].TableName = "Feedback Submitted Details";
        ds.Tables[1].TableName = "Feedback Not Submitted Details";
        ds.Tables[2].TableName = "StatisticalReport";
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
            ds.Tables[0].Rows.Add("No Record Found");

        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
            ds.Tables[1].Rows.Add("No Record Found");

        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count <= 0)
            ds.Tables[2].Rows.Add("No Record Found");



        //if (ds.Tables.Count!=null)
        //{
        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                //Add System.Data.DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AllFeedbackReport.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

    }

    protected void btnCommentReport_Click(object sender, EventArgs e)
    {
        string SessionNo = string.Empty;

        SessionNo = ddlSession.SelectedValue;

        int degree = 0;
        int scheme = Convert.ToInt32(ddlClgname.SelectedValue);
        int branch = 0;
        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        int section = Convert.ToInt32(ddlSection.SelectedValue);
        int feedbacktype = Convert.ToInt32(ddlFeedbackType.SelectedValue);
        DataSet ds = objSFBC.GetAllFeedbackCommentsReport(SessionNo, degree, scheme, branch, semester, section, feedbacktype);


        ds.Tables[0].TableName = "Feedback Comments";
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
        {
            //ds.Tables[0].Rows[0]["QUESTIONNAME"] = "No Record Found";
            objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
            return;
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                //Add System.Data.DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FeedbackCommentReport.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void ddlFeedbackTyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlFeedbackTyp.SelectedValue) > 0)
        {
            int feedbackmode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue)));

            if (feedbackmode == 3 && Session["OrgId"].ToString() == "2")
            {
                btnCommentReport.Visible = true;
            }
            else
            {
                btnCommentReport.Visible = false;
            }
        }
        lvFacultyDetails.DataSource = null;
        lvFacultyDetails.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_FEEDBACK_TYPENO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue) + ""; //+",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]); //+"";
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_rcpit.rpt", param);
        }
    }
    protected void ddlFeedbackReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlFeedbackReportType.SelectedValue) > 0)
        {
            dvallfeedback.Visible = false;
            //dvFeedbackReport.Visible = false;
            if (ddlFeedbackReportType.SelectedValue == "2")
            {
                dvFaculttyFeedback.Visible = true;
                btnFacultyFeedbackReport.Visible = true;
                //btnFacultyFeedbackReportPercentageWise.Visible = false;
                btnHODFeedbackReport.Visible = false;
            }
            //if (ddlFeedbackReportType.SelectedValue == "2")
            //{
            //    dvFaculttyFeedback.Visible = true;
            //    btnFacultyFeedbackReport.Visible = false;
            //    btnFacultyFeedbackReportPercentageWise.Visible = true;
            //    btnHODFeedbackReport.Visible = false;
            //}
            if (ddlFeedbackReportType.SelectedValue == "1")
            {
                dvFaculttyFeedback.Visible = true;
                btnFacultyFeedbackReport.Visible = false;
                //btnFacultyFeedbackReportPercentageWise.Visible = false;
                btnHODFeedbackReport.Visible = true;
            }
        }
        else
        {
            dvFaculttyFeedback.Visible = false;
        }
    }




    /////// <summary>
    /////// added by Amit B. on date 07-Nov-2023
    /////// </summary>
    /////// <param name="sender"></param>
    /////// <param name="e"></param>
    ////protected void btnFacultyFeedbackReportPercentageWise_Click(object sender, EventArgs e)
    ////{
    ////    try
    ////    {

    ////        DataSet ds = objSFBC.GetSubjectFeedbackCommonData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
    ////        string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_FEEDBACK_TYPENO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue) + "";

    ////        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
    ////        {              
    ////            if (Convert.ToInt32(Session["OrgId"]) == 2)
    ////            {
    ////                {
    ////                    ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_Crescent.rpt", param);
    ////                }
    ////            }
    ////            else
    ////            {
    ////                ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon.rpt", param);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
    ////        }
    ////    }
    ////    catch
    ////    {
    ////        throw;
    ////    }
    ////}
    protected void btnHODFeedbackReport_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = objSFBC.GetSubjectFeedbackCommonData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
            string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_FEEDBACK_TYPENO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue) + "";

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 10 || Convert.ToInt32(Session["OrgId"]) == 11 || Convert.ToInt32(Session["OrgId"]) == 12 || Convert.ToInt32(Session["OrgId"]) == 13 || Convert.ToInt32(Session["OrgId"]) == 14)
                {
                    ShowhodfeedbackReport("HOD_FEEDBACK_REPORT", "PRMITR_HODFeedbackReport.rpt", param);
                }
            }
            else
            {
                objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    //function to show report
    private void ShowhodfeedbackReport(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        if (Convert.ToInt32(Session["OrgId"]) == 2)
        {
            url += "&param=" + param + "";
        }
        else
        {
            url += "&param=" + param + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
        }
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        ////To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeed, this.updFeed.GetType(), "controlJSScript", sb.ToString(), true);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        // DataSet ds;
        //string SessionNo = string.Empty;
        //SessionNo = ddlSession.SelectedValue;
        //int degree = 0;
        //int scheme = Convert.ToInt32(ddlClgname.SelectedValue);
        //int branch = 0;
        //int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        //int section = Convert.ToInt32(ddlSection.SelectedValue);
        //int feedbacktype = Convert.ToInt32(ddlFeedbackType.SelectedValue);

        //ds = objSFBC.GetSubjectFeedbackCommonData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        DataSet ds = new DataSet();
        SP_Name = "PKG_ACD_HOD_FEEDBACK_REPORT";
        SP_Parameters = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SECTIONNO,@P_FEEDBACK_TYPENO";
        Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ViewState["branchno"]) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlSection.SelectedValue) + "," + Convert.ToInt32(ddlFeedbackTyp.SelectedValue);
        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
        {
            lvFacultyDetails.DataSource = ds;
            lvFacultyDetails.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvFacultyDetails);//Set label 
        }
        else
        {
            lvFacultyDetails.DataSource = null;
            lvFacultyDetails.DataBind();
            objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
        }
    }


    protected void lnkFacultyName_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int UA_NO = Convert.ToInt32(lnk.ToolTip);
            int courseno = Convert.ToInt32(lnk.CommandArgument);
            HiddenField hdnsection = lnk.FindControl("hdnsection") as HiddenField;
            int sectionno = Convert.ToInt32(hdnsection.Value);
            string param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(sectionno) + ",@P_UA_NO=" + UA_NO + ",@P_COURSENO=" + courseno + ",@P_FEEDBACK_TYPENO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue) + "";
            ShowReportNew("FeedBack_Analysis_Report", "rptFeedbackAnalysisReport_PCEN.rpt", param);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CommonFeedbackReport.lnkFacultyName_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportNew(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //if (Convert.ToInt32(Session["OrgId"]) == 2)
        //{
        //url += "&param=" + param + "";
        //}
        //else
        //{
        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
        //}
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        ////To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeed, this.updFeed.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void ddlserveycolscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlserveycolscheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlserveycolscheme.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlServeySession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlServeySession.Focus();
            }
        }
        else
        {
            ddlServeySession.Items.Clear();
            ddlServeySession.Items.Add(new ListItem("Please Select", "0"));
            ddlServeySemester.Items.Clear();
            ddlServeySemester.Items.Add(new ListItem("Please Select", "0"));
            ddlServeyDBType.Items.Clear();
            ddlServeyDBType.Items.Add(new ListItem("Please Select", "0"));
            ddlserveySection.Items.Clear();
            ddlserveySection.Items.Add(new ListItem("Please Select", "0"));
            ddlServeySession.Focus();
        }
    }
    protected void ddlServeySemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServeySemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlserveySection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "SECTIONNO");
            ddlserveySection.Focus();
        }
        else
        {
            ddlServeyDBType.Items.Clear();
            ddlServeyDBType.Items.Add(new ListItem("Please Select", "0"));
            ddlserveySection.Items.Clear();
            ddlserveySection.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlserveySection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlserveySection.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlServeyDBType, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0 AND ISNULL(IS_ACTIVE,0)=1 AND MODE_ID=3", "FEEDBACK_NO");
                ddlServeyDBType.Focus();
            }
            else
            {
                ddlServeyDBType.Items.Clear();
                ddlServeyDBType.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlServeySession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServeySession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlServeySemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON (S.SEMESTERNO=R.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND ISNULL(PREV_STATUS,0)=0 and R.SESSIONNO=" + ddlServeySession.SelectedValue, "R.SEMESTERNO");
            ddlServeySemester.Focus();
        }
        else
        {
            ddlServeySemester.Items.Clear();
            ddlServeySemester.Items.Add(new ListItem("Please Select", "0"));
            ddlServeyDBType.Items.Clear();
            ddlServeyDBType.Items.Add(new ListItem("Please Select", "0"));
            ddlserveySection.Items.Clear();
            ddlserveySection.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnExportSurvey_Click(object sender, EventArgs e)
    {
        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        DataSet ds = new DataSet();
        SP_Name = "PKG_ACD_STUDENT_FEEDBACK_REPORT_CRESCENT";
        SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SECTIONNO,@P_FEEDBACK_TYPENO";
        Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlSection.SelectedValue) + "," + Convert.ToInt32(ddlFeedbackTyp.SelectedValue);
        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        ds.Tables[0].TableName = "SSS Servey Report";
        if (ds.Tables[0].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=SSS_Survey_Report" + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
        }
    }

}

