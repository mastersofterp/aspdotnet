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

public partial class ACADEMIC_EXAMINATION_IssueBundle : System.Web.UI.Page
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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

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
            
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND DEGREENO<6", "DEGREENO");
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


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.SelectedIndex = 0;
        FillCourseList();
    }
    
    private void FillCourse()
    {
        try
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C", "C.COURSENO", "(C.CCODE + ' - ' +C.COURSE_NAME)AS COURSENAME", "(SUBID =1 OR MAXMARKS_E >0) AND (SCHEMENO=" + ddlScheme.SelectedValue + " or " + ddlScheme.SelectedValue + " =0 )AND (SEMESTERNO = " + ddlSemester.SelectedValue + "OR " + ddlSemester.SelectedValue + "= 0)", "COURSENAME");
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
        //objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO >0 AND UA_TYPE = 3 AND UA_FULLNAME NOT IN ('-','--')", "UA_FULLNAME");
        objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");

        DataSet ds = null;
        ds = objExamController.GetPresntAbsUFM_Student(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblAbsent.Text = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0]["ABSENT_STUD_CNT"].ToString()) + Convert.ToInt32(ds.Tables[0].Rows[0]["ABSENT_STUD_CNT_EX"].ToString()));
            lblAppStudent.Text = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0]["APPEARED_STUD_CNT"].ToString()) + Convert.ToInt32(ds.Tables[0].Rows[0]["EX_APPEARED_STUD_CNT"].ToString()));
            lblPresent.Text = Convert.ToString(Convert.ToInt32(lblAppStudent.Text) - Convert.ToInt32(lblAbsent.Text));
            lblUFM.Text = ds.Tables[0].Rows[0]["UFM_STUD_CNT"].ToString();
        }

        objCommon.FillDropDownList(ddlStaff, "ACD_STAFF S INNER JOIN USER_ACC U ON(S.UA_NO=U.UA_NO) INNER JOIN ACD_STUDENT_RESULT R ON (R.UA_NO=U.UA_NO)", "distinct u.ua_no", "staff_name", "r.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " and r.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and r.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and r.courseno=" + Convert.ToInt32(ddlCourse.SelectedValue), "U.ua_no");
        //ddlStaff.SelectedIndex = 1;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_STAFF S INNER JOIN USER_ACC U ON(S.UA_NO=U.UA_NO) INNER JOIN ACD_STUDENT_RESULT R ON (R.UA_NO=U.UA_NO)", "COUNT(*)", "r.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " and r.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and r.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and r.courseno=" + Convert.ToInt32(ddlCourse.SelectedValue)));
        if (count > 0)
        {
            ddlStaff.SelectedIndex = 1;
        }
        else
        {
            ddlStaff.SelectedIndex = 0;
        }
        ddlStaff.Enabled = false;

        if (Convert.ToInt32(ddlCourse.SelectedValue) > 0)
        {
            //COADING FOR AUTOGENERATE THE BUNDLE NO

            string STR1 = objCommon.LookUp("ACD_EXAM_BUNDLELIST", "MAX(BUNDLEID)+1", "BUNDLEID IS NOT NULL");
            if (STR1 == "")
            {
                STR1 = "1000";
            }

            txtBundle.Text = STR1;


        }
        else
        {
            txtBundle.Text = string.Empty;
        }
    }
    protected void chkFaculty_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFaculty.Checked == true)
        {
            trFaculty.Visible = true;
            trDept.Visible = true;
        }
        else
        {
            trFaculty.Visible = false;
            trDept.Visible = false;
        }
    }
    private void FillCourseList()
    {
        try
        {
            DataSet ds;

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE C ON  SR.COURSENO = C.COURSENO  INNER JOIN ACD_EXAM_BUNDLELIST B ON (SR.SESSIONNO = B.SESSIONNO AND B.COURSENO = SR.COURSENO )INNER JOIN USER_ACC U ON (U.UA_NO = B.VALUER_UA_NO) ", " DISTINCT C.COURSENO", " C.COURSENO, C.CCODE + C.COURSE_NAME COURSE_NAME,SR.VALUER_UA_NO,UA_FULLNAME,BUNDLE,(CASE WHEN ISNULL(STATUS,0) = 0 THEN 'ISSUED' WHEN STATUS =1 THEN 'RECEIVED' END)STATUS", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND (SR.SCHEMENO = " + ddlScheme.SelectedValue + " or " + ddlScheme.SelectedValue + "=0 )AND (C.COURSENO = " + ddlCourse.SelectedValue + " OR " + ddlCourse.SelectedValue + " =0)", "COURSE_NAME");
          
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvBundle.DataSource = ds;
                lvBundle.DataBind();
            }
            else
            {
                lvBundle.DataSource = ds;
                lvBundle.DataBind();
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
            //check if previous entry is done
            string str = objCommon.LookUp("ACD_EXAM_BUNDLELIST", "BUNDLEID", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue);
            if (str == "")
            {
                int ua_no=0;
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
                string seatfrom = objCommon.LookUp("ACD_STUDENT_RESULT", "REGNO", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + " ORDER BY REGNO");
                string seatto = objCommon.LookUp("ACD_STUDENT_RESULT", "REGNO", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + " ORDER BY REGNO DESC");
                //if (ccode != "" && seatfrom != "" && seatto != "")
                if (ccode != "")
                {
                    if (ddlFaculty.SelectedValue != "0")
                        ua_no = Convert.ToInt16(ddlFaculty.SelectedValue);
                    else
                        ua_no = Convert.ToInt16(ddlStaff.SelectedValue);

                    CustomStatus ret = (CustomStatus)objExamController.IssueBundle(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlCourse.SelectedValue), ccode, Convert.ToInt16(txtSet.Text), 0,ua_no, Convert.ToInt16(ddlScheme.SelectedValue), seatfrom, seatto, txtBundle.Text.Trim());
                    if (ret != CustomStatus.TransactionFailed || ret != CustomStatus.Error)
                    {
                        objCommon.DisplayMessage("Bundle alloted to faculty Successfully", this.Page);
                        FillCourseList();
                        ShowReport("IssueBundle", "rptBundleIssueReport.rpt");

                    }
                    else
                        
                    objCommon.ConfirmMessage(updBundle,"Error in Saving Record...",this.Page);
                }
            }
            else
            {
                objCommon.ConfirmMessage(updBundle,"Bundle already alloted", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    //private void FillDepartment()
    //{
    //    //fill department
    //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME");
    //}

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 ", "SCHEMENO");
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

    protected void txtBundle_TextChanged(object sender, EventArgs e)
    {
        string str = objCommon.LookUp("ACD_EXAM_BUNDLELIST", "BUNDLEID", "BUNDLE = '"+ txtBundle.Text +"'");
        if (str != "")
        {
            objCommon.DisplayMessage(this.updBundle,"Bundle already exists!", this.Page);
            btnSubmit.Enabled = false;
        }
        else
            btnSubmit.Enabled = true;
    }

    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeptName.SelectedIndex == 0)
        {
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO >0 AND UA_TYPE = 3 AND UA_FULLNAME NOT IN ('-','--') AND (UA_DEPTNO >0 OR UA_DEPTNO=" + ddlDeptName.SelectedValue+")", "UA_FULLNAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO >0 AND UA_TYPE = 3 AND UA_FULLNAME NOT IN ('-','--') AND UA_DEPTNO=" + ddlDeptName.SelectedValue, "UA_FULLNAME");
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlFaculty.SelectedValue) > 0)
        {
            //COADING FOR AUTOGENERATE THE BUNDLE NO

            string STR1 = objCommon.LookUp("ACD_EXAM_BUNDLELIST", "MAX(BUNDLEID)+1", "BUNDLEID IS NOT NULL");
            if (STR1 == "")
            {
                STR1 = "1000";
            }
            
                txtBundle.Text = STR1;
           

        }
        else
        {
            txtBundle.Text = string.Empty;
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ( Convert.ToInt32(ddlDegree.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO="+ ddlDegree.SelectedValue, "DEGREENO");
        }
    }

    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranch.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO='"+ddlDegree.SelectedValue+"' AND BRANCHNO="+ddlBranch.SelectedValue, "SCHEMENO");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)+",@P_SCHEMENO="+Convert.ToInt32(ddlScheme.SelectedValue)+",@P_COURSENO="+Convert.ToInt32(ddlCourse.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBundle, this.updBundle.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("IssueBundle", "rptBundleIssueReport.rpt");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ////Update Isssue Bundle
        int bundle = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_BUNDLELIST", "BUNDLEID", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue));
        if (bundle > 1)
        {
            int ua_no = 0;
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

            if (ccode != "")
            {
                if (ddlFaculty.SelectedValue != "0")
                    ua_no = Convert.ToInt16(ddlFaculty.SelectedValue);
                else
                    ua_no = Convert.ToInt16(ddlStaff.SelectedValue);
                CustomStatus ret = (CustomStatus)objExamController.UpdateIssueBundle(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlCourse.SelectedValue), Convert.ToInt16(ddlScheme.SelectedValue), Convert.ToInt16(txtSet.Text), ua_no, bundle.ToString());

                if (ret != CustomStatus.TransactionFailed || ret != CustomStatus.Error)
                {
                    objCommon.DisplayMessage("Update Bundle Successfully", this.Page);
                    FillCourseList();
                    ShowReport("IssueBundle", "rptBundleIssueReport.rpt");

                }
                else

                    objCommon.ConfirmMessage(updBundle, "Error in Saving Record...", this.Page);
            }

        } 

    }

    private void ShowDetail(int sessionno, int bundle)
    {
        ExamController objSC = new ExamController();
        DataTableReader dtr = objSC.GetIssueBundle(sessionno, bundle.ToString());

        if (dtr != null)

            if (dtr.Read())
            {
                ddlSession.SelectedValue = dtr["SESSIONNO"] == DBNull.Value ? "0" : dtr["SESSIONNO"].ToString();
                string schemeno = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "SCHEMENO IN (SELECT SCHEMENO FROM ACD_COURSE WHERE COURSENO =" + dtr["COURSENO"].ToString() + ")");
                ddlDegree.SelectedValue = objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + Convert.ToInt32(schemeno));

                if (Convert.ToInt32(ddlDegree.SelectedValue) > 0)
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "DEGREENO");
                }

                ddlBranch.SelectedValue = objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + Convert.ToInt32(schemeno));

                if (Convert.ToInt32(ddlBranch.SelectedValue) > 0)
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO='" + ddlDegree.SelectedValue + "' AND BRANCHNO=" + ddlBranch.SelectedValue, "SCHEMENO");
                }
                ddlScheme.SelectedValue = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "SCHEMENO=" + Convert.ToInt32(schemeno));

                if (ddlScheme.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (S.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SEMESTERNAME", "S.SEMESTERNO >0 AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue, "S.SEMESTERNO");
                }

                ddlSemester.SelectedValue = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNO IN (SELECT SEMESTERNO FROM ACD_COURSE WHERE COURSENO =" + dtr["COURSENO"].ToString() + ")");
                this.FillCourse();
                ddlCourse.SelectedValue = dtr["COURSENO"] == DBNull.Value ? "0" : dtr["COURSENO"].ToString();
                objCommon.FillDropDownList(ddlStaff, "ACD_STAFF S INNER JOIN USER_ACC U ON(S.UA_NO=U.UA_NO) INNER JOIN ACD_STUDENT_RESULT R ON (R.UA_NO=U.UA_NO)", "distinct u.ua_no", "staff_name", "r.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " and r.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and r.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and r.courseno=" + Convert.ToInt32(ddlCourse.SelectedValue), "U.ua_no");

                int count = Convert.ToInt32(objCommon.LookUp("ACD_STAFF S INNER JOIN USER_ACC U ON(S.UA_NO=U.UA_NO) INNER JOIN ACD_STUDENT_RESULT R ON (R.UA_NO=U.UA_NO)", "COUNT(*)", "r.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " and r.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and r.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and r.courseno=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                if (count > 0)
                {
                    //ddlStaff.SelectedValue = dtr["VALUER_UA_NO"] == DBNull.Value ? "0" : dtr["VALUER_UA_NO"].ToString();
                    ddlStaff.SelectedIndex = 1;
                }
                else
                {
                    ddlStaff.SelectedIndex = 0;
                }

                ddlStaff.Enabled = false;
                txtBundle.Text = dtr["BUNDLEID"].ToString();
                txtSet.Text = dtr["SECTIONNO"].ToString();
                objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");
            }
            else
            {
                objCommon.DisplayMessage(this.updBundle, "Bundle does not exists!", this.Page);
            }
        dtr.Close();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int bundle = int.Parse(btnEdit.CommandArgument);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            ShowDetail(sessionno, bundle);
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            trBundle.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_schememaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}

