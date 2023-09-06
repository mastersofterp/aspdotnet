using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using System.Net;

public partial class ACADEMIC_TimeTable_AlternetAttendance : System.Web.UI.Page
{
    #region Page Events

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAtt = new AcdAttendanceController();
    int done = 0;
    int i = 0;
    int j = 0;

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
                    // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PopulateDropDownList();
                    MakeCalender();

                    if (Convert.ToInt32(Session["usertype"].ToString()) == 1 || Convert.ToInt32(Session["usertype"].ToString()) == 8)
                    {
                        //  btnDetailReport.Visible = false;
                        btnShow.Visible = false;
                        btnSubmit.Visible = false;
                        btnDetailReport.Visible = true;
                    }
                    else
                    {
                        //  btnDetailReport.Visible = false;
                        btnShow.Visible = true;
                        btnSubmit.Visible = true;

                    }
                    objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
                }
            }
            else
            {
                divMsg.InnerHtml = string.Empty;
            }


        }
        catch
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
                Response.Redirect("~/notauthorized.aspx?page=AlternetAttendance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AlternetAttendance.aspx");
        }
    }

    #endregion

    // Clears all selection
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    // Used to fill dropdownlist
    protected void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "");
            }
            else
            {
                objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COURSE_TEACHER CT ON (CM.COLLEGE_ID = CT.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "ISNULL(CANCEL,0)=0 AND UA_NO = " + Session["userno"], "");
            }
            if (Session["usertype"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlInstitute1, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "");
            }
            else
            {
                objCommon.FillDropDownList(ddlInstitute1, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COURSE_TEACHER CT ON (CM.COLLEGE_ID = CT.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "ISNULL(CANCEL,0)=0 AND UA_NO = " + Session["userno"], "");
            }
            objCommon.FillDropDownList(ddlAttType, "ACD_TT_CLASSTYPE_MASTER", "CLASSTYPENO", "CLASSTYPE", "CLASSTYPENO IN(2,4)", "CLASSTYPENO");
            objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0", "D.DEPTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Clears all controls
    private void ClearControls()
    {
        ddlAttType.SelectedIndex = 0;
        txtAttDate.Text = string.Empty;
        lvCourse.Visible = false;
    }

    // Binds data to first listview of the page (lvCourse).
    private void BindListViewCourseSlot()
    {
        try
        {
            if (txtAttDate.Text != string.Empty)
            {
                DataSet ds = null;

                if (Session["userno"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }

                ds = objAtt.GetScheduleFacultyCourses(Convert.ToInt32(Session["userno"]), Convert.ToDateTime(txtAttDate.Text), Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds;
                    Session["dslvCourse"] = ds;
                    lvCourse.DataBind();
                    lvCourse.Visible = true;

                    foreach (ListViewDataItem lvHead in lvCourse.Items)
                    {
                        CheckBox chkCourse = lvHead.FindControl("chkCourse") as CheckBox;
                        Label lblCourseName = lvHead.FindControl("lblCourseName") as Label;
                        Label lblScheduleName = lvHead.FindControl("lblScheduleName") as Label;
                        Label lblTransferType = lvHead.FindControl("lblTransferType") as Label;
                        Label lblTakenName = lvHead.FindControl("lblTakenName") as Label;
                        Label lblSlot = lvHead.FindControl("lblSlot") as Label;
                        Label lblDate = lvHead.FindControl("lblDate") as Label;
                        DropDownList ddlFaculty = lvHead.FindControl("ddlFaculty") as DropDownList;
                        HiddenField hdnTakenUano = lvHead.FindControl("hdnTakenUano") as HiddenField;
                        HiddenField hdnTakenUano1 = lvHead.FindControl("hdnTakenUano1") as HiddenField;
                        HiddenField hdnTakenCourse = lvHead.FindControl("hdnTakenCourse") as HiddenField;
                        HiddenField hdnScheme = lvHead.FindControl("hdnScheme") as HiddenField;
                        HiddenField hdnSemester = lvHead.FindControl("hdnSemester") as HiddenField;
                        HiddenField hdnSection = lvHead.FindControl("hdnSection") as HiddenField;
                        HiddenField hdnBatch = lvHead.FindControl("hdnBatch") as HiddenField;

                        objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + UA_FULLNAME) AS UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");

                        int scheduleuano = lblScheduleName.ToolTip == string.Empty ? 0 : Convert.ToInt32(lblScheduleName.ToolTip);
                        int takenuano = hdnTakenUano.Value == string.Empty ? 0 : Convert.ToInt32(hdnTakenUano.Value);

                        ddlFaculty.SelectedValue = Convert.ToString(takenuano);

                        if (scheduleuano == takenuano)
                        {
                            takenuano = hdnTakenUano1.Value == string.Empty ? 0 : Convert.ToInt32(hdnTakenUano1.Value);
                        }

                        int takencourse = hdnTakenCourse.Value == string.Empty ? 0 : Convert.ToInt32(hdnTakenCourse.Value);
                        int courseno = chkCourse.ToolTip == string.Empty ? 0 : Convert.ToInt32(chkCourse.ToolTip);
                        int slotno = lblSlot.ToolTip == string.Empty ? 0 : Convert.ToInt32(lblSlot.ToolTip);
                        int transfertype = lblTransferType.ToolTip == string.Empty ? 0 : Convert.ToInt32(lblTransferType.ToolTip);

                        int AttDone = objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + Convert.ToInt32(lblDate.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(hdnScheme.Value) + " AND SEMESTERNO=" + Convert.ToInt32(hdnSemester.Value) + " AND SECTIONNO=" + Convert.ToInt32(hdnSection.Value) + " AND ISNULL(BATCHNO,0)=" + Convert.ToInt32(hdnBatch.Value) + " AND SLOTNO=" + slotno + " AND COURSENO=" + courseno + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CANCEL,0)=0 AND CAST(ATT_DATE AS DATE)=CONVERT(DATE,'" + txtAttDate.Text + "',103)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + Convert.ToInt32(lblDate.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(hdnScheme.Value) + " AND SEMESTERNO=" + Convert.ToInt32(hdnSemester.Value) + " AND SECTIONNO=" + Convert.ToInt32(hdnSection.Value) + " AND ISNULL(BATCHNO,0)=" + Convert.ToInt32(hdnBatch.Value) + " AND SLOTNO=" + slotno + " AND COURSENO=" + courseno + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CANCEL,0)=0 AND CAST(ATT_DATE AS DATE)=CONVERT(DATE,'" + txtAttDate.Text + "',103)"));

                        if (AttDone != 0)
                        {
                            chkCourse.Checked = true;
                            chkCourse.Enabled = false;
                        }
                        else
                        {
                            chkCourse.Checked = false;
                            chkCourse.Enabled = true;
                        }

                        if (scheduleuano != 0)
                        {
                            if (transfertype == 4)
                            {
                                done = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE AA", "MAX(AA.AANO)", " AA.SCHEDULE_UANO=" + scheduleuano + " AND TAKEN_COURSENO=" + takencourse + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE AA", "MAX(AA.AANO)", " AA.SCHEDULE_UANO=" + scheduleuano + " AND TAKEN_COURSENO=" + takencourse + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                            }
                            else
                            {
                                done = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE AA", "MAX(AA.AANO)", " AA.SCHEDULE_UANO=" + scheduleuano + " AND AA.TAKEN_UANO=" + takenuano + " AND TAKEN_COURSENO=" + takencourse + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE AA", "AA.AANO", " AA.SCHEDULE_UANO=" + scheduleuano + " AND AA.TAKEN_UANO=" + takenuano + " AND TAKEN_COURSENO=" + takencourse + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                            }

                            if (done != 0)
                            {
                                int cancelleduano = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "ISNULL(CANCEL,0)CANCEL", "AANO=" + done + "AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "ISNULL(CANCEL,0)CANCEL", "AANO=" + done + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));

                                if (cancelleduano == 0)
                                {
                                    chkCourse.Checked = true;
                                    chkCourse.Enabled = false;
                                }
                                else
                                {
                                    chkCourse.Checked = false;
                                    chkCourse.Enabled = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updatt, "No Data Found", this.Page);
                }

            }
            else
            {
                //objCommon.DisplayMessage(updatt, "No Subject is Alloted to Specified Faculty ", this);
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
            }

        }
        catch
        {
            throw;
        }
    }

    // Show button click event displaying listview based on selection 
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlAttType.SelectedIndex > 0)
        {
            BindListViewCourseSlot();
            lvFacultyCourse.Visible = false;
            lvFacultyCourseMutual.Visible = false;

            foreach (ListViewDataItem lvHead1 in lvCourse.Items)
            {
                DropDownList ddlFaculty = lvHead1.FindControl("ddlFaculty") as DropDownList;
            }
        }
        else
        {
            lvCourse.Visible = false;
        }
        MakeCalender();
    }

    // Calls method to Bind data to second listview of the page (lvFacultyCourse, lvFacultyCourseMutual) based on selected faculty in dropdownlist
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAttType.SelectedValue == "4" || ddlAttType.SelectedValue == "2")
            {
                foreach (ListViewDataItem lvHead in lvCourse.Items)
                {
                    DropDownList ddlFaculty = lvHead.FindControl("ddlFaculty") as DropDownList;
                    CheckBox chkCourse = lvHead.FindControl("chkCourse") as CheckBox;

                    if (chkCourse.Enabled && chkCourse.Checked)
                    {
                        if (ddlFaculty.SelectedIndex == 0)
                        {
                            btnSubmit.Enabled = false;
                            lvFacultyCourse.Visible = false;
                            lvFacultyCourseMutual.Visible = false;
                        }
                        else
                        {
                            btnSubmit.Enabled = true;
                            lvFacultyCourse.Visible = true;
                            lvFacultyCourseMutual.Visible = true;
                        }
                    }
                }

                CourseController objCC = new CourseController();
                BindListViewSelectedFaculty();

                if (ddlAttType.SelectedValue == "4")
                {
                    lvFacultyCourse.Visible = true;
                    lvFacultyCourseMutual.Visible = false;
                }
                else if (ddlAttType.SelectedValue == "2")
                {
                    lvFacultyCourseMutual.Visible = true;
                    lvFacultyCourse.Visible = false;
                }

            }
            else
            {
                lvFacultyCourse.Visible = false;
                lvFacultyCourseMutual.Visible = false;
                btnSubmit.Enabled = true;
            }
        }
        catch
        {
            throw;
        }
    }

    // Checks the Attendance type
    protected void ddlAttType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvFacultyCourse.Visible = false;
        lvFacultyCourseMutual.Visible = false;

        btnSubmit.Enabled = false;

        foreach (ListViewDataItem lvHead1 in lvCourse.Items)
        {
            DropDownList ddlFaculty = lvHead1.FindControl("ddlFaculty") as DropDownList;
            ddlFaculty.Enabled = false;
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
            ddlFaculty.SelectedIndex = 0;
        }

        ddlDept.SelectedIndex = 0;

        if (ddlAttType.SelectedIndex <= 0)
        {
            divddldept.Visible = false;
            lvCourse.Visible = false;
            lvFacultyCourse.Visible = false;

            foreach (ListViewDataItem lvHead1 in lvCourse.Items)
            {
                DropDownList ddlFaculty = lvHead1.FindControl("ddlFaculty") as DropDownList;
                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Add("Please Select");
                ddlFaculty.SelectedIndex = 0;
            }
        }
        else
        {
            // lvCourse.Visible = false;
            BindListViewCourseSlot();
            if (ddlAttType.SelectedValue == "3")
            {
                divddldept.Visible = true;
            }
            else
            {
                divddldept.Visible = false;
            }

            foreach (ListViewDataItem lvHead1 in lvCourse.Items)
            {
                DropDownList ddlFaculty = lvHead1.FindControl("ddlFaculty") as DropDownList;
                ddlFaculty.SelectedIndex = 0;
            }
        }
        lvCourse.Visible = false;
    }

    // Submit button click event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AttendanceTransfer();
    }

    //Binds the data to ListView (lvFacultyCourse, lvFacultyCourseMutual)
    private void BindListViewSelectedFaculty()
    {
        try
        {
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                DropDownList ddlFaculty = dataitem.FindControl("ddlFaculty") as DropDownList;
                CheckBox chkCourse = dataitem.FindControl("chkCourse") as CheckBox;
                HiddenField hdnSemester = dataitem.FindControl("hdnSemester") as HiddenField;
                HiddenField hdnSection = dataitem.FindControl("hdnSection") as HiddenField;
                HiddenField hdnBatch = dataitem.FindControl("hdnBatch") as HiddenField;
                HiddenField hdnScheme = dataitem.FindControl("hdnScheme") as HiddenField;
                Label lblSlot = dataitem.FindControl("lblSlot") as Label;
                Label lblDate = dataitem.FindControl("lblDate") as Label;

                if (chkCourse.Enabled && chkCourse.Checked)
                {
                    if (ddlFaculty.SelectedValue != "0")
                    {
                        int semesterno = 0;
                        int sectionno = 0;
                        int batchno = 0;
                        int schemeno = 0;
                        int slotno = 0;
                        int sessionno = 0;
                        int College_id = 0;
                        int deptno = 0;
                        string[] uano_dept = null;
                        int ua_no = 0;
                        if (ddlAttType.SelectedValue == "2")
                        {

                            semesterno = Convert.ToInt32(hdnSemester.Value);
                            sectionno = Convert.ToInt32(hdnSection.Value);
                            batchno = Convert.ToInt32(hdnBatch.Value);
                            schemeno = Convert.ToInt32(hdnScheme.Value);
                            slotno = Convert.ToInt32(lblSlot.ToolTip);
                            sessionno = Convert.ToInt32(lblDate.ToolTip);
                            College_id = Convert.ToInt32(ddlInstitute.SelectedValue);
                            uano_dept = ddlFaculty.SelectedValue.ToString().Split('-');
                            ua_no = uano_dept.Length > 0 && uano_dept != null ? Convert.ToInt32(uano_dept[0]) : 0;
                            deptno = uano_dept.Length > 1 && uano_dept != null ? Convert.ToInt32(uano_dept[1]) : 0;
                        }
                        else
                        {
                            //semesterno = 0;
                            //sectionno = 0;
                            //batchno = 0;
                            //schemeno = 0;
                            //sessionno = 0;
                            semesterno = Convert.ToInt32(hdnSemester.Value);
                            sectionno = Convert.ToInt32(hdnSection.Value);
                            batchno = Convert.ToInt32(hdnBatch.Value);
                            schemeno = Convert.ToInt32(hdnScheme.Value);
                            slotno = Convert.ToInt32(lblSlot.ToolTip);
                            sessionno = Convert.ToInt32(lblDate.ToolTip);
                            College_id = Convert.ToInt32(ddlInstitute.SelectedValue);
                            ua_no = Convert.ToInt32(ddlFaculty.SelectedValue);
                            deptno = Convert.ToInt32(ddlDept.SelectedValue);
                        }

                        DataSet ds = null;
                        ds = objAtt.GetTakenFacultyCourses(Convert.ToInt32(ua_no), Convert.ToDateTime(txtAttDate.Text), Convert.ToInt32(semesterno), Convert.ToInt32(sectionno), Convert.ToInt32(batchno), Convert.ToInt32(schemeno), Convert.ToInt32(ddlAttType.SelectedValue), Convert.ToInt32(sessionno), College_id, deptno);
                        int clashslotno = 0;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ddlAttType.SelectedValue != "2")
                            {
                                lvFacultyCourse.DataSource = ds;
                                lvFacultyCourse.DataBind();

                                DataSet dslvCourse = (DataSet)Session["dslvCourse"];


                                for (int i = 0; i < dslvCourse.Tables[0].Rows.Count; i++)
                                {
                                    int slotnolvCourse = Convert.ToInt32(dslvCourse.Tables[0].Rows[0]["SLOTNO"].ToString());

                                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                    {
                                        if (slotnolvCourse == Convert.ToInt32(ds.Tables[0].Rows[0]["MAIN_SLOTNO"].ToString()))
                                        {
                                            clashslotno = slotnolvCourse;
                                        }
                                    }
                                }

                                foreach (ListViewDataItem lvHead in lvFacultyCourse.Items)
                                {
                                    CheckBox chkFacCourse = lvHead.FindControl("chkFacCourse") as CheckBox;
                                    Label lblFacSlot = lvHead.FindControl("lblFacSlot") as Label;

                                    if (Convert.ToInt32(lblFacSlot.ToolTip) == clashslotno)
                                    {
                                        chkFacCourse.Enabled = false;
                                    }
                                    else
                                    {
                                        chkFacCourse.Enabled = true;
                                    }
                                }

                            }
                            else if (ddlAttType.SelectedValue == "2")
                            {
                                lvFacultyCourseMutual.DataSource = ds;
                                lvFacultyCourseMutual.DataBind();
                            }

                            if (ddlAttType.SelectedValue == "4")
                            {
                                lvFacultyCourse.Visible = true;
                                lvFacultyCourseMutual.Visible = false;
                            }
                            else if (ddlAttType.SelectedValue == "2")
                            {
                                lvFacultyCourseMutual.Visible = true;
                                lvFacultyCourse.Visible = false;
                            }

                            if (ddlAttType.SelectedValue == "4")
                            {
                                foreach (ListViewDataItem lvHead in lvFacultyCourse.Items)
                                {
                                    CheckBox chkFacCourse = lvHead.FindControl("chkFacCourse") as CheckBox;
                                    Label lblFacSlot = lvHead.FindControl("lblFacSlot") as Label;
                                    HiddenField hdnScheduleUano = lvHead.FindControl("hdnScheduleUano") as HiddenField;
                                    int taken_course = 0;
                                    int ScheduleUano = hdnScheduleUano.Value == string.Empty ? 0 : Convert.ToInt32(hdnScheduleUano.Value);

                                    if (ddlAttType.SelectedValue == "2")  // Mutual
                                    {
                                        taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND SLOTNO=" + slotno + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ua_no) + " AND SLOTNO=" + slotno + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                    }
                                    else if (ddlAttType.SelectedValue == "4")  // Swapping
                                    {
                                        taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ua_no) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                    }
                                    else
                                    {
                                        taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                    }

                                    int cancel = 0;

                                    Session["AttCan"] = 0;
                                    Session["taken_course"] = 0;

                                    if (taken_course > 0)
                                    {
                                        if (ddlAttType.SelectedValue == "2")
                                        {
                                            cancel = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + " AND SLOTNO=" + slotno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + ")") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + " AND SLOTNO=" + slotno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + ")"));
                                        }
                                        else
                                        {
                                            cancel = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + ")") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + ")"));
                                        }

                                        Session["AttCan"] = cancel;
                                        Session["taken_course"] = taken_course;

                                        if (cancel == 0)
                                        {
                                            chkFacCourse.Checked = true;
                                            chkFacCourse.Enabled = false;
                                        }
                                        else
                                        {
                                            chkFacCourse.Checked = false;
                                            chkFacCourse.Enabled = true;
                                        }
                                    }

                                    int AttDone = objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + Convert.ToInt32(sessionno) + " AND SCHEMENO=" + Convert.ToInt32(schemeno) + " AND SEMESTERNO=" + Convert.ToInt32(semesterno) + " AND SECTIONNO=" + Convert.ToInt32(sectionno) + " AND ISNULL(BATCHNO,0)=" + Convert.ToInt32(batchno) + " AND SLOTNO=" + Convert.ToInt32(lblFacSlot.ToolTip) + " AND COURSENO=" + Convert.ToInt32(chkFacCourse.ToolTip) + " AND UA_NO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0 AND CAST(ATT_DATE AS DATE)=CONVERT(DATE,'" + txtAttDate.Text + "',103)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + Convert.ToInt32(sessionno) + " AND SCHEMENO=" + Convert.ToInt32(schemeno) + " AND SEMESTERNO=" + Convert.ToInt32(semesterno) + " AND SECTIONNO=" + Convert.ToInt32(sectionno) + " AND ISNULL(BATCHNO,0)=" + Convert.ToInt32(batchno) + " AND SLOTNO=" + Convert.ToInt32(lblFacSlot.ToolTip) + " AND COURSENO=" + Convert.ToInt32(chkFacCourse.ToolTip) + " AND UA_NO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0 AND CAST(ATT_DATE AS DATE)=CONVERT(DATE,'" + txtAttDate.Text + "',103)"));

                                    if (AttDone != 0 && Convert.ToInt32(Session["AttCan"].ToString()) == 0 && Convert.ToInt32(Session["taken_course"].ToString()) != 0)
                                    {
                                        chkFacCourse.Checked = true;
                                        chkFacCourse.Enabled = false;
                                    }
                                    else if (Convert.ToInt32(Session["AttCan"].ToString()) == 0 && AttDone == 0 && Convert.ToInt32(Session["taken_course"].ToString()) != 0)
                                    {
                                        chkFacCourse.Checked = true;
                                        chkFacCourse.Enabled = false;
                                    }
                                    else if (Convert.ToInt32(Session["AttCan"].ToString()) == 0 && AttDone == 0 && Convert.ToInt32(Session["taken_course"].ToString()) == 0)
                                    {
                                        chkFacCourse.Checked = false;
                                        chkFacCourse.Enabled = true;
                                    }
                                    else
                                    {
                                        chkFacCourse.Checked = false;
                                        chkFacCourse.Enabled = true;
                                    }

                                }
                            }
                            else if (ddlAttType.SelectedValue == "2")
                            {
                                foreach (ListViewDataItem lvHead in lvFacultyCourseMutual.Items)
                                {
                                    CheckBox chkFacCourse = lvHead.FindControl("chkFacCourse") as CheckBox;
                                    Label lblFacSlot = lvHead.FindControl("lblFacSlot") as Label;
                                    HiddenField hdnScheduleUano = lvHead.FindControl("hdnScheduleUano") as HiddenField;
                                    HiddenField hdnSem = lvHead.FindControl("hdnSem") as HiddenField;
                                    HiddenField hdnBatchm = lvHead.FindControl("hdnBatch") as HiddenField;
                                    HiddenField hdnSectionm = lvHead.FindControl("hdnSection") as HiddenField;
                                    HiddenField hdnSchemem = lvHead.FindControl("hdnScheme") as HiddenField;
                                    HiddenField hdnSessionm = lvHead.FindControl("hdnSession") as HiddenField;
                                    Label lblTakenSlot = lvHead.FindControl("lblTakenSlot") as Label;

                                    int taken_course = 0;
                                    int ScheduleUano = hdnScheduleUano.Value == string.Empty ? 0 : Convert.ToInt32(hdnScheduleUano.Value);

                                    if (ddlAttType.SelectedValue == "2")
                                    {
                                        //taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND SLOTNO=" + Convert.ToInt32(lblTakenSlot.ToolTip) + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND SLOTNO=" + Convert.ToInt32(lblTakenSlot.ToolTip) + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                        taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                    }
                                    else if (ddlAttType.SelectedValue == "4")
                                    {
                                        taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ua_no) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_UANO=" + ScheduleUano + " AND TAKEN_COURSENO=" + chkFacCourse.ToolTip + " AND TAKEN_UANO=" + Convert.ToInt32(ua_no) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                    }
                                    else
                                    {
                                        taken_course = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", " MAX(AANO) AS AANO", "SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND SLOTNO=" + lblFacSlot.ToolTip + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                    }

                                    int cancel = 0;

                                    Session["AttCan"] = 0;
                                    Session["taken_course"] = 0;

                                    if (taken_course > 0)
                                    {
                                        if (ddlAttType.SelectedValue == "2")
                                        {
                                            cancel = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + ") AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "ISNULL(CANCEL,0)CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + ") AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                        }
                                        else
                                        {
                                            cancel = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND ISNULL(CANCEL,0)=0" + " AND SLOTNO=" + lblFacSlot.ToolTip + ") AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "ISNULL(CANCEL,0)CANCEL", "AANO=(SELECT MAX(AANO) AS AANO FROM ACD_ALTERNATE_ATTENDANCE WHERE SCHEDULE_COURSENO=" + chkFacCourse.ToolTip + " AND SCHEDULE_UANO=" + Convert.ToInt32(ua_no) + " AND ISNULL(CANCEL,0)=0" + " AND SLOTNO=" + lblFacSlot.ToolTip + ") AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                        }

                                        if (cancel == 0)
                                        {
                                            //chkFacCourse.Checked = true;
                                            // chkFacCourse.Enabled = false;
                                        }
                                        else
                                        {
                                            chkFacCourse.Checked = false;
                                            chkFacCourse.Enabled = true;
                                        }
                                    }

                                    Session["AttCan"] = cancel;
                                    Session["taken_course"] = taken_course;

                                    int ses = Convert.ToInt32(hdnSessionm.Value);

                                    //int AttDone = objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + Convert.ToInt32(hdnSessionm.Value) + " AND SCHEMENO=" + Convert.ToInt32(hdnSchemem.Value) + " AND SEMESTERNO=" + Convert.ToInt32(hdnSem.Value) + " AND SECTIONNO=" + Convert.ToInt32(hdnSectionm.Value) + " AND ISNULL(BATCHNO,0)=" + Convert.ToInt32(hdnBatchm.Value) + " AND SLOTNO=" + Convert.ToInt32(slotno) + " AND COURSENO=" + Convert.ToInt32(chkFacCourse.ToolTip) + " AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND CAST(ATT_DATE AS DATE)=CONVERT(DATE,'" + txtAttDate.Text + "',103)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + Convert.ToInt32(hdnSessionm.Value) + " AND SCHEMENO=" + Convert.ToInt32(hdnSchemem.Value) + " AND SEMESTERNO=" + Convert.ToInt32(hdnSem.Value) + " AND SECTIONNO=" + Convert.ToInt32(hdnSectionm.Value) + " AND ISNULL(BATCHNO,0)=" + Convert.ToInt32(hdnBatchm.Value) + " AND SLOTNO=" + Convert.ToInt32(slotno) + " AND COURSENO=" + Convert.ToInt32(chkFacCourse.ToolTip) + " AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND CAST(ATT_DATE AS DATE)=CONVERT(DATE,'" + txtAttDate.Text + "',103)"));

                                    //if (AttDone != 0 && Convert.ToInt32(Session["AttCan"].ToString()) == 0 && Convert.ToInt32(Session["taken_course"].ToString()) != 0)
                                    //{
                                    //    chkFacCourse.Checked = true;
                                    //    chkFacCourse.Enabled = false;
                                    //}
                                    //else if (Convert.ToInt32(Session["AttCan"].ToString()) == 0 && AttDone == 0 && Convert.ToInt32(Session["taken_course"].ToString()) != 0)
                                    //{
                                    //    chkFacCourse.Checked = true;
                                    //    chkFacCourse.Enabled = false;
                                    //}
                                    //else if (Convert.ToInt32(Session["AttCan"].ToString()) == 0 && AttDone == 0 && Convert.ToInt32(Session["taken_course"].ToString()) == 0)
                                    //{
                                    //    chkFacCourse.Checked = false;
                                    //    chkFacCourse.Enabled = true;
                                    //}
                                    //else
                                    //{
                                    //    chkFacCourse.Checked = false;
                                    //    chkFacCourse.Enabled = true;
                                    //}

                                }
                            }

                        }
                        else
                        {
                            //if (ddlAttType.SelectedValue != "2")
                            //{
                            objCommon.DisplayMessage(updatt, "No Slot Found of the Selected Faculty for Particular Date!", this);
                            //}

                            lvFacultyCourse.DataSource = null;
                            lvFacultyCourse.DataBind();
                            lvFacultyCourse.Visible = false;

                            lvFacultyCourseMutual.DataSource = null;
                            lvFacultyCourseMutual.DataBind();
                            lvFacultyCourseMutual.Visible = false;

                        }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    // Fills faculty in dropdownlist based on department selection.
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvHead in lvCourse.Items)
        {
            DropDownList ddlFaculty = lvHead.FindControl("ddlFaculty") as DropDownList;
            CheckBox chkCourse = lvHead.FindControl("chkCourse") as CheckBox;

            if (Session["userno"] != null)
            {
                if (chkCourse.Enabled && chkCourse.Checked)
                {
                    ddlFaculty.SelectedIndex = 0;
                    ddlFaculty.Enabled = true;
                    if (ddlAttType.SelectedValue == "2")
                    {
                        // objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON (D.DEPTNO=U.UA_DEPTNO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME", "UA_DEPTNO=" + ddlDept.SelectedValue + " AND CT.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID="+ddlInstitute.SelectedValue+") AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0", "U.UA_NO");
                        objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON (CAST(D.DEPTNO AS VARCHAR)  IN (U.UA_DEPTNO)INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "(U.UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + U.UA_FULLNAME) AS UA_FULLNAME", "CT.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + ") AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0", "U.UA_NO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON (CAST(D.DEPTNO AS VARCHAR)  IN (U.UA_DEPTNO)) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "(U.UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + U.UA_FULLNAME) AS UA_FULLNAME", "CAST(" + ddlDept.SelectedValue + " AS VARCHAR) IN (UA_DEPTNO) AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0", "U.UA_NO");
                        //objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON (D.DEPTNO=U.UA_DEPTNO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME +' ('+D.DEPTNAME +')'", "U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0", "U.UA_NO");
                    }
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }
    }

    // Sets listview & dropdownlist as initial
    protected void txtAttDate_TextChanged(object sender, EventArgs e)
    {
        ddlAttType.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlInstitute.SelectedIndex = 0;
        lvCourse.Visible = false;
        lvFacultyCourse.Visible = false;
    }

    // Calls BindCancelAttDetails method to fill listview
    protected void btnShowAtt_Click(object sender, EventArgs e)
    {
        if (Session["userno"] != null)
        {
            BindCancelAttDetails();
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
    }

    // Binds data to Cancel alternate attendance listview (lvCanAtt)
    private void BindCancelAttDetails()
    {
        DataSet ds = null;
        int College_id = Convert.ToInt32(ddlInstitute1.SelectedValue);
        int Degreeno = Convert.ToInt32(ddlDegree1.SelectedValue);
        ds = objAtt.GetAllAlternateAttendance(Convert.ToDateTime(txtAttDateCan.Text), Convert.ToInt32(Session["userno"].ToString()), College_id, Degreeno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvCanAtt.DataSource = ds;
            lvCanAtt.DataBind();
            lvCanAtt.Visible = true;
            btnCanAtt.Enabled = true;
            btnAttCanReport.Enabled = true;

            foreach (ListViewDataItem lvHead in lvCanAtt.Items)
            {
                CheckBox chkCanAtt = lvHead.FindControl("chkCanCourse") as CheckBox;
                Label lblCanScheduleFac = lvHead.FindControl("lblCanScheduleFac") as Label;
                Label lblCanTakenFac = lvHead.FindControl("lblCanTakenFac") as Label;
                Label lblCanAttType = lvHead.FindControl("lblCanAttType") as Label;

                int cancelleduano = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "1", "ISNULL(CANCEL,0)=" + 1 + " AND AANO=" + chkCanAtt.ToolTip + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COLLEGE_ID=" + ddlInstitute1.SelectedValue + " AND DEGREENO=" + ddlDegree1.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "1", "ISNULL(CANCEL,0)=" + 1 + " AND AANO=" + chkCanAtt.ToolTip + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COLLEGE_ID=" + ddlInstitute1.SelectedValue + " AND DEGREENO=" + ddlDegree1.SelectedValue));

                if (cancelleduano == 1)
                {
                    chkCanAtt.Checked = true;
                    chkCanAtt.Enabled = false;
                }
                else
                {
                    chkCanAtt.Checked = false;
                    chkCanAtt.Enabled = true;
                }
            }

        }
        else
        {
            objCommon.DisplayMessage(updatt, "No record is available for specified date !!", this);
            lvCanAtt.DataSource = null;
            lvCanAtt.DataBind();
            lvCanAtt.Visible = false;
            btnCanAtt.Enabled = false;
            btnAttCanReport.Enabled = false;
        }
    }

    // Cancels alternate attendance button click event.
    protected void btnCanAtt_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem lvHead in lvCanAtt.Items)
        {
            CheckBox chkCanAtt = lvHead.FindControl("chkCanCourse") as CheckBox;

            int aano = Convert.ToInt32(chkCanAtt.ToolTip);
            int uano = 0;
            DateTime candate = DateTime.Now;

            if (Session["userno"] != null)
            {
                uano = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }

            if (chkCanAtt.Enabled)
            {
                if (chkCanAtt.Checked)
                {
                    string hostName = Dns.GetHostName();
                    string ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    count++;
                    CustomStatus cs = 0;

                    DataSet ds = objCommon.FillDropDown("ACD_ALTERNATE_ATTENDANCE", "AANO, SESSIONNO, SCHEMENO, SEMESTERNO, SECTIONNO, ISNULL(BATCHNO,0)BATCHNO", "SLOTNO, TAKEN_COURSENO, TAKEN_UANO, SCHEDULE_UANO, TRANSFER_TYPE, ATTENDANCE_DATE", "ISNULL(CANCEL,0)=0 AND AANO=" + Convert.ToInt32(chkCanAtt.ToolTip) + " AND COLLEGE_ID=" + ddlInstitute1.SelectedValue + " AND DEGREENO=" + ddlDegree1.SelectedValue + " AND ISNULL(OrganizationId,0)=" + Session["OrgId"], "");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int sessionno = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"]);
                            int schemeno = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]);
                            int semesterno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"]);
                            int sectionno = Convert.ToInt32(ds.Tables[0].Rows[0]["SECTIONNO"]);
                            int batchno = Convert.ToInt32(ds.Tables[0].Rows[0]["BATCHNO"]);
                            int slotno = Convert.ToInt32(ds.Tables[0].Rows[0]["SLOTNO"]);
                            int courseno = Convert.ToInt32(ds.Tables[0].Rows[0]["TAKEN_COURSENO"]);
                            int transType = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSFER_TYPE"]);
                            int ua_no = 0;

                            if (transType == 2 || transType == 4)
                            {
                                ua_no = Convert.ToInt32(ds.Tables[0].Rows[0]["TAKEN_UANO"]);
                            }
                            else if (transType == 3)
                            {
                                ua_no = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEDULE_UANO"]);
                            }

                            DateTime AttDate1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["ATTENDANCE_DATE"]);
                            string AttDate = AttDate1.Date.ToString("dd/MM/yyyy");
                            int College_id = Convert.ToInt32(ddlInstitute1.SelectedValue);
                            int Degreeno = Convert.ToInt32(ddlDegree1.SelectedValue);
                            int AttDone = objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ATT_NO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno + " AND SECTIONNO=" + sectionno + " AND ISNULL(BATCHNO,0)=" + batchno + " AND SLOTNO=" + slotno + " AND COURSENO=" + courseno + " AND UA_NO=" + ua_no + " AND ISNULL(CANCEL,0)=0 AND CONVERT(DATE,ATT_DATE,103)=CONVERT(DATE,'" + AttDate + "',103) AND COLLEGE_ID=" + ddlInstitute1.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ATT_NO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND SEMESTERNO=" + semesterno + " AND SECTIONNO=" + sectionno + " AND ISNULL(BATCHNO,0)=" + batchno + " AND SLOTNO=" + slotno + " AND COURSENO=" + courseno + " AND UA_NO=" + ua_no + " AND ISNULL(CANCEL,0)=0 AND CONVERT(DATE,ATT_DATE,103)=CONVERT(DATE,'" + AttDate + "',103) AND COLLEGE_ID=" + ddlInstitute1.SelectedValue));

                            if (AttDone == 0)
                            {
                                cs = (CustomStatus)objAtt.AlternateAttCancel(aano, uano, ipaddress, candate, sessionno, schemeno, semesterno, sectionno, batchno, slotno, College_id, Degreeno);
                            }
                            else
                            {
                                objCommon.DisplayMessage(updatt, "Entry cant be cancelled, as faculty had already taken attendance for this subject !!", this.Page);
                                BindCancelAttDetails();
                            }
                        }
                    }

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updatt, "Attendance Updated Successfully !!", this.Page);
                        BindCancelAttDetails();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatt, "Attendance Updation Failed !!", this.Page);
                        BindCancelAttDetails();
                    }
                }
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(updatt, "Please Select Subject For Cancellation From Alternate Attendance List !!", this.Page);
        }
    }

    // Clears the fields in Cancel attendance tab.
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        txtAttDateCan.Text = string.Empty;
        lvCanAtt.Visible = false;
        btnAttCanReport.Enabled = false;
        btnCanAtt.Enabled = false;
        ddlInstitute1.SelectedIndex = -1;
        ddlDegree1.SelectedIndex = -1;
    }

    // Attendance cancel report button click event.
    protected void btnAttCanReport_Click(object sender, EventArgs e)
    {
        ShowReport("Alternate Attendance Cancellation", "rptAlternateAttendanceCancel.rpt", 1);
    }

    // Called for binding data to report.
    private void ShowReport(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())) == string.Empty ? "0" : (objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
            deptno = deptno.Replace(",", ".");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (reportno == 1)
            {
                DateTime attdate = Convert.ToDateTime(txtAttDateCan.Text);
                string attdate1 = attdate.ToString("yyyy-MM-dd");
                url += "&param=@P_ATTDATE=" + attdate1 + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString());
            }
            else if (reportno == 2)
            {
                DateTime attdate = Convert.ToDateTime(txtAttDate.Text);
                string attdate1 = attdate.ToString("yyyy-MM-dd");
                url += "&param=@P_ATTDATE=" + attdate1 + ",@P_DEPTNO=" + deptno + ",@P_USERTYPE=" + Convert.ToInt32(Session["usertype"].ToString()) + ",@P_ATTDATESUB=" + attdate1 + ",@P_DEPTNOSUB=" + deptno + ",@P_USERTYPESUB=" + Convert.ToInt32(Session["usertype"].ToString()) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }

    // Performs the action based on check changed.
    protected void chkCourse_CheckedChanged(object sender, EventArgs e)
    {
        lvFacultyCourse.Visible = false;
        lvFacultyCourseMutual.Visible = false;

        foreach (ListViewDataItem lvHead in lvCourse.Items)
        {
            DropDownList ddlFaculty = lvHead.FindControl("ddlFaculty") as DropDownList;
            CheckBox chkCourse = lvHead.FindControl("chkCourse") as CheckBox;
            Label lblDate = lvHead.FindControl("lblDate") as Label;
            HiddenField hdnSemester = lvHead.FindControl("hdnSemester") as HiddenField;
            HiddenField hdnSection = lvHead.FindControl("hdnSection") as HiddenField;
            HiddenField hdnBatch = lvHead.FindControl("hdnBatch") as HiddenField;
            HiddenField hdnScheme = lvHead.FindControl("hdnScheme") as HiddenField;
            HiddenField hdnDayno = lvHead.FindControl("hdnDayno") as HiddenField;

            int semester = Convert.ToInt32(hdnSemester.Value);
            int section = Convert.ToInt32(hdnSection.Value);
            int scheme = Convert.ToInt32(hdnScheme.Value);
            int batch = Convert.ToInt32(hdnBatch.Value);
            int dayno = Convert.ToInt32(hdnDayno.Value);
            int sessionno = Convert.ToInt32(lblDate.ToolTip);
            int _subID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(chkCourse.ToolTip)));

            if (Session["userno"] != null)
            {
                if (chkCourse.Enabled)
                {
                    if (!ddlFaculty.Enabled)
                    {
                        DataSet ds = null;
                        if (ddlAttType.SelectedValue == "2")
                        {
                            if (_subID == 2)
                            {
                                //objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME", "CT.SESSIONNO=" + sessionno + " AND CT.SECTIONNO=" + section + " AND CT.SCHEMENO=" + scheme + " AND CT.SEMESTERNO=" + semester + " AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.BATCHNO,0) IN(SELECT DISTINCT BATCHNO FROM ACD_COURSE_TEACHER CT INNER JOIN ACD_TIME_TABLE_CONFIG TTC ON (TTC.CTNO=CT.CT_NO) WHERE CT.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0  AND ISNULL(TTC.CANCEL,0)=0 AND SESSIONNO=" + sessionno + " AND CT.COURSENO=" + Convert.ToInt32(chkCourse.ToolTip) + " AND TTC.TIME_TABLE_DATE = CONVERT(VARCHAR(10), CONVERT(date, '" + txtAttDate.Text + "', 105), 23)) AND CT.COLLEGE_ID="+ddlInstitute.SelectedValue, "U.UA_FULLNAME");
                                // objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "CASE WHEN ISNULL(UA_DEPTNO,0)<>0 THEN U.UA_FULLNAME+' ('+DBO.FN_DESC('DEPARTMENT',UA_DEPTNO) +')' ELSE U.UA_FULLNAME END AS UA_FULLNAME", "CT.SESSIONNO=" + sessionno + " AND CT.SECTIONNO=" + section + " AND CT.SCHEMENO=" + scheme + " AND CT.SEMESTERNO=" + semester + " AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.BATCHNO,0) IN(SELECT DISTINCT BATCHNO FROM ACD_COURSE_TEACHER CT INNER JOIN ACD_TIME_TABLE_CONFIG TTC ON (TTC.CTNO=CT.CT_NO) WHERE CT.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0  AND ISNULL(TTC.CANCEL,0)=0 AND SESSIONNO=" + sessionno + " AND CT.COURSENO=" + Convert.ToInt32(chkCourse.ToolTip) + " AND TTC.TIME_TABLE_DATE = CONVERT(VARCHAR(10), CONVERT(date, '" + txtAttDate.Text + "', 105), 23)) ", "U.UA_NO");
                                ds = objAtt.Get_Faculty_For_Alternate_Attendance(sessionno, section, scheme, semester, _subID, Convert.ToInt32(chkCourse.ToolTip), Convert.ToInt32(ddlAttType.SelectedValue), 0, Convert.ToInt32(Session["userno"]), (txtAttDate.Text));
                            }
                            else
                            {
                                //objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME+' ('+dbo.FN_DESC('DEPARTMENT',ua_deptno) +')'", "CT.SESSIONNO=" + sessionno + " AND CT.SECTIONNO=" + section + " AND CT.SCHEMENO=" + scheme + " AND CT.SEMESTERNO=" + semester + " AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND CT.COLLEGE_ID=" + ddlInstitute.SelectedValue, "U.UA_FULLNAME");
                                // objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "CASE WHEN ISNULL(UA_DEPTNO,0)<>0 THEN U.UA_FULLNAME+' ('+DBO.FN_DESC('DEPARTMENT',UA_DEPTNO) +')' ELSE U.UA_FULLNAME END AS UA_FULLNAME", "CT.SESSIONNO=" + sessionno + " AND CT.SECTIONNO=" + section + " AND CT.SCHEMENO=" + scheme + " AND CT.SEMESTERNO=" + semester + " AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + "", "U.UA_NO");
                                ds = objAtt.Get_Faculty_For_Alternate_Attendance(sessionno, section, scheme, semester, _subID, Convert.ToInt32(chkCourse.ToolTip), Convert.ToInt32(ddlAttType.SelectedValue), 0, Convert.ToInt32(Session["userno"]), (txtAttDate.Text));
                            }
                            //* objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME", "CT.SESSIONNO=" + sessionno + " AND CT.SECTIONNO=" + section + " AND CT.SCHEMENO=" + scheme + " AND CT.SEMESTERNO=" + semester + " AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.BATCHNO,0) IN(ISNULL((SELECT DISTINCT BATCHNO FROM ACD_COURSE_TEACHER WHERE SESSIONNO=" + sessionno + " AND CT_NO IN(SELECT DISTINCT CT.CT_NO FROM ACD_COURSE_TEACHER CT INNER JOIN ACD_TIME_TABLE_CONFIG TTC ON (TTC.CTNO=CT.CT_NO) WHERE CT.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CT.CANCEL,0)=0  AND ISNULL(TTC.CANCEL,0)=0 AND SESSIONNO="+sessionno+" AND CT.COURSENO=" + Convert.ToInt32(chkCourse.ToolTip) + " AND TTC.TIME_TABLE_DATE = CONVERT(VARCHAR(10), CONVERT(date, '" + txtAttDate.Text + "', 105), 23))),0))", "U.UA_FULLNAME");
                        }
                        else
                        {
                            //objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON (D.DEPTNO=U.UA_DEPTNO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "CASE WHEN ISNULL(UA_DEPTNO,0)<>0 THEN U.UA_FULLNAME+' ('+DBO.FN_DESC('DEPARTMENT',UA_DEPTNO) +')' ELSE U.UA_FULLNAME END AS UA_FULLNAME", "UA_DEPTNO=" + ddlDept.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + " AND CT.COLLEGE_ID=" + ddlInstitute.SelectedValue, "U.UA_NO");
                            //objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON (CAST(D.DEPTNO AS VARCHAR)  IN (U.UA_DEPTNO)) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME", "CAST(" + ddlDept.SelectedValue + " AS VARCHAR) IN (UA_DEPTNO) AND ISNULL(CT.CANCEL,0)=0 AND U.UA_NO!=" + Convert.ToInt32(Session["userno"].ToString()) + "", "U.UA_NO");
                            ds = objAtt.Get_Faculty_For_Alternate_Attendance(sessionno, section, scheme, semester, _subID, Convert.ToInt32(chkCourse.ToolTip), Convert.ToInt32(ddlAttType.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(Session["userno"]), (txtAttDate.Text));
                        }

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlFaculty.Items.Clear();
                            ddlFaculty.Items.Add("Please Select");
                            ddlFaculty.SelectedItem.Value = "0";
                            ddlFaculty.DataSource = ds;
                            ddlFaculty.DataValueField = ds.Tables[0].Columns[0].ToString();
                            ddlFaculty.DataTextField = ds.Tables[0].Columns[1].ToString();
                            ddlFaculty.DataBind();
                        }

                    }
                }

                if (chkCourse.Enabled && !chkCourse.Checked)
                {
                    ddlFaculty.SelectedIndex = 0;
                }

            }
            else
            {
                Response.Redirect("~/default.aspx");
            }

            if (chkCourse.Checked && chkCourse.Enabled)
            {
                ddlFaculty.Enabled = true;
            }
            else
            {
                ddlFaculty.Enabled = false;
            }

        }
    }

    // Attendance details report button click.
    protected void btnDetailReport_Click(object sender, EventArgs e)
    {
        if (txtAttDate.Text != "")
        {
            //if (ddlInstitute.SelectedValue !="")
            //{
            ShowReport("Alternate Attendance Details Report", "rptAlternateAttendanceDetails.rpt", 2);
            //}
            //else
            //{ 
            //objCommon.DisplayMessage(updatt, "Please Select School/Instittue !!", this.Page);
            //}
        }
        else
        {
            objCommon.DisplayMessage(updatt, "Please enter Attendance Date !!", this.Page);
        }
    }

    // Called by Submit button click event for submitting the entry.
    private void AttendanceTransfer()
    {
        try
        {
            if (ddlAttType.SelectedValue == "2" || ddlAttType.SelectedValue == "3")  // 2=Mutual, 3=Engage, 4=Swapping
            {
                int subjectcheck = 0;
                int facsubjectcheck = 0;
                int takenfaculty = 0;

                if (ddlAttType.SelectedValue == "2" || ddlAttType.SelectedValue == "4")
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox chkCourse = dataitem.FindControl("chkCourse") as CheckBox;
                        DropDownList ddlFaculty = dataitem.FindControl("ddlFaculty") as DropDownList;

                        if (chkCourse.Enabled)
                        {
                            if (chkCourse.Checked && chkCourse.Enabled)
                            {
                                subjectcheck++;
                                if (ddlFaculty.SelectedValue == "0")
                                {
                                    takenfaculty = 1;
                                }
                            }
                        }
                    }

                    if (ddlAttType.SelectedValue == "2")
                    {
                        foreach (ListViewDataItem dataitem1 in lvFacultyCourseMutual.Items)
                        {
                            CheckBox chkFacCourse = dataitem1.FindControl("chkFacCourse") as CheckBox;

                            if (chkFacCourse.Enabled)
                            {
                                if (chkFacCourse.Checked && chkFacCourse.Enabled)
                                {
                                    facsubjectcheck++;
                                }
                            }
                        }
                    }
                    else if (ddlAttType.SelectedValue == "4")
                    {
                        foreach (ListViewDataItem dataitem1 in lvFacultyCourse.Items)
                        {
                            CheckBox chkFacCourse = dataitem1.FindControl("chkFacCourse") as CheckBox;
                            if (chkFacCourse.Enabled)
                            {
                                if (chkFacCourse.Checked && chkFacCourse.Enabled)
                                {
                                    facsubjectcheck++;
                                }
                            }
                        }
                    }

                    if (subjectcheck != facsubjectcheck)
                    {
                        if (takenfaculty == 1)
                        {
                            objCommon.DisplayMessage(updatt, "Please Select Taken Faculty !", this.Page);
                            return;
                        }
                        else if (subjectcheck > 0 && facsubjectcheck == 0)
                        {
                            objCommon.DisplayMessage(updatt, "Please Select Subject of selected Faculty !", this.Page);
                            return;
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatt, "Please Select Atleast One Subject from Each List !", this.Page);
                            return;
                        }
                    }

                    if (subjectcheck == 0 || facsubjectcheck == 0)
                    {
                        objCommon.DisplayMessage(updatt, "Please Select Atleast One Subject from Each List !", this.Page);
                        return;
                    }
                }

                string deptno = "0";
                int lecturetype = 0;
                int schedulecourse = 0;
                int scheduleuano = 0;
                int takencourse = 0;
                int takenuano = 0;
                int Degreeno = 0;//Added by Dileep Kare on 15.04.2021
                int College_id = 0;
                int cancel = 0;
                string hostName = string.Empty;
                string ipaddress = string.Empty;
                int uano = 0;
                int transfertype = 0;

                int ttno = 0;
                int alternatenomax = 0;
                int alternateno = 0;

                int semesterno = 0;
                int sectionno = 0;
                int batchno = 0;
                int schemeno = 0;
                int swapno = 0;
                int translot = 0;
                int slotno = 0;
                int sessionno = 0;
                string[] uano_dept = null;


                DateTime attdate = DateTime.MinValue;
                DateTime transdate = DateTime.MinValue;

                if (ddlAttType.SelectedValue == "3") // Engage
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox chkCourse = dataitem.FindControl("chkCourse") as CheckBox;
                        Label lblSlot = dataitem.FindControl("lblSlot") as Label;
                        Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
                        Label lblDate = dataitem.FindControl("lblDate") as Label;
                        HiddenField hdnSem1 = dataitem.FindControl("hdnSem1") as HiddenField;
                        HiddenField hdnSection = dataitem.FindControl("hdnSection") as HiddenField;
                        HiddenField hdnBatch = dataitem.FindControl("hdnBatch") as HiddenField;
                        HiddenField hdnScheme = dataitem.FindControl("hdnScheme") as HiddenField;
                        DropDownList ddlFaculty = dataitem.FindControl("ddlFaculty") as DropDownList;
                        HiddenField hdnTTNO = dataitem.FindControl("hdnTTNO") as HiddenField;

                        if (chkCourse.Enabled)
                        {
                            if (chkCourse.Checked && chkCourse.Enabled)
                            {
                                if (ddlFaculty.SelectedValue == "0")
                                {
                                    objCommon.DisplayMessage(updatt, "Please Select Taken Faculty!", this.Page);
                                    return;
                                }

                                i++;
                                sessionno = Convert.ToInt32(lblDate.ToolTip);
                                attdate = Convert.ToDateTime(txtAttDate.Text);

                                if (Session["userno"] == null)
                                {
                                    Response.Redirect(Request.Url.ToString());
                                }

                                deptno = (objCommon.LookUp("USER_ACC", "ISNULL(UA_DEPTNO,0)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                                lecturetype = Convert.ToInt32(ddlAttType.SelectedValue);
                                slotno = Convert.ToInt32(lblSlot.ToolTip);
                                schedulecourse = Convert.ToInt32(chkCourse.ToolTip);
                                scheduleuano = Convert.ToInt32(Session["userno"].ToString());
                                takencourse = Convert.ToInt32(chkCourse.ToolTip);

                                uano_dept = ddlFaculty.SelectedValue.ToString().Split('-');
                                takenuano = uano_dept.Length > 0 && uano_dept != null ? Convert.ToInt32(uano_dept[0]) : 0;//Convert.ToInt32(ddlFaculty.SelectedValue);

                                cancel = 0;
                                transdate = DateTime.Now;
                                hostName = Dns.GetHostName();
                                ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                                uano = Convert.ToInt32(Session["userno"].ToString());
                                transfertype = Convert.ToInt32(ddlAttType.SelectedValue);

                                semesterno = Convert.ToInt32(hdnSem1.Value);
                                sectionno = Convert.ToInt32(hdnSection.Value);
                                batchno = Convert.ToInt32(hdnBatch.Value);
                                schemeno = Convert.ToInt32(hdnScheme.Value);
                                swapno = 0;
                                translot = Convert.ToInt32(lblSlot.ToolTip);
                                ttno = hdnTTNO.Value == string.Empty ? 0 : Convert.ToInt32(hdnTTNO.Value);
                                College_id = Convert.ToInt32(ddlInstitute.SelectedValue);
                                Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                                alternatenomax = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(ISNULL(ALTERNATE_NO,0))", "SESSIONNO=" + sessionno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(ISNULL(ALTERNATE_NO,0))", "SESSIONNO=" + sessionno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                alternateno = alternatenomax + 1;
                                int OrgId = Convert.ToInt32(Session["OrgId"]);

                                Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);

                                CustomStatus cs = (CustomStatus)objAtt.InsertAlternateAttendance(sessionno, attdate, deptno, lecturetype, slotno, schedulecourse, scheduleuano,
                                                                                                  takencourse, takenuano, cancel, transdate, ipaddress, uano, transfertype,
                                                                                                   semesterno, sectionno, batchno, swapno, translot, schemeno, ttno, alternateno, College_id, Degreeno, OrgId);

                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayMessage(updatt, ddlAttType.SelectedItem.Text + " Subject Transfer Successful !!", this.Page);
                                    done = 0;
                                    BindListViewCourseSlot();

                                    if (ddlAttType.SelectedValue == "4")
                                    {
                                        BindListViewSelectedFaculty();
                                    }
                                }

                            }
                        }
                    }
                }
                else if (ddlAttType.SelectedValue == "2")  // Mutual
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox chkCourse = dataitem.FindControl("chkCourse") as CheckBox;
                        Label lblSlot = dataitem.FindControl("lblSlot") as Label;
                        Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
                        Label lblDate = dataitem.FindControl("lblDate") as Label;
                        DropDownList ddlFaculty = dataitem.FindControl("ddlFaculty") as DropDownList;
                        HiddenField hdnTTNO = dataitem.FindControl("hdnTTNO") as HiddenField;

                        if (chkCourse.Enabled)
                        {
                            if (chkCourse.Checked && chkCourse.Enabled)
                            {

                                if (ddlFaculty.SelectedValue == "0")
                                {
                                    objCommon.DisplayMessage(updatt, "Please Select Taken Faculty!", this.Page);
                                    return;
                                }

                                i++;
                                sessionno = Convert.ToInt32(lblDate.ToolTip);
                                attdate = Convert.ToDateTime(txtAttDate.Text);

                                if (Session["userno"] == null)
                                {
                                    Response.Redirect(Request.Url.ToString());
                                }

                                deptno = (objCommon.LookUp("USER_ACC", "ISNULL(UA_DEPTNO,0)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                                lecturetype = Convert.ToInt32(ddlAttType.SelectedValue);
                                schedulecourse = Convert.ToInt32(chkCourse.ToolTip);
                                scheduleuano = Convert.ToInt32(Session["userno"].ToString());

                                uano_dept = ddlFaculty.SelectedValue.ToString().Split('-');
                                takenuano = uano_dept.Length > 0 && uano_dept != null ? Convert.ToInt32(uano_dept[0]) : 0;

                                cancel = 0;
                                transdate = DateTime.Now;
                                hostName = Dns.GetHostName();
                                ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                                uano = Convert.ToInt32(Session["userno"].ToString());
                                transfertype = Convert.ToInt32(ddlAttType.SelectedValue);

                                swapno = 0;
                                translot = Convert.ToInt32(lblSlot.ToolTip);
                                slotno = Convert.ToInt32(lblSlot.ToolTip);

                                ttno = hdnTTNO.Value == string.Empty ? 0 : Convert.ToInt32(hdnTTNO.Value);
                                alternatenomax = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(ISNULL(ALTERNATE_NO,0))", "SESSIONNO=" + sessionno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(ISNULL(ALTERNATE_NO,0))", "SESSIONNO=" + sessionno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                                alternateno = alternatenomax + 1;
                            }
                        }
                    }

                    foreach (ListViewDataItem dataitem1 in lvFacultyCourseMutual.Items)
                    {
                        CheckBox chkFacCourse = dataitem1.FindControl("chkFacCourse") as CheckBox;
                        Label lblFacSlot = dataitem1.FindControl("lblFacSlot") as Label;
                        HiddenField hdnSem = dataitem1.FindControl("hdnSem") as HiddenField;
                        HiddenField hdnSection = dataitem1.FindControl("hdnSection") as HiddenField;
                        HiddenField hdnBatch = dataitem1.FindControl("hdnBatch") as HiddenField;
                        HiddenField hdnScheme = dataitem1.FindControl("hdnScheme") as HiddenField;
                        Label lblFacCourseName = dataitem1.FindControl("lblFacCourseName") as Label;

                        if (chkFacCourse.Enabled)
                        {
                            if (chkFacCourse.Checked && chkFacCourse.Enabled)
                            {
                                j++;
                                takencourse = Convert.ToInt32(chkFacCourse.ToolTip);
                                //slotno = Convert.ToInt32(lblFacSlot.ToolTip);
                                semesterno = Convert.ToInt32(hdnSem.Value);
                                sectionno = Convert.ToInt32(hdnSection.Value);
                                batchno = Convert.ToInt32(hdnBatch.Value);
                                schemeno = Convert.ToInt32(hdnScheme.Value);
                                Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                                College_id = Convert.ToInt32(ddlInstitute.SelectedValue);
                            }
                        }
                    }

                    int OrgId = Convert.ToInt32(Session["OrgId"]);

                    CustomStatus cs = (CustomStatus)objAtt.InsertAlternateAttendance(sessionno, attdate, deptno, lecturetype, slotno, schedulecourse, scheduleuano,
                                                                                      takencourse, takenuano, cancel, transdate, ipaddress, uano, transfertype,
                                                                                       semesterno, sectionno, batchno, swapno, translot, schemeno, ttno, alternateno, College_id, Degreeno, OrgId);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updatt, ddlAttType.SelectedItem.Text + " Subject Transfer Successful !!", this.Page);
                        done = 0;

                        if (ddlAttType.SelectedValue == "4" || ddlAttType.SelectedValue == "2")
                        {
                            BindListViewSelectedFaculty();
                            lvFacultyCourseMutual.DataSource = null;
                            lvFacultyCourseMutual.DataBind();
                            lvFacultyCourseMutual.Visible = false;
                        }

                        BindListViewCourseSlot();
                    }

                }
            }

            else if (ddlAttType.SelectedValue == "4") // Swapping
            {
                int subjectcheck = 0;
                int facsubjectcheck = 0;
                int ttno = 0;
                int alternatenomax = 0;
                int alternateno = 0;

                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    CheckBox chkCourse = dataitem.FindControl("chkCourse") as CheckBox;
                    if (chkCourse.Enabled)
                    {
                        if (chkCourse.Checked && chkCourse.Enabled)
                        {
                            subjectcheck++;
                        }
                    }
                }

                foreach (ListViewDataItem dataitem1 in lvFacultyCourse.Items)
                {
                    CheckBox chkFacCourse = dataitem1.FindControl("chkFacCourse") as CheckBox;
                    if (chkFacCourse.Enabled)
                    {
                        if (chkFacCourse.Checked && chkFacCourse.Enabled)
                        {
                            facsubjectcheck++;
                        }
                    }
                }

                if (subjectcheck > 1 || facsubjectcheck > 1)
                {
                    objCommon.DisplayMessage(updatt, "Please Select Only One Subject from Each List !!", this.Page);
                    return;
                }


                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    CheckBox chkCourse = dataitem.FindControl("chkCourse") as CheckBox;
                    Label lblSlot = dataitem.FindControl("lblSlot") as Label;
                    Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
                    Label lblDate = dataitem.FindControl("lblDate") as Label;
                    HiddenField hdnSem1 = dataitem.FindControl("hdnSem1") as HiddenField;
                    HiddenField hdnSection = dataitem.FindControl("hdnSection") as HiddenField;
                    HiddenField hdnBatch = dataitem.FindControl("hdnBatch") as HiddenField;
                    HiddenField hdnScheme = dataitem.FindControl("hdnScheme") as HiddenField;
                    int slotno = 0;
                    int translot = 0;
                    DropDownList ddlFaculty = dataitem.FindControl("ddlFaculty") as DropDownList;
                    HiddenField hdnTTNO = dataitem.FindControl("hdnTTNO") as HiddenField;

                    if (chkCourse.Enabled)
                    {
                        if (chkCourse.Checked && chkCourse.Enabled)
                        {
                            i++;
                            int sessionno = Convert.ToInt32(lblDate.ToolTip);
                            DateTime attdate = Convert.ToDateTime(txtAttDate.Text);
                            string deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())) == string.Empty ? "0" : (objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                            int lecturetype = Convert.ToInt32(ddlAttType.SelectedValue);
                            int takenuano = 0;
                            int swapnomax = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(SWAPNO)", "TRANSFER_TYPE=4 AND ISNULL(CANCEL,0)=0" + "AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(SWAPNO)", "TRANSFER_TYPE=4 AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                            int swapno = swapnomax + 1;
                            string[] uano_dept = null;
                            uano_dept = ddlFaculty.SelectedValue.Split('-');
                            //slotnoSubList = Convert.ToInt32(lblSlot.ToolTip());

                            foreach (ListViewDataItem dataitem1 in lvFacultyCourse.Items)
                            {
                                CheckBox chkFacCourse = dataitem1.FindControl("chkFacCourse") as CheckBox;
                                Label lblFacSlot = dataitem1.FindControl("lblFacSlot") as Label;

                                if (chkFacCourse.Enabled)
                                {
                                    if (chkFacCourse.Checked && chkFacCourse.Enabled)
                                    {
                                        j++;
                                        translot = Convert.ToInt32(lblFacSlot.ToolTip);
                                    }
                                }
                            }

                            int schedulecourse = Convert.ToInt32(chkCourse.ToolTip);
                            int scheduleuano = Convert.ToInt32(Session["userno"].ToString());
                            int takencourse = Convert.ToInt32(chkCourse.ToolTip);

                            uano_dept = ddlFaculty.SelectedValue.ToString().Split('-');
                            takenuano = uano_dept.Length > 0 && uano_dept != null ? Convert.ToInt32(uano_dept[0]) : 0;

                            int cancel = 0;
                            DateTime transdate = DateTime.Now;
                            string hostName = Dns.GetHostName();
                            string ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                            int uano = Convert.ToInt32(Session["userno"].ToString());
                            int transfertype = Convert.ToInt32(ddlAttType.SelectedValue);

                            int semesterno = Convert.ToInt32(hdnSem1.Value);
                            int sectionno = Convert.ToInt32(hdnSection.Value);
                            int batchno = Convert.ToInt32(hdnBatch.Value);
                            int schemeno = Convert.ToInt32(hdnScheme.Value);
                            slotno = Convert.ToInt32(lblSlot.ToolTip);
                            int College_id = Convert.ToInt32(ddlInstitute.SelectedValue);
                            int Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                            ttno = hdnTTNO.Value == string.Empty ? 0 : Convert.ToInt32(hdnTTNO.Value);
                            alternatenomax = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(ISNULL(ALTERNATE_NO,0))", "SESSIONNO=" + sessionno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "MAX(ISNULL(ALTERNATE_NO,0))", "SESSIONNO=" + sessionno + " AND COLLEGE_ID=" + ddlInstitute.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue));
                            alternateno = alternatenomax + 1;
                            int OrgId = Convert.ToInt32(Session["OrgId"]);
                            if (i > 0 && j > 0)
                            {
                                if (slotno != translot)
                                {
                                    CustomStatus cs = (CustomStatus)objAtt.InsertAlternateAttendance(sessionno, attdate, deptno, lecturetype, slotno, schedulecourse,
                                                                                                    scheduleuano, takencourse, takenuano, cancel, transdate, ipaddress, uano,
                                                                                                   transfertype, semesterno, sectionno, batchno, swapno, translot, schemeno, ttno, alternateno, College_id, Degreeno, OrgId);
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updatt, "Scheduled Slot is same as Main Slot!", this.Page);
                                    return;
                                }
                            }

                            sessionno = Convert.ToInt32(lblDate.ToolTip);
                            attdate = Convert.ToDateTime(txtAttDate.Text);
                            deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())) == string.Empty ? "0" : (objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                            lecturetype = Convert.ToInt32(ddlAttType.SelectedValue);

                            foreach (ListViewDataItem dataitem1 in lvFacultyCourse.Items)
                            {
                                CheckBox chkFacCourse = dataitem1.FindControl("chkFacCourse") as CheckBox;
                                Label lblFacSlot = dataitem1.FindControl("lblFacSlot") as Label;

                                if (chkFacCourse.Enabled)
                                {
                                    if (chkFacCourse.Checked && chkFacCourse.Enabled)
                                    {
                                        j++;
                                        schedulecourse = Convert.ToInt32(chkFacCourse.ToolTip);
                                        // scheduleuano = Convert.ToInt32(ddlFaculty.SelectedValue);
                                        scheduleuano = uano_dept.Length > 0 && uano_dept != null ? Convert.ToInt32(uano_dept[0]) : 0;
                                        //scheduleuano = Convert.ToInt32(Session["userno"].ToString());
                                        takencourse = Convert.ToInt32(chkFacCourse.ToolTip);
                                        //  takenuano = Convert.ToInt32(ddlFaculty.SelectedValue);
                                        slotno = Convert.ToInt32(lblFacSlot.ToolTip);
                                        takenuano = Convert.ToInt32(Session["userno"].ToString());

                                        schemeno = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT SCHEMENO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT SCHEMENO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)));
                                        semesterno = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT SEMESTERNO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT SEMESTERNO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)));
                                        sectionno = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT SECTIONNO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT SECTIONNO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)));
                                        batchno = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT BATCHNO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT BATCHNO", "COURSENO=" + chkFacCourse.ToolTip + " AND ISNULL(CANCEL,0)=0 AND UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue)));
                                    }
                                }
                            }

                            foreach (ListViewDataItem dataitem1 in lvCourse.Items)
                            {
                                chkCourse = dataitem1.FindControl("chkCourse") as CheckBox;
                                lblSlot = dataitem1.FindControl("lblSlot") as Label;

                                if (chkCourse.Enabled)
                                {
                                    if (chkCourse.Checked && chkCourse.Enabled)
                                    {
                                        translot = Convert.ToInt32(lblSlot.ToolTip);
                                    }
                                }
                            }

                            cancel = 0;
                            transdate = DateTime.Now;
                            hostName = Dns.GetHostName();
                            ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                            uano = Convert.ToInt32(Session["userno"].ToString());
                            transfertype = Convert.ToInt32(ddlAttType.SelectedValue);
                            OrgId = Convert.ToInt32(Session["OrgId"]);
                            if (j > 0)
                            {
                                CustomStatus cs = 0;
                                if (slotno != translot)
                                {
                                    cs = (CustomStatus)objAtt.InsertAlternateAttendance(sessionno, attdate, deptno, lecturetype, slotno, schedulecourse,
                                                                                                      scheduleuano, takencourse, takenuano, cancel, transdate, ipaddress, uano,
                                                                                                       transfertype, semesterno, sectionno, batchno, swapno, translot, schemeno, ttno, alternateno, College_id, Degreeno, OrgId);
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updatt, "Subject already exists for selected slot!", this.Page);
                                }

                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayMessage(updatt, ddlAttType.SelectedItem.Text + " Subject Transfer Successful!", this.Page);
                                    done = 0;

                                    if (ddlAttType.SelectedValue == "4")
                                    {
                                        BindListViewSelectedFaculty();
                                    }

                                    BindListViewCourseSlot();
                                }
                            }
                        }
                    }
                }
            }

            if (i == 0)
            {
                objCommon.DisplayMessage(updatt, "Please Select Subject from Subject List!", this.Page);
            }

            if (j == 0)
            {
                objCommon.DisplayMessage(updatt, "Please Select Subject of Selected Faculty!", this.Page);
            }

        }
        catch
        {
            throw;
        }
    }

    // Creates calendar
    void MakeCalender()
    {
        string textToWrite = "";
        string highlitedDates = "";

        string dayName = "", monthName = "", dateVal = "", yearVal = "";

        string fHeader = "function detect_sunday(sender, args) { var SD = sender._selectedDate;";

        string fBody1 = "";

        string fBody2 = "";

        string curdate = txtAttDate.Text;

        string fFooter = @"var i; for (i = 0; i < CD.length; i++) { var NSD = SD.getDate() + ""/"" + SD.getMonth() + ""/"" + SD.getFullYear(); var NCD = CD[i].getDate() + ""/"" + CD[i].getMonth() + ""/"" + CD[i].getFullYear(); if (NSD == NCD) { alert(""You cannot allot attendance for holiday - ""+EN[i]+"" !""); sender._selectedDate = new Date(0); sender._textbox.set_Value(""curdate""); break; } } }";

        DataSet dsHoliday = objCommon.FillDropDown("ACD_ACADEMIC_HOLIDAY_MASTER", "ACADEMIC_HOLIDAY_STDATE", "ACADEMIC_HOLIDAY_NAME", "", "HOLIDAY_NO");
        if (dsHoliday != null && dsHoliday.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsHoliday.Tables[0].Rows.Count; i++)
            {
                DateTime ExamDate = Convert.ToDateTime(dsHoliday.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_STDATE"].ToString());
                String Holiday = dsHoliday.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString();

                dayName = ExamDate.ToString("dddd");
                monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ExamDate.Month);
                dateVal = Convert.ToString(ExamDate.ToString("dd"));
                yearVal = Convert.ToString(ExamDate.Year);

                if (i == 0)
                {
                    fBody1 += "var CD = [";
                    fBody2 += "var EN = [";
                    highlitedDates += "function changeCalenderDateColor(sender, args) { var dateText = [";
                }
                if (i < dsHoliday.Tables[0].Rows.Count - 1)
                {
                    fBody1 += "new Date('" + ExamDate.ToString("yyyy/MM/dd") + "'),";
                    fBody2 += @"""" + Holiday + @""",";
                    highlitedDates += @"""" + dayName + ", " + monthName + " " + dateVal + ", " + yearVal + "\",";
                }
                else
                {
                    fBody1 += "new Date('" + ExamDate.ToString("yyyy/MM/dd") + "')];";
                    fBody2 += @"""" + Holiday + @"""];";
                    highlitedDates += @"""" + dayName + ", " + monthName + " " + dateVal + ", " + yearVal + "\"];";
                    highlitedDates += @"var i; for (i = 0; i < dateText.length; i++) { $(""div[title='"" + dateText[i] + ""']"").css(""color"", ""black""); }}";
                }

            }
        }

        textToWrite = fHeader + fBody1 + fBody2 + fFooter + highlitedDates;
        ScriptManager.RegisterStartupScript(this, GetType(), "script", textToWrite, true);
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvFacultyCourse.DataSource = null;
        lvFacultyCourse.DataBind();
        divddldept.Visible = false;
        lvCourse.Visible = false;
        ddlAttType.SelectedIndex = 0;
        lvFacultyCourse.Visible = false;
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)DEGREENO", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ") AND COLLEGE_ID=" + ddlInstitute.SelectedValue, "DEGREENO");
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D  INNER JOIN ACD_SCHEME SC ON (D.DEGREENO=SC.DEGREENO) INNER JOIN ACD_COURSE_TEACHER  CT ON (CT.SCHEMENO=SC.SCHEMENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "ISNULL(CANCEL,0)=0 AND CDB.COLLEGE_ID = " + ddlInstitute.SelectedValue + " AND UA_NO = " + Session["userno"], "");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvFacultyCourse.DataSource = null;
        lvFacultyCourse.DataBind();
        divddldept.Visible = false;
        lvCourse.Visible = false;
        ddlAttType.SelectedIndex = 0;
        lvFacultyCourse.Visible = false;
    }
    protected void ddlInstitute1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlDegree1, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ") AND COLLEGE_ID=" + ddlInstitute1.SelectedValue, "D.DEGREENO");
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree1, "ACD_DEGREE D  INNER JOIN ACD_SCHEME SC ON (D.DEGREENO=SC.DEGREENO) INNER JOIN ACD_COURSE_TEACHER  CT ON (CT.SCHEMENO=SC.SCHEMENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "ISNULL(CANCEL,0)=0 AND CDB.COLLEGE_ID = " + ddlInstitute1.SelectedValue + " AND UA_NO = " + Session["userno"], "");
        }
    }
}