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

public partial class ACADEMIC_EXAMINATION_MarksEntryByOperator_For_EndSem : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

    //string colTUT;
    //string colATT;
    //string colCE;
    //string colMID;
    //string colCT;

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

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
                // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = " + Request.QueryString["pageno"].ToString() + ")", "SESSIONNO DESC");

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1)", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");

                objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID"); 


                ddlSession.SelectedIndex = 1;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue, "DEGREENO");// + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")"
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus();
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                ddlSemester.Focus();
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void ShowCourses()
    //{
    //    DataSet ds = objMarksEntry.GetCourseForTeacher_FOR_OPERATOR(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

    //    if (ds != null && ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lblStatus.Visible = true;
    //            lblStatus.Text = "Marks Entry Status with Exam Name Coursewise.";
    //            lblStatus.ForeColor = System.Drawing.Color.Red;
    //            lvCourse.DataSource = ds.Tables[0];
    //            lvCourse.DataBind();
    //        }
    //        else
    //        {
    //            lblStatus.Text = "";
    //            lvCourse.DataSource = null;
    //            lvCourse.DataBind();
    //            objCommon.DisplayMessage("No Course found.", this.Page); lblStatus.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        lvCourse.DataSource = null;
    //        lvCourse.DataBind();
    //        objCommon.DisplayMessage("No Course found.", this.Page); lblStatus.Visible = false;
    //    }
    //}

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================
            LinkButton lnk = sender as LinkButton;
            if (!lnk.ToolTip.Equals(string.Empty))
            {
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                hdfSection.Value = sec_batch[0].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
                //GET THE EXAMS THAT ARE ON 
                //DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

                //new added on [10-sep-2016]
                int CourseNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COURSENO", "CCODE='" + lblCourse.ToolTip + "'"));

                DataSet dsExams = objMarksEntry.GetONExams_NEW_FOR_OPERATOR(Convert.ToInt32(CourseNo));
                string exams = string.Empty;
                if (dsExams != null && dsExams.Tables.Count > 0)
                {
                    if (dsExams.Tables[0].Rows.Count > 0)
                    {
                        DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                        while (dtr.Read())
                        {
                            exams += dtr["FLDNAME2"] == DBNull.Value ? string.Empty : dtr["FLDNAME2"].ToString() + ",";
                        }
                        dtr.Close();
                    }
                    else
                        objCommon.DisplayMessage("Exam for the Selected Course may be not be Started Or may be Locked!!!", this.Page);
                }
                else
                    objCommon.DisplayMessage("Exam for the Selected Course may be not be Started Or may be Locked!!!", this.Page);

                //If any exams are present then proceed
                if (exams.Length > 0)
                {
                    //Store exams to viewstate to access later
                    ViewState["exams"] = exams.Split(','); //store arrat
                    ViewState["exam"] = exams;

                    ddlExam.Items.Clear();
                    ddlExam.Items.Add(new ListItem("Select Exam", "0"));

                    DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                    while (dtr.Read())
                    {
                        if (dtr["FLDNAME2"] != DBNull.Value)
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
                                ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME2"].ToString()));
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
                        btnBack.Visible = false;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        lblStudents.Visible = false;
                        btnReport.Visible = false;
                        btnReport.Enabled = false;
                    }
                }
                else
                {
                    //gvStudent.DataSource = null;
                    //gvStudent.DataBind(); 
                    objCommon.DisplayMessage("No Exam for the Selected Course may be not be Started Or may be Locked!!!", this.Page);
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
        //0 - means - unlock
        SaveAndLock(0);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        //selection panel
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        GetStatus();
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

    private void SaveAndLock(int lock_status)
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

                //int courseno = Convert.ToInt32(lblCourse.ToolTip);
                string[] course = lblCourse.Text.Split('~');
                string ccode = course[0].Trim();

                if (ddlExam.SelectedValue.StartsWith("S"))
                    examtype = "S";
                else if (ddlExam.SelectedValue.StartsWith("E"))
                    examtype = "E";
                string examname = string.Empty;
                if (ddlExam.SelectedValue.Length > 2 && ddlExam.SelectedIndex > 0)
                    examname = ddlExam.SelectedValue.Substring(2);
                else if (ddlExam.SelectedIndex > 0)
                    examname = ddlExam.SelectedValue;

                CustomStatus cs = (CustomStatus)objMarksEntry.UpdateMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage("Marks Locked Successfully!!!", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    }
                    else
                        objCommon.DisplayMessage("Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);

                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    btnReport.Enabled = true;
                    ShowStudents();

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
                                        ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Check for Marks entered greater than Max Marks
                                    if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                                    {
                                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                    else if (Convert.ToInt16(txt.Text) < 0)
                                    {
                                        //Note : 401 for Absent and Not Eligible
                                        if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                                        {
                                        }
                                        else
                                        {
                                            ShowMessage("Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2 and -3 are allowed.");
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

    //private bool CheckMarks(int lock_status)
    //{
    //    bool flag = true;
    //    try
    //    {
    //        Label lbl;
    //        TextBox txt;
    //        string marks = string.Empty;
    //        string maxMarks = string.Empty;

    //        for (int j = 2; j <= gvStudent.Columns.Count; j++)    //columns
    //        {
    //            for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
    //            {
    //                if (gvStudent.Columns[2].Visible == true)
    //                {
    //                    if (j == 2) //TA MARKS
    //                    {
    //                        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblTAMarks") as Label;      //Max Marks 
    //                        txt = gvStudent.Rows[i].Cells[j].FindControl("txtTAMarks") as TextBox;    //Marks Entered 
    //                        maxMarks = lbl.Text.Trim();
    //                        marks = txt.Text.Trim();

    //                        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
    //                        {
    //                            if (txt.Text == "")
    //                            {
    //                                ShowMessage("Marks Entry Not Completed!!!");
    //                                txt.Focus();
    //                                flag = false;
    //                                break;
    //                            }
    //                            else
    //                            {
    //                                //Check for Marks entered greater than Max Marks
    //                                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                                {
    //                                    //Note : 401 for Absent
    //                                    if (Convert.ToInt16(txt.Text) == 401)
    //                                    {
    //                                    }
    //                                    else
    //                                    {
    //                                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                                        txt.Focus();
    //                                        flag = false;
    //                                        break;
    //                                    }
    //                                }
    //                            }

    //                            ////Check for Marks entered greater than Max Marks
    //                            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                            //{
    //                            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
    //                            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
    //                            //    {
    //                            //    }
    //                            //    else
    //                            //    {
    //                            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                            //        txt.Focus();
    //                            //        flag = false;
    //                            //        break;
    //                            //    }
    //                            //}
    //                        }
    //                        else
    //                        {
    //                            if (txt.Enabled == true)
    //                            {
    //                                if (lock_status == 1)
    //                                {
    //                                    ShowMessage("Marks Entry Not Completed!!!");
    //                                    txt.Focus();
    //                                    flag = false;
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }

    //                if (gvStudent.Columns[3].Visible == true)
    //                {
    //                    if (j == 3) //CT/FE MARKS
    //                    {
    //                        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT1Marks") as Label;      //Max Marks 
    //                        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT1Marks") as TextBox;    //Marks Entered 
    //                        maxMarks = lbl.Text.Trim();
    //                        marks = txt.Text.Trim();

    //                        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
    //                        {
    //                            if (txt.Text == "")
    //                            {
    //                                ShowMessage("Marks Entry Not Completed!!!");
    //                                txt.Focus();
    //                                flag = false;
    //                                break;
    //                            }
    //                            else
    //                            {
    //                                //Check for Marks entered greater than Max Marks
    //                                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                                {
    //                                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
    //                                    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
    //                                    {
    //                                    }
    //                                    else
    //                                    {
    //                                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                                        txt.Focus();
    //                                        flag = false;
    //                                        break;
    //                                    }
    //                                }
    //                            }

    //                            ////Check for Marks entered greater than Max Marks
    //                            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                            //{
    //                            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
    //                            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
    //                            //    {
    //                            //    }
    //                            //    else
    //                            //    {
    //                            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                            //        txt.Focus();
    //                            //        flag = false;
    //                            //        break;
    //                            //    }
    //                            //}
    //                        }
    //                        else
    //                        {
    //                            if (txt.Enabled == true)
    //                            {
    //                                if (lock_status == 1)
    //                                {
    //                                    ShowMessage("Marks Entry Not Completed!!!");
    //                                    txt.Focus();
    //                                    flag = false;
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }

    //                if (gvStudent.Columns[4].Visible == true)
    //                {

    //                    if (j == 4) //TA MARKS
    //                    {
    //                        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT2Marks") as Label;      //Max Marks 
    //                        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT2Marks") as TextBox;    //Marks Entered 
    //                        maxMarks = lbl.Text.Trim();
    //                        marks = txt.Text.Trim();

    //                        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
    //                        {
    //                            if (txt.Text == "")
    //                            {
    //                                ShowMessage("Marks Entry Not Completed!!!");
    //                                txt.Focus();
    //                                flag = false;
    //                                break;
    //                            }
    //                            else
    //                            {
    //                                //Check for Marks entered greater than Max Marks
    //                                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                                {
    //                                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
    //                                    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
    //                                    {
    //                                    }
    //                                    else
    //                                    {
    //                                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                                        txt.Focus();
    //                                        flag = false;
    //                                        break;
    //                                    }
    //                                }
    //                            }

    //                            ////Check for Marks entered greater than Max Marks
    //                            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                            //{
    //                            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
    //                            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
    //                            //    {
    //                            //    }
    //                            //    else
    //                            //    {
    //                            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                            //        txt.Focus();
    //                            //        flag = false;
    //                            //        break;
    //                            //    }
    //                            //}
    //                        }
    //                        else
    //                        {
    //                            if (txt.Enabled == true)
    //                            {
    //                                if (lock_status == 1)
    //                                {
    //                                    ShowMessage("Marks Entry Not Completed!!!");
    //                                    txt.Focus();
    //                                    flag = false;
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                if (gvStudent.Columns[6].Visible == true)
    //                {
    //                    if (j == 6) //TA-Pr MARKS
    //                    {
    //                        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblTAPrMarks") as Label;      //Max Marks 
    //                        txt = gvStudent.Rows[i].Cells[j].FindControl("txtTAPrMarks") as TextBox;    //Marks Entered 
    //                        maxMarks = lbl.Text.Trim();
    //                        marks = txt.Text.Trim();

    //                        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
    //                        {
    //                            if (txt.Text == "")
    //                            {
    //                                ShowMessage("Marks Entry Not Completed!!!");
    //                                txt.Focus();
    //                                flag = false;
    //                                break;
    //                            }
    //                            else
    //                            {
    //                                //Check for Marks entered greater than Max Marks
    //                                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
    //                                {
    //                                    //Note : 401 for Absent
    //                                    if (Convert.ToInt16(txt.Text) == 401)
    //                                    {
    //                                    }
    //                                    else
    //                                    {
    //                                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
    //                                        txt.Focus();
    //                                        flag = false;
    //                                        break;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (txt.Enabled == true)
    //                            {
    //                                if (lock_status == 1)
    //                                {
    //                                    ShowMessage("Marks Entry Not Completed!!!");
    //                                    txt.Focus();
    //                                    flag = false;
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                #region comment

    //                    //}
    //                    //else
    //                    //{
    //                    //    if (txt.Enabled == true)
    //                    //    {
    //                    //        //Grade marks
    //                    //        if (txt.Text.Trim().Equals("A") || txt.Text.Trim().Equals("B") || txt.Text.Trim().Equals("C") || txt.Text.Trim().Equals("D"))
    //                    //        {
    //                    //        }
    //                    //        else
    //                    //        {
    //                    //            if (lock_status == 1)
    //                    //            {
    //                    //                ShowMessage("Marks Entry Not Completed!!!");
    //                    //                txt.Focus();
    //                    //                flag = false;
    //                    //                break;
    //                    //            }
    //                    //            //else
    //                    //            //{
    //                    //            //    ShowMessage("Please Enter Marks in Range of A to D!!");
    //                    //            //    txt.Focus();
    //                    //            //    flag = false;
    //                    //            //    break;
    //                    //            //}
    //                    //        }
    //                    //    }
    //                //}
    //                #endregion


    //                if (flag == false) break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return flag;
    //}   

    //public string GetCourseName(object coursename, object schemename, object gpname)
    //{
    //    if (gpname == "")
    //    {
    //        return coursename.ToString() + " [" + schemename.ToString() + "]";
    //    }
    //    else
    //    {
    //        return coursename.ToString() + " [" + schemename.ToString() + "]" + "-" + "[" + gpname + "]";
    //    }
    //}

    #endregion

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        SaveAndLock(1);
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
        this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEWBy_Operator.rpt");//rptMarksList1.rpt
    }

    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();

        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + ddlExam.SelectedValue.ToString();
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
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
        //this.ShowCourses();
        this.GetStatus();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0)
        {
            ShowStudents();
            //ShowMessage("No Exams Available for Current Selection");
            //return;
        }
        else
        {
            objCommon.DisplayMessage("Please Select Exam!!", this.Page);
            ddlExam.Focus();
        }
    }

    private void ShowStudents()
    {
        try
        {
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            //if (ddlExam.SelectedValue.Length > 2)
            //dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), course[0].Trim(), Convert.ToInt32(ddlSubjectType.SelectedValue), ddlExam.SelectedValue.ToString(), Convert.ToInt32(lblDiv.ToolTip), Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(lbldept.ToolTip), Convert.ToInt32(lblSemesterno.ToolTip), Convert.ToInt32(lblBatch.ToolTip));
            //else
            //dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), course[0].Trim(), Convert.ToInt32(ddlSubjectType.SelectedValue), ddlExam.SelectedValue.ToString(), Convert.ToInt32(lblDiv.ToolTip), Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(lbldept.ToolTip), Convert.ToInt32(lblSemesterno.ToolTip), Convert.ToInt32(lblBatch.ToolTip));
            dsStudent = objMarksEntry.GetStudentsForMarkEntry_For_Operator(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), lblCourse.ToolTip, Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), ddlExam.SelectedValue.ToString());
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    if (ddlExam.SelectedValue == "EXTERMARK")
                    {
                        gvStudent.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvStudent.Columns[2].Visible = true;
                    }
                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                    {
                        gvStudent.Columns[3].HeaderText = ddlExam.SelectedItem.Text + " <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]";
                        gvStudent.Columns[3].Visible = true;

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                        ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
                    }
                    else
                    {
                        gvStudent.Columns[3].Visible = false; ViewState["LockStatus"] = "False";
                    }
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();

                    //for (int i = 0; i < gvStudent.Rows.Count; i++)
                    //{
                    //    TextBox txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    //    Label lblMarks = gvStudent.Rows[i].FindControl("lblMarks") as Label;

                    //    if (lblMarks.ToolTip == "True")
                    //    {
                    //        txtMarks.Enabled = false;
                    //    }
                    //}

                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Enabled = false; btnLock.Enabled = false;
                        objCommon.DisplayMessage("Selected Exam Not Applicable for Course!!", this.Page);
                    }
                    else
                    {
                        btnSave.Enabled = true; btnLock.Enabled = true;
                    }
                    if (ViewState["LockStatus"].ToString() == "True")
                    {
                        btnSave.Enabled = false; btnLock.Enabled = false;
                    }

                    pnlSelection.Visible = false; pnlMarkEntry.Visible = true; pnlStudGrid.Visible = true; lblStudents.Visible = true;
                    btnBack.Visible = true; btnSave.Visible = true; btnLock.Visible = true; btnReport.Visible = true; btnReport.Enabled = true;
                }
                else
                {
                    objCommon.DisplayMessage("Students Not Found..!!", this.Page);
                }
            }
            //else
            //{
            //    pnlStudGrid.Visible = false;
            //}
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
        //DataSet ds = objMarksEntry.GetCourseForTeacher(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        //if (ds != null && ds.Tables.Count > 0)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lstStatus.DataSource = ds.Tables[0];
        //        lstStatus.DataBind();
        //    }
        //    else
        //    {
        //        lstStatus.DataSource = null;
        //        lstStatus.DataBind();
        //    }
        //}
        //else
        //{
        //    lstStatus.DataSource = null;
        //    lstStatus.DataBind();
        //}
        TRCourses.Visible = false;
        DataSet ds = objMarksEntry.GetCourse_MarksEntryStatus_FOR_OPERATOR(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                TRStatus.Visible = true;
                //lblStatus.Visible = true;
                //lblStatus.Text = "Marks Entry Status with Exam Name Coursewise.";
                //lblStatus.ForeColor = System.Drawing.Color.Red;
                GVEntryStatus.DataSource = ds;
                GVEntryStatus.DataBind();
            }
            else
            {
                GVEntryStatus.DataSource = null;
                GVEntryStatus.DataBind();
                TRStatus.Visible = false;
                //lblStatus.Text = "";
                objCommon.DisplayMessage("No Course Found For This Subject Type.", this.Page); //lblStatus.Visible = false;
            }
        }
        else
        {
            GVEntryStatus.DataSource = null;
            GVEntryStatus.DataBind();
            objCommon.DisplayMessage("No Course Found For This Subject Type.", this.Page); //lblStatus.Visible = false;
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

                //for (int i = 0; i < row.Cells.Count; i++)
                //{
                //    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                //    {
                //        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                //                               previousRow.Cells[i].RowSpan + 1;
                //        previousRow.Cells[i].Visible = false;
                //    }
                //}



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
                    else
                    {
                        if (row.Cells[i].Text == previousRow.Cells[i].Text)
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

    protected void GVEntryStatus_RowDataBound(object sender, GridViewRowEventArgs e)//..........added on [20-09-2016]
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[2].Text)
            {
                case "PENDING":
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[2].BorderColor = System.Drawing.Color.Black;
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

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SUBJECTTYPE S ON (SR.SUBID = S.SUBID)", "DISTINCT  SR.SUBID", "S.SUBNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SEMESTERNO= " + ddlSemester.SelectedValue + " ", "SR.SUBID");//AND SR.PREV_STATUS = 0
                ddlSemester.Focus();
            }
            else
            {

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}