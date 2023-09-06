//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STUDENT ATTENDANCE REPORT                                            
// CREATION DATE : 21-OCT-2010                                                          
// CREATED BY    : MANGESH N. MOHATKAR                                                 
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.IO;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;

public partial class Academic_AttendanceReport : System.Web.UI.Page
{
    #region Page Evnets
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentAttendanceController objAtt = new StudentAttendanceController();
    string _UAIMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    lblCurSession.Text = Session["currentsession"].ToString();
                    lblUserNo.Text = Session["userno"].ToString();
                    PopulateDropDownList();
                    btnConsolidate.Visible = false;
                    btnStudentConsolidate.Visible = false;
                    btnFilledAtt.Visible = false;
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }

    #endregion

    #region Form Methods

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnConsolidate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedValue == "2" && ddlBatch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select Batch", this.Page);
                return;
            }
            ShowReport("Attendance_Report", "rptAttendanceConsolidatedReport.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnStudentConsolidate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedValue == "2" && ddlBatch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select Batch", this.Page);
                return;
            }
            ShowReportByFaculty("Student_Attendance_Report", "rptAttendanceConsolidatedReportByFaculty.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillSemester();
        //FillStudent(Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(lblUserNo.Text), Convert.ToInt32(lblCurSession.Text));
        //FillFaculty(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(lblUserNo.Text), Convert.ToInt32(ddlCourse.SelectedValue),Convert.ToInt32(ddlSubjectType.SelectedValue));

        if (Convert.ToInt32(Session["usertype"]) == 1)
        {
            objCommon.FillDropDownList("Please Select", ddlFaculty, "ACD_COURSE_TEACHER A WITH (NOLOCK),USER_ACC B WITH (NOLOCK)", "DISTINCT B.UA_NO", "B.UA_FULLNAME", "A.UA_NO = B.UA_NO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(ddlTerm.SelectedValue) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND A.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.UA_FULLNAME");
        }
        else if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC WITH (NOLOCK)", "UA_NO", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "");
            ddlFaculty.SelectedIndex = 1;
            ddlFaculty.Enabled = false;
        }

    }


    private void FillSemester()
    {
        try
        {
            // objCommon.FillDropDownList(ddlsemester,"ACD_COURSE_TEACHER A,ACD_SEMESTER B","DISTINCT B.SEMESTERNO","B.SEMESTERNAME","A.SEMESTERNO = B.SEMESTERNO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.SCHEMENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.DEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text),"B.SEMESTERNO");
            //objCommon.FillDropDownList(ddlsemester, "ACD_COURSE_TEACHER A WITH (NOLOCK),ACD_SEMESTER B WITH (NOLOCK)", "DISTINCT B.SEMESTERNO", "B.SEMESTERNAME", "A.SEMESTERNO = B.SEMESTERNO", "B.SEMESTERNO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDayWise_Click(object sender, EventArgs e)
    {
        string mmyy = Convert.ToInt32(ddlMonth.SelectedValue) + "/" + ddlYear.SelectedItem.Text;
        ShowDayWiseReport(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(lblUserNo.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), mmyy);
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDepartment(Convert.ToInt32(lblCurSession.Text), Convert.ToInt32(lblUserNo.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
        FillFacultyList();
    }

    private void FillFacultyList()
    {
        try
        {
            if (ddlSubjectType.SelectedValue == "1")
            {
                objCommon.FillDropDownList("Please Select", ddlFaculty, "ACD_COURSE_TEACHER A WITH (NOLOCK),USER_ACC B WITH (NOLOCK)", "DISTINCT B.UA_NO", "B.UA_FULLNAME", "A.UA_NO = B.UA_NO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.SCHEMENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND A.SECTIONNO =" + Convert.ToInt32(ddlDivision.SelectedValue), "B.UA_FULLNAME");
            }
            else if (ddlSubjectType.SelectedValue == "2")
            {
                objCommon.FillDropDownList("Please Select", ddlFaculty, "ACD_COURSE_TEACHER A WITH (NOLOCK),USER_ACC B WITH (NOLOCK)", "DISTINCT B.UA_NO", "B.UA_FULLNAME", "A.UA_NO = B.UA_NO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.SCHEMENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND A.SECTIONNO =" + Convert.ToInt32(ddlDivision.SelectedValue) + " AND A.BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue), "B.UA_FULLNAME");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillCourse(Convert.ToInt32(lblCurSession.Text),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(Session["usertype"].ToString()), Convert.ToInt32(lblUserNo.Text), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue));
        objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (S.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO= " + ddlTerm.SelectedValue + " AND SCHEMENO = " + ddlDept.SelectedValue + "", "SR.SEMESTERNO");
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubjectType.SelectedIndex > 0)
        {
            //ddlBatch.Enabled = true;
            //objCommon.FillDropDownList(ddlBatch, "ACD_BATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue), "BATCHNO");
            // FillDepartment(Convert.ToInt32(lblCurSession.Text),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(Session["usertype"].ToString()), Convert.ToInt32(lblUserNo.Text),Convert.ToInt32(ddlSubjectType.SelectedValue));
            if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "4")
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT COURSENO", "(CCODE + ' - ' + COURSENAME + ' ( ' + CONVERT(nvarchar,SCHEMENO)+ ' ) ') COURSE_NAME,  SCHEMENO ", "SUBID = " + ddlSubjectType.SelectedValue + " AND SEMESTERNO = " + ddlsemester.SelectedValue + " AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO =" + Convert.ToInt32(ddlTerm.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SCHEMENO, COURSE_NAME");
            }
            else if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3"))
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT COURSENO", "(CCODE + ' - ' + COURSENAME + ' ( ' + CONVERT(nvarchar,SCHEMENO)+ ' ) ') COURSE_NAME,  SCHEMENO ", "SUBID = " + ddlSubjectType.SelectedValue + " AND SEMESTERNO = " + ddlsemester.SelectedValue + " AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO =" + Convert.ToInt32(ddlTerm.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SCHEMENO, COURSE_NAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (C.COURSENO = CT.COURSENO) ", "DISTINCT CT.COURSENO", "(C.CCODE +' - '+C.COURSE_NAME) AS COURSENAME", "CT.SUBID = " + ddlSubjectType.SelectedValue + " AND CT.SEMESTERNO = " + ddlsemester.SelectedValue + " AND CT.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND C.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND CT.SESSIONNO =" + Convert.ToInt32(ddlTerm.SelectedValue) + " AND (CT.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "CT.COURSENO");
            }
        }
        else
        {
            // ddlBatch.Enabled = false;
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Private Methods

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string monthyear = ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["username"].ToString() + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(lblUserNo.Text) + ",@P_MONTHYEAR=" + monthyear + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(lblCurSession.Text) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportByFaculty(string reportTitle, string rptFileName)
    {
        try
        {
            string monthyear = Convert.ToInt32(ddlMonth.SelectedValue) + "/" + ddlYear.SelectedItem.Text;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["username"].ToString() + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(lblUserNo.Text) + ",@P_IDNO=" + Convert.ToInt32(ddlStudentConsolidate.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(lblCurSession.Text) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",username=" + Session["username"].ToString() + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(lblUserNo.Text) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) ;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    //FillDepartment(int sessionNo,int degreeno,int uatype, int uaNo, int subId )
    private void FillDepartment()
    {
        try
        {
            //SQLHelper objSQLHelper = new SQLHelper(_UAIMS);
            //SqlParameter[] objParams = null;
            //objParams = new SqlParameter[5];
            //objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
            //objParams[1] = new SqlParameter("@P_DEGREE", degreeno);
            //objParams[2] = new SqlParameter("@P_UATYPE", uatype);
            //objParams[3] = new SqlParameter("@P_UA_NO", uaNo);
            //objParams[4] = new SqlParameter("@P_SUBID", subId);

            //DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DEPT_BY_UA_NO", objParams);

            //ddlDept.DataSource = ds;
            //ddlDept.Items.Clear();
            //ddlDept.Items.Add("Please Select");
            //ddlDept.SelectedItem.Value = "0";
            //ddlDept.DataValueField = ds.Tables[0].Columns["DEPTNO"].ToString();
            //ddlDept.DataTextField = ds.Tables[0].Columns["DEPTNAME"].ToString();
            //ddlDept.DataBind();
            if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "4")
            {
                // objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
                objCommon.FillDropDownList(ddlDept, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEME_NAME", "SCHEMENO= " + ddlDegree.SelectedValue + "", "SCHEMENO");
            }
            else if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3"))
            {
                //objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"] + ") ", "B.BRANCHNO");
                objCommon.FillDropDownList(ddlDept, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEME_NAME", "SCHEMENO= " + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"] + " ", "SCHEMENO");
            }
            else
            {
                // objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO=" + Session["userdeptno"].ToString(), "B.LONGNAME");
                objCommon.FillDropDownList(ddlDept, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEME_NAME", "SCHEMENO= " + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"] + " ", "SCHEMENO");
            }

            ddlDept.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void FillCourse(int sessionNo, int degreeno, int usertype, int uaNo, int deptno, int subId)
    {
        try
        {
            //SQLHelper objSQLHelper = new SQLHelper(_UAIMS);
            //SqlParameter[] objParams = null;
            //objParams = new SqlParameter[6];
            //objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
            //objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
            //objParams[2] = new SqlParameter("@P_USERTYPE", usertype);
            //objParams[3] = new SqlParameter("@P_UA_NO", uaNo);
            //objParams[4] = new SqlParameter("@P_DEPTNO", deptno);
            //objParams[5] = new SqlParameter("@P_SUBID", subId);

            //DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSE_BY_UA_NO", objParams);

            //ddlCourse.DataSource = ds;
            //ddlCourse.Items.Clear();
            //ddlCourse.Items.Add("Please Select");
            //ddlCourse.SelectedItem.Value = "0";
            //ddlCourse.DataValueField = ds.Tables[0].Columns["COURSENO"].ToString();
            //ddlCourse.DataTextField = ds.Tables[0].Columns["COURSENAME"].ToString();
            //ddlCourse.DataBind();
            //Added by amit b.
            CourseController objCC = new CourseController();
            DataSet dsCourse = objCC.GetCourseForCourseAllotment_new(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            if (dsCourse.Tables.Count > 0)
            {
                ddlCourse.DataValueField = dsCourse.Tables[0].Columns[0].ColumnName;
                ddlCourse.DataTextField = dsCourse.Tables[0].Columns[1].ColumnName;
                ddlCourse.DataSource = dsCourse;
                ddlCourse.DataBind();
                //objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            }
            else
            {
                ddlCourse.DataSource = null;
                ddlCourse.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void FillStudent(int subId, int batchNo, int schemeNo, int courseNo, int uaNo, int sessionNo)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_SUBID", subId);
            objParams[1] = new SqlParameter("@P_BATCHNO", batchNo);
            objParams[2] = new SqlParameter("@P_SCHEMENO", schemeNo);
            objParams[3] = new SqlParameter("@P_COURSENO", courseNo);
            objParams[4] = new SqlParameter("@P_UA_NO", uaNo);
            objParams[5] = new SqlParameter("@P_SESSIONNO", sessionNo);

            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RET_ATTENDANCE_STUDENT", objParams);

            ddlStudentConsolidate.DataSource = ds;
            ddlStudentConsolidate.Items.Clear();
            ddlStudentConsolidate.Items.Add("Please Select");
            ddlStudentConsolidate.SelectedItem.Value = "0";

            ddlStudentConsolidate.DataValueField = ds.Tables[0].Columns["IDNO"].ToString();
            ddlStudentConsolidate.DataTextField = ds.Tables[0].Columns["STUDNAME"].ToString();
            ddlStudentConsolidate.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void FillFaculty(int schemeNo, int uaNo, int courseNo, int subId)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
            objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
            objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
            objParams[3] = new SqlParameter("@P_SUBID", subId);

            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_RET_ATTENDANCE_FAC_DEC", objParams);
            ddlFaculty.DataSource = ds;
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
            ddlFaculty.SelectedItem.Value = "0";
            ddlFaculty.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
            ddlFaculty.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
            ddlFaculty.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlTerm, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlTerm, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO=" + Session["currentsession"].ToString(), "SESSION_NAME DESC");
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("Please Select"));
            for (int i = DateTime.Now.Year; i > (DateTime.Now.Year - 2); i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString()));
            }
            if (Session["usertype"].ToString() == "1")
            {
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            }
            else if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "0")
            {
                int colno = Convert.ToInt32(Session["college_nos"]);
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME DESC");
                //objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH C WITH (NOLOCK) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON(C.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "COLLEGE_ID=" + colno + " AND D.DEGREENO > 0", "D.DEGREENAME");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
            }
            else
            {
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDayWiseReport(int schemeNo, int courseNo, int uaNo, int subId, string mmyy)
    {
        try
        {
            StudentAttendanceController objAC = new StudentAttendanceController();

            DataSet ds = objAC.GetDayWiseAttendanceData(schemeNo, courseNo, uaNo, subId, mmyy);
            lvDayWise.DataSource = ds;
            lvDayWise.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList("Please Select", ddlBatch, "ACD_COURSE_TEACHER A,ACD_BATCH B", "DISTINCT B.BATCHNO", "B.BATCHNAME", "A.BATCHNO= B.BATCHNO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.schemeno =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.DEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue), "B.BATCHNAME");
            // objCommon.FillDropDownList(ddlBatch, "ACD_BATCH", "DISTINCT BATCHNO", "BATCHNAME", "subid=" + ddlSubjectType.SelectedIndex, "");
            //FillFacultyList();
            ddlSubjectType.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
        //try
        //{
        //    //objCommon.FillDropDownList("Please Select", ddlDivision, "ACD_COURSE_TEACHER A,ACD_DIVISION B", "DISTINCT B.DIVISIONNO", "B.DIVISIONNAME", "A.SECTIONNO = B.DIVISIONNO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.DEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue)+" AND ADTEACHER= " + Session["userno"].ToString() , "B.DIVISIONNAME");

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.ddlsemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList("Please Select", ddlBatch, "ACD_COURSE_TEACHER A WITH (NOLOCK),ACD_BATCH B WITH (NOLOCK)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "A.BATCHNO= B.BATCHNO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.DEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + " AND A.TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND A.SECTIONNO =" + Convert.ToInt32(ddlDivision.SelectedValue), "B.BATCHNAME");
            FillFacultyList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedValue == "1")
                objCommon.FillDropDownList("Please Select", ddlStudentConsolidate, "ACD_STUDENT_RESULT A WITH (NOLOCK),ACD_STUDENT B WITH (NOLOCK)", "DISTINCT B.IDNO", "B.STUDNAME ,B.REGNO", "A.IDNO= B.IDNO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND B.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND A.SECTIONNO =" + Convert.ToInt32(ddlDivision.SelectedValue) + " AND ADTEACHER_TH =" + Convert.ToInt32(ddlFaculty.SelectedValue), "B.REGNO");
            else if (ddlSubjectType.SelectedValue == "2")
                objCommon.FillDropDownList("Please Select", ddlStudentConsolidate, "ACD_STUDENT_RESULT A WITH (NOLOCK),ACD_STUDENT B WITH (NOLOCK)", "DISTINCT B.IDNO", "B.STUDNAME ,B.REGNO", "A.IDNO= B.IDNO AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND B.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND A.SESSIONNO =" + Convert.ToInt32(lblCurSession.Text) + " AND A.SEMESTERNO =" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND A.SECTIONNO =" + Convert.ToInt32(ddlDivision.SelectedValue) + " AND ADTEACHER_PR =" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND A.BATCHNO =" + Convert.ToInt32(ddlBatch.SelectedValue), "B.REGNO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnUpToDate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedIndex <= 0 || ddlsemester.SelectedIndex <= 0 || txtAttDate.Text.Trim() == string.Empty || txtRecDate.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(updtime, "Please Select Scheme/SubjectType/Semester/From date/To Date", this.Page);
                return;
            }
            else
            {
                DataSet ds = objAtt.StudAttUpToDateReport(Convert.ToInt32(ddlTerm.SelectedValue),
            Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlsemester.SelectedValue),
            Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue),
            Convert.ToInt32(ddlFaculty.SelectedValue), txtAttDate.Text, txtRecDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.ShowReportUpToDate("DATE_Attendance_Report", "rptAttUpToDateReport.rpt");
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updtime, "No Record Found.", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(updtime, "No Record Found.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void ShowReportUpToDate(string reportTitle, string rptFileName)
    {
        try
        {
            //char ch = '/';
            //string[] Fromdate = txtAttDate.Text.Split(ch);
            //string[] Todate = txtRecDate.Text.Split(ch);
            //string fdate = Fromdate[1] + "/" + Fromdate[0] + "/" + Fromdate[2];
            //string tdate = Todate[1] + "/" + Todate[0] + "/" + Todate[2];

            //Added BY SUMIT ON 27-JAN-2020 
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(lblUserNo.Text) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlTerm.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_FROMDATE=" + fdate + ",@P_TODATE=" + tdate; // +",@P_PERIODNO=" + ddlPeriod.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_DIVISIONNO=" + Convert.ToInt32(ddlDivision.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlTerm.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_FROMDATE=" + txtAttDate.Text + ",@P_TODATE=" + txtRecDate.Text; // +",@P_PERIODNO=" + ddlPeriod.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //To open report with update panel
            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEndTerm_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedIndex <= 0 || ddlSubjectType.SelectedIndex <= 0 || ddlsemester.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage("Please Select Degree/Department/SubjectType/Semester", this.Page);
                return;
            }
            else
            {
                this.ShowReportTermEnd("TermEnd_Attendance_Report", "rptTermEndAttendanceReport.rpt");

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReportTermEnd(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(lblUserNo.Text) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlTerm.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnFilledAtt_Click(object sender, EventArgs e)
    {
        if (txtAttDate.Text.Trim() == string.Empty || txtRecDate.Text.Trim() == string.Empty)
        {
            ShowMessage("Please Select From Date and To Date");
            return;
        }
        else
            ShowDateWiseReport();
    }
    //Show Filled Attendance Report
    private void ShowDateWiseReport()
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ccode = objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

            string ContentType = string.Empty;
            char ch = '/';
            string[] Fromdate = txtAttDate.Text.Split(ch);
            string[] Todate = txtRecDate.Text.Split(ch);
            string fdate = Fromdate[1] + "/" + Fromdate[0] + "/" + Fromdate[2];
            string tdate = Todate[1] + "/" + Todate[0] + "/" + Todate[2];
            StudentAttendanceController objAC = new StudentAttendanceController();
            DataSet ds = objAC.GetDateWiseData(Convert.ToInt32(ddlTerm.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(lblUserNo.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), fdate, tdate, Convert.ToInt32(ddlBatch.SelectedValue));
            //DataSet ds = objAC.GetDateWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), txtFromDate.Text, txtTodate.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + ccode + "_" + txtAttDate.Text.Trim() + "_" + txtRecDate.Text.Trim() + ".xls";
                //string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtAttDate.Text.Trim() + "_" + txtRecDate.Text.Trim() + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                //Response.ContentType = "application/pdf";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

                //PDF
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                //StringWriter sw = new StringWriter();

                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                //GVDayWiseAtt.AllowPaging = false;
                //GVDayWiseAtt.DataBind();
                //GVDayWiseAtt.RenderControl(hw);

                //StringReader sr = new StringReader(sw.ToString());
                //Document pdfDoc = new Document(PageSize.LETTER_LANDSCAPE, 10f, 10f, 10f, 0f);

                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.Write(pdfDoc);
                //Response.End();    
            }
            else
            {
                objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillDepartment();
        if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "4")
        {
            // objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            objCommon.FillDropDownList(ddlDept, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO= " + ddlDegree.SelectedValue + "", "SCHEMENO");
        }
        else if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3"))
        {
            //objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"] + ") ", "B.BRANCHNO");
            objCommon.FillDropDownList(ddlDept, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO= " + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"] + " ", "SCHEMENO");
        }
        else
        {
            // objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO=" + Session["userdeptno"].ToString(), "B.LONGNAME");
            // objCommon.FillDropDownList(ddlDept, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO= " + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"] + " ", "SCHEMENO");
            //objCommon.FillDropDownList(ddlDept, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SCHEME SM ON (SM.SCHEMENO=SR.SCHEMENO)", "DISTINCT SR.SCHEMENO", "SCHEMENAME", "UA_NO= " + Session["userno"] + "and DEGREENO = " + ddlDegree.SelectedValue + " ", "SCHEMENO"); // first line
            objCommon.FillDropDownList(ddlDept, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SCHEME SM WITH (NOLOCK) ON (SM.SCHEMENO=SR.SCHEMENO)", "DISTINCT SR.SCHEMENO", "SCHEMENAME", "(UA_NO= " + Session["userno"] + "OR UA_NO_PRAC= " + Session["userno"] + "OR AD_TEACHER_TH= " + Session["userno"] + "OR AD_TEACHER_PR= " + Session["userno"] + ") and DEGREENO = " + ddlDegree.SelectedValue + " ", "SCHEMENO"); //new line
        }

        ddlDept.Focus();
    }
    protected void ddlDegScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegScheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlDegScheme.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

            }
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlDegScheme.Focus();
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
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                objCommon.FillDropDownList(ddlTerm, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                ddlTerm.Focus();
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
            }
        }
        else
        {
            ddlClgname.Focus();
            ddlTerm.Items.Clear();
            ddlTerm.Items.Add(new ListItem("Please Select", "0"));
            ddlsemester.Items.Clear();
            ddlsemester.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.SelectedIndex = 0;
        }
    }
    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTerm.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (S.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO= " + ddlTerm.SelectedValue + " AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "", "SR.SEMESTERNO");
            ddlsemester.Focus();
        }
        else
        {
            ddlsemester.Items.Clear();
            ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}
