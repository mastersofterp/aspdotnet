//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ACADEMIC - ADMIN MARK ENTRY                                     
// CREATION DATE : 24-NOV-2009                                                     
// CREATED BY    : NIRAJ D .PHALKE                                                 
// MODIFIED BY   : 15-JUNE-2010 SANJAY S RATNAPARKHI                               
// MODIFIED DESC : MODIFIED AS PER GEC REQ.                                        
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

public partial class ACADEMIC_EXAMINATION_ChangeGradeEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
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
                this.Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlDegree,"ACD_DEGREE","DEGREENO","DEGREENAME","DEGREENO > 0","DEGREENAME");
                
                //this.FillDeptList();
                rfvOperator.Visible = false;

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        if (Session["usertype"].ToString() == "7")
            btnLock.Visible = false;
        else
            btnLock.Visible = true;
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearStudGrid();
       
        btnShow.Focus();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedIndex <= 0)
        {
            ddlCourse.SelectedIndex = 0;
            ddlStudent.SelectedIndex = 0;
            ClearStudGrid();
        }
        else
        {
            //FillExams();
            objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT_RESULT_HIST SR INNER JOIN ACD_STUDENT S ON (SR.IDNO = S.IDNO)", "SR.IDNO", "(S.REGNO+' - '+S.STUDNAME)NAME", "SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SR.EXAM_REGISTERED = 1 AND (CANCEL IS NULL OR CANCEL=0)", "S.REGNO");
            ddlExam.Focus();
        }
    }
    protected void ddlPath_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPath.SelectedIndex <= 0)
        {
            ddlPath.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ClearStudGrid();
        }
        else
        {
            FillCourseList();
            ddlCourse.Focus();
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDept.SelectedIndex <= 0)
        {
            ddlDept.SelectedIndex = 0;
            ddlPath.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ClearStudGrid();
        }
        else
        {
            FillSchemeList();
            ddlPath.Focus();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowStudents();
    }

    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Test Exam Mark Entry
            TextBox txtMarks = e.Row.FindControl("txtMarks") as TextBox;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            if (lblMarks.ToolTip.ToUpper().Equals("TRUE")) txtMarks.Enabled = false;

            TextBox txtNewMrk = e.Row.FindControl("txtNewMrk") as TextBox;
            Label lblNewMarks = e.Row.FindControl("lblNewMarks") as Label;
            if (lblNewMarks.ToolTip.ToUpper().Equals("TRUE")) txtNewMrk.Enabled = false;

            //txtMarks.Attributes.Add("onblur", "validateMark(this," + lblMarks.Text + ")");
            //gvStudent.Columns[2].HeaderText = ddlExam.SelectedItem.Text; // +" [" + lblMarks.Text.ToString() + "]";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //0 - means - unlock
        //if (ddlExam.SelectedIndex > 0)
        //{
            SaveAndLock(0);
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Please Select Exams to Lock!!!",this.Page);
        //    ddlExam.Focus();
        //}
    }
    
    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        //if (ddlExam.SelectedIndex > 0)
        //{
            SaveAndLock(1);
        //}
        //else
        //{
        //    objCommon.DisplayMessage( "Please Select Exams to Lock!!!", this.Page);
        //    ddlExam.Focus();
        //}
    }
    
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region Private Methods
    
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry_Admin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry_Admin.aspx");
        }
    }
    
    private void FillDeptList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_BRANCH B ON B.DEPTNO = D.DEPTNO INNER JOIN ACD_DEGREE DG ON B.DEGREENO = DG.DEGREENO", "DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO > 0 AND DG.DEGREENO =" + ddlDegree.SelectedValue, "D.DEPTNAME");
            if (Session["usertype"].ToString() == "3")
                objCommon.FillDropDownList(ddlDept, "ACD_BRANCH B INNER JOIN USER_ACC U ON (B.DEPTNO=U.UA_DEPTNO)", "BRANCHNO", "LONGNAME", "U.UA_TYPE=3 AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND DEGREENO = " + ddlDegree.SelectedValue, string.Empty);
            else
                objCommon.FillDropDownList(ddlDept, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, string.Empty);

            ClearStudGrid();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillDeptList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            
        }
    }

    private void FillSchemeList()
    {
        try
        {
            DataSet ds = objMarksEntry.GetSchemeForMarkEntry_Admin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlPath.Items.Clear();
                ddlPath.Items.Add(new ListItem("Please Select", "0"));
                ddlPath.DataTextField = "SCHEMENAME";
                ddlPath.DataValueField = "SCHEMENO";
                ddlPath.DataSource = ds;
                ddlPath.DataBind();
            }
            else
            {
                ddlPath.DataSource = null;
                ddlPath.DataBind();
            }
            ClearStudGrid();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillSchemeList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillCourseList()
    {
        try
        {
            DataSet ds = objMarksEntry.GetCourseForMarkEntry_Admin(Convert.ToInt32(ddlSession.SelectedValue),  Convert.ToInt32(ddlPath.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.DataTextField = "COURSENAME";
                ddlCourse.DataValueField = "COURSENO";
                ddlCourse.DataSource = ds.Tables[0];
                ddlCourse.DataBind();
            }
            else
            {
                ddlCourse.DataSource = null;
                ddlCourse.DataBind();
            }
            ClearStudGrid();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillCourseList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillExams()
    {
        try
        {
            DataSet ds = objMarksEntry.GetExamsForCourse_Admin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.DataTextField = "EXAMNAME";
                ddlExam.DataValueField = "FLDNAME";
                ddlExam.DataSource = ds.Tables[0];
                ddlExam.DataBind();                
            }
            else
            {
                ddlExam.DataSource = null;
                ddlExam.DataBind();
            }
            ClearStudGrid();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillCourseList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void FillControlSheetNo()
    //{
    //    try
    //    {
    //        ddlControlSheet.Items.Clear();
    //        DataSet ds = objMarksEntry.GetControlSheetNoByCourse_Admin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ddlExam.SelectedValue);
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            ddlControlSheet.DataTextField = ds.Tables[0].Columns[0].ToString();
    //            ddlControlSheet.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlControlSheet.DataSource = ds;
    //            ddlControlSheet.DataBind();
    //        }
    //        else
    //        {
    //            ddlControlSheet.DataSource = null;
    //            ddlControlSheet.DataBind();
    //        }
    //        ddlControlSheet.Items.Add(new ListItem("Select All", "0"));
    //        ddlControlSheet.SelectedIndex = ddlControlSheet.Items.Count - 1;

    //        ClearStudGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.FillControlSheetNo --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //public string GetStatus(object status)
    //{
    //    if (status.ToString().ToLower().Equals("false"))
    //        return "UnLocked";
    //    else
    //        return "Locked";
    //}
    //private void BindExams()
    //{
    //    try
    //    {
    //        DataSet ds = objMarksEntry.GetCourse_LockDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue));
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            lvExams.DataSource = ds;
    //            lvExams.DataBind();
    //        }
    //        else
    //        {
    //            lvExams.DataSource = null;
    //            lvExams.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.BindExams --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //
    
    private void ShowStudents()
    {
        try
        {
            //Check grade is given or not
            string check = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlPath.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND EXAM_REGISTERED=1 AND GRADE IS NOT NULL AND (CANCEL IS NULL OR CANCEL=0)");
            if (check == "0")
            {
                objCommon.DisplayMessage("Old grade is not found!!", this.Page);
                gvStudent.Visible = false;
                return;
            }
            if (ddlStudent.SelectedValue != "0")
            {
                if (ddlDept.SelectedIndex > 0 && ddlPath.SelectedIndex > 0 && ddlCourse.SelectedIndex > 0)
                {

                    string[] course = ddlCourse.SelectedItem.Text.Split('-');
                    DataSet dsStudent = objMarksEntry.GetStudentsForModifyGradeEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), course[0].Trim(), ddlExam.SelectedValue, Convert.ToInt32(ddlStudent.SelectedValue));

                    if (dsStudent != null && dsStudent.Tables.Count > 0)
                    {
                        btnSave.Enabled = true;
                        btnLock.Enabled = true;
                        btnUnlock.Enabled = true;
                        btnProcess.Enabled = true;
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            //if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                            //{
                            gvStudent.Columns[2].Visible = true;
                            //}
                            //else
                            //    gvStudent.Columns[1].Visible = false;


                            lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                            //Bind the Student List
                            gvStudent.DataSource = dsStudent;
                            gvStudent.DataBind();
                            gvStudent.DataSource = dsStudent;
                            gvStudent.DataBind();

                            pnlStudGrid.Visible = true;
                            btnSave.Enabled = true;
                            btnLock.Enabled = true;

                            if (dsStudent.Tables[0].Rows[0]["Lock"] != DBNull.Value && dsStudent.Tables[0].Rows[0]["Lock"] != null && dsStudent.Tables[0].Rows[0]["Lock"].ToString() != string.Empty)
                            {
                                if (Convert.ToBoolean(dsStudent.Tables[0].Rows[0]["Lock"]) == true && Session["usertype"].ToString() == "1")
                                    btnUnlock.Visible = true;
                                else
                                    btnUnlock.Visible = false;
                            }
                            else
                                btnUnlock.Visible = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage("No Students for Selected Course & Exam !!", this.Page);
                            pnlStudGrid.Visible = false;
                            btnSave.Enabled = false;
                            btnLock.Enabled = false;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Students for Selected Course & Exam !!", this.Page);
                        pnlStudGrid.Visible = false;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select proper values!!", this.Page);
                }
            }
            else
            {
                pnlStudGrid.Visible = false;
                btnSave.Enabled = false;
                btnLock.Enabled = false;
                btnUnlock.Enabled = false;
                btnProcess.Enabled = false;
                gvStudent.DataSource = null;
                gvStudent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ClearStudGrid()
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();

        pnlStudGrid.Visible = false;
        btnSave.Enabled = false;
        btnLock.Enabled = false;
    }

    private bool CheckMarks(int lock_status)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txt;

            for (int i = 0; i < gvStudent.Rows.Count; i++)
            {
                if (gvStudent.Columns[1].Visible == true)
                {
                    lbl = gvStudent.Rows[i].Cells[2].FindControl("lblMarks") as Label;      //Max Marks 
                    txt = gvStudent.Rows[i].Cells[2].FindControl("txtMarks") as TextBox;    //Marks Entered 

                    //if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty))
                    //{
                    //    //Check for Marks entered greater than Max Marks
                    //    //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                    //    //{
                    //        //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                    //        if (txt.Text == "401" || txt.Text == "402" || txt.Text == "403")
                    //        {
                    //        }
                    //        else
                    //        {
                    //            objCommon.DisplayMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]",this.Page);
                    //            txt.Focus();
                    //            flag = false;
                    //            break;
                    //        }
                    //    //}
                    //}
                    //else
                    //{
                        //if (lock_status == 1)
                        //{
                        //    objCommon.DisplayMessage( "Marks Entry Not Completed!!!", this.Page);
                        //    txt.Focus();
                        //    flag = false;
                        //    break;
                            
                        //}
                    //}
                }

                if (flag == false) break;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void SaveAndLock(int lock_status)
    {
        try
        {
            //check for if any exams on
           
                int studids = 0;
                string marks = string.Empty;
                string newMarks = string.Empty;

                MarksEntryController objMarksEntry = new MarksEntryController();
                Label lbl;
                TextBox txtMarks;
                TextBox txtNewMrk;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //Note : -100 for Marks will be converted as NULL           
                //NULL means mark entry not done.                           
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    txtNewMrk = gvStudent.Rows[i].FindControl("txtNewMrk") as TextBox;
                    if (txtNewMrk.Text != "")
                    {
                        //Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                        studids = Convert.ToInt32(lbl.ToolTip);

                        //Exam Marks 
                        txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                        marks = txtMarks.Text.Trim() == string.Empty ? "-100" : txtMarks.Text.Trim();

                        //New Grade
                        newMarks = txtNewMrk.Text.Trim() == string.Empty ? "-100" : txtNewMrk.Text.Trim();
                    }
                }

                int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
                string[] course = ddlCourse.SelectedItem.Text.Split('-');
                string ccode = course[0].Trim();

                string examtype = "T";
                if (ddlExam.SelectedValue.StartsWith("T"))
                    examtype = "T";
                else if (ddlExam.SelectedValue.StartsWith("S"))
                    examtype = "S";
                else if (ddlExam.SelectedValue.StartsWith("E"))
                    examtype = "E";
                
               // CustomStatus cs = (CustomStatus)objMarksEntry.UpdateNewGrade(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids, marks,newMarks,lock_status, ddlExam.SelectedValue, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                CustomStatus cs = (CustomStatus)objMarksEntry.UpdateNewGrade(Convert.ToInt32(ddlSession.SelectedValue), courseno,studids, marks, newMarks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());

            if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                       objCommon.DisplayMessage("Grade Locked Successfully!!!",this.Page);
                    else
                        objCommon.DisplayMessage("Grade Saved Successfully!!!",this.Page);

                    ShowStudents();
                }
                else
                    objCommon.DisplayMessage("Error in Saving Marks!",this.Page);
            }
       
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckExamActivity()
    {
        //GET THE EXAMS THAT ARE ON 
        DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

        string exams = string.Empty;

        if (dsExams != null && dsExams.Tables.Count > 0)
        {
            if (dsExams.Tables[0].Rows.Count <= 0)
            {
                return false;
            }
            else
                return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            //if (this.CheckExamActivity() == true)
            //{
                pnlMain.Visible = true;
                ddlOperator.Focus();

                if (Session["usertype"].ToString() == "1")  //User Type - Admin 
                {
                    //objCommon.FillDropDownList(ddlOperator, "WIN_OPERATOR", "WIN_OPERATOR_NO", "OPERATOR_NAME", "WIN_OPERATOR_NO > 0 AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "WIN_OPERATOR_NO");
                    //rfvOperator.Visible = true;
                }
                else
                {
                    rfvOperator.Visible = false;
                    rowOperator.Style.Add("display", "none");
                }
            //}
            //else
            //{
            //    ddlSession.SelectedIndex = 0;
            //    pnlMain.Visible = false;
            //    objCommon.DisplayMessage("Exam Entry Stopped for Selected Session. Please Contact Admin!!", this.Page);
            //    pnlMain.Visible = false;
            //}
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlDept.SelectedIndex = 0;
            ddlPath.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ClearStudGrid();
        }


    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        MarksEntryController objMarkEntryContr = new MarksEntryController();
        objExam.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        objExam.SchemeNo = Convert.ToInt32(ddlPath.SelectedValue);
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SEMESTERNO", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
        objExam.SemesterNo = semesterno;
        string idnos = string.Empty;
        //string idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO ='" + txtEnrollno.Text + "' AND ENROLLNO IS NOT NULL"));
        string ipAddress = Convert.ToString(ViewState["ipAddress"]);
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            TextBox txtNewMrk = gvStudent.Rows[i].FindControl("txtNewMrk") as TextBox;
            if (txtNewMrk.Text != "")
            {
                Label lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                idnos += lbl.ToolTip + ",";
            }
        }
        idnos = idnos.Remove(idnos.Length - 1);

        string ret2 = objMarkEntryContr.MarkEntryResultProc(objExam, ipAddress, idnos);
        if (ret2 == "1")
        {
            objCommon.DisplayMessage("Result Processed  Successfully", this.Page);

        }
        else
        {
            objCommon.DisplayMessage("Error in Result Processing", this.Page);
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDeptList();
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            string idnos = string.Empty;
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            string[] course = ddlCourse.SelectedItem.Text.Split('-');
            string ccode = course[0].Trim();

            string examtype = "E";
            if (ddlExam.SelectedValue.StartsWith("T"))
                examtype = "T";
            else if (ddlExam.SelectedValue.StartsWith("S"))
                examtype = "S";
            else if (ddlExam.SelectedValue.StartsWith("E"))
                examtype = "E";

            int opNo = Convert.ToInt32(ddlOperator.SelectedValue);

            for (int i = 0; i < gvStudent.Rows.Count; i++)
            {
                TextBox txtNewMrk = gvStudent.Rows[i].FindControl("txtNewMrk") as TextBox;
                if (txtNewMrk.Text != "")
                {
                    Label lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    idnos += lbl.ToolTip + ",";
                }
            }
            idnos = idnos.Remove(idnos.Length - 1);

            CustomStatus cs = (CustomStatus)objMarksEntry.UnlockChangeGrade(Convert.ToInt32(ddlSession.SelectedValue), courseno, idnos, ccode, ddlExam.SelectedValue, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, opNo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Grade Unlocked Successfully!!!", this.Page);
                ShowStudents();
            }
            else
                objCommon.DisplayMessage("Error in Saving ..!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.ddlDegree_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowStudents();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ShowStudents();
    }
}
