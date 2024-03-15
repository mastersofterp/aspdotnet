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

public partial class ACADEMIC_FacultyFeedbackReport : System.Web.UI.Page
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
                PopulateDropDownList();


                //objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0", "FEEDBACK_NO");
                //PopulateDropDown();

                //objCommon.FillDropDownList(ddlFeedbackType, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0", "FEEDBACK_NO");

                //FillDropDownList();
                //to clear all controls
                AllClear();

            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }


    #region Old Logic Code
    //function to fill all dropdown


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

        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Convert.ToInt32(2);

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
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
    }

    //to get faculty feedback report
    protected void btnFacultyFeedbackReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SessionID = string.Empty;
            int Faculty_UA_NO = 0;
            if (ddlSession1.SelectedValue != "" && ddlSession1.SelectedValue != "0")
            {
                SessionID = ddlSession1.SelectedValue;
                //var SessionNO = objCommon.LookUp("ACD_SESSION_MASTER", " TOP 1 SESSIONNO", "ISNULL(IS_ACTIVE,0) = 1 AND SESSIONID=" + ddlSession1.SelectedValue);
                //SessionNo = SessionNO;
                if (ddlFaculty.SelectedValue != "" && ddlFaculty.SelectedValue != "0")
                {
                    Faculty_UA_NO = Convert.ToInt32(ddlFaculty.SelectedValue);

                    ViewState["college_id"] = 5;

                    //objCommon.FillDropDownList(ddlFaculty, "USER_ACC UA WITH (NOLOCK) INNER JOIN ACD_ONLINE_FEEDBACK FDB  ON UA.UA_NO = FDB.UA_NO INNER JOIN ACD_SESSION_MASTER SM ON FDB.SESSIONNO =  SM.SESSIONNO", "DISTINCT UA.UA_NO ", "UA.UA_FULLNAME AS FULLNAME", "UA_TYPE=3 AND SM.SESSIONID=" + ddlSession1.SelectedValue, "UA_FULLNAME");

                    #region Get Fetch Faculty wise report details

                    //DataSet ds = objSFBC.GetFacultyWiseFeedbackData(Convert.ToInt32(SessionID), Faculty_UA_NO);
                    string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
                    DataSet ds = new DataSet();
                    SP_Name = "PKG_ACD_STUDENT_FACULTY_WISE_FEEDBACK_REPORT";
                    SP_Parameters = "@P_SESSIONID,@P_FUA_NO";
                    Call_Values = "" + Convert.ToInt32(SessionID) + "," + Faculty_UA_NO ;
                    ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    

                    string param = string.Empty;
                    if (Session["usertype"].ToString() == "1")
                    {
                        param = "@P_SESSIONID=" + SessionID + ",@P_FUA_NO=" + Faculty_UA_NO + "";
                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        int ua_no = Convert.ToInt32(Session["userno"]);
                        param = "@P_SESSIONID=" + SessionID + ",@P_FUA_NO=" + Faculty_UA_NO + "";
                        //param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(degreeNo) + ",@P_BRANCHNO=" + Convert.ToInt32(branchNo) + ",@P_SCHEMENO=" + Convert.ToInt32(schemeNo) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_FEEDBACK_TYPENO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue) + ",@P_UANO=" + ua_no + "";
                    }

                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {

                        int ua_nos = Convert.ToInt32(Session["userno"]);

                        //ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_Student.rpt", param);
                        if (Session["usertype"].ToString() == "1")
                        {
                            ShowReport("Student_FeedBack_Count", "SubjectFacultyWiseFeedback_Student.rpt", param);
                            //ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_Student.rpt", param);
                        }
                        else if (Session["usertype"].ToString() == "3")
                        {
                            ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon_Faculty.rpt", param);
                            //ShowReport("Student_FeedBack_Count", "SubjectFacultyFeedbackCommon.rpt", param);
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(updFeed, "Record Not Found.", this.Page);
                    }
                    #endregion

                }
                else
                {
                    objCommon.DisplayMessage(updFeed, "Please Select Faculty!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updFeed, "Please Select Session!", this.Page);
                return;
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
            dvFaculttyFeedback.Visible = true;
            divrdofeedback.Visible = false;
        }
        if (rdotcpartfull.SelectedValue == "2")
        {
            dvallfeedback.Visible = true;
            divrdofeedback.Visible = false;
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = -1;
        ddlFeedbackType.SelectedIndex = 0;

    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        string SessionNo = string.Empty;
        int Faculty_UA_NO = 0;
        //foreach (ListItem itm in ddlCollege.Items)
        //{
        //    if (itm.Selected != true)
        //        continue;
        //    SessionNo += itm.Value + ",";
        //}

        if (ddlSession1.SelectedValue != "" && ddlSession1.SelectedValue != "0")
        {
            var SessionNO = objCommon.LookUp("ACD_SESSION_MASTER", " TOP 1 SESSIONNO", "ISNULL(IS_ACTIVE,0) = 1 AND SESSIONID=" + ddlSession1.SelectedValue);
            SessionNo = SessionNO;
        }
        else
        {

        }

        //SessionNo = SessionNo.Remove(SessionNo.Length - 1);
        Faculty_UA_NO = Convert.ToInt32(ddlFaculty.SelectedValue);

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

    #endregion Old Logic Code


    #region Code Added by GopalM Ticket#51940 - 29/12/2023
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession1, "ACD_ONLINE_FEEDBACK  FDB  INNER JOIN  ACD_SESSION_MASTER SM ON FDB.SESSIONNO =  SM.SESSIONNO", "DISTINCT SM.SESSIONID", "SM.SESSION_NAME", "ISNULL(IS_ACTIVE,0) = 1", "SM.SESSIONID  DESC");
        //objCommon.FillDropDownList(ddlSession1, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(FLOCK,0) = 1 AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONID DESC");
    }

    protected void ddlSession1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession1.SelectedIndex > 0)
        {
            //var SessionNO = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "ISNULL(IS_ACTIVE,0) = 1 AND SESSIONID=" + ddlSession1.SelectedValue);
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC UA WITH (NOLOCK) INNER JOIN ACD_ONLINE_FEEDBACK FDB  ON UA.UA_NO = FDB.UA_NO INNER JOIN ACD_SESSION_MASTER SM ON FDB.SESSIONNO =  SM.SESSIONNO", "DISTINCT UA.UA_NO ", "UA.UA_FULLNAME AS FULLNAME", "UA_TYPE=3 AND SM.SESSIONID=" + ddlSession1.SelectedValue, "UA_FULLNAME");
            ddlFaculty.Focus();


        }
        else
        {
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
            //ddlFeedbackTyp.Items.Clear();
            //ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
            //ddlSection.Items.Clear();
            //ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int FacultyUA_No = Convert.ToInt32(ddlFaculty.SelectedValue);

    }
    #endregion
}

