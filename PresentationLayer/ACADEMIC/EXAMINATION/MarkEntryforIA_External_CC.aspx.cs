using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using ClosedXML.Excel;
using System.Data.OleDb;

public partial class Academic_MarkEntryforIA_External_CC : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    string que_out = string.Empty;
    CustomStatus cs;
    string subexamno2;
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                GetGradePattern();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                //Check for Panel
                if (ViewState["action"] == null)
                {
                    //selection panel
                    pnlSelection.Visible = true;
                    pnlMarkEntry.Visible = false;
                    rptMarkCodes.Visible = false;
                }
                else if (ViewState["action"].ToString().Equals("markentry"))
                {
                    //mark entry panel
                    pnlMarkEntry.Visible = true;
                    rptMarkCodes.Visible = true;
                    pnlSelection.Visible = false;
                }
                int ua_type = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"])));
                //if (Convert.ToInt32(Session["userno"]) == 1)
                if (ua_type == 1)
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
                }
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                if (ddlSession.SelectedValue == "0")
                {

                }
                else
                {
                    this.GetExamWiseDates();
                }
                //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button

            }
        }
        divMsg.InnerHtml = string.Empty;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

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

    public void GETSTATUSDATE()
    {

        DataSet DateSessionDs = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "substring(cast(SA.START_DATE as varchar),1,12)START_DATE,substring(cast(SA.END_DATE as varchar),1,12)END_DATE", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%' AND SEMESTER like '%'  + CAST('" + Convert.ToInt32(ViewState["sem"].ToString()) + "' AS NVARCHAR(5))  +'%'    AND SA.STARTED = 1", "");
        if (DateSessionDs.Tables.Count > 0)
        {
            if (DateSessionDs.Tables[0].Rows.Count > 0)
            {
                string sessionno = DateSessionDs.Tables[0].Rows[0]["SESSION_NO"].ToString();
                lblStartD.Text = DateSessionDs.Tables[0].Rows[0]["START_DATE"].ToString();
                lblEndD.Text = DateSessionDs.Tables[0].Rows[0]["END_DATE"].ToString();
                ViewState["sessionno"] = sessionno;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
        }
    }

    private void ShowCourses()
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        DataSet ds = objMarksEntry.GetCourseForTeacherGrade(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

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
                objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page);
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page);
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            LinkButton lnk = sender as LinkButton;
            if (!lnk.ToolTip.Equals(string.Empty))
            {
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                ViewState["COURSENO"] = lblCourse.ToolTip;
                ViewState["CCODE"] = (objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO='" + lblCourse.ToolTip + "'"));
                ViewState["SCHEMENO"] = (objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO='" + lblCourse.ToolTip + "'"));
                GetGradePattern();
                if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                {
                    divnote.Visible = false;
                }
                else
                {
                    divnote.Visible = true;
                }

                string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.EXAMNO=" + Convert.ToInt32(lnk.CommandArgument.ToString().Split('+')[6]) + " )", "SESSIONNO DESC");
                if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                {
                    objCommon.DisplayMessage(this.updpanle1, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    return;
                }
                hdfSection.Value = sec_batch[0].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
                int CourseNo = 0;
                LinkButton btn = sender as LinkButton;
                CourseNo = Convert.ToInt32((btn.Parent.FindControl("hdnfld_courseno") as HiddenField).Value);
                ViewState["sem"] = Convert.ToInt32((btn.Parent.FindControl("hdnsem") as HiddenField).Value);
                ViewState["examSub_name"] = Convert.ToString(lnk.CommandArgument.Split('+')[4]);
                ViewState["exam_name"] = Convert.ToString(lnk.CommandArgument.Split('+')[5]);
                ViewState["exam_no"] = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[6]);
                ViewState["subexamNo"] = Convert.ToString(lnk.CommandArgument.Split('+')[2]);
                ViewState["examSub_name"] = Convert.ToString(lnk.CommandArgument.Split('+')[4]);
                ViewState["Sectionno"] = Convert.ToString(lnk.CommandArgument.Split('+')[1]);
                GETSTATUSDATE();
                string excelStatus = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ExcelMarkEntry", "");
                if (excelStatus == "1")
                {
                    lnkExcekImport.Visible = true;
                }
                else
                {
                    lnkExcekImport.Visible = false;
                }

                if (lnk.CommandArgument.ToString().Split('+')[3].ToString().Equals("S10"))
                {
                    ViewState["S10"] = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[3]);
                    ViewState["MODEL_EXAM_NAME"] = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[4]);

                    string itemName = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[4]);
                    string itemValue = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[3]) + "-" + Convert.ToString(lnk.CommandArgument.ToString().Split('+')[2]);

                    ddlSubExam.Items.Clear();
                    ddlSubExam.Items.Add(new ListItem("Select Exam", "0"));
                    ddlSubExam.Items.Add(new ListItem(itemName, itemValue));
                    ddlSubExam.SelectedIndex = 1;
                    ddlSubExam.Enabled = false;

                    ddlSubExam.Visible = false;
                    lblSubExamName.Visible = false;

                    if (ddlSubExam.Items.Count > 0)
                    {
                        pnlSelection.Visible = false;
                        pnlMarkEntry.Visible = true;
                        pnlStudGrid.Visible = false;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnPrintReport.Visible = false;
                        lblStudents.Visible = false;
                        ddlSubExam.SelectedIndex = 0;
                        ddlSubExam.Enabled = false;
                    }
                    DataSet dss = objCommon.FillDropDown("ACD_MARK_ENTRY_STATUS_CODES", "*", "", "", "");
                    rptMarkCodes.DataSource = dss;
                    rptMarkCodes.DataBind();
                }
                else
                {
                    ddlSubExam.Visible = true;
                    lblSubExamName.Visible = true;

                    DataSet dsExams = objMarksEntry.GetONExams_Grade(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                    string exams = string.Empty;
                    string examname = string.Empty;
                    if (dsExams != null && dsExams.Tables.Count > 0 && dsExams.Tables[0].Rows.Count > 0)
                    {

                        DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                        while (dtr.Read())
                        {
                            exams += dtr["FLDNAME3"] == DBNull.Value ? string.Empty : dtr["FLDNAME3"].ToString() + ",";
                            examname += dtr["FLDNAME"] == DBNull.Value ? string.Empty : dtr["FLDNAME"].ToString() + ",";
                        }
                        dtr.Close();

                    }
                    else
                        objCommon.DisplayMessage(this.updpanle1, "Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);


                    ViewState["subexamNo"] = Convert.ToString(lnk.CommandArgument.Split('+')[2]);
                    ViewState["examSub_name"] = Convert.ToString(lnk.CommandArgument.Split('+')[4]);
                    if (exams.Length > 0)
                    {
                        ViewState["exams"] = exams.Split(',');
                        ViewState["exam"] = exams;
                        ViewState["examname"] = examname.Trim(',');

                        ddlSubExam.Items.Clear();

                        DataTableReader dtr = dsExams.Tables[0].CreateDataReader();

                        while (dtr.Read())
                        {
                            if (ViewState["subexamNo"].ToString() == dtr["SUBEXAMNO"].ToString())
                            {
                                if (dtr["FLDNAME3"] != DBNull.Value)
                                {
                                    if (ddlSubjectType.SelectedIndex > 0)
                                    {
                                        ddlSubExam.Items.Add(new ListItem(dtr["SUBEXAMNAME"].ToString(), dtr["FLDNAME3"].ToString()));
                                    }
                                }
                            }
                        }
                        dtr.Close();
                        if (ddlSubExam.Items.Count > 0)
                        {
                            pnlSelection.Visible = false;
                            pnlMarkEntry.Visible = true;
                            pnlStudGrid.Visible = false;
                            btnSave.Visible = false;
                            btnLock.Visible = false;
                            btnPrintReport.Visible = false;
                            lblStudents.Visible = false;
                            ddlSubExam.SelectedIndex = 0;
                            ddlSubExam.Enabled = true;
                        }
                        DataSet dss = objCommon.FillDropDown("ACD_MARK_ENTRY_STATUS_CODES", "*", "", "", "");
                        rptMarkCodes.DataSource = dss;
                        rptMarkCodes.DataBind();
                        rptMarkCodes.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpanle1, "No Exam for the Selected Course may be not be Started Or may be Locked!!!", this.Page);
                    }

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
        if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
        {
            Updategradecard(0);
        }
        else
        {
            SaveAndLock(0);
        }
    }

    private void GetGradePattern()
    {
        try
        {
            ViewState["GRADEPATTERN"] = (objCommon.LookUp("ACD_SCHEME", "GRADEPATTERN", "SCHEMENO='" + ViewState["SCHEMENO"] + "'"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_MarkEntryforIA_External_CC.GetGradePattern --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        pnlStudGrid.Visible = false;
        rptMarkCodes.Visible = false;
        pnlUP.Visible = false;

        GetStatus();
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void SaveAndLock(int lock_status)
    {
        try
        {
            string API_Output = "";
            string examtype = string.Empty;
            string que_out = string.Empty;
            //Check for lock and null marks
            if (CheckMarks(lock_status) == false)
            {
                return;
            }
            string studids = string.Empty;
            string marks = string.Empty;

            Label lbl;
            TextBox txtMarks;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Note : -100 for Marks will be converted as NULL           
            //NULL means mark entry not done.                           
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            int session_type = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

            if (session_type == 1)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";

                    //Gather Exam Marks 
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                }
            }
            else if (session_type == 2)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;

                    if (txtMarks.Enabled != false)
                    {
                        //Gather Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                        studids += lbl.ToolTip + ",";

                        //Gather Exam Marks 
                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                    }
                }
                if (studids == "")
                {
                    objCommon.DisplayMessage(this.updpanle1, "Mark Entry Already Lock For Selected Students", this.Page);
                    return;
                }
            }
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();
            int courseNo = Convert.ToInt32(lblCourse.ToolTip);
            int FlagReval = 0;
            if (ddlSubExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlSubExam.SelectedValue.StartsWith("E"))
                examtype = "E";
            string examname = Convert.ToString(ViewState["exam_name"]);

            string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";

            int examno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "EXAMNO", "SUBEXAMNO=" + Convert.ToInt32(Convert.ToString(subExam_Name.Split('-')[1]))));
            cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_CPU_ADMIN_COMPONANT(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(ViewState["SCHEMENO"]), Convert.ToInt32(hdfSection.Value), subExam_Name, examno, subExam_Name);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                }
                btnShow_Click(null, null);
            }

            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Updated Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                }
                btnShow_Click(null, null);
            }
            else if (cs.Equals(CustomStatus.Others))
            {
                objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined for Selected Subject.", this.Page);
                btnShow_Click(null, null);
            }
            else
            {
                objCommon.DisplayMessage(this.updpanle1, "Error in Saving Marks!", this.Page);
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

    public void Updategradecard(int lock_status)
    {

        try
        {
            string API_Output = "";
            string examtype = string.Empty;
            //Check for lock and null marks

            string studids = string.Empty;
            string marks = string.Empty;
            string grademark = string.Empty;

            Label lbl;
            TextBox txtMarks;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Note : -100 for Marks will be converted as NULL           
            //NULL means mark entry not done.                           
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            int session_type = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

            if (session_type == 1)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";

                    //Gather Exam Marks
                    DropDownList ddlgrademarks = gvStudent.Rows[i].FindControl("ddlgrademarks") as DropDownList;
                    grademark += (ddlgrademarks.SelectedItem.Text) + ",";
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                }
            }
            else if (session_type == 2)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;

                    if (txtMarks.Enabled != false)
                    {
                        //Gather Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                        studids += lbl.ToolTip + ",";

                        //Gather Exam Marks 
                        DropDownList ddlgrademarks = gvStudent.Rows[i].FindControl("ddlgrademarks") as DropDownList;
                        grademark += (ddlgrademarks.SelectedItem.Text) + ",";
                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                    }
                }
                if (studids == "")
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Entry Already Lock For Selected Students", this.Page);
                    return;
                }
            }
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();

            int courseNo = Convert.ToInt32(lblCourse.ToolTip);

            if (ddlSubExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlSubExam.SelectedValue.StartsWith("E"))
                examtype = "E";
            string examname = Convert.ToString(ViewState["exam_name"]);

            string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";
            string ExamName = Convert.ToString(subExam_Name.Split('-')[0]);

            CustomStatus cs;
            cs = (CustomStatus)objMarksEntry.InsertGradeEntry_External(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), grademark, lock_status, ExamName, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);

                }
                GetDataGrade(0);
                btnShow_Click(null, null);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);

                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Updated Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                }
                ShowStudents();
                btnShow_Click(null, null);
            }
            else if (cs.Equals(CustomStatus.Others))
            {
                objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined for Selected Subject.", this.Page);
                ShowStudents();
                btnShow_Click(null, null);
            }

            else
            {
                objCommon.DisplayMessage(this.updpanle1, "Error in Saving Marks!", this.Page);
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

    public void GetDataGrade(int lock_status)
    {
        //int Gd = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(GRADEPATTERN,0) as GRADEPATTERN ", "SCHEMENO='" + ViewState["SCHEMENO"] + "'")));
        if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
        {

            gvStudent.Columns[4].Visible = false;
            gvStudent.Columns[5].Visible = true;
            //lnkExcekImport.Visible = false;

        }
        else
        {
            gvStudent.Columns[4].Visible = true;
            gvStudent.Columns[5].Visible = false;
        }
        string examtype = string.Empty;
        string examname = Convert.ToString(ViewState["exam_name"]);
        int a = 0;
        string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";
        string ExamName = Convert.ToString(subExam_Name.Split('-')[0]);
        if (ddlSubExam.SelectedValue.StartsWith("S"))
            examtype = "S";
        else if (ddlSubExam.SelectedValue.StartsWith("E"))
            examtype = "E";

        DataSet getdatestudent = objMarksEntry.GetCourse_GradeEntryStatus_External(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["COURSENO"]), ViewState["CCODE"].ToString(), 1, ExamName, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);

        if (getdatestudent.Tables.Count > 0)
        {
            if (getdatestudent.Tables[0].Rows.Count > 0)
            {

                if (getdatestudent.Tables[0].Rows[0]["Lock"] == System.DBNull.Value)
                {
                    foreach (GridViewRow rw in gvStudent.Rows)
                    {

                        DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                        objCommon.FillDropDownList(ddlgrade, "acd_direct_grade_system GS inner join acd_grade_new GN on GS.gradeno=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "isnull(GS.ACTIVESTATUS,0)=1 and isnull(GN.ACTIVESTATUS,0)=1  and levelno=2 and  schemano=" + ViewState["SCHEMENO"].ToString() + "", "");
                    }
                }
                else if (Convert.ToBoolean(getdatestudent.Tables[0].Rows[0]["Lock"]) == false)
                {
                    foreach (GridViewRow rw in gvStudent.Rows)
                    {
                        DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                        ddlgrade.SelectedItem.Text = Convert.ToString(getdatestudent.Tables[0].Rows[a]["SGRADE"].ToString());
                        a++;
                        ddlgrade.Enabled = true;
                    }
                }
                else
                {
                    foreach (GridViewRow rw in gvStudent.Rows)
                    {
                        DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                        objCommon.FillDropDownList(ddlgrade, "acd_direct_grade_system GS inner join acd_grade_new GN on GS.gradeno=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "isnull(GS.ACTIVESTATUS,0)=1 and isnull(GN.ACTIVESTATUS,0)=1  and levelno=2 and  schemano=" + ViewState["SCHEMENO"].ToString() + "", "");

                    }
                }
            }
            else
            {
                foreach (GridViewRow rw in gvStudent.Rows)
                {
                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                    objCommon.FillDropDownList(ddlgrade, "acd_direct_grade_system GS inner join acd_grade_new GN on GS.gradeno=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "isnull(GS.ACTIVESTATUS,0)=1 and isnull(GN.ACTIVESTATUS,0)=1  and levelno=2 and  schemano=" + ViewState["SCHEMENO"].ToString() + "", "");
                }
            }
        }
    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[3].Visible == true) return flag;
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
                                        objCommon.DisplayMessage(this.updpanle1, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
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
                                        if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                        {
                                            objCommon.DisplayMessage(this, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else if (Convert.ToDouble(txt.Text) < 0)
                                    {
                                        if (Convert.ToDouble(txt.Text) == -1 || Convert.ToDouble(txt.Text) == -2 || Convert.ToDouble(txt.Text) == -3 || Convert.ToDouble(txt.Text) == -4)
                                        {
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this, "Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.", this.Page);
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
                                        objCommon.DisplayMessage(this, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
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
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //int Gd = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(GRADEPATTERN,0) as GRADEPATTERN ", "SCHEMENO='" + ViewState["SCHEMENO"] + "'")));
        if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
        {
            Updategradecard(1);
        }
        else
        {
            SaveAndLock(1);
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        Div_ExamNameList.Visible = false;
        if (ddlSubjectType.SelectedIndex > 0)
        {
            this.ShowCourses();
            this.GetStatus();
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        if (ddlSubExam.Visible == true)
        {
            ShowStudents();
        }
        else
        {
            if (ddlSubExam.Enabled == false && Convert.ToString(ViewState["S10"]) == "S10")
            {
                ShowStudents_For_Model_Exam();
            }
            else
            {
                objCommon.DisplayMessage(this.updpanle1, "Please Select Sub Exam!!", this.Page);
                ddlSubExam.Focus();
            }
        }
    }

    #region show student
    private void ShowStudents()
    {
        try
        {
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            DataSet ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=(select schemeno from acd_course where courseno=" + Convert.ToInt32(ViewState["COURSENO"]) + ") AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + "AND SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + "", "");
            if (Convert.ToInt32(ViewState["GRADEPATTERN"]) != 1)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) < 0)
                    {
                        objCommon.DisplayMessage(this.updpanle1, "STOP !!! Rule 1 for " + Convert.ToString(ddlSubExam.SelectedItem.Text) + " is not Defined", this.Page);
                        return;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) < 0)
                    {
                        objCommon.DisplayMessage(this.updpanle1, "STOP !!! Rule 2 for " + Convert.ToString(ddlSubExam.SelectedItem.Text) + " is not Defined", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined", this.Page);
                    return;
                }
            }

            int exam_type = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
            if (exam_type == 1)
            {
                dsStudent = objMarksEntry.GetStudentsForMarkEntry_Grade(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToString(ViewState["exam_name"]), Convert.ToInt32(ViewState["COURSENO"]), Convert.ToString(ddlSubExam.SelectedValue), Convert.ToInt32(ddlSort.SelectedValue));
            }
            else
            {
                dsStudent = objMarksEntry.GetStudentsForMarkEntry_Grade_for_backlog(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToString(ddlSubExam.SelectedValue).Split('-')[0], Convert.ToInt32(ViewState["COURSENO"]), Convert.ToString(ddlSubExam.SelectedValue), Convert.ToInt32(ddlSort.SelectedValue));

            }
            int lockcount = 0;
            int pubcount = 0;
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ViewState["LOCKSTATUS"] = Convert.ToString(dsStudent.Tables[0].Rows[0]["LOCK"]);
                    ViewState["SemesterNo"] = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    string subexam = ddlSubExam.SelectedValue;
                    string substring = subexam.Substring(0, 1);
                    int internalmark = Convert.ToInt32(Math.Round(Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "MAXMARKS_I", "COURSENO=" + Convert.ToInt32(ViewState["COURSENO"])))));
                    //if (substring == "E")
                    if (substring == "E" && internalmark > 0 && Convert.ToInt32(ViewState["GRADEPATTERN"]) != 1)
                    {
                        ViewState["LOCKSTATUS"] = Convert.ToString(dsStudent.Tables[0].Rows[0]["LOCK"]);
                        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LOCKS1"]) == 0)
                        {
                            objCommon.DisplayMessage(this.updpanle1, "Internal Grade/Mark Entry is not Done.", this.Page);
                            return;
                        }
                        gvStudent.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvStudent.Columns[2].Visible = true;
                    }
                    if (dsStudent.Tables[0].Rows[0]["SMAX"] != "0")
                    {
                        gvStudent.Columns[4].HeaderText = "<center>" + ddlSubExam.SelectedItem.Text + "</center>" + "<center><span MaxMarks'>[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]</span></center>";
                        gvStudent.Columns[4].Visible = true;

                        hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                        hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];

                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                lockcount++;
                                lnkExcekImport.Enabled = false;
                                pnlUP.Visible = false;
                            }
                            else
                            {
                                lnkExcekImport.Enabled = true;
                                pnlUP.Visible = true;
                            }
                        }
                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dsStudent.Tables[0].Rows[0]["PUBLISH"].ToString()))
                            {
                                if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["PUBLISH"]) == true)
                                {
                                    pubcount++;
                                    //btnPublish.Text = "Unpublish";    //Commented on 30-09-2023 End sem Garde Entry Not required button
                                }
                                else
                                {
                                    //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button
                                }
                            }
                            else
                            {
                                //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button
                            }
                        }
                    }
                    //int Gd = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(GRADEPATTERN,0) as GRADEPATTERN ", "SCHEMENO='" + ViewState["SCHEMENO"] + "'")));
                    if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                    {

                        gvStudent.Columns[4].Visible = false;
                        gvStudent.Columns[5].Visible = true;
                        //lnkExcekImport.Visible = false;
                    }
                    else
                    {
                        gvStudent.Columns[4].Visible = true;
                        gvStudent.Columns[5].Visible = false;
                    }
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();
                    int session_type = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
                    if (session_type == 2)
                    {
                        int z = 0;
                        foreach (GridViewRow rw in gvStudent.Rows)
                        {
                            TextBox txtmark = (TextBox)rw.FindControl("txtmarks");
                            string regno = (dsStudent.Tables[0].Rows[z]["REGNO"]).ToString();

                            int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + " AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + " AND REGNO='" + regno + "'"));
                            int count_result = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + " AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + " AND REGNO='" + regno + "'"));
                            if (count > 0)
                            {
                                if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == true && Convert.ToInt32(dsStudent.Tables[0].Rows[z]["EXAMTYPE"]) == 2)
                                {
                                    txtmark.Enabled = false;
                                }
                                else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == false && Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCKR"]) == false && Convert.ToInt32(dsStudent.Tables[0].Rows[z]["EXAMTYPE"]) == 2)
                                {
                                    txtmark.Enabled = true;
                                }
                                else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == false && Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCKR"]) == true && Convert.ToInt32(dsStudent.Tables[0].Rows[z]["EXAMTYPE"]) == 2)
                                {
                                    txtmark.Enabled = false;
                                }
                            }
                            else if (count_result > 0)
                            {
                                if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCKR"]) == true && Convert.ToInt32(dsStudent.Tables[0].Rows[z]["EXAMTYPE"]) == 2)
                                {
                                    txtmark.Enabled = false;
                                }

                                else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCKR"]) == false && Convert.ToInt32(dsStudent.Tables[0].Rows[z]["EXAMTYPE"]) == 2)
                                {
                                    txtmark.Enabled = true;
                                }
                            }
                            z++;

                        }
                    }
                    string examtype = string.Empty;
                    string examname = Convert.ToString(ViewState["exam_name"]);
                    string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";
                    string ExamName = Convert.ToString(subExam_Name.Split('-')[0]);
                    if (ddlSubExam.SelectedValue.StartsWith("S"))
                        examtype = "S";
                    else if (ddlSubExam.SelectedValue.StartsWith("E"))
                        examtype = "E";
                    DataSet getdatestudent = objMarksEntry.GetCourse_GradeEntryStatus_External(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["COURSENO"]), ViewState["CCODE"].ToString(), 1, ExamName, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                    if (getdatestudent.Tables.Count > 0)
                    {
                        if (getdatestudent.Tables[0].Rows.Count > 0)
                        {

                            int a = 0;

                            if (getdatestudent.Tables[0].Rows[0]["Lock"] == System.DBNull.Value)
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");
                                }
                            }
                            else if (Convert.ToBoolean(getdatestudent.Tables[0].Rows[0]["Lock"]) == true)
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    if (Convert.ToString(getdatestudent.Tables[0].Rows[a]["SGRADE"].ToString()) != "")
                                    {
                                        ddlgrade.SelectedItem.Text = Convert.ToString(getdatestudent.Tables[0].Rows[a]["SGRADE"].ToString());
                                    }
                                    a++;
                                    ddlgrade.Enabled = false;
                                }
                            }
                            else if (Convert.ToBoolean(getdatestudent.Tables[0].Rows[0]["Lock"]) == false)
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");
                                    if (Convert.ToString(getdatestudent.Tables[0].Rows[a]["SGRADE"].ToString()) != "")
                                    {
                                        ddlgrade.SelectedItem.Text = Convert.ToString(getdatestudent.Tables[0].Rows[a]["SGRADE"].ToString());
                                    }
                                    a++;
                                    ddlgrade.Enabled = true;
                                    btnLock.Enabled = true;
                                }
                            }
                            else
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");
                                }
                            }
                            int b = 0;

                            if (getdatestudent.Tables[0].Rows[0]["PUBLISH"] == System.DBNull.Value)
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");
                                    if (Convert.ToString(getdatestudent.Tables[0].Rows[b]["SGRADE"].ToString()) != "")
                                    {
                                        ddlgrade.SelectedItem.Text = Convert.ToString(getdatestudent.Tables[0].Rows[b]["SGRADE"].ToString());
                                    }
                                    b++;
                                }

                            }
                            else if (Convert.ToBoolean(getdatestudent.Tables[0].Rows[0]["PUBLISH"]) == true)
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    if (Convert.ToString(getdatestudent.Tables[0].Rows[b]["SGRADE"].ToString()) != "")
                                    {
                                        ddlgrade.SelectedItem.Text = Convert.ToString(getdatestudent.Tables[0].Rows[b]["SGRADE"].ToString());
                                    }
                                    b++;
                                    //ddlgrade.Enabled = false;
                                    //btnPublish.Text = "Unpublish"; //Commented on 30-09-2023 End sem Garde Entry Not required button

                                }

                            }
                            else if (Convert.ToBoolean(getdatestudent.Tables[0].Rows[0]["PUBLISH"]) == false)
                            {
                                foreach (GridViewRow rw in gvStudent.Rows)
                                {
                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");
                                    if (Convert.ToString(getdatestudent.Tables[0].Rows[b]["SGRADE"].ToString()) != "")
                                    {
                                        ddlgrade.SelectedItem.Text = Convert.ToString(getdatestudent.Tables[0].Rows[b]["SGRADE"].ToString());
                                    }
                                    b++;
                                    ddlgrade.Enabled = true;
                                    btnLock.Enabled = true;
                                }


                            }
                            else
                            {

                                foreach (GridViewRow rw in gvStudent.Rows)
                                {

                                    DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                    objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");

                                }
                            }
                        }
                        else
                        {
                            foreach (GridViewRow rw in gvStudent.Rows)
                            {
                                DropDownList ddlgrade = (DropDownList)rw.FindControl("ddlgrademarks") as DropDownList;
                                objCommon.FillDropDownList(ddlgrade, "ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "GS.GRADENO", "GN.GRADE", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1 AND LEVELNO=" + Convert.ToInt32(2) + " AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString() + "", "");
                                ddlgrade.Items.Add(new ListItem("Please select", "0"));
                                ddlgrade.SelectedIndex = 0;
                            }
                        }
                    }
                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Visible = false; btnLock.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true; btnLock.Visible = true;
                    }
                    pnlSelection.Visible = false;
                    pnlMarkEntry.Visible = true;
                    pnlStudGrid.Visible = true;
                    lblStudents.Visible = true;
                    btnSave.Visible = true;
                    btnLock.Visible = true;
                    btnPrintReport.Visible = true;
                    if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                    {
                        lblGridHeading.Text = "Enter Grades for following Students";
                    }
                    else
                    {
                        lblGridHeading.Text = "Enter Marks for following Students";
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Students Not Found..!!", this.Page);
                }

                if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students
                {
                    btnSave.Visible = false;
                    btnLock.Visible = false;
                    //btnPublish.Visible = true; //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                else
                {
                    //btnPublish.Visible = true;  //Commented on 30-09-2023 End sem Garde Entry Not required button
                    //btnPublish.Text = "Publish";    //Commented on 30-09-2023 End sem Garde Entry Not required button
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

    #endregion

    private void ShowStudents_For_Model_Exam()
    {
        try
        {
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            string SP_Name = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_MODEL_EXAM";
            string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SECTIONNO, @P_SUBID, @P_UA_NO, @P_EXAM_NAME";
            string Call_Values = "" + ddlSession.SelectedValue + "," + Convert.ToInt32(ViewState["COURSENO"]) + "," + Convert.ToInt16(ViewState["sem"]) + "," + Convert.ToInt16(hdfSection.Value) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToString(ViewState["MODEL_EXAM_NAME"]) + "";

            dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            int lockcount = 0;
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ViewState["SemesterNo"] = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    if (ddlSubExam.SelectedValue == "EXTERMARK")
                    {
                        gvStudent.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvStudent.Columns[2].Visible = true;
                    }
                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                    {
                        gvStudent.Columns[4].HeaderText = "<center>" + ddlSubExam.SelectedItem.Text + "</center>" + "<span class='pull-left MaxMarks'>[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]</span>" + "<span class='pull-right'>[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]</span>";
                        gvStudent.Columns[4].Visible = true;

                        hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                        hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];

                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                lockcount++;
                            }
                        }
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
                        btnSave.Visible = false; btnLock.Visible = false;
                        objCommon.DisplayMessage(this.updpanle1, "Selected Exam Not Applicable for Mark Entry!!", this.Page);
                    }
                    else
                    {
                        btnSave.Visible = true; btnLock.Visible = true;
                    }

                    pnlSelection.Visible = false; pnlMarkEntry.Visible = true; pnlStudGrid.Visible = true; lblStudents.Visible = true;
                    btnSave.Visible = true; btnLock.Visible = true; btnPrintReport.Visible = true;
                    if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                    {
                        lblGridHeading.Text = "Enter Grades for following Students";
                    }
                    else
                    {
                        lblGridHeading.Text = "Enter Marks for following Students";
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Students Not Found..!!", this.Page);
                }
                if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students
                {
                    btnSave.Visible = false;
                    btnLock.Visible = false;
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
    //methods to get marks entry status course wise
    private void GetStatus()
    {
        rptExamName.DataSource = null;
        rptExamName.DataBind();
        Div_ExamNameList.Visible = false;
        DataSet ds = objMarksEntry.GetCourse_MarksEntryStatus_External(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            rptExamName.DataSource = ds;
            rptExamName.DataBind();
            Div_ExamNameList.Visible = true;
        }
        else
        {
            int aases_record = Convert.ToInt32(objCommon.LookUp("ACD_ASSESSMENT_EXAM_COMPONENT", "COUNT(*)", "SESSIONNO=" + ddlSession.SelectedValue + " AND UA_NO=" + Convert.ToInt16(Session["userno"]) + " AND isnull(cancle,0)=0"));
            int aases_lock = Convert.ToInt32(objCommon.LookUp("ACD_ASSESSMENT_EXAM_COMPONENT", "COUNT(*)", "SESSIONNO=" + ddlSession.SelectedValue + " AND UA_NO=" + Convert.ToInt16(Session["userno"]) + " AND isnull(cancle,0)=0 and isnull(lock,0)=1"));
            if (aases_record > 0)
            {
                if (aases_lock > 0)
                {
                    objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page);
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Please Check Exam Component are Lock or not.", this.Page);
                    return;
                }
            }
            rptExamName.DataSource = null;
            rptExamName.DataBind();
            lvCourse.Visible = false;
            Div_ExamNameList.Visible = false;
            objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page);

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

    protected void GVEntryStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[3].Text)
            {
                case "PENDING":
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].Enabled = false;
                    break;
                case "COMPLETED":
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    break;
                case "IN PROGRESS":
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Orange;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    break;
                default:
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    break;
            };
        }
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false;
        lblStudents.Visible = false;
        btnSave.Visible = false;
        btnLock.Visible = false;
        btnPrintReport.Visible = false;
        if (ddlSubExam.SelectedIndex > 0)
        {
            ddlSubExam.Enabled = true;

            DataSet ds = objCommon.FillDropDown("ACD_COURSE", "SUBID", "", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "", "");

            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SUBEXAM_NAME SN INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SN.SUBEXAMNO)", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(EC.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND ISNULL(CANCLE,0)=0 AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + "", "");
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + "", "");
            }
        }
        else if (ddlSubExam.SelectedIndex == 0)
        {
            ddlSubExam.SelectedIndex = 0;
            ddlSubExam.Enabled = false;
        }
    }

    protected void ddlSubExam_SelectedIndexChanged(object sender, EventArgs e)
    {

        pnlStudGrid.Visible = false;
        lblStudents.Visible = false;
        btnSave.Visible = false;
        btnLock.Visible = false;
        btnPrintReport.Visible = false;
    }

    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMarks = (TextBox)e.Row.FindControl("txtMarks");


        }
    }

    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {

        string fldname = objCommon.LookUp("ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlSubExam.SelectedItem.Text) + "'");
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(ViewState["COURSENO"])));
        int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + scheme));
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + scheme));
        int college_id = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "SCHEMENO=" + scheme + "AND DEGREENO=" + degreeno + "AND BRANCHNO=" + branchno));
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["CCODE"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + Convert.ToString(ViewState["exam_name"]) + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_SUB_EXAM=" + ddlSubExam.SelectedValue + "";
        url += "&param=@P_COLLEGE_CODE=" + college_id + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["CCODE"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + Convert.ToString(ViewState["exam_name"]) + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_SUB_EXAM=" + ddlSubExam.SelectedValue + "";

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        //update panel
        string Script = string.Empty;
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.updpanle1, updpanle1.GetType(), "Report", Script, true);
    }

    private void GetExamWiseDates()
    {
        try
        {
            DataSet dsmemssage = objCommon.FillDropDown("ACTIVITY_MASTER A, SESSION_ACTIVITY S,ACD_EXAM_NAME E", "E.FLDNAME,E.EXAMNAME", "S.END_DATE", "A.ACTIVITY_NO = S.ACTIVITY_NO AND A.EXAMNO = E.EXAMNO AND CONVERT(DATE,GETDATE(),103) >= CONVERT(DATE,S.START_DATE,103) AND CONVERT(DATE,GETDATE(),103) <= CONVERT(DATE,S.END_DATE,103)AND S.STARTED = 1 AND UA_TYPE COLLATE DATABASE_DEFAULT LIKE '%" + Convert.ToString(Session["usertype"]) + "%' AND PAGE_LINK LIKE '%" + Convert.ToString(Request.QueryString["pageno"].ToString()) + "%' AND S.SESSION_NO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND E.EXAMNAME <> '' ", "");
            if (dsmemssage != null && dsmemssage.Tables[0].Rows.Count > 0)
            {
                string message = string.Empty;
                string stopDate = string.Empty;
                for (int i = 0; i < dsmemssage.Tables[0].Rows.Count; i++)
                {
                    string examname = dsmemssage.Tables[0].Rows[i]["EXAMNAME"].ToString();
                    string enddate = dsmemssage.Tables[0].Rows[i]["END_DATE"].ToString();
                    if (enddate != string.Empty || enddate != "")
                    {
                        DateTime statusdate = Convert.ToDateTime(enddate);
                        string status = statusdate.ToString("d");
                        if (status != string.Empty || status != "")
                        {
                            message += " " + examname + ",";
                            stopDate += " " + status + ",";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.GetExamWiseDates --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        Div_ExamNameList.Visible = false;
        if (Convert.ToInt32(ddlSession.SelectedValue) > 0)
        {
            int ua_type = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"])));
            //if (Convert.ToInt32(Session["userno"]) == 1)
            if (ua_type == 1)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", "DISTINCT S.SUBID", "SUBNAME", "S.SUBID > 0 AND ISNULL(S.ACTIVESTATUS,0)=1 AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "");
            }
            else
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", "DISTINCT R.SUBID", "SUBNAME", "S.SUBID > 0 AND (UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " OR UA_NO_PRAC=" + Convert.ToInt32(Session["userno"].ToString()) + ") AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "");

            }
            this.GetExamWiseDates();
        }
        else
        {
        }
        ddlSubjectType.SelectedIndex = 0;
        rptExamName.DataSource = null;
        rptExamName.DataBind();
    }

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        //int Gd = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(GRADEPATTERN,0) as GRADEPATTERN ", "SCHEMENO='" + ViewState["SCHEMENO"] + "'")));
        if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
        {
            if (ddlSubExam.Visible == false)
            {
                string reportTitle = "MarksListReport";
                string rptFileName = "rptMarksList1_NEW_Atlas_Grade.rpt";

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_EXAM_NAME=" + Convert.ToString(ViewState["MODEL_EXAM_NAME"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.updpanle1, this.updpanle1.GetType(), "key", Print_Val, true);
            }
            else
            {
                this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_External_Grade_Report.rpt");
            }

        }
        else
        {
            if (ddlSubExam.Visible == false)
            {
                string reportTitle = "MarksListReport";
                string rptFileName = "rptMarksList1_External_MarkEntry_Report.rpt";

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_EXAM_NAME=" + Convert.ToString(ViewState["MODEL_EXAM_NAME"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.updpanle1, this.updpanle1.GetType(), "key", Print_Val, true);
            }
            else
            {
                this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_External_MarkEntry_Report.rpt");//rptMarksList1.rpt
            }
        }
    }

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)(sender);
        ViewState["courseNo_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[0]);
        lbl_SubjectName.Text = lbtn.CommandArgument.Split(',')[1];
        ViewState["sem_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[2]);
        ViewState["sec_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[3]);
        ViewState["examNo_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[4]);
        ViewState["examName_POP"] = Convert.ToString(lbtn.CommandArgument.Split(',')[5]);
        ViewState["fldname_POP"] = Convert.ToString(lbtn.CommandArgument.Split(',')[6]);

        ViewState["ccode_POP"] = lbl_SubjectName.Text.Split('~')[0];

        ddlExamPrint.Items.Clear();
        ddlExamPrint.Items.Add(new ListItem("Please Select", "0"));
        ddlExamPrint.Items.Add(new ListItem(ViewState["examName_POP"].ToString(), ViewState["examNo_POP"].ToString()));

        ddlSubExamPrint.Items.Clear();
        ddlSubExamPrint.Items.Add(new ListItem("Please Select", "0"));


        if (Convert.ToString(ViewState["fldname_POP"]) == "S10")
        {
            ddlSubExamPrint.Visible = false;
            lbl_SubExam_Print.Visible = false;
            btnPrintFront.Enabled = true;
        }
        else
        {
            ddlSubExamPrint.Visible = true;
            lbl_SubExam_Print.Visible = true;
            ddlSubExamPrint.Enabled = false;
            btnPrintFront.Enabled = false;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#PrintModal').modal('show');</script>", false);
        updPopUp.Update();
    }

    protected void ddlExamPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamPrint.SelectedIndex != 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_COURSE", "SUBID", "", "COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "", "");

            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
            {
                objCommon.FillDropDownList(ddlSubExamPrint, "ACD_SUBEXAM_NAME SB inner join ACD_ASSESSMENT_EXAM_COMPONENT EM on(SB.Subexamno=EM.subexamno)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SB.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME  ", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND isnull(cancle,0)=0 AND EXAMNO=" + Convert.ToString(ddlExamPrint.SelectedValue) + "", "");
                ddlSubExamPrint.Enabled = true;
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExamPrint, "ACD_SUBEXAM_NAME SB inner join ACD_ASSESSMENT_EXAM_COMPONENT EM on(SB.Subexamno=EM.subexamno)", "TOP(1) CAST(FLDNAME AS VARCHAR)+'-'+CAST(SB.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND isnull(cancle,0)=0 AND EXAMNO=" + Convert.ToString(ddlExamPrint.SelectedValue) + "", "");
                ddlSubExamPrint.Enabled = true;
            }
            ddlSubExamPrint.Enabled = true;
        }
        else
        {
            ddlSubExamPrint.Enabled = false;
        }

    }

    protected void ddlSubExamPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubExamPrint.SelectedIndex != 0)
        {
            btnPrintFront.Enabled = true;
        }
        else
        {
            btnPrintFront.Enabled = false;
        }
    }

    protected void btnPrintFront_Click(object sender, EventArgs e)
    {
        string rptFileName = string.Empty;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#PrintModal').modal('hide');</script>", true);
        int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO='" + Convert.ToInt32(ViewState["courseNo_POP"]) + "'"));
        int gd = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(GRADEPATTERN,0) as GRADEPATTERN ", "SCHEMENO='" + schemeno + "'")));
        if (gd == 1)
        {
            string reportTitle = "MarksListReport";
            rptFileName = "rptMarksList1_External_Grade_Report.rpt";
            string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + Convert.ToString(ViewState["ccode_POP"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_SUB_EXAM=" + ddlSubExamPrint.SelectedValue + "";


            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
        }
        else
        {
            string reportTitle = "MarksListReport";
            rptFileName = "rptMarksList1_External_MarkEntry_Report.rpt";
            string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_SUB_EXAM=" + ddlSubExamPrint.SelectedValue + "";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
        }
    }

    protected void btnPrintAll_Click(object sender, EventArgs e)
    {
        if (ddlExamPrint.SelectedIndex > 0)
        {

            string reportTitle = "CatMarksListReport";
            string rptFileName = "rptMarksList_Examwise.rpt";
            string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_EXAM=" + fldname.ToString() + "";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
        }
        else
        {
            int subid = Convert.ToInt32(ddlSubjectType.SelectedValue);
            if (subid == 1)
            {

                string reportTitle = "CatMarksListReport";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory.rpt";
                string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";
                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
            }
            else
            {

                string reportTitle = "CatMarksListReport";
                string rptFileName = "rptMarksList_ExamwiseNew.rpt";
                string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";
                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
            }
        }
    }


    protected void btnBlankDownld_Click(object sender, EventArgs e)
    {
        try
        {
            string excelname = string.Empty;
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            ViewState["StudCount"] = 0;
            int MExamNo = Convert.ToInt32(ViewState["exam_no"]);
            string subexamno = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "ISNULL(ACTIVESTATUS,0)=1 AND FLDNAME='" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[0] + "'");
            subexamno2 = Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1];
            Session["SUBEXAMNO"] = subexamno2;
            dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_Grade(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), MExamNo, Convert.ToInt32(ViewState["COURSENO"]), subexamno2);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlSubExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");
                    ViewState["StudCount"] = dsStudent.Tables[0].Rows.Count;
                    //Bind the Student List
                    DataTable dst = dsStudent.Tables[0];
                    DataGrid dg = new DataGrid();
                    if (dsStudent != null && dsStudent.Tables.Count > 0)
                    {
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            ///Modify for Grade Entry By Excel added by Injamam Ansari Date:24-11-2023
                            string[] selectedColumns = new[] { "IDNO", "STUDNAME", "REGNO1", "CCODE", "COURSENAME", "DEGREENAME", "BRANCHNAME", "SCHEMENAME", "SEMESTERNAME", "SESSIONNAME", "EXAMNAME", "SUBEXAMNAME", "SECTIONNAME", "MAXMARK" };
                            DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
                            dt.Columns["REGNO1"].ColumnName = "REGNO / ROLL_NO"; // change column names
                            if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                            {
                                dt.Columns.Remove("MAXMARK");
                                dt.Columns.Add("Grade");
                            }
                            else
                            {
                                dt.Columns.Add("MARKS");
                            }


                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                //Export the Excel file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                                    //Response.End();
                                }
                            }
                        }
                    }

                    pnlSelection.Visible = false;
                    pnlMarkEntry.Visible = true;
                    pnlStudGrid.Visible = true;
                    lblStudents.Visible = true;
                    btnBack.Visible = true;
                    if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                    {
                        lblGridHeading.Text = "Enter Grades for following Students";
                    }
                    else
                    {
                        lblGridHeading.Text = "Enter Marks for following Students";
                    }
                }
                else
                {
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

    private bool CheckExcelMarks(int lock_status, DataTable dt)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;
            string grade = string.Empty;
            DataColumnCollection columns = dt.Columns;

            if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
            {
                if (columns.Contains("Grade"))
                {
                    for (int j = 12; j < dt.Columns.Count; j++)    //columns
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)   //rows 
                        {

                            if (j == 12) // Grade
                            {
                                grade = dt.Rows[i]["Grade"].ToString();
                                if (!grade.Equals(string.Empty))
                                {
                                    if (grade == "")
                                    {
                                        if (lock_status == 1)
                                        {
                                            objCommon.DisplayMessage(updpanle1, "Grade Entry Not Completed!! Please Enter the Grade for all Students.", this.Page);
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //Check for Grades entered is valid grade
                                        string gardes = objCommon.LookUp("ACD_DIRECT_GRADE_SYSTEM GS INNER JOIN ACD_GRADE_NEW GN ON GS.GRADENO=GN.GRADENO", "STRING_AGG(GN.GRADE,',') AS GRADES", "ISNULL(GS.ACTIVESTATUS,0)=1 AND ISNULL(GN.ACTIVESTATUS,0)=1  AND LEVELNO=2 AND  SCHEMANO=" + ViewState["SCHEMENO"].ToString());
                                        if (!gardes.Contains(grade))
                                        {
                                            ShowMessage("Grade Entered [" + grade + "] is not a Valid Grade Kindly Enter proper Grades[" + gardes + "].");
                                            flag = false;
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    ShowMessage("Grade Entry Not Completed!! Please Enter the Grade for all Students.");
                                    flag = false;
                                    break;

                                }
                            }
                            if (flag == false)
                                break;
                        }
                    }
                }
                else
                {
                    ShowMessage("Invalid Excel File !!");
                    flag = false;

                }
            }
            else
            {
                if (columns.Contains("MARKS"))
                {
                    for (int j = 13; j < dt.Columns.Count; j++)    //columns
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)   //rows 
                        {

                            if (j == 13) // MARKS
                            {

                                maxMarks = dt.Rows[i]["MAXMARK"].ToString();
                                marks = dt.Rows[i]["MARKS"].ToString();
                                if (!marks.Equals(string.Empty) && !maxMarks.Equals(string.Empty))
                                {
                                    if (marks == "")
                                    {
                                        if (lock_status == 1)
                                        {
                                            objCommon.DisplayMessage(updpanle1, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);


                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //Check for Marks entered greater than Max Marks
                                        if (Convert.ToDouble(marks) > Convert.ToDouble(maxMarks))
                                        {

                                            if (Convert.ToDouble(marks) != 902 && Convert.ToDouble(marks) != 903 && Convert.ToDouble(marks) != 904 && Convert.ToDouble(marks) != 905 && Convert.ToDouble(marks) != 906)
                                            {
                                                ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero).");

                                                flag = false;
                                                break;
                                            }
                                        }
                                        else if (Convert.ToDouble(marks) < 0)
                                        {
                                            //Note : 401 for Absent and Not Eligible
                                            if (Convert.ToDouble(marks) == -1 || Convert.ToDouble(marks) == -2 || Convert.ToDouble(marks) == -3 || Convert.ToDouble(marks) == -4)
                                            {
                                                ShowMessage("Marks Entered [" + marks + "] can not be Less than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero).");
                                            }
                                            else
                                            {
                                                ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero).");

                                                flag = false;
                                                break;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                    flag = false;
                                    break;

                                }
                            }
                            if (flag == false)
                                break;
                        }
                    }
                }
                else
                {
                    ShowMessage("Invalid Excel File !!");
                    flag = false;

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

    private void SaveExcelMarks(int lock_status, DataTable dt, int semno)
    {
        try
        {
            string examtype = string.Empty;
            string subExam = string.Empty;
            CustomStatus cs = CustomStatus.Error;
            CustomStatus log = CustomStatus.Error;
            string file_name = ViewState["FileName"].ToString();
            int FlagReval = 0;
            string examname1 = Convert.ToString(ViewState["exam_name"]);
            string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";



            string studids = string.Empty;
            string marks = string.Empty;

            Label lbl;
            TextBox txtMarks;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Note : -100 for Marks will be converted as NULL           
            //NULL means mark entry not done.                           
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                studids += dt.Rows[i]["IDNO"].ToString() + ",";
                marks += dt.Rows[i]["MARKS"].ToString() == string.Empty ? "-100," : dt.Rows[i]["MARKS"].ToString() + ",";
            }
            int sectionno = Convert.ToInt32(hdfSection.Value);
            int courseno = Convert.ToInt32(lblCourse.ToolTip);
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();
            if (ddlSubExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlSubExam.SelectedValue.StartsWith("E"))
                examtype = "E";
            string examname = Convert.ToString(ViewState["exam_name"]);

            if (!string.IsNullOrEmpty(studids))
            {
                int examno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "EXAMNO", "SUBEXAMNO=" + Convert.ToInt32(Convert.ToString(subExam_Name.Split('-')[1]))));
                cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_CPU_ADMIN_COMPONANT(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname1, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(ViewState["SCHEMENO"]), Convert.ToInt32(hdfSection.Value), subExam_Name, examno, subExam_Name);
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(updpanle1, "Marks Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }
                else
                {
                    objCommon.DisplayMessage(updpanle1, "Marks Saved Successfully.", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void SaveExcelGrade(int lock_status, DataTable dt, int semno)
    {
        try
        {
            string examtype = string.Empty;
            string subExam = string.Empty;
            CustomStatus cs = CustomStatus.Error;
            CustomStatus log = CustomStatus.Error;
            string file_name = ViewState["FileName"].ToString();
            int FlagReval = 0;
            string examname1 = Convert.ToString(ViewState["exam_name"]);
            string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";

            string studids = string.Empty;
            string grades = string.Empty;

            Label lbl;
            TextBox txtMarks;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                studids += dt.Rows[i]["IDNO"].ToString() + ",";
                grades += dt.Rows[i]["GRADE"].ToString() == string.Empty ? " " : dt.Rows[i]["GRADE"].ToString() + ",";
            }
            int sectionno = Convert.ToInt32(hdfSection.Value);
            int courseno = Convert.ToInt32(lblCourse.ToolTip);
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();

            if (ddlSubExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlSubExam.SelectedValue.StartsWith("E"))
                examtype = "E";
            string examname = Convert.ToString(ViewState["exam_name"]);
            string ExamName = Convert.ToString(subExam_Name.Split('-')[0]);
            if (!string.IsNullOrEmpty(studids))

                // cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew_Grade(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids.Remove(studids.Length - 1, 1), grades.Remove(grades.Length - 1, 1), lock_status, examname1, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ViewState["sem"]), Convert.ToInt32(hdfSection.Value));

                cs = (CustomStatus)objMarksEntry.InsertGradeEntry_External(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids.Remove(studids.Length - 1, 1), grades.Remove(grades.Length - 1, 1), lock_status, ExamName, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(updpanle1, "Grade Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }
                else
                {
                    objCommon.DisplayMessage(updpanle1, "Grade Saved Successfully.", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FuBrowse.HasFile)
            {
                string path = MapPath("~/ExcelData/");
                ViewState["FileName"] = string.Empty;
                string filename = FuBrowse.FileName.ToString();
                string Extension = Path.GetExtension(filename);
                string Filepath = Server.MapPath("~/ExcelData/" + filename);
                if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                {
                    ViewState["FileName"] = filename;
                    FuBrowse.SaveAs(path + filename);// To save file in code folder to validate marks.
                    if (!CheckFormatAndImport(Extension, Filepath, "yes"))
                    {
                        File.Delete(Filepath); // To delete file from code folder after saved file in blob storage
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["SUBEXAMNO"]) == Convert.ToInt32(ViewState["subexamNo"]))
                        {
                            ShowStudents();
                            pnlUP.Visible = false;
                            objCommon.DisplayMessage(updpanle1, "Mark Entry Uploaded Successfully !", this);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpanle1, "Please Upload Correct Excel Sheet !", this);
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpanle1, "Only Excel Sheet is Allowed!", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpanle1, "Select File to Upload!!!", this);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool CheckFormatAndImport(string Extension, string FilePath, string isHDR)  //bool
    {
        string filename = ViewState["FileName"].ToString();
        Exam objExam = new Exam();
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 Excel07ConString
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath);
        string Message = string.Empty;
        int count = 0;
        OleDbConnection connExcel = new OleDbConnection(conStr);
        try
        {
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataSet ds = null;
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            //Check for lock and null marks
            if (CheckExcelMarks(0, dt) == false)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
                {
                    SaveExcelGrade(0, dt, Convert.ToInt32(ViewState["sem"].ToString()));
                }
                else
                {
                    SaveExcelMarks(0, dt, Convert.ToInt32(ViewState["sem"].ToString()));
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Please Check if the data is saved in sheet1 of the file you are uploading or the file is in correct format!! ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
        finally
        {
            connExcel.Close();
            connExcel.Dispose();
        }
    }

    protected void lnkExcekImport_Click(object sender, EventArgs e)
    {

        ViewState["markentrystatus"] = "0";
        if (ViewState["markentrystatus"].ToString() == "0")
        {
            pnlUP.Visible = true;
        }
        else
        {
            pnlUP.Visible = false;
            objCommon.DisplayMessage(updpanle1, "Mark Entry is locked!", this.Page);
        }
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        pnlUP.Visible = false;
    }

    public void publishgrade(int publish_status)
    {
        try
        {
            string API_Output = "";
            string examtype = string.Empty;
            string studids = string.Empty;
            string marks = string.Empty;
            string grademark = string.Empty;

            Label lbl;
            TextBox txtMarks;

            int session_type = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

            if (session_type == 1)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";
                    //Gather Exam Marks
                    DropDownList ddlgrademarks = gvStudent.Rows[i].FindControl("ddlgrademarks") as DropDownList;
                    grademark += (ddlgrademarks.SelectedItem.Text) + ",";
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";

                }
            }
            else if (session_type == 2)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;

                    if (txtMarks.Enabled != false)
                    {
                        //Gather Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                        studids += lbl.ToolTip + ",";
                        //Gather Exam Marks 
                        DropDownList ddlgrademarks = gvStudent.Rows[i].FindControl("ddlgrademarks") as DropDownList;
                        grademark += (ddlgrademarks.SelectedItem.Text) + ",";

                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                    }
                }
                if (studids == "")
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Entry Already Lock For Selected Students", this.Page);
                    return;
                }
            }
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();
            int courseNo = Convert.ToInt32(lblCourse.ToolTip);
            if (ddlSubExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlSubExam.SelectedValue.StartsWith("E"))
                examtype = "E";
            string examname = Convert.ToString(ViewState["exam_name"]);

            string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";
            string ExamName = Convert.ToString(subExam_Name.Split('-')[0]);
            CustomStatus cs;

            cs = (CustomStatus)objMarksEntry.InsertGradeEntry_External(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), grademark, publish_status, ExamName, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (publish_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Publish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Unpublish";    //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Unpublish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button

                }
                GetDataGrade(0);
                btnShow_Click(null, null);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (publish_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Publish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Unpublish";    //Commented on 30-09-2023 End sem Garde Entry Not required button

                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Grade Unpublish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                ShowStudents();
                btnShow_Click(null, null);
            }
            else if (cs.Equals(CustomStatus.Others))
            {
                objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined for Selected Subject.", this.Page);
                ShowStudents();
                btnShow_Click(null, null);
            }
            else
            {
                objCommon.DisplayMessage(this.updpanle1, "Error in Saving Marks!", this.Page);
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

    public void publishmarks(int pusblish_status)
    {
        try
        {
            string API_Output = "";
            string examtype = string.Empty;
            string que_out = string.Empty;
            if (CheckMarks(pusblish_status) == false)
            {
                return;
            }
            string studids = string.Empty;
            string marks = string.Empty;

            Label lbl;
            TextBox txtMarks;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Note : -100 for Marks will be converted as NULL           
            //NULL means mark entry not done.                           
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            int session_type = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

            if (session_type == 1)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";
                    //Gather Exam Marks 
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";

                }
            }
            else if (session_type == 2)
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;

                    if (txtMarks.Enabled != false)
                    {
                        //Gather Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                        studids += lbl.ToolTip + ",";
                        //Gather Exam Marks 
                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                    }
                }
                if (studids == "")
                {
                    objCommon.DisplayMessage(this.updpanle1, "Mark Entry Already Lock For Selected Students", this.Page);
                    return;
                }
            }
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();
            int courseNo = Convert.ToInt32(lblCourse.ToolTip);
            int FlagReval = 0;

            if (ddlSubExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlSubExam.SelectedValue.StartsWith("E"))
                examtype = "E";
            string examname = Convert.ToString(ViewState["exam_name"]);

            string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";

            // cs = (CustomStatus)objMarksEntry.UpdateMarkEntry_Grade_CC(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), pusblish_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));
            int examno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "EXAMNO", "SUBEXAMNO=" + Convert.ToInt32(Convert.ToString(subExam_Name.Split('-')[1]))));
            cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_CPU_ADMIN_COMPONANT(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), pusblish_status, examname, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(ViewState["SCHEMENO"]), Convert.ToInt32(hdfSection.Value), subExam_Name, examno, subExam_Name);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (pusblish_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Publish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Unpublish";    //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Unpublish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                btnShow_Click(null, null);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (pusblish_status == 1)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Publish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Unpublish";    //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Marks Unpublish Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //btnPublish.Text = "Publish";  //Commented on 30-09-2023 End sem Garde Entry Not required button
                }
                btnShow_Click(null, null);
            }

            else if (cs.Equals(CustomStatus.Others))
            {
                objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined for Selected Subject.", this.Page);
                btnShow_Click(null, null);
            }

            else
            {
                objCommon.DisplayMessage(this.updpanle1, "Error in Saving Marks!", this.Page);
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

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        if (btnPublish.Text == "Publish")
        {
            if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
            {
                if (Convert.ToBoolean(ViewState["LOCKSTATUS"]) == false)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Mark Entry Not Locked For Selected Students ! If you want Publish The marks for student?", this.Page);
                    publishgrade(1);
                    btnPublish.Text = "Unpublish";
                }
                else
                {
                    publishgrade(1);
                    btnPublish.Text = "Unpublish";
                }
            }
            else
            {
                if (Convert.ToBoolean(ViewState["LOCKSTATUS"]) == false)
                {
                    objCommon.DisplayMessage(this.updpanle1, "Mark Entry Not Locked For Selected Students ! If you want Publish The marks for student?", this.Page);
                    publishmarks(1);
                    btnPublish.Text = "Unpublish";
                }
                else
                {
                    publishmarks(1);
                    btnPublish.Text = "Unpublish";

                }
            }
        }
        else
        {
            if (Convert.ToInt32(ViewState["GRADEPATTERN"]) == 1)
            {

                publishgrade(0);
                btnPublish.Text = "Publish";


            }
            else
            {

                publishmarks(0);
                btnPublish.Text = "Publish";

            }

        }
    }
}


