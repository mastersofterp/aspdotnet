//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : VALUER ENTRY                              
// CREATION DATE : 06 DEC 2012                                                          
// CREATED BY    :                                          
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_EXAMINATION_ValuerEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    Exam objExam = new Exam();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.FillDropdown();
                    this.FillCourseList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0", "BRANCHNO");
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.SelectedIndex = 0;
        FillCourseList();
    }

    private void FillCourse()
    {
        try
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C", "C.COURSENO", "(C.CCODE + ' - ' +C.COURSE_NAME)AS COURSENAME", "SUBID =1 AND MAXMARKS_E >0 AND (SCHEMENO=" + ddlScheme.SelectedValue + " or " + ddlScheme.SelectedValue + " =0 )AND (SEMESTERNO = " + ddlSemester.SelectedValue + "OR " + ddlSemester.SelectedValue + "= 0)", "COURSENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ddlDay_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCourseList();
        objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO >0 AND UA_TYPE = 3", "UA_NO");
    }

    private void FillCourseList()
    {
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE C ON  SR.COURSENO = C.COURSENO  INNER JOIN USER_ACC U ON (U.UA_NO = VALUER_UA_NO ) INNER JOIN ACD_SCHEME S ON (S.SCHEMENO = sr.SCHEMENO )", "DISTINCT C.COURSENO", "C.CCODE + C.COURSE_NAME COURSE_NAME,VALUER_UA_NO,UA_FULLNAME,S.SCHEMENO,SCHEMENAME", "SESSIONNO = " + ddlSession.SelectedValue + " AND (SR.SCHEMENO = " + ddlScheme.SelectedValue + " or " + ddlScheme.SelectedValue + "=0 )AND (C.COURSENO = " + ddlCourse.SelectedValue + " OR " + ddlCourse.SelectedValue + " =0) AND VALUER_UA_NO > 0", "COURSE_NAME");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
            }
            else
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.FillCourseList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus ret = (CustomStatus)objExamController.AssignValuer(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlScheme.SelectedValue), Convert.ToInt16(ddlCourse.SelectedValue), Convert.ToInt16(ddlFaculty.SelectedValue));
            if (ret != CustomStatus.TransactionFailed || ret != CustomStatus.Error)
            {
                objCommon.DisplayMessage(this.updValuer, "Record Saved Successfully", this.Page);
                FillCourseList();
            }
            else
                objCommon.DisplayMessage(this.updValuer, "Error in Saving Record...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillDepartment()
    {
        //fill department
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME");
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillCourse();
        ddlCourse.Focus();
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        FillCourse();
        if (ddlScheme.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (S.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SEMESTERNAME", "S.SEMESTERNO >0 AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue, "S.SEMESTERNO");
        }
    }
  
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO="+ddlDegree.SelectedValue , "BRANCHNO");
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH BR ON (CD.BRANCHNO=BR.BRANCHNO)", "CD.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
            ddlBranch.Focus();
        }
        else
        {
            objCommon.DisplayMessage("Please Select Degree!", this.Page);
            ddlDegree.Focus();
        }
    }
}

