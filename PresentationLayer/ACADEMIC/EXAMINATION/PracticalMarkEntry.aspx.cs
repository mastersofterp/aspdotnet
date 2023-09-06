//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ACADEMIC - SUB COMPONETS WISE MARK ENTRY                                           
// CREATION DATE : 31-MARCH-2020                                                     
// CREATED BY    : MAHESH MALVE                                                 
// MODIFIED BY   :                                                   
// MODIFIED DESC : 
//=================================================================================


using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class ACADEMIC_EXAMINATION_PracticalMarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    string colS1T1Mark, colS1T2Mark, colS1T3Mark, colS1T4Mark, colS1T5Mark, colS2T1Mark, colS2T2Mark;
    int s;
    
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
            if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

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
                    //Check for Panel
                    if (ViewState["action"] == null)
                    {
                        //selection panel
                        pnlSelection.Visible = true;
                        pnlMarkEntry.Visible = false;
                    }
                    else if (ViewState["action"].ToString().Equals("markentry"))
                    {
                        //mark entry panel
                        pnlMarkEntry.Visible = true;
                        pnlSelection.Visible = false;
                    }

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", " DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) CROSS APPLY DBO.SPLITSTRING(COLLEGE_IDS,',') D WHERE STARTED = 1 AND  SHOW_STATUS =1 AND CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112) AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND D.DATA IN(SELECT DATA FROM DBO.SPLITSTRING('" + Session["college_nos"].ToString() + "',',')))", "");                     
                    ddlSession.SelectedIndex = 1;
                    //objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SUBID", "DBO.FN_DESC('SUBJECTTYPE',SUBID)SUBNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(UA_NO,0)= " + Convert.ToInt32(Session["userno"]) + " AND SUBID = 1", "SUBID");
                    objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SUBID", "DBO.FN_DESC('SUBJECTTYPE',SUBID)SUBNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND (ISNULL(UA_NO,0)= " + Convert.ToInt32(Session["userno"]) + " OR ISNULL(UA_NO_PRAC,0)= " + Convert.ToInt32(Session["userno"]) + ") AND SUBID IN(1,2)", "SUBID");
                    
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    if (ddlSession.Items.Count == 0)
                    {
                        objCommon.DisplayMessage(updMarkEntry, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    }
                }

            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void MarkEntryStatus(int SessionNo)
    {
        try
        {
            divstatus.Visible = false;
            DataSet dsExams = objMarksEntry.GetONExamsActivityBySubjectType(SessionNo, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
            if (dsExams.Tables[0].Rows.Count > 0)
            {
                string status = string.Empty;
                divstatus.Visible = true;
                DataTableReader dtr = dsExams.Tables[0].CreateDataReader();

                while (dtr.Read())
                {
                    if (status == string.Empty)
                        status = "This is to inform you that theory";
                    else
                        status += " & theory ";

                    status += dtr["EXAMNAME"] + " Mark Entry activity is going to be stopped on " + dtr["ENDDATE"];
                }
                dtr.Close();

                lblstatusmark.Text = status;
            }
        }
        catch (Exception Ex)
        { 
        
        }
    }

    private void ShowCourses()
    {
        try
        {
            DataSet ds = objMarksEntry.GetCourseForTeacher(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]),  Convert.ToInt16(ddlSubjectType.SelectedValue));

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds.Tables[0];
                    lvCourse.DataBind();
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page);
                }
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.ShowCourses --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            if (!lnk.ToolTip.Equals(string.Empty))
            {
                DataSet CourseEligibilityCheckCount = null;
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                ViewState["COURSENO"] = lblCourse.ToolTip;
                ViewState["CCODE"] = (objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "CCODE", "COURSENO='" + lblCourse.ToolTip + "'"));
                //CourseEligibilityCheckCount = objMarksEntry.CourseEligibilityCheck(Convert.ToInt32(ddlSession.SelectedValue), ViewState["CCODE"].ToString(), Convert.ToInt32(lnk.ToolTip));
                CourseEligibilityCheckCount = objMarksEntry.CourseEligibilityCheck_IA(Convert.ToInt32(ddlSession.SelectedValue), ViewState["CCODE"].ToString(), Convert.ToInt32(lnk.ToolTip));

                if (CourseEligibilityCheckCount.Tables[0].Rows.Count > 0)
                {
                    string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                    hdfSection.Value = sec_batch[0].ToString();

                    ddlSession2.Items.Clear();
                    ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                    hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
              
                    int CourseNo = 0;
                    LinkButton btn = sender as LinkButton;
                    CourseNo = Convert.ToInt32((btn.Parent.FindControl("hdnfld_courseno") as HiddenField).Value);
                    ViewState["sem"] = Convert.ToInt32((btn.Parent.FindControl("hdnsem") as HiddenField).Value);
                    hdfSemNo.Value = Convert.ToString(ViewState["sem"]);

                    DataSet dsExams = null;

                    //dsExams = objMarksEntry.GetONExamsActivityBySubjectType(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
                    dsExams = objMarksEntry.GetONExamsActivityBySubjectType_IA(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));

                    if (dsExams.Tables[0].Rows.Count > 0)
                    {
                        int PatternNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON (C.SCHEMENO=S.SCHEMENO)", "distinct PATTERNNO", "COURSENO=" + CourseNo + ""));
                        dsExams = objMarksEntry.GetPracticalSubExamsBySubjectType_IA(Convert.ToInt32(ddlSession.SelectedValue), PatternNo, Convert.ToInt32(ddlSubjectType.SelectedValue));
                    }
                    
                    int i = dsExams.Tables[0].Rows.Count;
                    string exams = string.Empty;
                    if (dsExams != null && dsExams.Tables.Count > 0)
                    {
                        if (dsExams.Tables[0].Rows.Count > 0)
                        {
                            DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                            while (dtr.Read())
                            {
                                exams += dtr["FLDNAME"] == DBNull.Value ? string.Empty : dtr["FLDNAME"].ToString() + ",";
                            }
                            dtr.Close();
                        }
                        else
                            objCommon.DisplayMessage(updMarkEntry, "Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);
                    }
                    else
                        ////objCommon.DisplayMessage("Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);
                        objCommon.DisplayMessage(updMarkEntry, "Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);

                    //If any exams are present then proceed
                    if (exams.Length > 0)
                    {
                        //Store exams to viewstate to access later
                        ViewState["exams"] = exams.Split(','); //store arrat
                        ViewState["exam"] = exams;

                        ddlExam.Items.Clear();
                        ddlExam.Items.Add(new ListItem("Select Exam", "0"));
                        int j = 0;
                        DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                        Dictionary<string, string> ddlTooltip = new Dictionary<string, string>();
                        while (dtr.Read())
                        {
                            j = j + 1;
                            if (dtr["FLDNAME"] != DBNull.Value)
                            {
                                if (ddlSubjectType.SelectedIndex > 0)
                                {
                                   
                                    //ListItem LI = new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME"].ToString());
                                    ListItem LI = new ListItem(dtr["EXAMNAME"].ToString(), dtr["EXAMNO"].ToString());
                                    LI.Attributes.Add("Title", dtr["EXAMNO"].ToString());
                                    ddlExam.Items.Add(LI);

                                    
                                    if (ddlTooltip != null)
                                    {
                                        ddlTooltip.Add(dtr["FLDNAME"].ToString(), dtr["EXAMNO"].ToString());
                                    }
                                    else
                                    {
                                        ddlTooltip.Add(dtr["FLDNAME"].ToString(), dtr["EXAMNO"].ToString());
                                    }
                                    Session["ddlTooltip"] = ddlTooltip;
                                }
                            }
                        }
                        dtr.Close();

                        if (ddlExam.Items.Count > 0)
                        {
                            pnlSelection.Visible = false;
                            pnlMarkEntry.Visible = true;
                            pnlStudGrid.Visible = false;
                            btnBack.Visible = true;
                            btnSave.Visible = false;
                            lblStudents.Visible = false;
                        }

                        //Added By Dileep Kare 22/02/2021 for Calculate Best of Two Mark.
                        string count = objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND CCODE='" + ViewState["CCODE"].ToString() + "' AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + " AND SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND S1T1_LOCK=1 AND   S1T2_LOCK=1 AND   S1T3_LOCK=1 AND   S1T4_LOCK=1");
                        if (Convert.ToInt32(count) > 0)
                        {
                            btnConsolidateMark.Enabled = true;
                        }
                        else
                        {
                            btnConsolidateMark.Enabled = false;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updMarkEntry, "No Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updMarkEntry, "Marks entry for Selected Course may not be Started !!", this.Page);
                    pnlMarkEntry.Visible = false;
                    return;
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAndLock(0, Convert.ToInt32(hdfSemNo.Value));
        string count = objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND CCODE='" + ViewState["CCODE"].ToString() + "' AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + " AND SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND S1T1_LOCK=1 AND   S1T2_LOCK=1 AND   S1T3_LOCK=1 AND   S1T4_LOCK=1");
        if (Convert.ToInt32(count) > 0)
        {
            btnConsolidateMark.Enabled = true;
        }
        else
        {
            btnConsolidateMark.Enabled = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        //GetStatus();
        GetStatus_IA();
    }

    private void BindJS()
    {
        try
        {
            int locks1 = 0;
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                //For all Sub Components
                TextBox txtS1T1Marks = gvRow.FindControl("txtS1T1Marks") as TextBox;
                Label lblMaxS1T1Marks = gvRow.FindControl("lblMaxS1T1Marks") as Label;
                Label lblMinS1T1Marks = gvRow.FindControl("lblMinS1T1Marks") as Label;
                HiddenField hdnS1T1Marks = gvRow.FindControl("hdnS1T1Marks") as HiddenField; //To Store the Marks Entered in hidden field
               
                if (lblMaxS1T1Marks.ToolTip.ToUpper().Equals("TRUE"))
                {
                    locks1 = 1;
                }

                if (lblMaxS1T1Marks.ToolTip.ToUpper().Equals("TRUE"))
                {
                    txtS1T1Marks.Enabled = false;
                    CheckBox chkS1T1MarksLock = gvStudent.HeaderRow.FindControl("chkS1T1MarksLock") as CheckBox;
                    chkS1T1MarksLock.Checked = true;
                    chkS1T1MarksLock.Enabled = false;
                }
                txtS1T1Marks.Attributes.Add("onblur", "validateMark(this," + lblMaxS1T1Marks.Text + "," + lblMinS1T1Marks.Text + ",'1'," + locks1 + "," + hdnS1T1Marks.Value + ")");
            }

            btnSave.Visible = true;
            if (gvStudent.Columns[3].Visible == true )
            {
                if (locks1 == 1 )
                {
                    btnSave.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.BindJS --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Style.Add("padding-top", "11%");
                    e.Row.Cells[1].Style.Add("padding-top", "11%");
                    e.Row.Cells[2].Style.Add("padding-top", "11%");
                    e.Row.Cells[3].Style.Add("padding-top", "11%");
                }
            }

            if (gvStudent.HeaderRow != null)
            {
                //string[] Examno = ddlExam.SelectedValue.Split('-');
                int MExamNo = Convert.ToInt32(ddlExam.SelectedValue);
                //DataSet dsExamName = objMarksEntry.GetPractical_Course_SubExamName(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["COURSENO"]), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue), MExamNo);
                DataSet dsExamName = objMarksEntry.GetIA_Course_SubExamName(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["COURSENO"]), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue), MExamNo);

                DataTableReader dtrExamName = dsExamName.Tables[0].CreateDataReader();
                Label lblHS1T1Marks = (Label)gvStudent.HeaderRow.FindControl("lblHS1T1Marks");

                while (dtrExamName.Read())
                {

                    if (Convert.ToInt32(dtrExamName["MAXMARK"]) > 0)
                    {
                        gvStudent.Columns[0].HeaderStyle.CssClass = "Srln";
                        if (Convert.ToInt32(ViewState["StudCount"].ToString()) > 8)
                        {
                            gvStudent.Columns[1].HeaderStyle.CssClass = "EnrollNo_8";
                            gvStudent.Columns[2].HeaderStyle.CssClass = "StudentName_8";
                            gvStudent.Columns[3].HeaderStyle.CssClass = "Head_8";
                        }
                        else
                        {
                            gvStudent.Columns[1].HeaderStyle.CssClass = "EnrollNo_8";
                            gvStudent.Columns[2].HeaderStyle.CssClass = "StudentName_8_8";
                            gvStudent.Columns[3].HeaderStyle.CssClass = "Head_8_8";
                        }

                        gvStudent.Columns[3].Visible = true;
                        colS1T1Mark = "true";
                        lblHS1T1Marks.Text = Convert.ToString(dtrExamName["EXAMNAME"].ToString()) + "<br>" + "[Max : " + dtrExamName["MAXMARK"].ToString() + "]" + " <br>" + "[Min : " + dtrExamName["MINMARK"].ToString() + "]";
                    }
                }
                dtrExamName.Close();
                dtrExamName.Dispose();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.gvStudent_RowDataBound --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkS1T1MarksLock_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkS1T1MarksLock = gvStudent.HeaderRow.FindControl("chkS1T1MarksLock") as CheckBox;
        if (chkS1T1MarksLock.Checked == true)
        {
            CheckMarks(1, "S1T1");
        }
    }

    #region Private/Public Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PracticalMarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PracticalMarkEntry.aspx");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void SaveAndLock(int lock_status, int semno)
    {
        try
        {
            string examtype = string.Empty;
            string subExam = string.Empty;
            CustomStatus cs = CustomStatus.Error;

            //check for if any exams on
            if (ddlExam.SelectedIndex > 0)
            {
                subExam = objCommon.LookUp("ACD_SUBEXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "SUBEXAMNO=" + ddlExam.SelectedValue + "");

                for (int j = 3; j < gvStudent.Columns.Count; j++)
                {
                    if (gvStudent.Columns[j].Visible == true)
                    {
                        string studids = string.Empty;
                        string marks = string.Empty;
                       

                        MarksEntryController objMarksEntry = new MarksEntryController();
                        Label lbl;
                        TextBox txtMarks;

                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        //Note : -100 for Marks will be converted as NULL           
                        //NULL means mark entry not done.                           
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                        for (int i = 0; i < gvStudent.Rows.Count; i++)
                        {
                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;

                            if (j == 3) //S1T1 MARKS
                            {
                                //Gather Exam Marks 
                                txtMarks = gvStudent.Rows[i].FindControl("txtS1T1Marks") as TextBox;

                                //if(Convert.ToInt32(ddlExam.SelectedValue)==1 )
                                //    subExam = "S1T1";
                                //else if (Convert.ToInt32(ddlExam.SelectedValue) == 2)
                                //    subExam = "S1T2";
                                //else if (Convert.ToInt32(ddlExam.SelectedValue) == 3)
                                //    subExam = "S1T3";
                                //else if (Convert.ToInt32(ddlExam.SelectedValue) == 4)
                                //    subExam = "S1T4";
                                //else if (Convert.ToInt32(ddlExam.SelectedValue) == 57) // Theory Subject
                                //    subExam = "S1T1";
                                //else if (Convert.ToInt32(ddlExam.SelectedValue) == 58)// Theory Subject
                                //    subExam = "S1T2";

                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                                studids += lbl.ToolTip + ",";

                                CheckBox chkS1T1MarksLock = gvStudent.HeaderRow.FindControl("chkS1T1MarksLock") as CheckBox;
                                if (chkS1T1MarksLock.Checked == true)
                                    lock_status = 1;
                                else
                                    lock_status = 0;
                            }
                            
                        }
                        int sectionno = Convert.ToInt32(hdfSection.Value);
                        int courseno = Convert.ToInt32(lblCourse.ToolTip);
                        string[] course = lblCourse.Text.Split('~');
                        string ccode = course[0].Trim();
                        examtype = "S";

                        if (!string.IsNullOrEmpty(studids))
                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryForSubExam(Convert.ToInt32(ddlSession.SelectedValue),courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["ExamNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue),Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                    }
                }
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(updMarkEntry, "Marks Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }
                else
                {
                    objCommon.DisplayMessage(updMarkEntry, "Marks Saved Successfully.", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }
            }
            else
                objCommon.DisplayMessage("Error in Saving Marks!", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[2].Visible == true) return flag;
        return false;
    }

    private bool CheckMarks(int lock_status)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;

            for (int j = 3; j < gvStudent.Columns.Count; j++)    //columns
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                {
                    if (gvStudent.Columns[j].Visible == true)
                    {
                        if (j == 3) //TA MARKS
                        {
                            lbl = gvStudent.Rows[i].Cells[j].FindControl("lblMarks") as Label;      //Max Marks 
                            txt = gvStudent.Rows[i].Cells[j].FindControl("txtMarks") as TextBox;    //Marks Entered 
                            maxMarks = lbl.Text.Trim();
                            marks = txt.Text.Trim();

                            if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                            {
                                if (txt.Text == "")
                                {
                                    if (lock_status == 1)
                                    {
                                        objCommon.DisplayMessage(updMarkEntry, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                        //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Check for Marks entered greater than Max Marks
                                    if (Convert.ToDouble(txt.Text) > Convert.ToDouble(lbl.Text))
                                    {
                                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                    else if (Convert.ToDouble(txt.Text) < 0)
                                    {
                                        //Note : 401 for Absent and Not Eligible
                                        if (Convert.ToDouble(txt.Text) == -1 || Convert.ToDouble(txt.Text) == -2 || Convert.ToDouble(txt.Text) == -3 || Convert.ToDouble(txt.Text) == -4)
                                        {
                                        }
                                        else
                                        {
                                            ShowMessage("Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.");
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (txt.Enabled == true)
                                {
                                    if (lock_status == 1)
                                    {
                                        ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (flag == false) break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    #endregion

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Enabled = false;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value;

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }

    protected void btnTAReport_Click(object sender, EventArgs e)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);

        if (Convert.ToInt32(subid) == 1)
        {
            this.ShowReportForMID("TAMarksListReport", "rptMarksListForMID.rpt");
        }
        else
        {
            this.ShowReportForMID("TAPracMarksListReport", "rptMarksListForMIDPrac.rpt");
        }
    }

    private void ShowReportForMID(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]);

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }

    protected void btnConsolidateReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("MarksListReport", "rptMarksList.rpt");
    }

    protected void ddlSession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT", "DISTINCT SUBID", "DBO.FN_DESC('SUBJECTTYPE',SUBID)SUBNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " and  UA_NO_PRAC =" + Convert.ToInt32(Session["userno"]) + "OR ISNULL(UA_NO,0)=" + Convert.ToInt32(Session["userno"]), "SUBID");

            //this.ShowCourses();
            //this.GetStatus();
            //this.MarkEntryStatus(Convert.ToInt32(ddlSession.SelectedValue));
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.ShowCourses();
        this.ShowCoursesIA();
        //this.GetStatus();
        this.GetStatus_IA();
    }

    protected void ddlExam_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0) 
        {
            ShowStudents();
        }
        else
        {
            objCommon.DisplayMessage(updMarkEntry, "Please Select Exam!!", this.Page);
            ddlExam.Focus();
        }
    }

    private void ShowStudents()
    {
        try
        {

            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;

            gvStudent.Columns[3].Visible = false;

            colS1T1Mark = string.Empty;
            colS1T2Mark = string.Empty;
            colS1T3Mark = string.Empty;
            colS1T4Mark = string.Empty;
            colS1T5Mark = string.Empty;
            colS2T1Mark = string.Empty;
            colS2T2Mark = string.Empty;
            ViewState["StudCount"] = 0;
            
            //string[] Examno = ddlExam.SelectedValue.Split('-');
            int MExamNo = Convert.ToInt32(ddlExam.SelectedValue);// Convert.ToInt32(Examno[0]);
            ViewState["ExamNo"] = MExamNo;
            //dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToInt32(Examno[0]), Convert.ToInt32(ViewState["COURSENO"]));
            dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_IA(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), MExamNo, Convert.ToInt32(ViewState["COURSENO"]));

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();
                    ViewState["StudCount"] = dsStudent.Tables[0].Rows.Count;
                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();

                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Enabled = false;
                        objCommon.DisplayMessage(updMarkEntry, "Selected Exam Not Applicable for Mark Entry!!", this.Page);
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }

                    BindJS();

                    pnlSelection.Visible = false;
                    pnlMarkEntry.Visible = true;
                    pnlStudGrid.Visible = true;
                    lblStudents.Visible = true;
                    btnBack.Visible = true; 
                    //btnSave.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "FreezHeader();", true);
                }
                else
                {
                    objCommon.DisplayMessage(updMarkEntry, "Students Not Found..!!", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    private void GetStatus()
    {
        DataSet ds = null;

        ds = objMarksEntry.GetPracticalCourse_MarksEntryStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
      
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GVEntryStatus.DataSource = ds;
            GVEntryStatus.DataBind();
        }
        else
        {
            GVEntryStatus.DataSource = null;
            GVEntryStatus.DataBind();
            lvCourse.Visible = false;
            objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page); 
        }
    }

    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {

            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (i == 0)
                    {
                        //lnkbtnCourseWISE
                        LinkButton cu = gridView.Rows[rowIndex].FindControl("lnkbtnCourse") as LinkButton;
                        LinkButton prev = gridView.Rows[rowIndex + 1].FindControl("lnkbtnCourse") as LinkButton;

                        if (cu.Text == prev.Text)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                   previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }

                    }
                    if (i == 4)
                    {
                        Button cu1 = gridView.Rows[rowIndex].FindControl("btnCourseWISE") as Button;
                        Button prev1 = gridView.Rows[rowIndex + 1].FindControl("btnCourseWISE") as Button;
                        if (cu1.ToolTip == prev1.ToolTip)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                                              previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }
                    }
                   
                }
            }
        }
    }

    protected void GVEntryStatus_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(GVEntryStatus);
    }

    protected void GVEntryStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
            {
                e.Row.Cells[0].Style.Add("padding-top", "6%");
                e.Row.Cells[1].Style.Add("padding-top", "6%");
                e.Row.Cells[2].Style.Add("padding-top", "6%");
                e.Row.Cells[3].Style.Add("padding-top", "6%");
                e.Row.Cells[4].Style.Add("padding-top", "6%");
            }

            switch (e.Row.Cells[2].Text)
            {
                case "PENDING":
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].Enabled = false;
                    break;
                case "COMPLETED":
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                    break;
                case "IN PROGRESS":
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Orange;
                    e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                    break;
                default:
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                    break;
            };

        }


    }

    private void ShowReportForcourse(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
    
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
                ",@P_COURSENO=" + Convert.ToInt32(ViewState["Courseno"]) + ",@P_SECTIONNO=" + ViewState["section"].ToString() +
                ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_UANO=" + Convert.ToInt16(Session["userno"]) +
                ",@P_SEMESTERNO=" + Convert.ToInt16(ViewState["semno"]);
   

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updMarkEntry, this.updMarkEntry.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnreportexamwise_Click(object sender, EventArgs e)
    {
        Button btnSelect = (sender as Button);
        ViewState["sec"] = btnSelect.CommandName;
        ViewState["CCODE"] = btnSelect.ToolTip;
        ViewState["Exam"] = btnSelect.CommandArgument;
        Button btn = sender as Button;
        ViewState["corseno"] = Convert.ToInt32((btn.Parent.FindControl("hdncorseno") as HiddenField).Value);
        ViewState["semester"] = Convert.ToInt32((btn.Parent.FindControl("hdnsemester") as HiddenField).Value);
        ViewState["fldName"] = Convert.ToString((btn.Parent.FindControl("hdnExamField") as HiddenField).Value);

        ViewState["hffldname"] = Convert.ToString((btn.Parent.FindControl("hffldname") as HiddenField).Value);
        ViewState["SubExamNo"] = objCommon.LookUp("ACD_STUDENT_TEST_MARK T INNER JOIN ACD_SCHEME SC ON(T.SCHEME_NO=SC.SCHEMENO) INNER JOIN ACD_SUBEXAM_NAME S ON(S.PATTERNNO=SC.PATTERNNO AND T.SUB_ID=S.SUBEXAM_SUBID)", "DISTINCT SUBEXAMNO", "S.FLDNAME='" + ViewState["hffldname"].ToString() + "' AND COURSENO=" + ViewState["corseno"].ToString() + " and CCODE='" + ViewState["CCODE"].ToString() + "' AND SECTIONNO=" + ViewState["sec"].ToString() + " AND T.SEMESTERNO=" + ViewState["semester"].ToString() + " AND SESSIONNO=" + ddlSession.SelectedValue + "");
        this.ShowReportMarksEntry("MarksListReport", "rptSubExamMarksList.rpt");
       
    }

    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {
        string fldname = string.Empty;
  
        fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + ViewState["Exam"].ToString() + "'");

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
     
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
           ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["CCODE"].ToString() +
           ",@P_SECTIONNO=" + ViewState["sec"].ToString() + ",@P_SUBID=" + ddlSubjectType.SelectedValue +
           ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["semester"]) +
           ",@P_COURSENO=" + Convert.ToInt32(ViewState["corseno"]) +
           ",@P_SUBEXAM=" + ViewState["hffldname"] +
           ",@P_SUBEXAMNO=" + Convert.ToInt32(ViewState["SubExamNo"]);
       

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updMarkEntry, this.updMarkEntry.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnCourseWISE_Click(object sender, EventArgs e)
    {
        Button btnSelectcourse = (sender as Button);
        ViewState["semno"] = Convert.ToInt32((btnSelectcourse.Parent.FindControl("hdnsem") as HiddenField).Value);
        ViewState["section"] = btnSelectcourse.CommandArgument;
        ViewState["Courseno"] = btnSelectcourse.CommandName;
       
        this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks_SubExam.rpt");
    }

    private bool CheckMarks(int lock_status, string MarksType)
    {
        bool flag = true;
        try
        {
            Label lblmax;
            Label lblmin;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;

            for (int j = 3; j <= gvStudent.Columns.Count; j++)    //columns
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                {
                    if (MarksType.Trim() == "S1T1")
                    {
                        if (gvStudent.Columns[3].Visible == true)
                        {
                            if (j == 3)
                            {
                                lblmax = gvStudent.Rows[i].Cells[j].FindControl("lblMaxS1T1Marks") as Label;      //Max Marks 
                                lblmin = gvStudent.Rows[i].Cells[j].FindControl("lblMinS1T1Marks") as Label;      //Min Marks 
                                txt = gvStudent.Rows[i].Cells[j].FindControl("txtS1T1Marks") as TextBox;          //Marks Entered 
                                if (!txt.Text.Trim().Equals(string.Empty) && !lblmax.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                                {
                                    if (txt.Text == "")
                                    {
                                        if (lock_status == 1)
                                        {
                                           // ShowMessage("");
                                            objCommon.DisplayMessage(updMarkEntry, "Marks entry not completed!! Please enter the marks for all students.", this.Page);
                                            CheckBox chkS1T1MarksLock = gvStudent.HeaderRow.FindControl("chkS1T1MarksLock") as CheckBox;
                                            chkS1T1MarksLock.Checked = false;
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //Check for Marks entered greater than Max Marks
                                        if (Convert.ToDouble(txt.Text) > Convert.ToDouble(lblmax.Text) || Convert.ToDouble(txt.Text) < Convert.ToDouble(lblmin.Text) || Convert.ToDouble(txt.Text) == -1 || Convert.ToDouble(txt.Text) == -2 || Convert.ToDouble(txt.Text) == -3 || Convert.ToDouble(txt.Text) == -4)
                                        {
                                            if (Convert.ToDouble(txt.Text) == -1 || Convert.ToDouble(txt.Text) == -2 || Convert.ToDouble(txt.Text) == -3 || Convert.ToDouble(txt.Text) == -4)
                                            { }
                                            else
                                            {
                                                CheckBox chkS1T1MarksLock = gvStudent.HeaderRow.FindControl("chkS1T1MarksLock") as CheckBox;
                                                chkS1T1MarksLock.Checked = false;
                                                //ShowMessage("Please Enter Marks in the Range of [" + lblmin.Text + "]" + " to " + "[" + lblmax.Text + "]");
                                                objCommon.DisplayMessage(updMarkEntry, "Please Enter Marks in the Range of [" + lblmin.Text + "]" + " to " + "[" + lblmax.Text + "]", this.Page);
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (txt.Enabled == true)
                                    {
                                        if (lock_status == 1)
                                        {
                                            objCommon.DisplayMessage(updMarkEntry, "Marks entry not completed!! Please enter the marks for all students.", this.Page);
                                            txt.Focus();
                                            CheckBox chkS1T1MarksLock = gvStudent.HeaderRow.FindControl("chkS1T1MarksLock") as CheckBox;
                                            chkS1T1MarksLock.Checked = false;
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void ShowCoursesIA()
    {
        try
        {
            DataSet ds = objMarksEntry.GetCourseForTeacher_IA(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds.Tables[0];
                    lvCourse.DataBind();
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page);
                }
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.ShowCoursesIA --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void GetStatus_IA()
    {
        DataSet ds = null;

        ds = objMarksEntry.GetPracticalCourse_MarksEntryStatus_IA(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GVEntryStatus.DataSource = ds;
            GVEntryStatus.DataBind();
        }
        else
        {
            GVEntryStatus.DataSource = null;
            GVEntryStatus.DataBind();
            lvCourse.Visible = false;
            objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page);
        }
    }

    private void MarkEntryStatus_IA(int SessionNo)
    {
        try
        {
            //
            divstatus.Visible = false;
            DataSet dsExams = objMarksEntry.GetONExamsActivityBySubjectType_IA(SessionNo, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
            if (dsExams.Tables[0].Rows.Count > 0)
            {
                string status = string.Empty;
                divstatus.Visible = true;
                DataTableReader dtr = dsExams.Tables[0].CreateDataReader();

                while (dtr.Read())
                {
                    if (status == string.Empty)
                        status = "This is to inform you that Practical ";
                    else
                        status += " & Practical ";

                    status += dtr["EXAMNAME"] + " Mark Entry activity is going to be stopped on " + dtr["ENDDATE"];
                }
                dtr.Close();

                lblstatusmark.Text = status;
            }
        }
        catch (Exception Ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.MarkEntryStatus_IA --> " + Ex.Message + " " + Ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //added By Dileep Kare on 22/02/2021 for Calculate Best of Two Average Marks. 

    protected void btnConsolidateMark_Click(object sender, EventArgs e)
    {
        CustomStatus cs = CustomStatus.Error;
        try
        {
            string Schemeno = objCommon.LookUp("ACD_STUDENT_RESULT ", "DISTINCT ISNULL(SCHEMENO,0)", "COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString()) + " AND CCODE='" + ViewState["CCODE"].ToString() + "' AND SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"].ToString()) + " AND UA_NO=" + Convert.ToInt16(Session["userno"].ToString()));
            int Sessionno = ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
            // int Schemeno   =0;
            int Semesterno = Convert.ToInt16(ViewState["sem"].ToString());
            int Sectionno = Convert.ToInt16(hdfSection.Value);
            int Subid = ddlSubjectType.SelectedIndex > 0 ? Convert.ToInt32(ddlSubjectType.SelectedValue) : 0;
            int Courseno = Convert.ToInt32(ViewState["COURSENO"]);
            string CCODE = ViewState["CCODE"].ToString();
            int Ua_NO = Convert.ToInt32(Session["userno"]);

            cs = (CustomStatus)objMarksEntry.Calculate_Consolidate_Marks(Sessionno, Convert.ToInt32(Schemeno), Semesterno, Sectionno, Subid, Courseno, CCODE, Ua_NO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(Page, "Best of Two Average Marks Calculate Successfully.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.btnConsolidateMark_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}


