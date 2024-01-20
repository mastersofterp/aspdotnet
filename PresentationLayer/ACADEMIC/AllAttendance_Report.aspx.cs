using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_AllAttendance_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentAttendanceController Attd = new StudentAttendanceController();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    DateTime StartDate, EndDate;

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
             
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                    PopulateDropDown();

                    
                }
                
            }           
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    #region Private Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page= AllAttendance_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AllAttendance_Report.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
           
            objCommon.FillDropDownList(ddlReportName, "ACD_REPORT_TYPE_MASTER", "REPORT_ID", "REPORT_NAME", "ISNULL(ACTIVE_STATUS,0)=1", "REPORT_ID");

                      
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    ////bind DEGREE NAME SESSION_PNAME  in drop down list. 
    protected void PopulateDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3", "SESSIONNO DESC");
            ddlSession.SelectedIndex = 0;
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            if (Session["usertype"].ToString() != "1")
            {             
                if (ddlReportName.SelectedItem.Text == "")
                {
                    objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING CM INNER JOIN ACD_COURSE_TEACHER CT ON (CM.COLLEGE_ID = CT.COLLEGE_ID)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "UA_NO = " + Session["userno"] + " AND ISNULL(CT.CANCEL,0)=0 ", "");
                }
                else
                {
                    //objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "");
                    objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                    string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0) = 1 AND D.DEGREENO>0", "D.DEGREENO");
                }
            }
            else
            {
                //objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID >0", "");
                objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND ISNULL(D.ACTIVESTATUS,0) = 1", "D.DEGREENO");
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //bind semester name in drop down list
    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        try
        {
            DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);
            ddlsemester.Items.Clear();
            ddlsemester.Items.Add("Please Select");
            ddlsemester.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsemester.DataSource = ds;
                ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlsemester.DataBind();
                ddlsemester.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }


    private void Validation()
    {
        if (ddlReportName.SelectedItem.Text == "SUBJECT WISE REPORT" || ddlReportName.SelectedItem.Text == "SUBJECT WISE DETAILS" || ddlReportName.SelectedItem.Text == "ATTENDANCE DETAILS")
        {          
            rfvInstitute.Visible = true;
            rfvCollege.Visible = false;
            rfvSession.Visible = true;
            rfvddlCollegeMulti.Visible = false;
            rfvSem.Visible = true;
            rfvSubjectType.Visible = false;
            rfvSection.Visible = true;
            rvfddlDepartment.Visible = false;
            rfvddlFaculty.Visible = false;
            rfvtxtFromDate.Visible = true;
            rfvtxtTodate.Visible = true;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;
        }

        if (ddlReportName.SelectedItem.Text == "CUMULATIVE ATTENDANCE" || ddlReportName.SelectedItem.Text == "ATT.REPORT WITH OD" || ddlReportName.SelectedItem.Text == "FACULTY INCOMPLETE ATTENDANCE")
        {          
            rfvInstitute.Visible = false;
            rfvCollege.Visible = false;
            rfvSession.Visible = true;
            rfvddlCollegeMulti.Visible = false;
            rfvSem.Visible = true;
            rfvSubjectType.Visible = false;
            rfvSection.Visible = true;
            rvfddlDepartment.Visible = false;
            rfvddlFaculty.Visible = false;
            rfvtxtFromDate.Visible = true;
            rfvtxtTodate.Visible = true;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;

        }

        if (ddlReportName.SelectedItem.Text == "CONSOLIDATED ATTENDANCE REPORT FORMAT-I (EXCEL)" || ddlReportName.SelectedItem.Text == "CONSOLIDATED ATTENDANCE REPORT FORMAT-II(EXCEL)")
        {
            rfvInstitute.Visible = true;
            rfvCollege.Visible = false;
            rfvSession.Visible = true;
            rfvddlCollegeMulti.Visible = false;
            rfvSem.Visible = false;
            rfvSubjectType.Visible = false;
            rfvSection.Visible = false;
            rvfddlDepartment.Visible = false;
            rfvddlFaculty.Visible = false;
            rfvtxtFromDate.Visible = false;
            rfvtxtTodate.Visible = false;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;
        }

        if (ddlReportName.SelectedItem.Text == "ATTENDANCE PENDING REPORT")
        {
            rfvInstitute.Visible = true;
            rfvCollege.Visible = false;
            rfvSession.Visible = true;
            rfvddlCollegeMulti.Visible = false;
            rfvSem.Visible = false;
            rfvSubjectType.Visible = false;
            rfvSection.Visible = false;
            rvfddlDepartment.Visible = false;
            rfvddlFaculty.Visible = false;
            rfvtxtFromDate.Visible = true;
            rfvtxtTodate.Visible = true;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;
        }

        if (ddlReportName.SelectedItem.Text == "SHOW ATTENDANCE DETAILS")
        {
            rfvInstitute.Visible = true;
            rfvCollege.Visible = false;
            rfvSession.Visible = true;
            rfvddlCollegeMulti.Visible = false;
            rfvSem.Visible = true;
            rfvSubjectType.Visible = false;
            rfvSection.Visible = false;
            rvfddlDepartment.Visible = false;
            rfvddlFaculty.Visible = false;
            rfvtxtFromDate.Visible = true;
            rfvtxtTodate.Visible = true;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;
        }

        if (ddlReportName.SelectedItem.Text == "COURSEWISE REPORT (EXCEL)" || ddlReportName.SelectedItem.Text == "SHOW STUDENT-WISE ATTENDANCE")
        {          
            rfvInstitute.Visible = false;
            rfvCollege.Visible = false;
            rfvSession.Visible = true;
             rfvddlCollegeMulti.Visible = true;
             rfvSem.Visible = false;
             rfvSubjectType.Visible = false;
             rfvSection.Visible = false;
             rvfddlDepartment.Visible = false;
             rfvddlFaculty.Visible = false;
            rfvtxtFromDate.Visible = true;
            rfvtxtTodate.Visible = true;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;
        }

        if (ddlReportName.SelectedItem.Text == "FACULTY WISE ATTENDANCE REPORT" || ddlReportName.SelectedItem.Text == "FACULTY WISE ATTENDANCE REPORT SHOW")
        {
            rfvInstitute.Visible = false;
            rfvCollege.Visible = true;
            rfvSession.Visible = true;
            rfvddlCollegeMulti.Visible = false;
            rfvSem.Visible = false;
            rfvSubjectType.Visible = false;
            rfvSection.Visible = false;
            rvfddlDepartment.Visible = true;
            rfvddlFaculty.Visible = true;
            rfvtxtFromDate.Visible = true;
            rfvtxtTodate.Visible = true;
            rfvPercentage.Visible = false;
            rfvtxtPercentageFrom.Visible = false;
            rfvPercentageTo.Visible = false;
        }
    }

    protected void BindDataFilters(int reportid)
    {
        try
        {
             DataSet ds = Attd.GetDetailsFromReportMaster(reportid);

             if (ds != null && ds.Tables.Count > 0)
             {
                 if (ddlReportName.SelectedItem.Text == "COURSEWISE REPORT (EXCEL)")
                 {
                     this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
                 }  

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_FROMDT_RFV"].ToString()) == 1)
                 {
                     divDateDetails.Visible = false;
                 }
                 else
                 {
                     divDateDetails.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_COLLEGE_SCHEME_STATUS"].ToString()) == 1)
                 {
                     divddlInstitute.Visible = true;
                 }
                 else 
                 {
                     divddlInstitute.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SESSION_STATUS"].ToString()) == 1)
                 {
                     divddlSession.Visible = true;
                 }
                 else
                 {
                     divddlSession.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SEMESTER_STATUS"].ToString()) == 1)
                 {
                     divddlSem.Visible = true;
                 }
                 else
                 {
                     divddlSem.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_COURSE_TYPE_STATUS"].ToString()) == 1)
                 {
                     divddlCourseType.Visible = true;
                 }
                 else
                 {
                     divddlCourseType.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_COURSE_STATUS"].ToString()) == 1)
                 {
                     divddlSubject.Visible = true;
                 }
                 else
                 {
                     divddlSubject.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SECTION_STATUS"].ToString()) == 1)
                 {
                     divddlSection.Visible = true;
                 }
                 else
                 {
                     divddlSection.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_FROM_DATE_STATUS"].ToString()) == 1)
                 {
                    divtxtFromDate.Visible = true;
                 }
                 else
                 {
                     divtxtFromDate.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_TO_DATE_STATUS"].ToString()) == 1)
                 {
                     divtxtTodate.Visible = true;
                 }
                 else
                 {
                     divtxtTodate.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_OPERATOR_STATUS"].ToString()) == 1)
                 {
                     divddlOperator.Visible = true;
                 }
                 else 
                 {
                     divddlOperator.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_PERCENTAGE_STATUS"].ToString()) == 1)
                 {
                     divtxtPercentage.Visible = true;
                 }
                 else
                 {
                     divtxtPercentage.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SUBJECT_TYPE_STATUS"].ToString()) == 1)
                 {
                     divddlSubjectType.Visible = true;
                 }
                 else
                 {
                     divddlSubjectType.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_THEORY_PRACTICAL_TUTORIAL_STATUS"].ToString()) == 1)
                 {
                     divddltheorypractical.Visible = true;
                 }
                 else
                 { 
                     divddltheorypractical.Visible=false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_BETWEEN_PERCENTAGE_STATUS"].ToString()) == 1)
                 {
                     divrdoPerBtn.Visible = true;
                 }
                 else
                 {
                     divrdoPerBtn.Visible = false;
                     divtxtPercentageFrom.Visible = false;
                     divtxtPercentageTo.Visible = false;
                 }
      
                 if(Convert.ToInt32(ds.Tables[0].Rows[0]["IS_MULTIPLE_COLLEGE_STATUS"].ToString())==1)
                 {
                     divddlCollege.Visible = true;
                 }  
                 else
                 {
                      divddlCollege.Visible = false;
                 }  
                
     
                 if(Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SCHOOL_INSTITUTE_STATUS"].ToString())==1)
                 {
                     divddlSchool.Visible = true;
                 } 
                 else
                 {
                     divddlSchool.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_DEPARTMENT_STATUS"].ToString()) == 1)
                 {
                     divddlDepartment.Visible = true;
                 }
                 else
                 {
                     divddlDepartment.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_FACULTY_STATUS"].ToString()) == 1)
                 {
                     divddlFaculty.Visible = true;
                 }
                 else
                 {
                     divddlFaculty.Visible = false;
                 }

                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["ISEXCEL_REPORT"].ToString()) == 1)
                 {
                     btnShow.Visible = false;
                     btnReport.Visible=false;
                     btnExcelReport.Visible = true;
                 }
                 else
                 {
                     btnShow.Visible = false;
                     btnReport.Visible = true; ;
                     btnExcelReport.Visible = false;
                 }


                 if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SHOW_REPORT"].ToString()) == 1)
                 {
                     btnShow.Visible = true;
                     btnReport.Visible=false;
                     btnExcelReport.Visible = false;
                 }
                 else
                 {
                     btnShow.Visible = false;
                     lvAttStatus.DataSource = null;
                     lvAttStatus.DataBind();
                     divFacultyAttendanceStatus.Visible = false;
                     lvAttendence.DataSource = null;
                     lvAttendence.DataBind();
                      divShowAttendanceDetails.Visible=false;
                     lvByPercent.DataSource = null;
                     lvByPercent.DataBind();
                     divShowAttendanceDetailsByPercent.Visible = false;
                     lvStudAttendance.DataSource = null;
                     lvStudAttendance.DataBind();
                     divStudentAttendanceDetails.Visible=false;

                 }



             }
        }
        catch
        {
 
        }
    }

    private void ClearControls()
    {
        ddlInstitute.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = -1;
        ddlSem.SelectedIndex = 0;
        ddlCourseType.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddltheorypractical.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        ddlOperator.SelectedIndex = 0;
        txtPercentage.Text = string.Empty;
        txtPercentageFrom.Text = string.Empty;
        txtPercentageTo.Text = string.Empty;
    }

    #endregion

    #region BindListview

    private void BindFacultyWiseStatus()
    {
        int facultyno = 0;
        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            facultyno = Convert.ToInt32(Session["userno"]);
        }
        else
        {
            facultyno = Convert.ToInt32(ddlFaculty.SelectedValue);
        }

        DataSet ds = objAttC.GetFacultywiseAttendanceStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), facultyno, Convert.ToInt32(ViewState["college_id"]));

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            divFacultyAttendanceStatus.Visible = true;
            lvAttStatus.DataSource = ds;
            lvAttStatus.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAttStatus);//Set label
            lvAttStatus.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(updSection, "No Record Found !", this.Page);
            lvAttStatus.Visible = false;
        }
    }

    private void BindStudentAttendanceDetails()
    {
        DataSet ds = Attd.GetStudAttDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), txtFromDate.Text, txtTodate.Text);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudAttendance.DataSource = ds;
                lvStudAttendance.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudAttendance);//Set label -
                divlvStudentHeading.Visible = true;
                // this.ShowReport("Attendance_Report", "rptAttSubjectWiseReport.rpt");
            }
            else
            {
                objCommon.DisplayUserMessage(updSection, "No Record Found.", this.Page);
                divlvStudentHeading.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updSection, "No Record Found.", this.Page);
            divlvStudentHeading.Visible = false;
        }
    }

    private void BindListView_Operator(int selector)
    {
        try
        {
            StudentAttendanceController objSAC = new StudentAttendanceController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance();
            objAtt.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAtt.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAtt.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAtt.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAtt.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAtt.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objAtt.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objAtt.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);

            if (txtTodate.Text != string.Empty && txtFromDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updSection, "End date should be greater than Start date", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
            string FromDate = txtFromDate.Text;
            string ToDate = txtTodate.Text;
            objAtt.Conditions = ddlOperator.SelectedValue.ToString();
            objAtt.Percentage = Convert.ToString(txtPercentage.Text);
            DataSet ds = objSAC.GetAttendanceSelectorWise(objAtt, selector, FromDate, ToDate);

            pnlByPercent.Visible = false;

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
                //pnlAttendence.Visible = false;
                //pnlByPercent.Visible = false;
                objCommon.DisplayMessage(updSection, "No Records Found.", this.Page);
                return;
            }
            else
            {
                pnlAttendence.Visible = true;
                lvAttendence.DataSource = ds;
                lvAttendence.DataBind();
            }



        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListByPer(int selector)
    {
        try
        {
            StudentAttendanceController objSAC = new StudentAttendanceController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance();
            objAtt.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAtt.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAtt.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAtt.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAtt.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            if (txtTodate.Text != string.Empty && txtFromDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updSection, "End date should be greater than Start date", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
       string FromDate = txtFromDate.Text;
           string ToDate =txtTodate.Text;
            if (txtPercentageTo.Text != string.Empty && txtPercentageFrom.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPercentageTo.Text) <= Convert.ToInt32(txtPercentageFrom.Text))
                {
                    objCommon.DisplayMessage(this.updSection, "To Percentage should be greater than From percentage", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
            objAtt.PercentageFrom = Convert.ToString(txtPercentageFrom.Text);
            objAtt.PercentageTo = Convert.ToString(txtPercentageTo.Text);
            objAtt.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objAtt.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);
            objAtt.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            DataSet dsPer = objSAC.GetAttendanceSelectorWise(objAtt, selector, FromDate, ToDate);
            pnlAttendence.Visible = false;
            pnlByPercent.Visible = true;
            if (dsPer.Tables[0].Rows.Count <= 0)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
                objCommon.DisplayMessage(updSection, "No Records Found.", this.Page);
                return;
            }
            else
            {
                lvByPercent.DataSource = dsPer;
                lvByPercent.DataBind();
            }
            if (txtPercentageFrom.Text == string.Empty)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
            }

            else if (txtPercentageTo.Text == string.Empty)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindForAll(int selector)
    {
        try
        {

            IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance();
            objAtt.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAtt.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAtt.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAtt.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAtt.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            if (txtTodate.Text != string.Empty && txtFromDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updSection, "End date should be greater than Start date", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
            string FromDate =txtFromDate.Text;
            string ToDate = txtTodate.Text;
            objAtt.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objAtt.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);
            objAtt.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);

            DataSet dsAll = Attd.GetAttendanceSelectorWise(objAtt, selector, FromDate, ToDate);
            pnlAttendence.Visible = false;
            pnlByPercent.Visible = true;
            if (dsAll.Tables[0].Rows.Count <= 0)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
                objCommon.DisplayMessage(updSection, "No Records Found.", this.Page);
                return;
            }
            else
            {
                lvByPercent.DataSource = dsAll;
                lvByPercent.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Excel Reports Methods

    private void ExcelReportShow()
    {
        string collegenos = string.Empty;
        foreach (ListItem itm in ddlCollege.Items)
        {
            if (itm.Selected != true)
                continue;
            collegenos += itm.Value + ",";
        }

        collegenos = collegenos.Remove(collegenos.Length - 1);
        int sessionid = 0;
        sessionid = Convert.ToInt32(ddlSession.SelectedValue);
        string FromDate = txtFromDate.Text;
        string ToDate = txtTodate.Text;
        DataSet ds = objAttC.GetAllStudentWiseAttendanceExcelReport(sessionid, collegenos, FromDate,ToDate);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].TableName = "Student-Wise Attendance Details";
            ds.Tables[1].TableName = "Student-Wise Attendance Summary";

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                ds.Tables[0].Rows.Add("No Record Found");

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                ds.Tables[1].Rows.Add("No Record Found");

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                    wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StudentWiseAttendanceSummary.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    private void ExcelReportConsolidatedFormat_II()
    {
        try
        {
            //Added by Nikhil Vinod Lambe on 11032020 to pass perFrom,perTo and selector
            int selector = 0;
            if (rdoPerBtn.Checked)
            {
                selector = 0;
                txtPercentage.Text = "0";
            }
            else if (rdoOpr.Checked)
            {
                selector = 1;
            }
            else
            {
                selector = 2;
                txtPercentage.Text = "0";
            }
            ////ShowReportinFormate(rdoReportType.SelectedValue, "rptConsolidatedAttendanceNew.rpt");
            GridView GV = new GridView();

            DateTime FromDate = txtFromDate.Text == string.Empty ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = txtTodate.Text == string.Empty ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(txtTodate.Text);
            DataSet ds = objCommon.GetAttendanceData1(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), FromDate, ToDate, Convert.ToInt32(ddlSection.SelectedValue), ddlOperator.SelectedValue, Convert.ToDouble(txtPercentage.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), txtPercentageFrom.Text.ToString(), txtPercentageTo.Text.ToString(), Convert.ToInt32(selector));

            //**************************************************************************************
            //GetAttendanceForAll(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector)
            //ds.Tables[0].Columns.Remove("ROLLNO");
            //ds.Tables[0].Columns.Remove("IDNO");
            //ds.Tables[0].Columns.Remove("SCHEMENO");
            //ds.Tables[0].Columns.Remove("SEMESTERNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=Attendance_Sem-" + ddlSem.SelectedItem + ".xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayUserMessage(updSection, "No Record Found.", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ExcelReportConsolidatedFormat_I()
    {
        try
        {
            //Added by Nikhil Vinod Lambe on 11032020 to pass perFrom,perTo and selector
            int selector = 0;
            if (rdoPerBtn.Checked)
            {
                selector = 0;
                txtPercentage.Text = "0";
            }
            else if (rdoOpr.Checked)
            {
                selector = 1;
            }
            else
            {
                selector = 2;
                txtPercentage.Text = "0";
            }
            ////ShowReportinFormate(rdoReportType.SelectedValue, "rptConsolidatedAttendanceNew.rpt");
            GridView GV = new GridView();

            DateTime FromDate = txtFromDate.Text == string.Empty ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = txtTodate.Text == string.Empty ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(txtTodate.Text);
            DataSet ds = objCommon.GetAttendanceData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), FromDate, ToDate, Convert.ToInt32(ddlSection.SelectedValue), ddlOperator.SelectedValue, Convert.ToDouble(txtPercentage.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), txtPercentageFrom.Text.ToString(), txtPercentageTo.Text.ToString(), Convert.ToInt32(selector));

            //**************************************************************************************
            //GetAttendanceForAll(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector)
            ds.Tables[0].Columns.Remove("ROLLNO");
            ds.Tables[0].Columns.Remove("IDNO");
            ds.Tables[0].Columns.Remove("SCHEMENO");
            ds.Tables[0].Columns.Remove("SEMESTERNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=Attendance_Sem-" + ddlSem.SelectedItem + ".xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayUserMessage(updSection, "No Record Found.", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ExcelReportShowStudentWise()
    {
        string collegenos = string.Empty;
        foreach (ListItem itm in ddlCollege.Items)
        {
            if (itm.Selected != true)
                continue;
            collegenos += itm.Value + ",";
        }

        collegenos = collegenos.Remove(collegenos.Length - 1);
        int sessionid = 0;
        sessionid = Convert.ToInt32(ddlSession.SelectedValue);
        string FromDate = txtFromDate.Text;
        string ToDate = txtTodate.Text;
        DataSet ds = objAttC.GetAllCoursesWiseAttendanceExcelReport(sessionid, collegenos, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].TableName = "Courses-Wise Attendance Summary";
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                    wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CourseWiseAttendanceSummary.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    private void ExcelReportFacultyWiseAttendanceReport()
    {
        GridView GVDayWiseAtt = new GridView();
        DataSet ds = Attd.GetStudAttDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), txtFromDate.Text, txtTodate.Text);
        //string degree = ds.Tables[0].Rows[3].ToString();
        //string branch = ds.Tables[0].Rows[4].ToString();
        //   string facuilty = ds.Tables[0].Rows[8].ToString();
        if (ds != null && ds.Tables.Count > 0)
        {
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment; filename=" + "Faculty wise Attendance Report" + "_" + txtFromDate.Text.Trim() + "_" + txtTodate.Text.Trim() + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Attendance Not Found", this.Page);
        }
    }

    #endregion

    #region CrystalReport Methods

    private void ShowReport_ATTENDANCE_PENDING_REPORT(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                ",@P_START_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy") +
                ",@P_END_DATE=" + Convert.ToDateTime(txtTodate.Text).ToString("dd-MM-yyyy") +
                ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) +
                ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);
            //divScript.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } ";
            //divScript.InnerHtml += " window.close();";
            //divScript.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //function to show the report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                + ",username=" + Session["userfullname"].ToString()
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                + ",@P_FROMDATE=" + txtFromDate.Text
                + ",@P_TODATE=" + txtTodate.Text
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                + ",@P_CONDITIONS=" + ddlOperator.SelectedValue
                + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim()
                + ",@P_SUBID=" + ddlSubjectType.SelectedValue
                + ",@P_COURSENO=" + ddlSubject.SelectedValue
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void AttDetailsReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                + ",username=" + Session["userfullname"].ToString()
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                + ",@P_FROMDATE=" + txtFromDate.Text
                + ",@P_TODATE=" +txtTodate.Text
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                + ",@P_CONDITIONS=" + ddlOperator.SelectedValue
                + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim()
                + ",@P_SUBID=" + ddlSubjectType.SelectedValue
                + ",@P_COURSENO=" + ddlSubject.SelectedValue
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CumulativeAttDetailsReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy")
                + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("dd/MM/yyyy")
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void AttReportWithOD(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"])
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                + ",@P_SUBID=" + ddlSubjectType.SelectedValue
                + ",@P_COURSENO=" + ddlSubject.SelectedValue
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                + ",@P_FROMDATE=" +txtFromDate.Text
                + ",@P_TODATE=" + txtTodate.Text
                + ",@P_CONDITIONS=" + ddlOperator.SelectedValue
                + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim()
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + "AttReportWithOD" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void FacultyIncompleteAttendance(string reportTitle, string rptFileName)
    {
        try
        {
            if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtTodate.Text))
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                    + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                    + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                    + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                    + ",@P_COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue)
                    + ",@P_FROMDATE=" + txtFromDate.Text
                    + ",@P_TODATE=" + txtTodate.Text
                    + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + "Faculty Incomplete Attendance" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);

                // ",@P_COLLEGE_ID="+ Convert.ToInt32(ddlInstitute.SelectedValue) +
            }
            else
            {
                objCommon.DisplayMessage(this.updSection, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion
  
    #region Onclick Events

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlReportName.SelectedItem.Text == "SUBJECT WISE REPORT")
        {
            ShowReport("Subject Wise Attendance", "rptSubjectwiseAttendance_New.rpt");
        }
        else if (ddlReportName.SelectedItem.Text == "SUBJECT WISE DETAILS")
        {
            ShowReport("Display Attendance", "rptAttendanceForDisplay.rpt");
        }
        else if (ddlReportName.SelectedItem.Text == "ATTENDANCE DETAILS")
        {
            this.AttDetailsReport("Attendance Details", "rptAttendanceDetails.rpt");
        }
        else if (ddlReportName.SelectedItem.Text == "CUMULATIVE ATTENDANCE")
        {
            this.CumulativeAttDetailsReport("Cumulative Attendance Details", "rptTotalAttendanceDetails.rpt");
        }
        else if (ddlReportName.SelectedItem.Text == "ATT.REPORT WITH OD")
        {
            this.AttReportWithOD("AttReportWithOD", "rptAttReportWithOD.rpt");

        }
        else if (ddlReportName.SelectedItem.Text == "FACULTY INCOMPLETE ATTENDANCE")
        {
            FacultyIncompleteAttendance("Faculty Incomplete Attendance", "rptFacultyIncompleteAttendance.rpt");
        }
        else if (ddlReportName.SelectedItem.Text == "ATTENDANCE PENDING REPORT")
        {
            ShowReport_ATTENDANCE_PENDING_REPORT("NotAttendanceDetails", "rptAttendanceNotSentByFaculty.rpt");
        }
       
    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        if (ddlReportName.SelectedItem.Text == "COURSEWISE REPORT (EXCEL)")
        {
            this.ExcelReportShow();
        }
        else if (ddlReportName.SelectedItem.Text == "CONSOLIDATED ATTENDANCE REPORT FORMAT-I (EXCEL)")
        {
            this.ExcelReportConsolidatedFormat_I();
        }
        else if (ddlReportName.SelectedItem.Text == "CONSOLIDATED ATTENDANCE REPORT FORMAT-II(EXCEL)")
        {
            this.ExcelReportConsolidatedFormat_II();
        }
        else if (ddlReportName.SelectedItem.Text == "SHOW STUDENT-WISE ATTENDANCE")
        {
            this.ExcelReportShowStudentWise();
        }
        else if (ddlReportName.SelectedItem.Text == "FACULTY WISE ATTENDANCE REPORT")
        {
            this.ExcelReportFacultyWiseAttendanceReport();
        }
        
            
        
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlReportName.SelectedItem.Text == "FACULTYWISE ATTENDANCE STATUS")
        {
            this.BindFacultyWiseStatus();
        }
        else if (ddlReportName.SelectedItem.Text == "FACULTY WISE ATTENDANCE REPORT SHOW")
        {
            int select = 0;

            if (rdoPerBtn.Checked == true)
            {
                select = 0;
                lvByPercent.Visible = true;
                BindListByPer(select);
            }
            else if (rdoOpr.Checked == true)
            {
                select = 1;
                pnlAttendence.Visible = true;
                lvAttendence.Visible = true;
                BindListView_Operator(select);
            }
            else
            {
                select = 2;
                lvByPercent.Visible = true;
                BindForAll(select);
            }
        }
        else if (ddlReportName.SelectedItem.Text == "SHOW ATTENDANCE DETAILS")
        {
            this.BindStudentAttendanceDetails();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Selected Index Change Events

    protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
    {
        divheading.Visible = true;
        ClearControls();
        Validation();
        BindDataFilters(Convert.ToInt32(ddlReportName.SelectedValue));
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstitute.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlInstitute.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            ddlSession.Focus();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString() + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportName.SelectedItem.Text == "COURSEWISE REPORT (EXCEL)" || ddlReportName.SelectedItem.Text == "SHOW STUDENT-WISE ATTENDANCE")
        {
            if (ddlSession.SelectedIndex > 0)
            {
                CourseController objStud = new CourseController();
                DataSet dsCollegeSession = objStud.GetCollegeBySessionid(1, Convert.ToInt32(ddlSession.SelectedValue));
                ddlCollege.Items.Clear();
                ddlCollege.DataSource = dsCollegeSession;
                ddlCollege.DataValueField = "COLLEGE_ID";
                ddlCollege.DataTextField = "COLLEGE_NAME";
                ddlCollege.DataBind();
            }
            else
            {
                ddlCollege.DataSource = null;
                ddlCollege.DataBind();
            }
        }
        else if (ddlReportName.SelectedItem.Text == "CONSOLIDATED ATTENDANCE REPORT FORMAT-I (EXCEL)" || ddlReportName.SelectedItem.Text == "CONSOLIDATED ATTENDANCE REPORT FORMAT-II(EXCEL)" || ddlReportName.SelectedItem.Text == "ATTENDANCE PENDING REPORT"  || ddlReportName.SelectedItem.Text== "SHOW ATTENDANCE DETAILS")
        {
            if (ddlSession.SelectedIndex > 0)
            {
                
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                ddlSem.Focus();
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        else if (ddlReportName.SelectedItem.Text == "FACULTYWISE ATTENDANCE STATUS")
        {
            if (ddlSession.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
                }
                else
                {
                    // clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "DEPTNAME ASC");
                }
                ddlDepartment.Focus();
            }
            else
            {
                ddlDepartment.Items.Clear();
                ddlDepartment.Items.Add("Please Select");
                ddlSem.Items.Clear();
                ddlSem.Items.Add("Please Select");
                ddlSection.Items.Clear();
                ddlSection.Items.Add("Please Select");
                ddlSubject.Items.Clear();
                ddlSubject.Items.Add("Please Select");
                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Add("Please Select");
            }
            lvAttStatus.DataSource = null;
            lvAttStatus.DataBind();
        }
        else
        {
            if (ddlSession.SelectedIndex > 0)
            {
                this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]));
                ddlSem.Focus();
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlSubject.Items.Clear();
                ddlSubject.Items.Add(new ListItem("Please Select", "0"));
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
            }
 
        }


       
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {    
        try
        {
            if (ddlReportName.SelectedItem.Text == "FACULTYWISE ATTENDANCE STATUS")
            {
                if (ddlSem.SelectedIndex > 0)
                {
                    if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        objCommon.FillDropDownList(ddlSection, "ACD_SECTION SEC INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SECTIONNO=SEC.SECTIONNO)", "DISTINCT CT.SECTIONNO", "SEC.SECTIONNAME", "CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND SEC.SECTIONNO>0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.SECTIONNO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSection, "ACD_SECTION SEC INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SECTIONNO=SEC.SECTIONNO)", "DISTINCT CT.SECTIONNO", "SEC.SECTIONNAME", "CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND SEC.SECTIONNO>0", "CT.SECTIONNO");
                    }
                    ddlSection.Focus();
                }
                else
                {
                    ddlSection.Items.Clear();
                    ddlSection.Items.Add("Please Select");
                    ddlSubject.Items.Clear();
                    ddlSubject.Items.Add("Please Select");
                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add("Please Select");
                }
                lvAttStatus.DataSource = null;
                lvAttStatus.DataBind();
            }
            else
            {


                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0 AND SR.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SC.SECTIONNAME");
                objCommon.FillDropDownList(ddlCourseType, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO=C.COURSENO INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and OC.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND OC.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");

                if (ddlSem.SelectedIndex > 0)
                {
                    //bind Subject name in drop down list
                    objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue, "O.COURSENO");

                    objCommon.FillDropDownList(ddlSubjectType, "ACD_ATTENDANCE C WITH (NOLOCK) INNER JOIN ACD_SUBJECTTYPE S WITH (NOLOCK) ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "AND C.SEMESTERNO=" + ddlSem.SelectedValue + " AND C.SESSIONNO=" + ddlSession.SelectedValue, "C.SUBID");
                    ddlSubjectType.Focus();
                }

                ddlCourseType.SelectedIndex = 0;
                ddlSubject.SelectedIndex = 0;
                ddlSection.SelectedIndex = 0;
                txtFromDate.Text = "";
                txtTodate.Text = "";
                txtPercentage.Text = "0";
                ddlSubjectType.Focus();

                DataSet ds = Attd.GetDetailsFromReportMaster(Convert.ToInt32(ddlReportName.SelectedValue));

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_FROMDT_RFV"].ToString()) == 1)
                {
                    StartDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                    EndDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                    divDateDetails.Visible = true;

                    lblTitleDate.Text = "Selected Session Start Date : " + StartDate.ToShortDateString() + " End Date : " + EndDate.ToShortDateString();
                }
                else
                {
                    divDateDetails.Visible = false;
                }
            }
        }
        catch
        {
            lblTitleDate.Text = "Selected Session Start Date : - End Date : -";
        }
    }

    protected void ddlCourseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourseType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue + " AND C.SUBID  = " + ddlSubjectType.SelectedValue, "O.COURSENO");
            ddlSubject.Focus();
        }

        ddlSection.SelectedIndex = 0;
        // ddlSubject.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtPercentage.Text = "0";
        ddlSubject.Focus();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlReportName.SelectedItem.Text == "FACULTYWISE ATTENDANCE STATUS")
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT CT.UA_NO", "U.UA_FULLNAME", "CT.COURSENO=" + ddlSubject.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND SESSIONNO=" + ddlSession.SelectedValue + "", "U.UA_FULLNAME");
                ddlFaculty.Focus();
            }
            else
            {
                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Add("Please Select");
            }
            lvAttStatus.DataSource = null;
            lvAttStatus.DataBind();
        }
        else
        {

            ddlSection.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtTodate.Text = "";
            txtPercentage.Text = "0";
            ddlSection.Focus();
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportName.SelectedItem.Text == "FACULTYWISE ATTENDANCE STATUS")
        {
            if (ddlSection.SelectedIndex > 0)
            {
                if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    //objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + ddlScheme.SelectedValue + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0", "CT.COURSENO");
                    objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.COURSENO");
                }
                else
                {
                    //objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + ddlScheme.SelectedValue + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.COURSENO");
                    objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0", "CT.COURSENO");
                }
                ddlSubject.Focus();
            }
            else
            {
                ddlSubject.Items.Clear();
                ddlSubject.Items.Add("Please Select");
                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Add("Please Select");
            }
            lvAttStatus.DataSource = null;
            lvAttStatus.DataBind();
        }
        else
        {
            txtFromDate.Text = "";
            txtTodate.Text = "";
            txtPercentage.Text = "0";
            txtFromDate.Focus();
            lvAttendence.Visible = false;
            lvByPercent.Visible = false;
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = Attd.GetDetailsFromReportMaster(Convert.ToInt32(ddlReportName.SelectedValue));

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_FROMDT_RFV"].ToString()) == 1)
        {
            try
            {
                DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));

                if (Convert.ToDateTime(txtFromDate.Text) < SDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                    txtFromDate.Text = string.Empty;
                    txtFromDate.Focus();
                }
                else if (Convert.ToDateTime(txtFromDate.Text) > EDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                    txtFromDate.Text = string.Empty;
                    txtFromDate.Focus();
                }
                else
                {
                    txtTodate.Focus();
                }
            }
            catch
            {
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
            }
        }
       
    }

    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = Attd.GetDetailsFromReportMaster(Convert.ToInt32(ddlReportName.SelectedValue));

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_FROMDT_RFV"].ToString()) == 1)
        {
            try
            {
                DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
                DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

                if (Convert.ToDateTime(txtTodate.Text) < SDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                    //  objCommon.DisplayMessage(this.Page, "End date should be greater than session start date", this.Page);
                    txtTodate.Text = string.Empty;
                    txtTodate.Focus();
                }
                else if (Convert.ToDateTime(txtTodate.Text) > EDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                    // objCommon.DisplayMessage(this.Page, "End date should be less than session end date", this.Page);
                    txtTodate.Text = string.Empty;
                    txtTodate.Focus();
                }
                else
                {
                    ddlOperator.Focus();
                }
            }
            catch
            {
                txtTodate.Text = string.Empty;
                txtTodate.Focus();
            }
        }
        
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rdoPerBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoPerBtn.Checked == true)
        {
            divtxtPercentageFrom.Visible = true;
            divtxtPercentageTo.Visible = true;
        }
        else
        {
            divtxtPercentageFrom.Visible = false;
            divtxtPercentageTo.Visible = false;
        }
    }

    protected void rdoOpr_CheckedChanged(object sender, EventArgs e)
    {

    }

    #endregion

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
        if (ddlSubjectType.SelectedIndex > 0)
        {
            ddlSection.Items.Clear();
            // objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION S ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue+" AND SR.SCHEMENO="+ddlScheme.SelectedValue+" AND SR.SEMESTERNO="+ddlSem.SelectedValue+" AND S.SECTIONNO>0", "S.SECTIONNO");
            objCommon.FillDropDownList(ddlSection, "ACD_ATTENDANCE SR WITH (NOLOCK) INNER JOIN ACD_SECTION S  WITH (NOLOCK) ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue + " AND S.SECTIONNO>0", "S.SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string clgID = ddlSchoolInstitute.SelectedIndex > 0 ?ddlSchoolInstitute.SelectedValue: "0";
        int Deptno = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        if (ddlDepartment.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER SEM, ACD_SESSION_MASTER SM", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "SEM.SEMESTERNO%2 IN(CASE WHEN SM.ODD_EVEN=1 THEN SM.ODD_EVEN ELSE 0 END) AND SM.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEM.SEMESTERNO>0", "SEM.SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("Please Select");
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }
}