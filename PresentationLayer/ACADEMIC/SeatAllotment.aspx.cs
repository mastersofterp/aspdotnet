//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Seating Arrangement Entry                                                         
// CREATION DATE : 26-MAR-2014
// CREATED BY    : UMESH K. GANORKAR                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;

public partial class ACADEMIC_SeatAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    SeatingController objSc = new SeatingController();
    string courses = string.Empty;

    #region PAGE LOAD

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // Set form mode equals to -1(New Mode).
                // ViewState["exdtno"] = "0";

                this.PopulateDropDown();
                //this.BindDates();
                divMsg.InnerHtml = string.Empty;
                //lvDate.Enabled = true;
            }
        }
    }

    #endregion

    #region PRIVATE METHODS

    private void PopulateDropDown()
    {
        try
        {
            //Term
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND FLOCK = 1", "SESSIONNO DESC");

            //College Name
            objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID DESC");


            //Exam Time Table Types
            //ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
            //ddlExTTType.Items.Add(new ListItem("Mid Exam Time Table", "1"));
            //ddlExTTType.Items.Add(new ListItem("End Exam Time Table", "2"));

            //Floor Slot
            objCommon.FillDropDownList(ddlFloor, "ACD_FLOOR", "FLOORNO", "FLOORNAME", "FLOORNO > 0", "FLOORNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindCoursesList()
    {
        try
        {
            DataSet ds = objSc.GetCoursesForSeatAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                lvCourse.DataSource = ds.Tables[0];
                lvCourse.DataBind();
                foreach (ListViewDataItem item in lvCourse.Items)
                {
                    CheckBox chk1 = item.FindControl("cbRow1") as CheckBox;
                    chk1.Checked = true;
                }
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.ddlDepartment_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindStudentList()
    {
        try
        {
            try
            {
                DataSet ds = objSc.GetStudentsForSeatAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
                if (ds != null && ds.Tables[0].Rows.Count > 0)

                    if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        int i = 0;
                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            CheckBox cbrow = item.FindControl("cbRow") as CheckBox;

                            cbrow.Checked = false;
                            //txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();

                            i++;
                        }
                        hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                        //pnlMain.Visible = true;
                        btnSubmit.Enabled = true;
                        btnReport.Enabled = true;
                        btnExamSheet.Enabled = true;

                    }
                    else
                    {
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        hdfTot.Value = "0";

                    }
                //else
                //{
                //    lblChkStud.Visible = true;
                //    lblChkStud.Text = "Registered Students not found for Course " + ddlCourse.SelectedItem.Text + "";
                //}
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_Examination.BindListView-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.BindStudentList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportinFormate(string reportTitle, string rptFileName)
    {
        int examday = 0;
        int slotno = 0;
        if (Convert.ToInt16(ddlCourse.SelectedValue) > 0)
        {
            foreach (ListViewDataItem item in lvCourse.Items)
            {
                CheckBox chk1 = item.FindControl("cbRow1") as CheckBox;
                Label ExamDate = item.FindControl("lblExamDate") as Label;
                Label Slot = item.FindControl("lblSlot") as Label;

                if (chk1.Checked)
                {
                    examday = Convert.ToInt16(ExamDate.ToolTip);
                    slotno = Convert.ToInt16(Slot.ToolTip);
                }
                break;
            }
        }
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAMNO=" + ddlExTTType.SelectedValue + ",@P_DAYNO=" + examday + ",@P_ROOMNO=" + ddlRoom.SelectedValue + ",@P_SLOTNO=" + slotno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #endregion

    #region BTN Event

    protected void btnFilterStud_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0 || ddlDegree.SelectedIndex == 0 || ddlExTTType.SelectedIndex == 0 ||
            ddlBranch.SelectedIndex == 0 || ddlSemester.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updExamdate, "Please Select Session/Exam/Degree/Branch/Semester", this.Page);
        }
        else
        {
            BindStudentList();
        }
    }

    protected void btnCancel0_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            int roomcapacitychk = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(ROOMNO)", "SESSIONNO =" + ddlSession.SelectedValue + " AND ROOMNO = " + ddlRoom.SelectedValue + " AND  EXAMNO = " + ddlExTTType.SelectedValue + " "));

            if (txtTotStud.Text == "")
                txtTotStud.Text = "0";

            if (Convert.ToInt32(txtTotStud.Text) > Convert.ToInt32(txtRemainCapacity.Text))
            {
                objCommon.DisplayMessage(this.updExamdate, "Selected Students is More than Room Capacity..!!", this.Page);
                return;
            }

            string idnos = string.Empty;
            string regnos = string.Empty;
            string courseno = string.Empty;
            string slotno = string.Empty;
            string examdate = string.Empty;
            int i = 0;

            foreach (ListViewDataItem item in lvStudent.Items)//GETTING STUDENT COUNT.
            {
                CheckBox chk = item.FindControl("cbRow") as CheckBox;
                HiddenField regno = item.FindControl("hdfRegno") as HiddenField;
                if (i < Convert.ToInt32(txtTotStud.Text))
                    chk.Checked = true;

                i++;

                if (chk.Checked)
                {
                    idnos += chk.ToolTip + ",";
                    regnos += regno.Value + ",";
                }
            }
            foreach (ListViewDataItem item in lvCourse.Items)//GETTING COURSE COUNT.
            {
                CheckBox chk1 = item.FindControl("cbRow1") as CheckBox;
                Label ExamDate = item.FindControl("lblExamDate") as Label;
                Label Slot = item.FindControl("lblSlot") as Label;


                if (chk1.Checked)
                {
                    courseno += chk1.ToolTip + ",";
                    examdate += ExamDate.Text + ",";
                    slotno += Slot.ToolTip + ",";
                }
            }
            if (idnos == string.Empty || regnos == string.Empty)//CHECK WHETHER STUDENT SELECTED OR NOT.
            {
                objCommon.DisplayMessage(this.updExamdate, "Please Select Students from List..", this.Page);
                return;
            }
            if (courseno == string.Empty)//CHECK WHETHER COURSE SELECTED OR NOT.
            {
                objCommon.DisplayMessage(this.updExamdate, "Please Select Course from List..", this.Page);
                return;
            }


            CustomStatus cs = (CustomStatus)objSc.SeatAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlRoom.SelectedValue), idnos, regnos, courseno, examdate, slotno, Session["colcode"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updExamdate, "Seating Arrangement Done...!!", this.Page);
                //ddlCourse.SelectedIndex = 0;
                // ddlBlocks.SelectedIndex = 0;
                txtTotStud.Text = string.Empty;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "Error while Seating Arrangement..!!", this.Page);
            }

            this.BindStudentList();
            int roomcapacity = Convert.ToInt32(objCommon.LookUp("ACD_ROOM", "ROOMCAPACITY", "ROOMNO = " + ddlRoom.SelectedValue + ""));
            txtRoomCapacity.Text = Convert.ToString(roomcapacity);
            //ddlDay.SelectedIndex = 0;
            //ddlSlot.SelectedIndex = 0;
            //if (ddlDay.SelectedIndex == 0)
            //{

            //    txtRemainCapacity.Text = string.Empty;
            //}
            //if (ddlSlot.SelectedIndex == 0)
            //{
            //    txtRemainCapacity.Text = string.Empty;
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_EndSemesterAttendanceSheet.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportinFormate("SEATALLOTMENT", "rptSeatAllot.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnExamSheet_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowExamSheetReport("SeatingArrangement_ExamSheet", "rptSeatArrangementExamSheet.rpt");
            ShowReportinFormate("SeatAllotExamSheet", "rptSeatAllotExamSheet.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CoursewiseStudentReport2.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region DDL Event

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //   BindCoursesList();

        if (ddlCourse.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlExTTType, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON(C.SCHEMENO=S.SCHEMENO)INNER JOIN ACD_EXAM_NAME E ON(E.PATTERNNO=S.PATTERNNO)", "E.EXAMNO", "E.EXAMNAME", "EXAMNAME <>'' AND  C.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "EXAMNO DESC");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Course!", this.Page);
            ddlCourse.Focus();
        }
    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeName.SelectedIndex > 0)
        {
            //Degree
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE C INNER JOIN ACD_DEGREE D ON (C.DEGREENO=D.DEGREENO)", "C.DEGREENO", "DEGREENAME", "C.DEGREENO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue), "DEGREENO");
        }
        else
        {
            objCommon.DisplayMessage("Please Select College Name!", this.Page);
            ddlCollegeName.Focus();
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Branch!", this.Page);
            ddlBranch.Focus();
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SeatAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            //if (ddlBranch.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Please Select Branch!", this.Page);
            //    ddlBranch.Focus();
            //}

            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue), "B.BRANCHNO");

            }
            else
            {
                objCommon.DisplayMessage("Please Select Branch!", this.Page);
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SeatAllotment.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlExTTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvCourse.DataSource = null;
        //lvCourse.DataBind();
        lvStudent.DataSource = null;

        lvStudent.DataBind();
        BindCoursesList();

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSession.SelectedIndex >= 0)
        //{
        //    ddlExTTType.SelectedIndex = 0;
        //    ddlDegree.SelectedIndex = 0;
        //    ddlBranch.SelectedIndex = 0;
        //    ddlSemester.SelectedIndex = 0;
        //    ddlRoom.SelectedIndex = 0;
        //}
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        //objCommon.FillDropDownList(ddlCourse, "ACD_EXAM_DATE  E INNER JOIN ACD_COURSE C ON (C.COURSENO = E.COURSENO)", "DISTINCT E.COURSENO", "(E.CCODE +' - '+C.COURSE_NAME) AS COURSE_NAME", "E.SESSIONNO = " + ddlSession.SelectedValue + " AND E.SEMESTERNO =  " + ddlSemester.SelectedValue + " AND E.EXAM_TT_TYPE = " + ddlExTTType.SelectedValue + "  AND E.BRANCHNO=" + ddlBranch.SelectedValue + "  AND E.DEGREENO=" + ddlDegree.SelectedValue + "", "E.COURSENO");

        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE E INNER JOIN ACD_SCHEME S ON E.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON E.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO ", "DISTINCT E.COURSENO", "E.CCODE + ' - ' + E.COURSE_NAME  AS COURSE_NAME", "E.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND E.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "E.COURSENO");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Semester!", this.Page);
            ddlSemester.Focus();
        }
        // BindCoursesList();
    }

    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoom.SelectedIndex == 0)
        {
            txtRoomCapacity.Text = string.Empty;
            txtRemainCapacity.Text = string.Empty;
        }
        else
        {
            int roomcapacity = Convert.ToInt32(objCommon.LookUp("ACD_ROOM", "ROOMCAPACITY", "ROOMNO = " + ddlRoom.SelectedValue + ""));
            txtRoomCapacity.Text = Convert.ToString(roomcapacity);

            //int roomcount = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(ROOMNO)", "SESSIONNO =" + ddlSession.SelectedValue + " AND ROOMNO = " + ddlRoom.SelectedValue + " AND  EXAMNO = " + ddlExTTType.SelectedValue + " "));

            //txtRemainCapacity.Text = Convert.ToString(roomcapacity - roomcount);
        }
        int examday = 0;
        int slotno = 0;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            CheckBox chk1 = item.FindControl("cbRow1") as CheckBox;
            Label ExamDate = item.FindControl("lblExamDate") as Label;
            Label Slot = item.FindControl("lblSlot") as Label;
            //if (i < Convert.ToInt32(txtTotStud.Text))
            //    chk1.Checked = true;

            //i++;

            if (chk1.Checked)
            {

                examday = Convert.ToInt16(ExamDate.ToolTip);
                slotno = Convert.ToInt16(Slot.ToolTip);
            }
            break;
        }
        int roomcapacity1 = Convert.ToInt32(objCommon.LookUp("ACD_ROOM", "ROOMCAPACITY", "ROOMNO = " + ddlRoom.SelectedValue + ""));
        //txtRoomCapacity.Text = Convert.ToString(roomcapacity);

        int roomcount = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "ISNULL(COUNT(ROOMNO),0)ROOMNO", "SESSIONNO=" + ddlSession.SelectedValue + " AND ROOMNO=" + ddlRoom.SelectedValue + " AND  EXAMNO=" + ddlExTTType.SelectedValue + " AND DAYNO=" + examday + " AND SLOTNO=" + slotno + ""));

        txtRemainCapacity.Text = Convert.ToString(roomcapacity1 - roomcount);
    }

    protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFloor.SelectedIndex > 0)
        {
            //Room Slot
            objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0 AND FLOORNO=" + Convert.ToInt32(ddlFloor.SelectedValue), "ROOMNO");

        }
        else
        {
            objCommon.DisplayMessage("Please Select Floor!", this.Page);
            ddlFloor.Focus();
        }
    }

    #endregion
}
