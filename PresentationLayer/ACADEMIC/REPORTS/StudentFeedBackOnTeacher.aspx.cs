//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : StudentFeedBackOnTeacher.aspx                                               
// CREATION DATE : 17-02-2016                                                   
// CREATED BY    : MOHITMSK                              
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Data.SqlClient;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_StudentFeedBackReport : System.Web.UI.Page
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "ISNULL(MUL_COL_FLAG,0)", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 0;
                }
                FillDropDownList();

                if (Session["usertype"].ToString().Equals("3"))
                {
                    LoadFacultyPanel();
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

    private void ShowDetail()
    {
        int userno;
        StudentController objSC = new StudentController();
        userno = Convert.ToInt32(Session["userno"]);

        try
        {
            if (userno > 0)
            {
                SqlDataReader dtr = objSC.GetUserDetails(userno);

                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        if (dtr["college_id"] != null && !dtr["college_id"].ToString().Equals("0"))
                            ddlCollege.SelectedValue = dtr["college_id"].ToString();

                        if (dtr["UA_DEPTNO"] != null && !dtr["UA_DEPTNO"].ToString().Equals("0"))
                            ddlDept.SelectedValue = dtr["UA_DEPTNO"].ToString();

                        objCommon.FillDropDownList(ddlFaculty, "user_acc  a inner join ACD_DEPARTMENT b on (b.DEPTNO IN(SELECT VALUE FROM DBO.SPLIT(A.UA_DEPTNO,',')))  inner join ACD_COLLEGE_DEGREE_BRANCH cdb on(b.DEPTNO =cdb.DEPTNO) CROSS APPLY DBO.SPLIT(UA_DEPTNO,',') AS C", "DISTINCT A.UA_NO", "A.UA_FULLNAME", "C.VALUE = " + ddlDept.SelectedValue + " and cdb.COLLEGE_ID = " + ddlCollege.SelectedValue, "A.UA_NO");
                            ddlFaculty.SelectedValue = dtr["UA_NO"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackOnTeacher.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void LoadFacultyPanel()
    {
        ddlSession.Enabled = false;
        ddlCollege.Enabled = false;
        ddlDept.Enabled = false;
        ddlFaculty.Enabled = false;
        btnCancelReport.Visible = false;
        ShowDetail();

    }

    private void FillDropDownList()
    {
        if (Session["usertype"].ToString().Equals("3"))
        {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 and FLOCK=1", "SESSIONNO DESC");
        ddlSession.SelectedIndex = ddlSession.Items.Count > 1 ? 1 : 0;
        }
        else
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO DESC");
        }     
        objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO DESC");
        
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackOnTeacher.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackOnTeacher.aspx");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.DEPTNO = B.DEPTNO)", " distinct A.DEPTNO", "A.DEPTNAME", "B.COLLEGE_ID = " + ddlCollege.SelectedValue, "A.DEPTNO");
                //ddlDept.SelectedValue = "0";
                ddlDept.Focus();
            }
            else
            {
                ddlDept.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackReport.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
            {
                //ddlFaculty
                //ddlFaculty.SelectedValue = "0";
                    //objCommon.FillDropDownList(ddlFaculty, "user_acc A inner join ACD_DEPARTMENT b on (a.UA_DEPTNO = b.DEPTNO) inner join ACD_COLLEGE_DEGREE_BRANCH cdb on(b.DEPTNO =cdb.DEPTNO) ", " DISTINCT A.UA_NO", "A.UA_FULLNAME", "B.DEPTNO = " + ddlDept.SelectedValue + " and cdb.COLLEGE_ID = "+ ddlCollege.SelectedValue , "A.UA_NO");
                

                    objCommon.FillDropDownList(ddlFaculty, "user_acc  a inner join ACD_DEPARTMENT b on (b.DEPTNO IN(SELECT VALUE FROM DBO.SPLIT(A.UA_DEPTNO,',')))  inner join ACD_COLLEGE_DEGREE_BRANCH cdb on(b.DEPTNO =cdb.DEPTNO) CROSS APPLY DBO.SPLIT(UA_DEPTNO,',') AS C", "DISTINCT A.UA_NO", "A.UA_FULLNAME", "C.VALUE = " + ddlDept.SelectedValue + " and cdb.COLLEGE_ID = " + ddlCollege.SelectedValue, "A.UA_NO");
                    ddlFaculty.Focus();
            }
            else
            {
                ddlFaculty.SelectedValue = "0";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackReport.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
       // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;    
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_SESSIONNO="+ Convert.ToInt32(ddlSession.SelectedValue) +",@P_UA_NO="+ Convert.ToInt32(ddlFaculty.SelectedValue) ;
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";


        ////To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeed, this.updFeed.GetType(), "controlJSScript", sb.ToString(), true);


    }

    protected void btnFeedbackReport_Click(object sender, EventArgs e)
    {
        ShowReport("Student Feedback On Teacher", "rptStudentFeedbackOnTeacher.rpt");
    }

    private void AllClear()
    {
        ddlSession.SelectedValue = "0";
        ddlCollege.SelectedValue = "0";
        ddlDept.SelectedValue = "0";
        ddlFaculty.SelectedValue = "0";
    }

    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        AllClear();
    }

}
