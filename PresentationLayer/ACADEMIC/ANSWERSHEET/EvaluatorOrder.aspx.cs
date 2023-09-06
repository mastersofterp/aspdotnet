//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : EVALUATOR ORDER                                                     
// CREATION DATE : 19-JUNE-2017                                                          
// CREATED BY    : ROHIT KUMAR TIWARI                               
// MODIFIED by   :                                                    
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ACADEMIC_ANSWERSHEET_EvaluatorOrder : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string Facnos = string.Empty;
    AnswerSheetController objAnsSheetController = new AnswerSheetController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #region Page Load
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                   
                    this.FillDropdown();
                  //  txtReportDate.Text = DateTime.Today.ToShortDateString();
                  //this.BindListView();
                    bindpageLoad();
                  //  btnReport.Enabled = false;
                  //  btnDeclarationReport.Enabled = false;
                    dept.Visible = false;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnReport);
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnDeclarationReport);

           
                  
                }
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
            //this.BindListView();
            //bindpageLoad();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorOrder --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=EvaluationRateMatser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EvaluationRateMatser.aspx");
        }
    }
    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
           // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
           // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
           // objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "EXAMNO", "EXAMNAME", "EXAMTYPE=2 and PATTERNNO=1", "EXAMNO DESC");
           // objCommon.FillDropDownList(ddlFaculty, "ACD_EXAM_STAFF WITH (NOLOCK)", "EXAM_STAFF_NO", "STAFF_NAME", "EXAM_STAFF_NO>0", "EXAM_STAFF_NO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorOrder.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
     #endregion

    #region Other Events
    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDegree.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
    //          ddlBranch.Focus();
    //    }
    //}

    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlDegree.SelectedValue) > 0 && ddlBranch.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ViewState["branchno"] + "AND DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO");
    //        objCommon.FillListBox(ddlFaculty, "ACD_EXAM_STAFF WITH (NOLOCK)", "EXAM_STAFF_NO", "STAFF_NAME", "(STAFF_TYPE='E' OR STAFF_TYPE='B') AND ACTIVE=1 AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]), "EXAM_STAFF_NO");
    //    }
    //    lvEvaluator.Visible = false;
    //}

    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ViewState["schemeno"], "SE.SEMESTERNO");
    //}
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateCourses();
    }

    private void bindpageLoad()
    {
        DataSet dss = objCommon.FillDropDown("ACD_EXAM_EVALUATOR E INNER JOIN ACD_SESSION_MASTER SM ON (E.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_SEMESTER S ON (E.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_COURSE C ON (E.COURSENO=C.COURSENO) INNER JOIN ACD_EXAM_STAFF ES ON (E.EXAM_STAFF_NO=ES.EXAM_STAFF_NO)", "E.EVAL_APPID", "E.EVAL_APPID,SM.SESSION_NAME,S.SEMESTERNAME,(C.CCODE+'-'+ C.COURSE_NAME) AS COURSENAME,(CASE E.EXAM_STAFF_TYPE WHEN '1' THEN 'Internal' WHEN '2' THEN 'External' WHEN null THEN '-' END)AS FAC_TYPE,ES.STAFF_NAME,(CASE E.APPROVE_STATUS when '0' then 'Pending' WHEN '1' THEN 'Approved' when '2' then 'Rejected' end) as APPROVE_STATUS", " E.STATUS=0", "E.EVAL_APPID DESC");
      if (dss.Tables[0].Rows.Count > 0)
      {
          lvEvaluator.DataSource = dss;
          lvEvaluator.DataBind();
      }
      else
      {
          lvEvaluator.DataSource = null;
          lvEvaluator.DataBind();
      }



        //    lvSession.DataSource = ds;
        //lvSession.DataBind();
    }


    private void PopulateCourses()
    {
        //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "CCODE+'-'+COURSE_NAME", "SEMESTERNO =" + ddlSem.SelectedValue + " AND SUBID IN (1,3) AND SCHEMENO =" + ddlScheme.SelectedValue + " AND COURSENO NOT IN (SELECT COURSENO FROM ACD_EXAM_EVALUATOR WHERE STATUS=0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMNO=" + ddlExam.SelectedValue + " AND EXAM_STAFF_TYPE=" + ddlEvalutor.SelectedValue + " AND EXAM_STAFF_NO =" + ddlFaculty.SelectedValue + ")", "COURSENO");

        objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT COURSENO", "CCODE+'-'+COURSENAME", "SUBID = 1 AND SEMESTERNO =" + ddlSem.SelectedValue + " AND SCHEMENO =" + ViewState["schemeno"] + " AND COURSENO NOT IN (SELECT COURSENO FROM ACD_EXAM_EVALUATOR WHERE STATUS=0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND EXAM_STAFF_TYPE=" + ddlEvalutor.SelectedValue + ")", "COURSENO");

        //BindListView();

      //  objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO","DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"] + "AND SR.SEMESTERNO = " + ddlSem.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
    }

    private void RevluationCourses()
    {
       // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C WITH (NOLOCK) inner join ACD_REVAL_RESULT R WITH (NOLOCK) on (C.COURSENO=R.COURSENO)", "distinct C.COURSENO", "C.CCODE+'-'+C.COURSE_NAME", "C.SEMESTERNO =" + ddlSem.SelectedValue + " AND C.SCHEMENO =" + ViewState["schemeno"] + " AND  R.REV_APPROVE_STAT=1 and C.COURSENO NOT IN (SELECT COURSENO FROM ACD_EXAM_EVALUATOR WHERE STATUS=0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMNO=" + ddlExam.SelectedValue + " AND EXAM_STAFF_TYPE=" + ddlEvalutor.SelectedValue + " AND EXAM_STAFF_NO =" + ddlFaculty.SelectedValue + ")", "COURSENO");
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "CCODE+'-'+COURSE_NAME", "SEMESTERNO =" + ddlSem.SelectedValue + " AND SUBID IN (1,3) AND SCHEMENO =" + ViewState["schemeno"], "COURSENO");
    }


    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEvalutor, "ACD_STAFF_TYPE WITH (NOLOCK)", "STAFFTYPE_NO", "STAFFTYPE_NAME", "", "STAFFTYPE_NO");
        //int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_EVALUATOR WITH (NOLOCK)", "count(1)", "COURSENO='" + ddlCourse.SelectedValue + "' and  SESSIONNO='" + ddlSession.SelectedValue + "' and Branchno='" + ViewState["branchno"] + "' and schemeno='" + ViewState["schemeno"] + "' and semesterno='" + ddlSem.SelectedValue + "' And APPROVE_STATUS=1"));
        //if (count > 0)
        //{
        //    btnReport.Enabled = true;
          //  btnDeclarationReport.Enabled = true;
        //}
        //else
        //{
        //    btnReport.Enabled = true;
        //    btnDeclarationReport.Enabled = true;
        //}
    }
  
    protected void ddlEvalutor_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            //objCommon.FillListBox(ddlFaculty, " ACD_EXAM_STAFF S WITH (NOLOCK) INNER JOIN ACD_EXAM_EVALUATOR E WITH (NOLOCK) ON (S.EXAM_STAFF_NO=E.EXAM_STAFF_NO)", "DISTINCT S.EXAM_STAFF_NO", "S.STAFF_NAME", "ACTIVE=1 AND STAFF_TYPE  like ('%" + ddlEvalutor.SelectedValue + "%')" + " AND S.BRANCHNO=" + ViewState["branchno"] + " AND S.EXAM_STAFF_NO NOT IN (SELECT EXAM_STAFF_NO FROM ACD_EXAM_EVALUATOR  WHERE APPROVE_STATUS=1 AND  EXAMTYPE=1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + ")", "EXAM_STAFF_NO");
        //}
        if (ddlEvalutor.SelectedIndex > 0)
            dept.Visible = true;
        else
            dept.Visible = false;
        //objCommon.FillDropDownList(ddlDepartment, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND BRANCHNO !=" + ddlBranch.SelectedValue, "BRANCHNO");
        objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DEPTNO", "DEPTNAME", "DEPTNO > 0 ", "DEPTNO");

       // BindListView();

    }
    //protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlExamType.SelectedValue == "1")
    //    {
    //        PopulateCourses();
    //    }
    //    else
    //    {
    //        RevluationCourses();
    //    }
    //}
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {        
            //objCommon.FillListBox(ddlFaculty, "ACD_EXAM_STAFF WITH (NOLOCK)", "EXAM_STAFF_NO", "STAFF_NAME", "ACTIVE=1 AND STAFF_TYPE like ('%" + ddlEvalutor.SelectedValue + "%')" + " AND BRANCHNO=" + ddlDepartment.SelectedValue + "AND EXAM_STAFF_NO NOT IN (SELECT EXAM_STAFF_NO FROM ACD_EXAM_EVALUATOR  WHERE APPROVE_STATUS=1 AND  EXAMTYPE=1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + ")", "EXAM_STAFF_NO");
       
        //objCommon.FillListBox(ddlFaculty, "ACD_EXAM_STAFF S WITH (NOLOCK) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO)", "EXAM_STAFF_NO", "S.STAFF_NAME+'-'+'('+B.SHORTNAME+')' AS STAFF_NAME", "ACTIVE=1 AND STAFF_TYPE like ('%" + ddlEvalutor.SelectedValue + "%')" + " AND S.BRANCHNO=" + ddlDepartment.SelectedValue + "AND S.EXAM_STAFF_NO NOT IN (SELECT EXAM_STAFF_NO FROM ACD_EXAM_EVALUATOR  WHERE APPROVE_STATUS=1 AND  EXAMTYPE=1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + ")", "S.EXAM_STAFF_NO");

        string FacultyNos = string.Empty;
        
        foreach (ListItem items in ddlDepartment.Items)
        {
            if (items.Selected == true)
            {
                //FacultyNos += (items.Value).Split('-')[0] + ','; 

                FacultyNos += items.Value + ','; 
                //collegenames += items.Text + ',';
            }
        }
        if (FacultyNos.Length > 1)
        {
            FacultyNos = FacultyNos.Remove(FacultyNos.Length - 1);
        }

        if (FacultyNos.Length > 0)
        {
            //objCommon.FillListBox(ddlFaculty, "ACD_EXAM_STAFF S WITH (NOLOCK) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO)", "EXAM_STAFF_NO", "S.STAFF_NAME+'-'+'('+B.SHORTNAME+')' AS STAFF_NAME", "ACTIVE=1 AND STAFF_TYPE like ('%" + ddlEvalutor.SelectedValue + "%')" + " AND S.BRANCHNO=" + ddlDepartment.SelectedValue + "AND S.EXAM_STAFF_NO NOT IN (SELECT EXAM_STAFF_NO FROM ACD_EXAM_EVALUATOR  WHERE APPROVE_STATUS=1 AND  EXAMTYPE=1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + ")", "S.EXAM_STAFF_NO");
            //objCommon.FillListBox(ddlFaculty, "ACD_EXAM_STAFF S WITH (NOLOCK) INNER JOIN ACD_DEPARTMENT B ON (S.DEPTNO=B.DEPTNO)", "EXAM_STAFF_NO", "S.STAFF_NAME+'-'+'('+B.DEPTCODE+')' AS STAFF_NAME", "ACTIVE=1 AND STAFF_TYPE like ('%" + ddlEvalutor.SelectedValue + "%')" + " AND S.DEPTNO=" + ddlDepartment.SelectedValue + "AND S.EXAM_STAFF_NO NOT IN (SELECT EXAM_STAFF_NO FROM ACD_EXAM_EVALUATOR  WHERE APPROVE_STATUS=1 AND  EXAMTYPE=1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + ")", "S.EXAM_STAFF_NO");


            objCommon.FillListBox(ddlFaculty, "ACD_EXAM_STAFF S WITH (NOLOCK) LEFT JOIN ACD_DEPARTMENT B ON (S.DEPTNO=B.DEPTNO)", "EXAM_STAFF_NO", "S.STAFF_NAME+'-'+'('+B.DEPTCODE+')' AS STAFF_NAME", " S.STAFF_TYPE=" +ddlEvalutor.SelectedValue+"AND ACTIVE=1 AND S.DEPTNO in" + "(" + FacultyNos + ")" + "AND S.EXAM_STAFF_NO NOT IN (SELECT EXAM_STAFF_NO FROM ACD_EXAM_EVALUATOR WHERE APPROVE_STATUS=1 AND  EXAMTYPE=1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + ")", "S.DEPTNO,S.EXAM_STAFF_NO");


            ddlFaculty.Focus();
            //btnReport.Enabled = true;
            //btnDeclarationReport.Enabled = true;
            //btnEvaluatorReport.Enabled = true;
        }
        else
        {
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
           
        }       
    }
    private void BindListView()
     {
        try
        {
            AnswerSheet objAnsSheet = new AnswerSheet();
            int scheme=0;
            //GetStaffNo();

            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            //objAnsSheet.ExamNo = Convert.ToInt32(ddlExamType.SelectedValue);
           // objAnsSheet.FacultyNo = Convert.ToInt32(ddlFaculty.SelectedValue);
            objAnsSheet.FacultyNos = Facnos;
            objAnsSheet.FacultyType = Convert.ToInt32(ddlEvalutor.SelectedValue);
            scheme =Convert.ToInt32(ViewState["schemeno"]);



            DataSet ds = objAnsSheetController.GetAnswerSheetEvaluateDetails(objAnsSheet,scheme);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvEvaluator.DataSource = ds;
                lvEvaluator.DataBind();
                lvEvaluator.Visible = true;
                ddlSession.Enabled = true;
               // ddlDegree.Enabled = true;
                //ddlBranch.Enabled = true;
               // ddlScheme.Enabled = true;
                //ddlExamType.Enabled = true;
                ddlSem.Enabled = true;
                
            }
            else
            {
                lvEvaluator.DataSource = null;
                lvEvaluator.DataBind();
                lvEvaluator.Visible = false;
                //  objCommon.DisplayMessage(this.UpdatePanel1, "Records not found.", this.Page);
                //  btnSubmit.Enabled = false;       
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorOrder.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    # endregion


     void GetStaffNo()
    {
        
        foreach (ListItem items in ddlFaculty.Items)   //ddlFaculty
        {
            if (items.Selected == true)
            {
                Facnos += items.Value + ',';
               
            }
        }
        if (Facnos == string.Empty || Facnos == "")
        {
            Facnos = "0";
        }
        else
        {
            Facnos = Facnos.Remove(Facnos.Length - 1);
        }
       
    }


     void GetStaffNoforreport()
     {

         foreach (ListItem items in ddlFaculty.Items)   //ddlFaculty
         {
             if (items.Selected == true)
             {
                 Facnos += items.Value + '$';

             }
         }
         if (Facnos == string.Empty || Facnos == "")
         {
             Facnos = "0";
         }
         else
         {
             Facnos = Facnos.Remove(Facnos.Length - 1);
         }

     }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ActiveStatus = 0;
            string deptnos = string.Empty;
            //string Facnos = GetStaffNo();



            //string activitynames = string.Empty;
            foreach (ListItem items in ddlDepartment.Items)
            {
                if (items.Selected == true)
                {
                    deptnos += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }
            deptnos = deptnos.Remove(deptnos.Length - 1);
            GetStaffNo();


           // int deptno = Convert.ToInt32(objCommon.LookUp("acd_branch", "deptno", "barnchno=" + ddlBranch.SelectedValue));
            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            //objAnsSheet.BranchNo = deptno;
            objAnsSheet.Exam_Staff_No = Convert.ToInt32(ddlFaculty.SelectedValue);
            objAnsSheet.FacultyType = Convert.ToInt32(ddlEvalutor.SelectedValue);
            //objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
            objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            //objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            objAnsSheet.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objAnsSheet.Reporting_Date = Convert.ToDateTime(txtReportDate.Text);
            string DeptNo = deptnos;
            //objAnsSheet.ExamStaffNo = Facnos;
            string ExamStaffNo = Facnos;

            //if (rblStatus.Checked == true)
            //{
            //    ActiveStatus = 1;
            //}
            //else
            //{
            //    ActiveStatus = 0;
            //}


         
          
            if (txtToDate.Text != "")
            {
                objAnsSheet.To_Date = Convert.ToDateTime(txtToDate.Text);
            }
            else
            {
                DateTime? dtt = null;
                objAnsSheet.To_Date = DateTime.MinValue;
            }


            objAnsSheet.UANO = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = (CustomStatus)objAnsSheetController.InsertAnswerSheetEvaluator(objAnsSheet, DeptNo,ExamStaffNo,ActiveStatus);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updSession, "Record Saved Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Server Error...", this.Page);
            }
            //txtReportDate.Text = DateTime.Today.ToShortDateString();
            lvEvaluator.Visible = true;
            //BindListView();
            bindpageLoad();
           // PopulateCourses();
            ClearSelection();
            //BindListView();


            //ddlClgname.SelectedIndex = 0;
            //ddlSession.SelectedIndex = 0;
            //ddlFaculty.SelectedIndex = 0;           
            //ddlSem.SelectedIndex = 0;
            //ddlCourse.SelectedIndex = 0;
            //txtReportDate.Text = string.Empty;
            //txtToDate.Text = string.Empty;
            //ddlDepartment.SelectedIndex = 0;
            //ddlDepartment.ClearSelection();
            //ddlEvalutor.SelectedIndex = 0;
            //ddlEvalutor.ClearSelection();
            //ddlFaculty.SelectedIndex = 0;
            //ddlFaculty.ClearSelection();
            //rblStatus.Checked = true;
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorOrder.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
      {
         //if (ddlEvalutor.SelectedValue == "5")
         //{
         //    ShowReport("pdf", "rptanswersheetEvaluation.rpt");
         //}
         //else
         //{
         //    ShowReport("pdf", "rptanswersheetEvaluator.rpt");              
         //}
         //if (ddlFaculty.Selected <= 0)
         //{
         //    objCommon.DisplayMessage(this.updSession, "Select Staff", this.Page);
         //    return;
         //}
          if (ddlClgname.SelectedIndex == 0)
          {
              objCommon.DisplayMessage(this.updSession, "Please Select College&Scheme", this.Page);
          }
          if (ddlSession.SelectedIndex == 0)
          {
              objCommon.DisplayMessage(this.updSession, "Please Select Session", this.Page);
          }
          if (ddlCourse.SelectedIndex == 0)
          {
              objCommon.DisplayMessage(this.updSession, "Please Select  Course", this.Page);
          }
          if (ddlEvalutor.SelectedIndex == 0)
          {
              objCommon.DisplayMessage(this.updSession, "Please Select  Staff Type", this.Page);
          }

         if (ddlEvalutor.SelectedValue == "1" || ddlEvalutor.SelectedValue == "2")
         {
             ShowReport("pdf", "rptanswersheetEvaluator.rpt");
         }
         else if (ddlEvalutor.SelectedValue == "3" || ddlEvalutor.SelectedValue == "4")
         {
             ShowReport("pdf", "rptPracEvaluatorOrder.rpt");
         }
         else
         {
             ShowReport("pdf", "rptAnsSheetCheckerOrder.rpt");
         }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
           // if (rptFileName.ToString() == "rptanswersheetEvaluator.rpt")
            if (rptFileName.ToString() == "rptanswersheetEvaluator.rpt" || rptFileName.ToString() == "rptPracEvaluatorOrder.rpt" || rptFileName.ToString() == "rptAnsSheetCheckerOrder.rpt")
            {
                url += "&filename=" + ddlFaculty.SelectedItem.Text + "_" + ddlEvalutor.SelectedItem.Text + "-Order" + ".pdf";
            }
            else
            {
                url += "&filename=" + ddlFaculty.SelectedItem.Text + "_" + ddlEvalutor.SelectedItem.Text + "-Declaration" + ".pdf";
            }
            url += "&path=~,Reports,Academic," + rptFileName;
           // GetStaffNo();
            GetStaffNoforreport();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAM_STAFF_NO= " + Convert.ToString(Facnos) +",@P_EXAM_STAFF_TYPE=" + ddlEvalutor.SelectedValue; 
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAM_STAFF_NO= '"+ Convert.ToString(Facnos)+"' "+",@P_EXAM_STAFF_TYPE=" + ddlEvalutor.SelectedValue; 
            
            // +",@UserName="; +Session["userfullname"].ToString();
        
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

          //  To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDeclarationReport_Click(object sender, EventArgs e)
    {
        //if (ddlEvalutor.SelectedValue == "5")
        //{
        //    ShowReport("pdf", "rptEvaluationDeclaration.rpt");
        //}
        //else
        //{
        //    ShowReport("pdf", "rptEvaluatorDeclaration.rpt");
        //}
        if (ddlEvalutor.SelectedValue == "1" || ddlEvalutor.SelectedValue == "2")
        {
            ShowReport("pdf", "rptEvaluatorDeclaration.rpt");
        }
        else if (ddlEvalutor.SelectedValue == "3" || ddlEvalutor.SelectedValue == "4")
        {
            ShowReport("pdf", "rptPracEvaluatorDeclaration.rpt");
        }
        else
        {
            ShowReport("pdf", "rptAnsSheetCheckerDeclaration.rpt");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lvEvaluator.DataSource = null;
        lvEvaluator.DataBind();
        ClearSelection();
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    private void ClearSelection()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
       // ddlFaculty.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        txtReportDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
       // ddlDepartment.SelectedIndex = 0;
        ddlDepartment.ClearSelection();
        ddlEvalutor.SelectedIndex = 0;
        ddlEvalutor.ClearSelection();
       // ddlFaculty.SelectedIndex = 0;
        ddlFaculty.ClearSelection();   
    }
    protected void ibtnEvalDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton ibtnEvalDelete = sender as ImageButton;
            int IDNO = int.Parse(ibtnEvalDelete.CommandArgument);

            //  int req_no = Convert.ToInt32(ibtnEvalDelete.AlternateText);
            int retStatus = objAnsSheetController.DeleteEvaluator(IDNO);
            if (retStatus == 1)
            {
                objCommon.DisplayMessage(this.updSession, "Allocation Cancelled Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Error in Cancelling Allocation", this.Page);
            }
            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_EVALUATOR E INNER JOIN ACD_SESSION_MASTER SM ON (E.SESSIONNO=SM.SESSIONNO)INNER JOIN ACD_SEMESTER S ON(E.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_COURSE C ON (E.COURSENO=C.COURSENO) INNER JOIN ACD_EXAM_STAFF ES ON (E.EXAM_STAFF_NO=ES.EXAM_STAFF_NO)", "E.EVAL_APPID", "SM.SESSION_NAME,S.SEMESTERNAME,(C.CCODE+'-'+ C.COURSE_NAME) AS COURSENAME,(CASE E.EXAM_STAFF_TYPE WHEN '1' THEN 'Internal' WHEN '2' THEN 'External' WHEN null THEN '-' END)AS FAC_TYPE,ES.STAFF_NAME", "EVAL_APPID>0" + IDNO, "E.EVAL_APPID ASC"); ;

            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //    lvEvaluator.DataSource = ds;
            //    lvEvaluator.DataBind();
            //}
            //    else
            //{
            //    lvEvaluator.DataSource = null;
            //    lvEvaluator.DataBind();
            //}

            bindpageLoad();
            //BindListView();
            //PopulateCourses();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorOrder.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }

    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

            Common objCommon = new Common();

            if (ddlClgname.SelectedIndex > 0)
            {
                //Common objCommon = new Common();
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                    GetStaffNo();
                    BindListView();
                   

                }
            }
            else
                  {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            //ddlExam.Items.Clear();
           // ddlExam.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
           // ddlsemester.Items.Clear();
           // ddlsemester.Items.Add(new ListItem("Please Select", "0"));
           // ddlSubjectType.Items.Clear();
           // ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
           // divstatus.Visible = false;
        }

        //Clear();
        



    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string FacultyNos = string.Empty;

        foreach (ListItem items in ddlFaculty.Items)
        {
            if (items.Selected == true)
            {
                FacultyNos += items.Value + ',';       
            }
        }
        if (FacultyNos.Length > 0)
        {
           
            lvEvaluator.Visible = true;
            btnEvaluatorReport.Enabled = true;
            BindListView();
        }
        else
        {
            
           // btnEvaluatorReport.Enabled = false;
            lvEvaluator.Visible = false;
        }

    }
    //protected void btnApproved_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        AnswerSheet objAnsSheet = new AnswerSheet();
    //        int ret = 0;
    //        string StaffNo = objCommon.LookUp("ACD_EXAM_EVALUATOR WITH (NOLOCK)", "EXAM_STAFF_NO", "SessionNo=" + Convert.ToInt32(ddlSession.SelectedValue) + "and BranchNo=" + Convert.ToInt32(ViewState["branchno"]) + "and ExamNo=" + Convert.ToInt32(ddlExamType.SelectedValue) + "and SchemeNo=" + Convert.ToInt32(ViewState["schemeno"]) + "and SemesterNo=" + Convert.ToInt32(ddlSem.SelectedValue) + "and CourseNo=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and Examtype=" + Convert.ToInt32(ddlExamType.SelectedValue));

    //        if (rblApproved.SelectedValue == "0")
    //        {
    //            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
    //            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
    //            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
    //            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
    //            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
    //            objAnsSheet.BranchNo = Convert.ToInt32(ViewState["branchno"]);
    //            //objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
    //            objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
    //            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
    //            objAnsSheet.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
    //            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);

    //            {
    //                ret = ret + objAnsSheetController.UpdateEvaluatorApproveStatus(objAnsSheet, StaffNo);
    //            }


    //            if (ret > 0)
    //            {
    //                objCommon.DisplayMessage(this.updSession, "Evaluator Approved Successfully.", this.Page);
    //                //  ClearSelection();

    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage("Error..!!", this.Page);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorOrder.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server UnAvailable");
    //    } objUaimsCommon.ShowError(Page, "Server Unavailable.");
        

    //}
    
    protected void btnEvaluatorReport_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updSession, "Please Select College & Scheme", this.Page);
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updSession, "Please Select Session", this.Page);
            return;
        }
        else
        {
            ShowApproveReport("EvaaluatorReport", "rptInternalExternalEvaluatorApprove.rpt");
        }
    }

    private void ShowApproveReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_STATUS=" + 0 + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "AnswersheetRecieve.ShowApproveReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ViewState["schemeno"], "SE.SEMESTERNO");
    }
    
}
 
    
