//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ACADEMIC - MARK ENTRY                                           
// CREATION DATE : 14-OCT-2009                                                     
// CREATED BY    : NIRAJ D .PHALKE                                                 
// MODIFIED BY   : 17-NOV-2009                                                     
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
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

public partial class Academic_MarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
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
                string date = objCommon.LookUp("SESSION_ACTIVITY", "END_DATE", "ACTIVITY_NO=1 AND ISNULL(STARTED,0)=1");
                if (date != string.Empty || date != "")
                {
                    DateTime statusdate = Convert.ToDateTime(date);
                    string status = statusdate.ToString("d");
                    if (status != string.Empty || status != "")
                    {
                        divstatus.Visible = true;
                        lblstatusmark.Text = status;
                    }
                }
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP 2 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND FLOCK=0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) cross apply dbo.splitstring(COLLEGE_IDS,',') d WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%') AND data in (select data  from dbo.splitstring('" + Session["college_nos"].ToString() + "'))", "");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_IDS LIKE ('%" + Session["college_nos"].ToString() + "%'))", "");


                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) cross apply dbo.splitstring(COLLEGE_IDS,',') d WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND d.data in(select data from dbo.splitstring('" + Session["college_nos"].ToString() + "',',')))", ""); 
                ddlSession.SelectedIndex = 1;
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT", "DISTINCT SUBID", "DBO.FN_DESC('SUBJECTTYPE',SUBID)SUBNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " and  (UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + " OR UA_NO_TUTR = " + Convert.ToInt32(Session["userno"]) + ") AND SUBID > 0", "SUBID");
                if (ddlSession.SelectedIndex == 1)
                    objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT", "DISTINCT SUBID", "DBO.FN_DESC('SUBJECTTYPE',SUBID)SUBNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " and  UA_NO_PRAC =" + Convert.ToInt32(Session["userno"]) + "OR ISNULL(UA_NO,0)=" + Convert.ToInt32(Session["userno"]), "SUBID");
               // else
                  //  ddlSubjectType.Items.Insert(0, new ListView("Please Select","Please Select"));

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                if (ddlSession.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(updMarkEntry, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                }
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void ShowCourses()
    {
        DataSet ds = objMarksEntry.GetCourseForTeacher(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                // lblStatus.Visible = true;
                // lblStatus.Text = "Marks Entry Status with Exam Name Coursewise.";
                // lblStatus.ForeColor = System.Drawing.Color.Red;
                lvCourse.DataSource = ds.Tables[0];
                lvCourse.DataBind();
            }
            else
            {
                // lblStatus.Text = "";
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page); //lblStatus.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page); //lblStatus.Visible = false;
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================

            //Added Mahesh on Dated 11/02/2020
            ddlSubExam.Items.Clear();
            ddlSubExam.Items.Add("Please Select");
            ddlSubExam.SelectedItem.Value = "0";

            LinkButton lnk = sender as LinkButton;
            if (!lnk.ToolTip.Equals(string.Empty))
            {
                DataSet CourseEligibilityCheckCount = null;
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                ViewState["COURSENO"] = lblCourse.ToolTip;
                ViewState["CCODE"] = (objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO='" + lblCourse.ToolTip + "'"));
                //CourseEligibilityCheckCount = objMarksEntry.CourseEligibilityCheck(Convert.ToInt32(ddlSession.SelectedValue), ViewState["CCODE"].ToString(), Convert.ToInt32(lnk.ToolTip));
                CourseEligibilityCheckCount = objMarksEntry.CourseEligibilityCheck_IA(Convert.ToInt32(ddlSession.SelectedValue), ViewState["CCODE"].ToString(), Convert.ToInt32(lnk.ToolTip));

                if (CourseEligibilityCheckCount.Tables[0].Rows.Count > 0)
                {
                    string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                    hdfSection.Value = sec_batch[0].ToString();

                    ddlSession2.Items.Clear();
                    ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                    hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
                    //GET THE EXAMS THAT ARE ON 
                    //DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

                    //*****************
                    //new added on [10-sep-2016]
                    ////int CourseNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COURSENO", "CCODE='" + lblCourse.ToolTip + "'"));
                    int CourseNo = 0;
                    LinkButton btn = sender as LinkButton;
                    CourseNo = Convert.ToInt32((btn.Parent.FindControl("hdnfld_courseno") as HiddenField).Value);
                    ViewState["sem"] = Convert.ToInt32((btn.Parent.FindControl("hdnsem") as HiddenField).Value);
                    hdfSemNo.Value = Convert.ToString(ViewState["sem"]);
                    //************* Commented below method by S.patil for getting exam names as per subject type.
                    //DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

                    //*************Added below method by S.patil for getting exam names as per subject type.
                    DataSet dsExams = null;
                    if (ddlSubjectType.SelectedValue != "2")
                    {
                        dsExams = objMarksEntry.GetONExamsActivityBySubjectType_IA_EXM(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
                    }
                    else
                    {  //practical Exam or subjectype=2
                        dsExams = objMarksEntry.GetONExamsActivityBySubjectType_IA_EXM(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
                        if (dsExams.Tables[0].Rows.Count > 0)
                        {
                            int PatternNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE  C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO)", "DISTINCT PATTERNNO", "COURSENO=" + CourseNo + ""));
                           // dsExams = objMarksEntry.GetONPracticalExamsBySubjectType(PatternNo);

                            dsExams = objMarksEntry.GetONPracticalExamsBySubjectType_Prac(PatternNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), int.Parse(Request.QueryString["pageno"].ToString()));
                        }
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
                                #region COMMENTED CODE

                                /*if (ddlSubjectType.SelectedValue == "2")   //Practical
                            {
                                //Add to Exam List Only if Practical, Oral, TermWork 
                                if (!dtr["FLDNAME2"].ToString().ToUpper().Contains("S3") ||
                                    !dtr["FLDNAME2"].ToString().ToUpper().Contains("S4"))
                                {
                                    ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME2"].ToString()));
                                }
                            }
                            else if (ddlSubjectType.SelectedValue == "1" || ddlSubjectType.SelectedValue == "3")   // Theory, Audit
                            {
                                //Add to Exam List Only if  Test Exam, End sem
                                if (dtr["FLDNAME2"].ToString().ToUpper().Contains("S1") ||
                                    dtr["FLDNAME2"].ToString().ToUpper().Contains("S2") ||
                                    dtr["FLDNAME2"].ToString().ToUpper().Contains("S5") ||
                                    dtr["FLDNAME2"].ToString().ToUpper().Contains("EXTERMARK"))
                                {
                                    ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME2"].ToString()));
                                }
                            }*/
                                #endregion

                                if (ddlSubjectType.SelectedIndex > 0)
                                {
                                    //ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME"].ToString()));

                                    //foreach (ListItem item in ddlExam.Items)
                                    //{
                                    //    //ddlExam.Attributes.Add("", dtr["EXAMNO"].ToString());
                                    //    item.Attributes.Add("title", dtr["EXAMNO"].ToString());
                                    //}
                                    string examname = "";
                                    examname = dtr["EXAMNAME"].ToString();

                                    //if (Convert.ToInt32(ddlSubjectType.SelectedValue) != 4)
                                    //{
                                    //    examname = dtr["EXAMNAME"].ToString();
                                    //}
                                    //else
                                    //{
                                    //    if (dtr["FLDNAME"].ToString() == "EXTERMARK")
                                    //    {
                                    //        examname = "External Marks";
                                    //    }
                                    //    else
                                    //    {
                                    //        examname = dtr["EXAMNAME"].ToString();
                                    //    }
                                    //}
                                    ListItem LI = new ListItem(examname.ToString(), dtr["FLDNAME"].ToString());
                                    LI.Attributes.Add("Title", dtr["EXAMNO"].ToString());
                                    ddlExam.Items.Add(LI);

                                    //ddlTooltip[j-1] = ddlExam.Items[j].Attributes["Title"];

                                    //ddlTooltip.Add(new KeyValuePair<string, int>(dtr["FLDNAME"].ToString(), Convert.ToInt32(dtr["EXAMNO"])));
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

                        // FillControlSheetNo();
                        //ShowStudents();

                        if (ddlExam.Items.Count > 0)
                        {
                            pnlSelection.Visible = false;
                            pnlMarkEntry.Visible = true;
                            pnlStudGrid.Visible = false;
                            btnBack.Visible = true;
                            btnSave.Visible = false;
                            btnLock.Visible = false;
                            lblStudents.Visible = false;
                            // btnReport.Visible = false;
                            // btnReport.Enabled = false;
                            // btncoursereport.Visible = false;
                        }
                    }
                    else
                    {
                        //gvStudent.DataSource = null;
                        //gvStudent.DataBind(); 
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
                objUCommon.ShowError(Page, "Academic_MarkEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      
        // Added by Nikhil V.Lambe on 22/02/2021 for showing the error message if one the marks textbox is empty.
        //for (int i = 0; i < gvStudent.Rows.Count; i++)
        //{
           
        //    TextBox txt = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
        //    if (txt.Text.Trim().Equals(string.Empty))
        //    {
        //        objCommon.DisplayMessage(this.Page, "Please Enter "+ddlExam.SelectedItem+" Marks", this.Page);
        //        return;
        //    }
        //}
        SaveAndLock(0, Convert.ToInt32(hdfSemNo.Value));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        ddlSubExam.SelectedIndex = 0;
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        btnAutoGenAttendance.Visible = false;
        //GetStatus();
        GetStatus_IA();
    }

    #region Private/Public Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
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
            //check for if any exams on
            if (ddlExam.SelectedIndex > 0)
            {
                //Check for lock and null marks
                if (CheckMarks(lock_status) == false)
                {
                    return;
                }
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
                    studids += lbl.ToolTip + ",";

                    //Gather Exam Marks 
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                }
                int sectionno = Convert.ToInt32(hdfSection.Value);
                int courseno = Convert.ToInt32(lblCourse.ToolTip);
                string[] course = lblCourse.Text.Split('~');
                string ccode = course[0].Trim();
                string subExam = string.Empty;
                string ExamName = string.Empty;
                //Added Mahesh on Date 10/02/2020 
                if (ddlExam.Enabled == true && ddlExam.SelectedIndex > 0)
                {
                    string[] exam = ddlExam.SelectedValue.Split('-');
                   
                    if (exam.Length <= 1)
                    {
                         ExamName = exam[0];
                    }
                    else
                          ExamName = exam[1];

                    if (ExamName.StartsWith("S"))
                        examtype = "S";
                    else if (ExamName.StartsWith("E"))
                        examtype = "E";
                }
                else
                {
                    if (ddlExam.SelectedValue.StartsWith("S"))
                        examtype = "S";
                    else if (ddlExam.SelectedValue.StartsWith("E"))
                        examtype = "E";
                }
                string examname = string.Empty;
                if (ddlExam.SelectedValue.Length > 2 && ddlExam.SelectedIndex > 0)
                {
                    if (ddlExam.SelectedIndex > 0)
                    {
                        string[] exam = ddlExam.SelectedValue.Split('-');
                        //string ExamName = exam[1];
                        if (exam.Length <= 1)
                        {
                            ExamName = exam[0];
                        }
                        else
                            ExamName = exam[1];

                        examname = ExamName;//ExamName.Substring(2); //ddlExam.SelectedValue.Substring(2);
                    }
                    else
                    {
                        examname = ddlExam.SelectedValue.Substring(2);
                    }
                }
                else if (ddlExam.SelectedIndex > 0)
                {
                    if (ddlSubExam.SelectedIndex > 0)
                    {
                        //examname = ddlExam.SelectedValue;
                        string[] exam = ddlExam.SelectedValue.Split('-');
                      //  string ExamName = exam[1];
                        if (exam.Length <= 1)
                        {
                            ExamName = exam[0];
                        }
                        else
                            ExamName = exam[1];

                        examname = ExamName.Substring(2);
                    }
                    else
                    {
                        examname = ddlExam.SelectedValue;
                    }
                }

                CustomStatus cs;
                if (ddlSubExam.Enabled == true && ddlSubExam.SelectedIndex > 0)
                {
                    subExam = ddlSubExam.SelectedValue;
                    // cs = (CustomStatus)objMarksEntry.UpdateMarkEntryForSubExam(Convert.ToInt32(ddlSession.SelectedValue),
                    //courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["ExamNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue),
                    //Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryForPracExam(Convert.ToInt32(ddlSession.SelectedValue),
                   courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["ExamNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue),
                   Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                }
                else
                {
                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage(updMarkEntry, "Marks Locked Successfully!!!", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    }
                    else
                        objCommon.DisplayMessage(updMarkEntry, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);

                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    // btnReport.Enabled = true;
                    if (gvStudent.Columns[3].Visible == true)
                    {
                        ShowStudents(1);
                    }
                    else
                    {
                        ShowStudents(0);
                    }

                    //DataSet dsStudent = null;
                    //if (ddlExam.SelectedValue.Length > 2)
                    //    dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), course[0].Trim(), Convert.ToInt32(ddlSubjectType.SelectedValue), ddlExam.SelectedValue.Substring(2), Convert.ToInt32(lblDiv.ToolTip), Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(lbldept.ToolTip), Convert.ToInt32(lblSemesterno.ToolTip), Convert.ToInt32(lblBatch.ToolTip));
                    //else
                    //    dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), course[0].Trim(), Convert.ToInt32(ddlSubjectType.SelectedValue), ddlExam.SelectedValue.ToString(), Convert.ToInt32(lblDiv.ToolTip), Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(lbldept.ToolTip), Convert.ToInt32(lblSemesterno.ToolTip), Convert.ToInt32(lblBatch.ToolTip));

                    //if (dsStudent != null && dsStudent.Tables.Count > 0)
                    //{
                    //    if (dsStudent.Tables[0].Rows.Count > 0)
                    //    {
                    //        gvStudent.DataSource = dsStudent;
                    //        gvStudent.DataBind();
                    //    }
                    //}
                }
                else
                    objCommon.DisplayMessage("Error in Saving Marks!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[4].Visible == true) return flag;
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

                                ////Check for Marks entered greater than Max Marks
                                //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                                //{
                                //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                                //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                                //    {
                                //    }
                                //    else
                                //    {
                                //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                //        txt.Focus();
                                //        flag = false;
                                //        break;
                                //    }
                                //}
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

                    #region Not Needed Commented by Manish
                    //if (gvStudent.Columns[4].Visible == true)
                    //{
                    //    if (j == 3) //CT/FE MARKS
                    //    {
                    //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT1Marks") as Label;      //Max Marks 
                    //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT1Marks") as TextBox;    //Marks Entered 
                    //        maxMarks = lbl.Text.Trim();
                    //        marks = txt.Text.Trim();

                    //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                    //        {
                    //            if (txt.Text == "")
                    //            {
                    //                ShowMessage("Marks Entry Not Completed!!!");
                    //                txt.Focus();
                    //                flag = false;
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                //Check for Marks entered greater than Max Marks
                    //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                    //                {
                    //                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                    //                    if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                    //                    {
                    //                    }
                    //                    else
                    //                    {
                    //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                    //                        txt.Focus();
                    //                        flag = false;
                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            ////Check for Marks entered greater than Max Marks
                    //            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                    //            //{
                    //            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                    //            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                    //            //    {
                    //            //    }
                    //            //    else
                    //            //    {
                    //            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                    //            //        txt.Focus();
                    //            //        flag = false;
                    //            //        break;
                    //            //    }
                    //            //}
                    //        }
                    //        else
                    //        {
                    //            if (txt.Enabled == true)
                    //            {
                    //                if (lock_status == 1)
                    //                {
                    //                    ShowMessage("Marks Entry Not Completed!!!");
                    //                    txt.Focus();
                    //                    flag = false;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    //if (gvStudent.Columns[5].Visible == true)
                    //{

                    //    if (j == 4) //TA MARKS
                    //    {
                    //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT2Marks") as Label;      //Max Marks 
                    //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT2Marks") as TextBox;    //Marks Entered 
                    //        maxMarks = lbl.Text.Trim();
                    //        marks = txt.Text.Trim();

                    //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                    //        {
                    //            if (txt.Text == "")
                    //            {
                    //                ShowMessage("Marks Entry Not Completed!!!");
                    //                txt.Focus();
                    //                flag = false;
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                //Check for Marks entered greater than Max Marks
                    //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                    //                {
                    //                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                    //                    if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                    //                    {
                    //                    }
                    //                    else
                    //                    {
                    //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                    //                        txt.Focus();
                    //                        flag = false;
                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            ////Check for Marks entered greater than Max Marks
                    //            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                    //            //{
                    //            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                    //            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                    //            //    {
                    //            //    }
                    //            //    else
                    //            //    {
                    //            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                    //            //        txt.Focus();
                    //            //        flag = false;
                    //            //        break;
                    //            //    }
                    //            //}
                    //        }
                    //        else
                    //        {
                    //            if (txt.Enabled == true)
                    //            {
                    //                if (lock_status == 1)
                    //                {
                    //                    ShowMessage("Marks Entry Not Completed!!!");
                    //                    txt.Focus();
                    //                    flag = false;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //if (gvStudent.Columns[7].Visible == true)
                    //{
                    //    if (j == 6) //TA-Pr MARKS
                    //    {
                    //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblTAPrMarks") as Label;      //Max Marks 
                    //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtTAPrMarks") as TextBox;    //Marks Entered 
                    //        maxMarks = lbl.Text.Trim();
                    //        marks = txt.Text.Trim();

                    //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                    //        {
                    //            if (txt.Text == "")
                    //            {
                    //                ShowMessage("Marks Entry Not Completed!!!");
                    //                txt.Focus();
                    //                flag = false;
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                //Check for Marks entered greater than Max Marks
                    //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                    //                {
                    //                    //Note : 401 for Absent
                    //                    if (Convert.ToInt16(txt.Text) == -1)
                    //                    {
                    //                    }
                    //                    else
                    //                    {
                    //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                    //                        txt.Focus();
                    //                        flag = false;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (txt.Enabled == true)
                    //            {
                    //                if (lock_status == 1)
                    //                {
                    //                    ShowMessage("Marks Entry Not Completed!!!");
                    //                    txt.Focus();
                    //                    flag = false;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    #region comment
                    //}
                    //else
                    //{
                    //    if (txt.Enabled == true)
                    //    {
                    //        //Grade marks
                    //        if (txt.Text.Trim().Equals("A") || txt.Text.Trim().Equals("B") || txt.Text.Trim().Equals("C") || txt.Text.Trim().Equals("D"))
                    //        {
                    //        }
                    //        else
                    //        {
                    //            if (lock_status == 1)
                    //            {
                    //                ShowMessage("Marks Entry Not Completed!!!");
                    //                txt.Focus();
                    //                flag = false;
                    //                break;
                    //            }
                    //            //else
                    //            //{
                    //            //    ShowMessage("Please Enter Marks in Range of A to D!!");
                    //            //    txt.Focus();
                    //            //    flag = false;
                    //            //    break;
                    //            //}
                    //        }
                    //    }
                    //}
                    #endregion

                    if (flag == false) break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    #endregion

    protected void btnLock_Click(object sender, EventArgs e)
    {
        // Added by Nikhil V.Lambe on 22/02/2021 for showing the error message if one the marks textbox is empty.
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {           
            TextBox txt = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
            if (txt.Text.Trim().Equals(string.Empty))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter " + ddlExam.SelectedItem + " Marks", this.Page);
                return;
            }
        }
        //1 - means lock marks
        SaveAndLock(1, Convert.ToInt32(hdfSemNo.Value));
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Enabled = false;
        btnLock.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //this.ShowReport("MarksListReport", "rptMarksList.rpt");
        //this.ShowReport("MarksListReport", "rptMarksListCt.rpt");
        this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW.rpt");//rptMarksList1.rpt
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

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubjectType.SelectedValue.ToString() == "2")
        {
            ddlSubExam.Visible = false;
            rfvSubExam.Enabled = false;
            SubDiv.Visible = false;
        }
        else
        {
            ddlSubExam.Visible = false;
            rfvSubExam.Enabled = false;
            SubDiv.Visible = false;
        }
        //this.ShowCourses();
        this.ShowCourses_IA();
        //this.GetStatus();
        this.GetStatus_IA();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0)
        {
            ShowStudents(0);
            //ShowMessage("No Exams Available for Current Selection");
            //return;
        }
        else
        {
            objCommon.DisplayMessage(updMarkEntry, "Please Select Exam!!", this.Page);
            ddlExam.Focus();
        }
    }

    private void ShowStudents(int status)
    {
        try
        {
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            if (ddlSubjectType.SelectedValue == "2")
            {
                string SubExamName = ddlSubExam.SelectedValue.Replace(" ", "");
                string[] Examno = ddlExam.SelectedValue.Split('-');
                int MExamNo = Convert.ToInt32(Examno[0]);
                string ExamName = Examno[1].ToString();
                //int SubExamNo = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + MExamNo + " AND FLDNAME='" + SubExamName + "'"));
                
                //dsStudent = objMarksEntry.GetStudentsForMarkEntrySubExam(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), ExamName, Convert.ToInt32(ViewState["COURSENO"]), ddlSubExam.SelectedValue.ToString(), SubExamNo);
                //dsStudent = objMarksEntry.GetStudentsForMarkEntry_EXM(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), ExamName, Convert.ToInt16(ViewState["sem"]), Convert.ToInt32(ViewState["COURSENO"]));
                dsStudent = objMarksEntry.GetStudentsForMarkEntry_EXM(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ViewState["sem"]),ExamName, Convert.ToInt32(ViewState["COURSENO"]));
            }
            else
            {
                if (status == 1)
                {
                    //dsStudent = objMarksEntry.GetStudentsForAttendanceMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), ddlExam.SelectedValue.ToString(), Convert.ToInt32(ViewState["COURSENO"]));
                    dsStudent = objMarksEntry.GetStudentsForAttendanceMarkEntry_IA_EXM(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), ddlExam.SelectedValue.ToString(), Convert.ToInt32(ViewState["COURSENO"]));

                }
                else
                {
                    //dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), ddlExam.SelectedValue.ToString(), Convert.ToInt32(ViewState["COURSENO"]));

                    dsStudent = objMarksEntry.GetStudentsForMarkEntry_IA_EXM(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), ddlExam.SelectedValue.ToString(), Convert.ToInt32(ViewState["COURSENO"]));
                }
            }
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    if (ddlExam.SelectedValue == "EXTERMARK")
                    {
                        gvStudent.Columns[2].Visible = false;
                        gvStudent.Columns[3].Visible = false;

                        //Added Mahesh on Dated 23/04/2020 for Decode Number or Enroll No/Roll No. Showing as per reff table staus.
                        bool EndSemBy_Decode_Or_Enroll = Convert.ToBoolean(objCommon.LookUp("REFF", "ISNULL(ENDSEMBY_DECODE_OR_ENROLL,0) AS ENDSEMBY_DECODE_OR_ENROLL", ""));
                        if (EndSemBy_Decode_Or_Enroll == true)
                        {
                            gvStudent.Columns[1].HeaderText = "Decode No.";
                        }
                        else
                        {
                            gvStudent.Columns[1].HeaderText = "Registration No. / Roll No.";
                            gvStudent.Columns[1].HeaderStyle.CssClass = "HeaderAlign";
                        }
                    }
                    else
                    {
                        if (status == 1)
                        {
                            gvStudent.Columns[3].Visible = true;
                            btnAutoGenAttendance.Enabled = false;

                        }
                        else
                        {
                            gvStudent.Columns[3].Visible = false;
                        }

                        gvStudent.Columns[1].HeaderText = "Registration No. / Roll No."; //Added Mahesh on Dated 23/04/2020
                        gvStudent.Columns[1].HeaderStyle.CssClass = "HeaderAlign";
                        gvStudent.Columns[2].Visible = true;


                    }
                    if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                    {
                        //gvStudent.Columns[3].HeaderText = ddlExam.SelectedItem.Text + " <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]";
                        gvStudent.Columns[4].HeaderText = dsStudent.Tables[0].Rows[0]["EXAMNAME"] + " <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]";
                        gvStudent.Columns[4].Visible = true;
                        gvStudent.Columns[4].HeaderStyle.CssClass = "HeaderAlign";

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                        ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
                    }
                    else
                        gvStudent.Columns[4].Visible = false;
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();

                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Enabled = false; btnLock.Enabled = false;
                        objCommon.DisplayMessage(updMarkEntry, "Selected Exam Not Applicable for Mark Entry!!", this.Page);
                    }
                    else
                    {
                        btnSave.Enabled = true; btnLock.Enabled = true;
                    }
                    if (ViewState["LockStatus"].ToString() == "True")
                    {
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnAutoGenAttendance.Enabled = false;
                    }
                    pnlSelection.Visible = false; pnlMarkEntry.Visible = true; pnlStudGrid.Visible = true; lblStudents.Visible = true;
                    btnBack.Visible = true; btnSave.Visible = true; btnLock.Visible = true;
                    //btnReport.Visible = true; 
                    //btnReport.Enabled = true;
                    //btncoursereport.Visible = true;
                }
                else
                {
                    if (status == 1)
                    {
                        objCommon.DisplayMessage(updMarkEntry, "Attendance Not Found..!!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updMarkEntry, "Students Not Found..!!", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    //methods to get marks entry status course wise..........added on [14-09-2016]
    private void GetStatus()
    {
        DataSet ds = null;
        //if (ddlSubjectType.SelectedValue == "2")
        //{
        //    ds = objMarksEntry.GetCourse_MarksEntryStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
        //}
        //else
        //{
        
        //ds = objMarksEntry.GetCourse_MarksEntryStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
        ds = objMarksEntry.GetCourse_MarksEntryStatus_IA(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        //}
        ////if (ds != null && ds.Tables.Count > 0)
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //  lblStatus.Visible = true;
            GVEntryStatus.DataSource = ds;
            GVEntryStatus.DataBind();
            //GridView_Row_Merger(GVEntryStatus);
        }
        else
        {
            GVEntryStatus.DataSource = null;
            GVEntryStatus.DataBind();
            lvCourse.Visible = false;
            objCommon.DisplayMessage(updMarkEntry, "No Course Found For This Subject Type.", this.Page); //lblStatus.Visible = false;
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

                        //////Added Mahesh on Dated 11/02/2020

                        ////HiddenField hdnfld_courseno_cu = gridView.Rows[rowIndex].FindControl("hdnfld_courseno") as HiddenField;
                        ////HiddenField hdnfld_courseno_prev = gridView.Rows[rowIndex + 1].FindControl("hdnfld_courseno") as HiddenField;

                        //if (cu.Text == prev.Text)
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
                    //else
                    //{
                    //if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    //{
                    //    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                    //                           previousRow.Cells[i].RowSpan + 1;
                    //    previousRow.Cells[i].Visible = false;
                    //}
                    //}
                }
            }
        }
    }

    protected void GVEntryStatus_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(GVEntryStatus);
    }

    protected void GVEntryStatus_RowDataBound(object sender, GridViewRowEventArgs e)//..........added on [20-09-2016]
    {
        if (ddlSubjectType.SelectedValue == "1" || ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "3" || ddlSubjectType.SelectedValue == "4")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = e.Row.FindControl("lblStatus") as Label;
                if (lbl.Text.Trim() == "PENDING")
                {
                    lbl.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].Enabled = false;
                }
                else if (lbl.Text.Trim() == "COMPLETED")
                {
                    lbl.ForeColor = System.Drawing.Color.Green;
                }
                else if (lbl.Text.Trim() == "IN PROGRESS")
                {
                    lbl.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    lbl.ForeColor = System.Drawing.Color.Black;
                }
                //switch (e.Row.Cells[2].Text)
                //{
                //    case "PENDING":
                //        e.Row.Cells[2].ForeColor = 
                //        e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                //        e.Row.Cells[3].Enabled = false;
                //        break;
                //    case "COMPLETED":
                //        e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
                //        e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                //        break;
                //    case "IN PROGRESS":
                //        e.Row.Cells[2].ForeColor = System.Drawing.Color.Orange;
                //        e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                //        break;
                //    default:
                //        e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                //        e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
                //        break;
                //};
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //hdnfld_courseno
                //hdnsem
                //hdfSectionno
                //hdfCCode
                HiddenField fldname = e.Row.FindControl("hffldname") as HiddenField;
                HiddenField hdnfld_courseno = e.Row.FindControl("hdnfld_courseno") as HiddenField;
                HiddenField hdfCCode = e.Row.FindControl("hdfCCode") as HiddenField;
                HiddenField hdfSectionno = e.Row.FindControl("hdfSectionno") as HiddenField;
                HiddenField hdnsem = e.Row.FindControl("hdnsem") as HiddenField;

                Label lbl = e.Row.FindControl("lblStatus") as Label;
                if (fldname.Value == "S1T1")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T1MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T1MARK IS NOT NULL AND ISNULL(S1T1_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else if (fldname.Value == "S1T2")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T2MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T2MARK IS NOT NULL AND ISNULL(S1T2_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else if (fldname.Value == "S1T3")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T3MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T3MARK IS NOT NULL AND ISNULL(S1T3_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else if (fldname.Value == "S1T4")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T4MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T4MARK IS NOT NULL AND ISNULL(S1T4_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else if (fldname.Value == "S1T5")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T5MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S1T5MARK IS NOT NULL AND ISNULL(S1T5_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else if (fldname.Value == "S2T1")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S2T1MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S2T1MARK IS NOT NULL AND ISNULL(S2T1_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else if (fldname.Value == "S2T2")
                {
                    int FLD = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S2T2MARK IS NOT NULL"));
                    if (FLD > 0)
                    {
                        int FLD1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*) AS CNT", "COURSENO=" + hdnfld_courseno.Value + " and CCODE='" + hdfCCode.Value + "' AND SECTIONNO=" + hdfSectionno.Value + " AND SEMESTERNO=" + hdnsem.Value + " AND SESSIONNO=" + ddlSession.SelectedValue + "  AND S2T2MARK IS NOT NULL AND ISNULL(S2T2_LOCK,0)=1"));
                        if (FLD1 > 0)
                        {
                            lbl.Text = "COMPLETED";
                            lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl.Text = "IN PROGRESS";
                            lbl.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        lbl.Text = "PENDING";
                        lbl.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].Enabled = false;
                    }
                }
                else
                {
                    lbl.Text = "PENDING";
                    lbl.ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].Enabled = false;
                }
            }

        }
    }

    //public void GridView_Row_Merger(GridView gridView)
    //{
    //    for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
    //    {
    //        GridViewRow currentRow = gridView.Rows[rowIndex];
    //        GridViewRow previousRow = gridView.Rows[rowIndex + 1];

    //        for (int i = 0; i < currentRow.Cells.Count; i++)
    //        {
    //            if (currentRow.Cells[i].Text == previousRow.Cells[i].Text)
    //            {
    //                if (previousRow.Cells[i].RowSpan < 2)
    //                    currentRow.Cells[i].RowSpan = 2;
    //                else
    //                    currentRow.Cells[i].RowSpan = previousRow.Cells[i].RowSpan + 1;
    //                previousRow.Cells[i].Visible = false;
    //            }
    //        }
    //    }
    //}

    /* for (int rowIndex = GVEntryStatus.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow currentRow = GVEntryStatus.Rows[rowIndex];
            GridViewRow previousRow = GVEntryStatus.Rows[rowIndex + 1];
            
            //if (currentRow.Cells[0].Text == previousRow.Cells[0].Text)
            //{
            //    currentRow.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 1 ? 2 :
            //        previousRow.Cells[0].RowSpan + 1;
            //    previousRow.Cells[0].Visible = false;
            //}

            for (int i = 0; i < currentRow.Cells.Count; i++)
            {
                if (currentRow.Cells[i].Text == previousRow.Cells[i].Text)
                {
                    if (previousRow.Cells[i].RowSpan < 2)
                    {
                        currentRow.Cells[i].RowSpan = 2;
                    }
                    else
                    {
                        currentRow.Cells[i].RowSpan = previousRow.Cells[i].RowSpan + 1;
                    }
                    previousRow.Cells[i].Visible = false;
                }
            }
        }*/

    //if (e.Row.RowIndex % 4 == 0)
    //{
    //    e.Row.Cells[0].Attributes.Add("rowspan", "4");
    //}
    //else
    //{
    //    e.Row.Cells[0].Visible = false;
    //}
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false;
        lblStudents.Visible = false;
        btnBack.Visible = true;
        btnSave.Visible = false;
        btnLock.Visible = false;
        //btnReport.Visible = false; 
        //btnReport.Enabled = false;

        //btncoursereport.Visible = false;
        if (ddlSubjectType.SelectedValue.ToString() == "2")
        {
            try
            {
                //Dictionary<string, string> EmployeeList = new Dictionary<string, string>();
                Dictionary<string, string> list = (Dictionary<string, string>)Session["ddlTooltip"];
                //for (int i = 0; i < objlt.Count; i++)
                //{
                //    var matches = from val in list.Contains("") where val.Key == 5 select val.Value;

                //}
                //    string s = Session["ddlTooltip"].ToString();
                foreach (KeyValuePair<string, string> author in list)
                {
                    //Console.WriteLine("Key: {0}, Value: {1}", author.Key, author.Value);
                    if (list.ContainsKey(ddlExam.SelectedValue) && author.Key == ddlExam.SelectedValue)
                    {

                        s = Convert.ToInt32(author.Value);
                        ViewState["ExamNo"] = s;
                        break;
                    }
                }
                objCommon.FillDropDownList(ddlSubExam, "ACD_EXAM_NAME E INNER JOIN ACD_SUBEXAM_NAME S ON(E.EXAMNO=S.EXAMNO)", "S.FLDNAME", "SUBEXAMNAME", "E.EXAMNO = " + s, "SUBEXAMNO");
                ddlSubExam.Focus();

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "Activity_ActivityMaster.PopulateSubExam --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else
        {
            ddlSubExam.SelectedValue = "0";
            if (ddlExam.SelectedValue == "S2")
            {
                //Commented by Nikhil V.Lambe on 22/02/2021
                //btnAutoGenAttendance.Visible = true;
                //btnAutoGenAttendance.Enabled = true;
                spanNote.Visible = true;
            }
            else
            {
                btnAutoGenAttendance.Visible = false;
                spanNote.Visible = false;
            }
        }
    }

    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMarks = (TextBox)e.Row.FindControl("txtMarks");
            //Label lblPercentage = (Label)e.Row.FindControl("lblPercentage");
            if (gvStudent.Columns[3].Visible == true)
            {
                txtMarks.ReadOnly = true;
            }
            else
            {
                txtMarks.ReadOnly = false;
            }
        }
    }
    //protected void btncoursereport_Click(object sender, EventArgs e)
    //{

    //}
    protected void btncoursereport_Click(object sender, EventArgs e)
    {
        this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks.rpt");
    }
    private void ShowReportForcourse(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        if (ddlSubjectType.SelectedValue.Equals("2"))
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
                ",@P_COURSENO=" + Convert.ToInt32(ViewState["Courseno"]) + ",@P_SECTIONNO=" + ViewState["section"].ToString() +
                ",@P_SEMNO=" + Convert.ToInt16(ViewState["semno"])+
                ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_UANO=" + Convert.ToInt16(Session["userno"])+
                ",@P_UANAME=" + Convert.ToString(Session["userfullname"]); 
                
                
        }
        else
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
                  ",@P_COURSENO=" + Convert.ToInt32(ViewState["Courseno"]) + ",@P_SECTIONNO=" + ViewState["section"].ToString() +
                  ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_UANO=" + Convert.ToInt16(Session["userno"]) +
                  ",@P_SEMNO=" + Convert.ToInt16(ViewState["semno"]) +
                  ",@P_UANAME=" + Convert.ToString(Session["userfullname"]);
        }

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

        if (ddlSubjectType.SelectedValue.Equals("2")) //Practical
        {
            ViewState["hffldname"] = Convert.ToString((btn.Parent.FindControl("hffldname") as HiddenField).Value);
            ViewState["SubExamNo"] = objCommon.LookUp("ACD_STUDENT_TEST_MARK T INNER JOIN ACD_SCHEME SC ON (T.SCHEME_NO=SC.SCHEMENO) INNER JOIN ACD_SUBEXAM_NAME S ON(SC.PATTERNNO=S.PATTERNNO)", "DISTINCT SUBEXAMNO", "S.FLDNAME='" + ViewState["hffldname"].ToString() + "' AND T.COURSENO=" + ViewState["corseno"].ToString() + " and T.CCODE='" + ViewState["CCODE"].ToString() + "' AND T.SECTIONNO=" + ViewState["sec"].ToString() + " AND T.SEMESTERNO=" + ViewState["semester"].ToString() + " AND T.SESSIONNO=" + ddlSession.SelectedValue + "");
           // this.ShowReportMarksEntry("MarksListReport", "rptSubExamMarksList.rpt");
            this.ShowReportMarksEntry("MarksListReport", "rptExamMarksList.rpt");

        }
        else
        {
            this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW.rpt");//rptMarksList1.rpt
        }
    }

    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {
        string fldname = string.Empty;
        //if (ddlSubjectType.SelectedValue == "2")
        //{
        //    fldname = ViewState["fldName"].ToString();
        //}
        //else
        //{
        //    fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + ViewState["Exam"].ToString() + "'");
        //}

        fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + ViewState["Exam"].ToString() + "'");

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        if (ddlSubjectType.SelectedValue.Equals("2")) //Pratical Sub exam Report
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
           ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["CCODE"].ToString() +
           ",@P_SECTIONNO=" + ViewState["sec"].ToString() + ",@P_SUBID=" + ddlSubjectType.SelectedValue +
           ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["semester"]) +
           ",@P_COURSENO=" + Convert.ToInt32(ViewState["corseno"]) +
           ",@P_SUBEXAM=" + ViewState["hffldname"] +
           ",@P_SUBEXAMNO=" + Convert.ToInt32(0);
        }
        else
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
                ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["CCODE"].ToString() +
                ",@P_SECTIONNO=" + ViewState["sec"].ToString() + ",@P_SUBID=" + ddlSubjectType.SelectedValue +
                ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["semester"]) +
                ",@P_COURSENO=" + Convert.ToInt32(ViewState["corseno"]);
        }

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
        if (ddlSubjectType.SelectedValue.Equals("2"))
        {
            //this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks_SubExam.rpt");
            this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks_Practical.rpt");
        }
        else if(ddlSubjectType.SelectedValue.Equals("3"))
        {
             this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks_Sessional.rpt");
        }
        else
        {
            //this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks.rpt");
            this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks_Theory.rpt");
        }

    }
    protected void ddlSubExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false;
        lblStudents.Visible = false;
        btnBack.Visible = true;
        btnSave.Visible = false;
        btnLock.Visible = false;
    }
    protected void btnAutoGenAttendance_Click(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        ShowStudents(1);
    }
    private void ShowCourses_IA()
    {
        DataSet ds = objMarksEntry.GetCourseForTeacher_IA(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                // lblStatus.Visible = true;
                // lblStatus.Text = "Marks Entry Status with Exam Name Coursewise.";
                // lblStatus.ForeColor = System.Drawing.Color.Red;
                lvCourse.DataSource = ds.Tables[0];
                lvCourse.DataBind();
            }
            else
            {
                // lblStatus.Text = "";
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page); //lblStatus.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page); //lblStatus.Visible = false;
        }
    }

    private void GetStatus_IA()
    {
        DataSet ds = null;
        //if (ddlSubjectType.SelectedValue == "2")
        //{
        //    ds = objMarksEntry.GetCourse_MarksEntryStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
        //}
        //else
        //{
        ds = objMarksEntry.GetCourse_MarksEntryStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
      //  ds = objMarksEntry.GetCourse_MarksEntryStatus_IA(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        //}
        ////if (ds != null && ds.Tables.Count > 0)
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //  lblStatus.Visible = true;
            GVEntryStatus.DataSource = ds;
            GVEntryStatus.DataBind();
            //GridView_Row_Merger(GVEntryStatus);
        }
        else
        {
            GVEntryStatus.DataSource = null;
            GVEntryStatus.DataBind();
            lvCourse.Visible = false;
            objCommon.DisplayMessage(updMarkEntry, "No Course Teacher or Student Allotment Found For This Course Type.", this.Page); //lblStatus.Visible = false;
        }
    }
}













